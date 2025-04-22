using System.Windows.Forms;
using System;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiIchiran.APP
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
            this.pnlSearchString = new r_framework.CustomControl.CustomPanel();
            this.lblUnpanGyousha = new System.Windows.Forms.Label();
            this.UNPAN_GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SHARYOU_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.UNPAN_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHARYOU_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lblSyasyu = new System.Windows.Forms.Label();
            this.lblCourse = new System.Windows.Forms.Label();
            this.lblSyaL = new System.Windows.Forms.Label();
            this.lbUntensya = new System.Windows.Forms.Label();
            this.lbHojyosya = new System.Windows.Forms.Label();
            this.txtSHAIN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.txtSHoujyosyaNm = new r_framework.CustomControl.CustomTextBox();
            this.txtSHASHU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.txtSHoujyosyaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.txtCourseNm = new r_framework.CustomControl.CustomTextBox();
            this.txtSHASHU_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.txtSHAIN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.txtCourseCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.pnlSearchString.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.searchString.Location = new System.Drawing.Point(0, 0);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(936, 70);
            this.searchString.TabIndex = 500;
            this.searchString.TabStop = false;
            this.searchString.Tag = "検索条件設定画面で設定した条件が表示されます。";
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(1, 447);
            this.bt_ptn1.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn1.TabIndex = 80;
            this.bt_ptn1.Text = "パターン1";
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(201, 447);
            this.bt_ptn2.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn2.TabIndex = 90;
            this.bt_ptn2.Text = "パターン2";
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(401, 447);
            this.bt_ptn3.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn3.TabIndex = 100;
            this.bt_ptn3.Text = "パターン3";
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(601, 447);
            this.bt_ptn4.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn4.TabIndex = 110;
            this.bt_ptn4.Text = "パターン4";
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(801, 447);
            this.bt_ptn5.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn5.TabIndex = 120;
            this.bt_ptn5.Text = "パターン5";
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(0, 72);
            this.customSortHeader1.TabIndex = 60;
            // 
            // pnlSearchString
            // 
            this.pnlSearchString.Controls.Add(this.lblUnpanGyousha);
            this.pnlSearchString.Controls.Add(this.UNPAN_GYOUSHA_NAME);
            this.pnlSearchString.Controls.Add(this.SHARYOU_NAME_RYAKU);
            this.pnlSearchString.Controls.Add(this.UNPAN_GYOUSHA_CD);
            this.pnlSearchString.Controls.Add(this.SHARYOU_CD);
            this.pnlSearchString.Controls.Add(this.lblSyasyu);
            this.pnlSearchString.Controls.Add(this.lblCourse);
            this.pnlSearchString.Controls.Add(this.lblSyaL);
            this.pnlSearchString.Controls.Add(this.lbUntensya);
            this.pnlSearchString.Controls.Add(this.lbHojyosya);
            this.pnlSearchString.Controls.Add(this.txtSHAIN_NAME);
            this.pnlSearchString.Controls.Add(this.txtSHoujyosyaNm);
            this.pnlSearchString.Controls.Add(this.txtSHASHU_NAME);
            this.pnlSearchString.Controls.Add(this.txtSHoujyosyaCd);
            this.pnlSearchString.Controls.Add(this.txtCourseNm);
            this.pnlSearchString.Controls.Add(this.txtSHASHU_CD);
            this.pnlSearchString.Controls.Add(this.txtSHAIN_CD);
            this.pnlSearchString.Controls.Add(this.txtCourseCd);
            this.pnlSearchString.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchString.Name = "pnlSearchString";
            this.pnlSearchString.Size = new System.Drawing.Size(799, 70);
            this.pnlSearchString.TabIndex = 802;
            // 
            // lblUnpanGyousha
            // 
            this.lblUnpanGyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblUnpanGyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUnpanGyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblUnpanGyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblUnpanGyousha.ForeColor = System.Drawing.Color.White;
            this.lblUnpanGyousha.Location = new System.Drawing.Point(321, 44);
            this.lblUnpanGyousha.Name = "lblUnpanGyousha";
            this.lblUnpanGyousha.Size = new System.Drawing.Size(110, 20);
            this.lblUnpanGyousha.TabIndex = 830;
            this.lblUnpanGyousha.Text = "運搬業者";
            this.lblUnpanGyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UNPAN_GYOUSHA_NAME
            // 
            this.UNPAN_GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNPAN_GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_NAME.DBFieldsName = "";
            this.UNPAN_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_NAME.DisplayItemName = "";
            this.UNPAN_GYOUSHA_NAME.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNPAN_GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.UNPAN_GYOUSHA_NAME.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_NAME.Location = new System.Drawing.Point(482, 44);
            this.UNPAN_GYOUSHA_NAME.MaxLength = 20;
            this.UNPAN_GYOUSHA_NAME.Name = "UNPAN_GYOUSHA_NAME";
            this.UNPAN_GYOUSHA_NAME.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_NAME.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.popupWindowSetting")));
            this.UNPAN_GYOUSHA_NAME.ReadOnly = true;
            this.UNPAN_GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_NAME.Size = new System.Drawing.Size(285, 20);
            this.UNPAN_GYOUSHA_NAME.TabIndex = 829;
            this.UNPAN_GYOUSHA_NAME.TabStop = false;
            this.UNPAN_GYOUSHA_NAME.Tag = " ";
            // 
            // SHARYOU_NAME_RYAKU
            // 
            this.SHARYOU_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHARYOU_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHARYOU_NAME_RYAKU.DBFieldsName = "SHARYOU_NAME_RYAKU";
            this.SHARYOU_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHARYOU_NAME_RYAKU.DisplayPopUp = null;
            this.SHARYOU_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_NAME_RYAKU.FocusOutCheckMethod")));
            this.SHARYOU_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHARYOU_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.SHARYOU_NAME_RYAKU.IsInputErrorOccured = false;
            this.SHARYOU_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.SHARYOU_NAME_RYAKU.Location = new System.Drawing.Point(482, 22);
            this.SHARYOU_NAME_RYAKU.MaxLength = 10;
            this.SHARYOU_NAME_RYAKU.Name = "SHARYOU_NAME_RYAKU";
            this.SHARYOU_NAME_RYAKU.PopupAfterExecute = null;
            this.SHARYOU_NAME_RYAKU.PopupBeforeExecute = null;
            this.SHARYOU_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHARYOU_NAME_RYAKU.PopupSearchSendParams")));
            this.SHARYOU_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHARYOU_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHARYOU_NAME_RYAKU.popupWindowSetting")));
            this.SHARYOU_NAME_RYAKU.ReadOnly = true;
            this.SHARYOU_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_NAME_RYAKU.RegistCheckMethod")));
            this.SHARYOU_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.SHARYOU_NAME_RYAKU.TabIndex = 826;
            this.SHARYOU_NAME_RYAKU.TabStop = false;
            this.SHARYOU_NAME_RYAKU.Tag = " ";
            // 
            // UNPAN_GYOUSHA_CD
            // 
            this.UNPAN_GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
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
            this.UNPAN_GYOUSHA_CD.Location = new System.Drawing.Point(436, 44);
            this.UNPAN_GYOUSHA_CD.MaxLength = 6;
            this.UNPAN_GYOUSHA_CD.Name = "UNPAN_GYOUSHA_CD";
            this.UNPAN_GYOUSHA_CD.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_CD.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_CD.PopupSetFormField = "UNPAN_GYOUSHA_CD, UNPAN_GYOUSHA_NAME";
            this.UNPAN_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNPAN_GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.UNPAN_GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.popupWindowSetting")));
            this.UNPAN_GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_CD.SetFormField = "UNPAN_GYOUSHA_CD, UNPAN_GYOUSHA_NAME";
            this.UNPAN_GYOUSHA_CD.Size = new System.Drawing.Size(47, 20);
            this.UNPAN_GYOUSHA_CD.TabIndex = 828;
            this.UNPAN_GYOUSHA_CD.Tag = "運搬業者を指定してください";
            this.UNPAN_GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // SHARYOU_CD
            // 
            this.SHARYOU_CD.BackColor = System.Drawing.SystemColors.Window;
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
            this.SHARYOU_CD.DisplayPopUp = null;
            this.SHARYOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_CD.FocusOutCheckMethod")));
            this.SHARYOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHARYOU_CD.ForeColor = System.Drawing.Color.Black;
            this.SHARYOU_CD.GetCodeMasterField = "SHARYOU_CD, SHARYOU_NAME_RYAKU";
            this.SHARYOU_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHARYOU_CD.IsInputErrorOccured = false;
            this.SHARYOU_CD.ItemDefinedTypes = "varchar";
            this.SHARYOU_CD.Location = new System.Drawing.Point(436, 22);
            this.SHARYOU_CD.MaxLength = 6;
            this.SHARYOU_CD.Name = "SHARYOU_CD";
            this.SHARYOU_CD.PopupAfterExecute = null;
            this.SHARYOU_CD.PopupBeforeExecute = null;
            this.SHARYOU_CD.PopupGetMasterField = "SHARYOU_CD, SHARYOU_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU, SHASHU_CD, SHASHU" +
    "_NAME_RYAKU, SHAIN_CD, SHAIN_NAME_RYAKU";
            this.SHARYOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHARYOU_CD.PopupSearchSendParams")));
            this.SHARYOU_CD.PopupSetFormField = "SHARYOU_CD, SHARYOU_NAME_RYAKU, UNPAN_GYOUSHA_CD, UNPAN_GYOUSHA_NAME, txtSHASHU_C" +
    "D,txtSHASHU_NAME,txtSHAIN_CD,txtSHAIN_NAME";
            this.SHARYOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHARYOU;
            this.SHARYOU_CD.PopupWindowName = "車両選択共通ポップアップ";
            this.SHARYOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHARYOU_CD.popupWindowSetting")));
            this.SHARYOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_CD.RegistCheckMethod")));
            this.SHARYOU_CD.SetFormField = "SHARYOU_CD, SHARYOU_NAME_RYAKU";
            this.SHARYOU_CD.Size = new System.Drawing.Size(47, 20);
            this.SHARYOU_CD.TabIndex = 825;
            this.SHARYOU_CD.Tag = "車輌を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHARYOU_CD.ZeroPaddengFlag = true;
            this.SHARYOU_CD.Enter += new System.EventHandler(this.SHARYOU_CD_Enter);
            this.SHARYOU_CD.Validating += new System.ComponentModel.CancelEventHandler(this.SHARYOU_CD_Validating);
            // 
            // lblSyasyu
            // 
            this.lblSyasyu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblSyasyu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSyasyu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSyasyu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblSyasyu.ForeColor = System.Drawing.Color.White;
            this.lblSyasyu.Location = new System.Drawing.Point(321, 0);
            this.lblSyasyu.Name = "lblSyasyu";
            this.lblSyasyu.Size = new System.Drawing.Size(110, 20);
            this.lblSyasyu.TabIndex = 821;
            this.lblSyasyu.Text = "車種";
            this.lblSyasyu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCourse
            // 
            this.lblCourse.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblCourse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCourse.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblCourse.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblCourse.ForeColor = System.Drawing.Color.White;
            this.lblCourse.Location = new System.Drawing.Point(0, 0);
            this.lblCourse.Name = "lblCourse";
            this.lblCourse.Size = new System.Drawing.Size(110, 20);
            this.lblCourse.TabIndex = 820;
            this.lblCourse.Text = "コース";
            this.lblCourse.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSyaL
            // 
            this.lblSyaL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblSyaL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSyaL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblSyaL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblSyaL.ForeColor = System.Drawing.Color.White;
            this.lblSyaL.Location = new System.Drawing.Point(321, 22);
            this.lblSyaL.Name = "lblSyaL";
            this.lblSyaL.Size = new System.Drawing.Size(110, 20);
            this.lblSyaL.TabIndex = 823;
            this.lblSyaL.Text = "車輌";
            this.lblSyaL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbUntensya
            // 
            this.lbUntensya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbUntensya.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbUntensya.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbUntensya.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbUntensya.ForeColor = System.Drawing.Color.White;
            this.lbUntensya.Location = new System.Drawing.Point(0, 22);
            this.lbUntensya.Name = "lbUntensya";
            this.lbUntensya.Size = new System.Drawing.Size(110, 20);
            this.lbUntensya.TabIndex = 822;
            this.lbUntensya.Text = "運転者";
            this.lbUntensya.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbHojyosya
            // 
            this.lbHojyosya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbHojyosya.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbHojyosya.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbHojyosya.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbHojyosya.ForeColor = System.Drawing.Color.White;
            this.lbHojyosya.Location = new System.Drawing.Point(0, 44);
            this.lbHojyosya.Name = "lbHojyosya";
            this.lbHojyosya.Size = new System.Drawing.Size(110, 20);
            this.lbHojyosya.TabIndex = 824;
            this.lbHojyosya.Text = "補助員";
            this.lbHojyosya.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSHAIN_NAME
            // 
            this.txtSHAIN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtSHAIN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSHAIN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtSHAIN_NAME.DisplayPopUp = null;
            this.txtSHAIN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHAIN_NAME.FocusOutCheckMethod")));
            this.txtSHAIN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtSHAIN_NAME.ForeColor = System.Drawing.Color.Black;
            this.txtSHAIN_NAME.IsInputErrorOccured = false;
            this.txtSHAIN_NAME.Location = new System.Drawing.Point(161, 22);
            this.txtSHAIN_NAME.Name = "txtSHAIN_NAME";
            this.txtSHAIN_NAME.PopupAfterExecute = null;
            this.txtSHAIN_NAME.PopupBeforeExecute = null;
            this.txtSHAIN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtSHAIN_NAME.PopupSearchSendParams")));
            this.txtSHAIN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtSHAIN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtSHAIN_NAME.popupWindowSetting")));
            this.txtSHAIN_NAME.ReadOnly = true;
            this.txtSHAIN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHAIN_NAME.RegistCheckMethod")));
            this.txtSHAIN_NAME.Size = new System.Drawing.Size(150, 20);
            this.txtSHAIN_NAME.TabIndex = 818;
            this.txtSHAIN_NAME.TabStop = false;
            this.txtSHAIN_NAME.Tag = " ";
            // 
            // txtSHoujyosyaNm
            // 
            this.txtSHoujyosyaNm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtSHoujyosyaNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSHoujyosyaNm.DBFieldsName = "SHAIN_NAME_RYAKU";
            this.txtSHoujyosyaNm.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtSHoujyosyaNm.DisplayPopUp = null;
            this.txtSHoujyosyaNm.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHoujyosyaNm.FocusOutCheckMethod")));
            this.txtSHoujyosyaNm.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtSHoujyosyaNm.ForeColor = System.Drawing.Color.Black;
            this.txtSHoujyosyaNm.IsInputErrorOccured = false;
            this.txtSHoujyosyaNm.Location = new System.Drawing.Point(161, 44);
            this.txtSHoujyosyaNm.Name = "txtSHoujyosyaNm";
            this.txtSHoujyosyaNm.PopupAfterExecute = null;
            this.txtSHoujyosyaNm.PopupBeforeExecute = null;
            this.txtSHoujyosyaNm.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtSHoujyosyaNm.PopupSearchSendParams")));
            this.txtSHoujyosyaNm.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtSHoujyosyaNm.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtSHoujyosyaNm.popupWindowSetting")));
            this.txtSHoujyosyaNm.ReadOnly = true;
            this.txtSHoujyosyaNm.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHoujyosyaNm.RegistCheckMethod")));
            this.txtSHoujyosyaNm.Size = new System.Drawing.Size(150, 20);
            this.txtSHoujyosyaNm.TabIndex = 819;
            this.txtSHoujyosyaNm.TabStop = false;
            this.txtSHoujyosyaNm.Tag = " ";
            // 
            // txtSHASHU_NAME
            // 
            this.txtSHASHU_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtSHASHU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSHASHU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtSHASHU_NAME.DisplayPopUp = null;
            this.txtSHASHU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHASHU_NAME.FocusOutCheckMethod")));
            this.txtSHASHU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtSHASHU_NAME.ForeColor = System.Drawing.Color.Black;
            this.txtSHASHU_NAME.IsInputErrorOccured = false;
            this.txtSHASHU_NAME.Location = new System.Drawing.Point(482, 0);
            this.txtSHASHU_NAME.Name = "txtSHASHU_NAME";
            this.txtSHASHU_NAME.PopupAfterExecute = null;
            this.txtSHASHU_NAME.PopupBeforeExecute = null;
            this.txtSHASHU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtSHASHU_NAME.PopupSearchSendParams")));
            this.txtSHASHU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtSHASHU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtSHASHU_NAME.popupWindowSetting")));
            this.txtSHASHU_NAME.ReadOnly = true;
            this.txtSHASHU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHASHU_NAME.RegistCheckMethod")));
            this.txtSHASHU_NAME.Size = new System.Drawing.Size(285, 20);
            this.txtSHASHU_NAME.TabIndex = 816;
            this.txtSHASHU_NAME.TabStop = false;
            this.txtSHASHU_NAME.Tag = " ";
            // 
            // txtSHoujyosyaCd
            // 
            this.txtSHoujyosyaCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtSHoujyosyaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSHoujyosyaCd.ChangeUpperCase = true;
            this.txtSHoujyosyaCd.CharacterLimitList = null;
            this.txtSHoujyosyaCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.txtSHoujyosyaCd.DBFieldsName = "SHAIN_CD";
            this.txtSHoujyosyaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtSHoujyosyaCd.DisplayItemName = "補助員";
            this.txtSHoujyosyaCd.DisplayPopUp = null;
            this.txtSHoujyosyaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHoujyosyaCd.FocusOutCheckMethod")));
            this.txtSHoujyosyaCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtSHoujyosyaCd.ForeColor = System.Drawing.Color.Black;
            this.txtSHoujyosyaCd.GetCodeMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.txtSHoujyosyaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtSHoujyosyaCd.IsInputErrorOccured = false;
            this.txtSHoujyosyaCd.ItemDefinedTypes = "varchar";
            this.txtSHoujyosyaCd.Location = new System.Drawing.Point(115, 44);
            this.txtSHoujyosyaCd.MaxLength = 6;
            this.txtSHoujyosyaCd.Name = "txtSHoujyosyaCd";
            this.txtSHoujyosyaCd.PopupAfterExecute = null;
            this.txtSHoujyosyaCd.PopupBeforeExecute = null;
            this.txtSHoujyosyaCd.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.txtSHoujyosyaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtSHoujyosyaCd.PopupSearchSendParams")));
            this.txtSHoujyosyaCd.PopupSetFormField = "txtSHoujyosyaCd,txtSHoujyosyaNm";
            this.txtSHoujyosyaCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.txtSHoujyosyaCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtSHoujyosyaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtSHoujyosyaCd.popupWindowSetting")));
            this.txtSHoujyosyaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHoujyosyaCd.RegistCheckMethod")));
            this.txtSHoujyosyaCd.SetFormField = "txtSHoujyosyaCd,txtSHoujyosyaNm";
            this.txtSHoujyosyaCd.ShortItemName = "補助員CD";
            this.txtSHoujyosyaCd.Size = new System.Drawing.Size(47, 20);
            this.txtSHoujyosyaCd.TabIndex = 814;
            this.txtSHoujyosyaCd.Tag = "補助員CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtSHoujyosyaCd.ZeroPaddengFlag = true;
            // 
            // txtCourseNm
            // 
            this.txtCourseNm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtCourseNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCourseNm.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtCourseNm.DisplayPopUp = null;
            this.txtCourseNm.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCourseNm.FocusOutCheckMethod")));
            this.txtCourseNm.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtCourseNm.ForeColor = System.Drawing.Color.Black;
            this.txtCourseNm.IsInputErrorOccured = false;
            this.txtCourseNm.Location = new System.Drawing.Point(161, 0);
            this.txtCourseNm.Name = "txtCourseNm";
            this.txtCourseNm.PopupAfterExecute = null;
            this.txtCourseNm.PopupBeforeExecute = null;
            this.txtCourseNm.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtCourseNm.PopupSearchSendParams")));
            this.txtCourseNm.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtCourseNm.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtCourseNm.popupWindowSetting")));
            this.txtCourseNm.ReadOnly = true;
            this.txtCourseNm.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCourseNm.RegistCheckMethod")));
            this.txtCourseNm.Size = new System.Drawing.Size(150, 20);
            this.txtCourseNm.TabIndex = 815;
            this.txtCourseNm.TabStop = false;
            this.txtCourseNm.Tag = " ";
            // 
            // txtSHASHU_CD
            // 
            this.txtSHASHU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.txtSHASHU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSHASHU_CD.ChangeUpperCase = true;
            this.txtSHASHU_CD.CharacterLimitList = null;
            this.txtSHASHU_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.txtSHASHU_CD.DBFieldsName = "ZAIKO_HINMEI_CD";
            this.txtSHASHU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtSHASHU_CD.DisplayItemName = "車種";
            this.txtSHASHU_CD.DisplayPopUp = null;
            this.txtSHASHU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHASHU_CD.FocusOutCheckMethod")));
            this.txtSHASHU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtSHASHU_CD.ForeColor = System.Drawing.Color.Black;
            this.txtSHASHU_CD.GetCodeMasterField = "SHASHU_CD,SHASHU_NAME_RYAKU";
            this.txtSHASHU_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtSHASHU_CD.IsInputErrorOccured = false;
            this.txtSHASHU_CD.ItemDefinedTypes = "varchar";
            this.txtSHASHU_CD.Location = new System.Drawing.Point(436, 0);
            this.txtSHASHU_CD.MaxLength = 3;
            this.txtSHASHU_CD.Name = "txtSHASHU_CD";
            this.txtSHASHU_CD.PopupAfterExecute = null;
            this.txtSHASHU_CD.PopupBeforeExecute = null;
            this.txtSHASHU_CD.PopupGetMasterField = "SHASHU_CD,SHASHU_NAME_RYAKU";
            this.txtSHASHU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtSHASHU_CD.PopupSearchSendParams")));
            this.txtSHASHU_CD.PopupSetFormField = "txtSHASHU_CD,txtSHASHU_NAME";
            this.txtSHASHU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHASHU;
            this.txtSHASHU_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.txtSHASHU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtSHASHU_CD.popupWindowSetting")));
            this.txtSHASHU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHASHU_CD.RegistCheckMethod")));
            this.txtSHASHU_CD.SetFormField = "txtSHASHU_CD,txtSHASHU_NAME";
            this.txtSHASHU_CD.ShortItemName = "車種CD";
            this.txtSHASHU_CD.Size = new System.Drawing.Size(47, 20);
            this.txtSHASHU_CD.TabIndex = 820;
            this.txtSHASHU_CD.Tag = "車種を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtSHASHU_CD.ZeroPaddengFlag = true;
            // 
            // txtSHAIN_CD
            // 
            this.txtSHAIN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.txtSHAIN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSHAIN_CD.ChangeUpperCase = true;
            this.txtSHAIN_CD.CharacterLimitList = null;
            this.txtSHAIN_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.txtSHAIN_CD.DBFieldsName = "SHAIN_CD";
            this.txtSHAIN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtSHAIN_CD.DisplayItemName = "運転者";
            this.txtSHAIN_CD.DisplayPopUp = null;
            this.txtSHAIN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHAIN_CD.FocusOutCheckMethod")));
            this.txtSHAIN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtSHAIN_CD.ForeColor = System.Drawing.Color.Black;
            this.txtSHAIN_CD.GetCodeMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.txtSHAIN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtSHAIN_CD.IsInputErrorOccured = false;
            this.txtSHAIN_CD.ItemDefinedTypes = "varchar";
            this.txtSHAIN_CD.Location = new System.Drawing.Point(115, 22);
            this.txtSHAIN_CD.MaxLength = 6;
            this.txtSHAIN_CD.Name = "txtSHAIN_CD";
            this.txtSHAIN_CD.PopupAfterExecute = null;
            this.txtSHAIN_CD.PopupBeforeExecute = null;
            this.txtSHAIN_CD.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.txtSHAIN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtSHAIN_CD.PopupSearchSendParams")));
            this.txtSHAIN_CD.PopupSendParams = new string[0];
            this.txtSHAIN_CD.PopupSetFormField = "txtSHAIN_CD,txtSHAIN_NAME";
            this.txtSHAIN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.txtSHAIN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.txtSHAIN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtSHAIN_CD.popupWindowSetting")));
            this.txtSHAIN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtSHAIN_CD.RegistCheckMethod")));
            this.txtSHAIN_CD.SetFormField = "txtSHAIN_CD,txtSHAIN_NAME";
            this.txtSHAIN_CD.ShortItemName = "運転者CD";
            this.txtSHAIN_CD.Size = new System.Drawing.Size(47, 20);
            this.txtSHAIN_CD.TabIndex = 813;
            this.txtSHAIN_CD.Tag = "運転者CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtSHAIN_CD.ZeroPaddengFlag = true;
            // 
            // txtCourseCd
            // 
            this.txtCourseCd.BackColor = System.Drawing.SystemColors.Window;
            this.txtCourseCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCourseCd.ChangeUpperCase = true;
            this.txtCourseCd.CharacterLimitList = null;
            this.txtCourseCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.txtCourseCd.DBFieldsName = "COURSE_NAME_CD";
            this.txtCourseCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtCourseCd.DisplayItemName = "コース";
            this.txtCourseCd.DisplayPopUp = null;
            this.txtCourseCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCourseCd.FocusOutCheckMethod")));
            this.txtCourseCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtCourseCd.ForeColor = System.Drawing.Color.Black;
            this.txtCourseCd.GetCodeMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAK";
            this.txtCourseCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtCourseCd.IsInputErrorOccured = false;
            this.txtCourseCd.ItemDefinedTypes = "varchar";
            this.txtCourseCd.Location = new System.Drawing.Point(115, 0);
            this.txtCourseCd.MaxLength = 6;
            this.txtCourseCd.Name = "txtCourseCd";
            this.txtCourseCd.PopupAfterExecute = null;
            this.txtCourseCd.PopupBeforeExecute = null;
            this.txtCourseCd.PopupGetMasterField = "COURSE_NAME_CD,COURSE_NAME_RYAKU";
            this.txtCourseCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtCourseCd.PopupSearchSendParams")));
            this.txtCourseCd.PopupSendParams = new string[0];
            this.txtCourseCd.PopupSetFormField = "txtCourseCd,txtCourseNm";
            this.txtCourseCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_COURSE_NAME;
            this.txtCourseCd.PopupWindowName = "マスタ共通ポップアップ";
            this.txtCourseCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtCourseCd.popupWindowSetting")));
            this.txtCourseCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtCourseCd.RegistCheckMethod")));
            this.txtCourseCd.SetFormField = "txtCourseCd,txtCourseNm";
            this.txtCourseCd.ShortItemName = "コースCD";
            this.txtCourseCd.Size = new System.Drawing.Size(47, 20);
            this.txtCourseCd.TabIndex = 811;
            this.txtCourseCd.Tag = "コースCDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.txtCourseCd.ZeroPaddengFlag = true;
            this.txtCourseCd.Enter += new System.EventHandler(this.txtCourseCd_Enter);
            this.txtCourseCd.Validating += new System.ComponentModel.CancelEventHandler(this.txtCourseCd_Validating);
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(260, 104);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 831;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 490);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.pnlSearchString);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F);
            this.Name = "UIForm";
            this.Text = "";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.pnlSearchString, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.pnlSearchString.ResumeLayout(false);
            this.pnlSearchString.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label lblSyasyu;
        private Label lblCourse;
        private Label lblSyaL;
        private Label lbUntensya;
        private Label lbHojyosya;
        internal r_framework.CustomControl.CustomTextBox txtSHAIN_NAME;
        internal r_framework.CustomControl.CustomTextBox txtSHoujyosyaNm;
        internal r_framework.CustomControl.CustomTextBox txtSHASHU_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox txtSHoujyosyaCd;
        internal r_framework.CustomControl.CustomTextBox txtCourseNm;
        internal r_framework.CustomControl.CustomAlphaNumTextBox txtSHASHU_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox txtSHAIN_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox txtCourseCd;
        internal r_framework.CustomControl.CustomPanel pnlSearchString;
        internal r_framework.CustomControl.CustomTextBox SHARYOU_NAME_RYAKU;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHARYOU_CD;
        private Label lblUnpanGyousha;
        internal r_framework.CustomControl.CustomTextBox UNPAN_GYOUSHA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox UNPAN_GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;

    }
}