using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using r_framework.CustomControl;
using r_framework.Utility;
using r_framework.Dao;
using r_framework.Entity;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using System.Xml;
using System.Runtime.InteropServices;
using System.Text;
using Shougun.Core.ExternalConnection.CommunicateLib.Helpers;

namespace r_framework.APP.Base
{
    /// <summary>
    /// ベースフォームのベースクラス
    /// </summary>
    public class BaseBaseForm : Form
    {
        // 20150902 katen #12048 「システム日付」の基準作成、適用 start‏
        /// <summary>
        /// システム日付
        /// </summary>
        public DateTime sysDate;
        // 20150902 katen #12048 「システム日付」の基準作成、適用 end‏

        /// <summary>埋め込み用の実作業を行うForm</summary>
        public Form inForm;

        /// <summary>リボンメニューのForm</summary>
        public Form ribbonForm;

        /// <summary>埋め込み用ヘッダーForm</summary>
        public Form headerForm;

        /// <summary>ポップアップタイプで表示するか否かの状態を保持するプロパティ</summary>
        /// <remarks>真の場合：ポップアップタイプで表示する、偽の場合：ポップアップタイプで表示しない</remarks>
        public bool IsPopupType { get; set; }

        /// <summary><para>明細フォームがリサイズ対応かどうか。</para>
        /// <para>画面側で対応している場合はtrueに。IsPopupType = falseが前提</para></summary>
        public bool IsInFormResizable { get; set; }

        //Communicate InxsSubApplication Start
        public delegate void OnReceiveMessage(string message);
        public event OnReceiveMessage OnReceiveMessageEvent;
        //Communicate InxsSubApplication End

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (!this.IsPopupType & this.IsInFormResizable)
            {
                this.ShowContentForm();
            }
        }

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>コンストラクタ</summary>
        public BaseBaseForm()
        {
            InitializeComponent();
            // 既存にあわせデフォルトはリサイズ未対応。
            this.IsInFormResizable = false;

            this.IsPopupType = false;

        }

        //FW_QA74:LeaveをValidatingへ移動
        private const int WM_CLOSE = 0x10;
        private const int WM_SYSCOMMAND = 0x112;
        private const int SC_CLOSE = 0xF060;
        private const int WM_COPYDATA = 0x4A;

