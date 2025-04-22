using System.Collections.Generic;
using System.Data;
using System.Linq;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Logic;
using FWK = r_framework.Logic;

namespace Shougun.Core.Master.UnchiHinmeiHoshu.Validator
{
    /// <summary>
    /// 運賃品名入力検証ロジック
    /// </summary>
    public class UnchiHinmeiHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UnchiHinmeiHoshuValidator()
        {
        }

        /// <summary>
        /// 運賃品名CD重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool HinmeiCDValidator(GcMultiRow gcMultiRow, DataTable dt, DataTable dtAll, bool isAllSearch)
        {
            // 要実装方法検討。現段階は仮実装。

            bool result = true;

            GcCustomAlphaNumTextBoxCell control = gcMultiRow[gcMultiRow.CurrentRow.Index, Const.UnchiHinmeiHoshuConstans.UNCHIN_HINMEI_CD] as GcCustomAlphaNumTextBoxCell;
            if (control == null
                || control.Value == null
                || string.IsNullOrEmpty(control.Value.ToString()))
                return result;

            // 重複チェック
            {
                var cells = new List<Cell>();

                // 表示分(検索条件による抽出分)
                {
                    // カレント行以外の社員CDを保持するリスト
                    var list = new List<Cell>();

                    foreach (Row row in gcMultiRow.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }

                        Cell cell = row.Cells[Const.UnchiHinmeiHoshuConstans.UNCHIN_HINMEI_CD];
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

                    var rows = enumRowAll.Except(enumRow, new DataRowUnchiHinmeiCompare());

                    var list = new List<GcCustomAlphaNumTextBoxCell>();
                    foreach (DataRow row in rows)
                    {
                        string cd = row.Field<string>(Const.UnchiHinmeiHoshuConstans.UNCHIN_HINMEI_CD);

                        GcCustomAlphaNumTextBoxCell cell = new GcCustomAlphaNumTextBoxCell();
                        cell.Value = cd;
                        list.Add(cell);
                    }
                    cells.AddRange(list);
                }

                FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
                string str = vali.DuplicationCheck();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!string.IsNullOrEmpty(str))
                {
                    msgLogic.MessageBoxShow("E022", "入力された運賃品名CD");
                    result = false;
                }
            }

            return result;
        }
    }
}
