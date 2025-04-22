using System.Windows.Forms;
namespace Shougun.Core.Master.CourseIchiran
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.LBL_DAY = new System.Windows.Forms.Label();
            this.COURSE_NAME_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.COURSE_NAME_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.COURSE_NAME = new r_framework.CustomControl.CustomTextBox();
            this.LBL_COURSE_NAME = new System.Windows.Forms.Label();
            this.LBL_GYOUSHA = new System.Windows.Forms.Label();
            this.GYOUSHA_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.LBL_GENBA = new System.Windows.Forms.Label();
            this.GENBA_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.LBL_HINMEI = new System.Windows.Forms.Label();
            this.HINMEI_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.HINMEI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HINMEI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.DAY_CD_8 = new r_framework.CustomControl.CustomRadioButton();
            this.DAY_CD_7 = new r_framework.CustomControl.CustomRadioButton();
            this.DAY_CD_6 = new r_framework.CustomControl.CustomRadioButton();
            this.DAY_CD_5 = new r_framework.CustomControl.CustomRadioButton();
            this.DAY_CD_4 = new r_framework.CustomControl.CustomRadioButton();
            this.DAY_CD_3 = new r_framework.CustomControl.CustomRadioButton();
            this.DAY_CD_2 = new r_framework.CustomControl.CustomRadioButton();
            this.DAY_CD_1 = new r_framework.CustomControl.CustomRadioButton();
            this.DAY_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.COURSE_NAME_CD_FUKUSHA = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.COURSE_NAME_FUKUSHA = new r_framework.CustomControl.CustomTextBox();
            this.DAY_CD_FUKUSHA = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DAY_NAME_FUKUSHA = new r_framework.CustomControl.CustomTextBox();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.searchString.Location = new System.Drawing.Point(0, 3);
            this.searchString.PopupSearchSendParams = null;
            this.searchString.ReadOnly = true;
            this.searchString.Size = new System.Drawing.Size(740, 180);
            this.searchString.TabIndex = 1;
            this.searchString.TabStop = false;
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(4, 510);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 24;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(204, 510);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 25;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 510);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 26;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(604, 510);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 27;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(804, 510);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 28;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(4, 131);
            this.customSortHeader1.TabIndex = 23;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(4, 155);
            this.customSearchHeader1.Visible = true;
            // 
            // LBL_DAY
            // 
            this.LBL_DAY.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_DAY.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_DAY.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_DAY.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LBL_DAY.ForeColor = System.Drawing.Color.White;
            this.LBL_DAY.Location = new System.Drawing.Point(3, 0);
            this.LBL_DAY.Name = "LBL_DAY";
            this.LBL_DAY.Size = new System.Drawing.Size(91, 20);
            this.LBL_DAY.TabIndex = 1;
            this.LBL_DAY.Text = "曜日※";
            this.LBL_DAY.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // COURSE_NAME_POPUP
            // 
            this.COURSE_NAME_POPUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.COURSE_NAME_POPUP.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.COURSE_NAME_POPUP.DBFieldsName = null;
            this.COURSE_NAME_POPUP.DefaultBackColor = System.Drawing.Color.Empty;
            this.COURSE_NAME_POPUP.DisplayItemName = null;
            this.COURSE_NAME_POPUP.DisplayPopUp = null;
            this.COURSE_NAME_POPUP.ErrorMessage = null;
            this.COURSE_NAME_POPUP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COURSE_NAME_POPUP.FocusOutCheckMethod")));
            this.COURSE_NAME_POPUP.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.COURSE_NAME_POPUP.GetCodeMasterField = null;
            this.COURSE_NAME_POPUP.Image = ((System.Drawing.Image)(resources.GetObject("COURSE_NAME_POPUP.Image")));
            this.COURSE_NAME_POPUP.ItemDefinedTypes = null;
            this.COURSE_NAME_POPUP.LinkedSettingTextBox = null;
            this.COURSE_NAME_POPUP.LinkedTextBoxs = null;
            this.COURSE_NAME_POPUP.Location = new System.Drawing.Point(447, 21);
            this.COURSE_NAME_POPUP.Name = "COURSE_NAME_POPUP";
            this.COURSE_NAME_POPUP.PopupAfterExecute = null;
            this.COURSE_NAME_POPUP.PopupAfterExecuteMethod = "";
            this.COURSE_NAME_POPUP.PopupBeforeExecute = null;
            this.COURSE_NAME_POPUP.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU";
            this.COURSE_NAME_POPUP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COURSE_NAME_POPUP.PopupSearchSendParams")));
            this.COURSE_NAME_POPUP.PopupSetFormField = "COURSE_NAME_CD,COURSE_NAME";
            this.COURSE_NAME_POPUP.PopupWindowId = r_framework.Const.WINDOW_ID.M_COURSE_NAME;
            this.COURSE_NAME_POPUP.PopupWindowName = "マスタ共通ポップアップ";
            this.COURSE_NAME_POPUP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COURSE_NAME_POPUP.popupWindowSetting")));
            this.COURSE_NAME_POPUP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COURSE_NAME_POPUP.RegistCheckMethod")));
            this.COURSE_NAME_POPUP.SearchDisplayFlag = 0;
            this.COURSE_NAME_POPUP.SetFormField = "COURSE_NAME_CD,COURSE_NAME";
            this.COURSE_NAME_POPUP.ShortItemName = null;
            this.COURSE_NAME_POPUP.Size = new System.Drawing.Size(22, 22);
            this.COURSE_NAME_POPUP.TabIndex = 7;
            this.COURSE_NAME_POPUP.TabStop = false;
            this.COURSE_NAME_POPUP.Tag = "コース名の検索を行います";
            this.COURSE_NAME_POPUP.Text = " ";
            this.COURSE_NAME_POPUP.UseVisualStyleBackColor = false;
            this.COURSE_NAME_POPUP.ZeroPaddengFlag = false;
            // 
            // COURSE_NAME_CD
            // 
            this.COURSE_NAME_CD.BackColor = System.Drawing.SystemColors.Window;
            this.COURSE_NAME_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.COURSE_NAME_CD.ChangeUpperCase = true;
            this.COURSE_NAME_CD.CharacterLimitList = null;
            this.COURSE_NAME_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.COURSE_NAME_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.COURSE_NAME_CD.DisplayPopUp = null;
            this.COURSE_NAME_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COURSE_NAME_CD.FocusOutCheckMethod")));
            this.COURSE_NAME_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.COURSE_NAME_CD.ForeColor = System.Drawing.Color.Black;
            this.COURSE_NAME_CD.GetCodeMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU";
            this.COURSE_NAME_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.COURSE_NAME_CD.IsInputErrorOccured = false;
            this.COURSE_NAME_CD.Location = new System.Drawing.Point(93, 22);
            this.COURSE_NAME_CD.MaxLength = 6;
            this.COURSE_NAME_CD.Name = "COURSE_NAME_CD";
            this.COURSE_NAME_CD.PopupAfterExecute = null;
            this.COURSE_NAME_CD.PopupAfterExecuteMethod = "";
            this.COURSE_NAME_CD.PopupBeforeExecute = null;
            this.COURSE_NAME_CD.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU";
            this.COURSE_NAME_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COURSE_NAME_CD.PopupSearchSendParams")));
            this.COURSE_NAME_CD.PopupSendParams = new string[0];
            this.COURSE_NAME_CD.PopupSetFormField = "COURSE_NAME_CD,COURSE_NAME";
            this.COURSE_NAME_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_COURSE_NAME;
            this.COURSE_NAME_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.COURSE_NAME_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COURSE_NAME_CD.popupWindowSetting")));
            this.COURSE_NAME_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COURSE_NAME_CD.RegistCheckMethod")));
            this.COURSE_NAME_CD.SetFormField = "COURSE_NAME_CD,COURSE_NAME";
            this.COURSE_NAME_CD.Size = new System.Drawing.Size(60, 20);
            this.COURSE_NAME_CD.TabIndex = 5;
            this.COURSE_NAME_CD.Tag = "コースを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.COURSE_NAME_CD.ZeroPaddengFlag = true;
            this.COURSE_NAME_CD.Enter += new System.EventHandler(this.COURSE_NAME_CD_Enter);
            this.COURSE_NAME_CD.Validated += new System.EventHandler(this.COURSE_NAME_CD_Validated);
            // 
            // COURSE_NAME
            // 
            this.COURSE_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.COURSE_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.COURSE_NAME.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.COURSE_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.COURSE_NAME.DisplayPopUp = null;
            this.COURSE_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COURSE_NAME.FocusOutCheckMethod")));
            this.COURSE_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.COURSE_NAME.ForeColor = System.Drawing.Color.Black;
            this.COURSE_NAME.ImeMode = System.Windows.Forms.ImeMode.On;
            this.COURSE_NAME.IsInputErrorOccured = false;
            this.COURSE_NAME.Location = new System.Drawing.Point(152, 22);
            this.COURSE_NAME.MaxLength = 40;
            this.COURSE_NAME.Name = "COURSE_NAME";
            this.COURSE_NAME.PopupAfterExecute = null;
            this.COURSE_NAME.PopupBeforeExecute = null;
            this.COURSE_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COURSE_NAME.PopupSearchSendParams")));
            this.COURSE_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COURSE_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COURSE_NAME.popupWindowSetting")));
            this.COURSE_NAME.ReadOnly = true;
            this.COURSE_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COURSE_NAME.RegistCheckMethod")));
            this.COURSE_NAME.Size = new System.Drawing.Size(290, 20);
            this.COURSE_NAME.TabIndex = 6;
            this.COURSE_NAME.TabStop = false;
            this.COURSE_NAME.Tag = "全角40文字以内で入力してください";
            // 
            // LBL_COURSE_NAME
            // 
            this.LBL_COURSE_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_COURSE_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_COURSE_NAME.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_COURSE_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LBL_COURSE_NAME.ForeColor = System.Drawing.Color.White;
            this.LBL_COURSE_NAME.Location = new System.Drawing.Point(3, 22);
            this.LBL_COURSE_NAME.Name = "LBL_COURSE_NAME";
            this.LBL_COURSE_NAME.Size = new System.Drawing.Size(91, 20);
            this.LBL_COURSE_NAME.TabIndex = 4;
            this.LBL_COURSE_NAME.Text = "コース";
            this.LBL_COURSE_NAME.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_GYOUSHA
            // 
            this.LBL_GYOUSHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_GYOUSHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_GYOUSHA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_GYOUSHA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LBL_GYOUSHA.ForeColor = System.Drawing.Color.White;
            this.LBL_GYOUSHA.Location = new System.Drawing.Point(3, 44);
            this.LBL_GYOUSHA.Name = "LBL_GYOUSHA";
            this.LBL_GYOUSHA.Size = new System.Drawing.Size(91, 20);
            this.LBL_GYOUSHA.TabIndex = 8;
            this.LBL_GYOUSHA.Text = "業者";
            this.LBL_GYOUSHA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_POPUP
            // 
            this.GYOUSHA_POPUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GYOUSHA_POPUP.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GYOUSHA_POPUP.DBFieldsName = null;
            this.GYOUSHA_POPUP.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_POPUP.DisplayItemName = null;
            this.GYOUSHA_POPUP.DisplayPopUp = null;
            this.GYOUSHA_POPUP.ErrorMessage = null;
            this.GYOUSHA_POPUP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_POPUP.FocusOutCheckMethod")));
            this.GYOUSHA_POPUP.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.GYOUSHA_POPUP.GetCodeMasterField = null;
            this.GYOUSHA_POPUP.Image = ((System.Drawing.Image)(resources.GetObject("GYOUSHA_POPUP.Image")));
            this.GYOUSHA_POPUP.ItemDefinedTypes = null;
            this.GYOUSHA_POPUP.LinkedSettingTextBox = null;
            this.GYOUSHA_POPUP.LinkedTextBoxs = null;
            this.GYOUSHA_POPUP.Location = new System.Drawing.Point(447, 43);
            this.GYOUSHA_POPUP.Name = "GYOUSHA_POPUP";
            this.GYOUSHA_POPUP.PopupAfterExecute = null;
            this.GYOUSHA_POPUP.PopupAfterExecuteMethod = "GyoushaPopupAfter";
            this.GYOUSHA_POPUP.PopupBeforeExecute = null;
            this.GYOUSHA_POPUP.PopupBeforeExecuteMethod = "GyoushaPopupBefore";
            this.GYOUSHA_POPUP.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_POPUP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_POPUP.PopupSearchSendParams")));
            this.GYOUSHA_POPUP.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GYOUSHA_POPUP.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA_ALL;
            this.GYOUSHA_POPUP.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_POPUP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_POPUP.popupWindowSetting")));
            this.GYOUSHA_POPUP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_POPUP.RegistCheckMethod")));
            this.GYOUSHA_POPUP.SearchDisplayFlag = 0;
            this.GYOUSHA_POPUP.SetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GYOUSHA_POPUP.ShortItemName = null;
            this.GYOUSHA_POPUP.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_POPUP.TabIndex = 11;
            this.GYOUSHA_POPUP.TabStop = false;
            this.GYOUSHA_POPUP.Tag = "業者の検索を行います";
            this.GYOUSHA_POPUP.Text = " ";
            this.GYOUSHA_POPUP.UseVisualStyleBackColor = false;
            this.GYOUSHA_POPUP.ZeroPaddengFlag = false;
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
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.Location = new System.Drawing.Point(93, 44);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GyoushaPopupAfter";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecuteMethod = "GyoushaPopupBefore";
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSendParams = new string[0];
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA_ALL;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(60, 20);
            this.GYOUSHA_CD.TabIndex = 9;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Enter += new System.EventHandler(this.GYOUSHA_CD_Enter);
            this.GYOUSHA_CD.Validated += new System.EventHandler(this.GYOUSHA_CD_Validated);
            // 
            // GYOUSHA_NAME
            // 
            this.GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME.DisplayPopUp = null;
            this.GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.FocusOutCheckMethod")));
            this.GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.On;
            this.GYOUSHA_NAME.IsInputErrorOccured = false;
            this.GYOUSHA_NAME.Location = new System.Drawing.Point(152, 44);
            this.GYOUSHA_NAME.MaxLength = 40;
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupAfterExecute = null;
            this.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.Size = new System.Drawing.Size(290, 20);
            this.GYOUSHA_NAME.TabIndex = 10;
            this.GYOUSHA_NAME.TabStop = false;
            this.GYOUSHA_NAME.Tag = "全角40文字以内で入力してください";
            // 
            // LBL_GENBA
            // 
            this.LBL_GENBA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_GENBA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_GENBA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_GENBA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LBL_GENBA.ForeColor = System.Drawing.Color.White;
            this.LBL_GENBA.Location = new System.Drawing.Point(3, 66);
            this.LBL_GENBA.Name = "LBL_GENBA";
            this.LBL_GENBA.Size = new System.Drawing.Size(91, 20);
            this.LBL_GENBA.TabIndex = 12;
            this.LBL_GENBA.Text = "現場";
            this.LBL_GENBA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_POPUP
            // 
            this.GENBA_POPUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GENBA_POPUP.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GENBA_POPUP.DBFieldsName = null;
            this.GENBA_POPUP.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_POPUP.DisplayItemName = null;
            this.GENBA_POPUP.DisplayPopUp = null;
            this.GENBA_POPUP.ErrorMessage = null;
            this.GENBA_POPUP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_POPUP.FocusOutCheckMethod")));
            this.GENBA_POPUP.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.GENBA_POPUP.GetCodeMasterField = null;
            this.GENBA_POPUP.Image = ((System.Drawing.Image)(resources.GetObject("GENBA_POPUP.Image")));
            this.GENBA_POPUP.ItemDefinedTypes = null;
            this.GENBA_POPUP.LinkedSettingTextBox = null;
            this.GENBA_POPUP.LinkedTextBoxs = null;
            this.GENBA_POPUP.Location = new System.Drawing.Point(447, 65);
            this.GENBA_POPUP.Name = "GENBA_POPUP";
            this.GENBA_POPUP.PopupAfterExecute = null;
            this.GENBA_POPUP.PopupAfterExecuteMethod = "";
            this.GENBA_POPUP.PopupBeforeExecute = null;
            this.GENBA_POPUP.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GENBA_POPUP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_POPUP.PopupSearchSendParams")));
            this.GENBA_POPUP.PopupSetFormField = "GENBA_CD,GENBA_NAME,GYOUSHA_CD,GYOUSHA_NAME";
            this.GENBA_POPUP.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA_ALL;
            this.GENBA_POPUP.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_POPUP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_POPUP.popupWindowSetting")));
            this.GENBA_POPUP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_POPUP.RegistCheckMethod")));
            this.GENBA_POPUP.SearchDisplayFlag = 0;
            this.GENBA_POPUP.SetFormField = "GENBA_CD,GENBA_NAME,GYOUSHA_CD,GYOUSHA_NAME";
            this.GENBA_POPUP.ShortItemName = null;
            this.GENBA_POPUP.Size = new System.Drawing.Size(22, 22);
            this.GENBA_POPUP.TabIndex = 15;
            this.GENBA_POPUP.TabStop = false;
            this.GENBA_POPUP.Tag = "現場の検索を行います";
            this.GENBA_POPUP.Text = " ";
            this.GENBA_POPUP.UseVisualStyleBackColor = false;
            this.GENBA_POPUP.ZeroPaddengFlag = false;
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
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.Location = new System.Drawing.Point(93, 66);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSendParams = new string[0];
            this.GENBA_CD.PopupSetFormField = "GENBA_CD,GENBA_NAME,GYOUSHA_CD,GYOUSHA_NAME";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA_ALL;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD,GENBA_NAME,GYOUSHA_CD,GYOUSHA_NAME";
            this.GENBA_CD.Size = new System.Drawing.Size(60, 20);
            this.GENBA_CD.TabIndex = 13;
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Enter += new System.EventHandler(this.GENBA_CD_Enter);
            this.GENBA_CD.Validated += new System.EventHandler(this.GENBA_CD_Validated);
            // 
            // GENBA_NAME
            // 
            this.GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME.DisplayPopUp = null;
            this.GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.FocusOutCheckMethod")));
            this.GENBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME.ImeMode = System.Windows.Forms.ImeMode.On;
            this.GENBA_NAME.IsInputErrorOccured = false;
            this.GENBA_NAME.Location = new System.Drawing.Point(152, 66);
            this.GENBA_NAME.MaxLength = 40;
            this.GENBA_NAME.Name = "GENBA_NAME";
            this.GENBA_NAME.PopupAfterExecute = null;
            this.GENBA_NAME.PopupBeforeExecute = null;
            this.GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME.PopupSearchSendParams")));
            this.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME.popupWindowSetting")));
            this.GENBA_NAME.ReadOnly = true;
            this.GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.RegistCheckMethod")));
            this.GENBA_NAME.Size = new System.Drawing.Size(290, 20);
            this.GENBA_NAME.TabIndex = 14;
            this.GENBA_NAME.TabStop = false;
            this.GENBA_NAME.Tag = "全角40文字以内で入力してください";
            // 
            // LBL_HINMEI
            // 
            this.LBL_HINMEI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_HINMEI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_HINMEI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_HINMEI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LBL_HINMEI.ForeColor = System.Drawing.Color.White;
            this.LBL_HINMEI.Location = new System.Drawing.Point(3, 88);
            this.LBL_HINMEI.Name = "LBL_HINMEI";
            this.LBL_HINMEI.Size = new System.Drawing.Size(91, 20);
            this.LBL_HINMEI.TabIndex = 16;
            this.LBL_HINMEI.Text = "回収品名";
            this.LBL_HINMEI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HINMEI_POPUP
            // 
            this.HINMEI_POPUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.HINMEI_POPUP.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.HINMEI_POPUP.DBFieldsName = null;
            this.HINMEI_POPUP.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_POPUP.DisplayItemName = null;
            this.HINMEI_POPUP.DisplayPopUp = null;
            this.HINMEI_POPUP.ErrorMessage = null;
            this.HINMEI_POPUP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_POPUP.FocusOutCheckMethod")));
            this.HINMEI_POPUP.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.HINMEI_POPUP.GetCodeMasterField = null;
            this.HINMEI_POPUP.Image = ((System.Drawing.Image)(resources.GetObject("HINMEI_POPUP.Image")));
            this.HINMEI_POPUP.ItemDefinedTypes = null;
            this.HINMEI_POPUP.LinkedSettingTextBox = null;
            this.HINMEI_POPUP.LinkedTextBoxs = null;
            this.HINMEI_POPUP.Location = new System.Drawing.Point(447, 87);
            this.HINMEI_POPUP.Name = "HINMEI_POPUP";
            this.HINMEI_POPUP.PopupAfterExecute = null;
            this.HINMEI_POPUP.PopupAfterExecuteMethod = "";
            this.HINMEI_POPUP.PopupBeforeExecute = null;
            this.HINMEI_POPUP.PopupGetMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_POPUP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_POPUP.PopupSearchSendParams")));
            this.HINMEI_POPUP.PopupSetFormField = "HINMEI_CD,HINMEI_NAME";
            this.HINMEI_POPUP.PopupWindowId = r_framework.Const.WINDOW_ID.M_HINMEI;
            this.HINMEI_POPUP.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.HINMEI_POPUP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_POPUP.popupWindowSetting")));
            this.HINMEI_POPUP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_POPUP.RegistCheckMethod")));
            this.HINMEI_POPUP.SearchDisplayFlag = 0;
            this.HINMEI_POPUP.SetFormField = "HINMEI_CD,HINMEI_NAME";
            this.HINMEI_POPUP.ShortItemName = null;
            this.HINMEI_POPUP.Size = new System.Drawing.Size(22, 22);
            this.HINMEI_POPUP.TabIndex = 19;
            this.HINMEI_POPUP.TabStop = false;
            this.HINMEI_POPUP.Tag = "品名の検索を行います";
            this.HINMEI_POPUP.Text = " ";
            this.HINMEI_POPUP.UseVisualStyleBackColor = false;
            this.HINMEI_POPUP.ZeroPaddengFlag = false;
            // 
            // HINMEI_CD
            // 
            this.HINMEI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HINMEI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_CD.ChangeUpperCase = true;
            this.HINMEI_CD.CharacterLimitList = null;
            this.HINMEI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_CD.DisplayPopUp = null;
            this.HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD.FocusOutCheckMethod")));
            this.HINMEI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HINMEI_CD.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_CD.GetCodeMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HINMEI_CD.IsInputErrorOccured = false;
            this.HINMEI_CD.Location = new System.Drawing.Point(93, 88);
            this.HINMEI_CD.MaxLength = 6;
            this.HINMEI_CD.Name = "HINMEI_CD";
            this.HINMEI_CD.PopupAfterExecute = null;
            this.HINMEI_CD.PopupAfterExecuteMethod = "";
            this.HINMEI_CD.PopupBeforeExecute = null;
            this.HINMEI_CD.PopupGetMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_CD.PopupSearchSendParams")));
            this.HINMEI_CD.PopupSendParams = new string[0];
            this.HINMEI_CD.PopupSetFormField = "HINMEI_CD,HINMEI_NAME";
            this.HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_HINMEI;
            this.HINMEI_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_CD.popupWindowSetting")));
            this.HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD.RegistCheckMethod")));
            this.HINMEI_CD.SetFormField = "HINMEI_CD,HINMEI_NAME";
            this.HINMEI_CD.Size = new System.Drawing.Size(60, 20);
            this.HINMEI_CD.TabIndex = 17;
            this.HINMEI_CD.Tag = "品名を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HINMEI_CD.ZeroPaddengFlag = true;
            this.HINMEI_CD.Enter += new System.EventHandler(this.HINMEI_CD_Enter);
            this.HINMEI_CD.Validated += new System.EventHandler(this.HINMEI_CD_Validated);
            // 
            // HINMEI_NAME
            // 
            this.HINMEI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HINMEI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_NAME.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.HINMEI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_NAME.DisplayPopUp = null;
            this.HINMEI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME.FocusOutCheckMethod")));
            this.HINMEI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HINMEI_NAME.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_NAME.ImeMode = System.Windows.Forms.ImeMode.On;
            this.HINMEI_NAME.IsInputErrorOccured = false;
            this.HINMEI_NAME.Location = new System.Drawing.Point(152, 88);
            this.HINMEI_NAME.MaxLength = 40;
            this.HINMEI_NAME.Name = "HINMEI_NAME";
            this.HINMEI_NAME.PopupAfterExecute = null;
            this.HINMEI_NAME.PopupBeforeExecute = null;
            this.HINMEI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_NAME.PopupSearchSendParams")));
            this.HINMEI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HINMEI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_NAME.popupWindowSetting")));
            this.HINMEI_NAME.ReadOnly = true;
            this.HINMEI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME.RegistCheckMethod")));
            this.HINMEI_NAME.Size = new System.Drawing.Size(290, 20);
            this.HINMEI_NAME.TabIndex = 18;
            this.HINMEI_NAME.TabStop = false;
            this.HINMEI_NAME.Tag = "全角40文字以内で入力してください";
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.DAY_CD_8);
            this.customPanel2.Controls.Add(this.DAY_CD_7);
            this.customPanel2.Controls.Add(this.DAY_CD_6);
            this.customPanel2.Controls.Add(this.DAY_CD_5);
            this.customPanel2.Controls.Add(this.DAY_CD_4);
            this.customPanel2.Controls.Add(this.DAY_CD_3);
            this.customPanel2.Controls.Add(this.DAY_CD_2);
            this.customPanel2.Controls.Add(this.DAY_CD_1);
            this.customPanel2.Location = new System.Drawing.Point(117, 0);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(440, 20);
            this.customPanel2.TabIndex = 126;
            // 
            // DAY_CD_8
            // 
            this.DAY_CD_8.AutoSize = true;
            this.DAY_CD_8.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_CD_8.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_8.FocusOutCheckMethod")));
            this.DAY_CD_8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DAY_CD_8.LinkedTextBox = "DAY_CD";
            this.DAY_CD_8.Location = new System.Drawing.Point(375, 0);
            this.DAY_CD_8.Name = "DAY_CD_8";
            this.DAY_CD_8.PopupAfterExecute = null;
            this.DAY_CD_8.PopupBeforeExecute = null;
            this.DAY_CD_8.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_CD_8.PopupSearchSendParams")));
            this.DAY_CD_8.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_CD_8.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAY_CD_8.popupWindowSetting")));
            this.DAY_CD_8.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_8.RegistCheckMethod")));
            this.DAY_CD_8.Size = new System.Drawing.Size(67, 17);
            this.DAY_CD_8.TabIndex = 16;
            this.DAY_CD_8.Tag = "曜日を選択します";
            this.DAY_CD_8.Text = "8.全て";
            this.DAY_CD_8.UseVisualStyleBackColor = true;
            this.DAY_CD_8.Value = "8";
            // 
            // DAY_CD_7
            // 
            this.DAY_CD_7.AutoSize = true;
            this.DAY_CD_7.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_CD_7.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_7.FocusOutCheckMethod")));
            this.DAY_CD_7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DAY_CD_7.LinkedTextBox = "DAY_CD";
            this.DAY_CD_7.Location = new System.Drawing.Point(322, 0);
            this.DAY_CD_7.Name = "DAY_CD_7";
            this.DAY_CD_7.PopupAfterExecute = null;
            this.DAY_CD_7.PopupBeforeExecute = null;
            this.DAY_CD_7.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_CD_7.PopupSearchSendParams")));
            this.DAY_CD_7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_CD_7.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAY_CD_7.popupWindowSetting")));
            this.DAY_CD_7.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_7.RegistCheckMethod")));
            this.DAY_CD_7.Size = new System.Drawing.Size(53, 17);
            this.DAY_CD_7.TabIndex = 5;
            this.DAY_CD_7.Tag = "曜日を選択します";
            this.DAY_CD_7.Text = "7.日";
            this.DAY_CD_7.UseVisualStyleBackColor = true;
            this.DAY_CD_7.Value = "7";
            // 
            // DAY_CD_6
            // 
            this.DAY_CD_6.AutoSize = true;
            this.DAY_CD_6.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_CD_6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_6.FocusOutCheckMethod")));
            this.DAY_CD_6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DAY_CD_6.LinkedTextBox = "DAY_CD";
            this.DAY_CD_6.Location = new System.Drawing.Point(269, 0);
            this.DAY_CD_6.Name = "DAY_CD_6";
            this.DAY_CD_6.PopupAfterExecute = null;
            this.DAY_CD_6.PopupBeforeExecute = null;
            this.DAY_CD_6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_CD_6.PopupSearchSendParams")));
            this.DAY_CD_6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_CD_6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAY_CD_6.popupWindowSetting")));
            this.DAY_CD_6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_6.RegistCheckMethod")));
            this.DAY_CD_6.Size = new System.Drawing.Size(53, 17);
            this.DAY_CD_6.TabIndex = 15;
            this.DAY_CD_6.Tag = "曜日を選択します";
            this.DAY_CD_6.Text = "6.土";
            this.DAY_CD_6.UseVisualStyleBackColor = true;
            this.DAY_CD_6.Value = "6";
            // 
            // DAY_CD_5
            // 
            this.DAY_CD_5.AutoSize = true;
            this.DAY_CD_5.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_CD_5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_5.FocusOutCheckMethod")));
            this.DAY_CD_5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DAY_CD_5.LinkedTextBox = "DAY_CD";
            this.DAY_CD_5.Location = new System.Drawing.Point(216, 0);
            this.DAY_CD_5.Name = "DAY_CD_5";
            this.DAY_CD_5.PopupAfterExecute = null;
            this.DAY_CD_5.PopupBeforeExecute = null;
            this.DAY_CD_5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_CD_5.PopupSearchSendParams")));
            this.DAY_CD_5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_CD_5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAY_CD_5.popupWindowSetting")));
            this.DAY_CD_5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_5.RegistCheckMethod")));
            this.DAY_CD_5.Size = new System.Drawing.Size(53, 17);
            this.DAY_CD_5.TabIndex = 14;
            this.DAY_CD_5.Tag = "曜日を選択します";
            this.DAY_CD_5.Text = "5.金";
            this.DAY_CD_5.UseVisualStyleBackColor = true;
            this.DAY_CD_5.Value = "5";
            // 
            // DAY_CD_4
            // 
            this.DAY_CD_4.AutoSize = true;
            this.DAY_CD_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_CD_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_4.FocusOutCheckMethod")));
            this.DAY_CD_4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DAY_CD_4.LinkedTextBox = "DAY_CD";
            this.DAY_CD_4.Location = new System.Drawing.Point(163, 0);
            this.DAY_CD_4.Name = "DAY_CD_4";
            this.DAY_CD_4.PopupAfterExecute = null;
            this.DAY_CD_4.PopupBeforeExecute = null;
            this.DAY_CD_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_CD_4.PopupSearchSendParams")));
            this.DAY_CD_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_CD_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAY_CD_4.popupWindowSetting")));
            this.DAY_CD_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_4.RegistCheckMethod")));
            this.DAY_CD_4.Size = new System.Drawing.Size(53, 17);
            this.DAY_CD_4.TabIndex = 13;
            this.DAY_CD_4.Tag = "曜日を選択します";
            this.DAY_CD_4.Text = "4.木";
            this.DAY_CD_4.UseVisualStyleBackColor = true;
            this.DAY_CD_4.Value = "4";
            // 
            // DAY_CD_3
            // 
            this.DAY_CD_3.AutoSize = true;
            this.DAY_CD_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_CD_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_3.FocusOutCheckMethod")));
            this.DAY_CD_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DAY_CD_3.LinkedTextBox = "DAY_CD";
            this.DAY_CD_3.Location = new System.Drawing.Point(110, 0);
            this.DAY_CD_3.Name = "DAY_CD_3";
            this.DAY_CD_3.PopupAfterExecute = null;
            this.DAY_CD_3.PopupBeforeExecute = null;
            this.DAY_CD_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_CD_3.PopupSearchSendParams")));
            this.DAY_CD_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_CD_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAY_CD_3.popupWindowSetting")));
            this.DAY_CD_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_3.RegistCheckMethod")));
            this.DAY_CD_3.Size = new System.Drawing.Size(53, 17);
            this.DAY_CD_3.TabIndex = 12;
            this.DAY_CD_3.Tag = "曜日を選択します";
            this.DAY_CD_3.Text = "3.水";
            this.DAY_CD_3.UseVisualStyleBackColor = true;
            this.DAY_CD_3.Value = "3";
            // 
            // DAY_CD_2
            // 
            this.DAY_CD_2.AutoSize = true;
            this.DAY_CD_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_CD_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_2.FocusOutCheckMethod")));
            this.DAY_CD_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DAY_CD_2.LinkedTextBox = "DAY_CD";
            this.DAY_CD_2.Location = new System.Drawing.Point(57, 0);
            this.DAY_CD_2.Name = "DAY_CD_2";
            this.DAY_CD_2.PopupAfterExecute = null;
            this.DAY_CD_2.PopupBeforeExecute = null;
            this.DAY_CD_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_CD_2.PopupSearchSendParams")));
            this.DAY_CD_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_CD_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAY_CD_2.popupWindowSetting")));
            this.DAY_CD_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_2.RegistCheckMethod")));
            this.DAY_CD_2.Size = new System.Drawing.Size(53, 17);
            this.DAY_CD_2.TabIndex = 11;
            this.DAY_CD_2.Tag = "曜日を選択します";
            this.DAY_CD_2.Text = "2.火";
            this.DAY_CD_2.UseVisualStyleBackColor = true;
            this.DAY_CD_2.Value = "2";
            // 
            // DAY_CD_1
            // 
            this.DAY_CD_1.AutoSize = true;
            this.DAY_CD_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_CD_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_1.FocusOutCheckMethod")));
            this.DAY_CD_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DAY_CD_1.LinkedTextBox = "DAY_CD";
            this.DAY_CD_1.Location = new System.Drawing.Point(4, 0);
            this.DAY_CD_1.Name = "DAY_CD_1";
            this.DAY_CD_1.PopupAfterExecute = null;
            this.DAY_CD_1.PopupBeforeExecute = null;
            this.DAY_CD_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_CD_1.PopupSearchSendParams")));
            this.DAY_CD_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_CD_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAY_CD_1.popupWindowSetting")));
            this.DAY_CD_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD_1.RegistCheckMethod")));
            this.DAY_CD_1.Size = new System.Drawing.Size(53, 17);
            this.DAY_CD_1.TabIndex = 10;
            this.DAY_CD_1.Tag = "曜日を選択します";
            this.DAY_CD_1.Text = "1.月";
            this.DAY_CD_1.UseVisualStyleBackColor = true;
            this.DAY_CD_1.Value = "1";
            // 
            // DAY_CD
            // 
            this.DAY_CD.BackColor = System.Drawing.SystemColors.Window;
            this.DAY_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DAY_CD.ChangeUpperCase = true;
            this.DAY_CD.CharacterLimitList = new char[] {
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8'};
            this.DAY_CD.DBFieldsName = "";
            this.DAY_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_CD.DisplayItemName = "曜日";
            this.DAY_CD.DisplayPopUp = null;
            this.DAY_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD.FocusOutCheckMethod")));
            this.DAY_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DAY_CD.ForeColor = System.Drawing.Color.Black;
            this.DAY_CD.IsInputErrorOccured = false;
            this.DAY_CD.ItemDefinedTypes = "";
            this.DAY_CD.LinkedRadioButtonArray = new string[] {
        "DAY_CD_1",
        "DAY_CD_2",
        "DAY_CD_3",
        "DAY_CD_4",
        "DAY_CD_5",
        "DAY_CD_6",
        "DAY_CD_7",
        "DAY_CD_8"};
            this.DAY_CD.Location = new System.Drawing.Point(93, 0);
            this.DAY_CD.Name = "DAY_CD";
            this.DAY_CD.PopupAfterExecute = null;
            this.DAY_CD.PopupBeforeExecute = null;
            this.DAY_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_CD.PopupSearchSendParams")));
            this.DAY_CD.PopupSetFormField = "";
            this.DAY_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_CD.PopupWindowName = "";
            this.DAY_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAY_CD.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            8,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DAY_CD.RangeSetting = rangeSettingDto2;
            this.DAY_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAY_CD.RegistCheckMethod")));
            this.DAY_CD.SetFormField = "";
            this.DAY_CD.ShortItemName = "検索条件";
            this.DAY_CD.Size = new System.Drawing.Size(25, 20);
            this.DAY_CD.TabIndex = 125;
            this.DAY_CD.Tag = "【1～8】のいずれかで入力してください";
            this.DAY_CD.WordWrap = false;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(218, 113);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(84, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 22;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(164, 113);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(48, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 21;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOU
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(98, 113);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOU";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(60, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.TabIndex = 20;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 111);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 20);
            this.label1.TabIndex = 420;
            this.label1.Text = "表示現場";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.MidnightBlue;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(567, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 421;
            this.label2.Text = "コース";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // COURSE_NAME_CD_FUKUSHA
            // 
            this.COURSE_NAME_CD_FUKUSHA.BackColor = System.Drawing.SystemColors.Window;
            this.COURSE_NAME_CD_FUKUSHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.COURSE_NAME_CD_FUKUSHA.ChangeUpperCase = true;
            this.COURSE_NAME_CD_FUKUSHA.CharacterLimitList = null;
            this.COURSE_NAME_CD_FUKUSHA.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.COURSE_NAME_CD_FUKUSHA.DefaultBackColor = System.Drawing.Color.Empty;
            this.COURSE_NAME_CD_FUKUSHA.DisplayPopUp = null;
            this.COURSE_NAME_CD_FUKUSHA.FocusOutCheckMethod = null;
            this.COURSE_NAME_CD_FUKUSHA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.COURSE_NAME_CD_FUKUSHA.ForeColor = System.Drawing.Color.Black;
            this.COURSE_NAME_CD_FUKUSHA.GetCodeMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU";
            this.COURSE_NAME_CD_FUKUSHA.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.COURSE_NAME_CD_FUKUSHA.IsInputErrorOccured = false;
            this.COURSE_NAME_CD_FUKUSHA.Location = new System.Drawing.Point(657, 42);
            this.COURSE_NAME_CD_FUKUSHA.MaxLength = 6;
            this.COURSE_NAME_CD_FUKUSHA.Name = "COURSE_NAME_CD_FUKUSHA";
            this.COURSE_NAME_CD_FUKUSHA.PopupAfterExecute = null;
            this.COURSE_NAME_CD_FUKUSHA.PopupAfterExecuteMethod = "";
            this.COURSE_NAME_CD_FUKUSHA.PopupBeforeExecute = null;
            this.COURSE_NAME_CD_FUKUSHA.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU";
            this.COURSE_NAME_CD_FUKUSHA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COURSE_NAME_CD_FUKUSHA.PopupSearchSendParams")));
            this.COURSE_NAME_CD_FUKUSHA.PopupSendParams = new string[0];
            this.COURSE_NAME_CD_FUKUSHA.PopupSetFormField = "COURSE_NAME_CD_FUKUSHA,COURSE_NAME_FUKUSHA";
            this.COURSE_NAME_CD_FUKUSHA.PopupWindowId = r_framework.Const.WINDOW_ID.M_COURSE_NAME;
            this.COURSE_NAME_CD_FUKUSHA.PopupWindowName = "マスタ共通ポップアップ";
            this.COURSE_NAME_CD_FUKUSHA.popupWindowSetting = null;
            this.COURSE_NAME_CD_FUKUSHA.RegistCheckMethod = null;
            this.COURSE_NAME_CD_FUKUSHA.SetFormField = "COURSE_NAME_CD_FUKUSHA,COURSE_NAME_FUKUSHA";
            this.COURSE_NAME_CD_FUKUSHA.Size = new System.Drawing.Size(60, 20);
            this.COURSE_NAME_CD_FUKUSHA.TabIndex = 24;
            this.COURSE_NAME_CD_FUKUSHA.Tag = "コースを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.COURSE_NAME_CD_FUKUSHA.ZeroPaddengFlag = true;
            this.COURSE_NAME_CD_FUKUSHA.Enter += new System.EventHandler(this.COURSE_NAME_CD_FUKUSHA_Enter);
            this.COURSE_NAME_CD_FUKUSHA.Validated += new System.EventHandler(this.COURSE_NAME_CD_FUKUSHA_Validated);
            // 
            // COURSE_NAME_FUKUSHA
            // 
            this.COURSE_NAME_FUKUSHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.COURSE_NAME_FUKUSHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.COURSE_NAME_FUKUSHA.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.COURSE_NAME_FUKUSHA.DefaultBackColor = System.Drawing.Color.Empty;
            this.COURSE_NAME_FUKUSHA.DisplayPopUp = null;
            this.COURSE_NAME_FUKUSHA.FocusOutCheckMethod = null;
            this.COURSE_NAME_FUKUSHA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.COURSE_NAME_FUKUSHA.ForeColor = System.Drawing.Color.Black;
            this.COURSE_NAME_FUKUSHA.ImeMode = System.Windows.Forms.ImeMode.On;
            this.COURSE_NAME_FUKUSHA.IsInputErrorOccured = false;
            this.COURSE_NAME_FUKUSHA.Location = new System.Drawing.Point(716, 42);
            this.COURSE_NAME_FUKUSHA.MaxLength = 40;
            this.COURSE_NAME_FUKUSHA.Name = "COURSE_NAME_FUKUSHA";
            this.COURSE_NAME_FUKUSHA.PopupAfterExecute = null;
            this.COURSE_NAME_FUKUSHA.PopupBeforeExecute = null;
            this.COURSE_NAME_FUKUSHA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COURSE_NAME_FUKUSHA.PopupSearchSendParams")));
            this.COURSE_NAME_FUKUSHA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COURSE_NAME_FUKUSHA.popupWindowSetting = null;
            this.COURSE_NAME_FUKUSHA.ReadOnly = true;
            this.COURSE_NAME_FUKUSHA.RegistCheckMethod = null;
            this.COURSE_NAME_FUKUSHA.Size = new System.Drawing.Size(133, 20);
            this.COURSE_NAME_FUKUSHA.TabIndex = 423;
            this.COURSE_NAME_FUKUSHA.TabStop = false;
            this.COURSE_NAME_FUKUSHA.Tag = "全角40文字以内で入力してください";
            // 
            // DAY_CD_FUKUSHA
            // 
            this.DAY_CD_FUKUSHA.BackColor = System.Drawing.SystemColors.Window;
            this.DAY_CD_FUKUSHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DAY_CD_FUKUSHA.ChangeUpperCase = true;
            this.DAY_CD_FUKUSHA.CharacterLimitList = new char[] {
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7'};
            this.DAY_CD_FUKUSHA.DBFieldsName = "";
            this.DAY_CD_FUKUSHA.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_CD_FUKUSHA.DisplayItemName = "曜日";
            this.DAY_CD_FUKUSHA.DisplayPopUp = null;
            this.DAY_CD_FUKUSHA.FocusOutCheckMethod = null;
            this.DAY_CD_FUKUSHA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DAY_CD_FUKUSHA.ForeColor = System.Drawing.Color.Black;
            this.DAY_CD_FUKUSHA.IsInputErrorOccured = false;
            this.DAY_CD_FUKUSHA.ItemDefinedTypes = "";
            this.DAY_CD_FUKUSHA.Location = new System.Drawing.Point(657, 20);
            this.DAY_CD_FUKUSHA.Name = "DAY_CD_FUKUSHA";
            this.DAY_CD_FUKUSHA.PopupAfterExecute = null;
            this.DAY_CD_FUKUSHA.PopupBeforeExecute = null;
            this.DAY_CD_FUKUSHA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_CD_FUKUSHA.PopupSearchSendParams")));
            this.DAY_CD_FUKUSHA.PopupSetFormField = "";
            this.DAY_CD_FUKUSHA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_CD_FUKUSHA.PopupWindowName = "";
            this.DAY_CD_FUKUSHA.popupWindowSetting = null;
            rangeSettingDto3.Max = new decimal(new int[] {
            7,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DAY_CD_FUKUSHA.RangeSetting = rangeSettingDto3;
            this.DAY_CD_FUKUSHA.RegistCheckMethod = null;
            this.DAY_CD_FUKUSHA.SetFormField = "";
            this.DAY_CD_FUKUSHA.ShortItemName = "";
            this.DAY_CD_FUKUSHA.Size = new System.Drawing.Size(34, 20);
            this.DAY_CD_FUKUSHA.TabIndex = 23;
            this.DAY_CD_FUKUSHA.Tag = "曜日を選択してください【1．月、2．火、3．水、4．木、5．金、6．土、7．日】";
            this.DAY_CD_FUKUSHA.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DAY_CD_FUKUSHA.WordWrap = false;
            this.DAY_CD_FUKUSHA.TextChanged += new System.EventHandler(this.DAY_CD_FUKUSHA_TextChanged);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.MidnightBlue;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(567, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 20);
            this.label3.TabIndex = 424;
            this.label3.Text = "曜日※";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.MidnightBlue;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(567, -2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 20);
            this.label4.TabIndex = 426;
            this.label4.Text = "複写先";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DAY_NAME_FUKUSHA
            // 
            this.DAY_NAME_FUKUSHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.DAY_NAME_FUKUSHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DAY_NAME_FUKUSHA.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.DAY_NAME_FUKUSHA.DefaultBackColor = System.Drawing.Color.Empty;
            this.DAY_NAME_FUKUSHA.DisplayPopUp = null;
            this.DAY_NAME_FUKUSHA.FocusOutCheckMethod = null;
            this.DAY_NAME_FUKUSHA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DAY_NAME_FUKUSHA.ForeColor = System.Drawing.Color.Black;
            this.DAY_NAME_FUKUSHA.ImeMode = System.Windows.Forms.ImeMode.On;
            this.DAY_NAME_FUKUSHA.IsInputErrorOccured = false;
            this.DAY_NAME_FUKUSHA.Location = new System.Drawing.Point(690, 20);
            this.DAY_NAME_FUKUSHA.MaxLength = 40;
            this.DAY_NAME_FUKUSHA.Name = "DAY_NAME_FUKUSHA";
            this.DAY_NAME_FUKUSHA.PopupAfterExecute = null;
            this.DAY_NAME_FUKUSHA.PopupBeforeExecute = null;
            this.DAY_NAME_FUKUSHA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAY_NAME_FUKUSHA.PopupSearchSendParams")));
            this.DAY_NAME_FUKUSHA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAY_NAME_FUKUSHA.popupWindowSetting = null;
            this.DAY_NAME_FUKUSHA.ReadOnly = true;
            this.DAY_NAME_FUKUSHA.RegistCheckMethod = null;
            this.DAY_NAME_FUKUSHA.Size = new System.Drawing.Size(43, 20);
            this.DAY_NAME_FUKUSHA.TabIndex = 427;
            this.DAY_NAME_FUKUSHA.TabStop = false;
            this.DAY_NAME_FUKUSHA.Tag = "全角40文字以内で入力してください";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1192, 553);
            this.Controls.Add(this.DAY_NAME_FUKUSHA);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DAY_CD_FUKUSHA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.COURSE_NAME_CD_FUKUSHA);
            this.Controls.Add(this.COURSE_NAME_FUKUSHA);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.customPanel2);
            this.Controls.Add(this.DAY_CD);
            this.Controls.Add(this.LBL_HINMEI);
            this.Controls.Add(this.HINMEI_POPUP);
            this.Controls.Add(this.HINMEI_CD);
            this.Controls.Add(this.HINMEI_NAME);
            this.Controls.Add(this.LBL_GENBA);
            this.Controls.Add(this.GENBA_POPUP);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GENBA_NAME);
            this.Controls.Add(this.LBL_GYOUSHA);
            this.Controls.Add(this.GYOUSHA_POPUP);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.GYOUSHA_NAME);
            this.Controls.Add(this.LBL_COURSE_NAME);
            this.Controls.Add(this.COURSE_NAME_POPUP);
            this.Controls.Add(this.COURSE_NAME_CD);
            this.Controls.Add(this.COURSE_NAME);
            this.Controls.Add(this.LBL_DAY);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.LBL_DAY, 0);
            this.Controls.SetChildIndex(this.COURSE_NAME, 0);
            this.Controls.SetChildIndex(this.COURSE_NAME_CD, 0);
            this.Controls.SetChildIndex(this.COURSE_NAME_POPUP, 0);
            this.Controls.SetChildIndex(this.LBL_COURSE_NAME, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_NAME, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_CD, 0);
            this.Controls.SetChildIndex(this.GYOUSHA_POPUP, 0);
            this.Controls.SetChildIndex(this.LBL_GYOUSHA, 0);
            this.Controls.SetChildIndex(this.GENBA_NAME, 0);
            this.Controls.SetChildIndex(this.GENBA_CD, 0);
            this.Controls.SetChildIndex(this.GENBA_POPUP, 0);
            this.Controls.SetChildIndex(this.LBL_GENBA, 0);
            this.Controls.SetChildIndex(this.HINMEI_NAME, 0);
            this.Controls.SetChildIndex(this.HINMEI_CD, 0);
            this.Controls.SetChildIndex(this.HINMEI_POPUP, 0);
            this.Controls.SetChildIndex(this.LBL_HINMEI, 0);
            this.Controls.SetChildIndex(this.DAY_CD, 0);
            this.Controls.SetChildIndex(this.customPanel2, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_DELETED, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI, 0);
            this.Controls.SetChildIndex(this.COURSE_NAME_FUKUSHA, 0);
            this.Controls.SetChildIndex(this.COURSE_NAME_CD_FUKUSHA, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.DAY_CD_FUKUSHA, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.DAY_NAME_FUKUSHA, 0);
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label LBL_DAY;
        internal r_framework.CustomControl.CustomPopupOpenButton COURSE_NAME_POPUP;
        internal r_framework.CustomControl.CustomAlphaNumTextBox COURSE_NAME_CD;
        internal r_framework.CustomControl.CustomTextBox COURSE_NAME;
        internal System.Windows.Forms.Label LBL_COURSE_NAME;
        internal System.Windows.Forms.Label LBL_GYOUSHA;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_POPUP;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME;
        internal System.Windows.Forms.Label LBL_GENBA;
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_POPUP;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME;
        internal System.Windows.Forms.Label LBL_HINMEI;
        internal r_framework.CustomControl.CustomPopupOpenButton HINMEI_POPUP;
        internal r_framework.CustomControl.CustomAlphaNumTextBox HINMEI_CD;
        internal r_framework.CustomControl.CustomTextBox HINMEI_NAME;
        internal r_framework.CustomControl.CustomPanel customPanel2;
        internal r_framework.CustomControl.CustomRadioButton DAY_CD_7;
        internal r_framework.CustomControl.CustomRadioButton DAY_CD_6;
        internal r_framework.CustomControl.CustomRadioButton DAY_CD_5;
        internal r_framework.CustomControl.CustomRadioButton DAY_CD_4;
        internal r_framework.CustomControl.CustomRadioButton DAY_CD_3;
        internal r_framework.CustomControl.CustomRadioButton DAY_CD_2;
        internal r_framework.CustomControl.CustomRadioButton DAY_CD_1;
        internal r_framework.CustomControl.CustomNumericTextBox2 DAY_CD;
        internal r_framework.CustomControl.CustomRadioButton DAY_CD_8;
        public CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
        public CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        public CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
        public Label label1;
        internal Label label2;
        internal r_framework.CustomControl.CustomAlphaNumTextBox COURSE_NAME_CD_FUKUSHA;
        internal r_framework.CustomControl.CustomTextBox COURSE_NAME_FUKUSHA;
        internal r_framework.CustomControl.CustomNumericTextBox2 DAY_CD_FUKUSHA;
        internal Label label3;
        internal Label label4;
        internal r_framework.CustomControl.CustomTextBox DAY_NAME_FUKUSHA;
    }
}