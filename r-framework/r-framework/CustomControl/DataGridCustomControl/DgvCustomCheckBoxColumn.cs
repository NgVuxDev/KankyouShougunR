using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.Dto;
using r_framework.Editor;

namespace r_framework.CustomControl.DataGridCustomControl
{
    /// <summary>
    /// データグリッドビュー用のカスタムチェックボックス
    /// </summary>
    public partial class DgvCustomCheckBoxColumn : DataGridViewCheckBoxColumn, ICustomeDataGridViewColmun
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public DgvCustomCheckBoxColumn()
        {
            if (RegistCheckMethod == null)
            {
                RegistCheckMethod = new Collection<SelectCheckDto>();
            }
            if (FocusOutCheckMethod == null)
            {
                FocusOutCheckMethod = new Collection<SelectCheckDto>();
            }

            this.CellTemplate = new DgvCustomCheckBoxCell();
        }

        /// <summary>
        /// コピー処理
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            DgvCustomCheckBoxColumn myCol = base.Clone() as DgvCustomCheckBoxColumn;
            myCol.DBFieldsName = this.DBFieldsName;
            myCol.ItemDefinedTypes = this.ItemDefinedTypes;
            myCol.DisplayItemName = this.DisplayItemName;
            myCol.ShortItemName = this.ShortItemName;
            myCol.SearchDisplayFlag = this.SearchDisplayFlag;
            myCol.RegistCheckMethod = this.RegistCheckMethod;
            myCol.FocusOutCheckMethod = this.FocusOutCheckMethod;
            myCol.ErrorMessage = this.ErrorMessage;
            myCol.GetCodeMasterField = this.GetCodeMasterField;
            myCol.SetFormField = this.SetFormField;

            myCol.ReadOnlyPopUp = this.ReadOnlyPopUp;
            myCol.PopupTitleLabel = this.PopupTitleLabel;
            myCol.ViewSearchItem = this.ViewSearchItem;

            myCol.ClearFormField = this.ClearFormField;
            myCol.PopupClearFormField = this.PopupClearFormField;
            return myCol;
        }

        /// <summary>
        /// DBのフィールド名を指定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBのフィールド名を記述してください。")]
        public string DBFieldsName { get; set; }
        private bool ShouldSerializeDBFieldsName()
        {
            return this.DBFieldsName != null;
        }

        /// <summary>
        /// DBのカラムの型を指定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBフィールドの型名を指定してください(varchar等)")]
        public string ItemDefinedTypes { get; set; }
        private bool ShouldSerializeItemDefinedTypes()
        {
            return this.ItemDefinedTypes != null;
        }

        /// <summary>
        /// 画面表示用の名称を指定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語名を指定してください。")]
        public string DisplayItemName { get; set; }
        private bool ShouldSerializeDisplayItemName()
        {
            return this.DisplayItemName != null;
        }

        /// <summary>
        /// 表示用の画面短縮名を指定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語短縮名を指定してください。")]
        public string ShortItemName { get; set; }
        private bool ShouldSerializeShortItemName()
        {
            return this.ShortItemName != null;
        }

        /// <summary>
        /// 汎用検索画面に表示するかを指定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_画面設定")]
        [Description("汎用検索画面に表示するかのフラグを設定してください(使用方法未定)")]
        public int SearchDisplayFlag { get; set; }
        private bool ShouldSerializeSearchDisplayFlag()
        {
            return this.SearchDisplayFlag != 0;
        }

        /// <summary>
        /// 登録時に行うチェックの情報を格納するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("登録時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> RegistCheckMethod { get; set; }
        private bool ShouldSerializeRegistCheckMethod()
        {
            return this.RegistCheckMethod != new Collection<SelectCheckDto>();
        }

        /// <summary>
        /// フォーカスアウト時チェック情報を格納するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("フォーカスアウト時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> FocusOutCheckMethod { get; set; }
        private bool ShouldSerializeFocusOutCheckMethod()
        {
            return this.FocusOutCheckMethod != new Collection<SelectCheckDto>();
        }

        /// <summary>
        /// エラーメッセージを格納するプロパティ
        /// </summary>
        [Category("EDISONプロパティ")]
        [Browsable(false)]
        public string ErrorMessage { get; set; }
        private bool ShouldSerializeErrorMessage()
        {
            return this.ErrorMessage != null;
        }

        /// <summary>
        /// マスタチェック時にどこのカラムからデータを取得するか設定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、取得を行うフィールド名を「,」区切りで入力してください。")]
        public string GetCodeMasterField { get; set; }
        private bool ShouldSerializeGetCodeMasterField()
        {
            return this.GetCodeMasterField != null;
        }

        /// <summary>
        /// マスタチェック時に取得したデータを設定するプロパティ
        /// </summary>
        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string SetFormField { get; set; }
        private bool ShouldSerializeSetFormField()
        {
            return this.SetFormField != null;
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