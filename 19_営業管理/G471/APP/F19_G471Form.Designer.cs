namespace Shougun.Core.BusinessManagement.Mitumorisyo
{
    partial class G471Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(G471Form));
            this.CUSOMLISTBOX_MITUMORISYO = new r_framework.CustomControl.CustomListBox();
            this.LABEL_MITUMORISYO_SELECT = new System.Windows.Forms.Label();
            this.bt_ptn12 = new r_framework.CustomControl.CustomButton();
            this.LABEL_TITLE = new System.Windows.Forms.Label();
            this.bt_ptn5 = new r_framework.CustomControl.CustomButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // CUSOMLISTBOX_MITUMORISYO
            // 
            this.CUSOMLISTBOX_MITUMORISYO.BackColor = System.Drawing.SystemColors.Window;
            this.CUSOMLISTBOX_MITUMORISYO.DefaultBackColor = System.Drawing.Color.Empty;
            this.CUSOMLISTBOX_MITUMORISYO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CUSOMLISTBOX_MITUMORISYO.FocusOutCheckMethod")));
            this.CUSOMLISTBOX_MITUMORISYO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.CUSOMLISTBOX_MITUMORISYO.FormattingEnabled = true;
            this.CUSOMLISTBOX_MITUMORISYO.Location = new System.Drawing.Point(12, 101);
            this.CUSOMLISTBOX_MITUMORISYO.Name = "CUSOMLISTBOX_MITUMORISYO";
            this.CUSOMLISTBOX_MITUMORISYO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("CUSOMLISTBOX_MITUMORISYO.PopupSearchSendParams")));
            this.CUSOMLISTBOX_MITUMORISYO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.CUSOMLISTBOX_MITUMORISYO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("CUSOMLISTBOX_MITUMORISYO.popupWindowSetting")));
            this.CUSOMLISTBOX_MITUMORISYO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("CUSOMLISTBOX_MITUMORISYO.RegistCheckMethod")));
            this.CUSOMLISTBOX_MITUMORISYO.Size = new System.Drawing.Size(345, 69);
            this.CUSOMLISTBOX_MITUMORISYO.TabIndex = 1;
            this.CUSOMLISTBOX_MITUMORISYO.Tag = "見積書を選択してください";
            this.CUSOMLISTBOX_MITUMORISYO.DoubleClick += new System.EventHandler(this.CUSOMLISTBOX_MITUMORISYO_DoubleClick);
            // 
            // LABEL_MITUMORISYO_SELECT
            // 
            this.LABEL_MITUMORISYO_SELECT.AutoSize = true;
            this.LABEL_MITUMORISYO_SELECT.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LABEL_MITUMORISYO_SELECT.Location = new System.Drawing.Point(17, 84);
            this.LABEL_MITUMORISYO_SELECT.Name = "LABEL_MITUMORISYO_SELECT";
            this.LABEL_MITUMORISYO_SELECT.Size = new System.Drawing.Size(189, 13);
            this.LABEL_MITUMORISYO_SELECT.TabIndex = 6;
            this.LABEL_MITUMORISYO_SELECT.Text = "見積書を選択してください。";
            // 
            // bt_ptn12
            // 
            this.bt_ptn12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_ptn12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn12.Location = new System.Drawing.Point(394, 188);
            this.bt_ptn12.Name = "bt_ptn12";
            this.bt_ptn12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn12.Size = new System.Drawing.Size(90, 35);
            this.bt_ptn12.TabIndex = 3;
            this.bt_ptn12.TabStop = false;
            this.bt_ptn12.Tag = "画面を閉じます";
            this.bt_ptn12.Text = "[F12] \r\n閉じる";
            this.bt_ptn12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_ptn12.UseVisualStyleBackColor = true;
            // 
            // LABEL_TITLE
            // 
            this.LABEL_TITLE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LABEL_TITLE.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LABEL_TITLE.ForeColor = System.Drawing.Color.White;
            this.LABEL_TITLE.Location = new System.Drawing.Point(12, 21);
            this.LABEL_TITLE.Name = "LABEL_TITLE";
            this.LABEL_TITLE.Size = new System.Drawing.Size(213, 34);
            this.LABEL_TITLE.TabIndex = 8;
            this.LABEL_TITLE.Text = "見積書選択";
            this.LABEL_TITLE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_ptn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(299, 188);
            this.bt_ptn5.Name = "bt_ptn5";
            this.bt_ptn5.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn5.Size = new System.Drawing.Size(90, 35);
            this.bt_ptn5.TabIndex = 2;
            this.bt_ptn5.TabStop = false;
            this.bt_ptn5.Tag = "見積入力画面を表示します";
            this.bt_ptn5.Text = "[F5] \r\n表示";
            this.bt_ptn5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_ptn5.UseVisualStyleBackColor = true;
            // 
            // G471Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(503, 237);
            this.Controls.Add(this.bt_ptn5);
            this.Controls.Add(this.LABEL_TITLE);
            this.Controls.Add(this.bt_ptn12);
            this.Controls.Add(this.LABEL_MITUMORISYO_SELECT);
            this.Controls.Add(this.CUSOMLISTBOX_MITUMORISYO);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "G471Form";
            this.Text = "見積書選択";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LABEL_MITUMORISYO_SELECT;
        private System.Windows.Forms.Label LABEL_TITLE;
        public r_framework.CustomControl.CustomListBox CUSOMLISTBOX_MITUMORISYO;
        public r_framework.CustomControl.CustomButton bt_ptn12;
        public r_framework.CustomControl.CustomButton bt_ptn5;
        private System.Windows.Forms.ToolTip toolTip1;
      
    }
}