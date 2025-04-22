using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using r_framework.Const;
using r_framework.Utility;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Const;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.Utility
{
    internal class CsvPatternManager
    {
        /// <summary>
        /// 伝種区分と出力項目リストのハッシュマップ
        /// </summary>
        internal List<CsvPatternSetting> PatternSettings { get; private set; }

        /// <summary>
        /// インスタンス
        /// </summary>
        private static readonly Lazy<CsvPatternManager> manager = new Lazy<CsvPatternManager>(() => new CsvPatternManager());

        /// <summary>
        ///
        /// </summary>
        private CsvPatternManager()
        {
            this.PatternSettings = new List<CsvPatternSetting>();
        }

        /// <summary>
        /// 指定した伝種区分のパターンデータをコレクションに追加します
        /// </summary>
        /// <param name="denshuKbn"></param>
        internal static List<CsvPatternSetting> GetCsvPatternSetting(int haniKbn, List<DENSHU_KBN> denshuKbns)
        {
            var settings = new List<CsvPatternSetting>();
            foreach (var denshuKbn in denshuKbns)
            {
                var settingz = manager.Value.PatternSettings.Where(x => x.HaniKbn == haniKbn && x.DenshuKbn == denshuKbn);
                if (settingz == null || settingz.Count() == 0)
                {
                    settingz = CsvPatternSetting.Create(haniKbn, denshuKbn);
                    manager.Value.PatternSettings.AddRange(settingz);
                }
                settings.AddRange(settingz);
            }
            return settings;
        }

        /// <summary>
        ///
        /// </summary>
        internal class CsvPatternSetting
        {
            /// <summary>
            /// 範囲区分
            /// </summary>
            internal int HaniKbn { get; private set; }

            /// <summary>
            /// 伝種区分
            /// </summary>
            internal DENSHU_KBN DenshuKbn { get; private set; }

            /// <summary>
            /// 出力区分毎の出力項目リスト
            /// </summary>
            /// <remarks>現段階は伝票と明細2つのグループを想定</remarks>
            internal List<OutputGroup> OutputGroups { get; private set; }

            /// <summary>
            ///
            /// </summary>
            internal JoinCondition FromCondition { get; private set; }

            /// <summary>
            /// Join条件リスト
            /// </summary>
            internal List<JoinCondition> JoinConditions { get; private set; }

            /// <summary>
            /// テーブルリスト
            /// </summary>
            /// <remarks>テーブル名重複しないは前提</remarks>
            private List<string> Tables;

            /// <summary>
            /// コンストラクタ
            /// </summary>
            private CsvPatternSetting(int haniKbn, DENSHU_KBN denshuKbn)
            {
                this.HaniKbn = haniKbn;
                this.DenshuKbn = denshuKbn;

                // 伝票別と明細別のカラムリストを初期化
                this.OutputGroups = new List<OutputGroup>();

                //
                this.JoinConditions = new List<JoinCondition>();

                // テーブルリストとJoin条件リストを初期化
                this.Tables = new List<string>();
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="haniKbn"></param>
            /// <param name="denshuKbn"></param>
            /// <returns></returns>
            internal static List<CsvPatternSetting> Create(int haniKbn, DENSHU_KBN denshuKbn)
            {
                LogUtility.DebugMethodStart(haniKbn, denshuKbn);
                var settings = new List<CsvPatternSetting>();

                var xmlPath = UIConstants.PATTERN_SETTING_XML_PATHS.SingleOrDefault(x => x.Item1 == haniKbn && x.Item2 == denshuKbn);

                var assembly = Assembly.GetExecutingAssembly();
                var resourceNames = assembly.GetManifestResourceNames();

                if (xmlPath != null &&
                    resourceNames != null && resourceNames.Length > 0)
                {
                    foreach (var file in xmlPath.Item3)
                    {
                        var xmlFullName = resourceNames.FirstOrDefault(f => f.EndsWith(string.Format("{0:000}_{1}.xml", haniKbn, file)));
                        LogUtility.Info(xmlFullName);
                        if (string.IsNullOrWhiteSpace(xmlFullName))
                            continue;

                        using (var resource = assembly.GetManifestResourceStream(xmlFullName))
                        {
                            var setting = new CsvPatternSetting(haniKbn, denshuKbn);

                            // 出力項目情報設定
                            var rootElement = XElement.Load(resource);

                            //
                            var selectElement = rootElement.Element(CsvPatternNode.Element.OutputColumnSelect);
                            if (selectElement == null)
                                continue;
                            var selectHaniKbnAttribute = selectElement.Attribute(CsvPatternNode.Attribute.HaniKbn);
                            var selectHaniKbn = 0;
                            if (selectHaniKbnAttribute == null ||
                                !int.TryParse(selectHaniKbnAttribute.Value, out selectHaniKbn) || selectHaniKbn != haniKbn)
                                continue;

                            // グループ(伝票又は明細)
                            var groupElements = selectElement.Elements(CsvPatternNode.Element.Group);
                            foreach (var groupElement in groupElements)
                            {
                                var groupOutputKbnAttribute = groupElement.Attribute(CsvPatternNode.Attribute.OutputKbn);
                                var groupOutputKbn = 0;
                                if (groupOutputKbnAttribute == null || !int.TryParse(groupOutputKbnAttribute.Value, out groupOutputKbn))
                                    continue;

                                var outputGroup = new OutputGroup(groupOutputKbn);
                                var columnElements = groupElement.Elements(CsvPatternNode.Element.Column);
                                foreach (var columnElement in columnElements)
                                {
                                    // 項目ID
                                    var columnIdAttribute = columnElement.Attribute(CsvPatternNode.Attribute.Id);
                                    var columnId = 0;
                                    if (columnIdAttribute == null || !int.TryParse(columnIdAttribute.Value, out columnId))
                                        continue;
                                    // 表示順
                                    var columnDispNumberAttribute = columnElement.Attribute(CsvPatternNode.Attribute.DispNumber);
                                    var columnDispNumber = 0;
                                    if (columnDispNumberAttribute == null || !int.TryParse(columnDispNumberAttribute.Value, out columnDispNumber))
                                        // 表示順が数字に変換できない場合は項目IDを入れる
                                        columnDispNumber = columnId;
                                    // 表示名
                                    var columnDispNameAttribute = columnElement.Attribute(CsvPatternNode.Attribute.DispName);
                                    var columnDispName =
                                        columnDispNameAttribute == null ? string.Empty : columnDispNameAttribute.Value;
                                    // テーブル名
                                    var columnTableNameAttribute = columnElement.Attribute(CsvPatternNode.Attribute.TableName);
                                    var columnTableName =
                                        columnTableNameAttribute == null ? string.Empty : columnTableNameAttribute.Value;
                                    // 物理名
                                    var columnNameAttribute = columnElement.Attribute(CsvPatternNode.Attribute.Name);
                                    var columnName =
                                        columnNameAttribute == null ? string.Empty : columnNameAttribute.Value; // Null?
                                    // 書式
                                    var columnFormatAttribute = columnElement.Attribute(CsvPatternNode.Attribute.Format);
                                    var columnFormat =
                                        columnFormatAttribute == null ? string.Empty : columnFormatAttribute.Value;
                                    outputGroup.OutputColumns.Add(
                                        new OutputGroup.OutputColumn(columnId, columnDispNumber, columnDispName, columnTableName, columnName, columnFormat)
                                        );
                                }
                                setting.OutputGroups.Add(outputGroup);
                                setting.Tables.AddRange(outputGroup.OutputColumns.Select(x => x.TableName).Distinct());
                            }

                            // JOIN条件の設定
                            var joinElements = selectElement.Elements(CsvPatternNode.Element.JoinCondition);
                            foreach (var joinElement in joinElements)
                            {
                                var joinTableNameAttribute = joinElement.Attribute(CsvPatternNode.Attribute.TableName);
                                var joinTableName = string.Empty;
                                if (joinTableNameAttribute == null || string.IsNullOrWhiteSpace(joinTableNameAttribute.Value))
                                    continue;
                                else
                                    joinTableName = joinTableNameAttribute.Value;

                                var joinQuery = joinElement.Value; // JOINクエリ

                                setting.JoinConditions.Add(new JoinCondition(joinTableName, joinQuery));
                                setting.Tables.Add(joinTableName);
                            }

                            // 主テーブル名の設定
                            var fromElement = selectElement.Element(CsvPatternNode.Element.FromCondition);
                            if (fromElement != null)
                            {
                                var fromTableNameAttribute = fromElement.Attribute(CsvPatternNode.Attribute.TableName);
                                if (fromTableNameAttribute != null && !string.IsNullOrWhiteSpace(fromTableNameAttribute.Value))
                                {
                                    var fromTableName = fromTableNameAttribute.Value;
                                    var fromQuery = fromElement.Value;
                                    setting.FromCondition = new JoinCondition(fromTableName, fromQuery);
                                    setting.Tables.Add(fromTableName);
                                }
                            }

                            // 重複テーブル名を削除
                            var tables = setting.Tables.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct().ToList();
                            setting.Tables.Clear();
                            setting.Tables.AddRange(tables);
                            setting.Tables.Insert(0, string.Empty);

                            settings.Add(setting);
                        }
                    }
                }

                LogUtility.DebugMethodEnd(settings);
                return settings;
            }

            /// <summary>
            ///
            /// </summary>
            public class JoinCondition : IComparable, IComparable<JoinCondition>
            {
                /// <summary>
                ///
                /// </summary>
                internal string Table { get; private set; }

                /// <summary>
                ///
                /// </summary>
                internal string Query { get; private set; }

                /// <summary>
                ///
                /// </summary>
                /// <param name="dtPatterns"></param>
                /// <param name="queries"></param>
                public JoinCondition(string table, string query)
                {
                    this.Table = table;
                    this.Query = query;
                }

                /// <summary>
                ///
                /// </summary>
                /// <param name="obj"></param>
                /// <returns></returns>
                public int CompareTo(object obj)
                {
                    // 引数がNull又はJoinConditionではない場合はArgumentNullExceptionをスローする
                    var other = obj as JoinCondition;
                    if (other == null)
                        throw new ArgumentNullException();

                    return this.CompareTo(other);
                }

                /// <summary>
                ///
                /// </summary>
                /// <param name="other"></param>
                /// <returns></returns>
                public int CompareTo(JoinCondition other)
                {
                    return this.Table.CompareTo(other.Table);
                }
            }

            /// <summary>
            ///
            /// </summary>
            public class OutputGroup : IComparable, IComparable<OutputGroup>
            {
                /// <summary>
                ///
                /// </summary>
                public int OutputKbn { get; private set; }

                /// <summary>
                ///
                /// </summary>
                public List<OutputColumn> OutputColumns { get; private set; }

                /// <summary>
                ///
                /// </summary>
                /// <param name="groupOutputKbn"></param>
                public OutputGroup(int outputKbn)
                {
                    this.OutputKbn = outputKbn;
                    this.OutputColumns = new List<OutputColumn>();
                }

                /// <summary>
                ///
                /// </summary>
                /// <param name="obj"></param>
                /// <returns></returns>
                public int CompareTo(object obj)
                {
                    // 引数がNull又はOutputGroupではない場合はArgumentNullExceptionをスローする
                    var other = obj as OutputGroup;
                    if (other == null)
                        throw new ArgumentNullException();

                    return this.CompareTo(other);
                }

                /// <summary>
                ///
                /// </summary>
                /// <param name="other"></param>
                /// <returns></returns>
                public int CompareTo(OutputGroup other)
                {
                    return this.OutputKbn.CompareTo(other.OutputKbn);
                }

                /// <summary>
                ///
                /// </summary>
                internal class OutputColumn : IComparable, IComparable<OutputColumn>
                {
                    /// <summary>
                    ///
                    /// </summary>
                    public int Id { get; private set; }

                    /// <summary>
                    ///
                    /// </summary>
                    public int DispNumber { get; private set; }

                    /// <summary>
                    ///
                    /// </summary>
                    public string DispName { get; private set; }

                    /// <summary>
                    ///
                    /// </summary>
                    public string TableName { get; private set; }

                    /// <summary>
                    ///
                    /// </summary>
                    public string Name { get; private set; }

                    /// <summary>
                    ///
                    /// </summary>
                    public string Format { get; private set; }

                    /// <summary>
                    ///
                    /// </summary>
                    /// <param name="id"></param>
                    /// <param name="dispNumber"></param>
                    /// <param name="dispName"></param>
                    /// <param name="tableName"></param>
                    /// <param name="name"></param>
                    /// <param name="format"></param>
                    public OutputColumn(int id, int dispNumber, string dispName, string tableName, string name, string format)
                    {
                        this.Id = id;
                        this.DispNumber = dispNumber;
                        this.DispName = dispName;
                        this.TableName = tableName;
                        this.Name = name;
                        this.Format = format;
                    }

                    public int CompareTo(object obj)
                    {
                        // 引数がNull又はOutputGroupではない場合はArgumentNullExceptionをスローする
                        var other = obj as OutputColumn;
                        if (other == null)
                            throw new ArgumentNullException();

                        return this.CompareTo(other);
                    }

                    public int CompareTo(OutputColumn other)
                    {
                        return this.Id.CompareTo(other.Id);
                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        internal class CsvPatternNode
        {
            #region 要素名

            internal class Element
            {
                /// <summary>
                /// Root
                /// </summary>
                internal static readonly string Root = "rootElement";

                /// <summary>
                /// OutputColumnSelect
                /// </summary>
                internal static readonly string OutputColumnSelect = "OutputColumnSelect";

                /// <summary>
                /// Group
                /// </summary>
                internal static readonly string Group = "Group";

                /// <summary>
                /// Column
                /// </summary>
                internal static readonly string Column = "Column";

                /// <summary>
                /// FromCondition
                /// </summary>
                internal static readonly string FromCondition = "FromCondition";

                /// <summary>
                /// JoinCondition
                /// </summary>
                internal static readonly string JoinCondition = "JoinCondition";
            }

            #endregion

            #region 属性名

            internal class Attribute
            {
                /// <summary>
                /// Kbn
                /// </summary>
                internal static readonly string HaniKbn = "Kbn";

                /// <summary>
                /// DenshuKbn
                /// </summary>
                internal static readonly string DenshuKbn = "DenshuKbn";

                /// <summary>
                /// OutputKbn
                /// </summary>
                internal static readonly string OutputKbn = "OutputKbn";

                /// <summary>
                /// DispName
                /// </summary>
                internal static readonly string DispName = "DispName";

                /// <summary>
                /// Name
                /// </summary>
                internal static readonly string Name = "Name";

                /// <summary>
                /// TableName
                /// </summary>
                internal static readonly string TableName = "TableName";

                /// <summary>
                /// Format
                /// </summary>
                internal static readonly string Format = "Format";

                /// <summary>
                /// Id
                /// </summary>
                internal static readonly string Id = "ID";

                /// <summary>
                /// DispNum
                /// </summary>
                internal static readonly string DispNumber = "DispNumber";
            }

            #endregion
        }
    }
}