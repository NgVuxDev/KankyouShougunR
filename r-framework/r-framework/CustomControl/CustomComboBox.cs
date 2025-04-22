using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;
using r_framework.Logic;
using r_framework.Utility;

namespace r_framework.CustomControl
{
    /// <summary>
    /// カスタムコンボボックスコントロール
    /// </summary>
    public partial class CustomComboBox : ComboBox, ICustomControl, ICustomTextBox
    {
        #region ICustomTextBox プロパティ
        /// <summary>
        /// 表示するPopUp。未指定(null)の場合、DLLファイルが使用される。
        /// </summary>
        public APP.PopUp.Base.SuperPopupForm DisplayPopUp { get; set; }

        /// <summary>
        /// 入力エラーが発生したかどうか
        /// </summary>
        public bool IsInputErrorOccured { get; set; }

        #endregion

        /// <summary>
        /// ドロップダウンの展開チェックタイマー
        /// </summary>
        private Timer timer = null;
        
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomComboBox()
        {
            InitializeComponent();

            RegistCheckMethod = new Collection<SelectCheckDto>();
            FocusOutCheckMethod = new Collection<SelectCheckDto>();
            popupWindowSetting = new Collection<JoinMethodDto>();
            PopupSearchSendParams = new Collection<PopupSearchSendParamDto>();

            // FormFieldのコピー
            if (string.IsNullOrEmpty(this.PopupSetFormField))
            {
                this.PopupSetFormField = this.SetFormField;
            }
            if (string.IsNullOrEmpty(this.PopupGetMasterField))
            {
                this.PopupGetMasterField = this.GetCodeMasterField;
            }

            // ヒントテキストの作成
            //this.CreateHintText();

            IsInputErrorOccured = false;

            upateBackColor(false);

            timer = new Timer();
            timer.Enabled = false;
            timer.Tick += new EventHandler(onTimeout);
            timer.Interval = 200;
        }

        /// <summary>
        /// 背景色更新
        /// </summary>
        private void upateBackColor(bool focused)
        {
            if (IsInputErrorOccured)
            {
                BackColor = Constans.ERROR_COLOR;
            }
            else if (focused && Enabled)
            {
                BackColor = Constans.FOCUSED_COLOR;
            }
            else
            {
                BackColor = Constans.NOMAL_COLOR;
            }
        }

        /// <summary>
        /// ペースト時のウィンドウメッセージ
        /// </summary>
        private const int WM_PAINT = 0x000F;

        /// <summary>
        /// ペイント処理
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_PAINT)
            {
                CustomControlLogic.DrawControlBorder(this);
            }
        }

        /// <summary>
        /// キー押下時処理
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !string.IsNullOrEmpty(this.PopupWindowName))
            {
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
                e.Handled = true;
            }
        }

        /// <summary>
        /// 入力処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            SuperForm superForm;
            if (ControlUtility.TryGetSuperForm(this, out superForm)
                && superForm.PreviousSaveFlag)
            {
                // 前回値を保持
                superForm.SetPreviousValue(this.GetResultText(), this);

                var cstmLogic = new CustomControlLogic(this);
                object[] obj = cstmLogic.GetPreviousControl();
                superForm.SetPreviousControlValue(obj);
            }
        }

        //FW_QA74:LeaveをValidatingへ移動
        /// <summary>
        /// 自動チェック処理。
        /// 閉じるボタンなどはCausesValidation=falseにすることで、クリック時にチェックを走らせないことが可能。
        /// </summary>
        /// <param name="e">チェックNGの場合はcancel=trueでフォーカス移動をキャンセル</param>
        protected override void OnValidating(CancelEventArgs e)
        {
            timer.Enabled = false;

            // ゼロ埋め処理
            var textLogic = new CustomTextBoxLogic(this);
            textLogic.ZeroPadding(this);

            // 自動フォーマット処理
            textLogic.Format(this);

            // MaxByte数まで切る
            textLogic.MaxByteCheckAndCut(this);

            // 自動チェック処理
            var cstmLogic = new CustomControlLogic(this);
            var ctrlUtil = new ControlUtility();
            var fields = ctrlUtil.GetAllControls(ControlUtility.GetTopControl(this));
            var mthodList = this.FocusOutCheckMethod;
            SuperForm superForm;
            if (mthodList != null && mthodList.Count != 0 && ControlUtility.TryGetSuperForm(this, out superForm))
            {
                e.Cancel = cstmLogic.StartingFocusOutCheck(this, fields, superForm);
            }

            IsInputErrorOccured = e.Cancel;

            //チェックしてから、業務や継承コントロール側のチェックへ移る 
            //　※イベントを使わずに、オーバーライドする継承先は先にbaseを動かしてから自前チェックすること！
            base.OnValidating(e);
        }

        /// <summary>
        /// フォーカスイン処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            upateBackColor(true);

            base.OnEnter(e);

            //if (!this.Focused)
            //{
            //    this.DroppedDown = true;
            //}
            timer.Enabled = true;
        }

        /// <summary>
        /// フォーカス取得後にドロップダウンが展開されていなければ展開する
        /// </summary>
        private void onTimeout(object sender, EventArgs e)
        {
            if (timer.Enabled)
            {
                if (Enabled && Focused && !DroppedDown)
                {
                    System.Windows.Forms.Cursor.Current = System.Windows.Forms.Cursors.Default; //カーソルが消えたままになる対応
                    this.DroppedDown = true;
                }
                timer.Enabled = false;
            }

        }

        /// <summary>
        /// フォーカス移動
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            upateBackColor(false);

            if (this.DroppedDown)
            {
                // 描画を止め、ドロップダウンを閉じた後で値の再セットを行う
                this.BeginUpdate();
                var item = this.Text;
                this.DroppedDown = false;
                this.SelectedItem = item;
                this.EndUpdate();
            }
        }

        /// <summary>
        /// インデックス変更時の派生するコントロールへ値を設定する処理
        /// </summary>
        protected override void OnSelectedIndexChanged(EventArgs e)
        {
            base.OnSelectedIndexChanged(e);
            
            if (string.IsNullOrEmpty(SetFormField))
            {
                return;
            }
            var contUtile = new ControlUtility();
            var control = contUtile.FindControl(this.Parent, this.SetFormField);
            contUtile.ChangeIndex(this, control);
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
        /// 設定されている値を取得する
        /// </summary>
        public string GetResultText()
        {
            return SelectedItem == null ? string.Empty : SelectedItem.ToString();
        }

        /// <summary>
        /// 名称取得
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return this.Name;
        }

        /// <summary>
        /// 値の設定を行う
        /// </summary>
        public void SetResultText(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                this.SelectedIndex = 0;
            }
            else
            {
                for (var i = 0; i < this.Items.Count; i++)
                {
                    if (this.Items[i].ToString() == value)
                    {
                        this.SelectedIndex = i;
                        break;
                    }
                }
            }
        }

        ///// <summary>
        ///// ヒントテキストの生成を行う
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
