namespace Shougun.Core.BusinessManagement.DenshiShinseiNyuuryoku
{
    partial class KessaiPopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KessaiPopupForm));
            this.lb_title = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.KESSAI_COMMENT = new r_framework.CustomControl.CustomTextBox();
            this.bt_func11 = new r_framework.CustomControl.CustomButton();
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
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
            this.lb_title.Text = "○○決裁";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 40);
            this.label1.TabIndex = 381;
            this.label1.Text = "コメントを入力してください。　全角100文字まで登録できます。";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KESSAI_COMMENT
            // 
            this.KESSAI_COMMENT.BackColor = System.Drawing.SystemColors.Window;
            this.KESSAI_COMMENT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KESSAI_COMMENT.CharactersNumber = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.KESSAI_COMMENT.DefaultBackColor = System.Drawing.Color.Empty;
            this.KESSAI_COMMENT.DisplayItemName = "決裁コメント";
            this.KESSAI_COMMENT.DisplayPopUp = null;
            this.KESSAI_COMMENT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KESSAI_COMMENT.FocusOutCheckMethod")));
            this.KESSAI_COMMENT.ForeColor = System.Drawing.Color.Black;
            this.KESSAI_COMMENT.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.KESSAI_COMMENT.IsInputErrorOccured = false;
            this.KESSAI_COMMENT.Location = new System.Drawing.Point(12, 100);
            this.KESSAI_COMMENT.MaxLength = 200;
            this.KESSAI_COMMENT.Multiline = true;
            this.KESSAI_COMMENT.Name = "KESSAI_COMMENT";
            this.KESSAI_COMMENT.PopupAfterExecute = null;
            this.KESSAI_COMMENT.PopupBeforeExecute = null;
            this.KESSAI_COMMENT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KESSAI_COMMENT.PopupSearchSendParams")));
            this.KESSAI_COMMENT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KESSAI_COMMENT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KESSAI_COMMENT.popupWindowSetting")));
            this.KESSAI_COMMENT.prevText = null;
            this.KESSAI_COMMENT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KESSAI_COMMENT.RegistCheckMethod")));
            this.KESSAI_COMMENT.Size = new System.Drawing.Size(292, 80);
            this.KESSAI_COMMENT.TabIndex = 1;
            // 
            // bt_func11
            // 
            this.bt_func11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func11.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func11.Location = new System.Drawing.Point(224, 200);
            this.bt_func11.Name = "bt_func11";
            this.bt_func11.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func11.Size = new System.Drawing.Size(80, 35);
            this.bt_func11.TabIndex = 382;
            this.bt_func11.TabStop = false;
            this.bt_func11.Tag = "";
            this.bt_func11.Text = "[F11]\r\n取消";
            this.bt_func11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func11.UseVisualStyleBackColor = false;
            // 
            // bt_func9
            // 
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func9.Location = new System.Drawing.Point(138, 200);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(80, 35);
            this.bt_func9.TabIndex = 383;
            this.bt_func9.TabStop = false;
            this.bt_func9.Tag = "";
            this.bt_func9.Text = "[F9]　　決定";
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            // 
            // KessaiPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(318, 252);
            this.Controls.Add(this.bt_func9);
            this.Controls.Add(this.bt_func11);
            this.Controls.Add(this.KESSAI_COMMENT);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_title);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KessaiPopupForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "決裁コメント入力";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomButton bt_func11;
        internal r_framework.CustomControl.CustomButton bt_func9;
        internal r_framework.CustomControl.CustomTextBox KESSAI_COMMENT;
    }
}