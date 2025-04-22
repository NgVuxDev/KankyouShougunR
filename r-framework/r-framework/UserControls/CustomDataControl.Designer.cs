using r_framework.CustomControl;

namespace r_framework.UserControls
{
    partial class CustomDataControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomDataControl));
            this.日付 = new CustomTextBox();
            this.button8 = new System.Windows.Forms.Button();
            this.曜日 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // 日付
            // 
            this.日付.CharactersNumber = 32767;
            this.日付.DBFieldsName = null;
            this.日付.DisplayItemName = null;
            this.日付.ErrorMessage = "";
            this.日付.FocusOutCheckMethod = null;
            this.日付.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.日付.GetCodeMasterField = "";
            this.日付.ItemDefinedTypes = null;
            this.日付.Location = new System.Drawing.Point(1, 2);
            this.日付.Name = "日付";
            this.日付.PopupWindowName = null;
            this.日付.RegistCheckMethod = null;
            this.日付.SearchDisplayFlag = 0;
            this.日付.SetFormField = "";
            this.日付.ShortItemName = null;
            this.日付.Size = new System.Drawing.Size(84, 20);
            this.日付.TabIndex = 0;
            this.日付.TextChanged += new System.EventHandler(this.customTextBox1_TextChanged);
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button8.Image = ((System.Drawing.Image)(resources.GetObject("button8.Image")));
            this.button8.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.button8.Location = new System.Drawing.Point(107, 1);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(22, 22);
            this.button8.TabIndex = 63;
            this.button8.TabStop = false;
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // 曜日
            // 
            this.曜日.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.曜日.Location = new System.Drawing.Point(85, 2);
            this.曜日.Name = "曜日";
            this.曜日.Size = new System.Drawing.Size(20, 20);
            this.曜日.TabIndex = 62;
            // 
            // CustomDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button8);
            this.Controls.Add(this.曜日);
            this.Controls.Add(this.日付);
            this.Name = "CustomDataControl";
            this.Size = new System.Drawing.Size(131, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomTextBox 日付;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.TextBox 曜日;
    }
}
