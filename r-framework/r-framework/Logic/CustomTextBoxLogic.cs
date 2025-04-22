using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using Microsoft.VisualBasic;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dto;
using r_framework.Setting;
using r_framework.Utility;
using System.Reflection;

namespace r_framework.Logic
{
    /// <summary>
    /// カスタムテキストボックス用の共通処理を纏めたクラス
    /// </summary>
    public class CustomTextBoxLogic
    {
        /// <summary>
        /// カスタムテキストボックスのインタフェース
        /// </summary>
        private ICustomTextBox _customText;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="customText"></param>
        public CustomTextBoxLogic(ICustomTextBox customText)
        {
            this._customText = customText;
        }

        /// <summary>
        /// カスタムテキストボックスインタフェース取得
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="customCtrl"></param>
        /// <param name="customTextCtrl"></param>
        /// <returns></returns>
        internal static bool TryGetCustomTextCtrl(object obj, out ICustomControl customCtrl, out ICustomTextBox customTextCtrl)
        {
            customTextCtrl = null;
            if (!CustomControlLogic.TryGetCustomCtrl(obj, out customCtrl))
            {
                return false;
            }

            customTextCtrl = obj as ICustomTextBox;
            return (customTextCtrl != null);
        }

        /// <summary>
        /// ゼロ埋め処理
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        internal bool ZeroPadding(object source)
        {
            if (!this._customText.ZeroPaddengFlag)
            {
                return false;
            }

            Decimal charactersNumber;
            object obj;
            if (!PropertyUtility.GetValue(source, Constans.CHARACTERS_NUMBER, out obj))
            {
                return false;
            }
            charactersNumber = (Decimal)obj;

            if (charactersNumber == 0 || source == null)
            {
                return false;
            }

            string text;
            //編集コントロールの場合チェックが必要
            if (source is DataGridViewCell && ((DataGridViewCell)source).IsInEditMode)
            {
                var cell = (DataGridViewCell)source;
                //編集中の場合
                text = cell.DataGridView.EditingControl.Text;
            }
            else
            {
                text = PropertyUtility.GetTextOrValue(source);
            }

            var byteData = System.Text.Encoding.UTF8.GetByteCount(text);
            var strCharactersUmber = obj.ToString();

            if (strCharactersUmber.Contains("."))
            {
                // 小数ありの場合はゼロパディングしない
                return false;
            }

            if (charactersNumber <= byteData)
            {
                return true;
            }

            if (!(Regex.Match(text, "^[a-zA-Z0-9]+$")).Success)
            {
                return false;
            }

            // フォーマット指定可能な場合の設定
            StringBuilder sb = new StringBuilder((int)charactersNumber);
            var format = sb.Append('0', (int)charactersNumber).ToString();
            var result = SettingCellFormat(source, format);

            var padData = text.PadLeft((int)charactersNumber, '0');

            if (result)
            {
            }
            else
            {
                result = PropertyUtility.SetTextOrValue(source, padData);
            }

            //編集コントロールの場合チェックが必要
            if (source is DataGridViewCell && ((DataGridViewCell)source).IsInEditMode)
            {
                var cell = (DataGridViewCell)source;
                //編集中の場合
                cell.DataGridView.EditingControl.Text = padData;
            }
            else if (source is Cell && ((Cell)source).IsInEditMode)
            {
                var cell = (Cell)source;
                //編集中の場合
                cell.GcMultiRow.EditingControl.Text = padData;
            }

            return result;
        }

        /// <summary>
        /// フリガナ設定処理
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fields"></param>
        internal void SettingFurigana(object source, object[] fields)
        {
            var setFields = ControlUtility.CreateFields(fields, this._customText.FuriganaAutoSetControl);
            string text = PropertyUtility.GetTextOrValue(source);
            if (setFields == null || setFields.Length == 0)
            {
                return;
            }
            text = this.ReplaceFuriganaData(text);

            string furigana = string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                using (var ime = new ImeLanguageLogic())
                {
                    furigana = ime.GetFuriganaWide(text);
                }
            }

            bool setFlag;
            foreach (var field in setFields)
            {
                //furigana = this.StringCut(furigana, field);
                if (string.IsNullOrEmpty(text))
                {
                    setFlag = PropertyUtility.SetTextOrValue(field, string.Empty);
                }
                else
                {
                    setFlag = PropertyUtility.SetTextOrValue(field, furigana);
                }

                // 値が設定された項目に対し、バイト数以降を切り捨てる
                if (setFlag)
                {
                    this.MaxByteCheckAndCut(field);
                }
            }
        }

