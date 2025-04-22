using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using r_framework.Dto;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuTeiki
{
    public partial class Form1 : Form
    {
        private StringBuilder argumentValues;

        #region load
        public Form1(StringBuilder argumentValues)
        {
            this.argumentValues = argumentValues;
            InitializeComponent();
        }

        static public void ShowMsgForm(StringBuilder s)
        {
            Form1 f = new Form1(s);
            // モーダル
            f.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.NaviAlertMsg.Text = argumentValues.ToString();
            this.bt_func12.Click += this.FormClose;
            this.bt_csv.Click += this.OutputCSV;
        }
        #endregion

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        private void OutputCSV(object sender, System.EventArgs e)
        {
            // CSV出力先選択
            var filePath = this.SelectCsvSaveFilePath();
            if (string.IsNullOrEmpty(filePath))
            {
                return;
            }

            // ファイル名(暫定)
            var fileName = "配車計画(NAVITIME)エラー内容" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

            string fullName = Path.Combine(filePath, fileName);

            // ファイルのエンコード(UTF-8)指定
            Encoding encoding = new UTF8Encoding(true);

            using (var sw = new StreamWriter(fullName, false, encoding))
            {
                // 書き込み
                sw.WriteLine(this.NaviAlertMsg.Text);
            }
        }

        /// <summary>
        /// CSVファイル出力場所選択
        /// </summary>
        /// <returns></returns>
        internal string SelectCsvSaveFilePath()
        {
            var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder();
            var title = "CSVファイルの出力場所を選択してください。";
            var initialPath = @"C:\Temp";
            var windowHandle = this.Handle;
            var isFileSelect = false;
            var isTerminalMode = SystemProperty.IsTerminalMode;
            var filePath = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);

            browserForFolder = null;

            return filePath;
        }
        #endregion

        #region 閉じる
        /// <summary>
        /// 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormClose(object sender, System.EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        #endregion
    }
}
