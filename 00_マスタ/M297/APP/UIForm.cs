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
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Reflection;
using r_framework.Utility;

namespace Shougun.Core.Master.EigyoTantoushaIkkatsu
{
    [Implementation]
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
            : base(WINDOW_ID.M_EIGYOU_TANTOUSHA_IKKATSU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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
                if (this.dgvTorihikisaki != null)
                {
                    this.dgvTorihikisaki.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
                if (this.dgvGyousha != null)
                {
                    this.dgvGyousha.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
                if (this.dgvGenba != null)
                {
                    this.dgvGenba.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
                if (this.labelEIGYOUSHA_AFTER != null)
                {
                    this.labelEIGYOUSHA_AFTER.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                }
                if (this.EIGYOUSHA_CD_AFTER != null)
                {
                    this.EIGYOUSHA_CD_AFTER.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                }
                if (this.EIGYOUSHA_NAME_AFTER != null)
                {
                    this.EIGYOUSHA_NAME_AFTER.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
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
        /// DataGridのCellValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgv_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.dgv_CellValidating(e);
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
        /// 営業者CDのValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EIGYOUSHA_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.logic.EIGYOUSHA_CD_Validating((CustomAlphaNumTextBox)sender, e);
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
        /// 社員部署CDのTextChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHAINBUSHO_CD_TextChanged(object sender, EventArgs e)
        {
            this.EIGYOUSHA_CD.Text = "";
            this.EIGYOUSHA_NAME.Text = "";
        }

        private void dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                this.logic.dgv_CellPainting(e);
            }
        }

        #endregion

        /// <summary>
        /// 部署の変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHAINBUSHO_CD_Validating(object sender, CancelEventArgs e)
        {
            //MOD NHU 20211102 #157016 S
            //this.logic.isError = false;
            //if (!this.logic.BushoCdValidated())
            //{
            //    e.Cancel = true;
            //}
            //MOD NHU 20211102 #157016 E
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

        #region MOD NHU 20211006 #155767
        private void CONDITION_VALUE_Enter(object sender, EventArgs e)
        {
            if (this.CONDITION_ITEM.Text == "取引先名"
                || this.CONDITION_ITEM.Text == "業者名"
                || this.CONDITION_ITEM.Text == "現場名"
                || this.CONDITION_ITEM.Text == "住所"
                || this.CONDITION_ITEM.Text == "部署名")
            {
                this.CONDITION_VALUE.ImeMode = ImeMode.Hiragana;
            }
        }
        // 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex == -1)
            {
                this.CHECK_ALL.Checked = !this.CHECK_ALL.Checked;
                this.logic.dgv.Refresh();
                this.CHECK_ALL.Focus();
            }
        }

        /// <summary>
        /// チェックボックス状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CHECK_ALL_CheckedChanged(object sender, EventArgs e)
        {
            if (this.logic.dgv.Rows.Count == 0)
            {
                return;
            }
            this.logic.dgv.CurrentCell = this.logic.dgv.Rows[0].Cells[2];
            foreach (DataGridViewRow row in this.logic.dgv.Rows)
            {
                row.Cells[0].Value = this.CHECK_ALL.Checked;
            }
            this.logic.dgv.CurrentCell = this.logic.dgv.Rows[0].Cells[0];
        }
        #endregion
    }
}
