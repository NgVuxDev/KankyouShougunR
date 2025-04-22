// $Id: TorihikisakiHoshuValidator.cs 37928 2014-12-22 08:00:05Z y-hosokawa@takumi-sys.co.jp $
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using FWK = r_framework.Logic;

namespace TorihikisakiHoshu.Validator
{
    /// <summary>
    /// 取引先保守検証ロジック
    /// </summary>
    public class TorihikisakiHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TorihikisakiHoshuValidator()
        {
        }

        /// <summary>
        /// 取引先CD重複チェック
        /// </summary>
        /// <param name="inputCd">入力された取引先CD</param>
        /// <param name="dtNyuukin">入金先マスタ</param>
        /// <param name="dtSyukkin">出金先マスタ</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns>true:重複なし、false:重複あり</returns>
        public bool TorihikisakiCDValidator(DataTable dtTorihiki, DataTable dtNyuukin, DataTable dtSyukkin, bool isRegister, out DialogResult result)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 取引先で使用されておらず、入金先で使用されている場合
            if (dtTorihiki.Rows.Count == 0 && dtNyuukin.Rows.Count > 0)
            {
                msgLogic.MessageBoxShow("E025", TorihikisakiHoshu.Properties.Resources.TORIHIKISAKI, TorihikisakiHoshu.Properties.Resources.NYUUKINSAKI);
                result = DialogResult.None;
                return false;
            }

            // 取引先で使用されておらず、出金先で使用されている場合
            if (dtTorihiki.Rows.Count == 0 && dtSyukkin.Rows.Count > 0)
            {
                msgLogic.MessageBoxShow("E025", TorihikisakiHoshu.Properties.Resources.TORIHIKISAKI, TorihikisakiHoshu.Properties.Resources.SYUKKINSAKI);
                result = DialogResult.None;
                return false;
            }

            // 取引先で使用されている場合
            if (dtTorihiki.Rows.Count > 0)
            {
                // 登録処理の場合は再入力を促すエラーメッセージを表示
                if (isRegister)
                {
                    result = msgLogic.MessageBoxShow("E005", "取引先");
                }
                else if ((bool)dtTorihiki.Rows[0]["DELETE_FLG"])
                {
                    // 削除済
                    // 削除されている明細を入力から修正実行されたときは復活をさせるかさせないかの選択ダイアログを表示
                    // 「はい」を選択した場合は修正モードで表示を行い、登録することにより削除フラグを外す。
                    result = msgLogic.MessageBoxShow("C057");
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
    }
}
