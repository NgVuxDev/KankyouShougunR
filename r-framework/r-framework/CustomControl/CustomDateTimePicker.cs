using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;
using r_framework.Logic;
using r_framework.Utility;
using System.Collections.Generic;
using System.Text;

namespace r_framework.CustomControl
{
    /// <summary>
    /// カスタム日付入力コントロール
    /// </summary>
    /// <remarks>
    /// http://japan.internet.com/developer/20050822/25.html を参考にしたと思われる。
    /// </remarks>
    public partial class CustomDateTimePicker : TextBox, ICustomControl, ICustomAutoChangeBackColor
    {
        #region 変数定義

        private Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");

        private bool isSelectAll = false;   // 全選択フラグ //No.3873

        /// <summary>日付変換対象フォーマット</summary>
        private static string[] dateFormat = new string[] { "yyyyMMdd", "yyyy/MM/dd", "y/M/d", "yyyy/M/d", "M/d", "MM/dd", "MMdd", "yyMMdd", "yyyy/MM/dd(ddd)", "yyyy-MM-dd", "yyyy", "yyyy/MM" };

        /// <summary>
        /// 表示するPopUp。未指定(null)の場合、DLLファイルが使用される。
        /// </summary>
        public APP.PopUp.Base.SuperPopupForm DisplayPopUp { get; set; }

        /// <summary>
        /// 入力エラーが発生したかどうか
        /// </summary>
        private bool isInputErrorOccured = false;
        public bool IsInputErrorOccured
        {
            get
            {
                return this.isInputErrorOccured;
            }
            set
            {
                this.isInputErrorOccured = value;
                this.UpdateBackColor();//拡張メソッド
            }
        }

        private bool autoChangeBackColorEnabled = true;
        /// <summary>
        /// 自動背景色変更モード
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("エラーやフォーカス時の色を独自に設定する場合はfalseに変更してください")]
        [DefaultValue(true)]
        public bool AutoChangeBackColorEnabled
        {
            get
            {
                return this.autoChangeBackColorEnabled;
            }
            set
            {
                this.autoChangeBackColorEnabled = value;
                this.UpdateBackColor();//拡張メソッド
            }
        }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomDateTimePicker()
        {
            InitializeComponent();
            RegistCheckMethod = new Collection<SelectCheckDto>();
            FocusOutCheckMethod = new Collection<SelectCheckDto>();
            popupWindowSetting = new Collection<JoinMethodDto>();
            PopupSearchSendParams = new Collection<PopupSearchSendParamDto>();

            NullValue = "";

            // FormFieldのコピー
            if (string.IsNullOrEmpty(this.PopupSetFormField))
            {
                this.PopupSetFormField = this.SetFormField;
            }
            if (string.IsNullOrEmpty(this.PopupGetMasterField))
            {
                this.PopupGetMasterField = this.GetCodeMasterField;
            }
            //this.CreateHintText();
            this.SuspendLayout();

            base.Location = new Point(0, 0);
            base.Multiline = false;
            base.Size = new Size(110, 20);
            base.BorderStyle = BorderStyle.FixedSingle;
            base.MaxLength = 10;
            //デフォルト
            this.ShowYoubi = true;
            this.MaxValue = System.Data.SqlTypes.SqlDateTime.MaxValue.Value;
            this.MinValue = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            this.YearValue = "";
            this.DisplayOnlyYear = false;
        }

        /// <summary>
        /// CreateControlイベントハンドラ
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            // FormFieldのコピー
            if (string.IsNullOrEmpty(this.PopupSetFormField))
            {
                this.PopupSetFormField = this.SetFormField;
            }
            if (string.IsNullOrEmpty(this.PopupGetMasterField))
            {
                this.PopupGetMasterField = this.GetCodeMasterField;
            }

