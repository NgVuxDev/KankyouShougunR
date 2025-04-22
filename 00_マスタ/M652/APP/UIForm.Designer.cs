using System.Windows.Forms;
namespace Shougun.Core.Master.KaishiZaikoJouhouHoshu.APP
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED = new r_framework.CustomControl.CustomCheckBox();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.CONDITION_ITEM = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.dgvCustomCheckBoxColumn1 = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.GENBA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GYOUSHA_SEARCH_BUTTON = new r_framework.CustomControl.CustomPopupOpenButton();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_LABEL = new System.Windows.Forms.Label();
            this.GYOUSHA_LABEL = new System.Windows.Forms.Label();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.DELETE_FLG = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.ZAIKO_HINMEI_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.ZAIKO_HINMEI_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.KAISHI_ZAIKO_RYOU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.KAISHI_ZAIKO_KINGAKU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.KAISHI_ZAIKO_TANKA = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.UPDATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.UPDATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_USER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CREATE_PC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UPDATE_PC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIME_STAMP = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.COL_GYOUSHA_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COL_GENBA_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // ICHIRAN_HYOUJI_JOUKEN_DELETED
            // 
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.AutoSize = true;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.DefaultBackColor = System.Drawing.Color.Empty;
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ICHIRAN_HYOUJI_JOUKEN_DELETED.FocusOutCheckMethod")));
            this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Location = new System.Drawing.Point(761, 71);
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
            // CONDITION_VALUE
            // 
            this.CONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.CONDITION_VALUE.DBFieldsName = "";
            this.CONDITION_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE.DisplayItemName = "検索条件";
            this.CONDITION_VALUE.DisplayPopUp = null;
            this.CONDITION_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.FocusOutCheckMethod")));
            this.CONDITION_VALUE.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_VALUE.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE.IsInputErrorOccured = false;
            this.CONDITION_VALUE.ItemDefinedTypes = "";
            this.CONDITION_VALUE.Location = new System.Drawing.Point(290, 69);
            this.CONDITION_VALUE.MaxLength = 20;
            this.CONDITION_VALUE.Name = "CONDITION_VALUE";
            this.CONDITION_VALUE.PopupAfterExecute = null;
            this.CONDITION_VALUE.PopupBeforeExecute = null;
            this.CONDITION_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE.PopupSearchSendParams")));
            this.CONDITION_VALUE.PopupSetFormField = "";
            this.CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE.PopupWindowName = "";
            this.CONDITION_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE.popupWindowSetting")));
            this.CONDITION_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.RegistCheckMethod")));
            this.CONDITION_VALUE.SetFormField = "";
            this.CONDITION_VALUE.ShortItemName = "検索条件";
            this.CONDITION_VALUE.Size = new System.Drawing.Size(338, 20);
            this.CONDITION_VALUE.TabIndex = 11;
            this.CONDITION_VALUE.Tag = "検索する文字を入力してください";
            this.CONDITION_VALUE.Enter += new System.EventHandler(this.CONDITION_VALUE_Enter);
            this.CONDITION_VALUE.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CONDITION_VALUE_KeyPress);
            // 
            // CONDITION_ITEM
            // 
            this.CONDITION_ITEM.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONDITION_ITEM.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_ITEM.ChangeUpperCase = true;
            this.CONDITION_ITEM.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.CONDITION_ITEM.DBFieldsName = "";
            this.CONDITION_ITEM.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_ITEM.DisplayItemName = "検索条件";
            this.CONDITION_ITEM.DisplayPopUp = null;
            this.CONDITION_ITEM.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM.FocusOutCheckMethod")));
            this.CONDITION_ITEM.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CONDITION_ITEM.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_ITEM.IsInputErrorOccured = false;
            this.CONDITION_ITEM.Location = new System.Drawing.Point(116, 69);
            this.CONDITION_ITEM.MaxLength = 10;
            this.CONDITION_ITEM.Name = "CONDITION_ITEM";
            this.CONDITION_ITEM.PopupAfterExecute = null;
            this.CONDITION_ITEM.PopupAfterExecuteMethod = "clearConditionValue";
            this.CONDITION_ITEM.PopupBeforeExecute = null;
            this.CONDITION_ITEM.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_ITEM.PopupSearchSendParams")));
            this.CONDITION_ITEM.PopupSendParams = new string[] {
        "Ichiran"};
            this.CONDITION_ITEM.PopupSetFormField = "CONDITION_ITEM,CONDITION_VALUE";
            this.CONDITION_ITEM.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_ITEM.PopupWindowName = "マスタ検索項目ポップアップ";
            this.CONDITION_ITEM.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_ITEM.popupWindowSetting")));
            this.CONDITION_ITEM.ReadOnly = true;
            this.CONDITION_ITEM.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_ITEM.RegistCheckMethod")));
            this.CONDITION_ITEM.SetFormField = "CONDITION_ITEM,CONDITION_VALUE";
            this.CONDITION_ITEM.ShortItemName = "検索条件";
            this.CONDITION_ITEM.Size = new System.Drawing.Size(175, 20);
            this.CONDITION_ITEM.TabIndex = 10;
            this.CONDITION_ITEM.Tag = "検索条件項目を指定してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(640, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 600;
            this.label1.Text = "表示条件";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label16.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label16.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(1, 69);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(110, 20);
            this.label16.TabIndex = 500;
            this.label16.Text = "検索条件";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToDeleteRows = false;
            this.Ichiran.AllowUserToResizeColumns = false;
            this.Ichiran.AllowUserToResizeRows = false;
            this.Ichiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("MS Gothic", 9.75F);
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
            this.ZAIKO_HINMEI_NAME_RYAKU,
            this.KAISHI_ZAIKO_RYOU,
            this.KAISHI_ZAIKO_KINGAKU,
            this.KAISHI_ZAIKO_TANKA,
            this.UPDATE_USER,
            this.UPDATE_DATE,
            this.CREATE_USER,
            this.CREATE_DATE,
            this.CREATE_PC,
            this.UPDATE_PC,
            this.TIME_STAMP,
            this.COL_GYOUSHA_CD,
            this.COL_GENBA_CD});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle17;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = null;
            this.Ichiran.Location = new System.Drawing.Point(1, 95);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(990, 352);
            this.Ichiran.TabIndex = 16;
            this.Ichiran.CellEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.Ichiran_CellEnter);
            this.Ichiran.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler(this.Ichiran_CellParsing);
            this.Ichiran.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.Ichiran_CellValidating);
            this.Ichiran.ColumnWidthChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.Ichiran_ColumnWidthChanged);
            this.Ichiran.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.Ichiran_EditingControlShowing);
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
            // GENBA_SEARCH_BUTTON
            // 
            this.GENBA_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GENBA_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GENBA_SEARCH_BUTTON.DBFieldsName = null;
            this.GENBA_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_SEARCH_BUTTON.DisplayItemName = null;
            this.GENBA_SEARCH_BUTTON.DisplayPopUp = null;
            this.GENBA_SEARCH_BUTTON.ErrorMessage = null;
            this.GENBA_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.GENBA_SEARCH_BUTTON.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GENBA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GENBA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GENBA_SEARCH_BUTTON.Image")));
            this.GENBA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GENBA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GENBA_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.GENBA_SEARCH_BUTTON.Location = new System.Drawing.Point(457, 30);
            this.GENBA_SEARCH_BUTTON.Name = "GENBA_SEARCH_BUTTON";
            this.GENBA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupAfterExecuteMethod = "SetGenba";
            this.GENBA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GENBA_SEARCH_BUTTON.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GENBA_SEARCH_BUTTON.PopupSetFormField = "GENBA_CD, GENBA_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_SEARCH_BUTTON.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.popupWindowSetting")));
            this.GENBA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GENBA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GENBA_SEARCH_BUTTON.SetFormField = null;
            this.GENBA_SEARCH_BUTTON.ShortItemName = null;
            this.GENBA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GENBA_SEARCH_BUTTON.TabIndex = 760;
            this.GENBA_SEARCH_BUTTON.TabStop = false;
            this.GENBA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GENBA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // GYOUSHA_SEARCH_BUTTON
            // 
            this.GYOUSHA_SEARCH_BUTTON.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.GYOUSHA_SEARCH_BUTTON.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.GYOUSHA_SEARCH_BUTTON.DBFieldsName = null;
            this.GYOUSHA_SEARCH_BUTTON.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_SEARCH_BUTTON.DisplayItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.DisplayPopUp = null;
            this.GYOUSHA_SEARCH_BUTTON.ErrorMessage = null;
            this.GYOUSHA_SEARCH_BUTTON.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.FocusOutCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_SEARCH_BUTTON.GetCodeMasterField = null;
            this.GYOUSHA_SEARCH_BUTTON.Image = ((System.Drawing.Image)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.Image")));
            this.GYOUSHA_SEARCH_BUTTON.ItemDefinedTypes = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedSettingTextBox = null;
            this.GYOUSHA_SEARCH_BUTTON.LinkedTextBoxs = null;
            this.GYOUSHA_SEARCH_BUTTON.Location = new System.Drawing.Point(457, 3);
            this.GYOUSHA_SEARCH_BUTTON.Name = "GYOUSHA_SEARCH_BUTTON";
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupAfterExecuteMethod = "SetGyousha";
            this.GYOUSHA_SEARCH_BUTTON.PopupBeforeExecute = null;
            this.GYOUSHA_SEARCH_BUTTON.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.PopupSearchSendParams")));
            this.GYOUSHA_SEARCH_BUTTON.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_SEARCH_BUTTON.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_SEARCH_BUTTON.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.popupWindowSetting")));
            this.GYOUSHA_SEARCH_BUTTON.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_SEARCH_BUTTON.RegistCheckMethod")));
            this.GYOUSHA_SEARCH_BUTTON.SearchDisplayFlag = 0;
            this.GYOUSHA_SEARCH_BUTTON.SetFormField = null;
            this.GYOUSHA_SEARCH_BUTTON.ShortItemName = null;
            this.GYOUSHA_SEARCH_BUTTON.Size = new System.Drawing.Size(22, 22);
            this.GYOUSHA_SEARCH_BUTTON.TabIndex = 759;
            this.GYOUSHA_SEARCH_BUTTON.TabStop = false;
            this.GYOUSHA_SEARCH_BUTTON.UseVisualStyleBackColor = false;
            this.GYOUSHA_SEARCH_BUTTON.ZeroPaddengFlag = false;
            // 
            // GENBA_NAME_RYAKU
            // 
            this.GENBA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GENBA_NAME_RYAKU.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_RYAKU.DisplayItemName = "現場名";
            this.GENBA_NAME_RYAKU.DisplayPopUp = null;
            this.GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GENBA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GENBA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.GENBA_NAME_RYAKU.Location = new System.Drawing.Point(170, 31);
            this.GENBA_NAME_RYAKU.MaxLength = 40;
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.ReadOnly = true;
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.GENBA_NAME_RYAKU.TabIndex = 758;
            this.GENBA_NAME_RYAKU.TabStop = false;
            this.GENBA_NAME_RYAKU.Tag = " ";
            // 
            // GYOUSHA_NAME_RYAKU
            // 
            this.GYOUSHA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.GYOUSHA_NAME_RYAKU.DBFieldsName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_RYAKU.DisplayItemName = "業者名";
            this.GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(170, 5);
            this.GYOUSHA_NAME_RYAKU.MaxLength = 40;
            this.GYOUSHA_NAME_RYAKU.Name = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.GYOUSHA_NAME_RYAKU.TabIndex = 756;
            this.GYOUSHA_NAME_RYAKU.TabStop = false;
            this.GYOUSHA_NAME_RYAKU.Tag = " ";
            // 
            // GENBA_LABEL
            // 
            this.GENBA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GENBA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GENBA_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GENBA_LABEL.ForeColor = System.Drawing.Color.White;
            this.GENBA_LABEL.Location = new System.Drawing.Point(1, 31);
            this.GENBA_LABEL.Name = "GENBA_LABEL";
            this.GENBA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.GENBA_LABEL.TabIndex = 762;
            this.GENBA_LABEL.Text = "現場※";
            this.GENBA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_LABEL
            // 
            this.GYOUSHA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GYOUSHA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GYOUSHA_LABEL.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_LABEL.ForeColor = System.Drawing.Color.White;
            this.GYOUSHA_LABEL.Location = new System.Drawing.Point(1, 5);
            this.GYOUSHA_LABEL.Name = "GYOUSHA_LABEL";
            this.GYOUSHA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.GYOUSHA_LABEL.TabIndex = 761;
            this.GYOUSHA_LABEL.Text = "業者※";
            this.GYOUSHA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD.ChangeUpperCase = true;
            this.GENBA_CD.CharacterLimitList = null;
            this.GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD.DBFieldsName = "GENBA_CD";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayItemName = "現場CD";
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "GENBA_CD, GENBA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(116, 31);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupAfterExecuteMethod = "SetGenba";
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD, GENBA_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD, GENBA_NAME_RYAKU";
            this.GENBA_CD.Size = new System.Drawing.Size(55, 20);
            this.GENBA_CD.TabIndex = 10;
            this.GENBA_CD.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.TextChanged += new System.EventHandler(this.GENBA_CD_TextChanged);
            this.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GENBA_CD_Validating);
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD.ChangeUpperCase = true;
            this.GYOUSHA_CD.CharacterLimitList = null;
            this.GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "業者CD";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(116, 5);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "SetGyousha";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupBeforeExecuteMethod = "PopupBeforeGyousha";
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(55, 20);
            this.GYOUSHA_CD.TabIndex = 5;
            this.GYOUSHA_CD.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.TextChanged += new System.EventHandler(this.GYOUSHA_CD_TextChanged);
            this.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_Validating);
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
            this.DELETE_FLG.ToolTipText = "削除する場合にはチェックを付けてください。";
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
            this.ZAIKO_HINMEI_CD.SetFormField = "ZAIKO_HINMEI_CD,ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_HINMEI_CD.ToolTipText = "英数字6桁以内で入力してください。";
            this.ZAIKO_HINMEI_CD.Width = 110;
            this.ZAIKO_HINMEI_CD.ZeroPaddengFlag = true;
            // 
            // ZAIKO_HINMEI_NAME_RYAKU
            // 
            this.ZAIKO_HINMEI_NAME_RYAKU.DataPropertyName = "ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_NAME_RYAKU.DBFieldsName = "ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.ZAIKO_HINMEI_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle4;
            this.ZAIKO_HINMEI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_RYAKU.FocusOutCheckMethod")));
            this.ZAIKO_HINMEI_NAME_RYAKU.HeaderText = "在庫品名";
            this.ZAIKO_HINMEI_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.ZAIKO_HINMEI_NAME_RYAKU.MaxInputLength = 40;
            this.ZAIKO_HINMEI_NAME_RYAKU.Name = "ZAIKO_HINMEI_NAME_RYAKU";
            this.ZAIKO_HINMEI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_RYAKU.PopupSearchSendParams")));
            this.ZAIKO_HINMEI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZAIKO_HINMEI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_RYAKU.popupWindowSetting")));
            this.ZAIKO_HINMEI_NAME_RYAKU.ReadOnly = true;
            this.ZAIKO_HINMEI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZAIKO_HINMEI_NAME_RYAKU.RegistCheckMethod")));
            this.ZAIKO_HINMEI_NAME_RYAKU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.ZAIKO_HINMEI_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ZAIKO_HINMEI_NAME_RYAKU.Width = 150;
            // 
            // KAISHI_ZAIKO_RYOU
            // 
            this.KAISHI_ZAIKO_RYOU.DataPropertyName = "KAISHI_ZAIKO_RYOU";
            this.KAISHI_ZAIKO_RYOU.DBFieldsName = "KAISHI_ZAIKO_RYOU";
            this.KAISHI_ZAIKO_RYOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.NullValue = null;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.KAISHI_ZAIKO_RYOU.DefaultCellStyle = dataGridViewCellStyle5;
            this.KAISHI_ZAIKO_RYOU.DisplayItemName = "開始在庫量";
            this.KAISHI_ZAIKO_RYOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAISHI_ZAIKO_RYOU.FocusOutCheckMethod")));
            this.KAISHI_ZAIKO_RYOU.FormatSetting = "システム設定(重量書式)";
            this.KAISHI_ZAIKO_RYOU.HeaderText = "開始在庫量";
            this.KAISHI_ZAIKO_RYOU.ItemDefinedTypes = "decimal";
            this.KAISHI_ZAIKO_RYOU.Name = "KAISHI_ZAIKO_RYOU";
            this.KAISHI_ZAIKO_RYOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KAISHI_ZAIKO_RYOU.PopupSearchSendParams")));
            this.KAISHI_ZAIKO_RYOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KAISHI_ZAIKO_RYOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KAISHI_ZAIKO_RYOU.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            196608});
            this.KAISHI_ZAIKO_RYOU.RangeSetting = rangeSettingDto1;
            this.KAISHI_ZAIKO_RYOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAISHI_ZAIKO_RYOU.RegistCheckMethod")));
            this.KAISHI_ZAIKO_RYOU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KAISHI_ZAIKO_RYOU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KAISHI_ZAIKO_RYOU.ToolTipText = "整数部分は半角7桁で入力してください。小数点以下はシステム設定を参照します。";
            // 
            // KAISHI_ZAIKO_KINGAKU
            // 
            this.KAISHI_ZAIKO_KINGAKU.CustomFormatSetting = "#,##0";
            this.KAISHI_ZAIKO_KINGAKU.DataPropertyName = "KAISHI_ZAIKO_KINGAKU";
            this.KAISHI_ZAIKO_KINGAKU.DBFieldsName = "KAISHI_ZAIKO_KINGAKU";
            this.KAISHI_ZAIKO_KINGAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N0";
            dataGridViewCellStyle6.NullValue = null;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.KAISHI_ZAIKO_KINGAKU.DefaultCellStyle = dataGridViewCellStyle6;
            this.KAISHI_ZAIKO_KINGAKU.DisplayItemName = "開始在庫金額";
            this.KAISHI_ZAIKO_KINGAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAISHI_ZAIKO_KINGAKU.FocusOutCheckMethod")));
            this.KAISHI_ZAIKO_KINGAKU.FormatSetting = "カスタム";
            this.KAISHI_ZAIKO_KINGAKU.HeaderText = "開始在庫金額";
            this.KAISHI_ZAIKO_KINGAKU.ItemDefinedTypes = "decimal";
            this.KAISHI_ZAIKO_KINGAKU.Name = "KAISHI_ZAIKO_KINGAKU";
            this.KAISHI_ZAIKO_KINGAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KAISHI_ZAIKO_KINGAKU.PopupSearchSendParams")));
            this.KAISHI_ZAIKO_KINGAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KAISHI_ZAIKO_KINGAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KAISHI_ZAIKO_KINGAKU.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.KAISHI_ZAIKO_KINGAKU.RangeSetting = rangeSettingDto2;
            this.KAISHI_ZAIKO_KINGAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAISHI_ZAIKO_KINGAKU.RegistCheckMethod")));
            this.KAISHI_ZAIKO_KINGAKU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KAISHI_ZAIKO_KINGAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KAISHI_ZAIKO_KINGAKU.ToolTipText = "半角8桁以内で入力してください。";
            // 
            // KAISHI_ZAIKO_TANKA
            // 
            this.KAISHI_ZAIKO_TANKA.DataPropertyName = "KAISHI_ZAIKO_TANKA";
            this.KAISHI_ZAIKO_TANKA.DBFieldsName = "KAISHI_ZAIKO_TANKA";
            this.KAISHI_ZAIKO_TANKA.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.KAISHI_ZAIKO_TANKA.DefaultCellStyle = dataGridViewCellStyle7;
            this.KAISHI_ZAIKO_TANKA.DisplayItemName = "開始在庫単価";
            this.KAISHI_ZAIKO_TANKA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAISHI_ZAIKO_TANKA.FocusOutCheckMethod")));
            this.KAISHI_ZAIKO_TANKA.FormatSetting = "システム設定(単価書式)";
            this.KAISHI_ZAIKO_TANKA.HeaderText = "開始在庫単価";
            this.KAISHI_ZAIKO_TANKA.ItemDefinedTypes = "decimal";
            this.KAISHI_ZAIKO_TANKA.Name = "KAISHI_ZAIKO_TANKA";
            this.KAISHI_ZAIKO_TANKA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KAISHI_ZAIKO_TANKA.PopupSearchSendParams")));
            this.KAISHI_ZAIKO_TANKA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KAISHI_ZAIKO_TANKA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KAISHI_ZAIKO_TANKA.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            1410065407,
            2,
            0,
            196608});
            this.KAISHI_ZAIKO_TANKA.RangeSetting = rangeSettingDto3;
            this.KAISHI_ZAIKO_TANKA.ReadOnly = true;
            this.KAISHI_ZAIKO_TANKA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KAISHI_ZAIKO_TANKA.RegistCheckMethod")));
            this.KAISHI_ZAIKO_TANKA.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KAISHI_ZAIKO_TANKA.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KAISHI_ZAIKO_TANKA.ToolTipText = " ";
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
            this.CREATE_PC.ReadOnly = true;
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
            this.UPDATE_PC.ReadOnly = true;
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
            this.TIME_STAMP.ReadOnly = true;
            this.TIME_STAMP.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TIME_STAMP.RegistCheckMethod")));
            this.TIME_STAMP.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TIME_STAMP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIME_STAMP.Visible = false;
            // 
            // COL_GYOUSHA_CD
            // 
            this.COL_GYOUSHA_CD.DataPropertyName = "GYOUSHA_CD";
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_GYOUSHA_CD.DefaultCellStyle = dataGridViewCellStyle15;
            this.COL_GYOUSHA_CD.HeaderText = "GYOUSHA_CD";
            this.COL_GYOUSHA_CD.Name = "COL_GYOUSHA_CD";
            this.COL_GYOUSHA_CD.ReadOnly = true;
            this.COL_GYOUSHA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COL_GYOUSHA_CD.Visible = false;
            // 
            // COL_GENBA_CD
            // 
            this.COL_GENBA_CD.DataPropertyName = "GENBA_CD";
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.COL_GENBA_CD.DefaultCellStyle = dataGridViewCellStyle16;
            this.COL_GENBA_CD.HeaderText = "GENBA_CD";
            this.COL_GENBA_CD.Name = "COL_GENBA_CD";
            this.COL_GENBA_CD.ReadOnly = true;
            this.COL_GENBA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.COL_GENBA_CD.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 497);
            this.Controls.Add(this.GENBA_SEARCH_BUTTON);
            this.Controls.Add(this.GYOUSHA_SEARCH_BUTTON);
            this.Controls.Add(this.GENBA_NAME_RYAKU);
            this.Controls.Add(this.GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.GENBA_LABEL);
            this.Controls.Add(this.GYOUSHA_LABEL);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.ICHIRAN_HYOUJI_JOUKEN_DELETED);
            this.Controls.Add(this.CONDITION_VALUE);
            this.Controls.Add(this.CONDITION_ITEM);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.Ichiran);
            this.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Shown += new System.EventHandler(this.UIForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomCheckBox ICHIRAN_HYOUJI_JOUKEN_DELETED;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label16;
        public r_framework.CustomControl.CustomDataGridView Ichiran;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn dgvCustomCheckBoxColumn1;
    
        internal r_framework.CustomControl.CustomPopupOpenButton GENBA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomPopupOpenButton GYOUSHA_SEARCH_BUTTON;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU;
        internal Label GENBA_LABEL;
        internal Label GYOUSHA_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn DELETE_FLG;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn ZAIKO_HINMEI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn ZAIKO_HINMEI_NAME_RYAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column KAISHI_ZAIKO_RYOU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column KAISHI_ZAIKO_KINGAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column KAISHI_ZAIKO_TANKA;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn UPDATE_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_USER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CREATE_DATE;
        private DataGridViewTextBoxColumn CREATE_PC;
        private DataGridViewTextBoxColumn UPDATE_PC;
        private r_framework.CustomControl.DgvCustomTextBoxColumn TIME_STAMP;
        private DataGridViewTextBoxColumn COL_GYOUSHA_CD;
        private DataGridViewTextBoxColumn COL_GENBA_CD;
    }
}