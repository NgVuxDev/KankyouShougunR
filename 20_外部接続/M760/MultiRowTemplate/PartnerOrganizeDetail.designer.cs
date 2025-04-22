namespace Shougun.Core.ExternalConnection.DenshiBunshoHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class PartnerOrganizeDetail
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartnerOrganizeDetail));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.NO = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.PARTNER_ORGANIZE_NM = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.NO);
            this.Row.Cells.Add(this.PARTNER_ORGANIZE_NM);
            this.Row.Height = 21;
            this.Row.Width = 880;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 880;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(35, 21);
            cellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle3.Border = border1;
            cellStyle3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ForeColor = System.Drawing.Color.White;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader1.Style = cellStyle3;
            this.gcCustomColumnHeader1.TabIndex = 0;
            this.gcCustomColumnHeader1.Value = "No";
            this.gcCustomColumnHeader1.ViewSearchItem = false;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FilterCellIndex = 1;
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(35, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(845, 21);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle4.Border = border2;
            cellStyle4.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.gcCustomColumnHeader2.Style = cellStyle4;
            this.gcCustomColumnHeader2.TabIndex = 7;
            this.gcCustomColumnHeader2.Value = "相手方";
            this.gcCustomColumnHeader2.ViewSearchItem = false;
            // 
            // NO
            // 
            this.NO.DataField = "NO";
            this.NO.DBFieldsName = "NO";
            this.NO.DefaultBackColor = System.Drawing.Color.Empty;
            this.NO.DisplayItemName = "No";
            this.NO.DisplayPopUp = null;
            this.NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NO.FocusOutCheckMethod")));
            this.NO.IsInputErrorOccured = false;
            this.NO.ItemDefinedTypes = "varchar";
            this.NO.Location = new System.Drawing.Point(0, 0);
            this.NO.Name = "NO";
            this.NO.PopupAfterExecute = null;
            this.NO.PopupBeforeExecute = null;
            this.NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NO.PopupSearchSendParams")));
            this.NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NO.PopupWindowName = "";
            this.NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NO.popupWindowSetting")));
            this.NO.RangeSetting = rangeSettingDto1;
            this.NO.ReadOnly = true;
            this.NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NO.RegistCheckMethod")));
            this.NO.ShortItemName = "No";
            this.NO.Size = new System.Drawing.Size(35, 21);
            cellStyle1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.NO.Style = cellStyle1;
            this.NO.TabIndex = 0;
            this.NO.Tag = "";
            // 
            // PARTNER_ORGANIZE_NM
            // 
            this.PARTNER_ORGANIZE_NM.CharactersNumber = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.PARTNER_ORGANIZE_NM.DataField = "PARTNER_ORGANIZE_NM";
            this.PARTNER_ORGANIZE_NM.DBFieldsName = "PARTNER_ORGANIZE_NM";
            this.PARTNER_ORGANIZE_NM.DefaultBackColor = System.Drawing.Color.Empty;
            this.PARTNER_ORGANIZE_NM.DisplayItemName = "相手方";
            this.PARTNER_ORGANIZE_NM.DisplayPopUp = null;
            this.PARTNER_ORGANIZE_NM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARTNER_ORGANIZE_NM.FocusOutCheckMethod")));
            this.PARTNER_ORGANIZE_NM.IsInputErrorOccured = false;
            this.PARTNER_ORGANIZE_NM.ItemDefinedTypes = "varchar";
            this.PARTNER_ORGANIZE_NM.Location = new System.Drawing.Point(35, 0);
            this.PARTNER_ORGANIZE_NM.MaxLength = 120;
            this.PARTNER_ORGANIZE_NM.Name = "PARTNER_ORGANIZE_NM";
            this.PARTNER_ORGANIZE_NM.PopupAfterExecute = null;
            this.PARTNER_ORGANIZE_NM.PopupBeforeExecute = null;
            this.PARTNER_ORGANIZE_NM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PARTNER_ORGANIZE_NM.PopupSearchSendParams")));
            this.PARTNER_ORGANIZE_NM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PARTNER_ORGANIZE_NM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PARTNER_ORGANIZE_NM.popupWindowSetting")));
            this.PARTNER_ORGANIZE_NM.ReadOnly = true;
            this.PARTNER_ORGANIZE_NM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARTNER_ORGANIZE_NM.RegistCheckMethod")));
            this.PARTNER_ORGANIZE_NM.ShortItemName = "相手方";
            this.PARTNER_ORGANIZE_NM.Size = new System.Drawing.Size(845, 21);
            cellStyle2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Off;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            this.PARTNER_ORGANIZE_NM.Style = cellStyle2;
            this.PARTNER_ORGANIZE_NM.TabIndex = 1;
            this.PARTNER_ORGANIZE_NM.Tag = "";
            // 
            // PartnerOrganizeDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle5.BackColor = System.Drawing.Color.Transparent;
            cellStyle5.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Silver);
            cellStyle5.Border = border3;
            cellStyle5.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle5.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle5.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle5.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle5.Format = "";
            cellStyle5.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle5.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle5.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle5.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle5.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle5.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle5.Margin = new System.Windows.Forms.Padding(0);
            cellStyle5.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle5.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle5.Padding = new System.Windows.Forms.Padding(0);
            cellStyle5.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle5.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle5.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle5.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle5.TextAngle = 0F;
            cellStyle5.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle5.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle5.TextIndent = 0;
            cellStyle5.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle5.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle5.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle5;
            this.Height = 42;
            this.Width = 880;

        }


        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        private r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell NO;
        internal r_framework.CustomControl.GcCustomTextBoxCell PARTNER_ORGANIZE_NM;

    }
}
