
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Utility;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuTeiki
{
    public class PopupLogic
    {
        internal SuperEntity[] entity { get; set; }

        private PopupForm form;

        private IS2Dao dao;

        private List<string> headerList { get; set; }

        internal Control[] popupViewControls { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PopupLogic(PopupForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            headerList = new List<string>();
            this.form = targetForm;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化
        /// </summary>
        internal bool WindowInit()
        {
            try
            {
                this.form.KeyPreview = true;

                this.EventInit();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// イベント初期化
        /// </summary>
        private void EventInit()
        {
            //閉じるボタン(F12)イベント生成
            this.form.bt_func12.Click += new EventHandler(this.form.FormClose);
        }

        /// <summary>
        /// DataGridViewにデータをセット
        /// </summary>
        /// <param name="sourceDT">マスタデータを格納したデータテーブル</param>
        /// <param name="columnsHeaderTitle">Gridのタイトル列テキスト</param>
        internal bool DataGridViewLoad(DataTable sourceDT, string[] columnsHeaderTitle)
        {
            try
            {
                LogUtility.DebugMethodStart(sourceDT, columnsHeaderTitle);

                // データテーブルが空で渡された場合、空行を追加する
                if (columnsHeaderTitle != null && sourceDT.Columns.Count == 0)
                {
                    for (int i = 0; i < columnsHeaderTitle.Length; i++)
                    {
                        sourceDT.Columns.Add(new DataColumn());
                    }
                }

                DataTable stringDT = GetStringDataTable(sourceDT);

                //DataGridViewへデータの設定
                this.form.customDataGridView1.DataSource = stringDT;

                // 列タイトル設定
                if (columnsHeaderTitle != null)
                {
                    for (var i = 0; i < columnsHeaderTitle.Length; i++)
                    {
                        this.form.customDataGridView1.Columns[i].HeaderText = columnsHeaderTitle[i];
                    }
                }

                foreach (DataGridViewRow Rows in this.form.customDataGridView1.Rows)
                {
                    foreach (DataGridViewCell cell in Rows.Cells)
                    {
                        cell.ReadOnly = true;
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DataGridViewLoad", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 先頭カラムが文字列のDataTableの取得
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetStringDataTable(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].DataType = typeof(string);
            }

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            LogUtility.DebugMethodEnd(table);

            return table;
        }

        /// <summary>
        /// Int型のコードを0埋めします。
        /// （今のところ拠点CDのみ）
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable CodeZeroPadding(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            int cdIndex = -1;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName == "CD" || dt.Columns[i].ColumnName == "KYOTEN_CD" || dt.Columns[i].ColumnName == "M_KYOTEN.KYOTEN_CD")
                {
                    cdIndex += i + 1;
                    break;
                }
            }
            if (cdIndex == -1)
            {
                return dt;
            }
            DataTable resultDt = dt.Clone();
            resultDt.Columns[cdIndex].DataType = typeof(string);
            string paddingCd = null;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow row = resultDt.NewRow();
                paddingCd = int.Parse(dt.Rows[i][cdIndex].ToString()).ToString("00");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    if (j != cdIndex)
                    {
                        row[j] = dt.Rows[i][j];
                    }
                    else
                    {
                        row[j] = paddingCd;
                    }
                }
                resultDt.Rows.Add(row);
            }

            LogUtility.DebugMethodEnd(resultDt);
            return resultDt;
        }

        /// <summary>
        /// グリッドビューの表示を整えます
        /// （セル内折り返し、セル縦横幅調整）
        /// </summary>
        internal bool CordinateGridSize()
        {
            try
            {
                LogUtility.DebugMethodStart();

                int columnCount = this.form.customDataGridView1.ColumnCount;
                // 列数0の場合は何もしない
                if (columnCount == 0)
                {
                    return false;
                }

                // 垂直スクロールバーのプロパティを取得
                var pi = this.form.customDataGridView1.GetType().GetProperty("VerticalScrollBar", BindingFlags.NonPublic | BindingFlags.Instance);
                var vsb = (VScrollBar)pi.GetValue(this.form.customDataGridView1, null);

                // セルを表示可能な領域の横幅を計算する
                int dgvWidth = this.form.customDataGridView1.Width;
                if (vsb.Visible)
                {
                    dgvWidth -= vsb.Width;
                }

                // 各カラムの横幅を個別に指定する
                int totalWidth = 0;
                int columnWidth = 0;

                // 業者CD
                columnWidth = (60) - SystemInformation.BorderSize.Width; // 線の太さを考慮
                this.form.customDataGridView1.Columns[0].Width = columnWidth; // 横幅指定
                this.form.customDataGridView1.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // ヘッダ中央揃え
                totalWidth += columnWidth;
                // 業者名
                columnWidth = (290) - SystemInformation.BorderSize.Width; // 線の太さを考慮
                this.form.customDataGridView1.Columns[1].Width = columnWidth; // 横幅指定
                this.form.customDataGridView1.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // ヘッダ中央揃え
                totalWidth += columnWidth;
                // 現場CD
                columnWidth = (60) - SystemInformation.BorderSize.Width; // 線の太さを考慮
                this.form.customDataGridView1.Columns[2].Width = columnWidth; // 横幅指定
                this.form.customDataGridView1.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // ヘッダ中央揃え
                totalWidth += columnWidth;
                // 現場名
                columnWidth = (290) - SystemInformation.BorderSize.Width; // 線の太さを考慮
                this.form.customDataGridView1.Columns[3].Width = columnWidth; // 横幅指定
                this.form.customDataGridView1.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // ヘッダ中央揃え
                totalWidth += columnWidth;

                // 残りを現場名に足す
                this.form.customDataGridView1.Columns[3].Width += dgvWidth - totalWidth - SystemInformation.BorderSize.Width - SystemInformation.BorderSize.Width;

                // セル内折り返し(半角英数字の羅列は折り返されない仕様)
                this.form.customDataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // 縦幅は自動調節
                this.form.customDataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CordinateGridSize", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        private string ReplaceSpecialCharacers(string value)
        {
            StringBuilder sb = new StringBuilder(value.Length);
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                switch (c)
                {
                    case ']':
                    case '[':
                    case '%':
                    case '*':
                        sb.Append("[").Append(c).Append("]");
                        break;
                    case '\'':
                        sb.Append("''");
                        break;
                    default:
                        sb.Append(c);
                        break;
                }
            }
            return sb.ToString();
        }
    }
}
