using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Event;
using r_framework.Logic;
using r_framework.Utility;
using System.Linq;

namespace r_framework.APP.Base
{
    /// <summary>
    /// すべてのフォームの基となるフォーム情報
    /// </summary>
    [Serializable()]
    public partial class SuperForm : Form
    {
        // 20150902 katen #12048 「システム日付」の基準作成、適用 start‏
        /// <summary>
        /// システム日付
        /// </summary>
        public DateTime sysDate;
        // 20150902 katen #12048 「システム日付」の基準作成、適用 end‏
        /// <summary>
        /// 登録時エラーフラグ
        /// </summary>
        public bool RegistErrorFlag = true;

        /// <summary>
        /// フォーカスアウトエラーフラグ
        /// </summary>
        public bool FocusOutErrorFlag = false;

        /// <summary>
        /// 画面に表示しているすべてのコントロールを格納するフィールド
        /// </summary>
        public Control[] allControl;

        /// <summary>タイトルヘッダの変更をするか否かを保持するフィールド</summary>
        protected bool isHeaderTitleModify = false;

        /// <summary>
        /// 画面ID
        /// </summary>
        public WINDOW_ID WindowId { get; set; }

        /// <summary>
        /// 処理区分
        /// </summary>
        public PROCESS_KBN ProcessKbn { get; set; }

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>
        /// 画面のタイプ
        /// </summary>
        public WINDOW_TYPE WindowType { get; set; }

        /// <summary>
        /// キーイベント
        /// </summary>
        public KeyEventArgs KeyEventKP { get; set; }

        /// <summary>
        /// 前回値更新フラグ
        /// </summary>
        internal bool PreviousSaveFlag = true;

        /// <summary>
        /// フォーカス移動する前のコントロール
        /// </summary>
        private ICustomControl previousControl;

        /// <summary>
        /// 前回値を保持
        /// </summary>
        private string previousValue;

        /// <summary>
        /// 保持する前回値を取得・設定します
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("GetPreviousValue,SetPreviousValueを使用してください")]
        public string PreviousValue
        {
            get
            {
                return this.previousValue;
            }
            set
            {
                this.previousValue = value;
                this.previousControl = null;
            }
        }

        /// <summary>
        /// 前回値に関連するコントロールの値を保持
        /// </summary>
        /// <remarks>
        /// Key:コントロール名
        /// Value:値
        /// </remarks>
        internal Dictionary<string, string> PreviousControlValue;

        /// <summary>
        /// 登録時チェックイベントデリゲート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public delegate void UserRegistCheckHandler(object sender, RegistCheckEventArgs e);

        /// <summary>
        /// 登録時チェックイベント
        /// </summary>
        [Category("EDISONイベント")]
        [Description("エラーチェック用のイベント\r\n引数で渡されるコントロールに対してユーザーコードチェック処理が必要な場合に設定してください。")]
        public event UserRegistCheckHandler UserRegistCheck;

