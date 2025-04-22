using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using C1.C1Zip;
using r_framework.Utility;

namespace Shougun.Core.Common
{
    /// <summary>
    /// LogFileDownLoaderが使用するロジッククラス
    /// </summary>
    public class SaveLogFilePopupLogic
    {
        private SaveLogFilePopup form;

        internal SaveLogFilePopupLogic(SaveLogFilePopup form)
        {
            this.form = form;
            this.LogFiles = new List<LogFileDto>();
        }

        /// <summary>
        /// ログファイル情報のリスト
        /// </summary>
        internal List<LogFileDto> LogFiles { get; set; }

        /// <summary>
        /// 選択カラムの名称
        /// </summary>
        internal readonly string SELECTED = "Selected";

        /// <summary>
        /// ファイル名カラムの名称
        /// </summary>
        internal readonly string FILE_NAME = "FileName";

        /// <summary>
        /// 作成日時カラムの名称
        /// </summary>
        internal readonly string CREATE_DATE = "CreateDate";

        /// <summary>
        /// 一度に表示する期間（日）
        /// </summary>
        internal int Term = 7;

        /// <summary>
        /// ログファイルを読み取り、データを取得する
        /// </summary>
        internal void LoadLogFiles()
        {
            var files = Directory.GetFiles(r_framework.Configuration.AppData.LogOutputDirectoryPath);

            foreach (var file in files)
            {
                this.LogFiles.Add(new LogFileDto(new FileInfo(file)));
            }
            this.LogFiles.Sort();
        }

        /// <summary>
        /// 日付を元にデータを抽出し、グリッドビューに表示します。
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        internal int CreateDGVforDate(DateTime date)
        {
            var end = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 999);
            var files = this.LogFiles.Where(n => n.CreateDate <= end &&
                n.CreateDate >= this.GetExecuteDate(date, false).AddDays(1)).ToArray();
            // 一旦クリアしてから行を追加する。
            this.form.customDataGridView1.Rows.Clear();

            for (int i = 0; i < files.Length; i++)
            {
                var file = this.LogFiles.FirstOrDefault(n => n.FileName == files[i].FileName);
                this.form.customDataGridView1.Rows.Add();
                this.form.customDataGridView1.Rows[i].Cells[this.SELECTED].Value = file.IsChecked;
                this.form.customDataGridView1.Rows[i].Cells[this.FILE_NAME].Value = file.FileName;
                this.form.customDataGridView1.Rows[i].Cells[this.CREATE_DATE].Value = file.CreateDate;
            }

            return files.Length;
        }

        /// <summary>
        /// 画面で選択されたログファイルを指定したパスにZipで保存します。
        /// </summary>
        /// <param name="path"></param>
        internal bool SaveLogForZip(string path)
        {
            LogUtility.DebugMethodStart(path);

            bool result = false;
            
            try
            {
                // ログフォルダにZIPファイルがある場合は削除する。
                var logFileDir = r_framework.Configuration.AppData.LogOutputDirectoryPath;
                var files = Directory.GetFiles(logFileDir, "*.zip");
                if (files.Length != 0)
                {
                    foreach (var file in files)
                    {
                        new FileInfo(file).Delete();
                    }
                }

                if (this.form != null)
                {
                    this.form.Cursor = Cursors.WaitCursor;
                }

                var list = this.LogFiles.Where(n => n.IsChecked).ToArray();

                if (list.Length == 0)
                {
                    return result;
                }

                // ログフォルダにZIPファイルを作成する。
                var zipFile = logFileDir + Path.GetFileName(path);
                var zip = new C1ZipFile();
                zip.CompressionLevel = CompressionLevelEnum.BestCompression;
                zip.Create(zipFile);
                zip.Open(zipFile);

                for (int i = 0; i < list.Length; i++)
                {
                    // 読み込みモードで開く
                    using (var fs = new FileStream(list[i].FilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        zip.Entries.Add(fs, list[i].FileName);
                    }
                }

                zip.Close();

                // ローカルにZIPファイルをコピーする。
                new FileInfo(zipFile).CopyTo(path, true);

                // ログフォルダからZIPファイルを削除する。
                new FileInfo(zipFile).Delete();

                result = true;

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal(ex);
                throw ex;
            }
            finally
            {
                if (this.form != null)
                {
                    this.form.Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// 受け取った日付から、表示日数後、もしくは前の日付を取得します。
        /// </summary>
        /// <param name="date">元の日付</param>
        /// <param name="isForward">true:後, false:前</param>
        /// <returns>取得した日付</returns>
        internal DateTime GetExecuteDate(DateTime date, bool isForward)
        {
            int term = isForward ? this.Term : -this.Term;
            date = date.AddDays(term);
            return date;
        }

        /// <summary>
        /// ベースファイルの絶対パスと対象ファイルの絶対パスを指定して対象ファイルの相対パスを取得します。
        /// </summary>
        /// <param name="basePath">ベースファイルパス</param>
        /// <param name="targetPath">対象ファイルパス</param>
        /// <returns>相対パス</returns>
        internal static string GetRelativePath(string basePath, string targetPath)
        {
            LogUtility.DebugMethodStart(basePath, targetPath);

            Uri uriRoot = new Uri(basePath);
            Uri uriPath = new Uri(uriRoot, targetPath);

            // 相対パスを取得
            var rtnPath = uriRoot.MakeRelative(uriPath);

            // デコード
            rtnPath = HttpUtility.UrlDecode(rtnPath);

            // 区切り文字が\ではなく/なので置換する
            rtnPath = rtnPath.Replace("/", "\\");

            LogUtility.DebugMethodEnd(rtnPath);
            return rtnPath;
        }

        /// <summary>
        /// ベースファイルの絶対パスと対象ファイルの相対パスを指定して対象ファイルの絶対パスを取得します。
        /// </summary>
        /// <param name="basePath"></param>
        /// <param name="targetPath"></param>
        /// <returns></returns>
        internal static string GetAbsolutePath(string basePath, string targetPath)
        {
            LogUtility.DebugMethodStart(basePath, targetPath);

            Uri uriRoot = new Uri(basePath);
            Uri uriPath = new Uri(uriRoot, targetPath);

            var rtnPath = uriPath.AbsolutePath;

            // デコード
            rtnPath = HttpUtility.UrlDecode(rtnPath);

            // 区切り文字が\ではなく/なので置換する
            rtnPath = rtnPath.Replace("/", "\\");

            LogUtility.DebugMethodEnd(rtnPath);
            return rtnPath;
        }
    }
}
