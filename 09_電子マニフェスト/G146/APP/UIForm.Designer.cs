namespace Shougun.Core.ElectronicManifest.SousinHoryuSaisyuSyobunhoukoku
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
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.searchString.Location = new System.Drawing.Point(3, 4);
            this.searchString.ReadOnly = true;
            this.searchString.Size = new System.Drawing.Size(997, 152);
            this.searchString.TabStop = false;
            this.searchString.Tag = "検索条件設定画面で設定した条件が表示されます";
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(1, 449);
            this.bt_ptn1.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn1.TabIndex = 5;
            this.bt_ptn1.Text = "パターン1";
            this.bt_ptn1.Click += new System.EventHandler(this.bt_ptn1_Click);
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn2.Location = new System.Drawing.Point(201, 449);
            this.bt_ptn2.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn2.TabIndex = 6;
            this.bt_ptn2.Text = "パターン2";
            this.bt_ptn2.Click += new System.EventHandler(this.bt_ptn2_Click);
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(401, 449);
            this.bt_ptn3.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn3.TabIndex = 7;
            this.bt_ptn3.Text = "パターン3";
            this.bt_ptn3.Click += new System.EventHandler(this.bt_ptn3_Click);
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(601, 449);
            this.bt_ptn4.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn4.TabIndex = 8;
            this.bt_ptn4.Text = "パターン4";
            this.bt_ptn4.Click += new System.EventHandler(this.bt_ptn4_Click);
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(801, 449);
            this.bt_ptn5.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn5.TabIndex = 9;
            this.bt_ptn5.Text = "パターン5";
            this.bt_ptn5.Click += new System.EventHandler(this.bt_ptn5_Click);
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(3, 4);
            this.customSortHeader1.Size = new System.Drawing.Size(1000, 24);
            this.customSortHeader1.TabIndex = 3;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 490);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

    }
}