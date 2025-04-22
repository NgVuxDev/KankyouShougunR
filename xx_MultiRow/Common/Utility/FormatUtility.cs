using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Utility;

namespace Shougun.Function.ShougunCSCommon.Utility
{
    /// <summary>
    /// フォーマット用ユーティリティ
    /// </summary>
    public class FormatUtility
    {
		/// <summary>
        /// パーセント用フォーマット
        /// 想定使用箇所：受入入力(割振%、調整%)、出荷入力(割振%、調整%)
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static string toPercentValue(decimal? percent)
        {
            LogUtility.DebugMethodStart();

            string returnValue = null;

            if (percent != null)
            {
                returnValue = ((decimal)percent).ToString("0.0");
            }

            LogUtility.DebugMethodEnd();
            return returnValue;
        }

        /// <summary>
        /// パーセント用フォーマット
        /// 想定使用箇所：受入入力(割振%、調整%)、出荷入力(割振%、調整%)
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static string toPercentValue(decimal percent)
        {
            LogUtility.DebugMethodStart();

            string returnValue = null;

            if (percent != null)
            {
                returnValue = percent.ToString("0.0");
            }

            LogUtility.DebugMethodEnd();
            return returnValue;
        }

        /// <summary>
        /// 重量値、金額用フォーマット
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToAmountValue(string value)
        {
            LogUtility.DebugMethodStart(value);

            string returnValue = value;
            if (string.IsNullOrEmpty(value))
            {
                return returnValue;
            }

            // TODO: DBからフォーマットの値を取得してフォーマットしなきゃいけない
            decimal num = -1;
            if (decimal.TryParse(value, out num))
            {
                returnValue = num.ToString("#,0");
            }

            LogUtility.DebugMethodEnd();
            return returnValue;
        }

		/// <summary>
		/// 重量値、金額、数量用フォーマット
		/// </summary>
		/// <param name="value"></param>
		/// <param name="formatStr"></param>
		/// <returns></returns>
		public static string ToAmountValue(string value, string formatStr)
		{
			LogUtility.DebugMethodStart(value, formatStr);

			// 初期化
			string returnValue = "";

			// 変換対象がNULLもしくはEmptyだった場合はブランクを返す
			if(false == string.IsNullOrEmpty(value))
			{
				decimal num = -1;
				if(decimal.TryParse(value, out num))
				{
					// 指定されたフォーマット書式に従い変換
					returnValue = num.ToString(formatStr);
				}
			}

			LogUtility.DebugMethodEnd(returnValue);
			return returnValue;
		}
	}
}
