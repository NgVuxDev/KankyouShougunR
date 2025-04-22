// $Id: DenManiHaikiShuruiHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
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

namespace DenManiHaikiShuruiHoshu.Validator
{
    /// <summary>
    /// 廃棄物種類保守検証ロジック
    /// </summary>
    public class DenManiHaikiShuruiHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DenManiHaikiShuruiHoshuValidator()
        {
        }

        /// <summary>
        /// 廃棄物種類CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool HaikiShuruiCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            bool result = true;

            GcCustomNumericTextBox2Cell control = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD] as GcCustomNumericTextBox2Cell;
            if (control == null
                || control.Value == null
                || string.IsNullOrEmpty(control.Value.ToString()))
                return result;

            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の廃棄物種類CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD];
                        if (cell.Selected)
                        {
                            continue;
                        }

                        list.Add(cell);
                    }
                    cells.AddRange(list);

                    FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
                    string str = vali.DuplicationCheck();

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    if (!string.IsNullOrEmpty(str))
                    {
                        msgLogic.MessageBoxShow("E022", "入力された電子廃棄物種類CD");
                        result = false;
                        return result;
                    }
                }

                // 非表示分(検索条件から漏れたデータ)
                {
                    IEnumerable<DataRow> enumRow = dt.AsEnumerable();
                    IEnumerable<DataRow> enumRowAll = dtAll.AsEnumerable();
                    var rows = enumRowAll.Except(enumRow, new DenManiOnlyHaikiShuruiCompare());

                    cells = new List<Cell>();
                    var list1 = new List<Cell>();
                    foreach (DataRow row in rows)
                    {
                        string shainCd = row.Field<string>(Const.DenManiHaikiShuruiHoshuConstans.HAIKI_SHURUI_CD);

                        GcCustomNumericTextBox2Cell cell = new GcCustomNumericTextBox2Cell();
                        cell.Value = shainCd;
                        list1.Add(cell);
                    }
                    cells.AddRange(list1);

                    FWK.Validator vali1 = new FWK.Validator(control, cells.ToArray());
                    string str1 = vali1.DuplicationCheck();

                    MessageBoxShowLogic msgLogic1 = new MessageBoxShowLogic();
                    if (!string.IsNullOrEmpty(str1))
                    {
                        msgLogic1.MessageBoxShow("E022", "入力された電子廃棄物種類CD");
                        result = false;
                    }
                    return result;
                }
            }
        }
    }
}
