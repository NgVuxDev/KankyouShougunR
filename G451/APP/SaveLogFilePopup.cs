using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Utility;

namespace Shougun.Core.Common
{
    public partial class SaveLogFilePopup : SuperForm
    {

        /// <summary>
        /// ロジッククラス
        /// </summary>
        private SaveLogFilePopupLogic logic;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SaveLogFilePopup()
        {
            InitializeComponent();
            this.logic = new SaveLogFilePopupLogic(this);
        }

        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// 初期表示
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            // ログファイル読み込み
            this.logic.LoadLogFiles();

            // グリッドビュー表示
            this.logic.CreateDGVforDate(DateTime.Today);

            // 各項目表示
            this.LogFileNumber.Text = this.logic.LogFiles.Count.ToString();
            this.DisplayNumber.Text = this.logic.Term.ToString();
            this.EndDate.Value = DateTime.Today;
            this.StartDate.Value = this.logic.GetExecuteDate(DateTime.Today, false);

            // イベント初期化
            this.EventInit();
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            this.EndDate.TextChanged += new System.EventHandler(this.EndDate_TextChanged);
            this.EndDate.ValueChanged += new System.EventHandler(this.EndDate_ValueChanged);
            this.DisplayNumber.TextChanged += new EventHandler(this.DisplayNumber_TextChanged);
            this.DisplayNumber.Validated += new EventHandler(this.DisplayNumber_Validated);
            this.customDataGridView1.CellValueChanged += new DataGridViewCellEventHandler(customDataGridView1_CellValueChanged);
            this.customDataGridView1.CurrentCellDirtyStateChanged += new EventHandler(customDataGridView1_CurrentCellDirtyStateChanged);

            foreach (Control ctrl in this.Controls)
            {
                ctrl.MouseHover += new EventHandler(ctrl_MouseHover);
                ctrl.MouseLeave += new EventHandler(ctrl_MouseLeave);
            }
        }

        /// <summary>
        /// キーダウン
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    this.AllCheckButton.PerformClick();
                    break;
                case Keys.F2:
                    this.AllCancelButton.PerformClick();
                    break;
                case Keys.F4:
                    this.PreviewButton.PerformClick();
                    break;
                case Keys.F5:
                    this.UpdateButton.PerformClick();
                    break;
                case Keys.F6:
                    this.ForwardButton.PerformClick();
                    break;
                case Keys.F10:
                    this.SaveFileButton.PerformClick();
                    break;
                case Keys.F12:
                    this.CloseButton.PerformClick();
                    break;
                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        /// <summary>
        /// フォーカスが移ったときにヒントテキストを表示する
        /// </summary>
        private void ctrl_MouseHover(object sender, EventArgs e)
        {
            if (sender == null)
            {
                return;
            }
            this.lb_hint.Text = (string)((Control)sender).Tag;
        }

        /// <summary>
        /// コントロールからマウスが離れたとき、ヒントテキストを消去する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ctrl_MouseLeave(object sender, EventArgs e)
        {
            this.lb_hint.Text = string.Empty;
        }

        /// <summary>
        /// 閉じるボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 保存するボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var list = this.logic.LogFiles.Where(n => n.IsChecked).ToArray();
                string firstFileName = "";
                string lastFileName = "";

