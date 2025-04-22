// $Id: GenbaHoshuValidator.cs 37928 2014-12-22 08:00:05Z y-hosokawa@takumi-sys.co.jp $
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Const;
using FWK = r_framework.Logic;
using System.Linq;
using System;

namespace GenbaHoshu.Validator
{
    /// <summary>
    /// 業者保守検証ロジック
    /// </summary>
    public class GenbaHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GenbaHoshuValidator()
        {
        }

        /// <summary>
        /// 現場CD重複チェック
        /// </summary>
        /// <param name="genbaEntity"></param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool GenbaCDValidator(M_GENBA genbaEntity, bool isRegister, out DialogResult result)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 現場で使用されている場合
            if (genbaEntity != null)
            {
                // 登録処理の場合は再入力を促すエラーメッセージを表示
                if (isRegister)
                {
                    result = msgLogic.MessageBoxShow("E005", "現場");
                }
                else if (genbaEntity.DELETE_FLG)
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
