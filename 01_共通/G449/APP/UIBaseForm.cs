using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;
using r_framework.APP.Base;
using r_framework.Logic;
using r_framework.FormManager;
using r_framework.Dto;

namespace Shougun.Core.Common.DenpyouHimodukeIchiran
{
    #region - Class -

    /// <summary>業務画面にて使用するベースクラス</summary>
    public partial class UIBaseForm : Form
    {
        #region - Fields -
        // 20150902 katen #12048 「システム日付」の基準作成、適用 start‏
        /// <summary>
        /// システム日付
        /// </summary>
        public DateTime sysDate;
        // 20150902 katen #12048 「システム日付」の基準作成、適用 end‏
        /// <summary>埋め込みようの実作業を行うForm</summary>
        public Form inForm;

        /// <summary>リボンメニューのForm</summary>
        public Form ribbonForm;

        /// <summary>埋め込みようヘッダーForm</summary>
        public Form headerForm;

        /// <summary>コントロールのユーティリティ</summary>
        private ControlUtility controlUtil = new ControlUtility();

        #endregion - Fields -

        #region - Constructors -

        /// <summary>コンストラクタ
        /// 引数で渡されたFormを埋め込み画面を表示する</summary>
        /// <param name="form">埋め込むForm</param>
        /// <param name="windowType">画面タイプ</param>
        public UIBaseForm(Form form, WINDOW_TYPE windowType, RibbonMainMenu ribbon = null)
        {
            this.InitializeComponent();

            if (ribbon == null)
            {
                this.ribbonForm = new RibbonMainMenu(FormManager.UserRibbonMenu.MenuConfigXML, (CommonInformation)FormManager.UserRibbonMenu.GlobalCommonInformation.Clone());
            }
            else
            {
                this.ribbonForm = new RibbonMainMenu(ribbon.MenuConfigXML, ribbon.GlobalCommonInformation);
            }

            this.inForm = form;

            switch (windowType)
            {
                case WINDOW_TYPE.ICHIRAN_WINDOW_FLAG:
                    this.headerForm = new ListHeaderForm();
                    break;
                default:
                    this.headerForm = new DetailedHeaderForm();
                    break;
            }

            this.ShowContentForm();
        }

        /// <summary>明細部にform、ヘッダー部にheaderForm</summary>
        /// <param name="form">明細フォーム</param>
        /// <param name="headerForm">ヘッダーフォーム</param>
        public UIBaseForm(Form form, HeaderBaseForm headerForm, RibbonMainMenu ribbon = null)
        {
            this.InitializeComponent();

            if (ribbon == null)
            {
                this.ribbonForm = new RibbonMainMenu(FormManager.UserRibbonMenu.MenuConfigXML, (CommonInformation)FormManager.UserRibbonMenu.GlobalCommonInformation.Clone());
            }
            else
            {
                this.ribbonForm = new RibbonMainMenu(ribbon.MenuConfigXML, ribbon.GlobalCommonInformation);
            }

            this.inForm = form;
            this.headerForm = headerForm;

            this.ShowContentForm();
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>ポップアップタイプで表示するか否かの状態を保持するプロパティ</summary>
        /// <remarks>真の場合：ポップアップタイプで表示する、偽の場合：ポップアップタイプで表示しない</remarks>
        public bool IsPopupType { get; set; }

        /// <summary>
        /// ESCボタンを押された際にフォーカスのあったコントロール
        /// </summary>
        protected Control EscapedControl { get; set; }

        #endregion - Properties -

        #region - Methods -

        /// <summary>メニュー/ヘッダー/明細フォームの表示</summary>
        private void ShowContentForm()
        {
            var x = 0;
            var y = 0;
            this.ribbonForm.Location = new Point(x, y);
            this.addSubForm(this.ribbonForm);
            y += this.ribbonForm.Size.Height;

            x = 12;
            y += 6;
            this.headerForm.Location = new Point(x, y);
            this.addSubForm(this.headerForm);
            y += this.headerForm.Size.Height;

            x = 10;
            y += 10;
            this.inForm.Location = new Point(x, y);
            this.inForm.Size = new Size(this.inForm.Size.Width, this.pn_foot.Location.Y - y);
            this.addSubForm(this.inForm);

            this.KeyPreview = true;
        }

        /// <summary>
        /// クライアント領域にサブフォームを追加表示する
        /// </summary>
        private void addSubForm(Form form)
        {
            form.TopLevel = false;
            this.Controls.Add(form);
            form.Show();
        }

        /// <summary>
        /// KeyDownイベントハンドラ
        /// </summary>
        private void UIBaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.imeStatus.IsConversion)
            {
                return;
            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (this.ActiveControl == inForm)
                {
                    var ctrl = inForm.ActiveControl;
                    if (ctrl != null)
                    {
                        // DataGridViewのフォーカス移動はCustomDataGridViewで対応
                        var dataGrid = ctrl as CustomDataGridView;
                        if (dataGrid != null)
                        {
                            return;
                        }
                    }
                }

                //課題 #1545 ESCサブファンクション番号入力対応
                if (txb_process != null && this.ActiveControl == txb_process)
                {

                    if (ExecuteProcess())
                    {
                        //ボタンを押した場合は、テキストを空にしてフォーカスを戻す
                        this.txb_process.Clear();
                        this.SetFocusOnEscapedControl();

                    } //ボタンを押せない場合は何もしない

                    e.Handled = true;
                    return;
                }
                

                // タブインデックスが最初のものにフォーカス
                Control firstControl;
                if (ControlUtility.TryGetFirstTabIndexControl(controlUtil.GetAllControls(inForm), out firstControl))
                {
                    firstControl.Select();
                }
                e.Handled = true;
            }

