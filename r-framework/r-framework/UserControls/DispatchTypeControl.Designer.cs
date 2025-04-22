using r_framework.CustomControl;

namespace r_framework.UserControls
{
    partial class DispatchTypeControl
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
            this.配車状況コンボボックス = new CustomComboBox();
            this.配車状況テキストボックス = new CustomTextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // 配車状況コンボボックス
            // 
            this.配車状況コンボボックス.CharactersNumber = 0;
            this.配車状況コンボボックス.DBFieldsName = null;
            this.配車状況コンボボックス.DisplayItemName = null;
            this.配車状況コンボボックス.ErrorMessage = "";
            this.配車状況コンボボックス.FocusOutCheckMethod = null;
            this.配車状況コンボボックス.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.配車状況コンボボックス.FormattingEnabled = true;
            this.配車状況コンボボックス.ItemDefinedTypes = null;
            this.配車状況コンボボックス.Items.AddRange(new object[] {
            ""});
            this.配車状況コンボボックス.Location = new System.Drawing.Point(117, 1);
            this.配車状況コンボボックス.Name = "配車状況コンボボックス";
            this.配車状況コンボボックス.PopupWindowName = null;
            this.配車状況コンボボックス.RegistCheckMethod = null;
            this.配車状況コンボボックス.SearchDisplayFlag = 0;
            this.配車状況コンボボックス.ShortItemName = null;
            this.配車状況コンボボックス.Size = new System.Drawing.Size(106, 21);
            this.配車状況コンボボックス.TabIndex = 3;
            // 
            // 配車状況テキストボックス
            // 
            this.配車状況テキストボックス.CharactersNumber = 32767;
            this.配車状況テキストボックス.DBFieldsName = null;
            this.配車状況テキストボックス.DisplayItemName = null;
            this.配車状況テキストボックス.ErrorMessage = "";
            this.配車状況テキストボックス.FocusOutCheckMethod = null;
            this.配車状況テキストボックス.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.配車状況テキストボックス.ItemDefinedTypes = null;
            this.配車状況テキストボックス.Location = new System.Drawing.Point(95, 1);
            this.配車状況テキストボックス.Name = "配車状況テキストボックス";
            this.配車状況テキストボックス.PopupWindowName = null;
            this.配車状況テキストボックス.RegistCheckMethod = null;
            this.配車状況テキストボックス.SearchDisplayFlag = 0;
            this.配車状況テキストボックス.ShortItemName = null;
            this.配車状況テキストボックス.Size = new System.Drawing.Size(20, 20);
            this.配車状況テキストボックス.TabIndex = 2;
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.DodgerBlue;
            this.label17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label17.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(1, 1);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(93, 20);
            this.label17.TabIndex = 189;
            this.label17.Text = "配車種類";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DispatchTypeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label17);
            this.Controls.Add(this.配車状況コンボボックス);
            this.Controls.Add(this.配車状況テキストボックス);
            this.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "DispatchTypeControl";
            this.Size = new System.Drawing.Size(224, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CustomComboBox 配車状況コンボボックス;
        private CustomTextBox 配車状況テキストボックス;
        private System.Windows.Forms.Label label17;
    }
}
