using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace KeiryouChouseiHoshu.Dto
{
    public class KeiryouChouseiHoshuDto
    {
        public M_HINMEI HinmeiSearchString { get; set; }
        public M_SHURUI ShuruiSearchString { get; set; }
        public M_UNIT UnitSearchString { get; set; }
        public M_KEIRYOU_CHOUSEI KeiryouChouseiSearchString { get; set; }

        /// <summary>
        /// 計量調整Entityの検索条件プロパティ設定有無チェック
        /// </summary>
        /// <returns>true:設定あり、false:設定なし</returns>
        public int PropertiesKeiryouChouseiExistCheck()
        {

            // 種類コード
            if (!string.IsNullOrEmpty(this.HinmeiSearchString.SHURUI_CD))
            {                
                return 1;
            }
            // 品名
            if (!string.IsNullOrEmpty(this.HinmeiSearchString.HINMEI_NAME_RYAKU))
            {
                return 2;
            }
            // 種類名
            if (!string.IsNullOrEmpty(this.ShuruiSearchString.SHURUI_NAME_RYAKU))
            {
                return 3;
            }
            // 単位区分
            if (!string.IsNullOrEmpty(this.UnitSearchString.UNIT_NAME_RYAKU))
            {
                return 4;
            }

            return 0;
        }
    }
}
