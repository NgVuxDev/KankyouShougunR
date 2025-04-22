using r_framework.Logic;
using r_framework.Const;
namespace Shougun.Core.SalesPayment.SyukkaNyuuryoku2
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class Const
    {
        /// <summary>ROW_NO</summary>
        internal const string CELL_NAME_ROW_NO = "ROW_NO";

        /// <summary>品名CD</summary>
        internal const string CELL_NAME_HINMEI_CD = "HINMEI_CD";

        /// <summary>品名</summary>
        internal const string CELL_NAME_HINMEI_NAME = "HINMEI_NAME";

        /// <summary>正味重量</summary>
        internal const string CELL_NAME_NET_JYUURYOU = "NET_JYUURYOU";

        /// <summary>伝票区分CD</summary>
        internal const string CELL_NAME_DENPYOU_KBN_CD = "DENPYOU_KBN_CD";

        /// <summary>伝票区分名</summary>
        internal const string CELL_NAME_DENPYOU_KBN_NAME = "DENPYOU_KBN_NAME";

        /// <summary>金額</summary>
        internal const string CELL_NAME_KINGAKU = "KINGAKU";

        /// <summary>調整重量</summary>
        internal const string CELL_NAME_CHOUSEI_JYUURYOU = "CHOUSEI_JYUURYOU";

        /// <summary>調整(%)</summary>
        internal const string CELL_NAME_CHOUSEI_PERCENT = "CHOUSEI_PERCENT";

        /// <summary>容器kg</summary>
        internal const string CELL_NAME_YOUKI_JYUURYOU = "YOUKI_JYUURYOU";

        /// <summary>割振重量</summary>
        internal const string CELL_NAME_WARIFURI_JYUURYOU = "WARIFURI_JYUURYOU";

        /// <summary>割振(%)</summary>
        internal const string CELL_NAME_WARIFURI_PERCENT = "WARIFURI_PERCENT";

        /// <summary>総重量</summary>
        internal const string CELL_NAME_STAK_JYUURYOU = "STACK_JYUURYOU";

        /// <summary>空車重量</summary>
        internal const string CELL_NAME_EMPTY_JYUURYOU = "EMPTY_JYUURYOU";

        /// <summary>計量時間</summary>
        internal const string CELL_NAME_KEIRYOU_TIME = "KEIRYOU_TIME";

        /// <summary>容器数量</summary>
        internal const string CELL_NAME_YOUKI_SUURYOU = "YOUKI_SUURYOU";

        /// <summary>容器CD</summary>
        internal const string CELL_NAME_YOUKI_CD = "YOUKI_CD";

        /// <summary>容器名</summary>
        internal const string CELL_NAME_YOUKI_NAME_RYAKU = "YOUKI_NAME_RYAKU";

        /// <summary>システムID</summary>
        internal const string CELL_NAME_SYSTEM_ID = "SYSTEM_ID";

        /// <summary>明細システムID</summary>
        internal const string CELL_NAME_DETAIL_SYSTEM_ID = "DETAIL_SYSTEM_ID";

        /// <summary>割振重量計算用のグルーピングNo</summary>
        internal const string CELL_NAME_warihuriNo = "warihuriNo";

        /// <summary>warihuriNo内の行番</summary>
        internal const string CELL_NAME_warihuriRowNo = "warihuriRowNo";

        /// <summary>単位CD</summary>
        internal const string CELL_NAME_UNIT_CD = "UNIT_CD";

        /// <summary>単位名</summary>
        internal const string CELL_NAME_UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";

        /// <summary>数量</summary>
        internal const string CELL_NAME_SUURYOU = "SUURYOU";

        /// <summary>単価</summary>
        internal const string CELL_NAME_TANKA = "TANKA";

        /// <summary>確定区分</summary>
        internal const string CELL_NAME_KAKUTEI_KBN = "KAKUTEI_KBN";

        /// <summary>売上支払日</summary>
        internal const string CELL_NAME_URIAGESHIHARAI_DATE = "URIAGESHIHARAI_DATE";

        /// <summary>締処理状況</summary>
        internal const string CELL_NAME_JOUKYOU = "JOUKYOU";

        /// <summary>マニフェスト番号</summary>
        internal const string CELL_NAME_MANIFEST_ID = "MANIFEST_ID";

        // 2次
        /// <summary>荷姿数量</summary>
        internal const string CELL_NAME_NISUGATA_SUURYOU = "NISUGATA_SUURYOU";

        /// <summary>荷姿単位CD</summary>
        internal const string CELL_NAME_NISUGATA_UNIT_CD = "NISUGATA_UNIT_CD";

        /// <summary>荷姿単位名</summary>
        internal const string CELL_NAME_NISUGATA_NAME_RYAKU = "NISUGATA_NAME_RYAKU";

        /// <summary>在庫品名CD</summary>
        internal const string CELL_NAME_ZAIKO_HINMEI_CD = "ZAIKO_HINMEI_CD";

        /// <summary>在庫品名</summary>
        internal const string CELL_NAME_ZAIKO_HINMEI_NAME = "ZAIKO_HINMEI_NAME";

        /// <summary>在庫単価</summary>
        internal const string CELL_NAME_ZAIKO_TANKA = "ZAIKO_TANKA";

        /// <summary>明細備考</summary>
        internal const string CELL_NAME_MEISAI_BIKOU = "MEISAI_BIKOU";

        /// <summary>消費税率</summary>
        internal const string CELL_NAME_SHOUHIZEI_RATE = "CELL_SHOUHIZEI_RATE";

        /// <summary>税区分CD</summary>
        internal const string CELL_NAME_HINMEI_ZEI_KBN_CD = "HINMEI_ZEI_KBN_CD";

        /// <summary>
        /// 明細行に売上と支払が混在している場合
        /// </summary>
        internal const int URIAGE_SHIHARAI_MIXED = 0;

        /// <summary>
        /// 明細行に売上のみある場合
        /// </summary>
        internal const int URIAGE_ONLY = 1;

        /// <summary>
        /// 明細行に支払のみある場合
        /// </summary>
        internal const int SHIHARAI_ONLY = 2;

        // 仕切書種類
        internal enum DENPYO_SHIKIRISHO_KIND
        {
            SEIKYUU = 1,
            SHIHARAI,
            SOUSAI
        }

        // 伝票発行区分
        internal const string DEF_HAKKOU_KBN_KOBETSU = "1";
        internal const string DEF_HAKKOU_KBN_SOUSAI = "2";
        internal const string DEF_HAKKOU_KBN_ALL = "3";

        /// <summary>
        /// 端数処理種別
        /// </summary>
        internal enum fractionType : int
        {
            CEILING = 1,	// 切り上げ
            FLOOR,		// 切り捨て
            ROUND,		// 四捨五入
        }

        /// <summary>
        /// システム単価書式コード
        /// </summary>
        internal enum SysTankaFormatCd : int
        {
            BLANK = 1,      // 1の位がゼロなら空白表示
            NONE,           // 1の位がゼロならゼロ表示
            ONEPOINT,       // 小数点第１位まで表示
            TWOPOINT,       // 小数点第２位まで表示
            THREEPOINT,     // 小数点第３位まで表示
        }

        /// <summary>
        /// システム数量書式コード
        /// </summary>
        internal enum SysSuuryouFormatCd : int
        {
            BLANK = 1,      // 1の位がゼロなら空白表示
            NONE,           // 1の位がゼロならゼロ表示
            ONEPOINT,       // 小数点第１位まで表示
            TWOPOINT,       // 小数点第２位まで表示
            THREEPOINT,     // 小数点第３位まで表示
        }

        /// <summary>
        /// 滞留登録された出荷伝票に設定する画面区分(新規)
        /// </summary>
        internal static readonly WINDOW_TYPE tairyuuWindowType = WINDOW_TYPE.NEW_WINDOW_FLAG;

        //PhuocLoc 2020/12/01 #136220 -Start
        /// <summary>
        /// 滞留一覧を表示しない場合
        /// </summary>
        internal const string TAIRYU_ICHIRAN_HIDDEN = "1";

        /// <summary>
        /// 滞留一覧を表示する場合
        /// </summary>
        internal const string TAIRYU_ICHIRAN_SHOW = "2";
        //PhuocLoc 2020/12/01 #136220 -End
    }
}