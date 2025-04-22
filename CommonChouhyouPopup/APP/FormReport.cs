using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using r_framework.Const;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using r_framework.Dto;
using r_framework.Logic;
using C1.C1Preview.Export;
using System.Threading;
using System.Diagnostics;

namespace CommonChouhyouPopup.App
{
    #region - Class -

    /// <summary>帳票出力クラス</summary>
    /// <remarks>
    /// 
    /// </remarks>
    public class FormReport : IDisposable 
    {
        // 互換性のために残してある。
        public string Text { get ; set; }
        public string Caption { get; set; }

        #region - Fields -

        /// <summary>ウィンドウＩＤを保持するフィールド</summary>
        protected WINDOW_ID windowID;

        /// <summary>レポート情報数を保持するフィールド</summary>
        protected int reportInfoCount;

        /// <summary>レポート情報の基本クラスを保持するフィールド</summary>
        protected ReportInfoBase[] reportInfoBase = null;

        /// <summary>現在選択されている帳票インデックスを保持するフィールド</summary>
        protected int curentChouhyouIndex = 0;

        /// <summary>XPSファイル名用 - 帳票プロジェクトID</summary>
        private string projectId = string.Empty;
        /// <summary>XPSファイル名用 - デフォルト帳票プロジェクトID</summary>
        const string DEFAULT_PROJECT_ID = "R000";

        /* マニフェスト 単票/連票識別文字(マニ以外はSとする) */
        /// <summary>マニフェスト識別文字 - 単票</summary>
        const string MANIFEST_TAN = "S";
        /// <summary>マニフェスト識別文字 - 連票</summary>
        const string MANIFEST_REN = "M";

        /* XPS出力用拡張子 */
        /// <summary>XPS拡張子 - tmp</summary>
        const string EXTENSION_TMP = ".tmp";
        /// <summary>XPS拡張子 - xps</summary>
        const string EXTENSION_XPS = ".xps";
        /// <summary>extension PDF</summary>
        const string EXTENSION_PDF = ".pdf";

        /* XPSファイル名用 - 接頭文字 */
        /// <summary>XPSファイル接頭文字 - 動作</summary>
        const string XPS_FILE_ACTION = "A";
        /// <summary>XPSファイル接頭文字 - 単/連票</summary>
        const string XPS_FILE_REN_TAN = "D";
        /// <summary>XPSファイル接頭文字 - 部数</summary>
        const string XPS_FILE_COPIE = "C";
        #endregion - Fields -

        #region - Constructors -

        protected FormReport()
        {
        }
        
        #region 廃止予定
        /// <summary>[削除予定]Initializes a new instance of the <see cref="FormReport" /> class.</summary>
        /// <param name="reportInfoBase">レポート情報の基本クラス</param>
        public FormReport(ReportInfoBase reportInfoBase, WINDOW_ID windowID = WINDOW_ID.NONE)
            : this(new ReportInfoBase[] { reportInfoBase }, null, windowID)
        {
        }

        /// <summary>[削除予定]Initializes a new instance of the <see cref="FormReport" /> class.</summary>
        /// <param name="reportInfoBase">レポート情報の基本クラス</param>
        public FormReport(ReportInfoBase[] reportInfoBase, WINDOW_ID windowID = WINDOW_ID.NONE)
            : this(reportInfoBase, null, windowID)
        {
        }
        #endregion 廃止予定

        /// <summary>Initializes a new instance of the <see cref="FormReport" /> class.</summary>
        /// <param name="reportInfoBase">レポート情報の基本クラス</param>
        /// <param name="projectId">帳票プロジェクトID</param>
        public FormReport(ReportInfoBase reportInfoBase, string projectId, WINDOW_ID windowID = WINDOW_ID.NONE)
            : this(new ReportInfoBase[] { reportInfoBase }, projectId, windowID)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="FormReport" /> class.</summary>
        /// <param name="reportInfoBase">レポート情報の基本クラス</param>
        /// <param name="projectId">帳票プロジェクトID</param>
        public FormReport(ReportInfoBase[] reportInfoBase, string projectId, WINDOW_ID windowID = WINDOW_ID.NONE)
        {
            this.windowID = windowID;

            // 印刷完了の状態
            this.IsPrintComplete = false;

            this.reportInfoCount = reportInfoBase.Length;

            this.reportInfoBase = new ReportInfoBase[this.reportInfoCount];

            for (int i = 0; i < this.reportInfoCount; i++)
            {
                this.reportInfoBase[i] = reportInfoBase[i];

                // レンダリング中の画面操作を許可しない。このメソッドの予備元の画面イベントのハンドラに再入するのを防ぐため。
                // そうしないと例えば受入入力画面などの「登録」メソッド実行中に画面を閉じたりすることができてしまう。
                this.reportInfoBase[i].ComponentOneReport.DoEvents = false;
            }

            this.projectId = projectId;

            this.IsManifestReport = false;
            this.IsTanpyou = true;
            this.PrintInitAction = 1;
            this.Copie = 1;
        }

