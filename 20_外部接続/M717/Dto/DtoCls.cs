using System;
using r_framework.Entity;

namespace Shougun.Core.ExternalConnection.ClientIdNyuuryoku
{
    public class DtoCls
    {
        public M_DENSHI_KEIYAKU_CLIENT_ID ClientIdSearchString { get; set; }
        public M_SHAIN ShainSearchString { get; set; }

        /// <summary>
        /// 社員Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        internal bool PropertiesUnitExistsCheck()
        {
            var ret = false;

            // 社員名
            if (!String.IsNullOrEmpty(this.ShainSearchString.SHAIN_NAME))
            {
                ret = true;
            }

            return ret;
        }
    }
}
