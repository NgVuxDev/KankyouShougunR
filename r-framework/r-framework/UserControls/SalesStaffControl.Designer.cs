using r_framework.CustomControl;

namespace CustomControl.UserControls
{
    partial class SalesStaffControl
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
            this.取引先名1 = new CustomTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // 取引先名1
            // 
            this.取引先名1.CharactersNumber = 32767;
            this.取引先名1.DBFieldsName = null;
            this.取引先名1.DisplayItemName = null;
            this.取引先名1.ErrorMessage = "";
            this.取引先名1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.取引先名1.ItemDefinedTypes = null;
            this.取引先名1.Location = new System.Drawing.Point(2, 0);
            this.取引先名1.Name = "取引先名1";
            this.取引先名1.PopupWindowName = null;
            this.取引先名1.RegistCheckMethod = null;
            this.取引先名1.SearchDisplayFlag = 0;
            this.取引先名1.ShortItemName = null;
            this.取引先名1.Size = new System.Drawing.Size(49, 20);
            this.取引先名1.TabIndex = 0;
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(53, 1);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(114, 20);
            this.textBox1.TabIndex = 1;
            this.textBox1.Tag = "担当者名";
            // 
            // SalesStaffControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.取引先名1);
            this.Name = "SalesStaffControl";
            this.Size = new System.Drawing.Size(170, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomTextBox 取引先名1;
        private System.Windows.Forms.TextBox textBox1;
    }
}
