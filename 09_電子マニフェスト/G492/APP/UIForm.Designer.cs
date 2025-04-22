namespace Shougun.Core.ElectronicManifest.DenshiManifestPatternTouroku
{
    partial class DenshiManifestPatternTouroku
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DenshiManifestPatternTouroku));
            this.lbl_title = new System.Windows.Forms.Label();
            this.lbl_PatternNameKana = new System.Windows.Forms.Label();
            this.lbl_PatternName = new System.Windows.Forms.Label();
            this.lbl_Comment = new System.Windows.Forms.Label();
            this.txt_PatternNameKana = new r_framework.CustomControl.CustomTextBox();
            this.txt_PatternName = new r_framework.CustomControl.CustomTextBox();
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_title.ForeColor = System.Drawing.Color.White;
            this.lbl_title.Location = new System.Drawing.Point(6, 5);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(490, 34);
            this.lbl_title.TabIndex = 4;
            this.lbl_title.Text = "直行一次パターン登録";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PatternNameKana
            // 
            this.lbl_PatternNameKana.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_PatternNameKana.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_PatternNameKana.ForeColor = System.Drawing.Color.White;
            this.lbl_PatternNameKana.Location = new System.Drawing.Point(6, 69);
            this.lbl_PatternNameKana.Name = "lbl_PatternNameKana";
            this.lbl_PatternNameKana.Size = new System.Drawing.Size(110, 20);
            this.lbl_PatternNameKana.TabIndex = 6;
            this.lbl_PatternNameKana.Text = "フリガナ※";
            this.lbl_PatternNameKana.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_PatternName
            // 
            this.lbl_PatternName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_PatternName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_PatternName.ForeColor = System.Drawing.Color.White;
            this.lbl_PatternName.Location = new System.Drawing.Point(6, 93);
            this.lbl_PatternName.Name = "lbl_PatternName";
            this.lbl_PatternName.Size = new System.Drawing.Size(110, 20);
            this.lbl_PatternName.TabIndex = 0;
            this.lbl_PatternName.Text = "パターン名※";
            this.lbl_PatternName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Comment
            // 
            this.lbl_Comment.Location = new System.Drawing.Point(6, 46);
            this.lbl_Comment.Name = "lbl_Comment";
            this.lbl_Comment.Size = new System.Drawing.Size(257, 22);
            this.lbl_Comment.TabIndex = 5;
            this.lbl_Comment.Text = "パターン名を入力してください。";
            this.lbl_Comment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_PatternNameKana
            // 
            this.txt_PatternNameKana.BackColor = System.Drawing.SystemColors.Window;
            this.txt_PatternNameKana.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_PatternNameKana.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_PatternNameKana.DisplayItemName = "フリガナ";
            this.txt_PatternNameKana.DisplayPopUp = null;
            this.txt_PatternNameKana.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_PatternNameKana.FocusOutCheckMethod")));
            this.txt_PatternNameKana.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_PatternNameKana.ForeColor = System.Drawing.Color.Black;
            this.txt_PatternNameKana.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.txt_PatternNameKana.IsInputErrorOccured = false;
            this.txt_PatternNameKana.Location = new System.Drawing.Point(121, 69);
            this.txt_PatternNameKana.MaxLength = 80;
            this.txt_PatternNameKana.Name = "txt_PatternNameKana";
            this.txt_PatternNameKana.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_PatternNameKana.PopupSearchSendParams")));
            this.txt_PatternNameKana.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_PatternNameKana.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_PatternNameKana.popupWindowSetting")));
            this.txt_PatternNameKana.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_PatternNameKana.RegistCheckMethod")));
            this.txt_PatternNameKana.Size = new System.Drawing.Size(298, 20);
            this.txt_PatternNameKana.TabIndex = 7;
            this.txt_PatternNameKana.Text = "１２３４５６７８９０１２３４５６７８９０";
            // 
            // txt_PatternName
            // 
            this.txt_PatternName.BackColor = System.Drawing.SystemColors.Window;
            this.txt_PatternName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_PatternName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_PatternName.DisplayPopUp = null;
            this.txt_PatternName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_PatternName.FocusOutCheckMethod")));
            this.txt_PatternName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_PatternName.ForeColor = System.Drawing.Color.Black;
            this.txt_PatternName.FuriganaAutoSetControl = "txt_PatternNameKana";
            this.txt_PatternName.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txt_PatternName.IsInputErrorOccured = false;
            this.txt_PatternName.Location = new System.Drawing.Point(121, 93);
            this.txt_PatternName.MaxLength = 40;
            this.txt_PatternName.Name = "txt_PatternName";
            this.txt_PatternName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_PatternName.PopupSearchSendParams")));
            this.txt_PatternName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_PatternName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_PatternName.popupWindowSetting")));
            this.txt_PatternName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_PatternName.RegistCheckMethod")));
            this.txt_PatternName.Size = new System.Drawing.Size(298, 20);
            this.txt_PatternName.TabIndex = 1;
            this.txt_PatternName.Text = "あああああ";
            // 
            // bt_func9
            // 
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Location = new System.Drawing.Point(302, 121);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(80, 35);
            this.bt_func9.TabIndex = 2;
            this.bt_func9.TabStop = false;
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            //this.bt_func12.CausesValidation = false;
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Location = new System.Drawing.Point(387, 121);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 3;
            this.bt_func12.TabStop = false;
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // DenshiManifestPatternTouroku
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(503, 169);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.bt_func9);
            this.Controls.Add(this.txt_PatternName);
            this.Controls.Add(this.txt_PatternNameKana);
            this.Controls.Add(this.lbl_Comment);
            this.Controls.Add(this.lbl_PatternName);
            this.Controls.Add(this.lbl_PatternNameKana);
            this.Controls.Add(this.lbl_title);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DenshiManifestPatternTouroku";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "マニフェストパターン登録ポップアップ";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lbl_title;
        internal r_framework.CustomControl.CustomTextBox txt_PatternNameKana;
        internal r_framework.CustomControl.CustomTextBox txt_PatternName;
        internal r_framework.CustomControl.CustomButton bt_func9;
        internal r_framework.CustomControl.CustomButton bt_func12;
        internal System.Windows.Forms.Label lbl_Comment;
        internal System.Windows.Forms.Label lbl_PatternNameKana;
        internal System.Windows.Forms.Label lbl_PatternName;

    }
}