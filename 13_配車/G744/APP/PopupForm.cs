using System.Data;
using System.Linq;
using System.Windows.Forms;
using r_framework.APP.PopUp.Base;
using r_framework.Utility;
using r_framework.Logic;
using System;
namespace Shougun.Core.Allocation.CarTransferTeiki
{
    public partial class PopupForm : SuperPopupForm
    {

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        public PopupLogic logic;
        public PopupDTOCls popupDto;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public string sql { get; set; }

        public PopupForm(PopupDTOCls dto)
        {
            InitializeComponent();
            this.Ichiran.IsBrowsePurpose = true;
            this.popupDto = dto;
        }

        protected override void OnLoad(System.EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            this.logic = new PopupLogic(this);

            bool catchErr = this.logic.WindowInit();

            this.Ichiran.KeyDown += this.DetailKeyDown; //行選択のEnterがKeyUpだけでやるとフォーカスがした移動してから選択されるため、ガードが必要

            // 表示を調整
            catchErr = this.logic.CordinateGridSize();
            if (catchErr)
            {
                return;
            }
            this.bt_func12.Click += this.FormClose;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        internal void FormClose(object sender, System.EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// ダブルクリック時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
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
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //グリッドでのEnterはKeyDownでカーソルが下移動するするため、Downで先にハンドルする必要がある
            if (e.KeyCode == Keys.Enter)
            {
                bool catchErr = this.logic.ElementDecision();
                if (catchErr)
                {
                    return;
                }
                e.Handled = true;
                this.DialogResult = DialogResult.OK;
            }
        }

        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 400;

        /// <summary>
        /// ラベルタイトルテキストチェンジ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_title_TextChanged(object sender, System.EventArgs e)
        {
            ControlUtility.AdjustTitleSize(lb_title, this.TitleMaxWidth);
        }

        #region 選択イベント
        /// <summary>
        /// 選択処理
        /// </summary>
        internal void Selected(object sender, System.EventArgs e)
        {
            if (this.Ichiran.CurrentRow == null || this.Ichiran.CurrentRow.Index == -1)
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
        #endregion
    }
}
