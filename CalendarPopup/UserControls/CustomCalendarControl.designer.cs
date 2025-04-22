namespace WindowsFormsApplication1.UserControls
{
    partial class CustomCalendarControl
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
            this.calendarPanel1 = new System.Windows.Forms.Panel();
            this.label07 = new System.Windows.Forms.Label();
            this.label06 = new System.Windows.Forms.Label();
            this.label05 = new System.Windows.Forms.Label();
            this.label04 = new System.Windows.Forms.Label();
            this.label03 = new System.Windows.Forms.Label();
            this.label02 = new System.Windows.Forms.Label();
            this.label01 = new System.Windows.Forms.Label();
            this.calendarTitle1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.FromNextBtn = new System.Windows.Forms.Button();
            this.FromPrevBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.calendarPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // calendarPanel1
            // 
            this.calendarPanel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.calendarPanel1.Controls.Add(this.label07);
            this.calendarPanel1.Controls.Add(this.label06);
            this.calendarPanel1.Controls.Add(this.label05);
            this.calendarPanel1.Controls.Add(this.label04);
            this.calendarPanel1.Controls.Add(this.label03);
            this.calendarPanel1.Controls.Add(this.label02);
            this.calendarPanel1.Controls.Add(this.label01);
            this.calendarPanel1.Controls.Add(this.calendarTitle1);
            this.calendarPanel1.Location = new System.Drawing.Point(4, 38);
            this.calendarPanel1.Name = "calendarPanel1";
            this.calendarPanel1.Size = new System.Drawing.Size(261, 205);
            this.calendarPanel1.TabIndex = 104;
            // 
            // label07
            // 
            this.label07.BackColor = System.Drawing.Color.Blue;
            this.label07.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label07.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label07.Location = new System.Drawing.Point(233, 26);
            this.label07.Name = "label07";
            this.label07.Size = new System.Drawing.Size(36, 22);
            this.label07.TabIndex = 111;
            this.label07.Text = "土";
            this.label07.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label06
            // 
            this.label06.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label06.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label06.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label06.Location = new System.Drawing.Point(195, 26);
            this.label06.Name = "label06";
            this.label06.Size = new System.Drawing.Size(36, 22);
            this.label06.TabIndex = 110;
            this.label06.Text = "金";
            this.label06.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label05
            // 
            this.label05.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label05.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label05.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label05.Location = new System.Drawing.Point(157, 26);
            this.label05.Name = "label05";
            this.label05.Size = new System.Drawing.Size(36, 22);
            this.label05.TabIndex = 109;
            this.label05.Text = "木";
            this.label05.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label04
            // 
            this.label04.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label04.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label04.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label04.Location = new System.Drawing.Point(119, 26);
            this.label04.Name = "label04";
            this.label04.Size = new System.Drawing.Size(36, 22);
            this.label04.TabIndex = 108;
            this.label04.Text = "水";
            this.label04.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label03
            // 
            this.label03.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label03.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label03.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label03.Location = new System.Drawing.Point(81, 26);
            this.label03.Name = "label03";
            this.label03.Size = new System.Drawing.Size(36, 22);
            this.label03.TabIndex = 107;
            this.label03.Text = "火";
            this.label03.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label02
            // 
            this.label02.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label02.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label02.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label02.Location = new System.Drawing.Point(43, 26);
            this.label02.Name = "label02";
            this.label02.Size = new System.Drawing.Size(36, 22);
            this.label02.TabIndex = 106;
            this.label02.Text = "月";
            this.label02.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label01
            // 
            this.label01.BackColor = System.Drawing.Color.Red;
            this.label01.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label01.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label01.Location = new System.Drawing.Point(5, 26);
            this.label01.Name = "label01";
            this.label01.Size = new System.Drawing.Size(36, 22);
            this.label01.TabIndex = 105;
            this.label01.Text = "日";
            this.label01.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // calendarTitle1
            // 
            this.calendarTitle1.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.calendarTitle1.ForeColor = System.Drawing.Color.White;
            this.calendarTitle1.Location = new System.Drawing.Point(106, 5);
            this.calendarTitle1.Name = "calendarTitle1";
            this.calendarTitle1.Size = new System.Drawing.Size(64, 15);
            this.calendarTitle1.TabIndex = 2;
            this.calendarTitle1.Text = "2013/05";
            this.calendarTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.FromNextBtn);
            this.panel1.Controls.Add(this.FromPrevBtn);
            this.panel1.Location = new System.Drawing.Point(5, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(260, 30);
            this.panel1.TabIndex = 101;
            // 
            // FromNextBtn
            // 
            this.FromNextBtn.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FromNextBtn.Location = new System.Drawing.Point(220, 1);
            this.FromNextBtn.Name = "FromNextBtn";
            this.FromNextBtn.Size = new System.Drawing.Size(36, 26);
            this.FromNextBtn.TabIndex = 150;
            this.FromNextBtn.Tag = "1ヶ月後のカレンダーに切り替わります";
            this.FromNextBtn.Text = "▷";
            this.FromNextBtn.UseVisualStyleBackColor = true;
            this.FromNextBtn.Click += new System.EventHandler(this.FromNextBtn_Click);
            // 
            // FromPrevBtn
            // 
            this.FromPrevBtn.Font = new System.Drawing.Font("ＭＳ ゴシック", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FromPrevBtn.Location = new System.Drawing.Point(3, 1);
            this.FromPrevBtn.Name = "FromPrevBtn";
            this.FromPrevBtn.Size = new System.Drawing.Size(36, 26);
            this.FromPrevBtn.TabIndex = 103;
            this.FromPrevBtn.Tag = "1ヶ月前のカレンダーに切り替わります";
            this.FromPrevBtn.Text = "◁";
            this.FromPrevBtn.UseVisualStyleBackColor = true;
            this.FromPrevBtn.Click += new System.EventHandler(this.FromPrevBtn_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.calendarPanel1);
            this.panel2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.panel2.Location = new System.Drawing.Point(0, 1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(271, 248);
            this.panel2.TabIndex = 100;
            // 
            // CustomCalendarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Name = "CustomCalendarControl";
            this.Size = new System.Drawing.Size(271, 249);
            this.Load += new System.EventHandler(this.CalendarControl_Load);
            this.calendarPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel calendarPanel1;
        private System.Windows.Forms.Label label07;
        private System.Windows.Forms.Label label06;
        private System.Windows.Forms.Label label05;
        private System.Windows.Forms.Label label04;
        private System.Windows.Forms.Label label03;
        private System.Windows.Forms.Label label02;
        private System.Windows.Forms.Label label01;
        private System.Windows.Forms.Label calendarTitle1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        //internal r_framework.CustomControl.CustomButton beforeButton;
        //internal r_framework.CustomControl.CustomButton afterButton;
        internal System.Windows.Forms.Button FromNextBtn;
        internal System.Windows.Forms.Button FromPrevBtn;
    }
}
