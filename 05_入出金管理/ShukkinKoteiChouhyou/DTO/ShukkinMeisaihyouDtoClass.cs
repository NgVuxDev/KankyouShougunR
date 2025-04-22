namespace Shougun.Core.ReceiptPayManagement.ShukkinKoteiChouhyou
{
    /// <summary>
    /// 出金明細表出力で使用するDto（主に検索条件として使用）
    /// </summary>
    public class ShukkinMeisaihyouDtoClass
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public ShukkinMeisaihyouDtoClass()
        {
            KyotenCd = 99;
            KyotenName = null;
            DateFrom = null;
            DateTo = null;
            TorihikisakiCdFrom = null;
            TorihikisakiFrom = null;
            TorihikisakiCdTo = null;
            TorihikisakiTo = null;
            Sort1 = 1;
            IsGroupDenpyouNumber = true;
            IsGroupTorihikisaki = true;
            IsGroupNyuushukkinKbn = true;
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
        /// 取引先CDFromを取得・設定します
        /// </summary>
        public string TorihikisakiCdFrom { get; set; }

        /// <summary>
        /// 取引先Fromを取得・設定します
        /// </summary>
        public string TorihikisakiFrom { get; set; }

        /// <summary>
        /// 取引先CDToを取得・設定します
        /// </summary>
        public string TorihikisakiCdTo { get; set; }

        /// <summary>
        /// 取引先Toを取得・設定します
        /// </summary>
        public string TorihikisakiTo { get; set; }

        /// <summary>
        /// 並び順１を取得・設定します
        /// </summary>
        public int Sort1 { get; set; }

        /// <summary>
        /// 伝票番号単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupDenpyouNumber { get; set; }

        /// <summary>
        /// 取引先単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupTorihikisaki { get; set; }

        /// <summary>
        /// 入出金区分単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupNyuushukkinKbn { get; set; }
    }
}
