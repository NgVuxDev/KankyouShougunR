namespace Shougun.Core.Stock.ZaikoMeisaiNyuuryoku.APP
{
    partial class F18_G165Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(F18_G165Form));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalKingaku = new r_framework.CustomControl.CustomTextBox();
            this.txtTotalSuuryou = new r_framework.CustomControl.CustomTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.txtNisugataUnit = new r_framework.CustomControl.CustomTextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.txtNisugata = new r_framework.CustomControl.CustomTextBox();
            this.txtKingaku = new r_framework.CustomControl.CustomTextBox();
            this.txtJyuuryou = new r_framework.CustomControl.CustomTextBox();
            this.ZaikoMeisaiIchiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.ZAIKO_HINMEI_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.ZAIKO_HINMEI_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.ZAIKO_HIRITSU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.ZAIKO_SUURYOU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.ZAIKO_TANKA = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.ZAIKO_KINGAKU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            ((System.ComponentModel.ISupportInitialize)(this.ZaikoMeisaiIchiran)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(640, 406);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 547;
            this.label3.Text = "在庫金額合計";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(539, 406);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 20);
            this.label2.TabIndex = 546;
            this.label2.Text = "在庫数量合計";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTotalKingaku
            // 
            this.txtTotalKingaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtTotalKingaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalKingaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtTotalKingaku.DisplayPopUp = null;
            this.txtTotalKingaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTotalKingaku.FocusOutCheckMethod")));
            this.txtTotalKingaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTotalKingaku.ForeColor = System.Drawing.Color.Black;
            this.txtTotalKingaku.IsInputErrorOccured = false;
            this.txtTotalKingaku.Location = new System.Drawing.Point(640, 425);
            this.txtTotalKingaku.Name = "txtTotalKingaku";
            this.txtTotalKingaku.PopupAfterExecute = null;
            this.txtTotalKingaku.PopupBeforeExecute = null;
            this.txtTotalKingaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtTotalKingaku.PopupSearchSendParams")));
            this.txtTotalKingaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtTotalKingaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtTotalKingaku.popupWindowSetting")));
            this.txtTotalKingaku.ReadOnly = true;
            this.txtTotalKingaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTotalKingaku.RegistCheckMethod")));
            this.txtTotalKingaku.Size = new System.Drawing.Size(100, 20);
            this.txtTotalKingaku.TabIndex = 90;
            this.txtTotalKingaku.TabStop = false;
            this.txtTotalKingaku.Tag = "";
            this.txtTotalKingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTotalSuuryou
            // 
            this.txtTotalSuuryou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtTotalSuuryou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTotalSuuryou.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtTotalSuuryou.DisplayPopUp = null;
            this.txtTotalSuuryou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTotalSuuryou.FocusOutCheckMethod")));
            this.txtTotalSuuryou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtTotalSuuryou.ForeColor = System.Drawing.Color.Black;
            this.txtTotalSuuryou.IsInputErrorOccured = false;
            this.txtTotalSuuryou.Location = new System.Drawing.Point(539, 425);
            this.txtTotalSuuryou.Name = "txtTotalSuuryou";
            this.txtTotalSuuryou.PopupAfterExecute = null;
            this.txtTotalSuuryou.PopupBeforeExecute = null;
            this.txtTotalSuuryou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtTotalSuuryou.PopupSearchSendParams")));
            this.txtTotalSuuryou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtTotalSuuryou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtTotalSuuryou.popupWindowSetting")));
            this.txtTotalSuuryou.ReadOnly = true;
            this.txtTotalSuuryou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTotalSuuryou.RegistCheckMethod")));
            this.txtTotalSuuryou.Size = new System.Drawing.Size(100, 20);
            this.txtTotalSuuryou.TabIndex = 80;
            this.txtTotalSuuryou.TabStop = false;
            this.txtTotalSuuryou.Tag = "";
            this.txtTotalSuuryou.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(138, 406);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 20);
            this.label9.TabIndex = 540;
            this.label9.Text = "荷姿単位";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label24
            // 
            this.label24.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label24.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label24.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label24.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label24.ForeColor = System.Drawing.Color.White;
            this.label24.Location = new System.Drawing.Point(67, 406);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(70, 20);
            this.label24.TabIndex = 541;
            this.label24.Text = "荷姿数量";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNisugataUnit
            // 
            this.txtNisugataUnit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtNisugataUnit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNisugataUnit.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNisugataUnit.DisplayPopUp = null;
            this.txtNisugataUnit.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNisugataUnit.FocusOutCheckMethod")));
            this.txtNisugataUnit.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNisugataUnit.ForeColor = System.Drawing.Color.Black;
            this.txtNisugataUnit.IsInputErrorOccured = false;
            this.txtNisugataUnit.Location = new System.Drawing.Point(138, 425);
            this.txtNisugataUnit.Name = "txtNisugataUnit";
            this.txtNisugataUnit.PopupAfterExecute = null;
            this.txtNisugataUnit.PopupBeforeExecute = null;
            this.txtNisugataUnit.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNisugataUnit.PopupSearchSendParams")));
            this.txtNisugataUnit.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNisugataUnit.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNisugataUnit.popupWindowSetting")));
            this.txtNisugataUnit.ReadOnly = true;
            this.txtNisugataUnit.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNisugataUnit.RegistCheckMethod")));
            this.txtNisugataUnit.Size = new System.Drawing.Size(70, 20);
            this.txtNisugataUnit.TabIndex = 60;
            this.txtNisugataUnit.TabStop = false;
            this.txtNisugataUnit.Tag = "";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(209, 406);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(70, 20);
            this.label10.TabIndex = 538;
            this.label10.Text = "金額";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label22.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label22.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label22.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(0, 406);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(66, 20);
            this.label22.TabIndex = 539;
            this.label22.Text = "正味重量";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNisugata
            // 
            this.txtNisugata.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtNisugata.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNisugata.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNisugata.DisplayPopUp = null;
            this.txtNisugata.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNisugata.FocusOutCheckMethod")));
            this.txtNisugata.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtNisugata.ForeColor = System.Drawing.Color.Black;
            this.txtNisugata.IsInputErrorOccured = false;
            this.txtNisugata.Location = new System.Drawing.Point(67, 425);
            this.txtNisugata.Name = "txtNisugata";
            this.txtNisugata.PopupAfterExecute = null;
            this.txtNisugata.PopupBeforeExecute = null;
            this.txtNisugata.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNisugata.PopupSearchSendParams")));
            this.txtNisugata.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNisugata.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNisugata.popupWindowSetting")));
            this.txtNisugata.ReadOnly = true;
            this.txtNisugata.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNisugata.RegistCheckMethod")));
            this.txtNisugata.Size = new System.Drawing.Size(70, 20);
            this.txtNisugata.TabIndex = 50;
            this.txtNisugata.TabStop = false;
            this.txtNisugata.Tag = "";
            this.txtNisugata.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtKingaku
            // 
            this.txtKingaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKingaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKingaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKingaku.DisplayPopUp = null;
            this.txtKingaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKingaku.FocusOutCheckMethod")));
            this.txtKingaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtKingaku.ForeColor = System.Drawing.Color.Black;
            this.txtKingaku.IsInputErrorOccured = false;
            this.txtKingaku.Location = new System.Drawing.Point(209, 425);
            this.txtKingaku.Name = "txtKingaku";
            this.txtKingaku.PopupAfterExecute = null;
            this.txtKingaku.PopupBeforeExecute = null;
            this.txtKingaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKingaku.PopupSearchSendParams")));
            this.txtKingaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKingaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKingaku.popupWindowSetting")));
            this.txtKingaku.ReadOnly = true;
            this.txtKingaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKingaku.RegistCheckMethod")));
            this.txtKingaku.Size = new System.Drawing.Size(70, 20);
            this.txtKingaku.TabIndex = 70;
            this.txtKingaku.TabStop = false;
            this.txtKingaku.Tag = "";
            this.txtKingaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtJyuuryou
            // 
            this.txtJyuuryou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtJyuuryou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJyuuryou.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtJyuuryou.DisplayPopUp = null;
            this.txtJyuuryou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtJyuuryou.FocusOutCheckMethod")));
            this.txtJyuuryou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txtJyuuryou.ForeColor = System.Drawing.Color.Black;
            this.txtJyuuryou.IsInputErrorOccured = false;
            this.txtJyuuryou.Location = new System.Drawing.Point(0, 425);
            this.txtJyuuryou.Name = "txtJyuuryou";
            this.txtJyuuryou.PopupAfterExecute = null;
            this.txtJyuuryou.PopupBeforeExecute = null;
            this.txtJyuuryou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtJyuuryou.PopupSearchSendParams")));
            this.txtJyuuryou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtJyuuryou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtJyuuryou.popupWindowSetting")));
            this.txtJyuuryou.ReadOnly = true;
            this.txtJyuuryou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtJyuuryou.RegistCheckMethod")));
            this.txtJyuuryou.Size = new System.Drawing.Size(66, 20);
            this.txtJyuuryou.TabIndex = 40;
            this.txtJyuuryou.TabStop = false;
            this.txtJyuuryou.Tag = "";
            this.txtJyuuryou.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ZaikoMeisaiIchiran
            // 
            this.ZaikoMeisaiIchiran.AllowUserToAddRows = false;
            this.ZaikoMeisaiIchiran.AllowUserToDeleteRows = false;
            this.ZaikoMeisaiIchiran.AllowUserToResizeRows = false;
            this.ZaikoMeisaiIchiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ZaikoMeisaiIchiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.ZaikoMeisaiIchiran.ColumnHeadersHeight = 21;
            this.ZaikoMeisaiIchiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.ZaikoMeisaiIchiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ZAIKO_HINMEI_CD,
            this.ZAIKO_HINMEI_RYAKU,
            this.ZAIKO_HIRITSU,
            this.ZAIKO_SUURYOU,
            this.ZAIKO_TANKA,
            this.ZAIKO_KINGAKU});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ZaikoMeisaiIchiran.DefaultCellStyle = dataGridViewCellStyle8;
            this.ZaikoMeisaiIchiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.ZaikoMeisaiIchiran.EnableHeadersVisualStyles = false;
            this.ZaikoMeisaiIchiran.GridColor = System.Drawing.Color.White;
            this.ZaikoMeisaiIchiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ZaikoMeisaiIchiran.IsReload = false;
            this.ZaikoMeisaiIchiran.LinkedDataPanelName = null;
            this.ZaikoMeisaiIchiran.Location = new System.Drawing.Point(0, 0);
            this.ZaikoMeisaiIchiran.MultiSelect = false;
            this.ZaikoMeisaiIchiran.Name = "ZaikoMeisaiIchiran";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ZaikoMeisaiIchiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.ZaikoMeisaiIchiran.RowHeadersVisible = false;
            this.ZaikoMeisaiIchiran.RowHeadersWidth = 20;
            this.ZaikoMeisaiIchiran.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.ZaikoMeisaiIchiran.RowTemplate.Height = 21;
            this.ZaikoMeisaiIchiran.ShowCellToolTips = false;
            this.ZaikoMeisaiIchiran.Size = new System.Drawing.Size(740, 401);
            this.ZaikoMeisaiIchiran.TabIndex = 30;
            this.ZaikoMeisaiIchiran.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.ZaikoMeisaiIchiran_CellValidated);
            this.ZaikoMeisaiIchiran.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.ZaikoMeisaiIchiran_CellValidating);
            // 
            // ZAIKO_HINMEI_CD
            // 
            this.ZAIKO_HINMEI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ZAIKO_HINMEI_CD.DefaultCellStyle = dataGridViewCellStyle2;
            this.ZAIKO_HINMEI_CD.DisplayItemName = "在庫品名CD";
            this.ZAIKO_HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_CD.GetCodeMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_RYAKU";
            this.ZAIKO_HINMEI_CD.HeaderText = "在庫品名CD※";
            this.ZAIKO_HINMEI_CD.MaxInputLength = 6;
            this.ZAIKO_HINMEI_CD.Name = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_CD.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZAIKO_HINMEI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.ZAIKO_HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_CD.popupWindowSetting")));
            this.ZAIKO_HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD.RegistCheckMethod")));
            this.ZAIKO_HINMEI_CD.SetFormField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_RYAKU";
            this.ZAIKO_HINMEI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_HINMEI_CD.Width = 110;
            this.ZAIKO_HINMEI_CD.ZeroPaddengFlag = true;
            // 
            // ZAIKO_HINMEI_RYAKU
            // 
            this.ZAIKO_HINMEI_RYAKU.DBFieldsName = "HINMEI_NAME";
            this.ZAIKO_HINMEI_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ZAIKO_HINMEI_RYAKU.DefaultCellStyle = dataGridViewCellStyle3;
            this.ZAIKO_HINMEI_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_RYAKU.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_RYAKU.HeaderText = "在庫品名";
            this.ZAIKO_HINMEI_RYAKU.Name = "ZAIKO_HINMEI_RYAKU";
            this.ZAIKO_HINMEI_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_RYAKU.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.ZAIKO_HINMEI_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_RYAKU.popupWindowSetting")));
            this.ZAIKO_HINMEI_RYAKU.ReadOnly = true;
            this.ZAIKO_HINMEI_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_RYAKU.RegistCheckMethod")));
            this.ZAIKO_HINMEI_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_HINMEI_RYAKU.Width = 171;
            // 
            // ZAIKO_HIRITSU
            // 
            this.ZAIKO_HIRITSU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ZAIKO_HIRITSU.DefaultCellStyle = dataGridViewCellStyle4;
            this.ZAIKO_HIRITSU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HIRITSU.FocusOutCheckMethod")));
            this.ZAIKO_HIRITSU.HeaderText = "在庫比率";
            this.ZAIKO_HIRITSU.Name = "ZAIKO_HIRITSU";
            this.ZAIKO_HIRITSU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HIRITSU.PopupSearchSendParams")));
            this.ZAIKO_HIRITSU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_HIRITSU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HIRITSU.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.ZAIKO_HIRITSU.RangeSetting = rangeSettingDto1;
            this.ZAIKO_HIRITSU.ReadOnly = true;
            this.ZAIKO_HIRITSU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HIRITSU.RegistCheckMethod")));
            this.ZAIKO_HIRITSU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_HIRITSU.Width = 90;
            // 
            // ZAIKO_SUURYOU
            // 
            this.ZAIKO_SUURYOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ZAIKO_SUURYOU.DefaultCellStyle = dataGridViewCellStyle5;
            this.ZAIKO_SUURYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_SUURYOU.FocusOutCheckMethod")));
            this.ZAIKO_SUURYOU.FormatSetting = "システム設定(重量書式)";
            this.ZAIKO_SUURYOU.HeaderText = "在庫数量(kg)";
            this.ZAIKO_SUURYOU.Name = "ZAIKO_SUURYOU";
            this.ZAIKO_SUURYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_SUURYOU.PopupSearchSendParams")));
            this.ZAIKO_SUURYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_SUURYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_SUURYOU.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.ZAIKO_SUURYOU.RangeSetting = rangeSettingDto2;
            this.ZAIKO_SUURYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_SUURYOU.RegistCheckMethod")));
            this.ZAIKO_SUURYOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_SUURYOU.Width = 120;
            // 
            // ZAIKO_TANKA
            // 
            this.ZAIKO_TANKA.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ZAIKO_TANKA.DefaultCellStyle = dataGridViewCellStyle6;
            this.ZAIKO_TANKA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_TANKA.FocusOutCheckMethod")));
            this.ZAIKO_TANKA.FormatSetting = "システム設定(単価書式)";
            this.ZAIKO_TANKA.HeaderText = "在庫単価";
            this.ZAIKO_TANKA.Name = "ZAIKO_TANKA";
            this.ZAIKO_TANKA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_TANKA.PopupSearchSendParams")));
            this.ZAIKO_TANKA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_TANKA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_TANKA.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.ZAIKO_TANKA.RangeSetting = rangeSettingDto3;
            this.ZAIKO_TANKA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_TANKA.RegistCheckMethod")));
            this.ZAIKO_TANKA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_TANKA.ToolTipText = "半角6桁以内で入力してください";
            this.ZAIKO_TANKA.Width = 90;
            // 
            // ZAIKO_KINGAKU
            // 
            this.ZAIKO_KINGAKU.CustomFormatSetting = "#,##0";
            this.ZAIKO_KINGAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.ZAIKO_KINGAKU.DefaultCellStyle = dataGridViewCellStyle7;
            this.ZAIKO_KINGAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_KINGAKU.FocusOutCheckMethod")));
            this.ZAIKO_KINGAKU.FormatSetting = "カスタム";
            this.ZAIKO_KINGAKU.HeaderText = "在庫金額";
            this.ZAIKO_KINGAKU.Name = "ZAIKO_KINGAKU";
            this.ZAIKO_KINGAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_KINGAKU.PopupSearchSendParams")));
            this.ZAIKO_KINGAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_KINGAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_KINGAKU.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.ZAIKO_KINGAKU.RangeSetting = rangeSettingDto4;
            this.ZAIKO_KINGAKU.ReadOnly = true;
            this.ZAIKO_KINGAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_KINGAKU.RegistCheckMethod")));
            this.ZAIKO_KINGAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_KINGAKU.Width = 120;
            // 
            // F18_G165Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.ZaikoMeisaiIchiran);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtTotalKingaku);
            this.Controls.Add(this.txtTotalSuuryou);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.txtNisugataUnit);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.txtNisugata);
            this.Controls.Add(this.txtKingaku);
            this.Controls.Add(this.txtJyuuryou);
            this.Name = "F18_G165Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.ZaikoMeisaiIchiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomTextBox txtTotalKingaku;
        public r_framework.CustomControl.CustomTextBox txtTotalSuuryou;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label24;
        public r_framework.CustomControl.CustomTextBox txtNisugataUnit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label22;
        public r_framework.CustomControl.CustomTextBox txtNisugata;
        public r_framework.CustomControl.CustomTextBox txtKingaku;
        public r_framework.CustomControl.CustomTextBox txtJyuuryou;
        public r_framework.CustomControl.CustomDataGridView ZaikoMeisaiIchiran;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn ZAIKO_HINMEI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn ZAIKO_HINMEI_RYAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column ZAIKO_HIRITSU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column ZAIKO_SUURYOU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column ZAIKO_TANKA;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column ZAIKO_KINGAKU;
    }
}