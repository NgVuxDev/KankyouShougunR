namespace Shougun.Core.PaperManifest.ManifestSyuuryoubiIchiran
{
    partial class UIForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this.customSearchHeader1 = new r_framework.CustomControl.DataGridCustomControl.CustomSearchHeader();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.searchString.ForeColor = System.Drawing.Color.Black;
            this.searchString.Location = new System.Drawing.Point(749, 1);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(179, 20);
            this.searchString.TabStop = false;
            this.searchString.Tag = "検索条件設定画面で設定した条件が表示されます";
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Location = new System.Drawing.Point(3, 410);
            this.bt_ptn1.Visible = false;
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Location = new System.Drawing.Point(204, 410);
            this.bt_ptn2.Visible = false;
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Location = new System.Drawing.Point(405, 410);
            this.bt_ptn3.Visible = false;
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Location = new System.Drawing.Point(606, 410);
            this.bt_ptn4.Visible = false;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Location = new System.Drawing.Point(807, 410);
            this.bt_ptn5.Visible = false;
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(3, 38);
            this.customSortHeader1.Size = new System.Drawing.Size(997, 25);
            this.customSortHeader1.TabIndex = 21;
            // 
            // customSearchHeader1
            // 
            this.customSearchHeader1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.customSearchHeader1.LinkedDataGridViewName = "customDataGridView1";
            this.customSearchHeader1.Location = new System.Drawing.Point(3, 12);
            this.customSearchHeader1.Name = "customSearchHeader1";
            this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);
            this.customSearchHeader1.TabIndex = 22;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.customSearchHeader1);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.DataGridCustomControl.CustomSearchHeader customSearchHeader1;

    }
}