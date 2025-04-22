using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace ManifestKansanHoshu.Dto
{
    public class ManifestKansanHoshuDto
    {
        public M_MANIFEST_KANSAN manifestKansanSearchString { get; set; }
        public M_HAIKI_NAME haikiNameSearchString { get; set; }
        public M_NISUGATA nisugataSearchString { get; set; }
        public M_UNIT UnitSearchString { get; set; }

        /// <summary>
        /// 廃棄物名称Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesHaikiNameExistCheck()
        {
            // 廃棄物略称名
            if (this.haikiNameSearchString != null &&  !string.IsNullOrEmpty(this.haikiNameSearchString.HAIKI_NAME_RYAKU))
            {
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// 荷姿Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesNisugataExistCheck()
        {
            // 荷姿名
            if (this.nisugataSearchString != null && !string.IsNullOrEmpty(this.nisugataSearchString.NISUGATA_NAME))
            {
                return true;
            }
            
            return false;
        }

        /// <summary>
        /// 単位Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesUnitExistsCheck()
        {
            var ret = false;

            // 単位名称
            if (!String.IsNullOrEmpty(this.UnitSearchString.UNIT_NAME_RYAKU))
            {
                ret = true;
            }

            return ret;
        }
    }
}
