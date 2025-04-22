using System;
namespace Shougun.Core.Stock.ZaikoKanriHyo.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class UIConstans
    {
		/// <summary>
		/// Button設定用XMLファイルパス
		/// </summary>
        internal static readonly string ButtonInfoXmlPath = "Shougun.Core.Stock.ZaikoKanriHyo.Setting.ButtonSetting.xml";

        /// <summary>
        /// 範囲条件情報（戻り値）
        /// </summary>
        public struct ConditionInfo
        {
            /// <summary>
            /// 出力区分
            /// </summary>
            public int OutPutKBN;
            /// <summary>
            /// 開始日付
            /// </summary>
            public DateTime DateFrom;
            /// <summary>
            /// 終了日付
            /// </summary>
            public DateTime DateTo;
            /// <summary>
            /// 開始業者CD
            /// </summary>
            public String GyoushaCdFrom;
            /// <summary>
            /// 終了業者CD
            /// </summary>
            public String GyoushaCdTo;
            /// <summary>
            /// 開始現場CD
            /// </summary>
            public String GenbaCdFrom;
            /// <summary>
            /// 終了現場CD
            /// </summary>
            public String GenbaCdTo;
            /// <summary>
            /// 開始在庫品名CD
            /// </summary>
            public String ZaikoHinmeiCdFrom;
            /// <summary>
            /// 終了在庫品名CD
            /// </summary>
            public String ZaikoHinmeiCdTo;
            /// <summary>
            /// 値格納フラグ
            /// </summary>
            public bool DataSetFlag;
        };
	}

}
