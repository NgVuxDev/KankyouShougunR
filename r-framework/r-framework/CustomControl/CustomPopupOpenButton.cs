using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;
using r_framework.Logic;
using r_framework.Utility;

namespace r_framework.CustomControl
{
    /// <summary>
    /// ポップアップオープン用ボタン
    /// </summary>
    public sealed partial class CustomPopupOpenButton : Button, ICustomControl
    {
        /// <summary>
        /// 表示するPopUp。未指定(null)の場合、DLLファイルが使用される。
        /// </summary>
        public APP.PopUp.Base.SuperPopupForm DisplayPopUp { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomPopupOpenButton()
        {
            InitializeComponent();
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
        /// ボタンクリック処理
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            // VUNGUYEN 20150529 テキストボックスには設定されたプロパティを参照する
            if (!string.IsNullOrEmpty(LinkedSettingTextBox))
            {
                var ctrlUtil1 = new ControlUtility();
                ctrlUtil1.ControlCollection = this.FindForm().Controls;

                var control = ctrlUtil1.GetSettingField(LinkedSettingTextBox);
                if (control is CustomTextBox)
                {
                    var csTextBox = (CustomTextBox)control;

                    if (csTextBox != null)
                    {
                        this.PopupGetMasterField = csTextBox.PopupGetMasterField;
                        this.PopupSearchSendParams = csTextBox.PopupSearchSendParams;
                        this.PopupSetFormField = csTextBox.PopupSetFormField;
                        this.PopupWindowId = csTextBox.PopupWindowId;
                        this.PopupWindowName = csTextBox.PopupWindowName;
                        this.popupWindowSetting = csTextBox.popupWindowSetting;
                        this.PopupSendParams = csTextBox.PopupSendParams;
                        this.ClearFormField = csTextBox.ClearFormField;
                        this.PopupClearFormField = csTextBox.PopupClearFormField;

                        // PopupAfterExecuteMethodのプロパティを設定されない場合
                        if (string.IsNullOrEmpty(this.PopupAfterExecuteMethod))
                        {
                            this.PopupAfterExecuteMethod = csTextBox.PopupAfterExecuteMethod;
                        }

                        // PopupBeforeExecuteMethodのプロパティを設定されない場合
                        if (string.IsNullOrEmpty(this.PopupBeforeExecuteMethod))
                        {
                            this.PopupBeforeExecuteMethod = csTextBox.PopupBeforeExecuteMethod;
                        }
                    }
                }
            }
            // VUNGUYEN 20150529 END

            var cstmLogic = new CustomControlLogic(this);
            var ctrlUtil = new ControlUtility();
            ctrlUtil.ControlCollection = this.Parent.Controls;
            object[] sendParamArray = null;

            if (this.PopupSendParams != null)
            {
                sendParamArray = new object[this.PopupSendParams.Length];
                for (int i = 0; i < this.PopupSendParams.Length; i++)
                {
                    var sendParam = this.PopupSendParams[i];
                    sendParamArray[i] = ctrlUtil.FindControl(this.FindForm(), sendParam);
                }
            }
            var fields = ctrlUtil.GetAllControls(ControlUtility.GetTopControl(this));

            cstmLogic.ShowPopupWindow(this, fields, this, sendParamArray);
        }

        /// <summary>
        /// PreviewKeyDownイベントを発生させます
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // 親フォームでキーイベントをハンドリング出来るようにするため
                e.IsInputKey = true;
            }

            base.OnPreviewKeyDown(e);
        }

        #region Property

        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBのフィールド名を記述してください。")]
        [Browsable(false)]
        public string DBFieldsName { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("対応するDBフィールドの型名を指定してください(varchar等)")]
        [Browsable(false)]
        public string ItemDefinedTypes { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語名を指定してください。")]
        public string DisplayItemName { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語短縮名を指定してください。")]
        public string ShortItemName { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("汎用検索画面に表示するかのフラグを設定してください(使用方法未定)")]
        [Browsable(false)]
        public int SearchDisplayFlag { get; set; }
        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("登録時に行うチェックを選んでください。")]
        [Browsable(false)]
        public Collection<SelectCheckDto> RegistCheckMethod { get; set; }
        [Category("EDISONプロパティ_チェック設定")]
        [System.ComponentModel.Editor(typeof(CheckCollectionEditor), typeof(System.Drawing.Design.UITypeEditor))]
        [Description("フォーカスアウト時に行うチェックを選んでください。")]
        [Browsable(false)]
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
        [Browsable(false)]
        public string GetCodeMasterField { get; set; }
        [Category("EDISONプロパティ_チェック設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string SetFormField { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("入力可能な最大桁数を指定してください。")]
        [Browsable(false)]
        public Decimal CharactersNumber { get; set; }
        [Category("EDISONプロパティ_画面設定")]
        [Description("trueの場合には「CharactersNumber」に指定した桁数までフォーカスアウト時に0埋めを行います。")]
        [Browsable(false)]
        public bool ZeroPaddengFlag { get; set; }
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップに表示する画面の種類を選んでください。")]
        public WINDOW_ID PopupWindowId { get; set; }
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップの表示条件を選んでください。")]
        public Collection<JoinMethodDto> popupWindowSetting { get; set; }

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

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップから取得した値を設定する、コントロールのName属性を指定してください。※使用禁止")]
        public string[] LinkedTextBoxs { get; set; }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップで取得済データ列名を設定する場合に指定してください。")]
        [Browsable(false)]
        public string[] PopupDataHeaderTitle { get; set; }
        private bool ShouldSerializePopupDataHeaderTitle()
        {
            return this.PopupDataHeaderTitle != null;
        }

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップの属性値を設定する、参照するコントロールのName属性を指定してください。")]
        public string LinkedSettingTextBox { get; set; }

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
        /// 名称取得
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// 設定値取得処理
        /// </summary>
        public string GetResultText()
        {
            return this.Text == "True" ? DB_FLAG.TRUE.ToString() : DB_FLAG.FALSE.ToString();
        }

        /// <summary>
        /// 値の設定処理
        /// </summary>
        public void SetResultText(string value)
        {
            if (Constans.NULL_STRING == value)
            {
                this.Text = string.Empty;
                return;
            }

            this.Text = value;
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

        /// <summary>ポップアップを開く前に実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl> PopupBeforeExecute { get; set; }

        /// <summary>ポップアップから戻ってきたら実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl, System.Windows.Forms.DialogResult> PopupAfterExecute { get; set; }

    }
}
