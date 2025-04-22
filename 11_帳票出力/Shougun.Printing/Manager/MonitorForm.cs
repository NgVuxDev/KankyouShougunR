using System;
using System.ComponentModel;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Diagnostics;
using Shougun.Printing.Common;
using Shougun.Printing.Common.UI;
using Shougun.Printing.Viewer;
using System.Runtime.InteropServices;
using System.Linq;

namespace Shougun.Printing.Manager
{
    /// <summary>
    /// 自動印刷モード列挙子
    /// </summary>
    internal enum autoPrintingMode
    {
        /// <summary>
        /// 直印刷要求帳票のみ印刷
        /// </summary>
        Auto,

        /// <summary>
        // 手動モード（自動では何もしない）
        /// </summary>
        Manual,

        /// <summary>
        /// 全印刷モード
        /// </summary>
        All,
    }

    /// <summary>
    /// 環境将軍R印刷管理/印刷中・XPSファイル監視フォーム(モードレス)
    /// </summary>
    public partial class MonitorForm : Form
    {
        private static MonitorForm instance = null;
    
        /// <summary>
        /// 表示時(非最小化状態)のWindow状態(最大化/標準のどちらか)
        /// 最小化状態に切り替わり時に直前の状態を設定する。最小化状態からの復元時に参照する。
        /// アプリ終了時に保存し起動後の最初の表示状態復元に利用する。
        /// </summary>
        private FormWindowState ShownWindowState = FormWindowState.Normal;

        /// <summary>
        /// 最後に通常状態（最大化/最小化ではない）だったときのFormの位置とサイズ。
        /// リサイズ時に設定する。
        /// アプリ終了時に保存し起動後の最初の表示状態復元に利用する。
        /// </summary>
        private Rectangle FormNormalBounds;

        private bool firstSelfFindFlag = true;

        private bool exitApplication = false;

        /// <summary>
        /// モニタ(印刷メイン画面)の作成とXPS出力監視の開始
        /// </summary>
        /// <returns></returns>
        public static Form CreateFormAndStartMonitoring()
        {
            if (Common.Initializer.ProcessMode == ProcessMode.NotSet)
            {
                throw new Exception("ProcessModes is not set.");
            }

            if (string.IsNullOrEmpty(LocalDirectories.PrintingDirectory))
            {
                throw new Exception("PrintingDirectory is not set.");
            }

            if (MonitorForm.instance == null)
            {
                // 最初の作成時はFormをユーザーに見せたくないので最小化状態でShowする。
                // さらにOnLoadでHideすることにより生成の瞬間を完全に見えなくする。
                MonitorForm.instance = new MonitorForm();
                MonitorForm.instance.ShowInTaskbar = false;
                MonitorForm.instance.WindowState = FormWindowState.Minimized;
                MonitorForm.instance.Show();
            }
            return MonitorForm.instance;
        }


        public static void TerminateMonitoring()
        {
            if (MonitorForm.instance != null)
            {
                MonitorForm.instance.exitApplication = true;
                MonitorForm.instance.Close();
            }
        }

                /// <summary>
        /// コンストラクタ
        /// </summary>
        private MonitorForm()
        {
            InitializeComponent();
            this.BackColor = FormStyle.FormBackColor;

            PreviewManager.Instance.CommandEventRelayHandler += this.onViewerCommand;

            this.filesysWatcher.EnableRaisingEvents = false;
            this.findTimer.Enabled = false;
            this.updateTimer.Enabled = false;
            this.filesysPreviewWatcher.EnableRaisingEvents = false;
            this.findPreviewTimer.Enabled = false;
            this.mapWatcher.EnableRaisingEvents = false;
            this.findMapTimer.Enabled = false;
        }

        // NOTE:ListBoxのAnchorでフォームのリサイズにあわせてListBoxもサイズが変わるようにしている。
        // ただし、ListBox.IntegralHeight(デフォルトはtrue)をfalseにしないとListBozのサイズが崩れるので注意。
        
        /// <summary>
        /// 最初のフォーム表示イベント。非表示にしてディレクトリ監視を開始する
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            this.Hide(); 

            this.filesysWatcher = new System.IO.FileSystemWatcher();
            this.filesysWatcher.Path = LocalDirectories.PrintingDirectory;
            this.filesysWatcher.IncludeSubdirectories = false;
            this.filesysWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName;
            this.filesysWatcher.Filter = "*.*";
            this.filesysWatcher.SynchronizingObject = this;
            this.filesysWatcher.Changed += onFilesysWatcherRaiseChanged;
            this.filesysWatcher.Created += onFilesysWatcherRaiseChanged;
            this.filesysWatcher.Deleted += onFilesysWatcherRaiseChanged;
            this.filesysWatcher.Renamed += onFilesysWatcherRaiseRenamed;
            this.filesysWatcher.EnableRaisingEvents = true;

