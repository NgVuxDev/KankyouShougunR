// $Id: KensakuKyoutsuuPopupForMultiKeyForm.cs 43431 2015-03-02 05:12:31Z sanbongi $
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using KensakuKyoutsuuPopupForMultiKey.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;
using r_framework.Logic;

using System.ComponentModel;

namespace KensakuKyoutsuuPopupForMultiKey.APP
{
    /// <summary>
    /// 複数キー用検索共通ポップアップ画面
    /// </summary>
    public partial class KensakuKyoutsuuPopupForMultiKeyForm : SuperPopupForm
    {

        #region フィールド
        /// <summary>
        /// 共通ロジック
        /// </summary>
        public KensakuKyoutsuuPopupForMultiKeyLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public bool keyPressFlag = false;
        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        private CustomRadioButton[] BOINRadioList;

        private string conditionRengeForMessage = string.Empty;

        /// <summary>
        /// 画面に表示される母音の配列
        /// </summary>
        public string[] BOINList = new string[] { "", "", "", "", "" };

        //母音の濁点等対応
        /// <summary>
        /// 母音検索用のフィルタ文字列
        /// </summary>
        public string[] BOINListFilter = new string[] { "", "", "", "", "" };

        #region コントロール
        /// <summary>検索条件：取引先</summary>
        public Label LABEL_TORIHIKISAKI { get; set; }
        public CustomAlphaNumTextBox TORIHIKISAKI_CD { get; set; }
        public CustomTextBox TORIHIKISAKI_NAME { get; set; }

        /// <summary>検索条件：業者</summary>
        public Label LABEL_GYOUSHA { get; set; }
        public CustomAlphaNumTextBox GYOUSHA_CD { get; set; }
        public CustomTextBox GYOUSHA_NAME { get; set; }

        /// <summary>検索条件：現場</summary>
        public Label LABEL_GENBA { get; set; }
        public CustomAlphaNumTextBox GENBA_CD { get; set; }
        public CustomTextBox GENBA_NAME { get; set; }

        /// <summary>検索条件：運搬業者</summary>
        public Label LABEL_UNPAN_GYOUSHA { get; set; }
        public CustomAlphaNumTextBox UNPAN_GYOUSHA_CD { get; set; }
        public CustomTextBox UNPAN_GYOUSHA_NAME { get; set; }

        /// <summary>検索条件：荷降業者</summary>
        public Label LABEL_NIOROSHI_GYOUSHA { get; set; }
        public CustomAlphaNumTextBox NIOROSHI_GYOUSHA_CD { get; set; }
        public CustomTextBox NIOROSHI_GYOUSHA_NAME { get; set; }

        /// <summary>検索条件：荷降現場</summary>
        public Label LABEL_NIOROSHI_GENBA { get; set; }
        public CustomAlphaNumTextBox NIOROSHI_GENBA_CD { get; set; }
        public CustomTextBox NIOROSHI_GENBA_NAME { get; set; }

        /// <summary>検索条件：種類検索条件</summary>
        /// 20250324
        public CustomComboBox SHURUI_KENSAKU_JOKEN_CBB { get; set; }

        //20250316
        public CustomComboBox SHURUI_KENSAKU_JOKEN_CBB1 { get; set; }

        /// <summary>検索条件：伝票区分設定</summary>
        public Label LABEL_DENPYOU_KBN { get; set; }
        public CustomNumericTextBox2 DENPYOU_KBN { get; set; }
        public CustomRadioButton DENPYOU_KBN_1 { get; set; }
        public CustomRadioButton DENPYOU_KBN_2 { get; set; }
        public CustomRadioButton DENPYOU_KBN_3 { get; set; }
        public Panel PANEL_DENPYOU_KBN { get; set; }

        /// <summary>検索条件: Genba Ichiran</summary>
        /// NvNhat #160897
        public CheckBox HYOUJI_JOUKEN_TEKIYOUGAI { get; set; }
        public CheckBox HYOUJI_JOUKEN_DELETED { get; set; }
        public Label label4 { get; set; }
        public CheckBox HYOUJI_JOUKEN_TEKIYOU { get; set; }
        /// <summary>検索条件：伝種区分設定</summary>
        internal string DENSHU_KBN_CD { get; set; }
        internal DateTime DENPYOU_DATE { get; set; }

        internal bool isKobetsuHinmeiTankSearchFlg { get; set; }

        #endregion

        #endregion

        #region 初期化処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public KensakuKyoutsuuPopupForMultiKeyForm()
        {
            InitializeComponent();
            this.customDataGridView1.IsBrowsePurpose = true;
            this.customDataGridView1.IsReload = true;
     
            BOINRadioList = new CustomRadioButton[] { this.BOIN1, this.BOIN2, this.BOIN3, this.BOIN4, this.BOIN5 };

            //todo:ポップアップ対象追加時修正箇所
            switch (this.WindowId)
            {
                case WINDOW_ID.M_HINMEI:
                    conditionRengeForMessage = "1～4";
                    break;

                case WINDOW_ID.M_DENSHI_JIGYOUJOU:
                    conditionRengeForMessage = "1、2、4～7";
                    break;

                default:
                    conditionRengeForMessage = "1～7";
                    break;
            }
        }
        #endregion

