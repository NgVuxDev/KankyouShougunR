// $Id: UIConstans.cs 20729 2014-05-16 02:38:01Z y-hosokawa@takumi-sys.co.jp $
using System;

namespace Shougun.Core.SalesManagement.MotochoHaniJokenPopUp.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class UIConstans
    {
		/// <summary>
		/// 画面種別
		/// </summary>
		public enum DispType : int
		{
			URIAGE = 0,	// 売上元帳
			SHIHARAI,	// 支払元帳
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
			/// 出力区分
			/// </summary>
			public int OutPutKBN;
			/// <summary>
			/// 取引区分（元帳種類）
			/// </summary>
			public int TorihikiKBN;
			/// <summary>
			/// 抽出方法
			/// </summary>
			public int TyuusyutuKBN;
			/// <summary>
			/// 締日
			/// </summary>
			public String Shimebi;
			/// <summary>
			/// 値格納フラグ
			/// </summary>
			public bool DataSetFlag;
		};
	}
}
