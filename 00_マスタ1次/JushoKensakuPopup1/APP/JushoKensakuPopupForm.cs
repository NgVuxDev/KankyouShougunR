// $Id: JushoKensakuPopupForm.cs 12989 2013-12-26 09:22:04Z ishibashi $
using System;
using System.Windows.Forms;
using JushoKensakuPopup1.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Entity;

namespace JushoKensakuPopup1.APP
{
    /// <summary>
    /// 住所検索ポップアップ画面
    /// </summary>
    public partial class JushoKensakuPopupForm : SuperPopupForm
    {
        public JushoKensakuPopupLogic logic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public JushoKensakuPopupForm()
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

            S_ZIP_CODE[] list = base.Params[0] as S_ZIP_CODE[];
            logic = new JushoKensakuPopupLogic(this, list);
            logic.SetDetailList();
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
        /// ダブルクリック処理時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void JushoDetail_CellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
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
        internal void JushoDetail_KeyUp(object sender, KeyEventArgs e)
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
