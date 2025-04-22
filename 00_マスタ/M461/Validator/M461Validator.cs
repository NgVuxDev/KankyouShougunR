// $Id: M461Validator.cs 37928 2014-12-22 08:00:05Z y-hosokawa@takumi-sys.co.jp $
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using FWK = r_framework.Logic;

namespace Shougun.Core.Master.HikiaiTorihikisakiHoshu.Validator
{
    /// <summary>
    /// 引合取引先保守検証ロジック
    /// </summary>
    public class M461Validator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public M461Validator()
        {
        }

        /// <summary>
        /// 取引先CD重複チェック
        /// </summary>
        /// <param name="inputCd">入力された取引先CD</param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <returns>true:重複なし、false:重複あり</returns>
        public bool TorihikisakiCDValidator(DataTable dtTorihiki, bool isRegister, out DialogResult result)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 引合取引先で使用されている場合
            if (dtTorihiki.Rows.Count > 0)
            {
                // 登録処理の場合は再入力を促すエラーメッセージを表示
                if (isRegister)
                {
                    result = msgLogic.MessageBoxShow("E005", "取引先");
                }
                else if ((bool)dtTorihiki.Rows[0]["DELETE_FLG"])
                {
                    //// 削除済
                    //msgLogic.MessageBoxShow("E026", HikiaiTorihikisakiHoshu.Properties.Resources.CODE);
                    //result = DialogResult.None;

                    //表示用確認メッセージ
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