        /// <summary>
        /// フリガナ設定処理
        /// 1次マスタのフレームワークより
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="fields"></param>
        /// <param name="furigana"></param>
        internal void SettingFuriganaPhase1(object source, string target, object[] fields, string furigana)
        {
            var setFields = ControlUtility.CreateFields(fields, target);
            if (setFields == null || setFields.Length == 0)
            {
                return;
            }
            foreach (var field in setFields)
            {
                if (field is TextBoxBase && (field as TextBoxBase).ReadOnly)
                {
                    // 読取専用テキストボックスにはフリガナを設定しない
                    continue;
                }

                bool setFlag = false;
                if (string.IsNullOrEmpty(furigana))
                {
                    PropertyUtility.SetTextOrValue(field, string.Empty);
                }
                else
                {
                    string text = PropertyUtility.GetTextOrValue(field) + furigana;
                    text = this.ReplaceFuriganaData(text);

                    // 入力可能な記号に置き換え、入力不可文字を削除する
                    text = text.Replace("＇", "’").Replace("、", "，").Replace("。", "．");
                    text = Regex.Replace(text, @"[^　|^(０-９)|^(ａ-ｚ)|^(Ａ-Ｚ)|^＆|^’|^，|^‐|^－|^ー|^．|^・|^(\u30A1-\u30F6)]", "");

                    setFlag = PropertyUtility.SetTextOrValue(field, text);
                }

                // 値が設定された項目に対し、バイト数以降を切り捨てる
                if (setFlag)
                {
                    this.MaxByteCheckAndCut(field);
                }
            }
        }

        /// <summary>
        /// 複写設定処理
        /// </summary>
        /// <param name="source"></param>
        /// <param name="fields"></param>
        internal void SettingCopyValue(object source, object[] fields)
        {
            var setFields = ControlUtility.CreateFields(fields, this._customText.CopyAutoSetControl);
            
            string text = string.Empty;
            PropertyInfo pi = null;
            if (PropertyUtility.TryGetInfo(source, "CopyAutoSetWithSpace", out pi) && (bool)pi.GetValue(source, null))
            {
                // 略称など複写でスペースを削除しない場合
                text = PropertyUtility.GetTextOrValueNotTrimSpace(source);
            }
            else
            {
                // 略称など複写でスペースを削除する場合
                text = PropertyUtility.GetTextOrValue(source);
            }

            if (setFields == null || setFields.Length == 0)
            {
                return;
            }
            text = this.ReplaceCopyData(text);

            bool setFlag;
            foreach (var field in setFields)
            {
                //text = this.StringCut(text, field);
                if (string.IsNullOrEmpty(text))
                {
                    setFlag = PropertyUtility.SetTextOrValue(field, string.Empty);
                }
                else
                {
                    setFlag = PropertyUtility.SetTextOrValue(field, text);
                }

                // 値が設定された項目に対し、バイト数以降を切り捨てる
                if (setFlag)
                {
                    this.MaxByteCheckAndCut(field);
                }
            }
        }

        #region 2013/09/04 削除

        ///// <summary>
        ///// 文字列のカットを行う
        ///// </summary>
        ///// <param name="source"></param>
        ///// <param name="fields"></param>
        //internal string StringCut(string text, object setControl)
        //{
        //    var maxLength = PropertyUtility.GetString(setControl, "CharactersNumber");

        //    int length;
        //    if (Int32.TryParse(maxLength, out length))
        //    {
        //        if (text.Length < length)
        //        {
        //            return text;
        //        }

        //        return text.Substring(0, length);
        //    }
        //    return text;
        //}

        #endregion 2013/09/04 削除

        /// <summary>
        /// フリガナ項目へ設定するときに特定の値を削除する
        /// </summary>
        /// <param name="str">削除対象の文字列</param>
        /// <returns>削除後の文字列</returns>
        internal string ReplaceFuriganaData(string str)
        {
            var replaceStr = str;
            foreach (var deleteStr in Constans.DELETE_FURIGANA_STR)
            {
                replaceStr = replaceStr.Replace(deleteStr, "");
            }
            return replaceStr;
        }

        /// <summary>
        /// 複写項目へ設定するときに特定の値を削除する
        /// </summary>
        /// <param name="str">削除対象の文字列</param>
        /// <returns>削除後の文字列</returns>
        internal string ReplaceCopyData(string str)
        {
            var replaceStr = str;
            foreach (var deleteStr in Constans.DELETE_COPY_STR)
            {
                replaceStr = Regex.Replace(replaceStr, deleteStr + "[ |　]*", "");
            }
            return replaceStr;
        }

