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
using Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.Logic;
using Seasar.Quill.Attrs;
using r_framework.Utility;
using r_framework.CustomControl;

namespace Shougun.Core.Allocation.JissekiUriageShiharaiKakutei.APP
{
    /// <summary>
    /// G330：実績売上支払確定
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region 内部変数

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        #region プロパティ

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// ヘッダフォーム
        /// </summary>
        internal UIHeader HeaderForm { get; private set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_ZISSEKI_URIAGE_SHIHARAI_KAKUTEI, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            LogUtility.DebugMethodStart();

            this.InitializeComponent();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント処理

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);

                if (!this.DesignMode)
                {
                    this.ParentBaseForm = (BusinessBaseForm)this.Parent;
                    this.HeaderForm = (UIHeader)this.ParentBaseForm.headerForm;

                    // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                    if (this.logic != null)
                    {
                        this.logic.Dispose();
                        this.logic = null;
                    }
                    this.logic = new LogicClass(this);

                    // 画面初期化
                    this.logic.WindowInit();

                    if (!isShown)
                    {
                        this.Height -= 14;
                        isShown = true;
                    }
                }

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    int GRID_HEIGHT_MIN_VALUE = 270;
                    int GRID_WIDTH_MIN_VALUE = 953;
                    int h = this.Height - 170;
                    int w = this.Width;

                    if (h < GRID_HEIGHT_MIN_VALUE)
                    {
                        this.customDataGridView1.Height = GRID_HEIGHT_MIN_VALUE;
                    }
                    else
                    {
                        this.customDataGridView1.Height = h;
                    }
                    if (w < GRID_WIDTH_MIN_VALUE)
                    {
                        this.customDataGridView1.Width = GRID_WIDTH_MIN_VALUE;
                    }
                    else
                    {
                        this.customDataGridView1.Width = w;
                    }

                    if (this.customDataGridView1.Height <= GRID_HEIGHT_MIN_VALUE
                        || this.customDataGridView1.Width <= GRID_WIDTH_MIN_VALUE)
                    {
                        this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Top;
                    }
                    else
                    {
                        this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(e);
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

        #region 取引区分のチェンジイベント

        /// <summary>
        /// 取引区分のチェンジイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TorihikiKbnValue_TextChanged(object sender, EventArgs e)
        {
            // 取引区分によって締日のEnabledを切り替えます。
            this.logic.ChangeTorihikiKbnValue();
        }
        #endregion 取引区分のチェンジイベント

        /// <summary>
        /// チェックボックスセルをクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    DataGridViewCell cell = this.customDataGridView1[e.ColumnIndex, e.RowIndex];
                    if (cell is r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell)
                    {
                        r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell checkCell
                            = cell as r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell;

                        checkCell.Value = !Convert.ToBoolean(checkCell.Value is DBNull ? 0 : checkCell.Value);

                        this.customDataGridView1.RefreshEdit();
                        this.customDataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }


                    CustomDataGridView grid = sender as CustomDataGridView;

                    if (grid != null && e.RowIndex > -1 && this.logic.dispDataRecord
                        && grid.Columns[e.ColumnIndex].Name.Equals(this.CHECK_SELECT.Name))
                    {
                        DataTable tbl = grid.DataSource as DataTable;

                        if ((int)tbl.DefaultView[e.RowIndex][0] == 1
                            && this.logic.IsShimeZumi(tbl.DefaultView[e.RowIndex][this.TORIHIKISAKI_CD.Name].ToString()))
                        {
                            // 完了メッセージ表示
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E150", "取引先CD");
                            tbl.DefaultView[e.RowIndex][0] = 0;
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }

        /// <summary>
        /// チェックボックスセルでスペースキー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if ((e.KeyData & Keys.KeyCode) == Keys.Space)
            {
                DataGridViewCell cell = this.customDataGridView1.CurrentCell;
                if (cell is r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell)
                {
                    r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell checkCell
                        = cell as r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell;

                    checkCell.Value = !Convert.ToBoolean(checkCell.Value is DBNull ? 0 : checkCell.Value);

                    this.customDataGridView1.RefreshEdit();
                    this.customDataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }

        #endregion イベント処理

        #region 20151021 hoanghm #13498

        private void txtBox_KyotenCd_Validated(object sender, EventArgs e)
        {
            if (this.txtBox_KyotenCd.Text.Equals("99"))
            {
                var messageShowLogic = new MessageBoxShowLogic();
                this.txtBox_KyotenNameRyaku.Text = string.Empty;
                messageShowLogic.MessageBoxShow("E020", "拠点");
                this.txtBox_KyotenCd.Focus();
            }
        }

        #endregion

        private void TORIHIKISAKI_CD_custom_Validated(object sender, EventArgs e)
        {
            this.logic.CheckTorihikisakiCd();
        }
    }
}
