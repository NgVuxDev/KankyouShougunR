namespace Shougun.Core.PaperManifest.ManifestImport
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
            this.rdoTsumikae = new r_framework.CustomControl.CustomRadioButton();
            this.lblHaikiKbn = new System.Windows.Forms.Label();
            this.rdoSampai = new r_framework.CustomControl.CustomRadioButton();
            this.rdoChokko = new r_framework.CustomControl.CustomRadioButton();
            this.panel1 = new r_framework.CustomControl.CustomPanel();
            this.txtHaikiKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lblError = new System.Windows.Forms.Label();
            this.lblFilePathKenpai = new System.Windows.Forms.Label();
            this.lblFilePathTsumikae = new System.Windows.Forms.Label();
            this.lblFilePathChokko = new System.Windows.Forms.Label();
            this.txtError = new r_framework.CustomControl.CustomNumericTextBox2();
            this.btnBrowseChokko = new r_framework.CustomControl.CustomButton();
            this.txtFilePathChokko = new r_framework.CustomControl.CustomTextBox();
            this.txtFilePathTsumikae = new r_framework.CustomControl.CustomTextBox();
            this.btnBrowseTsumikae = new r_framework.CustomControl.CustomButton();
            this.txtFilePathKenpai = new r_framework.CustomControl.CustomTextBox();
            this.btnBrowseKenpai = new r_framework.CustomControl.CustomButton();
            this.txtImportStatus = new r_framework.CustomControl.CustomTextBox();
            this.rdoManiHimoduke = new r_framework.CustomControl.CustomRadioButton();
            this.ManiHimoduke = new System.Windows.Forms.Label();
            this.lblCreateDate = new System.Windows.Forms.Label();
            this.dateTimeCreateDateFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.dateTimeCreateDateTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.txtFilePathManiHimoduke = new r_framework.CustomControl.CustomTextBox();
            this.btnBrowseManiHimoduke = new r_framework.CustomControl.CustomButton();
            this.lblCreateDateKara = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdoTsumikae
            // 
            this.rdoTsumikae.AutoSize = true;
            this.rdoTsumikae.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoTsumikae.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoTsumikae.FocusOutCheckMethod")));
            this.rdoTsumikae.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.rdoTsumikae.LinkedTextBox = "txtHaikiKbn";
            this.rdoTsumikae.Location = new System.Drawing.Point(143, 0);
            this.rdoTsumikae.Name = "rdoTsumikae";
            this.rdoTsumikae.PopupAfterExecute = null;
            this.rdoTsumikae.PopupBeforeExecute = null;
            this.rdoTsumikae.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoTsumikae.PopupSearchSendParams")));
            this.rdoTsumikae.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoTsumikae.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoTsumikae.popupWindowSetting")));
            this.rdoTsumikae.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoTsumikae.RegistCheckMethod")));
            this.rdoTsumikae.Size = new System.Drawing.Size(109, 17);
            this.rdoTsumikae.TabIndex = 2;
            this.rdoTsumikae.Tag = "";
            this.rdoTsumikae.Text = "2.産廃(積替)";
            this.rdoTsumikae.UseVisualStyleBackColor = true;
            this.rdoTsumikae.Value = "2";
            // 
            // lblHaikiKbn
            // 
            this.lblHaikiKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblHaikiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHaikiKbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHaikiKbn.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHaikiKbn.ForeColor = System.Drawing.Color.White;
            this.lblHaikiKbn.Location = new System.Drawing.Point(10, 0);
            this.lblHaikiKbn.Name = "lblHaikiKbn";
            this.lblHaikiKbn.Size = new System.Drawing.Size(171, 20);
            this.lblHaikiKbn.TabIndex = 0;
            this.lblHaikiKbn.Text = "対象マニフェスト種別※";
            this.lblHaikiKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdoSampai
            // 
            this.rdoSampai.AutoSize = true;
            this.rdoSampai.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoSampai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSampai.FocusOutCheckMethod")));
            this.rdoSampai.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.rdoSampai.LinkedTextBox = "txtHaikiKbn";
            this.rdoSampai.Location = new System.Drawing.Point(258, 0);
            this.rdoSampai.Name = "rdoSampai";
            this.rdoSampai.PopupAfterExecute = null;
            this.rdoSampai.PopupBeforeExecute = null;
            this.rdoSampai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoSampai.PopupSearchSendParams")));
            this.rdoSampai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoSampai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoSampai.popupWindowSetting")));
            this.rdoSampai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSampai.RegistCheckMethod")));
            this.rdoSampai.Size = new System.Drawing.Size(67, 17);
            this.rdoSampai.TabIndex = 3;
            this.rdoSampai.Tag = "";
            this.rdoSampai.Text = "3.建廃";
            this.rdoSampai.UseVisualStyleBackColor = true;
            this.rdoSampai.Value = "3";
            // 
            // rdoChokko
            // 
            this.rdoChokko.AutoSize = true;
            this.rdoChokko.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoChokko.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoChokko.FocusOutCheckMethod")));
            this.rdoChokko.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.rdoChokko.LinkedTextBox = "txtHaikiKbn";
            this.rdoChokko.Location = new System.Drawing.Point(27, 0);
            this.rdoChokko.Name = "rdoChokko";
            this.rdoChokko.PopupAfterExecute = null;
            this.rdoChokko.PopupBeforeExecute = null;
            this.rdoChokko.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoChokko.PopupSearchSendParams")));
            this.rdoChokko.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoChokko.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoChokko.popupWindowSetting")));
            this.rdoChokko.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoChokko.RegistCheckMethod")));
            this.rdoChokko.Size = new System.Drawing.Size(109, 17);
            this.rdoChokko.TabIndex = 1;
            this.rdoChokko.Tag = "";
            this.rdoChokko.Text = "1.産廃(直行)";
            this.rdoChokko.UseVisualStyleBackColor = true;
            this.rdoChokko.Value = "1";
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.rdoManiHimoduke);
            this.panel1.Controls.Add(this.rdoSampai);
            this.panel1.Controls.Add(this.rdoTsumikae);
            this.panel1.Controls.Add(this.rdoChokko);
            this.panel1.Controls.Add(this.txtHaikiKbn);
            this.panel1.Location = new System.Drawing.Point(186, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 20);
            this.panel1.TabIndex = 1;
            // 
            // rdoManiHimoduke
            // 
            this.rdoManiHimoduke.AutoSize = true;
            this.rdoManiHimoduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoManiHimoduke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoManiHimoduke.FocusOutCheckMethod")));
            this.rdoManiHimoduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoManiHimoduke.LinkedTextBox = "txtHaikiKbn";
            this.rdoManiHimoduke.Location = new System.Drawing.Point(331, 0);
            this.rdoManiHimoduke.Name = "rdoManiHimoduke";
            this.rdoManiHimoduke.PopupAfterExecute = null;
            this.rdoManiHimoduke.PopupBeforeExecute = null;
            this.rdoManiHimoduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoManiHimoduke.PopupSearchSendParams")));
            this.rdoManiHimoduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoManiHimoduke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoManiHimoduke.popupWindowSetting")));
            this.rdoManiHimoduke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoManiHimoduke.RegistCheckMethod")));
            this.rdoManiHimoduke.Size = new System.Drawing.Size(95, 17);
            this.rdoManiHimoduke.TabIndex = 4;
            this.rdoManiHimoduke.Tag = "";
            this.rdoManiHimoduke.Text = "4.マニ紐付";
            this.rdoManiHimoduke.UseVisualStyleBackColor = true;
            this.rdoManiHimoduke.Value = "4";
            // 
            // txtHaikiKbn
            // 
            this.txtHaikiKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtHaikiKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHaikiKbn.CharacterLimitList = new char[] {
        '1',
        '2',
        '3',
        '4'};
            this.txtHaikiKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHaikiKbn.DisplayItemName = "対象マニフェスト種別";
            this.txtHaikiKbn.DisplayPopUp = null;
            this.txtHaikiKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHaikiKbn.FocusOutCheckMethod")));
            this.txtHaikiKbn.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.txtHaikiKbn.ForeColor = System.Drawing.Color.Black;
            this.txtHaikiKbn.IsInputErrorOccured = false;
            this.txtHaikiKbn.LinkedRadioButtonArray = new string[] {
        "rdoChokko",
        "rdoTsumikae",
        "rdoSampai",
        "rdoManiHimoduke"};
            this.txtHaikiKbn.Location = new System.Drawing.Point(-1, -1);
            this.txtHaikiKbn.Name = "txtHaikiKbn";
            this.txtHaikiKbn.PopupAfterExecute = null;
            this.txtHaikiKbn.PopupBeforeExecute = null;
            this.txtHaikiKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHaikiKbn.PopupSearchSendParams")));
            this.txtHaikiKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHaikiKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHaikiKbn.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHaikiKbn.RangeSetting = rangeSettingDto1;
            this.txtHaikiKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHaikiKbn.RegistCheckMethod")));
            this.txtHaikiKbn.Size = new System.Drawing.Size(20, 20);
            this.txtHaikiKbn.TabIndex = 0;
            this.txtHaikiKbn.Tag = "【1～4】のいずれかで入力してください";
            this.txtHaikiKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtHaikiKbn.WordWrap = false;
            // 
            // lblError
            // 
            this.lblError.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblError.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblError.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lblError.ForeColor = System.Drawing.Color.White;
            this.lblError.Location = new System.Drawing.Point(10, 100);
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(125, 20);
            this.lblError.TabIndex = 11;
            this.lblError.Text = "エラー上限※";
            this.lblError.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFilePathKenpai
            // 
            this.lblFilePathKenpai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblFilePathKenpai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFilePathKenpai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFilePathKenpai.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lblFilePathKenpai.ForeColor = System.Drawing.Color.White;
            this.lblFilePathKenpai.Location = new System.Drawing.Point(10, 75);
            this.lblFilePathKenpai.Name = "lblFilePathKenpai";
            this.lblFilePathKenpai.Size = new System.Drawing.Size(125, 20);
            this.lblFilePathKenpai.TabIndex = 8;
            this.lblFilePathKenpai.Text = "建廃";
            this.lblFilePathKenpai.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFilePathTsumikae
            // 
            this.lblFilePathTsumikae.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblFilePathTsumikae.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFilePathTsumikae.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFilePathTsumikae.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lblFilePathTsumikae.ForeColor = System.Drawing.Color.White;
            this.lblFilePathTsumikae.Location = new System.Drawing.Point(10, 50);
            this.lblFilePathTsumikae.Name = "lblFilePathTsumikae";
            this.lblFilePathTsumikae.Size = new System.Drawing.Size(125, 20);
            this.lblFilePathTsumikae.TabIndex = 5;
            this.lblFilePathTsumikae.Text = "産廃(積替)";
            this.lblFilePathTsumikae.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFilePathChokko
            // 
            this.lblFilePathChokko.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblFilePathChokko.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFilePathChokko.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblFilePathChokko.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblFilePathChokko.ForeColor = System.Drawing.Color.White;
            this.lblFilePathChokko.Location = new System.Drawing.Point(10, 25);
            this.lblFilePathChokko.Name = "lblFilePathChokko";
            this.lblFilePathChokko.Size = new System.Drawing.Size(125, 20);
            this.lblFilePathChokko.TabIndex = 2;
            this.lblFilePathChokko.Text = "産廃(直行)";
            this.lblFilePathChokko.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtError
            // 
            this.txtError.BackColor = System.Drawing.SystemColors.Window;
            this.txtError.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtError.CustomFormatSetting = "#,###";
            this.txtError.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtError.DisplayItemName = "エラー上限";
            this.txtError.DisplayPopUp = null;
            this.txtError.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtError.FocusOutCheckMethod")));
            this.txtError.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.txtError.ForeColor = System.Drawing.Color.Black;
            this.txtError.FormatSetting = "カスタム";
            this.txtError.IsInputErrorOccured = false;
            this.txtError.ItemDefinedTypes = "smallint";
            this.txtError.Location = new System.Drawing.Point(139, 100);
            this.txtError.Name = "txtError";
            this.txtError.PopupAfterExecute = null;
            this.txtError.PopupBeforeExecute = null;
            this.txtError.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtError.PopupSearchSendParams")));
            this.txtError.PopupSendParams = new string[0];
            this.txtError.PopupSetFormField = "";
            this.txtError.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtError.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtError.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.txtError.RangeSetting = rangeSettingDto2;
            this.txtError.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtError.RegistCheckMethod")));
            this.txtError.SetFormField = "";
            this.txtError.ShortItemName = "エラー上限";
            this.txtError.Size = new System.Drawing.Size(60, 20);
            this.txtError.TabIndex = 12;
            this.txtError.Tag = "エラーファイルに出力するエラー件数の上限を入力してください";
            this.txtError.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtError.WordWrap = false;
            // 
            // btnBrowseChokko
            // 
            this.btnBrowseChokko.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnBrowseChokko.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnBrowseChokko.Location = new System.Drawing.Point(792, 24);
            this.btnBrowseChokko.Name = "btnBrowseChokko";
            this.btnBrowseChokko.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnBrowseChokko.Size = new System.Drawing.Size(53, 22);
            this.btnBrowseChokko.TabIndex = 4;
            this.btnBrowseChokko.Tag = "";
            this.btnBrowseChokko.Text = "参照";
            this.btnBrowseChokko.UseVisualStyleBackColor = false;
            // 
            // txtFilePathChokko
            // 
            this.txtFilePathChokko.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilePathChokko.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePathChokko.DBFieldsName = "";
            this.txtFilePathChokko.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtFilePathChokko.DisplayItemName = "";
            this.txtFilePathChokko.DisplayPopUp = null;
            this.txtFilePathChokko.FocusOutCheckMethod = null;
            this.txtFilePathChokko.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.txtFilePathChokko.ForeColor = System.Drawing.Color.Black;
            this.txtFilePathChokko.GetCodeMasterField = "";
            this.txtFilePathChokko.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtFilePathChokko.IsInputErrorOccured = false;
            this.txtFilePathChokko.ItemDefinedTypes = "varchar";
            this.txtFilePathChokko.Location = new System.Drawing.Point(139, 25);
            this.txtFilePathChokko.Name = "txtFilePathChokko";
            this.txtFilePathChokko.PopupAfterExecute = null;
            this.txtFilePathChokko.PopupBeforeExecute = null;
            this.txtFilePathChokko.PopupGetMasterField = "";
            this.txtFilePathChokko.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtFilePathChokko.PopupSearchSendParams")));
            this.txtFilePathChokko.PopupSendParams = new string[0];
            this.txtFilePathChokko.PopupSetFormField = "";
            this.txtFilePathChokko.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtFilePathChokko.PopupWindowName = "";
            this.txtFilePathChokko.popupWindowSetting = null;
            this.txtFilePathChokko.RegistCheckMethod = null;
            this.txtFilePathChokko.SetFormField = "";
            this.txtFilePathChokko.Size = new System.Drawing.Size(647, 20);
            this.txtFilePathChokko.TabIndex = 3;
            this.txtFilePathChokko.Tag = "産廃(直行)のファイルを指定してください";
            // 
            // txtFilePathTsumikae
            // 
            this.txtFilePathTsumikae.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilePathTsumikae.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePathTsumikae.DBFieldsName = "";
            this.txtFilePathTsumikae.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtFilePathTsumikae.DisplayItemName = "";
            this.txtFilePathTsumikae.DisplayPopUp = null;
            this.txtFilePathTsumikae.FocusOutCheckMethod = null;
            this.txtFilePathTsumikae.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.txtFilePathTsumikae.ForeColor = System.Drawing.Color.Black;
            this.txtFilePathTsumikae.GetCodeMasterField = "";
            this.txtFilePathTsumikae.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtFilePathTsumikae.IsInputErrorOccured = false;
            this.txtFilePathTsumikae.ItemDefinedTypes = "varchar";
            this.txtFilePathTsumikae.Location = new System.Drawing.Point(139, 50);
            this.txtFilePathTsumikae.Name = "txtFilePathTsumikae";
            this.txtFilePathTsumikae.PopupAfterExecute = null;
            this.txtFilePathTsumikae.PopupBeforeExecute = null;
            this.txtFilePathTsumikae.PopupGetMasterField = "";
            this.txtFilePathTsumikae.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtFilePathTsumikae.PopupSearchSendParams")));
            this.txtFilePathTsumikae.PopupSendParams = new string[0];
            this.txtFilePathTsumikae.PopupSetFormField = "";
            this.txtFilePathTsumikae.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtFilePathTsumikae.PopupWindowName = "";
            this.txtFilePathTsumikae.popupWindowSetting = null;
            this.txtFilePathTsumikae.RegistCheckMethod = null;
            this.txtFilePathTsumikae.SetFormField = "";
            this.txtFilePathTsumikae.Size = new System.Drawing.Size(647, 20);
            this.txtFilePathTsumikae.TabIndex = 6;
            this.txtFilePathTsumikae.Tag = "産廃(積替)のファイルを指定してください";
            // 
            // btnBrowseTsumikae
            // 
            this.btnBrowseTsumikae.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnBrowseTsumikae.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnBrowseTsumikae.Location = new System.Drawing.Point(792, 49);
            this.btnBrowseTsumikae.Name = "btnBrowseTsumikae";
            this.btnBrowseTsumikae.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnBrowseTsumikae.Size = new System.Drawing.Size(53, 22);
            this.btnBrowseTsumikae.TabIndex = 7;
            this.btnBrowseTsumikae.Tag = "";
            this.btnBrowseTsumikae.Text = "参照";
            this.btnBrowseTsumikae.UseVisualStyleBackColor = false;
            // 
            // txtFilePathKenpai
            // 
            this.txtFilePathKenpai.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilePathKenpai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePathKenpai.DBFieldsName = "";
            this.txtFilePathKenpai.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtFilePathKenpai.DisplayItemName = "";
            this.txtFilePathKenpai.DisplayPopUp = null;
            this.txtFilePathKenpai.FocusOutCheckMethod = null;
            this.txtFilePathKenpai.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.txtFilePathKenpai.ForeColor = System.Drawing.Color.Black;
            this.txtFilePathKenpai.GetCodeMasterField = "";
            this.txtFilePathKenpai.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtFilePathKenpai.IsInputErrorOccured = false;
            this.txtFilePathKenpai.ItemDefinedTypes = "varchar";
            this.txtFilePathKenpai.Location = new System.Drawing.Point(139, 75);
            this.txtFilePathKenpai.Name = "txtFilePathKenpai";
            this.txtFilePathKenpai.PopupAfterExecute = null;
            this.txtFilePathKenpai.PopupBeforeExecute = null;
            this.txtFilePathKenpai.PopupGetMasterField = "";
            this.txtFilePathKenpai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtFilePathKenpai.PopupSearchSendParams")));
            this.txtFilePathKenpai.PopupSendParams = new string[0];
            this.txtFilePathKenpai.PopupSetFormField = "";
            this.txtFilePathKenpai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtFilePathKenpai.PopupWindowName = "";
            this.txtFilePathKenpai.popupWindowSetting = null;
            this.txtFilePathKenpai.RegistCheckMethod = null;
            this.txtFilePathKenpai.SetFormField = "";
            this.txtFilePathKenpai.Size = new System.Drawing.Size(647, 20);
            this.txtFilePathKenpai.TabIndex = 9;
            this.txtFilePathKenpai.Tag = "建廃のファイルを指定してください";
            // 
            // btnBrowseKenpai
            // 
            this.btnBrowseKenpai.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnBrowseKenpai.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnBrowseKenpai.Location = new System.Drawing.Point(792, 74);
            this.btnBrowseKenpai.Name = "btnBrowseKenpai";
            this.btnBrowseKenpai.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnBrowseKenpai.Size = new System.Drawing.Size(53, 22);
            this.btnBrowseKenpai.TabIndex = 10;
            this.btnBrowseKenpai.Tag = "";
            this.btnBrowseKenpai.Text = "参照";
            this.btnBrowseKenpai.UseVisualStyleBackColor = false;
            // 
            // txtImportStatus
            // 
            this.txtImportStatus.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtImportStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtImportStatus.DBFieldsName = "";
            this.txtImportStatus.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtImportStatus.DisplayItemName = "";
            this.txtImportStatus.DisplayPopUp = null;
            this.txtImportStatus.FocusOutCheckMethod = null;
            this.txtImportStatus.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.txtImportStatus.ForeColor = System.Drawing.Color.Black;
            this.txtImportStatus.GetCodeMasterField = "";
            this.txtImportStatus.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtImportStatus.IsInputErrorOccured = false;
            this.txtImportStatus.ItemDefinedTypes = "varchar";
            this.txtImportStatus.Location = new System.Drawing.Point(12, 175);
            this.txtImportStatus.Multiline = true;
            this.txtImportStatus.Name = "txtImportStatus";
            this.txtImportStatus.PopupAfterExecute = null;
            this.txtImportStatus.PopupBeforeExecute = null;
            this.txtImportStatus.PopupGetMasterField = "";
            this.txtImportStatus.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtImportStatus.PopupSearchSendParams")));
            this.txtImportStatus.PopupSendParams = new string[0];
            this.txtImportStatus.PopupSetFormField = "";
            this.txtImportStatus.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtImportStatus.PopupWindowName = "";
            this.txtImportStatus.popupWindowSetting = null;
            this.txtImportStatus.ReadOnly = true;
            this.txtImportStatus.RegistCheckMethod = null;
            this.txtImportStatus.SetFormField = "";
            this.txtImportStatus.Size = new System.Drawing.Size(976, 303);
            this.txtImportStatus.TabIndex = 20;
            this.txtImportStatus.TabStop = false;
            this.txtImportStatus.Tag = "";
            // 
            // ManiHimoduke
            // 
            this.ManiHimoduke.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.ManiHimoduke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ManiHimoduke.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManiHimoduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ManiHimoduke.ForeColor = System.Drawing.Color.White;
            this.ManiHimoduke.Location = new System.Drawing.Point(10, 125);
            this.ManiHimoduke.Name = "ManiHimoduke";
            this.ManiHimoduke.Size = new System.Drawing.Size(125, 20);
            this.ManiHimoduke.TabIndex = 13;
            this.ManiHimoduke.Text = "マニ紐付";
            this.ManiHimoduke.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCreateDate
            // 
            this.lblCreateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblCreateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCreateDate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCreateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblCreateDate.ForeColor = System.Drawing.Color.White;
            this.lblCreateDate.Location = new System.Drawing.Point(10, 150);
            this.lblCreateDate.Name = "lblCreateDate";
            this.lblCreateDate.Size = new System.Drawing.Size(125, 20);
            this.lblCreateDate.TabIndex = 16;
            this.lblCreateDate.Text = "作成日期間";
            this.lblCreateDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dateTimeCreateDateFrom
            // 
            this.dateTimeCreateDateFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dateTimeCreateDateFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimeCreateDateFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dateTimeCreateDateFrom.Checked = false;
            this.dateTimeCreateDateFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dateTimeCreateDateFrom.DateTimeNowYear = "";
            this.dateTimeCreateDateFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dateTimeCreateDateFrom.DisplayItemName = "交付年月日FROM";
            this.dateTimeCreateDateFrom.DisplayPopUp = null;
            this.dateTimeCreateDateFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dateTimeCreateDateFrom.FocusOutCheckMethod")));
            this.dateTimeCreateDateFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dateTimeCreateDateFrom.ForeColor = System.Drawing.Color.Black;
            this.dateTimeCreateDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeCreateDateFrom.IsInputErrorOccured = false;
            this.dateTimeCreateDateFrom.Location = new System.Drawing.Point(139, 150);
            this.dateTimeCreateDateFrom.MaxLength = 10;
            this.dateTimeCreateDateFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dateTimeCreateDateFrom.Name = "dateTimeCreateDateFrom";
            this.dateTimeCreateDateFrom.NullValue = "";
            this.dateTimeCreateDateFrom.PopupAfterExecute = null;
            this.dateTimeCreateDateFrom.PopupBeforeExecute = null;
            this.dateTimeCreateDateFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dateTimeCreateDateFrom.PopupSearchSendParams")));
            this.dateTimeCreateDateFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dateTimeCreateDateFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dateTimeCreateDateFrom.popupWindowSetting")));
            this.dateTimeCreateDateFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dateTimeCreateDateFrom.RegistCheckMethod")));
            this.dateTimeCreateDateFrom.Size = new System.Drawing.Size(138, 20);
            this.dateTimeCreateDateFrom.TabIndex = 17;
            this.dateTimeCreateDateFrom.Tag = "日付を選択してください";
            this.dateTimeCreateDateFrom.Text = "2013/12/09(月)";
            this.dateTimeCreateDateFrom.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // dateTimeCreateDateTo
            // 
            this.dateTimeCreateDateTo.BackColor = System.Drawing.SystemColors.Window;
            this.dateTimeCreateDateTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dateTimeCreateDateTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dateTimeCreateDateTo.Checked = false;
            this.dateTimeCreateDateTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dateTimeCreateDateTo.DateTimeNowYear = "";
            this.dateTimeCreateDateTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dateTimeCreateDateTo.DisplayItemName = "交付年月日FROM";
            this.dateTimeCreateDateTo.DisplayPopUp = null;
            this.dateTimeCreateDateTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dateTimeCreateDateTo.FocusOutCheckMethod")));
            this.dateTimeCreateDateTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dateTimeCreateDateTo.ForeColor = System.Drawing.Color.Black;
            this.dateTimeCreateDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeCreateDateTo.IsInputErrorOccured = false;
            this.dateTimeCreateDateTo.Location = new System.Drawing.Point(308, 150);
            this.dateTimeCreateDateTo.MaxLength = 10;
            this.dateTimeCreateDateTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dateTimeCreateDateTo.Name = "dateTimeCreateDateTo";
            this.dateTimeCreateDateTo.NullValue = "";
            this.dateTimeCreateDateTo.PopupAfterExecute = null;
            this.dateTimeCreateDateTo.PopupBeforeExecute = null;
            this.dateTimeCreateDateTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dateTimeCreateDateTo.PopupSearchSendParams")));
            this.dateTimeCreateDateTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dateTimeCreateDateTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dateTimeCreateDateTo.popupWindowSetting")));
            this.dateTimeCreateDateTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dateTimeCreateDateTo.RegistCheckMethod")));
            this.dateTimeCreateDateTo.Size = new System.Drawing.Size(138, 20);
            this.dateTimeCreateDateTo.TabIndex = 19;
            this.dateTimeCreateDateTo.Tag = "日付を選択してください";
            this.dateTimeCreateDateTo.Text = "2013/12/09(月)";
            this.dateTimeCreateDateTo.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // txtFilePathManiHimoduke
            // 
            this.txtFilePathManiHimoduke.BackColor = System.Drawing.SystemColors.Window;
            this.txtFilePathManiHimoduke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePathManiHimoduke.DBFieldsName = "";
            this.txtFilePathManiHimoduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtFilePathManiHimoduke.DisplayItemName = "";
            this.txtFilePathManiHimoduke.DisplayPopUp = null;
            this.txtFilePathManiHimoduke.FocusOutCheckMethod = null;
            this.txtFilePathManiHimoduke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtFilePathManiHimoduke.ForeColor = System.Drawing.Color.Black;
            this.txtFilePathManiHimoduke.GetCodeMasterField = "";
            this.txtFilePathManiHimoduke.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.txtFilePathManiHimoduke.IsInputErrorOccured = false;
            this.txtFilePathManiHimoduke.ItemDefinedTypes = "varchar";
            this.txtFilePathManiHimoduke.Location = new System.Drawing.Point(139, 125);
            this.txtFilePathManiHimoduke.Name = "txtFilePathManiHimoduke";
            this.txtFilePathManiHimoduke.PopupAfterExecute = null;
            this.txtFilePathManiHimoduke.PopupBeforeExecute = null;
            this.txtFilePathManiHimoduke.PopupGetMasterField = "";
            this.txtFilePathManiHimoduke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtFilePathManiHimoduke.PopupSearchSendParams")));
            this.txtFilePathManiHimoduke.PopupSendParams = new string[0];
            this.txtFilePathManiHimoduke.PopupSetFormField = "";
            this.txtFilePathManiHimoduke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtFilePathManiHimoduke.PopupWindowName = "";
            this.txtFilePathManiHimoduke.popupWindowSetting = null;
            this.txtFilePathManiHimoduke.RegistCheckMethod = null;
            this.txtFilePathManiHimoduke.SetFormField = "";
            this.txtFilePathManiHimoduke.Size = new System.Drawing.Size(647, 20);
            this.txtFilePathManiHimoduke.TabIndex = 14;
            this.txtFilePathManiHimoduke.Tag = "マニ紐付のファイルを指定してください";
            // 
            // btnBrowseManiHimoduke
            // 
            this.btnBrowseManiHimoduke.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btnBrowseManiHimoduke.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnBrowseManiHimoduke.Location = new System.Drawing.Point(792, 123);
            this.btnBrowseManiHimoduke.Name = "btnBrowseManiHimoduke";
            this.btnBrowseManiHimoduke.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnBrowseManiHimoduke.Size = new System.Drawing.Size(53, 22);
            this.btnBrowseManiHimoduke.TabIndex = 15;
            this.btnBrowseManiHimoduke.Tag = "";
            this.btnBrowseManiHimoduke.Text = "参照";
            this.btnBrowseManiHimoduke.UseVisualStyleBackColor = false;
            // 
            // lblCreateDateKara
            // 
            this.lblCreateDateKara.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblCreateDateKara.Location = new System.Drawing.Point(285, 150);
            this.lblCreateDateKara.Name = "lblCreateDateKara";
            this.lblCreateDateKara.Size = new System.Drawing.Size(15, 20);
            this.lblCreateDateKara.TabIndex = 18;
            this.lblCreateDateKara.Text = "～";
            this.lblCreateDateKara.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.lblCreateDateKara);
            this.Controls.Add(this.btnBrowseManiHimoduke);
            this.Controls.Add(this.txtFilePathManiHimoduke);
            this.Controls.Add(this.dateTimeCreateDateTo);
            this.Controls.Add(this.dateTimeCreateDateFrom);
            this.Controls.Add(this.lblCreateDate);
            this.Controls.Add(this.ManiHimoduke);
            this.Controls.Add(this.txtError);
            this.Controls.Add(this.btnBrowseKenpai);
            this.Controls.Add(this.btnBrowseTsumikae);
            this.Controls.Add(this.btnBrowseChokko);
            this.Controls.Add(this.txtImportStatus);
            this.Controls.Add(this.txtFilePathKenpai);
            this.Controls.Add(this.txtFilePathTsumikae);
            this.Controls.Add(this.txtFilePathChokko);
            this.Controls.Add(this.lblFilePathChokko);
            this.Controls.Add(this.lblError);
            this.Controls.Add(this.lblFilePathKenpai);
            this.Controls.Add(this.lblFilePathTsumikae);
            this.Controls.Add(this.lblHaikiKbn);
            this.Controls.Add(this.panel1);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomRadioButton rdoTsumikae;
        public System.Windows.Forms.Label lblHaikiKbn;
        public r_framework.CustomControl.CustomRadioButton rdoSampai;
        public r_framework.CustomControl.CustomRadioButton rdoChokko;
        private r_framework.CustomControl.CustomPanel panel1;
        public r_framework.CustomControl.CustomNumericTextBox2 txtHaikiKbn;
        internal System.Windows.Forms.Label lblError;
        internal System.Windows.Forms.Label lblFilePathKenpai;
        internal System.Windows.Forms.Label lblFilePathTsumikae;
        public System.Windows.Forms.Label lblFilePathChokko;
        public r_framework.CustomControl.CustomNumericTextBox2 txtError;
        public r_framework.CustomControl.CustomButton btnBrowseChokko;
        public r_framework.CustomControl.CustomTextBox txtFilePathChokko;
        public r_framework.CustomControl.CustomTextBox txtFilePathTsumikae;
        public r_framework.CustomControl.CustomButton btnBrowseTsumikae;
        public r_framework.CustomControl.CustomTextBox txtFilePathKenpai;
        public r_framework.CustomControl.CustomButton btnBrowseKenpai;
        public r_framework.CustomControl.CustomTextBox txtImportStatus;
        internal System.Windows.Forms.Label ManiHimoduke;
        internal System.Windows.Forms.Label lblCreateDate;
        internal r_framework.CustomControl.CustomDateTimePicker dateTimeCreateDateFrom;
        internal r_framework.CustomControl.CustomDateTimePicker dateTimeCreateDateTo;
        public r_framework.CustomControl.CustomTextBox txtFilePathManiHimoduke;
        public r_framework.CustomControl.CustomButton btnBrowseManiHimoduke;
        public r_framework.CustomControl.CustomRadioButton rdoManiHimoduke;
        private System.Windows.Forms.Label lblCreateDateKara;
    }
}