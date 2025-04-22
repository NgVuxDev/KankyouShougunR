// $Id: UIForm.Designer.cs 32824 2014-10-20 10:31:57Z takeda $
namespace Shougun.Core.Allocation.ContenaIchiran
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.lb_gyousha = new System.Windows.Forms.Label();
            this.GYOUSHA_NAME_RYAKU_HEADER = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_CD_HEADER = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lb_genba = new System.Windows.Forms.Label();
            this.GENBA_NAME_RYAKU_HEADER = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_CD_HEADER = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.Ichiran = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.SecchiChouhuku = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CONTENA_SHURUI_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CONTENA_SHURUI_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SECCHI_DATE = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CONTENA_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.CONTENA_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GYOUSHA_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GENBA_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DAISUU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.EIGYOU_TANTOU_CD = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.SHAIN_NAME_RYAKU = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.DAYSCOUNT = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.GRAPH = new Shougun.Core.Allocation.ContenaIchiran.DataGridViewProgressBarColumn();
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
            this.dataGridViewProgressBarColumn1 = new Shougun.Core.Allocation.ContenaIchiran.DataGridViewProgressBarColumn();
            this.EIGYOU_TANTOU_CD_HEADER = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHAIN_NAME_RYAKU_HEADER = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.CONTENA_SHURUI_CD_HEADER = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CONTENA_CD_HEADER = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.CONTENA_NAME_RYAKU_HEADER = new r_framework.CustomControl.CustomTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.customSortHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSortHeader();
            this.IchiranForCSVOutput = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.dgvCustomTextBoxColumn1 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn2 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn3 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn4 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn5 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn6 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn7 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn8 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn9 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn10 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn11 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.dgvCustomTextBoxColumn12 = new r_framework.CustomControl.DgvCustomTextBoxColumn();
            this.ELAPSED_DAYS = new r_framework.CustomControl.CustomNumericTextBox2();
            this.EIGYOU_TANTOU_KBN = new r_framework.CustomControl.CustomTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.customPanel2 = new r_framework.CustomControl.CustomPanel();
            this.ChouhukuSecchiNomi2 = new r_framework.CustomControl.CustomRadioButton();
            this.ChouhukuSecchiNomi1 = new r_framework.CustomControl.CustomRadioButton();
            this.ChouhukuSecchiNomi = new r_framework.CustomControl.CustomNumericTextBox2();
            this.customPanel1 = new r_framework.CustomControl.CustomPanel();
            this.customSearchHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSearchHeader();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomAlphaNumTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IchiranForCSVOutput)).BeginInit();
            this.customPanel2.SuspendLayout();
            this.customPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_gyousha
            // 
            this.lb_gyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_gyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_gyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_gyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lb_gyousha.ForeColor = System.Drawing.Color.White;
            this.lb_gyousha.Location = new System.Drawing.Point(0, 10);
            this.lb_gyousha.Name = "lb_gyousha";
            this.lb_gyousha.Size = new System.Drawing.Size(110, 20);
            this.lb_gyousha.TabIndex = 703;
            this.lb_gyousha.Text = "業者";
            this.lb_gyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_NAME_RYAKU_HEADER
            // 
            this.GYOUSHA_NAME_RYAKU_HEADER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME_RYAKU_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_RYAKU_HEADER.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_RYAKU_HEADER.DisplayPopUp = null;
            this.GYOUSHA_NAME_RYAKU_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_HEADER.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU_HEADER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_NAME_RYAKU_HEADER.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU_HEADER.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU_HEADER.Location = new System.Drawing.Point(161, 10);
            this.GYOUSHA_NAME_RYAKU_HEADER.Name = "GYOUSHA_NAME_RYAKU_HEADER";
            this.GYOUSHA_NAME_RYAKU_HEADER.PopupAfterExecute = null;
            this.GYOUSHA_NAME_RYAKU_HEADER.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_RYAKU_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_HEADER.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_HEADER.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU_HEADER.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU_HEADER.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU_HEADER.Size = new System.Drawing.Size(286, 20);
            this.GYOUSHA_NAME_RYAKU_HEADER.TabIndex = 702;
            this.GYOUSHA_NAME_RYAKU_HEADER.TabStop = false;
            this.GYOUSHA_NAME_RYAKU_HEADER.Tag = " ";
            // 
            // GYOUSHA_CD_HEADER
            // 
            this.GYOUSHA_CD_HEADER.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD_HEADER.ChangeUpperCase = true;
            this.GYOUSHA_CD_HEADER.CharacterLimitList = null;
            this.GYOUSHA_CD_HEADER.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD_HEADER.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD_HEADER.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD_HEADER.DisplayItemName = "業者";
            this.GYOUSHA_CD_HEADER.DisplayPopUp = null;
            this.GYOUSHA_CD_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD_HEADER.FocusOutCheckMethod")));
            this.GYOUSHA_CD_HEADER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_CD_HEADER.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD_HEADER.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD_HEADER.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD_HEADER.IsInputErrorOccured = false;
            this.GYOUSHA_CD_HEADER.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD_HEADER.Location = new System.Drawing.Point(115, 10);
            this.GYOUSHA_CD_HEADER.MaxLength = 6;
            this.GYOUSHA_CD_HEADER.Name = "GYOUSHA_CD_HEADER";
            this.GYOUSHA_CD_HEADER.PopupAfterExecute = null;
            this.GYOUSHA_CD_HEADER.PopupAfterExecuteMethod = "PopupAfterExecuteGyousyaCD";
            this.GYOUSHA_CD_HEADER.PopupBeforeExecute = null;
            this.GYOUSHA_CD_HEADER.PopupBeforeExecuteMethod = "PopupBeforeExecuteGyousyaCD";
            this.GYOUSHA_CD_HEADER.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD_HEADER.PopupSearchSendParams")));
            this.GYOUSHA_CD_HEADER.PopupSendParams = new string[0];
            this.GYOUSHA_CD_HEADER.PopupSetFormField = "GYOUSHA_CD_HEADER,GYOUSHA_NAME_RYAKU_HEADER";
            this.GYOUSHA_CD_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD_HEADER.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD_HEADER.popupWindowSetting")));
            this.GYOUSHA_CD_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD_HEADER.RegistCheckMethod")));
            this.GYOUSHA_CD_HEADER.SetFormField = "GYOUSHA_CD_HEADER,GYOUSHA_NAME_RYAKU_HEADER";
            this.GYOUSHA_CD_HEADER.ShortItemName = "業者CD";
            this.GYOUSHA_CD_HEADER.Size = new System.Drawing.Size(47, 20);
            this.GYOUSHA_CD_HEADER.TabIndex = 1;
            this.GYOUSHA_CD_HEADER.Tag = "業者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD_HEADER.ZeroPaddengFlag = true;
            this.GYOUSHA_CD_HEADER.Enter += new System.EventHandler(this.GYOUSHA_CD_HEADER_Enter);
            this.GYOUSHA_CD_HEADER.Validating += new System.ComponentModel.CancelEventHandler(this.GYOUSHA_CD_HEADER_Validating);
            // 
            // lb_genba
            // 
            this.lb_genba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_genba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_genba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_genba.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lb_genba.ForeColor = System.Drawing.Color.White;
            this.lb_genba.Location = new System.Drawing.Point(498, 11);
            this.lb_genba.Name = "lb_genba";
            this.lb_genba.Size = new System.Drawing.Size(110, 20);
            this.lb_genba.TabIndex = 803;
            this.lb_genba.Text = "現場";
            this.lb_genba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_NAME_RYAKU_HEADER
            // 
            this.GENBA_NAME_RYAKU_HEADER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME_RYAKU_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_RYAKU_HEADER.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_RYAKU_HEADER.DisplayPopUp = null;
            this.GENBA_NAME_RYAKU_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU_HEADER.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU_HEADER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_NAME_RYAKU_HEADER.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU_HEADER.IsInputErrorOccured = false;
            this.GENBA_NAME_RYAKU_HEADER.Location = new System.Drawing.Point(692, 11);
            this.GENBA_NAME_RYAKU_HEADER.Name = "GENBA_NAME_RYAKU_HEADER";
            this.GENBA_NAME_RYAKU_HEADER.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU_HEADER.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU_HEADER.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU_HEADER.popupWindowSetting")));
            this.GENBA_NAME_RYAKU_HEADER.ReadOnly = true;
            this.GENBA_NAME_RYAKU_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU_HEADER.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU_HEADER.Size = new System.Drawing.Size(286, 20);
            this.GENBA_NAME_RYAKU_HEADER.TabIndex = 802;
            this.GENBA_NAME_RYAKU_HEADER.TabStop = false;
            this.GENBA_NAME_RYAKU_HEADER.Tag = " ";
            // 
            // GENBA_CD_HEADER
            // 
            this.GENBA_CD_HEADER.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD_HEADER.ChangeUpperCase = true;
            this.GENBA_CD_HEADER.CharacterLimitList = null;
            this.GENBA_CD_HEADER.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD_HEADER.DBFieldsName = "GENBA_CD";
            this.GENBA_CD_HEADER.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD_HEADER.DisplayItemName = "現場";
            this.GENBA_CD_HEADER.DisplayPopUp = null;
            this.GENBA_CD_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD_HEADER.FocusOutCheckMethod")));
            this.GENBA_CD_HEADER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_CD_HEADER.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD_HEADER.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GENBA_CD_HEADER.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD_HEADER.IsInputErrorOccured = false;
            this.GENBA_CD_HEADER.ItemDefinedTypes = "varchar";
            this.GENBA_CD_HEADER.Location = new System.Drawing.Point(613, 11);
            this.GENBA_CD_HEADER.MaxLength = 6;
            this.GENBA_CD_HEADER.Name = "GENBA_CD_HEADER";
            this.GENBA_CD_HEADER.PopupAfterExecute = null;
            this.GENBA_CD_HEADER.PopupBeforeExecute = null;
            this.GENBA_CD_HEADER.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GENBA_CD_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD_HEADER.PopupSearchSendParams")));
            this.GENBA_CD_HEADER.PopupSendParams = new string[0];
            this.GENBA_CD_HEADER.PopupSetFormField = "GENBA_CD_HEADER,GENBA_NAME_RYAKU_HEADER,GYOUSHA_CD_HEADER,GYOUSHA_NAME_RYAKU_HEAD" +
    "ER";
            this.GENBA_CD_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD_HEADER.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD_HEADER.popupWindowSetting")));
            this.GENBA_CD_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD_HEADER.RegistCheckMethod")));
            this.GENBA_CD_HEADER.SetFormField = "GENBA_CD_HEADER,GENBA_NAME_RYAKU_HEADER,GYOUSHA_CD_HEADER,GYOUSHA_NAME_RYAKU_HEAD" +
    "ER";
            this.GENBA_CD_HEADER.ShortItemName = "現場CD";
            this.GENBA_CD_HEADER.Size = new System.Drawing.Size(80, 20);
            this.GENBA_CD_HEADER.TabIndex = 2;
            this.GENBA_CD_HEADER.Tag = "現場を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD_HEADER.ZeroPaddengFlag = true;
            this.GENBA_CD_HEADER.Enter += new System.EventHandler(this.GENBA_CD_HEADER_Enter);
            this.GENBA_CD_HEADER.Validating += new System.ComponentModel.CancelEventHandler(this.GENBA_CD_HEADER_Validating);
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
            this.SecchiChouhuku,
            this.CONTENA_SHURUI_CD,
            this.CONTENA_SHURUI_NAME_RYAKU,
            this.SECCHI_DATE,
            this.CONTENA_CD,
            this.CONTENA_NAME_RYAKU,
            this.GYOUSHA_CD,
            this.GYOUSHA_NAME_RYAKU,
            this.GENBA_CD,
            this.GENBA_NAME_RYAKU,
            this.DAISUU,
            this.EIGYOU_TANTOU_CD,
            this.SHAIN_NAME_RYAKU,
            this.DAYSCOUNT,
            this.GRAPH});
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Ichiran.DefaultCellStyle = dataGridViewCellStyle17;
            this.Ichiran.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.Ichiran.EnableHeadersVisualStyles = false;
            this.Ichiran.GridColor = System.Drawing.Color.White;
            this.Ichiran.IsReload = false;
            this.Ichiran.LinkedDataPanelName = "customSortHeader1";
            this.Ichiran.Location = new System.Drawing.Point(0, 133);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle18.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle18.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Ichiran.RowHeadersDefaultCellStyle = dataGridViewCellStyle18;
            this.Ichiran.RowHeadersVisible = false;
            this.Ichiran.RowTemplate.Height = 21;
            this.Ichiran.ShowCellToolTips = false;
            this.Ichiran.Size = new System.Drawing.Size(980, 325);
            this.Ichiran.TabIndex = 8;
            this.Ichiran.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.Ichiran_CellFormatting);
            // 
            // SecchiChouhuku
            // 
            this.SecchiChouhuku.DataPropertyName = "SecchiChouhuku";
            this.SecchiChouhuku.DBFieldsName = "SecchiChouhuku";
            this.SecchiChouhuku.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            this.SecchiChouhuku.DefaultCellStyle = dataGridViewCellStyle2;
            this.SecchiChouhuku.DisplayItemName = "重複設置";
            this.SecchiChouhuku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SecchiChouhuku.FocusOutCheckMethod")));
            this.SecchiChouhuku.HeaderText = "重複設置";
            this.SecchiChouhuku.Name = "SecchiChouhuku";
            this.SecchiChouhuku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SecchiChouhuku.PopupSearchSendParams")));
            this.SecchiChouhuku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SecchiChouhuku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SecchiChouhuku.popupWindowSetting")));
            this.SecchiChouhuku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SecchiChouhuku.RegistCheckMethod")));
            this.SecchiChouhuku.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.SecchiChouhuku.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SecchiChouhuku.Visible = false;
            this.SecchiChouhuku.Width = 70;
            // 
            // CONTENA_SHURUI_CD
            // 
            this.CONTENA_SHURUI_CD.DataPropertyName = "CONTENA_SHURUI_CD";
            this.CONTENA_SHURUI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_SHURUI_CD.DefaultCellStyle = dataGridViewCellStyle3;
            this.CONTENA_SHURUI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_CD.HeaderText = "コンテナ種類CD";
            this.CONTENA_SHURUI_CD.Name = "CONTENA_SHURUI_CD";
            this.CONTENA_SHURUI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_CD.PopupSearchSendParams")));
            this.CONTENA_SHURUI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_CD.popupWindowSetting")));
            this.CONTENA_SHURUI_CD.ReadOnly = true;
            this.CONTENA_SHURUI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD.RegistCheckMethod")));
            this.CONTENA_SHURUI_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_CD.Visible = false;
            this.CONTENA_SHURUI_CD.Width = 130;
            // 
            // CONTENA_SHURUI_NAME_RYAKU
            // 
            this.CONTENA_SHURUI_NAME_RYAKU.DataPropertyName = "CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_SHURUI_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle4;
            this.CONTENA_SHURUI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU.HeaderText = "コンテナ種類名";
            this.CONTENA_SHURUI_NAME_RYAKU.Name = "CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.PopupSearchSendParams")));
            this.CONTENA_SHURUI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.popupWindowSetting")));
            this.CONTENA_SHURUI_NAME_RYAKU.ReadOnly = true;
            this.CONTENA_SHURUI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU.RegistCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_SHURUI_NAME_RYAKU.Width = 120;
            // 
            // SECCHI_DATE
            // 
            this.SECCHI_DATE.DataPropertyName = "SECCHI_DATE";
            this.SECCHI_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.Black;
            this.SECCHI_DATE.DefaultCellStyle = dataGridViewCellStyle5;
            this.SECCHI_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SECCHI_DATE.FocusOutCheckMethod")));
            this.SECCHI_DATE.HeaderText = "最終更新日";
            this.SECCHI_DATE.Name = "SECCHI_DATE";
            this.SECCHI_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SECCHI_DATE.PopupSearchSendParams")));
            this.SECCHI_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SECCHI_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SECCHI_DATE.popupWindowSetting")));
            this.SECCHI_DATE.ReadOnly = true;
            this.SECCHI_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SECCHI_DATE.RegistCheckMethod")));
            this.SECCHI_DATE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.SECCHI_DATE.Width = 85;
            // 
            // CONTENA_CD
            // 
            this.CONTENA_CD.DataPropertyName = "CONTENA_CD";
            this.CONTENA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_CD.DefaultCellStyle = dataGridViewCellStyle6;
            this.CONTENA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_CD.FocusOutCheckMethod")));
            this.CONTENA_CD.HeaderText = "コンテナCD";
            this.CONTENA_CD.Name = "CONTENA_CD";
            this.CONTENA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_CD.PopupSearchSendParams")));
            this.CONTENA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_CD.popupWindowSetting")));
            this.CONTENA_CD.ReadOnly = true;
            this.CONTENA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_CD.RegistCheckMethod")));
            this.CONTENA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CONTENA_CD.Visible = false;
            this.CONTENA_CD.Width = 130;
            // 
            // CONTENA_NAME_RYAKU
            // 
            this.CONTENA_NAME_RYAKU.DataPropertyName = "CONTENA_NAME_RYAKU";
            this.CONTENA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.Black;
            this.CONTENA_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle7;
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
            this.CONTENA_NAME_RYAKU.Width = 140;
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.DataPropertyName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.DefaultCellStyle = dataGridViewCellStyle8;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.HeaderText = "業者CD";
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.ReadOnly = true;
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GYOUSHA_CD.Visible = false;
            this.GYOUSHA_CD.Width = 80;
            // 
            // GYOUSHA_NAME_RYAKU
            // 
            this.GYOUSHA_NAME_RYAKU.DataPropertyName = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle9;
            this.GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.HeaderText = "業者名";
            this.GYOUSHA_NAME_RYAKU.Name = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GYOUSHA_NAME_RYAKU.Width = 160;
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.DataPropertyName = "GENBA_CD";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.DefaultCellStyle = dataGridViewCellStyle10;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.HeaderText = "現場CD";
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.ReadOnly = true;
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GENBA_CD.Visible = false;
            // 
            // GENBA_NAME_RYAKU
            // 
            this.GENBA_NAME_RYAKU.DataPropertyName = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle11;
            this.GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU.HeaderText = "現場名";
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.ReadOnly = true;
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GENBA_NAME_RYAKU.Width = 160;
            // 
            // DAISUU
            // 
            this.DAISUU.DataPropertyName = "DAISUU";
            this.DAISUU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.Black;
            this.DAISUU.DefaultCellStyle = dataGridViewCellStyle12;
            this.DAISUU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAISUU.FocusOutCheckMethod")));
            this.DAISUU.HeaderText = "台数";
            this.DAISUU.Name = "DAISUU";
            this.DAISUU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAISUU.PopupSearchSendParams")));
            this.DAISUU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAISUU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAISUU.popupWindowSetting")));
            this.DAISUU.ReadOnly = true;
            this.DAISUU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAISUU.RegistCheckMethod")));
            this.DAISUU.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DAISUU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DAISUU.Width = 50;
            // 
            // EIGYOU_TANTOU_CD
            // 
            this.EIGYOU_TANTOU_CD.DataPropertyName = "EIGYOU_TANTOU_CD";
            this.EIGYOU_TANTOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.Black;
            this.EIGYOU_TANTOU_CD.DefaultCellStyle = dataGridViewCellStyle13;
            this.EIGYOU_TANTOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOU_CD.FocusOutCheckMethod")));
            this.EIGYOU_TANTOU_CD.HeaderText = "営業担当者CD";
            this.EIGYOU_TANTOU_CD.Name = "EIGYOU_TANTOU_CD";
            this.EIGYOU_TANTOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EIGYOU_TANTOU_CD.PopupSearchSendParams")));
            this.EIGYOU_TANTOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.EIGYOU_TANTOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EIGYOU_TANTOU_CD.popupWindowSetting")));
            this.EIGYOU_TANTOU_CD.ReadOnly = true;
            this.EIGYOU_TANTOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOU_CD.RegistCheckMethod")));
            this.EIGYOU_TANTOU_CD.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.EIGYOU_TANTOU_CD.Visible = false;
            this.EIGYOU_TANTOU_CD.Width = 120;
            // 
            // SHAIN_NAME_RYAKU
            // 
            this.SHAIN_NAME_RYAKU.DataPropertyName = "SHAIN_NAME_RYAKU";
            this.SHAIN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.Black;
            this.SHAIN_NAME_RYAKU.DefaultCellStyle = dataGridViewCellStyle14;
            this.SHAIN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME_RYAKU.FocusOutCheckMethod")));
            this.SHAIN_NAME_RYAKU.HeaderText = "営業担当者名";
            this.SHAIN_NAME_RYAKU.Name = "SHAIN_NAME_RYAKU";
            this.SHAIN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_NAME_RYAKU.PopupSearchSendParams")));
            this.SHAIN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHAIN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_NAME_RYAKU.popupWindowSetting")));
            this.SHAIN_NAME_RYAKU.ReadOnly = true;
            this.SHAIN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME_RYAKU.RegistCheckMethod")));
            this.SHAIN_NAME_RYAKU.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DAYSCOUNT
            // 
            this.DAYSCOUNT.DataPropertyName = "DAYSCOUNT";
            this.DAYSCOUNT.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle15.Format = "d";
            dataGridViewCellStyle15.NullValue = null;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.Black;
            this.DAYSCOUNT.DefaultCellStyle = dataGridViewCellStyle15;
            this.DAYSCOUNT.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAYSCOUNT.FocusOutCheckMethod")));
            this.DAYSCOUNT.HeaderText = "無回転日数";
            this.DAYSCOUNT.Name = "DAYSCOUNT";
            this.DAYSCOUNT.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("DAYSCOUNT.PopupSearchSendParams")));
            this.DAYSCOUNT.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.DAYSCOUNT.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("DAYSCOUNT.popupWindowSetting")));
            this.DAYSCOUNT.ReadOnly = true;
            this.DAYSCOUNT.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("DAYSCOUNT.RegistCheckMethod")));
            this.DAYSCOUNT.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DAYSCOUNT.Width = 90;
            // 
            // GRAPH
            // 
            this.GRAPH.DataPropertyName = "GRAPH";
            dataGridViewCellStyle16.Format = "N0";
            dataGridViewCellStyle16.NullValue = "0";
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.Black;
            this.GRAPH.DefaultCellStyle = dataGridViewCellStyle16;
            this.GRAPH.HeaderText = "グラフ";
            this.GRAPH.Maximum = 100;
            this.GRAPH.Mimimum = 0;
            this.GRAPH.Name = "GRAPH";
            this.GRAPH.ReadOnly = true;
            this.GRAPH.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.GRAPH.Width = 180;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "コンテナ種類CD";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.Width = 130;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "コンテナ種類名";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
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
            // EIGYOU_TANTOU_CD_HEADER
            // 
            this.EIGYOU_TANTOU_CD_HEADER.BackColor = System.Drawing.SystemColors.Window;
            this.EIGYOU_TANTOU_CD_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.EIGYOU_TANTOU_CD_HEADER.ChangeUpperCase = true;
            this.EIGYOU_TANTOU_CD_HEADER.CharacterLimitList = null;
            this.EIGYOU_TANTOU_CD_HEADER.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.EIGYOU_TANTOU_CD_HEADER.DBFieldsName = "";
            this.EIGYOU_TANTOU_CD_HEADER.DefaultBackColor = System.Drawing.Color.Empty;
            this.EIGYOU_TANTOU_CD_HEADER.DisplayItemName = "";
            this.EIGYOU_TANTOU_CD_HEADER.DisplayPopUp = null;
            this.EIGYOU_TANTOU_CD_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOU_CD_HEADER.FocusOutCheckMethod")));
            this.EIGYOU_TANTOU_CD_HEADER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.EIGYOU_TANTOU_CD_HEADER.ForeColor = System.Drawing.Color.Black;
            this.EIGYOU_TANTOU_CD_HEADER.GetCodeMasterField = "SHAIN_CD,SHAIN_NAME";
            this.EIGYOU_TANTOU_CD_HEADER.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.EIGYOU_TANTOU_CD_HEADER.IsInputErrorOccured = false;
            this.EIGYOU_TANTOU_CD_HEADER.ItemDefinedTypes = "varchar";
            this.EIGYOU_TANTOU_CD_HEADER.Location = new System.Drawing.Point(782, 33);
            this.EIGYOU_TANTOU_CD_HEADER.MaxLength = 6;
            this.EIGYOU_TANTOU_CD_HEADER.Name = "EIGYOU_TANTOU_CD_HEADER";
            this.EIGYOU_TANTOU_CD_HEADER.PopupAfterExecute = null;
            this.EIGYOU_TANTOU_CD_HEADER.PopupBeforeExecute = null;
            this.EIGYOU_TANTOU_CD_HEADER.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME";
            this.EIGYOU_TANTOU_CD_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EIGYOU_TANTOU_CD_HEADER.PopupSearchSendParams")));
            this.EIGYOU_TANTOU_CD_HEADER.PopupSetFormField = "EIGYOU_TANTOU_CD_HEADER,SHAIN_NAME_RYAKU_HEADER";
            this.EIGYOU_TANTOU_CD_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.EIGYOU_TANTOU_CD_HEADER.PopupWindowName = "マスタ共通ポップアップ";
            this.EIGYOU_TANTOU_CD_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EIGYOU_TANTOU_CD_HEADER.popupWindowSetting")));
            this.EIGYOU_TANTOU_CD_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOU_CD_HEADER.RegistCheckMethod")));
            this.EIGYOU_TANTOU_CD_HEADER.SetFormField = "EIGYOU_TANTOU_CD_HEADER,SHAIN_NAME_RYAKU_HEADER";
            this.EIGYOU_TANTOU_CD_HEADER.ShortItemName = "営業担当者CD";
            this.EIGYOU_TANTOU_CD_HEADER.Size = new System.Drawing.Size(47, 20);
            this.EIGYOU_TANTOU_CD_HEADER.TabIndex = 3;
            this.EIGYOU_TANTOU_CD_HEADER.Tag = "営業担当者を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.EIGYOU_TANTOU_CD_HEADER.ZeroPaddengFlag = true;
            // 
            // SHAIN_NAME_RYAKU_HEADER
            // 
            this.SHAIN_NAME_RYAKU_HEADER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHAIN_NAME_RYAKU_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHAIN_NAME_RYAKU_HEADER.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_NAME_RYAKU_HEADER.DisplayPopUp = null;
            this.SHAIN_NAME_RYAKU_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME_RYAKU_HEADER.FocusOutCheckMethod")));
            this.SHAIN_NAME_RYAKU_HEADER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHAIN_NAME_RYAKU_HEADER.ForeColor = System.Drawing.Color.Black;
            this.SHAIN_NAME_RYAKU_HEADER.IsInputErrorOccured = false;
            this.SHAIN_NAME_RYAKU_HEADER.Location = new System.Drawing.Point(828, 33);
            this.SHAIN_NAME_RYAKU_HEADER.Name = "SHAIN_NAME_RYAKU_HEADER";
            this.SHAIN_NAME_RYAKU_HEADER.PopupAfterExecute = null;
            this.SHAIN_NAME_RYAKU_HEADER.PopupBeforeExecute = null;
            this.SHAIN_NAME_RYAKU_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_NAME_RYAKU_HEADER.PopupSearchSendParams")));
            this.SHAIN_NAME_RYAKU_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHAIN_NAME_RYAKU_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_NAME_RYAKU_HEADER.popupWindowSetting")));
            this.SHAIN_NAME_RYAKU_HEADER.ReadOnly = true;
            this.SHAIN_NAME_RYAKU_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME_RYAKU_HEADER.RegistCheckMethod")));
            this.SHAIN_NAME_RYAKU_HEADER.Size = new System.Drawing.Size(150, 20);
            this.SHAIN_NAME_RYAKU_HEADER.TabIndex = 802;
            this.SHAIN_NAME_RYAKU_HEADER.TabStop = false;
            this.SHAIN_NAME_RYAKU_HEADER.Tag = " ";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(667, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 803;
            this.label1.Text = "営業担当者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CONTENA_SHURUI_CD_HEADER
            // 
            this.CONTENA_SHURUI_CD_HEADER.BackColor = System.Drawing.SystemColors.Window;
            this.CONTENA_SHURUI_CD_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONTENA_SHURUI_CD_HEADER.ChangeUpperCase = true;
            this.CONTENA_SHURUI_CD_HEADER.CharacterLimitList = null;
            this.CONTENA_SHURUI_CD_HEADER.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.CONTENA_SHURUI_CD_HEADER.DBFieldsName = "CONTENA_SHURUI_CD";
            this.CONTENA_SHURUI_CD_HEADER.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONTENA_SHURUI_CD_HEADER.DisplayItemName = "";
            this.CONTENA_SHURUI_CD_HEADER.DisplayPopUp = null;
            this.CONTENA_SHURUI_CD_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD_HEADER.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_CD_HEADER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONTENA_SHURUI_CD_HEADER.ForeColor = System.Drawing.Color.Black;
            this.CONTENA_SHURUI_CD_HEADER.GetCodeMasterField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU,CONTENA_CD,CONTENA_NAME_RYAKU,GENBA_C" +
    "D,GENBA_NAME_RYAKU,GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.CONTENA_SHURUI_CD_HEADER.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CONTENA_SHURUI_CD_HEADER.IsInputErrorOccured = false;
            this.CONTENA_SHURUI_CD_HEADER.ItemDefinedTypes = "varchar";
            this.CONTENA_SHURUI_CD_HEADER.Location = new System.Drawing.Point(115, 32);
            this.CONTENA_SHURUI_CD_HEADER.MaxLength = 3;
            this.CONTENA_SHURUI_CD_HEADER.Name = "CONTENA_SHURUI_CD_HEADER";
            this.CONTENA_SHURUI_CD_HEADER.PopupAfterExecute = null;
            this.CONTENA_SHURUI_CD_HEADER.PopupBeforeExecute = null;
            this.CONTENA_SHURUI_CD_HEADER.PopupGetMasterField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU";
            this.CONTENA_SHURUI_CD_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_CD_HEADER.PopupSearchSendParams")));
            this.CONTENA_SHURUI_CD_HEADER.PopupSendParams = new string[0];
            this.CONTENA_SHURUI_CD_HEADER.PopupSetFormField = "CONTENA_SHURUI_CD_HEADER,CONTENA_SHURUI_NAME_RYAKU_HEADER";
            this.CONTENA_SHURUI_CD_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA_SHURUI;
            this.CONTENA_SHURUI_CD_HEADER.PopupWindowName = "マスタ共通ポップアップ";
            this.CONTENA_SHURUI_CD_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_CD_HEADER.popupWindowSetting")));
            this.CONTENA_SHURUI_CD_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_CD_HEADER.RegistCheckMethod")));
            this.CONTENA_SHURUI_CD_HEADER.SetFormField = "CONTENA_SHURUI_CD_HEADER,CONTENA_SHURUI_NAME_RYAKU_HEADER,CONTENA_CD_HEADER,CONTE" +
    "NA_NAME_RYAKU_HEADER,GENBA_CD_HEADER,GENBA_NAME_RYAKU_HEADER,GYOUSHA_CD_HEADER,G" +
    "YOUSHA_NAME_RYAKU_HEADER";
            this.CONTENA_SHURUI_CD_HEADER.ShortItemName = "コンテナ種類CD";
            this.CONTENA_SHURUI_CD_HEADER.Size = new System.Drawing.Size(47, 20);
            this.CONTENA_SHURUI_CD_HEADER.TabIndex = 4;
            this.CONTENA_SHURUI_CD_HEADER.Tag = "コンテナ種類を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CONTENA_SHURUI_CD_HEADER.ZeroPaddengFlag = true;
            this.CONTENA_SHURUI_CD_HEADER.Enter += new System.EventHandler(this.CONTENA_SHURUI_CD_HEADER_Enter);
            this.CONTENA_SHURUI_CD_HEADER.Validating += new System.ComponentModel.CancelEventHandler(this.CONTENA_SHURUI_CD_HEADER_Validating);
            // 
            // CONTENA_SHURUI_NAME_RYAKU_HEADER
            // 
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.DisplayPopUp = null;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU_HEADER.FocusOutCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.ForeColor = System.Drawing.Color.Black;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.IsInputErrorOccured = false;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.Location = new System.Drawing.Point(161, 32);
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.Name = "CONTENA_SHURUI_NAME_RYAKU_HEADER";
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.PopupAfterExecute = null;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.PopupBeforeExecute = null;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU_HEADER.PopupSearchSendParams")));
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU_HEADER.popupWindowSetting")));
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.ReadOnly = true;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_SHURUI_NAME_RYAKU_HEADER.RegistCheckMethod")));
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.Size = new System.Drawing.Size(150, 20);
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.TabIndex = 702;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.TabStop = false;
            this.CONTENA_SHURUI_NAME_RYAKU_HEADER.Tag = " ";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 703;
            this.label2.Text = "コンテナ種類";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CONTENA_CD_HEADER
            // 
            this.CONTENA_CD_HEADER.BackColor = System.Drawing.SystemColors.Window;
            this.CONTENA_CD_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONTENA_CD_HEADER.ChangeUpperCase = true;
            this.CONTENA_CD_HEADER.CharacterLimitList = null;
            this.CONTENA_CD_HEADER.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.CONTENA_CD_HEADER.DBFieldsName = "CONTENA_CD";
            this.CONTENA_CD_HEADER.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONTENA_CD_HEADER.DisplayItemName = "コンテナ";
            this.CONTENA_CD_HEADER.DisplayPopUp = null;
            this.CONTENA_CD_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_CD_HEADER.FocusOutCheckMethod")));
            this.CONTENA_CD_HEADER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONTENA_CD_HEADER.ForeColor = System.Drawing.Color.Black;
            this.CONTENA_CD_HEADER.GetCodeMasterField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU,CONTENA_CD,CONTENA_NAME_RYAKU";
            this.CONTENA_CD_HEADER.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.CONTENA_CD_HEADER.IsInputErrorOccured = false;
            this.CONTENA_CD_HEADER.ItemDefinedTypes = "varchar";
            this.CONTENA_CD_HEADER.Location = new System.Drawing.Point(432, 33);
            this.CONTENA_CD_HEADER.MaxLength = 10;
            this.CONTENA_CD_HEADER.Name = "CONTENA_CD_HEADER";
            this.CONTENA_CD_HEADER.PopupAfterExecute = null;
            this.CONTENA_CD_HEADER.PopupBeforeExecute = null;
            this.CONTENA_CD_HEADER.PopupGetMasterField = "CONTENA_SHURUI_CD,CONTENA_SHURUI_NAME_RYAKU,CONTENA_CD,CONTENA_NAME_RYAKU";
            this.CONTENA_CD_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_CD_HEADER.PopupSearchSendParams")));
            this.CONTENA_CD_HEADER.PopupSendParams = new string[0];
            this.CONTENA_CD_HEADER.PopupSetFormField = "CONTENA_SHURUI_CD_HEADER,CONTENA_SHURUI_NAME_RYAKU_HEADER,CONTENA_CD_HEADER,CONTE" +
    "NA_NAME_RYAKU_HEADER";
            this.CONTENA_CD_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.M_CONTENA;
            this.CONTENA_CD_HEADER.PopupWindowName = "マスタ共通ポップアップ";
            this.CONTENA_CD_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_CD_HEADER.popupWindowSetting")));
            this.CONTENA_CD_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_CD_HEADER.RegistCheckMethod")));
            this.CONTENA_CD_HEADER.SetFormField = "CONTENA_SHURUI_CD_HEADER,CONTENA_SHURUI_NAME_RYAKU_HEADER,CONTENA_CD_HEADER,CONTE" +
    "NA_NAME_RYAKU_HEADER";
            this.CONTENA_CD_HEADER.ShortItemName = "コンテナ名";
            this.CONTENA_CD_HEADER.Size = new System.Drawing.Size(80, 20);
            this.CONTENA_CD_HEADER.TabIndex = 5;
            this.CONTENA_CD_HEADER.Tag = "コンテナを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.CONTENA_CD_HEADER.Visible = false;
            this.CONTENA_CD_HEADER.ZeroPaddengFlag = true;
            this.CONTENA_CD_HEADER.Enter += new System.EventHandler(this.CONTENA_CD_HEADER_Enter);
            this.CONTENA_CD_HEADER.Validating += new System.ComponentModel.CancelEventHandler(this.CONTENA_CD_HEADER_Validating);
            // 
            // CONTENA_NAME_RYAKU_HEADER
            // 
            this.CONTENA_NAME_RYAKU_HEADER.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CONTENA_NAME_RYAKU_HEADER.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CONTENA_NAME_RYAKU_HEADER.DefaultBackColor = System.Drawing.Color.Empty;
            this.CONTENA_NAME_RYAKU_HEADER.DisplayPopUp = null;
            this.CONTENA_NAME_RYAKU_HEADER.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_NAME_RYAKU_HEADER.FocusOutCheckMethod")));
            this.CONTENA_NAME_RYAKU_HEADER.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CONTENA_NAME_RYAKU_HEADER.ForeColor = System.Drawing.Color.Black;
            this.CONTENA_NAME_RYAKU_HEADER.IsInputErrorOccured = false;
            this.CONTENA_NAME_RYAKU_HEADER.Location = new System.Drawing.Point(511, 33);
            this.CONTENA_NAME_RYAKU_HEADER.Name = "CONTENA_NAME_RYAKU_HEADER";
            this.CONTENA_NAME_RYAKU_HEADER.PopupAfterExecute = null;
            this.CONTENA_NAME_RYAKU_HEADER.PopupBeforeExecute = null;
            this.CONTENA_NAME_RYAKU_HEADER.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CONTENA_NAME_RYAKU_HEADER.PopupSearchSendParams")));
            this.CONTENA_NAME_RYAKU_HEADER.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CONTENA_NAME_RYAKU_HEADER.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CONTENA_NAME_RYAKU_HEADER.popupWindowSetting")));
            this.CONTENA_NAME_RYAKU_HEADER.ReadOnly = true;
            this.CONTENA_NAME_RYAKU_HEADER.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CONTENA_NAME_RYAKU_HEADER.RegistCheckMethod")));
            this.CONTENA_NAME_RYAKU_HEADER.Size = new System.Drawing.Size(150, 20);
            this.CONTENA_NAME_RYAKU_HEADER.TabIndex = 802;
            this.CONTENA_NAME_RYAKU_HEADER.TabStop = false;
            this.CONTENA_NAME_RYAKU_HEADER.Tag = " ";
            this.CONTENA_NAME_RYAKU_HEADER.Visible = false;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(317, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 803;
            this.label3.Text = "コンテナ名";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(-1, 54);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 803;
            this.label4.Text = "無回転日数";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(163, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 805;
            this.label5.Text = "日以上";
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customSortHeader1.LinkedDataGridViewName = "Ichiran";
            this.customSortHeader1.Location = new System.Drawing.Point(0, 101);
            this.customSortHeader1.Name = "customSortHeader1";
            this.customSortHeader1.Size = new System.Drawing.Size(980, 26);
            this.customSortHeader1.SortFlag = false;
            this.customSortHeader1.TabIndex = 0;
            this.customSortHeader1.TabStop = false;
            // 
            // IchiranForCSVOutput
            // 
            this.IchiranForCSVOutput.AllowUserToAddRows = false;
            this.IchiranForCSVOutput.AllowUserToDeleteRows = false;
            this.IchiranForCSVOutput.AllowUserToResizeColumns = false;
            this.IchiranForCSVOutput.AllowUserToResizeRows = false;
            this.IchiranForCSVOutput.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle19.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle19.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IchiranForCSVOutput.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle19;
            this.IchiranForCSVOutput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.IchiranForCSVOutput.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvCustomTextBoxColumn1,
            this.dgvCustomTextBoxColumn2,
            this.dgvCustomTextBoxColumn3,
            this.dgvCustomTextBoxColumn4,
            this.dgvCustomTextBoxColumn5,
            this.dgvCustomTextBoxColumn6,
            this.dgvCustomTextBoxColumn7,
            this.dgvCustomTextBoxColumn8,
            this.dgvCustomTextBoxColumn9,
            this.dgvCustomTextBoxColumn10,
            this.dgvCustomTextBoxColumn11,
            this.dgvCustomTextBoxColumn12});
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.IchiranForCSVOutput.DefaultCellStyle = dataGridViewCellStyle32;
            this.IchiranForCSVOutput.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.IchiranForCSVOutput.EnableHeadersVisualStyles = false;
            this.IchiranForCSVOutput.GridColor = System.Drawing.Color.White;
            this.IchiranForCSVOutput.IsReload = false;
            this.IchiranForCSVOutput.LinkedDataPanelName = "customSortHeader1";
            this.IchiranForCSVOutput.Location = new System.Drawing.Point(2, 463);
            this.IchiranForCSVOutput.MultiSelect = false;
            this.IchiranForCSVOutput.Name = "IchiranForCSVOutput";
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle33.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle33.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.IchiranForCSVOutput.RowHeadersDefaultCellStyle = dataGridViewCellStyle33;
            this.IchiranForCSVOutput.RowHeadersVisible = false;
            this.IchiranForCSVOutput.RowTemplate.Height = 21;
            this.IchiranForCSVOutput.ShowCellToolTips = false;
            this.IchiranForCSVOutput.Size = new System.Drawing.Size(110, 20);
            this.IchiranForCSVOutput.TabIndex = 806;
            this.IchiranForCSVOutput.Visible = false;
            // 
            // dgvCustomTextBoxColumn1
            // 
            this.dgvCustomTextBoxColumn1.DataPropertyName = "CONTENA_SHURUI_CD";
            this.dgvCustomTextBoxColumn1.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle20;
            this.dgvCustomTextBoxColumn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn1.HeaderText = "コンテナ種類CD";
            this.dgvCustomTextBoxColumn1.Name = "dgvCustomTextBoxColumn1";
            this.dgvCustomTextBoxColumn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn1.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn1.popupWindowSetting")));
            this.dgvCustomTextBoxColumn1.ReadOnly = true;
            this.dgvCustomTextBoxColumn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn1.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn1.Width = 130;
            // 
            // dgvCustomTextBoxColumn2
            // 
            this.dgvCustomTextBoxColumn2.DataPropertyName = "CONTENA_SHURUI_NAME_RYAKU";
            this.dgvCustomTextBoxColumn2.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle21;
            this.dgvCustomTextBoxColumn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn2.HeaderText = "コンテナ種類名";
            this.dgvCustomTextBoxColumn2.Name = "dgvCustomTextBoxColumn2";
            this.dgvCustomTextBoxColumn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn2.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn2.popupWindowSetting")));
            this.dgvCustomTextBoxColumn2.ReadOnly = true;
            this.dgvCustomTextBoxColumn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn2.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn2.Width = 130;
            // 
            // dgvCustomTextBoxColumn3
            // 
            this.dgvCustomTextBoxColumn3.DataPropertyName = "CONTENA_CD";
            this.dgvCustomTextBoxColumn3.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle22;
            this.dgvCustomTextBoxColumn3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn3.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn3.HeaderText = "コンテナCD";
            this.dgvCustomTextBoxColumn3.Name = "dgvCustomTextBoxColumn3";
            this.dgvCustomTextBoxColumn3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn3.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn3.popupWindowSetting")));
            this.dgvCustomTextBoxColumn3.ReadOnly = true;
            this.dgvCustomTextBoxColumn3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn3.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn3.Width = 130;
            // 
            // dgvCustomTextBoxColumn4
            // 
            this.dgvCustomTextBoxColumn4.DataPropertyName = "CONTENA_NAME_RYAKU";
            this.dgvCustomTextBoxColumn4.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn4.DefaultCellStyle = dataGridViewCellStyle23;
            this.dgvCustomTextBoxColumn4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn4.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn4.HeaderText = "コンテナ名";
            this.dgvCustomTextBoxColumn4.Name = "dgvCustomTextBoxColumn4";
            this.dgvCustomTextBoxColumn4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn4.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn4.popupWindowSetting")));
            this.dgvCustomTextBoxColumn4.ReadOnly = true;
            this.dgvCustomTextBoxColumn4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn4.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn4.Width = 150;
            // 
            // dgvCustomTextBoxColumn5
            // 
            this.dgvCustomTextBoxColumn5.DataPropertyName = "GYOUSHA_CD";
            this.dgvCustomTextBoxColumn5.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle24.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle24;
            this.dgvCustomTextBoxColumn5.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn5.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn5.HeaderText = "業者CD";
            this.dgvCustomTextBoxColumn5.Name = "dgvCustomTextBoxColumn5";
            this.dgvCustomTextBoxColumn5.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn5.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn5.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn5.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn5.popupWindowSetting")));
            this.dgvCustomTextBoxColumn5.ReadOnly = true;
            this.dgvCustomTextBoxColumn5.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn5.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn5.Width = 80;
            // 
            // dgvCustomTextBoxColumn6
            // 
            this.dgvCustomTextBoxColumn6.DataPropertyName = "GYOUSHA_NAME_RYAKU";
            this.dgvCustomTextBoxColumn6.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle25.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn6.DefaultCellStyle = dataGridViewCellStyle25;
            this.dgvCustomTextBoxColumn6.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn6.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn6.HeaderText = "業者名";
            this.dgvCustomTextBoxColumn6.Name = "dgvCustomTextBoxColumn6";
            this.dgvCustomTextBoxColumn6.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn6.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn6.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn6.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn6.popupWindowSetting")));
            this.dgvCustomTextBoxColumn6.ReadOnly = true;
            this.dgvCustomTextBoxColumn6.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn6.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn6.Width = 145;
            // 
            // dgvCustomTextBoxColumn7
            // 
            this.dgvCustomTextBoxColumn7.DataPropertyName = "GENBA_CD";
            this.dgvCustomTextBoxColumn7.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle26.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn7.DefaultCellStyle = dataGridViewCellStyle26;
            this.dgvCustomTextBoxColumn7.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn7.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn7.HeaderText = "現場CD";
            this.dgvCustomTextBoxColumn7.Name = "dgvCustomTextBoxColumn7";
            this.dgvCustomTextBoxColumn7.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn7.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn7.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn7.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn7.popupWindowSetting")));
            this.dgvCustomTextBoxColumn7.ReadOnly = true;
            this.dgvCustomTextBoxColumn7.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn7.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dgvCustomTextBoxColumn8
            // 
            this.dgvCustomTextBoxColumn8.DataPropertyName = "GENBA_NAME_RYAKU";
            this.dgvCustomTextBoxColumn8.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle27.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn8.DefaultCellStyle = dataGridViewCellStyle27;
            this.dgvCustomTextBoxColumn8.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn8.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn8.HeaderText = "現場名";
            this.dgvCustomTextBoxColumn8.Name = "dgvCustomTextBoxColumn8";
            this.dgvCustomTextBoxColumn8.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn8.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn8.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn8.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn8.popupWindowSetting")));
            this.dgvCustomTextBoxColumn8.ReadOnly = true;
            this.dgvCustomTextBoxColumn8.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn8.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn8.Width = 155;
            // 
            // dgvCustomTextBoxColumn9
            // 
            this.dgvCustomTextBoxColumn9.DataPropertyName = "EIGYOU_TANTOU_CD";
            this.dgvCustomTextBoxColumn9.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle28.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn9.DefaultCellStyle = dataGridViewCellStyle28;
            this.dgvCustomTextBoxColumn9.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn9.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn9.HeaderText = "営業担当者CD";
            this.dgvCustomTextBoxColumn9.Name = "dgvCustomTextBoxColumn9";
            this.dgvCustomTextBoxColumn9.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn9.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn9.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn9.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn9.popupWindowSetting")));
            this.dgvCustomTextBoxColumn9.ReadOnly = true;
            this.dgvCustomTextBoxColumn9.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn9.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn9.Width = 120;
            // 
            // dgvCustomTextBoxColumn10
            // 
            this.dgvCustomTextBoxColumn10.DataPropertyName = "SHAIN_NAME_RYAKU";
            this.dgvCustomTextBoxColumn10.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn10.DefaultCellStyle = dataGridViewCellStyle29;
            this.dgvCustomTextBoxColumn10.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn10.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn10.HeaderText = "営業担当者名";
            this.dgvCustomTextBoxColumn10.Name = "dgvCustomTextBoxColumn10";
            this.dgvCustomTextBoxColumn10.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn10.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn10.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn10.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn10.popupWindowSetting")));
            this.dgvCustomTextBoxColumn10.ReadOnly = true;
            this.dgvCustomTextBoxColumn10.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn10.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn10.Width = 145;
            // 
            // dgvCustomTextBoxColumn11
            // 
            this.dgvCustomTextBoxColumn11.DataPropertyName = "SECCHI_DATE";
            this.dgvCustomTextBoxColumn11.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle30.Format = "d";
            dataGridViewCellStyle30.NullValue = null;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn11.DefaultCellStyle = dataGridViewCellStyle30;
            this.dgvCustomTextBoxColumn11.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn11.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn11.HeaderText = "設置日";
            this.dgvCustomTextBoxColumn11.Name = "dgvCustomTextBoxColumn11";
            this.dgvCustomTextBoxColumn11.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn11.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn11.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn11.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn11.popupWindowSetting")));
            this.dgvCustomTextBoxColumn11.ReadOnly = true;
            this.dgvCustomTextBoxColumn11.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn11.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn11.Width = 105;
            // 
            // dgvCustomTextBoxColumn12
            // 
            this.dgvCustomTextBoxColumn12.DataPropertyName = "DAYSCOUNT";
            this.dgvCustomTextBoxColumn12.DefaultBackColor = System.Drawing.Color.Empty;
            dataGridViewCellStyle31.Format = "d";
            dataGridViewCellStyle31.NullValue = null;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvCustomTextBoxColumn12.DefaultCellStyle = dataGridViewCellStyle31;
            this.dgvCustomTextBoxColumn12.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn12.FocusOutCheckMethod")));
            this.dgvCustomTextBoxColumn12.HeaderText = "経過日数";
            this.dgvCustomTextBoxColumn12.Name = "dgvCustomTextBoxColumn12";
            this.dgvCustomTextBoxColumn12.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("dgvCustomTextBoxColumn12.PopupSearchSendParams")));
            this.dgvCustomTextBoxColumn12.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.dgvCustomTextBoxColumn12.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("dgvCustomTextBoxColumn12.popupWindowSetting")));
            this.dgvCustomTextBoxColumn12.ReadOnly = true;
            this.dgvCustomTextBoxColumn12.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("dgvCustomTextBoxColumn12.RegistCheckMethod")));
            this.dgvCustomTextBoxColumn12.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCustomTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dgvCustomTextBoxColumn12.Width = 90;
            // 
            // ELAPSED_DAYS
            // 
            this.ELAPSED_DAYS.BackColor = System.Drawing.SystemColors.Window;
            this.ELAPSED_DAYS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ELAPSED_DAYS.DefaultBackColor = System.Drawing.Color.Empty;
            this.ELAPSED_DAYS.DisplayPopUp = null;
            this.ELAPSED_DAYS.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ELAPSED_DAYS.FocusOutCheckMethod")));
            this.ELAPSED_DAYS.ForeColor = System.Drawing.Color.Black;
            this.ELAPSED_DAYS.FormatSetting = "数値(#)フォーマット";
            this.ELAPSED_DAYS.IsInputErrorOccured = false;
            this.ELAPSED_DAYS.Location = new System.Drawing.Point(114, 55);
            this.ELAPSED_DAYS.Name = "ELAPSED_DAYS";
            this.ELAPSED_DAYS.PopupAfterExecute = null;
            this.ELAPSED_DAYS.PopupBeforeExecute = null;
            this.ELAPSED_DAYS.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ELAPSED_DAYS.PopupSearchSendParams")));
            this.ELAPSED_DAYS.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ELAPSED_DAYS.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ELAPSED_DAYS.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.ELAPSED_DAYS.RangeSetting = rangeSettingDto1;
            this.ELAPSED_DAYS.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ELAPSED_DAYS.RegistCheckMethod")));
            this.ELAPSED_DAYS.Size = new System.Drawing.Size(47, 19);
            this.ELAPSED_DAYS.TabIndex = 6;
            this.ELAPSED_DAYS.Tag = "無回転日数を指定してください";
            this.ELAPSED_DAYS.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.ELAPSED_DAYS.WordWrap = false;
            // 
            // EIGYOU_TANTOU_KBN
            // 
            this.EIGYOU_TANTOU_KBN.BackColor = System.Drawing.SystemColors.Window;
            this.EIGYOU_TANTOU_KBN.DBFieldsName = "EIGYOU_TANTOU_KBN";
            this.EIGYOU_TANTOU_KBN.DefaultBackColor = System.Drawing.Color.Empty;
            this.EIGYOU_TANTOU_KBN.DisplayItemName = "";
            this.EIGYOU_TANTOU_KBN.DisplayPopUp = null;
            this.EIGYOU_TANTOU_KBN.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOU_KBN.FocusOutCheckMethod")));
            this.EIGYOU_TANTOU_KBN.ForeColor = System.Drawing.Color.Black;
            this.EIGYOU_TANTOU_KBN.IsInputErrorOccured = false;
            this.EIGYOU_TANTOU_KBN.ItemDefinedTypes = "bit";
            this.EIGYOU_TANTOU_KBN.Location = new System.Drawing.Point(115, 463);
            this.EIGYOU_TANTOU_KBN.Name = "EIGYOU_TANTOU_KBN";
            this.EIGYOU_TANTOU_KBN.PopupAfterExecute = null;
            this.EIGYOU_TANTOU_KBN.PopupBeforeExecute = null;
            this.EIGYOU_TANTOU_KBN.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("EIGYOU_TANTOU_KBN.PopupSearchSendParams")));
            this.EIGYOU_TANTOU_KBN.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.EIGYOU_TANTOU_KBN.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("EIGYOU_TANTOU_KBN.popupWindowSetting")));
            this.EIGYOU_TANTOU_KBN.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("EIGYOU_TANTOU_KBN.RegistCheckMethod")));
            this.EIGYOU_TANTOU_KBN.Size = new System.Drawing.Size(100, 19);
            this.EIGYOU_TANTOU_KBN.TabIndex = 807;
            this.EIGYOU_TANTOU_KBN.Text = "1";
            this.EIGYOU_TANTOU_KBN.Visible = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(0, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(100, 20);
            this.label6.TabIndex = 803;
            this.label6.Text = "重複設置絞込";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customPanel2
            // 
            this.customPanel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customPanel2.Controls.Add(this.ChouhukuSecchiNomi2);
            this.customPanel2.Controls.Add(this.ChouhukuSecchiNomi1);
            this.customPanel2.Controls.Add(this.ChouhukuSecchiNomi);
            this.customPanel2.Location = new System.Drawing.Point(105, 0);
            this.customPanel2.Name = "customPanel2";
            this.customPanel2.Size = new System.Drawing.Size(153, 20);
            this.customPanel2.TabIndex = 808;
            // 
            // ChouhukuSecchiNomi2
            // 
            this.ChouhukuSecchiNomi2.AutoSize = true;
            this.ChouhukuSecchiNomi2.DefaultBackColor = System.Drawing.Color.Empty;
            this.ChouhukuSecchiNomi2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ChouhukuSecchiNomi2.FocusOutCheckMethod")));
            this.ChouhukuSecchiNomi2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ChouhukuSecchiNomi2.LinkedTextBox = "ChouhukuSecchiNomi";
            this.ChouhukuSecchiNomi2.Location = new System.Drawing.Point(86, 1);
            this.ChouhukuSecchiNomi2.Name = "ChouhukuSecchiNomi2";
            this.ChouhukuSecchiNomi2.PopupAfterExecute = null;
            this.ChouhukuSecchiNomi2.PopupBeforeExecute = null;
            this.ChouhukuSecchiNomi2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ChouhukuSecchiNomi2.PopupSearchSendParams")));
            this.ChouhukuSecchiNomi2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ChouhukuSecchiNomi2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ChouhukuSecchiNomi2.popupWindowSetting")));
            this.ChouhukuSecchiNomi2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ChouhukuSecchiNomi2.RegistCheckMethod")));
            this.ChouhukuSecchiNomi2.Size = new System.Drawing.Size(67, 17);
            this.ChouhukuSecchiNomi2.TabIndex = 120;
            this.ChouhukuSecchiNomi2.Text = "2.無効";
            this.ChouhukuSecchiNomi2.UseVisualStyleBackColor = true;
            this.ChouhukuSecchiNomi2.Value = "2";
            // 
            // ChouhukuSecchiNomi1
            // 
            this.ChouhukuSecchiNomi1.AutoSize = true;
            this.ChouhukuSecchiNomi1.DefaultBackColor = System.Drawing.Color.Empty;
            this.ChouhukuSecchiNomi1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ChouhukuSecchiNomi1.FocusOutCheckMethod")));
            this.ChouhukuSecchiNomi1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ChouhukuSecchiNomi1.LinkedTextBox = "ChouhukuSecchiNomi";
            this.ChouhukuSecchiNomi1.Location = new System.Drawing.Point(21, 1);
            this.ChouhukuSecchiNomi1.Name = "ChouhukuSecchiNomi1";
            this.ChouhukuSecchiNomi1.PopupAfterExecute = null;
            this.ChouhukuSecchiNomi1.PopupBeforeExecute = null;
            this.ChouhukuSecchiNomi1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ChouhukuSecchiNomi1.PopupSearchSendParams")));
            this.ChouhukuSecchiNomi1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ChouhukuSecchiNomi1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ChouhukuSecchiNomi1.popupWindowSetting")));
            this.ChouhukuSecchiNomi1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ChouhukuSecchiNomi1.RegistCheckMethod")));
            this.ChouhukuSecchiNomi1.Size = new System.Drawing.Size(67, 17);
            this.ChouhukuSecchiNomi1.TabIndex = 110;
            this.ChouhukuSecchiNomi1.Text = "1.有効";
            this.ChouhukuSecchiNomi1.UseVisualStyleBackColor = true;
            this.ChouhukuSecchiNomi1.Value = "1";
            // 
            // ChouhukuSecchiNomi
            // 
            this.ChouhukuSecchiNomi.BackColor = System.Drawing.SystemColors.Window;
            this.ChouhukuSecchiNomi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ChouhukuSecchiNomi.DefaultBackColor = System.Drawing.Color.Empty;
            this.ChouhukuSecchiNomi.DisplayPopUp = null;
            this.ChouhukuSecchiNomi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ChouhukuSecchiNomi.FocusOutCheckMethod")));
            this.ChouhukuSecchiNomi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ChouhukuSecchiNomi.ForeColor = System.Drawing.Color.Black;
            this.ChouhukuSecchiNomi.IsInputErrorOccured = false;
            this.ChouhukuSecchiNomi.LinkedRadioButtonArray = new string[] {
        "ChouhukuSecchiNomi1",
        "ChouhukuSecchiNomi2"};
            this.ChouhukuSecchiNomi.Location = new System.Drawing.Point(-1, -1);
            this.ChouhukuSecchiNomi.Name = "ChouhukuSecchiNomi";
            this.ChouhukuSecchiNomi.PopupAfterExecute = null;
            this.ChouhukuSecchiNomi.PopupBeforeExecute = null;
            this.ChouhukuSecchiNomi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ChouhukuSecchiNomi.PopupSearchSendParams")));
            this.ChouhukuSecchiNomi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ChouhukuSecchiNomi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ChouhukuSecchiNomi.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.ChouhukuSecchiNomi.RangeSetting = rangeSettingDto2;
            this.ChouhukuSecchiNomi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ChouhukuSecchiNomi.RegistCheckMethod")));
            this.ChouhukuSecchiNomi.Size = new System.Drawing.Size(20, 20);
            this.ChouhukuSecchiNomi.TabIndex = 100;
            this.ChouhukuSecchiNomi.Tag = "重複設置項目が○のデータを抽出します";
            this.ChouhukuSecchiNomi.WordWrap = false;
            // 
            // customPanel1
            // 
            this.customPanel1.Controls.Add(this.customPanel2);
            this.customPanel1.Controls.Add(this.label6);
            this.customPanel1.Location = new System.Drawing.Point(221, 462);
            this.customPanel1.Name = "customPanel1";
            this.customPanel1.Size = new System.Drawing.Size(269, 20);
            this.customPanel1.TabIndex = 7;
            this.customPanel1.TabStop = true;
            this.customPanel1.Visible = false;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customSearchHeader1.LinkedDataGridViewName = "Ichiran";
            this.customSearchHeader1.Location = new System.Drawing.Point(0, 76);
            this.customSearchHeader1.Name = "customSearchHeader1";
            this.customSearchHeader1.Size = new System.Drawing.Size(980, 26);
            this.customSearchHeader1.TabIndex = 809;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.SystemColors.Window;
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharacterLimitList = null;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayItemName = "";
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.GetCodeMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(952, 32);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 6;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupGetMasterField = "";
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupSetFormField = "";
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.PopupWindowName = "検索共通ポップアップ";
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.SetFormField = "";
            this.ISNOT_NEED_DELETE_FLG.ShortItemName = "";
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(47, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 810;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "True";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.ELAPSED_DAYS);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.customSearchHeader1);
            this.Controls.Add(this.customPanel1);
            this.Controls.Add(this.EIGYOU_TANTOU_KBN);
            this.Controls.Add(this.IchiranForCSVOutput);
            this.Controls.Add(this.customSortHeader1);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lb_genba);
            this.Controls.Add(this.SHAIN_NAME_RYAKU_HEADER);
            this.Controls.Add(this.EIGYOU_TANTOU_CD_HEADER);
            this.Controls.Add(this.CONTENA_NAME_RYAKU_HEADER);
            this.Controls.Add(this.CONTENA_CD_HEADER);
            this.Controls.Add(this.GENBA_NAME_RYAKU_HEADER);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.GENBA_CD_HEADER);
            this.Controls.Add(this.CONTENA_SHURUI_NAME_RYAKU_HEADER);
            this.Controls.Add(this.lb_gyousha);
            this.Controls.Add(this.CONTENA_SHURUI_CD_HEADER);
            this.Controls.Add(this.GYOUSHA_NAME_RYAKU_HEADER);
            this.Controls.Add(this.GYOUSHA_CD_HEADER);
            this.Name = "UIForm";
            this.Text = "UIForm";
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IchiranForCSVOutput)).EndInit();
            this.customPanel2.ResumeLayout(false);
            this.customPanel2.PerformLayout();
            this.customPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lb_gyousha;
        private System.Windows.Forms.Label lb_genba;
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
        internal r_framework.CustomControl.CustomAlphaNumTextBox EIGYOU_TANTOU_CD_HEADER;
        internal r_framework.CustomControl.CustomTextBox SHAIN_NAME_RYAKU_HEADER;
        private System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomAlphaNumTextBox CONTENA_SHURUI_CD_HEADER;
        internal r_framework.CustomControl.CustomTextBox CONTENA_SHURUI_NAME_RYAKU_HEADER;
        internal r_framework.CustomControl.CustomAlphaNumTextBox CONTENA_CD_HEADER;
        internal r_framework.CustomControl.CustomTextBox CONTENA_NAME_RYAKU_HEADER;
        public r_framework.CustomControl.DataGridCustomControl.CustomSortHeader customSortHeader1;
        internal r_framework.CustomControl.CustomDataGridView IchiranForCSVOutput;
        internal r_framework.CustomControl.CustomNumericTextBox2 ELAPSED_DAYS;
        public r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU_HEADER;
        public r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD_HEADER;
        public r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU_HEADER;
        public r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD_HEADER;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn1;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn2;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn3;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn4;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn5;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn6;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn7;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn8;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn9;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn10;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn11;
        private r_framework.CustomControl.DgvCustomTextBoxColumn dgvCustomTextBoxColumn12;
        private r_framework.CustomControl.CustomTextBox EIGYOU_TANTOU_KBN;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal r_framework.CustomControl.CustomPanel customPanel2;
        private r_framework.CustomControl.CustomRadioButton ChouhukuSecchiNomi2;
        private r_framework.CustomControl.CustomRadioButton ChouhukuSecchiNomi1;
        internal r_framework.CustomControl.CustomNumericTextBox2 ChouhukuSecchiNomi;
        internal r_framework.CustomControl.CustomPanel customPanel1;
        internal System.Windows.Forms.Label label2;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SecchiChouhuku;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_SHURUI_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_SHURUI_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SECCHI_DATE;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn CONTENA_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GYOUSHA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GYOUSHA_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GENBA_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn GENBA_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DAISUU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn EIGYOU_TANTOU_CD;
        private r_framework.CustomControl.DgvCustomTextBoxColumn SHAIN_NAME_RYAKU;
        private r_framework.CustomControl.DgvCustomTextBoxColumn DAYSCOUNT;
        private DataGridViewProgressBarColumn GRAPH;
        internal r_framework.CustomControl.DataGridCustomControl.CustomSearchHeader customSearchHeader1;
        internal r_framework.CustomControl.CustomAlphaNumTextBox ISNOT_NEED_DELETE_FLG;

    }
}