namespace Shougun.Printing.Common.UI
{
    partial class MarginsSettingsDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.closeButton = new System.Windows.Forms.Button();
            this.changeButton = new System.Windows.Forms.Button();
            this.topTextBox = new System.Windows.Forms.TextBox();
            this.leftTextBox = new System.Windows.Forms.TextBox();
            this.rightTextBox = new System.Windows.Forms.TextBox();
            this.bottomTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.closeButton.Location = new System.Drawing.Point(320, 225);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(103, 41);
            this.closeButton.TabIndex = 1;
            this.closeButton.TabStop = false;
            this.closeButton.Text = "[F12]\r\nキャンセル";
            this.closeButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // changeButton
            // 
            this.changeButton.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.changeButton.Location = new System.Drawing.Point(193, 225);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(103, 41);
            this.changeButton.TabIndex = 0;
            this.changeButton.TabStop = false;
            this.changeButton.Text = "[F9]  \r\n変更";
            this.changeButton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // topTextBox
            // 
            this.topTextBox.Location = new System.Drawing.Point(182, 81);
            this.topTextBox.Name = "topTextBox";
            this.topTextBox.Size = new System.Drawing.Size(86, 20);
            this.topTextBox.TabIndex = 0;
            this.topTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.topTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.topTextBox.Enter += new System.EventHandler(this.textBox_Enter);
            this.topTextBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.topTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // leftTextBox
            // 
            this.leftTextBox.Location = new System.Drawing.Point(109, 132);
            this.leftTextBox.Name = "leftTextBox";
            this.leftTextBox.Size = new System.Drawing.Size(86, 20);
            this.leftTextBox.TabIndex = 3;
            this.leftTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.leftTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.leftTextBox.Enter += new System.EventHandler(this.textBox_Enter);
            this.leftTextBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.leftTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // rightTextBox
            // 
            this.rightTextBox.Location = new System.Drawing.Point(257, 132);
            this.rightTextBox.Name = "rightTextBox";
            this.rightTextBox.Size = new System.Drawing.Size(86, 20);
            this.rightTextBox.TabIndex = 1;
            this.rightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.rightTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.rightTextBox.Enter += new System.EventHandler(this.textBox_Enter);
            this.rightTextBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.rightTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // bottomTextBox
            // 
            this.bottomTextBox.Location = new System.Drawing.Point(182, 183);
            this.bottomTextBox.Name = "bottomTextBox";
            this.bottomTextBox.Size = new System.Drawing.Size(86, 20);
            this.bottomTextBox.TabIndex = 2;
            this.bottomTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bottomTextBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            this.bottomTextBox.Enter += new System.EventHandler(this.textBox_Enter);
            this.bottomTextBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.bottomTextBox.Validating += new System.ComponentModel.CancelEventHandler(this.textBox_Validating);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(182, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "上";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(185, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "下";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(109, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "左";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(257, 110);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(86, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "右";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 26);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(427, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "単位:mm。範囲:-99.9 ～ 99.9。小数点以下1位まで指定できます。";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MarginsSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 280);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bottomTextBox);
            this.Controls.Add(this.rightTextBox);
            this.Controls.Add(this.leftTextBox);
            this.Controls.Add(this.topTextBox);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.changeButton);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MarginsSettingsDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "余白調整値変更";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button changeButton;
        private System.Windows.Forms.TextBox topTextBox;
        private System.Windows.Forms.TextBox leftTextBox;
        private System.Windows.Forms.TextBox rightTextBox;
        private System.Windows.Forms.TextBox bottomTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}