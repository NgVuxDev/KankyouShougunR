using r_framework.CustomControl;
namespace r_framework.APP.Base
{
    partial class HeaderBaseForm
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
            this.lb_title = new System.Windows.Forms.Label();
            this.windowTypeLabel = new r_framework.CustomControl.CustomWindowTypeLabel();
            this.imeStatus = new r_framework.Components.ImeStatus(this.components);
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(82, 6);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(460, 34);
            this.lb_title.TabIndex = 387;
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.BackColor = System.Drawing.Color.Cornsilk;
            this.windowTypeLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.windowTypeLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.windowTypeLabel.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Bold);
            this.windowTypeLabel.ForeColor = System.Drawing.Color.Black;
            this.windowTypeLabel.Location = new System.Drawing.Point(0, 8);
            this.windowTypeLabel.Name = "windowTypeLabel";
            this.windowTypeLabel.Size = new System.Drawing.Size(70, 30);
            this.windowTypeLabel.TabIndex = 388;
            this.windowTypeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.windowTypeLabel.WindowType = r_framework.Const.WINDOW_TYPE.NONE;
            // 
            // HeaderBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.windowTypeLabel);
            this.Controls.Add(this.lb_title);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "HeaderBaseForm";
            this.Text = "BaseForm01";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.HeaderBaseForm_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        public CustomWindowTypeLabel windowTypeLabel;
        public System.Windows.Forms.Label lb_title;
        private Components.ImeStatus imeStatus;

    }
}