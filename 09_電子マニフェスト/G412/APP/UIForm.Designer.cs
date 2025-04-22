namespace Shougun.Core.ElectronicManifest.TuusinnRirekiShoukai
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            this.cdgrid_tuusinnrireki = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.Column1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.KANRI_ID = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.QUE_SEQ = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.pnl_ManiType = new r_framework.CustomControl.CustomPanel();
            this.crdo_SousinnSippai = new r_framework.CustomControl.CustomRadioButton();
            this.crdo_JWNETEraa = new r_framework.CustomControl.CustomRadioButton();
            this.crdo_JWNETSousinnKannryou = new r_framework.CustomControl.CustomRadioButton();
            this.crdo_SousinnKannryou = new r_framework.CustomControl.CustomRadioButton();
            this.crdo_SousinnMati = new r_framework.CustomControl.CustomRadioButton();
            this.crdo_SousinnHoryuu = new r_framework.CustomControl.CustomRadioButton();
            this.crdo_ZebuHyouji = new r_framework.CustomControl.CustomRadioButton();
            this.cantxt_Jyoutai = new r_framework.CustomControl.CustomNumericTextBox2();
            this.cantxt_ManiFestFrom = new r_framework.CustomControl.CustomNumericTextBox2();
            this.cantxt_ManiFestTo = new r_framework.CustomControl.CustomNumericTextBox2();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewButtonColumn1 = new System.Windows.Forms.DataGridViewButtonColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new r_framework.CustomControl.CustomPanel();
            this.cb_Koushinbi = new r_framework.CustomControl.CustomRadioButton();
            this.cb_Tourokubi = new r_framework.CustomControl.CustomRadioButton();
            this.txt_Hidzuke = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txt_Hidzuke_To = new r_framework.CustomControl.CustomDateTimePicker();
            this.txt_Hidzuke_From = new r_framework.CustomControl.CustomDateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.cdgrid_tuusinnrireki)).BeginInit();
            this.pnl_ManiType.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cdgrid_tuusinnrireki
            // 
            this.cdgrid_tuusinnrireki.AllowUserToAddRows = false;
            this.cdgrid_tuusinnrireki.AllowUserToResizeRows = false;
            this.cdgrid_tuusinnrireki.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cdgrid_tuusinnrireki.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.cdgrid_tuusinnrireki.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.cdgrid_tuusinnrireki.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column3,
            this.Column9,
            this.Column2,
            this.Column8,
            this.Column10,
            this.Column11,
            this.KANRI_ID,
            this.QUE_SEQ});
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.cdgrid_tuusinnrireki.DefaultCellStyle = dataGridViewCellStyle11;
            this.cdgrid_tuusinnrireki.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.cdgrid_tuusinnrireki.EnableHeadersVisualStyles = false;
            this.cdgrid_tuusinnrireki.GridColor = System.Drawing.Color.White;
            this.cdgrid_tuusinnrireki.IsReload = false;
            this.cdgrid_tuusinnrireki.LinkedDataPanelName = null;
            this.cdgrid_tuusinnrireki.Location = new System.Drawing.Point(3, 86);
            this.cdgrid_tuusinnrireki.MultiSelect = false;
            this.cdgrid_tuusinnrireki.Name = "cdgrid_tuusinnrireki";
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.cdgrid_tuusinnrireki.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.cdgrid_tuusinnrireki.RowHeadersVisible = false;
            this.cdgrid_tuusinnrireki.RowTemplate.Height = 21;
            this.cdgrid_tuusinnrireki.ShowCellToolTips = false;
            this.cdgrid_tuusinnrireki.Size = new System.Drawing.Size(989, 383);
            this.cdgrid_tuusinnrireki.TabIndex = 9;
            this.cdgrid_tuusinnrireki.TabStop = false;
            this.cdgrid_tuusinnrireki.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridView1_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
            this.Column1.HeaderText = "";
            this.Column1.MinimumWidth = 47;
            this.Column1.Name = "Column1";
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Column1.Text = "詳細";
            this.Column1.ToolTipText = "マニフェスト情報の詳細を表示します";
            this.Column1.UseColumnTextForButtonValue = true;
            this.Column1.Width = 47;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.Column3.DefaultCellStyle = dataGridViewCellStyle3;
            this.Column3.HeaderText = "通信状態";
            this.Column3.MinimumWidth = 100;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // Column9
            // 
            this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.Column9.DefaultCellStyle = dataGridViewCellStyle4;
            this.Column9.HeaderText = "マニフェスト番号";
            this.Column9.MinimumWidth = 130;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column9.Width = 130;
            // 
            // Column2
            // 
            this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.Column2.DefaultCellStyle = dataGridViewCellStyle5;
            this.Column2.HeaderText = "内容";
            this.Column2.MinimumWidth = 373;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column2.Width = 373;
            // 
            // Column8
            // 
            this.Column8.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.Column8.DefaultCellStyle = dataGridViewCellStyle6;
            this.Column8.HeaderText = "通信番号";
            this.Column8.MinimumWidth = 135;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column8.Width = 135;
            // 
            // Column10
            // 
            this.Column10.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.Column10.DefaultCellStyle = dataGridViewCellStyle7;
            this.Column10.HeaderText = "登録日";
            this.Column10.MinimumWidth = 142;
            this.Column10.Name = "Column10";
            this.Column10.ReadOnly = true;
            this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column10.Width = 142;
            // 
            // Column11
            // 
            this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.Column11.DefaultCellStyle = dataGridViewCellStyle8;
            this.Column11.HeaderText = "更新日";
            this.Column11.MinimumWidth = 142;
            this.Column11.Name = "Column11";
            this.Column11.ReadOnly = true;
            this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Column11.Width = 142;
            // 
            // KANRI_ID
            // 
            this.KANRI_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.KANRI_ID.DefaultCellStyle = dataGridViewCellStyle9;
            this.KANRI_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KANRI_ID.FocusOutCheckMethod")));
            this.KANRI_ID.HeaderText = "KANRI_ID";
            this.KANRI_ID.Name = "KANRI_ID";
            this.KANRI_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KANRI_ID.PopupSearchSendParams")));
            this.KANRI_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KANRI_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KANRI_ID.popupWindowSetting")));
            this.KANRI_ID.ReadOnly = true;
            this.KANRI_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KANRI_ID.RegistCheckMethod")));
            this.KANRI_ID.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.KANRI_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KANRI_ID.Visible = false;
            this.KANRI_ID.Width = 5;
            // 
            // QUE_SEQ
            // 
            this.QUE_SEQ.DataPropertyName = "QUE_SEQ";
            this.QUE_SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.QUE_SEQ.DefaultCellStyle = dataGridViewCellStyle10;
            this.QUE_SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("QUE_SEQ.FocusOutCheckMethod")));
            this.QUE_SEQ.HeaderText = "QUE_SEQ";
            this.QUE_SEQ.Name = "QUE_SEQ";
            this.QUE_SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("QUE_SEQ.PopupSearchSendParams")));
            this.QUE_SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.QUE_SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("QUE_SEQ.popupWindowSetting")));
            this.QUE_SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("QUE_SEQ.RegistCheckMethod")));
            this.QUE_SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.QUE_SEQ.Visible = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "マニフェスト番号";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(255, 2);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "～";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(130, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "状態";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnl_ManiType
            // 
            this.pnl_ManiType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_ManiType.Controls.Add(this.crdo_SousinnSippai);
            this.pnl_ManiType.Controls.Add(this.crdo_JWNETEraa);
            this.pnl_ManiType.Controls.Add(this.crdo_JWNETSousinnKannryou);
            this.pnl_ManiType.Controls.Add(this.crdo_SousinnKannryou);
            this.pnl_ManiType.Controls.Add(this.crdo_SousinnMati);
            this.pnl_ManiType.Controls.Add(this.crdo_SousinnHoryuu);
            this.pnl_ManiType.Controls.Add(this.crdo_ZebuHyouji);
            this.pnl_ManiType.Location = new System.Drawing.Point(159, 27);
            this.pnl_ManiType.Name = "pnl_ManiType";
            this.pnl_ManiType.Size = new System.Drawing.Size(500, 39);
            this.pnl_ManiType.TabIndex = 457;
            // 
            // crdo_SousinnSippai
            // 
            this.crdo_SousinnSippai.AutoSize = true;
            this.crdo_SousinnSippai.DefaultBackColor = System.Drawing.Color.Empty;
            this.crdo_SousinnSippai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_SousinnSippai.FocusOutCheckMethod")));
            this.crdo_SousinnSippai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.crdo_SousinnSippai.LinkedTextBox = "cantxt_Jyoutai";
            this.crdo_SousinnSippai.Location = new System.Drawing.Point(282, 19);
            this.crdo_SousinnSippai.Name = "crdo_SousinnSippai";
            this.crdo_SousinnSippai.PopupAfterExecute = null;
            this.crdo_SousinnSippai.PopupBeforeExecute = null;
            this.crdo_SousinnSippai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crdo_SousinnSippai.PopupSearchSendParams")));
            this.crdo_SousinnSippai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crdo_SousinnSippai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crdo_SousinnSippai.popupWindowSetting")));
            this.crdo_SousinnSippai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_SousinnSippai.RegistCheckMethod")));
            this.crdo_SousinnSippai.Size = new System.Drawing.Size(95, 17);
            this.crdo_SousinnSippai.TabIndex = 6;
            this.crdo_SousinnSippai.Text = "7.送信失敗";
            this.crdo_SousinnSippai.UseVisualStyleBackColor = true;
            this.crdo_SousinnSippai.Value = "7";
            // 
            // crdo_JWNETEraa
            // 
            this.crdo_JWNETEraa.AutoSize = true;
            this.crdo_JWNETEraa.DefaultBackColor = System.Drawing.Color.Empty;
            this.crdo_JWNETEraa.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_JWNETEraa.FocusOutCheckMethod")));
            this.crdo_JWNETEraa.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.crdo_JWNETEraa.LinkedTextBox = "cantxt_Jyoutai";
            this.crdo_JWNETEraa.Location = new System.Drawing.Point(151, 20);
            this.crdo_JWNETEraa.Name = "crdo_JWNETEraa";
            this.crdo_JWNETEraa.PopupAfterExecute = null;
            this.crdo_JWNETEraa.PopupBeforeExecute = null;
            this.crdo_JWNETEraa.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crdo_JWNETEraa.PopupSearchSendParams")));
            this.crdo_JWNETEraa.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crdo_JWNETEraa.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crdo_JWNETEraa.popupWindowSetting")));
            this.crdo_JWNETEraa.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_JWNETEraa.RegistCheckMethod")));
            this.crdo_JWNETEraa.Size = new System.Drawing.Size(116, 17);
            this.crdo_JWNETEraa.TabIndex = 5;
            this.crdo_JWNETEraa.Text = "6.JWNETエラー";
            this.crdo_JWNETEraa.UseVisualStyleBackColor = true;
            this.crdo_JWNETEraa.Value = "6";
            // 
            // crdo_JWNETSousinnKannryou
            // 
            this.crdo_JWNETSousinnKannryou.AutoSize = true;
            this.crdo_JWNETSousinnKannryou.DefaultBackColor = System.Drawing.Color.Empty;
            this.crdo_JWNETSousinnKannryou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_JWNETSousinnKannryou.FocusOutCheckMethod")));
            this.crdo_JWNETSousinnKannryou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.crdo_JWNETSousinnKannryou.LinkedTextBox = "cantxt_Jyoutai";
            this.crdo_JWNETSousinnKannryou.Location = new System.Drawing.Point(6, 20);
            this.crdo_JWNETSousinnKannryou.Name = "crdo_JWNETSousinnKannryou";
            this.crdo_JWNETSousinnKannryou.PopupAfterExecute = null;
            this.crdo_JWNETSousinnKannryou.PopupBeforeExecute = null;
            this.crdo_JWNETSousinnKannryou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crdo_JWNETSousinnKannryou.PopupSearchSendParams")));
            this.crdo_JWNETSousinnKannryou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crdo_JWNETSousinnKannryou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crdo_JWNETSousinnKannryou.popupWindowSetting")));
            this.crdo_JWNETSousinnKannryou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_JWNETSousinnKannryou.RegistCheckMethod")));
            this.crdo_JWNETSousinnKannryou.Size = new System.Drawing.Size(130, 17);
            this.crdo_JWNETSousinnKannryou.TabIndex = 4;
            this.crdo_JWNETSousinnKannryou.Text = "5.JWNET正常完了";
            this.crdo_JWNETSousinnKannryou.UseVisualStyleBackColor = true;
            this.crdo_JWNETSousinnKannryou.Value = "5";
            // 
            // crdo_SousinnKannryou
            // 
            this.crdo_SousinnKannryou.AutoSize = true;
            this.crdo_SousinnKannryou.DefaultBackColor = System.Drawing.Color.Empty;
            this.crdo_SousinnKannryou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_SousinnKannryou.FocusOutCheckMethod")));
            this.crdo_SousinnKannryou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.crdo_SousinnKannryou.LinkedTextBox = "cantxt_Jyoutai";
            this.crdo_SousinnKannryou.Location = new System.Drawing.Point(392, 1);
            this.crdo_SousinnKannryou.Name = "crdo_SousinnKannryou";
            this.crdo_SousinnKannryou.PopupAfterExecute = null;
            this.crdo_SousinnKannryou.PopupBeforeExecute = null;
            this.crdo_SousinnKannryou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crdo_SousinnKannryou.PopupSearchSendParams")));
            this.crdo_SousinnKannryou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crdo_SousinnKannryou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crdo_SousinnKannryou.popupWindowSetting")));
            this.crdo_SousinnKannryou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_SousinnKannryou.RegistCheckMethod")));
            this.crdo_SousinnKannryou.Size = new System.Drawing.Size(95, 17);
            this.crdo_SousinnKannryou.TabIndex = 3;
            this.crdo_SousinnKannryou.Text = "4.送信完了";
            this.crdo_SousinnKannryou.UseVisualStyleBackColor = true;
            this.crdo_SousinnKannryou.Value = "4";
            // 
            // crdo_SousinnMati
            // 
            this.crdo_SousinnMati.AutoSize = true;
            this.crdo_SousinnMati.DefaultBackColor = System.Drawing.Color.Empty;
            this.crdo_SousinnMati.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_SousinnMati.FocusOutCheckMethod")));
            this.crdo_SousinnMati.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.crdo_SousinnMati.LinkedTextBox = "cantxt_Jyoutai";
            this.crdo_SousinnMati.Location = new System.Drawing.Point(282, 1);
            this.crdo_SousinnMati.Name = "crdo_SousinnMati";
            this.crdo_SousinnMati.PopupAfterExecute = null;
            this.crdo_SousinnMati.PopupBeforeExecute = null;
            this.crdo_SousinnMati.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crdo_SousinnMati.PopupSearchSendParams")));
            this.crdo_SousinnMati.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crdo_SousinnMati.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crdo_SousinnMati.popupWindowSetting")));
            this.crdo_SousinnMati.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_SousinnMati.RegistCheckMethod")));
            this.crdo_SousinnMati.Size = new System.Drawing.Size(95, 17);
            this.crdo_SousinnMati.TabIndex = 2;
            this.crdo_SousinnMati.Text = "3.送信待ち";
            this.crdo_SousinnMati.UseVisualStyleBackColor = true;
            this.crdo_SousinnMati.Value = "3";
            // 
            // crdo_SousinnHoryuu
            // 
            this.crdo_SousinnHoryuu.AutoSize = true;
            this.crdo_SousinnHoryuu.DefaultBackColor = System.Drawing.Color.Empty;
            this.crdo_SousinnHoryuu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_SousinnHoryuu.FocusOutCheckMethod")));
            this.crdo_SousinnHoryuu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.crdo_SousinnHoryuu.LinkedTextBox = "cantxt_Jyoutai";
            this.crdo_SousinnHoryuu.Location = new System.Drawing.Point(151, 1);
            this.crdo_SousinnHoryuu.Name = "crdo_SousinnHoryuu";
            this.crdo_SousinnHoryuu.PopupAfterExecute = null;
            this.crdo_SousinnHoryuu.PopupBeforeExecute = null;
            this.crdo_SousinnHoryuu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crdo_SousinnHoryuu.PopupSearchSendParams")));
            this.crdo_SousinnHoryuu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crdo_SousinnHoryuu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crdo_SousinnHoryuu.popupWindowSetting")));
            this.crdo_SousinnHoryuu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_SousinnHoryuu.RegistCheckMethod")));
            this.crdo_SousinnHoryuu.Size = new System.Drawing.Size(95, 17);
            this.crdo_SousinnHoryuu.TabIndex = 1;
            this.crdo_SousinnHoryuu.Text = "2.送信保留";
            this.crdo_SousinnHoryuu.UseVisualStyleBackColor = true;
            this.crdo_SousinnHoryuu.Value = "2";
            // 
            // crdo_ZebuHyouji
            // 
            this.crdo_ZebuHyouji.AutoSize = true;
            this.crdo_ZebuHyouji.DefaultBackColor = System.Drawing.Color.Empty;
            this.crdo_ZebuHyouji.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_ZebuHyouji.FocusOutCheckMethod")));
            this.crdo_ZebuHyouji.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.crdo_ZebuHyouji.LinkedTextBox = "cantxt_Jyoutai";
            this.crdo_ZebuHyouji.Location = new System.Drawing.Point(6, 1);
            this.crdo_ZebuHyouji.Name = "crdo_ZebuHyouji";
            this.crdo_ZebuHyouji.PopupAfterExecute = null;
            this.crdo_ZebuHyouji.PopupBeforeExecute = null;
            this.crdo_ZebuHyouji.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("crdo_ZebuHyouji.PopupSearchSendParams")));
            this.crdo_ZebuHyouji.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.crdo_ZebuHyouji.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("crdo_ZebuHyouji.popupWindowSetting")));
            this.crdo_ZebuHyouji.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("crdo_ZebuHyouji.RegistCheckMethod")));
            this.crdo_ZebuHyouji.Size = new System.Drawing.Size(95, 17);
            this.crdo_ZebuHyouji.TabIndex = 0;
            this.crdo_ZebuHyouji.Text = "1.全て表示";
            this.crdo_ZebuHyouji.UseVisualStyleBackColor = true;
            this.crdo_ZebuHyouji.Value = "1";
            // 
            // cantxt_Jyoutai
            // 
            this.cantxt_Jyoutai.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_Jyoutai.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_Jyoutai.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_Jyoutai.DisplayPopUp = null;
            this.cantxt_Jyoutai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_Jyoutai.FocusOutCheckMethod")));
            this.cantxt_Jyoutai.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_Jyoutai.ForeColor = System.Drawing.Color.Black;
            this.cantxt_Jyoutai.IsInputErrorOccured = false;
            this.cantxt_Jyoutai.LinkedRadioButtonArray = new string[] {
        "crdo_ZebuHyouji",
        "crdo_SousinnHoryuu",
        "crdo_SousinnMati",
        "crdo_SousinnKannryou",
        "crdo_JWNETSousinnKannryou",
        "crdo_JWNETEraa",
        "crdo_SousinnSippai"};
            this.cantxt_Jyoutai.Location = new System.Drawing.Point(138, 27);
            this.cantxt_Jyoutai.Name = "cantxt_Jyoutai";
            this.cantxt_Jyoutai.PopupAfterExecute = null;
            this.cantxt_Jyoutai.PopupBeforeExecute = null;
            this.cantxt_Jyoutai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_Jyoutai.PopupSearchSendParams")));
            this.cantxt_Jyoutai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_Jyoutai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_Jyoutai.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            7,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.cantxt_Jyoutai.RangeSetting = rangeSettingDto1;
            this.cantxt_Jyoutai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_Jyoutai.RegistCheckMethod")));
            this.cantxt_Jyoutai.Size = new System.Drawing.Size(22, 20);
            this.cantxt_Jyoutai.TabIndex = 8;
            this.cantxt_Jyoutai.Tag = "半角1桁以内で入力してください";
            this.cantxt_Jyoutai.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cantxt_Jyoutai.WordWrap = false;
            // 
            // cantxt_ManiFestFrom
            // 
            this.cantxt_ManiFestFrom.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_ManiFestFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_ManiFestFrom.CustomFormatSetting = "00000000000";
            this.cantxt_ManiFestFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_ManiFestFrom.DisplayItemName = "マニフェスト番号From";
            this.cantxt_ManiFestFrom.DisplayPopUp = null;
            this.cantxt_ManiFestFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_ManiFestFrom.FocusOutCheckMethod")));
            this.cantxt_ManiFestFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_ManiFestFrom.ForeColor = System.Drawing.Color.Black;
            this.cantxt_ManiFestFrom.FormatSetting = "カスタム";
            this.cantxt_ManiFestFrom.IsInputErrorOccured = false;
            this.cantxt_ManiFestFrom.Location = new System.Drawing.Point(138, 2);
            this.cantxt_ManiFestFrom.Name = "cantxt_ManiFestFrom";
            this.cantxt_ManiFestFrom.PopupAfterExecute = null;
            this.cantxt_ManiFestFrom.PopupBeforeExecute = null;
            this.cantxt_ManiFestFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_ManiFestFrom.PopupSearchSendParams")));
            this.cantxt_ManiFestFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_ManiFestFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_ManiFestFrom.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.cantxt_ManiFestFrom.RangeSetting = rangeSettingDto2;
            this.cantxt_ManiFestFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_ManiFestFrom.RegistCheckMethod")));
            this.cantxt_ManiFestFrom.Size = new System.Drawing.Size(113, 20);
            this.cantxt_ManiFestFrom.TabIndex = 1;
            this.cantxt_ManiFestFrom.Tag = "半角11桁以内で入力してください";
            this.cantxt_ManiFestFrom.WordWrap = false;
            this.cantxt_ManiFestFrom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cantxt_ManiFestFrom_KeyPress);
            // 
            // cantxt_ManiFestTo
            // 
            this.cantxt_ManiFestTo.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_ManiFestTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_ManiFestTo.CustomFormatSetting = "00000000000";
            this.cantxt_ManiFestTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_ManiFestTo.DisplayItemName = "マニフェスト番号To";
            this.cantxt_ManiFestTo.DisplayPopUp = null;
            this.cantxt_ManiFestTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_ManiFestTo.FocusOutCheckMethod")));
            this.cantxt_ManiFestTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_ManiFestTo.ForeColor = System.Drawing.Color.Black;
            this.cantxt_ManiFestTo.FormatSetting = "カスタム";
            this.cantxt_ManiFestTo.IsInputErrorOccured = false;
            this.cantxt_ManiFestTo.Location = new System.Drawing.Point(295, 2);
            this.cantxt_ManiFestTo.Name = "cantxt_ManiFestTo";
            this.cantxt_ManiFestTo.PopupAfterExecute = null;
            this.cantxt_ManiFestTo.PopupBeforeExecute = null;
            this.cantxt_ManiFestTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_ManiFestTo.PopupSearchSendParams")));
            this.cantxt_ManiFestTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_ManiFestTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_ManiFestTo.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.cantxt_ManiFestTo.RangeSetting = rangeSettingDto3;
            this.cantxt_ManiFestTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_ManiFestTo.RegistCheckMethod")));
            this.cantxt_ManiFestTo.Size = new System.Drawing.Size(113, 20);
            this.cantxt_ManiFestTo.TabIndex = 3;
            this.cantxt_ManiFestTo.Tag = "半角11桁以内で入力してください";
            this.cantxt_ManiFestTo.WordWrap = false;
            this.cantxt_ManiFestTo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cantxt_ManiFestTo_KeyPress);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Width = 20;
            // 
            // dataGridViewButtonColumn1
            // 
            this.dataGridViewButtonColumn1.HeaderText = "";
            this.dataGridViewButtonColumn1.Name = "dataGridViewButtonColumn1";
            this.dataGridViewButtonColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewButtonColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.dataGridViewButtonColumn1.Text = "詳細";
            this.dataGridViewButtonColumn1.ToolTipText = "マニフェスト情報の詳細を表示します";
            this.dataGridViewButtonColumn1.UseColumnTextForButtonValue = true;
            this.dataGridViewButtonColumn1.Width = 70;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "通信番号";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Width = 250;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "マニフェスト番号";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Width = 180;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "内容";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Width = 500;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "通信状態";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Width = 180;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "登録日";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Width = 180;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "更新日";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Width = 180;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(414, 2);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "日付※";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.cb_Koushinbi);
            this.panel1.Controls.Add(this.cb_Tourokubi);
            this.panel1.Location = new System.Drawing.Point(543, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(178, 20);
            this.panel1.TabIndex = 459;
            // 
            // cb_Koushinbi
            // 
            this.cb_Koushinbi.AutoSize = true;
            this.cb_Koushinbi.DefaultBackColor = System.Drawing.Color.Empty;
            this.cb_Koushinbi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cb_Koushinbi.FocusOutCheckMethod")));
            this.cb_Koushinbi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cb_Koushinbi.LinkedTextBox = "txt_Hidzuke";
            this.cb_Koushinbi.Location = new System.Drawing.Point(95, 0);
            this.cb_Koushinbi.Name = "cb_Koushinbi";
            this.cb_Koushinbi.PopupAfterExecute = null;
            this.cb_Koushinbi.PopupBeforeExecute = null;
            this.cb_Koushinbi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cb_Koushinbi.PopupSearchSendParams")));
            this.cb_Koushinbi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cb_Koushinbi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cb_Koushinbi.popupWindowSetting")));
            this.cb_Koushinbi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cb_Koushinbi.RegistCheckMethod")));
            this.cb_Koushinbi.Size = new System.Drawing.Size(81, 17);
            this.cb_Koushinbi.TabIndex = 1;
            this.cb_Koushinbi.Tag = "";
            this.cb_Koushinbi.Text = "2.更新日";
            this.cb_Koushinbi.UseVisualStyleBackColor = true;
            this.cb_Koushinbi.Value = "2";
            // 
            // cb_Tourokubi
            // 
            this.cb_Tourokubi.AutoSize = true;
            this.cb_Tourokubi.DefaultBackColor = System.Drawing.Color.Empty;
            this.cb_Tourokubi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cb_Tourokubi.FocusOutCheckMethod")));
            this.cb_Tourokubi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cb_Tourokubi.LinkedTextBox = "txt_Hidzuke";
            this.cb_Tourokubi.Location = new System.Drawing.Point(9, 0);
            this.cb_Tourokubi.Name = "cb_Tourokubi";
            this.cb_Tourokubi.PopupAfterExecute = null;
            this.cb_Tourokubi.PopupBeforeExecute = null;
            this.cb_Tourokubi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cb_Tourokubi.PopupSearchSendParams")));
            this.cb_Tourokubi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cb_Tourokubi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cb_Tourokubi.popupWindowSetting")));
            this.cb_Tourokubi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cb_Tourokubi.RegistCheckMethod")));
            this.cb_Tourokubi.Size = new System.Drawing.Size(81, 17);
            this.cb_Tourokubi.TabIndex = 0;
            this.cb_Tourokubi.Tag = "";
            this.cb_Tourokubi.Text = "1.登録日";
            this.cb_Tourokubi.UseVisualStyleBackColor = true;
            this.cb_Tourokubi.Value = "1";
            // 
            // txt_Hidzuke
            // 
            this.txt_Hidzuke.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Hidzuke.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Hidzuke.DBFieldsName = "";
            this.txt_Hidzuke.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Hidzuke.DisplayPopUp = null;
            this.txt_Hidzuke.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Hidzuke.FocusOutCheckMethod")));
            this.txt_Hidzuke.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_Hidzuke.ForeColor = System.Drawing.Color.Black;
            this.txt_Hidzuke.IsInputErrorOccured = false;
            this.txt_Hidzuke.ItemDefinedTypes = "smallint";
            this.txt_Hidzuke.LinkedRadioButtonArray = new string[] {
        "cb_Tourokubi",
        "cb_Koushinbi"};
            this.txt_Hidzuke.Location = new System.Drawing.Point(522, 2);
            this.txt_Hidzuke.Name = "txt_Hidzuke";
            this.txt_Hidzuke.PopupAfterExecute = null;
            this.txt_Hidzuke.PopupBeforeExecute = null;
            this.txt_Hidzuke.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Hidzuke.PopupSearchSendParams")));
            this.txt_Hidzuke.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Hidzuke.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Hidzuke.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txt_Hidzuke.RangeSetting = rangeSettingDto4;
            this.txt_Hidzuke.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Hidzuke.RegistCheckMethod")));
            this.txt_Hidzuke.Size = new System.Drawing.Size(22, 20);
            this.txt_Hidzuke.TabIndex = 5;
            this.txt_Hidzuke.Tag = "半角1桁以内で入力してください";
            this.txt_Hidzuke.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txt_Hidzuke.WordWrap = false;
            // 
            // txt_Hidzuke_To
            // 
            this.txt_Hidzuke_To.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Hidzuke_To.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Hidzuke_To.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.txt_Hidzuke_To.Checked = false;
            this.txt_Hidzuke_To.CustomFormat = "yyyy/MM/dd(ddd)";
            this.txt_Hidzuke_To.DateTimeNowYear = "";
            this.txt_Hidzuke_To.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Hidzuke_To.DisplayPopUp = null;
            this.txt_Hidzuke_To.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Hidzuke_To.FocusOutCheckMethod")));
            this.txt_Hidzuke_To.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_Hidzuke_To.ForeColor = System.Drawing.Color.Black;
            this.txt_Hidzuke_To.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_Hidzuke_To.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_Hidzuke_To.IsInputErrorOccured = false;
            this.txt_Hidzuke_To.Location = new System.Drawing.Point(878, 2);
            this.txt_Hidzuke_To.MaxLength = 10;
            this.txt_Hidzuke_To.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.txt_Hidzuke_To.Name = "txt_Hidzuke_To";
            this.txt_Hidzuke_To.NullValue = "";
            this.txt_Hidzuke_To.PopupAfterExecute = null;
            this.txt_Hidzuke_To.PopupBeforeExecute = null;
            this.txt_Hidzuke_To.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Hidzuke_To.PopupSearchSendParams")));
            this.txt_Hidzuke_To.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Hidzuke_To.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Hidzuke_To.popupWindowSetting")));
            this.txt_Hidzuke_To.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Hidzuke_To.RegistCheckMethod")));
            this.txt_Hidzuke_To.Size = new System.Drawing.Size(115, 20);
            this.txt_Hidzuke_To.TabIndex = 7;
            this.txt_Hidzuke_To.Tag = "日付を選択してください";
            this.txt_Hidzuke_To.Text = "2013/11/12(火)";
            this.txt_Hidzuke_To.Value = new System.DateTime(2013, 11, 12, 0, 0, 0, 0);
            this.txt_Hidzuke_To.DoubleClick += new System.EventHandler(this.txt_Hidzuke_To_DoubleClick);
            // 
            // txt_Hidzuke_From
            // 
            this.txt_Hidzuke_From.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Hidzuke_From.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Hidzuke_From.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.txt_Hidzuke_From.Checked = false;
            this.txt_Hidzuke_From.CustomFormat = "yyyy/MM/dd(ddd)";
            this.txt_Hidzuke_From.DateTimeNowYear = "";
            this.txt_Hidzuke_From.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Hidzuke_From.DisplayPopUp = null;
            this.txt_Hidzuke_From.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Hidzuke_From.FocusOutCheckMethod")));
            this.txt_Hidzuke_From.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_Hidzuke_From.ForeColor = System.Drawing.Color.Black;
            this.txt_Hidzuke_From.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txt_Hidzuke_From.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_Hidzuke_From.IsInputErrorOccured = false;
            this.txt_Hidzuke_From.Location = new System.Drawing.Point(728, 2);
            this.txt_Hidzuke_From.MaxLength = 10;
            this.txt_Hidzuke_From.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.txt_Hidzuke_From.Name = "txt_Hidzuke_From";
            this.txt_Hidzuke_From.NullValue = "";
            this.txt_Hidzuke_From.PopupAfterExecute = null;
            this.txt_Hidzuke_From.PopupBeforeExecute = null;
            this.txt_Hidzuke_From.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Hidzuke_From.PopupSearchSendParams")));
            this.txt_Hidzuke_From.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Hidzuke_From.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Hidzuke_From.popupWindowSetting")));
            this.txt_Hidzuke_From.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Hidzuke_From.RegistCheckMethod")));
            this.txt_Hidzuke_From.Size = new System.Drawing.Size(115, 20);
            this.txt_Hidzuke_From.TabIndex = 6;
            this.txt_Hidzuke_From.Tag = "日付を選択してください";
            this.txt_Hidzuke_From.Text = "2013/11/12(火)";
            this.txt_Hidzuke_From.Value = new System.DateTime(2013, 11, 12, 0, 0, 0, 0);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(847, 2);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 20);
            this.label5.TabIndex = 461;
            this.label5.Text = "～";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.txt_Hidzuke_To);
            this.Controls.Add(this.txt_Hidzuke_From);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_Hidzuke);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cantxt_Jyoutai);
            this.Controls.Add(this.cantxt_ManiFestTo);
            this.Controls.Add(this.cantxt_ManiFestFrom);
            this.Controls.Add(this.pnl_ManiType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cdgrid_tuusinnrireki);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.cdgrid_tuusinnrireki)).EndInit();
            this.pnl_ManiType.ResumeLayout(false);
            this.pnl_ManiType.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.CustomDataGridView cdgrid_tuusinnrireki;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private r_framework.CustomControl.CustomPanel pnl_ManiType;
        public r_framework.CustomControl.CustomNumericTextBox2 cantxt_Jyoutai;
        private r_framework.CustomControl.CustomRadioButton crdo_JWNETEraa;
        private r_framework.CustomControl.CustomRadioButton crdo_JWNETSousinnKannryou;
        private r_framework.CustomControl.CustomRadioButton crdo_SousinnMati;
        private r_framework.CustomControl.CustomRadioButton crdo_SousinnHoryuu;
        private r_framework.CustomControl.CustomRadioButton crdo_ZebuHyouji;
        private r_framework.CustomControl.CustomRadioButton crdo_SousinnKannryou;
        private r_framework.CustomControl.CustomRadioButton crdo_SousinnSippai;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewButtonColumn dataGridViewButtonColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        public r_framework.CustomControl.CustomNumericTextBox2 cantxt_ManiFestFrom;
        public r_framework.CustomControl.CustomNumericTextBox2 cantxt_ManiFestTo;
        private System.Windows.Forms.DataGridViewButtonColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column11;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KANRI_ID;
        private r_framework.CustomControl.DgvCustomTextBoxColumn QUE_SEQ;
        private System.Windows.Forms.Label label4;
        private r_framework.CustomControl.CustomPanel panel1;
        public r_framework.CustomControl.CustomNumericTextBox2 txt_Hidzuke;
        internal r_framework.CustomControl.CustomRadioButton cb_Koushinbi;
        internal r_framework.CustomControl.CustomRadioButton cb_Tourokubi;
        internal r_framework.CustomControl.CustomDateTimePicker txt_Hidzuke_To;
        internal r_framework.CustomControl.CustomDateTimePicker txt_Hidzuke_From;
        private System.Windows.Forms.Label label5;
    }
}