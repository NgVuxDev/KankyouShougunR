using System;
using System.Text;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using System.Collections.Generic;

namespace Shougun.Printing.Common
{
    /// <summary>
    /// 将軍R印刷管理/帳票設定内容クラス
    /// 帳票種類ごとのユーザーの設定内容を保持する。
    /// 保存ファイルからの設定内容の読込/保存も行う。
    /// </summary>
    public class ReportSettings
    {
        private DateTime updateTime;

        /// <summary>
        /// 項目定義情報（IDや帳票設定キャプションなど）
        /// </summary>
        public ReportSettingsItem Item { get; private set; }

        /// <summary>
        /// プリンタ名（表示用）
        /// </summary>
        private string printerName = "";
        public string PrinterName 
        {
            get
            {
                return this.printerName;
            }
            set
            {
                this.printerName = value;
                this.Description = "";
                this.devMode = null;
                this.HasChanged = true;
            }
        }

        /// <summary>
        /// DevMode
        /// </summary>
        private Byte[] devMode = null;
        public Byte[] DevMode
        {
            get
            {
                return this.devMode;
            }
            set
            {
                this.devMode = value;
                this.Description = "";
                this.HasChanged = true;
            }
        }


        /// <summary>
        /// 設定内容概要（表示用）
        /// </summary>
        private string description = "";
        public string Description 
        { 
            get
            {
                if (string.IsNullOrEmpty(this.description) && !string.IsNullOrEmpty(this.PrinterName))
                {
                    string duplex = "プリンタ規定";
                    switch (this.PrinterSettings.Duplex)
                    {
                        case System.Drawing.Printing.Duplex.Simplex: duplex = "なし"; break;
                        case System.Drawing.Printing.Duplex.Vertical: duplex = "有効(垂直方向)"; break;
                        case System.Drawing.Printing.Duplex.Horizontal: duplex = "有効(水平方向)"; break;
                    }

                    var buf = new StringBuilder();
                    var ds = this.PrinterSettings.DefaultPageSettings;

                    buf.AppendFormat(" 用紙サイズ : {0}\r\n", ds.PaperSize.PaperName);
                    buf.AppendFormat("   給紙方法 : {0}\r\n", ds.PaperSource.SourceName);
                    buf.AppendFormat("   用紙方向 : {0}\r\n", ds.Landscape ? "横" : "縦");
                    buf.AppendFormat("   両面印刷 : {0}\r\n", duplex);
                    buf.AppendFormat(" カラー印刷 : {0}\r\n", ds.Color ? "有効" : "無効(モノクロ印刷)");
                    this.description = buf.ToString();
                }
                return this.description;
            }
            set
            {
                this.description = "";
            }
        }

        private Margins margins;
        public Margins Margins 
        {
            get
            {
                return this.margins;
            }
            set
            {
                this.margins = value;
                this.HasChanged = true;
            }
        }

        /// <summary>
        /// 帳票設定内容に変更があったかどうか
        /// </summary>
        public bool HasChanged { get; set; }

        /// <summary>
        /// 引数なしコンストラクタ。使用禁止
        /// </summary>
        private ReportSettings()
        {
        }

        /// <summary>
        /// コンストラクタ。帳票設定項目を指定する。
        /// </summary>
        /// <param name="item"></param>
        public ReportSettings(ReportSettingsItem item)
        {
            this.Item = item;
            this.margins = new Shougun.Printing.Common.Margins();
            this.HasChanged = false;
        }

        /// <summary>
        /// 設定ファイル保存パス（設定個別）
        /// </summary>
        public string getSettingsStoreFilePath()
        {
            string directory = "";
            if (Initializer.ProcessMode == ProcessMode.CloudServerSideProcess)
            {
                directory = ServerSettings.GetServerSettingsDirectory();
            }
            else
            {
                directory = LocalDirectories.SettingsDirectory;
            }
            if (!string.IsNullOrEmpty(directory))
            {
                string fileName = string.Format("ReportSettings_{0}", this.Item.SettingID);
                return Path.Combine(directory, fileName);
            }
            return null;
        }


        public PrinterSettings PrinterSettings
        {
            get
            {
                var printerSettings = (new PrintDocument()).PrinterSettings;
                printerSettings.PrinterName = this.PrinterName;
                if (this.DevMode != null)
                {
                    IntPtr pDevMode = IntPtr.Zero;
                    try
                    {
                        pDevMode = Marshal.AllocHGlobal(DevMode.Length);
                        Marshal.Copy(DevMode, 0, pDevMode, DevMode.Length);
                        printerSettings.SetHdevmode(pDevMode);
                    }
                    catch
                    {
                    }
                    finally
                    {
                        if (pDevMode != IntPtr.Zero)
                        {
                            Marshal.FreeHGlobal(pDevMode);
                        }
                    }
                }

                return printerSettings;
            }
        }

        /// <summary>
        /// 帳票設定のファイル保存
        /// HasChangedがfalseになる。
        /// </summary>
        public bool Save()
        {
            LastError.Clear();
            var path = this.getSettingsStoreFilePath();
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }

            try
            {
                var base64devmode = "";
                if (devMode != null&& devMode.Length > 0)
                {
                    base64devmode = Convert.ToBase64String(devMode, 0, devMode.Length);
                }
                
                // ファイルオープン
                using (var sw = new StreamWriter(path, false, Encoding.UTF8))
                {
                    sw.WriteLine("[PrinterName]");
                    sw.WriteLine("\"{0}\"", this.PrinterName);
                    sw.WriteLine("[DocumentProperties]");
                    sw.WriteLine(base64devmode);
                    sw.WriteLine("[Description]");
                    sw.WriteLine(this.Description);
                    sw.WriteLine("[Margin]");
                    sw.WriteLine(this.Margins.ToString());
                }
                this.HasChanged = false;
                return true;
            }
            catch(Exception ex)
            {
                LastError.Exception = ex;
            }
            return false;
        }

        /// <summary>
        /// 帳票設定のファイル読み込み
        /// HasChangedがfalseになる。
        /// </summary>
        public bool Load()
        {
            LastError.Clear();
            var path = this.getSettingsStoreFilePath();
            if (string.IsNullOrEmpty(path))
            {
                return false;
            }
            
            if (!this.HasChanged && File.Exists(path) && this.updateTime != null)
            {
                var time = File.GetLastWriteTime(path);
                if (this.updateTime.Equals(time))
                {
                    return true;
                }
            }

            this.printerName = "";
            this.devMode = null;
            this.Description = "";
            this.HasChanged = false;

            if (!File.Exists(path))
            {
                return false;
            }

            try
            {
                string printerName = "";
                string base64devmode = "";
                string margin = "0,0,0,0";

                using (var reader = new StreamReader(path, Encoding.UTF8))
                {
                    string line;
                    string prev = "";
                    while ((line = reader.ReadLine()) != null)
                    {
                        if (prev.Equals("[PrinterName]"))
                        {
                            printerName = line.Trim().Trim('\"');
                        }
                        if (prev.Equals("[DocumentProperties]"))
                        {
                            base64devmode = line.Trim();
                        }
                        if (prev.Equals("[Margin]"))
                        {
                            margin = line.Trim();
                        }
                        prev = line;
                    }
                }

                this.printerName = printerName;
                if (base64devmode.Length > 0)
                {
                    this.devMode = Convert.FromBase64String(base64devmode);
                }

                this.margins = Margins.Parse(margin);

                this.updateTime = File.GetLastWriteTime(path);

                return true;
            }
            catch (Exception ex)
            {
                LastError.Exception = ex;
            }
            finally
            {
            }
            return false;
        }
    }
}
