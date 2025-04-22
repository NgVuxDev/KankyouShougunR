using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.ManifestsuiihyoIchiran
{
    public class DTOClass
    {
        // 検索条件
        /// <summary> 検索条件  :一次二次区分</summary>
        /// <remarks> 1.一次</remarks>
        /// <remarks> 2.二次</remarks>
        public string FIRST_MANIFEST_KBN { get; set; }

        /// <summary> 検索条件  :範囲開始日</summary>
        public string DATE_START { get; set; }

        /// <summary> 検索条件  :範囲終了日</summary>
        public string DATE_END { get; set; }

        /// <summary> 検索条件  :拠点</summary>
        public string KYOTEN_CD { get; set; }

        /// <summary> 検索条件  :排出事業者FROM</summary>
        public string HST_GYOUSHA_CD_START { get; set; }

        /// <summary> 検索条件  :排出事業者TO</summary>
        public string HST_GYOUSHA_CD_END { get; set; }

        /// <summary> 検索条件  :排出事業場FROM</summary>
        public string HST_GENBA_CD_START { get; set; }

        /// <summary> 検索条件  :排出事業場FROM</summary>
        public string HST_GENBA_CD_END { get; set; }

        /// <summary> 検索条件  :運搬受託者FROM</summary>
        public string HST_UPN_GYOUSHA_CD_START { get; set; }

        /// <summary> 検索条件  :運搬受託者TO</summary>
        public string HST_UPN_GYOUSHA_CD_END { get; set; }

        /// <summary> 検索条件  :処分受託者FROM</summary>
        public string HST_UPN_SAKI_GYOUSHA_CD_START { get; set; }

        /// <summary> 検索条件  :処分受託者TO</summary>
        public string HST_UPN_SAKI_GYOUSHA_CD_END { get; set; }

        /// <summary> 検索条件  :処分事業場FROM</summary>
        public string HST_LAST_SBN_GENBA_CD_START { get; set; }

        /// <summary> 検索条件  :処分事業場TO</summary>
        public string HST_LAST_SBN_GENBA_CD_END { get; set; }

        /// <summary> 検索条件  :直行_廃棄物種類</summary>
        public string HST_HAIKI_SHURUI_CD1 { get; set; }

        /// <summary> 検索条件  :積替_廃棄物種類</summary>
        public string HST_HAIKI_SHURUI_CD2 { get; set; }

        /// <summary> 検索条件  :建廃_廃棄物種類</summary>
        public string HST_HAIKI_SHURUI_CD3 { get; set; }

        /// <summary> 検索条件  :電子_廃棄物種類</summary>
        public string HAIKIBUTU_DENSHI { get; set; }

        /// <summary> 検索条件  :出力内容 </summary>
        /// <remarks> 1.排出事業者別</remarks>
        /// <remarks> 2.運搬受託者別</remarks>
        /// <remarks> 3.処分受託者別</remarks>
        /// <remarks> 4.最終処分場別</remarks>
        /// <remarks> 5.廃棄物種類別</remarks>
        public string SHUTURYOKU_NAIYOU { get; set; }

        /// <summary> 検索条件  :出力区分</summary>
        /// <remarks> 1.合算</remarks>
        /// <remarks> 2.紙のみ</remarks>
        /// <remarks> 3.電子のみ</remarks>
        public string SHUTURYOKU_KUBUN { get; set; }
    }

    /// <summary>
    /// 検索条件
    /// </summary>
    public class JoukenParam
    {
        public string ichijinijiKbn { get; set; }           // 一時二次区分
        public string nengappiFrom { get; set; }            // 年月日開始
        public string nengappiTo { get; set; }              // 年月日終了
        public DateTime dateFrom { get; set; }              // 年月日開始
        public DateTime dateTo { get; set; }                // 年月日終了
        public long monthsBetween { get; set; }             //  
        public string kyoten { get; set; }                  // 拠点CD
        public string haiJigyouShaFrom { get; set; }        // 排出事業者CD開始
        public string haiJigyouShaTo { get; set; }          // 排出事業者CD終了
        public string haiJigyouBaFrom { get; set; }         // 排出事業場CD開始
        public string haiJigyouBaTo { get; set; }           // 排出事業場CD終了
        public string unpanJutakuShaFrom { get; set; }      // 運搬受託者CD開始
        public string unpanJutakuShaTo { get; set; }        // 運搬受託者CD終了
        public string shobunJutakuShaFrom { get; set; }     // 処分受託者CD開始
        public string shobunJutakuShaTo { get; set; }       // 処分受託者CD終了
        public string saisyuuShobunBashoFrom { get; set; }  // 最終処分場所CD開始
        public string saisyuuShobunBashoTo { get; set; }    // 最終処分場所CD終了
        public string chokkouHaikibutuSyurui { get; set; }  // 産廃（直行）廃棄物種類CD
        public string tsumikaeHaikibutuSyurui { get; set; } // 産廃（積替）廃棄物種類CD
        public string kenpaiHaikibutuSyurui { get; set; }   // 建廃廃棄物種類CD
        public string denshiHaikibutuSyurui { get; set; }   // 電子廃棄物種類CD
        public string syuturyokuNaiyoiu { get; set; }       // 出力内容
        public string syuturyokuKubun { get; set; }         // 出力区分
    }
}
