using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Shougun.Printing.Common;
using Shougun.Printing.Viewer;

namespace Shougun.Printing.Manager
{
    /// <summary>
    /// 印刷ジョブネージャ。印刷ジョブを管理する。
    /// </summary>
    internal class PrintJobManager
    {
        /// <summary>
        /// 印刷ジョブ管理クラスのインスタンス。シングルトン。
        /// </summary>
        public static PrintJobManager Instance
        {
            get
            {
                return PrintJobManager.singleton;
            }
        }
        private static PrintJobManager singleton = new PrintJobManager();

        public string JobTitle { get; private set; }
        public bool IsPrinting { get; private set; }
        public bool IsCompleted { get; private set; }
        public bool IsCancel { get; private set; }
        public Exception Error { get; private set; }
        public int PageCount { get; private set; }
        public int PrintedPageCount { get; private set; }
        public int Progress { get; private set; }
        private XpsPrinter xpsPrinter = null;
        
        /// <summary>
        /// プレビューマネージャのコンストラクタ
        /// </summary>
        private PrintJobManager()
        {
            this.Clear();
        }

        public void Clear()
        {
            this.IsPrinting = false;
            this.IsCompleted = false;
            this.IsCancel = false;
            this.Error = null;
            this.PageCount = 0;
            this.PrintedPageCount = 0;
            this.Progress = 0;
            this.xpsPrinter = null;
        }

        public bool StartAt(ReportPrintingInfo repInfo, bool withPrintDialog = false)
        {
            if (this.IsPrinting && !this.IsCompleted)
            {
                return false;
            }

            this.Clear();

            var jobTitle = repInfo.PreviewTitle;
            var RepArray = new ReportPrintingInfo[] {repInfo};

            bool success = start(jobTitle, RepArray, withPrintDialog);
            return success;
        }

        public bool StartAuto(List<ReportPrintingInfo> repInfoList)
        {
            if (this.IsPrinting && !this.IsCompleted)
            {
                return false;
            }

            this.Clear();

            // 帳票リスト先頭からまとめて印刷可能なものを数える
            ReportPrintingInfo prev = null;
            var printRepInfoList = new List<ReportPrintingInfo>(); // 印刷対象の帳票情報を格納
            foreach (var repInfo in repInfoList)
            {
                if (printRepInfoList.Count >= 20)
                {
                    // 20ファイルを上限とする
                    break;
                }

                if (repInfo.Status != ReportInfoStatus.Waiting)
                {
                    continue;
                }

                if (prev != null)
                {
                    // マニフェストまたは部数指定のあるものは連続印刷しない
                    if (repInfo.IsManifest || repInfo.Copies > 1)
                    {
                        break;
                    }

                    // 一つ前と設定が同じでなければ連続印刷しない
                    if (repInfo.SettingsID == null)
                    {
                        if (prev.SettingsID != null)
                        {
                            break;
                        }
                    }
                    else if (!repInfo.SettingsID.Equals(prev.SettingsID))
                    {
                        break;
                    }
                }
                prev = repInfo;
                printRepInfoList.Add(repInfo);
            }

            if (printRepInfoList.Count == 0)
            {
                return false;
            }

            if (printRepInfoList.Count == 1)
            {
                return StartAt(printRepInfoList[0], false);
            }

            var repArray = printRepInfoList.ToArray();
            var jobTitle = string.Format("帳票{0}～{1}", repArray.First().SerialNo, repArray.Last().SerialNo);

            bool success = start(jobTitle, repArray, false);

            return success;
        }

        private bool start(string jobTitle, ReportPrintingInfo[] repArray, bool withPrintDialog)
        {
            this.JobTitle = jobTitle;
            this.IsPrinting = true;

            this.xpsPrinter = new XpsPrinter();
            // パスだけ渡す
            this.xpsPrinter.tmpPrintingDirectory = LocalDirectories.PrintingTMPDirectory;

            bool success = false;

            try
            {
                var settings = repArray[0].Settings;
                string printerName = null;
                byte[] devmode = null;
                if (settings != null)
                {
                    printerName = settings.PrinterName;
                    devmode = settings.DevMode;
                }

                if (string.IsNullOrEmpty(printerName))
                {
                    printerName = (new System.Drawing.Printing.PrintDocument()).PrinterSettings.PrinterName;
                }

                this.xpsPrinter.PrintEventHandler += this.onPrintEventHandler;

                if (repArray.GetLength(0) == 1)
                {
                    var repInfo = repArray[0];
                    this.PageCount += repInfo.PageCount;
                    success = this.xpsPrinter.Print(JobTitle, repInfo.XpsPath, printerName, repInfo.Copies, devmode, withPrintDialog);
                }
                else
                {
                    var pathList = new List<string>();
                    foreach(var repInfo in repArray)
                    {
                        this.PageCount += repInfo.PageCount;
                        pathList.Add(repInfo.XpsPath);
                    }
                    success = this.xpsPrinter.Print(JobTitle, pathList.ToArray(), printerName, devmode);
                }

                if (success)
                {
                    foreach (var repInfo in repArray)
                    {
                        repInfo.Status = ReportInfoStatus.Printing;
                    }
                    this.onPrintEventHandler(xpsPrinter);
                }
                else
                {
                    this.IsCancel = true;
                    this.IsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                this.Error = ex;
                this.IsCompleted = true;
            }
            return success;
        }

        public void Cancel()
        {
            if (this.IsPrinting && !this.IsCompleted)
            {
                if (this.xpsPrinter != null)
                {
                    this.xpsPrinter.Cancel();
                }
            }
        }

        private void onPrintEventHandler(object sender)
        {
            var xpsPrinter = sender as XpsPrinter;
            if (xpsPrinter != null)
            {
                this.Error = xpsPrinter.Error;
                this.IsCompleted = xpsPrinter.IsCompleted;
                this.IsCancel = xpsPrinter.IsCancel;
                this.PrintedPageCount = xpsPrinter.PrintedPageCount;
                this.Progress = xpsPrinter.Progress;
            }
        }
    }
}
