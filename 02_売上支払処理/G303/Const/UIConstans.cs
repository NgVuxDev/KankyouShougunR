namespace Shougun.Core.SalesPayment.Tairyuichiran.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class UIConstans
    {
		/// <summary>
		/// 対象テーブル
		/// </summary>
		internal enum targetTable : int
		{
			UKEIRE = 1,	// 受入
			SHUKKA		// 出荷
		}

        /// <summary>
        /// 滞留番号
        /// </summary>
        internal static readonly string TAIRYU_NUMBER = "滞留番号";

        /// <summary>
        /// システム上必須な項目（伝票種類）
        /// </summary>
        internal static readonly string DENPYOU_SHURUI = "伝票種類";

        /// <summary>
        /// 伝票テーブル名
        /// </summary>
        internal static readonly string ENTRY_TABLE = "SUMMARY_ENTRY";

        /// <summary>
        /// 明細テーブル名
        /// </summary>
        internal static readonly string DETAIL_TABLE = "SUMMARY_DETAIL";

        /// <summary>
        /// システム上必須な項目（伝票番号）
        /// </summary>
        internal static readonly string HIDDEN_DENPYOU_NUMBER = "HIDDEN_DENPYOU_NUMBER";

        /// <summary>
        /// システム上必須な項目（明細システムID）
        /// </summary>
        internal static readonly string HIDDEN_DETAIL_SYSTEM_ID = "HIDDEN_DETAIL_SYSTEM_ID";

        ///// <summary>
        ///// システム税計算区分利用形態
        ///// </summary>
        //internal static readonly string DENPYOU = "1";   // 締毎税・伝票毎税
        //internal static readonly string MEISAI = "2";   // 締毎税・明細毎税

        ///// <summary>
        ///// 最大桁数
        ///// </summary>
        //internal static int MAX_LENGTH = 32767;

        ///// <summary>
        ///// Button設定用XMLファイルパス
        ///// </summary>
        //internal static readonly string ButtonInfoXmlPath = "Shougun.Core.SalesManagement.UriageMotocho.Setting.ButtonSetting.xml";
        ///// <summary>
        ///// Report設定用XMLファイルパス
        ///// </summary>
        //internal static readonly string ReportInfoXmlPath = "Shougun.Core.SalesManagement.UriageMotocho.Report.R415-Form.xml";
	}
}