        #endregion - Constructors -

        #region - Properties -

        /// <summary>印刷完了の状態を保持するプロパティ</summary>
        /// <remarks>真の場合：印刷完了、偽の場合：印刷未完了</remarks>
        public bool IsPrintComplete { get; set; }

        /// <summary>エクセル出力する際のエクセル出力をするかＰＤＦ出力するかの状態を保持するプロパティ</summary>
        /// <remarks>真の場合：ＰＤＦで出力、偽の場合：Ｅｘｃｅｌで出力</remarks>
        public bool IsOutputPDF { get; set; }

        /// <summary>マニフェスト帳票かどうか</summary>
        public bool IsManifestReport { get; set; }

        /// <summary>単票/連票識別 - True:単票 False:連票</summary>
        public bool IsTanpyou { get; set; }

        /// <summary>XPS出力専用 - 印刷アプリ初期動作(1.直印刷、2.プレビュー、3.ポップアップ)</summary>
        public int PrintInitAction { get; set; }

        /// <summary>XPS出力専用 - 印刷部数(初期値1)</summary>
        public int Copie { get; set; }
        #endregion - Properties -

        #region - Methods -

        static C1.C1Preview.Export.XpsExporter xpsExporter = new XpsExporter();
        //20210607 hoang ref #151522 start
        //static C1.C1Preview.Export.PdfExporter pdfExporter = new PdfExporter();
        //20210607 hoang ref #151522 end

        /// <summary>
        /// プレビュー処理を実行する
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="async"></param>
        /// <param name="async2"></param>
        public void Preview(string filePath, bool async = true, bool async2 = false)
        {
            // 非同期モードなら非同期出力スレッドに要求して終了
            if (async && Logic.ContinuousPrinting.IsRunning)
            {
                Logic.AsyncXpsExportThread.AddRequest(this);
                return;
            }

            if (async2)
            {
                Logic.AsyncXpsExportThread.AddRequest(this);
                return;
            }

            try
            {
                // 保存パス取得
                string savePath = Logic.ContinuousPrinting.GetXpsPrintingDirectory();
                if (string.IsNullOrEmpty(savePath))
                {
                    // アラートはパス取得側で行うため無いなら中断
                    return;
                }

                savePath = savePath.Replace("Printing", "FilePreview");
                string fileName = Path.GetFileName(filePath);
                savePath = Path.Combine(savePath, fileName);

                if (r_framework.Configuration.AppConfig.IsTerminalMode)
                {
                    /* クラウドの場合TEMPフォルダで作成されてたファイルをクライアント側の最終出力先に移動 */
                    Logic.AsyncXpsTransferThread.AddRequest(filePath, savePath);
                }
                else
                {
                    // オンプレの場合は、そもそも呼ばれることは想定してない
                    File.Move(filePath, savePath);
                }
            }
            catch (Exception ex)
            {
                // アラート
                string msg = "プレビューに失敗しました。\r\n" + ex.Message;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShowError(msg);

                // ログ
                LogUtility.Fatal("Preview Error", ex);

                throw ex;
            }
        }

