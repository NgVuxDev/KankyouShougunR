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
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle10 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Gyousha_Itaku_KeiyakuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle11 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
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
            // 
            // Row
            // 
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_SHURUI);
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_NO);
            this.Row.Cells.Add(this.YUUKOU_BEGIN);
            this.Row.Cells.Add(this.label1);
            this.Row.Cells.Add(this.YUUKOU_END);
            this.Row.Cells.Add(this.ITAKU_KEIYAKU_STATUS);
            this.Row.Height = 21;
            this.Row.Width = 550;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader4);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader5);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 550;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(95, 21);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle7.Border = border1;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.gcCustomColumnHeader2.Style = cellStyle7;
            this.gcCustomColumnHeader2.TabIndex = 0;
            this.gcCustomColumnHeader2.Value = "委託契約番号";
            this.gcCustomColumnHeader2.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FilterCellIndex = 3;
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(95, 0);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(175, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle8.Border = border2;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle8;
            this.gcCustomColumnHeader3.TabIndex = 1;
            this.gcCustomColumnHeader3.Value = "契約書種類";
            this.gcCustomColumnHeader3.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader4
            // 
            this.gcCustomColumnHeader4.FilterCellIndex = 1;
            this.gcCustomColumnHeader4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader4.Location = new System.Drawing.Point(270, 0);
            this.gcCustomColumnHeader4.Name = "gcCustomColumnHeader4";
            this.gcCustomColumnHeader4.Size = new System.Drawing.Size(200, 21);
            cellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle9.Border = border3;
            cellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader4.Style = cellStyle9;
            this.gcCustomColumnHeader4.TabIndex = 2;
            this.gcCustomColumnHeader4.Value = "有効期限";
            this.gcCustomColumnHeader4.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader5
            // 
            this.gcCustomColumnHeader5.FilterCellIndex = 4;
            this.gcCustomColumnHeader5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader5.Location = new System.Drawing.Point(470, 0);
            this.gcCustomColumnHeader5.Name = "gcCustomColumnHeader5";
            this.gcCustomColumnHeader5.Size = new System.Drawing.Size(80, 21);
            cellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle10.Border = border4;
            cellStyle10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle10.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader5.Style = cellStyle10;
            this.gcCustomColumnHeader5.TabIndex = 3;
            this.gcCustomColumnHeader5.Value = "ステータス";
            this.gcCustomColumnHeader5.ViewSearchItem = true;
            // 
            // ITAKU_KEIYAKU_NO
            // 
            this.ITAKU_KEIYAKU_NO.DataField = "ITAKU_NO";
            this.ITAKU_KEIYAKU_NO.DBFieldsName = "ITAKU_NO";
            this.ITAKU_KEIYAKU_NO.DisplayItemName = "委託契約番号";
            this.ITAKU_KEIYAKU_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.FocusOutCheckMethod")));
            this.ITAKU_KEIYAKU_NO.ItemDefinedTypes = "varchar";
            this.ITAKU_KEIYAKU_NO.Location = new System.Drawing.Point(0, 0);
            this.ITAKU_KEIYAKU_NO.Name = "ITAKU_KEIYAKU_NO";
            this.ITAKU_KEIYAKU_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.PopupSearchSendParams")));
            this.ITAKU_KEIYAKU_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.popupWindowSetting")));
            this.ITAKU_KEIYAKU_NO.ReadOnly = true;
            this.ITAKU_KEIYAKU_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_NO.RegistCheckMethod")));
            this.ITAKU_KEIYAKU_NO.Selectable = false;
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
            this.ITAKU_KEIYAKU_SHURUI.DisplayItemName = "委託契約書種類";
            this.ITAKU_KEIYAKU_SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_SHURUI.FocusOutCheckMethod")));
            this.ITAKU_KEIYAKU_SHURUI.ItemDefinedTypes = "varchar";
            this.ITAKU_KEIYAKU_SHURUI.Location = new System.Drawing.Point(95, 0);
            this.ITAKU_KEIYAKU_SHURUI.Name = "ITAKU_KEIYAKU_SHURUI";
            this.ITAKU_KEIYAKU_SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITAKU_KEIYAKU_SHURUI.PopupSearchSendParams")));
            this.ITAKU_KEIYAKU_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITAKU_KEIYAKU_SHURUI.popupWindowSetting")));
            this.ITAKU_KEIYAKU_SHURUI.ReadOnly = true;
            this.ITAKU_KEIYAKU_SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_SHURUI.RegistCheckMethod")));
            this.ITAKU_KEIYAKU_SHURUI.Selectable = false;
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
            this.YUUKOU_BEGIN.DisplayItemName = "有効期限開始";
            this.YUUKOU_BEGIN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_BEGIN.FocusOutCheckMethod")));
            this.YUUKOU_BEGIN.ItemDefinedTypes = "datetime";
            this.YUUKOU_BEGIN.Location = new System.Drawing.Point(270, 0);
            this.YUUKOU_BEGIN.Name = "YUUKOU_BEGIN";
            this.YUUKOU_BEGIN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("YUUKOU_BEGIN.PopupSearchSendParams")));
            this.YUUKOU_BEGIN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YUUKOU_BEGIN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("YUUKOU_BEGIN.popupWindowSetting")));
            this.YUUKOU_BEGIN.ReadOnly = true;
            this.YUUKOU_BEGIN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_BEGIN.RegistCheckMethod")));
            this.YUUKOU_BEGIN.Selectable = false;
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
            this.label1.DisplayItemName = "";
            this.label1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("label1.FocusOutCheckMethod")));
            this.label1.Location = new System.Drawing.Point(349, 0);
            this.label1.Name = "label1";
            this.label1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("label1.PopupSearchSendParams")));
            this.label1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.label1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("label1.popupWindowSetting")));
            this.label1.ReadOnly = true;
            this.label1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("label1.RegistCheckMethod")));
            this.label1.Selectable = false;
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
            this.YUUKOU_END.DisplayItemName = "有効期限終了";
            this.YUUKOU_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_END.FocusOutCheckMethod")));
            this.YUUKOU_END.ItemDefinedTypes = "datetime";
            this.YUUKOU_END.Location = new System.Drawing.Point(389, 0);
            this.YUUKOU_END.Name = "YUUKOU_END";
            this.YUUKOU_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("YUUKOU_END.PopupSearchSendParams")));
            this.YUUKOU_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YUUKOU_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("YUUKOU_END.popupWindowSetting")));
            this.YUUKOU_END.ReadOnly = true;
            this.YUUKOU_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YUUKOU_END.RegistCheckMethod")));
            this.YUUKOU_END.Selectable = false;
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
            this.ITAKU_KEIYAKU_STATUS.DisplayItemName = "委託契約ステータス";
            this.ITAKU_KEIYAKU_STATUS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_STATUS.FocusOutCheckMethod")));
            this.ITAKU_KEIYAKU_STATUS.ItemDefinedTypes = "varchar";
            this.ITAKU_KEIYAKU_STATUS.Location = new System.Drawing.Point(470, 0);
            this.ITAKU_KEIYAKU_STATUS.Name = "ITAKU_KEIYAKU_STATUS";
            this.ITAKU_KEIYAKU_STATUS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITAKU_KEIYAKU_STATUS.PopupSearchSendParams")));
            this.ITAKU_KEIYAKU_STATUS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITAKU_KEIYAKU_STATUS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITAKU_KEIYAKU_STATUS.popupWindowSetting")));
            this.ITAKU_KEIYAKU_STATUS.ReadOnly = true;
            this.ITAKU_KEIYAKU_STATUS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITAKU_KEIYAKU_STATUS.RegistCheckMethod")));
            this.ITAKU_KEIYAKU_STATUS.Selectable = false;
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
            // Gyousha_Itaku_KeiyakuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle11.BackColor = System.Drawing.Color.Transparent;
            cellStyle11.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Silver);
            cellStyle11.Border = border5;
            cellStyle11.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle11.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle11.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle11.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle11.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle11.Format = "";
            cellStyle11.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle11.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle11.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle11.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle11.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle11.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle11.Margin = new System.Windows.Forms.Padding(0);
            cellStyle11.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle11.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle11.Padding = new System.Windows.Forms.Padding(0);
            cellStyle11.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle11.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle11.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle11.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle11.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle11.TextAngle = 0F;
            cellStyle11.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle11.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle11.TextIndent = 0;
            cellStyle11.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle11.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle11.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle11;
            this.Height = 42;
            this.Width = 550;

        }


        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader4;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader5;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_KEIYAKU_NO;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_KEIYAKU_SHURUI;
        internal r_framework.CustomControl.GcCustomTextBoxCell YUUKOU_BEGIN;
        internal r_framework.CustomControl.GcCustomTextBoxCell label1;
        internal r_framework.CustomControl.GcCustomTextBoxCell YUUKOU_END;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITAKU_KEIYAKU_STATUS;

    }
}