            //this.CreateHintText();
            this.UpdateBackColor();//拡張メソッド
        }

        #endregion

        #region Property

        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBのフィールド名を記述してください。")]
        public string DBFieldsName { get; set; }
        private bool ShouldSerializeDBFieldsName()
        {
            return this.DBFieldsName != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBフィールドの型名を指定してください(varchar等)")]
        public string ItemDefinedTypes { get; set; }
        private bool ShouldSerializeItemDefinedTypes()
        {
            return this.ItemDefinedTypes != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語名を指定してください。")]
        public string DisplayItemName { get; set; }
        private bool ShouldSerializeDisplayItemName()
        {
            return this.DisplayItemName != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語短縮名を指定してください。")]
        public string ShortItemName { get; set; }
        private bool ShouldSerializeShortItemName()
        {
            return this.ShortItemName != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("汎用検索画面に表示するかのフラグを設定してください(使用方法未定)")]
        public int SearchDisplayFlag { get; set; }
        private bool ShouldSerializeSearchDisplayFlag()
        {
            return this.SearchDisplayFlag != 0;
        }

        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("登録時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> RegistCheckMethod { get; set; }
        private bool ShouldSerializeRegistCheckMethod()
        {
            return this.RegistCheckMethod != new Collection<SelectCheckDto>();
        }

        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("フォーカスアウト時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> FocusOutCheckMethod { get; set; }
        private bool ShouldSerializeFocusOutCheckMethod()
        {
            return this.FocusOutCheckMethod != new Collection<SelectCheckDto>();
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [TypeConverter(typeof(PopupWindowConverter))]
        [Description("スペースキー押下時に起動したいポップアップ画面を選択してください。")]
        public string PopupWindowName { get; set; }
        private bool ShouldSerializePopupWindowName()
        {
            return this.PopupWindowName != null;
        }

        [Category("EDISONプロパティ")]
        [Browsable(false)]
        public string ErrorMessage { get; set; }
        private bool ShouldSerializeErrorMessage()
        {
            return this.ErrorMessage != null;
        }

        [Category("EDISONプロパティ")]
        [Browsable(false)]
        public Color DefaultBackColor { get; set; }
        private bool ShouldSerializeDefaultBackColor()
        {
            return this.DefaultBackColor != null;
        }

        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、取得を行うフィールド名を「,」区切りで入力してください。")]
        public string GetCodeMasterField { get; set; }
        private bool ShouldSerializeGetCodeMasterField()
        {
            return this.GetCodeMasterField != null;
        }

        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string SetFormField { get; set; }
        private bool ShouldSerializeSetFormField()
        {
            return this.SetFormField != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("入力可能な最大桁数を指定してください。")]
        public decimal CharactersNumber { get; set; }
        private bool ShouldSerializeCharactersNumber()
        {
            return this.CharactersNumber != 0;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("trueの場合には「CharactersNumber」に指定した桁数までフォーカスアウト時に0埋めを行います。")]
        public bool ZeroPaddengFlag { get; set; }
        private bool ShouldSerializeZeroPaddengFlag()
        {
            return this.ZeroPaddengFlag != false;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップに表示する画面の種類を選んでください。")]
        public WINDOW_ID PopupWindowId { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップでマルチセレクトをする場合に指定してください。")]
        public bool PopupMultiSelect { get; set; }
        private bool ShouldSerializePopupMultiSelect()
        {
            return this.PopupMultiSelect != false;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップで取得済データを設定する場合に指定してください。")]
        public DataTable PopupDataSource { get; set; }
        private bool ShouldSerializePopupDataSource()
        {
            return this.PopupDataSource != null;
        }

        /// <summary>
        /// ポップアップへ送信するコントロール
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        public string[] PopupSendParams { get; set; }
        private bool ShouldSerializePopupSendParams()
        {
            return this.PopupSendParams != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップの表示条件を選んでください。")]
        public Collection<JoinMethodDto> popupWindowSetting { get; set; }

        /// <summary>
        /// 検索ポップアップへ送信する値
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("検索ポップアップで絞込みを行うため、テーブルのキー名と値を指定")]
        public Collection<PopupSearchSendParamDto> PopupSearchSendParams { get; set; }
        private bool ShouldSerializePopupSearchSendParams()
        {
            return this.PopupSearchSendParams != null;
        }

        //日付
        [Category("EDISONプロパティ_画面設定")]
        [Description("曜日を表示する場合はTrueにしてください[yyyy/MM/dd(ddd)]")]
        [DefaultValue(true)]
        public bool ShowYoubi
        {
            get;
            set;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("日付の最大値を指定してください。")]
        public DateTime MaxValue { get; set; }
        private bool ShouldSerializeMaxValue()
        {
            //Default属性が使えない場合は ShouldSerialize を前に着けた関数を用意するとロジックで判定できる
            return this.MaxValue != System.Data.SqlTypes.SqlDateTime.MaxValue.Value;
        }


        [Category("EDISONプロパティ_画面設定")]
        [Description("日付の最小値を指定してください。")]
        public DateTime MinValue { get; set; }
        private bool ShouldSerializeDateTime()
        {
            return this.MinValue != System.Data.SqlTypes.SqlDateTime.MinValue.Value;
        }

        //年の表示有無
        [Category("EDISONプロパティ_画面設定")]
        [Description("年のみを表示する場合はTrueにしてください[yyyy]")]
        [DefaultValue(false)]
        public bool DisplayOnlyYear
        {
            get;
            set;
        }

        //年
        public string YearValue;
        [Category("EDISONプロパティ_画面設定")]
        [Description("年を設定してください。")]
        public string DateTimeNowYear
        {
            get
            {
                return this.YearValue;
            }
            set
            {
                this.YearValue = value;
            }
        }

        /// <summary>
        /// Null判定フラグ
        /// </summary>
        private bool _isNull;

        /// <summary>
        /// Nullの場合の値
        /// </summary>
        private string _nullValue;

        /// <summary>
        /// DateTimePickerフォーマット
        /// </summary>
        private DateTimePickerFormat _datetimeFormat = DateTimePickerFormat.Long;

        /// <summary>
        /// Maskフォーマット
        /// </summary>
        private MaskFormat _format = new MaskFormat();

        /// <summary>
        /// フォント
        /// </summary>
        private Font _font = new Font("ＭＳ ゴシック", 9F);

        /// <summary>
        /// チェック有無
        /// </summary>
        private bool _checked;

        /// <summary>
        /// フォーマットを元にstringを設定する
        /// </summary>
        private string _formatAsString;

        [Category("EDISONプロパティ_ポップアップ設定")]
        public string PopupGetMasterField { get; set; }
        private bool ShouldSerializePopupGetMasterField()
        {
            return this.PopupGetMasterField != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string PopupSetFormField { get; set; }
        private bool ShouldSerializePopupSetFormField()
        {
            return this.PopupSetFormField != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップから戻ってきた後に実行させたいメソッド名を指定してください。")]
        public string PopupAfterExecuteMethod { get; set; }
        private bool ShouldSerializePopupAfterExecuteMethod()
        {
            return this.PopupAfterExecuteMethod != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップが開く前に実行させたいメソッド名を指定してください。")]
        public string PopupBeforeExecuteMethod { get; set; }
        private bool ShouldSerializePopupBeforeExecuteMethod()
        {
            return this.PopupBeforeExecuteMethod != null;
        }

        [Category("EDISONプロパティ_チェック設定")]
        public string ClearFormField { get; set; }
        private bool ShouldSerializeClearFormField()
        {
            return this.ClearFormField != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        public string PopupClearFormField { get; set; }
        private bool ShouldSerializePopupClearFormField()
        {
            return this.PopupClearFormField != null;
        }
        #endregion

        /// <summary>
        /// カスタムフォーマット設定
        /// </summary>
        public new String CustomFormat { get; set; }
        private bool ShouldSerializeCustomFormat()
        {
            return this.CustomFormat != null;
        }

        /// <summary>
        /// null値の設定
        /// </summary>
        public String NullValue
        {
            get { return _nullValue; }
            set { _nullValue = value; }
        }

        /// <summary>
        /// 名称取得
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.Name;
        }

        ///// <summary>
        ///// ヒントテキスト生成処理
        ///// </summary>
        //public void CreateHintText()
        //{
        //    if (this.DesignMode)
        //    {
        //        return;
        //    }
        //    if (this.Tag == null || string.IsNullOrEmpty(this.Tag.ToString()))
        //    {
        //        this.Tag = ControlUtility.CreateHintText(this);
        //    }
        //}

        /// <summary>
        /// 入力値の取得処理
        /// </summary>
        public string GetResultText()
        {
            if (this.Text == null)
            {
                return string.Empty;
            }
            else
            {
                return this.Text.ToString();
            }
        }

        /// <summary>
        /// 値の設定処理
        /// </summary>
        public void SetResultText(string value)
        {
            if (string.IsNullOrEmpty(value) || Constans.NULL_STRING == value)
            {
                this.Value = null;
            }
            else
            {
                this.Value = value.ToString();
            }
        }

        /// <summary>
        /// 設定されている値
        /// </summary>
        public new Object Value
        {
            get
            {
                if (string.IsNullOrEmpty(this.Text))
                {
                    return null;
                }

                string val = this.Text;

                if (this.DisplayOnlyYear)
                {
                    val = val + "/01/01";
                }

                DateTime outResult = new DateTime();
                if (DateTime.TryParseExact(val, CustomDateTimePicker.dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out outResult))
                {
                    return DateTime.ParseExact(val, CustomDateTimePicker.dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None);
                }
                else
                {
                    return this.Text;
                }
            }
            set
            {
                bool check = false;
                string val = "";
                if (value == null || value == DBNull.Value)
                {
                    val = "";
                }
                else if (value is DateTime)
                {
                    val = ((DateTime)value).ToString("yyyy/MM/dd");
                    check = true;
                }
                else if (this.DisplayOnlyYear)
                {
                    val = value.ToString() + "/01/01";
                }
                else
                {
                    if (value.ToString().Length >= 10)
                    {
                        val = value.ToString().Substring(0, 10);
                    }
                    else
                    {
                        val = value.ToString();
                    }
                }

                if (string.IsNullOrEmpty(val))
                {
                    this.Text = "";
                    return;
                }
                else
                {
                    try
                    {
                        DateTime dt = DateTime.ParseExact(val, CustomDateTimePicker.dateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None);

                        if (this.DisplayOnlyYear)
                        {
                            if (check)
                            {
                                CorpInfoUtility corpInfoUtil = new CorpInfoUtility();
                                int year = corpInfoUtil.GetCurrentYear(dt);
                                this.Text = year.ToString();
                            }
                            else
                            {
                                this.Text = dt.ToString("yyyy");
                            }
                        }
                        else if (this.ShowYoubi)
                        {
                            if (this.Focused)
                            {
                                this.Text = dt.ToString("yyyy/MM/dd");
                            }
                            else
                            {
                                this.Text = dt.ToString("yyyy/MM/dd(ddd)");
                            }
                        }
                        else
                        {
                            // 暫定：年月フォーマットのみ対応
                            this.Text = dt.ToString(this.isFormatMonth() ? this.CustomFormat : "yyyy/MM/dd");
                        }
                    }
                    catch
                    {
                        this.Text = val;
                    }
                }

            }
        }

        /// <summary>
        /// フォーマットの設定処理
        /// </summary>
        private void SetFormat()
        {
            CultureInfo ci = Thread.CurrentThread.CurrentCulture;
            DateTimeFormatInfo dtf = ci.DateTimeFormat;
            switch (_format)
            {
                default:
                    break;
            }
        }

        /// <summary>
        /// フォーマットにて取得したデータ
        /// </summary>
        private string FormatAsString
        {
            get { return _formatAsString; }
            set
            {
                _formatAsString = value;
            }
        }

        /// <summary>
        ///  DateTimePickerカスタムフォーマット設定
        /// </summary>
        public new DateTimePickerFormat Format
        {
            get { return _datetimeFormat; }
            set
            {
                _datetimeFormat = value;
            }
        }

        /// <summary>
        /// カレンダーフォント設定
        /// </summary>
        public new Font CalendarFont
        {
            get { return _font; }
            set
            {
                _font = value;
            }
        }


        //  背景色更新

        protected override void OnEnabledChanged(EventArgs e)
        {
            this.UpdateBackColor(false);
            base.OnEnabledChanged(e);
        }
        protected override void OnReadOnlyChanged(EventArgs e)
        {
            this.UpdateBackColor();
            base.OnReadOnlyChanged(e);
        }

        /// <summary>
        /// 貼り付け防止処理
        /// </summary>
        const int WM_PASTE = 0x302;

        /// <summary>
        /// ペイント処理
        /// </summary>
        /// <param name="m"></param>
        [System.Security.Permissions.SecurityPermission(
            System.Security.Permissions.SecurityAction.LinkDemand,
            Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_PASTE)
            {
                IDataObject iData = Clipboard.GetDataObject();
                //文字列がクリップボードにあるか
                if (iData != null && iData.GetDataPresent(DataFormats.Text))
                {
                    string clipStr = (string)iData.GetData(DataFormats.Text);
                    //クリップボードの文字列が数字のみか調べる
                    if (!System.Text.RegularExpressions.Regex.IsMatch(
                        clipStr,
                        @"^[0-9/]+$"))
                    {
                        return;
                    }
                }
            }

            base.WndProc(ref m);
        }

        /// <summary>
        /// キー押下時処理
        /// </summary>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // フォーカス移動はSuperForm,～BaseFormのKeyDownで処理するため
                // Tabボタンも通常キー入力とする
                e.IsInputKey = true;
            }

            base.OnPreviewKeyDown(e);
        }

        /// <summary>
        /// OnEnter中かどうかのフラグ OnEnter以外では変えてはいけない
        /// </summary>
        protected bool _entering = false;
        /// <summary>
        /// フォーカス取得
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            _entering = true;
            try
            {
                this.UpdateBackColor(true);//拡張メソッド

                base.OnEnter(e);

                SuperForm superForm;
                if (ControlUtility.TryGetSuperForm(this, out superForm)
                    && superForm.PreviousSaveFlag)
                {
                    // 前回値保持
                    superForm.SetPreviousValue(this.GetResultText(), this);

                    var cstmLogic = new CustomControlLogic(this);
                    object[] obj = cstmLogic.GetPreviousControl();
                    superForm.SetPreviousControlValue(obj);
                }
                base.Focus();
                this.ChangeDateTimeFormat(false);
                base.SelectAll();
            }
            finally
            {
                _entering = false;
            }
        }

        // No.3873-->
        /// <summary>
        /// Clickイベントハンドラ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (this.ReadOnly == false && isSelectAll == false)
            {
                base.SelectAll();   // 全選択にする
                isSelectAll = true;
            }
        }

        /// <summary>
        /// 日付のフォーマット変換を行う
        /// </summary>
        public void ChangeDateTimeFormat(bool convert)
        {
            //ReadOnly対応
            if (this.ReadOnly) return;

            string value = this.Text;

            if (string.IsNullOrEmpty(value)) return; //空の場合は処理不要


            DateTime dateTime;
            string val = String.Empty;
            // 暫定：年月フォーマットのみ対応
            string format = this.isFormatMonth() ? this.CustomFormat : "yyyy/MM/dd";

            if (DateTime.TryParse(value, out dateTime))
            {
                if (convert)
                {
                    if (this.ShowYoubi)
                    {
                        format = "yyyy/MM/dd(ddd)";
                    }
                }
                val = dateTime.ToString(format);
                //this.Value = val; //内部ではvalueは極力使わないように         
                this.Text = val;
            }
        }

        /// <summary>
        /// 自動チェック処理。
        /// 閉じるボタンなどはCausesValidation=falseにすることで、クリック時にチェックを走らせないことが可能。
        /// </summary>
        /// <param name="e">チェックNGの場合はcancel=trueでフォーカス移動をキャンセル</param>
        protected override void OnValidating(CancelEventArgs e)
        {
            if (this.ReadOnly) return; //読み取り専用の場合処理しない

            // 自動チェック処理
            var cstmLogic = new CustomControlLogic(this);
            var ctrlUtil = new ControlUtility();
            var fields = ctrlUtil.GetAllControls(ControlUtility.GetTopControl(this));
            var mthodList = this.FocusOutCheckMethod;

            if (this.DisplayOnlyYear)
            {
                if (!this.CheckYear())
                {
                    this.IsInputErrorOccured = true;
                    e.Cancel = true;
                    return;
                }
                this.IsInputErrorOccured = false;
            }
            else
            {
                if (!this.CheckInputDateTime())
                {
                    this.IsInputErrorOccured = true;
                    e.Cancel = true;
                    return;
                }

                this.IsInputErrorOccured = false;
                // this.Value = base.Text;
                SuperForm superForm;
                if (mthodList != null && mthodList.Count != 0 && ControlUtility.TryGetSuperForm(this, out superForm))
                {
                    e.Cancel = cstmLogic.StartingFocusOutCheck(this, fields, superForm);
                    if (e.Cancel)
                    {
                        return;
                    }
                }
            }

            if (!e.Cancel)
            {
                //validating成功時のみ値変更とする
                this.UpdateBackColor(false); //validating成功時は フォーカスは通常で！
                this.OnValueChanged();
            }

            base.OnValidating(e);
        }

        /// <summary>
        /// 年のチェック行う
        /// </summary>
        /// <returns></returns>
        public bool CheckYear()
        {
            string value = base.Text;

            if (value.Trim().Length == 0) { return true; }

            var _maxdate = this.MaxValue;
            var _mindate = this.MinValue;
            DateTime dt;
            //複数書式でパース率を上げる
            string[] formats = { "yyyy", "yy" };

            if (DateTime.TryParseExact(value, formats, null, System.Globalization.DateTimeStyles.None, out dt))
            {
                if (dt.Year > _maxdate.Year)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E042", "[ " + _maxdate + " ]以下");
                    return false;
                }
                else if (dt.Year < _mindate.Year)
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E042", "[ " + _mindate + " ]以上");
                    return false;
                }
            }
            else
            {
                var messageShowLogic = new MessageBoxShowLogic();
                messageShowLogic.MessageBoxShow("E084", value);
                return false;
            }
            this.YearValue = dt.Year.ToString();
            this.Text = dt.Year.ToString();
            return true;
        }

        /// <summary>
        /// 日付のチェック行う
        /// </summary>
        /// <returns></returns>
        public bool CheckInputDateTime()
        {
            if (string.IsNullOrEmpty(this.Text)) return true;

            DateTime dateTime;

            //日付チェックを共通化
            string errmsg = Validator.CheckDateTimeString(this.Text, this.MaxValue, this.MinValue, out dateTime);
            if (!string.IsNullOrEmpty(errmsg))
            {
                Shougun.Core.Message.MessageBoxUtility.MessageBoxShowError(errmsg);
                return false;
            }

            string dateValue = String.Empty;
            if (this.ShowYoubi)
            {
                dateValue = ((DateTime)dateTime).ToString("yyyy/MM/dd(ddd)");
            }
            else
            {
                // 暫定：年月フォーマットのみ対応
                dateValue = ((DateTime)dateTime).ToString(this.isFormatMonth() ? this.CustomFormat : "yyyy/MM/dd");
            }
            base.Text = dateValue;
            return true;
        }

        /// <summary>
        /// 半角チェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool isHankaku(string str)
        {
            int num = sjisEnc.GetByteCount(str);
            return num == str.Length;
        }

        /// <summary>
        /// オブジェクトが数値であるかどうかを返します
        /// </summary>
        /// <param name="stTarget"></param>
        /// <returns>
        ///  指定したオブジェクトが数値であれば true。それ以外は false。</returns>
        public bool IsNumeric(string stTarget)
        {
            decimal dNullable;

            return decimal.TryParse(
                stTarget,
                System.Globalization.NumberStyles.Any,
                null,
                out dNullable
            );
        }

        /// <summary>
        /// 全角チェック
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool isZenkaku(string str)
        {
            int num = sjisEnc.GetByteCount(str);
            return num == str.Length * 2;
        }

        /// <summary>
        /// OnLeave中かどうかのフラグ OnLeave以外では変えてはいけない
        /// </summary>
        protected bool _leaving = false;
        /// <summary>
        /// 値チェックはValidatingを利用してください。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            this._leaving = true;
            try
            {
                base.OnLeave(e);
                //this.isInputErrorOccured = false; //エラーはLeaveでは残す
                this.ChangeDateTimeFormat(true);
                this.UpdateBackColor(false);//拡張メソッド

                isSelectAll = false;    // No.3873
            }
            finally
            {
                this._leaving = false;
            }
        }

        /// <summary>
        /// データタイムを設定する処理
        /// </summary>
        private void SetToDateTimeValue()
        {
            if (_isNull)
            {
                SetFormat();
                _isNull = false;
                this.OnTextChanged(eva);
            }
        }

        /// <summary>
        /// キーアップイベント
        /// </summary>
        /// <param name="e">イベントハンドラ</param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Delete)
            //{
            //   // this.Value = null;
            //    //OnTextChanged(eva);
            //}
            //else
            //{
            //なにかキーを押されたら、元の値を復元。（その際内部の値は変わってないので自前でChangeイベントを呼ぶ必要あり
            if (Control.MouseButtons == MouseButtons.None && _isNull)
            {
                switch (e.KeyCode)
                {
                    case Keys.Tab:
                    case Keys.Enter:
                    case Keys.Escape:
                    case Keys.F1:
                    case Keys.F2:
                    case Keys.F3:
                    case Keys.F4:
                    case Keys.F5:
                    case Keys.F6:
                    case Keys.F7:
                    case Keys.F8:
                    case Keys.F9:
                    case Keys.F10:
                    case Keys.F11:
                    case Keys.F12:
                        break; //タブやエンター、ファンクション系は何もしない
                    default:
                        SetToDateTimeValue(); //nullの時は値を復元、nullでない場合は何もしない。
                        break;
                }
                // }
            }
            base.OnKeyUp(e);
        }

        /// <summary>
        /// 全角/半角または全角スペースのキーコード
        /// </summary>
        private const int WIDE_SPACE_OR_ZENKAKU_HANKAKU = 229;

        /// <summary>
        /// KeyDownで「全角/半角または全角スペースのキーコード」が来たかどうか
        /// </summary>
        private bool downSpace = false;

        /// <summary>
        /// KeyDownで修飾キーとして「Ctrlキー」が押されたかどうか
        /// </summary>
        private bool isControlKeyDown;

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            //0-9 バックスペース、スラッシュを許可
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == '\b' || e.KeyChar == '/')
            {
                //数値は入力可
                //年のみj表示の場合
                if (this.DisplayOnlyYear)
                {
                    if (base.Text.Length >= 4 && e.KeyChar != '\b')
                    {
                        if (base.Text.Length == 4)
                        {
                            base.Text = "";
                        }
                        else
                        {
                            e.Handled = true; //何もさせない
                        }
                    }
                }
            }
            else
            {
                if (!this.isControlKeyDown)
                {
                    this.isControlKeyDown = false;
                    e.Handled = true; //何もさせない
                }
            }

            //全角スペースチェック KeyDownだと漢字ボタンと区別がつかないため、KeyPressで判断が必要(SHift+spaceで半角も来る）
            if (this.downSpace && (e.KeyChar == '　' || e.KeyChar == ' '))
            {
                if (this is IDataGridViewEditingControl)
                {
                    //エディティングコントロールの場合はGridのイベントで処理するので、ここでは何もしない
                }
                //ポップアップ設定がない場合は 自動設定
                else if (string.IsNullOrEmpty(this.PopupWindowName))
                {
                    string bk = this.PopupSetFormField;
                    try
                    {
                        this.PopupWindowName = "カレンダーポップアップ";
                        this.PopupSetFormField = this.Name;
                        this.PopUp();

                    }
                    finally
                    {
                        this.PopupWindowName = "";
                        this.PopupSetFormField = bk;
                    }


                }
                else
                {
                    //設定がある場合はそれに従う
                    this.PopUp();
                }

                e.Handled = true;//入力キャンセル
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        /// キーダウン処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {

                if (this is IDataGridViewEditingControl)
                {
                    //エディティングコントロールの場合はGridのイベントで処理するので、ここでは何もしない
                }
                // テキストボックスにてスペースキー押下時の処理
                // ポップアップ画面が設定されている場合は、表示を行う
                //ポップアップ設定がない場合は 自動設定
                else if (string.IsNullOrEmpty(this.PopupWindowName))
                {
                    string bk = this.PopupSetFormField;
                    try
                    {
                        this.PopupWindowName = "カレンダーポップアップ";
                        this.PopupSetFormField = this.Name;
                        this.PopUp();

                    }
                    finally
                    {
                        this.PopupWindowName = "";
                        this.PopupSetFormField = bk;
                    }

                }
                else
                {
                    //設定がある場合はそれに従う
                    this.PopUp();
                }

                e.Handled = true;
            }

            //全角スペースチェック KeyDownだと漢字ボタンと区別がつかないため、KeyPressで判断が必要
            if ((int)e.KeyCode == WIDE_SPACE_OR_ZENKAKU_HANKAKU)
            {
                this.downSpace = true;
            }
            else
            {
                this.downSpace = false;
            }

            this.isControlKeyDown = false;
            if (e.Modifiers == Keys.Control)
            {
                this.isControlKeyDown = true;
            }

            base.OnKeyDown(e);
        }

        /// <summary>
        /// null値が設定された場合
        /// </summary>
        private void SetToNullValue()
        {
            _isNull = true;
            base.Text = NullValue;
        }

        /// <summary>
        /// このクラスで発行したかどうか管理するための静的変数
        /// </summary>
        private static EventArgs eva = new EventArgs();

        /// <summary>
        /// 値が変更されたときに発生するイベント
        /// </summary>
        /// <param name="eventargs">コモンコントロールで発生している場合EventArgs.Empty、このクラスで発生している場合はevaになる</param>
        protected override void OnTextChanged(EventArgs eventargs)
        {
            //入力がある場合色をリセット
            if (!this._entering && !this._leaving)
            {
                this.isInputErrorOccured = false;
            }

            //実行する
            base.OnTextChanged(eventargs);

            //8ケタ数値のみで入力したときだけ自動でフォーマット
            if (this.Text.Length == 8)
            {
                DateTime dt;
                if (DateTime.TryParseExact(this.Text, "yyyyMMdd", null, DateTimeStyles.None, out dt))
                {

                    this.Text = dt.ToString("yyyy/MM/dd");
                    this.SelectionStart = this.TextLength;
                }
            }

            //OnEnterの途中では処理NG（曜日が付与される）
            if (!_entering)
            {
                //時間も無理やり画面でセットされた場合の対策
                if (this.Text.Contains(":") || this.Text.Contains("-") || !this.Focused)
                {
                    if (!this.DisplayOnlyYear)
                    {
                        Value = this.Text; // ToString()だと yyyy/MM/dd HH:mm:ss などでセットされるので 左だけに切る
                    }
                }
            }
        }

        //CheckedがfalseだとTextとValueの値が異なる現象が発生するため。
        /// <summary>
        /// 値をセットしても何もおきません。※既存APがデザイナで値を設定しているため。
        /// 取得する場合は。baseの値を返します。
        /// </summary>
        [Obsolete("CheckedがfalseだとTextとValueの値が異なる現象が発生するため、このプロパティのSetterは動作しないように修正しています。本当にCheckedを利用したい場合はSetChecked、GetCheckedを利用してください。")]
        public bool Checked
        {
            get { return _checked; }
            set { _checked = value; }
        }

        /// <summary>
        /// base実行後、デザイナでの初期値（コントロールを配置した日時）をNowで上書きしています。
        /// </summary>
        /// <param name="e">未使用</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            //this.Value = DateTime.Today; //デザイナでの初期値を上書きする ※時間を含めると1秒ずれてもFromToチェックで引っかかるため
        }

        [Description("Textプロパティの値がコントロールで変更されたときに発生するイベントです。")]
        public new event EventHandler ValueChanged;
        private void OnValueChanged()
        {
            if (!(this.ValueChanged == null))
                this.ValueChanged(this, EventArgs.Empty);
        }

        private bool _ReadOnlyPopUp = false;
        /// <summary>
        /// ポップアップを読み取り専用でも出すかどうかを設定します
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("読み取り専用でもポップアップを起動する場合はTrueにしてください")]
        [DefaultValue(false)]
        public bool ReadOnlyPopUp
        {
            get
            {
                return _ReadOnlyPopUp;
            }
            set
            {
                _ReadOnlyPopUp = value;
            }
        }
        /// <summary>
        /// ポップアップのタイトルを強制的に上書きする場合は設定してください
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップのタイトルを強制的に上書きする場合は設定してください")]
        public string PopupTitleLabel { get; set; }
        private bool ShouldSerializePopupTitleLabel()
        {
            return !string.IsNullOrEmpty(this.PopupTitleLabel);
        }

        /// <summary>年月フォーマットかどうか</summary>
        /// <returns></returns>
        private bool isFormatMonth()
        {
            return this.Format == DateTimePickerFormat.Custom && this.CustomFormat == "yyyy/MM";
        }

        /// <summary>ポップアップを開く前に実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl> PopupBeforeExecute { get; set; }

        /// <summary>ポップアップから戻ってきたら実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl, System.Windows.Forms.DialogResult> PopupAfterExecute { get; set; }

    }
}