        /// <summary>
        /// フォーマット処理
        /// </summary>
        /// <param name="source">値を取得/設定するコントロール</param>
        /// <returns>結果</returns>
        public bool Format(object source)
        {
            string strValue;

            if (source is DataGridViewCell && ((DataGridViewCell)source).IsInEditMode)
            {
                var cell = (DataGridViewCell)source;
                strValue = cell.DataGridView.EditingControl.Text;
            }
            else
            {
                strValue = PropertyUtility.GetTextOrValue(source);
            }

            if (string.IsNullOrEmpty(strValue))
            {
                return false;
            }

            // フォーマット処理
            if (CustomTextBoxLogic.CheckNotStringTypeCell(source))
            {
                // コントロールが文字列型以外のセルの場合
                // 編集した文字列を設定することが出来ないので
                // Style.Formatプロパティを使用して設定を行う。
                return this.GetFormattedStringForCell(source, strValue);
            }
            else
            {
                // コントロールがセル以外
                // もしくは文字列型のセルの場合
                // 直接編集した文字列を設定する。
                if (!this.GetFormattedString(strValue, out strValue))
                {
                    return false;
                }

                return PropertyUtility.SetTextOrValue(source, strValue);
            }
        }

        /// <summary>
        /// フォーマット処理(DataGridVIew用)
        /// </summary>
        /// <param name="source">値を取得/設定するコントロール</param>
        /// <returns>結果</returns>
        internal bool Format(object source, DataGridViewCellFormattingEventArgs e)
        {
            string strValue;

            if (e.Value != null && !(e.Value is DBNull))
            {
                strValue = e.Value.ToString();
            }
            else
            {
                strValue = "";
            }

            //if (source is DataGridViewCell && ((DataGridViewCell)source).IsInEditMode)
            //{
            //    var cell = (DataGridViewCell)source;
            //    strValue = cell.DataGridView.EditingControl.Text;
            //}
            //else
            //{
            //    strValue = PropertyUtility.GetTextOrValue(source);
            //}

            if (string.IsNullOrEmpty(strValue))
            {
                return false;
            }

            // フォーマット処理
            if (CustomTextBoxLogic.CheckNotStringTypeCell(source))
            {
                // コントロールが文字列型以外のセルの場合
                // 編集した文字列を設定することが出来ないので
                // Style.Formatプロパティを使用して設定を行う。
                bool ret = this.GetFormattedStringForCell(source, strValue);

                // 20151209 元数値項目削除 Start
                ////数値セルに文字型がバインドされた場合の特殊措置
                //var cell = source as DgvCustomNumericTextBoxCell;
                //if (cell != null && cell.Value is string && "カスタム".Equals(cell.FormatSetting))
                //{
                //    decimal dec;
                //    if (decimal.TryParse(strValue, System.Globalization.NumberStyles.Any, null, out dec))
                //    {
                //        e.Value = dec.ToString(cell.CustomFormatSetting);
                //        e.FormattingApplied = true;
                //    }
                //}
                // 20151209 元数値項目削除 End
                return ret;
            }
            else
            {
                // コントロールがセル以外
                // もしくは文字列型のセルの場合
                // 直接編集した文字列を設定する。
                if (!this.GetFormattedString(strValue, out strValue))
                {
                    return false;
                }

                return PropertyUtility.SetTextOrValue(source, strValue);
            }
        }

        /// <summary>
        /// フォーマット処理(MultiRow専用)
        /// </summary>
        /// <param name="source">値を取得/設定するコントロール</param>
        /// <returns>結果</returns>
        internal bool Format(Cell source, CellFormattingEventArgs e)
        {
            //セルの valueを参照しようとするとスタックオーバーフローが起きるので注意が必要

            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return false;
            }

            // フォーマット処理
            var txt = source as ICustomTextBox;
            if (txt == null) return false;

            // フォーマット処理
            // DataTableは現状DataGridView呼出時のみ対応

            var num = new Decimal();
            var value = e.Value.ToString();
            var dto = (new FormatSetting()).GetSetting(txt.FormatSetting);
            var result = "";

