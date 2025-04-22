namespace Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku
{
    partial class UIForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto6 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto7 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto8 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto9 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto10 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto11 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto12 = new r_framework.Dto.RangeSettingDto();
            this.SHIME_STATUS = new r_framework.CustomControl.CustomTextBox();
            this.lblShimeStatus = new System.Windows.Forms.Label();
            this.GETSUJI_DATE = new r_framework.CustomControl.CustomDateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblTorihikisakiCd = new System.Windows.Forms.Label();
            this.DETAIL = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.PREVIOUS_MONTH_BALANCE = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.NYUUSHUKKIN_KINGAKU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.KINGAKU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.TAX = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.TOTAL_KINGAKU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.LOCK_ZANDAKA = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.ADJUST_TAX = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.ZANDAKA = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.YEAR = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.MONTH = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.SEQ = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.UPDATE_SEQ = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.SEARCH_TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SEARCH_TORIHIKISAKI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.btnSearchTorihikisaki = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DETAIL)).BeginInit();
            this.SuspendLayout();
            // 
            // SHIME_STATUS
            // 
            this.SHIME_STATUS.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHIME_STATUS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHIME_STATUS.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.SHIME_STATUS.DBFieldsName = "";
            this.SHIME_STATUS.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHIME_STATUS.DisplayItemName = "ステータス";
            this.SHIME_STATUS.DisplayPopUp = null;
            this.SHIME_STATUS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIME_STATUS.FocusOutCheckMethod")));
            this.SHIME_STATUS.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHIME_STATUS.ForeColor = System.Drawing.Color.Black;
            this.SHIME_STATUS.IsInputErrorOccured = false;
            this.SHIME_STATUS.ItemDefinedTypes = "varchar";
            this.SHIME_STATUS.Location = new System.Drawing.Point(375, 0);
            this.SHIME_STATUS.MaxLength = 0;
            this.SHIME_STATUS.Name = "SHIME_STATUS";
            this.SHIME_STATUS.PopupAfterExecute = null;
            this.SHIME_STATUS.PopupBeforeExecute = null;
            this.SHIME_STATUS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHIME_STATUS.PopupSearchSendParams")));
            this.SHIME_STATUS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHIME_STATUS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHIME_STATUS.popupWindowSetting")));
            this.SHIME_STATUS.prevText = null;
            this.SHIME_STATUS.PrevText = null;
            this.SHIME_STATUS.ReadOnly = true;
            this.SHIME_STATUS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIME_STATUS.RegistCheckMethod")));
            this.SHIME_STATUS.Size = new System.Drawing.Size(110, 20);
            this.SHIME_STATUS.TabIndex = 15;
            this.SHIME_STATUS.TabStop = false;
            this.SHIME_STATUS.Tag = " ";
            // 
            // lblShimeStatus
            // 
            this.lblShimeStatus.BackColor = System.Drawing.Color.DarkGreen;
            this.lblShimeStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShimeStatus.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShimeStatus.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblShimeStatus.Location = new System.Drawing.Point(260, 0);
            this.lblShimeStatus.Name = "lblShimeStatus";
            this.lblShimeStatus.Size = new System.Drawing.Size(110, 20);
            this.lblShimeStatus.TabIndex = 19;
            this.lblShimeStatus.Text = "締状態";
            this.lblShimeStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GETSUJI_DATE
            // 
            this.GETSUJI_DATE.BackColor = System.Drawing.SystemColors.Window;
            this.GETSUJI_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GETSUJI_DATE.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.GETSUJI_DATE.Checked = false;
            this.GETSUJI_DATE.CustomFormat = "yyyy/MM";
            this.GETSUJI_DATE.DateTimeNowYear = "";
            this.GETSUJI_DATE.DBFieldsName = "";
            this.GETSUJI_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.GETSUJI_DATE.DisplayItemName = "月次年月";
            this.GETSUJI_DATE.DisplayPopUp = null;
            this.GETSUJI_DATE.Enabled = false;
            this.GETSUJI_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GETSUJI_DATE.FocusOutCheckMethod")));
            this.GETSUJI_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GETSUJI_DATE.ForeColor = System.Drawing.Color.Black;
            this.GETSUJI_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.GETSUJI_DATE.IsInputErrorOccured = false;
            this.GETSUJI_DATE.ItemDefinedTypes = "datetime";
            this.GETSUJI_DATE.Location = new System.Drawing.Point(120, 0);
            this.GETSUJI_DATE.MaxLength = 10;
            this.GETSUJI_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.GETSUJI_DATE.Name = "GETSUJI_DATE";
            this.GETSUJI_DATE.NullValue = "";
            this.GETSUJI_DATE.PopupAfterExecute = null;
            this.GETSUJI_DATE.PopupBeforeExecute = null;
            this.GETSUJI_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GETSUJI_DATE.PopupSearchSendParams")));
            this.GETSUJI_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GETSUJI_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GETSUJI_DATE.popupWindowSetting")));
            this.GETSUJI_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GETSUJI_DATE.RegistCheckMethod")));
            this.GETSUJI_DATE.ShowYoubi = false;
            this.GETSUJI_DATE.Size = new System.Drawing.Size(110, 20);
            this.GETSUJI_DATE.TabIndex = 10;
            this.GETSUJI_DATE.Tag = "月次処理対象年月が表示されます";
            this.GETSUJI_DATE.Text = "2014/12";
            this.GETSUJI_DATE.Value = new System.DateTime(2014, 12, 1, 0, 0, 0, 0);
            // 
            // lblDate
            // 
            this.lblDate.BackColor = System.Drawing.Color.DarkGreen;
            this.lblDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblDate.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblDate.Location = new System.Drawing.Point(5, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(110, 20);
            this.lblDate.TabIndex = 17;
            this.lblDate.Text = "月次年月";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTorihikisakiCd
            // 
            this.lblTorihikisakiCd.BackColor = System.Drawing.Color.DarkGreen;
            this.lblTorihikisakiCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTorihikisakiCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTorihikisakiCd.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lblTorihikisakiCd.Location = new System.Drawing.Point(5, 70);
            this.lblTorihikisakiCd.Name = "lblTorihikisakiCd";
            this.lblTorihikisakiCd.Size = new System.Drawing.Size(110, 20);
            this.lblTorihikisakiCd.TabIndex = 21;
            this.lblTorihikisakiCd.Text = "取引先";
            this.lblTorihikisakiCd.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DETAIL
            // 
            this.DETAIL.AllowUserToAddRows = false;
            this.DETAIL.AllowUserToDeleteRows = false;
            this.DETAIL.AllowUserToResizeRows = false;
            this.DETAIL.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DETAIL.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.DETAIL.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DETAIL.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TORIHIKISAKI_CD,
            this.TORIHIKISAKI_NAME_RYAKU,
            this.PREVIOUS_MONTH_BALANCE,
            this.NYUUSHUKKIN_KINGAKU,
            this.KINGAKU,
            this.TAX,
            this.TOTAL_KINGAKU,
            this.LOCK_ZANDAKA,
            this.ADJUST_TAX,
            this.ZANDAKA,
            this.YEAR,
            this.MONTH,
            this.SEQ,
            this.UPDATE_SEQ});
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DETAIL.DefaultCellStyle = dataGridViewCellStyle16;
            this.DETAIL.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.DETAIL.EnableHeadersVisualStyles = false;
            this.DETAIL.GridColor = System.Drawing.Color.White;
            this.DETAIL.IsReload = false;
            this.DETAIL.LinkedDataPanelName = null;
            this.DETAIL.Location = new System.Drawing.Point(5, 100);
            this.DETAIL.MultiSelect = false;
            this.DETAIL.Name = "DETAIL";
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.DETAIL.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.DETAIL.RowHeadersVisible = false;
            this.DETAIL.RowTemplate.Height = 21;
            this.DETAIL.ShowCellToolTips = false;
            this.DETAIL.Size = new System.Drawing.Size(990, 370);
            this.DETAIL.TabIndex = 30;
            this.DETAIL.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.DETAIL_CellEnter);
            this.DETAIL.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.DETAIL_CellValidated);
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD.DataPropertyName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DBFieldsName = "";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.DefaultCellStyle = dataGridViewCellStyle2;
            this.TORIHIKISAKI_CD.DisplayItemName = "";
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = null;
            this.TORIHIKISAKI_CD.HeaderText = "取引先CD";
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "";
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_CD.popupWindowSetting = null;
            this.TORIHIKISAKI_CD.ReadOnly = true;
            this.TORIHIKISAKI_CD.RegistCheckMethod = null;
            this.TORIHIKISAKI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TORIHIKISAKI_CD.ToolTipText = "　";
            this.TORIHIKISAKI_CD.Width = 80;
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.DataPropertyName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle3;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayItemName = "";
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.HeaderText = "取引先名";
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "";
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TORIHIKISAKI_NAME_RYAKU.ToolTipText = "　";
            // 
            // PREVIOUS_MONTH_BALANCE
            // 
            this.PREVIOUS_MONTH_BALANCE.CustomFormatSetting = "#,##0";
            this.PREVIOUS_MONTH_BALANCE.DataPropertyName = "PREVIOUS_MONTH_BALANCE";
            this.PREVIOUS_MONTH_BALANCE.DBFieldsName = "";
            this.PREVIOUS_MONTH_BALANCE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "#,##0";
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.PREVIOUS_MONTH_BALANCE.DefaultCellStyle = dataGridViewCellStyle4;
            this.PREVIOUS_MONTH_BALANCE.DisplayItemName = "";
            this.PREVIOUS_MONTH_BALANCE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PREVIOUS_MONTH_BALANCE.FocusOutCheckMethod")));
            this.PREVIOUS_MONTH_BALANCE.FormatSetting = "カスタム";
            this.PREVIOUS_MONTH_BALANCE.HeaderText = "繰越残高";
            this.PREVIOUS_MONTH_BALANCE.ItemDefinedTypes = "";
            this.PREVIOUS_MONTH_BALANCE.Name = "PREVIOUS_MONTH_BALANCE";
            this.PREVIOUS_MONTH_BALANCE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PREVIOUS_MONTH_BALANCE.PopupSearchSendParams")));
            this.PREVIOUS_MONTH_BALANCE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PREVIOUS_MONTH_BALANCE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PREVIOUS_MONTH_BALANCE.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.PREVIOUS_MONTH_BALANCE.RangeSetting = rangeSettingDto1;
            this.PREVIOUS_MONTH_BALANCE.ReadOnly = true;
            this.PREVIOUS_MONTH_BALANCE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PREVIOUS_MONTH_BALANCE.RegistCheckMethod")));
            this.PREVIOUS_MONTH_BALANCE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PREVIOUS_MONTH_BALANCE.ToolTipText = "　";
            // 
            // NYUUSHUKKIN_KINGAKU
            // 
            this.NYUUSHUKKIN_KINGAKU.CustomFormatSetting = "#,##0";
            this.NYUUSHUKKIN_KINGAKU.DataPropertyName = "NYUUSHUKKIN_KINGAKU";
            this.NYUUSHUKKIN_KINGAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "#,##0";
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.NYUUSHUKKIN_KINGAKU.DefaultCellStyle = dataGridViewCellStyle5;
            this.NYUUSHUKKIN_KINGAKU.DisplayItemName = "";
            this.NYUUSHUKKIN_KINGAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUSHUKKIN_KINGAKU.FocusOutCheckMethod")));
            this.NYUUSHUKKIN_KINGAKU.FormatSetting = "カスタム";
            this.NYUUSHUKKIN_KINGAKU.HeaderText = "入金額";
            this.NYUUSHUKKIN_KINGAKU.ItemDefinedTypes = "";
            this.NYUUSHUKKIN_KINGAKU.Name = "NYUUSHUKKIN_KINGAKU";
            this.NYUUSHUKKIN_KINGAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUSHUKKIN_KINGAKU.PopupSearchSendParams")));
            this.NYUUSHUKKIN_KINGAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUSHUKKIN_KINGAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUSHUKKIN_KINGAKU.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NYUUSHUKKIN_KINGAKU.RangeSetting = rangeSettingDto2;
            this.NYUUSHUKKIN_KINGAKU.ReadOnly = true;
            this.NYUUSHUKKIN_KINGAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUSHUKKIN_KINGAKU.RegistCheckMethod")));
            this.NYUUSHUKKIN_KINGAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NYUUSHUKKIN_KINGAKU.ToolTipText = "　";
            // 
            // KINGAKU
            // 
            this.KINGAKU.CustomFormatSetting = "#,##0";
            this.KINGAKU.DataPropertyName = "KINGAKU";
            this.KINGAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "#,##0";
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.KINGAKU.DefaultCellStyle = dataGridViewCellStyle6;
            this.KINGAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KINGAKU.FocusOutCheckMethod")));
            this.KINGAKU.FormatSetting = "カスタム";
            this.KINGAKU.HeaderText = "税抜売上金額";
            this.KINGAKU.Name = "KINGAKU";
            this.KINGAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KINGAKU.PopupSearchSendParams")));
            this.KINGAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KINGAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KINGAKU.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.KINGAKU.RangeSetting = rangeSettingDto3;
            this.KINGAKU.ReadOnly = true;
            this.KINGAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KINGAKU.RegistCheckMethod")));
            this.KINGAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TAX
            // 
            this.TAX.CustomFormatSetting = "#,##0";
            this.TAX.DataPropertyName = "TAX";
            this.TAX.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "#,##0";
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.TAX.DefaultCellStyle = dataGridViewCellStyle7;
            this.TAX.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TAX.FocusOutCheckMethod")));
            this.TAX.FormatSetting = "カスタム";
            this.TAX.HeaderText = "消費税";
            this.TAX.Name = "TAX";
            this.TAX.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TAX.PopupSearchSendParams")));
            this.TAX.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TAX.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TAX.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TAX.RangeSetting = rangeSettingDto4;
            this.TAX.ReadOnly = true;
            this.TAX.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TAX.RegistCheckMethod")));
            this.TAX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TAX.Width = 90;
            // 
            // TOTAL_KINGAKU
            // 
            this.TOTAL_KINGAKU.CustomFormatSetting = "#,##0";
            this.TOTAL_KINGAKU.DataPropertyName = "TOTAL_KINGAKU";
            this.TOTAL_KINGAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "#,##0";
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.TOTAL_KINGAKU.DefaultCellStyle = dataGridViewCellStyle8;
            this.TOTAL_KINGAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOTAL_KINGAKU.FocusOutCheckMethod")));
            this.TOTAL_KINGAKU.FormatSetting = "カスタム";
            this.TOTAL_KINGAKU.HeaderText = "税込売上金額";
            this.TOTAL_KINGAKU.Name = "TOTAL_KINGAKU";
            this.TOTAL_KINGAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TOTAL_KINGAKU.PopupSearchSendParams")));
            this.TOTAL_KINGAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TOTAL_KINGAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TOTAL_KINGAKU.popupWindowSetting")));
            rangeSettingDto5.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TOTAL_KINGAKU.RangeSetting = rangeSettingDto5;
            this.TOTAL_KINGAKU.ReadOnly = true;
            this.TOTAL_KINGAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOTAL_KINGAKU.RegistCheckMethod")));
            this.TOTAL_KINGAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LOCK_ZANDAKA
            // 
            this.LOCK_ZANDAKA.CustomFormatSetting = "#,##0";
            this.LOCK_ZANDAKA.DataPropertyName = "LOCK_ZANDAKA";
            this.LOCK_ZANDAKA.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "#,##0";
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.LOCK_ZANDAKA.DefaultCellStyle = dataGridViewCellStyle9;
            this.LOCK_ZANDAKA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LOCK_ZANDAKA.FocusOutCheckMethod")));
            this.LOCK_ZANDAKA.FormatSetting = "カスタム";
            this.LOCK_ZANDAKA.HeaderText = "調整前差引残高";
            this.LOCK_ZANDAKA.Name = "LOCK_ZANDAKA";
            this.LOCK_ZANDAKA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LOCK_ZANDAKA.PopupSearchSendParams")));
            this.LOCK_ZANDAKA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LOCK_ZANDAKA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LOCK_ZANDAKA.popupWindowSetting")));
            rangeSettingDto6.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.LOCK_ZANDAKA.RangeSetting = rangeSettingDto6;
            this.LOCK_ZANDAKA.ReadOnly = true;
            this.LOCK_ZANDAKA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LOCK_ZANDAKA.RegistCheckMethod")));
            this.LOCK_ZANDAKA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // ADJUST_TAX
            // 
            this.ADJUST_TAX.CustomFormatSetting = "#,##0";
            this.ADJUST_TAX.DataPropertyName = "ADJUST_TAX";
            this.ADJUST_TAX.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "#,##0";
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.ADJUST_TAX.DefaultCellStyle = dataGridViewCellStyle10;
            this.ADJUST_TAX.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ADJUST_TAX.FocusOutCheckMethod")));
            this.ADJUST_TAX.FormatSetting = "カスタム";
            this.ADJUST_TAX.HeaderText = "消費税調整額";
            this.ADJUST_TAX.Name = "ADJUST_TAX";
            this.ADJUST_TAX.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ADJUST_TAX.PopupSearchSendParams")));
            this.ADJUST_TAX.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ADJUST_TAX.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ADJUST_TAX.popupWindowSetting")));
            rangeSettingDto7.Max = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            rangeSettingDto7.Min = new decimal(new int[] {
            9999999,
            0,
            0,
            -2147483648});
            this.ADJUST_TAX.RangeSetting = rangeSettingDto7;
            this.ADJUST_TAX.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ADJUST_TAX.RegistCheckMethod")));
            this.ADJUST_TAX.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ADJUST_TAX.ToolTipText = "半角8桁以内で入力してください";
            // 
            // ZANDAKA
            // 
            this.ZANDAKA.CustomFormatSetting = "#,##0";
            this.ZANDAKA.DataPropertyName = "ZANDAKA";
            this.ZANDAKA.DBFieldsName = "";
            this.ZANDAKA.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "#,##0";
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.ZANDAKA.DefaultCellStyle = dataGridViewCellStyle11;
            this.ZANDAKA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZANDAKA.FocusOutCheckMethod")));
            this.ZANDAKA.FormatSetting = "カスタム";
            this.ZANDAKA.HeaderText = "調整後差引残高";
            this.ZANDAKA.Name = "ZANDAKA";
            this.ZANDAKA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZANDAKA.PopupSearchSendParams")));
            this.ZANDAKA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZANDAKA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZANDAKA.popupWindowSetting")));
            rangeSettingDto8.Max = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            rangeSettingDto8.Min = new decimal(new int[] {
            99999999,
            0,
            0,
            -2147483648});
            this.ZANDAKA.RangeSetting = rangeSettingDto8;
            this.ZANDAKA.ReadOnly = true;
            this.ZANDAKA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZANDAKA.RegistCheckMethod")));
            this.ZANDAKA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // YEAR
            // 
            this.YEAR.DataPropertyName = "YEAR";
            this.YEAR.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.YEAR.DefaultCellStyle = dataGridViewCellStyle12;
            this.YEAR.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YEAR.FocusOutCheckMethod")));
            this.YEAR.HeaderText = "YEAR";
            this.YEAR.Name = "YEAR";
            this.YEAR.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("YEAR.PopupSearchSendParams")));
            this.YEAR.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.YEAR.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("YEAR.popupWindowSetting")));
            rangeSettingDto9.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.YEAR.RangeSetting = rangeSettingDto9;
            this.YEAR.ReadOnly = true;
            this.YEAR.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("YEAR.RegistCheckMethod")));
            this.YEAR.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.YEAR.Visible = false;
            // 
            // MONTH
            // 
            this.MONTH.DataPropertyName = "MONTH";
            this.MONTH.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.MONTH.DefaultCellStyle = dataGridViewCellStyle13;
            this.MONTH.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MONTH.FocusOutCheckMethod")));
            this.MONTH.HeaderText = "MONTH";
            this.MONTH.Name = "MONTH";
            this.MONTH.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MONTH.PopupSearchSendParams")));
            this.MONTH.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MONTH.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MONTH.popupWindowSetting")));
            rangeSettingDto10.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.MONTH.RangeSetting = rangeSettingDto10;
            this.MONTH.ReadOnly = true;
            this.MONTH.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MONTH.RegistCheckMethod")));
            this.MONTH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MONTH.Visible = false;
            // 
            // SEQ
            // 
            this.SEQ.DataPropertyName = "SEQ";
            this.SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.SEQ.DefaultCellStyle = dataGridViewCellStyle14;
            this.SEQ.FocusOutCheckMethod = null;
            this.SEQ.HeaderText = "SEQ";
            this.SEQ.Name = "SEQ";
            this.SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEQ.PopupSearchSendParams")));
            this.SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEQ.popupWindowSetting = null;
            rangeSettingDto11.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SEQ.RangeSetting = rangeSettingDto11;
            this.SEQ.ReadOnly = true;
            this.SEQ.RegistCheckMethod = null;
            this.SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SEQ.Visible = false;
            // 
            // UPDATE_SEQ
            // 
            this.UPDATE_SEQ.DataPropertyName = "UPDATE_SEQ";
            this.UPDATE_SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.UPDATE_SEQ.DefaultCellStyle = dataGridViewCellStyle15;
            this.UPDATE_SEQ.FocusOutCheckMethod = null;
            this.UPDATE_SEQ.HeaderText = "UPDATE_SEQ";
            this.UPDATE_SEQ.Name = "UPDATE_SEQ";
            this.UPDATE_SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_SEQ.PopupSearchSendParams")));
            this.UPDATE_SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_SEQ.popupWindowSetting = null;
            rangeSettingDto12.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.UPDATE_SEQ.RangeSetting = rangeSettingDto12;
            this.UPDATE_SEQ.ReadOnly = true;
            this.UPDATE_SEQ.RegistCheckMethod = null;
            this.UPDATE_SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_SEQ.Visible = false;
            // 
            // SEARCH_TORIHIKISAKI_CD
            // 
            this.SEARCH_TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SEARCH_TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEARCH_TORIHIKISAKI_CD.ChangeUpperCase = true;
            this.SEARCH_TORIHIKISAKI_CD.CharacterLimitList = null;
            this.SEARCH_TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.SEARCH_TORIHIKISAKI_CD.DBFieldsName = "TORIHIKISAKI_CD";
            this.SEARCH_TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEARCH_TORIHIKISAKI_CD.DisplayItemName = "取引先";
            this.SEARCH_TORIHIKISAKI_CD.DisplayPopUp = null;
            this.SEARCH_TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.SEARCH_TORIHIKISAKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SEARCH_TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.SEARCH_TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.SEARCH_TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SEARCH_TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.SEARCH_TORIHIKISAKI_CD.Location = new System.Drawing.Point(120, 70);
            this.SEARCH_TORIHIKISAKI_CD.MaxLength = 6;
            this.SEARCH_TORIHIKISAKI_CD.Name = "SEARCH_TORIHIKISAKI_CD";
            this.SEARCH_TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.SEARCH_TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.SEARCH_TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.SEARCH_TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEARCH_TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.SEARCH_TORIHIKISAKI_CD.PopupSendParams = new string[0];
            this.SEARCH_TORIHIKISAKI_CD.PopupSetFormField = "SEARCH_TORIHIKISAKI_CD,SEARCH_TORIHIKISAKI_NAME";
            this.SEARCH_TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.SEARCH_TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.SEARCH_TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEARCH_TORIHIKISAKI_CD.popupWindowSetting")));
            this.SEARCH_TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_TORIHIKISAKI_CD.RegistCheckMethod")));
            this.SEARCH_TORIHIKISAKI_CD.SetFormField = "SEARCH_TORIHIKISAKI_CD,SEARCH_TORIHIKISAKI_NAME";
            this.SEARCH_TORIHIKISAKI_CD.ShortItemName = "取引先CD";
            this.SEARCH_TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.SEARCH_TORIHIKISAKI_CD.TabIndex = 20;
            this.SEARCH_TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SEARCH_TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            this.SEARCH_TORIHIKISAKI_CD.Validated += new System.EventHandler(this.SEARCH_TORIHIKISAKI_CD_Validated);
            // 
            // SEARCH_TORIHIKISAKI_NAME
            // 
            this.SEARCH_TORIHIKISAKI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SEARCH_TORIHIKISAKI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEARCH_TORIHIKISAKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEARCH_TORIHIKISAKI_NAME.DisplayPopUp = null;
            this.SEARCH_TORIHIKISAKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_TORIHIKISAKI_NAME.FocusOutCheckMethod")));
            this.SEARCH_TORIHIKISAKI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SEARCH_TORIHIKISAKI_NAME.ForeColor = System.Drawing.Color.Black;
            this.SEARCH_TORIHIKISAKI_NAME.IsInputErrorOccured = false;
            this.SEARCH_TORIHIKISAKI_NAME.Location = new System.Drawing.Point(169, 70);
            this.SEARCH_TORIHIKISAKI_NAME.Name = "SEARCH_TORIHIKISAKI_NAME";
            this.SEARCH_TORIHIKISAKI_NAME.PopupAfterExecute = null;
            this.SEARCH_TORIHIKISAKI_NAME.PopupBeforeExecute = null;
            this.SEARCH_TORIHIKISAKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEARCH_TORIHIKISAKI_NAME.PopupSearchSendParams")));
            this.SEARCH_TORIHIKISAKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEARCH_TORIHIKISAKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEARCH_TORIHIKISAKI_NAME.popupWindowSetting")));
            this.SEARCH_TORIHIKISAKI_NAME.ReadOnly = true;
            this.SEARCH_TORIHIKISAKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEARCH_TORIHIKISAKI_NAME.RegistCheckMethod")));
            this.SEARCH_TORIHIKISAKI_NAME.Size = new System.Drawing.Size(170, 20);
            this.SEARCH_TORIHIKISAKI_NAME.TabIndex = 21;
            this.SEARCH_TORIHIKISAKI_NAME.TabStop = false;
            this.SEARCH_TORIHIKISAKI_NAME.Tag = "";
            // 
            // btnSearchTorihikisaki
            // 
            this.btnSearchTorihikisaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnSearchTorihikisaki.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnSearchTorihikisaki.DBFieldsName = null;
            this.btnSearchTorihikisaki.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnSearchTorihikisaki.DisplayItemName = null;
            this.btnSearchTorihikisaki.DisplayPopUp = null;
            this.btnSearchTorihikisaki.ErrorMessage = null;
            this.btnSearchTorihikisaki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnSearchTorihikisaki.FocusOutCheckMethod")));
            this.btnSearchTorihikisaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btnSearchTorihikisaki.GetCodeMasterField = null;
            this.btnSearchTorihikisaki.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchTorihikisaki.Image")));
            this.btnSearchTorihikisaki.ItemDefinedTypes = null;
            this.btnSearchTorihikisaki.LinkedSettingTextBox = null;
            this.btnSearchTorihikisaki.LinkedTextBoxs = null;
            this.btnSearchTorihikisaki.Location = new System.Drawing.Point(345, 68);
            this.btnSearchTorihikisaki.Name = "btnSearchTorihikisaki";
            this.btnSearchTorihikisaki.PopupAfterExecute = null;
            this.btnSearchTorihikisaki.PopupAfterExecuteMethod = "btnSearchTorihikisaki_PopupAfterExcuteMethod";
            this.btnSearchTorihikisaki.PopupBeforeExecute = null;
            this.btnSearchTorihikisaki.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.btnSearchTorihikisaki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnSearchTorihikisaki.PopupSearchSendParams")));
            this.btnSearchTorihikisaki.PopupSendParams = new string[0];
            this.btnSearchTorihikisaki.PopupSetFormField = "SEARCH_TORIHIKISAKI_CD,SEARCH_TORIHIKISAKI_NAME";
            this.btnSearchTorihikisaki.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.btnSearchTorihikisaki.PopupWindowName = "検索共通ポップアップ";
            this.btnSearchTorihikisaki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnSearchTorihikisaki.popupWindowSetting")));
            this.btnSearchTorihikisaki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnSearchTorihikisaki.RegistCheckMethod")));
            this.btnSearchTorihikisaki.SearchDisplayFlag = 0;
            this.btnSearchTorihikisaki.SetFormField = "SEARCH_TORIHIKISAKI_CD,SEARCH_TORIHIKISAKI_NAME";
            this.btnSearchTorihikisaki.ShortItemName = null;
            this.btnSearchTorihikisaki.Size = new System.Drawing.Size(22, 22);
            this.btnSearchTorihikisaki.TabIndex = 31;
            this.btnSearchTorihikisaki.TabStop = false;
            this.btnSearchTorihikisaki.UseVisualStyleBackColor = false;
            this.btnSearchTorihikisaki.ZeroPaddengFlag = false;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.SystemColors.Window;
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayItemName = "";
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.GetCodeMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(384, 70);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 2;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupGetMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupSetFormField = "";
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.PopupWindowName = "";
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.SetFormField = "";
            this.ISNOT_NEED_DELETE_FLG.ShortItemName = "";
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(59, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 32;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "True";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            this.ISNOT_NEED_DELETE_FLG.ZeroPaddengFlag = true;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 475);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.btnSearchTorihikisaki);
            this.Controls.Add(this.SEARCH_TORIHIKISAKI_CD);
            this.Controls.Add(this.SEARCH_TORIHIKISAKI_NAME);
            this.Controls.Add(this.DETAIL);
            this.Controls.Add(this.lblTorihikisakiCd);
            this.Controls.Add(this.SHIME_STATUS);
            this.Controls.Add(this.lblShimeStatus);
            this.Controls.Add(this.GETSUJI_DATE);
            this.Controls.Add(this.lblDate);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.DETAIL)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox SHIME_STATUS;
        internal System.Windows.Forms.Label lblShimeStatus;
        internal r_framework.CustomControl.CustomDateTimePicker GETSUJI_DATE;
        internal System.Windows.Forms.Label lblDate;
        internal System.Windows.Forms.Label lblTorihikisakiCd;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SEARCH_TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomTextBox SEARCH_TORIHIKISAKI_NAME;
        internal r_framework.CustomControl.CustomDataGridView DETAIL;
        internal r_framework.CustomControl.CustomPopupOpenButton btnSearchTorihikisaki;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn TORIHIKISAKI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TORIHIKISAKI_NAME_RYAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column PREVIOUS_MONTH_BALANCE;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column NYUUSHUKKIN_KINGAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column KINGAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column TAX;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column TOTAL_KINGAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column LOCK_ZANDAKA;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column ADJUST_TAX;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column ZANDAKA;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column YEAR;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column MONTH;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column SEQ;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column UPDATE_SEQ;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}