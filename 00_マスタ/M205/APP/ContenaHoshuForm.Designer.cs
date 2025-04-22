namespace Shougun.Core.Master.ContenaHoshu.APP
{
    partial class ContenaHoshuForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ContenaHoshuForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle1 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager1 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            this.lbl_Kensakujouken = new System.Windows.Forms.Label();
            this.txt_ConditionValue = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Hyoujijouken = new System.Windows.Forms.Label();
            this.chk_Sakujo = new r_framework.CustomControl.CustomCheckBox();
            this.txt_ConditionItem = new r_framework.CustomControl.CustomTextBox();
            this.dgvCsv = new r_framework.CustomControl.CustomDataGridView(this.components);
            this.chkDelete = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtContenaSyuruiCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtContenaSyuruiName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtContenaCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtContenaMei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtContenaRyakuMei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtGyoushaCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtGyoushaMei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtGenbaCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtGenbaMei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtKyotenCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtKyotenMei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSyaryouCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSharyouMei = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtimKounyubi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtimSaishinsyuufukubi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtimSettibi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dtimHikiagebi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtJoukyouCd = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtJoukyou = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtBikou = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtKoushinsha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtKoushinbi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSakuseisha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtSakuseibi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.タイムスタンプ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.mlt_gcCustomMultiRow = new r_framework.CustomControl.GcCustomMultiRow(this.components);
            this.uiDetail1 = new Shougun.Core.Master.ContenaHoshu.MultiRowTemplate.UIDetail();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCsv)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mlt_gcCustomMultiRow)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Kensakujouken
            // 
            this.lbl_Kensakujouken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kensakujouken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kensakujouken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kensakujouken.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_Kensakujouken.ForeColor = System.Drawing.Color.White;
            this.lbl_Kensakujouken.Location = new System.Drawing.Point(10, 4);
            this.lbl_Kensakujouken.Name = "lbl_Kensakujouken";
            this.lbl_Kensakujouken.Size = new System.Drawing.Size(93, 20);
            this.lbl_Kensakujouken.TabIndex = 0;
            this.lbl_Kensakujouken.Text = "検索条件";
            this.lbl_Kensakujouken.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_ConditionValue
            // 
            this.txt_ConditionValue.BackColor = System.Drawing.SystemColors.Window;
            this.txt_ConditionValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ConditionValue.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.txt_ConditionValue.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_ConditionValue.DisplayItemName = "検索条件";
            this.txt_ConditionValue.DisplayPopUp = null;
            this.txt_ConditionValue.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ConditionValue.FocusOutCheckMethod")));
            this.txt_ConditionValue.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_ConditionValue.ForeColor = System.Drawing.Color.Black;
            this.txt_ConditionValue.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.txt_ConditionValue.IsInputErrorOccured = false;
            this.txt_ConditionValue.ItemDefinedTypes = "DATETIME";
            this.txt_ConditionValue.Location = new System.Drawing.Point(257, 4);
            this.txt_ConditionValue.MaxLength = 20;
            this.txt_ConditionValue.Name = "txt_ConditionValue";
            this.txt_ConditionValue.PopupAfterExecute = null;
            this.txt_ConditionValue.PopupBeforeExecute = null;
            this.txt_ConditionValue.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_ConditionValue.PopupSearchSendParams")));
            this.txt_ConditionValue.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_ConditionValue.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_ConditionValue.popupWindowSetting")));
            this.txt_ConditionValue.prevText = null;
            this.txt_ConditionValue.PrevText = null;
            this.txt_ConditionValue.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ConditionValue.RegistCheckMethod")));
            this.txt_ConditionValue.ShortItemName = "検索条件";
            this.txt_ConditionValue.Size = new System.Drawing.Size(290, 20);
            this.txt_ConditionValue.TabIndex = 2;
            this.txt_ConditionValue.Tag = "検索する文字を入力してください";
            this.txt_ConditionValue.Enter += new System.EventHandler(this.txt_ConditionValue_Enter);
            this.txt_ConditionValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_ConditionValue_KeyPress);
            // 
            // lbl_Hyoujijouken
            // 
            this.lbl_Hyoujijouken.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Hyoujijouken.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Hyoujijouken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Hyoujijouken.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_Hyoujijouken.ForeColor = System.Drawing.Color.White;
            this.lbl_Hyoujijouken.Location = new System.Drawing.Point(553, 4);
            this.lbl_Hyoujijouken.Name = "lbl_Hyoujijouken";
            this.lbl_Hyoujijouken.Size = new System.Drawing.Size(93, 20);
            this.lbl_Hyoujijouken.TabIndex = 3;
            this.lbl_Hyoujijouken.Text = "表示条件";
            this.lbl_Hyoujijouken.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk_Sakujo
            // 
            this.chk_Sakujo.AutoSize = true;
            this.chk_Sakujo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.chk_Sakujo.DefaultBackColor = System.Drawing.Color.Empty;
            this.chk_Sakujo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chk_Sakujo.FocusOutCheckMethod")));
            this.chk_Sakujo.Location = new System.Drawing.Point(652, 6);
            this.chk_Sakujo.Name = "chk_Sakujo";
            this.chk_Sakujo.PopupAfterExecute = null;
            this.chk_Sakujo.PopupBeforeExecute = null;
            this.chk_Sakujo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chk_Sakujo.PopupSearchSendParams")));
            this.chk_Sakujo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chk_Sakujo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chk_Sakujo.popupWindowSetting")));
            this.chk_Sakujo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chk_Sakujo.RegistCheckMethod")));
            this.chk_Sakujo.Size = new System.Drawing.Size(48, 16);
            this.chk_Sakujo.TabIndex = 5;
            this.chk_Sakujo.Tag = "削除データを検索する場合チェックを付けてください";
            this.chk_Sakujo.Text = "削除済も含めて全て表示";
            this.chk_Sakujo.UseVisualStyleBackColor = false;
            // 
            // txt_ConditionItem
            // 
            this.txt_ConditionItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_ConditionItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_ConditionItem.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txt_ConditionItem.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_ConditionItem.DisplayItemName = "検索条件";
            this.txt_ConditionItem.DisplayPopUp = null;
            this.txt_ConditionItem.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ConditionItem.FocusOutCheckMethod")));
            this.txt_ConditionItem.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_ConditionItem.ForeColor = System.Drawing.Color.Black;
            this.txt_ConditionItem.IsInputErrorOccured = false;
            this.txt_ConditionItem.ItemDefinedTypes = "";
            this.txt_ConditionItem.Location = new System.Drawing.Point(108, 4);
            this.txt_ConditionItem.MaxLength = 10;
            this.txt_ConditionItem.Name = "txt_ConditionItem";
            this.txt_ConditionItem.PopupAfterExecute = null;
            this.txt_ConditionItem.PopupAfterExecuteMethod = "";
            this.txt_ConditionItem.PopupBeforeExecute = null;
            this.txt_ConditionItem.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_ConditionItem.PopupSearchSendParams")));
            this.txt_ConditionItem.PopupSendParams = new string[] {
        "mlt_gcCustomMultiRow"};
            this.txt_ConditionItem.PopupSetFormField = "txt_ConditionItem,txt_ConditionValue";
            this.txt_ConditionItem.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_ConditionItem.PopupWindowName = "マスタ検索項目ポップアップ";
            this.txt_ConditionItem.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_ConditionItem.popupWindowSetting")));
            this.txt_ConditionItem.prevText = null;
            this.txt_ConditionItem.PrevText = null;
            this.txt_ConditionItem.ReadOnly = true;
            this.txt_ConditionItem.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_ConditionItem.RegistCheckMethod")));
            this.txt_ConditionItem.SetFormField = "txt_ConditionItem,txt_ConditionValue";
            this.txt_ConditionItem.ShortItemName = "検索条件";
            this.txt_ConditionItem.Size = new System.Drawing.Size(150, 20);
            this.txt_ConditionItem.TabIndex = 1;
            this.txt_ConditionItem.Tag = "検索条件を指定してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // dgvCsv
            // 
            this.dgvCsv.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCsv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvCsv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCsv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.chkDelete,
            this.txtContenaSyuruiCd,
            this.txtContenaSyuruiName,
            this.txtContenaCd,
            this.txtContenaMei,
            this.txtContenaRyakuMei,
            this.txtJoukyouCd,
            this.txtJoukyou,
            this.txtGyoushaCd,
            this.txtGyoushaMei,
            this.txtGenbaCd,
            this.txtGenbaMei,
            this.txtKyotenCd,
            this.txtKyotenMei,
            this.txtSyaryouCd,
            this.txtSharyouMei,
            this.txtBikou,
            this.dtimKounyubi,
            this.dtimSaishinsyuufukubi,
            this.dtimSettibi,
            this.dtimHikiagebi,
            this.txtKoushinsha,
            this.txtKoushinbi,
            this.txtSakuseisha,
            this.txtSakuseibi,
            this.タイムスタンプ});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvCsv.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvCsv.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvCsv.EnableHeadersVisualStyles = false;
            this.dgvCsv.GridColor = System.Drawing.Color.White;
            this.dgvCsv.IsReload = false;
            this.dgvCsv.LinkedDataPanelName = null;
            this.dgvCsv.Location = new System.Drawing.Point(896, 6);
            this.dgvCsv.MultiSelect = false;
            this.dgvCsv.Name = "dgvCsv";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvCsv.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvCsv.RowHeadersVisible = false;
            this.dgvCsv.RowTemplate.Height = 21;
            this.dgvCsv.ShowCellToolTips = false;
            this.dgvCsv.Size = new System.Drawing.Size(92, 21);
            this.dgvCsv.TabIndex = 416;
            this.dgvCsv.TabStop = false;
            this.dgvCsv.Visible = false;
            // 
            // chkDelete
            // 
            this.chkDelete.DataPropertyName = "DELETE_FLG";
            this.chkDelete.HeaderText = "削除";
            this.chkDelete.Name = "chkDelete";
            this.chkDelete.ReadOnly = true;
            this.chkDelete.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtContenaSyuruiCd
            // 
            this.txtContenaSyuruiCd.DataPropertyName = "CONTENA_SHURUI_CD";
            this.txtContenaSyuruiCd.HeaderText = "コンテナ種類CD※";
            this.txtContenaSyuruiCd.Name = "txtContenaSyuruiCd";
            this.txtContenaSyuruiCd.ReadOnly = true;
            this.txtContenaSyuruiCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtContenaSyuruiName
            // 
            this.txtContenaSyuruiName.DataPropertyName = "CONTENA_SHURUI_NAME_RYAKU";
            this.txtContenaSyuruiName.HeaderText = "コンテナ種類名";
            this.txtContenaSyuruiName.Name = "txtContenaSyuruiName";
            this.txtContenaSyuruiName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtContenaCd
            // 
            this.txtContenaCd.DataPropertyName = "CONTENA_CD";
            this.txtContenaCd.HeaderText = "コンテナCD※";
            this.txtContenaCd.Name = "txtContenaCd";
            this.txtContenaCd.ReadOnly = true;
            this.txtContenaCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtContenaMei
            // 
            this.txtContenaMei.DataPropertyName = "CONTENA_NAME";
            this.txtContenaMei.HeaderText = "コンテナ名※";
            this.txtContenaMei.Name = "txtContenaMei";
            this.txtContenaMei.ReadOnly = true;
            this.txtContenaMei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtContenaRyakuMei
            // 
            this.txtContenaRyakuMei.DataPropertyName = "CONTENA_NAME_RYAKU";
            this.txtContenaRyakuMei.HeaderText = "コンテナ略称名※";
            this.txtContenaRyakuMei.Name = "txtContenaRyakuMei";
            this.txtContenaRyakuMei.ReadOnly = true;
            this.txtContenaRyakuMei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtGyoushaCd
            // 
            this.txtGyoushaCd.DataPropertyName = "GYOUSHA_CD";
            this.txtGyoushaCd.HeaderText = "業者CD";
            this.txtGyoushaCd.Name = "txtGyoushaCd";
            this.txtGyoushaCd.ReadOnly = true;
            this.txtGyoushaCd.Visible = false;
            this.txtGyoushaCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtGyoushaMei
            // 
            this.txtGyoushaMei.DataPropertyName = "GYOUSHA_NAME_RYAKU";
            this.txtGyoushaMei.HeaderText = "業者名";
            this.txtGyoushaMei.Name = "txtGyoushaMei";
            this.txtGyoushaMei.ReadOnly = true;
            this.txtGyoushaMei.Visible = false;
            this.txtGyoushaMei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtGenbaCd
            // 
            this.txtGenbaCd.DataPropertyName = "GENBA_CD";
            this.txtGenbaCd.HeaderText = "現場CD";
            this.txtGenbaCd.Name = "txtGenbaCd";
            this.txtGenbaCd.ReadOnly = true;
            this.txtGenbaCd.Visible = false;
            this.txtGenbaCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtGenbaMei
            // 
            this.txtGenbaMei.DataPropertyName = "GENBA_NAME_RYAKU";
            this.txtGenbaMei.HeaderText = "現場名";
            this.txtGenbaMei.Name = "txtGenbaMei";
            this.txtGenbaMei.ReadOnly = true;
            this.txtGenbaMei.Visible = false;
            this.txtGenbaMei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtKyotenCd
            // 
            this.txtKyotenCd.DataPropertyName = "KYOTEN_CD";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            this.txtKyotenCd.DefaultCellStyle = dataGridViewCellStyle2;
            this.txtKyotenCd.HeaderText = "拠点CD";
            this.txtKyotenCd.Name = "txtKyotenCd";
            this.txtKyotenCd.ReadOnly = true;
            this.txtKyotenCd.Visible = false;
            this.txtKyotenCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtKyotenMei
            // 
            this.txtKyotenMei.DataPropertyName = "KYOTEN_NAME_RYAKU";
            this.txtKyotenMei.HeaderText = "拠点名";
            this.txtKyotenMei.Name = "txtKyotenMei";
            this.txtKyotenMei.ReadOnly = true;
            this.txtKyotenMei.Visible = false;
            this.txtKyotenMei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtSyaryouCd
            // 
            this.txtSyaryouCd.DataPropertyName = "SHARYOU_CD";
            this.txtSyaryouCd.HeaderText = "車輌CD";
            this.txtSyaryouCd.Name = "txtSyaryouCd";
            this.txtSyaryouCd.ReadOnly = true;
            this.txtSyaryouCd.Visible = false;
            this.txtSyaryouCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtSharyouMei
            // 
            this.txtSharyouMei.DataPropertyName = "SHARYOU_NAME_RYAKU";
            this.txtSharyouMei.HeaderText = "車輌名";
            this.txtSharyouMei.Name = "txtSharyouMei";
            this.txtSharyouMei.ReadOnly = true;
            this.txtSharyouMei.Visible = false;
            this.txtSharyouMei.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dtimKounyubi
            // 
            this.dtimKounyubi.DataPropertyName = "KOUNYUU_DATE";
            this.dtimKounyubi.HeaderText = "購入日";
            this.dtimKounyubi.Name = "dtimKounyubi";
            this.dtimKounyubi.ReadOnly = true;
            this.dtimKounyubi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dtimSaishinsyuufukubi
            // 
            this.dtimSaishinsyuufukubi.DataPropertyName = "LAST_SHUUFUKU_DATE";
            this.dtimSaishinsyuufukubi.HeaderText = "最終修復日";
            this.dtimSaishinsyuufukubi.Name = "dtimSaishinsyuufukubi";
            this.dtimSaishinsyuufukubi.ReadOnly = true;
            this.dtimSaishinsyuufukubi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dtimSettibi
            // 
            this.dtimSettibi.DataPropertyName = "SECCHI_DATE";
            this.dtimSettibi.HeaderText = "設置日";
            this.dtimSettibi.Name = "dtimSettibi";
            this.dtimSettibi.ReadOnly = true;
            this.dtimSettibi.Visible = false;
            this.dtimSettibi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // dtimHikiagebi
            //
            this.dtimHikiagebi.DataPropertyName = "HIKIAGE_DATE";
            this.dtimHikiagebi.HeaderText = "引揚日";
            this.dtimHikiagebi.Name = "dtimHikiagebi";
            this.dtimHikiagebi.ReadOnly = true;
            this.dtimHikiagebi.Visible = false;
            this.dtimHikiagebi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtJoukyouCd
            // 
            this.txtJoukyouCd.DataPropertyName = "JOUKYOU_KBN";
            this.txtJoukyouCd.HeaderText = "状況CD";
            this.txtJoukyouCd.Name = "txtJoukyouCd";
            this.txtJoukyouCd.ReadOnly = true;
            this.txtJoukyouCd.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtJoukyou
            // 
            this.txtJoukyou.DataPropertyName = "CONTENA_JOUKYOU_NAME_RYAKU";
            this.txtJoukyou.HeaderText = "状況";
            this.txtJoukyou.Name = "txtJoukyou";
            this.txtJoukyou.ReadOnly = true;
            this.txtJoukyou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtBikou
            // 
            this.txtBikou.DataPropertyName = "CONTENA_BIKOU";
            this.txtBikou.HeaderText = "備考";
            this.txtBikou.Name = "txtBikou";
            this.txtBikou.ReadOnly = true;
            this.txtBikou.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtKoushinsha
            // 
            this.txtKoushinsha.DataPropertyName = "UPDATE_USER";
            this.txtKoushinsha.HeaderText = "更新者";
            this.txtKoushinsha.Name = "txtKoushinsha";
            this.txtKoushinsha.ReadOnly = true;
            this.txtKoushinsha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtKoushinbi
            // 
            this.txtKoushinbi.DataPropertyName = "UPDATE_DATE";
            this.txtKoushinbi.HeaderText = "更新日";
            this.txtKoushinbi.Name = "txtKoushinbi";
            this.txtKoushinbi.ReadOnly = true;
            this.txtKoushinbi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtSakuseisha
            // 
            this.txtSakuseisha.DataPropertyName = "CREATE_USER";
            this.txtSakuseisha.HeaderText = "作成者";
            this.txtSakuseisha.Name = "txtSakuseisha";
            this.txtSakuseisha.ReadOnly = true;
            this.txtSakuseisha.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // txtSakuseibi
            // 
            this.txtSakuseibi.DataPropertyName = "CREATE_DATE";
            this.txtSakuseibi.HeaderText = "作成日";
            this.txtSakuseibi.Name = "txtSakuseibi";
            this.txtSakuseibi.ReadOnly = true;
            this.txtSakuseibi.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // タイムスタンプ
            // 
            this.タイムスタンプ.DataPropertyName = "TIME_STAMP";
            this.タイムスタンプ.HeaderText = "タイムスタンプ";
            this.タイムスタンプ.Name = "タイムスタンプ";
            this.タイムスタンプ.ReadOnly = true;
            this.タイムスタンプ.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.タイムスタンプ.Visible = false;
            // 
            // mlt_gcCustomMultiRow
            // 
            this.mlt_gcCustomMultiRow.AllowUserToDeleteRows = false;
            cellStyle1.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.mlt_gcCustomMultiRow.ColumnHeadersDefaultHeaderCellStyle = cellStyle1;
            this.mlt_gcCustomMultiRow.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Red);
            this.mlt_gcCustomMultiRow.DefaultCellStyle = cellStyle2;
            this.mlt_gcCustomMultiRow.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.mlt_gcCustomMultiRow.Location = new System.Drawing.Point(9, 31);
            this.mlt_gcCustomMultiRow.MultiSelect = false;
            this.mlt_gcCustomMultiRow.Name = "mlt_gcCustomMultiRow";
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftLeft)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftRight)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ReverseSelectCurrentRow)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToPreviousPage)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToNextPage)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager1.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            this.mlt_gcCustomMultiRow.ShortcutKeyManager = shortcutKeyManager1;
            this.mlt_gcCustomMultiRow.Size = new System.Drawing.Size(985, 450);
            this.mlt_gcCustomMultiRow.SplitMode = GrapeCity.Win.MultiRow.SplitMode.None;
            this.mlt_gcCustomMultiRow.TabIndex = 7;
            this.mlt_gcCustomMultiRow.Template = this.uiDetail1;
            this.mlt_gcCustomMultiRow.CellValidating += new System.EventHandler<GrapeCity.Win.MultiRow.CellValidatingEventArgs>(this.mlt_gcCustomMultiRow_CellValidating);
            this.mlt_gcCustomMultiRow.CellEndEdit += new System.EventHandler<GrapeCity.Win.MultiRow.CellEndEditEventArgs>(this.mlt_gcCustomMultiRow_CellEndEdit);
            this.mlt_gcCustomMultiRow.CellEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.mlt_gcCustomMultiRow_CellEnter);
            // 
            // ContenaHoshuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.dgvCsv);
            this.Controls.Add(this.mlt_gcCustomMultiRow);
            this.Controls.Add(this.chk_Sakujo);
            this.Controls.Add(this.lbl_Hyoujijouken);
            this.Controls.Add(this.txt_ConditionValue);
            this.Controls.Add(this.txt_ConditionItem);
            this.Controls.Add(this.lbl_Kensakujouken);
            this.Location = new System.Drawing.Point(108, 4);
            this.Name = "ContenaHoshuForm";
            this.Text = "UIForm";
            this.Shown += new System.EventHandler(this.ContenaHoshuForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCsv)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mlt_gcCustomMultiRow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lbl_Kensakujouken;
        internal System.Windows.Forms.Label lbl_Hyoujijouken;
        internal r_framework.CustomControl.CustomCheckBox chk_Sakujo;
        internal r_framework.CustomControl.CustomTextBox txt_ConditionValue;
        private Shougun.Core.Master.ContenaHoshu.MultiRowTemplate.UIDetail uiDetail1;
        internal r_framework.CustomControl.GcCustomMultiRow mlt_gcCustomMultiRow;
        internal r_framework.CustomControl.CustomTextBox txt_ConditionItem;
        public r_framework.CustomControl.CustomDataGridView dgvCsv;
        private System.Windows.Forms.DataGridViewTextBoxColumn chkDelete;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtContenaSyuruiCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtContenaSyuruiName;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtContenaCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtContenaMei;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtContenaRyakuMei;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtGyoushaCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtGyoushaMei;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtGenbaCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtGenbaMei;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtKyotenCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtKyotenMei;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtSyaryouCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtSharyouMei;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtimKounyubi;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtimSaishinsyuufukubi;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtimSettibi;
        private System.Windows.Forms.DataGridViewTextBoxColumn dtimHikiagebi;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtJoukyouCd;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtJoukyou;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtBikou;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtKoushinsha;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtKoushinbi;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtSakuseisha;
        private System.Windows.Forms.DataGridViewTextBoxColumn txtSakuseibi;
        private System.Windows.Forms.DataGridViewTextBoxColumn タイムスタンプ;

    }
}