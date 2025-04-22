namespace JushoKensakuPopup1.APP
{
    partial class JushoKensakuPopupForm
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
            this.lb_title = new System.Windows.Forms.Label();
            this.JushoDetail = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.jushoKensakuDetail1 = new JushoKensakuPopup1.MultiRowTemplate.JushoKensakuDetail();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            ((System.ComponentModel.ISupportInitialize)(this.JushoDetail)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20F, System.Drawing.FontStyle.Bold);
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(12, 9);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(232, 31);
            this.lb_title.TabIndex = 379;
            this.lb_title.Text = "住所検索";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JushoDetail
            // 
            this.JushoDetail.AllowClipboard = false;
            this.JushoDetail.AllowUserToAddRows = false;
            this.JushoDetail.AllowUserToDeleteRows = false;
            this.JushoDetail.ClipboardCopyMode = GrapeCity.Win.MultiRow.ClipboardCopyMode.Disable;
            this.JushoDetail.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Double, System.Drawing.Color.Red);
            this.JushoDetail.Location = new System.Drawing.Point(13, 52);
            this.JushoDetail.Name = "JushoDetail";
            this.JushoDetail.Size = new System.Drawing.Size(683, 399);
            this.JushoDetail.TabIndex = 380;
            this.JushoDetail.Template = this.jushoKensakuDetail1;
            this.JushoDetail.Text = "JushoDetail";
            this.JushoDetail.CellDoubleClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.JushoDetail_CellDoubleClick);
            this.JushoDetail.KeyUp += new System.Windows.Forms.KeyEventHandler(this.JushoDetail_KeyUp);
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func12.Location = new System.Drawing.Point(621, 478);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 391;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "func12";
            this.bt_func12.Text = "　[F12]　閉じる";
            this.bt_func12.UseVisualStyleBackColor = false;
            this.bt_func12.Click += new System.EventHandler(this.FormClose);
            // 
            // JushoKensakuPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(713, 525);
            this.Controls.Add(this.JushoDetail);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.lb_title);
            this.Name = "JushoKensakuPopupForm";
            this.Text = "JushoKensakuPopupForm";
            ((System.ComponentModel.ISupportInitialize)(this.JushoDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        private MultiRowTemplate.JushoKensakuDetail jushoKensakuDetail1;
        public r_framework.CustomControl.CustomButton bt_func12;
        internal GrapeCity.Win.MultiRow.GcMultiRow JushoDetail;
    }
}