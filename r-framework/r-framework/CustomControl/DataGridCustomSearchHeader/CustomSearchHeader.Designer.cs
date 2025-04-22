namespace r_framework.CustomControl.DataGridCustomControl
{
    partial class CustomSearchHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomSearchHeader));
            this.label1 = new System.Windows.Forms.Label();
            this.txboxSearchSettingInfo = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "フィルタ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txboxSearchSettingInfo
            // 
            this.txboxSearchSettingInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txboxSearchSettingInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txboxSearchSettingInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txboxSearchSettingInfo.DefaultBackColor = System.Drawing.Color.Empty;
            this.txboxSearchSettingInfo.DisplayPopUp = null;
            this.txboxSearchSettingInfo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txboxSearchSettingInfo.FocusOutCheckMethod")));
            this.txboxSearchSettingInfo.ForeColor = System.Drawing.Color.Black;
            this.txboxSearchSettingInfo.IsInputErrorOccured = false;
            this.txboxSearchSettingInfo.Location = new System.Drawing.Point(113, 2);
            this.txboxSearchSettingInfo.Name = "txboxSearchSettingInfo";
            this.txboxSearchSettingInfo.PopupAfterExecute = null;
            this.txboxSearchSettingInfo.PopupBeforeExecute = null;
            this.txboxSearchSettingInfo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txboxSearchSettingInfo.PopupSearchSendParams")));
            this.txboxSearchSettingInfo.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.txboxSearchSettingInfo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txboxSearchSettingInfo.popupWindowSetting")));
            this.txboxSearchSettingInfo.prevText = null;
            this.txboxSearchSettingInfo.ReadOnly = true;
            this.txboxSearchSettingInfo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txboxSearchSettingInfo.RegistCheckMethod")));
            this.txboxSearchSettingInfo.Size = new System.Drawing.Size(734, 20);
            this.txboxSearchSettingInfo.TabIndex = 1;
            this.txboxSearchSettingInfo.TabStop = false;
            this.txboxSearchSettingInfo.WordWrap = false;
            // 
            // CustomSearchHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txboxSearchSettingInfo);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "CustomSearchHeader";
            this.Size = new System.Drawing.Size(847, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label1;
        public CustomTextBox txboxSearchSettingInfo;

    }
}
