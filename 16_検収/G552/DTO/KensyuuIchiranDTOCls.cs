using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlTypes;

namespace Shougun.Core.Inspection.KenshuIchiranJokenShiteiPopup
{
    public class KensyuuIchiranDTOCls
    {
        public int Shukka_Entry_KYOTEN_CD { get; set; }
        public string Shukka_Entry_KYOTEN_NAME { get; set; }
        public string Shukka_Entry_KENSHUHINMEI_CD_1 { get; set; }
        public string Shukka_Entry_KENSHUHINMEI_NAME_1 { get; set; }
        public string Shukka_Entry_KENSHUHINMEI_CD_2 { get; set; }
        public string Shukka_Entry_KENSHUHINMEI_NAME_2 { get; set; }
        public string Shukka_Entry_KENSHU_UMU { get; set; }
        /// <summary>
        /// 検収状況
        /// 1：未検収（KENSHU_MUST_KBN = 1 AND KENSHU_DATE IS NULL）
        /// 2：検収済（KENSHU_MUST_KBN = 1 AND KENSHU_DATE IS NOT NULL）
        /// </summary>
        public short Shukka_Entry_KENSHU_JYOUKYOU { get; set; }

        public SqlDateTime Shukka_Entry_Denpyou_Date_Begin { get; set; }
        public SqlDateTime Shukka_Entry_Denpyou_Date_End { get; set; }
        public SqlDateTime Shukka_Entry_Kenshu_Date_Begin { get; set; }
        public SqlDateTime Shukka_Entry_Kenshu_Date_End { get; set; }
        public String Shukka_Detail_Hinmei_Cd_1 { get; set; }
        public String Shukka_Detail_Hinmei_Name_1 { get; set; }
        public String Shukka_Detail_Hinmei_Cd_2 { get; set; }
        public String Shukka_Detail_Hinmei_Name_2 { get; set; }
        public String Kenshu_Detail_Hinmei_Cd_1 { get; set; }
        public String Kenshu_Detail_Hinmei_Name_1 { get; set; }
        public String Kenshu_Detail_Hinmei_Cd_2 { get; set; }
        public String Kenshu_Detail_Hinmei_Name_2 { get; set; }
        public String Shukka_Entry_Torihikisaki_Cd_1 { get; set; }
        public String Shukka_Entry_Torihikisaki_Name_1 { get; set; }
        public String Shukka_Entry_Torihikisaki_Cd_2 { get; set; }
        public String Shukka_Entry_Torihikisaki_Name_2 { get; set; }
        public String Shukka_Entry_Gyousha_Cd_1 { get; set; }
        public String Shukka_Entry_Gyousha_Name_1 { get; set; }
        public String Shukka_Entry_Gyousha_Cd_2 { get; set; }
        public String Shukka_Entry_Gyousha_Name_2 { get; set; }
        public String Shukka_Entry_Genba_Cd_1 { get; set; }
        public String Shukka_Entry_Genba_Name_1 { get; set; }
        public String Shukka_Entry_Genba_Cd_2 { get; set; }
        public String Shukka_Entry_Genba_Name_2 { get; set; }
        public String Shukka_Entry_Nizumi_Gyousha_Cd_1 { get; set; }
        public String Shukka_Entry_Nizumi_Gyousha_Name_1 { get; set; }
        public String Shukka_Entry_Nizumi_Gyousha_Cd_2 { get; set; }
        public String Shukka_Entry_Nizumi_Gyousha_Name_2 { get; set; }
        public String Shukka_Entry_Nizumi_Genba_Cd_1 { get; set; }
        public String Shukka_Entry_Nizumi_Genba_Name_1 { get; set; }
        public String Shukka_Entry_Nizumi_Genba_Cd_2 { get; set; }
        public String Shukka_Entry_Nizumi_Genba_Name_2 { get; set; }


        public String Shukka_Entry_CORP_RYAKU_NAME { get; set; }
    }
}
