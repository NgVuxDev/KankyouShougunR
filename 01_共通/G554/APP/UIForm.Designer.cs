using System.Windows.Forms;
using System;
namespace Shougun.Core.Common.DenpyouhimozukePatternIchiran
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
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
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
            this.label1 = new System.Windows.Forms.Label();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.dgvDenpyouhimozuke = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.DELETE_FLG = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.DEFAULT_KBN = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.DISP_NUMBER = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.OUTPUT_KBN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.PATTERN_NAME = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SYSTEM_ID_MOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SYSTEM_ID_MOPK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEQ_MOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SEQ_MOPK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OUTPUT_KBN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SHAIN_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIME_STAMP_MOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIME_STAMP_MOPK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DISP_NUMBER_DEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEFAULT_KBN_DEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDenpyouhimozuke)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "パターン名";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CONDITION_VALUE
            // 
            this.CONDITION_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.CONDITION_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONDITION_VALUE.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CONDITION_VALUE.DBFieldsName = "";
            this.CONDITION_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONDITION_VALUE.DisplayItemName = "検索条件";
            this.CONDITION_VALUE.DisplayPopUp = null;
            this.CONDITION_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.FocusOutCheckMethod")));
            this.CONDITION_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONDITION_VALUE.ForeColor = System.Drawing.Color.Black;
            this.CONDITION_VALUE.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.CONDITION_VALUE.IsInputErrorOccured = false;
            this.CONDITION_VALUE.Location = new System.Drawing.Point(115, 1);
            this.CONDITION_VALUE.MaxLength = 10;
            this.CONDITION_VALUE.Name = "CONDITION_VALUE";
            this.CONDITION_VALUE.PopupAfterExecute = null;
            this.CONDITION_VALUE.PopupBeforeExecute = null;
            this.CONDITION_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE.PopupSearchSendParams")));
            this.CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE.popupWindowSetting")));
            this.CONDITION_VALUE.prevText = null;
            this.CONDITION_VALUE.PrevText = null;
            this.CONDITION_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.RegistCheckMethod")));
            this.CONDITION_VALUE.Size = new System.Drawing.Size(145, 20);
            this.CONDITION_VALUE.TabIndex = 1;
            this.CONDITION_VALUE.Tag = "入力された文字列であいまい検索を行います";
            // 
            // dgvDenpyouhimozuke
            // 
            this.dgvDenpyouhimozuke.AllowUserToAddRows = false;
            this.dgvDenpyouhimozuke.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDenpyouhimozuke.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDenpyouhimozuke.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDenpyouhimozuke.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DELETE_FLG,
            this.DEFAULT_KBN,
            this.DISP_NUMBER,
            this.OUTPUT_KBN_NAME,
            this.PATTERN_NAME,
            this.SYSTEM_ID_MOP,
            this.SYSTEM_ID_MOPK,
            this.SEQ_MOP,
            this.SEQ_MOPK,
            this.OUTPUT_KBN,
            this.SHAIN_CD,
            this.TIME_STAMP_MOP,
            this.TIME_STAMP_MOPK,
            this.DISP_NUMBER_DEL,
            this.DEFAULT_KBN_DEL});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDenpyouhimozuke.DefaultCellStyle = dataGridViewCellStyle17;
            this.dgvDenpyouhimozuke.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDenpyouhimozuke.EnableHeadersVisualStyles = false;
            this.dgvDenpyouhimozuke.GridColor = System.Drawing.Color.White;
            this.dgvDenpyouhimozuke.IsReload = false;
            this.dgvDenpyouhimozuke.LinkedDataPanelName = null;
            this.dgvDenpyouhimozuke.Location = new System.Drawing.Point(0, 23);
            this.dgvDenpyouhimozuke.MultiSelect = false;
            this.dgvDenpyouhimozuke.Name = "dgvDenpyouhimozuke";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDenpyouhimozuke.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.dgvDenpyouhimozuke.RowHeadersVisible = false;
            this.dgvDenpyouhimozuke.RowTemplate.Height = 21;
            this.dgvDenpyouhimozuke.ShowCellToolTips = false;
            this.dgvDenpyouhimozuke.Size = new System.Drawing.Size(990, 447);
            this.dgvDenpyouhimozuke.TabIndex = 4;
            this.dgvDenpyouhimozuke.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PatternIchiran_CellContentClick);
            this.dgvDenpyouhimozuke.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PatternIchiran_CellDoubleClick);
            this.dgvDenpyouhimozuke.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.dgvDenpyouhimozuke_CellValidating);
            this.dgvDenpyouhimozuke.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            // 
            // DELETE_FLG
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.DELETE_FLG.DefaultCellStyle = dataGridViewCellStyle2;
            this.DELETE_FLG.FocusOutCheckMethod = null;
            this.DELETE_FLG.HeaderText = "削";
            this.DELETE_FLG.Name = "DELETE_FLG";
            this.DELETE_FLG.RegistCheckMethod = null;
            this.DELETE_FLG.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DELETE_FLG.ToolTipText = "削除したいパターンにチェックを付けてください。";
            this.DELETE_FLG.Width = 35;
            // 
            // DEFAULT_KBN
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.NullValue = false;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.DEFAULT_KBN.DefaultCellStyle = dataGridViewCellStyle3;
            this.DEFAULT_KBN.FocusOutCheckMethod = null;
            this.DEFAULT_KBN.HeaderText = "デフォルト";
            this.DEFAULT_KBN.Name = "DEFAULT_KBN";
            this.DEFAULT_KBN.RegistCheckMethod = null;
            this.DEFAULT_KBN.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DEFAULT_KBN.ToolTipText = "デフォルト表示させたいパターンにチェックを付けてください";
            this.DEFAULT_KBN.Width = 85;
            // 
            // DISP_NUMBER
            // 
            this.DISP_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.DISP_NUMBER.DefaultCellStyle = dataGridViewCellStyle4;
            this.DISP_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DISP_NUMBER.FocusOutCheckMethod")));
            this.DISP_NUMBER.HeaderText = "表示";
            this.DISP_NUMBER.Name = "DISP_NUMBER";
            this.DISP_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DISP_NUMBER.PopupSearchSendParams")));
            this.DISP_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DISP_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DISP_NUMBER.popupWindowSetting")));
            this.DISP_NUMBER.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            5,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.DISP_NUMBER.RangeSetting = rangeSettingDto1;
            this.DISP_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DISP_NUMBER.RegistCheckMethod")));
            this.DISP_NUMBER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DISP_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DISP_NUMBER.ToolTipText = "【1～5】のいずれかで入力してください。";
            this.DISP_NUMBER.Width = 55;
            // 
            // OUTPUT_KBN_NAME
            // 
            this.OUTPUT_KBN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.OUTPUT_KBN_NAME.DefaultCellStyle = dataGridViewCellStyle5;
            this.OUTPUT_KBN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_NAME.FocusOutCheckMethod")));
            this.OUTPUT_KBN_NAME.HeaderText = "出力区分";
            this.OUTPUT_KBN_NAME.Name = "OUTPUT_KBN_NAME";
            this.OUTPUT_KBN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("OUTPUT_KBN_NAME.PopupSearchSendParams")));
            this.OUTPUT_KBN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.OUTPUT_KBN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("OUTPUT_KBN_NAME.popupWindowSetting")));
            this.OUTPUT_KBN_NAME.ReadOnly = true;
            this.OUTPUT_KBN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_NAME.RegistCheckMethod")));
            this.OUTPUT_KBN_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.OUTPUT_KBN_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OUTPUT_KBN_NAME.Width = 75;
            // 
            // PATTERN_NAME
            // 
            this.PATTERN_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.PATTERN_NAME.DBFieldsName = "char";
            this.PATTERN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.PATTERN_NAME.DefaultCellStyle = dataGridViewCellStyle6;
            this.PATTERN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.FocusOutCheckMethod")));
            this.PATTERN_NAME.HeaderText = "パターン名";
            this.PATTERN_NAME.MaxInputLength = 20;
            this.PATTERN_NAME.Name = "PATTERN_NAME";
            this.PATTERN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_NAME.PopupSearchSendParams")));
            this.PATTERN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_NAME.popupWindowSetting")));
            this.PATTERN_NAME.ReadOnly = true;
            this.PATTERN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.RegistCheckMethod")));
            this.PATTERN_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PATTERN_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PATTERN_NAME.ToolTipText = "ダブルクリックで一覧出力項目選択画面に切り替わります。";
            this.PATTERN_NAME.Width = 300;
            // 
            // SYSTEM_ID_MOP
            // 
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.SYSTEM_ID_MOP.DefaultCellStyle = dataGridViewCellStyle7;
            this.SYSTEM_ID_MOP.HeaderText = "SYSTEM_ID_MOP";
            this.SYSTEM_ID_MOP.Name = "SYSTEM_ID_MOP";
            this.SYSTEM_ID_MOP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SYSTEM_ID_MOP.Visible = false;
            // 
            // SYSTEM_ID_MOPK
            // 
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.SYSTEM_ID_MOPK.DefaultCellStyle = dataGridViewCellStyle8;
            this.SYSTEM_ID_MOPK.HeaderText = "SYSTEM_ID_MOPK";
            this.SYSTEM_ID_MOPK.Name = "SYSTEM_ID_MOPK";
            this.SYSTEM_ID_MOPK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SYSTEM_ID_MOPK.Visible = false;
            // 
            // SEQ_MOP
            // 
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.SEQ_MOP.DefaultCellStyle = dataGridViewCellStyle9;
            this.SEQ_MOP.HeaderText = "SEQ_MOP";
            this.SEQ_MOP.Name = "SEQ_MOP";
            this.SEQ_MOP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SEQ_MOP.Visible = false;
            // 
            // SEQ_MOPK
            // 
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.SEQ_MOPK.DefaultCellStyle = dataGridViewCellStyle10;
            this.SEQ_MOPK.HeaderText = "SEQ_MOPK";
            this.SEQ_MOPK.Name = "SEQ_MOPK";
            this.SEQ_MOPK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SEQ_MOPK.Visible = false;
            // 
            // OUTPUT_KBN
            // 
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.OUTPUT_KBN.DefaultCellStyle = dataGridViewCellStyle11;
            this.OUTPUT_KBN.HeaderText = "OUTPUT_KBN";
            this.OUTPUT_KBN.Name = "OUTPUT_KBN";
            this.OUTPUT_KBN.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.OUTPUT_KBN.Visible = false;
            // 
            // SHAIN_CD
            // 
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.SHAIN_CD.DefaultCellStyle = dataGridViewCellStyle12;
            this.SHAIN_CD.HeaderText = "SHAIN_CD";
            this.SHAIN_CD.Name = "SHAIN_CD";
            this.SHAIN_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SHAIN_CD.Visible = false;
            // 
            // TIME_STAMP_MOP
            // 
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.TIME_STAMP_MOP.DefaultCellStyle = dataGridViewCellStyle13;
            this.TIME_STAMP_MOP.HeaderText = "TIME_STAMP_MOP";
            this.TIME_STAMP_MOP.Name = "TIME_STAMP_MOP";
            this.TIME_STAMP_MOP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIME_STAMP_MOP.Visible = false;
            // 
            // TIME_STAMP_MOPK
            // 
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.TIME_STAMP_MOPK.DefaultCellStyle = dataGridViewCellStyle14;
            this.TIME_STAMP_MOPK.HeaderText = "TIME_STAMP_MOPK";
            this.TIME_STAMP_MOPK.Name = "TIME_STAMP_MOPK";
            this.TIME_STAMP_MOPK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIME_STAMP_MOPK.Visible = false;
            // 
            // DISP_NUMBER_DEL
            // 
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.DISP_NUMBER_DEL.DefaultCellStyle = dataGridViewCellStyle15;
            this.DISP_NUMBER_DEL.HeaderText = "DISP_NUMBER_DEL";
            this.DISP_NUMBER_DEL.Name = "DISP_NUMBER_DEL";
            this.DISP_NUMBER_DEL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DISP_NUMBER_DEL.Visible = false;
            // 
            // DEFAULT_KBN_DEL
            // 
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.DEFAULT_KBN_DEL.DefaultCellStyle = dataGridViewCellStyle16;
            this.DEFAULT_KBN_DEL.HeaderText = "DEFAULT_KBN_DEL";
            this.DEFAULT_KBN_DEL.Name = "DEFAULT_KBN_DEL";
            this.DEFAULT_KBN_DEL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DEFAULT_KBN_DEL.Visible = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(264, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 22);
            this.button1.TabIndex = 5;
            this.button1.Text = "検索";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Search);
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgvDenpyouhimozuke);
            this.Controls.Add(this.CONDITION_VALUE);
            this.Controls.Add(this.label1);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDenpyouhimozuke)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void TextBoxDec_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void TextBoxText_KeyPress(object sender, KeyPressEventArgs e)
        {
            //0～9と、バックスペース以外の時は、イベントをキャンセルする
            if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != '\b')
            {
                e.Handled = false;
            }

        }

        private void dataGridView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                e.Control.KeyPress -= new KeyPressEventHandler(TextBoxDec_KeyPress);
                e.Control.KeyPress -= new KeyPressEventHandler(TextBoxText_KeyPress);

                if (this.dgvDenpyouhimozuke.CurrentCell.ColumnIndex == 2)
                {
                    e.Control.ImeMode = ImeMode.Alpha;
                    e.Control.KeyPress += new KeyPressEventHandler(TextBoxDec_KeyPress);
                }

                if (this.dgvDenpyouhimozuke.CurrentCell.ColumnIndex == 3 || this.dgvDenpyouhimozuke.CurrentCell.ColumnIndex == 4)
                {
                    e.Control.ImeMode = ImeMode.Hiragana;
                    e.Control.KeyPress += new KeyPressEventHandler(TextBoxText_KeyPress);
                }
            }
        }

        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE;
        public r_framework.CustomControl.CustomDataGridView dgvDenpyouhimozuke;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn DELETE_FLG;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn DEFAULT_KBN;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column DISP_NUMBER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn OUTPUT_KBN_NAME;
        private r_framework.CustomControl.DgvCustomTextBoxColumn PATTERN_NAME;
        private DataGridViewTextBoxColumn SYSTEM_ID_MOP;
        private DataGridViewTextBoxColumn SYSTEM_ID_MOPK;
        private DataGridViewTextBoxColumn SEQ_MOP;
        private DataGridViewTextBoxColumn SEQ_MOPK;
        private DataGridViewTextBoxColumn OUTPUT_KBN;
        private DataGridViewTextBoxColumn SHAIN_CD;
        private DataGridViewTextBoxColumn TIME_STAMP_MOP;
        private DataGridViewTextBoxColumn TIME_STAMP_MOPK;
        private DataGridViewTextBoxColumn DISP_NUMBER_DEL;
        private DataGridViewTextBoxColumn DEFAULT_KBN_DEL;
        private Button button1;
    }
}