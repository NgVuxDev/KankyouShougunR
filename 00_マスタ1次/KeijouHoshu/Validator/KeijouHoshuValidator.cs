// $Id: KeijouHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
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

namespace KeijouHoshu.Validator
{
    /// <summary>
    /// 形状保守検証ロジック
    /// </summary>
    public class KeijouHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KeijouHoshuValidator()
        {
        }
        /// <summary>
        /// 形状CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool KeijouCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            bool result = true;
            GcCustomAlphaNumTextBoxCell control = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.KeijouHoshuConstans.KEIJOU_CD] as GcCustomAlphaNumTextBoxCell;
            if (control == null
                || control.Value == null
                || string.IsNullOrEmpty(control.Value.ToString()))
                return result;
            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の形状CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        Cell cell = row.Cells[Const.KeijouHoshuConstans.KEIJOU_CD];
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

                    var rows = enumRowAll.Except(enumRow, new DataRowKeijouCompare());
                    var list = new List<GcCustomAlphaNumTextBoxCell>();
                    foreach (DataRow row in rows)
                    {
                        string keijouCd = row.Field<string>(Const.KeijouHoshuConstans.KEIJOU_CD);
                        GcCustomAlphaNumTextBoxCell cell = new GcCustomAlphaNumTextBoxCell();
                        cell.Value = keijouCd;
                        list.Add(cell);
                    }
                    cells.AddRange(list);
                }
                FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
                string str = vali.DuplicationCheck();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(str))
                {
                    msgLogic.MessageBoxShow("E022", "入力された形状CD");
                    result = false;
                }
            }
            return result;
        }
    }
}
