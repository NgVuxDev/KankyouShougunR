namespace Gyousha_Itaku_Keiyaku.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class Gyousha_Itaku_KeiyakuDetail
    {
        /// <summary> 
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region MultiRow Template Designer generated code

        /// <summary> 
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
this.components = new System.ComponentModel.Container();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle12 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gyousha_Itaku_KeiyakuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle13 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border6 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader5 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.ITAKU_KEIYAKU_NO = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ITAKU_KEIYAKU_SHURUI = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.YUUKOU_BEGIN = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.label1 = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.YUUKOU_END = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ITAKU_KEIYAKU_STATUS = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.ITAKU_SYSTEM_ID = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_SHURUI);
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_NO);
            this.Row.Cells.Add(this.YUUKOU_BEGIN);
            this.Row.Cells.Add(this.label1);
            this.Row.Cells.Add(this.YUUKOU_END);
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_STATUS);
            this.Row.Cells.Add(this.ITAKU_SYSTEM_ID);
            this.Row.Height = 21;
            this.Row.Width = 549;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader4);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader5);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 549;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(95, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle8.Border = border1;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.gcCustomColumnHeader1.Style = cellStyle8;
            this.gcCustomColumnHeader1.TabIndex = 0;
            this.gcCustomColumnHeader1.Value = "委託契約番号";
            this.gcCustomColumnHeader1.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FilterCellIndex = 3;
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(95, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(175, 21);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle9.Border = border2;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader2.Style = cellStyle9;
            this.gcCustomColumnHeader2.TabIndex = 1;
            this.gcCustomColumnHeader2.Value = "契約書種類";
            this.gcCustomColumnHeader2.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FilterCellIndex = 1;
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(270, 0);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(200, 21);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle10.Border = border3;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle10;
            this.gcCustomColumnHeader3.TabIndex = 2;
            this.gcCustomColumnHeader3.Value = "有効期限";
            this.gcCustomColumnHeader3.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader4
            // 
            this.gcCustomColumnHeader4.FilterCellIndex = 4;
            this.gcCustomColumnHeader4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader4.Location = new System.Drawing.Point(470, 0);
            this.gcCustomColumnHeader4.Name = "gcCustomColumnHeader4";
            this.gcCustomColumnHeader4.Size = new System.Drawing.Size(80, 21);
            cellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle11.Border = border4;
            cellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader4.Style = cellStyle11;
            this.gcCustomColumnHeader4.TabIndex = 3;
            this.gcCustomColumnHeader4.Value = "ステータス";
            this.gcCustomColumnHeader4.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader5
            // 
            this.gcCustomColumnHeader5.FilterCellIndex = 4;
            this.gcCustomColumnHeader5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader5.Location = new System.Drawing.Point(549, 0);
            this.gcCustomColumnHeader5.Name = "gcCustomColumnHeader5";
            this.gcCustomColumnHeader5.Size = new System.Drawing.Size(124, 21);
            cellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle12.Border = border5;
            cellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle12.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader5.Style = cellStyle12;
            this.gcCustomColumnHeader5.TabIndex = 4;
            this.gcCustomColumnHeader5.Value = "システムID(隠)";
            this.gcCustomColumnHeader5.ViewSearchItem = true;
            this.gcCustomColumnHeader5.Visible = false;
            // 
            // ITAKU_KEIYAKU_NO
            // 
            this.ITAKU_KEIYAKU_NO.DataField = "ITAKU_NO";
            this.ITAKU_KEIYAKU_NO.DBFieldsName = "ITAKU_NO";
            this.ITAKU_KEIYAKU_NO.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITAKU_KEIYAKU_NO.DisplayItemName = "委託契約番号";
            this.ITAKU_KEIYAKU_NO.DisplayPopUp = null;
            this.ITAKU_KEIYAKU_NO.ItemDefinedTypes = "varchar";
            this.ITAKU_KEIYAKU_NO.Location = new System.Drawing.Point(0, 0);
            this.ITAKU_KEIYAKU_NO.Name = "ITAKU_KEIYAKU_NO";
            this.ITAKU_KEIYAKU_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_NO.ReadOnly = true;
            this.ITAKU_KEIYAKU_NO.ShortItemName = "委託契約番号";
            this.ITAKU_KEIYAKU_NO.Size = new System.Drawing.Size(95, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.ITAKU_KEIYAKU_NO.Style = cellStyle2;
            this.ITAKU_KEIYAKU_NO.TabIndex = 1;
            this.ITAKU_KEIYAKU_NO.TabStop = false;
            this.ITAKU_KEIYAKU_NO.Tag = "委託契約番号が表示されます";
            // 
            // ITAKU_KEIYAKU_SHURUI
            // 
            this.ITAKU_KEIYAKU_SHURUI.DataField = "ITAKU_SHURUI";
            this.ITAKU_KEIYAKU_SHURUI.DBFieldsName = "ITAKU_SHURUI";
            this.ITAKU_KEIYAKU_SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITAKU_KEIYAKU_SHURUI.DisplayItemName = "委託契約書種類";
            this.ITAKU_KEIYAKU_SHURUI.DisplayPopUp = null;
            this.ITAKU_KEIYAKU_SHURUI.ItemDefinedTypes = "varchar";
            this.ITAKU_KEIYAKU_SHURUI.Location = new System.Drawing.Point(95, 0);
            this.ITAKU_KEIYAKU_SHURUI.Name = "ITAKU_KEIYAKU_SHURUI";
            this.ITAKU_KEIYAKU_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_SHURUI.ReadOnly = true;
            this.ITAKU_KEIYAKU_SHURUI.ShortItemName = "委託契約書種類";
            this.ITAKU_KEIYAKU_SHURUI.Size = new System.Drawing.Size(175, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.ITAKU_KEIYAKU_SHURUI.Style = cellStyle1;
            this.ITAKU_KEIYAKU_SHURUI.TabIndex = 2;
            this.ITAKU_KEIYAKU_SHURUI.TabStop = false;
            this.ITAKU_KEIYAKU_SHURUI.Tag = "契約書種類が表示されます";
            // 
            // YUUKOU_BEGIN
            // 
            this.YUUKOU_BEGIN.DataField = "YUUKOU_BEGIN";
            this.YUUKOU_BEGIN.DBFieldsName = "YUUKOU_BEGIN";
            this.YUUKOU_BEGIN.DefaultBackColor = System.Drawing.Color.Empty;
            this.YUUKOU_BEGIN.DisplayItemName = "有効期限開始";
            this.YUUKOU_BEGIN.DisplayPopUp = null;
            this.YUUKOU_BEGIN.ItemDefinedTypes = "datetime";
            this.YUUKOU_BEGIN.Location = new System.Drawing.Point(270, 0);
            this.YUUKOU_BEGIN.Name = "YUUKOU_BEGIN";
            this.YUUKOU_BEGIN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YUUKOU_BEGIN.ReadOnly = true;
            this.YUUKOU_BEGIN.ShortItemName = "有効期限開始";
            this.YUUKOU_BEGIN.Size = new System.Drawing.Size(79, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.YUUKOU_BEGIN.Style = cellStyle3;
            this.YUUKOU_BEGIN.TabIndex = 3;
            this.YUUKOU_BEGIN.TabStop = false;
            this.YUUKOU_BEGIN.Tag = "有効期間開始が表示されます";
            // 
            // label1
            // 
            this.label1.DBFieldsName = "";
            this.label1.DefaultBackColor = System.Drawing.Color.Empty;
            this.label1.DisplayItemName = "";
            this.label1.DisplayPopUp = null;
            this.label1.Location = new System.Drawing.Point(349, 0);
            this.label1.Name = "label1";
            this.label1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.label1.ReadOnly = true;
            this.label1.Size = new System.Drawing.Size(40, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.label1.Style = cellStyle4;
            this.label1.TabIndex = 4;
            this.label1.TabStop = false;
            this.label1.Tag = "";
            this.label1.Value = "～";
            // 
            // YUUKOU_END
            // 
            this.YUUKOU_END.DataField = "YUUKOU_END";
            this.YUUKOU_END.DBFieldsName = "YUUKOU_END";
            this.YUUKOU_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.YUUKOU_END.DisplayItemName = "有効期限終了";
            this.YUUKOU_END.DisplayPopUp = null;
            this.YUUKOU_END.ItemDefinedTypes = "datetime";
            this.YUUKOU_END.Location = new System.Drawing.Point(389, 0);
            this.YUUKOU_END.Name = "YUUKOU_END";
            this.YUUKOU_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YUUKOU_END.ReadOnly = true;
            this.YUUKOU_END.ShortItemName = "有効期限終了";
            this.YUUKOU_END.Size = new System.Drawing.Size(81, 21);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.YUUKOU_END.Style = cellStyle5;
            this.YUUKOU_END.TabIndex = 5;
            this.YUUKOU_END.TabStop = false;
            this.YUUKOU_END.Tag = "有効期間終了が表示されます";
            // 
            // ITAKU_KEIYAKU_STATUS
            // 
            this.ITAKU_KEIYAKU_STATUS.DataField = "ITAKU_STATUS";
            this.ITAKU_KEIYAKU_STATUS.DBFieldsName = "ITAKU_STATUS";
            this.ITAKU_KEIYAKU_STATUS.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITAKU_KEIYAKU_STATUS.DisplayItemName = "委託契約ステータス";
            this.ITAKU_KEIYAKU_STATUS.DisplayPopUp = null;
            this.ITAKU_KEIYAKU_STATUS.ItemDefinedTypes = "varchar";
            this.ITAKU_KEIYAKU_STATUS.Location = new System.Drawing.Point(470, 0);
            this.ITAKU_KEIYAKU_STATUS.Name = "ITAKU_KEIYAKU_STATUS";
            this.ITAKU_KEIYAKU_STATUS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_STATUS.ReadOnly = true;
            this.ITAKU_KEIYAKU_STATUS.ShortItemName = "委託契約ステータス";
            this.ITAKU_KEIYAKU_STATUS.Size = new System.Drawing.Size(79, 21);
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle6.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.ITAKU_KEIYAKU_STATUS.Style = cellStyle6;
            this.ITAKU_KEIYAKU_STATUS.TabIndex = 6;
            this.ITAKU_KEIYAKU_STATUS.TabStop = false;
            this.ITAKU_KEIYAKU_STATUS.Tag = "ステータスが表示されます";
            // 
            // ITAKU_SYSTEM_ID
            // 
            this.ITAKU_SYSTEM_ID.DataField = "SYSTEM_ID";
            this.ITAKU_SYSTEM_ID.DBFieldsName = "SYSTEM_ID";
            this.ITAKU_SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITAKU_SYSTEM_ID.DisplayItemName = "システムID";
            this.ITAKU_SYSTEM_ID.DisplayPopUp = null;
            this.ITAKU_SYSTEM_ID.ItemDefinedTypes = "varchar";
            this.ITAKU_SYSTEM_ID.Location = new System.Drawing.Point(550, 0);
            this.ITAKU_SYSTEM_ID.Name = "ITAKU_SYSTEM_ID";
            this.ITAKU_SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_SYSTEM_ID.ReadOnly = true;
            this.ITAKU_SYSTEM_ID.Selectable = false;
            this.ITAKU_SYSTEM_ID.ShortItemName = "システムID";
            this.ITAKU_SYSTEM_ID.Size = new System.Drawing.Size(123, 21);
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle7.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.ITAKU_SYSTEM_ID.Style = cellStyle7;
            this.ITAKU_SYSTEM_ID.TabIndex = 7;
            this.ITAKU_SYSTEM_ID.TabStop = false;
            this.ITAKU_SYSTEM_ID.Tag = "システムIDが表示されます";
            this.ITAKU_SYSTEM_ID.Visible = false;
            // 
            // Gyousha_Itaku_KeiyakuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle13.BackColor = System.Drawing.Color.Transparent;
            cellStyle13.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border6.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Silver);
            cellStyle13.Border = border6;
            cellStyle13.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle13.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle13.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle13.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle13.Format = "";
            cellStyle13.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle13.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle13.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle13.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle13.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle13.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle13.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle13.Margin = new System.Windows.Forms.Padding(0);
            cellStyle13.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle13.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle13.Padding = new System.Windows.Forms.Padding(0);
            cellStyle13.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle13.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle13.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle13.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle13.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle13.TextAngle = 0F;
            cellStyle13.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle13.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle13.TextIndent = 0;
            cellStyle13.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle13.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle13.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle13;
            this.Height = 42;
            this.Width = 549;

        }


        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_KEIYAKU_NO;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_KEIYAKU_SHURUI;
        internal r_framework.CustomControl.GcCustomTextBoxCell YUUKOU_BEGIN;
        internal r_framework.CustomControl.GcCustomTextBoxCell label1;
        internal r_framework.CustomControl.GcCustomTextBoxCell YUUKOU_END;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_KEIYAKU_STATUS;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_SYSTEM_ID;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader5;

    }
}
