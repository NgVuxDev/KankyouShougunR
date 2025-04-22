// $Id: SystemSetteiHoshuConstans.cs 49000 2015-05-07 04:53:56Z y-hosokawa@takumi-sys.co.jp $

namespace SystemSetteiHoshu.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class SystemSetteiHoshuConstans
    {
        /// <summary>書式</summary>
        public static readonly string FORMAT_1 = "#,###";
        public static readonly string FORMAT_2 = "#,##0";
        public static readonly string FORMAT_3 = "#,##0.0";
        public static readonly string FORMAT_4 = "#,##0.00";
        public static readonly string FORMAT_5 = "#,##0.000";
        public static readonly string FORMAT_6 = "#,##0.0000";

        #region 書式区分
        /// <summary>書式区分(請求/支払先別)</summary>
        public static readonly char SHOSHIKI_KBN_TORIHIKISAKI = '1';

        /// <summary>書式区分(業者別)</summary>
        public static readonly char SHOSHIKI_KBN_GYOUSHA = '2';

        /// <summary>書式区分(現場別)</summary>
        public static readonly char SHOSHIKI_KBN_GENBA = '3';
        #endregion

        #region 書式明細区分
        /// <summary>書式明細区分(なし)</summary>
        public static readonly char SHOSHIKI_MEISAI_KBN_NONE = '1';

        /// <summary>書式明細区分(業者毎)</summary>
        public static readonly char SHOSHIKI_MEISAI_KBN_GYOUSHA = '2';

        /// <summary>書式明細区分(現場毎)</summary>
        public static readonly char SHOSHIKI_MEISAI_KBN_GENBA = '3';
        #endregion

        #region 税計算区分
        /// <summary>税計算区分(伝票毎)</summary>
        public static readonly char ZEI_KEISAN_KBN_DENPYOU = '1';

        /// <summary>税計算区分(請求毎)</summary>
        public static readonly char ZEI_KEISAN_KBN_SEIKYUU = '2';
        #endregion

        #region 税区分
        /// <summary>税区分(外税)</summary>
        public static readonly char ZEI_KBN_SOTO = '1';

        /// <summary>税区分(内税)</summary>
        public static readonly char ZEI_KBN_UCHI = '2';

        /// <summary>税区分(非課税)</summary>
        public static readonly char ZEI_KBN_NONE = '3';
        #endregion

        #region 確定利用
        /// <summary>確定利用する</summary>
        public static readonly char KAKUTEI_RIYOU_ON = '1';

        /// <summary>確定利用しない</summary>
        public static readonly char KAKUTEI_RIYOU_OFF = '2';
        #endregion 

        #region 確定フラグ
        /// <summary>確定伝票</summary>
        public static readonly char KAKUTEI_DENPYOU = '1';

        /// <summary>未確定伝票</summary>
        public static readonly char MIKAKUTEI_DENPYOU = '2';
        #endregion

        #region システム設定入力(初回登録)処理用
        /// <summary>システム設定入力用(初回登録)パスワード</summary>
        public static readonly string MESSAGE_INITAIL_PASSWORDS = "Ed203388";
        #endregion

        #region 請求携帯
        /// <summary>請求携帯(単月請求)</summary>
        public static readonly char SEIKYU_KEITAI_KBN_TANGETSU = '1';

        /// <summary>請求携帯(繰越請求)</summary>
        public static readonly char SEIKYU_KEITAI_KBN_KURIKOSI = '2';
        #endregion

        #region 入金/出金明細
        /// <summary>入金/出金明細(表示する)</summary>
        public static readonly char NYUSYUKIN_MEISAI_KBN_HYOUJI = '1';

        /// <summary>入金/出金明細(表示なし)</summary>
        public static readonly char NYUSYUKIN_MEISAI_KBN_HIHYOUJI = '2';
        #endregion
    }
}
