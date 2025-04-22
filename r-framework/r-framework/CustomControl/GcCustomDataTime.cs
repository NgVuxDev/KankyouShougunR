using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Const;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;
using r_framework.Utility;
using r_framework.Logic;

namespace r_framework.CustomControl
{
    /// <summary>
    /// MultiRowにて使用する、日付入力項目のカスタムコントロール
    /// </summary>
    public partial class GcCustomDataTime : GcCustomTextBoxCell, ICustomControl
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GcCustomDataTime()
        {
            InitializeComponent();
            this.Style.Font = Constans.DEFAULT_MULTIROW_FONT;
            this.Style.InputScope = InputScopeNameValue.Default;
            this.Style.ImeMode = ImeMode.Disable;

            // FormFieldのコピー
            if (string.IsNullOrEmpty(this.PopupSetFormField))
            {
                this.PopupSetFormField = this.SetFormField;
            }
            if (string.IsNullOrEmpty(this.PopupGetMasterField))
            {
                this.PopupGetMasterField = this.GetCodeMasterField;
            }

            //デフォルト
            this.ShowYoubi = true;
            this.MaxValue = System.Data.SqlTypes.SqlDateTime.MaxValue.Value;
            this.MinValue = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
            this.Format = DateTimePickerFormat.Custom;
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(CellPaintingEventArgs e)
        {
            base.OnPaint(e);
            if (RegistCheckMethod == null)
            {
                RegistCheckMethod = new Collection<SelectCheckDto>();
            }
            if (FocusOutCheckMethod == null)
            {
                FocusOutCheckMethod = new Collection<SelectCheckDto>();
            }
            if (popupWindowSetting == null)
            {
                popupWindowSetting = new Collection<JoinMethodDto>();
            }
            if (PopupSearchSendParams == null)
            {
                PopupSearchSendParams = new Collection<PopupSearchSendParamDto>();
            }
        }

        /// <summary>
        /// フォーカスイン処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(CellEventArgs e)
        {
            //this.ClearUnlock();
            base.OnEnter(e);
        }

        /// <summary>
        /// クリック処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(CellEventArgs e)
        {
            //this.ClearUnlock();
            base.OnClick(e);
        }

        /// <summary>
        /// 複製処理
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            GcCustomDataTime myCustomDataTime = base.Clone() as GcCustomDataTime;
            
            //this.CopyFields(myCustomDataTime); ゆくゆくは共通化すべき

            myCustomDataTime.DBFieldsName = this.DBFieldsName;
            myCustomDataTime.ItemDefinedTypes = this.ItemDefinedTypes;
            myCustomDataTime.DisplayItemName = this.DisplayItemName;
            myCustomDataTime.ShortItemName = this.ShortItemName;
            myCustomDataTime.SearchDisplayFlag = this.SearchDisplayFlag;
            myCustomDataTime.RegistCheckMethod = this.RegistCheckMethod;
            myCustomDataTime.FocusOutCheckMethod = this.FocusOutCheckMethod;
            myCustomDataTime.PopupWindowName = this.PopupWindowName;
            myCustomDataTime.ErrorMessage = this.ErrorMessage;
            myCustomDataTime.DefaultBackColor = this.DefaultBackColor;
            myCustomDataTime.GetCodeMasterField = this.GetCodeMasterField;
            myCustomDataTime.SetFormField = this.SetFormField;
            myCustomDataTime.CharactersNumber = this.CharactersNumber;
            myCustomDataTime.ZeroPaddengFlag = this.ZeroPaddengFlag;
            myCustomDataTime.PopupWindowId = this.PopupWindowId;
            myCustomDataTime.PopupMultiSelect = this.PopupMultiSelect;
            myCustomDataTime.PopupDataSource = this.PopupDataSource;
            myCustomDataTime.popupWindowSetting = this.popupWindowSetting;
            myCustomDataTime.PopupSearchSendParams = this.PopupSearchSendParams;
            myCustomDataTime.PopupSetFormField = this.PopupSetFormField;
            myCustomDataTime.PopupAfterExecuteMethod = this.PopupAfterExecuteMethod;
            myCustomDataTime.PopupBeforeExecuteMethod = this.PopupBeforeExecuteMethod;
            myCustomDataTime.ShowYoubi = this.ShowYoubi;

            myCustomDataTime.ClearFormField = this.ClearFormField;
            myCustomDataTime.PopupClearFormField = this.PopupClearFormField;
            return myCustomDataTime;
        }

