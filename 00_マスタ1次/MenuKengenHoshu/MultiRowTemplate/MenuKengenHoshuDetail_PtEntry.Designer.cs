namespace MenuKengenHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class MenuKengenHoshuDetail_PtEntry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MenuKengenHoshuDetail_PtEntry));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.HD_PATTERN_NAME = new r_framework.CustomControl.GcCustomColumnHeader();
            this.HD_PATTERN_FURIGANA = new r_framework.CustomControl.GcCustomColumnHeader();
            this.PATTERN_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.PATTERN_FURIGANA = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.PATTERN_ID = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.PATTERN_NAME);
            this.Row.Cells.Add(this.PATTERN_FURIGANA);
            this.Row.Cells.Add(this.PATTERN_ID);
            this.Row.Height = 21;
            this.Row.Width = 250;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.HD_PATTERN_NAME);
            this.columnHeaderSection1.Cells.Add(this.HD_PATTERN_FURIGANA);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.Width = 250;
            // 
            // HD_PATTERN_NAME
            // 
            this.HD_PATTERN_NAME.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HD_PATTERN_NAME.Location = new System.Drawing.Point(0, 0);
            this.HD_PATTERN_NAME.Name = "HD_PATTERN_NAME";
            this.HD_PATTERN_NAME.Size = new System.Drawing.Size(150, 21);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle4.Border = border1;
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.HD_PATTERN_NAME.Style = cellStyle4;
            this.HD_PATTERN_NAME.TabIndex = 0;
            this.HD_PATTERN_NAME.Value = "パターン名";
            this.HD_PATTERN_NAME.ViewSearchItem = false;
            // 
            // HD_PATTERN_FURIGANA
            // 
            this.HD_PATTERN_FURIGANA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HD_PATTERN_FURIGANA.Location = new System.Drawing.Point(150, 0);
            this.HD_PATTERN_FURIGANA.Name = "HD_PATTERN_FURIGANA";
            this.HD_PATTERN_FURIGANA.Size = new System.Drawing.Size(100, 21);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border2;
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.HD_PATTERN_FURIGANA.Style = cellStyle5;
            this.HD_PATTERN_FURIGANA.TabIndex = 1;
            this.HD_PATTERN_FURIGANA.Value = "フリガナ";
            this.HD_PATTERN_FURIGANA.ViewSearchItem = false;
            // 
            // PATTERN_NAME
            // 
            this.PATTERN_NAME.AutoChangeBackColorEnabled = false;
            this.PATTERN_NAME.DataField = "PATTERN_NAME";
            this.PATTERN_NAME.DBFieldsName = "PATTERN_NAME";
            this.PATTERN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.PATTERN_NAME.DisplayItemName = "パターン名";
            this.PATTERN_NAME.DisplayPopUp = null;
            this.PATTERN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.FocusOutCheckMethod")));
            this.PATTERN_NAME.IsInputErrorOccured = false;
            this.PATTERN_NAME.ItemDefinedTypes = "varchar";
            this.PATTERN_NAME.Location = new System.Drawing.Point(0, 0);
            this.PATTERN_NAME.Name = "PATTERN_NAME";
            this.PATTERN_NAME.PopupAfterExecute = null;
            this.PATTERN_NAME.PopupBeforeExecute = null;
            this.PATTERN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_NAME.PopupSearchSendParams")));
            this.PATTERN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_NAME.popupWindowSetting")));
            this.PATTERN_NAME.ReadOnly = true;
            this.PATTERN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.RegistCheckMethod")));
            this.PATTERN_NAME.ShortItemName = "パターン名";
            this.PATTERN_NAME.Size = new System.Drawing.Size(150, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.PATTERN_NAME.Style = cellStyle1;
            this.PATTERN_NAME.TabIndex = 0;
            this.PATTERN_NAME.Tag = "パターン名が表示されます";
            // 
            // PATTERN_FURIGANA
            // 
            this.PATTERN_FURIGANA.AutoChangeBackColorEnabled = false;
            this.PATTERN_FURIGANA.DataField = "PATTERN_FURIGANA";
            this.PATTERN_FURIGANA.DBFieldsName = "PATTERN_FURIGANA";
            this.PATTERN_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            this.PATTERN_FURIGANA.DisplayItemName = "フリガナ";
            this.PATTERN_FURIGANA.DisplayPopUp = null;
            this.PATTERN_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_FURIGANA.FocusOutCheckMethod")));
            this.PATTERN_FURIGANA.IsInputErrorOccured = false;
            this.PATTERN_FURIGANA.ItemDefinedTypes = "varchar";
            this.PATTERN_FURIGANA.Location = new System.Drawing.Point(150, 0);
            this.PATTERN_FURIGANA.Name = "PATTERN_FURIGANA";
            this.PATTERN_FURIGANA.PopupAfterExecute = null;
            this.PATTERN_FURIGANA.PopupBeforeExecute = null;
            this.PATTERN_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_FURIGANA.PopupSearchSendParams")));
            this.PATTERN_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_FURIGANA.popupWindowSetting")));
            this.PATTERN_FURIGANA.ReadOnly = true;
            this.PATTERN_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_FURIGANA.RegistCheckMethod")));
            this.PATTERN_FURIGANA.ShortItemName = "フリガナ";
            this.PATTERN_FURIGANA.Size = new System.Drawing.Size(100, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.PATTERN_FURIGANA.Style = cellStyle2;
            this.PATTERN_FURIGANA.TabIndex = 1;
            this.PATTERN_FURIGANA.Tag = "フリガナが表示されます";
            // 
            // PATTERN_ID
            // 
            this.PATTERN_ID.DataField = "PATTERN_ID";
            this.PATTERN_ID.DBFieldsName = "PATTERN_ID";
            this.PATTERN_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.PATTERN_ID.DisplayItemName = "パターンID";
            this.PATTERN_ID.DisplayPopUp = null;
            this.PATTERN_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_ID.FocusOutCheckMethod")));
            this.PATTERN_ID.IsInputErrorOccured = false;
            this.PATTERN_ID.ItemDefinedTypes = "long";
            this.PATTERN_ID.Location = new System.Drawing.Point(250, 0);
            this.PATTERN_ID.Name = "PATTERN_ID";
            this.PATTERN_ID.PopupAfterExecute = null;
            this.PATTERN_ID.PopupBeforeExecute = null;
            this.PATTERN_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_ID.PopupSearchSendParams")));
            this.PATTERN_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_ID.popupWindowSetting")));
            this.PATTERN_ID.PrevText = null;
            this.PATTERN_ID.RangeSetting = rangeSettingDto1;
            this.PATTERN_ID.ReadOnly = true;
            this.PATTERN_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_ID.RegistCheckMethod")));
            this.PATTERN_ID.ShortItemName = "パターンID";
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle3.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.Normal;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.PATTERN_ID.Style = cellStyle3;
            this.PATTERN_ID.TabIndex = 2;
            this.PATTERN_ID.TabStop = false;
            // 
            // MenuKengenHoshuDetail_PtEntry
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
            cellStyle6.SelectionBackColor = System.Drawing.SystemColors.HighlightText;
            cellStyle6.SelectionForeColor = System.Drawing.SystemColors.WindowText;
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
            this.Width = 250;

        }

        #endregion

        private GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        private r_framework.CustomControl.GcCustomColumnHeader HD_PATTERN_NAME;
        private r_framework.CustomControl.GcCustomTextBoxCell PATTERN_NAME;
        private r_framework.CustomControl.GcCustomColumnHeader HD_PATTERN_FURIGANA;
        private r_framework.CustomControl.GcCustomTextBoxCell PATTERN_FURIGANA;
        private r_framework.CustomControl.GcCustomNumericTextBox2Cell PATTERN_ID;
    }
}
