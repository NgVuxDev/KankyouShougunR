using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// データグリッドビュー用のカスタムコンボボックスカラム
    /// </summary>
    public partial class DgvCustomComboBoxColumn : DataGridViewComboBoxColumn, ICustomeDataGridViewColmun
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DgvCustomComboBoxColumn()
        {
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

            this.CellTemplate = new DgvCustomComboBoxCell();
        }

        /// <summary>
        /// コピー処理
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            DgvCustomComboBoxColumn myCol = base.Clone() as DgvCustomComboBoxColumn;
            myCol.popupWindowSetting = this.popupWindowSetting;
            myCol.DBFieldsName = this.DBFieldsName;
            myCol.ItemDefinedTypes = this.ItemDefinedTypes;
            myCol.DisplayItemName = this.DisplayItemName;
            myCol.ShortItemName = this.ShortItemName;
            myCol.RegistCheckMethod = this.RegistCheckMethod;
            myCol.FocusOutCheckMethod = this.FocusOutCheckMethod;
            myCol.PopupWindowName = this.PopupWindowName;
            myCol.ErrorMessage = this.ErrorMessage;
            myCol.GetCodeMasterField = this.GetCodeMasterField;
            myCol.SetFormField = this.SetFormField;
            myCol.CharactersNumber = this.CharactersNumber;
            myCol.ZeroPaddengFlag = this.ZeroPaddengFlag;
            myCol.PopupWindowId = this.PopupWindowId;
            myCol.PopupMultiSelect = this.PopupMultiSelect;

            myCol.ReadOnlyPopUp = this.ReadOnlyPopUp;
            myCol.PopupTitleLabel = this.PopupTitleLabel;
            myCol.ViewSearchItem = this.ViewSearchItem;

            myCol.ClearFormField = this.ClearFormField;
            myCol.PopupClearFormField = this.PopupClearFormField;
            return myCol;
        }

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

        private bool _ViewSearchItem = true; //デフォルトはTrueにして 不要なものだけ設定するようにする
        /// <summary>
        /// マスタ検索項目ポップアップに出すかどうかを設定します。Trueは表示/Falseは非表示
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("検索条件ポップアップに表示するか否かを選択してください")]
        [DefaultValue(true)]
        public bool ViewSearchItem
        {
            get
            {
                return this._ViewSearchItem;
            }
            set
            {
                this._ViewSearchItem = value;
            }
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

    }
}
