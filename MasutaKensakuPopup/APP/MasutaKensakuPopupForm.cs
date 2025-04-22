// $Id: MasutaKensakuPopupForm.cs 13875 2014-01-10 11:37:25Z koga $
using System;
using System.Windows.Forms;
using MasutaKensakuPopup.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.CustomControl;
using r_framework.Logic;
using System.Data;

namespace MasutaKensakuPopup.APP
{
    /// <summary>
    /// 検索項目ポップアップ画面
    /// </summary>
    public partial class MasutaKensakuPopupForm : SuperPopupForm
    {
        public MasutaKensakuPopupLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        private GcCustomMultiRow gcMultiRow;
        private CustomDataGridView dgv;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MasutaKensakuPopupForm()
        {
            InitializeComponent();
            this.MasterItem.IsBrowsePurpose = true;

            logic = new MasutaKensakuPopupLogic();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (base.Params[0] is GcCustomMultiRow)
            {
                this.gcMultiRow = base.Params[0] as GcCustomMultiRow;
            }

            if (base.Params[0] is CustomDataGridView)
            {
                this.dgv = base.Params[0] as CustomDataGridView;
            }

            logic = new MasutaKensakuPopupLogic(this, this.gcMultiRow, this.dgv);

            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            MultiRowCreateLogic multiRowCreateLogic = new MultiRowCreateLogic();

            DataTable dt = multiRowCreateLogic.CreateDetailList(base.Params[0]);

            this.MasterItem.DataSource = dt;

        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FormClose(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ItemKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                base.ReturnParams = null;
                this.FormClose(this, new EventArgs());
            }
        }

        /// <summary>
        /// 選択処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool catchErr = this.logic.ElementDecision();
                if (catchErr)
                {
                    return;
                }
            }
            base.OnKeyDown(e);
        }

        /// <summary>
        /// ダブルクリック時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MasterItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            this.logic.ElementDecision();
        }
    }
}
