using System.Collections.Generic;
using System.Windows.Forms;
using MasterKyoutsuPopup1.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Utility;

namespace MasterKyoutsuPopup1.APP
{
    public partial class MasterKyoutsuPopupForm : SuperPopupForm
    {

        public MasterKyoutsuPopupLogic logic;

        public string title = string.Empty;

        public List<string> headerList { get; set; }

        public string sql { get; set; }

        public MasterKyoutsuPopupForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            logic = new MasterKyoutsuPopupLogic(this);
            this.logic.WindowInit();
            this.logic.MasterSearch();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        internal virtual void FormClose(object sender, System.EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// ダブルクリック時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DetailCellDoubleClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            this.logic.ElementDecision();
            this.DialogResult = DialogResult.OK;
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DetailKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Enter:
                        this.logic.ElementDecision();
                        this.DialogResult = DialogResult.OK;
                        break;
                    default:
                        // NOTHING
                        break;
                }
            }
            catch
            {
                throw;
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DetailKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F12)
            {
                base.ReturnParams = null;
                this.Close();
                this.DialogResult = DialogResult.Cancel;
            }
        }
    }
}
