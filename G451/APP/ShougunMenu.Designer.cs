namespace Shougun.Core.Common.Menu
{
    partial class ShougunMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShougunMenu));
            this.messageSettingDto1 = new Shougun.Core.Common.Menu.TsuuchiJouhou.MessageSettingDto();
            this.messageSettingDto2 = new Shougun.Core.Common.Menu.TsuuchiJouhou.MessageSettingDto();
            this.messageSettingDto3 = new Shougun.Core.Common.Menu.TsuuchiJouhou.MessageSettingDto();
            this.tsuuchiJouhou1 = new Shougun.Core.Common.Menu.TsuuchiJouhou();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.VersionLabel = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.SaveLogLabel = new System.Windows.Forms.Label();
            this.lblSupport = new System.Windows.Forms.Label();
            this.pnlSupport = new System.Windows.Forms.Panel();
            this.lblOpenLinkSupport = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.pnlSupport.SuspendLayout();
            this.SuspendLayout();
            // 
            // messageSettingDto1
            // 
            this.messageSettingDto1.ExistMessage = "JWNETからの新着『重要通知情報』が{0}あります。";
            this.messageSettingDto1.LinkFormID = "G411";
            this.messageSettingDto1.Unit = "件";
            this.messageSettingDto1.ZeroMessage = "JWNETからの新着『重要通知情報』はありません。";
            // 
            // messageSettingDto2
            // 
            this.messageSettingDto2.ExistMessage = "JWNETからの新着『お知らせ通知情報』が{0}あります。";
            this.messageSettingDto2.LinkFormID = "G411";
            this.messageSettingDto2.Unit = "件";
            this.messageSettingDto2.ZeroMessage = "JWNETからの新着『お知らせ通知情報』はありません。";
            // 
            // messageSettingDto3
            // 
            this.messageSettingDto3.ExistMessage = "JWNETとの『通信確認情報』が{0}あります。";
            this.messageSettingDto3.LinkFormID = "G412";
            this.messageSettingDto3.Unit = "件";
            this.messageSettingDto3.ZeroMessage = "";
            // 
            // tsuuchiJouhou1
            // 
            this.tsuuchiJouhou1.BackColor = System.Drawing.Color.Transparent;
            this.tsuuchiJouhou1.FrameBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tsuuchiJouhou1.Location = new System.Drawing.Point(64, 132);
            this.tsuuchiJouhou1.MessageColor = System.Drawing.Color.Black;
            this.tsuuchiJouhou1.MessageFont = new System.Drawing.Font("MS Gothic", 9.75F);
            this.tsuuchiJouhou1.MessageSetting.Add(this.messageSettingDto1);
            this.tsuuchiJouhou1.MessageSetting.Add(this.messageSettingDto2);
            this.tsuuchiJouhou1.MessageSetting.Add(this.messageSettingDto3);
            this.tsuuchiJouhou1.MessageSize = new System.Drawing.Size(410, 20);
            this.tsuuchiJouhou1.MessageSpan = 3;
            this.tsuuchiJouhou1.MessageStartLocation = new System.Drawing.Point(10, 18);
            this.tsuuchiJouhou1.Name = "tsuuchiJouhou1";
            this.tsuuchiJouhou1.Size = new System.Drawing.Size(445, 100);
            this.tsuuchiJouhou1.TabIndex = 1;
            this.tsuuchiJouhou1.TitleAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.tsuuchiJouhou1.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.tsuuchiJouhou1.TitleFont = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.tsuuchiJouhou1.TitleForColor = System.Drawing.Color.White;
            this.tsuuchiJouhou1.TitleLocationX = 30;
            this.tsuuchiJouhou1.TitleSize = new System.Drawing.Size(120, 20);
            this.tsuuchiJouhou1.TitleVisible = true;
            this.tsuuchiJouhou1.TsuuchiSpan = 300;
            this.tsuuchiJouhou1.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pictureBox1.Location = new System.Drawing.Point(1, 601);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(362, 139);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // VersionLabel
            // 
            this.VersionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.VersionLabel.AutoSize = true;
            this.VersionLabel.Font = new System.Drawing.Font("Meiryo UI", 12F);
            this.VersionLabel.Location = new System.Drawing.Point(134, 720);
            this.VersionLabel.Name = "VersionLabel";
            this.VersionLabel.Size = new System.Drawing.Size(99, 20);
            this.VersionLabel.TabIndex = 2;
            this.VersionLabel.Text = "Version情報";
            this.VersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.VersionLabel.DoubleClick += new System.EventHandler(this.VersionLabel_DoubleClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.Location = new System.Drawing.Point(666, 230);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(480, 510);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 4;
            this.pictureBox2.TabStop = false;
            // 
            // SaveLogLabel
            // 
            this.SaveLogLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.SaveLogLabel.Location = new System.Drawing.Point(-1, 573);
            this.SaveLogLabel.Name = "SaveLogLabel";
            this.SaveLogLabel.Size = new System.Drawing.Size(25, 25);
            this.SaveLogLabel.TabIndex = 5;
            this.SaveLogLabel.DoubleClick += new System.EventHandler(this.SaveLogLabel_DoubleClick);
            // 
            // lblSupport
            // 
            this.lblSupport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblSupport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSupport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSupport.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSupport.ForeColor = System.Drawing.Color.White;
            this.lblSupport.Location = new System.Drawing.Point(600, 132);
            this.lblSupport.Name = "lblSupport";
            this.lblSupport.Size = new System.Drawing.Size(120, 20);
            this.lblSupport.TabIndex = 36;
            this.lblSupport.Text = "ヘルプ";
            this.lblSupport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSupport
            // 
            this.pnlSupport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSupport.Controls.Add(this.lblOpenLinkSupport);
            this.pnlSupport.Location = new System.Drawing.Point(590, 142);
            this.pnlSupport.Name = "pnlSupport";
            this.pnlSupport.Size = new System.Drawing.Size(350, 50);
            this.pnlSupport.TabIndex = 37;
            // 
            // lblOpenLinkSupport
            // 
            this.lblOpenLinkSupport.AutoSize = true;
            this.lblOpenLinkSupport.Font = new System.Drawing.Font("MS Gothic", 11.25F);
            this.lblOpenLinkSupport.Location = new System.Drawing.Point(10, 15);
            this.lblOpenLinkSupport.Name = "lblOpenLinkSupport";
            this.lblOpenLinkSupport.Size = new System.Drawing.Size(183, 15);
            this.lblOpenLinkSupport.TabIndex = 12;
            this.lblOpenLinkSupport.TabStop = true;
            this.lblOpenLinkSupport.Text = "サポートサイトはこちら";
            this.lblOpenLinkSupport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOpenLinkSupport.Click += new System.EventHandler(this.lblOpenLinkSupport_Click);
            // 
            // ShougunMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1146, 740);
            this.Controls.Add(this.lblSupport);
            this.Controls.Add(this.pnlSupport);
            this.Controls.Add(this.VersionLabel);
            this.Controls.Add(this.SaveLogLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.tsuuchiJouhou1);
            this.Controls.Add(this.pictureBox2);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = this.Size;
            this.Name = "ShougunMenu";
            this.Text = "環境将軍R　メインメニュー";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShougunMenu_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ShougunMenu_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.pnlSupport.ResumeLayout(false);
            this.pnlSupport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TsuuchiJouhou.MessageSettingDto messageSettingDto1;
        private TsuuchiJouhou.MessageSettingDto messageSettingDto2;
        private TsuuchiJouhou.MessageSettingDto messageSettingDto3;
        private TsuuchiJouhou tsuuchiJouhou1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label VersionLabel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label SaveLogLabel;
        internal System.Windows.Forms.Label lblSupport;
        internal System.Windows.Forms.Panel pnlSupport;
        private System.Windows.Forms.LinkLabel lblOpenLinkSupport;
    }
}