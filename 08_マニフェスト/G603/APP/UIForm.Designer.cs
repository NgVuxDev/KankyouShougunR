namespace Shougun.Core.PaperManifest.JissekiHokokuSisetsu
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            this.label38 = new System.Windows.Forms.Label();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.dgv_csv = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.label5 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.rdoSAISYUUSYOUBUN = new r_framework.CustomControl.CustomRadioButton();
            this.rdoJIGYOUJOU = new r_framework.CustomControl.CustomRadioButton();
            this.txtJIGYOUJOU_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label11 = new System.Windows.Forms.Label();
            this.CHIIKI_NAME = new r_framework.CustomControl.CustomTextBox();
            this.bt_chiiki_search = new r_framework.CustomControl.CustomPopupOpenButton();
            this.CHIIKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbtn_HaisyutuJigyoubaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.label6 = new System.Windows.Forms.Label();
            this.HOUKOKU_GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HOUKOKU_GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.cbtn_HaisyutuGyousyaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.HOUKOKU_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.HOUKOKU_GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.HOZON_NAME = new r_framework.CustomControl.CustomTextBox();
            this.customPanel3 = new r_framework.CustomControl.CustomPanel();
            this.rdoHyoujun1 = new r_framework.CustomControl.CustomRadioButton();
            this.txtHOUKOKU_SHOSHIKI = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel5 = new r_framework.CustomControl.CustomPanel();
            this.rdoSTOKUBETU = new r_framework.CustomControl.CustomRadioButton();
            this.rdoHUTUWU = new r_framework.CustomControl.CustomRadioButton();
            this.txtTOKUBETSU_KANRI_KBN = new r_framework.CustomControl.CustomNumericTextBox2();
            this.TEISHUTU_DATE = new r_framework.CustomControl.CustomDateTimePicker();
            this.DATE_BEGIN = new r_framework.CustomControl.CustomDateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.DATE_END = new r_framework.CustomControl.CustomDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label611 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.customPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_csv)).BeginInit();
            this.customPanel2.SuspendLayout();
            this.customPanel3.SuspendLayout();
            this.customPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(-92, -162);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(20, 20);
            this.label38.TabIndex = 515;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.customPanel1.Controls.Add(this.dgv_csv);
            this.customPanel1.Controls.Add(this.label5);
            this.customPanel1.Controls.Add(this.label19);
            this.customPanel1.Controls.Add(this.customPanel2);
            this.customPanel1.Controls.Add(this.label11);
            this.customPanel1.Controls.Add(this.CHIIKI_NAME);
            this.customPanel1.Controls.Add(this.bt_chiiki_search);
            this.customPanel1.Controls.Add(this.CHIIKI_CD);
            this.customPanel1.Controls.Add(this.label4);
            this.customPanel1.Controls.Add(this.cbtn_HaisyutuJigyoubaSan);
            this.customPanel1.Controls.Add(this.label6);
            this.customPanel1.Controls.Add(this.HOUKOKU_GENBA_CD);
            this.customPanel1.Controls.Add(this.HOUKOKU_GENBA_NAME);
            this.customPanel1.Controls.Add(this.cbtn_HaisyutuGyousyaSan);
            this.customPanel1.Controls.Add(this.HOUKOKU_GYOUSHA_CD);
            this.customPanel1.Controls.Add(this.HOUKOKU_GYOUSHA_NAME);
            this.customPanel1.Controls.Add(this.label3);
            this.customPanel1.Controls.Add(this.HOZON_NAME);
            this.customPanel1.Controls.Add(this.customPanel3);
            this.customPanel1.Controls.Add(this.customPanel5);
            this.customPanel1.Controls.Add(this.TEISHUTU_DATE);
            this.customPanel1.Controls.Add(this.DATE_BEGIN);
            this.customPanel1.Controls.Add(this.label7);
            this.customPanel1.Controls.Add(this.label1);
            this.customPanel1.Controls.Add(this.DATE_END);
            this.customPanel1.Controls.Add(this.label2);
            this.customPanel1.Controls.Add(this.label9);
            this.customPanel1.Controls.Add(this.label611);
            this.customPanel1.Controls.Add(this.label10);
            this.customPanel1.Location = new System.Drawing.Point(1, 22);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(1080, 446);
            this.customPanel1.TabIndex = 724;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(362, 184);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 20;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(40, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10027;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // dgv_csv
            // 
            this.dgv_csv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_csv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_csv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgv_csv.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_csv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgv_csv.EnableHeadersVisualStyles = false;
            this.dgv_csv.GridColor = System.Drawing.Color.White;
            this.dgv_csv.IsReload = false;
            this.dgv_csv.LinkedDataPanelName = null;
            this.dgv_csv.Location = new System.Drawing.Point(798, 200);
            this.dgv_csv.MultiSelect = false;
            this.dgv_csv.Name = "dgv_csv";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_csv.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgv_csv.RowHeadersVisible = false;
            this.dgv_csv.RowTemplate.Height = 21;
            this.dgv_csv.ShowCellToolTips = false;
            this.dgv_csv.Size = new System.Drawing.Size(106, 64);
            this.dgv_csv.TabIndex = 732;
            this.dgv_csv.TabStop = false;
            this.dgv_csv.Visible = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(1, 426);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(333, 20);
            this.label5.TabIndex = 731;
            this.label5.Text = "※処理施設ではCSV出力の項目選択はありません";
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.label19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label19.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label19.ForeColor = System.Drawing.Color.Black;
            this.label19.Location = new System.Drawing.Point(3, 161);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(915, 20);
            this.label19.TabIndex = 731;
            this.label19.Text = "※再生処理分（一次マニフェスト）と処分委託分（二次マニフェスト）を抽出する場合に対象とする事業場抽出条件を選択してください。";
            // 
            // customPanel2
            // 
            this.customPanel2.Controls.Add(this.rdoSAISYUUSYOUBUN);
            this.customPanel2.Controls.Add(this.rdoJIGYOUJOU);
            this.customPanel2.Controls.Add(this.txtJIGYOUJOU_KBN);
            this.customPanel2.Location = new System.Drawing.Point(119, 138);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(495, 20);
            this.customPanel2.TabIndex = 6;
            // 
            // rdoSAISYUUSYOUBUN
            // 
            this.rdoSAISYUUSYOUBUN.AutoSize = true;
            this.rdoSAISYUUSYOUBUN.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoSAISYUUSYOUBUN.DisplayItemName = "特別管理型";
            this.rdoSAISYUUSYOUBUN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSAISYUUSYOUBUN.FocusOutCheckMethod")));
            this.rdoSAISYUUSYOUBUN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoSAISYUUSYOUBUN.LinkedTextBox = "txtJIGYOUJOU_KBN";
            this.rdoSAISYUUSYOUBUN.Location = new System.Drawing.Point(223, 1);
            this.rdoSAISYUUSYOUBUN.Name = "rdoSAISYUUSYOUBUN";
            this.rdoSAISYUUSYOUBUN.PopupAfterExecute = null;
            this.rdoSAISYUUSYOUBUN.PopupBeforeExecute = null;
            this.rdoSAISYUUSYOUBUN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoSAISYUUSYOUBUN.PopupSearchSendParams")));
            this.rdoSAISYUUSYOUBUN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoSAISYUUSYOUBUN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoSAISYUUSYOUBUN.popupWindowSetting")));
            this.rdoSAISYUUSYOUBUN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSAISYUUSYOUBUN.RegistCheckMethod")));
            this.rdoSAISYUUSYOUBUN.ShortItemName = "特別管理型";
            this.rdoSAISYUUSYOUBUN.Size = new System.Drawing.Size(263, 17);
            this.rdoSAISYUUSYOUBUN.TabIndex = 78;
            this.rdoSAISYUUSYOUBUN.Tag = "特別管理型を対象とする場合チェックをつけてください";
            this.rdoSAISYUUSYOUBUN.Text = "2.最終処分を行った場所を対象に抽出";
            this.rdoSAISYUUSYOUBUN.UseVisualStyleBackColor = true;
            this.rdoSAISYUUSYOUBUN.Value = "2";
            // 
            // rdoJIGYOUJOU
            // 
            this.rdoJIGYOUJOU.AutoSize = true;
            this.rdoJIGYOUJOU.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoJIGYOUJOU.DisplayItemName = "処分事業場";
            this.rdoJIGYOUJOU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoJIGYOUJOU.FocusOutCheckMethod")));
            this.rdoJIGYOUJOU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoJIGYOUJOU.LinkedTextBox = "txtJIGYOUJOU_KBN";
            this.rdoJIGYOUJOU.Location = new System.Drawing.Point(25, 1);
            this.rdoJIGYOUJOU.Name = "rdoJIGYOUJOU";
            this.rdoJIGYOUJOU.PopupAfterExecute = null;
            this.rdoJIGYOUJOU.PopupBeforeExecute = null;
            this.rdoJIGYOUJOU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoJIGYOUJOU.PopupSearchSendParams")));
            this.rdoJIGYOUJOU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoJIGYOUJOU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoJIGYOUJOU.popupWindowSetting")));
            this.rdoJIGYOUJOU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoJIGYOUJOU.RegistCheckMethod")));
            this.rdoJIGYOUJOU.ShortItemName = "処分事業場";
            this.rdoJIGYOUJOU.Size = new System.Drawing.Size(193, 17);
            this.rdoJIGYOUJOU.TabIndex = 74;
            this.rdoJIGYOUJOU.Tag = "普通産廃を対象とする場合チェックをつけてください";
            this.rdoJIGYOUJOU.Text = "1.処分事業場を対象に抽出";
            this.rdoJIGYOUJOU.UseVisualStyleBackColor = true;
            this.rdoJIGYOUJOU.Value = "1";
            // 
            // txtJIGYOUJOU_KBN
            // 
            this.txtJIGYOUJOU_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.txtJIGYOUJOU_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtJIGYOUJOU_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtJIGYOUJOU_KBN.DisplayItemName = "処分事業場抽出条件";
            this.txtJIGYOUJOU_KBN.DisplayPopUp = null;
            this.txtJIGYOUJOU_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtJIGYOUJOU_KBN.FocusOutCheckMethod")));
            this.txtJIGYOUJOU_KBN.ForeColor = System.Drawing.Color.Black;
            this.txtJIGYOUJOU_KBN.IsInputErrorOccured = false;
            this.txtJIGYOUJOU_KBN.LinkedRadioButtonArray = new string[] {
        "rdoJIGYOUJOU",
        "rdoSAISYUUSYOUBUN"};
            this.txtJIGYOUJOU_KBN.Location = new System.Drawing.Point(0, 0);
            this.txtJIGYOUJOU_KBN.Name = "txtJIGYOUJOU_KBN";
            this.txtJIGYOUJOU_KBN.PopupAfterExecute = null;
            this.txtJIGYOUJOU_KBN.PopupBeforeExecute = null;
            this.txtJIGYOUJOU_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtJIGYOUJOU_KBN.PopupSearchSendParams")));
            this.txtJIGYOUJOU_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtJIGYOUJOU_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtJIGYOUJOU_KBN.popupWindowSetting")));
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
            this.txtJIGYOUJOU_KBN.RangeSetting = rangeSettingDto1;
            this.txtJIGYOUJOU_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtJIGYOUJOU_KBN.RegistCheckMethod")));
            this.txtJIGYOUJOU_KBN.Size = new System.Drawing.Size(20, 20);
            this.txtJIGYOUJOU_KBN.TabIndex = 6;
            this.txtJIGYOUJOU_KBN.Tag = "処分事業場の抽出条件を選択してください";
            this.txtJIGYOUJOU_KBN.WordWrap = false;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(4, 137);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(111, 20);
            this.label11.TabIndex = 730;
            this.label11.Text = "処分事業場抽出条件";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CHIIKI_NAME
            // 
            this.CHIIKI_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CHIIKI_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CHIIKI_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.CHIIKI_NAME.DBFieldsName = "CHIIKI_NAME_RYAKU";
            this.CHIIKI_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHIIKI_NAME.DisplayItemName = "地域名";
            this.CHIIKI_NAME.DisplayPopUp = null;
            this.CHIIKI_NAME.ErrorMessage = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.CHIIKI_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHIIKI_NAME.FocusOutCheckMethod")));
            this.CHIIKI_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CHIIKI_NAME.ForeColor = System.Drawing.Color.Black;
            this.CHIIKI_NAME.GetCodeMasterField = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.CHIIKI_NAME.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.CHIIKI_NAME.IsInputErrorOccured = false;
            this.CHIIKI_NAME.ItemDefinedTypes = "varchar";
            this.CHIIKI_NAME.Location = new System.Drawing.Point(168, 184);
            this.CHIIKI_NAME.MaxLength = 0;
            this.CHIIKI_NAME.Name = "CHIIKI_NAME";
            this.CHIIKI_NAME.PopupAfterExecute = null;
            this.CHIIKI_NAME.PopupBeforeExecute = null;
            this.CHIIKI_NAME.PopupGetMasterField = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.CHIIKI_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHIIKI_NAME.PopupSearchSendParams")));
            this.CHIIKI_NAME.PopupSetFormField = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.CHIIKI_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CHIIKI_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHIIKI_NAME.popupWindowSetting")));
            this.CHIIKI_NAME.ReadOnly = true;
            this.CHIIKI_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHIIKI_NAME.RegistCheckMethod")));
            this.CHIIKI_NAME.SetFormField = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.CHIIKI_NAME.ShortItemName = "地域名";
            this.CHIIKI_NAME.Size = new System.Drawing.Size(150, 20);
            this.CHIIKI_NAME.TabIndex = 727;
            this.CHIIKI_NAME.TabStop = false;
            this.CHIIKI_NAME.Tag = "地域が表示されます";
            // 
            // bt_chiiki_search
            // 
            this.bt_chiiki_search.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_chiiki_search.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.bt_chiiki_search.DBFieldsName = null;
            this.bt_chiiki_search.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_chiiki_search.DisplayItemName = "地域検索";
            this.bt_chiiki_search.DisplayPopUp = null;
            this.bt_chiiki_search.ErrorMessage = null;
            this.bt_chiiki_search.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("bt_chiiki_search.FocusOutCheckMethod")));
            this.bt_chiiki_search.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.bt_chiiki_search.GetCodeMasterField = null;
            this.bt_chiiki_search.Image = ((System.Drawing.Image)(resources.GetObject("bt_chiiki_search.Image")));
            this.bt_chiiki_search.ItemDefinedTypes = null;
            this.bt_chiiki_search.LinkedSettingTextBox = null;
            this.bt_chiiki_search.LinkedTextBoxs = null;
            this.bt_chiiki_search.Location = new System.Drawing.Point(322, 184);
            this.bt_chiiki_search.Name = "bt_chiiki_search";
            this.bt_chiiki_search.PopupAfterExecute = null;
            this.bt_chiiki_search.PopupBeforeExecute = null;
            this.bt_chiiki_search.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("bt_chiiki_search.PopupSearchSendParams")));
            this.bt_chiiki_search.PopupWindowId = r_framework.Const.WINDOW_ID.M_CHIIKI;
            this.bt_chiiki_search.PopupWindowName = "マスタ共通ポップアップ";
            this.bt_chiiki_search.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("bt_chiiki_search.popupWindowSetting")));
            this.bt_chiiki_search.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("bt_chiiki_search.RegistCheckMethod")));
            this.bt_chiiki_search.SearchDisplayFlag = 0;
            this.bt_chiiki_search.SetFormField = "CHIIKI_CD,CHIIKI_NAME";
            this.bt_chiiki_search.ShortItemName = "地域検索";
            this.bt_chiiki_search.Size = new System.Drawing.Size(22, 22);
            this.bt_chiiki_search.TabIndex = 728;
            this.bt_chiiki_search.TabStop = false;
            this.bt_chiiki_search.Tag = "地域検索画面を表示します";
            this.bt_chiiki_search.UseVisualStyleBackColor = true;
            this.bt_chiiki_search.ZeroPaddengFlag = false;
            // 
            // CHIIKI_CD
            // 
            this.CHIIKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.CHIIKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CHIIKI_CD.ChangeUpperCase = true;
            this.CHIIKI_CD.CharacterLimitList = null;
            this.CHIIKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.CHIIKI_CD.DBFieldsName = "CHIIKI_CD";
            this.CHIIKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.CHIIKI_CD.DisplayItemName = "提出先";
            this.CHIIKI_CD.DisplayPopUp = null;
            this.CHIIKI_CD.ErrorMessage = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.CHIIKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHIIKI_CD.FocusOutCheckMethod")));
            this.CHIIKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CHIIKI_CD.ForeColor = System.Drawing.Color.Black;
            this.CHIIKI_CD.GetCodeMasterField = "CHIIKI_CD,CHIIKI_NAME_RYAKU";
            this.CHIIKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CHIIKI_CD.IsInputErrorOccured = false;
            this.CHIIKI_CD.ItemDefinedTypes = "varchar";
            this.CHIIKI_CD.Location = new System.Drawing.Point(119, 184);
            this.CHIIKI_CD.MaxLength = 6;
            this.CHIIKI_CD.Name = "CHIIKI_CD";
            this.CHIIKI_CD.PopupAfterExecute = null;
            this.CHIIKI_CD.PopupBeforeExecute = null;
            this.CHIIKI_CD.PopupGetMasterField = "CHIIKI_CD,CHIIKI_NAME_RYAKU";
            this.CHIIKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CHIIKI_CD.PopupSearchSendParams")));
            this.CHIIKI_CD.PopupSetFormField = "CHIIKI_CD,CHIIKI_NAME";
            this.CHIIKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_CHIIKI;
            this.CHIIKI_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.CHIIKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CHIIKI_CD.popupWindowSetting")));
            this.CHIIKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CHIIKI_CD.RegistCheckMethod")));
            this.CHIIKI_CD.SetFormField = "CHIIKI_CD,CHIIKI_NAME";
            this.CHIIKI_CD.ShortItemName = "提出先";
            this.CHIIKI_CD.Size = new System.Drawing.Size(50, 20);
            this.CHIIKI_CD.TabIndex = 7;
            this.CHIIKI_CD.Tag = "提出先を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CHIIKI_CD.ZeroPaddengFlag = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 20);
            this.label4.TabIndex = 700;
            this.label4.Text = "報告事業場";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbtn_HaisyutuJigyoubaSan
            // 
            this.cbtn_HaisyutuJigyoubaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_HaisyutuJigyoubaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_HaisyutuJigyoubaSan.DBFieldsName = null;
            this.cbtn_HaisyutuJigyoubaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_HaisyutuJigyoubaSan.DisplayItemName = null;
            this.cbtn_HaisyutuJigyoubaSan.DisplayPopUp = null;
            this.cbtn_HaisyutuJigyoubaSan.ErrorMessage = null;
            this.cbtn_HaisyutuJigyoubaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.FocusOutCheckMethod")));
            this.cbtn_HaisyutuJigyoubaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_HaisyutuJigyoubaSan.GetCodeMasterField = null;
            this.cbtn_HaisyutuJigyoubaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.Image")));
            this.cbtn_HaisyutuJigyoubaSan.ItemDefinedTypes = null;
            this.cbtn_HaisyutuJigyoubaSan.LinkedSettingTextBox = null;
            this.cbtn_HaisyutuJigyoubaSan.LinkedTextBoxs = null;
            this.cbtn_HaisyutuJigyoubaSan.Location = new System.Drawing.Point(464, 110);
            this.cbtn_HaisyutuJigyoubaSan.Name = "cbtn_HaisyutuJigyoubaSan";
            this.cbtn_HaisyutuJigyoubaSan.PopupAfterExecute = null;
            this.cbtn_HaisyutuJigyoubaSan.PopupBeforeExecute = null;
            this.cbtn_HaisyutuJigyoubaSan.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams")));
            this.cbtn_HaisyutuJigyoubaSan.PopupSetFormField = "HOUKOKU_GENBA_CD,HOUKOKU_GENBA_NAME,HOUKOKU_GYOUSHA_CD,HOUKOKU_GYOUSHA_NAME";
            this.cbtn_HaisyutuJigyoubaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cbtn_HaisyutuJigyoubaSan.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cbtn_HaisyutuJigyoubaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.popupWindowSetting")));
            this.cbtn_HaisyutuJigyoubaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.RegistCheckMethod")));
            this.cbtn_HaisyutuJigyoubaSan.SearchDisplayFlag = 0;
            this.cbtn_HaisyutuJigyoubaSan.SetFormField = null;
            this.cbtn_HaisyutuJigyoubaSan.ShortItemName = null;
            this.cbtn_HaisyutuJigyoubaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_HaisyutuJigyoubaSan.TabIndex = 699;
            this.cbtn_HaisyutuJigyoubaSan.TabStop = false;
            this.cbtn_HaisyutuJigyoubaSan.Tag = "報告事業場の検索を行います";
            this.cbtn_HaisyutuJigyoubaSan.Text = " ";
            this.cbtn_HaisyutuJigyoubaSan.UseVisualStyleBackColor = false;
            this.cbtn_HaisyutuJigyoubaSan.ZeroPaddengFlag = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(4, 184);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 20);
            this.label6.TabIndex = 725;
            this.label6.Text = "提出先";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HOUKOKU_GENBA_CD
            // 
            this.HOUKOKU_GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HOUKOKU_GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOUKOKU_GENBA_CD.ChangeUpperCase = true;
            this.HOUKOKU_GENBA_CD.CharacterLimitList = null;
            this.HOUKOKU_GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HOUKOKU_GENBA_CD.DBFieldsName = "GENBA_CD";
            this.HOUKOKU_GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKU_GENBA_CD.DisplayItemName = "報告事業場";
            this.HOUKOKU_GENBA_CD.DisplayPopUp = null;
            this.HOUKOKU_GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_GENBA_CD.FocusOutCheckMethod")));
            this.HOUKOKU_GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HOUKOKU_GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.HOUKOKU_GENBA_CD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.HOUKOKU_GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HOUKOKU_GENBA_CD.IsInputErrorOccured = false;
            this.HOUKOKU_GENBA_CD.ItemDefinedTypes = "varchar";
            this.HOUKOKU_GENBA_CD.Location = new System.Drawing.Point(119, 111);
            this.HOUKOKU_GENBA_CD.MaxLength = 6;
            this.HOUKOKU_GENBA_CD.Name = "HOUKOKU_GENBA_CD";
            this.HOUKOKU_GENBA_CD.PopupAfterExecute = null;
            this.HOUKOKU_GENBA_CD.PopupBeforeExecute = null;
            this.HOUKOKU_GENBA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU";
            this.HOUKOKU_GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKU_GENBA_CD.PopupSearchSendParams")));
            this.HOUKOKU_GENBA_CD.PopupSendParams = new string[0];
            this.HOUKOKU_GENBA_CD.PopupSetFormField = "HOUKOKU_GYOUSHA_CD,HOUKOKU_GYOUSHA_NAME,HOUKOKU_GENBA_CD,HOUKOKU_GENBA_NAME";
            this.HOUKOKU_GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.HOUKOKU_GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.HOUKOKU_GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKU_GENBA_CD.popupWindowSetting")));
            this.HOUKOKU_GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_GENBA_CD.RegistCheckMethod")));
            this.HOUKOKU_GENBA_CD.SetFormField = "HOUKOKU_GENBA_CD,HOUKOKU_GENBA_NAME";
            this.HOUKOKU_GENBA_CD.Size = new System.Drawing.Size(60, 20);
            this.HOUKOKU_GENBA_CD.TabIndex = 4;
            this.HOUKOKU_GENBA_CD.Tag = "報告事業場CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HOUKOKU_GENBA_CD.ZeroPaddengFlag = true;
            // 
            // HOUKOKU_GENBA_NAME
            // 
            this.HOUKOKU_GENBA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HOUKOKU_GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOUKOKU_GENBA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.HOUKOKU_GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKU_GENBA_NAME.DisplayPopUp = null;
            this.HOUKOKU_GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_GENBA_NAME.FocusOutCheckMethod")));
            this.HOUKOKU_GENBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HOUKOKU_GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.HOUKOKU_GENBA_NAME.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HOUKOKU_GENBA_NAME.IsInputErrorOccured = false;
            this.HOUKOKU_GENBA_NAME.Location = new System.Drawing.Point(178, 111);
            this.HOUKOKU_GENBA_NAME.MaxLength = 0;
            this.HOUKOKU_GENBA_NAME.Name = "HOUKOKU_GENBA_NAME";
            this.HOUKOKU_GENBA_NAME.PopupAfterExecute = null;
            this.HOUKOKU_GENBA_NAME.PopupBeforeExecute = null;
            this.HOUKOKU_GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKU_GENBA_NAME.PopupSearchSendParams")));
            this.HOUKOKU_GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOUKOKU_GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKU_GENBA_NAME.popupWindowSetting")));
            this.HOUKOKU_GENBA_NAME.ReadOnly = true;
            this.HOUKOKU_GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_GENBA_NAME.RegistCheckMethod")));
            this.HOUKOKU_GENBA_NAME.Size = new System.Drawing.Size(285, 20);
            this.HOUKOKU_GENBA_NAME.TabIndex = 698;
            this.HOUKOKU_GENBA_NAME.TabStop = false;
            // 
            // cbtn_HaisyutuGyousyaSan
            // 
            this.cbtn_HaisyutuGyousyaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_HaisyutuGyousyaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_HaisyutuGyousyaSan.DBFieldsName = null;
            this.cbtn_HaisyutuGyousyaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_HaisyutuGyousyaSan.DisplayItemName = null;
            this.cbtn_HaisyutuGyousyaSan.DisplayPopUp = null;
            this.cbtn_HaisyutuGyousyaSan.ErrorMessage = null;
            this.cbtn_HaisyutuGyousyaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.FocusOutCheckMethod")));
            this.cbtn_HaisyutuGyousyaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_HaisyutuGyousyaSan.GetCodeMasterField = null;
            this.cbtn_HaisyutuGyousyaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_HaisyutuGyousyaSan.Image")));
            this.cbtn_HaisyutuGyousyaSan.ItemDefinedTypes = null;
            this.cbtn_HaisyutuGyousyaSan.LinkedSettingTextBox = null;
            this.cbtn_HaisyutuGyousyaSan.LinkedTextBoxs = null;
            this.cbtn_HaisyutuGyousyaSan.Location = new System.Drawing.Point(464, 79);
            this.cbtn_HaisyutuGyousyaSan.Name = "cbtn_HaisyutuGyousyaSan";
            this.cbtn_HaisyutuGyousyaSan.PopupAfterExecute = null;
            this.cbtn_HaisyutuGyousyaSan.PopupAfterExecuteMethod = "After_GyoushaPop";
            this.cbtn_HaisyutuGyousyaSan.PopupBeforeExecute = null;
            this.cbtn_HaisyutuGyousyaSan.PopupBeforeExecuteMethod = "Before_GyoushaPop";
            this.cbtn_HaisyutuGyousyaSan.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.cbtn_HaisyutuGyousyaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.PopupSearchSendParams")));
            this.cbtn_HaisyutuGyousyaSan.PopupSetFormField = "HOUKOKU_GYOUSHA_CD,HOUKOKU_GYOUSHA_NAME";
            this.cbtn_HaisyutuGyousyaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cbtn_HaisyutuGyousyaSan.PopupWindowName = "検索共通ポップアップ";
            this.cbtn_HaisyutuGyousyaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.popupWindowSetting")));
            this.cbtn_HaisyutuGyousyaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.RegistCheckMethod")));
            this.cbtn_HaisyutuGyousyaSan.SearchDisplayFlag = 0;
            this.cbtn_HaisyutuGyousyaSan.SetFormField = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.cbtn_HaisyutuGyousyaSan.ShortItemName = null;
            this.cbtn_HaisyutuGyousyaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_HaisyutuGyousyaSan.TabIndex = 697;
            this.cbtn_HaisyutuGyousyaSan.TabStop = false;
            this.cbtn_HaisyutuGyousyaSan.Tag = "報告事業者の検索を行います";
            this.cbtn_HaisyutuGyousyaSan.Text = " ";
            this.cbtn_HaisyutuGyousyaSan.UseVisualStyleBackColor = false;
            this.cbtn_HaisyutuGyousyaSan.ZeroPaddengFlag = false;
            // 
            // HOUKOKU_GYOUSHA_CD
            // 
            this.HOUKOKU_GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.HOUKOKU_GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOUKOKU_GYOUSHA_CD.ChangeUpperCase = true;
            this.HOUKOKU_GYOUSHA_CD.CharacterLimitList = null;
            this.HOUKOKU_GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.HOUKOKU_GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.HOUKOKU_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKU_GYOUSHA_CD.DisplayItemName = "報告事業者";
            this.HOUKOKU_GYOUSHA_CD.DisplayPopUp = null;
            this.HOUKOKU_GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_GYOUSHA_CD.FocusOutCheckMethod")));
            this.HOUKOKU_GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HOUKOKU_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.HOUKOKU_GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.HOUKOKU_GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.HOUKOKU_GYOUSHA_CD.IsInputErrorOccured = false;
            this.HOUKOKU_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.HOUKOKU_GYOUSHA_CD.Location = new System.Drawing.Point(118, 80);
            this.HOUKOKU_GYOUSHA_CD.MaxLength = 6;
            this.HOUKOKU_GYOUSHA_CD.Name = "HOUKOKU_GYOUSHA_CD";
            this.HOUKOKU_GYOUSHA_CD.PopupAfterExecute = null;
            this.HOUKOKU_GYOUSHA_CD.PopupAfterExecuteMethod = "After_GyoushaPop";
            this.HOUKOKU_GYOUSHA_CD.PopupBeforeExecute = null;
            this.HOUKOKU_GYOUSHA_CD.PopupBeforeExecuteMethod = "Before_GyoushaPop";
            this.HOUKOKU_GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.HOUKOKU_GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKU_GYOUSHA_CD.PopupSearchSendParams")));
            this.HOUKOKU_GYOUSHA_CD.PopupSendParams = new string[0];
            this.HOUKOKU_GYOUSHA_CD.PopupSetFormField = "HOUKOKU_GYOUSHA_CD,HOUKOKU_GYOUSHA_NAME";
            this.HOUKOKU_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.HOUKOKU_GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.HOUKOKU_GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKU_GYOUSHA_CD.popupWindowSetting")));
            this.HOUKOKU_GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_GYOUSHA_CD.RegistCheckMethod")));
            this.HOUKOKU_GYOUSHA_CD.SetFormField = "HOUKOKU_GYOUSHA_CD,HOUKOKU_GYOUSHA_NAME";
            this.HOUKOKU_GYOUSHA_CD.Size = new System.Drawing.Size(60, 20);
            this.HOUKOKU_GYOUSHA_CD.TabIndex = 3;
            this.HOUKOKU_GYOUSHA_CD.Tag = "報告事業者CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.HOUKOKU_GYOUSHA_CD.ZeroPaddengFlag = true;
            // 
            // HOUKOKU_GYOUSHA_NAME
            // 
            this.HOUKOKU_GYOUSHA_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.HOUKOKU_GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOUKOKU_GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.HOUKOKU_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOUKOKU_GYOUSHA_NAME.DisplayPopUp = null;
            this.HOUKOKU_GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_GYOUSHA_NAME.FocusOutCheckMethod")));
            this.HOUKOKU_GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HOUKOKU_GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.HOUKOKU_GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.HOUKOKU_GYOUSHA_NAME.IsInputErrorOccured = false;
            this.HOUKOKU_GYOUSHA_NAME.Location = new System.Drawing.Point(177, 80);
            this.HOUKOKU_GYOUSHA_NAME.MaxLength = 0;
            this.HOUKOKU_GYOUSHA_NAME.Name = "HOUKOKU_GYOUSHA_NAME";
            this.HOUKOKU_GYOUSHA_NAME.PopupAfterExecute = null;
            this.HOUKOKU_GYOUSHA_NAME.PopupBeforeExecute = null;
            this.HOUKOKU_GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOUKOKU_GYOUSHA_NAME.PopupSearchSendParams")));
            this.HOUKOKU_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOUKOKU_GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOUKOKU_GYOUSHA_NAME.popupWindowSetting")));
            this.HOUKOKU_GYOUSHA_NAME.ReadOnly = true;
            this.HOUKOKU_GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOUKOKU_GYOUSHA_NAME.RegistCheckMethod")));
            this.HOUKOKU_GYOUSHA_NAME.Size = new System.Drawing.Size(285, 20);
            this.HOUKOKU_GYOUSHA_NAME.TabIndex = 696;
            this.HOUKOKU_GYOUSHA_NAME.TabStop = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(3, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.TabIndex = 695;
            this.label3.Text = "報告事業者";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // HOZON_NAME
            // 
            this.HOZON_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.HOZON_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HOZON_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.HOZON_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HOZON_NAME.DisplayItemName = "帳票名";
            this.HOZON_NAME.DisplayPopUp = null;
            this.HOZON_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOZON_NAME.FocusOutCheckMethod")));
            this.HOZON_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HOZON_NAME.ForeColor = System.Drawing.Color.Black;
            this.HOZON_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HOZON_NAME.IsInputErrorOccured = false;
            this.HOZON_NAME.Location = new System.Drawing.Point(118, 18);
            this.HOZON_NAME.MaxLength = 40;
            this.HOZON_NAME.Name = "HOZON_NAME";
            this.HOZON_NAME.PopupAfterExecute = null;
            this.HOZON_NAME.PopupBeforeExecute = null;
            this.HOZON_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HOZON_NAME.PopupSearchSendParams")));
            this.HOZON_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HOZON_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HOZON_NAME.popupWindowSetting")));
            this.HOZON_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HOZON_NAME.RegistCheckMethod")));
            this.HOZON_NAME.Size = new System.Drawing.Size(285, 20);
            this.HOZON_NAME.TabIndex = 1;
            this.HOZON_NAME.Tag = "帳票名を入力してください";
            // 
            // customPanel3
            // 
            this.customPanel3.Controls.Add(this.rdoHyoujun1);
            this.customPanel3.Controls.Add(this.txtHOUKOKU_SHOSHIKI);
            this.customPanel3.Location = new System.Drawing.Point(119, 215);
            this.customPanel3.Name = "customPanel3";
            this.customPanel3.Size = new System.Drawing.Size(199, 20);
            this.customPanel3.TabIndex = 8;
            // 
            // rdoHyoujun1
            // 
            this.rdoHyoujun1.AutoSize = true;
            this.rdoHyoujun1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoHyoujun1.DisplayItemName = "合算";
            this.rdoHyoujun1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHyoujun1.FocusOutCheckMethod")));
            this.rdoHyoujun1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoHyoujun1.LinkedTextBox = "txtHOUKOKU_SHOSHIKI";
            this.rdoHyoujun1.Location = new System.Drawing.Point(29, 1);
            this.rdoHyoujun1.Name = "rdoHyoujun1";
            this.rdoHyoujun1.PopupAfterExecute = null;
            this.rdoHyoujun1.PopupBeforeExecute = null;
            this.rdoHyoujun1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoHyoujun1.PopupSearchSendParams")));
            this.rdoHyoujun1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoHyoujun1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoHyoujun1.popupWindowSetting")));
            this.rdoHyoujun1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHyoujun1.RegistCheckMethod")));
            this.rdoHyoujun1.ShortItemName = "標準様式1-9/1-10号";
            this.rdoHyoujun1.Size = new System.Drawing.Size(165, 17);
            this.rdoHyoujun1.TabIndex = 74;
            this.rdoHyoujun1.Tag = "普通産廃書式の場合チェックをつけてください";
            this.rdoHyoujun1.Text = "1.標準様式1-9/1-10号";
            this.rdoHyoujun1.UseVisualStyleBackColor = true;
            this.rdoHyoujun1.Value = "1";
            // 
            // txtHOUKOKU_SHOSHIKI
            // 
            this.txtHOUKOKU_SHOSHIKI.BackColor = System.Drawing.SystemColors.Window;
            this.txtHOUKOKU_SHOSHIKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHOUKOKU_SHOSHIKI.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHOUKOKU_SHOSHIKI.DisplayItemName = "提出書式";
            this.txtHOUKOKU_SHOSHIKI.DisplayPopUp = null;
            this.txtHOUKOKU_SHOSHIKI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHOUKOKU_SHOSHIKI.FocusOutCheckMethod")));
            this.txtHOUKOKU_SHOSHIKI.ForeColor = System.Drawing.Color.Black;
            this.txtHOUKOKU_SHOSHIKI.IsInputErrorOccured = false;
            this.txtHOUKOKU_SHOSHIKI.LinkedRadioButtonArray = new string[] {
        "rdoHyoujun1"};
            this.txtHOUKOKU_SHOSHIKI.Location = new System.Drawing.Point(0, 0);
            this.txtHOUKOKU_SHOSHIKI.Name = "txtHOUKOKU_SHOSHIKI";
            this.txtHOUKOKU_SHOSHIKI.PopupAfterExecute = null;
            this.txtHOUKOKU_SHOSHIKI.PopupBeforeExecute = null;
            this.txtHOUKOKU_SHOSHIKI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHOUKOKU_SHOSHIKI.PopupSearchSendParams")));
            this.txtHOUKOKU_SHOSHIKI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHOUKOKU_SHOSHIKI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHOUKOKU_SHOSHIKI.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            1,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHOUKOKU_SHOSHIKI.RangeSetting = rangeSettingDto2;
            this.txtHOUKOKU_SHOSHIKI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHOUKOKU_SHOSHIKI.RegistCheckMethod")));
            this.txtHOUKOKU_SHOSHIKI.Size = new System.Drawing.Size(20, 20);
            this.txtHOUKOKU_SHOSHIKI.TabIndex = 8;
            this.txtHOUKOKU_SHOSHIKI.Tag = "提出書式を指定してください";
            this.txtHOUKOKU_SHOSHIKI.WordWrap = false;
            // 
            // customPanel5
            // 
            this.customPanel5.Controls.Add(this.rdoSTOKUBETU);
            this.customPanel5.Controls.Add(this.rdoHUTUWU);
            this.customPanel5.Controls.Add(this.txtTOKUBETSU_KANRI_KBN);
            this.customPanel5.Location = new System.Drawing.Point(120, 246);
            this.customPanel5.Name = "customPanel5";
            this.customPanel5.Size = new System.Drawing.Size(315, 20);
            this.customPanel5.TabIndex = 11;
            // 
            // rdoSTOKUBETU
            // 
            this.rdoSTOKUBETU.AutoSize = true;
            this.rdoSTOKUBETU.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoSTOKUBETU.DisplayItemName = "特別管理型";
            this.rdoSTOKUBETU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSTOKUBETU.FocusOutCheckMethod")));
            this.rdoSTOKUBETU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoSTOKUBETU.LinkedTextBox = "txtTOKUBETSU_KANRI_KBN";
            this.rdoSTOKUBETU.Location = new System.Drawing.Point(173, 1);
            this.rdoSTOKUBETU.Name = "rdoSTOKUBETU";
            this.rdoSTOKUBETU.PopupAfterExecute = null;
            this.rdoSTOKUBETU.PopupBeforeExecute = null;
            this.rdoSTOKUBETU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoSTOKUBETU.PopupSearchSendParams")));
            this.rdoSTOKUBETU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoSTOKUBETU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoSTOKUBETU.popupWindowSetting")));
            this.rdoSTOKUBETU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSTOKUBETU.RegistCheckMethod")));
            this.rdoSTOKUBETU.ShortItemName = "特別管理型";
            this.rdoSTOKUBETU.Size = new System.Drawing.Size(109, 17);
            this.rdoSTOKUBETU.TabIndex = 78;
            this.rdoSTOKUBETU.Tag = "特別管理型を対象とする場合チェックをつけてください";
            this.rdoSTOKUBETU.Text = "2.特別管理型";
            this.rdoSTOKUBETU.UseVisualStyleBackColor = true;
            this.rdoSTOKUBETU.Value = "2";
            // 
            // rdoHUTUWU
            // 
            this.rdoHUTUWU.AutoSize = true;
            this.rdoHUTUWU.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoHUTUWU.DisplayItemName = "普通産業廃棄物";
            this.rdoHUTUWU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHUTUWU.FocusOutCheckMethod")));
            this.rdoHUTUWU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoHUTUWU.LinkedTextBox = "txtTOKUBETSU_KANRI_KBN";
            this.rdoHUTUWU.Location = new System.Drawing.Point(25, 1);
            this.rdoHUTUWU.Name = "rdoHUTUWU";
            this.rdoHUTUWU.PopupAfterExecute = null;
            this.rdoHUTUWU.PopupBeforeExecute = null;
            this.rdoHUTUWU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoHUTUWU.PopupSearchSendParams")));
            this.rdoHUTUWU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoHUTUWU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoHUTUWU.popupWindowSetting")));
            this.rdoHUTUWU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoHUTUWU.RegistCheckMethod")));
            this.rdoHUTUWU.ShortItemName = "普通産業廃棄物";
            this.rdoHUTUWU.Size = new System.Drawing.Size(137, 17);
            this.rdoHUTUWU.TabIndex = 74;
            this.rdoHUTUWU.Tag = "普通産廃を対象とする場合チェックをつけてください";
            this.rdoHUTUWU.Text = "1.普通産業廃棄物";
            this.rdoHUTUWU.UseVisualStyleBackColor = true;
            this.rdoHUTUWU.Value = "1";
            // 
            // txtTOKUBETSU_KANRI_KBN
            // 
            this.txtTOKUBETSU_KANRI_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.txtTOKUBETSU_KANRI_KBN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTOKUBETSU_KANRI_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtTOKUBETSU_KANRI_KBN.DisplayItemName = "廃棄物区分";
            this.txtTOKUBETSU_KANRI_KBN.DisplayPopUp = null;
            this.txtTOKUBETSU_KANRI_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTOKUBETSU_KANRI_KBN.FocusOutCheckMethod")));
            this.txtTOKUBETSU_KANRI_KBN.ForeColor = System.Drawing.Color.Black;
            this.txtTOKUBETSU_KANRI_KBN.IsInputErrorOccured = false;
            this.txtTOKUBETSU_KANRI_KBN.LinkedRadioButtonArray = new string[] {
        "rdoHUTUWU",
        "rdoSTOKUBETU"};
            this.txtTOKUBETSU_KANRI_KBN.Location = new System.Drawing.Point(0, 0);
            this.txtTOKUBETSU_KANRI_KBN.Name = "txtTOKUBETSU_KANRI_KBN";
            this.txtTOKUBETSU_KANRI_KBN.PopupAfterExecute = null;
            this.txtTOKUBETSU_KANRI_KBN.PopupBeforeExecute = null;
            this.txtTOKUBETSU_KANRI_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtTOKUBETSU_KANRI_KBN.PopupSearchSendParams")));
            this.txtTOKUBETSU_KANRI_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtTOKUBETSU_KANRI_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtTOKUBETSU_KANRI_KBN.popupWindowSetting")));
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
            this.txtTOKUBETSU_KANRI_KBN.RangeSetting = rangeSettingDto3;
            this.txtTOKUBETSU_KANRI_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtTOKUBETSU_KANRI_KBN.RegistCheckMethod")));
            this.txtTOKUBETSU_KANRI_KBN.Size = new System.Drawing.Size(20, 20);
            this.txtTOKUBETSU_KANRI_KBN.TabIndex = 11;
            this.txtTOKUBETSU_KANRI_KBN.Tag = "廃棄物区分を指定してください";
            this.txtTOKUBETSU_KANRI_KBN.WordWrap = false;
            // 
            // TEISHUTU_DATE
            // 
            this.TEISHUTU_DATE.BackColor = System.Drawing.SystemColors.Window;
            this.TEISHUTU_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TEISHUTU_DATE.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.TEISHUTU_DATE.Checked = false;
            this.TEISHUTU_DATE.CustomFormat = "yyyy/MM/dd(ddd)";
            this.TEISHUTU_DATE.DateTimeNowYear = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.TEISHUTU_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.TEISHUTU_DATE.DisplayItemName = "伝票日付範囲指定（From）";
            this.TEISHUTU_DATE.DisplayPopUp = null;
            this.TEISHUTU_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEISHUTU_DATE.FocusOutCheckMethod")));
            this.TEISHUTU_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TEISHUTU_DATE.ForeColor = System.Drawing.Color.Black;
            this.TEISHUTU_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.TEISHUTU_DATE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TEISHUTU_DATE.IsInputErrorOccured = false;
            this.TEISHUTU_DATE.Location = new System.Drawing.Point(119, 49);
            this.TEISHUTU_DATE.MaxLength = 10;
            this.TEISHUTU_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.TEISHUTU_DATE.Name = "TEISHUTU_DATE";
            this.TEISHUTU_DATE.NullValue = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.TEISHUTU_DATE.PopupAfterExecute = null;
            this.TEISHUTU_DATE.PopupBeforeExecute = null;
            this.TEISHUTU_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TEISHUTU_DATE.PopupSearchSendParams")));
            this.TEISHUTU_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TEISHUTU_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TEISHUTU_DATE.popupWindowSetting")));
            this.TEISHUTU_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TEISHUTU_DATE.RegistCheckMethod")));
            this.TEISHUTU_DATE.Size = new System.Drawing.Size(138, 20);
            this.TEISHUTU_DATE.TabIndex = 2;
            this.TEISHUTU_DATE.Tag = "提出日を入力してください";
            this.TEISHUTU_DATE.Text = "2013/12/11(水)";
            this.TEISHUTU_DATE.Value = new System.DateTime(2013, 12, 11, 0, 0, 0, 0);
            // 
            // DATE_BEGIN
            // 
            this.DATE_BEGIN.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_BEGIN.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_BEGIN.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_BEGIN.Checked = false;
            this.DATE_BEGIN.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DATE_BEGIN.DateTimeNowYear = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.DATE_BEGIN.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_BEGIN.DisplayItemName = "対象期間（From）";
            this.DATE_BEGIN.DisplayPopUp = null;
            this.DATE_BEGIN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_BEGIN.FocusOutCheckMethod")));
            this.DATE_BEGIN.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_BEGIN.ForeColor = System.Drawing.Color.Black;
            this.DATE_BEGIN.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_BEGIN.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_BEGIN.IsInputErrorOccured = false;
            this.DATE_BEGIN.Location = new System.Drawing.Point(120, 275);
            this.DATE_BEGIN.MaxLength = 10;
            this.DATE_BEGIN.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_BEGIN.Name = "DATE_BEGIN";
            this.DATE_BEGIN.NullValue = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.DATE_BEGIN.PopupAfterExecute = null;
            this.DATE_BEGIN.PopupBeforeExecute = null;
            this.DATE_BEGIN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_BEGIN.PopupSearchSendParams")));
            this.DATE_BEGIN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_BEGIN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_BEGIN.popupWindowSetting")));
            this.DATE_BEGIN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_BEGIN.RegistCheckMethod")));
            this.DATE_BEGIN.Size = new System.Drawing.Size(138, 20);
            this.DATE_BEGIN.TabIndex = 12;
            this.DATE_BEGIN.Tag = "対象期間を入力してください";
            this.DATE_BEGIN.Text = "2013/12/11(水)";
            this.DATE_BEGIN.Value = new System.DateTime(2013, 12, 11, 0, 0, 0, 0);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(3, 215);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 20);
            this.label7.TabIndex = 607;
            this.label7.Text = "提出書式";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(2, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 20);
            this.label1.TabIndex = 606;
            this.label1.Text = "帳票名";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DATE_END
            // 
            this.DATE_END.BackColor = System.Drawing.SystemColors.Window;
            this.DATE_END.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DATE_END.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.DATE_END.Checked = false;
            this.DATE_END.CustomFormat = "yyyy/MM/dd(ddd)";
            this.DATE_END.DateTimeNowYear = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.DATE_END.DefaultBackColor = System.Drawing.Color.Empty;
            this.DATE_END.DisplayItemName = "対象期間（To）";
            this.DATE_END.DisplayPopUp = null;
            this.DATE_END.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_END.FocusOutCheckMethod")));
            this.DATE_END.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.DATE_END.ForeColor = System.Drawing.Color.Black;
            this.DATE_END.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.DATE_END.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.DATE_END.IsInputErrorOccured = false;
            this.DATE_END.Location = new System.Drawing.Point(296, 275);
            this.DATE_END.MaxLength = 10;
            this.DATE_END.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.DATE_END.Name = "DATE_END";
            this.DATE_END.NullValue = global::Shougun.Core.PaperManifest.JissekiHokokuSisetsu.Properties.Resources.String1;
            this.DATE_END.PopupAfterExecute = null;
            this.DATE_END.PopupBeforeExecute = null;
            this.DATE_END.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DATE_END.PopupSearchSendParams")));
            this.DATE_END.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DATE_END.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DATE_END.popupWindowSetting")));
            this.DATE_END.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DATE_END.RegistCheckMethod")));
            this.DATE_END.Size = new System.Drawing.Size(138, 20);
            this.DATE_END.TabIndex = 13;
            this.DATE_END.Tag = "対象期間を入力してください";
            this.DATE_END.Text = "2013/12/11(水)";
            this.DATE_END.Value = new System.DateTime(2013, 12, 11, 0, 0, 0, 0);
            this.DATE_END.DoubleClick += new System.EventHandler(this.DATE_END_DoubleClick);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(113, 20);
            this.label2.TabIndex = 606;
            this.label2.Text = "提出日";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(4, 246);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(113, 20);
            this.label9.TabIndex = 604;
            this.label9.Text = "廃棄物区分";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label611
            // 
            this.label611.AutoSize = true;
            this.label611.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label611.Location = new System.Drawing.Point(266, 278);
            this.label611.Name = "label611";
            this.label611.Size = new System.Drawing.Size(21, 13);
            this.label611.TabIndex = 655;
            this.label611.Text = "～";
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(4, 275);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 20);
            this.label10.TabIndex = 671;
            this.label10.Text = "対象期間";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1100, 507);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.label38);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "UIForm";
            this.customPanel1.ResumeLayout(false);
            this.customPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_csv)).EndInit();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanel3.ResumeLayout(false);
            this.customPanel3.PerformLayout();
            this.customPanel5.ResumeLayout(false);
            this.customPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label38;
        private r_framework.CustomControl.CustomPanel customPanel1;
        public r_framework.CustomControl.CustomDateTimePicker DATE_BEGIN;
        public System.Windows.Forms.Label label7;
        public r_framework.CustomControl.CustomDateTimePicker DATE_END;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label611;
        private System.Windows.Forms.Label label10;
        public r_framework.CustomControl.CustomDateTimePicker TEISHUTU_DATE;
        private r_framework.CustomControl.CustomPanel customPanel3;
        private r_framework.CustomControl.CustomRadioButton rdoHyoujun1;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtHOUKOKU_SHOSHIKI;
        private r_framework.CustomControl.CustomPanel customPanel5;
        private r_framework.CustomControl.CustomRadioButton rdoSTOKUBETU;
        private r_framework.CustomControl.CustomRadioButton rdoHUTUWU;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtTOKUBETSU_KANRI_KBN;
        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox HOZON_NAME;
        public System.Windows.Forms.Label label4;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_HaisyutuJigyoubaSan;
        internal r_framework.CustomControl.CustomAlphaNumTextBox HOUKOKU_GENBA_CD;
        internal r_framework.CustomControl.CustomTextBox HOUKOKU_GENBA_NAME;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_HaisyutuGyousyaSan;
        internal r_framework.CustomControl.CustomAlphaNumTextBox HOUKOKU_GYOUSHA_CD;
        internal r_framework.CustomControl.CustomTextBox HOUKOKU_GYOUSHA_NAME;
        public System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        internal r_framework.CustomControl.CustomTextBox CHIIKI_NAME;
        internal r_framework.CustomControl.CustomPopupOpenButton bt_chiiki_search;
        internal r_framework.CustomControl.CustomAlphaNumTextBox CHIIKI_CD;
        private r_framework.CustomControl.CustomPanel customPanel2;
        private r_framework.CustomControl.CustomRadioButton rdoSAISYUUSYOUBUN;
        private r_framework.CustomControl.CustomRadioButton rdoJIGYOUJOU;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtJIGYOUJOU_KBN;
        public System.Windows.Forms.Label label11;
        public System.Windows.Forms.Label label19;
        public System.Windows.Forms.Label label5;
        public r_framework.CustomControl.CustomDataGridView dgv_csv;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;

    }
}