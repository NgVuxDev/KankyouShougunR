namespace BikoUchiwakeNyuryoku.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class BikoUchiwakeNyuryokuDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BikoUchiwakeNyuryokuDetail));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.BIKO_CD = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.BIKO_NOTE = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.BIKO_CD);
            this.Row.Cells.Add(this.BIKO_NOTE);
            this.Row.Height = 21;
            this.Row.Width = 945;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 945;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(87, 21);
            cellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle3.Border = border1;
            cellStyle3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle3.ForeColor = System.Drawing.Color.White;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.gcCustomColumnHeader2.Style = cellStyle3;
            this.gcCustomColumnHeader2.TabIndex = 2;
            this.gcCustomColumnHeader2.Value = "備考CD";
            this.gcCustomColumnHeader2.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(87, 0);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(858, 21);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border2;
            cellStyle4.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle4;
            this.gcCustomColumnHeader3.TabIndex = 3;
            this.gcCustomColumnHeader3.Value = "備考内容";
            this.gcCustomColumnHeader3.ViewSearchItem = true;
            // 
            // BIKO_CD
            // 
            this.BIKO_CD.CustomFormatSetting = "000";
            this.BIKO_CD.DataField = "BIKO_CD";
            this.BIKO_CD.DBFieldsName = "BIKO_CD";
            this.BIKO_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.BIKO_CD.DisplayItemName = "備考CD";
            this.BIKO_CD.DisplayPopUp = null;
            this.BIKO_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_CD.FocusOutCheckMethod")));
            this.BIKO_CD.FormatSetting = "カスタム";
            this.BIKO_CD.IsInputErrorOccured = false;
            this.BIKO_CD.ItemDefinedTypes = "varchar";
            this.BIKO_CD.Location = new System.Drawing.Point(0, 0);
            this.BIKO_CD.Name = "BIKO_CD";
            this.BIKO_CD.PopupAfterExecute = null;
            this.BIKO_CD.PopupBeforeExecute = null;
            this.BIKO_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BIKO_CD.PopupSearchSendParams")));
            this.BIKO_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BIKO_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BIKO_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.BIKO_CD.RangeSetting = rangeSettingDto1;
            this.BIKO_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_CD.RegistCheckMethod")));
            this.BIKO_CD.ShortItemName = "備考CD";
            this.BIKO_CD.Size = new System.Drawing.Size(87, 21);
            cellStyle1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.BIKO_CD.Style = cellStyle1;
            this.BIKO_CD.TabIndex = 1;
            this.BIKO_CD.Tag = "全角20文字/半角40文字以内で入力してください";
            // 
            // BIKO_NOTE
            // 
            this.BIKO_NOTE.CharactersNumber = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.BIKO_NOTE.CopyAutoSetControl = "";
            this.BIKO_NOTE.CopyAutoSetWithSpace = true;
            this.BIKO_NOTE.DataField = "BIKO_NOTE";
            this.BIKO_NOTE.DBFieldsName = "BIKO_NOTE";
            this.BIKO_NOTE.DefaultBackColor = System.Drawing.Color.Empty;
            this.BIKO_NOTE.DisplayItemName = "備考内容";
            this.BIKO_NOTE.DisplayPopUp = null;
            this.BIKO_NOTE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_NOTE.FocusOutCheckMethod")));
            this.BIKO_NOTE.FuriganaAutoSetControl = "";
            this.BIKO_NOTE.IsInputErrorOccured = false;
            this.BIKO_NOTE.ItemDefinedTypes = "varchar";
            this.BIKO_NOTE.Location = new System.Drawing.Point(87, 0);
            this.BIKO_NOTE.MaxLength = 120;
            this.BIKO_NOTE.Name = "BIKO_NOTE";
            this.BIKO_NOTE.PopupAfterExecute = null;
            this.BIKO_NOTE.PopupBeforeExecute = null;
            this.BIKO_NOTE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BIKO_NOTE.PopupSearchSendParams")));
            this.BIKO_NOTE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BIKO_NOTE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BIKO_NOTE.popupWindowSetting")));
            this.BIKO_NOTE.ReadOnly = true;
            this.BIKO_NOTE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_NOTE.RegistCheckMethod")));
            this.BIKO_NOTE.Size = new System.Drawing.Size(858, 21);
            cellStyle2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.BIKO_NOTE.Style = cellStyle2;
            this.BIKO_NOTE.TabIndex = 2;
            this.BIKO_NOTE.Tag = "";
            // 
            // BikoUchiwakeNyuryokuDetail
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
            this.Width = 945;

        }

        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell BIKO_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell BIKO_NOTE;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
    }
}
