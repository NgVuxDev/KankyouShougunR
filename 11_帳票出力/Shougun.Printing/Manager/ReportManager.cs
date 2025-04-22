using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.IO;
using Shougun.Printing.Common;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace Shougun.Printing.Manager
{
    internal class ReportManager
    {
        public static ReportManager Instance
        {
            get
            {
                return ReportManager.singleton;
            }
        }
        private static ReportManager singleton = new ReportManager();

        public List<ReportPrintingInfo> Items { get; private set; }

        public int TotalPageCount { get; private set; }
        public int TotalFileCount { get; private set; }
        public int PrintedPageCount { get; set; }

        private ReportManager()
        {
            this.Items = new List<ReportPrintingInfo>();
            this.Clear();
        }

        public void Clear()
        {
            this.TotalPageCount = 0;
            this.TotalFileCount = 0;
            this.PrintedPageCount = 0;
            this.Items.Clear();
        }

        ///// <summary>
        ///// 地図のhtmlファイルのプロセスが閉じるのを検知したら
        ///// htmlファイルを削除する
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void p_Exited(object sender, EventArgs e)
        //{
        //    // プロセス終了時に通る
        //    //MessageBox.Show("プロセスが終了しました");
        //    DirectoryInfo dirInfo = new DirectoryInfo(LocalDirectories.PrintingDirectory);
        //    List<FileInfo> mapFile = new List<FileInfo>(dirInfo.GetFiles("*.html"));
        //    foreach (var map in mapFile)
        //    {
        //        try
        //        {
        //            //MessageBox.Show("htmlファイルを削除します");
        //            File.Delete(map.FullName);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //        }
        //        continue;
        //    }
        //}

        public enum KnownFolder
        {
            Downloads,
        }
        private static Guid FOLDERID_Downloads = new Guid("{374DE290-123F-4565-9164-39C4925E467B}");

        [DllImport("Shell32.dll", PreserveSig = false)]
        private static extern void SHGetKnownFolderPath(ref Guid refid, uint flags, IntPtr htoken, out IntPtr path);
        public static string GetKnownFolder(KnownFolder folder)
        {
            Guid id;
            switch (folder)
            {
                case KnownFolder.Downloads:
                default:
                    id = FOLDERID_Downloads;
                    break;
            }
            IntPtr path_ptr;
            SHGetKnownFolderPath(ref id, 0, IntPtr.Zero, out path_ptr);
            var path = Marshal.PtrToStringUni(path_ptr);
            Marshal.FreeCoTaskMem(path_ptr);
            return path;
        }

        public bool FindFilesInPrintingDirectory()
        {
            bool newFileFound = false;
            var items = new List<ReportPrintingInfo>();
            try
            {
                //var filesEnum = Directory.EnumerateFiles(LocalDirectories.PrintingDirectory, "*.xps");
                //var filesList = filesEnum.ToList();
                //filesList.Sort();

                // XPSファイル名の「YYYYMMDDHHMISS_」の次の数字を見てソートする
                // 例) 
                //    20150416121049_1_R000_A3_DS_C1.xps
                //    20150416121049_10_R000_A3_DS_C1.xps
                //    上記の場合、1と10を比較する

                DirectoryInfo dirInfo = new DirectoryInfo(LocalDirectories.PrintingDirectory);
                List<FileInfo> filesList = new List<FileInfo>(dirInfo.GetFiles("*.xps"));
                filesList.Sort(delegate(FileInfo x, FileInfo y)
                {
                    return int.Parse(x.Name.Substring(15, x.Name.Substring(15, x.Name.Length - 15).IndexOf('_')))
                                .CompareTo(int.Parse(y.Name.Substring(15, y.Name.Substring(15, y.Name.Length - 15).IndexOf('_'))));
                });

                foreach (var path in filesList)
                {
                    // #12205 削除フラグファイルがあるXPSファイルは削除を試みた上で無視する
                    string remmoveFlagFile = Path.ChangeExtension(path.FullName, "del");
                    if (File.Exists(remmoveFlagFile))
                    {
                        try
                        {
                            File.Delete(path.FullName);
                            File.Delete(remmoveFlagFile);
                        }
                        catch { }
                        continue;
                    }

                    var repInfo = this.FindItemByXpsFilePath(path.FullName);
                    if (repInfo == null && !newFileFound)
                    {
                        repInfo = new ReportPrintingInfo(path.FullName);
                        if (repInfo != null)
                        {
                            this.TotalFileCount++;
                            this.TotalPageCount += repInfo.PageCount;
                            newFileFound = true;
                        }
                    }

                    if (repInfo != null)
                    {
                        items.Add(repInfo);
                    }
                }
            }
            catch
            {
            }
            this.Items = items;
            return newFileFound;
        }

        public ReportPrintingInfo FindItemByXpsFilePath(string path)
        {
            if (this.Items != null)
            {
                foreach (var repInfo in this.Items)
                {
                    if (repInfo.XpsPath.CompareTo(path) == 0)
                    {
                        return repInfo;
                    }
                }
            }
            return null;
        }

        public bool Delete(int index, bool NoPrinted = false)
        {
            if (this.Items != null)
            {
                if (index >= 0 && index < this.Items.Count)
                {
                    var repInfo = this.Items[index];
                    return this.Delete(repInfo, NoPrinted);
                }
            }
            return false;
        }

        public bool Delete(ReportPrintingInfo repInfo, bool printed = false)
        {
            if (this.Items != null && repInfo.Status != ReportInfoStatus.Printing)
            {
                try
                {
                    var backupPath = Path.Combine(LocalDirectories.BackupDirectory, repInfo.FileName);
                    try
                    {
                        File.Copy(repInfo.XpsPath, backupPath, true);
                        File.Delete(repInfo.XpsPath);
                    }
                    catch
                    {
                        // #12205 XPSファイルの削除に失敗した場合は削除フラグファイルを作成しておく
                        string remmoveFlagFile = Path.ChangeExtension(repInfo.XpsPath, "del");
                        using (var file = new FileStream(remmoveFlagFile, FileMode.Create)) { }
                    }

                    this.Items.Remove(repInfo);

                    if (printed)
                    {
                        this.PrintedPageCount += repInfo.PageCount;
                    }
                    else
                    {
                        this.TotalFileCount--;
                        this.TotalPageCount -= repInfo.PageCount;
                    }
                    return true;
                }
                catch
                {
                }
            }
            return false;
        }

        public void DeleteAll()
        {
            if (this.Items != null)
            {
                var array = this.Items.ToArray();
                foreach (var repInfo in array)
                {
                    repInfo.Status = ReportInfoStatus.Waiting;
                    this.Delete(repInfo);
                }
            }
        }

        public void Dispose()
        {
            /*
            foreach (var repInfo in this.repInfoList)
            {
                repInfo.Dispose();
            }
            */
            this.Items.Clear();
        }

        public void CleanupOldBackupReportFiles()
        {
            try
            {
                var a = new TimeSpan(-7, 0, 0, 0);
                var xpsFiles = Directory.EnumerateFiles(LocalDirectories.BackupDirectory, "*.xps");
                foreach (var xpsFile in xpsFiles)
                {
                    var timeStamp = System.IO.File.GetLastWriteTime(xpsFile);
                    var span = timeStamp - System.DateTime.Now;
                    if (span.CompareTo(a) < 0)
                    {
                        System.IO.File.Delete(xpsFile);
                    }
                }
            }
            catch
            {
            }
        }

        public void CleanupPreviewFiles()
        {
            try
            {
                var files = Directory.EnumerateFiles(LocalDirectories.FilePreviewDirectory, "*.*");
                foreach (var file in files)
                {
                    System.IO.File.Delete(file);
                }
            }
            catch
            {
            }
        }

        public void CleanupMapFiles()
        {
            try
            {
                var files = Directory.EnumerateFiles(LocalDirectories.MapsDirectory, "*.*");
                foreach (var file in files)
                {
                    System.IO.File.Delete(file);
                }
            }
            catch
            {
            }
        }

        public void sortedItems()
        {
            var sorted = this.Items.OrderBy(o => o.SerialNo).ToArray();
            this.Items.Clear();
            for (int i = 0; i < sorted.Count(); i++)
            {
                var repInfo = sorted[i];
                this.Items.Add(repInfo);
            }
        }
    }
}
