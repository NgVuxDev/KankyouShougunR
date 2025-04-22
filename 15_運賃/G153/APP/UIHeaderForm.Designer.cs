namespace Shougun.Core.Carriage.UnchinNyuuRyoku
{
    partial class UIHeaderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeaderForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.txt_KYOTEN_CD = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txt_KYOTEN_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Text = "新規";
            // 
            // lb_title
            // 
            this.lb_title.Size = new System.Drawing.Size(178, 34);
            this.lb_title.Text = "運賃入力";
            // 
            // txt_KYOTEN_CD
            // 
            this.txt_KYOTEN_CD.BackColor = System.Drawing.SystemColors.Window;
            this.txt_KYOTEN_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KYOTEN_CD.CustomFormatSetting = "00";
            this.txt_KYOTEN_CD.DBFieldsName = "KYOTEN_CD";
            this.txt_KYOTEN_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KYOTEN_CD.DisplayItemName = "拠点CD";
            this.txt_KYOTEN_CD.DisplayPopUp = null;
            this.txt_KYOTEN_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KYOTEN_CD.FocusOutCheckMethod")));
            this.txt_KYOTEN_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_KYOTEN_CD.ForeColor = System.Drawing.Color.Black;
            this.txt_KYOTEN_CD.FormatSetting = "カスタム";
            this.txt_KYOTEN_CD.GetCodeMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txt_KYOTEN_CD.IsInputErrorOccured = false;
            this.txt_KYOTEN_CD.ItemDefinedTypes = "varchar";
            this.txt_KYOTEN_CD.Location = new System.Drawing.Point(555, 2);
            this.txt_KYOTEN_CD.Name = "txt_KYOTEN_CD";
            this.txt_KYOTEN_CD.PopupAfterExecute = null;
            this.txt_KYOTEN_CD.PopupBeforeExecute = null;
            this.txt_KYOTEN_CD.PopupGetMasterField = "KYOTEN_CD,KYOTEN_NAME_RYAKU";
            this.txt_KYOTEN_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KYOTEN_CD.PopupSearchSendParams")));
            this.txt_KYOTEN_CD.PopupSetFormField = "txt_KYOTEN_CD,txt_KYOTEN_NAME_RYAKU";
            this.txt_KYOTEN_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_KYOTEN;
            this.txt_KYOTEN_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.txt_KYOTEN_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KYOTEN_CD.popupWindowSetting")));
            rangeSettingDto1.Max = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.txt_KYOTEN_CD.RangeSetting = rangeSettingDto1;
            this.txt_KYOTEN_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KYOTEN_CD.RegistCheckMethod")));
            this.txt_KYOTEN_CD.SetFormField = "txt_KYOTEN_CD,txt_KYOTEN_NAME_RYAKU";
            this.txt_KYOTEN_CD.Size = new System.Drawing.Size(30, 20);
            this.txt_KYOTEN_CD.TabIndex = 401;
            this.txt_KYOTEN_CD.Tag = "半角2桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.txt_KYOTEN_CD.WordWrap = false;
            // 
            // txt_KYOTEN_NAME_RYAKU
            // 
            this.txt_KYOTEN_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_KYOTEN_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KYOTEN_NAME_RYAKU.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.txt_KYOTEN_NAME_RYAKU.DBFieldsName = "TORIHIKISAKI_NAME_RYAKU";
            this.txt_KYOTEN_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.txt_KYOTEN_NAME_RYAKU.DisplayItemName = "";
            this.txt_KYOTEN_NAME_RYAKU.DisplayPopUp = null;
            this.txt_KYOTEN_NAME_RYAKU.ErrorMessage = "";
            this.txt_KYOTEN_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KYOTEN_NAME_RYAKU.FocusOutCheckMethod")));
            this.txt_KYOTEN_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txt_KYOTEN_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.txt_KYOTEN_NAME_RYAKU.GetCodeMasterField = "";
            this.txt_KYOTEN_NAME_RYAKU.IsInputErrorOccured = false;
            this.txt_KYOTEN_NAME_RYAKU.ItemDefinedTypes = "";
            this.txt_KYOTEN_NAME_RYAKU.Location = new System.Drawing.Point(587, 2);
            this.txt_KYOTEN_NAME_RYAKU.MaxLength = 20;
            this.txt_KYOTEN_NAME_RYAKU.Name = "txt_KYOTEN_NAME_RYAKU";
            this.txt_KYOTEN_NAME_RYAKU.PopupAfterExecute = null;
            this.txt_KYOTEN_NAME_RYAKU.PopupBeforeExecute = null;
            this.txt_KYOTEN_NAME_RYAKU.PopupGetMasterField = "";
            this.txt_KYOTEN_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KYOTEN_NAME_RYAKU.PopupSearchSendParams")));
            this.txt_KYOTEN_NAME_RYAKU.PopupSetFormField = "";
            this.txt_KYOTEN_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_KYOTEN_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KYOTEN_NAME_RYAKU.popupWindowSetting")));
            this.txt_KYOTEN_NAME_RYAKU.ReadOnly = true;
            this.txt_KYOTEN_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KYOTEN_NAME_RYAKU.RegistCheckMethod")));
            this.txt_KYOTEN_NAME_RYAKU.SetFormField = "";
            this.txt_KYOTEN_NAME_RYAKU.Size = new System.Drawing.Size(160, 20);
            this.txt_KYOTEN_NAME_RYAKU.TabIndex = 404;
            this.txt_KYOTEN_NAME_RYAKU.TabStop = false;
            this.txt_KYOTEN_NAME_RYAKU.Tag = "　";
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(444, 2);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 403;
            this.TORIHIKISAKI_LABEL.Text = "拠点※";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(490, 25);
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
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 10028;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // UIHeaderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 46);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.txt_KYOTEN_CD);
            this.Controls.Add(this.txt_KYOTEN_NAME_RYAKU);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Name = "UIHeaderForm";
            this.Text = "UIHeaderForm";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.TORIHIKISAKI_LABEL, 0);
            this.Controls.SetChildIndex(this.txt_KYOTEN_NAME_RYAKU, 0);
            this.Controls.SetChildIndex(this.txt_KYOTEN_CD, 0);
            this.Controls.SetChildIndex(this.ISNOT_NEED_DELETE_FLG, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomNumericTextBox2 txt_KYOTEN_CD;
        internal r_framework.CustomControl.CustomTextBox txt_KYOTEN_NAME_RYAKU;
        internal System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;

    }
}