        /// <summary>
        /// ヒントラベル
        /// </summary>
        private Label hintLabel;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SuperForm()
        {
            InitializeComponent();
            if (this.DesignMode)
            {
                return;
            }

            this.AutoScroll = true;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SuperForm(WINDOW_ID windowId)
        {
            InitializeComponent();
            this.WindowId = windowId;

            if (this.DesignMode)
            {
                return;
            }

            this.AutoScroll = true;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SuperForm(WINDOW_ID windowId, WINDOW_TYPE windowType)
        {
            InitializeComponent();
            this.WindowType = windowType;
            this.WindowId = windowId;
            if (this.DesignMode)
            {
                return;
            }

            this.AutoScroll = true;
        }

        /// <summary>
        /// Formロード時にデータハンドリングテーブルから情報を取得し設定を行う
        /// </summary>
        private void SuperForm_Load(object sender, EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            this.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom;

            if (WINDOW_ID.MAIN_MENU != this.WindowId)
            {
                // ヘッダー初期化
                this.HeaderFormInit();
            }

            // ヒントラベルの取得
            this.hintLabel = controlUtil.FindControl(ControlUtility.GetTopControl(this), "lb_hint") as Label;

            // 全コントロールを取得
            if (this.allControl == null)
            {
                this.allControl = controlUtil.GetAllControls(this);
            }
            foreach (Control c in allControl)
            {
                var multiRow = c is GcMultiRow;
                if (!multiRow)
                {
                    Control_Enter(c);
                }
            }

            //ヘッダのヒント対応

            Control[] headerControls = null;
            if (this.Parent is BusinessBaseForm)
            {
                headerControls = controlUtil.GetAllControls(((BusinessBaseForm)this.Parent).headerForm);
            }
            else if (this.Parent is MasterBaseForm)
            {
                headerControls = controlUtil.GetAllControls(((MasterBaseForm)this.Parent).headerForm);
            }
            else if (this.Parent is IchiranBaseForm)
            {
                headerControls = controlUtil.GetAllControls(((IchiranBaseForm)this.Parent).headerForm);
            }
            if (headerControls != null)
            {
                foreach (var c in headerControls)
                {
                    if (!(c is GcMultiRow)) Control_Enter(c);
                }
            }

            //自動リサイズ 下部にコントロール配置している画面がおかしくなるので一旦封印
            //this.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        }

        /// <summary>
        /// ヘッダーフォーム初期化処理
        /// タイトルラベル、処理モード等を変更
        /// </summary>
        public void HeaderFormInit()
        {
            var parentForm = this.Parent ?? this;
            var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");
            if (titleControl != null && !this.isHeaderTitleModify)
            {   // タイトル変更しない場合はWINDOW_IDからタイトルを生成
                titleControl.Text = this.WindowId.ToTitleString();
            }
            // タイトルはフォームヘッダーの画面タイトル文字列と同じであること
            parentForm.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(titleControl == null ? this.WindowId.ToTitleString() : titleControl.Text);

            var typeLabel = (CustomWindowTypeLabel)controlUtil.FindControl(parentForm, "windowTypeLabel");
            if (typeLabel != null)
            {
                typeLabel.WindowType = this.WindowType;

                typeLabel.Text = WindowType.ToTypeString();

                if (string.IsNullOrEmpty(typeLabel.Text))
                {
                    typeLabel.Visible = false;
                    titleControl.Location = new Point(0, 6);
                }
                else
                {
                    typeLabel.Visible = true;
                    titleControl.Location = new Point(82, 6);
                }

                typeLabel.BackColor = WindowType.ToLabelColor();
                typeLabel.ForeColor = WindowType.ToLabelForeColor();

            }
        }

        /// <summary>
        /// Clickイベントメソッドの上書き
        /// </summary>
        public void C_Regist(Control c)
        {
            c.Click += this.RegistEvent;
        }

        /// <summary>
        /// 登録時の自動実行処理
        /// </summary>
        private void RegistEvent(object sender, EventArgs e)
        {
            var button = sender as CustomButton;

            this.ProcessKbn = button == null ? PROCESS_KBN.NONE : button.ProcessKbn;

            var autoCheckLogic = new AutoRegistCheckLogic(this.allControl);

            this.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
        }

        /// <summary>
        /// 現在フォーカスがあたっているコントロール
        /// </summary>
        [Category("EDISONプロパティ")]
        public Control FoucusControl { get; set; }

        /// <summary>
        /// ヒントラベルに値を設定するメソッド
        /// </summary>
        public string TextHint
        {
            set
            {
                var parentForm = Parent;
                var index = parentForm.Controls.IndexOfKey("lb_hint");

                parentForm.Controls[index].Text = value;
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
        /// フォーカスが移ったときにヒントテキストを表示する
        /// </summary>
        void c_GotFocus(object sender, EventArgs e)
        {
            if (this.hintLabel == null)
            {
                return;
            }

            string hintText = string.Empty;

            //this.FoucusControl = ActiveControl;
            this.FoucusControl = sender as Control;

            // フォーカスアウトエラーフラグ解除
            this.FocusOutErrorFlag = false;

            if (this.FoucusControl != null)
            {
                if (this.FoucusControl.Tag != null)
                {
                    hintText = this.FoucusControl.Tag.ToString();
                }
            }

            this.hintLabel.Text = hintText;
        }

        /// <summary>
        /// KeyDownイベントハンドラ
        /// </summary>
        public void SuperForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (this.imeStatus.IsConversion)
            {
                return;
            }

            this.KeyEventKP = e;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {

                //コンテナコントロール対応（末端のアクティブコントロールを取得を試みる）
                var act = ControlUtility.GetActiveControl(this);

                // DataGridViewのフォーカス移動はCustomDataGridViewで対応
                if (act is CustomDataGridView || act is DataGridViewComboBoxEditingControl)
                {
                    return;
                }

                // 複数行TextBoxなら改行入力
                var textBox = act as TextBox;
                if (textBox != null && textBox.Multiline && textBox.AcceptsReturn)
                {
                    e.Handled = true;
                    return;
                }

                // EnterキーでTabキーと同じようにフォーカス
                this.ProcessTabKey(!e.Shift);
                e.Handled = true;
            }

            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            {

                //課題 #1577 MultiRowでDateTimePicker等プルダウン系を非編集状態時にクリックするとF4をMultiRow側で発行している対策
                if (e.KeyCode == Keys.F4 && this.ActiveControl is GrapeCity.Win.MultiRow.DateTimePickerEditingControl)
                {
                    e.Handled = true; //falseだとフォーカスが入らない。trueだとフォーカスが入る。（フォーカス状態であれば、プルダウンを押されてもF4が発生しない）
                    return;
                    //制限事項として、日付セルにフォーカスがあると、F4を押すと編集状態になる。（他のセルはF4の処理が実行される）
                }

                //KeyUpに移動
                //var buttonName = "bt_func" + (e.KeyCode - Keys.F1 + 1);
                //ControlUtility.ClickButton(this, buttonName);
                //e.Handled = true;
            }
        }

        /// <summary>
        /// KeyUpイベントハンドラ
        /// </summary>
        public void SuperForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.imeStatus.IsConversion)
            {
                return;
            }

            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            {

                //課題 #1577 MultiRowでDateTimePicker等プルダウン系を非編集状態時にクリックするとF4をMultiRow側で発行している対策
                if (e.KeyCode == Keys.F4 && this.ActiveControl is GrapeCity.Win.MultiRow.DateTimePickerEditingControl)
                {
                    e.Handled = true; //falseだとフォーカスが入らない。trueだとフォーカスが入る。（フォーカス状態であれば、プルダウンを押されてもF4が発生しない）
                    return;
                    //制限事項として、日付セルにフォーカスがあると、F4を押すと編集状態になる。（他のセルはF4の処理が実行される）
                }


                var buttonName = "bt_func" + (e.KeyCode - Keys.F1 + 1);
                ControlUtility.ClickButton(this, buttonName);
                e.Handled = true;
            }


        }



        /// <summary>
        /// 前回値の値を保持
        /// </summary>
        /// <param name="previousControl"></param>
        internal void SetPreviousControlValue(object[] previousControl)
        {
            // コントロールのName,Text(Value)のセットで保持
            this.PreviousControlValue = new Dictionary<string, string>();

            foreach (object obj in previousControl)
            {
                ICustomControl ctrl = obj as ICustomControl;
                if (ctrl == null)
                {
                    continue;
                }

                string name = PropertyUtility.GetString(obj, "Name");
                if (!this.PreviousControlValue.ContainsKey(name))
                {
                    this.PreviousControlValue.Add(name, ctrl.GetResultText());
                }
            }
        }

        /// <summary>
        /// 登録チェックイベント処理
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public virtual void OnUserRegistCheck(object source, RegistCheckEventArgs e)
        {
            if (UserRegistCheck != null)
            {
                this.UserRegistCheck(source, e);
            }
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            //アンカー強制クリア
            if (e.Control is GcCustomMultiRow)
            {
                ((GcCustomMultiRow)e.Control).Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
            else if (e.Control is CustomDataGridView)
            {
                ((CustomDataGridView)e.Control).Anchor = AnchorStyles.Top | AnchorStyles.Left;
            }
        }


        protected override void OnShown(EventArgs e)
        {
            //UIガイド対応　グリッドの初期フォーカスセット
            foreach (Control c in ControlUtility.GetAllControlsEx(this))
            {
                var dgv = c as CustomDataGridView;

                if (dgv != null && dgv.CurrentCell == null && dgv.Rows.Count > 0)
                {
                    //不可視セル対応(不可視だと例外が出るため最初のセルを設定)
                    foreach (DataGridViewCell cell in dgv.Rows[0].Cells
                                                      .Cast<DataGridViewCell>()
                                                      .Where(x => x.Visible)
                                                      .OrderBy(x => x.OwningColumn.DisplayIndex))
                    {
                        dgv.CurrentCell = cell;
                        break;
                    }
                }

            }


            base.OnShown(e);
        }

        /// <summary>
        /// 保持する前回値をセットします
        /// </summary>
        /// <param name="value">前回値</param>
        /// <param name="control">コントロール</param>
        internal void SetPreviousValue(string value, ICustomControl control)
        {
            this.previousValue = value;
            this.previousControl = control;
        }

        /// <summary>
        /// 保持している前回値のコントロールを取得します
        /// </summary>
        /// <returns>保持している前回値のコントロール</returns>
        internal ICustomControl GetPreviousControl()
        {
            return this.previousControl;
        }

        /// <summary>
        /// 保持している前回値を取得します
        /// </summary>
        /// <returns>保持している前回値</returns>
        internal string GetPreviousValue()
        {
            return this.previousValue;
        }

        /// <summary>
        /// 自フォームから見て、一番親のフォームを見つけ閉じる
        /// </summary>
        public void CloseTopForm()
        {
            Control topForm = this;
            while (topForm.Parent != null)
            {
                topForm = topForm.Parent;
            }
            ((Form)topForm).Close();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

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
        }

        /// <summary>
        /// Clickイベントメソッドの上書き
        /// </summary>
        public void C_BeforeRegist(Control c)
        {
            c.Click -= this.BeforeRegistEvent;
            c.Click += this.BeforeRegistEvent;
        }

        /// <summary>
        /// 登録時の自動実行処理
        /// </summary>
        private void BeforeRegistEvent(object sender, EventArgs e)
        {
            var button = sender as CustomButton;

            this.ProcessKbn = button == null ? PROCESS_KBN.NONE : button.ProcessKbn;

            var autoCheckLogic = new AutoRegistCheckLogic(this.allControl);

            this.RegistErrorFlag = autoCheckLogic.BeforeRegistCheck();
        }

        /// <summary>
        /// Clickイベントメソッドの上書き
        /// </summary>
        public void C_MasterRegist(Control c)
        {
            c.Click += this.MasterRegistEvent;
        }

        /// <summary>
        /// 登録時の自動実行処理
        /// </summary>
        private void MasterRegistEvent(object sender, EventArgs e)
        {
            var button = sender as CustomButton;

            this.ProcessKbn = button == null ? PROCESS_KBN.NONE : button.ProcessKbn;

            var autoCheckLogic = new AutoRegistCheckLogic(this.allControl);

            this.RegistErrorFlag = autoCheckLogic.AutoMasterRegistCheck();
        }
    }
}
