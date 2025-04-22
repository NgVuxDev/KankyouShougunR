using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.CustomControl;
using r_framework.Utility;
using System.Data;
using System.Collections.Generic;

namespace Shougun.Core.Common.BusinessCommon.Utility
{
    /// <summary>
    /// グリッドの内容をCSVに出力します。
    /// </summary>
    public class CSVExport
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CSVExport()
        {
            //デフォルトはFALSE（改行等を消して出力）
            this.ForceHeaderOriginal = false;
        }

        /// <summary>
        /// ヘッダをそのまま出力するかどうか？
        /// TRUE:改行等あってもそのまま出力/FALSE:改行を消して出力（デフォルトはFALSE）
        /// </summary>
        public bool ForceHeaderOriginal { get; set; }

        /// <summary>
        /// CSV出力時の共通パス
        /// </summary>
        public static string InitialDirectory
        {
            get
            {
                return Properties.Settings.Default.CSVExportInitialDirectory;
            }
            set
            {
                Properties.Settings.Default.CSVExportInitialDirectory = value;
                Properties.Settings.Default.Save();
            }
        }

        //TODO:関数の移行が終わったら消したほうが良い
        /// <summary>
        /// 画面に表示されているデータをCSVに出力する
        /// </summary>
        /// <param name="inDgv">データ</param>
        /// <param name="isHeaderOutPut">ヘッダ出力制御</param>
        /// <param name="isLastRowOutPut">最後行出力制御</param>
        [Obsolete("ファイル名の初期値(画面名）を設定してください", false)]
        public void ConvertCustomDataGridViewToCsv(CustomDataGridView inDgv, Boolean isHeaderOutPut, Boolean isLastRowOutPut)
        {
            System.Diagnostics.Trace.Assert(false, "CSVExportの旧関数を呼び出しています。新関数にPG修正してください。");

            //とりあえず互換性保持
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //string defaultFilename = DateTime.Now.ToString("yyyyMMdd_HHmmss"); //デフォルト
            string defaultFilename = this.getDBDateTime().ToString("yyyyMMdd_HHmmss"); //デフォルト
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end

            Form form = inDgv.FindForm();
            if (form != null && form.Parent != null)
            {
                defaultFilename = form.Parent.Text; //スーパーフォームの上にいる、ベースフォームのtextをとりあえず利用。
            }

            ConvertCustomDataGridViewToCsv(inDgv, isHeaderOutPut, isLastRowOutPut, defaultFilename, (SuperForm)form);
        }

