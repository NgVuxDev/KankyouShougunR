namespace Shougun.Core.Scale.KeiryouHoukoku.DTO
{
    /// <summary>
    /// Dto
    /// </summary>
    public class DTOCls
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public DTOCls()
        {
            KyotenCd = 99;
            KyotenName = null;
            HoukokuShurui = 1;
            KeiryouKbn = 1;
            DenpyouKbnCd = 1;
            DateFrom = null;
            DateTo = null;
            TorihikisakiCdFrom = null;
            TorihikisakiCdTo = null;
            TorihikisakiFrom = null;
            TorihikisakiTo = null;
            GyoushaCdFrom = null;
            GyoushaCdTo = null;
            GyoushaFrom = null;
            GyoushaTo = null;
            GenbaCdFrom = null;
            GenbaCdTo = null;
            GenbaFrom = null;
            GenbaTo = null;
            UpnGyoushaCdFrom = null;
            UpnGyoushaFrom = null;
            UpnGyoushaCdTo = null;
            UpnGyoushaTo = null;
            HinmeiCdFrom = null;
            HinmeiFrom = null;
            HinmeiCdTo = null;
            HinmeiTo = null;
            ShuruiCdFrom = null;
            ShuruiFrom = null;
            ShuruiCdTo = null;
            ShuruiTo = null;
            BunruiCdFrom = null;
            BunruiFrom = null;
            BunruiCdTo = null;
            BunruiTo = null;
            KeitaiKbnCdFrom = null;
            KeitaiKbnFrom = null;
            KeitaiKbnCdTo = null;
            KeitaiKbnTo = null;
            IsGroupTorihikisaki = true;
            IsGroupGyousha = true;
            IsGroupGenba = true;
            IsGroupDenpyouNumber = true;
        }
        /// <summary>
        /// 報告種類を取得・設定します
        /// </summary>
        public int HoukokuShurui { get; set; }

        /// <summary>
        /// 集計単位を取得・設定します
        /// </summary>
        public int GroupTani { get; set; }

        /// <summary>
        /// 取引先単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupTorihikisaki { get; set; }

        /// <summary>
        /// 業者単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupGyousha { get; set; }

        /// <summary>
        /// 現場単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupGenba { get; set; }

        /// <summary>
        /// 伝票番号単位で集計するかを取得・設定します
        /// </summary>
        public bool IsGroupDenpyouNumber { get; set; }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public int KyotenCd { get; set; }

        /// <summary>
        /// 拠点名称を取得・設定します
        /// </summary>
        public string KyotenName { get; set; }

        /// <summary>
        /// 伝票区分CDを取得・設定します
        /// </summary>
        public int DenpyouKbnCd { get; set; }

        /// <summary>
        /// 計量区分CDを取得・設定します
        /// </summary>
        public int KeiryouKbn { get; set; }

        /// <summary>
        /// 日付種類CDを取得・設定します
        /// </summary>
        public int DateShuruiCd { get; set; }

        /// <summary>
        /// 日付範囲を取得・設定します
        /// </summary>
        public int DateHani { get; set; }

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
        /// 業者CDFromを取得・設定します
        /// </summary>
        public string GyoushaCdFrom { get; set; }

        /// <summary>
        /// 業者Fromを取得・設定します
        /// </summary>
        public string GyoushaFrom { get; set; }

        /// <summary>
        /// 業者CDToを取得・設定します
        /// </summary>
        public string GyoushaCdTo { get; set; }

        /// <summary>
        /// 業者Toを取得・設定します
        /// </summary>
        public string GyoushaTo { get; set; }

        /// <summary>
        /// 現場CDFromを取得・設定します
        /// </summary>
        public string GenbaCdFrom { get; set; }

        /// <summary>
        /// 現場Fromを取得・設定します
        /// </summary>
        public string GenbaFrom { get; set; }

        /// <summary>
        /// 現場CDToを取得・設定します
        /// </summary>
        public string GenbaCdTo { get; set; }

        /// <summary>
        /// 現場Toを取得・設定します
        /// </summary>
        public string GenbaTo { get; set; }

        /// <summary>
        /// 運搬業者CDFromを取得・設定します
        /// </summary>
        public string UpnGyoushaCdFrom { get; set; }

        /// <summary>
        /// 運搬業者Fromを取得・設定します
        /// </summary>
        public string UpnGyoushaFrom { get; set; }

        /// <summary>
        /// 運搬業者CDToを取得・設定します
        /// </summary>
        public string UpnGyoushaCdTo { get; set; }

        /// <summary>
        /// 運搬業者Toを取得・設定します
        /// </summary>
        public string UpnGyoushaTo { get; set; }

        /// <summary>
        /// 品名CDFromを取得・設定します
        /// </summary>
        public string HinmeiCdFrom { get; set; }

        /// <summary>
        /// 品名Fromを取得・設定します
        /// </summary>
        public string HinmeiFrom { get; set; }

        /// <summary>
        /// 品名CDToを取得・設定します
        /// </summary>
        public string HinmeiCdTo { get; set; }

        /// <summary>
        /// 品名Toを取得・設定します
        /// </summary>
        public string HinmeiTo { get; set; }

        /// <summary>
        /// 種類CDFromを取得・設定します
        /// </summary>
        public string ShuruiCdFrom { get; set; }

        /// <summary>
        /// 種類Fromを取得・設定します
        /// </summary>
        public string ShuruiFrom { get; set; }

        /// <summary>
        /// 種類CDToを取得・設定します
        /// </summary>
        public string ShuruiCdTo { get; set; }

        /// <summary>
        /// 種類Toを取得・設定します
        /// </summary>
        public string ShuruiTo { get; set; }

        /// <summary>
        /// 分類CDFromを取得・設定します
        /// </summary>
        public string BunruiCdFrom { get; set; }

        /// <summary>
        /// 分類Fromを取得・設定します
        /// </summary>
        public string BunruiFrom { get; set; }

        /// <summary>
        /// 分類CDToを取得・設定します
        /// </summary>
        public string BunruiCdTo { get; set; }

        /// <summary>
        /// 分類Toを取得・設定します
        /// </summary>
        public string BunruiTo { get; set; }

        /// <summary>
        /// 形態区分CDFromを取得・設定します
        /// </summary>
        public string KeitaiKbnCdFrom { get; set; }

        /// <summary>
        /// 形態区分Fromを取得・設定します
        /// </summary>
        public string KeitaiKbnFrom { get; set; }

        /// <summary>
        /// 形態区分CDToを取得・設定します
        /// </summary>
        public string KeitaiKbnCdTo { get; set; }

        /// <summary>
        /// 形態区分Toを取得・設定します
        /// </summary>
        public string KeitaiKbnTo { get; set; }

    }
}