            ReportManager.Instance.CleanupPreviewFiles();
            this.filesysPreviewWatcher = new FileSystemWatcher();
            this.filesysPreviewWatcher.Path = LocalDirectories.FilePreviewDirectory;
            this.filesysPreviewWatcher.IncludeSubdirectories = false;
            this.filesysPreviewWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName;
            this.filesysPreviewWatcher.Filter = "*.*";
            this.filesysPreviewWatcher.SynchronizingObject = this;
            this.filesysPreviewWatcher.Created += onFilesysPreviewWatcherRaiseChanged;
            this.filesysPreviewWatcher.EnableRaisingEvents = true;
            
            ReportManager.Instance.CleanupMapFiles();
            this.mapWatcher = new FileSystemWatcher();
            this.mapWatcher.Path = LocalDirectories.MapsDirectory;
            this.mapWatcher.IncludeSubdirectories = false;
            this.mapWatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.FileName;
            this.mapWatcher.Filter = "*.*";
            this.mapWatcher.SynchronizingObject = this;
            this.mapWatcher.Created += onFilesysMapWatcherRaiseChanged;
            this.mapWatcher.EnableRaisingEvents = true;

            // 前回の保留分があるかもしれないので1秒後にディレクトリを検索してみる
            this.setFindTimer(1000);
        }

        /// <summary>
        /// ユーザーによるフォームを閉じる操作を無効化する。
        /// Task Barのサムネイルウインドウの×ボタン
        /// </summary>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Debug.WriteLine(string.Format("e.CloseReason={0}", e.CloseReason.ToString()), "MonitorForm");
            if (!this.exitApplication && e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Debug.WriteLine("canceled", "MonitorForm");

                if (ReportManager.Instance.Items.Count > 0)
                {
                    string message = "全てキャンセルしてもよろしいですか？";
                    var result = MessageBox.Show(message, Application.ProductName, 
                            MessageBoxButtons.YesNo, 
                            MessageBoxIcon.Question,
                            MessageBoxDefaultButton.Button1,
                            MessageBoxOptions.DefaultDesktopOnly);
                    this.activate();
                    if (result == DialogResult.Yes)
                    {
                        PrintJobManager.Instance.Cancel();
                        PreviewManager.Instance.CloseAll();
                        ReportManager.Instance.DeleteAll();
                        AbortPrinting.AbortSequence();
                    }
                }
                return;
            }
           
            // TODO:ここで通常時のウインドウ位置・サイズと最後が最大化状態かどうかを保存する
            Debug.WriteLine("OnFormClosing()", "MonitorForm");

            this.filesysWatcher.EnableRaisingEvents = false;
            this.findTimer.Enabled = false;
            this.updateTimer.Enabled = false;
            this.filesysPreviewWatcher.EnableRaisingEvents = false;
            this.findPreviewTimer.Enabled = false;
            this.mapWatcher.EnableRaisingEvents = false;
            this.findMapTimer.Enabled = false;

            // Viewerの後始末
            PreviewManager.Instance.CommandEventRelayHandler -= this.onViewerCommand;
            PreviewManager.Instance.CloseAll();
            ReportManager.Instance.Dispose();

