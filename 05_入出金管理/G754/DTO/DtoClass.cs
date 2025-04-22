namespace Shougun.Core.ReceiptPayManagement.ShukkinYoteiIchiran
{
    /// <summary>
    /// 出金予定一覧出力で使用するDto（主に検索条件として使用）
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
            ShukkinYoteiDateFrom = null;
            ShukkinYoteiDateTo = null;
            SeisanDateFrom = null;
            SeisanDateTo = null;
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
            IsGroupShukkinYoteiBi = true;
            Shoshiki = 1;
            ShukkinKeshigomuJoukyou = 1;
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
        /// 出金予定日Fromを取得・設定します
        /// </summary>
        public string ShukkinYoteiDateFrom { get; set; }

        /// <summary>
        /// 出金予定日Toを取得・設定します
        /// </summary>
        public string ShukkinYoteiDateTo { get; set; }

        /// <summary>
        /// 精算日Fromを取得・設定します
        /// </summary>
        public string SeisanDateFrom { get; set; }

        /// <summary>
        /// 精算日Toを取得・設定します
        /// </summary>
        public string SeisanDateTo { get; set; }

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
        /// 出金予定日単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupShukkinYoteiBi { get; set; }

        /// <summary>
        /// 精算書書式を取得・設定します
        /// </summary>
        public int Shoshiki { get; set; }

        /// <summary>
        /// 出金消込状況 (1.する 2.しない)
        /// </summary>
        public int ShukkinKeshigomuJoukyou { get; set; }
    }
}