        #region 画面コントロールイベント
        /// <summary>
        /// 画面起動時処理
        /// </summary>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            logic = new KensakuKyoutsuuPopupForMultiKeyLogic(this);
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
            if (this.WindowId.Equals(WINDOW_ID.M_HINMEI_SEARCH))
            {
                // ラベル名称の初期値・・・システム設定ーマスター品名から抽出条件を取得しセット
                if (this.logic.sysInfoEntity.HINMEI_SEARCH_CHUSYUTSU_JOKEN.Value.Equals(2))
                {
                    this.bt_func1.Text = "[F1]　　個別単価";
                }
                this.DENPYOU_KBN.Text = this.logic.sysInfoEntity.HINMEI_SEARCH_DENPYOU_KBN.Value.ToString();
            }
            else
            {
                this.bt_func1.Text = string.Empty;
                this.bt_func1.Enabled = false;
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
        /// 母音の入力値が変更された場合の処理
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
                    // ひとつでもあれば母音絞込みを有効に
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
            this.plBOIN.Visible = BOINVisibleFlag;
            this.label2.Visible = BOINVisibleFlag;

            this.InitialSort(sender, e);

        }

        /// <summary>
        /// 母音の入力値をクリアする
        /// </summary>
        public bool SetBOINValueClear()
        {
            try
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
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetBOINValueClear", ex);
                this.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
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


        /// <summary>
        /// 検索条件の番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void PARENT_CONDITION_ITEM_Modified(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.PARENT_CONDITION_ITEM.Text))
            //{
            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //    msgLogic.MessageBoxShow("E042", conditionRengeForMessage);
            //    this.PARENT_CONDITION_ITEM.Focus();
            //    return;
            //}
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
        /// 検索条件の番号更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CHILD_CONDITION_ITEM_Modified(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.CHILD_CONDITION_ITEM.Text))
            //{
            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //    msgLogic.MessageBoxShow("E042", conditionRengeForMessage);
            //    this.PARENT_CONDITION_ITEM.Focus();
            //    return;
            //}
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
        protected void PARENT_CONDITION_VALUE_OnLeave(object sender, EventArgs e)
        {
            return;

            //PT721
            //if (this.logic.Search() == 0)
            //{
            //    //Chiledの条件で絶対0件になる条件が入っていると詰むのでNG 何もせず次コントロールへ
            //    //this.PARENT_CONDITION_VALUE.Focus(); 

            //}
            //else
            //{
            //    this.customDataGridView1.Focus();
            //}
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void CHILD_CONDITION_VALUE_OnLeave(object sender, EventArgs e)
        {
            //PARENTと同様に自動検索(PARENTと共通化）
            PARENT_CONDITION_VALUE_OnLeave(sender, e);
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void PARENT_CONDITION_VALUE_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
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
                PARENT_CONDITION_VALUE_OnLeave(sender, EventArgs.Empty);
            }
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void CHILD_CONDITION_VALUE_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
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
                CHILD_CONDITION_VALUE_OnLeave(sender, EventArgs.Empty);
            }
        }

        /// <summary>
        /// ﾌﾘｶﾞﾅ頭文字(母音)変更後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FILTER_SHIIN_VALUE_Modified(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.FILTER_SHIIN_VALUE.Text))
            //{
            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //    msgLogic.MessageBoxShow("E042", "1～12");
            //    this.FILTER_SHIIN_VALUE.Focus();
            //    return;
            //}

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
        /// ﾌﾘｶﾞﾅ頭文字(母音)更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void FILTER_BOIN_VALUE_Modified(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(this.FILTER_BOIN_VALUE.Text))
            //{
            //    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //    msgLogic.MessageBoxShow("E042", "1～5");
            //    this.FILTER_SHIIN_VALUE.Focus();
            //    return;
            //}

            this.InitialSort(sender, e);

            if (this.customDataGridView1.RowCount < 1)
            {
                this.FILTER_BOIN_VALUE.Focus();
            }
            else
            {
                this.customDataGridView1.Focus();
            }
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
            if (this.PopupMultiSelect)
            {
                return;
            }
            //最初の可視セルを選択する
            this.customDataGridView1.CurrentCell = this.customDataGridView1.Rows[e.RowIndex].Cells.Cast<DataGridViewCell>().OrderBy(x=>x.OwningColumn.DisplayIndex).First(x=> x.Visible==true);

            this.Selected(sender, e);
        }

