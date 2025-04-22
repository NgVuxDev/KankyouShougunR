namespace Shougun.Core.ReceiptPayManagement.NyuukinYoteiIchiran
{
    /// <summary>
    /// 入金予定一覧出力で使用するDto（主に検索条件として使用）
    /// </summary>
    public class NyuukinYoteiIchiranDtoClass
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public NyuukinYoteiIchiranDtoClass()
        {
            KyotenCd = 99;
            KyotenName = null;
            NyuukinYoteiDateFrom = null;
            NyuukinYoteiDateTo = null;
            SeikyuuDateFrom = null;
            SeikyuuDateTo = null;
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
            IsGroupNyuukinYoteiBi = true;
            Shoshiki = 1;
            NyuukinKeshigomuJoukyou = 1;
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
        /// 入金予定日Fromを取得・設定します
        /// </summary>
        public string NyuukinYoteiDateFrom { get; set; }

        /// <summary>
        /// 入金予定日Toを取得・設定します
        /// </summary>
        public string NyuukinYoteiDateTo { get; set; }

        /// <summary>
        /// 請求日Fromを取得・設定します
        /// </summary>
        public string SeikyuuDateFrom { get; set; }

        /// <summary>
        /// 請求日Toを取得・設定します
        /// </summary>
        public string SeikyuuDateTo { get; set; }

        /// <summary>
        /// 営業者CDFromを取得・設定します
        /// </summary>
        public string EigyoushaCdFrom { get; set; }

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

        /// <summary>
        /// 入金予定日単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupNyuukinYoteiBi { get; set; }

        /// <summary>
        /// 請求書書式を取得・設定します
        /// </summary>
        public int Shoshiki { get; set; }

        /// <summary>
        /// 入金消込状況 (1.する 2.しない)
        /// </summary>
        public int NyuukinKeshigomuJoukyou { get; set; }
    }
}
