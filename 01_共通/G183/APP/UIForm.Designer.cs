namespace ContenaPopup.APP
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lb_title = new System.Windows.Forms.Label();
            this.PARENT_CONDITION4 = new r_framework.CustomControl.CustomRadioButton();
            this.PARENT_CONDITION3 = new r_framework.CustomControl.CustomRadioButton();
            this.PARENT_CONDITION2 = new r_framework.CustomControl.CustomRadioButton();
            this.PARENT_CONDITION1 = new r_framework.CustomControl.CustomRadioButton();
            this.PARENT_CONDITION_ITEM = new r_framework.CustomControl.CustomNumericTextBox2();
            this.CHILD_CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.customSortHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSortHeader();
            this.panel2 = new System.Windows.Forms.Panel();
            this.CHILD_CONDITION3 = new r_framework.CustomControl.CustomRadioButton();
            this.CHILD_CONDITION2 = new r_framework.CustomControl.CustomRadioButton();
            this.CHILD_CONDITION1 = new r_framework.CustomControl.CustomRadioButton();
            this.CHILD_CONDITION_ITEM = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customDataGridView1 = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.PARENT_CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.lb_hint = new System.Windows.Forms.Label();
            this.bt_func11 = new r_framework.CustomControl.CustomButton();
            this.bt_func10 = new r_framework.CustomControl.CustomButton();
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
            this.bt_func8 = new r_framework.CustomControl.CustomButton();
            this.bt_func7 = new r_framework.CustomControl.CustomButton();
            this.bt_func6 = new r_framework.CustomControl.CustomButton();
            this.bt_func5 = new r_framework.CustomControl.CustomButton();
            this.bt_func4 = new r_framework.CustomControl.CustomButton();
            this.bt_func3 = new r_framework.CustomControl.CustomButton();
            this.bt_func2 = new r_framework.CustomControl.CustomButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.bt_func1 = new r_framework.CustomControl.CustomButton();
            this.GENNBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GENNBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSYA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSYA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.customPopupOpenButton2 = new r_framework.CustomControl.CustomPopupOpenButton();
            this.customPopupOpenButton1 = new r_framework.CustomControl.CustomPopupOpenButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold);
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(12, 9);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(320, 34);
            this.lb_title.TabIndex = 508;
            this.lb_title.Text = "コンテナ名検索";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PARENT_CONDITION4
            // 
            this.PARENT_CONDITION4.AutoSize = true;
            this.PARENT_CONDITION4.DefaultBackColor = System.Drawing.Color.Empty;
            this.PARENT_CONDITION4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION4.FocusOutCheckMethod")));
            this.PARENT_CONDITION4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PARENT_CONDITION4.LinkedTextBox = "PARENT_CONDITION_ITEM";
            this.PARENT_CONDITION4.Location = new System.Drawing.Point(269, 0);
            this.PARENT_CONDITION4.Name = "PARENT_CONDITION4";
            this.PARENT_CONDITION4.PopupAfterExecute = null;
            this.PARENT_CONDITION4.PopupBeforeExecute = null;
            this.PARENT_CONDITION4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PARENT_CONDITION4.PopupSearchSendParams")));
            this.PARENT_CONDITION4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PARENT_CONDITION4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PARENT_CONDITION4.popupWindowSetting")));
            this.PARENT_CONDITION4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION4.RegistCheckMethod")));
            this.PARENT_CONDITION4.Size = new System.Drawing.Size(67, 17);
            this.PARENT_CONDITION4.TabIndex = 10;
            this.PARENT_CONDITION4.Tag = "フリーが対象の場合チェックを付けてください";
            this.PARENT_CONDITION4.Text = "4. ﾌﾘｰ";
            this.PARENT_CONDITION4.UseVisualStyleBackColor = true;
            this.PARENT_CONDITION4.Value = "4";
            // 
            // PARENT_CONDITION3
            // 
            this.PARENT_CONDITION3.AutoSize = true;
            this.PARENT_CONDITION3.DefaultBackColor = System.Drawing.Color.Empty;
            this.PARENT_CONDITION3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION3.FocusOutCheckMethod")));
            this.PARENT_CONDITION3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PARENT_CONDITION3.LinkedTextBox = "PARENT_CONDITION_ITEM";
            this.PARENT_CONDITION3.Location = new System.Drawing.Point(183, 0);
            this.PARENT_CONDITION3.Name = "PARENT_CONDITION3";
            this.PARENT_CONDITION3.PopupAfterExecute = null;
            this.PARENT_CONDITION3.PopupBeforeExecute = null;
            this.PARENT_CONDITION3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PARENT_CONDITION3.PopupSearchSendParams")));
            this.PARENT_CONDITION3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PARENT_CONDITION3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PARENT_CONDITION3.popupWindowSetting")));
            this.PARENT_CONDITION3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION3.RegistCheckMethod")));
            this.PARENT_CONDITION3.Size = new System.Drawing.Size(81, 17);
            this.PARENT_CONDITION3.TabIndex = 9;
            this.PARENT_CONDITION3.Tag = "ﾌﾘｶﾞﾅが対象の場合チェックを付けてください";
            this.PARENT_CONDITION3.Text = "3. ﾌﾘｶﾞﾅ";
            this.PARENT_CONDITION3.UseVisualStyleBackColor = true;
            this.PARENT_CONDITION3.Value = "3";
            // 
            // PARENT_CONDITION2
            // 
            this.PARENT_CONDITION2.AutoSize = true;
            this.PARENT_CONDITION2.DefaultBackColor = System.Drawing.Color.Empty;
            this.PARENT_CONDITION2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION2.FocusOutCheckMethod")));
            this.PARENT_CONDITION2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PARENT_CONDITION2.LinkedTextBox = "PARENT_CONDITION_ITEM";
            this.PARENT_CONDITION2.Location = new System.Drawing.Point(104, 0);
            this.PARENT_CONDITION2.Name = "PARENT_CONDITION2";
            this.PARENT_CONDITION2.PopupAfterExecute = null;
            this.PARENT_CONDITION2.PopupBeforeExecute = null;
            this.PARENT_CONDITION2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PARENT_CONDITION2.PopupSearchSendParams")));
            this.PARENT_CONDITION2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PARENT_CONDITION2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PARENT_CONDITION2.popupWindowSetting")));
            this.PARENT_CONDITION2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION2.RegistCheckMethod")));
            this.PARENT_CONDITION2.Size = new System.Drawing.Size(74, 17);
            this.PARENT_CONDITION2.TabIndex = 8;
            this.PARENT_CONDITION2.Tag = "略称が対象の場合チェックを付けてください";
            this.PARENT_CONDITION2.Text = "2. 略称";
            this.PARENT_CONDITION2.UseVisualStyleBackColor = true;
            this.PARENT_CONDITION2.Value = "2";
            // 
            // PARENT_CONDITION1
            // 
            this.PARENT_CONDITION1.AutoSize = true;
            this.PARENT_CONDITION1.DefaultBackColor = System.Drawing.Color.Empty;
            this.PARENT_CONDITION1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION1.FocusOutCheckMethod")));
            this.PARENT_CONDITION1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PARENT_CONDITION1.LinkedTextBox = "PARENT_CONDITION_ITEM";
            this.PARENT_CONDITION1.Location = new System.Drawing.Point(25, 0);
            this.PARENT_CONDITION1.Name = "PARENT_CONDITION1";
            this.PARENT_CONDITION1.PopupAfterExecute = null;
            this.PARENT_CONDITION1.PopupBeforeExecute = null;
            this.PARENT_CONDITION1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PARENT_CONDITION1.PopupSearchSendParams")));
            this.PARENT_CONDITION1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PARENT_CONDITION1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PARENT_CONDITION1.popupWindowSetting")));
            this.PARENT_CONDITION1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION1.RegistCheckMethod")));
            this.PARENT_CONDITION1.Size = new System.Drawing.Size(74, 17);
            this.PARENT_CONDITION1.TabIndex = 7;
            this.PARENT_CONDITION1.Tag = "コードが対象の場合チェックを付けてください";
            this.PARENT_CONDITION1.Text = "1. ｺｰﾄﾞ";
            this.PARENT_CONDITION1.UseVisualStyleBackColor = true;
            this.PARENT_CONDITION1.Value = "1";
            // 
            // PARENT_CONDITION_ITEM
            // 
            this.PARENT_CONDITION_ITEM.BackColor = System.Drawing.SystemColors.Window;
            this.PARENT_CONDITION_ITEM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PARENT_CONDITION_ITEM.DefaultBackColor = System.Drawing.Color.Empty;
            this.PARENT_CONDITION_ITEM.DisplayPopUp = null;
            this.PARENT_CONDITION_ITEM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION_ITEM.FocusOutCheckMethod")));
            this.PARENT_CONDITION_ITEM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PARENT_CONDITION_ITEM.ForeColor = System.Drawing.Color.Black;
            this.PARENT_CONDITION_ITEM.IsInputErrorOccured = false;
            this.PARENT_CONDITION_ITEM.LinkedRadioButtonArray = new string[] {
        "PARENT_CONDITION1",
        "PARENT_CONDITION2",
        "PARENT_CONDITION3",
        "PARENT_CONDITION4"};
            this.PARENT_CONDITION_ITEM.Location = new System.Drawing.Point(-1, -1);
            this.PARENT_CONDITION_ITEM.Name = "PARENT_CONDITION_ITEM";
            this.PARENT_CONDITION_ITEM.PopupAfterExecute = null;
            this.PARENT_CONDITION_ITEM.PopupBeforeExecute = null;
            this.PARENT_CONDITION_ITEM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PARENT_CONDITION_ITEM.PopupSearchSendParams")));
            this.PARENT_CONDITION_ITEM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PARENT_CONDITION_ITEM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PARENT_CONDITION_ITEM.popupWindowSetting")));
            this.PARENT_CONDITION_ITEM.prevText = null;
            this.PARENT_CONDITION_ITEM.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PARENT_CONDITION_ITEM.RangeSetting = rangeSettingDto1;
            this.PARENT_CONDITION_ITEM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION_ITEM.RegistCheckMethod")));
            this.PARENT_CONDITION_ITEM.Size = new System.Drawing.Size(20, 20);
            this.PARENT_CONDITION_ITEM.TabIndex = 6;
            this.PARENT_CONDITION_ITEM.Tag = "【1～4】のいずれかで入力してください";
            this.PARENT_CONDITION_ITEM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.PARENT_CONDITION_ITEM.WordWrap = false;
            this.PARENT_CONDITION_ITEM.Validated += new System.EventHandler(this.PARENT_CONDITION_ITEM_Validated);
            // 
            // CHILD_CONDITION_VALUE
            // 
            this.CHILD_CONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.CHILD_CONDITION_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CHILD_CONDITION_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHILD_CONDITION_VALUE.DisplayPopUp = null;
            this.CHILD_CONDITION_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHILD_CONDITION_VALUE.FocusOutCheckMethod")));
            this.CHILD_CONDITION_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CHILD_CONDITION_VALUE.ForeColor = System.Drawing.Color.Black;
            this.CHILD_CONDITION_VALUE.IsInputErrorOccured = false;
            this.CHILD_CONDITION_VALUE.Location = new System.Drawing.Point(341, -1);
            this.CHILD_CONDITION_VALUE.MaxLength = 10;
            this.CHILD_CONDITION_VALUE.Name = "CHILD_CONDITION_VALUE";
            this.CHILD_CONDITION_VALUE.PopupAfterExecute = null;
            this.CHILD_CONDITION_VALUE.PopupBeforeExecute = null;
            this.CHILD_CONDITION_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHILD_CONDITION_VALUE.PopupSearchSendParams")));
            this.CHILD_CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CHILD_CONDITION_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHILD_CONDITION_VALUE.popupWindowSetting")));
            this.CHILD_CONDITION_VALUE.prevText = null;
            this.CHILD_CONDITION_VALUE.PrevText = null;
            this.CHILD_CONDITION_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHILD_CONDITION_VALUE.RegistCheckMethod")));
            this.CHILD_CONDITION_VALUE.Size = new System.Drawing.Size(160, 20);
            this.CHILD_CONDITION_VALUE.TabIndex = 16;
            this.CHILD_CONDITION_VALUE.Tag = "検索文字列を入力してください";
            this.CHILD_CONDITION_VALUE.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CHILD_CONDITION_VALUE_KeyUp);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.TabIndex = 520;
            this.label3.Text = "コンテナ検索条件";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customSortHeader1.LinkedDataGridViewName = "customDataGridView1";
            this.customSortHeader1.Location = new System.Drawing.Point(9, 160);
            this.customSortHeader1.Name = "customSortHeader1";
            this.customSortHeader1.Size = new System.Drawing.Size(989, 27);
            this.customSortHeader1.SortFlag = false;
            this.customSortHeader1.TabIndex = 521;
            this.customSortHeader1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.CHILD_CONDITION_VALUE);
            this.panel2.Controls.Add(this.CHILD_CONDITION3);
            this.panel2.Controls.Add(this.CHILD_CONDITION2);
            this.panel2.Controls.Add(this.CHILD_CONDITION1);
            this.panel2.Controls.Add(this.CHILD_CONDITION_ITEM);
            this.panel2.Location = new System.Drawing.Point(155, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(502, 20);
            this.panel2.TabIndex = 5;
            // 
            // CHILD_CONDITION3
            // 
            this.CHILD_CONDITION3.AutoSize = true;
            this.CHILD_CONDITION3.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHILD_CONDITION3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHILD_CONDITION3.FocusOutCheckMethod")));
            this.CHILD_CONDITION3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CHILD_CONDITION3.LinkedTextBox = "CHILD_CONDITION_ITEM";
            this.CHILD_CONDITION3.Location = new System.Drawing.Point(183, 0);
            this.CHILD_CONDITION3.Name = "CHILD_CONDITION3";
            this.CHILD_CONDITION3.PopupAfterExecute = null;
            this.CHILD_CONDITION3.PopupBeforeExecute = null;
            this.CHILD_CONDITION3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHILD_CONDITION3.PopupSearchSendParams")));
            this.CHILD_CONDITION3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CHILD_CONDITION3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHILD_CONDITION3.popupWindowSetting")));
            this.CHILD_CONDITION3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHILD_CONDITION3.RegistCheckMethod")));
            this.CHILD_CONDITION3.Size = new System.Drawing.Size(67, 17);
            this.CHILD_CONDITION3.TabIndex = 15;
            this.CHILD_CONDITION3.Tag = "フリーが対象の場合チェックを付けてください";
            this.CHILD_CONDITION3.Text = "3. ﾌﾘｰ";
            this.CHILD_CONDITION3.UseVisualStyleBackColor = true;
            this.CHILD_CONDITION3.Value = "3";
            // 
            // CHILD_CONDITION2
            // 
            this.CHILD_CONDITION2.AutoSize = true;
            this.CHILD_CONDITION2.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHILD_CONDITION2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHILD_CONDITION2.FocusOutCheckMethod")));
            this.CHILD_CONDITION2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CHILD_CONDITION2.LinkedTextBox = "CHILD_CONDITION_ITEM";
            this.CHILD_CONDITION2.Location = new System.Drawing.Point(104, 0);
            this.CHILD_CONDITION2.Name = "CHILD_CONDITION2";
            this.CHILD_CONDITION2.PopupAfterExecute = null;
            this.CHILD_CONDITION2.PopupBeforeExecute = null;
            this.CHILD_CONDITION2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHILD_CONDITION2.PopupSearchSendParams")));
            this.CHILD_CONDITION2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CHILD_CONDITION2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHILD_CONDITION2.popupWindowSetting")));
            this.CHILD_CONDITION2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHILD_CONDITION2.RegistCheckMethod")));
            this.CHILD_CONDITION2.Size = new System.Drawing.Size(74, 17);
            this.CHILD_CONDITION2.TabIndex = 14;
            this.CHILD_CONDITION2.Tag = "略称が対象の場合チェックを付けてください";
            this.CHILD_CONDITION2.Text = "2. 略称";
            this.CHILD_CONDITION2.UseVisualStyleBackColor = true;
            this.CHILD_CONDITION2.Value = "2";
            // 
            // CHILD_CONDITION1
            // 
            this.CHILD_CONDITION1.AutoSize = true;
            this.CHILD_CONDITION1.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHILD_CONDITION1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHILD_CONDITION1.FocusOutCheckMethod")));
            this.CHILD_CONDITION1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CHILD_CONDITION1.LinkedTextBox = "CHILD_CONDITION_ITEM";
            this.CHILD_CONDITION1.Location = new System.Drawing.Point(25, 0);
            this.CHILD_CONDITION1.Name = "CHILD_CONDITION1";
            this.CHILD_CONDITION1.PopupAfterExecute = null;
            this.CHILD_CONDITION1.PopupBeforeExecute = null;
            this.CHILD_CONDITION1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHILD_CONDITION1.PopupSearchSendParams")));
            this.CHILD_CONDITION1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CHILD_CONDITION1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHILD_CONDITION1.popupWindowSetting")));
            this.CHILD_CONDITION1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHILD_CONDITION1.RegistCheckMethod")));
            this.CHILD_CONDITION1.Size = new System.Drawing.Size(74, 17);
            this.CHILD_CONDITION1.TabIndex = 13;
            this.CHILD_CONDITION1.Tag = "コードが対象の場合チェックを付けてください";
            this.CHILD_CONDITION1.Text = "1. ｺｰﾄﾞ";
            this.CHILD_CONDITION1.UseVisualStyleBackColor = true;
            this.CHILD_CONDITION1.Value = "1";
            // 
            // CHILD_CONDITION_ITEM
            // 
            this.CHILD_CONDITION_ITEM.BackColor = System.Drawing.SystemColors.Window;
            this.CHILD_CONDITION_ITEM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CHILD_CONDITION_ITEM.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHILD_CONDITION_ITEM.DisplayPopUp = null;
            this.CHILD_CONDITION_ITEM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHILD_CONDITION_ITEM.FocusOutCheckMethod")));
            this.CHILD_CONDITION_ITEM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CHILD_CONDITION_ITEM.ForeColor = System.Drawing.Color.Black;
            this.CHILD_CONDITION_ITEM.IsInputErrorOccured = false;
            this.CHILD_CONDITION_ITEM.LinkedRadioButtonArray = new string[] {
        "CHILD_CONDITION1",
        "CHILD_CONDITION2",
        "CHILD_CONDITION3"};
            this.CHILD_CONDITION_ITEM.Location = new System.Drawing.Point(-1, -1);
            this.CHILD_CONDITION_ITEM.Name = "CHILD_CONDITION_ITEM";
            this.CHILD_CONDITION_ITEM.PopupAfterExecute = null;
            this.CHILD_CONDITION_ITEM.PopupBeforeExecute = null;
            this.CHILD_CONDITION_ITEM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHILD_CONDITION_ITEM.PopupSearchSendParams")));
            this.CHILD_CONDITION_ITEM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CHILD_CONDITION_ITEM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHILD_CONDITION_ITEM.popupWindowSetting")));
            this.CHILD_CONDITION_ITEM.prevText = null;
            this.CHILD_CONDITION_ITEM.PrevText = null;
            rangeSettingDto2.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.CHILD_CONDITION_ITEM.RangeSetting = rangeSettingDto2;
            this.CHILD_CONDITION_ITEM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHILD_CONDITION_ITEM.RegistCheckMethod")));
            this.CHILD_CONDITION_ITEM.Size = new System.Drawing.Size(20, 20);
            this.CHILD_CONDITION_ITEM.TabIndex = 12;
            this.CHILD_CONDITION_ITEM.Tag = "【1～3】のいずれかで入力してください";
            this.CHILD_CONDITION_ITEM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.CHILD_CONDITION_ITEM.WordWrap = false;
            this.CHILD_CONDITION_ITEM.Validated += new System.EventHandler(this.CHILD_CONDITION_ITEM_Validated);
            // 
            // customDataGridView1
            // 
            this.customDataGridView1.AllowUserToAddRows = false;
            this.customDataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.customDataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.customDataGridView1.EnableHeadersVisualStyles = false;
            this.customDataGridView1.GridColor = System.Drawing.Color.White;
            this.customDataGridView1.IsReload = false;
            this.customDataGridView1.LinkedDataPanelName = "customSortHeader1";
            this.customDataGridView1.Location = new System.Drawing.Point(9, 188);
            this.customDataGridView1.MultiSelect = false;
            this.customDataGridView1.Name = "customDataGridView1";
            this.customDataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.customDataGridView1.RowHeadersVisible = false;
            this.customDataGridView1.RowTemplate.Height = 21;
            this.customDataGridView1.ShowCellToolTips = false;
            this.customDataGridView1.Size = new System.Drawing.Size(989, 380);
            this.customDataGridView1.TabIndex = 17;
            this.customDataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DetailCellDoubleClick);
            this.customDataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DetailKeyDown);
            this.customDataGridView1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DetailKeyUp);
            // 
            // PARENT_CONDITION_VALUE
            // 
            this.PARENT_CONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.PARENT_CONDITION_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PARENT_CONDITION_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.PARENT_CONDITION_VALUE.DisplayPopUp = null;
            this.PARENT_CONDITION_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION_VALUE.FocusOutCheckMethod")));
            this.PARENT_CONDITION_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PARENT_CONDITION_VALUE.ForeColor = System.Drawing.Color.Black;
            this.PARENT_CONDITION_VALUE.IsInputErrorOccured = false;
            this.PARENT_CONDITION_VALUE.Location = new System.Drawing.Point(341, -1);
            this.PARENT_CONDITION_VALUE.MaxLength = 10;
            this.PARENT_CONDITION_VALUE.Name = "PARENT_CONDITION_VALUE";
            this.PARENT_CONDITION_VALUE.PopupAfterExecute = null;
            this.PARENT_CONDITION_VALUE.PopupBeforeExecute = null;
            this.PARENT_CONDITION_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PARENT_CONDITION_VALUE.PopupSearchSendParams")));
            this.PARENT_CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PARENT_CONDITION_VALUE.PopupWindowName = "";
            this.PARENT_CONDITION_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PARENT_CONDITION_VALUE.popupWindowSetting")));
            this.PARENT_CONDITION_VALUE.prevText = null;
            this.PARENT_CONDITION_VALUE.PrevText = null;
            this.PARENT_CONDITION_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PARENT_CONDITION_VALUE.RegistCheckMethod")));
            this.PARENT_CONDITION_VALUE.Size = new System.Drawing.Size(160, 20);
            this.PARENT_CONDITION_VALUE.TabIndex = 11;
            this.PARENT_CONDITION_VALUE.Tag = "検索文字列を入力してください";
            this.PARENT_CONDITION_VALUE.KeyUp += new System.Windows.Forms.KeyEventHandler(this.PARENT_CONDITION_VALUE_KeyUp);
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func12.Location = new System.Drawing.Point(918, 602);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 22;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // lb_hint
            // 
            this.lb_hint.BackColor = System.Drawing.Color.Black;
            this.lb_hint.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lb_hint.ForeColor = System.Drawing.Color.Yellow;
            this.lb_hint.Location = new System.Drawing.Point(9, 577);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(989, 21);
            this.lb_hint.TabIndex = 512;
            this.lb_hint.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            this.lb_hint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_func11
            // 
            this.bt_func11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func11.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func11.Enabled = false;
            this.bt_func11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func11.Location = new System.Drawing.Point(837, 602);
            this.bt_func11.Name = "bt_func11";
            this.bt_func11.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func11.Size = new System.Drawing.Size(80, 35);
            this.bt_func11.TabIndex = 519;
            this.bt_func11.TabStop = false;
            this.bt_func11.Tag = "";
            this.bt_func11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func11.UseVisualStyleBackColor = false;
            // 
            // bt_func10
            // 
            this.bt_func10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func10.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func10.Enabled = false;
            this.bt_func10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func10.Location = new System.Drawing.Point(756, 602);
            this.bt_func10.Name = "bt_func10";
            this.bt_func10.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func10.Size = new System.Drawing.Size(80, 35);
            this.bt_func10.TabIndex = 21;
            this.bt_func10.TabStop = false;
            this.bt_func10.Tag = "";
            this.bt_func10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func10.UseVisualStyleBackColor = false;
            // 
            // bt_func9
            // 
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Enabled = false;
            this.bt_func9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func9.Location = new System.Drawing.Point(675, 602);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(80, 35);
            this.bt_func9.TabIndex = 20;
            this.bt_func9.TabStop = false;
            this.bt_func9.Tag = "";
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            // 
            // bt_func8
            // 
            this.bt_func8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func8.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func8.Enabled = false;
            this.bt_func8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func8.Location = new System.Drawing.Point(585, 602);
            this.bt_func8.Name = "bt_func8";
            this.bt_func8.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func8.Size = new System.Drawing.Size(80, 35);
            this.bt_func8.TabIndex = 19;
            this.bt_func8.TabStop = false;
            this.bt_func8.Tag = "";
            this.bt_func8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func8.UseVisualStyleBackColor = false;
            // 
            // bt_func7
            // 
            this.bt_func7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func7.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func7.Enabled = false;
            this.bt_func7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func7.Location = new System.Drawing.Point(504, 602);
            this.bt_func7.Name = "bt_func7";
            this.bt_func7.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func7.Size = new System.Drawing.Size(80, 35);
            this.bt_func7.TabIndex = 18;
            this.bt_func7.TabStop = false;
            this.bt_func7.Tag = "";
            this.bt_func7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func7.UseVisualStyleBackColor = false;
            // 
            // bt_func6
            // 
            this.bt_func6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func6.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func6.Enabled = false;
            this.bt_func6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func6.Location = new System.Drawing.Point(423, 602);
            this.bt_func6.Name = "bt_func6";
            this.bt_func6.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func6.Size = new System.Drawing.Size(80, 35);
            this.bt_func6.TabIndex = 17;
            this.bt_func6.TabStop = false;
            this.bt_func6.Tag = "";
            this.bt_func6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func6.UseVisualStyleBackColor = false;
            // 
            // bt_func5
            // 
            this.bt_func5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func5.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func5.Enabled = false;
            this.bt_func5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func5.Location = new System.Drawing.Point(342, 602);
            this.bt_func5.Name = "bt_func5";
            this.bt_func5.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func5.Size = new System.Drawing.Size(80, 35);
            this.bt_func5.TabIndex = 503;
            this.bt_func5.TabStop = false;
            this.bt_func5.Tag = "";
            this.bt_func5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func5.UseVisualStyleBackColor = false;
            // 
            // bt_func4
            // 
            this.bt_func4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func4.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func4.Enabled = false;
            this.bt_func4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func4.Location = new System.Drawing.Point(252, 602);
            this.bt_func4.Name = "bt_func4";
            this.bt_func4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func4.Size = new System.Drawing.Size(80, 35);
            this.bt_func4.TabIndex = 516;
            this.bt_func4.TabStop = false;
            this.bt_func4.Tag = "";
            this.bt_func4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func4.UseVisualStyleBackColor = false;
            // 
            // bt_func3
            // 
            this.bt_func3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func3.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func3.Enabled = false;
            this.bt_func3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func3.Location = new System.Drawing.Point(171, 602);
            this.bt_func3.Name = "bt_func3";
            this.bt_func3.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func3.Size = new System.Drawing.Size(80, 35);
            this.bt_func3.TabIndex = 515;
            this.bt_func3.TabStop = false;
            this.bt_func3.Tag = "";
            this.bt_func3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func3.UseVisualStyleBackColor = false;
            // 
            // bt_func2
            // 
            this.bt_func2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func2.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func2.Enabled = false;
            this.bt_func2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func2.Location = new System.Drawing.Point(90, 602);
            this.bt_func2.Name = "bt_func2";
            this.bt_func2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func2.Size = new System.Drawing.Size(80, 35);
            this.bt_func2.TabIndex = 514;
            this.bt_func2.TabStop = false;
            this.bt_func2.Tag = "";
            this.bt_func2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func2.UseVisualStyleBackColor = false;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.PARENT_CONDITION_VALUE);
            this.panel1.Controls.Add(this.PARENT_CONDITION4);
            this.panel1.Controls.Add(this.PARENT_CONDITION3);
            this.panel1.Controls.Add(this.PARENT_CONDITION2);
            this.panel1.Controls.Add(this.PARENT_CONDITION1);
            this.panel1.Controls.Add(this.PARENT_CONDITION_ITEM);
            this.panel1.Location = new System.Drawing.Point(155, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(502, 20);
            this.panel1.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(0, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(150, 20);
            this.label16.TabIndex = 511;
            this.label16.Text = "コンテナ種類検索条件";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func1
            // 
            this.bt_func1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func1.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func1.Enabled = false;
            this.bt_func1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func1.Location = new System.Drawing.Point(9, 602);
            this.bt_func1.Name = "bt_func1";
            this.bt_func1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func1.Size = new System.Drawing.Size(80, 35);
            this.bt_func1.TabIndex = 513;
            this.bt_func1.TabStop = false;
            this.bt_func1.Tag = "";
            this.bt_func1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func1.UseVisualStyleBackColor = false;
            // 
            // GENNBA_NAME_RYAKU
            // 
            this.GENNBA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENNBA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENNBA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GENNBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENNBA_NAME_RYAKU.DisplayPopUp = null;
            this.GENNBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENNBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENNBA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENNBA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GENNBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GENNBA_NAME_RYAKU.Location = new System.Drawing.Point(216, 84);
            this.GENNBA_NAME_RYAKU.Name = "GENNBA_NAME_RYAKU";
            this.GENNBA_NAME_RYAKU.PopupAfterExecute = null;
            this.GENNBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GENNBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENNBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENNBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENNBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENNBA_NAME_RYAKU.popupWindowSetting")));
            this.GENNBA_NAME_RYAKU.prevText = null;
            this.GENNBA_NAME_RYAKU.PrevText = null;
            this.GENNBA_NAME_RYAKU.ReadOnly = true;
            this.GENNBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENNBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENNBA_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.GENNBA_NAME_RYAKU.TabIndex = 529;
            this.GENNBA_NAME_RYAKU.TabStop = false;
            this.GENNBA_NAME_RYAKU.Tag = "検索する文字を入力してください";
            // 
            // GENNBA_CD
            // 
            this.GENNBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GENNBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENNBA_CD.ChangeUpperCase = true;
            this.GENNBA_CD.CharacterLimitList = null;
            this.GENNBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENNBA_CD.DBFieldsName = "GENBA_CD";
            this.GENNBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENNBA_CD.DisplayItemName = "";
            this.GENNBA_CD.DisplayPopUp = null;
            this.GENNBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENNBA_CD.FocusOutCheckMethod")));
            this.GENNBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GENNBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENNBA_CD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GENNBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENNBA_CD.IsInputErrorOccured = false;
            this.GENNBA_CD.ItemDefinedTypes = "varchar";
            this.GENNBA_CD.Location = new System.Drawing.Point(167, 84);
            this.GENNBA_CD.MaxLength = 6;
            this.GENNBA_CD.Name = "GENNBA_CD";
            this.GENNBA_CD.PopupAfterExecute = null;
            this.GENNBA_CD.PopupBeforeExecute = null;
            this.GENNBA_CD.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GENNBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENNBA_CD.PopupSearchSendParams")));
            this.GENNBA_CD.PopupSetFormField = "GENNBA_CD,GENNBA_NAME_RYAKU,GYOUSYA_CD,GYOUSYA_NAME_RYAKU";
            this.GENNBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENNBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENNBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENNBA_CD.popupWindowSetting")));
            this.GENNBA_CD.prevText = null;
            this.GENNBA_CD.PrevText = null;
            this.GENNBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENNBA_CD.RegistCheckMethod")));
            this.GENNBA_CD.SetFormField = "GENNBA_CD,GENNBA_NAME_RYAKU,GYOUSYA_CD,GYOUSYA_NAME_RYAKU";
            this.GENNBA_CD.ShortItemName = "";
            this.GENNBA_CD.Size = new System.Drawing.Size(50, 20);
            this.GENNBA_CD.TabIndex = 3;
            this.GENNBA_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.GENNBA_CD.ZeroPaddengFlag = true;
            this.GENNBA_CD.Validated += new System.EventHandler(this.GENBA_CD_Validated);
            // 
            // GYOUSYA_NAME_RYAKU
            // 
            this.GYOUSYA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSYA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSYA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GYOUSYA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSYA_NAME_RYAKU.DisplayPopUp = null;
            this.GYOUSYA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSYA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GYOUSYA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSYA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GYOUSYA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GYOUSYA_NAME_RYAKU.Location = new System.Drawing.Point(216, 62);
            this.GYOUSYA_NAME_RYAKU.Name = "GYOUSYA_NAME_RYAKU";
            this.GYOUSYA_NAME_RYAKU.PopupAfterExecute = null;
            this.GYOUSYA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GYOUSYA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSYA_NAME_RYAKU.PopupSearchSendParams")));
            this.GYOUSYA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSYA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSYA_NAME_RYAKU.popupWindowSetting")));
            this.GYOUSYA_NAME_RYAKU.prevText = null;
            this.GYOUSYA_NAME_RYAKU.PrevText = null;
            this.GYOUSYA_NAME_RYAKU.ReadOnly = true;
            this.GYOUSYA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSYA_NAME_RYAKU.RegistCheckMethod")));
            this.GYOUSYA_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.GYOUSYA_NAME_RYAKU.TabIndex = 527;
            this.GYOUSYA_NAME_RYAKU.TabStop = false;
            this.GYOUSYA_NAME_RYAKU.Tag = "検索する文字を入力してください";
            // 
            // GYOUSYA_CD
            // 
            this.GYOUSYA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSYA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSYA_CD.ChangeUpperCase = true;
            this.GYOUSYA_CD.CharacterLimitList = null;
            this.GYOUSYA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSYA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSYA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSYA_CD.DisplayItemName = "業者";
            this.GYOUSYA_CD.DisplayPopUp = null;
            this.GYOUSYA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSYA_CD.FocusOutCheckMethod")));
            this.GYOUSYA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.GYOUSYA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSYA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSYA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSYA_CD.IsInputErrorOccured = false;
            this.GYOUSYA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSYA_CD.Location = new System.Drawing.Point(167, 62);
            this.GYOUSYA_CD.MaxLength = 6;
            this.GYOUSYA_CD.Name = "GYOUSYA_CD";
            this.GYOUSYA_CD.PopupAfterExecute = null;
            this.GYOUSYA_CD.PopupAfterExecuteMethod = "GYOUSHA_CD_PopupAfterMethod";
            this.GYOUSYA_CD.PopupBeforeExecute = null;
            this.GYOUSYA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSYA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSYA_CD.PopupSearchSendParams")));
            this.GYOUSYA_CD.PopupSendParams = new string[0];
            this.GYOUSYA_CD.PopupSetFormField = "GYOUSYA_CD,GYOUSYA_NAME_RYAKU";
            this.GYOUSYA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSYA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSYA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSYA_CD.popupWindowSetting")));
            this.GYOUSYA_CD.prevText = null;
            this.GYOUSYA_CD.PrevText = null;
            this.GYOUSYA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSYA_CD.RegistCheckMethod")));
            this.GYOUSYA_CD.SetFormField = "GYOUSYA_CD,GYOUSYA_NAME_RYAKU";
            this.GYOUSYA_CD.ShortItemName = "業者CD";
            this.GYOUSYA_CD.Size = new System.Drawing.Size(50, 20);
            this.GYOUSYA_CD.TabIndex = 1;
            this.GYOUSYA_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSYA_CD.ZeroPaddengFlag = true;
            this.GYOUSYA_CD.Enter += new System.EventHandler(this.GYOUSHA_CD_Enter);
            this.GYOUSYA_CD.Validated += new System.EventHandler(this.GYOUSHA_CD_Validated);
            // 
            // customPopupOpenButton2
            // 
            this.customPopupOpenButton2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.customPopupOpenButton2.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.customPopupOpenButton2.DBFieldsName = null;
            this.customPopupOpenButton2.DefaultBackColor = System.Drawing.Color.Empty;
            this.customPopupOpenButton2.DisplayItemName = "";
            this.customPopupOpenButton2.DisplayPopUp = null;
            this.customPopupOpenButton2.ErrorMessage = null;
            this.customPopupOpenButton2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customPopupOpenButton2.FocusOutCheckMethod")));
            this.customPopupOpenButton2.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.customPopupOpenButton2.GetCodeMasterField = null;
            this.customPopupOpenButton2.Image = ((System.Drawing.Image)(resources.GetObject("customPopupOpenButton2.Image")));
            this.customPopupOpenButton2.ItemDefinedTypes = null;
            this.customPopupOpenButton2.LinkedSettingTextBox = null;
            this.customPopupOpenButton2.LinkedTextBoxs = null;
            this.customPopupOpenButton2.Location = new System.Drawing.Point(381, 83);
            this.customPopupOpenButton2.Name = "customPopupOpenButton2";
            this.customPopupOpenButton2.PopupAfterExecute = null;
            this.customPopupOpenButton2.PopupAfterExecuteMethod = "";
            this.customPopupOpenButton2.PopupBeforeExecute = null;
            this.customPopupOpenButton2.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.customPopupOpenButton2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customPopupOpenButton2.PopupSearchSendParams")));
            this.customPopupOpenButton2.PopupSetFormField = "GENNBA_CD,GENNBA_NAME_RYAKU";
            this.customPopupOpenButton2.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.customPopupOpenButton2.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.customPopupOpenButton2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customPopupOpenButton2.popupWindowSetting")));
            this.customPopupOpenButton2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customPopupOpenButton2.RegistCheckMethod")));
            this.customPopupOpenButton2.SearchDisplayFlag = 0;
            this.customPopupOpenButton2.SetFormField = "";
            this.customPopupOpenButton2.ShortItemName = "";
            this.customPopupOpenButton2.Size = new System.Drawing.Size(22, 22);
            this.customPopupOpenButton2.TabIndex = 4;
            this.customPopupOpenButton2.TabStop = false;
            this.customPopupOpenButton2.UseVisualStyleBackColor = false;
            this.customPopupOpenButton2.ZeroPaddengFlag = false;
            // 
            // customPopupOpenButton1
            // 
            this.customPopupOpenButton1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.customPopupOpenButton1.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.customPopupOpenButton1.DBFieldsName = null;
            this.customPopupOpenButton1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customPopupOpenButton1.DisplayItemName = "";
            this.customPopupOpenButton1.DisplayPopUp = null;
            this.customPopupOpenButton1.ErrorMessage = null;
            this.customPopupOpenButton1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customPopupOpenButton1.FocusOutCheckMethod")));
            this.customPopupOpenButton1.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.customPopupOpenButton1.GetCodeMasterField = null;
            this.customPopupOpenButton1.Image = ((System.Drawing.Image)(resources.GetObject("customPopupOpenButton1.Image")));
            this.customPopupOpenButton1.ItemDefinedTypes = null;
            this.customPopupOpenButton1.LinkedSettingTextBox = null;
            this.customPopupOpenButton1.LinkedTextBoxs = null;
            this.customPopupOpenButton1.Location = new System.Drawing.Point(381, 61);
            this.customPopupOpenButton1.Name = "customPopupOpenButton1";
            this.customPopupOpenButton1.PopupAfterExecute = null;
            this.customPopupOpenButton1.PopupAfterExecuteMethod = "GYOUSHA_CD_PopupAfterMethod";
            this.customPopupOpenButton1.PopupBeforeExecute = null;
            this.customPopupOpenButton1.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.customPopupOpenButton1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customPopupOpenButton1.PopupSearchSendParams")));
            this.customPopupOpenButton1.PopupSetFormField = "GYOUSYA_CD,GYOUSYA_NAME_RYAKU";
            this.customPopupOpenButton1.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.customPopupOpenButton1.PopupWindowName = "検索共通ポップアップ";
            this.customPopupOpenButton1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customPopupOpenButton1.popupWindowSetting")));
            this.customPopupOpenButton1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customPopupOpenButton1.RegistCheckMethod")));
            this.customPopupOpenButton1.SearchDisplayFlag = 0;
            this.customPopupOpenButton1.SetFormField = "";
            this.customPopupOpenButton1.ShortItemName = "";
            this.customPopupOpenButton1.Size = new System.Drawing.Size(22, 22);
            this.customPopupOpenButton1.TabIndex = 2;
            this.customPopupOpenButton1.TabStop = false;
            this.customPopupOpenButton1.UseVisualStyleBackColor = false;
            this.customPopupOpenButton1.ZeroPaddengFlag = false;
            this.customPopupOpenButton1.Enter += new System.EventHandler(this.GYOUSHA_CD_Enter);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 84);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 20);
            this.label5.TabIndex = 528;
            this.label5.Text = "現場";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 20);
            this.label1.TabIndex = 526;
            this.label1.Text = "業者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.label16);
            this.customPanel1.Controls.Add(this.label3);
            this.customPanel1.Controls.Add(this.panel1);
            this.customPanel1.Controls.Add(this.panel2);
            this.customPanel1.Location = new System.Drawing.Point(12, 106);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(657, 42);
            this.customPanel1.TabIndex = 5;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 647);
            this.Controls.Add(this.GENNBA_NAME_RYAKU);
            this.Controls.Add(this.GENNBA_CD);
            this.Controls.Add(this.GYOUSYA_NAME_RYAKU);
            this.Controls.Add(this.GYOUSYA_CD);
            this.Controls.Add(this.customPopupOpenButton2);
            this.Controls.Add(this.customPopupOpenButton1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.customSortHeader1);
            this.Controls.Add(this.customDataGridView1);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.lb_hint);
            this.Controls.Add(this.bt_func11);
            this.Controls.Add(this.bt_func10);
            this.Controls.Add(this.bt_func9);
            this.Controls.Add(this.bt_func8);
            this.Controls.Add(this.bt_func7);
            this.Controls.Add(this.bt_func6);
            this.Controls.Add(this.bt_func5);
            this.Controls.Add(this.bt_func4);
            this.Controls.Add(this.bt_func3);
            this.Controls.Add(this.bt_func2);
            this.Controls.Add(this.bt_func1);
            this.Controls.Add(this.customPanel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UIForm";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.ContenaPopupForm_KeyUp);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        internal r_framework.CustomControl.CustomRadioButton PARENT_CONDITION4;
        internal r_framework.CustomControl.CustomNumericTextBox2 PARENT_CONDITION_ITEM;
        internal r_framework.CustomControl.CustomTextBox CHILD_CONDITION_VALUE;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Panel panel2;
        internal r_framework.CustomControl.CustomRadioButton CHILD_CONDITION3;
        private r_framework.CustomControl.CustomRadioButton CHILD_CONDITION2;
        private r_framework.CustomControl.CustomRadioButton CHILD_CONDITION1;
        internal r_framework.CustomControl.CustomNumericTextBox2 CHILD_CONDITION_ITEM;
        internal r_framework.CustomControl.CustomTextBox PARENT_CONDITION_VALUE;
        public r_framework.CustomControl.CustomButton bt_func12;
        public System.Windows.Forms.Label lb_hint;
        public r_framework.CustomControl.CustomButton bt_func11;
        public r_framework.CustomControl.CustomButton bt_func10;
        public r_framework.CustomControl.CustomButton bt_func9;
        public r_framework.CustomControl.CustomButton bt_func8;
        public r_framework.CustomControl.CustomButton bt_func7;
        public r_framework.CustomControl.CustomButton bt_func6;
        public r_framework.CustomControl.CustomButton bt_func5;
        public r_framework.CustomControl.CustomButton bt_func4;
        public r_framework.CustomControl.CustomButton bt_func3;
        public r_framework.CustomControl.CustomButton bt_func2;
        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Label label16;
        public r_framework.CustomControl.CustomButton bt_func1;
        public r_framework.CustomControl.CustomTextBox GENNBA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENNBA_CD;
        public r_framework.CustomControl.CustomTextBox GYOUSYA_NAME_RYAKU;
        public r_framework.CustomControl.CustomAlphaNumTextBox GYOUSYA_CD;
        public r_framework.CustomControl.CustomPopupOpenButton customPopupOpenButton2;
        public r_framework.CustomControl.CustomPopupOpenButton customPopupOpenButton1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        public r_framework.CustomControl.DataGridCustomControl.CustomSortHeader customSortHeader1;
        public r_framework.CustomControl.CustomDataGridView customDataGridView1;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomRadioButton PARENT_CONDITION3;
        internal r_framework.CustomControl.CustomRadioButton PARENT_CONDITION2;
        internal r_framework.CustomControl.CustomRadioButton PARENT_CONDITION1;
    }
}