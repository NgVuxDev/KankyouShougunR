// $Id: UIForm.Designer.cs 55371 2015-07-10 11:07:15Z t-thanhson@e-mall.co.jp $
using System.Windows.Forms;
using System;

namespace Shougun.Core.Reception.UketukeiIchiran
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
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.pnlSearchString = new r_framework.CustomControl.CustomPanel();
            this.cmbShihariaShimebi = new r_framework.CustomControl.CustomComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbShimebi = new r_framework.CustomControl.CustomComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.testNIOROSHI_GYOUSHA_CD = new r_framework.CustomControl.CustomTextBox();
            this.testGYOUSHA_CD = new r_framework.CustomControl.CustomTextBox();
            this.testNIOROSHI_GENBA_CD = new r_framework.CustomControl.CustomTextBox();
            this.testGENBA_CD = new r_framework.CustomControl.CustomTextBox();
            this.NIOROSHI_GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.NIOROSHI_GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.NIOROSHI_GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.NIOROSHI_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.UNPAN_GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.UNPAN_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.labelNioGyo = new System.Windows.Forms.Label();
            this.lable2 = new System.Windows.Forms.Label();
            this.lableNioGen = new System.Windows.Forms.Label();
            this.GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lable5 = new System.Windows.Forms.Label();
            this.GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lable3 = new System.Windows.Forms.Label();
            this.lable1 = new System.Windows.Forms.Label();
            this.panel_DenpyouSyurui = new r_framework.CustomControl.CustomPanel();
            this.lblHaishaJyokyou = new System.Windows.Forms.Label();
            this.HAISHA_JOKYO_NAME = new r_framework.CustomControl.CustomTextBox();
            this.HAISHA_JOKYO_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lblHaishaSyurui = new System.Windows.Forms.Label();
            this.HAISHA_SHURUI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.HAISHA_SHURUI_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtNum_DenPyouSyurui = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.radbtnSsMk = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnSsSk = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnSyuusyuu = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnKuremu = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnSyukka = new r_framework.CustomControl.CustomRadioButton();
            this.radbtnMotikomi = new r_framework.CustomControl.CustomRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.UKETSUKE_EXPORT_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.UKETSUKE_EXPORT_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.UKETSUKE_EXPORT_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.UKETSUKE_EXPORT_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.pnlSearchString.SuspendLayout();
            this.panel_DenpyouSyurui.SuspendLayout();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.Enabled = false;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Location = new System.Drawing.Point(0, 0);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(687, 90);
            this.searchString.TabStop = false;
            this.searchString.Tag = "検索条件設定画面で設定した条件を表示します";
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(0, 408);
            this.bt_ptn1.TabIndex = 61;
            this.bt_ptn1.Text = "パターン設定なし";
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(200, 408);
            this.bt_ptn2.TabIndex = 62;
            this.bt_ptn2.Text = "パターン設定なし";
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(400, 408);
            this.bt_ptn3.TabIndex = 63;
            this.bt_ptn3.Text = "パターン設定なし";
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(600, 408);
            this.bt_ptn4.TabIndex = 64;
            this.bt_ptn4.Text = "パターン設定なし";
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(800, 408);
            this.bt_ptn5.TabIndex = 65;
            this.bt_ptn5.Text = "パターン設定なし";
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(0, 158);
            this.customSortHeader1.TabIndex = 29;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(4, 158);
            // 
            // pnlSearchString
            // 
            this.pnlSearchString.Controls.Add(this.UKETSUKE_EXPORT_KBN);
            this.pnlSearchString.Controls.Add(this.customPanel2);
            this.pnlSearchString.Controls.Add(this.label3);
            this.pnlSearchString.Controls.Add(this.cmbShihariaShimebi);
            this.pnlSearchString.Controls.Add(this.label2);
            this.pnlSearchString.Controls.Add(this.cmbShimebi);
            this.pnlSearchString.Controls.Add(this.label4);
            this.pnlSearchString.Controls.Add(this.testNIOROSHI_GYOUSHA_CD);
            this.pnlSearchString.Controls.Add(this.testGYOUSHA_CD);
            this.pnlSearchString.Controls.Add(this.testNIOROSHI_GENBA_CD);
            this.pnlSearchString.Controls.Add(this.testGENBA_CD);
            this.pnlSearchString.Controls.Add(this.NIOROSHI_GENBA_NAME);
            this.pnlSearchString.Controls.Add(this.NIOROSHI_GENBA_CD);
            this.pnlSearchString.Controls.Add(this.NIOROSHI_GYOUSHA_NAME);
            this.pnlSearchString.Controls.Add(this.NIOROSHI_GYOUSHA_CD);
            this.pnlSearchString.Controls.Add(this.UNPAN_GYOUSHA_NAME);
            this.pnlSearchString.Controls.Add(this.UNPAN_GYOUSHA_CD);
            this.pnlSearchString.Controls.Add(this.labelNioGyo);
            this.pnlSearchString.Controls.Add(this.lable2);
            this.pnlSearchString.Controls.Add(this.lableNioGen);
            this.pnlSearchString.Controls.Add(this.GENBA_NAME);
            this.pnlSearchString.Controls.Add(this.GENBA_CD);
            this.pnlSearchString.Controls.Add(this.lable5);
            this.pnlSearchString.Controls.Add(this.GYOUSHA_NAME);
            this.pnlSearchString.Controls.Add(this.GYOUSHA_CD);
            this.pnlSearchString.Controls.Add(this.TORIHIKISAKI_NAME);
            this.pnlSearchString.Controls.Add(this.TORIHIKISAKI_CD);
            this.pnlSearchString.Controls.Add(this.lable3);
            this.pnlSearchString.Controls.Add(this.lable1);
            this.pnlSearchString.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchString.Name = "pnlSearchString";
            this.pnlSearchString.Size = new System.Drawing.Size(687, 155);
            this.pnlSearchString.TabIndex = 10;
            // 
            // cmbShihariaShimebi
            // 
            this.cmbShihariaShimebi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbShihariaShimebi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbShihariaShimebi.BackColor = System.Drawing.SystemColors.Window;
            this.cmbShihariaShimebi.DefaultBackColor = System.Drawing.Color.Empty;
            this.cmbShihariaShimebi.DisplayPopUp = null;
            this.cmbShihariaShimebi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShihariaShimebi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmbShihariaShimebi.FocusOutCheckMethod")));
            this.cmbShihariaShimebi.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbShihariaShimebi.FormattingEnabled = true;
            this.cmbShihariaShimebi.IsInputErrorOccured = false;
            this.cmbShihariaShimebi.Items.AddRange(new object[] {
            "",
            "0",
            "5",
            "10",
            "15",
            "20",
            "25",
            "31"});
            this.cmbShihariaShimebi.Location = new System.Drawing.Point(275, 0);
            this.cmbShihariaShimebi.Name = "cmbShihariaShimebi";
            this.cmbShihariaShimebi.PopupAfterExecute = null;
            this.cmbShihariaShimebi.PopupBeforeExecute = null;
            this.cmbShihariaShimebi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cmbShihariaShimebi.PopupSearchSendParams")));
            this.cmbShihariaShimebi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cmbShihariaShimebi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cmbShihariaShimebi.popupWindowSetting")));
            this.cmbShihariaShimebi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmbShihariaShimebi.RegistCheckMethod")));
            this.cmbShihariaShimebi.Size = new System.Drawing.Size(40, 21);
            this.cmbShihariaShimebi.TabIndex = 12;
            this.cmbShihariaShimebi.Tag = "支払締日を入力してください";
            this.cmbShihariaShimebi.SelectedIndexChanged += new System.EventHandler(this.cmbShihariaShimebi_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(160, 1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 531;
            this.label2.Text = "支払締日";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cmbShimebi
            // 
            this.cmbShimebi.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cmbShimebi.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbShimebi.BackColor = System.Drawing.SystemColors.Window;
            this.cmbShimebi.DefaultBackColor = System.Drawing.Color.Empty;
            this.cmbShimebi.DisplayPopUp = null;
            this.cmbShimebi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbShimebi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmbShimebi.FocusOutCheckMethod")));
            this.cmbShimebi.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cmbShimebi.FormattingEnabled = true;
            this.cmbShimebi.IsInputErrorOccured = false;
            this.cmbShimebi.Items.AddRange(new object[] {
            "",
            "0",
            "5",
            "10",
            "15",
            "20",
            "25",
            "31"});
            this.cmbShimebi.Location = new System.Drawing.Point(115, 0);
            this.cmbShimebi.Name = "cmbShimebi";
            this.cmbShimebi.PopupAfterExecute = null;
            this.cmbShimebi.PopupBeforeExecute = null;
            this.cmbShimebi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cmbShimebi.PopupSearchSendParams")));
            this.cmbShimebi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cmbShimebi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cmbShimebi.popupWindowSetting")));
            this.cmbShimebi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cmbShimebi.RegistCheckMethod")));
            this.cmbShimebi.Size = new System.Drawing.Size(40, 21);
            this.cmbShimebi.TabIndex = 11;
            this.cmbShimebi.Tag = "請求締日を入力してください";
            this.cmbShimebi.SelectedIndexChanged += new System.EventHandler(this.shimebiChanged);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 529;
            this.label4.Text = "請求締日";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // testNIOROSHI_GYOUSHA_CD
            // 
            this.testNIOROSHI_GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.testNIOROSHI_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.testNIOROSHI_GYOUSHA_CD.DisplayPopUp = null;
            this.testNIOROSHI_GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testNIOROSHI_GYOUSHA_CD.FocusOutCheckMethod")));
            this.testNIOROSHI_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.testNIOROSHI_GYOUSHA_CD.IsInputErrorOccured = false;
            this.testNIOROSHI_GYOUSHA_CD.Location = new System.Drawing.Point(452, 112);
            this.testNIOROSHI_GYOUSHA_CD.Name = "testNIOROSHI_GYOUSHA_CD";
            this.testNIOROSHI_GYOUSHA_CD.PopupAfterExecute = null;
            this.testNIOROSHI_GYOUSHA_CD.PopupBeforeExecute = null;
            this.testNIOROSHI_GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("testNIOROSHI_GYOUSHA_CD.PopupSearchSendParams")));
            this.testNIOROSHI_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.testNIOROSHI_GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("testNIOROSHI_GYOUSHA_CD.popupWindowSetting")));
            this.testNIOROSHI_GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testNIOROSHI_GYOUSHA_CD.RegistCheckMethod")));
            this.testNIOROSHI_GYOUSHA_CD.Size = new System.Drawing.Size(11, 20);
            this.testNIOROSHI_GYOUSHA_CD.TabIndex = 25;
            this.testNIOROSHI_GYOUSHA_CD.Visible = false;
            // 
            // testGYOUSHA_CD
            // 
            this.testGYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.testGYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.testGYOUSHA_CD.DisplayPopUp = null;
            this.testGYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testGYOUSHA_CD.FocusOutCheckMethod")));
            this.testGYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.testGYOUSHA_CD.IsInputErrorOccured = false;
            this.testGYOUSHA_CD.Location = new System.Drawing.Point(453, 46);
            this.testGYOUSHA_CD.Name = "testGYOUSHA_CD";
            this.testGYOUSHA_CD.PopupAfterExecute = null;
            this.testGYOUSHA_CD.PopupBeforeExecute = null;
            this.testGYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("testGYOUSHA_CD.PopupSearchSendParams")));
            this.testGYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.testGYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("testGYOUSHA_CD.popupWindowSetting")));
            this.testGYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testGYOUSHA_CD.RegistCheckMethod")));
            this.testGYOUSHA_CD.Size = new System.Drawing.Size(11, 20);
            this.testGYOUSHA_CD.TabIndex = 16;
            this.testGYOUSHA_CD.Visible = false;
            // 
            // testNIOROSHI_GENBA_CD
            // 
            this.testNIOROSHI_GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.testNIOROSHI_GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.testNIOROSHI_GENBA_CD.DisplayPopUp = null;
            this.testNIOROSHI_GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testNIOROSHI_GENBA_CD.FocusOutCheckMethod")));
            this.testNIOROSHI_GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.testNIOROSHI_GENBA_CD.IsInputErrorOccured = false;
            this.testNIOROSHI_GENBA_CD.Location = new System.Drawing.Point(453, 133);
            this.testNIOROSHI_GENBA_CD.Name = "testNIOROSHI_GENBA_CD";
            this.testNIOROSHI_GENBA_CD.PopupAfterExecute = null;
            this.testNIOROSHI_GENBA_CD.PopupBeforeExecute = null;
            this.testNIOROSHI_GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("testNIOROSHI_GENBA_CD.PopupSearchSendParams")));
            this.testNIOROSHI_GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.testNIOROSHI_GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("testNIOROSHI_GENBA_CD.popupWindowSetting")));
            this.testNIOROSHI_GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testNIOROSHI_GENBA_CD.RegistCheckMethod")));
            this.testNIOROSHI_GENBA_CD.Size = new System.Drawing.Size(10, 20);
            this.testNIOROSHI_GENBA_CD.TabIndex = 28;
            this.testNIOROSHI_GENBA_CD.Visible = false;
            // 
            // testGENBA_CD
            // 
            this.testGENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.testGENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.testGENBA_CD.DisplayPopUp = null;
            this.testGENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testGENBA_CD.FocusOutCheckMethod")));
            this.testGENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.testGENBA_CD.IsInputErrorOccured = false;
            this.testGENBA_CD.Location = new System.Drawing.Point(454, 69);
            this.testGENBA_CD.Name = "testGENBA_CD";
            this.testGENBA_CD.PopupAfterExecute = null;
            this.testGENBA_CD.PopupBeforeExecute = null;
            this.testGENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("testGENBA_CD.PopupSearchSendParams")));
            this.testGENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.testGENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("testGENBA_CD.popupWindowSetting")));
            this.testGENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("testGENBA_CD.RegistCheckMethod")));
            this.testGENBA_CD.Size = new System.Drawing.Size(10, 20);
            this.testGENBA_CD.TabIndex = 19;
            this.testGENBA_CD.Visible = false;
            // 
            // NIOROSHI_GENBA_NAME
            // 
            this.NIOROSHI_GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.NIOROSHI_GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIOROSHI_GENBA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.NIOROSHI_GENBA_NAME.DBFieldsName = "NIOROSHI_GENBA_NAME";
            this.NIOROSHI_GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIOROSHI_GENBA_NAME.DisplayPopUp = null;
            this.NIOROSHI_GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GENBA_NAME.FocusOutCheckMethod")));
            this.NIOROSHI_GENBA_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NIOROSHI_GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.NIOROSHI_GENBA_NAME.IsInputErrorOccured = false;
            this.NIOROSHI_GENBA_NAME.Location = new System.Drawing.Point(164, 134);
            this.NIOROSHI_GENBA_NAME.MaxLength = 0;
            this.NIOROSHI_GENBA_NAME.Name = "NIOROSHI_GENBA_NAME";
            this.NIOROSHI_GENBA_NAME.PopupAfterExecute = null;
            this.NIOROSHI_GENBA_NAME.PopupBeforeExecute = null;
            this.NIOROSHI_GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIOROSHI_GENBA_NAME.PopupSearchSendParams")));
            this.NIOROSHI_GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NIOROSHI_GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIOROSHI_GENBA_NAME.popupWindowSetting")));
            this.NIOROSHI_GENBA_NAME.ReadOnly = true;
            this.NIOROSHI_GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GENBA_NAME.RegistCheckMethod")));
            this.NIOROSHI_GENBA_NAME.Size = new System.Drawing.Size(286, 20);
            this.NIOROSHI_GENBA_NAME.TabIndex = 72;
            this.NIOROSHI_GENBA_NAME.TabStop = false;
            this.NIOROSHI_GENBA_NAME.Tag = " ";
            // 
            // NIOROSHI_GENBA_CD
            // 
            this.NIOROSHI_GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.NIOROSHI_GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIOROSHI_GENBA_CD.ChangeUpperCase = true;
            this.NIOROSHI_GENBA_CD.CharacterLimitList = null;
            this.NIOROSHI_GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NIOROSHI_GENBA_CD.DBFieldsName = "NIOROSHI_GENBA_CD";
            this.NIOROSHI_GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIOROSHI_GENBA_CD.DisplayItemName = "荷降現場";
            this.NIOROSHI_GENBA_CD.DisplayPopUp = null;
            this.NIOROSHI_GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GENBA_CD.FocusOutCheckMethod")));
            this.NIOROSHI_GENBA_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NIOROSHI_GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.NIOROSHI_GENBA_CD.GetCodeMasterField = "";
            this.NIOROSHI_GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NIOROSHI_GENBA_CD.IsInputErrorOccured = false;
            this.NIOROSHI_GENBA_CD.ItemDefinedTypes = "varchar";
            this.NIOROSHI_GENBA_CD.Location = new System.Drawing.Point(115, 134);
            this.NIOROSHI_GENBA_CD.MaxLength = 6;
            this.NIOROSHI_GENBA_CD.Name = "NIOROSHI_GENBA_CD";
            this.NIOROSHI_GENBA_CD.PopupAfterExecute = null;
            this.NIOROSHI_GENBA_CD.PopupBeforeExecute = null;
            this.NIOROSHI_GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GENBA_CD,GYOUSHA_CD, GYOUSHA_NAME_RYAKU,GYOUSHA_CD";
            this.NIOROSHI_GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIOROSHI_GENBA_CD.PopupSearchSendParams")));
            this.NIOROSHI_GENBA_CD.PopupSetFormField = "NIOROSHI_GENBA_CD, NIOROSHI_GENBA_NAME,testNIOROSHI_GENBA_CD,NIOROSHI_GYOUSHA_CD," +
    " NIOROSHI_GYOUSHA_NAME,testNIOROSHI_GYOUSHA_CD";
            this.NIOROSHI_GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.NIOROSHI_GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.NIOROSHI_GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIOROSHI_GENBA_CD.popupWindowSetting")));
            this.NIOROSHI_GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GENBA_CD.RegistCheckMethod")));
            this.NIOROSHI_GENBA_CD.SetFormField = "";
            this.NIOROSHI_GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.NIOROSHI_GENBA_CD.TabIndex = 71;
            this.NIOROSHI_GENBA_CD.Tag = "荷降現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.NIOROSHI_GENBA_CD.Text = "000001";
            this.NIOROSHI_GENBA_CD.ZeroPaddengFlag = true;
            this.NIOROSHI_GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.NIOROSHI_GENBA_CD_Validating);
            // 
            // NIOROSHI_GYOUSHA_NAME
            // 
            this.NIOROSHI_GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.NIOROSHI_GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIOROSHI_GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.NIOROSHI_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIOROSHI_GYOUSHA_NAME.DisplayPopUp = null;
            this.NIOROSHI_GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GYOUSHA_NAME.FocusOutCheckMethod")));
            this.NIOROSHI_GYOUSHA_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NIOROSHI_GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.NIOROSHI_GYOUSHA_NAME.IsInputErrorOccured = false;
            this.NIOROSHI_GYOUSHA_NAME.Location = new System.Drawing.Point(164, 112);
            this.NIOROSHI_GYOUSHA_NAME.MaxLength = 0;
            this.NIOROSHI_GYOUSHA_NAME.Name = "NIOROSHI_GYOUSHA_NAME";
            this.NIOROSHI_GYOUSHA_NAME.PopupAfterExecute = null;
            this.NIOROSHI_GYOUSHA_NAME.PopupBeforeExecute = null;
            this.NIOROSHI_GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIOROSHI_GYOUSHA_NAME.PopupSearchSendParams")));
            this.NIOROSHI_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NIOROSHI_GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIOROSHI_GYOUSHA_NAME.popupWindowSetting")));
            this.NIOROSHI_GYOUSHA_NAME.ReadOnly = true;
            this.NIOROSHI_GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GYOUSHA_NAME.RegistCheckMethod")));
            this.NIOROSHI_GYOUSHA_NAME.Size = new System.Drawing.Size(286, 20);
            this.NIOROSHI_GYOUSHA_NAME.TabIndex = 62;
            this.NIOROSHI_GYOUSHA_NAME.TabStop = false;
            this.NIOROSHI_GYOUSHA_NAME.Tag = " ";
            // 
            // NIOROSHI_GYOUSHA_CD
            // 
            this.NIOROSHI_GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.NIOROSHI_GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.NIOROSHI_GYOUSHA_CD.ChangeUpperCase = true;
            this.NIOROSHI_GYOUSHA_CD.CharacterLimitList = null;
            this.NIOROSHI_GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.NIOROSHI_GYOUSHA_CD.DBFieldsName = "NIOROSHI_GYOUSHA_CD";
            this.NIOROSHI_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.NIOROSHI_GYOUSHA_CD.DisplayItemName = "荷降業者";
            this.NIOROSHI_GYOUSHA_CD.DisplayPopUp = null;
            this.NIOROSHI_GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GYOUSHA_CD.FocusOutCheckMethod")));
            this.NIOROSHI_GYOUSHA_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.NIOROSHI_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.NIOROSHI_GYOUSHA_CD.GetCodeMasterField = "";
            this.NIOROSHI_GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.NIOROSHI_GYOUSHA_CD.IsInputErrorOccured = false;
            this.NIOROSHI_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.NIOROSHI_GYOUSHA_CD.Location = new System.Drawing.Point(115, 112);
            this.NIOROSHI_GYOUSHA_CD.MaxLength = 6;
            this.NIOROSHI_GYOUSHA_CD.Name = "NIOROSHI_GYOUSHA_CD";
            this.NIOROSHI_GYOUSHA_CD.PopupAfterExecute = null;
            this.NIOROSHI_GYOUSHA_CD.PopupAfterExecuteMethod = "NioroshiGyoushaCdPopUpAfter";
            this.NIOROSHI_GYOUSHA_CD.PopupBeforeExecute = null;
            this.NIOROSHI_GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.NIOROSHI_GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NIOROSHI_GYOUSHA_CD.PopupSearchSendParams")));
            this.NIOROSHI_GYOUSHA_CD.PopupSetFormField = "NIOROSHI_GYOUSHA_CD, NIOROSHI_GYOUSHA_NAME";
            this.NIOROSHI_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.NIOROSHI_GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.NIOROSHI_GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NIOROSHI_GYOUSHA_CD.popupWindowSetting")));
            this.NIOROSHI_GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NIOROSHI_GYOUSHA_CD.RegistCheckMethod")));
            this.NIOROSHI_GYOUSHA_CD.SetFormField = "";
            this.NIOROSHI_GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.NIOROSHI_GYOUSHA_CD.TabIndex = 61;
            this.NIOROSHI_GYOUSHA_CD.Tag = "荷降業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.NIOROSHI_GYOUSHA_CD.Text = "000001";
            this.NIOROSHI_GYOUSHA_CD.ZeroPaddengFlag = true;
            this.NIOROSHI_GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.NIOROSHI_GYOUSHA_CD_Validating);
            // 
            // UNPAN_GYOUSHA_NAME
            // 
            this.UNPAN_GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNPAN_GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_NAME.DBFieldsName = "";
            this.UNPAN_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_NAME.DisplayItemName = "varchar";
            this.UNPAN_GYOUSHA_NAME.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.UNPAN_GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_NAME.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_NAME.Location = new System.Drawing.Point(164, 90);
            this.UNPAN_GYOUSHA_NAME.MaxLength = 0;
            this.UNPAN_GYOUSHA_NAME.Name = "UNPAN_GYOUSHA_NAME";
            this.UNPAN_GYOUSHA_NAME.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_NAME.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.popupWindowSetting")));
            this.UNPAN_GYOUSHA_NAME.ReadOnly = true;
            this.UNPAN_GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_NAME.Size = new System.Drawing.Size(286, 20);
            this.UNPAN_GYOUSHA_NAME.TabIndex = 52;
            this.UNPAN_GYOUSHA_NAME.TabStop = false;
            this.UNPAN_GYOUSHA_NAME.Tag = " ";
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
            this.UNPAN_GYOUSHA_CD.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.UNPAN_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_CD.GetCodeMasterField = "";
            this.UNPAN_GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UNPAN_GYOUSHA_CD.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.UNPAN_GYOUSHA_CD.Location = new System.Drawing.Point(115, 90);
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
            this.UNPAN_GYOUSHA_CD.SetFormField = "";
            this.UNPAN_GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.UNPAN_GYOUSHA_CD.TabIndex = 51;
            this.UNPAN_GYOUSHA_CD.Tag = "運搬業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.UNPAN_GYOUSHA_CD.Text = "000001";
            this.UNPAN_GYOUSHA_CD.ZeroPaddengFlag = true;
            this.UNPAN_GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.UNPAN_GYOUSHA_CD_Validating);
            // 
            // labelNioGyo
            // 
            this.labelNioGyo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelNioGyo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelNioGyo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelNioGyo.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelNioGyo.ForeColor = System.Drawing.Color.White;
            this.labelNioGyo.Location = new System.Drawing.Point(0, 112);
            this.labelNioGyo.Name = "labelNioGyo";
            this.labelNioGyo.Size = new System.Drawing.Size(110, 20);
            this.labelNioGyo.TabIndex = 12;
            this.labelNioGyo.Text = "荷降業者";
            this.labelNioGyo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lable2
            // 
            this.lable2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable2.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable2.ForeColor = System.Drawing.Color.White;
            this.lable2.Location = new System.Drawing.Point(0, 90);
            this.lable2.Name = "lable2";
            this.lable2.Size = new System.Drawing.Size(110, 20);
            this.lable2.TabIndex = 9;
            this.lable2.Text = "運搬業者";
            this.lable2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lableNioGen
            // 
            this.lableNioGen.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lableNioGen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lableNioGen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lableNioGen.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lableNioGen.ForeColor = System.Drawing.Color.White;
            this.lableNioGen.Location = new System.Drawing.Point(0, 134);
            this.lableNioGen.Name = "lableNioGen";
            this.lableNioGen.Size = new System.Drawing.Size(110, 20);
            this.lableNioGen.TabIndex = 15;
            this.lableNioGen.Text = "荷降現場";
            this.lableNioGen.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.GENBA_NAME.DBFieldsName = "";
            this.GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME.DisplayPopUp = null;
            this.GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.FocusOutCheckMethod")));
            this.GENBA_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME.IsInputErrorOccured = false;
            this.GENBA_NAME.ItemDefinedTypes = "varchar";
            this.GENBA_NAME.Location = new System.Drawing.Point(164, 68);
            this.GENBA_NAME.MaxLength = 0;
            this.GENBA_NAME.Name = "GENBA_NAME";
            this.GENBA_NAME.PopupAfterExecute = null;
            this.GENBA_NAME.PopupBeforeExecute = null;
            this.GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME.PopupSearchSendParams")));
            this.GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME.popupWindowSetting")));
            this.GENBA_NAME.ReadOnly = true;
            this.GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME.RegistCheckMethod")));
            this.GENBA_NAME.Size = new System.Drawing.Size(286, 20);
            this.GENBA_NAME.TabIndex = 42;
            this.GENBA_NAME.TabStop = false;
            this.GENBA_NAME.Tag = " ";
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
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(115, 68);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU,GENBA_CD,GYOUSHA_CD, GYOUSHA_NAME_RYAKU,GYOUSHA_CD";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD, GENBA_NAME,testGENBA_CD,GYOUSHA_CD, GYOUSHA_NAME,testGYOUSHA_CD";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "";
            this.GENBA_CD.Size = new System.Drawing.Size(50, 20);
            this.GENBA_CD.TabIndex = 41;
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.Text = "000001";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GENBA_CD_Validating);
            // 
            // lable5
            // 
            this.lable5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable5.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable5.ForeColor = System.Drawing.Color.White;
            this.lable5.Location = new System.Drawing.Point(0, 68);
            this.lable5.Name = "lable5";
            this.lable5.Size = new System.Drawing.Size(110, 20);
            this.lable5.TabIndex = 6;
            this.lable5.Text = "現場";
            this.lable5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.GYOUSHA_NAME.DBFieldsName = "";
            this.GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME.DisplayPopUp = null;
            this.GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.FocusOutCheckMethod")));
            this.GYOUSHA_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME.IsInputErrorOccured = false;
            this.GYOUSHA_NAME.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME.Location = new System.Drawing.Point(164, 46);
            this.GYOUSHA_NAME.MaxLength = 0;
            this.GYOUSHA_NAME.Name = "GYOUSHA_NAME";
            this.GYOUSHA_NAME.PopupAfterExecute = null;
            this.GYOUSHA_NAME.PopupBeforeExecute = null;
            this.GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME.PopupSearchSendParams")));
            this.GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME.popupWindowSetting")));
            this.GYOUSHA_NAME.ReadOnly = true;
            this.GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME.RegistCheckMethod")));
            this.GYOUSHA_NAME.Size = new System.Drawing.Size(286, 20);
            this.GYOUSHA_NAME.TabIndex = 32;
            this.GYOUSHA_NAME.TabStop = false;
            this.GYOUSHA_NAME.Tag = " ";
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
            this.GYOUSHA_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(115, 46);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GyoushaCdPopUpAfter";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(50, 20);
            this.GYOUSHA_CD.TabIndex = 31;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.Text = "000001";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_Validating);
            // 
            // TORIHIKISAKI_NAME
            // 
            this.TORIHIKISAKI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME.DBFieldsName = "";
            this.TORIHIKISAKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_NAME.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_NAME.Location = new System.Drawing.Point(164, 24);
            this.TORIHIKISAKI_NAME.MaxLength = 0;
            this.TORIHIKISAKI_NAME.Name = "TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_NAME.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME.popupWindowSetting")));
            this.TORIHIKISAKI_NAME.ReadOnly = true;
            this.TORIHIKISAKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME.Size = new System.Drawing.Size(286, 20);
            this.TORIHIKISAKI_NAME.TabIndex = 22;
            this.TORIHIKISAKI_NAME.TabStop = false;
            this.TORIHIKISAKI_NAME.Tag = " ";
            this.TORIHIKISAKI_NAME.Text = "１２３４５６７８９０";
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
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(115, 24);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD, TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(50, 20);
            this.TORIHIKISAKI_CD.TabIndex = 21;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.Text = "000001";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            this.TORIHIKISAKI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.TORIHIKISAKI_CD_Validating);
            // 
            // lable3
            // 
            this.lable3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable3.ForeColor = System.Drawing.Color.White;
            this.lable3.Location = new System.Drawing.Point(0, 46);
            this.lable3.Name = "lable3";
            this.lable3.Size = new System.Drawing.Size(110, 20);
            this.lable3.TabIndex = 3;
            this.lable3.Text = "業者";
            this.lable3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lable1
            // 
            this.lable1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lable1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lable1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lable1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lable1.ForeColor = System.Drawing.Color.White;
            this.lable1.Location = new System.Drawing.Point(0, 24);
            this.lable1.Name = "lable1";
            this.lable1.Size = new System.Drawing.Size(110, 20);
            this.lable1.TabIndex = 0;
            this.lable1.Text = "取引先";
            this.lable1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_DenpyouSyurui
            // 
            this.panel_DenpyouSyurui.Controls.Add(this.lblHaishaJyokyou);
            this.panel_DenpyouSyurui.Controls.Add(this.HAISHA_JOKYO_NAME);
            this.panel_DenpyouSyurui.Controls.Add(this.HAISHA_JOKYO_CD);
            this.panel_DenpyouSyurui.Controls.Add(this.lblHaishaSyurui);
            this.panel_DenpyouSyurui.Controls.Add(this.HAISHA_SHURUI_NAME);
            this.panel_DenpyouSyurui.Controls.Add(this.HAISHA_SHURUI_CD);
            this.panel_DenpyouSyurui.Controls.Add(this.txtNum_DenPyouSyurui);
            this.panel_DenpyouSyurui.Controls.Add(this.customPanel1);
            this.panel_DenpyouSyurui.Controls.Add(this.label1);
            this.panel_DenpyouSyurui.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.panel_DenpyouSyurui.Location = new System.Drawing.Point(682, 0);
            this.panel_DenpyouSyurui.Name = "panel_DenpyouSyurui";
            this.panel_DenpyouSyurui.Size = new System.Drawing.Size(313, 139);
            this.panel_DenpyouSyurui.TabIndex = 6;
            this.panel_DenpyouSyurui.TabStop = true;
            // 
            // lblHaishaJyokyou
            // 
            this.lblHaishaJyokyou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblHaishaJyokyou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHaishaJyokyou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHaishaJyokyou.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHaishaJyokyou.ForeColor = System.Drawing.Color.White;
            this.lblHaishaJyokyou.Location = new System.Drawing.Point(3, 2);
            this.lblHaishaJyokyou.Name = "lblHaishaJyokyou";
            this.lblHaishaJyokyou.Size = new System.Drawing.Size(110, 20);
            this.lblHaishaJyokyou.TabIndex = 652;
            this.lblHaishaJyokyou.Text = "配車状況";
            this.lblHaishaJyokyou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HAISHA_JOKYO_NAME
            // 
            this.HAISHA_JOKYO_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HAISHA_JOKYO_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HAISHA_JOKYO_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.HAISHA_JOKYO_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAISHA_JOKYO_NAME.DisplayPopUp = null;
            this.HAISHA_JOKYO_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHA_JOKYO_NAME.FocusOutCheckMethod")));
            this.HAISHA_JOKYO_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HAISHA_JOKYO_NAME.ForeColor = System.Drawing.Color.Black;
            this.HAISHA_JOKYO_NAME.IsInputErrorOccured = false;
            this.HAISHA_JOKYO_NAME.Location = new System.Drawing.Point(22, 24);
            this.HAISHA_JOKYO_NAME.MaxLength = 0;
            this.HAISHA_JOKYO_NAME.Name = "HAISHA_JOKYO_NAME";
            this.HAISHA_JOKYO_NAME.PopupAfterExecute = null;
            this.HAISHA_JOKYO_NAME.PopupBeforeExecute = null;
            this.HAISHA_JOKYO_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAISHA_JOKYO_NAME.PopupSearchSendParams")));
            this.HAISHA_JOKYO_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAISHA_JOKYO_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAISHA_JOKYO_NAME.popupWindowSetting")));
            this.HAISHA_JOKYO_NAME.ReadOnly = true;
            this.HAISHA_JOKYO_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHA_JOKYO_NAME.RegistCheckMethod")));
            this.HAISHA_JOKYO_NAME.Size = new System.Drawing.Size(106, 20);
            this.HAISHA_JOKYO_NAME.TabIndex = 12;
            this.HAISHA_JOKYO_NAME.TabStop = false;
            this.HAISHA_JOKYO_NAME.Tag = "";
            this.HAISHA_JOKYO_NAME.Text = "受注";
            // 
            // HAISHA_JOKYO_CD
            // 
            this.HAISHA_JOKYO_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HAISHA_JOKYO_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HAISHA_JOKYO_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAISHA_JOKYO_CD.DisplayItemName = "配車状況";
            this.HAISHA_JOKYO_CD.DisplayPopUp = null;
            this.HAISHA_JOKYO_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHA_JOKYO_CD.FocusOutCheckMethod")));
            this.HAISHA_JOKYO_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HAISHA_JOKYO_CD.ForeColor = System.Drawing.Color.Black;
            this.HAISHA_JOKYO_CD.IsInputErrorOccured = false;
            this.HAISHA_JOKYO_CD.Location = new System.Drawing.Point(3, 24);
            this.HAISHA_JOKYO_CD.Name = "HAISHA_JOKYO_CD";
            this.HAISHA_JOKYO_CD.PopupAfterExecute = null;
            this.HAISHA_JOKYO_CD.PopupBeforeExecute = null;
            this.HAISHA_JOKYO_CD.PopupGetMasterField = "HAISHA_JOKYO_CD,HAISHA_JOKYO_NAME";
            this.HAISHA_JOKYO_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAISHA_JOKYO_CD.PopupSearchSendParams")));
            this.HAISHA_JOKYO_CD.PopupSetFormField = "HAISHA_JOKYO_CD,HAISHA_JOKYO_NAME";
            this.HAISHA_JOKYO_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAISHA_JOKYO_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.HAISHA_JOKYO_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAISHA_JOKYO_CD.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.HAISHA_JOKYO_CD.RangeSetting = rangeSettingDto2;
            this.HAISHA_JOKYO_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHA_JOKYO_CD.RegistCheckMethod")));
            this.HAISHA_JOKYO_CD.Size = new System.Drawing.Size(20, 20);
            this.HAISHA_JOKYO_CD.TabIndex = 11;
            this.HAISHA_JOKYO_CD.Tag = "配車状況を指定してください（スペースキー押下にて、選択画面を表示します）";
            this.HAISHA_JOKYO_CD.Text = "1";
            this.HAISHA_JOKYO_CD.WordWrap = false;
            this.HAISHA_JOKYO_CD.Validating += new System.ComponentModel.CancelEventHandler(this.HAISHA_JOKYO_CD_Validating);
            // 
            // lblHaishaSyurui
            // 
            this.lblHaishaSyurui.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblHaishaSyurui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHaishaSyurui.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblHaishaSyurui.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblHaishaSyurui.ForeColor = System.Drawing.Color.White;
            this.lblHaishaSyurui.Location = new System.Drawing.Point(3, 46);
            this.lblHaishaSyurui.Name = "lblHaishaSyurui";
            this.lblHaishaSyurui.Size = new System.Drawing.Size(110, 20);
            this.lblHaishaSyurui.TabIndex = 653;
            this.lblHaishaSyurui.Text = "配車種類";
            this.lblHaishaSyurui.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HAISHA_SHURUI_NAME
            // 
            this.HAISHA_SHURUI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HAISHA_SHURUI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HAISHA_SHURUI_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.HAISHA_SHURUI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAISHA_SHURUI_NAME.DisplayPopUp = null;
            this.HAISHA_SHURUI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHA_SHURUI_NAME.FocusOutCheckMethod")));
            this.HAISHA_SHURUI_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HAISHA_SHURUI_NAME.ForeColor = System.Drawing.Color.Black;
            this.HAISHA_SHURUI_NAME.IsInputErrorOccured = false;
            this.HAISHA_SHURUI_NAME.Location = new System.Drawing.Point(22, 67);
            this.HAISHA_SHURUI_NAME.MaxLength = 0;
            this.HAISHA_SHURUI_NAME.Name = "HAISHA_SHURUI_NAME";
            this.HAISHA_SHURUI_NAME.PopupAfterExecute = null;
            this.HAISHA_SHURUI_NAME.PopupBeforeExecute = null;
            this.HAISHA_SHURUI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAISHA_SHURUI_NAME.PopupSearchSendParams")));
            this.HAISHA_SHURUI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAISHA_SHURUI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAISHA_SHURUI_NAME.popupWindowSetting")));
            this.HAISHA_SHURUI_NAME.ReadOnly = true;
            this.HAISHA_SHURUI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHA_SHURUI_NAME.RegistCheckMethod")));
            this.HAISHA_SHURUI_NAME.Size = new System.Drawing.Size(106, 20);
            this.HAISHA_SHURUI_NAME.TabIndex = 22;
            this.HAISHA_SHURUI_NAME.TabStop = false;
            this.HAISHA_SHURUI_NAME.Tag = "";
            this.HAISHA_SHURUI_NAME.Text = "通常";
            // 
            // HAISHA_SHURUI_CD
            // 
            this.HAISHA_SHURUI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HAISHA_SHURUI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HAISHA_SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HAISHA_SHURUI_CD.DisplayItemName = "配車種類";
            this.HAISHA_SHURUI_CD.DisplayPopUp = null;
            this.HAISHA_SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHA_SHURUI_CD.FocusOutCheckMethod")));
            this.HAISHA_SHURUI_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HAISHA_SHURUI_CD.ForeColor = System.Drawing.Color.Black;
            this.HAISHA_SHURUI_CD.IsInputErrorOccured = false;
            this.HAISHA_SHURUI_CD.Location = new System.Drawing.Point(3, 67);
            this.HAISHA_SHURUI_CD.Name = "HAISHA_SHURUI_CD";
            this.HAISHA_SHURUI_CD.PopupAfterExecute = null;
            this.HAISHA_SHURUI_CD.PopupBeforeExecute = null;
            this.HAISHA_SHURUI_CD.PopupGetMasterField = "HAISHA_SHURUI_CD,HAISHA_SHURUI_NAME";
            this.HAISHA_SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAISHA_SHURUI_CD.PopupSearchSendParams")));
            this.HAISHA_SHURUI_CD.PopupSetFormField = "HAISHA_SHURUI_CD,HAISHA_SHURUI_NAME";
            this.HAISHA_SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAISHA_SHURUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.HAISHA_SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAISHA_SHURUI_CD.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            9,
            0,
            0,
            0});
            this.HAISHA_SHURUI_CD.RangeSetting = rangeSettingDto3;
            this.HAISHA_SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHA_SHURUI_CD.RegistCheckMethod")));
            this.HAISHA_SHURUI_CD.Size = new System.Drawing.Size(20, 20);
            this.HAISHA_SHURUI_CD.TabIndex = 21;
            this.HAISHA_SHURUI_CD.Tag = "配車種類を指定してください（スペースキー押下にて、選択画面を表示します）";
            this.HAISHA_SHURUI_CD.Text = "1";
            this.HAISHA_SHURUI_CD.WordWrap = false;
            this.HAISHA_SHURUI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.HAISHA_SHURUI_CD_Validating);
            // 
            // txtNum_DenPyouSyurui
            // 
            this.txtNum_DenPyouSyurui.BackColor = System.Drawing.SystemColors.Window;
            this.txtNum_DenPyouSyurui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNum_DenPyouSyurui.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNum_DenPyouSyurui.DisplayPopUp = null;
            this.txtNum_DenPyouSyurui.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_DenPyouSyurui.FocusOutCheckMethod")));
            this.txtNum_DenPyouSyurui.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.txtNum_DenPyouSyurui.ForeColor = System.Drawing.Color.Black;
            this.txtNum_DenPyouSyurui.IsInputErrorOccured = false;
            this.txtNum_DenPyouSyurui.LinkedRadioButtonArray = new string[] {
        "radbtnSyuusyuu",
        "radbtnSyukka",
        "radbtnMotikomi",
        "radbtnKuremu",
        "radbtnSsSk",
        "radbtnSsMk"};
            this.txtNum_DenPyouSyurui.Location = new System.Drawing.Point(139, 22);
            this.txtNum_DenPyouSyurui.Name = "txtNum_DenPyouSyurui";
            this.txtNum_DenPyouSyurui.PopupAfterExecute = null;
            this.txtNum_DenPyouSyurui.PopupBeforeExecute = null;
            this.txtNum_DenPyouSyurui.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNum_DenPyouSyurui.PopupSearchSendParams")));
            this.txtNum_DenPyouSyurui.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNum_DenPyouSyurui.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNum_DenPyouSyurui.popupWindowSetting")));
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
            this.txtNum_DenPyouSyurui.RangeSetting = rangeSettingDto4;
            this.txtNum_DenPyouSyurui.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNum_DenPyouSyurui.RegistCheckMethod")));
            this.txtNum_DenPyouSyurui.Size = new System.Drawing.Size(20, 20);
            this.txtNum_DenPyouSyurui.TabIndex = 1;
            this.txtNum_DenPyouSyurui.Tag = "【1～6】のいずれかで入力してください";
            this.txtNum_DenPyouSyurui.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtNum_DenPyouSyurui.WordWrap = false;
            this.txtNum_DenPyouSyurui.TextChanged += new System.EventHandler(this.txtNum_DenPyouSyurui_TextChanged);
            this.txtNum_DenPyouSyurui.Leave += new System.EventHandler(this.txtNum_DenPyouSyurui_Leave);
            // 
            // customPanel1
            // 
            this.customPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel1.Controls.Add(this.radbtnSsMk);
            this.customPanel1.Controls.Add(this.radbtnSsSk);
            this.customPanel1.Controls.Add(this.radbtnSyuusyuu);
            this.customPanel1.Controls.Add(this.radbtnKuremu);
            this.customPanel1.Controls.Add(this.radbtnSyukka);
            this.customPanel1.Controls.Add(this.radbtnMotikomi);
            this.customPanel1.Location = new System.Drawing.Point(159, 22);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(150, 114);
            this.customPanel1.TabIndex = 31;
            // 
            // radbtnSsMk
            // 
            this.radbtnSsMk.AutoSize = true;
            this.radbtnSsMk.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnSsMk.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSsMk.FocusOutCheckMethod")));
            this.radbtnSsMk.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.radbtnSsMk.LinkedTextBox = "txtNum_DenPyouSyurui";
            this.radbtnSsMk.Location = new System.Drawing.Point(3, 92);
            this.radbtnSsMk.Name = "radbtnSsMk";
            this.radbtnSsMk.PopupAfterExecute = null;
            this.radbtnSsMk.PopupBeforeExecute = null;
            this.radbtnSsMk.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnSsMk.PopupSearchSendParams")));
            this.radbtnSsMk.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnSsMk.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnSsMk.popupWindowSetting")));
            this.radbtnSsMk.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSsMk.RegistCheckMethod")));
            this.radbtnSsMk.Size = new System.Drawing.Size(109, 17);
            this.radbtnSsMk.TabIndex = 5;
            this.radbtnSsMk.Tag = "伝票種類が「6.収集＋持込」の場合にはチェックを付けてください";
            this.radbtnSsMk.Text = "6.収集＋持込";
            this.radbtnSsMk.UseVisualStyleBackColor = true;
            this.radbtnSsMk.Value = "6";
            // 
            // radbtnSsSk
            // 
            this.radbtnSsSk.AutoSize = true;
            this.radbtnSsSk.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnSsSk.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSsSk.FocusOutCheckMethod")));
            this.radbtnSsSk.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.radbtnSsSk.LinkedTextBox = "txtNum_DenPyouSyurui";
            this.radbtnSsSk.Location = new System.Drawing.Point(3, 69);
            this.radbtnSsSk.Name = "radbtnSsSk";
            this.radbtnSsSk.PopupAfterExecute = null;
            this.radbtnSsSk.PopupBeforeExecute = null;
            this.radbtnSsSk.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnSsSk.PopupSearchSendParams")));
            this.radbtnSsSk.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnSsSk.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnSsSk.popupWindowSetting")));
            this.radbtnSsSk.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSsSk.RegistCheckMethod")));
            this.radbtnSsSk.Size = new System.Drawing.Size(109, 17);
            this.radbtnSsSk.TabIndex = 4;
            this.radbtnSsSk.Tag = "伝票種類が「5.収集＋出荷」の場合にはチェックを付けてください";
            this.radbtnSsSk.Text = "5.収集＋出荷";
            this.radbtnSsSk.UseVisualStyleBackColor = true;
            this.radbtnSsSk.Value = "5";
            // 
            // radbtnSyuusyuu
            // 
            this.radbtnSyuusyuu.AutoSize = true;
            this.radbtnSyuusyuu.Checked = true;
            this.radbtnSyuusyuu.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnSyuusyuu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSyuusyuu.FocusOutCheckMethod")));
            this.radbtnSyuusyuu.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.radbtnSyuusyuu.LinkedTextBox = "txtNum_DenPyouSyurui";
            this.radbtnSyuusyuu.Location = new System.Drawing.Point(3, 2);
            this.radbtnSyuusyuu.Name = "radbtnSyuusyuu";
            this.radbtnSyuusyuu.PopupAfterExecute = null;
            this.radbtnSyuusyuu.PopupBeforeExecute = null;
            this.radbtnSyuusyuu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnSyuusyuu.PopupSearchSendParams")));
            this.radbtnSyuusyuu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnSyuusyuu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnSyuusyuu.popupWindowSetting")));
            this.radbtnSyuusyuu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSyuusyuu.RegistCheckMethod")));
            this.radbtnSyuusyuu.Size = new System.Drawing.Size(67, 17);
            this.radbtnSyuusyuu.TabIndex = 0;
            this.radbtnSyuusyuu.Tag = "伝票種類が「1.収集」の場合にはチェックを付けてください";
            this.radbtnSyuusyuu.Text = "1.収集";
            this.radbtnSyuusyuu.UseVisualStyleBackColor = true;
            this.radbtnSyuusyuu.Value = "1";
            // 
            // radbtnKuremu
            // 
            this.radbtnKuremu.AutoSize = true;
            this.radbtnKuremu.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnKuremu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnKuremu.FocusOutCheckMethod")));
            this.radbtnKuremu.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.radbtnKuremu.LinkedTextBox = "txtNum_DenPyouSyurui";
            this.radbtnKuremu.Location = new System.Drawing.Point(3, 46);
            this.radbtnKuremu.Name = "radbtnKuremu";
            this.radbtnKuremu.PopupAfterExecute = null;
            this.radbtnKuremu.PopupBeforeExecute = null;
            this.radbtnKuremu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnKuremu.PopupSearchSendParams")));
            this.radbtnKuremu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnKuremu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnKuremu.popupWindowSetting")));
            this.radbtnKuremu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnKuremu.RegistCheckMethod")));
            this.radbtnKuremu.Size = new System.Drawing.Size(95, 17);
            this.radbtnKuremu.TabIndex = 3;
            this.radbtnKuremu.Tag = "伝票種類が「4.クレーム」の場合にはチェックを付けてください";
            this.radbtnKuremu.Text = "4.クレーム";
            this.radbtnKuremu.UseVisualStyleBackColor = true;
            this.radbtnKuremu.Value = "4";
            // 
            // radbtnSyukka
            // 
            this.radbtnSyukka.AutoSize = true;
            this.radbtnSyukka.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnSyukka.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSyukka.FocusOutCheckMethod")));
            this.radbtnSyukka.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.radbtnSyukka.LinkedTextBox = "txtNum_DenPyouSyurui";
            this.radbtnSyukka.Location = new System.Drawing.Point(76, 2);
            this.radbtnSyukka.Name = "radbtnSyukka";
            this.radbtnSyukka.PopupAfterExecute = null;
            this.radbtnSyukka.PopupBeforeExecute = null;
            this.radbtnSyukka.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnSyukka.PopupSearchSendParams")));
            this.radbtnSyukka.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnSyukka.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnSyukka.popupWindowSetting")));
            this.radbtnSyukka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnSyukka.RegistCheckMethod")));
            this.radbtnSyukka.Size = new System.Drawing.Size(67, 17);
            this.radbtnSyukka.TabIndex = 1;
            this.radbtnSyukka.Tag = "伝票種類が「2.出荷」の場合にはチェックを付けてください";
            this.radbtnSyukka.Text = "2.出荷";
            this.radbtnSyukka.UseVisualStyleBackColor = true;
            this.radbtnSyukka.Value = "2";
            // 
            // radbtnMotikomi
            // 
            this.radbtnMotikomi.AutoSize = true;
            this.radbtnMotikomi.DefaultBackColor = System.Drawing.Color.Empty;
            this.radbtnMotikomi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnMotikomi.FocusOutCheckMethod")));
            this.radbtnMotikomi.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.radbtnMotikomi.LinkedTextBox = "txtNum_DenPyouSyurui";
            this.radbtnMotikomi.Location = new System.Drawing.Point(3, 24);
            this.radbtnMotikomi.Name = "radbtnMotikomi";
            this.radbtnMotikomi.PopupAfterExecute = null;
            this.radbtnMotikomi.PopupBeforeExecute = null;
            this.radbtnMotikomi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radbtnMotikomi.PopupSearchSendParams")));
            this.radbtnMotikomi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radbtnMotikomi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radbtnMotikomi.popupWindowSetting")));
            this.radbtnMotikomi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radbtnMotikomi.RegistCheckMethod")));
            this.radbtnMotikomi.Size = new System.Drawing.Size(67, 17);
            this.radbtnMotikomi.TabIndex = 2;
            this.radbtnMotikomi.Tag = "伝票種類が「3.持込」の場合にはチェックを付けてください";
            this.radbtnMotikomi.Text = "3.持込";
            this.radbtnMotikomi.UseVisualStyleBackColor = true;
            this.radbtnMotikomi.Value = "3";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(140, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 40;
            this.label1.Text = "伝票種類※";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UKETSUKE_EXPORT_KBN
            // 
            this.UKETSUKE_EXPORT_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.UKETSUKE_EXPORT_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UKETSUKE_EXPORT_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_EXPORT_KBN.DisplayItemName = "受付データ抽出条件";
            this.UKETSUKE_EXPORT_KBN.DisplayPopUp = null;
            this.UKETSUKE_EXPORT_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN.FocusOutCheckMethod")));
            this.UKETSUKE_EXPORT_KBN.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.UKETSUKE_EXPORT_KBN.ForeColor = System.Drawing.Color.Black;
            this.UKETSUKE_EXPORT_KBN.IsInputErrorOccured = false;
            this.UKETSUKE_EXPORT_KBN.LinkedRadioButtonArray = new string[] {
        "UKETSUKE_EXPORT_KBN_1",
        "UKETSUKE_EXPORT_KBN_2",
        "UKETSUKE_EXPORT_KBN_3"};
            this.UKETSUKE_EXPORT_KBN.Location = new System.Drawing.Point(459, 25);
            this.UKETSUKE_EXPORT_KBN.Name = "UKETSUKE_EXPORT_KBN";
            this.UKETSUKE_EXPORT_KBN.PopupAfterExecute = null;
            this.UKETSUKE_EXPORT_KBN.PopupBeforeExecute = null;
            this.UKETSUKE_EXPORT_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN.PopupSearchSendParams")));
            this.UKETSUKE_EXPORT_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UKETSUKE_EXPORT_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.UKETSUKE_EXPORT_KBN.RangeSetting = rangeSettingDto1;
            this.UKETSUKE_EXPORT_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN.RegistCheckMethod")));
            this.UKETSUKE_EXPORT_KBN.Size = new System.Drawing.Size(20, 20);
            this.UKETSUKE_EXPORT_KBN.TabIndex = 535;
            this.UKETSUKE_EXPORT_KBN.Tag = "【1～3】のいずれかで入力してください";
            this.UKETSUKE_EXPORT_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.UKETSUKE_EXPORT_KBN.WordWrap = false;
            this.UKETSUKE_EXPORT_KBN.TextChanged += new System.EventHandler(this.UKETSUKE_EXPORT_KBN_TextChanged);
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.UKETSUKE_EXPORT_KBN_1);
            this.customPanel2.Controls.Add(this.UKETSUKE_EXPORT_KBN_2);
            this.customPanel2.Controls.Add(this.UKETSUKE_EXPORT_KBN_3);
            this.customPanel2.Location = new System.Drawing.Point(479, 25);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(199, 78);
            this.customPanel2.TabIndex = 536;
            // 
            // UKETSUKE_EXPORT_KBN_1
            // 
            this.UKETSUKE_EXPORT_KBN_1.AutoSize = true;
            this.UKETSUKE_EXPORT_KBN_1.Checked = true;
            this.UKETSUKE_EXPORT_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_EXPORT_KBN_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_1.FocusOutCheckMethod")));
            this.UKETSUKE_EXPORT_KBN_1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.UKETSUKE_EXPORT_KBN_1.LinkedTextBox = "UKETSUKE_EXPORT_KBN";
            this.UKETSUKE_EXPORT_KBN_1.Location = new System.Drawing.Point(3, 2);
            this.UKETSUKE_EXPORT_KBN_1.Name = "UKETSUKE_EXPORT_KBN_1";
            this.UKETSUKE_EXPORT_KBN_1.PopupAfterExecute = null;
            this.UKETSUKE_EXPORT_KBN_1.PopupBeforeExecute = null;
            this.UKETSUKE_EXPORT_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_1.PopupSearchSendParams")));
            this.UKETSUKE_EXPORT_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UKETSUKE_EXPORT_KBN_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_1.popupWindowSetting")));
            this.UKETSUKE_EXPORT_KBN_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_1.RegistCheckMethod")));
            this.UKETSUKE_EXPORT_KBN_1.Size = new System.Drawing.Size(172, 17);
            this.UKETSUKE_EXPORT_KBN_1.TabIndex = 0;
            this.UKETSUKE_EXPORT_KBN_1.Tag = " ";
            this.UKETSUKE_EXPORT_KBN_1.Text = "1.環境将軍R受付データ";
            this.UKETSUKE_EXPORT_KBN_1.UseVisualStyleBackColor = true;
            this.UKETSUKE_EXPORT_KBN_1.Value = "1";
            // 
            // UKETSUKE_EXPORT_KBN_2
            // 
            this.UKETSUKE_EXPORT_KBN_2.AutoSize = true;
            this.UKETSUKE_EXPORT_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_EXPORT_KBN_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_2.FocusOutCheckMethod")));
            this.UKETSUKE_EXPORT_KBN_2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.UKETSUKE_EXPORT_KBN_2.LinkedTextBox = "UKETSUKE_EXPORT_KBN";
            this.UKETSUKE_EXPORT_KBN_2.Location = new System.Drawing.Point(3, 25);
            this.UKETSUKE_EXPORT_KBN_2.Name = "UKETSUKE_EXPORT_KBN_2";
            this.UKETSUKE_EXPORT_KBN_2.PopupAfterExecute = null;
            this.UKETSUKE_EXPORT_KBN_2.PopupBeforeExecute = null;
            this.UKETSUKE_EXPORT_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_2.PopupSearchSendParams")));
            this.UKETSUKE_EXPORT_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UKETSUKE_EXPORT_KBN_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_2.popupWindowSetting")));
            this.UKETSUKE_EXPORT_KBN_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_2.RegistCheckMethod")));
            this.UKETSUKE_EXPORT_KBN_2.Size = new System.Drawing.Size(200, 17);
            this.UKETSUKE_EXPORT_KBN_2.TabIndex = 1;
            this.UKETSUKE_EXPORT_KBN_2.Tag = "伝票種類が「2.出荷」の場合にはチェックを付けてください";
            this.UKETSUKE_EXPORT_KBN_2.Text = "2.将軍-INXS連携受付データ";
            this.UKETSUKE_EXPORT_KBN_2.UseVisualStyleBackColor = true;
            this.UKETSUKE_EXPORT_KBN_2.Value = "2";
            // 
            // UKETSUKE_EXPORT_KBN_3
            // 
            this.UKETSUKE_EXPORT_KBN_3.AutoSize = true;
            this.UKETSUKE_EXPORT_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.UKETSUKE_EXPORT_KBN_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_3.FocusOutCheckMethod")));
            this.UKETSUKE_EXPORT_KBN_3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.UKETSUKE_EXPORT_KBN_3.LinkedTextBox = "UKETSUKE_EXPORT_KBN";
            this.UKETSUKE_EXPORT_KBN_3.Location = new System.Drawing.Point(3, 48);
            this.UKETSUKE_EXPORT_KBN_3.Name = "UKETSUKE_EXPORT_KBN_3";
            this.UKETSUKE_EXPORT_KBN_3.PopupAfterExecute = null;
            this.UKETSUKE_EXPORT_KBN_3.PopupBeforeExecute = null;
            this.UKETSUKE_EXPORT_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_3.PopupSearchSendParams")));
            this.UKETSUKE_EXPORT_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UKETSUKE_EXPORT_KBN_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_3.popupWindowSetting")));
            this.UKETSUKE_EXPORT_KBN_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UKETSUKE_EXPORT_KBN_3.RegistCheckMethod")));
            this.UKETSUKE_EXPORT_KBN_3.Size = new System.Drawing.Size(67, 17);
            this.UKETSUKE_EXPORT_KBN_3.TabIndex = 2;
            this.UKETSUKE_EXPORT_KBN_3.Tag = " ";
            this.UKETSUKE_EXPORT_KBN_3.Text = "3.全て";
            this.UKETSUKE_EXPORT_KBN_3.UseVisualStyleBackColor = true;
            this.UKETSUKE_EXPORT_KBN_3.Value = "3";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(459, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(154, 20);
            this.label3.TabIndex = 537;
            this.label3.Text = "受付データ抽出条件※";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1001, 507);
            this.Controls.Add(this.panel_DenpyouSyurui);
            this.Controls.Add(this.pnlSearchString);
            this.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.Name = "UIForm";
            this.Text = "";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.pnlSearchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.panel_DenpyouSyurui, 0);
            this.pnlSearchString.ResumeLayout(false);
            this.pnlSearchString.PerformLayout();
            this.panel_DenpyouSyurui.ResumeLayout(false);
            this.panel_DenpyouSyurui.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomPanel pnlSearchString;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        private Label lable3;
        private Label lable1;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        private Label lable5;
        internal r_framework.CustomControl.CustomTextBox NIOROSHI_GENBA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox NIOROSHI_GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox NIOROSHI_GYOUSHA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox NIOROSHI_GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox UNPAN_GYOUSHA_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox UNPAN_GYOUSHA_CD;
        private Label labelNioGyo;
        private Label lable2;
        private Label lableNioGen;
        private r_framework.CustomControl.CustomPanel panel_DenpyouSyurui;
        public r_framework.CustomControl.CustomRadioButton radbtnKuremu;
        public r_framework.CustomControl.CustomRadioButton radbtnMotikomi;
        public r_framework.CustomControl.CustomNumericTextBox2 txtNum_DenPyouSyurui;
        public r_framework.CustomControl.CustomRadioButton radbtnSyuusyuu;
        public r_framework.CustomControl.CustomRadioButton radbtnSyukka;
        private Label label1;
        public r_framework.CustomControl.CustomTextBox testGENBA_CD;
        public r_framework.CustomControl.CustomTextBox testNIOROSHI_GENBA_CD;
        private r_framework.CustomControl.CustomTextBox testNIOROSHI_GYOUSHA_CD;
        private r_framework.CustomControl.CustomTextBox testGYOUSHA_CD;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomComboBox cmbShimebi;
        public Label label4;
        private Label lblHaishaJyokyou;
        internal r_framework.CustomControl.CustomTextBox HAISHA_JOKYO_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 HAISHA_JOKYO_CD;
        private Label lblHaishaSyurui;
        internal r_framework.CustomControl.CustomTextBox HAISHA_SHURUI_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 HAISHA_SHURUI_CD;
        public r_framework.CustomControl.CustomRadioButton radbtnSsMk;
        public r_framework.CustomControl.CustomRadioButton radbtnSsSk;
        internal r_framework.CustomControl.CustomComboBox cmbShihariaShimebi;
        public Label label2;
        public r_framework.CustomControl.CustomNumericTextBox2 UKETSUKE_EXPORT_KBN;
        private r_framework.CustomControl.CustomPanel customPanel2;
        public r_framework.CustomControl.CustomRadioButton UKETSUKE_EXPORT_KBN_1;
        public r_framework.CustomControl.CustomRadioButton UKETSUKE_EXPORT_KBN_2;
        public r_framework.CustomControl.CustomRadioButton UKETSUKE_EXPORT_KBN_3;
        private Label label3;

    }
}