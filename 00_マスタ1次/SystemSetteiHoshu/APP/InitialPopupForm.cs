using System;
using System.Windows.Forms;
using GamenSeigyoHoshu;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
using r_framework.Configuration;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using SystemSetteiHoshu.Logic;

namespace SystemSetteiHoshu.APP
{
    public partial class InitialPopupForm : SuperPopupForm
    {
        public InitialPopupFormLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public ControlUtility controlUtil = new ControlUtility();

        public string title = string.Empty;
        /// <summary>DB接続先コンボボックスの選択中のインデックス</summary>
        private int selectedDatabaseComboBoxIndex;
        /// <summary>Selected inxs subapp database</summary>
        private int selectedInxsSubAppDatabaseComboBoxIndex;

        /// <summary>DB接続先コンボボックスの選択中のインデックス</summary>
        private int selectedDatabaseLOGComboBoxIndex;
        /// <summary>Selected inxs subapp database</summary>

        public InitialPopupForm()
        {
            InitializeComponent();

            //PhuocLoc 2022/01/04 #158897, #158898 -Start
            if (!AppConfig.AppOptions.IsWANSign())
            {
                this.tabControl1.TabPages.Remove(this.tabPage7);
            }
            //PhuocLoc 2022/01/04 #158897, #158898 -End

            if (!AppConfig.AppOptions.IsRakurakuMeisai())
            {
                this.tabControl1.TabPages.Remove(this.tabPage9);
            }
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            logic = new InitialPopupFormLogic(this);

            var allControl = controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }

            this.logic.WindowInit();
        }

        /// <summary>
        /// 画面制御
        /// </summary>
        internal virtual void bt_func1_Click(object sender, System.EventArgs e)
        {
            // 画面制御入力画面を呼び出す
            GamenSeigyoHoshuForm gamenSeigyoform = new GamenSeigyoHoshuForm();
            MasterBaseForm mForm = new MasterBaseForm(gamenSeigyoform, WINDOW_TYPE.NEW_WINDOW_FLAG, true);
            mForm.ShowDialog();
        }

        /// <summary>
        /// 反映処理
        /// </summary>
        internal virtual void Reflection(object sender, System.EventArgs e)
        {
            if (!this.logic.ElementDecision())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        internal virtual void FormClose(object sender, System.EventArgs e)
        {
            base.ReturnParams = null;
            this.Close();
            this.DialogResult = DialogResult.Cancel;
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
        protected void c_GotFocus(object sender, EventArgs e)
        {
            var activ = ActiveControl as SuperPopupForm;

            if (activ == null)
            {
                if (ActiveControl != null)
                {
                    this.lb_hint.Text = (string)ActiveControl.Tag;
                }
            }
        }

        private void InitialPopupForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    ControlUtility.ClickButton(this, "bt_func1");
                    break;
                case Keys.F2:
                    ControlUtility.ClickButton(this, "bt_func2");
                    break;
                case Keys.F3:
                    ControlUtility.ClickButton(this, "bt_func3");
                    break;
                case Keys.F4:
                    ControlUtility.ClickButton(this, "bt_func4");
                    break;
                case Keys.F5:
                    ControlUtility.ClickButton(this, "bt_func5");
                    break;
                case Keys.F6:
                    ControlUtility.ClickButton(this, "bt_func6");
                    break;
                case Keys.F7:
                    ControlUtility.ClickButton(this, "bt_func7");
                    break;
                case Keys.F8:
                    ControlUtility.ClickButton(this, "bt_func8");
                    break;
                case Keys.F9:
                    ControlUtility.ClickButton(this, "bt_func9");
                    break;
                case Keys.F10:
                    ControlUtility.ClickButton(this, "bt_func10");
                    break;
                case Keys.F11:
                    ControlUtility.ClickButton(this, "bt_func11");
                    break;
                case Keys.F12:
                    ControlUtility.ClickButton(this, "bt_func12");
                    break;
            }
        }

