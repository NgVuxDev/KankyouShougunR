
namespace Shougun.Core.SalesPayment.ShiharaiJunihyo
{
    public class ShiharaiJunihyoDto
    {
        #region Property

        /// <summary>
        /// セレクト対象カラムを取得・設定します
        /// </summary>
        public string SELECT_COLUMN { get; set; }

        /// <summary>
        /// グループ化の対象カラムを取得・設定します
        /// </summary>
        public string GROUP_COLUMN { get; set; }

        /// <summary>
        /// ソート対象カラムを取得・設定します
        /// </summary>
        public string SORT_COLUMN { get; set; }

        /// <summary>
        /// 拠点CD
        /// </summary>
        public int KYOTEN_CD { set; get; }

        /// <summary>
        /// 伝票日付_From
        /// </summary>
        public string DENPYOU_DATE_FROM { set; get; }

        /// <summary>
        /// 伝票日付_To
        /// </summary>
        public string DENPYOU_DATE_TO { set; get; }

        /// <summary>
        /// 支払日付_From
        /// </summary>
        public string SHIHARAI_DATE_FROM { set; get; }

        /// <summary>
        /// 支払日付_To
        /// </summary>
        public string SHIHARAI_DATE_TO { set; get; }

        /// <summary>
        /// 更新日付_From
        /// </summary>
        public string UPDATE_DATE_FROM { set; get; }

        /// <summary>
        /// 更新日付_To
        /// </summary>
        public string UPDATE_DATE_TO { set; get; }

        /// <summary>
        /// 伝票種類を取得・設定します
        /// </summary>
        public int DENPYOU_SHURUI { get; set; }

        /// <summary>
        /// 取引区分を取得・設定します
        /// </summary>
        public int TORIHIKI_KBN { get; set; }

        /// <summary>
        /// 確定区分を取得・設定します
        /// </summary>
        public int KAKUTEI_KBN { get; set; }

        /// <summary>
        /// 締処理状況を取得・設定します
        /// </summary>
        public int SHIME_JOKYO { get; set; }

        /// <summary>
        /// 取引先CD_From
        /// </summary>
        public string TORIHIKISAKI_CD_FROM { set; get; }

        /// <summary>
        /// 取引先CD_To
        /// </summary>
        public string TORIHIKISAKI_CD_TO { set; get; }

        /// <summary>
        /// 業者CD_From
        /// </summary>
        public string GYOUSHA_CD_FROM { set; get; }

        /// <summary>
        /// 業者CD_To
        /// </summary>
        public string GYOUSHA_CD_TO { set; get; }

        /// <summary>
        /// 現場CD_From
        /// </summary>
        public string GENBA_CD_FROM { set; get; }

        /// <summary>
        /// 現場CD_To
        /// </summary>
        public string GENBA_CD_TO { set; get; }

        /// <summary>
        /// 営業担当者CD_From
        /// </summary>
        public string EIGYOU_TANTOUSHA_CD_FROM { set; get; }

        /// <summary>
        /// 営業担当者CD_To
        /// </summary>
        public string EIGYOU_TANTOUSHA_CD_TO { set; get; }

        /// <summary>
        /// 運転者CD_From
        /// </summary>
        public string UNTENSHA_CD_FROM { set; get; }

        /// <summary>
        /// 運転者CD_To
        /// </summary>
        public string UNTENSHA_CD_TO { set; get; }

        /// <summary>
        /// 品名CD_From
        /// </summary>
        public string HINMEI_CD_FROM { set; get; }

        /// <summary>
        /// 品名CD_To
        /// </summary>
        public string HINMEI_CD_TO { set; get; }

        /// <summary>
        /// 種類CD_From
        /// </summary>
        public string SHURUI_CD_FROM { set; get; }

        /// <summary>
        /// 種類CD_To
        /// </summary>
        public string SHURUI_CD_TO { set; get; }

        /// <summary>
        /// 分類CD_From
        /// </summary>
        public string BUNRUI_CD_FROM { set; get; }

        /// <summary>
        /// 分類CD_To
        /// </summary>
        public string BUNRUI_CD_TO { set; get; }

        /// <summary>
        /// 順位
        /// </summary>
        public int RANK { set; get; }

        //PhuocLoc 2020/12/07 #136228 -Start
        /// <summary>
        /// 集計項目CD_From
        /// </summary>
        public string SHUUKEI_KOUMOKU_CD_FROM { set; get; }

        /// <summary>
        /// 集計項目CD_To
        /// </summary>
        public string SHUUKEI_KOUMOKU_CD_TO { set; get; }
        //PhuocLoc 2020/12/07 #136228 -End

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShiharaiJunihyoDto()
        {
            // プロパティ初期化
            SELECT_COLUMN = string.Empty;               // セレクト対象カラム名
            GROUP_COLUMN = string.Empty;                // グループ化の対象カラム名
            SORT_COLUMN = string.Empty;                 // ソート対象カラム名
            KYOTEN_CD = 99;                             // 拠点CD
            DENPYOU_DATE_FROM = string.Empty;           // 伝票日付_From
            DENPYOU_DATE_TO = string.Empty;             // 伝票日付_To
            SHIHARAI_DATE_FROM = string.Empty;          // 支払日付_From
            SHIHARAI_DATE_TO = string.Empty;            // 支払日付_To
            UPDATE_DATE_FROM = string.Empty;            // 更新日付_From
            UPDATE_DATE_TO = string.Empty;              // 更新日付_To
            // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) Start
            DENPYOU_SHURUI = 5;                         // 伝票種類
            // 20150514 伝種「4.代納」追加(不具合一覧(つ) 23) End
            TORIHIKI_KBN = 3;                           // 取引区分
            KAKUTEI_KBN = 3;                            // 確定区分
            SHIME_JOKYO = 3;                            // 締処理状況
            TORIHIKISAKI_CD_FROM = string.Empty;        // 取引先CD_From
            TORIHIKISAKI_CD_TO = string.Empty;          // 取引先CD_To
            GYOUSHA_CD_FROM = string.Empty;             // 業者CD_From
            GYOUSHA_CD_TO = string.Empty;               // 業者CD_To
            GENBA_CD_FROM = string.Empty;               // 現場CD_From
            GENBA_CD_TO = string.Empty;                 // 現場CD_To
            EIGYOU_TANTOUSHA_CD_FROM = string.Empty;    // 営業担当者CD_From
            EIGYOU_TANTOUSHA_CD_TO = string.Empty;      // 営業担当者CD_To
            UNTENSHA_CD_FROM = string.Empty;            // 運転者CD_From
            UNTENSHA_CD_TO = string.Empty;              // 運転者CD_To
            HINMEI_CD_FROM = string.Empty;              // 品名CD_From
            HINMEI_CD_TO = string.Empty;                // 品名CD_To
            RANK = 0;                                   // 順位
        }

        #endregion
    }
}
