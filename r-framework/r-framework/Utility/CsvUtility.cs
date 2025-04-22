using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dto;
using Dgv = r_framework.CustomControl.CustomDataGridView;
using DgvCol = System.Windows.Forms.DataGridViewColumn;
using DgvRow = System.Windows.Forms.DataGridViewRow;
using Mr = r_framework.CustomControl.GcCustomMultiRow;
using MrRow = GrapeCity.Win.MultiRow.Row;
using System.Threading;

namespace r_framework.Utility
{
    /// <summary>
    /// CSVユーティリティ
    /// </summary>
    public class CsvUtility
    {
        #region 定数

        #region ファイル名再編集用定数

        /// <summary>
        ///
        /// </summary>
        private static readonly string[] ESCAPE_CHARS = { "\\", "/", ":", "*", "?", "\"", "<", ">", "|", "#", "{", "}", "%", "&", "~", ".", "[", "]" };

        /// <summary>
        ///
        /// </summary>
        //private static readonly string[] REPLACE_CHARS = { "￥", "／", "：", "＊", "？", "″", "＜", "＞", "｜", "＃", "｛", "｝", "％", "＆", "～", "．" };
        private static readonly string[] REPLACE_CHARS = { "", "", "", "", "", "", "＜", "＞", "｜", "＃", "｛", "｝", "％", "＆", "～", "．", "", "" };

        #endregion ファイル名再編集用定数

        #endregion 定数

        #region 変数

        #region 初期化する時一旦保存用

        private string tempFileName = string.Empty;

        private CsvColumns tempColumns = null;
        private CsvFormats tempFormats = null;
        private CsvHeaders tempHeaders = null;
        private bool tempOutputHeader = true;
        private bool tempUseOriginalHeaderPriority = false;

        #endregion 初期化する時一旦保存用

        private object source = null;

        private CsvColumns outputColumns = null;
        private CsvFormats outputFormats = null;
        private CsvHeaders outputHeaders = null;

        private List<string> notOuputColumns = null;

        private SuperForm form = null;
        private Encoding encoding = null;
        private string fullName = string.Empty;

        private bool initialized = false;

