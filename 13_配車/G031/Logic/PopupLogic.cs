using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Utility;
using r_framework.CustomControl;
using r_framework.OriginalException;
using System.Text;
using Seasar.Framework.Exceptions;
using Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku;
using Shougun.Core.Common.BusinessCommon.Utility;

namespace Shougun.Core.Allocation.CourseHaishaIraiNyuuryoku
{
    public class PopupLogic
    {
        internal SuperEntity[] entity { get; set; }

        private PopupForm form;

        private PopupDAOCls dao;

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
            this.dao = DaoInitUtility.GetComponent<PopupDAOCls>();

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

                this.SetHeaderText();

                this.EventInit();

                this.form.SAGYOU_DATE.Text = this.form.popupDto.SAGYOU_DATE;

                this.Search();

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
            // 選択ボタン(F9)イベント生成
            this.form.bt_func9.Click += new EventHandler(this.form.Selected);

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
                //先頭にブランク行の追加を行う
                DataRow row = stringDT.NewRow();
                for (int i = 0; i < stringDT.Columns.Count; i++)
                {
                    //先頭にブランク行の追加を行うためにEmptyにて初期化する
                    row[i] = string.Empty;
                }
                stringDT.Rows.InsertAt(row, 0);

                //DataGridViewへデータの設定
                this.form.Ichiran.DataSource = stringDT;

                // 列タイトル設定
                if (columnsHeaderTitle != null)
                {
                    for (var i = 0; i < columnsHeaderTitle.Length; i++)
                    {
                        this.form.Ichiran.Columns[i].HeaderText = columnsHeaderTitle[i];
                    }
                }

                foreach (DataGridViewRow Rows in this.form.Ichiran.Rows)
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
        /// マスタデータの検索を行い加工する
        /// </summary>
        internal bool Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable dt = new DataTable();
                if (this.form.popupDto.courseOnly)
                {
                    dt = this.dao.GetCoursePopupData(this.form.popupDto);
                }
                else
                {
                    dt = this.dao.GetPopupData(this.form.popupDto);
                }
                this.DataGridViewLoad(dt, this.headerList.ToArray());

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
        /// multiRowHeaderの初期化を行う
        /// </summary>
        private void SetHeaderText()
        {
            string title = string.Empty;
            if (this.form.popupDto.courseOnly)
            {
                headerList.Add("コースCD");
                headerList.Add("コース名");
                title = "コース検索";
                this.form.SAGYOU_DATE_LABEL.Visible = false;
                this.form.SAGYOU_DATE.Visible = false;
                this.form.Ichiran.Location = new System.Drawing.Point(12, 43);
                this.form.Ichiran.Size = new System.Drawing.Size(400, 356);
            }
            else
            {
                headerList.Add("定期配車番号");
                headerList.Add("コースCD");
                headerList.Add("コース名");
                title = "定期配車検索";
            }

            //タイトルラベルの強制変更対応
            if (!string.IsNullOrEmpty(this.form.PopupTitleLabel))
            {
                title = this.form.PopupTitleLabel;
                this.form.lb_title.Text = title;
                this.form.Text = title;

                ControlUtility.AdjustTitleSize(this.form.lb_title, this.TitleMaxWidth);
            }
            else
            {
                this.form.lb_title.Text = title;
                this.form.Text = title;
            }
        }

        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 658;

        internal bool ElementDecision()
        {
            try
            {
                LogUtility.DebugMethodStart();

                Dictionary<int, List<PopupReturnParam>> setParamList = new Dictionary<int, List<PopupReturnParam>>();
                List<PopupReturnParam> setParam;
                for (int i = 0; i < this.form.Ichiran.Columns.Count; i++)
                {
                    PopupReturnParam popupParam = new PopupReturnParam();
                    var setDate = this.form.Ichiran[i, this.form.Ichiran.CurrentRow.Index];

                    //var control = setDate as ICustomControl;
                    var control = setDate as DataGridViewTextBoxCell;

                    popupParam.Key = "Value";

                    popupParam.Value = setDate.Value.ToString();

                    if (setParamList.ContainsKey(i))
                    {
                        setParam = setParamList[i];
                    }
                    else
                    {
                        setParam = new List<PopupReturnParam>();
                    }

                    setParam.Add(popupParam);


                    setParamList.Add(i, setParam);
                }

                this.form.ReturnParams = setParamList;
                this.form.Close();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ElementDecision", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
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

                int columnCount = this.form.Ichiran.ColumnCount;
                // 列数0の場合は何もしない
                if (columnCount == 0)
                {
                    return false;
                }

                // 垂直スクロールバーのプロパティを取得
                var pi = this.form.Ichiran.GetType().GetProperty("VerticalScrollBar", BindingFlags.NonPublic | BindingFlags.Instance);
                var vsb = (VScrollBar)pi.GetValue(this.form.Ichiran, null);

                // セルを表示可能な領域の横幅を計算する
                int dgvWidth = this.form.Ichiran.Width;
                if (vsb.Visible)
                {
                    dgvWidth -= vsb.Width;
                }

                // 各カラムの横幅を指定する
                int columnWidth = (dgvWidth / columnCount) - SystemInformation.BorderSize.Width; // 線の太さを考慮
                for (int i = 0; i < columnCount; i++)
                {
                    this.form.Ichiran.Columns[i].Width = columnWidth; // 横幅指定
                    this.form.Ichiran.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter; // ヘッダ中央揃え
                }
                // 余りを1列目に足す
                this.form.Ichiran.Columns[0].Width += dgvWidth % columnCount;

                // セル内折り返し(半角英数字の羅列は折り返されない仕様)
                this.form.Ichiran.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                // 縦幅は自動調節
                this.form.Ichiran.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

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
    }
}
