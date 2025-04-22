namespace Shougun.Core.Allocation.HaishaWariateDay
{          
    [System.ComponentModel.ToolboxItem(true)]
    partial class TemplateMihaisha
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TemplateMihaisha));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.columnHeaderCell1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.cellHaishaShurui = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.cellSagyouDateKubun = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.cellGenchakuJikan = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.cellDenpyouContent = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.cellHaishaShurui);
            this.Row.Cells.Add(this.cellSagyouDateKubun);
            this.Row.Cells.Add(this.cellGenchakuJikan);
            this.Row.Cells.Add(this.cellDenpyouContent);
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Row.DefaultCellStyle = cellStyle5;
            this.Row.Height = 88;
            this.Row.Width = 180;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell1);
            this.columnHeaderSection1.Height = 42;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 180;
            // 
            // columnHeaderCell1
            // 
            this.columnHeaderCell1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell1.Location = new System.Drawing.Point(0, 0);
            this.columnHeaderCell1.Name = "columnHeaderCell1";
            this.columnHeaderCell1.Size = new System.Drawing.Size(180, 42);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle6.Border = border1;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            this.columnHeaderCell1.Style = cellStyle6;
            this.columnHeaderCell1.TabIndex = 0;
            this.columnHeaderCell1.Value = "未配車";
            this.columnHeaderCell1.ViewSearchItem = false;
            // 
            // cellHaishaShurui
            // 
            this.cellHaishaShurui.DataField = "HAISHA_SHURUI";
            this.cellHaishaShurui.DefaultBackColor = System.Drawing.Color.Empty;
            this.cellHaishaShurui.DisplayPopUp = null;
            this.cellHaishaShurui.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cellHaishaShurui.FocusOutCheckMethod")));
            this.cellHaishaShurui.IsInputErrorOccured = false;
            this.cellHaishaShurui.Location = new System.Drawing.Point(0, 0);
            this.cellHaishaShurui.Name = "cellHaishaShurui";
            this.cellHaishaShurui.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cellHaishaShurui.PopupSearchSendParams")));
            this.cellHaishaShurui.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cellHaishaShurui.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cellHaishaShurui.popupWindowSetting")));
            this.cellHaishaShurui.ReadOnly = true;
            this.cellHaishaShurui.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cellHaishaShurui.RegistCheckMethod")));
            this.cellHaishaShurui.Selectable = false;
            this.cellHaishaShurui.Size = new System.Drawing.Size(20, 20);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.cellHaishaShurui.Style = cellStyle1;
            this.cellHaishaShurui.TabIndex = 0;
            // 
            // cellSagyouDateKubun
            // 
            this.cellSagyouDateKubun.DataField = "SAGYOUDATE_KUBUN";
            this.cellSagyouDateKubun.DefaultBackColor = System.Drawing.Color.Empty;
            this.cellSagyouDateKubun.DisplayPopUp = null;
            this.cellSagyouDateKubun.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cellSagyouDateKubun.FocusOutCheckMethod")));
            this.cellSagyouDateKubun.IsInputErrorOccured = false;
            this.cellSagyouDateKubun.Location = new System.Drawing.Point(20, 0);
            this.cellSagyouDateKubun.Name = "cellSagyouDateKubun";
            this.cellSagyouDateKubun.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cellSagyouDateKubun.PopupSearchSendParams")));
            this.cellSagyouDateKubun.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cellSagyouDateKubun.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cellSagyouDateKubun.popupWindowSetting")));
            this.cellSagyouDateKubun.ReadOnly = true;
            this.cellSagyouDateKubun.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cellSagyouDateKubun.RegistCheckMethod")));
            this.cellSagyouDateKubun.Selectable = false;
            this.cellSagyouDateKubun.Size = new System.Drawing.Size(40, 20);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.cellSagyouDateKubun.Style = cellStyle2;
            this.cellSagyouDateKubun.TabIndex = 1;
            // 
            // cellGenchakuJikan
            // 
            this.cellGenchakuJikan.DataField = "GENCHAKU_JIKAN";
            this.cellGenchakuJikan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cellGenchakuJikan.DisplayPopUp = null;
            this.cellGenchakuJikan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cellGenchakuJikan.FocusOutCheckMethod")));
            this.cellGenchakuJikan.IsInputErrorOccured = false;
            this.cellGenchakuJikan.Location = new System.Drawing.Point(60, 0);
            this.cellGenchakuJikan.Name = "cellGenchakuJikan";
            this.cellGenchakuJikan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cellGenchakuJikan.PopupSearchSendParams")));
            this.cellGenchakuJikan.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cellGenchakuJikan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cellGenchakuJikan.popupWindowSetting")));
            this.cellGenchakuJikan.ReadOnly = true;
            this.cellGenchakuJikan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cellGenchakuJikan.RegistCheckMethod")));
            this.cellGenchakuJikan.Selectable = false;
            this.cellGenchakuJikan.Size = new System.Drawing.Size(120, 20);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.cellGenchakuJikan.Style = cellStyle3;
            this.cellGenchakuJikan.TabIndex = 2;
            // 
            // cellDenpyouContent
            // 
            this.cellDenpyouContent.DataField = "DENPYOU_CONTENT";
            this.cellDenpyouContent.DefaultBackColor = System.Drawing.Color.Empty;
            this.cellDenpyouContent.DisplayPopUp = null;
            this.cellDenpyouContent.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cellDenpyouContent.FocusOutCheckMethod")));
            this.cellDenpyouContent.IsInputErrorOccured = false;
            this.cellDenpyouContent.Location = new System.Drawing.Point(0, 20);
            this.cellDenpyouContent.Name = "cellDenpyouContent";
            this.cellDenpyouContent.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cellDenpyouContent.PopupSearchSendParams")));
            this.cellDenpyouContent.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cellDenpyouContent.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cellDenpyouContent.popupWindowSetting")));
            this.cellDenpyouContent.ReadOnly = true;
            this.cellDenpyouContent.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cellDenpyouContent.RegistCheckMethod")));
            this.cellDenpyouContent.Size = new System.Drawing.Size(180, 68);
            cellStyle4.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            cellStyle4.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            this.cellDenpyouContent.Style = cellStyle4;
            this.cellDenpyouContent.TabIndex = 3;
            // 
            // TemplateMihaisha
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            cellStyle7.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Black);
            cellStyle7.Border = border2;
            cellStyle7.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle7.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle7.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle7.ForeColor = System.Drawing.Color.White;
            cellStyle7.Format = "";
            cellStyle7.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle7.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle7.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle7.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle7.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle7.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle7.Margin = new System.Windows.Forms.Padding(0);
            cellStyle7.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle7.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            cellStyle7.Padding = new System.Windows.Forms.Padding(0);
            cellStyle7.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle7.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle7.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle7.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle7.TextAngle = 0F;
            cellStyle7.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle7.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle7.TextIndent = 0;
            cellStyle7.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle7.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle7.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.ColumnHeadersDefaultHeaderCellStyle = cellStyle7;
            cellStyle8.BackColor = System.Drawing.Color.Transparent;
            cellStyle8.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Silver);
            cellStyle8.Border = border3;
            cellStyle8.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle8.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle8.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle8.Format = "";
            cellStyle8.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle8.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle8.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle8.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle8.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle8.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle8.Margin = new System.Windows.Forms.Padding(0);
            cellStyle8.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle8.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle8.Padding = new System.Windows.Forms.Padding(0);
            cellStyle8.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle8.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle8.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle8.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle8.TextAngle = 0F;
            cellStyle8.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle8.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle8.TextIndent = 0;
            cellStyle8.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle8.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle8.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle8;
            this.Height = 130;
            this.Width = 180;

        }
        

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell1;
        private r_framework.CustomControl.GcCustomTextBoxCell cellHaishaShurui;
        private r_framework.CustomControl.GcCustomTextBoxCell cellSagyouDateKubun;
        private r_framework.CustomControl.GcCustomTextBoxCell cellGenchakuJikan;
        private r_framework.CustomControl.GcCustomTextBoxCell cellDenpyouContent;
        
    }
}
