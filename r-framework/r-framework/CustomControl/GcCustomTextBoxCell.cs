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
using r_framework.Utility;
using r_framework.Logic;

namespace r_framework.CustomControl
{
    /// <summary>
    /// MultiRowにて使用されるカスタムテキストボックス
    /// </summary>
    public partial class GcCustomTextBoxCell : TextBoxCell, ICustomControl, ICustomTextBox, ICustomAutoChangeBackColor
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GcCustomTextBoxCell()
        {
            InitializeComponent();
            //this.MaxLength = CharactersNumber;
            this.Style.Font = Constans.DEFAULT_MULTIROW_FONT;
            this.Style.InputScope = InputScopeNameValue.Default;
            this.Style.ImeMode = ImeMode.Off;
            this.Style.ImeSentenceMode = ImeSentenceMode.Normal;

            // FormFieldのコピー
            if (string.IsNullOrEmpty(this.PopupSetFormField))
            {
                this.PopupSetFormField = this.SetFormField;
            }
            if (string.IsNullOrEmpty(this.PopupGetMasterField))
            {
                this.PopupGetMasterField = this.GetCodeMasterField;
            }

        }

        #region ICustomTextBox プロパティ
        /// <summary>
        /// 表示するPopUp。未指定(null)の場合、DLLファイルが使用される。
        /// </summary>
        public APP.PopUp.Base.SuperPopupForm DisplayPopUp { get; set; }

        private bool _IsInputErrorOccured = false;
        /// <summary>
        /// 入力エラーが発生したかどうか
        /// </summary>
        public bool IsInputErrorOccured
        {
            get
            {
                return this._IsInputErrorOccured;
            }
            set
            {
                this._IsInputErrorOccured = value;
                this.UpdateBackColor();
            }
        }
        #endregion

