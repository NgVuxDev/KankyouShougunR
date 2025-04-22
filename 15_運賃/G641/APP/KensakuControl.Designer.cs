namespace Shougun.Core.Carriage.Unchinichiran.APP
{
    partial class KensakuControl
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KensakuControl));
            this.UNNBANGYOUSYA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.UNNBANGYOUSYA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Gyousha = new r_framework.CustomControl.CustomPopupOpenButton();
            this.SuspendLayout();
            // 
            // UNNBANGYOUSYA_NAME_RYAKU
            // 
            this.UNNBANGYOUSYA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNNBANGYOUSYA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNNBANGYOUSYA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNNBANGYOUSYA_NAME_RYAKU.DisplayPopUp = null;
            this.UNNBANGYOUSYA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNNBANGYOUSYA_NAME_RYAKU.FocusOutCheckMethod")));
            this.UNNBANGYOUSYA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNNBANGYOUSYA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.UNNBANGYOUSYA_NAME_RYAKU.IsInputErrorOccured = false;
            this.UNNBANGYOUSYA_NAME_RYAKU.Location = new System.Drawing.Point(169, 2);
            this.UNNBANGYOUSYA_NAME_RYAKU.Name = "UNNBANGYOUSYA_NAME_RYAKU";
            this.UNNBANGYOUSYA_NAME_RYAKU.PopupAfterExecute = null;
            this.UNNBANGYOUSYA_NAME_RYAKU.PopupBeforeExecute = null;
            this.UNNBANGYOUSYA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNNBANGYOUSYA_NAME_RYAKU.PopupSearchSendParams")));
            this.UNNBANGYOUSYA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNNBANGYOUSYA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNNBANGYOUSYA_NAME_RYAKU.popupWindowSetting")));
            this.UNNBANGYOUSYA_NAME_RYAKU.prevText = null;
            this.UNNBANGYOUSYA_NAME_RYAKU.ReadOnly = true;
            this.UNNBANGYOUSYA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNNBANGYOUSYA_NAME_RYAKU.RegistCheckMethod")));
            this.UNNBANGYOUSYA_NAME_RYAKU.Size = new System.Drawing.Size(285, 20);
            this.UNNBANGYOUSYA_NAME_RYAKU.TabIndex = 8;
            this.UNNBANGYOUSYA_NAME_RYAKU.TabStop = false;
            this.UNNBANGYOUSYA_NAME_RYAKU.Tag = "";
            // 
            // UNNBANGYOUSYA_CD
            // 
            this.UNNBANGYOUSYA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.UNNBANGYOUSYA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNNBANGYOUSYA_CD.ChangeUpperCase = true;
            this.UNNBANGYOUSYA_CD.CharacterLimitList = null;
            this.UNNBANGYOUSYA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.UNNBANGYOUSYA_CD.DBFieldsName = "";
            this.UNNBANGYOUSYA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNNBANGYOUSYA_CD.DisplayItemName = "";
            this.UNNBANGYOUSYA_CD.DisplayPopUp = null;
            this.UNNBANGYOUSYA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNNBANGYOUSYA_CD.FocusOutCheckMethod")));
            this.UNNBANGYOUSYA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNNBANGYOUSYA_CD.ForeColor = System.Drawing.Color.Black;
            this.UNNBANGYOUSYA_CD.GetCodeMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNNBANGYOUSYA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UNNBANGYOUSYA_CD.IsInputErrorOccured = false;
            this.UNNBANGYOUSYA_CD.ItemDefinedTypes = "varchar";
            this.UNNBANGYOUSYA_CD.Location = new System.Drawing.Point(115, 2);
            this.UNNBANGYOUSYA_CD.MaxLength = 6;
            this.UNNBANGYOUSYA_CD.Name = "UNNBANGYOUSYA_CD";
            this.UNNBANGYOUSYA_CD.PopupAfterExecute = null;
            this.UNNBANGYOUSYA_CD.PopupBeforeExecute = null;
            this.UNNBANGYOUSYA_CD.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.UNNBANGYOUSYA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNNBANGYOUSYA_CD.PopupSearchSendParams")));
            this.UNNBANGYOUSYA_CD.PopupSetFormField = "UNNBANGYOUSYA_CD, UNNBANGYOUSYA_NAME_RYAKU";
            this.UNNBANGYOUSYA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNNBANGYOUSYA_CD.PopupWindowName = "検索共通ポップアップ";
            this.UNNBANGYOUSYA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNNBANGYOUSYA_CD.popupWindowSetting")));
            this.UNNBANGYOUSYA_CD.prevText = null;
            this.UNNBANGYOUSYA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNNBANGYOUSYA_CD.RegistCheckMethod")));
            this.UNNBANGYOUSYA_CD.SetFormField = "UNNBANGYOUSYA_CD, UNNBANGYOUSYA_NAME_RYAKU";
            this.UNNBANGYOUSYA_CD.ShortItemName = "運搬業者CD";
            this.UNNBANGYOUSYA_CD.Size = new System.Drawing.Size(55, 20);
            this.UNNBANGYOUSYA_CD.TabIndex = 4;
            this.UNNBANGYOUSYA_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.UNNBANGYOUSYA_CD.ZeroPaddengFlag = true;
            this.UNNBANGYOUSYA_CD.Validated += new System.EventHandler(this.UNNBANGYOUSYA_CD_Validated);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "運搬業者";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Gyousha
            // 
            this.btn_Gyousha.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.btn_Gyousha.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.btn_Gyousha.DBFieldsName = null;
            this.btn_Gyousha.DefaultBackColor = System.Drawing.Color.Empty;
            this.btn_Gyousha.DisplayItemName = null;
            this.btn_Gyousha.DisplayPopUp = null;
            this.btn_Gyousha.ErrorMessage = null;
            this.btn_Gyousha.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btn_Gyousha.FocusOutCheckMethod")));
            this.btn_Gyousha.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.btn_Gyousha.GetCodeMasterField = null;
            this.btn_Gyousha.Image = ((System.Drawing.Image)(resources.GetObject("btn_Gyousha.Image")));
            this.btn_Gyousha.ItemDefinedTypes = null;
            this.btn_Gyousha.LinkedTextBoxs = null;
            this.btn_Gyousha.Location = new System.Drawing.Point(456, 1);
            this.btn_Gyousha.Name = "btn_Gyousha";
            this.btn_Gyousha.PopupAfterExecute = null;
            this.btn_Gyousha.PopupAfterExecuteMethod = "";
            this.btn_Gyousha.PopupBeforeExecute = null;
            this.btn_Gyousha.PopupBeforeExecuteMethod = "";
            this.btn_Gyousha.PopupGetMasterField = "GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.btn_Gyousha.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("btn_Gyousha.PopupSearchSendParams")));
            this.btn_Gyousha.PopupSetFormField = "UNNBANGYOUSYA_CD, UNNBANGYOUSYA_NAME_RYAKU";
            this.btn_Gyousha.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.btn_Gyousha.PopupWindowName = "検索共通ポップアップ";
            this.btn_Gyousha.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("btn_Gyousha.popupWindowSetting")));
            this.btn_Gyousha.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("btn_Gyousha.RegistCheckMethod")));
            this.btn_Gyousha.SearchDisplayFlag = 0;
            this.btn_Gyousha.SetFormField = null;
            this.btn_Gyousha.ShortItemName = null;
            this.btn_Gyousha.Size = new System.Drawing.Size(22, 22);
            this.btn_Gyousha.TabIndex = 10;
            this.btn_Gyousha.TabStop = false;
            this.btn_Gyousha.Tag = "業者を指定してください";
            this.btn_Gyousha.UseVisualStyleBackColor = false;
            this.btn_Gyousha.ZeroPaddengFlag = false;
            // 
            // KensakuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_Gyousha);
            this.Controls.Add(this.UNNBANGYOUSYA_NAME_RYAKU);
            this.Controls.Add(this.UNNBANGYOUSYA_CD);
            this.Controls.Add(this.label1);
            this.Name = "KensakuControl";
            this.Size = new System.Drawing.Size(671, 140);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox UNNBANGYOUSYA_NAME_RYAKU;
        public r_framework.CustomControl.CustomAlphaNumTextBox UNNBANGYOUSYA_CD;
        internal System.Windows.Forms.Label label1;
        public r_framework.CustomControl.CustomPopupOpenButton btn_Gyousha;
    }
}
