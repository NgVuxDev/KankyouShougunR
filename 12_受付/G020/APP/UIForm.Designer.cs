namespace Shougun.Core.Reception.UketsukeKuremuNyuuryoku
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
            this.TXT_NAIYOU1 = new r_framework.CustomControl.CustomTextBox();
            this.lblUKETSUKE_NUMBER = new System.Windows.Forms.Label();
            this.lable5 = new System.Windows.Forms.Label();
            this.lable4 = new System.Windows.Forms.Label();
            this.lable3 = new System.Windows.Forms.Label();
            this.lable1 = new System.Windows.Forms.Label();
            this.UKETSUKE_DATE = new r_framework.CustomControl.CustomDateTimePicker();
            this.TORIHIKISAKI_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.UKETSUKE_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.GYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.UKETSUKE_PREVIOUS_BUTTON = new r_framework.CustomControl.CustomButton();
            this.UKETSUKE_NEXT_BUTTON = new r_framework.CustomControl.CustomButton();
            this.EIGYOU_TANTOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.EIGYOU_TANTOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lable2 = new System.Windows.Forms.Label();
            this.TAIOUKANRYOU_DETA = new r_framework.CustomControl.CustomDateTimePicker();
            this.lable6 = new System.Windows.Forms.Label();
            this.lable7 = new System.Windows.Forms.Label();
            this.lable8 = new System.Windows.Forms.Label();
            this.lable9 = new System.Windows.Forms.Label();
            this.TXT_TOIAWASESYA = new r_framework.CustomControl.CustomTextBox();
            this.TXT_HYOUDAI = new r_framework.CustomControl.CustomTextBox();
            this.TXT_NAIYOU2 = new r_framework.CustomControl.CustomTextBox();
            this.TXT_NAIYOU3 = new r_framework.CustomControl.CustomTextBox();
            this.TXT_NAIYOU6 = new r_framework.CustomControl.CustomTextBox();
            this.TXT_NAIYOU5 = new r_framework.CustomControl.CustomTextBox();
            this.TXT_NAIYOU4 = new r_framework.CustomControl.CustomTextBox();
            this.TXT_NAIYOU8 = new r_framework.CustomControl.CustomTextBox();
            this.TXT_NAIYOU7 = new r_framework.CustomControl.CustomTextBox();
            this.UKETSUKE_DATE_MINUTE = new r_framework.CustomControl.CustomMinuteComboBox();
            this.UKETSUKE_DATE_HOUR = new r_framework.CustomControl.CustomHourComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TXT_NAIYOU1
            // 
            this.TXT_NAIYOU1.BackColor = System.Drawing.SystemColors.Window;
            this.TXT_NAIYOU1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_NAIYOU1.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TXT_NAIYOU1.DefaultBackColor = System.Drawing.Color.Empty;
            this.TXT_NAIYOU1.DisplayPopUp = null;
            this.TXT_NAIYOU1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU1.FocusOutCheckMethod")));
            this.TXT_NAIYOU1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TXT_NAIYOU1.ForeColor = System.Drawing.Color.Black;
            this.TXT_NAIYOU1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TXT_NAIYOU1.IsInputErrorOccured = false;
            this.TXT_NAIYOU1.Location = new System.Drawing.Point(115, 142);
            this.TXT_NAIYOU1.MaxLength = 80;
            this.TXT_NAIYOU1.Name = "TXT_NAIYOU1";
            this.TXT_NAIYOU1.PopupAfterExecute = null;
            this.TXT_NAIYOU1.PopupBeforeExecute = null;
            this.TXT_NAIYOU1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TXT_NAIYOU1.PopupSearchSendParams")));
            this.TXT_NAIYOU1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TXT_NAIYOU1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TXT_NAIYOU1.popupWindowSetting")));
            this.TXT_NAIYOU1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU1.RegistCheckMethod")));
            this.TXT_NAIYOU1.Size = new System.Drawing.Size(575, 20);
            this.TXT_NAIYOU1.TabIndex = 31;
            this.TXT_NAIYOU1.Tag = "クレーム内容を入力してください";
            this.TXT_NAIYOU1.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            // 
            // lblUKETSUKE_NUMBER
            // 
            this.lblUKETSUKE_NUMBER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblUKETSUKE_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUKETSUKE_NUMBER.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUKETSUKE_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUKETSUKE_NUMBER.ForeColor = System.Drawing.Color.White;
            this.lblUKETSUKE_NUMBER.Location = new System.Drawing.Point(382, 0);
            this.lblUKETSUKE_NUMBER.Name = "lblUKETSUKE_NUMBER";
            this.lblUKETSUKE_NUMBER.Size = new System.Drawing.Size(110, 20);
            this.lblUKETSUKE_NUMBER.TabIndex = 6;
            this.lblUKETSUKE_NUMBER.Text = "受付番号";
            this.lblUKETSUKE_NUMBER.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lable5
            // 
            this.lable5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable5.ForeColor = System.Drawing.Color.White;
            this.lable5.Location = new System.Drawing.Point(0, 66);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(110, 20);
            this.lable5.TabIndex = 17;
            this.lable5.Text = "現場";
            this.lable5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lable4
            // 
            this.lable4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable4.ForeColor = System.Drawing.Color.White;
            this.lable4.Location = new System.Drawing.Point(0, 44);
            this.lable4.Name = "lable4";
            this.lable4.Size = new System.Drawing.Size(110, 20);
            this.lable4.TabIndex = 13;
            this.lable4.Text = "業者※";
            this.lable4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lable3
            // 
            this.lable3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable3.ForeColor = System.Drawing.Color.White;
            this.lable3.Location = new System.Drawing.Point(0, 22);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(110, 20);
            this.lable3.TabIndex = 20;
            this.lable3.Text = "取引先";
            this.lable3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lable1
            // 
            this.lable1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable1.ForeColor = System.Drawing.Color.White;
            this.lable1.Location = new System.Drawing.Point(0, 0);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(110, 20);
            this.lable1.TabIndex = 0;
            this.lable1.Text = "受付日※";
            this.lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UKETSUKE_DATE
            // 
            this.UKETSUKE_DATE.BackColor = System.Drawing.SystemColors.Window;
            this.UKETSUKE_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UKETSUKE_DATE.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.UKETSUKE_DATE.Checked = false;
            this.UKETSUKE_DATE.CustomFormat = "yyyy/MM/dd(ddd)";
            this.UKETSUKE_DATE.DateTimeNowYear = "";
            this.UKETSUKE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_DATE.DisplayItemName = "受付日";
            this.UKETSUKE_DATE.DisplayPopUp = null;
            this.UKETSUKE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_DATE.FocusOutCheckMethod")));
            this.UKETSUKE_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UKETSUKE_DATE.ForeColor = System.Drawing.Color.Black;
            this.UKETSUKE_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.UKETSUKE_DATE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UKETSUKE_DATE.IsInputErrorOccured = false;
            this.UKETSUKE_DATE.Location = new System.Drawing.Point(115, 0);
            this.UKETSUKE_DATE.MaxLength = 10;
            this.UKETSUKE_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.UKETSUKE_DATE.Name = "UKETSUKE_DATE";
            this.UKETSUKE_DATE.NullValue = "";
            this.UKETSUKE_DATE.PopupAfterExecute = null;
            this.UKETSUKE_DATE.PopupBeforeExecute = null;
            this.UKETSUKE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UKETSUKE_DATE.PopupSearchSendParams")));
            this.UKETSUKE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UKETSUKE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UKETSUKE_DATE.popupWindowSetting")));
            this.UKETSUKE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_DATE.RegistCheckMethod")));
            this.UKETSUKE_DATE.Size = new System.Drawing.Size(135, 20);
            this.UKETSUKE_DATE.TabIndex = 1;
            this.UKETSUKE_DATE.Tag = "受付日を指定してください";
            this.UKETSUKE_DATE.Text = "2013/12/09(月)";
            this.UKETSUKE_DATE.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // TORIHIKISAKI_SEARCH_BUTTON
            // 
            this.TORIHIKISAKI_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TORIHIKISAKI_SEARCH_BUTTON.DBFieldsName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_SEARCH_BUTTON.DisplayItemName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.DisplayPopUp = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.ErrorMessage = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.TORIHIKISAKI_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_SEARCH_BUTTON.GetCodeMasterField = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.Image")));
            this.TORIHIKISAKI_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.LinkedTextBoxs = new string[0];
            this.TORIHIKISAKI_SEARCH_BUTTON.Location = new System.Drawing.Point(454, 21);
            this.TORIHIKISAKI_SEARCH_BUTTON.Name = "TORIHIKISAKI_SEARCH_BUTTON";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupAfterExecuteMethod = "TorihikisakiBtnPopupMethod";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.PopupSearchSendParams")));
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.popupWindowSetting")));
            this.TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_SEARCH_BUTTON.RegistCheckMethod")));
            this.TORIHIKISAKI_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.TORIHIKISAKI_SEARCH_BUTTON.SetFormField = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.ShortItemName = null;
            this.TORIHIKISAKI_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.TORIHIKISAKI_SEARCH_BUTTON.TabIndex = 23;
            this.TORIHIKISAKI_SEARCH_BUTTON.TabStop = false;
            this.TORIHIKISAKI_SEARCH_BUTTON.Tag = "取引先検索画面を表示します";
            this.TORIHIKISAKI_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.TORIHIKISAKI_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // TORIHIKISAKI_NAME
            // 
            this.TORIHIKISAKI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME.DBFieldsName = "";
            this.TORIHIKISAKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_NAME.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TORIHIKISAKI_NAME.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME.Location = new System.Drawing.Point(164, 22);
            this.TORIHIKISAKI_NAME.MaxLength = 40;
            this.TORIHIKISAKI_NAME.Name = "TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_NAME.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME.popupWindowSetting")));
            this.TORIHIKISAKI_NAME.ReadOnly = true;
            this.TORIHIKISAKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME.Size = new System.Drawing.Size(285, 20);
            this.TORIHIKISAKI_NAME.TabIndex = 22;
            this.TORIHIKISAKI_NAME.TabStop = false;
            this.TORIHIKISAKI_NAME.Tag = " ";
            this.TORIHIKISAKI_NAME.Text = "１２３４５６７８９０";
            this.TORIHIKISAKI_NAME.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD.ChangeUpperCase = true;
            this.TORIHIKISAKI_CD.CharacterLimitList = null;
            this.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(115, 22);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = new System.Action<r_framework.CustomControl.ICustomControl, System.Windows.Forms.DialogResult>(this.TorihikisakiPopupAfterExecute);
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD.TabIndex = 21;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.Text = "000001";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD.Enter += new System.EventHandler(this.TORIHIKISAKI_CD_Enter);
            this.TORIHIKISAKI_CD.Validated += new System.EventHandler(this.TORIHIKISAKI_CD_Validated);
            // 
            // UKETSUKE_NUMBER
            // 
            this.UKETSUKE_NUMBER.BackColor = System.Drawing.SystemColors.Window;
            this.UKETSUKE_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UKETSUKE_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_NUMBER.DisplayItemName = "受付番号";
            this.UKETSUKE_NUMBER.DisplayPopUp = null;
            this.UKETSUKE_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_NUMBER.FocusOutCheckMethod")));
            this.UKETSUKE_NUMBER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UKETSUKE_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.UKETSUKE_NUMBER.IsInputErrorOccured = false;
            this.UKETSUKE_NUMBER.Location = new System.Drawing.Point(497, 0);
            this.UKETSUKE_NUMBER.Name = "UKETSUKE_NUMBER";
            this.UKETSUKE_NUMBER.PopupAfterExecute = null;
            this.UKETSUKE_NUMBER.PopupBeforeExecute = null;
            this.UKETSUKE_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UKETSUKE_NUMBER.PopupSearchSendParams")));
            this.UKETSUKE_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UKETSUKE_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UKETSUKE_NUMBER.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.UKETSUKE_NUMBER.RangeSetting = rangeSettingDto1;
            this.UKETSUKE_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_NUMBER.RegistCheckMethod")));
            this.UKETSUKE_NUMBER.Size = new System.Drawing.Size(105, 20);
            this.UKETSUKE_NUMBER.TabIndex = 7;
            this.UKETSUKE_NUMBER.Tag = "受付番号を指定してください";
            this.UKETSUKE_NUMBER.WordWrap = false;
            this.UKETSUKE_NUMBER.Validated += new System.EventHandler(this.UKETSUKE_NUMBER_Validated);
            // 
            // GYOUSHA_SEARCH_BUTTON
            // 
            this.GYOUSHA_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GYOUSHA_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GYOUSHA_SEARCH_BUTTON.DBFieldsName = null;
            this.GYOUSHA_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_SEARCH_BUTTON.DisplayItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.DisplayPopUp = null;
            this.GYOUSHA_SEARCH_BUTTON.ErrorMessage = null;
            this.GYOUSHA_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GYOUSHA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.Image")));
            this.GYOUSHA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.GYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(454, 43);
            this.GYOUSHA_SEARCH_BUTTON.Name = "GYOUSHA_SEARCH_BUTTON";
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "GyoushaPopupAfterMethod";
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecuteMethod = "GyoushaPopupBeforeMethod";
            this.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.popupWindowSetting")));
            this.GYOUSHA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GYOUSHA_SEARCH_BUTTON.SetFormField = null;
            this.GYOUSHA_SEARCH_BUTTON.ShortItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_SEARCH_BUTTON.TabIndex = 16;
            this.GYOUSHA_SEARCH_BUTTON.TabStop = false;
            this.GYOUSHA_SEARCH_BUTTON.Tag = "業者検索画面を表示します";
            this.GYOUSHA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GYOUSHA_SEARCH_BUTTON.ZeroPaddengFlag = false;
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
            this.GYOUSHA_NAME.Location = new System.Drawing.Point(164, 44);
            this.GYOUSHA_NAME.MaxLength = 40;
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupAfterExecute = null;
            this.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.Size = new System.Drawing.Size(285, 20);
            this.GYOUSHA_NAME.TabIndex = 15;
            this.GYOUSHA_NAME.TabStop = false;
            this.GYOUSHA_NAME.Tag = " ";
            this.GYOUSHA_NAME.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
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
            this.GYOUSHA_CD.Location = new System.Drawing.Point(115, 44);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GyoushaPopupAfterMethod";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecuteMethod = "GyoushaPopupBeforeMethod";
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD.TabIndex = 14;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.Text = "000001";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Enter += new System.EventHandler(this.GYOUSHA_CD_Enter);
            this.GYOUSHA_CD.Validated += new System.EventHandler(this.GYOUSHA_CD_Validated);
            // 
            // GENBA_SEARCH_BUTTON
            // 
            this.GENBA_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GENBA_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GENBA_SEARCH_BUTTON.DBFieldsName = null;
            this.GENBA_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_SEARCH_BUTTON.DisplayItemName = null;
            this.GENBA_SEARCH_BUTTON.DisplayPopUp = null;
            this.GENBA_SEARCH_BUTTON.ErrorMessage = null;
            this.GENBA_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.GENBA_SEARCH_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GENBA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GENBA_SEARCH_BUTTON.Image")));
            this.GENBA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GENBA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GENBA_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.GENBA_SEARCH_BUTTON.Location = new System.Drawing.Point(454, 65);
            this.GENBA_SEARCH_BUTTON.Name = "GENBA_SEARCH_BUTTON";
            this.GENBA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "GenbaPopupAfter";
            this.GENBA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupBeforeExecuteMethod = "GenbaPopupBefore";
            this.GENBA_SEARCH_BUTTON.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD";
            this.GENBA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GENBA_SEARCH_BUTTON.PopupSetFormField = "GENBA_CD, GENBA_NAME,GYOUSHA_CD";
            this.GENBA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_SEARCH_BUTTON.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.popupWindowSetting")));
            this.GENBA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GENBA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GENBA_SEARCH_BUTTON.SetFormField = null;
            this.GENBA_SEARCH_BUTTON.ShortItemName = null;
            this.GENBA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GENBA_SEARCH_BUTTON.TabIndex = 20;
            this.GENBA_SEARCH_BUTTON.TabStop = false;
            this.GENBA_SEARCH_BUTTON.Tag = "現場検索画面を表示します";
            this.GENBA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GENBA_SEARCH_BUTTON.ZeroPaddengFlag = false;
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
            this.GENBA_NAME.Location = new System.Drawing.Point(164, 66);
            this.GENBA_NAME.MaxLength = 40;
            this.GENBA_NAME.Name = "GENBA_NAME";
            this.GENBA_NAME.PopupAfterExecute = null;
            this.GENBA_NAME.PopupBeforeExecute = null;
            this.GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME.PopupSearchSendParams")));
            this.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME.popupWindowSetting")));
            this.GENBA_NAME.ReadOnly = true;
            this.GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.RegistCheckMethod")));
            this.GENBA_NAME.Size = new System.Drawing.Size(285, 20);
            this.GENBA_NAME.TabIndex = 19;
            this.GENBA_NAME.TabStop = false;
            this.GENBA_NAME.Tag = " ";
            this.GENBA_NAME.Enter += new System.EventHandler(this.Control_Enter);
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
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
            this.GENBA_CD.Location = new System.Drawing.Point(115, 66);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = new System.Action<r_framework.CustomControl.ICustomControl, System.Windows.Forms.DialogResult>(this.GenbaPopupAfterExecute);
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupBeforeExecuteMethod = "GenbaPopupBefore";
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GYOUSHA_CD";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD, GENBA_NAME,GYOUSHA_CD";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD, GENBA_NAME";
            this.GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD.TabIndex = 18;
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.Text = "000001";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Enter += new System.EventHandler(this.GENBA_CD_Enter);
            this.GENBA_CD.Validated += new System.EventHandler(this.GENBA_CD_Validated);
            // 
            // UKETSUKE_PREVIOUS_BUTTON
            // 
            this.UKETSUKE_PREVIOUS_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_PREVIOUS_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UKETSUKE_PREVIOUS_BUTTON.Location = new System.Drawing.Point(607, -1);
            this.UKETSUKE_PREVIOUS_BUTTON.Name = "UKETSUKE_PREVIOUS_BUTTON";
            this.UKETSUKE_PREVIOUS_BUTTON.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.UKETSUKE_PREVIOUS_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.UKETSUKE_PREVIOUS_BUTTON.TabIndex = 8;
            this.UKETSUKE_PREVIOUS_BUTTON.Tag = "１つ前の伝票を表示します。";
            this.UKETSUKE_PREVIOUS_BUTTON.Text = "前";
            this.UKETSUKE_PREVIOUS_BUTTON.UseVisualStyleBackColor = true;
            this.UKETSUKE_PREVIOUS_BUTTON.Click += new System.EventHandler(this.UKETSUKE_PREVIOUS_BUTTON_Click);
            // 
            // UKETSUKE_NEXT_BUTTON
            // 
            this.UKETSUKE_NEXT_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_NEXT_BUTTON.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UKETSUKE_NEXT_BUTTON.Location = new System.Drawing.Point(629, -1);
            this.UKETSUKE_NEXT_BUTTON.Name = "UKETSUKE_NEXT_BUTTON";
            this.UKETSUKE_NEXT_BUTTON.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.UKETSUKE_NEXT_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.UKETSUKE_NEXT_BUTTON.TabIndex = 9;
            this.UKETSUKE_NEXT_BUTTON.Tag = "１つ後の伝票を表示します。";
            this.UKETSUKE_NEXT_BUTTON.Text = "次";
            this.UKETSUKE_NEXT_BUTTON.UseVisualStyleBackColor = true;
            this.UKETSUKE_NEXT_BUTTON.Click += new System.EventHandler(this.UKETSUKE_NEXT_BUTTON_Click);
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
            this.EIGYOU_TANTOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.EIGYOU_TANTOUSHA_NAME.DisplayPopUp = null;
            this.EIGYOU_TANTOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.FocusOutCheckMethod")));
            this.EIGYOU_TANTOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EIGYOU_TANTOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.EIGYOU_TANTOUSHA_NAME.IsInputErrorOccured = false;
            this.EIGYOU_TANTOUSHA_NAME.Location = new System.Drawing.Point(827, 0);
            this.EIGYOU_TANTOUSHA_NAME.MaxLength = 0;
            this.EIGYOU_TANTOUSHA_NAME.Name = "EIGYOU_TANTOUSHA_NAME";
            this.EIGYOU_TANTOUSHA_NAME.PopupAfterExecute = null;
            this.EIGYOU_TANTOUSHA_NAME.PopupBeforeExecute = null;
            this.EIGYOU_TANTOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.PopupSearchSendParams")));
            this.EIGYOU_TANTOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.EIGYOU_TANTOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.popupWindowSetting")));
            this.EIGYOU_TANTOUSHA_NAME.ReadOnly = true;
            this.EIGYOU_TANTOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOUSHA_NAME.RegistCheckMethod")));
            this.EIGYOU_TANTOUSHA_NAME.Size = new System.Drawing.Size(117, 20);
            this.EIGYOU_TANTOUSHA_NAME.TabIndex = 12;
            this.EIGYOU_TANTOUSHA_NAME.TabStop = false;
            this.EIGYOU_TANTOUSHA_NAME.Tag = "";
            this.EIGYOU_TANTOUSHA_NAME.Text = "溝口　学";
            // 
            // EIGYOU_TANTOUSHA_CD
            // 
            this.EIGYOU_TANTOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.EIGYOU_TANTOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EIGYOU_TANTOUSHA_CD.ChangeUpperCase = true;
            this.EIGYOU_TANTOUSHA_CD.CharacterLimitList = null;
            this.EIGYOU_TANTOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.EIGYOU_TANTOUSHA_CD.DBFieldsName = "EIGYOU_TANTOUSHA_CD";
            this.EIGYOU_TANTOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.EIGYOU_TANTOUSHA_CD.DisplayItemName = "営業担当";
            this.EIGYOU_TANTOUSHA_CD.DisplayPopUp = null;
            this.EIGYOU_TANTOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOUSHA_CD.FocusOutCheckMethod")));
            this.EIGYOU_TANTOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.EIGYOU_TANTOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.EIGYOU_TANTOUSHA_CD.GetCodeMasterField = "SHAIN_CD, SHAIN_NAME_RYAKU";
            this.EIGYOU_TANTOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.EIGYOU_TANTOUSHA_CD.IsInputErrorOccured = false;
            this.EIGYOU_TANTOUSHA_CD.ItemDefinedTypes = "varchar";
            this.EIGYOU_TANTOUSHA_CD.Location = new System.Drawing.Point(778, 0);
            this.EIGYOU_TANTOUSHA_CD.MaxLength = 6;
            this.EIGYOU_TANTOUSHA_CD.Name = "EIGYOU_TANTOUSHA_CD";
            this.EIGYOU_TANTOUSHA_CD.PopupAfterExecute = null;
            this.EIGYOU_TANTOUSHA_CD.PopupBeforeExecute = null;
            this.EIGYOU_TANTOUSHA_CD.PopupGetMasterField = "SHAIN_CD, SHAIN_NAME_RYAKU";
            this.EIGYOU_TANTOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EIGYOU_TANTOUSHA_CD.PopupSearchSendParams")));
            this.EIGYOU_TANTOUSHA_CD.PopupSetFormField = "EIGYOU_TANTOUSHA_CD, EIGYOU_TANTOUSHA_NAME";
            this.EIGYOU_TANTOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.EIGYOU_TANTOUSHA_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.EIGYOU_TANTOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EIGYOU_TANTOUSHA_CD.popupWindowSetting")));
            this.EIGYOU_TANTOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOUSHA_CD.RegistCheckMethod")));
            this.EIGYOU_TANTOUSHA_CD.SetFormField = "EIGYOU_TANTOUSHA_CD, EIGYOU_TANTOUSHA_NAME";
            this.EIGYOU_TANTOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.EIGYOU_TANTOUSHA_CD.TabIndex = 11;
            this.EIGYOU_TANTOUSHA_CD.Tag = "営業担当を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.EIGYOU_TANTOUSHA_CD.Text = "000001";
            this.EIGYOU_TANTOUSHA_CD.ZeroPaddengFlag = true;
            this.EIGYOU_TANTOUSHA_CD.Validated += new System.EventHandler(this.EIGYOU_TANTOUSHA_CD_Validated);
            // 
            // lable2
            // 
            this.lable2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable2.ForeColor = System.Drawing.Color.White;
            this.lable2.Location = new System.Drawing.Point(663, 0);
            this.lable2.Name = "lable2";
            this.lable2.Size = new System.Drawing.Size(110, 20);
            this.lable2.TabIndex = 10;
            this.lable2.Text = "営業担当";
            this.lable2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TAIOUKANRYOU_DETA
            // 
            this.TAIOUKANRYOU_DETA.BackColor = System.Drawing.SystemColors.Window;
            this.TAIOUKANRYOU_DETA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TAIOUKANRYOU_DETA.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.TAIOUKANRYOU_DETA.Checked = false;
            this.TAIOUKANRYOU_DETA.CustomFormat = "yyyy/MM/dd(ddd)";
            this.TAIOUKANRYOU_DETA.DateTimeNowYear = "";
            this.TAIOUKANRYOU_DETA.DefaultBackColor = System.Drawing.Color.Empty;
            this.TAIOUKANRYOU_DETA.DisplayPopUp = null;
            this.TAIOUKANRYOU_DETA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TAIOUKANRYOU_DETA.FocusOutCheckMethod")));
            this.TAIOUKANRYOU_DETA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TAIOUKANRYOU_DETA.ForeColor = System.Drawing.Color.Black;
            this.TAIOUKANRYOU_DETA.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TAIOUKANRYOU_DETA.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TAIOUKANRYOU_DETA.IsInputErrorOccured = false;
            this.TAIOUKANRYOU_DETA.Location = new System.Drawing.Point(610, 66);
            this.TAIOUKANRYOU_DETA.MaxLength = 10;
            this.TAIOUKANRYOU_DETA.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.TAIOUKANRYOU_DETA.Name = "TAIOUKANRYOU_DETA";
            this.TAIOUKANRYOU_DETA.NullValue = "";
            this.TAIOUKANRYOU_DETA.PopupAfterExecute = null;
            this.TAIOUKANRYOU_DETA.PopupBeforeExecute = null;
            this.TAIOUKANRYOU_DETA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TAIOUKANRYOU_DETA.PopupSearchSendParams")));
            this.TAIOUKANRYOU_DETA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TAIOUKANRYOU_DETA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TAIOUKANRYOU_DETA.popupWindowSetting")));
            this.TAIOUKANRYOU_DETA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TAIOUKANRYOU_DETA.RegistCheckMethod")));
            this.TAIOUKANRYOU_DETA.Size = new System.Drawing.Size(135, 20);
            this.TAIOUKANRYOU_DETA.TabIndex = 25;
            this.TAIOUKANRYOU_DETA.Tag = "対応完了日を指定してください";
            this.TAIOUKANRYOU_DETA.Text = "2013/12/09(月)";
            this.TAIOUKANRYOU_DETA.Value = new System.DateTime(2013, 12, 9, 0, 0, 0, 0);
            // 
            // lable6
            // 
            this.lable6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable6.ForeColor = System.Drawing.Color.White;
            this.lable6.Location = new System.Drawing.Point(495, 66);
            this.lable6.Name = "lable6";
            this.lable6.Size = new System.Drawing.Size(110, 20);
            this.lable6.TabIndex = 24;
            this.lable6.Text = "対応完了日";
            this.lable6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lable7
            // 
            this.lable7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable7.ForeColor = System.Drawing.Color.White;
            this.lable7.Location = new System.Drawing.Point(0, 98);
            this.lable7.Name = "lable7";
            this.lable7.Size = new System.Drawing.Size(110, 20);
            this.lable7.TabIndex = 26;
            this.lable7.Text = "表題※";
            this.lable7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lable8
            // 
            this.lable8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable8.ForeColor = System.Drawing.Color.White;
            this.lable8.Location = new System.Drawing.Point(0, 120);
            this.lable8.Name = "lable8";
            this.lable8.Size = new System.Drawing.Size(110, 20);
            this.lable8.TabIndex = 28;
            this.lable8.Text = "先方問合せ者※";
            this.lable8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lable9
            // 
            this.lable9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable9.ForeColor = System.Drawing.Color.White;
            this.lable9.Location = new System.Drawing.Point(0, 142);
            this.lable9.Name = "lable9";
            this.lable9.Size = new System.Drawing.Size(110, 20);
            this.lable9.TabIndex = 30;
            this.lable9.Text = "内容";
            this.lable9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TXT_TOIAWASESYA
            // 
            this.TXT_TOIAWASESYA.BackColor = System.Drawing.SystemColors.Window;
            this.TXT_TOIAWASESYA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_TOIAWASESYA.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.TXT_TOIAWASESYA.DefaultBackColor = System.Drawing.Color.Empty;
            this.TXT_TOIAWASESYA.DisplayItemName = "先方問合せ者";
            this.TXT_TOIAWASESYA.DisplayPopUp = null;
            this.TXT_TOIAWASESYA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_TOIAWASESYA.FocusOutCheckMethod")));
            this.TXT_TOIAWASESYA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TXT_TOIAWASESYA.ForeColor = System.Drawing.Color.Black;
            this.TXT_TOIAWASESYA.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TXT_TOIAWASESYA.IsInputErrorOccured = false;
            this.TXT_TOIAWASESYA.Location = new System.Drawing.Point(115, 120);
            this.TXT_TOIAWASESYA.MaxLength = 40;
            this.TXT_TOIAWASESYA.Name = "TXT_TOIAWASESYA";
            this.TXT_TOIAWASESYA.PopupAfterExecute = null;
            this.TXT_TOIAWASESYA.PopupBeforeExecute = null;
            this.TXT_TOIAWASESYA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TXT_TOIAWASESYA.PopupSearchSendParams")));
            this.TXT_TOIAWASESYA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TXT_TOIAWASESYA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TXT_TOIAWASESYA.popupWindowSetting")));
            this.TXT_TOIAWASESYA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_TOIAWASESYA.RegistCheckMethod")));
            this.TXT_TOIAWASESYA.Size = new System.Drawing.Size(295, 20);
            this.TXT_TOIAWASESYA.TabIndex = 29;
            this.TXT_TOIAWASESYA.Tag = "先方問合せ者を入力してください";
            this.TXT_TOIAWASESYA.Text = "１２３４５６７８９０１２３４５６７８９０";
            // 
            // TXT_HYOUDAI
            // 
            this.TXT_HYOUDAI.BackColor = System.Drawing.SystemColors.Window;
            this.TXT_HYOUDAI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_HYOUDAI.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.TXT_HYOUDAI.DefaultBackColor = System.Drawing.Color.Empty;
            this.TXT_HYOUDAI.DisplayItemName = "表題";
            this.TXT_HYOUDAI.DisplayPopUp = null;
            this.TXT_HYOUDAI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_HYOUDAI.FocusOutCheckMethod")));
            this.TXT_HYOUDAI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TXT_HYOUDAI.ForeColor = System.Drawing.Color.Black;
            this.TXT_HYOUDAI.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TXT_HYOUDAI.IsInputErrorOccured = false;
            this.TXT_HYOUDAI.Location = new System.Drawing.Point(115, 98);
            this.TXT_HYOUDAI.MaxLength = 40;
            this.TXT_HYOUDAI.Name = "TXT_HYOUDAI";
            this.TXT_HYOUDAI.PopupAfterExecute = null;
            this.TXT_HYOUDAI.PopupBeforeExecute = null;
            this.TXT_HYOUDAI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TXT_HYOUDAI.PopupSearchSendParams")));
            this.TXT_HYOUDAI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TXT_HYOUDAI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TXT_HYOUDAI.popupWindowSetting")));
            this.TXT_HYOUDAI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_HYOUDAI.RegistCheckMethod")));
            this.TXT_HYOUDAI.Size = new System.Drawing.Size(295, 20);
            this.TXT_HYOUDAI.TabIndex = 27;
            this.TXT_HYOUDAI.Tag = "表題を入力してください";
            this.TXT_HYOUDAI.Text = "１２３４５６７８９０１２３４５６７８９０";
            // 
            // TXT_NAIYOU2
            // 
            this.TXT_NAIYOU2.BackColor = System.Drawing.SystemColors.Window;
            this.TXT_NAIYOU2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_NAIYOU2.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TXT_NAIYOU2.DefaultBackColor = System.Drawing.Color.Empty;
            this.TXT_NAIYOU2.DisplayPopUp = null;
            this.TXT_NAIYOU2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU2.FocusOutCheckMethod")));
            this.TXT_NAIYOU2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TXT_NAIYOU2.ForeColor = System.Drawing.Color.Black;
            this.TXT_NAIYOU2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TXT_NAIYOU2.IsInputErrorOccured = false;
            this.TXT_NAIYOU2.Location = new System.Drawing.Point(115, 164);
            this.TXT_NAIYOU2.MaxLength = 80;
            this.TXT_NAIYOU2.Name = "TXT_NAIYOU2";
            this.TXT_NAIYOU2.PopupAfterExecute = null;
            this.TXT_NAIYOU2.PopupBeforeExecute = null;
            this.TXT_NAIYOU2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TXT_NAIYOU2.PopupSearchSendParams")));
            this.TXT_NAIYOU2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TXT_NAIYOU2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TXT_NAIYOU2.popupWindowSetting")));
            this.TXT_NAIYOU2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU2.RegistCheckMethod")));
            this.TXT_NAIYOU2.Size = new System.Drawing.Size(575, 20);
            this.TXT_NAIYOU2.TabIndex = 32;
            this.TXT_NAIYOU2.Tag = "クレーム内容を入力してください";
            this.TXT_NAIYOU2.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            // 
            // TXT_NAIYOU3
            // 
            this.TXT_NAIYOU3.BackColor = System.Drawing.SystemColors.Window;
            this.TXT_NAIYOU3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_NAIYOU3.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TXT_NAIYOU3.DefaultBackColor = System.Drawing.Color.Empty;
            this.TXT_NAIYOU3.DisplayPopUp = null;
            this.TXT_NAIYOU3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU3.FocusOutCheckMethod")));
            this.TXT_NAIYOU3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TXT_NAIYOU3.ForeColor = System.Drawing.Color.Black;
            this.TXT_NAIYOU3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TXT_NAIYOU3.IsInputErrorOccured = false;
            this.TXT_NAIYOU3.Location = new System.Drawing.Point(115, 186);
            this.TXT_NAIYOU3.MaxLength = 80;
            this.TXT_NAIYOU3.Name = "TXT_NAIYOU3";
            this.TXT_NAIYOU3.PopupAfterExecute = null;
            this.TXT_NAIYOU3.PopupBeforeExecute = null;
            this.TXT_NAIYOU3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TXT_NAIYOU3.PopupSearchSendParams")));
            this.TXT_NAIYOU3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TXT_NAIYOU3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TXT_NAIYOU3.popupWindowSetting")));
            this.TXT_NAIYOU3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU3.RegistCheckMethod")));
            this.TXT_NAIYOU3.Size = new System.Drawing.Size(575, 20);
            this.TXT_NAIYOU3.TabIndex = 33;
            this.TXT_NAIYOU3.Tag = "クレーム内容を入力してください";
            this.TXT_NAIYOU3.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            // 
            // TXT_NAIYOU6
            // 
            this.TXT_NAIYOU6.BackColor = System.Drawing.SystemColors.Window;
            this.TXT_NAIYOU6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_NAIYOU6.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TXT_NAIYOU6.DefaultBackColor = System.Drawing.Color.Empty;
            this.TXT_NAIYOU6.DisplayPopUp = null;
            this.TXT_NAIYOU6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU6.FocusOutCheckMethod")));
            this.TXT_NAIYOU6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TXT_NAIYOU6.ForeColor = System.Drawing.Color.Black;
            this.TXT_NAIYOU6.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TXT_NAIYOU6.IsInputErrorOccured = false;
            this.TXT_NAIYOU6.Location = new System.Drawing.Point(115, 252);
            this.TXT_NAIYOU6.MaxLength = 80;
            this.TXT_NAIYOU6.Name = "TXT_NAIYOU6";
            this.TXT_NAIYOU6.PopupAfterExecute = null;
            this.TXT_NAIYOU6.PopupBeforeExecute = null;
            this.TXT_NAIYOU6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TXT_NAIYOU6.PopupSearchSendParams")));
            this.TXT_NAIYOU6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TXT_NAIYOU6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TXT_NAIYOU6.popupWindowSetting")));
            this.TXT_NAIYOU6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU6.RegistCheckMethod")));
            this.TXT_NAIYOU6.Size = new System.Drawing.Size(575, 20);
            this.TXT_NAIYOU6.TabIndex = 36;
            this.TXT_NAIYOU6.Tag = "クレーム内容を入力してください";
            this.TXT_NAIYOU6.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            // 
            // TXT_NAIYOU5
            // 
            this.TXT_NAIYOU5.BackColor = System.Drawing.SystemColors.Window;
            this.TXT_NAIYOU5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_NAIYOU5.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TXT_NAIYOU5.DefaultBackColor = System.Drawing.Color.Empty;
            this.TXT_NAIYOU5.DisplayPopUp = null;
            this.TXT_NAIYOU5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU5.FocusOutCheckMethod")));
            this.TXT_NAIYOU5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TXT_NAIYOU5.ForeColor = System.Drawing.Color.Black;
            this.TXT_NAIYOU5.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TXT_NAIYOU5.IsInputErrorOccured = false;
            this.TXT_NAIYOU5.Location = new System.Drawing.Point(115, 230);
            this.TXT_NAIYOU5.MaxLength = 80;
            this.TXT_NAIYOU5.Name = "TXT_NAIYOU5";
            this.TXT_NAIYOU5.PopupAfterExecute = null;
            this.TXT_NAIYOU5.PopupBeforeExecute = null;
            this.TXT_NAIYOU5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TXT_NAIYOU5.PopupSearchSendParams")));
            this.TXT_NAIYOU5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TXT_NAIYOU5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TXT_NAIYOU5.popupWindowSetting")));
            this.TXT_NAIYOU5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU5.RegistCheckMethod")));
            this.TXT_NAIYOU5.Size = new System.Drawing.Size(575, 20);
            this.TXT_NAIYOU5.TabIndex = 35;
            this.TXT_NAIYOU5.Tag = "クレーム内容を入力してください";
            this.TXT_NAIYOU5.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            // 
            // TXT_NAIYOU4
            // 
            this.TXT_NAIYOU4.BackColor = System.Drawing.SystemColors.Window;
            this.TXT_NAIYOU4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_NAIYOU4.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TXT_NAIYOU4.DefaultBackColor = System.Drawing.Color.Empty;
            this.TXT_NAIYOU4.DisplayPopUp = null;
            this.TXT_NAIYOU4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU4.FocusOutCheckMethod")));
            this.TXT_NAIYOU4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TXT_NAIYOU4.ForeColor = System.Drawing.Color.Black;
            this.TXT_NAIYOU4.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TXT_NAIYOU4.IsInputErrorOccured = false;
            this.TXT_NAIYOU4.Location = new System.Drawing.Point(115, 208);
            this.TXT_NAIYOU4.MaxLength = 80;
            this.TXT_NAIYOU4.Name = "TXT_NAIYOU4";
            this.TXT_NAIYOU4.PopupAfterExecute = null;
            this.TXT_NAIYOU4.PopupBeforeExecute = null;
            this.TXT_NAIYOU4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TXT_NAIYOU4.PopupSearchSendParams")));
            this.TXT_NAIYOU4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TXT_NAIYOU4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TXT_NAIYOU4.popupWindowSetting")));
            this.TXT_NAIYOU4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU4.RegistCheckMethod")));
            this.TXT_NAIYOU4.Size = new System.Drawing.Size(575, 20);
            this.TXT_NAIYOU4.TabIndex = 34;
            this.TXT_NAIYOU4.Tag = "クレーム内容を入力してください";
            this.TXT_NAIYOU4.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            // 
            // TXT_NAIYOU8
            // 
            this.TXT_NAIYOU8.BackColor = System.Drawing.SystemColors.Window;
            this.TXT_NAIYOU8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_NAIYOU8.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TXT_NAIYOU8.DefaultBackColor = System.Drawing.Color.Empty;
            this.TXT_NAIYOU8.DisplayPopUp = null;
            this.TXT_NAIYOU8.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU8.FocusOutCheckMethod")));
            this.TXT_NAIYOU8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TXT_NAIYOU8.ForeColor = System.Drawing.Color.Black;
            this.TXT_NAIYOU8.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TXT_NAIYOU8.IsInputErrorOccured = false;
            this.TXT_NAIYOU8.Location = new System.Drawing.Point(115, 296);
            this.TXT_NAIYOU8.MaxLength = 80;
            this.TXT_NAIYOU8.Name = "TXT_NAIYOU8";
            this.TXT_NAIYOU8.PopupAfterExecute = null;
            this.TXT_NAIYOU8.PopupBeforeExecute = null;
            this.TXT_NAIYOU8.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TXT_NAIYOU8.PopupSearchSendParams")));
            this.TXT_NAIYOU8.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TXT_NAIYOU8.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TXT_NAIYOU8.popupWindowSetting")));
            this.TXT_NAIYOU8.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU8.RegistCheckMethod")));
            this.TXT_NAIYOU8.Size = new System.Drawing.Size(575, 20);
            this.TXT_NAIYOU8.TabIndex = 38;
            this.TXT_NAIYOU8.Tag = "クレーム内容を入力してください";
            this.TXT_NAIYOU8.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            // 
            // TXT_NAIYOU7
            // 
            this.TXT_NAIYOU7.BackColor = System.Drawing.SystemColors.Window;
            this.TXT_NAIYOU7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TXT_NAIYOU7.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.TXT_NAIYOU7.DefaultBackColor = System.Drawing.Color.Empty;
            this.TXT_NAIYOU7.DisplayPopUp = null;
            this.TXT_NAIYOU7.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU7.FocusOutCheckMethod")));
            this.TXT_NAIYOU7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TXT_NAIYOU7.ForeColor = System.Drawing.Color.Black;
            this.TXT_NAIYOU7.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.TXT_NAIYOU7.IsInputErrorOccured = false;
            this.TXT_NAIYOU7.Location = new System.Drawing.Point(115, 274);
            this.TXT_NAIYOU7.MaxLength = 80;
            this.TXT_NAIYOU7.Name = "TXT_NAIYOU7";
            this.TXT_NAIYOU7.PopupAfterExecute = null;
            this.TXT_NAIYOU7.PopupBeforeExecute = null;
            this.TXT_NAIYOU7.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TXT_NAIYOU7.PopupSearchSendParams")));
            this.TXT_NAIYOU7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TXT_NAIYOU7.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TXT_NAIYOU7.popupWindowSetting")));
            this.TXT_NAIYOU7.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TXT_NAIYOU7.RegistCheckMethod")));
            this.TXT_NAIYOU7.Size = new System.Drawing.Size(575, 20);
            this.TXT_NAIYOU7.TabIndex = 37;
            this.TXT_NAIYOU7.Tag = "クレーム内容を入力してください";
            this.TXT_NAIYOU7.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            // 
            // UKETSUKE_DATE_MINUTE
            // 
            this.UKETSUKE_DATE_MINUTE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.UKETSUKE_DATE_MINUTE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.UKETSUKE_DATE_MINUTE.BackColor = System.Drawing.SystemColors.Window;
            this.UKETSUKE_DATE_MINUTE.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.UKETSUKE_DATE_MINUTE.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_DATE_MINUTE.DisplayPopUp = null;
            this.UKETSUKE_DATE_MINUTE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UKETSUKE_DATE_MINUTE.DropDownWidth = 42;
            this.UKETSUKE_DATE_MINUTE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_DATE_MINUTE.FocusOutCheckMethod")));
            this.UKETSUKE_DATE_MINUTE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UKETSUKE_DATE_MINUTE.FormattingEnabled = true;
            this.UKETSUKE_DATE_MINUTE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UKETSUKE_DATE_MINUTE.IsInputErrorOccured = false;
            this.UKETSUKE_DATE_MINUTE.ItemDefinedTypes = "varchar";
            this.UKETSUKE_DATE_MINUTE.LinkedHourComboBox = null;
            this.UKETSUKE_DATE_MINUTE.Location = new System.Drawing.Point(311, 0);
            this.UKETSUKE_DATE_MINUTE.MaxLength = 2;
            this.UKETSUKE_DATE_MINUTE.Name = "UKETSUKE_DATE_MINUTE";
            this.UKETSUKE_DATE_MINUTE.PopupAfterExecute = null;
            this.UKETSUKE_DATE_MINUTE.PopupBeforeExecute = null;
            this.UKETSUKE_DATE_MINUTE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UKETSUKE_DATE_MINUTE.PopupSearchSendParams")));
            this.UKETSUKE_DATE_MINUTE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UKETSUKE_DATE_MINUTE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UKETSUKE_DATE_MINUTE.popupWindowSetting")));
            this.UKETSUKE_DATE_MINUTE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_DATE_MINUTE.RegistCheckMethod")));
            this.UKETSUKE_DATE_MINUTE.Size = new System.Drawing.Size(42, 21);
            this.UKETSUKE_DATE_MINUTE.TabIndex = 4;
            this.UKETSUKE_DATE_MINUTE.Tag = "受付時間（分）を指定してください";
            this.UKETSUKE_DATE_MINUTE.Validated += new System.EventHandler(this.UKETSUKE_DATE_MINUTE_Validated);
            // 
            // UKETSUKE_DATE_HOUR
            // 
            this.UKETSUKE_DATE_HOUR.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.UKETSUKE_DATE_HOUR.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.UKETSUKE_DATE_HOUR.BackColor = System.Drawing.SystemColors.Window;
            this.UKETSUKE_DATE_HOUR.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.UKETSUKE_DATE_HOUR.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_DATE_HOUR.DisplayPopUp = null;
            this.UKETSUKE_DATE_HOUR.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.UKETSUKE_DATE_HOUR.DropDownWidth = 42;
            this.UKETSUKE_DATE_HOUR.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_DATE_HOUR.FocusOutCheckMethod")));
            this.UKETSUKE_DATE_HOUR.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UKETSUKE_DATE_HOUR.FormattingEnabled = true;
            this.UKETSUKE_DATE_HOUR.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UKETSUKE_DATE_HOUR.IsInputErrorOccured = false;
            this.UKETSUKE_DATE_HOUR.ItemDefinedTypes = "varchar";
            this.UKETSUKE_DATE_HOUR.LinkedMinuteComboBox = null;
            this.UKETSUKE_DATE_HOUR.Location = new System.Drawing.Point(252, 0);
            this.UKETSUKE_DATE_HOUR.MaxLength = 2;
            this.UKETSUKE_DATE_HOUR.Name = "UKETSUKE_DATE_HOUR";
            this.UKETSUKE_DATE_HOUR.PopupAfterExecute = null;
            this.UKETSUKE_DATE_HOUR.PopupBeforeExecute = null;
            this.UKETSUKE_DATE_HOUR.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UKETSUKE_DATE_HOUR.PopupSearchSendParams")));
            this.UKETSUKE_DATE_HOUR.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UKETSUKE_DATE_HOUR.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UKETSUKE_DATE_HOUR.popupWindowSetting")));
            this.UKETSUKE_DATE_HOUR.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_DATE_HOUR.RegistCheckMethod")));
            this.UKETSUKE_DATE_HOUR.Size = new System.Drawing.Size(42, 21);
            this.UKETSUKE_DATE_HOUR.TabIndex = 2;
            this.UKETSUKE_DATE_HOUR.Tag = "受付時間（時）を指定してください";
            this.UKETSUKE_DATE_HOUR.Validated += new System.EventHandler(this.UKETSUKE_DATE_HOUR_Validated);
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(355, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(15, 20);
            this.label10.TabIndex = 5;
            this.label10.Text = "分";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(296, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(15, 20);
            this.label11.TabIndex = 3;
            this.label11.Text = "時";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.UKETSUKE_DATE_MINUTE);
            this.Controls.Add(this.UKETSUKE_DATE_HOUR);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.TXT_NAIYOU8);
            this.Controls.Add(this.TXT_NAIYOU7);
            this.Controls.Add(this.TXT_NAIYOU6);
            this.Controls.Add(this.TXT_NAIYOU5);
            this.Controls.Add(this.TXT_NAIYOU4);
            this.Controls.Add(this.TXT_NAIYOU3);
            this.Controls.Add(this.TXT_NAIYOU2);
            this.Controls.Add(this.TXT_HYOUDAI);
            this.Controls.Add(this.TXT_TOIAWASESYA);
            this.Controls.Add(this.lable9);
            this.Controls.Add(this.lable8);
            this.Controls.Add(this.lable7);
            this.Controls.Add(this.TAIOUKANRYOU_DETA);
            this.Controls.Add(this.lable6);
            this.Controls.Add(this.EIGYOU_TANTOUSHA_NAME);
            this.Controls.Add(this.EIGYOU_TANTOUSHA_CD);
            this.Controls.Add(this.UKETSUKE_NEXT_BUTTON);
            this.Controls.Add(this.UKETSUKE_PREVIOUS_BUTTON);
            this.Controls.Add(this.GENBA_SEARCH_BUTTON);
            this.Controls.Add(this.GENBA_NAME);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GYOUSHA_SEARCH_BUTTON);
            this.Controls.Add(this.GYOUSHA_NAME);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.UKETSUKE_NUMBER);
            this.Controls.Add(this.TORIHIKISAKI_SEARCH_BUTTON);
            this.Controls.Add(this.TORIHIKISAKI_NAME);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.UKETSUKE_DATE);
            this.Controls.Add(this.TXT_NAIYOU1);
            this.Controls.Add(this.lable2);
            this.Controls.Add(this.lblUKETSUKE_NUMBER);
            this.Controls.Add(this.lable5);
            this.Controls.Add(this.lable4);
            this.Controls.Add(this.lable3);
            this.Controls.Add(this.lable1);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Shown += new System.EventHandler(this.UIForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lable5;
        private System.Windows.Forms.Label lable4;
        private System.Windows.Forms.Label lable3;
        private System.Windows.Forms.Label lable1;
        internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomNumericTextBox2 UKETSUKE_NUMBER;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox TXT_NAIYOU1;
        internal r_framework.CustomControl.CustomDateTimePicker UKETSUKE_DATE;
        internal r_framework.CustomControl.CustomButton UKETSUKE_PREVIOUS_BUTTON;
        internal r_framework.CustomControl.CustomButton UKETSUKE_NEXT_BUTTON;
        internal r_framework.CustomControl.CustomTextBox EIGYOU_TANTOUSHA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox EIGYOU_TANTOUSHA_CD;
        private System.Windows.Forms.Label lable2;
        internal r_framework.CustomControl.CustomDateTimePicker TAIOUKANRYOU_DETA;
        private System.Windows.Forms.Label lable6;
        private System.Windows.Forms.Label lable7;
        private System.Windows.Forms.Label lable8;
        private System.Windows.Forms.Label lable9;
        internal r_framework.CustomControl.CustomTextBox TXT_TOIAWASESYA;
        internal r_framework.CustomControl.CustomTextBox TXT_HYOUDAI;
        internal r_framework.CustomControl.CustomTextBox TXT_NAIYOU2;
        internal r_framework.CustomControl.CustomTextBox TXT_NAIYOU3;
        internal r_framework.CustomControl.CustomTextBox TXT_NAIYOU6;
        internal r_framework.CustomControl.CustomTextBox TXT_NAIYOU5;
        internal r_framework.CustomControl.CustomTextBox TXT_NAIYOU4;
        internal r_framework.CustomControl.CustomTextBox TXT_NAIYOU8;
        internal r_framework.CustomControl.CustomTextBox TXT_NAIYOU7;
        internal System.Windows.Forms.Label lblUKETSUKE_NUMBER;
        internal r_framework.CustomControl.CustomMinuteComboBox UKETSUKE_DATE_MINUTE;
        internal r_framework.CustomControl.CustomHourComboBox UKETSUKE_DATE_HOUR;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
    }
}