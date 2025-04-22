namespace SystemSetteiHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class SystemSetteiHoshuDetail
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SystemSetteiHoshuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            this.gcCustomColumnHeader1 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.gcCustomColumnHeader2 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.ITEM_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.TAB_INDEX = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            this.CONTOROL_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.ITEM_NAME);
            this.Row.Cells.Add(this.TAB_INDEX);
            this.Row.Cells.Add(this.CONTOROL_NAME);
            this.Row.Height = 21;
            this.Row.Width = 305;
            // 
            // gcCustomColumnHeader1
            // 
            this.gcCustomColumnHeader1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader1.Location = new System.Drawing.Point(0, 0);
            this.gcCustomColumnHeader1.Name = "gcCustomColumnHeader1";
            this.gcCustomColumnHeader1.Size = new System.Drawing.Size(250, 20);
            cellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle4.Border = border1;
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle4.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader1.Style = cellStyle4;
            this.gcCustomColumnHeader1.TabIndex = 23;
            this.gcCustomColumnHeader1.Value = "項目名";
            this.gcCustomColumnHeader1.ViewSearchItem = true;
            // 
            // gcCustomColumnHeader2
            // 
            this.gcCustomColumnHeader2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gcCustomColumnHeader2.Location = new System.Drawing.Point(250, 0);
            this.gcCustomColumnHeader2.Name = "gcCustomColumnHeader2";
            this.gcCustomColumnHeader2.Size = new System.Drawing.Size(55, 20);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.SystemColors.Window);
            cellStyle5.Border = border2;
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            this.gcCustomColumnHeader2.Style = cellStyle5;
            this.gcCustomColumnHeader2.TabIndex = 22;
            this.gcCustomColumnHeader2.Value = "タブ順";
            this.gcCustomColumnHeader2.ViewSearchItem = true;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader2);
            this.columnHeaderSection1.Cells.Add(this.gcCustomColumnHeader1);
            this.columnHeaderSection1.Height = 20;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.ReadOnly = false;
            this.columnHeaderSection1.Selectable = true;
            this.columnHeaderSection1.Width = 305;
            // 
            // ITEM_NAME
            // 
            this.ITEM_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.ITEM_NAME.CopyAutoSetControl = "";
            this.ITEM_NAME.DBFieldsName = "";
            this.ITEM_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.ITEM_NAME.DisplayItemName = "項目名";
            this.ITEM_NAME.DisplayPopUp = null;
            this.ITEM_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITEM_NAME.FocusOutCheckMethod")));
            this.ITEM_NAME.FuriganaAutoSetControl = "";
            this.ITEM_NAME.IsInputErrorOccured = false;
            this.ITEM_NAME.ItemDefinedTypes = "varchar";
            this.ITEM_NAME.Location = new System.Drawing.Point(0, 0);
            this.ITEM_NAME.MaxLength = 20;
            this.ITEM_NAME.Name = "ITEM_NAME";
            this.ITEM_NAME.PopupAfterExecute = null;
            this.ITEM_NAME.PopupBeforeExecute = null;
            this.ITEM_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ITEM_NAME.PopupSearchSendParams")));
            this.ITEM_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ITEM_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ITEM_NAME.popupWindowSetting")));
            this.ITEM_NAME.ReadOnly = true;
            this.ITEM_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ITEM_NAME.RegistCheckMethod")));
            this.ITEM_NAME.Selectable = false;
            this.ITEM_NAME.ShortItemName = "項目名";
            this.ITEM_NAME.Size = new System.Drawing.Size(250, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle1.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.ITEM_NAME.Style = cellStyle1;
            this.ITEM_NAME.TabIndex = 1;
            this.ITEM_NAME.Tag = "項目名が表示されます";
            // 
            // TAB_INDEX
            // 
            this.TAB_INDEX.DBFieldsName = "";
            this.TAB_INDEX.DefaultBackColor = System.Drawing.Color.Empty;
            this.TAB_INDEX.DisplayItemName = "タブ順";
            this.TAB_INDEX.DisplayPopUp = null;
            this.TAB_INDEX.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TAB_INDEX.FocusOutCheckMethod")));
            this.TAB_INDEX.IsInputErrorOccured = false;
            this.TAB_INDEX.ItemDefinedTypes = "varchar";
            this.TAB_INDEX.Location = new System.Drawing.Point(250, 0);
            this.TAB_INDEX.Name = "TAB_INDEX";
            this.TAB_INDEX.PopupAfterExecute = null;
            this.TAB_INDEX.PopupBeforeExecute = null;
            this.TAB_INDEX.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TAB_INDEX.PopupSearchSendParams")));
            this.TAB_INDEX.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TAB_INDEX.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TAB_INDEX.popupWindowSetting")));
            this.TAB_INDEX.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.TAB_INDEX.RangeSetting = rangeSettingDto1;
            this.TAB_INDEX.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TAB_INDEX.RegistCheckMethod")));
            this.TAB_INDEX.ShortItemName = "タブ順";
            this.TAB_INDEX.Size = new System.Drawing.Size(55, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
            this.TAB_INDEX.Style = cellStyle2;
            this.TAB_INDEX.TabIndex = 2;
            this.TAB_INDEX.Tag = "タブ順を指定してください";
            // 
            // CONTOROL_NAME
            // 
            this.CONTOROL_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.CONTOROL_NAME.CopyAutoSetControl = "";
            this.CONTOROL_NAME.DBFieldsName = "";
            this.CONTOROL_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONTOROL_NAME.DisplayItemName = "コントロール名";
            this.CONTOROL_NAME.DisplayPopUp = null;
            this.CONTOROL_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTOROL_NAME.FocusOutCheckMethod")));
            this.CONTOROL_NAME.FuriganaAutoSetControl = "";
            this.CONTOROL_NAME.IsInputErrorOccured = false;
            this.CONTOROL_NAME.ItemDefinedTypes = "varchar";
            this.CONTOROL_NAME.Location = new System.Drawing.Point(0, 0);
            this.CONTOROL_NAME.MaxLength = 20;
            this.CONTOROL_NAME.Name = "CONTOROL_NAME";
            this.CONTOROL_NAME.PopupAfterExecute = null;
            this.CONTOROL_NAME.PopupBeforeExecute = null;
            this.CONTOROL_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTOROL_NAME.PopupSearchSendParams")));
            this.CONTOROL_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTOROL_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTOROL_NAME.popupWindowSetting")));
            this.CONTOROL_NAME.ReadOnly = true;
            this.CONTOROL_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTOROL_NAME.RegistCheckMethod")));
            this.CONTOROL_NAME.Selectable = false;
            this.CONTOROL_NAME.ShortItemName = "コントロール名";
            this.CONTOROL_NAME.Size = new System.Drawing.Size(250, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            this.CONTOROL_NAME.Style = cellStyle3;
            this.CONTOROL_NAME.TabIndex = 3;
            this.CONTOROL_NAME.TabStop = false;
            this.CONTOROL_NAME.Tag = "";
            this.CONTOROL_NAME.Visible = false;
            // 
            // SystemSetteiHoshuDetail
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
            this.Height = 41;
            this.Width = 305;

        }

        #endregion

        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader1;
        internal r_framework.CustomControl.GcCustomColumnHeader gcCustomColumnHeader2;
        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        internal r_framework.CustomControl.GcCustomTextBoxCell ITEM_NAME;
        internal r_framework.CustomControl.GcCustomNumericTextBox2Cell TAB_INDEX;
        internal r_framework.CustomControl.GcCustomTextBoxCell CONTOROL_NAME;
    }
}
