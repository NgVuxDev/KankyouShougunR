using System.Data;
namespace Shougun.Core.Reception.UketsukeMeisaihyo
{
    /// <summary>
    /// 受付明細表 出力用DTO
    /// </summary>
    public class PrintDtoClass
    {
        /// <summary>会社名</summary>
        public string CORP_NAME_RYAKU { set; get; }
        /// <summary>拠点名</summary>
        public string KYOTEN_NAME_RYAKU { set; get; }
        /// <summary>発行年月日</summary>
        public string PRINT_DATE { set; get; }

        /// <summary>抽出条件 伝票種類</summary>
        public string SEARCH_DENPYOU_KBN { set; get; }
        /// <summary>抽出条件 日付</summary>
        public string SEARCH_DATE_LABEL { set; get; }
        /// <summary>抽出条件 日付範囲</summary>
        public string SEARCH_DATE { set; get; }
        /// <summary>抽出条件 配車状況</summary>
        public string SEARCH_HAISHA_JOUKYOU { set; get; }
        /// <summary>抽出条件 取引先</summary>
        public string SEARCH_TORIHIKISAKI { set; get; }
        /// <summary>抽出条件 業者</summary>
        public string SEARCH_GYOUSHA { set; get; }
        /// <summary>抽出条件 現場</summary>
        public string SEARCH_GENBA { set; get; }
        /// <summary>抽出条件 運搬業者</summary>
        public string SEARCH_UNPAN_GYOUSHA { set; get; }
        /// <summary>抽出条件 車種</summary>
        public string SEARCH_SHASHU { set; get; }
        /// <summary>抽出条件 車輌</summary>
        public string SEARCH_SHARYO { set; get; }
        /// <summary>抽出条件 運転者</summary>
        public string SEARCH_UNTENSHA { set; get; }

        /// <summary>Detail出力用DataTable</summary>
        public DataTable DETAIL_DATA_TABLE { set; get; }

        /// <summary>並び順(1.取引先CD順 2.業者CD順 3.運搬業者CD順 4.運転者CD順 5.作業日順 6.受付番号順)</summary>
        public string ORDER { set; get; }

        /// <summary>売上金額合計</summary>
        public string URIAGE_TOTAL_KINGAKU { set; get; }

        /// <summary>支払金額合計</summary>
        public string SHIHARAI_TOTAL_KINGAKU { set; get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PrintDtoClass()
        {
            // Nothing
        }
    }

    /// <summary>
    /// 受付明細表 帳票データ検索用DTO
    /// </summary>
    public class SearchDtoClass
    {
        /// <summary>伝票種類 収集</summary>
        public bool DENPYOU_SHURUI_SHUSHU { set; get; }
        /// <summary>伝票種類 出荷</summary>
        public bool DENPYOU_SHURUI_SHUKKA { set; get; }
        /// <summary>伝票種類 持込</summary>
        public bool DENPYOU_SHURUI_MOCHIKOMI { set; get; }
        /// <summary>拠点CD</summary>
        public int KYOTEN_CD { set; get; }
        /// <summary>日付(1.受付日 2.作業日 3.入力日付)</summary>
        public int HIDUKE { set; get; }
        /// <summary>配車状況 受注</summary>
        public bool HAISHA_JOUKYOU_JUCHU { set; get; }
        /// <summary>配車状況 配車</summary>
        public bool HAISHA_JOUKYOU_HAISHA { set; get; }
        /// <summary>配車状況 計上</summary>
        public bool HAISHA_JOUKYOU_KEIJO { set; get; }
        /// <summary>配車状況 キャンセル</summary>
        public bool HAISHA_JOUKYOU_CANCEL { set; get; }
        /// <summary>配車状況 回収なし</summary>
        public bool HAISHA_JOUKYOU_KAISHUNASHI { set; get; }
        /// <summary>日付範囲From</summary>
        public string HIDUKE_RANGE_FROM { set; get; }
        /// <summary>日付範囲To</summary>
        public string HIDUKE_RANGE_TO { set; get; }
        /// <summary>取引先CDFrom</summary>
        public string TORIHIKISAKI_CD_FROM { set; get; }
        /// <summary>取引先CDTo</summary>
        public string TORIHIKISAKI_CD_TO { set; get; }
        /// <summary>業者CDFrom</summary>
        public string GYOUSHA_CD_FROM { set; get; }
        /// <summary>業者CDTo</summary>
        public string GYOUSHA_CD_TO { set; get; }
        /// <summary>現場CDFrom</summary>
        public string GENBA_CD_FROM { set; get; }
        /// <summary>現場CDTo</summary>
        public string GENBA_CD_TO { set; get; }
        /// <summary>運搬業者CDFrom</summary>
        public string UNPAN_GYOUSHA_CD_FROM { set; get; }
        /// <summary>運搬業者CDTo</summary>
        public string UNPAN_GYOUSHA_CD_TO { set; get; }
        /// <summary>車種CDFrom</summary>
        public string SHASHU_CD_FROM { set; get; }
        /// <summary>車種CDTo</summary>
        public string SHASHU_CD_TO { set; get; }
        /// <summary>車輌CDFrom</summary>
        public string SHARYOU_CD_FROM { set; get; }
        /// <summary>車輌CDTo</summary>
        public string SHARYOU_CD_TO { set; get; }
        /// <summary>運転者CDFrom</summary>
        public string UNTENSHA_CD_FROM { set; get; }
        /// <summary>運転者CDTo</summary>
        public string UNTENSHA_CD_TO { set; get; }
        /// <summary>OrderBy句(並び順)</summary>
        public int ORDER { set; get; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SearchDtoClass()
        {
            DENPYOU_SHURUI_SHUSHU = false;
            DENPYOU_SHURUI_SHUKKA = false;
            DENPYOU_SHURUI_MOCHIKOMI = false;
            HIDUKE = 1;
            ORDER = 1;
        }
    }
}
