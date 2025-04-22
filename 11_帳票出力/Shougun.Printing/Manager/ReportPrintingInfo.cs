using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Shougun.Printing.Common;
using Shougun.Printing.Viewer;

namespace Shougun.Printing.Manager
{
    internal enum ReportInfoStatus
    {
        /// <summary>
        /// 帳票は印刷できない
        /// </summary>
        Unprintable,

        /// <summary>
        /// 帳票は待ち状態
        /// </summary>
        Waiting,

        /// <summary>
        /// 帳票はプレビュー中
        /// </summary>
        Previewing,

        /// <summary>
        /// 帳票は印刷中
        /// </summary>
        Printing,

        /// <summary>
        /// 帳票は印刷済み
        /// </summary>
        Completed,
    }

    /// <summary>
    /// 将軍からの要求
    /// </summary>
    internal enum ReportActionRequires
    {
        /// <summary>
        /// 帳票をプレビューするか印刷するかはユーザーの選択が必要
        /// </summary>
        UserSelect,

        /// <summary>
        /// 帳票は印刷が必要（ユーザー問い合わせなしで即印刷）
        /// </summary>
        DirectPrint,

        /// <summary>
        /// 帳票はプレビューが必要（ユーザー問い合わせなしで即プレビュー）
        /// </summary>
        DirectPreview,
        
        /// <summary>
        /// 帳票は印刷が必要（ユーザー問い合わせなしで即印刷）
        /// </summary>
        ForceDirectPrint,
    }

    internal class ReportPrintingInfo
    {
        /// <summary>
        /// 帳票の状態
        /// </summary>
        public string XpsPath { get; private set; }
        public string FileName { get; private set; }
        public DateTime CreateTime { get; private set; }
        public int SerialNo { get; private set; }
        public string ReportID { get; private set; }
        public ReportActionRequires Require { get; private set; }
        public bool IsActionAlready { get; set; }
        public bool IsMultiReport { get; private set; }
        public bool IsManifest { get; private set; }
        public string SettingsID { get; private set; }
        public string SettingsTitle { get; private set; }
        public string DocumentTitle { get; private set; }
        public string PreviewTitle { get; private set; }
        public int Copies { get; private set; }
        public ReportSettings Settings { get; private set; }
        public int PageCount { get; private set; }

        public ReportInfoStatus Status { get; set; }
        public Exception Error { get; set; }

        public override string ToString()
        {
            return this.PreviewTitle;
        }

        private ReportPrintingInfo()
        {
        }

        public ReportPrintingInfo(string path)
        {
            // データメンバ・プロパティの初期化
            this.XpsPath = path;
            this.FileName = Path.GetFileName(path);
            this.CreateTime = File.GetCreationTime(path);
            this.ReportID = "R000";
            this.SerialNo = 0;
            this.Require = ReportActionRequires.UserSelect;
            this.Copies = 1;
            this.Settings = null;
            this.Status = ReportInfoStatus.Waiting;
            this.Error = null;
            this.IsActionAlready = false;

            // ファイル名のパースと印刷設定の準備。
            // 例外が発生したら設定できたとこまで。
            try
            {
                this.parseFileName();
            }
            catch
            {
            }
            try
            {
                this.prepareSettings();
            }
            catch
            {
            }
            try
            {
                this.prepareXpsFile();
            }
            catch
            {
            }

            this.preparePreviewTitle();
        }

