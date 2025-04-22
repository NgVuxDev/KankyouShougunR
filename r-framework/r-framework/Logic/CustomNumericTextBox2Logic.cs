using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using r_framework.Dto;
using r_framework.Setting;

namespace r_framework.Logic
{
    /// <summary>
    /// カスタム数値テキストボックスにて利用されるロジックを
    /// まとめているロジッククラス
    /// </summary>
    internal static class CustomNumericTextBox2Logic
    {
        private static readonly string NUMERIC_CUSTOM_FORMAT_SETTING_PATTERN = @"^(#|0|#,###|#,##0)(\.(0+#*|#+))?$";

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatSetting"></param>
        /// <returns></returns>
        private static FormatSettingDto GetFormatSetting(string formatSetting)
        {
            return string.IsNullOrWhiteSpace(formatSetting) ? null : new FormatSetting().GetSetting(formatSetting);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatSetting"></param>
        /// <param name="customFormatSetting"></param>
        /// <param name="fmt"></param>
        private static void GetFormat(string formatSetting, string customFormatSetting, out string fmt)
        {
            FormatSettingDto formatSettingDto;
            CustomNumericTextBox2Logic.GetFormat(formatSetting, customFormatSetting, out formatSettingDto, out fmt);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatSetting"></param>
        /// <param name="customFormatSetting"></param>
        /// <param name="formatSettingDto"></param>
        /// <param name="fmt"></param>
        private static void GetFormat(string formatSetting, string customFormatSetting, out FormatSettingDto formatSettingDto, out string fmt)
        {
            formatSettingDto = CustomNumericTextBox2Logic.GetFormatSetting(formatSetting);
            if (formatSettingDto == null)
            {
                fmt = string.Empty;
            }
            else
            {
                switch (formatSettingDto.FormatType)
                {
                    case FORMAT_TYPE.Numeric:
                        fmt = formatSettingDto.Format;
                        break;

                    case FORMAT_TYPE.SysInfo:
                        if (!CustomTextBoxLogic.TryGetFormatBySystemProperty(formatSettingDto, out fmt))
                            fmt = string.Empty;
                        break;

                    case FORMAT_TYPE.Custom:
                        fmt = customFormatSetting ?? string.Empty;
                        break;

                    default:
                        fmt = string.Empty;
                        break;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="customFormatSetting"></param>
        /// <returns></returns>
        internal static bool CheckNumericCustomFormatSetting(string customFormatSetting)
        {
            if (!string.IsNullOrWhiteSpace(customFormatSetting))
            {
                return
                    (customFormatSetting.Length > 1 && customFormatSetting.Replace("0", "").Length == 0) || // 「^00+$」の場合、CDと扱い
                    Regex.IsMatch(customFormatSetting, NUMERIC_CUSTOM_FORMAT_SETTING_PATTERN); // 数値フォーマット
            }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatSetting"></param>
        /// <returns></returns>
        internal static bool CheckNumericFormatSetting(string formatSetting)
        {
            var dto = CustomNumericTextBox2Logic.GetFormatSetting(formatSetting);
            if (dto == null)
            {
                return false;
            }
            else if (dto.FormatType != FORMAT_TYPE.Numeric && dto.FormatType != FORMAT_TYPE.SysInfo && dto.FormatType != FORMAT_TYPE.Custom)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 最大入力文字数取得
        /// </summary>
        /// <param name="formatSetting"></param>
        /// <param name="customFormatSetting"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="useEditingFormat"></param>
        /// <returns></returns>
        internal static int GetMaxLength(string formatSetting, string customFormatSetting, decimal min, decimal max, bool useEditingFormat)
        {
            // フォーマット取得
            FormatSettingDto formatSettingDto;
            string fmt;
            CustomNumericTextBox2Logic.GetFormat(formatSetting, customFormatSetting, out formatSettingDto, out fmt);

            // 最大・最小値にフォーマットを適用する
            string minstr = CustomNumericTextBox2Logic.Formatting(formatSettingDto, fmt, min);
            string maxstr = CustomNumericTextBox2Logic.Formatting(formatSettingDto, fmt, max);

            // 数値の場合、編集中フラグを参照し、コンマを削除する。
            if (useEditingFormat)
            {
                minstr = minstr.Replace(",", "");
                maxstr = maxstr.Replace(",", "");
            }

            return Math.Max(minstr.Length, maxstr.Length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatSetting"></param>
        /// <param name="customFormatSetting"></param>
        /// <returns></returns>
        internal static bool GetZeroPaddingFlag(string formatSetting, string customFormatSetting)
        {
            // フォーマット取得
            string fmt;
            CustomNumericTextBox2Logic.GetFormat(formatSetting, customFormatSetting, out fmt);

            // 「^00+$」の場合、CDと扱い
            return fmt.Length > 1 && fmt.Replace("0", "").Length == 0;
        }

        /// <summary>
        /// 入力キーがキャンセル可能か判定
        /// </summary>
        /// <param name="chr">入力キー</param>
        /// <param name="txt">入力対象TextBoxの文字列</param>
        /// <param name="caret"></param>
        /// <param name="formatSetting"></param>
        /// <param name="customFormatSetting"></param>
        /// <param name="min">許容最小値</param>
        /// <param name="max">許容最大値</param>
        /// <param name="limits">入力制限Charリスト</param>
        /// <returns>true:キャンセル、false:キャンセルしない</returns>
        internal static bool CanAcceptOnKeyPress(char chr, string txt, int caret, string formatSetting, string customFormatSetting, decimal min, decimal max, char[] limits)
        {
            bool accept = false;

            // フォーマット取得
            string fmt;
            CustomNumericTextBox2Logic.GetFormat(formatSetting, customFormatSetting, out fmt);

            // テキスト内の小数部の桁数
            int inputFrac = CustomNumericTextBox2Logic.CountFractionalPartText(txt);
            int formatFrac = CustomNumericTextBox2Logic.CountFractionalPartText(fmt);

            // テキスト内のピリオドの位置
            int period = txt.IndexOf('.');

            if (char.IsControl(chr))
            {
                accept = true;
            }
            else if (char.IsNumber(chr))
            {
                accept = (
                    (limits == null || limits.Length == 0 || limits.Contains(chr)) &&
                    (period < 0 || caret <= period || inputFrac < formatFrac));
            }
            else if (chr == '-')
            {
                // 任意位置で"-"入力して正負数自動変換機能は必要か...な？(InputManより)
                if (min < 0)
                {
                    accept = (caret == 0 && txt.IndexOf('-') < 0);
                }
            }
            else if (chr == '.')
            {
                if (formatFrac > 0)
                {
                    accept = (period < 0);
                }
            }

            // 入力後の値を試算し、最大・最小値で制御する。
            if (accept)
            {
                if (char.IsControl(chr) && chr == 22)
                {
                    // 貼り付けの場合
                    var iData = Clipboard.GetDataObject();
                    if (iData != null && iData.GetDataPresent(DataFormats.Text))
                    {
                        txt = txt.Insert(caret, (string)iData.GetData(DataFormats.Text)).
                            // 貼り付け文字列を判定する時、改行を除外する。
                            Replace("\r", "").Replace("\n", "");
                    }
                }
                else if ((char.IsNumber(chr) || chr == '-' || chr == '.') && caret >= 0)
                {
                    // 文字入力の場合、入力後の文字列で判定。
                    txt = txt.Insert(caret, chr.ToString());
                }

                // 数値に変換する場合のみチェックする
                decimal val;
                if (decimal.TryParse(txt, out val))
                {
                    accept &= (val >= min && val <= max) ||
                        // 入力途中
                        txt.Length < CustomNumericTextBox2Logic.GetMaxLength(formatSetting, customFormatSetting, min, max, true);
                }
            }

            return accept;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        internal static bool CanParseClipboardText()
        {
            var iData = Clipboard.GetDataObject();
            if (iData != null && iData.GetDataPresent(DataFormats.Text))
            {
                string txt = (string)iData.GetData(DataFormats.Text);
                decimal val;
                if (decimal.TryParse(txt, out val))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Validating。不正値の場合はメッセージボックス表示
        /// </summary>
        /// <param name="txt">入力テキスト</param>
        /// <param name="min">許容最小値</param>
        /// <param name="max">許容最大値</param>
        /// <returns>true:OK、false:NG</returns>
        internal static bool Validating(string txt, decimal min, decimal max, char[] limits)
        {
            string messageId = null;
            string[] messageParams = null;

            if (!string.IsNullOrWhiteSpace(txt))
            {
                if (txt.Length == 1 &&
                    limits != null && limits.Length > 0 && !limits.Contains(txt[0]))
                {
                    messageId = "E084";
                    messageParams = new string[] { txt };
                }
                else
                {
                    decimal val;
                    if (!decimal.TryParse(txt, out val))
                    {
                        messageId = "E084";
                        messageParams = new string[] { txt };
                    }
                    else
                    {
                        if (val < min || val > max)
                        {
                            messageId = "W001";
                            messageParams = new string[] { min.ToString(), max.ToString() };
                        }
                    }
                }
            }

            if (messageId != null)
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow(messageId, messageParams);
                return false;
            }

            return true;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatSetting"></param>
        /// <param name="customFormatSetting"></param>
        /// <param name="obj"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        internal static bool Parsing(string formatSetting, string customFormatSetting, object obj, out decimal val)
        {
            val = 0;
            try
            {
                if (obj != null && !string.IsNullOrWhiteSpace(obj.ToString())) // DBNull.ValueはToString()で処理する
                {
                    // フォーマットした値取得
                    val = CustomNumericTextBox2Logic.GetFormatRoundedNumeric(formatSetting, customFormatSetting, Convert.ToDecimal(obj.ToString()));
                    return true;
                }
            }
            catch { }

            return false;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatSetting"></param>
        /// <param name="customFormatSetting"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        internal static string Formatting(string formatSetting, string customFormatSetting, decimal val)
        {
            // フォーマット取得
            FormatSettingDto formatSettingDto;
            string fmt;
            CustomNumericTextBox2Logic.GetFormat(formatSetting, customFormatSetting, out formatSettingDto, out fmt);

            // フォーマットする
            return CustomNumericTextBox2Logic.Formatting(formatSettingDto, fmt, val);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatSettingDto"></param>
        /// <param name="fmt"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        internal static string Formatting(FormatSettingDto formatSettingDto, string fmt, decimal val)
        {
            string txt = string.Empty;
            try
            {
                // フォーマットした値を取得
                val = CustomNumericTextBox2Logic.GetFormatRoundedNumeric(formatSettingDto, fmt, val);
                txt = val.ToString(fmt);
                if (txt.StartsWith("-.") || txt.StartsWith("."))
                {
                    // 小数且つフォーマットによって「.x」又は「-.x」のようになった場合、必ず「0.x」又は「-0.x」で表示するように。
                    txt = txt.Insert(txt.IndexOf("."), "0");
                }
            }
            catch { }

            return txt;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatSetting"></param>
        /// <param name="customFormatSetting"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private static decimal GetFormatRoundedNumeric(string formatSetting, string customFormatSetting, decimal val)
        {
            // フォーマット取得
            FormatSettingDto formatSettingDto;
            string fmt;
            CustomNumericTextBox2Logic.GetFormat(formatSetting, customFormatSetting, out formatSettingDto, out fmt);

            // フォーマットした値を戻す
            return CustomNumericTextBox2Logic.GetFormatRoundedNumeric(formatSettingDto, fmt, val);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="formatSettingDto"></param>
        /// <param name="fmt"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        private static decimal GetFormatRoundedNumeric(FormatSettingDto formatSettingDto, string fmt, decimal val)
        {
            if (formatSettingDto == null)
            {
                return val;
            }

            // CustomTextBoxLogic.Formatなど参照
            int formatFrac = CustomNumericTextBox2Logic.CountFractionalPartText(fmt);
            switch (formatSettingDto.FormatType)
            {
                case Dto.FORMAT_TYPE.SysInfo:
                    if (formatSettingDto.ColumnName.Equals("MANIFEST_SUURYO_FORMAT"))
                    {
                        // 端数処理(四捨五入)
                        // マニ数量対して特殊処理
                        return CustomNumericTextBox2Logic.Round(val, formatFrac, ROUNDING_TYPE.AwayFromZero);
                    }
                    else
                    {
                        // 端数処理
                        return CustomNumericTextBox2Logic.Round(val, formatFrac, ROUNDING_TYPE.Floor);
                    }

                case Dto.FORMAT_TYPE.Numeric:
                case Dto.FORMAT_TYPE.Custom:
                    // 端数処理
                    return CustomNumericTextBox2Logic.Round(val, formatFrac, ROUNDING_TYPE.Floor);

                case FORMAT_TYPE.Expression:
                default:
                    return val;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="val"></param>
        /// <param name="frac"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private static decimal Round(decimal val, int frac, ROUNDING_TYPE type)
        {
            switch (type)
            {
                case ROUNDING_TYPE.Ceiling:
                    decimal ceiling = 1M;
                    for (int i = 0; i < frac; i++)
                    {
                        ceiling *= 10M;
                    }

                    val *= ceiling;
                    return (val >= 0 ? Math.Ceiling(val) : Math.Floor(val)) / ceiling;
                case ROUNDING_TYPE.Floor:
                    decimal floor = 1M;
                    for (int i = 0; i < frac; i++)
                    {
                        floor *= 10M;
                    }

                    val *= floor;
                    return (val >= 0 ? Math.Floor(val) : Math.Ceiling(val)) / floor;
                case ROUNDING_TYPE.ToEven:
                    return Math.Round(val, frac, MidpointRounding.ToEven);

                case ROUNDING_TYPE.AwayFromZero:
                    return Math.Round(val, frac, MidpointRounding.AwayFromZero);

                default:
                    return val;
            }
        }

        /// <summary>
        /// 数値文字列の小数部(ピリオドより後ろ)の文字数を数える
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        private static int CountFractionalPartText(string txt)
        {
            int count = 0;
            if (!string.IsNullOrWhiteSpace(txt))
            {
                int index = txt.LastIndexOf('.');
                if (index >= 0)
                {
                    count = txt.Length - index - 1;
                }
            }
            return count;
        }

        private enum ROUNDING_TYPE
        {
            Ceiling,
            Floor,
            ToEven,
            AwayFromZero
        }
    }
}