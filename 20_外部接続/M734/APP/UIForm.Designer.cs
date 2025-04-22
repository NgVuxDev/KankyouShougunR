namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle37 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle38 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle39 = new System.Windows.Forms.DataGridViewCellStyle();
            this.KEIYAKU_JYOUKYOU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KEIYAKU_JYOUKYOU_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label6 = new System.Windows.Forms.Label();
            this.DATE_SELECT_6 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_5 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_4 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_3 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_2 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT_1 = new r_framework.CustomControl.CustomRadioButton();
            this.DATE_SELECT = new r_framework.CustomControl.CustomNumericTextBox2();
            this.labelInfo = new System.Windows.Forms.Label();
            this.DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.CONDITION_ITEM1 = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_VALUE1 = new r_framework.CustomControl.CustomTextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.CONDITION_ITEM3 = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_VALUE3 = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CONDITION_ITEM4 = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_VALUE4 = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CONDITION_ITEM2 = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_VALUE2 = new r_framework.CustomControl.CustomTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SOUFU_TITLE = new r_framework.CustomControl.CustomTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.FILE_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.KEIYAKUSHA = new r_framework.CustomControl.CustomTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgv_CONDITION = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.dgvCustomTextBoxColumn1 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn2 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn3 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn4 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn5 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn6 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn7 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn8 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn9 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn10 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CONDITION)).BeginInit();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.ReadOnly = true;
            this.searchString.Size = new System.Drawing.Size(1010, 139);
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(3, 466);
            this.bt_ptn1.TabIndex = 240;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(204, 466);
            this.bt_ptn2.TabIndex = 250;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(405, 466);
            this.bt_ptn3.TabIndex = 260;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(606, 466);
            this.bt_ptn4.TabIndex = 270;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(807, 466);
            this.bt_ptn5.TabIndex = 280;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Location = new System.Drawing.Point(4, 172);
            this.customSortHeader1.TabIndex = 230;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Location = new System.Drawing.Point(6, 147);
            this.customSearchHeader1.TabIndex = 220;
            this.customSearchHeader1.Visible = true;
            // 
            // KEIYAKU_JYOUKYOU_NAME
            // 
            this.KEIYAKU_JYOUKYOU_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KEIYAKU_JYOUKYOU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKU_JYOUKYOU_NAME.DBFieldsName = "KEIYAKU_JYOUKYOU_NAME";
            this.KEIYAKU_JYOUKYOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKU_JYOUKYOU_NAME.DisplayPopUp = null;
            this.KEIYAKU_JYOUKYOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.FocusOutCheckMethod")));
            this.KEIYAKU_JYOUKYOU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KEIYAKU_JYOUKYOU_NAME.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKU_JYOUKYOU_NAME.IsInputErrorOccured = false;
            this.KEIYAKU_JYOUKYOU_NAME.Location = new System.Drawing.Point(138, 9);
            this.KEIYAKU_JYOUKYOU_NAME.Name = "KEIYAKU_JYOUKYOU_NAME";
            this.KEIYAKU_JYOUKYOU_NAME.PopupAfterExecute = null;
            this.KEIYAKU_JYOUKYOU_NAME.PopupBeforeExecute = null;
            this.KEIYAKU_JYOUKYOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.PopupSearchSendParams")));
            this.KEIYAKU_JYOUKYOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKU_JYOUKYOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.popupWindowSetting")));
            this.KEIYAKU_JYOUKYOU_NAME.ReadOnly = true;
            this.KEIYAKU_JYOUKYOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.RegistCheckMethod")));
            this.KEIYAKU_JYOUKYOU_NAME.Size = new System.Drawing.Size(115, 20);
            this.KEIYAKU_JYOUKYOU_NAME.TabIndex = 660;
            this.KEIYAKU_JYOUKYOU_NAME.TabStop = false;
            // 
            // KEIYAKU_JYOUKYOU_CD
            // 
            this.KEIYAKU_JYOUKYOU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KEIYAKU_JYOUKYOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKU_JYOUKYOU_CD.DBFieldsName = "KEIYAKU_JYOUKYOU_CD";
            this.KEIYAKU_JYOUKYOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKU_JYOUKYOU_CD.DisplayItemName = "契約状況";
            this.KEIYAKU_JYOUKYOU_CD.DisplayPopUp = null;
            this.KEIYAKU_JYOUKYOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.FocusOutCheckMethod")));
            this.KEIYAKU_JYOUKYOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KEIYAKU_JYOUKYOU_CD.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKU_JYOUKYOU_CD.IsInputErrorOccured = false;
            this.KEIYAKU_JYOUKYOU_CD.Location = new System.Drawing.Point(117, 9);
            this.KEIYAKU_JYOUKYOU_CD.Name = "KEIYAKU_JYOUKYOU_CD";
            this.KEIYAKU_JYOUKYOU_CD.PopupAfterExecute = null;
            this.KEIYAKU_JYOUKYOU_CD.PopupBeforeExecute = null;
            this.KEIYAKU_JYOUKYOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.PopupSearchSendParams")));
            this.KEIYAKU_JYOUKYOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKU_JYOUKYOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.KEIYAKU_JYOUKYOU_CD.RangeSetting = rangeSettingDto2;
            this.KEIYAKU_JYOUKYOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.RegistCheckMethod")));
            this.KEIYAKU_JYOUKYOU_CD.ShortItemName = "契約状況";
            this.KEIYAKU_JYOUKYOU_CD.Size = new System.Drawing.Size(20, 20);
            this.KEIYAKU_JYOUKYOU_CD.TabIndex = 10;
            this.KEIYAKU_JYOUKYOU_CD.Tag = "契約状況を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KEIYAKU_JYOUKYOU_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.KEIYAKU_JYOUKYOU_CD.WordWrap = false;
            this.KEIYAKU_JYOUKYOU_CD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Keiyaku_Jyoukyou_KeyDown);
            this.KEIYAKU_JYOUKYOU_CD.Validated += new System.EventHandler(this.KEIYAKU_JYOUKYOU_CD_Validated);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(4, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 658;
            this.label6.Text = "契約状況";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DATE_SELECT_6
            // 
            this.DATE_SELECT_6.AutoSize = true;
            this.DATE_SELECT_6.DBFieldsName = "";
            this.DATE_SELECT_6.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_6.DisplayItemName = "更新種別";
            this.DATE_SELECT_6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_6.FocusOutCheckMethod")));
            this.DATE_SELECT_6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_6.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_6.Location = new System.Drawing.Point(635, 33);
            this.DATE_SELECT_6.Name = "DATE_SELECT_6";
            this.DATE_SELECT_6.PopupAfterExecute = null;
            this.DATE_SELECT_6.PopupBeforeExecute = null;
            this.DATE_SELECT_6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_6.PopupSearchSendParams")));
            this.DATE_SELECT_6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_6.popupWindowSetting")));
            this.DATE_SELECT_6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_6.RegistCheckMethod")));
            this.DATE_SELECT_6.ShortItemName = "更新種別";
            this.DATE_SELECT_6.Size = new System.Drawing.Size(95, 17);
            this.DATE_SELECT_6.TabIndex = 80;
            this.DATE_SELECT_6.Tag = "日付なしで検索を行う場合チェックを付けてください";
            this.DATE_SELECT_6.Text = "6.日付なし";
            this.DATE_SELECT_6.UseVisualStyleBackColor = true;
            this.DATE_SELECT_6.Value = "6";
            // 
            // DATE_SELECT_5
            // 
            this.DATE_SELECT_5.AutoSize = true;
            this.DATE_SELECT_5.DBFieldsName = "";
            this.DATE_SELECT_5.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_5.DisplayItemName = "更新種別";
            this.DATE_SELECT_5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_5.FocusOutCheckMethod")));
            this.DATE_SELECT_5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_5.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_5.Location = new System.Drawing.Point(526, 33);
            this.DATE_SELECT_5.Name = "DATE_SELECT_5";
            this.DATE_SELECT_5.PopupAfterExecute = null;
            this.DATE_SELECT_5.PopupBeforeExecute = null;
            this.DATE_SELECT_5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_5.PopupSearchSendParams")));
            this.DATE_SELECT_5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_5.popupWindowSetting")));
            this.DATE_SELECT_5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_5.RegistCheckMethod")));
            this.DATE_SELECT_5.ShortItemName = "更新種別";
            this.DATE_SELECT_5.Size = new System.Drawing.Size(109, 17);
            this.DATE_SELECT_5.TabIndex = 70;
            this.DATE_SELECT_5.Tag = "契約終了日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_5.Text = "5.契約終了日";
            this.DATE_SELECT_5.UseVisualStyleBackColor = true;
            this.DATE_SELECT_5.Value = "5";
            // 
            // DATE_SELECT_4
            // 
            this.DATE_SELECT_4.AutoSize = true;
            this.DATE_SELECT_4.DBFieldsName = "";
            this.DATE_SELECT_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_4.DisplayItemName = "更新種別";
            this.DATE_SELECT_4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_4.FocusOutCheckMethod")));
            this.DATE_SELECT_4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_4.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_4.Location = new System.Drawing.Point(416, 33);
            this.DATE_SELECT_4.Name = "DATE_SELECT_4";
            this.DATE_SELECT_4.PopupAfterExecute = null;
            this.DATE_SELECT_4.PopupBeforeExecute = null;
            this.DATE_SELECT_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_4.PopupSearchSendParams")));
            this.DATE_SELECT_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_4.popupWindowSetting")));
            this.DATE_SELECT_4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_4.RegistCheckMethod")));
            this.DATE_SELECT_4.ShortItemName = "更新種別";
            this.DATE_SELECT_4.Size = new System.Drawing.Size(109, 17);
            this.DATE_SELECT_4.TabIndex = 60;
            this.DATE_SELECT_4.Tag = "契約開始日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_4.Text = "4.契約開始日";
            this.DATE_SELECT_4.UseVisualStyleBackColor = true;
            this.DATE_SELECT_4.Value = "4";
            // 
            // DATE_SELECT_3
            // 
            this.DATE_SELECT_3.AutoSize = true;
            this.DATE_SELECT_3.DBFieldsName = "";
            this.DATE_SELECT_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_3.DisplayItemName = "更新種別";
            this.DATE_SELECT_3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_3.FocusOutCheckMethod")));
            this.DATE_SELECT_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_3.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_3.Location = new System.Drawing.Point(306, 33);
            this.DATE_SELECT_3.Name = "DATE_SELECT_3";
            this.DATE_SELECT_3.PopupAfterExecute = null;
            this.DATE_SELECT_3.PopupBeforeExecute = null;
            this.DATE_SELECT_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_3.PopupSearchSendParams")));
            this.DATE_SELECT_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_3.popupWindowSetting")));
            this.DATE_SELECT_3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_3.RegistCheckMethod")));
            this.DATE_SELECT_3.ShortItemName = "更新種別";
            this.DATE_SELECT_3.Size = new System.Drawing.Size(109, 17);
            this.DATE_SELECT_3.TabIndex = 50;
            this.DATE_SELECT_3.Tag = "契約締結日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_3.Text = "3.契約締結日";
            this.DATE_SELECT_3.UseVisualStyleBackColor = true;
            this.DATE_SELECT_3.Value = "3";
            // 
            // DATE_SELECT_2
            // 
            this.DATE_SELECT_2.AutoSize = true;
            this.DATE_SELECT_2.DBFieldsName = "";
            this.DATE_SELECT_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_2.DisplayItemName = "更新種別";
            this.DATE_SELECT_2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_2.FocusOutCheckMethod")));
            this.DATE_SELECT_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_2.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_2.Location = new System.Drawing.Point(223, 33);
            this.DATE_SELECT_2.Name = "DATE_SELECT_2";
            this.DATE_SELECT_2.PopupAfterExecute = null;
            this.DATE_SELECT_2.PopupBeforeExecute = null;
            this.DATE_SELECT_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_2.PopupSearchSendParams")));
            this.DATE_SELECT_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_2.popupWindowSetting")));
            this.DATE_SELECT_2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_2.RegistCheckMethod")));
            this.DATE_SELECT_2.ShortItemName = "更新種別";
            this.DATE_SELECT_2.Size = new System.Drawing.Size(81, 17);
            this.DATE_SELECT_2.TabIndex = 40;
            this.DATE_SELECT_2.Tag = "更新日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_2.Text = "2.更新日";
            this.DATE_SELECT_2.UseVisualStyleBackColor = true;
            this.DATE_SELECT_2.Value = "2";
            // 
            // DATE_SELECT_1
            // 
            this.DATE_SELECT_1.AutoSize = true;
            this.DATE_SELECT_1.DBFieldsName = "";
            this.DATE_SELECT_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT_1.DisplayItemName = "更新種別";
            this.DATE_SELECT_1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_1.FocusOutCheckMethod")));
            this.DATE_SELECT_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT_1.LinkedTextBox = "DATE_SELECT";
            this.DATE_SELECT_1.Location = new System.Drawing.Point(142, 33);
            this.DATE_SELECT_1.Name = "DATE_SELECT_1";
            this.DATE_SELECT_1.PopupAfterExecute = null;
            this.DATE_SELECT_1.PopupBeforeExecute = null;
            this.DATE_SELECT_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT_1.PopupSearchSendParams")));
            this.DATE_SELECT_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT_1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT_1.popupWindowSetting")));
            this.DATE_SELECT_1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT_1.RegistCheckMethod")));
            this.DATE_SELECT_1.ShortItemName = "更新種別";
            this.DATE_SELECT_1.Size = new System.Drawing.Size(81, 17);
            this.DATE_SELECT_1.TabIndex = 30;
            this.DATE_SELECT_1.Tag = "作成日で検索を行う場合チェックを付けてください";
            this.DATE_SELECT_1.Text = "1.作成日";
            this.DATE_SELECT_1.UseVisualStyleBackColor = true;
            this.DATE_SELECT_1.Value = "1";
            // 
            // DATE_SELECT
            // 
            this.DATE_SELECT.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_SELECT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_SELECT.DBFieldsName = "";
            this.DATE_SELECT.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_SELECT.DisplayItemName = "検索条件";
            this.DATE_SELECT.DisplayPopUp = null;
            this.DATE_SELECT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT.FocusOutCheckMethod")));
            this.DATE_SELECT.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DATE_SELECT.ForeColor = System.Drawing.Color.Black;
            this.DATE_SELECT.IsInputErrorOccured = false;
            this.DATE_SELECT.ItemDefinedTypes = "";
            this.DATE_SELECT.LinkedRadioButtonArray = new string[] {
        "DATE_SELECT_1",
        "DATE_SELECT_2",
        "DATE_SELECT_3",
        "DATE_SELECT_4",
        "DATE_SELECT_5",
        "DATE_SELECT_6",
        ""};
            this.DATE_SELECT.Location = new System.Drawing.Point(117, 31);
            this.DATE_SELECT.Name = "DATE_SELECT";
            this.DATE_SELECT.PopupAfterExecute = null;
            this.DATE_SELECT.PopupBeforeExecute = null;
            this.DATE_SELECT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_SELECT.PopupSearchSendParams")));
            this.DATE_SELECT.PopupSetFormField = "";
            this.DATE_SELECT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_SELECT.PopupWindowName = "";
            this.DATE_SELECT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_SELECT.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            6,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DATE_SELECT.RangeSetting = rangeSettingDto3;
            this.DATE_SELECT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_SELECT.RegistCheckMethod")));
            this.DATE_SELECT.SetFormField = "";
            this.DATE_SELECT.ShortItemName = "検索条件";
            this.DATE_SELECT.Size = new System.Drawing.Size(20, 20);
            this.DATE_SELECT.TabIndex = 20;
            this.DATE_SELECT.Tag = "【1～6】のいずれかで入力してください";
            this.DATE_SELECT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.DATE_SELECT.WordWrap = false;
            this.DATE_SELECT.TextChanged += new System.EventHandler(this.DATE_SELECT_TextChanged);
            // 
            // labelInfo
            // 
            this.labelInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labelInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labelInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelInfo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelInfo.ForeColor = System.Drawing.Color.White;
            this.labelInfo.Location = new System.Drawing.Point(4, 31);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(110, 20);
            this.labelInfo.TabIndex = 678;
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DATE_TO
            // 
            this.DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_TO.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_TO.Checked = false;
            this.DATE_TO.DateTimeNowYear = "";
            this.DATE_TO.DBFieldsName = "";
            this.DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_TO.DisplayItemName = "";
            this.DATE_TO.DisplayPopUp = null;
            this.DATE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_TO.FocusOutCheckMethod")));
            this.DATE_TO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_TO.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_TO.IsInputErrorOccured = false;
            this.DATE_TO.ItemDefinedTypes = "datetime";
            this.DATE_TO.Location = new System.Drawing.Point(854, 31);
            this.DATE_TO.MaxLength = 10;
            this.DATE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_TO.Name = "DATE_TO";
            this.DATE_TO.NullValue = "";
            this.DATE_TO.PopupAfterExecute = null;
            this.DATE_TO.PopupBeforeExecute = null;
            this.DATE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_TO.PopupSearchSendParams")));
            this.DATE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_TO.popupWindowSetting")));
            this.DATE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_TO.RegistCheckMethod")));
            this.DATE_TO.ShortItemName = "";
            this.DATE_TO.Size = new System.Drawing.Size(100, 20);
            this.DATE_TO.TabIndex = 100;
            this.DATE_TO.Tag = "";
            this.DATE_TO.Value = null;
            this.DATE_TO.DoubleClick += new System.EventHandler(this.DATE_TO_DoubleClick);
            // 
            // DATE_FROM
            // 
            this.DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_FROM.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_FROM.Checked = false;
            this.DATE_FROM.DateTimeNowYear = "";
            this.DATE_FROM.DBFieldsName = "";
            this.DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_FROM.DisplayItemName = "";
            this.DATE_FROM.DisplayPopUp = null;
            this.DATE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_FROM.FocusOutCheckMethod")));
            this.DATE_FROM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_FROM.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_FROM.IsInputErrorOccured = false;
            this.DATE_FROM.ItemDefinedTypes = "datetime";
            this.DATE_FROM.Location = new System.Drawing.Point(733, 31);
            this.DATE_FROM.MaxLength = 10;
            this.DATE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_FROM.Name = "DATE_FROM";
            this.DATE_FROM.NullValue = "";
            this.DATE_FROM.PopupAfterExecute = null;
            this.DATE_FROM.PopupBeforeExecute = null;
            this.DATE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_FROM.PopupSearchSendParams")));
            this.DATE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_FROM.popupWindowSetting")));
            this.DATE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_FROM.RegistCheckMethod")));
            this.DATE_FROM.ShortItemName = "";
            this.DATE_FROM.Size = new System.Drawing.Size(100, 20);
            this.DATE_FROM.TabIndex = 90;
            this.DATE_FROM.Tag = "";
            this.DATE_FROM.Value = null;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(836, 31);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 20);
            this.label4.TabIndex = 681;
            this.label4.Text = "～";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CONDITION_ITEM1
            // 
            this.CONDITION_ITEM1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONDITION_ITEM1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_ITEM1.CharactersNumber = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.CONDITION_ITEM1.DBFieldsName = "";
            this.CONDITION_ITEM1.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_ITEM1.DisplayItemName = "検索条件1";
            this.CONDITION_ITEM1.DisplayPopUp = null;
            this.CONDITION_ITEM1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM1.FocusOutCheckMethod")));
            this.CONDITION_ITEM1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_ITEM1.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_ITEM1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CONDITION_ITEM1.IsInputErrorOccured = false;
            this.CONDITION_ITEM1.Location = new System.Drawing.Point(117, 97);
            this.CONDITION_ITEM1.MaxLength = 0;
            this.CONDITION_ITEM1.Name = "CONDITION_ITEM1";
            this.CONDITION_ITEM1.PopupAfterExecute = null;
            this.CONDITION_ITEM1.PopupBeforeExecute = null;
            this.CONDITION_ITEM1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_ITEM1.PopupSearchSendParams")));
            this.CONDITION_ITEM1.PopupSendParams = new string[] {
        "dgv_CONDITION"};
            this.CONDITION_ITEM1.PopupSetFormField = "CONDITION_ITEM1,CONDITION_VALUE1";
            this.CONDITION_ITEM1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM1.PopupWindowName = "マスタ検索項目ポップアップ";
            this.CONDITION_ITEM1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_ITEM1.popupWindowSetting")));
            this.CONDITION_ITEM1.ReadOnly = true;
            this.CONDITION_ITEM1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM1.RegistCheckMethod")));
            this.CONDITION_ITEM1.SetFormField = "CONDITION_ITEM1,CONDITION_VALUE1";
            this.CONDITION_ITEM1.ShortItemName = "検索条件1";
            this.CONDITION_ITEM1.Size = new System.Drawing.Size(212, 20);
            this.CONDITION_ITEM1.TabIndex = 140;
            this.CONDITION_ITEM1.Tag = "検索条件を指定してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // CONDITION_VALUE1
            // 
            this.CONDITION_VALUE1.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE1.CharactersNumber = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.CONDITION_VALUE1.DBFieldsName = "";
            this.CONDITION_VALUE1.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE1.DisplayItemName = "検索条件1";
            this.CONDITION_VALUE1.DisplayPopUp = null;
            this.CONDITION_VALUE1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE1.FocusOutCheckMethod")));
            this.CONDITION_VALUE1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_VALUE1.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_VALUE1.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE1.IsInputErrorOccured = false;
            this.CONDITION_VALUE1.ItemDefinedTypes = "";
            this.CONDITION_VALUE1.Location = new System.Drawing.Point(330, 97);
            this.CONDITION_VALUE1.MaxLength = 0;
            this.CONDITION_VALUE1.Name = "CONDITION_VALUE1";
            this.CONDITION_VALUE1.PopupAfterExecute = null;
            this.CONDITION_VALUE1.PopupBeforeExecute = null;
            this.CONDITION_VALUE1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE1.PopupSearchSendParams")));
            this.CONDITION_VALUE1.PopupSetFormField = "";
            this.CONDITION_VALUE1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE1.PopupWindowName = "";
            this.CONDITION_VALUE1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE1.popupWindowSetting")));
            this.CONDITION_VALUE1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE1.RegistCheckMethod")));
            this.CONDITION_VALUE1.SetFormField = "";
            this.CONDITION_VALUE1.ShortItemName = "検索条件1";
            this.CONDITION_VALUE1.Size = new System.Drawing.Size(167, 20);
            this.CONDITION_VALUE1.TabIndex = 150;
            this.CONDITION_VALUE1.Tag = "検索する文字を入力してください";
            this.CONDITION_VALUE1.Enter += new System.EventHandler(this.CONDITION_VALUE1_Enter);
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(4, 97);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(110, 20);
            this.label16.TabIndex = 684;
            this.label16.Text = "検索条件1";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CONDITION_ITEM3
            // 
            this.CONDITION_ITEM3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONDITION_ITEM3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_ITEM3.CharactersNumber = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.CONDITION_ITEM3.DBFieldsName = "";
            this.CONDITION_ITEM3.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_ITEM3.DisplayItemName = "検索条件3";
            this.CONDITION_ITEM3.DisplayPopUp = null;
            this.CONDITION_ITEM3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM3.FocusOutCheckMethod")));
            this.CONDITION_ITEM3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_ITEM3.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_ITEM3.IsInputErrorOccured = false;
            this.CONDITION_ITEM3.Location = new System.Drawing.Point(117, 119);
            this.CONDITION_ITEM3.MaxLength = 0;
            this.CONDITION_ITEM3.Name = "CONDITION_ITEM3";
            this.CONDITION_ITEM3.PopupAfterExecute = null;
            this.CONDITION_ITEM3.PopupBeforeExecute = null;
            this.CONDITION_ITEM3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_ITEM3.PopupSearchSendParams")));
            this.CONDITION_ITEM3.PopupSendParams = new string[] {
        "dgv_CONDITION"};
            this.CONDITION_ITEM3.PopupSetFormField = "CONDITION_ITEM3,CONDITION_VALUE3";
            this.CONDITION_ITEM3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM3.PopupWindowName = "マスタ検索項目ポップアップ";
            this.CONDITION_ITEM3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_ITEM3.popupWindowSetting")));
            this.CONDITION_ITEM3.ReadOnly = true;
            this.CONDITION_ITEM3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM3.RegistCheckMethod")));
            this.CONDITION_ITEM3.SetFormField = "CONDITION_ITEM3,CONDITION_VALUE3";
            this.CONDITION_ITEM3.ShortItemName = "検索条件3";
            this.CONDITION_ITEM3.Size = new System.Drawing.Size(212, 20);
            this.CONDITION_ITEM3.TabIndex = 180;
            this.CONDITION_ITEM3.Tag = "検索条件を指定してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // CONDITION_VALUE3
            // 
            this.CONDITION_VALUE3.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE3.CharactersNumber = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.CONDITION_VALUE3.DBFieldsName = "";
            this.CONDITION_VALUE3.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE3.DisplayItemName = "検索条件3";
            this.CONDITION_VALUE3.DisplayPopUp = null;
            this.CONDITION_VALUE3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE3.FocusOutCheckMethod")));
            this.CONDITION_VALUE3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_VALUE3.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_VALUE3.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE3.IsInputErrorOccured = false;
            this.CONDITION_VALUE3.ItemDefinedTypes = "";
            this.CONDITION_VALUE3.Location = new System.Drawing.Point(330, 119);
            this.CONDITION_VALUE3.MaxLength = 0;
            this.CONDITION_VALUE3.Name = "CONDITION_VALUE3";
            this.CONDITION_VALUE3.PopupAfterExecute = null;
            this.CONDITION_VALUE3.PopupBeforeExecute = null;
            this.CONDITION_VALUE3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE3.PopupSearchSendParams")));
            this.CONDITION_VALUE3.PopupSetFormField = "";
            this.CONDITION_VALUE3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE3.PopupWindowName = "";
            this.CONDITION_VALUE3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE3.popupWindowSetting")));
            this.CONDITION_VALUE3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE3.RegistCheckMethod")));
            this.CONDITION_VALUE3.SetFormField = "";
            this.CONDITION_VALUE3.ShortItemName = "検索条件3";
            this.CONDITION_VALUE3.Size = new System.Drawing.Size(167, 20);
            this.CONDITION_VALUE3.TabIndex = 190;
            this.CONDITION_VALUE3.Tag = "検索する文字を入力してください";
            this.CONDITION_VALUE3.Enter += new System.EventHandler(this.CONDITION_VALUE3_Enter);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 687;
            this.label1.Text = "検索条件3";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CONDITION_ITEM4
            // 
            this.CONDITION_ITEM4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONDITION_ITEM4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_ITEM4.CharactersNumber = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.CONDITION_ITEM4.DBFieldsName = "";
            this.CONDITION_ITEM4.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_ITEM4.DisplayItemName = "検索条件4";
            this.CONDITION_ITEM4.DisplayPopUp = null;
            this.CONDITION_ITEM4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM4.FocusOutCheckMethod")));
            this.CONDITION_ITEM4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_ITEM4.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_ITEM4.IsInputErrorOccured = false;
            this.CONDITION_ITEM4.Location = new System.Drawing.Point(621, 119);
            this.CONDITION_ITEM4.MaxLength = 0;
            this.CONDITION_ITEM4.Name = "CONDITION_ITEM4";
            this.CONDITION_ITEM4.PopupAfterExecute = null;
            this.CONDITION_ITEM4.PopupBeforeExecute = null;
            this.CONDITION_ITEM4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_ITEM4.PopupSearchSendParams")));
            this.CONDITION_ITEM4.PopupSendParams = new string[] {
        "dgv_CONDITION"};
            this.CONDITION_ITEM4.PopupSetFormField = "CONDITION_ITEM4,CONDITION_VALUE4";
            this.CONDITION_ITEM4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM4.PopupWindowName = "マスタ検索項目ポップアップ";
            this.CONDITION_ITEM4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_ITEM4.popupWindowSetting")));
            this.CONDITION_ITEM4.ReadOnly = true;
            this.CONDITION_ITEM4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM4.RegistCheckMethod")));
            this.CONDITION_ITEM4.SetFormField = "CONDITION_ITEM4,CONDITION_VALUE4";
            this.CONDITION_ITEM4.ShortItemName = "検索条件4";
            this.CONDITION_ITEM4.Size = new System.Drawing.Size(212, 20);
            this.CONDITION_ITEM4.TabIndex = 200;
            this.CONDITION_ITEM4.Tag = "検索条件を指定してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // CONDITION_VALUE4
            // 
            this.CONDITION_VALUE4.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE4.CharactersNumber = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.CONDITION_VALUE4.DBFieldsName = "";
            this.CONDITION_VALUE4.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE4.DisplayItemName = "検索条件4";
            this.CONDITION_VALUE4.DisplayPopUp = null;
            this.CONDITION_VALUE4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE4.FocusOutCheckMethod")));
            this.CONDITION_VALUE4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_VALUE4.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_VALUE4.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE4.IsInputErrorOccured = false;
            this.CONDITION_VALUE4.ItemDefinedTypes = "";
            this.CONDITION_VALUE4.Location = new System.Drawing.Point(834, 119);
            this.CONDITION_VALUE4.MaxLength = 0;
            this.CONDITION_VALUE4.Name = "CONDITION_VALUE4";
            this.CONDITION_VALUE4.PopupAfterExecute = null;
            this.CONDITION_VALUE4.PopupBeforeExecute = null;
            this.CONDITION_VALUE4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE4.PopupSearchSendParams")));
            this.CONDITION_VALUE4.PopupSetFormField = "";
            this.CONDITION_VALUE4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE4.PopupWindowName = "";
            this.CONDITION_VALUE4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE4.popupWindowSetting")));
            this.CONDITION_VALUE4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE4.RegistCheckMethod")));
            this.CONDITION_VALUE4.SetFormField = "";
            this.CONDITION_VALUE4.ShortItemName = "検索条件4";
            this.CONDITION_VALUE4.Size = new System.Drawing.Size(167, 20);
            this.CONDITION_VALUE4.TabIndex = 210;
            this.CONDITION_VALUE4.Tag = "検索する文字を入力してください";
            this.CONDITION_VALUE4.Enter += new System.EventHandler(this.CONDITION_VALUE4_Enter);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(508, 119);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 690;
            this.label3.Text = "検索条件4";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CONDITION_ITEM2
            // 
            this.CONDITION_ITEM2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONDITION_ITEM2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_ITEM2.CharactersNumber = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.CONDITION_ITEM2.DBFieldsName = "";
            this.CONDITION_ITEM2.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_ITEM2.DisplayItemName = "検索条件2";
            this.CONDITION_ITEM2.DisplayPopUp = null;
            this.CONDITION_ITEM2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM2.FocusOutCheckMethod")));
            this.CONDITION_ITEM2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_ITEM2.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_ITEM2.IsInputErrorOccured = false;
            this.CONDITION_ITEM2.Location = new System.Drawing.Point(621, 97);
            this.CONDITION_ITEM2.MaxLength = 0;
            this.CONDITION_ITEM2.Name = "CONDITION_ITEM2";
            this.CONDITION_ITEM2.PopupAfterExecute = null;
            this.CONDITION_ITEM2.PopupBeforeExecute = null;
            this.CONDITION_ITEM2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_ITEM2.PopupSearchSendParams")));
            this.CONDITION_ITEM2.PopupSendParams = new string[] {
        "dgv_CONDITION"};
            this.CONDITION_ITEM2.PopupSetFormField = "CONDITION_ITEM2,CONDITION_VALUE2";
            this.CONDITION_ITEM2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM2.PopupWindowName = "マスタ検索項目ポップアップ";
            this.CONDITION_ITEM2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_ITEM2.popupWindowSetting")));
            this.CONDITION_ITEM2.ReadOnly = true;
            this.CONDITION_ITEM2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM2.RegistCheckMethod")));
            this.CONDITION_ITEM2.SetFormField = "CONDITION_ITEM2,CONDITION_VALUE2";
            this.CONDITION_ITEM2.ShortItemName = "検索条件2";
            this.CONDITION_ITEM2.Size = new System.Drawing.Size(212, 20);
            this.CONDITION_ITEM2.TabIndex = 160;
            this.CONDITION_ITEM2.Tag = "検索条件を指定してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // CONDITION_VALUE2
            // 
            this.CONDITION_VALUE2.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE2.CharactersNumber = new decimal(new int[] {
            32,
            0,
            0,
            0});
            this.CONDITION_VALUE2.DBFieldsName = "";
            this.CONDITION_VALUE2.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE2.DisplayItemName = "検索条件2";
            this.CONDITION_VALUE2.DisplayPopUp = null;
            this.CONDITION_VALUE2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE2.FocusOutCheckMethod")));
            this.CONDITION_VALUE2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_VALUE2.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_VALUE2.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE2.IsInputErrorOccured = false;
            this.CONDITION_VALUE2.ItemDefinedTypes = "";
            this.CONDITION_VALUE2.Location = new System.Drawing.Point(834, 97);
            this.CONDITION_VALUE2.MaxLength = 0;
            this.CONDITION_VALUE2.Name = "CONDITION_VALUE2";
            this.CONDITION_VALUE2.PopupAfterExecute = null;
            this.CONDITION_VALUE2.PopupBeforeExecute = null;
            this.CONDITION_VALUE2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE2.PopupSearchSendParams")));
            this.CONDITION_VALUE2.PopupSetFormField = "";
            this.CONDITION_VALUE2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE2.PopupWindowName = "";
            this.CONDITION_VALUE2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE2.popupWindowSetting")));
            this.CONDITION_VALUE2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE2.RegistCheckMethod")));
            this.CONDITION_VALUE2.SetFormField = "";
            this.CONDITION_VALUE2.ShortItemName = "検索条件2";
            this.CONDITION_VALUE2.Size = new System.Drawing.Size(167, 20);
            this.CONDITION_VALUE2.TabIndex = 170;
            this.CONDITION_VALUE2.Tag = "検索する文字を入力してください";
            this.CONDITION_VALUE2.Enter += new System.EventHandler(this.CONDITION_VALUE2_Enter);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(508, 97);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 693;
            this.label5.Text = "検索条件2";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SOUFU_TITLE
            // 
            this.SOUFU_TITLE.BackColor = System.Drawing.SystemColors.Window;
            this.SOUFU_TITLE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SOUFU_TITLE.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.SOUFU_TITLE.DBFieldsName = "SOUFU_TITLE";
            this.SOUFU_TITLE.DefaultBackColor = System.Drawing.Color.Empty;
            this.SOUFU_TITLE.DisplayItemName = "送付タイトル";
            this.SOUFU_TITLE.DisplayPopUp = null;
            this.SOUFU_TITLE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SOUFU_TITLE.FocusOutCheckMethod")));
            this.SOUFU_TITLE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SOUFU_TITLE.ForeColor = System.Drawing.Color.Black;
            this.SOUFU_TITLE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.SOUFU_TITLE.IsInputErrorOccured = false;
            this.SOUFU_TITLE.Location = new System.Drawing.Point(117, 53);
            this.SOUFU_TITLE.MaxLength = 80;
            this.SOUFU_TITLE.Name = "SOUFU_TITLE";
            this.SOUFU_TITLE.PopupAfterExecute = null;
            this.SOUFU_TITLE.PopupBeforeExecute = null;
            this.SOUFU_TITLE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SOUFU_TITLE.PopupSearchSendParams")));
            this.SOUFU_TITLE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SOUFU_TITLE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SOUFU_TITLE.popupWindowSetting")));
            this.SOUFU_TITLE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SOUFU_TITLE.RegistCheckMethod")));
            this.SOUFU_TITLE.ShortItemName = "送付タイトル";
            this.SOUFU_TITLE.Size = new System.Drawing.Size(290, 20);
            this.SOUFU_TITLE.TabIndex = 110;
            this.SOUFU_TITLE.Tag = "全角４０桁以内で入力してください";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(4, 53);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(110, 20);
            this.label17.TabIndex = 694;
            this.label17.Text = "送付タイトル";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FILE_NAME
            // 
            this.FILE_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.FILE_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FILE_NAME.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.FILE_NAME.DBFieldsName = "FILE_NAME";
            this.FILE_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.FILE_NAME.DisplayItemName = "ファイル名";
            this.FILE_NAME.DisplayPopUp = null;
            this.FILE_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILE_NAME.FocusOutCheckMethod")));
            this.FILE_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FILE_NAME.ForeColor = System.Drawing.Color.Black;
            this.FILE_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.FILE_NAME.IsInputErrorOccured = false;
            this.FILE_NAME.Location = new System.Drawing.Point(117, 75);
            this.FILE_NAME.MaxLength = 80;
            this.FILE_NAME.Name = "FILE_NAME";
            this.FILE_NAME.PopupAfterExecute = null;
            this.FILE_NAME.PopupBeforeExecute = null;
            this.FILE_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FILE_NAME.PopupSearchSendParams")));
            this.FILE_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FILE_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("FILE_NAME.popupWindowSetting")));
            this.FILE_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("FILE_NAME.RegistCheckMethod")));
            this.FILE_NAME.ShortItemName = "ファイル名";
            this.FILE_NAME.Size = new System.Drawing.Size(290, 20);
            this.FILE_NAME.TabIndex = 130;
            this.FILE_NAME.Tag = "全角４０桁以内で入力してください";
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(4, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 696;
            this.label7.Text = "ファイル名";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KEIYAKUSHA
            // 
            this.KEIYAKUSHA.BackColor = System.Drawing.SystemColors.Window;
            this.KEIYAKUSHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKUSHA.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.KEIYAKUSHA.DBFieldsName = "KEIYAKUSHA";
            this.KEIYAKUSHA.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKUSHA.DisplayItemName = "契約者";
            this.KEIYAKUSHA.DisplayPopUp = null;
            this.KEIYAKUSHA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHA.FocusOutCheckMethod")));
            this.KEIYAKUSHA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KEIYAKUSHA.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKUSHA.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.KEIYAKUSHA.IsInputErrorOccured = false;
            this.KEIYAKUSHA.Location = new System.Drawing.Point(621, 53);
            this.KEIYAKUSHA.MaxLength = 80;
            this.KEIYAKUSHA.Name = "KEIYAKUSHA";
            this.KEIYAKUSHA.PopupAfterExecute = null;
            this.KEIYAKUSHA.PopupBeforeExecute = null;
            this.KEIYAKUSHA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKUSHA.PopupSearchSendParams")));
            this.KEIYAKUSHA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKUSHA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKUSHA.popupWindowSetting")));
            this.KEIYAKUSHA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHA.RegistCheckMethod")));
            this.KEIYAKUSHA.ShortItemName = "契約者";
            this.KEIYAKUSHA.Size = new System.Drawing.Size(290, 20);
            this.KEIYAKUSHA.TabIndex = 120;
            this.KEIYAKUSHA.Tag = "全角４０桁以内で入力してください";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(508, 53);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 698;
            this.label8.Text = "契約者";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgv_CONDITION
            // 
            this.dgv_CONDITION.AllowUserToAddRows = false;
            this.dgv_CONDITION.AllowUserToResizeRows = false;
            this.dgv_CONDITION.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle27.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle27.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle27.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle27.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle27.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_CONDITION.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle27;
            this.dgv_CONDITION.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle28.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle28.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle28.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle28.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle28.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_CONDITION.DefaultCellStyle = dataGridViewCellStyle28;
            this.dgv_CONDITION.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_CONDITION.EnableHeadersVisualStyles = false;
            this.dgv_CONDITION.GridColor = System.Drawing.Color.White;
            this.dgv_CONDITION.IsReload = false;
            this.dgv_CONDITION.LinkedDataPanelName = null;
            this.dgv_CONDITION.Location = new System.Drawing.Point(568, 318);
            this.dgv_CONDITION.MultiSelect = false;
            this.dgv_CONDITION.Name = "dgv_CONDITION";
            this.dgv_CONDITION.ReadOnly = true;
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle29.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle29.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_CONDITION.RowHeadersDefaultCellStyle = dataGridViewCellStyle29;
            this.dgv_CONDITION.RowHeadersVisible = false;
            this.dgv_CONDITION.RowTemplate.Height = 21;
            this.dgv_CONDITION.ShowCellToolTips = false;
            this.dgv_CONDITION.Size = new System.Drawing.Size(429, 85);
            this.dgv_CONDITION.TabIndex = 700;
            this.dgv_CONDITION.Visible = false;
            // 
            // dgvCustomTextBoxColumn1
            // 
            this.dgvCustomTextBoxColumn1.DataPropertyName = "DENSHI_KEIYAKU_SHORUI_INFO";
            this.dgvCustomTextBoxColumn1.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle30;
            this.dgvCustomTextBoxColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn1.HeaderText = "書類情報1";
            this.dgvCustomTextBoxColumn1.ItemDefinedTypes = "varchar";
            this.dgvCustomTextBoxColumn1.Name = "dgvCustomTextBoxColumn1";
            this.dgvCustomTextBoxColumn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn1.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn1.popupWindowSetting")));
            this.dgvCustomTextBoxColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn1.Width = 150;
            // 
            // dgvCustomTextBoxColumn2
            // 
            this.dgvCustomTextBoxColumn2.DataPropertyName = "DENSHI_KEIYAKU_SHORUI_INFO";
            this.dgvCustomTextBoxColumn2.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle31;
            this.dgvCustomTextBoxColumn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn2.HeaderText = "書類情報2";
            this.dgvCustomTextBoxColumn2.Name = "dgvCustomTextBoxColumn2";
            this.dgvCustomTextBoxColumn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn2.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn2.popupWindowSetting")));
            this.dgvCustomTextBoxColumn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn3
            // 
            this.dgvCustomTextBoxColumn3.DataPropertyName = "DENSHI_KEIYAKU_SHORUI_INFO";
            this.dgvCustomTextBoxColumn3.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle32;
            this.dgvCustomTextBoxColumn3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn3.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn3.HeaderText = "書類情報3";
            this.dgvCustomTextBoxColumn3.Name = "dgvCustomTextBoxColumn3";
            this.dgvCustomTextBoxColumn3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn3.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn3.popupWindowSetting")));
            this.dgvCustomTextBoxColumn3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn3.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn4
            // 
            this.dgvCustomTextBoxColumn4.DataPropertyName = "DENSHI_KEIYAKU_SHORUI_INFO";
            this.dgvCustomTextBoxColumn4.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle33;
            this.dgvCustomTextBoxColumn4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn4.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn4.HeaderText = "書類情報4";
            this.dgvCustomTextBoxColumn4.Name = "dgvCustomTextBoxColumn4";
            this.dgvCustomTextBoxColumn4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn4.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn4.popupWindowSetting")));
            this.dgvCustomTextBoxColumn4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn4.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn5
            // 
            this.dgvCustomTextBoxColumn5.DataPropertyName = "DENSHI_KEIYAKU_SHORUI_INFO";
            this.dgvCustomTextBoxColumn5.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle34;
            this.dgvCustomTextBoxColumn5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn5.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn5.HeaderText = "書類情報5";
            this.dgvCustomTextBoxColumn5.Name = "dgvCustomTextBoxColumn5";
            this.dgvCustomTextBoxColumn5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn5.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn5.popupWindowSetting")));
            this.dgvCustomTextBoxColumn5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn5.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn6
            // 
            this.dgvCustomTextBoxColumn6.DataPropertyName = "DENSHI_KEIYAKU_SHORUI_INFO";
            this.dgvCustomTextBoxColumn6.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle35;
            this.dgvCustomTextBoxColumn6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn6.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn6.HeaderText = "書類情報6";
            this.dgvCustomTextBoxColumn6.Name = "dgvCustomTextBoxColumn6";
            this.dgvCustomTextBoxColumn6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn6.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn6.popupWindowSetting")));
            this.dgvCustomTextBoxColumn6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn6.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn7
            // 
            this.dgvCustomTextBoxColumn7.DataPropertyName = "DENSHI_KEIYAKU_SHORUI_INFO";
            this.dgvCustomTextBoxColumn7.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle36;
            this.dgvCustomTextBoxColumn7.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn7.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn7.HeaderText = "書類情報7";
            this.dgvCustomTextBoxColumn7.Name = "dgvCustomTextBoxColumn7";
            this.dgvCustomTextBoxColumn7.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn7.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn7.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn7.popupWindowSetting")));
            this.dgvCustomTextBoxColumn7.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn7.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn8
            // 
            this.dgvCustomTextBoxColumn8.DataPropertyName = "DENSHI_KEIYAKU_SHORUI_INFO";
            this.dgvCustomTextBoxColumn8.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle37.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle37;
            this.dgvCustomTextBoxColumn8.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn8.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn8.HeaderText = "書類情報8";
            this.dgvCustomTextBoxColumn8.Name = "dgvCustomTextBoxColumn8";
            this.dgvCustomTextBoxColumn8.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn8.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn8.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn8.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn8.popupWindowSetting")));
            this.dgvCustomTextBoxColumn8.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn8.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn9
            // 
            this.dgvCustomTextBoxColumn9.DataPropertyName = "DENSHI_KEIYAKU_SHORUI_INFO";
            this.dgvCustomTextBoxColumn9.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle38.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle38;
            this.dgvCustomTextBoxColumn9.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn9.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn9.HeaderText = "書類情報9";
            this.dgvCustomTextBoxColumn9.Name = "dgvCustomTextBoxColumn9";
            this.dgvCustomTextBoxColumn9.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn9.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn9.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn9.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn9.popupWindowSetting")));
            this.dgvCustomTextBoxColumn9.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn9.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn10
            // 
            this.dgvCustomTextBoxColumn10.DataPropertyName = "DENSHI_KEIYAKU_SHORUI_INFO";
            this.dgvCustomTextBoxColumn10.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle39.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle39;
            this.dgvCustomTextBoxColumn10.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn10.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn10.HeaderText = "書類情報10";
            this.dgvCustomTextBoxColumn10.Name = "dgvCustomTextBoxColumn10";
            this.dgvCustomTextBoxColumn10.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn10.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn10.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn10.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn10.popupWindowSetting")));
            this.dgvCustomTextBoxColumn10.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn10.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = false;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1022, 490);
            this.Controls.Add(this.dgv_CONDITION);
            this.Controls.Add(this.KEIYAKUSHA);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.FILE_NAME);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.SOUFU_TITLE);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.CONDITION_ITEM2);
            this.Controls.Add(this.CONDITION_VALUE2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.CONDITION_ITEM4);
            this.Controls.Add(this.CONDITION_VALUE4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CONDITION_ITEM3);
            this.Controls.Add(this.CONDITION_VALUE3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CONDITION_ITEM1);
            this.Controls.Add(this.CONDITION_VALUE1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.DATE_TO);
            this.Controls.Add(this.DATE_FROM);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DATE_SELECT_6);
            this.Controls.Add(this.DATE_SELECT_5);
            this.Controls.Add(this.DATE_SELECT_4);
            this.Controls.Add(this.DATE_SELECT_3);
            this.Controls.Add(this.DATE_SELECT_2);
            this.Controls.Add(this.DATE_SELECT_1);
            this.Controls.Add(this.DATE_SELECT);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.KEIYAKU_JYOUKYOU_NAME);
            this.Controls.Add(this.KEIYAKU_JYOUKYOU_CD);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.PreviousValue = "";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.KEIYAKU_JYOUKYOU_CD, 0);
            this.Controls.SetChildIndex(this.KEIYAKU_JYOUKYOU_NAME, 0);
            this.Controls.SetChildIndex(this.labelInfo, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_1, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_2, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_3, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_4, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_5, 0);
            this.Controls.SetChildIndex(this.DATE_SELECT_6, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.DATE_FROM, 0);
            this.Controls.SetChildIndex(this.DATE_TO, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.label16, 0);
            this.Controls.SetChildIndex(this.CONDITION_VALUE1, 0);
            this.Controls.SetChildIndex(this.CONDITION_ITEM1, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.CONDITION_VALUE3, 0);
            this.Controls.SetChildIndex(this.CONDITION_ITEM3, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.CONDITION_VALUE4, 0);
            this.Controls.SetChildIndex(this.CONDITION_ITEM4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.CONDITION_VALUE2, 0);
            this.Controls.SetChildIndex(this.CONDITION_ITEM2, 0);
            this.Controls.SetChildIndex(this.label17, 0);
            this.Controls.SetChildIndex(this.SOUFU_TITLE, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.FILE_NAME, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.KEIYAKUSHA, 0);
            this.Controls.SetChildIndex(this.dgv_CONDITION, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_CONDITION)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox KEIYAKU_JYOUKYOU_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 KEIYAKU_JYOUKYOU_CD;
        private System.Windows.Forms.Label label6;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_6;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_5;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_4;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_3;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_2;
        internal r_framework.CustomControl.CustomRadioButton DATE_SELECT_1;
        internal r_framework.CustomControl.CustomNumericTextBox2 DATE_SELECT;
        internal System.Windows.Forms.Label labelInfo;
        internal r_framework.CustomControl.CustomDateTimePicker DATE_TO;
        internal r_framework.CustomControl.CustomDateTimePicker DATE_FROM;
        internal System.Windows.Forms.Label label4;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM1;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE1;
        private System.Windows.Forms.Label label16;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM3;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE3;
        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM4;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE4;
        private System.Windows.Forms.Label label3;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM2;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE2;
        private System.Windows.Forms.Label label5;
        internal r_framework.CustomControl.CustomTextBox SOUFU_TITLE;
        private System.Windows.Forms.Label label17;
        internal r_framework.CustomControl.CustomTextBox FILE_NAME;
        private System.Windows.Forms.Label label7;
        internal r_framework.CustomControl.CustomTextBox KEIYAKUSHA;
        private System.Windows.Forms.Label label8;
        internal r_framework.CustomControl.CustomDataGridView dgv_CONDITION;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn1;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn2;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn3;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn4;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn5;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn6;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn7;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn8;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn9;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn10;
    }
}