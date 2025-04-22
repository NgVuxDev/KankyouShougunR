using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Converter;
using r_framework.Dto;
using r_framework.Editor;
using r_framework.Event;
using r_framework.Logic;
using r_framework.Utility;

namespace r_framework.CustomControl
{
    /// <summary>
    /// カスタムテキストボックスクラス
    /// </summary>
    public partial class CustomTextBox : TextBox, ICustomControl, ICustomTextBox, ICustomAutoChangeBackColor
    {
        #region ICustomTextBox プロパティ

        /// <summary>
        /// 表示するPopUp。未指定(null)の場合、DLLファイルが使用される。
        /// </summary>
        public APP.PopUp.Base.SuperPopupForm DisplayPopUp { get; set; }

        private bool isInputErrorOccured = false;

        /// <summary>
        /// 入力エラーが発生したかどうか
        /// </summary>
        [Browsable(false)]
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

        #endregion ICustomTextBox プロパティ

        private bool isSelectAll = false;   // 全選択フラグ //No.3873

        /// <summary>
        /// IMEフリガナ取得用オブジェクト
        /// 1次マスタのフレームワークより
        /// </summary>
        public NativeWindowContorol imeFuri = null;

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
                this.UpdateBackColor();
            }
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CustomTextBox()
        {
            this.InitializeComponent();

            this.RegistCheckMethod = new Collection<SelectCheckDto>();
            this.FocusOutCheckMethod = new Collection<SelectCheckDto>();
            this.popupWindowSetting = new Collection<JoinMethodDto>();
            this.PopupSearchSendParams = new Collection<PopupSearchSendParamDto>();

            this.isInputErrorOccured = false;
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
            this.UpdateBackColor(this.Focused);
        }

        // 背景色更新
        protected override void OnEnabledChanged(EventArgs e)
        {
            this.UpdateBackColor();
            base.OnEnabledChanged(e);
        }

        protected override void OnReadOnlyChanged(EventArgs e)
        {
            this.UpdateBackColor();
            base.OnReadOnlyChanged(e);
        }

