using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Printing;
using System.Printing.Interop;
using System.Diagnostics;

namespace Shougun.Printing.Viewer
{
    public class XpsPrinter
    {
        public string Title { get; private set; }
        public string[] XpsPathArray { get; private set; }
        public string PrinterName { get; private set; }
        public byte[] Devmode { get; private set; }
        public int Copies { get; private set; }
        public string tmpPrintingDirectory { get; set; }
        
        public bool IsCompleted { get; private set; }
        public bool IsCancel { get; private set; }
        public Exception Error { get; private set; }
        public int Progress { get; private set; }
        public int PageCount { get; private set; }
        public int PrintedPageCount { get; private set; }
        private int progressNotifiedCount;
        private System.Windows.Controls.PrintDialog pDialog;

        public XpsPrinter()
        {
            this.initPropaties();
        }

        private void initPropaties()
        {
            this.Title = "";
            this.XpsPathArray = null;
            this.PrinterName = null;
            this.Devmode = null;
            this.Copies = 1;
            this.IsCompleted = false;
            this.IsCancel = false;
            this.Error = null;
            this.Progress = 0;
            this.PageCount = 0;
            this.PrintedPageCount = 0;
            this.progressNotifiedCount = 0;
            this.pDialog = null;
        }

        public bool Print(string title, string xpsPath, string printerName, int copies, byte [] devmode, bool withPrintDialog)
        {
            bool success = false;
            this.initPropaties();

            this.Title = title;
            this.XpsPathArray = new string[] { xpsPath };
            this.PrinterName = printerName;
            this.Devmode = devmode;
            this.Copies = (copies > 0) ? copies : 1;

            try
            {
                success = print(withPrintDialog);
                if (!success)
                {
                    this.IsCancel = true;
                    this.IsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                this.IsCompleted = true;
                this.Error = ex;
            }

            return success;
        }

        public bool Print(string title, string[] xpsPathArray, string printerName, byte[] devmode)
        {
            bool success = false;
            this.initPropaties();

            this.Title = title;
            this.XpsPathArray = xpsPathArray;
            this.PrinterName = printerName;
            this.Devmode = devmode;
            this.Copies = 1;
            try
            {
                success = print(false);
                if (!success)
                {
                    this.IsCancel = true;
                    this.IsCompleted = true;
                }
            }
            catch (Exception ex)
            {
                this.IsCompleted = true;
                this.Error = ex;
            }

            return success;
        }

        private FixedDocumentSequence margeXpsDocument(string[] xpsPathArray, bool isTwoSideDuplexing)
        {
            var margedFixedDocumentSequence = new FixedDocumentSequence();

            foreach (var xpsPath in xpsPathArray)
            {
                using (var xpsDocument = new XpsDocument(xpsPath, System.IO.FileAccess.Read))
                {
                    foreach (var docRef in xpsDocument.GetFixedDocumentSequence().References)
                    {
                        var docRefCopy = new DocumentReference();

                        docRefCopy.Source = docRef.Source;
                        (docRefCopy as System.Windows.Markup.IUriContext).BaseUri = (docRef as System.Windows.Markup.IUriContext).BaseUri;

                        margedFixedDocumentSequence.References.Add(docRefCopy);
                    }

                    // 両面印刷で奇数ページなら帳票単位で裏表でくっつかないように白紙ページを挿入する
                    if (isTwoSideDuplexing && margedFixedDocumentSequence.DocumentPaginator.PageCount % 2 != 0)
                    {
                        // 白紙ページの作成
                        var content = new PageContent();
                        content.Child = new FixedPage();

                        // 白紙ページをドキュメント末尾に追加
                        var references = margedFixedDocumentSequence.References;
                        var fixedDocument = references[references.Count-1].GetDocument(false);
                        fixedDocument.Pages.Add(content);
                    }

                    Shougun.Printing.Common.LocalDirectories.Info("読込完了: " + xpsPath);
                }
            }
            //// 統合したXPSファイルを再定義する
            //margedFixedDocumentSequence = ReGetXpsDocument(margedFixedDocumentSequence);

            return margedFixedDocumentSequence;
        }

        private FixedDocumentSequence ReGetXpsDocument(FixedDocumentSequence baseFixedDocumentSequence)
        {
            /* XPS保存ファイル名作成 */
            string fileName = "summarize.xps";

            // 保存パスは設定済みである
            string savePath = tmpPrintingDirectory;
            string fullXpsPath = System.IO.Path.Combine(savePath, fileName);

            // パッケージを作成
            System.IO.Packaging.Package package = System.IO.Packaging.Package.Open(fullXpsPath, System.IO.FileMode.Create);

            // パッケージと関連付ける XpsDocument オブジェクトを作成
            XpsDocument xpsDoc = new XpsDocument(package);

            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDoc);

            // FixedDocument オブジェクトをファイルに出力する
            writer.Write(baseFixedDocumentSequence.DocumentPaginator);

            // オブジェクトを解放
            xpsDoc.Close();
            package.Close();

            // 作り直したXPSファイルを元々のロジックで使用するオブジェクトに再定義
            var margedFixedDocumentSequence = new FixedDocumentSequence();

            // 作り直したXPSファイルを読み込む
            using (var xpsDocument = new XpsDocument(fullXpsPath, System.IO.FileAccess.Read))
            {
                foreach (var docRef in xpsDocument.GetFixedDocumentSequence().References)
                {
                    var docRefCopy = new DocumentReference();

                    docRefCopy.Source = docRef.Source;
                    (docRefCopy as System.Windows.Markup.IUriContext).BaseUri = (docRef as System.Windows.Markup.IUriContext).BaseUri;

                    margedFixedDocumentSequence.References.Add(docRefCopy);
                }
            }

            return margedFixedDocumentSequence;
        }
        
