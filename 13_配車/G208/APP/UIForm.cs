using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Allocation.Sharyoukyuudounyuryoku
{
    public partial class UIForm : SuperForm
    {
        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// カレンダ変更フラグ
        /// </summary>
        private bool calendarFlg = true;

        /// <summary>
        /// paintフラグ
        /// </summary>
        internal bool paintFlg;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        #region コンストラクタ

        public UIForm()
            //コンストラクタ
            : base(WINDOW_ID.T_SYARYOU_KYUUDOU_NYUURYOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
        }

        #endregion

        #region 画面 Loadイベント

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                base.OnLoad(e);

                // 画面情報の初期化
                this.logic.WindowInit();

                if (!isShown)
                {
                    this.Height -= 7;
                    isShown = true;
                }
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面 Shownイベント

        /// <summary>
        /// 画面最初表示されたときの処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 赤枠非表示する
                this.clearCurrentCell();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// DataGridView赤枠非表示処理
        /// </summary>
        internal void clearCurrentCell()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 赤枠非表示する
                this.dgvSharyouKyudou1.CurrentCell = null;
                this.dgvSharyouKyudou2.CurrentCell = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("clearCurrentCell", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region カレンダ日付 DateChangedイベント

        /// <summary>
        /// カレンダ日付変更処理
        /// </summary>
        /// <returns></returns>
        private void monthCalendar_DateChanged(object sender, DateRangeEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // カレンダ選択の最大日数を設定する
                this.monthCalendar.MaxSelectionCount = 1;

                // カレンダ日付変更処理
                if (!this.logic.calendarDateChanged(calendarFlg)) { return; }
                this.calendarFlg = true;
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 車輌DataGridView CurrentCellChangedイベント

        /// <summary>
        /// 車輌DataGridView CurrentCellChangedイベント
        /// </summary>
        /// <returns></returns>
        public void dgvSharyou_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // 行目を取得する
                int index = this.dgvSharyou.CurrentCell.RowIndex;

                // 20141112 koukouei 休動管理機能 start
                paintFlg = false;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool catchErr = false;
                bool henkouFlg = this.logic.workCloseHenkouCheck(out catchErr);
                if (catchErr) { return; }
                if (index != this.logic.rowIndex && henkouFlg && msgLogic.MessageBoxShowConfirm("選択した内容が破棄されますがよろしいですか。") != DialogResult.Yes)
                {
                    this.dgvSharyou.Rows[this.logic.rowIndex].Selected = true;
                    paintFlg = true;
                    return;
                }
                else if (index == this.logic.rowIndex)
                {
                    return;
                }
                this.logic.rowIndex = index;
                // 20141112 koukouei 休動管理機能 end

                // 検索業者CD
                this.TXT_GYOUSHA_CD.Text = this.dgvSharyou["GYOUSHA_CD", index].Value.ToString();
                // 車輌CDを設定する
                this.TXT_SHARYOU_CD.Text = this.dgvSharyou["SHARYOU_CD", index].Value.ToString();
                // 車輌名を設定する
                this.TXT_SHARYOU_NAME.Text = this.dgvSharyou["SHARYOU_NAME_RYAKU", index].Value.ToString();
                // 20141008 koukouei 休動管理機能 start
                // 運搬業者CDを設定する
                this.TXT_UNPAN_GYOUSHA_CD.Text = Convert.ToString(this.dgvSharyou["GYOUSHA_CD", index].Value);
                // 運搬業者名を設定する
                this.TXT_UNPAN_GYOUSHA_NAME.Text = Convert.ToString(this.dgvSharyou["GYOUSHA_NAME_RYAKU", index].Value);
                // 20141008 koukouei 休動管理機能 end

                // 20141120 koukoueo 休動管理機能 start
                // 車種CDを設定する
                this.TXT_SHASYU_CD.Text = this.dgvSharyou["SHASYU_CD", index].Value.ToString();
                // 車種名を設定する
                this.TXT_SHASYU_NAME.Text = this.dgvSharyou["SHASHU_NAME_RYAKU", index].Value.ToString();
                // 20141120 koukoueo 休動管理機能 end

                // 検索日付を取得する
                String searchDate = this.monthCalendar.SelectionStart.ToString("yyyy/MM");

                // 車輌休動データを取得
                this.logic.SearchWorkClosedSharyouData(this.TXT_GYOUSHA_CD.Text, this.TXT_SHARYOU_CD.Text, searchDate);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 日付DataGridView CurrentCellChangedイベント

        /// <summary>
        /// 日付DataGridView CurrentCellChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dgvSharyouKyudou1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.dgvSharyouKyudou1.CurrentCell == null)
                {
                    return;
                }

                int index = this.dgvSharyouKyudou1.CurrentCell.RowIndex;
                this.calendarFlg = false;
                // 車輌休動DataGridView1の行クリック処理
                this.logic.dgvSharyouKyudouCellClick(index, "1");
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        public void dgvSharyouKyudou2_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.dgvSharyouKyudou2.CurrentCell == null)
                {
                    return;
                }

                int index = this.dgvSharyouKyudou2.CurrentCell.RowIndex;
                this.calendarFlg = false;
                // 車輌休動DataGridView1の行クリック処理
                this.logic.dgvSharyouKyudouCellClick(index, "2");
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ConstClass.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 日付DataGridView Enterイベント

        /// <summary>
        /// 日付DataGridView Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSharyouKyudou1_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.dgvSharyouKyudou2.CurrentCell = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvSharyouKyudou1_Enter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        private void dgvSharyouKyudou2_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.dgvSharyouKyudou1.CurrentCell = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvSharyouKyudou2_Enter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 日付DataGridView2 KeyDownイベント

        /// <summary>
        /// 日付DataGridView2 KeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSharyouKyudou2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && e.Shift && dgvSharyouKyudou2.CurrentCell != null && dgvSharyouKyudou2.CurrentCell.ColumnIndex == 0 && dgvSharyouKyudou2.CurrentCell.RowIndex == 0)
                {
                    // 左上の場合
                    dgvSharyouKyudou1.Select();
                    dgvSharyouKyudou1.CurrentCell = dgvSharyouKyudou1[3, 15];

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvSharyouKyudou2_KeyDown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 日付DataGridView2 PreviewKeyDownイベント

        /// <summary>
        /// 日付DataGridView2 PreviewKeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSharyouKyudou2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // タブのKeyDownを発生させるための処理
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && e.Shift && dgvSharyouKyudou2.CurrentCell != null && dgvSharyouKyudou2.CurrentCell.ColumnIndex == 0 && dgvSharyouKyudou2.CurrentCell.RowIndex == 0)
                {
                    // 左上の場合
                    e.IsInputKey = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvSharyouKyudou2_PreviewKeyDown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        // 20141112 koukouei 休動管理機能 start

        #region 日付DataGridView 一括チェックイベント

        /// <summary>
        /// 日付DataGridView 一括チェックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.dgvSharyouKyudou1.Rows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow row in this.dgvSharyouKyudou1.Rows)
            {
                row.Cells[2].Value = checkBox1.Checked;
            }
        }

        #endregion

        #region 日付DataGridView2 一括チェックイベント

        /// <summary>
        /// 日付DataGridView2 一括チェックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (this.dgvSharyouKyudou2.Rows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow row in this.dgvSharyouKyudou2.Rows)
            {
                row.Cells[2].Value = checkBox2.Checked;
            }
        }

        #endregion

        #region 車輌DataGridView Paintイベント

        /// <summary>
        /// 車輌DataGridView Paintイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvSharyou_Paint(object sender, PaintEventArgs e)
        {
            if (this.dgvSharyou.Rows.Count > 0 && this.paintFlg)
            {
                this.dgvSharyou.CurrentCellChanged -= this.dgvSharyou_CurrentCellChanged;
                this.dgvSharyou.CurrentCell = this.dgvSharyou[0, this.logic.rowIndex];
                this.dgvSharyou.CurrentCellChanged += this.dgvSharyou_CurrentCellChanged;
                this.paintFlg = false;
            }
        }

        #endregion

        internal void dgvSharyouKyudou1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var cell = this.dgvSharyouKyudou1["HENKOU_FLG1", e.RowIndex];
                bool flg = Convert.ToBoolean(cell.Value);
                cell.Value = !flg;
            }
        }

        internal void dgvSharyouKyudou2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var cell = this.dgvSharyouKyudou2["HENKOU_FLG2", e.RowIndex];
                bool flg = Convert.ToBoolean(cell.Value);
                cell.Value = !flg;
            }
        }

        private void dgvSharyouKyudou1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = this.dgvSharyouKyudou1[e.ColumnIndex, e.RowIndex];
            if (cell is r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell)
            {
                dgvSharyouKyudou1.ImeMode = ImeMode.Disable;
            }
            else
            {
                dgvSharyouKyudou1.ImeMode = ImeMode.Hiragana;
            }
        }

        private void dgvSharyouKyudou2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = this.dgvSharyouKyudou2[e.ColumnIndex, e.RowIndex];
            if (cell is r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell)
            {
                dgvSharyouKyudou2.ImeMode = ImeMode.Disable;
            }
            else
            {
                dgvSharyouKyudou2.ImeMode = ImeMode.Hiragana;
            }
        }

        // 20141112 koukouei 休動管理機能 end
    }
}