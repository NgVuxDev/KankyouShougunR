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
using r_framework.Logic;
using r_framework.Utility;

namespace r_framework.CustomControl
{
    public partial class GcCustomPopupOpenButtonCell_Ex : ButtonCell, ICustomControl
    {
        /// <summary>
        /// カスタムポップアップボタンセル
        /// </summary>
        public GcCustomPopupOpenButtonCell_Ex()
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
        }

        /// <summary>
        /// ペイント処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(CellPaintingEventArgs e)
        {
            base.OnPaint(e);
            if (this.DesignMode)
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
                if (PopupSearchSendParams == null)
                {
                    PopupSearchSendParams = new Collection<PopupSearchSendParamDto>();
                }

            }
        }

        /// <summary>
        /// クローン処理
        /// </summary>
        /// <returns></returns>
        public override object Clone()
        {
            GcCustomPopupOpenButtonCell_Ex myCustomPopupOpenButtonCell = base.Clone() as GcCustomPopupOpenButtonCell_Ex;
            myCustomPopupOpenButtonCell.DBFieldsName = this.DBFieldsName;
            myCustomPopupOpenButtonCell.ItemDefinedTypes = this.ItemDefinedTypes;
            myCustomPopupOpenButtonCell.DisplayItemName = this.DisplayItemName;
            myCustomPopupOpenButtonCell.ShortItemName = this.ShortItemName;
            myCustomPopupOpenButtonCell.SearchDisplayFlag = this.SearchDisplayFlag;
            myCustomPopupOpenButtonCell.RegistCheckMethod = this.RegistCheckMethod;
            myCustomPopupOpenButtonCell.FocusOutCheckMethod = this.FocusOutCheckMethod;
            myCustomPopupOpenButtonCell.PopupWindowName = this.PopupWindowName;
            myCustomPopupOpenButtonCell.ErrorMessage = this.ErrorMessage;
            myCustomPopupOpenButtonCell.DefaultBackColor = this.DefaultBackColor;
            myCustomPopupOpenButtonCell.GetCodeMasterField = this.GetCodeMasterField;
            myCustomPopupOpenButtonCell.SetFormField = this.SetFormField;
            myCustomPopupOpenButtonCell.CharactersNumber = this.CharactersNumber;
            myCustomPopupOpenButtonCell.ZeroPaddengFlag = this.ZeroPaddengFlag;
            myCustomPopupOpenButtonCell.PopupWindowId = this.PopupWindowId;
            myCustomPopupOpenButtonCell.PopupMultiSelect = this.PopupMultiSelect;
            myCustomPopupOpenButtonCell.PopupDataSource = this.PopupDataSource;
            myCustomPopupOpenButtonCell.popupWindowSetting = this.popupWindowSetting;
            myCustomPopupOpenButtonCell.PopupSearchSendParams = this.PopupSearchSendParams;
            myCustomPopupOpenButtonCell.PopupSetFormField = this.PopupSetFormField;
            myCustomPopupOpenButtonCell.PopupSendParams = this.PopupSendParams;
            myCustomPopupOpenButtonCell.PopupGetMasterField = this.PopupGetMasterField;
            myCustomPopupOpenButtonCell.PopupAfterExecuteMethod = this.PopupAfterExecuteMethod;
            myCustomPopupOpenButtonCell.PopupBeforeExecuteMethod = this.PopupBeforeExecuteMethod;

            myCustomPopupOpenButtonCell.ReadOnlyPopUp = this.ReadOnlyPopUp;
            myCustomPopupOpenButtonCell.PopupTitleLabel = this.PopupTitleLabel;

            myCustomPopupOpenButtonCell.ClearFormField = this.ClearFormField;
            myCustomPopupOpenButtonCell.PopupClearFormField = this.PopupClearFormField;
            return myCustomPopupOpenButtonCell;
        }

        /// <summary>
        /// クリック処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(CellEventArgs e)
        {
            // クリック処理はSelectable=falseでも実行出来てしまう為
            // DataSource=nullの場合は実行しないようにする。
            if (this.GcMultiRow.DataSource == null && this.GcMultiRow.Rows.Count == 0)
            {
                return;
            }

            base.OnClick(e);
            CustomControlLogic cstmLogic = new CustomControlLogic(this);
            var cell = this;
            Row row = this.GcMultiRow.Rows[cell.RowIndex];

            var ctrlUtil = new ControlUtility();
            object[] sendParamArray = null;

            if (cell.PopupSendParams != null)
            {
                sendParamArray = new Control[cell.PopupSendParams.Length];
                for (int i = 0; i < cell.PopupSendParams.Length; i++)
                {
                    var sendParam = cell.PopupSendParams[i];
                    sendParamArray[i] = ctrlUtil.FindControl(this.GcMultiRow.FindForm(), sendParam);
                }
            }

            // ポップアップ処理
            cstmLogic.ShowPopupWindow(cell, row.Cells.ToArray(), sendParamArray, null);
        }

        #region Property

        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBのフィールド名を記述してください。")]
        public string DBFieldsName { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBフィールドの型名を指定してください(varchar等)")]
        public string ItemDefinedTypes { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語名を指定してください。")]
        public string DisplayItemName { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語短縮名を指定してください。")]
        public string ShortItemName { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("汎用検索画面に表示するかのフラグを設定してください(使用方法未定)")]
        public int SearchDisplayFlag { get; set; }
        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("登録時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> RegistCheckMethod { get; set; }
        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("フォーカスアウト時に行うチェックを選んでください。")]
        public Collection<SelectCheckDto> FocusOutCheckMethod { get; set; }
        [Category("EDISONプロパティ_ポップアップ設定")]
        [TypeConverter(typeof(PopupWindowConverter))]
        [Description("スペースキー押下時に起動したいポップアップ画面を選択してください。")]
        public string PopupWindowName { get; set; }
        [Category("EDISONプロパティ")]
        [Browsable(false)]
        public string ErrorMessage { get; set; }
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
        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string SetFormField { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("入力可能な最大桁数を指定してください。")]
        public Decimal CharactersNumber { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("trueの場合には「CharactersNumber」に指定した桁数までフォーカスアウト時に0埋めを行います。")]
        public bool ZeroPaddengFlag { get; set; }
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップに表示する画面の種類を選んでください。")]
        public WINDOW_ID PopupWindowId { get; set; }
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップの表示条件を選んでください。")]
        public Collection<JoinMethodDto> popupWindowSetting { get; set; }
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
        /// 検索ポップアップへ送信する値
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("検索ポップアップで絞込みを行うため、テーブルのキー名と値を指定")]
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
        /// ポップアップへ送信するコントロール
        /// </summary>
        [Category("EDISONプロパティ")]
        //[DisplayName("同時チェックコントロール")]
        public string[] PopupSendParams { get; set; }
        private bool ShouldSerializePopupSendParams()
        {
            return this.PopupSendParams != null;
        }
        private bool ShouldSerializeFocusOutCheckMethod()
        {
            return this.FocusOutCheckMethod != new Collection<SelectCheckDto>();
        }

        private bool ShouldSerializeRegistCheckMethod()
        {
            return this.RegistCheckMethod != new Collection<SelectCheckDto>();
        }

        /// <summary>
        /// 名称取得処理
        /// </summary>
        /// <returns>名称</returns>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// 値の取得処理
        /// チェックがついている場合は「1」を未チェックの場合は「0」を返却
        /// </summary>
        public string GetResultText()
        {
            if (this.Value == null)
            {
                return DB_FLAG.FALSE.ToString();
            }

            return this.Value.ToString() == "True" ? DB_FLAG.TRUE.ToString() : DB_FLAG.FALSE.ToString();
        }

        /// <summary>
        /// 値の設定クラス
        /// </summary>
        public void SetResultText(string value)
        {
            this.Value = value == DB_FLAG.TRUE.ToString() ? DB_FLAG.TRUE.ToString() : DB_FLAG.FALSE.ToString();
        }

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

        /// <summary>ポップアップを開く前に実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl> PopupBeforeExecute { get; set; }

        /// <summary>ポップアップから戻ってきたら実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl, System.Windows.Forms.DialogResult> PopupAfterExecute { get; set; }


    }
}