        private bool print(bool withPrintDialog)
        {

            PrintQueue printQueue = null;
            bool autoOrientation = true; // 自動用紙方向判定が必要
            bool isTwoSideDuplexing = false; // 両面印刷かどうかのフラグ

            // プリンタが指定されている場合
            if (!string.IsNullOrEmpty(this.PrinterName))
            {
                // プリンタサーバーの解決
                string serverName = null; // nullならローカルサーバー
                if (this.PrinterName.StartsWith(@"\\"))
                {
                    // \\で始まるなら先頭のUNC文字列を取り出し共有マシン名でプリントサーバーを作成
                    var separatorIndex = this.PrinterName.LastIndexOf('\\');
                    serverName = this.PrinterName.Substring(0, separatorIndex);
                }

                using (var printServer = new PrintServer(serverName))
                {
                    // プリントキューの解決
                    printQueue = printServer.GetPrintQueue(this.PrinterName);
    
                    // ドライバ依存の印刷設定(devmode)をPrintTicketに設定する
                    if (this.Devmode != null && this.Devmode.Length > 0)
                    {
                        using (var ptc = new PrintTicketConverter(this.PrinterName, 1))
                        {
                            var printTicket = ptc.ConvertDevModeToPrintTicket(this.Devmode);
                            var result = printQueue.MergeAndValidatePrintTicket(printTicket, null);
                            printQueue.UserPrintTicket = result.ValidatedPrintTicket;
                            autoOrientation = false;

                            // 予め両面印刷の設定がされているか判定する
                            var duplexing = printQueue.UserPrintTicket.Duplexing;
                            if (duplexing != null)
                            {
                                isTwoSideDuplexing = (duplexing == Duplexing.TwoSidedLongEdge
                                                    || duplexing == Duplexing.TwoSidedShortEdge);
                            }
                        }
                    }
                }
            }

            var fixedDocumentSequence = margeXpsDocument(this.XpsPathArray, isTwoSideDuplexing);
            var documentPaginator = fixedDocumentSequence.DocumentPaginator;

            // プリントキュー印刷ダイアログの作成
            this.pDialog = new System.Windows.Controls.PrintDialog();
            if (printQueue != null)
            {
                this.pDialog.PrintQueue = printQueue;
                this.pDialog.PrintTicket = printQueue.UserPrintTicket;
            }

            if (autoOrientation)
            {
                using (var page = documentPaginator.GetPage(0))
                {
                    if (page.Size.Height >= page.Size.Width)
                    {
                        this.pDialog.PrintTicket.PageOrientation = PageOrientation.Portrait;
                    }
                    else
                    {
                        this.pDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
                    }
                }
            }
            this.pDialog.PrintTicket.CopyCount = this.Copies;
            
            // 印刷ダイアログの表示
            if (withPrintDialog)
            {
                if (Application.Current != null)
                {
                    var window = Application.Current.MainWindow;
                    window.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width / 2 - 560 / 2;
                    window.Top = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height / 2 - 400 / 2;
                    window.Width = 0;
                    window.Height = 0;
                }

                this.pDialog.UserPageRangeEnabled = true;

                this.pDialog.MinPage = 1;
                this.pDialog.MaxPage = (uint)documentPaginator.PageCount;
                var dlgResult = pDialog.ShowDialog();

                if (dlgResult == null || !dlgResult.Value)
                {
                    // ユーザーキャンセルまたはシステム強制終了
                    return false;
                }

                if (pDialog.PageRangeSelection == System.Windows.Controls.PageRangeSelection.UserPages)
                {
                    fixedDocumentSequence = pageSelectXpsDocument(this.XpsPathArray, isTwoSideDuplexing, pDialog.PageRange);
                    documentPaginator = fixedDocumentSequence.DocumentPaginator;
                }
            }

            if (this.pDialog.PrintTicket.CopyCount != null)
            {
                int copies = (int)this.pDialog.PrintTicket.CopyCount;
                this.Copies = (copies > 0) ? copies : 1;
            }

            /* 非同期でやるならこっち*/
            this.Progress = 5;
            this.PageCount = documentPaginator.PageCount;
            xpsDocumentWriter = PrintQueue.CreateXpsDocumentWriter(pDialog.PrintQueue);
            xpsDocumentWriter.WritingCompleted +=  this.asyncPrintCompleted;
            xpsDocumentWriter.WritingCancelled +=  this.asyncPrintingCancelled;
            xpsDocumentWriter.WritingProgressChanged += this.asyncPrintingProgress;
            xpsDocumentWriter.WriteAsync(fixedDocumentSequence, this.pDialog.PrintTicket);
            return true;
        }

