namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomiShusei
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvNyukinDeleteMeisai = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.DELETE_FLG = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.NYUUKIN_NUMBER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GYOUSHA_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GENBA_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SEIKYUU_NUMBER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SEIKYUU_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SEIKYUUGAKU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.KeshikomiGaku = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.keshikomiGakuTotal = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.MiKeshikomiGaku = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.maeKeshikomiGakuTotal = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.KESHIKOMI_BIKOU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SYSTEM_ID = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.KESHIKOMI_SEQ = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.KAGAMI_NUMBER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.ROW_NUMBER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SEIKYUU_DATE_TO = new r_framework.CustomControl.CustomDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.SEIKYUU_DATE_FROM = new r_framework.CustomControl.CustomDateTimePicker();
            this.lblSeikyuubi = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lblTORIHIKISAKI = new System.Windows.Forms.Label();
            this.TORIHIKISAKI_FROM_POPUP = new r_framework.CustomControl.CustomPopupOpenButton();
            this.label1 = new System.Windows.Forms.Label();
            this.Zandaka = new r_framework.CustomControl.CustomCheckBox();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNyukinDeleteMeisai)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNyukinDeleteMeisai
            // 
            this.dgvNyukinDeleteMeisai.AllowUserToAddRows = false;
            this.dgvNyukinDeleteMeisai.AllowUserToDeleteRows = false;
            this.dgvNyukinDeleteMeisai.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNyukinDeleteMeisai.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNyukinDeleteMeisai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNyukinDeleteMeisai.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DELETE_FLG,
            this.NYUUKIN_NUMBER,
            this.GYOUSHA_CD,
            this.GYOUSHA_NAME_RYAKU,
            this.GENBA_CD,
            this.GENBA_NAME_RYAKU,
            this.SEIKYUU_NUMBER,
            this.SEIKYUU_DATE,
            this.SEIKYUUGAKU,
            this.KeshikomiGaku,
            this.keshikomiGakuTotal,
            this.MiKeshikomiGaku,
            this.maeKeshikomiGakuTotal,
            this.KESHIKOMI_BIKOU,
            this.SYSTEM_ID,
            this.KESHIKOMI_SEQ,
            this.KAGAMI_NUMBER,
            this.ROW_NUMBER});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNyukinDeleteMeisai.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvNyukinDeleteMeisai.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvNyukinDeleteMeisai.EnableHeadersVisualStyles = false;
            this.dgvNyukinDeleteMeisai.GridColor = System.Drawing.Color.White;
            this.dgvNyukinDeleteMeisai.IsReload = false;
            this.dgvNyukinDeleteMeisai.LinkedDataPanelName = null;
            this.dgvNyukinDeleteMeisai.Location = new System.Drawing.Point(5, 94);
            this.dgvNyukinDeleteMeisai.MultiSelect = false;
            this.dgvNyukinDeleteMeisai.Name = "dgvNyukinDeleteMeisai";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle21.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNyukinDeleteMeisai.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.dgvNyukinDeleteMeisai.RowHeadersVisible = false;
            this.dgvNyukinDeleteMeisai.RowTemplate.Height = 21;
            this.dgvNyukinDeleteMeisai.ShowCellToolTips = false;
            this.dgvNyukinDeleteMeisai.Size = new System.Drawing.Size(986, 371);
            this.dgvNyukinDeleteMeisai.TabIndex = 504;
            this.dgvNyukinDeleteMeisai.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNyukinDeleteMeisai_CellEnter);
            this.dgvNyukinDeleteMeisai.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNyukinDeleteMeisai_CellValidated);
            this.dgvNyukinDeleteMeisai.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvNyukinDeleteMeisai_CellValidating);
            this.dgvNyukinDeleteMeisai.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvNyukinDeleteMeisai_EditingControlShowing);
            // 
            // DELETE_FLG
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.DELETE_FLG.DefaultCellStyle = dataGridViewCellStyle2;
            this.DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.FocusOutCheckMethod")));
            this.DELETE_FLG.HeaderText = "削除";
            this.DELETE_FLG.Name = "DELETE_FLG";
            this.DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.RegistCheckMethod")));
            this.DELETE_FLG.Width = 50;
            // 
            // NYUUKIN_NUMBER
            // 
            this.NYUUKIN_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.NYUUKIN_NUMBER.DefaultCellStyle = dataGridViewCellStyle3;
            this.NYUUKIN_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKIN_NUMBER.FocusOutCheckMethod")));
            this.NYUUKIN_NUMBER.HeaderText = "入金番号";
            this.NYUUKIN_NUMBER.Name = "NYUUKIN_NUMBER";
            this.NYUUKIN_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKIN_NUMBER.PopupSearchSendParams")));
            this.NYUUKIN_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKIN_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKIN_NUMBER.popupWindowSetting")));
            this.NYUUKIN_NUMBER.ReadOnly = true;
            this.NYUUKIN_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKIN_NUMBER.RegistCheckMethod")));
            this.NYUUKIN_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NYUUKIN_NUMBER.Width = 70;
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.DefaultCellStyle = dataGridViewCellStyle4;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.GetCodeMasterField = "";
            this.GYOUSHA_CD.HeaderText = "業者CD";
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.ReadOnly = true;
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GYOUSHA_CD.Width = 62;
            // 
            // GYOUSHA_NAME_RYAKU
            // 
            this.GYOUSHA_NAME_RYAKU.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle5;
            this.GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.HeaderText = "業者名";
            this.GYOUSHA_NAME_RYAKU.Name = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GYOUSHA_NAME_RYAKU.Width = 150;
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.DBFieldsName = "GENBA_CD";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.DefaultCellStyle = dataGridViewCellStyle6;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.HeaderText = "現場CD";
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.ReadOnly = true;
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GENBA_CD.Width = 62;
            // 
            // GENBA_NAME_RYAKU
            // 
            this.GENBA_NAME_RYAKU.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle7;
            this.GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU.HeaderText = "現場名";
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.ReadOnly = true;
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GENBA_NAME_RYAKU.Width = 150;
            // 
            // SEIKYUU_NUMBER
            // 
            this.SEIKYUU_NUMBER.DBFieldsName = "SEIKYUU_NUMBER";
            this.SEIKYUU_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.SEIKYUU_NUMBER.DefaultCellStyle = dataGridViewCellStyle8;
            this.SEIKYUU_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_NUMBER.FocusOutCheckMethod")));
            this.SEIKYUU_NUMBER.HeaderText = "請求番号";
            this.SEIKYUU_NUMBER.Name = "SEIKYUU_NUMBER";
            this.SEIKYUU_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUU_NUMBER.PopupSearchSendParams")));
            this.SEIKYUU_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUU_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUU_NUMBER.popupWindowSetting")));
            this.SEIKYUU_NUMBER.ReadOnly = true;
            this.SEIKYUU_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_NUMBER.RegistCheckMethod")));
            this.SEIKYUU_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SEIKYUU_NUMBER.Width = 70;
            // 
            // SEIKYUU_DATE
            // 
            this.SEIKYUU_DATE.DBFieldsName = "SEIKYUU_DATE";
            this.SEIKYUU_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.SEIKYUU_DATE.DefaultCellStyle = dataGridViewCellStyle9;
            this.SEIKYUU_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DATE.FocusOutCheckMethod")));
            this.SEIKYUU_DATE.HeaderText = "請求日付";
            this.SEIKYUU_DATE.Name = "SEIKYUU_DATE";
            this.SEIKYUU_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUU_DATE.PopupSearchSendParams")));
            this.SEIKYUU_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUU_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUU_DATE.popupWindowSetting")));
            this.SEIKYUU_DATE.ReadOnly = true;
            this.SEIKYUU_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DATE.RegistCheckMethod")));
            this.SEIKYUU_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SEIKYUU_DATE.Width = 110;
            // 
            // SEIKYUUGAKU
            // 
            this.SEIKYUUGAKU.CustomFormatSetting = "#,##0";
            this.SEIKYUUGAKU.DBFieldsName = "SEIKYUUGAKU";
            this.SEIKYUUGAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.SEIKYUUGAKU.DefaultCellStyle = dataGridViewCellStyle10;
            this.SEIKYUUGAKU.DisplayItemName = "";
            this.SEIKYUUGAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUUGAKU.FocusOutCheckMethod")));
            this.SEIKYUUGAKU.FormatSetting = "カスタム";
            this.SEIKYUUGAKU.HeaderText = "請求額";
            this.SEIKYUUGAKU.Name = "SEIKYUUGAKU";
            this.SEIKYUUGAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUUGAKU.PopupSearchSendParams")));
            this.SEIKYUUGAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUUGAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUUGAKU.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.SEIKYUUGAKU.RangeSetting = rangeSettingDto1;
            this.SEIKYUUGAKU.ReadOnly = true;
            this.SEIKYUUGAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUUGAKU.RegistCheckMethod")));
            this.SEIKYUUGAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SEIKYUUGAKU.Width = 95;
            // 
            // KeshikomiGaku
            // 
            this.KeshikomiGaku.CustomFormatSetting = "#,##0";
            this.KeshikomiGaku.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.KeshikomiGaku.DefaultCellStyle = dataGridViewCellStyle11;
            this.KeshikomiGaku.DisplayItemName = "";
            this.KeshikomiGaku.FocusOutCheckMethod = null;
            this.KeshikomiGaku.FormatSetting = "カスタム";
            this.KeshikomiGaku.HeaderText = "消込額";
            this.KeshikomiGaku.Name = "KeshikomiGaku";
            this.KeshikomiGaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KeshikomiGaku.PopupSearchSendParams")));
            this.KeshikomiGaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KeshikomiGaku.popupWindowSetting = null;
            rangeSettingDto2.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.KeshikomiGaku.RangeSetting = rangeSettingDto2;
            this.KeshikomiGaku.RegistCheckMethod = null;
            this.KeshikomiGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KeshikomiGaku.ToolTipText = "半角9桁以内で入力してください";
            this.KeshikomiGaku.Width = 95;
            // 
            // keshikomiGakuTotal
            // 
            this.keshikomiGakuTotal.CustomFormatSetting = "#,##0";
            this.keshikomiGakuTotal.DBFieldsName = "keshikomiGakuTotal";
            this.keshikomiGakuTotal.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.keshikomiGakuTotal.DefaultCellStyle = dataGridViewCellStyle12;
            this.keshikomiGakuTotal.DisplayItemName = "総消込額";
            this.keshikomiGakuTotal.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("keshikomiGakuTotal.FocusOutCheckMethod")));
            this.keshikomiGakuTotal.FormatSetting = "カスタム";
            this.keshikomiGakuTotal.HeaderText = "総消込額";
            this.keshikomiGakuTotal.Name = "keshikomiGakuTotal";
            this.keshikomiGakuTotal.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("keshikomiGakuTotal.PopupSearchSendParams")));
            this.keshikomiGakuTotal.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.keshikomiGakuTotal.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("keshikomiGakuTotal.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.keshikomiGakuTotal.RangeSetting = rangeSettingDto3;
            this.keshikomiGakuTotal.ReadOnly = true;
            this.keshikomiGakuTotal.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("keshikomiGakuTotal.RegistCheckMethod")));
            this.keshikomiGakuTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.keshikomiGakuTotal.Width = 95;
            // 
            // MiKeshikomiGaku
            // 
            this.MiKeshikomiGaku.CustomFormatSetting = "#,##0";
            this.MiKeshikomiGaku.DBFieldsName = "MiKeshikomiGaku";
            this.MiKeshikomiGaku.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.MiKeshikomiGaku.DefaultCellStyle = dataGridViewCellStyle13;
            this.MiKeshikomiGaku.DisplayItemName = "";
            this.MiKeshikomiGaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MiKeshikomiGaku.FocusOutCheckMethod")));
            this.MiKeshikomiGaku.FormatSetting = "カスタム";
            this.MiKeshikomiGaku.HeaderText = "未消込額";
            this.MiKeshikomiGaku.Name = "MiKeshikomiGaku";
            this.MiKeshikomiGaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MiKeshikomiGaku.PopupSearchSendParams")));
            this.MiKeshikomiGaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MiKeshikomiGaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MiKeshikomiGaku.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.MiKeshikomiGaku.RangeSetting = rangeSettingDto4;
            this.MiKeshikomiGaku.ReadOnly = true;
            this.MiKeshikomiGaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MiKeshikomiGaku.RegistCheckMethod")));
            this.MiKeshikomiGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MiKeshikomiGaku.Width = 95;
            // 
            // maeKeshikomiGakuTotal
            // 
            this.maeKeshikomiGakuTotal.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.maeKeshikomiGakuTotal.DefaultCellStyle = dataGridViewCellStyle14;
            this.maeKeshikomiGakuTotal.DisplayItemName = "";
            this.maeKeshikomiGakuTotal.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("maeKeshikomiGakuTotal.FocusOutCheckMethod")));
            this.maeKeshikomiGakuTotal.HeaderText = "maeKeshikomiGakuTotal";
            this.maeKeshikomiGakuTotal.Name = "maeKeshikomiGakuTotal";
            this.maeKeshikomiGakuTotal.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("maeKeshikomiGakuTotal.PopupSearchSendParams")));
            this.maeKeshikomiGakuTotal.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.maeKeshikomiGakuTotal.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("maeKeshikomiGakuTotal.popupWindowSetting")));
            rangeSettingDto5.Max = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.maeKeshikomiGakuTotal.RangeSetting = rangeSettingDto5;
            this.maeKeshikomiGakuTotal.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("maeKeshikomiGakuTotal.RegistCheckMethod")));
            this.maeKeshikomiGakuTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.maeKeshikomiGakuTotal.Visible = false;
            // 
            // KESHIKOMI_BIKOU
            // 
            this.KESHIKOMI_BIKOU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.KESHIKOMI_BIKOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.KESHIKOMI_BIKOU.DefaultCellStyle = dataGridViewCellStyle15;
            this.KESHIKOMI_BIKOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KESHIKOMI_BIKOU.FocusOutCheckMethod")));
            this.KESHIKOMI_BIKOU.HeaderText = "消込備考";
            this.KESHIKOMI_BIKOU.MaxInputLength = 40;
            this.KESHIKOMI_BIKOU.Name = "KESHIKOMI_BIKOU";
            this.KESHIKOMI_BIKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KESHIKOMI_BIKOU.PopupSearchSendParams")));
            this.KESHIKOMI_BIKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KESHIKOMI_BIKOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KESHIKOMI_BIKOU.popupWindowSetting")));
            this.KESHIKOMI_BIKOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KESHIKOMI_BIKOU.RegistCheckMethod")));
            this.KESHIKOMI_BIKOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KESHIKOMI_BIKOU.ToolTipText = "全角20文字以内で入力してください";
            this.KESHIKOMI_BIKOU.Width = 290;
            // 
            // SYSTEM_ID
            // 
            this.SYSTEM_ID.DBFieldsName = "SYSTEM_ID";
            this.SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.SYSTEM_ID.DefaultCellStyle = dataGridViewCellStyle16;
            this.SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.FocusOutCheckMethod")));
            this.SYSTEM_ID.HeaderText = "SYSTEM_ID";
            this.SYSTEM_ID.Name = "SYSTEM_ID";
            this.SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SYSTEM_ID.PopupSearchSendParams")));
            this.SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SYSTEM_ID.popupWindowSetting")));
            this.SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.RegistCheckMethod")));
            this.SYSTEM_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SYSTEM_ID.Visible = false;
            // 
            // KESHIKOMI_SEQ
            // 
            this.KESHIKOMI_SEQ.DBFieldsName = "KESHIKOMI_SEQ";
            this.KESHIKOMI_SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            this.KESHIKOMI_SEQ.DefaultCellStyle = dataGridViewCellStyle17;
            this.KESHIKOMI_SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KESHIKOMI_SEQ.FocusOutCheckMethod")));
            this.KESHIKOMI_SEQ.HeaderText = "KESHIKOMI_SEQ";
            this.KESHIKOMI_SEQ.Name = "KESHIKOMI_SEQ";
            this.KESHIKOMI_SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KESHIKOMI_SEQ.PopupSearchSendParams")));
            this.KESHIKOMI_SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KESHIKOMI_SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KESHIKOMI_SEQ.popupWindowSetting")));
            this.KESHIKOMI_SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KESHIKOMI_SEQ.RegistCheckMethod")));
            this.KESHIKOMI_SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KESHIKOMI_SEQ.Visible = false;
            // 
            // KAGAMI_NUMBER
            // 
            this.KAGAMI_NUMBER.DBFieldsName = "KAGAMI_NUMBER";
            this.KAGAMI_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            this.KAGAMI_NUMBER.DefaultCellStyle = dataGridViewCellStyle18;
            this.KAGAMI_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAGAMI_NUMBER.FocusOutCheckMethod")));
            this.KAGAMI_NUMBER.HeaderText = "KAGAMI_NUMBER";
            this.KAGAMI_NUMBER.Name = "KAGAMI_NUMBER";
            this.KAGAMI_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KAGAMI_NUMBER.PopupSearchSendParams")));
            this.KAGAMI_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KAGAMI_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KAGAMI_NUMBER.popupWindowSetting")));
            this.KAGAMI_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAGAMI_NUMBER.RegistCheckMethod")));
            this.KAGAMI_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KAGAMI_NUMBER.Visible = false;
            // 
            // ROW_NUMBER
            // 
            this.ROW_NUMBER.DBFieldsName = "SORT_SEIKYUU_DATE";
            this.ROW_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black;
            this.ROW_NUMBER.DefaultCellStyle = dataGridViewCellStyle19;
            this.ROW_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ROW_NUMBER.FocusOutCheckMethod")));
            this.ROW_NUMBER.HeaderText = "ROW_NUMBER";
            this.ROW_NUMBER.Name = "ROW_NUMBER";
            this.ROW_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ROW_NUMBER.PopupSearchSendParams")));
            this.ROW_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ROW_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ROW_NUMBER.popupWindowSetting")));
            this.ROW_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ROW_NUMBER.RegistCheckMethod")));
            this.ROW_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ROW_NUMBER.Visible = false;
            // 
            // SEIKYUU_DATE_TO
            // 
            this.SEIKYUU_DATE_TO.BackColor = System.Drawing.SystemColors.Window;
            this.SEIKYUU_DATE_TO.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEIKYUU_DATE_TO.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.SEIKYUU_DATE_TO.Checked = false;
            this.SEIKYUU_DATE_TO.DateTimeNowYear = "";
            this.SEIKYUU_DATE_TO.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEIKYUU_DATE_TO.DisplayItemName = "請求日付To";
            this.SEIKYUU_DATE_TO.DisplayPopUp = null;
            this.SEIKYUU_DATE_TO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DATE_TO.FocusOutCheckMethod")));
            this.SEIKYUU_DATE_TO.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEIKYUU_DATE_TO.ForeColor = System.Drawing.Color.Black;
            this.SEIKYUU_DATE_TO.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.SEIKYUU_DATE_TO.IsInputErrorOccured = false;
            this.SEIKYUU_DATE_TO.Location = new System.Drawing.Point(266, 3);
            this.SEIKYUU_DATE_TO.MaxLength = 10;
            this.SEIKYUU_DATE_TO.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SEIKYUU_DATE_TO.Name = "SEIKYUU_DATE_TO";
            this.SEIKYUU_DATE_TO.NullValue = "";
            this.SEIKYUU_DATE_TO.PopupAfterExecute = null;
            this.SEIKYUU_DATE_TO.PopupBeforeExecute = null;
            this.SEIKYUU_DATE_TO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUU_DATE_TO.PopupSearchSendParams")));
            this.SEIKYUU_DATE_TO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUU_DATE_TO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUU_DATE_TO.popupWindowSetting")));
            this.SEIKYUU_DATE_TO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DATE_TO.RegistCheckMethod")));
            this.SEIKYUU_DATE_TO.Size = new System.Drawing.Size(110, 20);
            this.SEIKYUU_DATE_TO.TabIndex = 10008;
            this.SEIKYUU_DATE_TO.Tag = "";
            this.SEIKYUU_DATE_TO.Value = null;
            this.SEIKYUU_DATE_TO.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SEIKYUU_DATE_TO_MouseDoubleClick);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label2.Location = new System.Drawing.Point(244, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 20);
            this.label2.TabIndex = 10010;
            this.label2.Text = "～";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SEIKYUU_DATE_FROM
            // 
            this.SEIKYUU_DATE_FROM.BackColor = System.Drawing.SystemColors.Window;
            this.SEIKYUU_DATE_FROM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SEIKYUU_DATE_FROM.CalendarFont = new System.Drawing.Font("MS Gothic", 9F);
            this.SEIKYUU_DATE_FROM.Checked = false;
            this.SEIKYUU_DATE_FROM.DateTimeNowYear = "";
            this.SEIKYUU_DATE_FROM.DefaultBackColor = System.Drawing.Color.Empty;
            this.SEIKYUU_DATE_FROM.DisplayItemName = "請求日付From";
            this.SEIKYUU_DATE_FROM.DisplayPopUp = null;
            this.SEIKYUU_DATE_FROM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DATE_FROM.FocusOutCheckMethod")));
            this.SEIKYUU_DATE_FROM.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.SEIKYUU_DATE_FROM.ForeColor = System.Drawing.Color.Black;
            this.SEIKYUU_DATE_FROM.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.SEIKYUU_DATE_FROM.IsInputErrorOccured = false;
            this.SEIKYUU_DATE_FROM.Location = new System.Drawing.Point(127, 3);
            this.SEIKYUU_DATE_FROM.MaxLength = 10;
            this.SEIKYUU_DATE_FROM.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SEIKYUU_DATE_FROM.Name = "SEIKYUU_DATE_FROM";
            this.SEIKYUU_DATE_FROM.NullValue = "";
            this.SEIKYUU_DATE_FROM.PopupAfterExecute = null;
            this.SEIKYUU_DATE_FROM.PopupBeforeExecute = null;
            this.SEIKYUU_DATE_FROM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUU_DATE_FROM.PopupSearchSendParams")));
            this.SEIKYUU_DATE_FROM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUU_DATE_FROM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUU_DATE_FROM.popupWindowSetting")));
            this.SEIKYUU_DATE_FROM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DATE_FROM.RegistCheckMethod")));
            this.SEIKYUU_DATE_FROM.Size = new System.Drawing.Size(110, 20);
            this.SEIKYUU_DATE_FROM.TabIndex = 10007;
            this.SEIKYUU_DATE_FROM.Tag = "";
            this.SEIKYUU_DATE_FROM.Value = null;
            // 
            // lblSeikyuubi
            // 
            this.lblSeikyuubi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblSeikyuubi.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lblSeikyuubi.ForeColor = System.Drawing.Color.White;
            this.lblSeikyuubi.Location = new System.Drawing.Point(2, 2);
            this.lblSeikyuubi.Name = "lblSeikyuubi";
            this.lblSeikyuubi.Size = new System.Drawing.Size(120, 20);
            this.lblSeikyuubi.TabIndex = 10009;
            this.lblSeikyuubi.Text = "請求日付※";
            this.lblSeikyuubi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TORIHIKISAKI_NAME
            // 
            this.TORIHIKISAKI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TORIHIKISAKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_NAME.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TORIHIKISAKI_NAME.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME.Location = new System.Drawing.Point(177, 29);
            this.TORIHIKISAKI_NAME.MaxLength = 20;
            this.TORIHIKISAKI_NAME.Name = "TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_NAME.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME.popupWindowSetting")));
            this.TORIHIKISAKI_NAME.ReadOnly = true;
            this.TORIHIKISAKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME.Size = new System.Drawing.Size(290, 20);
            this.TORIHIKISAKI_NAME.TabIndex = 10012;
            this.TORIHIKISAKI_NAME.TabStop = false;
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD.ChangeUpperCase = true;
            this.TORIHIKISAKI_CD.CharacterLimitList = null;
            this.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(127, 29);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_CD.ShortItemName = "取引先";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(51, 20);
            this.TORIHIKISAKI_CD.TabIndex = 10011;
            this.TORIHIKISAKI_CD.Tag = "取引先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // lblTORIHIKISAKI
            // 
            this.lblTORIHIKISAKI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblTORIHIKISAKI.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lblTORIHIKISAKI.ForeColor = System.Drawing.Color.White;
            this.lblTORIHIKISAKI.Location = new System.Drawing.Point(3, 29);
            this.lblTORIHIKISAKI.Name = "lblTORIHIKISAKI";
            this.lblTORIHIKISAKI.Size = new System.Drawing.Size(120, 20);
            this.lblTORIHIKISAKI.TabIndex = 10014;
            this.lblTORIHIKISAKI.Text = "取引先※";
            this.lblTORIHIKISAKI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TORIHIKISAKI_FROM_POPUP
            // 
            this.TORIHIKISAKI_FROM_POPUP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_FROM_POPUP.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.TORIHIKISAKI_FROM_POPUP.DBFieldsName = null;
            this.TORIHIKISAKI_FROM_POPUP.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_FROM_POPUP.DisplayItemName = "取引先検索";
            this.TORIHIKISAKI_FROM_POPUP.DisplayPopUp = null;
            this.TORIHIKISAKI_FROM_POPUP.ErrorMessage = null;
            this.TORIHIKISAKI_FROM_POPUP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_FROM_POPUP.FocusOutCheckMethod")));
            this.TORIHIKISAKI_FROM_POPUP.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.TORIHIKISAKI_FROM_POPUP.GetCodeMasterField = null;
            this.TORIHIKISAKI_FROM_POPUP.Image = ((System.Drawing.Image)(resources.GetObject("TORIHIKISAKI_FROM_POPUP.Image")));
            this.TORIHIKISAKI_FROM_POPUP.ItemDefinedTypes = null;
            this.TORIHIKISAKI_FROM_POPUP.LinkedSettingTextBox = null;
            this.TORIHIKISAKI_FROM_POPUP.LinkedTextBoxs = null;
            this.TORIHIKISAKI_FROM_POPUP.Location = new System.Drawing.Point(470, 28);
            this.TORIHIKISAKI_FROM_POPUP.Name = "TORIHIKISAKI_FROM_POPUP";
            this.TORIHIKISAKI_FROM_POPUP.PopupAfterExecute = null;
            this.TORIHIKISAKI_FROM_POPUP.PopupBeforeExecute = null;
            this.TORIHIKISAKI_FROM_POPUP.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_FROM_POPUP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_FROM_POPUP.PopupSearchSendParams")));
            this.TORIHIKISAKI_FROM_POPUP.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_FROM_POPUP.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_FROM_POPUP.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_FROM_POPUP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_FROM_POPUP.popupWindowSetting")));
            this.TORIHIKISAKI_FROM_POPUP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_FROM_POPUP.RegistCheckMethod")));
            this.TORIHIKISAKI_FROM_POPUP.SearchDisplayFlag = 0;
            this.TORIHIKISAKI_FROM_POPUP.SetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME";
            this.TORIHIKISAKI_FROM_POPUP.ShortItemName = "取引先検索";
            this.TORIHIKISAKI_FROM_POPUP.Size = new System.Drawing.Size(22, 22);
            this.TORIHIKISAKI_FROM_POPUP.TabIndex = 10013;
            this.TORIHIKISAKI_FROM_POPUP.TabStop = false;
            this.TORIHIKISAKI_FROM_POPUP.Tag = "検索画面を表示します";
            this.TORIHIKISAKI_FROM_POPUP.UseVisualStyleBackColor = false;
            this.TORIHIKISAKI_FROM_POPUP.ZeroPaddengFlag = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 10015;
            this.label1.Text = "開始残高";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Zandaka
            // 
            this.Zandaka.AutoSize = true;
            this.Zandaka.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.Zandaka.DefaultBackColor = System.Drawing.Color.Empty;
            this.Zandaka.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Zandaka.FocusOutCheckMethod")));
            this.Zandaka.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Zandaka.Location = new System.Drawing.Point(128, 61);
            this.Zandaka.Name = "Zandaka";
            this.Zandaka.PopupAfterExecute = null;
            this.Zandaka.PopupBeforeExecute = null;
            this.Zandaka.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("Zandaka.PopupSearchSendParams")));
            this.Zandaka.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.Zandaka.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("Zandaka.popupWindowSetting")));
            this.Zandaka.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("Zandaka.RegistCheckMethod")));
            this.Zandaka.Size = new System.Drawing.Size(15, 14);
            this.Zandaka.TabIndex = 10016;
            this.Zandaka.Tag = "開始残高を選択してください";
            this.Zandaka.UseVisualStyleBackColor = false;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.SystemColors.Window;
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayItemName = "";
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.GetCodeMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(498, 31);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 2;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupGetMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupSetFormField = "";
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.PopupWindowName = "";
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.SetFormField = "";
            this.ISNOT_NEED_DELETE_FLG.ShortItemName = "";
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(59, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10017;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "True";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            this.ISNOT_NEED_DELETE_FLG.ZeroPaddengFlag = true;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 477);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.TORIHIKISAKI_FROM_POPUP);
            this.Controls.Add(this.TORIHIKISAKI_NAME);
            this.Controls.Add(this.Zandaka);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.lblTORIHIKISAKI);
            this.Controls.Add(this.SEIKYUU_DATE_TO);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.SEIKYUU_DATE_FROM);
            this.Controls.Add(this.lblSeikyuubi);
            this.Controls.Add(this.dgvNyukinDeleteMeisai);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvNyukinDeleteMeisai)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomDataGridView dgvNyukinDeleteMeisai;
        public r_framework.CustomControl.CustomDateTimePicker SEIKYUU_DATE_TO;
        private System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomDateTimePicker SEIKYUU_DATE_FROM;
        private System.Windows.Forms.Label lblSeikyuubi;
        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        private System.Windows.Forms.Label lblTORIHIKISAKI;
        internal r_framework.CustomControl.CustomPopupOpenButton TORIHIKISAKI_FROM_POPUP;
        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomCheckBox Zandaka;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn DELETE_FLG;
        private r_framework.CustomControl.DgvCustomTextBoxColumn NYUUKIN_NUMBER;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn GYOUSHA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GYOUSHA_NAME_RYAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn GENBA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GENBA_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SEIKYUU_NUMBER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SEIKYUU_DATE;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column SEIKYUUGAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column KeshikomiGaku;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column keshikomiGakuTotal;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column MiKeshikomiGaku;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column maeKeshikomiGakuTotal;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KESHIKOMI_BIKOU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SYSTEM_ID;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KESHIKOMI_SEQ;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KAGAMI_NUMBER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn ROW_NUMBER;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}