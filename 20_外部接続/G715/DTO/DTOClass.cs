
namespace Shougun.Core.ExternalConnection.DenshiKeiyakuNyuryoku
{
    /// <summary>
    /// 電子契約入力で使用する委託契約情報
    /// </summary>
    public class DenshiKeiyakuItakuDataDTO
    {
        /// <summary>委託契約ファイルパス</summary>
        public string ITAKU_KEIYAKU_FILE_PATH { get; set; }

        /// <summary>排出事業者CD</summary>
        public string HAISHUTSU_JIGYOUSHA_CD { get; set; }

        /// <summary>排出事業場CD</summary>
        public string HAISHUTSU_JIGYOUJOU_CD { get; set; }

        /// <summary>個別指定チェック</summary>
        public bool KOBETSU_SHITEI_CHECK { get; set; }

        /// <summary>備考１</summary>
        public string BIKOU1 { get; set; }

        /// <summary>備考２</summary>
        public string BIKOU2 { get; set; }

        /// <summary>委託契約タイプ</summary>
        public string ITAKU_KEIYAKU_TYPE { get; set; }

        /// <summary>委託契約種類</summary>
        public string ITAKU_KEIYAKU_SHURUI { get; set; }

        /// <summary>契約日</summary>
        public string KEIYAKUSHO_KEIYAKU_DATE { get; set; }

        /// <summary>作成日</summary>
        public string KEIYAKUSHO_CREATE_DATE { get; set; }

        /// <summary>送付日</summary>
        public string KEIYAKUSHO_SEND_DATE { get; set; }

        /// <summary>委託契約番号</summary>
        public string ITAKU_KEIYAKU_NO { get; set; }

        /// <summary>更新種別</summary>
        public string KOUSHIN_SHUBETSU { get; set; }

        /// <summary>有効期限開始</summary>
        public string YUUKOU_BEGIN { get; set; }

        /// <summary>有効期限終了</summary>
        public string YUUKOU_END { get; set; }

        /// <summary>更新終了日</summary>
        public string KOUSHIN_END_DATE { get; set; }

        /// <summary>排出事業場メモ</summary>
        public string HST_FREE_COMMENT { get; set; }

        /// <summary>処分許可区分</summary>
        public string SBN_KYOKA_KBN { get; set; }

        /// <summary>運搬許可区分</summary>
        public string UPN_KYOKA_KBN { get; set; }

        /// <summary>処分業者CD</summary>
        public string SBN_GYOUSHA_CD { get; set; }

        /// <summary>処分業者名</summary>
        public string SBN_GYOUSHA_NAME { get; set; }

        /// <summary>処分現場CD</summary>
        public string SBN_GENBA_CD { get; set; }

        /// <summary>処分現場名</summary>
        public string SBN_GENBA_NAME { get; set; }

        /// <summary>処分地域CD</summary>
        public string SBN_CHIIKI_CD { get; set; }

        /// <summary>運搬業者CD</summary>
        public string UPN_GYOUSHA_CD { get; set; }

        /// <summary>運搬業者名</summary>
        public string UPN_GYOUSHA_NAME { get; set; }

        /// <summary>運搬現場CD</summary>
        public string UPN_GENBA_CD { get; set; }

        /// <summary>運搬現場名</summary>
        public string UPN_GENBA_NAME { get; set; }

        /// <summary>運搬地域CD</summary>
        public string UPN_CHIIKI_CD { get; set; }

    }

    /// <summary>
    /// 電子契約入力の契約情報タブで使用する情報
    /// </summary>
    public class KeiyakuInfoDataDTO
    {
        /// <summary>委託契約基本の業者名</summary>
        public string KIHON_GYOUSHA_NAME_RYAKU { get; set; }

        /// <summary>委託契約基本の現場名</summary>
        public string KIHON_GENBA_NAME_RYAKU { get; set; }

        /// <summary>委託契約基本排出現場の業者CD</summary>
        public string HTS_HAISHUTSU_JIGYOUSHA_CD { get; set; }

        /// <summary>委託契約基本排出現場の現場CD</summary>
        public string HTS_HAISHUTSU_JIGYOUJOU_CD { get; set; }

        /// <summary>委託契約基本排出現場の現場名</summary>
        public string HTS_HAISHUTSU_JIGYOUJOU_NAME { get; set; }

        /// <summary>委託契約基本排出現場の現場住所１</summary>
        public string HTS_HAISHUTSU_JIGYOUJOU_ADDRESS1 { get; set; }

        /// <summary>委託契約基本排出現場の現場住所２</summary>
        public string HTS_HAISHUTSU_JIGYOUJOU_ADDRESS2 { get; set; }

        /// <summary>委託契約基本排出現場の現場都道府県名</summary>
        public string HTS_TODOUFUKEN_NAME { get; set; }

    }

    /// <summary>
    /// 許可番号データ検索用のDTO
    /// </summary>
    public class KyokaNoSearchDataDTO
    {
        /// <summary>業者CD</summary>
        public string GYOUSHA_CD { get; set; }

        /// <summary>現場CD</summary>
        public string GENBA_CD { get; set; }

        /// <summary>地域CD</summary>
        public string CHIIKI_CD { get; set; }
    }
}
