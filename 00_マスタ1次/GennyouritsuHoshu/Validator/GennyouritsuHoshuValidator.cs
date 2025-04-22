// $Id: GennyouritsuHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
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
using r_framework.Dao;

namespace GennyouritsuHoshu.Validator
{
    /// <summary>
    /// 減容率保守検証ロジック
    /// </summary>
    public class GennyouritsuHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GennyouritsuHoshuValidator()
        {
        }

        /// <summary>
        ///  廃棄物名称CD、処分方法CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool HaikiNameCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。
            bool result = true;

            GcCustomAlphaNumTextBoxCell controlHaiki = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.GennyouritsuHoshuConstans.HAIKI_NAME_CD] as GcCustomAlphaNumTextBoxCell;
            GcCustomAlphaNumTextBoxCell controlShobun = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.GennyouritsuHoshuConstans.SHOBUN_HOUHOU_CD] as GcCustomAlphaNumTextBoxCell;
            
            //廃棄物CDは""OKとするため条件からはずす
            /*if (controlHaiki == null || controlHaiki.Value == null || string.IsNullOrEmpty(controlHaiki.Value.ToString()))
            {
                return result;
            }*/

            if (controlShobun == null || controlShobun.Value == null || string.IsNullOrEmpty(controlShobun.Value.ToString()))
            {
                return result;
            }

            // 重複チェック
            {
                    // カレント行以外の廃棄物名称CDを保持するリスト
                    var listHaiki = new List<Cell>();
                    var listShobun = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cellHaiki = row.Cells[Const.GennyouritsuHoshuConstans.HAIKI_NAME_CD];
                        if (cellHaiki.Selected)
                        {
                            continue;
                        }

                        Cell cellShobun = row.Cells[Const.GennyouritsuHoshuConstans.SHOBUN_HOUHOU_CD];
                        if (cellShobun.Selected)
                        {
                            continue;
                        }

                        listHaiki.Add(cellHaiki);
                        listShobun.Add(cellShobun);
                    }
                }

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DataRowGennyouritsuCompare());

                    foreach (DataRow row in rows)
                    {
                        string haikiCd = row.Field<string>(Const.GennyouritsuHoshuConstans.HAIKI_NAME_CD);

                        GcCustomAlphaNumTextBoxCell cellhaiki = new GcCustomAlphaNumTextBoxCell();
                        cellhaiki.Value = haikiCd;
                        listHaiki.Add(cellhaiki);

                        string shobunCd = row.Field<string>(Const.GennyouritsuHoshuConstans.SHOBUN_HOUHOU_CD);
                        
                        GcCustomAlphaNumTextBoxCell cellshobun = new GcCustomAlphaNumTextBoxCell();
                        cellshobun.Value = shobunCd;
                        listShobun.Add(cellshobun);
                    }
                }

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                for (int i = 0; i < listHaiki.Count; i++)
                {
                    if (controlHaiki.Value.ToString() == listHaiki[i].Value.ToString())
                    {
                        if (controlShobun.Value.ToString() == listShobun[i].Value.ToString())
                        {
                            msgLogic.MessageBoxShow("E003", "処分方法CD", controlShobun.Value.ToString());
                            result = false;
                        }
                    }
                }
            }

            return result;
        }
    }
}
