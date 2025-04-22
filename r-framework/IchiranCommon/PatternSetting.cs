using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using r_framework.Utility;
using Shougun.Core.Common.IchiranCommon.Const;
using Shougun.Core.Common.IchiranCommon.Dto;
using r_framework.Dto;

namespace Shougun.Core.Common.IchiranCommon
{
    internal class PatternSetting
    {
        /// <summary>
        /// 伝種区分CD
        /// </summary>
        internal int DenshuKbn;

        /// <summary>
        /// 出力区分毎の出力項目リスト
        /// </summary>
        internal Dictionary<int, OutputGroup> OutputGroups;

        /// <summary>
        /// JOIN条件のリスト
        /// </summary>
        internal Dictionary<int, JoinCondition> JoinConditions;

        /// <summary>
        /// テーブルIDとテーブル名のハッシュテーブル
        /// </summary>
        internal Dictionary<int, string> TableMap;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal PatternSetting(int denshuKbn)
        {
            this.DenshuKbn = denshuKbn;
            this.OutputGroups = new Dictionary<int, OutputGroup>();
            this.JoinConditions = new Dictionary<int, JoinCondition>();
            this.TableMap = new Dictionary<int, string>();
            this.TableMap[0] = string.Empty;
        }

        /// <summary>
        /// テーブルIDからテーブル名を取得します
        /// </summary>
        /// <param name="tableID">テーブルID</param>
        /// <returns>テーブル名</returns>
        internal string GetTableName(int tableID)
        {
            var tableName = string.Empty;

            if (this.TableMap.ContainsKey(tableID))
            {
                tableName = this.TableMap[tableID];
            }

            return tableName;
        }

