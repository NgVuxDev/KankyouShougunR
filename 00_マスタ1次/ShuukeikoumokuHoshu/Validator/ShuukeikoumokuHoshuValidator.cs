// $Id: ShuukeikoumokuHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
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

namespace ShuukeikoumokuHoshu.Validator
{
    /// <summary>
    /// 集計項目保守検証ロジック
    /// </summary>
    public class ShuukeikoumokuHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShuukeikoumokuHoshuValidator()
        {
        }

        /// <summary>
        /// 集計項目保守CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <returns></returns>
        public bool ShuukeikoumokuCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            bool result = true;

            GcCustomAlphaNumTextBoxCell control = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.ShuukeikoumokuHoshuConstans.SHUUKEI_KOUMOKU_CD] as GcCustomAlphaNumTextBoxCell;

            if (control == null
                || control.Value == null
                || string.IsNullOrEmpty(control.Value.ToString()))
                return result;
            

            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の銀行CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.ShuukeikoumokuHoshuConstans.SHUUKEI_KOUMOKU_CD];
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

                    var rows = enumRowAll.Except(enumRow, new DataRowShuukeikoumokuHoshuCompare());

                    var list = new List<GcCustomAlphaNumTextBoxCell>();
                    foreach (DataRow row in rows)
                    {
                        string shuukeikoumokuCd = row.Field<string>(Const.ShuukeikoumokuHoshuConstans.SHUUKEI_KOUMOKU_CD);

                        GcCustomAlphaNumTextBoxCell cell = new GcCustomAlphaNumTextBoxCell();
                        cell.Value = shuukeikoumokuCd;
                        list.Add(cell);
                    }
                    cells.AddRange(list);
              }

              FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
              string str = vali.DuplicationCheck();

              MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
              if (!string.IsNullOrEmpty(str))
              {
                   msgLogic.MessageBoxShow("E022", "入力された集計項目CD");
                   result = false;
              }
         }

            return result;
        }
    }
}
