// $Id: ItakuKeiyakuHoshuValidator.cs 12324 2013-12-23 12:55:25Z ishibashi $
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using FWK = r_framework.Logic;

namespace ItakuKeiyakuHoshu.Validator
{
    /// <summary>
    /// 委託契約保守検証ロジック
    /// </summary>
    public class ItakuKeiyakuHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ItakuKeiyakuHoshuValidator()
        {
        }

        /// <summary>
        /// システムID重複チェック
        /// </summary>
        /// <param name="inputCd">入力された入金先CD</param>
        /// <returns>true:重複なし、false:重複あり</returns>
        public bool SystemIDValidator(DataTable dtKihon, out DialogResult result)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            string[] msg;

            result = DialogResult.None;

            // 使用されている場合
            if (dtKihon.Rows.Count > 0)
            {
                if ((bool)dtKihon.Rows[0]["DELETE_FLG"])
                {
                    // 削除済
                    msg = new string[1];
                    msg[0] = ItakuKeiyakuHoshu.Properties.Resources.SYSTEM_ID;
                    result = msgLogic.MessageBoxShow("E026", msg);
                }
                else
                {
                    result = msgLogic.MessageBoxShow("C022");
                }

                return false;
            }

            return true;
        }
    }
}