        /// <summary>
        /// [F10]頭文字ソート
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void InitialSort(object sender, EventArgs e)
        {
            this.logic.InvokeInitialSort();
        }

        /// <summary>
        /// [F1]品名ﾏｽﾀ/個別単価
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void BetsuSearch(object sender, EventArgs e)
        {
            int searchCount = this.logic.Search(true);
            if (searchCount > 0)
            {
                this.customDataGridView1.Focus();
            }
            else if (searchCount.Equals(0))
            {
                if (this.WindowId.Equals(WINDOW_ID.M_HINMEI_SEARCH) && this.isKobetsuHinmeiTankSearchFlg)
                {
                    this.errmessage.MessageBoxShow("E288");
                }
            }
        }

        /// <summary>
        /// [F7]クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Clear(object sender, EventArgs e)
        {
            bool check = true;
            if (this.PARENT_CONDITION_ITEM.Enabled)
            {
                check = false;
                if (this.WindowId.Equals(WINDOW_ID.M_DENSHI_JIGYOUJOU))
                {
                    this.PARENT_CONDITION_ITEM.Text = "2";
                }
                else
                {
                    this.PARENT_CONDITION_ITEM.Text = "3";

                }
            }

            if (this.PARENT_CONDITION_VALUE.Enabled)
            {
                check = false;
                this.PARENT_CONDITION_VALUE.Text = string.Empty;
            }

            if (this.WindowId.Equals(WINDOW_ID.M_DENSHI_JIGYOUJOU))
            {
                this.CHILD_CONDITION_ITEM.Text = "2";
            }
            else
            {
                this.CHILD_CONDITION_ITEM.Text = "3";
            }
            this.CHILD_CONDITION_VALUE.Text = string.Empty;
            this.FILTER_SHIIN_VALUE.Text = string.Empty;
            this.FILTER_BOIN_VALUE.Text = string.Empty;

            if (this.WindowId.Equals(WINDOW_ID.M_HINMEI))
            {
                this.SHURUI_KENSAKU_JOKEN_CBB1.SelectedIndex = -1;
            }

            if (this.WindowId.Equals(WINDOW_ID.M_HINMEI_SEARCH))
            {
                this.SHURUI_KENSAKU_JOKEN_CBB.SelectedIndex = -1;
            }

            if (this.WindowId.Equals(WINDOW_ID.M_GENBA))
            {
                this.PARENT_CONDITION_ITEM.Text = "7";
                this.CHILD_CONDITION_ITEM.Text = "7";
            }

            this.customSortHeader1.ClearCustomSortSetting(); //ソート設定もクリアする
            //this.customDataGridView1.DataSource = null; //データはクリアせず再検索されるまでは残す。


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
            this.logic.SettingProperties(true);//NvNhat #160897
        }

        /// <summary>
        /// [F8]検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Search(object sender, EventArgs e)
        {
            int searchCount = this.logic.Search();
            if (searchCount > 0)
            {
                this.customDataGridView1.Focus();
            }
            else if (searchCount.Equals(0))
            {
                if (this.WindowId.Equals(WINDOW_ID.M_HINMEI_SEARCH) || this.WindowId.Equals(WINDOW_ID.M_HINMEI))
                {
                    this.errmessage.MessageBoxShow("E288");
                }
            }
        }

        /// <summary>
        /// [F9]確定
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
        /// [F10]並べ替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MoveToSort(object sender, EventArgs e)
        {
            this.customSortHeader1.ShowCustomSortSettingDialog();
        }

        /// <summary>
        /// [F12]閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Close(object sender, EventArgs e)
        {
            this.logic.SettingProperties(false);//NvNhat #160897
            base.ReturnParams = null;
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
        #endregion

        //20250325
        internal void SHURUI_KENSAKU_JOKEN_CBB_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string inputValue = this.SHURUI_KENSAKU_JOKEN_CBB.Text;
                bool found = false;

                foreach (var item in this.SHURUI_KENSAKU_JOKEN_CBB.Items)
                {
                    string itemValue = item is DataRowView ? ((DataRowView)item)["SHURUI_NAME_RYAKU"].ToString() : item.ToString();

                    if (itemValue.Equals(inputValue))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    this.errmessage.MessageBoxShowError("値が登録されていません");
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
                
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                return;
            }
        }

        //20250326
        internal void SHURUI_KENSAKU_JOKEN_CBB1_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                string inputValue = this.SHURUI_KENSAKU_JOKEN_CBB1.Text;
                bool found = false;

                foreach (var item in this.SHURUI_KENSAKU_JOKEN_CBB1.Items)
                {
                    string itemValue = item is DataRowView ? ((DataRowView)item)["SHURUI_NAME_RYAKU"].ToString() : item.ToString();

                    if (itemValue.Equals(inputValue))
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    this.errmessage.MessageBoxShowError("値が登録されていません");
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                return;
            }
        }
    }
}
