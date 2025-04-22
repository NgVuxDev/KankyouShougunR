namespace Shougun.Printing.Manager
{
    partial class MonitorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MonitorForm));
            this.filesysWatcher = new System.IO.FileSystemWatcher();
            this.ReportInfoListBox = new System.Windows.Forms.ListBox();
            this.previewButton = new System.Windows.Forms.Button();
            this.printButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.descriptionTextBox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.findTimer = new System.Windows.Forms.Timer(this.components);
            this.bitmapMaximizeButton = new System.Windows.Forms.Button();
            this.bitmapMinimizeButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.printStopButton = new System.Windows.Forms.Button();
            this.printAllButton = new System.Windows.Forms.Button();
            this.previewAllCloseButton = new System.Windows.Forms.Button();
            this.f8Button = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.StatusLabel = new System.Windows.Forms.Label();
            this.updateTimer = new System.Windows.Forms.Timer(this.components);
            this.mapWatcher = new System.IO.FileSystemWatcher();
            this.findMapTimer = new System.Windows.Forms.Timer(this.components);
            this.findPreviewTimer = new System.Windows.Forms.Timer(this.components);
            this.filesysPreviewWatcher = new System.IO.FileSystemWatcher();
            ((System.ComponentModel.ISupportInitialize)(this.filesysWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesysPreviewWatcher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapWatcher)).BeginInit();
            this.SuspendLayout();
            // 
            // filesysWatcher
            // 
            this.filesysWatcher.EnableRaisingEvents = true;
            this.filesysWatcher.SynchronizingObject = this;
            // 
            // ReportInfoListBox
            // 
            this.ReportInfoListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ReportInfoListBox.CausesValidation = false;
            this.ReportInfoListBox.IntegralHeight = false;
            this.ReportInfoListBox.ItemHeight = 12;
            this.ReportInfoListBox.Location = new System.Drawing.Point(531, 68);
            this.ReportInfoListBox.Name = "ReportInfoListBox";
            this.ReportInfoListBox.Size = new System.Drawing.Size(342, 182);
            this.ReportInfoListBox.TabIndex = 0;
            this.ReportInfoListBox.SelectedIndexChanged += new System.EventHandler(this.ReportInfoListBox_SelectedIndexChanged);
            this.ReportInfoListBox.DoubleClick += new System.EventHandler(this.ReportInfoListBox_DoubleClick);
            // 
            // previewButton
            // 
            this.previewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.previewButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.previewButton.Location = new System.Drawing.Point(233, 439);
            this.previewButton.Name = "previewButton";
            this.previewButton.Size = new System.Drawing.Size(86, 35);
            this.previewButton.TabIndex = 6;
            this.previewButton.TabStop = false;
            this.previewButton.Text = "[F5][Ent]\r\nプレビュー";
            this.previewButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.previewButton.UseVisualStyleBackColor = true;
            this.previewButton.Click += new System.EventHandler(this.previewButton_Click);
            // 
            // printButton
            // 
            this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.printButton.Location = new System.Drawing.Point(566, 439);
            this.printButton.Name = "printButton";
            this.printButton.Size = new System.Drawing.Size(75, 35);
            this.printButton.TabIndex = 7;
            this.printButton.TabStop = false;
            this.printButton.Text = "[F9]\r\n印刷";
            this.printButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.printButton.UseVisualStyleBackColor = true;
            this.printButton.Click += new System.EventHandler(this.printButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.deleteButton.Location = new System.Drawing.Point(398, 439);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 35);
            this.deleteButton.TabIndex = 8;
            this.deleteButton.TabStop = false;
            this.deleteButton.Text = "[F7]\r\n削除";
            this.deleteButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(9, 405);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(862, 23);
            this.label1.TabIndex = 9;
            this.label1.Text = "ダブルクリックまたはEnterキーでもプレビューが開きます。";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // descriptionTextBox
            // 
            this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.descriptionTextBox.BackColor = System.Drawing.Color.White;
            this.descriptionTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionTextBox.CausesValidation = false;
            this.descriptionTextBox.Location = new System.Drawing.Point(531, 256);
            this.descriptionTextBox.Multiline = true;
            this.descriptionTextBox.Name = "descriptionTextBox";
            this.descriptionTextBox.ReadOnly = true;
            this.descriptionTextBox.Size = new System.Drawing.Size(341, 144);
            this.descriptionTextBox.TabIndex = 10;
            this.descriptionTextBox.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(10, 68);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(516, 332);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // findTimer
            // 
            this.findTimer.Tick += new System.EventHandler(this.onFindTimer);
            // 
            // bitmapMaximizeButton
            // 
            this.bitmapMaximizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bitmapMaximizeButton.Location = new System.Drawing.Point(506, 377);
            this.bitmapMaximizeButton.Name = "bitmapMaximizeButton";
            this.bitmapMaximizeButton.Size = new System.Drawing.Size(19, 21);
            this.bitmapMaximizeButton.TabIndex = 12;
            this.bitmapMaximizeButton.TabStop = false;
            this.bitmapMaximizeButton.Text = "+";
            this.bitmapMaximizeButton.UseVisualStyleBackColor = true;
            this.bitmapMaximizeButton.Click += new System.EventHandler(this.bitmapMaximizeButton_Click);
            // 
            // bitmapMinimizeButton
            // 
            this.bitmapMinimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bitmapMinimizeButton.Location = new System.Drawing.Point(488, 377);
            this.bitmapMinimizeButton.Name = "bitmapMinimizeButton";
            this.bitmapMinimizeButton.Size = new System.Drawing.Size(19, 21);
            this.bitmapMinimizeButton.TabIndex = 13;
            this.bitmapMinimizeButton.TabStop = false;
            this.bitmapMinimizeButton.Text = "-";
            this.bitmapMinimizeButton.UseVisualStyleBackColor = true;
            this.bitmapMinimizeButton.Click += new System.EventHandler(this.bitmapMinimizeButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.button1.Location = new System.Drawing.Point(797, 439);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 35);
            this.button1.TabIndex = 14;
            this.button1.TabStop = false;
            this.button1.Text = "[F12]\r\n閉じる";
            this.button1.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // printStopButton
            // 
            this.printStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printStopButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.printStopButton.Location = new System.Drawing.Point(720, 439);
            this.printStopButton.Name = "printStopButton";
            this.printStopButton.Size = new System.Drawing.Size(75, 35);
            this.printStopButton.TabIndex = 15;
            this.printStopButton.TabStop = false;
            this.printStopButton.Text = "[F11]\r\n印刷停止";
            this.printStopButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.printStopButton.UseVisualStyleBackColor = true;
            this.printStopButton.Click += new System.EventHandler(this.printStopButton_Click);
            // 
            // printAllButton
            // 
            this.printAllButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.printAllButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.printAllButton.Location = new System.Drawing.Point(643, 439);
            this.printAllButton.Name = "printAllButton";
            this.printAllButton.Size = new System.Drawing.Size(75, 35);
            this.printAllButton.TabIndex = 16;
            this.printAllButton.TabStop = false;
            this.printAllButton.Text = "[F10]\r\n全て印刷";
            this.printAllButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.printAllButton.UseVisualStyleBackColor = true;
            this.printAllButton.Click += new System.EventHandler(this.printAllButton_Click);
            // 
            // previewAllCloseButton
            // 
            this.previewAllCloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.previewAllCloseButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.previewAllCloseButton.Location = new System.Drawing.Point(321, 439);
            this.previewAllCloseButton.Name = "previewAllCloseButton";
            this.previewAllCloseButton.Size = new System.Drawing.Size(75, 35);
            this.previewAllCloseButton.TabIndex = 17;
            this.previewAllCloseButton.TabStop = false;
            this.previewAllCloseButton.Text = "[F6]\r\nプレ全閉";
            this.previewAllCloseButton.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.previewAllCloseButton.UseVisualStyleBackColor = true;
            this.previewAllCloseButton.Click += new System.EventHandler(this.previewAllCloseButton_Click);
            // 
            // f8Button
            // 
            this.f8Button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.f8Button.Enabled = false;
            this.f8Button.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.f8Button.Location = new System.Drawing.Point(474, 439);
            this.f8Button.Name = "f8Button";
            this.f8Button.Size = new System.Drawing.Size(75, 35);
            this.f8Button.TabIndex = 18;
            this.f8Button.TabStop = false;
            this.f8Button.Text = "[F8]\r\n";
            this.f8Button.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.f8Button.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.AccessibleName = "";
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(531, 13);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(341, 21);
            this.progressBar1.TabIndex = 20;
            // 
            // StatusLabel
            // 
            this.StatusLabel.BackColor = System.Drawing.Color.White;
            this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.StatusLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StatusLabel.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.StatusLabel.Location = new System.Drawing.Point(10, 11);
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(516, 49);
            this.StatusLabel.TabIndex = 22;
            this.StatusLabel.Text = "label2";
            this.StatusLabel.UseMnemonic = false;
            // 
            // updateTimer
            // 
            this.updateTimer.Tick += new System.EventHandler(this.onPrintingProcTimer);
            // 
            // findPreviewTimer
            // 
            this.findPreviewTimer.Tick += new System.EventHandler(this.onFindPreviewTimer);
            // 
            // filesysPreviewWatcher
            // 
            this.filesysPreviewWatcher.EnableRaisingEvents = true;
            this.filesysPreviewWatcher.SynchronizingObject = this;
            // 
            // mapWatcher
            // 
            this.mapWatcher.EnableRaisingEvents = true;
            this.mapWatcher.SynchronizingObject = this;
            // 
            // findMapTimer
            // 
            this.findMapTimer.Tick += new System.EventHandler(this.onFindMapTimer);
            // 
            // MonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.ClientSize = new System.Drawing.Size(883, 481);
            this.Controls.Add(this.StatusLabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.f8Button);
            this.Controls.Add(this.previewAllCloseButton);
            this.Controls.Add(this.printAllButton);
            this.Controls.Add(this.printStopButton);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.bitmapMinimizeButton);
            this.Controls.Add(this.bitmapMaximizeButton);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.descriptionTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.printButton);
            this.Controls.Add(this.previewButton);
            this.Controls.Add(this.ReportInfoListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(311, 280);
            this.Name = "MonitorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "環境将軍R - 印刷";
            ((System.ComponentModel.ISupportInitialize)(this.filesysWatcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.filesysPreviewWatcher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mapWatcher)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.IO.FileSystemWatcher filesysWatcher;
        private System.Windows.Forms.ListBox ReportInfoListBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button printButton;
        private System.Windows.Forms.Button previewButton;
        private System.Windows.Forms.TextBox descriptionTextBox;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer findTimer;
        private System.Windows.Forms.Button bitmapMinimizeButton;
        private System.Windows.Forms.Button bitmapMaximizeButton;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button f8Button;
        private System.Windows.Forms.Button previewAllCloseButton;
        private System.Windows.Forms.Button printAllButton;
        private System.Windows.Forms.Button printStopButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label StatusLabel;
        private System.Windows.Forms.Timer updateTimer;
        private System.Windows.Forms.Timer findPreviewTimer;
        private System.IO.FileSystemWatcher filesysPreviewWatcher;
        private System.IO.FileSystemWatcher mapWatcher;
        private System.Windows.Forms.Timer findMapTimer;

    }
}