            if (e.KeyCode == Keys.Escape)
            {

                //課題 #1545 ESCサブファンクション番号入力対応
                //ESC中にESCを押されたらフォーカスを戻す
                if (txb_process != null && this.ActiveControl == txb_process)
                {
                    SetFocusOnEscapedControl();
                    e.Handled = true;
                    return;
                }

                //ESC以外でESCを押されたらESCにフォーカス移動＋コントロールを保存
                if (txb_process != null && txb_process.Visible)
                {
                    SetFocusOnEsc();
                    e.Handled = true;
                    return;
                }
            }

            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            {
                var buttonName = "bt_func" + (e.KeyCode - Keys.F1 + 1);
                ControlUtility.ClickButton(this, buttonName);
                e.Handled = true;
            }
        }

        /// <summary>画面サイズ変更時に埋め込まれたFormを拡大縮小するクラス
        /// C1導入時に不要になる可能性在り</summary>
        public virtual void BaseForm03_SizeChanged(object sender, System.EventArgs e)
        {
            if (this.inForm == null) return; //オートリサイズ暫定対策

            if (!this.IsPopupType)
            {   // ポップアップタイプで表示しない場合
                this.inForm.TopLevel = false;
                //FW_QA62:inFormがボタンに重なる
                //this.inForm.Size = new Size { Height = Size.Height - 280, Width = this.inForm.Size.Width };
                this.inForm.Size = new Size
                {
                    Height = this.pn_foot.Location.Y - this.inForm.Location.Y - 10,
                    Width = this.inForm.Size.Width
                };
                this.inForm.Location = new Point(12, this.headerForm.Location.Y + this.headerForm.Size.Height + 10);
            }

            this.Controls.Add(this.inForm);
            this.inForm.Show();
        }

        /// <summary>フォーカスが移ったときにヒントテキストを表示する</summary>
        void c_GotFocus(object sender, EventArgs e)
        {
            var activ = ActiveControl as Form;

            if (activ == null)
            {
                if (ActiveControl != null)
                {
                    if (this.ActiveControl == headerForm)
                    {
                        if (headerForm.ActiveControl != null)
                        {
                            this.lb_hint.Text = (string)headerForm.ActiveControl.Tag;
                        }
                        else
                        {
                            this.lb_hint.Text = string.Empty;
                        }
                    }
                    else
                    {
                        this.lb_hint.Text = (string)ActiveControl.Tag;
                    }
                }
            }
        }

        /// <summary>ロード</summary>
        /// <param name="sender">処理対象オブジェクト</param>
        /// <param name="e">イベント</param>
        private void UIBaseForm_Load(object sender, EventArgs e)
        {
            if (this.IsPopupType)
            {   // ポップアップタイプで表示する場合
                this.ribbonForm.Visible = false;

                this.headerForm.Location = new Point(12, this.headerForm.Location.Y - this.ribbonForm.Height);
                this.inForm.Location = new Point(10, this.inForm.Location.Y - this.ribbonForm.Height);

                this.Size = new Size(this.pn_foot.Width + 30, this.Size.Height - this.ribbonForm.Height); //サブファンクションを非表示、リボンを消すので高さを縮める
                this.ProcessButtonPanel.Visible = false; //サブファンクションを非表示
                this.progresBar.Visible = false; //プログレスバーを消す
                this.MaximizeBox = false; //最大化禁止
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle; //ウインドウサイズ固定
                this.statusStrip1.Visible = false; //ステータスバー消す

                //ポップアップフォーム対応
                this.inForm.Size = new Size
                {
                    Height = this.pn_foot.Location.Y - (this.headerForm.Location.Y + this.headerForm.Size.Height) - 10,
                    Width = this.inForm.Size.Width
                };

                this.ribbonForm.Visible = false; //inFormのリサイズ後にしないと、なぜかリボンが消えない。
            }

            var allControl = this.controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }

