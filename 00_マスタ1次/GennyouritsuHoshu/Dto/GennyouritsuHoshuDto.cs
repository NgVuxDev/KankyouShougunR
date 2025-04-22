// $Id: GennyouritsuHoshuDto.cs 12324 2013-12-23 12:55:25Z ishibashi $
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace GennyouritsuHoshu.Dto
{
    public class GennyouritsuHoshuDto
    {
        public M_HAIKI_NAME HaikiNameSearchString { get; set; }
        public M_SHOBUN_HOUHOU ShobunHouhouSearchString { get; set; }
        public M_GENNYOURITSU GennyouritsuSearchString { get; set; }
        public M_GENNYOURITSU AllSearchString { get; set; }

        /// <summary>
        /// 廃棄物名称Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesHaikiNameExistCheck()
        {
            // 廃棄物名称
            if (!string.IsNullOrEmpty(this.HaikiNameSearchString.HAIKI_NAME_RYAKU))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 処分方法Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public bool PropertiesShobunHouhouExistCheck()
        {
            // 処分方法名称
            if (!string.IsNullOrEmpty(this.ShobunHouhouSearchString.SHOBUN_HOUHOU_NAME_RYAKU))
            {
                return true;
            }

            return false;
        }
    }
}
