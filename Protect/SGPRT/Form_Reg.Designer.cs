namespace SGPRT
{
    partial class Form_Reg
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
            this.Close = new System.Windows.Forms.Button();
            this.Run = new System.Windows.Forms.Button();
            this.Protect_Key = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Protect_Lock = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCopy = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Close
            // 
            this.Close.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Close.Location = new System.Drawing.Point(270, 220);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(70, 40);
            this.Close.TabIndex = 6;
            this.Close.Text = "閉じる";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Run
            // 
            this.Run.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Run.Location = new System.Drawing.Point(173, 220);
            this.Run.Name = "Run";
            this.Run.Size = new System.Drawing.Size(70, 40);
            this.Run.TabIndex = 5;
            this.Run.Text = "登録";
            this.Run.UseVisualStyleBackColor = true;
            this.Run.Click += new System.EventHandler(this.Run_Click);
            // 
            // Protect_Key
            // 
            this.Protect_Key.AllowDrop = true;
            this.Protect_Key.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Protect_Key.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.Protect_Key.Location = new System.Drawing.Point(40, 150);
            this.Protect_Key.Name = "Protect_Key";
            this.Protect_Key.Size = new System.Drawing.Size(300, 23);
            this.Protect_Key.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(40, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "解除キー";
            // 
            // Protect_Lock
            // 
            this.Protect_Lock.AllowDrop = true;
            this.Protect_Lock.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Protect_Lock.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Protect_Lock.Location = new System.Drawing.Point(40, 50);
            this.Protect_Lock.Name = "Protect_Lock";
            this.Protect_Lock.ReadOnly = true;
            this.Protect_Lock.Size = new System.Drawing.Size(300, 23);
            this.Protect_Lock.TabIndex = 1;
            this.Protect_Lock.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(40, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "ユーザーID";
            // 
            // btnCopy
            // 
            this.btnCopy.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnCopy.Location = new System.Drawing.Point(190, 80);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(150, 26);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.TabStop = false;
            this.btnCopy.Text = "クリップボードにコピー";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // Form_Reg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(380, 280);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.Protect_Lock);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.Close);
            this.Controls.Add(this.Run);
            this.Controls.Add(this.Protect_Key);
            this.Controls.Add(this.label1);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_Reg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ユーザー登録";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form_Reg_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form_Reg_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button Run;
        private System.Windows.Forms.TextBox Protect_Key;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Protect_Lock;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCopy;
    }
}

