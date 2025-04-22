namespace Shougun.Printing.Client
{
    partial class BackgroundForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackgroundForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.programMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.PrintSettingsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.DestinationCopyMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.closeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.TerminateMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon.BalloonTipText = "マウス右クリックでメニューを表示できます。";
            this.notifyIcon.BalloonTipTitle = "環境将軍R クライアント印刷プログラム";
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "環境将軍R クライアント印刷プログラム\r\n左クリックで印刷画面を前面に表示します。\r\n右クリックでメニューを表示します。";
            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseClick);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.programMenuItem,
            this.toolStripSeparator1,
            this.PrintSettingsMenuItem,
            this.toolStripSeparator2,
            this.DestinationCopyMenuItem,
            this.toolStripSeparator3,
            this.closeMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(305, 110);
            this.contextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip_Opening);
            // 
            // programMenuItem
            // 
            this.programMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.programMenuItem.ForeColor = System.Drawing.Color.White;
            this.programMenuItem.Name = "programMenuItem";
            this.programMenuItem.Size = new System.Drawing.Size(304, 22);
            this.programMenuItem.Text = "(プログラム名)";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(301, 6);
            // 
            // PrintSettingsMenuItem
            // 
            this.PrintSettingsMenuItem.AutoToolTip = true;
            this.PrintSettingsMenuItem.Name = "PrintSettingsMenuItem";
            this.PrintSettingsMenuItem.Size = new System.Drawing.Size(304, 22);
            this.PrintSettingsMenuItem.Text = "印刷設定...";
            this.PrintSettingsMenuItem.ToolTipText = "印刷設定画面を表示します。";
            this.PrintSettingsMenuItem.Click += new System.EventHandler(this.PrintSettingsMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(301, 6);
            // 
            // DestinationCopyMenuItem
            // 
            this.DestinationCopyMenuItem.AutoToolTip = true;
            this.DestinationCopyMenuItem.Name = "DestinationCopyMenuItem";
            this.DestinationCopyMenuItem.Size = new System.Drawing.Size(304, 22);
            this.DestinationCopyMenuItem.Text = "出力先フォルダをクリップボードにコピー";
            this.DestinationCopyMenuItem.ToolTipText = "クリップボードにコピーしたテキストは環境将軍R本体のマスタ/システムメニュー/印刷設定画面で利用します。";
            this.DestinationCopyMenuItem.Click += new System.EventHandler(this.DestinationCopyMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(301, 6);
            // 
            // closeMenuItem
            // 
            this.closeMenuItem.Name = "closeMenuItem";
            this.closeMenuItem.Size = new System.Drawing.Size(304, 22);
            this.closeMenuItem.Text = "終了";
            this.closeMenuItem.Click += new System.EventHandler(this.closeMenuItem_Click);
            // 
            // TerminateMenuItem
            // 
            this.TerminateMenuItem.Name = "TerminateMenuItem";
            this.TerminateMenuItem.Size = new System.Drawing.Size(32, 19);
            // 
            // BackgroundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(219, 100);
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BackgroundForm";
            this.Opacity = 0D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "環境将軍 印刷バックグラウンドプログラム";
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem PrintSettingsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DestinationCopyMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem programMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

