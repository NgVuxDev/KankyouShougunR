// $Id: ConstCls.cs 22686 2014-06-09 08:46:13Z nagata $
namespace Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.Const
{
    class ConstCls
    {
        //拠点CD（全社）
        public const string KyouTenZenSya = "99";

        /// <summary>伝票日付Code</summary>
        public const string HidukeCD_DenPyou = "1";
        /// <summary>伝票日付Name</summary>
        public const string HidukeName_DenPyou = "作業日※";

        /// <summary>入力日付Code</summary>
        public const string HidukeCD_NyuuRyoku = "2";
        /// <summary>入力日付Name</summary>
        public const string HidukeName_NyuuRyoku = "入力日付※";

        /// <summary>アラート件数の最小値</summary>
        public const long AlertNumber_Min = 1;
        /// <summary>アラート件数の最大値</summary>
        public const long AlertNumber_Max = 99999;

        /// <summary>明細部の非表示列（システムID）</summary>
        public const string HIDDEN_COLUMN_SYSTEM_ID = "SYSTEM_ID_HIDDEN";
        /// <summary>明細部の非表示列（枝番）</summary>
        public const string HIDDEN_COLUMN_SEQ = "SEQ_HIDDEN";
        /// <summary>明細部の非表示列（定期実績番号）</summary>
        public const string HIDDEN_COLUMN_JISSEKI_NUMBER = "TEIKI_JISSEKI_NUMBER_HIDDEN";
        /// <summary>明細部の非表示列（作業日）</summary>
        public const string HIDDEN_COLUMN_SAGYOU_DATE = "SAGYOU_DATE_HIDDEN";
        /// <summary>明細部の非表示列（明細システムID）</summary>
        public const string HIDDEN_COLUMN_DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID_HIDDEN";

        /// <summary>明細部のTextAlign変更対象列（伝票区分CD）</summary>
        public const string ALIGN_COLUMN_DENPYOU_KBN_CD = "伝票区分CD";
    }
}
