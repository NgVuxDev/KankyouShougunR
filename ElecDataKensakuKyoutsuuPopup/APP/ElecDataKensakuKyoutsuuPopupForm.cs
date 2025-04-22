// $Id: ElecDataKensakuKyoutsuuPopupForm.cs 43381 2015-03-02 00:24:25Z nagata $
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using ElecDataKensakuKyoutsuuPopup.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;
using Shougun.Core.Message;
using r_framework.Logic;

namespace ElecDataKensakuKyoutsuuPopup.APP
{
    /// <summary>
    /// 検索共通ポップアップ画面
    /// </summary>
    public partial class ElecDataKensakuKyoutsuuPopupForm : SuperPopupForm
    {
        #region フィールド

        /// <summary>
        /// 共通ロジック
        /// </summary>
        public ElecDataKensakuKyoutsuuPopupLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public bool keyPressFlag = false;

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        private CustomRadioButton[] BOINRadioList;

        /// <summary>
        /// 画面に表示される母音(段)の配列
        /// </summary>
        public string[] BOINList = new string[] { "", "", "", "", "" };

        /// <summary>
        /// 検索用の母音(段)の配列
        /// </summary>
        public string[] BOINListFilter = new string[] { "", "", "", "", "" };

        #endregion

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ElecDataKensakuKyoutsuuPopupForm()
        {
            InitializeComponent();
            this.customDataGridView1.IsBrowsePurpose = true;
            this.customDataGridView1.IsReload = true;
            BOINRadioList = new CustomRadioButton[] { this.BOIN1, this.BOIN2, this.BOIN3, this.BOIN4, this.BOIN5 };
        }

        #endregion

        #region 画面コントロールイベント

        /// <summary>
        /// フォーカスアウト時処理
        /// </summary>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            logic = new ElecDataKensakuKyoutsuuPopupLogic(this);
            bool catchErr = false;
            if (!this.logic.CheckColumn(out catchErr) && !catchErr)
            {
                // 開発者向けメッセージを表示
                MessageBoxUtility.MessageBoxShowError("開発者情報：必要なカラムが足りません。頭文字検索等をするためPopupDataHeaderTitleは検索ポップアップのDENSHI_JIGYOUJOU_COLUMNSの値が含まれるようにお願いします。");
                return;
            }
            else if (catchErr)
            {
                return;
            }

            catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            var allControl = controlUtil.GetAllControls(this);
            foreach (Control c in allControl)
            {
                Control_Enter(c);
            }

            this.CONDITION_ITEM.Focus();
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
        /// 母音(段)の入力値をクリアする
        /// </summary>
        public void SetBOINValueClear()
        {
            //bool BOINVisibleFlag = false;
            for (int i = 0; i < 5; i++)
            {
                BOINRadioList[i].Text = string.Empty;
                BOINRadioList[i].Tag = " ";
                BOINList[i] = string.Empty;
                BOINListFilter[i] = string.Empty;

                //初期表示に戻す
                BOINRadioList[i].Text = (i + 1) + ".";
            }

            // 初期化
            this.FILTER_BOIN_VALUE.Text = string.Empty;
            //this.FILTER_BOIN_VALUE.Visible = BOINVisibleFlag;
            //this.label2.Visible = BOINVisibleFlag;
        }

        /// <summary>
        /// 検索条件の番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CONDITION_ITEM_Modified(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 検索条件の番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PARENT_CONDITION_ITEM_TextChanged(object sender, EventArgs e)
        {
            this.logic.ImeControlCondition();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CONDITION_VALUE_OnLeave(object sender, EventArgs e)
        {
            this.FILTER_SHIIN_VALUE.Text = string.Empty;
            this.FILTER_BOIN_VALUE.Text = string.Empty;
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void CONDITION_VALUE_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //IME入力中は何もしない
            if (this.imeStatus.IsConversion)
            {
                return;
            }

            //EnterはLeaveで動作するのでここでは不要。
            if (e.KeyCode == Keys.Down)
            {
                //処理を統一
                CONDITION_VALUE_OnLeave(sender, EventArgs.Empty);
            }
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

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            var activ = ActiveControl as SuperPopupForm;
            base.OnKeyPress(e);
        }

        #endregion

        #region ファンクションイベント

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.customDataGridView1.Rows.Count > 0)
                {
                    bool catchErr = this.logic.ElementDecision();
                    if (catchErr)
                    {
                        e.Handled = true;
                        return;
                    }
                    this.DialogResult = DialogResult.OK;
                }
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
            //最初の可視セルを選択する
            this.customDataGridView1.CurrentCell = this.customDataGridView1.Rows[e.RowIndex].Cells.Cast<DataGridViewCell>().OrderBy(x => x.OwningColumn.DisplayIndex).First(x => x.Visible == true);

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
            if (this.CONDITION_ITEM.Enabled)
            {
                check = false;
                switch (this.WindowId)
                {
                    case WINDOW_ID.M_DENSHI_JIGYOUSHA:
                        this.CONDITION_ITEM.Text = "2";
                        break;

                    default:
                        if (this.CONDITION3.Enabled)
                        {
                            this.CONDITION_ITEM.Text = "3";
                        }
                        break;
                }
            }
            if (this.CONDITION_VALUE.Enabled)
            {
                check = false;
                this.CONDITION_VALUE.Text = string.Empty;
            }
            this.FILTER_SHIIN_VALUE.Text = string.Empty;
            this.FILTER_BOIN_VALUE.Text = string.Empty;

            this.customSortHeader1.ClearCustomSortSetting(); //ソート設定もクリアする
            //this.customDataGridView1.DataSource = null; //データはクリアせず再検索されるまでは残す。

            DataTable dt = (DataTable)this.customDataGridView1.DataSource;
            if (dt != null)
            {
                dt.Rows.Clear();
                //this.customDataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            }

            if (check)
            {
                this.FILTER_SHIIN_VALUE.Focus();
            }
            else
            {
                this.CONDITION_ITEM.Focus();
            }
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
        public virtual void SortSetting(object sender, EventArgs e)
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
            base.ReturnParams = null;
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        #endregion
    }
}