            switch (dto.FormatType)
            {
                case Dto.FORMAT_TYPE.Expression:

                    // 正規表現によるフォーマット
                    result = System.Text.RegularExpressions.Regex.Replace(value, dto.Expression, dto.Format);
                    break;

                case Dto.FORMAT_TYPE.Numeric:

                    // 数値に適用するフォーマット
                    if (Decimal.TryParse(value, out num))
                    {
                        // 端数処理
                        num = CustomTextBoxLogic.Rounding(num, dto.Format);

                        // 数値フォーマット処理
                        var format = "{0:" + dto.Format + "}";
                        result = String.Format(format, num);
                    }
                    break;

                case Dto.FORMAT_TYPE.SysInfo:

                    // DB参照による数値フォーマット
                    if (Decimal.TryParse(value, out num))
                    {
                        string format;
                        if (CustomTextBoxLogic.TryGetFormatBySystemProperty(dto, out format))
                        {
                            if (dto.ColumnName.Equals("MANIFEST_SUURYO_FORMAT"))
                            {
                                // 端数処理(四捨五入)
                                // 四捨五入や切り上げ、切捨てを画面側でコントロールしたい場合は、CustomControlにプロパティを追加する必要有
                                num = CustomTextBoxLogic.Round(num, format);
                            }
                            else
                            {
                                // 端数処理
                                num = CustomTextBoxLogic.Rounding(num, format);
                            }

                            // 数値フォーマット処理
                            format = "{0:" + format + "}";
                            result = String.Format(format, num);
                        }
                    }
                    break;

                case Dto.FORMAT_TYPE.Custom:

                    // 数値に適用するフォーマット
                    if (Decimal.TryParse(value, out num))
                    {
                        // 端数処理
                        num = CustomTextBoxLogic.Rounding(num, _customText.CustomFormatSetting);

                        // 数値フォーマット処理
                        //var format = "{0:" + _customText.CustomFormatSetting + "}";
                        //result = String.Format(format, num);
                        result = num.ToString(_customText.CustomFormatSetting);
                    }
                    break;

                //default:
                //    return false;
            }

