using System;

namespace Shougun.Core.Common.Kakepopup.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public class UIConstans
    {
		/// <summary>
		/// 画面種別
		/// </summary>
		public enum DispType
		{
			URIKAKE = 0,	// 売掛金一覧表
			KAIKAKE,	    // 買掛金一覧表
		};
		/// <summary>
		/// 範囲条件情報（戻り値）
		/// </summary>
		public struct ConditionInfo
		{
			/// <summary>
			/// 呼び出し画面
			/// </summary>
			public DispType ShowDisplay;
            /// <summary>
            /// 出力区分
            /// </summary>
            public int OutPutKBN;
            /// <summary>
            /// <summary>
            /// 抽出方法
            /// </summary>
            public int TyusyutsuHouhou;
            /// <summary>
			/// 開始日付
			/// </summary>
			public DateTime StartDay;
			/// <summary>
			/// 終了日付
			/// </summary>
			public DateTime EndDay;
			/// <summary>
			/// 開始取引先CD
			/// </summary>
			public String StartTorihikisakiCD;
			/// <summary>
			/// 終了取引先CD
			/// </summary>
			public String EndTorihikisakiCD;
            /// <summary>
            /// 値格納フラグ
            /// </summary>
            public bool DataSetFlag;
		};
	}
}
