namespace KaisyaKyujitsuHoshu.APP
{
    partial class KaisyaKyujitsuHoshuForm
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
            this.calendarControl1 = new KaisyaKyujitsuHoshu.UserControls.CalendarControl();
            this.SuspendLayout();
            // 
            // calendarControl1
            // 
            this.calendarControl1.CalendarDataSource = null;
            this.calendarControl1.calendarPanel = null;
            this.calendarControl1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.calendarControl1.Location = new System.Drawing.Point(10, 10);
            this.calendarControl1.MaxDate = new System.DateTime(((long)(0)));
            this.calendarControl1.MaxDateTime = new System.DateTime(((long)(0)));
            this.calendarControl1.MinDate = new System.DateTime(((long)(0)));
            this.calendarControl1.MinDateTime = new System.DateTime(((long)(0)));
            this.calendarControl1.Name = "calendarControl1";
            this.calendarControl1.Size = new System.Drawing.Size(977, 472);
            this.calendarControl1.StartDateTime = new System.DateTime(2013, 9, 1, 0, 0, 0, 0);
            this.calendarControl1.sysSunday = null;
            this.calendarControl1.TabIndex = 0;
            this.calendarControl1.OnBeforeButton_Click += new KaisyaKyujitsuHoshu.UserControls.CalendarControl.BeforeHandler(this.calendarControl1_OnBeforeButton_Click);
            this.calendarControl1.OnAfterButton_Click += new KaisyaKyujitsuHoshu.UserControls.CalendarControl.AfterHandler(this.calendarControl1_OnAfterButton_Click);
            // 
            // KaisyaKyujitsuHoshuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.calendarControl1);
            this.Name = "KaisyaKyujitsuHoshuForm";
            this.Text = "s";
            this.ResumeLayout(false);

        }

        #endregion

        public UserControls.CalendarControl calendarControl1;

    }
}