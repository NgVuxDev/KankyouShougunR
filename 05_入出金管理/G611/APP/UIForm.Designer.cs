namespace Shougun.Core.ReceiptPayManagement.NyukinKeshikomiNyuryoku
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            this.txtNyukinSakiName = new r_framework.CustomControl.CustomTextBox();
            this.lblNyukinSaki = new System.Windows.Forms.Label();
            this.txtKonkaiNyukinGaku = new r_framework.CustomControl.CustomTextBox();
            this.lblKonkaiNyukinGaku = new System.Windows.Forms.Label();
            this.txtKonkaiKeshikomiGaku = new r_framework.CustomControl.CustomTextBox();
            this.lblKonkaiKeshikomiGaku = new System.Windows.Forms.Label();
            this.txtRuikeiMinyukinGaku = new r_framework.CustomControl.CustomTextBox();
            this.lblRuikeiMinyukinGaku = new System.Windows.Forms.Label();
            this.dgvNyukinDeleteMeisai = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.DELETE_FLG = new r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn();
            this.GYOUSHA_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GENBA_CD = new r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SEIKYUU_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SEIKYUUGAKU = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.KeshikomiGaku = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.keshikomiGakuTotal = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.MiKeshikomiGaku = new r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column();
            this.KESHIKOMI_BIKOU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SEIKYUU_NUMBER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.KAGAMI_NUMBER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SYSTEM_ID = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.KESHIKOMI_SEQ = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.NYUUKIN_NUMBER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.ROW_NUMBER = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNyukinDeleteMeisai)).BeginInit();
            this.SuspendLayout();
            // 
            // txtNyukinSakiName
            // 
            this.txtNyukinSakiName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtNyukinSakiName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNyukinSakiName.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtNyukinSakiName.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.txtNyukinSakiName.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtNyukinSakiName.DisplayItemName = "";
            this.txtNyukinSakiName.DisplayPopUp = null;
            this.txtNyukinSakiName.ErrorMessage = "";
            this.txtNyukinSakiName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNyukinSakiName.FocusOutCheckMethod")));
            this.txtNyukinSakiName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtNyukinSakiName.ForeColor = System.Drawing.Color.Black;
            this.txtNyukinSakiName.GetCodeMasterField = "";
            this.txtNyukinSakiName.IsInputErrorOccured = false;
            this.txtNyukinSakiName.ItemDefinedTypes = "";
            this.txtNyukinSakiName.Location = new System.Drawing.Point(120, 3);
            this.txtNyukinSakiName.MaxLength = 20;
            this.txtNyukinSakiName.Name = "txtNyukinSakiName";
            this.txtNyukinSakiName.PopupAfterExecute = null;
            this.txtNyukinSakiName.PopupBeforeExecute = null;
            this.txtNyukinSakiName.PopupGetMasterField = "";
            this.txtNyukinSakiName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtNyukinSakiName.PopupSearchSendParams")));
            this.txtNyukinSakiName.PopupSetFormField = "";
            this.txtNyukinSakiName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtNyukinSakiName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtNyukinSakiName.popupWindowSetting")));
            this.txtNyukinSakiName.ReadOnly = true;
            this.txtNyukinSakiName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtNyukinSakiName.RegistCheckMethod")));
            this.txtNyukinSakiName.SetFormField = "";
            this.txtNyukinSakiName.Size = new System.Drawing.Size(285, 20);
            this.txtNyukinSakiName.TabIndex = 10;
            this.txtNyukinSakiName.TabStop = false;
            this.txtNyukinSakiName.Tag = "　";
            // 
            // lblNyukinSaki
            // 
            this.lblNyukinSaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblNyukinSaki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNyukinSaki.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblNyukinSaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblNyukinSaki.ForeColor = System.Drawing.Color.White;
            this.lblNyukinSaki.Location = new System.Drawing.Point(5, 3);
            this.lblNyukinSaki.Name = "lblNyukinSaki";
            this.lblNyukinSaki.Size = new System.Drawing.Size(110, 20);
            this.lblNyukinSaki.TabIndex = 9;
            this.lblNyukinSaki.Text = "取引先※";
            this.lblNyukinSaki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtKonkaiNyukinGaku
            // 
            this.txtKonkaiNyukinGaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKonkaiNyukinGaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKonkaiNyukinGaku.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtKonkaiNyukinGaku.CustomFormatSetting = "#,##0";
            this.txtKonkaiNyukinGaku.DBFieldsName = "";
            this.txtKonkaiNyukinGaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKonkaiNyukinGaku.DisplayItemName = "今回入金額";
            this.txtKonkaiNyukinGaku.DisplayPopUp = null;
            this.txtKonkaiNyukinGaku.ErrorMessage = "";
            this.txtKonkaiNyukinGaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKonkaiNyukinGaku.FocusOutCheckMethod")));
            this.txtKonkaiNyukinGaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKonkaiNyukinGaku.ForeColor = System.Drawing.Color.Black;
            this.txtKonkaiNyukinGaku.FormatSetting = "カスタム";
            this.txtKonkaiNyukinGaku.GetCodeMasterField = "";
            this.txtKonkaiNyukinGaku.IsInputErrorOccured = false;
            this.txtKonkaiNyukinGaku.ItemDefinedTypes = "";
            this.txtKonkaiNyukinGaku.Location = new System.Drawing.Point(120, 33);
            this.txtKonkaiNyukinGaku.MaxLength = 20;
            this.txtKonkaiNyukinGaku.Name = "txtKonkaiNyukinGaku";
            this.txtKonkaiNyukinGaku.PopupAfterExecute = null;
            this.txtKonkaiNyukinGaku.PopupBeforeExecute = null;
            this.txtKonkaiNyukinGaku.PopupGetMasterField = "";
            this.txtKonkaiNyukinGaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKonkaiNyukinGaku.PopupSearchSendParams")));
            this.txtKonkaiNyukinGaku.PopupSetFormField = "";
            this.txtKonkaiNyukinGaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKonkaiNyukinGaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKonkaiNyukinGaku.popupWindowSetting")));
            this.txtKonkaiNyukinGaku.ReadOnly = true;
            this.txtKonkaiNyukinGaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKonkaiNyukinGaku.RegistCheckMethod")));
            this.txtKonkaiNyukinGaku.SetFormField = "";
            this.txtKonkaiNyukinGaku.Size = new System.Drawing.Size(150, 20);
            this.txtKonkaiNyukinGaku.TabIndex = 12;
            this.txtKonkaiNyukinGaku.TabStop = false;
            this.txtKonkaiNyukinGaku.Tag = "　";
            this.txtKonkaiNyukinGaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblKonkaiNyukinGaku
            // 
            this.lblKonkaiNyukinGaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKonkaiNyukinGaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKonkaiNyukinGaku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKonkaiNyukinGaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblKonkaiNyukinGaku.ForeColor = System.Drawing.Color.White;
            this.lblKonkaiNyukinGaku.Location = new System.Drawing.Point(5, 33);
            this.lblKonkaiNyukinGaku.Name = "lblKonkaiNyukinGaku";
            this.lblKonkaiNyukinGaku.Size = new System.Drawing.Size(110, 20);
            this.lblKonkaiNyukinGaku.TabIndex = 11;
            this.lblKonkaiNyukinGaku.Text = "今回入金額";
            this.lblKonkaiNyukinGaku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtKonkaiKeshikomiGaku
            // 
            this.txtKonkaiKeshikomiGaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtKonkaiKeshikomiGaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKonkaiKeshikomiGaku.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtKonkaiKeshikomiGaku.CustomFormatSetting = "#,##0";
            this.txtKonkaiKeshikomiGaku.DBFieldsName = "";
            this.txtKonkaiKeshikomiGaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtKonkaiKeshikomiGaku.DisplayItemName = "今回消込額";
            this.txtKonkaiKeshikomiGaku.DisplayPopUp = null;
            this.txtKonkaiKeshikomiGaku.ErrorMessage = "";
            this.txtKonkaiKeshikomiGaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKonkaiKeshikomiGaku.FocusOutCheckMethod")));
            this.txtKonkaiKeshikomiGaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtKonkaiKeshikomiGaku.ForeColor = System.Drawing.Color.Black;
            this.txtKonkaiKeshikomiGaku.FormatSetting = "カスタム";
            this.txtKonkaiKeshikomiGaku.GetCodeMasterField = "";
            this.txtKonkaiKeshikomiGaku.IsInputErrorOccured = false;
            this.txtKonkaiKeshikomiGaku.ItemDefinedTypes = "";
            this.txtKonkaiKeshikomiGaku.Location = new System.Drawing.Point(120, 55);
            this.txtKonkaiKeshikomiGaku.MaxLength = 20;
            this.txtKonkaiKeshikomiGaku.Name = "txtKonkaiKeshikomiGaku";
            this.txtKonkaiKeshikomiGaku.PopupAfterExecute = null;
            this.txtKonkaiKeshikomiGaku.PopupBeforeExecute = null;
            this.txtKonkaiKeshikomiGaku.PopupGetMasterField = "";
            this.txtKonkaiKeshikomiGaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtKonkaiKeshikomiGaku.PopupSearchSendParams")));
            this.txtKonkaiKeshikomiGaku.PopupSetFormField = "";
            this.txtKonkaiKeshikomiGaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtKonkaiKeshikomiGaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtKonkaiKeshikomiGaku.popupWindowSetting")));
            this.txtKonkaiKeshikomiGaku.ReadOnly = true;
            this.txtKonkaiKeshikomiGaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtKonkaiKeshikomiGaku.RegistCheckMethod")));
            this.txtKonkaiKeshikomiGaku.SetFormField = "";
            this.txtKonkaiKeshikomiGaku.Size = new System.Drawing.Size(150, 20);
            this.txtKonkaiKeshikomiGaku.TabIndex = 14;
            this.txtKonkaiKeshikomiGaku.TabStop = false;
            this.txtKonkaiKeshikomiGaku.Tag = "　";
            this.txtKonkaiKeshikomiGaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblKonkaiKeshikomiGaku
            // 
            this.lblKonkaiKeshikomiGaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblKonkaiKeshikomiGaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblKonkaiKeshikomiGaku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblKonkaiKeshikomiGaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblKonkaiKeshikomiGaku.ForeColor = System.Drawing.Color.White;
            this.lblKonkaiKeshikomiGaku.Location = new System.Drawing.Point(5, 55);
            this.lblKonkaiKeshikomiGaku.Name = "lblKonkaiKeshikomiGaku";
            this.lblKonkaiKeshikomiGaku.Size = new System.Drawing.Size(110, 20);
            this.lblKonkaiKeshikomiGaku.TabIndex = 13;
            this.lblKonkaiKeshikomiGaku.Text = "今回消込額";
            this.lblKonkaiKeshikomiGaku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtRuikeiMinyukinGaku
            // 
            this.txtRuikeiMinyukinGaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txtRuikeiMinyukinGaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRuikeiMinyukinGaku.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txtRuikeiMinyukinGaku.CustomFormatSetting = "#,##0";
            this.txtRuikeiMinyukinGaku.DBFieldsName = "";
            this.txtRuikeiMinyukinGaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtRuikeiMinyukinGaku.DisplayItemName = "未入金額";
            this.txtRuikeiMinyukinGaku.DisplayPopUp = null;
            this.txtRuikeiMinyukinGaku.ErrorMessage = "";
            this.txtRuikeiMinyukinGaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtRuikeiMinyukinGaku.FocusOutCheckMethod")));
            this.txtRuikeiMinyukinGaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtRuikeiMinyukinGaku.ForeColor = System.Drawing.Color.Black;
            this.txtRuikeiMinyukinGaku.FormatSetting = "カスタム";
            this.txtRuikeiMinyukinGaku.GetCodeMasterField = "";
            this.txtRuikeiMinyukinGaku.IsInputErrorOccured = false;
            this.txtRuikeiMinyukinGaku.ItemDefinedTypes = "";
            this.txtRuikeiMinyukinGaku.Location = new System.Drawing.Point(120, 85);
            this.txtRuikeiMinyukinGaku.MaxLength = 20;
            this.txtRuikeiMinyukinGaku.Name = "txtRuikeiMinyukinGaku";
            this.txtRuikeiMinyukinGaku.PopupAfterExecute = null;
            this.txtRuikeiMinyukinGaku.PopupBeforeExecute = null;
            this.txtRuikeiMinyukinGaku.PopupGetMasterField = "";
            this.txtRuikeiMinyukinGaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtRuikeiMinyukinGaku.PopupSearchSendParams")));
            this.txtRuikeiMinyukinGaku.PopupSetFormField = "";
            this.txtRuikeiMinyukinGaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtRuikeiMinyukinGaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtRuikeiMinyukinGaku.popupWindowSetting")));
            this.txtRuikeiMinyukinGaku.ReadOnly = true;
            this.txtRuikeiMinyukinGaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtRuikeiMinyukinGaku.RegistCheckMethod")));
            this.txtRuikeiMinyukinGaku.SetFormField = "";
            this.txtRuikeiMinyukinGaku.Size = new System.Drawing.Size(150, 20);
            this.txtRuikeiMinyukinGaku.TabIndex = 17;
            this.txtRuikeiMinyukinGaku.TabStop = false;
            this.txtRuikeiMinyukinGaku.Tag = "　";
            this.txtRuikeiMinyukinGaku.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblRuikeiMinyukinGaku
            // 
            this.lblRuikeiMinyukinGaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblRuikeiMinyukinGaku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRuikeiMinyukinGaku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRuikeiMinyukinGaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblRuikeiMinyukinGaku.ForeColor = System.Drawing.Color.White;
            this.lblRuikeiMinyukinGaku.Location = new System.Drawing.Point(5, 85);
            this.lblRuikeiMinyukinGaku.Name = "lblRuikeiMinyukinGaku";
            this.lblRuikeiMinyukinGaku.Size = new System.Drawing.Size(110, 20);
            this.lblRuikeiMinyukinGaku.TabIndex = 16;
            this.lblRuikeiMinyukinGaku.Text = "未入金額";
            this.lblRuikeiMinyukinGaku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dgvNyukinDeleteMeisai
            // 
            this.dgvNyukinDeleteMeisai.AllowUserToAddRows = false;
            this.dgvNyukinDeleteMeisai.AllowUserToDeleteRows = false;
            this.dgvNyukinDeleteMeisai.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNyukinDeleteMeisai.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvNyukinDeleteMeisai.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNyukinDeleteMeisai.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DELETE_FLG,
            this.GYOUSHA_CD,
            this.GYOUSHA_NAME_RYAKU,
            this.GENBA_CD,
            this.GENBA_NAME_RYAKU,
            this.SEIKYUU_DATE,
            this.SEIKYUUGAKU,
            this.KeshikomiGaku,
            this.keshikomiGakuTotal,
            this.MiKeshikomiGaku,
            this.KESHIKOMI_BIKOU,
            this.SEIKYUU_NUMBER,
            this.KAGAMI_NUMBER,
            this.SYSTEM_ID,
            this.KESHIKOMI_SEQ,
            this.NYUUKIN_NUMBER,
            this.ROW_NUMBER});
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvNyukinDeleteMeisai.DefaultCellStyle = dataGridViewCellStyle19;
            this.dgvNyukinDeleteMeisai.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvNyukinDeleteMeisai.EnableHeadersVisualStyles = false;
            this.dgvNyukinDeleteMeisai.GridColor = System.Drawing.Color.White;
            this.dgvNyukinDeleteMeisai.IsReload = false;
            this.dgvNyukinDeleteMeisai.LinkedDataPanelName = null;
            this.dgvNyukinDeleteMeisai.Location = new System.Drawing.Point(5, 111);
            this.dgvNyukinDeleteMeisai.MultiSelect = false;
            this.dgvNyukinDeleteMeisai.Name = "dgvNyukinDeleteMeisai";
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle20.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvNyukinDeleteMeisai.RowHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.dgvNyukinDeleteMeisai.RowHeadersVisible = false;
            this.dgvNyukinDeleteMeisai.RowTemplate.Height = 21;
            this.dgvNyukinDeleteMeisai.ShowCellToolTips = false;
            this.dgvNyukinDeleteMeisai.Size = new System.Drawing.Size(917, 304);
            this.dgvNyukinDeleteMeisai.TabIndex = 505;
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
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.DefaultCellStyle = dataGridViewCellStyle3;
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
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle4;
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
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.DefaultCellStyle = dataGridViewCellStyle5;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.HeaderText = "現場CD";
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.ReadOnly = true;
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GENBA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GENBA_CD.Width = 62;
            // 
            // GENBA_NAME_RYAKU
            // 
            this.GENBA_NAME_RYAKU.DBFieldsName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle6;
            this.GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU.HeaderText = "現場名";
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.ReadOnly = true;
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.GENBA_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GENBA_NAME_RYAKU.Width = 150;
            // 
            // SEIKYUU_DATE
            // 
            this.SEIKYUU_DATE.DBFieldsName = "SEIKYUU_DATE";
            this.SEIKYUU_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.SEIKYUU_DATE.DefaultCellStyle = dataGridViewCellStyle7;
            this.SEIKYUU_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DATE.FocusOutCheckMethod")));
            this.SEIKYUU_DATE.HeaderText = "請求日付";
            this.SEIKYUU_DATE.Name = "SEIKYUU_DATE";
            this.SEIKYUU_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUU_DATE.PopupSearchSendParams")));
            this.SEIKYUU_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUU_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUU_DATE.popupWindowSetting")));
            this.SEIKYUU_DATE.ReadOnly = true;
            this.SEIKYUU_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_DATE.RegistCheckMethod")));
            this.SEIKYUU_DATE.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SEIKYUU_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SEIKYUU_DATE.Width = 110;
            // 
            // SEIKYUUGAKU
            // 
            this.SEIKYUUGAKU.CustomFormatSetting = "#,##0";
            this.SEIKYUUGAKU.DBFieldsName = "SEIKYUUGAKU";
            this.SEIKYUUGAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.SEIKYUUGAKU.DefaultCellStyle = dataGridViewCellStyle8;
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
            this.KeshikomiGaku.DBFieldsName = "KeshikomiGaku";
            this.KeshikomiGaku.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.KeshikomiGaku.DefaultCellStyle = dataGridViewCellStyle9;
            this.KeshikomiGaku.DisplayItemName = "消込額";
            this.KeshikomiGaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KeshikomiGaku.FocusOutCheckMethod")));
            this.KeshikomiGaku.FormatSetting = "カスタム";
            this.KeshikomiGaku.HeaderText = "消込額";
            this.KeshikomiGaku.Name = "KeshikomiGaku";
            this.KeshikomiGaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KeshikomiGaku.PopupSearchSendParams")));
            this.KeshikomiGaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KeshikomiGaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KeshikomiGaku.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.KeshikomiGaku.RangeSetting = rangeSettingDto2;
            this.KeshikomiGaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KeshikomiGaku.RegistCheckMethod")));
            this.KeshikomiGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KeshikomiGaku.ToolTipText = "半角9桁以内で入力してください";
            this.KeshikomiGaku.Width = 95;
            // 
            // keshikomiGakuTotal
            // 
            this.keshikomiGakuTotal.CustomFormatSetting = "#,##0";
            this.keshikomiGakuTotal.DBFieldsName = "keshikomiGakuTotal";
            this.keshikomiGakuTotal.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.keshikomiGakuTotal.DefaultCellStyle = dataGridViewCellStyle10;
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
            this.keshikomiGakuTotal.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.keshikomiGakuTotal.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.keshikomiGakuTotal.Width = 95;
            // 
            // MiKeshikomiGaku
            // 
            this.MiKeshikomiGaku.CustomFormatSetting = "#,##0";
            this.MiKeshikomiGaku.DBFieldsName = "MiKeshikomiGaku";
            this.MiKeshikomiGaku.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.MiKeshikomiGaku.DefaultCellStyle = dataGridViewCellStyle11;
            this.MiKeshikomiGaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MiKeshikomiGaku.FocusOutCheckMethod")));
            this.MiKeshikomiGaku.FormatSetting = "カスタム";
            this.MiKeshikomiGaku.HeaderText = "未消込額";
            this.MiKeshikomiGaku.Name = "MiKeshikomiGaku";
            this.MiKeshikomiGaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("MiKeshikomiGaku.PopupSearchSendParams")));
            this.MiKeshikomiGaku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.MiKeshikomiGaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("MiKeshikomiGaku.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            rangeSettingDto4.Min = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.MiKeshikomiGaku.RangeSetting = rangeSettingDto4;
            this.MiKeshikomiGaku.ReadOnly = true;
            this.MiKeshikomiGaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("MiKeshikomiGaku.RegistCheckMethod")));
            this.MiKeshikomiGaku.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.MiKeshikomiGaku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MiKeshikomiGaku.Width = 95;
            // 
            // KESHIKOMI_BIKOU
            // 
            this.KESHIKOMI_BIKOU.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.KESHIKOMI_BIKOU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.KESHIKOMI_BIKOU.DefaultCellStyle = dataGridViewCellStyle12;
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
            // SEIKYUU_NUMBER
            // 
            this.SEIKYUU_NUMBER.DBFieldsName = "SEIKYUU_NUMBER";
            this.SEIKYUU_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.SEIKYUU_NUMBER.DefaultCellStyle = dataGridViewCellStyle13;
            this.SEIKYUU_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_NUMBER.FocusOutCheckMethod")));
            this.SEIKYUU_NUMBER.HeaderText = "請求番号";
            this.SEIKYUU_NUMBER.Name = "SEIKYUU_NUMBER";
            this.SEIKYUU_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SEIKYUU_NUMBER.PopupSearchSendParams")));
            this.SEIKYUU_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SEIKYUU_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SEIKYUU_NUMBER.popupWindowSetting")));
            this.SEIKYUU_NUMBER.ReadOnly = true;
            this.SEIKYUU_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SEIKYUU_NUMBER.RegistCheckMethod")));
            this.SEIKYUU_NUMBER.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SEIKYUU_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SEIKYUU_NUMBER.Visible = false;
            this.SEIKYUU_NUMBER.Width = 70;
            // 
            // KAGAMI_NUMBER
            // 
            this.KAGAMI_NUMBER.DBFieldsName = "KAGAMI_NUMBER";
            this.KAGAMI_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.KAGAMI_NUMBER.DefaultCellStyle = dataGridViewCellStyle14;
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
            // SYSTEM_ID
            // 
            this.SYSTEM_ID.DBFieldsName = "SYSTEM_ID";
            this.SYSTEM_ID.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.SYSTEM_ID.DefaultCellStyle = dataGridViewCellStyle15;
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
            // KESHIKOMI_SEQ
            // 
            this.KESHIKOMI_SEQ.DBFieldsName = "KESHIKOMI_SEQ";
            this.KESHIKOMI_SEQ.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.KESHIKOMI_SEQ.DefaultCellStyle = dataGridViewCellStyle16;
            this.KESHIKOMI_SEQ.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KESHIKOMI_SEQ.FocusOutCheckMethod")));
            this.KESHIKOMI_SEQ.HeaderText = "KESHIKOMI_SEQ";
            this.KESHIKOMI_SEQ.Name = "KESHIKOMI_SEQ";
            this.KESHIKOMI_SEQ.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KESHIKOMI_SEQ.PopupSearchSendParams")));
            this.KESHIKOMI_SEQ.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KESHIKOMI_SEQ.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KESHIKOMI_SEQ.popupWindowSetting")));
            this.KESHIKOMI_SEQ.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KESHIKOMI_SEQ.RegistCheckMethod")));
            this.KESHIKOMI_SEQ.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.KESHIKOMI_SEQ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.KESHIKOMI_SEQ.Visible = false;
            // 
            // NYUUKIN_NUMBER
            // 
            this.NYUUKIN_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.Black;
            this.NYUUKIN_NUMBER.DefaultCellStyle = dataGridViewCellStyle17;
            this.NYUUKIN_NUMBER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKIN_NUMBER.FocusOutCheckMethod")));
            this.NYUUKIN_NUMBER.HeaderText = "NYUUKIN_NUMBER";
            this.NYUUKIN_NUMBER.Name = "NYUUKIN_NUMBER";
            this.NYUUKIN_NUMBER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("NYUUKIN_NUMBER.PopupSearchSendParams")));
            this.NYUUKIN_NUMBER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.NYUUKIN_NUMBER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("NYUUKIN_NUMBER.popupWindowSetting")));
            this.NYUUKIN_NUMBER.ReadOnly = true;
            this.NYUUKIN_NUMBER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("NYUUKIN_NUMBER.RegistCheckMethod")));
            this.NYUUKIN_NUMBER.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NYUUKIN_NUMBER.Visible = false;
            this.NYUUKIN_NUMBER.Width = 70;
            // 
            // ROW_NUMBER
            // 
            this.ROW_NUMBER.DBFieldsName = "SORT_SEIKYUU_DATE";
            this.ROW_NUMBER.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.Black;
            this.ROW_NUMBER.DefaultCellStyle = dataGridViewCellStyle18;
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
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(929, 410);
            this.Controls.Add(this.dgvNyukinDeleteMeisai);
            this.Controls.Add(this.txtRuikeiMinyukinGaku);
            this.Controls.Add(this.lblRuikeiMinyukinGaku);
            this.Controls.Add(this.txtKonkaiKeshikomiGaku);
            this.Controls.Add(this.lblKonkaiKeshikomiGaku);
            this.Controls.Add(this.txtKonkaiNyukinGaku);
            this.Controls.Add(this.lblKonkaiNyukinGaku);
            this.Controls.Add(this.txtNyukinSakiName);
            this.Controls.Add(this.lblNyukinSaki);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvNyukinDeleteMeisai)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox txtNyukinSakiName;
        internal System.Windows.Forms.Label lblNyukinSaki;
        internal r_framework.CustomControl.CustomTextBox txtKonkaiNyukinGaku;
        internal System.Windows.Forms.Label lblKonkaiNyukinGaku;
        internal r_framework.CustomControl.CustomTextBox txtKonkaiKeshikomiGaku;
        internal System.Windows.Forms.Label lblKonkaiKeshikomiGaku;
        internal r_framework.CustomControl.CustomTextBox txtRuikeiMinyukinGaku;
        internal System.Windows.Forms.Label lblRuikeiMinyukinGaku;
        internal r_framework.CustomControl.CustomDataGridView dgvNyukinDeleteMeisai;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomCheckBoxColumn DELETE_FLG;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn GYOUSHA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GYOUSHA_NAME_RYAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomAlphaNumTextBoxColumn GENBA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GENBA_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SEIKYUU_DATE;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column SEIKYUUGAKU;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column KeshikomiGaku;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column keshikomiGakuTotal;
        private r_framework.CustomControl.DataGridCustomControl.DgvCustomNumericTextBox2Column MiKeshikomiGaku;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KESHIKOMI_BIKOU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SEIKYUU_NUMBER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KAGAMI_NUMBER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SYSTEM_ID;
        private r_framework.CustomControl.DgvCustomTextBoxColumn KESHIKOMI_SEQ;
        private r_framework.CustomControl.DgvCustomTextBoxColumn NYUUKIN_NUMBER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn ROW_NUMBER;
    }
}