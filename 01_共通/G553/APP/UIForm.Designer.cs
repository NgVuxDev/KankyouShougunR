namespace Shougun.Core.Common.TruckScaleWeight
{
    partial class UIForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            this.btnSetWeight = new r_framework.CustomControl.CustomButton();
            this.btnProcessWeight = new r_framework.CustomControl.CustomButton();
            this.customTextBox1 = new r_framework.CustomControl.CustomTextBox();
            this.truckScaleWeight1 = new Shougun.Core.Common.TruckScaleWeight.TruckScaleWeight(this.components);
            this.SuspendLayout();
            // 
            // btnSetWeight
            // 
            this.btnSetWeight.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnSetWeight.Location = new System.Drawing.Point(38, 41);
            this.btnSetWeight.Name = "btnSetWeight";
            this.btnSetWeight.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnSetWeight.Size = new System.Drawing.Size(102, 23);
            this.btnSetWeight.TabIndex = 0;
            this.btnSetWeight.Text = "重量値を設定する";
            this.btnSetWeight.UseVisualStyleBackColor = true;
            this.btnSetWeight.Click += new System.EventHandler(this.btnSetWeight_Click);
            // 
            // btnProcessWeight
            // 
            this.btnProcessWeight.DefaultBackColor = System.Drawing.Color.Empty;
            this.btnProcessWeight.Location = new System.Drawing.Point(38, 131);
            this.btnProcessWeight.Name = "btnProcessWeight";
            this.btnProcessWeight.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.btnProcessWeight.Size = new System.Drawing.Size(102, 23);
            this.btnProcessWeight.TabIndex = 0;
            this.btnProcessWeight.Text = "重量値読込み";
            this.btnProcessWeight.UseVisualStyleBackColor = true;
            this.btnProcessWeight.Click += new System.EventHandler(this.btnProcessWeight_Click);
            // 
            // customTextBox1
            // 
            this.customTextBox1.AutoChangeBackColorEnabled = true;
            this.customTextBox1.BackColor = System.Drawing.SystemColors.Window;
            this.customTextBox1.DefaultBackColor = System.Drawing.Color.Empty;
            this.customTextBox1.DisplayPopUp = null;
            this.customTextBox1.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox1.FocusOutCheckMethod")));
            this.customTextBox1.IsInputErrorOccured = false;
            this.customTextBox1.Location = new System.Drawing.Point(38, 87);
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox1.PopupSearchSendParams")));
            this.customTextBox1.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customTextBox1.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox1.popupWindowSetting")));
            this.customTextBox1.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox1.RegistCheckMethod")));
            this.customTextBox1.Size = new System.Drawing.Size(100, 19);
            this.customTextBox1.TabIndex = 1;
            this.customTextBox1.Text = "350";
            // 
            // truckScaleWeight1
            // 
            this.truckScaleWeight1.ContainerControl = this;
            this.truckScaleWeight1.WeightControl = "label1";
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1192, 593);
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.btnProcessWeight);
            this.Controls.Add(this.btnSetWeight);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Load += new System.EventHandler(this.UIForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private r_framework.CustomControl.CustomButton btnSetWeight;
        private TruckScaleWeight truckScaleWeight1;
        private r_framework.CustomControl.CustomButton btnProcessWeight;
        private r_framework.CustomControl.CustomTextBox customTextBox1;




    }
}