        [Browsable(false)]
        [Obsolete("privateに変更予定、使用禁止")]
        public bool autoChangeBackColorEnabled = true;
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
                return autoChangeBackColorEnabled;
            }
            set
            {
                autoChangeBackColorEnabled = value;
                this.UpdateBackColor();
            }
        }

        /// <summary>
        /// 描画処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(CellPaintingEventArgs e)
        {
            base.OnPaint(e);
            //this.CreateHintText();
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
        /// コピー処理
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            GcCustomTextBoxCell myTextBoxCell = base.Clone() as GcCustomTextBoxCell;
            myTextBoxCell.popupWindowSetting = this.popupWindowSetting;
            myTextBoxCell.DBFieldsName = this.DBFieldsName;
            myTextBoxCell.ItemDefinedTypes = this.ItemDefinedTypes;
            myTextBoxCell.DisplayItemName = this.DisplayItemName;
            myTextBoxCell.ShortItemName = this.ShortItemName;
            myTextBoxCell.SearchDisplayFlag = this.SearchDisplayFlag;
            myTextBoxCell.RegistCheckMethod = this.RegistCheckMethod;
            myTextBoxCell.FocusOutCheckMethod = this.FocusOutCheckMethod;
            myTextBoxCell.PopupWindowName = this.PopupWindowName;
            myTextBoxCell.ErrorMessage = this.ErrorMessage;
            myTextBoxCell.DefaultBackColor = this.DefaultBackColor;
            myTextBoxCell.GetCodeMasterField = this.GetCodeMasterField;
            myTextBoxCell.SetFormField = this.SetFormField;
            myTextBoxCell.CharactersNumber = this.CharactersNumber;
            myTextBoxCell.ZeroPaddengFlag = this.ZeroPaddengFlag;
            myTextBoxCell.PopupWindowId = this.PopupWindowId;
            myTextBoxCell.FuriganaAutoSetControl = this.FuriganaAutoSetControl;
            myTextBoxCell.FormatSetting = this.FormatSetting;
            myTextBoxCell.CustomFormatSetting = this.CustomFormatSetting;
            myTextBoxCell.CopyAutoSetControl = this.CopyAutoSetControl;
            myTextBoxCell.CopyAutoSetWithSpace = this.CopyAutoSetWithSpace;
            myTextBoxCell.ChangeUpperCase = this.ChangeUpperCase;
            myTextBoxCell.ChangeWideCase = this.ChangeWideCase;
            myTextBoxCell.Name = this.Name;
            myTextBoxCell.PopupSendParams = this.PopupSendParams;
            myTextBoxCell.PopupSearchSendParams = this.PopupSearchSendParams;
            myTextBoxCell.PopupSetFormField = this.PopupSetFormField;
            myTextBoxCell.PopupAfterExecuteMethod = this.PopupAfterExecuteMethod;
            myTextBoxCell.PopupBeforeExecuteMethod = this.PopupBeforeExecuteMethod;
            myTextBoxCell.PopupGetMasterField = this.PopupGetMasterField;

            myTextBoxCell.ReadOnlyPopUp = this.ReadOnlyPopUp;
            myTextBoxCell.PopupTitleLabel = this.PopupTitleLabel;

            myTextBoxCell.AutoChangeBackColorEnabled = this.AutoChangeBackColorEnabled;

            myTextBoxCell.ClearFormField = this.ClearFormField;
            myTextBoxCell.PopupClearFormField = this.PopupClearFormField;
            return myTextBoxCell;
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
        /// <para>PopupDataSourceを指定した場合、ここで指定した列名がDataGridViewのタイトル行に使用される。</para>
        /// <para>PopupDataSourceを指定して、PopupDataHeaderTitleを指定しない場合、PopupDataSourceに設定されている列名が表示される</para>
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップで取得済データ列名を設定する場合に指定してください。")]
        [Browsable(false)]
        public string[] PopupDataHeaderTitle { get; set; }
        private bool ShouldSerializePopupDataHeaderTitle()
        {
            return this.PopupDataHeaderTitle != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("フリガナの自動設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string FuriganaAutoSetControl { get; set; }
        private bool ShouldSerializeFuriganaAutoSetControl()
        {
            return this.FuriganaAutoSetControl != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("自動複写設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string CopyAutoSetControl { get; set; }
        private bool ShouldSerializeCopyAutoSetControl()
        {
            return this.CopyAutoSetControl != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("自動複写時にスペースも含める場合はTrueで設定してください。")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [DefaultValue(false)]
        public bool CopyAutoSetWithSpace { get; set; }
        private bool ShouldSerializeFuriganaAutoSetWithSpace()
        {
            return this.CopyAutoSetWithSpace != false;
        }
        internal void ResetFuriganaAutoSetWithSpace()
        {
            this.CopyAutoSetWithSpace = false;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("フォーカスアウト時に大文字に変換する場合に指定してください。")]
        public bool ChangeUpperCase { get; set; }
        private bool ShouldSerializeChangeUpperCase()
        {
            return this.ChangeUpperCase != false;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("フォーカスアウト時に全角文字に変換する場合に指定してください。")]
        public bool ChangeWideCase { get; set; }
        private bool ShouldSerializeChangeWideCase()
        {
            return this.ChangeWideCase != false;
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

        [Category("EDISONプロパティ_画面設定")]
        [Description("フォーカスアウト時に行うフォーマットを選んでください。")]
        [TypeConverter(typeof(FormatConverter))]
        public string FormatSetting { get; set; }
        private bool ShouldSerializeFormatSetting()
        {
            return this.FormatSetting != null;
        }

        [Category("EDISONプロパティ_画面設定")]
        [Description("FormatSettingでカスタムを指定した場合にフォーマット（「#」「,」「0」「.」の組合せのみ）を設定してください。")]
        public string CustomFormatSetting { get; set; }
        private bool ShouldSerializeCustomFormatSetting()
        {
            return this.CustomFormatSetting != null;
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

        [Category("EDISONプロパティ_テキストボックス編集判定")]
        [Description("テキストボックスが編集済みかどうかの判定を返す")]
        public bool TextBoxChanged { get; set; }
        private bool ShouldSerializeTextBoxChanged()
        {
            return this.TextBoxChanged != false;
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

        #endregion

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

        /// <summary>
        /// 値の取得処理
        /// </summary>
        virtual public string GetResultText()
        {
            if (this.Value == null)
            {
                return string.Empty;
            }
            return this.Value.ToString();
        }

        /// <summary>
        /// 値の設定処理
        /// </summary>
        virtual public void SetResultText(string value)
        {
            var readFlag = this.ReadOnly;
            this.ReadOnly = false;

            if (string.IsNullOrEmpty(value))
            {
                this.GcMultiRow.SetValue(this.RowIndex, this.CellIndex, DBNull.Value);
            }
            else
            {
                this.GcMultiRow.SetValue(this.RowIndex, this.CellIndex, value);
            }

            this.ReadOnly = readFlag;
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
        ///// ヒントテキスト設定処理
        ///// </summary>
        //public virtual void CreateHintText()
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


        ///// <summary>
        ///// 背景色更新
        ///// </summary>
        //public void UpdateBackColor(bool focus)
        //{

        //    if (!this.Enabled)
        //    {
        //        this.Style.BackColor = Constans.DISABLE_COLOR;
        //    }
        //    else if (this.IsInputErrorOccured)
        //    {
        //        this.Style.BackColor = Constans.ERROR_COLOR;
        //    }
        //    else if (this.ReadOnly)
        //    {
        //        this.Style.BackColor = Constans.READONLY_COLOR;
        //    }
        //    else if (focus)
        //    {
        //        this.Style.BackColor = Constans.FOCUSED_COLOR;
        //    }
        //    else
        //    {
        //        this.Style.BackColor = Constans.NOMAL_COLOR;
        //    }


        //    //エディットコントロールも変える
        //    if (this.GcMultiRow != null && this.GcMultiRow.EditingControl != null)
        //        this.GcMultiRow.EditingControl.BackColor = this.Style.BackColor;
        //    //パネルも
        //    if (this.GcMultiRow != null && this.GcMultiRow.EditingPanel != null)
        //        this.GcMultiRow.EditingPanel.BackColor = this.Style.BackColor;                  

        //    this.Invalidate();

        //}
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



        #region ICustomAutoChangeBackColor メンバー


        /// <summary>
        /// フォーカスがあるかどうか
        /// </summary>
        public bool Focused
        {
            get
            {
                return (this.GcMultiRow != null && this.GcMultiRow.CurrentCell != null && this.GcMultiRow.Focused
                        && this.GcMultiRow.CurrentCell.RowIndex == this.RowIndex
                        && this.GcMultiRow.CurrentCell.CellIndex == this.CellIndex);
            }
        }

        #endregion

        /// <summary>ポップアップを開く前に実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl> PopupBeforeExecute { get; set; }

        /// <summary>ポップアップから戻ってきたら実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl, System.Windows.Forms.DialogResult> PopupAfterExecute { get; set; }

        /// <summary>
        /// 入力用コントロールの型情報を取得する。
        /// MultoRowがTextBoxセルの編集開始直前に呼び出す。
        /// </summary>
        public override Type EditType
        {
            get
            {
                return typeof(GcCustomTextBoxEditingControl);
            }
        }
    }
}
