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
using Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai.Logic;
using DataGridViewCheckBoxColumnHeaderEx;

namespace Shougun.Core.ElectronicManifest.TuuchiJouhouShoukai
{
    /// <summary>
    /// 通知情報明細
    /// </summary>
    public partial class TuuchiJouhouMeisai : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private TuuchiJouhouMeisaiLogic logic;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 修正／取消フラグ
        /// </summary>
        public bool ApprovalFlg { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public TuuchiJouhouMeisai()
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new TuuchiJouhouMeisaiLogic (this);

        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="tuuchiBiFrom">通知日From</param>
        /// <param name="tuuchiBiTo">通知日To</param>
        /// <param name="kakuninKbn">確認</param>
        /// <param name="tuuchiCd">通知コード</param>
        public TuuchiJouhouMeisai(string tuuchiBiFrom , string tuuchiBiTo , string kakuninKbn , string tuuchiCd)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new TuuchiJouhouMeisaiLogic(this);

            //パラメータ
            Properties.Settings.Default.tuuchiBiFrom = tuuchiBiFrom;
            Properties.Settings.Default.tuuchiBiTo = tuuchiBiTo;
            Properties.Settings.Default.readFlag = kakuninKbn;
            Properties.Settings.Default.tuuchiCd = tuuchiCd;
            Properties.Settings.Default.Save();

        }
        #endregion

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

        #region 初期化
        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            // Anchorの設定は必ずOnLoadで行うこと
            this.cdgv_TuuchiJouhouMeisai.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            //修正／取消SEQモード
            if (Const.ConstCls.TuuchiCd.Contains(Properties.Settings.Default.tuuchiCd)) this.ApprovalFlg = true;
            else this.ApprovalFlg = false;

            //初期化
            if (!this.logic.WindowInit())
            {
                return;
            }
                        
            //ヘッダーのチェックボックスカラムを追加
            HeaderCheckBoxSupport();

            //検索処理
            if (!this.logic.Kensaku())
            {
                return;
            }