        private FixedDocumentSequence pageSelectXpsDocument(string[] xpsPathArray, bool isTwoSideDuplexing, System.Windows.Controls.PageRange pRange)
        {
            var pageSelectedFixedDocumentSequence = new FixedDocumentSequence();

            foreach (var xpsPath in xpsPathArray)
            {
                using (var xpsDocument = new XpsDocument(xpsPath, System.IO.FileAccess.Read))
                {
                    var fixedDocumentSequence = xpsDocument.GetFixedDocumentSequence();
                    var fdsReferences = fixedDocumentSequence.References;

                    FixedDocument docNew = new System.Windows.Documents.FixedDocument();
                    System.Windows.Markup.IAddChild pages = (System.Windows.Markup.IAddChild)docNew;

                    for (int p = pRange.PageFrom - 1; p < pRange.PageTo; p++)
                    {
                        PageContent pageContent;
                        pageContent = new System.Windows.Documents.PageContent();
                        pageContent.BeginInit();

                        //var page = this.fixedDocument.Pages[pageIndex]; 
                        var page = fdsReferences[fdsReferences.Count - 1].GetDocument(false).Pages[p];
                        ((System.Windows.Markup.IUriContext)pageContent).BaseUri = ((System.Windows.Markup.IUriContext)page).BaseUri;
                        pageContent.Source = page.Source;
                        pageContent.EndInit();
                        pages.AddChild(pageContent);
                    }

                    DocumentReference docRef = new DocumentReference();
                    docRef.SetDocument(docNew);
                    pageSelectedFixedDocumentSequence.References.Add(docRef);

                    // 両面印刷で奇数ページなら帳票単位で裏表でくっつかないように白紙ページを挿入する
                    if (isTwoSideDuplexing && pageSelectedFixedDocumentSequence.DocumentPaginator.PageCount % 2 != 0)
                    {
                        // 白紙ページの作成
                        var content = new PageContent();
                        content.Child = new FixedPage();

                        // 白紙ページをドキュメント末尾に追加
                        var references = pageSelectedFixedDocumentSequence.References;
                        var fixedDocument = references[references.Count - 1].GetDocument(false);
                        fixedDocument.Pages.Add(content);
                    }
                }
            }

            // ページ指定したXPSファイルを再定義する
            pageSelectedFixedDocumentSequence = ReGetXpsDocument(pageSelectedFixedDocumentSequence);

            return pageSelectedFixedDocumentSequence;
        }