        #endregion 変数

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="form"></param>
        /// <param name="fileName"></param>
        /// <param name="customColumns"></param>
        /// <param name="customFormats"></param>
        /// <param name="customHeaders"></param>
        /// <param name="outputHeader"></param>
        /// <param name="useOriginalHeaderPriority"></param>
        /// <param name="encoding"></param>
        private CsvUtility(SuperForm form, string fileName,
            CsvColumns customColumns, CsvFormats customFormats, CsvHeaders customHeaders, bool outputHeader, bool useOriginalHeaderPriority,
            Encoding encoding, List<string> notOutputColumns)
        {
            // 対象フォーム
            this.form = form;

            // 一時変数
            this.tempFileName = fileName;
            this.tempColumns = customColumns;
            this.tempFormats = customFormats;
            this.tempHeaders = customHeaders;
            this.tempOutputHeader = outputHeader;
            this.tempUseOriginalHeaderPriority = useOriginalHeaderPriority;

            // エンコード
            this.encoding = encoding;

            this.notOuputColumns = notOutputColumns;

            this.initialized = false;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="mr"></param>
        /// <param name="form"></param>
        /// <param name="fileName"></param>
        /// <param name="customColumns"></param>
        /// <param name="customFormats"></param>
        /// <param name="customHeaders"></param>
        /// <param name="outputHeader"></param>
        /// <param name="useOriginalHeaderPriority"></param>
        /// <param name="encoding"></param>
        public CsvUtility(GcCustomMultiRow mr, SuperForm form = null, string fileName = "",
            CsvColumns customColumns = null, CsvFormats customFormats = null, CsvHeaders customHeaders = null, bool outputHeader = true, bool useOriginalHeaderPriority = false,
            Encoding encoding = null, List<string> notOutputColumns = null)
            : this(form ?? (SuperForm)mr.FindForm(), fileName, customColumns, customFormats, customHeaders, outputHeader, useOriginalHeaderPriority, encoding ?? new UTF8Encoding(true), notOutputColumns)
        {
            this.source = mr;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="form"></param>
        /// <param name="fileName"></param>
        /// <param name="customColumns"></param>
        /// <param name="customFormats"></param>
        /// <param name="customHeaders"></param>
        /// <param name="outputHeader"></param>
        /// <param name="useOriginalHeaderPriority"></param>
        /// <param name="encoding"></param>
        /// <param name="bomFlag"></param>
        public CsvUtility(CustomDataGridView dgv, SuperForm form = null, string fileName = "",
            CsvColumns customColumns = null, CsvFormats customFormats = null, CsvHeaders customHeaders = null, bool outputHeader = true, bool useOriginalHeaderPriority = false,
            Encoding encoding = null, bool bomFlag = true, List<string> notOutputColumns = null)
            : this(form ?? (SuperForm)dgv.FindForm(), fileName, customColumns, customFormats, customHeaders, outputHeader, useOriginalHeaderPriority, encoding ?? new UTF8Encoding(bomFlag), notOutputColumns)
        {
            this.source = dgv;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="form"></param>
        /// <param name="fileName"></param>
        /// <param name="customColumns"></param>
        /// <param name="customFormats"></param>
        /// <param name="customHeaders"></param>
        /// <param name="outputHeader"></param>
        /// <param name="encoding"></param>
        public CsvUtility(DataTable dt, SuperForm form, string fileName = "",
            CsvColumns customColumns = null, CsvFormats customFormats = null, CsvHeaders customHeaders = null, bool outputHeader = true,
            Encoding encoding = null, List<string> notOutputColumns = null)
            : this(form, fileName, customColumns, customFormats, customHeaders, outputHeader, true, encoding ?? new UTF8Encoding(true), notOutputColumns)
        {
            this.source = dt;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dtList"></param>
        /// <param name="form"></param>
        /// <param name="fileName"></param>
        /// <param name="customColumns"></param>
        /// <param name="customFormats"></param>
        /// <param name="customHeaders"></param>
        /// <param name="outputHeader"></param>
        /// <param name="encoding"></param>
        public CsvUtility(List<DataTable> dtList, SuperForm form, string fileName = "",
            CsvColumns customColumns = null, CsvFormats customFormats = null, CsvHeaders customHeaders = null, bool outputHeader = true,
            Encoding encoding = null, List<string> notOutputColumns = null)
            : this(form, fileName, customColumns, customFormats, customHeaders, outputHeader, true, encoding ?? new UTF8Encoding(true), notOutputColumns)
        {
            this.source = dtList;
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="list">カンマ区切りで整形済のリスト</param>
        /// <param name="form">画面フォーム</param>
        /// <param name="fileName">ファイル名</param>
        public CsvUtility(List<string> list, SuperForm form, string fileName)
        {
            // データリスト
            this.source = list;
            // 対象フォーム
            this.form = form;
            // ファイル名
            this.tempFileName = fileName;
            // エンコーディング
            this.encoding = new UTF8Encoding(true);
        }

        #endregion コンストラクタ

        #region メソッド

        #region 出力メソッド

        // Begin: LANDUONG - 20220211 - refs#157800
        public bool OutputNew()
        {
            if (this.source == null ||
                (this.source is Mr && (this.source as Mr).Rows.Count == 0) ||
                (this.source is Dgv && (this.source as Dgv).Rows.Count == 0) ||
                (this.source is DataTable && (this.source as DataTable).Rows.Count == 0) ||
                (this.source is List<string> && (this.source as List<string>).Count == 0) ||
                (this.source is List<DataTable> && (this.source as List<DataTable>).Count == 0))
            {
                LogUtility.Warn("出力対象データがありません。");
                MessageBox.Show("対象データが無い為、出力を中止しました", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            #region ファイル名設定

            // 一時ファイル名設定
            this.encoding = Encoding.GetEncoding("Shift_JIS");
            var fileName = this.tempFileName;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                // フォームからデフォルトファイル名を作成
                if (this.form != null)
                {
                    fileName = form.WindowId.ToTitleString(); // スーパーフォームの上にいる、ベースフォームのtextをとりあえず利用。
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        var titleControl = new ControlUtility().FindControl(this.form.Parent ?? this.form, "lb_title");
                        if (titleControl != null)
                        {
                            fileName = titleControl.Text;
                        }
                    }
                }
            }
            fileName = this.EscapeFileName(fileName) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
            var directoryName = string.Empty;
            using (var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder())
            {
                var title = "CSVファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;

                directoryName = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);
            }

            if (string.IsNullOrWhiteSpace(directoryName) || string.IsNullOrWhiteSpace(fileName))
            {
                LogUtility.Error("出力パスは空文字です。");
                return false;
            }

            // 最終出力ファイル名設定
            this.fullName = Path.Combine(directoryName, fileName);

            #endregion ファイル名設定

            bool ret = false;

            // リストの場合、整形済みのため初期化をスキップ。
            if (this.source is List<DataTable> == true)
            {
                List<DataTable> dtList = (List<DataTable>)this.source;
                int index = 0;
                foreach (DataTable dt in dtList)
                {
                    var fileNameNew = this.tempFileName;
                    if (string.IsNullOrWhiteSpace(fileNameNew))
                    {
                        // フォームからデフォルトファイル名を作成
                        if (this.form != null)
                        {
                            fileNameNew = form.WindowId.ToTitleString(); // スーパーフォームの上にいる、ベースフォームのtextをとりあえず利用。
                            if (string.IsNullOrWhiteSpace(fileNameNew))
                            {
                                var titleControl = new ControlUtility().FindControl(this.form.Parent ?? this.form, "lb_title");
                                if (titleControl != null)
                                {
                                    fileNameNew = titleControl.Text;
                                }
                            }
                        }
                    }
                    if (index > 0)
                    {
                        Thread.Sleep(1000);
                    }
                    fileNameNew = this.EscapeFileName(fileNameNew) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                    // 最終出力ファイル名設定
                    this.fullName = Path.Combine(directoryName, fileNameNew);
                    this.source = dt;
                    this.Initialize();
                    ret = this.OutputCsvNew();
                    index++;
                }
            }
            else
            {
                if (this.source is List<string> == false)
                {
                    this.Initialize();
                }
                ret = this.OutputCsvNew();
            }

            return ret;
        }

        /// <summary>
        /// CSV出力
        /// </summary>
        internal bool OutputCsvNew()
        {
            StreamWriter sw = null;
            try
            {
                // Create
                Encoding encoding = Encoding.GetEncoding("Shift_JIS");
                using (sw = new StreamWriter(this.fullName, false, encoding))
                {
                    // ヘッダ出力すると指示し、ヘッダ設定を行う、OutputHeaderは必ずNullではないので、出力する
                    // ヘッダ出力しないと指示し、ヘッダ設定を行わない、OutputHeaderは必ずNullので、出力しない
                    if (this.outputHeaders != null)
                    {
                        this.OutputHeaderLine(sw);
                    }

                    this.OutputCellLinesNew(sw);

                    sw.Close();

                    return true;
                }
            }
            // TODO: エラー処理
            //catch (Exception ex)
            //{
            //    throw new EdisonException(ex, string.Empty);
            //}
            finally
            {
                #region StreamWriter後処理

                if (sw != null)
                {
                    try
                    {
                        sw.Close();
                        sw.Dispose();
                    }
                    catch
                    {
                        // 処理なし
                    }
                    finally
                    {
                        sw = null;
                    }
                }

                #endregion StreamWriter後処理
            }
        }

        private void OutputCellLinesNew(StreamWriter sw)
        {
            if (this.source is Mr)
            {
                #region MR

                // 行ループ
                foreach (var row in (this.source as Mr).Rows.Cast<MrRow>())
                {
                    // 新規行出力しない
                    if (row.IsNewRow) continue;

                    // 行編集
                    var format = string.Empty;
                    var values = new List<string>();
                    // 項目ループ
                    foreach (var column in this.outputColumns)
                    {
                        var value = string.Empty;
                        // MRに表示される項目
                        if (column.ColumnType == CsvColumns.SOURCE_TYPE.MR)
                        {
                            var cell = column.ColumnName.StartsWith("#") ?
                                row.Cells[Convert.ToInt32(column.ColumnName.Replace("#", ""))] : row.Cells[column.ColumnName];
                            if (cell.Value != null) // DBNull許容
                            {
                                if (this.outputFormats != null && this.outputFormats.TryGetValue(column.ColumnName, out format))
                                {
                                    value = string.Format("{0:" + format + "}", cell.Value);
                                }
                                else
                                {
                                    if (cell.IsInEditMode && cell.EditedFormattedValue != null)
                                    {
                                        value = cell.EditedFormattedValue.ToString();
                                    }
                                    else if (cell.FormattedValue != null)
                                    {
                                        value = cell.FormattedValue.ToString();
                                    }
                                    else
                                    {
                                        value = cell.Value.ToString();
                                    }
                                }
                            }
                        }
                        // MRに表示されない、バインドデータから取得できる項目
                        else //if (column.ColumnType == CsvColumns.SOURCE_TYPE.DT)
                        {
                            var obj = (row.DataBoundItem as DataRowView).Row[column.ColumnName];
                            if (obj != null) // DBNull許容
                            {
                                if (this.outputFormats != null && this.outputFormats.TryGetValue(column.ColumnName, out format))
                                {
                                    value = string.Format("{0:" + format + "}", obj);
                                }
                                else
                                {
                                    value = obj.ToString();
                                }
                            }
                        }
                        values.Add(value);
                    }

                    // 行書込み
                    this.WriteCsvLineRaku(sw, values);
                }

                #endregion MR
            }
            else if (this.source is Dgv)
            {
                #region DGV

                // 行ループ
                foreach (var row in (this.source as Dgv).Rows.Cast<DgvRow>())
                {
                    // 新規行出力しない
                    if (row.IsNewRow) continue;

                    // 行編集
                    var format = string.Empty;
                    var values = new List<string>();
                    // 項目ループ
                    foreach (var column in this.outputColumns)
                    {
                        var value = string.Empty;
                        // DGVに表示される項目
                        if (column.ColumnType == CsvColumns.SOURCE_TYPE.DGV)
                        {
                            var cell = column.ColumnName.StartsWith("#") ?
                                row.Cells[Convert.ToInt32(column.ColumnName.Replace("#", ""))] : row.Cells[column.ColumnName];
                            if (cell.Value != null) // DBNull許容
                            {
                                if (this.outputFormats != null && this.outputFormats.TryGetValue(column.ColumnName, out format))
                                {
                                    value = string.Format("{0:" + format + "}", cell.Value);
                                }
                                else
                                {
                                    if (cell.IsInEditMode && cell.EditedFormattedValue != null)
                                    {
                                        value = cell.EditedFormattedValue.ToString();
                                    }
                                    else if (cell.FormattedValue != null)
                                    {
                                        value = cell.FormattedValue.ToString();
                                    }
                                    else
                                    {
                                        value = cell.Value.ToString();
                                    }
                                }
                            }
                        }
                        // DGVに表示されない、バインドデータから取得できる項目
                        else //if (column.ColumnType == CsvColumns.SOURCE_TYPE.DT)
                        {
                            var obj = (row.DataBoundItem as DataRowView).Row[column.ColumnName];
                            if (obj != null) // DBNull許容
                            {
                                if (this.outputFormats != null && this.outputFormats.TryGetValue(column.ColumnName, out format))
                                {
                                    value = string.Format("{0:" + format + "}", obj);
                                }
                                else
                                {
                                    value = obj.ToString();
                                }
                            }
                        }
                        values.Add(value);
                    }

                    // 行書込み
                    this.WriteCsvLineRaku(sw, values);
                }

                #endregion DGV
            }
            else if (this.source is DataTable)
            {
                #region DataTable

                // 行ループ
                foreach (var row in (this.source as DataTable).Rows.Cast<DataRow>())
                {
                    // 行編集
                    var format = string.Empty;
                    var values = new List<string>();
                    // 項目ループ
                    //var values = this.outputColumns.Select(
                    //    column =>
                    //    {
                    //        if (Convert.IsDBNull(row[column.ColumnName]))
                    //        {
                    //            return string.Empty;
                    //        }
                    //        else if (this.outputFormats.TryGetValue(column, out format)) // && !string.IsNullOrWhiteSpace(format))
                    //        {
                    //            return string.Format("{0:" + format + "}", row[column.ColumnName]);
                    //        }
                    //        else
                    //        {
                    //            return row[column.ColumnName].ToString();
                    //        }
                    //    });
                    foreach (var column in this.outputColumns)
                    {
                        var value = string.Empty;
                        var obj = row[column.ColumnName];
                        if (obj != null) // DBNull許容
                        {
                            if (this.outputFormats != null && this.outputFormats.TryGetValue(column.ColumnName, out format))
                            {
                                value = string.Format("{0:" + format + "}", obj);
                            }
                            else
                            {
                                value = obj.ToString();
                            }
                        }
                        values.Add(value);
                    }

                    // 行書込み
                    this.WriteCsvLineRaku(sw, values);  // QNTUAN #157800 S
                }

                #endregion DataTable
            }
            else if (this.source is List<string>)
            {
                #region List<string>
                // 行書込み
                var values = new List<string>();
                values = (List<string>)this.source;
                this.WriteCsvLineRaku(sw, values);
                #endregion List<string>
            }
        }
        // End: LANDUONG - 20220211 - refs#157800

        /// <summary>
        /// CSV出力
        /// </summary>
        public bool Output()
        {
            if (this.source == null ||
                (this.source is Mr && (this.source as Mr).Rows.Count == 0) ||
                (this.source is Dgv && (this.source as Dgv).Rows.Count == 0) ||
                (this.source is DataTable && (this.source as DataTable).Rows.Count == 0) ||
                (this.source is List<string> && (this.source as List<string>).Count == 0) ||
                (this.source is List<DataTable> && (this.source as List<DataTable>).Count == 0))
            {
                LogUtility.Warn("出力対象データがありません。");
                MessageBox.Show("対象データが無い為、出力を中止しました", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            #region ファイル名設定

            // 一時ファイル名設定
            var fileName = this.tempFileName;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                // フォームからデフォルトファイル名を作成
                if (this.form != null)
                {
                    fileName = form.WindowId.ToTitleString(); // スーパーフォームの上にいる、ベースフォームのtextをとりあえず利用。
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        var titleControl = new ControlUtility().FindControl(this.form.Parent ?? this.form, "lb_title");
                        if (titleControl != null)
                        {
                            fileName = titleControl.Text;
                        }
                    }
                }
            }
            fileName = this.EscapeFileName(fileName) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";

            var directoryName = string.Empty;
            using (var browserForFolder = new r_framework.BrowseForFolder.BrowseForFolder())
            {
                var title = "CSVファイルの出力場所を選択してください。";
                var initialPath = @"C:\Temp";
                var windowHandle = form.Handle;
                var isFileSelect = false;
                var isTerminalMode = SystemProperty.IsTerminalMode;

                directoryName = browserForFolder.SelectFolder(title, initialPath, windowHandle, isFileSelect);
            }

            if (string.IsNullOrWhiteSpace(directoryName) || string.IsNullOrWhiteSpace(fileName))
            {
                LogUtility.Error("出力パスは空文字です。");
                return false;
            }

            // 最終出力ファイル名設定
            this.fullName = Path.Combine(directoryName, fileName);

            #endregion ファイル名設定

            bool ret = false;

            // リストの場合、整形済みのため初期化をスキップ。
            if (this.source is List<DataTable> == true)
            {
                List<DataTable> dtList = (List<DataTable>)this.source;
                int index = 0;
                foreach (DataTable dt in dtList)
                {
                    var fileNameNew = this.tempFileName;
                    if (string.IsNullOrWhiteSpace(fileNameNew))
                    {
                        // フォームからデフォルトファイル名を作成
                        if (this.form != null)
                        {
                            fileNameNew = form.WindowId.ToTitleString(); // スーパーフォームの上にいる、ベースフォームのtextをとりあえず利用。
                            if (string.IsNullOrWhiteSpace(fileNameNew))
                            {
                                var titleControl = new ControlUtility().FindControl(this.form.Parent ?? this.form, "lb_title");
                                if (titleControl != null)
                                {
                                    fileNameNew = titleControl.Text;
                                }
                            }
                        }
                    }
                    if (index > 0)
                    {
                        Thread.Sleep(1000);
                    }
                    fileNameNew = this.EscapeFileName(fileNameNew) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                    // 最終出力ファイル名設定
                    this.fullName = Path.Combine(directoryName, fileNameNew);
                    this.source = dt;
                    this.Initialize();
                    ret = this.OutputCsv();
                    index++;
                }
            }
            else
            {
                if (this.source is List<string> == false)
                {
                    this.Initialize();
                }
                ret = this.OutputCsv();
            }

            return ret;
            }

        /// <summary>
        /// CSV出力
        /// </summary>
        internal bool OutputCsv()
        {
            StreamWriter sw = null;
            try
            {
                // Create
                using (sw = new StreamWriter(this.fullName, false, this.encoding))
                {
                    // ヘッダ出力すると指示し、ヘッダ設定を行う、OutputHeaderは必ずNullではないので、出力する
                    // ヘッダ出力しないと指示し、ヘッダ設定を行わない、OutputHeaderは必ずNullので、出力しない
                    if (this.outputHeaders != null)
                    {
                        this.OutputHeaderLine(sw);
                    }

                    this.OutputCellLines(sw);

                    sw.Close();

                    return true;
                }
            }
            // TODO: エラー処理
            //catch (Exception ex)
            //{
            //    throw new EdisonException(ex, string.Empty);
            //}
            finally
            {
                #region StreamWriter後処理

                if (sw != null)
                {
                    try
                    {
                        sw.Close();
                        sw.Dispose();
                    }
                    catch
                    {
                        // 処理なし
                    }
                    finally
                    {
                        sw = null;
                    }
                }

                #endregion StreamWriter後処理
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>
        /// 使い方：
        /// 複数ページありのMR又はDGVに対して、1ページ目出力した後、残り部分をページ毎に出力する、
        /// 又は、データ量が多いで、一括で出力するとメモリリックが発生する場合、指定件数分毎に出力する。
        /// </remarks>
        public bool OutputContinous(object source)
        {
            // 初期化してない(後続出力方法ので、先ず、一回Outputメソッドを呼び出すことは必要です。)
            if (!this.initialized)
            {
                LogUtility.Error("本メソッドは後続の連続出力用のメソッドので、先に一回だけOutputメソッドを呼び出すが必要です。");
                return false;
            }

            #region 出力対象チェックと設定

            if (source == null)
            {
                LogUtility.Warn("出力対象データがありません。");
                return false;
            }
            else if (source is Mr)
            {
                #region MR

                // データは存在するか判断
                if ((source as Mr).Rows.Count == 0)
                {
                    LogUtility.Warn("出力対象データがありません。");
                    return false;
                }
                // 前回出力と一致するか判断
                else if (this.source is Mr && (this.source as Mr).Template.Equals((source as Mr).Template))
                {
                    this.source = source;
                }
                else
                {
                    LogUtility.Error("連続出力対象データと前回の型は不一致です。");
                    return false;
                }

                #endregion MR
            }
            else if (source is Dgv)
            {
                #region DGV

                if ((source as Dgv).Rows.Count == 0)
                {
                    LogUtility.Warn("出力対象データがありません。");
                    return false;
                }
                else if (this.source is Dgv && (this.source as Dgv).Columns.Equals((source as Dgv).Columns))
                {
                    this.source = source;
                }
                else
                {
                    LogUtility.Error("連続出力対象データと前回の型は不一致です。");
                    return false;
                }

                #endregion DGV
            }
            else if (source is DataTable)
            {
                #region DataTable

                if ((source as DataTable).Rows.Count == 0)
                {
                    LogUtility.Warn("出力対象データがありません。");
                    return false;
                }
                else if (this.source is DataTable && (this.source as DataTable).Columns.Equals((source as DataTable).Columns))
                {
                    this.source = source;
                }
                else if (this.source is Mr &&
                    (this.source as Mr).DataSource != null &&
                    (this.source as Mr).DataSource is DataTable && ((this.source as Mr).DataSource as DataTable).Columns.Equals((source as DataTable).Columns))
                {
                    (this.source as Mr).DataSource = source;
                }
                else if (this.source is Dgv &&
                    (this.source as Dgv).DataSource != null &&
                    (this.source as Dgv).DataSource is DataTable && ((this.source as Dgv).DataSource as DataTable).Columns.Equals((source as DataTable).Columns))
                {
                    (this.source as Dgv).DataSource = source;
                }
                else
                {
                    LogUtility.Error("連続出力対象データと前回の型は不一致です。");
                    return false;
                }

                #endregion DataTable
            }
            else
            {
                #region 以外(支持しないので型不正エラー)

                LogUtility.Error("出力対象の型は不正です。");
                return false;

                #endregion 以外(支持しないので型不正エラー)
            }

            #endregion 出力対象チェックと設定

            StreamWriter sw = null;
            try
            {
                // Append
                using (sw = new StreamWriter(this.fullName, true, this.encoding))
                {
                    this.OutputCellLines(sw);
                    sw.Close();
                }

                return true;
            }
            // TODO: エラー処理
            //catch (Exception ex)
            //{
            //    throw new EdisonException(ex, string.Empty);
            //}
            finally
            {
                #region StreamWriter後処理

                if (sw != null)
                {
                    try
                    {
                        sw.Close();
                    }
                    catch
                    {
                        // 処理なし
                    }
                    finally
                    {
                        sw = null;
                    }
                }

                #endregion StreamWriter後処理
            }
        }

        #region 現時点で利用しない

        /// <summary>
        ///
        /// </summary>
        /// <param name="source"></param>
        /// <remarks>
        /// 使い方：
        /// 複数ページありのMR又はDGVに対して、1ページ目出力した後、残り部分をページ毎に出力する、
        /// 又は、データ量が多いで、一括で出力するとメモリリックが発生する場合、指定件数分毎に出力する。
        /// </remarks>
        [Obsolete("現時点で利用しない。")]
        private bool OutputContinous(object source, StreamWriter sw)
        {
            // 初期化してない(後続出力方法ので、先ず、一回Outputメソッドを呼び出すことは必要です。)
            if (!this.initialized)
            {
                LogUtility.Error("本メソッドは後続の連続出力用のメソッドので、先に一回だけOutputメソッドを呼び出すが必要です。");
                return false;
            }

            #region 出力対象チェックと設定

            if (source == null)
            {
                LogUtility.Warn("出力対象データがありません。");
                return false;
            }
            else if (source is Mr)
            {
                #region MR

                // データは存在するか判断
                if ((source as Mr).Rows.Count == 0)
                {
                    LogUtility.Warn("出力対象データがありません。");
                    return false;
                }
                // 前回出力と一致するか判断
                else if (this.source is Mr && (this.source as Mr).Template.Equals((source as Mr).Template))
                {
                    this.source = source;
                }
                else
                {
                    LogUtility.Error("連続出力対象データと前回の型は不一致です。");
                    return false;
                }

                #endregion MR
            }
            else if (source is Dgv)
            {
                #region DGV

                if ((source as Dgv).Rows.Count == 0)
                {
                    LogUtility.Warn("出力対象データがありません。");
                    return false;
                }
                else if (this.source is Dgv && (this.source as Dgv).Columns.Equals((source as Dgv).Columns))
                {
                    this.source = source;
                }
                else
                {
                    LogUtility.Error("連続出力対象データと前回の型は不一致です。");
                    return false;
                }

                #endregion DGV
            }
            else if (source is DataTable)
            {
                #region DataTable

                if ((source as DataTable).Rows.Count == 0)
                {
                    LogUtility.Warn("出力対象データがありません。");
                    return false;
                }
                else if (this.source is DataTable && (this.source as DataTable).Columns.Equals((source as DataTable).Columns))
                {
                    this.source = source;
                }
                else if (this.source is Mr &&
                    (this.source as Mr).DataSource != null &&
                    (this.source as Mr).DataSource is DataTable && ((this.source as Mr).DataSource as DataTable).Columns.Equals((source as DataTable).Columns))
                {
                    (this.source as Mr).DataSource = source;
                }
                else if (this.source is Dgv &&
                    (this.source as Dgv).DataSource != null &&
                    (this.source as Dgv).DataSource is DataTable && ((this.source as Dgv).DataSource as DataTable).Columns.Equals((source as DataTable).Columns))
                {
                    (this.source as Dgv).DataSource = source;
                }
                else
                {
                    LogUtility.Error("連続出力対象データと前回の型は不一致です。");
                    return false;
                }

                #endregion DataTable
            }
            else
            {
                #region 以外(支持しないので型不正エラー)

                LogUtility.Error("出力対象の型は不正です。");
                return false;

                #endregion 以外(支持しないので型不正エラー)
            }

            #endregion 出力対象チェックと設定

            this.OutputCellLines(sw);
            return true;
        }

        /// <summary>
        /// 初期化(出力準備)
        /// </summary>
        private void Initialize()
        {
            // 出力項目チェックと設定
            this.SetColumns();

            // 出力フォーマットチェックと設定
            this.SetFormats();

            // 出力ヘッダチェックと設定
            if (this.tempOutputHeader)
            {
                this.SetHeaders();
            }

            this.initialized = true;
        }

        #endregion 現時点で利用しない

        public bool OutputWithPath(string filePath)
        {
            if (this.source == null ||
                (this.source is Mr && (this.source as Mr).Rows.Count == 0) ||
                (this.source is Dgv && (this.source as Dgv).Rows.Count == 0) ||
                (this.source is DataTable && (this.source as DataTable).Rows.Count == 0) ||
                (this.source is List<string> && (this.source as List<string>).Count == 0) ||
                (this.source is List<DataTable> && (this.source as List<DataTable>).Count == 0))
            {
                LogUtility.Warn("出力対象データがありません。");
                return false;
            }

            #region ファイル名設定
            // 一時ファイル名設定
            var fileName = this.tempFileName;
            if (string.IsNullOrWhiteSpace(fileName))
            {
                // フォームからデフォルトファイル名を作成
                if (this.form != null)
                {
                    fileName = form.WindowId.ToTitleString(); // スーパーフォームの上にいる、ベースフォームのtextをとりあえず利用。
                    if (string.IsNullOrWhiteSpace(fileName))
                    {
                        var titleControl = new ControlUtility().FindControl(this.form.Parent ?? this.form, "lb_title");
                        if (titleControl != null)
                        {
                            fileName = titleControl.Text;
                        }
                    }
                }
            }
            fileName = this.EscapeFileName(fileName) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
            if (string.IsNullOrWhiteSpace(filePath) || string.IsNullOrWhiteSpace(fileName))
            {
                LogUtility.Error("出力パスは空文字です。");
                return false;
            }
            // 最終出力ファイル名設定
            this.fullName = Path.Combine(filePath, fileName);
            #endregion

            bool ret = false;
            // リストの場合、整形済みのため初期化をスキップ。
            if (this.source is List<DataTable> == true)
            {
                List<DataTable> dtList = (List<DataTable>)this.source;
                int index = 0;
                foreach (DataTable dt in dtList)
                {
                    var fileNameNew = this.tempFileName;
                    if (string.IsNullOrWhiteSpace(fileNameNew))
                    {
                        // フォームからデフォルトファイル名を作成
                        if (this.form != null)
                        {
                            fileNameNew = form.WindowId.ToTitleString(); // スーパーフォームの上にいる、ベースフォームのtextをとりあえず利用。
                            if (string.IsNullOrWhiteSpace(fileNameNew))
                            {
                                var titleControl = new ControlUtility().FindControl(this.form.Parent ?? this.form, "lb_title");
                                if (titleControl != null)
                                {
                                    fileNameNew = titleControl.Text;
                                }
                            }
                        }
                    }
                    if (index > 0)
                    {
                        Thread.Sleep(1000);
                    }
                    fileNameNew = this.EscapeFileName(fileNameNew) + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv";
                    // 最終出力ファイル名設定
                    this.fullName = Path.Combine(filePath, fileNameNew);
                    this.source = dt;
                    this.Initialize();
                    ret = this.OutputCsvNew();
                    index++;
                }
            }
            else
            {
                if (this.source is List<string> == false)
                {
                    this.Initialize();
                }
                ret = this.OutputCsvNew();
            }
            return ret;
        }
        #endregion 出力メソッド

        #region 事前編集メソッド

        /// <summary>
        /// 出力項目設定
        /// </summary>
        /// <remarks>
        /// 出力対象項目名が指定されない場合、MR/DGV/DataTableから、出力項目を作成する、
        /// 出力対象項目名が指定される場合、MR/DGV/DataTableの既存項目を比較とチェックし、出力項目を設定する。
        /// </remarks>
        private void SetColumns()
        {
            if (this.source is Mr)
            {
                #region MR

                var mr = this.source as Mr;
                // チェック用と表示用項目名配列作成
                var checkColumnNamesMr = new List<string>();
                var checkColumnDataFieldsMr = new List<string>();
                var dispColumnNamesMr = new List<string>();
                // MRからソート済みの列情報を作成する
                foreach (var index in mr.Template.ColumnHeaders[0].Cells.OrderBy(cell => cell.Location.Y).ThenBy(cell => cell.Location.X).Select(cell => cell.CellIndex))
                {
                    var columnTemplate = mr.Template.Row.Cells[index];
                    var columnVisible = columnTemplate.Visible;
                    var columnName = string.IsNullOrWhiteSpace(columnTemplate.Name) ? "#" + index : columnTemplate.Name.Trim();
                    var columnDataField = columnTemplate.DataField.Trim();
                    checkColumnNamesMr.Add(columnName);
                    if (columnVisible)
                    {
                        if (this.notOuputColumns == null || !this.notOuputColumns.Contains(columnName))
                        {
                            dispColumnNamesMr.Add(columnName);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(columnTemplate.DataField))
                    {
                        checkColumnDataFieldsMr.Add(columnDataField);
                    }
                }
                // MRのデータソースから、MRに表示されてない列をチェック用項目名配列に追加する
                var checkColumnNamesTable = new List<string>();
                if (mr.DataSource != null && mr.DataSource is DataTable)
                {
                    checkColumnNamesTable.AddRange(
                        (mr.DataSource as DataTable).Columns.Cast<DataColumn>().
                        Select(column => column.ColumnName).
                        Where(columnName => !checkColumnNamesMr.Contains(columnName) && !checkColumnDataFieldsMr.Contains(columnName)));
                }

                // 出力項目が指定された場合
                if (this.tempColumns != null)
                {
                    // 存在しない列が指定されてた場合
                    if (this.tempColumns.Any(column =>
                        !checkColumnNamesMr.Contains(column.ColumnName) && !checkColumnDataFieldsMr.Contains(column.ColumnName) &&
                        !checkColumnNamesTable.Contains(column.ColumnName)))
                    {
                        //throw new EdisonException("出力対象に存在しない項目が指定されてる。");
                        LogUtility.Error("出力対象に存在しない項目が指定されてる。");
                    }

                    var rebuildColumnNames = new List<string>();
                    var rebuildColumnTypes = new List<CsvColumns.SOURCE_TYPE>();
                    foreach (var column in this.tempColumns)
                    {
                        if (checkColumnNamesMr.Contains(column.ColumnName) || checkColumnDataFieldsMr.Contains(column.ColumnName))
                        {
                            rebuildColumnNames.Add(column.ColumnName);
                            rebuildColumnTypes.Add(CsvColumns.SOURCE_TYPE.MR);
                        }
                        else if (checkColumnNamesTable.Contains(column.ColumnName))
                        {
                            rebuildColumnNames.Add(column.ColumnName);
                            rebuildColumnTypes.Add(CsvColumns.SOURCE_TYPE.DT);
                        }
                    }
                    this.outputColumns = CsvColumns.CreateCustomColumns(rebuildColumnNames.ToArray(), rebuildColumnTypes.ToArray());
                }
                // 出力項目が指定されてない場合
                else
                {
                    // 表示用項目名を出力項目名として設定する
                    this.outputColumns = CsvColumns.CreateCustomColumns(dispColumnNamesMr.ToArray(), CsvColumns.SOURCE_TYPE.MR);
                }

                #endregion MR
            }
            else if (this.source is Dgv)
            {
                #region DGV

                var dgv = this.source as Dgv;
                // チェック用と表示用項目名配列作成
                var checkColumnNamesDgv = new List<string>();
                var checkColumnDataPropertiesDgv = new List<string>();
                var dispColumnNamesDgv = new List<string>();
                // DGVからソート済みの列情報を作成する
                foreach (var column in dgv.Columns.Cast<DgvCol>().OrderBy(column => column.DisplayIndex))
                {
                    var columnVisible = column.Visible;
                    var columnName = string.IsNullOrWhiteSpace(column.Name) ? "#" + column.DisplayIndex : column.Name.Trim();
                    var columnDataProperty = column.DataPropertyName.Trim();
                    checkColumnNamesDgv.Add(columnName);
                    if (columnVisible)
                    {
                        if (this.notOuputColumns == null || !this.notOuputColumns.Contains(columnName))
                        {
                            dispColumnNamesDgv.Add(columnName);
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(columnDataProperty))
                    {
                        checkColumnDataPropertiesDgv.Add(columnDataProperty);
                    }
                }
                // DGVのデータソースから、DGVに表示されてない列をチェック用項目名配列に追加する
                var checkColumnNamesTable = new List<string>();
                if (dgv.DataSource != null && dgv.DataSource is DataTable)
                {
                    checkColumnNamesTable.AddRange(
                        (dgv.DataSource as DataTable).Columns.Cast<DataColumn>().
                        Select(column => column.ColumnName).
                        Where(columnName => !checkColumnNamesDgv.Contains(columnName) && !checkColumnDataPropertiesDgv.Contains(columnName)));
                }

                // 出力項目が指定された場合
                if (this.tempColumns != null)
                {
                    // 存在しない列が指定されてた場合
                    if (this.tempColumns.Any(column =>
                        !checkColumnNamesDgv.Contains(column.ColumnName) && !checkColumnDataPropertiesDgv.Contains(column.ColumnName) &&
                        !checkColumnNamesTable.Contains(column.ColumnName)))
                    {
                        //throw new EdisonException("出力対象に存在しない項目が指定されてる。");
                        LogUtility.Error("出力対象に存在しない項目が指定されてる。");
                    }

                    var rebuildColumnNames = new List<string>();
                    var rebuildColumnTypes = new List<CsvColumns.SOURCE_TYPE>();
                    foreach (var column in this.tempColumns)
                    {
                        if (checkColumnNamesDgv.Contains(column.ColumnName) || checkColumnDataPropertiesDgv.Contains(column.ColumnName))
                        {
                            rebuildColumnNames.Add(column.ColumnName);
                            rebuildColumnTypes.Add(CsvColumns.SOURCE_TYPE.DGV);
                        }
                        else if (checkColumnNamesTable.Contains(column.ColumnName))
                        {
                            rebuildColumnNames.Add(column.ColumnName);
                            rebuildColumnTypes.Add(CsvColumns.SOURCE_TYPE.DT);
                        }
                    }
                    this.outputColumns = CsvColumns.CreateCustomColumns(rebuildColumnNames.ToArray(), rebuildColumnTypes.ToArray());
                }
                // 出力項目が指定されてない場合
                else
                {
                    // 表示用項目名を出力項目名として設定する
                    this.outputColumns = CsvColumns.CreateCustomColumns(dispColumnNamesDgv.ToArray(), CsvColumns.SOURCE_TYPE.DGV);
                }

                #endregion DGV
            }
            else if (this.source is DataTable)
            {
                #region DataTable

                // DataTableから直接項目名配列作成を作成する
                var checkColumnNames = (this.source as DataTable).Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();
                var dispColumnNamesTbl = checkColumnNames.Where(columnName => (this.notOuputColumns == null || !this.notOuputColumns.Contains(columnName))).ToList();
                // 出力項目が指定された場合
                if (this.tempColumns != null)
                {
                    // 存在しない列が指定されてた場合
                    if (this.tempColumns.Any(column => !checkColumnNames.Contains(column.ColumnName)))
                    {
                        //throw new EdisonException("出力対象に存在しない項目が指定されてる。");
                        LogUtility.Warn("出力対象に存在しない項目が指定されてる。");
                    }

                    var rebuildColumnNames = new List<string>();
                    foreach (var column in this.tempColumns)
                    {
                        if (checkColumnNames.Contains(column.ColumnName))
                        {
                            rebuildColumnNames.Add(column.ColumnName);
                        }
                    }
                    this.outputColumns = CsvColumns.CreateCustomColumns(rebuildColumnNames.ToArray(), CsvColumns.SOURCE_TYPE.DT);
                }
                else
                {
                    this.outputColumns = CsvColumns.CreateCustomColumns(dispColumnNamesTbl.ToArray(), CsvColumns.SOURCE_TYPE.DT);
                }

                #endregion DataTable
            }
        }

        /// <summary>
        /// フォーマット設定
        /// </summary>
        private void SetFormats()
        {
            // 指定したフォーマット項目配列をチェックする
            if (this.tempFormats != null)
            {
                if (this.tempFormats.Any(x => !this.outputColumns.Select(y => y.ColumnName).Contains(x.Key)))
                    //throw new EdisonException("出力対象に存在しない項目が指定されてる。");
                    LogUtility.Warn("出力対象に存在しない項目が指定されてる。");

                this.outputFormats = this.tempFormats;
                // フォーマットの空白を除く
                foreach (var key in this.outputFormats.Keys.ToArray())
                    this.outputFormats[key] = this.outputFormats[key].Trim();
            }
        }

        /// <summary>
        /// ヘッダ部出力文字設定
        /// </summary>
        private void SetHeaders()
        {
            var headers = new List<string>();
            if (this.source is Mr)
            {
                #region MR

                var mr = this.source as Mr;
                foreach (var column in this.outputColumns)
                {
                    // MR表示ヘッダから出力ヘッダを生成する
                    if (column.ColumnType == CsvColumns.SOURCE_TYPE.MR)
                    {
                        var header = column.ColumnName.StartsWith("#") ?
                            mr.Template.ColumnHeaders[0].Cells[Convert.ToInt32(column.ColumnName.Replace("#", ""))] :
                            mr.Template.ColumnHeaders[0].Cells[mr.Template.Row.Cells.FirstOrDefault(x => x.Name == column.ColumnName).CellIndex];
                        headers.Add(header.DisplayText);
                    }
                    else
                    {
                        headers.Add(column.ColumnName);
                    }
                }

                #endregion MR
            }
            else if (this.source is Dgv)
            {
                #region DGV

                var dgv = this.source as Dgv;
                foreach (var column in this.outputColumns)
                {
                    // DGV表示ヘッダから出力ヘッダを生成する
                    if (column.ColumnType == CsvColumns.SOURCE_TYPE.DGV)
                    {
                        var header = column.ColumnName.StartsWith("#") ?
                            dgv.Columns[Convert.ToInt32(column.ColumnName.Replace("#", ""))] : dgv.Columns[column.ColumnName];
                        headers.Add(header.HeaderText);
                    }
                    else
                    {
                        headers.Add(column.ColumnName);
                    }
                }

                #endregion DGV
            }
            else if (this.source is DataTable)
            {
                #region DataTable

                // DataTableは特に表示名などはないので、直接列名からコピーを作成する。
                headers.AddRange(this.outputColumns.Select(x => x.ColumnName));

                #endregion DataTable
            }
            // (MR又はDGVで設定した)ヘッダの改行を削除する
            if (!this.tempUseOriginalHeaderPriority)
            {
                for (int i = 0; i < headers.Count; i++)
                {
                    var header = headers[i];
                    header = Regex.Replace(header, " *\r *", "\r"); // 改行前後にあるスペースは全部消す
                    header = Regex.Replace(header, " *\n *", "\n"); // 同上
                    header = header.Replace("\r", ""); // 改行コードを消す
                    header = header.Replace("\n", ""); // 同上
                    headers[i] = header.Trim();
                }
            }
            this.outputHeaders = CsvHeaders.CreateCustomHeaders(this.outputColumns.Select(x => x.ColumnName).ToArray(), headers.ToArray());

            if (this.tempHeaders != null)
            {
                if (this.tempHeaders.Any(x => !this.outputColumns.Select(y => y.ColumnName).Contains(x.Key)))
                {
                    //throw new EdisonException("出力対象に存在しない項目が指定されてる。");
                    LogUtility.Warn("出力対象に存在しない項目が指定されてる。");
                }

                // 指定したヘッダを入れ替え
                // 外部指定ので、改行処理をしない
                foreach (var header in this.tempHeaders)
                {
                    if (this.outputHeaders.ContainsKey(header.Key))
                    {
                        this.outputHeaders[header.Key] = header.Value;
                    }
                }
            }
        }

        /// <summary>
        /// ファイル名内の不可用文字退避
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private string EscapeFileName(string fileName)
        {
            for (int i = 0; i < ESCAPE_CHARS.Length; i++)
            {
                fileName = fileName.Replace(ESCAPE_CHARS[i], REPLACE_CHARS[i]);
            }
            return fileName;
        }

        #endregion 事前編集メソッド

        #region ファイル書込みメソッド

        /// <summary>
        ///
        /// </summary>
        /// <param name="sw"></param>
        private void OutputHeaderLine(StreamWriter sw)
        {
            // ヘッダ編集
            var value = string.Empty;
            var values = this.outputColumns.Select(column => this.outputHeaders.TryGetValue(column.ColumnName, out value) ? value : string.Empty);

            // ヘッダ書込み
            this.WriteCsvLine(sw, values);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="directoryName"></param>
        /// <param name="fileName"></param>
        /// <param name="cellLines"></param>
        /// <param name="customFormats"></param>
        /// <param name="givenHeaders"></param>
        /// <param name="originalHeader"></param>
        private void OutputCellLines(StreamWriter sw)
        {
            if (this.source is Mr)
            {
                #region MR

                // 行ループ
                foreach (var row in (this.source as Mr).Rows.Cast<MrRow>())
                {
                    // 新規行出力しない
                    if (row.IsNewRow) continue;

                    // 行編集
                    var format = string.Empty;
                    var values = new List<string>();
                    // 項目ループ
                    foreach (var column in this.outputColumns)
                    {
                        var value = string.Empty;
                        // MRに表示される項目
                        if (column.ColumnType == CsvColumns.SOURCE_TYPE.MR)
                        {
                            var cell = column.ColumnName.StartsWith("#") ?
                                row.Cells[Convert.ToInt32(column.ColumnName.Replace("#", ""))] : row.Cells[column.ColumnName];
                            if (cell.Value != null) // DBNull許容
                            {
                                if (this.outputFormats != null && this.outputFormats.TryGetValue(column.ColumnName, out format))
                                {
                                    value = string.Format("{0:" + format + "}", cell.Value);
                                }
                                else
                                {
                                    if (cell.IsInEditMode && cell.EditedFormattedValue != null)
                                    {
                                        value = cell.EditedFormattedValue.ToString();
                                    }
                                    else if (cell.FormattedValue != null)
                                    {
                                        value = cell.FormattedValue.ToString();
                                    }
                                    else
                                    {
                                        value = cell.Value.ToString();
                                    }
                                }
                            }
                        }
                        // MRに表示されない、バインドデータから取得できる項目
                        else //if (column.ColumnType == CsvColumns.SOURCE_TYPE.DT)
                        {
                            var obj = (row.DataBoundItem as DataRowView).Row[column.ColumnName];
                            if (obj != null) // DBNull許容
                            {
                                if (this.outputFormats != null && this.outputFormats.TryGetValue(column.ColumnName, out format))
                                {
                                    value = string.Format("{0:" + format + "}", obj);
                                }
                                else
                                {
                                    value = obj.ToString();
                                }
                            }
                        }
                        values.Add(value);
                    }

                    // 行書込み
                    this.WriteCsvLine(sw, values);
                }

                #endregion MR
            }
            else if (this.source is Dgv)
            {
                #region DGV

                // 行ループ
                foreach (var row in (this.source as Dgv).Rows.Cast<DgvRow>())
                {
                    // 新規行出力しない
                    if (row.IsNewRow) continue;

                    // 行編集
                    var format = string.Empty;
                    var values = new List<string>();
                    // 項目ループ
                    foreach (var column in this.outputColumns)
                    {
                        var value = string.Empty;
                        // DGVに表示される項目
                        if (column.ColumnType == CsvColumns.SOURCE_TYPE.DGV)
                        {
                            var cell = column.ColumnName.StartsWith("#") ?
                                row.Cells[Convert.ToInt32(column.ColumnName.Replace("#", ""))] : row.Cells[column.ColumnName];
                            if (cell.Value != null) // DBNull許容
                            {
                                if (this.outputFormats != null && this.outputFormats.TryGetValue(column.ColumnName, out format))
                                {
                                    value = string.Format("{0:" + format + "}", cell.Value);
                                }
                                else
                                {
                                    if (cell.IsInEditMode && cell.EditedFormattedValue != null)
                                    {
                                        value = cell.EditedFormattedValue.ToString();
                                    }
                                    else if (cell.FormattedValue != null)
                                    {
                                        value = cell.FormattedValue.ToString();
                                    }
                                    else
                                    {
                                        value = cell.Value.ToString();
                                    }
                                }
                            }
                        }
                        // DGVに表示されない、バインドデータから取得できる項目
                        else //if (column.ColumnType == CsvColumns.SOURCE_TYPE.DT)
                        {
                            var obj = (row.DataBoundItem as DataRowView).Row[column.ColumnName];
                            if (obj != null) // DBNull許容
                            {
                                if (this.outputFormats != null && this.outputFormats.TryGetValue(column.ColumnName, out format))
                                {
                                    value = string.Format("{0:" + format + "}", obj);
                                }
                                else
                                {
                                    value = obj.ToString();
                                }
                            }
                        }
                        values.Add(value);
                    }

                    // 行書込み
                    this.WriteCsvLine(sw, values);
                }

                #endregion DGV
            }
            else if (this.source is DataTable)
            {
                #region DataTable

                // 行ループ
                foreach (var row in (this.source as DataTable).Rows.Cast<DataRow>())
                {
                    // 行編集
                    var format = string.Empty;
                    var values = new List<string>();
                    // 項目ループ
                    //var values = this.outputColumns.Select(
                    //    column =>
                    //    {
                    //        if (Convert.IsDBNull(row[column.ColumnName]))
                    //        {
                    //            return string.Empty;
                    //        }
                    //        else if (this.outputFormats.TryGetValue(column, out format)) // && !string.IsNullOrWhiteSpace(format))
                    //        {
                    //            return string.Format("{0:" + format + "}", row[column.ColumnName]);
                    //        }
                    //        else
                    //        {
                    //            return row[column.ColumnName].ToString();
                    //        }
                    //    });
                    foreach (var column in this.outputColumns)
                    {
                        var value = string.Empty;
                        var obj = row[column.ColumnName];
                        if (obj != null) // DBNull許容
                        {
                            if (this.outputFormats != null && this.outputFormats.TryGetValue(column.ColumnName, out format))
                            {
                                value = string.Format("{0:" + format + "}", obj);
                            }
                            else
                            {
                                value = obj.ToString();
                            }
                        }
                        values.Add(value);
                    }

                    // 行書込み
                    this.WriteCsvLine(sw, values);
                }

                #endregion DataTable
            }
            else if (this.source is List<string>)
            {
                #region List<string>
                // 行書込み
                var values = new List<string>();
                values = (List<string>)this.source;
                this.WriteCsvLine(sw, values);
                #endregion List<string>
            }
        }

        /// <summary>
        /// CSV行書き込み
        /// </summary>
        /// <param name="sw"></param>
        /// <param name="values"></param>
        /// <remarks>valuesを(必要なら囲む)","連結して、CSVファイルに書き込む。</remarks>
        private void WriteCsvLine(StreamWriter sw, IEnumerable<string> values)
        {
            // リストの場合、整形済みのため、そのまま書き込む。
            if (this.source is List<string>)
            {
                foreach (string value in values)
                {
                    sw.WriteLine(value);
                }
            }
            else
            {
                sw.WriteLine(string.Join(",", values.Select(
                x =>
                {
                    if (x.Contains('"') || x.Contains(',') || x.Contains('\r') || x.Contains('\n') ||
                        x.StartsWith(" ") || x.EndsWith(" ") ||
                    x.StartsWith("\t") || x.EndsWith("\t"))
                    {
                        if (x.Contains('"'))
                        {
                            // ["]を[""]とする
                            x = x.Replace("\"", "\"\"");
                        }
                        return "\"" + x + "\"";
                    }
                    else
                    {
                        return x;
                    }
                })));
            }
        }

        // Begin: LANDUONG - 20220211 - refs#157800
        private void WriteCsvLineRaku(StreamWriter sw, IEnumerable<string> values)
        {
            // リストの場合、整形済みのため、そのまま書き込む。
            if (this.source is List<string>)
            {
                foreach (string value in values)
                {
                    sw.WriteLine(value);
                }
            }
            else
            {
                sw.WriteLine(string.Join(",", values.Select(
                x =>
                {
                    if (x.Contains('"') || x.Contains(',') || x.Contains('\r') || x.Contains('\n') ||
                        x.StartsWith(" ") || x.EndsWith(" ") ||
                    x.StartsWith("\t") || x.EndsWith("\t"))
                    {
                        if (x.Contains('"'))
                        {
                            // ["]を[""]とする
                            x = x.Replace("\"", "");
                        }
                        return "\"" + x + "\"";
                    }
                    else
                    {
                        return x;
                    }
                })));
            }
        }
        // End: LANDUONG - 20220211 - refs#157800

        #endregion ファイル書込みメソッド

        #endregion メソッド

        #region 内部クラス

        public class CsvColumns : List<CsvColumns.CsvColumn>
        {
            private CsvColumns()
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="columnNames"></param>
            /// <returns></returns>
            public static CsvColumns CreateCustomColumns(string[] columnNames)
            {
                return CreateCustomColumns(columnNames, SOURCE_TYPE.DT);
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="columnNames"></param>
            /// <param name="columnType"></param>
            /// <returns></returns>
            internal static CsvColumns CreateCustomColumns(string[] columnNames, SOURCE_TYPE columnType)
            {
                // Nullの場合、Nullを戻す。
                if (columnNames == null)
                {
                    return null;
                }
                else if (columnNames.Any(x => string.IsNullOrWhiteSpace(x)) || columnNames.Select(x => x.Trim()).Distinct().Count() < columnNames.Length)
                {
                    //throw new EdisonException("項目名に空文字又は重複項目があります。");
                    LogUtility.Error("項目名に空文字又は重複項目があります。");
                    return null;
                }

                var columns = new CsvColumns();
                for (int i = 0; i < columnNames.Length; i++)
                {
                    columns.Add(new CsvColumn(columnNames[i].Trim(), columnType));
                }
                return columns;
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="columnNames"></param>
            /// <param name="columnTypes"></param>
            /// <returns></returns>
            internal static CsvColumns CreateCustomColumns(string[] columnNames, SOURCE_TYPE[] columnTypes)
            {
                // 両方もNullの場合、Nullを戻す。
                if (columnNames == null && columnTypes == null)
                {
                    return null;
                }
                // 一方はNullでもう一方はデータあり、又は、両方の数量は不一致の場合。
                else if ((columnNames == null && columnTypes != null) || (columnNames != null && columnTypes == null) || (columnNames.Length != columnTypes.Length))
                {
                    //throw new EdisonException("項目名数と項目取得元種別数は不一致です。");
                    LogUtility.Error("項目名数と項目取得元種別数は不一致です。");
                    return null;
                }
                else if (columnNames.Any(x => string.IsNullOrWhiteSpace(x)) || columnNames.Select(x => x.Trim()).Distinct().Count() < columnNames.Length)
                {
                    //throw new EdisonException("項目名に空文字又は重複項目があります。");
                    LogUtility.Error("項目名に空文字又は重複項目があります。");
                    return null;
                }

                var columns = new CsvColumns();
                for (int i = 0; i < columnNames.Length; i++)
                {
                    columns.Add(new CsvColumn(columnNames[i].Trim(), columnTypes[i]));
                }
                return columns;
            }

            /// <summary>
            ///
            /// </summary>
            public class CsvColumn : Tuple<string, SOURCE_TYPE>
            {
                internal string ColumnName { get { return base.Item1; } }
                internal SOURCE_TYPE ColumnType { get { return base.Item2; } }

                internal CsvColumn(string columnName, SOURCE_TYPE columnType)
                    : base(columnName, columnType)
                {
                }
            }

            /// <summary>
            /// 項目取得元種別
            /// </summary>
            /// <remarks>MRから又はDGVから又はDataTableから取得と示す</remarks>
            public enum SOURCE_TYPE
            {
                MR,
                DGV,
                DT
            }
        }

        public class CsvFormats : Dictionary<string, string>
        {
            private CsvFormats()
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="columnNames"></param>
            /// <param name="formatProviders"></param>
            /// <returns></returns>
            /// <remarks>
            /// 使い方：
            /// CreateCustomFormats({ "COL_A", "COL_B", "#3" }, { "#,##0", "yyyyMMdd", "#0" })
            /// "COL_A"と"COL_B"は列名と示し、"#3"は具体の列番号を指定したい場合を使う。
            /// </remarks>
            public static CsvFormats CreateCustomFormats(string[] columnNames, string[] formatProviders)
            {
                // 両方もNullの場合、Nullを戻す。
                if (columnNames == null && formatProviders == null)
                {
                    return null;
                }
                // 一方はNullでもう一方はデータあり、又は、両方の数量は不一致の場合。
                else if ((columnNames == null && formatProviders != null) || (columnNames != null && formatProviders == null) || (columnNames.Length != formatProviders.Length))
                {
                    //throw new EdisonException("項目名数とフォーマット文字項目数は不一致です。");
                    LogUtility.Error("項目名数とフォーマット文字項目数は不一致です。");
                    return null;
                }
                else if (columnNames.Any(x => string.IsNullOrWhiteSpace(x)) || columnNames.Select(x => x.Trim()).Distinct().Count() < columnNames.Length)
                {
                    //throw new EdisonException("項目名に空文字又は重複項目があります。");
                    LogUtility.Error("項目名に空文字又は重複項目があります。");
                    return null;
                }
                else if (formatProviders.Any(x => string.IsNullOrWhiteSpace(x)))
                {
                    //throw new EdisonException("フォーマット文字項目に空文字があります。");
                    LogUtility.Error("フォーマット文字項目に空文字があります。");
                    return null;
                }

                var formats = new CsvFormats();
                for (int i = 0; i < columnNames.Length; i++)
                {
                    formats[columnNames[i].Trim()] = formatProviders[i];
                }
                return formats;
            }
        }

        public class CsvHeaders : Dictionary<string, string>
        {
            private CsvHeaders()
            {
            }

            /// <summary>
            ///
            /// </summary>
            /// <param name="columnNames"></param>
            /// <param name="headerTexts"></param>
            /// <returns></returns>
            /// <remarks>
            /// 使い方：
            /// CreateCustomHeaders({ "COL_A", "COL_B", "#3" }, { "列A", "カラムＢ", "列C\r\n改行あり" })
            /// "COL_A"と"COL_B"は列名と示し、"#3"は具体の列番号を指定したい場合を使う。
            /// 又は、
            /// CreateCustomHeaders(mrC.Template.Columns.Select(
            ///     x => x.ColumnName).ToArray(), mrC.Template.Columns.Select(x => x.ColumnDispName).ToArray()
            ///     )
            /// </remarks>
            public static CsvHeaders CreateCustomHeaders(string[] columnNames, string[] headerTexts)
            {
                // 両方もNullの場合、Nullを戻す。
                if (columnNames == null && headerTexts == null)
                {
                    return null;
                }
                // 一方はNullでもう一方はデータあり、又は、両方の数量は不一致の場合。
                else if ((columnNames == null && headerTexts != null) || (columnNames != null && headerTexts == null) || (columnNames.Length != headerTexts.Length))
                {
                    //throw new EdisonException("項目名数とヘッダ表示文字項目数は不一致です。");
                    LogUtility.Error("項目名数とヘッダ表示文字項目数は不一致です。");
                    return null;
                }
                else if (columnNames.Any(x => string.IsNullOrWhiteSpace(x)) || columnNames.Select(x => x.Trim()).Distinct().Count() < columnNames.Length)
                {
                    //throw new EdisonException("項目名に空文字又は重複項目があります。");
                    LogUtility.Error("項目名に空文字又は重複項目があります。");
                    return null;
                }

                var headers = new CsvHeaders();
                for (int i = 0; i < columnNames.Length; i++)
                {
                    headers[columnNames[i].Trim()] = headerTexts[i];
                }
                return headers;
            }
        }

        #endregion 内部クラス
    }
}