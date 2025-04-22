namespace Shougun.Core.ReceiptPayManagement.MinyuukinIchiran
{
    /// <summary>
    /// 入金明細表出力で使用するDto（主に検索条件として使用）
    /// </summary>
    public class MinyuukinIchiranDtoClass
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public MinyuukinIchiranDtoClass()
        {
            KyotenCd = 99;
            KyotenName = null;
            EigyoushaCdFrom = null;
            EigyoushaFrom = null;
            EigyoushaCdTo = null;
            EigyoushaTo = null;
            TorihikisakiCdFrom = null;
            TorihikisakiFrom = null;
            TorihikisakiCdTo = null;
            TorihikisakiTo = null;
            Sort1 = 1;
            Sort2 = 1;
            IsGroupEigyousha = true;
            IsGroupTorihikisaki = true;
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
        /// 営業者CDFromを取得・設定します
        /// </summary>
        public string EigyoushaCdFrom { get; set; }

        /// <summary>
        /// 請求書書式を取得・設定します
        /// </summary>
        public int Syosiki { get; set; }

        /// <summary>
        /// 営業者Fromを取得・設定します
        /// </summary>
        public string EigyoushaFrom { get; set; }

        /// <summary>
        /// 営業者CDToを取得・設定します
        /// </summary>
        public string EigyoushaCdTo { get; set; }

        /// <summary>
        /// 営業者Toを取得・設定します
        /// </summary>
        public string EigyoushaTo { get; set; }

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
        /// 並び順２を取得・設定します
        /// </summary>
        public int Sort2 { get; set; }

        /// <summary>
        /// 営業者単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupEigyousha { get; set; }

        /// <summary>
        /// 取引先単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupTorihikisaki { get; set; }
    }
}
