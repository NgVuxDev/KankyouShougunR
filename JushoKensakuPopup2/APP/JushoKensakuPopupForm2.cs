// $Id: JushoKensakuPopupForm.cs 3620 2013-10-15 02:55:28Z sys_dev_02 $
using System;
using System.Windows.Forms;
using JushoKensakuPopup2.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Entity;
using r_framework.Logic;

namespace JushoKensakuPopup2.APP
{
    /// <summary>
    /// 住所検索ポップアップ画面
    /// </summary>
    public partial class JushoKensakuPopupForm2 : SuperPopupForm
    {
        public JushoKensakuPopupLogic2 logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public JushoKensakuPopupForm2()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.KeyDown += JushoDetail_KeyDown;

            S_ZIP_CODE[] list = base.Params[0] as S_ZIP_CODE[];
            logic = new JushoKensakuPopupLogic2(this, list);
            logic.SetDetailList();


        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FormClose(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// ダブルクリック処理時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void JushoDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            this.JushoDetail.CurrentCell = this.JushoDetail.Rows[e.RowIndex].Cells[0];

            bool catchErr = this.logic.ElementDecision();
            if (catchErr)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void JushoDetail_KeyUp(object sender, KeyEventArgs e)
        {
            //呼ばれていない
            if (e.KeyCode == Keys.Enter)
            {
                bool catchErr = this.logic.ElementDecision();
                if (catchErr)
                {
                    return;
                }
                this.DialogResult = DialogResult.OK;
            }
            if (e.KeyCode == Keys.F12)
            {
                base.ReturnParams = null;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        /// <summary>
        /// エンターで選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void JushoDetail_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //グリッドでのEnterはKeyDownでカーソルが下移動するするため、Downで先にハンドルする必要がある
            if (e.KeyCode == Keys.Enter)
            {
                bool catchErr = this.logic.ElementDecision();
                if (catchErr)
                {
                    e.Handled = true;
                    return;
                }
                e.Handled = true;
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
