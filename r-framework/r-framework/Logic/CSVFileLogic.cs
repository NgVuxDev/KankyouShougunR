using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.CustomControl;
using r_framework.Dto;
using r_framework.Utility;

namespace r_framework.Logic
{
    /// <summary>
    /// 明細部分からCSVファイルを出力するクラス
    /// </summary>
    public class CSVFileLogic
    {
        /// <summary>
        /// 出力対象のMultiRow
        /// </summary>
        public GcCustomMultiRow Detail { get; set; }

        /// <summary>
        /// 出力対象のDataGridView
        /// </summary>
        public CustomDataGridView DataGridVew { get; set; }

        /// <summary>
        /// MultiRowを左上から並び替えたもの
        /// </summary>
        public List<MultiRowLocationDto> MultirowLocation { get; set; }

        /// <summary>
        /// 出力ファイル名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// ヘッダー出力フラグ
        /// </summary>
        public bool headerOutputFlag { get; set; }

        /// <summary>
        /// 指定出力対象項目(列)
        /// </summary>
        /// <remarks>
        /// 指定方法はCsvUtility.CsvColumns.CreateCustomColumns()を参照
        /// </remarks>
        public CsvUtility.CsvColumns CsvColumns { get; set; }

        /// <summary>
        /// 指定出力対象各項目の出力フォーマット
        /// </summary>
        /// <remarks>
        /// 指定方法はCsvUtility.CsvFormats.CreateCustomFormats()を参照
        /// </remarks>
        public CsvUtility.CsvFormats CsvFormats { get; set; }

        /// <summary>
        /// 指定出力項目のヘッダ表示文字
        /// </summary>
        /// <remarks>
        /// 指定方法はCsvUtility.CsvHeaders.CreateCustomHeaders()を参照
        /// </remarks>
        public CsvUtility.CsvHeaders CsvHeaders { get; set; }

        /// <summary>
        /// 明細情報からCSVファイル情報を生成するメソッド
        /// </summary>
        /// <param name="outputStream">出力先のStream</param>
        private void OutputCSVFile(StreamWriter outputStream)
        {
            if (headerOutputFlag)
            {
                this.OutputCSVHeader(outputStream);
            }

            //レコードを書き込む
            foreach (var row in Detail.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }
                List<string> csvData = new List<string>();
                foreach (var dto in MultirowLocation)
                {
                    var index = dto.Cells.CellIndex;

                    //セルが非表示の場合にはCSVに出力しない
                    if (!row[index].Visible)
                    {
                        continue;
                    }

                    //フィールドの取得
                    string field = string.Empty;
                    if (row[index].Value != null)
                    {
                        field = row[index].Value.ToString();
                    }

                    //セルがGcCustomDataTimeの場合、フォーマットする
                    if (row[index] is GcCustomDataTimePicker)
                    {
                        if (!string.IsNullOrWhiteSpace(field))
                        {
                            field = string.Format("{0:" + ((GcCustomDataTimePicker)row[index]).CustomFormat + "}", DateTime.Parse(field));
                        }
                    }

                    //"で囲む必要があるか調べる
                    if (field.IndexOf('"') > -1 ||
                        field.IndexOf(',') > -1 ||
                        field.IndexOf('\r') > -1 ||
                        field.IndexOf('\n') > -1 ||
                        field.StartsWith(" ") || field.StartsWith("\t") ||
                        field.EndsWith(" ") || field.EndsWith("\t"))
                    {
                        if (field.IndexOf('"') > -1)
                        {
                            //"を""とする
                            field = field.Replace("\"", "\"\"");
                        }
                        field = "\"" + field + "\"";
                    }
                    //フィールドを書き込む
                    csvData.Add(field);
                }
                //改行する
                outputStream.Write(String.Join(",", csvData.ToArray()) + "\r\n");
            }
        }

        /// <summary>
        /// 明細情報からヘッダー情報を生成する処理
        /// </summary>
        /// <param name="outputStream"></param>
        private void OutputCSVHeader(StreamWriter outputStream)
        {
            List<string> csvHeader = new List<string>();
            foreach (var dto in MultirowLocation)
            {
                //フィールドの取得
                string field = dto.Cells.Value.ToString();
                //"で囲む必要があるか調べる
                if (field.IndexOf('"') > -1 ||
                    field.IndexOf(',') > -1 ||
                    field.IndexOf('\r') > -1 ||
                    field.IndexOf('\n') > -1 ||
                    field.StartsWith(" ") || field.StartsWith("\t") ||
                    field.EndsWith(" ") || field.EndsWith("\t"))
                {
                    if (field.IndexOf('"') > -1)
                    {
                        //"を""とする
                        field = field.Replace("\"", "\"\"");
                    }
                    field = "\"" + field + "\"";
                }
                //フィールドを書き込む
                csvHeader.Add(field);
            }
            //改行する
            outputStream.Write(String.Join(",", csvHeader.ToArray()) + "\r\n");
        }

