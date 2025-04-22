namespace Shougun.Core.Common.TruckScaleTsuushin
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
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto6 = new r_framework.Dto.RangeSettingDto();
            this.txtUse = new r_framework.CustomControl.CustomNumericTextBox2();
            this.labelBANK = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtFilePath = new r_framework.CustomControl.CustomTextBox();
            this.radbtnUse1 = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnUse2 = new r_framework.CustomControl.CustomRadioButton();
            this.btnBrowse = new r_framework.CustomControl.CustomButton();
            this.txtFileNoReactTime = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtFileDetectTime = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtSTWeightCount = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtDetectAllowWeight = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radbtnSwitch2 = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnSwitch1 = new r_framework.CustomControl.CustomRadioButton();
            this.txtWeightDisplaySwitch = new r_framework.CustomControl.CustomNumericTextBox2();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtUse
            // 
            this.txtUse.BackColor = System.Drawing.SystemColors.Window;
            this.txtUse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtUse.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtUse.DisplayPopUp = null;
            this.txtUse.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtUse.FocusOutCheckMethod")));
            this.txtUse.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtUse.ForeColor = System.Drawing.Color.Black;
            this.txtUse.IsInputErrorOccured = false;
            this.txtUse.ItemDefinedTypes = "varchar";
            this.txtUse.LinkedRadioButtonArray = new string[] {
        "radbtnUse1",
        "radbtnUse2"};
            this.txtUse.Location = new System.Drawing.Point(131, 2);
            this.txtUse.Name = "txtUse";
            this.txtUse.PopupAfterExecute = null;
            this.txtUse.PopupBeforeExecute = null;
            this.txtUse.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtUse.PopupSearchSendParams")));
            this.txtUse.PopupSendParams = new string[0];
            this.txtUse.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtUse.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtUse.popupWindowSetting")));
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
            this.txtUse.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtUse.RegistCheckMethod")));
            this.txtUse.Size = new System.Drawing.Size(40, 20);
            this.txtUse.TabIndex = 1;
            this.txtUse.Tag = "重量値取込みを入力してください \n1:通信する、2:通信しない";
            this.txtUse.WordWrap = false;
            this.txtUse.TextChanged += new System.EventHandler(this.txtUse_TextChanged);
            // 
            // labelBANK
            // 
            this.labelBANK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelBANK.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.labelBANK.ForeColor = System.Drawing.Color.White;
            this.labelBANK.Location = new System.Drawing.Point(1, 2);
            this.labelBANK.Name = "labelBANK";
            this.labelBANK.Size = new System.Drawing.Size(125, 20);
            this.labelBANK.TabIndex = 0;
            this.labelBANK.Text = "重量値取込み";
            this.labelBANK.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(125, 20);
            this.label1.TabIndex = 8;
            this.label1.Text = "重量ファイル名";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(125, 20);
            this.label2.TabIndex = 14;
            this.label2.Text = "ファイル検出間隔";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(1, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 20);
            this.label3.TabIndex = 11;
            this.label3.Text = "無反応検出間隔";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(1, 134);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 20);
            this.label4.TabIndex = 20;
            this.label4.Text = "安定重量回数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(1, 112);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 20);
            this.label5.TabIndex = 17;
            this.label5.Text = "検出重量";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePath.DBFieldsName = "";
            this.txtFilePath.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtFilePath.DisplayItemName = "";
            this.txtFilePath.DisplayPopUp = null;
            this.txtFilePath.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtFilePath.FocusOutCheckMethod")));
            this.txtFilePath.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtFilePath.ForeColor = System.Drawing.Color.Black;
            this.txtFilePath.GetCodeMasterField = "";
            this.txtFilePath.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtFilePath.IsInputErrorOccured = false;
            this.txtFilePath.ItemDefinedTypes = "varchar";
            this.txtFilePath.Location = new System.Drawing.Point(131, 46);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.PopupAfterExecute = null;
            this.txtFilePath.PopupBeforeExecute = null;
            this.txtFilePath.PopupGetMasterField = "";
            this.txtFilePath.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtFilePath.PopupSearchSendParams")));
            this.txtFilePath.PopupSendParams = new string[0];
            this.txtFilePath.PopupSetFormField = "";
            this.txtFilePath.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtFilePath.PopupWindowName = "";
            this.txtFilePath.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtFilePath.popupWindowSetting")));
            this.txtFilePath.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtFilePath.RegistCheckMethod")));
            this.txtFilePath.SetFormField = "";
            this.txtFilePath.Size = new System.Drawing.Size(401, 20);
            this.txtFilePath.TabIndex = 9;
            this.txtFilePath.Tag = "重要ファイル名を入力してください";
            // 
            // radbtnUse1
            // 
            this.radbtnUse1.AutoSize = true;
            this.radbtnUse1.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnUse1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnUse1.FocusOutCheckMethod")));
            this.radbtnUse1.LinkedTextBox = "txtUse";
            this.radbtnUse1.Location = new System.Drawing.Point(177, 5);
            this.radbtnUse1.Name = "radbtnUse1";
            this.radbtnUse1.PopupAfterExecute = null;
            this.radbtnUse1.PopupBeforeExecute = null;
            this.radbtnUse1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnUse1.PopupSearchSendParams")));
            this.radbtnUse1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnUse1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnUse1.popupWindowSetting")));
            this.radbtnUse1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnUse1.RegistCheckMethod")));
            this.radbtnUse1.Size = new System.Drawing.Size(74, 16);
            this.radbtnUse1.TabIndex = 2;
            this.radbtnUse1.Tag = "重量値取込みを入力してください \n1:通信する、2:通信しない";
            this.radbtnUse1.Text = "1.通信する";
            this.radbtnUse1.UseVisualStyleBackColor = true;
            this.radbtnUse1.Value = "1";
            this.radbtnUse1.Click += new System.EventHandler(this.radbtnUse2_Click);
            // 
            // radbtnUse2
            // 
            this.radbtnUse2.AutoSize = true;
            this.radbtnUse2.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnUse2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnUse2.FocusOutCheckMethod")));
            this.radbtnUse2.LinkedTextBox = "txtUse";
            this.radbtnUse2.Location = new System.Drawing.Point(259, 5);
            this.radbtnUse2.Name = "radbtnUse2";
            this.radbtnUse2.PopupAfterExecute = null;
            this.radbtnUse2.PopupBeforeExecute = null;
            this.radbtnUse2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnUse2.PopupSearchSendParams")));
            this.radbtnUse2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnUse2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnUse2.popupWindowSetting")));
            this.radbtnUse2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnUse2.RegistCheckMethod")));
            this.radbtnUse2.Size = new System.Drawing.Size(84, 16);
            this.radbtnUse2.TabIndex = 3;
            this.radbtnUse2.Tag = "重量値取込みを入力してください \n1:通信する、2:通信しない";
            this.radbtnUse2.Text = "2.通信しない";
            this.radbtnUse2.UseVisualStyleBackColor = true;
            this.radbtnUse2.Value = "2";
            this.radbtnUse2.Click += new System.EventHandler(this.radbtnUse2_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnBrowse.Location = new System.Drawing.Point(539, 45);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnBrowse.Size = new System.Drawing.Size(53, 22);
            this.btnBrowse.TabIndex = 10;
            this.btnBrowse.Tag = "参照ファイル名を指定します";
            this.btnBrowse.Text = "参照";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtFileNoReactTime
            // 
            this.txtFileNoReactTime.BackColor = System.Drawing.SystemColors.Window;
            this.txtFileNoReactTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFileNoReactTime.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtFileNoReactTime.DisplayPopUp = null;
            this.txtFileNoReactTime.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtFileNoReactTime.FocusOutCheckMethod")));
            this.txtFileNoReactTime.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtFileNoReactTime.ForeColor = System.Drawing.Color.Black;
            this.txtFileNoReactTime.IsInputErrorOccured = false;
            this.txtFileNoReactTime.ItemDefinedTypes = "smallint";
            this.txtFileNoReactTime.Location = new System.Drawing.Point(131, 68);
            this.txtFileNoReactTime.Name = "txtFileNoReactTime";
            this.txtFileNoReactTime.PopupAfterExecute = null;
            this.txtFileNoReactTime.PopupBeforeExecute = null;
            this.txtFileNoReactTime.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtFileNoReactTime.PopupSearchSendParams")));
            this.txtFileNoReactTime.PopupSendParams = new string[0];
            this.txtFileNoReactTime.PopupSetFormField = "";
            this.txtFileNoReactTime.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtFileNoReactTime.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtFileNoReactTime.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txtFileNoReactTime.RangeSetting = rangeSettingDto2;
            this.txtFileNoReactTime.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtFileNoReactTime.RegistCheckMethod")));
            this.txtFileNoReactTime.SetFormField = "";
            this.txtFileNoReactTime.Size = new System.Drawing.Size(60, 20);
            this.txtFileNoReactTime.TabIndex = 12;
            this.txtFileNoReactTime.Tag = "無反応検出間隔を入力してください";
            this.txtFileNoReactTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFileNoReactTime.WordWrap = false;
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
            this.txtFileDetectTime.Location = new System.Drawing.Point(131, 90);
            this.txtFileDetectTime.Name = "txtFileDetectTime";
            this.txtFileDetectTime.PopupAfterExecute = null;
            this.txtFileDetectTime.PopupBeforeExecute = null;
            this.txtFileDetectTime.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtFileDetectTime.PopupSearchSendParams")));
            this.txtFileDetectTime.PopupSendParams = new string[0];
            this.txtFileDetectTime.PopupSetFormField = "";
            this.txtFileDetectTime.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtFileDetectTime.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtFileDetectTime.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtFileDetectTime.RangeSetting = rangeSettingDto3;
            this.txtFileDetectTime.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtFileDetectTime.RegistCheckMethod")));
            this.txtFileDetectTime.SetFormField = "";
            this.txtFileDetectTime.Size = new System.Drawing.Size(60, 20);
            this.txtFileDetectTime.TabIndex = 15;
            this.txtFileDetectTime.Tag = "ファイル検出間隔を入力してください";
            this.txtFileDetectTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtFileDetectTime.WordWrap = false;
            // 
            // txtSTWeightCount
            // 
            this.txtSTWeightCount.BackColor = System.Drawing.SystemColors.Window;
            this.txtSTWeightCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSTWeightCount.DBFieldsName = "";
            this.txtSTWeightCount.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtSTWeightCount.DisplayItemName = "";
            this.txtSTWeightCount.DisplayPopUp = null;
            this.txtSTWeightCount.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSTWeightCount.FocusOutCheckMethod")));
            this.txtSTWeightCount.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtSTWeightCount.ForeColor = System.Drawing.Color.Black;
            this.txtSTWeightCount.GetCodeMasterField = "";
            this.txtSTWeightCount.IsInputErrorOccured = false;
            this.txtSTWeightCount.ItemDefinedTypes = "smallint";
            this.txtSTWeightCount.Location = new System.Drawing.Point(131, 134);
            this.txtSTWeightCount.Name = "txtSTWeightCount";
            this.txtSTWeightCount.PopupAfterExecute = null;
            this.txtSTWeightCount.PopupBeforeExecute = null;
            this.txtSTWeightCount.PopupGetMasterField = "";
            this.txtSTWeightCount.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtSTWeightCount.PopupSearchSendParams")));
            this.txtSTWeightCount.PopupSendParams = new string[0];
            this.txtSTWeightCount.PopupSetFormField = "";
            this.txtSTWeightCount.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtSTWeightCount.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtSTWeightCount.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.txtSTWeightCount.RangeSetting = rangeSettingDto4;
            this.txtSTWeightCount.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSTWeightCount.RegistCheckMethod")));
            this.txtSTWeightCount.SetFormField = "";
            this.txtSTWeightCount.Size = new System.Drawing.Size(60, 20);
            this.txtSTWeightCount.TabIndex = 21;
            this.txtSTWeightCount.Tag = "安定重量回数を入力してください";
            this.txtSTWeightCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtSTWeightCount.WordWrap = false;
            // 
            // txtDetectAllowWeight
            // 
            this.txtDetectAllowWeight.BackColor = System.Drawing.SystemColors.Window;
            this.txtDetectAllowWeight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDetectAllowWeight.CustomFormatSetting = "#,###";
            this.txtDetectAllowWeight.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtDetectAllowWeight.DisplayPopUp = null;
            this.txtDetectAllowWeight.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtDetectAllowWeight.FocusOutCheckMethod")));
            this.txtDetectAllowWeight.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtDetectAllowWeight.ForeColor = System.Drawing.Color.Black;
            this.txtDetectAllowWeight.FormatSetting = "カスタム";
            this.txtDetectAllowWeight.IsInputErrorOccured = false;
            this.txtDetectAllowWeight.ItemDefinedTypes = "decimal";
            this.txtDetectAllowWeight.Location = new System.Drawing.Point(131, 112);
            this.txtDetectAllowWeight.Name = "txtDetectAllowWeight";
            this.txtDetectAllowWeight.PopupAfterExecute = null;
            this.txtDetectAllowWeight.PopupBeforeExecute = null;
            this.txtDetectAllowWeight.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtDetectAllowWeight.PopupSearchSendParams")));
            this.txtDetectAllowWeight.PopupSendParams = new string[0];
            this.txtDetectAllowWeight.PopupSetFormField = "";
            this.txtDetectAllowWeight.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtDetectAllowWeight.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtDetectAllowWeight.popupWindowSetting")));
            rangeSettingDto5.Max = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.txtDetectAllowWeight.RangeSetting = rangeSettingDto5;
            this.txtDetectAllowWeight.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtDetectAllowWeight.RegistCheckMethod")));
            this.txtDetectAllowWeight.SetFormField = "";
            this.txtDetectAllowWeight.Size = new System.Drawing.Size(60, 20);
            this.txtDetectAllowWeight.TabIndex = 18;
            this.txtDetectAllowWeight.Tag = "検出重量を入力してください";
            this.txtDetectAllowWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDetectAllowWeight.WordWrap = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(198, 73);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "秒";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(198, 93);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(32, 12);
            this.label7.TabIndex = 16;
            this.label7.Text = "msec";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(197, 115);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(18, 12);
            this.label8.TabIndex = 19;
            this.label8.Text = "Kg";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(197, 138);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 22;
            this.label9.Text = "回";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(1, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(125, 20);
            this.label10.TabIndex = 4;
            this.label10.Text = "自動手動重量表示";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.radbtnSwitch2);
            this.panel1.Controls.Add(this.radbtnSwitch1);
            this.panel1.Controls.Add(this.txtWeightDisplaySwitch);
            this.panel1.Location = new System.Drawing.Point(129, 23);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(273, 23);
            this.panel1.TabIndex = 72;
            // 
            // radbtnSwitch2
            // 
            this.radbtnSwitch2.AutoSize = true;
            this.radbtnSwitch2.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnSwitch2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSwitch2.FocusOutCheckMethod")));
            this.radbtnSwitch2.LinkedTextBox = "txtWeightDisplaySwitch";
            this.radbtnSwitch2.Location = new System.Drawing.Point(130, 4);
            this.radbtnSwitch2.Name = "radbtnSwitch2";
            this.radbtnSwitch2.PopupAfterExecute = null;
            this.radbtnSwitch2.PopupBeforeExecute = null;
            this.radbtnSwitch2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnSwitch2.PopupSearchSendParams")));
            this.radbtnSwitch2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnSwitch2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnSwitch2.popupWindowSetting")));
            this.radbtnSwitch2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSwitch2.RegistCheckMethod")));
            this.radbtnSwitch2.Size = new System.Drawing.Size(79, 16);
            this.radbtnSwitch2.TabIndex = 10;
            this.radbtnSwitch2.Tag = "自動手動重量表示を入力してください \n1:自動表示、2:手動表示";
            this.radbtnSwitch2.Text = "2.手動表示";
            this.radbtnSwitch2.UseVisualStyleBackColor = true;
            this.radbtnSwitch2.Value = "2";
            // 
            // radbtnSwitch1
            // 
            this.radbtnSwitch1.AutoSize = true;
            this.radbtnSwitch1.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnSwitch1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSwitch1.FocusOutCheckMethod")));
            this.radbtnSwitch1.LinkedTextBox = "txtWeightDisplaySwitch";
            this.radbtnSwitch1.Location = new System.Drawing.Point(48, 4);
            this.radbtnSwitch1.Name = "radbtnSwitch1";
            this.radbtnSwitch1.PopupAfterExecute = null;
            this.radbtnSwitch1.PopupBeforeExecute = null;
            this.radbtnSwitch1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnSwitch1.PopupSearchSendParams")));
            this.radbtnSwitch1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnSwitch1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnSwitch1.popupWindowSetting")));
            this.radbtnSwitch1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSwitch1.RegistCheckMethod")));
            this.radbtnSwitch1.Size = new System.Drawing.Size(79, 16);
            this.radbtnSwitch1.TabIndex = 9;
            this.radbtnSwitch1.Tag = "自動手動重量表示を入力してください \n1:自動表示、2:手動表示";
            this.radbtnSwitch1.Text = "1.自動表示";
            this.radbtnSwitch1.UseVisualStyleBackColor = true;
            this.radbtnSwitch1.Value = "1";
            // 
            // txtWeightDisplaySwitch
            // 
            this.txtWeightDisplaySwitch.BackColor = System.Drawing.SystemColors.Window;
            this.txtWeightDisplaySwitch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtWeightDisplaySwitch.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtWeightDisplaySwitch.DisplayItemName = "自動手動重量表示";
            this.txtWeightDisplaySwitch.DisplayPopUp = null;
            this.txtWeightDisplaySwitch.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtWeightDisplaySwitch.FocusOutCheckMethod")));
            this.txtWeightDisplaySwitch.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtWeightDisplaySwitch.ForeColor = System.Drawing.Color.Black;
            this.txtWeightDisplaySwitch.IsInputErrorOccured = false;
            this.txtWeightDisplaySwitch.ItemDefinedTypes = "";
            this.txtWeightDisplaySwitch.LinkedRadioButtonArray = new string[] {
        "radbtnSwitch1",
        "radbtnSwitch2"};
            this.txtWeightDisplaySwitch.Location = new System.Drawing.Point(2, 1);
            this.txtWeightDisplaySwitch.Name = "txtWeightDisplaySwitch";
            this.txtWeightDisplaySwitch.PopupAfterExecute = null;
            this.txtWeightDisplaySwitch.PopupBeforeExecute = null;
            this.txtWeightDisplaySwitch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtWeightDisplaySwitch.PopupSearchSendParams")));
            this.txtWeightDisplaySwitch.PopupSendParams = new string[0];
            this.txtWeightDisplaySwitch.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtWeightDisplaySwitch.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtWeightDisplaySwitch.popupWindowSetting")));
            rangeSettingDto6.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto6.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtWeightDisplaySwitch.RangeSetting = rangeSettingDto6;
            this.txtWeightDisplaySwitch.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtWeightDisplaySwitch.RegistCheckMethod")));
            this.txtWeightDisplaySwitch.ShortItemName = "自動手動重量表示";
            this.txtWeightDisplaySwitch.Size = new System.Drawing.Size(40, 20);
            this.txtWeightDisplaySwitch.TabIndex = 8;
            this.txtWeightDisplaySwitch.Tag = "自動手動重量表示を入力してください \n1:自動表示、2:手動表示";
            this.txtWeightDisplaySwitch.WordWrap = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtSTWeightCount);
            this.Controls.Add(this.txtDetectAllowWeight);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.radbtnUse2);
            this.Controls.Add(this.radbtnUse1);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFileDetectTime);
            this.Controls.Add(this.txtFileNoReactTime);
            this.Controls.Add(this.txtUse);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelBANK);
            this.Controls.Add(this.panel1);
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UIForm";
            this.Load += new System.EventHandler(this.Form_Load);
            this.Shown += new System.EventHandler(this.Form_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label labelBANK;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label3;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.Label label5;
        public r_framework.CustomControl.CustomTextBox txtFilePath;
        public r_framework.CustomControl.CustomNumericTextBox2 txtUse;
        private r_framework.CustomControl.CustomRadioButton radbtnUse1;
        private r_framework.CustomControl.CustomRadioButton radbtnUse2;
        private r_framework.CustomControl.CustomButton btnBrowse;
        public r_framework.CustomControl.CustomNumericTextBox2 txtFileNoReactTime;
        public r_framework.CustomControl.CustomNumericTextBox2 txtFileDetectTime;
        public r_framework.CustomControl.CustomNumericTextBox2 txtSTWeightCount;
        public r_framework.CustomControl.CustomNumericTextBox2 txtDetectAllowWeight;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel1;
        private r_framework.CustomControl.CustomRadioButton radbtnSwitch2;
        private r_framework.CustomControl.CustomRadioButton radbtnSwitch1;
        public r_framework.CustomControl.CustomNumericTextBox2 txtWeightDisplaySwitch;
    }
}