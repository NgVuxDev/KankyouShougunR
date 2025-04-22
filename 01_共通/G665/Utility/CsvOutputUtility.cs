using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Shougun.Core.Common.HanyoCSVShutsuryoku.Const;
using Shougun.Core.Common.HanyoCSVShutsuryoku.DAO;
using Shougun.Core.Common.HanyoCSVShutsuryoku.DTO;
using System;

namespace Shougun.Core.Common.HanyoCSVShutsuryoku.Utility
{
    /// <summary>
    ///
    /// </summary>
    internal class CsvOutputUtility
    {
        /// <summary>
        ///
        /// </summary>
        private SuperForm form;

        /// <summary>
        ///
        /// </summary>
        private JokenDto joken;

        /// <summary>
        ///
        /// </summary>
        private PatternDto pattern;

        /// <summary>
        ///
        /// </summary>
        private CsvOutputDao dao;

        /// <summary>
        ///
        /// </summary>
        /// <param name="column"></param>
        public CsvOutputUtility(SuperForm form, JokenDto joken, PatternDto pattern)
        {
            LogUtility.DebugMethodStart(form, joken, pattern);

            this.form = form;
            this.joken = joken;
            this.pattern = pattern;
            this.dao = DaoInitUtility.GetComponent<CsvOutputDao>();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        ///
        /// </summary>
        internal void CsvOutput()
        {
            List<string> aliases;
            Dictionary<string, string> formats;
            var queries = this.SqlCreate(out aliases, out formats);

            var results = new List<DataTable>();
            foreach (var query in queries)
            {
                LogUtility.Info(query);
                results.Add(dao.GetCsvOutputData(query));
            }
            if (results.Count > 0)
            {
                // 出力用テーブル
                var merged = new DataTable();

                // 検索結果の1個目テーブルを基準として、テーブル列を構成する
                var types = results.SelectMany(
                    t => t.Columns.OfType<DataColumn>()
                    ).GroupBy(
                    c => c.ColumnName
                    ).ToDictionary(
                    g => g.Key,
                    // XMLより生成した各列に、NULLである列が存在し、
                    // 該当列デフォルトのデータ型はintで、
                    // 他テーブルをマージするとエラーが発生するので、
                    // 各列に対し、全テーブルから該当列を検索し、
                    // intではないデータ型を優先設定する
                    g => g.Select(c => c.DataType).Where(x => x != typeof(int)).DefaultIfEmpty(typeof(int)).First()
                    );
                // 出力用のテーブルの列を生成
                for (int i = 0; i < results[0].Columns.Count; i++)
                    merged.Columns.Add(results[0].Columns[i].ColumnName, types[results[0].Columns[i].ColumnName]);

                // 全テーブルをマージ
                foreach (var result in results)
                    foreach (var row in result.Rows.OfType<DataRow>())
                        merged.ImportRow(row);

                // マージしたテーブルにソート順を適用
                var verged = merged.DefaultView;
                verged.Sort = string.Join(", ", aliases.ToArray());
                merged = verged.ToTable();

                // 出力列をフィルター
                var aliasez = CsvUtility.CsvColumns.CreateCustomColumns(aliases.ToArray());

                // 出力用各列フォーマットが指定された場合、該当フォーマットを適用
                //// 特別で、システムフォーマットを指定された場合、実フォーマット構造文に変換
                var j = 0;
                var columns = new string[formats.Count];
                var providers = new string[formats.Count];
                foreach (var format in formats)
                {
                    columns[j] = format.Key;
                    //switch (format.Value)
                    //{
                    //    case "1":
                    //        providers[j] = SystemProperty.Format.Suuryou;
                    //        break;

                    //    case "2":
                    //        providers[j] = SystemProperty.Format.Suuryou;
                    //        break;

                    //    case "3":
                    //        providers[j] = SystemProperty.Format.Tanka;
                    //        break;

                    //    default:
                    providers[j] = format.Value;
                    //        break;
                    //}
                    j++;
                }
                var formatz = CsvUtility.CsvFormats.CreateCustomFormats(columns, providers);

                // CSV出力
                if (new CsvUtility(
                    merged,
                    form,
                    fileName: string.Format(UIConstants.CSV_FILE_PREFIX_FORMAT, pattern.CsvPattern.PATTERN_NAME),
                    customColumns: aliasez,
                    customFormats: formatz,
                    outputHeader: true
                    ).Output())
                    MessageBox.Show("出力が完了しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="aliases"></param>
        /// <param name="formats"></param>
        /// <returns></returns>
        private List<string> SqlCreate(out List<string> aliases, out Dictionary<string, string> formats)
        {
            aliases = new List<string>();
            formats = new Dictionary<string, string>();

            var queries = new List<string>();
            var denshuKbns = new List<DENSHU_KBN>();
            switch (this.joken.HaniKbn)
            {
                case 1:
                    if (this.pattern.CsvPatternHanbaikanri != null)
                    {
                        if (this.pattern.CsvPatternHanbaikanri.DENPYOU_SHURUI_UKEIRE)
                            denshuKbns.Add(DENSHU_KBN.UKEIRE);
                        if (this.pattern.CsvPatternHanbaikanri.DENPYOU_SHURUI_SHUKKA)
                            denshuKbns.Add(DENSHU_KBN.SHUKKA);
                        if (this.pattern.CsvPatternHanbaikanri.DENPYOU_SHURUI_UR_SH)
                            denshuKbns.Add(DENSHU_KBN.URIAGE_SHIHARAI);
                        if (this.pattern.CsvPatternHanbaikanri.DENPYOU_SHURUI_DAINOU)
                            denshuKbns.Add(DENSHU_KBN.DAINOU);
                    }
                    break;

                case 2:
                    if (this.pattern.CsvPatternNyuushukkin != null)
                    {
                        if (this.pattern.CsvPatternNyuushukkin.DENPYOU_SHURUI_NYUUKIN)
                            denshuKbns.Add(DENSHU_KBN.NYUUKIN);
                        if (this.pattern.CsvPatternNyuushukkin.DENPYOU_SHURUI_SHUKKIN)
                            denshuKbns.Add(DENSHU_KBN.SHUKKIN);
                    }
                    break;

                default:
                    break;
            }

            var settings = CsvPatternManager.GetCsvPatternSetting(this.joken.HaniKbn, denshuKbns);
            for (int i = 0; i < settings.Count; i++)
            {
                var setting = settings[i];

                var selectClause = string.Empty;
                var fromClause = string.Empty;
                var joinClause = string.Empty;
                var orderClause = string.Empty;
                var whereClause = string.Empty;

                var tables = new List<string>();
                var selects = new Queue<string>();
                var joins = new List<string>();
                var orders = new Queue<string>();
                var wheres = new List<string>();

                #region SELECTとORDER句用配列生成(パターンXMLより)

                var outputColumns = setting.OutputGroups.SelectMany(
                    g => g.OutputColumns.Select(
                        c => new
                        {
                            g.OutputKbn,
                            OutputColumn = c
                        }));
                foreach (var mCsvPatternColumn in pattern.CsvPatternColumns.OrderBy(x => x.SORT_NO))
                {
                    var column = outputColumns.FirstOrDefault(
                        x => x.OutputKbn == mCsvPatternColumn.OUTPUT_KBN.Value && x.OutputColumn.Id == mCsvPatternColumn.KOUMOKU_ID.Value
                        );

                    if (column == null)
                        continue;

                    // SelectClause
                    var columnTable = column.OutputColumn.TableName;
                    var columnName = column.OutputColumn.Name;
                    if (!string.IsNullOrWhiteSpace(column.OutputColumn.TableName) && // テーブル名あり
                        !column.OutputColumn.Name.Contains(".")) // 項目名にテーブル名が含まれない
                    {
                        columnName = string.Format("{0}.{1}", columnTable, columnName);
                        tables.Add(columnTable);
                    }
                    selects.Enqueue(string.Format("{1} AS [{0}]", column.OutputColumn.DispName, columnName));

                    // OrderByClause
                    orders.Enqueue(string.Format("[{0}]", column.OutputColumn.DispName));

                    // 別名
                    if (!aliases.Contains(column.OutputColumn.DispName))
                        aliases.Add(column.OutputColumn.DispName);

                    // フォーマット
                    if (!string.IsNullOrWhiteSpace(column.OutputColumn.Format) && !formats.Keys.Contains(column.OutputColumn.DispName))
                        formats[column.OutputColumn.DispName] = column.OutputColumn.Format;
                }
                tables = tables.Distinct().ToList();

                // SYSTEM_IDとSEQ追加
                selects.Enqueue(string.Format("{0}.{1}", setting.FromCondition.Table, "SYSTEM_ID"));
                selects.Enqueue(string.Format("{0}.{1}", setting.FromCondition.Table, "SEQ"));

                // DETAIL_SYSTEM_ID追加(明細がある場合)
                if (!pattern.CsvPattern.OUTPUT_KBN.IsNull && pattern.CsvPattern.OUTPUT_KBN.Value == 2)
                {
                    var detailTable = setting.FromCondition.Table.Substring(0, setting.FromCondition.Table.IndexOf("_ENTRY")) + "_DETAIL";
                    selects.Enqueue(string.Format("{0}.{1}", detailTable, "DETAIL_SYSTEM_ID"));
                }

                #endregion

                #region FROM句配列生成(パターンXMLより)

                // FromConditionを利用するので処理不要

                #endregion

                #region 基本WHERE句配列生成(テーブル毎に固定)

                // 基本WHERE条件作成
                // 削除フラグ = 0
                wheres.Add(string.Format("{0}.DELETE_FLG = 0", setting.FromCondition.Table));
                // SEQ最大
                wheres.Add(string.Format("{1}.SEQ = (SELECT MAX(SEQMAX.SEQ) FROM {0} SEQMAX WHERE SEQMAX.SYSTEM_ID = {1}.SYSTEM_ID)",
                    !string.IsNullOrWhiteSpace(setting.FromCondition.Query) ? setting.FromCondition.Query : setting.FromCondition.Table,
                    setting.FromCondition.Table));

                #endregion

                #region パターンWHERE句配列生成(パターンDTOより)

                switch (this.joken.HaniKbn)
                {
                    case 1:
                        if (this.pattern.CsvPatternHanbaikanri != null)
                        {
                            // 取引区分
                            if (this.pattern.CsvPatternHanbaikanri.TORIHIKI_KBN != 3)
                            {
                                if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_URIAGE.IsTrue)
                                    wheres.Add(string.Format("{0} = {1}", "M_TORIHIKI_KBN1.TORIHIKI_KBN_CD", this.pattern.CsvPatternHanbaikanri.TORIHIKI_KBN));
                                if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_SHIHARAI.IsTrue)
                                    wheres.Add(string.Format("{0} = {1}", "M_TORIHIKI_KBN2.TORIHIKI_KBN_CD", this.pattern.CsvPatternHanbaikanri.TORIHIKI_KBN));
                            }

                            #region 売上支払と締状況の組み合わせ

                            // 売上支払チェック
                            var uriageshiharai = new List<string>();
                            if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_URIAGE.IsTrue)
                                uriageshiharai.Add(string.Format("{0} = 1", this.ColumnLocate("DENPYOU_KBN_CD", selects, setting)));
                            if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_SHIHARAI.IsTrue)
                                uriageshiharai.Add(string.Format("{0} = 2", this.ColumnLocate("DENPYOU_KBN_CD", selects, setting)));
                            wheres.Add(string.Format("({0})", string.Join(" OR ", uriageshiharai)));

                            if (setting.DenshuKbn != DENSHU_KBN.DAINOU) // 受入・出荷・売上/支払
                            {
                                // 確定区分(代納には確定区分は無い)
                                if (this.pattern.CsvPatternHanbaikanri.KAKUTEI_KBN != 3)
                                    wheres.Add(string.Format("{0} = {1}", this.ColumnLocate("KAKUTEI_KBN", selects, setting), this.pattern.CsvPatternHanbaikanri.KAKUTEI_KBN));

                                // 締状況区分
                                if (this.pattern.CsvPatternHanbaikanri.SHIME_SHORI_JOUKYOU == 1)
                                {
                                    if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_URIAGE.IsTrue)
                                        wheres.Add(string.Format("{0} = {1}", "T_SEIKYUU_DETAIL1.DENPYOU_SHURUI_CD", (int)setting.DenshuKbn));
                                    if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_SHIHARAI.IsTrue)
                                        wheres.Add(string.Format("{0} = {1}", "T_SEISAN_DETAIL1.DENPYOU_SHURUI_CD", (int)setting.DenshuKbn));
                                }
                                else if (this.pattern.CsvPatternHanbaikanri.SHIME_SHORI_JOUKYOU == 2)
                                {
                                    if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_URIAGE.IsTrue)
                                        wheres.Add(string.Format("({0} IS NULL OR {0} != {1})", "T_SEIKYUU_DETAIL1.DENPYOU_SHURUI_CD", (int)setting.DenshuKbn));
                                    if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_SHIHARAI.IsTrue)
                                        wheres.Add(string.Format("({0} IS NULL OR {0} != {1})", "T_SEISAN_DETAIL1.DENPYOU_SHURUI_CD", (int)setting.DenshuKbn));
                                }
                            }
                            else // 代納の場合、請求又は精算の一方のみ当たる、単独で処理
                            {
                                // 締状況区分
                                if (this.pattern.CsvPatternHanbaikanri.SHIME_SHORI_JOUKYOU == 1)
                                {
                                    if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_URIAGE.IsTrue
                                        && this.ColumnLocate("DENPYOU_SHURUI_CD", selects, setting) == "T_SEIKYUU_DETAIL1.DENPYOU_SHURUI_CD")
                                        wheres.Add(string.Format("{0} = {1}", "T_SEIKYUU_DETAIL1.DENPYOU_SHURUI_CD", (int)setting.DenshuKbn));
                                    if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_SHIHARAI.IsTrue
                                        && this.ColumnLocate("DENPYOU_SHURUI_CD", selects, setting) == "T_SEISAN_DETAIL1.DENPYOU_SHURUI_CD")
                                        wheres.Add(string.Format("{0} = {1}", "T_SEISAN_DETAIL1.DENPYOU_SHURUI_CD", (int)setting.DenshuKbn));
                                }
                                else if (this.pattern.CsvPatternHanbaikanri.SHIME_SHORI_JOUKYOU == 2)
                                {
                                    if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_URIAGE.IsTrue
                                        && this.ColumnLocate("DENPYOU_SHURUI_CD", selects, setting) == "T_SEIKYUU_DETAIL1.DENPYOU_SHURUI_CD")
                                        wheres.Add(string.Format("({0} IS NULL OR {0} != {1})", "T_SEIKYUU_DETAIL1.DENPYOU_SHURUI_CD", (int)setting.DenshuKbn));
                                    if (this.pattern.CsvPatternHanbaikanri.DENPYOU_KBN_SHIHARAI.IsTrue
                                        && this.ColumnLocate("DENPYOU_SHURUI_CD", selects, setting) == "T_SEISAN_DETAIL1.DENPYOU_SHURUI_CD")
                                        wheres.Add(string.Format("({0} IS NULL OR {0} != {1})", "T_SEISAN_DETAIL1.DENPYOU_SHURUI_CD", (int)setting.DenshuKbn));
                                }
                            }

                            #endregion
                        }
                        break;

                    case 2:
                        if (this.pattern.CsvPatternNyuushukkin != null)
                        {
                            if (this.pattern.CsvPatternNyuushukkin.SHIME_SHORI_JOUKYOU == 1)
                                wheres.Add(string.Format("{0} = {1}", this.ColumnLocate("DENPYOU_SHURUI_CD", selects, setting), (int)setting.DenshuKbn));
                            else if (this.pattern.CsvPatternNyuushukkin.SHIME_SHORI_JOUKYOU == 2)
                                wheres.Add(string.Format("({0} IS NULL OR {0} != {1})", this.ColumnLocate("DENPYOU_SHURUI_CD", selects, setting), (int)setting.DenshuKbn));
                        }
                        break;

                    default:
                        break;
                }

                #endregion

                #region 出力条件WHERE句配列生成(出力条件より)

                // 拠点(共通必須)
                var kyotenCd = (short)-1;
                var kyotenNm = string.Empty;
                if (!string.IsNullOrWhiteSpace(joken.KyotenCd) && short.TryParse(joken.KyotenCd, out kyotenCd) && kyotenCd != -1 && kyotenCd != 99)
                    wheres.Add(string.Format("{0} = {1}", this.ColumnLocate("KYOTEN_CD", selects, setting), kyotenCd));

                // 日付(共通)
                var dateSpecify2 = UIConstants.DATE_SPECIFY_KBNS.FirstOrDefault(d => d.Item1 == this.joken.DateSpecify);
                if (!string.IsNullOrWhiteSpace(this.joken.DateFrom))
                    wheres.Add(string.Format("CONVERT(DATE, {0}) >= CONVERT(DATE, '{1}')", this.ColumnLocate(dateSpecify2.Item3, selects, setting), this.joken.DateFrom));
                if (!string.IsNullOrWhiteSpace(this.joken.DateFrom))
                    wheres.Add(string.Format("CONVERT(DATE, {0}) <= CONVERT(DATE, '{1}')", this.ColumnLocate(dateSpecify2.Item3, selects, setting), this.joken.DateTo));

                // 取引先(共通)
                if (!string.IsNullOrWhiteSpace(this.joken.TorihikisakiCdFrom))
                    wheres.Add(string.Format("{0} >= '{1}'", this.ColumnLocate("TORIHIKISAKI_CD", selects, setting), this.joken.TorihikisakiCdFrom));
                if (!string.IsNullOrWhiteSpace(this.joken.TorihikisakiCdTo))
                    wheres.Add(string.Format("{0} <= '{1}'", this.ColumnLocate("TORIHIKISAKI_CD", selects, setting), this.joken.TorihikisakiCdTo));

                switch (this.joken.HaniKbn)
                {
                    case 1: // 販売(受入・出荷・売上/支払・代納)
                        // 業者
                        if (!string.IsNullOrWhiteSpace(this.joken.GyoushaCdFrom))
                            wheres.Add(string.Format("{0} >= '{1}'", this.ColumnLocate("GYOUSHA_CD", selects, setting), this.joken.GyoushaCdFrom));
                        if (!string.IsNullOrWhiteSpace(this.joken.GyoushaCdTo))
                            wheres.Add(string.Format("{0} <= '{1}'", this.ColumnLocate("GYOUSHA_CD", selects, setting), this.joken.GyoushaCdTo));

                        // 現場
                        if (!string.IsNullOrWhiteSpace(this.joken.GenbaCdFrom))
                            wheres.Add(string.Format("{0} >= '{1}'", this.ColumnLocate("GENBA_CD", selects, setting), this.joken.GenbaCdFrom));
                        if (!string.IsNullOrWhiteSpace(this.joken.GenbaCdTo))
                            wheres.Add(string.Format("{0} <= '{1}'", this.ColumnLocate("GENBA_CD", selects, setting), this.joken.GenbaCdTo));
                        break;

                    case 2: // 入出金
                        // 入出金の場合、下記条件が入金のみ存在するので、入金のみに設定する
                        if (setting.DenshuKbn == DENSHU_KBN.NYUUKIN)
                        {
                            // 入金先
                            if (!string.IsNullOrWhiteSpace(this.joken.NyuukinsakiCdFrom))
                                wheres.Add(string.Format("{0} >= '{1}'", this.ColumnLocate("NYUUKINSAKI_CD", selects, setting), this.joken.NyuukinsakiCdFrom));
                            if (!string.IsNullOrWhiteSpace(this.joken.NyuukinsakiCdTo))
                                wheres.Add(string.Format("{0} <= '{1}'", this.ColumnLocate("NYUUKINSAKI_CD", selects, setting), this.joken.NyuukinsakiCdTo));

                            // 銀行
                            if (!string.IsNullOrWhiteSpace(this.joken.BankCdFrom))
                                wheres.Add(string.Format("{0} >= '{1}'", this.ColumnLocate("BANK_CD", selects, setting), this.joken.BankCdFrom));
                            if (!string.IsNullOrWhiteSpace(this.joken.BankCdTo))
                                wheres.Add(string.Format("{0} <= '{1}'", this.ColumnLocate("BANK_CD", selects, setting), this.joken.BankCdTo));

                            // 銀行支店
                            if (!string.IsNullOrWhiteSpace(this.joken.BankShitenCdFrom))
                                wheres.Add(string.Format("{0} >= '{1}'", this.ColumnLocate("BANK_SHITEN_CD", selects, setting), this.joken.BankShitenCdFrom));
                            if (!string.IsNullOrWhiteSpace(this.joken.BankShitenCdTo))
                                wheres.Add(string.Format("{0} <= '{1}'", this.ColumnLocate("BANK_SHITEN_CD", selects, setting), this.joken.BankShitenCdTo));
                        }
                        break;

                    default:
                        break;
                }

                #endregion

                #region JOIN句配列生成(パターンXMLより)

                // SELECT句とWHERE句が必要なJOINを追加
                foreach (var joinCondition in setting.JoinConditions)
                    if (tables.Contains(joinCondition.Table) ||
                        selects.Any(x => x.Contains(joinCondition.Table)) ||
                        wheres.Any(x => x.Contains(joinCondition.Table)))
                        joins.Add(joinCondition.Query);

                // JOIN済みのテーブルから繰り返す検索し、必要なJOINテーブルも追加
                while (true)
                {
                    var newjoin = false;
                    foreach (var joinCondition in setting.JoinConditions)
                    {
                        var refjoin = joins.FirstOrDefault(x => x.Contains(joinCondition.Table));
                        if (refjoin != null)
                        {
                            if (!joins.Contains(joinCondition.Query))
                            {
                                joins.Insert(joins.IndexOf(refjoin), joinCondition.Query);
                                newjoin = true;
                                // JOIN配列を変えられたので、再検索を行う
                                break;
                            }
                        }
                    }
                    // 全JOIN追加済み
                    if (!newjoin)
                        break;
                }

                #endregion

                // 配列からSELECT句を作成
                if (selects.Count > 0)
                    selectClause = string.Format(" SELECT DISTINCT {0} ", string.Join(", ", selects));

                // 配列からORDER BY句を生成
                if (orders.Count > 0)
                    orderClause = string.Format(" ORDER BY {0} ", string.Join(", ", orders));

                // XMLのFromConditionからFROM句を生成
                fromClause = string.Format(" FROM {0} {1} ",
                    // 直接テーブルの場合、Table名を使う
                    // 複合テーブルの場合、Query内容をテーブルとし、Table名を別名として使う
                    setting.FromCondition.Query, setting.FromCondition.Table);

                // 配列からJOIN句を生成
                if (joins.Count > 0)
                    joinClause = string.Join(" ", joins);

                // 配列からWHERE句を生成
                if (wheres.Count > 0)
                    whereClause = string.Format(" WHERE {0} ", string.Join(" AND ", wheres));

                var query = string.Concat(selectClause, fromClause, joinClause, whereClause, orderClause);
                queries.Add(query);
            }

            return queries;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="column"></param>
        /// <param name="selects"></param>
        /// <param name="setting"></param>
        /// <returns></returns>
        private string ColumnLocate(string column, Queue<string> selects, CsvPatternManager.CsvPatternSetting setting)
        {
            var scompare = string.Format(".{0} ", column);
            // 先ずはSelectClauseからカラム名を検索
            var select = selects.FirstOrDefault(x => x.Contains(scompare));
            if (select != null)
            {
                var seidx = select.IndexOf(scompare);
                var ssidx = select.LastIndexOf(" ", seidx) + 1;
                return select.Substring(ssidx, seidx - ssidx + column.Length + 1);
            }

            // OutputColumnsからカラム名を検索
            var outputColumn =
                setting.OutputGroups.SelectMany(
                x => x.OutputColumns
                ).FirstOrDefault(
                x => x.Name != "NULL" &&
                    // カラム名は問合せと等し、又は含むの場合
                    ((!string.IsNullOrWhiteSpace(x.TableName) && x.Name == column) || x.Name.Contains(scompare))
                    );
            if (outputColumn != null)
                if (outputColumn.Name == column)
                {
                    return string.Format("{0}.{1}", outputColumn.TableName, outputColumn.Name);
                }
                else
                {
                    var seidx = outputColumn.Name.IndexOf(scompare);
                    var ssidx = outputColumn.Name.LastIndexOf(" ", seidx) + 1;
                    return outputColumn.Name.Substring(ssidx, seidx - ssidx + column.Length + 1);
                }

            // 当たらない場合、FromTableを戻す
            return string.Format("{0}.{1}", setting.FromCondition.Table, column);
        }
    }
}