            // 初期サイズ調整
            this.BaseForm03_SizeChanged(null, null);

            // タブインデックスが最初のものにフォーカス
            Control firstControl;
            if (ControlUtility.TryGetFirstTabIndexControl(controlUtil.GetAllControls(inForm), out firstControl))
            {
                firstControl.Select();
            }
        }

        /// <summary>イベント処理の追加を行う</summary>
        /// <param name="c">追加を行う対象のコントロール</param>
        private void Control_Enter(Control c)
        {
            c.Enter -= c_GotFocus;
            c.Enter += c_GotFocus;
        }

        //FW_QA74:LeaveをValidatingへ移動
        private const int WM_CLOSE = 0x10;
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_CLOSE = 0xF060;
        /// <summary>
        /// メッセージポンプ
        /// ・閉じる系の処理はvalidateをキャンセル
        /// </summary>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {

            if (m.Msg == WM_CLOSE)
            {
                //base.WndProcの前に書き換え必要
                this.FindForm().AutoValidate = AutoValidate.Disable;
            }

            //×やALT+F4
            if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_CLOSE)
            {
                //base.WndProcの前に書き換え必要
                this.FindForm().AutoValidate = AutoValidate.Disable;
            }
            base.WndProc(ref m);
        }


        //課題 #1545 ESCサブファンクション番号入力対応

        /// <summary>
        /// 1文字コントロールなので入力される都度全選択して常時書き換えできるようにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txb_process_TextChanged(object sender , EventArgs e)
        {
            txb_process.SelectAll();//全選択にする

            //入力と同時に実行したい場合は以下を復活
            //ExecuteProcess();
        }

        /// <summary>
        /// ESCのテキストボックスの値を利用してサブファンクションの実行を試みる
        /// </summary>
        /// <returns>テキストが数値かつ該当ボタンがあった場合true、ボタンのEnabled=falseの場合falseが返る</returns>
        public bool ExecuteProcess()
        {
            //ログ出力
            LogUtility.DebugMethodStart();

            //ログ用戻り値 finallyで使うのでTRYのスコープ外が必要
            bool ret = false;

            try
            {
                int no = 0;
                if (int.TryParse(this.txb_process.Text.Trim(), out no))
                {
                    ret = ExecuteProcess(no);
                }
                else
                {
                    LogUtility.Debug("サブファンクション:非数値:" + this.txb_process.Text);
                }
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
        }

        /// <summary>
        /// 指定Noのサブファンクションを実行する
        /// </summary>
        /// <param name="no">サブファンクションNo</param>
        /// <returns>該当ボタンがあった場合true、ボタンのEnabled=falseの場合falseが返る</returns>
        public bool ExecuteProcess(int no)
        {
            //ログ出力
            LogUtility.DebugMethodStart(no);

            //ログ用戻り値 finallyで使うのでTRYのスコープ外が必要
            bool ret = false;

            try
            {
                ret = ControlUtility.ClickButton(this, "bt_process" + no);
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
        }

        public bool SetFocusOnEscapedControl()
        {
            //ログ出力
            LogUtility.DebugMethodStart();

            //ログ用戻り値 finallyで使うのでTRYのスコープ外が必要
            bool ret = false;

            try
            {
                if (this.EscapedControl != null)
                {
                    this.EscapedControl.Focus(); //保存したコントロールへフォーカス戻す
                    this.EscapedControl = null; //クリア
                    ret = true;
                }

                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
        }
        /// <summary>
        /// ESCを押された際にESCへフォーカスをセットします。
        /// </summary>
        /// <returns></returns>
        protected bool SetFocusOnEsc()
        {
            //ログ出力
            LogUtility.DebugMethodStart();

            //ログ用戻り値 finallyで使うのでTRYのスコープ外が必要
            bool ret = false;

            try
            {
                this.EscapedControl = ControlUtility.GetActiveControl(this); //現在のアクティブコントロールを保存※フォームの下のコントロールにも潜る
                txb_process.Focus(); //ESCにフォーカス移動
                ret = true;

                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
        }

        /// <summary>
        /// ESC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txb_process_Leave(object sender, EventArgs e)
        {
            this.EscapedControl = null;
        }
        #endregion - Methods -
    }

    #endregion - Class -
}
