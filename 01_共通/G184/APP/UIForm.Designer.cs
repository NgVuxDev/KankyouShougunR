namespace Shougun.Core.Common.ContenaShitei
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labseichi = new System.Windows.Forms.Label();
            this.labHikiage = new System.Windows.Forms.Label();
            this.lbl_SechiTotal = new System.Windows.Forms.Label();
            this.lbl_HikiageTotal = new System.Windows.Forms.Label();
            this.ctxt_SechiTotal = new r_framework.CustomControl.CustomTextBox();
            this.ctxt_HikiageTotal = new r_framework.CustomControl.CustomTextBox();
            this.customDataGridViewSechi = new Shougun.Core.Common.ContenaShitei.DgvCustom();
            this.CONTENA_SHURUI_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.CONTENA_SHURUI_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CONTENA_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.CONTENA_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DAISUU_CNT = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.DELETE_FLG = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.TIME_STAMP = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.customDataGridViewHikiage = new Shougun.Core.Common.ContenaShitei.DgvCustom();
            this.CONTENA_SHURUI_CD_HIKIAGE = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CONTENA_CD_HIKIAGE = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.CONTENA_NAME_RYAKU_HIKIAGE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DAISUU_CNT_HIKIAGE = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.DELETE_FLG_HIKIAGE = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.TIME_STAMP_HIKIAGE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewSechi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewHikiage)).BeginInit();
            this.SuspendLayout();
            // 
            // labseichi
            // 
            this.labseichi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labseichi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labseichi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labseichi.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.labseichi.ForeColor = System.Drawing.Color.White;
            this.labseichi.Location = new System.Drawing.Point(0, 0);
            this.labseichi.Name = "labseichi";
            this.labseichi.Size = new System.Drawing.Size(110, 20);
            this.labseichi.TabIndex = 0;
            this.labseichi.Text = "設置";
            this.labseichi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labHikiage
            // 
            this.labHikiage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.labHikiage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.labHikiage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labHikiage.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.labHikiage.ForeColor = System.Drawing.Color.White;
            this.labHikiage.Location = new System.Drawing.Point(0, 241);
            this.labHikiage.Name = "labHikiage";
            this.labHikiage.Size = new System.Drawing.Size(110, 20);
            this.labHikiage.TabIndex = 5;
            this.labHikiage.Text = "引揚";
            this.labHikiage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_SechiTotal
            // 
            this.lbl_SechiTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_SechiTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SechiTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_SechiTotal.Font = new System.Drawing.Font("MS Gothic", 9.25F);
            this.lbl_SechiTotal.ForeColor = System.Drawing.Color.White;
            this.lbl_SechiTotal.Location = new System.Drawing.Point(762, 216);
            this.lbl_SechiTotal.Name = "lbl_SechiTotal";
            this.lbl_SechiTotal.Size = new System.Drawing.Size(66, 20);
            this.lbl_SechiTotal.TabIndex = 181;
            this.lbl_SechiTotal.Text = "設置合計";
            this.lbl_SechiTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_SechiTotal.Visible = false;
            // 
            // lbl_HikiageTotal
            // 
            this.lbl_HikiageTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_HikiageTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_HikiageTotal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_HikiageTotal.Font = new System.Drawing.Font("MS Gothic", 9.25F);
            this.lbl_HikiageTotal.ForeColor = System.Drawing.Color.White;
            this.lbl_HikiageTotal.Location = new System.Drawing.Point(762, 457);
            this.lbl_HikiageTotal.Name = "lbl_HikiageTotal";
            this.lbl_HikiageTotal.Size = new System.Drawing.Size(66, 20);
            this.lbl_HikiageTotal.TabIndex = 182;
            this.lbl_HikiageTotal.Text = "引揚合計";
            this.lbl_HikiageTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lbl_HikiageTotal.Visible = false;
            // 
            // ctxt_SechiTotal
            // 
            this.ctxt_SechiTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_SechiTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_SechiTotal.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_SechiTotal.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_SechiTotal.DisplayPopUp = null;
            this.ctxt_SechiTotal.Enabled = false;
            this.ctxt_SechiTotal.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_SechiTotal.FocusOutCheckMethod")));
            this.ctxt_SechiTotal.Font = new System.Drawing.Font("MS Gothic", 9.25F);
            this.ctxt_SechiTotal.ForeColor = System.Drawing.Color.Black;
            this.ctxt_SechiTotal.IsInputErrorOccured = false;
            this.ctxt_SechiTotal.Location = new System.Drawing.Point(834, 216);
            this.ctxt_SechiTotal.MaxLength = 0;
            this.ctxt_SechiTotal.Name = "ctxt_SechiTotal";
            this.ctxt_SechiTotal.PopupAfterExecute = null;
            this.ctxt_SechiTotal.PopupBeforeExecute = null;
            this.ctxt_SechiTotal.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_SechiTotal.PopupSearchSendParams")));
            this.ctxt_SechiTotal.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_SechiTotal.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_SechiTotal.popupWindowSetting")));
            this.ctxt_SechiTotal.ReadOnly = true;
            this.ctxt_SechiTotal.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_SechiTotal.RegistCheckMethod")));
            this.ctxt_SechiTotal.Size = new System.Drawing.Size(105, 20);
            this.ctxt_SechiTotal.TabIndex = 183;
            this.ctxt_SechiTotal.TabStop = false;
            this.ctxt_SechiTotal.Tag = "減容後数量の合計値が表示されます";
            this.ctxt_SechiTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ctxt_SechiTotal.Visible = false;
            // 
            // ctxt_HikiageTotal
            // 
            this.ctxt_HikiageTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_HikiageTotal.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_HikiageTotal.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_HikiageTotal.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_HikiageTotal.DisplayPopUp = null;
            this.ctxt_HikiageTotal.Enabled = false;
            this.ctxt_HikiageTotal.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HikiageTotal.FocusOutCheckMethod")));
            this.ctxt_HikiageTotal.Font = new System.Drawing.Font("MS Gothic", 9.25F);
            this.ctxt_HikiageTotal.ForeColor = System.Drawing.Color.Black;
            this.ctxt_HikiageTotal.IsInputErrorOccured = false;
            this.ctxt_HikiageTotal.Location = new System.Drawing.Point(834, 457);
            this.ctxt_HikiageTotal.MaxLength = 0;
            this.ctxt_HikiageTotal.Name = "ctxt_HikiageTotal";
            this.ctxt_HikiageTotal.PopupAfterExecute = null;
            this.ctxt_HikiageTotal.PopupBeforeExecute = null;
            this.ctxt_HikiageTotal.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_HikiageTotal.PopupSearchSendParams")));
            this.ctxt_HikiageTotal.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_HikiageTotal.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_HikiageTotal.popupWindowSetting")));
            this.ctxt_HikiageTotal.ReadOnly = true;
            this.ctxt_HikiageTotal.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HikiageTotal.RegistCheckMethod")));
            this.ctxt_HikiageTotal.Size = new System.Drawing.Size(105, 20);
            this.ctxt_HikiageTotal.TabIndex = 184;
            this.ctxt_HikiageTotal.TabStop = false;
            this.ctxt_HikiageTotal.Tag = "減容後数量の合計値が表示されます";
            this.ctxt_HikiageTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ctxt_HikiageTotal.Visible = false;
            // 
            // customDataGridViewSechi
            // 
            this.customDataGridViewSechi.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewSechi.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.customDataGridViewSechi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewSechi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CONTENA_SHURUI_CD,
            this.CONTENA_SHURUI_NAME_RYAKU,
            this.CONTENA_CD,
            this.CONTENA_NAME_RYAKU,
            this.DAISUU_CNT,
            this.DELETE_FLG,
            this.TIME_STAMP});
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewSechi.DefaultCellStyle = dataGridViewCellStyle9;
            this.customDataGridViewSechi.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.customDataGridViewSechi.EnableHeadersVisualStyles = false;
            this.customDataGridViewSechi.GridColor = System.Drawing.Color.White;
            this.customDataGridViewSechi.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.customDataGridViewSechi.IsReload = false;
            this.customDataGridViewSechi.LinkedDataPanelName = null;
            this.customDataGridViewSechi.Location = new System.Drawing.Point(0, 23);
            this.customDataGridViewSechi.MultiSelect = false;
            this.customDataGridViewSechi.Name = "customDataGridViewSechi";
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle10.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewSechi.RowHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.customDataGridViewSechi.RowHeadersVisible = false;
            this.customDataGridViewSechi.RowTemplate.Height = 21;
            this.customDataGridViewSechi.ShowCellToolTips = false;
            this.customDataGridViewSechi.Size = new System.Drawing.Size(702, 213);
            this.customDataGridViewSechi.TabIndex = 1;
            this.customDataGridViewSechi.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridViewSechi_CellEnter);
            this.customDataGridViewSechi.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridViewSechi_CellValidated);
            this.customDataGridViewSechi.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.customDataGridViewSechi_CellValidating);
            this.customDataGridViewSechi.MouseClick += new System.Windows.Forms.MouseEventHandler(this.customDataGridViewSechi_MouseClick);
            // 
            // CONTENA_SHURUI_CD
            // 
            this.CONTENA_SHURUI_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.CONTENA_SHURUI_CD.DBFieldsName = "CONTENA_SHURUI_CD";
            this.CONTENA_SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_SHURUI_CD.DefaultCellStyle = dataGridViewCellStyle2;
            this.CONTENA_SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_CD.GetCodeMasterField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_CD.HeaderText = "コンテナ種類CD";
            this.CONTENA_SHURUI_CD.MaxInputLength = 3;
            this.CONTENA_SHURUI_CD.Name = "CONTENA_SHURUI_CD";
            this.CONTENA_SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_CD.PopupSearchSendParams")));
            this.CONTENA_SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA_SHURUI;
            this.CONTENA_SHURUI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.CONTENA_SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_CD.popupWindowSetting")));
            this.CONTENA_SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD.RegistCheckMethod")));
            this.CONTENA_SHURUI_CD.SetFormField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_CD.ToolTipText = "コンテナ種類CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CONTENA_SHURUI_CD.Width = 120;
            this.CONTENA_SHURUI_CD.ZeroPaddengFlag = true;
            // 
            // CONTENA_SHURUI_NAME_RYAKU
            // 
            this.CONTENA_SHURUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_SHURUI_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle3;
            this.CONTENA_SHURUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU.HeaderText = "コンテナ種類名";
            this.CONTENA_SHURUI_NAME_RYAKU.Name = "CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.PopupSearchSendParams")));
            this.CONTENA_SHURUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.popupWindowSetting")));
            this.CONTENA_SHURUI_NAME_RYAKU.ReadOnly = true;
            this.CONTENA_SHURUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.RegistCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CONTENA_SHURUI_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_NAME_RYAKU.Width = 210;
            // 
            // CONTENA_CD
            // 
            this.CONTENA_CD.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CONTENA_CD.DBFieldsName = "CONTENA_CD";
            this.CONTENA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_CD.DefaultCellStyle = dataGridViewCellStyle4;
            this.CONTENA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_CD.FocusOutCheckMethod")));
            this.CONTENA_CD.GetCodeMasterField = "CONTENA_CD,CONTENA_NAME_RYAKU";
            this.CONTENA_CD.HeaderText = "コンテナCD";
            this.CONTENA_CD.MaxInputLength = 10;
            this.CONTENA_CD.Name = "CONTENA_CD";
            this.CONTENA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_CD.PopupSearchSendParams")));
            this.CONTENA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA;
            this.CONTENA_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.CONTENA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_CD.popupWindowSetting")));
            this.CONTENA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_CD.RegistCheckMethod")));
            this.CONTENA_CD.SetFormField = "CONTENA_CD,CONTENA_NAME_RYAKU";
            this.CONTENA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_CD.ToolTipText = "コンテナCDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CONTENA_CD.Visible = false;
            this.CONTENA_CD.ZeroPaddengFlag = true;
            // 
            // CONTENA_NAME_RYAKU
            // 
            this.CONTENA_NAME_RYAKU.DBFieldsName = "CONTENA_NAME_RYAKU";
            this.CONTENA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle5;
            this.CONTENA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_NAME_RYAKU.FocusOutCheckMethod")));
            this.CONTENA_NAME_RYAKU.HeaderText = "コンテナ名";
            this.CONTENA_NAME_RYAKU.Name = "CONTENA_NAME_RYAKU";
            this.CONTENA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_NAME_RYAKU.PopupSearchSendParams")));
            this.CONTENA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_NAME_RYAKU.popupWindowSetting")));
            this.CONTENA_NAME_RYAKU.ReadOnly = true;
            this.CONTENA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_NAME_RYAKU.RegistCheckMethod")));
            this.CONTENA_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_NAME_RYAKU.Visible = false;
            this.CONTENA_NAME_RYAKU.Width = 210;
            // 
            // DAISUU_CNT
            // 
            this.DAISUU_CNT.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.DAISUU_CNT.DefaultCellStyle = dataGridViewCellStyle6;
            this.DAISUU_CNT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAISUU_CNT.FocusOutCheckMethod")));
            this.DAISUU_CNT.HeaderText = "台数";
            this.DAISUU_CNT.Name = "DAISUU_CNT";
            this.DAISUU_CNT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAISUU_CNT.PopupSearchSendParams")));
            this.DAISUU_CNT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAISUU_CNT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAISUU_CNT.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.DAISUU_CNT.RangeSetting = rangeSettingDto1;
            this.DAISUU_CNT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAISUU_CNT.RegistCheckMethod")));
            this.DAISUU_CNT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DAISUU_CNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DAISUU_CNT.ToolTipText = "1~999の値を入力してください";
            this.DAISUU_CNT.Width = 60;
            // 
            // DELETE_FLG
            // 
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle7.NullValue = false;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.DELETE_FLG.DefaultCellStyle = dataGridViewCellStyle7;
            this.DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.FocusOutCheckMethod")));
            this.DELETE_FLG.HeaderText = "削除フラグ";
            this.DELETE_FLG.Name = "DELETE_FLG";
            this.DELETE_FLG.ReadOnly = true;
            this.DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG.RegistCheckMethod")));
            this.DELETE_FLG.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DELETE_FLG.Visible = false;
            // 
            // TIME_STAMP
            // 
            this.TIME_STAMP.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.TIME_STAMP.DefaultCellStyle = dataGridViewCellStyle8;
            this.TIME_STAMP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP.FocusOutCheckMethod")));
            this.TIME_STAMP.HeaderText = "TIME_STAMP";
            this.TIME_STAMP.Name = "TIME_STAMP";
            this.TIME_STAMP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TIME_STAMP.PopupSearchSendParams")));
            this.TIME_STAMP.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TIME_STAMP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TIME_STAMP.popupWindowSetting")));
            this.TIME_STAMP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP.RegistCheckMethod")));
            this.TIME_STAMP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TIME_STAMP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIME_STAMP.Visible = false;
            // 
            // customDataGridViewHikiage
            // 
            this.customDataGridViewHikiage.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle11.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewHikiage.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle11;
            this.customDataGridViewHikiage.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.customDataGridViewHikiage.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CONTENA_SHURUI_CD_HIKIAGE,
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE,
            this.CONTENA_CD_HIKIAGE,
            this.CONTENA_NAME_RYAKU_HIKIAGE,
            this.DAISUU_CNT_HIKIAGE,
            this.DELETE_FLG_HIKIAGE,
            this.TIME_STAMP_HIKIAGE});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.customDataGridViewHikiage.DefaultCellStyle = dataGridViewCellStyle19;
            this.customDataGridViewHikiage.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.customDataGridViewHikiage.EnableHeadersVisualStyles = false;
            this.customDataGridViewHikiage.GridColor = System.Drawing.Color.White;
            this.customDataGridViewHikiage.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.customDataGridViewHikiage.IsReload = false;
            this.customDataGridViewHikiage.LinkedDataPanelName = null;
            this.customDataGridViewHikiage.Location = new System.Drawing.Point(0, 264);
            this.customDataGridViewHikiage.MultiSelect = false;
            this.customDataGridViewHikiage.Name = "customDataGridViewHikiage";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.customDataGridViewHikiage.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.customDataGridViewHikiage.RowHeadersVisible = false;
            this.customDataGridViewHikiage.RowTemplate.Height = 21;
            this.customDataGridViewHikiage.ShowCellToolTips = false;
            this.customDataGridViewHikiage.Size = new System.Drawing.Size(702, 213);
            this.customDataGridViewHikiage.TabIndex = 4;
            this.customDataGridViewHikiage.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridViewHikiage_CellEnter);
            this.customDataGridViewHikiage.CellValidated += new System.Windows.Forms.DataGridViewCellEventHandler(this.customDataGridViewHikiage_CellValidated);
            this.customDataGridViewHikiage.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.customDataGridViewHikiage_CellValidating);
            this.customDataGridViewHikiage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.customDataGridViewHikiage_MouseClick);
            // 
            // CONTENA_SHURUI_CD_HIKIAGE
            // 
            this.CONTENA_SHURUI_CD_HIKIAGE.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.CONTENA_SHURUI_CD_HIKIAGE.DBFieldsName = "CONTENA_SHURUI_CD_HIKIAGE";
            this.CONTENA_SHURUI_CD_HIKIAGE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_SHURUI_CD_HIKIAGE.DefaultCellStyle = dataGridViewCellStyle12;
            this.CONTENA_SHURUI_CD_HIKIAGE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD_HIKIAGE.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_CD_HIKIAGE.GetCodeMasterField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_CD_HIKIAGE.HeaderText = "コンテナ種類CD";
            this.CONTENA_SHURUI_CD_HIKIAGE.MaxInputLength = 3;
            this.CONTENA_SHURUI_CD_HIKIAGE.Name = "CONTENA_SHURUI_CD_HIKIAGE";
            this.CONTENA_SHURUI_CD_HIKIAGE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_CD_HIKIAGE.PopupSearchSendParams")));
            this.CONTENA_SHURUI_CD_HIKIAGE.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA_SHURUI;
            this.CONTENA_SHURUI_CD_HIKIAGE.PopupWindowName = "マスタ共通ポップアップ";
            this.CONTENA_SHURUI_CD_HIKIAGE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_CD_HIKIAGE.popupWindowSetting")));
            this.CONTENA_SHURUI_CD_HIKIAGE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD_HIKIAGE.RegistCheckMethod")));
            this.CONTENA_SHURUI_CD_HIKIAGE.SetFormField = "CONTENA_SHURUI_CD_HIKIAGE,CONTENA_SHURUI_NAME_RYAKU_HIKIAGE";
            this.CONTENA_SHURUI_CD_HIKIAGE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_CD_HIKIAGE.ToolTipText = "コンテナ種類CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CONTENA_SHURUI_CD_HIKIAGE.Width = 120;
            this.CONTENA_SHURUI_CD_HIKIAGE.ZeroPaddengFlag = true;
            // 
            // CONTENA_SHURUI_NAME_RYAKU_HIKIAGE
            // 
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.DBFieldsName = "CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle13.Format = "N0";
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.DefaultCellStyle = dataGridViewCellStyle13;
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.HeaderText = "コンテナ種類名";
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.Name = "CONTENA_SHURUI_NAME_RYAKU_HIKIAGE";
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.PopupSearchSendParams")));
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.popupWindowSetting")));
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.ReadOnly = true;
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.RegistCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_NAME_RYAKU_HIKIAGE.Width = 210;
            // 
            // CONTENA_CD_HIKIAGE
            // 
            this.CONTENA_CD_HIKIAGE.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CONTENA_CD_HIKIAGE.DBFieldsName = "CONTENA_CD_HIKIAGE";
            this.CONTENA_CD_HIKIAGE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle14.Format = "N0";
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_CD_HIKIAGE.DefaultCellStyle = dataGridViewCellStyle14;
            this.CONTENA_CD_HIKIAGE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_CD_HIKIAGE.FocusOutCheckMethod")));
            this.CONTENA_CD_HIKIAGE.GetCodeMasterField = "CONTENA_CD,CONTENA_NAME_RYAKU,CONTENA_SHURUI_CD";
            this.CONTENA_CD_HIKIAGE.HeaderText = "コンテナCD";
            this.CONTENA_CD_HIKIAGE.MaxInputLength = 10;
            this.CONTENA_CD_HIKIAGE.Name = "CONTENA_CD_HIKIAGE";
            this.CONTENA_CD_HIKIAGE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_CD_HIKIAGE.PopupSearchSendParams")));
            this.CONTENA_CD_HIKIAGE.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA;
            this.CONTENA_CD_HIKIAGE.PopupWindowName = "マスタ共通ポップアップ";
            this.CONTENA_CD_HIKIAGE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_CD_HIKIAGE.popupWindowSetting")));
            this.CONTENA_CD_HIKIAGE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_CD_HIKIAGE.RegistCheckMethod")));
            this.CONTENA_CD_HIKIAGE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CONTENA_CD_HIKIAGE.SetFormField = "CONTENA_CD_HIKIAGE,CONTENA_NAME_RYAKU_HIKIAGE";
            this.CONTENA_CD_HIKIAGE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_CD_HIKIAGE.ToolTipText = "コンテナCDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CONTENA_CD_HIKIAGE.Visible = false;
            this.CONTENA_CD_HIKIAGE.ZeroPaddengFlag = true;
            // 
            // CONTENA_NAME_RYAKU_HIKIAGE
            // 
            this.CONTENA_NAME_RYAKU_HIKIAGE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_NAME_RYAKU_HIKIAGE.DefaultCellStyle = dataGridViewCellStyle15;
            this.CONTENA_NAME_RYAKU_HIKIAGE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_NAME_RYAKU_HIKIAGE.FocusOutCheckMethod")));
            this.CONTENA_NAME_RYAKU_HIKIAGE.HeaderText = "コンテナ名";
            this.CONTENA_NAME_RYAKU_HIKIAGE.Name = "CONTENA_NAME_RYAKU_HIKIAGE";
            this.CONTENA_NAME_RYAKU_HIKIAGE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_NAME_RYAKU_HIKIAGE.PopupSearchSendParams")));
            this.CONTENA_NAME_RYAKU_HIKIAGE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_NAME_RYAKU_HIKIAGE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_NAME_RYAKU_HIKIAGE.popupWindowSetting")));
            this.CONTENA_NAME_RYAKU_HIKIAGE.ReadOnly = true;
            this.CONTENA_NAME_RYAKU_HIKIAGE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_NAME_RYAKU_HIKIAGE.RegistCheckMethod")));
            this.CONTENA_NAME_RYAKU_HIKIAGE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CONTENA_NAME_RYAKU_HIKIAGE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_NAME_RYAKU_HIKIAGE.Visible = false;
            this.CONTENA_NAME_RYAKU_HIKIAGE.Width = 210;
            // 
            // DAISUU_CNT_HIKIAGE
            // 
            this.DAISUU_CNT_HIKIAGE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.DAISUU_CNT_HIKIAGE.DefaultCellStyle = dataGridViewCellStyle16;
            this.DAISUU_CNT_HIKIAGE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAISUU_CNT_HIKIAGE.FocusOutCheckMethod")));
            this.DAISUU_CNT_HIKIAGE.HeaderText = "台数";
            this.DAISUU_CNT_HIKIAGE.Name = "DAISUU_CNT_HIKIAGE";
            this.DAISUU_CNT_HIKIAGE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAISUU_CNT_HIKIAGE.PopupSearchSendParams")));
            this.DAISUU_CNT_HIKIAGE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAISUU_CNT_HIKIAGE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAISUU_CNT_HIKIAGE.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.DAISUU_CNT_HIKIAGE.RangeSetting = rangeSettingDto2;
            this.DAISUU_CNT_HIKIAGE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAISUU_CNT_HIKIAGE.RegistCheckMethod")));
            this.DAISUU_CNT_HIKIAGE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DAISUU_CNT_HIKIAGE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DAISUU_CNT_HIKIAGE.ToolTipText = "1~999の値を入力してください";
            this.DAISUU_CNT_HIKIAGE.Width = 60;
            // 
            // DELETE_FLG_HIKIAGE
            // 
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle17.NullValue = false;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            this.DELETE_FLG_HIKIAGE.DefaultCellStyle = dataGridViewCellStyle17;
            this.DELETE_FLG_HIKIAGE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG_HIKIAGE.FocusOutCheckMethod")));
            this.DELETE_FLG_HIKIAGE.HeaderText = "削除フラグ";
            this.DELETE_FLG_HIKIAGE.Name = "DELETE_FLG_HIKIAGE";
            this.DELETE_FLG_HIKIAGE.ReadOnly = true;
            this.DELETE_FLG_HIKIAGE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DELETE_FLG_HIKIAGE.RegistCheckMethod")));
            this.DELETE_FLG_HIKIAGE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DELETE_FLG_HIKIAGE.Visible = false;
            // 
            // TIME_STAMP_HIKIAGE
            // 
            this.TIME_STAMP_HIKIAGE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            this.TIME_STAMP_HIKIAGE.DefaultCellStyle = dataGridViewCellStyle18;
            this.TIME_STAMP_HIKIAGE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP_HIKIAGE.FocusOutCheckMethod")));
            this.TIME_STAMP_HIKIAGE.HeaderText = "TIME_STAMP";
            this.TIME_STAMP_HIKIAGE.Name = "TIME_STAMP_HIKIAGE";
            this.TIME_STAMP_HIKIAGE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TIME_STAMP_HIKIAGE.PopupSearchSendParams")));
            this.TIME_STAMP_HIKIAGE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TIME_STAMP_HIKIAGE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TIME_STAMP_HIKIAGE.popupWindowSetting")));
            this.TIME_STAMP_HIKIAGE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP_HIKIAGE.RegistCheckMethod")));
            this.TIME_STAMP_HIKIAGE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIME_STAMP_HIKIAGE.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.ctxt_HikiageTotal);
            this.Controls.Add(this.ctxt_SechiTotal);
            this.Controls.Add(this.lbl_HikiageTotal);
            this.Controls.Add(this.lbl_SechiTotal);
            this.Controls.Add(this.labHikiage);
            this.Controls.Add(this.customDataGridViewSechi);
            this.Controls.Add(this.customDataGridViewHikiage);
            this.Controls.Add(this.labseichi);
            this.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Shown += new System.EventHandler(this.UIForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewSechi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customDataGridViewHikiage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labseichi;
        private System.Windows.Forms.Label labHikiage;
        public DgvCustom customDataGridViewHikiage;
        public DgvCustom customDataGridViewSechi;
        internal System.Windows.Forms.Label lbl_SechiTotal;
        internal System.Windows.Forms.Label lbl_HikiageTotal;
        internal r_framework.CustomControl.CustomTextBox ctxt_SechiTotal;
        internal r_framework.CustomControl.CustomTextBox ctxt_HikiageTotal;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn CONTENA_SHURUI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_SHURUI_NAME_RYAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn CONTENA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_NAME_RYAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column DAISUU_CNT;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn DELETE_FLG;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TIME_STAMP;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn CONTENA_SHURUI_CD_HIKIAGE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_SHURUI_NAME_RYAKU_HIKIAGE;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn CONTENA_CD_HIKIAGE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_NAME_RYAKU_HIKIAGE;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column DAISUU_CNT_HIKIAGE;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn DELETE_FLG_HIKIAGE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TIME_STAMP_HIKIAGE;
    }
}