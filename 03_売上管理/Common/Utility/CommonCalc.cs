using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Function.ShougunCSCommon.Utility
{
	/// <summary>
	/// 将軍共通で使用する計算処理クラス
	/// </summary>
	public static class CommonCalc
	{
		/// <summary>
		/// 端数処理種別
		/// </summary>
		private enum fractionType
		{
			CEILING = 1,	// 切り上げ
			FLOOR,		// 切り捨て
			ROUND,		// 四捨五入
		}

		/// <summary>
		/// 指定された端数CDに従い、金額の端数処理を行う
		/// </summary>
		/// <param name="kingaku">端数処理対象金額</param>
		/// <param name="calcCD">端数CD</param>
		/// <returns name="decimal">端数処理後の金額</returns>
		public static decimal FractionCalc(decimal kingaku, int calcCD)
		{
			decimal returnVal = 0;		// 戻り値

			switch((fractionType)calcCD)
		    {
				case fractionType.CEILING:
					returnVal = Math.Ceiling(kingaku);
					break;
				case fractionType.FLOOR:
					returnVal = Math.Floor(kingaku);
					break;
				case fractionType.ROUND:
                    returnVal = Math.Round(kingaku, 0, MidpointRounding.AwayFromZero);
					break;
				default:
					// NOTHING
					break;
			}

			return returnVal;
		}
	}
}
