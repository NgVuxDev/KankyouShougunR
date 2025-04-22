using System.Windows.Forms;
using System;

namespace Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran
{
    partial class NyuuSyutuKinIchiranForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NyuuSyutuKinIchiranForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.label3 = new System.Windows.Forms.Label();
            this.txtNum_DenpyouSyurui = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radbtn_Syuutukin = new r_framework.CustomControl.CustomRadioButton();
            this.radbtn_Nyuukin = new r_framework.CustomControl.CustomRadioButton();
            this.TorihikiPopupButton = new r_framework.CustomControl.CustomPopupOpenButton();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.panel2 = new r_framework.CustomControl.CustomPanel();
            this.NYUUKINSAKI_LABEL = new System.Windows.Forms.Label();
            this.NYUUKINSAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.NYUUKINSAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.NyuukinPopupButton = new r_framework.CustomControl.CustomPopupOpenButton();
            this.lblBangou = new System.Windows.Forms.Label();
            this.NYUUKIN_NUMBER = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Location = new System.Drawing.Point(0, 1);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(745, 134);
            this.searchString.TabIndex = 1;
            this.searchString.Tag = "検索条件設定画面で設定した条件が表示されます";
            this.searchString.Visible = false;
            this.searchString.ZeroPaddengFlag = true;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(4, 429);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 4;
            this.bt_ptn1.Text = "パターン１";
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(204, 429);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 5;
            this.bt_ptn2.Text = "パターン２";
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 429);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 6;
            this.bt_ptn3.Text = "パターン３";
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(604, 429);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 7;
            this.bt_ptn4.Text = "パターン４";
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(804, 429);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 8;
            this.bt_ptn5.Text = "パターン５";
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(3, 89);
            this.customSortHeader1.Size = new System.Drawing.Size(997, 25);
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(3, 67);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(564, 1);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "伝票種類※";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNum_DenpyouSyurui
            // 
            this.txtNum_DenpyouSyurui.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_DenpyouSyurui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_DenpyouSyurui.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_DenpyouSyurui.DisplayPopUp = null;
            this.txtNum_DenpyouSyurui.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_DenpyouSyurui.FocusOutCheckMethod")));
            this.txtNum_DenpyouSyurui.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.txtNum_DenpyouSyurui.ForeColor = System.Drawing.Color.Black;
            this.txtNum_DenpyouSyurui.IsInputErrorOccured = false;
            this.txtNum_DenpyouSyurui.LinkedRadioButtonArray = new string[] {
        "radbtn_Nyuukin",
        "radbtn_Syuutukin"};
            this.txtNum_DenpyouSyurui.Location = new System.Drawing.Point(679, 1);
            this.txtNum_DenpyouSyurui.Name = "txtNum_DenpyouSyurui";
            this.txtNum_DenpyouSyurui.PopupAfterExecute = null;
            this.txtNum_DenpyouSyurui.PopupBeforeExecute = null;
            this.txtNum_DenpyouSyurui.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_DenpyouSyurui.PopupSearchSendParams")));
            this.txtNum_DenpyouSyurui.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_DenpyouSyurui.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_DenpyouSyurui.popupWindowSetting")));
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
            this.txtNum_DenpyouSyurui.RangeSetting = rangeSettingDto1;
            this.txtNum_DenpyouSyurui.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_DenpyouSyurui.RegistCheckMethod")));
            this.txtNum_DenpyouSyurui.Size = new System.Drawing.Size(20, 20);
            this.txtNum_DenpyouSyurui.TabIndex = 3;
            this.txtNum_DenpyouSyurui.Tag = "【1～2】のいずれかで入力してください";
            this.txtNum_DenpyouSyurui.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_DenpyouSyurui.WordWrap = false;
            this.txtNum_DenpyouSyurui.Validating += new System.ComponentModel.CancelEventHandler(this.txtNum_DenpyouSyurui_Validating);
            // 
            // radbtn_Syuutukin
            // 
            this.radbtn_Syuutukin.AutoSize = true;
            this.radbtn_Syuutukin.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Syuutukin.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Syuutukin.FocusOutCheckMethod")));
            this.radbtn_Syuutukin.LinkedTextBox = "txtNum_DenpyouSyurui";
            this.radbtn_Syuutukin.Location = new System.Drawing.Point(75, 0);
            this.radbtn_Syuutukin.Name = "radbtn_Syuutukin";
            this.radbtn_Syuutukin.PopupAfterExecute = null;
            this.radbtn_Syuutukin.PopupBeforeExecute = null;
            this.radbtn_Syuutukin.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Syuutukin.PopupSearchSendParams")));
            this.radbtn_Syuutukin.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Syuutukin.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Syuutukin.popupWindowSetting")));
            this.radbtn_Syuutukin.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Syuutukin.RegistCheckMethod")));
            this.radbtn_Syuutukin.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Syuutukin.TabIndex = 5;
            this.radbtn_Syuutukin.Tag = "伝票種類が「2.出金」の場合にはチェックを付けてください";
            this.radbtn_Syuutukin.Text = "2.出金";
            this.radbtn_Syuutukin.UseVisualStyleBackColor = true;
            this.radbtn_Syuutukin.Value = "2";
            // 
            // radbtn_Nyuukin
            // 
            this.radbtn_Nyuukin.AutoSize = true;
            this.radbtn_Nyuukin.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtn_Nyuukin.DisplayItemName = "";
            this.radbtn_Nyuukin.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Nyuukin.FocusOutCheckMethod")));
            this.radbtn_Nyuukin.LinkedTextBox = "txtNum_DenpyouSyurui";
            this.radbtn_Nyuukin.Location = new System.Drawing.Point(4, 0);
            this.radbtn_Nyuukin.Name = "radbtn_Nyuukin";
            this.radbtn_Nyuukin.PopupAfterExecute = null;
            this.radbtn_Nyuukin.PopupBeforeExecute = null;
            this.radbtn_Nyuukin.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtn_Nyuukin.PopupSearchSendParams")));
            this.radbtn_Nyuukin.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtn_Nyuukin.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtn_Nyuukin.popupWindowSetting")));
            this.radbtn_Nyuukin.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtn_Nyuukin.RegistCheckMethod")));
            this.radbtn_Nyuukin.Size = new System.Drawing.Size(67, 17);
            this.radbtn_Nyuukin.TabIndex = 4;
            this.radbtn_Nyuukin.Tag = "伝票種類が「1.入金」の場合にはチェックを付けてください";
            this.radbtn_Nyuukin.Text = "1.入金";
            this.radbtn_Nyuukin.UseVisualStyleBackColor = true;
            this.radbtn_Nyuukin.Value = "1";
            // 
            // TorihikiPopupButton
            // 
            this.TorihikiPopupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TorihikiPopupButton.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TorihikiPopupButton.DBFieldsName = null;
            this.TorihikiPopupButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.TorihikiPopupButton.DisplayItemName = "取引先CD";
            this.TorihikiPopupButton.DisplayPopUp = null;
            this.TorihikiPopupButton.ErrorMessage = null;
            this.TorihikiPopupButton.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TorihikiPopupButton.FocusOutCheckMethod")));
            this.TorihikiPopupButton.Font = new System.Drawing.Font("MS Gothic", 11.25F);
            this.TorihikiPopupButton.GetCodeMasterField = null;
            this.TorihikiPopupButton.Image = ((System.Drawing.Image)(resources.GetObject("TorihikiPopupButton.Image")));
            this.TorihikiPopupButton.ItemDefinedTypes = null;
            this.TorihikiPopupButton.LinkedSettingTextBox = null;
            this.TorihikiPopupButton.LinkedTextBoxs = null;
            this.TorihikiPopupButton.Location = new System.Drawing.Point(469, 22);
            this.TorihikiPopupButton.Name = "TorihikiPopupButton";
            this.TorihikiPopupButton.PopupAfterExecute = null;
            this.TorihikiPopupButton.PopupBeforeExecute = null;
            this.TorihikiPopupButton.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TorihikiPopupButton.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TorihikiPopupButton.PopupSearchSendParams")));
            this.TorihikiPopupButton.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TorihikiPopupButton.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TorihikiPopupButton.PopupWindowName = "検索共通ポップアップ";
            this.TorihikiPopupButton.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TorihikiPopupButton.popupWindowSetting")));
            this.TorihikiPopupButton.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TorihikiPopupButton.RegistCheckMethod")));
            this.TorihikiPopupButton.SearchDisplayFlag = 0;
            this.TorihikiPopupButton.SetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TorihikiPopupButton.ShortItemName = "取引先CD";
            this.TorihikiPopupButton.Size = new System.Drawing.Size(22, 22);
            this.TorihikiPopupButton.TabIndex = 375;
            this.TorihikiPopupButton.TabStop = false;
            this.TorihikiPopupButton.UseVisualStyleBackColor = false;
            this.TorihikiPopupButton.ZeroPaddengFlag = false;
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD.CharacterLimitList = null;
            this.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先CD";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(116, 23);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ShortItemName = "取引先CD";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(60, 20);
            this.TORIHIKISAKI_CD.TabIndex = 1;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayItemName = "";
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.ErrorMessage = "";
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.GetCodeMasterField = "";
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.ItemDefinedTypes = "";
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(175, 23);
            this.TORIHIKISAKI_NAME_RYAKU.MaxLength = 20;
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupGetMasterField = "";
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupSetFormField = "";
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.SetFormField = "";
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 377;
            this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            this.TORIHIKISAKI_NAME_RYAKU.Tag = "　";
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(3, 23);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 376;
            this.TORIHIKISAKI_LABEL.Text = "取引先";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.radbtn_Nyuukin);
            this.panel2.Controls.Add(this.radbtn_Syuutukin);
            this.panel2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.panel2.Location = new System.Drawing.Point(698, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(145, 20);
            this.panel2.TabIndex = 378;
            // 
            // NYUUKINSAKI_LABEL
            // 
            this.NYUUKINSAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.NYUUKINSAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NYUUKINSAKI_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NYUUKINSAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.NYUUKINSAKI_LABEL.Location = new System.Drawing.Point(3, 45);
            this.NYUUKINSAKI_LABEL.Name = "NYUUKINSAKI_LABEL";
            this.NYUUKINSAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.NYUUKINSAKI_LABEL.TabIndex = 379;
            this.NYUUKINSAKI_LABEL.Text = "入金先";
            this.NYUUKINSAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NYUUKINSAKI_CD
            // 
            this.NYUUKINSAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKINSAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_CD.CharacterLimitList = null;
            this.NYUUKINSAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NYUUKINSAKI_CD.DBFieldsName = "NYUUKINSAKI_CD";
            this.NYUUKINSAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_CD.DisplayItemName = "入金先CD";
            this.NYUUKINSAKI_CD.DisplayPopUp = null;
            this.NYUUKINSAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_CD.FocusOutCheckMethod")));
            this.NYUUKINSAKI_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NYUUKINSAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_CD.GetCodeMasterField = "NYUUKINSAKI_CD,NYUUKINSAKI_NAME_RYAKU";
            this.NYUUKINSAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NYUUKINSAKI_CD.IsInputErrorOccured = false;
            this.NYUUKINSAKI_CD.ItemDefinedTypes = "varchar";
            this.NYUUKINSAKI_CD.Location = new System.Drawing.Point(116, 45);
            this.NYUUKINSAKI_CD.MaxLength = 6;
            this.NYUUKINSAKI_CD.Name = "NYUUKINSAKI_CD";
            this.NYUUKINSAKI_CD.PopupAfterExecute = null;
            this.NYUUKINSAKI_CD.PopupBeforeExecute = null;
            this.NYUUKINSAKI_CD.PopupGetMasterField = "NYUUKINSAKI_CD,NYUUKINSAKI_NAME_RYAKU";
            this.NYUUKINSAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_CD.PopupSearchSendParams")));
            this.NYUUKINSAKI_CD.PopupSetFormField = "NYUUKINSAKI_CD,NYUUKINSAKI_NAME_RYAKU";
            this.NYUUKINSAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_NYUUKINSAKI;
            this.NYUUKINSAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.NYUUKINSAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_CD.popupWindowSetting")));
            this.NYUUKINSAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_CD.RegistCheckMethod")));
            this.NYUUKINSAKI_CD.SetFormField = "NYUUKINSAKI_CD,NYUUKINSAKI_NAME_RYAKU";
            this.NYUUKINSAKI_CD.ShortItemName = "入金先CD";
            this.NYUUKINSAKI_CD.Size = new System.Drawing.Size(60, 20);
            this.NYUUKINSAKI_CD.TabIndex = 380;
            this.NYUUKINSAKI_CD.Tag = "入金先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.NYUUKINSAKI_CD.ZeroPaddengFlag = true;
            // 
            // NYUUKINSAKI_NAME_RYAKU
            // 
            this.NYUUKINSAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.NYUUKINSAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKINSAKI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.NYUUKINSAKI_NAME_RYAKU.DBFieldsName = "NYUUKINSAKI_NAME_RYAKU";
            this.NYUUKINSAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKINSAKI_NAME_RYAKU.DisplayItemName = "";
            this.NYUUKINSAKI_NAME_RYAKU.DisplayPopUp = null;
            this.NYUUKINSAKI_NAME_RYAKU.ErrorMessage = "";
            this.NYUUKINSAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.NYUUKINSAKI_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NYUUKINSAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.NYUUKINSAKI_NAME_RYAKU.GetCodeMasterField = "";
            this.NYUUKINSAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.NYUUKINSAKI_NAME_RYAKU.ItemDefinedTypes = "";
            this.NYUUKINSAKI_NAME_RYAKU.Location = new System.Drawing.Point(175, 45);
            this.NYUUKINSAKI_NAME_RYAKU.MaxLength = 20;
            this.NYUUKINSAKI_NAME_RYAKU.Name = "NYUUKINSAKI_NAME_RYAKU";
            this.NYUUKINSAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.NYUUKINSAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.NYUUKINSAKI_NAME_RYAKU.PopupGetMasterField = "";
            this.NYUUKINSAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKINSAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.NYUUKINSAKI_NAME_RYAKU.PopupSetFormField = "";
            this.NYUUKINSAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKINSAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKINSAKI_NAME_RYAKU.popupWindowSetting")));
            this.NYUUKINSAKI_NAME_RYAKU.ReadOnly = true;
            this.NYUUKINSAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKINSAKI_NAME_RYAKU.RegistCheckMethod")));
            this.NYUUKINSAKI_NAME_RYAKU.SetFormField = "";
            this.NYUUKINSAKI_NAME_RYAKU.Size = new System.Drawing.Size(290, 20);
            this.NYUUKINSAKI_NAME_RYAKU.TabIndex = 381;
            this.NYUUKINSAKI_NAME_RYAKU.TabStop = false;
            this.NYUUKINSAKI_NAME_RYAKU.Tag = "　";
            // 
            // NyuukinPopupButton
            // 
            this.NyuukinPopupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.NyuukinPopupButton.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.NyuukinPopupButton.DBFieldsName = null;
            this.NyuukinPopupButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.NyuukinPopupButton.DisplayItemName = "入金先CD";
            this.NyuukinPopupButton.DisplayPopUp = null;
            this.NyuukinPopupButton.ErrorMessage = null;
            this.NyuukinPopupButton.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NyuukinPopupButton.FocusOutCheckMethod")));
            this.NyuukinPopupButton.Font = new System.Drawing.Font("MS Gothic", 11.25F);
            this.NyuukinPopupButton.GetCodeMasterField = null;
            this.NyuukinPopupButton.Image = ((System.Drawing.Image)(resources.GetObject("NyuukinPopupButton.Image")));
            this.NyuukinPopupButton.ItemDefinedTypes = null;
            this.NyuukinPopupButton.LinkedSettingTextBox = null;
            this.NyuukinPopupButton.LinkedTextBoxs = null;
            this.NyuukinPopupButton.Location = new System.Drawing.Point(469, 44);
            this.NyuukinPopupButton.Name = "NyuukinPopupButton";
            this.NyuukinPopupButton.PopupAfterExecute = null;
            this.NyuukinPopupButton.PopupBeforeExecute = null;
            this.NyuukinPopupButton.PopupGetMasterField = "NYUUKINSAKI_CD,NYUUKINSAKI_NAME_RYAKU";
            this.NyuukinPopupButton.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NyuukinPopupButton.PopupSearchSendParams")));
            this.NyuukinPopupButton.PopupSetFormField = "NYUUKINSAKI_CD,NYUUKINSAKI_NAME_RYAKU";
            this.NyuukinPopupButton.PopupWindowId = r_framework.Const.WINDOW_ID.M_NYUUKINSAKI;
            this.NyuukinPopupButton.PopupWindowName = "検索共通ポップアップ";
            this.NyuukinPopupButton.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NyuukinPopupButton.popupWindowSetting")));
            this.NyuukinPopupButton.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NyuukinPopupButton.RegistCheckMethod")));
            this.NyuukinPopupButton.SearchDisplayFlag = 0;
            this.NyuukinPopupButton.SetFormField = "NYUUKINSAKI_CD,NYUUKINSAKI_NAME_RYAKU";
            this.NyuukinPopupButton.ShortItemName = "入金先CD";
            this.NyuukinPopupButton.Size = new System.Drawing.Size(22, 22);
            this.NyuukinPopupButton.TabIndex = 382;
            this.NyuukinPopupButton.TabStop = false;
            this.NyuukinPopupButton.UseVisualStyleBackColor = false;
            this.NyuukinPopupButton.ZeroPaddengFlag = false;
            // 
            // lblBangou
            // 
            this.lblBangou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblBangou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBangou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBangou.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblBangou.ForeColor = System.Drawing.Color.White;
            this.lblBangou.Location = new System.Drawing.Point(3, 1);
            this.lblBangou.Name = "lblBangou";
            this.lblBangou.Size = new System.Drawing.Size(110, 20);
            this.lblBangou.TabIndex = 9999;
            this.lblBangou.Text = "入金番号";
            this.lblBangou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NYUUKIN_NUMBER
            // 
            this.NYUUKIN_NUMBER.BackColor = System.Drawing.SystemColors.Window;
            this.NYUUKIN_NUMBER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NYUUKIN_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            this.NYUUKIN_NUMBER.DisplayPopUp = null;
            this.NYUUKIN_NUMBER.FocusOutCheckMethod = null;
            this.NYUUKIN_NUMBER.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NYUUKIN_NUMBER.ForeColor = System.Drawing.Color.Black;
            this.NYUUKIN_NUMBER.FormatSetting = "数値(#)フォーマット";
            this.NYUUKIN_NUMBER.IsInputErrorOccured = false;
            this.NYUUKIN_NUMBER.Location = new System.Drawing.Point(116, 1);
            this.NYUUKIN_NUMBER.Name = "NYUUKIN_NUMBER";
            this.NYUUKIN_NUMBER.PopupAfterExecute = null;
            this.NYUUKIN_NUMBER.PopupBeforeExecute = null;
            this.NYUUKIN_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKIN_NUMBER.PopupSearchSendParams")));
            this.NYUUKIN_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKIN_NUMBER.popupWindowSetting = null;
            rangeSettingDto2.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.NYUUKIN_NUMBER.RangeSetting = rangeSettingDto2;
            this.NYUUKIN_NUMBER.RegistCheckMethod = null;
            this.NYUUKIN_NUMBER.Size = new System.Drawing.Size(84, 20);
            this.NYUUKIN_NUMBER.TabIndex = 190;
            this.NYUUKIN_NUMBER.Tag = "半角10桁以内で入力してください";
            this.NYUUKIN_NUMBER.WordWrap = false;
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
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(533, 48);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10000;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // NyuuSyutuKinIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 458);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.NYUUKIN_NUMBER);
            this.Controls.Add(this.lblBangou);
            this.Controls.Add(this.NyuukinPopupButton);
            this.Controls.Add(this.NYUUKINSAKI_CD);
            this.Controls.Add(this.NYUUKINSAKI_NAME_RYAKU);
            this.Controls.Add(this.NYUUKINSAKI_LABEL);
            this.Controls.Add(this.txtNum_DenpyouSyurui);
            this.Controls.Add(this.TorihikiPopupButton);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Name = "NyuuSyutuKinIchiranForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_LABEL, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_CD, 0);
            this.Controls.SetChildIndex(this.TorihikiPopupButton, 0);
            this.Controls.SetChildIndex(this.txtNum_DenpyouSyurui, 0);
            this.Controls.SetChildIndex(this.NYUUKINSAKI_LABEL, 0);
            this.Controls.SetChildIndex(this.NYUUKINSAKI_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.NYUUKINSAKI_CD, 0);
            this.Controls.SetChildIndex(this.NyuukinPopupButton, 0);
            this.Controls.SetChildIndex(this.lblBangou, 0);
            this.Controls.SetChildIndex(this.NYUUKIN_NUMBER, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal Label label3;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_DenpyouSyurui;
        public r_framework.CustomControl.CustomRadioButton radbtn_Syuutukin;
        public r_framework.CustomControl.CustomRadioButton radbtn_Nyuukin;
        internal r_framework.CustomControl.CustomPopupOpenButton TorihikiPopupButton;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal Label TORIHIKISAKI_LABEL;
        private r_framework.CustomControl.CustomPanel panel2;
        internal Label NYUUKINSAKI_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox NYUUKINSAKI_CD;
        internal r_framework.CustomControl.CustomTextBox NYUUKINSAKI_NAME_RYAKU;
        internal r_framework.CustomControl.CustomPopupOpenButton NyuukinPopupButton;
        internal Label lblBangou;
        internal r_framework.CustomControl.CustomNumericTextBox2 NYUUKIN_NUMBER;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}