        /// <summary>
        /// メッセージポンプ
        /// ・閉じる系の処理はvalidateをキャンセル
        /// </summary>
        protected override void WndProc(ref Message m)
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
            //Communicate InxsSubApplication Start
            if (m.Msg == WM_COPYDATA)
            {
                COPYDATASTRUCT mystr = new COPYDATASTRUCT();
                Type mytype = mystr.GetType();
                mystr = (COPYDATASTRUCT)m.GetLParam(mytype);

                byte[] bytes = Encoding.UTF8.GetBytes(mystr.lpData);
                string plaintext = Encoding.UTF8.GetString(bytes);

                if (!string.IsNullOrWhiteSpace(plaintext))
                {
                    var args = EncryptionUtility.Decrypt(plaintext).Trim();
                    if (OnReceiveMessageEvent != null)
                    {
                        OnReceiveMessageEvent(args);
                    }
                }
            }
            //Communicate InxsSubApplication End
            base.WndProc(ref m);
        }

        /// <summary>
        /// フォーカスが移ったときにヒントテキストを表示する
        /// </summary>
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

                    CurrentTabIndex = ActiveControl.TabIndex;
                }
            }
        }

        /// <summary>
        /// フォーカスイン時に実行されるメソッドの追加を行う
        /// </summary>
        /// <param name="c">追加を行う対象のコントロール</param>
        /// <returns></returns>
        private void Control_Enter(Control c)
        {
            c.Enter -= c_GotFocus;
            c.Enter += c_GotFocus;
        }

        /// <summary>
        /// ベースFormのロード処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            this.Visible = false;

            base.OnLoad(e);

            if (this.DesignMode)
            {
                this.Visible = true;
                return;
            }

            //イベント
            this.txb_process.TextChanged += new System.EventHandler(txb_process_TextChanged);
            this.txb_process.Leave += new System.EventHandler(txb_process_Leave);


            var allControl = controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }

            //// 初期サイズ調整
            this.ShowContentForm();

            //OnShownへ移動（起動時のコントロールのフォーカスインの色が変わらない）
            //// タブインデックスが最初のものにフォーカス
            //Control firstControl;
            //if (ControlUtility.TryGetFirstTabIndexControl(controlUtil.GetAllControls(inForm), out firstControl))
            //{
            //    firstControl.Select();

            //    this.lb_hint.Text = (string)firstControl.Tag;//ヒント表示を強制
            //    this.CurrentTabIndex = firstControl.TabIndex;
            //    //this.c_GotFocus(firstControl, EventArgs.Empty); このタイミングでは うまく動かないので自前で処理
            //}

            if (this.StartPosition == FormStartPosition.WindowsDefaultLocation && this.Modal)
            {
                Form parentForm = Form.ActiveForm;
                if (parentForm != null)
                {
                    //this.Location = new System.Drawing.Point(parentForm.Location.X + (parentForm.Width - this.Width) / 2,
                    //                                         parentForm.Location.Y + (parentForm.Height - this.Height) / 2);
                    this.Location = new System.Drawing.Point(parentForm.Location.X, parentForm.Location.Y);
                }
                else
                {
                    this.StartPosition = FormStartPosition.CenterParent;
                }
            }

            this.Visible = true;
        }

        /// <summary>
        /// フォームが最初に表示されるときのイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // タブインデックスが最初のものにフォーカス
            Control firstControl;
            if (ControlUtility.TryGetFirstTabIndexControl(controlUtil.GetAllControls(inForm), out firstControl))
            {
                firstControl.Select();

                this.lb_hint.Text = (string)firstControl.Tag;//ヒント表示を強制
                this.CurrentTabIndex = firstControl.TabIndex;
                //this.c_GotFocus(firstControl, EventArgs.Empty); このタイミングでは うまく動かないので自前で処理
            }

            if (!this.IsPopupType)
            {
                // 循環参照となるためBusinessCommonを参照設定できず
                // CurrentUserCustomConfigProfile.csを利用できないため
                // ピンポイントでXMLファイルを読み込ませる
                string ItemName = string.Empty;
                string ItemValue = string.Empty;
                var xmlDoc = new XmlDataDocument();
                xmlDoc.Load(r_framework.Configuration.AppData.CurrentUserCustomConfigProfilePath);
                var ItemSettings = xmlDoc.SelectNodes("//ItemSettings");
                
                foreach(XmlNode emp in ItemSettings)
                {
                    ItemName = emp.SelectSingleNode("Name").InnerText;
                    ItemValue = emp.SelectSingleNode("Value").InnerText;
                    if (ItemName=="画面表示サイズ")
                    {
                        break;
                    }
                }
                if (ItemValue.Length == 0)
                {
                    ItemValue = "1";
                }
                // 最大化
                if (ItemValue == "2")
                {

                    this.WindowState = FormWindowState.Maximized;
                }
                else
                {
                    this.WindowState = FormWindowState.Normal;
                }
            }

            base.OnShown(e);
        }

        /// <summary>メニュー/ヘッダー/明細フォームの表示</summary>
        private void ShowContentForm()
        {
            if (this.ribbonForm == null) return;

            //キー入力をフォームで受けるための設定
            this.KeyPreview = true;

            int headerY = 6; //マージン
            if (this.IsPopupType)
            {
                // ポップアップタイプで表示する場合
                this.ribbonForm.Visible = false;

                this.ProcessButtonPanel.Visible = false; //サブファンクションを非表示
                this.MaximizeBox = false; //最大化禁止
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle; //ウインドウサイズ固定
                this.statusStrip1.Visible = false; //ステータスバー消す
                this.ribbonForm.Visible = false; //inFormのリサイズ後にしないと、なぜかリボンが消えない。
                this.inForm.Anchor = AnchorStyles.Left | AnchorStyles.Top; // アンカーリセット
            }
            else
            {
                //表示の際は加算する
                headerY += this.ribbonForm.Location.Y + this.ribbonForm.Size.Height;
            }

            // ポップアップでなく、明細フォームがリサイズ対応の場合は計算値
            var inFormHeight = !this.IsPopupType & this.IsInFormResizable
                                ? this.Height - (headerY + this.headerForm.Height + this.headerForm.Size.Height + 10 + this.pn_foot.Height + this.statusStrip1.Height)
                                : 490;
            var inFormWidth = !this.IsPopupType & this.IsInFormResizable ? this.ClientSize.Width - 20 : this.inForm.Size.Width;

            new[] {
                // メニュー (リボンはフォームと同じサイズに)
                new { f = this.ribbonForm, l = new Point(0, 0), s = new Size(this.ClientSize.Width ,this.ribbonForm.Height)}, 
                // ヘッダー（ヘッダはサイズ変更しない）
                new { f = this.headerForm, l = new Point(12, headerY), s = new Size()},
                // 明細（高さだけ自動調整する）
                new { f = this.inForm, l = new Point(10, headerY + this.headerForm.Size.Height + 10), s = new Size { Height = inFormHeight , Width = inFormWidth }}
                }.ToList().ForEach(content => this.ShowContent(content.f, content.l, content.s));

            if (this.IsPopupType)
            {
                // ポップアップタイプで表示する場合
                this.ribbonForm.Visible = false; //inFormのリサイズ後にしないと、なぜかリボンが消えない。

                //非表示分フォームを小さく
                //　リボン分減らす（アンカーで移動）
                this.Height = this.Height - this.ribbonForm.Height;

                var bk = this.pn_foot.Location;
                //ステータスバー分減らす
                this.Height = this.Height - this.statusStrip1.Height;
                //パネルが自動で上にずれるので（半分、22縮めると11上に移動していた）
                this.pn_foot.Location = bk;

                this.Width = this.Width - this.ProcessButtonPanel.Width;
            }
        }

        /// <summary>BusinessBaseFormにshowFormを表示する</summary>
        /// <param name="showForm">表示するフォーム</param>
        /// <param name="showLocation">表示位置</param>
        /// <param name="showSize">表示サイズ</param>
        private void ShowContent(Form showForm, Point showLocation, Size showSize)
        {
            showForm.TopLevel = false;
            if (!showSize.IsEmpty)
            {
                showForm.Size = showSize;
            }

            showForm.Location = showLocation;
            this.Controls.Add(showForm);
            showForm.Show();
        }

        /// <summary>
        /// ESCボタンを押された際にフォーカスのあったコントロール
        /// </summary>
        [Browsable(false)]
        protected Control EscapedControl { get; set; }

        /// <summary>
        /// KeyDown
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            //デバッグ用
            if (e.KeyCode == Keys.Pause)
            {
                ShowFormInfo(this, this.ribbonForm, this.headerForm, this.inForm, this.pn_foot, this.ProcessButtonPanel, this.statusStrip1);
            }

            if (this.imeStatus.IsConversion)
            {
                return;
            }

            if (e.Alt)
            {
                // ESCにフォーカスがある場合はキー操作によるリボンへのフォーカス移動は行わない
                if (txb_process != null && this.ActiveControl == txb_process)
                {
                    e.Handled = true;
                    return;
                }

                // リボンにフォーカスがある場合は元のコントロールにフォーカスを戻す
                if (this.ribbonForm != null && this.ActiveControl == this.ribbonForm)
                {
                    this.SetFocusOnEscapedControl();
                    e.Handled = true;
                    return;
                }

                // リボンとESC以外にフォーカスがある場合はリボンにフォーカス
                if (this.ribbonForm != null && this.ribbonForm.Visible)
                {
                    this.EscapedControl = ControlUtility.GetActiveControl(this); //現在のアクティブコントロールを保存※フォームの下のコントロールにも潜る
                    this.SetFocusOnSpecificControl(this.ribbonForm);
                    e.Handled = true;
                    return;
                }

            }

            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (this.ActiveControl == inForm)
                {
                    //var ctrl = inForm.ActiveControl;
                    var ctrl = ControlUtility.GetActiveControl(inForm);//コンテナコントロール対策
                    if (ctrl != null)
                    {
                        // DataGridViewのフォーカス移動はCustomDataGridViewで対応
                        if (ctrl is CustomDataGridView || ctrl is DataGridViewComboBoxEditingControl)
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

                    } //ボタンを押せない場合は何もしない

                    e.Handled = true;
                    return;
                }

                // TAB ボタンを押した場合はタブインデックスが最初のものにフォーカス
                bool flag = HeaderBaseForm.HeaderFocusFlag;
                // タブインデックスが最初のものにフォーカス
                if (flag)
                {
                    string a = Convert.ToString(e.KeyData);
                    if (e.KeyData.Equals(Keys.LButton | Keys.Back | Keys.Shift))
                    {
                        this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
                    }
                    else
                        if (!SetFocusFirstControl())
                        {
                            this.SelectNextControl(this.ActiveControl, !e.Shift, true, true, true);
                        }
                }
                else
                {
                    if (!SetFocusFirstControl())
                    {
                        Control firstControl;
                        if (ControlUtility.TryGetFirstTabIndexControl(controlUtil.GetAllControls(inForm), out firstControl))
                        {
                            firstControl.Select();
                        }
                    }
                }
                e.Handled = true;

            }

            if (e.KeyCode == Keys.Escape)
            {
                // リボンにフォーカスがある場合はキー操作によるESCへのフォーカス移動は行わない
                if (this.ribbonForm != null && this.ActiveControl == this.ribbonForm)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true; //beep音を消す
                    return;
                }

                //課題 #1545 ESCサブファンクション番号入力対応
                //ESC中にESCを押されたらフォーカスを戻す
                if (txb_process != null && this.ActiveControl == txb_process)
                {
                    this.SetFocusOnEscapedControl();
                    e.Handled = true;
                    e.SuppressKeyPress = true; //beep音を消す
                    return;
                }

                //ESC以外でESCを押されたらESCにフォーカス移動＋コントロールを保存
                if (txb_process != null && txb_process.Visible)
                {
                    this.SetFocusOnSpecificControl(this.txb_process);
                    e.Handled = true;
                    e.SuppressKeyPress = true; //beep音を消す
                    return;
                }

                //beep音は消す
                e.Handled = true;
                e.SuppressKeyPress = true; //beep音を消す
                return;

            }

            //KeyUpに移動
            //if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            //{
            //    var buttonName = "bt_func" + (e.KeyCode - Keys.F1 + 1);
            //    ControlUtility.ClickButton(this, buttonName);
            //    e.Handled = true;
            //}


            base.OnKeyDown(e);
        }

        /// <summary>
        /// KeyUp
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (this.imeStatus.IsConversion)
            {
                return;
            }

            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            {
                var buttonName = "bt_func" + (e.KeyCode - Keys.F1 + 1);
                ControlUtility.ClickButton(this, buttonName);
                e.Handled = true;
            }

            base.OnKeyUp(e);
        }

        //課題 #1545 ESCサブファンクション番号入力対応

        /// <summary>
        /// 1文字コントロールなので入力される都度全選択して常時書き換えできるようにする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txb_process_TextChanged(object sender, EventArgs e)
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
                //ClickButtonではフォーカス移動するので、いったん退避
                var act = this.EscapedControl;

                SetFocusOnEscapedControl();

                ret = ControlUtility.ClickButton(this, "bt_process" + no);

                //処理後に戻す
                this.EscapedControl = act;

                return ret;
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

                    var act = this.EscapedControl;

                    act.Focus(); //保存したコントロールへフォーカス戻す

                    if (act.Parent != null && !((act.Parent is Panel) || (act.Parent is GroupBox))) act.Parent.Focus();

                    this.EscapedControl = null; //クリア
                    ret = true;
                }

                return ret;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
        }

        /// <summary>
        /// 現在のフォーカス位置を保存してから
        /// 指定のコントロールにフォーカスを移動します。
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        protected bool SetFocusOnSpecificControl(Control control)
        {
            //ログ出力
            LogUtility.DebugMethodStart(control);

            //ログ用戻り値 finallyで使うのでTRYのスコープ外が必要
            bool ret = false;

            try
            {
                this.EscapedControl = ControlUtility.GetActiveControl(this); //現在のアクティブコントロールを保存※フォームの下のコントロールにも潜る

                //グリッドのEditingControlにフォーカスがあったときにおかしな動きになるので、その対応。
                if (this.EscapedControl is IDataGridViewEditingControl)
                {
                    IDataGridViewEditingControl c = this.EscapedControl as IDataGridViewEditingControl;
                    this.EscapedControl = c.EditingControlDataGridView; //フォーカスはグリッドに合わせればセルもアクティブになる
                }
                else if (this.EscapedControl is GrapeCity.Win.MultiRow.IEditingControl)
                {
                    GrapeCity.Win.MultiRow.IEditingControl c = this.EscapedControl as GrapeCity.Win.MultiRow.IEditingControl;
                    this.EscapedControl = c.GcMultiRow;
                }

                control.Focus(); //指定のコントロールにフォーカス移動
                ret = true;

                return ret;
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

        //フォーカス制御
        /// <summary>
        /// フォーカスがあたっているコントロール
        /// </summary>
        [Browsable(false)]
        public int CurrentTabIndex { get; set; }

        /// <summary>
        ///  F12でTAB ボタンを押した場合はタブインデックスが最初のものにフォーカス
        /// </summary>
        public bool SetFocusFirstControl()
        {
            Control lastControl;
            if (ControlUtility.TryBaseFormGetLastTabIndexControl(controlUtil.GetAllControls(this), CurrentTabIndex, out lastControl))
            {
                if (ControlUtility.TryGetFirstTabIndexControl(controlUtil.GetAllControls(inForm), out lastControl))
                {
                    lastControl.Select();
                }

                return true;
            }

            return false;
        }

        ///DEBUG用
        protected static void ShowFormInfo(Form baseForm, Form ribbonForm, Form headerForm, Form inForm, Panel footPnl, Panel subfuncPnl, StatusStrip sts)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Control o;
            string nm;

            o = baseForm;
            nm = "Form";
            sb.AppendFormat("{0}:Size={1}*{2},ClientSize={3}*{4}", nm, o.Size.Width, o.Height, o.ClientSize.Width, o.ClientSize.Height).AppendLine();
            o = ribbonForm;
            nm = "ribbon";
            sb.AppendFormat("{0}({7}):Size={1}*{2},ClientSize={3}*{4},Location={5}*{6}", nm, o.Size.Width, o.Height, o.ClientSize.Width, o.ClientSize.Height, o.Location.X, o.Location.Y, o.Visible).AppendLine();
            o = headerForm;
            nm = "head";
            sb.AppendFormat("{0}({7}):Size={1}*{2},ClientSize={3}*{4},Location={5}*{6}", nm, o.Size.Width, o.Height, o.ClientSize.Width, o.ClientSize.Height, o.Location.X, o.Location.Y, o.Visible).AppendLine();
            o = inForm;
            nm = "inForm";
            sb.AppendFormat("{0}({7}):Size={1}*{2},ClientSize={3}*{4},Location={5}*{6}", nm, o.Size.Width, o.Height, o.ClientSize.Width, o.ClientSize.Height, o.Location.X, o.Location.Y, o.Visible).AppendLine();
            o = footPnl;
            nm = "foot";
            sb.AppendFormat("{0}({7}):Size={1}*{2},ClientSize={3}*{4},Location={5}*{6}", nm, o.Size.Width, o.Height, o.ClientSize.Width, o.ClientSize.Height, o.Location.X, o.Location.Y, o.Visible).AppendLine();
            o = subfuncPnl;
            nm = "sub";
            sb.AppendFormat("{0}({7}):Size={1}*{2},ClientSize={3}*{4},Location={5}*{6}", nm, o.Size.Width, o.Height, o.ClientSize.Width, o.ClientSize.Height, o.Location.X, o.Location.Y, o.Visible).AppendLine();
            o = sts;
            nm = "status";
            sb.AppendFormat("{0}({7}):Size={1}*{2},ClientSize={3}*{4},Location={5}*{6}", nm, o.Size.Width, o.Height, o.ClientSize.Width, o.ClientSize.Height, o.Location.X, o.Location.Y, o.Visible).AppendLine();

            MessageBox.Show(sb.ToString(), "フォーム情報");

        }

        protected override void OnActivated(System.EventArgs e)
        {
            base.OnActivated(e);

            // アクティブなテキストボックスのIME変換モードを設定する。
            // 勝手に変換モードが無変換になってしまうことがある現象の対策。
            r_framework.Utility.ImeUtility.AdjustControlImeSentenceMode(this.ActiveControl);
        }

        /// <summary>
        /// 非アクティブ時のイベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnDeactivate(EventArgs e)
        {
            base.OnDeactivate(e);
            // クラウド版でマスタメニューを開いたまま、
            // 他のアプリをアクティブにした時、
            // マスタメニューが残ってしまう対応
            if (!r_framework.Configuration.AppConfig.IsForeground() &&
                 r_framework.Configuration.AppConfig.IsTerminalMode)
            {
                ((RibbonMainMenu)this.ribbonForm).CloseOrbDropDown();
            }
        }

        public void SetEscapedControl(Control ctrl)
        {
            this.EscapedControl = ctrl;
        }

        #region デザイナ
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                FormManager.FormManager.NotifyDisposingForm(this);
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.pn_foot = new System.Windows.Forms.Panel();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.lb_hint = new System.Windows.Forms.Label();
            this.bt_func11 = new r_framework.CustomControl.CustomButton();
            this.bt_func10 = new r_framework.CustomControl.CustomButton();
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
            this.bt_func8 = new r_framework.CustomControl.CustomButton();
            this.bt_func7 = new r_framework.CustomControl.CustomButton();
            this.bt_func6 = new r_framework.CustomControl.CustomButton();
            this.bt_func5 = new r_framework.CustomControl.CustomButton();
            this.bt_func4 = new r_framework.CustomControl.CustomButton();
            this.bt_func3 = new r_framework.CustomControl.CustomButton();
            this.bt_func2 = new r_framework.CustomControl.CustomButton();
            this.bt_func1 = new r_framework.CustomControl.CustomButton();
            this.ProcessButtonPanel = new System.Windows.Forms.Panel();
            this.txb_process = new r_framework.CustomControl.CustomNumericTextBox2();
            this.bt_process5 = new r_framework.CustomControl.CustomButton();
            this.bt_process4 = new r_framework.CustomControl.CustomButton();
            this.bt_process2 = new r_framework.CustomControl.CustomButton();
            this.bt_process1 = new r_framework.CustomControl.CustomButton();
            this.bt_process3 = new r_framework.CustomControl.CustomButton();
            this.bt_process6 = new CustomButton();//160013
            this.lb_process = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progresBar = new System.Windows.Forms.ToolStripProgressBar();
            this.imeStatus = new r_framework.Components.ImeStatus(this.components);
            this.pn_foot.SuspendLayout();
            this.ProcessButtonPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_foot
            // 
            this.pn_foot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pn_foot.CausesValidation = false;
            this.pn_foot.Controls.Add(this.bt_func12);
            this.pn_foot.Controls.Add(this.lb_hint);
            this.pn_foot.Controls.Add(this.bt_func11);
            this.pn_foot.Controls.Add(this.bt_func10);
            this.pn_foot.Controls.Add(this.bt_func9);
            this.pn_foot.Controls.Add(this.bt_func8);
            this.pn_foot.Controls.Add(this.bt_func7);
            this.pn_foot.Controls.Add(this.bt_func6);
            this.pn_foot.Controls.Add(this.bt_func5);
            this.pn_foot.Controls.Add(this.bt_func4);
            this.pn_foot.Controls.Add(this.bt_func3);
            this.pn_foot.Controls.Add(this.bt_func2);
            this.pn_foot.Controls.Add(this.bt_func1);
            this.pn_foot.Location = new System.Drawing.Point(12, 639);
            this.pn_foot.Name = "pn_foot";
            this.pn_foot.Size = new System.Drawing.Size(999, 68);
            this.pn_foot.TabIndex = 204;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            // #12198対応によりコメントアウト。
            // 閉じるボタン押下してカーソルを動かした場合、一部画面（紙マニ、運賃）でフォーカスが抜けられる。
            // 下記ソースをコメントアウトし、最低１回はチェック処理を起動させる。
            //this.bt_func12.CausesValidation = false;
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func12.Location = new System.Drawing.Point(912, 29);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 390;
            this.bt_func12.Tag = "";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // lb_hint
            // 
            this.lb_hint.BackColor = System.Drawing.Color.Black;
            this.lb_hint.Font = new System.Drawing.Font("Meiryo", 9.75F);
            this.lb_hint.ForeColor = System.Drawing.Color.Yellow;
            this.lb_hint.Location = new System.Drawing.Point(3, 4);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(989, 21);
            this.lb_hint.TabIndex = 0;
            this.lb_hint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_func11
            // 
            this.bt_func11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func11.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func11.Enabled = false;
            this.bt_func11.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func11.Location = new System.Drawing.Point(831, 29);
            this.bt_func11.Name = "bt_func11";
            this.bt_func11.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func11.Size = new System.Drawing.Size(80, 35);
            this.bt_func11.TabIndex = 389;
            this.bt_func11.Tag = "";
            this.bt_func11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func11.UseVisualStyleBackColor = false;
            // 
            // bt_func10
            // 
            this.bt_func10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func10.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func10.Enabled = false;
            this.bt_func10.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func10.Location = new System.Drawing.Point(750, 29);
            this.bt_func10.Name = "bt_func10";
            this.bt_func10.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func10.Size = new System.Drawing.Size(80, 35);
            this.bt_func10.TabIndex = 388;
            this.bt_func10.Tag = "";
            this.bt_func10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func10.UseVisualStyleBackColor = false;
            // 
            // bt_func9
            // 
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Enabled = false;
            this.bt_func9.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func9.Location = new System.Drawing.Point(669, 29);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(80, 35);
            this.bt_func9.TabIndex = 387;
            this.bt_func9.Tag = "";
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            // 
            // bt_func8
            // 
            this.bt_func8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func8.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func8.Enabled = false;
            this.bt_func8.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func8.Location = new System.Drawing.Point(579, 29);
            this.bt_func8.Name = "bt_func8";
            this.bt_func8.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func8.Size = new System.Drawing.Size(80, 35);
            this.bt_func8.TabIndex = 386;
            this.bt_func8.Tag = "";
            this.bt_func8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func8.UseVisualStyleBackColor = false;
            // 
            // bt_func7
            // 
            this.bt_func7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func7.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func7.Enabled = false;
            this.bt_func7.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func7.Location = new System.Drawing.Point(498, 29);
            this.bt_func7.Name = "bt_func7";
            this.bt_func7.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func7.Size = new System.Drawing.Size(80, 35);
            this.bt_func7.TabIndex = 385;
            this.bt_func7.Tag = "";
            this.bt_func7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func7.UseVisualStyleBackColor = false;
            // 
            // bt_func6
            // 
            this.bt_func6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func6.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func6.Enabled = false;
            this.bt_func6.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func6.Location = new System.Drawing.Point(417, 29);
            this.bt_func6.Name = "bt_func6";
            this.bt_func6.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func6.Size = new System.Drawing.Size(80, 35);
            this.bt_func6.TabIndex = 384;
            this.bt_func6.Tag = "";
            this.bt_func6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func6.UseVisualStyleBackColor = false;
            // 
            // bt_func5
            // 
            this.bt_func5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func5.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func5.Enabled = false;
            this.bt_func5.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func5.Location = new System.Drawing.Point(336, 29);
            this.bt_func5.Name = "bt_func5";
            this.bt_func5.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func5.Size = new System.Drawing.Size(80, 35);
            this.bt_func5.TabIndex = 383;
            this.bt_func5.Tag = "";
            this.bt_func5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func5.UseVisualStyleBackColor = false;
            // 
            // bt_func4
            // 
            this.bt_func4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func4.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func4.Enabled = false;
            this.bt_func4.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func4.Location = new System.Drawing.Point(246, 29);
            this.bt_func4.Name = "bt_func4";
            this.bt_func4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func4.Size = new System.Drawing.Size(80, 35);
            this.bt_func4.TabIndex = 382;
            this.bt_func4.Tag = "";
            this.bt_func4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func4.UseVisualStyleBackColor = false;
            // 
            // bt_func3
            // 
            this.bt_func3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func3.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func3.Enabled = false;
            this.bt_func3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func3.Location = new System.Drawing.Point(165, 29);
            this.bt_func3.Name = "bt_func3";
            this.bt_func3.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func3.Size = new System.Drawing.Size(80, 35);
            this.bt_func3.TabIndex = 381;
            this.bt_func3.Tag = "";
            this.bt_func3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func3.UseVisualStyleBackColor = false;
            // 
            // bt_func2
            // 
            this.bt_func2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func2.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func2.Enabled = false;
            this.bt_func2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func2.Location = new System.Drawing.Point(84, 29);
            this.bt_func2.Name = "bt_func2";
            this.bt_func2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func2.Size = new System.Drawing.Size(80, 35);
            this.bt_func2.TabIndex = 380;
            this.bt_func2.Tag = "";
            this.bt_func2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func2.UseVisualStyleBackColor = false;
            // 
            // bt_func1
            // 
            this.bt_func1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func1.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func1.Enabled = false;
            this.bt_func1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func1.Location = new System.Drawing.Point(3, 29);
            this.bt_func1.Name = "bt_func1";
            this.bt_func1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func1.Size = new System.Drawing.Size(80, 35);
            this.bt_func1.TabIndex = 379;
            this.bt_func1.Tag = "";
            this.bt_func1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func1.UseVisualStyleBackColor = false;
            // 
            // ProcessButtonPanel
            // 
            this.ProcessButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessButtonPanel.Controls.Add(this.txb_process);
            this.ProcessButtonPanel.Controls.Add(this.bt_process5);
            this.ProcessButtonPanel.Controls.Add(this.bt_process4);
            this.ProcessButtonPanel.Controls.Add(this.bt_process2);
            this.ProcessButtonPanel.Controls.Add(this.bt_process1);
            this.ProcessButtonPanel.Controls.Add(this.bt_process3);
            this.ProcessButtonPanel.Controls.Add(this.lb_process);
            this.ProcessButtonPanel.Location = new System.Drawing.Point(1024, 528);
            this.ProcessButtonPanel.Name = "ProcessButtonPanel";
            this.ProcessButtonPanel.Size = new System.Drawing.Size(156, 179);
            this.ProcessButtonPanel.TabIndex = 391;
            // 
            // txb_process
            // 
            this.txb_process.BackColor = System.Drawing.SystemColors.Window;
            this.txb_process.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_process.DefaultBackColor = System.Drawing.Color.Empty;
            this.txb_process.DisplayPopUp = null;
            this.txb_process.ErrorMessage = "";
            this.txb_process.FocusOutCheckMethod = null;
            this.txb_process.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txb_process.ForeColor = System.Drawing.Color.Black;
            this.txb_process.GetCodeMasterField = "";
            this.txb_process.IsInputErrorOccured = false;
            this.txb_process.Location = new System.Drawing.Point(110, 155);
            this.txb_process.Name = "txb_process";
            this.txb_process.PopupAfterExecute = null;
            this.txb_process.PopupBeforeExecute = null;
            this.txb_process.PopupGetMasterField = "";
            this.txb_process.PopupSetFormField = "";
            this.txb_process.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txb_process.popupWindowSetting = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            5,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txb_process.RangeSetting = rangeSettingDto1;
            this.txb_process.RegistCheckMethod = null;
            this.txb_process.SetFormField = "";
            this.txb_process.Size = new System.Drawing.Size(23, 20);
            this.txb_process.TabIndex = 390;
            this.txb_process.TabStop = false;
            this.txb_process.Tag = " ";
            this.txb_process.WordWrap = false;
            // 
            // bt_process5
            // 
            this.bt_process5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_process5.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_process5.Enabled = false;
            this.bt_process5.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_process5.Location = new System.Drawing.Point(3, 123);
            this.bt_process5.Name = "bt_process5";
            this.bt_process5.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_process5.Size = new System.Drawing.Size(150, 30);
            this.bt_process5.TabIndex = 395;
            this.bt_process5.Tag = "";
            this.bt_process5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_process5.UseVisualStyleBackColor = false;
            // 
            // bt_process4
            // 
            this.bt_process4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_process4.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_process4.Enabled = false;
            this.bt_process4.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_process4.Location = new System.Drawing.Point(3, 93);
            this.bt_process4.Name = "bt_process4";
            this.bt_process4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_process4.Size = new System.Drawing.Size(150, 30);
            this.bt_process4.TabIndex = 394;
            this.bt_process4.Tag = "";
            this.bt_process4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_process4.UseVisualStyleBackColor = false;
            // 
            // bt_process2
            // 
            this.bt_process2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_process2.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_process2.Enabled = false;
            this.bt_process2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_process2.Location = new System.Drawing.Point(3, 33);
            this.bt_process2.Name = "bt_process2";
            this.bt_process2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_process2.Size = new System.Drawing.Size(150, 30);
            this.bt_process2.TabIndex = 392;
            this.bt_process2.Tag = "";
            this.bt_process2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_process2.UseVisualStyleBackColor = false;
            // 
            // bt_process1
            // 
            this.bt_process1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_process1.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_process1.Enabled = false;
            this.bt_process1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_process1.Location = new System.Drawing.Point(3, 3);
            this.bt_process1.Name = "bt_process1";
            this.bt_process1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_process1.Size = new System.Drawing.Size(150, 30);
            this.bt_process1.TabIndex = 391;
            this.bt_process1.Tag = "";
            this.bt_process1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_process1.UseVisualStyleBackColor = false;
            // 
            // bt_process3
            // 
            this.bt_process3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_process3.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_process3.Enabled = false;
            this.bt_process3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_process3.Location = new System.Drawing.Point(3, 63);
            this.bt_process3.Name = "bt_process3";
            this.bt_process3.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_process3.Size = new System.Drawing.Size(150, 30);
            this.bt_process3.TabIndex = 393;
            this.bt_process3.Tag = "";
            this.bt_process3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_process3.UseVisualStyleBackColor = false;
            //160013 S
            // 
            // bt_process6
            // 
            bt_process6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            bt_process6.DefaultBackColor = System.Drawing.Color.Empty;
            bt_process6.Enabled = false;
            bt_process6.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            bt_process6.Location = new System.Drawing.Point(3, 153);
            bt_process6.Name = "bt_process6";
            bt_process6.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            bt_process6.Size = new System.Drawing.Size(150, 30);
            bt_process6.TabIndex = 395;
            bt_process6.Tag = "";
            bt_process6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            bt_process6.UseVisualStyleBackColor = false;
            //160013 E
            // 
            // lb_process
            // 
            this.lb_process.BackColor = System.Drawing.Color.DarkGreen;
            this.lb_process.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_process.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_process.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lb_process.ForeColor = System.Drawing.Color.White;
            this.lb_process.Location = new System.Drawing.Point(3, 156);
            this.lb_process.Name = "lb_process";
            this.lb_process.Size = new System.Drawing.Size(100, 19);
            this.lb_process.TabIndex = 5;
            this.lb_process.Text = "処理No (ESC)";
            this.lb_process.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progresBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1192, 22);
            this.statusStrip1.TabIndex = 392;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progresBar
            // 
            this.progresBar.Name = "progresBar";
            this.progresBar.Size = new System.Drawing.Size(100, 16);
            // 
            // BaseBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1192, 730);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ProcessButtonPanel);
            this.Controls.Add(this.pn_foot);
            this.KeyPreview = true;
            this.Name = "BaseBaseForm";
            this.Text = "環境将軍Ｒ";
            this.pn_foot.ResumeLayout(false);
            this.ProcessButtonPanel.ResumeLayout(false);
            this.ProcessButtonPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        //実装必須コントロール
        public Label lb_hint;
        public CustomNumericTextBox2 txb_process;
        public System.Windows.Forms.Panel pn_foot;
        public System.Windows.Forms.Panel ProcessButtonPanel;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public Components.ImeStatus imeStatus;

        //共通
        public CustomButton bt_func12;
        public CustomButton bt_func11;
        public CustomButton bt_func10;
        public CustomButton bt_func9;
        public CustomButton bt_func8;
        public CustomButton bt_func7;
        public CustomButton bt_func6;
        public CustomButton bt_func5;
        public CustomButton bt_func4;
        public CustomButton bt_func3;
        public CustomButton bt_func2;
        public CustomButton bt_func1;
        public CustomButton bt_process6;//160013
        public CustomButton bt_process5;
        public CustomButton bt_process4;
        public CustomButton bt_process2;
        public CustomButton bt_process1;
        public CustomButton bt_process3;
        public System.Windows.Forms.Label lb_process;
        public System.Windows.Forms.ToolStripProgressBar progresBar;
        #endregion
    }
}
