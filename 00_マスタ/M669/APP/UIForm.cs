using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Master.ContenaQrHakkou.Logic;
using System.Drawing;
using System.Data;

namespace Shougun.Core.Master.ContenaQrHakkou.APP
{
    /// <summary>
    /// QRコード発行
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicCls logic;

        /// <summary>
        /// メッセージ
        /// </summary>
        public MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        /// <summary>
        /// 前回コンテナ種類コード
        /// </summary>
        public string beforCONTENA_SHURUI_CD = string.Empty;

        /// <summary>
        /// 前回コンテナ種類コードPopBefor
        /// </summary>
        private string preCONTENA_SHURUI_CD { get; set; }

        /// <summary>
        /// 前回コンテナ種類コードPopAfter
        /// </summary>
        private string curCONTENA_SHURUI_CD { get; set; }

        #endregion

        #region 画面初期処理
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.M_CONTENA_QR_HAKKOU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicCls(this);
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
                if (!this.logic.WindowInit())
                {
                    return;
                }

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.Ichiran != null)
                {
                    this.Ichiran.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom;
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

        #endregion

        #region Founction処理

        #region コンテナ種類チェック処理
        /// <summary>
        /// コンテナ種類チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONTENA_SHURUI_CD_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (string.IsNullOrEmpty(this.CONTENA_SHURUI_CD.Text))
                {
                    this.CONTENA_SHURUI_NAME_RYAKU.Text = string.Empty;
                    this.CONTENA_CD.Text = string.Empty;
                    this.CONTENA_NAME_RYAKU.Text = string.Empty;
                    this.beforCONTENA_SHURUI_CD = string.Empty;
                }
                else if (this.beforCONTENA_SHURUI_CD != this.CONTENA_SHURUI_CD.Text)
                {
                    this.CONTENA_CD.Text = string.Empty;
                    this.CONTENA_NAME_RYAKU.Text = string.Empty;
                    this.beforCONTENA_SHURUI_CD = this.CONTENA_SHURUI_CD.Text;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("CONTENA_SHURUI_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コンテナ種類前回値
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONTENA_SHURUI_CD_Enter(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.beforCONTENA_SHURUI_CD = this.CONTENA_SHURUI_CD.Text;

            }
            catch (Exception ex)
            {
                LogUtility.Error("CONTENA_SHURUI_CD_Enter", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

        }

        /// <summary>
        /// コンテナ種類 PopupBeforeExecuteMethod
        /// </summary>
        public void CONTENA_SHURUI_CD_PopupBeforeExecuteMethod()
        {
            preCONTENA_SHURUI_CD = this.CONTENA_SHURUI_CD.Text;
        }

        /// <summary>
        /// コンテナ種類 PopupAfterExecuteMethod
        /// </summary>
        public void CONTENA_SHURUI_CD_PopupAfterExecuteMethod()
        {
            curCONTENA_SHURUI_CD = this.CONTENA_SHURUI_CD.Text;
            if (preCONTENA_SHURUI_CD != curCONTENA_SHURUI_CD)
            {
                this.CONTENA_CD.Text = string.Empty;
                this.CONTENA_NAME_RYAKU.Text = string.Empty;
            }
        }
        #endregion

        #region コンテナチェック処理
        /// <summary>
        /// コンテナチェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONTENA_CD_Validating(object sender, CancelEventArgs e)
        {
            // コンテナ種類CD入力しない場合
            if (string.IsNullOrEmpty(this.CONTENA_SHURUI_CD.Text) && !string.IsNullOrEmpty(this.CONTENA_CD.Text))
            {
                this.errmessage.MessageBoxShow("E012", "コンテナ種類CD");
                this.CONTENA_NAME_RYAKU.Text = string.Empty;
                this.CONTENA_CD.IsInputErrorOccured = true;
                this.CONTENA_CD.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.CONTENA_CD.Text))
            {
                this.CONTENA_NAME_RYAKU.Text = string.Empty;
            }
            else
            {
                // コンテナチェック
                this.logic.CheckContena();
            }
        }
        #endregion

        #region プレビュー処理
        /// <summary>
        /// プレビュー処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void PreView(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (false == base.RegistErrorFlag)
                {
                    // 明細がない場合
                    if (this.Ichiran.Rows.Count <= 0)
                    {
                        this.errmessage.MessageBoxShow("E044");
                        return;
                    }
                    else
                    {
                        // 明細選択がない場合
                        bool isChecked = false;
                        foreach (DataGridViewRow row in this.Ichiran.Rows)
                        {
                            if ((bool)row.Cells["DET_CHECKED"].Value)
                            {
                                if (row.Cells["DET_SHOYUU_DAISUU"].Value == null || string.IsNullOrEmpty(row.Cells["DET_SHOYUU_DAISUU"].Value.ToString()))
                                {
                                    ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells["DET_SHOYUU_DAISUU"], true);
                                    row.Cells["DET_SHOYUU_DAISUU"].Style.BackColor = Constans.ERROR_COLOR;
                                    row.Cells["DET_SHOYUU_DAISUU"].Selected = true;
                                    this.errmessage.MessageBoxShow("E048", "1～99");
                                    this.Ichiran.Focus();
                                    row.Cells["DET_SHOYUU_DAISUU"].Selected = true;
                                    return;
                                }

                                isChecked = true;
                            }
                        }

                        if (!isChecked)
                        {
                            this.errmessage.MessageBoxShow("E051", "印刷対象の明細");
                            return;
                        }
                    }

                    // QRコードを発行処理
                    this.logic.PreView();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("PreView", ex);
                throw;
            }
        }
        #endregion

        #region 検索処理
        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Search(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 明細クリア
                this.Ichiran.Rows.Clear();
                this.ALL_CHECKED.Checked = false;

                // 画面データ検索
                if (this.logic.Search() == -1)
                {
                    return;
                }
                else
                {
                    // 検索結果表示する
                    if (0 < this.logic.SearchResult.Rows.Count)
                    {
                        int rowIndex = 0;
                        foreach (DataRow row in this.logic.SearchResult.Rows)
                        {
                            this.Ichiran.Rows.Add();
                            this.Ichiran.Rows[rowIndex].Cells["DET_CHECKED"].Value = false;
                            this.Ichiran.Rows[rowIndex].Cells["DET_CONTENA_SHURUI_CD"].Value = row["CONTENA_SHURUI_CD"].ToString();
                            this.Ichiran.Rows[rowIndex].Cells["DET_CONTENA_SHURUI_NAME_RYAKU"].Value = row["CONTENA_SHURUI_NAME_RYAKU"].ToString();
                            this.Ichiran.Rows[rowIndex].Cells["DET_CONTENA_CD"].Value = row["CONTENA_CD"].ToString();
                            this.Ichiran.Rows[rowIndex].Cells["DET_CONTENA_NAME_RYAKU"].Value = row["CONTENA_NAME_RYAKU"].ToString();
                            if (!string.IsNullOrEmpty(row["SHOYUU_DAISUU"].ToString()))
                            {
                                int daisuu = int.Parse(row["SHOYUU_DAISUU"].ToString());

                                if (daisuu > 99)
                                {
                                    // 100以上の場合は99で表示。
                                    daisuu = 99;
                                }

                                this.Ichiran.Rows[rowIndex].Cells["DET_SHOYUU_DAISUU"].Value = daisuu;
                            }
                            else
                            {
                                this.Ichiran.Rows[rowIndex].Cells["DET_SHOYUU_DAISUU"].Value = string.Empty;
                            }
                            
                            rowIndex++;
                        }

                    }
                    else
                    {
                        // 明細クリア
                        this.Ichiran.Rows.Clear();
                        // ゼロ件メッセージ表示
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("C001", "検索結果");
                    }

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 閉じる処理
        /// <summary>
        /// 閉じる処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void FormClose(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                var parentForm = (MasterBaseForm)this.Parent;
                this.Close();
                parentForm.Close();
            }
            catch (Exception ex)
            {
                LogUtility.Error("FormClose", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 列ヘッダーにチェックボックスを表示
        /// <summary>
        /// 列ヘッダーにチェックボックスを表示
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void Ichiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (e.ColumnIndex == 0)
                {
                    // 列ヘッダーのみ処理を行う。(CheckBox配置列が先頭列の場合)
                    if (e.RowIndex == -1)
                    {
                        using (Bitmap bmp = new Bitmap(100, 100))
                        {
                            // チェックボックスの描画領域を確保
                            using (Graphics g = Graphics.FromImage(bmp))
                            {
                                g.Clear(Color.Transparent);
                            }

                            // 描画領域の中央に配置
                            Point pt1 = new Point((bmp.Width - ALL_CHECKED.Width) / 2, (bmp.Height - ALL_CHECKED.Height) / 2);
                            if (pt1.X < 0) pt1.X = 0;
                            if (pt1.Y < 0) pt1.Y = 0;

                            // Bitmapに描画
                            ALL_CHECKED.DrawToBitmap(bmp, new Rectangle(pt1.X, pt1.Y, bmp.Width, bmp.Height));

                            // DataGridViewの現在描画中のセルの中央に描画
                            int x = (e.CellBounds.Width - bmp.Width) / 2;
                            int y = (e.CellBounds.Height - bmp.Height) / 2;

                            Point pt2 = new Point(e.CellBounds.Left + x, e.CellBounds.Top + y);

                            e.Paint(e.ClipBounds, e.PaintParts);
                            e.Graphics.DrawImage(bmp, pt2);
                            e.Handled = true;
                        }
                    }
                }  
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellPainting", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            
            
            
        }
        #endregion

        #region 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// <summary>
        /// すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void ALL_CHECKED_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.Ichiran.Rows.Count == 0)
                {
                    return;
                }
                foreach (DataGridViewRow row in this.Ichiran.Rows)
                {
                    row.Cells["DET_CHECKED"].Value = ALL_CHECKED.Checked;
                }

                this.Ichiran.CurrentCell = this.Ichiran.Rows[0].Cells[1];

            }
            catch (Exception ex)
            {
                LogUtility.Error("ALL_CHECKED_CheckedChanged", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }


        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void Ichiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (e.ColumnIndex == 0 && e.RowIndex == -1)
                {
                    ALL_CHECKED.Checked = !ALL_CHECKED.Checked;
                    this.Ichiran.Refresh();
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellClick", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion
    }
}