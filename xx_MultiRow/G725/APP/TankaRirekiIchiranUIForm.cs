using System;
using System.Data;
using System.Data.SqlTypes;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Message;
using System.Windows.Forms;

namespace Shougun.Core.SalesPayment.TankaRirekiIchiran
{
    /// <summary>
    /// G725 単価履歴画面
    /// </summary>
    public partial class TankaRirekiIchiranUIForm : SuperForm
    {
        #region 宣言
        /// <summary>
        /// ロジッククラス
        /// </summary>
        private TankaRirekiIchiranLogic logic;
        
        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        public bool keyPressFlag = false;

        public SqlDecimal returnTanka = SqlDecimal.Null;
        public DialogResult dialogResult = DialogResult.Cancel;
        public string returnUnitCd = "";

        #endregion

        #region 初期化
        /// <summary>
        /// 
        /// </summary>
        public TankaRirekiIchiranUIForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowID">ウィンドウID</param>
        public TankaRirekiIchiranUIForm(WINDOW_ID windowID, string formId,
            string kyotenCd, string torihikisakiCd, string gyoushaCd, string genbaCd, string unpanGyoushaCd, 
            string nizumiGyoushaCd, string nizumiGenbaCd, string nioroshiGyoushaCd, string nioroshiGenbaCd, string hinmeiCd)
        {
            LogUtility.DebugMethodStart(windowID, formId, kyotenCd, torihikisakiCd, gyoushaCd, genbaCd, unpanGyoushaCd, nizumiGyoushaCd, nizumiGenbaCd, nioroshiGyoushaCd, nioroshiGenbaCd, hinmeiCd);
            this.InitializeComponent();
            this.WindowId = windowID;
            this.logic = new TankaRirekiIchiranLogic(this, formId, kyotenCd, torihikisakiCd,
                gyoushaCd, genbaCd, unpanGyoushaCd, nizumiGyoushaCd, nizumiGenbaCd, nioroshiGyoushaCd, nioroshiGenbaCd, hinmeiCd);
            this.TANKA_RIREKI_ICHIRAN.AutoGenerateColumns = false;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面が表示されたときに処理します
        /// </summary>
        /// <param name="e">イベント引数</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);
            base.OnLoad(e);
            if (!this.logic.WindowInit()) { return; }
            this.Initialize();
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面内の項目を初期化します
        /// </summary>
        public void Initialize()
        {
            LogUtility.DebugMethodStart();
            var allControl = controlUtil.GetAllControls(this);
            foreach (Control control in allControl)
            {
                this.ControlEnter(control);
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// フォーカスイン時に実行されるメソッドの追加を行う
        /// </summary>
        /// <param name="c">追加を行う対象のコントロール</param>
        /// <returns></returns>
        private void ControlEnter(Control control)
        {
            control.Enter -= this.ControlGotFocus;
            control.Enter += this.ControlGotFocus;
        }

        /// <summary>
        /// フォーカスが移ったときにヒントテキストを表示する
        /// </summary>
        protected void ControlGotFocus(object sender, EventArgs e)
        {
            var active = ActiveControl as SuperForm;

            if (active == null)
            {
                if (ActiveControl != null)
                {
                    if (ActiveControl is DataGridView)
                    {
                        if (this.TANKA_RIREKI_ICHIRAN.Rows.Count <= 0)
                        {
                            if (this.keyPressFlag)
                            {
                                var control = this.GetNextControl(ActiveControl, false);

                                this.SelectNextControl(control, false, true, true, true);
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
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            this.keyPressFlag = false;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (e.KeyData.Equals(Keys.LButton | Keys.Back | Keys.Shift)
                  || e.KeyData.Equals(Keys.LButton | Keys.MButton | Keys.Back | Keys.Shift))
                {
                    this.keyPressFlag = true;
                }
            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            if (this.TANKA_RIREKI_ICHIRAN != null)
            {
                this.TANKA_RIREKI_ICHIRAN.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                this.lb_hint.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                this.bt_func1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func2.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func3.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func4.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func5.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func8.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func9.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func10.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func11.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                this.bt_func12.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            }
            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
            base.OnShown(e);
        }
        #endregion

        #region 前月(F1)ボタン
        /// <summary>
        /// 前月ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc1_Clicked(object sender, EventArgs e)
        {
            this.logic.ButtonMonthPrevious();
        }
        #endregion

        #region 次月(F2)ボタン
        /// <summary>
        /// 次月ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc2_Clicked(object sender, EventArgs e)
        {
            this.logic.ButtonMonthNext();
        }
        #endregion

        #region 条件取消(F7)ボタン
        /// <summary>
        /// 条件取消ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc7_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.logic.ClearValueForm();
            this.logic.LoadCondition();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 検索(F8)ボタン
        /// <summary>
        /// 検索ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc8_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            if (this.logic.IsInputRequest())
            {
                return;
            }
            if (this.CheckDate())
            {
                return;
            }
            int count = this.logic.Search();
            if (count <= 0)
            {
                MessageBoxUtility.MessageBoxShow("C001");
            }
            else
            { 
            
            }
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 確定登録(F9)ボタン
        /// <summary>
        /// 確定登録ボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc9_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.TANKA_RIREKI_ICHIRAN.CurrentRow == null)
            {
                MessageBoxUtility.MessageBoxShowError("該当する対象データがありません。");
            }
            else
            {
                this.logic.SetSelectedData(this.TANKA_RIREKI_ICHIRAN.CurrentRow.Index);
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 並び替え(F10)ボタン
        /// <summary>
        /// 並び替えボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc10_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.customSortHeader1.ShowCustomSortSettingDialog();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region フィル(F11)タボタン
        /// <summary>
        /// フィルタボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc11_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            this.customSearchHeader1.ShowCustomSearchSettingDialog();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 閉じる(F12)ボタン
        /// <summary>
        /// 閉じるボタンがクリックされたときに処理します
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        public virtual void ButtonFunc12_Clicked(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.dialogResult = DialogResult.Cancel;

            this.Close();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckDate()
        {
            this.HIDZUKE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.HIDZUKE_TO.BackColor = Constans.NOMAL_COLOR;
            // 入力されない場合
            if (string.IsNullOrEmpty(this.HIDZUKE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.HIDZUKE_TO.Text))
            {
                return false;
            }

            DateTime date_from = DateTime.Parse(this.HIDZUKE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.HIDZUKE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.HIDZUKE_FROM.IsInputErrorOccured = true;
                this.HIDZUKE_TO.IsInputErrorOccured = true;
                this.HIDZUKE_FROM.BackColor = Constans.ERROR_COLOR;
                this.HIDZUKE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "伝票日付From", "伝票日付To" };
                MessageBoxShowLogic msglogic = new MessageBoxShowLogic();
                msglogic.MessageBoxShow("E030", errorMsg);
                this.HIDZUKE_FROM.Focus();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HIDZUKE_FROM_Leave(object sender, EventArgs e)
        {
            this.HIDZUKE_FROM.IsInputErrorOccured = false;
            this.HIDZUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHINSEI_DATE_TO_Leave(object sender, EventArgs e)
        {
            this.HIDZUKE_TO.IsInputErrorOccured = false;
            this.HIDZUKE_TO.BackColor = Constans.NOMAL_COLOR;
        }
        #endregion

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141127 teikyou ダブルクリックを追加する　start
        private void HIDZUKE_TO_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);
            var hidzukeiFromTextBox = this.HIDZUKE_FROM;
            var hidzukeiToTextBox = this.HIDZUKE_TO;
            hidzukeiToTextBox.Text = hidzukeiFromTextBox.Text;
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 取引先, 業者, 現場
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.logic.errorValidating)
            {
                this.logic.beforeGyoushaCd = this.GYOUSHA_CD.Text;
            }
            this.logic.GyoushaCdEnter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.GyoushaCdValidating(e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void GyoushaBeforePopup()
        {
            this.logic.beforeGyoushaCd = this.GYOUSHA_CD.Text;
            this.logic.GyoushaCdEnter(true);
        }

        /// <summary>
        /// 
        /// </summary>
        public void GyoushaAfterPopup()
        {
            this.logic.GetTorihikisakiInfoByGyousha();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.GenbaCdEnter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.GenbaCdValidating(e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void GenbaBeforePopup()
        {
            this.logic.GenbaCdEnter(true);
        }

        /// <summary>
        /// 
        /// </summary>
        public void GenbaAfterPopup()
        {
            this.logic.GetTorihikisakiInfoByGenba();
        }
        #endregion

        #region 運搬業者
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.UnpanGyoushaCdEnter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.UnpanGyoushaCdValidating(e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void UnpanGyoushaBeforePopup()
        {
            this.logic.UnpanGyoushaCdEnter(true);
        }
        #endregion

        #region 荷積業者, 荷積現場
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.logic.errorValidating)
            {
                this.logic.beforeGyoushaCd = this.NIZUMI_GYOUSHA_CD.Text;
            }
            this.logic.NizumiGyoushaCdEnter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.NizumiGyoushaCdValidating(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.NizumiGenbaCdEnter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIZUMI_GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.NizumiGenbaCdValidating(e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void NizumiGyoushaBeforePopup()
        {
            this.logic.beforeGyoushaCd = this.NIZUMI_GYOUSHA_CD.Text;
            this.logic.NizumiGyoushaCdEnter(true);
        }

        /// <summary>
        /// 
        /// </summary>
        public void NizumiGenbaBeforePopup()
        {
            this.logic.NizumiGenbaCdEnter(true);
        }
        #endregion

        #region 荷降業者, 荷降現場
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_Enter(object sender, EventArgs e)
        {
            if (!this.logic.errorValidating)
            {
                this.logic.beforeGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
            }
            this.logic.NioroshiGyoushaCdEnter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.NioroshiGyoushaCdValidating(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Enter(object sender, EventArgs e)
        {
            this.logic.NioroshiGenbaCdEnter();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.NioroshiGenbaCdValidating(e);
        }

        /// <summary>
        /// 
        /// </summary>
        public void NioroshiGyoushaBeforePopup()
        {
            this.logic.beforeGyoushaCd = this.NIOROSHI_GYOUSHA_CD.Text;
            this.logic.NioroshiGyoushaCdEnter(true);
        }

        /// <summary>
        /// 
        /// </summary>
        public void NioroshiGenbaBeforePopup()
        {
            this.logic.NioroshiGenbaCdEnter(true);
        }
        #endregion

        private void TANKA_RIREKI_ICHIRAN_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.SetSelectedData(e.RowIndex);
        }
    }
}
