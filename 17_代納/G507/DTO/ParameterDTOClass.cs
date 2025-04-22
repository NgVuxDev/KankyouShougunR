using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using r_framework.Const;

namespace Shougun.Core.PayByProxy.DainoDenpyoHakkou.DTO
{
    public class ParameterDTOClass
    {
        /// <summary>
        /// 画面のタイプ
        /// </summary>
        public WINDOW_TYPE WindowType { get; set; }
        /// <summary>
        /// 出荷システムId
        /// </summary>
        public string Shukka_SystemId { get; set; }
        /// <summary>
        /// 受入システムId
        /// </summary>
        public string Ukeire_SystemId { get; set; }
        /// <summary>
        ///		出荷売上日付	:	Uriage_Date	
        /// </summary>		
        public String Shukka_Date { get; set; }
        /// <summary>
        ///		受入支払日付	:	Shiharai_Date	
        /// </summary>		
        public String Ukeire_Date { get; set; }
        /// <summary>
        ///     売上消費税率
        /// </summary>
        public string Uriage_Shouhizei_Rate { get; set; }
        /// <summary>
        ///     支払消費税率
        /// </summary>
        public string Shiharai_Shouhizei_Rate { get; set; }
        /// <summary>
        ///		受入支払取引_税計算区分	:	Seikyu_Zeikeisan_Kbn	
        /// </summary>		
        public String Ukeire_Zeikeisan_Kbn { get; set; }
        /// <summary>
        ///		受入支払取引_税区分	:	Seikyu_Zei_Kbn	
        /// </summary>		
        public String Ukeire_Zei_Kbn { get; set; }
        /// <summary>
        ///		受入支払取引先CD	:	Torihikisaki_Cd	
        /// </summary>		
        public String Ukeire_Torihikisaki_Cd { get; set; }

        /// <summary>
        ///		出荷売上取引_税計算区分	:	Seikyu_Zeikeisan_Kbn	
        /// </summary>		
        public String Shukka_Zeikeisan_Kbn { get; set; }
        /// <summary>
        ///		出荷売上取引_税区分	:	Seikyu_Zei_Kbn	
        /// </summary>		
        public String Shukka_Zei_Kbn { get; set; }
        /// <summary>
        ///		出荷売上取引_取引先CD	:	Torihikisaki_Cd	
        /// </summary>		
        public String Shukka_Torihikisaki_Cd { get; set; }
        /// <summary>
        ///		出荷売上分前回残高	:	Seikyu_Zenkai_Zentaka	
        /// </summary>		
        public String Shukka_Zenkai_Zentaka { get; set; }
        /// <summary>
        ///		出荷売上分今回金額	:	Seikyu_Konkai_Kingaku	
        /// </summary>		
        public String Shukka_Konkai_Kingaku { get; set; }
        /// <summary>
        ///		出荷売上分今回税額	:	Seikyu_Konkai_Zeigaku	
        /// </summary>		
        public String Shukka_Konkai_Zeigaku { get; set; }
        /// <summary>
        ///		出荷売上分今回取引	:	Seikyu_Konkai_Rorihiki	
        /// </summary>		
        public String Shukka_Konkai_Rorihiki { get; set; }
        /// <summary>
        ///		出荷売上分差引残高	:	Seikyu_Sagaku_Zentaka	
        /// </summary>		
        public String Shukka_Sagaku_Zentaka { get; set; }
        /// <summary>
        ///		受入支払分前回残高	:	Shiharai_Zenkai_Zentaka	
        /// </summary>		
        public String Ukeire_Zenkai_Zentaka { get; set; }
        /// <summary>
        ///		受入支払分今回金額	:	Shiharai_Konkai_Kingaku	
        /// </summary>		
        public String Ukeire_Konkai_Kingaku { get; set; }
        /// <summary>
        ///		受入支払分今回税額	:	Shiharai_Konkai_Zeigaku	
        /// </summary>		
        public String Ukeire_Konkai_Zeigaku { get; set; }
        /// <summary>
        ///		受入支払分今回取引	:	Shiharai_Konkai_Rorihiki	
        /// </summary>		
        public String Ukeire_Konkai_Rorihiki { get; set; }
        /// <summary>
        ///		受入支払分差引残高	:	Shiharai_Sagaku_Zentaka	
        /// </summary>		
        public String Ukeire_Sagaku_Zentaka { get; set; }
        /// <summary>
        ///		出荷売上伝票明細リスト	:	Seikyu_Out_Tenpyo_Cnt	
        /// </summary>		
        public List<MeiseiDTOClass> Shukka_Out_Tenpyo_Cnt { get; set; }
        /// <summary>
        ///		受入支払伝票明細リスト	:	Shiharai_Out_Tenpyo_Cnt	
        /// </summary>		
        public List<MeiseiDTOClass> Ukeire_Out_Tenpyo_Cnt { get; set; }
    }
}
