using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace KongouHaikibutsuHoshu.Dto
{
    public class KongouHaikibutsuHoshuDto
    {
        public M_HAIKI_SHURUI HaikiShuruiSearchString { get; set; }
        public M_KONGOU_HAIKIBUTSU KongouHaikibutsuSearchString { get; set; }

        /// <summary>
        /// 廃棄物種類Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesHaikiShuruiExistCheck()
        {

            // 廃棄物種類コード
            if (!string.IsNullOrEmpty(this.HaikiShuruiSearchString.HAIKI_SHURUI_CD))
            {                
                return true;
            }

            // 廃棄物種類略称
            if (!string.IsNullOrEmpty(this.HaikiShuruiSearchString.HAIKI_SHURUI_NAME_RYAKU))
            {
                return true;
            }

            return false;
        }
    }
}
