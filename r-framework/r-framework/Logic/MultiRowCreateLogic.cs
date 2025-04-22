
using System.Collections.Generic;
using System.Data;
using GrapeCity.Win.MultiRow;
using r_framework.CustomControl;
using System.Windows.Forms;
using System;
using System.Linq;
using r_framework.Utility;


namespace r_framework.Logic
{
    /// <summary>
    /// MultiRowの生成ロジック
    /// </summary>
    public class MultiRowCreateLogic
    {
        /// <summary>
        /// MultiRowクリエイト処理
        /// </summary>
        /// <param name="headerList">ヘッダーの名称リスト</param>
        /// <param name="table">DBから取得したデータ</param>
        /// <returns>作成したMultiRowのテンプレート</returns>
        public Template CreateMultiRow(List<string> headerList, DataTable table)
        {
            List<ColumnHeaderCell> headerCellList = new List<ColumnHeaderCell>();
            List<GcCustomTextBoxCell> cellList = new List<GcCustomTextBoxCell>();
            GcCustomMultiRow multirow = new GcCustomMultiRow();
            ColumnHeaderSection headerSection = new ColumnHeaderSection();
            int locationX = 0;
            int locationY = 0;
            int colmunWidth = 0;
            GrapeCity.Win.MultiRow.Template tmplate = new Template();
            for (int i = 0; i < table.Columns.Count; i++)
            {

                colmunWidth = this.CalcWidthSize(table, i, headerList[i]);
                //ヘッダーの動的生成
                ColumnHeaderCell headerCell = new ColumnHeaderCell();
                CellStyle cellStyle1 = new CellStyle();
                Border border1 = new Border();

                headerCell.FilterCellName = headerList[i];
                headerCell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                headerCell.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;

                headerCell.Name = headerList[i];
                headerCell.Value = headerList[i];
                headerCell.Location = new System.Drawing.Point(locationX, locationY);
                headerCell.Size = new System.Drawing.Size(colmunWidth, 21);
                cellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
                border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.White);
                cellStyle1.Border = border1;
                cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                cellStyle1.ForeColor = System.Drawing.Color.White;
                cellStyle1.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
                headerCell.Style = cellStyle1;
                headerSection.Cells.Add(headerCell);

                CellStyle cellStyle2 = new CellStyle();
                GcCustomTextBoxCell textBoxCell = new GcCustomTextBoxCell();
                textBoxCell.Name = table.Columns[i].ColumnName;
                textBoxCell.DataField = table.Columns[i].ColumnName;

                textBoxCell.ShortItemName = i.ToString();
                textBoxCell.Location = new System.Drawing.Point(locationX, locationY);
                textBoxCell.Size = new System.Drawing.Size(colmunWidth, 21);
                cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
                cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
                textBoxCell.Style = cellStyle2;
                tmplate.Row.Cells.Add(textBoxCell);
                tmplate.Row.Height = 21;

                locationX += colmunWidth;
            }
            headerSection.Name = "columnHeaderSection1";
            headerSection.Height = 21;

