using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dto;
using r_framework.Utility;
using Seasar.Quill.Attrs;

namespace r_framework.Logic
{
    /// <summary>
    /// カスタムコントロールにて自動実行される各チェックメソッド
    /// </summary>
    [Implementation]
    public class Validator
    {
        /// <summary>
        /// PDFファイルの拡張子
        /// </summary>
        private static readonly string PDF = ".pdf";

        /// <summary>
        /// BMPファイルの拡張子
        /// </summary>
        private static readonly string BMP = ".bmp";

        /// <summary>
        /// PNGファイルの拡張子
        /// </summary>
        private static readonly string PNG = ".png";

        /// <summary>
        /// JPEGファイルの拡張子
        /// </summary>
        private static readonly string JPEG = ".jpeg";

        /// <summary>
        /// JPEGファイルの拡張子
        /// </summary>
        private static readonly string JPG = ".jpg";

        /// <summary>
        /// GIFファイルの拡張子
        /// </summary>
        private static readonly string GIF = ".gif";

        /// <summary>
        /// メッセージユーティリティ
        /// </summary>
        private MessageUtility Message { get; set; }

        /// <summary>
        /// コントロール
        /// </summary>
        public ICustomControl CheckControl { get; private set; }

        /// <summary>
        /// 判定先パラメータ
        /// </summary>
        public object[] Param { get; set; }

        /// <summary>
        /// 判定条件
        /// </summary>
        public object[] Conditions { get; set; }

        /// <summary>
        /// 引数指定なしのコンストラクタ
        /// </summary>
        public Validator()
        {
        }

        /// <summary>
        /// 引数指定ありのコンストラクタ
        /// </summary>
        /// <parameparam name="control">チェックを実施するコントロール</parameparam>
        public Validator(ICustomControl control, object[] obj)
        {
            this.CheckControl = control;

            this.Param = obj;

            Message = new MessageUtility();
        }

        /// <summary>
        /// 引数指定ありのコンストラクタ
        /// </summary>
        /// <parameparam name="control">チェックを実施するコントロール</parameparam>
        public Validator(ICustomControl control, object[] conditin, object[] obj)
        {
            this.CheckControl = control;

            this.Param = obj;

            this.Conditions = conditin;

            Message = new MessageUtility();
        }

        ///// <summary>
        ///// 引数指定ありのコンストラクタ
        ///// </summary>
        ///// <parameparam name="control">チェックを実施するコントロール</parameparam>
        ///// <parameparam name="controlCollection">値の設定を行うコントロールコレクション</parameparam>
        //public Validator(Control control, Control.ControlCollection controlCollection)
        //{
        //    this.CheckControl = control;
        //    this.checkControlCollection = controlCollection;

        //    Message = new MessageUtility();
        //}

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <param name="iYear">年</param>
        /// <param name="iMonth">月</param>
        /// <param name="iDay">日</param>
        /// <returns>チェック結果</returns>
        public virtual bool IsDate(int iYear, int iMonth, int iDay)
        {
            if ((DateTime.MinValue.Year > iYear) || (iYear > DateTime.MaxValue.Year))
            {
                return false;
            }

            if ((DateTime.MinValue.Month > iMonth) || (iMonth > DateTime.MaxValue.Month))
            {
                return false;
            }

            var iLastDay = DateTime.DaysInMonth(iYear, iMonth);

            if ((DateTime.MinValue.Day > iDay) || (iDay > iLastDay))
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 必須入力チェック
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public virtual string MandatoryCheck()
        {
            var returnStr = "";

            var resultText = this.CheckControl.GetResultText();
            if (resultText == null || resultText.Length == 0)
            {
                var itemName = this.CheckControl.DisplayItemName;
                returnStr = Message.GetMessage("E001").MESSAGE;
                returnStr = String.Format(returnStr, itemName);
            }
            return returnStr;
        }

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns>チェック結果</returns>
        public virtual bool IsDate()
        {
            DateTime dateTime;
            if (DateTime.TryParse(CheckControl.GetResultText(), out dateTime))
            {
                if ((DateTime.MinValue.Year > dateTime.Year) || (dateTime.Year > DateTime.MaxValue.Year))
                {
                    return false;
                }

                if ((DateTime.MinValue.Month > dateTime.Month) || (dateTime.Month > DateTime.MaxValue.Month))
                {
                    return false;
                }

                var iLastDay = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);

                if ((DateTime.MinValue.Day > dateTime.Day) || (dateTime.Day > iLastDay))
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 時間コンボボックス整合性チェック
        /// </summary>
        /// <returns>チェック結果</returns>
        public virtual string HourComboBoxCheck()
        {
            var hourComboBox = this.CheckControl as CustomHourComboBox;

            if (hourComboBox == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(hourComboBox.LinkedMinuteComboBox))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(Convert.ToString(hourComboBox.SelectedItem)))
            {
                return string.Empty;
            }

            var minuteComboBox = Param[0] as CustomMinuteComboBox;

            if (string.IsNullOrEmpty(Convert.ToString(minuteComboBox.SelectedItem)))
            {
                var returnStr = Message.GetMessage("E027").MESSAGE;
                returnStr = String.Format(returnStr, minuteComboBox.DisplayItemName);
                return returnStr;
            }
            return string.Empty;
        }

        /// <summary>
        /// 分コンボボックス整合性チェック
        /// </summary>
        /// <returns>チェック結果</returns>
        public virtual string MinuteComboBoxCheck()
        {
            var minuteComboBox = this.CheckControl as CustomMinuteComboBox;

            if (minuteComboBox == null)
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(minuteComboBox.LinkedHourComboBox))
            {
                return string.Empty;
            }

            if (string.IsNullOrEmpty(Convert.ToString(minuteComboBox.SelectedItem)))
            {
                return string.Empty;
            }

            var hourComboBox = Param[0] as CustomHourComboBox;

            if (string.IsNullOrEmpty(Convert.ToString(hourComboBox.SelectedItem)))
            {
                var returnStr = Message.GetMessage("E027").MESSAGE;
                returnStr = String.Format(returnStr, hourComboBox.DisplayItemName);
                return returnStr;
            }
            return string.Empty;
        }

