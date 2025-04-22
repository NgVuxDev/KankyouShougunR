namespace Shougun.Core.ElectronicManifest.MihimodukeIchiran.APP
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
			if(disposing && (components != null))
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
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            GrapeCity.Win.MultiRow.CellStyle cellStyle2 = new GrapeCity.Win.MultiRow.CellStyle();
            GrapeCity.Win.MultiRow.ShortcutKeyManager shortcutKeyManager2 = new GrapeCity.Win.MultiRow.ShortcutKeyManager();
            this.lbl_HikiwatasiBi = new System.Windows.Forms.Label();
            this.lbl_MasterInfo = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cantxt_JigyoujouCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.cantxt_JigyoushaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ctxt_JigyoujouName = new r_framework.CustomControl.CustomTextBox();
            this.ctxt_JigyoushaName = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Jigyoujou = new System.Windows.Forms.Label();
            this.lbl_Jigyousha = new System.Windows.Forms.Label();
            this.lbl_ShogunMaster = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.cantxt_GenbaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.cantxt_GyoushaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ctxt_GenbaName = new r_framework.CustomControl.CustomTextBox();
            this.ctxt_GyoushaName = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Genba = new System.Windows.Forms.Label();
            this.lbl_Gyousha = new System.Windows.Forms.Label();
            this.ctxt_Sentakusaki = new r_framework.CustomControl.CustomTextBox();
            this.lbl_SentakuSaki = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cdate_HikiwatasiBiFrom = new r_framework.CustomControl.CustomDateTimePicker();
            this.cdate_HikiwatasiBiTo = new r_framework.CustomControl.CustomDateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.cntxt_ManifestIdFrom = new r_framework.CustomControl.CustomNumericTextBox2();
            this.cntxt_ManifestIdTo = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label3 = new System.Windows.Forms.Label();
            this.Ichiran = new r_framework.CustomControl.GcCustomMultiRow(this.components);
            this.uiDetail1 = new Shougun.Core.ElectronicManifest.MihimodukeIchiran.MultiRowTemplate.UIDetail();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_HikiwatasiBi
            // 
            this.lbl_HikiwatasiBi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_HikiwatasiBi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_HikiwatasiBi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_HikiwatasiBi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_HikiwatasiBi.ForeColor = System.Drawing.Color.White;
            this.lbl_HikiwatasiBi.Location = new System.Drawing.Point(0, 0);
            this.lbl_HikiwatasiBi.Name = "lbl_HikiwatasiBi";
            this.lbl_HikiwatasiBi.Size = new System.Drawing.Size(128, 20);
            this.lbl_HikiwatasiBi.TabIndex = 0;
            this.lbl_HikiwatasiBi.Text = "引渡し日";
            this.lbl_HikiwatasiBi.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_MasterInfo
            // 
            this.lbl_MasterInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_MasterInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_MasterInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_MasterInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_MasterInfo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_MasterInfo.ForeColor = System.Drawing.Color.White;
            this.lbl_MasterInfo.Location = new System.Drawing.Point(5, 380);
            this.lbl_MasterInfo.Name = "lbl_MasterInfo";
            this.lbl_MasterInfo.Size = new System.Drawing.Size(110, 20);
            this.lbl_MasterInfo.TabIndex = 8;
            this.lbl_MasterInfo.Text = "マスタ情報";
            this.lbl_MasterInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.cantxt_JigyoujouCd);
            this.panel2.Controls.Add(this.cantxt_JigyoushaCd);
            this.panel2.Controls.Add(this.ctxt_JigyoujouName);
            this.panel2.Controls.Add(this.ctxt_JigyoushaName);
            this.panel2.Controls.Add(this.lbl_Jigyoujou);
            this.panel2.Controls.Add(this.lbl_Jigyousha);
            this.panel2.Location = new System.Drawing.Point(0, 389);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(570, 89);
            this.panel2.TabIndex = 696;
            // 
            // cantxt_JigyoujouCd
            // 
            this.cantxt_JigyoujouCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.cantxt_JigyoujouCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_JigyoujouCd.CharacterLimitList = null;
            this.cantxt_JigyoujouCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_JigyoujouCd.DisplayPopUp = null;
            this.cantxt_JigyoujouCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_JigyoujouCd.FocusOutCheckMethod")));
            this.cantxt_JigyoujouCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_JigyoujouCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_JigyoujouCd.IsInputErrorOccured = false;
            this.cantxt_JigyoujouCd.Location = new System.Drawing.Point(82, 50);
            this.cantxt_JigyoujouCd.Name = "cantxt_JigyoujouCd";
            this.cantxt_JigyoujouCd.PopupAfterExecute = null;
            this.cantxt_JigyoujouCd.PopupBeforeExecute = null;
            this.cantxt_JigyoujouCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_JigyoujouCd.PopupSearchSendParams")));
            this.cantxt_JigyoujouCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_JigyoujouCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_JigyoujouCd.popupWindowSetting")));
            this.cantxt_JigyoujouCd.ReadOnly = true;
            this.cantxt_JigyoujouCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_JigyoujouCd.RegistCheckMethod")));
            this.cantxt_JigyoujouCd.Size = new System.Drawing.Size(93, 20);
            this.cantxt_JigyoujouCd.TabIndex = 13;
            this.cantxt_JigyoujouCd.TabStop = false;
            this.cantxt_JigyoujouCd.Tag = "";
            // 
            // cantxt_JigyoushaCd
            // 
            this.cantxt_JigyoushaCd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.cantxt_JigyoushaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_JigyoushaCd.CharacterLimitList = null;
            this.cantxt_JigyoushaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_JigyoushaCd.DisplayPopUp = null;
            this.cantxt_JigyoushaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_JigyoushaCd.FocusOutCheckMethod")));
            this.cantxt_JigyoushaCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_JigyoushaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_JigyoushaCd.IsInputErrorOccured = false;
            this.cantxt_JigyoushaCd.Location = new System.Drawing.Point(82, 22);
            this.cantxt_JigyoushaCd.Name = "cantxt_JigyoushaCd";
            this.cantxt_JigyoushaCd.PopupAfterExecute = null;
            this.cantxt_JigyoushaCd.PopupBeforeExecute = null;
            this.cantxt_JigyoushaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_JigyoushaCd.PopupSearchSendParams")));
            this.cantxt_JigyoushaCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_JigyoushaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_JigyoushaCd.popupWindowSetting")));
            this.cantxt_JigyoushaCd.ReadOnly = true;
            this.cantxt_JigyoushaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_JigyoushaCd.RegistCheckMethod")));
            this.cantxt_JigyoushaCd.Size = new System.Drawing.Size(93, 20);
            this.cantxt_JigyoushaCd.TabIndex = 10;
            this.cantxt_JigyoushaCd.TabStop = false;
            this.cantxt_JigyoushaCd.Tag = "";
            // 
            // ctxt_JigyoujouName
            // 
            this.ctxt_JigyoujouName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_JigyoujouName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_JigyoujouName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_JigyoujouName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_JigyoujouName.DisplayPopUp = null;
            this.ctxt_JigyoujouName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_JigyoujouName.FocusOutCheckMethod")));
            this.ctxt_JigyoujouName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_JigyoujouName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_JigyoujouName.IsInputErrorOccured = false;
            this.ctxt_JigyoujouName.Location = new System.Drawing.Point(174, 50);
            this.ctxt_JigyoujouName.MaxLength = 0;
            this.ctxt_JigyoujouName.Name = "ctxt_JigyoujouName";
            this.ctxt_JigyoujouName.PopupAfterExecute = null;
            this.ctxt_JigyoujouName.PopupBeforeExecute = null;
            this.ctxt_JigyoujouName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_JigyoujouName.PopupSearchSendParams")));
            this.ctxt_JigyoujouName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_JigyoujouName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_JigyoujouName.popupWindowSetting")));
            this.ctxt_JigyoujouName.ReadOnly = true;
            this.ctxt_JigyoujouName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_JigyoujouName.RegistCheckMethod")));
            this.ctxt_JigyoujouName.Size = new System.Drawing.Size(388, 20);
            this.ctxt_JigyoujouName.TabIndex = 14;
            this.ctxt_JigyoujouName.TabStop = false;
            this.ctxt_JigyoujouName.Tag = "";
            // 
            // ctxt_JigyoushaName
            // 
            this.ctxt_JigyoushaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_JigyoushaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_JigyoushaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_JigyoushaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_JigyoushaName.DisplayPopUp = null;
            this.ctxt_JigyoushaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_JigyoushaName.FocusOutCheckMethod")));
            this.ctxt_JigyoushaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_JigyoushaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_JigyoushaName.IsInputErrorOccured = false;
            this.ctxt_JigyoushaName.Location = new System.Drawing.Point(174, 22);
            this.ctxt_JigyoushaName.MaxLength = 0;
            this.ctxt_JigyoushaName.Name = "ctxt_JigyoushaName";
            this.ctxt_JigyoushaName.PopupAfterExecute = null;
            this.ctxt_JigyoushaName.PopupBeforeExecute = null;
            this.ctxt_JigyoushaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_JigyoushaName.PopupSearchSendParams")));
            this.ctxt_JigyoushaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_JigyoushaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_JigyoushaName.popupWindowSetting")));
            this.ctxt_JigyoushaName.ReadOnly = true;
            this.ctxt_JigyoushaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_JigyoushaName.RegistCheckMethod")));
            this.ctxt_JigyoushaName.Size = new System.Drawing.Size(388, 20);
            this.ctxt_JigyoushaName.TabIndex = 11;
            this.ctxt_JigyoushaName.TabStop = false;
            this.ctxt_JigyoushaName.Tag = "";
            // 
            // lbl_Jigyoujou
            // 
            this.lbl_Jigyoujou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Jigyoujou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Jigyoujou.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Jigyoujou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Jigyoujou.ForeColor = System.Drawing.Color.White;
            this.lbl_Jigyoujou.Location = new System.Drawing.Point(4, 50);
            this.lbl_Jigyoujou.Name = "lbl_Jigyoujou";
            this.lbl_Jigyoujou.Size = new System.Drawing.Size(72, 20);
            this.lbl_Jigyoujou.TabIndex = 12;
            this.lbl_Jigyoujou.Text = "事業場";
            this.lbl_Jigyoujou.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Jigyousha
            // 
            this.lbl_Jigyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Jigyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Jigyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Jigyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Jigyousha.ForeColor = System.Drawing.Color.White;
            this.lbl_Jigyousha.Location = new System.Drawing.Point(4, 22);
            this.lbl_Jigyousha.Name = "lbl_Jigyousha";
            this.lbl_Jigyousha.Size = new System.Drawing.Size(72, 20);
            this.lbl_Jigyousha.TabIndex = 9;
            this.lbl_Jigyousha.Text = "事業者";
            this.lbl_Jigyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_ShogunMaster
            // 
            this.lbl_ShogunMaster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_ShogunMaster.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_ShogunMaster.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ShogunMaster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_ShogunMaster.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_ShogunMaster.ForeColor = System.Drawing.Color.White;
            this.lbl_ShogunMaster.Location = new System.Drawing.Point(584, 380);
            this.lbl_ShogunMaster.Name = "lbl_ShogunMaster";
            this.lbl_ShogunMaster.Size = new System.Drawing.Size(110, 20);
            this.lbl_ShogunMaster.TabIndex = 15;
            this.lbl_ShogunMaster.Text = "将軍連携マスタ";
            this.lbl_ShogunMaster.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.cantxt_GenbaCd);
            this.panel3.Controls.Add(this.cantxt_GyoushaCd);
            this.panel3.Controls.Add(this.ctxt_GenbaName);
            this.panel3.Controls.Add(this.ctxt_GyoushaName);
            this.panel3.Controls.Add(this.lbl_Genba);
            this.panel3.Controls.Add(this.lbl_Gyousha);
            this.panel3.Location = new System.Drawing.Point(575, 389);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(420, 89);
            this.panel3.TabIndex = 691;
            // 
            // cantxt_GenbaCd
            // 
            this.cantxt_GenbaCd.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_GenbaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_GenbaCd.ChangeUpperCase = true;
            this.cantxt_GenbaCd.CharacterLimitList = null;
            this.cantxt_GenbaCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_GenbaCd.DBFieldsName = "";
            this.cantxt_GenbaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_GenbaCd.DisplayItemName = "現場CD";
            this.cantxt_GenbaCd.DisplayPopUp = null;
            this.cantxt_GenbaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_GenbaCd.FocusOutCheckMethod")));
            this.cantxt_GenbaCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_GenbaCd.GetCodeMasterField = "";
            this.cantxt_GenbaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_GenbaCd.IsInputErrorOccured = false;
            this.cantxt_GenbaCd.Location = new System.Drawing.Point(72, 50);
            this.cantxt_GenbaCd.MaxLength = 6;
            this.cantxt_GenbaCd.Name = "cantxt_GenbaCd";
            this.cantxt_GenbaCd.PopupAfterExecute = null;
            this.cantxt_GenbaCd.PopupBeforeExecute = null;
            this.cantxt_GenbaCd.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GENBA_CD,GENBA_NAME_RYAKU";
            this.cantxt_GenbaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_GenbaCd.PopupSearchSendParams")));
            this.cantxt_GenbaCd.PopupSendParams = new string[0];
            this.cantxt_GenbaCd.PopupSetFormField = "cantxt_GyoushaCd,ctxt_GyoushaName,cantxt_GenbaCd,ctxt_GenbaName";
            this.cantxt_GenbaCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cantxt_GenbaCd.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cantxt_GenbaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_GenbaCd.popupWindowSetting")));
            this.cantxt_GenbaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_GenbaCd.RegistCheckMethod")));
            this.cantxt_GenbaCd.SetFormField = "";
            this.cantxt_GenbaCd.ShortItemName = "現場CD";
            this.cantxt_GenbaCd.Size = new System.Drawing.Size(58, 20);
            this.cantxt_GenbaCd.TabIndex = 25;
            this.cantxt_GenbaCd.Tag = "";
            this.cantxt_GenbaCd.ZeroPaddengFlag = true;
            this.cantxt_GenbaCd.BackColorChanged += new System.EventHandler(this.cantxt_GenbaCd_BackColorChanged);
            this.cantxt_GenbaCd.Enter += new System.EventHandler(this.cantxt_GenbaCd_Enter);
            this.cantxt_GenbaCd.Validated += new System.EventHandler(this.cantxt_GenbaCd_Validated);
            // 
            // cantxt_GyoushaCd
            // 
            this.cantxt_GyoushaCd.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_GyoushaCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_GyoushaCd.ChangeUpperCase = true;
            this.cantxt_GyoushaCd.CharacterLimitList = null;
            this.cantxt_GyoushaCd.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_GyoushaCd.DBFieldsName = "";
            this.cantxt_GyoushaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_GyoushaCd.DisplayItemName = "業者CD";
            this.cantxt_GyoushaCd.DisplayPopUp = null;
            this.cantxt_GyoushaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_GyoushaCd.FocusOutCheckMethod")));
            this.cantxt_GyoushaCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_GyoushaCd.GetCodeMasterField = "";
            this.cantxt_GyoushaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_GyoushaCd.IsInputErrorOccured = false;
            this.cantxt_GyoushaCd.Location = new System.Drawing.Point(72, 22);
            this.cantxt_GyoushaCd.MaxLength = 6;
            this.cantxt_GyoushaCd.Name = "cantxt_GyoushaCd";
            this.cantxt_GyoushaCd.PopupAfterExecute = null;
            this.cantxt_GyoushaCd.PopupAfterExecuteMethod = "Gyousha_PopupAfterExecuteMethod";
            this.cantxt_GyoushaCd.PopupBeforeExecute = null;
            this.cantxt_GyoushaCd.PopupBeforeExecuteMethod = "Gyousha_PopupBeforeExecuteMethod";
            this.cantxt_GyoushaCd.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.cantxt_GyoushaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_GyoushaCd.PopupSearchSendParams")));
            this.cantxt_GyoushaCd.PopupSetFormField = "cantxt_GyoushaCd,ctxt_GyoushaName";
            this.cantxt_GyoushaCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cantxt_GyoushaCd.PopupWindowName = "検索共通ポップアップ";
            this.cantxt_GyoushaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_GyoushaCd.popupWindowSetting")));
            this.cantxt_GyoushaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_GyoushaCd.RegistCheckMethod")));
            this.cantxt_GyoushaCd.SetFormField = "";
            this.cantxt_GyoushaCd.ShortItemName = "業者CD";
            this.cantxt_GyoushaCd.Size = new System.Drawing.Size(58, 20);
            this.cantxt_GyoushaCd.TabIndex = 24;
            this.cantxt_GyoushaCd.Tag = "";
            this.cantxt_GyoushaCd.ZeroPaddengFlag = true;
            this.cantxt_GyoushaCd.BackColorChanged += new System.EventHandler(this.cantxt_GyoushaCd_BackColorChanged);
            this.cantxt_GyoushaCd.Enter += new System.EventHandler(this.cantxt_GyoushaCd_Enter);
            this.cantxt_GyoushaCd.Validated += new System.EventHandler(this.cantxt_GyoushaCd_Validated);
            // 
            // ctxt_GenbaName
            // 
            this.ctxt_GenbaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_GenbaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_GenbaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_GenbaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_GenbaName.DisplayPopUp = null;
            this.ctxt_GenbaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_GenbaName.FocusOutCheckMethod")));
            this.ctxt_GenbaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_GenbaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_GenbaName.IsInputErrorOccured = false;
            this.ctxt_GenbaName.Location = new System.Drawing.Point(129, 50);
            this.ctxt_GenbaName.MaxLength = 0;
            this.ctxt_GenbaName.Name = "ctxt_GenbaName";
            this.ctxt_GenbaName.PopupAfterExecute = null;
            this.ctxt_GenbaName.PopupBeforeExecute = null;
            this.ctxt_GenbaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_GenbaName.PopupSearchSendParams")));
            this.ctxt_GenbaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_GenbaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_GenbaName.popupWindowSetting")));
            this.ctxt_GenbaName.ReadOnly = true;
            this.ctxt_GenbaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_GenbaName.RegistCheckMethod")));
            this.ctxt_GenbaName.Size = new System.Drawing.Size(283, 20);
            this.ctxt_GenbaName.TabIndex = 21;
            this.ctxt_GenbaName.TabStop = false;
            this.ctxt_GenbaName.Tag = "";
            // 
            // ctxt_GyoushaName
            // 
            this.ctxt_GyoushaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_GyoushaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_GyoushaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_GyoushaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_GyoushaName.DisplayPopUp = null;
            this.ctxt_GyoushaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_GyoushaName.FocusOutCheckMethod")));
            this.ctxt_GyoushaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_GyoushaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_GyoushaName.IsInputErrorOccured = false;
            this.ctxt_GyoushaName.Location = new System.Drawing.Point(129, 22);
            this.ctxt_GyoushaName.MaxLength = 0;
            this.ctxt_GyoushaName.Name = "ctxt_GyoushaName";
            this.ctxt_GyoushaName.PopupAfterExecute = null;
            this.ctxt_GyoushaName.PopupBeforeExecute = null;
            this.ctxt_GyoushaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_GyoushaName.PopupSearchSendParams")));
            this.ctxt_GyoushaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_GyoushaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_GyoushaName.popupWindowSetting")));
            this.ctxt_GyoushaName.ReadOnly = true;
            this.ctxt_GyoushaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_GyoushaName.RegistCheckMethod")));
            this.ctxt_GyoushaName.Size = new System.Drawing.Size(283, 20);
            this.ctxt_GyoushaName.TabIndex = 18;
            this.ctxt_GyoushaName.TabStop = false;
            this.ctxt_GyoushaName.Tag = "";
            // 
            // lbl_Genba
            // 
            this.lbl_Genba.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Genba.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Genba.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Genba.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Genba.ForeColor = System.Drawing.Color.White;
            this.lbl_Genba.Location = new System.Drawing.Point(8, 50);
            this.lbl_Genba.Name = "lbl_Genba";
            this.lbl_Genba.Size = new System.Drawing.Size(58, 20);
            this.lbl_Genba.TabIndex = 19;
            this.lbl_Genba.Text = "現場";
            this.lbl_Genba.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Gyousha
            // 
            this.lbl_Gyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Gyousha.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Gyousha.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Gyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Gyousha.ForeColor = System.Drawing.Color.White;
            this.lbl_Gyousha.Location = new System.Drawing.Point(8, 22);
            this.lbl_Gyousha.Name = "lbl_Gyousha";
            this.lbl_Gyousha.Size = new System.Drawing.Size(58, 20);
            this.lbl_Gyousha.TabIndex = 16;
            this.lbl_Gyousha.Text = "業者";
            this.lbl_Gyousha.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctxt_Sentakusaki
            // 
            this.ctxt_Sentakusaki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ctxt_Sentakusaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_Sentakusaki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_Sentakusaki.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_Sentakusaki.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_Sentakusaki.DisplayPopUp = null;
            this.ctxt_Sentakusaki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_Sentakusaki.FocusOutCheckMethod")));
            this.ctxt_Sentakusaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_Sentakusaki.ForeColor = System.Drawing.Color.Black;
            this.ctxt_Sentakusaki.IsInputErrorOccured = false;
            this.ctxt_Sentakusaki.Location = new System.Drawing.Point(115, 349);
            this.ctxt_Sentakusaki.MaxLength = 0;
            this.ctxt_Sentakusaki.Name = "ctxt_Sentakusaki";
            this.ctxt_Sentakusaki.PopupAfterExecute = null;
            this.ctxt_Sentakusaki.PopupBeforeExecute = null;
            this.ctxt_Sentakusaki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_Sentakusaki.PopupSearchSendParams")));
            this.ctxt_Sentakusaki.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_Sentakusaki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_Sentakusaki.popupWindowSetting")));
            this.ctxt_Sentakusaki.ReadOnly = true;
            this.ctxt_Sentakusaki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_Sentakusaki.RegistCheckMethod")));
            this.ctxt_Sentakusaki.Size = new System.Drawing.Size(338, 20);
            this.ctxt_Sentakusaki.TabIndex = 7;
            this.ctxt_Sentakusaki.TabStop = false;
            this.ctxt_Sentakusaki.Tag = "";
            // 
            // lbl_SentakuSaki
            // 
            this.lbl_SentakuSaki.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lbl_SentakuSaki.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_SentakuSaki.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_SentakuSaki.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_SentakuSaki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_SentakuSaki.ForeColor = System.Drawing.Color.White;
            this.lbl_SentakuSaki.Location = new System.Drawing.Point(0, 349);
            this.lbl_SentakuSaki.Name = "lbl_SentakuSaki";
            this.lbl_SentakuSaki.Size = new System.Drawing.Size(110, 20);
            this.lbl_SentakuSaki.TabIndex = 6;
            this.lbl_SentakuSaki.Text = "選択先";
            this.lbl_SentakuSaki.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(280, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "～";
            // 
            // cdate_HikiwatasiBiFrom
            // 
            this.cdate_HikiwatasiBiFrom.BackColor = System.Drawing.SystemColors.Window;
            this.cdate_HikiwatasiBiFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cdate_HikiwatasiBiFrom.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.cdate_HikiwatasiBiFrom.Checked = false;
            this.cdate_HikiwatasiBiFrom.CustomFormat = "yyyy/MM/dd(ddd)";
            this.cdate_HikiwatasiBiFrom.DateTimeNowYear = "";
            this.cdate_HikiwatasiBiFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.cdate_HikiwatasiBiFrom.DisplayItemName = "開始日付";
            this.cdate_HikiwatasiBiFrom.DisplayPopUp = null;
            this.cdate_HikiwatasiBiFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cdate_HikiwatasiBiFrom.FocusOutCheckMethod")));
            this.cdate_HikiwatasiBiFrom.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cdate_HikiwatasiBiFrom.ForeColor = System.Drawing.Color.Black;
            this.cdate_HikiwatasiBiFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdate_HikiwatasiBiFrom.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cdate_HikiwatasiBiFrom.IsInputErrorOccured = false;
            this.cdate_HikiwatasiBiFrom.Location = new System.Drawing.Point(136, 0);
            this.cdate_HikiwatasiBiFrom.MaxLength = 10;
            this.cdate_HikiwatasiBiFrom.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.cdate_HikiwatasiBiFrom.Name = "cdate_HikiwatasiBiFrom";
            this.cdate_HikiwatasiBiFrom.NullValue = "";
            this.cdate_HikiwatasiBiFrom.PopupAfterExecute = null;
            this.cdate_HikiwatasiBiFrom.PopupBeforeExecute = null;
            this.cdate_HikiwatasiBiFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cdate_HikiwatasiBiFrom.PopupSearchSendParams")));
            this.cdate_HikiwatasiBiFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cdate_HikiwatasiBiFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cdate_HikiwatasiBiFrom.popupWindowSetting")));
            this.cdate_HikiwatasiBiFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cdate_HikiwatasiBiFrom.RegistCheckMethod")));
            this.cdate_HikiwatasiBiFrom.ShortItemName = "開始日付";
            this.cdate_HikiwatasiBiFrom.Size = new System.Drawing.Size(138, 20);
            this.cdate_HikiwatasiBiFrom.TabIndex = 1;
            this.cdate_HikiwatasiBiFrom.Tag = "日付を選択してください";
            this.cdate_HikiwatasiBiFrom.Text = "2013/12/10(火)";
            this.cdate_HikiwatasiBiFrom.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
            this.cdate_HikiwatasiBiFrom.Leave += new System.EventHandler(this.cdate_HikiwatasiBiFrom_Leave);
            // 
            // cdate_HikiwatasiBiTo
            // 
            this.cdate_HikiwatasiBiTo.BackColor = System.Drawing.SystemColors.Window;
            this.cdate_HikiwatasiBiTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cdate_HikiwatasiBiTo.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.cdate_HikiwatasiBiTo.Checked = false;
            this.cdate_HikiwatasiBiTo.CustomFormat = "yyyy/MM/dd(ddd)";
            this.cdate_HikiwatasiBiTo.DateTimeNowYear = "";
            this.cdate_HikiwatasiBiTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.cdate_HikiwatasiBiTo.DisplayItemName = "終了日付";
            this.cdate_HikiwatasiBiTo.DisplayPopUp = null;
            this.cdate_HikiwatasiBiTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cdate_HikiwatasiBiTo.FocusOutCheckMethod")));
            this.cdate_HikiwatasiBiTo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cdate_HikiwatasiBiTo.ForeColor = System.Drawing.Color.Black;
            this.cdate_HikiwatasiBiTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cdate_HikiwatasiBiTo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cdate_HikiwatasiBiTo.IsInputErrorOccured = false;
            this.cdate_HikiwatasiBiTo.Location = new System.Drawing.Point(307, 0);
            this.cdate_HikiwatasiBiTo.MaxLength = 10;
            this.cdate_HikiwatasiBiTo.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.cdate_HikiwatasiBiTo.Name = "cdate_HikiwatasiBiTo";
            this.cdate_HikiwatasiBiTo.NullValue = "";
            this.cdate_HikiwatasiBiTo.PopupAfterExecute = null;
            this.cdate_HikiwatasiBiTo.PopupBeforeExecute = null;
            this.cdate_HikiwatasiBiTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cdate_HikiwatasiBiTo.PopupSearchSendParams")));
            this.cdate_HikiwatasiBiTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cdate_HikiwatasiBiTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cdate_HikiwatasiBiTo.popupWindowSetting")));
            this.cdate_HikiwatasiBiTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cdate_HikiwatasiBiTo.RegistCheckMethod")));
            this.cdate_HikiwatasiBiTo.ShortItemName = "終了日付";
            this.cdate_HikiwatasiBiTo.Size = new System.Drawing.Size(138, 20);
            this.cdate_HikiwatasiBiTo.TabIndex = 3;
            this.cdate_HikiwatasiBiTo.Tag = "日付を選択してください";
            this.cdate_HikiwatasiBiTo.Text = "2013/12/10(火)";
            this.cdate_HikiwatasiBiTo.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
            this.cdate_HikiwatasiBiTo.Leave += new System.EventHandler(this.cdate_HikiwatasiBiTo_Leave);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(0, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 20);
            this.label2.TabIndex = 697;
            this.label2.Text = "マニフェスト番号";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cntxt_ManifestIdFrom
            // 
            this.cntxt_ManifestIdFrom.BackColor = System.Drawing.SystemColors.Window;
            this.cntxt_ManifestIdFrom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cntxt_ManifestIdFrom.CustomFormatSetting = "00000000000";
            this.cntxt_ManifestIdFrom.DefaultBackColor = System.Drawing.Color.Empty;
            this.cntxt_ManifestIdFrom.DisplayItemName = "マニフェスト番号From";
            this.cntxt_ManifestIdFrom.DisplayPopUp = null;
            this.cntxt_ManifestIdFrom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cntxt_ManifestIdFrom.FocusOutCheckMethod")));
            this.cntxt_ManifestIdFrom.ForeColor = System.Drawing.Color.Black;
            this.cntxt_ManifestIdFrom.FormatSetting = "カスタム";
            this.cntxt_ManifestIdFrom.IsInputErrorOccured = false;
            this.cntxt_ManifestIdFrom.ItemDefinedTypes = "varchar";
            this.cntxt_ManifestIdFrom.Location = new System.Drawing.Point(136, 25);
            this.cntxt_ManifestIdFrom.Name = "cntxt_ManifestIdFrom";
            this.cntxt_ManifestIdFrom.PopupAfterExecute = null;
            this.cntxt_ManifestIdFrom.PopupBeforeExecute = null;
            this.cntxt_ManifestIdFrom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cntxt_ManifestIdFrom.PopupSearchSendParams")));
            this.cntxt_ManifestIdFrom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cntxt_ManifestIdFrom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cntxt_ManifestIdFrom.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.cntxt_ManifestIdFrom.RangeSetting = rangeSettingDto3;
            this.cntxt_ManifestIdFrom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cntxt_ManifestIdFrom.RegistCheckMethod")));
            this.cntxt_ManifestIdFrom.Size = new System.Drawing.Size(138, 20);
            this.cntxt_ManifestIdFrom.TabIndex = 4;
            this.cntxt_ManifestIdFrom.Tag = "半角11桁以内で入力してください";
            this.cntxt_ManifestIdFrom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cntxt_ManifestIdFrom.WordWrap = false;
            // 
            // cntxt_ManifestIdTo
            // 
            this.cntxt_ManifestIdTo.BackColor = System.Drawing.SystemColors.Window;
            this.cntxt_ManifestIdTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cntxt_ManifestIdTo.CustomFormatSetting = "00000000000";
            this.cntxt_ManifestIdTo.DefaultBackColor = System.Drawing.Color.Empty;
            this.cntxt_ManifestIdTo.DisplayItemName = "マニフェスト番号To";
            this.cntxt_ManifestIdTo.DisplayPopUp = null;
            this.cntxt_ManifestIdTo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cntxt_ManifestIdTo.FocusOutCheckMethod")));
            this.cntxt_ManifestIdTo.ForeColor = System.Drawing.Color.Black;
            this.cntxt_ManifestIdTo.FormatSetting = "カスタム";
            this.cntxt_ManifestIdTo.IsInputErrorOccured = false;
            this.cntxt_ManifestIdTo.ItemDefinedTypes = "varchar";
            this.cntxt_ManifestIdTo.Location = new System.Drawing.Point(307, 25);
            this.cntxt_ManifestIdTo.Name = "cntxt_ManifestIdTo";
            this.cntxt_ManifestIdTo.PopupAfterExecute = null;
            this.cntxt_ManifestIdTo.PopupBeforeExecute = null;
            this.cntxt_ManifestIdTo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cntxt_ManifestIdTo.PopupSearchSendParams")));
            this.cntxt_ManifestIdTo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cntxt_ManifestIdTo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cntxt_ManifestIdTo.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            1215752191,
            23,
            0,
            0});
            this.cntxt_ManifestIdTo.RangeSetting = rangeSettingDto1;
            this.cntxt_ManifestIdTo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cntxt_ManifestIdTo.RegistCheckMethod")));
            this.cntxt_ManifestIdTo.Size = new System.Drawing.Size(138, 20);
            this.cntxt_ManifestIdTo.TabIndex = 6;
            this.cntxt_ManifestIdTo.Tag = "半角11桁以内で入力してください";
            this.cntxt_ManifestIdTo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cntxt_ManifestIdTo.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(280, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "～";
            // 
            // Ichiran
            // 
            this.Ichiran.AllowUserToAddRows = false;
            this.Ichiran.AllowUserToDeleteRows = false;
            cellStyle2.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
            this.Ichiran.ColumnHeadersDefaultHeaderCellStyle = cellStyle2;
            this.Ichiran.CurrentRowBorderLine = new GrapeCity.Win.MultiRow.Line(GrapeCity.Win.MultiRow.LineStyle.Medium, System.Drawing.Color.Red);
            this.Ichiran.EditMode = GrapeCity.Win.MultiRow.EditMode.EditOnEnter;
            this.Ichiran.Location = new System.Drawing.Point(0, 60);
            this.Ichiran.MultiSelect = false;
            this.Ichiran.Name = "Ichiran";
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftLeft)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftRight)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstCellInRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastCellInRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastCellInRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.DefaultModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollUp)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollDown)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollLeft)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.ScrollRight)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToPreviousPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Space)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.VerticalScrollToNextPage)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToFirstPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToLastPage)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.DisplayModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ReverseSelectCurrentRow)), System.Windows.Forms.Keys.Space));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToPreviousPage)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.ScrollActions.HorizontalScrollToNextPage)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.ListBoxModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousCell)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextCell)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Tab)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), System.Windows.Forms.Keys.Home));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), System.Windows.Forms.Keys.End));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Up));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousRow)), System.Windows.Forms.Keys.Left));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Down));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextRow)), System.Windows.Forms.Keys.Right));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToPreviousPage)), System.Windows.Forms.Keys.PageUp));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.MoveToNextPage)), System.Windows.Forms.Keys.Next));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Home)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToFirstRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.End)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToLastRow)), ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                                | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Up)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToPreviousRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Left)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftToNextRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Right)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageUp)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.PageUp)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.ShiftPageDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Next)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.SelectionActions.SelectAll)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.BeginEdit)), System.Windows.Forms.Keys.F2));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.CommitRow)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Cut)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Copy)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Paste)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Insert)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.Clear)), System.Windows.Forms.Keys.Delete));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.DeleteSelectedRows)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D0)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.InputNullValue)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.NumPad0)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), System.Windows.Forms.Keys.F4));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(((GrapeCity.Win.MultiRow.Action)(GrapeCity.Win.MultiRow.EditingActions.ShowDropDown)), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Down)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Return));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToNextContorol(), System.Windows.Forms.Keys.Tab));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Return)))));
            shortcutKeyManager2.RowModeList.Add(new GrapeCity.Win.MultiRow.ShortcutKey(new r_framework.CustomControl.GcCustomMultiRow.GcCustomMoveToPreviousContorol(), ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Tab)))));
            this.Ichiran.ShortcutKeyManager = shortcutKeyManager2;
            this.Ichiran.Size = new System.Drawing.Size(1000, 283);
            this.Ichiran.SplitMode = GrapeCity.Win.MultiRow.SplitMode.None;
            this.Ichiran.TabIndex = 7;
            this.Ichiran.Template = this.uiDetail1;
            this.Ichiran.TemplateScaleSize = new System.Drawing.SizeF(1.166667F, 1.083333F);
            this.Ichiran.CellEnter += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.Ichiran_CellEnter);
            this.Ichiran.CellLeave += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.Ichiran_CellLeave);
            this.Ichiran.CellContentClick += new System.EventHandler<GrapeCity.Win.MultiRow.CellEventArgs>(this.Ichiran_CellContentClick);
            this.Ichiran.CellEditedFormattedValueChanged += new System.EventHandler<GrapeCity.Win.MultiRow.CellEditedFormattedValueChangedEventArgs>(this.Ichiran_CellEditedFormattedValueChanged);
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.cntxt_ManifestIdTo);
            this.Controls.Add(this.cntxt_ManifestIdFrom);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cdate_HikiwatasiBiTo);
            this.Controls.Add(this.cdate_HikiwatasiBiFrom);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lbl_ShogunMaster);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Ichiran);
            this.Controls.Add(this.lbl_MasterInfo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.ctxt_Sentakusaki);
            this.Controls.Add(this.lbl_SentakuSaki);
            this.Controls.Add(this.lbl_HikiwatasiBi);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.WindowId = r_framework.Const.WINDOW_ID.T_MIHIMODUKE_ICHIRAN;
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Ichiran)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		public System.Windows.Forms.Label lbl_HikiwatasiBi;
		public System.Windows.Forms.Label lbl_MasterInfo;
		public System.Windows.Forms.Panel panel2;
        public r_framework.CustomControl.CustomTextBox ctxt_JigyoujouName;
        public r_framework.CustomControl.CustomTextBox ctxt_JigyoushaName;
		public System.Windows.Forms.Label lbl_Jigyoujou;
		public System.Windows.Forms.Label lbl_Jigyousha;
		public System.Windows.Forms.Label lbl_ShogunMaster;
		public System.Windows.Forms.Panel panel3;
        public r_framework.CustomControl.CustomTextBox ctxt_GenbaName;
        public r_framework.CustomControl.CustomTextBox ctxt_GyoushaName;
		public System.Windows.Forms.Label lbl_Genba;
		public System.Windows.Forms.Label lbl_Gyousha;
        public r_framework.CustomControl.CustomTextBox ctxt_Sentakusaki;
        public System.Windows.Forms.Label lbl_SentakuSaki;
        public r_framework.CustomControl.CustomAlphaNumTextBox cantxt_JigyoujouCd;
        public r_framework.CustomControl.CustomAlphaNumTextBox cantxt_JigyoushaCd;
        public r_framework.CustomControl.CustomAlphaNumTextBox cantxt_GenbaCd;
        public r_framework.CustomControl.CustomAlphaNumTextBox cantxt_GyoushaCd;
        internal r_framework.CustomControl.GcCustomMultiRow Ichiran;
		private MultiRowTemplate.UIDetail uiDetail1;
        public System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomDateTimePicker cdate_HikiwatasiBiFrom;
        public r_framework.CustomControl.CustomDateTimePicker cdate_HikiwatasiBiTo;
        public System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomNumericTextBox2 cntxt_ManifestIdFrom;
        internal r_framework.CustomControl.CustomNumericTextBox2 cntxt_ManifestIdTo;
        public System.Windows.Forms.Label label3;
	}
}