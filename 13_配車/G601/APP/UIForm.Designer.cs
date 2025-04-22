namespace Shougun.Core.Common.MobileTsuushin
{
    partial class UIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.masterBrowse = new r_framework.CustomControl.CustomButton();
            this.masterOutputPath = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.transBrowse = new r_framework.CustomControl.CustomButton();
            this.transOutputPath = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.inputBrowse = new r_framework.CustomControl.CustomButton();
            this.inputPath = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.backUpBrowse = new r_framework.CustomControl.CustomButton();
            this.backUpPath = new r_framework.CustomControl.CustomTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TITLE_LABEL = new System.Windows.Forms.Label();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
            this.SuspendLayout();
            // 
            // masterBrowse
            // 
            this.masterBrowse.DefaultBackColor = System.Drawing.Color.Empty;
            this.masterBrowse.Location = new System.Drawing.Point(524, 56);
            this.masterBrowse.Name = "masterBrowse";
            this.masterBrowse.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.masterBrowse.Size = new System.Drawing.Size(53, 22);
            this.masterBrowse.TabIndex = 3;
            this.masterBrowse.Tag = "ファイル出力先を指定します";
            this.masterBrowse.Text = "参照";
            this.masterBrowse.UseVisualStyleBackColor = true;
            // 
            // masterOutputPath
            // 
            this.masterOutputPath.BackColor = System.Drawing.SystemColors.Window;
            this.masterOutputPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.masterOutputPath.DBFieldsName = "";
            this.masterOutputPath.DefaultBackColor = System.Drawing.Color.Empty;
            this.masterOutputPath.DisplayItemName = "";
            this.masterOutputPath.DisplayPopUp = null;
            this.masterOutputPath.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("masterOutputPath.FocusOutCheckMethod")));
            this.masterOutputPath.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.masterOutputPath.ForeColor = System.Drawing.Color.Black;
            this.masterOutputPath.GetCodeMasterField = "";
            this.masterOutputPath.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.masterOutputPath.IsInputErrorOccured = false;
            this.masterOutputPath.ItemDefinedTypes = "varchar";
            this.masterOutputPath.Location = new System.Drawing.Point(167, 57);
            this.masterOutputPath.Name = "masterOutputPath";
            this.masterOutputPath.PopupAfterExecute = null;
            this.masterOutputPath.PopupBeforeExecute = null;
            this.masterOutputPath.PopupGetMasterField = "";
            this.masterOutputPath.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("masterOutputPath.PopupSearchSendParams")));
            this.masterOutputPath.PopupSendParams = new string[0];
            this.masterOutputPath.PopupSetFormField = "";
            this.masterOutputPath.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.masterOutputPath.PopupWindowName = "";
            this.masterOutputPath.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("masterOutputPath.popupWindowSetting")));
            this.masterOutputPath.prevText = null;
            this.masterOutputPath.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("masterOutputPath.RegistCheckMethod")));
            this.masterOutputPath.SetFormField = "";
            this.masterOutputPath.Size = new System.Drawing.Size(350, 20);
            this.masterOutputPath.TabIndex = 2;
            this.masterOutputPath.Tag = "出力先を入力してください";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "マスタファイル出力先";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // transBrowse
            // 
            this.transBrowse.DefaultBackColor = System.Drawing.Color.Empty;
            this.transBrowse.Location = new System.Drawing.Point(524, 78);
            this.transBrowse.Name = "transBrowse";
            this.transBrowse.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.transBrowse.Size = new System.Drawing.Size(53, 22);
            this.transBrowse.TabIndex = 5;
            this.transBrowse.Tag = "ファイル出力先を指定します";
            this.transBrowse.Text = "参照";
            this.transBrowse.UseVisualStyleBackColor = true;
            // 
            // transOutputPath
            // 
            this.transOutputPath.BackColor = System.Drawing.SystemColors.Window;
            this.transOutputPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.transOutputPath.DBFieldsName = "";
            this.transOutputPath.DefaultBackColor = System.Drawing.Color.Empty;
            this.transOutputPath.DisplayItemName = "";
            this.transOutputPath.DisplayPopUp = null;
            this.transOutputPath.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("transOutputPath.FocusOutCheckMethod")));
            this.transOutputPath.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.transOutputPath.ForeColor = System.Drawing.Color.Black;
            this.transOutputPath.GetCodeMasterField = "";
            this.transOutputPath.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.transOutputPath.IsInputErrorOccured = false;
            this.transOutputPath.ItemDefinedTypes = "varchar";
            this.transOutputPath.Location = new System.Drawing.Point(167, 79);
            this.transOutputPath.Name = "transOutputPath";
            this.transOutputPath.PopupAfterExecute = null;
            this.transOutputPath.PopupBeforeExecute = null;
            this.transOutputPath.PopupGetMasterField = "";
            this.transOutputPath.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("transOutputPath.PopupSearchSendParams")));
            this.transOutputPath.PopupSendParams = new string[0];
            this.transOutputPath.PopupSetFormField = "";
            this.transOutputPath.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.transOutputPath.PopupWindowName = "";
            this.transOutputPath.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("transOutputPath.popupWindowSetting")));
            this.transOutputPath.prevText = null;
            this.transOutputPath.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("transOutputPath.RegistCheckMethod")));
            this.transOutputPath.SetFormField = "";
            this.transOutputPath.Size = new System.Drawing.Size(350, 20);
            this.transOutputPath.TabIndex = 4;
            this.transOutputPath.Tag = "出力先を入力してください";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(12, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "配車ファイル出力先";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // inputBrowse
            // 
            this.inputBrowse.DefaultBackColor = System.Drawing.Color.Empty;
            this.inputBrowse.Location = new System.Drawing.Point(524, 100);
            this.inputBrowse.Name = "inputBrowse";
            this.inputBrowse.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.inputBrowse.Size = new System.Drawing.Size(53, 22);
            this.inputBrowse.TabIndex = 7;
            this.inputBrowse.Tag = "ファイル取込先を指定します";
            this.inputBrowse.Text = "参照";
            this.inputBrowse.UseVisualStyleBackColor = true;
            // 
            // inputPath
            // 
            this.inputPath.BackColor = System.Drawing.SystemColors.Window;
            this.inputPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputPath.DBFieldsName = "";
            this.inputPath.DefaultBackColor = System.Drawing.Color.Empty;
            this.inputPath.DisplayItemName = "";
            this.inputPath.DisplayPopUp = null;
            this.inputPath.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("inputPath.FocusOutCheckMethod")));
            this.inputPath.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.inputPath.ForeColor = System.Drawing.Color.Black;
            this.inputPath.GetCodeMasterField = "";
            this.inputPath.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.inputPath.IsInputErrorOccured = false;
            this.inputPath.ItemDefinedTypes = "varchar";
            this.inputPath.Location = new System.Drawing.Point(167, 101);
            this.inputPath.Name = "inputPath";
            this.inputPath.PopupAfterExecute = null;
            this.inputPath.PopupBeforeExecute = null;
            this.inputPath.PopupGetMasterField = "";
            this.inputPath.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("inputPath.PopupSearchSendParams")));
            this.inputPath.PopupSendParams = new string[0];
            this.inputPath.PopupSetFormField = "";
            this.inputPath.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.inputPath.PopupWindowName = "";
            this.inputPath.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("inputPath.popupWindowSetting")));
            this.inputPath.prevText = null;
            this.inputPath.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("inputPath.RegistCheckMethod")));
            this.inputPath.SetFormField = "";
            this.inputPath.Size = new System.Drawing.Size(350, 20);
            this.inputPath.TabIndex = 6;
            this.inputPath.Tag = "取込先を入力してください";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(12, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(150, 20);
            this.label4.TabIndex = 13;
            this.label4.Text = "実績データ取込先";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backUpBrowse
            // 
            this.backUpBrowse.DefaultBackColor = System.Drawing.Color.Empty;
            this.backUpBrowse.Location = new System.Drawing.Point(524, 122);
            this.backUpBrowse.Name = "backUpBrowse";
            this.backUpBrowse.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.backUpBrowse.Size = new System.Drawing.Size(53, 22);
            this.backUpBrowse.TabIndex = 9;
            this.backUpBrowse.Tag = "ファイル保存先を指定します";
            this.backUpBrowse.Text = "参照";
            this.backUpBrowse.UseVisualStyleBackColor = true;
            // 
            // backUpPath
            // 
            this.backUpPath.BackColor = System.Drawing.SystemColors.Window;
            this.backUpPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.backUpPath.DBFieldsName = "";
            this.backUpPath.DefaultBackColor = System.Drawing.Color.Empty;
            this.backUpPath.DisplayItemName = "";
            this.backUpPath.DisplayPopUp = null;
            this.backUpPath.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("backUpPath.FocusOutCheckMethod")));
            this.backUpPath.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.backUpPath.ForeColor = System.Drawing.Color.Black;
            this.backUpPath.GetCodeMasterField = "";
            this.backUpPath.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.backUpPath.IsInputErrorOccured = false;
            this.backUpPath.ItemDefinedTypes = "varchar";
            this.backUpPath.Location = new System.Drawing.Point(167, 123);
            this.backUpPath.Name = "backUpPath";
            this.backUpPath.PopupAfterExecute = null;
            this.backUpPath.PopupBeforeExecute = null;
            this.backUpPath.PopupGetMasterField = "";
            this.backUpPath.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("backUpPath.PopupSearchSendParams")));
            this.backUpPath.PopupSendParams = new string[0];
            this.backUpPath.PopupSetFormField = "";
            this.backUpPath.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.backUpPath.PopupWindowName = "";
            this.backUpPath.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("backUpPath.popupWindowSetting")));
            this.backUpPath.prevText = null;
            this.backUpPath.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("backUpPath.RegistCheckMethod")));
            this.backUpPath.SetFormField = "";
            this.backUpPath.Size = new System.Drawing.Size(350, 20);
            this.backUpPath.TabIndex = 8;
            this.backUpPath.Tag = "保存先を入力してください";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 20);
            this.label5.TabIndex = 16;
            this.label5.Text = "バックアップ保存先";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TITLE_LABEL
            // 
            this.TITLE_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TITLE_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TITLE_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TITLE_LABEL.ForeColor = System.Drawing.Color.White;
            this.TITLE_LABEL.Location = new System.Drawing.Point(12, 9);
            this.TITLE_LABEL.Name = "TITLE_LABEL";
            this.TITLE_LABEL.Size = new System.Drawing.Size(364, 31);
            this.TITLE_LABEL.TabIndex = 381;
            this.TITLE_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func12.Location = new System.Drawing.Point(487, 150);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(90, 35);
            this.bt_func12.TabIndex = 383;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "画面を閉じます";
            this.bt_func12.Text = "[F12]\r\n閉じる";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // bt_func9
            // 
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_func9.Location = new System.Drawing.Point(391, 150);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(90, 35);
            this.bt_func9.TabIndex = 382;
            this.bt_func9.TabStop = false;
            this.bt_func9.Tag = "画面内容を設定ファイルへ登録します";
            this.bt_func9.Text = "[F9]\r\n保存";
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(584, 193);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.bt_func9);
            this.Controls.Add(this.TITLE_LABEL);
            this.Controls.Add(this.backUpBrowse);
            this.Controls.Add(this.backUpPath);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.inputBrowse);
            this.Controls.Add(this.inputPath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.transBrowse);
            this.Controls.Add(this.transOutputPath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.masterBrowse);
            this.Controls.Add(this.masterOutputPath);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public r_framework.CustomControl.CustomButton masterBrowse;
        public r_framework.CustomControl.CustomTextBox masterOutputPath;
        public System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomButton transBrowse;
        public r_framework.CustomControl.CustomTextBox transOutputPath;
        public System.Windows.Forms.Label label3;
        public r_framework.CustomControl.CustomButton inputBrowse;
        public r_framework.CustomControl.CustomTextBox inputPath;
        public System.Windows.Forms.Label label4;
        public r_framework.CustomControl.CustomButton backUpBrowse;
        public r_framework.CustomControl.CustomTextBox backUpPath;
        public System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label TITLE_LABEL;
        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomButton bt_func9;
    }
}