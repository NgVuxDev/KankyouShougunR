// $Id: NyuushukkinKbnHoshuValidator.cs 18150 2014-03-27 09:30:30Z sc.h.hashimoto $
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

namespace NyuushukkinKbnHoshu.Validator
{
    /// <summary>
    /// 入出金区分保守検証ロジック
    /// </summary>
    public class NyuushukkinKbnHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NyuushukkinKbnHoshuValidator()
        {
        }

        /// <summary>
        /// 入出金区分CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool NyuushukkinKbnCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            bool result = true;

            GcCustomNumericTextBox2Cell control = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.NyuushukkinKbnHoshuConstans.NYUUSHUKKIN_KBN_CD] as GcCustomNumericTextBox2Cell;

            if (control == null ||
                control.Value == null ||
                string.IsNullOrEmpty(control.Value.ToString()))
            {
                return result;
            }

            // No3750-->
            // 入出金入力区分CDに0を入力させない
            // 52-99はRangeSettingで抑制
            if (int.Parse(gcMultiRow.CurrentCell.Value.ToString()) == 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E002", "入出金区分CD", "1～51");
                return false;
            }
            // No3750<--

            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の入出金区分CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.NyuushukkinKbnHoshuConstans.NYUUSHUKKIN_KBN_CD];
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

                    var rows = enumRowAll.Except(enumRow, new DataRowNyuushukkinKbnCompare());

                    var list = new List<GcCustomNumericTextBox2Cell>();
                    foreach (DataRow row in rows)
                    {
                        Int16 nyuudhukkinkbnCd = row.Field<Int16>(Const.NyuushukkinKbnHoshuConstans.NYUUSHUKKIN_KBN_CD);

                        GcCustomNumericTextBox2Cell cell = new GcCustomNumericTextBox2Cell();
                        cell.Value = nyuudhukkinkbnCd;
                        list.Add(cell);
                    }
                    cells.AddRange(list);
                }

                FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
                string str = vali.DuplicationCheck();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(str))
                {
                    msgLogic.MessageBoxShow("E022", "入力された入出金区分CD");
                    result = false;
                }
            }

            return result;
        }
    }
}
