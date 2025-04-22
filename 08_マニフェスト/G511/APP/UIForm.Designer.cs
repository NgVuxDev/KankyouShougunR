namespace Shougun.Core.PaperManifest.KoufuJoukyouHoukokushoIchiran
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.IchiranGRD = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.txtTitle2 = new r_framework.CustomControl.CustomTextBox();
            this.txtTitle1 = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGYOUSHUCD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtTeishutusaki = new r_framework.CustomControl.CustomTextBox();
            this.txtGenbaAddress = new r_framework.CustomControl.CustomTextBox();
            this.txtGenbaNm = new r_framework.CustomControl.CustomTextBox();
            this.txtName2 = new r_framework.CustomControl.CustomTextBox();
            this.txtADDRESS2 = new r_framework.CustomControl.CustomTextBox();
            this.hiddenValue = new r_framework.CustomControl.CustomTextBox();
            this.txtGenbaTel = new r_framework.CustomControl.CustomTextBox();
            this.txtTel = new r_framework.CustomControl.CustomTextBox();
            this.txtDAIHYOUSHA = new r_framework.CustomControl.CustomTextBox();
            this.txtName1 = new r_framework.CustomControl.CustomTextBox();
            this.txtADDRESS1 = new r_framework.CustomControl.CustomTextBox();
            this.txtGYOUSHUNM = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpCreadDate = new r_framework.CustomControl.CustomDateTimePicker();
            this.label25 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAllHaishuRyo = new r_framework.CustomControl.CustomTextBox();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.ROW_NO = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.HAIKI_SHURUI_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.HAISHU_RYOU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.COUFUMAISUU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.UPN_FUTSUU_KYOKA_NO = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPN_GYOUSHA_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPN_SAKI_GENBA_ADDRESS = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SBN_FUTSUU_KYOKA_NO = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SBN_GYOUSHA_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SBN_GENBA_ADDRESS = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.IchiranGRD)).BeginInit();
            this.customPanel1.SuspendLayout();
            this.customPanel2.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // IchiranGRD
            // 
            this.IchiranGRD.AllowUserToAddRows = false;
            this.IchiranGRD.AllowUserToDeleteRows = false;
            this.IchiranGRD.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.IchiranGRD.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IchiranGRD.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.IchiranGRD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IchiranGRD.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ROW_NO,
            this.HAIKI_SHURUI_NAME_RYAKU,
            this.HAISHU_RYOU,
            this.COUFUMAISUU,
            this.UPN_FUTSUU_KYOKA_NO,
            this.UPN_GYOUSHA_NAME,
            this.UPN_SAKI_GENBA_ADDRESS,
            this.SBN_FUTSUU_KYOKA_NO,
            this.SBN_GYOUSHA_NAME,
            this.SBN_GENBA_ADDRESS});
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.IchiranGRD.DefaultCellStyle = dataGridViewCellStyle12;
            this.IchiranGRD.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.IchiranGRD.EnableHeadersVisualStyles = false;
            this.IchiranGRD.GridColor = System.Drawing.Color.White;
            this.IchiranGRD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.IchiranGRD.IsBrowsePurpose = true;
            this.IchiranGRD.IsReload = false;
            this.IchiranGRD.LinkedDataPanelName = null;
            this.IchiranGRD.Location = new System.Drawing.Point(3, 3);
            this.IchiranGRD.MultiSelect = false;
            this.IchiranGRD.Name = "IchiranGRD";
            this.IchiranGRD.ReadOnly = true;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle13.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IchiranGRD.RowHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.IchiranGRD.RowHeadersVisible = false;
            this.IchiranGRD.RowTemplate.Height = 21;
            this.IchiranGRD.ShowCellToolTips = false;
            this.IchiranGRD.Size = new System.Drawing.Size(990, 252);
            this.IchiranGRD.TabIndex = 110;
            this.IchiranGRD.TabStop = false;
            this.IchiranGRD.Tag = "0";
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.customPanel2);
            this.customPanel1.Controls.Add(this.txtGYOUSHUCD);
            this.customPanel1.Controls.Add(this.label39);
            this.customPanel1.Controls.Add(this.label20);
            this.customPanel1.Controls.Add(this.label19);
            this.customPanel1.Controls.Add(this.txtTeishutusaki);
            this.customPanel1.Controls.Add(this.txtGenbaAddress);
            this.customPanel1.Controls.Add(this.txtGenbaNm);
            this.customPanel1.Controls.Add(this.txtName2);
            this.customPanel1.Controls.Add(this.txtADDRESS2);
            this.customPanel1.Controls.Add(this.hiddenValue);
            this.customPanel1.Controls.Add(this.txtGenbaTel);
            this.customPanel1.Controls.Add(this.txtTel);
            this.customPanel1.Controls.Add(this.txtDAIHYOUSHA);
            this.customPanel1.Controls.Add(this.txtName1);
            this.customPanel1.Controls.Add(this.txtADDRESS1);
            this.customPanel1.Controls.Add(this.txtGYOUSHUNM);
            this.customPanel1.Controls.Add(this.label3);
            this.customPanel1.Controls.Add(this.dtpCreadDate);
            this.customPanel1.Controls.Add(this.label25);
            this.customPanel1.Controls.Add(this.label38);
            this.customPanel1.Controls.Add(this.label37);
            this.customPanel1.Controls.Add(this.label36);
            this.customPanel1.Controls.Add(this.label21);
            this.customPanel1.Controls.Add(this.label23);
            this.customPanel1.Controls.Add(this.label28);
            this.customPanel1.Controls.Add(this.label29);
            this.customPanel1.Controls.Add(this.label31);
            this.customPanel1.Location = new System.Drawing.Point(0, 2);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(993, 200);
            this.customPanel1.TabIndex = 729;
            // 
            // customPanel2
            // 
            this.customPanel2.Controls.Add(this.txtTitle2);
            this.customPanel2.Controls.Add(this.txtTitle1);
            this.customPanel2.Controls.Add(this.label2);
            this.customPanel2.Location = new System.Drawing.Point(3, 28);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(472, 34);
            this.customPanel2.TabIndex = 730;
            this.customPanel2.Visible = false;
            // 
            // txtTitle2
            // 
            this.txtTitle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtTitle2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtTitle2.DBFieldsName = "";
            this.txtTitle2.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtTitle2.DisplayPopUp = null;
            this.txtTitle2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTitle2.FocusOutCheckMethod")));
            this.txtTitle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtTitle2.ForeColor = System.Drawing.Color.Black;
            this.txtTitle2.IsInputErrorOccured = false;
            this.txtTitle2.Location = new System.Drawing.Point(271, 3);
            this.txtTitle2.MaxLength = 20;
            this.txtTitle2.Name = "txtTitle2";
            this.txtTitle2.PopupAfterExecute = null;
            this.txtTitle2.PopupBeforeExecute = null;
            this.txtTitle2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtTitle2.PopupSearchSendParams")));
            this.txtTitle2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtTitle2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtTitle2.popupWindowSetting")));
            this.txtTitle2.ReadOnly = true;
            this.txtTitle2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTitle2.RegistCheckMethod")));
            this.txtTitle2.Size = new System.Drawing.Size(198, 20);
            this.txtTitle2.TabIndex = 608;
            this.txtTitle2.TabStop = false;
            this.txtTitle2.Tag = " ";
            // 
            // txtTitle1
            // 
            this.txtTitle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtTitle1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTitle1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtTitle1.DBFieldsName = "";
            this.txtTitle1.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtTitle1.DisplayPopUp = null;
            this.txtTitle1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTitle1.FocusOutCheckMethod")));
            this.txtTitle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtTitle1.ForeColor = System.Drawing.Color.Black;
            this.txtTitle1.IsInputErrorOccured = false;
            this.txtTitle1.Location = new System.Drawing.Point(59, 3);
            this.txtTitle1.MaxLength = 20;
            this.txtTitle1.Name = "txtTitle1";
            this.txtTitle1.PopupAfterExecute = null;
            this.txtTitle1.PopupBeforeExecute = null;
            this.txtTitle1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtTitle1.PopupSearchSendParams")));
            this.txtTitle1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtTitle1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtTitle1.popupWindowSetting")));
            this.txtTitle1.ReadOnly = true;
            this.txtTitle1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTitle1.RegistCheckMethod")));
            this.txtTitle1.Size = new System.Drawing.Size(206, 20);
            this.txtTitle1.TabIndex = 607;
            this.txtTitle1.TabStop = false;
            this.txtTitle1.Tag = " ";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 20);
            this.label2.TabIndex = 609;
            this.label2.Text = "表題";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtGYOUSHUCD
            // 
            this.txtGYOUSHUCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtGYOUSHUCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGYOUSHUCD.CharacterLimitList = null;
            this.txtGYOUSHUCD.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.txtGYOUSHUCD.DBFieldsName = "GYOUSHA_CD";
            this.txtGYOUSHUCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtGYOUSHUCD.DisplayItemName = "業者CD(From)";
            this.txtGYOUSHUCD.DisplayPopUp = null;
            this.txtGYOUSHUCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGYOUSHUCD.FocusOutCheckMethod")));
            this.txtGYOUSHUCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtGYOUSHUCD.ForeColor = System.Drawing.Color.Black;
            this.txtGYOUSHUCD.GetCodeMasterField = "";
            this.txtGYOUSHUCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txtGYOUSHUCD.IsInputErrorOccured = false;
            this.txtGYOUSHUCD.ItemDefinedTypes = "varchar";
            this.txtGYOUSHUCD.Location = new System.Drawing.Point(117, 153);
            this.txtGYOUSHUCD.MaxLength = 4;
            this.txtGYOUSHUCD.Name = "txtGYOUSHUCD";
            this.txtGYOUSHUCD.PopupAfterExecute = null;
            this.txtGYOUSHUCD.PopupAfterExecuteMethod = "";
            this.txtGYOUSHUCD.PopupBeforeExecute = null;
            this.txtGYOUSHUCD.PopupGetMasterField = "";
            this.txtGYOUSHUCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtGYOUSHUCD.PopupSearchSendParams")));
            this.txtGYOUSHUCD.PopupSetFormField = "";
            this.txtGYOUSHUCD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtGYOUSHUCD.PopupWindowName = "検索共通ポップアップ";
            this.txtGYOUSHUCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtGYOUSHUCD.popupWindowSetting")));
            this.txtGYOUSHUCD.ReadOnly = true;
            this.txtGYOUSHUCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGYOUSHUCD.RegistCheckMethod")));
            this.txtGYOUSHUCD.SetFormField = "";
            this.txtGYOUSHUCD.Size = new System.Drawing.Size(50, 20);
            this.txtGYOUSHUCD.TabIndex = 694;
            this.txtGYOUSHUCD.TabStop = false;
            this.txtGYOUSHUCD.Tag = " ";
            this.txtGYOUSHUCD.ZeroPaddengFlag = true;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.label39.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label39.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label39.ForeColor = System.Drawing.Color.Black;
            this.label39.Location = new System.Drawing.Point(582, 154);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(278, 20);
            this.label39.TabIndex = 693;
            this.label39.Text = "（法人にあっては名称及び代表者の氏名）";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.label20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label20.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(467, 22);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 20);
            this.label20.TabIndex = 692;
            this.label20.Text = "報告者";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(422, 2);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(20, 20);
            this.label19.TabIndex = 691;
            this.label19.Text = "殿";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTeishutusaki
            // 
            this.txtTeishutusaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtTeishutusaki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTeishutusaki.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtTeishutusaki.DBFieldsName = "";
            this.txtTeishutusaki.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtTeishutusaki.DisplayPopUp = null;
            this.txtTeishutusaki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTeishutusaki.FocusOutCheckMethod")));
            this.txtTeishutusaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtTeishutusaki.ForeColor = System.Drawing.Color.Black;
            this.txtTeishutusaki.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtTeishutusaki.IsInputErrorOccured = false;
            this.txtTeishutusaki.Location = new System.Drawing.Point(118, 2);
            this.txtTeishutusaki.MaxLength = 20;
            this.txtTeishutusaki.Name = "txtTeishutusaki";
            this.txtTeishutusaki.PopupAfterExecute = null;
            this.txtTeishutusaki.PopupBeforeExecute = null;
            this.txtTeishutusaki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtTeishutusaki.PopupSearchSendParams")));
            this.txtTeishutusaki.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtTeishutusaki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtTeishutusaki.popupWindowSetting")));
            this.txtTeishutusaki.ReadOnly = true;
            this.txtTeishutusaki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTeishutusaki.RegistCheckMethod")));
            this.txtTeishutusaki.Size = new System.Drawing.Size(300, 20);
            this.txtTeishutusaki.TabIndex = 690;
            this.txtTeishutusaki.TabStop = false;
            this.txtTeishutusaki.Tag = " ";
            this.txtTeishutusaki.Text = "１２３４５６７８９０１２３４５６７８９０";
            // 
            // txtGenbaAddress
            // 
            this.txtGenbaAddress.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtGenbaAddress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGenbaAddress.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.txtGenbaAddress.DBFieldsName = "";
            this.txtGenbaAddress.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtGenbaAddress.DisplayPopUp = null;
            this.txtGenbaAddress.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGenbaAddress.FocusOutCheckMethod")));
            this.txtGenbaAddress.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtGenbaAddress.ForeColor = System.Drawing.Color.Black;
            this.txtGenbaAddress.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtGenbaAddress.IsInputErrorOccured = false;
            this.txtGenbaAddress.Location = new System.Drawing.Point(117, 111);
            this.txtGenbaAddress.MaxLength = 40;
            this.txtGenbaAddress.Multiline = true;
            this.txtGenbaAddress.Name = "txtGenbaAddress";
            this.txtGenbaAddress.PopupAfterExecute = null;
            this.txtGenbaAddress.PopupBeforeExecute = null;
            this.txtGenbaAddress.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtGenbaAddress.PopupSearchSendParams")));
            this.txtGenbaAddress.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtGenbaAddress.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtGenbaAddress.popupWindowSetting")));
            this.txtGenbaAddress.ReadOnly = true;
            this.txtGenbaAddress.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGenbaAddress.RegistCheckMethod")));
            this.txtGenbaAddress.Size = new System.Drawing.Size(300, 40);
            this.txtGenbaAddress.TabIndex = 180;
            this.txtGenbaAddress.TabStop = false;
            this.txtGenbaAddress.Tag = " ";
            // 
            // txtGenbaNm
            // 
            this.txtGenbaNm.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtGenbaNm.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGenbaNm.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.txtGenbaNm.DBFieldsName = "";
            this.txtGenbaNm.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtGenbaNm.DisplayPopUp = null;
            this.txtGenbaNm.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGenbaNm.FocusOutCheckMethod")));
            this.txtGenbaNm.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtGenbaNm.ForeColor = System.Drawing.Color.Black;
            this.txtGenbaNm.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txtGenbaNm.IsInputErrorOccured = false;
            this.txtGenbaNm.Location = new System.Drawing.Point(117, 68);
            this.txtGenbaNm.MaxLength = 40;
            this.txtGenbaNm.Multiline = true;
            this.txtGenbaNm.Name = "txtGenbaNm";
            this.txtGenbaNm.PopupAfterExecute = null;
            this.txtGenbaNm.PopupBeforeExecute = null;
            this.txtGenbaNm.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtGenbaNm.PopupSearchSendParams")));
            this.txtGenbaNm.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtGenbaNm.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtGenbaNm.popupWindowSetting")));
            this.txtGenbaNm.ReadOnly = true;
            this.txtGenbaNm.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGenbaNm.RegistCheckMethod")));
            this.txtGenbaNm.Size = new System.Drawing.Size(300, 40);
            this.txtGenbaNm.TabIndex = 170;
            this.txtGenbaNm.TabStop = false;
            this.txtGenbaNm.Tag = " ";
            // 
            // txtName2
            // 
            this.txtName2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtName2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtName2.DBFieldsName = "";
            this.txtName2.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtName2.DisplayPopUp = null;
            this.txtName2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtName2.FocusOutCheckMethod")));
            this.txtName2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtName2.ForeColor = System.Drawing.Color.Black;
            this.txtName2.IsInputErrorOccured = false;
            this.txtName2.Location = new System.Drawing.Point(594, 112);
            this.txtName2.MaxLength = 20;
            this.txtName2.Name = "txtName2";
            this.txtName2.PopupAfterExecute = null;
            this.txtName2.PopupBeforeExecute = null;
            this.txtName2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtName2.PopupSearchSendParams")));
            this.txtName2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtName2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtName2.popupWindowSetting")));
            this.txtName2.ReadOnly = true;
            this.txtName2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtName2.RegistCheckMethod")));
            this.txtName2.Size = new System.Drawing.Size(399, 20);
            this.txtName2.TabIndex = 150;
            this.txtName2.TabStop = false;
            this.txtName2.Tag = " ";
            // 
            // txtADDRESS2
            // 
            this.txtADDRESS2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtADDRESS2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtADDRESS2.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtADDRESS2.DBFieldsName = "";
            this.txtADDRESS2.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtADDRESS2.DisplayPopUp = null;
            this.txtADDRESS2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtADDRESS2.FocusOutCheckMethod")));
            this.txtADDRESS2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtADDRESS2.ForeColor = System.Drawing.Color.Black;
            this.txtADDRESS2.IsInputErrorOccured = false;
            this.txtADDRESS2.Location = new System.Drawing.Point(594, 67);
            this.txtADDRESS2.MaxLength = 20;
            this.txtADDRESS2.Name = "txtADDRESS2";
            this.txtADDRESS2.PopupAfterExecute = null;
            this.txtADDRESS2.PopupBeforeExecute = null;
            this.txtADDRESS2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtADDRESS2.PopupSearchSendParams")));
            this.txtADDRESS2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtADDRESS2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtADDRESS2.popupWindowSetting")));
            this.txtADDRESS2.ReadOnly = true;
            this.txtADDRESS2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtADDRESS2.RegistCheckMethod")));
            this.txtADDRESS2.Size = new System.Drawing.Size(399, 20);
            this.txtADDRESS2.TabIndex = 150;
            this.txtADDRESS2.TabStop = false;
            this.txtADDRESS2.Tag = " ";
            // 
            // hiddenValue
            // 
            this.hiddenValue.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.hiddenValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.hiddenValue.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.hiddenValue.DBFieldsName = "";
            this.hiddenValue.DefaultBackColor = System.Drawing.Color.Empty;
            this.hiddenValue.DisplayPopUp = null;
            this.hiddenValue.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("hiddenValue.FocusOutCheckMethod")));
            this.hiddenValue.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.hiddenValue.ForeColor = System.Drawing.Color.Black;
            this.hiddenValue.IsInputErrorOccured = false;
            this.hiddenValue.Location = new System.Drawing.Point(255, 175);
            this.hiddenValue.MaxLength = 0;
            this.hiddenValue.Name = "hiddenValue";
            this.hiddenValue.PopupAfterExecute = null;
            this.hiddenValue.PopupBeforeExecute = null;
            this.hiddenValue.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("hiddenValue.PopupSearchSendParams")));
            this.hiddenValue.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.hiddenValue.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("hiddenValue.popupWindowSetting")));
            this.hiddenValue.ReadOnly = true;
            this.hiddenValue.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("hiddenValue.RegistCheckMethod")));
            this.hiddenValue.Size = new System.Drawing.Size(53, 20);
            this.hiddenValue.TabIndex = 150;
            this.hiddenValue.TabStop = false;
            this.hiddenValue.Tag = " ";
            this.hiddenValue.Text = "0";
            this.hiddenValue.Visible = false;
            // 
            // txtGenbaTel
            // 
            this.txtGenbaTel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtGenbaTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGenbaTel.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtGenbaTel.DBFieldsName = "";
            this.txtGenbaTel.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtGenbaTel.DisplayPopUp = null;
            this.txtGenbaTel.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGenbaTel.FocusOutCheckMethod")));
            this.txtGenbaTel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtGenbaTel.ForeColor = System.Drawing.Color.Black;
            this.txtGenbaTel.IsInputErrorOccured = false;
            this.txtGenbaTel.Location = new System.Drawing.Point(117, 175);
            this.txtGenbaTel.MaxLength = 13;
            this.txtGenbaTel.Name = "txtGenbaTel";
            this.txtGenbaTel.PopupAfterExecute = null;
            this.txtGenbaTel.PopupBeforeExecute = null;
            this.txtGenbaTel.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtGenbaTel.PopupSearchSendParams")));
            this.txtGenbaTel.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtGenbaTel.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtGenbaTel.popupWindowSetting")));
            this.txtGenbaTel.ReadOnly = true;
            this.txtGenbaTel.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGenbaTel.RegistCheckMethod")));
            this.txtGenbaTel.Size = new System.Drawing.Size(110, 20);
            this.txtGenbaTel.TabIndex = 150;
            this.txtGenbaTel.TabStop = false;
            this.txtGenbaTel.Tag = " ";
            this.txtGenbaTel.Text = "030-4396-6668";
            // 
            // txtTel
            // 
            this.txtTel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtTel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTel.CharactersNumber = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.txtTel.DBFieldsName = "";
            this.txtTel.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtTel.DisplayPopUp = null;
            this.txtTel.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTel.FocusOutCheckMethod")));
            this.txtTel.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtTel.ForeColor = System.Drawing.Color.Black;
            this.txtTel.IsInputErrorOccured = false;
            this.txtTel.Location = new System.Drawing.Point(594, 176);
            this.txtTel.MaxLength = 13;
            this.txtTel.Name = "txtTel";
            this.txtTel.PopupAfterExecute = null;
            this.txtTel.PopupBeforeExecute = null;
            this.txtTel.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtTel.PopupSearchSendParams")));
            this.txtTel.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtTel.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtTel.popupWindowSetting")));
            this.txtTel.ReadOnly = true;
            this.txtTel.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTel.RegistCheckMethod")));
            this.txtTel.Size = new System.Drawing.Size(110, 20);
            this.txtTel.TabIndex = 150;
            this.txtTel.TabStop = false;
            this.txtTel.Tag = " ";
            // 
            // txtDAIHYOUSHA
            // 
            this.txtDAIHYOUSHA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtDAIHYOUSHA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDAIHYOUSHA.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtDAIHYOUSHA.DBFieldsName = "";
            this.txtDAIHYOUSHA.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtDAIHYOUSHA.DisplayPopUp = null;
            this.txtDAIHYOUSHA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtDAIHYOUSHA.FocusOutCheckMethod")));
            this.txtDAIHYOUSHA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtDAIHYOUSHA.ForeColor = System.Drawing.Color.Black;
            this.txtDAIHYOUSHA.IsInputErrorOccured = false;
            this.txtDAIHYOUSHA.Location = new System.Drawing.Point(594, 134);
            this.txtDAIHYOUSHA.MaxLength = 20;
            this.txtDAIHYOUSHA.Name = "txtDAIHYOUSHA";
            this.txtDAIHYOUSHA.PopupAfterExecute = null;
            this.txtDAIHYOUSHA.PopupBeforeExecute = null;
            this.txtDAIHYOUSHA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtDAIHYOUSHA.PopupSearchSendParams")));
            this.txtDAIHYOUSHA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtDAIHYOUSHA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtDAIHYOUSHA.popupWindowSetting")));
            this.txtDAIHYOUSHA.ReadOnly = true;
            this.txtDAIHYOUSHA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtDAIHYOUSHA.RegistCheckMethod")));
            this.txtDAIHYOUSHA.Size = new System.Drawing.Size(399, 20);
            this.txtDAIHYOUSHA.TabIndex = 150;
            this.txtDAIHYOUSHA.TabStop = false;
            this.txtDAIHYOUSHA.Tag = " ";
            // 
            // txtName1
            // 
            this.txtName1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtName1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtName1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtName1.DBFieldsName = "";
            this.txtName1.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtName1.DisplayPopUp = null;
            this.txtName1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtName1.FocusOutCheckMethod")));
            this.txtName1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtName1.ForeColor = System.Drawing.Color.Black;
            this.txtName1.IsInputErrorOccured = false;
            this.txtName1.Location = new System.Drawing.Point(594, 90);
            this.txtName1.MaxLength = 20;
            this.txtName1.Name = "txtName1";
            this.txtName1.PopupAfterExecute = null;
            this.txtName1.PopupBeforeExecute = null;
            this.txtName1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtName1.PopupSearchSendParams")));
            this.txtName1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtName1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtName1.popupWindowSetting")));
            this.txtName1.ReadOnly = true;
            this.txtName1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtName1.RegistCheckMethod")));
            this.txtName1.Size = new System.Drawing.Size(399, 20);
            this.txtName1.TabIndex = 150;
            this.txtName1.TabStop = false;
            this.txtName1.Tag = " ";
            // 
            // txtADDRESS1
            // 
            this.txtADDRESS1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtADDRESS1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtADDRESS1.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtADDRESS1.DBFieldsName = "";
            this.txtADDRESS1.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtADDRESS1.DisplayPopUp = null;
            this.txtADDRESS1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtADDRESS1.FocusOutCheckMethod")));
            this.txtADDRESS1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtADDRESS1.ForeColor = System.Drawing.Color.Black;
            this.txtADDRESS1.IsInputErrorOccured = false;
            this.txtADDRESS1.Location = new System.Drawing.Point(594, 45);
            this.txtADDRESS1.MaxLength = 20;
            this.txtADDRESS1.Name = "txtADDRESS1";
            this.txtADDRESS1.PopupAfterExecute = null;
            this.txtADDRESS1.PopupBeforeExecute = null;
            this.txtADDRESS1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtADDRESS1.PopupSearchSendParams")));
            this.txtADDRESS1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtADDRESS1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtADDRESS1.popupWindowSetting")));
            this.txtADDRESS1.ReadOnly = true;
            this.txtADDRESS1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtADDRESS1.RegistCheckMethod")));
            this.txtADDRESS1.Size = new System.Drawing.Size(399, 20);
            this.txtADDRESS1.TabIndex = 150;
            this.txtADDRESS1.TabStop = false;
            this.txtADDRESS1.Tag = " ";
            // 
            // txtGYOUSHUNM
            // 
            this.txtGYOUSHUNM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtGYOUSHUNM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtGYOUSHUNM.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txtGYOUSHUNM.DBFieldsName = "";
            this.txtGYOUSHUNM.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtGYOUSHUNM.DisplayPopUp = null;
            this.txtGYOUSHUNM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGYOUSHUNM.FocusOutCheckMethod")));
            this.txtGYOUSHUNM.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtGYOUSHUNM.ForeColor = System.Drawing.Color.Black;
            this.txtGYOUSHUNM.IsInputErrorOccured = false;
            this.txtGYOUSHUNM.Location = new System.Drawing.Point(166, 153);
            this.txtGYOUSHUNM.MaxLength = 20;
            this.txtGYOUSHUNM.Name = "txtGYOUSHUNM";
            this.txtGYOUSHUNM.PopupAfterExecute = null;
            this.txtGYOUSHUNM.PopupBeforeExecute = null;
            this.txtGYOUSHUNM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtGYOUSHUNM.PopupSearchSendParams")));
            this.txtGYOUSHUNM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtGYOUSHUNM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtGYOUSHUNM.popupWindowSetting")));
            this.txtGYOUSHUNM.ReadOnly = true;
            this.txtGYOUSHUNM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtGYOUSHUNM.RegistCheckMethod")));
            this.txtGYOUSHUNM.Size = new System.Drawing.Size(251, 20);
            this.txtGYOUSHUNM.TabIndex = 85;
            this.txtGYOUSHUNM.TabStop = false;
            this.txtGYOUSHUNM.Tag = " ";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 601;
            this.label3.Text = "提出先";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpCreadDate
            // 
            this.dtpCreadDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.dtpCreadDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dtpCreadDate.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.dtpCreadDate.Checked = false;
            this.dtpCreadDate.CustomFormat = "yyyy/MM/dd(ddd)";
            this.dtpCreadDate.DateTimeNowYear = "";
            this.dtpCreadDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.dtpCreadDate.DisplayItemName = "伝票日付範囲指定（From）";
            this.dtpCreadDate.DisplayPopUp = null;
            this.dtpCreadDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpCreadDate.FocusOutCheckMethod")));
            this.dtpCreadDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.dtpCreadDate.ForeColor = System.Drawing.Color.Black;
            this.dtpCreadDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpCreadDate.IsInputErrorOccured = false;
            this.dtpCreadDate.Location = new System.Drawing.Point(625, 2);
            this.dtpCreadDate.MaxLength = 10;
            this.dtpCreadDate.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpCreadDate.Name = "dtpCreadDate";
            this.dtpCreadDate.NullValue = "";
            this.dtpCreadDate.PopupAfterExecute = null;
            this.dtpCreadDate.PopupBeforeExecute = null;
            this.dtpCreadDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dtpCreadDate.PopupSearchSendParams")));
            this.dtpCreadDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dtpCreadDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dtpCreadDate.popupWindowSetting")));
            this.dtpCreadDate.ReadOnly = true;
            this.dtpCreadDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dtpCreadDate.RegistCheckMethod")));
            this.dtpCreadDate.Size = new System.Drawing.Size(138, 20);
            this.dtpCreadDate.TabIndex = 40;
            this.dtpCreadDate.TabStop = false;
            this.dtpCreadDate.Tag = " ";
            this.dtpCreadDate.Text = "2013/12/11(水)";
            this.dtpCreadDate.Value = new System.DateTime(2013, 12, 11, 0, 0, 0, 0);
            // 
            // label25
            // 
            this.label25.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label25.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label25.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label25.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label25.ForeColor = System.Drawing.Color.White;
            this.label25.Location = new System.Drawing.Point(3, 176);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(110, 20);
            this.label25.TabIndex = 606;
            this.label25.Text = "電話番号";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label38.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label38.ForeColor = System.Drawing.Color.White;
            this.label38.Location = new System.Drawing.Point(479, 176);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(110, 20);
            this.label38.TabIndex = 606;
            this.label38.Text = "電話番号";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label37
            // 
            this.label37.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label37.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label37.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label37.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label37.ForeColor = System.Drawing.Color.White;
            this.label37.Location = new System.Drawing.Point(479, 134);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(110, 20);
            this.label37.TabIndex = 606;
            this.label37.Text = "代表者";
            this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label36.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label36.ForeColor = System.Drawing.Color.White;
            this.label36.Location = new System.Drawing.Point(479, 90);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(110, 20);
            this.label36.TabIndex = 606;
            this.label36.Text = "氏名";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label21.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label21.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(479, 45);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(110, 20);
            this.label21.TabIndex = 606;
            this.label21.Text = "住所";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label23.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label23.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label23.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(510, 2);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(110, 20);
            this.label23.TabIndex = 606;
            this.label23.Text = "作成日";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label28
            // 
            this.label28.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label28.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label28.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label28.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label28.ForeColor = System.Drawing.Color.White;
            this.label28.Location = new System.Drawing.Point(3, 112);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(110, 20);
            this.label28.TabIndex = 605;
            this.label28.Text = "事業場の所在地";
            this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label29
            // 
            this.label29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label29.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label29.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label29.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label29.ForeColor = System.Drawing.Color.White;
            this.label29.Location = new System.Drawing.Point(3, 69);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(110, 20);
            this.label29.TabIndex = 605;
            this.label29.Text = "事業場の名称";
            this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label31.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label31.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label31.ForeColor = System.Drawing.Color.White;
            this.label31.Location = new System.Drawing.Point(3, 154);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(110, 20);
            this.label31.TabIndex = 605;
            this.label31.Text = "業種";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 258);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(138, 20);
            this.label1.TabIndex = 606;
            this.label1.Text = "排出量合計(t)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtAllHaishuRyo
            // 
            this.txtAllHaishuRyo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.txtAllHaishuRyo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtAllHaishuRyo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAllHaishuRyo.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txtAllHaishuRyo.DBFieldsName = "";
            this.txtAllHaishuRyo.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtAllHaishuRyo.DisplayPopUp = null;
            this.txtAllHaishuRyo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtAllHaishuRyo.FocusOutCheckMethod")));
            this.txtAllHaishuRyo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtAllHaishuRyo.ForeColor = System.Drawing.Color.Black;
            this.txtAllHaishuRyo.IsInputErrorOccured = false;
            this.txtAllHaishuRyo.Location = new System.Drawing.Point(147, 258);
            this.txtAllHaishuRyo.MaxLength = 0;
            this.txtAllHaishuRyo.Name = "txtAllHaishuRyo";
            this.txtAllHaishuRyo.PopupAfterExecute = null;
            this.txtAllHaishuRyo.PopupBeforeExecute = null;
            this.txtAllHaishuRyo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtAllHaishuRyo.PopupSearchSendParams")));
            this.txtAllHaishuRyo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtAllHaishuRyo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtAllHaishuRyo.popupWindowSetting")));
            this.txtAllHaishuRyo.ReadOnly = true;
            this.txtAllHaishuRyo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtAllHaishuRyo.RegistCheckMethod")));
            this.txtAllHaishuRyo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.txtAllHaishuRyo.Size = new System.Drawing.Size(110, 20);
            this.txtAllHaishuRyo.TabIndex = 150;
            this.txtAllHaishuRyo.TabStop = false;
            this.txtAllHaishuRyo.Tag = " ";
            this.txtAllHaishuRyo.Text = "0";
            // 
            // customPanel3
            // 
            this.customPanel3.Controls.Add(this.label1);
            this.customPanel3.Controls.Add(this.txtAllHaishuRyo);
            this.customPanel3.Controls.Add(this.IchiranGRD);
            this.customPanel3.Location = new System.Drawing.Point(0, 202);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(993, 284);
            this.customPanel3.TabIndex = 730;
            // 
            // ROW_NO
            // 
            this.ROW_NO.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.ROW_NO.DefaultCellStyle = dataGridViewCellStyle2;
            this.ROW_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ROW_NO.FocusOutCheckMethod")));
            this.ROW_NO.HeaderText = "番号";
            this.ROW_NO.Name = "ROW_NO";
            this.ROW_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ROW_NO.PopupSearchSendParams")));
            this.ROW_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ROW_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ROW_NO.popupWindowSetting")));
            this.ROW_NO.ReadOnly = true;
            this.ROW_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ROW_NO.RegistCheckMethod")));
            this.ROW_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ROW_NO.Width = 50;
            // 
            // HAIKI_SHURUI_NAME_RYAKU
            // 
            this.HAIKI_SHURUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.HAIKI_SHURUI_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle3;
            this.HAIKI_SHURUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.HAIKI_SHURUI_NAME_RYAKU.HeaderText = "産業廃棄物の種類";
            this.HAIKI_SHURUI_NAME_RYAKU.Name = "HAIKI_SHURUI_NAME_RYAKU";
            this.HAIKI_SHURUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.PopupSearchSendParams")));
            this.HAIKI_SHURUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAIKI_SHURUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.popupWindowSetting")));
            this.HAIKI_SHURUI_NAME_RYAKU.ReadOnly = true;
            this.HAIKI_SHURUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAIKI_SHURUI_NAME_RYAKU.RegistCheckMethod")));
            this.HAIKI_SHURUI_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HAIKI_SHURUI_NAME_RYAKU.Width = 130;
            // 
            // HAISHU_RYOU
            // 
            this.HAISHU_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.HAISHU_RYOU.DefaultCellStyle = dataGridViewCellStyle4;
            this.HAISHU_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHU_RYOU.FocusOutCheckMethod")));
            this.HAISHU_RYOU.FormatSetting = "システム設定(マニフェスト書式)";
            this.HAISHU_RYOU.HeaderText = "排出量(t)";
            this.HAISHU_RYOU.ItemDefinedTypes = "decimal";
            this.HAISHU_RYOU.Name = "HAISHU_RYOU";
            this.HAISHU_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HAISHU_RYOU.PopupSearchSendParams")));
            this.HAISHU_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HAISHU_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HAISHU_RYOU.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.HAISHU_RYOU.RangeSetting = rangeSettingDto1;
            this.HAISHU_RYOU.ReadOnly = true;
            this.HAISHU_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HAISHU_RYOU.RegistCheckMethod")));
            this.HAISHU_RYOU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.HAISHU_RYOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // COUFUMAISUU
            // 
            this.COUFUMAISUU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.COUFUMAISUU.DefaultCellStyle = dataGridViewCellStyle5;
            this.COUFUMAISUU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COUFUMAISUU.FocusOutCheckMethod")));
            this.COUFUMAISUU.HeaderText = "管理票の交付枚数";
            this.COUFUMAISUU.MaxInputLength = 2;
            this.COUFUMAISUU.Name = "COUFUMAISUU";
            this.COUFUMAISUU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COUFUMAISUU.PopupSearchSendParams")));
            this.COUFUMAISUU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COUFUMAISUU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COUFUMAISUU.popupWindowSetting")));
            this.COUFUMAISUU.ReadOnly = true;
            this.COUFUMAISUU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COUFUMAISUU.RegistCheckMethod")));
            this.COUFUMAISUU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.COUFUMAISUU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COUFUMAISUU.Width = 110;
            // 
            // UPN_FUTSUU_KYOKA_NO
            // 
            this.UPN_FUTSUU_KYOKA_NO.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.UPN_FUTSUU_KYOKA_NO.DefaultCellStyle = dataGridViewCellStyle6;
            this.UPN_FUTSUU_KYOKA_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_FUTSUU_KYOKA_NO.FocusOutCheckMethod")));
            this.UPN_FUTSUU_KYOKA_NO.HeaderText = "運搬受託者の許可番号";
            this.UPN_FUTSUU_KYOKA_NO.Name = "UPN_FUTSUU_KYOKA_NO";
            this.UPN_FUTSUU_KYOKA_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPN_FUTSUU_KYOKA_NO.PopupSearchSendParams")));
            this.UPN_FUTSUU_KYOKA_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPN_FUTSUU_KYOKA_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPN_FUTSUU_KYOKA_NO.popupWindowSetting")));
            this.UPN_FUTSUU_KYOKA_NO.ReadOnly = true;
            this.UPN_FUTSUU_KYOKA_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_FUTSUU_KYOKA_NO.RegistCheckMethod")));
            this.UPN_FUTSUU_KYOKA_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UPN_GYOUSHA_NAME
            // 
            this.UPN_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UPN_GYOUSHA_NAME.DefaultCellStyle = dataGridViewCellStyle7;
            this.UPN_GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_GYOUSHA_NAME.FocusOutCheckMethod")));
            this.UPN_GYOUSHA_NAME.HeaderText = "運搬受託者の氏名又は名称";
            this.UPN_GYOUSHA_NAME.Name = "UPN_GYOUSHA_NAME";
            this.UPN_GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPN_GYOUSHA_NAME.PopupSearchSendParams")));
            this.UPN_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPN_GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPN_GYOUSHA_NAME.popupWindowSetting")));
            this.UPN_GYOUSHA_NAME.ReadOnly = true;
            this.UPN_GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_GYOUSHA_NAME.RegistCheckMethod")));
            this.UPN_GYOUSHA_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPN_GYOUSHA_NAME.Width = 130;
            // 
            // UPN_SAKI_GENBA_ADDRESS
            // 
            this.UPN_SAKI_GENBA_ADDRESS.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UPN_SAKI_GENBA_ADDRESS.DefaultCellStyle = dataGridViewCellStyle8;
            this.UPN_SAKI_GENBA_ADDRESS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_SAKI_GENBA_ADDRESS.FocusOutCheckMethod")));
            this.UPN_SAKI_GENBA_ADDRESS.HeaderText = "運搬先の住所";
            this.UPN_SAKI_GENBA_ADDRESS.Name = "UPN_SAKI_GENBA_ADDRESS";
            this.UPN_SAKI_GENBA_ADDRESS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPN_SAKI_GENBA_ADDRESS.PopupSearchSendParams")));
            this.UPN_SAKI_GENBA_ADDRESS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPN_SAKI_GENBA_ADDRESS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPN_SAKI_GENBA_ADDRESS.popupWindowSetting")));
            this.UPN_SAKI_GENBA_ADDRESS.ReadOnly = true;
            this.UPN_SAKI_GENBA_ADDRESS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPN_SAKI_GENBA_ADDRESS.RegistCheckMethod")));
            this.UPN_SAKI_GENBA_ADDRESS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPN_SAKI_GENBA_ADDRESS.Width = 130;
            // 
            // SBN_FUTSUU_KYOKA_NO
            // 
            this.SBN_FUTSUU_KYOKA_NO.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.SBN_FUTSUU_KYOKA_NO.DefaultCellStyle = dataGridViewCellStyle9;
            this.SBN_FUTSUU_KYOKA_NO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SBN_FUTSUU_KYOKA_NO.FocusOutCheckMethod")));
            this.SBN_FUTSUU_KYOKA_NO.HeaderText = "処分受託者の許可番号";
            this.SBN_FUTSUU_KYOKA_NO.Name = "SBN_FUTSUU_KYOKA_NO";
            this.SBN_FUTSUU_KYOKA_NO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SBN_FUTSUU_KYOKA_NO.PopupSearchSendParams")));
            this.SBN_FUTSUU_KYOKA_NO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SBN_FUTSUU_KYOKA_NO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SBN_FUTSUU_KYOKA_NO.popupWindowSetting")));
            this.SBN_FUTSUU_KYOKA_NO.ReadOnly = true;
            this.SBN_FUTSUU_KYOKA_NO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SBN_FUTSUU_KYOKA_NO.RegistCheckMethod")));
            this.SBN_FUTSUU_KYOKA_NO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SBN_GYOUSHA_NAME
            // 
            this.SBN_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SBN_GYOUSHA_NAME.DefaultCellStyle = dataGridViewCellStyle10;
            this.SBN_GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SBN_GYOUSHA_NAME.FocusOutCheckMethod")));
            this.SBN_GYOUSHA_NAME.HeaderText = "処分受託者の氏名又は名称";
            this.SBN_GYOUSHA_NAME.Name = "SBN_GYOUSHA_NAME";
            this.SBN_GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SBN_GYOUSHA_NAME.PopupSearchSendParams")));
            this.SBN_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SBN_GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SBN_GYOUSHA_NAME.popupWindowSetting")));
            this.SBN_GYOUSHA_NAME.ReadOnly = true;
            this.SBN_GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SBN_GYOUSHA_NAME.RegistCheckMethod")));
            this.SBN_GYOUSHA_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SBN_GYOUSHA_NAME.Width = 130;
            // 
            // SBN_GENBA_ADDRESS
            // 
            this.SBN_GENBA_ADDRESS.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.SBN_GENBA_ADDRESS.DefaultCellStyle = dataGridViewCellStyle11;
            this.SBN_GENBA_ADDRESS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SBN_GENBA_ADDRESS.FocusOutCheckMethod")));
            this.SBN_GENBA_ADDRESS.HeaderText = "処分場所の住所";
            this.SBN_GENBA_ADDRESS.Name = "SBN_GENBA_ADDRESS";
            this.SBN_GENBA_ADDRESS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SBN_GENBA_ADDRESS.PopupSearchSendParams")));
            this.SBN_GENBA_ADDRESS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SBN_GENBA_ADDRESS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SBN_GENBA_ADDRESS.popupWindowSetting")));
            this.SBN_GENBA_ADDRESS.ReadOnly = true;
            this.SBN_GENBA_ADDRESS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SBN_GENBA_ADDRESS.RegistCheckMethod")));
            this.SBN_GENBA_ADDRESS.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SBN_GENBA_ADDRESS.Width = 130;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.customPanel3);
            this.Controls.Add(this.customPanel1);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.IchiranGRD)).EndInit();
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal r_framework.CustomControl.CustomDataGridView IchiranGRD;
        private r_framework.CustomControl.CustomPanel customPanel1;
        internal r_framework.CustomControl.CustomTextBox txtGenbaAddress;
        internal r_framework.CustomControl.CustomTextBox txtGenbaNm;
        internal r_framework.CustomControl.CustomTextBox txtADDRESS1;
        internal r_framework.CustomControl.CustomTextBox txtGYOUSHUNM;
        public System.Windows.Forms.Label label3;
        public r_framework.CustomControl.CustomDateTimePicker dtpCreadDate;
        public System.Windows.Forms.Label label21;
        public System.Windows.Forms.Label label23;
        public System.Windows.Forms.Label label28;
        public System.Windows.Forms.Label label29;
        public System.Windows.Forms.Label label31;
        internal r_framework.CustomControl.CustomTextBox txtTeishutusaki;
        public System.Windows.Forms.Label label19;
        public System.Windows.Forms.Label label20;
        internal r_framework.CustomControl.CustomTextBox txtName2;
        internal r_framework.CustomControl.CustomTextBox txtADDRESS2;
        internal r_framework.CustomControl.CustomTextBox txtName1;
        public System.Windows.Forms.Label label36;
        internal r_framework.CustomControl.CustomTextBox txtDAIHYOUSHA;
        public System.Windows.Forms.Label label37;
        public System.Windows.Forms.Label label39;
        internal r_framework.CustomControl.CustomTextBox txtTel;
        public System.Windows.Forms.Label label38;
        internal r_framework.CustomControl.CustomTextBox txtGenbaTel;
        public System.Windows.Forms.Label label25;
        internal r_framework.CustomControl.CustomAlphaNumTextBox txtGYOUSHUCD;
        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox txtAllHaishuRyo;
        internal r_framework.CustomControl.CustomTextBox hiddenValue;
        private r_framework.CustomControl.CustomPanel customPanel2;
        internal r_framework.CustomControl.CustomTextBox txtTitle2;
        internal r_framework.CustomControl.CustomTextBox txtTitle1;
        public System.Windows.Forms.Label label2;
        private r_framework.CustomControl.CustomPanel customPanel3;
        private r_framework.CustomControl.DgvCustomTextBoxColumn ROW_NO;
        private r_framework.CustomControl.DgvCustomTextBoxColumn HAIKI_SHURUI_NAME_RYAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column HAISHU_RYOU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn COUFUMAISUU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPN_FUTSUU_KYOKA_NO;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPN_GYOUSHA_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPN_SAKI_GENBA_ADDRESS;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SBN_FUTSUU_KYOKA_NO;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SBN_GYOUSHA_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SBN_GENBA_ADDRESS;






    }
}