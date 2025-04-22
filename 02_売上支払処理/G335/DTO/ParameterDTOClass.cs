using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.SalesPayment.DenpyouHakou
{
    public class ParameterDTOClass
    {
        /// <summary>
        ///		売上日付	:	Uriage_Date	
        /// </summary>		
        public String Uriage_Date { get; set; }
        /// <summary>
        ///		支払日付	:	Shiharai_Date	
        /// </summary>		
        public String Shiharai_Date { get; set; }
        /// <summary>
        ///		取引先CD	:	Torihikisaki_Cd	
        /// </summary>		
        public String Torihikisaki_Cd { get; set; }
        /// <summary>
        ///		業者CD	:	Gyousha_Cd	
        /// </summary>		
        public String Gyousha_Cd { get; set; }
        /// <summary>
        ///		売上合計金額（税ぬき）	:	Uriage_Amount_Total	
        /// </summary>		
        public String Uriage_Amount_Total { get; set; }
        /// <summary>
        ///		支払合計金額（税ぬき）	:	Shiharai_Amount_Total	
        /// </summary>		
        public String Shiharai_Amount_Total { get; set; }
        /// <summary>
        ///		伝票明細リスト	:	Tenpyo_Cnt	
        /// </summary>		
        public List<MeiseiDTOClass> Tenpyo_Cnt { get; set; }
        /// <summary>
        ///		確定区分	:	Kakute_Kbn	
        /// </summary>		
        public String Kakute_Kbn { get; set; }
        /// <summary>
        ///		売上/支払区分	:	Ur_Sh_Kbn	
        /// </summary>		
        public String Ur_Sh_Kbn { get; set; }
        /// <summary>
        ///		品名CD	:	Hinmae_Cd	
        /// </summary>		
        public String Hinmae_Cd { get; set; }
        /// <summary>
        ///		金額（税ぬき）	:	Kingaku	
        /// </summary>		
        public String Kingaku { get; set; }
        /// <summary>
        ///		請求取引_税計算区分	:	Seikyu_Zeikeisan_Kbn	
        /// </summary>		
        public String Seikyu_Zeikeisan_Kbn { get; set; }
        /// <summary>
        ///		請求取引_税区分	:	Seikyu_Zei_Kbn	
        /// </summary>		
        public String Seikyu_Zei_Kbn { get; set; }
        /// <summary>
        ///		請求取引_取引区分	:	Seikyu_Rohiki_Kbn	
        /// </summary>		
        public String Seikyu_Rohiki_Kbn { get; set; }
        /// <summary>
        ///		請求取引_精算区分	:	Seikyu_Seisan_Kbn	
        /// </summary>		
        public String Seikyu_Seisan_Kbn { get; set; }
        /// <summary>
        ///		請求取引_伝票発行区分	:	Seikyu_Hakou_Kbn	
        /// </summary>		
        public String Seikyu_Hakou_Kbn { get; set; }
        /// <summary>
        ///		支払取引_税計算区分	:	Shiharai_Zeikeisan_Kbn	
        /// </summary>		
        public String Shiharai_Zeikeisan_Kbn { get; set; }
        /// <summary>
        ///		支払取引_税区分	:	Shiharai_Zei_Kbn	
        /// </summary>		
        public String Shiharai_Zei_Kbn { get; set; }
        /// <summary>
        ///		支払取引_取引区分	:	Shiharai_Rohiki_Kbn	
        /// </summary>		
        public String Shiharai_Rohiki_Kbn { get; set; }
        /// <summary>
        ///		支払取引_精算区分	:	Shiharai_Seisan_Kbn	
        /// </summary>		
        public String Shiharai_Seisan_Kbn { get; set; }
        /// <summary>
        ///		支払取引_伝票発行区分	:	Shiharai_Hakou_Kbn	
        /// </summary>		
        public String Shiharai_Hakou_Kbn { get; set; }
        /// <summary>
        ///		相殺	:	Sosatu	
        /// </summary>		
        public String Sosatu { get; set; }
        /// <summary>
        ///		発行区分	:	Hakou_Kbn	
        /// </summary>		
        public String Hakou_Kbn { get; set; }
        /// <summary>
        ///		請求分前回残高	:	Seikyu_Zenkai_Zentaka	
        /// </summary>		
        public String Seikyu_Zenkai_Zentaka { get; set; }
        /// <summary>
        ///		請求分今回金額	:	Seikyu_Konkai_Kingaku	
        /// </summary>		
        public String Seikyu_Konkai_Kingaku { get; set; }
        /// <summary>
        ///		請求分今回税額	:	Seikyu_Konkai_Zeigaku	
        /// </summary>		
        public String Seikyu_Konkai_Zeigaku { get; set; }
        /// <summary>
        ///		請求分今回取引	:	Seikyu_Konkai_Rorihiki	
        /// </summary>		
        public String Seikyu_Konkai_Rorihiki { get; set; }
        /// <summary>
        ///		請求分相殺金額	:	Seikyu_Sousatu_Kingaku	
        /// </summary>		
        public String Seikyu_Sousatu_Kingaku { get; set; }
        /// <summary>
        ///		請求分入出金額	:	Seikyu_Nyusyu_Kingaku	
        /// </summary>		
        public String Seikyu_Nyusyu_Kingaku { get; set; }
        /// <summary>
        ///		請求分差引残高	:	Seikyu_Sagaku_Zentaka	
        /// </summary>		
        public String Seikyu_Sagaku_Zentaka { get; set; }
        /// <summary>
        ///		請求分消費税率	:	Seikyu_Syohizei_Ritu	
        /// </summary>		
        public String Seikyu_Syohizei_Ritu { get; set; }
        /// <summary>
        ///		支払分前回残高	:	Shiharai_Zenkai_Zentaka	
        /// </summary>		
        public String Shiharai_Zenkai_Zentaka { get; set; }
        /// <summary>
        ///		支払分今回金額	:	Shiharai_Konkai_Kingaku	
        /// </summary>		
        public String Shiharai_Konkai_Kingaku { get; set; }
        /// <summary>
        ///		支払分今回税額	:	Shiharai_Konkai_Zeigaku	
        /// </summary>		
        public String Shiharai_Konkai_Zeigaku { get; set; }
        /// <summary>
        ///		支払分今回取引	:	Shiharai_Konkai_Rorihiki	
        /// </summary>		
        public String Shiharai_Konkai_Rorihiki { get; set; }
        /// <summary>
        ///		支払分相殺金額	:	Shiharai_Sousatu_Kingaku	
        /// </summary>		
        public String Shiharai_Sousatu_Kingaku { get; set; }
        /// <summary>
        ///		支払分入出金額	:	Shiharai_Nyusyu_Kingaku	
        /// </summary>		
        public String Shiharai_Nyusyu_Kingaku { get; set; }
        /// <summary>
        ///		支払分差引残高	:	Shiharai_Sagaku_Zentaka	
        /// </summary>		
        public String Shiharai_Sagaku_Zentaka { get; set; }
        /// <summary>
        ///		支払分消費税率	:	Shiharai_Syohizei_Ritu	
        /// </summary>		
        public String Shiharai_Syohizei_Ritu { get; set; }
        /// <summary>
        ///		今回金額合計	:	Gokei_Konkai_Kingaku	
        /// </summary>		
        public String Gokei_Konkai_Kingaku { get; set; }
        /// <summary>
        ///		今回税額合計	:	Gokei_Konkai_Zeigaku	
        /// </summary>		
        public String Gokei_Konkai_Zeigaku { get; set; }
        /// <summary>
        ///		今回取引合計	:	Gokei_Konkai_Rorihiki	
        /// </summary>		
        public String Gokei_Konkai_Rorihiki { get; set; }
        /// <summary>
        ///		相殺金額合計	:	Gokei_Sousatu_Kingaku	
        /// </summary>		
        public String Gokei_Sousatu_Kingaku { get; set; }
        /// <summary>
        ///		入出金額合計	:	Gokei_Nyusyu_Kingaku	
        /// </summary>		
        public String Gokei_Nyusyu_Kingaku { get; set; }
        /// <summary>
        ///		差引残高合計	:	Gokei_Sagaku_Zentaka	
        /// </summary>		
        public String Gokei_Sagaku_Zentaka { get; set; }
        /// <summary>
        ///		運賃税区分	:	Untin_Zei_Kbn	
        /// </summary>		
        public String Untin_Zei_Kbn { get; set; }
        /// <summary>
        ///		運賃金額	:	Untin_Kingaku	
        /// </summary>		
        public String Untin_Kingaku { get; set; }
        /// <summary>
        ///		運賃消費税	:	Untin_Syohizei	
        /// </summary>		
        public String Untin_Syohizei { get; set; }
        /// <summary>
        ///		運賃合計	:	Untin_Gokei	
        /// </summary>		
        public String Untin_Gokei { get; set; }
        /// <summary>
        ///		運賃消費税率	:	Untin_Syohizei_Ritu	
        /// </summary>		
        public String Untin_Syohizei_Ritu { get; set; }
        /// <summary>
        ///		領収証	:	Ryousyusyou	
        /// </summary>		
        public String Ryousyusyou { get; set; }
        /// <summary>
        ///		但し書き	:	Tadasi_Kaki	
        /// </summary>		
        public String Tadasi_Kaki { get; set; }
        /// <summary>
        ///		敬称1	:	Keisyou_1	
        /// </summary>		
        public String Keisyou_1 { get; set; }
        /// <summary>
        ///		敬称2	:	Keisyou_2	
        /// </summary>		
        public String Keisyou_2 { get; set; }
        /// <summary>
        ///		キャッシャ連動	:	Kyasya	
        /// </summary>		
        public String Kyasya { get; set; }
        /// <summary>
        ///		請求出力伝票明細リスト	:	Seikyu_Out_Tenpyo_Cnt	
        /// </summary>		
        public List<SyuturyokuMeiseiDTOClass> Seikyu_Out_Tenpyo_Cnt { get; set; }
        /// <summary>
        ///		支払出力伝票明細リスト	:	Shiharai_Out_Tenpyo_Cnt	
        /// </summary>		
        public List<SyuturyokuMeiseiDTOClass> Shiharai_Out_Tenpyo_Cnt { get; set; }
        /// <summary>
        ///		消費税外税	:	Syohizei_Gai	
        /// </summary>		
        public String Syohizei_Gai { get; set; }
        /// <summary>
        ///		消費税内税	:	Syohizei_Nai	
        /// </summary>		
        public String Syohizei_Nai { get; set; }
        /// <summary>
        ///		品名別税区分CD	:	Hinmae_Zei_Kbn	
        /// </summary>		
        public String Hinmae_Zei_Kbn { get; set; }
        /// <summary>
        ///		品名別税消費税外税	:	Hinmae_Zei_Gai	
        /// </summary>		
        public String Hinmae_Zei_Gai { get; set; }
        /// <summary>
        ///		品名別税消費税内税	:	Hinmae_Zei_Nai	
        /// </summary>		
        public String Hinmae_Zei_Nai { get; set; }
        /// <summary>
        ///     滞留区分　：　Tairyuu_Kbn
        /// </summary>
        public Boolean Tairyuu_Kbn { get; set; }
        /// <summary>
        ///		システムID	:	System_Id	
        /// </summary>		
        public String System_Id { get; set; }

        /// <summary>
        /// 売上消費税率
        /// </summary>
        public string Uriage_Shouhizei_Rate { get; set; }

        /// <summary>
        /// 支払消費税率
        /// </summary>
        public string Shiharai_Shouhizei_Rate { get; set; }

        /// <summary>
        /// 計量票発行区分
        /// </summary>
        public string Keiryou_Prirnt_Kbn_Value { get; set; }

        /// <summary>
        /// 仕切書/計量票出力有無
        /// </summary>
        public bool Print_Enable { get; set; }

        /// <summary>
        ///	領収書/仕切書_売上)課税金額	:	R_KAZEI_KINGAKU	
        /// </summary>		
        public String R_KAZEI_KINGAKU { get; set; }
        /// <summary>
        ///	領収書/仕切書_売上)非課税金額	:	R_HIKAZEI_KINGAKU	
        /// </summary>		
        public String R_HIKAZEI_KINGAKU { get; set; }
        /// <summary>
        ///	領収書/仕切書_売上)課税消費税	:	R_KAZEI_SHOUHIZEI	
        /// </summary>		
        public String R_KAZEI_SHOUHIZEI { get; set; }

        /// <summary>
        ///	仕切書_支払)課税金額	:	R_KAZEI_KINGAKU_SHIHARAI
        /// </summary>		
        public String R_KAZEI_KINGAKU_SHIHARAI { get; set; }
        /// <summary>
        ///	仕切書_支払)非課税金額	:	R_HIKAZEI_KINGAKU_SHIHARAI
        /// </summary>		
        public String R_HIKAZEI_KINGAKU_SHIHARAI { get; set; }
        /// <summary>
        ///	仕切書_支払)課税消費税	:	R_KAZEI_SHOUHIZEI_SHIHARAI
        /// </summary>		
        public String R_KAZEI_SHOUHIZEI_SHIHARAI { get; set; }


        #region 月次処理 - 月次ロックチェックで使用

        /// <summary>
        /// 伝票日付
        /// </summary>
        public string DenpyouDate { get; set; }

        /// <summary>
        /// 伝票日付(画面表示時)
        /// </summary>
        public string BeforeDenpyouDate { get; set; }

        #endregion
    }
}
