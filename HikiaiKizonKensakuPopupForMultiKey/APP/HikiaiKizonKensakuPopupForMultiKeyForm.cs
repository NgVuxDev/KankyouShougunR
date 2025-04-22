// $Id: KensakuKyoutsuuPopupForMultiKeyForm.cs 2105 2013-09-19 06:22:30Z sanbongi $
using System;
using System.Data;
using System.Windows.Forms;
using HikiaiKizonKensakuPopupForMultiKey.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Logic;
using r_framework.Utility;

namespace HikiaiKizonKensakuPopupForMultiKey.APP
{
    /// <summary>
    /// 複数キー用検索共通ポップアップ画面
    /// </summary>
    public partial class HikiaiKizonKensakuPopupForMultiKeyForm : SuperPopupForm
    {

        #region フィールド
        /// <summary>
        /// 共通ロジック
        /// </summary>
        public HikiaiKizonKensakuPopupForMultiKeyLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 指定のキーが押されたかを記します
        /// </summary>
        public bool keyPressFlag = false;

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        private CustomRadioButton[] shiinRadioList;

        private string conditionRengeForMessage = string.Empty;

        /// <summary>
        /// 画面に表示される子音の配列
        /// </summary>
        public string[] shiinList = new string[] { "", "", "", "", "" };

        /// <summary>Layout of Genba Hikiai</summary>
        public CheckBox HYOUJI_JOUKEN_TEKIYOUGAI { get; set; }
        public CheckBox HYOUJI_JOUKEN_DELETED { get; set; }
        public Label label5 { get; set; }
        public CheckBox HYOUJI_JOUKEN_TEKIYOU { get; set; }
        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public HikiaiKizonKensakuPopupForMultiKeyForm()
        {
            InitializeComponent();
            this.customDataGridView1.IsReload = true;
            shiinRadioList = new CustomRadioButton[] { this.SHIIN1, this.SHIIN2, this.SHIIN3, this.SHIIN4, this.SHIIN5 };
            switch (this.WindowId)
            {
                case WINDOW_ID.M_HINMEI:
                    conditionRengeForMessage = "1～4";
                    break;

                default:
                    conditionRengeForMessage = "1～7";
                    break;
            }
        }
        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// フォーカスアウト時処理
        /// </summary>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            logic = new HikiaiKizonKensakuPopupForMultiKeyLogic(this);
            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            var allControl = controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }

            this.PARENT_CONDITION_VALUE.Focus();
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

