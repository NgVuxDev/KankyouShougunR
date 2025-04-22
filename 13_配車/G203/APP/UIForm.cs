using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Allocation.HannyuusakiKyuudouNyuuryoku
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
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_HANNYUUSAKI_KYUUDOU_NYUURYOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClass(this);
            }
            catch (Exception ex)
            {
                LogUtility.Error("UIForm", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 画面 Loadイベント

        /// <summary>
        /// 画面Load処理
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
                LogUtility.Error("OnLoad", ex);
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
                LogUtility.Error("dgvHiduke2_Enter", ex);
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
                this.dgvHiduke1.CurrentCell = null;
                this.dgvHiduke2.CurrentCell = null;
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

        #region 搬入先MultiRow SelectionChangedイベント

        /// <summary>
        /// 搬入先MultiRow SelectionChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void gcHannyuusakiList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 当前選択された行のIndexを取得する
                int rowIndex = this.gcHannyuusakiList.CurrentRow.Index;

                // 20141112 koukouei 休動管理機能 start
                paintFlg = false;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                bool henkouFlg = this.logic.workCloseHenkouCheck();
                if (rowIndex != this.logic.rowIndex && henkouFlg && msgLogic.MessageBoxShowConfirm("選択した内容が破棄されますがよろしいですか。") != DialogResult.Yes)
                {
                    this.gcHannyuusakiList.Rows[this.logic.rowIndex].Selected = true;
                    paintFlg = true;
                    return;
                }
                else if (rowIndex == this.logic.rowIndex)
                {
                    return;
                }

                this.logic.rowIndex = rowIndex;
                // 20141112 koukouei 休動管理機能 end

                // 業者CDを設定する
                this.GYOUSHA_CD.Text = this.gcHannyuusakiList[rowIndex, ConstClass.ColName.GYOUSHA_CD].Value.ToString();
                // 業者名を設定する
                this.GYOUSHA_NAME_RYAKU.Text = this.gcHannyuusakiList[rowIndex, ConstClass.ColName.GYOUSHA_NAME_RYAKU].Value.ToString();
                // 現場CDを設定する
                this.GENBA_CD.Text = this.gcHannyuusakiList[rowIndex, ConstClass.ColName.GENBA_CD].Value.ToString();
                // 現場名を設定する
                this.GENBA_NAME_RYAKU.Text = this.gcHannyuusakiList[rowIndex, ConstClass.ColName.GENBA_NAME_RYAKU].Value.ToString();

                // 検索日付を取得する
                String searchDate = this.monthCalendar.SelectionStart.ToString("yyyy/MM");
                // 搬入先休動データを取得
                this.logic.GetHannyuusakiKyuudouData(searchDate);
            }
            catch (Exception ex)
            {
                LogUtility.Error("gcHannyuusakiList_SelectionChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 日付DataGridView CellClickイベント

        /// <summary>
        /// 日付DataGridView1 CellClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHiduke1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex < 0)
                {
                    return;
                }

                this.calendarFlg = false;

                // 選択した行目の日付を取得する
                String selectedDay = this.dgvHiduke1.Rows[e.RowIndex].Cells["CLOSED_DATE"].Value.ToString();
                // 日付DataGridViewの行クリック処理
                this.logic.dgvHidukeCellClick(selectedDay);
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvHiduke1_CellClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 日付DataGridView2 CellClickイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHiduke2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (e.RowIndex < 0)
                {
                    return;
                }

                this.calendarFlg = false;

                // 選択した行目の日付を取得する
                String selectedDay = this.dgvHiduke2.Rows[e.RowIndex].Cells["CLOSED_DATE2"].Value.ToString();
                // 日付DataGridViewの行クリック処理
                this.logic.dgvHidukeCellClick(selectedDay);
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvHiduke2_CellClick", ex);
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
        /// 日付DataGridView1 Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHiduke1_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.dgvHiduke2.CurrentCell = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvHiduke1_Enter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 日付DataGridView2 Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHiduke2_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.dgvHiduke1.CurrentCell = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvHiduke2_Enter", ex);
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
        private void dgvHiduke2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && e.Shift && dgvHiduke2.CurrentCell != null && dgvHiduke2.CurrentCell.ColumnIndex == 0 && dgvHiduke2.CurrentCell.RowIndex == 0)
                {
                    // 左上の場合
                    dgvHiduke1.Select();
                    dgvHiduke1.CurrentCell = dgvHiduke1[3, 15];

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvHiduke2_KeyDown", ex);
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
        private void dgvHiduke2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // タブのKeyDownを発生させるための処理
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && e.Shift && dgvHiduke2.CurrentCell != null && dgvHiduke2.CurrentCell.ColumnIndex == 0 && dgvHiduke2.CurrentCell.RowIndex == 0)
                {
                    // 左上の場合
                    e.IsInputKey = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvHiduke2_PreviewKeyDown", ex);
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
        /// カレンダ日付 DateChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                LogUtility.Error("monthCalendar_DateChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region カレンダ日付 DateSelectedイベント

        /// <summary>
        /// カレンダ日付 DateSelectedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void monthCalendar_DateSelected(object sender, DateRangeEventArgs e)
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
                LogUtility.Error("monthCalendar_DateSelected", ex);
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
            if (this.dgvHiduke1.Rows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow row in this.dgvHiduke1.Rows)
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
            if (this.dgvHiduke2.Rows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow row in this.dgvHiduke2.Rows)
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
        private void gcHannyuusakiList_Paint(object sender, PaintEventArgs e)
        {
            if (this.gcHannyuusakiList.Rows.Count > 0 && this.paintFlg)
            {
                this.gcHannyuusakiList.CurrentCellChanged -= this.gcHannyuusakiList_SelectionChanged;
                this.gcHannyuusakiList.CurrentCell = this.gcHannyuusakiList[0, this.logic.rowIndex];
                this.gcHannyuusakiList.CurrentCellChanged += this.gcHannyuusakiList_SelectionChanged;
                this.paintFlg = false;
            }
        }

        #endregion

        internal void dgvHiduke1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var cell = this.dgvHiduke1["HENKOU_FLG1", e.RowIndex];
                bool flg = Convert.ToBoolean(cell.Value);
                cell.Value = !flg;
            }
        }

        internal void dgvHiduke2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var cell = this.dgvHiduke2["HENKOU_FLG2", e.RowIndex];
                bool flg = Convert.ToBoolean(cell.Value);
                cell.Value = !flg;
            }
        }

        private void dgvHiduke1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = this.dgvHiduke1[e.ColumnIndex, e.RowIndex];
            if (cell is r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell)
            {
                dgvHiduke1.ImeMode = ImeMode.Disable;
            }
            else
            {
                dgvHiduke1.ImeMode = ImeMode.Hiragana;
            }
        }

        private void dgvHiduke2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = this.dgvHiduke2[e.ColumnIndex, e.RowIndex];
            if (cell is r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell)
            {
                dgvHiduke2.ImeMode = ImeMode.Disable;
            }
            else
            {
                dgvHiduke2.ImeMode = ImeMode.Hiragana;
            }
        }

        // 20141112 koukouei 休動管理機能 end
    }
}