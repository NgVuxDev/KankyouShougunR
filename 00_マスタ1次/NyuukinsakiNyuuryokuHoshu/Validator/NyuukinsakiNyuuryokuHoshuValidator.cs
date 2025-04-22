// $Id: NyuukinsakiNyuuryokuHoshuValidator.cs 14207 2014-01-16 03:45:34Z sugioka $
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using FWK = r_framework.Logic;

namespace NyuukinsakiNyuuryokuHoshu.Validator
{
    /// <summary>
    /// 入金先保守検証ロジック
    /// </summary>
    public class NyuukinsakiNyuuryokuHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public NyuukinsakiNyuuryokuHoshuValidator()
        {
        }

        /// <summary>
        /// 入金先CD重複チェック
        /// </summary>
        /// <param name="inputCd">入力された入金先CD</param>
        /// <returns>true:重複なし、false:重複あり</returns>
        public bool NyuukinsakiCDValidator(DataTable dtNyuukin, DataTable dtTorihiki, out DialogResult result)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            var torihikiUseFlg = false;

            // 取引先で使用されている場合
            if (dtTorihiki.Rows.Count > 0)
            {
                torihikiUseFlg = true;
            }

            // 入金先で使用されている場合
            if (dtNyuukin.Rows.Count > 0)
            {
                if ((bool)dtNyuukin.Rows[0]["DELETE_FLG"])
                {
                    // 削除済
                    if (torihikiUseFlg)
                    {
                        // 取引先で使用されている場合、復活不可能
                        msgLogic.MessageBoxShow("E142", "該当の入金先", "取引先作成時");
                        result = DialogResult.None;
                        return false;
                    }
                    else
                    {
                        // 削除されたデータを復活させるかどうか確認する
                        result = msgLogic.MessageBoxShow("C057");
                    }
                }
                else
                {
                    result = msgLogic.MessageBoxShow("C017");
                }

                return false;
            }

            result = DialogResult.None;

            return true;
        }

        /// <summary>
        /// フリコミ人名重複チェック
        /// </summary>
        /// <param name="gcMultiRow"></param>
        /// <param name="dt"></param>
        /// <param name="dtAll"></param>
        /// <param name="isAllSearch"></param>
        /// <returns></returns>
        public bool FurikomiNameValidator(GcMultiRow gcMultiRow)
        {
            GcCustomTextBoxCell control =
                gcMultiRow[gcMultiRow.CurrentRow.Index, Const.NyuukinsakiNyuuryokuHoshuConstans.FURIKOMI_NAME] as GcCustomTextBoxCell;

            // 編集セルの入力値チェック
            if (control == null
                || control.Value == null
                || string.IsNullOrEmpty(control.Value.ToString()))
            {
                return true;
            }

            var cells = new List<Cell>();   // カレント行以外のフリコミ先名を保持するリスト

            // 新規行、選択行以外のデータを抽出
            foreach (Row row in gcMultiRow.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                Cell cell = row.Cells[Const.NyuukinsakiNyuuryokuHoshuConstans.FURIKOMI_NAME];
                if (cell.Selected)
                {
                    continue;
                }

                cells.Add(cell);
            }

            // 重複チェック
            FWK.Validator vali = new FWK.Validator(control, cells.ToArray());
            string str = vali.DuplicationCheck();

            if (!string.IsNullOrEmpty(str))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E013");
                return false;
            }

            return true;
        }
    }
}
