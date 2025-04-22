namespace Shougun.Core.PaperManifest.JissekiHokokuUnpanCsv
{
    partial class UIHeader
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
            this.btnZenOn = new r_framework.CustomControl.CustomButton();
            this.btnZenOff = new r_framework.CustomControl.CustomButton();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Size = new System.Drawing.Size(25, 30);
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(31, 5);
            this.lb_title.Size = new System.Drawing.Size(419, 34);
            // 
            // btnZenOn
            // 
            this.btnZenOn.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnZenOn.Location = new System.Drawing.Point(542, 11);
            this.btnZenOn.Name = "btnZenOn";
            this.btnZenOn.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnZenOn.Size = new System.Drawing.Size(75, 23);
            this.btnZenOn.TabIndex = 389;
            this.btnZenOn.Text = "全選択";
            this.btnZenOn.UseVisualStyleBackColor = true;
            // 
            // btnZenOff
            // 
            this.btnZenOff.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnZenOff.Location = new System.Drawing.Point(623, 11);
            this.btnZenOff.Name = "btnZenOff";
            this.btnZenOff.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnZenOff.Size = new System.Drawing.Size(75, 23);
            this.btnZenOff.TabIndex = 390;
            this.btnZenOff.Text = "全解除";
            this.btnZenOff.UseVisualStyleBackColor = true;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1100, 46);
            this.Controls.Add(this.btnZenOff);
            this.Controls.Add(this.btnZenOn);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.btnZenOn, 0);
            this.Controls.SetChildIndex(this.btnZenOff, 0);
            this.ResumeLayout(false);

        }

        #endregion

        public r_framework.CustomControl.CustomButton btnZenOn;
        public r_framework.CustomControl.CustomButton btnZenOff;



    }
}