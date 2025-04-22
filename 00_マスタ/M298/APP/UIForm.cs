using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;

namespace Shougun.Core.Master.BankIkkatsu
{
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        #endregion

        #region イベント

        /// <summary>
        /// コンストラクター
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_BANK_IKKATSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// 画面Load処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);
                this.logic = new LogicClass(this);
                this.logic.WindowInit();

				// Anchorの設定は必ずOnLoadで行うこと
                if (this.dgvBank != null)
                {
                    this.dgvBank.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;
            base.OnShown(e);
        }

        /// <summary>
        /// DataGridのCellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBank_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.dgvBank_CellValidating(e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 銀行支店CD、銀行支店（変更後）CDのValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_SHITEN_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.BANK_SHITEN_CD_Validating((CustomAlphaNumTextBox)sender, e);
            }
            catch (Exception ex)
            {
                LogUtility.Error("Error", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 銀行CDのTextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_CD_TextChanged(object sender, EventArgs e)
        {
            this.BANK_SHITEN_CD.Text = "";
            this.BANK_SHITEN_NAME.Text = "";
            this.BANK_SHITEN_CD_OLD.Text = "";
            this.KOUZA_SHURUI.Text = "";
            this.KOUZA_NO.Text = "";
            this.KOUZA_NAME.Text = "";
        }

        /// <summary>
        /// 銀行（変更後）CDのTextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BANK_CD_AFTER_TextChanged(object sender, EventArgs e)
        {
            this.BANK_SHITEN_CD_AFTER.Text = "";
            this.BANK_SHITEN_NAME_AFTER.Text = "";
            this.BANK_SHITEN_CD_AFTER_OLD.Text = "";
            this.KOUZA_SHURUI_AFTER.Text = "";
            this.KOUZA_NO_AFTER.Text = "";
            this.KOUZA_NAME_AFTER.Text = "";
        }

        /// <summary>
        /// 銀行支店CD列ポップアップ後処理
        /// </summary>
        public void FURIKOMI_BANK_SHITEN_CD_AFTERColumn_PopupAfter()
        {
            var cell = this.dgvBank.CurrentCell;
            this.dgvBank["KOUZA_SHURUI_AFTERColumn", cell.RowIndex].Value = this.KOUZA_SHURUI_GRID.Text;
            this.dgvBank["KOUZA_NO_AFTERColumn", cell.RowIndex].Value = this.KOUZA_NO_GRID.Text;
            this.dgvBank["KOUZA_NAME_AFTERColumn", cell.RowIndex].Value = this.KOUZA_NAME_GRID.Text;
        }

        /// <summary>
        /// DataGridのRowEnterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBank_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            this.BANK_SHITEN_CD_GRID.Text = this.dgvBank["FURIKOMI_BANK_SHITEN_CD_AFTERColumn", e.RowIndex].Value.ToString();
            this.KOUZA_SHURUI_GRID.Text = this.dgvBank["KOUZA_SHURUI_AFTERColumn", e.RowIndex].Value.ToString();
            this.KOUZA_NO_GRID.Text = this.dgvBank["KOUZA_NO_AFTERColumn", e.RowIndex].Value.ToString();
            this.KOUZA_NAME_GRID.Text = this.dgvBank["KOUZA_NAME_AFTERColumn", e.RowIndex].Value.ToString();
        }

        /// <summary>
        /// DataGridのCellValueChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvBank_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvBank.Columns[e.ColumnIndex].Name == "FURIKOMI_BANK_CD_AFTERColumn")
            {
                var row = this.dgvBank.Rows[e.RowIndex];
                row.Cells["FURIKOMI_BANK_SHITEN_CD_AFTERColumn"].Value = "";
                row.Cells["BANK_SHITEN_NAME_AFTERColumn"].Value = "";
                this.BANK_SHITEN_CD_GRID.Text = "";
                row.Cells["KOUZA_SHURUI_AFTERColumn"].Value = this.KOUZA_SHURUI_GRID.Text = "";
                row.Cells["KOUZA_NO_AFTERColumn"].Value = this.KOUZA_NO_GRID.Text = "";
                row.Cells["KOUZA_NAME_AFTERColumn"].Value = this.KOUZA_NAME_GRID.Text = "";
            }
        }

        private void dgvBank_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                this.logic.dgvBank_CellPainting(e);
            }
        }

        private void dgvBank_Scroll(object sender, ScrollEventArgs e)
        {
            this.dgvBank.Refresh();
        }

        #endregion
    }
}