        /// <summary>
        /// 電話番号チェック処理
        /// </summary>
        /// <returns>チェック結果</returns>
        public virtual string TelNumberCheck()
        {
            var checkStr = this.CheckControl.GetResultText();
            var returnStr = "";

            if (string.IsNullOrEmpty(checkStr))
            {
                return returnStr;
            }

            if (!this.isTelNumberValid(checkStr))
            {
                var itemName = this.CheckControl.DisplayItemName;

                returnStr = Message.GetMessage("E009").MESSAGE;
                returnStr = string.Format(returnStr, itemName);
            }

            return returnStr;
        }

        /// <summary>
        /// 引数が電話番号で指定可能かチェックする
        /// </summary>
        /// <param name="strTel">電話番号</param>
        /// <returns>true:正常、false:異常</returns>
        public bool isTelNumberValid(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }

            // 電話番号か調べる(数字は1～11桁であること、#と*は数字と見なす)
            var len = str.Replace("(", "").Replace(")", "").Replace("-", "").Replace("+", "").Length;
            if (0 < len && len < 12)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 郵便番号チェック処理
        /// </summary>
        /// <returns>チェック結果</returns>
        public virtual string PostalCodeCheck()
        {
            var checkStr = this.CheckControl.GetResultText();
            var returnStr = "";

            if (string.IsNullOrEmpty(checkStr))
            {
                return returnStr;
            }

            //郵便番号形式か調べる([数字3桁]-[数字4桁]形式又は数字7桁であること)
            var postalCodeFlag =
                Regex.IsMatch(checkStr, @"^\d{3}-\d{4}$", RegexOptions.ECMAScript) ||
                Regex.IsMatch(checkStr, @"^\d{7}$", RegexOptions.ECMAScript);
            if (!postalCodeFlag)
            {
                var itemName = this.CheckControl.DisplayItemName;

                returnStr = Message.GetMessage("E010").MESSAGE;
                returnStr = string.Format(returnStr, itemName);
            }

            return returnStr;
        }

        /// <summary>
        /// メールアドレスチェック処理
        /// </summary>
        public virtual string MailAddressCheck()
        {
            var returnStr = "";

            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return returnStr;
            }

            //メールアドレス形式か調べる
            bool isMailFlag = System.Text.RegularExpressions.Regex.IsMatch(
                this.CheckControl.GetResultText(),
                @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!isMailFlag)
            {
                var itemName = this.CheckControl.DisplayItemName;

                returnStr = Message.GetMessage("E008").MESSAGE;
                returnStr = String.Format(returnStr, itemName);
            }
            return returnStr;
        }

        /// <summary>
        /// 半角カタカナ形式かチェックする
        /// </summary>
        public virtual string HanKatakanaCheck()
        {
            var returnStr = "";

            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return returnStr;
            }

            //半角カタカナか調べる
            bool IsKatakanaFlag = System.Text.RegularExpressions.Regex.IsMatch(
                this.CheckControl.GetResultText(),
                @"^[ 0-9a-zA-Z\uFF66-\uFF9F]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!IsKatakanaFlag)
            {
                var itemName = this.CheckControl.DisplayItemName;

                returnStr = Message.GetMessage("E014").MESSAGE;
                returnStr = String.Format(returnStr, itemName);
            }
            return returnStr;
        }

        /// <summary>
        /// 全角カタカナ形式かチェックする
        /// </summary>
        public virtual string ZenKatakanaCheck()
        {
            var returnStr = "";

            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return returnStr;
            }

            // 全角カタカナか調べる
            // スペース、数字、英字、記号「＆」「’」「，」「‐」「－」「ー」「．」「・」、カタカナ
            bool IsKatakanaFlag = System.Text.RegularExpressions.Regex.IsMatch(
                this.CheckControl.GetResultText(),
                @"^[　０-９ａ-ｚＡ-Ｚ＆’，‐－ー．・\u30A1-\u30F6]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!IsKatakanaFlag)
            {
                var itemName = this.CheckControl.DisplayItemName;

                returnStr = Message.GetMessage("E015").MESSAGE;
                returnStr = String.Format(returnStr, itemName);
            }
            return returnStr;
        }

        /// <summary>
        /// 半角記号チェック
        /// </summary>
        public virtual string HanSymbolCheck()
        {
            var returnStr = "";

            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return returnStr;
            }

            //半角記号形式か調べる
            bool isSimbolFlag = System.Text.RegularExpressions.Regex.IsMatch(
                this.CheckControl.GetResultText(),
                @"^[a-zA-Z\u0021-\u002F\u003A-\u0040\u005B-\u0060\u007B-\u007E]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!isSimbolFlag)
            {
                var itemName = this.CheckControl.DisplayItemName;

                returnStr = Message.GetMessage("E017").MESSAGE;
                returnStr = String.Format(returnStr, itemName);
            }
            return returnStr;
        }

        /// <summary>
        /// 全角記号チェック
        /// </summary>
        public virtual string ZenSymbolCheck()
        {
            var returnStr = "";

            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return returnStr;
            }

            //全角記号形式チェック
            bool isSimbolFlag = System.Text.RegularExpressions.Regex.IsMatch(
                this.CheckControl.GetResultText(),
                @"^[ａ-ｚＡ-Ｚ\uFF01-\uFF0F\uFF1A-\uFF20\uFF3B-\uFF40\uFF5B-\uFF5E]+$",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            if (!isSimbolFlag)
            {
                var itemName = this.CheckControl.DisplayItemName;

                returnStr = Message.GetMessage("E018").MESSAGE;
                returnStr = String.Format(returnStr, itemName);
            }
            return returnStr;
        }

