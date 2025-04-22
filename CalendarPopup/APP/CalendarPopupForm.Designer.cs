namespace CalendarPopup.APP
{
    partial class CalendarPopupForm
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
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
            this.bt_func10 = new r_framework.CustomControl.CustomButton();
            this.customCalendarControl1 = new WindowsFormsApplication1.UserControls.CustomCalendarControl();
            this.SuspendLayout();
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func12.Location = new System.Drawing.Point(233, 242);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 506;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // bt_func9
            // 
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Enabled = false;
            this.bt_func9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func9.Location = new System.Drawing.Point(5, 242);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(80, 35);
            this.bt_func9.TabIndex = 505;
            this.bt_func9.TabStop = false;
            this.bt_func9.Tag = "";
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            // 
            // bt_func10
            // 
            this.bt_func10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func10.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func10.Enabled = false;
            this.bt_func10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_func10.Location = new System.Drawing.Point(121, 242);
            this.bt_func10.Name = "bt_func10";
            this.bt_func10.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func10.Size = new System.Drawing.Size(80, 35);
            this.bt_func10.TabIndex = 507;
            this.bt_func10.TabStop = false;
            this.bt_func10.Tag = "";
            this.bt_func10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func10.UseVisualStyleBackColor = false;
            // 
            // customCalendarControl1
            // 
            this.customCalendarControl1.CalendarDataSource = null;
            this.customCalendarControl1.calendarPanel = null;
            this.customCalendarControl1.DayText = null;
            this.customCalendarControl1.Location = new System.Drawing.Point(3, 4);
            this.customCalendarControl1.MaxDate = new System.DateTime(((long)(0)));
            this.customCalendarControl1.MaxDateTime = new System.DateTime(((long)(0)));
            this.customCalendarControl1.MinDate = new System.DateTime(((long)(0)));
            this.customCalendarControl1.MinDateTime = new System.DateTime(((long)(0)));
            this.customCalendarControl1.Name = "customCalendarControl1";
            this.customCalendarControl1.Size = new System.Drawing.Size(310, 235);
            this.customCalendarControl1.StartDateTime = new System.DateTime(2013, 12, 1, 0, 0, 0, 0);
            this.customCalendarControl1.sysSunday = null;
            this.customCalendarControl1.TabIndex = 0;
            this.customCalendarControl1.OnFromPrevButton_Click += new WindowsFormsApplication1.UserControls.CustomCalendarControl.BeforeHandler(this.calendarControl1_OnFromPrevButton_Click);
            this.customCalendarControl1.OnFromNextButton_Click += new WindowsFormsApplication1.UserControls.CustomCalendarControl.AfterHandler(this.calendarControl1_OnFromNextButton_Click);
            this.customCalendarControl1.OnDayDouble_Click += new WindowsFormsApplication1.UserControls.CustomCalendarControl.AfterHandler(this.calendarControl1_OnDayDouble_Click);
            // 
            // CalendarPopupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 280);
            this.Controls.Add(this.bt_func10);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.bt_func9);
            this.Controls.Add(this.customCalendarControl1);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalendarPopupForm";
            this.Text = "カレンダー";
            this.ResumeLayout(false);

        }

        #endregion

        public WindowsFormsApplication1.UserControls.CustomCalendarControl customCalendarControl1;
        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomButton bt_func9;
        public r_framework.CustomControl.CustomButton bt_func10;

    }
}