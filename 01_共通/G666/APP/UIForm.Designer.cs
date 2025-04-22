namespace Shougun.Core.Common.CtiRenkeiSettei
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
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.TITLE_LABEL = new System.Windows.Forms.Label();
            this.btnBrowse = new r_framework.CustomControl.CustomButton();
            this.radbtnUse2 = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnUse1 = new r_framework.CustomControl.CustomRadioButton();
            this.txtFilePath = new r_framework.CustomControl.CustomTextBox();
            this.txtUse = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.labelBANK = new System.Windows.Forms.Label();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFileDetectTime = new r_framework.CustomControl.CustomNumericTextBox2();
            this.bt_func1 = new r_framework.CustomControl.CustomButton();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
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
            this.TITLE_LABEL.TabIndex = 382;
            this.TITLE_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnBrowse
            // 
            this.btnBrowse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnBrowse.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnBrowse.Location = new System.Drawing.Point(528, 91);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnBrowse.Size = new System.Drawing.Size(53, 22);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Tag = "参照ファイル名を指定します";
            this.btnBrowse.Text = "参照";
            this.btnBrowse.UseVisualStyleBackColor = false;
            // 
            // radbtnUse2
            // 
            this.radbtnUse2.AutoSize = true;
            this.radbtnUse2.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnUse2.FocusOutCheckMethod = null;
            this.radbtnUse2.LinkedTextBox = "txtUse";
            this.radbtnUse2.Location = new System.Drawing.Point(85, 1);
            this.radbtnUse2.Name = "radbtnUse2";
            this.radbtnUse2.PopupAfterExecute = null;
            this.radbtnUse2.PopupBeforeExecute = null;
            this.radbtnUse2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnUse2.PopupSearchSendParams")));
            this.radbtnUse2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnUse2.popupWindowSetting = null;
            this.radbtnUse2.RegistCheckMethod = null;
            this.radbtnUse2.Size = new System.Drawing.Size(60, 16);
            this.radbtnUse2.TabIndex = 3;
            this.radbtnUse2.Tag = "CTI連携を入力してください \n1:する、2:しない";
            this.radbtnUse2.Text = "2.しない";
            this.radbtnUse2.UseVisualStyleBackColor = true;
            this.radbtnUse2.Value = "2";
            // 
            // radbtnUse1
            // 
            this.radbtnUse1.AutoSize = true;
            this.radbtnUse1.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnUse1.FocusOutCheckMethod = null;
            this.radbtnUse1.LinkedTextBox = "txtUse";
            this.radbtnUse1.Location = new System.Drawing.Point(29, 1);
            this.radbtnUse1.Name = "radbtnUse1";
            this.radbtnUse1.PopupAfterExecute = null;
            this.radbtnUse1.PopupBeforeExecute = null;
            this.radbtnUse1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnUse1.PopupSearchSendParams")));
            this.radbtnUse1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnUse1.popupWindowSetting = null;
            this.radbtnUse1.RegistCheckMethod = null;
            this.radbtnUse1.Size = new System.Drawing.Size(50, 16);
            this.radbtnUse1.TabIndex = 2;
            this.radbtnUse1.Tag = "CTI連携を入力してください \n1:する、2:しない";
            this.radbtnUse1.Text = "1.する";
            this.radbtnUse1.UseVisualStyleBackColor = true;
            this.radbtnUse1.Value = "1";
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePath.DBFieldsName = "";
            this.txtFilePath.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtFilePath.DisplayItemName = "";
            this.txtFilePath.DisplayPopUp = null;
            this.txtFilePath.FocusOutCheckMethod = null;
            this.txtFilePath.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtFilePath.ForeColor = System.Drawing.Color.Black;
            this.txtFilePath.GetCodeMasterField = "";
            this.txtFilePath.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtFilePath.IsInputErrorOccured = false;
            this.txtFilePath.ItemDefinedTypes = "varchar";
            this.txtFilePath.Location = new System.Drawing.Point(176, 92);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.PopupAfterExecute = null;
            this.txtFilePath.PopupBeforeExecute = null;
            this.txtFilePath.PopupGetMasterField = "";
            this.txtFilePath.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtFilePath.PopupSearchSendParams")));
            this.txtFilePath.PopupSendParams = new string[0];
            this.txtFilePath.PopupSetFormField = "";
            this.txtFilePath.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtFilePath.PopupWindowName = "";
            this.txtFilePath.popupWindowSetting = null;
            this.txtFilePath.RegistCheckMethod = null;
            this.txtFilePath.SetFormField = "";
            this.txtFilePath.Size = new System.Drawing.Size(350, 20);
            this.txtFilePath.TabIndex = 4;
            this.txtFilePath.Tag = "重要ファイル名を入力してください";
            // 
            // txtUse
            // 
            this.txtUse.AccessibleDescription = "CTI連携";
            this.txtUse.AccessibleName = "CTI連携";
            this.txtUse.BackColor = System.Drawing.SystemColors.Window;
            this.txtUse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUse.CharacterLimitList = new char[] {
        '1',
        '2'};
            this.txtUse.DBFieldsName = "";
            this.txtUse.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtUse.DisplayItemName = "";
            this.txtUse.DisplayPopUp = null;
            this.txtUse.FocusOutCheckMethod = null;
            this.txtUse.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtUse.ForeColor = System.Drawing.Color.Black;
            this.txtUse.GetCodeMasterField = "";
            this.txtUse.IsInputErrorOccured = false;
            this.txtUse.ItemDefinedTypes = "varchar";
            this.txtUse.LinkedRadioButtonArray = new string[] {
        "radbtnUse1",
        "radbtnUse2"};
            this.txtUse.Location = new System.Drawing.Point(-1, -1);
            this.txtUse.Multiline = true;
            this.txtUse.Name = "txtUse";
            this.txtUse.PopupAfterExecute = null;
            this.txtUse.PopupBeforeExecute = null;
            this.txtUse.PopupGetMasterField = "";
            this.txtUse.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtUse.PopupSearchSendParams")));
            this.txtUse.PopupSendParams = new string[0];
            this.txtUse.PopupSetFormField = "";
            this.txtUse.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtUse.PopupWindowName = "";
            this.txtUse.popupWindowSetting = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtUse.RangeSetting = rangeSettingDto1;
            this.txtUse.RegistCheckMethod = null;
            this.txtUse.SetFormField = "";
            this.txtUse.Size = new System.Drawing.Size(20, 20);
            this.txtUse.TabIndex = 1;
            this.txtUse.Tag = "CTI連携を入力してください \n1:する、2:しない";
            this.txtUse.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtUse.WordWrap = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(155, 20);
            this.label1.TabIndex = 397;
            this.label1.Text = "連動テキストファイル";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelBANK
            // 
            this.labelBANK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelBANK.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelBANK.ForeColor = System.Drawing.Color.White;
            this.labelBANK.Location = new System.Drawing.Point(12, 68);
            this.labelBANK.Name = "labelBANK";
            this.labelBANK.Size = new System.Drawing.Size(155, 20);
            this.labelBANK.TabIndex = 393;
            this.labelBANK.Text = "CTI連携";
            this.labelBANK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func12.Location = new System.Drawing.Point(490, 180);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(90, 35);
            this.bt_func12.TabIndex = 9;
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
            this.bt_func9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func9.Location = new System.Drawing.Point(390, 180);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(90, 35);
            this.bt_func9.TabIndex = 8;
            this.bt_func9.TabStop = false;
            this.bt_func9.Tag = "保存を実行します";
            this.bt_func9.Text = "[F9]\r\n保存";
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.txtUse);
            this.customPanel1.Controls.Add(this.radbtnUse1);
            this.customPanel1.Controls.Add(this.radbtnUse2);
            this.customPanel1.Location = new System.Drawing.Point(176, 68);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(214, 20);
            this.customPanel1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(155, 20);
            this.label2.TabIndex = 399;
            this.label2.Text = "ファイル検出間隔";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(239, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 12);
            this.label3.TabIndex = 401;
            this.label3.Text = "msec";
            // 
            // txtFileDetectTime
            // 
            this.txtFileDetectTime.BackColor = System.Drawing.SystemColors.Window;
            this.txtFileDetectTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileDetectTime.CustomFormatSetting = "#,###";
            this.txtFileDetectTime.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtFileDetectTime.DisplayPopUp = null;
            this.txtFileDetectTime.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtFileDetectTime.FocusOutCheckMethod")));
            this.txtFileDetectTime.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtFileDetectTime.ForeColor = System.Drawing.Color.Black;
            this.txtFileDetectTime.FormatSetting = "カスタム";
            this.txtFileDetectTime.IsInputErrorOccured = false;
            this.txtFileDetectTime.ItemDefinedTypes = "smallint";
            this.txtFileDetectTime.Location = new System.Drawing.Point(176, 116);
            this.txtFileDetectTime.Name = "txtFileDetectTime";
            this.txtFileDetectTime.PopupAfterExecute = null;
            this.txtFileDetectTime.PopupBeforeExecute = null;
            this.txtFileDetectTime.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtFileDetectTime.PopupSearchSendParams")));
            this.txtFileDetectTime.PopupSendParams = new string[0];
            this.txtFileDetectTime.PopupSetFormField = "";
            this.txtFileDetectTime.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtFileDetectTime.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtFileDetectTime.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtFileDetectTime.RangeSetting = rangeSettingDto2;
            this.txtFileDetectTime.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtFileDetectTime.RegistCheckMethod")));
            this.txtFileDetectTime.SetFormField = "";
            this.txtFileDetectTime.Size = new System.Drawing.Size(60, 20);
            this.txtFileDetectTime.TabIndex = 6;
            this.txtFileDetectTime.Tag = "ファイル検出間隔を入力してください";
            this.txtFileDetectTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFileDetectTime.WordWrap = false;
            // 
            // bt_func1
            // 
            this.bt_func1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func1.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func1.Location = new System.Drawing.Point(12, 180);
            this.bt_func1.Name = "bt_func1";
            this.bt_func1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func1.Size = new System.Drawing.Size(90, 35);
            this.bt_func1.TabIndex = 7;
            this.bt_func1.TabStop = false;
            this.bt_func1.Tag = "DBﾒﾝﾃを実行します";
            this.bt_func1.Text = "[F1]\r\nDBﾒﾝﾃ";
            this.bt_func1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func1.UseVisualStyleBackColor = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(590, 221);
            this.Controls.Add(this.bt_func1);
            this.Controls.Add(this.txtFileDetectTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.bt_func9);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelBANK);
            this.Controls.Add(this.TITLE_LABEL);
            this.Controls.Add(this.customPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UIForm";
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label TITLE_LABEL;
        public r_framework.CustomControl.CustomButton btnBrowse;
        private r_framework.CustomControl.CustomRadioButton radbtnUse2;
        private r_framework.CustomControl.CustomRadioButton radbtnUse1;
        public r_framework.CustomControl.CustomTextBox txtFilePath;
        public r_framework.CustomControl.CustomNumericTextBox2 txtUse;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label labelBANK;
        internal r_framework.CustomControl.CustomButton bt_func12;
        internal r_framework.CustomControl.CustomButton bt_func9;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public r_framework.CustomControl.CustomNumericTextBox2 txtFileDetectTime;
        internal r_framework.CustomControl.CustomButton bt_func1;
    }
}