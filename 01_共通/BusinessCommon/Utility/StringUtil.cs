using System;
using r_framework.Utility;
using System.Windows.Forms;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;
using System.Reflection;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    public class StringUtil
    {
        #region "20150714 CongBinh 追加"
        /// <summary>
        /// 値
        /// </summary>
        private const string Value = "Value";

        /// <summary>
        /// テキスト
        /// </summary>
        private const string Text = "Text";

        /// <summary>
        /// Endcoding
        /// </summary>
        private const string Endcoding = "Shift_JIS";

        /// <summary>
        /// Regex
        /// </summary>
        private const string Regex = "[0-9A-Za-z ]+";

        #endregion
        /// <summary>
        /// 変換する前に各バターンチェック
        /// </summary>
        /// <param name="prmValue">渡すパラメータがＮＵＬＬだったら戻り値はブランクになります。</param>
        /// <returns></returns>
        public static string ConverToString(object prmValue)
        {
            string sValues = null;

            //入力値がNULLの場合 
            if (prmValue == null)
            {
                return string.Empty;
            }

            object value = null;
            object[] index = null;
            PropertyInfo pi;

            //入力するオブジェクトがMultirowのセル又はDataGridViewのセルの場合
            #region "CongBinh 20150714 修正"
            //if (TryGetInfo(prmValue, "Value", out pi))
            if (PropertyUtility.TryGetInfo(prmValue, Value, out pi))
            #endregion
            {
                value = pi.GetValue(prmValue, index);
            }
            //入力するオブジェクトがコンポーネントの場合
            #region "CongBinh 20150714 修正"
            //else if (TryGetInfo(prmValue, "Text", out pi))
            else if (PropertyUtility.TryGetInfo(prmValue, Text, out pi))
            #endregion
            {
                value = pi.GetValue(prmValue, index);
            }
            //入力するオブジェクトが日付、数文字、。。。の場合
            else
            {
                value = prmValue;
            }

            //入力値がNULLではない場合
            if (value != null)
            {
                sValues = value.ToString();
            }
            //入力値がNULLの場合
            else
            {
                sValues = string.Empty;
            }
            return sValues;
        }

        /// <summary>
        /// 2byteと1byteを判断してカットする
        /// </summary>
        /// <param name="prmValue">半角OR全角英数字</param>
        /// <param name="prmLength">必要なバイト数</param>
        /// <returns>必要なバイト数の文字列</returns>
        public static string SubString(String prmValue, int prmLength)
        {
            #region "CongBinh 20150714 削除"
            //char[] array = ConverToString(prmValue).ToCharArray();
            #endregion

            byte[] byteArray;
            string sValue = "";
            Encoding encoding = Encoding.GetEncoding(Endcoding);

            //入力値が空文字ではない場合
            #region "CongBinh 20150714 修正"
            //if (array.Length > 0)
            if (!String.IsNullOrEmpty(prmValue))
            #endregion
            {
                byteArray = encoding.GetBytes(prmValue);

                //入力値の長さは入力する長さより長い場合
                if (byteArray.Length > prmLength)
                {
                    sValue = encoding.GetString(byteArray, 0, prmLength);
                }
                else
                {
                    sValue = prmValue;
                }
            }
            return sValue;
        }

        /// <summary>
        /// 2byteと1byteを判断してカットする
        /// </summary>
        /// <param name="prmValue">半角英数字</param>
        /// <returns>全角英数字</returns>
        public static string ConvertOneByteToTwoByte(String prmValue)
        {
            LogUtility.DebugMethodStart(prmValue);

            Regex re = new Regex(Regex);
            string output = re.Replace(prmValue, MyReplacer);

            LogUtility.DebugMethodEnd(output);
            return output;
        }

        // <summary>
        /// 全角の英数字の文字列を半角に変換する
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        static string MyReplacer(Match m)
        {
            LogUtility.DebugMethodStart(m);

            string res = Strings.StrConv(m.Value, VbStrConv.Wide, 0);

            LogUtility.DebugMethodEnd(res);
            return res;
        }

        /// <summary>
        /// 半角英数字が含まれているかどうか判定
        /// </summary>
        /// <param name="val"></param>
        /// <returns>true:含まれる。false:含まれない。(引数がNullまたはEmptyの場合もfalse)</returns>
        public static bool ContainsHankakuAlphaNum(string val)
        {
            // 判定対象が無い場合はfalse
            if (string.IsNullOrEmpty(val)) return false;

            Regex re = new Regex(Regex);
            return (re.IsMatch(val));
        }

        /// <summary>
        /// 行数と各行の長さを確定したい。
        /// </summary>
        /// <param name="prmValue">コンバート必要な文字列</param>
        /// <param name="prmLine">文字列の行数</param>
        /// <param name="prmLength">各行の長さ</param>
        /// <returns>既に確定した行数と各行の長さの文字列</returns>
        public static string SubStringExt(String prmValue, int prmLine, int prmLength)
        {
            #region "CongBinh 20150714 修正"
            //var arr1 = ConverToString(prmValue);
            //char[] array = arr1.ToCharArray();

            ////入力値が空文字ではない場合
            //if (array.Length > 0)
            //{
            //    prmValue = "";
            //    int iChar = 0;
            //    int iLine = 0;
            //    for (int i = 0; i < arr1.Length; i++)
            //    {
            //        prmValue += arr1[i].ToString();

            //        iChar++;
            //        if (iChar == prmLength)
            //        {
            //            iChar = 0;
            //            iLine++;
            //            if (iLine == prmLine)
            //            {
            //                break;
            //            }
            //            prmValue += Environment.NewLine;
            //        }
            //    }
            //}
            //return prmValue;

            string strTmp = string.Empty;

            //入力値が空文字ではない場合
            if (!String.IsNullOrEmpty(prmValue))
            {
                int iChar = 0;
                int iLine = 0;
                for (int i = 0; i < prmValue.Length; i++)
                {
                    strTmp += prmValue[i].ToString();

                    iChar++;
                    if (iChar == prmLength)
                    {
                        iChar = 0;
                        iLine++;
                        if (iLine == prmLine)
                        {
                            break;
                        }
                        strTmp += Environment.NewLine;
                    }
                }
            }
            return strTmp;

            #endregion
        }
    }
}
