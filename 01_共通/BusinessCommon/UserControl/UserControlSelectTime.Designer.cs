namespace Shougun.Core.Common.BusinessCommon.UserControl
{
    partial class UserControlSelectTime
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cmbBun = new r_framework.CustomControl.CustomComboBox();
            this.cmbJikan = new r_framework.CustomControl.CustomComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label1.Location = new System.Drawing.Point(54, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = ":";
            // 
            // cmbBun
            // 
            this.cmbBun.BackColor = System.Drawing.SystemColors.Window;
            this.cmbBun.DefaultBackColor = System.Drawing.Color.Empty;
            this.cmbBun.DisplayPopUp = null;
            this.cmbBun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbBun.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.cmbBun.FormattingEnabled = true;
            this.cmbBun.Location = new System.Drawing.Point(77, 0);
            this.cmbBun.Margin = new System.Windows.Forms.Padding(0);
            this.cmbBun.MaximumSize = new System.Drawing.Size(46, 0);
            this.cmbBun.MaxLength = 2;
            this.cmbBun.MinimumSize = new System.Drawing.Size(46, 0);
            this.cmbBun.Name = "cmbBun";
            this.cmbBun.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cmbBun.Size = new System.Drawing.Size(46, 21);
            this.cmbBun.TabIndex = 1;
            this.cmbBun.Tag = " は 2 文字以内で入力してください。";
            this.cmbBun.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbBun_KeyPress);
            this.cmbBun.Leave += new System.EventHandler(this.cmbBun_Leave);
            // 
            // cmbJikan
            // 
            this.cmbJikan.BackColor = System.Drawing.SystemColors.Window;
            this.cmbJikan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cmbJikan.DisplayPopUp = null;
            this.cmbJikan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbJikan.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.cmbJikan.FormattingEnabled = true;
            this.cmbJikan.Location = new System.Drawing.Point(0, 0);
            this.cmbJikan.Margin = new System.Windows.Forms.Padding(0);
            this.cmbJikan.MaximumSize = new System.Drawing.Size(46, 0);
            this.cmbJikan.MaxLength = 2;
            this.cmbJikan.MinimumSize = new System.Drawing.Size(46, 0);
            this.cmbJikan.Name = "cmbJikan";
            this.cmbJikan.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cmbJikan.Size = new System.Drawing.Size(46, 21);
            this.cmbJikan.TabIndex = 0;
            this.cmbJikan.Tag = " は 2 文字以内で入力してください。";
            this.cmbJikan.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbJikan_KeyPress);
            this.cmbJikan.Leave += new System.EventHandler(this.cmbJikan_Leave);
            // 
            // UserControlSelectTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.cmbBun);
            this.Controls.Add(this.cmbJikan);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(123, 21);
            this.MinimumSize = new System.Drawing.Size(123, 21);
            this.Name = "UserControlSelectTime";
            this.Size = new System.Drawing.Size(121, 19);
            this.Load += new System.EventHandler(this.UserControlSelectTime_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private r_framework.CustomControl.CustomComboBox cmbJikan;
        private r_framework.CustomControl.CustomComboBox cmbBun;

    }
}
