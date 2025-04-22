using System.Data.SqlTypes;
namespace Shougun.Core.Billing.SeikyuuMeisaihyouShutsuryoku
{
    /// <summary>
    /// 入金明細表出力で使用するDto（主に検索条件として使用）
    /// </summary>
    public class DTOClass
    {
        /// <summary>
        /// デフォルトコンストラクタ
        /// </summary>
        public DTOClass()
        {
            SYOSIKI = 1;
            KYOTEN_CD = SqlInt16.Null;
            HIDUKE_FROM = SqlDateTime.Null;
            HIDUKE_TO = SqlDateTime.Null;
            TORIHIKISAKI_CD_FROM = null;
            TORIHIKISAKI_CD_TO = null;
           
        }

        /// <summary>
        /// 拠点CDを取得・設定します
        /// </summary>
        public SqlInt16 KYOTEN_CD { get; set; }

        /// <summary>
        /// 請求日付Fromを取得・設定します
        /// </summary>
        public SqlDateTime HIDUKE_FROM { get; set; }
        /// <summary>
        /// 請求日付Toを取得・設定します
        /// </summary>
        public SqlDateTime HIDUKE_TO { get; set; }
        /// <summary>
        /// 請求書書式を取得・設定します
        /// </summary>
        public int SYOSIKI { get; set; }

        /// <summary>
        /// 取引先CDFromを取得・設定します
        /// </summary>
        public string TORIHIKISAKI_CD_FROM { get; set; }

        // <summary>
        /// 取引先名Fromを取得・設定します
        /// </summary>
        public string TORIHIKISAKI_NAME_RYAKU_FROM { get; set; }

        /// <summary>
        /// 取引先CDToを取得・設定します
        /// </summary>
        public string TORIHIKISAKI_CD_TO { get; set; }

        /// <summary>
        /// 取引先名Toを取得・設定します
        /// </summary>
        public string TORIHIKISAKI_NAME_RYAKU_TO { get; set; }

        /// <summary>
        /// 並び順
        /// </summary>
        public string TORIHIKISAKI_SORT { get; set; }
    }
}
