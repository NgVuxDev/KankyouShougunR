// $Id: TorihikikubunHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
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

namespace TorihikikubunHoshu.Validator
{
    /// <summary>
    /// 取引区分保守検証ロジック
    /// </summary>
    public class TorihikikubunHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TorihikikubunHoshuValidator()
        {
        }

        /// <summary>
        /// 取引区分CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool TorihikikubunCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            bool result = true;

            GcCustomNumericTextBox2Cell control = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.TorihikikubunHoshuConstans.TORIHIKI_KBN_CD] as GcCustomNumericTextBox2Cell;
            if (control == null
                || control.Value == null
                || string.IsNullOrEmpty(control.Value.ToString()))
                return result;

            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の取引区分CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.TorihikikubunHoshuConstans.TORIHIKI_KBN_CD];
                        if (cell.Selected)
                        {
                            continue;
                        }

                        list.Add(cell);
                    }
                    cells.AddRange(list);
                }

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();

                    var rows = enumRowAll.Except(enumRow, new DataRowTorihikikubunCompare());

                    var list = new List<GcCustomTextBoxCell>();
                    //var list = new List<GcCustomAlphaNumTextBoxCell>();
                    foreach (DataRow row in rows)
                    {
                        string TorihikiKbnCd = Const.TorihikikubunHoshuConstans.TORIHIKI_KBN_CD.ToString();
                        //string TorihikiKbnCd = eRow.Field<string>(Const.TorihikikubunHoshuConstans.TORIHIKI_KBN_CD);

                        GcCustomTextBoxCell cell = new GcCustomTextBoxCell();
                        //GcCustomAlphaNumTextBoxCell eCell = new GcCustomAlphaNumTextBoxCell();
                        cell.Value = TorihikiKbnCd;
                        list.Add(cell);
                    }
                    cells.AddRange(list);
                }

                FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
                string str = vali.DuplicationCheck();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(str))
                {
                    msgLogic.MessageBoxShow("E003", "取引区分CD", control.Value.ToString());
                    result = false;
                }
            }

            return result;
        }
    }
}
