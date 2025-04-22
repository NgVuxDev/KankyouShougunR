namespace Shougun.Core.Common.HanyoCSVShutsuryoku.APP.Panel
{
    partial class NyuushukkinPatternPanel
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NyuushukkinPatternPanel));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.lblDenshuKbn = new System.Windows.Forms.Label();
            this.pnlDenshuKbn = new System.Windows.Forms.Panel();
            this.txtDenshuKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.chkDenshuKbnShukkin = new r_framework.CustomControl.CustomCheckBox();
            this.chkDenshuKbnNyuukin = new r_framework.CustomControl.CustomCheckBox();
            this.lblShimeKbn = new System.Windows.Forms.Label();
            this.pnlShimeKbn = new System.Windows.Forms.Panel();
            this.rdoShimeKbn3 = new r_framework.CustomControl.CustomRadioButton();
            this.rdoShimeKbn2 = new r_framework.CustomControl.CustomRadioButton();
            this.rdoShimeKbn1 = new r_framework.CustomControl.CustomRadioButton();
            this.txtShimeKbn = new r_framework.CustomControl.CustomNumericTextBox2();
            this.pnlDummy = new r_framework.CustomControl.CustomPanel();
            this.pnlDenshuKbn.SuspendLayout();
            this.pnlShimeKbn.SuspendLayout();
            this.pnlDummy.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDenshuKbn
            // 
            this.lblDenshuKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblDenshuKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDenshuKbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDenshuKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblDenshuKbn.ForeColor = System.Drawing.Color.White;
            this.lblDenshuKbn.Location = new System.Drawing.Point(0, 0);
            this.lblDenshuKbn.Name = "lblDenshuKbn";
            this.lblDenshuKbn.Size = new System.Drawing.Size(110, 20);
            this.lblDenshuKbn.TabIndex = 10;
            this.lblDenshuKbn.Text = "伝票種類※";
            this.lblDenshuKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlDenshuKbn
            // 
            this.pnlDenshuKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDenshuKbn.Controls.Add(this.txtDenshuKbn);
            this.pnlDenshuKbn.Controls.Add(this.chkDenshuKbnShukkin);
            this.pnlDenshuKbn.Controls.Add(this.chkDenshuKbnNyuukin);
            this.pnlDenshuKbn.Location = new System.Drawing.Point(116, 0);
            this.pnlDenshuKbn.Name = "pnlDenshuKbn";
            this.pnlDenshuKbn.Size = new System.Drawing.Size(132, 20);
            this.pnlDenshuKbn.TabIndex = 20;
            // 
            // txtDenshuKbn
            // 
            this.txtDenshuKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtDenshuKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDenshuKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtDenshuKbn.DisplayItemName = "伝票種類";
            this.txtDenshuKbn.DisplayPopUp = null;
            this.txtDenshuKbn.Enabled = false;
            this.txtDenshuKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtDenshuKbn.FocusOutCheckMethod")));
            this.txtDenshuKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtDenshuKbn.ForeColor = System.Drawing.Color.Black;
            this.txtDenshuKbn.IsInputErrorOccured = false;
            this.txtDenshuKbn.Location = new System.Drawing.Point(-21, -1);
            this.txtDenshuKbn.Name = "txtDenshuKbn";
            this.txtDenshuKbn.PopupAfterExecute = null;
            this.txtDenshuKbn.PopupBeforeExecute = null;
            this.txtDenshuKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtDenshuKbn.PopupSearchSendParams")));
            this.txtDenshuKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtDenshuKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtDenshuKbn.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.txtDenshuKbn.RangeSetting = rangeSettingDto1;
            this.txtDenshuKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtDenshuKbn.RegistCheckMethod")));
            this.txtDenshuKbn.ShortItemName = "伝票種類";
            this.txtDenshuKbn.Size = new System.Drawing.Size(20, 20);
            this.txtDenshuKbn.TabIndex = 25;
            this.txtDenshuKbn.TabStop = false;
            this.txtDenshuKbn.Tag = "";
            this.txtDenshuKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtDenshuKbn.WordWrap = false;
            // 
            // chkDenshuKbnShukkin
            // 
            this.chkDenshuKbnShukkin.AutoSize = true;
            this.chkDenshuKbnShukkin.BackColor = System.Drawing.SystemColors.Control;
            this.chkDenshuKbnShukkin.DefaultBackColor = System.Drawing.Color.Empty;
            this.chkDenshuKbnShukkin.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkDenshuKbnShukkin.FocusOutCheckMethod")));
            this.chkDenshuKbnShukkin.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkDenshuKbnShukkin.Location = new System.Drawing.Point(65, 1);
            this.chkDenshuKbnShukkin.Name = "chkDenshuKbnShukkin";
            this.chkDenshuKbnShukkin.PopupAfterExecute = null;
            this.chkDenshuKbnShukkin.PopupBeforeExecute = null;
            this.chkDenshuKbnShukkin.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chkDenshuKbnShukkin.PopupSearchSendParams")));
            this.chkDenshuKbnShukkin.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chkDenshuKbnShukkin.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chkDenshuKbnShukkin.popupWindowSetting")));
            this.chkDenshuKbnShukkin.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkDenshuKbnShukkin.RegistCheckMethod")));
            this.chkDenshuKbnShukkin.Size = new System.Drawing.Size(54, 17);
            this.chkDenshuKbnShukkin.TabIndex = 40;
            this.chkDenshuKbnShukkin.Text = "出金";
            this.chkDenshuKbnShukkin.UseVisualStyleBackColor = false;
            // 
            // chkDenshuKbnNyuukin
            // 
            this.chkDenshuKbnNyuukin.AutoSize = true;
            this.chkDenshuKbnNyuukin.BackColor = System.Drawing.SystemColors.Control;
            this.chkDenshuKbnNyuukin.DefaultBackColor = System.Drawing.Color.Empty;
            this.chkDenshuKbnNyuukin.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkDenshuKbnNyuukin.FocusOutCheckMethod")));
            this.chkDenshuKbnNyuukin.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.chkDenshuKbnNyuukin.Location = new System.Drawing.Point(5, 1);
            this.chkDenshuKbnNyuukin.Name = "chkDenshuKbnNyuukin";
            this.chkDenshuKbnNyuukin.PopupAfterExecute = null;
            this.chkDenshuKbnNyuukin.PopupBeforeExecute = null;
            this.chkDenshuKbnNyuukin.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("chkDenshuKbnNyuukin.PopupSearchSendParams")));
            this.chkDenshuKbnNyuukin.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.chkDenshuKbnNyuukin.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("chkDenshuKbnNyuukin.popupWindowSetting")));
            this.chkDenshuKbnNyuukin.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("chkDenshuKbnNyuukin.RegistCheckMethod")));
            this.chkDenshuKbnNyuukin.Size = new System.Drawing.Size(54, 17);
            this.chkDenshuKbnNyuukin.TabIndex = 30;
            this.chkDenshuKbnNyuukin.Text = "入金";
            this.chkDenshuKbnNyuukin.UseVisualStyleBackColor = false;
            // 
            // lblShimeKbn
            // 
            this.lblShimeKbn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblShimeKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblShimeKbn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblShimeKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblShimeKbn.ForeColor = System.Drawing.Color.White;
            this.lblShimeKbn.Location = new System.Drawing.Point(0, 22);
            this.lblShimeKbn.Name = "lblShimeKbn";
            this.lblShimeKbn.Size = new System.Drawing.Size(110, 20);
            this.lblShimeKbn.TabIndex = 50;
            this.lblShimeKbn.Text = "締処理状況※";
            this.lblShimeKbn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlShimeKbn
            // 
            this.pnlShimeKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlShimeKbn.Controls.Add(this.rdoShimeKbn3);
            this.pnlShimeKbn.Controls.Add(this.rdoShimeKbn2);
            this.pnlShimeKbn.Controls.Add(this.rdoShimeKbn1);
            this.pnlShimeKbn.Controls.Add(this.txtShimeKbn);
            this.pnlShimeKbn.Location = new System.Drawing.Point(116, 22);
            this.pnlShimeKbn.Name = "pnlShimeKbn";
            this.pnlShimeKbn.Size = new System.Drawing.Size(266, 20);
            this.pnlShimeKbn.TabIndex = 70;
            // 
            // rdoShimeKbn3
            // 
            this.rdoShimeKbn3.AutoSize = true;
            this.rdoShimeKbn3.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoShimeKbn3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShimeKbn3.FocusOutCheckMethod")));
            this.rdoShimeKbn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoShimeKbn3.LinkedTextBox = "txtShimeKbn";
            this.rdoShimeKbn3.Location = new System.Drawing.Point(185, 1);
            this.rdoShimeKbn3.Name = "rdoShimeKbn3";
            this.rdoShimeKbn3.PopupAfterExecute = null;
            this.rdoShimeKbn3.PopupBeforeExecute = null;
            this.rdoShimeKbn3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoShimeKbn3.PopupSearchSendParams")));
            this.rdoShimeKbn3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoShimeKbn3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoShimeKbn3.popupWindowSetting")));
            this.rdoShimeKbn3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShimeKbn3.RegistCheckMethod")));
            this.rdoShimeKbn3.Size = new System.Drawing.Size(67, 17);
            this.rdoShimeKbn3.TabIndex = 100;
            this.rdoShimeKbn3.Tag = "締処理状況を選択します";
            this.rdoShimeKbn3.Text = "3.全て";
            this.rdoShimeKbn3.UseVisualStyleBackColor = true;
            this.rdoShimeKbn3.Value = "3";
            // 
            // rdoShimeKbn2
            // 
            this.rdoShimeKbn2.AutoSize = true;
            this.rdoShimeKbn2.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoShimeKbn2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShimeKbn2.FocusOutCheckMethod")));
            this.rdoShimeKbn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoShimeKbn2.LinkedTextBox = "txtShimeKbn";
            this.rdoShimeKbn2.Location = new System.Drawing.Point(98, 1);
            this.rdoShimeKbn2.Name = "rdoShimeKbn2";
            this.rdoShimeKbn2.PopupAfterExecute = null;
            this.rdoShimeKbn2.PopupBeforeExecute = null;
            this.rdoShimeKbn2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoShimeKbn2.PopupSearchSendParams")));
            this.rdoShimeKbn2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoShimeKbn2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoShimeKbn2.popupWindowSetting")));
            this.rdoShimeKbn2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShimeKbn2.RegistCheckMethod")));
            this.rdoShimeKbn2.Size = new System.Drawing.Size(67, 17);
            this.rdoShimeKbn2.TabIndex = 90;
            this.rdoShimeKbn2.Tag = "締処理状況を選択します";
            this.rdoShimeKbn2.Text = "2.未締";
            this.rdoShimeKbn2.UseVisualStyleBackColor = true;
            this.rdoShimeKbn2.Value = "2";
            // 
            // rdoShimeKbn1
            // 
            this.rdoShimeKbn1.AutoSize = true;
            this.rdoShimeKbn1.CausesValidation = false;
            this.rdoShimeKbn1.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoShimeKbn1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShimeKbn1.FocusOutCheckMethod")));
            this.rdoShimeKbn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoShimeKbn1.LinkedTextBox = "txtShimeKbn";
            this.rdoShimeKbn1.Location = new System.Drawing.Point(25, 1);
            this.rdoShimeKbn1.Name = "rdoShimeKbn1";
            this.rdoShimeKbn1.PopupAfterExecute = null;
            this.rdoShimeKbn1.PopupBeforeExecute = null;
            this.rdoShimeKbn1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoShimeKbn1.PopupSearchSendParams")));
            this.rdoShimeKbn1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoShimeKbn1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoShimeKbn1.popupWindowSetting")));
            this.rdoShimeKbn1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoShimeKbn1.RegistCheckMethod")));
            this.rdoShimeKbn1.Size = new System.Drawing.Size(53, 17);
            this.rdoShimeKbn1.TabIndex = 80;
            this.rdoShimeKbn1.Tag = "締処理状況を選択します";
            this.rdoShimeKbn1.Text = "1.済";
            this.rdoShimeKbn1.UseVisualStyleBackColor = true;
            this.rdoShimeKbn1.Value = "1";
            // 
            // txtShimeKbn
            // 
            this.txtShimeKbn.BackColor = System.Drawing.SystemColors.Window;
            this.txtShimeKbn.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtShimeKbn.ChangeUpperCase = true;
            this.txtShimeKbn.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtShimeKbn.DisplayItemName = "締処理状況";
            this.txtShimeKbn.DisplayPopUp = null;
            this.txtShimeKbn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtShimeKbn.FocusOutCheckMethod")));
            this.txtShimeKbn.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtShimeKbn.ForeColor = System.Drawing.Color.Black;
            this.txtShimeKbn.IsInputErrorOccured = false;
            this.txtShimeKbn.LinkedRadioButtonArray = new string[] {
        "rdoShimeKbn1",
        "rdoShimeKbn2",
        "rdoShimeKbn3"};
            this.txtShimeKbn.Location = new System.Drawing.Point(-1, -1);
            this.txtShimeKbn.Name = "txtShimeKbn";
            this.txtShimeKbn.PopupAfterExecute = null;
            this.txtShimeKbn.PopupBeforeExecute = null;
            this.txtShimeKbn.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtShimeKbn.PopupSearchSendParams")));
            this.txtShimeKbn.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtShimeKbn.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtShimeKbn.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtShimeKbn.RangeSetting = rangeSettingDto2;
            this.txtShimeKbn.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtShimeKbn.RegistCheckMethod")));
            this.txtShimeKbn.ShortItemName = "締処理状況";
            this.txtShimeKbn.Size = new System.Drawing.Size(20, 20);
            this.txtShimeKbn.TabIndex = 60;
            this.txtShimeKbn.Tag = "【１～３】のいずれかで入力してください";
            this.txtShimeKbn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtShimeKbn.WordWrap = false;
            // 
            // pnlDummy
            // 
            this.pnlDummy.AutoSize = true;
            this.pnlDummy.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlDummy.Controls.Add(this.lblDenshuKbn);
            this.pnlDummy.Controls.Add(this.pnlShimeKbn);
            this.pnlDummy.Controls.Add(this.lblShimeKbn);
            this.pnlDummy.Controls.Add(this.pnlDenshuKbn);
            this.pnlDummy.Location = new System.Drawing.Point(0, 0);
            this.pnlDummy.Margin = new System.Windows.Forms.Padding(0);
            this.pnlDummy.Name = "pnlDummy";
            this.pnlDummy.Size = new System.Drawing.Size(385, 45);
            this.pnlDummy.TabIndex = 71;
            // 
            // NyuushukkinPatternPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pnlDummy);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "NyuushukkinPatternPanel";
            this.Size = new System.Drawing.Size(385, 45);
            this.pnlDenshuKbn.ResumeLayout(false);
            this.pnlDenshuKbn.PerformLayout();
            this.pnlShimeKbn.ResumeLayout(false);
            this.pnlShimeKbn.PerformLayout();
            this.pnlDummy.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDenshuKbn;
        private System.Windows.Forms.Panel pnlDenshuKbn;
        private System.Windows.Forms.Label lblShimeKbn;
        internal r_framework.CustomControl.CustomCheckBox chkDenshuKbnShukkin;
        internal r_framework.CustomControl.CustomCheckBox chkDenshuKbnNyuukin;
        private System.Windows.Forms.Panel pnlShimeKbn;
        internal r_framework.CustomControl.CustomRadioButton rdoShimeKbn3;
        internal r_framework.CustomControl.CustomRadioButton rdoShimeKbn2;
        internal r_framework.CustomControl.CustomRadioButton rdoShimeKbn1;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtShimeKbn;
        internal r_framework.CustomControl.CustomPanel pnlDummy;
        internal r_framework.CustomControl.CustomNumericTextBox2 txtDenshuKbn;
    }
}
