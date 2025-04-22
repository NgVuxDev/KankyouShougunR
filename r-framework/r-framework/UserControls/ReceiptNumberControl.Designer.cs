using r_framework.CustomControl;

namespace r_framework.UserControls
{
    partial class ReceiptNumberControl
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
            this.button27 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.customTextBox1 = new CustomTextBox();
            this.SuspendLayout();
            // 
            // button27
            // 
            this.button27.Location = new System.Drawing.Point(162, 1);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(22, 22);
            this.button27.TabIndex = 172;
            this.button27.Text = "次";
            this.button27.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(141, 1);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(22, 22);
            this.button5.TabIndex = 171;
            this.button5.Text = "前";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // customTextBox1
            // 
            this.customTextBox1.CharactersNumber = 32767;
            this.customTextBox1.DBFieldsName = null;
            this.customTextBox1.DisplayItemName = null;
            this.customTextBox1.ErrorMessage = "";
            this.customTextBox1.FocusOutCheckMethod = null;
            this.customTextBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.customTextBox1.ItemDefinedTypes = null;
            this.customTextBox1.Location = new System.Drawing.Point(2, 2);
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.PopupWindowName = null;
            this.customTextBox1.RegistCheckMethod = null;
            this.customTextBox1.SearchDisplayFlag = 0;
            this.customTextBox1.ShortItemName = null;
            this.customTextBox1.Size = new System.Drawing.Size(138, 20);
            this.customTextBox1.TabIndex = 173;
            // 
            // ReceiptNumberControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.button27);
            this.Controls.Add(this.button5);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "ReceiptNumberControl";
            this.Size = new System.Drawing.Size(186, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button27;
        private System.Windows.Forms.Button button5;
        private CustomTextBox customTextBox1;
    }
}
