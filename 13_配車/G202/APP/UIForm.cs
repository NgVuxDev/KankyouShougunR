using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;

namespace Shougun.Core.Allocation.Untenshakyudounyuuryoku
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
            : base(WINDOW_ID.T_UNTENSYA_KYUUDOU_NYUURYOKU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
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
                if (!this.logic.WindowInit()) { return; }

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
                this.dgvUntenShaKyudou1.CurrentCell = null;
                this.dgvUntenShaKyudou2.CurrentCell = null;
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

        #region 運転者DataGridView CurrentCellChangedイベント

        /// <summary>
        /// 運転者DataGridView CurrentCellChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dgvUntenSha_CurrentCellChanged(object sender, EventArgs e)
        {
            int index = this.dgvUntenSha.CurrentCell.RowIndex;

            // 20141112 koukouei 休動管理機能 start
            paintFlg = false;
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            bool catchErr = false;
            bool henkouFlg = this.logic.workCloseHenkouCheck(out catchErr);
            if (catchErr) { return; }
            if (index != this.logic.rowIndex && henkouFlg && msgLogic.MessageBoxShowConfirm("選択した内容が破棄されますがよろしいですか。") != DialogResult.Yes)
            {
                this.dgvUntenSha.Rows[this.logic.rowIndex].Selected = true;
                paintFlg = true;
                return;
            }
            else if (index == this.logic.rowIndex)
            {
                return;
            }
            this.logic.rowIndex = index;
            // 20141112 koukouei 休動管理機能 end

            // 検索運転者CDを取得する
            String shainCd = this.dgvUntenSha.Rows[index].Cells[ConstClass.ColName.SHAIN_CD].Value.ToString();

            // 運転者CDを設定する
            this.TXT_UNTENSHA_CD.Text = shainCd;
            // 運転者名を設定する
            this.TXT_UNTENSHA_NAME.Text = this.dgvUntenSha.Rows[index].Cells[ConstClass.ColName.SHAIN_NAME_RYAKU].Value.ToString();

            // 検索日付を取得する
            String searchDate = this.monthCalendar.SelectionStart.ToString("yyyy/MM");

            // 運転者休動データを取得
            this.logic.SearchWorkClosedUntenshaData(shainCd, searchDate);
        }

        #endregion

        #region 運転休動DataGridView1 CurrentCellChangedイベント

        /// <summary>
        /// 運転休動DataGridView1 CurrentCellChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dgvUntenShaKyudou1_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.dgvUntenShaKyudou1.CurrentCell == null)
                {
                    return;
                }

                int index = this.dgvUntenShaKyudou1.CurrentCell.RowIndex;
                this.calendarFlg = false;
                // 運転者休動DataGridView1の行クリック処理
                this.logic.dgvUntenShaKyudouCellClick(index, "1");
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

        #region 運転休動DataGridView2 CurrentCellChangedイベント

        /// <summary>
        /// 運転休動DataGridView2 CurrentCellChangedイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void dgvUntenShaKyudou2_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.dgvUntenShaKyudou2.CurrentCell == null)
                {
                    return;
                }

                int index = this.dgvUntenShaKyudou2.CurrentCell.RowIndex;
                this.calendarFlg = false;
                // 運転者休動DataGridView1の行クリック処理
                this.logic.dgvUntenShaKyudouCellClick(index, "2");
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

        #region 運転休動DataGridView1 Enterイベント

        /// <summary>
        /// 運転休動DataGridView1 Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUntenShaKyudou1_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.dgvUntenShaKyudou2.CurrentCell = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvUntenShaKyudou1_Enter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 運転休動DataGridView2 Enterイベント

        /// <summary>
        /// 運転休動DataGridView2 Enterイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUntenShaKyudou2_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.dgvUntenShaKyudou1.CurrentCell = null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvUntenShaKyudou2_Enter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 運転休動DataGridView2 KeyDownイベント

        /// <summary>
        /// 運転休動DataGridView2 KeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUntenShaKyudou2_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && e.Shift && dgvUntenShaKyudou2.CurrentCell != null && dgvUntenShaKyudou2.CurrentCell.ColumnIndex == 0 && dgvUntenShaKyudou2.CurrentCell.RowIndex == 0)
                {
                    // 左上の場合
                    dgvUntenShaKyudou1.Select();
                    dgvUntenShaKyudou1.CurrentCell = dgvUntenShaKyudou1[3, 15];

                    e.Handled = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvUntenShaKyudou2_KeyDown", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 運転休動DataGridView2 PreviewKeyDownイベント

        /// <summary>
        /// 運転休動DataGridView2 PreviewKeyDownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvUntenShaKyudou2_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // タブのKeyDownを発生させるための処理
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && e.Shift && dgvUntenShaKyudou2.CurrentCell != null && dgvUntenShaKyudou2.CurrentCell.ColumnIndex == 0 && dgvUntenShaKyudou2.CurrentCell.RowIndex == 0)
                {
                    // 左上の場合
                    e.IsInputKey = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("dgvUntenShaKyudou2_PreviewKeyDown", ex);
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
            if (this.dgvUntenShaKyudou1.Rows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow row in this.dgvUntenShaKyudou1.Rows)
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
            if (this.dgvUntenShaKyudou2.Rows.Count == 0)
            {
                return;
            }
            foreach (DataGridViewRow row in this.dgvUntenShaKyudou2.Rows)
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
        private void dgvUntenSha_Paint(object sender, PaintEventArgs e)
        {
            if (this.dgvUntenSha.Rows.Count > 0 && this.paintFlg)
            {
                this.dgvUntenSha.CurrentCellChanged -= this.dgvUntenSha_CurrentCellChanged;
                this.dgvUntenSha.CurrentCell = this.dgvUntenSha[0, this.logic.rowIndex];
                this.dgvUntenSha.CurrentCellChanged += this.dgvUntenSha_CurrentCellChanged;
                this.paintFlg = false;
            }
        }

        #endregion

        internal void dgvUntenShaKyudou1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var cell = this.dgvUntenShaKyudou1["HENKOU_FLG1", e.RowIndex];
                bool flg = Convert.ToBoolean(cell.Value);
                cell.Value = !flg;
            }
        }

        internal void dgvUntenShaKyudou2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                var cell = this.dgvUntenShaKyudou2["HENKOU_FLG2", e.RowIndex];
                bool flg = Convert.ToBoolean(cell.Value);
                cell.Value = !flg;
            }
        }

        private void dgvUntenShaKyudou1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = this.dgvUntenShaKyudou1[e.ColumnIndex, e.RowIndex];
            if (cell is r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell)
            {
                dgvUntenShaKyudou1.ImeMode = ImeMode.Disable;
            }
            else
            {
                dgvUntenShaKyudou1.ImeMode = ImeMode.Hiragana;
            }
        }

        private void dgvUntenShaKyudou2_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = this.dgvUntenShaKyudou2[e.ColumnIndex, e.RowIndex];
            if (cell is r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxCell)
            {
                dgvUntenShaKyudou2.ImeMode = ImeMode.Disable;
            }
            else
            {
                dgvUntenShaKyudou2.ImeMode = ImeMode.Hiragana;
            }
        }

        // 20141112 koukouei 休動管理機能 end
    }
}