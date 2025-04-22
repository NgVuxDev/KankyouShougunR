using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Const;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;

namespace r_framework.CustomControl
{
    /// <summary>
    /// MultiRowにて使用する、日付入力項目のカスタムコントロール
    /// </summary>
    public partial class GcCustomDataTimePicker : DateTimePickerCell, ICustomControl
    {
        /// <summary>
        /// 保存用読取専用フラグ
        /// </summary>
        private bool _readOnly;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GcCustomDataTimePicker()
        {
            InitializeComponent();
            this.Style.Font = Constans.DEFAULT_MULTIROW_FONT;
            this.Style.InputScope = InputScopeNameValue.Default;
            this.Style.ImeMode = ImeMode.Disable;
            this._readOnly = this.ReadOnly;

            // FormFieldのコピー
            if (string.IsNullOrEmpty(this.PopupSetFormField))
            {
                this.PopupSetFormField = this.SetFormField;
            }
            if (string.IsNullOrEmpty(this.PopupGetMasterField))
            {
                this.PopupGetMasterField = this.GetCodeMasterField;
            }
            this.RegistCheckMethod = new Collection<Dto.SelectCheckDto>();
            this.FocusOutCheckMethod = new Collection<Dto.SelectCheckDto>();
            this.PopupSearchSendParams = new Collection<PopupSearchSendParamDto>();
            this.popupWindowSetting = new Collection<JoinMethodDto>();
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
            this.ClearUnlock();
            base.OnEnter(e);
        }

        /// <summary>
        /// クリック処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(CellEventArgs e)
        {
            this.ClearUnlock();
            base.OnClick(e);
        }

        /// <summary>
        /// 複製処理
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            GcCustomDataTimePicker myCustomDataTime = base.Clone() as GcCustomDataTimePicker;
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

            myCustomDataTime.ClearFormField = this.ClearFormField;
            myCustomDataTime.PopupClearFormField = this.PopupClearFormField;
            return myCustomDataTime;
        }

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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("登録時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> RegistCheckMethod { get; set; }
        private bool ShouldSerializeRegistCheckMethod()
        {
            return this.RegistCheckMethod != new Collection<SelectCheckDto>();
        }

        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
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
        public Decimal CharactersNumber { get; set; }
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
        private bool ShouldSerializePopupWindowId()
        {
            return this.PopupWindowId != null;
        }

        /// <summary>
        /// ポップアップで複数選択の設定を行うプロパティ
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップでマルチセレクトをする場合に指定してください。")]
        public bool PopupMultiSelect { get; set; }
        private bool ShouldSerializePopupMultiSelect()
        {
            return this.PopupMultiSelect != false;
        }

        /// <summary>
        /// ポップアップで取得済データを設定するプロパティ
        /// </summary>
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
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Collection<JoinMethodDto> popupWindowSetting { get; set; }

        /// <summary>
        /// 検索ポップアップへ送信する値
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("検索ポップアップで絞込みを行うため、テーブルのキー名と値を指定")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Collection<PopupSearchSendParamDto> PopupSearchSendParams { get; set; }
        private bool ShouldSerializePopupSearchSendParams()
        {
            return this.PopupSearchSendParams != null;
        }

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

        /// <summary>ポップアップを開く前に実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl> PopupBeforeExecute { get; set; }

        /// <summary>ポップアップから戻ってきたら実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl, System.Windows.Forms.DialogResult> PopupAfterExecute { get; set; }

        #endregion

        /// <summary>
        /// 初期化中ロック
        /// </summary>
        [Browsable(false)]
        public bool IsClearLock { get; private set; }

        /// <summary>
        /// 入力値の取得処理
        /// </summary>
        public string GetResultText()
        {
            return Convert.ToString(this.Value);
        }

        /// <summary>
        /// 値の設定処理
        /// </summary>
        public void SetResultText(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                this.Value = Convert.ToDateTime(value);
            }
        }

        /// <summary>
        /// コントロール名取得
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// ヒントテキスト設定処理
        /// </summary>
        public void CreateHintText()
        {
            if (this.DesignMode)
            {
                return;
            }
            if (this.Tag == null || string.IsNullOrEmpty(this.Tag.ToString()))
            {
                this.Tag = this.DisplayItemName + " は " + this.CharactersNumber + " 文字以内で入力してください。";
            }
        }

        /// <summary>
        /// 初期化中ロック
        /// </summary>
        public void ClearLock()
        {
            this._readOnly = this.ReadOnly;
            this.ReadOnly = true;
            this.IsClearLock = true;
        }

        /// <summary>
        /// 初期化中ロック解除
        /// </summary>
        public void ClearUnlock()
        {
            this.ReadOnly = this._readOnly;
            this.IsClearLock = false;
        }
    }
}
