using System.Windows.Forms;
namespace Shougun.Core.Master.ZaikoHiritsuHoshu.APP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
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
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new r_framework.CustomControl.CustomCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label55 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.HINMEI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.btnSearchHinmei = new r_framework.CustomControl.CustomPopupOpenButton();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.DELETE_FLG = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.ZAIKO_HINMEI_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.ZAIKO_HINMEI_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.ZAIKO_HIRITSU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.ZAIKO_HIRITSU_UNIT = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.BIKOU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_PC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPDATE_PC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIME_STAMP = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DENSHUKBNCD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HINMEICD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UK_DENSHU_KBN_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.UK_HINMEI_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UK_ZAIKO_HINMEI_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.panel_HidukeSentaku = new r_framework.CustomControl.CustomPanel();
            this.DENSHU_KBN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.radioButton2 = new r_framework.CustomControl.CustomRadioButton();
            this.radioButton1 = new r_framework.CustomControl.CustomRadioButton();
            this.dgvCustomCheckBoxColumn1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.HINMEI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.panel_HidukeSentaku.SuspendLayout();
            this.SuspendLayout();
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.DefaultBackColor = System.Drawing.Color.Empty;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.FocusOutCheckMethod")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(122, 24);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Name = "ICHIRAN_HYOUJI_JOUKEN_DELETED";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupAfterExecute = null;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupBeforeExecute = null;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupSearchSendParams")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.popupWindowSetting")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.RegistCheckMethod")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Size = new System.Drawing.Size(180, 17);
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.TabIndex = 14;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Tag = "削除されたデータを対象とする場合チェックを付けてください";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Text = "削除済も含めて全て表示";
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.UseVisualStyleBackColor = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(1, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 600;
            this.label1.Text = "表示条件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label55
            // 
            this.label55.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label55.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label55.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label55.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label55.ForeColor = System.Drawing.Color.White;
            this.label55.Location = new System.Drawing.Point(640, 0);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(110, 20);
            this.label55.TabIndex = 400;
            this.label55.Text = "受入/出荷※";
            this.label55.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label55.Visible = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 300;
            this.label2.Text = "品名※";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HINMEI_NAME_RYAKU
            // 
            this.HINMEI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HINMEI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.HINMEI_NAME_RYAKU.DBFieldsName = "HINMEI_NAME_RYAKU";
            this.HINMEI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_NAME_RYAKU.DisplayItemName = "品名";
            this.HINMEI_NAME_RYAKU.DisplayPopUp = null;
            this.HINMEI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME_RYAKU.FocusOutCheckMethod")));
            this.HINMEI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HINMEI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HINMEI_NAME_RYAKU.IsInputErrorOccured = false;
            this.HINMEI_NAME_RYAKU.Location = new System.Drawing.Point(170, 0);
            this.HINMEI_NAME_RYAKU.MaxLength = 0;
            this.HINMEI_NAME_RYAKU.Name = "HINMEI_NAME_RYAKU";
            this.HINMEI_NAME_RYAKU.PopupAfterExecute = null;
            this.HINMEI_NAME_RYAKU.PopupBeforeExecute = null;
            this.HINMEI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_NAME_RYAKU.PopupSearchSendParams")));
            this.HINMEI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HINMEI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_NAME_RYAKU.popupWindowSetting")));
            this.HINMEI_NAME_RYAKU.ReadOnly = true;
            this.HINMEI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_NAME_RYAKU.RegistCheckMethod")));
            this.HINMEI_NAME_RYAKU.Size = new System.Drawing.Size(173, 20);
            this.HINMEI_NAME_RYAKU.TabIndex = 3;
            this.HINMEI_NAME_RYAKU.TabStop = false;
            this.HINMEI_NAME_RYAKU.Tag = "";
            // 
            // btnSearchHinmei
            // 
            this.btnSearchHinmei.BackColor = System.Drawing.SystemColors.Control;
            this.btnSearchHinmei.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btnSearchHinmei.DBFieldsName = null;
            this.btnSearchHinmei.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnSearchHinmei.DisplayItemName = null;
            this.btnSearchHinmei.DisplayPopUp = null;
            this.btnSearchHinmei.ErrorMessage = null;
            this.btnSearchHinmei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnSearchHinmei.FocusOutCheckMethod")));
            this.btnSearchHinmei.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btnSearchHinmei.GetCodeMasterField = null;
            this.btnSearchHinmei.Image = ((System.Drawing.Image)(resources.GetObject("btnSearchHinmei.Image")));
            this.btnSearchHinmei.ItemDefinedTypes = null;
            this.btnSearchHinmei.LinkedSettingTextBox = null;
            this.btnSearchHinmei.LinkedTextBoxs = new string[] {
        "GyoushaCode"};
            this.btnSearchHinmei.Location = new System.Drawing.Point(348, -1);
            this.btnSearchHinmei.Name = "btnSearchHinmei";
            this.btnSearchHinmei.PopupAfterExecute = null;
            this.btnSearchHinmei.PopupBeforeExecute = null;
            this.btnSearchHinmei.PopupGetMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.btnSearchHinmei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btnSearchHinmei.PopupSearchSendParams")));
            this.btnSearchHinmei.PopupSetFormField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.btnSearchHinmei.PopupWindowId = r_framework.Const.WINDOW_ID.M_HINMEI;
            this.btnSearchHinmei.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.btnSearchHinmei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btnSearchHinmei.popupWindowSetting")));
            this.btnSearchHinmei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btnSearchHinmei.RegistCheckMethod")));
            this.btnSearchHinmei.SearchDisplayFlag = 0;
            this.btnSearchHinmei.SetFormField = null;
            this.btnSearchHinmei.ShortItemName = null;
            this.btnSearchHinmei.Size = new System.Drawing.Size(22, 22);
            this.btnSearchHinmei.TabIndex = 4;
            this.btnSearchHinmei.TabStop = false;
            this.btnSearchHinmei.UseVisualStyleBackColor = false;
            this.btnSearchHinmei.ZeroPaddengFlag = false;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToDeleteRows = false;
            this.Ichiran.AllowUserToResizeColumns = false;
            this.Ichiran.AllowUserToResizeRows = false;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.Ichiran.ColumnHeadersHeight = 21;
            this.Ichiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Ichiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DELETE_FLG,
            this.ZAIKO_HINMEI_CD,
            this.ZAIKO_HINMEI_NAME,
            this.ZAIKO_HIRITSU,
            this.ZAIKO_HIRITSU_UNIT,
            this.BIKOU,
            this.UPDATE_USER,
            this.UPDATE_DATE,
            this.CREATE_USER,
            this.CREATE_DATE,
            this.CREATE_PC,
            this.UPDATE_PC,
            this.TIME_STAMP,
            this.DENSHUKBNCD,
            this.HINMEICD,
            this.UK_DENSHU_KBN_CD,
            this.UK_HINMEI_CD,
            this.UK_ZAIKO_HINMEI_CD});
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle20;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = null;
            this.Ichiran.Location = new System.Drawing.Point(1, 63);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle21.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle21;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(990, 384);
            this.Ichiran.TabIndex = 16;
            this.Ichiran.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ichiran_CellEnter);
            this.Ichiran.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.Ichiran_CellPainting);
            this.Ichiran.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.Ichiran_CellParsing);
            this.Ichiran.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Ichiran_CellValidating);
            this.Ichiran.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.Ichiran_ColumnWidthChanged);
            this.Ichiran.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Ichiran_EditingControlShowing);
            // 
            // DELETE_FLG
            // 
            this.DELETE_FLG.DataPropertyName = "DELETE_FLG";
            this.DELETE_FLG.DBFieldsName = "DELETE_FLG";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.DELETE_FLG.DefaultCellStyle = dataGridViewCellStyle2;
            this.DELETE_FLG.FocusOutCheckMethod = null;
            this.DELETE_FLG.HeaderText = "削除";
            this.DELETE_FLG.ItemDefinedTypes = "bit";
            this.DELETE_FLG.Name = "DELETE_FLG";
            this.DELETE_FLG.RegistCheckMethod = null;
            this.DELETE_FLG.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DELETE_FLG.ToolTipText = "削除する場合、チェックしてください";
            this.DELETE_FLG.ViewSearchItem = false;
            this.DELETE_FLG.Width = 43;
            // 
            // ZAIKO_HINMEI_CD
            // 
            this.ZAIKO_HINMEI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.ZAIKO_HINMEI_CD.DataPropertyName = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD.DBFieldsName = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_CD.DefaultCellStyle = dataGridViewCellStyle3;
            this.ZAIKO_HINMEI_CD.DisplayItemName = "在庫品名CD";
            this.ZAIKO_HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_CD.GetCodeMasterField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_CD.HeaderText = "在庫品名CD";
            this.ZAIKO_HINMEI_CD.ItemDefinedTypes = "varchar";
            this.ZAIKO_HINMEI_CD.MaxInputLength = 6;
            this.ZAIKO_HINMEI_CD.Name = "ZAIKO_HINMEI_CD";
            this.ZAIKO_HINMEI_CD.PopupAfterExecuteMethod = "";
            this.ZAIKO_HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_CD.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_ZAIKO_HINMEI;
            this.ZAIKO_HINMEI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.ZAIKO_HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_CD.popupWindowSetting")));
            this.ZAIKO_HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_CD.RegistCheckMethod")));
            this.ZAIKO_HINMEI_CD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ZAIKO_HINMEI_CD.SetFormField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_HINMEI_CD.ToolTipText = "在庫品名を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.ZAIKO_HINMEI_CD.Width = 110;
            this.ZAIKO_HINMEI_CD.ZeroPaddengFlag = true;
            // 
            // ZAIKO_HINMEI_NAME
            // 
            this.ZAIKO_HINMEI_NAME.DataPropertyName = "ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_NAME.DBFieldsName = "ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_NAME.DefaultCellStyle = dataGridViewCellStyle4;
            this.ZAIKO_HINMEI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_NAME.HeaderText = "在庫品名";
            this.ZAIKO_HINMEI_NAME.ItemDefinedTypes = "varchar";
            this.ZAIKO_HINMEI_NAME.MaxInputLength = 40;
            this.ZAIKO_HINMEI_NAME.Name = "ZAIKO_HINMEI_NAME";
            this.ZAIKO_HINMEI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_HINMEI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.popupWindowSetting")));
            this.ZAIKO_HINMEI_NAME.ReadOnly = true;
            this.ZAIKO_HINMEI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME.RegistCheckMethod")));
            this.ZAIKO_HINMEI_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ZAIKO_HINMEI_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_HINMEI_NAME.Width = 290;
            // 
            // ZAIKO_HIRITSU
            // 
            this.ZAIKO_HIRITSU.DataPropertyName = "ZAIKO_HIRITSU";
            this.ZAIKO_HIRITSU.DBFieldsName = "ZAIKO_HIRITSU";
            this.ZAIKO_HIRITSU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HIRITSU.DefaultCellStyle = dataGridViewCellStyle5;
            this.ZAIKO_HIRITSU.DisplayItemName = "比率";
            this.ZAIKO_HIRITSU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HIRITSU.FocusOutCheckMethod")));
            this.ZAIKO_HIRITSU.HeaderText = "比率";
            this.ZAIKO_HIRITSU.ItemDefinedTypes = "smallint";
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
            this.ZAIKO_HIRITSU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HIRITSU.RegistCheckMethod")));
            this.ZAIKO_HIRITSU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ZAIKO_HIRITSU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_HIRITSU.ToolTipText = "半角3桁以内で入力してください";
            this.ZAIKO_HIRITSU.Width = 43;
            // 
            // ZAIKO_HIRITSU_UNIT
            // 
            this.ZAIKO_HIRITSU_UNIT.DataPropertyName = "ZAIKO_HIRITSU_UNIT";
            this.ZAIKO_HIRITSU_UNIT.DBFieldsName = "ZAIKO_HIRITSU_UNIT";
            this.ZAIKO_HIRITSU_UNIT.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle6.NullValue = "%";
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HIRITSU_UNIT.DefaultCellStyle = dataGridViewCellStyle6;
            this.ZAIKO_HIRITSU_UNIT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HIRITSU_UNIT.FocusOutCheckMethod")));
            this.ZAIKO_HIRITSU_UNIT.HeaderText = "比率単位";
            this.ZAIKO_HIRITSU_UNIT.ItemDefinedTypes = "varchar";
            this.ZAIKO_HIRITSU_UNIT.MaxInputLength = 1;
            this.ZAIKO_HIRITSU_UNIT.Name = "ZAIKO_HIRITSU_UNIT";
            this.ZAIKO_HIRITSU_UNIT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HIRITSU_UNIT.PopupSearchSendParams")));
            this.ZAIKO_HIRITSU_UNIT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_HIRITSU_UNIT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HIRITSU_UNIT.popupWindowSetting")));
            this.ZAIKO_HIRITSU_UNIT.ReadOnly = true;
            this.ZAIKO_HIRITSU_UNIT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HIRITSU_UNIT.RegistCheckMethod")));
            this.ZAIKO_HIRITSU_UNIT.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ZAIKO_HIRITSU_UNIT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_HIRITSU_UNIT.ViewSearchItem = false;
            this.ZAIKO_HIRITSU_UNIT.Width = 23;
            // 
            // BIKOU
            // 
            this.BIKOU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.BIKOU.DataPropertyName = "BIKOU";
            this.BIKOU.DBFieldsName = "BIKOU";
            this.BIKOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.BIKOU.DefaultCellStyle = dataGridViewCellStyle7;
            this.BIKOU.DisplayItemName = "備考";
            this.BIKOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKOU.FocusOutCheckMethod")));
            this.BIKOU.HeaderText = "備考";
            this.BIKOU.ItemDefinedTypes = "varchar";
            this.BIKOU.MaxInputLength = 20;
            this.BIKOU.Name = "BIKOU";
            this.BIKOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("BIKOU.PopupSearchSendParams")));
            this.BIKOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.BIKOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("BIKOU.popupWindowSetting")));
            this.BIKOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("BIKOU.RegistCheckMethod")));
            this.BIKOU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.BIKOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.BIKOU.ToolTipText = "備考は全角10文字以内で入力してください。";
            this.BIKOU.Width = 150;
            // 
            // UPDATE_USER
            // 
            this.UPDATE_USER.DataPropertyName = "UPDATE_USER";
            this.UPDATE_USER.DBFieldsName = "UPDATE_USER";
            this.UPDATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.UPDATE_USER.DefaultCellStyle = dataGridViewCellStyle8;
            this.UPDATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.FocusOutCheckMethod")));
            this.UPDATE_USER.HeaderText = "更新者";
            this.UPDATE_USER.ItemDefinedTypes = "varchar";
            this.UPDATE_USER.MaxInputLength = 16;
            this.UPDATE_USER.Name = "UPDATE_USER";
            this.UPDATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_USER.PopupSearchSendParams")));
            this.UPDATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_USER.popupWindowSetting")));
            this.UPDATE_USER.ReadOnly = true;
            this.UPDATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_USER.RegistCheckMethod")));
            this.UPDATE_USER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UPDATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_USER.Width = 120;
            // 
            // UPDATE_DATE
            // 
            this.UPDATE_DATE.DataPropertyName = "UPDATE_DATE";
            this.UPDATE_DATE.DBFieldsName = "UPDATE_DATE";
            this.UPDATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.Format = "G";
            dataGridViewCellStyle9.NullValue = null;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.UPDATE_DATE.DefaultCellStyle = dataGridViewCellStyle9;
            this.UPDATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.FocusOutCheckMethod")));
            this.UPDATE_DATE.HeaderText = "更新日";
            this.UPDATE_DATE.ItemDefinedTypes = "datetime";
            this.UPDATE_DATE.MaxInputLength = 18;
            this.UPDATE_DATE.Name = "UPDATE_DATE";
            this.UPDATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UPDATE_DATE.PopupSearchSendParams")));
            this.UPDATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UPDATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UPDATE_DATE.popupWindowSetting")));
            this.UPDATE_DATE.ReadOnly = true;
            this.UPDATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UPDATE_DATE.RegistCheckMethod")));
            this.UPDATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.UPDATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_DATE.Width = 145;
            // 
            // CREATE_USER
            // 
            this.CREATE_USER.DataPropertyName = "CREATE_USER";
            this.CREATE_USER.DBFieldsName = "CREATE_USER";
            this.CREATE_USER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_USER.DefaultCellStyle = dataGridViewCellStyle10;
            this.CREATE_USER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.FocusOutCheckMethod")));
            this.CREATE_USER.HeaderText = "作成者";
            this.CREATE_USER.ItemDefinedTypes = "varchar";
            this.CREATE_USER.MaxInputLength = 16;
            this.CREATE_USER.Name = "CREATE_USER";
            this.CREATE_USER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_USER.PopupSearchSendParams")));
            this.CREATE_USER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_USER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_USER.popupWindowSetting")));
            this.CREATE_USER.ReadOnly = true;
            this.CREATE_USER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_USER.RegistCheckMethod")));
            this.CREATE_USER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_USER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_USER.Width = 120;
            // 
            // CREATE_DATE
            // 
            this.CREATE_DATE.DataPropertyName = "CREATE_DATE";
            this.CREATE_DATE.DBFieldsName = "CREATE_DATE";
            this.CREATE_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.Format = "G";
            dataGridViewCellStyle11.NullValue = null;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_DATE.DefaultCellStyle = dataGridViewCellStyle11;
            this.CREATE_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.FocusOutCheckMethod")));
            this.CREATE_DATE.HeaderText = "作成日";
            this.CREATE_DATE.ItemDefinedTypes = "datetime";
            this.CREATE_DATE.MaxInputLength = 18;
            this.CREATE_DATE.Name = "CREATE_DATE";
            this.CREATE_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CREATE_DATE.PopupSearchSendParams")));
            this.CREATE_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CREATE_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CREATE_DATE.popupWindowSetting")));
            this.CREATE_DATE.ReadOnly = true;
            this.CREATE_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CREATE_DATE.RegistCheckMethod")));
            this.CREATE_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CREATE_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_DATE.Width = 145;
            // 
            // CREATE_PC
            // 
            this.CREATE_PC.DataPropertyName = "CREATE_PC";
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.CREATE_PC.DefaultCellStyle = dataGridViewCellStyle12;
            this.CREATE_PC.HeaderText = "CREATE_PC";
            this.CREATE_PC.Name = "CREATE_PC";
            this.CREATE_PC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CREATE_PC.Visible = false;
            // 
            // UPDATE_PC
            // 
            this.UPDATE_PC.DataPropertyName = "UPDATE_PC";
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.UPDATE_PC.DefaultCellStyle = dataGridViewCellStyle13;
            this.UPDATE_PC.HeaderText = "UPDATE_PC";
            this.UPDATE_PC.Name = "UPDATE_PC";
            this.UPDATE_PC.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UPDATE_PC.Visible = false;
            // 
            // TIME_STAMP
            // 
            this.TIME_STAMP.DataPropertyName = "TIME_STAMP";
            this.TIME_STAMP.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.TIME_STAMP.DefaultCellStyle = dataGridViewCellStyle14;
            this.TIME_STAMP.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP.FocusOutCheckMethod")));
            this.TIME_STAMP.HeaderText = "TIME_STAMP";
            this.TIME_STAMP.ItemDefinedTypes = "timestamp";
            this.TIME_STAMP.Name = "TIME_STAMP";
            this.TIME_STAMP.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TIME_STAMP.PopupSearchSendParams")));
            this.TIME_STAMP.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TIME_STAMP.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TIME_STAMP.popupWindowSetting")));
            this.TIME_STAMP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP.RegistCheckMethod")));
            this.TIME_STAMP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TIME_STAMP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIME_STAMP.Visible = false;
            // 
            // DENSHUKBNCD
            // 
            this.DENSHUKBNCD.DataPropertyName = "DENSHUKBNCD";
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.DENSHUKBNCD.DefaultCellStyle = dataGridViewCellStyle15;
            this.DENSHUKBNCD.HeaderText = "DENSHUKBNCD";
            this.DENSHUKBNCD.Name = "DENSHUKBNCD";
            this.DENSHUKBNCD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DENSHUKBNCD.Visible = false;
            // 
            // HINMEICD
            // 
            this.HINMEICD.DataPropertyName = "HINMEICD";
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.HINMEICD.DefaultCellStyle = dataGridViewCellStyle16;
            this.HINMEICD.HeaderText = "HINMEICD";
            this.HINMEICD.Name = "HINMEICD";
            this.HINMEICD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.HINMEICD.Visible = false;
            // 
            // UK_DENSHU_KBN_CD
            // 
            this.UK_DENSHU_KBN_CD.DataPropertyName = "UK_DENSHU_KBN_CD";
            this.UK_DENSHU_KBN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            this.UK_DENSHU_KBN_CD.DefaultCellStyle = dataGridViewCellStyle17;
            this.UK_DENSHU_KBN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UK_DENSHU_KBN_CD.FocusOutCheckMethod")));
            this.UK_DENSHU_KBN_CD.HeaderText = "UK_DENSHU_KBN_CD";
            this.UK_DENSHU_KBN_CD.Name = "UK_DENSHU_KBN_CD";
            this.UK_DENSHU_KBN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UK_DENSHU_KBN_CD.PopupSearchSendParams")));
            this.UK_DENSHU_KBN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UK_DENSHU_KBN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UK_DENSHU_KBN_CD.popupWindowSetting")));
            this.UK_DENSHU_KBN_CD.RangeSetting = rangeSettingDto2;
            this.UK_DENSHU_KBN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UK_DENSHU_KBN_CD.RegistCheckMethod")));
            this.UK_DENSHU_KBN_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UK_DENSHU_KBN_CD.Visible = false;
            // 
            // UK_HINMEI_CD
            // 
            this.UK_HINMEI_CD.DataPropertyName = "UK_HINMEI_CD";
            this.UK_HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            this.UK_HINMEI_CD.DefaultCellStyle = dataGridViewCellStyle18;
            this.UK_HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UK_HINMEI_CD.FocusOutCheckMethod")));
            this.UK_HINMEI_CD.HeaderText = "UK_HINMEI_CD";
            this.UK_HINMEI_CD.Name = "UK_HINMEI_CD";
            this.UK_HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UK_HINMEI_CD.PopupSearchSendParams")));
            this.UK_HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UK_HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UK_HINMEI_CD.popupWindowSetting")));
            this.UK_HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UK_HINMEI_CD.RegistCheckMethod")));
            this.UK_HINMEI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UK_HINMEI_CD.Visible = false;
            // 
            // UK_ZAIKO_HINMEI_CD
            // 
            this.UK_ZAIKO_HINMEI_CD.DataPropertyName = "UK_ZAIKO_HINMEI_CD";
            this.UK_ZAIKO_HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.Black;
            this.UK_ZAIKO_HINMEI_CD.DefaultCellStyle = dataGridViewCellStyle19;
            this.UK_ZAIKO_HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UK_ZAIKO_HINMEI_CD.FocusOutCheckMethod")));
            this.UK_ZAIKO_HINMEI_CD.HeaderText = "UK_ZAIKO_HINMEI_CD";
            this.UK_ZAIKO_HINMEI_CD.Name = "UK_ZAIKO_HINMEI_CD";
            this.UK_ZAIKO_HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UK_ZAIKO_HINMEI_CD.PopupSearchSendParams")));
            this.UK_ZAIKO_HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UK_ZAIKO_HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UK_ZAIKO_HINMEI_CD.popupWindowSetting")));
            this.UK_ZAIKO_HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UK_ZAIKO_HINMEI_CD.RegistCheckMethod")));
            this.UK_ZAIKO_HINMEI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UK_ZAIKO_HINMEI_CD.Visible = false;
            // 
            // panel_HidukeSentaku
            // 
            this.panel_HidukeSentaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_HidukeSentaku.Controls.Add(this.DENSHU_KBN_CD);
            this.panel_HidukeSentaku.Controls.Add(this.radioButton2);
            this.panel_HidukeSentaku.Controls.Add(this.radioButton1);
            this.panel_HidukeSentaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel_HidukeSentaku.Location = new System.Drawing.Point(755, 0);
            this.panel_HidukeSentaku.Name = "panel_HidukeSentaku";
            this.panel_HidukeSentaku.Size = new System.Drawing.Size(183, 20);
            this.panel_HidukeSentaku.TabIndex = 6;
            this.panel_HidukeSentaku.Visible = false;
            // 
            // DENSHU_KBN_CD
            // 
            this.DENSHU_KBN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.DENSHU_KBN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DENSHU_KBN_CD.DBFieldsName = "";
            this.DENSHU_KBN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.DENSHU_KBN_CD.DisplayItemName = "受入/出荷";
            this.DENSHU_KBN_CD.DisplayPopUp = null;
            this.DENSHU_KBN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHU_KBN_CD.FocusOutCheckMethod")));
            this.DENSHU_KBN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.DENSHU_KBN_CD.ForeColor = System.Drawing.Color.Black;
            this.DENSHU_KBN_CD.IsInputErrorOccured = false;
            this.DENSHU_KBN_CD.ItemDefinedTypes = "";
            this.DENSHU_KBN_CD.LinkedRadioButtonArray = new string[] {
        "radioButton1",
        "radioButton2"};
            this.DENSHU_KBN_CD.Location = new System.Drawing.Point(-1, -1);
            this.DENSHU_KBN_CD.Name = "DENSHU_KBN_CD";
            this.DENSHU_KBN_CD.PopupAfterExecute = null;
            this.DENSHU_KBN_CD.PopupBeforeExecute = null;
            this.DENSHU_KBN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DENSHU_KBN_CD.PopupSearchSendParams")));
            this.DENSHU_KBN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DENSHU_KBN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DENSHU_KBN_CD.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto3.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DENSHU_KBN_CD.RangeSetting = rangeSettingDto3;
            this.DENSHU_KBN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DENSHU_KBN_CD.RegistCheckMethod")));
            this.DENSHU_KBN_CD.ShortItemName = "";
            this.DENSHU_KBN_CD.Size = new System.Drawing.Size(20, 20);
            this.DENSHU_KBN_CD.TabIndex = 6;
            this.DENSHU_KBN_CD.Tag = "【1、2】のいずれかで入力してください";
            this.DENSHU_KBN_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.DENSHU_KBN_CD.WordWrap = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.DefaultBackColor = System.Drawing.Color.Empty;
            this.radioButton2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radioButton2.FocusOutCheckMethod")));
            this.radioButton2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton2.LinkedTextBox = "DENSHU_KBN_CD";
            this.radioButton2.Location = new System.Drawing.Point(107, 1);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.PopupAfterExecute = null;
            this.radioButton2.PopupBeforeExecute = null;
            this.radioButton2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radioButton2.PopupSearchSendParams")));
            this.radioButton2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radioButton2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radioButton2.popupWindowSetting")));
            this.radioButton2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radioButton2.RegistCheckMethod")));
            this.radioButton2.Size = new System.Drawing.Size(74, 17);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.Tag = "受入/出荷が明細毎を対象とする場合チェックを付けてください";
            this.radioButton2.Text = "2. 出荷";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.Value = "2";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.DefaultBackColor = System.Drawing.Color.Empty;
            this.radioButton1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radioButton1.FocusOutCheckMethod")));
            this.radioButton1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton1.LinkedTextBox = "DENSHU_KBN_CD";
            this.radioButton1.Location = new System.Drawing.Point(27, 1);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.PopupAfterExecute = null;
            this.radioButton1.PopupBeforeExecute = null;
            this.radioButton1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("radioButton1.PopupSearchSendParams")));
            this.radioButton1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.radioButton1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("radioButton1.popupWindowSetting")));
            this.radioButton1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("radioButton1.RegistCheckMethod")));
            this.radioButton1.Size = new System.Drawing.Size(74, 17);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.Tag = "受入/出荷が伝票毎を対象とする場合チェックを付けてください";
            this.radioButton1.Text = "1. 受入";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.Value = "1";
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
            // HINMEI_CD
            // 
            this.HINMEI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HINMEI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HINMEI_CD.ChangeUpperCase = true;
            this.HINMEI_CD.CharacterLimitList = null;
            this.HINMEI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HINMEI_CD.DBFieldsName = "HINMEI_CD";
            this.HINMEI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HINMEI_CD.DisplayItemName = "品名CD";
            this.HINMEI_CD.DisplayPopUp = null;
            this.HINMEI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD.FocusOutCheckMethod")));
            this.HINMEI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.HINMEI_CD.ForeColor = System.Drawing.Color.Black;
            this.HINMEI_CD.GetCodeMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HINMEI_CD.IsInputErrorOccured = false;
            this.HINMEI_CD.ItemDefinedTypes = "varchar";
            this.HINMEI_CD.Location = new System.Drawing.Point(116, 0);
            this.HINMEI_CD.MaxLength = 6;
            this.HINMEI_CD.Name = "HINMEI_CD";
            this.HINMEI_CD.PopupAfterExecute = null;
            this.HINMEI_CD.PopupBeforeExecute = null;
            this.HINMEI_CD.PopupGetMasterField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HINMEI_CD.PopupSearchSendParams")));
            this.HINMEI_CD.PopupSetFormField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_HINMEI;
            this.HINMEI_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.HINMEI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HINMEI_CD.popupWindowSetting")));
            this.HINMEI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HINMEI_CD.RegistCheckMethod")));
            this.HINMEI_CD.SetFormField = "HINMEI_CD,HINMEI_NAME_RYAKU";
            this.HINMEI_CD.ShortItemName = "品名CD";
            this.HINMEI_CD.Size = new System.Drawing.Size(56, 20);
            this.HINMEI_CD.TabIndex = 2;
            this.HINMEI_CD.Tag = "品名を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HINMEI_CD.ZeroPaddengFlag = true;
            this.HINMEI_CD.TextChanged += new System.EventHandler(this.HINMEI_CD_TextChanged);
            this.HINMEI_CD.Validating += new System.ComponentModel.CancelEventHandler(this.HINMEI_CD_Validating);
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.panel_HidukeSentaku);
            this.Controls.Add(this.btnSearchHinmei);
            this.Controls.Add(this.label55);
            this.Controls.Add(this.HINMEI_CD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HINMEI_NAME_RYAKU);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ichiran);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.panel_HidukeSentaku.ResumeLayout(false);
            this.panel_HidukeSentaku.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomCheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        internal System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label2;
        public r_framework.CustomControl.CustomTextBox HINMEI_NAME_RYAKU;
        public r_framework.CustomControl.CustomPopupOpenButton btnSearchHinmei;
        public r_framework.CustomControl.CustomDataGridView Ichiran;
        internal r_framework.CustomControl.CustomPanel panel_HidukeSentaku;
        public r_framework.CustomControl.CustomNumericTextBox2 DENSHU_KBN_CD;
        public r_framework.CustomControl.CustomRadioButton radioButton2;
        public r_framework.CustomControl.CustomRadioButton radioButton1;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn dgvCustomCheckBoxColumn1;
        public r_framework.CustomControl.CustomAlphaNumTextBox HINMEI_CD;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn DELETE_FLG;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn ZAIKO_HINMEI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn ZAIKO_HINMEI_NAME;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column ZAIKO_HIRITSU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn ZAIKO_HIRITSU_UNIT;
        private r_framework.CustomControl.DgvCustomTextBoxColumn BIKOU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_DATE;
        private DataGridViewTextBoxColumn CREATE_PC;
        private DataGridViewTextBoxColumn UPDATE_PC;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TIME_STAMP;
        private DataGridViewTextBoxColumn DENSHUKBNCD;
        private DataGridViewTextBoxColumn HINMEICD;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column UK_DENSHU_KBN_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UK_HINMEI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UK_ZAIKO_HINMEI_CD;
    }
}