// $Id:

namespace Shougun.Core.SalesPayment.UriageShiharaiKoteiChouhyou
{
    /// <summary>
    /// G568で使用するDto（主に検索条件として使用）
    /// </summary>
    public class UriageShiharaiMeisaihyouDtoClass
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public UriageShiharaiMeisaihyouDtoClass()
        {
            KyotenCd = 99;
            KyotenName = null;
            NyuuryokuTantoushaCd = null;
            NyuuryokuTantousyaName = null;
            DenpyouKbnCd = 1;
            // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) Start
            DenpyouShuruiCd = 5;
            // 20150513 伝種「4.代納」追加(不具合一覧(つ) 23) End
            DateFrom = null;
            DateTo = null;
            ShimeJoukyouCd = 3;
            KakuteiKbnCd = 3;
            TorihikiKbnCd = 3;
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
            Order = 1;
            IsGroupTorihikisaki = true;
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
        public string NyuuryokuTantousyaName { get; set; }

        /// <summary>
        /// 伝票区分CDを取得・設定します
        /// </summary>
        public int DenpyouKbnCd { get; set; }

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
        /// 締め状況CDを取得・設定します
        /// </summary>
        public int ShimeJoukyouCd { get; set; }

        /// <summary>
        /// 確定区分CDを取得・設定します
        /// </summary>
        public int KakuteiKbnCd { get; set; }

        /// <summary>
        /// 取引区分CDを取得・設定します
        /// </summary>
        public int TorihikiKbnCd { get; set; }

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
        /// 並び順を取得・設定します
        /// </summary>
        public int Order { get; set; }

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
    }
}
