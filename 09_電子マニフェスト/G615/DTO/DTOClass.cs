// $Id: DTOClass.cs 38457 2014-12-29 07:31:12Z j-kikuchi $
using System;

namespace Shougun.Core.ElectronicManifest.KongouHaikibutsuJoukyouIchiran
{
    /// <summary>
    /// 混合廃棄物状況一覧検索条件DTO
    /// </summary>
    public class findConditionDTO
    {
        #region - Field -
        /// <summary>マニ帳票数量区分</summary>
        public Int16 MANIFEST_REPORT_SUU_KBN { get; set; }
        /// <summary>抽出日付区分</summary>
        public string DATE_KBN { get; set; }
        /// <summary>抽出日付FROM</summary>
        public DateTime DATE_FROM { get; set; }
        /// <summary>抽出日付TO</summary>
        public DateTime DATE_TO { get; set; }
        /// <summary>紐付表示区分</summary>
        public string RELATION_SHOW_KBN { get; set; }
        /// <summary>二次表示区分</summary>
        public string NEXT_SHOW_KBN { get; set; }
        /// <summary>排出事業者CD</summary>
        public string HST_GYOUSHA_CD { get; set; }
        /// <summary>排出事業場CD</summary>
        public string HST_GENBA_CD { get; set; }
        /// <summary>処分受託者CD</summary>
        public string SBN_GYOUSHA_CD { get; set; }
        /// <summary>運搬先の事業場CD</summary>
        public string UPN_SAKI_GENBA_CD { get; set; }
        /// <summary>運搬受託者CD</summary>
        public string UPN_JYUTAKUSHA_CD { get; set; }
        /// <summary>報告書分類CD</summary>
        public string HOUKOKUSHO_BUNRUI_CD { get; set; }
        /// <summary>電子廃棄物種類CD</summary>
        public string DENSHI_HAIKI_SHURUI_CD { get; set; }
        /// <summary>マニフェスト番号FROM</summary>
        public string MANIFEST_ID_FROM { get; set; }
        /// <summary>マニフェスト番号TO</summary>
        public string MANIFEST_ID_TO { get; set; }

        #endregion - Field -
    }
}
