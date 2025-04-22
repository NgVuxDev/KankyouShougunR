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
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle5 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BikoUchiwakeNyuryokuDetail));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader3 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.BIKO_CD = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.BIKO_NOTE = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.DETAIL_SYSTEM_ID = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.BIKO_CD);
            this.Row.Cells.Add(this.BIKO_NOTE);
            this.Row.Cells.Add(this.DETAIL_SYSTEM_ID);
            this.Row.Height = 21;
            this.Row.Width = 447;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader3);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 447;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(68, 21);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border1;
            cellStyle4.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.gcCustomColumnHeader2.Style = cellStyle4;
            this.gcCustomColumnHeader2.TabIndex = 2;
            this.gcCustomColumnHeader2.Value = "備考CD";
            this.gcCustomColumnHeader2.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader3
            // 
            this.gcCustomColumnHeader3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader3.Location = new System.Drawing.Point(68, 0);
            this.gcCustomColumnHeader3.Name = "gcCustomColumnHeader3";
            this.gcCustomColumnHeader3.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.gcCustomColumnHeader3.Size = new System.Drawing.Size(375, 21);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border2;
            cellStyle5.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader3.Style = cellStyle5;
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
            this.BIKO_CD.Size = new System.Drawing.Size(68, 21);
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
            this.BIKO_NOTE.Location = new System.Drawing.Point(68, 0);
            this.BIKO_NOTE.MaxLength = 120;
            this.BIKO_NOTE.Name = "BIKO_NOTE";
            this.BIKO_NOTE.PopupAfterExecute = null;
            this.BIKO_NOTE.PopupBeforeExecute = null;
            this.BIKO_NOTE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BIKO_NOTE.PopupSearchSendParams")));
            this.BIKO_NOTE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BIKO_NOTE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BIKO_NOTE.popupWindowSetting")));
            this.BIKO_NOTE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKO_NOTE.RegistCheckMethod")));
            this.BIKO_NOTE.Size = new System.Drawing.Size(375, 21);
            cellStyle2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.BIKO_NOTE.Style = cellStyle2;
            this.BIKO_NOTE.TabIndex = 2;
            this.BIKO_NOTE.Tag = "";
            // 
            // DETAIL_SYSTEM_ID
            // 
            this.DETAIL_SYSTEM_ID.DBFieldsName = "";
            this.DETAIL_SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.DETAIL_SYSTEM_ID.DisplayItemName = "";
            this.DETAIL_SYSTEM_ID.DisplayPopUp = null;
            this.DETAIL_SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DETAIL_SYSTEM_ID.FocusOutCheckMethod")));
            this.DETAIL_SYSTEM_ID.IsInputErrorOccured = false;
            this.DETAIL_SYSTEM_ID.ItemDefinedTypes = "int";
            this.DETAIL_SYSTEM_ID.Location = new System.Drawing.Point(396, 0);
            this.DETAIL_SYSTEM_ID.Name = "DETAIL_SYSTEM_ID";
            this.DETAIL_SYSTEM_ID.PopupAfterExecute = null;
            this.DETAIL_SYSTEM_ID.PopupBeforeExecute = null;
            this.DETAIL_SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DETAIL_SYSTEM_ID.PopupSearchSendParams")));
            this.DETAIL_SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DETAIL_SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DETAIL_SYSTEM_ID.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.DETAIL_SYSTEM_ID.RangeSetting = rangeSettingDto2;
            this.DETAIL_SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DETAIL_SYSTEM_ID.RegistCheckMethod")));
            this.DETAIL_SYSTEM_ID.ShortItemName = "";
            this.DETAIL_SYSTEM_ID.Size = new System.Drawing.Size(47, 21);
            cellStyle3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.DETAIL_SYSTEM_ID.Style = cellStyle3;
            this.DETAIL_SYSTEM_ID.TabIndex = 3;
            this.DETAIL_SYSTEM_ID.Tag = "全角20文字/半角40文字以内で入力してください";
            this.DETAIL_SYSTEM_ID.Visible = false;
            // 
            // BikoUchiwakeNyuryokuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle6.BackColor = System.Drawing.Color.Transparent;
            cellStyle6.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Silver);
            cellStyle6.Border = border3;
            cellStyle6.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle6.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle6.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle6.Format = "";
            cellStyle6.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle6.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle6.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle6.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle6.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle6.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle6.Margin = new System.Windows.Forms.Padding(0);
            cellStyle6.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle6.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle6.Padding = new System.Windows.Forms.Padding(0);
            cellStyle6.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle6.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle6.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle6.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle6.TextAngle = 0F;
            cellStyle6.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle6.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle6.TextIndent = 0;
            cellStyle6.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle6.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle6.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle6;
            this.Height = 42;
            this.Width = 447;

        }

        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell BIKO_CD;
        internal r_framework.CustomControl.GcCustomTextBoxCell BIKO_NOTE;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader3;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell DETAIL_SYSTEM_ID;
    }
}
