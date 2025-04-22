using r_framework.CustomControl;

namespace r_framework.UserControls
{
    partial class ReceptionDateControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ReceptionDateControl));
            this.customTextBox1 = new CustomTextBox();
            this.customTextBox2 = new CustomTextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            this.customTextBox1.Location = new System.Drawing.Point(2, 0);
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.PopupWindowName = null;
            this.customTextBox1.RegistCheckMethod = null;
            this.customTextBox1.SearchDisplayFlag = 0;
            this.customTextBox1.ShortItemName = null;
            this.customTextBox1.Size = new System.Drawing.Size(84, 20);
            this.customTextBox1.TabIndex = 0;
            // 
            // customTextBox2
            // 
            this.customTextBox2.CharactersNumber = 32767;
            this.customTextBox2.DBFieldsName = null;
            this.customTextBox2.DisplayItemName = null;
            this.customTextBox2.ErrorMessage = "";
            this.customTextBox2.FocusOutCheckMethod = null;
            this.customTextBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.customTextBox2.ItemDefinedTypes = null;
            this.customTextBox2.Location = new System.Drawing.Point(86, 0);
            this.customTextBox2.Name = "customTextBox2";
            this.customTextBox2.PopupWindowName = null;
            this.customTextBox2.RegistCheckMethod = null;
            this.customTextBox2.SearchDisplayFlag = 0;
            this.customTextBox2.ShortItemName = null;
            this.customTextBox2.Size = new System.Drawing.Size(20, 20);
            this.customTextBox2.TabIndex = 1;
            // 
            // button4
            // 
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.button4.Location = new System.Drawing.Point(107, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(22, 22);
            this.button4.TabIndex = 167;
            this.button4.UseVisualStyleBackColor = true;
            // 
            // ReceptionDateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button4);
            this.Controls.Add(this.customTextBox2);
            this.Controls.Add(this.customTextBox1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "ReceptionDateControl";
            this.Size = new System.Drawing.Size(130, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomTextBox customTextBox1;
        private CustomTextBox customTextBox2;
        private System.Windows.Forms.Button button4;
    }
}