        /// <summary>
        /// Windowsメッセージハンドラ
        /// </summary>
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            // WM_PAINT
            if (m.Msg == 0x000F)
            {
                CustomControlLogic.DrawControlBorder(this);
            }
        }

        /// <summary>
        /// OnEnter直後のTextを保持します
        /// Validatingの先頭で、変更有無のチェック等での利用を想定
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue("")]
        public string PrevText { get; set; }
        private bool ShouldSerializePrevText()
        {
            return !string.IsNullOrWhiteSpace(this.prevText);
        }
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [DefaultValue("")]
        public string prevText { get { return this.PrevText; } set { this.PrevText = value; } }
        private bool ShouldSerializeprevText()
        {
            return !string.IsNullOrWhiteSpace(this.prevText);
        }

        /// <summary>
        /// 変更があったかどうか（prevTextと現在値の比較結果を返す）
        /// </summary>
        /// <returns></returns>
        public bool isChanged()
        {
            if (IsInputErrorOccured || string.IsNullOrEmpty(this.Text))
            {
                return true; //エラー時と空入力時は必ず変更有としてチェックさせる
            }
            return !string.Equals(this.prevText, this.Text);
        }

        /// <summary>
        /// Enterイベントハンドラ(フォーカス取得)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnEnter(EventArgs e)
        {
            if (!this.isInputErrorOccured) //エラー出ないときだけ保存する
            {
                this.prevText = this.Text; //前回値保存
            }
            else
            {
                this.prevText = null; //エラーを一度出したら、次は必ずチェックするようにする
            }
            //フォーカス取得時にフラグをリセットする
            //->再度、画面側のチェックで問題が発生した場合はフラグがセットされる
            this.isInputErrorOccured = false;

            this.UpdateBackColor(true);

            base.OnEnter(e);

            // 入力モードがひらがななら強制的に変換モードを有効にする。
            // 勝手に変換モードが無変換になってしまうことがある現象の対策。
            r_framework.Utility.ImeUtility.AdjustControlImeSentenceMode(this);

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

            // 1次マスタのフレームワークより
            // フリガナ設定のあるコントロールの場合
            if (this.imeFuri == null && !string.IsNullOrWhiteSpace(this.FuriganaAutoSetControl))
            {
                this.imeFuri = new NativeWindowContorol(this);
                this.imeFuri.OnConverted += new NativeWindowContorol.Converted(OnImeConvertedEvent);
                this.imeFuri.MsgEnabled = true;
            }

            // No.3873-->
            if (this.ReadOnly == false)
            {
                //base.SelectAll(); // 全選択にする
                if (this.IsHandleCreated && !this.Disposing && !this.IsDisposed)
                {
                    this.BeginInvoke((Action)base.SelectAll);
                }
            }
            // No.3873<--
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

        // No.3873<--

        /// <summary>
        /// Leaveイベントハンドラ(フォーカス移動)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.UpdateBackColor(false);

            isSelectAll = false;    // No.3873
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
        /// KeyPressイベントハンドラ
        /// </summary>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                // Enter送りの際に音が鳴ってしまうのを防止
                if (!(this.Multiline && this.AcceptsReturn))
                {
                    e.Handled = true;
                }
            }
            //タブの音防止
            if (e.KeyChar == '\t')
            {
                e.Handled = true;
            }

            //全角スペースチェック KeyDownだと漢字ボタンと区別がつかないため、KeyPressで判断が必要(SHift+spaceで半角も来る）
            if (this.downSpace && (e.KeyChar == '　' || e.KeyChar == ' ') && !string.IsNullOrEmpty(this.PopupWindowName))
            {
                // テキストボックスにてスペースキー押下時の処理
                // ポップアップ画面が設定されている場合は、表示を行う
                var cstmLogic = new CustomControlLogic(this, this.DisplayPopUp, null);
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
                e.Handled = true;//入力キャンセル
            }

            base.OnKeyPress(e);
        }

        /// <summary>
        /// PreviewKeyDownイベントハンドラ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                // フォーカス移動はSuperForm,～BaseFormのKeyDownで処理するため
                // Tabボタンも通常キー入力とする
                e.IsInputKey = true;
            }

            base.OnPreviewKeyDown(e);

            // アクティブなテキストボックスのIME変換モードを設定する。
            // 勝手に変換モードが無変換になってしまうことがある現象の対策。
            r_framework.Utility.ImeUtility.AdjustControlImeSentenceMode(this);
        }

        /// <summary>
        /// KeyDownイベントハンドラ
        /// </summary>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space && !string.IsNullOrEmpty(this.PopupWindowName))
            {
                // テキストボックスにてスペースキー押下時の処理
                // ポップアップ画面が設定されている場合は、表示を行う
                var cstmLogic = new CustomControlLogic(this, this.DisplayPopUp, null);
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
                switch (cstmLogic.ShowPopupWindow(this, fields, this, sendParamArray))
                {
                    case DialogResult.OK:
                        this.prevText = ""; //前回値クリアして必ずvalidatingさせる
                        break;

                    default:
                        break;
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

            base.OnKeyDown(e);
        }

        /// <summary>
        /// Validating中かどうか
        /// </summary>
        private bool _IsValidating = false;

        /// <summary>
        /// 自動チェック処理。
        /// 閉じるボタンなどはCausesValidation=falseにすることで、クリック時にチェックを走らせないことが可能。
        /// </summary>
        /// <param name="e">チェックNGの場合はcancel=trueでフォーカス移動をキャンセル</param>
        protected override void OnValidating(CancelEventArgs e)
        {
            //FW_QA74:LeaveをValidatingへ移動

            if (this.ReadOnly) return; //読み取り専用の場合処理しない

            this._IsValidating = true;//validating中は エラー以外の色制御しない（フォーカスアウト後にvalidatingが動くのでそこでフォーカス色になる）
            try
            {
                // ゼロ埋め処理
                var textLogic = new CustomTextBoxLogic(this);
                textLogic.ZeroPadding(this);

                // 自動フォーマット処理
                textLogic.Format(this);

                // 大文字変換処理
                textLogic.ChangeUpperCase(this);

                // 全角変換処理
                textLogic.ChangeWideCase(this);

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

                if (e.Cancel)
                {
                    this.IsInputErrorOccured = true;
                }

                //チェックしてから、業務や継承コントロール側のチェックへ移る
                //　※イベントを使わずに、オーバーライドする継承先は先にbaseを動かしてから自前チェックすること！
                base.OnValidating(e);
            }
            finally
            {
                this._IsValidating = false;
            }
        }

        /// <summary>
        /// テキスト変更時処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnTextChanged(EventArgs e)
        {
            if (!this._IsValidating) //validating中はエラーにしか色は変えない！
            {
                this.IsInputErrorOccured = false;
                this.UpdateBackColor();
            }

            var ctrlUtil = new ControlUtility();
            var textLogic = new CustomTextBoxLogic(this);
            var fields = ctrlUtil.GetAllControls(ControlUtility.GetTopControl(this));

            // フリガナ設定処理
            if (!string.IsNullOrEmpty(this.FuriganaAutoSetControl))
            {
                // 1次マスタのフレームワークより
                if (string.IsNullOrWhiteSpace(this.Text))
                {
                    textLogic.SettingFuriganaPhase1(this, this.FuriganaAutoSetControl, fields, string.Empty);
                }
            }

            // 自動複写処理
            if (!string.IsNullOrEmpty(this.CopyAutoSetControl))
            {
                textLogic.SettingCopyValue(this, fields);
            }
            base.OnTextChanged(e);
        }

        /// <summary>
        /// IME変換イベント処理
        /// 1次マスタのフレームワークより
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnImeConvertedEvent(object sender, ConvertedEventArgs e)
        {
            var ctrlUtil = new ControlUtility();
            var textLogic = new CustomTextBoxLogic(this);
            var fields = ctrlUtil.GetAllControls(ControlUtility.GetTopControl(this));

            // フリガナ設定処理
            textLogic.SettingFuriganaPhase1(this, this.FuriganaAutoSetControl, fields, e.YomiString);
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

        [Category("EDISONプロパティ")]
        [Browsable(false)]
        public Color DefaultBackColor { get; set; }
        private bool ShouldSerializeDefaultBackColor()
        {
            return this.DefaultBackColor != null;
        }

        /// <summary>
        /// ポップアップへ送信するコントロール
        /// </summary>
        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("ポップアップへ送信するコントロール指定")]
        public string[] PopupSendParams { get; set; }
        private bool ShouldSerializePopupSendParams()
        {
            return this.PopupSendParams != null;
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

        [Category("EDISONプロパティ_画面設定")]
        [Description("画面に表示する項目の日本語名を指定してください。")]
        public string DisplayItemName { get; set; }
        private bool ShouldSerializeDisplayItemName()
        {
            return this.DisplayItemName != null;
        }

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
        public string PopupGetMasterField { get; set; }
        private bool ShouldSerializePopupGetMasterField()
        {
            return this.PopupGetMasterField != null;
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

        [Category("EDISONプロパティ_ポップアップ設定")]
        [Description("マスタチェック時に存在した場合、値の設定を行うならば、設定を行うコントロール名を「,」区切りで入力してください。")]
        public string PopupSetFormField { get; set; }
        private bool ShouldSerializePopupSetFormField()
        {
            return this.PopupSetFormField != null;
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
        #endregion Property

        /// <summary>
        /// 値の取得処理
        /// </summary>
        public string GetResultText()
        {
            return this.Text;
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

            if (this.ItemDefinedTypes == DB_TYPE.MONEY.ToTypeString())
            {
                this.Text = string.Format("{0:#,0}", value);
                return;
            }

            this.Text = value;
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

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
        }

        /// <summary>ポップアップを開く前に実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl> PopupBeforeExecute { get; set; }

        /// <summary>ポップアップから戻ってきたら実行されるイベント</summary>
        [Browsable(false)]
        public Action<ICustomControl, DialogResult> PopupAfterExecute { get; set; }
    }
}