                if (list.Length == 0)
                {
                    // 選択ファイル数0
                    MessageBox.Show(this, "ファイルが選択されていません。", "アラート", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 確認メッセージ作成
                var sb = new StringBuilder();
                sb.AppendLine("以下のファイルをZip形式で圧縮して保存します。");
                sb.AppendLine("よろしいですか？");

                for (int i = 0; i < list.Length; i++)
                {
                    sb.AppendLine("　" + list[i].FileName);

                    // isTerminalMode=trueの時のファイル名の決定([firstFileName-lastFileName].ext)
                    if (firstFileName == "")
                    {
                        firstFileName = list[i].FileName;
                        lastFileName = firstFileName;
                    }
                    if (firstFileName != list[i].FileName)
                    {
                        lastFileName = firstFileName + "-" + list[i].FileName;
                    }
                }

                if (DialogResult.Cancel == MessageBox.Show(this, sb.ToString(), "確認", 
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                {
                    // キャンセルクリックで終了
                    return;
                }

                // ファイル保存ダイアログ
                if (r_framework.Dto.SystemProperty.IsTerminalMode)
                {
                    // BrowseForFolderを使う
                    var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
                    var title = "参照するファイルを選択してください。";
                    var initialPath = @"C:\";
                    var windowHandle = this.Handle;
                    var isFileSelect = false;
                    var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

                    if (filePath != "")
                    {
                        if (filePath.Substring(filePath.Length-1, 1) != "\\")
                        {
                            filePath = filePath + "\\" + lastFileName + ".zip";
                        }
                        else
                        {
                            filePath = filePath + lastFileName + ".zip";
                        }

                        browserForFolder = null;

                        this.logic.SaveLogForZip(filePath);
                        MessageBox.Show(this, "ファイルを保存しました。", "インフォメーション",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else
                {
                    // 通常のファイル保存ダイアログを使う
                    using (var sfd = new SaveFileDialog())
                    {
                        sfd.CheckPathExists = true;
                        sfd.Filter = "Zipファイル|*.zip";
                        sfd.Title = "圧縮ファイルの保存先を選択してください。";
                        if (DialogResult.OK == sfd.ShowDialog(this))
                        {
                            var path = sfd.FileName;
                            if (!path.ToLower().EndsWith(".zip"))
                            {
                                path += ".zip";
                            }

                            this.logic.SaveLogForZip(path);
                            MessageBox.Show(this, "ファイルを保存しました。", "インフォメーション",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }


            }
            catch (IOException ex)
            {
                MessageBox.Show(this, "ファイルの保存に失敗しました。もう一度試してください。", "エラー", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogUtility.Error(ex);
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "ファイルの保存に失敗しました。しばらく経ってからもう一度試してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogUtility.Fatal(ex);
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// セルの内容が変更されたとき
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            //this.customDataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
        }

        /// <summary>
        /// セルのバリュー変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void customDataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.customDataGridView1.CurrentRow == null || this.customDataGridView1.Rows.Count == 0)
            {
                return;
            }

            var idxSel = this.customDataGridView1.Columns[this.logic.SELECTED].Index;
            var idxNam = this.customDataGridView1.Columns[this.logic.FILE_NAME].Index;

            if (e.RowIndex != -1 && e.ColumnIndex == idxSel)
            {
                DataGridViewCheckBoxCell clickedCell = ((DataGridViewCheckBoxCell)(this.customDataGridView1[idxSel, e.RowIndex]));
                var fileName = this.customDataGridView1[idxNam, e.RowIndex].Value.ToString();
                var dto = this.logic.LogFiles.FirstOrDefault(n => n.FileName == fileName);
                if (dto != null)
                {
                    // 内部データを更新
                    //dto.IsChecked = clickedCell.EditedFormattedValue.ToString().ToLower().Equals("true");
                    dto.IsChecked = clickedCell.Value.ToString().ToLower().Equals("true");
                }
            }

            // 選択数更新
            this.SelectedNumber.Text = this.logic.LogFiles.Count(n => n.IsChecked).ToString();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 終了日付のバリュー変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndDate_ValueChanged(object sender, EventArgs e)
        {
            this.UpdateStartDate();
        }

        /// <summary>
        /// 終了日付のテキスト変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EndDate_TextChanged(object sender, EventArgs e)
        {
            // 入力中は発生させない
            if (this.EndDate.Text.Length >= 10)
            {
                this.UpdateStartDate();
            }
        }

        /// <summary>
        /// 表示日数のバリュー変更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayNumber_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.DisplayNumber.Text))
            {
                // 空白はフォーカスアウト時にチェックする
                return;
            }

            int result = 0;

            if (int.TryParse(this.DisplayNumber.Text, out result))
            {
                if (result > 0)
                {
                    this.logic.Term = result;

                    // 表示期間も更新する
                    this.UpdateStartDate();
                }
            }
        }

        /// <summary>
        /// 表示日数検証後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DisplayNumber_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.DisplayNumber.Text) || this.DisplayNumber.Text == "0")
            {
                this.DisplayNumber.IsInputErrorOccured = true;
                MessageBox.Show(this, "表示日数は1以上で設定してください。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.DisplayNumber.Focus();
            }
        }

        /// <summary>
        /// 更新ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateButton_Click(object sender, EventArgs e)
        {
            var date = (DateTime)this.EndDate.Value;
            if (date == null)
            {
                MessageBox.Show(this, "表示期間が不正です。もう一度設定してください。", "アラート", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.customDataGridView1.CellValueChanged -= this.customDataGridView1_CellValueChanged;
                var cnt = this.logic.CreateDGVforDate(date);
                this.customDataGridView1.CellValueChanged += this.customDataGridView1_CellValueChanged;
                if (cnt == 0)
                {
                    MessageBox.Show(this, "指定された期間に出力されたログファイルはありません。", "インフォメーション", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            this.AllCancelButton.PerformClick();
        }

        /// <summary>
        /// 前へボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewButton_Click(object sender, EventArgs e)
        {
            this.UpdateEndDate(false);
        }

        /// <summary>
        /// 次へボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ForwardButton_Click(object sender, EventArgs e)
        {
            this.UpdateEndDate(true);
        }

        /// <summary>
        /// 全選択ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllCheckButton_Click(object sender, EventArgs e)
        {
            //this.logic.LogFiles.ForEach(n => n.IsChecked = true);
            for (int i = 0; i < this.customDataGridView1.Rows.Count; i++)
            {
                this.logic.LogFiles.FirstOrDefault(
                                    n => n.FileName == this.customDataGridView1.Rows[i].Cells[this.logic.FILE_NAME].Value.ToString())
                                    .IsChecked = true;

                this.customDataGridView1.Rows[i].Cells[this.logic.SELECTED].Value = "true";
            }
            this.SelectedNumber.Text = this.logic.LogFiles.Count(n => n.IsChecked).ToString();
        }

        /// <summary>
        /// 全選択解除ボタンクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllCancelButton_Click(object sender, EventArgs e)
        {
            this.logic.LogFiles.ForEach(n => n.IsChecked = false);
            for (int i = 0; i < this.customDataGridView1.Rows.Count; i++)
            {
                this.customDataGridView1.Rows[i].Cells[this.logic.SELECTED].Value = "false";
            }
            this.SelectedNumber.Text = this.logic.LogFiles.Count(n => n.IsChecked).ToString();
        }

        /// <summary>
        /// 全選択チェックボックスクリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void customCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            var check = (CheckBox)sender;
            if (check == null)
            {
                return;
            }

            var fileName = string.Empty;

            for (int i = 0; i < this.customDataGridView1.Rows.Count; i++)
            {
                fileName = this.customDataGridView1.Rows[i].Cells[this.logic.FILE_NAME].Value.ToString();
                var dto = this.logic.LogFiles.FirstOrDefault(n => n.FileName == fileName);
                if (dto != null)
                {
                    dto.IsChecked = check.Checked;
                }
                this.customDataGridView1.Rows[i].Cells[this.logic.SELECTED].Value = check.Checked;
            }
        }

        /// <summary>
        /// 開始日を更新します
        /// </summary>
        /// <param name="date"></param>
        private void UpdateStartDate()
        {
            var endDate = (DateTime)this.EndDate.Value;
            if (endDate == null)
            {
                return;
            }

            this.StartDate.Value = this.logic.GetExecuteDate(endDate, false).AddDays(1);
        }

        /// <summary>
        /// 終了日を更新します
        /// </summary>
        /// <param name="isForward">true:次へ, false:前へ</param>
        /// <returns></returns>
        private bool UpdateEndDate(bool isForward)
        {
            var endDate = (DateTime)this.EndDate.Value;

            try
            {
                if (endDate == null || endDate < this.EndDate.MinValue || endDate >= this.EndDate.MaxValue)
                {
                    // 不正
                    throw new ArgumentOutOfRangeException("EndDate");
                }

                var tmp = this.logic.GetExecuteDate(endDate, isForward);
                if (tmp == null || tmp < this.EndDate.MinValue || tmp >= this.EndDate.MaxValue)
                {
                    // 不正
                    throw new ArgumentOutOfRangeException("EndDate");
                }

                this.EndDate.Value = tmp;
            }
            catch (ArgumentOutOfRangeException ex)
            {
                // 演算不可能な日付の場合はログを出して失敗として処理する
                LogUtility.Error(ex);
                MessageBox.Show(this, string.Format("終了日は[{0}]以上、[{1}]以下で設定してください。", 
                    this.EndDate.MinValue.ToString("yyyy/MM/dd"), this.EndDate.MaxValue.ToString("yyyy/MM/dd")), 
                    "アラート", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // 元に戻す
                this.EndDate.Value = endDate;
                return false;
            }
            return true;
        }
    }
}