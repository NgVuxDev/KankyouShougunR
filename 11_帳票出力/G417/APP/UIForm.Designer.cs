namespace Shougun.Core.Common.MeisaihyoSyukeihyoPatternSentakuPopup
{
    partial class G417_MeisaihyoSyukeihyoPatternSentakuPopupForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(G417_MeisaihyoSyukeihyoPatternSentakuPopupForm));
            this.lb_title = new System.Windows.Forms.Label();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_func5 = new r_framework.CustomControl.CustomButton();
            this.bt_func4 = new r_framework.CustomControl.CustomButton();
            this.bt_func3 = new r_framework.CustomControl.CustomButton();
            this.bt_func2 = new r_framework.CustomControl.CustomButton();
            this.label1 = new System.Windows.Forms.Label();
            this.customListBox = new r_framework.CustomControl.CustomListBox();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20F, System.Drawing.FontStyle.Bold);
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(12, 9);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(404, 31);
            this.lb_title.TabIndex = 0;
            this.lb_title.Text = "***明細表";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func12.Location = new System.Drawing.Point(336, 236);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 7;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "func12";
            this.bt_func12.Text = "[F12]\r\n閉じる";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            this.bt_func12.Click += new System.EventHandler(this.ButtonFunc12_Click);
            // 
            // bt_func5
            // 
            this.bt_func5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func5.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func5.Location = new System.Drawing.Point(255, 236);
            this.bt_func5.Name = "bt_func5";
            this.bt_func5.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func5.Size = new System.Drawing.Size(80, 35);
            this.bt_func5.TabIndex = 6;
            this.bt_func5.TabStop = false;
            this.bt_func5.Tag = "func5";
            this.bt_func5.Text = "[F5]\r\n表示";
            this.bt_func5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func5.UseVisualStyleBackColor = false;
            this.bt_func5.Click += new System.EventHandler(this.ButtonFunc5_Click);
            // 
            // bt_func4
            // 
            this.bt_func4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func4.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func4.Location = new System.Drawing.Point(174, 236);
            this.bt_func4.Name = "bt_func4";
            this.bt_func4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func4.Size = new System.Drawing.Size(80, 35);
            this.bt_func4.TabIndex = 5;
            this.bt_func4.TabStop = false;
            this.bt_func4.Tag = "func4";
            this.bt_func4.Text = "[F4]\r\n削除";
            this.bt_func4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func4.UseVisualStyleBackColor = false;
            this.bt_func4.Click += new System.EventHandler(this.ButtonFunc4_Click);
            // 
            // bt_func3
            // 
            this.bt_func3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func3.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func3.Location = new System.Drawing.Point(93, 236);
            this.bt_func3.Name = "bt_func3";
            this.bt_func3.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func3.Size = new System.Drawing.Size(80, 35);
            this.bt_func3.TabIndex = 4;
            this.bt_func3.TabStop = false;
            this.bt_func3.Tag = "func3";
            this.bt_func3.Text = "[F3]\r\n修正";
            this.bt_func3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func3.UseVisualStyleBackColor = false;
            this.bt_func3.Click += new System.EventHandler(this.ButtonFunc3_Click);
            // 
            // bt_func2
            // 
            this.bt_func2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func2.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func2.Location = new System.Drawing.Point(12, 236);
            this.bt_func2.Name = "bt_func2";
            this.bt_func2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func2.Size = new System.Drawing.Size(80, 35);
            this.bt_func2.TabIndex = 3;
            this.bt_func2.TabStop = false;
            this.bt_func2.Tag = "func2";
            this.bt_func2.Text = "[F2]\r\n新規";
            this.bt_func2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func2.UseVisualStyleBackColor = false;
            this.bt_func2.Click += new System.EventHandler(this.ButtonFunc2_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "出力帳票を選択してください。";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // customListBox
            // 
            this.customListBox.BackColor = System.Drawing.SystemColors.Window;
            this.customListBox.DefaultBackColor = System.Drawing.Color.Empty;
            this.customListBox.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customListBox.FocusOutCheckMethod")));
            this.customListBox.FormattingEnabled = true;
            this.customListBox.ItemHeight = 12;
            this.customListBox.Items.AddRange(new object[] {
            "1111",
            "2222",
            "3333",
            "4444",
            "5555"});
            this.customListBox.Location = new System.Drawing.Point(12, 79);
            this.customListBox.Name = "customListBox";
            this.customListBox.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customListBox.PopupSearchSendParams")));
            this.customListBox.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customListBox.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customListBox.popupWindowSetting")));
            this.customListBox.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customListBox.RegistCheckMethod")));
            this.customListBox.Size = new System.Drawing.Size(404, 148);
            this.customListBox.TabIndex = 2;
            // 
            // G417_MeisaihyoSyukeihyoPatternSentakuPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(428, 283);
            this.Controls.Add(this.customListBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.bt_func5);
            this.Controls.Add(this.bt_func4);
            this.Controls.Add(this.bt_func3);
            this.Controls.Add(this.bt_func2);
            this.Controls.Add(this.lb_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "G417_MeisaihyoSyukeihyoPatternSentakuPopupForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "＊＊＊明細表";
            this.Load += new System.EventHandler(this.UIForm_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomButton bt_func5;
        public r_framework.CustomControl.CustomButton bt_func4;
        public r_framework.CustomControl.CustomButton bt_func3;
        public r_framework.CustomControl.CustomButton bt_func2;
        public System.Windows.Forms.Label label1;
        internal r_framework.CustomControl.CustomListBox customListBox;

    }
}