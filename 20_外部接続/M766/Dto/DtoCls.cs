using System;
using r_framework.Entity;

namespace Shougun.Core.ExternalConnection.SmsReceiverNyuuryoku
{
    public class DtoCls
    {
        public M_SMS_RECEIVER ReceiverSearchString { get; set; }

        /// <summary>
        /// 受信者Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        internal bool PropertiesUnitExistsCheck()
        {
            var ret = false;

            // 受信者名
            if (!String.IsNullOrEmpty(this.ReceiverSearchString.RECEIVER_NAME))
            {
                ret = true;
            }

            return ret;
        }
    }
}
