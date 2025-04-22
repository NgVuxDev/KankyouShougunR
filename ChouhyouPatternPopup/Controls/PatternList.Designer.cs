namespace ChouhyouPatternPopup.Controls
{
    partial class PatternList
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatternList));
            this.label1 = new System.Windows.Forms.Label();
            this.PATTERN_LIST_BOX = new r_framework.CustomControl.CustomListBox();
            this.SHUUKEI_FLAG_4 = new r_framework.CustomControl.CustomCheckBox();
            this.SHUUKEI_FLAG_3 = new r_framework.CustomControl.CustomCheckBox();
            this.SHUUKEI_FLAG_2 = new r_framework.CustomControl.CustomCheckBox();
            this.SHUUKEI_FLAG_1 = new r_framework.CustomControl.CustomCheckBox();
            this.SHUUKEI_KOUMOKU_4 = new r_framework.CustomControl.CustomComboBox();
            this.SHUUKEI_KOUMOKU_3 = new r_framework.CustomControl.CustomComboBox();
            this.SHUUKEI_KOUMOKU_2 = new r_framework.CustomControl.CustomComboBox();
            this.SHUUKEI_KOUMOKU_1 = new r_framework.CustomControl.CustomComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlMeisaiItem = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.SUURYOU_UNIT_DISP_FLG = new r_framework.CustomControl.CustomCheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.HINMEI_DISP_FLG = new r_framework.CustomControl.CustomCheckBox();
            this.NET_JUURYOU_DISP_FLG = new r_framework.CustomControl.CustomCheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlMeisaiItem.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 13);
            this.label1.TabIndex = 9999;
            this.label1.Text = "出力帳票を選択してください。";
            // 
            // PATTERN_LIST_BOX
            // 
            this.PATTERN_LIST_BOX.BackColor = System.Drawing.SystemColors.Window;
            this.PATTERN_LIST_BOX.DefaultBackColor = System.Drawing.Color.Empty;
            this.PATTERN_LIST_BOX.DisplayMember = "PATTERN_NAME";
            this.PATTERN_LIST_BOX.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_LIST_BOX.FocusOutCheckMethod")));
            this.PATTERN_LIST_BOX.FormattingEnabled = true;
            this.PATTERN_LIST_BOX.ItemHeight = 12;
            this.PATTERN_LIST_BOX.Location = new System.Drawing.Point(5, 25);
            this.PATTERN_LIST_BOX.Name = "PATTERN_LIST_BOX";
            this.PATTERN_LIST_BOX.PopupAfterExecute = null;
            this.PATTERN_LIST_BOX.PopupBeforeExecute = null;
            this.PATTERN_LIST_BOX.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_LIST_BOX.PopupSearchSendParams")));
            this.PATTERN_LIST_BOX.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_LIST_BOX.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_LIST_BOX.popupWindowSetting")));
            this.PATTERN_LIST_BOX.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_LIST_BOX.RegistCheckMethod")));
            this.PATTERN_LIST_BOX.Size = new System.Drawing.Size(290, 76);
            this.PATTERN_LIST_BOX.TabIndex = 0;
            this.PATTERN_LIST_BOX.SelectedIndexChanged += new System.EventHandler(this.PATTERN_LIST_BOX_SelectedIndexChanged);
            this.PATTERN_LIST_BOX.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.PATTERN_LIST_BOX_MouseDoubleClick);
            // 
            // SHUUKEI_FLAG_4
            // 
            this.SHUUKEI_FLAG_4.AutoSize = true;
            this.SHUUKEI_FLAG_4.BackColor = System.Drawing.SystemColors.Control;
            this.SHUUKEI_FLAG_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_4.Enabled = false;
            this.SHUUKEI_FLAG_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_4.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_4.Location = new System.Drawing.Point(530, 85);
            this.SHUUKEI_FLAG_4.Name = "SHUUKEI_FLAG_4";
            this.SHUUKEI_FLAG_4.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_4.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_4.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_4.popupWindowSetting")));
            this.SHUUKEI_FLAG_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_4.RegistCheckMethod")));
            this.SHUUKEI_FLAG_4.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_4.TabIndex = 10007;
            this.SHUUKEI_FLAG_4.Text = "集計";
            this.SHUUKEI_FLAG_4.UseVisualStyleBackColor = false;
            // 
            // SHUUKEI_FLAG_3
            // 
            this.SHUUKEI_FLAG_3.AutoSize = true;
            this.SHUUKEI_FLAG_3.BackColor = System.Drawing.SystemColors.Control;
            this.SHUUKEI_FLAG_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_3.Enabled = false;
            this.SHUUKEI_FLAG_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_3.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_3.Location = new System.Drawing.Point(530, 60);
            this.SHUUKEI_FLAG_3.Name = "SHUUKEI_FLAG_3";
            this.SHUUKEI_FLAG_3.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_3.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_3.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_3.popupWindowSetting")));
            this.SHUUKEI_FLAG_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_3.RegistCheckMethod")));
            this.SHUUKEI_FLAG_3.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_3.TabIndex = 10006;
            this.SHUUKEI_FLAG_3.Text = "集計";
            this.SHUUKEI_FLAG_3.UseVisualStyleBackColor = false;
            // 
            // SHUUKEI_FLAG_2
            // 
            this.SHUUKEI_FLAG_2.AutoSize = true;
            this.SHUUKEI_FLAG_2.BackColor = System.Drawing.SystemColors.Control;
            this.SHUUKEI_FLAG_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_2.Enabled = false;
            this.SHUUKEI_FLAG_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_2.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_2.Location = new System.Drawing.Point(530, 35);
            this.SHUUKEI_FLAG_2.Name = "SHUUKEI_FLAG_2";
            this.SHUUKEI_FLAG_2.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_2.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_2.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_2.popupWindowSetting")));
            this.SHUUKEI_FLAG_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_2.RegistCheckMethod")));
            this.SHUUKEI_FLAG_2.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_2.TabIndex = 10005;
            this.SHUUKEI_FLAG_2.Text = "集計";
            this.SHUUKEI_FLAG_2.UseVisualStyleBackColor = false;
            // 
            // SHUUKEI_FLAG_1
            // 
            this.SHUUKEI_FLAG_1.AutoSize = true;
            this.SHUUKEI_FLAG_1.BackColor = System.Drawing.SystemColors.Control;
            this.SHUUKEI_FLAG_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_FLAG_1.Enabled = false;
            this.SHUUKEI_FLAG_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_1.FocusOutCheckMethod")));
            this.SHUUKEI_FLAG_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_FLAG_1.Location = new System.Drawing.Point(530, 9);
            this.SHUUKEI_FLAG_1.Name = "SHUUKEI_FLAG_1";
            this.SHUUKEI_FLAG_1.PopupAfterExecute = null;
            this.SHUUKEI_FLAG_1.PopupBeforeExecute = null;
            this.SHUUKEI_FLAG_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_FLAG_1.PopupSearchSendParams")));
            this.SHUUKEI_FLAG_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_FLAG_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_FLAG_1.popupWindowSetting")));
            this.SHUUKEI_FLAG_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_FLAG_1.RegistCheckMethod")));
            this.SHUUKEI_FLAG_1.Size = new System.Drawing.Size(54, 17);
            this.SHUUKEI_FLAG_1.TabIndex = 10004;
            this.SHUUKEI_FLAG_1.Text = "集計";
            this.SHUUKEI_FLAG_1.UseVisualStyleBackColor = false;
            // 
            // SHUUKEI_KOUMOKU_4
            // 
            this.SHUUKEI_KOUMOKU_4.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUUKEI_KOUMOKU_4.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUUKEI_KOUMOKU_4.BackColor = System.Drawing.SystemColors.Window;
            this.SHUUKEI_KOUMOKU_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_KOUMOKU_4.DisplayMember = "KOUMOKU_RONRI_NAME";
            this.SHUUKEI_KOUMOKU_4.DisplayPopUp = null;
            this.SHUUKEI_KOUMOKU_4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUUKEI_KOUMOKU_4.Enabled = false;
            this.SHUUKEI_KOUMOKU_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_4.FocusOutCheckMethod")));
            this.SHUUKEI_KOUMOKU_4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_KOUMOKU_4.FormattingEnabled = true;
            this.SHUUKEI_KOUMOKU_4.IsInputErrorOccured = false;
            this.SHUUKEI_KOUMOKU_4.Location = new System.Drawing.Point(416, 82);
            this.SHUUKEI_KOUMOKU_4.Name = "SHUUKEI_KOUMOKU_4";
            this.SHUUKEI_KOUMOKU_4.PopupAfterExecute = null;
            this.SHUUKEI_KOUMOKU_4.PopupBeforeExecute = null;
            this.SHUUKEI_KOUMOKU_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_KOUMOKU_4.PopupSearchSendParams")));
            this.SHUUKEI_KOUMOKU_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_KOUMOKU_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_KOUMOKU_4.popupWindowSetting")));
            this.SHUUKEI_KOUMOKU_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_4.RegistCheckMethod")));
            this.SHUUKEI_KOUMOKU_4.Size = new System.Drawing.Size(104, 20);
            this.SHUUKEI_KOUMOKU_4.TabIndex = 10003;
            this.SHUUKEI_KOUMOKU_4.Tag = "";
            this.SHUUKEI_KOUMOKU_4.ValueMember = "KOUMOKU_RONRI_NAME";
            // 
            // SHUUKEI_KOUMOKU_3
            // 
            this.SHUUKEI_KOUMOKU_3.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUUKEI_KOUMOKU_3.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUUKEI_KOUMOKU_3.BackColor = System.Drawing.SystemColors.Window;
            this.SHUUKEI_KOUMOKU_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_KOUMOKU_3.DisplayMember = "KOUMOKU_RONRI_NAME";
            this.SHUUKEI_KOUMOKU_3.DisplayPopUp = null;
            this.SHUUKEI_KOUMOKU_3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUUKEI_KOUMOKU_3.Enabled = false;
            this.SHUUKEI_KOUMOKU_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_3.FocusOutCheckMethod")));
            this.SHUUKEI_KOUMOKU_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_KOUMOKU_3.FormattingEnabled = true;
            this.SHUUKEI_KOUMOKU_3.IsInputErrorOccured = false;
            this.SHUUKEI_KOUMOKU_3.Location = new System.Drawing.Point(416, 57);
            this.SHUUKEI_KOUMOKU_3.Name = "SHUUKEI_KOUMOKU_3";
            this.SHUUKEI_KOUMOKU_3.PopupAfterExecute = null;
            this.SHUUKEI_KOUMOKU_3.PopupBeforeExecute = null;
            this.SHUUKEI_KOUMOKU_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_KOUMOKU_3.PopupSearchSendParams")));
            this.SHUUKEI_KOUMOKU_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_KOUMOKU_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_KOUMOKU_3.popupWindowSetting")));
            this.SHUUKEI_KOUMOKU_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_3.RegistCheckMethod")));
            this.SHUUKEI_KOUMOKU_3.Size = new System.Drawing.Size(104, 20);
            this.SHUUKEI_KOUMOKU_3.TabIndex = 10002;
            this.SHUUKEI_KOUMOKU_3.Tag = "";
            this.SHUUKEI_KOUMOKU_3.ValueMember = "KOUMOKU_RONRI_NAME";
            // 
            // SHUUKEI_KOUMOKU_2
            // 
            this.SHUUKEI_KOUMOKU_2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUUKEI_KOUMOKU_2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUUKEI_KOUMOKU_2.BackColor = System.Drawing.SystemColors.Window;
            this.SHUUKEI_KOUMOKU_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_KOUMOKU_2.DisplayMember = "KOUMOKU_RONRI_NAME";
            this.SHUUKEI_KOUMOKU_2.DisplayPopUp = null;
            this.SHUUKEI_KOUMOKU_2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUUKEI_KOUMOKU_2.Enabled = false;
            this.SHUUKEI_KOUMOKU_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_2.FocusOutCheckMethod")));
            this.SHUUKEI_KOUMOKU_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_KOUMOKU_2.FormattingEnabled = true;
            this.SHUUKEI_KOUMOKU_2.IsInputErrorOccured = false;
            this.SHUUKEI_KOUMOKU_2.Location = new System.Drawing.Point(416, 32);
            this.SHUUKEI_KOUMOKU_2.Name = "SHUUKEI_KOUMOKU_2";
            this.SHUUKEI_KOUMOKU_2.PopupAfterExecute = null;
            this.SHUUKEI_KOUMOKU_2.PopupBeforeExecute = null;
            this.SHUUKEI_KOUMOKU_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_KOUMOKU_2.PopupSearchSendParams")));
            this.SHUUKEI_KOUMOKU_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_KOUMOKU_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_KOUMOKU_2.popupWindowSetting")));
            this.SHUUKEI_KOUMOKU_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_2.RegistCheckMethod")));
            this.SHUUKEI_KOUMOKU_2.Size = new System.Drawing.Size(104, 20);
            this.SHUUKEI_KOUMOKU_2.TabIndex = 10001;
            this.SHUUKEI_KOUMOKU_2.Tag = "";
            this.SHUUKEI_KOUMOKU_2.ValueMember = "KOUMOKU_RONRI_NAME";
            // 
            // SHUUKEI_KOUMOKU_1
            // 
            this.SHUUKEI_KOUMOKU_1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.SHUUKEI_KOUMOKU_1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.SHUUKEI_KOUMOKU_1.BackColor = System.Drawing.SystemColors.Window;
            this.SHUUKEI_KOUMOKU_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUUKEI_KOUMOKU_1.DisplayMember = "KOUMOKU_RONRI_NAME";
            this.SHUUKEI_KOUMOKU_1.DisplayPopUp = null;
            this.SHUUKEI_KOUMOKU_1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.SHUUKEI_KOUMOKU_1.Enabled = false;
            this.SHUUKEI_KOUMOKU_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_1.FocusOutCheckMethod")));
            this.SHUUKEI_KOUMOKU_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SHUUKEI_KOUMOKU_1.FormattingEnabled = true;
            this.SHUUKEI_KOUMOKU_1.IsInputErrorOccured = false;
            this.SHUUKEI_KOUMOKU_1.Location = new System.Drawing.Point(416, 7);
            this.SHUUKEI_KOUMOKU_1.Name = "SHUUKEI_KOUMOKU_1";
            this.SHUUKEI_KOUMOKU_1.PopupAfterExecute = null;
            this.SHUUKEI_KOUMOKU_1.PopupBeforeExecute = null;
            this.SHUUKEI_KOUMOKU_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUUKEI_KOUMOKU_1.PopupSearchSendParams")));
            this.SHUUKEI_KOUMOKU_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUUKEI_KOUMOKU_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUUKEI_KOUMOKU_1.popupWindowSetting")));
            this.SHUUKEI_KOUMOKU_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUUKEI_KOUMOKU_1.RegistCheckMethod")));
            this.SHUUKEI_KOUMOKU_1.Size = new System.Drawing.Size(104, 20);
            this.SHUUKEI_KOUMOKU_1.TabIndex = 10000;
            this.SHUUKEI_KOUMOKU_1.Tag = "";
            this.SHUUKEI_KOUMOKU_1.ValueMember = "KOUMOKU_RONRI_NAME";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(307, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 18);
            this.label2.TabIndex = 10008;
            this.label2.Text = "集計項目";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlMeisaiItem
            // 
            this.pnlMeisaiItem.Controls.Add(this.label4);
            this.pnlMeisaiItem.Controls.Add(this.SUURYOU_UNIT_DISP_FLG);
            this.pnlMeisaiItem.Controls.Add(this.label3);
            this.pnlMeisaiItem.Controls.Add(this.label6);
            this.pnlMeisaiItem.Controls.Add(this.HINMEI_DISP_FLG);
            this.pnlMeisaiItem.Controls.Add(this.NET_JUURYOU_DISP_FLG);
            this.pnlMeisaiItem.Controls.Add(this.label5);
            this.pnlMeisaiItem.Location = new System.Drawing.Point(588, 6);
            this.pnlMeisaiItem.Name = "pnlMeisaiItem";
            this.pnlMeisaiItem.Size = new System.Drawing.Size(256, 73);
            this.pnlMeisaiItem.TabIndex = 10009;
            this.pnlMeisaiItem.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(101, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 18);
            this.label4.TabIndex = 10001;
            this.label4.Text = "品名";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SUURYOU_UNIT_DISP_FLG
            // 
            this.SUURYOU_UNIT_DISP_FLG.AutoSize = true;
            this.SUURYOU_UNIT_DISP_FLG.BackColor = System.Drawing.SystemColors.Control;
            this.SUURYOU_UNIT_DISP_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.SUURYOU_UNIT_DISP_FLG.Enabled = false;
            this.SUURYOU_UNIT_DISP_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SUURYOU_UNIT_DISP_FLG.FocusOutCheckMethod")));
            this.SUURYOU_UNIT_DISP_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SUURYOU_UNIT_DISP_FLG.Location = new System.Drawing.Point(197, 54);
            this.SUURYOU_UNIT_DISP_FLG.Name = "SUURYOU_UNIT_DISP_FLG";
            this.SUURYOU_UNIT_DISP_FLG.PopupAfterExecute = null;
            this.SUURYOU_UNIT_DISP_FLG.PopupBeforeExecute = null;
            this.SUURYOU_UNIT_DISP_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SUURYOU_UNIT_DISP_FLG.PopupSearchSendParams")));
            this.SUURYOU_UNIT_DISP_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SUURYOU_UNIT_DISP_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SUURYOU_UNIT_DISP_FLG.popupWindowSetting")));
            this.SUURYOU_UNIT_DISP_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SUURYOU_UNIT_DISP_FLG.RegistCheckMethod")));
            this.SUURYOU_UNIT_DISP_FLG.Size = new System.Drawing.Size(54, 17);
            this.SUURYOU_UNIT_DISP_FLG.TabIndex = 73;
            this.SUURYOU_UNIT_DISP_FLG.Text = "表示";
            this.SUURYOU_UNIT_DISP_FLG.UseVisualStyleBackColor = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(5, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 18);
            this.label3.TabIndex = 10000;
            this.label3.Text = "明細項目";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(101, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(90, 18);
            this.label6.TabIndex = 10005;
            this.label6.Text = "数量/単位";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HINMEI_DISP_FLG
            // 
            this.HINMEI_DISP_FLG.AutoSize = true;
            this.HINMEI_DISP_FLG.BackColor = System.Drawing.SystemColors.Control;
            this.HINMEI_DISP_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_DISP_FLG.Enabled = false;
            this.HINMEI_DISP_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_DISP_FLG.FocusOutCheckMethod")));
            this.HINMEI_DISP_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HINMEI_DISP_FLG.Location = new System.Drawing.Point(197, 3);
            this.HINMEI_DISP_FLG.Name = "HINMEI_DISP_FLG";
            this.HINMEI_DISP_FLG.PopupAfterExecute = null;
            this.HINMEI_DISP_FLG.PopupBeforeExecute = null;
            this.HINMEI_DISP_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_DISP_FLG.PopupSearchSendParams")));
            this.HINMEI_DISP_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HINMEI_DISP_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_DISP_FLG.popupWindowSetting")));
            this.HINMEI_DISP_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_DISP_FLG.RegistCheckMethod")));
            this.HINMEI_DISP_FLG.Size = new System.Drawing.Size(54, 17);
            this.HINMEI_DISP_FLG.TabIndex = 71;
            this.HINMEI_DISP_FLG.Text = "表示";
            this.HINMEI_DISP_FLG.UseVisualStyleBackColor = false;
            // 
            // NET_JUURYOU_DISP_FLG
            // 
            this.NET_JUURYOU_DISP_FLG.AutoSize = true;
            this.NET_JUURYOU_DISP_FLG.BackColor = System.Drawing.SystemColors.Control;
            this.NET_JUURYOU_DISP_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.NET_JUURYOU_DISP_FLG.Enabled = false;
            this.NET_JUURYOU_DISP_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NET_JUURYOU_DISP_FLG.FocusOutCheckMethod")));
            this.NET_JUURYOU_DISP_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.NET_JUURYOU_DISP_FLG.Location = new System.Drawing.Point(197, 29);
            this.NET_JUURYOU_DISP_FLG.Name = "NET_JUURYOU_DISP_FLG";
            this.NET_JUURYOU_DISP_FLG.PopupAfterExecute = null;
            this.NET_JUURYOU_DISP_FLG.PopupBeforeExecute = null;
            this.NET_JUURYOU_DISP_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NET_JUURYOU_DISP_FLG.PopupSearchSendParams")));
            this.NET_JUURYOU_DISP_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NET_JUURYOU_DISP_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NET_JUURYOU_DISP_FLG.popupWindowSetting")));
            this.NET_JUURYOU_DISP_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NET_JUURYOU_DISP_FLG.RegistCheckMethod")));
            this.NET_JUURYOU_DISP_FLG.Size = new System.Drawing.Size(54, 17);
            this.NET_JUURYOU_DISP_FLG.TabIndex = 72;
            this.NET_JUURYOU_DISP_FLG.Text = "表示";
            this.NET_JUURYOU_DISP_FLG.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(101, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 18);
            this.label5.TabIndex = 10003;
            this.label5.Text = "正味重量";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PatternList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlMeisaiItem);
            this.Controls.Add(this.SHUUKEI_FLAG_4);
            this.Controls.Add(this.SHUUKEI_FLAG_3);
            this.Controls.Add(this.SHUUKEI_FLAG_2);
            this.Controls.Add(this.SHUUKEI_FLAG_1);
            this.Controls.Add(this.SHUUKEI_KOUMOKU_4);
            this.Controls.Add(this.SHUUKEI_KOUMOKU_3);
            this.Controls.Add(this.SHUUKEI_KOUMOKU_2);
            this.Controls.Add(this.SHUUKEI_KOUMOKU_1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PATTERN_LIST_BOX);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "PatternList";
            this.Size = new System.Drawing.Size(847, 108);
            this.pnlMeisaiItem.ResumeLayout(false);
            this.pnlMeisaiItem.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomListBox PATTERN_LIST_BOX;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_4;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_3;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_2;
        private r_framework.CustomControl.CustomCheckBox SHUUKEI_FLAG_1;
        private r_framework.CustomControl.CustomComboBox SHUUKEI_KOUMOKU_4;
        private r_framework.CustomControl.CustomComboBox SHUUKEI_KOUMOKU_3;
        private r_framework.CustomControl.CustomComboBox SHUUKEI_KOUMOKU_2;
        private r_framework.CustomControl.CustomComboBox SHUUKEI_KOUMOKU_1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlMeisaiItem;
        private System.Windows.Forms.Label label4;
        private r_framework.CustomControl.CustomCheckBox SUURYOU_UNIT_DISP_FLG;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private r_framework.CustomControl.CustomCheckBox HINMEI_DISP_FLG;
        private r_framework.CustomControl.CustomCheckBox NET_JUURYOU_DISP_FLG;
        private System.Windows.Forms.Label label5;

    }
}