        /// <summary>
        /// データグリッドのデータをCSVに出力します
        /// </summary>
        /// <param name="inDgv">出力対象のデータグリッド</param>
        /// <param name="isHeaderOutPut">Trueの場合、ヘッダを出力</param>
        /// <param name="isLastRowOutPut">Trueの場合、最終行を出力</param>
        /// <param name="defaultFilename">ファイル名（日時と拡張子はメソッド内で付加）</param>
        /// <param name="form">呼び出し元のフォーム</param>
        /// <returns>CSVを出力した       = True
        ///          CSVを出力しなかった = False</returns>
        public bool ConvertCustomDataGridViewToCsv(CustomDataGridView inDgv, Boolean isHeaderOutPut, Boolean isLastRowOutPut, string defaultFilename, SuperForm form, bool bomFlg = true, List<string> notOutputColumns = null)
        {
            try
            {
                r_framework.Utility.LogUtility.DebugMethodStart(inDgv, isHeaderOutPut, isLastRowOutPut, defaultFilename, form, bomFlg, notOutputColumns);

                if (new CsvUtility(inDgv, form, defaultFilename, outputHeader: isHeaderOutPut, bomFlag: bomFlg, notOutputColumns: notOutputColumns).Output())
                {
                    MessageBox.Show("出力が完了しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                r_framework.Utility.LogUtility.DebugMethodEnd();
            }
        }

        public bool ConvertDataGridViewToJisCsvWithPath(CustomDataGridView inDgv, bool isHeaderOutPut, bool isLastRowOutPut, string filePath, string fileName, SuperForm form, List<string> notOutputColumns = null)
        {
            LogUtility.DebugMethodStart(inDgv, isHeaderOutPut, isLastRowOutPut, filePath, fileName, form, notOutputColumns);
            try
            {
                var shift_jis = Encoding.GetEncoding("Shift_JIS");
                var csvUtil = new CsvUtility(inDgv, form, fileName, outputHeader: isHeaderOutPut, encoding: shift_jis, notOutputColumns: notOutputColumns);
                if (csvUtil.OutputWithPath(filePath))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        // Begin: LANDUONG - 20220211 - refs#157800
        public bool ConvertDataTableToCsvRaku(DataTable inDgv, Boolean isHeaderOutPut, Boolean isLastRowOutPut, string defaultFilename, SuperForm form)
        {
            try
            {
                r_framework.Utility.LogUtility.DebugMethodStart(inDgv, isHeaderOutPut, isLastRowOutPut, defaultFilename, form);
                if (new CsvUtility(inDgv, form, defaultFilename, outputHeader: isHeaderOutPut).OutputNew())
                {
                    MessageBox.Show("出力が完了しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                r_framework.Utility.LogUtility.DebugMethodEnd();
            }
        }
        // End: LANDUONG - 20220211 - refs#157800

        /// <summary>
        /// データグリッドのデータをCSVに出力します
        /// </summary>
        /// <param name="inDgv">出力対象のデータグリッド</param>
        /// <param name="isHeaderOutPut">Trueの場合、ヘッダを出力</param>
        /// <param name="isLastRowOutPut">Trueの場合、最終行を出力</param>
        /// <param name="defaultFilename">ファイル名（日時と拡張子はメソッド内で付加）</param>
        /// <param name="form">呼び出し元のフォーム</param>
        /// <returns>CSVを出力した       = True
        ///          CSVを出力しなかった = False</returns>
        public bool ConvertDataTableToCsv(DataTable inDgv, Boolean isHeaderOutPut, Boolean isLastRowOutPut, string defaultFilename, SuperForm form, List<string> notOutputColumns = null)
        {
            try
            {
                r_framework.Utility.LogUtility.DebugMethodStart(inDgv, isHeaderOutPut, isLastRowOutPut, defaultFilename, form, notOutputColumns);

                if (new CsvUtility(inDgv, form, defaultFilename, outputHeader: isHeaderOutPut, notOutputColumns: notOutputColumns).Output())
                {
                    MessageBox.Show("出力が完了しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                r_framework.Utility.LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// データグリッドのデータをCSVに出力します
        /// </summary>
        /// <param name="inDgvList">出力対象のデータグリッド</param>
        /// <param name="isHeaderOutPut">Trueの場合、ヘッダを出力</param>
        /// <param name="isLastRowOutPut">Trueの場合、最終行を出力</param>
        /// <param name="defaultFilename">ファイル名（日時と拡張子はメソッド内で付加）</param>
        /// <param name="form">呼び出し元のフォーム</param>
        /// <returns>CSVを出力した       = True
        ///          CSVを出力しなかった = False</returns>
        public bool ConvertDataTableToManyCsv(List<DataTable> inDgvList, Boolean isHeaderOutPut, Boolean isLastRowOutPut, string defaultFilename, SuperForm form, List<string> notOutputColumns = null)
        {
            try
            {
                r_framework.Utility.LogUtility.DebugMethodStart(inDgvList, isHeaderOutPut, isLastRowOutPut, defaultFilename, form, notOutputColumns);

                if (new CsvUtility(inDgvList, form, defaultFilename, outputHeader: isHeaderOutPut, notOutputColumns: notOutputColumns).Output())
                {
                    MessageBox.Show("出力が完了しました。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IOException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                if (ex.Message.Contains("別のプロセスで使用されているため"))
                {
                    MessageBox.Show("ファイルのオープンに失敗しました。\r\n他のアプリケーションでファイルを開いている可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    throw; // 想定外の場合は再スローする
                }
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                r_framework.Utility.LogUtility.Error(ex);
                MessageBox.Show("ファイルのオープンに失敗しました。\r\n選択したファイルへの書き込み権限が無い可能性があります。", "CSV出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                r_framework.Utility.LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ファイル名に使用できない文字を使用できる文字に変換します
        /// </summary>
        /// <param name="fileName">ファイル名</param>
        /// <returns>変換後のファイル名</returns>
        private static string EscapeFileName(string fileName)
        {
            string ret = fileName;

            ret = ret.Replace("/", "／");
            ret = ret.Replace("\\", "￥");
            ret = ret.Replace(":", "：");
            ret = ret.Replace("*", "＊");
            ret = ret.Replace("?", "？");
            ret = ret.Replace("\"", "”");
            ret = ret.Replace("<", "＜");
            ret = ret.Replace(">", "＞");
            ret = ret.Replace("|", "｜");
            ret = ret.Replace("#", "＃");
            ret = ret.Replace("{", "｛");
            ret = ret.Replace("}", "｝");
            ret = ret.Replace("%", "％");
            ret = ret.Replace("&", "＆");
            ret = ret.Replace("~", "～");
            ret = ret.Replace(".", "．");

            return ret;
        }

        /// <summary>
        /// 項目をチェックして、必要あればダブルクオートをつける。"は""に変換する。
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static string CSVQuote(string m)
        {
            if (string.IsNullOrEmpty(m))
                return "";
            else
            {
                //"で囲む必要があるか調べる
                if (m.IndexOf('"') > -1 ||
                    m.IndexOf(',') > -1 ||
                    m.IndexOf('\r') > -1 ||
                    m.IndexOf('\n') > -1 ||
                    m.StartsWith(" ") || m.StartsWith("\t") ||
                    m.EndsWith(" ") || m.EndsWith("\t"))
                {
                    if (m.IndexOf('"') > -1)
                    {
                        //"を""とする
                        m = m.Replace("\"", "\"\"");
                    }
                    m = "\"" + m + "\"";
                }

                return m;
            }
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            r_framework.Dao.GET_SYSDATEDao dateDao = DaoInitUtility.GetComponent<r_framework.Dao.GET_SYSDATEDao>();
            System.Data.DataTable dt = dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}