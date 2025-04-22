// $Id: UIHeaderForm.Designer.cs 50276 2015-05-21 06:30:50Z sanbongi $
namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeader));
            this.label5 = new System.Windows.Forms.Label();
            this.readDataNumber = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(275, 8);
            this.windowTypeLabel.Size = new System.Drawing.Size(106, 32);
            this.windowTypeLabel.TabIndex = 1000;
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold);
            this.lb_title.Location = new System.Drawing.Point(0, 6);
            this.lb_title.Size = new System.Drawing.Size(269, 34);
            this.lb_title.TabIndex = 1001;
            this.lb_title.Text = "電子契約最新照会";
            this.lb_title.TextChanged += new System.EventHandler(this.lb_title_TextChanged);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(979, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 1018;
            this.label5.Text = "読込データ件数";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // readDataNumber
            // 
            this.readDataNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.readDataNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.readDataNumber.DefaultBackColor = System.Drawing.Color.Empty;
            this.readDataNumber.DisplayPopUp = null;
            this.readDataNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("readDataNumber.FocusOutCheckMethod")));
            this.readDataNumber.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.readDataNumber.ForeColor = System.Drawing.Color.Black;
            this.readDataNumber.IsInputErrorOccured = false;
            this.readDataNumber.Location = new System.Drawing.Point(1093, 6);
            this.readDataNumber.Name = "readDataNumber";
            this.readDataNumber.PopupAfterExecute = null;
            this.readDataNumber.PopupBeforeExecute = null;
            this.readDataNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("readDataNumber.PopupSearchSendParams")));
            this.readDataNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.readDataNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("readDataNumber.popupWindowSetting")));
            this.readDataNumber.ReadOnly = true;
            this.readDataNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("readDataNumber.RegistCheckMethod")));
            this.readDataNumber.Size = new System.Drawing.Size(80, 20);
            this.readDataNumber.TabIndex = 12;
            this.readDataNumber.TabStop = false;
            this.readDataNumber.Tag = "検索結果の総件数が表示します";
            this.readDataNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.readDataNumber);
            this.Controls.Add(this.label5);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.readDataNumber, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label label5;
        public r_framework.CustomControl.CustomTextBox readDataNumber;

    }
}