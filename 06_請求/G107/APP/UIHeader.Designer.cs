namespace Shougun.Core.Billing.SeikyuushoHakkou
{
    partial class UIHeader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIHeader));
            this.txt_BusyoMei = new r_framework.CustomControl.CustomTextBox();
            this.txt_BusyoCD = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Busyo = new System.Windows.Forms.Label();
            this.txt_KyotenMei = new r_framework.CustomControl.CustomTextBox();
            this.txt_KyotenCD = new r_framework.CustomControl.CustomTextBox();
            this.lbl_Kyoten = new System.Windows.Forms.Label();
            this.customTextBox4 = new r_framework.CustomControl.CustomTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.customTextBox3 = new r_framework.CustomControl.CustomTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.customTextBox2 = new r_framework.CustomControl.CustomTextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.customTextBox31 = new r_framework.CustomControl.CustomTextBox();
            this.受付日 = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // windowTypeLabel
            // 
            this.windowTypeLabel.Location = new System.Drawing.Point(8, 7);
            this.windowTypeLabel.Visible = false;
            // 
            // lb_title
            // 
            this.lb_title.Location = new System.Drawing.Point(8, 7);
            this.lb_title.Size = new System.Drawing.Size(280, 35);
            this.lb_title.Text = "請求書発行";
            // 
            // txt_BusyoMei
            // 
            this.txt_BusyoMei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_BusyoMei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_BusyoMei.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_BusyoMei.Enabled = false;
            this.txt_BusyoMei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_BusyoMei.FocusOutCheckMethod")));
            this.txt_BusyoMei.Location = new System.Drawing.Point(468, 27);
            this.txt_BusyoMei.MaxLength = 0;
            this.txt_BusyoMei.Name = "txt_BusyoMei";
            this.txt_BusyoMei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_BusyoMei.PopupSearchSendParams")));
            this.txt_BusyoMei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_BusyoMei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_BusyoMei.popupWindowSetting")));
            this.txt_BusyoMei.ReadOnly = true;
            this.txt_BusyoMei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_BusyoMei.RegistCheckMethod")));
            this.txt_BusyoMei.Size = new System.Drawing.Size(150, 19);
            this.txt_BusyoMei.TabIndex = 537;
            this.txt_BusyoMei.Tag = " は 0 文字以内で入力してください。";
            // 
            // txt_BusyoCD
            // 
            this.txt_BusyoCD.BackColor = System.Drawing.SystemColors.Window;
            this.txt_BusyoCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_BusyoCD.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_BusyoCD.Enabled = false;
            this.txt_BusyoCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_BusyoCD.FocusOutCheckMethod")));
            this.txt_BusyoCD.Location = new System.Drawing.Point(412, 27);
            this.txt_BusyoCD.MaxLength = 0;
            this.txt_BusyoCD.Name = "txt_BusyoCD";
            this.txt_BusyoCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_BusyoCD.PopupSearchSendParams")));
            this.txt_BusyoCD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_BusyoCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_BusyoCD.popupWindowSetting")));
            this.txt_BusyoCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_BusyoCD.RegistCheckMethod")));
            this.txt_BusyoCD.Size = new System.Drawing.Size(53, 19);
            this.txt_BusyoCD.TabIndex = 536;
            this.txt_BusyoCD.Tag = " は 0 文字以内で入力してください。";
            // 
            // lbl_Busyo
            // 
            this.lbl_Busyo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Busyo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Busyo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Busyo.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Busyo.ForeColor = System.Drawing.Color.White;
            this.lbl_Busyo.Location = new System.Drawing.Point(298, 26);
            this.lbl_Busyo.Name = "lbl_Busyo";
            this.lbl_Busyo.Size = new System.Drawing.Size(110, 20);
            this.lbl_Busyo.TabIndex = 535;
            this.lbl_Busyo.Text = "部門";
            this.lbl_Busyo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_KyotenMei
            // 
            this.txt_KyotenMei.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.txt_KyotenMei.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KyotenMei.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_KyotenMei.Enabled = false;
            this.txt_KyotenMei.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenMei.FocusOutCheckMethod")));
            this.txt_KyotenMei.Location = new System.Drawing.Point(468, 6);
            this.txt_KyotenMei.MaxLength = 0;
            this.txt_KyotenMei.Name = "txt_KyotenMei";
            this.txt_KyotenMei.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KyotenMei.PopupSearchSendParams")));
            this.txt_KyotenMei.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_KyotenMei.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KyotenMei.popupWindowSetting")));
            this.txt_KyotenMei.ReadOnly = true;
            this.txt_KyotenMei.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenMei.RegistCheckMethod")));
            this.txt_KyotenMei.Size = new System.Drawing.Size(150, 19);
            this.txt_KyotenMei.TabIndex = 534;
            this.txt_KyotenMei.Tag = " は 0 文字以内で入力してください。";
            // 
            // txt_KyotenCD
            // 
            this.txt_KyotenCD.BackColor = System.Drawing.SystemColors.Window;
            this.txt_KyotenCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_KyotenCD.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txt_KyotenCD.Enabled = false;
            this.txt_KyotenCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenCD.FocusOutCheckMethod")));
            this.txt_KyotenCD.Location = new System.Drawing.Point(412, 6);
            this.txt_KyotenCD.MaxLength = 0;
            this.txt_KyotenCD.Name = "txt_KyotenCD";
            this.txt_KyotenCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txt_KyotenCD.PopupSearchSendParams")));
            this.txt_KyotenCD.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txt_KyotenCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txt_KyotenCD.popupWindowSetting")));
            this.txt_KyotenCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txt_KyotenCD.RegistCheckMethod")));
            this.txt_KyotenCD.Size = new System.Drawing.Size(53, 19);
            this.txt_KyotenCD.TabIndex = 533;
            this.txt_KyotenCD.Tag = " は 0 文字以内で入力してください。";
            // 
            // lbl_Kyoten
            // 
            this.lbl_Kyoten.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lbl_Kyoten.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Kyoten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lbl_Kyoten.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_Kyoten.ForeColor = System.Drawing.Color.White;
            this.lbl_Kyoten.Location = new System.Drawing.Point(298, 5);
            this.lbl_Kyoten.Name = "lbl_Kyoten";
            this.lbl_Kyoten.Size = new System.Drawing.Size(110, 20);
            this.lbl_Kyoten.TabIndex = 532;
            this.lbl_Kyoten.Text = "拠点";
            this.lbl_Kyoten.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customTextBox4
            // 
            this.customTextBox4.BackColor = System.Drawing.SystemColors.Window;
            this.customTextBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox4.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.customTextBox4.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox4.FocusOutCheckMethod")));
            this.customTextBox4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.customTextBox4.Location = new System.Drawing.Point(1065, 25);
            this.customTextBox4.MaxLength = 0;
            this.customTextBox4.Name = "customTextBox4";
            this.customTextBox4.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox4.PopupSearchSendParams")));
            this.customTextBox4.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customTextBox4.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox4.popupWindowSetting")));
            this.customTextBox4.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox4.RegistCheckMethod")));
            this.customTextBox4.Size = new System.Drawing.Size(105, 20);
            this.customTextBox4.TabIndex = 531;
            this.customTextBox4.Tag = " は 0 文字以内で入力してください。";
            this.customTextBox4.Text = "1,000";
            this.customTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(952, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 530;
            this.label2.Text = "アラート件数";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customTextBox3
            // 
            this.customTextBox3.BackColor = System.Drawing.SystemColors.Window;
            this.customTextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox3.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.customTextBox3.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox3.FocusOutCheckMethod")));
            this.customTextBox3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.customTextBox3.Location = new System.Drawing.Point(1065, 4);
            this.customTextBox3.MaxLength = 0;
            this.customTextBox3.Name = "customTextBox3";
            this.customTextBox3.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox3.PopupSearchSendParams")));
            this.customTextBox3.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customTextBox3.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox3.popupWindowSetting")));
            this.customTextBox3.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox3.RegistCheckMethod")));
            this.customTextBox3.Size = new System.Drawing.Size(105, 20);
            this.customTextBox3.TabIndex = 529;
            this.customTextBox3.Tag = " は 0 文字以内で入力してください。";
            this.customTextBox3.Text = "20";
            this.customTextBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(952, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 528;
            this.label1.Text = "読込データ件数";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.customTextBox2);
            this.panel2.Controls.Add(this.label38);
            this.panel2.Controls.Add(this.customTextBox31);
            this.panel2.Controls.Add(this.受付日);
            this.panel2.Location = new System.Drawing.Point(620, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(331, 44);
            this.panel2.TabIndex = 538;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.SystemColors.Control;
            this.button4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button4.Image = ((System.Drawing.Image)(resources.GetObject("button4.Image")));
            this.button4.Location = new System.Drawing.Point(309, 21);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(22, 22);
            this.button4.TabIndex = 409;
            this.button4.UseVisualStyleBackColor = false;
            // 
            // customTextBox2
            // 
            this.customTextBox2.BackColor = System.Drawing.SystemColors.Window;
            this.customTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox2.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.customTextBox2.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox2.FocusOutCheckMethod")));
            this.customTextBox2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.customTextBox2.Location = new System.Drawing.Point(223, 23);
            this.customTextBox2.MaxLength = 0;
            this.customTextBox2.Name = "customTextBox2";
            this.customTextBox2.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox2.PopupSearchSendParams")));
            this.customTextBox2.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customTextBox2.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox2.popupWindowSetting")));
            this.customTextBox2.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox2.RegistCheckMethod")));
            this.customTextBox2.Size = new System.Drawing.Size(85, 20);
            this.customTextBox2.TabIndex = 408;
            this.customTextBox2.Tag = " は 0 文字以内で入力してください。";
            this.customTextBox2.Text = "2013/05/15";
            this.customTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label38
            // 
            this.label38.BackColor = System.Drawing.Color.Transparent;
            this.label38.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label38.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label38.ForeColor = System.Drawing.Color.Black;
            this.label38.Location = new System.Drawing.Point(203, 20);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(19, 24);
            this.label38.TabIndex = 407;
            this.label38.Text = "～";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // customTextBox31
            // 
            this.customTextBox31.BackColor = System.Drawing.SystemColors.Window;
            this.customTextBox31.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.customTextBox31.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.customTextBox31.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox31.FocusOutCheckMethod")));
            this.customTextBox31.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.customTextBox31.Location = new System.Drawing.Point(115, 23);
            this.customTextBox31.MaxLength = 0;
            this.customTextBox31.Name = "customTextBox31";
            this.customTextBox31.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("customTextBox31.PopupSearchSendParams")));
            this.customTextBox31.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.customTextBox31.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("customTextBox31.popupWindowSetting")));
            this.customTextBox31.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("customTextBox31.RegistCheckMethod")));
            this.customTextBox31.Size = new System.Drawing.Size(85, 20);
            this.customTextBox31.TabIndex = 406;
            this.customTextBox31.Tag = " は 0 文字以内で入力してください。";
            this.customTextBox31.Text = "2013/05/01";
            this.customTextBox31.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // 受付日
            // 
            this.受付日.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.受付日.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.受付日.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.受付日.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.受付日.ForeColor = System.Drawing.Color.White;
            this.受付日.Location = new System.Drawing.Point(0, 23);
            this.受付日.Name = "受付日";
            this.受付日.Size = new System.Drawing.Size(110, 20);
            this.受付日.TabIndex = 405;
            this.受付日.Text = "伝票日付";
            this.受付日.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIHeader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1174, 50);
            this.Controls.Add(this.txt_BusyoMei);
            this.Controls.Add(this.txt_BusyoCD);
            this.Controls.Add(this.lbl_Busyo);
            this.Controls.Add(this.txt_KyotenMei);
            this.Controls.Add(this.txt_KyotenCD);
            this.Controls.Add(this.lbl_Kyoten);
            this.Controls.Add(this.customTextBox4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.customTextBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Name = "UIHeader";
            this.Text = "HeaderSample";
            this.Controls.SetChildIndex(this.lb_title, 0);
            this.Controls.SetChildIndex(this.windowTypeLabel, 0);
            this.Controls.SetChildIndex(this.panel2, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.customTextBox3, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.customTextBox4, 0);
            this.Controls.SetChildIndex(this.lbl_Kyoten, 0);
            this.Controls.SetChildIndex(this.txt_KyotenCD, 0);
            this.Controls.SetChildIndex(this.txt_KyotenMei, 0);
            this.Controls.SetChildIndex(this.lbl_Busyo, 0);
            this.Controls.SetChildIndex(this.txt_BusyoCD, 0);
            this.Controls.SetChildIndex(this.txt_BusyoMei, 0);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomTextBox txt_BusyoMei;
        public r_framework.CustomControl.CustomTextBox txt_BusyoCD;
        public System.Windows.Forms.Label lbl_Busyo;
        public r_framework.CustomControl.CustomTextBox txt_KyotenMei;
        public r_framework.CustomControl.CustomTextBox txt_KyotenCD;
        public System.Windows.Forms.Label lbl_Kyoten;
        private r_framework.CustomControl.CustomTextBox customTextBox4;
        private System.Windows.Forms.Label label2;
        private r_framework.CustomControl.CustomTextBox customTextBox3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button button4;
        private r_framework.CustomControl.CustomTextBox customTextBox2;
        private System.Windows.Forms.Label label38;
        private r_framework.CustomControl.CustomTextBox customTextBox31;
        private System.Windows.Forms.Label 受付日;

    }
}