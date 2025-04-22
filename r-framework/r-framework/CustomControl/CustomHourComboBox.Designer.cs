namespace r_framework.CustomControl
{
    sealed partial class CustomHourComboBox
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
            this.SuspendLayout();
            // 
            // CustomHourComboBox
            // 
            this.CharactersNumber = 2;
            this.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DropDownWidth = 42;
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.FormattingEnabled = true;
            this.ItemDefinedTypes = "varchar";
            this.MaxLength = 2;
            this.Size = new System.Drawing.Size(42, 20);
            this.Tag = "";
            this.ResumeLayout(false);

        }

        #endregion

    }
}
