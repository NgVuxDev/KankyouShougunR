// $Id: CasherDTOClass.cs 42061 2015-02-10 10:10:07Z j-kikuchi $
using System;

namespace Shougun.Core.Common.BusinessCommon
{
    /// <summary>
    /// キャッシャ連携DTO
    /// </summary>
    public class CasherDTOClass
    {
        #region - Field -
        /// <summary>伝票日付</summary>
        public DateTime DENPYOU_DATE { get; set; }
        /// <summary>入力担当者CD</summary>
        public string NYUURYOKU_TANTOUSHA_CD { get; set; }
        /// <summary>伝票番号</summary>
        public Int64 DENPYOU_NUMBER { get; set; }
        /// <summary>金額</summary>
        public decimal KINGAKU { get; set; }
        /// <summary>備考</summary>
        public string BIKOU { get; set; }
        /// <summary>伝種区分CD</summary>
        public Int16 DENSHU_KBN_CD { get; set; }
        /// <summary>拠点CD</summary>
        public Int16 KYOTEN_CD { get; set; }

        #endregion - Field -
    }
}
