namespace BookmarkHoshu.MultiRowTemplate
{
    [System.ComponentModel.ToolboxItem(true)]
    partial class BookmarkHoshuDetail
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
            GrapeCity.Win.MultiRow.Border border1 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle6 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border2 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle7 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border3 = new GrapeCity.Win.MultiRow.Border();
            GrapeCity.Win.MultiRow.CellStyle cellStyle8 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border4 = new GrapeCity.Win.MultiRow.Border();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookmarkHoshuDetail));
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle3 = new GrapeCity.Win.MultiRow.CellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle4 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle9 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.Border border5 = new GrapeCity.Win.MultiRow.Border();
            this.columnHeaderSection1 = new GrapeCity.Win.MultiRow.ColumnHeaderSection();
            this.HD_KUBUN_NAME = new r_framework.CustomControl.GcCustomColumnHeader();
            this.HD_KINOU_NAME = new r_framework.CustomControl.GcCustomColumnHeader();
            this.columnHeaderCell4 = new r_framework.CustomControl.GcCustomColumnHeader();
            this.HD_MY_FAVORITE = new r_framework.CustomControl.GcCustomColumnHeader();
            this.KUBUN_NAME = new r_framework.CustomControl.GcCustomAlphaNumTextBoxCell();
            this.KINOU_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.MENU_NAME = new r_framework.CustomControl.GcCustomTextBoxCell();
            this.MY_FAVORITE = new r_framework.CustomControl.GcCustomNumericTextBox2Cell();
            // 
            // Row
            // 
            this.Row.Cells.Add(this.KUBUN_NAME);
            this.Row.Cells.Add(this.KINOU_NAME);
            this.Row.Cells.Add(this.MENU_NAME);
            this.Row.Cells.Add(this.MY_FAVORITE);
            this.Row.Height = 21;
            this.Row.Width = 556;
            // 
            // columnHeaderSection1
            // 
            this.columnHeaderSection1.Cells.Add(this.HD_KUBUN_NAME);
            this.columnHeaderSection1.Cells.Add(this.HD_KINOU_NAME);
            this.columnHeaderSection1.Cells.Add(this.columnHeaderCell4);
            this.columnHeaderSection1.Cells.Add(this.HD_MY_FAVORITE);
            this.columnHeaderSection1.Height = 21;
            this.columnHeaderSection1.Name = "columnHeaderSection1";
            this.columnHeaderSection1.ReadOnly = false;
            this.columnHeaderSection1.Selectable = true;
            this.columnHeaderSection1.Width = 556;
            // 
            // HD_KUBUN_NAME
            // 
            this.HD_KUBUN_NAME.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HD_KUBUN_NAME.Location = new System.Drawing.Point(0, 0);
            this.HD_KUBUN_NAME.Name = "HD_KUBUN_NAME";
            this.HD_KUBUN_NAME.Size = new System.Drawing.Size(160, 21);
            cellStyle5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border1.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle5.Border = border1;
            cellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle5.ForeColor = System.Drawing.Color.White;
            cellStyle5.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.HD_KUBUN_NAME.Style = cellStyle5;
            this.HD_KUBUN_NAME.TabIndex = 0;
            this.HD_KUBUN_NAME.Value = "区分";
            this.HD_KUBUN_NAME.ViewSearchItem = false;
            // 
            // HD_KINOU_NAME
            // 
            this.HD_KINOU_NAME.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HD_KINOU_NAME.Location = new System.Drawing.Point(160, 0);
            this.HD_KINOU_NAME.Name = "HD_KINOU_NAME";
            this.HD_KINOU_NAME.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.HD_KINOU_NAME.Size = new System.Drawing.Size(160, 21);
            cellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border2.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle6.Border = border2;
            cellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle6.ForeColor = System.Drawing.Color.White;
            cellStyle6.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.HD_KINOU_NAME.Style = cellStyle6;
            this.HD_KINOU_NAME.TabIndex = 1;
            this.HD_KINOU_NAME.Value = "機能";
            this.HD_KINOU_NAME.ViewSearchItem = true;
            // 
            // columnHeaderCell4
            // 
            this.columnHeaderCell4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.columnHeaderCell4.Location = new System.Drawing.Point(320, 0);
            this.columnHeaderCell4.Name = "columnHeaderCell4";
            this.columnHeaderCell4.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.columnHeaderCell4.Size = new System.Drawing.Size(180, 21);
            cellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border3.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle7.Border = border3;
            cellStyle7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle7.ForeColor = System.Drawing.Color.White;
            cellStyle7.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.columnHeaderCell4.Style = cellStyle7;
            this.columnHeaderCell4.TabIndex = 2;
            this.columnHeaderCell4.Value = "メニュー";
            this.columnHeaderCell4.ViewSearchItem = true;
            // 
            // HD_MY_FAVORITE
            // 
            this.HD_MY_FAVORITE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HD_MY_FAVORITE.Location = new System.Drawing.Point(500, 0);
            this.HD_MY_FAVORITE.Name = "HD_MY_FAVORITE";
            this.HD_MY_FAVORITE.SelectionMode = GrapeCity.Win.MultiRow.MultiRowSelectionMode.None;
            this.HD_MY_FAVORITE.Size = new System.Drawing.Size(56, 21);
            cellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            border4.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Gray);
            cellStyle8.Border = border4;
            cellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle8.ForeColor = System.Drawing.Color.White;
            cellStyle8.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.HD_MY_FAVORITE.Style = cellStyle8;
            this.HD_MY_FAVORITE.TabIndex = 3;
            this.HD_MY_FAVORITE.Value = "表示";
            this.HD_MY_FAVORITE.ViewSearchItem = false;
            // 
            // KUBUN_NAME
            // 
            this.KUBUN_NAME.CharacterLimitList = null;
            this.KUBUN_NAME.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.KUBUN_NAME.DataField = "KUBUN_NAME";
            this.KUBUN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KUBUN_NAME.DisplayItemName = "区分";
            this.KUBUN_NAME.DisplayPopUp = null;
            this.KUBUN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KUBUN_NAME.FocusOutCheckMethod")));
            this.KUBUN_NAME.IsInputErrorOccured = false;
            this.KUBUN_NAME.ItemDefinedTypes = "varchar";
            this.KUBUN_NAME.Location = new System.Drawing.Point(0, 0);
            this.KUBUN_NAME.Mergeable = true;
            this.KUBUN_NAME.Name = "KUBUN_NAME";
            this.KUBUN_NAME.PopupAfterExecute = null;
            this.KUBUN_NAME.PopupBeforeExecute = null;
            this.KUBUN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KUBUN_NAME.PopupSearchSendParams")));
            this.KUBUN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KUBUN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KUBUN_NAME.popupWindowSetting")));
            this.KUBUN_NAME.ReadOnly = true;
            this.KUBUN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KUBUN_NAME.RegistCheckMethod")));
            this.KUBUN_NAME.Selectable = false;
            this.KUBUN_NAME.SetFormField = "MENU_NAME";
            this.KUBUN_NAME.ShortItemName = "";
            this.KUBUN_NAME.Size = new System.Drawing.Size(160, 21);
            cellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.KUBUN_NAME.Style = cellStyle1;
            this.KUBUN_NAME.TabIndex = 0;
            this.KUBUN_NAME.Tag = "";
            this.KUBUN_NAME.ToolTipText = "区分が表示されます";
            // 
            // KINOU_NAME
            // 
            this.KINOU_NAME.CopyAutoSetControl = "";
            this.KINOU_NAME.DataField = "KINOU_NAME";
            this.KINOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KINOU_NAME.DisplayItemName = "カテゴリー";
            this.KINOU_NAME.DisplayPopUp = null;
            this.KINOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KINOU_NAME.FocusOutCheckMethod")));
            this.KINOU_NAME.FuriganaAutoSetControl = "";
            this.KINOU_NAME.IsInputErrorOccured = false;
            this.KINOU_NAME.ItemDefinedTypes = "varchar";
            this.KINOU_NAME.Location = new System.Drawing.Point(160, 0);
            this.KINOU_NAME.Mergeable = true;
            this.KINOU_NAME.Name = "KINOU_NAME";
            this.KINOU_NAME.PopupAfterExecute = null;
            this.KINOU_NAME.PopupBeforeExecute = null;
            this.KINOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KINOU_NAME.PopupSearchSendParams")));
            this.KINOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KINOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KINOU_NAME.popupWindowSetting")));
            this.KINOU_NAME.ReadOnly = true;
            this.KINOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KINOU_NAME.RegistCheckMethod")));
            this.KINOU_NAME.Selectable = false;
            this.KINOU_NAME.SetFormField = "MENU_NAME";
            this.KINOU_NAME.ShortItemName = "";
            this.KINOU_NAME.Size = new System.Drawing.Size(160, 21);
            cellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle2.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.KINOU_NAME.Style = cellStyle2;
            this.KINOU_NAME.TabIndex = 1;
            this.KINOU_NAME.Tag = "";
            this.KINOU_NAME.ToolTipText = "機能が表示されます";
            // 
            // MENU_NAME
            // 
            this.MENU_NAME.DataField = "MENU_NAME";
            this.MENU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.MENU_NAME.DisplayItemName = "メニュー";
            this.MENU_NAME.DisplayPopUp = null;
            this.MENU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MENU_NAME.FocusOutCheckMethod")));
            this.MENU_NAME.IsInputErrorOccured = false;
            this.MENU_NAME.ItemDefinedTypes = "varchar";
            this.MENU_NAME.Location = new System.Drawing.Point(320, 0);
            this.MENU_NAME.Name = "MENU_NAME";
            this.MENU_NAME.PopupAfterExecute = null;
            this.MENU_NAME.PopupBeforeExecute = null;
            this.MENU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MENU_NAME.PopupSearchSendParams")));
            this.MENU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MENU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MENU_NAME.popupWindowSetting")));
            this.MENU_NAME.ReadOnly = true;
            this.MENU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MENU_NAME.RegistCheckMethod")));
            this.MENU_NAME.Selectable = false;
            this.MENU_NAME.SetFormField = "MENU_NAME";
            this.MENU_NAME.Size = new System.Drawing.Size(180, 21);
            cellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            cellStyle3.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle3.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            cellStyle3.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopLeft;
            this.MENU_NAME.Style = cellStyle3;
            this.MENU_NAME.TabIndex = 2;
            this.MENU_NAME.Tag = "";
            this.MENU_NAME.ToolTipText = "メニューが表示されます";
            // 
            // MY_FAVORITE
            // 
            this.MY_FAVORITE.DataField = "MY_FAVORITE";
            this.MY_FAVORITE.DefaultBackColor = System.Drawing.Color.Empty;
            this.MY_FAVORITE.DisplayItemName = "表示順";
            this.MY_FAVORITE.DisplayPopUp = null;
            this.MY_FAVORITE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MY_FAVORITE.FocusOutCheckMethod")));
            this.MY_FAVORITE.IsInputErrorOccured = false;
            this.MY_FAVORITE.Location = new System.Drawing.Point(500, 0);
            this.MY_FAVORITE.Name = "MY_FAVORITE";
            this.MY_FAVORITE.PopupAfterExecute = null;
            this.MY_FAVORITE.PopupBeforeExecute = null;
            this.MY_FAVORITE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MY_FAVORITE.PopupSearchSendParams")));
            this.MY_FAVORITE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MY_FAVORITE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MY_FAVORITE.popupWindowSetting")));
            this.MY_FAVORITE.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            10,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MY_FAVORITE.RangeSetting = rangeSettingDto1;
            this.MY_FAVORITE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MY_FAVORITE.RegistCheckMethod")));
            this.MY_FAVORITE.Size = new System.Drawing.Size(56, 21);
            cellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            cellStyle4.ImeMode = System.Windows.Forms.ImeMode.Disable;
            cellStyle4.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.Normal;
            cellStyle4.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle4.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.TopRight;
            this.MY_FAVORITE.Style = cellStyle4;
            this.MY_FAVORITE.TabIndex = 3;
            this.MY_FAVORITE.Tag = "マイメニューに追加したいメニューに数字【1～10】を入れてください";
            // 
            // BookmarkHoshuDetail
            // 
            this.ColumnHeaders.AddRange(new GrapeCity.Win.MultiRow.ColumnHeaderSection[] {
            this.columnHeaderSection1});
            cellStyle9.BackColor = System.Drawing.Color.Transparent;
            cellStyle9.BackgroundGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            border5.Outline = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Thin, System.Drawing.Color.Silver);
            cellStyle9.Border = border5;
            cellStyle9.DisabledBackColor = System.Drawing.SystemColors.Control;
            cellStyle9.DisabledForeColor = System.Drawing.SystemColors.GrayText;
            cellStyle9.DisabledGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle9.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle9.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle9.Format = "";
            cellStyle9.GradientDirection = GrapeCity.Win.MultiRow.GradientDirection.Center;
            cellStyle9.GradientStyle = GrapeCity.Win.MultiRow.GradientStyle.None;
            cellStyle9.ImageAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            cellStyle9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            cellStyle9.ImeSentenceMode = GrapeCity.Win.MultiRow.ImeSentenceMode.NoControl;
            cellStyle9.InputScope = GrapeCity.Win.MultiRow.InputScopeNameValue.Default;
            cellStyle9.LineAdjustment = GrapeCity.Win.MultiRow.LineAdjustment.None;
            cellStyle9.Margin = new System.Windows.Forms.Padding(0);
            cellStyle9.MouseOverGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle9.Multiline = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle9.Padding = new System.Windows.Forms.Padding(0);
            cellStyle9.PatternColor = System.Drawing.SystemColors.WindowText;
            cellStyle9.PatternStyle = GrapeCity.Win.MultiRow.MultiRowHatchStyle.None;
            cellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            cellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            cellStyle9.SelectionGradientEffect = new GrapeCity.Win.MultiRow.GradientEffect(null, GrapeCity.Win.MultiRow.GradientStyle.None, GrapeCity.Win.MultiRow.GradientDirection.Center);
            cellStyle9.TextAdjustment = GrapeCity.Win.MultiRow.TextAdjustment.Near;
            cellStyle9.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
            cellStyle9.TextAngle = 0F;
            cellStyle9.TextEffect = GrapeCity.Win.MultiRow.TextEffect.Flat;
            cellStyle9.TextImageRelation = GrapeCity.Win.MultiRow.MultiRowTextImageRelation.Overlay;
            cellStyle9.TextIndent = 0;
            cellStyle9.TextVertical = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle9.UseCompatibleTextRendering = GrapeCity.Win.MultiRow.MultiRowTriState.False;
            cellStyle9.WordWrap = GrapeCity.Win.MultiRow.MultiRowTriState.True;
            this.DefaultCellStyle = cellStyle9;
            this.Height = 42;
            this.Width = 556;

        }


        #endregion

        public GrapeCity.Win.MultiRow.ColumnHeaderSection columnHeaderSection1;
        public r_framework.CustomControl.GcCustomColumnHeader HD_KUBUN_NAME;
        public r_framework.CustomControl.GcCustomColumnHeader HD_KINOU_NAME;
        public r_framework.CustomControl.GcCustomColumnHeader columnHeaderCell4;
        public r_framework.CustomControl.GcCustomColumnHeader HD_MY_FAVORITE;
        private r_framework.CustomControl.GcCustomTextBoxCell KINOU_NAME;
        private r_framework.CustomControl.GcCustomAlphaNumTextBoxCell KUBUN_NAME;
        private r_framework.CustomControl.GcCustomTextBoxCell MENU_NAME;
        private r_framework.CustomControl.GcCustomNumericTextBox2Cell MY_FAVORITE;
    }
}
