namespace Shougun.Core.SalesPayment.SyukkaNyuuryoku2
{
    partial class UIHeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KYOTEN_LABEL = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.CreateUser = new r_framework.CustomControl.CustomTextBox();
            this.CreateDate = new r_framework.CustomControl.CustomTextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.LastUpdateUser = new r_framework.CustomControl.CustomTextBox();
            this.LastUpdateDate = new r_framework.CustomControl.CustomTextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.KEIRYOU_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.UKETSUKE_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KEIRYOU_NUMBER_LABEL = new System.Windows.Forms.Label();
            this.UKETSUKE_NUMBER_LABEL = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.KENSHU_MUST_KBN = new r_framework.CustomControl.CustomCheckBox();
            this.label21 = new System.Windows.Forms.Label();
            this.txtKensyuu = new r_framework.CustomControl.CustomTextBox();
            this.lblKensyuu = new System.Windows.Forms.Label();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.lb_title.Size = new System.Drawing.Size(178, 34);
            this.lb_title.Text = "出荷入力";
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.CharacterLimitList = null;
            this.KYOTEN_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.KYOTEN_NAME_RYAKU.DBFieldsName = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(646, 2);
            this.KYOTEN_NAME_RYAKU.MaxLength = 0;
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 26);
            this.KYOTEN_NAME_RYAKU.TabIndex = 507;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = " ";
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.CustomFormatSetting = "00";
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点CD";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.FormatSetting = "カスタム";
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(611, 2);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(36, 26);
            this.KYOTEN_CD.TabIndex = 0;
            this.KYOTEN_CD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
            // 
            // KYOTEN_LABEL
            // 
            this.KYOTEN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.KYOTEN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KYOTEN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KYOTEN_LABEL.ForeColor = System.Drawing.Color.White;
            this.KYOTEN_LABEL.Location = new System.Drawing.Point(510, 2);
            this.KYOTEN_LABEL.Name = "KYOTEN_LABEL";
            this.KYOTEN_LABEL.Size = new System.Drawing.Size(98, 25);
            this.KYOTEN_LABEL.TabIndex = 506;
            this.KYOTEN_LABEL.Text = "拠点※";
            this.KYOTEN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 40F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Aqua;
            this.label1.Location = new System.Drawing.Point(270, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 62);
            this.label1.TabIndex = 497;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // CreateUser
            // 
            this.CreateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CreateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateUser.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CreateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.CreateUser.DisplayPopUp = null;
            this.CreateUser.ErrorMessage = "";
            this.CreateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateUser.FocusOutCheckMethod")));
            this.CreateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateUser.ForeColor = System.Drawing.Color.Black;
            this.CreateUser.GetCodeMasterField = "";
            this.CreateUser.IsInputErrorOccured = false;
            this.CreateUser.Location = new System.Drawing.Point(856, 0);
            this.CreateUser.MaxLength = 0;
            this.CreateUser.Name = "CreateUser";
            this.CreateUser.PopupAfterExecute = null;
            this.CreateUser.PopupBeforeExecute = null;
            this.CreateUser.PopupGetMasterField = "";
            this.CreateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateUser.PopupSearchSendParams")));
            this.CreateUser.PopupSetFormField = "";
            this.CreateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CreateUser.popupWindowSetting")));
            this.CreateUser.ReadOnly = true;
            this.CreateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateUser.RegistCheckMethod")));
            this.CreateUser.SetFormField = "";
            this.CreateUser.Size = new System.Drawing.Size(160, 20);
            this.CreateUser.TabIndex = 767;
            this.CreateUser.TabStop = false;
            this.CreateUser.Tag = "初回登録者が表示されます。";
            this.CreateUser.Visible = false;
            // 
            // CreateDate
            // 
            this.CreateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CreateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CreateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.CreateDate.DisplayPopUp = null;
            this.CreateDate.ErrorMessage = "";
            this.CreateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateDate.FocusOutCheckMethod")));
            this.CreateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateDate.ForeColor = System.Drawing.Color.Black;
            this.CreateDate.GetCodeMasterField = "";
            this.CreateDate.IsInputErrorOccured = false;
            this.CreateDate.Location = new System.Drawing.Point(1015, 0);
            this.CreateDate.MaxLength = 0;
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.PopupAfterExecute = null;
            this.CreateDate.PopupBeforeExecute = null;
            this.CreateDate.PopupGetMasterField = "";
            this.CreateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateDate.PopupSearchSendParams")));
            this.CreateDate.PopupSetFormField = "";
            this.CreateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CreateDate.popupWindowSetting")));
            this.CreateDate.ReadOnly = true;
            this.CreateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateDate.RegistCheckMethod")));
            this.CreateDate.SetFormField = "";
            this.CreateDate.Size = new System.Drawing.Size(160, 20);
            this.CreateDate.TabIndex = 768;
            this.CreateDate.TabStop = false;
            this.CreateDate.Tag = "初回登録日が表示されます。";
            this.CreateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CreateDate.Visible = false;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label36.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label36.ForeColor = System.Drawing.Color.White;
            this.label36.Location = new System.Drawing.Point(776, 0);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(75, 20);
            this.label36.TabIndex = 769;
            this.label36.Text = "初回登録";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label36.Visible = false;
            // 
            // LastUpdateUser
            // 
            this.LastUpdateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LastUpdateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastUpdateUser.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.LastUpdateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.LastUpdateUser.DisplayPopUp = null;
            this.LastUpdateUser.ErrorMessage = "";
            this.LastUpdateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateUser.FocusOutCheckMethod")));
            this.LastUpdateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateUser.ForeColor = System.Drawing.Color.Black;
            this.LastUpdateUser.GetCodeMasterField = "";
            this.LastUpdateUser.IsInputErrorOccured = false;
            this.LastUpdateUser.Location = new System.Drawing.Point(856, 22);
            this.LastUpdateUser.MaxLength = 0;
            this.LastUpdateUser.Name = "LastUpdateUser";
            this.LastUpdateUser.PopupAfterExecute = null;
            this.LastUpdateUser.PopupBeforeExecute = null;
            this.LastUpdateUser.PopupGetMasterField = "";
            this.LastUpdateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateUser.PopupSearchSendParams")));
            this.LastUpdateUser.PopupSetFormField = "";
            this.LastUpdateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LastUpdateUser.popupWindowSetting")));
            this.LastUpdateUser.ReadOnly = true;
            this.LastUpdateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateUser.RegistCheckMethod")));
            this.LastUpdateUser.SetFormField = "";
            this.LastUpdateUser.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateUser.TabIndex = 770;
            this.LastUpdateUser.TabStop = false;
            this.LastUpdateUser.Tag = "最終更新者が表示されます。";
            this.LastUpdateUser.Visible = false;
            // 
            // LastUpdateDate
            // 
            this.LastUpdateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LastUpdateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastUpdateDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.LastUpdateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.LastUpdateDate.DisplayPopUp = null;
            this.LastUpdateDate.ErrorMessage = "";
            this.LastUpdateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateDate.FocusOutCheckMethod")));
            this.LastUpdateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateDate.ForeColor = System.Drawing.Color.Black;
            this.LastUpdateDate.GetCodeMasterField = "";
            this.LastUpdateDate.IsInputErrorOccured = false;
            this.LastUpdateDate.Location = new System.Drawing.Point(1015, 22);
            this.LastUpdateDate.MaxLength = 0;
            this.LastUpdateDate.Name = "LastUpdateDate";
            this.LastUpdateDate.PopupAfterExecute = null;
            this.LastUpdateDate.PopupBeforeExecute = null;
            this.LastUpdateDate.PopupGetMasterField = "";
            this.LastUpdateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateDate.PopupSearchSendParams")));
            this.LastUpdateDate.PopupSetFormField = "";
            this.LastUpdateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LastUpdateDate.popupWindowSetting")));
            this.LastUpdateDate.ReadOnly = true;
            this.LastUpdateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateDate.RegistCheckMethod")));
            this.LastUpdateDate.SetFormField = "";
            this.LastUpdateDate.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateDate.TabIndex = 771;
            this.LastUpdateDate.TabStop = false;
            this.LastUpdateDate.Tag = "最終更新日が表示されます。";
            this.LastUpdateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LastUpdateDate.Visible = false;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label35.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(776, 22);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(75, 20);
            this.label35.TabIndex = 772;
            this.label35.Text = "最終更新";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label35.Visible = false;
            // 
            // KEIRYOU_NUMBER
            // 
            this.KEIRYOU_NUMBER.BackColor = System.Drawing.SystemColors.Window;
            this.KEIRYOU_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIRYOU_NUMBER.DBFieldsName = "UKETSUKE_NUMBER";
            this.KEIRYOU_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIRYOU_NUMBER.DisplayItemName = "計量番号";
            this.KEIRYOU_NUMBER.DisplayPopUp = null;
            this.KEIRYOU_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIRYOU_NUMBER.FocusOutCheckMethod")));
            this.KEIRYOU_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KEIRYOU_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.KEIRYOU_NUMBER.IsInputErrorOccured = false;
            this.KEIRYOU_NUMBER.ItemDefinedTypes = "bigint";
            this.KEIRYOU_NUMBER.Location = new System.Drawing.Point(979, 30);
            this.KEIRYOU_NUMBER.Name = "KEIRYOU_NUMBER";
            this.KEIRYOU_NUMBER.PopupAfterExecute = null;
            this.KEIRYOU_NUMBER.PopupBeforeExecute = null;
            this.KEIRYOU_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIRYOU_NUMBER.PopupSearchSendParams")));
            this.KEIRYOU_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIRYOU_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIRYOU_NUMBER.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.KEIRYOU_NUMBER.RangeSetting = rangeSettingDto2;
            this.KEIRYOU_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIRYOU_NUMBER.RegistCheckMethod")));
            this.KEIRYOU_NUMBER.Size = new System.Drawing.Size(106, 26);
            this.KEIRYOU_NUMBER.TabIndex = 2;
            this.KEIRYOU_NUMBER.Tag = "半角10桁以内で入力してください";
            this.KEIRYOU_NUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.KEIRYOU_NUMBER.WordWrap = false;
            this.KEIRYOU_NUMBER.TextChanged += new System.EventHandler(this.KEIRYOU_NUMBER_TextChanged);
            this.KEIRYOU_NUMBER.Validated += new System.EventHandler(this.KEIRYOU_NUMBER_Validated);
            // 
            // UKETSUKE_NUMBER
            // 
            this.UKETSUKE_NUMBER.BackColor = System.Drawing.SystemColors.Window;
            this.UKETSUKE_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UKETSUKE_NUMBER.DBFieldsName = "UKETSUKE_NUMBER";
            this.UKETSUKE_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_NUMBER.DisplayItemName = "受付番号";
            this.UKETSUKE_NUMBER.DisplayPopUp = null;
            this.UKETSUKE_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_NUMBER.FocusOutCheckMethod")));
            this.UKETSUKE_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.UKETSUKE_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.UKETSUKE_NUMBER.IsInputErrorOccured = false;
            this.UKETSUKE_NUMBER.ItemDefinedTypes = "bigint";
            this.UKETSUKE_NUMBER.Location = new System.Drawing.Point(979, 2);
            this.UKETSUKE_NUMBER.Name = "UKETSUKE_NUMBER";
            this.UKETSUKE_NUMBER.PopupAfterExecute = null;
            this.UKETSUKE_NUMBER.PopupBeforeExecute = null;
            this.UKETSUKE_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UKETSUKE_NUMBER.PopupSearchSendParams")));
            this.UKETSUKE_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UKETSUKE_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UKETSUKE_NUMBER.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.UKETSUKE_NUMBER.RangeSetting = rangeSettingDto3;
            this.UKETSUKE_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_NUMBER.RegistCheckMethod")));
            this.UKETSUKE_NUMBER.Size = new System.Drawing.Size(106, 26);
            this.UKETSUKE_NUMBER.TabIndex = 1;
            this.UKETSUKE_NUMBER.Tag = "半角10桁以内で入力してください";
            this.UKETSUKE_NUMBER.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UKETSUKE_NUMBER.WordWrap = false;
            this.UKETSUKE_NUMBER.TextChanged += new System.EventHandler(this.UKETSUKE_NUMBER_TextChanged);
            this.UKETSUKE_NUMBER.Validated += new System.EventHandler(this.UKETSUKE_NUMBER_Validated);
            // 
            // KEIRYOU_NUMBER_LABEL
            // 
            this.KEIRYOU_NUMBER_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.KEIRYOU_NUMBER_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIRYOU_NUMBER_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KEIRYOU_NUMBER_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.KEIRYOU_NUMBER_LABEL.ForeColor = System.Drawing.Color.White;
            this.KEIRYOU_NUMBER_LABEL.Location = new System.Drawing.Point(878, 31);
            this.KEIRYOU_NUMBER_LABEL.Name = "KEIRYOU_NUMBER_LABEL";
            this.KEIRYOU_NUMBER_LABEL.Size = new System.Drawing.Size(98, 25);
            this.KEIRYOU_NUMBER_LABEL.TabIndex = 779;
            this.KEIRYOU_NUMBER_LABEL.Text = "計量番号";
            this.KEIRYOU_NUMBER_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UKETSUKE_NUMBER_LABEL
            // 
            this.UKETSUKE_NUMBER_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.UKETSUKE_NUMBER_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UKETSUKE_NUMBER_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UKETSUKE_NUMBER_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.UKETSUKE_NUMBER_LABEL.ForeColor = System.Drawing.Color.White;
            this.UKETSUKE_NUMBER_LABEL.Location = new System.Drawing.Point(878, 3);
            this.UKETSUKE_NUMBER_LABEL.Name = "UKETSUKE_NUMBER_LABEL";
            this.UKETSUKE_NUMBER_LABEL.Size = new System.Drawing.Size(98, 25);
            this.UKETSUKE_NUMBER_LABEL.TabIndex = 780;
            this.UKETSUKE_NUMBER_LABEL.Text = "受付番号";
            this.UKETSUKE_NUMBER_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.KENSHU_MUST_KBN);
            this.customPanel2.Location = new System.Drawing.Point(611, 30);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(36, 26);
            this.customPanel2.TabIndex = 1018;
            // 
            // KENSHU_MUST_KBN
            // 
            this.KENSHU_MUST_KBN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.KENSHU_MUST_KBN.DBFieldsName = "KENSHU_MUST_KBN";
            this.KENSHU_MUST_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENSHU_MUST_KBN.DisplayItemName = "要検収";
            this.KENSHU_MUST_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSHU_MUST_KBN.FocusOutCheckMethod")));
            this.KENSHU_MUST_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KENSHU_MUST_KBN.ItemDefinedTypes = "bit";
            this.KENSHU_MUST_KBN.Location = new System.Drawing.Point(10, 5);
            this.KENSHU_MUST_KBN.Name = "KENSHU_MUST_KBN";
            this.KENSHU_MUST_KBN.PopupAfterExecute = null;
            this.KENSHU_MUST_KBN.PopupBeforeExecute = null;
            this.KENSHU_MUST_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENSHU_MUST_KBN.PopupSearchSendParams")));
            this.KENSHU_MUST_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENSHU_MUST_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENSHU_MUST_KBN.popupWindowSetting")));
            this.KENSHU_MUST_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENSHU_MUST_KBN.RegistCheckMethod")));
            this.KENSHU_MUST_KBN.Size = new System.Drawing.Size(18, 17);
            this.KENSHU_MUST_KBN.TabIndex = 15;
            this.KENSHU_MUST_KBN.TabStop = false;
            this.KENSHU_MUST_KBN.UseVisualStyleBackColor = false;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(510, 30);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(98, 26);
            this.label21.TabIndex = 1019;
            this.label21.Text = "要検収";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtKensyuu
            // 
            this.txtKensyuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKensyuu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKensyuu.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtKensyuu.Cursor = System.Windows.Forms.Cursors.Default;
            this.txtKensyuu.DBFieldsName = "";
            this.txtKensyuu.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKensyuu.DisplayPopUp = null;
            this.txtKensyuu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKensyuu.FocusOutCheckMethod")));
            this.txtKensyuu.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.txtKensyuu.ForeColor = System.Drawing.Color.Black;
            this.txtKensyuu.IsInputErrorOccured = false;
            this.txtKensyuu.ItemDefinedTypes = "";
            this.txtKensyuu.Location = new System.Drawing.Point(747, 30);
            this.txtKensyuu.MaxLength = 0;
            this.txtKensyuu.Name = "txtKensyuu";
            this.txtKensyuu.PopupAfterExecute = null;
            this.txtKensyuu.PopupBeforeExecute = null;
            this.txtKensyuu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKensyuu.PopupSearchSendParams")));
            this.txtKensyuu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKensyuu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKensyuu.popupWindowSetting")));
            this.txtKensyuu.ReadOnly = true;
            this.txtKensyuu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKensyuu.RegistCheckMethod")));
            this.txtKensyuu.Size = new System.Drawing.Size(105, 26);
            this.txtKensyuu.TabIndex = 1016;
            this.txtKensyuu.TabStop = false;
            this.txtKensyuu.Tag = " ";
            // 
            // lblKensyuu
            // 
            this.lblKensyuu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(160)))));
            this.lblKensyuu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKensyuu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKensyuu.Font = new System.Drawing.Font("ＭＳ ゴシック", 14F);
            this.lblKensyuu.ForeColor = System.Drawing.Color.White;
            this.lblKensyuu.Location = new System.Drawing.Point(652, 30);
            this.lblKensyuu.Name = "lblKensyuu";
            this.lblKensyuu.Size = new System.Drawing.Size(93, 26);
            this.lblKensyuu.TabIndex = 1017;
            this.lblKensyuu.Text = "検収状況";
            this.lblKensyuu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1180, 73);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.txtKensyuu);
            this.Controls.Add(this.lblKensyuu);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KEIRYOU_NUMBER);
            this.Controls.Add(this.UKETSUKE_NUMBER);
            this.Controls.Add(this.KEIRYOU_NUMBER_LABEL);
            this.Controls.Add(this.UKETSUKE_NUMBER_LABEL);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.LastUpdateDate);
            this.Controls.Add(this.LastUpdateUser);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.CreateDate);
            this.Controls.Add(this.CreateUser);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.KYOTEN_LABEL);
            this.Controls.Add(this.label1);
            this.Name = "UIHeaderForm";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.KYOTEN_LABEL, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.CreateUser, 0);
            this.Controls.SetChildIndex(this.CreateDate, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.LastUpdateUser, 0);
            this.Controls.SetChildIndex(this.LastUpdateDate, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.UKETSUKE_NUMBER_LABEL, 0);
            this.Controls.SetChildIndex(this.KEIRYOU_NUMBER_LABEL, 0);
            this.Controls.SetChildIndex(this.UKETSUKE_NUMBER, 0);
            this.Controls.SetChildIndex(this.KEIRYOU_NUMBER, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.lblKensyuu, 0);
            this.Controls.SetChildIndex(this.txtKensyuu, 0);
            this.Controls.SetChildIndex(this.label21, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.customPanel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomAlphaNumTextBox KYOTEN_NAME_RYAKU;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        public System.Windows.Forms.Label KYOTEN_LABEL;
        public System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomTextBox CreateUser;
        public r_framework.CustomControl.CustomTextBox CreateDate;
        public System.Windows.Forms.Label label36;
        public r_framework.CustomControl.CustomTextBox LastUpdateUser;
        public r_framework.CustomControl.CustomTextBox LastUpdateDate;
        public System.Windows.Forms.Label label35;
        internal r_framework.CustomControl.CustomNumericTextBox2 KEIRYOU_NUMBER;
        internal r_framework.CustomControl.CustomNumericTextBox2 UKETSUKE_NUMBER;
        internal System.Windows.Forms.Label KEIRYOU_NUMBER_LABEL;
        internal System.Windows.Forms.Label UKETSUKE_NUMBER_LABEL;
        private r_framework.CustomControl.CustomPanel customPanel2;
        internal r_framework.CustomControl.CustomCheckBox KENSHU_MUST_KBN;
        internal System.Windows.Forms.Label label21;
        internal r_framework.CustomControl.CustomTextBox txtKensyuu;
        internal System.Windows.Forms.Label lblKensyuu;
    }
}
