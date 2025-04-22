using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.CustomControl;
using r_framework.Utility;
using GrapeCity.Win.MultiRow;

namespace Shougun.Core.Allocation.HaishaWariateDay
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_HAISHA_WARIATE_DAY, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Load(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic = new LogicClass(this);
                this.logic.WindowInit();
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrHaisha_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                this.logic.mrHaisha_CellClick(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrMihaisha_CellClick(object sender, GrapeCity.Win.MultiRow.CellEventArgs e)
        {
             this.logic.mrMihaisha_CellClick(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void mrHaisha_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                this.logic.mrHaisha_CellFormatting(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrMihaisha_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.Scope == CellScope.Row)
            {
                this.logic.mrMihaisha_CellFormatting(e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrHaisha_CellBeginEdit(object sender, CellBeginEditEventArgs e)
        {
            this.logic.mrHaisha_CellBeginEdit(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrHaisha_CellValidated(object sender, CellEventArgs e)
        {
            this.logic.CheckKakuteiDenpyou(e);
            if (!this.logic.validationCancelFlg)
            {
                this.logic.mrHaisha_CellValidated(e);
            }
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void mrHaisha_OnCellEnter(object sender, CellEventArgs e)
        {
            this.logic.mrHaisha_OnCellEnter(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbHaishaMemo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                var forward = (Control.ModifierKeys & Keys.Shift) != Keys.Shift;
                this.SelectNextControl(tbHaishaMemo, forward, false, true, true);
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrHaisha_CellDoubleClick(object sender, CellEventArgs e)
        {
            this.logic.mrHaisha_CellDoubleClick(e);
        }

        /// <summary>
        /// 未配車リストのCellDoubleクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrMihaisha_CellDoubleClick(object sender, CellEventArgs e)
        {
            this.logic.mrMihaisha_CellDoubleClick(e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mrMihaisha_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!this.logic.mrMihaisha_MouseDown(e)) { return; }
        }


        private void UNTENSHA_CD_Validated(object sender, EventArgs e)
        {
            if (this.logic.CheckUntensha())
            {
                // 正常に運転者CDが入力されていれば、該当運転者の最初の行を表示
                this.logic.JumpGridRowByCode();
            }
        }

        private void SHASHU_CD_Validated(object sender, EventArgs e)
        {
            // 該当車種の最初の行を表示
            this.logic.JumpGridRowByCode();
        }

		/// <summary>
        /// 
        /// </summary>
        private string beforeSagyouDate = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpSagyouDate_Enter(object sender, EventArgs e)
        {
            beforeSagyouDate = dtpSagyouDate.Text;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpSagyouDate_Validated(object sender, EventArgs e)
        {
            if (beforeSagyouDate != dtpSagyouDate.Text)
            {
                this.logic.ClearForm();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbWariateSettei_TextChanged(object sender, EventArgs e)
        {
            this.logic.selectedCell = null;
            this.mrHaisha.Refresh();
            this.mrMihaisha.Refresh();
        }
    }
}
