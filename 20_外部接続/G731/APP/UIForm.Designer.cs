namespace Shougun.Core.ExternalConnection.FileUploadIchiran.APP
{
    partial class UIForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
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

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto6 = new r_framework.Dto.RangeSettingDto();
            this.FILE_NAME = new r_framework.CustomControl.CustomTextBox();
            this.HOJOIN_CD_LABEL = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.CREATE_DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.CREATE_DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.TEKIYOU_LABEL = new System.Windows.Forms.Label();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.customSearchHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSearchHeader();
            this.customSortHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSortHeader();
            this.dgvCustomDataTimeColumn1 = new r_framework.CustomControl.DgvCustomDataTimeColumn();
            this.dgvCustomTextBoxColumn1 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomNumericTextBox2Column1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.dgvCustomTextBoxColumn2 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn3 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomNumericTextBox2Column2 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.CREATE_DATE = new r_framework.CustomControl.DgvCustomDataTimeColumn();
            this.FILE_PATH = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.FILE_LENGTH = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.WINDOW_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.FILE_ID = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.dgvCustomNumericTextBox2Column3 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.dgvCustomNumericTextBox2Column4 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // FILE_NAME
            // 
            this.FILE_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.FILE_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FILE_NAME.CharactersNumber = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.FILE_NAME.DBFieldsName = "";
            this.FILE_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.FILE_NAME.DisplayPopUp = null;
            this.FILE_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILE_NAME.FocusOutCheckMethod")));
            this.FILE_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.FILE_NAME.ForeColor = System.Drawing.Color.Black;
            this.FILE_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.FILE_NAME.IsInputErrorOccured = false;
            this.FILE_NAME.ItemDefinedTypes = "varchar";
            this.FILE_NAME.Location = new System.Drawing.Point(126, 35);
            this.FILE_NAME.MaxLength = 0;
            this.FILE_NAME.Name = "FILE_NAME";
            this.FILE_NAME.PopupAfterExecute = null;
            this.FILE_NAME.PopupBeforeExecute = null;
            this.FILE_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FILE_NAME.PopupSearchSendParams")));
            this.FILE_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FILE_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FILE_NAME.popupWindowSetting")));
            this.FILE_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILE_NAME.RegistCheckMethod")));
            this.FILE_NAME.Size = new System.Drawing.Size(867, 20);
            this.FILE_NAME.TabIndex = 60;
            this.FILE_NAME.Tag = " ";
            // 
            // HOJOIN_CD_LABEL
            // 
            this.HOJOIN_CD_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.HOJOIN_CD_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOJOIN_CD_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HOJOIN_CD_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HOJOIN_CD_LABEL.ForeColor = System.Drawing.Color.White;
            this.HOJOIN_CD_LABEL.Location = new System.Drawing.Point(12, 35);
            this.HOJOIN_CD_LABEL.Name = "HOJOIN_CD_LABEL";
            this.HOJOIN_CD_LABEL.Size = new System.Drawing.Size(110, 20);
            this.HOJOIN_CD_LABEL.TabIndex = 50;
            this.HOJOIN_CD_LABEL.Text = "ファイル名";
            this.HOJOIN_CD_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.Location = new System.Drawing.Point(258, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 30;
            this.label3.Text = "～";
            // 
            // CREATE_DATE_TO
            // 
            this.CREATE_DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.CREATE_DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CREATE_DATE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.CREATE_DATE_TO.Checked = false;
            this.CREATE_DATE_TO.DateTimeNowYear = "";
            this.CREATE_DATE_TO.DBFieldsName = "";
            this.CREATE_DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_DATE_TO.DisplayItemName = "登録日付(終了日)";
            this.CREATE_DATE_TO.DisplayPopUp = null;
            this.CREATE_DATE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE_TO.FocusOutCheckMethod")));
            this.CREATE_DATE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CREATE_DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.CREATE_DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.CREATE_DATE_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CREATE_DATE_TO.IsInputErrorOccured = false;
            this.CREATE_DATE_TO.ItemDefinedTypes = "datetime";
            this.CREATE_DATE_TO.Location = new System.Drawing.Point(285, 12);
            this.CREATE_DATE_TO.MaxLength = 10;
            this.CREATE_DATE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.CREATE_DATE_TO.Name = "CREATE_DATE_TO";
            this.CREATE_DATE_TO.NullValue = "";
            this.CREATE_DATE_TO.PopupAfterExecute = null;
            this.CREATE_DATE_TO.PopupBeforeExecute = null;
            this.CREATE_DATE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE_TO.PopupSearchSendParams")));
            this.CREATE_DATE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE_TO.popupWindowSetting")));
            this.CREATE_DATE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE_TO.RegistCheckMethod")));
            this.CREATE_DATE_TO.ShortItemName = "登録日付(終了日)";
            this.CREATE_DATE_TO.Size = new System.Drawing.Size(124, 20);
            this.CREATE_DATE_TO.TabIndex = 40;
            this.CREATE_DATE_TO.Tag = "登録日付(終了日)を入力してください";
            this.CREATE_DATE_TO.Value = null;
            // 
            // CREATE_DATE_FROM
            // 
            this.CREATE_DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.CREATE_DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CREATE_DATE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.CREATE_DATE_FROM.Checked = false;
            this.CREATE_DATE_FROM.DateTimeNowYear = "";
            this.CREATE_DATE_FROM.DBFieldsName = "";
            this.CREATE_DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.CREATE_DATE_FROM.DisplayItemName = "登録日付(開始日)";
            this.CREATE_DATE_FROM.DisplayPopUp = null;
            this.CREATE_DATE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE_FROM.FocusOutCheckMethod")));
            this.CREATE_DATE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CREATE_DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.CREATE_DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.CREATE_DATE_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CREATE_DATE_FROM.IsInputErrorOccured = false;
            this.CREATE_DATE_FROM.ItemDefinedTypes = "datetime";
            this.CREATE_DATE_FROM.Location = new System.Drawing.Point(126, 12);
            this.CREATE_DATE_FROM.MaxLength = 10;
            this.CREATE_DATE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.CREATE_DATE_FROM.Name = "CREATE_DATE_FROM";
            this.CREATE_DATE_FROM.NullValue = "";
            this.CREATE_DATE_FROM.PopupAfterExecute = null;
            this.CREATE_DATE_FROM.PopupBeforeExecute = null;
            this.CREATE_DATE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE_FROM.PopupSearchSendParams")));
            this.CREATE_DATE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE_FROM.popupWindowSetting")));
            this.CREATE_DATE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE_FROM.RegistCheckMethod")));
            this.CREATE_DATE_FROM.ShortItemName = "登録日付(開始日)";
            this.CREATE_DATE_FROM.Size = new System.Drawing.Size(124, 20);
            this.CREATE_DATE_FROM.TabIndex = 20;
            this.CREATE_DATE_FROM.Tag = "登録日付(開始日)を入力してください";
            this.CREATE_DATE_FROM.Value = null;
            // 
            // TEKIYOU_LABEL
            // 
            this.TEKIYOU_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TEKIYOU_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEKIYOU_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TEKIYOU_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TEKIYOU_LABEL.ForeColor = System.Drawing.Color.White;
            this.TEKIYOU_LABEL.Location = new System.Drawing.Point(12, 12);
            this.TEKIYOU_LABEL.Name = "TEKIYOU_LABEL";
            this.TEKIYOU_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TEKIYOU_LABEL.TabIndex = 10;
            this.TEKIYOU_LABEL.Text = "登録日付";
            this.TEKIYOU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToAddRows = false;
            this.Ichiran.AllowUserToDeleteRows = false;
            this.Ichiran.AllowUserToResizeRows = false;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CREATE_DATE,
            this.FILE_PATH,
            this.FILE_LENGTH,
            this.WINDOW_NAME,
            this.CREATE_USER,
            this.FILE_ID});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle8;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = "customSortHeader1";
            this.Ichiran.Location = new System.Drawing.Point(13, 120);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(980, 338);
            this.Ichiran.TabIndex = 90;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customSearchHeader1.LinkedDataGridViewName = "Ichiran";
            this.customSearchHeader1.Location = new System.Drawing.Point(13, 69);
            this.customSearchHeader1.Name = "customSearchHeader1";
            this.customSearchHeader1.Size = new System.Drawing.Size(980, 26);
            this.customSearchHeader1.TabIndex = 70;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customSortHeader1.LinkedDataGridViewName = "Ichiran";
            this.customSortHeader1.Location = new System.Drawing.Point(13, 93);
            this.customSortHeader1.Name = "customSortHeader1";
            this.customSortHeader1.Size = new System.Drawing.Size(980, 26);
            this.customSortHeader1.SortFlag = false;
            this.customSortHeader1.TabIndex = 80;
            this.customSortHeader1.TabStop = false;
            // 
            // dgvCustomDataTimeColumn1
            // 
            this.dgvCustomDataTimeColumn1.DataPropertyName = "CREATE_DATE";
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomDataTimeColumn1.DefaultCellStyle = dataGridViewCellStyle10;
            this.dgvCustomDataTimeColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomDataTimeColumn1.FocusOutCheckMethod")));
            this.dgvCustomDataTimeColumn1.HeaderText = "登録日";
            this.dgvCustomDataTimeColumn1.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dgvCustomDataTimeColumn1.Name = "dgvCustomDataTimeColumn1";
            this.dgvCustomDataTimeColumn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomDataTimeColumn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomDataTimeColumn1.popupWindowSetting")));
            this.dgvCustomDataTimeColumn1.ReadOnly = true;
            this.dgvCustomDataTimeColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomDataTimeColumn1.RegistCheckMethod")));
            this.dgvCustomDataTimeColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomDataTimeColumn1.Width = 110;
            // 
            // dgvCustomTextBoxColumn1
            // 
            this.dgvCustomTextBoxColumn1.DataPropertyName = "FILE_PATH";
            this.dgvCustomTextBoxColumn1.DBFieldsName = "";
            this.dgvCustomTextBoxColumn1.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle11;
            this.dgvCustomTextBoxColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn1.HeaderText = "登録日";
            this.dgvCustomTextBoxColumn1.Name = "dgvCustomTextBoxColumn1";
            this.dgvCustomTextBoxColumn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn1.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn1.popupWindowSetting")));
            this.dgvCustomTextBoxColumn1.ReadOnly = true;
            this.dgvCustomTextBoxColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn1.Width = 450;
            // 
            // dgvCustomNumericTextBox2Column1
            // 
            this.dgvCustomNumericTextBox2Column1.DataPropertyName = "FILE_LENGTH";
            this.dgvCustomNumericTextBox2Column1.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomNumericTextBox2Column1.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvCustomNumericTextBox2Column1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomNumericTextBox2Column1.FocusOutCheckMethod")));
            this.dgvCustomNumericTextBox2Column1.HeaderText = "ファイルサイズ(M)";
            this.dgvCustomNumericTextBox2Column1.Name = "dgvCustomNumericTextBox2Column1";
            this.dgvCustomNumericTextBox2Column1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomNumericTextBox2Column1.PopupSearchSendParams")));
            this.dgvCustomNumericTextBox2Column1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomNumericTextBox2Column1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomNumericTextBox2Column1.popupWindowSetting")));
            this.dgvCustomNumericTextBox2Column1.RangeSetting = rangeSettingDto3;
            this.dgvCustomNumericTextBox2Column1.ReadOnly = true;
            this.dgvCustomNumericTextBox2Column1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomNumericTextBox2Column1.RegistCheckMethod")));
            this.dgvCustomNumericTextBox2Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomNumericTextBox2Column1.Width = 150;
            // 
            // dgvCustomTextBoxColumn2
            // 
            this.dgvCustomTextBoxColumn2.DataPropertyName = "WINDOW_NAME";
            this.dgvCustomTextBoxColumn2.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle13;
            this.dgvCustomTextBoxColumn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn2.HeaderText = "ファイルパス/ファイル名";
            this.dgvCustomTextBoxColumn2.Name = "dgvCustomTextBoxColumn2";
            this.dgvCustomTextBoxColumn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn2.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn2.popupWindowSetting")));
            this.dgvCustomTextBoxColumn2.ReadOnly = true;
            this.dgvCustomTextBoxColumn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn2.Width = 450;
            // 
            // dgvCustomTextBoxColumn3
            // 
            this.dgvCustomTextBoxColumn3.DataPropertyName = "CREATE_USER";
            this.dgvCustomTextBoxColumn3.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle14;
            this.dgvCustomTextBoxColumn3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn3.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn3.HeaderText = "登録者";
            this.dgvCustomTextBoxColumn3.Name = "dgvCustomTextBoxColumn3";
            this.dgvCustomTextBoxColumn3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn3.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn3.popupWindowSetting")));
            this.dgvCustomTextBoxColumn3.ReadOnly = true;
            this.dgvCustomTextBoxColumn3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn3.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomNumericTextBox2Column2
            // 
            this.dgvCustomNumericTextBox2Column2.DataPropertyName = "FILE_ID";
            this.dgvCustomNumericTextBox2Column2.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomNumericTextBox2Column2.DefaultCellStyle = dataGridViewCellStyle15;
            this.dgvCustomNumericTextBox2Column2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomNumericTextBox2Column2.FocusOutCheckMethod")));
            this.dgvCustomNumericTextBox2Column2.HeaderText = "登録画面";
            this.dgvCustomNumericTextBox2Column2.Name = "dgvCustomNumericTextBox2Column2";
            this.dgvCustomNumericTextBox2Column2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomNumericTextBox2Column2.PopupSearchSendParams")));
            this.dgvCustomNumericTextBox2Column2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomNumericTextBox2Column2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomNumericTextBox2Column2.popupWindowSetting")));
            this.dgvCustomNumericTextBox2Column2.RangeSetting = rangeSettingDto4;
            this.dgvCustomNumericTextBox2Column2.ReadOnly = true;
            this.dgvCustomNumericTextBox2Column2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomNumericTextBox2Column2.RegistCheckMethod")));
            this.dgvCustomNumericTextBox2Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomNumericTextBox2Column2.Visible = false;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_DATE.DefaultCellStyle = dataGridViewCellStyle2;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.HeaderText = "登録日";
            this.CREATE_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_DATE.Width = 110;
            // 
            // FILE_PATH
            // 
            this.FILE_PATH.DataPropertyName = "FILE_PATH";
            this.FILE_PATH.DBFieldsName = "";
            this.FILE_PATH.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.FILE_PATH.DefaultCellStyle = dataGridViewCellStyle3;
            this.FILE_PATH.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILE_PATH.FocusOutCheckMethod")));
            this.FILE_PATH.HeaderText = "ファイルパス/ファイル名";
            this.FILE_PATH.Name = "FILE_PATH";
            this.FILE_PATH.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FILE_PATH.PopupSearchSendParams")));
            this.FILE_PATH.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FILE_PATH.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FILE_PATH.popupWindowSetting")));
            this.FILE_PATH.ReadOnly = true;
            this.FILE_PATH.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILE_PATH.RegistCheckMethod")));
            this.FILE_PATH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FILE_PATH.Width = 450;
            // 
            // FILE_LENGTH
            // 
            this.FILE_LENGTH.DataPropertyName = "FILE_LENGTH";
            this.FILE_LENGTH.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.FILE_LENGTH.DefaultCellStyle = dataGridViewCellStyle4;
            this.FILE_LENGTH.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILE_LENGTH.FocusOutCheckMethod")));
            this.FILE_LENGTH.HeaderText = "ファイルサイズ(M)";
            this.FILE_LENGTH.Name = "FILE_LENGTH";
            this.FILE_LENGTH.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FILE_LENGTH.PopupSearchSendParams")));
            this.FILE_LENGTH.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FILE_LENGTH.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FILE_LENGTH.popupWindowSetting")));
            this.FILE_LENGTH.RangeSetting = rangeSettingDto1;
            this.FILE_LENGTH.ReadOnly = true;
            this.FILE_LENGTH.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILE_LENGTH.RegistCheckMethod")));
            this.FILE_LENGTH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FILE_LENGTH.Width = 135;
            // 
            // WINDOW_NAME
            // 
            this.WINDOW_NAME.DataPropertyName = "WINDOW_NAME";
            this.WINDOW_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.WINDOW_NAME.DefaultCellStyle = dataGridViewCellStyle5;
            this.WINDOW_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("WINDOW_NAME.FocusOutCheckMethod")));
            this.WINDOW_NAME.HeaderText = "登録画面";
            this.WINDOW_NAME.Name = "WINDOW_NAME";
            this.WINDOW_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("WINDOW_NAME.PopupSearchSendParams")));
            this.WINDOW_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.WINDOW_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("WINDOW_NAME.popupWindowSetting")));
            this.WINDOW_NAME.ReadOnly = true;
            this.WINDOW_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("WINDOW_NAME.RegistCheckMethod")));
            this.WINDOW_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.WINDOW_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.WINDOW_NAME.Width = 150;
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.DataPropertyName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_USER.DefaultCellStyle = dataGridViewCellStyle6;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.HeaderText = "登録者";
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // FILE_ID
            // 
            this.FILE_ID.DataPropertyName = "FILE_ID";
            this.FILE_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.FILE_ID.DefaultCellStyle = dataGridViewCellStyle7;
            this.FILE_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILE_ID.FocusOutCheckMethod")));
            this.FILE_ID.HeaderText = "ファイルID";
            this.FILE_ID.Name = "FILE_ID";
            this.FILE_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FILE_ID.PopupSearchSendParams")));
            this.FILE_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FILE_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FILE_ID.popupWindowSetting")));
            this.FILE_ID.RangeSetting = rangeSettingDto2;
            this.FILE_ID.ReadOnly = true;
            this.FILE_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILE_ID.RegistCheckMethod")));
            this.FILE_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FILE_ID.Visible = false;
            // 
            // dgvCustomNumericTextBox2Column3
            // 
            this.dgvCustomNumericTextBox2Column3.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomNumericTextBox2Column3.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvCustomNumericTextBox2Column3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomNumericTextBox2Column3.FocusOutCheckMethod")));
            this.dgvCustomNumericTextBox2Column3.HeaderText = "登録者";
            this.dgvCustomNumericTextBox2Column3.Name = "dgvCustomNumericTextBox2Column3";
            this.dgvCustomNumericTextBox2Column3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomNumericTextBox2Column3.PopupSearchSendParams")));
            this.dgvCustomNumericTextBox2Column3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomNumericTextBox2Column3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomNumericTextBox2Column3.popupWindowSetting")));
            this.dgvCustomNumericTextBox2Column3.RangeSetting = rangeSettingDto5;
            this.dgvCustomNumericTextBox2Column3.ReadOnly = true;
            this.dgvCustomNumericTextBox2Column3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomNumericTextBox2Column3.RegistCheckMethod")));
            this.dgvCustomNumericTextBox2Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomNumericTextBox2Column4
            // 
            this.dgvCustomNumericTextBox2Column4.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomNumericTextBox2Column4.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvCustomNumericTextBox2Column4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomNumericTextBox2Column4.FocusOutCheckMethod")));
            this.dgvCustomNumericTextBox2Column4.HeaderText = "ファイルID";
            this.dgvCustomNumericTextBox2Column4.Name = "dgvCustomNumericTextBox2Column4";
            this.dgvCustomNumericTextBox2Column4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomNumericTextBox2Column4.PopupSearchSendParams")));
            this.dgvCustomNumericTextBox2Column4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomNumericTextBox2Column4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomNumericTextBox2Column4.popupWindowSetting")));
            this.dgvCustomNumericTextBox2Column4.RangeSetting = rangeSettingDto6;
            this.dgvCustomNumericTextBox2Column4.ReadOnly = true;
            this.dgvCustomNumericTextBox2Column4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomNumericTextBox2Column4.RegistCheckMethod")));
            this.dgvCustomNumericTextBox2Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomNumericTextBox2Column4.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 471);
            this.Controls.Add(this.customSearchHeader1);
            this.Controls.Add(this.customSortHeader1);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CREATE_DATE_TO);
            this.Controls.Add(this.CREATE_DATE_FROM);
            this.Controls.Add(this.TEKIYOU_LABEL);
            this.Controls.Add(this.FILE_NAME);
            this.Controls.Add(this.HOJOIN_CD_LABEL);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox FILE_NAME;
        internal System.Windows.Forms.Label HOJOIN_CD_LABEL;
        private System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomDateTimePicker CREATE_DATE_TO;
        internal r_framework.CustomControl.CustomDateTimePicker CREATE_DATE_FROM;
        internal System.Windows.Forms.Label TEKIYOU_LABEL;
        public r_framework.CustomControl.DataGridCustomControl.CustomSortHeader customSortHeader1;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn1;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn2;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgvCustomNumericTextBox2Column1;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgvCustomNumericTextBox2Column2;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgvCustomNumericTextBox2Column3;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column dgvCustomNumericTextBox2Column4;
        public r_framework.CustomControl.DataGridCustomControl.CustomSearchHeader customSearchHeader1;
        public r_framework.CustomControl.CustomDataGridView Ichiran;
        private r_framework.CustomControl.DgvCustomDataTimeColumn CREATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn FILE_PATH;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column FILE_LENGTH;
        private r_framework.CustomControl.DgvCustomTextBoxColumn WINDOW_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_USER;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column FILE_ID;
        private r_framework.CustomControl.DgvCustomDataTimeColumn dgvCustomDataTimeColumn1;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn3;
    }
}