        /// <summary>
        /// ファイル名をパースして関連するプロパティを設定する
        /// </summary>
        private void parseFileName()
        {
            // (YYYYMMDDHHMMSS)_{連番}_R{帳票ID}_A{動作フラグ}_D{単票/連票フラグ}_C{部数}.xps
            // YYYYMMDDHHMMSS：西暦年月日時刻
            // 連番：1オリジン、アプリケーションが起動してからの連番。
            // 帳票ID：「R012」などの画面・帳票ID
            // 動作フラグ：1=ダイレクト印刷、2=プレビュー、3:印刷/プレビュー選択
            // 単票/連票フラグ：S=単票、M=連票 ※マニフェスト以外は常にS
            // 部数：デフォルトは１
            // ex) 20140528182659_12_R493_A1_DM_C3.xps
            // →R493直行用マニフェスト連票、直接印刷実行   


            var buf = new StringBuilder();
            char status = '0';
            string parseString = Path.GetFileNameWithoutExtension(this.FileName) + "_";
            int remainLength = parseString.Length;

            foreach (char c in parseString)
            {
                remainLength--;

                switch (status)
                {
                    // 先頭の作成日時
                    case '0':
                        if (c == '_')
                        {
                            this.CreateTime = DateTime.ParseExact(buf.ToString(), "yyyyMMddHHmmss", null);
                            buf.Clear();
                            status = '1';
                            continue;
                        }
                        break;
                    
                    // 連番
                    case '1':
                        if (c == '_')
                        {
                            this.SerialNo = int.Parse(buf.ToString());
                            buf.Clear();
                            status = '_';
                            continue;
                        }
                        break;

                    // セパレータ
                    case '_': 
                        switch (c)
                        {
                            case 'R':
                            case 'X':
                                status = c;
                                break; //帳票IDなら先頭のRとXRもIDに含める
                            case 'A':
                            case 'D':
                            case 'C':
                                status = c;
                                continue;
                        }
                        break;

                    // 拡張帳票ID
                    case 'X':
                        switch (c)
                        {
                            case 'R':
                                status = c;
                                break;
                        }
                        break;

                    // 帳票ID
                    case 'R':
                        // 帳票ID自体にアンダーバーが含まれてることを許す。
                        // ただし、末尾がセパレータの場合は削る。
                        if ((c != '_' && !char.IsDigit(c)) || remainLength == 0)
                        {
                            if (buf.Length > 0 && buf[buf.Length - 1] == '_')
                            {
                                buf.Remove(buf.Length - 1, 1);
                            }
                            this.ReportID = buf.ToString();
                            buf.Clear();
                            status = c;
                            continue;
                        }
                        break;
                    
                    // 動作フラグ
                    case 'A':
                        if (c == '_')
                        {
                            switch (buf.ToString())
                            {
                                case "1":
                                    this.Require = ReportActionRequires.DirectPrint;
                                    break;
                                case "2":
                                    this.Require = ReportActionRequires.DirectPreview;
                                    break;
                                case "4":
                                    this.Require = ReportActionRequires.ForceDirectPrint;
                                    break;
                            }
                            buf.Clear();
                            status = '_';
                            continue;
                        }
                        break;

                    // 単票/連票フラグ
                    case 'D':
                        if (c == '_')
                        {
                            this.IsMultiReport = buf.ToString().Equals("M");
                            buf.Clear();
                            status = '_';
                            continue;
                        }
                        break;
                    
                    // 部数
                    case 'C':
                        if (c == '_')
                        {
                            int value;
                            if (int.TryParse(buf.ToString(), out value))
                            {
                                if ( value >= 0)
                                {
                                    this.Copies = value;
                                }
                            }
                            buf.Clear();
                            status = '_';
                            continue;
                        }
                        break;
                }
                buf.Append(c);
            }
        }

        private void prepareSettings()
        {
            this.Settings = ReportSettingsManager.Instance.FindByReportId(this.ReportID, this.IsMultiReport);

            if (this.Settings != null)
            {
                var item = this.Settings.Item;
                this.SettingsID = item.SettingID;
                this.IsManifest = item.Type.Equals("Manifest");
                this.SettingsTitle = item.Caption;
                this.Settings.Load();
            }
        }

        private void prepareXpsFile()
        {

            // 情報取得のため、ファイルがまだ作成中だったら試行回数分リトライする
            // クラウドの場合、ファイルのコピーでこの状況が発生する。大きいファイルだとリトライ回数または待機秒数増やしたほうがいいかもしれない
            // アプリケーションの特性上、待機秒数は増やさずにリトライ回数を増やした方が得策か
            waitingAllowFileAccess();

            try
            {
                using (var xpsFile = new Shougun.Printing.Viewer.XpsFile(this.XpsPath))
                {
                    this.PageCount = xpsFile.PageCount;
                    this.DocumentTitle = xpsFile.Title;
                }
                return;
            }
            catch
            {
            }
            this.Status = ReportInfoStatus.Unprintable;
        }

        private void waitingAllowFileAccess()
        {
            return;
            //System.Diagnostics.Debug.WriteLine("waitingAllowFileAccess:start");
            Stream st = null;

            // 再試行回数
            int maxCount = 100;

            // 再試行までの間隔（ミリ秒）
            int waitTime = 1000;

            for (int i = 0; i < maxCount; i++)
            {
                try
                {
                    //System.Diagnostics.Debug.WriteLine("TryOpen: " + this.XpsPath);
                    if ((st = File.Open(this.XpsPath, FileMode.Open, FileAccess.Read, FileShare.None)) != null)
                    {
                        break;
                    }
                }
                catch
                {
                    System.Threading.Thread.Sleep(waitTime);
                    //System.Diagnostics.Debug.WriteLine("ReTry: " + i.ToString());
                }
            }
            st.Close();
            st = null;
            //System.Diagnostics.Debug.WriteLine("waitingAllowFileAccess:end");
        }

        private void preparePreviewTitle()
        {
            string previewTitle = "";
            if (!string.IsNullOrEmpty(this.DocumentTitle))
            {
                previewTitle = this.DocumentTitle;
            }
            else if (!string.IsNullOrEmpty(this.SettingsTitle))
            {
                previewTitle = this.SettingsTitle;
            }
            else
            {
                previewTitle = this.ReportID;
            }

            this.PreviewTitle = string.Format("[{0}] {1} ({2}ページ)", this.SerialNo, previewTitle, this.PageCount);
        }

        public System.Drawing.Bitmap GetBitmap(int page)
        {
            System.Drawing.Bitmap bitmap = null;
            if (this.Status == ReportInfoStatus.Waiting || this.Status == ReportInfoStatus.Previewing)
            {
                try
                {
                    using (var xpsFile = new XpsFile(this.XpsPath))
                    {
                        bitmap = xpsFile.GetBitmap(page);
                    }
                }
                catch
                {
                }
            }
            return bitmap;
        }
    }
}
