using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    public static class StringExtension
    {
        private const string Endcoding = "Shift_JIS";
        private static char[] SpecialCharTwoByte = new char[] { '㎡', '㎥' };
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmContent"></param>
        /// <param name="prmLenght"></param>
        /// <returns></returns>
        public static string SubStringByByte(this string prmContent, int prmLenght)
        {
            if (!String.IsNullOrEmpty(prmContent))
            {
                char[] arrayChar = prmContent.ToCharArray();
                return GetStringByLenght(arrayChar, prmLenght, 0);
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prmContent"></param>
        /// <param name="prmLenght"></param>
        /// <param name="prmLine"></param>
        /// <returns></returns>
        public static string SubStringByByte(this string prmContent, int prmLenght, int prmLine)
        {
            if (!String.IsNullOrEmpty(prmContent))
            {
                List<string> ret = new List<string>();
                while (prmContent.Length > 0 && ret.Count < prmLine)
                {
                    string temp = SubStringByByte(prmContent, prmLenght);
                    ret.Add(temp);
                    prmContent = prmContent.Substring(temp.Length);
                }
                return string.Join(Environment.NewLine, ret);
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrayChar"></param>
        /// <param name="byteMaxLenght"></param>
        /// <param name="startByteIndex"></param>
        /// <returns></returns>
        private static string GetStringByLenght(char[] arrayChar, int byteMaxLenght, int startByteIndex)
        {

            Encoding encoding = Encoding.GetEncoding(Endcoding);
            StringBuilder sb = new StringBuilder();

            int countTotalByte = 0;
            int indext = startByteIndex;
            //現在の結果文字列のバイト数は切る必要があるバイト数より少ない時
            while (countTotalByte < byteMaxLenght)
            {
                //arrayCharの次Charのバイト数をカウントする
                if (indext <= arrayChar.Length - 1)
                {
                    int temp = GetByteCountByIndex(encoding, arrayChar, indext);

                    if (countTotalByte + temp <= byteMaxLenght)
                    {
                        sb.Append(arrayChar[indext]);
                        countTotalByte += temp;
                    }
                    else
                    {
                        break;
                    }
                    indext++;
                }
                else
                {
                    break;
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="encoding"></param>
        /// <param name="arrayChar"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static int GetByteCountByIndex(Encoding encoding, char[] arrayChar, int index)
        {
            int result = 0;
            if (index <= arrayChar.Length - 1)
            {
                if (SpecialCharTwoByte.Contains(arrayChar[index]))
                    return 2;
                else
                    return encoding.GetByteCount(new char[] { arrayChar[index] });
            }
            else
            {
                return result;
            }
        }

        /// <summary>
        /// 小数点以下の数値が、0の場合小数点以下をトリム
        /// 例: 2.00 → 2
        /// 例: 4.50 → 4.5
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ZeroTorimu(this string prmValue)
        {
            if (!String.IsNullOrWhiteSpace(prmValue))
            {
                while (prmValue.Contains(".")
                    && (prmValue.LastIndexOf('.') == prmValue.Length - 1
                        || prmValue.LastIndexOf('0') == prmValue.Length - 1))
                {
                    prmValue = prmValue.Substring(0, prmValue.Length - 1);
                }
                return prmValue;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
