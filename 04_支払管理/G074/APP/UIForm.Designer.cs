namespace Shougun.Core.PaymentManagement.KaikakekinItiranHyo
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto6 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            this.KaikakekinItiranHyo = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.col_TorihikisakiCD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.col_TorihikisakiName = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.col_KurikosiZandaka = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_ShukkinGaku = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_ZeinukiShiharai = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_Shohizei = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_ZeikomiShiharai = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_SashihikiShiharaiZandaka = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_Shimebi1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.col_Shimebi2 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.col_Shimebi3 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.KaikakekinItiranHyo)).BeginInit();
            this.SuspendLayout();
            // 
            // KaikakekinItiranHyo
            // 
            this.KaikakekinItiranHyo.AllowUserToAddRows = false;
            this.KaikakekinItiranHyo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.KaikakekinItiranHyo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.KaikakekinItiranHyo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.KaikakekinItiranHyo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_TorihikisakiCD,
            this.col_TorihikisakiName,
            this.col_KurikosiZandaka,
            this.col_ShukkinGaku,
            this.col_ZeinukiShiharai,
            this.col_Shohizei,
            this.col_ZeikomiShiharai,
            this.col_SashihikiShiharaiZandaka,
            this.col_Shimebi1,
            this.col_Shimebi2,
            this.col_Shimebi3});
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.KaikakekinItiranHyo.DefaultCellStyle = dataGridViewCellStyle13;
            this.KaikakekinItiranHyo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.KaikakekinItiranHyo.EnableHeadersVisualStyles = false;
            this.KaikakekinItiranHyo.GridColor = System.Drawing.Color.White;
            this.KaikakekinItiranHyo.IsReload = false;
            this.KaikakekinItiranHyo.LinkedDataPanelName = null;
            this.KaikakekinItiranHyo.Location = new System.Drawing.Point(1, 1);
            this.KaikakekinItiranHyo.MultiSelect = false;
            this.KaikakekinItiranHyo.Name = "KaikakekinItiranHyo";
            this.KaikakekinItiranHyo.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.KaikakekinItiranHyo.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.KaikakekinItiranHyo.RowHeadersVisible = false;
            this.KaikakekinItiranHyo.RowTemplate.Height = 21;
            this.KaikakekinItiranHyo.ShowCellToolTips = false;
            this.KaikakekinItiranHyo.Size = new System.Drawing.Size(987, 448);
            this.KaikakekinItiranHyo.TabIndex = 0;
            this.KaikakekinItiranHyo.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.KaikakekinItiranHyo_SortCompare);
            // 
            // col_TorihikisakiCD
            // 
            this.col_TorihikisakiCD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.col_TorihikisakiCD.DefaultCellStyle = dataGridViewCellStyle2;
            this.col_TorihikisakiCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_TorihikisakiCD.FocusOutCheckMethod")));
            this.col_TorihikisakiCD.HeaderText = "取引先CD";
            this.col_TorihikisakiCD.Name = "col_TorihikisakiCD";
            this.col_TorihikisakiCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_TorihikisakiCD.PopupSearchSendParams")));
            this.col_TorihikisakiCD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_TorihikisakiCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_TorihikisakiCD.popupWindowSetting")));
            this.col_TorihikisakiCD.ReadOnly = true;
            this.col_TorihikisakiCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_TorihikisakiCD.RegistCheckMethod")));
            this.col_TorihikisakiCD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_TorihikisakiName
            // 
            this.col_TorihikisakiName.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.col_TorihikisakiName.DefaultCellStyle = dataGridViewCellStyle3;
            this.col_TorihikisakiName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_TorihikisakiName.FocusOutCheckMethod")));
            this.col_TorihikisakiName.HeaderText = "取引先名";
            this.col_TorihikisakiName.Name = "col_TorihikisakiName";
            this.col_TorihikisakiName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_TorihikisakiName.PopupSearchSendParams")));
            this.col_TorihikisakiName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_TorihikisakiName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_TorihikisakiName.popupWindowSetting")));
            this.col_TorihikisakiName.ReadOnly = true;
            this.col_TorihikisakiName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_TorihikisakiName.RegistCheckMethod")));
            this.col_TorihikisakiName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_KurikosiZandaka
            // 
            this.col_KurikosiZandaka.CustomFormatSetting = "#,##0";
            this.col_KurikosiZandaka.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.col_KurikosiZandaka.DefaultCellStyle = dataGridViewCellStyle4;
            this.col_KurikosiZandaka.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_KurikosiZandaka.FocusOutCheckMethod")));
            this.col_KurikosiZandaka.FormatSetting = "カスタム";
            this.col_KurikosiZandaka.HeaderText = "繰越残高";
            this.col_KurikosiZandaka.Name = "col_KurikosiZandaka";
            this.col_KurikosiZandaka.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_KurikosiZandaka.PopupSearchSendParams")));
            this.col_KurikosiZandaka.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_KurikosiZandaka.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_KurikosiZandaka.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            276447231,
            23283,
            0,
            0});
            this.col_KurikosiZandaka.RangeSetting = rangeSettingDto1;
            this.col_KurikosiZandaka.ReadOnly = true;
            this.col_KurikosiZandaka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_KurikosiZandaka.RegistCheckMethod")));
            this.col_KurikosiZandaka.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_ShukkinGaku
            // 
            this.col_ShukkinGaku.CustomFormatSetting = "#,##0";
            this.col_ShukkinGaku.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.col_ShukkinGaku.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_ShukkinGaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_ShukkinGaku.FocusOutCheckMethod")));
            this.col_ShukkinGaku.FormatSetting = "カスタム";
            this.col_ShukkinGaku.HeaderText = "出金額";
            this.col_ShukkinGaku.Name = "col_ShukkinGaku";
            this.col_ShukkinGaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_ShukkinGaku.PopupSearchSendParams")));
            this.col_ShukkinGaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_ShukkinGaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_ShukkinGaku.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.col_ShukkinGaku.RangeSetting = rangeSettingDto2;
            this.col_ShukkinGaku.ReadOnly = true;
            this.col_ShukkinGaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_ShukkinGaku.RegistCheckMethod")));
            this.col_ShukkinGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_ZeinukiShiharai
            // 
            this.col_ZeinukiShiharai.CustomFormatSetting = "#,##0";
            this.col_ZeinukiShiharai.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.col_ZeinukiShiharai.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_ZeinukiShiharai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_ZeinukiShiharai.FocusOutCheckMethod")));
            this.col_ZeinukiShiharai.FormatSetting = "カスタム";
            this.col_ZeinukiShiharai.HeaderText = "税抜支払金額";
            this.col_ZeinukiShiharai.Name = "col_ZeinukiShiharai";
            this.col_ZeinukiShiharai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_ZeinukiShiharai.PopupSearchSendParams")));
            this.col_ZeinukiShiharai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_ZeinukiShiharai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_ZeinukiShiharai.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.col_ZeinukiShiharai.RangeSetting = rangeSettingDto3;
            this.col_ZeinukiShiharai.ReadOnly = true;
            this.col_ZeinukiShiharai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_ZeinukiShiharai.RegistCheckMethod")));
            this.col_ZeinukiShiharai.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_Shohizei
            // 
            this.col_Shohizei.CustomFormatSetting = "#,##0";
            this.col_Shohizei.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N0";
            dataGridViewCellStyle7.NullValue = null;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.col_Shohizei.DefaultCellStyle = dataGridViewCellStyle7;
            this.col_Shohizei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_Shohizei.FocusOutCheckMethod")));
            this.col_Shohizei.FormatSetting = "カスタム";
            this.col_Shohizei.HeaderText = "消費税";
            this.col_Shohizei.Name = "col_Shohizei";
            this.col_Shohizei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_Shohizei.PopupSearchSendParams")));
            this.col_Shohizei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_Shohizei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_Shohizei.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.col_Shohizei.RangeSetting = rangeSettingDto4;
            this.col_Shohizei.ReadOnly = true;
            this.col_Shohizei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_Shohizei.RegistCheckMethod")));
            this.col_Shohizei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_ZeikomiShiharai
            // 
            this.col_ZeikomiShiharai.CustomFormatSetting = "#,##0";
            this.col_ZeikomiShiharai.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.col_ZeikomiShiharai.DefaultCellStyle = dataGridViewCellStyle8;
            this.col_ZeikomiShiharai.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_ZeikomiShiharai.FocusOutCheckMethod")));
            this.col_ZeikomiShiharai.FormatSetting = "カスタム";
            this.col_ZeikomiShiharai.HeaderText = "税込支払金額";
            this.col_ZeikomiShiharai.Name = "col_ZeikomiShiharai";
            this.col_ZeikomiShiharai.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_ZeikomiShiharai.PopupSearchSendParams")));
            this.col_ZeikomiShiharai.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_ZeikomiShiharai.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_ZeikomiShiharai.popupWindowSetting")));
            rangeSettingDto5.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.col_ZeikomiShiharai.RangeSetting = rangeSettingDto5;
            this.col_ZeikomiShiharai.ReadOnly = true;
            this.col_ZeikomiShiharai.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_ZeikomiShiharai.RegistCheckMethod")));
            this.col_ZeikomiShiharai.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_SashihikiShiharaiZandaka
            // 
            this.col_SashihikiShiharaiZandaka.CustomFormatSetting = "#,##0";
            this.col_SashihikiShiharaiZandaka.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N0";
            dataGridViewCellStyle9.NullValue = null;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.col_SashihikiShiharaiZandaka.DefaultCellStyle = dataGridViewCellStyle9;
            this.col_SashihikiShiharaiZandaka.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_SashihikiShiharaiZandaka.FocusOutCheckMethod")));
            this.col_SashihikiShiharaiZandaka.FormatSetting = "カスタム";
            this.col_SashihikiShiharaiZandaka.HeaderText = "差引残高";
            this.col_SashihikiShiharaiZandaka.Name = "col_SashihikiShiharaiZandaka";
            this.col_SashihikiShiharaiZandaka.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_SashihikiShiharaiZandaka.PopupSearchSendParams")));
            this.col_SashihikiShiharaiZandaka.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_SashihikiShiharaiZandaka.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_SashihikiShiharaiZandaka.popupWindowSetting")));
            rangeSettingDto6.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.col_SashihikiShiharaiZandaka.RangeSetting = rangeSettingDto6;
            this.col_SashihikiShiharaiZandaka.ReadOnly = true;
            this.col_SashihikiShiharaiZandaka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_SashihikiShiharaiZandaka.RegistCheckMethod")));
            this.col_SashihikiShiharaiZandaka.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_Shimebi1
            // 
            this.col_Shimebi1.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.col_Shimebi1.DefaultCellStyle = dataGridViewCellStyle10;
            this.col_Shimebi1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_Shimebi1.FocusOutCheckMethod")));
            this.col_Shimebi1.HeaderText = "締日1";
            this.col_Shimebi1.Name = "col_Shimebi1";
            this.col_Shimebi1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_Shimebi1.PopupSearchSendParams")));
            this.col_Shimebi1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_Shimebi1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_Shimebi1.popupWindowSetting")));
            this.col_Shimebi1.ReadOnly = true;
            this.col_Shimebi1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_Shimebi1.RegistCheckMethod")));
            this.col_Shimebi1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_Shimebi2
            // 
            this.col_Shimebi2.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.col_Shimebi2.DefaultCellStyle = dataGridViewCellStyle11;
            this.col_Shimebi2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_Shimebi2.FocusOutCheckMethod")));
            this.col_Shimebi2.HeaderText = "締日2";
            this.col_Shimebi2.Name = "col_Shimebi2";
            this.col_Shimebi2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_Shimebi2.PopupSearchSendParams")));
            this.col_Shimebi2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_Shimebi2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_Shimebi2.popupWindowSetting")));
            this.col_Shimebi2.ReadOnly = true;
            this.col_Shimebi2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_Shimebi2.RegistCheckMethod")));
            this.col_Shimebi2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_Shimebi3
            // 
            this.col_Shimebi3.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.col_Shimebi3.DefaultCellStyle = dataGridViewCellStyle12;
            this.col_Shimebi3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_Shimebi3.FocusOutCheckMethod")));
            this.col_Shimebi3.HeaderText = "締日3";
            this.col_Shimebi3.Name = "col_Shimebi3";
            this.col_Shimebi3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_Shimebi3.PopupSearchSendParams")));
            this.col_Shimebi3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_Shimebi3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_Shimebi3.popupWindowSetting")));
            this.col_Shimebi3.ReadOnly = true;
            this.col_Shimebi3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_Shimebi3.RegistCheckMethod")));
            this.col_Shimebi3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 461);
            this.Controls.Add(this.KaikakekinItiranHyo);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.KaikakekinItiranHyo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public r_framework.CustomControl.CustomDataGridView KaikakekinItiranHyo;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn col_TorihikisakiCD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn col_TorihikisakiName;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_KurikosiZandaka;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_ShukkinGaku;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_ZeinukiShiharai;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_Shohizei;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_ZeikomiShiharai;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_SashihikiShiharaiZandaka;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn col_Shimebi1;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn col_Shimebi2;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn col_Shimebi3;

    }
}