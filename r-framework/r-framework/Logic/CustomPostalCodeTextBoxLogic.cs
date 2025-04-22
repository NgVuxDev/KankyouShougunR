using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using r_framework.Const;

namespace r_framework.Logic
{
    /// <summary>
    /// 郵便番号コントロール共通ロジッククラス
    /// </summary>
    internal static class CustomPostalCodeTextBoxLogic
    {
        private static readonly string ALLOW_CHARACTER_LIST = "-";

        /// <summary>
        /// 入力キー受理判定
        /// </summary>
        /// <param name="chr">入力文字</param>
        /// <returns>true:OK、false:NG</returns>
        internal static bool CanAcceptOnKeyPress(char chr)
        {
            return char.IsControl(chr) || char.IsNumber(chr) ||
                Constans.ALLOW_KEY_CHARS_ALLINPUT.Contains(chr) ||
                CustomPostalCodeTextBoxLogic.ALLOW_CHARACTER_LIST.Contains(chr);
        }

        /// <summary>
        /// クリップボードテキスト貼り付け受理判定
        /// </summary>
        /// <returns></returns>
        internal static bool CanAcceptClipboardText()
        {
            var iData = Clipboard.GetDataObject();
            if (iData != null && iData.GetDataPresent(DataFormats.Text))
            {
                string txt = ((string)iData.GetData(DataFormats.Text));
                if (CustomPostalCodeTextBoxLogic.IsValid(txt, true))
                {
                    Clipboard.SetData(DataFormats.Text, txt);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validating
        /// </summary>
        /// <param name="txt">入力テキスト</param>
        /// <returns>true:OK、false:NG</returns>
        /// <remarks>不正値の場合はメッセージボックス表示</remarks>
        internal static bool Validating(string txt)
        {
            if (!string.IsNullOrWhiteSpace(txt))
            {
                if (!CustomPostalCodeTextBoxLogic.IsValid(txt))
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E010", "郵便番号");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        internal static bool Parsing(object obj, out string val)
        {
            val = string.Empty;
            try
            {
                if (obj != null && !string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    // フォーマットした値取得
                    if (CustomPostalCodeTextBoxLogic.IsValid(obj.ToString()))
                    {
                        val = obj.ToString();
                        return true;
                    }
                }
            }
            catch { }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        internal static string Formatting(string txt)
        {
            string value = null;
            try
            {
                if (!string.IsNullOrWhiteSpace(txt))
                {
                    if (CustomPostalCodeTextBoxLogic.IsValid(txt))
                    {
                        value = txt.Replace("-", "").Insert(3, "-");
                    }
                }
            }
            catch { }

            return value;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private static bool IsValid(string txt, bool onPaste = false)
        {
            // 空
            if (string.IsNullOrWhiteSpace(txt))
            {
                return true;
            }

            // 正規表現と一致
            if (Regex.IsMatch(txt, @"^\d{3}-\d{4}$", RegexOptions.ECMAScript) ||
                Regex.IsMatch(txt, @"^\d{7}$", RegexOptions.ECMAScript) ||
                (onPaste && (Regex.IsMatch(txt, @"^\d{0,7}\r?\n?$", RegexOptions.ECMAScript) || Regex.IsMatch(txt, @"^\d{3}-\d{0,4}\r?\n?$", RegexOptions.ECMAScript))))
            {
                return true;
            }

            return false;
        }
    }
}