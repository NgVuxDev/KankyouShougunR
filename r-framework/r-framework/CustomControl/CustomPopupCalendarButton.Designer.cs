namespace r_framework.CustomControl
{
    sealed partial class CustomPopupCalendarButton
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomPopupCalendarButton));
            this.SuspendLayout();
            // 
            // CustomCalendarPopupButton
            // 
            this.Image = ((System.Drawing.Image)(resources.GetObject("CustomPopupCalendarImg")));
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 11.25F);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "button1";
            this.Size = new System.Drawing.Size(22, 21);
            this.TabIndex = 0;
            this.TabStop = false;
            this.UseVisualStyleBackColor = false;
            this.ResumeLayout(false);

        }

        #endregion


    }
}
