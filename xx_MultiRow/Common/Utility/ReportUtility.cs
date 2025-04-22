using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Function.ShougunCSCommon.Utility
{
    /// <summary>
    /// 帳票関連のユーティリティ
    /// </summary>
    public class ReportUtility
    {
        /// <summary>
        /// 領収書用ダミーデータ取得
        /// </summary>
        /// <returns></returns>
        public static string[] GetRoushuushoReportDummyData()
        {
            // 敬称1
            string keishou1 = "御中";
            // 敬称2
            string keishou2 = "御中";
            // 但し書き
            string tadashigaki = "処分料";

            string[] dummyData = new string[3];
            dummyData[0] = keishou1;
            dummyData[1] = keishou2;
            dummyData[2] = tadashigaki;

            return dummyData;
        }

        /// <summary>
        /// Cutting input string (1 byte and 2 byte) into two substrings, Ex: 領収書123
        /// </summary>
        /// <param name="input">input string</param>
        /// <param name="lenght1">number of character (1 byte character) in first line</param>
        /// <param name="str1">return string 1</param>
        /// <param name="str2">return string 2</param>
        public static void SubString(string input, int lenght1, ref string str1, ref string str2)
        {
            if (input.Length > 0)
            {
                byte[] byteArray;
                Encoding encoding = Encoding.GetEncoding("Shift_JIS");
                int iLenght = 0;
                for (int i = 0; i < input.Length; i++)
                {                    
                    byteArray = encoding.GetBytes(input[i].ToString());
                    if (byteArray.Length > 1) //2 byte character
                    {
                        if (iLenght < lenght1) //line 1
                        {
                            iLenght += 2;
                            str1 += input[i];
                        }
                        else //line 2
                        {
                            str2 += input[i];
                        }
                    }
                    else //1 byte character
                    {
                        if (iLenght < lenght1) //line 1
                        {
                            iLenght += 1;
                            str1 += input[i];
                        }
                        else //line 2
                        {
                            str2 += input[i];
                        }
                    }
                }
            }
            else
            {
                str1 = string.Empty;
                str2 = string.Empty;
            }
        }
    }
}
