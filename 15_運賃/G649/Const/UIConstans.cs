// $Id: UIConstans.cs 20729 2014-05-16 02:38:01Z y-hosokawa@takumi-sys.co.jp $
using System;

namespace Shougun.Core.Carriage.UnchinDaichouHaniJokenPopUp.Const
{
    /// <summary>
    /// 定数クラス
    /// </summary>
    public static class UIConstans
    {
        /// <summary>
        /// 範囲条件情報（戻り値）
        /// </summary>
        public struct ConditionInfo
        {
            /// <summary>
            /// 開始日付
            /// </summary>
            public DateTime StartDay;
            /// <summary>
            /// 終了日付
            /// </summary>
            public DateTime EndDay;
            /// <summary>
            /// 開始運搬業者CD
            /// </summary>
            public String StartUnpanGyoushaCD;
            /// <summary>
            /// 終了運搬業者CD
            /// </summary>
            public String EndUnpanGyoushaCD;
            /// <summary>
            /// 出力区分
            /// </summary>
            public int OutPutKBN;
            /// <summary>
            /// データ設定フラッグ
            /// </summary>
            public bool DataSetFlag;
        };
    }
}
