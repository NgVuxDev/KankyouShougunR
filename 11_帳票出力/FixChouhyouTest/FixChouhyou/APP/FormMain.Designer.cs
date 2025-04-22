namespace FixChouhyou
{
    partial class FormMain
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
            this.buttonDisplay = new System.Windows.Forms.Button();
            this.buttonClose = new System.Windows.Forms.Button();
            this.labelChouhyoumei = new System.Windows.Forms.Label();
            this.panelParam = new System.Windows.Forms.Panel();
            this.textBoxParam3 = new System.Windows.Forms.TextBox();
            this.labelParam3 = new System.Windows.Forms.Label();
            this.textBoxParam2 = new System.Windows.Forms.TextBox();
            this.labelParam2 = new System.Windows.Forms.Label();
            this.textBoxParam1 = new System.Windows.Forms.TextBox();
            this.labelParam1 = new System.Windows.Forms.Label();
            this.checkBoxSampleData = new System.Windows.Forms.CheckBox();
            this.panelParam.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonDisplay
            // 
            this.buttonDisplay.Location = new System.Drawing.Point(12, 35);
            this.buttonDisplay.Name = "buttonDisplay";
            this.buttonDisplay.Size = new System.Drawing.Size(75, 44);
            this.buttonDisplay.TabIndex = 0;
            this.buttonDisplay.Text = "表示";
            this.buttonDisplay.UseVisualStyleBackColor = true;
            this.buttonDisplay.Click += new System.EventHandler(this.ButtonDisplay_Click);
            // 
            // buttonClose
            // 
            this.buttonClose.Location = new System.Drawing.Point(159, 35);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(75, 44);
            this.buttonClose.TabIndex = 1;
            this.buttonClose.Text = "閉じる";
            this.buttonClose.UseVisualStyleBackColor = true;
            this.buttonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // labelChouhyoumei
            // 
            this.labelChouhyoumei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelChouhyoumei.Location = new System.Drawing.Point(12, 9);
            this.labelChouhyoumei.Name = "labelChouhyoumei";
            this.labelChouhyoumei.Size = new System.Drawing.Size(222, 23);
            this.labelChouhyoumei.TabIndex = 2;
            this.labelChouhyoumei.Text = "ラベル";
            this.labelChouhyoumei.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelParam
            // 
            this.panelParam.Controls.Add(this.checkBoxSampleData);
            this.panelParam.Controls.Add(this.textBoxParam3);
            this.panelParam.Controls.Add(this.labelParam3);
            this.panelParam.Controls.Add(this.textBoxParam2);
            this.panelParam.Controls.Add(this.labelParam2);
            this.panelParam.Controls.Add(this.textBoxParam1);
            this.panelParam.Controls.Add(this.labelParam1);
            this.panelParam.Location = new System.Drawing.Point(241, 9);
            this.panelParam.Name = "panelParam";
            this.panelParam.Size = new System.Drawing.Size(314, 102);
            this.panelParam.TabIndex = 3;
            this.panelParam.Visible = false;
            // 
            // textBoxParam3
            // 
            this.textBoxParam3.Location = new System.Drawing.Point(212, 76);
            this.textBoxParam3.Name = "textBoxParam3";
            this.textBoxParam3.Size = new System.Drawing.Size(89, 19);
            this.textBoxParam3.TabIndex = 5;
            this.textBoxParam3.Visible = false;
            // 
            // labelParam3
            // 
            this.labelParam3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelParam3.Location = new System.Drawing.Point(10, 77);
            this.labelParam3.Name = "labelParam3";
            this.labelParam3.Size = new System.Drawing.Size(196, 18);
            this.labelParam3.TabIndex = 4;
            this.labelParam3.Text = "パラメーター3：";
            this.labelParam3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelParam3.Visible = false;
            // 
            // textBoxParam2
            // 
            this.textBoxParam2.Location = new System.Drawing.Point(212, 51);
            this.textBoxParam2.Name = "textBoxParam2";
            this.textBoxParam2.Size = new System.Drawing.Size(89, 19);
            this.textBoxParam2.TabIndex = 3;
            this.textBoxParam2.Visible = false;
            // 
            // labelParam2
            // 
            this.labelParam2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelParam2.Location = new System.Drawing.Point(10, 52);
            this.labelParam2.Name = "labelParam2";
            this.labelParam2.Size = new System.Drawing.Size(196, 18);
            this.labelParam2.TabIndex = 2;
            this.labelParam2.Text = "パラメーター2：";
            this.labelParam2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelParam2.Visible = false;
            // 
            // textBoxParam1
            // 
            this.textBoxParam1.Location = new System.Drawing.Point(212, 26);
            this.textBoxParam1.Name = "textBoxParam1";
            this.textBoxParam1.Size = new System.Drawing.Size(89, 19);
            this.textBoxParam1.TabIndex = 1;
            this.textBoxParam1.Visible = false;
            // 
            // labelParam1
            // 
            this.labelParam1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.labelParam1.Location = new System.Drawing.Point(10, 27);
            this.labelParam1.Name = "labelParam1";
            this.labelParam1.Size = new System.Drawing.Size(196, 18);
            this.labelParam1.TabIndex = 0;
            this.labelParam1.Text = "パラメーター1：";
            this.labelParam1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelParam1.Visible = false;
            // 
            // checkBoxSampleData
            // 
            this.checkBoxSampleData.AutoSize = true;
            this.checkBoxSampleData.Checked = true;
            this.checkBoxSampleData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSampleData.Location = new System.Drawing.Point(12, 7);
            this.checkBoxSampleData.Name = "checkBoxSampleData";
            this.checkBoxSampleData.Size = new System.Drawing.Size(114, 16);
            this.checkBoxSampleData.TabIndex = 6;
            this.checkBoxSampleData.Text = "サンプルデータ使用";
            this.checkBoxSampleData.UseVisualStyleBackColor = true;
            this.checkBoxSampleData.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 123);
            this.Controls.Add(this.panelParam);
            this.Controls.Add(this.labelChouhyoumei);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.buttonDisplay);
            this.Name = "FormMain";
            this.Text = "UIForm";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.panelParam.ResumeLayout(false);
            this.panelParam.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonDisplay;
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Label labelChouhyoumei;
        private System.Windows.Forms.Panel panelParam;
        private System.Windows.Forms.TextBox textBoxParam3;
        private System.Windows.Forms.Label labelParam3;
        private System.Windows.Forms.TextBox textBoxParam2;
        private System.Windows.Forms.Label labelParam2;
        private System.Windows.Forms.TextBox textBoxParam1;
        private System.Windows.Forms.Label labelParam1;
        private System.Windows.Forms.CheckBox checkBoxSampleData;
    }
}