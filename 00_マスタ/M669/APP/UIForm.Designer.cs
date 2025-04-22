using System.Windows.Forms;
namespace Shougun.Core.Master.ContenaQrHakkou.APP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.DET_CHECKED = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.DET_CONTENA_SHURUI_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DET_CONTENA_SHURUI_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DET_CONTENA_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DET_CONTENA_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DET_SHOYUU_DAISUU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.dgvCustomCheckBoxColumn1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.LBL_PAGE = new System.Windows.Forms.Label();
            this.LBL_CONTENA_SYURUI = new System.Windows.Forms.Label();
            this.LBL_CONTENA = new System.Windows.Forms.Label();
            this.PAGE_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.PNL_PAGE = new r_framework.CustomControl.CustomPanel();
            this.PAGE_KBN_4 = new r_framework.CustomControl.CustomRadioButton();
            this.PAGE_KBN_3 = new r_framework.CustomControl.CustomRadioButton();
            this.PAGE_KBN_1 = new r_framework.CustomControl.CustomRadioButton();
            this.PAGE_KBN_2 = new r_framework.CustomControl.CustomRadioButton();
            this.CONTENA_SHURUI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.CONTENA_SHURUI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.CONTENA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.CONTENA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.ALL_CHECKED = new r_framework.CustomControl.CustomCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.PNL_PAGE.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToDeleteRows = false;
            this.Ichiran.AllowUserToResizeColumns = false;
            this.Ichiran.AllowUserToResizeRows = false;
            this.Ichiran.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Ichiran.ColumnHeadersHeight = 21;
            this.Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DET_CHECKED,
            this.DET_CONTENA_SHURUI_CD,
            this.DET_CONTENA_SHURUI_NAME_RYAKU,
            this.DET_CONTENA_CD,
            this.DET_CONTENA_NAME_RYAKU,
            this.DET_SHOYUU_DAISUU});
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle8;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = null;
            this.Ichiran.Location = new System.Drawing.Point(1, 99);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(990, 352);
            this.Ichiran.TabIndex = 130;
            this.Ichiran.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ichiran_CellClick);
            this.Ichiran.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.Ichiran_CellPainting);
            // 
            // DET_CHECKED
            // 
            this.DET_CHECKED.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DET_CHECKED.DataPropertyName = "DET_CHECKED";
            this.DET_CHECKED.DBFieldsName = "ALL_CHECKED";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DET_CHECKED.DefaultCellStyle = dataGridViewCellStyle2;
            this.DET_CHECKED.FocusOutCheckMethod = null;
            this.DET_CHECKED.HeaderText = "";
            this.DET_CHECKED.ItemDefinedTypes = "bit";
            this.DET_CHECKED.Name = "DET_CHECKED";
            this.DET_CHECKED.RegistCheckMethod = null;
            this.DET_CHECKED.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DET_CHECKED.ToolTipText = "印刷対象の明細を選択してください";
            this.DET_CHECKED.ViewSearchItem = false;
            this.DET_CHECKED.Width = 60;
            // 
            // DET_CONTENA_SHURUI_CD
            //
            this.DET_CONTENA_SHURUI_CD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DET_CONTENA_SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.DET_CONTENA_SHURUI_CD.DefaultCellStyle = dataGridViewCellStyle3;
            this.DET_CONTENA_SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DET_CONTENA_SHURUI_CD.FocusOutCheckMethod")));
            this.DET_CONTENA_SHURUI_CD.HeaderText = "コンテナ種類CD";
            this.DET_CONTENA_SHURUI_CD.Name = "DET_CONTENA_SHURUI_CD";
            this.DET_CONTENA_SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DET_CONTENA_SHURUI_CD.PopupSearchSendParams")));
            this.DET_CONTENA_SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DET_CONTENA_SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DET_CONTENA_SHURUI_CD.popupWindowSetting")));
            this.DET_CONTENA_SHURUI_CD.ReadOnly = true;
            this.DET_CONTENA_SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DET_CONTENA_SHURUI_CD.RegistCheckMethod")));
            this.DET_CONTENA_SHURUI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DET_CONTENA_SHURUI_CD.Width = 111;
            this.DET_CONTENA_SHURUI_CD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DET_CONTENA_SHURUI_NAME_RYAKU
            //
            this.DET_CONTENA_SHURUI_NAME_RYAKU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DET_CONTENA_SHURUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.DET_CONTENA_SHURUI_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle4;
            this.DET_CONTENA_SHURUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DET_CONTENA_SHURUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.DET_CONTENA_SHURUI_NAME_RYAKU.HeaderText = "コンテナ種類名";
            this.DET_CONTENA_SHURUI_NAME_RYAKU.Name = "DET_CONTENA_SHURUI_NAME_RYAKU";
            this.DET_CONTENA_SHURUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DET_CONTENA_SHURUI_NAME_RYAKU.PopupSearchSendParams")));
            this.DET_CONTENA_SHURUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DET_CONTENA_SHURUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DET_CONTENA_SHURUI_NAME_RYAKU.popupWindowSetting")));
            this.DET_CONTENA_SHURUI_NAME_RYAKU.ReadOnly = true;
            this.DET_CONTENA_SHURUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DET_CONTENA_SHURUI_NAME_RYAKU.RegistCheckMethod")));
            this.DET_CONTENA_SHURUI_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DET_CONTENA_SHURUI_NAME_RYAKU.Width = 130;
            this.DET_CONTENA_SHURUI_NAME_RYAKU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DET_CONTENA_CD
            //
            this.DET_CONTENA_CD.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DET_CONTENA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.DET_CONTENA_CD.DefaultCellStyle = dataGridViewCellStyle5;
            this.DET_CONTENA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DET_CONTENA_CD.FocusOutCheckMethod")));
            this.DET_CONTENA_CD.HeaderText = "コンテナCD";
            this.DET_CONTENA_CD.Name = "DET_CONTENA_CD";
            this.DET_CONTENA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DET_CONTENA_CD.PopupSearchSendParams")));
            this.DET_CONTENA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DET_CONTENA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DET_CONTENA_CD.popupWindowSetting")));
            this.DET_CONTENA_CD.ReadOnly = true;
            this.DET_CONTENA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DET_CONTENA_CD.RegistCheckMethod")));
            this.DET_CONTENA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DET_CONTENA_CD.Width = 83;
            this.DET_CONTENA_CD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DET_CONTENA_NAME_RYAKU
            //
            this.DET_CONTENA_NAME_RYAKU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DET_CONTENA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.DET_CONTENA_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle6;
            this.DET_CONTENA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DET_CONTENA_NAME_RYAKU.FocusOutCheckMethod")));
            this.DET_CONTENA_NAME_RYAKU.HeaderText = "コンテナ名";
            this.DET_CONTENA_NAME_RYAKU.Name = "DET_CONTENA_NAME_RYAKU";
            this.DET_CONTENA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DET_CONTENA_NAME_RYAKU.PopupSearchSendParams")));
            this.DET_CONTENA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DET_CONTENA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DET_CONTENA_NAME_RYAKU.popupWindowSetting")));
            this.DET_CONTENA_NAME_RYAKU.ReadOnly = true;
            this.DET_CONTENA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DET_CONTENA_NAME_RYAKU.RegistCheckMethod")));
            this.DET_CONTENA_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DET_CONTENA_NAME_RYAKU.Width = 150;
            this.DET_CONTENA_NAME_RYAKU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DET_SHOYUU_DAISUU
            //
            this.DET_SHOYUU_DAISUU.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DET_SHOYUU_DAISUU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.DET_SHOYUU_DAISUU.DefaultCellStyle = dataGridViewCellStyle7;
            this.DET_SHOYUU_DAISUU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DET_SHOYUU_DAISUU.FocusOutCheckMethod")));
            this.DET_SHOYUU_DAISUU.HeaderText = "印刷数";
            this.DET_SHOYUU_DAISUU.MinimumWidth = 2;
            this.DET_SHOYUU_DAISUU.Name = "DET_SHOYUU_DAISUU";
            this.DET_SHOYUU_DAISUU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DET_SHOYUU_DAISUU.PopupSearchSendParams")));
            this.DET_SHOYUU_DAISUU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DET_SHOYUU_DAISUU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DET_SHOYUU_DAISUU.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DET_SHOYUU_DAISUU.RangeSetting = rangeSettingDto1;
            this.DET_SHOYUU_DAISUU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DET_SHOYUU_DAISUU.RegistCheckMethod")));
            this.DET_SHOYUU_DAISUU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DET_SHOYUU_DAISUU.ToolTipText = "印刷対象の発行部数を入力してください";
            this.DET_SHOYUU_DAISUU.Width = 55;
            this.DET_SHOYUU_DAISUU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // dgvCustomCheckBoxColumn1
            // 
            this.dgvCustomCheckBoxColumn1.DataPropertyName = "DELETE_FLG";
            this.dgvCustomCheckBoxColumn1.DBFieldsName = "DELETE_FLG";
            this.dgvCustomCheckBoxColumn1.FocusOutCheckMethod = null;
            this.dgvCustomCheckBoxColumn1.HeaderText = "削除";
            this.dgvCustomCheckBoxColumn1.ItemDefinedTypes = "bit";
            this.dgvCustomCheckBoxColumn1.Name = "dgvCustomCheckBoxColumn1";
            this.dgvCustomCheckBoxColumn1.RegistCheckMethod = null;
            this.dgvCustomCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCustomCheckBoxColumn1.ToolTipText = "削除する場合、チェックしてください";
            this.dgvCustomCheckBoxColumn1.ViewSearchItem = false;
            this.dgvCustomCheckBoxColumn1.Width = 43;
            // 
            // LBL_PAGE
            // 
            this.LBL_PAGE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_PAGE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_PAGE.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_PAGE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LBL_PAGE.ForeColor = System.Drawing.Color.White;
            this.LBL_PAGE.Location = new System.Drawing.Point(1, 9);
            this.LBL_PAGE.Name = "LBL_PAGE";
            this.LBL_PAGE.Size = new System.Drawing.Size(120, 20);
            this.LBL_PAGE.TabIndex = 10;
            this.LBL_PAGE.Text = "発行数/ページ※";
            this.LBL_PAGE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_CONTENA_SYURUI
            // 
            this.LBL_CONTENA_SYURUI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_CONTENA_SYURUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_CONTENA_SYURUI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_CONTENA_SYURUI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LBL_CONTENA_SYURUI.ForeColor = System.Drawing.Color.White;
            this.LBL_CONTENA_SYURUI.Location = new System.Drawing.Point(1, 39);
            this.LBL_CONTENA_SYURUI.Name = "LBL_CONTENA_SYURUI";
            this.LBL_CONTENA_SYURUI.Size = new System.Drawing.Size(120, 20);
            this.LBL_CONTENA_SYURUI.TabIndex = 40;
            this.LBL_CONTENA_SYURUI.Text = "コンテナ種類";
            this.LBL_CONTENA_SYURUI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LBL_CONTENA
            // 
            this.LBL_CONTENA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LBL_CONTENA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LBL_CONTENA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LBL_CONTENA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LBL_CONTENA.ForeColor = System.Drawing.Color.White;
            this.LBL_CONTENA.Location = new System.Drawing.Point(1, 69);
            this.LBL_CONTENA.Name = "LBL_CONTENA";
            this.LBL_CONTENA.Size = new System.Drawing.Size(120, 20);
            this.LBL_CONTENA.TabIndex = 70;
            this.LBL_CONTENA.Text = "コンテナ";
            this.LBL_CONTENA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PAGE_KBN
            // 
            this.PAGE_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.PAGE_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PAGE_KBN.DBFieldsName = "";
            this.PAGE_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.PAGE_KBN.DisplayItemName = "発行数/ページ";
            this.PAGE_KBN.DisplayPopUp = null;
            this.PAGE_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PAGE_KBN.FocusOutCheckMethod")));
            this.PAGE_KBN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.PAGE_KBN.ForeColor = System.Drawing.Color.Black;
            this.PAGE_KBN.IsInputErrorOccured = false;
            this.PAGE_KBN.LinkedRadioButtonArray = new string[] {
        "PAGE_KBN_1",
        "PAGE_KBN_2",
        "PAGE_KBN_3",
        "PAGE_KBN_4"};
            this.PAGE_KBN.Location = new System.Drawing.Point(126, 9);
            this.PAGE_KBN.Name = "PAGE_KBN";
            this.PAGE_KBN.PopupAfterExecute = null;
            this.PAGE_KBN.PopupBeforeExecute = null;
            this.PAGE_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PAGE_KBN.PopupSearchSendParams")));
            this.PAGE_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PAGE_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PAGE_KBN.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            4,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PAGE_KBN.RangeSetting = rangeSettingDto2;
            this.PAGE_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PAGE_KBN.RegistCheckMethod")));
            this.PAGE_KBN.Size = new System.Drawing.Size(76, 20);
            this.PAGE_KBN.TabIndex = 20;
            this.PAGE_KBN.Tag = "【1～4】のいずれかで入力してください";
            this.PAGE_KBN.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PAGE_KBN.WordWrap = false;
            // 
            // PNL_PAGE
            // 
            this.PNL_PAGE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.PNL_PAGE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PNL_PAGE.Controls.Add(this.PAGE_KBN_4);
            this.PNL_PAGE.Controls.Add(this.PAGE_KBN_3);
            this.PNL_PAGE.Controls.Add(this.PAGE_KBN_1);
            this.PNL_PAGE.Controls.Add(this.PAGE_KBN_2);
            this.PNL_PAGE.Location = new System.Drawing.Point(201, 9);
            this.PNL_PAGE.Name = "PNL_PAGE";
            this.PNL_PAGE.Size = new System.Drawing.Size(289, 20);
            this.PNL_PAGE.TabIndex = 30;
            // 
            // PAGE_KBN_4
            // 
            this.PAGE_KBN_4.AutoSize = true;
            this.PAGE_KBN_4.DefaultBackColor = System.Drawing.Color.Empty;
            this.PAGE_KBN_4.FocusOutCheckMethod = null;
            this.PAGE_KBN_4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.PAGE_KBN_4.LinkedTextBox = "PAGE_KBN";
            this.PAGE_KBN_4.Location = new System.Drawing.Point(216, 0);
            this.PAGE_KBN_4.Name = "PAGE_KBN_4";
            this.PAGE_KBN_4.PopupAfterExecute = null;
            this.PAGE_KBN_4.PopupBeforeExecute = null;
            this.PAGE_KBN_4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PAGE_KBN_4.PopupSearchSendParams")));
            this.PAGE_KBN_4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PAGE_KBN_4.popupWindowSetting = null;
            this.PAGE_KBN_4.RegistCheckMethod = null;
            this.PAGE_KBN_4.Size = new System.Drawing.Size(67, 17);
            this.PAGE_KBN_4.TabIndex = 40;
            this.PAGE_KBN_4.Tag = "発行数/ページを選択します";
            this.PAGE_KBN_4.Text = "4.６つ";
            this.PAGE_KBN_4.UseVisualStyleBackColor = true;
            this.PAGE_KBN_4.Value = "4";
            // 
            // PAGE_KBN_3
            // 
            this.PAGE_KBN_3.AutoSize = true;
            this.PAGE_KBN_3.DefaultBackColor = System.Drawing.Color.Empty;
            this.PAGE_KBN_3.FocusOutCheckMethod = null;
            this.PAGE_KBN_3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.PAGE_KBN_3.LinkedTextBox = "PAGE_KBN";
            this.PAGE_KBN_3.Location = new System.Drawing.Point(145, 0);
            this.PAGE_KBN_3.Name = "PAGE_KBN_3";
            this.PAGE_KBN_3.PopupAfterExecute = null;
            this.PAGE_KBN_3.PopupBeforeExecute = null;
            this.PAGE_KBN_3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PAGE_KBN_3.PopupSearchSendParams")));
            this.PAGE_KBN_3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PAGE_KBN_3.popupWindowSetting = null;
            this.PAGE_KBN_3.RegistCheckMethod = null;
            this.PAGE_KBN_3.Size = new System.Drawing.Size(67, 17);
            this.PAGE_KBN_3.TabIndex = 30;
            this.PAGE_KBN_3.Tag = "発行数/ページを選択します";
            this.PAGE_KBN_3.Text = "3.４つ";
            this.PAGE_KBN_3.UseVisualStyleBackColor = true;
            this.PAGE_KBN_3.Value = "3";
            // 
            // PAGE_KBN_1
            // 
            this.PAGE_KBN_1.AutoSize = true;
            this.PAGE_KBN_1.CausesValidation = false;
            this.PAGE_KBN_1.DefaultBackColor = System.Drawing.Color.Empty;
            this.PAGE_KBN_1.FocusOutCheckMethod = null;
            this.PAGE_KBN_1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.PAGE_KBN_1.LinkedTextBox = "PAGE_KBN";
            this.PAGE_KBN_1.Location = new System.Drawing.Point(3, 0);
            this.PAGE_KBN_1.Name = "PAGE_KBN_1";
            this.PAGE_KBN_1.PopupAfterExecute = null;
            this.PAGE_KBN_1.PopupBeforeExecute = null;
            this.PAGE_KBN_1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PAGE_KBN_1.PopupSearchSendParams")));
            this.PAGE_KBN_1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PAGE_KBN_1.popupWindowSetting = null;
            this.PAGE_KBN_1.RegistCheckMethod = null;
            this.PAGE_KBN_1.Size = new System.Drawing.Size(67, 17);
            this.PAGE_KBN_1.TabIndex = 10;
            this.PAGE_KBN_1.Tag = "発行数/ページを選択します";
            this.PAGE_KBN_1.Text = "1.１つ";
            this.PAGE_KBN_1.UseVisualStyleBackColor = true;
            this.PAGE_KBN_1.Value = "1";
            // 
            // PAGE_KBN_2
            // 
            this.PAGE_KBN_2.AutoSize = true;
            this.PAGE_KBN_2.DefaultBackColor = System.Drawing.Color.Empty;
            this.PAGE_KBN_2.FocusOutCheckMethod = null;
            this.PAGE_KBN_2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.PAGE_KBN_2.LinkedTextBox = "PAGE_KBN";
            this.PAGE_KBN_2.Location = new System.Drawing.Point(74, 0);
            this.PAGE_KBN_2.Name = "PAGE_KBN_2";
            this.PAGE_KBN_2.PopupAfterExecute = null;
            this.PAGE_KBN_2.PopupBeforeExecute = null;
            this.PAGE_KBN_2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PAGE_KBN_2.PopupSearchSendParams")));
            this.PAGE_KBN_2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PAGE_KBN_2.popupWindowSetting = null;
            this.PAGE_KBN_2.RegistCheckMethod = null;
            this.PAGE_KBN_2.Size = new System.Drawing.Size(67, 17);
            this.PAGE_KBN_2.TabIndex = 20;
            this.PAGE_KBN_2.Tag = "発行数/ページを選択します";
            this.PAGE_KBN_2.Text = "2.２つ";
            this.PAGE_KBN_2.UseVisualStyleBackColor = true;
            this.PAGE_KBN_2.Value = "2";
            // 
            // CONTENA_SHURUI_CD
            // 
            this.CONTENA_SHURUI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.CONTENA_SHURUI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONTENA_SHURUI_CD.ChangeUpperCase = true;
            this.CONTENA_SHURUI_CD.CharacterLimitList = null;
            this.CONTENA_SHURUI_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.CONTENA_SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONTENA_SHURUI_CD.DisplayItemName = "コンテナ種類CD";
            this.CONTENA_SHURUI_CD.DisplayPopUp = null;
            this.CONTENA_SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONTENA_SHURUI_CD.ForeColor = System.Drawing.Color.Black;
            this.CONTENA_SHURUI_CD.GetCodeMasterField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CONTENA_SHURUI_CD.IsInputErrorOccured = false;
            this.CONTENA_SHURUI_CD.Location = new System.Drawing.Point(126, 39);
            this.CONTENA_SHURUI_CD.MaxLength = 3;
            this.CONTENA_SHURUI_CD.Name = "CONTENA_SHURUI_CD";
            this.CONTENA_SHURUI_CD.PopupAfterExecute = null;
            this.CONTENA_SHURUI_CD.PopupAfterExecuteMethod = "CONTENA_SHURUI_CD_PopupAfterExecuteMethod";
            this.CONTENA_SHURUI_CD.PopupBeforeExecute = null;
            this.CONTENA_SHURUI_CD.PopupBeforeExecuteMethod = "CONTENA_SHURUI_CD_PopupBeforeExecuteMethod";
            this.CONTENA_SHURUI_CD.PopupGetMasterField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_CD.PopupSearchSendParams")));
            this.CONTENA_SHURUI_CD.PopupSendParams = new string[0];
            this.CONTENA_SHURUI_CD.PopupSetFormField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA_SHURUI;
            this.CONTENA_SHURUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.CONTENA_SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_CD.popupWindowSetting")));
            this.CONTENA_SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD.RegistCheckMethod")));
            this.CONTENA_SHURUI_CD.SetFormField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_CD.Size = new System.Drawing.Size(76, 20);
            this.CONTENA_SHURUI_CD.TabIndex = 50;
            this.CONTENA_SHURUI_CD.Tag = "コンテナ種類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CONTENA_SHURUI_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CONTENA_SHURUI_CD.ZeroPaddengFlag = true;
            this.CONTENA_SHURUI_CD.Enter += new System.EventHandler(this.CONTENA_SHURUI_CD_Enter);
            this.CONTENA_SHURUI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.CONTENA_SHURUI_CD_Validating);
            // 
            // CONTENA_SHURUI_NAME_RYAKU
            // 
            this.CONTENA_SHURUI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONTENA_SHURUI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONTENA_SHURUI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.CONTENA_SHURUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONTENA_SHURUI_NAME_RYAKU.DisplayPopUp = null;
            this.CONTENA_SHURUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONTENA_SHURUI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.CONTENA_SHURUI_NAME_RYAKU.IsInputErrorOccured = false;
            this.CONTENA_SHURUI_NAME_RYAKU.Location = new System.Drawing.Point(201, 39);
            this.CONTENA_SHURUI_NAME_RYAKU.MaxLength = 40;
            this.CONTENA_SHURUI_NAME_RYAKU.Name = "CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_NAME_RYAKU.PopupAfterExecute = null;
            this.CONTENA_SHURUI_NAME_RYAKU.PopupBeforeExecute = null;
            this.CONTENA_SHURUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.PopupSearchSendParams")));
            this.CONTENA_SHURUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.popupWindowSetting")));
            this.CONTENA_SHURUI_NAME_RYAKU.ReadOnly = true;
            this.CONTENA_SHURUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.RegistCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU.Size = new System.Drawing.Size(289, 20);
            this.CONTENA_SHURUI_NAME_RYAKU.TabIndex = 60;
            this.CONTENA_SHURUI_NAME_RYAKU.TabStop = false;
            this.CONTENA_SHURUI_NAME_RYAKU.Tag = "";
            // 
            // CONTENA_CD
            // 
            this.CONTENA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.CONTENA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONTENA_CD.ChangeUpperCase = true;
            this.CONTENA_CD.CharacterLimitList = null;
            this.CONTENA_CD.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CONTENA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONTENA_CD.DisplayItemName = "コンテナCD";
            this.CONTENA_CD.DisplayPopUp = null;
            this.CONTENA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_CD.FocusOutCheckMethod")));
            this.CONTENA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONTENA_CD.ForeColor = System.Drawing.Color.Black;
            this.CONTENA_CD.GetCodeMasterField = "CONTENA_CD,CONTENA_NAME_RYAKU";
            this.CONTENA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CONTENA_CD.IsInputErrorOccured = false;
            this.CONTENA_CD.Location = new System.Drawing.Point(126, 69);
            this.CONTENA_CD.MaxLength = 10;
            this.CONTENA_CD.Name = "CONTENA_CD";
            this.CONTENA_CD.PopupAfterExecute = null;
            this.CONTENA_CD.PopupAfterExecuteMethod = "";
            this.CONTENA_CD.PopupBeforeExecute = null;
            this.CONTENA_CD.PopupGetMasterField = "CONTENA_CD,CONTENA_NAME_RYAKU,CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_CD.PopupSearchSendParams")));
            this.CONTENA_CD.PopupSendParams = new string[0];
            this.CONTENA_CD.PopupSetFormField = "CONTENA_CD,CONTENA_NAME_RYAKU,CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA;
            this.CONTENA_CD.PopupWindowName = "コンテナ検索ポップアップ";
            this.CONTENA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_CD.popupWindowSetting")));
            this.CONTENA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_CD.RegistCheckMethod")));
            this.CONTENA_CD.SetFormField = "CONTENA_CD,CONTENA_NAME_RYAKU";
            this.CONTENA_CD.Size = new System.Drawing.Size(76, 20);
            this.CONTENA_CD.TabIndex = 80;
            this.CONTENA_CD.Tag = "コンテナを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CONTENA_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CONTENA_CD.ZeroPaddengFlag = true;
            this.CONTENA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.CONTENA_CD_Validating);
            // 
            // CONTENA_NAME_RYAKU
            // 
            this.CONTENA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONTENA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONTENA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.CONTENA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONTENA_NAME_RYAKU.DisplayPopUp = null;
            this.CONTENA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_NAME_RYAKU.FocusOutCheckMethod")));
            this.CONTENA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONTENA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.CONTENA_NAME_RYAKU.IsInputErrorOccured = false;
            this.CONTENA_NAME_RYAKU.Location = new System.Drawing.Point(201, 69);
            this.CONTENA_NAME_RYAKU.MaxLength = 40;
            this.CONTENA_NAME_RYAKU.Name = "CONTENA_NAME_RYAKU";
            this.CONTENA_NAME_RYAKU.PopupAfterExecute = null;
            this.CONTENA_NAME_RYAKU.PopupBeforeExecute = null;
            this.CONTENA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_NAME_RYAKU.PopupSearchSendParams")));
            this.CONTENA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_NAME_RYAKU.popupWindowSetting")));
            this.CONTENA_NAME_RYAKU.ReadOnly = true;
            this.CONTENA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_NAME_RYAKU.RegistCheckMethod")));
            this.CONTENA_NAME_RYAKU.Size = new System.Drawing.Size(289, 20);
            this.CONTENA_NAME_RYAKU.TabIndex = 90;
            this.CONTENA_NAME_RYAKU.TabStop = false;
            this.CONTENA_NAME_RYAKU.Tag = "";
            // 
            // ALL_CHECKED
            // 
            this.ALL_CHECKED.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ALL_CHECKED.CheckAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ALL_CHECKED.DefaultBackColor = System.Drawing.Color.Empty;
            this.ALL_CHECKED.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ALL_CHECKED.FocusOutCheckMethod")));
            this.ALL_CHECKED.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.ALL_CHECKED.Location = new System.Drawing.Point(509, 71);
            this.ALL_CHECKED.Name = "ALL_CHECKED";
            this.ALL_CHECKED.PopupAfterExecute = null;
            this.ALL_CHECKED.PopupBeforeExecute = null;
            this.ALL_CHECKED.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ALL_CHECKED.PopupSearchSendParams")));
            this.ALL_CHECKED.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ALL_CHECKED.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ALL_CHECKED.popupWindowSetting")));
            this.ALL_CHECKED.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ALL_CHECKED.RegistCheckMethod")));
            this.ALL_CHECKED.Size = new System.Drawing.Size(13, 14);
            this.ALL_CHECKED.TabIndex = 131;
            this.ALL_CHECKED.Text = "締";
            this.ALL_CHECKED.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.ALL_CHECKED.UseVisualStyleBackColor = false;
            this.ALL_CHECKED.Visible = false;
            this.ALL_CHECKED.CheckedChanged += new System.EventHandler(this.ALL_CHECKED_CheckedChanged);
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 497);
            this.Controls.Add(this.ALL_CHECKED);
            this.Controls.Add(this.CONTENA_CD);
            this.Controls.Add(this.CONTENA_NAME_RYAKU);
            this.Controls.Add(this.CONTENA_SHURUI_CD);
            this.Controls.Add(this.CONTENA_SHURUI_NAME_RYAKU);
            this.Controls.Add(this.PAGE_KBN);
            this.Controls.Add(this.PNL_PAGE);
            this.Controls.Add(this.LBL_CONTENA);
            this.Controls.Add(this.LBL_CONTENA_SYURUI);
            this.Controls.Add(this.LBL_PAGE);
            this.Controls.Add(this.Ichiran);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.PNL_PAGE.ResumeLayout(false);
            this.PNL_PAGE.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomDataGridView Ichiran;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn dgvCustomCheckBoxColumn1;
        internal Label LBL_PAGE;
        internal Label LBL_CONTENA_SYURUI;
        internal Label LBL_CONTENA;
        public r_framework.CustomControl.CustomNumericTextBox2 PAGE_KBN;
        internal r_framework.CustomControl.CustomRadioButton PAGE_KBN_4;
        internal r_framework.CustomControl.CustomRadioButton PAGE_KBN_3;
        internal r_framework.CustomControl.CustomRadioButton PAGE_KBN_1;
        internal r_framework.CustomControl.CustomRadioButton PAGE_KBN_2;
        internal r_framework.CustomControl.CustomAlphaNumTextBox CONTENA_SHURUI_CD;
        internal r_framework.CustomControl.CustomTextBox CONTENA_SHURUI_NAME_RYAKU;
        internal r_framework.CustomControl.CustomAlphaNumTextBox CONTENA_CD;
        internal r_framework.CustomControl.CustomTextBox CONTENA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomCheckBox ALL_CHECKED;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn DET_CHECKED;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DET_CONTENA_SHURUI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DET_CONTENA_SHURUI_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DET_CONTENA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DET_CONTENA_NAME_RYAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column DET_SHOYUU_DAISUU;
        internal r_framework.CustomControl.CustomPanel PNL_PAGE;
    }
}