        XpsDocumentWriter xpsDocumentWriter = null;

        public void Cancel()
        {
            if (this.pDialog != null && this.pDialog.PrintQueue != null)
            {
                try
                {
                    foreach (var job in this.pDialog.PrintQueue.GetPrintJobInfoCollection())
                    {
                        job.Cancel();
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("XpsPrinter.Cancel() Exception: {0}", ex);
                }
            }
        }

        private void asyncPrintCompleted(object sender, WritingCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.IsCancel = true;
                this.Progress = 0;
            }
            else if (e.Error != null)
            {
                this.Error = e.Error;
                this.Progress = 0;
            }
            else 
            {
                this.PrintedPageCount = this.PageCount;
                this.Progress = 100;
            }
            try
            {
                xpsDocumentWriter.WritingCompleted -= this.asyncPrintCompleted;
                xpsDocumentWriter.WritingCancelled -= this.asyncPrintingCancelled;
                xpsDocumentWriter.WritingProgressChanged -= this.asyncPrintingProgress;
                xpsDocumentWriter = null;
                clearPrintDialog();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("XpsPrinter.asyncPrintCompleted() Exception: {0}", ex);
            }
            this.pDialog = null;
            this.IsCompleted = true;
            this.onPrintEventHandler();
        }

        private void asyncPrintingProgress(object sender, WritingProgressChangedEventArgs e)
        {
            this.progressNotifiedCount++;

            int progress = 0;
            if (this.PageCount > 1)
            {
                if (e.Number > this.PrintedPageCount && e.Number <= this.PageCount)
                {
                    this.PrintedPageCount = e.Number;
                    progress = e.Number * 100 / this.PageCount;
                }
            }
            else
            {
                progress = (this.progressNotifiedCount * 100) / 4;
            }

            if (progress > 90)
            {
                progress = 90;
            }

            if (this.Progress < progress)
            {
                this.Progress = progress;
            }

            Debug.WriteLine("progressNotifiedCount = {0}, e.Number = {1}, Progress = {2}", this.progressNotifiedCount, e.Number, this.Progress);
 
            this.onPrintEventHandler();
        }

        private void asyncPrintingCancelled(object sender, WritingCancelledEventArgs e)
        {
            this.IsCompleted = true;
            this.IsCancel = true;
            this.Progress = 0;
            this.onPrintEventHandler();
        }
        
        public event PrinteEventHandler PrintEventHandler;
        private void onPrintEventHandler()
        {
            if (this.PrintEventHandler != null)
            {
                this.PrintEventHandler(this);
            }
        }

        private void clearPrintDialog()
        {
            if (this.pDialog != null)
            {
                if (this.pDialog.PrintQueue != null)
                {
                    this.pDialog.PrintQueue.Dispose();
                }
            }
            this.pDialog = null;
        }


        /// <summary>
        /// ユーザーページ指定用の拡張ページネータ。内部クラス
        /// </summary>
        private class DocumentPaginatorEx : System.Windows.Documents.DocumentPaginator
        {
            private System.Windows.Controls.PageRange pageRange;
            private System.Windows.Documents.DocumentPaginator paginator;

            public DocumentPaginatorEx(
              System.Windows.Documents.DocumentPaginator paginator,
              System.Windows.Controls.PageRange pageRange)
            {
                this.pageRange = pageRange;
                this.paginator = paginator;
            }

            public override System.Windows.Documents.DocumentPage GetPage(int pageNumber)
            {
                return paginator.GetPage(pageNumber + pageRange.PageFrom - 1);
            }

            public override bool IsPageCountValid
            {
                get { return true; }
            }

            public override int PageCount
            {
                get
                {
                    return pageRange.PageTo - pageRange.PageFrom + 1;
                }
            }

            public override Size PageSize
            {
                get { return paginator.PageSize; }
                set { paginator.PageSize = value; }
            }

            public override System.Windows.Documents.IDocumentPaginatorSource Source
            {
                get { return paginator.Source; }
            }
        }    
    }
}
