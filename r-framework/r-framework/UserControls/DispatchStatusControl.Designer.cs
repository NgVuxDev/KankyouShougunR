using r_framework.CustomControl;

namespace r_framework.UserControls
{
    partial class DispatchStatusControl
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
            this.配車コンボボックス = new CustomComboBox();
            this.配車テキストボックス = new CustomTextBox();
            this.SuspendLayout();
            // 
            // 配車コンボボックス
            // 
            this.配車コンボボックス.CharactersNumber = 0;
            this.配車コンボボックス.DisplayItemName = null;
            this.配車コンボボックス.ErrorMessage = "";
            this.配車コンボボックス.FocusOutCheckMethod = null;
            this.配車コンボボックス.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.配車コンボボックス.FormattingEnabled = true;
            this.配車コンボボックス.ItemDefinedTypes = null;
            this.配車コンボボックス.Items.AddRange(new object[] {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""});
            this.配車コンボボックス.Location = new System.Drawing.Point(36, 2);
            this.配車コンボボックス.Name = "配車コンボボックス";
            this.配車コンボボックス.PopupWindowName = null;
            this.配車コンボボックス.RegistCheckMethod = null;
            this.配車コンボボックス.SearchDisplayFlag = 0;
            this.配車コンボボックス.ShortItemName = null;
            this.配車コンボボックス.Size = new System.Drawing.Size(91, 21);
            this.配車コンボボックス.TabIndex = 1;
            // 
            // 配車テキストボックス
            // 
            this.配車テキストボックス.CharactersNumber = 32767;
            this.配車テキストボックス.DBFieldsName = null;
            this.配車テキストボックス.DisplayItemName = null;
            this.配車テキストボックス.ErrorMessage = "";
            this.配車テキストボックス.FocusOutCheckMethod = null;
            this.配車テキストボックス.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.配車テキストボックス.ItemDefinedTypes = null;
            this.配車テキストボックス.Location = new System.Drawing.Point(3, 2);
            this.配車テキストボックス.Name = "配車テキストボックス";
            this.配車テキストボックス.PopupWindowName = null;
            this.配車テキストボックス.RegistCheckMethod = null;
            this.配車テキストボックス.SearchDisplayFlag = 0;
            this.配車テキストボックス.ShortItemName = null;
            this.配車テキストボックス.Size = new System.Drawing.Size(30, 20);
            this.配車テキストボックス.TabIndex = 0;
            // 
            // DispatchStatusControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.Controls.Add(this.配車コンボボックス);
            this.Controls.Add(this.配車テキストボックス);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "DispatchStatusControl";
            this.Size = new System.Drawing.Size(129, 23);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomTextBox 配車テキストボックス;
        private CustomComboBox 配車コンボボックス;
    }
}
