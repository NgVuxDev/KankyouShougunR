namespace Shougun.Core.ExternalConnection.SmsResult
{
    partial class SmsResultShoukai
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SmsResultShoukai));
            this.lb_title = new System.Windows.Forms.Label();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.bt_func8 = new r_framework.CustomControl.CustomButton();
            this.label2 = new System.Windows.Forms.Label();
            this.SAGYOU_DATE = new r_framework.CustomControl.CustomDateTimePicker();
            this.SuspendLayout();
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold);
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(17, 21);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(393, 34);
            this.lb_title.TabIndex = 0;
            this.lb_title.Text = "ｼｮｰﾄﾒｯｾｰｼﾞ着信状況照会";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.CausesValidation = false;
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func12.Location = new System.Drawing.Point(330, 166);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 80;
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // bt_func8
            // 
            this.bt_func8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func8.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func8.Enabled = false;
            this.bt_func8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func8.Location = new System.Drawing.Point(244, 166);
            this.bt_func8.Name = "bt_func8";
            this.bt_func8.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func8.Size = new System.Drawing.Size(80, 35);
            this.bt_func8.TabIndex = 70;
            this.bt_func8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func8.UseVisualStyleBackColor = false;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(17, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 664;
            this.label2.Text = "メッセージ送信日";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SAGYOU_DATE
            // 
            this.SAGYOU_DATE.BackColor = System.Drawing.SystemColors.Window;
            this.SAGYOU_DATE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SAGYOU_DATE.CalendarFont = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.SAGYOU_DATE.Checked = false;
            this.SAGYOU_DATE.DateTimeNowYear = "";
            this.SAGYOU_DATE.DBFieldsName = "";
            this.SAGYOU_DATE.DefaultBackColor = System.Drawing.Color.Empty;
            this.SAGYOU_DATE.DisplayItemName = "作業日";
            this.SAGYOU_DATE.DisplayPopUp = null;
            this.SAGYOU_DATE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAGYOU_DATE.FocusOutCheckMethod")));
            this.SAGYOU_DATE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SAGYOU_DATE.ForeColor = System.Drawing.Color.Black;
            this.SAGYOU_DATE.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.SAGYOU_DATE.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SAGYOU_DATE.IsInputErrorOccured = false;
            this.SAGYOU_DATE.ItemDefinedTypes = "datetime";
            this.SAGYOU_DATE.Location = new System.Drawing.Point(152, 79);
            this.SAGYOU_DATE.MaxLength = 10;
            this.SAGYOU_DATE.MinValue = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.SAGYOU_DATE.Name = "SAGYOU_DATE";
            this.SAGYOU_DATE.NullValue = "";
            this.SAGYOU_DATE.PopupAfterExecute = null;
            this.SAGYOU_DATE.PopupBeforeExecute = null;
            this.SAGYOU_DATE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SAGYOU_DATE.PopupSearchSendParams")));
            this.SAGYOU_DATE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SAGYOU_DATE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SAGYOU_DATE.popupWindowSetting")));
            this.SAGYOU_DATE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SAGYOU_DATE.RegistCheckMethod")));
            this.SAGYOU_DATE.ShortItemName = "作業日";
            this.SAGYOU_DATE.Size = new System.Drawing.Size(124, 20);
            this.SAGYOU_DATE.TabIndex = 665;
            this.SAGYOU_DATE.Tag = "作業日を入力して下さい";
            this.SAGYOU_DATE.Value = null;
            // 
            // SmsResultShoukai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(463, 222);
            this.Controls.Add(this.SAGYOU_DATE);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bt_func8);
            this.Controls.Add(this.bt_func12);
            this.Controls.Add(this.lb_title);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SmsResultShoukai";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label lb_title;
        public r_framework.CustomControl.CustomButton bt_func12;
        public r_framework.CustomControl.CustomButton bt_func8;
        private System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomDateTimePicker SAGYOU_DATE;



    }
}