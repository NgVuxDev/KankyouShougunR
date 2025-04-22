// $Id: UIHeader.Designer.cs 21477 2014-05-27 01:55:33Z takeda $
using r_framework.CustomControl;
namespace Shougun.Core.PaperManifest.JissekiHokokuIchiran
{
    partial class UIHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeader));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.customPanel8 = new r_framework.CustomControl.CustomPanel();
            this.rdoUnbanJiseiki = new r_framework.CustomControl.CustomRadioButton();
            this.rdoSisetsuJiseiki = new r_framework.CustomControl.CustomRadioButton();
            this.rdoSyobunJiseiki = new r_framework.CustomControl.CustomRadioButton();
            this.txtHokokuSyurui = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label14 = new System.Windows.Forms.Label();
            this.customPanel8.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(10, 6);
            this.lb_title.Size = new System.Drawing.Size(422, 34);
            // 
            // customPanel8
            // 
            this.customPanel8.Controls.Add(this.rdoUnbanJiseiki);
            this.customPanel8.Controls.Add(this.rdoSisetsuJiseiki);
            this.customPanel8.Controls.Add(this.rdoSyobunJiseiki);
            this.customPanel8.Controls.Add(this.txtHokokuSyurui);
            this.customPanel8.Location = new System.Drawing.Point(570, 20);
            this.customPanel8.Name = "customPanel8";
            this.customPanel8.Size = new System.Drawing.Size(378, 20);
            this.customPanel8.TabIndex = 737;
            // 
            // rdoUnbanJiseiki
            // 
            this.rdoUnbanJiseiki.AutoSize = true;
            this.rdoUnbanJiseiki.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoUnbanJiseiki.DisplayItemName = "運搬実績";
            this.rdoUnbanJiseiki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoUnbanJiseiki.FocusOutCheckMethod")));
            this.rdoUnbanJiseiki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoUnbanJiseiki.LinkedTextBox = "txtHokokuSyurui";
            this.rdoUnbanJiseiki.Location = new System.Drawing.Point(271, 1);
            this.rdoUnbanJiseiki.Name = "rdoUnbanJiseiki";
            this.rdoUnbanJiseiki.PopupAfterExecute = null;
            this.rdoUnbanJiseiki.PopupBeforeExecute = null;
            this.rdoUnbanJiseiki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoUnbanJiseiki.PopupSearchSendParams")));
            this.rdoUnbanJiseiki.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoUnbanJiseiki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoUnbanJiseiki.popupWindowSetting")));
            this.rdoUnbanJiseiki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoUnbanJiseiki.RegistCheckMethod")));
            this.rdoUnbanJiseiki.ShortItemName = "運搬実績";
            this.rdoUnbanJiseiki.Size = new System.Drawing.Size(95, 17);
            this.rdoUnbanJiseiki.TabIndex = 734;
            this.rdoUnbanJiseiki.Tag = "運搬実績を対象とする場合選択してください";
            this.rdoUnbanJiseiki.Text = "3.運搬実績";
            this.rdoUnbanJiseiki.UseVisualStyleBackColor = true;
            this.rdoUnbanJiseiki.Value = "3";
            // 
            // rdoSisetsuJiseiki
            // 
            this.rdoSisetsuJiseiki.AutoSize = true;
            this.rdoSisetsuJiseiki.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoSisetsuJiseiki.DisplayItemName = "処理施設実績";
            this.rdoSisetsuJiseiki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSisetsuJiseiki.FocusOutCheckMethod")));
            this.rdoSisetsuJiseiki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoSisetsuJiseiki.LinkedTextBox = "txtHokokuSyurui";
            this.rdoSisetsuJiseiki.Location = new System.Drawing.Point(135, 1);
            this.rdoSisetsuJiseiki.Name = "rdoSisetsuJiseiki";
            this.rdoSisetsuJiseiki.PopupAfterExecute = null;
            this.rdoSisetsuJiseiki.PopupBeforeExecute = null;
            this.rdoSisetsuJiseiki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoSisetsuJiseiki.PopupSearchSendParams")));
            this.rdoSisetsuJiseiki.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoSisetsuJiseiki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoSisetsuJiseiki.popupWindowSetting")));
            this.rdoSisetsuJiseiki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSisetsuJiseiki.RegistCheckMethod")));
            this.rdoSisetsuJiseiki.ShortItemName = "処理施設実績";
            this.rdoSisetsuJiseiki.Size = new System.Drawing.Size(123, 17);
            this.rdoSisetsuJiseiki.TabIndex = 78;
            this.rdoSisetsuJiseiki.Tag = "処理施設実績を対象とする場合選択してください";
            this.rdoSisetsuJiseiki.Text = "2.処理施設実績";
            this.rdoSisetsuJiseiki.UseVisualStyleBackColor = true;
            this.rdoSisetsuJiseiki.Value = "2";
            // 
            // rdoSyobunJiseiki
            // 
            this.rdoSyobunJiseiki.AutoSize = true;
            this.rdoSyobunJiseiki.DefaultBackColor = System.Drawing.Color.Empty;
            this.rdoSyobunJiseiki.DisplayItemName = "処分実績";
            this.rdoSyobunJiseiki.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSyobunJiseiki.FocusOutCheckMethod")));
            this.rdoSyobunJiseiki.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.rdoSyobunJiseiki.LinkedTextBox = "txtHokokuSyurui";
            this.rdoSyobunJiseiki.Location = new System.Drawing.Point(25, 1);
            this.rdoSyobunJiseiki.Name = "rdoSyobunJiseiki";
            this.rdoSyobunJiseiki.PopupAfterExecute = null;
            this.rdoSyobunJiseiki.PopupBeforeExecute = null;
            this.rdoSyobunJiseiki.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("rdoSyobunJiseiki.PopupSearchSendParams")));
            this.rdoSyobunJiseiki.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.rdoSyobunJiseiki.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("rdoSyobunJiseiki.popupWindowSetting")));
            this.rdoSyobunJiseiki.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("rdoSyobunJiseiki.RegistCheckMethod")));
            this.rdoSyobunJiseiki.ShortItemName = "全て";
            this.rdoSyobunJiseiki.Size = new System.Drawing.Size(95, 17);
            this.rdoSyobunJiseiki.TabIndex = 74;
            this.rdoSyobunJiseiki.Tag = "処分実績を対象とする場合選択してください";
            this.rdoSyobunJiseiki.Text = "1.処分実績";
            this.rdoSyobunJiseiki.UseVisualStyleBackColor = true;
            this.rdoSyobunJiseiki.Value = "1";
            // 
            // txtHokokuSyurui
            // 
            this.txtHokokuSyurui.BackColor = System.Drawing.SystemColors.Window;
            this.txtHokokuSyurui.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtHokokuSyurui.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtHokokuSyurui.DisplayItemName = "実績報告書種類";
            this.txtHokokuSyurui.DisplayPopUp = null;
            this.txtHokokuSyurui.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHokokuSyurui.FocusOutCheckMethod")));
            this.txtHokokuSyurui.ForeColor = System.Drawing.Color.Black;
            this.txtHokokuSyurui.IsInputErrorOccured = false;
            this.txtHokokuSyurui.LinkedRadioButtonArray = new string[] {
        "rdoSyobunJiseiki",
        "rdoSisetsuJiseiki",
        "rdoUnbanJiseiki"};
            this.txtHokokuSyurui.Location = new System.Drawing.Point(0, 0);
            this.txtHokokuSyurui.Name = "txtHokokuSyurui";
            this.txtHokokuSyurui.PopupAfterExecute = null;
            this.txtHokokuSyurui.PopupBeforeExecute = null;
            this.txtHokokuSyurui.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtHokokuSyurui.PopupSearchSendParams")));
            this.txtHokokuSyurui.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtHokokuSyurui.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtHokokuSyurui.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHokokuSyurui.RangeSetting = rangeSettingDto1;
            this.txtHokokuSyurui.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtHokokuSyurui.RegistCheckMethod")));
            this.txtHokokuSyurui.Size = new System.Drawing.Size(20, 20);
            this.txtHokokuSyurui.TabIndex = 16;
            this.txtHokokuSyurui.Tag = "実績報告書の種類を選択してください";
            this.txtHokokuSyurui.WordWrap = false;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label14.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(451, 19);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(113, 20);
            this.label14.TabIndex = 738;
            this.label14.Text = "実績報告書種類";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.customPanel8);
            this.Controls.Add(this.label14);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIHeader";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.label14, 0);
            this.Controls.SetChildIndex(this.customPanel8, 0);
            this.customPanel8.ResumeLayout(false);
            this.customPanel8.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        internal CustomNumericTextBox2 txtHokokuSyurui;
        public System.Windows.Forms.Label label14;
        internal CustomPanel customPanel8;
        internal CustomRadioButton rdoUnbanJiseiki;
        internal CustomRadioButton rdoSisetsuJiseiki;
        internal CustomRadioButton rdoSyobunJiseiki;

    }
}