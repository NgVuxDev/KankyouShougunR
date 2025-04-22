namespace Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    internal class ConstCls
    {
        // HINT: Switch句に利用する必要限り、Constキーワードを付け、
        //       その他に、Static Readonlyで定義すべき。
        //       While variables will be used in SWITCH statement, declare them with CONST.
        //       Otherwise, declare them with STATIC READONLY.

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        internal static readonly string BUTTON_INFO_XML_PATH = "Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.Setting.ButtonSetting.xml";

        /// <summary>
        /// 拠点CD 99(全社)
        /// </summary>
        internal static readonly short KYOTEN_CD_ZENSHA = 99;

        internal const int CONTENA_JOUKYOU_KBN_SECCHI = 2;
        internal const int CONTENA_JOUKYOU_KBN_HIKIAGE = 3;

        /// <summary>
        /// 伝票日付区分ラベル文字
        /// </summary>
        internal static readonly string DENPYOU_DATE_KBN_NAME_DENPYOU = "伝票日付※";
        internal static readonly string DENPYOU_DATE_KBN_NAME_NYUURYOKU = "入力日付※";
        internal static readonly string DENPYOU_DATE_KBN_NAME_SAGYOU = "作業日※";

        #region グリッドカラム

        internal const string COLUMN_CHECK = "CHECK";
        internal const string COLUMN_DENPYOU_DATE = "mr_DENPYOU_DATE";
        internal const string COLUMN_URIAGE_DATE = "mr_URIAGE_DATE";
        internal const string COLUMN_SHIHARAI_DATE = "mr_SHIHARAI_DATE";
        internal const string COLUMN_TORIHIKISAKI_CD = "mr_TORIHIKISAKI_CD";
        internal const string COLUMN_TORIHIKISAKI_NAME = "mr_TORIHIKISAKI_NAME";
        internal const string COLUMN_GYOUSHA_CD = "mr_GYOUSHA_CD";
        internal const string COLUMN_GYOUSHA_NAME = "mr_GYOUSHA_NAME";
        internal const string COLUMN_GENBA_CD = "mr_GENBA_CD";
        internal const string COLUMN_GENBA_NAME = "mr_GENBA_NAME";
        internal const string COLUMN_UNPAN_GYOUSHA_CD = "mr_UNPAN_GYOUSHA_CD";
        internal const string COLUMN_UNPAN_GYOUSHA_NAME = "mr_UNPAN_GYOUSHA_NAME";
        internal const string COLUMN_NIOROSHI_GYOUSHA_CD = "mr_NIOROSHI_GYOUSHA_CD";
        internal const string COLUMN_NIOROSHI_GYOUSHA_NAME = "mr_NIOROSHI_GYOUSHA_NAME";
        internal const string COLUMN_NIOROSHI_GENBA_CD = "mr_NIOROSHI_GENBA_CD";
        internal const string COLUMN_NIOROSHI_GENBA_NAME = "mr_NIOROSHI_GENBA_NAME";
        internal const string COLUMN_DENPYOU_NO = "mr_DENPYOU_NO";
        internal const string COLUMN_HINMEI_CD = "mr_HINMEI_CD";
        internal const string COLUMN_HINMEI_NAME = "mr_HINMEI_NAME";
        internal const string COLUMN_SUURYOU = "mr_SUURYOU";
        internal const string COLUMN_UNIT_NAME = "mr_UNIT_NAME";
        internal const string COLUMN_UNIT_CD = "mr_UNIT_CD";
        internal const string COLUMN_TANKA = "mr_TANKA";
        internal const string COLUMN_KINGAKU = "mr_KINGAKU";
        internal const string COLUMN_TORIHIKI_KBN = "mr_TORIHIKI_KBN";
        internal const string COLUMN_ZEIKEISAN_KBN = "mr_ZEIKEISAN_KBN";
        internal const string COLUMN_BLANK_1 = "mr_BLANK_1";
        internal const string COLUMN_KYOTEN_NAME = "mr_KYOTEN_NAME";
        internal const string COLUMN_DENSHU_KBN = "mr_DENSHU_KBN";
        internal const string COLUMN_DENPYOU_KBN_CD = "mr_DENPYOU_KBN_CD";
        internal const string COLUMN_DENPYOU_KBN_NAME = "mr_DENPYOU_KBN_NAME";
        internal const string COLUMN_BLANK_2 = "mr_BLANK_2";
        internal const string COLUMN_NEW_TANKA = "mr_NEW_TANKA";
        internal const string COLUMN_NEW_KINGAKU = "mr_NEW_KINGAKU";
        internal const string COLUMN_MEISAI_BIKOU = "mr_MEISAI_BIKOU";

        #endregion
    }
}