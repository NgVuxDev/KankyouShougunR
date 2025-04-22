namespace r_framework.CustomControl.DataGridCustomControl
{
    partial class CustomSortHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomSortHeader));
            this.label3 = new System.Windows.Forms.Label();
            this.txboxSortSettingInfo = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(0, 2);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "並び順";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txboxSortSettingInfo
            // 
            this.txboxSortSettingInfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txboxSortSettingInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txboxSortSettingInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txboxSortSettingInfo.DefaultBackColor = System.Drawing.Color.Empty;
            this.txboxSortSettingInfo.DisplayPopUp = null;
            this.txboxSortSettingInfo.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txboxSortSettingInfo.FocusOutCheckMethod")));
            this.txboxSortSettingInfo.Location = new System.Drawing.Point(113, 2);
            this.txboxSortSettingInfo.Name = "txboxSortSettingInfo";
            this.txboxSortSettingInfo.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txboxSortSettingInfo.PopupSearchSendParams")));
            this.txboxSortSettingInfo.PopupWindowId = r_framework.Const.WINDOW_ID.NONE;
            this.txboxSortSettingInfo.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txboxSortSettingInfo.popupWindowSetting")));
            this.txboxSortSettingInfo.ReadOnly = true;
            this.txboxSortSettingInfo.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txboxSortSettingInfo.RegistCheckMethod")));
            this.txboxSortSettingInfo.Size = new System.Drawing.Size(734, 20);
            this.txboxSortSettingInfo.TabIndex = 0;
            this.txboxSortSettingInfo.TabStop = false;
            this.txboxSortSettingInfo.WordWrap = false;
            // 
            // CustomSortHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txboxSortSettingInfo);
            this.Controls.Add(this.label3);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "CustomSortHeader";
            this.Size = new System.Drawing.Size(847, 26);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Label label3;
        public CustomTextBox txboxSortSettingInfo;



    }
}
