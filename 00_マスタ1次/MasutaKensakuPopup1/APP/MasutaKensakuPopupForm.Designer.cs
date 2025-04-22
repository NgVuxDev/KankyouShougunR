namespace MasutaKensakuPopup1.APP
{
    partial class MasutaKensakuPopupForm
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
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            this.lb_title = new System.Windows.Forms.Label();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.MasterItem = new GrapeCity.Win.MultiRow.GcMultiRow();
            this.masutaKensakuPopupDetail1 = new MasutaKensakuPopup1.MultiRowTemplate.MasutaKensakuPopupDetail();
            ((System.ComponentModel.ISupportInitialize)(this.MasterItem)).BeginInit();
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
            this.lb_title.Text = "検索条件";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func12.Location = new System.Drawing.Point(244, 375);
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
            // MasterItem
            // 
            this.MasterItem.AllowClipboard = false;
            this.MasterItem.AllowUserToAddRows = false;
            this.MasterItem.AllowUserToDeleteRows = false;
            this.MasterItem.AutoFitContent = GrapeCity.Win.MultiRow.AutoFitContent.All;
            this.MasterItem.ClipboardCopyMode = GrapeCity.Win.MultiRow.ClipboardCopyMode.Disable;
            this.MasterItem.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Double, System.Drawing.Color.Red);
            cellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            cellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            cellStyle1.SelectionBackColor = System.Drawing.Color.Transparent;
            cellStyle1.SelectionForeColor = System.Drawing.Color.Black;
            this.MasterItem.DefaultCellStyle = cellStyle1;
            this.MasterItem.Location = new System.Drawing.Point(12, 43);
            this.MasterItem.Name = "MasterItem";
            this.MasterItem.Size = new System.Drawing.Size(312, 326);
            this.MasterItem.TabIndex = 1;
            this.MasterItem.Template = this.masutaKensakuPopupDetail1;
            this.MasterItem.Text = "gcMultiRow1";
            this.MasterItem.CellDoubleClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.ItemCellDoubleClick);
            this.MasterItem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ItemKeyUp);
            // 
            // MasutaKensakuPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(336, 422);
            this.Controls.Add(this.MasterItem);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.lb_title);
            this.Name = "MasutaKensakuPopupForm";
            this.Text = "MasutaKensakuPopupForm";
            ((System.ComponentModel.ISupportInitialize)(this.MasterItem)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        private MultiRowTemplate.MasutaKensakuPopupDetail masutaKensakuPopupDetail1;
        internal GrapeCity.Win.MultiRow.GcMultiRow MasterItem;
        public r_framework.CustomControl.CustomButton bt_func12;
    }
}