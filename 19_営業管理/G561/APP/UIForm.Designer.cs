namespace ShouninzumiDenshiShinseiIchiran.APP
{
    partial class UIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this.lblSearchCondition = new System.Windows.Forms.Label();
            this.lblShinseiDate = new System.Windows.Forms.Label();
            this.lblShainName = new System.Windows.Forms.Label();
            this.lblFromTo = new System.Windows.Forms.Label();
            this.SHINSEI_END = new r_framework.CustomControl.CustomDateTimePicker();
            this.SHINSEI_BEGIN = new r_framework.CustomControl.CustomDateTimePicker();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new System.Windows.Forms.CheckBox();
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = new System.Windows.Forms.CheckBox();
            this.lblShowCondition = new System.Windows.Forms.Label();
            this.CONDITION_ITEM = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.txtBoxShainName = new r_framework.CustomControl.CustomTextBox();
            this.txtBoxShainCD = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.Location = new System.Drawing.Point(984, 3);
            this.searchString.ReadOnly = true;
            this.searchString.Size = new System.Drawing.Size(20, 19);
            this.searchString.TabIndex = 100;
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 201;
            this.bt_ptn1.Visible = false;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 202;
            this.bt_ptn2.Visible = false;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 203;
            this.bt_ptn3.Visible = false;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(604, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 204;
            this.bt_ptn4.Visible = false;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(804, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 205;
            this.bt_ptn5.Visible = false;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(984, 24);
            this.customSortHeader1.Size = new System.Drawing.Size(20, 14);
            this.customSortHeader1.TabIndex = 118;
            this.customSortHeader1.Visible = false;
            // 
            // lblSearchCondition
            // 
            this.lblSearchCondition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblSearchCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSearchCondition.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblSearchCondition.ForeColor = System.Drawing.Color.White;
            this.lblSearchCondition.Location = new System.Drawing.Point(12, 70);
            this.lblSearchCondition.Name = "lblSearchCondition";
            this.lblSearchCondition.Size = new System.Drawing.Size(110, 20);
            this.lblSearchCondition.TabIndex = 105;
            this.lblSearchCondition.Text = "検索条件";
            this.lblSearchCondition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShinseiDate
            // 
            this.lblShinseiDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblShinseiDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShinseiDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShinseiDate.ForeColor = System.Drawing.Color.White;
            this.lblShinseiDate.Location = new System.Drawing.Point(12, 44);
            this.lblShinseiDate.Name = "lblShinseiDate";
            this.lblShinseiDate.Size = new System.Drawing.Size(110, 20);
            this.lblShinseiDate.TabIndex = 103;
            this.lblShinseiDate.Text = "申請日付";
            this.lblShinseiDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblShainName
            // 
            this.lblShainName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblShainName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShainName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShainName.ForeColor = System.Drawing.Color.White;
            this.lblShainName.Location = new System.Drawing.Point(12, 18);
            this.lblShainName.Name = "lblShainName";
            this.lblShainName.Size = new System.Drawing.Size(110, 20);
            this.lblShainName.TabIndex = 101;
            this.lblShainName.Text = "社員名";
            this.lblShainName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFromTo
            // 
            this.lblFromTo.AutoSize = true;
            this.lblFromTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblFromTo.Location = new System.Drawing.Point(263, 47);
            this.lblFromTo.Name = "lblFromTo";
            this.lblFromTo.Size = new System.Drawing.Size(21, 13);
            this.lblFromTo.TabIndex = 116;
            this.lblFromTo.Text = "～";
            // 
            // SHINSEI_END
            // 
            this.SHINSEI_END.BackColor = System.Drawing.SystemColors.Window;
            this.SHINSEI_END.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHINSEI_END.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.SHINSEI_END.Checked = false;
            this.SHINSEI_END.DateTimeNowYear = "";
            this.SHINSEI_END.DBFieldsName = "TEKIYOU_END";
            this.SHINSEI_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHINSEI_END.DisplayItemName = "適用終了";
            this.SHINSEI_END.DisplayPopUp = null;
            this.SHINSEI_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHINSEI_END.FocusOutCheckMethod")));
            this.SHINSEI_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHINSEI_END.ForeColor = System.Drawing.Color.Black;
            this.SHINSEI_END.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SHINSEI_END.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHINSEI_END.IsInputErrorOccured = false;
            this.SHINSEI_END.ItemDefinedTypes = "datetime";
            this.SHINSEI_END.Location = new System.Drawing.Point(289, 44);
            this.SHINSEI_END.MaxLength = 10;
            this.SHINSEI_END.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SHINSEI_END.Name = "SHINSEI_END";
            this.SHINSEI_END.NullValue = "";
            this.SHINSEI_END.PopupAfterExecute = null;
            this.SHINSEI_END.PopupBeforeExecute = null;
            this.SHINSEI_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHINSEI_END.PopupSearchSendParams")));
            this.SHINSEI_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHINSEI_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHINSEI_END.popupWindowSetting")));
            this.SHINSEI_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHINSEI_END.RegistCheckMethod")));
            this.SHINSEI_END.ShortItemName = "適用終了";
            this.SHINSEI_END.Size = new System.Drawing.Size(124, 20);
            this.SHINSEI_END.TabIndex = 117;
            this.SHINSEI_END.Tag = "適用終了日を入力して下さい";
            this.SHINSEI_END.Value = null;
            // 
            // SHINSEI_BEGIN
            // 
            this.SHINSEI_BEGIN.BackColor = System.Drawing.SystemColors.Window;
            this.SHINSEI_BEGIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHINSEI_BEGIN.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.SHINSEI_BEGIN.Checked = false;
            this.SHINSEI_BEGIN.DateTimeNowYear = "";
            this.SHINSEI_BEGIN.DBFieldsName = "TEKIYOU_BEGIN";
            this.SHINSEI_BEGIN.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHINSEI_BEGIN.DisplayItemName = "適用開始";
            this.SHINSEI_BEGIN.DisplayPopUp = null;
            this.SHINSEI_BEGIN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHINSEI_BEGIN.FocusOutCheckMethod")));
            this.SHINSEI_BEGIN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHINSEI_BEGIN.ForeColor = System.Drawing.Color.Black;
            this.SHINSEI_BEGIN.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SHINSEI_BEGIN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHINSEI_BEGIN.IsInputErrorOccured = false;
            this.SHINSEI_BEGIN.ItemDefinedTypes = "datetime";
            this.SHINSEI_BEGIN.Location = new System.Drawing.Point(131, 44);
            this.SHINSEI_BEGIN.MaxLength = 10;
            this.SHINSEI_BEGIN.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SHINSEI_BEGIN.Name = "SHINSEI_BEGIN";
            this.SHINSEI_BEGIN.NullValue = "";
            this.SHINSEI_BEGIN.PopupAfterExecute = null;
            this.SHINSEI_BEGIN.PopupBeforeExecute = null;
            this.SHINSEI_BEGIN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHINSEI_BEGIN.PopupSearchSendParams")));
            this.SHINSEI_BEGIN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHINSEI_BEGIN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHINSEI_BEGIN.popupWindowSetting")));
            this.SHINSEI_BEGIN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHINSEI_BEGIN.RegistCheckMethod")));
            this.SHINSEI_BEGIN.ShortItemName = "適用開始";
            this.SHINSEI_BEGIN.Size = new System.Drawing.Size(124, 20);
            this.SHINSEI_BEGIN.TabIndex = 115;
            this.SHINSEI_BEGIN.Tag = "適用開始日を入力して下さい";
            this.SHINSEI_BEGIN.Value = null;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Location = new System.Drawing.Point(826, 73);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Size = new System.Drawing.Size(84, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.TabIndex = 419;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Text = "適用期間外";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(772, 73);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(48, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 418;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = true;
            // 
            // ICHIRAN_HYOUJI_JOUKEN_TEKIYOU
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Location = new System.Drawing.Point(706, 73);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Name = "ICHIRAN_HYOUJI_JOUKEN_TEKIYOU";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Size = new System.Drawing.Size(60, 16);
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.TabIndex = 417;
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Text = "適用中";
            this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.UseVisualStyleBackColor = true;
            // 
            // lblShowCondition
            // 
            this.lblShowCondition.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblShowCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShowCondition.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblShowCondition.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblShowCondition.ForeColor = System.Drawing.Color.White;
            this.lblShowCondition.Location = new System.Drawing.Point(590, 70);
            this.lblShowCondition.Name = "lblShowCondition";
            this.lblShowCondition.Size = new System.Drawing.Size(110, 20);
            this.lblShowCondition.TabIndex = 420;
            this.lblShowCondition.Text = "表示条件";
            this.lblShowCondition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CONDITION_ITEM
            // 
            this.CONDITION_ITEM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONDITION_ITEM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_ITEM.CharactersNumber = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.CONDITION_ITEM.DBFieldsName = "";
            this.CONDITION_ITEM.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_ITEM.DisplayItemName = "検索条件";
            this.CONDITION_ITEM.DisplayPopUp = null;
            this.CONDITION_ITEM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_ITEM.Location = new System.Drawing.Point(131, 71);
            this.CONDITION_ITEM.MaxLength = 0;
            this.CONDITION_ITEM.Name = "CONDITION_ITEM";
            this.CONDITION_ITEM.PopupSendParams = new string[] {
        "Ichiran"};
            this.CONDITION_ITEM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM.PopupWindowName = "マスタ検索項目ポップアップ";
            this.CONDITION_ITEM.ReadOnly = true;
            this.CONDITION_ITEM.SetFormField = "CONDITION_ITEM,CONDITION_VALUE";
            this.CONDITION_ITEM.ShortItemName = "検索条件";
            this.CONDITION_ITEM.Size = new System.Drawing.Size(150, 20);
            this.CONDITION_ITEM.TabIndex = 421;
            this.CONDITION_ITEM.Tag = " 検索条件を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            // 
            // CONDITION_VALUE
            // 
            this.CONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE.CharactersNumber = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.CONDITION_VALUE.DBFieldsName = "";
            this.CONDITION_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE.DisplayItemName = "検索条件";
            this.CONDITION_VALUE.DisplayPopUp = null;
            this.CONDITION_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE.ItemDefinedTypes = "";
            this.CONDITION_VALUE.Location = new System.Drawing.Point(280, 71);
            this.CONDITION_VALUE.MaxLength = 0;
            this.CONDITION_VALUE.Name = "CONDITION_VALUE";
            this.CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE.PopupWindowName = "";
            this.CONDITION_VALUE.SetFormField = "";
            this.CONDITION_VALUE.ShortItemName = "検索条件";
            this.CONDITION_VALUE.Size = new System.Drawing.Size(290, 20);
            this.CONDITION_VALUE.TabIndex = 422;
            this.CONDITION_VALUE.Tag = "検索する文字を入力して下さい";
            // 
            // txtBoxShainName
            // 
            this.txtBoxShainName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtBoxShainName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxShainName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBoxShainName.DisplayPopUp = null;
            this.txtBoxShainName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBoxShainName.FocusOutCheckMethod")));
            this.txtBoxShainName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtBoxShainName.ForeColor = System.Drawing.Color.Black;
            this.txtBoxShainName.IsInputErrorOccured = false;
            this.txtBoxShainName.Location = new System.Drawing.Point(181, 18);
            this.txtBoxShainName.Name = "txtBoxShainName";
            this.txtBoxShainName.PopupAfterExecute = null;
            this.txtBoxShainName.PopupBeforeExecute = null;
            this.txtBoxShainName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBoxShainName.PopupSearchSendParams")));
            this.txtBoxShainName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtBoxShainName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtBoxShainName.popupWindowSetting")));
            this.txtBoxShainName.prevText = null;
            this.txtBoxShainName.ReadOnly = true;
            this.txtBoxShainName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBoxShainName.RegistCheckMethod")));
            this.txtBoxShainName.Size = new System.Drawing.Size(117, 20);
            this.txtBoxShainName.TabIndex = 424;
            this.txtBoxShainName.TabStop = false;
            // 
            // txtBoxShainCD
            // 
            this.txtBoxShainCD.BackColor = System.Drawing.SystemColors.Window;
            this.txtBoxShainCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBoxShainCD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.txtBoxShainCD.DBFieldsName = "SHAIN_CD";
            this.txtBoxShainCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtBoxShainCD.DisplayItemName = "社員CD";
            this.txtBoxShainCD.DisplayPopUp = null;
            this.txtBoxShainCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBoxShainCD.FocusOutCheckMethod")));
            this.txtBoxShainCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtBoxShainCD.ForeColor = System.Drawing.Color.Black;
            this.txtBoxShainCD.GetCodeMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.txtBoxShainCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtBoxShainCD.IsInputErrorOccured = false;
            this.txtBoxShainCD.ItemDefinedTypes = "smallint";
            this.txtBoxShainCD.Location = new System.Drawing.Point(132, 18);
            this.txtBoxShainCD.MaxLength = 6;
            this.txtBoxShainCD.Name = "txtBoxShainCD";
            this.txtBoxShainCD.PopupAfterExecute = null;
            this.txtBoxShainCD.PopupBeforeExecute = null;
            this.txtBoxShainCD.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.txtBoxShainCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtBoxShainCD.PopupSearchSendParams")));
            this.txtBoxShainCD.PopupSetFormField = "txtBox_Eigyotantosya,txtBox_Eigyosyamei";
            this.txtBoxShainCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.txtBoxShainCD.PopupWindowName = "マスタ共通ポップアップ";
            this.txtBoxShainCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtBoxShainCD.popupWindowSetting")));
            this.txtBoxShainCD.prevText = null;
            this.txtBoxShainCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtBoxShainCD.RegistCheckMethod")));
            this.txtBoxShainCD.SetFormField = "txtBox_Eigyotantosya,txtBox_Eigyosyamei";
            this.txtBoxShainCD.Size = new System.Drawing.Size(50, 20);
            this.txtBoxShainCD.TabIndex = 423;
            this.txtBoxShainCD.Tag = "営業担当者を指定して下さい（スペースキー押下にて、検索画面を表示します）";
            this.txtBoxShainCD.ZeroPaddengFlag = true;
            // 
            // ShouninzumiDenshiShinseiIchiranForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 458);
            this.Controls.Add(this.txtBoxShainName);
            this.Controls.Add(this.txtBoxShainCD);
            this.Controls.Add(this.CONDITION_VALUE);
            this.Controls.Add(this.CONDITION_ITEM);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU);
            this.Controls.Add(this.lblShowCondition);
            this.Controls.Add(this.lblFromTo);
            this.Controls.Add(this.SHINSEI_END);
            this.Controls.Add(this.SHINSEI_BEGIN);
            this.Controls.Add(this.lblSearchCondition);
            this.Controls.Add(this.lblShinseiDate);
            this.Controls.Add(this.lblShainName);
            this.Name = "ShouninzumiDenshiShinseiIchiranForm";
            this.Text = "ShouninzumiDenshiShinseiIchiran";
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.lblShainName, 0);
            this.Controls.SetChildIndex(this.lblShinseiDate, 0);
            this.Controls.SetChildIndex(this.lblSearchCondition, 0);
            this.Controls.SetChildIndex(this.SHINSEI_BEGIN, 0);
            this.Controls.SetChildIndex(this.SHINSEI_END, 0);
            this.Controls.SetChildIndex(this.lblFromTo, 0);
            this.Controls.SetChildIndex(this.lblShowCondition, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_DELETED, 0);
            this.Controls.SetChildIndex(this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI, 0);
            this.Controls.SetChildIndex(this.CONDITION_ITEM, 0);
            this.Controls.SetChildIndex(this.CONDITION_VALUE, 0);
            this.Controls.SetChildIndex(this.txtBoxShainCD, 0);
            this.Controls.SetChildIndex(this.txtBoxShainName, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblSearchCondition;
        private System.Windows.Forms.Label lblShinseiDate;
        private System.Windows.Forms.Label lblShainName;
        private System.Windows.Forms.Label lblFromTo;
        internal r_framework.CustomControl.CustomDateTimePicker SHINSEI_END;
        internal r_framework.CustomControl.CustomDateTimePicker SHINSEI_BEGIN;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        public System.Windows.Forms.CheckBox ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
        public System.Windows.Forms.Label lblShowCondition;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE;
        public r_framework.CustomControl.CustomTextBox txtBoxShainName;
        public r_framework.CustomControl.CustomTextBox txtBoxShainCD;
    }
}
