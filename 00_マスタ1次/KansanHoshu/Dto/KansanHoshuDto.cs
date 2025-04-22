using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace KansanHoshu.Dto
{
    public class KansanHoshuDto
    {
        public M_KANSAN KansanSearchString { get; set; }
        public M_HINMEI HinmeiSearchString { get; set; }

        /// <summary>
        /// 廃棄物種類Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesHaikiShuruiExistCheck()
        {

            // 廃棄物種類コード
            if (!string.IsNullOrEmpty(this.HinmeiSearchString.HINMEI_NAME_RYAKU))
            {                
                return true;
            }
            return false;
        }
    }
}
