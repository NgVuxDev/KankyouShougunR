namespace FukusuuSentakuPopup1.APP
{
    partial class FukusuuSentakuPopupForm
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
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_func1 = new r_framework.CustomControl.CustomButton();
            this.masterDetail = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.fukusuuSentakuDetail1 = new FukusuuSentakuPopup1.FukusuuSentakuDetail();
            ((System.ComponentModel.ISupportInitialize)(this.masterDetail)).BeginInit();
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
            this.lb_title.TabIndex = 380;
            this.lb_title.Text = "廃棄物種類選択";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func12.Location = new System.Drawing.Point(318, 469);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 392;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "func12";
            this.bt_func12.Text = "　[F12]　閉じる";
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // bt_func1
            // 
            this.bt_func1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func1.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func1.Location = new System.Drawing.Point(12, 469);
            this.bt_func1.Name = "bt_func1";
            this.bt_func1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func1.Size = new System.Drawing.Size(80, 35);
            this.bt_func1.TabIndex = 393;
            this.bt_func1.TabStop = false;
            this.bt_func1.Tag = "func1";
            this.bt_func1.Text = "　[F1]　選択";
            this.bt_func1.UseVisualStyleBackColor = false;
            // 
            // masterDetail
            // 
            this.masterDetail.AllowUserToAddRows = false;
            this.masterDetail.Location = new System.Drawing.Point(5, 44);
            this.masterDetail.Name = "masterDetail";
            this.masterDetail.Size = new System.Drawing.Size(401, 419);
            this.masterDetail.TabIndex = 394;
            this.masterDetail.Template = this.fukusuuSentakuDetail1;
            this.masterDetail.Text = "gcMultiRow1";
            // 
            // FukusuuSentakuPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(412, 512);
            this.Controls.Add(this.masterDetail);
            this.Controls.Add(this.bt_func1);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.lb_title);
            this.KeyPreview = true;
            this.Name = "FukusuuSentakuPopupForm";
            this.Text = "osi";
            ((System.ComponentModel.ISupportInitialize)(this.masterDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        public r_framework.CustomControl.CustomButton bt_func12;
        private FukusuuSentakuDetail fukusuuSentakuDetail1;
        public r_framework.CustomControl.CustomButton bt_func1;
        internal GrapeCity.Win.MultiRow.GcMultiRow masterDetail;
    }
}