            tmplate.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] { headerSection });
            tmplate.Width = locationX;
            return tmplate;
        }

        /// <summary>
        /// カラムサイズを計算
        /// </summary>
        /// <param name="table">DBから取得したデータ</param>
        /// <param name="index">対象行番号</param>
        /// <param name="header">ヘッダー</param>
        /// <returns></returns>
        private int CalcWidthSize(DataTable table, int index, string header)
        {
            int width = 0;
            using (System.Drawing.Font font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F))
            {
                foreach (DataRow row in table.Rows)
                {
                    var obj = row[index];
                    if (obj == null)
                    {
                        continue;
                    }
                    string text = obj.ToString();

                    var size = System.Windows.Forms.TextRenderer.MeasureText(text, font);
                    // 若干誤差があるため余裕を持たせる
                    int sizeWidth = size.Width + 15;

                    if (width < sizeWidth)
                    {
                        width = sizeWidth;
                    }
                }

                // ヘッダと比較
                var headerSize = System.Windows.Forms.TextRenderer.MeasureText(header, font);
                int headerWidth = headerSize.Width + 15;
                if (width < headerWidth)
                {
                    width = headerWidth;
                }
            }

            return width;
        }
        
        public DataTable CreateDetailList(object param)
        {
            if (param is GcCustomMultiRow) return CreateDetailListInner((GcCustomMultiRow)param);
            if (param is CustomDataGridView) return CreateDetailListInner((CustomDataGridView)param);
            return null;
        }


        /// <summary>
        /// 一覧データ作成(MultiRow用)
        /// </summary>
        public DataTable CreateDetailListInner(GcCustomMultiRow mr)
        {

            MultiRowIndexCreateLogic multirowLocationLogic = new MultiRowIndexCreateLogic();
            multirowLocationLogic.multiRow = mr;

            multirowLocationLogic.CreateLocations();

            DataTable dt = CreateDataTable();

            foreach (var dto in multirowLocationLogic.sortEndList)
            {
                var index = dto.Cells.CellIndex;
                var customHeaderCell = mr.ColumnHeaders[0].Cells[index] as GcCustomColumnHeader;

               
                if (customHeaderCell != null)
                {

                    //非表示も出さない
                    // →2016/02/08 ViewSearchItem=False のみ出さないように変更
                    if (!customHeaderCell.ViewSearchItem)
                    {
                        continue;
                    }
                }
                else
                {
                    //コーナーや カスタムヘッダ以外は無視
                    continue;
                }


                Cell cell = mr.Template.Row.Cells[index];

                var customCont = cell as ICustomControl;

                if (customCont == null) continue; //ボタン等のカスタム以外は出さない

                string name = customHeaderCell.Value.ToString();
                //ImeMode ime = GetIME(name, cell);
                string data = cell.DataField;
                string itemDef = customCont.ItemDefinedTypes;
                ImeMode ime = GetIME(name, itemDef);

                //列名未設定は出さない（ソートできないので）
                if (string.IsNullOrEmpty(data))
                {
                    continue;
                }

                dt.Rows.Add(name, ime, data, itemDef);
            }

            //   this.form.MasterItem.DataSource = dt;
            return dt;
        }

        /// <summary>
        /// 一覧データ作成(DataGridView用)
        /// </summary>
        public DataTable CreateDetailListInner(CustomDataGridView dgv)
        {

           DataGridViewColumn[] cols = new DataGridViewColumn[dgv.Columns.Count];
           dgv.Columns.CopyTo(cols, 0);

            DataTable dt = CreateDataTable();
            

            foreach (var col in cols.OrderBy(n => n.DisplayIndex))
            {
                var index = col.DisplayIndex;

                //非表示、またはViewSearchItemがFalseのものは表示しない
                var icol = col as ICustomeDataGridViewColmun;
                if (!col.Visible || (icol != null && !icol.ViewSearchItem))
                {
                    continue;
                }

                string name = col.HeaderText;
                //ImeMode ime = GetIME(name, col);
                string data = PropertyUtility.GetString(col, "DBFieldsName");
                string itemDef = PropertyUtility.GetString(col, "ItemDefinedTypes");
                ImeMode ime = GetIME(name, itemDef);
                dt.Rows.Add(name, ime, data, itemDef);
            }

            //this.form.MasterItem.DataSource = dt;
            return dt;
        }

        /// <summary>
        /// データテーブル作成
        /// </summary>
        /// <returns></returns>
        public DataTable CreateDataTable()
        {
            var dt = new DataTable();

            DataColumn dcItem = new DataColumn("Item", typeof(string));//項目名
            DataColumn dcImeMode = new DataColumn("ImeMode", typeof(ImeMode));
            DataColumn dcDataField = new DataColumn("DBFieldsName", typeof(string));
            DataColumn dcItemTypes = new DataColumn("ItemDefinedTypes", typeof(string));

            dt.Columns.Add(dcItem);
            dt.Columns.Add(dcImeMode);
            dt.Columns.Add(dcDataField);
            dt.Columns.Add(dcItemTypes);

            return dt;
        }

        /// <summary>
        /// IMEの取得
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public ImeMode GetIME(string name, Cell cell)
        {
            if (!cell.ReadOnly)
            {
                return cell.Style.ImeMode;
            }

            // 表示専用であればヘッダ名称から推測して判定
            if (name.Contains("CD")
                || name.Equals("更新日")
                || name.Equals("作成日"))
            {
                return ImeMode.Disable;
            }
            else if (name.Contains("フリガナ"))
            {
                return ImeMode.Katakana;
            }
            else if (cell is GcCustomDataTime)
            {
                return cell.Style.ImeMode;
            }
            else
            {
                return ImeMode.Hiragana;
            }
        }
        /// <summary>
        /// IMEの取得
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public ImeMode GetIME(string name, DataGridViewColumn col)
        {

            //ヘッダ名称から推測して判定
            if (String.IsNullOrEmpty(name)
                || name.Contains("CD")
                || name.Equals("更新日")
                || name.Equals("作成日"))
            {
                return ImeMode.Disable;
            }
            else if (name.Contains("フリガナ"))
            {
                return ImeMode.Katakana;
            }
            else
            {
                return ImeMode.Hiragana;
            }

        }

        /// <summary>
        /// IMEの取得 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cell"></param>
        /// <returns></returns>
        public ImeMode GetIME(string name, string itemDef)
        {

            //ヘダ_名称から推測して判定
            if (String.IsNullOrEmpty(name)
                || name.Contains("CD")
                || name.Equals("更新日")
                || name.Equals("作成日"))
            {
                return ImeMode.Disable;
            }
            else if (name.Contains("フリガナ"))
            {
                return ImeMode.Katakana;
            }

            //ItemDefinedTypesでA判â定e
            if (String.IsNullOrEmpty(itemDef)
                || itemDef.Equals("smallint")
                || itemDef.Equals("float")
                || itemDef.Equals("decimal"))
            {
                return ImeMode.Disable;
            }
            else
            {
                return ImeMode.Hiragana;
            }
        }
        /// <summary>
        /// MultiRowクリエイト処理
        /// </summary>
        /// <param name="headerList">ヘッダーの名称リスト</param>
        /// <param name="table">DBから取得したデータ</param>
        /// <param name="addSelectCol">選択行の追加</param>
        /// <returns>作成したMultiRowのテンプレート</returns>
        public Template CreateMultiRowUnpan(List<string> headerList, DataTable table, bool addSelectCol)
        {
            List<ColumnHeaderCell> headerCellList = new List<ColumnHeaderCell>();
            List<GcCustomTextBoxCell> cellList = new List<GcCustomTextBoxCell>();
            GcCustomMultiRow multirow = new GcCustomMultiRow();
            ColumnHeaderSection headerSection = new ColumnHeaderSection();
            int locationX = 0;
            int locationY = 0;
            int colmunWidth = 0;
            GrapeCity.Win.MultiRow.Template tmplate = new Template();

            if (addSelectCol)
            {
                // ヘッダー
                var SelectHeader = new ColumnHeaderCell();
                SelectHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                SelectHeader.Location = new System.Drawing.Point(locationX, locationY);
                SelectHeader.Name = "SelectHeader";
                SelectHeader.Size = new System.Drawing.Size(24, 21);
                var cellStyleSelectHearder = new CellStyle();
                cellStyleSelectHearder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
                var borderSelectHearder = new Border();
                borderSelectHearder.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
                cellStyleSelectHearder.Border = borderSelectHearder;
                cellStyleSelectHearder.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                cellStyleSelectHearder.ForeColor = System.Drawing.Color.White;
                cellStyleSelectHearder.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
                SelectHeader.Style = cellStyleSelectHearder;
                SelectHeader.TabIndex = 1;
                SelectHeader.Value = "選";
                headerSection.Cells.Add(SelectHeader);
                // セル
                GcCustomCheckBoxCell CHECKED;
                CHECKED = new r_framework.CustomControl.GcCustomCheckBoxCell();
                CHECKED.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
                CHECKED.DataField = "CHECKED";
                CHECKED.DBFieldsName = null;
                CHECKED.DefaultBackColor = System.Drawing.Color.Empty;
                CHECKED.DisplayItemName = null;
                CHECKED.ErrorMessage = null;
                CHECKED.GetCodeMasterField = null;
                CHECKED.ItemDefinedTypes = null;
                CHECKED.Location = new System.Drawing.Point(locationX, locationY);
                CHECKED.Name = "CHECKED";
                CHECKED.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
                CHECKED.PopupWindowName = null;
                CHECKED.SearchDisplayFlag = 0;
                CHECKED.SetFormField = null;
                CHECKED.ShortItemName = null;
                CHECKED.Size = new System.Drawing.Size(24, 21);
                var cellStyleCheckCell = new CellStyle();
                cellStyleCheckCell.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                cellStyleCheckCell.ImeMode = System.Windows.Forms.ImeMode.Disable;
                cellStyleCheckCell.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
                CHECKED.Style = cellStyleCheckCell;
                CHECKED.TabIndex = 1;
                CHECKED.ZeroPaddengFlag = false;
                tmplate.Row.Cells.Add(CHECKED);
                // 幅
                locationX += SelectHeader.Size.Width;
            }

            for (int i = 0; i < table.Columns.Count-1; i++)
            {
                if (headerList[i].Contains("_VISIBLE_FALSE"))
                {
                    // 非表示列(幅0で対応)
                    colmunWidth = 0;
                }
                else
                {
                    colmunWidth = this.CalcWidthSize(table, i, headerList[i]);
                }
                //ヘッダーの動的生成
                ColumnHeaderCell headerCell = new ColumnHeaderCell();
                CellStyle cellStyle1 = new CellStyle();
                Border border1 = new Border();

                headerCell.FilterCellName = headerList[i];
                headerCell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                headerCell.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;

                headerCell.Name = headerList[i];
                headerCell.Value = headerList[i];
                headerCell.Location = new System.Drawing.Point(locationX, locationY);
                headerCell.Size = new System.Drawing.Size(colmunWidth, 21);
                cellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
                border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.White);
                cellStyle1.Border = border1;
                cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                cellStyle1.ForeColor = System.Drawing.Color.White;
                cellStyle1.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
                headerCell.Style = cellStyle1;
                if (headerCell.Name.ToString() == "許可KBN"
                    || headerCell.Name.ToString() == "業者CD"
                    || headerCell.Name.ToString() == "現場CD"
                    || headerCell.Name.ToString() == "地域CD"
                    || headerCell.Name.ToString() == "特別管理KBN")
                {
                    headerCell.Visible = false;
                }
                headerSection.Cells.Add(headerCell);

                CellStyle cellStyle2 = new CellStyle();
                GcCustomTextBoxCell textBoxCell = new GcCustomTextBoxCell();
                textBoxCell.Name = table.Columns[i].ColumnName;
                textBoxCell.DataField = table.Columns[i].ColumnName;

                textBoxCell.ShortItemName = i.ToString();
                textBoxCell.Location = new System.Drawing.Point(locationX, locationY);
                textBoxCell.Size = new System.Drawing.Size(colmunWidth, 21);
                cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
                cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
                textBoxCell.Style = cellStyle2;
                if (textBoxCell.Name.ToString() == "KYOKA_KBN"
                    || textBoxCell.Name.ToString() == "GYOUSHA_CD"
                    || textBoxCell.Name.ToString() == "GENBA_CD"
                    || textBoxCell.Name.ToString() == "CHIIKI_CD"
                    || textBoxCell.Name.ToString() == "TOKUBETSU_KANRI_KBN")
                {
                    textBoxCell.Visible = false;
                }
                tmplate.Row.Cells.Add(textBoxCell);
                tmplate.Row.Height = 21;

                if (textBoxCell.Name.ToString() != "KYOKA_KBN"
                    && textBoxCell.Name.ToString() != "GYOUSHA_CD"
                    && textBoxCell.Name.ToString() != "GENBA_CD"
                    && textBoxCell.Name.ToString() != "CHIIKI_CD"
                    && textBoxCell.Name.ToString() != "TOKUBETSU_KANRI_KBN")
                {
                    locationX += colmunWidth;
                }
            }

            //ヘッダーの動的生成
            ColumnHeaderCell headerCell1 = new ColumnHeaderCell();
            CellStyle cellStyle3 = new CellStyle();
            Border border2 = new Border();

            headerCell1.FilterCellName = "積";
            headerCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            headerCell1.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;

            headerCell1.Name = "積";
            headerCell1.Value = "積";
            headerCell1.Location = new System.Drawing.Point(locationX, locationY);
            headerCell1.Size = new System.Drawing.Size(24, 21);
            cellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.White);
            cellStyle3.Border = border2;
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ForeColor = System.Drawing.Color.White;
            cellStyle3.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            headerCell1.Style = cellStyle3;
            headerSection.Cells.Add(headerCell1);
            // セル
            GcCustomCheckBoxCell CHECKED1;
            CHECKED1 = new r_framework.CustomControl.GcCustomCheckBoxCell();
            CHECKED1.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            CHECKED1.DataField = "TSUMIKAE";
            CHECKED1.DBFieldsName = null;
            CHECKED1.DefaultBackColor = System.Drawing.Color.Empty;
            CHECKED1.DisplayItemName = null;
            CHECKED1.ErrorMessage = null;
            CHECKED1.GetCodeMasterField = null;
            CHECKED1.ItemDefinedTypes = null;
            CHECKED1.Location = new System.Drawing.Point(locationX, locationY);
            CHECKED1.Name = "TSUMIKAE";
            CHECKED1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            CHECKED1.PopupWindowName = null;
            CHECKED1.SearchDisplayFlag = 0;
            CHECKED1.SetFormField = null;
            CHECKED1.ShortItemName = null;
            CHECKED1.Size = new System.Drawing.Size(24, 21);
            var cellStyleCheckCell1 = new CellStyle();
            cellStyleCheckCell1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyleCheckCell1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyleCheckCell1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            CHECKED1.Style = cellStyleCheckCell1;
            CHECKED1.TabIndex = 1;
            CHECKED1.ZeroPaddengFlag = false;
            tmplate.Row.Cells.Add(CHECKED1);
            locationX += 24;

            headerSection.Name = "columnHeaderSection1";
            headerSection.Height = 21;

            tmplate.ColumnHeaders.Clear();
            tmplate.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] { headerSection });
            tmplate.Width = locationX;
            return tmplate;
        }

        /// <summary>
        /// MultiRowクリエイト処理
        /// </summary>
        /// <param name="headerList">ヘッダーの名称リスト</param>
        /// <param name="table">DBから取得したデータ</param>
        /// <param name="addSelectCol">選択行の追加</param>
        /// <returns>作成したMultiRowのテンプレート</returns>
        public Template CreateMultiRowSbn(List<string> headerList, DataTable table, bool addSelectCol)
        {
            List<ColumnHeaderCell> headerCellList = new List<ColumnHeaderCell>();
            List<GcCustomTextBoxCell> cellList = new List<GcCustomTextBoxCell>();
            GcCustomMultiRow multirow = new GcCustomMultiRow();
            ColumnHeaderSection headerSection = new ColumnHeaderSection();
            int locationX = 0;
            int locationY = 0;
            int colmunWidth = 0;
            GrapeCity.Win.MultiRow.Template tmplate = new Template();

            if (addSelectCol)
            {
                // ヘッダー
                var SelectHeader = new ColumnHeaderCell();
                SelectHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                SelectHeader.Location = new System.Drawing.Point(locationX, locationY);
                SelectHeader.Name = "SelectHeader";
                SelectHeader.Size = new System.Drawing.Size(24, 21);
                var cellStyleSelectHearder = new CellStyle();
                cellStyleSelectHearder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
                var borderSelectHearder = new Border();
                borderSelectHearder.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
                cellStyleSelectHearder.Border = borderSelectHearder;
                cellStyleSelectHearder.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                cellStyleSelectHearder.ForeColor = System.Drawing.Color.White;
                cellStyleSelectHearder.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
                SelectHeader.Style = cellStyleSelectHearder;
                SelectHeader.TabIndex = 1;
                SelectHeader.Value = "選";
                headerSection.Cells.Add(SelectHeader);
                // セル
                GcCustomCheckBoxCell CHECKED;
                CHECKED = new r_framework.CustomControl.GcCustomCheckBoxCell();
                CHECKED.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
                CHECKED.DataField = "CHECKED";
                CHECKED.DBFieldsName = null;
                CHECKED.DefaultBackColor = System.Drawing.Color.Empty;
                CHECKED.DisplayItemName = null;
                CHECKED.ErrorMessage = null;
                CHECKED.GetCodeMasterField = null;
                CHECKED.ItemDefinedTypes = null;
                CHECKED.Location = new System.Drawing.Point(locationX, locationY);
                CHECKED.Name = "CHECKED";
                CHECKED.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
                CHECKED.PopupWindowName = null;
                CHECKED.SearchDisplayFlag = 0;
                CHECKED.SetFormField = null;
                CHECKED.ShortItemName = null;
                CHECKED.Size = new System.Drawing.Size(24, 21);
                var cellStyleCheckCell = new CellStyle();
                cellStyleCheckCell.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                cellStyleCheckCell.ImeMode = System.Windows.Forms.ImeMode.Disable;
                cellStyleCheckCell.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
                CHECKED.Style = cellStyleCheckCell;
                CHECKED.TabIndex = 1;
                CHECKED.ZeroPaddengFlag = false;
                tmplate.Row.Cells.Add(CHECKED);
                // 幅
                locationX += SelectHeader.Size.Width;
            }

            for (int i = 0; i < table.Columns.Count; i++)
            {

                colmunWidth = this.CalcWidthSize(table, i, headerList[i]);
                //ヘッダーの動的生成
                ColumnHeaderCell headerCell = new ColumnHeaderCell();
                CellStyle cellStyle1 = new CellStyle();
                Border border1 = new Border();

                headerCell.FilterCellName = headerList[i];
                headerCell.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                headerCell.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;

                headerCell.Name = headerList[i];
                headerCell.Value = headerList[i];
                headerCell.Location = new System.Drawing.Point(locationX, locationY);
                headerCell.Size = new System.Drawing.Size(colmunWidth, 21);
                cellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
                border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.White);
                cellStyle1.Border = border1;
                cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                cellStyle1.ForeColor = System.Drawing.Color.White;
                cellStyle1.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
                headerCell.Style = cellStyle1;
                headerSection.Cells.Add(headerCell);

                CellStyle cellStyle2 = new CellStyle();
                GcCustomTextBoxCell textBoxCell = new GcCustomTextBoxCell();
                textBoxCell.Name = table.Columns[i].ColumnName;
                textBoxCell.DataField = table.Columns[i].ColumnName;

                textBoxCell.ShortItemName = i.ToString();
                textBoxCell.Location = new System.Drawing.Point(locationX, locationY);
                textBoxCell.Size = new System.Drawing.Size(colmunWidth, 21);
                cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
                cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
                cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
                textBoxCell.Style = cellStyle2;
                tmplate.Row.Cells.Add(textBoxCell);
                tmplate.Row.Height = 21;

                locationX += colmunWidth;
            }
            headerSection.Name = "columnHeaderSection1";
            headerSection.Height = 21;

            tmplate.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] { headerSection });
            tmplate.Width = locationX;
            return tmplate;

        }
    }

}
