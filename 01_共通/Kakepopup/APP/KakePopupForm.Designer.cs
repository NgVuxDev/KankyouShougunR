namespace Shougun.Core.Common.Kakepopup.App
{
    partial class KakePopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KakePopupForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.lbl_hanishitei = new System.Windows.Forms.Label();
            this.lbl_syuturyokunaiyou = new System.Windows.Forms.Label();
            this.lbl_hidukehanishitei = new System.Windows.Forms.Label();
            this.lbl_torihikisaki = new System.Windows.Forms.Label();
            this.txt_syuturyokunaiyou = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rdobtn_torihikisakijun = new r_framework.CustomControl.CustomRadioButton();
            this.rdobtn_zandakajun = new r_framework.CustomControl.CustomRadioButton();
            this.dtp_denpyouhizukefrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.dtp_denpyouhizuketo = new r_framework.CustomControl.CustomDateTimePicker();
            this.txt_starttorihikisakiname = new r_framework.CustomControl.CustomTextBox();
            this.txt_starttorihikisakicd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_endtorihikisakicd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.txt_endtorihikisakiname = new r_framework.CustomControl.CustomTextBox();
            this.btn_zengetsu = new r_framework.CustomControl.CustomButton();
            this.btn_jigetsu = new r_framework.CustomControl.CustomButton();
            this.btn_kensakujikkou = new r_framework.CustomControl.CustomButton();
            this.btn_cancel = new r_framework.CustomControl.CustomButton();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.customPanel4 = new r_framework.CustomControl.CustomPanel();
            this.btn_syuryoutorihikisaki = new r_framework.CustomControl.CustomPopupOpenButton();
            this.btn_kaishitorihikisaki = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.customPanel1.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.customPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_hanishitei
            // 
            this.lbl_hanishitei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_hanishitei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_hanishitei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_hanishitei.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_hanishitei.ForeColor = System.Drawing.Color.White;
            this.lbl_hanishitei.Location = new System.Drawing.Point(12, 9);
            this.lbl_hanishitei.Name = "lbl_hanishitei";
            this.lbl_hanishitei.Size = new System.Drawing.Size(463, 35);
            this.lbl_hanishitei.TabIndex = 500;
            this.lbl_hanishitei.Text = "売掛金一覧表 - 範囲条件指定";
            this.lbl_hanishitei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_syuturyokunaiyou
            // 
            this.lbl_syuturyokunaiyou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_syuturyokunaiyou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_syuturyokunaiyou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_syuturyokunaiyou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_syuturyokunaiyou.ForeColor = System.Drawing.Color.White;
            this.lbl_syuturyokunaiyou.Location = new System.Drawing.Point(12, 115);
            this.lbl_syuturyokunaiyou.Name = "lbl_syuturyokunaiyou";
            this.lbl_syuturyokunaiyou.Size = new System.Drawing.Size(110, 20);
            this.lbl_syuturyokunaiyou.TabIndex = 501;
            this.lbl_syuturyokunaiyou.Text = "出力内容※";
            this.lbl_syuturyokunaiyou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_hidukehanishitei
            // 
            this.lbl_hidukehanishitei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_hidukehanishitei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_hidukehanishitei.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_hidukehanishitei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_hidukehanishitei.ForeColor = System.Drawing.Color.White;
            this.lbl_hidukehanishitei.Location = new System.Drawing.Point(12, 64);
            this.lbl_hidukehanishitei.Name = "lbl_hidukehanishitei";
            this.lbl_hidukehanishitei.Size = new System.Drawing.Size(110, 20);
            this.lbl_hidukehanishitei.TabIndex = 504;
            this.lbl_hidukehanishitei.Text = "伝票日付範囲※";
            this.lbl_hidukehanishitei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_torihikisaki
            // 
            this.lbl_torihikisaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_torihikisaki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_torihikisaki.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_torihikisaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_torihikisaki.ForeColor = System.Drawing.Color.White;
            this.lbl_torihikisaki.Location = new System.Drawing.Point(12, 166);
            this.lbl_torihikisaki.Name = "lbl_torihikisaki";
            this.lbl_torihikisaki.Size = new System.Drawing.Size(110, 20);
            this.lbl_torihikisaki.TabIndex = 505;
            this.lbl_torihikisaki.Text = "取引先";
            this.lbl_torihikisaki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_syuturyokunaiyou
            // 
            this.txt_syuturyokunaiyou.BackColor = System.Drawing.SystemColors.Window;
            this.txt_syuturyokunaiyou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_syuturyokunaiyou.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_syuturyokunaiyou.DisplayItemName = "出力内容";
            this.txt_syuturyokunaiyou.DisplayPopUp = null;
            this.txt_syuturyokunaiyou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_syuturyokunaiyou.FocusOutCheckMethod")));
            this.txt_syuturyokunaiyou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_syuturyokunaiyou.ForeColor = System.Drawing.Color.Black;
            this.txt_syuturyokunaiyou.IsInputErrorOccured = false;
            this.txt_syuturyokunaiyou.LinkedRadioButtonArray = new string[] {
        "rdobtn_torihikisakijun",
        "rdobtn_zandakajun"};
            this.txt_syuturyokunaiyou.Location = new System.Drawing.Point(-1, -1);
            this.txt_syuturyokunaiyou.Name = "txt_syuturyokunaiyou";
            this.txt_syuturyokunaiyou.PopupAfterExecute = null;
            this.txt_syuturyokunaiyou.PopupBeforeExecute = null;
            this.txt_syuturyokunaiyou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_syuturyokunaiyou.PopupSearchSendParams")));
            this.txt_syuturyokunaiyou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_syuturyokunaiyou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_syuturyokunaiyou.popupWindowSetting")));
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
            this.txt_syuturyokunaiyou.RangeSetting = rangeSettingDto1;
            this.txt_syuturyokunaiyou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_syuturyokunaiyou.RegistCheckMethod")));
            this.txt_syuturyokunaiyou.Size = new System.Drawing.Size(20, 20);
            this.txt_syuturyokunaiyou.TabIndex = 1;
            this.txt_syuturyokunaiyou.Tag = "【1、2】のいずれかで入力してください";
            this.txt_syuturyokunaiyou.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_syuturyokunaiyou.WordWrap = false;
            this.txt_syuturyokunaiyou.TextChanged += new System.EventHandler(this.txt_syuturyokunaiyou_TextChanged);
            // 
            // rdobtn_torihikisakijun
            // 
            this.rdobtn_torihikisakijun.AutoSize = true;
            this.rdobtn_torihikisakijun.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdobtn_torihikisakijun.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdobtn_torihikisakijun.FocusOutCheckMethod")));
            this.rdobtn_torihikisakijun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdobtn_torihikisakijun.LinkedTextBox = "txt_syuturyokunaiyou";
            this.rdobtn_torihikisakijun.Location = new System.Drawing.Point(25, 0);
            this.rdobtn_torihikisakijun.Name = "rdobtn_torihikisakijun";
            this.rdobtn_torihikisakijun.PopupAfterExecute = null;
            this.rdobtn_torihikisakijun.PopupBeforeExecute = null;
            this.rdobtn_torihikisakijun.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdobtn_torihikisakijun.PopupSearchSendParams")));
            this.rdobtn_torihikisakijun.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdobtn_torihikisakijun.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdobtn_torihikisakijun.popupWindowSetting")));
            this.rdobtn_torihikisakijun.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdobtn_torihikisakijun.RegistCheckMethod")));
            this.rdobtn_torihikisakijun.Size = new System.Drawing.Size(95, 17);
            this.rdobtn_torihikisakijun.TabIndex = 2;
            this.rdobtn_torihikisakijun.Tag = "出力内容を「取引先順」にする場合にはチェックを付けてください";
            this.rdobtn_torihikisakijun.Text = "1.取引先順";
            this.rdobtn_torihikisakijun.UseVisualStyleBackColor = true;
            this.rdobtn_torihikisakijun.Value = "1";
            // 
            // rdobtn_zandakajun
            // 
            this.rdobtn_zandakajun.AutoSize = true;
            this.rdobtn_zandakajun.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdobtn_zandakajun.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdobtn_zandakajun.FocusOutCheckMethod")));
            this.rdobtn_zandakajun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdobtn_zandakajun.LinkedTextBox = "txt_syuturyokunaiyou";
            this.rdobtn_zandakajun.Location = new System.Drawing.Point(133, 0);
            this.rdobtn_zandakajun.Name = "rdobtn_zandakajun";
            this.rdobtn_zandakajun.PopupAfterExecute = null;
            this.rdobtn_zandakajun.PopupBeforeExecute = null;
            this.rdobtn_zandakajun.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdobtn_zandakajun.PopupSearchSendParams")));
            this.rdobtn_zandakajun.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdobtn_zandakajun.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdobtn_zandakajun.popupWindowSetting")));
            this.rdobtn_zandakajun.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdobtn_zandakajun.RegistCheckMethod")));
            this.rdobtn_zandakajun.Size = new System.Drawing.Size(81, 17);
            this.rdobtn_zandakajun.TabIndex = 3;
            this.rdobtn_zandakajun.Tag = "出力内容を「残高順」にする場合にはチェックを付けてください";
            this.rdobtn_zandakajun.Text = "2.残高順";
            this.rdobtn_zandakajun.UseVisualStyleBackColor = true;
            this.rdobtn_zandakajun.Value = "2";
            // 
            // dtp_denpyouhizukefrom
            // 
            this.dtp_denpyouhizukefrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_denpyouhizukefrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_denpyouhizukefrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_denpyouhizukefrom.Checked = false;
            this.dtp_denpyouhizukefrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtp_denpyouhizukefrom.DateTimeNowYear = "";
            this.dtp_denpyouhizukefrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_denpyouhizukefrom.DisplayItemName = "開始日付";
            this.dtp_denpyouhizukefrom.DisplayPopUp = null;
            this.dtp_denpyouhizukefrom.Enabled = false;
            this.dtp_denpyouhizukefrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_denpyouhizukefrom.FocusOutCheckMethod")));
            this.dtp_denpyouhizukefrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_denpyouhizukefrom.ForeColor = System.Drawing.Color.Black;
            this.dtp_denpyouhizukefrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_denpyouhizukefrom.IsInputErrorOccured = false;
            this.dtp_denpyouhizukefrom.Location = new System.Drawing.Point(5, 6);
            this.dtp_denpyouhizukefrom.MaxLength = 10;
            this.dtp_denpyouhizukefrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_denpyouhizukefrom.Name = "dtp_denpyouhizukefrom";
            this.dtp_denpyouhizukefrom.NullValue = "";
            this.dtp_denpyouhizukefrom.PopupAfterExecute = null;
            this.dtp_denpyouhizukefrom.PopupBeforeExecute = null;
            this.dtp_denpyouhizukefrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_denpyouhizukefrom.PopupSearchSendParams")));
            this.dtp_denpyouhizukefrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_denpyouhizukefrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_denpyouhizukefrom.popupWindowSetting")));
            this.dtp_denpyouhizukefrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_denpyouhizukefrom.RegistCheckMethod")));
            this.dtp_denpyouhizukefrom.ShortItemName = "伝票日付FROM";
            this.dtp_denpyouhizukefrom.Size = new System.Drawing.Size(138, 20);
            this.dtp_denpyouhizukefrom.TabIndex = 9;
            this.dtp_denpyouhizukefrom.Tag = "「日付範囲指定-開始日付」を入力してください";
            this.dtp_denpyouhizukefrom.Text = "2013/12/09(月)";
            this.dtp_denpyouhizukefrom.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label6.Location = new System.Drawing.Point(156, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 533;
            this.label6.Text = "～";
            // 
            // dtp_denpyouhizuketo
            // 
            this.dtp_denpyouhizuketo.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_denpyouhizuketo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_denpyouhizuketo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_denpyouhizuketo.Checked = false;
            this.dtp_denpyouhizuketo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtp_denpyouhizuketo.DateTimeNowYear = "";
            this.dtp_denpyouhizuketo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_denpyouhizuketo.DisplayItemName = "終了日付";
            this.dtp_denpyouhizuketo.DisplayPopUp = null;
            this.dtp_denpyouhizuketo.Enabled = false;
            this.dtp_denpyouhizuketo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_denpyouhizuketo.FocusOutCheckMethod")));
            this.dtp_denpyouhizuketo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_denpyouhizuketo.ForeColor = System.Drawing.Color.Black;
            this.dtp_denpyouhizuketo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_denpyouhizuketo.IsInputErrorOccured = false;
            this.dtp_denpyouhizuketo.Location = new System.Drawing.Point(188, 6);
            this.dtp_denpyouhizuketo.MaxLength = 10;
            this.dtp_denpyouhizuketo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_denpyouhizuketo.Name = "dtp_denpyouhizuketo";
            this.dtp_denpyouhizuketo.NullValue = "";
            this.dtp_denpyouhizuketo.PopupAfterExecute = null;
            this.dtp_denpyouhizuketo.PopupBeforeExecute = null;
            this.dtp_denpyouhizuketo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_denpyouhizuketo.PopupSearchSendParams")));
            this.dtp_denpyouhizuketo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_denpyouhizuketo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_denpyouhizuketo.popupWindowSetting")));
            this.dtp_denpyouhizuketo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_denpyouhizuketo.RegistCheckMethod")));
            this.dtp_denpyouhizuketo.Size = new System.Drawing.Size(138, 20);
            this.dtp_denpyouhizuketo.TabIndex = 10;
            this.dtp_denpyouhizuketo.Tag = "「日付範囲指定-終了日付」を入力してください";
            this.dtp_denpyouhizuketo.Text = "2013/12/09(月)";
            this.dtp_denpyouhizuketo.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // txt_starttorihikisakiname
            // 
            this.txt_starttorihikisakiname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_starttorihikisakiname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_starttorihikisakiname.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_starttorihikisakiname.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.txt_starttorihikisakiname.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_starttorihikisakiname.DisplayPopUp = null;
            this.txt_starttorihikisakiname.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_starttorihikisakiname.FocusOutCheckMethod")));
            this.txt_starttorihikisakiname.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_starttorihikisakiname.ForeColor = System.Drawing.Color.Black;
            this.txt_starttorihikisakiname.IsInputErrorOccured = false;
            this.txt_starttorihikisakiname.Location = new System.Drawing.Point(57, 8);
            this.txt_starttorihikisakiname.MaxLength = 20;
            this.txt_starttorihikisakiname.Name = "txt_starttorihikisakiname";
            this.txt_starttorihikisakiname.PopupAfterExecute = null;
            this.txt_starttorihikisakiname.PopupBeforeExecute = null;
            this.txt_starttorihikisakiname.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_starttorihikisakiname.PopupSearchSendParams")));
            this.txt_starttorihikisakiname.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_starttorihikisakiname.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_starttorihikisakiname.popupWindowSetting")));
            this.txt_starttorihikisakiname.ReadOnly = true;
            this.txt_starttorihikisakiname.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_starttorihikisakiname.RegistCheckMethod")));
            this.txt_starttorihikisakiname.Size = new System.Drawing.Size(287, 20);
            this.txt_starttorihikisakiname.TabIndex = 12;
            this.txt_starttorihikisakiname.TabStop = false;
            this.txt_starttorihikisakiname.Tag = "は 20 文字以内で入力してください。";
            // 
            // txt_starttorihikisakicd
            // 
            this.txt_starttorihikisakicd.BackColor = System.Drawing.SystemColors.Window;
            this.txt_starttorihikisakicd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_starttorihikisakicd.ChangeUpperCase = true;
            this.txt_starttorihikisakicd.CharacterLimitList = null;
            this.txt_starttorihikisakicd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.txt_starttorihikisakicd.DBFieldsName = "TORIHIKI_CD";
            this.txt_starttorihikisakicd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_starttorihikisakicd.DisplayItemName = "開始取引先CD";
            this.txt_starttorihikisakicd.DisplayPopUp = null;
            this.txt_starttorihikisakicd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_starttorihikisakicd.FocusOutCheckMethod")));
            this.txt_starttorihikisakicd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_starttorihikisakicd.ForeColor = System.Drawing.Color.Black;
            this.txt_starttorihikisakicd.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.txt_starttorihikisakicd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_starttorihikisakicd.IsInputErrorOccured = false;
            this.txt_starttorihikisakicd.ItemDefinedTypes = "varchar";
            this.txt_starttorihikisakicd.Location = new System.Drawing.Point(5, 8);
            this.txt_starttorihikisakicd.MaxLength = 6;
            this.txt_starttorihikisakicd.Name = "txt_starttorihikisakicd";
            this.txt_starttorihikisakicd.PopupAfterExecute = null;
            this.txt_starttorihikisakicd.PopupBeforeExecute = null;
            this.txt_starttorihikisakicd.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.txt_starttorihikisakicd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_starttorihikisakicd.PopupSearchSendParams")));
            this.txt_starttorihikisakicd.PopupSetFormField = "txt_starttorihikisakicd,txt_starttorihikisakiname";
            this.txt_starttorihikisakicd.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.txt_starttorihikisakicd.PopupWindowName = "検索共通ポップアップ";
            this.txt_starttorihikisakicd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_starttorihikisakicd.popupWindowSetting")));
            this.txt_starttorihikisakicd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_starttorihikisakicd.RegistCheckMethod")));
            this.txt_starttorihikisakicd.SetFormField = "txt_starttorihikisakicd,txt_starttorihikisakiname";
            this.txt_starttorihikisakicd.ShortItemName = "取引先CD";
            this.txt_starttorihikisakicd.Size = new System.Drawing.Size(53, 20);
            this.txt_starttorihikisakicd.TabIndex = 11;
            this.txt_starttorihikisakicd.Tag = "開始取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txt_starttorihikisakicd.ZeroPaddengFlag = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.Location = new System.Drawing.Point(379, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 599;
            this.label1.Text = "～";
            // 
            // txt_endtorihikisakicd
            // 
            this.txt_endtorihikisakicd.BackColor = System.Drawing.SystemColors.Window;
            this.txt_endtorihikisakicd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_endtorihikisakicd.ChangeUpperCase = true;
            this.txt_endtorihikisakicd.CharacterLimitList = null;
            this.txt_endtorihikisakicd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.txt_endtorihikisakicd.DBFieldsName = "TORIHIKI_CD";
            this.txt_endtorihikisakicd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_endtorihikisakicd.DisplayItemName = "終了取引先CD";
            this.txt_endtorihikisakicd.DisplayPopUp = null;
            this.txt_endtorihikisakicd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_endtorihikisakicd.FocusOutCheckMethod")));
            this.txt_endtorihikisakicd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_endtorihikisakicd.ForeColor = System.Drawing.Color.Black;
            this.txt_endtorihikisakicd.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.txt_endtorihikisakicd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_endtorihikisakicd.IsInputErrorOccured = false;
            this.txt_endtorihikisakicd.ItemDefinedTypes = "varchar";
            this.txt_endtorihikisakicd.Location = new System.Drawing.Point(408, 8);
            this.txt_endtorihikisakicd.MaxLength = 6;
            this.txt_endtorihikisakicd.Name = "txt_endtorihikisakicd";
            this.txt_endtorihikisakicd.PopupAfterExecute = null;
            this.txt_endtorihikisakicd.PopupBeforeExecute = null;
            this.txt_endtorihikisakicd.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.txt_endtorihikisakicd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_endtorihikisakicd.PopupSearchSendParams")));
            this.txt_endtorihikisakicd.PopupSetFormField = "txt_endtorihikisakicd,txt_endtorihikisakiname";
            this.txt_endtorihikisakicd.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.txt_endtorihikisakicd.PopupWindowName = "検索共通ポップアップ";
            this.txt_endtorihikisakicd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_endtorihikisakicd.popupWindowSetting")));
            this.txt_endtorihikisakicd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_endtorihikisakicd.RegistCheckMethod")));
            this.txt_endtorihikisakicd.SetFormField = "txt_endtorihikisakicd,txt_endtorihikisakiname";
            this.txt_endtorihikisakicd.ShortItemName = "取引先CD";
            this.txt_endtorihikisakicd.Size = new System.Drawing.Size(53, 20);
            this.txt_endtorihikisakicd.TabIndex = 14;
            this.txt_endtorihikisakicd.Tag = "終了取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txt_endtorihikisakicd.ZeroPaddengFlag = true;
            this.txt_endtorihikisakicd.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TorihikisakiToDoubleClick);
            // 
            // txt_endtorihikisakiname
            // 
            this.txt_endtorihikisakiname.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_endtorihikisakiname.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_endtorihikisakiname.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_endtorihikisakiname.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.txt_endtorihikisakiname.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_endtorihikisakiname.DisplayPopUp = null;
            this.txt_endtorihikisakiname.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_endtorihikisakiname.FocusOutCheckMethod")));
            this.txt_endtorihikisakiname.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_endtorihikisakiname.ForeColor = System.Drawing.Color.Black;
            this.txt_endtorihikisakiname.IsInputErrorOccured = false;
            this.txt_endtorihikisakiname.Location = new System.Drawing.Point(460, 8);
            this.txt_endtorihikisakiname.MaxLength = 20;
            this.txt_endtorihikisakiname.Name = "txt_endtorihikisakiname";
            this.txt_endtorihikisakiname.PopupAfterExecute = null;
            this.txt_endtorihikisakiname.PopupBeforeExecute = null;
            this.txt_endtorihikisakiname.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_endtorihikisakiname.PopupSearchSendParams")));
            this.txt_endtorihikisakiname.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_endtorihikisakiname.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_endtorihikisakiname.popupWindowSetting")));
            this.txt_endtorihikisakiname.ReadOnly = true;
            this.txt_endtorihikisakiname.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_endtorihikisakiname.RegistCheckMethod")));
            this.txt_endtorihikisakiname.Size = new System.Drawing.Size(287, 20);
            this.txt_endtorihikisakiname.TabIndex = 15;
            this.txt_endtorihikisakiname.TabStop = false;
            this.txt_endtorihikisakiname.Tag = "は 20 文字以内で入力してください。";
            // 
            // btn_zengetsu
            // 
            this.btn_zengetsu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_zengetsu.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_zengetsu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_zengetsu.Location = new System.Drawing.Point(12, 216);
            this.btn_zengetsu.Name = "btn_zengetsu";
            this.btn_zengetsu.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_zengetsu.Size = new System.Drawing.Size(90, 35);
            this.btn_zengetsu.TabIndex = 17;
            this.btn_zengetsu.TabStop = false;
            this.btn_zengetsu.Tag = "日付範囲指定の開始／終了を前月に変更します";
            this.btn_zengetsu.Text = "[F1]\r\n前月";
            this.btn_zengetsu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_zengetsu.UseVisualStyleBackColor = false;
            // 
            // btn_jigetsu
            // 
            this.btn_jigetsu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_jigetsu.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_jigetsu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_jigetsu.Location = new System.Drawing.Point(107, 216);
            this.btn_jigetsu.Name = "btn_jigetsu";
            this.btn_jigetsu.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_jigetsu.Size = new System.Drawing.Size(90, 35);
            this.btn_jigetsu.TabIndex = 18;
            this.btn_jigetsu.TabStop = false;
            this.btn_jigetsu.Tag = "日付範囲指定の開始／終了を次月に変更します";
            this.btn_jigetsu.Text = "[F2]\r\n次月";
            this.btn_jigetsu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_jigetsu.UseVisualStyleBackColor = false;
            // 
            // btn_kensakujikkou
            // 
            this.btn_kensakujikkou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_kensakujikkou.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_kensakujikkou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_kensakujikkou.Location = new System.Drawing.Point(712, 216);
            this.btn_kensakujikkou.Name = "btn_kensakujikkou";
            this.btn_kensakujikkou.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_kensakujikkou.Size = new System.Drawing.Size(90, 35);
            this.btn_kensakujikkou.TabIndex = 19;
            this.btn_kensakujikkou.TabStop = false;
            this.btn_kensakujikkou.Tag = "検索を実行します";
            this.btn_kensakujikkou.Text = "[F8]\r\n検索実行";
            this.btn_kensakujikkou.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_kensakujikkou.UseVisualStyleBackColor = false;
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_cancel.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_cancel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_cancel.Location = new System.Drawing.Point(807, 216);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btn_cancel.Size = new System.Drawing.Size(90, 35);
            this.btn_cancel.TabIndex = 20;
            this.btn_cancel.TabStop = false;
            this.btn_cancel.Tag = "画面を閉じます";
            this.btn_cancel.Text = "[F12]\r\nキャンセル";
            this.btn_cancel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_cancel.UseVisualStyleBackColor = false;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.rdobtn_zandakajun);
            this.customPanel1.Controls.Add(this.rdobtn_torihikisakijun);
            this.customPanel1.Controls.Add(this.txt_syuturyokunaiyou);
            this.customPanel1.Location = new System.Drawing.Point(127, 115);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(241, 20);
            this.customPanel1.TabIndex = 0;
            // 
            // customPanel3
            // 
            this.customPanel3.Controls.Add(this.dtp_denpyouhizuketo);
            this.customPanel3.Controls.Add(this.label6);
            this.customPanel3.Controls.Add(this.dtp_denpyouhizukefrom);
            this.customPanel3.Location = new System.Drawing.Point(122, 58);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(353, 38);
            this.customPanel3.TabIndex = 3;
            // 
            // customPanel4
            // 
            this.customPanel4.Controls.Add(this.btn_syuryoutorihikisaki);
            this.customPanel4.Controls.Add(this.btn_kaishitorihikisaki);
            this.customPanel4.Controls.Add(this.txt_endtorihikisakicd);
            this.customPanel4.Controls.Add(this.txt_endtorihikisakiname);
            this.customPanel4.Controls.Add(this.label1);
            this.customPanel4.Controls.Add(this.txt_starttorihikisakicd);
            this.customPanel4.Controls.Add(this.txt_starttorihikisakiname);
            this.customPanel4.Location = new System.Drawing.Point(122, 158);
            this.customPanel4.Name = "customPanel4";
            this.customPanel4.Size = new System.Drawing.Size(781, 36);
            this.customPanel4.TabIndex = 4;
            // 
            // btn_syuryoutorihikisaki
            // 
            this.btn_syuryoutorihikisaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_syuryoutorihikisaki.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btn_syuryoutorihikisaki.DBFieldsName = null;
            this.btn_syuryoutorihikisaki.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_syuryoutorihikisaki.DisplayItemName = null;
            this.btn_syuryoutorihikisaki.DisplayPopUp = null;
            this.btn_syuryoutorihikisaki.ErrorMessage = null;
            this.btn_syuryoutorihikisaki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btn_syuryoutorihikisaki.FocusOutCheckMethod")));
            this.btn_syuryoutorihikisaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btn_syuryoutorihikisaki.GetCodeMasterField = null;
            this.btn_syuryoutorihikisaki.Image = global::Shougun.Core.Common.Kakepopup.Properties.Resources.虫眼鏡;
            this.btn_syuryoutorihikisaki.ItemDefinedTypes = null;
            this.btn_syuryoutorihikisaki.LinkedSettingTextBox = null;
            this.btn_syuryoutorihikisaki.LinkedTextBoxs = null;
            this.btn_syuryoutorihikisaki.Location = new System.Drawing.Point(750, 7);
            this.btn_syuryoutorihikisaki.Name = "btn_syuryoutorihikisaki";
            this.btn_syuryoutorihikisaki.PopupAfterExecute = null;
            this.btn_syuryoutorihikisaki.PopupAfterExecuteMethod = "ToCDPopupAfterUpdate";
            this.btn_syuryoutorihikisaki.PopupBeforeExecute = null;
            this.btn_syuryoutorihikisaki.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.btn_syuryoutorihikisaki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btn_syuryoutorihikisaki.PopupSearchSendParams")));
            this.btn_syuryoutorihikisaki.PopupSetFormField = "txt_endtorihikisakicd,txt_endtorihikisakiname";
            this.btn_syuryoutorihikisaki.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.btn_syuryoutorihikisaki.PopupWindowName = "検索共通ポップアップ";
            this.btn_syuryoutorihikisaki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btn_syuryoutorihikisaki.popupWindowSetting")));
            this.btn_syuryoutorihikisaki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btn_syuryoutorihikisaki.RegistCheckMethod")));
            this.btn_syuryoutorihikisaki.SearchDisplayFlag = 0;
            this.btn_syuryoutorihikisaki.SetFormField = "txt_endtorihikisakicd,txt_endtorihikisakiname";
            this.btn_syuryoutorihikisaki.ShortItemName = null;
            this.btn_syuryoutorihikisaki.Size = new System.Drawing.Size(22, 22);
            this.btn_syuryoutorihikisaki.TabIndex = 16;
            this.btn_syuryoutorihikisaki.TabStop = false;
            this.btn_syuryoutorihikisaki.Tag = "検索画面を表示します";
            this.btn_syuryoutorihikisaki.UseVisualStyleBackColor = false;
            this.btn_syuryoutorihikisaki.ZeroPaddengFlag = false;
            // 
            // btn_kaishitorihikisaki
            // 
            this.btn_kaishitorihikisaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_kaishitorihikisaki.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btn_kaishitorihikisaki.DBFieldsName = null;
            this.btn_kaishitorihikisaki.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_kaishitorihikisaki.DisplayItemName = null;
            this.btn_kaishitorihikisaki.DisplayPopUp = null;
            this.btn_kaishitorihikisaki.ErrorMessage = null;
            this.btn_kaishitorihikisaki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btn_kaishitorihikisaki.FocusOutCheckMethod")));
            this.btn_kaishitorihikisaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.btn_kaishitorihikisaki.GetCodeMasterField = null;
            this.btn_kaishitorihikisaki.Image = global::Shougun.Core.Common.Kakepopup.Properties.Resources.虫眼鏡;
            this.btn_kaishitorihikisaki.ItemDefinedTypes = null;
            this.btn_kaishitorihikisaki.LinkedSettingTextBox = null;
            this.btn_kaishitorihikisaki.LinkedTextBoxs = null;
            this.btn_kaishitorihikisaki.Location = new System.Drawing.Point(348, 7);
            this.btn_kaishitorihikisaki.Name = "btn_kaishitorihikisaki";
            this.btn_kaishitorihikisaki.PopupAfterExecute = null;
            this.btn_kaishitorihikisaki.PopupAfterExecuteMethod = "FromCDPopupAfterUpdate";
            this.btn_kaishitorihikisaki.PopupBeforeExecute = null;
            this.btn_kaishitorihikisaki.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.btn_kaishitorihikisaki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btn_kaishitorihikisaki.PopupSearchSendParams")));
            this.btn_kaishitorihikisaki.PopupSetFormField = "txt_starttorihikisakicd,txt_starttorihikisakiname";
            this.btn_kaishitorihikisaki.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.btn_kaishitorihikisaki.PopupWindowName = "検索共通ポップアップ";
            this.btn_kaishitorihikisaki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btn_kaishitorihikisaki.popupWindowSetting")));
            this.btn_kaishitorihikisaki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btn_kaishitorihikisaki.RegistCheckMethod")));
            this.btn_kaishitorihikisaki.SearchDisplayFlag = 0;
            this.btn_kaishitorihikisaki.SetFormField = "txt_starttorihikisakicd,txt_starttorihikisakiname";
            this.btn_kaishitorihikisaki.ShortItemName = null;
            this.btn_kaishitorihikisaki.Size = new System.Drawing.Size(22, 22);
            this.btn_kaishitorihikisaki.TabIndex = 13;
            this.btn_kaishitorihikisaki.TabStop = false;
            this.btn_kaishitorihikisaki.Tag = "検索画面を表示します";
            this.btn_kaishitorihikisaki.UseVisualStyleBackColor = false;
            this.btn_kaishitorihikisaki.ZeroPaddengFlag = false;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(437, 121);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 20;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(40, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 670;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // KakePopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(914, 262);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.customPanel4);
            this.Controls.Add(this.customPanel3);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_kensakujikkou);
            this.Controls.Add(this.btn_jigetsu);
            this.Controls.Add(this.btn_zengetsu);
            this.Controls.Add(this.lbl_torihikisaki);
            this.Controls.Add(this.lbl_hidukehanishitei);
            this.Controls.Add(this.lbl_syuturyokunaiyou);
            this.Controls.Add(this.lbl_hanishitei);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "KakePopupForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "売掛金一覧表 - 範囲条件指定";
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.customPanel4.ResumeLayout(false);
            this.customPanel4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lbl_hanishitei;
        public System.Windows.Forms.Label lbl_syuturyokunaiyou;
        public System.Windows.Forms.Label lbl_hidukehanishitei;
        public System.Windows.Forms.Label lbl_torihikisaki;
        private r_framework.CustomControl.CustomRadioButton rdobtn_torihikisakijun;
        private r_framework.CustomControl.CustomRadioButton rdobtn_zandakajun;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private r_framework.CustomControl.CustomPanel customPanel1;
        private r_framework.CustomControl.CustomPanel customPanel3;
        private r_framework.CustomControl.CustomPanel customPanel4;
        public r_framework.CustomControl.CustomDateTimePicker dtp_denpyouhizukefrom;
        public r_framework.CustomControl.CustomDateTimePicker dtp_denpyouhizuketo;
        public r_framework.CustomControl.CustomNumericTextBox2 txt_syuturyokunaiyou;
        public r_framework.CustomControl.CustomButton btn_kensakujikkou;
        public r_framework.CustomControl.CustomButton btn_cancel;
        public r_framework.CustomControl.CustomButton btn_zengetsu;
        public r_framework.CustomControl.CustomButton btn_jigetsu;
        internal r_framework.CustomControl.CustomPopupOpenButton btn_kaishitorihikisaki;
        internal r_framework.CustomControl.CustomPopupOpenButton btn_syuryoutorihikisaki;
        internal r_framework.CustomControl.CustomAlphaNumTextBox txt_starttorihikisakicd;
        internal r_framework.CustomControl.CustomTextBox txt_starttorihikisakiname;
        internal r_framework.CustomControl.CustomTextBox txt_endtorihikisakiname;
        internal r_framework.CustomControl.CustomAlphaNumTextBox txt_endtorihikisakicd;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}