        /// <summary>
        /// XPSファイル形式で印刷処理を実行する
        /// </summary>
        public void PrintXPS(bool async = true,bool async2 = false)
        {
            if (this.reportInfoBase == null)
            {
                return;
            }

            // 非同期モードなら非同期出力スレッドに要求して終了
            if (async && Logic.ContinuousPrinting.IsRunning)
            {
                Logic.AsyncXpsExportThread.AddRequest(this);
                return;
            }

            if (async2)
            {
                Logic.AsyncXpsExportThread.AddRequest(this);
                return;
            }

            try
            {
                /* 
                 * XPSは拡張子を出力時「tmp」にし、完了時「xps」に変更する
                 * 印刷アプリ側が出力処理中に読み込むのを防止するため。
                 */

                // 保存パス取得
                string savePath = Logic.ContinuousPrinting.GetXpsPrintingDirectory();
                if (string.IsNullOrEmpty(savePath))
                {
                    // アラートはパス取得側で行うため無いなら中断
                    return;
                }

                // 余白設定
                string id = DEFAULT_PROJECT_ID;
                if (!string.IsNullOrEmpty(this.projectId))
                {
                    id = this.projectId;
                }
                var margins = Shougun.Printing.Common.ReportSettingsManager.GetMarginDelta(id, !IsTanpyou);

                // 年月日時分秒は出力中に変わる可能性があるため最初に取得した値に連番をたして使う
                DateTime dt = DateTime.Now;
                string dateTime = dt.ToString("yyyyMMddHHmmss");

                for (int i = 0; i < this.reportInfoCount; i++)
                {
                    // 余白設定
                    var layout = this.reportInfoBase[i].ComponentOneReport.Layout;
                    layout.MarginBottom = Math.Max(0, layout.MarginBottom + margins.Bottom);
                    layout.MarginLeft = Math.Max(0, layout.MarginLeft + margins.Left);
                    layout.MarginRight = Math.Max(0, layout.MarginRight + margins.Right);
                    layout.MarginTop = Math.Max(0, layout.MarginTop + margins.Top);

                    // 印刷ドキュメント
                    C1.C1Preview.C1PrintDocument printDoc = this.reportInfoBase[i].ComponentOneReport.C1Document;

                    // ***用紙サイズ、向きはテンプレートのまま出力***

                    /* ドキュメント情報設定 */
                    // タイトル
                    if (!string.IsNullOrEmpty(this.reportInfoBase[i].Title))
                    {
                        printDoc.DocumentInfo.Title = this.reportInfoBase[i].Title;
                    }
                    else if (!string.IsNullOrEmpty(this.Caption))
                    {
                        printDoc.DocumentInfo.Title = this.Caption;
                    }
                    else if (!string.IsNullOrEmpty(this.Text))
                    {
                        printDoc.DocumentInfo.Title = this.Text;
                    }

                    // 発行済み識別文言(コメントに設定)
                    printDoc.DocumentInfo.Comment = this.reportInfoBase[i].Hakkouzumi;

                    // XPS生成用のプリンタとしてXPS Document Writerを設定
                    // （これをしないと「通常使うプリンタ」によってレンダリングが異なってしまう）
                    printDoc.CreationDevice = C1.C1Preview.MeasurementDeviceEnum.Printer;
                    printDoc.CreationPrinterName = "Microsoft XPS Document Writer";

                    //20250414
                    //printDoc.CreationDevice = C1.C1Preview.MeasurementDeviceEnum.Screen;
                    //printDoc.CreationPrinterName = null;

                    /* XPS保存ファイル名作成 */
                    string fileName = this.CreateXPSFileName(dateTime);
                    string fullXpsPath = Path.Combine(savePath, fileName);

                    /* XPS出力用Expoter生成(保存ダイアログなし) */
                    xpsExporter.Document = this.reportInfoBase[i].ComponentOneReport;
                    xpsExporter.ShowOptions = false;

                    // 最終出力パス：拡張子「xps」
                    string xpsFileName = Path.ChangeExtension(fullXpsPath, EXTENSION_XPS);

                    if (r_framework.Configuration.AppConfig.IsTerminalMode)
                    {
                        /* クラウドの場合TEMPフォルダでtmpファイル作成後、クライアント側の最終出力先にリネームしつつ移動 */
                        string tmpDirectryName = Path.GetTempPath();
                        string fullTmpXpsPath = Path.Combine(tmpDirectryName, fileName);

                        xpsExporter.Export(fullTmpXpsPath);
                        Logic.AsyncXpsTransferThread.AddRequest(fullTmpXpsPath, xpsFileName);
                        // File.Move(fullTmpXpsPath, xpsFileName);
                    }
                    else
                    {
                        /* オンプレの場合tmpファイルを最終出力先に出力後リネーム */
                        xpsExporter.Export(fullXpsPath);
                        File.Move(fullXpsPath, xpsFileName);
                        // クラウドでのスレッド動作をオンプレ環境でテストしたいときはこちら
                        // Logic.AsyncXpsTransportThread.AddRequest(fullXpsPath, xpsFileName); 
                    }
                }

                // 印刷完了の状態
                this.IsPrintComplete = true;
            }
            catch (Exception ex)
            {
                // アラート
                string msg = "印刷に失敗しました。\r\n" + ex.Message;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShowError(msg);

                // ログ
                LogUtility.Fatal("Print Error", ex);

                throw ex;
            }
        }

