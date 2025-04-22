namespace Shougun.Core.Common
{
    partial class SaveLogFilePopup
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SaveLogFilePopup));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.Selected = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.CreateDate = new r_framework.CustomControl.DgvCustomDataTimeColumn();
            this.FileName = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SaveFileButton = new System.Windows.Forms.Button();
            this.CloseButton = new System.Windows.Forms.Button();
            this.IdLabel = new System.Windows.Forms.Label();
            this.lb_title = new System.Windows.Forms.Label();
            this.AllCancelButton = new System.Windows.Forms.Button();
            this.lb_hint = new System.Windows.Forms.Label();
            this.SelectedNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.LogFileNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lbl_読込データ件数 = new System.Windows.Forms.Label();
            this.lbl_アラート件数 = new System.Windows.Forms.Label();
            this.DisplayNumber = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.PreviewButton = new System.Windows.Forms.Button();
            this.ForwardButton = new System.Windows.Forms.Button();
            this.UpdateButton = new System.Windows.Forms.Button();
            this.AllCheckButton = new System.Windows.Forms.Button();
            this.StartDate = new r_framework.CustomControl.CustomDateTimePicker();
            this.customTextBox1 = new r_framework.CustomControl.CustomTextBox();
            this.EndDate = new r_framework.CustomControl.CustomDateTimePicker();
            this.AllCheckBox = new r_framework.CustomControl.CustomCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Selected,
            this.CreateDate,
            this.FileName});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.customDataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.customDataGridView1.EnableHeadersVisualStyles = false;
            this.customDataGridView1.GridColor = System.Drawing.Color.White;
            this.customDataGridView1.IsReload = false;
            this.customDataGridView1.LinkedDataPanelName = null;
            this.customDataGridView1.Location = new System.Drawing.Point(12, 129);
            this.customDataGridView1.MultiSelect = false;
            this.customDataGridView1.Name = "customDataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 21;
            this.customDataGridView1.ShowCellToolTips = false;
            this.customDataGridView1.Size = new System.Drawing.Size(478, 233);
            this.customDataGridView1.TabIndex = 3;
            // 
            // Selected
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.Selected.DefaultCellStyle = dataGridViewCellStyle2;
            this.Selected.FocusOutCheckMethod = null;
            this.Selected.HeaderText = "";
            this.Selected.Name = "Selected";
            this.Selected.RegistCheckMethod = null;
            this.Selected.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Selected.Width = 22;
            // 
            // CreateDate
            // 
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.CreateDate.DefaultCellStyle = dataGridViewCellStyle3;
            this.CreateDate.FocusOutCheckMethod = null;
            this.CreateDate.HeaderText = "作成日付";
            this.CreateDate.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateDate.popupWindowSetting = null;
            this.CreateDate.ReadOnly = true;
            this.CreateDate.RegistCheckMethod = null;
            this.CreateDate.Width = 160;
            // 
            // FileName
            // 
            this.FileName.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.FileName.DefaultCellStyle = dataGridViewCellStyle4;
            this.FileName.FocusOutCheckMethod = null;
            this.FileName.HeaderText = "ファイル名";
            this.FileName.Name = "FileName";
            this.FileName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FileName.PopupSearchSendParams")));
            this.FileName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FileName.popupWindowSetting = null;
            this.FileName.ReadOnly = true;
            this.FileName.RegistCheckMethod = null;
            this.FileName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.FileName.Width = 294;
            // 
            // SaveFileButton
            // 
            this.SaveFileButton.Font = new System.Drawing.Font("MS Gothic", 9F);
            this.SaveFileButton.Location = new System.Drawing.Point(496, 315);
            this.SaveFileButton.Name = "SaveFileButton";
            this.SaveFileButton.Size = new System.Drawing.Size(125, 35);
            this.SaveFileButton.TabIndex = 9;
            this.SaveFileButton.Tag = "選択しているログファイルをzip圧縮して保存します。";
            this.SaveFileButton.Text = "[F10]保存する";
            this.SaveFileButton.UseVisualStyleBackColor = true;
            this.SaveFileButton.Click += new System.EventHandler(this.SaveFileButton_Click);
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("MS Gothic", 9F);
            this.CloseButton.Location = new System.Drawing.Point(496, 356);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(125, 35);
            this.CloseButton.TabIndex = 10;
            this.CloseButton.Tag = "このウィンドウを閉じます。";
            this.CloseButton.Text = "[F12]閉じる";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
            // 
            // IdLabel
            // 
            this.IdLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.IdLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.IdLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.IdLabel.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.IdLabel.ForeColor = System.Drawing.Color.White;
            this.IdLabel.Location = new System.Drawing.Point(12, 62);
            this.IdLabel.Name = "IdLabel";
            this.IdLabel.Size = new System.Drawing.Size(119, 20);
            this.IdLabel.TabIndex = 3;
            this.IdLabel.Text = "表示期間";
            this.IdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("MS Gothic", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(12, 9);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(422, 39);
            this.lb_title.TabIndex = 4;
            this.lb_title.Text = "ログファイル保存ポップアップ";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AllCancelButton
            // 
            this.AllCancelButton.Font = new System.Drawing.Font("MS Gothic", 9F);
            this.AllCancelButton.Location = new System.Drawing.Point(496, 274);
            this.AllCancelButton.Name = "AllCancelButton";
            this.AllCancelButton.Size = new System.Drawing.Size(125, 35);
            this.AllCancelButton.TabIndex = 8;
            this.AllCancelButton.Tag = "表示されていないものを含めて、全てのページチェックボックスの選択を解除します。";
            this.AllCancelButton.Text = "[F2]全選択解除";
            this.AllCancelButton.UseVisualStyleBackColor = true;
            this.AllCancelButton.Click += new System.EventHandler(this.AllCancelButton_Click);
            // 
            // lb_hint
            // 
            this.lb_hint.BackColor = System.Drawing.Color.Black;
            this.lb_hint.Font = new System.Drawing.Font("Meiryo", 9.75F);
            this.lb_hint.ForeColor = System.Drawing.Color.Yellow;
            this.lb_hint.Location = new System.Drawing.Point(12, 365);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(478, 26);
            this.lb_hint.TabIndex = 6;
            this.lb_hint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SelectedNumber
            // 
            this.SelectedNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SelectedNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SelectedNumber.CustomFormatSetting = "#,##0";
            this.SelectedNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.SelectedNumber.DisplayPopUp = null;
            this.SelectedNumber.FocusOutCheckMethod = null;
            this.SelectedNumber.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SelectedNumber.ForeColor = System.Drawing.Color.Black;
            this.SelectedNumber.FormatSetting = "カスタム";
            this.SelectedNumber.IsInputErrorOccured = false;
            this.SelectedNumber.Location = new System.Drawing.Point(559, 9);
            this.SelectedNumber.Name = "SelectedNumber";
            this.SelectedNumber.PopupAfterExecute = null;
            this.SelectedNumber.PopupBeforeExecute = null;
            this.SelectedNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SelectedNumber.PopupSearchSendParams")));
            this.SelectedNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SelectedNumber.popupWindowSetting = null;
            this.SelectedNumber.RangeSetting = rangeSettingDto1;
            this.SelectedNumber.ReadOnly = true;
            this.SelectedNumber.RegistCheckMethod = null;
            this.SelectedNumber.Size = new System.Drawing.Size(62, 20);
            this.SelectedNumber.TabIndex = 385;
            this.SelectedNumber.TabStop = false;
            this.SelectedNumber.Tag = "選択されているログファイルの数です。";
            this.SelectedNumber.Text = "0";
            this.SelectedNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SelectedNumber.WordWrap = false;
            // 
            // LogFileNumber
            // 
            this.LogFileNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LogFileNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogFileNumber.CustomFormatSetting = "#,##0";
            this.LogFileNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.LogFileNumber.DisplayPopUp = null;
            this.LogFileNumber.FocusOutCheckMethod = null;
            this.LogFileNumber.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LogFileNumber.ForeColor = System.Drawing.Color.Black;
            this.LogFileNumber.FormatSetting = "カスタム";
            this.LogFileNumber.IsInputErrorOccured = false;
            this.LogFileNumber.ItemDefinedTypes = "float";
            this.LogFileNumber.Location = new System.Drawing.Point(559, 28);
            this.LogFileNumber.Name = "LogFileNumber";
            this.LogFileNumber.PopupAfterExecute = null;
            this.LogFileNumber.PopupBeforeExecute = null;
            this.LogFileNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LogFileNumber.PopupSearchSendParams")));
            this.LogFileNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LogFileNumber.popupWindowSetting = null;
            this.LogFileNumber.RangeSetting = rangeSettingDto2;
            this.LogFileNumber.ReadOnly = true;
            this.LogFileNumber.RegistCheckMethod = null;
            this.LogFileNumber.Size = new System.Drawing.Size(62, 20);
            this.LogFileNumber.TabIndex = 384;
            this.LogFileNumber.TabStop = false;
            this.LogFileNumber.Tag = "ログファイルの総数です。";
            this.LogFileNumber.Text = "0";
            this.LogFileNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.LogFileNumber.WordWrap = false;
            // 
            // lbl_読込データ件数
            // 
            this.lbl_読込データ件数.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_読込データ件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_読込データ件数.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_読込データ件数.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_読込データ件数.ForeColor = System.Drawing.Color.White;
            this.lbl_読込データ件数.Location = new System.Drawing.Point(440, 28);
            this.lbl_読込データ件数.Name = "lbl_読込データ件数";
            this.lbl_読込データ件数.Size = new System.Drawing.Size(119, 20);
            this.lbl_読込データ件数.TabIndex = 383;
            this.lbl_読込データ件数.Text = "ログファイル数";
            this.lbl_読込データ件数.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_アラート件数
            // 
            this.lbl_アラート件数.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_アラート件数.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_アラート件数.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_アラート件数.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_アラート件数.ForeColor = System.Drawing.Color.White;
            this.lbl_アラート件数.Location = new System.Drawing.Point(440, 9);
            this.lbl_アラート件数.Name = "lbl_アラート件数";
            this.lbl_アラート件数.Size = new System.Drawing.Size(119, 20);
            this.lbl_アラート件数.TabIndex = 382;
            this.lbl_アラート件数.Text = "選択数";
            this.lbl_アラート件数.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DisplayNumber
            // 
            this.DisplayNumber.BackColor = System.Drawing.SystemColors.Window;
            this.DisplayNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DisplayNumber.CustomFormatSetting = "#,##0";
            this.DisplayNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.DisplayNumber.DisplayPopUp = null;
            this.DisplayNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DisplayNumber.FocusOutCheckMethod")));
            this.DisplayNumber.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.DisplayNumber.ForeColor = System.Drawing.Color.Black;
            this.DisplayNumber.FormatSetting = "カスタム";
            this.DisplayNumber.IsInputErrorOccured = false;
            this.DisplayNumber.Location = new System.Drawing.Point(559, 62);
            this.DisplayNumber.Name = "DisplayNumber";
            this.DisplayNumber.PopupAfterExecute = null;
            this.DisplayNumber.PopupBeforeExecute = null;
            this.DisplayNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DisplayNumber.PopupSearchSendParams")));
            this.DisplayNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DisplayNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DisplayNumber.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.DisplayNumber.RangeSetting = rangeSettingDto3;
            this.DisplayNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DisplayNumber.RegistCheckMethod")));
            this.DisplayNumber.Size = new System.Drawing.Size(62, 20);
            this.DisplayNumber.TabIndex = 2;
            this.DisplayNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DisplayNumber.WordWrap = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(440, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 387;
            this.label1.Text = "表示日数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PreviewButton
            // 
            this.PreviewButton.Font = new System.Drawing.Font("MS Gothic", 9F);
            this.PreviewButton.Location = new System.Drawing.Point(12, 88);
            this.PreviewButton.Name = "PreviewButton";
            this.PreviewButton.Size = new System.Drawing.Size(125, 35);
            this.PreviewButton.TabIndex = 4;
            this.PreviewButton.Tag = "表示期間を表示日数分戻します。";
            this.PreviewButton.Text = "[F4]前へ";
            this.PreviewButton.UseVisualStyleBackColor = true;
            this.PreviewButton.Click += new System.EventHandler(this.PreviewButton_Click);
            // 
            // ForwardButton
            // 
            this.ForwardButton.Font = new System.Drawing.Font("MS Gothic", 9F);
            this.ForwardButton.Location = new System.Drawing.Point(365, 89);
            this.ForwardButton.Name = "ForwardButton";
            this.ForwardButton.Size = new System.Drawing.Size(125, 35);
            this.ForwardButton.TabIndex = 6;
            this.ForwardButton.Tag = "表示期間を表示日数分進めます。";
            this.ForwardButton.Text = "[F6]次へ";
            this.ForwardButton.UseVisualStyleBackColor = true;
            this.ForwardButton.Click += new System.EventHandler(this.ForwardButton_Click);
            // 
            // UpdateButton
            // 
            this.UpdateButton.Font = new System.Drawing.Font("MS Gothic", 9F);
            this.UpdateButton.Location = new System.Drawing.Point(189, 88);
            this.UpdateButton.Name = "UpdateButton";
            this.UpdateButton.Size = new System.Drawing.Size(125, 35);
            this.UpdateButton.TabIndex = 5;
            this.UpdateButton.Tag = "グリッドの表示を更新します。";
            this.UpdateButton.Text = "[F5]表示更新";
            this.UpdateButton.UseVisualStyleBackColor = true;
            this.UpdateButton.Click += new System.EventHandler(this.UpdateButton_Click);
            // 
            // AllCheckButton
            // 
            this.AllCheckButton.Font = new System.Drawing.Font("MS Gothic", 9F);
            this.AllCheckButton.Location = new System.Drawing.Point(496, 233);
            this.AllCheckButton.Name = "AllCheckButton";
            this.AllCheckButton.Size = new System.Drawing.Size(125, 35);
            this.AllCheckButton.TabIndex = 7;
            this.AllCheckButton.Tag = "表示されていないものを含めて、全てのチェックボックスを選択状態にします。";
            this.AllCheckButton.Text = "[F1]全選択";
            this.AllCheckButton.UseVisualStyleBackColor = true;
            this.AllCheckButton.Click += new System.EventHandler(this.AllCheckButton_Click);
            // 
            // StartDate
            // 
            this.StartDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.StartDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StartDate.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.StartDate.Checked = false;
            this.StartDate.DateTimeNowYear = "";
            this.StartDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.StartDate.DisplayPopUp = null;
            this.StartDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("StartDate.FocusOutCheckMethod")));
            this.StartDate.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.StartDate.ForeColor = System.Drawing.Color.Black;
            this.StartDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.StartDate.IsInputErrorOccured = false;
            this.StartDate.Location = new System.Drawing.Point(131, 62);
            this.StartDate.MaxLength = 10;
            this.StartDate.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.StartDate.Name = "StartDate";
            this.StartDate.NullValue = "";
            this.StartDate.PopupAfterExecute = null;
            this.StartDate.PopupBeforeExecute = null;
            this.StartDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("StartDate.PopupSearchSendParams")));
            this.StartDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.StartDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("StartDate.popupWindowSetting")));
            this.StartDate.ReadOnly = true;
            this.StartDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("StartDate.RegistCheckMethod")));
            this.StartDate.Size = new System.Drawing.Size(125, 20);
            this.StartDate.TabIndex = 393;
            this.StartDate.TabStop = false;
            this.StartDate.Tag = "表示期間の開始日付が自動で表示されます。";
            this.StartDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.StartDate.Value = null;
            // 
            // customTextBox1
            // 
            this.customTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.customTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customTextBox1.DisplayPopUp = null;
            this.customTextBox1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox1.FocusOutCheckMethod")));
            this.customTextBox1.Font = new System.Drawing.Font("MS Gothic", 9.5F);
            this.customTextBox1.ForeColor = System.Drawing.Color.Black;
            this.customTextBox1.IsInputErrorOccured = false;
            this.customTextBox1.Location = new System.Drawing.Point(249, 62);
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.PopupAfterExecute = null;
            this.customTextBox1.PopupBeforeExecute = null;
            this.customTextBox1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox1.PopupSearchSendParams")));
            this.customTextBox1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customTextBox1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox1.popupWindowSetting")));
            this.customTextBox1.ReadOnly = true;
            this.customTextBox1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox1.RegistCheckMethod")));
            this.customTextBox1.Size = new System.Drawing.Size(30, 20);
            this.customTextBox1.TabIndex = 395;
            this.customTextBox1.TabStop = false;
            this.customTextBox1.Text = "～";
            this.customTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // EndDate
            // 
            this.EndDate.BackColor = System.Drawing.SystemColors.Window;
            this.EndDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EndDate.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.EndDate.Checked = false;
            this.EndDate.DateTimeNowYear = "";
            this.EndDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.EndDate.DisplayPopUp = null;
            this.EndDate.FocusOutCheckMethod = null;
            this.EndDate.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.EndDate.ForeColor = System.Drawing.Color.Black;
            this.EndDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.EndDate.IsInputErrorOccured = false;
            this.EndDate.Location = new System.Drawing.Point(278, 62);
            this.EndDate.MaxLength = 10;
            this.EndDate.MaxValue = new System.DateTime(9999, 12, 31, 23, 59, 0, 0);
            this.EndDate.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.EndDate.Name = "EndDate";
            this.EndDate.NullValue = "";
            this.EndDate.PopupAfterExecute = null;
            this.EndDate.PopupBeforeExecute = null;
            this.EndDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EndDate.PopupSearchSendParams")));
            this.EndDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.EndDate.popupWindowSetting = null;
            this.EndDate.RegistCheckMethod = null;
            this.EndDate.Size = new System.Drawing.Size(125, 20);
            this.EndDate.TabIndex = 1;
            this.EndDate.Tag = "表示期間の終了日付を指定します。";
            this.EndDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.EndDate.Value = null;
            // 
            // AllCheckBox
            // 
            this.AllCheckBox.AutoSize = true;
            this.AllCheckBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.AllCheckBox.DefaultBackColor = System.Drawing.Color.Empty;
            this.AllCheckBox.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("AllCheckBox.FocusOutCheckMethod")));
            this.AllCheckBox.Location = new System.Drawing.Point(17, 132);
            this.AllCheckBox.Name = "AllCheckBox";
            this.AllCheckBox.PopupAfterExecute = null;
            this.AllCheckBox.PopupBeforeExecute = null;
            this.AllCheckBox.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("AllCheckBox.PopupSearchSendParams")));
            this.AllCheckBox.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.AllCheckBox.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("AllCheckBox.popupWindowSetting")));
            this.AllCheckBox.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("AllCheckBox.RegistCheckMethod")));
            this.AllCheckBox.Size = new System.Drawing.Size(15, 14);
            this.AllCheckBox.TabIndex = 396;
            this.AllCheckBox.TabStop = false;
            this.AllCheckBox.Tag = "表示されている全てのチェックボックスの選択状態を切り替えます。";
            this.AllCheckBox.UseVisualStyleBackColor = false;
            this.AllCheckBox.CheckedChanged += new System.EventHandler(this.customCheckBox1_CheckedChanged);
            // 
            // SaveLogFilePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(630, 400);
            this.Controls.Add(this.AllCheckBox);
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.EndDate);
            this.Controls.Add(this.StartDate);
            this.Controls.Add(this.AllCheckButton);
            this.Controls.Add(this.UpdateButton);
            this.Controls.Add(this.ForwardButton);
            this.Controls.Add(this.PreviewButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DisplayNumber);
            this.Controls.Add(this.SelectedNumber);
            this.Controls.Add(this.LogFileNumber);
            this.Controls.Add(this.lbl_読込データ件数);
            this.Controls.Add(this.lbl_アラート件数);
            this.Controls.Add(this.lb_hint);
            this.Controls.Add(this.AllCancelButton);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.IdLabel);
            this.Controls.Add(this.CloseButton);
            this.Controls.Add(this.SaveFileButton);
            this.Controls.Add(this.customDataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SaveLogFilePopup";
            this.Text = "ログファイル保存ポップアップ";
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomDataGridView customDataGridView1;
        internal System.Windows.Forms.Button SaveFileButton;
        internal System.Windows.Forms.Button CloseButton;
        internal System.Windows.Forms.Label IdLabel;
        internal System.Windows.Forms.Label lb_title;
        internal System.Windows.Forms.Button AllCancelButton;
        internal System.Windows.Forms.Label lb_hint;
        internal r_framework.CustomControl.CustomNumericTextBox2 SelectedNumber;
        internal r_framework.CustomControl.CustomNumericTextBox2 LogFileNumber;
        internal System.Windows.Forms.Label lbl_読込データ件数;
        internal System.Windows.Forms.Label lbl_アラート件数;
        internal r_framework.CustomControl.CustomNumericTextBox2 DisplayNumber;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Button PreviewButton;
        internal System.Windows.Forms.Button ForwardButton;
        internal System.Windows.Forms.Button UpdateButton;
        internal System.Windows.Forms.Button AllCheckButton;
        internal r_framework.CustomControl.CustomDateTimePicker StartDate;
        internal r_framework.CustomControl.CustomTextBox customTextBox1;
        internal r_framework.CustomControl.CustomDateTimePicker EndDate;
        internal r_framework.CustomControl.CustomCheckBox AllCheckBox;
        internal r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn Selected;
        internal r_framework.CustomControl.DgvCustomDataTimeColumn CreateDate;
        internal r_framework.CustomControl.DgvCustomTextBoxColumn FileName;
    }
}