            base.OnFormClosing(e);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);
            Debug.WriteLine("OnFormClosed()", "MonitorForm");
            ReportManager.Instance.CleanupOldBackupReportFiles();
            ReportManager.Instance.CleanupPreviewFiles();
            //削除しなくてもよいかも？
            //ReportManager.Instance.CleanupMapFiles();
            Application.Exit();
        }


        /// <summary>
        /// タイマの有効無効の設定。有効な場合はインターバル時間も指定すること。
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="interval"></param>
        private void setFindTimer(int interval)
        {
            if (interval > 0)
            {
                this.findTimer.Interval = interval;
                this.findTimer.Enabled = true;
            }
            else
            {
                this.findTimer.Enabled = false;
            }
        }

        /// <summary>
        /// タイマの有効無効の設定。有効な場合はインターバル時間も指定すること。
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="interval"></param>
        private void setFindPreviewTimer(int interval)
        {
            if (interval > 0)
            {
                this.findPreviewTimer.Interval = interval;
                this.findPreviewTimer.Enabled = true;
            }
            else
            {
                this.findPreviewTimer.Enabled = false;
            }
        }

        /// <summary>
        /// FilesystemWatcherからのRenameイベントハンドラ
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void onFilesysWatcherRaiseRenamed(Object source, RenamedEventArgs e)
        {
            if (this.filesysWatcher.EnableRaisingEvents)
            {
                if (!this.findTimer.Enabled)
                {
                    this.setFindTimer(50);
                }
            }
        }

        /// <summary>
        /// FilesystemWatcherからのChangedイベントハンドラ
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void onFilesysWatcherRaiseChanged(Object source, FileSystemEventArgs e)
        {
            if (this.filesysWatcher.EnableRaisingEvents)
            {
                if (!this.findTimer.Enabled)
                {
                    this.setFindTimer(50);
                }
            }
        }

        /// <summary>
        /// FilesystemWatcherからのChangedイベントハンドラ
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void onFilesysPreviewWatcherRaiseChanged(Object source, FileSystemEventArgs e)
        {
            if (this.filesysPreviewWatcher.EnableRaisingEvents)
            {
                if (!this.findPreviewTimer.Enabled)
                {
                    this.setFindPreviewTimer(50);
                }
            }
        }

        /// <summary>
        /// ダイアログ表示中にタイマーで再入するのをブロックするフラグ
        /// </summary>
        private bool blockFindPreviewTimerRecursive = false;

        /// <summary>
        /// タイマイベントハンドラ。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onFindPreviewTimer(object sender, EventArgs e)
        {
            if (!this.blockFindPreviewTimerRecursive)
            {
                this.blockFindPreviewTimerRecursive = true;
                this.findPreviewFileProc();
                this.blockFindPreviewTimerRecursive = false;
            }
        }

        /// <summary>
        /// ファイル検索処理
        /// </summary>
        private void findPreviewFileProc()
        {
            this.setFindPreviewTimer(0);

            ProcessStartHelper.startPreviewProcess();
        }

        /// <summary>
        /// ダイアログ表示中にタイマーで再入するのをブロックするフラグ
        /// </summary>
        private bool blockFindTimerRecursive = false;

        /// <summary>
        /// タイマイベントハンドラ。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onFindTimer(object sender, EventArgs e)
        {
            if (!this.blockFindTimerRecursive)
            {
                this.blockFindTimerRecursive = true;
                this.findFileProc();
                this.blockFindTimerRecursive = false;
            }
        }

        /// <summary>
        /// ファイル検索処理
        /// </summary>
        private void findFileProc()
        {
            this.setFindTimer(0);

            // 更新のチェック
            if (this.checkVerup())
            {
                return;
            }
            
            ProcessStartHelper.startProcess();
            ProcessStartHelper.startProcess2();

            // 出力ディレクトリ内のXPSファイルを検索
            bool continueFinding = ReportManager.Instance.FindFilesInPrintingDirectory();
            ReportManager.Instance.sortedItems();

            // リストボックスを更新
            this.updateReportInfoListBox();
            this.updateProgressStatus();

            // 検索継続ならタイマの再設定
            if (continueFinding)
            {
                this.setFindTimer(50);
            }

            if (this.printingSequenceNo == 0)
            {
                this.setPrintingProcTimer(1);
            }

            if (ReportManager.Instance.Items.Count == 0)
            {
                ReportManager.Instance.CleanupOldBackupReportFiles();
            }

        }

        /// <summary>
        /// 印刷帳票の一覧リストボックスを更新
        /// </summary>
        /// <returns></returns>
        private void updateReportInfoListBox()
        {
            // 現在のユーザー選択項目位置を保存
            int selIndex = this.ReportInfoListBox.SelectedIndex;
            int topIndex = this.ReportInfoListBox.TopIndex;

            // 再描画の抑制
            this.ReportInfoListBox.BeginUpdate();

            // リストボックスの項目を最新の帳票リストに入れ替える
            this.ReportInfoListBox.Items.Clear();
            // 印刷番号順になっていないためSerialNoでソートする
            var sortedItems = ReportManager.Instance.Items.OrderBy(o => o.SerialNo).ToArray();
            for (int i = 0; i < sortedItems.Count(); i++)
            {
                var repInfo = sortedItems[i];
                this.ReportInfoListBox.Items.Add(repInfo);
            }

            // 帳票リストボックスの選択項目の決定
            if (this.ReportInfoListBox.Items.Count > 0)
            {
                if (topIndex >= this.ReportInfoListBox.Items.Count)
                {
                    topIndex = this.ReportInfoListBox.Items.Count - 1;
                }
                if (topIndex < 0)
                {
                    topIndex = 0;
                }
                this.ReportInfoListBox.TopIndex = topIndex;
                
                // 末尾が選択されていて件数が減ったのなら
                if (selIndex >= this.ReportInfoListBox.Items.Count)
                {
                    selIndex = this.ReportInfoListBox.Items.Count - 1;
                }

                if (selIndex < 0)
                {
                    selIndex = 0;
                }

                this.ReportInfoListBox.SelectedIndex = selIndex;
            }

            // リストボックスを描画
            this.ReportInfoListBox.EndUpdate();

            // 選択帳票の表示を更新
            this.updateSelReportSettings();
        }

        private void setPrintingProcTimer(int interval = 1)
        {
            if (interval > 0)
            {
                this.updateTimer.Interval = interval;
                this.updateTimer.Enabled = true;
            }
            else
            {
                this.updateTimer.Enabled = false;
            }
        }

 
        /// <summary>
        /// ダイアログ表示中にタイマーで再入するのをブロックするフラグ
        /// </summary>
        private bool blockPrintingProcTimerRecursive = false;
        private void onPrintingProcTimer(object sender, EventArgs e)
        {
            if (!this.blockPrintingProcTimerRecursive)
            {
                this.blockPrintingProcTimerRecursive = true;
                this.printingProc();
                this.blockPrintingProcTimerRecursive = false;
            }
        }

        private int printingSequenceNo = 0;
        private autoPrintingMode autoPrintingMode = autoPrintingMode.Auto;
        private void printingProc()
        {
            // まずは再入防止にタイマーを切る
            this.setPrintingProcTimer(0);

            Debug.WriteLine(string.Format("printingProc() printingSequenceNo = {0}, autoPrintingMode = {1}"
                        , this.printingSequenceNo, this.autoPrintingMode));

            // シーケンス番号に応じた分岐処理
            switch (this.printingSequenceNo)
            {
                default:
                case 0: // 初期状態(非表示)
                    if (ReportManager.Instance.Items.Count > 0)
                    {
                        // 前回終了時のものが残っていたら自動動作したことにしてマニュアルモードにする
                        if (this.firstSelfFindFlag)
                        {
                            foreach (var r in ReportManager.Instance.Items)
                            {
                                r.IsActionAlready = true;
                            }
                            this.autoPrintingMode = autoPrintingMode.Manual;
                        }
                        this.printingSequenceNo = 1;
                    }
                    this.firstSelfFindFlag = false;
                    break;

                case 1: // 表示状態(印刷中なし)
                    if (ReportManager.Instance.Items.Count == 0)
                    {
                        if (this.autoPrintingMode == autoPrintingMode.All)
                        {
                            // 全印刷でまだ続きがありそうなら自動的に閉じない
                            if (AbortPrinting.CanAborting())
                            {
                                break; ;
                            }
                        }

                        this.autoPrintingMode = autoPrintingMode.Auto;
                        ReportManager.Instance.Clear();
                        PrintJobManager.Instance.Clear();
                        this.hide();
                        this.printingSequenceNo = 0;
                        break;
                    }

                    this.autoStartPrinting();

                    this.updateProgressStatus();

                    if (PrintJobManager.Instance.IsPrinting && !PrintJobManager.Instance.IsCompleted)
                    {
                        this.printingSequenceNo = 10;
                    }
                    break;

                case 10: // 印刷中
                    this.updateProgressStatus();
                    if (PrintJobManager.Instance.IsCompleted)
                    {
                        this.onPrintJobCompleted();
                        this.printingSequenceNo = 1;
                        this.updateProgressStatus();
                    }
                    break;
            }
            if (this.printingSequenceNo > 0)
            {
                this.setPrintingProcTimer(1000);
            }
        }

        /// <summary>
        /// 自動処理判定。先頭の帳票が印刷開始できればする。
        /// </summary>
        /// <returns>印刷開始したときはtrue</returns>
        private bool autoStartPrinting()
        {
            bool success = false;

            if (this.autoPrintingMode == autoPrintingMode.All)
            {
                this.show();
                if ((ReportManager.Instance.Items.Count > 0) && (PreviewManager.Instance.Items.Count == 0))
                {
                    var cursor = Cursor.Current;
                    Cursor.Current = Cursors.WaitCursor;
                    success = PrintJobManager.Instance.StartAuto(ReportManager.Instance.Items);
                    Cursor.Current = cursor;
                    foreach (var r in ReportManager.Instance.Items)
                    {
                        r.IsActionAlready = true;
                    }
                }
            }
            else
            {
                ReportPrintingInfo printCandidateRepInfo = null; // 印刷候補
                ReportPrintingInfo previwCandidateRepInfo = null; // 印刷候補
                ReportPrintingInfo printForceCandidateRepInfo = null; // 印刷候補

                foreach (var r in ReportManager.Instance.Items)
                {
                    if (r.IsActionAlready)
                    {
                        continue;
                    }

                    if (r.Require == ReportActionRequires.DirectPrint)
                    {
                        if (printCandidateRepInfo == null)
                        {
                            if (r.Settings != null)
                            {
                                r.Settings.Load();
                                if (!string.IsNullOrEmpty(r.Settings.PrinterName))
                                {
                                    r.IsActionAlready = true;
                                    printCandidateRepInfo = r;
                                }
                            }
                        }
                        if (printCandidateRepInfo == null)
                        {
                            this.autoPrintingMode = autoPrintingMode.Manual;
                        }
                    }
                    else if (r.Require == ReportActionRequires.ForceDirectPrint)
                    {
                        if (printForceCandidateRepInfo == null)
                        {
                            if (r.Settings != null)
                            {
                                r.IsActionAlready = true;
                                printForceCandidateRepInfo = r;

                            }
                        }
                        if (printForceCandidateRepInfo == null)
                        {
                            this.autoPrintingMode = autoPrintingMode.Manual;
                        }
                    }
                    else if (r.Require == ReportActionRequires.DirectPreview)
                    {
                        // 1回の判定では最初の候補だけを実際にプレビューし、他の候補は動作済みにする。
                        // (周期的に判定されるので次々とプレビューが開くのを防止するため）
                        r.IsActionAlready = true;
                        if (previwCandidateRepInfo == null)
                        {
                            if (this.autoPrintingMode == autoPrintingMode.Auto)
                            {
                                previwCandidateRepInfo = r;
                            }
                        }
                        this.autoPrintingMode = autoPrintingMode.Manual;
                    }
                    else
                    {
                        this.autoPrintingMode = autoPrintingMode.Manual;
                    }
                }

                if (this.autoPrintingMode != autoPrintingMode.Auto)
                {
                    this.show();
                    if (previwCandidateRepInfo != null)
                    {
                        this.preview(previwCandidateRepInfo);
                    }
                }

                if (printCandidateRepInfo != null)
                {
                    printCandidateRepInfo.Settings.Load();
                    if (!string.IsNullOrEmpty(printCandidateRepInfo.Settings.PrinterName))
                    {
                        this.print(true, printCandidateRepInfo, false);
                        return true;
                    }
                }

                if (printForceCandidateRepInfo != null)
                {
                    this.print(true, printForceCandidateRepInfo, false);
                    return true;
                }
            }
            return success;
        }

        /// <summary>
        /// 印刷ジョブ終了処理
        /// </summary>
        private void onPrintJobCompleted()
        {
            var job = PrintJobManager.Instance;
            if (!job.IsCompleted)
            {
                return ;
            }

            if (job.IsCancel || job.Error != null)
            {
                this.autoPrintingMode = autoPrintingMode.Manual;
            }

            var compList = new List<ReportPrintingInfo>();
            foreach (var repInfo in ReportManager.Instance.Items)
            {
                if (repInfo.Status == ReportInfoStatus.Printing)
                {
                    if (job.IsCancel || job.Error != null)
                    {
                        repInfo.Status = ReportInfoStatus.Waiting;
                    }
                    else
                    {
                        repInfo.Status = ReportInfoStatus.Completed;
                        compList.Add(repInfo);
                    }
                }
            }

            foreach (var repInfo in compList)
            {
                delete(true, repInfo);
            }

            job.Clear();
        }

        /// <summary>
        /// 表示
        /// </summary>
        /// <param name="force"></param>
        private void show()
        {
            if (!this.ShowInTaskbar)
            {
                // 将軍Rの表示位置取得
                RECT winRect = new RECT();
                GetWindowRect(GetForegroundWindow(), ref winRect);
                Rectangle rect = new Rectangle(winRect.left, winRect.top, 
                                               winRect.right - winRect.left, 
                                               winRect.bottom - winRect.top);

                // 将軍Rの中央に表示
                if (rect != Rectangle.Empty)
                {
                    this.StartPosition = FormStartPosition.Manual;
                    this.Location = new Point(rect.Location.X + (rect.Width - this.FormNormalBounds.Width) / 2,
                                              rect.Location.Y + (rect.Height - this.FormNormalBounds.Height) / 2);
                }
                this.activate();
            }
        }

        /// <summary>
        /// 前面表示に切り替え
        /// </summary>
        /// <param name="force"></param>
        private void activate()
        {
            // タスクバーに表示されていなければ表示状態を復元し前面に
            if (!this.ShowInTaskbar)
            {
                this.ShowInTaskbar = true;
                this.Show();
            }

            // 最小化状態なら通常状態に戻す
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = this.ShownWindowState;
            }

            if (this.WindowState == FormWindowState.Normal)
            {
                this.Size = this.FormNormalBounds.Size;
                this.Location = this.FormNormalBounds.Location;
            }

            // アクティブなプレビューがなければ前面に
            if (!PreviewManager.Instance.HasActiveSomeone())
            {
                this.Activate();
                this.BringToFront();
            }
        }

        /// <summary>
        /// 非表示に切り替え
        /// </summary>
        private void hide()
        {
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            this.Hide();
        }

        private void updateProgressStatus()
        {
            var buf = new StringBuilder();
            int totalProgressValue = 0;

            var mgr = ReportManager.Instance;
            var job = PrintJobManager.Instance;


            if (mgr.TotalFileCount > 0)
            {
                switch (this.autoPrintingMode)
                {
                    case autoPrintingMode.All:
                        buf.Append("<全印刷中> ");
                        break;
                    case autoPrintingMode.Auto:
//                        buf.Append("<自動印刷中> ");
                        break;
                    case autoPrintingMode.Manual:
                        break;
                    default:
                        break;
                }
                int total = mgr.TotalPageCount;
                int completed = mgr.PrintedPageCount;
                totalProgressValue = (total == 0 ? 0 : completed * 100 / total);
                if (job.IsPrinting)
                {
                    completed += job.PrintedPageCount;
                    totalProgressValue += (total == 0 ? 0 : job.Progress * job.PageCount / total);
                }
                
                buf.AppendFormat("トータル:{0}p  済:{1}p  残り:{2}p", total, completed, total - completed);
                buf.AppendLine();
            }

            if (job.IsPrinting)
            {
                if (job.Error != null)
                {
                    buf.AppendFormat("エラー/{0}", job.Error.Message);
                }
                else if (job.IsCancel)
                {
                    buf.Append("キャンセル");
                }
                else if (job.IsCompleted)
                {
                    buf.Append("印刷完了");
                }
                else
                {
                    buf.Append("印刷中");
                }
                buf.AppendFormat(":{0}", job.JobTitle);
                buf.AppendLine();
            }

            this.StatusLabel.Text = buf.ToString();
            this.progressBar1.Value = (totalProgressValue > this.progressBar1.Maximum) ? this.progressBar1.Maximum : totalProgressValue;

            Debug.WriteLine(this.StatusLabel.Text);
            Debug.WriteLine("progress = {0}", totalProgressValue);
        }

        private ReportPrintingInfo selectedRepInfo = null;
        private void updateSelReportSettings()
        {
            // リストボックスの選択項目から帳票を特定
            var selItem = this.ReportInfoListBox.SelectedItem;
            var repInfo = selItem as ReportPrintingInfo;

            // 前回と選択対象の帳票が変わってなければ何もしない
            if (this.selectedRepInfo == repInfo)
            {
                return;
            }
            this.selectedRepInfo = repInfo;

            // 前回選択されていた帳票用のサムネイルビットマップの後始末
            if (this.pictureBox1.Image != null)
            {
                var bitmap = this.pictureBox1.Image;
                this.pictureBox1.Image = null;
                bitmap.Dispose();
            }

            this.descriptionTextBox.Text = "";

            // 今回選択されているものがなければ終了
            if (repInfo == null)
            {
                return;
            }

            // サムネイルプレビューの表示
            this.pictureBox1.Image = repInfo.GetBitmap(0);

            // 印刷設定/帳票概要の表示
            string printerName = "";
            string description = "";
            if (repInfo.Settings != null)
            {
                repInfo.Settings.Load();
                printerName = repInfo.Settings.PrinterName;
                description = repInfo.Settings.Description;
            }

            var buf = new StringBuilder();

            System.Drawing.Printing.PrinterSettings printerSettings = null;

            if (string.IsNullOrEmpty(printerName) || string.IsNullOrEmpty(description))
            {
                printerSettings = (new System.Drawing.Printing.PrintDocument()).PrinterSettings;
            }

            if (string.IsNullOrEmpty(printerName) && printerSettings != null)
            {
                var printertSettings = (new System.Drawing.Printing.PrintDocument()).PrinterSettings;
                printerName = printertSettings.PrinterName;
            }

            if (string.IsNullOrEmpty(description) && printerSettings != null && !string.IsNullOrEmpty(printerName))
            {
                string duplex = "プリンタ規定";
                switch (printerSettings.Duplex)
                {
                    case System.Drawing.Printing.Duplex.Simplex: duplex = "なし"; break;
                    case System.Drawing.Printing.Duplex.Vertical: duplex = "有効(垂直方向)"; break;
                    case System.Drawing.Printing.Duplex.Horizontal: duplex = "有効(水平方向)"; break;
                }

                printerSettings.PrinterName = printerName; ;
                var ds = printerSettings.DefaultPageSettings;
                buf.AppendFormat(" 用紙サイズ : {0}\r\n", ds.PaperSize.PaperName);
                buf.AppendFormat("   用紙方向 : {0}\r\n", ds.Landscape ? "横" : "縦");
                buf.AppendFormat("   給紙方法 : {0}\r\n", ds.PaperSource.SourceName);
                buf.AppendFormat("   両面印刷 : {0}\r\n", duplex);
                buf.AppendFormat(" カラー印刷 : {0}\r\n", ds.Color ? "有効" : "無効(モノクロ印刷)");
 
                description = buf.ToString();
            }

            buf.Clear();
            buf.AppendFormat("     帳票名 : {0}\r\n", repInfo.PreviewTitle);
            buf.AppendFormat("   ページ数 : {0}\r\n", repInfo.PageCount);
            buf.AppendFormat("   作成日時 : {0}\r\n", repInfo.CreateTime.ToString("M月d日 HH:mm.ss"));
            buf.AppendFormat("   プリンタ : {0}\r\n", printerName);
            buf.AppendLine(description);
            this.descriptionTextBox.Text = buf.ToString();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            this.updateSelReportSettings();
        }

        /// <summary>
        /// 指定帳票をプレビュー
        /// </summary>
        /// <param name="repInfo"></param>
        private void preview(ReportPrintingInfo repInfo = null)
        {
            // 全て印刷中はプレビューさせない
            if (this.autoPrintingMode == autoPrintingMode.All)
            {
                return;
            }

            // 特定レポート指定無しならリストボックスで選択されているものを候補にする。
            if (repInfo == null)
            {
                repInfo = this.ReportInfoListBox.SelectedItem as ReportPrintingInfo;
            }

            if (repInfo != null && repInfo.Status != ReportInfoStatus.Printing)
            {
                // プレビュー表示した場合は手動モードに切り替え
                this.autoPrintingMode = autoPrintingMode.Manual;

                repInfo.IsActionAlready = true;
                PreviewManager.Instance.Open(repInfo.XpsPath, repInfo.PreviewTitle);
            }
        }

        /// <summary>
        /// プレビュー対象帳票の移動
        /// </summary>
        /// <param name="xpsViewer"></param>
        /// <param name="index"></param>
        private void movePrevew(XpsViewer xpsViewer, int index)
        {
            if (index >= 0 && index < ReportManager.Instance.Items.Count)
            {
                var repInfo = ReportManager.Instance.Items[index];
                if (repInfo.Status != ReportInfoStatus.Printing)
                {
                    repInfo.IsActionAlready = true;
                    PreviewManager.Instance.Replace(xpsViewer, repInfo.XpsPath, repInfo.PreviewTitle);
                }
            }
        }

        /// <summary>
        /// 指定帳票を印刷開始
        /// </summary>
        /// <param name="auto"></param>
        /// <param name="repInfo"></param>
        /// <param name="withPrintDialog"></param>
        private void print(bool auto = false, ReportPrintingInfo repInfo = null, bool withPrintDialog = true)
        {
            if (repInfo == null)
            {
                repInfo = this.ReportInfoListBox.SelectedItem as ReportPrintingInfo;
            }

            if (repInfo == null)
            {
                return;
            }

            repInfo.IsActionAlready = true;

            // プレビュー中ならビューワをいったん前面表示
            // (印刷ダイアログをビューワの上に表示させたい)
            var viewer = PreviewManager.Instance.FindItemByXpsFilePath(repInfo.XpsPath);
            if (viewer != null)
            {
                viewer.Show();
            }

            if (!auto)
            {
                this.autoPrintingMode = autoPrintingMode.Manual;
            }

            // 印刷ダイアログがモーダルになってくれないので擬似的にモーダルな状態を再現
            this.Enabled = false;
            PreviewManager.Instance.BlockPreviewing(true);


            if (repInfo.Settings != null)
            {
                repInfo.Settings.Load();
            }

            repInfo.Status = ReportInfoStatus.Printing;
            bool success = PrintJobManager.Instance.StartAt(repInfo, withPrintDialog);


            this.Enabled = true;
            PreviewManager.Instance.BlockPreviewing(false);

            if (viewer != null)
            {
                if (success)
                {
                    PreviewManager.Instance.Close(viewer);
                    this.activate();
                }
                else
                {
                    viewer.Show();
                }
            }
            this.updateProgressStatus();
        }

        private void onPrintEventHandler(object sender)
        {

            this.updateProgressStatus();
        }

        private void delete(bool printed = false, ReportPrintingInfo repInfo = null)
        {
            if (repInfo == null)
            {
                repInfo = this.ReportInfoListBox.SelectedItem as ReportPrintingInfo;
            }

            if (repInfo == null)
            {
                return;
            }

            // 印刷中の帳票なら削除させない
            if (repInfo.Status == ReportInfoStatus.Printing)
            {
                return;
            }

            // 手動操作での削除ならマニュアルモードに切り替え
            if (!printed)
            {
                this.autoPrintingMode = autoPrintingMode.Manual;
            }

            PreviewManager.Instance.Close(repInfo.XpsPath);
            ReportManager.Instance.Delete(repInfo, printed);

            this.updateReportInfoListBox();
            
            // 既に表示しているときだけ前面表示する
            // （自動印刷で非表示のときは表示しないままにする）
            if (this.ShowInTaskbar)
            {
                this.activate();
            }
        }

        private void allPrint()
        {
            PreviewManager.Instance.CloseAll();
            this.autoPrintingMode = autoPrintingMode.All;
        }

        private void stop()
        {
            this.autoPrintingMode = autoPrintingMode.Manual;
            PrintJobManager.Instance.Cancel();
        }

        private void onViewerCommand(object sender, string command)
        {
            if (!this.Enabled)
            {
                return ;
            }

            var xpsViewer = sender as XpsViewer;
            if (xpsViewer == null)
            {
                return;
            }

            var repInfo = ReportManager.Instance.FindItemByXpsFilePath(xpsViewer.XpsPath);
            if (repInfo == null)
            {
                return;
            }

            int index = ReportManager.Instance.Items.IndexOf(repInfo);

            switch (command)
            {
                case "Print":
                    this.print(false, repInfo);
                    break;

                case "Delete":
                    this.delete(false, repInfo);
                    break;

                case "PreviousContent":
                    this.movePrevew(xpsViewer, index - 1);
                    break;

                case "NextContent":
                    this.movePrevew(xpsViewer, index + 1);
                    break;

                case "Closed":
                    this.activate();
                    break;
            }
        }

        private void ReportInfoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.updateSelReportSettings();
        }

        private void ReportInfoListBox_DoubleClick(object sender, EventArgs e)
        {
            this.preview();
        }

        private void previewButton_Click(object sender, EventArgs e)
        {
            this.preview();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            this.delete(false);
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            this.print();
        }

        private void previewAllCloseButton_Click(object sender, EventArgs e)
        {
            PreviewManager.Instance.CloseAll();
        }

        private void printAllButton_Click(object sender, EventArgs e)
        {
            this.allPrint();
        }

        private void printStopButton_Click(object sender, EventArgs e)
        {
            this.stop();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ENTERキーでボタン押下にならないようにする
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                return false;
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// キー押下イベントハンドラ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            switch (e.KeyData)
            {
                case Keys.F5:
                case Keys.Enter:
                    this.preview();
                    break;

                case Keys.F6:
                    PreviewManager.Instance.CloseAll();
                    break;

                case Keys.Delete:
                case Keys.F7:
                    this.delete();
                    break;
                
                case Keys.F9:
                    this.print();
                    break;

                case Keys.F10:
                    this.allPrint();
                    break;

                case Keys.F11:
                    this.stop();
                    break;

                case Keys.F12:
                    this.Close();
                    break;
                
                default:
                    return;
            }
            e.Handled = true;
        }

