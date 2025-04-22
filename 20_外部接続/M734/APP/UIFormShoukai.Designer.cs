namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
{
    partial class ShoukaiJouken
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShoukaiJouken));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.cbx_AllFile = new r_framework.CustomControl.CustomCheckBox();
            this.lb_title = new System.Windows.Forms.Label();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_func8 = new r_framework.CustomControl.CustomButton();
            this.txt_Page = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label1 = new System.Windows.Forms.Label();
            this.KEIYAKU_JYOUKYOU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.KEIYAKU_JYOUKYOU_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SHAIN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SHAIN_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SuspendLayout();
            // 
            // cbx_AllFile
            // 
            this.cbx_AllFile.AutoSize = true;
            this.cbx_AllFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.cbx_AllFile.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbx_AllFile.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_AllFile.FocusOutCheckMethod")));
            this.cbx_AllFile.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.cbx_AllFile.Location = new System.Drawing.Point(151, 127);
            this.cbx_AllFile.Name = "cbx_AllFile";
            this.cbx_AllFile.PopupAfterExecute = null;
            this.cbx_AllFile.PopupBeforeExecute = null;
            this.cbx_AllFile.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbx_AllFile.PopupSearchSendParams")));
            this.cbx_AllFile.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cbx_AllFile.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbx_AllFile.popupWindowSetting")));
            this.cbx_AllFile.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_AllFile.RegistCheckMethod")));
            this.cbx_AllFile.Size = new System.Drawing.Size(292, 17);
            this.cbx_AllFile.TabIndex = 50;
            this.cbx_AllFile.Tag = "";
            this.cbx_AllFile.Text = "クラウドサイン全ファイルを最新照会する";
            this.cbx_AllFile.UseVisualStyleBackColor = false;
            this.cbx_AllFile.CheckedChanged += new System.EventHandler(this.cbx_AllFile_CheckedChanged);
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold);
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(17, 21);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(393, 34);
            this.lb_title.TabIndex = 0;
            this.lb_title.Text = "電子契約照会条件";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.CausesValidation = false;
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func12.Location = new System.Drawing.Point(330, 166);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 80;
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // bt_func8
            // 
            this.bt_func8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func8.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func8.Enabled = false;
            this.bt_func8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func8.Location = new System.Drawing.Point(244, 166);
            this.bt_func8.Name = "bt_func8";
            this.bt_func8.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func8.Size = new System.Drawing.Size(80, 35);
            this.bt_func8.TabIndex = 70;
            this.bt_func8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func8.UseVisualStyleBackColor = false;
            // 
            // txt_Page
            // 
            this.txt_Page.BackColor = System.Drawing.SystemColors.Window;
            this.txt_Page.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_Page.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_Page.DisplayPopUp = null;
            this.txt_Page.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Page.FocusOutCheckMethod")));
            this.txt_Page.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_Page.ForeColor = System.Drawing.Color.Black;
            this.txt_Page.IsInputErrorOccured = false;
            this.txt_Page.Location = new System.Drawing.Point(107, 125);
            this.txt_Page.Name = "txt_Page";
            this.txt_Page.PopupAfterExecute = null;
            this.txt_Page.PopupBeforeExecute = null;
            this.txt_Page.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_Page.PopupSearchSendParams")));
            this.txt_Page.PopupSendParams = new string[0];
            this.txt_Page.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_Page.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_Page.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txt_Page.RangeSetting = rangeSettingDto1;
            this.txt_Page.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_Page.RegistCheckMethod")));
            this.txt_Page.Size = new System.Drawing.Size(38, 20);
            this.txt_Page.TabIndex = 40;
            this.txt_Page.Tag = "";
            this.txt_Page.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_Page.WordWrap = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(17, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 20);
            this.label1.TabIndex = 91;
            this.label1.Text = "ページ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KEIYAKU_JYOUKYOU_NAME
            // 
            this.KEIYAKU_JYOUKYOU_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.KEIYAKU_JYOUKYOU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKU_JYOUKYOU_NAME.DBFieldsName = "KEIYAKU_JYOUKYOU_NAME";
            this.KEIYAKU_JYOUKYOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKU_JYOUKYOU_NAME.DisplayPopUp = null;
            this.KEIYAKU_JYOUKYOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.FocusOutCheckMethod")));
            this.KEIYAKU_JYOUKYOU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KEIYAKU_JYOUKYOU_NAME.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKU_JYOUKYOU_NAME.IsInputErrorOccured = false;
            this.KEIYAKU_JYOUKYOU_NAME.Location = new System.Drawing.Point(133, 102);
            this.KEIYAKU_JYOUKYOU_NAME.Name = "KEIYAKU_JYOUKYOU_NAME";
            this.KEIYAKU_JYOUKYOU_NAME.PopupAfterExecute = null;
            this.KEIYAKU_JYOUKYOU_NAME.PopupBeforeExecute = null;
            this.KEIYAKU_JYOUKYOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.PopupSearchSendParams")));
            this.KEIYAKU_JYOUKYOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKU_JYOUKYOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.popupWindowSetting")));
            this.KEIYAKU_JYOUKYOU_NAME.ReadOnly = true;
            this.KEIYAKU_JYOUKYOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_NAME.RegistCheckMethod")));
            this.KEIYAKU_JYOUKYOU_NAME.Size = new System.Drawing.Size(113, 20);
            this.KEIYAKU_JYOUKYOU_NAME.TabIndex = 20;
            this.KEIYAKU_JYOUKYOU_NAME.TabStop = false;
            // 
            // KEIYAKU_JYOUKYOU_CD
            // 
            this.KEIYAKU_JYOUKYOU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.KEIYAKU_JYOUKYOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKU_JYOUKYOU_CD.DBFieldsName = "KEIYAKU_JYOUKYOU_CD";
            this.KEIYAKU_JYOUKYOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKU_JYOUKYOU_CD.DisplayItemName = "契約状況";
            this.KEIYAKU_JYOUKYOU_CD.DisplayPopUp = null;
            this.KEIYAKU_JYOUKYOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.FocusOutCheckMethod")));
            this.KEIYAKU_JYOUKYOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KEIYAKU_JYOUKYOU_CD.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKU_JYOUKYOU_CD.IsInputErrorOccured = false;
            this.KEIYAKU_JYOUKYOU_CD.Location = new System.Drawing.Point(107, 102);
            this.KEIYAKU_JYOUKYOU_CD.Name = "KEIYAKU_JYOUKYOU_CD";
            this.KEIYAKU_JYOUKYOU_CD.PopupAfterExecute = null;
            this.KEIYAKU_JYOUKYOU_CD.PopupBeforeExecute = null;
            this.KEIYAKU_JYOUKYOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.PopupSearchSendParams")));
            this.KEIYAKU_JYOUKYOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKU_JYOUKYOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.popupWindowSetting")));
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
            this.KEIYAKU_JYOUKYOU_CD.RangeSetting = rangeSettingDto2;
            this.KEIYAKU_JYOUKYOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKU_JYOUKYOU_CD.RegistCheckMethod")));
            this.KEIYAKU_JYOUKYOU_CD.ShortItemName = "契約状況";
            this.KEIYAKU_JYOUKYOU_CD.Size = new System.Drawing.Size(20, 20);
            this.KEIYAKU_JYOUKYOU_CD.TabIndex = 10;
            this.KEIYAKU_JYOUKYOU_CD.Tag = "契約状況を指定してください（スペースキー押下にて、検索画面を表示します）";
            this.KEIYAKU_JYOUKYOU_CD.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.KEIYAKU_JYOUKYOU_CD.WordWrap = false;
            this.KEIYAKU_JYOUKYOU_CD.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Keiyaku_Jyoukyou_KeyDown);
            this.KEIYAKU_JYOUKYOU_CD.Validated += new System.EventHandler(this.KEIYAKU_JYOUKYOU_CD_Validated);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(17, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(84, 20);
            this.label6.TabIndex = 661;
            this.label6.Text = "契約状況";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 20);
            this.label2.TabIndex = 664;
            this.label2.Text = "作成者";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHAIN_NAME
            // 
            this.SHAIN_NAME.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHAIN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHAIN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_NAME.DisplayPopUp = null;
            this.SHAIN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME.FocusOutCheckMethod")));
            this.SHAIN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHAIN_NAME.ForeColor = System.Drawing.Color.Black;
            this.SHAIN_NAME.IsInputErrorOccured = false;
            this.SHAIN_NAME.Location = new System.Drawing.Point(163, 79);
            this.SHAIN_NAME.Name = "SHAIN_NAME";
            this.SHAIN_NAME.PopupAfterExecute = null;
            this.SHAIN_NAME.PopupBeforeExecute = null;
            this.SHAIN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_NAME.PopupSearchSendParams")));
            this.SHAIN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHAIN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_NAME.popupWindowSetting")));
            this.SHAIN_NAME.ReadOnly = true;
            this.SHAIN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_NAME.RegistCheckMethod")));
            this.SHAIN_NAME.Size = new System.Drawing.Size(102, 20);
            this.SHAIN_NAME.TabIndex = 5;
            this.SHAIN_NAME.TabStop = false;
            this.SHAIN_NAME.Tag = " ";
            // 
            // SHAIN_CD
            // 
            this.SHAIN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHAIN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHAIN_CD.ChangeUpperCase = true;
            this.SHAIN_CD.CharacterLimitList = null;
            this.SHAIN_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.SHAIN_CD.DBFieldsName = "SHAIN_CD";
            this.SHAIN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHAIN_CD.DisplayItemName = "クライアントID";
            this.SHAIN_CD.DisplayPopUp = null;
            this.SHAIN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_CD.FocusOutCheckMethod")));
            this.SHAIN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHAIN_CD.ForeColor = System.Drawing.Color.Black;
            this.SHAIN_CD.GetCodeMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.SHAIN_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHAIN_CD.IsInputErrorOccured = false;
            this.SHAIN_CD.ItemDefinedTypes = "varchar";
            this.SHAIN_CD.Location = new System.Drawing.Point(107, 79);
            this.SHAIN_CD.MaxLength = 6;
            this.SHAIN_CD.Name = "SHAIN_CD";
            this.SHAIN_CD.PopupAfterExecute = null;
            this.SHAIN_CD.PopupBeforeExecute = null;
            this.SHAIN_CD.PopupGetMasterField = "SHAIN_CD,SHAIN_NAME_RYAKU";
            this.SHAIN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHAIN_CD.PopupSearchSendParams")));
            this.SHAIN_CD.PopupSendParams = new string[0];
            this.SHAIN_CD.PopupSetFormField = "SHAIN_CD,SHAIN_NAME";
            this.SHAIN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHAIN;
            this.SHAIN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.SHAIN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHAIN_CD.popupWindowSetting")));
            this.SHAIN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHAIN_CD.RegistCheckMethod")));
            this.SHAIN_CD.SetFormField = "SHAIN_CD,SHAIN_NAME";
            this.SHAIN_CD.ShortItemName = "社員CD";
            this.SHAIN_CD.Size = new System.Drawing.Size(50, 20);
            this.SHAIN_CD.TabIndex = 1;
            this.SHAIN_CD.Tag = "社員CDを指定してください（スペースキー押下にて、検索画面を表示します）";
            this.SHAIN_CD.ZeroPaddengFlag = true;
            // 
            // ShoukaiJouken
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(463, 222);
            this.Controls.Add(this.SHAIN_NAME);
            this.Controls.Add(this.SHAIN_CD);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.KEIYAKU_JYOUKYOU_NAME);
            this.Controls.Add(this.KEIYAKU_JYOUKYOU_CD);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_Page);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_func8);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.cbx_AllFile);
            this.Controls.Add(this.lb_title);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ShoukaiJouken";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomCheckBox cbx_AllFile;
        public r_framework.CustomControl.CustomButton bt_func8;
        internal r_framework.CustomControl.CustomNumericTextBox2 txt_Page;
        internal System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomTextBox KEIYAKU_JYOUKYOU_NAME;
        internal r_framework.CustomControl.CustomNumericTextBox2 KEIYAKU_JYOUKYOU_CD;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomTextBox SHAIN_NAME;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHAIN_CD;



    }
}