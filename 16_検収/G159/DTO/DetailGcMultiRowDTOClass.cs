using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.Inspection.KensyuuIchiran
{
    public class KensyuuIchiranDetailMultirowDTOCls
    {
        ///////////////////////////////////////////////////////////////////
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_DENPYOU_DATE_BEGIN	
        ///// </summary>		
        //public DateTime Shukka_Entry_Denpyou_Date_Begin { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_DENPYOU_DATE_END	
        ///// </summary>		
        //public DateTime Shukka_Entry_Denpyou_Date_End { get; set; }

        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_KENSHU_DATE_BEGIN
        ///// </summary>		
        //public DateTime Shukka_Entry_Kenshu_Date_Begin { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_KENSHU_DATE_END	
        ///// </summary>		
        //public DateTime Shukka_Entry_Kenshu_Date_End { get; set; }

        ///// <summary>
        /////		売上日付	:	SHUKKA_DETAIL_HINMEI_CD_1	
        ///// </summary>		
        //public String Shukka_Detail_Hinmei_Cd_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_DETAIL_HINMEI_NAME_1	
        ///// </summary>		
        //public String Shukka_Detail_Hinmei_Name_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_DETAIL_HINMEI_CD_2	
        ///// </summary>		
        //public String Shukka_Detail_Hinmei_Cd_2 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_DETAIL_HINMEI_NAME_2	
        ///// </summary>		
        //public String Shukka_Detail_Hinmei_Name_2 { get; set; }

        ///// <summary>
        /////		売上日付	:	KENSHU_DETAIL_HINMEI_CD_1
        ///// </summary>		
        //public String Kenshu_Detail_Hinmei_Cd_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	KENSHU_DETAIL_HINMEI_NAME_1	
        ///// </summary>		
        //public String Kenshu_Detail_Hinmei_Name_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	KENSHU_DETAIL_HINMEI_CD_2
        ///// </summary>		
        //public String Kenshu_Detail_Hinmei_Cd_2 { get; set; }
        ///// <summary>
        /////		売上日付	:	KENSHU_DETAIL_HINMEI_NAME_2	
        ///// </summary>		
        //public String Kenshu_Detail_Hinmei_Name_2 { get; set; }

        ///// <summary>
        /////		売上日付	:	M_GENBA_Kenshu_Youhi	
        ///// </summary>		
        //public String M_Genba_Kenshu_Youhi { get; set; }

        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_TORIHIKISAKI_CD_1
        ///// </summary>		
        //public String Shukka_Entry_Torihikisaki_Cd_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_TORIHIKISAKI_NAME_1	
        ///// </summary>		
        //public String Shukka_Entry_Torihikisaki_Name_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_TORIHIKISAKI_CD_2	
        ///// </summary>		
        //public String Shukka_Entry_Torihikisaki_Cd_2 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_TORIHIKISAKI_NAME_2	
        ///// </summary>		
        //public String Shukka_Entry_Torihikisaki_Name_2 { get; set; }

        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_GYOUSHA_CD_1
        ///// </summary>		
        //public String Shukka_Entry_Gyousha_Cd_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_GYOUSHA_NAME_1	
        ///// </summary>		
        //public String Shukka_Entry_Gyousha_Name_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_GYOUSHA_CD_2	
        ///// </summary>		
        //public String Shukka_Entry_Gyousha_Cd_2 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_GYOUSHA_NAME_2	
        ///// </summary>		
        //public String Shukka_Entry_Gyousha_Name_2 { get; set; }

        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_GENBA_CD_1
        ///// </summary>		
        //public String Shukka_Entry_Genba_Cd_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_GENBA_NAME_1	
        ///// </summary>		
        //public String Shukka_Entry_Genba_Name_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_GENBA_CD_2	
        ///// </summary>		
        //public String Shukka_Entry_Genba_Cd_2 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_GENBA_NAME_2	
        ///// </summary>		
        //public String Shukka_Entry_Genba_Name_2 { get; set; }

        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_NIZUMI_GYOUSHA_CD_1	
        ///// </summary>		
        //public String Shukka_Entry_Nizumi_Gyousha_Cd_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_NIZUMI_GYOUSHA_NAME_1	
        ///// </summary>		
        //public String Shukka_Entry_Nizumi_Gyousha_Name_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_NIZUMI_GYOUSHA_CD_2	
        ///// </summary>		
        //public String Shukka_Entry_Nizumi_Gyousha_Cd_2 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_NIZUMI_GYOUSHA_NAME_2	
        ///// </summary>		
        //public String Shukka_Entry_Nizumi_Gyousha_Name_2 { get; set; }

        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_NIZUMI_GENBA_CD_1
        ///// </summary>		
        //public String Shukka_Entry_Nizumi_Genba_Cd_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_NIZUMI_GENBA_CD_1	
        ///// </summary>		
        //public String Shukka_Entry_Nizumi_Genba_Name_1 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_NIZUMI_GENBA_CD_2
        ///// </summary>		
        //public String Shukka_Entry_Nizumi_Genba_Cd_2 { get; set; }
        ///// <summary>
        /////		売上日付	:	SHUKKA_ENTRY_NIZUMI_GENBA_CD_2	
        ///// </summary>		
        //public String Shukka_Entry_Nizumi_Genba_Name_2 { get; set; }

        ///////////////////////////////////////////////////////////////////////
        /// <summary>
        ///		売上日付	:	SHUKKA_ENTRY_SHUKKA_NUMBER	
        /// </summary>		
        public string Shukka_Entry_Shukka_Number { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_ENTRY_ROW_NO	
        /// </summary>		
        public int Shukka_Entry_Row_No { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_ENTRY_DENPYOU_DATE	
        /// </summary>		
        public String Shukka_Entry_Denpyou_Date { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_ENTRY_KENSHU_DATE	
        /// </summary>		
        public String Shukka_Entry_Kenshu_Date { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_ENTRY_TORIHIKISAKI_CD	
        /// </summary>		
        public String Shukka_Entry_Torihikisaki_Cd { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_ENTRY_TORIHIKISAKI_NAME
        /// </summary>		
        public String Shukka_Entry_Torihikisaki_Name { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_ENTRY_GYOUSHA_CD	
        /// </summary>		
        public String Shukka_Entry_Gyousha_Cd { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_ENTRY_GYOUSHA_NAME
        /// </summary>		
        public String Shukka_Entry_Gyousha_Name { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_ENTRY_GENBA_CD	
        /// </summary>		
        public String Shukka_Entry_Genba_Cd { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_ENTRY_GENBA_NAME	
        /// </summary>		
        public String Shukka_Entry_Genba_Name { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_DETAIL_HINMEI_CD	
        /// </summary>		
        public String Shukka_Detail_Hinmei_Cd { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_DETAIL_HINMEI_NAME	
        /// </summary>		
        public String Shukka_Detail_Hinmei_Name { get; set; }
        /// <summary>
        ///		売上日付	:	KENSYU_DETAIL_HINMEI_CD	
        /// </summary>		
        public String Kensyu_Detail_Hinmei_Cd { get; set; }
        /// <summary>
        ///		売上日付	:	KENSYU_DETAIL_HINMEI_NAME	
        /// </summary>		
        public String Kensyu_Detail_Hinmei_Name { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_DETAIL_NET_JYUURYOU	
        /// </summary>		
        public string Shukka_Detail_Net_Jyuuryou { get; set; }
        /// <summary>
        ///		売上日付	:	KENSYU_DETAIL_KENSHU_NET	
        /// </summary>		
        public string Kensyu_Detail_Kenshu_Net { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_DETAIL_SUURYOU	
        /// </summary>		
        public string Shukka_Detail_Suuryou { get; set; }
        /// <summary>
        ///		売上日付	:	KENSYU_DETAIL_SUURYOU	
        /// </summary>		
        public string Kensyu_Detail_Suuryou { get; set; }
        /// <summary>
        ///		売上日付	:	KENSYU_DETAIL_BUBIKI	
        /// </summary>		
        public string Kensyu_Detail_Bubiki { get; set; }
        /// <summary>
        ///		売上日付	:	M_UNIT_SHUKKA_DETAIL_UNIT_NAME_RYAKU	
        /// </summary>		
        public String M_Unit_Shukka_Detail_Unit_Name_Ryaku { get; set; }
        /// <summary>
        ///		売上日付	:	M_UNIT_KENSYU_DETAIL_UNIT_NAME_RYAKU	
        /// </summary>		
        public String M_Unit_Kensyu_Detail_Unit_Name_Ryaku { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_DETAIL_TANKA	
        /// </summary>		
        public string Shukka_Detail_Tanka { get; set; }
        /// <summary>
        ///		売上日付	:	KENSYU_DETAIL_TANKA	
        /// </summary>		
        public string Kensyu_Detail_Tanka { get; set; }
        /// <summary>
        ///		売上日付	:	SHUKKA_DETAIL_KINGAKU	
        /// </summary>		
        public string Shukka_Detail_Kingaku { get; set; }
        /// <summary>
        ///		売上日付	:	KENSYU_DETAIL_KINGAKU	
        /// </summary>		
        public string Kensyu_Detail_Kingaku { get; set; }
    }
}
