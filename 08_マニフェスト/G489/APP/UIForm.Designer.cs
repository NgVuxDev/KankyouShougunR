namespace Shougun.Core.PaperManifest.InsatsuBusuSettei
{
    partial class InsatsuBusuSettei
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InsatsuBusuSettei));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.txt_KoufuNo = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.lbl_KoufuNo = new System.Windows.Forms.Label();
            this.cbx_ManifestToroku = new r_framework.CustomControl.CustomCheckBox();
            this.txt_InsatsuBusu = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lbl_InsatsuBusu = new System.Windows.Forms.Label();
            this.lb_title = new System.Windows.Forms.Label();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
            this.bt_func4 = new r_framework.CustomControl.CustomButton();
            this.bt_func1 = new r_framework.CustomControl.CustomButton();
            this.bt_func11 = new r_framework.CustomControl.CustomButton();
            this.SuspendLayout();
            // 
            // txt_KoufuNo
            // 
            this.txt_KoufuNo.BackColor = System.Drawing.SystemColors.Window;
            this.txt_KoufuNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KoufuNo.CharacterLimitList = null;
            this.txt_KoufuNo.CharactersNumber = new decimal(new int[] {
            11,
            0,
            0,
            0});
            this.txt_KoufuNo.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KoufuNo.DisplayPopUp = null;
            this.txt_KoufuNo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KoufuNo.FocusOutCheckMethod")));
            this.txt_KoufuNo.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_KoufuNo.ForeColor = System.Drawing.Color.Black;
            this.txt_KoufuNo.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.txt_KoufuNo.IsInputErrorOccured = false;
            this.txt_KoufuNo.Location = new System.Drawing.Point(132, 108);
            this.txt_KoufuNo.MaxLength = 11;
            this.txt_KoufuNo.Name = "txt_KoufuNo";
            this.txt_KoufuNo.PopupAfterExecute = null;
            this.txt_KoufuNo.PopupBeforeExecute = null;
            this.txt_KoufuNo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KoufuNo.PopupSearchSendParams")));
            this.txt_KoufuNo.PopupSendParams = new string[0];
            this.txt_KoufuNo.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_KoufuNo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KoufuNo.popupWindowSetting")));
            this.txt_KoufuNo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KoufuNo.RegistCheckMethod")));
            this.txt_KoufuNo.Size = new System.Drawing.Size(99, 20);
            this.txt_KoufuNo.TabIndex = 50;
            this.txt_KoufuNo.Tag = "半角11桁以内で入力してください";
            this.txt_KoufuNo.Validated += new System.EventHandler(this.txt_KoufuNo_Validated);
            // 
            // lbl_KoufuNo
            // 
            this.lbl_KoufuNo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_KoufuNo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_KoufuNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_KoufuNo.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lbl_KoufuNo.ForeColor = System.Drawing.Color.White;
            this.lbl_KoufuNo.Location = new System.Drawing.Point(17, 108);
            this.lbl_KoufuNo.Name = "lbl_KoufuNo";
            this.lbl_KoufuNo.Size = new System.Drawing.Size(110, 20);
            this.lbl_KoufuNo.TabIndex = 40;
            this.lbl_KoufuNo.Text = "交付番号※";
            this.lbl_KoufuNo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbx_ManifestToroku
            // 
            this.cbx_ManifestToroku.AutoSize = true;
            this.cbx_ManifestToroku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.cbx_ManifestToroku.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbx_ManifestToroku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_ManifestToroku.FocusOutCheckMethod")));
            this.cbx_ManifestToroku.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.cbx_ManifestToroku.Location = new System.Drawing.Point(220, 72);
            this.cbx_ManifestToroku.Name = "cbx_ManifestToroku";
            this.cbx_ManifestToroku.PopupAfterExecute = null;
            this.cbx_ManifestToroku.PopupBeforeExecute = null;
            this.cbx_ManifestToroku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbx_ManifestToroku.PopupSearchSendParams")));
            this.cbx_ManifestToroku.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.cbx_ManifestToroku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbx_ManifestToroku.popupWindowSetting")));
            this.cbx_ManifestToroku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbx_ManifestToroku.RegistCheckMethod")));
            this.cbx_ManifestToroku.Size = new System.Drawing.Size(166, 17);
            this.cbx_ManifestToroku.TabIndex = 30;
            this.cbx_ManifestToroku.TabStop = false;
            this.cbx_ManifestToroku.Tag = "マニフェスト登録を行う場合チェックを付けてください";
            this.cbx_ManifestToroku.Text = "マニフェスト登録する";
            this.cbx_ManifestToroku.UseVisualStyleBackColor = false;
            this.cbx_ManifestToroku.Visible = false;
            // 
            // txt_InsatsuBusu
            // 
            this.txt_InsatsuBusu.BackColor = System.Drawing.SystemColors.Window;
            this.txt_InsatsuBusu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_InsatsuBusu.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_InsatsuBusu.DisplayPopUp = null;
            this.txt_InsatsuBusu.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_InsatsuBusu.FocusOutCheckMethod")));
            this.txt_InsatsuBusu.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txt_InsatsuBusu.ForeColor = System.Drawing.Color.Black;
            this.txt_InsatsuBusu.IsInputErrorOccured = false;
            this.txt_InsatsuBusu.Location = new System.Drawing.Point(132, 72);
            this.txt_InsatsuBusu.Name = "txt_InsatsuBusu";
            this.txt_InsatsuBusu.PopupAfterExecute = null;
            this.txt_InsatsuBusu.PopupBeforeExecute = null;
            this.txt_InsatsuBusu.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_InsatsuBusu.PopupSearchSendParams")));
            this.txt_InsatsuBusu.PopupSendParams = new string[0];
            this.txt_InsatsuBusu.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_InsatsuBusu.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_InsatsuBusu.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.txt_InsatsuBusu.RangeSetting = rangeSettingDto1;
            this.txt_InsatsuBusu.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_InsatsuBusu.RegistCheckMethod")));
            this.txt_InsatsuBusu.Size = new System.Drawing.Size(38, 20);
            this.txt_InsatsuBusu.TabIndex = 20;
            this.txt_InsatsuBusu.Tag = "半角3桁以内で入力してください";
            this.txt_InsatsuBusu.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txt_InsatsuBusu.WordWrap = false;
            this.txt_InsatsuBusu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_InsatsuBusu_KeyPress);
            // 
            // lbl_InsatsuBusu
            // 
            this.lbl_InsatsuBusu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_InsatsuBusu.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_InsatsuBusu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_InsatsuBusu.Font = new System.Drawing.Font("MS Gothic", 9.75F);
            this.lbl_InsatsuBusu.ForeColor = System.Drawing.Color.White;
            this.lbl_InsatsuBusu.Location = new System.Drawing.Point(17, 72);
            this.lbl_InsatsuBusu.Name = "lbl_InsatsuBusu";
            this.lbl_InsatsuBusu.Size = new System.Drawing.Size(110, 20);
            this.lbl_InsatsuBusu.TabIndex = 10;
            this.lbl_InsatsuBusu.Text = "印刷部数※";
            this.lbl_InsatsuBusu.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("MS Gothic", 20.25F, System.Drawing.FontStyle.Bold);
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(17, 21);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(393, 34);
            this.lb_title.TabIndex = 0;
            this.lb_title.Text = "印刷部数の設定";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.CausesValidation = false;
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func12.Location = new System.Drawing.Point(334, 155);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 90;
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // bt_func9
            // 
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Enabled = false;
            this.bt_func9.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func9.Location = new System.Drawing.Point(172, 155);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(80, 35);
            this.bt_func9.TabIndex = 80;
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            // 
            // bt_func4
            // 
            this.bt_func4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func4.CausesValidation = false;
            this.bt_func4.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func4.Enabled = false;
            this.bt_func4.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func4.Location = new System.Drawing.Point(91, 155);
            this.bt_func4.Name = "bt_func4";
            this.bt_func4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func4.Size = new System.Drawing.Size(80, 35);
            this.bt_func4.TabIndex = 70;
            this.bt_func4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func4.UseVisualStyleBackColor = false;
            // 
            // bt_func1
            // 
            this.bt_func1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func1.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func1.Enabled = false;
            this.bt_func1.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func1.Location = new System.Drawing.Point(10, 155);
            this.bt_func1.Name = "bt_func1";
            this.bt_func1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func1.Size = new System.Drawing.Size(80, 35);
            this.bt_func1.TabIndex = 60;
            this.bt_func1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func1.UseVisualStyleBackColor = false;
            // 
            // bt_func11
            // 
            this.bt_func11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func11.CausesValidation = false;
            this.bt_func11.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func11.Enabled = false;
            this.bt_func11.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func11.Location = new System.Drawing.Point(253, 155);
            this.bt_func11.Name = "bt_func11";
            this.bt_func11.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func11.Size = new System.Drawing.Size(80, 35);
            this.bt_func11.TabIndex = 90;
            this.bt_func11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func11.UseVisualStyleBackColor = false;
            // 
            // InsatsuBusuSettei
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(427, 209);
            this.Controls.Add(this.bt_func4);
            this.Controls.Add(this.bt_func1);
            this.Controls.Add(this.bt_func11);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.txt_KoufuNo);
            this.Controls.Add(this.lbl_KoufuNo);
            this.Controls.Add(this.bt_func9);
            this.Controls.Add(this.cbx_ManifestToroku);
            this.Controls.Add(this.txt_InsatsuBusu);
            this.Controls.Add(this.lbl_InsatsuBusu);
            this.Controls.Add(this.lb_title);
            this.Font = new System.Drawing.Font("MS Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InsatsuBusuSettei";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomAlphaNumTextBox txt_KoufuNo;
        internal System.Windows.Forms.Label lbl_KoufuNo;
        internal r_framework.CustomControl.CustomNumericTextBox2 txt_InsatsuBusu;
        internal System.Windows.Forms.Label lbl_InsatsuBusu;
        public System.Windows.Forms.Label lb_title;
        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomButton bt_func9;
        public r_framework.CustomControl.CustomCheckBox cbx_ManifestToroku;
        public r_framework.CustomControl.CustomButton bt_func4;
        public r_framework.CustomControl.CustomButton bt_func1;
        public r_framework.CustomControl.CustomButton bt_func11;



    }
}