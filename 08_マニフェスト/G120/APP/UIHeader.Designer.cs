namespace Shougun.Core.PaperManifest.SampaiManifestoThumiKae
{
    partial class SampaiManifestoThumiKaeHeader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SampaiManifestoThumiKaeHeader));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.lbl_Kyoten = new System.Windows.Forms.Label();
            this.ctxt_KyotenCd = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lbl_syokaitouroku = new System.Windows.Forms.Label();
            this.lbl_lastupdate = new System.Windows.Forms.Label();
            this.customTextBox1 = new r_framework.CustomControl.CustomTextBox();
            this.customTextBox2 = new r_framework.CustomControl.CustomTextBox();
            this.customTextBox3 = new r_framework.CustomControl.CustomTextBox();
            this.customTextBox4 = new r_framework.CustomControl.CustomTextBox();
            this.ctxt_KyotenMei = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.BackColor = System.Drawing.Color.PaleTurquoise;
            this.windowTypeLabel.Text = "新規";
            // 
            // lb_title
            // 
            this.lb_title.Text = "産廃マニフェスト(積替用)一次";
            // 
            // lbl_Kyoten
            // 
            this.lbl_Kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_Kyoten.Location = new System.Drawing.Point(569, 2);
            this.lbl_Kyoten.Name = "lbl_Kyoten";
            this.lbl_Kyoten.Size = new System.Drawing.Size(69, 20);
            this.lbl_Kyoten.TabIndex = 517;
            this.lbl_Kyoten.Text = "拠点※";
            this.lbl_Kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ctxt_KyotenCd
            // 
            this.ctxt_KyotenCd.BackColor = System.Drawing.SystemColors.Window;
            this.ctxt_KyotenCd.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_KyotenCd.CustomFormatSetting = "00";
            this.ctxt_KyotenCd.DBFieldsName = "KYOTEN_CD";
            this.ctxt_KyotenCd.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_KyotenCd.DisplayItemName = "拠点CD";
            this.ctxt_KyotenCd.DisplayPopUp = null;
            this.ctxt_KyotenCd.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_KyotenCd.FocusOutCheckMethod")));
            this.ctxt_KyotenCd.ForeColor = System.Drawing.Color.Black;
            this.ctxt_KyotenCd.FormatSetting = "カスタム";
            this.ctxt_KyotenCd.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.ctxt_KyotenCd.IsInputErrorOccured = false;
            this.ctxt_KyotenCd.ItemDefinedTypes = "smallint";
            this.ctxt_KyotenCd.Location = new System.Drawing.Point(643, 2);
            this.ctxt_KyotenCd.Name = "ctxt_KyotenCd";
            this.ctxt_KyotenCd.PopupAfterExecute = null;
            this.ctxt_KyotenCd.PopupBeforeExecute = null;
            this.ctxt_KyotenCd.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.ctxt_KyotenCd.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_KyotenCd.PopupSearchSendParams")));
            this.ctxt_KyotenCd.PopupSendParams = new string[] {
        "ctxt_KyotenCd",
        "ctxt_KyotenMei"};
            this.ctxt_KyotenCd.PopupSetFormField = "ctxt_KyotenCd,ctxt_KyotenMei";
            this.ctxt_KyotenCd.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.ctxt_KyotenCd.PopupWindowName = "マスタ共通ポップアップ";
            this.ctxt_KyotenCd.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_KyotenCd.popupWindowSetting")));
            this.ctxt_KyotenCd.prevText = null;
            this.ctxt_KyotenCd.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.ctxt_KyotenCd.RangeSetting = rangeSettingDto1;
            this.ctxt_KyotenCd.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_KyotenCd.RegistCheckMethod")));
            this.ctxt_KyotenCd.SetFormField = "ctxt_KyotenCd,ctxt_KyotenMei";
            this.ctxt_KyotenCd.ShortItemName = "拠点CD";
            this.ctxt_KyotenCd.Size = new System.Drawing.Size(30, 20);
            this.ctxt_KyotenCd.TabIndex = 1;
            this.ctxt_KyotenCd.Tag = "半角2桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.ctxt_KyotenCd.WordWrap = false;
            this.ctxt_KyotenCd.Validated += new System.EventHandler(this.ctxt_KyotenCd_Validated);
            // 
            // lbl_syokaitouroku
            // 
            this.lbl_syokaitouroku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_syokaitouroku.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_syokaitouroku.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_syokaitouroku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_syokaitouroku.ForeColor = System.Drawing.Color.White;
            this.lbl_syokaitouroku.Location = new System.Drawing.Point(833, 2);
            this.lbl_syokaitouroku.Name = "lbl_syokaitouroku";
            this.lbl_syokaitouroku.Size = new System.Drawing.Size(93, 20);
            this.lbl_syokaitouroku.TabIndex = 532;
            this.lbl_syokaitouroku.Text = "初回登録";
            this.lbl_syokaitouroku.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_syokaitouroku.Visible = false;
            // 
            // lbl_lastupdate
            // 
            this.lbl_lastupdate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_lastupdate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_lastupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_lastupdate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_lastupdate.ForeColor = System.Drawing.Color.White;
            this.lbl_lastupdate.Location = new System.Drawing.Point(833, 24);
            this.lbl_lastupdate.Name = "lbl_lastupdate";
            this.lbl_lastupdate.Size = new System.Drawing.Size(93, 20);
            this.lbl_lastupdate.TabIndex = 533;
            this.lbl_lastupdate.Text = "最終更新";
            this.lbl_lastupdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_lastupdate.Visible = false;
            // 
            // customTextBox1
            // 
            this.customTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.customTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox1.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.customTextBox1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customTextBox1.DisplayPopUp = null;
            this.customTextBox1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox1.FocusOutCheckMethod")));
            this.customTextBox1.ForeColor = System.Drawing.Color.Black;
            this.customTextBox1.IsInputErrorOccured = false;
            this.customTextBox1.Location = new System.Drawing.Point(1039, 24);
            this.customTextBox1.MaxLength = 0;
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.PopupAfterExecute = null;
            this.customTextBox1.PopupBeforeExecute = null;
            this.customTextBox1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox1.PopupSearchSendParams")));
            this.customTextBox1.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.customTextBox1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox1.popupWindowSetting")));
            this.customTextBox1.prevText = null;
            this.customTextBox1.PrevText = null;
            this.customTextBox1.ReadOnly = true;
            this.customTextBox1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox1.RegistCheckMethod")));
            this.customTextBox1.Size = new System.Drawing.Size(126, 20);
            this.customTextBox1.TabIndex = 8;
            this.customTextBox1.TabStop = false;
            this.customTextBox1.Tag = "";
            this.customTextBox1.Visible = false;
            // 
            // customTextBox2
            // 
            this.customTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.customTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox2.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.customTextBox2.DefaultBackColor = System.Drawing.Color.Empty;
            this.customTextBox2.DisplayPopUp = null;
            this.customTextBox2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox2.FocusOutCheckMethod")));
            this.customTextBox2.ForeColor = System.Drawing.Color.Black;
            this.customTextBox2.IsInputErrorOccured = false;
            this.customTextBox2.Location = new System.Drawing.Point(1039, 2);
            this.customTextBox2.MaxLength = 0;
            this.customTextBox2.Name = "customTextBox2";
            this.customTextBox2.PopupAfterExecute = null;
            this.customTextBox2.PopupBeforeExecute = null;
            this.customTextBox2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox2.PopupSearchSendParams")));
            this.customTextBox2.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.customTextBox2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox2.popupWindowSetting")));
            this.customTextBox2.prevText = null;
            this.customTextBox2.PrevText = null;
            this.customTextBox2.ReadOnly = true;
            this.customTextBox2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox2.RegistCheckMethod")));
            this.customTextBox2.Size = new System.Drawing.Size(126, 20);
            this.customTextBox2.TabIndex = 4;
            this.customTextBox2.TabStop = false;
            this.customTextBox2.Tag = " ";
            this.customTextBox2.Visible = false;
            // 
            // customTextBox3
            // 
            this.customTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.customTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox3.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.customTextBox3.DefaultBackColor = System.Drawing.Color.Empty;
            this.customTextBox3.DisplayPopUp = null;
            this.customTextBox3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox3.FocusOutCheckMethod")));
            this.customTextBox3.ForeColor = System.Drawing.Color.Black;
            this.customTextBox3.IsInputErrorOccured = false;
            this.customTextBox3.Location = new System.Drawing.Point(931, 24);
            this.customTextBox3.MaxLength = 0;
            this.customTextBox3.Name = "customTextBox3";
            this.customTextBox3.PopupAfterExecute = null;
            this.customTextBox3.PopupBeforeExecute = null;
            this.customTextBox3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox3.PopupSearchSendParams")));
            this.customTextBox3.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.customTextBox3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox3.popupWindowSetting")));
            this.customTextBox3.prevText = null;
            this.customTextBox3.PrevText = null;
            this.customTextBox3.ReadOnly = true;
            this.customTextBox3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox3.RegistCheckMethod")));
            this.customTextBox3.Size = new System.Drawing.Size(109, 20);
            this.customTextBox3.TabIndex = 7;
            this.customTextBox3.TabStop = false;
            this.customTextBox3.Tag = "";
            this.customTextBox3.Visible = false;
            // 
            // customTextBox4
            // 
            this.customTextBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.customTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox4.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.customTextBox4.DefaultBackColor = System.Drawing.Color.Empty;
            this.customTextBox4.DisplayPopUp = null;
            this.customTextBox4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox4.FocusOutCheckMethod")));
            this.customTextBox4.ForeColor = System.Drawing.Color.Black;
            this.customTextBox4.IsInputErrorOccured = false;
            this.customTextBox4.Location = new System.Drawing.Point(931, 2);
            this.customTextBox4.MaxLength = 0;
            this.customTextBox4.Name = "customTextBox4";
            this.customTextBox4.PopupAfterExecute = null;
            this.customTextBox4.PopupBeforeExecute = null;
            this.customTextBox4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox4.PopupSearchSendParams")));
            this.customTextBox4.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.customTextBox4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox4.popupWindowSetting")));
            this.customTextBox4.prevText = null;
            this.customTextBox4.PrevText = null;
            this.customTextBox4.ReadOnly = true;
            this.customTextBox4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox4.RegistCheckMethod")));
            this.customTextBox4.Size = new System.Drawing.Size(109, 20);
            this.customTextBox4.TabIndex = 3;
            this.customTextBox4.TabStop = false;
            this.customTextBox4.Tag = "";
            this.customTextBox4.Visible = false;
            // 
            // ctxt_KyotenMei
            // 
            this.ctxt_KyotenMei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_KyotenMei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_KyotenMei.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.ctxt_KyotenMei.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_KyotenMei.DisplayPopUp = null;
            this.ctxt_KyotenMei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_KyotenMei.FocusOutCheckMethod")));
            this.ctxt_KyotenMei.ForeColor = System.Drawing.Color.Black;
            this.ctxt_KyotenMei.IsInputErrorOccured = false;
            this.ctxt_KyotenMei.Location = new System.Drawing.Point(672, 2);
            this.ctxt_KyotenMei.MaxLength = 0;
            this.ctxt_KyotenMei.Name = "ctxt_KyotenMei";
            this.ctxt_KyotenMei.PopupAfterExecute = null;
            this.ctxt_KyotenMei.PopupBeforeExecute = null;
            this.ctxt_KyotenMei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_KyotenMei.PopupSearchSendParams")));
            this.ctxt_KyotenMei.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.ctxt_KyotenMei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_KyotenMei.popupWindowSetting")));
            this.ctxt_KyotenMei.prevText = null;
            this.ctxt_KyotenMei.PrevText = null;
            this.ctxt_KyotenMei.ReadOnly = true;
            this.ctxt_KyotenMei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_KyotenMei.RegistCheckMethod")));
            this.ctxt_KyotenMei.Size = new System.Drawing.Size(156, 20);
            this.ctxt_KyotenMei.TabIndex = 2;
            this.ctxt_KyotenMei.TabStop = false;
            this.ctxt_KyotenMei.Tag = " は 0 文字以内で入力してください。";
            // 
            // SampaiManifestoThumiKaeHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.customTextBox3);
            this.Controls.Add(this.customTextBox4);
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.customTextBox2);
            this.Controls.Add(this.ctxt_KyotenMei);
            this.Controls.Add(this.lbl_Kyoten);
            this.Controls.Add(this.lbl_lastupdate);
            this.Controls.Add(this.lbl_syokaitouroku);
            this.Controls.Add(this.ctxt_KyotenCd);
            this.Name = "SampaiManifestoThumiKaeHeader";
            this.Text = "産廃マニフェスト(直行用)一次";
            this.Controls.SetChildIndex(this.ctxt_KyotenCd, 0);
            this.Controls.SetChildIndex(this.lbl_syokaitouroku, 0);
            this.Controls.SetChildIndex(this.lbl_lastupdate, 0);
            this.Controls.SetChildIndex(this.lbl_Kyoten, 0);
            this.Controls.SetChildIndex(this.ctxt_KyotenMei, 0);
            this.Controls.SetChildIndex(this.customTextBox2, 0);
            this.Controls.SetChildIndex(this.customTextBox1, 0);
            this.Controls.SetChildIndex(this.customTextBox4, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.customTextBox3, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label lbl_Kyoten;
        internal r_framework.CustomControl.CustomNumericTextBox2 ctxt_KyotenCd;
        internal System.Windows.Forms.Label lbl_syokaitouroku;
        internal System.Windows.Forms.Label lbl_lastupdate;
        internal r_framework.CustomControl.CustomTextBox customTextBox1;
        internal r_framework.CustomControl.CustomTextBox customTextBox2;
        internal r_framework.CustomControl.CustomTextBox customTextBox3;
        internal r_framework.CustomControl.CustomTextBox customTextBox4;
        internal r_framework.CustomControl.CustomTextBox ctxt_KyotenMei;


    }
}