        /// <summary>
        /// コンボボックスに紐付くテキストボックスに入力された値のチェックを行う
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public virtual string ComboBoxCodeCheck()
        {
            var returnMessage = "";
            var masterType = this.CheckControl.GetType();
            var comboBox = Param[0] as ComboBox;
            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                comboBox.SelectedIndex = 0;
            }
            else
            {
                var value = comboBox.ValueMember.Split(',');
                var checkFlag = false;
                for (var i = 0; i < value.Length; i++)
                {
                    if (value[i] == this.CheckControl.GetResultText())
                    {
                        comboBox.SelectedIndex = i;
                        checkFlag = true;
                        break;
                    }
                }
                if (!checkFlag)
                {
                    returnMessage = Message.GetMessage("E011").MESSAGE;
                    returnMessage = returnMessage.Replace("{0}", this.CheckControl.SetFormField);
                }
            }
            return returnMessage;
        }

        /// <summary>
        /// 重複チェック（仮）
        /// </summary>
        public virtual string DuplicationCheck()
        {
            var returnMessage = string.Empty;
            foreach (var param in Param)
            {
                var control = param as ICustomControl;

                if (control.GetResultText() == CheckControl.GetResultText())
                {
                    returnMessage = Message.GetMessage("E013").MESSAGE;
                }
            }
            return returnMessage;
        }

        /// <summary>
        /// 同一チェック
        /// </summary>
        /// <returns></returns>
        public virtual bool EqualsCheck()
        {
            var checkFlag = true;
            for (int i = 0; i < this.Param.Length; i++)
            {
                var checkControl = this.Param[i] as ICustomControl;
                if (this.Conditions[i] != null && checkControl != null && this.Conditions[i].ToString() != checkControl.GetResultText())
                {
                    checkFlag = false;
                    break;
                }
            }
            return checkFlag;
        }

        /// <summary>
        /// 不同一チェック
        /// </summary>
        /// <returns></returns>
        public virtual bool NotEqualsCheck()
        {
            var checkFlag = true;
            for (int i = 0; i < this.Param.Length; i++)
            {
                var checkControl = this.Param[i] as ICustomControl;
                //20250409
                if (this.Conditions[i] != null && checkControl != null && this.Conditions[i].ToString() == checkControl.GetResultText())
                {
                    checkFlag = false;
                    break;
                }
            }
            return checkFlag;
        }

        /// <summary>
        /// 小数の場合のバイト数チェック
        /// </summary>
        /// <returns></returns>
        public virtual string DecimalMaxByteCheck()
        {
            var strLength = this.CheckControl.CharactersNumber.ToString();
            var resultText = this.CheckControl.GetResultText();

            if (string.IsNullOrEmpty(resultText))
            {
                // チェックできない状態の場合は何もしない
                return string.Empty;
            }

            if (!strLength.Contains("."))
            {
                // 小数点なしの場合、
                // 各コントローラのフォーカスアウト処理でチェックしているため
                // ここではチェック不要
            }
            else
            {
                Decimal charactersNumberValue = 0;
                if (!Decimal.TryParse(strLength, out charactersNumberValue) || charactersNumberValue < 1)
                {
                    return string.Empty;
                }

                var byteDataList = strLength.Split('.');
                var resultTextList = resultText.Split('.');

                // "3.3"と必ず小数点は一個しかないはず(小数点の数でチェックしてもOK)
                if (2 < byteDataList.Length || 2 < resultTextList.Length)
                {
                    return string.Empty;
                }

                for (int i = 0; i < 2; i++)
                {
                    var byteData = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(resultTextList[i]);
                    if (int.Parse(byteDataList[i]) < byteData)
                    {
                        var errorMessage = Message.GetMessage("E019").MESSAGE;
                        int intForMessage = int.Parse(byteDataList[0]) + int.Parse(byteDataList[1]) + 1;
                        errorMessage = String.Format(errorMessage, "整数" + int.Parse(byteDataList[0]) + "文字、小数以下" + int.Parse(byteDataList[1]) + "文字", "整数" + int.Parse(byteDataList[0]) / 2 + "文字、小数以下" + int.Parse(byteDataList[1]) / 2 + "文字");
                        return errorMessage;
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 委託契約書ファイル形式チェック
        /// </summary>
        /// <returns></returns>
        public virtual string ItakukeiyakushoCheck()
        {
            if (string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                return string.Empty;
            }

            string errorMessage = string.Empty;

            errorMessage = FileExists();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return errorMessage;
            }

            string stExtension = System.IO.Path.GetExtension(this.CheckControl.GetResultText());
            if (stExtension != null)
            {
                stExtension = stExtension.ToLower();
            }

            if (stExtension == PDF)
            {
                errorMessage = PdfFormatCheck();
                if (string.IsNullOrEmpty(errorMessage))
                {
                    return string.Empty;
                }
            }
            else if (stExtension == BMP || stExtension == PNG || stExtension == JPEG || stExtension == JPG || stExtension == GIF)
            {
                errorMessage = ImageFormatCheck();
                if (string.IsNullOrEmpty(errorMessage))
                {
                    return string.Empty;
                }
            }
            else
            {
                errorMessage = Message.GetMessage("E023").MESSAGE;
            }

            return errorMessage;
        }

        /// <summary>
        /// ファイル存在チェック
        /// </summary>
        /// <returns></returns>
        public virtual string FileExists()
        {
            if (File.Exists(this.CheckControl.GetResultText()))
            {
                return string.Empty;
            }
            return Message.GetMessage("E024").MESSAGE.Replace("{0}", this.CheckControl.DisplayItemName);
        }

        /// <summary>
        /// 拡張子チェック
        /// </summary>
        /// <returns></returns>
        public virtual string ExtensionCheck()
        {
            string stExtension = System.IO.Path.GetExtension(this.CheckControl.GetResultText());

            foreach (var extension in Param)
            {
                if (stExtension == extension.ToString())
                {
                    return string.Empty;
                }
            }

            return Message.GetMessage("E024").MESSAGE.Replace("{0}", this.CheckControl.DisplayItemName);
        }

        /// <summary>
        /// PDFファイルフォーマットチェック
        /// </summary>
        /// <returns></returns>
        public virtual string PdfFormatCheck()
        {
            string stExtension = System.IO.Path.GetExtension(this.CheckControl.GetResultText());
            if (stExtension != null)
            {
                stExtension = stExtension.ToLower();
            }

            if (stExtension == PDF)
            {
                using (var reader = new PDFReader())
                {
                    var readFlag = reader.IsValid(this.CheckControl.GetResultText());

                    if (!readFlag)
                    {
                        return Message.GetMessage("E023").MESSAGE;
                    }
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// 画像フォーマットチェック
        /// </summary>
        /// <returns></returns>
        public virtual string ImageFormatCheck()
        {
            string stExtension = System.IO.Path.GetExtension(this.CheckControl.GetResultText());
            if (stExtension != null)
            {
                stExtension = stExtension.ToLower();
            }

            if (stExtension == PDF)
            {
                return string.Empty;
            }

            System.Drawing.Image img = System.Drawing.Image.FromFile(this.CheckControl.GetResultText());

            //イメージのファイル形式を調べる
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
            {
                return string.Empty;
            }
            else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
            {
                return string.Empty;
            }
            else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
            {
                return string.Empty;
            }
            else if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
            {
                return string.Empty;
            }

            //画像ファイルフォーマットエラーメッセージ
            return Message.GetMessage("E023").MESSAGE;
        }

        /// <summary>
        /// 日付整合性チェック(From用)
        /// FromとToの両方に値がある場合にのみチェックをします。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public virtual string FromDateIntegrityCheck()
        {
            //FW_QA154_FromとToだけはエラーでも相互移動できるように対応
            Control compareControl = Param[0] as Control;
            Control myself = this.CheckControl as Control;
            if (compareControl != null)
            {
                compareControl.CausesValidation = true; //初期化
                compareControl.BackColor = Const.Constans.NOMAL_COLOR; //相方が赤く残対策
                PropertyUtility.SetValue(compareControl, "IsInputErrorOccured", false); // ※テキストとコンボだけにあるプロパティ これがtrueだとフォーカスインすると赤くなる
            }
            var returnMessage = string.Empty;
            if (!string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                var targetDate = new DateTime();
                var fromDateParseFlag = DateTime.TryParse(this.CheckControl.GetResultText(), out targetDate);
                if (!fromDateParseFlag)
                {
                    return "From日付がエラーです"; //validating序盤で日付になっているのでここには来ないはず
                }

                var compareTarget = new DateTime();
                var compareTargetTemp = Param[0] as ICustomControl;
                //var toDateParseFlag = DateTime.TryParse(compareTargetTemp.GetResultText(), out compareTarget);

                //if (!toDateParseFlag)
                //{
                //    if (compareTargetTemp.GetResultText().Trim().Length > 0)
                //    {
                //        var messageUtil = new MessageUtility();
                //        returnMessage = messageUtil.GetMessage("E030").MESSAGE;
                //        compareControl.CausesValidation = false; //エラー時はチェック対象外に
                //        compareControl.BackColor = Const.Constans.ERROR_COLOR; //相方も赤くする
                //        PropertyUtility.SetValue(compareControl, "IsInputErrorOccured", true);
                //    }
                //    return returnMessage;
                //}

                string text = compareTargetTemp.GetResultText();

                if (string.IsNullOrEmpty(text))
                {
                    //正常処理 ※メソッド下部とソース重複するので注意
                    //相方の色変え
                    PropertyUtility.SetValue(Param[0], "IsInputErrorOccured", false);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (Param[0] is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)Param[0]).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (compareControl != null) //色はICustomAutoChangeBackColorを実装していればそちらを優先（色を変えない設定もあるため）
                    {
                        compareControl.BackColor = Const.Constans.NOMAL_COLOR; //相方も白くする
                    }

                    //自分の色変え
                    PropertyUtility.SetValue(this.CheckControl, "IsInputErrorOccured", false);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (this.CheckControl is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)this.CheckControl).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (myself != null)
                    {
                        myself.BackColor = Const.Constans.ERROR_COLOR; //自分も白くする
                    }

                    return returnMessage; //相方が空だとチェックはしない
                }
                DateTime maxvalue;
                DateTime minvalue;

                //エラーメッセージ改善
                if (Param[0] is CustomDateTimePicker)
                {
                    var d = Param[0] as CustomDateTimePicker;
                    maxvalue = d.MaxValue;
                    minvalue = d.MinValue;
                }
                else if (Param[0] is DgvCustomDataTimeCell)
                {
                    var d = Param[0] as DgvCustomDataTimeCell;
                    var col = (DgvCustomDataTimeColumn)d.OwningColumn;
                    maxvalue = col.MaxValue;
                    minvalue = col.MinValue;
                }
                else if (Param[0] is GcCustomDataTime)
                {
                    var d = Param[0] as GcCustomDataTime;
                    maxvalue = d.MaxValue;
                    minvalue = d.MinValue;
                }
                else
                {
                    //対象外
                    //相方の色変え
                    PropertyUtility.SetValue(Param[0], "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (Param[0] is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)Param[0]).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (compareControl != null) //色はICustomAutoChangeBackColorを実装していればそちらを優先（色を変えない設定もあるため）
                    {
                        compareControl.BackColor = Const.Constans.ERROR_COLOR; //相方も赤くする
                    }
                    //必ず実行
                    if (compareControl != null) compareControl.CausesValidation = false; //エラー時はチェック対象外に

                    //自分の色変え
                    PropertyUtility.SetValue(this.CheckControl, "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (this.CheckControl is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)this.CheckControl).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (myself != null)
                    {
                        myself.BackColor = Const.Constans.ERROR_COLOR; //自分も赤くする
                    }

                    return "日付整合性チェック対象外のコントロールです";
                }

                returnMessage = CheckDateTimeString(text, maxvalue, minvalue, out compareTarget);
                if (!string.IsNullOrEmpty(returnMessage))
                {
                    //相方の色変え
                    PropertyUtility.SetValue(Param[0], "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (Param[0] is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)Param[0]).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (compareControl != null) //色はICustomAutoChangeBackColorを実装していればそちらを優先（色を変えない設定もあるため）
                    {
                        compareControl.BackColor = Const.Constans.ERROR_COLOR; //相方も赤くする
                    }
                    //必ず実行
                    if (compareControl != null) compareControl.CausesValidation = false; //エラー時はチェック対象外に

                    //自分の色変え
                    PropertyUtility.SetValue(this.CheckControl, "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (this.CheckControl is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)this.CheckControl).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (myself != null)
                    {
                        myself.BackColor = Const.Constans.ERROR_COLOR; //自分も赤くする
                    }

                    return returnMessage;
                }

                if (compareTarget < targetDate)
                {
                    //FW_QA154_FromとToだけはエラーでも相互移動できるように対応
                    //相方の色変え
                    PropertyUtility.SetValue(Param[0], "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (Param[0] is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)Param[0]).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (compareControl != null) //色はICustomAutoChangeBackColorを実装していればそちらを優先（色を変えない設定もあるため）
                    {
                        compareControl.BackColor = Const.Constans.ERROR_COLOR; //相方も赤くする
                    }
                    //必ず実行
                    if (compareControl != null) compareControl.CausesValidation = false; //エラー時はチェック対象外に

                    //自分の色変え
                    PropertyUtility.SetValue(this.CheckControl, "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (this.CheckControl is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)this.CheckControl).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (myself != null)
                    {
                        myself.BackColor = Const.Constans.ERROR_COLOR; //自分も赤くする
                    }

                    var messageUtil = new MessageUtility();
                    returnMessage = messageUtil.GetMessage("E030").MESSAGE;
                    returnMessage = String.Format(returnMessage, this.CheckControl.DisplayItemName, compareTargetTemp.DisplayItemName);
                    return returnMessage;
                }
                else
                {
                    //正常
                }
            }
            else
            {
                // 日付が入力されていなければチェックしない
                //相方が空なら正常
            }
            //正常 共通処理
            //相方の色変え
            PropertyUtility.SetValue(Param[0], "IsInputErrorOccured", false);
            //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
            if (Param[0] is ICustomAutoChangeBackColor)
            {
                ((ICustomAutoChangeBackColor)Param[0]).UpdateBackColor(false);//フォーカスなし
            }
            else if (compareControl != null) //色はICustomAutoChangeBackColorを実装していればそちらを優先（色を変えない設定もあるため）
            {
                compareControl.BackColor = Const.Constans.NOMAL_COLOR; //相方も白くする
            }

            //自分の色変え
            PropertyUtility.SetValue(this.CheckControl, "IsInputErrorOccured", false);
            //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
            if (this.CheckControl is ICustomAutoChangeBackColor)
            {
                ((ICustomAutoChangeBackColor)this.CheckControl).UpdateBackColor(false);//フォーカスなし
            }
            else if (myself != null)
            {
                myself.BackColor = Const.Constans.ERROR_COLOR; //自分も白くする
            }

            return returnMessage;
        }

        /// <summary>
        /// 日付整合性チェック(To用)
        /// FromとToの両方に値がある場合にのみチェックをします。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public virtual string ToDateIntegrityCheck()
        {
            //FW_QA154_FromとToだけはエラーでも相互移動できるように対応
            Control compareControl = Param[0] as Control;
            Control myself = this.CheckControl as Control;
            if (compareControl != null)
            {
                compareControl.CausesValidation = true; //初期化
                compareControl.BackColor = Const.Constans.NOMAL_COLOR; //相方が赤く残対策
                PropertyUtility.SetValue(compareControl, "IsInputErrorOccured", false); // ※テキストとコンボだけにあるプロパティ これがtrueだとフォーカスインすると赤くなる
            }
            var returnMessage = string.Empty;
            if (!string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                var targetDate = new DateTime();
                var toDateParseFlag = DateTime.TryParse(this.CheckControl.GetResultText(), out targetDate);
                if (!toDateParseFlag)
                {
                    return returnMessage;
                }

                var compareTarget = new DateTime();
                var compareTargetTemp = Param[0] as ICustomControl;
                //var fromDateParseFlag = DateTime.TryParse(compareTargetTemp.GetResultText(), out compareTarget);

                //if (!fromDateParseFlag)
                //{
                //    if (compareTargetTemp.GetResultText().Trim().Length > 0)
                //    {
                //        var messageUtil = new MessageUtility();
                //        returnMessage = messageUtil.GetMessage("E030").MESSAGE;
                //        compareControl.CausesValidation = false; //エラー時はチェック対象外に
                //        compareControl.BackColor = Const.Constans.ERROR_COLOR; //相方も赤くする
                //        PropertyUtility.SetValue(compareControl, "IsInputErrorOccured", true);
                //    }
                //    return returnMessage;
                //}

                string text = compareTargetTemp.GetResultText();

                if (string.IsNullOrEmpty(text))
                {
                    //正常処理 ※メソッド下部とソース重複するので注意
                    //相方の色変え
                    PropertyUtility.SetValue(Param[0], "IsInputErrorOccured", false);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (Param[0] is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)Param[0]).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (compareControl != null) //色はICustomAutoChangeBackColorを実装していればそちらを優先（色を変えない設定もあるため）
                    {
                        compareControl.BackColor = Const.Constans.NOMAL_COLOR; //相方も白くする
                    }

                    //自分の色変え
                    PropertyUtility.SetValue(this.CheckControl, "IsInputErrorOccured", false);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (this.CheckControl is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)this.CheckControl).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (myself != null)
                    {
                        myself.BackColor = Const.Constans.ERROR_COLOR; //自分も白くする
                    }

                    return returnMessage; //相方が空だとチェックはしない
                }
                DateTime maxvalue;
                DateTime minvalue;

                //エラーメッセージ改善
                if (Param[0] is CustomDateTimePicker)
                {
                    var d = Param[0] as CustomDateTimePicker;
                    maxvalue = d.MaxValue;
                    minvalue = d.MinValue;
                }
                else if (Param[0] is DgvCustomDataTimeCell)
                {
                    var d = Param[0] as DgvCustomDataTimeCell;
                    var col = (DgvCustomDataTimeColumn)d.OwningColumn;
                    maxvalue = col.MaxValue;
                    minvalue = col.MinValue;
                }
                else if (Param[0] is GcCustomDataTime)
                {
                    var d = Param[0] as GcCustomDataTime;
                    maxvalue = d.MaxValue;
                    minvalue = d.MinValue;
                }
                else
                {
                    //対象外
                    //相方の色変え
                    PropertyUtility.SetValue(Param[0], "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (Param[0] is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)Param[0]).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (compareControl != null) //色はICustomAutoChangeBackColorを実装していればそちらを優先（色を変えない設定もあるため）
                    {
                        compareControl.BackColor = Const.Constans.ERROR_COLOR; //相方も赤くする
                    }
                    //必ず実行
                    if (compareControl != null) compareControl.CausesValidation = false; //エラー時はチェック対象外に

                    //自分の色変え
                    PropertyUtility.SetValue(this.CheckControl, "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (this.CheckControl is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)this.CheckControl).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (myself != null)
                    {
                        myself.BackColor = Const.Constans.ERROR_COLOR; //自分も赤くする
                    }

                    return "日付整合性チェック対象外のコントロールです";
                }

                returnMessage = CheckDateTimeString(text, maxvalue, minvalue, out compareTarget);
                if (!string.IsNullOrEmpty(returnMessage))
                {
                    //相方の色変え
                    PropertyUtility.SetValue(Param[0], "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (Param[0] is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)Param[0]).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (compareControl != null) //色はICustomAutoChangeBackColorを実装していればそちらを優先（色を変えない設定もあるため）
                    {
                        compareControl.BackColor = Const.Constans.ERROR_COLOR; //相方も赤くする
                    }
                    //必ず実行
                    if (compareControl != null) compareControl.CausesValidation = false; //エラー時はチェック対象外に

                    //自分の色変え
                    PropertyUtility.SetValue(this.CheckControl, "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (this.CheckControl is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)this.CheckControl).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (myself != null)
                    {
                        myself.BackColor = Const.Constans.ERROR_COLOR; //自分も赤くする
                    }

                    return returnMessage;
                }

                if (targetDate < compareTarget)
                {
                    //FW_QA154_FromとToだけはエラーでも相互移動できるように対応
                    //相方の色変え
                    PropertyUtility.SetValue(Param[0], "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (Param[0] is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)Param[0]).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (compareControl != null) //色はICustomAutoChangeBackColorを実装していればそちらを優先（色を変えない設定もあるため）
                    {
                        compareControl.BackColor = Const.Constans.ERROR_COLOR; //相方も赤くする
                    }
                    //必ず実行
                    if (compareControl != null) compareControl.CausesValidation = false; //エラー時はチェック対象外に

                    //自分の色変え
                    PropertyUtility.SetValue(this.CheckControl, "IsInputErrorOccured", true);
                    //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
                    if (this.CheckControl is ICustomAutoChangeBackColor)
                    {
                        ((ICustomAutoChangeBackColor)this.CheckControl).UpdateBackColor(false);//フォーカスなし
                    }
                    else if (myself != null)
                    {
                        myself.BackColor = Const.Constans.ERROR_COLOR; //自分も赤くする
                    }

                    var messageUtil = new MessageUtility();
                    returnMessage = messageUtil.GetMessage("E030").MESSAGE;
                    returnMessage = String.Format(returnMessage, compareTargetTemp.DisplayItemName, this.CheckControl.DisplayItemName);
                    return returnMessage;
                }
                else
                {
                    //正常
                }
            }
            else
            {
                // 日付が入力されていなければチェックしない
                //相方が空なら正常
            }
            //正常 共通処理
            //相方の色変え
            PropertyUtility.SetValue(Param[0], "IsInputErrorOccured", false);
            //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
            if (Param[0] is ICustomAutoChangeBackColor)
            {
                ((ICustomAutoChangeBackColor)Param[0]).UpdateBackColor(false);//フォーカスなし
            }
            else if (compareControl != null) //色はICustomAutoChangeBackColorを実装していればそちらを優先（色を変えない設定もあるため）
            {
                compareControl.BackColor = Const.Constans.NOMAL_COLOR; //相方も白くする
            }

            //自分の色変え
            PropertyUtility.SetValue(this.CheckControl, "IsInputErrorOccured", false);
            //Fromからシフト+タブ移動で出るとフォーカスが残っているので青になる、その対策
            if (this.CheckControl is ICustomAutoChangeBackColor)
            {
                ((ICustomAutoChangeBackColor)this.CheckControl).UpdateBackColor(false);//フォーカスなし
            }
            else if (myself != null)
            {
                myself.BackColor = Const.Constans.ERROR_COLOR; //自分も白くする
            }

            return returnMessage;
        }

        /// <summary>
        /// 文字が日付として認識できるかチェックを行う
        /// </summary>
        /// <param name="input"></param>
        /// <param name="MaxValue"></param>
        /// <param name="MinValue"></param>
        /// <returns>エラーメッセージ</returns>
        public static string CheckDateTimeString(string value, DateTime _maxdate, DateTime _mindate, out DateTime dt)
        {
            string returnMessage = "";

            _maxdate = _maxdate.Date;//時分秒は利用しない
            _mindate = _mindate.Date;//時分秒は利用しない

            var messageUtil = new MessageUtility();

            //複数書式でパース率を上げる
            string[] formats = { "yyyyMMdd", "yyyy/MM/dd", "y/M/d", "yyyy/M/d", "M/d", "MM/dd", "MMdd", "yyMMdd", "yyyy/MM/dd(ddd)" };

            if (DateTime.TryParseExact(value, formats, null, System.Globalization.DateTimeStyles.None, out dt))
            {
                if (dt > _maxdate)
                {
                    returnMessage = messageUtil.GetMessage("E042").MESSAGE;
                    returnMessage = String.Format(returnMessage, "[ " + _maxdate.ToString("yyyy/MM/dd") + " ]以下");
                    return returnMessage;
                }
                else if (dt < _mindate)
                {
                    returnMessage = messageUtil.GetMessage("E042").MESSAGE;
                    returnMessage = String.Format(returnMessage, "[ " + _mindate.ToString("yyyy/MM/dd") + " ]以上");
                    return returnMessage;
                }
                else
                {
                    value = dt.ToString("yyyy/MM/dd");
                }
            }

            if (!DateTime.TryParse(value, out dt))
            {
                returnMessage = messageUtil.GetMessage("E084").MESSAGE;
                returnMessage = String.Format(returnMessage, value);
            }

            return returnMessage;
        }

        /// <summary>
        /// 数値範囲チェック
        /// 入力値がパラメータ最大値、最小値の範囲内であるかをチェックをします。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public virtual string NumericRangeCheck()
        {
            var returnMessage = string.Empty;

            var text = this.CheckControl.GetResultText();
            if (string.IsNullOrEmpty(text))
            {
                // 入力値が設定されていない場合、チェックしない
                return returnMessage;
            }

            object objTemp;
            if (!PropertyUtility.GetValue(this.CheckControl, "RangeSetting", out objTemp))
            {
                // 範囲が設定されていない場合、チェックしない
                return returnMessage;
            }

            var range = objTemp as RangeSettingDto;
            if (range == null)
            {
                // 範囲クラスに変換出来ない場合、チェックしない
                LogUtility.CallerFrame = new System.Diagnostics.StackFrame();
                LogUtility.Message = "プロパティ(RangeSetting)の値をクラスに変換出来ませんでした。";
                LogUtility.Warn();
                return returnMessage;
            }

            var targetNum = new Decimal();
            if (!Decimal.TryParse(text, out targetNum))
            {
                // 数値に変換出来ない場合、チェックしない
                LogUtility.CallerFrame = new System.Diagnostics.StackFrame();
                LogUtility.Message = String.Format("入力値[{0}]が数値に変換出来ませんでした。", text);
                LogUtility.Warn();
                return returnMessage;
            }

            if (targetNum < range.Min || range.Max < targetNum)
            {
                // 最大値、最小値を対象コントロールのプロパティでフォーマットする
                var strMin = range.Min.ToString();
                var strMax = range.Max.ToString();
                ICustomControl cstmCtrl;
                ICustomTextBox cstmText;
                if (CustomTextBoxLogic.TryGetCustomTextCtrl(this.CheckControl, out cstmCtrl, out cstmText))
                {
                    CustomTextBoxLogic logic = new CustomTextBoxLogic(cstmText);
                    logic.GetFormattedString(strMin, out strMin);
                    logic.GetFormattedString(strMax, out strMax);
                }

                // 範囲外の場合、エラーメッセージを設定する
                var messageUtil = new MessageUtility();
                returnMessage = messageUtil.GetMessage("E002").MESSAGE;
                var strRange = String.Format("{0}～{1}", strMin, strMax);
                returnMessage = String.Format(returnMessage, this.CheckControl.DisplayItemName, strRange);
            }

            return returnMessage;
        }

        /// <summary>
        /// CD整合性チェック(From用)
        /// FromとToの両方に値がある場合にのみチェックをします。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public virtual string FromCDIntegrityCheck()
        {
            //FW_QA154_FromとToだけはエラーでも相互移動できるように対応
            Control compareControl = Param[0] as Control;
            if (compareControl != null)
            {
                compareControl.CausesValidation = true; //初期化
                compareControl.BackColor = Const.Constans.NOMAL_COLOR; //相方が赤く残対策
                PropertyUtility.SetValue(compareControl, "IsInputErrorOccured", false); // ※テキストとコンボだけにあるプロパティ これがtrueだとフォーカスインすると赤くなる
            }
            var returnMessage = string.Empty;
            if (!string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                var targetStr = this.CheckControl.GetResultText();

                var compareTargetTmp = Param[0] as ICustomControl;
                String compareTargetStr = compareTargetTmp.GetResultText();
                if (string.IsNullOrEmpty(compareTargetStr))
                {
                    return returnMessage;
                }

                if (0 < string.Compare(targetStr, compareTargetStr))
                {
                    //FW_QA154_FromとToだけはエラーでも相互移動できるように対応
                    if (compareControl != null)
                    {
                        compareControl.CausesValidation = false; //エラー時はチェック対象外に
                        compareControl.BackColor = Const.Constans.ERROR_COLOR; //相方も赤くする
                        PropertyUtility.SetValue(compareControl, "IsInputErrorOccured", true); // ※テキストとコンボだけにあるプロパティ これがtrueだとフォーカスインすると赤くなる
                    }

                    var messageUtil = new MessageUtility();
                    returnMessage = messageUtil.GetMessage("E032").MESSAGE;
                    returnMessage = String.Format(returnMessage, this.CheckControl.DisplayItemName, compareTargetTmp.DisplayItemName);
                }
            }
            else
            {
                // 日付が入力されていなければチェックしない
                return returnMessage;
            }
            return returnMessage;
        }

        /// <summary>
        /// CD整合性チェック(To用)
        /// FromとToの両方に値がある場合にのみチェックをします。
        /// </summary>
        /// <returns>エラーメッセージ</returns>
        public virtual string ToCDIntegrityCheck()
        {
            //FW_QA154_FromとToだけはエラーでも相互移動できるように対応
            Control compareControl = Param[0] as Control;
            if (compareControl != null)
            {
                compareControl.CausesValidation = true; //初期化
                compareControl.BackColor = Const.Constans.NOMAL_COLOR; //相方が赤く残対策
                PropertyUtility.SetValue(compareControl, "IsInputErrorOccured", false); // ※テキストとコンボだけにあるプロパティ これがtrueだとフォーカスインすると赤くなる
            }
            var returnMessage = string.Empty;
            if (!string.IsNullOrEmpty(this.CheckControl.GetResultText()))
            {
                var targetStr = this.CheckControl.GetResultText();

                var compareTargetTmp = Param[0] as ICustomControl;
                String compareTargetStr = compareTargetTmp.GetResultText();
                if (string.IsNullOrEmpty(compareTargetStr))
                {
                    return returnMessage;
                }

                if (0 < string.Compare(compareTargetStr, targetStr))
                {
                    //FW_QA154_FromとToだけはエラーでも相互移動できるように対応
                    if (compareControl != null)
                    {
                        compareControl.CausesValidation = false; //エラー時はチェック対象外に
                        compareControl.BackColor = Const.Constans.ERROR_COLOR; //相方も赤くする
                        PropertyUtility.SetValue(compareControl, "IsInputErrorOccured", true); // ※テキストとコンボだけにあるプロパティ これがtrueだとフォーカスインすると赤くなる
                    }

                    var messageUtil = new MessageUtility();
                    returnMessage = messageUtil.GetMessage("E032").MESSAGE;
                    returnMessage = String.Format(returnMessage, compareTargetTmp.DisplayItemName, this.CheckControl.DisplayItemName);
                }
            }
            else
            {
                // 日付が入力されていなければチェックしない
                return returnMessage;
            }
            return returnMessage;
        }

        /// <summary>
        /// JWNETの中で有効なShift-JIS文字かを判定する
        /// </summary>
        /// <returns></returns>
        public virtual string JWNetValidShiftJisCharCheck()
        {
            string returnMessage = string.Empty;

            string targetStr = this.CheckControl.GetResultText();
            if (!this.isJWNetValidShiftJisCharForSign(targetStr))
            {
                var messageUtil = new MessageUtility();
                returnMessage = messageUtil.GetMessage("E071").MESSAGE;
                returnMessage = String.Format(returnMessage, this.CheckControl.DisplayItemName);
            }

            return returnMessage;
        }

        /// <summary>
        /// JWNETの中で有効なShift-JIS文字かを判定する(半角記号対応)
        /// GENESYS-ecoのJavaコードから流用
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isOneByte"></param>
        /// <returns></returns>
        public virtual bool isJWNetValidShiftJisCharForSign(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }

            Encoding enc = Encoding.GetEncoding("Shift_JIS");
            string sStr = Regex.Replace(str, "[?―～∥－￠￡￢]+", "");//?を禁則文字ではないの空白で換える
            byte[] aryByte;
            aryByte = enc.GetBytes(sStr);
            int iLen = aryByte.Length;
            for (int i = 0; i < iLen; i++)
            {
                int iTmp = aryByte[i] & 0xFF;
                //2バイト文字上位1バイト　 0x81～0x9f、 0xe0～0xef
                //2バイト文字下位1バイト　 0x40～0x7e、 0x80～0xfc
                //2バイト
                if (iTmp >= 0x81 && iTmp <= 0x9f || iTmp >= 0xe0 && iTmp <= 0xef)
                {
                    if (i == iLen - 1) { return false; }//最後バイト
                    int iTmp2 = aryByte[i + 1] & 0xFF;
                    if (iTmp2 >= 0x40 && iTmp2 <= 0x7e || iTmp2 >= 0x80 && iTmp2 <= 0xfc)
                    {
                        i++;//下位1バイト飛び出し
                    }
                    else
                    {
                        return false;//2バイト文字下位1バイト範囲外
                    }
                    //連続範囲
                    if (iTmp >= 0x89 && iTmp <= 0x97 || iTmp >= 0x99 && iTmp <= 0x9f || iTmp >= 0xe0 && iTmp <= 0xe9)
                    {
                        continue;
                    }
                    else if (iTmp == 0x88)
                    {
                        if (iTmp2 >= 0x9f)
                        {
                            continue;//次文字
                        }
                        return false; //有効文字外
                    }
                    else if (iTmp == 0x98)
                    {
                        if (iTmp2 <= 0x72 || iTmp2 >= 9f)
                        {
                            continue;//次文字
                        }
                        return false; //有効文字外
                    }
                    else if (iTmp == 0xea)
                    {
                        if (iTmp2 <= 0xa4)
                        {
                            continue;//次文字
                        }
                        return false; //有効文字外
                    }
                    else if (iTmp == 0x81)
                    {
                        if (iTmp2 >= 0x40 && iTmp2 <= 0x9e || iTmp2 >= 0x0f && iTmp2 <= 0xac
                                || iTmp2 >= 0xb8 && iTmp2 <= 0xbf || iTmp2 >= 0xc8 && iTmp2 <= 0xce
                                || iTmp2 >= 0xda && iTmp2 <= 0xe8 || iTmp2 >= 0xf0 && iTmp2 <= 0xf7
                                || iTmp2 == 0xfc
                                )
                        {
                            continue;//次文字
                        }
                        return false; //有効文字外
                    }
                    else if (iTmp == 0x82)
                    {
                        if (iTmp2 >= 0x4f && iTmp2 <= 0x58 || iTmp2 >= 0x60 && iTmp2 <= 0x79
                                || iTmp2 >= 0x81 && iTmp2 <= 0x9a || iTmp2 >= 0x9f && iTmp2 <= 0xf1
                                )
                        {
                            continue;//次文字
                        }
                        return false; //有効文字外
                    }
                    else if (iTmp == 0x83)
                    {
                        if (iTmp2 >= 0x40 && iTmp2 <= 0x96 || iTmp2 >= 0x9f && iTmp2 <= 0xb6
                                || iTmp2 >= 0xbf && iTmp2 <= 0xd6
                                )
                        {
                            continue;//次文字
                        }
                        return false; //有効文字外
                    }
                    else if (iTmp == 0x84)
                    {
                        if (iTmp2 >= 0x40 && iTmp2 <= 0x60 || iTmp2 >= 0x70 && iTmp2 <= 0x91
                                || iTmp2 >= 0x9f && iTmp2 <= 0xbe
                                )
                        {
                            continue;//次文字
                        }
                        return false; //有効文字外
                    }
                    return false;
                }
                //1バイト
                else if (iTmp >= 0x20 && iTmp <= 0x7e)
                {//全角スペースOKのところ、半角スペースもOK
                    switch (iTmp)
                    {
                        case '?': //変換できない文字がある禁則文字
                        case '\'':
                        case '%':
                        case '\n':
                        case '"':
                        case '_':
                        case '\r':
                        case '<':
                        case '>':
                        case ',':
                            return false;
                        case '\\':
                            if ((i + 1) < iLen && ((aryByte[i + 1] & 0xFF) == 'n' || (aryByte[i + 1] & 0xFF) == 'r'))
                            {
                                return false;
                            }
                            break;
                    }
                    continue;
                }
                return false;
            } //for end
            return true;
        }

        public bool Equals(Validator other)
        {
            return this.Equals(other);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}