// $Id:

namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou
{
    /// <summary>
    /// G569で使用するDto（主に検索条件として使用）
    /// </summary>
    internal class UkeireShukkaMeisaihyouDtoClass
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        internal UkeireShukkaMeisaihyouDtoClass()
        {
            KyotenCd = 99;
            KyotenName = null;
            NyuuryokuTantoushaCd = null;
            NyuuryokuTantoushaName = null;
            DenpyouShuruiCd = 1;
            DateFrom = null;
            DateTo = null;
            NyuushukkaKbn = null;
            GyoushaCdFrom = null;
            GyoushaCdTo = null;
            GyoushaFrom = null;
            GyoushaTo = null;
            GenbaCdFrom = null;
            GenbaCdTo = null;
            GenbaFrom = null;
            GenbaTo = null;
            UnpanGyoushaCdFrom = null;
            UnpanGyoushaCdTo = null;
            UnpanGyoushaFrom = null;
            UnpanGyoushaTo = null;
            KeitaiKbnFrom = null;
            KeitaiFrom = null;
            KeitaiKbnTo = null;
            KeitaiTo = null;
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
            Order = 1;
            IsGroupGyousha = true;
            IsGroupGenba = true;
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
        /// 入力担当者CDを取得・設定します
        /// </summary>
        public string NyuuryokuTantoushaCd { get; set; }

        /// <summary>
        /// 入力担当者名称を取得・設定します
        /// </summary>
        public string NyuuryokuTantoushaName { get; set; }

        /// <summary>
        /// 伝票種類CDを取得・設定します
        /// </summary>
        public int DenpyouShuruiCd { get; set; }

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
        /// 入出荷区分を取得・設定します
        /// </summary>
        public string NyuushukkaKbn { get; set; }

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
        /// 形態区分Fromを取得・設定します
        /// </summary>
        public string KeitaiKbnFrom { get; set; }

        /// <summary>
        /// 形態Fromを取得・設定します
        /// </summary>
        public string KeitaiFrom { get; set; }

        /// <summary>
        /// 形態区分Toを取得・設定します
        /// </summary>
        public string KeitaiKbnTo { get; set; }

        /// <summary>
        /// 形態Toを取得・設定します
        /// </summary>
        public string KeitaiTo { get; set; }

        /// <summary>
        /// 品名CdFromを取得・設定します
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
        /// 種類CdFromを取得・設定します
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
        /// 並び順を取得・設定します
        /// </summary>
        public int Order { get; set; }

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
    }
}