        void FILTER_BOIN_VALUE_Changed(object sender, EventArgs e)
        {
            if (this.FILTER_BOIN_VALUE == null || string.IsNullOrEmpty(this.FILTER_BOIN_VALUE.Text))
            {
                return;
            }

            string[] strList = new string[] { "", "", "", "", "" };

            switch (this.FILTER_BOIN_VALUE.Text)
            {
                case "1":
                    strList = Constans.A_GYO_STR;
                    break;

                case "2":
                    strList = Constans.KA_GYO_STR;
                    break;

                case "3":
                    strList = Constans.SA_GYO_STR;
                    break;

                case "4":
                    strList = Constans.TA_GYO_STR;
                    break;

                case "5":
                    strList = Constans.NA_GYO_STR;
                    break;

                case "6":
                    strList = Constans.HA_GYO_STR;
                    break;

                case "7":
                    strList = Constans.MA_GYO_STR;
                    break;

                case "8":
                    strList = Constans.YA_GYO_STR;
                    break;

                case "9":
                    strList = Constans.RA_GYO_STR;
                    break;

                case "10":
                    strList = Constans.WA_GYO_STR;
                    break;

                default:
                    break;

            }

            bool shiinVisibleFlag = false;
            for (int i = 0; i < 5; i++)
            {
                if (string.IsNullOrEmpty(strList[i]))
                {
                    shiinRadioList[i].Text = string.Empty;
                    shiinRadioList[i].Tag = " ";
                    shiinList[i] = string.Empty;
                }
                else
                {
                    // ひとつでもあれば子音絞込みを有効に
                    shiinVisibleFlag = true;
                    shiinRadioList[i].Text = (i + 1).ToString() + ".　" + strList[i];
                    shiinRadioList[i].Tag = string.Format("{0}が対象の場合チェックを付けてください", strList[i]);
                    shiinList[i] = strList[i];
                }

                // 空のは非表示にする
                shiinRadioList[i].Visible = (!string.IsNullOrEmpty(shiinRadioList[i].Text));
            }

            // 初期化
            this.FILTER_SHIIN_VALUE.Text = string.Empty;
            this.FILTER_SHIIN_VALUE.Visible = shiinVisibleFlag;
            this.label2.Visible = shiinVisibleFlag;

            this.InitialSort(sender, e);

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
                    if (ActiveControl is DataGridView)
                    {
                        if (this.customDataGridView1.Rows.Count <= 0)
                        {
                            if (this.keyPressFlag)
                            {
                                var ctrl = this.GetNextControl(ActiveControl, false);

                                this.SelectNextControl(ctrl, false, true, true, true);
                            }
                            else
                            {
                                this.SelectNextControl(this, true, true, true, true);
                            }
                        }
                    }
                    this.lb_hint.Text = (string)ActiveControl.Tag;
                }
            }
        }

        /// <summary>
        /// OnKeyDownイベントのオーバーライド
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.keyPressFlag = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                var activ = ActiveControl as SuperPopupForm;

                string a = Convert.ToString(e.KeyData);
                if (e.KeyData.Equals(Keys.LButton | Keys.Back | Keys.Shift)
                  || e.KeyData.Equals(Keys.LButton | Keys.MButton | Keys.Back | Keys.Shift))
                {
                    this.keyPressFlag = true;
                }
            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// 検索条件の番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PARENT_CONDITION_ITEM_Modified(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.PARENT_CONDITION_ITEM.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E042", conditionRengeForMessage);
                this.PARENT_CONDITION_ITEM.Focus();
                return;
            }
        }

        /// <summary>
        /// 検索条件の番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PARENT_CONDITION_ITEM_TextChanged(object sender, EventArgs e)
        {
            this.logic.ImeControlParentCondition();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PARENT_CONDITION_VALUE_OnLeave(object sender, EventArgs e)
        {
            //this.FILTER_BOIN_VALUE.Text = string.Empty;
            //this.FILTER_SHIIN_VALUE.Text = string.Empty;
            //if (this.logic.Search() == 0)
            //{
            //    this.PARENT_CONDITION_VALUE.Focus();
            //}
            //else
            //{
            //    this.customDataGridView1.Focus();
            //}
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void PARENT_CONDITION_VALUE_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                if (!string.IsNullOrEmpty(this.PARENT_CONDITION_VALUE.Text))
                {
                    int count = this.logic.Search();
                    if (count == 0)
                    {
                        this.PARENT_CONDITION_VALUE.Focus();
                    }
                    else if (count > 0)
                    {
                        this.customDataGridView1.Focus();
                    }
                }
            }
        }

        /// <summary>
        /// 検索条件の番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CHILD_CONDITION_ITEM_Modified(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.CHILD_CONDITION_ITEM.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E042", conditionRengeForMessage);
                this.PARENT_CONDITION_ITEM.Focus();
                return;
            }
        }

        /// <summary>
        /// 検索条件の番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CHILD_CONDITION_ITEM_TextChanged(object sender, EventArgs e)
        {
            this.logic.ImeControlChildCondition();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CHILD_CONDITION_VALUE_OnLeave(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// ﾌﾘｶﾞﾅ頭文字(母音)変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FILTER_BOIN_VALUE_Modified(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.FILTER_BOIN_VALUE.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E042", "1～12");
                this.FILTER_BOIN_VALUE.Focus();
                return;
            }

            this.InitialSort(sender, e);

            if (this.FILTER_BOIN_VALUE.Text.Equals("11") || this.FILTER_BOIN_VALUE.Text.Equals("12"))
            {
                if (this.customDataGridView1.RowCount < 1)
                {
                    this.FILTER_BOIN_VALUE.Focus();
                }
                else
                {
                    this.customDataGridView1.Focus();
                }
            }
            else
            {
                if (this.customDataGridView1.RowCount < 1)
                {
                    this.FILTER_BOIN_VALUE.Focus();
                }
                else
                {
                    this.FILTER_SHIIN_VALUE.Focus();
                }
            }

        }

        /// <summary>
        /// ﾌﾘｶﾞﾅ頭文字(子音)更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FILTER_SHIIN_VALUE_Modified(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.FILTER_SHIIN_VALUE.Text))
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E042", "1～5");
                this.FILTER_BOIN_VALUE.Focus();
                return;
            }

            this.InitialSort(sender, e);

            if (this.customDataGridView1.RowCount < 1)
            {
                this.FILTER_SHIIN_VALUE.Focus();
            }
            else
            {
                this.customDataGridView1.Focus();
            }
        }

        /// <summary>
        /// 頭文字ソート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitialSort(object sender, EventArgs e)
        {
            this.logic.InvokeInitialSort();
        }

        /// <summary>
        /// キー押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KensakuKyoutsuuPopupForMultiKeyForm_KeyUp(object sender, KeyEventArgs e)
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
        #endregion

        #region ファンクションイベント
        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DetailKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            this.KensakuKyoutsuuPopupForMultiKeyForm_KeyUp(sender, e);
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool catchErr = this.logic.ElementDecision();
                if (catchErr)
                {
                    return;
                }
                this.DialogResult = DialogResult.OK;
                e.Handled = true;
            }
        }

        /// <summary>
        /// ダブルクリック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            if (this.PopupMultiSelect)
            {
                return;
            }
            bool catchErr = this.logic.ElementDecision();
            if (catchErr)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Clear(object sender, EventArgs e)
        {
            bool check = true;
            if (this.PARENT_CONDITION_ITEM.Enabled)
            {
                check = false;
                this.PARENT_CONDITION_ITEM.Text = "3";
            }

            if (this.PARENT_CONDITION_VALUE.Enabled)
            {
                this.PARENT_CONDITION_VALUE.Text = string.Empty;
            }

            this.CHILD_CONDITION_ITEM.Text = "3";
            this.CHILD_CONDITION_VALUE.Text = string.Empty;
            this.FILTER_BOIN_VALUE.Text = string.Empty;
            this.FILTER_SHIIN_VALUE.Text = string.Empty;

            this.PARENT_CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.PARENT_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.CHILD_CONDITION_ITEM.ImeMode = ImeMode.Alpha;
            this.CHILD_CONDITION_VALUE.ImeMode = ImeMode.Katakana;
            this.FILTER_BOIN_VALUE.ImeMode = ImeMode.Alpha;
            this.FILTER_SHIIN_VALUE.ImeMode = ImeMode.Alpha;
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customSortHeader1.ClearCustomSortSetting(); //ソート設定もクリアする
            this.rdlKizon.Checked = true;

            DataTable dt = (DataTable)this.customDataGridView1.DataSource;

            if (dt != null && 0 < dt.Rows.Count)
            {
                dt.Rows.Clear();
            }

            if (check)
            {
                this.CHILD_CONDITION_VALUE.Focus();
            }
            else
            {
                this.PARENT_CONDITION_VALUE.Focus();
            }
            this.logic.SettingProperties(true);
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            if (this.logic.Search() > 0)
            {
                this.customDataGridView1.Focus();
            }
        }

        /// <summary>
        /// 確定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Selected(object sender, EventArgs e)
        {
            if (this.customDataGridView1 != null && 0 < this.customDataGridView1.RowCount)
            {
                bool catchErr = this.logic.ElementDecision();
                if (catchErr)
                {
                    return;
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// 並替移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MoveToSort(object sender, EventArgs e)
        {
            this.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Close(object sender, EventArgs e)
        {
            this.logic.SettingProperties(false);
            base.ReturnParams = null;
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

    }
}
