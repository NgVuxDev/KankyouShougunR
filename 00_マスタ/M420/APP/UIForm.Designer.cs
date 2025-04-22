namespace Shougun.Core.Master.SaishuShobunBasyoPatternIchiran.APP
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
            this.rdb_KensakuCdFurigana = new r_framework.CustomControl.CustomRadioButton();
            this.lbl_PattenName = new System.Windows.Forms.Label();
            this.rdb_KensakuCdPatten = new r_framework.CustomControl.CustomRadioButton();
            this.numtxt_KensakuCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.rdb_Zenpan = new r_framework.CustomControl.CustomRadioButton();
            this.rdb_Kenpai = new r_framework.CustomControl.CustomRadioButton();
            this.numtxt_KeiyakushoTypeCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lbl_KeiyakushoType = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.txt_SerchPattern = new r_framework.CustomControl.CustomTextBox();
            this.customPanel1.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.searchString.Enabled = false;
            this.searchString.Size = new System.Drawing.Size(438, 80);
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(2, 427);
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(203, 427);
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(605, 427);
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(806, 427);
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Location = new System.Drawing.Point(4, 100);
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(4, 85);
            // 
            // rdb_KensakuCdFurigana
            // 
            this.rdb_KensakuCdFurigana.AutoSize = true;
            this.rdb_KensakuCdFurigana.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdb_KensakuCdFurigana.DisplayItemName = "";
            this.rdb_KensakuCdFurigana.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdb_KensakuCdFurigana.FocusOutCheckMethod")));
            this.rdb_KensakuCdFurigana.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdb_KensakuCdFurigana.LinkedTextBox = "numtxt_KensakuCd";
            this.rdb_KensakuCdFurigana.Location = new System.Drawing.Point(118, 0);
            this.rdb_KensakuCdFurigana.Name = "rdb_KensakuCdFurigana";
            this.rdb_KensakuCdFurigana.PopupAfterExecute = null;
            this.rdb_KensakuCdFurigana.PopupBeforeExecute = null;
            this.rdb_KensakuCdFurigana.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdb_KensakuCdFurigana.PopupSearchSendParams")));
            this.rdb_KensakuCdFurigana.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdb_KensakuCdFurigana.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdb_KensakuCdFurigana.popupWindowSetting")));
            this.rdb_KensakuCdFurigana.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdb_KensakuCdFurigana.RegistCheckMethod")));
            this.rdb_KensakuCdFurigana.ShortItemName = "";
            this.rdb_KensakuCdFurigana.Size = new System.Drawing.Size(95, 17);
            this.rdb_KensakuCdFurigana.TabIndex = 9;
            this.rdb_KensakuCdFurigana.Tag = "フリガナで検索したい場合チェックを付けてください";
            this.rdb_KensakuCdFurigana.Text = "2.フリガナ";
            this.rdb_KensakuCdFurigana.UseVisualStyleBackColor = true;
            this.rdb_KensakuCdFurigana.Value = "2";
            // 
            // lbl_PattenName
            // 
            this.lbl_PattenName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_PattenName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_PattenName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_PattenName.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_PattenName.Location = new System.Drawing.Point(12, 24);
            this.lbl_PattenName.Name = "lbl_PattenName";
            this.lbl_PattenName.Size = new System.Drawing.Size(99, 20);
            this.lbl_PattenName.TabIndex = 5;
            this.lbl_PattenName.Text = "パターン名";
            this.lbl_PattenName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // rdb_KensakuCdPatten
            // 
            this.rdb_KensakuCdPatten.AutoSize = true;
            this.rdb_KensakuCdPatten.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdb_KensakuCdPatten.DisplayItemName = "";
            this.rdb_KensakuCdPatten.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdb_KensakuCdPatten.FocusOutCheckMethod")));
            this.rdb_KensakuCdPatten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdb_KensakuCdPatten.LinkedTextBox = "numtxt_KensakuCd";
            this.rdb_KensakuCdPatten.Location = new System.Drawing.Point(3, 0);
            this.rdb_KensakuCdPatten.Name = "rdb_KensakuCdPatten";
            this.rdb_KensakuCdPatten.PopupAfterExecute = null;
            this.rdb_KensakuCdPatten.PopupBeforeExecute = null;
            this.rdb_KensakuCdPatten.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdb_KensakuCdPatten.PopupSearchSendParams")));
            this.rdb_KensakuCdPatten.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdb_KensakuCdPatten.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdb_KensakuCdPatten.popupWindowSetting")));
            this.rdb_KensakuCdPatten.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdb_KensakuCdPatten.RegistCheckMethod")));
            this.rdb_KensakuCdPatten.ShortItemName = "";
            this.rdb_KensakuCdPatten.Size = new System.Drawing.Size(109, 17);
            this.rdb_KensakuCdPatten.TabIndex = 8;
            this.rdb_KensakuCdPatten.Tag = "パターンで検索したい場合チェックを付けてください";
            this.rdb_KensakuCdPatten.Text = "1.パターン名";
            this.rdb_KensakuCdPatten.UseVisualStyleBackColor = true;
            this.rdb_KensakuCdPatten.Value = "1";
            // 
            // numtxt_KensakuCd
            // 
            this.numtxt_KensakuCd.BackColor = System.Drawing.SystemColors.Window;
            this.numtxt_KensakuCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numtxt_KensakuCd.DBFieldsName = "";
            this.numtxt_KensakuCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.numtxt_KensakuCd.DisplayItemName = "検索CD";
            this.numtxt_KensakuCd.DisplayPopUp = null;
            this.numtxt_KensakuCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numtxt_KensakuCd.FocusOutCheckMethod")));
            this.numtxt_KensakuCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.numtxt_KensakuCd.ForeColor = System.Drawing.Color.Black;
            this.numtxt_KensakuCd.IsInputErrorOccured = false;
            this.numtxt_KensakuCd.ItemDefinedTypes = "";
            this.numtxt_KensakuCd.LinkedRadioButtonArray = new string[] {
        "rdb_KensakuCdPatten",
        "rdb_KensakuCdFurigana"};
            this.numtxt_KensakuCd.Location = new System.Drawing.Point(12, 0);
            this.numtxt_KensakuCd.Name = "numtxt_KensakuCd";
            this.numtxt_KensakuCd.PopupAfterExecute = null;
            this.numtxt_KensakuCd.PopupAfterExecuteMethod = "";
            this.numtxt_KensakuCd.PopupBeforeExecute = null;
            this.numtxt_KensakuCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("numtxt_KensakuCd.PopupSearchSendParams")));
            this.numtxt_KensakuCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.numtxt_KensakuCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("numtxt_KensakuCd.popupWindowSetting")));
            this.numtxt_KensakuCd.prevText = null;
            this.numtxt_KensakuCd.PrevText = null;
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
            this.numtxt_KensakuCd.RangeSetting = rangeSettingDto1;
            this.numtxt_KensakuCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numtxt_KensakuCd.RegistCheckMethod")));
            this.numtxt_KensakuCd.ShortItemName = "";
            this.numtxt_KensakuCd.Size = new System.Drawing.Size(20, 20);
            this.numtxt_KensakuCd.TabIndex = 7;
            this.numtxt_KensakuCd.Tag = "半角1桁以内で入力してください";
            this.numtxt_KensakuCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numtxt_KensakuCd.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.customPanel3);
            this.customPanel1.Controls.Add(this.numtxt_KeiyakushoTypeCd);
            this.customPanel1.Controls.Add(this.lbl_KeiyakushoType);
            this.customPanel1.Controls.Add(this.customPanel2);
            this.customPanel1.Controls.Add(this.txt_SerchPattern);
            this.customPanel1.Controls.Add(this.numtxt_KensakuCd);
            this.customPanel1.Controls.Add(this.lbl_PattenName);
            this.customPanel1.Location = new System.Drawing.Point(0, 2);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(556, 80);
            this.customPanel1.TabIndex = 10;
            // 
            // customPanel3
            // 
            this.customPanel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel3.Controls.Add(this.rdb_Zenpan);
            this.customPanel3.Controls.Add(this.rdb_Kenpai);
            this.customPanel3.Location = new System.Drawing.Point(169, 49);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(243, 20);
            this.customPanel3.TabIndex = 14;
            // 
            // rdb_Zenpan
            // 
            this.rdb_Zenpan.AutoSize = true;
            this.rdb_Zenpan.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdb_Zenpan.DisplayItemName = "";
            this.rdb_Zenpan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdb_Zenpan.FocusOutCheckMethod")));
            this.rdb_Zenpan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdb_Zenpan.LinkedTextBox = "numtxt_KeiyakushoTypeCd";
            this.rdb_Zenpan.Location = new System.Drawing.Point(3, 0);
            this.rdb_Zenpan.Name = "rdb_Zenpan";
            this.rdb_Zenpan.PopupAfterExecute = null;
            this.rdb_Zenpan.PopupBeforeExecute = null;
            this.rdb_Zenpan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdb_Zenpan.PopupSearchSendParams")));
            this.rdb_Zenpan.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdb_Zenpan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdb_Zenpan.popupWindowSetting")));
            this.rdb_Zenpan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdb_Zenpan.RegistCheckMethod")));
            this.rdb_Zenpan.ShortItemName = "";
            this.rdb_Zenpan.Size = new System.Drawing.Size(109, 17);
            this.rdb_Zenpan.TabIndex = 8;
            this.rdb_Zenpan.Tag = "委託契約書様式が「1.全般連様式」の場合はチェックを付けてください";
            this.rdb_Zenpan.Text = "1.全産連様式";
            this.rdb_Zenpan.UseVisualStyleBackColor = true;
            this.rdb_Zenpan.Value = "1";
            // 
            // rdb_Kenpai
            // 
            this.rdb_Kenpai.AutoSize = true;
            this.rdb_Kenpai.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdb_Kenpai.DisplayItemName = "";
            this.rdb_Kenpai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdb_Kenpai.FocusOutCheckMethod")));
            this.rdb_Kenpai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdb_Kenpai.LinkedTextBox = "numtxt_KeiyakushoTypeCd";
            this.rdb_Kenpai.Location = new System.Drawing.Point(118, 0);
            this.rdb_Kenpai.Name = "rdb_Kenpai";
            this.rdb_Kenpai.PopupAfterExecute = null;
            this.rdb_Kenpai.PopupBeforeExecute = null;
            this.rdb_Kenpai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdb_Kenpai.PopupSearchSendParams")));
            this.rdb_Kenpai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdb_Kenpai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdb_Kenpai.popupWindowSetting")));
            this.rdb_Kenpai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdb_Kenpai.RegistCheckMethod")));
            this.rdb_Kenpai.ShortItemName = "";
            this.rdb_Kenpai.Size = new System.Drawing.Size(123, 17);
            this.rdb_Kenpai.TabIndex = 9;
            this.rdb_Kenpai.Tag = "委託契約書様式が「2.建廃個別様式」の場合はチェックを付けてください";
            this.rdb_Kenpai.Text = "2.建廃個別様式";
            this.rdb_Kenpai.UseVisualStyleBackColor = true;
            this.rdb_Kenpai.Value = "2";
            // 
            // numtxt_KeiyakushoTypeCd
            // 
            this.numtxt_KeiyakushoTypeCd.BackColor = System.Drawing.SystemColors.Window;
            this.numtxt_KeiyakushoTypeCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.numtxt_KeiyakushoTypeCd.DBFieldsName = "";
            this.numtxt_KeiyakushoTypeCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.numtxt_KeiyakushoTypeCd.DisplayItemName = "委託契約書様式";
            this.numtxt_KeiyakushoTypeCd.DisplayPopUp = null;
            this.numtxt_KeiyakushoTypeCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numtxt_KeiyakushoTypeCd.FocusOutCheckMethod")));
            this.numtxt_KeiyakushoTypeCd.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.numtxt_KeiyakushoTypeCd.ForeColor = System.Drawing.Color.Black;
            this.numtxt_KeiyakushoTypeCd.IsInputErrorOccured = false;
            this.numtxt_KeiyakushoTypeCd.ItemDefinedTypes = "";
            this.numtxt_KeiyakushoTypeCd.LinkedRadioButtonArray = new string[] {
        "rdb_Zenpan",
        "rdb_Kenpai"};
            this.numtxt_KeiyakushoTypeCd.Location = new System.Drawing.Point(150, 49);
            this.numtxt_KeiyakushoTypeCd.Name = "numtxt_KeiyakushoTypeCd";
            this.numtxt_KeiyakushoTypeCd.PopupAfterExecute = null;
            this.numtxt_KeiyakushoTypeCd.PopupAfterExecuteMethod = "";
            this.numtxt_KeiyakushoTypeCd.PopupBeforeExecute = null;
            this.numtxt_KeiyakushoTypeCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("numtxt_KeiyakushoTypeCd.PopupSearchSendParams")));
            this.numtxt_KeiyakushoTypeCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.numtxt_KeiyakushoTypeCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("numtxt_KeiyakushoTypeCd.popupWindowSetting")));
            this.numtxt_KeiyakushoTypeCd.prevText = null;
            this.numtxt_KeiyakushoTypeCd.PrevText = null;
            rangeSettingDto2.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numtxt_KeiyakushoTypeCd.RangeSetting = rangeSettingDto2;
            this.numtxt_KeiyakushoTypeCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("numtxt_KeiyakushoTypeCd.RegistCheckMethod")));
            this.numtxt_KeiyakushoTypeCd.ShortItemName = "";
            this.numtxt_KeiyakushoTypeCd.Size = new System.Drawing.Size(20, 20);
            this.numtxt_KeiyakushoTypeCd.TabIndex = 13;
            this.numtxt_KeiyakushoTypeCd.Tag = "半角1桁以内で入力してください";
            this.numtxt_KeiyakushoTypeCd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numtxt_KeiyakushoTypeCd.WordWrap = false;
            // 
            // lbl_KeiyakushoType
            // 
            this.lbl_KeiyakushoType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_KeiyakushoType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_KeiyakushoType.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_KeiyakushoType.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.lbl_KeiyakushoType.Location = new System.Drawing.Point(12, 49);
            this.lbl_KeiyakushoType.Name = "lbl_KeiyakushoType";
            this.lbl_KeiyakushoType.Size = new System.Drawing.Size(132, 20);
            this.lbl_KeiyakushoType.TabIndex = 12;
            this.lbl_KeiyakushoType.Text = "委託契約書様式※";
            this.lbl_KeiyakushoType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.rdb_KensakuCdPatten);
            this.customPanel2.Controls.Add(this.rdb_KensakuCdFurigana);
            this.customPanel2.Location = new System.Drawing.Point(31, 0);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(243, 20);
            this.customPanel2.TabIndex = 11;
            // 
            // txt_SerchPattern
            // 
            this.txt_SerchPattern.BackColor = System.Drawing.SystemColors.Window;
            this.txt_SerchPattern.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_SerchPattern.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_SerchPattern.DisplayPopUp = null;
            this.txt_SerchPattern.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_SerchPattern.FocusOutCheckMethod")));
            this.txt_SerchPattern.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_SerchPattern.ForeColor = System.Drawing.Color.Black;
            this.txt_SerchPattern.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.txt_SerchPattern.IsInputErrorOccured = false;
            this.txt_SerchPattern.Location = new System.Drawing.Point(117, 24);
            this.txt_SerchPattern.MaxLength = 20;
            this.txt_SerchPattern.Name = "txt_SerchPattern";
            this.txt_SerchPattern.PopupAfterExecute = null;
            this.txt_SerchPattern.PopupBeforeExecute = null;
            this.txt_SerchPattern.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_SerchPattern.PopupSearchSendParams")));
            this.txt_SerchPattern.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_SerchPattern.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_SerchPattern.popupWindowSetting")));
            this.txt_SerchPattern.prevText = null;
            this.txt_SerchPattern.PrevText = null;
            this.txt_SerchPattern.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_SerchPattern.RegistCheckMethod")));
            this.txt_SerchPattern.Size = new System.Drawing.Size(351, 20);
            this.txt_SerchPattern.TabIndex = 10;
            this.txt_SerchPattern.Tag = "全角２０文字以内で入力してください";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 458);
            this.Controls.Add(this.customPanel1);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.customPanel1, 0);
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomRadioButton rdb_KensakuCdFurigana;
        internal System.Windows.Forms.Label lbl_PattenName;
        internal r_framework.CustomControl.CustomRadioButton rdb_KensakuCdPatten;
        internal r_framework.CustomControl.CustomNumericTextBox2 numtxt_KensakuCd;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomTextBox txt_SerchPattern;
        private r_framework.CustomControl.CustomPanel customPanel2;
        private r_framework.CustomControl.CustomPanel customPanel3;
        internal r_framework.CustomControl.CustomRadioButton rdb_Zenpan;
        internal r_framework.CustomControl.CustomRadioButton rdb_Kenpai;
        internal r_framework.CustomControl.CustomNumericTextBox2 numtxt_KeiyakushoTypeCd;
        internal System.Windows.Forms.Label lbl_KeiyakushoType;
    }
}