        private void UKEIRESHUKA_GAMEN_SIZE_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(this.UKEIRESHUKA_GAMEN_SIZE.Text) && "1".Equals(this.UKEIRESHUKA_GAMEN_SIZE.Text))
            {
                this.DENPYOU_HAKOU_HYOUJI.ReadOnly = false;
                this.HYOUJI_YES.Enabled = true;
                this.HYOUJI_NO.Enabled = true;
                //160029 S
                this.BARCODO_SHINKAKU.ReadOnly = false;
                this.rb_BARCODO_CODE39.Enabled = true;
                this.rb_BARCODO_QR.Enabled = true;
                //160029 E
            }
            else
            {
                this.DENPYOU_HAKOU_HYOUJI.ReadOnly = true;
                this.HYOUJI_YES.Enabled = false;
                this.HYOUJI_NO.Enabled = false;
                //160029 S
                this.BARCODO_SHINKAKU.ReadOnly = true;
                this.rb_BARCODO_CODE39.Enabled = false;
                this.rb_BARCODO_QR.Enabled = false;
                //160029 E
            }
        }

        /// <summary>
        /// 接続先データコンボボックスフォーカスイン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DatabaseFileConnection_GotFocus(object sender, EventArgs e)
        {
            // 現在選択インデックスを保存する
            this.selectedDatabaseComboBoxIndex = this.DB_FILE_CONNECT.SelectedIndex;
        }

        /// <summary>
        /// 接続先データコンボボックスフォーカスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DatabaseFileConnection_Leave(object sender, EventArgs e)
        {
            this.CheckSelectedDatabaseConnection();
        }

        /// <summary>
        /// 接続先データコンボボックス選択値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DatabaseFileConnection_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.CheckSelectedDatabaseConnection();
        }

        /// <summary>
        /// 接続先データコンボボックス選択値のチェック
        /// </summary>
        private void CheckSelectedDatabaseConnection()
        {
            // 変更されなかった場合はチェックしない
            if (this.DB_FILE_CONNECT.SelectedIndex == this.selectedDatabaseComboBoxIndex)
            {
                return;
            }

            // 選択インデックス登録
            this.selectedDatabaseComboBoxIndex = this.DB_FILE_CONNECT.SelectedIndex;

            // カーソルを待機カーソルに変更
            Cursor.Current = Cursors.WaitCursor;

            this.logic.CheckSelectedItem();

            // カーソルを元に戻す
            Cursor.Current = Cursors.Default;
        }

        #region InxsSubapplication Database connection setting

        /// <summary>
        /// 接続先データコンボボックスフォーカスイン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DatabaseInxsSubappConnection_GotFocus(object sender, EventArgs e)
        {
            // 現在選択インデックスを保存する
            this.selectedInxsSubAppDatabaseComboBoxIndex = this.DB_SUBAPP_CONNECT.SelectedIndex;
        }

        /// <summary>
        /// 接続先データコンボボックスフォーカスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DatabaseInxsSubappConnection_Leave(object sender, EventArgs e)
        {
            this.CheckSelectedDatabaseInxsSubappConnection();
        }

        /// <summary>
        /// 接続先データコンボボックス選択値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DatabaseInxsSubappConnection_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.CheckSelectedDatabaseInxsSubappConnection();
        }

        /// <summary>
        /// 接続先データコンボボックス選択値のチェック
        /// </summary>
        private void CheckSelectedDatabaseInxsSubappConnection()
        {
            // 変更されなかった場合はチェックしない
            if (this.DB_SUBAPP_CONNECT.SelectedIndex == this.selectedInxsSubAppDatabaseComboBoxIndex)
            {
                return;
            }

            // 選択インデックス登録
            this.selectedInxsSubAppDatabaseComboBoxIndex = this.DB_SUBAPP_CONNECT.SelectedIndex;

            // カーソルを待機カーソルに変更
            Cursor.Current = Cursors.WaitCursor;

            this.logic.CheckSelectedConnection();

            // カーソルを元に戻す
            Cursor.Current = Cursors.Default;
        }

        #endregion
        //QN_QUAN add 20211229 #158952 S
        /// <summary>
        /// 接続先データコンボボックスフォーカスイン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DatabaseLOGConnection_GotFocus(object sender, EventArgs e)
        {
            // 現在選択インデックスを保存する
            this.selectedDatabaseLOGComboBoxIndex = this.DB_LOG_CONNECT.SelectedIndex;
        }

        /// <summary>
        /// 接続先データコンボボックスフォーカスアウト
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DatabaseLOGConnection_Leave(object sender, EventArgs e)
        {
            this.CheckSelectedDatabaseLOGConnection();
        }

        /// <summary>
        /// 接続先データコンボボックス選択値変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DatabaseLOGConnection_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.CheckSelectedDatabaseLOGConnection();
        }

        /// <summary>
        /// 接続先データコンボボックス選択値のチェック
        /// </summary>
        private void CheckSelectedDatabaseLOGConnection()
        {
            // 変更されなかった場合はチェックしない
            if (this.DB_LOG_CONNECT.SelectedIndex == this.selectedDatabaseLOGComboBoxIndex)
            {
                return;
            }

            // 選択インデックス登録
            this.selectedDatabaseLOGComboBoxIndex = this.DB_LOG_CONNECT.SelectedIndex;

            // カーソルを待機カーソルに変更
            Cursor.Current = Cursors.WaitCursor;

            this.logic.CheckSelectedItemLOG();

            // カーソルを元に戻す
            Cursor.Current = Cursors.Default;
        }
        //QN_QUAN add 20211229 #158952 E
    }
}