        #region Property

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_画面設定")]
        //[Description("対応するDBのフィールド名を記述してください。")]
        //public string DBFieldsName { get; set; }
        //private bool ShouldSerializeDBFieldsName()
        //{
        //    return this.DBFieldsName != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_画面設定")]
        //[Description("対応するDBフィールドの型名を指定してください(varchar等)")]
        //public string ItemDefinedTypes { get; set; }
        //private bool ShouldSerializeItemDefinedTypes()
        //{
        //    return this.ItemDefinedTypes != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_画面設定")]
        //[Description("画面に表示する項目の日本語名を指定してください。")]
        //public string DisplayItemName { get; set; }
        //private bool ShouldSerializeDisplayItemName()
        //{
        //    return this.DisplayItemName != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_画面設定")]
        //[Description("画面に表示する項目の日本語短縮名を指定してください。")]
        //public string ShortItemName { get; set; }
        //private bool ShouldSerializeShortItemName()
        //{
        //    return this.ShortItemName != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_画面設定")]
        //[Description("汎用検索画面に表示するかのフラグを設定してください(使用方法未定)")]
        //public int SearchDisplayFlag { get; set; }
        //private bool ShouldSerializeSearchDisplayFlag()
        //{
        //    return this.SearchDisplayFlag != 0;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_チェック設定")]
        //[System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        //[Description("登録時に行うチェックを選んでください。")]
        //public Collection<SelectCheckDto> RegistCheckMethod { get; set; }
        //private bool ShouldSerializeRegistCheckMethod()
        //{
        //    return this.RegistCheckMethod != new Collection<SelectCheckDto>();
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_チェック設定")]
        //[System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        //[Description("フォーカスアウト時に行うチェックを選んでください。")]
        //public Collection<SelectCheckDto> FocusOutCheckMethod { get; set; }
        //private bool ShouldSerializeFocusOutCheckMethod()
        //{
        //    return this.FocusOutCheckMethod != new Collection<SelectCheckDto>();
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //[TypeConverter(typeof(PopupWindowConverter))]
        //[Description("スペースキー押下時に起動したいポップアップ画面を選択してください。")]
        //public string PopupWindowName { get; set; }
        //private bool ShouldSerializePopupWindowName()
        //{
        //    return this.PopupWindowName != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ")]
        //[Browsable(false)]
        //public string ErrorMessage { get; set; }
        //private bool ShouldSerializeErrorMessage()
        //{
        //    return this.ErrorMessage != null;
        //}

        [Category("EDISONプロパティ")]
        [Browsable(false)]
        public Color DefaultBackColor { get; set; }
        private bool ShouldSerializeDefaultBackColor()
        {
            return this.DefaultBackColor != null;
        }

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_チェック設定")]
        //[Description("マスタチェック時に存在した場合、値の設定を行うならば、取得を行うフィールド名を「,」区切りで入力してください。")]
        //public string GetCodeMasterField { get; set; }
        //private bool ShouldSerializeGetCodeMasterField()
        //{
        //    return this.GetCodeMasterField != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_チェック設定")]
        //[Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        //public string SetFormField { get; set; }
        //private bool ShouldSerializeSetFormField()
        //{
        //    return this.SetFormField != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_画面設定")]
        //[Description("入力可能な最大桁数を指定してください。")]
        //public Decimal CharactersNumber { get; set; }
        //private bool ShouldSerializeCharactersNumber()
        //{
        //    return this.CharactersNumber != 0;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_画面設定")]
        //[Description("trueの場合には「CharactersNumber」に指定した桁数までフォーカスアウト時に0埋めを行います。")]
        //public bool ZeroPaddengFlag { get; set; }
        //private bool ShouldSerializeZeroPaddengFlag()
        //{
        //    return this.ZeroPaddengFlag != false;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //[Description("ポップアップに表示する画面の種類を選んでください。")]
        //public WINDOW_ID PopupWindowId { get; set; }
        //private bool ShouldSerializePopupWindowId()
        //{
        //    return this.PopupWindowId != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        ///// <summary>
        ///// ポップアップで複数選択の設定を行うプロパティ
        ///// </summary>
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //[Description("ポップアップでマルチセレクトをする場合に指定してください。")]
        //public bool PopupMultiSelect { get; set; }
        //private bool ShouldSerializePopupMultiSelect()
        //{
        //    return this.PopupMultiSelect != false;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        ///// <summary>
        ///// ポップアップで取得済データを設定するプロパティ
        ///// </summary>
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //[Description("ポップアップで取得済データを設定する場合に指定してください。")]
        //public DataTable PopupDataSource { get; set; }
        //private bool ShouldSerializePopupDataSource()
        //{
        //    return this.PopupDataSource != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        ///// <summary>
        ///// ポップアップへ送信するコントロール
        ///// </summary>
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //public string[] PopupSendParams { get; set; }
        //private bool ShouldSerializePopupSendParams()
        //{
        //    return this.PopupSendParams != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //[Description("ポップアップの表示条件を選んでください。")]
        //public Collection<JoinMethodDto> popupWindowSetting { get; set; }

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        ///// <summary>
        ///// 検索ポップアップへ送信する値
        ///// </summary>
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //[Description("検索ポップアップで絞込みを行うため、テーブルのキー名と値を指定")]
        //public Collection<PopupSearchSendParamDto> PopupSearchSendParams { get; set; }
        //private bool ShouldSerializePopupSearchSendParams()
        //{
        //    return this.PopupSearchSendParams != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //public string PopupGetMasterField { get; set; }
        //private bool ShouldSerializePopupGetMasterField()
        //{
        //    return this.PopupGetMasterField != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //[Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        //public string PopupSetFormField { get; set; }
        //private bool ShouldSerializePopupSetFormField()
        //{
        //    return this.PopupSetFormField != null;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //[Description("ポップアップから戻ってきた後に実行させたいメソッド名を指定してください。")]
        //public string PopupAfterExecuteMethod { get; set; }
        //private bool ShouldSerializePopupAfterExecuteMethod()
        //{
        //    return this.PopupAfterExecuteMethod != null;
        //}