            //確認列のチェック状態を設定する
            if (string.Equals(Const.ConstCls.KAKUNIN_DEFAULT, Properties.Settings.Default.readFlag))
                this.SetKakuinCbState();

        }

        /// <summary>
        /// ヘッダーのチェックボックスカラムを追加処理
        /// </summary>
        public void HeaderCheckBoxSupport()
        {

            DataGridviewCheckboxHeaderCell headerCell = 
                new DataGridviewCheckboxHeaderCell(Const.ConstCls.SHOUNIN_COLUMN_INDEX);

            //承認列のプロパティを設定する
            if (this.ApprovalFlg)
            {
                headerCell.CheckBoxEnable = true;
                this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].ReadOnly = false;
            }
            else
            {
                headerCell.CheckBoxEnable = false;
                this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].ReadOnly = true;
            }

            this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderCell = headerCell;
            this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderText = Const.ConstCls.SHOUNIN_HEADERTEXT;
            this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderCell.Style.Alignment = 
                DataGridViewContentAlignment.TopCenter;
            this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].ToolTipText = Const.ConstCls.SHOUNIN_TOOLTIPTEXT;

            //否認列のプロパティを設定する
            headerCell = new DataGridviewCheckboxHeaderCell(Const.ConstCls.HININ_COLUMN_INDEX);
            if (this.ApprovalFlg)
            {
                headerCell.CheckBoxEnable = true;
                this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].ReadOnly = false;
            }
            else
            {
                headerCell.CheckBoxEnable = false;
                this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].ReadOnly = true;
            }
            this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderCell = headerCell;
            this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderText = Const.ConstCls.HININ_HEADERTEXT;
            this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderCell.Style.Alignment = 
                DataGridViewContentAlignment.TopCenter;
            this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].ToolTipText = Const.ConstCls.HININ_TOOLTIPTEXT;

            //確認列のプロパティを設定する
            headerCell = new DataGridviewCheckboxHeaderCell(Const.ConstCls.KAKUNIN_COLUMN_INDEX);
            headerCell.CheckBoxEnable = true;
            this.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].HeaderCell = headerCell;
            this.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].HeaderText = Const.ConstCls.KAKUNIN_HEADERTEXT;
            this.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].HeaderCell.Style.Alignment = 
                DataGridViewContentAlignment.TopCenter;
            this.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].ToolTipText = Const.ConstCls.KAKUNIN_TOOLTIPTEXT;

        }
        #endregion

        #region Formクローズ処理
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            //パラメータ
            Properties.Settings.Default.tuuchiBiFrom = string.Empty;
            Properties.Settings.Default.tuuchiBiTo = string.Empty;
            Properties.Settings.Default.readFlag = string.Empty;
            Properties.Settings.Default.tuuchiCd = string.Empty;
            Properties.Settings.Default.Save();

            BusinessBaseForm parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録
        /// </summary>
        public virtual void Regist(object sender, EventArgs e)
        {
            this.logic.Regist();
        }
        #endregion

        #region セールクリック
        /// <summary>
        /// セールクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string kanriId = string.Empty;
            string seq = string.Empty;
            string ApprovalSeq = string.Empty;
            string LatestSeq = string.Empty;

            //ボタン列クリック
            if (this.cdgv_TuuchiJouhouMeisai.CurrentCell is System.Windows.Forms.DataGridViewButtonCell)
            {

                kanriId = Convert.ToString(this.cdgv_TuuchiJouhouMeisai.CurrentRow.Cells["KANRI_ID"].Value);
                if (ApprovalFlg) seq = Convert.ToString(this.cdgv_TuuchiJouhouMeisai.CurrentRow.Cells["APPROVAL_SEQ"].Value);
                else seq = Convert.ToString(this.cdgv_TuuchiJouhouMeisai.CurrentRow.Cells["LATEST_SEQ"].Value);

                ApprovalSeq = Convert.ToString(this.cdgv_TuuchiJouhouMeisai.CurrentRow.Cells["APPROVAL_SEQ"].Value);
                LatestSeq = Convert.ToString(this.cdgv_TuuchiJouhouMeisai.CurrentRow.Cells["LATEST_SEQ"].Value);
                string tuuchiCd = Properties.Settings.Default.tuuchiCd;

                //電子マニフェスト入力画面を表示する
                this.logic.ElectronicManifestNyuryokuShow(kanriId, seq, ApprovalSeq, LatestSeq, tuuchiCd);
                return;
            }

            //承認列クリック
            if (this.cdgv_TuuchiJouhouMeisai.CurrentCell is System.Windows.Forms.DataGridViewCheckBoxCell
                && this.cdgv_TuuchiJouhouMeisai.CurrentCell.ColumnIndex == Const.ConstCls.SHOUNIN_COLUMN_INDEX)
            {

                if ((bool)this.cdgv_TuuchiJouhouMeisai.CurrentRow.Cells["SHOUNIN"].EditedFormattedValue)
                    this.cdgv_TuuchiJouhouMeisai.CurrentRow.Cells["HININ"].Value = false;
                this.SetShouninCbState();
                return;
            }

            //否認列クリック
            if (this.cdgv_TuuchiJouhouMeisai.CurrentCell is System.Windows.Forms.DataGridViewCheckBoxCell
                && this.cdgv_TuuchiJouhouMeisai.CurrentCell.ColumnIndex == 3)
            {
                if ((bool)this.cdgv_TuuchiJouhouMeisai.CurrentRow.Cells["HININ"].EditedFormattedValue)
                    this.cdgv_TuuchiJouhouMeisai.CurrentRow.Cells["SHOUNIN"].Value = false;
                this.SetHininCbState();
                return;
            }

            //確認列クリック
            if (this.cdgv_TuuchiJouhouMeisai.CurrentCell is System.Windows.Forms.DataGridViewCheckBoxCell
                && this.cdgv_TuuchiJouhouMeisai.CurrentCell.ColumnIndex == 4)
            {
                this.SetKakuinCbState();
                return;
            }

        }

        /// <summary>
        /// 承認チェックボックスの状態を設定する
        /// </summary>
        private void SetShouninCbState()
        {
            int cbCheckedCount = 0;

            foreach(DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
            {
                if ((bool)dgvr.Cells["SHOUNIN"].EditedFormattedValue) cbCheckedCount += 1;
            }

            if (cbCheckedCount == 0)
                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                    (this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderCell)).SetCheckBoxState(false);
            if (cbCheckedCount == this.cdgv_TuuchiJouhouMeisai.RowCount)
            {
                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                    (this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderCell)).SetCheckBoxState(true);
                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                    (this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderCell)).SetCheckBoxState(false);
            }

            this.cdgv_TuuchiJouhouMeisai.Invalidate();
        }

        /// <summary>
        /// 否認チェックボックスの状態を設定する
        /// </summary>
        private void SetHininCbState()
        {
            int cbCheckedCount = 0;

            foreach (DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
            {
                if ((bool)dgvr.Cells["HININ"].EditedFormattedValue) cbCheckedCount += 1;
            }

            if (cbCheckedCount == 0)
                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                    (this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderCell)).SetCheckBoxState(false);
            if (cbCheckedCount == this.cdgv_TuuchiJouhouMeisai.RowCount)
            {
                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                    (this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderCell)).SetCheckBoxState(true);
                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                    (this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderCell)).SetCheckBoxState(false);
            }

            this.cdgv_TuuchiJouhouMeisai.Invalidate();
        }

        /// <summary>
        /// 確認チェックボックスの状態を設定する
        /// </summary>
        private void SetKakuinCbState()
        {
            int cbCheckedCount = 0;

            foreach (DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
            {
                if ((bool)dgvr.Cells["KAKUNIN"].EditedFormattedValue) cbCheckedCount += 1;
            }

            if (cbCheckedCount == 0)
                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                    (this.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].HeaderCell)).SetCheckBoxState(false);
            if (cbCheckedCount == this.cdgv_TuuchiJouhouMeisai.RowCount)
                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                    (this.cdgv_TuuchiJouhouMeisai.Columns["KAKUNIN"].HeaderCell)).SetCheckBoxState(true);

            this.cdgv_TuuchiJouhouMeisai.Invalidate();
        }
        #endregion

        #region 列ヘッダーがクリック
        /// <summary>
        /// 列ヘッダーがクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cdgv_TuuchiJouhouMeisai_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int cbCheckedCount = 0;

            //データ件数が０の場合
            if(this.cdgv_TuuchiJouhouMeisai.RowCount ==0 ) return ;

            if (e.ColumnIndex == 3)
            {
                //否認列
                DataGridViewColumn col = this.cdgv_TuuchiJouhouMeisai.Columns[e.ColumnIndex];
                DataGridviewCheckboxHeaderCell header = col.HeaderCell as DataGridviewCheckboxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                }
                foreach (DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
                {
                    if ((bool)dgvr.Cells["HININ"].EditedFormattedValue) cbCheckedCount += 1;
                }

                if (cbCheckedCount == 0)
                    ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                        (this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderCell)).SetCheckBoxState(false);
                if (cbCheckedCount == this.cdgv_TuuchiJouhouMeisai.RowCount)
                {
                    ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                        (this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderCell)).SetCheckBoxState(true);
                    ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                        (this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderCell)).SetCheckBoxState(false);

                    foreach (DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
                    {
                        dgvr.Cells["SHOUNIN"].Value = false;
                    }
                }
            }
            else if (e.ColumnIndex == 2)
            {
                //承認列
                DataGridViewColumn col = this.cdgv_TuuchiJouhouMeisai.Columns[e.ColumnIndex];
                DataGridviewCheckboxHeaderCell header = col.HeaderCell as DataGridviewCheckboxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                }
                foreach (DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
                {
                    if ((bool)dgvr.Cells["SHOUNIN"].EditedFormattedValue) cbCheckedCount += 1;
                }

                if (cbCheckedCount == 0)
                    ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                        (this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderCell)).SetCheckBoxState(false);
                if (cbCheckedCount == this.cdgv_TuuchiJouhouMeisai.RowCount)
                {
                    ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                        (this.cdgv_TuuchiJouhouMeisai.Columns["SHOUNIN"].HeaderCell)).SetCheckBoxState(true);
                    ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                        (this.cdgv_TuuchiJouhouMeisai.Columns["HININ"].HeaderCell)).SetCheckBoxState(false);

                    foreach (DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
                    {
                        dgvr.Cells["HININ"].Value=false;
                    }
                }
            }
            else if (e.ColumnIndex == 4)
            {
                DataGridViewColumn col = this.cdgv_TuuchiJouhouMeisai.Columns[e.ColumnIndex];
                DataGridviewCheckboxHeaderCell header = col.HeaderCell as DataGridviewCheckboxHeaderCell;
                if (header != null)
                {
                    header.MouseClick(e);
                }
            }

            this.cdgv_TuuchiJouhouMeisai.RefreshEdit();
            this.cdgv_TuuchiJouhouMeisai.Invalidate();

        }
        #endregion

        public void cdgv_TuuchiJouhouMeisai_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewCell cell = this.cdgv_TuuchiJouhouMeisai[e.ColumnIndex, e.RowIndex];
                if (cell is DataGridViewCheckBoxCell)
                {
                    DataGridViewCheckBoxCell checkCell = cell as DataGridViewCheckBoxCell;
                    bool allOffFlg = false;
                    bool allOnFlg = false;

                    if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
                    {
                        // 否認、承認列
                        int colDiff = 1;
                        if (e.ColumnIndex == 3)
                        {
                            colDiff = -1;
                        }

                        DataGridViewCell targetCell = this.cdgv_TuuchiJouhouMeisai[e.ColumnIndex + colDiff, e.RowIndex];
                        DataGridViewCheckBoxCell targetCheckCell = targetCell as DataGridViewCheckBoxCell;

                        if (!Convert.ToBoolean(checkCell.Value))
                        {
                            //チェック付ける時、targetCellはOFF
                            targetCheckCell.Value = Convert.ToBoolean(checkCell.Value) ? false : false;
                            // ROWのセルが全部OFFならheaderも外す
                            foreach (DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
                            {
                                if ((bool)dgvr.Cells[e.ColumnIndex + colDiff].Value)
                                {
                                    allOffFlg = true;
                                }
                                if (!(bool)dgvr.Cells[e.ColumnIndex].Value && dgvr.Cells[e.ColumnIndex].RowIndex != e.RowIndex)
                                {
                                    allOnFlg = true;
                                }
                            }
                            if (!allOffFlg)
                            {
                                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                                    (this.cdgv_TuuchiJouhouMeisai.Columns[e.ColumnIndex + colDiff].HeaderCell)).SetCheckBoxState(false);
                            }
                            if (!allOnFlg)
                            {
                                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                                    (this.cdgv_TuuchiJouhouMeisai.Columns[e.ColumnIndex].HeaderCell)).SetCheckBoxState(true);
                            }
                        }
                        else
                        {
                            foreach (DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
                            {
                                if ((bool)dgvr.Cells[e.ColumnIndex].Value && dgvr.Cells[e.ColumnIndex].RowIndex != e.RowIndex)
                                {
                                    allOffFlg = true;
                                    break;
                                }
                            }
                            if (!allOffFlg)
                            {
                                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                                    (this.cdgv_TuuchiJouhouMeisai.Columns[e.ColumnIndex].HeaderCell)).SetCheckBoxState(false);
                            }
                        }

                        this.cdgv_TuuchiJouhouMeisai.RefreshEdit();
                        this.cdgv_TuuchiJouhouMeisai.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                    else if (e.ColumnIndex == 4)
                    {
                        // 確認列
                        if (!Convert.ToBoolean(checkCell.Value))
                        {
                            // ROWのセルが全部OFFならheaderも外す
                            foreach (DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
                            {
                                if (!(bool)dgvr.Cells[e.ColumnIndex].Value && dgvr.Cells[e.ColumnIndex].RowIndex != e.RowIndex)
                                {
                                    allOnFlg = true;
                                    break;
                                }
                            }
                            if (!allOnFlg)
                            {
                                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                                    (this.cdgv_TuuchiJouhouMeisai.Columns[e.ColumnIndex].HeaderCell)).SetCheckBoxState(true);
                            }
                        }
                        else
                        {
                            foreach (DataGridViewRow dgvr in this.cdgv_TuuchiJouhouMeisai.Rows)
                            {
                                if ((bool)dgvr.Cells[e.ColumnIndex].Value && dgvr.Cells[e.ColumnIndex].RowIndex != e.RowIndex)
                                {
                                    allOffFlg = true;
                                    break;
                                }
                            }
                            if (!allOffFlg)
                            {
                                ((DataGridViewCheckBoxColumnHeaderEx.DataGridviewCheckboxHeaderCell)
                                    (this.cdgv_TuuchiJouhouMeisai.Columns[e.ColumnIndex].HeaderCell)).SetCheckBoxState(false);
                            }
                        }

                        this.cdgv_TuuchiJouhouMeisai.RefreshEdit();
                        this.cdgv_TuuchiJouhouMeisai.CommitEdit(DataGridViewDataErrorContexts.Commit);
                    }
                }
            }
        }

        public void cdgv_TuuchiJouhouMeisai_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = this.cdgv_TuuchiJouhouMeisai.CurrentCell;
            if (cell is DataGridViewCheckBoxCell)
            {
                if (cell.ColumnIndex == 2 || cell.ColumnIndex == 3)
                {
                    int colDiff = 1;
                    if (cell.ColumnIndex == 3)
                    {
                        colDiff = -1;
                    }

                    DataGridViewCell targetCell = this.cdgv_TuuchiJouhouMeisai[cell.ColumnIndex + colDiff, cell.RowIndex];
                    DataGridViewCheckBoxCell targetCheckCell = targetCell as DataGridViewCheckBoxCell;
                    DataGridViewCheckBoxCell checkCell = cell as DataGridViewCheckBoxCell;

                    if (!Convert.ToBoolean(checkCell.Value))
                    {
                        targetCheckCell.Value = Convert.ToBoolean(checkCell.Value) ? false : false;
                    }

                    this.cdgv_TuuchiJouhouMeisai.RefreshEdit();
                    this.cdgv_TuuchiJouhouMeisai.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }
    }
}
