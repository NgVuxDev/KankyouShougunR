namespace Shougun.Core.Allocation.Teikihaisyajissekihyou
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
            this.label_Shuturyokukubun = new System.Windows.Forms.Label();
            this.lbl_Kikan = new System.Windows.Forms.Label();
            this.txt_Shuturyokukubun = new r_framework.CustomControl.CustomNumericTextBox2();
            this.rdo_Geppou = new r_framework.CustomControl.CustomRadioButton();
            this.rdo_Nenpou = new r_framework.CustomControl.CustomRadioButton();
            this.lbl_FromTo = new System.Windows.Forms.Label();
            this.dtp_KikanTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.dtp_KikanFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.GENBA_NAME_RYAKU_From = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_CD_From = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME_RYAKU_From = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_CD_From = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label_Genba = new System.Windows.Forms.Label();
            this.label_Gyousha = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.GENBA_NAME_RYAKU_To = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_CD_To = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME_RYAKU_To = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_CD_To = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.txt_KyotenName = new r_framework.CustomControl.CustomTextBox();
            this.txt_KyotenCD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lbl_Kyoten = new System.Windows.Forms.Label();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.rdo_Anbun = new r_framework.CustomControl.CustomRadioButton();
            this.rdo_JissekiKansan = new r_framework.CustomControl.CustomRadioButton();
            this.txt_Shuukeisuuryou = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label_Shuukeisuuryou = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.rdo_Kansan = new r_framework.CustomControl.CustomRadioButton();
            this.rdo_Jisseki = new r_framework.CustomControl.CustomRadioButton();
            this.TORIHIKISAKI_NAME_RYAKU_To = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_CD_To = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_NAME_RYAKU_From = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_CD_From = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SHIMEBI = new r_framework.CustomControl.CustomComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.SHURUI_NAME_RYAKU_To = new r_framework.CustomControl.CustomTextBox();
            this.SHURUI_CD_To = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.SHURUI_NAME_RYAKU_From = new r_framework.CustomControl.CustomTextBox();
            this.SHURUI_CD_From = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Shuturyokukubun
            // 
            this.label_Shuturyokukubun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label_Shuturyokukubun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Shuturyokukubun.Location = new System.Drawing.Point(2, 43);
            this.label_Shuturyokukubun.Name = "label_Shuturyokukubun";
            this.label_Shuturyokukubun.Size = new System.Drawing.Size(110, 20);
            this.label_Shuturyokukubun.TabIndex = 9999;
            this.label_Shuturyokukubun.Text = "出力区分※";
            this.label_Shuturyokukubun.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Kikan
            // 
            this.lbl_Kikan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kikan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kikan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kikan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kikan.ForeColor = System.Drawing.Color.White;
            this.lbl_Kikan.Location = new System.Drawing.Point(2, 67);
            this.lbl_Kikan.Name = "lbl_Kikan";
            this.lbl_Kikan.Size = new System.Drawing.Size(110, 20);
            this.lbl_Kikan.TabIndex = 9999;
            this.lbl_Kikan.Text = "期間※";
            this.lbl_Kikan.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Shuturyokukubun
            // 
            this.txt_Shuturyokukubun.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Shuturyokukubun.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Shuturyokukubun.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Shuturyokukubun.DisplayItemName = "出力区分";
            this.txt_Shuturyokukubun.DisplayPopUp = null;
            this.txt_Shuturyokukubun.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Shuturyokukubun.FocusOutCheckMethod")));
            this.txt_Shuturyokukubun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_Shuturyokukubun.ForeColor = System.Drawing.Color.Black;
            this.txt_Shuturyokukubun.GetCodeMasterField = "";
            this.txt_Shuturyokukubun.IsInputErrorOccured = false;
            this.txt_Shuturyokukubun.LinkedRadioButtonArray = new string[] {
        "rdo_Geppou",
        "rdo_Nenpou"};
            this.txt_Shuturyokukubun.Location = new System.Drawing.Point(117, 43);
            this.txt_Shuturyokukubun.Name = "txt_Shuturyokukubun";
            this.txt_Shuturyokukubun.PopupAfterExecute = null;
            this.txt_Shuturyokukubun.PopupBeforeExecute = null;
            this.txt_Shuturyokukubun.PopupGetMasterField = "";
            this.txt_Shuturyokukubun.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Shuturyokukubun.PopupSearchSendParams")));
            this.txt_Shuturyokukubun.PopupSetFormField = "";
            this.txt_Shuturyokukubun.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.txt_Shuturyokukubun.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Shuturyokukubun.popupWindowSetting")));
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
            this.txt_Shuturyokukubun.RangeSetting = rangeSettingDto1;
            this.txt_Shuturyokukubun.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Shuturyokukubun.RegistCheckMethod")));
            this.txt_Shuturyokukubun.SetFormField = "";
            this.txt_Shuturyokukubun.Size = new System.Drawing.Size(20, 20);
            this.txt_Shuturyokukubun.TabIndex = 30;
            this.txt_Shuturyokukubun.Tag = "【1～2】のいずれかで入力してください";
            this.txt_Shuturyokukubun.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Shuturyokukubun.WordWrap = false;
            // 
            // rdo_Geppou
            // 
            this.rdo_Geppou.AutoSize = true;
            this.rdo_Geppou.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_Geppou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Geppou.FocusOutCheckMethod")));
            this.rdo_Geppou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_Geppou.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdo_Geppou.LinkedTextBox = "txt_Shuturyokukubun";
            this.rdo_Geppou.Location = new System.Drawing.Point(7, 0);
            this.rdo_Geppou.Name = "rdo_Geppou";
            this.rdo_Geppou.PopupAfterExecute = null;
            this.rdo_Geppou.PopupBeforeExecute = null;
            this.rdo_Geppou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_Geppou.PopupSearchSendParams")));
            this.rdo_Geppou.PopupSetFormField = "";
            this.rdo_Geppou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_Geppou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_Geppou.popupWindowSetting")));
            this.rdo_Geppou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Geppou.RegistCheckMethod")));
            this.rdo_Geppou.SetFormField = "";
            this.rdo_Geppou.Size = new System.Drawing.Size(67, 17);
            this.rdo_Geppou.TabIndex = 32;
            this.rdo_Geppou.Tag = "出力区分を「月報」にする場合は、チェックを付けてください";
            this.rdo_Geppou.Text = "1.月報";
            this.rdo_Geppou.UseVisualStyleBackColor = true;
            this.rdo_Geppou.Value = "1";
            this.rdo_Geppou.CheckedChanged += new System.EventHandler(this.rdo_Geppou_CheckedChanged);
            // 
            // rdo_Nenpou
            // 
            this.rdo_Nenpou.AutoSize = true;
            this.rdo_Nenpou.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_Nenpou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Nenpou.FocusOutCheckMethod")));
            this.rdo_Nenpou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_Nenpou.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdo_Nenpou.LinkedTextBox = "txt_Shuturyokukubun";
            this.rdo_Nenpou.Location = new System.Drawing.Point(108, 0);
            this.rdo_Nenpou.Name = "rdo_Nenpou";
            this.rdo_Nenpou.PopupAfterExecute = null;
            this.rdo_Nenpou.PopupBeforeExecute = null;
            this.rdo_Nenpou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_Nenpou.PopupSearchSendParams")));
            this.rdo_Nenpou.PopupSetFormField = "";
            this.rdo_Nenpou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_Nenpou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_Nenpou.popupWindowSetting")));
            this.rdo_Nenpou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Nenpou.RegistCheckMethod")));
            this.rdo_Nenpou.SetFormField = "";
            this.rdo_Nenpou.Size = new System.Drawing.Size(67, 17);
            this.rdo_Nenpou.TabIndex = 33;
            this.rdo_Nenpou.Tag = "出力区分を「年報」にする場合は、チェックを付けてください";
            this.rdo_Nenpou.Text = "2.年報";
            this.rdo_Nenpou.UseVisualStyleBackColor = true;
            this.rdo_Nenpou.Value = "2";
            this.rdo_Nenpou.CheckedChanged += new System.EventHandler(this.rdo_Nenpou_CheckedChanged);
            // 
            // lbl_FromTo
            // 
            this.lbl_FromTo.BackColor = System.Drawing.Color.Transparent;
            this.lbl_FromTo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_FromTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_FromTo.ForeColor = System.Drawing.Color.Black;
            this.lbl_FromTo.Location = new System.Drawing.Point(270, 67);
            this.lbl_FromTo.Name = "lbl_FromTo";
            this.lbl_FromTo.Size = new System.Drawing.Size(19, 20);
            this.lbl_FromTo.TabIndex = 9999;
            this.lbl_FromTo.Text = "～";
            this.lbl_FromTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtp_KikanTo
            // 
            this.dtp_KikanTo.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_KikanTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_KikanTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_KikanTo.Checked = false;
            this.dtp_KikanTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtp_KikanTo.DateTimeNowYear = "";
            this.dtp_KikanTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_KikanTo.DisplayItemName = "期間To";
            this.dtp_KikanTo.DisplayPopUp = null;
            this.dtp_KikanTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_KikanTo.FocusOutCheckMethod")));
            this.dtp_KikanTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_KikanTo.ForeColor = System.Drawing.Color.Black;
            this.dtp_KikanTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_KikanTo.IsInputErrorOccured = false;
            this.dtp_KikanTo.Location = new System.Drawing.Point(301, 67);
            this.dtp_KikanTo.MaxLength = 10;
            this.dtp_KikanTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_KikanTo.Name = "dtp_KikanTo";
            this.dtp_KikanTo.NullValue = "";
            this.dtp_KikanTo.PopupAfterExecute = null;
            this.dtp_KikanTo.PopupBeforeExecute = null;
            this.dtp_KikanTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_KikanTo.PopupSearchSendParams")));
            this.dtp_KikanTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_KikanTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_KikanTo.popupWindowSetting")));
            this.dtp_KikanTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_KikanTo.RegistCheckMethod")));
            this.dtp_KikanTo.Size = new System.Drawing.Size(138, 20);
            this.dtp_KikanTo.TabIndex = 41;
            this.dtp_KikanTo.Tag = "期間(To)を指定してください";
            this.dtp_KikanTo.Text = "2013/12/01(日)";
            this.dtp_KikanTo.Value = new System.DateTime(2013, 12, 1, 0, 0, 0, 0);
            // 
            // dtp_KikanFrom
            // 
            this.dtp_KikanFrom.BackColor = System.Drawing.SystemColors.Window;
            this.dtp_KikanFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtp_KikanFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtp_KikanFrom.Checked = false;
            this.dtp_KikanFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtp_KikanFrom.DateTimeNowYear = "";
            this.dtp_KikanFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtp_KikanFrom.DisplayItemName = "期間From";
            this.dtp_KikanFrom.DisplayPopUp = null;
            this.dtp_KikanFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_KikanFrom.FocusOutCheckMethod")));
            this.dtp_KikanFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtp_KikanFrom.ForeColor = System.Drawing.Color.Black;
            this.dtp_KikanFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtp_KikanFrom.IsInputErrorOccured = false;
            this.dtp_KikanFrom.Location = new System.Drawing.Point(117, 67);
            this.dtp_KikanFrom.MaxLength = 10;
            this.dtp_KikanFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtp_KikanFrom.Name = "dtp_KikanFrom";
            this.dtp_KikanFrom.NullValue = "";
            this.dtp_KikanFrom.PopupAfterExecute = null;
            this.dtp_KikanFrom.PopupBeforeExecute = null;
            this.dtp_KikanFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtp_KikanFrom.PopupSearchSendParams")));
            this.dtp_KikanFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtp_KikanFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtp_KikanFrom.popupWindowSetting")));
            this.dtp_KikanFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtp_KikanFrom.RegistCheckMethod")));
            this.dtp_KikanFrom.Size = new System.Drawing.Size(138, 20);
            this.dtp_KikanFrom.TabIndex = 40;
            this.dtp_KikanFrom.Tag = "期間(From)を指定してください";
            this.dtp_KikanFrom.Text = "2013/12/01(日)";
            this.dtp_KikanFrom.Value = new System.DateTime(2013, 12, 1, 0, 0, 0, 0);
            // 
            // GENBA_NAME_RYAKU_From
            // 
            this.GENBA_NAME_RYAKU_From.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME_RYAKU_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_RYAKU_From.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GENBA_NAME_RYAKU_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_RYAKU_From.DisplayPopUp = null;
            this.GENBA_NAME_RYAKU_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU_From.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_NAME_RYAKU_From.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU_From.IsInputErrorOccured = false;
            this.GENBA_NAME_RYAKU_From.Location = new System.Drawing.Point(166, 139);
            this.GENBA_NAME_RYAKU_From.Name = "GENBA_NAME_RYAKU_From";
            this.GENBA_NAME_RYAKU_From.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU_From.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU_From.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU_From.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU_From.popupWindowSetting")));
            this.GENBA_NAME_RYAKU_From.ReadOnly = true;
            this.GENBA_NAME_RYAKU_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU_From.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU_From.Size = new System.Drawing.Size(285, 20);
            this.GENBA_NAME_RYAKU_From.TabIndex = 81;
            this.GENBA_NAME_RYAKU_From.TabStop = false;
            this.GENBA_NAME_RYAKU_From.Tag = "";
            // 
            // GENBA_CD_From
            // 
            this.GENBA_CD_From.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD_From.CharacterLimitList = null;
            this.GENBA_CD_From.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD_From.DBFieldsName = "GENBA_CD";
            this.GENBA_CD_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD_From.DisplayItemName = "現場CDFrom";
            this.GENBA_CD_From.DisplayPopUp = null;
            this.GENBA_CD_From.Enabled = false;
            this.GENBA_CD_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD_From.FocusOutCheckMethod")));
            this.GENBA_CD_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_CD_From.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD_From.GetCodeMasterField = "GENBA_CD, GENBA_NAME_RYAKU";
            this.GENBA_CD_From.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD_From.IsInputErrorOccured = false;
            this.GENBA_CD_From.ItemDefinedTypes = "varchar";
            this.GENBA_CD_From.Location = new System.Drawing.Point(117, 139);
            this.GENBA_CD_From.MaxLength = 6;
            this.GENBA_CD_From.Name = "GENBA_CD_From";
            this.GENBA_CD_From.PopupAfterExecute = null;
            this.GENBA_CD_From.PopupBeforeExecute = null;
            this.GENBA_CD_From.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU";
            this.GENBA_CD_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD_From.PopupSearchSendParams")));
            this.GENBA_CD_From.PopupSendParams = new string[0];
            this.GENBA_CD_From.PopupSetFormField = "GENBA_CD_From, GENBA_NAME_RYAKU_From";
            this.GENBA_CD_From.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD_From.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD_From.popupWindowSetting")));
            this.GENBA_CD_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD_From.RegistCheckMethod")));
            this.GENBA_CD_From.SetFormField = "GENBA_CD_From, GENBA_NAME_RYAKU_From";
            this.GENBA_CD_From.ShortItemName = "現場CDFrom";
            this.GENBA_CD_From.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD_From.TabIndex = 80;
            this.GENBA_CD_From.Tag = "現場CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD_From.ZeroPaddengFlag = true;
            // 
            // GYOUSHA_NAME_RYAKU_From
            // 
            this.GYOUSHA_NAME_RYAKU_From.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME_RYAKU_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_RYAKU_From.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GYOUSHA_NAME_RYAKU_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_RYAKU_From.DisplayPopUp = null;
            this.GYOUSHA_NAME_RYAKU_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_From.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME_RYAKU_From.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU_From.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU_From.Location = new System.Drawing.Point(166, 115);
            this.GYOUSHA_NAME_RYAKU_From.Name = "GYOUSHA_NAME_RYAKU_From";
            this.GYOUSHA_NAME_RYAKU_From.PopupAfterExecute = null;
            this.GYOUSHA_NAME_RYAKU_From.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_RYAKU_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_From.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU_From.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_From.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU_From.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_From.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU_From.Size = new System.Drawing.Size(285, 20);
            this.GYOUSHA_NAME_RYAKU_From.TabIndex = 71;
            this.GYOUSHA_NAME_RYAKU_From.TabStop = false;
            this.GYOUSHA_NAME_RYAKU_From.Tag = "";
            // 
            // GYOUSHA_CD_From
            // 
            this.GYOUSHA_CD_From.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD_From.CharacterLimitList = null;
            this.GYOUSHA_CD_From.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD_From.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD_From.DisplayItemName = "業者CDFrom";
            this.GYOUSHA_CD_From.DisplayPopUp = null;
            this.GYOUSHA_CD_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD_From.FocusOutCheckMethod")));
            this.GYOUSHA_CD_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_CD_From.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD_From.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD_From.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD_From.IsInputErrorOccured = false;
            this.GYOUSHA_CD_From.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD_From.Location = new System.Drawing.Point(117, 115);
            this.GYOUSHA_CD_From.MaxLength = 6;
            this.GYOUSHA_CD_From.Name = "GYOUSHA_CD_From";
            this.GYOUSHA_CD_From.PopupAfterExecute = null;
            this.GYOUSHA_CD_From.PopupAfterExecuteMethod = "GYOUSHA_CD_FROM_PopAft";
            this.GYOUSHA_CD_From.PopupBeforeExecute = null;
            this.GYOUSHA_CD_From.PopupBeforeExecuteMethod = "GYOUSHA_CD_FROM_PopBef";
            this.GYOUSHA_CD_From.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD_From.PopupSearchSendParams")));
            this.GYOUSHA_CD_From.PopupSendParams = new string[0];
            this.GYOUSHA_CD_From.PopupSetFormField = "GYOUSHA_CD_From,GYOUSHA_NAME_RYAKU_From";
            this.GYOUSHA_CD_From.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD_From.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD_From.popupWindowSetting")));
            this.GYOUSHA_CD_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD_From.RegistCheckMethod")));
            this.GYOUSHA_CD_From.SetFormField = "GYOUSHA_CD_From,GYOUSHA_NAME_RYAKU_From";
            this.GYOUSHA_CD_From.ShortItemName = "業者CDFrom";
            this.GYOUSHA_CD_From.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD_From.TabIndex = 70;
            this.GYOUSHA_CD_From.Tag = "業者CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD_From.ZeroPaddengFlag = true;
            this.GYOUSHA_CD_From.Enter += new System.EventHandler(this.GYOUSHA_CD_From_Enter);
            this.GYOUSHA_CD_From.Validated += new System.EventHandler(this.GYOUSHA_CD_From_Validated);
            // 
            // label_Genba
            // 
            this.label_Genba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label_Genba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Genba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Genba.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Genba.ForeColor = System.Drawing.Color.White;
            this.label_Genba.Location = new System.Drawing.Point(2, 139);
            this.label_Genba.Name = "label_Genba";
            this.label_Genba.Size = new System.Drawing.Size(110, 20);
            this.label_Genba.TabIndex = 9999;
            this.label_Genba.Text = "現場";
            this.label_Genba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_Gyousha
            // 
            this.label_Gyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label_Gyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Gyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label_Gyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_Gyousha.ForeColor = System.Drawing.Color.White;
            this.label_Gyousha.Location = new System.Drawing.Point(2, 115);
            this.label_Gyousha.Name = "label_Gyousha";
            this.label_Gyousha.Size = new System.Drawing.Size(110, 20);
            this.label_Gyousha.TabIndex = 9999;
            this.label_Gyousha.Text = "業者";
            this.label_Gyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(466, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 20);
            this.label1.TabIndex = 9999;
            this.label1.Text = "～";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(466, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(19, 20);
            this.label2.TabIndex = 9999;
            this.label2.Text = "～";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_NAME_RYAKU_To
            // 
            this.GENBA_NAME_RYAKU_To.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME_RYAKU_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_RYAKU_To.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GENBA_NAME_RYAKU_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_RYAKU_To.DisplayPopUp = null;
            this.GENBA_NAME_RYAKU_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU_To.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_NAME_RYAKU_To.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU_To.IsInputErrorOccured = false;
            this.GENBA_NAME_RYAKU_To.Location = new System.Drawing.Point(546, 139);
            this.GENBA_NAME_RYAKU_To.Name = "GENBA_NAME_RYAKU_To";
            this.GENBA_NAME_RYAKU_To.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU_To.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU_To.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU_To.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU_To.popupWindowSetting")));
            this.GENBA_NAME_RYAKU_To.ReadOnly = true;
            this.GENBA_NAME_RYAKU_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU_To.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU_To.Size = new System.Drawing.Size(285, 20);
            this.GENBA_NAME_RYAKU_To.TabIndex = 83;
            this.GENBA_NAME_RYAKU_To.TabStop = false;
            this.GENBA_NAME_RYAKU_To.Tag = "";
            // 
            // GENBA_CD_To
            // 
            this.GENBA_CD_To.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD_To.CharacterLimitList = null;
            this.GENBA_CD_To.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD_To.DBFieldsName = "GENBA_CD";
            this.GENBA_CD_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD_To.DisplayItemName = "現場CDTo";
            this.GENBA_CD_To.DisplayPopUp = null;
            this.GENBA_CD_To.Enabled = false;
            this.GENBA_CD_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD_To.FocusOutCheckMethod")));
            this.GENBA_CD_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_CD_To.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD_To.GetCodeMasterField = "GENBA_CD, GENBA_NAME_RYAKU";
            this.GENBA_CD_To.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD_To.IsInputErrorOccured = false;
            this.GENBA_CD_To.ItemDefinedTypes = "varchar";
            this.GENBA_CD_To.Location = new System.Drawing.Point(497, 139);
            this.GENBA_CD_To.MaxLength = 6;
            this.GENBA_CD_To.Name = "GENBA_CD_To";
            this.GENBA_CD_To.PopupAfterExecute = null;
            this.GENBA_CD_To.PopupBeforeExecute = null;
            this.GENBA_CD_To.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU";
            this.GENBA_CD_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD_To.PopupSearchSendParams")));
            this.GENBA_CD_To.PopupSetFormField = "GENBA_CD_To, GENBA_NAME_RYAKU_To";
            this.GENBA_CD_To.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD_To.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD_To.popupWindowSetting")));
            this.GENBA_CD_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD_To.RegistCheckMethod")));
            this.GENBA_CD_To.SetFormField = "GENBA_CD_To, GENBA_NAME_RYAKU_To";
            this.GENBA_CD_To.ShortItemName = "現場CDTo";
            this.GENBA_CD_To.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD_To.TabIndex = 82;
            this.GENBA_CD_To.Tag = "現場CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD_To.ZeroPaddengFlag = true;
            // 
            // GYOUSHA_NAME_RYAKU_To
            // 
            this.GYOUSHA_NAME_RYAKU_To.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME_RYAKU_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_RYAKU_To.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GYOUSHA_NAME_RYAKU_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_RYAKU_To.DisplayPopUp = null;
            this.GYOUSHA_NAME_RYAKU_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_To.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME_RYAKU_To.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU_To.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU_To.Location = new System.Drawing.Point(546, 115);
            this.GYOUSHA_NAME_RYAKU_To.Name = "GYOUSHA_NAME_RYAKU_To";
            this.GYOUSHA_NAME_RYAKU_To.PopupAfterExecute = null;
            this.GYOUSHA_NAME_RYAKU_To.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_RYAKU_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_To.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU_To.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_To.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU_To.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_To.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU_To.Size = new System.Drawing.Size(285, 20);
            this.GYOUSHA_NAME_RYAKU_To.TabIndex = 73;
            this.GYOUSHA_NAME_RYAKU_To.TabStop = false;
            this.GYOUSHA_NAME_RYAKU_To.Tag = "";
            // 
            // GYOUSHA_CD_To
            // 
            this.GYOUSHA_CD_To.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD_To.CharacterLimitList = null;
            this.GYOUSHA_CD_To.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD_To.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD_To.DisplayItemName = "業者CDTo";
            this.GYOUSHA_CD_To.DisplayPopUp = null;
            this.GYOUSHA_CD_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD_To.FocusOutCheckMethod")));
            this.GYOUSHA_CD_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_CD_To.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD_To.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD_To.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD_To.IsInputErrorOccured = false;
            this.GYOUSHA_CD_To.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD_To.Location = new System.Drawing.Point(497, 115);
            this.GYOUSHA_CD_To.MaxLength = 6;
            this.GYOUSHA_CD_To.Name = "GYOUSHA_CD_To";
            this.GYOUSHA_CD_To.PopupAfterExecute = null;
            this.GYOUSHA_CD_To.PopupAfterExecuteMethod = "GYOUSHA_CD_TO_PopAft";
            this.GYOUSHA_CD_To.PopupBeforeExecute = null;
            this.GYOUSHA_CD_To.PopupBeforeExecuteMethod = "GYOUSHA_CD_TO_PopBef";
            this.GYOUSHA_CD_To.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD_To.PopupSearchSendParams")));
            this.GYOUSHA_CD_To.PopupSendParams = new string[0];
            this.GYOUSHA_CD_To.PopupSetFormField = "GYOUSHA_CD_To,GYOUSHA_NAME_RYAKU_To";
            this.GYOUSHA_CD_To.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD_To.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD_To.popupWindowSetting")));
            this.GYOUSHA_CD_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD_To.RegistCheckMethod")));
            this.GYOUSHA_CD_To.SetFormField = "GYOUSHA_CD_To,GYOUSHA_NAME_RYAKU_To";
            this.GYOUSHA_CD_To.ShortItemName = "業者CDTo";
            this.GYOUSHA_CD_To.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD_To.TabIndex = 72;
            this.GYOUSHA_CD_To.Tag = "業者CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD_To.ZeroPaddengFlag = true;
            this.GYOUSHA_CD_To.Enter += new System.EventHandler(this.GYOUSHA_CD_To_Enter);
            this.GYOUSHA_CD_To.Validated += new System.EventHandler(this.GYOUSHA_CD_To_Validated);
            // 
            // txt_KyotenName
            // 
            this.txt_KyotenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_KyotenName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KyotenName.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.txt_KyotenName.DBFieldsName = "";
            this.txt_KyotenName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KyotenName.DisplayPopUp = null;
            this.txt_KyotenName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenName.FocusOutCheckMethod")));
            this.txt_KyotenName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_KyotenName.ForeColor = System.Drawing.Color.Black;
            this.txt_KyotenName.IsInputErrorOccured = false;
            this.txt_KyotenName.Location = new System.Drawing.Point(180, 19);
            this.txt_KyotenName.MaxLength = 10;
            this.txt_KyotenName.Name = "txt_KyotenName";
            this.txt_KyotenName.PopupAfterExecute = null;
            this.txt_KyotenName.PopupBeforeExecute = null;
            this.txt_KyotenName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KyotenName.PopupSearchSendParams")));
            this.txt_KyotenName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_KyotenName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KyotenName.popupWindowSetting")));
            this.txt_KyotenName.ReadOnly = true;
            this.txt_KyotenName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenName.RegistCheckMethod")));
            this.txt_KyotenName.Size = new System.Drawing.Size(157, 20);
            this.txt_KyotenName.TabIndex = 11;
            this.txt_KyotenName.TabStop = false;
            this.txt_KyotenName.Tag = "拠点名が表示されます";
            // 
            // txt_KyotenCD
            // 
            this.txt_KyotenCD.BackColor = System.Drawing.SystemColors.Window;
            this.txt_KyotenCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KyotenCD.CustomFormatSetting = "00";
            this.txt_KyotenCD.DBFieldsName = "KYOTEN_CD";
            this.txt_KyotenCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KyotenCD.DisplayItemName = "拠点";
            this.txt_KyotenCD.DisplayPopUp = null;
            this.txt_KyotenCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenCD.FocusOutCheckMethod")));
            this.txt_KyotenCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_KyotenCD.ForeColor = System.Drawing.Color.Black;
            this.txt_KyotenCD.FormatSetting = "カスタム";
            this.txt_KyotenCD.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.txt_KyotenCD.IsInputErrorOccured = false;
            this.txt_KyotenCD.ItemDefinedTypes = "smallint";
            this.txt_KyotenCD.Location = new System.Drawing.Point(117, 19);
            this.txt_KyotenCD.Name = "txt_KyotenCD";
            this.txt_KyotenCD.PopupAfterExecute = null;
            this.txt_KyotenCD.PopupBeforeExecute = null;
            this.txt_KyotenCD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txt_KyotenCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KyotenCD.PopupSearchSendParams")));
            this.txt_KyotenCD.PopupSetFormField = "txt_KyotenCD,txt_KyotenName";
            this.txt_KyotenCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txt_KyotenCD.PopupWindowName = "マスタ共通ポップアップ";
            this.txt_KyotenCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KyotenCD.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txt_KyotenCD.RangeSetting = rangeSettingDto2;
            this.txt_KyotenCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenCD.RegistCheckMethod")));
            this.txt_KyotenCD.SetFormField = "txt_KyotenCD,txt_KyotenName";
            this.txt_KyotenCD.Size = new System.Drawing.Size(64, 20);
            this.txt_KyotenCD.TabIndex = 10;
            this.txt_KyotenCD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txt_KyotenCD.WordWrap = false;
            // 
            // lbl_Kyoten
            // 
            this.lbl_Kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_Kyoten.Location = new System.Drawing.Point(2, 19);
            this.lbl_Kyoten.Name = "lbl_Kyoten";
            this.lbl_Kyoten.Size = new System.Drawing.Size(110, 20);
            this.lbl_Kyoten.TabIndex = 9999;
            this.lbl_Kyoten.Text = "拠点※";
            this.lbl_Kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.rdo_Nenpou);
            this.customPanel1.Controls.Add(this.rdo_Geppou);
            this.customPanel1.Location = new System.Drawing.Point(136, 43);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(201, 20);
            this.customPanel1.TabIndex = 31;
            // 
            // rdo_Anbun
            // 
            this.rdo_Anbun.AutoSize = true;
            this.rdo_Anbun.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_Anbun.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Anbun.FocusOutCheckMethod")));
            this.rdo_Anbun.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_Anbun.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdo_Anbun.LinkedTextBox = "txt_Shuukeisuuryou";
            this.rdo_Anbun.Location = new System.Drawing.Point(188, 1);
            this.rdo_Anbun.Name = "rdo_Anbun";
            this.rdo_Anbun.PopupAfterExecute = null;
            this.rdo_Anbun.PopupBeforeExecute = null;
            this.rdo_Anbun.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_Anbun.PopupSearchSendParams")));
            this.rdo_Anbun.PopupSetFormField = "";
            this.rdo_Anbun.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_Anbun.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_Anbun.popupWindowSetting")));
            this.rdo_Anbun.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Anbun.RegistCheckMethod")));
            this.rdo_Anbun.SetFormField = "";
            this.rdo_Anbun.Size = new System.Drawing.Size(95, 17);
            this.rdo_Anbun.TabIndex = 103;
            this.rdo_Anbun.Tag = "集計対象数量を「按分数量」にする場合は、チェックを付けてください";
            this.rdo_Anbun.Text = "3.按分数量";
            this.rdo_Anbun.UseVisualStyleBackColor = true;
            this.rdo_Anbun.Value = "3";
            // 
            // rdo_JissekiKansan
            // 
            this.rdo_JissekiKansan.AutoSize = true;
            this.rdo_JissekiKansan.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_JissekiKansan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_JissekiKansan.FocusOutCheckMethod")));
            this.rdo_JissekiKansan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_JissekiKansan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdo_JissekiKansan.LinkedTextBox = "txt_Shuukeisuuryou";
            this.rdo_JissekiKansan.Location = new System.Drawing.Point(293, 1);
            this.rdo_JissekiKansan.Name = "rdo_JissekiKansan";
            this.rdo_JissekiKansan.PopupAfterExecute = null;
            this.rdo_JissekiKansan.PopupBeforeExecute = null;
            this.rdo_JissekiKansan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_JissekiKansan.PopupSearchSendParams")));
            this.rdo_JissekiKansan.PopupSetFormField = "";
            this.rdo_JissekiKansan.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_JissekiKansan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_JissekiKansan.popupWindowSetting")));
            this.rdo_JissekiKansan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_JissekiKansan.RegistCheckMethod")));
            this.rdo_JissekiKansan.SetFormField = "";
            this.rdo_JissekiKansan.Size = new System.Drawing.Size(130, 17);
            this.rdo_JissekiKansan.TabIndex = 105;
            this.rdo_JissekiKansan.Tag = "集計対象数量を「数量+換算数量」にする場合は、チェックを付けてください";
            this.rdo_JissekiKansan.Text = "4.数量+換算数量";
            this.rdo_JissekiKansan.UseVisualStyleBackColor = true;
            this.rdo_JissekiKansan.Value = "4";
            // 
            // txt_Shuukeisuuryou
            // 
            this.txt_Shuukeisuuryou.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Shuukeisuuryou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Shuukeisuuryou.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Shuukeisuuryou.DisplayItemName = "表示数量";
            this.txt_Shuukeisuuryou.DisplayPopUp = null;
            this.txt_Shuukeisuuryou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Shuukeisuuryou.FocusOutCheckMethod")));
            this.txt_Shuukeisuuryou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_Shuukeisuuryou.ForeColor = System.Drawing.Color.Black;
            this.txt_Shuukeisuuryou.GetCodeMasterField = "";
            this.txt_Shuukeisuuryou.IsInputErrorOccured = false;
            this.txt_Shuukeisuuryou.LinkedRadioButtonArray = new string[] {
        "rdo_Jisseki",
        "rdo_Kansan",
        "rdo_Anbun",
        "rdo_JissekiKansan"};
            this.txt_Shuukeisuuryou.Location = new System.Drawing.Point(117, 187);
            this.txt_Shuukeisuuryou.Name = "txt_Shuukeisuuryou";
            this.txt_Shuukeisuuryou.PopupAfterExecute = null;
            this.txt_Shuukeisuuryou.PopupBeforeExecute = null;
            this.txt_Shuukeisuuryou.PopupGetMasterField = "";
            this.txt_Shuukeisuuryou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Shuukeisuuryou.PopupSearchSendParams")));
            this.txt_Shuukeisuuryou.PopupSetFormField = "";
            this.txt_Shuukeisuuryou.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.txt_Shuukeisuuryou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Shuukeisuuryou.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_Shuukeisuuryou.RangeSetting = rangeSettingDto3;
            this.txt_Shuukeisuuryou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Shuukeisuuryou.RegistCheckMethod")));
            this.txt_Shuukeisuuryou.SetFormField = "";
            this.txt_Shuukeisuuryou.Size = new System.Drawing.Size(20, 20);
            this.txt_Shuukeisuuryou.TabIndex = 100;
            this.txt_Shuukeisuuryou.Tag = "【1～4】のいずれかで入力してください";
            this.txt_Shuukeisuuryou.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Shuukeisuuryou.WordWrap = false;
            // 
            // label_Shuukeisuuryou
            // 
            this.label_Shuukeisuuryou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label_Shuukeisuuryou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_Shuukeisuuryou.Location = new System.Drawing.Point(2, 187);
            this.label_Shuukeisuuryou.Name = "label_Shuukeisuuryou";
            this.label_Shuukeisuuryou.Size = new System.Drawing.Size(110, 20);
            this.label_Shuukeisuuryou.TabIndex = 9999;
            this.label_Shuukeisuuryou.Text = "集計対象数量※";
            this.label_Shuukeisuuryou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.rdo_Kansan);
            this.customPanel2.Controls.Add(this.rdo_Jisseki);
            this.customPanel2.Controls.Add(this.rdo_Anbun);
            this.customPanel2.Controls.Add(this.rdo_JissekiKansan);
            this.customPanel2.Location = new System.Drawing.Point(136, 187);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(464, 20);
            this.customPanel2.TabIndex = 101;
            // 
            // rdo_Kansan
            // 
            this.rdo_Kansan.AutoSize = true;
            this.rdo_Kansan.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_Kansan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Kansan.FocusOutCheckMethod")));
            this.rdo_Kansan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_Kansan.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdo_Kansan.LinkedTextBox = "txt_Shuukeisuuryou";
            this.rdo_Kansan.Location = new System.Drawing.Point(83, 1);
            this.rdo_Kansan.Name = "rdo_Kansan";
            this.rdo_Kansan.PopupAfterExecute = null;
            this.rdo_Kansan.PopupBeforeExecute = null;
            this.rdo_Kansan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_Kansan.PopupSearchSendParams")));
            this.rdo_Kansan.PopupSetFormField = "";
            this.rdo_Kansan.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_Kansan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_Kansan.popupWindowSetting")));
            this.rdo_Kansan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Kansan.RegistCheckMethod")));
            this.rdo_Kansan.SetFormField = "";
            this.rdo_Kansan.Size = new System.Drawing.Size(95, 17);
            this.rdo_Kansan.TabIndex = 102;
            this.rdo_Kansan.Tag = "集計対象数量を「換算数量」にする場合は、チェックを付けてください";
            this.rdo_Kansan.Text = "2.換算数量";
            this.rdo_Kansan.UseVisualStyleBackColor = true;
            this.rdo_Kansan.Value = "2";
            // 
            // rdo_Jisseki
            // 
            this.rdo_Jisseki.AutoSize = true;
            this.rdo_Jisseki.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdo_Jisseki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Jisseki.FocusOutCheckMethod")));
            this.rdo_Jisseki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdo_Jisseki.ForeColor = System.Drawing.SystemColors.ControlText;
            this.rdo_Jisseki.LinkedTextBox = "txt_Shuukeisuuryou";
            this.rdo_Jisseki.Location = new System.Drawing.Point(6, 1);
            this.rdo_Jisseki.Name = "rdo_Jisseki";
            this.rdo_Jisseki.PopupAfterExecute = null;
            this.rdo_Jisseki.PopupBeforeExecute = null;
            this.rdo_Jisseki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdo_Jisseki.PopupSearchSendParams")));
            this.rdo_Jisseki.PopupSetFormField = "";
            this.rdo_Jisseki.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdo_Jisseki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdo_Jisseki.popupWindowSetting")));
            this.rdo_Jisseki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdo_Jisseki.RegistCheckMethod")));
            this.rdo_Jisseki.SetFormField = "";
            this.rdo_Jisseki.Size = new System.Drawing.Size(67, 17);
            this.rdo_Jisseki.TabIndex = 101;
            this.rdo_Jisseki.Tag = "集計対象数量を「数量」にする場合は、チェックを付けてください";
            this.rdo_Jisseki.Text = "1.数量";
            this.rdo_Jisseki.UseVisualStyleBackColor = true;
            this.rdo_Jisseki.Value = "1";
            // 
            // TORIHIKISAKI_NAME_RYAKU_To
            // 
            this.TORIHIKISAKI_NAME_RYAKU_To.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU_To.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_RYAKU_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU_To.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU_To.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_NAME_RYAKU_To.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU_To.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU_To.Location = new System.Drawing.Point(546, 91);
            this.TORIHIKISAKI_NAME_RYAKU_To.Name = "TORIHIKISAKI_NAME_RYAKU_To";
            this.TORIHIKISAKI_NAME_RYAKU_To.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU_To.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU_To.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU_To.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU_To.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU_To.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU_To.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU_To.Size = new System.Drawing.Size(285, 20);
            this.TORIHIKISAKI_NAME_RYAKU_To.TabIndex = 63;
            this.TORIHIKISAKI_NAME_RYAKU_To.TabStop = false;
            this.TORIHIKISAKI_NAME_RYAKU_To.Tag = "";
            // 
            // TORIHIKISAKI_CD_To
            // 
            this.TORIHIKISAKI_CD_To.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD_To.CharacterLimitList = null;
            this.TORIHIKISAKI_CD_To.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD_To.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD_To.DisplayItemName = "取引先CDTo";
            this.TORIHIKISAKI_CD_To.DisplayPopUp = null;
            this.TORIHIKISAKI_CD_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD_To.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_CD_To.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD_To.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD_To.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD_To.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD_To.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD_To.Location = new System.Drawing.Point(497, 91);
            this.TORIHIKISAKI_CD_To.MaxLength = 6;
            this.TORIHIKISAKI_CD_To.Name = "TORIHIKISAKI_CD_To";
            this.TORIHIKISAKI_CD_To.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD_To.PopupAfterExecuteMethod = "";
            this.TORIHIKISAKI_CD_To.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD_To.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD_To.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD_To.PopupSendParams = new string[0];
            this.TORIHIKISAKI_CD_To.PopupSetFormField = "TORIHIKISAKI_CD_To, TORIHIKISAKI_NAME_RYAKU_To";
            this.TORIHIKISAKI_CD_To.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD_To.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD_To.popupWindowSetting")));
            this.TORIHIKISAKI_CD_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD_To.RegistCheckMethod")));
            this.TORIHIKISAKI_CD_To.SetFormField = "TORIHIKISAKI_CD_To, TORIHIKISAKI_NAME_RYAKU_To";
            this.TORIHIKISAKI_CD_To.ShortItemName = "取引先CDTo";
            this.TORIHIKISAKI_CD_To.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD_To.TabIndex = 62;
            this.TORIHIKISAKI_CD_To.Tag = "取引先CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD_To.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD_To.Validating += new System.ComponentModel.CancelEventHandler(this.TORIHIKISAKI_CD_To_Validating);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(466, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(19, 20);
            this.label3.TabIndex = 9999;
            this.label3.Text = "～";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TORIHIKISAKI_NAME_RYAKU_From
            // 
            this.TORIHIKISAKI_NAME_RYAKU_From.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU_From.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_RYAKU_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU_From.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU_From.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_NAME_RYAKU_From.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU_From.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU_From.Location = new System.Drawing.Point(166, 91);
            this.TORIHIKISAKI_NAME_RYAKU_From.Name = "TORIHIKISAKI_NAME_RYAKU_From";
            this.TORIHIKISAKI_NAME_RYAKU_From.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU_From.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU_From.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU_From.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU_From.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU_From.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU_From.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU_From.Size = new System.Drawing.Size(285, 20);
            this.TORIHIKISAKI_NAME_RYAKU_From.TabIndex = 61;
            this.TORIHIKISAKI_NAME_RYAKU_From.TabStop = false;
            this.TORIHIKISAKI_NAME_RYAKU_From.Tag = "";
            // 
            // TORIHIKISAKI_CD_From
            // 
            this.TORIHIKISAKI_CD_From.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD_From.CharacterLimitList = null;
            this.TORIHIKISAKI_CD_From.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD_From.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD_From.DisplayItemName = "取引先CDFrom";
            this.TORIHIKISAKI_CD_From.DisplayPopUp = null;
            this.TORIHIKISAKI_CD_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD_From.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_CD_From.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD_From.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD_From.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD_From.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD_From.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD_From.Location = new System.Drawing.Point(117, 91);
            this.TORIHIKISAKI_CD_From.MaxLength = 6;
            this.TORIHIKISAKI_CD_From.Name = "TORIHIKISAKI_CD_From";
            this.TORIHIKISAKI_CD_From.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD_From.PopupAfterExecuteMethod = "";
            this.TORIHIKISAKI_CD_From.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD_From.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD_From.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD_From.PopupSendParams = new string[0];
            this.TORIHIKISAKI_CD_From.PopupSetFormField = "TORIHIKISAKI_CD_From, TORIHIKISAKI_NAME_RYAKU_From";
            this.TORIHIKISAKI_CD_From.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD_From.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD_From.popupWindowSetting")));
            this.TORIHIKISAKI_CD_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD_From.RegistCheckMethod")));
            this.TORIHIKISAKI_CD_From.SetFormField = "TORIHIKISAKI_CD_From, TORIHIKISAKI_NAME_RYAKU_From";
            this.TORIHIKISAKI_CD_From.ShortItemName = "取引先CDFrom";
            this.TORIHIKISAKI_CD_From.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD_From.TabIndex = 60;
            this.TORIHIKISAKI_CD_From.Tag = "取引先CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD_From.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD_From.Validating += new System.ComponentModel.CancelEventHandler(this.TORIHIKISAKI_CD_From_Validating);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(2, 91);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 9999;
            this.label4.Text = "取引先";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(2, 271);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 9999;
            this.label5.Text = "締日";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label5.Visible = false;
            // 
            // SHIMEBI
            // 
            this.SHIMEBI.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHIMEBI.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHIMEBI.BackColor = System.Drawing.SystemColors.Window;
            this.SHIMEBI.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHIMEBI.DisplayPopUp = null;
            this.SHIMEBI.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHIMEBI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIMEBI.FocusOutCheckMethod")));
            this.SHIMEBI.FormattingEnabled = true;
            this.SHIMEBI.IsInputErrorOccured = false;
            this.SHIMEBI.Items.AddRange(new object[] {
            "",
            "0",
            "5",
            "10",
            "15",
            "20",
            "25",
            "31"});
            this.SHIMEBI.Location = new System.Drawing.Point(117, 271);
            this.SHIMEBI.Name = "SHIMEBI";
            this.SHIMEBI.PopupAfterExecute = null;
            this.SHIMEBI.PopupBeforeExecute = null;
            this.SHIMEBI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHIMEBI.PopupSearchSendParams")));
            this.SHIMEBI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHIMEBI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHIMEBI.popupWindowSetting")));
            this.SHIMEBI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHIMEBI.RegistCheckMethod")));
            this.SHIMEBI.Size = new System.Drawing.Size(50, 21);
            this.SHIMEBI.TabIndex = 50;
            this.SHIMEBI.Tag = "締日を入力してください";
            this.SHIMEBI.Visible = false;
            this.SHIMEBI.SelectionChangeCommitted += new System.EventHandler(this.SHIMEBI_SelectionChangeCommitted);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(2, 163);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 10000;
            this.label6.Text = "種類";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHURUI_NAME_RYAKU_To
            // 
            this.SHURUI_NAME_RYAKU_To.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHURUI_NAME_RYAKU_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHURUI_NAME_RYAKU_To.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.SHURUI_NAME_RYAKU_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHURUI_NAME_RYAKU_To.DisplayPopUp = null;
            this.SHURUI_NAME_RYAKU_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_NAME_RYAKU_To.FocusOutCheckMethod")));
            this.SHURUI_NAME_RYAKU_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHURUI_NAME_RYAKU_To.ForeColor = System.Drawing.Color.Black;
            this.SHURUI_NAME_RYAKU_To.IsInputErrorOccured = false;
            this.SHURUI_NAME_RYAKU_To.Location = new System.Drawing.Point(546, 163);
            this.SHURUI_NAME_RYAKU_To.Name = "SHURUI_NAME_RYAKU_To";
            this.SHURUI_NAME_RYAKU_To.PopupAfterExecute = null;
            this.SHURUI_NAME_RYAKU_To.PopupBeforeExecute = null;
            this.SHURUI_NAME_RYAKU_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI_NAME_RYAKU_To.PopupSearchSendParams")));
            this.SHURUI_NAME_RYAKU_To.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHURUI_NAME_RYAKU_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI_NAME_RYAKU_To.popupWindowSetting")));
            this.SHURUI_NAME_RYAKU_To.ReadOnly = true;
            this.SHURUI_NAME_RYAKU_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_NAME_RYAKU_To.RegistCheckMethod")));
            this.SHURUI_NAME_RYAKU_To.Size = new System.Drawing.Size(285, 20);
            this.SHURUI_NAME_RYAKU_To.TabIndex = 93;
            this.SHURUI_NAME_RYAKU_To.TabStop = false;
            this.SHURUI_NAME_RYAKU_To.Tag = "";
            // 
            // SHURUI_CD_To
            // 
            this.SHURUI_CD_To.BackColor = System.Drawing.SystemColors.Window;
            this.SHURUI_CD_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHURUI_CD_To.CharacterLimitList = null;
            this.SHURUI_CD_To.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.SHURUI_CD_To.DBFieldsName = "SHURUI_CD";
            this.SHURUI_CD_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHURUI_CD_To.DisplayItemName = "種類CDTo";
            this.SHURUI_CD_To.DisplayPopUp = null;
            this.SHURUI_CD_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_CD_To.FocusOutCheckMethod")));
            this.SHURUI_CD_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHURUI_CD_To.ForeColor = System.Drawing.Color.Black;
            this.SHURUI_CD_To.GetCodeMasterField = "SHURUI_CD, SHURUI_NAME_RYAKU";
            this.SHURUI_CD_To.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHURUI_CD_To.IsInputErrorOccured = false;
            this.SHURUI_CD_To.ItemDefinedTypes = "varchar";
            this.SHURUI_CD_To.Location = new System.Drawing.Point(497, 163);
            this.SHURUI_CD_To.MaxLength = 3;
            this.SHURUI_CD_To.Name = "SHURUI_CD_To";
            this.SHURUI_CD_To.PopupAfterExecute = null;
            this.SHURUI_CD_To.PopupBeforeExecute = null;
            this.SHURUI_CD_To.PopupGetMasterField = "SHURUI_CD, SHURUI_NAME_RYAKU";
            this.SHURUI_CD_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI_CD_To.PopupSearchSendParams")));
            this.SHURUI_CD_To.PopupSetFormField = "SHURUI_CD_To, SHURUI_NAME_RYAKU_To";
            this.SHURUI_CD_To.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHURUI;
            this.SHURUI_CD_To.PopupWindowName = "マスタ共通ポップアップ";
            this.SHURUI_CD_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI_CD_To.popupWindowSetting")));
            this.SHURUI_CD_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_CD_To.RegistCheckMethod")));
            this.SHURUI_CD_To.SetFormField = "SHURUI_CD_To, SHURUI_NAME_RYAKU_To";
            this.SHURUI_CD_To.ShortItemName = "種類CDTo";
            this.SHURUI_CD_To.Size = new System.Drawing.Size(50, 20);
            this.SHURUI_CD_To.TabIndex = 92;
            this.SHURUI_CD_To.Tag = "種類CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHURUI_CD_To.ZeroPaddengFlag = true;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(466, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 20);
            this.label7.TabIndex = 10005;
            this.label7.Text = "～";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHURUI_NAME_RYAKU_From
            // 
            this.SHURUI_NAME_RYAKU_From.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHURUI_NAME_RYAKU_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHURUI_NAME_RYAKU_From.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.SHURUI_NAME_RYAKU_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHURUI_NAME_RYAKU_From.DisplayPopUp = null;
            this.SHURUI_NAME_RYAKU_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_NAME_RYAKU_From.FocusOutCheckMethod")));
            this.SHURUI_NAME_RYAKU_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHURUI_NAME_RYAKU_From.ForeColor = System.Drawing.Color.Black;
            this.SHURUI_NAME_RYAKU_From.IsInputErrorOccured = false;
            this.SHURUI_NAME_RYAKU_From.Location = new System.Drawing.Point(166, 163);
            this.SHURUI_NAME_RYAKU_From.Name = "SHURUI_NAME_RYAKU_From";
            this.SHURUI_NAME_RYAKU_From.PopupAfterExecute = null;
            this.SHURUI_NAME_RYAKU_From.PopupBeforeExecute = null;
            this.SHURUI_NAME_RYAKU_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI_NAME_RYAKU_From.PopupSearchSendParams")));
            this.SHURUI_NAME_RYAKU_From.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHURUI_NAME_RYAKU_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI_NAME_RYAKU_From.popupWindowSetting")));
            this.SHURUI_NAME_RYAKU_From.ReadOnly = true;
            this.SHURUI_NAME_RYAKU_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_NAME_RYAKU_From.RegistCheckMethod")));
            this.SHURUI_NAME_RYAKU_From.Size = new System.Drawing.Size(285, 20);
            this.SHURUI_NAME_RYAKU_From.TabIndex = 91;
            this.SHURUI_NAME_RYAKU_From.TabStop = false;
            this.SHURUI_NAME_RYAKU_From.Tag = "";
            // 
            // SHURUI_CD_From
            // 
            this.SHURUI_CD_From.BackColor = System.Drawing.SystemColors.Window;
            this.SHURUI_CD_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHURUI_CD_From.CharacterLimitList = null;
            this.SHURUI_CD_From.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.SHURUI_CD_From.DBFieldsName = "SHURUI_CD";
            this.SHURUI_CD_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHURUI_CD_From.DisplayItemName = "種類CDFrom";
            this.SHURUI_CD_From.DisplayPopUp = null;
            this.SHURUI_CD_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_CD_From.FocusOutCheckMethod")));
            this.SHURUI_CD_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHURUI_CD_From.ForeColor = System.Drawing.Color.Black;
            this.SHURUI_CD_From.GetCodeMasterField = "SHURUI_CD, SHURUI_NAME_RYAKU";
            this.SHURUI_CD_From.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHURUI_CD_From.IsInputErrorOccured = false;
            this.SHURUI_CD_From.ItemDefinedTypes = "varchar";
            this.SHURUI_CD_From.Location = new System.Drawing.Point(117, 163);
            this.SHURUI_CD_From.MaxLength = 3;
            this.SHURUI_CD_From.Name = "SHURUI_CD_From";
            this.SHURUI_CD_From.PopupAfterExecute = null;
            this.SHURUI_CD_From.PopupBeforeExecute = null;
            this.SHURUI_CD_From.PopupGetMasterField = "SHURUI_CD, SHURUI_NAME_RYAKU";
            this.SHURUI_CD_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHURUI_CD_From.PopupSearchSendParams")));
            this.SHURUI_CD_From.PopupSendParams = new string[0];
            this.SHURUI_CD_From.PopupSetFormField = "SHURUI_CD_From, SHURUI_NAME_RYAKU_From";
            this.SHURUI_CD_From.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHURUI;
            this.SHURUI_CD_From.PopupWindowName = "マスタ共通ポップアップ";
            this.SHURUI_CD_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHURUI_CD_From.popupWindowSetting")));
            this.SHURUI_CD_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHURUI_CD_From.RegistCheckMethod")));
            this.SHURUI_CD_From.SetFormField = "SHURUI_CD_From, SHURUI_NAME_RYAKU_From";
            this.SHURUI_CD_From.ShortItemName = "種類CDFrom";
            this.SHURUI_CD_From.Size = new System.Drawing.Size(50, 20);
            this.SHURUI_CD_From.TabIndex = 90;
            this.SHURUI_CD_From.Tag = "種類CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHURUI_CD_From.ZeroPaddengFlag = true;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.Enabled = false;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(352, 20);
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(20, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 20028;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(844, 346);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.SHURUI_NAME_RYAKU_To);
            this.Controls.Add(this.SHURUI_CD_To);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SHURUI_NAME_RYAKU_From);
            this.Controls.Add(this.SHURUI_CD_From);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.SHIMEBI);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU_To);
            this.Controls.Add(this.TORIHIKISAKI_CD_To);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU_From);
            this.Controls.Add(this.TORIHIKISAKI_CD_From);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Shuukeisuuryou);
            this.Controls.Add(this.label_Shuukeisuuryou);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.txt_KyotenName);
            this.Controls.Add(this.txt_KyotenCD);
            this.Controls.Add(this.lbl_Kyoten);
            this.Controls.Add(this.GENBA_NAME_RYAKU_To);
            this.Controls.Add(this.GENBA_CD_To);
            this.Controls.Add(this.GYOUSHA_NAME_RYAKU_To);
            this.Controls.Add(this.GYOUSHA_CD_To);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.GENBA_NAME_RYAKU_From);
            this.Controls.Add(this.GENBA_CD_From);
            this.Controls.Add(this.GYOUSHA_NAME_RYAKU_From);
            this.Controls.Add(this.GYOUSHA_CD_From);
            this.Controls.Add(this.dtp_KikanFrom);
            this.Controls.Add(this.dtp_KikanTo);
            this.Controls.Add(this.lbl_FromTo);
            this.Controls.Add(this.txt_Shuturyokukubun);
            this.Controls.Add(this.label_Gyousha);
            this.Controls.Add(this.label_Genba);
            this.Controls.Add(this.lbl_Kikan);
            this.Controls.Add(this.label_Shuturyokukubun);
            this.Controls.Add(this.customPanel1);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label_Shuturyokukubun;
        private System.Windows.Forms.Label lbl_Kikan;
        internal r_framework.CustomControl.CustomNumericTextBox2 txt_Shuturyokukubun;
        internal r_framework.CustomControl.CustomRadioButton rdo_Geppou;
        internal r_framework.CustomControl.CustomRadioButton rdo_Nenpou;
        private System.Windows.Forms.Label lbl_FromTo;
        internal r_framework.CustomControl.CustomDateTimePicker dtp_KikanTo;
        internal r_framework.CustomControl.CustomDateTimePicker dtp_KikanFrom;
        public r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU_From;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD_From;
        public r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU_From;
        public r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD_From;
        private System.Windows.Forms.Label label_Genba;
        private System.Windows.Forms.Label label_Gyousha;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU_To;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD_To;
        public r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU_To;
        public r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD_To;
        internal r_framework.CustomControl.CustomTextBox txt_KyotenName;
        internal r_framework.CustomControl.CustomNumericTextBox2 txt_KyotenCD;
        private System.Windows.Forms.Label lbl_Kyoten;
        private r_framework.CustomControl.CustomPanel customPanel1;
		internal r_framework.CustomControl.CustomRadioButton rdo_Anbun;
		internal r_framework.CustomControl.CustomRadioButton rdo_JissekiKansan;
		internal r_framework.CustomControl.CustomNumericTextBox2 txt_Shuukeisuuryou;
		private System.Windows.Forms.Label label_Shuukeisuuryou;
        private r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU_To;
        public r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD_To;
        private System.Windows.Forms.Label label3;
        public r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU_From;
        public r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD_From;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        internal r_framework.CustomControl.CustomComboBox SHIMEBI;
        private System.Windows.Forms.Label label6;
        public r_framework.CustomControl.CustomTextBox SHURUI_NAME_RYAKU_To;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHURUI_CD_To;
        private System.Windows.Forms.Label label7;
        public r_framework.CustomControl.CustomTextBox SHURUI_NAME_RYAKU_From;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHURUI_CD_From;
        internal r_framework.CustomControl.CustomRadioButton rdo_Kansan;
        internal r_framework.CustomControl.CustomRadioButton rdo_Jisseki;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}