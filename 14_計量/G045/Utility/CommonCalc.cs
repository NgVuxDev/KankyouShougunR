using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using r_framework.Utility;
using Shougun.Core.Scale.Keiryou.Const;
using Shougun.Core.Scale.Keiryou.Dto;

namespace Shougun.Core.Scale.Keiryou.Utility
{
	/// <summary>
	/// 将軍共通で使用する計算処理クラス
	/// </summary>
	public static class CommonCalc
	{
		/// <summary>
		/// 端数処理種別
		/// </summary>
		private enum fractionType : int
		{
			CEILING = 1,	// 切り上げ
			FLOOR,		// 切り捨て
			ROUND,		// 四捨五入
		}

        /// <summary>
        /// 端数処理桁用Enum
        /// </summary>
        private enum hasuKetaType : short
        {
            NONE = 1,       // 1の位
            ONEPOINT,       // 小数第一位
            TOWPOINT,       // 小数第二位
            THREEPOINT,     // 小数第三位
            FOUR,           // 小数第四位
        }

        /// <summary>
        /// 指定された端数CD、端数桁数に従い、金額の端数処理を行う
        /// </summary>
        /// <param name="kingaku">端数処理対象金額</param>
        /// <param name="calcCD">端数CD</param>
        /// <param name="hasuKeta">端数処理桁数(SYS_INFOの端数処理桁数をそのまま指定してください)</param>
        /// <returns name="decimal">端数処理後の金額</returns>
        public static decimal FractionCalc(decimal kingaku, int calcCD, short hasuKeta)
        {
            decimal returnVal = 0;		// 戻り値
            double hasuKetaCoefficient = 1;
            decimal sign = 1;
            if (kingaku < 0)
            {
                sign = -1;
            }

            switch ((hasuKetaType)hasuKeta)
            {
                case hasuKetaType.NONE:
                    break;

                default:
                    //hasuKetaCoefficient = Math.Pow(10, hasuKeta - 1);
                    hasuKetaCoefficient = Math.Pow(10, hasuKeta - 2);   // 共通関数と桁が異なっていたため変更
                    break;
            }

            kingaku = Math.Abs(kingaku);

            switch ((fractionType)calcCD)
            {
                case fractionType.CEILING:
                    returnVal = Math.Ceiling(kingaku * (decimal)hasuKetaCoefficient) / (decimal)hasuKetaCoefficient;
                    break;
                case fractionType.FLOOR:
                    returnVal = Math.Floor(kingaku * (decimal)hasuKetaCoefficient) / (decimal)hasuKetaCoefficient;
                    break;
                case fractionType.ROUND:
                    returnVal = Math.Round(kingaku * (decimal)hasuKetaCoefficient, MidpointRounding.AwayFromZero) / (decimal)hasuKetaCoefficient;
                    break;
                default:
                    // NOTHING
                    break;
            }

            returnVal = returnVal * sign;

            return returnVal;
        }

        /// <summary>
        /// 金額の共通フォーマットメソッド
        /// 単価などM_SYS_INFO等にフォーマットが設定されている
        /// ものについては使用しないでください
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string DecimalFormat(Decimal num)
        {
            string format = "#,##0";
            return string.Format("{0:" + format + "}", num);
        }
	}
}
