namespace Shougun.Core.ExternalConnection.SmsNyuuryoku
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            this.HASSEIMOTO_LABEL = new System.Windows.Forms.Label();
            this.UPDATE_USER_LABEL = new System.Windows.Forms.Label();
            this.SAGYOU_DATE = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GENCHAKU_TIME = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DENPYOU_SHURUI = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.rdBtnSyuusyuu = new r_framework.CustomControl.CustomRadioButton();
            this.rdBtnKuremu = new r_framework.CustomControl.CustomRadioButton();
            this.rdBtnSyukka = new r_framework.CustomControl.CustomRadioButton();
            this.rdBtnMotikomi = new r_framework.CustomControl.CustomRadioButton();
            this.DENPYOU_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.COURSE_NAME_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.COURSE_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.ROW_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label4 = new System.Windows.Forms.Label();
            this.btnClearSubject = new r_framework.CustomControl.CustomButton();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.rdBtnDisplay = new r_framework.CustomControl.CustomRadioButton();
            this.rdBtnHide = new r_framework.CustomControl.CustomRadioButton();
            this.RECEIVER_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnClearGrettings = new r_framework.CustomControl.CustomButton();
            this.btnClearText1 = new r_framework.CustomControl.CustomButton();
            this.label7 = new System.Windows.Forms.Label();
            this.btnClearText2 = new r_framework.CustomControl.CustomButton();
            this.label8 = new System.Windows.Forms.Label();
            this.btnClearText3 = new r_framework.CustomControl.CustomButton();
            this.label9 = new System.Windows.Forms.Label();
            this.btnClearText4 = new r_framework.CustomControl.CustomButton();
            this.label10 = new System.Windows.Forms.Label();
            this.btnClearSignature = new r_framework.CustomControl.CustomButton();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.GENBA_ADDRESS1 = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_ADDRESS2 = new r_framework.CustomControl.CustomTextBox();
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.receiverName = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.mobilePhoneNumber = new r_framework.CustomControl.DataGridCustomControl.DgvCustomPhoneNumberTextBoxColumn();
            this.sendFlg = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.label15 = new System.Windows.Forms.Label();
            this.EIGYOU_TANTOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.EIGYOU_TANTOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.UNPAN_GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.UNPAN_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.SHASHU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SHASHU_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.SHARYOU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SHARYOU_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.dgvCustomTextBoxColumn1 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomPhoneNumberTextBoxColumn1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomPhoneNumberTextBoxColumn();
            this.dgvCustomCheckBoxColumn1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.SUBJECT = new System.Windows.Forms.TextBox();
            this.GREETINGS = new System.Windows.Forms.TextBox();
            this.TEXT1 = new System.Windows.Forms.TextBox();
            this.TEXT2 = new System.Windows.Forms.TextBox();
            this.TEXT3 = new System.Windows.Forms.TextBox();
            this.TEXT4 = new System.Windows.Forms.TextBox();
            this.SIGNATURE = new System.Windows.Forms.TextBox();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // HASSEIMOTO_LABEL
            // 
            this.HASSEIMOTO_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.HASSEIMOTO_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HASSEIMOTO_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.HASSEIMOTO_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HASSEIMOTO_LABEL.ForeColor = System.Drawing.Color.White;
            this.HASSEIMOTO_LABEL.Location = new System.Drawing.Point(483, 9);
            this.HASSEIMOTO_LABEL.Name = "HASSEIMOTO_LABEL";
            this.HASSEIMOTO_LABEL.Size = new System.Drawing.Size(79, 22);
            this.HASSEIMOTO_LABEL.TabIndex = 772;
            this.HASSEIMOTO_LABEL.Text = "伝票種類";
            this.HASSEIMOTO_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UPDATE_USER_LABEL
            // 
            this.UPDATE_USER_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.UPDATE_USER_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UPDATE_USER_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UPDATE_USER_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UPDATE_USER_LABEL.ForeColor = System.Drawing.Color.White;
            this.UPDATE_USER_LABEL.Location = new System.Drawing.Point(483, 34);
            this.UPDATE_USER_LABEL.Name = "UPDATE_USER_LABEL";
            this.UPDATE_USER_LABEL.Size = new System.Drawing.Size(79, 22);
            this.UPDATE_USER_LABEL.TabIndex = 784;
            this.UPDATE_USER_LABEL.Text = "伝票番号";
            this.UPDATE_USER_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SAGYOU_DATE
            // 
            this.SAGYOU_DATE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SAGYOU_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAGYOU_DATE.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.SAGYOU_DATE.DBFieldsName = "";
            this.SAGYOU_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAGYOU_DATE.DisplayPopUp = null;
            this.SAGYOU_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAGYOU_DATE.FocusOutCheckMethod")));
            this.SAGYOU_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SAGYOU_DATE.ForeColor = System.Drawing.Color.Black;
            this.SAGYOU_DATE.IsInputErrorOccured = false;
            this.SAGYOU_DATE.Location = new System.Drawing.Point(111, 9);
            this.SAGYOU_DATE.MaxLength = 0;
            this.SAGYOU_DATE.Multiline = true;
            this.SAGYOU_DATE.Name = "SAGYOU_DATE";
            this.SAGYOU_DATE.PopupAfterExecute = null;
            this.SAGYOU_DATE.PopupBeforeExecute = null;
            this.SAGYOU_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAGYOU_DATE.PopupSearchSendParams")));
            this.SAGYOU_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAGYOU_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAGYOU_DATE.popupWindowSetting")));
            this.SAGYOU_DATE.ReadOnly = true;
            this.SAGYOU_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAGYOU_DATE.RegistCheckMethod")));
            this.SAGYOU_DATE.Size = new System.Drawing.Size(110, 22);
            this.SAGYOU_DATE.TabIndex = 932;
            this.SAGYOU_DATE.TabStop = false;
            this.SAGYOU_DATE.Tag = " ";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(8, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 22);
            this.label1.TabIndex = 931;
            this.label1.Text = "作業日";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENCHAKU_TIME
            // 
            this.GENCHAKU_TIME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENCHAKU_TIME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENCHAKU_TIME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GENCHAKU_TIME.DBFieldsName = "";
            this.GENCHAKU_TIME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENCHAKU_TIME.DisplayPopUp = null;
            this.GENCHAKU_TIME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENCHAKU_TIME.FocusOutCheckMethod")));
            this.GENCHAKU_TIME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENCHAKU_TIME.ForeColor = System.Drawing.Color.Black;
            this.GENCHAKU_TIME.IsInputErrorOccured = false;
            this.GENCHAKU_TIME.Location = new System.Drawing.Point(330, 8);
            this.GENCHAKU_TIME.MaxLength = 0;
            this.GENCHAKU_TIME.Multiline = true;
            this.GENCHAKU_TIME.Name = "GENCHAKU_TIME";
            this.GENCHAKU_TIME.PopupAfterExecute = null;
            this.GENCHAKU_TIME.PopupBeforeExecute = null;
            this.GENCHAKU_TIME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENCHAKU_TIME.PopupSearchSendParams")));
            this.GENCHAKU_TIME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENCHAKU_TIME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENCHAKU_TIME.popupWindowSetting")));
            this.GENCHAKU_TIME.ReadOnly = true;
            this.GENCHAKU_TIME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENCHAKU_TIME.RegistCheckMethod")));
            this.GENCHAKU_TIME.Size = new System.Drawing.Size(79, 22);
            this.GENCHAKU_TIME.TabIndex = 934;
            this.GENCHAKU_TIME.TabStop = false;
            this.GENCHAKU_TIME.Tag = " ";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(227, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 22);
            this.label2.TabIndex = 933;
            this.label2.Text = "作業時間";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DENPYOU_SHURUI
            // 
            this.DENPYOU_SHURUI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.DENPYOU_SHURUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENPYOU_SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_SHURUI.DisplayPopUp = null;
            this.DENPYOU_SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_SHURUI.FocusOutCheckMethod")));
            this.DENPYOU_SHURUI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DENPYOU_SHURUI.ForeColor = System.Drawing.Color.Black;
            this.DENPYOU_SHURUI.IsInputErrorOccured = false;
            this.DENPYOU_SHURUI.LinkedRadioButtonArray = new string[] {
        "rdBtnSyuusyuu",
        "rdBtnSyukka",
        "rdBtnMotikomi",
        "rdBtnKuremu"};
            this.DENPYOU_SHURUI.Location = new System.Drawing.Point(565, 9);
            this.DENPYOU_SHURUI.Multiline = true;
            this.DENPYOU_SHURUI.Name = "DENPYOU_SHURUI";
            this.DENPYOU_SHURUI.PopupAfterExecute = null;
            this.DENPYOU_SHURUI.PopupBeforeExecute = null;
            this.DENPYOU_SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_SHURUI.PopupSearchSendParams")));
            this.DENPYOU_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_SHURUI.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            6,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DENPYOU_SHURUI.RangeSetting = rangeSettingDto1;
            this.DENPYOU_SHURUI.ReadOnly = true;
            this.DENPYOU_SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_SHURUI.RegistCheckMethod")));
            this.DENPYOU_SHURUI.Size = new System.Drawing.Size(22, 22);
            this.DENPYOU_SHURUI.TabIndex = 935;
            this.DENPYOU_SHURUI.TabStop = false;
            this.DENPYOU_SHURUI.Tag = "【1～4】のいずれかで入力して下さい";
            this.DENPYOU_SHURUI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DENPYOU_SHURUI.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.customPanel1.Controls.Add(this.rdBtnSyuusyuu);
            this.customPanel1.Controls.Add(this.rdBtnKuremu);
            this.customPanel1.Controls.Add(this.rdBtnSyukka);
            this.customPanel1.Controls.Add(this.rdBtnMotikomi);
            this.customPanel1.ForeColor = System.Drawing.Color.Black;
            this.customPanel1.Location = new System.Drawing.Point(593, 10);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(350, 20);
            this.customPanel1.TabIndex = 936;
            // 
            // rdBtnSyuusyuu
            // 
            this.rdBtnSyuusyuu.AutoSize = true;
            this.rdBtnSyuusyuu.Checked = true;
            this.rdBtnSyuusyuu.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdBtnSyuusyuu.Enabled = false;
            this.rdBtnSyuusyuu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnSyuusyuu.FocusOutCheckMethod")));
            this.rdBtnSyuusyuu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdBtnSyuusyuu.LinkedTextBox = "DENPYOU_SHURUI";
            this.rdBtnSyuusyuu.Location = new System.Drawing.Point(3, 2);
            this.rdBtnSyuusyuu.Name = "rdBtnSyuusyuu";
            this.rdBtnSyuusyuu.PopupAfterExecute = null;
            this.rdBtnSyuusyuu.PopupBeforeExecute = null;
            this.rdBtnSyuusyuu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdBtnSyuusyuu.PopupSearchSendParams")));
            this.rdBtnSyuusyuu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdBtnSyuusyuu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdBtnSyuusyuu.popupWindowSetting")));
            this.rdBtnSyuusyuu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnSyuusyuu.RegistCheckMethod")));
            this.rdBtnSyuusyuu.Size = new System.Drawing.Size(67, 17);
            this.rdBtnSyuusyuu.TabIndex = 0;
            this.rdBtnSyuusyuu.Tag = "伝票種類が「1.収集」の場合にはチェックを付けて下さい";
            this.rdBtnSyuusyuu.Text = "1.収集";
            this.rdBtnSyuusyuu.UseVisualStyleBackColor = true;
            this.rdBtnSyuusyuu.Value = "1";
            // 
            // rdBtnKuremu
            // 
            this.rdBtnKuremu.AutoSize = true;
            this.rdBtnKuremu.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdBtnKuremu.Enabled = false;
            this.rdBtnKuremu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnKuremu.FocusOutCheckMethod")));
            this.rdBtnKuremu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdBtnKuremu.LinkedTextBox = "DENPYOU_SHURUI";
            this.rdBtnKuremu.Location = new System.Drawing.Point(222, 2);
            this.rdBtnKuremu.Name = "rdBtnKuremu";
            this.rdBtnKuremu.PopupAfterExecute = null;
            this.rdBtnKuremu.PopupBeforeExecute = null;
            this.rdBtnKuremu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdBtnKuremu.PopupSearchSendParams")));
            this.rdBtnKuremu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdBtnKuremu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdBtnKuremu.popupWindowSetting")));
            this.rdBtnKuremu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnKuremu.RegistCheckMethod")));
            this.rdBtnKuremu.Size = new System.Drawing.Size(67, 17);
            this.rdBtnKuremu.TabIndex = 3;
            this.rdBtnKuremu.Tag = "伝票種類が「4.定期」の場合にはチェックを付けて下さい";
            this.rdBtnKuremu.Text = "4.定期";
            this.rdBtnKuremu.UseVisualStyleBackColor = true;
            this.rdBtnKuremu.Value = "4";
            // 
            // rdBtnSyukka
            // 
            this.rdBtnSyukka.AutoSize = true;
            this.rdBtnSyukka.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdBtnSyukka.Enabled = false;
            this.rdBtnSyukka.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnSyukka.FocusOutCheckMethod")));
            this.rdBtnSyukka.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdBtnSyukka.LinkedTextBox = "DENPYOU_SHURUI";
            this.rdBtnSyukka.Location = new System.Drawing.Point(76, 2);
            this.rdBtnSyukka.Name = "rdBtnSyukka";
            this.rdBtnSyukka.PopupAfterExecute = null;
            this.rdBtnSyukka.PopupBeforeExecute = null;
            this.rdBtnSyukka.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdBtnSyukka.PopupSearchSendParams")));
            this.rdBtnSyukka.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdBtnSyukka.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdBtnSyukka.popupWindowSetting")));
            this.rdBtnSyukka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnSyukka.RegistCheckMethod")));
            this.rdBtnSyukka.Size = new System.Drawing.Size(67, 17);
            this.rdBtnSyukka.TabIndex = 1;
            this.rdBtnSyukka.Tag = "伝票種類が「2.出荷」の場合にはチェックを付けて下さい";
            this.rdBtnSyukka.Text = "2.出荷";
            this.rdBtnSyukka.UseVisualStyleBackColor = true;
            this.rdBtnSyukka.Value = "2";
            // 
            // rdBtnMotikomi
            // 
            this.rdBtnMotikomi.AutoSize = true;
            this.rdBtnMotikomi.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdBtnMotikomi.Enabled = false;
            this.rdBtnMotikomi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnMotikomi.FocusOutCheckMethod")));
            this.rdBtnMotikomi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdBtnMotikomi.LinkedTextBox = "DENPYOU_SHURUI";
            this.rdBtnMotikomi.Location = new System.Drawing.Point(149, 2);
            this.rdBtnMotikomi.Name = "rdBtnMotikomi";
            this.rdBtnMotikomi.PopupAfterExecute = null;
            this.rdBtnMotikomi.PopupBeforeExecute = null;
            this.rdBtnMotikomi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdBtnMotikomi.PopupSearchSendParams")));
            this.rdBtnMotikomi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdBtnMotikomi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdBtnMotikomi.popupWindowSetting")));
            this.rdBtnMotikomi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnMotikomi.RegistCheckMethod")));
            this.rdBtnMotikomi.Size = new System.Drawing.Size(67, 17);
            this.rdBtnMotikomi.TabIndex = 2;
            this.rdBtnMotikomi.Tag = "伝票種類が「3.持込」の場合にはチェックを付けて下さい";
            this.rdBtnMotikomi.Text = "3.持込";
            this.rdBtnMotikomi.UseVisualStyleBackColor = true;
            this.rdBtnMotikomi.Value = "3";
            // 
            // DENPYOU_NUMBER
            // 
            this.DENPYOU_NUMBER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.DENPYOU_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENPYOU_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENPYOU_NUMBER.DisplayPopUp = null;
            this.DENPYOU_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_NUMBER.FocusOutCheckMethod")));
            this.DENPYOU_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DENPYOU_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.DENPYOU_NUMBER.IsInputErrorOccured = false;
            this.DENPYOU_NUMBER.Location = new System.Drawing.Point(565, 34);
            this.DENPYOU_NUMBER.Multiline = true;
            this.DENPYOU_NUMBER.Name = "DENPYOU_NUMBER";
            this.DENPYOU_NUMBER.PopupAfterExecute = null;
            this.DENPYOU_NUMBER.PopupBeforeExecute = null;
            this.DENPYOU_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENPYOU_NUMBER.PopupSearchSendParams")));
            this.DENPYOU_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENPYOU_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENPYOU_NUMBER.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.DENPYOU_NUMBER.RangeSetting = rangeSettingDto2;
            this.DENPYOU_NUMBER.ReadOnly = true;
            this.DENPYOU_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENPYOU_NUMBER.RegistCheckMethod")));
            this.DENPYOU_NUMBER.Size = new System.Drawing.Size(92, 22);
            this.DENPYOU_NUMBER.TabIndex = 937;
            this.DENPYOU_NUMBER.TabStop = false;
            this.DENPYOU_NUMBER.Tag = "伝票番号を指定して下さい";
            this.DENPYOU_NUMBER.WordWrap = false;
            // 
            // COURSE_NAME_CD
            // 
            this.COURSE_NAME_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.COURSE_NAME_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.COURSE_NAME_CD.ChangeUpperCase = true;
            this.COURSE_NAME_CD.CharacterLimitList = null;
            this.COURSE_NAME_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.COURSE_NAME_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.COURSE_NAME_CD.DisplayItemName = "コース名";
            this.COURSE_NAME_CD.DisplayPopUp = null;
            this.COURSE_NAME_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COURSE_NAME_CD.FocusOutCheckMethod")));
            this.COURSE_NAME_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.COURSE_NAME_CD.ForeColor = System.Drawing.Color.Black;
            this.COURSE_NAME_CD.GetCodeMasterField = "COURSE_NAME_CD,COURSE_NAME";
            this.COURSE_NAME_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.COURSE_NAME_CD.IsInputErrorOccured = false;
            this.COURSE_NAME_CD.Location = new System.Drawing.Point(565, 59);
            this.COURSE_NAME_CD.MaxLength = 6;
            this.COURSE_NAME_CD.Multiline = true;
            this.COURSE_NAME_CD.Name = "COURSE_NAME_CD";
            this.COURSE_NAME_CD.PopupAfterExecute = null;
            this.COURSE_NAME_CD.PopupBeforeExecute = null;
            this.COURSE_NAME_CD.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME";
            this.COURSE_NAME_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COURSE_NAME_CD.PopupSearchSendParams")));
            this.COURSE_NAME_CD.PopupSendParams = new string[] {
        "Ichiran"};
            this.COURSE_NAME_CD.PopupSetFormField = "COURSE_NAME_CD,COURSE_NAME";
            this.COURSE_NAME_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_COURSE_NAME;
            this.COURSE_NAME_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.COURSE_NAME_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COURSE_NAME_CD.popupWindowSetting")));
            this.COURSE_NAME_CD.ReadOnly = true;
            this.COURSE_NAME_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COURSE_NAME_CD.RegistCheckMethod")));
            this.COURSE_NAME_CD.SetFormField = "COURSE_NAME_CD,COURSE_NAME";
            this.COURSE_NAME_CD.ShortItemName = "検索条件";
            this.COURSE_NAME_CD.Size = new System.Drawing.Size(50, 22);
            this.COURSE_NAME_CD.TabIndex = 938;
            this.COURSE_NAME_CD.TabStop = false;
            this.COURSE_NAME_CD.Tag = "コース名を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.COURSE_NAME_CD.ZeroPaddengFlag = true;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(483, 59);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(79, 22);
            this.label16.TabIndex = 939;
            this.label16.Text = "コース名";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // COURSE_NAME
            // 
            this.COURSE_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.COURSE_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.COURSE_NAME.CharactersNumber = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.COURSE_NAME.DBFieldsName = "";
            this.COURSE_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.COURSE_NAME.DisplayItemName = "検索条件";
            this.COURSE_NAME.DisplayPopUp = null;
            this.COURSE_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COURSE_NAME.FocusOutCheckMethod")));
            this.COURSE_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.COURSE_NAME.ForeColor = System.Drawing.Color.Black;
            this.COURSE_NAME.IsInputErrorOccured = false;
            this.COURSE_NAME.ItemDefinedTypes = "";
            this.COURSE_NAME.Location = new System.Drawing.Point(614, 59);
            this.COURSE_NAME.MaxLength = 40;
            this.COURSE_NAME.Multiline = true;
            this.COURSE_NAME.Name = "COURSE_NAME";
            this.COURSE_NAME.PopupAfterExecute = null;
            this.COURSE_NAME.PopupBeforeExecute = null;
            this.COURSE_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COURSE_NAME.PopupSearchSendParams")));
            this.COURSE_NAME.PopupSetFormField = "";
            this.COURSE_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COURSE_NAME.PopupWindowName = "";
            this.COURSE_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COURSE_NAME.popupWindowSetting")));
            this.COURSE_NAME.ReadOnly = true;
            this.COURSE_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COURSE_NAME.RegistCheckMethod")));
            this.COURSE_NAME.SetFormField = "";
            this.COURSE_NAME.ShortItemName = "検索条件";
            this.COURSE_NAME.Size = new System.Drawing.Size(188, 22);
            this.COURSE_NAME.TabIndex = 940;
            this.COURSE_NAME.TabStop = false;
            this.COURSE_NAME.Tag = " ";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(809, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 22);
            this.label3.TabIndex = 941;
            this.label3.Text = "順番";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ROW_NUMBER
            // 
            this.ROW_NUMBER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ROW_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ROW_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.ROW_NUMBER.DisplayPopUp = null;
            this.ROW_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ROW_NUMBER.FocusOutCheckMethod")));
            this.ROW_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ROW_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.ROW_NUMBER.IsInputErrorOccured = false;
            this.ROW_NUMBER.Location = new System.Drawing.Point(854, 59);
            this.ROW_NUMBER.Multiline = true;
            this.ROW_NUMBER.Name = "ROW_NUMBER";
            this.ROW_NUMBER.PopupAfterExecute = null;
            this.ROW_NUMBER.PopupBeforeExecute = null;
            this.ROW_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ROW_NUMBER.PopupSearchSendParams")));
            this.ROW_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ROW_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ROW_NUMBER.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.ROW_NUMBER.RangeSetting = rangeSettingDto3;
            this.ROW_NUMBER.ReadOnly = true;
            this.ROW_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ROW_NUMBER.RegistCheckMethod")));
            this.ROW_NUMBER.Size = new System.Drawing.Size(52, 22);
            this.ROW_NUMBER.TabIndex = 942;
            this.ROW_NUMBER.TabStop = false;
            this.ROW_NUMBER.Tag = "";
            this.ROW_NUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ROW_NUMBER.WordWrap = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(8, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 22);
            this.label4.TabIndex = 943;
            this.label4.Text = "件名";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearSubject
            // 
            this.btnClearSubject.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnClearSubject.Location = new System.Drawing.Point(8, 59);
            this.btnClearSubject.Name = "btnClearSubject";
            this.btnClearSubject.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnClearSubject.Size = new System.Drawing.Size(100, 22);
            this.btnClearSubject.TabIndex = 945;
            this.btnClearSubject.TabStop = false;
            this.btnClearSubject.Text = "クリア";
            this.btnClearSubject.UseVisualStyleBackColor = true;
            this.btnClearSubject.Click += new System.EventHandler(this.btnClearSubject_Click);
            // 
            // customPanel2
            // 
            this.customPanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.customPanel2.Controls.Add(this.rdBtnDisplay);
            this.customPanel2.Controls.Add(this.rdBtnHide);
            this.customPanel2.Location = new System.Drawing.Point(139, 84);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(292, 22);
            this.customPanel2.TabIndex = 948;
            // 
            // rdBtnDisplay
            // 
            this.rdBtnDisplay.AutoSize = true;
            this.rdBtnDisplay.Checked = true;
            this.rdBtnDisplay.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdBtnDisplay.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnDisplay.FocusOutCheckMethod")));
            this.rdBtnDisplay.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdBtnDisplay.LinkedTextBox = "RECEIVER_KBN";
            this.rdBtnDisplay.Location = new System.Drawing.Point(3, 2);
            this.rdBtnDisplay.Name = "rdBtnDisplay";
            this.rdBtnDisplay.PopupAfterExecute = null;
            this.rdBtnDisplay.PopupBeforeExecute = null;
            this.rdBtnDisplay.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdBtnDisplay.PopupSearchSendParams")));
            this.rdBtnDisplay.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdBtnDisplay.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdBtnDisplay.popupWindowSetting")));
            this.rdBtnDisplay.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnDisplay.RegistCheckMethod")));
            this.rdBtnDisplay.Size = new System.Drawing.Size(67, 17);
            this.rdBtnDisplay.TabIndex = 0;
            this.rdBtnDisplay.Tag = "受信者を表示する場合にはチェックを付けて下さい";
            this.rdBtnDisplay.Text = "1.表示";
            this.rdBtnDisplay.UseVisualStyleBackColor = true;
            this.rdBtnDisplay.Value = "1";
            // 
            // rdBtnHide
            // 
            this.rdBtnHide.AutoSize = true;
            this.rdBtnHide.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdBtnHide.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnHide.FocusOutCheckMethod")));
            this.rdBtnHide.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdBtnHide.LinkedTextBox = "RECEIVER_KBN";
            this.rdBtnHide.Location = new System.Drawing.Point(76, 2);
            this.rdBtnHide.Name = "rdBtnHide";
            this.rdBtnHide.PopupAfterExecute = null;
            this.rdBtnHide.PopupBeforeExecute = null;
            this.rdBtnHide.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdBtnHide.PopupSearchSendParams")));
            this.rdBtnHide.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdBtnHide.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdBtnHide.popupWindowSetting")));
            this.rdBtnHide.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdBtnHide.RegistCheckMethod")));
            this.rdBtnHide.Size = new System.Drawing.Size(81, 17);
            this.rdBtnHide.TabIndex = 1;
            this.rdBtnHide.Tag = "受信者を表示しない場合にはチェックを付けて下さい";
            this.rdBtnHide.Text = "2.非表示";
            this.rdBtnHide.UseVisualStyleBackColor = true;
            this.rdBtnHide.Value = "2";
            // 
            // RECEIVER_KBN
            // 
            this.RECEIVER_KBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.RECEIVER_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RECEIVER_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.RECEIVER_KBN.DisplayPopUp = null;
            this.RECEIVER_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RECEIVER_KBN.FocusOutCheckMethod")));
            this.RECEIVER_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.RECEIVER_KBN.ForeColor = System.Drawing.Color.Black;
            this.RECEIVER_KBN.IsInputErrorOccured = false;
            this.RECEIVER_KBN.LinkedRadioButtonArray = new string[] {
        "rdBtnDisplay",
        "rdBtnHide"};
            this.RECEIVER_KBN.Location = new System.Drawing.Point(111, 84);
            this.RECEIVER_KBN.Multiline = true;
            this.RECEIVER_KBN.Name = "RECEIVER_KBN";
            this.RECEIVER_KBN.PopupAfterExecute = null;
            this.RECEIVER_KBN.PopupBeforeExecute = null;
            this.RECEIVER_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("RECEIVER_KBN.PopupSearchSendParams")));
            this.RECEIVER_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.RECEIVER_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("RECEIVER_KBN.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            6,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RECEIVER_KBN.RangeSetting = rangeSettingDto4;
            this.RECEIVER_KBN.ReadOnly = true;
            this.RECEIVER_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("RECEIVER_KBN.RegistCheckMethod")));
            this.RECEIVER_KBN.Size = new System.Drawing.Size(22, 22);
            this.RECEIVER_KBN.TabIndex = 947;
            this.RECEIVER_KBN.TabStop = false;
            this.RECEIVER_KBN.Tag = "【1～2】のいずれかで入力して下さい";
            this.RECEIVER_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.RECEIVER_KBN.WordWrap = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(8, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 22);
            this.label5.TabIndex = 946;
            this.label5.Text = "受信者";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(8, 112);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 22);
            this.label6.TabIndex = 949;
            this.label6.Text = "挨拶文";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearGrettings
            // 
            this.btnClearGrettings.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnClearGrettings.Location = new System.Drawing.Point(8, 137);
            this.btnClearGrettings.Name = "btnClearGrettings";
            this.btnClearGrettings.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnClearGrettings.Size = new System.Drawing.Size(100, 22);
            this.btnClearGrettings.TabIndex = 951;
            this.btnClearGrettings.TabStop = false;
            this.btnClearGrettings.Text = "クリア";
            this.btnClearGrettings.UseVisualStyleBackColor = true;
            this.btnClearGrettings.Click += new System.EventHandler(this.btnClearGrettings_Click);
            // 
            // btnClearText1
            // 
            this.btnClearText1.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnClearText1.Location = new System.Drawing.Point(8, 190);
            this.btnClearText1.Name = "btnClearText1";
            this.btnClearText1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnClearText1.Size = new System.Drawing.Size(100, 22);
            this.btnClearText1.TabIndex = 954;
            this.btnClearText1.TabStop = false;
            this.btnClearText1.Text = "クリア";
            this.btnClearText1.UseVisualStyleBackColor = true;
            this.btnClearText1.Click += new System.EventHandler(this.btnClearText1_Click);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(8, 165);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 22);
            this.label7.TabIndex = 952;
            this.label7.Text = "本文1";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearText2
            // 
            this.btnClearText2.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnClearText2.Location = new System.Drawing.Point(8, 266);
            this.btnClearText2.Name = "btnClearText2";
            this.btnClearText2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnClearText2.Size = new System.Drawing.Size(100, 22);
            this.btnClearText2.TabIndex = 957;
            this.btnClearText2.TabStop = false;
            this.btnClearText2.Text = "クリア";
            this.btnClearText2.UseVisualStyleBackColor = true;
            this.btnClearText2.Click += new System.EventHandler(this.btnClearText2_Click);
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(8, 241);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(100, 22);
            this.label8.TabIndex = 955;
            this.label8.Text = "本文2";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearText3
            // 
            this.btnClearText3.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnClearText3.Location = new System.Drawing.Point(8, 342);
            this.btnClearText3.Name = "btnClearText3";
            this.btnClearText3.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnClearText3.Size = new System.Drawing.Size(100, 22);
            this.btnClearText3.TabIndex = 960;
            this.btnClearText3.TabStop = false;
            this.btnClearText3.Text = "クリア";
            this.btnClearText3.UseVisualStyleBackColor = true;
            this.btnClearText3.Click += new System.EventHandler(this.btnClearText3_Click);
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(8, 317);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(100, 22);
            this.label9.TabIndex = 958;
            this.label9.Text = "本文3";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearText4
            // 
            this.btnClearText4.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnClearText4.Location = new System.Drawing.Point(8, 395);
            this.btnClearText4.Name = "btnClearText4";
            this.btnClearText4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnClearText4.Size = new System.Drawing.Size(100, 22);
            this.btnClearText4.TabIndex = 963;
            this.btnClearText4.TabStop = false;
            this.btnClearText4.Text = "クリア";
            this.btnClearText4.UseVisualStyleBackColor = true;
            this.btnClearText4.Click += new System.EventHandler(this.btnClearText4_Click);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(8, 370);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(100, 22);
            this.label10.TabIndex = 961;
            this.label10.Text = "本文4";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnClearSignature
            // 
            this.btnClearSignature.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnClearSignature.Location = new System.Drawing.Point(8, 448);
            this.btnClearSignature.Name = "btnClearSignature";
            this.btnClearSignature.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnClearSignature.Size = new System.Drawing.Size(100, 22);
            this.btnClearSignature.TabIndex = 966;
            this.btnClearSignature.TabStop = false;
            this.btnClearSignature.Text = "クリア";
            this.btnClearSignature.UseVisualStyleBackColor = true;
            this.btnClearSignature.Click += new System.EventHandler(this.btnClearSignature_Click);
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(8, 423);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(100, 22);
            this.label11.TabIndex = 964;
            this.label11.Text = "署名";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(483, 112);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(79, 22);
            this.label12.TabIndex = 968;
            this.label12.Text = "業者";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD.ChangeUpperCase = true;
            this.GYOUSHA_CD.CharacterLimitList = null;
            this.GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "業者";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(565, 112);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Multiline = true;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecuteMethod = "GyoushaPopupBeforeMethod";
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.ReadOnly = true;
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(50, 22);
            this.GYOUSHA_CD.TabIndex = 969;
            this.GYOUSHA_CD.TabStop = false;
            this.GYOUSHA_CD.Tag = "業者を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // GYOUSHA_NAME
            // 
            this.GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GYOUSHA_NAME.DBFieldsName = "";
            this.GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME.DisplayPopUp = null;
            this.GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.FocusOutCheckMethod")));
            this.GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GYOUSHA_NAME.IsInputErrorOccured = false;
            this.GYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME.Location = new System.Drawing.Point(614, 112);
            this.GYOUSHA_NAME.MaxLength = 40;
            this.GYOUSHA_NAME.Multiline = true;
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupAfterExecute = null;
            this.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.Size = new System.Drawing.Size(286, 22);
            this.GYOUSHA_NAME.TabIndex = 970;
            this.GYOUSHA_NAME.TabStop = false;
            this.GYOUSHA_NAME.Tag = " ";
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(483, 137);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(79, 22);
            this.label13.TabIndex = 971;
            this.label13.Text = "現場";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD.ChangeUpperCase = true;
            this.GENBA_CD.CharacterLimitList = null;
            this.GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD.DBFieldsName = "GENBA_CD";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayItemName = "現場";
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "GENBA_CD, GENBA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(565, 137);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Multiline = true;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupBeforeExecuteMethod = "GenbaPopupBefore";
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD, GENBA_NAME,GYOUSHA_CD";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.ReadOnly = true;
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD, GENBA_NAME";
            this.GENBA_CD.Size = new System.Drawing.Size(50, 22);
            this.GENBA_CD.TabIndex = 972;
            this.GENBA_CD.TabStop = false;
            this.GENBA_CD.Tag = "現場を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            // 
            // GENBA_NAME
            // 
            this.GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GENBA_NAME.DBFieldsName = "";
            this.GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME.DisplayPopUp = null;
            this.GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.FocusOutCheckMethod")));
            this.GENBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GENBA_NAME.IsInputErrorOccured = false;
            this.GENBA_NAME.ItemDefinedTypes = "varchar";
            this.GENBA_NAME.Location = new System.Drawing.Point(614, 137);
            this.GENBA_NAME.MaxLength = 40;
            this.GENBA_NAME.Multiline = true;
            this.GENBA_NAME.Name = "GENBA_NAME";
            this.GENBA_NAME.PopupAfterExecute = null;
            this.GENBA_NAME.PopupBeforeExecute = null;
            this.GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME.PopupSearchSendParams")));
            this.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME.popupWindowSetting")));
            this.GENBA_NAME.ReadOnly = true;
            this.GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.RegistCheckMethod")));
            this.GENBA_NAME.Size = new System.Drawing.Size(286, 22);
            this.GENBA_NAME.TabIndex = 973;
            this.GENBA_NAME.TabStop = false;
            this.GENBA_NAME.Tag = " ";
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(483, 162);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(79, 22);
            this.label14.TabIndex = 974;
            this.label14.Text = "住所";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_ADDRESS1
            // 
            this.GENBA_ADDRESS1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_ADDRESS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_ADDRESS1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GENBA_ADDRESS1.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_ADDRESS1.DisplayPopUp = null;
            this.GENBA_ADDRESS1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_ADDRESS1.FocusOutCheckMethod")));
            this.GENBA_ADDRESS1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_ADDRESS1.ForeColor = System.Drawing.Color.Black;
            this.GENBA_ADDRESS1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GENBA_ADDRESS1.IsInputErrorOccured = false;
            this.GENBA_ADDRESS1.Location = new System.Drawing.Point(565, 162);
            this.GENBA_ADDRESS1.MaxLength = 40;
            this.GENBA_ADDRESS1.Multiline = true;
            this.GENBA_ADDRESS1.Name = "GENBA_ADDRESS1";
            this.GENBA_ADDRESS1.PopupAfterExecute = null;
            this.GENBA_ADDRESS1.PopupBeforeExecute = null;
            this.GENBA_ADDRESS1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_ADDRESS1.PopupSearchSendParams")));
            this.GENBA_ADDRESS1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_ADDRESS1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_ADDRESS1.popupWindowSetting")));
            this.GENBA_ADDRESS1.ReadOnly = true;
            this.GENBA_ADDRESS1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_ADDRESS1.RegistCheckMethod")));
            this.GENBA_ADDRESS1.Size = new System.Drawing.Size(336, 22);
            this.GENBA_ADDRESS1.TabIndex = 975;
            this.GENBA_ADDRESS1.TabStop = false;
            this.GENBA_ADDRESS1.Tag = "";
            // 
            // GENBA_ADDRESS2
            // 
            this.GENBA_ADDRESS2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_ADDRESS2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_ADDRESS2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.GENBA_ADDRESS2.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_ADDRESS2.DisplayPopUp = null;
            this.GENBA_ADDRESS2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_ADDRESS2.FocusOutCheckMethod")));
            this.GENBA_ADDRESS2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_ADDRESS2.ForeColor = System.Drawing.Color.Black;
            this.GENBA_ADDRESS2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.GENBA_ADDRESS2.IsInputErrorOccured = false;
            this.GENBA_ADDRESS2.Location = new System.Drawing.Point(565, 187);
            this.GENBA_ADDRESS2.MaxLength = 40;
            this.GENBA_ADDRESS2.Multiline = true;
            this.GENBA_ADDRESS2.Name = "GENBA_ADDRESS2";
            this.GENBA_ADDRESS2.PopupAfterExecute = null;
            this.GENBA_ADDRESS2.PopupBeforeExecute = null;
            this.GENBA_ADDRESS2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_ADDRESS2.PopupSearchSendParams")));
            this.GENBA_ADDRESS2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_ADDRESS2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_ADDRESS2.popupWindowSetting")));
            this.GENBA_ADDRESS2.ReadOnly = true;
            this.GENBA_ADDRESS2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_ADDRESS2.RegistCheckMethod")));
            this.GENBA_ADDRESS2.Size = new System.Drawing.Size(336, 22);
            this.GENBA_ADDRESS2.TabIndex = 976;
            this.GENBA_ADDRESS2.TabStop = false;
            this.GENBA_ADDRESS2.Tag = "";
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.AllowUserToDeleteRows = false;
            this.customDataGridView1.AllowUserToResizeRows = false;
            this.customDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.receiverName,
            this.mobilePhoneNumber,
            this.sendFlg});
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridView1.DefaultCellStyle = dataGridViewCellStyle5;
            this.customDataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.customDataGridView1.EnableHeadersVisualStyles = false;
            this.customDataGridView1.GridColor = System.Drawing.Color.White;
            this.customDataGridView1.IsReload = false;
            this.customDataGridView1.LinkedDataPanelName = null;
            this.customDataGridView1.Location = new System.Drawing.Point(483, 215);
            this.customDataGridView1.MultiSelect = false;
            this.customDataGridView1.Name = "customDataGridView1";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 21;
            this.customDataGridView1.ShowCellToolTips = false;
            this.customDataGridView1.Size = new System.Drawing.Size(352, 96);
            this.customDataGridView1.TabIndex = 977;
            this.customDataGridView1.TabStop = false;
            // 
            // receiverName
            // 
            this.receiverName.CharactersNumber = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.receiverName.DataPropertyName = "RECEIVER_NAME";
            this.receiverName.DBFieldsName = "RECEIVER_NAME";
            this.receiverName.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.receiverName.DefaultCellStyle = dataGridViewCellStyle2;
            this.receiverName.DisplayItemName = "受信者名";
            this.receiverName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("receiverName.FocusOutCheckMethod")));
            this.receiverName.HeaderText = "受信者名";
            this.receiverName.MaxInputLength = 8;
            this.receiverName.Name = "receiverName";
            this.receiverName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("receiverName.PopupSearchSendParams")));
            this.receiverName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.receiverName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("receiverName.popupWindowSetting")));
            this.receiverName.ReadOnly = true;
            this.receiverName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("receiverName.RegistCheckMethod")));
            this.receiverName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.receiverName.Width = 150;
            // 
            // mobilePhoneNumber
            // 
            this.mobilePhoneNumber.DataPropertyName = "MOBILE_PHONE_NUMBER";
            this.mobilePhoneNumber.DBFieldsName = "MOBILE_PHONE_NUMBER";
            this.mobilePhoneNumber.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.mobilePhoneNumber.DefaultCellStyle = dataGridViewCellStyle3;
            this.mobilePhoneNumber.DisplayItemName = "電話番号";
            this.mobilePhoneNumber.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("mobilePhoneNumber.FocusOutCheckMethod")));
            this.mobilePhoneNumber.HeaderText = "携帯番号";
            this.mobilePhoneNumber.Name = "mobilePhoneNumber";
            this.mobilePhoneNumber.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("mobilePhoneNumber.PopupSearchSendParams")));
            this.mobilePhoneNumber.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.mobilePhoneNumber.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("mobilePhoneNumber.popupWindowSetting")));
            this.mobilePhoneNumber.ReadOnly = true;
            this.mobilePhoneNumber.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("mobilePhoneNumber.RegistCheckMethod")));
            this.mobilePhoneNumber.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.mobilePhoneNumber.Width = 150;
            // 
            // sendFlg
            // 
            this.sendFlg.DBFieldsName = "";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.NullValue = false;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.sendFlg.DefaultCellStyle = dataGridViewCellStyle4;
            this.sendFlg.DisplayItemName = "送信";
            this.sendFlg.FillWeight = 50F;
            this.sendFlg.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("sendFlg.FocusOutCheckMethod")));
            this.sendFlg.HeaderText = "送信";
            this.sendFlg.Name = "sendFlg";
            this.sendFlg.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("sendFlg.RegistCheckMethod")));
            this.sendFlg.Width = 50;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label15.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(483, 317);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(79, 22);
            this.label15.TabIndex = 978;
            this.label15.Text = "営業担当者";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EIGYOU_TANTOUSHA_NAME
            // 
            this.EIGYOU_TANTOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.EIGYOU_TANTOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EIGYOU_TANTOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.EIGYOU_TANTOUSHA_NAME.DBFieldsName = "SHAIN_NAME_RYAKU";
            this.EIGYOU_TANTOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.EIGYOU_TANTOUSHA_NAME.DisplayPopUp = null;
            this.EIGYOU_TANTOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.FocusOutCheckMethod")));
            this.EIGYOU_TANTOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EIGYOU_TANTOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.EIGYOU_TANTOUSHA_NAME.IsInputErrorOccured = false;
            this.EIGYOU_TANTOUSHA_NAME.Location = new System.Drawing.Point(615, 317);
            this.EIGYOU_TANTOUSHA_NAME.MaxLength = 0;
            this.EIGYOU_TANTOUSHA_NAME.Multiline = true;
            this.EIGYOU_TANTOUSHA_NAME.Name = "EIGYOU_TANTOUSHA_NAME";
            this.EIGYOU_TANTOUSHA_NAME.PopupAfterExecute = null;
            this.EIGYOU_TANTOUSHA_NAME.PopupBeforeExecute = null;
            this.EIGYOU_TANTOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.PopupSearchSendParams")));
            this.EIGYOU_TANTOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.EIGYOU_TANTOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.popupWindowSetting")));
            this.EIGYOU_TANTOUSHA_NAME.ReadOnly = true;
            this.EIGYOU_TANTOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.RegistCheckMethod")));
            this.EIGYOU_TANTOUSHA_NAME.Size = new System.Drawing.Size(187, 22);
            this.EIGYOU_TANTOUSHA_NAME.TabIndex = 980;
            this.EIGYOU_TANTOUSHA_NAME.TabStop = false;
            this.EIGYOU_TANTOUSHA_NAME.Tag = "";
            // 
            // EIGYOU_TANTOUSHA_CD
            // 
            this.EIGYOU_TANTOUSHA_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.EIGYOU_TANTOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EIGYOU_TANTOUSHA_CD.ChangeUpperCase = true;
            this.EIGYOU_TANTOUSHA_CD.CharacterLimitList = null;
            this.EIGYOU_TANTOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.EIGYOU_TANTOUSHA_CD.DBFieldsName = "EIGYOU_TANTOU_CD";
            this.EIGYOU_TANTOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.EIGYOU_TANTOUSHA_CD.DisplayItemName = "営業担当";
            this.EIGYOU_TANTOUSHA_CD.DisplayPopUp = null;
            this.EIGYOU_TANTOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOUSHA_CD.FocusOutCheckMethod")));
            this.EIGYOU_TANTOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.EIGYOU_TANTOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.EIGYOU_TANTOUSHA_CD.GetCodeMasterField = "EIGYOU_TANTOU_CD, SHAIN_NAME_RYAKU";
            this.EIGYOU_TANTOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.EIGYOU_TANTOUSHA_CD.IsInputErrorOccured = false;
            this.EIGYOU_TANTOUSHA_CD.ItemDefinedTypes = "varchar";
            this.EIGYOU_TANTOUSHA_CD.Location = new System.Drawing.Point(566, 317);
            this.EIGYOU_TANTOUSHA_CD.MaxLength = 6;
            this.EIGYOU_TANTOUSHA_CD.Multiline = true;
            this.EIGYOU_TANTOUSHA_CD.Name = "EIGYOU_TANTOUSHA_CD";
            this.EIGYOU_TANTOUSHA_CD.PopupAfterExecute = null;
            this.EIGYOU_TANTOUSHA_CD.PopupBeforeExecute = null;
            this.EIGYOU_TANTOUSHA_CD.PopupGetMasterField = "EIGYOU_TANTOU_CD, SHAIN_NAME_RYAKU";
            this.EIGYOU_TANTOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EIGYOU_TANTOUSHA_CD.PopupSearchSendParams")));
            this.EIGYOU_TANTOUSHA_CD.PopupSetFormField = "EIGYOU_TANTOUSHA_CD, EIGYOU_TANTOUSHA_NAME";
            this.EIGYOU_TANTOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.EIGYOU_TANTOUSHA_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.EIGYOU_TANTOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EIGYOU_TANTOUSHA_CD.popupWindowSetting")));
            this.EIGYOU_TANTOUSHA_CD.ReadOnly = true;
            this.EIGYOU_TANTOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOUSHA_CD.RegistCheckMethod")));
            this.EIGYOU_TANTOUSHA_CD.SetFormField = "EIGYOU_TANTOUSHA_CD, EIGYOU_TANTOUSHA_NAME";
            this.EIGYOU_TANTOUSHA_CD.Size = new System.Drawing.Size(50, 22);
            this.EIGYOU_TANTOUSHA_CD.TabIndex = 979;
            this.EIGYOU_TANTOUSHA_CD.TabStop = false;
            this.EIGYOU_TANTOUSHA_CD.Tag = "営業担当を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.EIGYOU_TANTOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(483, 342);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(79, 22);
            this.label17.TabIndex = 981;
            this.label17.Text = "運搬業者";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UNPAN_GYOUSHA_NAME
            // 
            this.UNPAN_GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNPAN_GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_NAME.DBFieldsName = "";
            this.UNPAN_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_NAME.DisplayItemName = "";
            this.UNPAN_GYOUSHA_NAME.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNPAN_GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.UNPAN_GYOUSHA_NAME.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_NAME.Location = new System.Drawing.Point(615, 342);
            this.UNPAN_GYOUSHA_NAME.MaxLength = 40;
            this.UNPAN_GYOUSHA_NAME.Multiline = true;
            this.UNPAN_GYOUSHA_NAME.Name = "UNPAN_GYOUSHA_NAME";
            this.UNPAN_GYOUSHA_NAME.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_NAME.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.popupWindowSetting")));
            this.UNPAN_GYOUSHA_NAME.ReadOnly = true;
            this.UNPAN_GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_NAME.Size = new System.Drawing.Size(286, 22);
            this.UNPAN_GYOUSHA_NAME.TabIndex = 983;
            this.UNPAN_GYOUSHA_NAME.TabStop = false;
            this.UNPAN_GYOUSHA_NAME.Tag = " ";
            // 
            // UNPAN_GYOUSHA_CD
            // 
            this.UNPAN_GYOUSHA_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNPAN_GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_CD.ChangeUpperCase = true;
            this.UNPAN_GYOUSHA_CD.CharacterLimitList = null;
            this.UNPAN_GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_CD.DBFieldsName = "UNPAN_GYOUSHA_CD";
            this.UNPAN_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_CD.DisplayItemName = "運搬業者";
            this.UNPAN_GYOUSHA_CD.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNPAN_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UNPAN_GYOUSHA_CD.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.UNPAN_GYOUSHA_CD.Location = new System.Drawing.Point(566, 342);
            this.UNPAN_GYOUSHA_CD.MaxLength = 6;
            this.UNPAN_GYOUSHA_CD.Multiline = true;
            this.UNPAN_GYOUSHA_CD.Name = "UNPAN_GYOUSHA_CD";
            this.UNPAN_GYOUSHA_CD.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_CD.PopupAfterExecuteMethod = "UnpanGyoushaBtnPopupMethod";
            this.UNPAN_GYOUSHA_CD.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_CD.PopupBeforeExecuteMethod = "UnpanGyoushaPopupBeforeMethod";
            this.UNPAN_GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_CD.PopupSetFormField = "UNPAN_GYOUSHA_CD, UNPAN_GYOUSHA_NAME";
            this.UNPAN_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNPAN_GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.UNPAN_GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.popupWindowSetting")));
            this.UNPAN_GYOUSHA_CD.ReadOnly = true;
            this.UNPAN_GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_CD.SetFormField = "UNPAN_GYOUSHA_CD, UNPAN_GYOUSHA_NAME";
            this.UNPAN_GYOUSHA_CD.Size = new System.Drawing.Size(50, 22);
            this.UNPAN_GYOUSHA_CD.TabIndex = 982;
            this.UNPAN_GYOUSHA_CD.TabStop = false;
            this.UNPAN_GYOUSHA_CD.Tag = "運搬業者を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.UNPAN_GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label18.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(483, 367);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(79, 22);
            this.label18.TabIndex = 984;
            this.label18.Text = "車種";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHASHU_NAME
            // 
            this.SHASHU_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHASHU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHASHU_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.SHASHU_NAME.DBFieldsName = "";
            this.SHASHU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHASHU_NAME.DisplayPopUp = null;
            this.SHASHU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHASHU_NAME.FocusOutCheckMethod")));
            this.SHASHU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHASHU_NAME.ForeColor = System.Drawing.Color.Black;
            this.SHASHU_NAME.IsInputErrorOccured = false;
            this.SHASHU_NAME.Location = new System.Drawing.Point(615, 367);
            this.SHASHU_NAME.MaxLength = 0;
            this.SHASHU_NAME.Multiline = true;
            this.SHASHU_NAME.Name = "SHASHU_NAME";
            this.SHASHU_NAME.PopupAfterExecute = null;
            this.SHASHU_NAME.PopupBeforeExecute = null;
            this.SHASHU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHASHU_NAME.PopupSearchSendParams")));
            this.SHASHU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHASHU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHASHU_NAME.popupWindowSetting")));
            this.SHASHU_NAME.ReadOnly = true;
            this.SHASHU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHASHU_NAME.RegistCheckMethod")));
            this.SHASHU_NAME.Size = new System.Drawing.Size(187, 22);
            this.SHASHU_NAME.TabIndex = 986;
            this.SHASHU_NAME.TabStop = false;
            this.SHASHU_NAME.Tag = " ";
            // 
            // SHASHU_CD
            // 
            this.SHASHU_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHASHU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHASHU_CD.ChangeUpperCase = true;
            this.SHASHU_CD.CharacterLimitList = null;
            this.SHASHU_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.SHASHU_CD.DBFieldsName = "SHASHU_CD";
            this.SHASHU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHASHU_CD.DisplayItemName = "車種";
            this.SHASHU_CD.DisplayPopUp = null;
            this.SHASHU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHASHU_CD.FocusOutCheckMethod")));
            this.SHASHU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHASHU_CD.ForeColor = System.Drawing.Color.Black;
            this.SHASHU_CD.GetCodeMasterField = "SHASHU_CD, SHASHU_NAME_RYAKU";
            this.SHASHU_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHASHU_CD.IsInputErrorOccured = false;
            this.SHASHU_CD.ItemDefinedTypes = "varchar";
            this.SHASHU_CD.Location = new System.Drawing.Point(566, 367);
            this.SHASHU_CD.MaxLength = 3;
            this.SHASHU_CD.Multiline = true;
            this.SHASHU_CD.Name = "SHASHU_CD";
            this.SHASHU_CD.PopupAfterExecute = null;
            this.SHASHU_CD.PopupBeforeExecute = null;
            this.SHASHU_CD.PopupGetMasterField = "SHASHU_CD, SHASHU_NAME_RYAKU";
            this.SHASHU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHASHU_CD.PopupSearchSendParams")));
            this.SHASHU_CD.PopupSetFormField = "SHASHU_CD, SHASHU_NAME";
            this.SHASHU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHASHU;
            this.SHASHU_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.SHASHU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHASHU_CD.popupWindowSetting")));
            this.SHASHU_CD.ReadOnly = true;
            this.SHASHU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHASHU_CD.RegistCheckMethod")));
            this.SHASHU_CD.SetFormField = "SHASHU_CD, SHASHU_NAME";
            this.SHASHU_CD.Size = new System.Drawing.Size(50, 22);
            this.SHASHU_CD.TabIndex = 985;
            this.SHASHU_CD.TabStop = false;
            this.SHASHU_CD.Tag = "車種を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.SHASHU_CD.ZeroPaddengFlag = true;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label19.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(483, 392);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(79, 22);
            this.label19.TabIndex = 987;
            this.label19.Text = "車輌";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHARYOU_NAME
            // 
            this.SHARYOU_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHARYOU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHARYOU_NAME.DBFieldsName = "";
            this.SHARYOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHARYOU_NAME.DisplayPopUp = null;
            this.SHARYOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_NAME.FocusOutCheckMethod")));
            this.SHARYOU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHARYOU_NAME.ForeColor = System.Drawing.Color.Black;
            this.SHARYOU_NAME.IsInputErrorOccured = false;
            this.SHARYOU_NAME.ItemDefinedTypes = "varchar";
            this.SHARYOU_NAME.Location = new System.Drawing.Point(615, 392);
            this.SHARYOU_NAME.MaxLength = 10;
            this.SHARYOU_NAME.Multiline = true;
            this.SHARYOU_NAME.Name = "SHARYOU_NAME";
            this.SHARYOU_NAME.PopupAfterExecute = null;
            this.SHARYOU_NAME.PopupBeforeExecute = null;
            this.SHARYOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHARYOU_NAME.PopupSearchSendParams")));
            this.SHARYOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHARYOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHARYOU_NAME.popupWindowSetting")));
            this.SHARYOU_NAME.ReadOnly = true;
            this.SHARYOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_NAME.RegistCheckMethod")));
            this.SHARYOU_NAME.Size = new System.Drawing.Size(187, 22);
            this.SHARYOU_NAME.TabIndex = 989;
            this.SHARYOU_NAME.TabStop = false;
            this.SHARYOU_NAME.Tag = " ";
            // 
            // SHARYOU_CD
            // 
            this.SHARYOU_CD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHARYOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHARYOU_CD.ChangeUpperCase = true;
            this.SHARYOU_CD.CharacterLimitList = null;
            this.SHARYOU_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.SHARYOU_CD.DBFieldsName = "SHARYOU_CD";
            this.SHARYOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHARYOU_CD.DisplayItemName = "車輌";
            this.SHARYOU_CD.DisplayPopUp = null;
            this.SHARYOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_CD.FocusOutCheckMethod")));
            this.SHARYOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHARYOU_CD.ForeColor = System.Drawing.Color.Black;
            this.SHARYOU_CD.GetCodeMasterField = "";
            this.SHARYOU_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHARYOU_CD.IsInputErrorOccured = false;
            this.SHARYOU_CD.ItemDefinedTypes = "varchar";
            this.SHARYOU_CD.Location = new System.Drawing.Point(566, 392);
            this.SHARYOU_CD.MaxLength = 6;
            this.SHARYOU_CD.Multiline = true;
            this.SHARYOU_CD.Name = "SHARYOU_CD";
            this.SHARYOU_CD.PopupAfterExecute = null;
            this.SHARYOU_CD.PopupAfterExecuteMethod = "";
            this.SHARYOU_CD.PopupBeforeExecute = null;
            this.SHARYOU_CD.PopupGetMasterField = "SHARYOU_CD, SHARYOU_NAME_RYAKU";
            this.SHARYOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHARYOU_CD.PopupSearchSendParams")));
            this.SHARYOU_CD.PopupSetFormField = "SHARYOU_CD,SHARYOU_NAME,UNPAN_GYOUSHA_CD,UNPAN_GYOUSHA_NAME,SHASHU_CD,SHASHU_NAME" +
                ",UNTENSHA_CD,UNTENSHA_NAME";
            this.SHARYOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHARYOU_CLOSED;
            this.SHARYOU_CD.PopupWindowName = "車両選択共通ポップアップ";
            this.SHARYOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHARYOU_CD.popupWindowSetting")));
            this.SHARYOU_CD.ReadOnly = true;
            this.SHARYOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_CD.RegistCheckMethod")));
            this.SHARYOU_CD.SetFormField = "";
            this.SHARYOU_CD.Size = new System.Drawing.Size(50, 22);
            this.SHARYOU_CD.TabIndex = 988;
            this.SHARYOU_CD.TabStop = false;
            this.SHARYOU_CD.Tag = "車輌を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.SHARYOU_CD.ZeroPaddengFlag = true;
            // 
            // dgvCustomTextBoxColumn1
            // 
            this.dgvCustomTextBoxColumn1.CharactersNumber = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.dgvCustomTextBoxColumn1.DataPropertyName = "RECEIVER_NAME";
            this.dgvCustomTextBoxColumn1.DBFieldsName = "RECEIVER_NAME";
            this.dgvCustomTextBoxColumn1.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle7;
            this.dgvCustomTextBoxColumn1.DisplayItemName = "受信者名";
            this.dgvCustomTextBoxColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn1.HeaderText = "受信者名";
            this.dgvCustomTextBoxColumn1.MaxInputLength = 8;
            this.dgvCustomTextBoxColumn1.Name = "dgvCustomTextBoxColumn1";
            this.dgvCustomTextBoxColumn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn1.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn1.popupWindowSetting")));
            this.dgvCustomTextBoxColumn1.ReadOnly = true;
            this.dgvCustomTextBoxColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn1.Width = 150;
            // 
            // dgvCustomPhoneNumberTextBoxColumn1
            // 
            this.dgvCustomPhoneNumberTextBoxColumn1.DataPropertyName = "MOBILE_PHONE_NUMBER";
            this.dgvCustomPhoneNumberTextBoxColumn1.DBFieldsName = "MOBILE_PHONE_NUMBER";
            this.dgvCustomPhoneNumberTextBoxColumn1.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomPhoneNumberTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle8;
            this.dgvCustomPhoneNumberTextBoxColumn1.DisplayItemName = "電話番号";
            this.dgvCustomPhoneNumberTextBoxColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomPhoneNumberTextBoxColumn1.FocusOutCheckMethod")));
            this.dgvCustomPhoneNumberTextBoxColumn1.HeaderText = "携帯番号";
            this.dgvCustomPhoneNumberTextBoxColumn1.Name = "dgvCustomPhoneNumberTextBoxColumn1";
            this.dgvCustomPhoneNumberTextBoxColumn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomPhoneNumberTextBoxColumn1.PopupSearchSendParams")));
            this.dgvCustomPhoneNumberTextBoxColumn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomPhoneNumberTextBoxColumn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomPhoneNumberTextBoxColumn1.popupWindowSetting")));
            this.dgvCustomPhoneNumberTextBoxColumn1.ReadOnly = true;
            this.dgvCustomPhoneNumberTextBoxColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomPhoneNumberTextBoxColumn1.RegistCheckMethod")));
            this.dgvCustomPhoneNumberTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomPhoneNumberTextBoxColumn1.Width = 150;
            // 
            // dgvCustomCheckBoxColumn1
            // 
            this.dgvCustomCheckBoxColumn1.DBFieldsName = "";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle9.NullValue = false;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomCheckBoxColumn1.DefaultCellStyle = dataGridViewCellStyle9;
            this.dgvCustomCheckBoxColumn1.DisplayItemName = "送信";
            this.dgvCustomCheckBoxColumn1.FillWeight = 50F;
            this.dgvCustomCheckBoxColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomCheckBoxColumn1.FocusOutCheckMethod")));
            this.dgvCustomCheckBoxColumn1.HeaderText = "送信";
            this.dgvCustomCheckBoxColumn1.Name = "dgvCustomCheckBoxColumn1";
            this.dgvCustomCheckBoxColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomCheckBoxColumn1.RegistCheckMethod")));
            this.dgvCustomCheckBoxColumn1.Width = 50;
            // 
            // SUBJECT
            // 
            this.SUBJECT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SUBJECT.ForeColor = System.Drawing.Color.Black;
            this.SUBJECT.Location = new System.Drawing.Point(111, 34);
            this.SUBJECT.MaxLength = 30;
            this.SUBJECT.Multiline = true;
            this.SUBJECT.Name = "SUBJECT";
            this.SUBJECT.Size = new System.Drawing.Size(320, 47);
            this.SUBJECT.TabIndex = 990;
            this.SUBJECT.TabStop = false;
            this.SUBJECT.Tag = "ｼｮｰﾄﾒｯｾｰｼﾞの件名を入力してください";
            // 
            // GREETINGS
            // 
            this.GREETINGS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GREETINGS.ForeColor = System.Drawing.Color.Black;
            this.GREETINGS.Location = new System.Drawing.Point(111, 112);
            this.GREETINGS.MaxLength = 60;
            this.GREETINGS.Multiline = true;
            this.GREETINGS.Name = "GREETINGS";
            this.GREETINGS.Size = new System.Drawing.Size(320, 47);
            this.GREETINGS.TabIndex = 991;
            this.GREETINGS.TabStop = false;
            this.GREETINGS.Tag = "ｼｮｰﾄﾒｯｾｰｼﾞ本文の、挨拶文を入力してください";
            // 
            // TEXT1
            // 
            this.TEXT1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEXT1.ForeColor = System.Drawing.Color.Black;
            this.TEXT1.Location = new System.Drawing.Point(111, 165);
            this.TEXT1.MaxLength = 65;
            this.TEXT1.Multiline = true;
            this.TEXT1.Name = "TEXT1";
            this.TEXT1.Size = new System.Drawing.Size(320, 70);
            this.TEXT1.TabIndex = 992;
            this.TEXT1.TabStop = false;
            this.TEXT1.Tag = "ｼｮｰﾄﾒｯｾｰｼﾞ本文になります（未入力可）";
            // 
            // TEXT2
            // 
            this.TEXT2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEXT2.ForeColor = System.Drawing.Color.Black;
            this.TEXT2.Location = new System.Drawing.Point(111, 241);
            this.TEXT2.MaxLength = 65;
            this.TEXT2.Multiline = true;
            this.TEXT2.Name = "TEXT2";
            this.TEXT2.Size = new System.Drawing.Size(320, 70);
            this.TEXT2.TabIndex = 993;
            this.TEXT2.TabStop = false;
            this.TEXT2.Tag = "ｼｮｰﾄﾒｯｾｰｼﾞ本文になります（未入力可）";
            // 
            // TEXT3
            // 
            this.TEXT3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEXT3.ForeColor = System.Drawing.Color.Black;
            this.TEXT3.Location = new System.Drawing.Point(111, 317);
            this.TEXT3.MaxLength = 65;
            this.TEXT3.Multiline = true;
            this.TEXT3.Name = "TEXT3";
            this.TEXT3.Size = new System.Drawing.Size(320, 47);
            this.TEXT3.TabIndex = 994;
            this.TEXT3.TabStop = false;
            this.TEXT3.Tag = "ｼｮｰﾄﾒｯｾｰｼﾞ本文になります（未入力可）";
            // 
            // TEXT4
            // 
            this.TEXT4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEXT4.ForeColor = System.Drawing.Color.Black;
            this.TEXT4.Location = new System.Drawing.Point(111, 370);
            this.TEXT4.MaxLength = 65;
            this.TEXT4.Multiline = true;
            this.TEXT4.Name = "TEXT4";
            this.TEXT4.Size = new System.Drawing.Size(320, 47);
            this.TEXT4.TabIndex = 995;
            this.TEXT4.TabStop = false;
            this.TEXT4.Tag = "ｼｮｰﾄﾒｯｾｰｼﾞ本文になります（未入力可）";
            // 
            // SIGNATURE
            // 
            this.SIGNATURE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SIGNATURE.ForeColor = System.Drawing.Color.Black;
            this.SIGNATURE.Location = new System.Drawing.Point(111, 423);
            this.SIGNATURE.MaxLength = 660;
            this.SIGNATURE.Multiline = true;
            this.SIGNATURE.Name = "SIGNATURE";
            this.SIGNATURE.Size = new System.Drawing.Size(320, 47);
            this.SIGNATURE.TabIndex = 996;
            this.SIGNATURE.TabStop = false;
            this.SIGNATURE.Tag = "ｼｮｰﾄﾒｯｾｰｼﾞ本文の、署名になります（未入力可）";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.SIGNATURE);
            this.Controls.Add(this.TEXT4);
            this.Controls.Add(this.TEXT3);
            this.Controls.Add(this.TEXT2);
            this.Controls.Add(this.TEXT1);
            this.Controls.Add(this.GREETINGS);
            this.Controls.Add(this.SUBJECT);
            this.Controls.Add(this.SHARYOU_NAME);
            this.Controls.Add(this.SHARYOU_CD);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.SHASHU_NAME);
            this.Controls.Add(this.SHASHU_CD);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.UNPAN_GYOUSHA_NAME);
            this.Controls.Add(this.UNPAN_GYOUSHA_CD);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.EIGYOU_TANTOUSHA_NAME);
            this.Controls.Add(this.EIGYOU_TANTOUSHA_CD);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.GENBA_ADDRESS2);
            this.Controls.Add(this.GENBA_ADDRESS1);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.GENBA_NAME);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.GYOUSHA_NAME);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnClearSignature);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnClearText4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.btnClearText3);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.btnClearText2);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnClearText1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnClearGrettings);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.RECEIVER_KBN);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnClearSubject);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ROW_NUMBER);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.COURSE_NAME_CD);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.COURSE_NAME);
            this.Controls.Add(this.DENPYOU_NUMBER);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.DENPYOU_SHURUI);
            this.Controls.Add(this.GENCHAKU_TIME);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SAGYOU_DATE);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UPDATE_USER_LABEL);
            this.Controls.Add(this.HASSEIMOTO_LABEL);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.PreviousValue = "";
            this.Text = "UIForm";
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.DgvCustomTextBoxColumn colComment;
        private r_framework.CustomControl.DgvCustomTextBoxColumn colTourokushaName;
        private r_framework.CustomControl.DgvCustomTextBoxColumn colTourokuDate;
        internal System.Windows.Forms.Label HASSEIMOTO_LABEL;
        internal System.Windows.Forms.Label UPDATE_USER_LABEL;
        internal r_framework.CustomControl.CustomTextBox SAGYOU_DATE;
        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox GENCHAKU_TIME;
        private System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomNumericTextBox2 DENPYOU_SHURUI;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomRadioButton rdBtnSyuusyuu;
        public r_framework.CustomControl.CustomRadioButton rdBtnKuremu;
        public r_framework.CustomControl.CustomRadioButton rdBtnSyukka;
        public r_framework.CustomControl.CustomRadioButton rdBtnMotikomi;
        internal r_framework.CustomControl.CustomNumericTextBox2 DENPYOU_NUMBER;
        internal r_framework.CustomControl.CustomAlphaNumTextBox COURSE_NAME_CD;
        internal System.Windows.Forms.Label label16;
        internal r_framework.CustomControl.CustomTextBox COURSE_NAME;
        internal System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomNumericTextBox2 ROW_NUMBER;
        private System.Windows.Forms.Label label4;
        private r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomRadioButton rdBtnDisplay;
        public r_framework.CustomControl.CustomRadioButton rdBtnHide;
        public r_framework.CustomControl.CustomNumericTextBox2 RECEIVER_KBN;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal r_framework.CustomControl.CustomButton btnClearSubject;
        internal r_framework.CustomControl.CustomButton btnClearGrettings;
        internal r_framework.CustomControl.CustomButton btnClearText1;
        internal System.Windows.Forms.Label label7;
        internal r_framework.CustomControl.CustomButton btnClearText2;
        internal System.Windows.Forms.Label label8;
        internal r_framework.CustomControl.CustomButton btnClearText3;
        internal System.Windows.Forms.Label label9;
        internal r_framework.CustomControl.CustomButton btnClearText4;
        internal System.Windows.Forms.Label label10;
        internal r_framework.CustomControl.CustomButton btnClearSignature;
        internal System.Windows.Forms.Label label11;
        internal System.Windows.Forms.Label label12;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME;
        internal System.Windows.Forms.Label label13;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME;
        internal System.Windows.Forms.Label label14;
        internal r_framework.CustomControl.CustomTextBox GENBA_ADDRESS1;
        internal r_framework.CustomControl.CustomTextBox GENBA_ADDRESS2;
        internal System.Windows.Forms.Label label15;
        internal r_framework.CustomControl.CustomTextBox EIGYOU_TANTOUSHA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox EIGYOU_TANTOUSHA_CD;
        internal System.Windows.Forms.Label label17;
        internal r_framework.CustomControl.CustomTextBox UNPAN_GYOUSHA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox UNPAN_GYOUSHA_CD;
        internal System.Windows.Forms.Label label18;
        internal r_framework.CustomControl.CustomTextBox SHASHU_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHASHU_CD;
        internal System.Windows.Forms.Label label19;
        internal r_framework.CustomControl.CustomTextBox SHARYOU_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHARYOU_CD;
        internal r_framework.CustomControl.CustomDataGridView customDataGridView1;
        private r_framework.CustomControl.DgvCustomTextBoxColumn receiverName;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomPhoneNumberTextBoxColumn mobilePhoneNumber;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn sendFlg;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn1;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomPhoneNumberTextBoxColumn dgvCustomPhoneNumberTextBoxColumn1;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn dgvCustomCheckBoxColumn1;
        internal System.Windows.Forms.TextBox SUBJECT;
        internal System.Windows.Forms.TextBox GREETINGS;
        internal System.Windows.Forms.TextBox TEXT1;
        internal System.Windows.Forms.TextBox TEXT2;
        internal System.Windows.Forms.TextBox TEXT3;
        internal System.Windows.Forms.TextBox TEXT4;
        internal System.Windows.Forms.TextBox SIGNATURE;
    }
}