namespace Shougun.Core.Billing.AtenaLabel.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class UIConstans
    {
		/// <summary>
		/// Button設定用XMLファイルパス
		/// </summary>
        internal static readonly string ButtonInfoXmlPath = "Shougun.Core.Billing.AtenaLabel.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面種別
        /// </summary>
        internal static readonly int SEIKYUU = 0;	// 請求宛名ラベル
        internal static readonly int SEISAN = 1;	    // 精算宛名ラベル
        internal static readonly int MANIFEST = 2;       // マニフェスト宛名ラベル

        /// <summary>
        /// 最大印刷データ数
        /// </summary>
        internal static readonly int PRINTMAX= 12;　
	}
}
