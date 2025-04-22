using System.Windows.Forms;
using System;
namespace Shougun.Core.Common.PatternIchiran
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label1 = new System.Windows.Forms.Label();
            this.CONDITION_VALUE = new r_framework.CustomControl.CustomTextBox();
            this.PatternIchiran = new r_framework.CustomControl.CustomDataGridView(this.components);
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
            this.DENSHU_KBN_CD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIME_STAMP_MOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TIME_STAMP_MOPK = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DISP_NUMBER_DEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEFAULT_KBN_DEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.PatternIchiran)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
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
            20,
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
            this.CONDITION_VALUE.Location = new System.Drawing.Point(115, 0);
            this.CONDITION_VALUE.MaxLength = 20;
            this.CONDITION_VALUE.Name = "CONDITION_VALUE";
            this.CONDITION_VALUE.PopupAfterExecute = null;
            this.CONDITION_VALUE.PopupBeforeExecute = null;
            this.CONDITION_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONDITION_VALUE.PopupSearchSendParams")));
            this.CONDITION_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONDITION_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONDITION_VALUE.popupWindowSetting")));
            this.CONDITION_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONDITION_VALUE.RegistCheckMethod")));
            this.CONDITION_VALUE.Size = new System.Drawing.Size(160, 20);
            this.CONDITION_VALUE.TabIndex = 1;
            this.CONDITION_VALUE.Tag = "検索する文字を入力してください";
            // 
            // PatternIchiran
            // 
            this.PatternIchiran.AllowUserToAddRows = false;
            this.PatternIchiran.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PatternIchiran.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.PatternIchiran.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.PatternIchiran.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
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
            this.DENSHU_KBN_CD,
            this.TIME_STAMP_MOP,
            this.TIME_STAMP_MOPK,
            this.DISP_NUMBER_DEL,
            this.DEFAULT_KBN_DEL});
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.PatternIchiran.DefaultCellStyle = dataGridViewCellStyle18;
            this.PatternIchiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.PatternIchiran.EnableHeadersVisualStyles = false;
            this.PatternIchiran.GridColor = System.Drawing.Color.White;
            this.PatternIchiran.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.PatternIchiran.IsReload = false;
            this.PatternIchiran.LinkedDataPanelName = null;
            this.PatternIchiran.Location = new System.Drawing.Point(0, 22);
            this.PatternIchiran.MultiSelect = false;
            this.PatternIchiran.Name = "PatternIchiran";
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle19.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.PatternIchiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.PatternIchiran.RowHeadersVisible = false;
            this.PatternIchiran.RowTemplate.Height = 21;
            this.PatternIchiran.ShowCellToolTips = false;
            this.PatternIchiran.Size = new System.Drawing.Size(1000, 457);
            this.PatternIchiran.TabIndex = 4;
            this.PatternIchiran.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PatternIchiran_CellContentClick);
            this.PatternIchiran.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.PatternIchiran_CellDoubleClick);
            this.PatternIchiran.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView_EditingControlShowing);
            // 
            // DELETE_FLG
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = false;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.DELETE_FLG.DefaultCellStyle = dataGridViewCellStyle2;
            this.DELETE_FLG.FocusOutCheckMethod = null;
            this.DELETE_FLG.HeaderText = "削除";
            this.DELETE_FLG.Name = "DELETE_FLG";
            this.DELETE_FLG.RegistCheckMethod = null;
            this.DELETE_FLG.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DELETE_FLG.ToolTipText = "削除したいパターンにチェックを付けてください。";
            this.DELETE_FLG.Width = 70;
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
            this.DEFAULT_KBN.ToolTipText = "デフォルト表示させたいパターンにチェックを付けてください。";
            this.DEFAULT_KBN.Width = 120;
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
            rangeSettingDto1.Max = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.DISP_NUMBER.RangeSetting = rangeSettingDto1;
            this.DISP_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DISP_NUMBER.RegistCheckMethod")));
            this.DISP_NUMBER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DISP_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DISP_NUMBER.ToolTipText = "【1～5】のいずれかで入力してください。";
            this.DISP_NUMBER.Width = 80;
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
            this.OUTPUT_KBN_NAME.Width = 160;
            // 
            // PATTERN_NAME
            // 
            this.PATTERN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.PATTERN_NAME.DefaultCellStyle = dataGridViewCellStyle6;
            this.PATTERN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.FocusOutCheckMethod")));
            this.PATTERN_NAME.HeaderText = "パターン名";
            this.PATTERN_NAME.Name = "PATTERN_NAME";
            this.PATTERN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_NAME.PopupSearchSendParams")));
            this.PATTERN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_NAME.popupWindowSetting")));
            this.PATTERN_NAME.ReadOnly = true;
            this.PATTERN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.RegistCheckMethod")));
            this.PATTERN_NAME.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.PATTERN_NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PATTERN_NAME.ToolTipText = "ダブルクリックで一覧出力項目選択画面に切り替わります。";
            this.PATTERN_NAME.Width = 160;
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
            // DENSHU_KBN_CD
            // 
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.DENSHU_KBN_CD.DefaultCellStyle = dataGridViewCellStyle13;
            this.DENSHU_KBN_CD.HeaderText = "DENSHU_KBN_CD";
            this.DENSHU_KBN_CD.Name = "DENSHU_KBN_CD";
            this.DENSHU_KBN_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DENSHU_KBN_CD.Visible = false;
            // 
            // TIME_STAMP_MOP
            // 
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.TIME_STAMP_MOP.DefaultCellStyle = dataGridViewCellStyle14;
            this.TIME_STAMP_MOP.HeaderText = "TIME_STAMP_MOP";
            this.TIME_STAMP_MOP.Name = "TIME_STAMP_MOP";
            this.TIME_STAMP_MOP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIME_STAMP_MOP.Visible = false;
            // 
            // TIME_STAMP_MOPK
            // 
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.TIME_STAMP_MOPK.DefaultCellStyle = dataGridViewCellStyle15;
            this.TIME_STAMP_MOPK.HeaderText = "TIME_STAMP_MOPK";
            this.TIME_STAMP_MOPK.Name = "TIME_STAMP_MOPK";
            this.TIME_STAMP_MOPK.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.TIME_STAMP_MOPK.Visible = false;
            // 
            // DISP_NUMBER_DEL
            // 
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.DISP_NUMBER_DEL.DefaultCellStyle = dataGridViewCellStyle16;
            this.DISP_NUMBER_DEL.HeaderText = "DISP_NUMBER_DEL";
            this.DISP_NUMBER_DEL.Name = "DISP_NUMBER_DEL";
            this.DISP_NUMBER_DEL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DISP_NUMBER_DEL.Visible = false;
            // 
            // DEFAULT_KBN_DEL
            // 
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            this.DEFAULT_KBN_DEL.DefaultCellStyle = dataGridViewCellStyle17;
            this.DEFAULT_KBN_DEL.HeaderText = "DEFAULT_KBN_DEL";
            this.DEFAULT_KBN_DEL.Name = "DEFAULT_KBN_DEL";
            this.DEFAULT_KBN_DEL.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DEFAULT_KBN_DEL.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.PatternIchiran);
            this.Controls.Add(this.CONDITION_VALUE);
            this.Controls.Add(this.label1);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.PatternIchiran)).EndInit();
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

                if (this.PatternIchiran.CurrentCell.ColumnIndex == 2)
                {
                    //e.Control.ImeMode = ImeMode.Alpha;//2012.12.26 touti つくば　No.1058
                    e.Control.ImeMode = ImeMode.Disable;

                    e.Control.KeyPress += new KeyPressEventHandler(TextBoxDec_KeyPress);
                }

                if (this.PatternIchiran.CurrentCell.ColumnIndex == 3 || this.PatternIchiran.CurrentCell.ColumnIndex == 4)
                {
                    e.Control.ImeMode = ImeMode.Hiragana;
                    e.Control.KeyPress += new KeyPressEventHandler(TextBoxText_KeyPress);
                }
            }
        }

        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox CONDITION_ITEM;
        internal r_framework.CustomControl.CustomTextBox CONDITION_VALUE;
        public r_framework.CustomControl.CustomDataGridView PatternIchiran;
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
        private DataGridViewTextBoxColumn DENSHU_KBN_CD;
        private DataGridViewTextBoxColumn TIME_STAMP_MOP;
        private DataGridViewTextBoxColumn TIME_STAMP_MOPK;
        private DataGridViewTextBoxColumn DISP_NUMBER_DEL;
        private DataGridViewTextBoxColumn DEFAULT_KBN_DEL;
    }
}