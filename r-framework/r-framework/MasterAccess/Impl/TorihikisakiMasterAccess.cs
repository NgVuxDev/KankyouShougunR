using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace r_framework.MasterAccess.Impl
{
    class TorihikisakiMasterAccess : Base.AbstractMasterAcess
        <Dao.IM_TORIHIKISAKIDao, Entity.M_TORIHIKISAKI>
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TorihikisakiMasterAccess(CustomControl.ICustomControl control, object[] obj, object[] sendParam)
            : base(control, obj, sendParam,
                   "TORIHIKISAKI_CD", "取引先CD", "TORIHIKISAKI_NAME")
        {
        }

        /// <summary>
        /// 取引先コード重複チェック
        /// 入力された取引先コードがすでにＤＢに登録されてるかをチェックする、登録されている場合はエラーとなる
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public virtual string CodeEnteredCheck()
        {
            var returnStr = "";

            //空以外かつ、エラー無の場合 登録済みなのでエラーにする
            if ( !string.IsNullOrEmpty(CheckControl.GetResultText()) && string.IsNullOrEmpty(this.CodePresenceCheck()) ) //コードが存在したらエラーを返す
            {
                var messageUtil = new r_framework.Utility.MessageUtility();
                returnStr = messageUtil.GetMessage("E022").MESSAGE;
                returnStr = String.Format(returnStr, this.CheckControl.GetResultText());
            }
            return returnStr;
        }


    }

}