        /// <summary>
        /// PDFファイル形式で印刷処理を実行する
        /// </summary>
        /// <param name="isKingakuTate">御見積書:True, 単価御見積書:False</param>
        public string ExportPDF(string reportName, string path)
        {
            if (this.reportInfoBase == null)
            {
                return string.Empty;
            }

            try
            {
                /* 
                 * PDFは拡張子を出力時「tmp」にし、完了時「xps」に変更する
                 * 印刷アプリ側が出力処理中に読み込むのを防止するため。
                 */

                // 余白設定
                string id = DEFAULT_PROJECT_ID;
                if (!string.IsNullOrEmpty(this.projectId))
                {
                    id = this.projectId;
                }
                var margins = Shougun.Printing.Common.ReportSettingsManager.GetMarginDelta(id, !IsTanpyou);

                // 年月日時分秒は出力中に変わる可能性があるため最初に取得した値に連番をたして使う
                DateTime dt = DateTime.Now;
                string dateTime = dt.ToString("yyyyMMddHHmmss");
                string pdfFileName = string.Empty;

                for (int i = 0; i < this.reportInfoCount; i++)
                {
                    // 余白設定
                    var layout = this.reportInfoBase[i].ComponentOneReport.Layout;
                    layout.MarginBottom = Math.Max(0, layout.MarginBottom + margins.Bottom);
                    layout.MarginLeft = Math.Max(0, layout.MarginLeft + margins.Left);
                    layout.MarginRight = Math.Max(0, layout.MarginRight + margins.Right);
                    layout.MarginTop = Math.Max(0, layout.MarginTop + margins.Top);

                    // 印刷ドキュメント
                    C1.C1Preview.C1PrintDocument printDoc = this.reportInfoBase[i].ComponentOneReport.C1Document;

                    // ***用紙サイズ、向きはテンプレートのまま出力***

                    /* ドキュメント情報設定 */
                    // タイトル
                    if (!string.IsNullOrEmpty(this.reportInfoBase[i].Title))
                    {
                        printDoc.DocumentInfo.Title = this.reportInfoBase[i].Title;
                    }
                    else if (!string.IsNullOrEmpty(this.Caption))
                    {
                        printDoc.DocumentInfo.Title = this.Caption;
                    }
                    else if (!string.IsNullOrEmpty(this.Text))
                    {
                        printDoc.DocumentInfo.Title = this.Text;
                    }

                    // 発行済み識別文言(コメントに設定)
                    printDoc.DocumentInfo.Comment = this.reportInfoBase[i].Hakkouzumi;

                    // PDF生成用のプリンタとしてXPS Document Writerを設定
                    // （これをしないと「通常使うプリンタ」によってレンダリングが異なってしまう）
                    //printDoc.CreationDevice = C1.C1Preview.MeasurementDeviceEnum.Printer;
                    //printDoc.CreationPrinterName = "Foxit Reader PDF Printer";

                    /* PDF保存ファイル名作成 */

                    /* PDF出力用Expoter生成(保存ダイアログなし) */
                    //20210607 hoang ref #151522 start
                    C1.C1Preview.Export.PdfExporter pdfExporter = new PdfExporter();
                    //20210607 hoang ref #151522 end
                    pdfExporter.Document = this.reportInfoBase[i].ComponentOneReport;
                    pdfExporter.ShowOptions = false;

                    // 最終出力パス：拡張子「xps」
                    string fileName = reportName;
                    if (String.IsNullOrEmpty(fileName))
                    {
                        this.CreateXPSFileName(dateTime);
                    }
                    //string tmpDirectryName = Path.GetTempPath();
                    string fullPdfPath = Path.Combine(path, fileName);
                    pdfFileName = Path.ChangeExtension(fullPdfPath, EXTENSION_PDF);
                    //pdfFileName = fullPdfPath + EXTENSION_PDF;

                    pdfExporter.Export(pdfFileName);

                    //if (!string.IsNullOrWhiteSpace(pdfFileName))
                    //{
                    //    System.Diagnostics.Process.Start(pdfFileName);
                    //}
                }

                return pdfFileName;
            }
            catch (Exception ex)
            {
                // アラート
                string msg = "印刷に失敗しました。\r\n" + ex.Message;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShowError(msg);

                // ログ
                LogUtility.Fatal("Print Error", ex);

                throw ex;
            }
        }