        /// <summary>
        /// CSVファイルを出力します
        /// </summary>
        /// <param name="form">呼び出し元フォーム</param>
        public void CreateCSVFile(SuperForm form)
        {
            if (new CsvUtility(this.Detail, form, string.Empty, this.CsvColumns, this.CsvFormats, this.CsvHeaders, this.headerOutputFlag, false).Output())
            {
                MessageBox.Show("出力が完了しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// DataGridVewの明細部分からCSVファイルの出力情報を生成する処理
        /// </summary>
        /// <param name="outputStream">出力先のStream</param>
        private void OutputCSVFileForDataGrid(StreamWriter outputStream)
        {
            if (headerOutputFlag)
            {
                this.OutputCSVHeaderForDataGrid(outputStream);
            }

            //レコードを書き込む
            foreach (DataGridViewRow row in DataGridVew.Rows)
            {
                foreach (ICustomDataGridControl cell in row.Cells)
                {
                    //フィールドの取得
                    string field = cell.GetResultText();
                    //"で囲む必要があるか調べる
                    if (field.IndexOf('"') > -1 ||
                        field.IndexOf(',') > -1 ||
                        field.IndexOf('\r') > -1 ||
                        field.IndexOf('\n') > -1 ||
                        field.StartsWith(" ") || field.StartsWith("\t") ||
                        field.EndsWith(" ") || field.EndsWith("\t"))
                    {
                        if (field.IndexOf('"') > -1)
                        {
                            //"を""とする
                            field = field.Replace("\"", "\"\"");
                        }
                        field = "\"" + field + "\"";
                    }
                    //フィールドを書き込む
                    outputStream.Write(field);
                    outputStream.Write(',');
                }
                //改行する
                outputStream.Write("\r\n");
            }
        }

        /// <summary>
        /// DataGridVewの明細部分からCSVファイルのヘッダー出力情報を生成する処理
        /// </summary>
        /// <param name="outputStream"></param>
        private void OutputCSVHeaderForDataGrid(StreamWriter outputStream)
        {
            foreach (DataGridViewColumn column in DataGridVew.Columns)
            {
                //フィールドの取得
                string field = column.HeaderText;
                //"で囲む必要があるか調べる
                if (field.IndexOf('"') > -1 ||
                    field.IndexOf(',') > -1 ||
                    field.IndexOf('\r') > -1 ||
                    field.IndexOf('\n') > -1 ||
                    field.StartsWith(" ") || field.StartsWith("\t") ||
                    field.EndsWith(" ") || field.EndsWith("\t"))
                {
                    if (field.IndexOf('"') > -1)
                    {
                        //"を""とする
                        field = field.Replace("\"", "\"\"");
                    }
                    field = "\"" + field + "\"";
                }
                //フィールドを書き込む
                outputStream.Write(field);
                outputStream.Write(',');
            }
            //改行する
            outputStream.Write("\r\n");
        }

        /// <summary>
        /// DataGridViewの明細情報からCSVファイルを作成して出力する処理
        /// </summary>
        public void CreateCSVFileForDataGrid()
        {
            //SaveFileDialogクラスのインスタンスを作成
            SaveFileDialog sfd = new SaveFileDialog();

            //はじめのファイル名を指定する
            sfd.FileName = FileName + ".csv";
            //はじめに表示されるフォルダを指定する
            sfd.InitialDirectory = @"C:\";
            //[ファイルの種類]に表示される選択肢を指定する
            sfd.Filter =
                "CSVファイル(*.csv)|*.csv|すべてのファイル(*.*)|*.*";
            //[ファイルの種類]ではじめに
            //「すべてのファイル」が選択されているようにする
            sfd.FilterIndex = 2;
            //タイトルを設定する
            sfd.Title = "保存先のファイルを選択してください";
            //ダイアログボックスを閉じる前に現在のディレクトリを復元するようにする
            sfd.RestoreDirectory = true;
            //既に存在するファイル名を指定したとき警告する
            //デフォルトでTrueなので指定する必要はない
            sfd.OverwritePrompt = true;
            //存在しないパスが指定されたとき警告を表示する
            //デフォルトでTrueなので指定する必要はない
            sfd.CheckPathExists = true;

            //ダイアログを表示する
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                //OKボタンがクリックされたとき
                //選択された名前で新しいファイルを作成し、
                //読み書きアクセス許可でそのファイルを開く
                //既存のファイルが選択されたときはデータが消える恐れあり
                System.IO.Stream stream;
                stream = sfd.OpenFile();
                if (stream != null)
                {
                    //ファイルに書き込む
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(stream);

                    this.OutputCSVFileForDataGrid(sw);

                    //閉じる
                    sw.Close();
                    stream.Close();
                }
            }
        }
    }
}