        /// <summary>
        /// 出力区分と項目IDから項目を取得します。
        /// </summary>
        /// <param name="outputKbn"></param>
        /// <param name="columnID"></param>
        /// <returns></returns>
        internal OutputColumn GetColumn(int outputKbn, int columnID)
        {
            if (this.OutputGroups.ContainsKey(outputKbn) && this[outputKbn].OutputColumns.ContainsKey(columnID))
            {
                return this[outputKbn, columnID];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 出力区分と項目IDから項目を取得します。
        /// </summary>
        /// <param name="outputKbn"></param>
        /// <param name="columnID"></param>
        /// <param name="patternKbn"></param>
        /// <returns></returns>
        internal OutputColumn GetColumn(int outputKbn, int columnID, int patternKbn)
        {
            if (outputKbn <= patternKbn)
            {
                return this.GetColumn(outputKbn, columnID);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 出力区分と項目IDから項目を取得します。
        /// </summary>
        /// <param name="outputKbn"></param>
        /// <param name="columnID"></param>
        /// <returns></returns>
        internal OutputColumn GetColumn(SqlInt16 outputKbn, SqlInt32 columnID)
        {

            if (!outputKbn.IsNull && !columnID.IsNull)
            {
                int kbn = (int)outputKbn;
                int id = (int)columnID;

                return this.GetColumn(kbn, id);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 出力区分と項目IDから項目を取得します。
        /// </summary>
        /// <param name="outputKbn"></param>
        /// <param name="columnID"></param>
        /// <param name="patternKbn"></param>
        /// <returns></returns>
        internal  OutputColumn GetColumn(SqlInt16 outputKbn, SqlInt32 columnID, SqlInt16 patternKbn)
        {
            if (!outputKbn.IsNull && !patternKbn.IsNull && outputKbn <= patternKbn)
            {
                return this.GetColumn(outputKbn, columnID);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 項目表示名から書式情報を取得します。
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        internal string GetFormat(string columnName)
        {
            var col = this.OutputGroups.SelectMany(n => n.Value.OutputColumns).FirstOrDefault(n => n.Value.DispName == columnName);

            if (col.Value == null)
            {
                return string.Empty;
            }
            else
            {
                return col.Value.Format;
            }
        }

        /// <summary>
        /// テーブル名からテーブルIDを取得します。
        /// 取得できなかった場合、テーブル名を新たにハッシュマップに追加し、そのIDを返します。
        /// </summary>
        /// <param name="tableName">テーブル名</param>
        /// <returns>テーブルID</returns>
        private int GetAndSetTableID(string tableName)
        {
            var tableID = 0;

            if (this.TableMap.ContainsValue(tableName))
            {
                tableID = this.TableMap.FirstOrDefault(n => n.Value == tableName).Key;
            }
            else
            {
                tableID = this.TableMap.Count;
                this.TableMap[this.TableMap.Count] = tableName;
            }

            return tableID;
        }

        /// <summary>
        /// Xmlを読み込みます。
        /// </summary>
        internal bool LoadSetting()
        {
            LogUtility.DebugMethodStart();

            try
            {
                // リソース名
                var fileName = string.Empty;

                // 現在のコードを実行しているAssemblyを取得
                Assembly assembly = Assembly.GetExecutingAssembly();
                //このアセンブリの全てのリソース名を取得する
                string[] resources = assembly.GetManifestResourceNames();
                // 伝種区分CDからファイル名を取得
                var denshuKbnStr = string.Format("_{0}.xml", this.DenshuKbn.ToString("00000"));
                fileName = resources.FirstOrDefault(n => n.ToLower().EndsWith(denshuKbnStr));

                if (string.IsNullOrWhiteSpace(fileName))
                {
                    // 見つからない場合は失敗
                    LogUtility.Error("伝種区分に対応したXmlファイルが見つかりませんでした。");
                    return false;
                }

                using (var rs = assembly.GetManifestResourceStream(fileName))
                {
                    // 出力項目情報設定
                    var element = XElement.Load(rs);
                    var groups = element.Descendants(XmlTexts.Group).Where(n => n.NodeType == XmlNodeType.Element);
                    foreach (var group in groups)
                    {
                        var outputKbn = group.Attribute(XmlTexts.OutputKbn);
                        int kbnCd;                                                                  // 出力区分
                        if (outputKbn == null || !int.TryParse(outputKbn.Value, out kbnCd))
                        {
                            continue;
                        }

                        this.OutputGroups.Add(kbnCd, new OutputGroup(kbnCd));
                        foreach (var column in group.Descendants(XmlTexts.Column))
                        {
                            int columnID;                                                           // 項目ID
                            var id = GetXmlAttributeValue(column, XmlTexts.ID);
                            if (!int.TryParse(id, out columnID))
                            {
                                // 項目IDが数字に変換できない場合はスルー
                                continue;
                            }

                            int dispNum;
                            var num = GetXmlAttributeValue(column, XmlTexts.DispNumber);            // 表示順
                            if (!int.TryParse(num, out dispNum))
                            {
                                // 表示順が数字に変換できない場合は項目IDを入れる
                                dispNum = columnID;
                            }

                            var dispName = GetXmlAttributeValue(column, XmlTexts.DispName);         // 表示名
                            var name = GetXmlAttributeValue(column, XmlTexts.Name);                 // 物理名
                            var format = GetXmlAttributeValue(column, XmlTexts.Format);             // 書式
                            var tableName = GetXmlAttributeValue(column, XmlTexts.TableName);       // テーブル名
                            var needs = GetXmlAttributeValue(column, XmlTexts.Needs) == "1";        // 必須区分

                            // コレクションに追加
                            this.OutputGroups[kbnCd].Add(columnID,
                                new OutputColumn(kbnCd, columnID, dispNum, dispName, name, this.GetAndSetTableID(tableName), format, needs));
                        }
                    }

                    // JOIN条件の設定
                    var joinConditions = element.Descendants(XmlTexts.JoinCondition).Where(n => n.NodeType == XmlNodeType.Element);
                    foreach (var joinCondition in joinConditions)
                    {
                        var tableName = GetXmlAttributeValue(joinCondition, XmlTexts.TableName);    // テーブル名
                        if (!this.TableMap.ContainsValue(tableName))
                        {
                            // テーブルマップにないテーブル名だった場合はスルー 
                            continue;
                        }

                        var query = joinCondition.Value;                                            // JOINクエリ
                        int applyCheckType = 0;                                                     // 有効チェックタイプ 
                        int.TryParse(GetXmlAttributeValue(joinCondition, XmlTexts.ApplyCheckType), out applyCheckType);

                        // コレクションに追加
                        this.JoinConditions.Add(this.GetAndSetTableID(tableName), 
                            new JoinCondition(this.GetAndSetTableID(tableName), query));
                    }
                }

                //Communicate InxsSubApplication Start
                if (r_framework.Configuration.AppConfig.AppOptions.IsInxsUketsuke())
                {
                    // リソース名
                    var fileNameInxs = string.Empty;
                    // 伝種区分CDからファイル名を取得
                    var denshuKbnStrInxs = string.Format("_{0}_inxs.xml", this.DenshuKbn.ToString("00000"));
                    fileNameInxs = resources.FirstOrDefault(n => n.ToLower().EndsWith(denshuKbnStrInxs));

                    if (!string.IsNullOrWhiteSpace(fileNameInxs) && !string.IsNullOrEmpty(SystemProperty.AppSettingInxs.BusinessType))
                    {
                        using (var rs = assembly.GetManifestResourceStream(fileNameInxs))
                        {
                            // 出力項目情報設定
                            var element = XElement.Load(rs);
                            var inxsHyoujunTemplate = element.Descendants(XmlTexts.InxsHyoujunTemplate).Where(n => n.NodeType == XmlNodeType.Element
                                                                                                                   && (string)n.Attribute("Cd") == SystemProperty.AppSettingInxs.BusinessType).FirstOrDefault();
                            if (inxsHyoujunTemplate != null)
                            {
                                var groups = inxsHyoujunTemplate.Descendants(XmlTexts.Group).Where(n => n.NodeType == XmlNodeType.Element);
                                foreach (var group in groups)
                                {
                                    var outputKbn = group.Attribute(XmlTexts.OutputKbn);
                                    int kbnCd;                                                                  // 出力区分
                                    if (outputKbn == null || !int.TryParse(outputKbn.Value, out kbnCd))
                                    {
                                        continue;
                                    }

                                    if (!this.OutputGroups.ContainsKey(kbnCd))
                                    {
                                        this.OutputGroups.Add(kbnCd, new OutputGroup(kbnCd));
                                    }
                                    foreach (var column in group.Descendants(XmlTexts.Column))
                                    {
                                        int columnID;                                                           // 項目ID
                                        var id = GetXmlAttributeValue(column, XmlTexts.ID);
                                        if (!int.TryParse(id, out columnID))
                                        {
                                            // 項目IDが数字に変換できない場合はスルー
                                            continue;
                                        }

                                        int dispNum;
                                        var num = GetXmlAttributeValue(column, XmlTexts.DispNumber);            // 表示順
                                        if (!int.TryParse(num, out dispNum))
                                        {
                                            // 表示順が数字に変換できない場合は項目IDを入れる
                                            dispNum = columnID;
                                        }

                                        var dispName = GetXmlAttributeValue(column, XmlTexts.DispName);         // 表示名
                                        var name = GetXmlAttributeValue(column, XmlTexts.Name);                 // 物理名
                                        var format = GetXmlAttributeValue(column, XmlTexts.Format);             // 書式
                                        var tableName = GetXmlAttributeValue(column, XmlTexts.TableName);       // テーブル名
                                        var needs = GetXmlAttributeValue(column, XmlTexts.Needs) == "1";        // 必須区分

                                        // コレクションに追加
                                        this.OutputGroups[kbnCd].Add(columnID,
                                            new OutputColumn(kbnCd, columnID, dispNum, dispName, name, this.GetAndSetTableID(tableName), format, needs, true)); //True => Column of Inxs
                                    }
                                }

                                // JOIN条件の設定
                                var joinConditions = inxsHyoujunTemplate.Descendants(XmlTexts.JoinCondition).Where(n => n.NodeType == XmlNodeType.Element);
                                foreach (var joinCondition in joinConditions)
                                {
                                    var tableName = GetXmlAttributeValue(joinCondition, XmlTexts.TableName);    // テーブル名
                                    if (!this.TableMap.ContainsValue(tableName))
                                    {
                                        // テーブルマップにないテーブル名だった場合はスルー 
                                        continue;
                                    }

                                    var query = joinCondition.Value;                                            // JOINクエリ
                                    int applyCheckType = 0;                                                     // 有効チェックタイプ 
                                    int.TryParse(GetXmlAttributeValue(joinCondition, XmlTexts.ApplyCheckType), out applyCheckType);

                                    // コレクションに追加
                                    this.JoinConditions.Add(this.GetAndSetTableID(tableName),
                                        new JoinCondition(this.GetAndSetTableID(tableName), query));
                                }
                            }
                        }
                    }
                }
                //Communicate InxsSubApplication End

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// XElementから指定した名称の属性の値を取得します
        /// </summary>
        /// <param name="ele">対象のXElement</param>
        /// <param name="name">属性名</param>
        /// <returns>値（見つからなければ空白を返す）</returns>
        private static string GetXmlAttributeValue(XElement ele, string name)
        {
            var res = string.Empty;

            var attribute = ele.Attribute(name);

            if (attribute != null)
            {
                res = attribute.Value;
            }

            return res;
        }

        #region インデクサ

        /// <summary>
        /// 出力区分によって出力項目へアクセスするインデクサ
        /// </summary>
        /// <param name="outputKbn">出力区分によって</param>
        /// <returns>出力項目へ</returns>
        internal OutputGroup this[int outputKbn]
        {
            get
            {
                return this.OutputGroups[outputKbn];
            }

            set
            {
                this.OutputGroups[outputKbn] = value;
            }
        }

        /// <summary>
        /// 出力区分と項目IDによって出力項目へアクセスするインデクサ
        /// </summary>
        /// <param name="outputKbn">出力区分</param>
        /// <param name="columnID">項目ID</param>
        /// <returns>出力項目</returns>
        internal OutputColumn this[int outputKbn, int columnID]
        {
            get
            {
                return this.OutputGroups[outputKbn].OutputColumns[columnID];
            }

            set
            {
                this.OutputGroups[outputKbn].OutputColumns[columnID] = value;
            }
        }

        #endregion
    }
}
