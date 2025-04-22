using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.BusinessManagement.EigyouYojitsuKanrihyou.Dto
{
    internal class SearchNendoCondition
    {

        #region 検索条件
        /// <summary>検索条件</summary>
        public string busyouCD { get; set; } // 部署コード
        public string denpyouKbn { get; set; } // 伝票区分
        public DateTime nendo1FirstDay { get; set; } //年度1最初日
        public DateTime nendo2FirstDay { get; set; } //年度2最初日
        public DateTime nendo3FirstDay { get; set; } //年度3最初日
        public DateTime nendo4FirstDay { get; set; } //年度4最初日
        public DateTime nendo5FirstDay { get; set; } //年度5最初日
        public DateTime nendo6FirstDay { get; set; } //年度6最初日
        public DateTime nendo7FirstDay { get; set; } //年度7最初日
        public DateTime nendo8FirstDay { get; set; } //年度8最初日
        public DateTime nendo9FirstDay { get; set; } //年度9最初日
        public DateTime nendo1LastDay { get; set; } //年度1最終日
        public DateTime nendo2LastDay { get; set; } //年度2最終日
        public DateTime nendo3LastDay { get; set; } //年度3最終日
        public DateTime nendo4LastDay { get; set; } //年度4最終日
        public DateTime nendo5LastDay { get; set; } //年度5最終日
        public DateTime nendo6LastDay { get; set; } //年度6最終日
        public DateTime nendo7LastDay { get; set; } //年度7最終日
        public DateTime nendo8LastDay { get; set; } //年度8最終日
        public DateTime nendo9LastDay { get; set; } //年度9最終日
        public string nendo1 { get; set; } // 年度1
        public string nendo2 { get; set; } // 年度2
        public string nendo3 { get; set; } // 年度3
        public string nendo4 { get; set; } // 年度4
        public string nendo5 { get; set; } // 年度5
        public string nendo6 { get; set; } // 年度6
        public string nendo7 { get; set; } // 年度7
        public string nendo8 { get; set; } // 年度8
        public string nendo9 { get; set; } // 年度9
        public string jinen1 { get; set; } // 年度の次の年1
        public string jinen2 { get; set; } // 年度の次の年2
        public string jinen3 { get; set; } // 年度の次の年3
        public string jinen4 { get; set; } // 年度の次の年4
        public string jinen5 { get; set; } // 年度の次の年5
        public string jinen6 { get; set; } // 年度の次の年6
        public string jinen7 { get; set; } // 年度の次の年7
        public string jinen8 { get; set; } // 年度の次の年8
        public string jinen9 { get; set; } // 年度の次の年9
        public bool JINEN_FLG_01 { get; set; } // 年度の次の年か判定フラグ1
        public bool JINEN_FLG_02 { get; set; } // 年度の次の年か判定フラグ2
        public bool JINEN_FLG_03 { get; set; } // 年度の次の年か判定フラグ3
        public bool JINEN_FLG_04 { get; set; } // 年度の次の年か判定フラグ4
        public bool JINEN_FLG_05 { get; set; } // 年度の次の年か判定フラグ5
        public bool JINEN_FLG_06 { get; set; } // 年度の次の年か判定フラグ6
        public bool JINEN_FLG_07 { get; set; } // 年度の次の年か判定フラグ7
        public bool JINEN_FLG_08 { get; set; } // 年度の次の年か判定フラグ8
        public bool JINEN_FLG_09 { get; set; } // 年度の次の年か判定フラグ9
        public bool JINEN_FLG_10 { get; set; } // 年度の次の年か判定フラグ10
        public bool JINEN_FLG_11 { get; set; } // 年度の次の年か判定フラグ11
        public bool JINEN_FLG_12 { get; set; } // 年度の次の年か判定フラグ12
        #endregion
    }
}
