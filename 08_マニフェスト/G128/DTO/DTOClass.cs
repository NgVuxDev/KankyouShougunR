using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;

namespace Shougun.Core.PaperManifest.Manifestsuiihyo
{
    /// <summary>
    /// パラメータ
    /// </summary>
    public class SerchCheckManiDtoCls
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
}