/*        protected override bool ProcessDialogKey(Keys keyData)
        {
            if ((keyData & Keys.KeyCode) == Keys.Enter)
            {
                this.preview();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }
*/
        protected override void OnMove(EventArgs e)
        {
            base.OnMove(e);
            this.onBoundsChanged();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.onBoundsChanged();
        }

        private void onBoundsChanged()
        {
            Debug.WriteLine("{0}: {1}, {2}, {3}", this.Text, this.WindowState, this.Location.ToString(), this.Size.ToString());

            if (this.Location.X > 0 && this.Location.Y > 0)
            {
                this.FormNormalBounds.X = this.Location.X;
                this.FormNormalBounds.Y = this.Location.Y;
            }

            if (this.WindowState == FormWindowState.Normal)
            {
                this.FormNormalBounds.Size = this.Size;
            }

            if (this.WindowState != FormWindowState.Minimized)
            {
                this.ShownWindowState = this.WindowState;
            }
        }

        private void bitmapMaximizeButton_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Normal;
        }

        private void bitmapMinimizeButton_Click(object sender, EventArgs e)
        {
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private bool checkVerup()
        {
            // クラウド/クライアント側プロセスでなければ関係ない
            if (Common.Initializer.ProcessMode != ProcessMode.CloudClientSideProcess)
            {
                return false;
            }
            
            // 新しい更新があるかチェック
            int serverRev, clientRev;
            if (!Shougun.Printing.Verup.Launcher.ExistsVerupClientSide(out serverRev, out clientRev))
            {
                return false;
            }

            // ユーザーに問い合わせ
            string message = string.Format("新しいバージョンの{0}が見つかりました。\r\n現在のリビジョン={1}、新しいリビジョン={2}\r\n{0}を更新しますか？",
                    Application.ProductName, clientRev, serverRev);
            var result = MessageBox.Show(message, Application.ProductName, 
                                    MessageBoxButtons.YesNo, 
                                    MessageBoxIcon.Exclamation, 
                                    MessageBoxDefaultButton.Button1,
                                    MessageBoxOptions.DefaultDesktopOnly);
            if (result == DialogResult.Yes)
            {
                try
                {
                    // 更新プログラムを起動（自分は終了する）
                    if (Shougun.Printing.Verup.Launcher.LaunchVerupClientSide())
                    {
                        Application.Exit();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception: {0}", ex.ToString(), 0);
                }
            }

            // 更新が見つかったが更新しない場合の後始末
            Shougun.Printing.Verup.Launcher.CancelVerupClientSide();
            return false;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

        #region mapbox連携

        /// <summary>
        /// FilesystemWatcherからのChangedイベントハンドラ
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void onFilesysMapWatcherRaiseChanged(Object source, FileSystemEventArgs e)
        {
            if (this.mapWatcher.EnableRaisingEvents)
            {
                if (!this.findMapTimer.Enabled)
                {
                    this.setFindMapTimer(50);
                }
            }
        }

        /// <summary>
        /// タイマの有効無効の設定。有効な場合はインターバル時間も指定すること。
        /// </summary>
        /// <param name="enable"></param>
        /// <param name="interval"></param>
        private void setFindMapTimer(int interval)
        {
            if (interval > 0)
            {
                this.findMapTimer.Interval = interval;
                this.findMapTimer.Enabled = true;
            }
            else
            {
                this.findMapTimer.Enabled = false;
            }
        }

        /// <summary>
        /// ダイアログ表示中にタイマーで再入するのをブロックするフラグ
        /// </summary>
        private bool blockFindMapTimerRecursive = false;

        /// <summary>
        /// タイマイベントハンドラ。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void onFindMapTimer(object sender, EventArgs e)
        {
            if (!this.blockFindMapTimerRecursive)
            {
                this.blockFindMapTimerRecursive = true;
                this.findMapFileProc();
                this.blockFindMapTimerRecursive = false;
            }
        }

        /// <summary>
        /// ファイル検索処理
        /// </summary>
        private void findMapFileProc()
        {
            this.setFindMapTimer(0);

            ProcessStartHelper.startMapProcess();
        }

        #endregion
    }
}
