// $Id: GyoushaHoshuValidator.cs 233 2013-07-10 09:00:34Z tecs_suzuki $
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
using r_framework.Utility;
using r_framework.Dao;

namespace GyoushaHoshu.Validator
{
    /// <summary>
    /// 業者保守検証ロジック
    /// </summary>
    public class GyoushaHoshuValidator
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GyoushaHoshuValidator()
        {
        }

        /// <summary>
        /// 業者CD重複チェック
        /// </summary>
        /// <param name="gyoushaEntity"></param>
        /// <param name="isRegister">登録中か判断します</param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool GyoushaCDValidator(M_GYOUSHA gyoushaEntity,bool isRegister, out DialogResult result)
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            // 業者で使用されている場合
            if (gyoushaEntity != null)
            {
                // 登録処理の場合は再入力を促すエラーメッセージを表示
                if (isRegister)
                {
                    result = msgLogic.MessageBoxShow("E005","業者");
                }else if (gyoushaEntity.DELETE_FLG)
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

        /// <summary>
        /// 指定した業者に未削除の現場があるかどうか
        /// </summary>
        /// <returns>あればtrue</returns>
        internal bool HasGenba(string gyousha_cd)
        {
            LogUtility.DebugMethodStart(gyousha_cd);

            bool result = true;
            try
            {
                var sql = String.Format("SELECT COUNT(*) FROM M_GENBA WHERE GYOUSHA_CD = '{0}' AND DELETE_FLG = 0;", gyousha_cd);
                var dt = DaoInitUtility.GetComponent<IM_GYOUSHADao>().GetDateForStringSql(sql);

                result = Convert.ToInt32(dt.Rows[0][0]) > 0;
                return result;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }
    }
}
