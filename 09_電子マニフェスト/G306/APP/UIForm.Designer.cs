namespace Shougun.Core.ElectronicManifest.UnpanShuryouHoukokuIkkatuNyuuryoku
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
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            this.lb_title = new System.Windows.Forms.Label();
            this.lbl_InsatsuBusu = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.cDt_UnnpannSyuryouhi = new r_framework.CustomControl.CustomDateTimePicker();
            this.ctxt_UnnpannTanntousyaName = new r_framework.CustomControl.CustomTextBox();
            this.ctxt_HoukokuTanntousyaName = new r_framework.CustomControl.CustomTextBox();
            this.cantxt_Unnpannryou = new r_framework.CustomControl.CustomNumericTextBox2();
            this.cantxt_YukabutuJyuusyuuryou = new r_framework.CustomControl.CustomNumericTextBox2();
            this.cantxt_UnnpannryouTanniCD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ctxt_UnnpannryouTanniName = new r_framework.CustomControl.CustomTextBox();
            this.cantxt_SyaryouCD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ctxt_SyaryouName = new r_framework.CustomControl.CustomTextBox();
            this.cantxt_YukabutuJyuusyuuryouTanniCD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ctxt_YukabutuJyuusyuuryouTanniName = new r_framework.CustomControl.CustomTextBox();
            this.ccb_hikiwatasiHi = new r_framework.CustomControl.CustomCheckBox();
            this.ccb_UnnpannTanntousya = new r_framework.CustomControl.CustomCheckBox();
            this.ccb_hikiwatasiRyou = new r_framework.CustomControl.CustomCheckBox();
            this.ccb_SyaryouCD = new r_framework.CustomControl.CustomCheckBox();
            this.ctxt_bikou = new r_framework.CustomControl.CustomTextBox();
            this.bt_allSelect = new r_framework.CustomControl.CustomButton();
            this.bt_Input = new r_framework.CustomControl.CustomButton();
            this.bt_Erase = new r_framework.CustomControl.CustomButton();
            this.bt_Close = new r_framework.CustomControl.CustomButton();
            this.cantxt_hideEdiId = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.cantxt_hideDenshiUseKbn = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.cantxt_HoukokuTanntousyaCD = new Shougun.Core.ElectronicManifest.CustomControls_Ex.CustomForMasterKyoutuuPopup_Ex();
            this.cantxt_UnnpannTanntousyaCD = new Shougun.Core.ElectronicManifest.CustomControls_Ex.CustomForMasterKyoutuuPopup_Ex();
            this.cantxt_hideGyoushaCd = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold);
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(6, 10);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(567, 37);
            this.lb_title.TabIndex = 1;
            this.lb_title.Text = "運搬終了報告情報　　一括入力";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_InsatsuBusu
            // 
            this.lbl_InsatsuBusu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_InsatsuBusu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_InsatsuBusu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_InsatsuBusu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lbl_InsatsuBusu.ForeColor = System.Drawing.Color.White;
            this.lbl_InsatsuBusu.Location = new System.Drawing.Point(6, 70);
            this.lbl_InsatsuBusu.Name = "lbl_InsatsuBusu";
            this.lbl_InsatsuBusu.Size = new System.Drawing.Size(140, 20);
            this.lbl_InsatsuBusu.TabIndex = 2;
            this.lbl_InsatsuBusu.Text = "運搬終了日";
            this.lbl_InsatsuBusu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(6, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(140, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "運搬担当者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(6, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "報告担当者";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(6, 136);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(140, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "運搬量";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(6, 158);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 20);
            this.label4.TabIndex = 6;
            this.label4.Text = "単位";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(6, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(140, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "有価物拾集量";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(6, 202);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(140, 20);
            this.label6.TabIndex = 8;
            this.label6.Text = "単位";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(6, 224);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(140, 20);
            this.label7.TabIndex = 9;
            this.label7.Text = "車輌番号";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(6, 246);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(140, 20);
            this.label8.TabIndex = 10;
            this.label8.Tag = "";
            this.label8.Text = "備考";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cDt_UnnpannSyuryouhi
            // 
            this.cDt_UnnpannSyuryouhi.BackColor = System.Drawing.SystemColors.Window;
            this.cDt_UnnpannSyuryouhi.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cDt_UnnpannSyuryouhi.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.cDt_UnnpannSyuryouhi.Checked = false;
            this.cDt_UnnpannSyuryouhi.CustomFormat = "yyyy/MM/dd(ddd)";
            this.cDt_UnnpannSyuryouhi.DateTimeNowYear = "";
            this.cDt_UnnpannSyuryouhi.DefaultBackColor = System.Drawing.Color.Empty;
            this.cDt_UnnpannSyuryouhi.DisplayItemName = "";
            this.cDt_UnnpannSyuryouhi.DisplayPopUp = null;
            this.cDt_UnnpannSyuryouhi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cDt_UnnpannSyuryouhi.FocusOutCheckMethod")));
            this.cDt_UnnpannSyuryouhi.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cDt_UnnpannSyuryouhi.ForeColor = System.Drawing.Color.Black;
            this.cDt_UnnpannSyuryouhi.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.cDt_UnnpannSyuryouhi.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cDt_UnnpannSyuryouhi.IsInputErrorOccured = false;
            this.cDt_UnnpannSyuryouhi.Location = new System.Drawing.Point(153, 70);
            this.cDt_UnnpannSyuryouhi.MaxLength = 10;
            this.cDt_UnnpannSyuryouhi.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.cDt_UnnpannSyuryouhi.Name = "cDt_UnnpannSyuryouhi";
            this.cDt_UnnpannSyuryouhi.NullValue = "";
            this.cDt_UnnpannSyuryouhi.PopupAfterExecute = null;
            this.cDt_UnnpannSyuryouhi.PopupBeforeExecute = null;
            this.cDt_UnnpannSyuryouhi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cDt_UnnpannSyuryouhi.PopupSearchSendParams")));
            this.cDt_UnnpannSyuryouhi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cDt_UnnpannSyuryouhi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cDt_UnnpannSyuryouhi.popupWindowSetting")));
            this.cDt_UnnpannSyuryouhi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cDt_UnnpannSyuryouhi.RegistCheckMethod")));
            this.cDt_UnnpannSyuryouhi.Size = new System.Drawing.Size(138, 20);
            this.cDt_UnnpannSyuryouhi.TabIndex = 1;
            this.cDt_UnnpannSyuryouhi.Tag = "日付を選択してください";
            this.cDt_UnnpannSyuryouhi.Text = "2013/12/10(火)";
            this.cDt_UnnpannSyuryouhi.Value = new System.DateTime(2013, 12, 10, 0, 0, 0, 0);
            // 
            // ctxt_UnnpannTanntousyaName
            // 
            this.ctxt_UnnpannTanntousyaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_UnnpannTanntousyaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_UnnpannTanntousyaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_UnnpannTanntousyaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_UnnpannTanntousyaName.DisplayPopUp = null;
            this.ctxt_UnnpannTanntousyaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_UnnpannTanntousyaName.FocusOutCheckMethod")));
            this.ctxt_UnnpannTanntousyaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_UnnpannTanntousyaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_UnnpannTanntousyaName.IsInputErrorOccured = false;
            this.ctxt_UnnpannTanntousyaName.Location = new System.Drawing.Point(245, 92);
            this.ctxt_UnnpannTanntousyaName.MaxLength = 0;
            this.ctxt_UnnpannTanntousyaName.Name = "ctxt_UnnpannTanntousyaName";
            this.ctxt_UnnpannTanntousyaName.PopupAfterExecute = null;
            this.ctxt_UnnpannTanntousyaName.PopupBeforeExecute = null;
            this.ctxt_UnnpannTanntousyaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_UnnpannTanntousyaName.PopupSearchSendParams")));
            this.ctxt_UnnpannTanntousyaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_UnnpannTanntousyaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_UnnpannTanntousyaName.popupWindowSetting")));
            this.ctxt_UnnpannTanntousyaName.ReadOnly = true;
            this.ctxt_UnnpannTanntousyaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_UnnpannTanntousyaName.RegistCheckMethod")));
            this.ctxt_UnnpannTanntousyaName.Size = new System.Drawing.Size(192, 20);
            this.ctxt_UnnpannTanntousyaName.TabIndex = 618;
            this.ctxt_UnnpannTanntousyaName.TabStop = false;
            this.ctxt_UnnpannTanntousyaName.Tag = " ";
            // 
            // ctxt_HoukokuTanntousyaName
            // 
            this.ctxt_HoukokuTanntousyaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_HoukokuTanntousyaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_HoukokuTanntousyaName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_HoukokuTanntousyaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_HoukokuTanntousyaName.DisplayPopUp = null;
            this.ctxt_HoukokuTanntousyaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HoukokuTanntousyaName.FocusOutCheckMethod")));
            this.ctxt_HoukokuTanntousyaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_HoukokuTanntousyaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_HoukokuTanntousyaName.IsInputErrorOccured = false;
            this.ctxt_HoukokuTanntousyaName.Location = new System.Drawing.Point(245, 114);
            this.ctxt_HoukokuTanntousyaName.MaxLength = 0;
            this.ctxt_HoukokuTanntousyaName.Name = "ctxt_HoukokuTanntousyaName";
            this.ctxt_HoukokuTanntousyaName.PopupAfterExecute = null;
            this.ctxt_HoukokuTanntousyaName.PopupBeforeExecute = null;
            this.ctxt_HoukokuTanntousyaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_HoukokuTanntousyaName.PopupSearchSendParams")));
            this.ctxt_HoukokuTanntousyaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_HoukokuTanntousyaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_HoukokuTanntousyaName.popupWindowSetting")));
            this.ctxt_HoukokuTanntousyaName.ReadOnly = true;
            this.ctxt_HoukokuTanntousyaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HoukokuTanntousyaName.RegistCheckMethod")));
            this.ctxt_HoukokuTanntousyaName.Size = new System.Drawing.Size(192, 20);
            this.ctxt_HoukokuTanntousyaName.TabIndex = 620;
            this.ctxt_HoukokuTanntousyaName.TabStop = false;
            this.ctxt_HoukokuTanntousyaName.Tag = " ";
            // 
            // cantxt_Unnpannryou
            // 
            this.cantxt_Unnpannryou.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_Unnpannryou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_Unnpannryou.CustomFormatSetting = "#,##0.000";
            this.cantxt_Unnpannryou.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_Unnpannryou.DisplayItemName = "";
            this.cantxt_Unnpannryou.DisplayPopUp = null;
            this.cantxt_Unnpannryou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_Unnpannryou.FocusOutCheckMethod")));
            this.cantxt_Unnpannryou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_Unnpannryou.ForeColor = System.Drawing.Color.Black;
            this.cantxt_Unnpannryou.FormatSetting = "カスタム";
            this.cantxt_Unnpannryou.IsInputErrorOccured = false;
            this.cantxt_Unnpannryou.Location = new System.Drawing.Point(153, 136);
            this.cantxt_Unnpannryou.Name = "cantxt_Unnpannryou";
            this.cantxt_Unnpannryou.PopupAfterExecute = null;
            this.cantxt_Unnpannryou.PopupBeforeExecute = null;
            this.cantxt_Unnpannryou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_Unnpannryou.PopupSearchSendParams")));
            this.cantxt_Unnpannryou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_Unnpannryou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_Unnpannryou.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99999999,
            0,
            0,
            196608});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.cantxt_Unnpannryou.RangeSetting = rangeSettingDto1;
            this.cantxt_Unnpannryou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_Unnpannryou.RegistCheckMethod")));
            this.cantxt_Unnpannryou.Size = new System.Drawing.Size(81, 20);
            this.cantxt_Unnpannryou.TabIndex = 6;
            this.cantxt_Unnpannryou.Tag = "";
            this.cantxt_Unnpannryou.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cantxt_Unnpannryou.WordWrap = false;
            this.cantxt_Unnpannryou.Leave += new System.EventHandler(this.cantxt_Unnpannryou_Leave);
            // 
            // cantxt_YukabutuJyuusyuuryou
            // 
            this.cantxt_YukabutuJyuusyuuryou.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_YukabutuJyuusyuuryou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_YukabutuJyuusyuuryou.CustomFormatSetting = "#,##0.000";
            this.cantxt_YukabutuJyuusyuuryou.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_YukabutuJyuusyuuryou.DisplayItemName = "マニフェスト番号From";
            this.cantxt_YukabutuJyuusyuuryou.DisplayPopUp = null;
            this.cantxt_YukabutuJyuusyuuryou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_YukabutuJyuusyuuryou.FocusOutCheckMethod")));
            this.cantxt_YukabutuJyuusyuuryou.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_YukabutuJyuusyuuryou.ForeColor = System.Drawing.Color.Black;
            this.cantxt_YukabutuJyuusyuuryou.FormatSetting = "カスタム";
            this.cantxt_YukabutuJyuusyuuryou.IsInputErrorOccured = false;
            this.cantxt_YukabutuJyuusyuuryou.Location = new System.Drawing.Point(153, 180);
            this.cantxt_YukabutuJyuusyuuryou.Name = "cantxt_YukabutuJyuusyuuryou";
            this.cantxt_YukabutuJyuusyuuryou.PopupAfterExecute = null;
            this.cantxt_YukabutuJyuusyuuryou.PopupBeforeExecute = null;
            this.cantxt_YukabutuJyuusyuuryou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_YukabutuJyuusyuuryou.PopupSearchSendParams")));
            this.cantxt_YukabutuJyuusyuuryou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_YukabutuJyuusyuuryou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_YukabutuJyuusyuuryou.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            99999999,
            0,
            0,
            196608});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.cantxt_YukabutuJyuusyuuryou.RangeSetting = rangeSettingDto2;
            this.cantxt_YukabutuJyuusyuuryou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_YukabutuJyuusyuuryou.RegistCheckMethod")));
            this.cantxt_YukabutuJyuusyuuryou.Size = new System.Drawing.Size(81, 20);
            this.cantxt_YukabutuJyuusyuuryou.TabIndex = 9;
            this.cantxt_YukabutuJyuusyuuryou.Tag = "";
            this.cantxt_YukabutuJyuusyuuryou.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.cantxt_YukabutuJyuusyuuryou.WordWrap = false;
            this.cantxt_YukabutuJyuusyuuryou.Leave += new System.EventHandler(this.cantxt_YukabutuJyuusyuuryou_Leave);
            // 
            // cantxt_UnnpannryouTanniCD
            // 
            this.cantxt_UnnpannryouTanniCD.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_UnnpannryouTanniCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_UnnpannryouTanniCD.CharacterLimitList = new char[] {
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9',
        '0'};
            this.cantxt_UnnpannryouTanniCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_UnnpannryouTanniCD.DisplayPopUp = null;
            this.cantxt_UnnpannryouTanniCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_UnnpannryouTanniCD.FocusOutCheckMethod")));
            this.cantxt_UnnpannryouTanniCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_UnnpannryouTanniCD.ForeColor = System.Drawing.Color.Black;
            this.cantxt_UnnpannryouTanniCD.GetCodeMasterField = "UNIT_CD,UNIT_NAME_RYAKU";
            this.cantxt_UnnpannryouTanniCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_UnnpannryouTanniCD.IsInputErrorOccured = false;
            this.cantxt_UnnpannryouTanniCD.Location = new System.Drawing.Point(153, 158);
            this.cantxt_UnnpannryouTanniCD.MaxLength = 2;
            this.cantxt_UnnpannryouTanniCD.Name = "cantxt_UnnpannryouTanniCD";
            this.cantxt_UnnpannryouTanniCD.PopupAfterExecute = null;
            this.cantxt_UnnpannryouTanniCD.PopupBeforeExecute = null;
            this.cantxt_UnnpannryouTanniCD.PopupGetMasterField = "UNIT_CD,UNIT_NAME_RYAKU";
            this.cantxt_UnnpannryouTanniCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_UnnpannryouTanniCD.PopupSearchSendParams")));
            this.cantxt_UnnpannryouTanniCD.PopupSetFormField = "cantxt_UnnpannryouTanniCD,ctxt_UnnpannryouTanniName";
            this.cantxt_UnnpannryouTanniCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_UNIT;
            this.cantxt_UnnpannryouTanniCD.PopupWindowName = "マスタ共通ポップアップ";
            this.cantxt_UnnpannryouTanniCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_UnnpannryouTanniCD.popupWindowSetting")));
            rangeSettingDto3.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.cantxt_UnnpannryouTanniCD.RangeSetting = rangeSettingDto3;
            this.cantxt_UnnpannryouTanniCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_UnnpannryouTanniCD.RegistCheckMethod")));
            this.cantxt_UnnpannryouTanniCD.SetFormField = "cantxt_UnnpannryouTanniCD,ctxt_UnnpannryouTanniName";
            this.cantxt_UnnpannryouTanniCD.Size = new System.Drawing.Size(29, 20);
            this.cantxt_UnnpannryouTanniCD.TabIndex = 8;
            this.cantxt_UnnpannryouTanniCD.Tag = "半角1桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            // 
            // ctxt_UnnpannryouTanniName
            // 
            this.ctxt_UnnpannryouTanniName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_UnnpannryouTanniName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_UnnpannryouTanniName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_UnnpannryouTanniName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_UnnpannryouTanniName.DisplayPopUp = null;
            this.ctxt_UnnpannryouTanniName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_UnnpannryouTanniName.FocusOutCheckMethod")));
            this.ctxt_UnnpannryouTanniName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_UnnpannryouTanniName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_UnnpannryouTanniName.IsInputErrorOccured = false;
            this.ctxt_UnnpannryouTanniName.Location = new System.Drawing.Point(181, 158);
            this.ctxt_UnnpannryouTanniName.MaxLength = 0;
            this.ctxt_UnnpannryouTanniName.Name = "ctxt_UnnpannryouTanniName";
            this.ctxt_UnnpannryouTanniName.PopupAfterExecute = null;
            this.ctxt_UnnpannryouTanniName.PopupBeforeExecute = null;
            this.ctxt_UnnpannryouTanniName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_UnnpannryouTanniName.PopupSearchSendParams")));
            this.ctxt_UnnpannryouTanniName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_UnnpannryouTanniName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_UnnpannryouTanniName.popupWindowSetting")));
            this.ctxt_UnnpannryouTanniName.ReadOnly = true;
            this.ctxt_UnnpannryouTanniName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_UnnpannryouTanniName.RegistCheckMethod")));
            this.ctxt_UnnpannryouTanniName.Size = new System.Drawing.Size(53, 20);
            this.ctxt_UnnpannryouTanniName.TabIndex = 628;
            this.ctxt_UnnpannryouTanniName.TabStop = false;
            this.ctxt_UnnpannryouTanniName.Tag = " ";
            // 
            // cantxt_SyaryouCD
            // 
            this.cantxt_SyaryouCD.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_SyaryouCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_SyaryouCD.CharacterLimitList = null;
            this.cantxt_SyaryouCD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_SyaryouCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_SyaryouCD.DisplayPopUp = null;
            this.cantxt_SyaryouCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_SyaryouCD.FocusOutCheckMethod")));
            this.cantxt_SyaryouCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_SyaryouCD.ForeColor = System.Drawing.Color.Black;
            this.cantxt_SyaryouCD.GetCodeMasterField = "SHARYOU_CD,SHARYOU_NAME_RYAKU";
            this.cantxt_SyaryouCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_SyaryouCD.IsInputErrorOccured = false;
            this.cantxt_SyaryouCD.Location = new System.Drawing.Point(153, 224);
            this.cantxt_SyaryouCD.MaxLength = 6;
            this.cantxt_SyaryouCD.Name = "cantxt_SyaryouCD";
            this.cantxt_SyaryouCD.PopupAfterExecute = null;
            this.cantxt_SyaryouCD.PopupBeforeExecute = null;
            this.cantxt_SyaryouCD.PopupGetMasterField = "SHARYOU_CD,SHARYOU_NAME_RYAKU";
            this.cantxt_SyaryouCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_SyaryouCD.PopupSearchSendParams")));
            this.cantxt_SyaryouCD.PopupSetFormField = "cantxt_SyaryouCD,ctxt_SyaryouName";
            this.cantxt_SyaryouCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHARYOU;
            this.cantxt_SyaryouCD.PopupWindowName = "マスタ共通ポップアップ";
            this.cantxt_SyaryouCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_SyaryouCD.popupWindowSetting")));
            this.cantxt_SyaryouCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_SyaryouCD.RegistCheckMethod")));
            this.cantxt_SyaryouCD.SetFormField = "cantxt_SyaryouCD,ctxt_SyaryouName";
            this.cantxt_SyaryouCD.Size = new System.Drawing.Size(70, 20);
            this.cantxt_SyaryouCD.TabIndex = 11;
            this.cantxt_SyaryouCD.Tag = "";
            this.cantxt_SyaryouCD.ZeroPaddengFlag = true;
            this.cantxt_SyaryouCD.BackColorChanged += new System.EventHandler(this.cantxt_SyaryouCD_BackColorChanged);
            this.cantxt_SyaryouCD.Enter += new System.EventHandler(this.cantxt_SyaryouCD_Enter);
            this.cantxt_SyaryouCD.Leave += new System.EventHandler(this.cantxt_SyaryouCD_Leave);
            // 
            // ctxt_SyaryouName
            // 
            this.ctxt_SyaryouName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_SyaryouName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_SyaryouName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_SyaryouName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_SyaryouName.DisplayPopUp = null;
            this.ctxt_SyaryouName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_SyaryouName.FocusOutCheckMethod")));
            this.ctxt_SyaryouName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_SyaryouName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_SyaryouName.IsInputErrorOccured = false;
            this.ctxt_SyaryouName.Location = new System.Drawing.Point(222, 224);
            this.ctxt_SyaryouName.MaxLength = 0;
            this.ctxt_SyaryouName.Name = "ctxt_SyaryouName";
            this.ctxt_SyaryouName.PopupAfterExecute = null;
            this.ctxt_SyaryouName.PopupBeforeExecute = null;
            this.ctxt_SyaryouName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_SyaryouName.PopupSearchSendParams")));
            this.ctxt_SyaryouName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_SyaryouName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_SyaryouName.popupWindowSetting")));
            this.ctxt_SyaryouName.ReadOnly = true;
            this.ctxt_SyaryouName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_SyaryouName.RegistCheckMethod")));
            this.ctxt_SyaryouName.Size = new System.Drawing.Size(192, 20);
            this.ctxt_SyaryouName.TabIndex = 630;
            this.ctxt_SyaryouName.TabStop = false;
            this.ctxt_SyaryouName.Tag = " ";
            // 
            // cantxt_YukabutuJyuusyuuryouTanniCD
            // 
            this.cantxt_YukabutuJyuusyuuryouTanniCD.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.CharacterLimitList = new char[] {
        '1',
        '2',
        '3',
        '4',
        '5',
        '6',
        '7',
        '8',
        '9',
        '0'};
            this.cantxt_YukabutuJyuusyuuryouTanniCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.DisplayPopUp = null;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_YukabutuJyuusyuuryouTanniCD.FocusOutCheckMethod")));
            this.cantxt_YukabutuJyuusyuuryouTanniCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_YukabutuJyuusyuuryouTanniCD.ForeColor = System.Drawing.Color.Black;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.GetCodeMasterField = "UNIT_CD,UNIT_NAME_RYAKU";
            this.cantxt_YukabutuJyuusyuuryouTanniCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.IsInputErrorOccured = false;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.Location = new System.Drawing.Point(153, 202);
            this.cantxt_YukabutuJyuusyuuryouTanniCD.MaxLength = 2;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.Name = "cantxt_YukabutuJyuusyuuryouTanniCD";
            this.cantxt_YukabutuJyuusyuuryouTanniCD.PopupAfterExecute = null;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.PopupBeforeExecute = null;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.PopupGetMasterField = "UNIT_CD,UNIT_NAME_RYAKU";
            this.cantxt_YukabutuJyuusyuuryouTanniCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_YukabutuJyuusyuuryouTanniCD.PopupSearchSendParams")));
            this.cantxt_YukabutuJyuusyuuryouTanniCD.PopupSetFormField = "cantxt_YukabutuJyuusyuuryouTanniCD,ctxt_YukabutuJyuusyuuryouTanniName";
            this.cantxt_YukabutuJyuusyuuryouTanniCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_UNIT;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.PopupWindowName = "マスタ共通ポップアップ";
            this.cantxt_YukabutuJyuusyuuryouTanniCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_YukabutuJyuusyuuryouTanniCD.popupWindowSetting")));
            rangeSettingDto4.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.cantxt_YukabutuJyuusyuuryouTanniCD.RangeSetting = rangeSettingDto4;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_YukabutuJyuusyuuryouTanniCD.RegistCheckMethod")));
            this.cantxt_YukabutuJyuusyuuryouTanniCD.SetFormField = "cantxt_YukabutuJyuusyuuryouTanniCD,ctxt_YukabutuJyuusyuuryouTanniName";
            this.cantxt_YukabutuJyuusyuuryouTanniCD.Size = new System.Drawing.Size(29, 20);
            this.cantxt_YukabutuJyuusyuuryouTanniCD.TabIndex = 10;
            this.cantxt_YukabutuJyuusyuuryouTanniCD.Tag = "";
            // 
            // ctxt_YukabutuJyuusyuuryouTanniName
            // 
            this.ctxt_YukabutuJyuusyuuryouTanniName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_YukabutuJyuusyuuryouTanniName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_YukabutuJyuusyuuryouTanniName.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_YukabutuJyuusyuuryouTanniName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_YukabutuJyuusyuuryouTanniName.DisplayPopUp = null;
            this.ctxt_YukabutuJyuusyuuryouTanniName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_YukabutuJyuusyuuryouTanniName.FocusOutCheckMethod")));
            this.ctxt_YukabutuJyuusyuuryouTanniName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ctxt_YukabutuJyuusyuuryouTanniName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_YukabutuJyuusyuuryouTanniName.IsInputErrorOccured = false;
            this.ctxt_YukabutuJyuusyuuryouTanniName.Location = new System.Drawing.Point(181, 202);
            this.ctxt_YukabutuJyuusyuuryouTanniName.MaxLength = 0;
            this.ctxt_YukabutuJyuusyuuryouTanniName.Name = "ctxt_YukabutuJyuusyuuryouTanniName";
            this.ctxt_YukabutuJyuusyuuryouTanniName.PopupAfterExecute = null;
            this.ctxt_YukabutuJyuusyuuryouTanniName.PopupBeforeExecute = null;
            this.ctxt_YukabutuJyuusyuuryouTanniName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_YukabutuJyuusyuuryouTanniName.PopupSearchSendParams")));
            this.ctxt_YukabutuJyuusyuuryouTanniName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_YukabutuJyuusyuuryouTanniName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_YukabutuJyuusyuuryouTanniName.popupWindowSetting")));
            this.ctxt_YukabutuJyuusyuuryouTanniName.ReadOnly = true;
            this.ctxt_YukabutuJyuusyuuryouTanniName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_YukabutuJyuusyuuryouTanniName.RegistCheckMethod")));
            this.ctxt_YukabutuJyuusyuuryouTanniName.Size = new System.Drawing.Size(53, 20);
            this.ctxt_YukabutuJyuusyuuryouTanniName.TabIndex = 632;
            this.ctxt_YukabutuJyuusyuuryouTanniName.TabStop = false;
            this.ctxt_YukabutuJyuusyuuryouTanniName.Tag = " ";
            // 
            // ccb_hikiwatasiHi
            // 
            this.ccb_hikiwatasiHi.AutoSize = true;
            this.ccb_hikiwatasiHi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ccb_hikiwatasiHi.DefaultBackColor = System.Drawing.Color.Empty;
            this.ccb_hikiwatasiHi.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ccb_hikiwatasiHi.FocusOutCheckMethod")));
            this.ccb_hikiwatasiHi.Font = new System.Drawing.Font("MS UI Gothic", 9.75F);
            this.ccb_hikiwatasiHi.Location = new System.Drawing.Point(296, 73);
            this.ccb_hikiwatasiHi.Name = "ccb_hikiwatasiHi";
            this.ccb_hikiwatasiHi.PopupAfterExecute = null;
            this.ccb_hikiwatasiHi.PopupBeforeExecute = null;
            this.ccb_hikiwatasiHi.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ccb_hikiwatasiHi.PopupSearchSendParams")));
            this.ccb_hikiwatasiHi.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ccb_hikiwatasiHi.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ccb_hikiwatasiHi.popupWindowSetting")));
            this.ccb_hikiwatasiHi.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ccb_hikiwatasiHi.RegistCheckMethod")));
            this.ccb_hikiwatasiHi.Size = new System.Drawing.Size(126, 17);
            this.ccb_hikiwatasiHi.TabIndex = 2;
            this.ccb_hikiwatasiHi.Text = "引渡し日使用有無";
            this.ccb_hikiwatasiHi.UseVisualStyleBackColor = false;
            this.ccb_hikiwatasiHi.CheckedChanged += new System.EventHandler(this.ccb_hikiwatasiHi_CheckedChanged);
            // 
            // ccb_UnnpannTanntousya
            // 
            this.ccb_UnnpannTanntousya.AutoSize = true;
            this.ccb_UnnpannTanntousya.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ccb_UnnpannTanntousya.DefaultBackColor = System.Drawing.Color.Empty;
            this.ccb_UnnpannTanntousya.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ccb_UnnpannTanntousya.FocusOutCheckMethod")));
            this.ccb_UnnpannTanntousya.Font = new System.Drawing.Font("MS UI Gothic", 9.75F);
            this.ccb_UnnpannTanntousya.Location = new System.Drawing.Point(442, 95);
            this.ccb_UnnpannTanntousya.Name = "ccb_UnnpannTanntousya";
            this.ccb_UnnpannTanntousya.PopupAfterExecute = null;
            this.ccb_UnnpannTanntousya.PopupBeforeExecute = null;
            this.ccb_UnnpannTanntousya.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ccb_UnnpannTanntousya.PopupSearchSendParams")));
            this.ccb_UnnpannTanntousya.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ccb_UnnpannTanntousya.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ccb_UnnpannTanntousya.popupWindowSetting")));
            this.ccb_UnnpannTanntousya.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ccb_UnnpannTanntousya.RegistCheckMethod")));
            this.ccb_UnnpannTanntousya.Size = new System.Drawing.Size(182, 17);
            this.ccb_UnnpannTanntousya.TabIndex = 4;
            this.ccb_UnnpannTanntousya.Text = "登録時運搬担当者使用有無";
            this.ccb_UnnpannTanntousya.UseVisualStyleBackColor = false;
            this.ccb_UnnpannTanntousya.CheckedChanged += new System.EventHandler(this.ccb_UnnpannTanntousya_CheckedChanged);
            // 
            // ccb_hikiwatasiRyou
            // 
            this.ccb_hikiwatasiRyou.AutoSize = true;
            this.ccb_hikiwatasiRyou.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ccb_hikiwatasiRyou.DefaultBackColor = System.Drawing.Color.Empty;
            this.ccb_hikiwatasiRyou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ccb_hikiwatasiRyou.FocusOutCheckMethod")));
            this.ccb_hikiwatasiRyou.Font = new System.Drawing.Font("MS UI Gothic", 9.75F);
            this.ccb_hikiwatasiRyou.Location = new System.Drawing.Point(239, 139);
            this.ccb_hikiwatasiRyou.Name = "ccb_hikiwatasiRyou";
            this.ccb_hikiwatasiRyou.PopupAfterExecute = null;
            this.ccb_hikiwatasiRyou.PopupBeforeExecute = null;
            this.ccb_hikiwatasiRyou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ccb_hikiwatasiRyou.PopupSearchSendParams")));
            this.ccb_hikiwatasiRyou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ccb_hikiwatasiRyou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ccb_hikiwatasiRyou.popupWindowSetting")));
            this.ccb_hikiwatasiRyou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ccb_hikiwatasiRyou.RegistCheckMethod")));
            this.ccb_hikiwatasiRyou.Size = new System.Drawing.Size(159, 17);
            this.ccb_hikiwatasiRyou.TabIndex = 7;
            this.ccb_hikiwatasiRyou.Text = "引渡し量・単位使用有無";
            this.ccb_hikiwatasiRyou.UseVisualStyleBackColor = false;
            this.ccb_hikiwatasiRyou.CheckedChanged += new System.EventHandler(this.ccb_hikiwatasiRyou_CheckedChanged);
            // 
            // ccb_SyaryouCD
            // 
            this.ccb_SyaryouCD.AutoSize = true;
            this.ccb_SyaryouCD.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ccb_SyaryouCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.ccb_SyaryouCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ccb_SyaryouCD.FocusOutCheckMethod")));
            this.ccb_SyaryouCD.Font = new System.Drawing.Font("MS UI Gothic", 9.75F);
            this.ccb_SyaryouCD.Location = new System.Drawing.Point(419, 227);
            this.ccb_SyaryouCD.Name = "ccb_SyaryouCD";
            this.ccb_SyaryouCD.PopupAfterExecute = null;
            this.ccb_SyaryouCD.PopupBeforeExecute = null;
            this.ccb_SyaryouCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ccb_SyaryouCD.PopupSearchSendParams")));
            this.ccb_SyaryouCD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ccb_SyaryouCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ccb_SyaryouCD.popupWindowSetting")));
            this.ccb_SyaryouCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ccb_SyaryouCD.RegistCheckMethod")));
            this.ccb_SyaryouCD.Size = new System.Drawing.Size(169, 17);
            this.ccb_SyaryouCD.TabIndex = 12;
            this.ccb_SyaryouCD.Text = "登録時車輌番号使用有無";
            this.ccb_SyaryouCD.UseVisualStyleBackColor = false;
            this.ccb_SyaryouCD.CheckedChanged += new System.EventHandler(this.ccb_SyaryouCD_CheckedChanged);
            // 
            // ctxt_bikou
            // 
            this.ctxt_bikou.BackColor = System.Drawing.SystemColors.Window;
            this.ctxt_bikou.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_bikou.CharactersNumber = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.ctxt_bikou.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_bikou.DisplayPopUp = null;
            this.ctxt_bikou.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_bikou.FocusOutCheckMethod")));
            this.ctxt_bikou.ForeColor = System.Drawing.Color.Black;
            this.ctxt_bikou.IsInputErrorOccured = false;
            this.ctxt_bikou.Location = new System.Drawing.Point(153, 246);
            this.ctxt_bikou.MaxLength = 128;
            this.ctxt_bikou.Multiline = true;
            this.ctxt_bikou.Name = "ctxt_bikou";
            this.ctxt_bikou.PopupAfterExecute = null;
            this.ctxt_bikou.PopupBeforeExecute = null;
            this.ctxt_bikou.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_bikou.PopupSearchSendParams")));
            this.ctxt_bikou.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_bikou.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_bikou.popupWindowSetting")));
            this.ctxt_bikou.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_bikou.RegistCheckMethod")));
            this.ctxt_bikou.Size = new System.Drawing.Size(430, 100);
            this.ctxt_bikou.TabIndex = 13;
            this.ctxt_bikou.Enter += new System.EventHandler(this.ctxt_bikou_Enter);
            this.ctxt_bikou.Validating += new System.ComponentModel.CancelEventHandler(this.ctxt_bikou_Validating);
            // 
            // bt_allSelect
            // 
            this.bt_allSelect.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_allSelect.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_allSelect.Enabled = false;
            this.bt_allSelect.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_allSelect.Location = new System.Drawing.Point(288, 357);
            this.bt_allSelect.Name = "bt_allSelect";
            this.bt_allSelect.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_allSelect.Size = new System.Drawing.Size(80, 35);
            this.bt_allSelect.TabIndex = 14;
            this.bt_allSelect.Text = "[F7]\r\n全て選択";
            this.bt_allSelect.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_allSelect.UseVisualStyleBackColor = false;
            this.bt_allSelect.Click += new System.EventHandler(this.bt_allSelect_Click);
            // 
            // bt_Input
            // 
            this.bt_Input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_Input.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_Input.Enabled = false;
            this.bt_Input.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Input.Location = new System.Drawing.Point(374, 357);
            this.bt_Input.Name = "bt_Input";
            this.bt_Input.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_Input.Size = new System.Drawing.Size(80, 35);
            this.bt_Input.TabIndex = 15;
            this.bt_Input.Text = "[F9]\r\n入力";
            this.bt_Input.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Input.UseVisualStyleBackColor = false;
            this.bt_Input.Click += new System.EventHandler(this.bt_Input_Click);
            // 
            // bt_Erase
            // 
            this.bt_Erase.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_Erase.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_Erase.Enabled = false;
            this.bt_Erase.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Erase.Location = new System.Drawing.Point(460, 357);
            this.bt_Erase.Name = "bt_Erase";
            this.bt_Erase.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_Erase.Size = new System.Drawing.Size(80, 35);
            this.bt_Erase.TabIndex = 16;
            this.bt_Erase.Text = "[F11]\r\n消去";
            this.bt_Erase.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Erase.UseVisualStyleBackColor = false;
            this.bt_Erase.Click += new System.EventHandler(this.bt_Erase_Click);
            // 
            // bt_Close
            // 
            this.bt_Close.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_Close.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_Close.Enabled = false;
            this.bt_Close.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_Close.Location = new System.Drawing.Point(544, 357);
            this.bt_Close.Name = "bt_Close";
            this.bt_Close.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_Close.Size = new System.Drawing.Size(80, 35);
            this.bt_Close.TabIndex = 17;
            this.bt_Close.Text = "[F12]\r\n閉じる";
            this.bt_Close.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_Close.UseVisualStyleBackColor = false;
            this.bt_Close.Click += new System.EventHandler(this.bt_Close_Click);
            // 
            // cantxt_hideEdiId
            // 
            this.cantxt_hideEdiId.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_hideEdiId.CharacterLimitList = null;
            this.cantxt_hideEdiId.DBFieldsName = "GYOUSHA_CD";
            this.cantxt_hideEdiId.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_hideEdiId.DisplayPopUp = null;
            this.cantxt_hideEdiId.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_hideEdiId.FocusOutCheckMethod")));
            this.cantxt_hideEdiId.ForeColor = System.Drawing.Color.Black;
            this.cantxt_hideEdiId.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_hideEdiId.IsInputErrorOccured = false;
            this.cantxt_hideEdiId.ItemDefinedTypes = "VARCHAR";
            this.cantxt_hideEdiId.Location = new System.Drawing.Point(6, 278);
            this.cantxt_hideEdiId.MaxLength = 7;
            this.cantxt_hideEdiId.Name = "cantxt_hideEdiId";
            this.cantxt_hideEdiId.PopupAfterExecute = null;
            this.cantxt_hideEdiId.PopupBeforeExecute = null;
            this.cantxt_hideEdiId.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_hideEdiId.PopupSearchSendParams")));
            this.cantxt_hideEdiId.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_hideEdiId.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_hideEdiId.popupWindowSetting")));
            this.cantxt_hideEdiId.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_hideEdiId.RegistCheckMethod")));
            this.cantxt_hideEdiId.Size = new System.Drawing.Size(100, 20);
            this.cantxt_hideEdiId.TabIndex = 639;
            this.cantxt_hideEdiId.Visible = false;
            // 
            // cantxt_hideDenshiUseKbn
            // 
            this.cantxt_hideDenshiUseKbn.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_hideDenshiUseKbn.CharacterLimitList = null;
            this.cantxt_hideDenshiUseKbn.CharactersNumber = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.cantxt_hideDenshiUseKbn.DBFieldsName = "DENSHI_USE_KBN";
            this.cantxt_hideDenshiUseKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_hideDenshiUseKbn.DisplayPopUp = null;
            this.cantxt_hideDenshiUseKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_hideDenshiUseKbn.FocusOutCheckMethod")));
            this.cantxt_hideDenshiUseKbn.ForeColor = System.Drawing.Color.Black;
            this.cantxt_hideDenshiUseKbn.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_hideDenshiUseKbn.IsInputErrorOccured = false;
            this.cantxt_hideDenshiUseKbn.ItemDefinedTypes = "bit";
            this.cantxt_hideDenshiUseKbn.Location = new System.Drawing.Point(6, 304);
            this.cantxt_hideDenshiUseKbn.MaxLength = 7;
            this.cantxt_hideDenshiUseKbn.Name = "cantxt_hideDenshiUseKbn";
            this.cantxt_hideDenshiUseKbn.PopupAfterExecute = null;
            this.cantxt_hideDenshiUseKbn.PopupBeforeExecute = null;
            this.cantxt_hideDenshiUseKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_hideDenshiUseKbn.PopupSearchSendParams")));
            this.cantxt_hideDenshiUseKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_hideDenshiUseKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_hideDenshiUseKbn.popupWindowSetting")));
            this.cantxt_hideDenshiUseKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_hideDenshiUseKbn.RegistCheckMethod")));
            this.cantxt_hideDenshiUseKbn.Size = new System.Drawing.Size(100, 20);
            this.cantxt_hideDenshiUseKbn.TabIndex = 640;
            this.cantxt_hideDenshiUseKbn.Text = "1";
            this.cantxt_hideDenshiUseKbn.Visible = false;
            // 
            // cantxt_HoukokuTanntousyaCD
            // 
            this.cantxt_HoukokuTanntousyaCD.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_HoukokuTanntousyaCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_HoukokuTanntousyaCD.ChangeUpperCase = true;
            this.cantxt_HoukokuTanntousyaCD.CharacterLimitList = null;
            this.cantxt_HoukokuTanntousyaCD.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cantxt_HoukokuTanntousyaCD.CheckOK_KanyushaCD = "";
            this.cantxt_HoukokuTanntousyaCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_HoukokuTanntousyaCD.DisplayPopUp = null;
            this.cantxt_HoukokuTanntousyaCD.EDI_MEMBER_ID_ControlName = "cantxt_hideEdiId";
            this.cantxt_HoukokuTanntousyaCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HoukokuTanntousyaCD.FocusOutCheckMethod")));
            this.cantxt_HoukokuTanntousyaCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_HoukokuTanntousyaCD.ForeColor = System.Drawing.Color.Black;
            this.cantxt_HoukokuTanntousyaCD.GetCodeMasterField = "TANTOUSHA_CD,TANTOUSHA_NAME";
            this.cantxt_HoukokuTanntousyaCD.HOUKOKU_HUYOU_KBN = false;
            this.cantxt_HoukokuTanntousyaCD.HST_KBN = false;
            this.cantxt_HoukokuTanntousyaCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_HoukokuTanntousyaCD.IsInputErrorOccured = false;
            this.cantxt_HoukokuTanntousyaCD.IsInputMustbeCheck = false;
            this.cantxt_HoukokuTanntousyaCD.JIGYOUJOU_KBN = null;
            this.cantxt_HoukokuTanntousyaCD.JIGYOUSHA_KBN = null;
            this.cantxt_HoukokuTanntousyaCD.LinkedGENBA_CD_ControlName = null;
            this.cantxt_HoukokuTanntousyaCD.Location = new System.Drawing.Point(153, 114);
            this.cantxt_HoukokuTanntousyaCD.MaxLength = 10;
            this.cantxt_HoukokuTanntousyaCD.Name = "cantxt_HoukokuTanntousyaCD";
            this.cantxt_HoukokuTanntousyaCD.PopupAfterExecute = null;
            this.cantxt_HoukokuTanntousyaCD.PopupBeforeExecute = null;
            this.cantxt_HoukokuTanntousyaCD.PopupGetMasterField = "TANTOUSHA_CD,TANTOUSHA_NAME";
            this.cantxt_HoukokuTanntousyaCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_HoukokuTanntousyaCD.PopupSearchSendParams")));
            this.cantxt_HoukokuTanntousyaCD.PopupSetFormField = "cantxt_HoukokuTanntousyaCD,ctxt_HoukokuTanntousyaName";
            this.cantxt_HoukokuTanntousyaCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_TANTOUSHA;
            this.cantxt_HoukokuTanntousyaCD.PopupWindowName = "マスタ共通ポップアップ";
            this.cantxt_HoukokuTanntousyaCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_HoukokuTanntousyaCD.popupWindowSetting")));
            this.cantxt_HoukokuTanntousyaCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HoukokuTanntousyaCD.RegistCheckMethod")));
            this.cantxt_HoukokuTanntousyaCD.SBN_KBN = false;
            this.cantxt_HoukokuTanntousyaCD.SetFormField = "cantxt_HoukokuTanntousyaCD,ctxt_HoukokuTanntousyaName";
            this.cantxt_HoukokuTanntousyaCD.Size = new System.Drawing.Size(93, 20);
            this.cantxt_HoukokuTanntousyaCD.TabIndex = 5;
            this.cantxt_HoukokuTanntousyaCD.Tag = "半角10桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_HoukokuTanntousyaCD.TANTOUSHA_KBN = "4";
            this.cantxt_HoukokuTanntousyaCD.UPN_KBN = false;
            this.cantxt_HoukokuTanntousyaCD.ZeroPaddengFlag = true;
            // 
            // cantxt_UnnpannTanntousyaCD
            // 
            this.cantxt_UnnpannTanntousyaCD.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_UnnpannTanntousyaCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_UnnpannTanntousyaCD.ChangeUpperCase = true;
            this.cantxt_UnnpannTanntousyaCD.CharacterLimitList = null;
            this.cantxt_UnnpannTanntousyaCD.CharactersNumber = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.cantxt_UnnpannTanntousyaCD.CheckOK_KanyushaCD = "";
            this.cantxt_UnnpannTanntousyaCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_UnnpannTanntousyaCD.DisplayPopUp = null;
            this.cantxt_UnnpannTanntousyaCD.EDI_MEMBER_ID_ControlName = "cantxt_hideEdiId";
            this.cantxt_UnnpannTanntousyaCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_UnnpannTanntousyaCD.FocusOutCheckMethod")));
            this.cantxt_UnnpannTanntousyaCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cantxt_UnnpannTanntousyaCD.ForeColor = System.Drawing.Color.Black;
            this.cantxt_UnnpannTanntousyaCD.GetCodeMasterField = "TANTOUSHA_CD,TANTOUSHA_NAME";
            this.cantxt_UnnpannTanntousyaCD.HOUKOKU_HUYOU_KBN = false;
            this.cantxt_UnnpannTanntousyaCD.HST_KBN = false;
            this.cantxt_UnnpannTanntousyaCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_UnnpannTanntousyaCD.IsInputErrorOccured = false;
            this.cantxt_UnnpannTanntousyaCD.IsInputMustbeCheck = false;
            this.cantxt_UnnpannTanntousyaCD.JIGYOUJOU_KBN = null;
            this.cantxt_UnnpannTanntousyaCD.JIGYOUSHA_KBN = null;
            this.cantxt_UnnpannTanntousyaCD.LinkedGENBA_CD_ControlName = null;
            this.cantxt_UnnpannTanntousyaCD.Location = new System.Drawing.Point(153, 92);
            this.cantxt_UnnpannTanntousyaCD.MaxLength = 10;
            this.cantxt_UnnpannTanntousyaCD.Name = "cantxt_UnnpannTanntousyaCD";
            this.cantxt_UnnpannTanntousyaCD.PopupAfterExecute = null;
            this.cantxt_UnnpannTanntousyaCD.PopupBeforeExecute = null;
            this.cantxt_UnnpannTanntousyaCD.PopupGetMasterField = "TANTOUSHA_CD,TANTOUSHA_NAME";
            this.cantxt_UnnpannTanntousyaCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_UnnpannTanntousyaCD.PopupSearchSendParams")));
            this.cantxt_UnnpannTanntousyaCD.PopupSetFormField = "cantxt_UnnpannTanntousyaCD,ctxt_UnnpannTanntousyaName";
            this.cantxt_UnnpannTanntousyaCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_DENSHI_TANTOUSHA;
            this.cantxt_UnnpannTanntousyaCD.PopupWindowName = "マスタ共通ポップアップ";
            this.cantxt_UnnpannTanntousyaCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_UnnpannTanntousyaCD.popupWindowSetting")));
            this.cantxt_UnnpannTanntousyaCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_UnnpannTanntousyaCD.RegistCheckMethod")));
            this.cantxt_UnnpannTanntousyaCD.SBN_KBN = false;
            this.cantxt_UnnpannTanntousyaCD.SetFormField = "cantxt_UnnpannTanntousyaCD,ctxt_UnnpannTanntousyaName";
            this.cantxt_UnnpannTanntousyaCD.Size = new System.Drawing.Size(93, 20);
            this.cantxt_UnnpannTanntousyaCD.TabIndex = 3;
            this.cantxt_UnnpannTanntousyaCD.Tag = "半角10桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_UnnpannTanntousyaCD.TANTOUSHA_KBN = "3";
            this.cantxt_UnnpannTanntousyaCD.UPN_KBN = false;
            this.cantxt_UnnpannTanntousyaCD.ZeroPaddengFlag = true;
            // 
            // cantxt_hideGyoushaCd
            // 
            this.cantxt_hideGyoushaCd.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_hideGyoushaCd.CharacterLimitList = null;
            this.cantxt_hideGyoushaCd.DBFieldsName = "GYOUSHA_CD";
            this.cantxt_hideGyoushaCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_hideGyoushaCd.DisplayPopUp = null;
            this.cantxt_hideGyoushaCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_hideGyoushaCd.FocusOutCheckMethod")));
            this.cantxt_hideGyoushaCd.ForeColor = System.Drawing.Color.Black;
            this.cantxt_hideGyoushaCd.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_hideGyoushaCd.IsInputErrorOccured = false;
            this.cantxt_hideGyoushaCd.ItemDefinedTypes = "VARCHAR";
            this.cantxt_hideGyoushaCd.Location = new System.Drawing.Point(6, 330);
            this.cantxt_hideGyoushaCd.MaxLength = 7;
            this.cantxt_hideGyoushaCd.Name = "cantxt_hideGyoushaCd";
            this.cantxt_hideGyoushaCd.PopupAfterExecute = null;
            this.cantxt_hideGyoushaCd.PopupBeforeExecute = null;
            this.cantxt_hideGyoushaCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_hideGyoushaCd.PopupSearchSendParams")));
            this.cantxt_hideGyoushaCd.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cantxt_hideGyoushaCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_hideGyoushaCd.popupWindowSetting")));
            this.cantxt_hideGyoushaCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_hideGyoushaCd.RegistCheckMethod")));
            this.cantxt_hideGyoushaCd.Size = new System.Drawing.Size(100, 20);
            this.cantxt_hideGyoushaCd.TabIndex = 641;
            this.cantxt_hideGyoushaCd.Visible = false;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(644, 411);
            this.Controls.Add(this.cantxt_hideGyoushaCd);
            this.Controls.Add(this.cantxt_hideDenshiUseKbn);
            this.Controls.Add(this.cantxt_hideEdiId);
            this.Controls.Add(this.bt_Close);
            this.Controls.Add(this.bt_Erase);
            this.Controls.Add(this.bt_Input);
            this.Controls.Add(this.bt_allSelect);
            this.Controls.Add(this.ctxt_bikou);
            this.Controls.Add(this.ccb_SyaryouCD);
            this.Controls.Add(this.ccb_hikiwatasiRyou);
            this.Controls.Add(this.ccb_UnnpannTanntousya);
            this.Controls.Add(this.ccb_hikiwatasiHi);
            this.Controls.Add(this.cantxt_YukabutuJyuusyuuryouTanniCD);
            this.Controls.Add(this.ctxt_YukabutuJyuusyuuryouTanniName);
            this.Controls.Add(this.cantxt_SyaryouCD);
            this.Controls.Add(this.ctxt_SyaryouName);
            this.Controls.Add(this.cantxt_UnnpannryouTanniCD);
            this.Controls.Add(this.ctxt_UnnpannryouTanniName);
            this.Controls.Add(this.cantxt_YukabutuJyuusyuuryou);
            this.Controls.Add(this.cantxt_Unnpannryou);
            this.Controls.Add(this.cantxt_HoukokuTanntousyaCD);
            this.Controls.Add(this.ctxt_HoukokuTanntousyaName);
            this.Controls.Add(this.cantxt_UnnpannTanntousyaCD);
            this.Controls.Add(this.ctxt_UnnpannTanntousyaName);
            this.Controls.Add(this.cDt_UnnpannSyuryouhi);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_InsatsuBusu);
            this.Controls.Add(this.lb_title);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "運搬終了報告情報　　一括入力";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.UIForm_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        internal System.Windows.Forms.Label lbl_InsatsuBusu;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.Label label7;
        internal System.Windows.Forms.Label label8;
        internal r_framework.CustomControl.CustomDateTimePicker cDt_UnnpannSyuryouhi;
        public Shougun.Core.ElectronicManifest.CustomControls_Ex.CustomForMasterKyoutuuPopup_Ex cantxt_UnnpannTanntousyaCD;
        public r_framework.CustomControl.CustomTextBox ctxt_UnnpannTanntousyaName;
        public Shougun.Core.ElectronicManifest.CustomControls_Ex.CustomForMasterKyoutuuPopup_Ex cantxt_HoukokuTanntousyaCD;
        public r_framework.CustomControl.CustomTextBox ctxt_HoukokuTanntousyaName;
        public r_framework.CustomControl.CustomNumericTextBox2 cantxt_Unnpannryou;
        public r_framework.CustomControl.CustomNumericTextBox2 cantxt_YukabutuJyuusyuuryou;
        public r_framework.CustomControl.CustomNumericTextBox2 cantxt_UnnpannryouTanniCD;
        public r_framework.CustomControl.CustomTextBox ctxt_UnnpannryouTanniName;
        public r_framework.CustomControl.CustomAlphaNumTextBox cantxt_SyaryouCD;
        public r_framework.CustomControl.CustomTextBox ctxt_SyaryouName;
        public r_framework.CustomControl.CustomNumericTextBox2 cantxt_YukabutuJyuusyuuryouTanniCD;
        public r_framework.CustomControl.CustomTextBox ctxt_YukabutuJyuusyuuryouTanniName;
        public r_framework.CustomControl.CustomTextBox ctxt_bikou;
        public r_framework.CustomControl.CustomButton bt_allSelect;
        public r_framework.CustomControl.CustomButton bt_Input;
        public r_framework.CustomControl.CustomButton bt_Erase;
        public r_framework.CustomControl.CustomButton bt_Close;
        public r_framework.CustomControl.CustomCheckBox ccb_hikiwatasiHi;
        public r_framework.CustomControl.CustomCheckBox ccb_UnnpannTanntousya;
        public r_framework.CustomControl.CustomCheckBox ccb_SyaryouCD;
        public r_framework.CustomControl.CustomCheckBox ccb_hikiwatasiRyou;
        public r_framework.CustomControl.CustomAlphaNumTextBox cantxt_hideEdiId;
        public r_framework.CustomControl.CustomAlphaNumTextBox cantxt_hideDenshiUseKbn;
        public r_framework.CustomControl.CustomAlphaNumTextBox cantxt_hideGyoushaCd;
    }
}