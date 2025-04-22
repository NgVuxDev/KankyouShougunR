namespace Shougun.Core.SalesManagement.UrikakekinItiranHyo
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
            this.UrikakekinItiranHyo = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.col_TorihikisakiCD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.col_TorihikisakiName = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.col_KurikosiZandaka = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_NyukinGaku = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_ZeinukiUriage = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_Shohizei = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_ZeikomiUriage = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_SashihikiUriageZandaka = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.col_Shimebi1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.col_Shimebi2 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.col_Shimebi3 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.UrikakekinItiranHyo)).BeginInit();
            this.SuspendLayout();
            // 
            // UrikakekinItiranHyo
            // 
            this.UrikakekinItiranHyo.AllowUserToAddRows = false;
            this.UrikakekinItiranHyo.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UrikakekinItiranHyo.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.UrikakekinItiranHyo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.UrikakekinItiranHyo.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_TorihikisakiCD,
            this.col_TorihikisakiName,
            this.col_KurikosiZandaka,
            this.col_NyukinGaku,
            this.col_ZeinukiUriage,
            this.col_Shohizei,
            this.col_ZeikomiUriage,
            this.col_SashihikiUriageZandaka,
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
            this.UrikakekinItiranHyo.DefaultCellStyle = dataGridViewCellStyle13;
            this.UrikakekinItiranHyo.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.UrikakekinItiranHyo.EnableHeadersVisualStyles = false;
            this.UrikakekinItiranHyo.GridColor = System.Drawing.Color.White;
            this.UrikakekinItiranHyo.IsReload = false;
            this.UrikakekinItiranHyo.LinkedDataPanelName = null;
            this.UrikakekinItiranHyo.Location = new System.Drawing.Point(1, 1);
            this.UrikakekinItiranHyo.MultiSelect = false;
            this.UrikakekinItiranHyo.Name = "UrikakekinItiranHyo";
            this.UrikakekinItiranHyo.ReadOnly = true;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.UrikakekinItiranHyo.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.UrikakekinItiranHyo.RowHeadersVisible = false;
            this.UrikakekinItiranHyo.RowTemplate.Height = 21;
            this.UrikakekinItiranHyo.ShowCellToolTips = false;
            this.UrikakekinItiranHyo.Size = new System.Drawing.Size(987, 448);
            this.UrikakekinItiranHyo.TabIndex = 0;
            this.UrikakekinItiranHyo.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.UrikakekinItiranHyo_SortCompare);
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
            0,
            0,
            0,
            0});
            this.col_KurikosiZandaka.RangeSetting = rangeSettingDto1;
            this.col_KurikosiZandaka.ReadOnly = true;
            this.col_KurikosiZandaka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_KurikosiZandaka.RegistCheckMethod")));
            this.col_KurikosiZandaka.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_NyukinGaku
            // 
            this.col_NyukinGaku.CustomFormatSetting = "#,##0";
            this.col_NyukinGaku.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.col_NyukinGaku.DefaultCellStyle = dataGridViewCellStyle5;
            this.col_NyukinGaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_NyukinGaku.FocusOutCheckMethod")));
            this.col_NyukinGaku.FormatSetting = "カスタム";
            this.col_NyukinGaku.HeaderText = "入金額";
            this.col_NyukinGaku.Name = "col_NyukinGaku";
            this.col_NyukinGaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_NyukinGaku.PopupSearchSendParams")));
            this.col_NyukinGaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_NyukinGaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_NyukinGaku.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.col_NyukinGaku.RangeSetting = rangeSettingDto2;
            this.col_NyukinGaku.ReadOnly = true;
            this.col_NyukinGaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_NyukinGaku.RegistCheckMethod")));
            this.col_NyukinGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_ZeinukiUriage
            // 
            this.col_ZeinukiUriage.CustomFormatSetting = "#,##0";
            this.col_ZeinukiUriage.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.col_ZeinukiUriage.DefaultCellStyle = dataGridViewCellStyle6;
            this.col_ZeinukiUriage.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_ZeinukiUriage.FocusOutCheckMethod")));
            this.col_ZeinukiUriage.FormatSetting = "カスタム";
            this.col_ZeinukiUriage.HeaderText = "税抜売上金額";
            this.col_ZeinukiUriage.Name = "col_ZeinukiUriage";
            this.col_ZeinukiUriage.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_ZeinukiUriage.PopupSearchSendParams")));
            this.col_ZeinukiUriage.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_ZeinukiUriage.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_ZeinukiUriage.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.col_ZeinukiUriage.RangeSetting = rangeSettingDto3;
            this.col_ZeinukiUriage.ReadOnly = true;
            this.col_ZeinukiUriage.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_ZeinukiUriage.RegistCheckMethod")));
            this.col_ZeinukiUriage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            // col_ZeikomiUriage
            // 
            this.col_ZeikomiUriage.CustomFormatSetting = "#,##0";
            this.col_ZeikomiUriage.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N0";
            dataGridViewCellStyle8.NullValue = null;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.col_ZeikomiUriage.DefaultCellStyle = dataGridViewCellStyle8;
            this.col_ZeikomiUriage.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_ZeikomiUriage.FocusOutCheckMethod")));
            this.col_ZeikomiUriage.FormatSetting = "カスタム";
            this.col_ZeikomiUriage.HeaderText = "税込売上金額";
            this.col_ZeikomiUriage.Name = "col_ZeikomiUriage";
            this.col_ZeikomiUriage.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_ZeikomiUriage.PopupSearchSendParams")));
            this.col_ZeikomiUriage.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_ZeikomiUriage.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_ZeikomiUriage.popupWindowSetting")));
            rangeSettingDto5.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.col_ZeikomiUriage.RangeSetting = rangeSettingDto5;
            this.col_ZeikomiUriage.ReadOnly = true;
            this.col_ZeikomiUriage.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_ZeikomiUriage.RegistCheckMethod")));
            this.col_ZeikomiUriage.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // col_SashihikiUriageZandaka
            // 
            this.col_SashihikiUriageZandaka.CustomFormatSetting = "#,##0";
            this.col_SashihikiUriageZandaka.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N0";
            dataGridViewCellStyle9.NullValue = null;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.col_SashihikiUriageZandaka.DefaultCellStyle = dataGridViewCellStyle9;
            this.col_SashihikiUriageZandaka.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_SashihikiUriageZandaka.FocusOutCheckMethod")));
            this.col_SashihikiUriageZandaka.FormatSetting = "カスタム";
            this.col_SashihikiUriageZandaka.HeaderText = "差引残高";
            this.col_SashihikiUriageZandaka.Name = "col_SashihikiUriageZandaka";
            this.col_SashihikiUriageZandaka.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("col_SashihikiUriageZandaka.PopupSearchSendParams")));
            this.col_SashihikiUriageZandaka.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.col_SashihikiUriageZandaka.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("col_SashihikiUriageZandaka.popupWindowSetting")));
            rangeSettingDto6.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.col_SashihikiUriageZandaka.RangeSetting = rangeSettingDto6;
            this.col_SashihikiUriageZandaka.ReadOnly = true;
            this.col_SashihikiUriageZandaka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("col_SashihikiUriageZandaka.RegistCheckMethod")));
            this.col_SashihikiUriageZandaka.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
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
            this.Controls.Add(this.UrikakekinItiranHyo);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.UrikakekinItiranHyo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public r_framework.CustomControl.CustomDataGridView UrikakekinItiranHyo;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn col_TorihikisakiCD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn col_TorihikisakiName;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_KurikosiZandaka;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_NyukinGaku;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_ZeinukiUriage;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_Shohizei;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_ZeikomiUriage;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column col_SashihikiUriageZandaka;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn col_Shimebi1;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn col_Shimebi2;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn col_Shimebi3;

    }
}