using System.Linq;
using System.Windows.Forms;
using r_framework.Const;

namespace r_framework.Logic
{
    /// <summary>
    /// 電話番号コントロール共通ロジッククラス
    /// </summary>
    internal static class CustomPhoneNumberTextBoxLogic
    {
        //private static readonly string ALLOW_CHARACTER_LIST = "()-+#*";
        private static readonly string ALLOW_CHARACTER_LIST = "-";
        private static readonly string ALLOW_CHARACTER_LIST_USE_PARENTHESES = "()-";

        /// <summary>
        /// 入力キー受理判定
        /// </summary>
        /// <param name="chr">入力文字</param>
        /// <param name="text">現在の値</param>
        /// <param name="caret">キャレット位置</param>
        /// <returns>true:OK、false:NG</returns>
        //internal static bool CanAcceptOnKeyPress(char chr)
        //{
        //    return char.IsControl(chr) || char.IsNumber(chr) ||
        //        Constans.ALLOW_KEY_CHARS_ALLINPUT.Contains(chr) ||
        //        CustomPhoneNumberTextBoxLogic.ALLOW_CHARACTER_LIST.Contains(chr);
        //}

        /// <summary>
        /// 入力キー受理判定
        /// </summary>
        /// <param name="chr">入力文字</param>
        /// <param name="useParentheses">括弧を使用するかのフラグ</param>
        /// <returns>true:OK、false:NG</returns>
        internal static bool CanAcceptOnKeyPress(char chr, bool useParentheses)
        {
            if (useParentheses)
            {
                return char.IsControl(chr) || char.IsNumber(chr) ||
                    Constans.ALLOW_KEY_CHARS_ALLINPUT.Contains(chr) ||
                    CustomPhoneNumberTextBoxLogic.ALLOW_CHARACTER_LIST_USE_PARENTHESES.Contains(chr);
            }
            else
            {
                return char.IsControl(chr) || char.IsNumber(chr) ||
                    Constans.ALLOW_KEY_CHARS_ALLINPUT.Contains(chr) ||
                    CustomPhoneNumberTextBoxLogic.ALLOW_CHARACTER_LIST.Contains(chr);
            }
        }

        /// <summary>
        /// クリップボードテキスト貼り付け受理判定
        /// </summary>
        /// <returns></returns>
        //internal static bool CanAcceptClipboardText()
        //{
        //    var iData = Clipboard.GetDataObject();
        //    if (iData != null && iData.GetDataPresent(DataFormats.Text))
        //    {
        //        string txt = (string)iData.GetData(DataFormats.Text);
        //        return CustomPhoneNumberTextBoxLogic.IsValid(txt, true);
        //    }
        //    return false;
        //}

        /// <summary>
        /// クリップボードテキスト貼り付け受理判定
        /// </summary>
        /// <param name="useParentheses">括弧を使用するかのフラグ</param>
        /// <returns></returns>
        internal static bool CanAcceptClipboardText(bool useParentheses)
        {
            var iData = Clipboard.GetDataObject();
            if (iData != null && iData.GetDataPresent(DataFormats.Text))
            {
                string txt = (string)iData.GetData(DataFormats.Text);
                return CustomPhoneNumberTextBoxLogic.IsValid(txt, useParentheses, true);
            }
            return false;
        }

        /// <summary>
        /// Validating
        /// </summary>
        /// <param name="txt">入力テキスト</param>
        /// <returns>true:OK、false:NG</returns>
        /// <remarks>不正値の場合はメッセージボックス表示</remarks>
        //internal static bool Validating(string txt)
        //{
        //    if (!string.IsNullOrWhiteSpace(txt))
        //    {
        //        if (!CustomPhoneNumberTextBoxLogic.IsValid(txt))
        //        {
        //            var messageShowLogic = new MessageBoxShowLogic();
        //            messageShowLogic.MessageBoxShow("E009", "電話番号");
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        /// <summary>
        /// Validating
        /// </summary>
        /// <param name="txt">入力テキスト</param>
        /// <param name="useParentheses">括弧を使用するかのフラグ</param>
        /// <returns>true:OK、false:NG</returns>
        /// <remarks>不正値の場合はメッセージボックス表示</remarks>
        internal static bool Validating(string txt, bool useParentheses)
        {
            if (!string.IsNullOrWhiteSpace(txt))
            {
                if (!CustomPhoneNumberTextBoxLogic.IsValid(txt, useParentheses, false))
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E009", "電話番号");
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        //private static bool IsValid(string txt, bool onPaste = false)
        //{
        //    if (string.IsNullOrWhiteSpace(txt))
        //    {
        //        return true;
        //    }

        //    if (txt.All(c =>
        //        char.IsNumber(c) ||
        //        Constans.ALLOW_KEY_CHARS_ALLINPUT.Contains(c) ||
        //        (onPaste && Constans.ALLOW_KEY_CHARS_NEWLINE.Contains(c)) ||
        //        CustomPhoneNumberTextBoxLogic.ALLOW_CHARACTER_LIST.Contains(c)))
        //    {
        //        // 電話番号か調べる(数字は1～11桁であること、#と*は数字と見なす)
        //        var len = txt.Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", "").Length;
        //        if (0 < len && len < 12)
        //        {
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        private static bool IsValid(string txt, bool useParentheses, bool onPaste = false)
        {
            if (string.IsNullOrWhiteSpace(txt))
            {
                return true;
            }

            string allowCharacterList;
            if (useParentheses)
            {
                allowCharacterList = CustomPhoneNumberTextBoxLogic.ALLOW_CHARACTER_LIST_USE_PARENTHESES;
            }
            else
            {
                allowCharacterList = CustomPhoneNumberTextBoxLogic.ALLOW_CHARACTER_LIST;
            }

            if (txt.All(c =>
                char.IsNumber(c) ||
                Constans.ALLOW_KEY_CHARS_ALLINPUT.Contains(c) ||
                (onPaste && Constans.ALLOW_KEY_CHARS_NEWLINE.Contains(c)) ||
                allowCharacterList.Contains(c)))
            {
                // 電話番号か調べる(数字は1～11桁であること、#と*は数字と見なす)
                var len = txt.Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", "").Length;
                if (0 < len && len < 12)
                {
                    return true;
                }
            }

            return false;
        }
    }
}