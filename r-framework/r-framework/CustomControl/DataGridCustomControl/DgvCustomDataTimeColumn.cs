using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Converter;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dto;
using r_framework.Editor;

namespace r_framework.CustomControl
{
    /// <summary>
    /// データグリッドビュー用のカスタムデイトタイムカラム
    /// </summary>
    public class DgvCustomDataTimeColumn : DataGridViewColumn, ICustomeDataGridViewColmun
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DgvCustomDataTimeColumn()
            : base(new DgvCustomDataTimeCell())
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

            //デフォルト
            this.ShowYoubi = true;
            this.MaxValue = System.Data.SqlTypes.SqlDateTime.MaxValue.Value;
            this.MinValue = System.Data.SqlTypes.SqlDateTime.MinValue.Value;
        }

        /// <summary>
        /// セルのテンプレート
        /// </summary>
        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a CalendarCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(DgvCustomDataTimeCell)))
                {
                    throw new InvalidCastException("Must be a CalendarCell");
                }
                base.CellTemplate = value;
            }
        }

        /// <summary>
        /// コピー処理
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            DgvCustomDataTimeColumn myCol = base.Clone() as DgvCustomDataTimeColumn;
            myCol.popupWindowSetting = this.popupWindowSetting;
            myCol.DBFieldsName = this.DBFieldsName;
            myCol.ItemDefinedTypes = this.ItemDefinedTypes;
            myCol.DisplayItemName = this.DisplayItemName;
            myCol.ShortItemName = this.ShortItemName;
            myCol.SearchDisplayFlag = this.SearchDisplayFlag;
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
            myCol.FuriganaAutoSetControl = this.FuriganaAutoSetControl;
            myCol.CopyAutoSetControl = this.CopyAutoSetControl;

            myCol.ShowYoubi = this.ShowYoubi;
            myCol.ReadOnlyPopUp = this.ReadOnlyPopUp;
            myCol.PopupTitleLabel = this.PopupTitleLabel;
            myCol.ViewSearchItem = this.ViewSearchItem;
            myCol.AutoChangeBackColorEnabled = this.AutoChangeBackColorEnabled;

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
                return autoChangeBackColorEnabled;
            }
            set
            {
                autoChangeBackColorEnabled = value;
                //todo:子セルへ反映？
            }
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
    }
}
