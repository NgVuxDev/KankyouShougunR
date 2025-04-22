// $Id: UnitHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Logic;

using FWK = r_framework.Logic;

namespace UnitHoshu.Validator
{
    /// <summary>
    /// 単位保守検証ロジック
    /// </summary>
    public class UnitHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnitHoshuValidator()
        {
        }

        /// <summary>
        /// 単位CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool UnitCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            bool result = true;

            GcCustomNumericTextBox2Cell control = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.UnitHoshuConstans.UNIT_CD] as GcCustomNumericTextBox2Cell;
            if (control == null
                || control.Value == null
                || string.IsNullOrEmpty(control.Value.ToString()))
                return result;

            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の単位CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.UnitHoshuConstans.UNIT_CD];
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

                    var rows = enumRowAll.Except(enumRow, new DataRowUnitCompare());

                    var list = new List<GcCustomNumericTextBox2Cell>();
                    foreach (DataRow row in rows)
                    {
                        Int16 unitCd = row.Field<Int16>(Const.UnitHoshuConstans.UNIT_CD);

                        GcCustomNumericTextBox2Cell cell = new GcCustomNumericTextBox2Cell();
                        cell.Value = unitCd;
                        list.Add(cell);
                    }
                    cells.AddRange(list);
                }

                FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
                string str = vali.DuplicationCheck();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(str))
                {
                    msgLogic.MessageBoxShow("E022", "入力された単位CD");
                    result = false;
                }
            }

            return result;
        }
    }
}