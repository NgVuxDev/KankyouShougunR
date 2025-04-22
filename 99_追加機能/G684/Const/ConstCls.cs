namespace Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Const
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
        internal static readonly string BUTTON_INFO_XML_PATH = "Shougun.Core.BusinessManagement.DenpyouIkkatuUpdate.Setting.ButtonSetting.xml";

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
        internal const string COLUMN_DENPYOU_NO = "mr_DENPYOU_NO";
        internal const string COLUMN_KYOTEN_CD = "mr_KYOTEN_CD";
        internal const string COLUMN_KYOTEN_NAME = "mr_KYOTEN_NAME";
        internal const string COLUMN_KAKUTEI_KBN = "mr_KAKUTEI_KBN";
        internal const string COLUMN_DENPYOU_DATE = "mr_DENPYOU_DATE";
        internal const string COLUMN_URIAGE_DATE = "mr_URIAGE_DATE";
        internal const string COLUMN_SHIHARAI_DATE = "mr_SHIHARAI_DATE";
        internal const string COLUMN_TORIHIKISAKI_CD = "mr_TORIHIKISAKI_CD";
        internal const string COLUMN_TORIHIKISAKI_NAME = "mr_TORIHIKISAKI_NAME";
        internal const string COLUMN_GYOUSHA_CD = "mr_GYOUSHA_CD";
        internal const string COLUMN_GYOUSHA_NAME = "mr_GYOUSHA_NAME";
        internal const string COLUMN_GENBA_CD = "mr_GENBA_CD";
        internal const string COLUMN_GENBA_NAME = "mr_GENBA_NAME";
        internal const string COLUMN_NIZUMI_GYOUSHA_CD = "mr_NIZUMI_GYOUSHA_CD";
        internal const string COLUMN_NIZUMI_GYOUSHA_NAME = "mr_NIZUMI_GYOUSHA_NAME";
        internal const string COLUMN_NIZUMI_GENBA_CD = "mr_NIZUMI_GENBA_CD";
        internal const string COLUMN_NIZUMI_GENBA_NAME = "mr_NIZUMI_GENBA_NAME";
        internal const string COLUMN_NIOROSHI_GYOUSHA_CD = "mr_NIOROSHI_GYOUSHA_CD";
        internal const string COLUMN_NIOROSHI_GYOUSHA_NAME = "mr_NIOROSHI_GYOUSHA_NAME";
        internal const string COLUMN_NIOROSHI_GENBA_CD = "mr_NIOROSHI_GENBA_CD";
        internal const string COLUMN_NIOROSHI_GENBA_NAME = "mr_NIOROSHI_GENBA_NAME";
        internal const string COLUMN_EIGYOU_TANTOUSHA_CD = "mr_EIGYOU_TANTOUSHA_CD";
        internal const string COLUMN_EIGYOU_TANTOUSHA_NAME = "mr_EIGYOU_TANTOUSHA_NAME";
        internal const string COLUMN_NYUURYOKU_TANTOUSHA_NAME = "mr_NYUURYOKU_TANTOUSHA_NAME";
        internal const string COLUMN_SHARYOU_CD = "mr_SHARYOU_CD";
        internal const string COLUMN_SHARYOU_NAME = "mr_SHARYOU_NAME";
        internal const string COLUMN_SHASHU_CD = "mr_SHASHU_CD";
        internal const string COLUMN_SHASHU_NAME = "mr_SHASHU_NAME";
        internal const string COLUMN_UNPAN_GYOUSHA_CD = "mr_UNPAN_GYOUSHA_CD";
        internal const string COLUMN_UNPAN_GYOUSHA_NAME = "mr_UNPAN_GYOUSHA_NAME";
        internal const string COLUMN_UNTENSHA_CD = "mr_UNTENSHA_CD";
        internal const string COLUMN_UNTENSHA_NAME = "mr_UNTENSHA_NAME";
        internal const string COLUMN_KEITAI_KBN_CD = "mr_KEITAI_KBN_CD";
        internal const string COLUMN_KEITAI_KBN = "mr_KEITAI_KBN";
        internal const string COLUMN_DAIKAN_KBN_CD = "mr_DAIKAN_KBN_CD";
        internal const string COLUMN_DAIKAN_KBN = "mr_DAIKAN_KBN";
        internal const string COLUMN_MANIFEST_SHURUI_CD = "mr_MANIFEST_SHURUI_CD";
        internal const string COLUMN_MANIFEST_SHURUI_NAME = "mr_MANIFEST_SHURUI_NAME";
        internal const string COLUMN_MANIFEST_TEHAI_CD = "mr_MANIFEST_TEHAI_CD";
        internal const string COLUMN_MANIFEST_TEHAI_NAME = "mr_MANIFEST_TEHAI_NAME";
        internal const string COLUMN_DENPYOU_BIKOU = "mr_DENPYOU_BIKOU";
        internal const string COLUMN_TAIRYUU_BIKOU = "mr_TAIRYUU_BIKOU";

        #endregion
    }
}