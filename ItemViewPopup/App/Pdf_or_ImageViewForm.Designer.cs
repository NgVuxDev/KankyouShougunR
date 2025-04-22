namespace ItemViewPopup.App
{
    partial class Pdf_or_ImageViewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Pdf_or_ImageViewForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.pctBox = new System.Windows.Forms.PictureBox();
            this.txtFilePath = new r_framework.CustomControl.CustomTextBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.bt_Close = new r_framework.CustomControl.CustomButton();
            this.axAcroPDF = new AxAcroPDFLib.AxAcroPDF();
            this.bt_SetScope = new r_framework.CustomControl.CustomButton();
            this.bt_FirstPage = new r_framework.CustomControl.CustomButton();
            this.bt_PreviousPage = new r_framework.CustomControl.CustomButton();
            this.bt_NextPage = new r_framework.CustomControl.CustomButton();
            this.bt_LastPage = new r_framework.CustomControl.CustomButton();
            this.txtScopeRate = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lblScopeRatePercent = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pctBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF)).BeginInit();
            this.SuspendLayout();
            // 
            // pctBox
            // 
            this.pctBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pctBox.Location = new System.Drawing.Point(12, 27);
            this.pctBox.Name = "pctBox";
            this.pctBox.Size = new System.Drawing.Size(760, 491);
            this.pctBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pctBox.TabIndex = 7;
            this.pctBox.TabStop = false;
            // 
            // txtFilePath
            // 
            this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtFilePath.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtFilePath.DisplayPopUp = null;
            this.txtFilePath.FocusOutCheckMethod = null;
            this.txtFilePath.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtFilePath.ForeColor = System.Drawing.Color.Black;
            this.txtFilePath.IsInputErrorOccured = false;
            this.txtFilePath.Location = new System.Drawing.Point(126, 4);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtFilePath.PopupSearchSendParams")));
            this.txtFilePath.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.txtFilePath.popupWindowSetting = null;
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.RegistCheckMethod = null;
            this.txtFilePath.Size = new System.Drawing.Size(646, 20);
            this.txtFilePath.TabIndex = 2;
            this.txtFilePath.Tag = "表示しているデータのファイルパス";
            // 
            // lblFilePath
            // 
            this.lblFilePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFilePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFilePath.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFilePath.ForeColor = System.Drawing.Color.White;
            this.lblFilePath.Location = new System.Drawing.Point(12, 2);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(108, 22);
            this.lblFilePath.TabIndex = 0;
            this.lblFilePath.Text = "ファイルパス";
            this.lblFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_Close
            // 
            this.bt_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bt_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_Close.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_Close.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_Close.Location = new System.Drawing.Point(692, 524);
            this.bt_Close.Name = "bt_Close";
            this.bt_Close.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_Close.Size = new System.Drawing.Size(80, 35);
            this.bt_Close.TabIndex = 22;
            this.bt_Close.TabStop = false;
            this.bt_Close.Tag = "";
            this.bt_Close.Text = "[F12]　　閉じる";
            this.bt_Close.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Close.UseVisualStyleBackColor = false;
            this.bt_Close.Click += new System.EventHandler(this.bt_Close_Click);
            // 
            // axAcroPDF
            // 
            this.axAcroPDF.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.axAcroPDF.Enabled = true;
            this.axAcroPDF.Location = new System.Drawing.Point(12, 27);
            this.axAcroPDF.Name = "axAcroPDF";
            this.axAcroPDF.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axAcroPDF.OcxState")));
            this.axAcroPDF.Size = new System.Drawing.Size(760, 491);
            this.axAcroPDF.TabIndex = 10;
            this.axAcroPDF.TabStop = false;
            // 
            // bt_SetScope
            // 
            this.bt_SetScope.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_SetScope.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_SetScope.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_SetScope.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_SetScope.Location = new System.Drawing.Point(59, 524);
            this.bt_SetScope.Name = "bt_SetScope";
            this.bt_SetScope.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_SetScope.Size = new System.Drawing.Size(80, 35);
            this.bt_SetScope.TabIndex = 11;
            this.bt_SetScope.TabStop = false;
            this.bt_SetScope.Tag = "";
            this.bt_SetScope.Text = "[F1]　　倍率";
            this.bt_SetScope.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_SetScope.UseVisualStyleBackColor = false;
            this.bt_SetScope.Click += new System.EventHandler(this.bt_SetScope_Click);
            // 
            // bt_FirstPage
            // 
            this.bt_FirstPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_FirstPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_FirstPage.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_FirstPage.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_FirstPage.Location = new System.Drawing.Point(145, 524);
            this.bt_FirstPage.Name = "bt_FirstPage";
            this.bt_FirstPage.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_FirstPage.Size = new System.Drawing.Size(80, 35);
            this.bt_FirstPage.TabIndex = 15;
            this.bt_FirstPage.TabStop = false;
            this.bt_FirstPage.Tag = "";
            this.bt_FirstPage.Text = "[F5]　　先頭頁";
            this.bt_FirstPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_FirstPage.UseVisualStyleBackColor = false;
            this.bt_FirstPage.Click += new System.EventHandler(this.bt_FirstPage_Click);
            // 
            // bt_PreviousPage
            // 
            this.bt_PreviousPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_PreviousPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_PreviousPage.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_PreviousPage.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_PreviousPage.Location = new System.Drawing.Point(231, 524);
            this.bt_PreviousPage.Name = "bt_PreviousPage";
            this.bt_PreviousPage.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_PreviousPage.Size = new System.Drawing.Size(80, 35);
            this.bt_PreviousPage.TabIndex = 16;
            this.bt_PreviousPage.TabStop = false;
            this.bt_PreviousPage.Tag = "";
            this.bt_PreviousPage.Text = "[F6]　　前頁";
            this.bt_PreviousPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_PreviousPage.UseVisualStyleBackColor = false;
            this.bt_PreviousPage.Click += new System.EventHandler(this.bt_PreviousPage_Click);
            // 
            // bt_NextPage
            // 
            this.bt_NextPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_NextPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_NextPage.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_NextPage.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_NextPage.Location = new System.Drawing.Point(317, 524);
            this.bt_NextPage.Name = "bt_NextPage";
            this.bt_NextPage.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_NextPage.Size = new System.Drawing.Size(80, 35);
            this.bt_NextPage.TabIndex = 17;
            this.bt_NextPage.TabStop = false;
            this.bt_NextPage.Tag = "";
            this.bt_NextPage.Text = "[F7]　　次頁";
            this.bt_NextPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_NextPage.UseVisualStyleBackColor = false;
            this.bt_NextPage.Click += new System.EventHandler(this.bt_NextPage_Click);
            // 
            // bt_LastPage
            // 
            this.bt_LastPage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_LastPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_LastPage.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_LastPage.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_LastPage.Location = new System.Drawing.Point(403, 524);
            this.bt_LastPage.Name = "bt_LastPage";
            this.bt_LastPage.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_LastPage.Size = new System.Drawing.Size(80, 35);
            this.bt_LastPage.TabIndex = 18;
            this.bt_LastPage.TabStop = false;
            this.bt_LastPage.Tag = "";
            this.bt_LastPage.Text = "[F8]　　最終頁";
            this.bt_LastPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_LastPage.UseVisualStyleBackColor = false;
            this.bt_LastPage.Click += new System.EventHandler(this.bt_LastPage_Click);
            // 
            // txtScopeRate
            // 
            this.txtScopeRate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtScopeRate.AutoChangeBackColorEnabled = false;
            this.txtScopeRate.BackColor = System.Drawing.SystemColors.Window;
            this.txtScopeRate.CharacterLimitList = new char[0];
            this.txtScopeRate.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtScopeRate.DisplayPopUp = null;
            this.txtScopeRate.FocusOutCheckMethod = null;
            this.txtScopeRate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtScopeRate.ForeColor = System.Drawing.Color.Black;
            this.txtScopeRate.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtScopeRate.IsInputErrorOccured = false;
            this.txtScopeRate.LinkedRadioButtonArray = new string[0];
            this.txtScopeRate.Location = new System.Drawing.Point(12, 524);
            this.txtScopeRate.Name = "txtScopeRate";
            this.txtScopeRate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtScopeRate.PopupSearchSendParams")));
            this.txtScopeRate.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.txtScopeRate.popupWindowSetting = null;
            this.txtScopeRate.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            400,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtScopeRate.RangeSetting = rangeSettingDto1;
            this.txtScopeRate.RegistCheckMethod = null;
            this.txtScopeRate.Size = new System.Drawing.Size(36, 20);
            this.txtScopeRate.TabIndex = 1;
            this.txtScopeRate.Tag = "PDFの表示倍率";
            this.txtScopeRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblScopeRatePercent
            // 
            this.lblScopeRatePercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblScopeRatePercent.AutoSize = true;
            this.lblScopeRatePercent.Location = new System.Drawing.Point(46, 528);
            this.lblScopeRatePercent.Name = "lblScopeRatePercent";
            this.lblScopeRatePercent.Size = new System.Drawing.Size(11, 12);
            this.lblScopeRatePercent.TabIndex = 18;
            this.lblScopeRatePercent.Text = "%";
            // 
            // Pdf_or_ImageViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.lblScopeRatePercent);
            this.Controls.Add(this.txtScopeRate);
            this.Controls.Add(this.bt_LastPage);
            this.Controls.Add(this.bt_NextPage);
            this.Controls.Add(this.bt_PreviousPage);
            this.Controls.Add(this.bt_FirstPage);
            this.Controls.Add(this.bt_SetScope);
            this.Controls.Add(this.bt_Close);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.pctBox);
            this.Controls.Add(this.axAcroPDF);
            this.Name = "Pdf_or_ImageViewForm";
            this.Text = "Pdf_or_ImageViewForm";
            this.Load += new System.EventHandler(this.Pdf_or_ImageViewForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pctBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axAcroPDF)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pctBox;
        internal r_framework.CustomControl.CustomTextBox txtFilePath;
        internal System.Windows.Forms.Label lblFilePath;
        public r_framework.CustomControl.CustomButton bt_Close;
        private AxAcroPDFLib.AxAcroPDF axAcroPDF;
        public r_framework.CustomControl.CustomButton bt_SetScope;
        public r_framework.CustomControl.CustomButton bt_FirstPage;
        public r_framework.CustomControl.CustomButton bt_PreviousPage;
        public r_framework.CustomControl.CustomButton bt_NextPage;
        public r_framework.CustomControl.CustomButton bt_LastPage;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtScopeRate;
        private System.Windows.Forms.Label lblScopeRatePercent;
    }
}