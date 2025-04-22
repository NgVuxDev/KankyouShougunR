// $Id: KobetsuHinmeiTankaHoshuValidator.cs 31332 2014-10-01 07:56:54Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GrapeCity.Win.MultiRow;
using r_framework.Logic;
using r_framework.Utility;

namespace KobetsuHinmeiTankaHoshu.Validator
{
    /// <summary>
    /// 個別品名単価保守検証ロジック
    /// </summary>
    public class KobetsuHinmeiTankaHoshuValidator
    {
        /// <summary>
        /// 最大日付（適用終了日が未入力時に使用）
        /// </summary>
        private const string MAX_DATE = "9999/12/31";

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KobetsuHinmeiTankaHoshuValidator()
        {
        }

        /// <summary>
        /// 個別品名単価CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool KobetsuHinmeiTankaCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            // 重複チェック
            {
                // カレント行以外の廃棄物名称CDを保持するリスト
                var isChange = new List<bool>();
                var listHim = new List<object>();
                var listDenshu = new List<object>();
                var listUnit = new List<object>();
                var listUnpan = new List<object>();
                var listNGyousha = new List<object>();
                var listNGenba = new List<object>();
                var listBegin = new List<object>();
                var listEnd = new List<object>();

                // 比較用データテーブル
                var allDataTable = dt.Copy();

                // 表示分(検索条件による抽出分)
                {
                    foreach (DataRow row in ((DataTable)gcMultiRow.DataSource).Rows)
                    {
                        object denshukbncd = row[Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_CD];
                        object hinmeicd = row[Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_CD];
                        object unitcd = row[Const.KobetsuHinmeiTankaHoshuConstans.UNIT_CD];
                        object unpancd = row[Const.KobetsuHinmeiTankaHoshuConstans.UNPAN_GYOUSHA_CD];
                        object nioroshigyoucd = row[Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GYOUSHA_CD];
                        object nioroshigencd = row[Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GENBA_CD];
                        object tekiyoubegin = row[Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_BEGIN];
                        object tekiyouend = row[Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_END];

                        // 修正対象が本当に修正されているかチェックする
                        if (row[Const.KobetsuHinmeiTankaHoshuConstans.SYS_ID] != null && !row[Const.KobetsuHinmeiTankaHoshuConstans.SYS_ID].ToString().Equals(string.Empty))
                        {
                            DataRow[] dr = dtAll.Select(String.Format("SYS_ID = '{0}'", row[Const.KobetsuHinmeiTankaHoshuConstans.SYS_ID].ToString()));
                            if (dr.Length > 0
                                && ((bool)dr[0][Const.KobetsuHinmeiTankaHoshuConstans.DELETE_FLG]).Equals(((bool)row[Const.KobetsuHinmeiTankaHoshuConstans.DELETE_FLG]))
                                && dr[0][Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_CD].ToString().Equals(row[Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_CD].ToString())
                                && dr[0][Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_CD].ToString().Equals(row[Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_CD].ToString())
                                && dr[0][Const.KobetsuHinmeiTankaHoshuConstans.UNIT_CD].ToString().Equals(row[Const.KobetsuHinmeiTankaHoshuConstans.UNIT_CD].ToString())
                                && dr[0][Const.KobetsuHinmeiTankaHoshuConstans.UNPAN_GYOUSHA_CD].ToString().Equals(row[Const.KobetsuHinmeiTankaHoshuConstans.UNPAN_GYOUSHA_CD].ToString())
                                && dr[0][Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GYOUSHA_CD].ToString().Equals(row[Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GYOUSHA_CD].ToString())
                                && dr[0][Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GENBA_CD].ToString().Equals(row[Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GENBA_CD].ToString())
                                && dr[0][Const.KobetsuHinmeiTankaHoshuConstans.TANKA].ToString().Equals(row[Const.KobetsuHinmeiTankaHoshuConstans.TANKA].ToString())
                                && dr[0][Const.KobetsuHinmeiTankaHoshuConstans.BIKOU].ToString().Equals(row[Const.KobetsuHinmeiTankaHoshuConstans.BIKOU].ToString())
                                && (dr[0][Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_BEGIN] == null ? string.Empty : dr[0][Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_BEGIN].ToString()) == (row[Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_BEGIN] == null ? string.Empty : row[Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_BEGIN].ToString())
                                && (dr[0][Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_END] == null ? string.Empty : dr[0][Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_END].ToString()) == (row[Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_END] == null ? string.Empty : row[Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_END].ToString()))
                            {
                                row.RejectChanges();
                            }
                        }

                        isChange.Add(row.RowState != DataRowState.Unchanged);
                        listDenshu.Add(denshukbncd);
                        listHim.Add(hinmeicd);
                        listUnit.Add(unitcd);
                        listUnpan.Add(unpancd);
                        listNGyousha.Add(nioroshigyoucd);
                        listNGenba.Add(nioroshigencd);
                        listBegin.Add(tekiyoubegin);
                        listEnd.Add(tekiyouend);
                    }
                }
                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DataRowKobetsuHinmeiTankaCompare());
                    foreach (DataRow row in rows)
                    {
                        isChange.Add(false);

                        //品名CD
                        listHim.Add(row.Field<string>(Const.KobetsuHinmeiTankaHoshuConstans.HINMEI_CD));

                        //伝種区分CD
                        if (row.Field<Object>(Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_CD) == null)
                        {
                            listDenshu.Add(null);
                        }
                        else
                        {
                            listDenshu.Add(row.Field<Int16>(Const.KobetsuHinmeiTankaHoshuConstans.DENSHU_KBN_CD));
                        }

                        //単位CD
                        if (row.Field<Object>(Const.KobetsuHinmeiTankaHoshuConstans.UNIT_CD) == null)
                        {
                            listUnit.Add(null);
                        }
                        else
                        {
                            listUnit.Add(row.Field<Int16>(Const.KobetsuHinmeiTankaHoshuConstans.UNIT_CD));
                        }

                        //運搬業者CD
                        listUnpan.Add(row.Field<string>(Const.KobetsuHinmeiTankaHoshuConstans.UNPAN_GYOUSHA_CD));

                        //荷降先業者CD
                        listNGyousha.Add(row.Field<string>(Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GYOUSHA_CD));

                        //荷降先現場CD
                        listNGenba.Add(row.Field<string>(Const.KobetsuHinmeiTankaHoshuConstans.NIOROSHI_GENBA_CD));

                        //適用開始日
                        if (row.Field<Object>(Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_BEGIN) == null)
                        {
                            listBegin.Add(null);
                        }
                        else
                        {
                            listBegin.Add(row.Field<DateTime>(Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_BEGIN));
                        }

                        //適用終了日
                        if (row.Field<Object>(Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_END) == null)
                        {
                            listEnd.Add(null);
                        }
                        else
                        {
                            listEnd.Add(row.Field<DateTime>(Const.KobetsuHinmeiTankaHoshuConstans.TEKIYOU_END));
                        }

                        // 比較用データテーブルにも追加
                        DataRow dr_copy = allDataTable.NewRow();
                        dr_copy.ItemArray = row.ItemArray;
                        allDataTable.Rows.Add(dr_copy);
                    }
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                for (int i = 0; i < listHim.Count; i++)
                {
                    if (!isChange[i]) continue;

                    for (int j = 0; j < listHim.Count; j++)
                    {
                        if (i == j) continue;

                        //キーが同一の数
                        int cellCount = 0;

                        // 削除チェックがついている かつ 未登録行の場合
                        if (allDataTable.Rows[i][Const.KobetsuHinmeiTankaHoshuConstans.DELETE_FLG].Equals(true)
                            && allDataTable.Rows[i][Const.KobetsuHinmeiTankaHoshuConstans.SYS_ID].ToString().Equals("")
                            || (allDataTable.Rows[j][Const.KobetsuHinmeiTankaHoshuConstans.DELETE_FLG].Equals(true)
                            && allDataTable.Rows[j][Const.KobetsuHinmeiTankaHoshuConstans.SYS_ID].ToString().Equals("")))
                        {
                            continue;
                        }

                        //品名CDが同一
                        if ((listHim[i] == null || listHim[i].ToString() == "") &&
                            (listHim[j] == null || listHim[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listHim[i].ToString() != "" && listHim[j].ToString() != "")
                        {
                            if (listHim[i].ToString() == listHim[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //伝種区分CDが同一
                        if ((listDenshu[i] == null || listDenshu[i].ToString() == "") &&
                            (listDenshu[j] == null || listDenshu[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listDenshu[i].ToString() != "" && listDenshu[j].ToString() != "")
                        {
                            if (listDenshu[i].ToString() == listDenshu[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //単位CDが同一
                        if ((listUnit[i] == null || listUnit[i].ToString() == "") &&
                            (listUnit[j] == null || listUnit[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listUnit[i].ToString() != "" && listUnit[j].ToString() != "")
                        {
                            if (listUnit[i].ToString() == listUnit[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //運搬業者CDが同一
                        if ((listUnpan[i] == null || listUnpan[i].ToString() == "") &&
                            (listUnpan[j] == null || listUnpan[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listUnpan[i].ToString() != "" && listUnpan[j].ToString() != "")
                        {
                            if (listUnpan[i].ToString() == listUnpan[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //荷降先業者CDが同一
                        if ((listNGyousha[i] == null || listNGyousha[i].ToString() == "") &&
                            (listNGyousha[j] == null || listNGyousha[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listNGyousha[i].ToString() != "" && listNGyousha[j].ToString() != "")
                        {
                            if (listNGyousha[i].ToString() == listNGyousha[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //荷降先現場CDが同一
                        if ((listNGenba[i] == null || listNGenba[i].ToString() == "") &&
                            (listNGenba[j] == null || listNGenba[j].ToString() == ""))
                        {
                            cellCount += 1;
                        }
                        else if (listNGenba[i].ToString() != "" && listNGenba[j].ToString() != "")
                        {
                            if (listNGenba[i].ToString() == listNGenba[j].ToString())
                            {
                                cellCount += 1;
                            }
                        }
                        else
                        {
                            continue;
                        }

                        //品名CD、伝種区分CD、単位、運搬業者CD、荷降先業者CD、荷降先CDが同一の場合
                        if (cellCount == 6)
                        {
                            bool checkDate = true;

                            //検証データを変数に格納しておく
                            //※置換データが実データに反映されないように
                            DateTime srcBegin = (DateTime)listBegin[i];
                            DateTime srcEnd = DateTime.Parse(MAX_DATE);
                            if (listEnd[i] != null && !string.IsNullOrWhiteSpace(listEnd[i].ToString()))
                            {
                                srcEnd = (DateTime)listEnd[i];
                            }
                            DateTime targetBegin = (DateTime)listBegin[j];
                            DateTime targetEnd = DateTime.Parse(MAX_DATE);
                            if (listEnd[j] != null && !string.IsNullOrWhiteSpace(listEnd[j].ToString()))
                            {
                                targetEnd = (DateTime)listEnd[j];
                            }

                            //チェックパターン①　開始日存在
                            if (srcBegin <= targetBegin && targetBegin <= srcEnd)
                            {
                                checkDate = false;
                            }

                            //チェックパターン②　終了日存在
                            if (srcBegin <= targetEnd && targetEnd <= srcEnd)
                            {
                                checkDate = false;
                            }

                            //チェックパターン③　開始終了内包
                            if (targetBegin <= srcBegin && srcEnd <= targetEnd)
                            {
                                checkDate = false;
                            }

                            //適用期間範囲のチェック結果、NGであれば重複エラーとする
                            if (!checkDate)
                            {
                                msgLogic.MessageBoxShow("E013");
                                return false;
                            }
                        }
                    }

                    // 登録修正の行チェックに問題がない場合、関連性チェックを行う。
                    if (!this.RelationalDataEntryCheck(i, listHim, listDenshu, listUnit, listUnpan, listNGyousha, listNGenba, listBegin, listEnd))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// 登録/修正情報の関連性チェックを行う
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="listHim"></param>
        /// <param name="listDenshu"></param>
        /// <param name="listUnit"></param>
        /// <param name="listUnpan"></param>
        /// <param name="listNGyousha"></param>
        /// <param name="listNGenba"></param>
        /// <param name="listBegin"></param>
        /// <param name="listEnd"></param>
        /// <returns></returns>
        public bool RelationalDataEntryCheck(int rowIndex, List<object> listHim, List<object> listDenshu, List<object> listUnit, List<object> listUnpan, List<object> listNGyousha, List<object> listNGenba, List<object> listBegin, List<object> listEnd)
        {
            try
            {
                LogUtility.DebugMethodStart(rowIndex, listHim, listDenshu, listUnit, listUnpan, listNGyousha, listNGenba, listBegin, listEnd);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                int checkFlg = 0;

                // 同一品名、伝種区分、単位を持つ行のテーブルを作成する
                DataTable dtTarget = new DataTable();
                dtTarget.Columns.Add("HINMEI_CD", typeof(string));
                dtTarget.Columns.Add("DENSHU_KBN", typeof(Int16));
                dtTarget.Columns.Add("UNIT_CD", typeof(Int16));
                dtTarget.Columns.Add("UNPAN_GYOUSHA_CD", typeof(string));
                dtTarget.Columns.Add("NIOROSHI_GYOUSHA_CD", typeof(string));
                dtTarget.Columns.Add("NIOROSHI_GENBA_CD", typeof(string));
                dtTarget.Columns.Add("TEKIYOU_BEGIN", typeof(DateTime));
                dtTarget.Columns.Add("TEKIYOU_END", typeof(DateTime));
                for (int i = 0; i < listHim.Count; i++)
                {
                    if (i == rowIndex) continue;

                    if ((listHim[rowIndex] != null && !string.IsNullOrWhiteSpace(listHim[rowIndex].ToString()))
                        && (listDenshu[rowIndex] != null && !string.IsNullOrWhiteSpace(listDenshu[rowIndex].ToString()))
                        && (listUnit[rowIndex] != null && !string.IsNullOrWhiteSpace(listUnit[rowIndex].ToString()))
                        && (listHim[i] != null && !string.IsNullOrWhiteSpace(listHim[i].ToString()))
                        && (listDenshu[i] != null && !string.IsNullOrWhiteSpace(listDenshu[i].ToString()))
                        && (listUnit[i] != null && !string.IsNullOrWhiteSpace(listUnit[i].ToString()))
                        && listHim[rowIndex].ToString().Equals(listHim[i].ToString())
                        && listDenshu[rowIndex].ToString().Equals(listDenshu[i].ToString())
                        && listUnit[rowIndex].ToString().Equals(listUnit[i].ToString()))
                    {
                        DataRow row = dtTarget.NewRow();
                        row["HINMEI_CD"] = listHim[i];
                        row["HINMEI_CD"] = listHim[i];
                        row["DENSHU_KBN"] = listDenshu[i];
                        row["UNIT_CD"] = listUnit[i];
                        if (listUnpan[i] == null || string.IsNullOrWhiteSpace(listUnpan[i].ToString()))
                        {
                            row["UNPAN_GYOUSHA_CD"] = DBNull.Value;
                        }
                        else
                        {
                            row["UNPAN_GYOUSHA_CD"] = listUnpan[i];
                        }
                        if (listNGyousha[i] == null || string.IsNullOrWhiteSpace(listNGyousha[i].ToString()))
                        {
                            row["NIOROSHI_GYOUSHA_CD"] = DBNull.Value;
                        }
                        else
                        {
                            row["NIOROSHI_GYOUSHA_CD"] = listNGyousha[i];
                        }
                        if (listNGenba[i] == null || string.IsNullOrWhiteSpace(listNGenba[i].ToString()))
                        {
                            row["NIOROSHI_GENBA_CD"] = DBNull.Value;
                        }
                        else
                        {
                            row["NIOROSHI_GENBA_CD"] = listNGenba[i];
                        }
                        if (listBegin[i] == null || string.IsNullOrWhiteSpace(listBegin[i].ToString()))
                        {
                            row["TEKIYOU_BEGIN"] = DBNull.Value;
                        }
                        else
                        {
                            row["TEKIYOU_BEGIN"] = listBegin[i];
                        }
                        if (listEnd[i] == null || string.IsNullOrWhiteSpace(listEnd[i].ToString()))
                        {
                            row["TEKIYOU_END"] = DateTime.Parse(MAX_DATE);
                        }
                        else
                        {
                            row["TEKIYOU_END"] = listEnd[i];
                        }
                        dtTarget.Rows.Add(row);
                    }
                }

                // 比較元の日付をフォーマットする
                DateTime srcBegin = (DateTime)listBegin[rowIndex];
                DateTime srcEnd = DateTime.Parse(MAX_DATE);
                if (listEnd[rowIndex] != null && !string.IsNullOrWhiteSpace(listEnd[rowIndex].ToString()))
                {
                    srcEnd = (DateTime)listEnd[rowIndex];
                }
                string tekiyouStr = string.Format("((TEKIYOU_BEGIN >= #{0}# AND TEKIYOU_BEGIN <= #{1}#) OR (TEKIYOU_END >= #{0}# AND TEKIYOU_END <= #{1}#) OR (TEKIYOU_BEGIN <= #{0}# AND TEKIYOU_END >= #{1}#))", srcBegin, srcEnd);

                // 運搬業者の入力があり、荷降先業者・荷降先現場の入力がない場合
                if ((listUnpan[rowIndex] != null && !string.IsNullOrWhiteSpace(listUnpan[rowIndex].ToString()))
                    && (listNGyousha[rowIndex] == null || string.IsNullOrWhiteSpace(listNGyousha[rowIndex].ToString()))
                    && (listNGenba[rowIndex] == null || string.IsNullOrWhiteSpace(listNGenba[rowIndex].ToString())))
                {
                    DataRow[] rows;

                    // 運搬業者・荷降先現場の入力がなく、荷降先業者の入力行が存在するかチェック
                    rows = dtTarget.Select("UNPAN_GYOUSHA_CD IS NULL AND NIOROSHI_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GENBA_CD IS NULL AND " + tekiyouStr);
                    if (rows.Length > 0)
                    {
                        // 運搬業者・荷降先業者が入力され、荷降先現場のない行が存在するかチェック
                        rows = dtTarget.Select("UNPAN_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GENBA_CD IS NULL AND " + tekiyouStr);
                        if (rows.Length <= 0)
                        {
                            checkFlg = 1;
                        }
                    }

                    // 運搬業者入力がなく、荷降先業者・荷降先現場の入力行が存在するかチェック
                    rows = dtTarget.Select("UNPAN_GYOUSHA_CD IS NULL AND NIOROSHI_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GENBA_CD IS NOT NULL AND " + tekiyouStr);
                    if (rows.Length > 0)
                    {
                        // 運搬業者・荷降先業者・荷降先現場が入力された行が存在するかチェック
                        rows = dtTarget.Select("UNPAN_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GENBA_CD IS NOT NULL AND " + tekiyouStr);
                        if (rows.Length <= 0)
                        {
                            checkFlg = 2;
                        }
                    }
                }

                // 荷降先業者の入力があり、運搬業者・荷降先現場の入力がない場合
                if ((listUnpan[rowIndex] == null || string.IsNullOrWhiteSpace(listUnpan[rowIndex].ToString()))
                    && (listNGyousha[rowIndex] != null && !string.IsNullOrWhiteSpace(listNGyousha[rowIndex].ToString()))
                    && (listNGenba[rowIndex] == null || string.IsNullOrWhiteSpace(listNGenba[rowIndex].ToString())))
                {
                    DataRow[] rows;

                    // 荷降先業者・荷降先現場の入力がなく、運搬業者の入力行が存在するかチェック
                    rows = dtTarget.Select("UNPAN_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GYOUSHA_CD IS NULL AND NIOROSHI_GENBA_CD IS NULL AND " + tekiyouStr);
                    if (rows.Length > 0)
                    {
                        // 運搬業者・荷降先業者が入力され、荷降先現場のない行が存在するかチェック
                        rows = dtTarget.Select("UNPAN_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GENBA_CD IS NULL AND " + tekiyouStr);
                        if (rows.Length <= 0)
                        {
                            checkFlg = 1;
                        }
                    }
                }

                // 荷降先業者・荷降先現場の入力があり、運搬業者の入力がない場合
                if ((listUnpan[rowIndex] == null || string.IsNullOrWhiteSpace(listUnpan[rowIndex].ToString()))
                    && (listNGyousha[rowIndex] != null && !string.IsNullOrWhiteSpace(listNGyousha[rowIndex].ToString()))
                    && (listNGenba[rowIndex] != null && !string.IsNullOrWhiteSpace(listNGenba[rowIndex].ToString())))
                {
                    DataRow[] rows;

                    // 荷降先業者・荷降先現場の入力がなく、運搬業者の入力行が存在するかチェック
                    rows = dtTarget.Select("UNPAN_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GYOUSHA_CD IS NULL AND NIOROSHI_GENBA_CD IS NULL AND " + tekiyouStr);
                    if (rows.Length > 0)
                    {
                        // 運搬業者・荷降先業者・荷降先現場の入力が入力された行が存在するかチェック
                        rows = dtTarget.Select("UNPAN_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GENBA_CD IS NOT NULL AND " + tekiyouStr);
                        if (rows.Length <= 0)
                        {
                            checkFlg = 2;
                        }
                    }
                }

                // 運搬業者・荷降先業者の入力があり、荷降先現場の入力がない場合
                if ((listUnpan[rowIndex] != null && !string.IsNullOrWhiteSpace(listUnpan[rowIndex].ToString()))
                    && (listNGyousha[rowIndex] != null && !string.IsNullOrWhiteSpace(listNGyousha[rowIndex].ToString()))
                    && (listNGenba[rowIndex] == null || string.IsNullOrWhiteSpace(listNGenba[rowIndex].ToString())))
                {
                    // 荷降先業者・荷降先現場の入力がなく、運搬業者の入力行が存在するかチェック
                    DataRow[] rows = dtTarget.Select("UNPAN_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GYOUSHA_CD IS NULL AND NIOROSHI_GENBA_CD IS NULL");
                    // 運搬業者・荷降先現場の入力がなく、荷降先業者の入力行が存在するかチェック
                    DataRow[] rows2 = dtTarget.Select("UNPAN_GYOUSHA_CD IS NULL AND NIOROSHI_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GENBA_CD IS NULL");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DateTime targetBegin1 = (DateTime)rows[i]["TEKIYOU_BEGIN"];
                        DateTime targetEnd1 = (DateTime)rows[i]["TEKIYOU_END"];

                        for (int j = 0; j < rows2.Length; j++)
                        {
                            DateTime targetBegin2 = (DateTime)rows2[j]["TEKIYOU_BEGIN"];
                            DateTime targetEnd2 = (DateTime)rows2[j]["TEKIYOU_END"];

                            // 有効期間が範囲内かチェック
                            if ((targetBegin1 <= targetBegin2 && targetBegin2 <= targetEnd1) || (targetBegin1 <= targetEnd2 && targetEnd2 <= targetEnd1) || (targetBegin2 <= targetBegin1 && targetEnd1 <= targetEnd2))
                            {
                                if (!(((targetBegin1 <= srcBegin && srcBegin <= targetEnd1) || (targetBegin1 <= srcEnd && srcEnd <= targetEnd1) || (srcBegin <= targetBegin1 && targetEnd1 <= srcEnd))
                                    && ((targetBegin2 <= srcBegin && srcBegin <= targetEnd2) || (targetBegin2 <= srcEnd && srcEnd <= targetEnd2) || (srcBegin <= targetBegin2 && targetEnd2 <= srcEnd))))
                                {
                                    checkFlg = 1;
                                }
                            }
                        }
                    }
                }

                // 運搬業者・荷降先業者・荷降先現場の入力がされている場合
                if ((listUnpan[rowIndex] != null && !string.IsNullOrWhiteSpace(listUnpan[rowIndex].ToString()))
                    && (listNGyousha[rowIndex] != null && !string.IsNullOrWhiteSpace(listNGyousha[rowIndex].ToString()))
                    && (listNGenba[rowIndex] != null && !string.IsNullOrWhiteSpace(listNGenba[rowIndex].ToString())))
                {
                    // 荷降先業者・荷降先現場の入力がなく、運搬業者の入力行が存在するかチェック
                    DataRow[] rows = dtTarget.Select("UNPAN_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GYOUSHA_CD IS NULL AND NIOROSHI_GENBA_CD IS NULL");
                    // 運搬業者の入力がなく、荷降先業者・荷降先現場の入力行が存在するかチェック
                    DataRow[] rows2 = dtTarget.Select("UNPAN_GYOUSHA_CD IS NULL AND NIOROSHI_GYOUSHA_CD IS NOT NULL AND NIOROSHI_GENBA_CD IS NOT NULL");
                    for (int i = 0; i < rows.Length; i++)
                    {
                        DateTime targetBegin1 = (DateTime)rows[i]["TEKIYOU_BEGIN"];
                        DateTime targetEnd1 = (DateTime)rows[i]["TEKIYOU_END"];

                        for (int j = 0; j < rows2.Length; j++)
                        {
                            DateTime targetBegin2 = (DateTime)rows2[j]["TEKIYOU_BEGIN"];
                            DateTime targetEnd2 = (DateTime)rows2[j]["TEKIYOU_END"];

                            // 有効期間が範囲内かチェック
                            if ((targetBegin1 <= targetBegin2 && targetBegin2 <= targetEnd1) || (targetBegin1 <= targetEnd2 && targetEnd2 <= targetEnd1) || (targetBegin2 <= targetBegin1 && targetEnd1 <= targetEnd2))
                            {
                                if (!(((targetBegin1 <= srcBegin && srcBegin <= targetEnd1) || (targetBegin1 <= srcEnd && srcEnd <= targetEnd1) || (srcBegin <= targetBegin1 && targetEnd1 <= srcEnd))
                                    && ((targetBegin2 <= srcBegin && srcBegin <= targetEnd2) || (targetBegin2 <= srcEnd && srcEnd <= targetEnd2) || (srcBegin <= targetBegin2 && targetEnd2 <= srcEnd))))
                                {
                                    checkFlg = 2;
                                }
                            }
                        }
                    }
                }

                // チェックフラグ>0の場合、エラー処理を行う
                if (checkFlg > 0)
                {
                    string msgId = string.Empty;
                    switch (checkFlg)
                    {
                        case 1:
                            msgId = "E165";
                            break;
                        case 2:
                            msgId = "E166";
                            break;
                    }
                    msgLogic.MessageBoxShow(msgId);
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex.Message);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return true;
        }
    }
}