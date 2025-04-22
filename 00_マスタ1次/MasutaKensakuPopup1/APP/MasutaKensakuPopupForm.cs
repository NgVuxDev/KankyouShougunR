// $Id: MasutaKensakuPopupForm.cs 12989 2013-12-26 09:22:04Z ishibashi $
using System;
using System.Windows.Forms;
using MasutaKensakuPopup1.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.CustomControl;

namespace MasutaKensakuPopup1.APP
{
    /// <summary>
    /// 検索項目ポップアップ画面
    /// </summary>
    public partial class MasutaKensakuPopupForm : SuperPopupForm
    {
        public MasutaKensakuPopupLogic logic;

        private GcCustomMultiRow gcMultiRow;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MasutaKensakuPopupForm()
        {
            InitializeComponent();

            logic = new MasutaKensakuPopupLogic();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.gcMultiRow = (GcCustomMultiRow)base.Params[0];
            logic = new MasutaKensakuPopupLogic(this, this.gcMultiRow);
            this.logic.WindowInit();
            this.logic.CreateDetailList();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void FormClose(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ダブルクリック時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ItemCellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            this.logic.ElementDecision();
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ItemKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.logic.ElementDecision();
            }
            if (e.KeyCode == Keys.F12)
            {
                base.ReturnParams = null;
                this.Close();
            }
        }
    }
}
