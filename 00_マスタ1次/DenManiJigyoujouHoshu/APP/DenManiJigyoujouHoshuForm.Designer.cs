using r_framework.CustomControl;
using System;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using DenManiJigyoujouHoshu.Logic;
using r_framework.APP.Base;
using r_framework.Const;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace DenManiJigyoujouHoshu.APP
{
    partial class DenManiJigyoujouHoshuForm : SuperForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DenManiJigyoujouHoshuForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.label15 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new r_framework.CustomControl.CustomPanel();
            this.EDI_MEMBER_ID = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.JIGYOUJOU_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.JIGYOUSHA_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.JIGYOUSHA_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.JIGYOUSHA_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.JIGYOUSHA_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.JIGYOUJOU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.JIGYOUJOU_ADDRESS4 = new r_framework.CustomControl.CustomTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.JIGYOUJOU_TEL = new r_framework.CustomControl.CustomPhoneNumberTextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.JIGYOUJOU_ADDRESS3 = new r_framework.CustomControl.CustomTextBox();
            this.JIGYOUJOU_ADDRESS2 = new r_framework.CustomControl.CustomTextBox();
            this.JIGYOUJOU_ADDRESS1 = new r_framework.CustomControl.CustomTextBox();
            this.JIGYOUJOU_POST = new r_framework.CustomControl.CustomPostalCodeTextBox();
            this.JIGYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.JWNET_JIGYOUJOU_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.SIKUCHOUSON_SEARCH_BUTTON = new r_framework.CustomControl.CustomButton();
            this.JIGYOUJOU_POST_SEACRH_BUTTON = new r_framework.CustomControl.CustomButton();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label15.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label15.CausesValidation = false;
            this.label15.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(19, 345);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(200, 20);
            this.label15.TabIndex = 644;
            this.label15.Text = "将軍連携　業者　";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.GENBA_SEARCH_BUTTON);
            this.panel2.Controls.Add(this.GYOUSHA_SEARCH_BUTTON);
            this.panel2.Controls.Add(this.GENBA_NAME);
            this.panel2.Controls.Add(this.GENBA_CD);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.GYOUSHA_CD);
            this.panel2.Controls.Add(this.GYOUSHA_NAME);
            this.panel2.Location = new System.Drawing.Point(13, 355);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(963, 63);
            this.panel2.TabIndex = 17;
            // 
            // GENBA_NAME
            // 
            this.GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GENBA_NAME.DBFieldsName = "GENBA_MEI";
            this.GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME.DisplayPopUp = null;
            this.GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.FocusOutCheckMethod")));
            this.GENBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_NAME.IsInputErrorOccured = false;
            this.GENBA_NAME.Location = new System.Drawing.Point(201, 36);
            this.GENBA_NAME.MaxLength = 0;
            this.GENBA_NAME.Name = "GENBA_NAME";
            this.GENBA_NAME.PopupAfterExecute = null;
            this.GENBA_NAME.PopupBeforeExecute = null;
            this.GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME.PopupSearchSendParams")));
            this.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME.popupWindowSetting")));
            this.GENBA_NAME.ReadOnly = true;
            this.GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.RegistCheckMethod")));
            this.GENBA_NAME.Size = new System.Drawing.Size(290, 20);
            this.GENBA_NAME.TabIndex = 21;
            this.GENBA_NAME.TabStop = false;
            this.GENBA_NAME.Tag = "現場名が表示されます";
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
            this.GENBA_CD.DisplayItemName = "現場CD";
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(140, 36);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "GENBA_CD_AfterPopup";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GYOUSHA_CD,GENBA_CD";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GYOUSHA_CD,GENBA_CD";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD,GENBA_NAME";
            this.GENBA_CD.ShortItemName = "現場CD";
            this.GENBA_CD.Size = new System.Drawing.Size(55, 20);
            this.GENBA_CD.TabIndex = 20;
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索も出来ます）";
            this.GENBA_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GENBA_CD_Validating);
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.CausesValidation = false;
            this.label17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(14, 36);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 20);
            this.label17.TabIndex = 640;
            this.label17.Text = "現場";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.CausesValidation = false;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(14, 12);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(120, 20);
            this.label16.TabIndex = 635;
            this.label16.Text = "業者";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.GYOUSHA_CD.DisplayItemName = "業者CD";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(140, 12);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GYOUSHA_CD_AfterPopup";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecuteMethod = "GYOUSHA_CD_BeforePopup";
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD,GYOUSHA_NAME";
            this.GYOUSHA_CD.ShortItemName = "業者CD";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(55, 20);
            this.GYOUSHA_CD.TabIndex = 18;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索も出来ます）";
            this.GYOUSHA_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_Validating);
            // 
            // GYOUSHA_NAME
            // 
            this.GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME.DisplayPopUp = null;
            this.GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.FocusOutCheckMethod")));
            this.GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_NAME.IsInputErrorOccured = false;
            this.GYOUSHA_NAME.Location = new System.Drawing.Point(201, 12);
            this.GYOUSHA_NAME.MaxLength = 0;
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupAfterExecute = null;
            this.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.Size = new System.Drawing.Size(290, 20);
            this.GYOUSHA_NAME.TabIndex = 19;
            this.GYOUSHA_NAME.TabStop = false;
            this.GYOUSHA_NAME.Tag = "業者名が表示されます";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.CausesValidation = false;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(18, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(200, 20);
            this.label1.TabIndex = 642;
            this.label1.Text = "電子マニフェスト　";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.SIKUCHOUSON_SEARCH_BUTTON);
            this.panel1.Controls.Add(this.JIGYOUJOU_POST_SEACRH_BUTTON);
            this.panel1.Controls.Add(this.EDI_MEMBER_ID);
            this.panel1.Controls.Add(this.JIGYOUJOU_CD);
            this.panel1.Controls.Add(this.JIGYOUSHA_KBN_3);
            this.panel1.Controls.Add(this.JIGYOUSHA_KBN_2);
            this.panel1.Controls.Add(this.JIGYOUSHA_KBN_1);
            this.panel1.Controls.Add(this.JIGYOUSHA_KBN);
            this.panel1.Controls.Add(this.JIGYOUJOU_NAME);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.JIGYOUJOU_ADDRESS4);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.JIGYOUJOU_TEL);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.JIGYOUJOU_ADDRESS3);
            this.panel1.Controls.Add(this.JIGYOUJOU_ADDRESS2);
            this.panel1.Controls.Add(this.JIGYOUJOU_ADDRESS1);
            this.panel1.Controls.Add(this.JIGYOUJOU_POST);
            this.panel1.Controls.Add(this.JIGYOUSHA_NAME);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label13);
            this.panel1.Controls.Add(this.JWNET_JIGYOUJOU_CD);
            this.panel1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel1.Location = new System.Drawing.Point(13, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(962, 327);
            this.panel1.TabIndex = 0;
            // 
            // EDI_MEMBER_ID
            // 
            this.EDI_MEMBER_ID.BackColor = System.Drawing.SystemColors.Window;
            this.EDI_MEMBER_ID.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EDI_MEMBER_ID.ChangeUpperCase = true;
            this.EDI_MEMBER_ID.CharacterLimitList = null;
            this.EDI_MEMBER_ID.CharactersNumber = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.EDI_MEMBER_ID.DBFieldsName = "EDI_MEMBER_ID";
            this.EDI_MEMBER_ID.DefaultBackColor = System.Drawing.Color.Empty;
            this.EDI_MEMBER_ID.DisplayItemName = "加入者番号";
            this.EDI_MEMBER_ID.DisplayPopUp = null;
            this.EDI_MEMBER_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EDI_MEMBER_ID.FocusOutCheckMethod")));
            this.EDI_MEMBER_ID.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.EDI_MEMBER_ID.ForeColor = System.Drawing.Color.Black;
            this.EDI_MEMBER_ID.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.EDI_MEMBER_ID.IsInputErrorOccured = false;
            this.EDI_MEMBER_ID.ItemDefinedTypes = "varchar";
            this.EDI_MEMBER_ID.Location = new System.Drawing.Point(139, 39);
            this.EDI_MEMBER_ID.MaxLength = 7;
            this.EDI_MEMBER_ID.Name = "EDI_MEMBER_ID";
            this.EDI_MEMBER_ID.PopupAfterExecute = null;
            this.EDI_MEMBER_ID.PopupAfterExecuteMethod = "";
            this.EDI_MEMBER_ID.PopupBeforeExecute = null;
            this.EDI_MEMBER_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EDI_MEMBER_ID.PopupSearchSendParams")));
            this.EDI_MEMBER_ID.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_JIGYOUSHA;
            this.EDI_MEMBER_ID.PopupWindowName = "検索共通ポップアップ";
            this.EDI_MEMBER_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EDI_MEMBER_ID.popupWindowSetting")));
            this.EDI_MEMBER_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EDI_MEMBER_ID.RegistCheckMethod")));
            this.EDI_MEMBER_ID.ShortItemName = "加入者番号";
            this.EDI_MEMBER_ID.Size = new System.Drawing.Size(65, 20);
            this.EDI_MEMBER_ID.TabIndex = 5;
            this.EDI_MEMBER_ID.Tag = "加入者番号を指定してください（スペースキー押下にて、検索も出来ます）";
            this.EDI_MEMBER_ID.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.EDI_MEMBER_ID.ZeroPaddengFlag = true;
            this.EDI_MEMBER_ID.Validating += new System.ComponentModel.CancelEventHandler(this.EDI_MEMBER_ID_Validating);
            // 
            // JIGYOUJOU_CD
            // 
            this.JIGYOUJOU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUJOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUJOU_CD.CustomFormatSetting = "0000000000";
            this.JIGYOUJOU_CD.DBFieldsName = "JIGYOUJOU_CD";
            this.JIGYOUJOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUJOU_CD.DisplayItemName = "事業場コード";
            this.JIGYOUJOU_CD.DisplayPopUp = null;
            this.JIGYOUJOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_CD.FocusOutCheckMethod")));
            this.JIGYOUJOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUJOU_CD.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUJOU_CD.FormatSetting = "カスタム";
            this.JIGYOUJOU_CD.IsInputErrorOccured = false;
            this.JIGYOUJOU_CD.ItemDefinedTypes = "varchar";
            this.JIGYOUJOU_CD.Location = new System.Drawing.Point(139, 117);
            this.JIGYOUJOU_CD.Name = "JIGYOUJOU_CD";
            this.JIGYOUJOU_CD.PopupAfterExecute = null;
            this.JIGYOUJOU_CD.PopupBeforeExecute = null;
            this.JIGYOUJOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUJOU_CD.PopupSearchSendParams")));
            this.JIGYOUJOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUJOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUJOU_CD.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.JIGYOUJOU_CD.RangeSetting = rangeSettingDto2;
            this.JIGYOUJOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_CD.RegistCheckMethod")));
            this.JIGYOUJOU_CD.ShortItemName = "事業場コード";
            this.JIGYOUJOU_CD.Size = new System.Drawing.Size(80, 20);
            this.JIGYOUJOU_CD.TabIndex = 7;
            this.JIGYOUJOU_CD.Tag = "半角数字10文字以内で入力してください";
            this.JIGYOUJOU_CD.WordWrap = false;
            this.JIGYOUJOU_CD.Validating += new System.ComponentModel.CancelEventHandler(this.JIGYOUJOU_CD_Validating);
            // 
            // JIGYOUSHA_KBN_3
            // 
            this.JIGYOUSHA_KBN_3.AutoSize = true;
            this.JIGYOUSHA_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_KBN_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_3.FocusOutCheckMethod")));
            this.JIGYOUSHA_KBN_3.LinkedTextBox = "JIGYOUSHA_KBN";
            this.JIGYOUSHA_KBN_3.Location = new System.Drawing.Point(421, 18);
            this.JIGYOUSHA_KBN_3.Name = "JIGYOUSHA_KBN_3";
            this.JIGYOUSHA_KBN_3.PopupAfterExecute = null;
            this.JIGYOUSHA_KBN_3.PopupBeforeExecute = null;
            this.JIGYOUSHA_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_KBN_3.PopupSearchSendParams")));
            this.JIGYOUSHA_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_KBN_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_KBN_3.popupWindowSetting")));
            this.JIGYOUSHA_KBN_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_3.RegistCheckMethod")));
            this.JIGYOUSHA_KBN_3.Size = new System.Drawing.Size(116, 17);
            this.JIGYOUSHA_KBN_3.TabIndex = 4;
            this.JIGYOUSHA_KBN_3.Text = "3. 処分事業者";
            this.JIGYOUSHA_KBN_3.UseVisualStyleBackColor = true;
            this.JIGYOUSHA_KBN_3.Value = "3";
            // 
            // JIGYOUSHA_KBN_2
            // 
            this.JIGYOUSHA_KBN_2.AutoSize = true;
            this.JIGYOUSHA_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_2.FocusOutCheckMethod")));
            this.JIGYOUSHA_KBN_2.LinkedTextBox = "JIGYOUSHA_KBN";
            this.JIGYOUSHA_KBN_2.Location = new System.Drawing.Point(286, 18);
            this.JIGYOUSHA_KBN_2.Name = "JIGYOUSHA_KBN_2";
            this.JIGYOUSHA_KBN_2.PopupAfterExecute = null;
            this.JIGYOUSHA_KBN_2.PopupBeforeExecute = null;
            this.JIGYOUSHA_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_KBN_2.PopupSearchSendParams")));
            this.JIGYOUSHA_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_KBN_2.popupWindowSetting")));
            this.JIGYOUSHA_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_2.RegistCheckMethod")));
            this.JIGYOUSHA_KBN_2.Size = new System.Drawing.Size(130, 17);
            this.JIGYOUSHA_KBN_2.TabIndex = 3;
            this.JIGYOUSHA_KBN_2.Text = "2. 収集運搬業者";
            this.JIGYOUSHA_KBN_2.UseVisualStyleBackColor = true;
            this.JIGYOUSHA_KBN_2.Value = "2";
            // 
            // JIGYOUSHA_KBN_1
            // 
            this.JIGYOUSHA_KBN_1.AutoSize = true;
            this.JIGYOUSHA_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_1.FocusOutCheckMethod")));
            this.JIGYOUSHA_KBN_1.LinkedTextBox = "JIGYOUSHA_KBN";
            this.JIGYOUSHA_KBN_1.Location = new System.Drawing.Point(165, 18);
            this.JIGYOUSHA_KBN_1.Name = "JIGYOUSHA_KBN_1";
            this.JIGYOUSHA_KBN_1.PopupAfterExecute = null;
            this.JIGYOUSHA_KBN_1.PopupBeforeExecute = null;
            this.JIGYOUSHA_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_KBN_1.PopupSearchSendParams")));
            this.JIGYOUSHA_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_KBN_1.popupWindowSetting")));
            this.JIGYOUSHA_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN_1.RegistCheckMethod")));
            this.JIGYOUSHA_KBN_1.Size = new System.Drawing.Size(116, 17);
            this.JIGYOUSHA_KBN_1.TabIndex = 2;
            this.JIGYOUSHA_KBN_1.Text = "1. 排出事業者";
            this.JIGYOUSHA_KBN_1.UseVisualStyleBackColor = true;
            this.JIGYOUSHA_KBN_1.Value = "1";
            // 
            // JIGYOUSHA_KBN
            // 
            this.JIGYOUSHA_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUSHA_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_KBN.DBFieldsName = "JIGYOUSHA_KBN";
            this.JIGYOUSHA_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_KBN.DisplayItemName = "事業者区分";
            this.JIGYOUSHA_KBN.DisplayPopUp = null;
            this.JIGYOUSHA_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN.FocusOutCheckMethod")));
            this.JIGYOUSHA_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_KBN.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_KBN.IsInputErrorOccured = false;
            this.JIGYOUSHA_KBN.ItemDefinedTypes = "smallint";
            this.JIGYOUSHA_KBN.LinkedRadioButtonArray = new string[] {
        "JIGYOUSHA_KBN_1",
        "JIGYOUSHA_KBN_2",
        "JIGYOUSHA_KBN_3"};
            this.JIGYOUSHA_KBN.Location = new System.Drawing.Point(139, 15);
            this.JIGYOUSHA_KBN.Name = "JIGYOUSHA_KBN";
            this.JIGYOUSHA_KBN.PopupAfterExecute = null;
            this.JIGYOUSHA_KBN.PopupBeforeExecute = null;
            this.JIGYOUSHA_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_KBN.PopupSearchSendParams")));
            this.JIGYOUSHA_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_KBN.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.JIGYOUSHA_KBN.RangeSetting = rangeSettingDto3;
            this.JIGYOUSHA_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_KBN.RegistCheckMethod")));
            this.JIGYOUSHA_KBN.ShortItemName = "事業者区分";
            this.JIGYOUSHA_KBN.Size = new System.Drawing.Size(20, 20);
            this.JIGYOUSHA_KBN.TabIndex = 1;
            this.JIGYOUSHA_KBN.Tag = "【1～3】のいずれかで入力してください";
            this.JIGYOUSHA_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.JIGYOUSHA_KBN.WordWrap = false;
            this.JIGYOUSHA_KBN.TextChanged += new System.EventHandler(this.JIGYOUSHA_KBN_TextChanged);
            // 
            // JIGYOUJOU_NAME
            // 
            this.JIGYOUJOU_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUJOU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUJOU_NAME.CharactersNumber = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.JIGYOUJOU_NAME.DBFieldsName = "JIGYOUJOU_NAME";
            this.JIGYOUJOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUJOU_NAME.DisplayItemName = "事業場名";
            this.JIGYOUJOU_NAME.DisplayPopUp = null;
            this.JIGYOUJOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_NAME.FocusOutCheckMethod")));
            this.JIGYOUJOU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUJOU_NAME.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUJOU_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUJOU_NAME.IsInputErrorOccured = false;
            this.JIGYOUJOU_NAME.ItemDefinedTypes = "varchar";
            this.JIGYOUJOU_NAME.Location = new System.Drawing.Point(139, 140);
            this.JIGYOUJOU_NAME.MaxLength = 120;
            this.JIGYOUJOU_NAME.Multiline = true;
            this.JIGYOUJOU_NAME.Name = "JIGYOUJOU_NAME";
            this.JIGYOUJOU_NAME.PopupAfterExecute = null;
            this.JIGYOUJOU_NAME.PopupBeforeExecute = null;
            this.JIGYOUJOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUJOU_NAME.PopupSearchSendParams")));
            this.JIGYOUJOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUJOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUJOU_NAME.popupWindowSetting")));
            this.JIGYOUJOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_NAME.RegistCheckMethod")));
            this.JIGYOUJOU_NAME.ShortItemName = "事業場名";
            this.JIGYOUJOU_NAME.Size = new System.Drawing.Size(710, 36);
            this.JIGYOUJOU_NAME.TabIndex = 8;
            this.JIGYOUJOU_NAME.Tag = "全角６０文字以内で入力してください";
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.CausesValidation = false;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(13, 140);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(120, 20);
            this.label6.TabIndex = 626;
            this.label6.Text = "事業場名称";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JIGYOUJOU_ADDRESS4
            // 
            this.JIGYOUJOU_ADDRESS4.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUJOU_ADDRESS4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUJOU_ADDRESS4.CharactersNumber = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.JIGYOUJOU_ADDRESS4.DBFieldsName = "JIGYOUJOU_ADDRESS4";
            this.JIGYOUJOU_ADDRESS4.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUJOU_ADDRESS4.DisplayItemName = "詳細住所";
            this.JIGYOUJOU_ADDRESS4.DisplayPopUp = null;
            this.JIGYOUJOU_ADDRESS4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_ADDRESS4.FocusOutCheckMethod")));
            this.JIGYOUJOU_ADDRESS4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUJOU_ADDRESS4.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUJOU_ADDRESS4.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUJOU_ADDRESS4.IsInputErrorOccured = false;
            this.JIGYOUJOU_ADDRESS4.ItemDefinedTypes = "varchar";
            this.JIGYOUJOU_ADDRESS4.Location = new System.Drawing.Point(139, 276);
            this.JIGYOUJOU_ADDRESS4.MaxLength = 100;
            this.JIGYOUJOU_ADDRESS4.Name = "JIGYOUJOU_ADDRESS4";
            this.JIGYOUJOU_ADDRESS4.PopupAfterExecute = null;
            this.JIGYOUJOU_ADDRESS4.PopupBeforeExecute = null;
            this.JIGYOUJOU_ADDRESS4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUJOU_ADDRESS4.PopupSearchSendParams")));
            this.JIGYOUJOU_ADDRESS4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUJOU_ADDRESS4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUJOU_ADDRESS4.popupWindowSetting")));
            this.JIGYOUJOU_ADDRESS4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_ADDRESS4.RegistCheckMethod")));
            this.JIGYOUJOU_ADDRESS4.ShortItemName = "詳細住所";
            this.JIGYOUJOU_ADDRESS4.Size = new System.Drawing.Size(710, 20);
            this.JIGYOUJOU_ADDRESS4.TabIndex = 15;
            this.JIGYOUJOU_ADDRESS4.Tag = "全角５０文字以内で入力してください";
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(13, 116);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "事業場コード";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.CausesValidation = false;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "事業者区分";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JIGYOUJOU_TEL
            // 
            this.JIGYOUJOU_TEL.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUJOU_TEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUJOU_TEL.DBFieldsName = "JIGYOUJOU_TEL";
            this.JIGYOUJOU_TEL.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUJOU_TEL.DisplayItemName = "電話番号";
            this.JIGYOUJOU_TEL.DisplayPopUp = null;
            this.JIGYOUJOU_TEL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_TEL.FocusOutCheckMethod")));
            this.JIGYOUJOU_TEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUJOU_TEL.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUJOU_TEL.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.JIGYOUJOU_TEL.IsInputErrorOccured = false;
            this.JIGYOUJOU_TEL.ItemDefinedTypes = "varchar";
            this.JIGYOUJOU_TEL.Location = new System.Drawing.Point(139, 300);
            this.JIGYOUJOU_TEL.Name = "JIGYOUJOU_TEL";
            this.JIGYOUJOU_TEL.PopupAfterExecute = null;
            this.JIGYOUJOU_TEL.PopupBeforeExecute = null;
            this.JIGYOUJOU_TEL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUJOU_TEL.PopupSearchSendParams")));
            this.JIGYOUJOU_TEL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUJOU_TEL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUJOU_TEL.popupWindowSetting")));
            this.JIGYOUJOU_TEL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_TEL.RegistCheckMethod")));
            this.JIGYOUJOU_TEL.ShortItemName = "電話番号";
            this.JIGYOUJOU_TEL.Size = new System.Drawing.Size(115, 20);
            this.JIGYOUJOU_TEL.TabIndex = 16;
            this.JIGYOUJOU_TEL.Tag = "半角数字（ハイフン可）１5文字以内で入力してください";
            this.JIGYOUJOU_TEL.UseParentheses = true;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.CausesValidation = false;
            this.label12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(13, 300);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 20);
            this.label12.TabIndex = 0;
            this.label12.Text = "電話番号";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // JIGYOUJOU_ADDRESS3
            // 
            this.JIGYOUJOU_ADDRESS3.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUJOU_ADDRESS3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUJOU_ADDRESS3.ChangeWideCase = true;
            this.JIGYOUJOU_ADDRESS3.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.JIGYOUJOU_ADDRESS3.DBFieldsName = "JIGYOUJOU_ADDRESS3";
            this.JIGYOUJOU_ADDRESS3.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUJOU_ADDRESS3.DisplayItemName = "町域";
            this.JIGYOUJOU_ADDRESS3.DisplayPopUp = null;
            this.JIGYOUJOU_ADDRESS3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_ADDRESS3.FocusOutCheckMethod")));
            this.JIGYOUJOU_ADDRESS3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUJOU_ADDRESS3.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUJOU_ADDRESS3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUJOU_ADDRESS3.IsInputErrorOccured = false;
            this.JIGYOUJOU_ADDRESS3.ItemDefinedTypes = "varchar";
            this.JIGYOUJOU_ADDRESS3.Location = new System.Drawing.Point(139, 252);
            this.JIGYOUJOU_ADDRESS3.MaxLength = 80;
            this.JIGYOUJOU_ADDRESS3.Name = "JIGYOUJOU_ADDRESS3";
            this.JIGYOUJOU_ADDRESS3.PopupAfterExecute = null;
            this.JIGYOUJOU_ADDRESS3.PopupBeforeExecute = null;
            this.JIGYOUJOU_ADDRESS3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUJOU_ADDRESS3.PopupSearchSendParams")));
            this.JIGYOUJOU_ADDRESS3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUJOU_ADDRESS3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUJOU_ADDRESS3.popupWindowSetting")));
            this.JIGYOUJOU_ADDRESS3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_ADDRESS3.RegistCheckMethod")));
            this.JIGYOUJOU_ADDRESS3.ShortItemName = "町域";
            this.JIGYOUJOU_ADDRESS3.Size = new System.Drawing.Size(570, 20);
            this.JIGYOUJOU_ADDRESS3.TabIndex = 14;
            this.JIGYOUJOU_ADDRESS3.Tag = "全角４０文字以内で入力してください";
            // 
            // JIGYOUJOU_ADDRESS2
            // 
            this.JIGYOUJOU_ADDRESS2.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUJOU_ADDRESS2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUJOU_ADDRESS2.ChangeWideCase = true;
            this.JIGYOUJOU_ADDRESS2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.JIGYOUJOU_ADDRESS2.DBFieldsName = "JIGYOUJOU_ADDRESS2";
            this.JIGYOUJOU_ADDRESS2.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUJOU_ADDRESS2.DisplayItemName = "市区町村";
            this.JIGYOUJOU_ADDRESS2.DisplayPopUp = null;
            this.JIGYOUJOU_ADDRESS2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_ADDRESS2.FocusOutCheckMethod")));
            this.JIGYOUJOU_ADDRESS2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUJOU_ADDRESS2.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUJOU_ADDRESS2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUJOU_ADDRESS2.IsInputErrorOccured = false;
            this.JIGYOUJOU_ADDRESS2.ItemDefinedTypes = "varchar";
            this.JIGYOUJOU_ADDRESS2.Location = new System.Drawing.Point(139, 228);
            this.JIGYOUJOU_ADDRESS2.MaxLength = 40;
            this.JIGYOUJOU_ADDRESS2.Name = "JIGYOUJOU_ADDRESS2";
            this.JIGYOUJOU_ADDRESS2.PopupAfterExecute = null;
            this.JIGYOUJOU_ADDRESS2.PopupBeforeExecute = null;
            this.JIGYOUJOU_ADDRESS2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUJOU_ADDRESS2.PopupSearchSendParams")));
            this.JIGYOUJOU_ADDRESS2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUJOU_ADDRESS2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUJOU_ADDRESS2.popupWindowSetting")));
            this.JIGYOUJOU_ADDRESS2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_ADDRESS2.RegistCheckMethod")));
            this.JIGYOUJOU_ADDRESS2.ShortItemName = "市区町村";
            this.JIGYOUJOU_ADDRESS2.Size = new System.Drawing.Size(290, 20);
            this.JIGYOUJOU_ADDRESS2.TabIndex = 12;
            this.JIGYOUJOU_ADDRESS2.Tag = "全角２０文字以内で入力してください";
            // 
            // JIGYOUJOU_ADDRESS1
            // 
            this.JIGYOUJOU_ADDRESS1.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUJOU_ADDRESS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUJOU_ADDRESS1.ChangeWideCase = true;
            this.JIGYOUJOU_ADDRESS1.CharactersNumber = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.JIGYOUJOU_ADDRESS1.DBFieldsName = "JIGYOUJOU_ADDRESS1";
            this.JIGYOUJOU_ADDRESS1.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUJOU_ADDRESS1.DisplayItemName = "都道府県";
            this.JIGYOUJOU_ADDRESS1.DisplayPopUp = null;
            this.JIGYOUJOU_ADDRESS1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_ADDRESS1.FocusOutCheckMethod")));
            this.JIGYOUJOU_ADDRESS1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUJOU_ADDRESS1.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUJOU_ADDRESS1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.JIGYOUJOU_ADDRESS1.IsInputErrorOccured = false;
            this.JIGYOUJOU_ADDRESS1.ItemDefinedTypes = "varchar";
            this.JIGYOUJOU_ADDRESS1.Location = new System.Drawing.Point(139, 204);
            this.JIGYOUJOU_ADDRESS1.MaxLength = 8;
            this.JIGYOUJOU_ADDRESS1.Name = "JIGYOUJOU_ADDRESS1";
            this.JIGYOUJOU_ADDRESS1.PopupAfterExecute = null;
            this.JIGYOUJOU_ADDRESS1.PopupBeforeExecute = null;
            this.JIGYOUJOU_ADDRESS1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUJOU_ADDRESS1.PopupSearchSendParams")));
            this.JIGYOUJOU_ADDRESS1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUJOU_ADDRESS1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUJOU_ADDRESS1.popupWindowSetting")));
            this.JIGYOUJOU_ADDRESS1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_ADDRESS1.RegistCheckMethod")));
            this.JIGYOUJOU_ADDRESS1.ShortItemName = "都道府県";
            this.JIGYOUJOU_ADDRESS1.Size = new System.Drawing.Size(65, 20);
            this.JIGYOUJOU_ADDRESS1.TabIndex = 11;
            this.JIGYOUJOU_ADDRESS1.Tag = "全角４文字以内で入力してください";
            // 
            // JIGYOUJOU_POST
            // 
            this.JIGYOUJOU_POST.BackColor = System.Drawing.SystemColors.Window;
            this.JIGYOUJOU_POST.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUJOU_POST.DBFieldsName = "JIGYOUJOU_POST";
            this.JIGYOUJOU_POST.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUJOU_POST.DisplayItemName = "郵便番号";
            this.JIGYOUJOU_POST.DisplayPopUp = null;
            this.JIGYOUJOU_POST.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_POST.FocusOutCheckMethod")));
            this.JIGYOUJOU_POST.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUJOU_POST.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUJOU_POST.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.JIGYOUJOU_POST.IsInputErrorOccured = false;
            this.JIGYOUJOU_POST.ItemDefinedTypes = "varchar";
            this.JIGYOUJOU_POST.Location = new System.Drawing.Point(139, 180);
            this.JIGYOUJOU_POST.Name = "JIGYOUJOU_POST";
            this.JIGYOUJOU_POST.PopupAfterExecute = null;
            this.JIGYOUJOU_POST.PopupBeforeExecute = null;
            this.JIGYOUJOU_POST.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUJOU_POST.PopupSearchSendParams")));
            this.JIGYOUJOU_POST.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUJOU_POST.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUJOU_POST.popupWindowSetting")));
            this.JIGYOUJOU_POST.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUJOU_POST.RegistCheckMethod")));
            this.JIGYOUJOU_POST.ShortItemName = "郵便番号";
            this.JIGYOUJOU_POST.Size = new System.Drawing.Size(65, 20);
            this.JIGYOUJOU_POST.TabIndex = 9;
            this.JIGYOUJOU_POST.Tag = "郵便番号を指定してください";
            // 
            // JIGYOUSHA_NAME
            // 
            this.JIGYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.JIGYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JIGYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.JIGYOUSHA_NAME.DBFieldsName = "JIGYOUSHA_NAME";
            this.JIGYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUSHA_NAME.DisplayItemName = "事業者名";
            this.JIGYOUSHA_NAME.DisplayPopUp = null;
            this.JIGYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_NAME.FocusOutCheckMethod")));
            this.JIGYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JIGYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.JIGYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.JIGYOUSHA_NAME.IsInputErrorOccured = false;
            this.JIGYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.JIGYOUSHA_NAME.Location = new System.Drawing.Point(139, 64);
            this.JIGYOUSHA_NAME.MaxLength = 0;
            this.JIGYOUSHA_NAME.Multiline = true;
            this.JIGYOUSHA_NAME.Name = "JIGYOUSHA_NAME";
            this.JIGYOUSHA_NAME.PopupAfterExecute = null;
            this.JIGYOUSHA_NAME.PopupBeforeExecute = null;
            this.JIGYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JIGYOUSHA_NAME.PopupSearchSendParams")));
            this.JIGYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JIGYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JIGYOUSHA_NAME.popupWindowSetting")));
            this.JIGYOUSHA_NAME.ReadOnly = true;
            this.JIGYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JIGYOUSHA_NAME.RegistCheckMethod")));
            this.JIGYOUSHA_NAME.ShortItemName = "事業者名";
            this.JIGYOUSHA_NAME.Size = new System.Drawing.Size(710, 48);
            this.JIGYOUSHA_NAME.TabIndex = 6;
            this.JIGYOUSHA_NAME.TabStop = false;
            this.JIGYOUSHA_NAME.Tag = "事業者名が表示されます";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.CausesValidation = false;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(13, 276);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(120, 20);
            this.label11.TabIndex = 0;
            this.label11.Text = "詳細住所";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.CausesValidation = false;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(13, 252);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(120, 20);
            this.label10.TabIndex = 0;
            this.label10.Text = "町域";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.CausesValidation = false;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(13, 228);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(120, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "市区町村";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.CausesValidation = false;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(13, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(120, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "都道府県";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.CausesValidation = false;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(13, 180);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "郵便番号";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(13, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "事業者名称";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(13, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "加入者番号";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label13.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label13.CausesValidation = false;
            this.label13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(263, 300);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(120, 20);
            this.label13.TabIndex = 0;
            this.label13.Text = "JWNET事業場CD";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label13.Visible = false;
            // 
            // JWNET_JIGYOUJOU_CD
            // 
            this.JWNET_JIGYOUJOU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.JWNET_JIGYOUJOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.JWNET_JIGYOUJOU_CD.CharacterLimitList = null;
            this.JWNET_JIGYOUJOU_CD.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.JWNET_JIGYOUJOU_CD.DBFieldsName = "JWNET_JIGYOUJOU_CD";
            this.JWNET_JIGYOUJOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.JWNET_JIGYOUJOU_CD.DisplayItemName = "JWNET事業場CD";
            this.JWNET_JIGYOUJOU_CD.DisplayPopUp = null;
            this.JWNET_JIGYOUJOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JWNET_JIGYOUJOU_CD.FocusOutCheckMethod")));
            this.JWNET_JIGYOUJOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.JWNET_JIGYOUJOU_CD.ForeColor = System.Drawing.Color.Black;
            this.JWNET_JIGYOUJOU_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.JWNET_JIGYOUJOU_CD.IsInputErrorOccured = false;
            this.JWNET_JIGYOUJOU_CD.ItemDefinedTypes = "varchar";
            this.JWNET_JIGYOUJOU_CD.Location = new System.Drawing.Point(389, 300);
            this.JWNET_JIGYOUJOU_CD.MaxLength = 10;
            this.JWNET_JIGYOUJOU_CD.Name = "JWNET_JIGYOUJOU_CD";
            this.JWNET_JIGYOUJOU_CD.PopupAfterExecute = null;
            this.JWNET_JIGYOUJOU_CD.PopupBeforeExecute = null;
            this.JWNET_JIGYOUJOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("JWNET_JIGYOUJOU_CD.PopupSearchSendParams")));
            this.JWNET_JIGYOUJOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.JWNET_JIGYOUJOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("JWNET_JIGYOUJOU_CD.popupWindowSetting")));
            this.JWNET_JIGYOUJOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("JWNET_JIGYOUJOU_CD.RegistCheckMethod")));
            this.JWNET_JIGYOUJOU_CD.ShortItemName = "JWNET事業場CD";
            this.JWNET_JIGYOUJOU_CD.Size = new System.Drawing.Size(100, 20);
            this.JWNET_JIGYOUJOU_CD.TabIndex = 17;
            this.JWNET_JIGYOUJOU_CD.Tag = "半角数字10文字以内で入力してください";
            this.JWNET_JIGYOUJOU_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.JWNET_JIGYOUJOU_CD.Visible = false;
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
            this.GENBA_SEARCH_BUTTON.Location = new System.Drawing.Point(494, 35);
            this.GENBA_SEARCH_BUTTON.Name = "GENBA_SEARCH_BUTTON";
            this.GENBA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "GENBA_CD_AfterPopup";
            this.GENBA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupBeforeExecuteMethod = "";
            this.GENBA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD,GENBA_CD";
            this.GENBA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GENBA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD,GENBA_CD";
            this.GENBA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_SEARCH_BUTTON.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.popupWindowSetting")));
            this.GENBA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GENBA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GENBA_SEARCH_BUTTON.SetFormField = null;
            this.GENBA_SEARCH_BUTTON.ShortItemName = null;
            this.GENBA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GENBA_SEARCH_BUTTON.TabIndex = 651;
            this.GENBA_SEARCH_BUTTON.TabStop = false;
            this.GENBA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GENBA_SEARCH_BUTTON.ZeroPaddengFlag = false;
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
            this.GYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(494, 11);
            this.GYOUSHA_SEARCH_BUTTON.Name = "GYOUSHA_SEARCH_BUTTON";
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "GYOUSHA_CD_AfterPopup";
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecuteMethod = "GYOUSHA_CD_BeforePopup";
            this.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD";
            this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD";
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.popupWindowSetting")));
            this.GYOUSHA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GYOUSHA_SEARCH_BUTTON.SetFormField = null;
            this.GYOUSHA_SEARCH_BUTTON.ShortItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_SEARCH_BUTTON.TabIndex = 650;
            this.GYOUSHA_SEARCH_BUTTON.TabStop = false;
            this.GYOUSHA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GYOUSHA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // SIKUCHOUSON_SEARCH_BUTTON
            // 
            this.SIKUCHOUSON_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.SIKUCHOUSON_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("SIKUCHOUSON_SEARCH_BUTTON.Image")));
            this.SIKUCHOUSON_SEARCH_BUTTON.Location = new System.Drawing.Point(435, 227);
            this.SIKUCHOUSON_SEARCH_BUTTON.Name = "SIKUCHOUSON_SEARCH_BUTTON";
            this.SIKUCHOUSON_SEARCH_BUTTON.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.SIKUCHOUSON_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.SIKUCHOUSON_SEARCH_BUTTON.TabIndex = 13;
            this.SIKUCHOUSON_SEARCH_BUTTON.Tag = "指定した都道府県、市区町村から住所検索画面を表示します";
            this.SIKUCHOUSON_SEARCH_BUTTON.UseVisualStyleBackColor = true;
            this.SIKUCHOUSON_SEARCH_BUTTON.Click += new System.EventHandler(this.SIKUCHOUSON_SEARCH_BUTTON_Click);
            // 
            // JIGYOUJOU_POST_SEACRH_BUTTON
            // 
            this.JIGYOUJOU_POST_SEACRH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.JIGYOUJOU_POST_SEACRH_BUTTON.Location = new System.Drawing.Point(208, 180);
            this.JIGYOUJOU_POST_SEACRH_BUTTON.Name = "JIGYOUJOU_POST_SEACRH_BUTTON";
            this.JIGYOUJOU_POST_SEACRH_BUTTON.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.JIGYOUJOU_POST_SEACRH_BUTTON.Size = new System.Drawing.Size(86, 20);
            this.JIGYOUJOU_POST_SEACRH_BUTTON.TabIndex = 10;
            this.JIGYOUJOU_POST_SEACRH_BUTTON.Tag = "指定した郵便番号から住所検索画面を表示します";
            this.JIGYOUJOU_POST_SEACRH_BUTTON.Text = "住所参照";
            this.JIGYOUJOU_POST_SEACRH_BUTTON.UseVisualStyleBackColor = true;
            this.JIGYOUJOU_POST_SEACRH_BUTTON.Click += new System.EventHandler(this.JIGYOUJOU_POST_SEACRH_BUTTON_Click);
            // 
            // DenManiJigyoujouHoshuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(999, 436);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "DenManiJigyoujouHoshuForm";
            this.UserRegistCheck += new r_framework.APP.Base.SuperForm.UserRegistCheckHandler(this.DenManiJigyoujouHoshuForm_UserRegistCheck);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private r_framework.CustomControl.CustomRadioButton SBN_KBN;
        private r_framework.CustomControl.CustomRadioButton UPN_KBN;
        private r_framework.CustomControl.CustomRadioButton HST_KBN;
        private Panel panel2;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME;
        internal r_framework.CustomControl.CustomPanel panel1;
        internal r_framework.CustomControl.CustomTextBox JIGYOUJOU_NAME;
        internal r_framework.CustomControl.CustomTextBox JIGYOUJOU_ADDRESS4;
        internal r_framework.CustomControl.CustomPhoneNumberTextBox JIGYOUJOU_TEL;
        internal r_framework.CustomControl.CustomTextBox JIGYOUJOU_ADDRESS3;
        internal r_framework.CustomControl.CustomTextBox JIGYOUJOU_ADDRESS2;
        internal r_framework.CustomControl.CustomTextBox JIGYOUJOU_ADDRESS1;
        internal r_framework.CustomControl.CustomPostalCodeTextBox JIGYOUJOU_POST;
        internal r_framework.CustomControl.CustomTextBox JIGYOUSHA_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 JIGYOUSHA_KBN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        internal CustomRadioButton JIGYOUSHA_KBN_1;
        internal CustomRadioButton JIGYOUSHA_KBN_3;
        internal CustomRadioButton JIGYOUSHA_KBN_2;
        internal CustomNumericTextBox2 JIGYOUJOU_CD;
        internal CustomAlphaNumTextBox EDI_MEMBER_ID;
        internal CustomAlphaNumTextBox JWNET_JIGYOUJOU_CD;
        private Label label13;
        internal CustomPopupOpenButton GENBA_SEARCH_BUTTON;
        internal CustomPopupOpenButton GYOUSHA_SEARCH_BUTTON;
        internal CustomButton JIGYOUJOU_POST_SEACRH_BUTTON;
        internal CustomButton SIKUCHOUSON_SEARCH_BUTTON;
    }
}
