// $Id: UnchinTankaHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using FWK = r_framework.Logic;
using System.Data;
using System;
using r_framework.Utility;


namespace Shougun.Core.Master.UnchinTankaHoshu.Validator
{
    /// <summary>
    /// 運賃単価入力検証ロジック
    /// </summary>
    public class UnchinTankaHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnchinTankaHoshuValidator()
        {
        }

        /// <summary>
        /// 重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool UnchinTankaValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            // 重複チェック
            // カレント行以外の廃棄物名称CDを保持するリスト
            var isChange = new List<bool>();
            var listHim = new List<string>();
            var listUnit = new List<string>();
            var listShashu = new List<string>();

            // 比較用データテーブル
            var allDataTable = dt.Copy();

            // 表示分(検索条件による抽出分)
            {

                string hinmeicd = "";
                string unitcd = "";
                string shashu = "";

                foreach (DataRow row in ((DataTable)gcMultiRow.DataSource).Rows)
                {
                    if (row[Const.UnchinTankaHoshuConstans.DELETE_FLG] != null && row[Const.UnchinTankaHoshuConstans.DELETE_FLG].ToString() == "True"
                        && (row["CREATE_USER"] == null || string.IsNullOrEmpty(row["CREATE_USER"].ToString())))
                    {
                        continue;
                    }
                    hinmeicd = Convert.ToString(row[Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD]);
                    unitcd = Convert.ToString(row[Const.UnchinTankaHoshuConstans.UNIT_CD]);
                    shashu = Convert.ToString(row[Const.UnchinTankaHoshuConstans.SHASHU_CD]);

                    // 修正対象が本当に修正されているかチェックする
                    if (!string.IsNullOrEmpty(hinmeicd) && string.IsNullOrEmpty(unitcd))
                    {
                        DataRow[] dr = dtAll.Select(String.Format("UNCHIN_HINMEI_CD = '{0}' AND UNIT_CD = '{1}' AND TANKA = '{2}'", hinmeicd, unitcd, shashu));
                        if (dr.Length > 0
                            && ((bool)dr[0][Const.UnchinTankaHoshuConstans.DELETE_FLG]).Equals(((bool)row[Const.UnchinTankaHoshuConstans.DELETE_FLG]))
                            && dr[0][Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD].ToString().Equals(row[Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD].ToString())
                            && dr[0][Const.UnchinTankaHoshuConstans.UNIT_CD].ToString().Equals(row[Const.UnchinTankaHoshuConstans.UNIT_CD].ToString())
                            && dr[0][Const.UnchinTankaHoshuConstans.TANKA].ToString().Equals(row[Const.UnchinTankaHoshuConstans.TANKA].ToString())
                            && dr[0][Const.UnchinTankaHoshuConstans.SHASHU_CD].ToString().Equals(row[Const.UnchinTankaHoshuConstans.SHASHU_CD].ToString())
                            && dr[0][Const.UnchinTankaHoshuConstans.BIKOU].ToString().Equals(row[Const.UnchinTankaHoshuConstans.BIKOU].ToString()))
                        {
                            row.RejectChanges();
                        }
                    }

                    isChange.Add(row.RowState != DataRowState.Unchanged);
                    listHim.Add(hinmeicd);
                    listUnit.Add(unitcd);
                    listShashu.Add(shashu);
                }
            }
            // 非表示分(検索条件から漏れたデータ)
            {
                IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                var rows = enumRowAll.Except(enumRow, new DataRowUnchinTankaHoshuCompare());
                foreach (DataRow row in rows)
                {
                    isChange.Add(false);

                    //品名CD
                    listHim.Add(row.Field<string>(Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD));

                    //単位CD
                    if (row.Field<Object>(Const.UnchinTankaHoshuConstans.UNIT_CD) == null)
                    {
                        listUnit.Add("");
                    }
                    else
                    {
                        listUnit.Add(Convert.ToString(row.Field<Int16>(Const.UnchinTankaHoshuConstans.UNIT_CD)));
                    }

                    //車種CD
                    listShashu.Add(row.Field<string>(Const.UnchinTankaHoshuConstans.SHASHU_CD));

                    // 比較用データテーブルにも追加
                    DataRow dr_copy = allDataTable.NewRow();
                    dr_copy.ItemArray = row.ItemArray;
                    allDataTable.Rows.Add(dr_copy);
                }
            }

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            string hinmeicd_i = "";
            string unitcd_i = "";
            string hinmeicd_j = "";
            string unitcd_j = "";

            for (int i = 0; i < listHim.Count; i++)
            {
                if (!isChange[i]) continue;
                hinmeicd_i = Convert.ToString(allDataTable.Rows[i][Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD]);
                unitcd_i = Convert.ToString(allDataTable.Rows[i][Const.UnchinTankaHoshuConstans.UNIT_CD]);

                for (int j = 0; j < listHim.Count; j++)
                {
                    if (i == j) continue;

                    //キーが同一の数
                    int cellCount = 0;

                    hinmeicd_j = Convert.ToString(allDataTable.Rows[j][Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD]);
                    unitcd_j = Convert.ToString(allDataTable.Rows[j][Const.UnchinTankaHoshuConstans.UNIT_CD]);
                    // 削除チェックがついている かつ 未登録行の場合
                    if ((allDataTable.Rows[i][Const.UnchinTankaHoshuConstans.DELETE_FLG].Equals(true)
                        && string.IsNullOrEmpty(hinmeicd_i)
                        && string.IsNullOrEmpty(unitcd_i))
                        || (allDataTable.Rows[j][Const.UnchinTankaHoshuConstans.DELETE_FLG].Equals(true)
                        && string.IsNullOrEmpty(hinmeicd_j)
                        && string.IsNullOrEmpty(unitcd_j)))
                    {
                        continue;
                    }

                    //品名CDが同一
                    if (string.IsNullOrEmpty(listHim[i]) && string.IsNullOrEmpty(listHim[j]))
                    {
                        cellCount += 1;
                    }
                    else if (!string.IsNullOrEmpty(listHim[i]) && !string.IsNullOrEmpty(listHim[j]))
                    {
                        if (listHim[i] == listHim[j])
                        {
                            cellCount += 1;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    //単位CDが同一
                    if (string.IsNullOrEmpty(listUnit[i]) && string.IsNullOrEmpty(listUnit[j]))
                    {
                        cellCount += 1;
                    }
                    else if (!string.IsNullOrEmpty(listUnit[i]) && !string.IsNullOrEmpty(listUnit[j]))
                    {
                        if (listUnit[i] == listUnit[j])
                        {
                            cellCount += 1;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    //車種CDが同一
                    if (string.IsNullOrEmpty(listShashu[i]) && string.IsNullOrEmpty(listShashu[j]))
                    {
                        cellCount += 1;
                    }
                    else if (!string.IsNullOrEmpty(listShashu[i]) && !string.IsNullOrEmpty(listShashu[j]))
                    {
                        if (listShashu[i] == listShashu[j])
                        {
                            cellCount += 1;
                        }
                    }
                    else
                    {
                        continue;
                    }

                    //品名CD、単位、車種が同一の場合
                    if (cellCount == 3)
                    {
                        msgLogic.MessageBoxShow("E013");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
