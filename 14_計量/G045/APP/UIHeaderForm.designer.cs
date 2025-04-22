namespace Shougun.Core.Scale.Keiryou
{
    partial class UIHeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.KYOTEN_LABEL = new System.Windows.Forms.Label();
            this.label35 = new System.Windows.Forms.Label();
            this.LastUpdateDate = new r_framework.CustomControl.CustomTextBox();
            this.LastUpdateUser = new r_framework.CustomControl.CustomTextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.CreateDate = new r_framework.CustomControl.CustomTextBox();
            this.CreateUser = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(212, 34);
            this.lb_title.Text = "計量";
            // 
            // KYOTEN_NAME_RYAKU
            // 
            this.KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_NAME_RYAKU.CharacterLimitList = null;
            this.KYOTEN_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.KYOTEN_NAME_RYAKU.DBFieldsName = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_NAME_RYAKU.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.KYOTEN_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(606, 2);
            this.KYOTEN_NAME_RYAKU.MaxLength = 0;
            this.KYOTEN_NAME_RYAKU.Name = "KYOTEN_NAME_RYAKU";
            this.KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.KYOTEN_NAME_RYAKU.TabIndex = 520;
            this.KYOTEN_NAME_RYAKU.TabStop = false;
            this.KYOTEN_NAME_RYAKU.Tag = " ";
            // 
            // KYOTEN_LABEL
            // 
            this.KYOTEN_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.KYOTEN_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.KYOTEN_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_LABEL.ForeColor = System.Drawing.Color.White;
            this.KYOTEN_LABEL.Location = new System.Drawing.Point(510, 2);
            this.KYOTEN_LABEL.Name = "KYOTEN_LABEL";
            this.KYOTEN_LABEL.Size = new System.Drawing.Size(60, 20);
            this.KYOTEN_LABEL.TabIndex = 519;
            this.KYOTEN_LABEL.Text = "拠点※";
            this.KYOTEN_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label35
            // 
            this.label35.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label35.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label35.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label35.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label35.ForeColor = System.Drawing.Color.White;
            this.label35.Location = new System.Drawing.Point(769, 24);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(75, 20);
            this.label35.TabIndex = 516;
            this.label35.Text = "最終更新";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LastUpdateDate
            // 
            this.LastUpdateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LastUpdateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastUpdateDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.LastUpdateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.LastUpdateDate.DisplayPopUp = null;
            this.LastUpdateDate.ErrorMessage = "";
            this.LastUpdateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateDate.FocusOutCheckMethod")));
            this.LastUpdateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateDate.ForeColor = System.Drawing.Color.Black;
            this.LastUpdateDate.GetCodeMasterField = "";
            this.LastUpdateDate.IsInputErrorOccured = false;
            this.LastUpdateDate.Location = new System.Drawing.Point(1008, 24);
            this.LastUpdateDate.MaxLength = 0;
            this.LastUpdateDate.Name = "LastUpdateDate";
            this.LastUpdateDate.PopupAfterExecute = null;
            this.LastUpdateDate.PopupBeforeExecute = null;
            this.LastUpdateDate.PopupGetMasterField = "";
            this.LastUpdateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateDate.PopupSearchSendParams")));
            this.LastUpdateDate.PopupSetFormField = "";
            this.LastUpdateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LastUpdateDate.popupWindowSetting")));
            this.LastUpdateDate.ReadOnly = true;
            this.LastUpdateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateDate.RegistCheckMethod")));
            this.LastUpdateDate.SetFormField = "";
            this.LastUpdateDate.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateDate.TabIndex = 515;
            this.LastUpdateDate.TabStop = false;
            this.LastUpdateDate.Tag = "最終更新日が表示されます。";
            this.LastUpdateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LastUpdateUser
            // 
            this.LastUpdateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.LastUpdateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LastUpdateUser.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.LastUpdateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.LastUpdateUser.DisplayPopUp = null;
            this.LastUpdateUser.ErrorMessage = "";
            this.LastUpdateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateUser.FocusOutCheckMethod")));
            this.LastUpdateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LastUpdateUser.ForeColor = System.Drawing.Color.Black;
            this.LastUpdateUser.GetCodeMasterField = "";
            this.LastUpdateUser.IsInputErrorOccured = false;
            this.LastUpdateUser.Location = new System.Drawing.Point(849, 24);
            this.LastUpdateUser.MaxLength = 0;
            this.LastUpdateUser.Name = "LastUpdateUser";
            this.LastUpdateUser.PopupAfterExecute = null;
            this.LastUpdateUser.PopupBeforeExecute = null;
            this.LastUpdateUser.PopupGetMasterField = "";
            this.LastUpdateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("LastUpdateUser.PopupSearchSendParams")));
            this.LastUpdateUser.PopupSetFormField = "";
            this.LastUpdateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.LastUpdateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("LastUpdateUser.popupWindowSetting")));
            this.LastUpdateUser.ReadOnly = true;
            this.LastUpdateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("LastUpdateUser.RegistCheckMethod")));
            this.LastUpdateUser.SetFormField = "";
            this.LastUpdateUser.Size = new System.Drawing.Size(160, 20);
            this.LastUpdateUser.TabIndex = 514;
            this.LastUpdateUser.TabStop = false;
            this.LastUpdateUser.Tag = "最終更新者が表示されます。";
            // 
            // label36
            // 
            this.label36.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label36.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label36.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label36.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label36.ForeColor = System.Drawing.Color.White;
            this.label36.Location = new System.Drawing.Point(769, 2);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(75, 20);
            this.label36.TabIndex = 513;
            this.label36.Text = "初回登録";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // CreateDate
            // 
            this.CreateDate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CreateDate.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateDate.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CreateDate.DefaultBackColor = System.Drawing.Color.Empty;
            this.CreateDate.DisplayPopUp = null;
            this.CreateDate.ErrorMessage = "";
            this.CreateDate.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateDate.FocusOutCheckMethod")));
            this.CreateDate.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateDate.ForeColor = System.Drawing.Color.Black;
            this.CreateDate.GetCodeMasterField = "";
            this.CreateDate.IsInputErrorOccured = false;
            this.CreateDate.Location = new System.Drawing.Point(1008, 2);
            this.CreateDate.MaxLength = 0;
            this.CreateDate.Name = "CreateDate";
            this.CreateDate.PopupAfterExecute = null;
            this.CreateDate.PopupBeforeExecute = null;
            this.CreateDate.PopupGetMasterField = "";
            this.CreateDate.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateDate.PopupSearchSendParams")));
            this.CreateDate.PopupSetFormField = "";
            this.CreateDate.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateDate.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CreateDate.popupWindowSetting")));
            this.CreateDate.ReadOnly = true;
            this.CreateDate.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateDate.RegistCheckMethod")));
            this.CreateDate.SetFormField = "";
            this.CreateDate.Size = new System.Drawing.Size(160, 20);
            this.CreateDate.TabIndex = 512;
            this.CreateDate.TabStop = false;
            this.CreateDate.Tag = "初回登録日が表示されます。";
            this.CreateDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CreateUser
            // 
            this.CreateUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.CreateUser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CreateUser.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.CreateUser.DefaultBackColor = System.Drawing.Color.Empty;
            this.CreateUser.DisplayPopUp = null;
            this.CreateUser.ErrorMessage = "";
            this.CreateUser.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateUser.FocusOutCheckMethod")));
            this.CreateUser.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.CreateUser.ForeColor = System.Drawing.Color.Black;
            this.CreateUser.GetCodeMasterField = "";
            this.CreateUser.IsInputErrorOccured = false;
            this.CreateUser.Location = new System.Drawing.Point(849, 2);
            this.CreateUser.MaxLength = 0;
            this.CreateUser.Name = "CreateUser";
            this.CreateUser.PopupAfterExecute = null;
            this.CreateUser.PopupBeforeExecute = null;
            this.CreateUser.PopupGetMasterField = "";
            this.CreateUser.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CreateUser.PopupSearchSendParams")));
            this.CreateUser.PopupSetFormField = "";
            this.CreateUser.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CreateUser.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CreateUser.popupWindowSetting")));
            this.CreateUser.ReadOnly = true;
            this.CreateUser.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CreateUser.RegistCheckMethod")));
            this.CreateUser.SetFormField = "";
            this.CreateUser.Size = new System.Drawing.Size(160, 20);
            this.CreateUser.TabIndex = 511;
            this.CreateUser.TabStop = false;
            this.CreateUser.Tag = "初回登録者が表示されます。";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.Aqua;
            this.label1.Location = new System.Drawing.Point(300, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 35);
            this.label1.TabIndex = 510;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // KYOTEN_CD
            // 
            this.KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KYOTEN_CD.DisplayItemName = "拠点CD";
            this.KYOTEN_CD.DisplayPopUp = null;
            this.KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.FocusOutCheckMethod")));
            this.KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.IsInputErrorOccured = false;
            this.KYOTEN_CD.ItemDefinedTypes = "smallint";
            this.KYOTEN_CD.Location = new System.Drawing.Point(575, 2);
            this.KYOTEN_CD.Name = "KYOTEN_CD";
            this.KYOTEN_CD.PopupAfterExecute = null;
            this.KYOTEN_CD.PopupBeforeExecute = null;
            this.KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KYOTEN_CD.PopupSearchSendParams")));
            this.KYOTEN_CD.PopupSetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KYOTEN_CD.RegistCheckMethod")));
            this.KYOTEN_CD.SetFormField = "KYOTEN_CD, KYOTEN_NAME_RYAKU";
            this.KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.KYOTEN_CD.TabIndex = 0;
            this.KYOTEN_CD.Tag = "拠点を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KYOTEN_CD.WordWrap = false;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(511, 23);
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
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.KYOTEN_CD);
            this.Controls.Add(this.KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.KYOTEN_LABEL);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.LastUpdateDate);
            this.Controls.Add(this.LastUpdateUser);
            this.Controls.Add(this.label36);
            this.Controls.Add(this.CreateDate);
            this.Controls.Add(this.CreateUser);
            this.Controls.Add(this.label1);
            this.Name = "UIHeaderForm";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.CreateUser, 0);
            this.Controls.SetChildIndex(this.CreateDate, 0);
            this.Controls.SetChildIndex(this.label36, 0);
            this.Controls.SetChildIndex(this.LastUpdateUser, 0);
            this.Controls.SetChildIndex(this.LastUpdateDate, 0);
            this.Controls.SetChildIndex(this.label35, 0);
            this.Controls.SetChildIndex(this.KYOTEN_LABEL, 0);
            this.Controls.SetChildIndex(this.KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomAlphaNumTextBox KYOTEN_NAME_RYAKU;
        public System.Windows.Forms.Label KYOTEN_LABEL;
        public System.Windows.Forms.Label label35;
        public r_framework.CustomControl.CustomTextBox LastUpdateDate;
        public r_framework.CustomControl.CustomTextBox LastUpdateUser;
        public System.Windows.Forms.Label label36;
        public r_framework.CustomControl.CustomTextBox CreateDate;
        public r_framework.CustomControl.CustomTextBox CreateUser;
        public System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomNumericTextBox2 KYOTEN_CD;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
    }
}