            e.Value = result;
            return true;
        }

        /// <summary>
        /// パース処理（整形テキスト→数値）(MultiRow専用)
        /// </summary>
        /// <param name="source">値を取得/設定するコントロール</param>
        /// <returns>結果</returns>
        internal bool Parse(Cell source, CellParsingEventArgs e)
        {
            //セルの valueを参照しようとするとスタックオーバーフローが起きるので注意が必要

            if (e.Value == null || string.IsNullOrEmpty(e.Value.ToString()))
            {
                return false;
            }

            // フォーマット処理
            var txt = source as ICustomTextBox;
            if (txt == null) return false;

            // フォーマット処理
            // DataTableは現状DataGridView呼出時のみ対応

            var num = new Decimal();
            var value = e.Value.ToString();
            var dto = (new FormatSetting()).GetSetting(txt.FormatSetting);

            switch (dto.FormatType)
            {
                case Dto.FORMAT_TYPE.Expression:

                    // 正規表現によるフォーマット
                    //result = System.Text.RegularExpressions.Regex.Replace(value, dto.Expression, dto.Format);
                    //正規表現は逆は不可
                    return false;

                default:

                    // 汎用数値変換（HEX以外はいろいろ変換）
                    if (Decimal.TryParse(value, System.Globalization.NumberStyles.Any, null, out num))
                    {
                        // 端数処理
                        var format = String.Empty;
                        // システム設定から書式を取得して処理する桁数を決める
                        CustomTextBoxLogic.TryGetFormatBySystemProperty(dto, out format);
                        dto.Format = format;
                        num = CustomTextBoxLogic.Rounding(num, dto.Format);
                        e.Value = num;
                    }
                    return true;
            }
        }

        /// <summary>
        /// フォーマット処理
        /// </summary>
        /// <param name="value">元の値</param>
        /// <param name="table">マスタデータ</param>
        /// <param name="result">フォーマット済の値</param>
        /// <returns>結果</returns>
        internal bool GetFormattedString(string value, out string result)
        {
            result = value;
            var setting = this._customText.FormatSetting;
            if (string.IsNullOrEmpty(setting))
            {
                return false;
            }

            var dto = (new FormatSetting()).GetSetting(setting);
            if (dto == null)
            {
                return false;
            }

            // フォーマット処理
            // DataTableは現状DataGridView呼出時のみ対応
            var num = new Decimal();
            switch (dto.FormatType)
            {
                case Dto.FORMAT_TYPE.Expression:

                    // 正規表現によるフォーマット
                    result = System.Text.RegularExpressions.Regex.Replace(result, dto.Expression, dto.Format);
                    break;

                case Dto.FORMAT_TYPE.Numeric:

                    // 数値に適用するフォーマット
                    if (Decimal.TryParse(result, out num))
                    {
                        // 端数処理
                        num = CustomTextBoxLogic.Rounding(num, dto.Format);

                        // 数値フォーマット処理
                        var format = "{0:" + dto.Format + "}";
                        result = String.Format(format, num);
                    }
                    break;

                case Dto.FORMAT_TYPE.SysInfo:

                    // DB参照による数値フォーマット
                    if (Decimal.TryParse(result, out num))
                    {
                        string format;
                        if (CustomTextBoxLogic.TryGetFormatBySystemProperty(dto, out format))
                        {
                            if (dto.ColumnName.Equals("MANIFEST_SUURYO_FORMAT"))
                            {
                                // 端数処理(四捨五入)
                                // 四捨五入や切り上げ、切捨てを画面側でコントロールしたい場合は、CustomControlにプロパティを追加する必要有
                                num = CustomTextBoxLogic.Round(num, format);
                            }
                            else
                            {
                                // 端数処理
                                num = CustomTextBoxLogic.Rounding(num, format);
                            }

                            // 数値フォーマット処理
                            format = "{0:" + format + "}";
                            result = String.Format(format, num);
                        }
                    }
                    break;

                case Dto.FORMAT_TYPE.Custom:

                    // 数値に適用するフォーマット
                    if (Decimal.TryParse(result, out num))
                    {
                        // 端数処理
                        num = CustomTextBoxLogic.Rounding(num, _customText.CustomFormatSetting);

                        // 数値フォーマット処理
                        var format = "{0:" + _customText.CustomFormatSetting + "}";
                        result = String.Format(format, num);
                    }
                    break;

                default:
                    return false;
            }

            return true;
        }

        /// <summary>
        /// フォーマット処理（数値型セル用）
        /// </summary>
        /// <param name="source">設定対象のコントロール</param>
        /// <param name="value">元の値</param>
        /// <returns>結果</returns>
        internal bool GetFormattedStringForCell(object source, string value)
        {
            var result = false;
            //var setting = this._customText.FormatSetting;
            string customFormat = String.Empty;
            string setting = null;
            if (source is System.Windows.Forms.DataGridViewCell)
            {
                var cell = source as System.Windows.Forms.DataGridViewCell;
                var column = cell.OwningColumn as r_framework.CustomControl.DgvCustomTextBoxColumn;
                if (column == null) //コンボもここを通るため、その対策。
                {
                    return result;
                }
                setting = column.FormatSetting;
                customFormat = column.CustomFormatSetting;
            }
            else if (source is GrapeCity.Win.MultiRow.Cell)
            {
                var cell = source as r_framework.CustomControl.GcCustomTextBoxCell;
                if (cell == null)
                {
                    return result;
                }
                setting = cell.FormatSetting;
                customFormat = cell.CustomFormatSetting;
            }
            else if (source is GrapeCity.Win.MultiRow.TextBoxEditingControl)
            {
                var cell = source as GrapeCity.Win.MultiRow.TextBoxEditingControl;
                var column1 = cell.GcMultiRow;
                var curr = column1.CurrentCell as r_framework.CustomControl.GcCustomTextBoxCell;
                setting = curr.FormatSetting;
                customFormat = curr.CustomFormatSetting;
            }
            else
            {
                setting = this._customText.FormatSetting;
                customFormat = _customText.CustomFormatSetting;
            }

            if (string.IsNullOrEmpty(setting))
            {
                return result;
            }

            var dto = (new FormatSetting()).GetSetting(setting);
            if (dto == null)
            {
                return result;
            }

            // フォーマット処理
            // DataTableは現状DataGridView呼出時のみ対応
            var num = new Decimal();
            switch (dto.FormatType)
            {
                case Dto.FORMAT_TYPE.Expression:
                    // 正規表現によるフォーマット
                    string val = System.Text.RegularExpressions.Regex.Replace(value, dto.Expression, dto.Format);
                    result = CustomTextBoxLogic.SetValueAndFormat(source, val, dto.Format);
                    break;

                case Dto.FORMAT_TYPE.Numeric:

                    // 数値に適用するフォーマット
                    if (Decimal.TryParse(value, out num))
                    {
                        // 端数処理(切捨て)
                        num = CustomTextBoxLogic.Rounding(num, dto.Format);

                        // 数値フォーマット処理
                        result = CustomTextBoxLogic.SetValueAndFormat(source, num, dto.Format);
                    }
                    break;

                case Dto.FORMAT_TYPE.SysInfo:
                    if (Decimal.TryParse(value, out num))
                    {
                        string format;
                        if (CustomTextBoxLogic.TryGetFormatBySystemProperty(dto, out format))
                        {
                            if (dto.ColumnName.Equals("MANIFEST_SUURYO_FORMAT"))
                            {
                                // 端数処理(四捨五入)
                                // 四捨五入や切り上げ、切捨てを画面側でコントロールしたい場合は、CustomControlにプロパティを追加する必要有
                                num = CustomTextBoxLogic.Round(num, format);
                            }
                            else
                            {
                                // 端数処理(切捨て)
                                num = CustomTextBoxLogic.Rounding(num, format);
                            }

                            // 数値フォーマット処理
                            result = CustomTextBoxLogic.SetValueAndFormat(source, num, format);
                        }
                    }
                    break;

                case Dto.FORMAT_TYPE.Custom:

                    // 数値に適用するフォーマット
                    if (Decimal.TryParse(value, out num))
                    {
                        // 端数処理
                        // num = CustomTextBoxLogic.Rounding(num, _customText.CustomFormatSetting);
                        num = CustomTextBoxLogic.Rounding(num, customFormat);

                        // 数値フォーマット処理
                        //var format = "{0:" + _customText.CustomFormatSetting + "}";
                        //var format = "{0:" + customFormat + "}";
                        //result = CustomTextBoxLogic.SetValueAndFormat(source, String.Format(format, num), format);

                        var format = customFormat;
                        result = CustomTextBoxLogic.SetValueAndFormat(source, num, format);// formatして数値型セルに値セットすると落ちるので numのまま渡す
                    }
                    break;

                default:
                    return result;
            }

            return result;
        }

        /// <summary>
        /// バイト数チェック
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        internal void MaxByteCheckAndCut(object source)
        {
            object maxByte;
            if (!PropertyUtility.GetValue(source, Constans.CHARACTERS_NUMBER, out maxByte))
            {
                return;
            }
            var strLength = maxByte.ToString();

            string resultText;

            if (source is DataGridViewCell && ((DataGridViewCell)source).IsInEditMode)
            {
                var cell = (DataGridViewCell)source;
                resultText = cell.DataGridView.EditingControl.Text;
            }
            else
            {
                resultText = PropertyUtility.GetTextOrValueNotTrimSpace(source);
            }

            if (string.IsNullOrEmpty(resultText))
            {
                // チェックできない状態の場合は何もしない
                return;
            }

            if (!strLength.Contains("."))
            {
                int charactersNumberValue = 0;
                if (!int.TryParse(strLength, out charactersNumberValue) || charactersNumberValue < 1)
                {
                    // CharactersNumberの指定が不正な場合は何もしない
                    return;
                }

                System.Text.Encoding hEncoding = System.Text.Encoding.GetEncoding("Shift_JIS");
                byte[] bytes = hEncoding.GetBytes(resultText);
                int maxByteCount = int.Parse(strLength);
                if (bytes.Length <= maxByteCount)
                {
                    return;
                }

                string result = resultText.Substring(0, hEncoding.GetString(bytes, 0, maxByteCount).Length);

                while (maxByteCount < hEncoding.GetByteCount(result))
                {
                    result = result.Substring(0, result.Length - 1);
                }

                if (source is DataGridViewCell && ((DataGridViewCell)source).IsInEditMode)
                {
                    var cell = (DataGridViewCell)source;
                    cell.DataGridView.EditingControl.Text = result;
                }

                // 20151209 元数値項目削除 Start
                ////データバインドエラー対策（フォーマット指定があって　maxlengthチェックに引っかかると ここを通る）
                //if (source is DgvCustomNumericTextBoxCell)
                //{
                //    var cell = (DgvCustomNumericTextBoxCell)source;
                //    if (cell.OwningColumn.IsDataBound)
                //    {
                //        //数値型でvalueはセットする
                //        decimal dec;
                //        if (decimal.TryParse(result, System.Globalization.NumberStyles.Any, null, out dec))
                //        {
                //            PropertyUtility.SetTextOrValue(source, dec);
                //            return;
                //        }
                //    }
                //}
                //if (source is GcCustomNumericTextBoxCell)
                //{
                //    var cell = (GcCustomNumericTextBoxCell)source;
                //    if (cell.IsDataBound)
                //    {
                //        //数値型でvalueはセットする
                //        decimal dec;
                //        if (decimal.TryParse(result, System.Globalization.NumberStyles.Any, null, out dec))
                //        {
                //            PropertyUtility.SetTextOrValue(source, dec);
                //            return;
                //        }
                //    }
                //}
                // 20151209 元数値項目削除 End

                PropertyUtility.SetTextOrValue(source, result);
            }

            return;
        }

        /// <summary>
        /// 大文字変換
        /// </summary>
        /// <param name="source"></param>
        internal void ChangeUpperCase(object source)
        {
            bool changeUpperCase;
            object obj;
            if (!PropertyUtility.GetValue(source, Constans.CHANGE_UPPER_CASE, out obj))
            {
                return;
            }
            changeUpperCase = (bool)obj;

            if (!changeUpperCase)
            {
                return;
            }
            var text = PropertyUtility.GetTextOrValue(source);

            PropertyUtility.SetTextOrValue(source, text.ToUpper());

            return;
        }

        /// <summary>
        /// 全角変換
        /// </summary>
        /// <param name="source"></param>
        internal void ChangeWideCase(object source)
        {
            bool changeWideCase;
            object obj;
            if (!PropertyUtility.GetValue(source, Constans.CHANGE_WIDE_CASE, out obj))
            {
                return;
            }
            changeWideCase = (bool)obj;

            if (!changeWideCase)
            {
                return;
            }
            var text = PropertyUtility.GetTextOrValue(source);

            PropertyUtility.SetTextOrValue(source, Strings.StrConv(text, VbStrConv.Wide));

            return;
        }

        #region private

        /// <summary>
        /// フォーマット指定処理
        /// </summary>
        /// <param name="source"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private bool SettingCellFormat(object source, string format)
        {
            var result = false;

            object objValue;
            if (!PropertyUtility.GetValue(source, "Value", out objValue))
            {
                return false;
            }

            if (objValue is string)
            {
                return false;
            }

            object objStyle;
            if (PropertyUtility.GetValue(source, "Style", out objStyle))
            {
                var style = objStyle as CellStyle;
                if (style != null)
                {
                    style.Format = format;
                    result = true;
                }
                var dgvStyle = objStyle as DataGridViewCellStyle;
                if (dgvStyle != null)
                {
                    dgvStyle.Format = format;
                    result = true;
                }
            }

            return result;
        }

        /// <summary>
        /// テーブルよりフォーマットを取得
        /// （未使用。TryGetFormatBySystemProperty()に移行）
        /// </summary>
        /// <param name="dto">フォーマット設定DTO</param>
        /// <param name="table">マスタデータ</param>
        /// <param name="format">取得したフォーマット</param>
        /// <returns>結果</returns>
        private static bool TryGetFormatByTable(FormatSettingDto dto, DataTable table, out string format)
        {
            format = string.Empty;

            if (table == null)
            {
                // テーブルが取得出来なかった場合
                return false;
            }

            // レコード、列が存在する場合のみ取得
            if (0 < table.Rows.Count &&
                table.Columns.Contains(dto.ColumnName))
            {
                // 最初のレコードのデータを取得
                object temp = table.Rows[0][dto.ColumnName];
                if (temp is string)
                {
                    format = temp as string;
                }
            }

            return !string.IsNullOrEmpty(format);
        }

        /// <summary>
        /// SystemPropertyよりフォーマット取得
        /// </summary>
        /// <param name="dto">フォーマット設定Dto</param>
        /// <param name="format">取得したフォーマット</param>
        /// <returns>結果</returns>
        internal static bool TryGetFormatBySystemProperty(FormatSettingDto dto, out string format)
        {
            format = string.Empty;
            switch (dto.ColumnName)
            {
                case "SYS_TANKA_FORMAT":
                    format = SystemProperty.Format.Tanka;
                    break;

                case "SYS_JYURYOU_FORMAT":
                    format = SystemProperty.Format.Jyuryou;
                    break;

                case "SYS_SUURYOU_FORMAT":
                    format = SystemProperty.Format.Suuryou;
                    break;

                case "MANIFEST_SUURYO_FORMAT":
                    format = SystemProperty.Format.ManifestSuuryou;
                    break;

                case "ITAKU_KEIYAKU_TANKA_FORMAT":
                    format = SystemProperty.Format.ItakuKeiyakuTanka;
                    break;

                case "ITAKU_KEIYAKU_SUURYOU_FORMAT":
                    format = SystemProperty.Format.ItakuKeiyakuSuuryou;
                    break;

                default:
                    return false;
            }
            return !string.IsNullOrEmpty(format);
        }

        /// <summary>
        /// 端数処理(切り捨て)
        /// </summary>
        /// <param name="num">対象</param>
        /// <param name="format"></param>
        /// <returns>結果</returns>
        private static Decimal Rounding(Decimal num, string format)
        {
            Decimal result = num;

            // 小数点以下の桁数を取得
            var index = format.IndexOf('.');
            var decimals = 0;
            if (index <= 0 || index == format.Length - 1)
            {
                //return result;
                //整数にする必要があるので処理継続
            }
            else
            {
                decimals = format.Substring(index + 1).Length;
            }

            // 端数処理
            Decimal powValue = 1.0M;
            for (int i = 0; i < decimals; i++)
            {
                powValue = Decimal.Multiply(powValue, 10);
            }

            if (num > 0)
            {
                // 切り捨て
                result = Decimal.Divide(Decimal.Floor(Decimal.Multiply(result, powValue)), powValue);
            }
            else
            {
                result = Decimal.Divide(Decimal.Ceiling(Decimal.Multiply(result, powValue)), powValue);
            }

            return result;
        }

        /// <summary>
        /// 端数処理(四捨五入)
        /// </summary>
        /// <param name="num">対象</param>
        /// <param name="format"></param>
        /// <returns>結果</returns>
        private static Decimal Round(Decimal num, string format)
        {
            Decimal result = num;

            // 小数点以下の桁数を取得
            var index = format.IndexOf('.');
            var decimals = 0;
            if (index <= 0 || index == format.Length - 1)
            {
            }
            else
            {
                decimals = format.Substring(index + 1).Length;
            }

            result = Math.Round(num, decimals, MidpointRounding.AwayFromZero);

            return result;
        }

        /// <summary>
        /// 値とフォーマット設定
        /// </summary>
        /// <param name="source">設定対象のコントロール</param>
        /// <param name="value">値</param>
        /// <param name="format">フォーマット</param>
        /// <returns>結果</returns>
        private static bool SetValueAndFormat(object source, object value, string format)
        {
            var multiRowCell = source as Cell;
            var dgvCell = source as DataGridViewCell;
            if (multiRowCell != null)
            {
                // No.3723-->
                // DataGridViewCellの無限ループ対策をmultiRowCellにも反映
                //multiRowCell.Value = value;
                if (value is decimal && !(multiRowCell.Value is DBNull) && multiRowCell.Value != null && (!string.Empty.Equals(multiRowCell.Value.ToString())) && decimal.Equals(Convert.ToDecimal(value), Convert.ToDecimal(multiRowCell.Value.ToString()))) // sqldecimal等のsqlTypeの場合うまくコンバートできないのでToStringが必要
                {
                    //なにもしない
                }
                else if (value is string && !(multiRowCell.Value is DBNull) && multiRowCell.Value != null && string.Equals(value, multiRowCell.Value.ToString()))
                {
                    //なにもしない
                }
                else if (multiRowCell.Value != value)
                {
                    multiRowCell.Value = value;
                }
                // No.3723<--
                multiRowCell.Style.Format = format;
                return true;
            }
            else if (dgvCell != null)
            {
                if (dgvCell.IsInEditMode)
                {
                    //dgvCell.DataGridView.EditingControl.Text = value.ToString();
                    dgvCell.DataGridView.EditingControl.Text = string.Format("{0:" + format + "}", value);
                }

                //無限ループ対策（フォーマットのある列の行をマウスで移動で発生　　valueセット時にフォーマットが動き またここを通るの繰り返し）
                if (value is decimal && !(dgvCell.Value is DBNull) && dgvCell.Value != null && (!string.Empty.Equals(dgvCell.Value.ToString())) && decimal.Equals(Convert.ToDecimal(value), Convert.ToDecimal(dgvCell.Value.ToString()))) // sqldecimal等のsqlTypeの場合うまくコンバートできないのでToStringが必要
                {
                    //なにもしない
                }
                else if (value is string && !(dgvCell.Value is DBNull) && dgvCell.Value != null && string.Equals(value, dgvCell.Value.ToString()))
                {
                    //なにもしない
                    //dgvCell.Value = value;
                }
                else if (!object.Equals(value, dgvCell.Value))
                {
                    //値変更（整数セルの端数カットなど）
                    dgvCell.Value = value;
                }

                dgvCell.Style.Format = format;
                return true;
            }

            return false;
        }

        /// <summary>
        /// 文字列型以外のセルかどうかをチェックする
        /// </summary>
        /// <param name="source">対象のコントロール</param>
        /// <returns>結果</returns>
        private static bool CheckNotStringTypeCell(object source)
        {
            var multiRowCell = source as Cell;
            var dgvCell = source as DataGridViewCell;
            if (multiRowCell != null &&
                !multiRowCell.ValueType.Equals(typeof(string)))
            {
                return true;
            }
            else if (dgvCell != null &&
                !dgvCell.ValueType.Equals(typeof(string)))
            {
                return true;
            }

            return false;
        }

        #endregion private
    }
}