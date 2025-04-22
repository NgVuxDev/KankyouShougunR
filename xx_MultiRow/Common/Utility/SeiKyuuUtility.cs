using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Function.ShougunCSCommon.Utility
{
    /// <summary>
    /// 請求関連のユーティリティ
    /// </summary>
    public class SeiKyuuUtility
    {
        /// <summary>
        /// 近々の締日付を返す
        /// </summary>
        /// <param name="hiduke">元になる日付</param>
        /// <param name="shimebis">締日配列</param>
        /// <returns>締日付</returns>
        public DateTime GetNearSeikyuDate(DateTime hiduke, short[] shimebis)
        {
            DateTime returnVal = hiduke.Date;

            short day = (short)hiduke.Day;
            short shimebi = 0;

            if (shimebis.Length < 1)
            {
                return returnVal;
            }

            Array.Sort(shimebis);
            
            short begintRange = 0;

            for (int i = 0; i < shimebis.Length; i++)
            {
                var tempSimebi = shimebis[i];

                if (begintRange < day && day <= tempSimebi)
                {
                    shimebi = tempSimebi;
                    break;
                }

                begintRange = tempSimebi;
            }

            DateTime tempReturnVal = DateTime.Now;

            if (shimebi < 1)
            {
                // 3つ目の締日を超えている場合
                // 翌月の最初の締日を設定
                if (DateTime.TryParse(hiduke.AddMonths(1).Year + "/" + hiduke.AddMonths(1).Month + "/" + shimebis[0], out tempReturnVal))
                {
                    returnVal = tempReturnVal.Date;
                }
                else
                {
                    // 29日以降指定している場合に日付生成で失敗するはずなので、
                    // 月末を指定
                    returnVal = new DateTime(hiduke.Year, hiduke.Month, DateTime.DaysInMonth(hiduke.Year, hiduke.Month));
                }
            }
            else
            {
                if (DateTime.TryParse(hiduke.Year + "/" + hiduke.Month + "/" + shimebi, out tempReturnVal))
                {
                    returnVal = tempReturnVal.Date;
                }
                else
                {
                    // 29日以降指定している場合に日付生成で失敗するはずなので、
                    // 月末を指定
                    returnVal = new DateTime(hiduke.Year, hiduke.Month, DateTime.DaysInMonth(hiduke.Year, hiduke.Month));
                }
            }

            return returnVal;
        }
    }
}
