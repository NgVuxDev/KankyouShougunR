// $Id:

namespace Shougun.Core.Carriage.UnchinMeisaihyouDto
{
    /// <summary>
    /// G642で使用するDto（主に検索条件として使用）
    /// </summary>
    public class DtoClass
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public DtoClass()
        {
            KyotenCd = 99;
            KyotenName = null;
            KeitaiKbnCd = null;
            KeitaiKbnName = null;
            DenshuKbn = 6;
            DateFrom = null;
            DateTo = null;
            UnpanGyoushaCdFrom = null;
            UnpanGyoushaCdTo = null;
            UnpanGyoushaFrom = null;
            UnpanGyoushaTo = null;
            Order = 1;
            IsGroupUnpanGyousha = true;
            IsGroupDenpyouNumber = true;
        }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public int KyotenCd { get; set; }

        /// <summary>
        /// 拠点名称を取得・設定します
        /// </summary>
        public string KyotenName { get; set; }

        /// <summary>
        /// 形態区分CDを取得・設定します
        /// </summary>
        public string KeitaiKbnCd { get; set; }

        /// <summary>
        /// 形態区分名称を取得・設定します
        /// </summary>
        public string KeitaiKbnName { get; set; }

        /// <summary>
        /// 伝種区分を取得・設定します
        /// </summary>
        public int DenshuKbn { get; set; }

        /// <summary>
        /// 日付種類CDを取得・設定します
        /// </summary>
        public int DateShuruiCd { get; set; }

        /// <summary>
        /// 日付Fromを取得・設定します
        /// </summary>
        public string DateFrom { get; set; }

        /// <summary>
        /// 日付Toを取得・設定します
        /// </summary>
        public string DateTo { get; set; }

        /// <summary>
        /// 運搬業者CDFromを取得・設定します
        /// </summary>
        public string UnpanGyoushaCdFrom { get; set; }

        /// <summary>
        /// 運搬業者Fromを取得・設定します
        /// </summary>
        public string UnpanGyoushaFrom { get; set; }

        /// <summary>
        /// 運搬業者CDToを取得・設定します
        /// </summary>
        public string UnpanGyoushaCdTo { get; set; }

        /// <summary>
        /// 運搬業者Toを取得・設定します
        /// </summary>
        public string UnpanGyoushaTo { get; set; }

        /// <summary>
        /// 並び順を取得・設定します
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 運搬業者単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupUnpanGyousha { get; set; }

        /// <summary>
        /// 伝票日付単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupDenpyouDate { get; set; }

        /// <summary>
        /// 伝票番号単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupDenpyouNumber { get; set; }
    }
}
