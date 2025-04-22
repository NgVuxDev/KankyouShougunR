using r_framework.Components;
using r_framework.CustomControl;

namespace Shougun.Core.Common.DenpyouHimodukeIchiran
{
    partial class UIBaseForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIBaseForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            this.pn_foot = new System.Windows.Forms.Panel();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.lb_hint = new System.Windows.Forms.Label();
            this.bt_func11 = new r_framework.CustomControl.CustomButton();
            this.bt_func10 = new r_framework.CustomControl.CustomButton();
            this.bt_func9 = new r_framework.CustomControl.CustomButton();
            this.bt_func8 = new r_framework.CustomControl.CustomButton();
            this.bt_func7 = new r_framework.CustomControl.CustomButton();
            this.bt_func6 = new r_framework.CustomControl.CustomButton();
            this.bt_func5 = new r_framework.CustomControl.CustomButton();
            this.bt_func4 = new r_framework.CustomControl.CustomButton();
            this.bt_func3 = new r_framework.CustomControl.CustomButton();
            this.bt_func2 = new r_framework.CustomControl.CustomButton();
            this.bt_func1 = new r_framework.CustomControl.CustomButton();
            this.ProcessButtonPanel = new System.Windows.Forms.Panel();
            this.txb_process = new r_framework.CustomControl.CustomNumericTextBox2();
            this.bt_process5 = new r_framework.CustomControl.CustomButton();
            this.bt_process4 = new r_framework.CustomControl.CustomButton();
            this.bt_process2 = new r_framework.CustomControl.CustomButton();
            this.bt_process1 = new r_framework.CustomControl.CustomButton();
            this.bt_process3 = new r_framework.CustomControl.CustomButton();
            this.lb_process = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progresBar = new System.Windows.Forms.ToolStripProgressBar();
            this.imeStatus = new r_framework.Components.ImeStatus(this.components);
            this.pn_foot.SuspendLayout();
            this.ProcessButtonPanel.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pn_foot
            // 
            this.pn_foot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pn_foot.CausesValidation = false;
            this.pn_foot.Controls.Add(this.bt_func12);
            this.pn_foot.Controls.Add(this.lb_hint);
            this.pn_foot.Controls.Add(this.bt_func11);
            this.pn_foot.Controls.Add(this.bt_func10);
            this.pn_foot.Controls.Add(this.bt_func9);
            this.pn_foot.Controls.Add(this.bt_func8);
            this.pn_foot.Controls.Add(this.bt_func7);
            this.pn_foot.Controls.Add(this.bt_func6);
            this.pn_foot.Controls.Add(this.bt_func5);
            this.pn_foot.Controls.Add(this.bt_func4);
            this.pn_foot.Controls.Add(this.bt_func3);
            this.pn_foot.Controls.Add(this.bt_func2);
            this.pn_foot.Controls.Add(this.bt_func1);
            this.pn_foot.Location = new System.Drawing.Point(12, 639);
            this.pn_foot.Name = "pn_foot";
            this.pn_foot.Size = new System.Drawing.Size(999, 68);
            this.pn_foot.TabIndex = 204;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            //this.bt_func12.CausesValidation = false;
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func12.Location = new System.Drawing.Point(912, 29);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 390;
            this.bt_func12.Tag = "";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // lb_hint
            // 
            this.lb_hint.BackColor = System.Drawing.Color.Black;
            this.lb_hint.Font = new System.Drawing.Font("メイリオ", 9.75F);
            this.lb_hint.ForeColor = System.Drawing.Color.Yellow;
            this.lb_hint.Location = new System.Drawing.Point(3, 4);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(989, 21);
            this.lb_hint.TabIndex = 0;
            this.lb_hint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_func11
            // 
            this.bt_func11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func11.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func11.Enabled = false;
            this.bt_func11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func11.Location = new System.Drawing.Point(831, 29);
            this.bt_func11.Name = "bt_func11";
            this.bt_func11.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func11.Size = new System.Drawing.Size(80, 35);
            this.bt_func11.TabIndex = 389;
            this.bt_func11.Tag = "";
            this.bt_func11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func11.UseVisualStyleBackColor = false;
            // 
            // bt_func10
            // 
            this.bt_func10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func10.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func10.Enabled = false;
            this.bt_func10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func10.Location = new System.Drawing.Point(750, 29);
            this.bt_func10.Name = "bt_func10";
            this.bt_func10.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func10.Size = new System.Drawing.Size(80, 35);
            this.bt_func10.TabIndex = 388;
            this.bt_func10.Tag = "";
            this.bt_func10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func10.UseVisualStyleBackColor = false;
            // 
            // bt_func9
            // 
            this.bt_func9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func9.Enabled = false;
            this.bt_func9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func9.Location = new System.Drawing.Point(669, 29);
            this.bt_func9.Name = "bt_func9";
            this.bt_func9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func9.Size = new System.Drawing.Size(80, 35);
            this.bt_func9.TabIndex = 387;
            this.bt_func9.Tag = "";
            this.bt_func9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func9.UseVisualStyleBackColor = false;
            // 
            // bt_func8
            // 
            this.bt_func8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func8.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func8.Enabled = false;
            this.bt_func8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func8.Location = new System.Drawing.Point(579, 29);
            this.bt_func8.Name = "bt_func8";
            this.bt_func8.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func8.Size = new System.Drawing.Size(80, 35);
            this.bt_func8.TabIndex = 386;
            this.bt_func8.Tag = "";
            this.bt_func8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func8.UseVisualStyleBackColor = false;
            // 
            // bt_func7
            // 
            this.bt_func7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func7.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func7.Enabled = false;
            this.bt_func7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func7.Location = new System.Drawing.Point(498, 29);
            this.bt_func7.Name = "bt_func7";
            this.bt_func7.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func7.Size = new System.Drawing.Size(80, 35);
            this.bt_func7.TabIndex = 385;
            this.bt_func7.Tag = "";
            this.bt_func7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func7.UseVisualStyleBackColor = false;
            // 
            // bt_func6
            // 
            this.bt_func6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func6.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func6.Enabled = false;
            this.bt_func6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func6.Location = new System.Drawing.Point(417, 29);
            this.bt_func6.Name = "bt_func6";
            this.bt_func6.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func6.Size = new System.Drawing.Size(80, 35);
            this.bt_func6.TabIndex = 384;
            this.bt_func6.Tag = "";
            this.bt_func6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func6.UseVisualStyleBackColor = false;
            // 
            // bt_func5
            // 
            this.bt_func5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func5.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func5.Enabled = false;
            this.bt_func5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func5.Location = new System.Drawing.Point(336, 29);
            this.bt_func5.Name = "bt_func5";
            this.bt_func5.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func5.Size = new System.Drawing.Size(80, 35);
            this.bt_func5.TabIndex = 383;
            this.bt_func5.Tag = "";
            this.bt_func5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func5.UseVisualStyleBackColor = false;
            // 
            // bt_func4
            // 
            this.bt_func4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func4.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func4.Enabled = false;
            this.bt_func4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func4.Location = new System.Drawing.Point(246, 29);
            this.bt_func4.Name = "bt_func4";
            this.bt_func4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func4.Size = new System.Drawing.Size(80, 35);
            this.bt_func4.TabIndex = 382;
            this.bt_func4.Tag = "";
            this.bt_func4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func4.UseVisualStyleBackColor = false;
            // 
            // bt_func3
            // 
            this.bt_func3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func3.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func3.Enabled = false;
            this.bt_func3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func3.Location = new System.Drawing.Point(165, 29);
            this.bt_func3.Name = "bt_func3";
            this.bt_func3.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func3.Size = new System.Drawing.Size(80, 35);
            this.bt_func3.TabIndex = 381;
            this.bt_func3.Tag = "";
            this.bt_func3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func3.UseVisualStyleBackColor = false;
            // 
            // bt_func2
            // 
            this.bt_func2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func2.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func2.Enabled = false;
            this.bt_func2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func2.Location = new System.Drawing.Point(84, 29);
            this.bt_func2.Name = "bt_func2";
            this.bt_func2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func2.Size = new System.Drawing.Size(80, 35);
            this.bt_func2.TabIndex = 380;
            this.bt_func2.Tag = "";
            this.bt_func2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func2.UseVisualStyleBackColor = false;
            // 
            // bt_func1
            // 
            this.bt_func1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func1.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func1.Enabled = false;
            this.bt_func1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func1.Location = new System.Drawing.Point(3, 29);
            this.bt_func1.Name = "bt_func1";
            this.bt_func1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func1.Size = new System.Drawing.Size(80, 35);
            this.bt_func1.TabIndex = 379;
            this.bt_func1.Tag = "";
            this.bt_func1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func1.UseVisualStyleBackColor = false;
            // 
            // ProcessButtonPanel
            // 
            this.ProcessButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ProcessButtonPanel.Controls.Add(this.txb_process);
            this.ProcessButtonPanel.Controls.Add(this.bt_process5);
            this.ProcessButtonPanel.Controls.Add(this.bt_process4);
            this.ProcessButtonPanel.Controls.Add(this.bt_process2);
            this.ProcessButtonPanel.Controls.Add(this.bt_process1);
            this.ProcessButtonPanel.Controls.Add(this.bt_process3);
            this.ProcessButtonPanel.Controls.Add(this.lb_process);
            this.ProcessButtonPanel.Location = new System.Drawing.Point(1024, 528);
            this.ProcessButtonPanel.Name = "ProcessButtonPanel";
            this.ProcessButtonPanel.Size = new System.Drawing.Size(156, 179);
            this.ProcessButtonPanel.TabIndex = 391;
            // 
            // txb_process
            // 
            this.txb_process.BackColor = System.Drawing.SystemColors.Window;
            this.txb_process.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_process.DefaultBackColor = System.Drawing.Color.Empty;
            this.txb_process.DisplayPopUp = null;
            this.txb_process.ErrorMessage = "";
            this.txb_process.FocusOutCheckMethod = null;
            this.txb_process.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.txb_process.ForeColor = System.Drawing.Color.Black;
            this.txb_process.GetCodeMasterField = "";
            this.txb_process.IsInputErrorOccured = false;
            this.txb_process.LinkedRadioButtonArray = new string[0];
            this.txb_process.Location = new System.Drawing.Point(110, 155);
            this.txb_process.Name = "txb_process";
            this.txb_process.PopupAfterExecute = null;
            this.txb_process.PopupBeforeExecute = null;
            this.txb_process.PopupGetMasterField = "";
            this.txb_process.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txb_process.PopupSearchSendParams")));
            this.txb_process.PopupSetFormField = "";
            this.txb_process.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txb_process.popupWindowSetting = null;
            this.txb_process.prevText = null;
            this.txb_process.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txb_process.RangeSetting = rangeSettingDto1;
            this.txb_process.RegistCheckMethod = null;
            this.txb_process.SetFormField = "";
            this.txb_process.Size = new System.Drawing.Size(23, 20);
            this.txb_process.TabIndex = 390;
            this.txb_process.TabStop = false;
            this.txb_process.Tag = " ";
            this.txb_process.WordWrap = false;
            this.txb_process.TextChanged += new System.EventHandler(this.txb_process_TextChanged);
            this.txb_process.Leave += new System.EventHandler(this.txb_process_Leave);
            // 
            // bt_process5
            // 
            this.bt_process5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_process5.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_process5.Enabled = false;
            this.bt_process5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_process5.Location = new System.Drawing.Point(3, 123);
            this.bt_process5.Name = "bt_process5";
            this.bt_process5.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_process5.Size = new System.Drawing.Size(150, 30);
            this.bt_process5.TabIndex = 395;
            this.bt_process5.Tag = "";
            this.bt_process5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_process5.UseVisualStyleBackColor = false;
            // 
            // bt_process4
            // 
            this.bt_process4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_process4.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_process4.Enabled = false;
            this.bt_process4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_process4.Location = new System.Drawing.Point(3, 93);
            this.bt_process4.Name = "bt_process4";
            this.bt_process4.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_process4.Size = new System.Drawing.Size(150, 30);
            this.bt_process4.TabIndex = 394;
            this.bt_process4.Tag = "";
            this.bt_process4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_process4.UseVisualStyleBackColor = false;
            // 
            // bt_process2
            // 
            this.bt_process2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_process2.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_process2.Enabled = false;
            this.bt_process2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_process2.Location = new System.Drawing.Point(3, 33);
            this.bt_process2.Name = "bt_process2";
            this.bt_process2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_process2.Size = new System.Drawing.Size(150, 30);
            this.bt_process2.TabIndex = 392;
            this.bt_process2.Tag = "";
            this.bt_process2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_process2.UseVisualStyleBackColor = false;
            // 
            // bt_process1
            // 
            this.bt_process1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_process1.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_process1.Enabled = false;
            this.bt_process1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_process1.Location = new System.Drawing.Point(3, 3);
            this.bt_process1.Name = "bt_process1";
            this.bt_process1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_process1.Size = new System.Drawing.Size(150, 30);
            this.bt_process1.TabIndex = 391;
            this.bt_process1.Tag = "";
            this.bt_process1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_process1.UseVisualStyleBackColor = false;
            // 
            // bt_process3
            // 
            this.bt_process3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_process3.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_process3.Enabled = false;
            this.bt_process3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_process3.Location = new System.Drawing.Point(3, 63);
            this.bt_process3.Name = "bt_process3";
            this.bt_process3.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_process3.Size = new System.Drawing.Size(150, 30);
            this.bt_process3.TabIndex = 393;
            this.bt_process3.Tag = "";
            this.bt_process3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_process3.UseVisualStyleBackColor = false;
            // 
            // lb_process
            // 
            this.lb_process.BackColor = System.Drawing.Color.DarkGreen;
            this.lb_process.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lb_process.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lb_process.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lb_process.ForeColor = System.Drawing.Color.White;
            this.lb_process.Location = new System.Drawing.Point(3, 156);
            this.lb_process.Name = "lb_process";
            this.lb_process.Size = new System.Drawing.Size(100, 19);
            this.lb_process.TabIndex = 5;
            this.lb_process.Text = "処理No (ESC)";
            this.lb_process.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progresBar});
            this.statusStrip1.Location = new System.Drawing.Point(0, 708);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1192, 22);
            this.statusStrip1.TabIndex = 392;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progresBar
            // 
            this.progresBar.Name = "progresBar";
            this.progresBar.Size = new System.Drawing.Size(100, 16);
            // 
            // UIBaseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1192, 730);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.ProcessButtonPanel);
            this.Controls.Add(this.pn_foot);
            this.KeyPreview = true;
            this.Name = "UIBaseForm";
            this.Text = "環境将軍Ｒ";
            this.Load += new System.EventHandler(this.UIBaseForm_Load);
            this.SizeChanged += new System.EventHandler(this.BaseForm03_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.UIBaseForm_KeyDown);
            this.pn_foot.ResumeLayout(false);
            this.ProcessButtonPanel.ResumeLayout(false);
            this.ProcessButtonPanel.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Panel pn_foot;
        public System.Windows.Forms.Label lb_hint;
        public CustomButton bt_func12;
        public CustomButton bt_func11;
        public CustomButton bt_func10;
        public CustomButton bt_func9;
        public CustomButton bt_func8;
        public CustomButton bt_func7;
        public CustomButton bt_func6;
        public CustomButton bt_func5;
        public CustomButton bt_func4;
        public CustomButton bt_func3;
        public CustomButton bt_func2;
        public CustomButton bt_func1;
        public System.Windows.Forms.Panel ProcessButtonPanel;
        public CustomButton bt_process5;
        public CustomButton bt_process4;
        public CustomButton bt_process2;
        public CustomButton bt_process1;
        public CustomButton bt_process3;
        public System.Windows.Forms.Label lb_process;
        public System.Windows.Forms.ToolStripProgressBar progresBar;
        public System.Windows.Forms.StatusStrip statusStrip1;
        public CustomNumericTextBox2 txb_process;
        private ImeStatus imeStatus;
    }
}