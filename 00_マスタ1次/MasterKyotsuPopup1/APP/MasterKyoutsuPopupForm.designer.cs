using System.Windows.Forms;
namespace MasterKyoutsuPopup1.APP
{
    partial class MasterKyoutsuPopupForm
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
            this.masterDetail = new GrapeCity.Win.MultiRow.GcMultiRow();
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
            this.lb_title.TabIndex = 379;
            this.lb_title.Text = "○○○○○タイトル";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func12.Location = new System.Drawing.Point(332, 405);
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
            // masterDetail
            // 
            this.masterDetail.AllowClipboard = false;
            this.masterDetail.AllowUserToAddRows = false;
            this.masterDetail.AllowUserToDeleteRows = false;
            this.masterDetail.AutoFitContent = GrapeCity.Win.MultiRow.AutoFitContent.All;
            this.masterDetail.ClipboardCopyMode = GrapeCity.Win.MultiRow.ClipboardCopyMode.Disable;
            this.masterDetail.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Double, System.Drawing.Color.Red);
            this.masterDetail.Location = new System.Drawing.Point(12, 44);
            this.masterDetail.Name = "masterDetail";
            this.masterDetail.Size = new System.Drawing.Size(400, 355);
            this.masterDetail.TabIndex = 392;
            this.masterDetail.Text = "masterDetail";
            this.masterDetail.CellDoubleClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.DetailCellDoubleClick);
            this.masterDetail.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailKeyDown);
            this.masterDetail.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DetailKeyUp);
            // 
            // MasterKyoutsuPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(424, 451);
            this.Controls.Add(this.masterDetail);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.lb_title);
            this.Name = "MasterKyoutsuPopupForm";
            ((System.ComponentModel.ISupportInitialize)(this.masterDetail)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public Label lb_title;
        public r_framework.CustomControl.CustomButton bt_func12;
        internal GrapeCity.Win.MultiRow.GcMultiRow masterDetail;
    }
}
