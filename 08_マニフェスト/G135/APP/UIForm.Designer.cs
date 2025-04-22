// $Id: UIForm.Designer.cs 24236 2014-06-30 02:59:52Z takeda $
namespace Shougun.Core.PaperManifest.JissekiHokokuIchiran
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.HOKOKU_NENDO = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.HOZON_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.HOKOKU_SYOSHIKI = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.HOKOKU_SYOSHIKI_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TEISHUTSU_CHIIKI_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TEISHUTSU_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GOV_OR_MAY_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TOKUBETSU_KANRI_KBN = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.TOKUBETSU_KANRI_SYURUI = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GYOUSHA_KBN = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GYOUSHA_KBN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.KEN_KBN = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.KEN_KBN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_DATE = new r_framework.CustomControl.DgvCustomDataTimeColumn();
            this.CREATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SYSTEM_ID = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SEQ = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.REPORT_KBN = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.customSortHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSortHeader();
            this.customPanel8 = new r_framework.CustomControl.CustomPanel();
            this.rdoMade = new r_framework.CustomControl.CustomRadioButton();
            this.rdoNento = new r_framework.CustomControl.CustomRadioButton();
            this.txtHokokuNento = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label14 = new System.Windows.Forms.Label();
            this.cbtn_Next = new r_framework.CustomControl.CustomButton();
            this.cbtn_Previous = new r_framework.CustomControl.CustomButton();
            this.cantxt_Nento = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewProgressBarColumn1 = new Shougun.Core.PaperManifest.JissekiHokokuIchiran.DataGridViewProgressBarColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.customPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToAddRows = false;
            this.Ichiran.AllowUserToDeleteRows = false;
            this.Ichiran.AllowUserToResizeRows = false;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Ichiran.ColumnHeadersHeight = 21;
            this.Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.HOKOKU_NENDO,
            this.HOZON_NAME,
            this.HOKOKU_SYOSHIKI,
            this.HOKOKU_SYOSHIKI_NAME,
            this.TEISHUTSU_CHIIKI_CD,
            this.TEISHUTSU_NAME,
            this.GOV_OR_MAY_NAME,
            this.TOKUBETSU_KANRI_KBN,
            this.TOKUBETSU_KANRI_SYURUI,
            this.GYOUSHA_KBN,
            this.GYOUSHA_KBN_NAME,
            this.KEN_KBN,
            this.KEN_KBN_NAME,
            this.CREATE_DATE,
            this.CREATE_USER,
            this.SYSTEM_ID,
            this.SEQ,
            this.REPORT_KBN});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle20;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = "customSortHeader1";
            this.Ichiran.Location = new System.Drawing.Point(0, 71);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle21.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(980, 377);
            this.Ichiran.TabIndex = 5;
            this.Ichiran.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ichiran_CellDoubleClick);
            // 
            // HOKOKU_NENDO
            // 
            this.HOKOKU_NENDO.CharactersNumber = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.HOKOKU_NENDO.DataPropertyName = "HOKOKU_NENDO";
            this.HOKOKU_NENDO.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.HOKOKU_NENDO.DefaultCellStyle = dataGridViewCellStyle2;
            this.HOKOKU_NENDO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOKOKU_NENDO.FocusOutCheckMethod")));
            this.HOKOKU_NENDO.HeaderText = "報告年度";
            this.HOKOKU_NENDO.Name = "HOKOKU_NENDO";
            this.HOKOKU_NENDO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOKOKU_NENDO.PopupSearchSendParams")));
            this.HOKOKU_NENDO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOKOKU_NENDO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOKOKU_NENDO.popupWindowSetting")));
            this.HOKOKU_NENDO.ReadOnly = true;
            this.HOKOKU_NENDO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOKOKU_NENDO.RegistCheckMethod")));
            this.HOKOKU_NENDO.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HOKOKU_NENDO.Width = 80;
            // 
            // HOZON_NAME
            // 
            this.HOZON_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.HOZON_NAME.DataPropertyName = "HOZON_NAME";
            this.HOZON_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.HOZON_NAME.DefaultCellStyle = dataGridViewCellStyle3;
            this.HOZON_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOZON_NAME.FocusOutCheckMethod")));
            this.HOZON_NAME.HeaderText = "保存名";
            this.HOZON_NAME.Name = "HOZON_NAME";
            this.HOZON_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOZON_NAME.PopupSearchSendParams")));
            this.HOZON_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOZON_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOZON_NAME.popupWindowSetting")));
            this.HOZON_NAME.ReadOnly = true;
            this.HOZON_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOZON_NAME.RegistCheckMethod")));
            this.HOZON_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HOZON_NAME.Width = 120;
            // 
            // HOKOKU_SYOSHIKI
            // 
            this.HOKOKU_SYOSHIKI.DataPropertyName = "HOUKOKU_SHOSHIKI";
            this.HOKOKU_SYOSHIKI.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.HOKOKU_SYOSHIKI.DefaultCellStyle = dataGridViewCellStyle4;
            this.HOKOKU_SYOSHIKI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOKOKU_SYOSHIKI.FocusOutCheckMethod")));
            this.HOKOKU_SYOSHIKI.HeaderText = "提出書式区分";
            this.HOKOKU_SYOSHIKI.Name = "HOKOKU_SYOSHIKI";
            this.HOKOKU_SYOSHIKI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOKOKU_SYOSHIKI.PopupSearchSendParams")));
            this.HOKOKU_SYOSHIKI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOKOKU_SYOSHIKI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOKOKU_SYOSHIKI.popupWindowSetting")));
            this.HOKOKU_SYOSHIKI.ReadOnly = true;
            this.HOKOKU_SYOSHIKI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOKOKU_SYOSHIKI.RegistCheckMethod")));
            this.HOKOKU_SYOSHIKI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HOKOKU_SYOSHIKI.Visible = false;
            this.HOKOKU_SYOSHIKI.Width = 130;
            // 
            // HOKOKU_SYOSHIKI_NAME
            // 
            this.HOKOKU_SYOSHIKI_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.HOKOKU_SYOSHIKI_NAME.DataPropertyName = "HOUKOKU_SHOSHIKI_NAME";
            this.HOKOKU_SYOSHIKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.HOKOKU_SYOSHIKI_NAME.DefaultCellStyle = dataGridViewCellStyle5;
            this.HOKOKU_SYOSHIKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOKOKU_SYOSHIKI_NAME.FocusOutCheckMethod")));
            this.HOKOKU_SYOSHIKI_NAME.HeaderText = "提出書式";
            this.HOKOKU_SYOSHIKI_NAME.Name = "HOKOKU_SYOSHIKI_NAME";
            this.HOKOKU_SYOSHIKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOKOKU_SYOSHIKI_NAME.PopupSearchSendParams")));
            this.HOKOKU_SYOSHIKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOKOKU_SYOSHIKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOKOKU_SYOSHIKI_NAME.popupWindowSetting")));
            this.HOKOKU_SYOSHIKI_NAME.ReadOnly = true;
            this.HOKOKU_SYOSHIKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOKOKU_SYOSHIKI_NAME.RegistCheckMethod")));
            this.HOKOKU_SYOSHIKI_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HOKOKU_SYOSHIKI_NAME.Width = 80;
            // 
            // TEISHUTSU_CHIIKI_CD
            // 
            this.TEISHUTSU_CHIIKI_CD.DataPropertyName = "TEISHUTSU_CHIIKI_CD";
            this.TEISHUTSU_CHIIKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.TEISHUTSU_CHIIKI_CD.DefaultCellStyle = dataGridViewCellStyle6;
            this.TEISHUTSU_CHIIKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEISHUTSU_CHIIKI_CD.FocusOutCheckMethod")));
            this.TEISHUTSU_CHIIKI_CD.HeaderText = "提出先CD";
            this.TEISHUTSU_CHIIKI_CD.Name = "TEISHUTSU_CHIIKI_CD";
            this.TEISHUTSU_CHIIKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TEISHUTSU_CHIIKI_CD.PopupSearchSendParams")));
            this.TEISHUTSU_CHIIKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TEISHUTSU_CHIIKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TEISHUTSU_CHIIKI_CD.popupWindowSetting")));
            this.TEISHUTSU_CHIIKI_CD.ReadOnly = true;
            this.TEISHUTSU_CHIIKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEISHUTSU_CHIIKI_CD.RegistCheckMethod")));
            this.TEISHUTSU_CHIIKI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TEISHUTSU_CHIIKI_CD.Visible = false;
            this.TEISHUTSU_CHIIKI_CD.Width = 80;
            // 
            // TEISHUTSU_NAME
            // 
            this.TEISHUTSU_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TEISHUTSU_NAME.DataPropertyName = "TEISHUTSU_NAME";
            this.TEISHUTSU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.TEISHUTSU_NAME.DefaultCellStyle = dataGridViewCellStyle7;
            this.TEISHUTSU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEISHUTSU_NAME.FocusOutCheckMethod")));
            this.TEISHUTSU_NAME.HeaderText = "提出先";
            this.TEISHUTSU_NAME.Name = "TEISHUTSU_NAME";
            this.TEISHUTSU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TEISHUTSU_NAME.PopupSearchSendParams")));
            this.TEISHUTSU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TEISHUTSU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TEISHUTSU_NAME.popupWindowSetting")));
            this.TEISHUTSU_NAME.ReadOnly = true;
            this.TEISHUTSU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEISHUTSU_NAME.RegistCheckMethod")));
            this.TEISHUTSU_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TEISHUTSU_NAME.Width = 115;
            // 
            // GOV_OR_MAY_NAME
            // 
            this.GOV_OR_MAY_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GOV_OR_MAY_NAME.DataPropertyName = "GOV_OR_MAY_NAME";
            this.GOV_OR_MAY_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.GOV_OR_MAY_NAME.DefaultCellStyle = dataGridViewCellStyle8;
            this.GOV_OR_MAY_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GOV_OR_MAY_NAME.FocusOutCheckMethod")));
            this.GOV_OR_MAY_NAME.HeaderText = "知事・市長名";
            this.GOV_OR_MAY_NAME.Name = "GOV_OR_MAY_NAME";
            this.GOV_OR_MAY_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GOV_OR_MAY_NAME.PopupSearchSendParams")));
            this.GOV_OR_MAY_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GOV_OR_MAY_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GOV_OR_MAY_NAME.popupWindowSetting")));
            this.GOV_OR_MAY_NAME.ReadOnly = true;
            this.GOV_OR_MAY_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GOV_OR_MAY_NAME.RegistCheckMethod")));
            this.GOV_OR_MAY_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GOV_OR_MAY_NAME.Width = 110;
            // 
            // TOKUBETSU_KANRI_KBN
            // 
            this.TOKUBETSU_KANRI_KBN.DataPropertyName = "TOKUBETSU_KANRI_KBN";
            this.TOKUBETSU_KANRI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.TOKUBETSU_KANRI_KBN.DefaultCellStyle = dataGridViewCellStyle9;
            this.TOKUBETSU_KANRI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOKUBETSU_KANRI_KBN.FocusOutCheckMethod")));
            this.TOKUBETSU_KANRI_KBN.HeaderText = "特管区分KBN";
            this.TOKUBETSU_KANRI_KBN.Name = "TOKUBETSU_KANRI_KBN";
            this.TOKUBETSU_KANRI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TOKUBETSU_KANRI_KBN.PopupSearchSendParams")));
            this.TOKUBETSU_KANRI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TOKUBETSU_KANRI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TOKUBETSU_KANRI_KBN.popupWindowSetting")));
            this.TOKUBETSU_KANRI_KBN.ReadOnly = true;
            this.TOKUBETSU_KANRI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOKUBETSU_KANRI_KBN.RegistCheckMethod")));
            this.TOKUBETSU_KANRI_KBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TOKUBETSU_KANRI_KBN.Visible = false;
            // 
            // TOKUBETSU_KANRI_SYURUI
            // 
            this.TOKUBETSU_KANRI_SYURUI.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.TOKUBETSU_KANRI_SYURUI.DataPropertyName = "TOKUBETSU_KANRI_SYURUI";
            this.TOKUBETSU_KANRI_SYURUI.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.TOKUBETSU_KANRI_SYURUI.DefaultCellStyle = dataGridViewCellStyle10;
            this.TOKUBETSU_KANRI_SYURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOKUBETSU_KANRI_SYURUI.FocusOutCheckMethod")));
            this.TOKUBETSU_KANRI_SYURUI.HeaderText = "特管区分";
            this.TOKUBETSU_KANRI_SYURUI.Name = "TOKUBETSU_KANRI_SYURUI";
            this.TOKUBETSU_KANRI_SYURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TOKUBETSU_KANRI_SYURUI.PopupSearchSendParams")));
            this.TOKUBETSU_KANRI_SYURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TOKUBETSU_KANRI_SYURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TOKUBETSU_KANRI_SYURUI.popupWindowSetting")));
            this.TOKUBETSU_KANRI_SYURUI.ReadOnly = true;
            this.TOKUBETSU_KANRI_SYURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TOKUBETSU_KANRI_SYURUI.RegistCheckMethod")));
            this.TOKUBETSU_KANRI_SYURUI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TOKUBETSU_KANRI_SYURUI.Width = 80;
            // 
            // GYOUSHA_KBN
            // 
            this.GYOUSHA_KBN.DataPropertyName = "GYOUSHA_KBN";
            this.GYOUSHA_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_KBN.DefaultCellStyle = dataGridViewCellStyle11;
            this.GYOUSHA_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_KBN.FocusOutCheckMethod")));
            this.GYOUSHA_KBN.HeaderText = "自社業種区分KBN";
            this.GYOUSHA_KBN.Name = "GYOUSHA_KBN";
            this.GYOUSHA_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_KBN.PopupSearchSendParams")));
            this.GYOUSHA_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_KBN.popupWindowSetting")));
            this.GYOUSHA_KBN.ReadOnly = true;
            this.GYOUSHA_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_KBN.RegistCheckMethod")));
            this.GYOUSHA_KBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GYOUSHA_KBN.Visible = false;
            this.GYOUSHA_KBN.Width = 120;
            // 
            // GYOUSHA_KBN_NAME
            // 
            this.GYOUSHA_KBN_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.GYOUSHA_KBN_NAME.DataPropertyName = "GYOUSHA_KBN_NAME";
            this.GYOUSHA_KBN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_KBN_NAME.DefaultCellStyle = dataGridViewCellStyle12;
            this.GYOUSHA_KBN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_KBN_NAME.FocusOutCheckMethod")));
            this.GYOUSHA_KBN_NAME.HeaderText = "自社業種区分";
            this.GYOUSHA_KBN_NAME.Name = "GYOUSHA_KBN_NAME";
            this.GYOUSHA_KBN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_KBN_NAME.PopupSearchSendParams")));
            this.GYOUSHA_KBN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_KBN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_KBN_NAME.popupWindowSetting")));
            this.GYOUSHA_KBN_NAME.ReadOnly = true;
            this.GYOUSHA_KBN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_KBN_NAME.RegistCheckMethod")));
            this.GYOUSHA_KBN_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // KEN_KBN
            // 
            this.KEN_KBN.DataPropertyName = "KEN_KBN";
            this.KEN_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.KEN_KBN.DefaultCellStyle = dataGridViewCellStyle13;
            this.KEN_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEN_KBN.FocusOutCheckMethod")));
            this.KEN_KBN.HeaderText = "県区分KBN";
            this.KEN_KBN.Name = "KEN_KBN";
            this.KEN_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEN_KBN.PopupSearchSendParams")));
            this.KEN_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEN_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEN_KBN.popupWindowSetting")));
            this.KEN_KBN.ReadOnly = true;
            this.KEN_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEN_KBN.RegistCheckMethod")));
            this.KEN_KBN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KEN_KBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KEN_KBN.Visible = false;
            this.KEN_KBN.Width = 50;
            // 
            // KEN_KBN_NAME
            // 
            this.KEN_KBN_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.KEN_KBN_NAME.DataPropertyName = "KEN_KBN_NAME";
            this.KEN_KBN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.Format = "d";
            dataGridViewCellStyle14.NullValue = null;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.KEN_KBN_NAME.DefaultCellStyle = dataGridViewCellStyle14;
            this.KEN_KBN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEN_KBN_NAME.FocusOutCheckMethod")));
            this.KEN_KBN_NAME.HeaderText = "県区分";
            this.KEN_KBN_NAME.Name = "KEN_KBN_NAME";
            this.KEN_KBN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEN_KBN_NAME.PopupSearchSendParams")));
            this.KEN_KBN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEN_KBN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEN_KBN_NAME.popupWindowSetting")));
            this.KEN_KBN_NAME.ReadOnly = true;
            this.KEN_KBN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEN_KBN_NAME.RegistCheckMethod")));
            this.KEN_KBN_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KEN_KBN_NAME.Width = 70;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            dataGridViewCellStyle15.Format = "N0";
            dataGridViewCellStyle15.NullValue = "0";
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_DATE.DefaultCellStyle = dataGridViewCellStyle15;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.HeaderText = "保存年月日";
            this.CREATE_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_DATE.Width = 110;
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.CharactersNumber = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.CREATE_USER.DataPropertyName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_USER.DefaultCellStyle = dataGridViewCellStyle16;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.HeaderText = "保存者";
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SYSTEM_ID
            // 
            this.SYSTEM_ID.DataPropertyName = "SYSTEM_ID";
            this.SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            this.SYSTEM_ID.DefaultCellStyle = dataGridViewCellStyle17;
            this.SYSTEM_ID.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.FocusOutCheckMethod")));
            this.SYSTEM_ID.HeaderText = "SYSTEM_ID";
            this.SYSTEM_ID.Name = "SYSTEM_ID";
            this.SYSTEM_ID.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SYSTEM_ID.PopupSearchSendParams")));
            this.SYSTEM_ID.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SYSTEM_ID.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SYSTEM_ID.popupWindowSetting")));
            this.SYSTEM_ID.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SYSTEM_ID.RegistCheckMethod")));
            this.SYSTEM_ID.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SYSTEM_ID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SYSTEM_ID.Visible = false;
            // 
            // SEQ
            // 
            this.SEQ.DataPropertyName = "SEQ";
            this.SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            this.SEQ.DefaultCellStyle = dataGridViewCellStyle18;
            this.SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEQ.FocusOutCheckMethod")));
            this.SEQ.HeaderText = "SEQ";
            this.SEQ.Name = "SEQ";
            this.SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEQ.PopupSearchSendParams")));
            this.SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEQ.popupWindowSetting")));
            this.SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEQ.RegistCheckMethod")));
            this.SEQ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SEQ.Visible = false;
            // 
            // REPORT_KBN
            // 
            this.REPORT_KBN.DataPropertyName = "REPORT_KBN";
            this.REPORT_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black;
            this.REPORT_KBN.DefaultCellStyle = dataGridViewCellStyle19;
            this.REPORT_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("REPORT_KBN.FocusOutCheckMethod")));
            this.REPORT_KBN.HeaderText = "REPORT_KBN";
            this.REPORT_KBN.Name = "REPORT_KBN";
            this.REPORT_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("REPORT_KBN.PopupSearchSendParams")));
            this.REPORT_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.REPORT_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("REPORT_KBN.popupWindowSetting")));
            this.REPORT_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("REPORT_KBN.RegistCheckMethod")));
            this.REPORT_KBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.REPORT_KBN.Visible = false;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customSortHeader1.LinkedDataGridViewName = "Ichiran";
            this.customSortHeader1.Location = new System.Drawing.Point(0, 26);
            this.customSortHeader1.Name = "customSortHeader1";
            this.customSortHeader1.Size = new System.Drawing.Size(980, 26);
            this.customSortHeader1.SortFlag = false;
            this.customSortHeader1.TabIndex = 0;
            this.customSortHeader1.TabStop = false;
            // 
            // customPanel8
            // 
            this.customPanel8.Controls.Add(this.rdoMade);
            this.customPanel8.Controls.Add(this.rdoNento);
            this.customPanel8.Controls.Add(this.txtHokokuNento);
            this.customPanel8.Location = new System.Drawing.Point(655, 0);
            this.customPanel8.Name = "customPanel8";
            this.customPanel8.Size = new System.Drawing.Size(216, 20);
            this.customPanel8.TabIndex = 2;
            // 
            // rdoMade
            // 
            this.rdoMade.AutoSize = true;
            this.rdoMade.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoMade.DisplayItemName = "迄";
            this.rdoMade.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoMade.FocusOutCheckMethod")));
            this.rdoMade.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoMade.LinkedTextBox = "txtHokokuNento";
            this.rdoMade.Location = new System.Drawing.Point(135, 1);
            this.rdoMade.Name = "rdoMade";
            this.rdoMade.PopupAfterExecute = null;
            this.rdoMade.PopupBeforeExecute = null;
            this.rdoMade.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoMade.PopupSearchSendParams")));
            this.rdoMade.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoMade.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoMade.popupWindowSetting")));
            this.rdoMade.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoMade.RegistCheckMethod")));
            this.rdoMade.ShortItemName = "迄";
            this.rdoMade.Size = new System.Drawing.Size(53, 17);
            this.rdoMade.TabIndex = 3;
            this.rdoMade.Tag = "選択報告年度迄のデータを抽出する場合選択してください";
            this.rdoMade.Text = "2.迄";
            this.rdoMade.UseVisualStyleBackColor = true;
            this.rdoMade.Value = "2";
            // 
            // rdoNento
            // 
            this.rdoNento.AutoSize = true;
            this.rdoNento.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoNento.DisplayItemName = "年";
            this.rdoNento.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoNento.FocusOutCheckMethod")));
            this.rdoNento.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoNento.LinkedTextBox = "txtHokokuNento";
            this.rdoNento.Location = new System.Drawing.Point(25, 1);
            this.rdoNento.Name = "rdoNento";
            this.rdoNento.PopupAfterExecute = null;
            this.rdoNento.PopupBeforeExecute = null;
            this.rdoNento.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoNento.PopupSearchSendParams")));
            this.rdoNento.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoNento.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoNento.popupWindowSetting")));
            this.rdoNento.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoNento.RegistCheckMethod")));
            this.rdoNento.ShortItemName = "年";
            this.rdoNento.Size = new System.Drawing.Size(53, 17);
            this.rdoNento.TabIndex = 2;
            this.rdoNento.Tag = "選択報告年度のデータのみを抽出する場合選択してください";
            this.rdoNento.Text = "1.年";
            this.rdoNento.UseVisualStyleBackColor = true;
            this.rdoNento.Value = "1";
            // 
            // txtHokokuNento
            // 
            this.txtHokokuNento.BackColor = System.Drawing.SystemColors.Window;
            this.txtHokokuNento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHokokuNento.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHokokuNento.DisplayItemName = "報告年度区分";
            this.txtHokokuNento.DisplayPopUp = null;
            this.txtHokokuNento.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHokokuNento.FocusOutCheckMethod")));
            this.txtHokokuNento.ForeColor = System.Drawing.Color.Black;
            this.txtHokokuNento.IsInputErrorOccured = false;
            this.txtHokokuNento.LinkedRadioButtonArray = new string[] {
        "rdoNento",
        "rdoMade"};
            this.txtHokokuNento.Location = new System.Drawing.Point(0, 0);
            this.txtHokokuNento.Name = "txtHokokuNento";
            this.txtHokokuNento.PopupAfterExecute = null;
            this.txtHokokuNento.PopupBeforeExecute = null;
            this.txtHokokuNento.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHokokuNento.PopupSearchSendParams")));
            this.txtHokokuNento.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHokokuNento.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHokokuNento.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHokokuNento.RangeSetting = rangeSettingDto1;
            this.txtHokokuNento.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHokokuNento.RegistCheckMethod")));
            this.txtHokokuNento.Size = new System.Drawing.Size(20, 19);
            this.txtHokokuNento.TabIndex = 1;
            this.txtHokokuNento.Tag = "年度の選択条件を選択してください";
            this.txtHokokuNento.WordWrap = false;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(451, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 20);
            this.label14.TabIndex = 740;
            this.label14.Text = "報告年度";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbtn_Next
            // 
            this.cbtn_Next.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_Next.Location = new System.Drawing.Point(935, 0);
            this.cbtn_Next.Name = "cbtn_Next";
            this.cbtn_Next.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.cbtn_Next.Size = new System.Drawing.Size(42, 22);
            this.cbtn_Next.TabIndex = 4;
            this.cbtn_Next.Tag = "報告年度を翌年度に変更します";
            this.cbtn_Next.Text = "翌年";
            this.cbtn_Next.UseVisualStyleBackColor = true;
            this.cbtn_Next.Click += new System.EventHandler(this.cbtn_Next_Click);
            // 
            // cbtn_Previous
            // 
            this.cbtn_Previous.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_Previous.Location = new System.Drawing.Point(885, 0);
            this.cbtn_Previous.Name = "cbtn_Previous";
            this.cbtn_Previous.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.cbtn_Previous.Size = new System.Drawing.Size(42, 22);
            this.cbtn_Previous.TabIndex = 3;
            this.cbtn_Previous.Tag = "報告年度を前年度に変更します";
            this.cbtn_Previous.Text = "前年";
            this.cbtn_Previous.UseVisualStyleBackColor = true;
            this.cbtn_Previous.Click += new System.EventHandler(this.cbtn_Previous_Click);
            // 
            // cantxt_Nento
            // 
            this.cantxt_Nento.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_Nento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_Nento.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_Nento.DisplayItemName = "報告年度";
            this.cantxt_Nento.DisplayPopUp = null;
            this.cantxt_Nento.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_Nento.FocusOutCheckMethod")));
            this.cantxt_Nento.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.cantxt_Nento.ForeColor = System.Drawing.Color.Black;
            this.cantxt_Nento.IsInputErrorOccured = false;
            this.cantxt_Nento.Location = new System.Drawing.Point(570, 0);
            this.cantxt_Nento.Name = "cantxt_Nento";
            this.cantxt_Nento.PopupAfterExecute = null;
            this.cantxt_Nento.PopupBeforeExecute = null;
            this.cantxt_Nento.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_Nento.PopupSearchSendParams")));
            this.cantxt_Nento.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_Nento.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_Nento.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1753,
            0,
            0,
            0});
            this.cantxt_Nento.RangeSetting = rangeSettingDto2;
            this.cantxt_Nento.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_Nento.RegistCheckMethod")));
            this.cantxt_Nento.Size = new System.Drawing.Size(35, 20);
            this.cantxt_Nento.TabIndex = 1;
            this.cantxt_Nento.Text = "2013";
            this.cantxt_Nento.WordWrap = false;
            this.cantxt_Nento.Validated += new System.EventHandler(this.cantxt_Nento_Validated);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(611, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 742;
            this.label1.Text = "年度";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "コンテナ種類CD";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn1.Visible = false;
            this.dataGridViewTextBoxColumn1.Width = 130;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "コンテナ種類名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dataGridViewTextBoxColumn2.Visible = false;
            this.dataGridViewTextBoxColumn2.Width = 130;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "コンテナCD";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 130;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "コンテナ名";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 150;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "業者CD";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 80;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "業者名";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 145;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.HeaderText = "現場CD";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "現場名";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 145;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "営業担当者CD";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 120;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.HeaderText = "営業担当者名";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 145;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "設置日";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 75;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "経過日数";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 90;
            // 
            // dataGridViewProgressBarColumn1
            // 
            this.dataGridViewProgressBarColumn1.HeaderText = "グラフ";
            this.dataGridViewProgressBarColumn1.Maximum = 100;
            this.dataGridViewProgressBarColumn1.Mimimum = 0;
            this.dataGridViewProgressBarColumn1.Name = "dataGridViewProgressBarColumn1";
            this.dataGridViewProgressBarColumn1.Width = 192;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cantxt_Nento);
            this.Controls.Add(this.cbtn_Next);
            this.Controls.Add(this.cbtn_Previous);
            this.Controls.Add(this.customPanel8);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.customSortHeader1);
            this.Controls.Add(this.Ichiran);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.customPanel8.ResumeLayout(false);
            this.customPanel8.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomDataGridView Ichiran;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewProgressBarColumn dataGridViewProgressBarColumn1;
        public r_framework.CustomControl.DataGridCustomControl.CustomSortHeader customSortHeader1;
        private r_framework.CustomControl.CustomPanel customPanel8;
        private r_framework.CustomControl.CustomRadioButton rdoMade;
        private r_framework.CustomControl.CustomRadioButton rdoNento;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtHokokuNento;
        public System.Windows.Forms.Label label14;
        private r_framework.CustomControl.CustomButton cbtn_Next;
        private r_framework.CustomControl.CustomButton cbtn_Previous;
        private r_framework.CustomControl.DgvCustomTextBoxColumn HOKOKU_NENDO;
        private r_framework.CustomControl.DgvCustomTextBoxColumn HOZON_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn HOKOKU_SYOSHIKI;
        private r_framework.CustomControl.DgvCustomTextBoxColumn HOKOKU_SYOSHIKI_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TEISHUTSU_CHIIKI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TEISHUTSU_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GOV_OR_MAY_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TOKUBETSU_KANRI_KBN;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TOKUBETSU_KANRI_SYURUI;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GYOUSHA_KBN;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GYOUSHA_KBN_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KEN_KBN;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KEN_KBN_NAME;
        private r_framework.CustomControl.DgvCustomDataTimeColumn CREATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SYSTEM_ID;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SEQ;
        private r_framework.CustomControl.DgvCustomTextBoxColumn REPORT_KBN;
        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomNumericTextBox2 cantxt_Nento;

    }
}