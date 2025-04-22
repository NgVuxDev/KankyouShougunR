// $Id: NyuukinTorikomiTorihikiKensakuPopupForm.cs 43385 2015-03-02 00:45:43Z sanbongi $
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using NyuukinTorikomiTorihikiKensakuPopup.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;

namespace NyuukinTorikomiTorihikiKensakuPopup.APP
{
    /// <summary>
    /// 検索共通ポップアップ画面
    /// </summary>
    public partial class NyuukinTorikomiTorihikiKensakuForm : SuperPopupForm
    {
        #region フィールド
        /// <summary>
        /// 共通ロジック
        /// </summary>
        public NyuukinTorikomiTorihikiKensakuLogic logic;

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
        public NyuukinTorikomiTorihikiKensakuForm()
        {
            InitializeComponent();
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

            logic = new NyuukinTorikomiTorihikiKensakuLogic(this);
            this.logic.WindowInit();

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
        /// 子音(行)の入力値が変更された場合の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void FILTER_SHIIN_VALUE_Changed(object sender, EventArgs e)
        {
            if (this.FILTER_SHIIN_VALUE == null || string.IsNullOrEmpty(this.FILTER_SHIIN_VALUE.Text))
            {
                this.SetBOINValueClear();
                //this.plBOIN.Visible = false;
                return;
            }

            string[] strList = new string[] { "", "", "", "", "" };
            string[] strListFilter = new string[] { "", "", "", "", "" };

            switch (this.FILTER_SHIIN_VALUE.Text)
            {
                case "1":
                    strList = Constans.A_GYO_STR;
                    strListFilter = Constans.Agyou_SHIIN;
                    break;

                case "2":
                    strList = Constans.KA_GYO_STR;
                    strListFilter = Constans.KAgyou_SHIIN;
                    break;

                case "3":
                    strList = Constans.SA_GYO_STR;
                    strListFilter = Constans.SAgyou_SHIIN;
                    break;

                case "4":
                    strList = Constans.TA_GYO_STR;
                    strListFilter = Constans.TAgyou_SHIIN;
                    break;

                case "5":
                    strList = Constans.NA_GYO_STR;
                    strListFilter = Constans.NAgyou_SHIIN;
                    break;

                case "6":
                    strList = Constans.HA_GYO_STR;
                    strListFilter = Constans.HAgyou_SHIIN;
                    break;

                case "7":
                    strList = Constans.MA_GYO_STR;
                    strListFilter = Constans.MAgyou_SHIIN;
                    break;

                case "8":
                    strList = Constans.YA_GYO_STR;
                    strListFilter = Constans.YAgyou_SHIIN;
                    break;

                case "9":
                    strList = Constans.RA_GYO_STR;
                    strListFilter = Constans.RAgyou_SHIIN;
                    break;

                case "10":
                    strList = Constans.WA_GYO_STR;
                    strListFilter = Constans.WAgyou_SHIIN;
                    break;

                default:
                    break;

            }

            bool BOINVisibleFlag = false;
            for (int i = 0; i < 5; i++)
            {
                if (string.IsNullOrEmpty(strList[i]))
                {
                    BOINRadioList[i].Text = string.Empty;
                    BOINRadioList[i].Tag = " ";
                    BOINList[i] = string.Empty;
                    BOINListFilter[i] = string.Empty;
                }
                else
                {
                    // ひとつでもあれば母音(段)絞込みを有効に
                    BOINVisibleFlag = true;
                    BOINRadioList[i].Text = (i + 1).ToString() + ". " + strList[i];
                    BOINRadioList[i].Tag = string.Format("{0}が対象の場合チェックを付けてください", strList[i]);
                    BOINList[i] = strList[i];
                    BOINListFilter[i] = strListFilter[i];
                }

                // 空のは非表示にする
                BOINRadioList[i].Visible = (!string.IsNullOrEmpty(BOINRadioList[i].Text));
            }

            // 初期化
            this.FILTER_BOIN_VALUE.Text = string.Empty;
            this.FILTER_BOIN_VALUE.Visible = BOINVisibleFlag;
            //this.plBOIN.Visible = BOINVisibleFlag;
            //this.label2.Visible = BOINVisibleFlag;

            this.InitialSort(sender, e);
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
                BOINRadioList[i].Text = (i+1) + ".";
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
        /// ﾌﾘｶﾞﾅ頭文字(子音(行))変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FILTER_SHIIN_VALUE_Modified(object sender, EventArgs e)
        {
            this.InitialSort(sender, e);

            if (this.FILTER_SHIIN_VALUE.Text.Equals("11") || this.FILTER_SHIIN_VALUE.Text.Equals("12"))
            {
                if (this.customDataGridView1.RowCount < 1)
                {
                    this.FILTER_SHIIN_VALUE.Focus();
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
                    this.FILTER_SHIIN_VALUE.Focus();
                }
                else
                {
                    this.FILTER_BOIN_VALUE.Focus();
                }
            }
        }

        /// <summary>
        /// ﾌﾘｶﾞﾅ頭文字(母音(段))更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FILTER_BOIN_VALUE_Modified(object sender, EventArgs e)
        {
            this.InitialSort(sender, e);
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
                    this.logic.ElementDecision();
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

            this.logic.ElementDecision();
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
                if (this.WindowId.Equals(WINDOW_ID.M_DENSHI_JIGYOUSHA))
                {
                    this.CONDITION_ITEM.Text = "2";
                }
                else
                {
                    this.CONDITION_ITEM.Text = "3";
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

            if (dt != null && 0 < dt.Rows.Count)
            {
                dt.Rows.Clear();
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
            if (this.logic.CheckInput())
            {
                if (this.logic.Search() != 0)
                {
                    this.customDataGridView1.Focus();
                }
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
                this.logic.ElementDecision();
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
