// $Id: HikiaiGyoushaValidator.cs 462 2013-10-10 09:00:34Z tecs_suzuki $
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

namespace Shougun.Core.Master.HikiaiGyousha.Validator
{
    /// <summary>
    /// 業者保守検証ロジック
    /// </summary>
    public class HikiaiGyoushaValidator
    {

        /// <summary>
        /// 業者CD重複チェック
        /// </summary>
        /// <param name="gyoushaEntity"></param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool GyoushaCDValidator(M_HIKIAI_GYOUSHA gyoushaEntity, bool isRegister, out DialogResult result)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 業者で使用されている場合
            if (gyoushaEntity != null)
            {
                // 登録処理の場合は再入力を促すエラーメッセージを表示
                if (isRegister)
                {
                    result = msgLogic.MessageBoxShow("E005", "業者");
                }
                else if (gyoushaEntity.DELETE_FLG)
                {
                    // 削除済
                    //msgLogic.MessageBoxShow("E026", "コード");
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