        /// <summary>
        /// XPSファイル形式で印刷処理を実行する
        /// 汎用帳票などの呼び出し元の変更が間に合わないため無理やりここで出力させる。
        /// </summary>
        public DialogResult ShowDialog()
        {
            // ここが呼ばれる場合はプレビュー表示
            this.PrintInitAction = 2;
            this.PrintXPS();
            return DialogResult.OK;
        }

        /// <summary>
        /// XPSファイル形式で印刷処理を実行する
        /// 汎用帳票などの呼び出し元の変更が間に合わないため無理やりここで出力させる。
        /// </summary>
        public void Show()
        {
            // ここが呼ばれる場合はポップアップ表示
            this.PrintInitAction = 2;
            this.PrintXPS();
        }



        /// <summary>
        /// XPSファイル名を作成します。
        /// 「yyyyMMddhhmmss_連番_Rxxx_印刷アプリ動作文字_単票/連票文字.xps」の形で命名します。
        /// </summary>
        /// <param name="dateTime">年月日時分秒の文字列</param>
        /// <returns>XPSファイル名</returns>
        private string CreateXPSFileName(string dateTime)
        {
            //LogUtility.DebugMethodStart(dateTime, no);
            LogUtility.Info("CreateXPSFileName Star");

            StringBuilder fileName = new StringBuilder();

            // 年月日時分秒
            fileName.Append(dateTime);

            /* 連番 */
            fileName.Append("_");
            // システムプロパティの連番をインクリ(ユーザスコープで連番)
            SystemProperty.XPSPrintInfo.PrintNo++;
            fileName.Append(SystemProperty.XPSPrintInfo.PrintNo);

            // プロジェクトID
            fileName.Append("_");
            if (!string.IsNullOrEmpty(this.projectId))
            {
                fileName.Append(this.projectId);
            }
            else
            {
                // 指定IDなしの場合はデフォルト値(R000)
                fileName.Append(DEFAULT_PROJECT_ID);
            }

            // 印刷アプリ初期動作
            fileName.Append("_");
            fileName.Append(XPS_FILE_ACTION);
            fileName.Append(this.PrintInitAction);

            // 単票/連表識別文字(マニ以外は単票)
            fileName.Append("_");
            fileName.Append(XPS_FILE_REN_TAN);
            if (this.IsManifestReport && !this.IsTanpyou)
            {
                fileName.Append(MANIFEST_REN);
            }
            else
            {
                fileName.Append(MANIFEST_TAN);
            }

            // 部数
            fileName.Append("_");
            fileName.Append(XPS_FILE_COPIE);
            fileName.Append(this.Copie);

            /* 
             * XPSは拡張子を出力時「tmp」にし、完了時「xps」に変更する
             * 印刷アプリ側が出力処理中に読み込むのを防止するため。
             */
            fileName.Append(EXTENSION_TMP);

            LogUtility.Info("CreateXPSFileName - FileName:" + fileName.ToString());
            //LogUtility.DebugMethodEnd();
            LogUtility.Info("CreateXPSFileName End");

            return fileName.ToString();
        }
        
        public void Dispose()
        {
        }

        #endregion - Methods -
    }

    #endregion - Class -
}