        //日付独自
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

        #endregion

        /// <summary>
        /// 初期化中ロック
        /// </summary>
        [Browsable(false)]
        [Obsolete("未使用・互換性保持目的")]
        public bool IsClearLock { get; private set; }


        /// <summary>
        /// 入力値の取得処理
        /// </summary>
        override public string GetResultText()
        {
            return Convert.ToString(this.Value);
        }

        /// <summary>
        /// 値の設定処理
        /// </summary>
        override public void SetResultText(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.Value = Convert.ToDateTime(value);
            }
        }

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        ///// <summary>
        ///// コントロール名取得
        ///// </summary>
        ///// <returns></returns>
        //public string GetName()
        //{
        //    return this.Name;
        //}

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        ///// <summary>
        ///// ヒントテキスト設定処理
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

        //継承元で定義しているので 上書きするとキャスト時におかしくなります！
        //private bool _ReadOnlyPopUp = false;
        ///// <summary>
        ///// ポップアップを読み取り専用でも出すかどうかを設定します
        ///// </summary>
        //[Category("EDISONプロパティ_ポップアップ設定")]
        //[Description("読み取り専用でもポップアップを起動する場合はTrueにしてください")]
        //[DefaultValue(false)]
        //public bool ReadOnlyPopUp
        //{
        //    get
        //    {
        //        return _ReadOnlyPopUp;
        //    }
        //    set
        //    {
        //        _ReadOnlyPopUp = value;
        //    }
        //}
        ///// <summary>
        ///// 初期化中ロック
        ///// </summary>
        //public void ClearLock()
        //{
        //    this._readOnly = this.ReadOnly;
        //    this.ReadOnly = true;
        //    this.IsClearLock = true;
        //}

        ///// <summary>
        ///// 初期化中ロック解除
        ///// </summary>
        //public void ClearUnlock()
        //{
        //    this.ReadOnly = this._readOnly;
        //    this.IsClearLock = false;
        //}



        //ダミープロパティ

        [Browsable(false)]
        [Obsolete("未使用プロパティです。デザイナの初期海外で利用している場合は修正が必要です。")]
        public System.Windows.Forms.DateTimePickerFormat Format { get; set; }
        [Browsable(false)]
        [Obsolete("未使用プロパティです。デザイナの初期海外で利用している場合は修正が必要です。")]
        public string CustomFormat { get; set; }
        [Browsable(false)]
        [Obsolete("未使用プロパティです。デザイナの初期海外で利用している場合は修正が必要です。")]
        public System.Drawing.Font CalendarFont { get; set; }

        [Browsable(false)]
        public string PrevText { get; set; }


        //日付対応
        /// <summary>
        /// セル編集状態処理
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="formattedValue"></param>
        /// <param name="style"></param>
        protected override void InitializeEditingControl(int rowIndex, object formattedValue, CellStyle style)
        {
            base.InitializeEditingControl(rowIndex, formattedValue, style);
        }

        /// <summary>
        /// セル編集状態解除処理
        /// </summary>
        /// <param name="rowIndex"></param>
        protected override void TerminateEditingControl(int rowIndex)
        {
            base.TerminateEditingControl(rowIndex);
        }

        /// <summary>
        /// 編集コントロールタイプ
        /// </summary>
        public override Type EditType
        {
            get
            {
                return typeof(GcCustomeDateTimeTextBoxEditingControl);
            }
        }

        protected override void OnCellFormatting(CellFormattingEventArgs e)
        {
            //デザイナ設定強制上書き(グリッドが狭いときは 曜日を先に隠す)
            this.Style.TextAlign = MultiRowContentAlignment.MiddleLeft;

            //書式設定
            if (e.Value is DateTime)
            {
                if (this.ShowYoubi)
                {
                    e.Value = ((DateTime)e.Value).ToString("yyyy/MM/dd(ddd)");
                }
                else
                {
                    e.Value = ((DateTime)e.Value).ToString("yyyy/MM/dd");
                }
                e.FormattingApplied = true; //フォーマットOK
            }

            base.OnCellFormatting(e);
        }
        protected override void OnCellParsing(CellParsingEventArgs e)
        {

            DateTime dt;
            string[] formats = { "yyyy/MM/dd", "yyyy/MM/dd(ddd)", "yyyyMMdd" }; //yyyyMMddはポップアップからのフォーマット
            if (e.Value != null && DateTime.TryParseExact(e.Value.ToString(), formats, null, System.Globalization.DateTimeStyles.None, out dt))
            {
                e.Value = dt;
                e.ParsingApplied = true; //パースOK

            }

            base.OnCellParsing(e);
        }
    }

    /// <summary>
    /// 数値専用編集コントロールクラス
    /// </summary>
    public class GcCustomeDateTimeTextBoxEditingControl : TextBoxEditingControl
    {
        /// <summary>
        /// KeyDownで修飾キーとして「Ctrlキー」が押されたかどうか
        /// </summary>
        private bool isControlKeyDown;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GcCustomeDateTimeTextBoxEditingControl()
        {
            //IMEを無効にする
            base.ImeMode = ImeMode.Disable;

        }

        /// <summary>
        /// 貼り付け防止処理
        /// </summary>
        const int WM_PASTE = 0x302;

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
                    //クリップボードの文字列が数字とスラッシュのみか調べる
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
        /// テキスト変更処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            //base.OnTextChanged(e);
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
            if (this.GcMultiRow.IsCurrentCellInEditMode)
            {
                base.OnTextChanged(e);
                this.GcMultiRow.NotifyCurrentCellDirty(true);
            }
        }

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
            }
            else
            {
                if (!this.isControlKeyDown)
                {
                    this.isControlKeyDown = false;
                    e.Handled = true; //何もさせない
                }
            }

            base.OnKeyPress(e);
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.KeyCode)
            {
                case Keys.Space:
                    //ポップアップで使うかも
                    var dateTimeCell = this.GcMultiRow.CurrentCell as GcCustomDataTime;
                    if (dateTimeCell != null)
                    {
                        //日付セル専用
                        string bk = dateTimeCell.PopupSetFormField;
                        try
                        {
                            dateTimeCell.PopupWindowName = "カレンダーポップアップ";
                            dateTimeCell.PopupSetFormField = dateTimeCell.Name;
                            dateTimeCell.PopUp();
                        }
                        finally
                        {
                            dateTimeCell.PopupWindowName = "";
                            dateTimeCell.PopupSetFormField = bk;
                        }
                    }
                    break;
            }

            this.isControlKeyDown = false;
            if (e.Modifiers == Keys.Control)
            {
                this.isControlKeyDown = true;
            }
        }

        /// <summary>
        /// エティット開始前処理
        /// </summary>
        /// <param name="selectAll"></param>
        public override void PrepareEditingControlForEdit(bool selectAll)
        {
            base.PrepareEditingControlForEdit(selectAll);

            this.MaxLength = 10;
            this.TextAlign = HorizontalAlignment.Left;

            DateTime dt;

            string[] formats = { "yyyy/MM/dd", "yyyy/MM/dd(ddd)" };
            if (DateTime.TryParseExact(this.Text, formats, CultureInfo.CurrentCulture, DateTimeStyles.None, out dt))
            {
                this.Text = dt.ToString("yyyy/MM/dd");
            }


            //日付は必ず全選択で
            this.SelectAll();
        }


        //Multirow側のCellValidatingで対応すること！（こっちは基本通らない）
        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
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

        /// <summary>ポップアップを開く前に実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl> PopupBeforeExecute { get; set; }

        /// <summary>ポップアップから戻ってきたら実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl, System.Windows.Forms.DialogResult> PopupAfterExecute { get; set; }

    }

}
