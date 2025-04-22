namespace Shougun.Printing.Common.UI
{
    partial class ServerPrintSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerPrintSettingsDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.storeButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.printingDirectoryTextBox = new System.Windows.Forms.TextBox();
            this.pasteButton = new System.Windows.Forms.Button();
            this.testButton = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.clientNameLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(19, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(207, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "印刷設定";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(27, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 22);
            this.label2.TabIndex = 2;
            this.label2.Text = "出力先フォルダ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // storeButton
            // 
            this.storeButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.storeButton.Location = new System.Drawing.Point(520, 357);
            this.storeButton.Name = "storeButton";
            this.storeButton.Size = new System.Drawing.Size(88, 38);
            this.storeButton.TabIndex = 5;
            this.storeButton.Text = "[F9]\r\n保存";
            this.storeButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.storeButton.UseVisualStyleBackColor = true;
            this.storeButton.Click += new System.EventHandler(this.storeButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.closeButton.Location = new System.Drawing.Point(614, 357);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(88, 38);
            this.closeButton.TabIndex = 9;
            this.closeButton.Text = "[F12]    終了";
            this.closeButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // printingDirectoryTextBox
            // 
            this.printingDirectoryTextBox.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.5F);
            this.printingDirectoryTextBox.Location = new System.Drawing.Point(24, 168);
            this.printingDirectoryTextBox.Name = "printingDirectoryTextBox";
            this.printingDirectoryTextBox.Size = new System.Drawing.Size(678, 20);
            this.printingDirectoryTextBox.TabIndex = 10;
            this.printingDirectoryTextBox.TextChanged += new System.EventHandler(this.printingDirectoryTextBox_TextChanged);
            // 
            // pasteButton
            // 
            this.pasteButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.pasteButton.Location = new System.Drawing.Point(392, 143);
            this.pasteButton.Name = "pasteButton";
            this.pasteButton.Size = new System.Drawing.Size(310, 22);
            this.pasteButton.TabIndex = 13;
            this.pasteButton.Text = "クリップボードから貼り付け";
            this.pasteButton.UseVisualStyleBackColor = true;
            this.pasteButton.Click += new System.EventHandler(this.pasteButton_Click);
            // 
            // testButton
            // 
            this.testButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.testButton.Location = new System.Drawing.Point(392, 357);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(88, 38);
            this.testButton.TabIndex = 15;
            this.testButton.Text = "[F8]    \r\nテスト";
            this.testButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(24, 200);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(678, 140);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.Location = new System.Drawing.Point(27, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(192, 22);
            this.label3.TabIndex = 17;
            this.label3.Text = "クライアントPC名";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // clientNameLabel
            // 
            this.clientNameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clientNameLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.clientNameLabel.Location = new System.Drawing.Point(229, 86);
            this.clientNameLabel.Name = "clientNameLabel";
            this.clientNameLabel.Size = new System.Drawing.Size(265, 22);
            this.clientNameLabel.TabIndex = 18;
            this.clientNameLabel.Text = "クライアントPC名";
            this.clientNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ServerPrintSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(718, 404);
            this.Controls.Add(this.clientNameLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.testButton);
            this.Controls.Add(this.pasteButton);
            this.Controls.Add(this.printingDirectoryTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.storeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ServerPrintSettingsDialog";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "印刷設定";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button storeButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.TextBox printingDirectoryTextBox;
        private System.Windows.Forms.Button pasteButton;
        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label clientNameLabel;
    }
}