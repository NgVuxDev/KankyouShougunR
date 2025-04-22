namespace Shougun.Core.Common.InsatsuSettei
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto3 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto4 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto5 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto6 = new r_framework.Dto.RangeSettingDto();
            this.lblRecognizeReport = new System.Windows.Forms.Label();
            this.OUTPUT_KBN_VALUE = new r_framework.CustomControl.CustomNumericTextBox2();
            this.lblReportDispName = new System.Windows.Forms.Label();
            this.lblGenaralPrinter = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.OUTPUT_KBN_VERTICAL = new r_framework.CustomControl.CustomRadioButton();
            this.OUTPUT_KBN_HORIZONTAL = new r_framework.CustomControl.CustomRadioButton();
            this.listBoxtReprotDispName = new r_framework.CustomControl.CustomListBox();
            this.lblBlankSetting = new System.Windows.Forms.Label();
            this.lblDirection = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPrtSetBlankBottom = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtPrtSetBlankTop = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtPrtSetBlankLeft = new r_framework.CustomControl.CustomNumericTextBox2();
            this.txtPrtSetBlankRight = new r_framework.CustomControl.CustomNumericTextBox2();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblOutputPrinter = new System.Windows.Forms.Label();
            this.lblOutputPaper = new System.Windows.Forms.Label();
            this.lblOutputDevice = new System.Windows.Forms.Label();
            this.listBoxtOutputPrinter = new r_framework.CustomControl.CustomListBox();
            this.listBoxtOutputPaper = new r_framework.CustomControl.CustomListBox();
            this.listBoxOutputDevice = new r_framework.CustomControl.CustomListBox();
            this.pn_foot = new System.Windows.Forms.Panel();
            this.bt_func12 = new r_framework.CustomControl.CustomButton();
            this.lb_hint = new System.Windows.Forms.Label();
            this.bt_func10 = new r_framework.CustomControl.CustomButton();
            this.bt_func2 = new r_framework.CustomControl.CustomButton();
            this.bt_func1 = new r_framework.CustomControl.CustomButton();
            this.lb_title = new System.Windows.Forms.Label();
            this.ptGenaralPrinter = new System.Windows.Forms.Label();
            this.ptlRecognizeReport = new System.Windows.Forms.Label();
            this.PrtPaperSizes = new System.Drawing.Printing.PrintDocument();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.COLOR_KBN_COLOR = new r_framework.CustomControl.CustomRadioButton();
            this.COLOR_KBN_MONOCHRO = new r_framework.CustomControl.CustomRadioButton();
            this.COLOR_KBN_VALUE = new r_framework.CustomControl.CustomNumericTextBox2();
            this.panel1.SuspendLayout();
            this.pn_foot.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblRecognizeReport
            // 
            this.lblRecognizeReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblRecognizeReport.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRecognizeReport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblRecognizeReport.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblRecognizeReport.ForeColor = System.Drawing.Color.White;
            this.lblRecognizeReport.Location = new System.Drawing.Point(355, 79);
            this.lblRecognizeReport.Name = "lblRecognizeReport";
            this.lblRecognizeReport.Size = new System.Drawing.Size(153, 20);
            this.lblRecognizeReport.TabIndex = 5;
            this.lblRecognizeReport.Text = "ﾏﾆﾌｪｽﾄ印字ﾌﾟﾘﾝﾀ";
            this.lblRecognizeReport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OUTPUT_KBN_VALUE
            // 
            this.OUTPUT_KBN_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.OUTPUT_KBN_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OUTPUT_KBN_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.OUTPUT_KBN_VALUE.DisplayItemName = "印刷";
            this.OUTPUT_KBN_VALUE.DisplayPopUp = null;
            this.OUTPUT_KBN_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_VALUE.FocusOutCheckMethod")));
            this.OUTPUT_KBN_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.OUTPUT_KBN_VALUE.ForeColor = System.Drawing.Color.Black;
            this.OUTPUT_KBN_VALUE.IsInputErrorOccured = false;
            this.OUTPUT_KBN_VALUE.LinkedRadioButtonArray = new string[0];
            this.OUTPUT_KBN_VALUE.Location = new System.Drawing.Point(484, 420);
            this.OUTPUT_KBN_VALUE.Name = "OUTPUT_KBN_VALUE";
            this.OUTPUT_KBN_VALUE.PopupAfterExecute = null;
            this.OUTPUT_KBN_VALUE.PopupBeforeExecute = null;
            this.OUTPUT_KBN_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("OUTPUT_KBN_VALUE.PopupSearchSendParams")));
            this.OUTPUT_KBN_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.OUTPUT_KBN_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("OUTPUT_KBN_VALUE.popupWindowSetting")));
            this.OUTPUT_KBN_VALUE.prevText = null;
            this.OUTPUT_KBN_VALUE.PrevText = null;
            rangeSettingDto1.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto1.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.OUTPUT_KBN_VALUE.RangeSetting = rangeSettingDto1;
            this.OUTPUT_KBN_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_VALUE.RegistCheckMethod")));
            this.OUTPUT_KBN_VALUE.Size = new System.Drawing.Size(20, 20);
            this.OUTPUT_KBN_VALUE.TabIndex = 14;
            this.OUTPUT_KBN_VALUE.Tag = "出力区分を選択します";
            this.OUTPUT_KBN_VALUE.WordWrap = false;
            this.OUTPUT_KBN_VALUE.TextChanged += new System.EventHandler(this.OUTPUT_KBN_VALUE_TextChanged);
            // 
            // lblReportDispName
            // 
            this.lblReportDispName.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblReportDispName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblReportDispName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblReportDispName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblReportDispName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblReportDispName.ForeColor = System.Drawing.Color.White;
            this.lblReportDispName.Location = new System.Drawing.Point(12, 54);
            this.lblReportDispName.Name = "lblReportDispName";
            this.lblReportDispName.Size = new System.Drawing.Size(325, 30);
            this.lblReportDispName.TabIndex = 1;
            this.lblReportDispName.Text = "帳票名";
            this.lblReportDispName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGenaralPrinter
            // 
            this.lblGenaralPrinter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblGenaralPrinter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblGenaralPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblGenaralPrinter.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblGenaralPrinter.ForeColor = System.Drawing.Color.White;
            this.lblGenaralPrinter.Location = new System.Drawing.Point(355, 55);
            this.lblGenaralPrinter.Name = "lblGenaralPrinter";
            this.lblGenaralPrinter.Size = new System.Drawing.Size(153, 20);
            this.lblGenaralPrinter.TabIndex = 3;
            this.lblGenaralPrinter.Text = "通常使うプリンタ";
            this.lblGenaralPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(484, 464);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 22;
            this.label4.Text = "下余白";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.OUTPUT_KBN_VERTICAL);
            this.panel1.Controls.Add(this.OUTPUT_KBN_HORIZONTAL);
            this.panel1.Location = new System.Drawing.Point(503, 420);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(175, 20);
            this.panel1.TabIndex = 15;
            // 
            // OUTPUT_KBN_VERTICAL
            // 
            this.OUTPUT_KBN_VERTICAL.AutoSize = true;
            this.OUTPUT_KBN_VERTICAL.DefaultBackColor = System.Drawing.Color.Empty;
            this.OUTPUT_KBN_VERTICAL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_VERTICAL.FocusOutCheckMethod")));
            this.OUTPUT_KBN_VERTICAL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.OUTPUT_KBN_VERTICAL.LinkedTextBox = "OUTPUT_KBN_VALUE";
            this.OUTPUT_KBN_VERTICAL.Location = new System.Drawing.Point(93, 1);
            this.OUTPUT_KBN_VERTICAL.Name = "OUTPUT_KBN_VERTICAL";
            this.OUTPUT_KBN_VERTICAL.PopupAfterExecute = null;
            this.OUTPUT_KBN_VERTICAL.PopupBeforeExecute = null;
            this.OUTPUT_KBN_VERTICAL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("OUTPUT_KBN_VERTICAL.PopupSearchSendParams")));
            this.OUTPUT_KBN_VERTICAL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.OUTPUT_KBN_VERTICAL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("OUTPUT_KBN_VERTICAL.popupWindowSetting")));
            this.OUTPUT_KBN_VERTICAL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_VERTICAL.RegistCheckMethod")));
            this.OUTPUT_KBN_VERTICAL.Size = new System.Drawing.Size(81, 17);
            this.OUTPUT_KBN_VERTICAL.TabIndex = 1;
            this.OUTPUT_KBN_VERTICAL.Tag = "印刷方向を選択します";
            this.OUTPUT_KBN_VERTICAL.Text = "2.横方向";
            this.OUTPUT_KBN_VERTICAL.UseVisualStyleBackColor = true;
            this.OUTPUT_KBN_VERTICAL.Value = "2";
            // 
            // OUTPUT_KBN_HORIZONTAL
            // 
            this.OUTPUT_KBN_HORIZONTAL.AutoSize = true;
            this.OUTPUT_KBN_HORIZONTAL.CausesValidation = false;
            this.OUTPUT_KBN_HORIZONTAL.DefaultBackColor = System.Drawing.Color.Empty;
            this.OUTPUT_KBN_HORIZONTAL.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_HORIZONTAL.FocusOutCheckMethod")));
            this.OUTPUT_KBN_HORIZONTAL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.OUTPUT_KBN_HORIZONTAL.LinkedTextBox = "OUTPUT_KBN_VALUE";
            this.OUTPUT_KBN_HORIZONTAL.Location = new System.Drawing.Point(6, 1);
            this.OUTPUT_KBN_HORIZONTAL.Name = "OUTPUT_KBN_HORIZONTAL";
            this.OUTPUT_KBN_HORIZONTAL.PopupAfterExecute = null;
            this.OUTPUT_KBN_HORIZONTAL.PopupBeforeExecute = null;
            this.OUTPUT_KBN_HORIZONTAL.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("OUTPUT_KBN_HORIZONTAL.PopupSearchSendParams")));
            this.OUTPUT_KBN_HORIZONTAL.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.OUTPUT_KBN_HORIZONTAL.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("OUTPUT_KBN_HORIZONTAL.popupWindowSetting")));
            this.OUTPUT_KBN_HORIZONTAL.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("OUTPUT_KBN_HORIZONTAL.RegistCheckMethod")));
            this.OUTPUT_KBN_HORIZONTAL.Size = new System.Drawing.Size(81, 17);
            this.OUTPUT_KBN_HORIZONTAL.TabIndex = 0;
            this.OUTPUT_KBN_HORIZONTAL.Tag = "印刷方向を選択します";
            this.OUTPUT_KBN_HORIZONTAL.Text = "1.縦方向";
            this.OUTPUT_KBN_HORIZONTAL.UseVisualStyleBackColor = true;
            this.OUTPUT_KBN_HORIZONTAL.Value = "1";
            // 
            // listBoxtReprotDispName
            // 
            this.listBoxtReprotDispName.BackColor = System.Drawing.SystemColors.Window;
            this.listBoxtReprotDispName.DefaultBackColor = System.Drawing.Color.Empty;
            this.listBoxtReprotDispName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("listBoxtReprotDispName.FocusOutCheckMethod")));
            this.listBoxtReprotDispName.FormattingEnabled = true;
            this.listBoxtReprotDispName.Location = new System.Drawing.Point(12, 87);
            this.listBoxtReprotDispName.Name = "listBoxtReprotDispName";
            this.listBoxtReprotDispName.PopupAfterExecute = null;
            this.listBoxtReprotDispName.PopupBeforeExecute = null;
            this.listBoxtReprotDispName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("listBoxtReprotDispName.PopupSearchSendParams")));
            this.listBoxtReprotDispName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.listBoxtReprotDispName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("listBoxtReprotDispName.popupWindowSetting")));
            this.listBoxtReprotDispName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("listBoxtReprotDispName.RegistCheckMethod")));
            this.listBoxtReprotDispName.Size = new System.Drawing.Size(325, 407);
            this.listBoxtReprotDispName.TabIndex = 2;
            this.listBoxtReprotDispName.SelectedIndexChanged += new System.EventHandler(this.listBoxtReprotDispName_SelectedIndexChanged);
            // 
            // lblBlankSetting
            // 
            this.lblBlankSetting.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblBlankSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBlankSetting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblBlankSetting.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblBlankSetting.ForeColor = System.Drawing.Color.White;
            this.lblBlankSetting.Location = new System.Drawing.Point(354, 442);
            this.lblBlankSetting.Name = "lblBlankSetting";
            this.lblBlankSetting.Size = new System.Drawing.Size(125, 42);
            this.lblBlankSetting.TabIndex = 19;
            this.lblBlankSetting.Text = "余白設定（ミリ）";
            this.lblBlankSetting.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDirection
            // 
            this.lblDirection.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblDirection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblDirection.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblDirection.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.lblDirection.ForeColor = System.Drawing.Color.White;
            this.lblDirection.Location = new System.Drawing.Point(354, 420);
            this.lblDirection.Name = "lblDirection";
            this.lblDirection.Size = new System.Drawing.Size(125, 20);
            this.lblDirection.TabIndex = 13;
            this.lblDirection.Text = "印刷方向";
            this.lblDirection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(484, 442);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 20;
            this.label7.Text = "上余白";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtPrtSetBlankBottom
            // 
            this.txtPrtSetBlankBottom.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrtSetBlankBottom.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrtSetBlankBottom.CustomFormatSetting = "#,##0.##";
            this.txtPrtSetBlankBottom.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtPrtSetBlankBottom.DisplayPopUp = null;
            this.txtPrtSetBlankBottom.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPrtSetBlankBottom.FocusOutCheckMethod")));
            this.txtPrtSetBlankBottom.ForeColor = System.Drawing.Color.Black;
            this.txtPrtSetBlankBottom.FormatSetting = "カスタム";
            this.txtPrtSetBlankBottom.IsInputErrorOccured = false;
            this.txtPrtSetBlankBottom.LinkedRadioButtonArray = new string[0];
            this.txtPrtSetBlankBottom.Location = new System.Drawing.Point(599, 464);
            this.txtPrtSetBlankBottom.Multiline = true;
            this.txtPrtSetBlankBottom.Name = "txtPrtSetBlankBottom";
            this.txtPrtSetBlankBottom.PopupAfterExecute = null;
            this.txtPrtSetBlankBottom.PopupBeforeExecute = null;
            this.txtPrtSetBlankBottom.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtPrtSetBlankBottom.PopupSearchSendParams")));
            this.txtPrtSetBlankBottom.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtPrtSetBlankBottom.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtPrtSetBlankBottom.popupWindowSetting")));
            this.txtPrtSetBlankBottom.prevText = "";
            this.txtPrtSetBlankBottom.PrevText = "";
            rangeSettingDto2.Max = new decimal(new int[] {
            9999,
            0,
            0,
            131072});
            this.txtPrtSetBlankBottom.RangeSetting = rangeSettingDto2;
            this.txtPrtSetBlankBottom.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPrtSetBlankBottom.RegistCheckMethod")));
            this.txtPrtSetBlankBottom.Size = new System.Drawing.Size(80, 20);
            this.txtPrtSetBlankBottom.TabIndex = 23;
            this.txtPrtSetBlankBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrtSetBlankBottom.WordWrap = false;
            // 
            // txtPrtSetBlankTop
            // 
            this.txtPrtSetBlankTop.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrtSetBlankTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrtSetBlankTop.CustomFormatSetting = "#,##0.##";
            this.txtPrtSetBlankTop.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtPrtSetBlankTop.DisplayPopUp = null;
            this.txtPrtSetBlankTop.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPrtSetBlankTop.FocusOutCheckMethod")));
            this.txtPrtSetBlankTop.ForeColor = System.Drawing.Color.Black;
            this.txtPrtSetBlankTop.FormatSetting = "カスタム";
            this.txtPrtSetBlankTop.IsInputErrorOccured = false;
            this.txtPrtSetBlankTop.LinkedRadioButtonArray = new string[0];
            this.txtPrtSetBlankTop.Location = new System.Drawing.Point(599, 442);
            this.txtPrtSetBlankTop.Multiline = true;
            this.txtPrtSetBlankTop.Name = "txtPrtSetBlankTop";
            this.txtPrtSetBlankTop.PopupAfterExecute = null;
            this.txtPrtSetBlankTop.PopupBeforeExecute = null;
            this.txtPrtSetBlankTop.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtPrtSetBlankTop.PopupSearchSendParams")));
            this.txtPrtSetBlankTop.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtPrtSetBlankTop.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtPrtSetBlankTop.popupWindowSetting")));
            this.txtPrtSetBlankTop.prevText = "";
            this.txtPrtSetBlankTop.PrevText = "";
            rangeSettingDto3.Max = new decimal(new int[] {
            9999,
            0,
            0,
            131072});
            this.txtPrtSetBlankTop.RangeSetting = rangeSettingDto3;
            this.txtPrtSetBlankTop.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPrtSetBlankTop.RegistCheckMethod")));
            this.txtPrtSetBlankTop.Size = new System.Drawing.Size(80, 20);
            this.txtPrtSetBlankTop.TabIndex = 21;
            this.txtPrtSetBlankTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrtSetBlankTop.WordWrap = false;
            // 
            // txtPrtSetBlankLeft
            // 
            this.txtPrtSetBlankLeft.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrtSetBlankLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrtSetBlankLeft.CustomFormatSetting = "#,##0.##";
            this.txtPrtSetBlankLeft.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtPrtSetBlankLeft.DisplayPopUp = null;
            this.txtPrtSetBlankLeft.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPrtSetBlankLeft.FocusOutCheckMethod")));
            this.txtPrtSetBlankLeft.ForeColor = System.Drawing.Color.Black;
            this.txtPrtSetBlankLeft.FormatSetting = "カスタム";
            this.txtPrtSetBlankLeft.IsInputErrorOccured = false;
            this.txtPrtSetBlankLeft.LinkedRadioButtonArray = new string[0];
            this.txtPrtSetBlankLeft.Location = new System.Drawing.Point(799, 442);
            this.txtPrtSetBlankLeft.Multiline = true;
            this.txtPrtSetBlankLeft.Name = "txtPrtSetBlankLeft";
            this.txtPrtSetBlankLeft.PopupAfterExecute = null;
            this.txtPrtSetBlankLeft.PopupBeforeExecute = null;
            this.txtPrtSetBlankLeft.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtPrtSetBlankLeft.PopupSearchSendParams")));
            this.txtPrtSetBlankLeft.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtPrtSetBlankLeft.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtPrtSetBlankLeft.popupWindowSetting")));
            this.txtPrtSetBlankLeft.prevText = "";
            this.txtPrtSetBlankLeft.PrevText = "";
            rangeSettingDto4.Max = new decimal(new int[] {
            9999,
            0,
            0,
            131072});
            this.txtPrtSetBlankLeft.RangeSetting = rangeSettingDto4;
            this.txtPrtSetBlankLeft.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPrtSetBlankLeft.RegistCheckMethod")));
            this.txtPrtSetBlankLeft.Size = new System.Drawing.Size(80, 20);
            this.txtPrtSetBlankLeft.TabIndex = 25;
            this.txtPrtSetBlankLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrtSetBlankLeft.WordWrap = false;
            // 
            // txtPrtSetBlankRight
            // 
            this.txtPrtSetBlankRight.BackColor = System.Drawing.SystemColors.Window;
            this.txtPrtSetBlankRight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPrtSetBlankRight.CustomFormatSetting = "#,##0.##";
            this.txtPrtSetBlankRight.DefaultBackColor = System.Drawing.Color.Empty;
            this.txtPrtSetBlankRight.DisplayPopUp = null;
            this.txtPrtSetBlankRight.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPrtSetBlankRight.FocusOutCheckMethod")));
            this.txtPrtSetBlankRight.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.txtPrtSetBlankRight.ForeColor = System.Drawing.Color.Black;
            this.txtPrtSetBlankRight.FormatSetting = "カスタム";
            this.txtPrtSetBlankRight.IsInputErrorOccured = false;
            this.txtPrtSetBlankRight.LinkedRadioButtonArray = new string[0];
            this.txtPrtSetBlankRight.Location = new System.Drawing.Point(799, 464);
            this.txtPrtSetBlankRight.Multiline = true;
            this.txtPrtSetBlankRight.Name = "txtPrtSetBlankRight";
            this.txtPrtSetBlankRight.PopupAfterExecute = null;
            this.txtPrtSetBlankRight.PopupBeforeExecute = null;
            this.txtPrtSetBlankRight.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txtPrtSetBlankRight.PopupSearchSendParams")));
            this.txtPrtSetBlankRight.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txtPrtSetBlankRight.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("txtPrtSetBlankRight.popupWindowSetting")));
            this.txtPrtSetBlankRight.prevText = "";
            this.txtPrtSetBlankRight.PrevText = "";
            rangeSettingDto5.Max = new decimal(new int[] {
            9999,
            0,
            0,
            131072});
            this.txtPrtSetBlankRight.RangeSetting = rangeSettingDto5;
            this.txtPrtSetBlankRight.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("txtPrtSetBlankRight.RegistCheckMethod")));
            this.txtPrtSetBlankRight.Size = new System.Drawing.Size(80, 20);
            this.txtPrtSetBlankRight.TabIndex = 27;
            this.txtPrtSetBlankRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPrtSetBlankRight.WordWrap = false;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(684, 442);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 24;
            this.label8.Text = "左余白";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(684, 464);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 20);
            this.label9.TabIndex = 26;
            this.label9.Text = "右余白";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOutputPrinter
            // 
            this.lblOutputPrinter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblOutputPrinter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutputPrinter.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOutputPrinter.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOutputPrinter.ForeColor = System.Drawing.Color.White;
            this.lblOutputPrinter.Location = new System.Drawing.Point(354, 107);
            this.lblOutputPrinter.Name = "lblOutputPrinter";
            this.lblOutputPrinter.Size = new System.Drawing.Size(268, 30);
            this.lblOutputPrinter.TabIndex = 7;
            this.lblOutputPrinter.Text = "出力プリンタ";
            this.lblOutputPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOutputPaper
            // 
            this.lblOutputPaper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblOutputPaper.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutputPaper.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOutputPaper.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOutputPaper.ForeColor = System.Drawing.Color.White;
            this.lblOutputPaper.Location = new System.Drawing.Point(628, 107);
            this.lblOutputPaper.Name = "lblOutputPaper";
            this.lblOutputPaper.Size = new System.Drawing.Size(186, 30);
            this.lblOutputPaper.TabIndex = 9;
            this.lblOutputPaper.Text = "出力用紙";
            this.lblOutputPaper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOutputDevice
            // 
            this.lblOutputDevice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lblOutputDevice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOutputDevice.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.lblOutputDevice.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblOutputDevice.ForeColor = System.Drawing.Color.White;
            this.lblOutputDevice.Location = new System.Drawing.Point(820, 107);
            this.lblOutputDevice.Name = "lblOutputDevice";
            this.lblOutputDevice.Size = new System.Drawing.Size(182, 30);
            this.lblOutputDevice.TabIndex = 11;
            this.lblOutputDevice.Text = "出力デバイス";
            this.lblOutputDevice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listBoxtOutputPrinter
            // 
            this.listBoxtOutputPrinter.BackColor = System.Drawing.SystemColors.Window;
            this.listBoxtOutputPrinter.DefaultBackColor = System.Drawing.Color.Empty;
            this.listBoxtOutputPrinter.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("listBoxtOutputPrinter.FocusOutCheckMethod")));
            this.listBoxtOutputPrinter.FormattingEnabled = true;
            this.listBoxtOutputPrinter.Location = new System.Drawing.Point(354, 140);
            this.listBoxtOutputPrinter.Name = "listBoxtOutputPrinter";
            this.listBoxtOutputPrinter.PopupAfterExecute = null;
            this.listBoxtOutputPrinter.PopupBeforeExecute = null;
            this.listBoxtOutputPrinter.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("listBoxtOutputPrinter.PopupSearchSendParams")));
            this.listBoxtOutputPrinter.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.listBoxtOutputPrinter.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("listBoxtOutputPrinter.popupWindowSetting")));
            this.listBoxtOutputPrinter.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("listBoxtOutputPrinter.RegistCheckMethod")));
            this.listBoxtOutputPrinter.Size = new System.Drawing.Size(268, 277);
            this.listBoxtOutputPrinter.TabIndex = 8;
            this.listBoxtOutputPrinter.SelectedIndexChanged += new System.EventHandler(this.listBoxtOutputPrinter_SelectedIndexChanged);
            // 
            // listBoxtOutputPaper
            // 
            this.listBoxtOutputPaper.BackColor = System.Drawing.SystemColors.Window;
            this.listBoxtOutputPaper.DefaultBackColor = System.Drawing.Color.Empty;
            this.listBoxtOutputPaper.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("listBoxtOutputPaper.FocusOutCheckMethod")));
            this.listBoxtOutputPaper.FormattingEnabled = true;
            this.listBoxtOutputPaper.Location = new System.Drawing.Point(628, 141);
            this.listBoxtOutputPaper.Name = "listBoxtOutputPaper";
            this.listBoxtOutputPaper.PopupAfterExecute = null;
            this.listBoxtOutputPaper.PopupBeforeExecute = null;
            this.listBoxtOutputPaper.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("listBoxtOutputPaper.PopupSearchSendParams")));
            this.listBoxtOutputPaper.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.listBoxtOutputPaper.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("listBoxtOutputPaper.popupWindowSetting")));
            this.listBoxtOutputPaper.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("listBoxtOutputPaper.RegistCheckMethod")));
            this.listBoxtOutputPaper.Size = new System.Drawing.Size(186, 277);
            this.listBoxtOutputPaper.TabIndex = 10;
            this.listBoxtOutputPaper.SelectedIndexChanged += new System.EventHandler(this.listBoxtOutputPaper_SelectedIndexChanged);
            // 
            // listBoxOutputDevice
            // 
            this.listBoxOutputDevice.BackColor = System.Drawing.SystemColors.Window;
            this.listBoxOutputDevice.DefaultBackColor = System.Drawing.Color.Empty;
            this.listBoxOutputDevice.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("listBoxOutputDevice.FocusOutCheckMethod")));
            this.listBoxOutputDevice.FormattingEnabled = true;
            this.listBoxOutputDevice.Location = new System.Drawing.Point(821, 141);
            this.listBoxOutputDevice.Name = "listBoxOutputDevice";
            this.listBoxOutputDevice.PopupAfterExecute = null;
            this.listBoxOutputDevice.PopupBeforeExecute = null;
            this.listBoxOutputDevice.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("listBoxOutputDevice.PopupSearchSendParams")));
            this.listBoxOutputDevice.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.listBoxOutputDevice.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("listBoxOutputDevice.popupWindowSetting")));
            this.listBoxOutputDevice.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("listBoxOutputDevice.RegistCheckMethod")));
            this.listBoxOutputDevice.Size = new System.Drawing.Size(181, 277);
            this.listBoxOutputDevice.TabIndex = 12;
            // 
            // pn_foot
            // 
            this.pn_foot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pn_foot.Controls.Add(this.bt_func12);
            this.pn_foot.Controls.Add(this.lb_hint);
            this.pn_foot.Controls.Add(this.bt_func10);
            this.pn_foot.Controls.Add(this.bt_func2);
            this.pn_foot.Controls.Add(this.bt_func1);
            this.pn_foot.Location = new System.Drawing.Point(12, 500);
            this.pn_foot.Name = "pn_foot";
            this.pn_foot.Size = new System.Drawing.Size(996, 68);
            this.pn_foot.TabIndex = 390;
            // 
            // bt_func12
            // 
            this.bt_func12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func12.Enabled = false;
            this.bt_func12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func12.Location = new System.Drawing.Point(911, 30);
            this.bt_func12.Name = "bt_func12";
            this.bt_func12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func12.Size = new System.Drawing.Size(80, 35);
            this.bt_func12.TabIndex = 4;
            this.bt_func12.TabStop = false;
            this.bt_func12.Tag = "画面を閉じます";
            this.bt_func12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func12.UseVisualStyleBackColor = false;
            // 
            // lb_hint
            // 
            this.lb_hint.BackColor = System.Drawing.Color.Black;
            this.lb_hint.Font = new System.Drawing.Font("メイリオ", 9.75F);
            this.lb_hint.ForeColor = System.Drawing.Color.Yellow;
            this.lb_hint.Location = new System.Drawing.Point(3, 0);
            this.lb_hint.Name = "lb_hint";
            this.lb_hint.Size = new System.Drawing.Size(989, 28);
            this.lb_hint.TabIndex = 0;
            this.lb_hint.Text = "１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０１２３４５６７８９０";
            this.lb_hint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bt_func10
            // 
            this.bt_func10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func10.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func10.Enabled = false;
            this.bt_func10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func10.Location = new System.Drawing.Point(829, 30);
            this.bt_func10.Name = "bt_func10";
            this.bt_func10.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func10.Size = new System.Drawing.Size(80, 35);
            this.bt_func10.TabIndex = 3;
            this.bt_func10.TabStop = false;
            this.bt_func10.Tag = "選択帳票の印刷設定情報を保存します";
            this.bt_func10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func10.UseVisualStyleBackColor = false;
            // 
            // bt_func2
            // 
            this.bt_func2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func2.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func2.Enabled = false;
            this.bt_func2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func2.Location = new System.Drawing.Point(747, 30);
            this.bt_func2.Name = "bt_func2";
            this.bt_func2.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func2.Size = new System.Drawing.Size(80, 35);
            this.bt_func2.TabIndex = 2;
            this.bt_func2.TabStop = false;
            this.bt_func2.Tag = "プレビューを表示します";
            this.bt_func2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func2.UseVisualStyleBackColor = false;
            // 
            // bt_func1
            // 
            this.bt_func1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.bt_func1.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_func1.Enabled = false;
            this.bt_func1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bt_func1.Location = new System.Drawing.Point(655, 30);
            this.bt_func1.Name = "bt_func1";
            this.bt_func1.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_func1.Size = new System.Drawing.Size(90, 35);
            this.bt_func1.TabIndex = 1;
            this.bt_func1.TabStop = false;
            this.bt_func1.Tag = "余白既定値を設定します";
            this.bt_func1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_func1.UseVisualStyleBackColor = false;
            // 
            // lb_title
            // 
            this.lb_title.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.lb_title.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lb_title.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lb_title.ForeColor = System.Drawing.Color.White;
            this.lb_title.Location = new System.Drawing.Point(12, 9);
            this.lb_title.Name = "lb_title";
            this.lb_title.Size = new System.Drawing.Size(282, 34);
            this.lb_title.TabIndex = 0;
            this.lb_title.Text = "印刷設定";
            this.lb_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ptGenaralPrinter
            // 
            this.ptGenaralPrinter.Location = new System.Drawing.Point(514, 55);
            this.ptGenaralPrinter.Name = "ptGenaralPrinter";
            this.ptGenaralPrinter.Size = new System.Drawing.Size(472, 20);
            this.ptGenaralPrinter.TabIndex = 4;
            this.ptGenaralPrinter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ptlRecognizeReport
            // 
            this.ptlRecognizeReport.Location = new System.Drawing.Point(514, 79);
            this.ptlRecognizeReport.Name = "ptlRecognizeReport";
            this.ptlRecognizeReport.Size = new System.Drawing.Size(472, 20);
            this.ptlRecognizeReport.TabIndex = 6;
            this.ptlRecognizeReport.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(684, 420);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "カラー設定";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.COLOR_KBN_COLOR);
            this.panel2.Controls.Add(this.COLOR_KBN_MONOCHRO);
            this.panel2.Location = new System.Drawing.Point(818, 420);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(184, 20);
            this.panel2.TabIndex = 18;
            // 
            // COLOR_KBN_COLOR
            // 
            this.COLOR_KBN_COLOR.AutoSize = true;
            this.COLOR_KBN_COLOR.DefaultBackColor = System.Drawing.Color.Empty;
            this.COLOR_KBN_COLOR.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COLOR_KBN_COLOR.FocusOutCheckMethod")));
            this.COLOR_KBN_COLOR.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.COLOR_KBN_COLOR.LinkedTextBox = "COLOR_KBN_VALUE";
            this.COLOR_KBN_COLOR.Location = new System.Drawing.Point(104, 1);
            this.COLOR_KBN_COLOR.Name = "COLOR_KBN_COLOR";
            this.COLOR_KBN_COLOR.PopupAfterExecute = null;
            this.COLOR_KBN_COLOR.PopupBeforeExecute = null;
            this.COLOR_KBN_COLOR.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COLOR_KBN_COLOR.PopupSearchSendParams")));
            this.COLOR_KBN_COLOR.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COLOR_KBN_COLOR.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COLOR_KBN_COLOR.popupWindowSetting")));
            this.COLOR_KBN_COLOR.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COLOR_KBN_COLOR.RegistCheckMethod")));
            this.COLOR_KBN_COLOR.Size = new System.Drawing.Size(81, 17);
            this.COLOR_KBN_COLOR.TabIndex = 1;
            this.COLOR_KBN_COLOR.Tag = "印刷カラーを選択します";
            this.COLOR_KBN_COLOR.Text = "2.カラー";
            this.COLOR_KBN_COLOR.UseVisualStyleBackColor = true;
            this.COLOR_KBN_COLOR.Value = "2";
            // 
            // COLOR_KBN_MONOCHRO
            // 
            this.COLOR_KBN_MONOCHRO.AutoSize = true;
            this.COLOR_KBN_MONOCHRO.CausesValidation = false;
            this.COLOR_KBN_MONOCHRO.DefaultBackColor = System.Drawing.Color.Empty;
            this.COLOR_KBN_MONOCHRO.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COLOR_KBN_MONOCHRO.FocusOutCheckMethod")));
            this.COLOR_KBN_MONOCHRO.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.COLOR_KBN_MONOCHRO.LinkedTextBox = "COLOR_KBN_VALUE";
            this.COLOR_KBN_MONOCHRO.Location = new System.Drawing.Point(4, 1);
            this.COLOR_KBN_MONOCHRO.Name = "COLOR_KBN_MONOCHRO";
            this.COLOR_KBN_MONOCHRO.PopupAfterExecute = null;
            this.COLOR_KBN_MONOCHRO.PopupBeforeExecute = null;
            this.COLOR_KBN_MONOCHRO.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COLOR_KBN_MONOCHRO.PopupSearchSendParams")));
            this.COLOR_KBN_MONOCHRO.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COLOR_KBN_MONOCHRO.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COLOR_KBN_MONOCHRO.popupWindowSetting")));
            this.COLOR_KBN_MONOCHRO.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COLOR_KBN_MONOCHRO.RegistCheckMethod")));
            this.COLOR_KBN_MONOCHRO.Size = new System.Drawing.Size(95, 17);
            this.COLOR_KBN_MONOCHRO.TabIndex = 0;
            this.COLOR_KBN_MONOCHRO.Tag = "印刷カラーを選択します";
            this.COLOR_KBN_MONOCHRO.Text = "1.モノクロ";
            this.COLOR_KBN_MONOCHRO.UseVisualStyleBackColor = true;
            this.COLOR_KBN_MONOCHRO.Value = "1";
            // 
            // COLOR_KBN_VALUE
            // 
            this.COLOR_KBN_VALUE.BackColor = System.Drawing.SystemColors.Window;
            this.COLOR_KBN_VALUE.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.COLOR_KBN_VALUE.DefaultBackColor = System.Drawing.Color.Empty;
            this.COLOR_KBN_VALUE.DisplayItemName = "カラー";
            this.COLOR_KBN_VALUE.DisplayPopUp = null;
            this.COLOR_KBN_VALUE.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COLOR_KBN_VALUE.FocusOutCheckMethod")));
            this.COLOR_KBN_VALUE.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.COLOR_KBN_VALUE.ForeColor = System.Drawing.Color.Black;
            this.COLOR_KBN_VALUE.IsInputErrorOccured = false;
            this.COLOR_KBN_VALUE.LinkedRadioButtonArray = new string[] {
        "COLOR_KBN_MONOCHRO",
        "COLOR_KBN_COLOR"};
            this.COLOR_KBN_VALUE.Location = new System.Drawing.Point(799, 420);
            this.COLOR_KBN_VALUE.Name = "COLOR_KBN_VALUE";
            this.COLOR_KBN_VALUE.PopupAfterExecute = null;
            this.COLOR_KBN_VALUE.PopupBeforeExecute = null;
            this.COLOR_KBN_VALUE.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("COLOR_KBN_VALUE.PopupSearchSendParams")));
            this.COLOR_KBN_VALUE.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.COLOR_KBN_VALUE.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("COLOR_KBN_VALUE.popupWindowSetting")));
            this.COLOR_KBN_VALUE.prevText = "";
            this.COLOR_KBN_VALUE.PrevText = "";
            rangeSettingDto6.Max = new decimal(new int[] {
            2,
            0,
            0,
            0});
            rangeSettingDto6.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.COLOR_KBN_VALUE.RangeSetting = rangeSettingDto6;
            this.COLOR_KBN_VALUE.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("COLOR_KBN_VALUE.RegistCheckMethod")));
            this.COLOR_KBN_VALUE.Size = new System.Drawing.Size(20, 20);
            this.COLOR_KBN_VALUE.TabIndex = 17;
            this.COLOR_KBN_VALUE.Tag = "印刷カラーを選択します";
            this.COLOR_KBN_VALUE.WordWrap = false;
            this.COLOR_KBN_VALUE.Validated += new System.EventHandler(this.COLOR_KBN_VALUE_Validated);
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(1011, 568);
            this.Controls.Add(this.COLOR_KBN_VALUE);
            this.Controls.Add(this.ptlRecognizeReport);
            this.Controls.Add(this.ptGenaralPrinter);
            this.Controls.Add(this.lb_title);
            this.Controls.Add(this.pn_foot);
            this.Controls.Add(this.listBoxOutputDevice);
            this.Controls.Add(this.listBoxtOutputPaper);
            this.Controls.Add(this.listBoxtOutputPrinter);
            this.Controls.Add(this.lblOutputDevice);
            this.Controls.Add(this.lblOutputPaper);
            this.Controls.Add(this.lblOutputPrinter);
            this.Controls.Add(this.txtPrtSetBlankLeft);
            this.Controls.Add(this.txtPrtSetBlankRight);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtPrtSetBlankTop);
            this.Controls.Add(this.txtPrtSetBlankBottom);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblDirection);
            this.Controls.Add(this.lblBlankSetting);
            this.Controls.Add(this.listBoxtReprotDispName);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblGenaralPrinter);
            this.Controls.Add(this.lblReportDispName);
            this.Controls.Add(this.OUTPUT_KBN_VALUE);
            this.Controls.Add(this.lblRecognizeReport);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "UIForm";
            this.ShowIcon = false;
            this.Text = "印刷設定";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pn_foot.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblRecognizeReport;
        private System.Windows.Forms.Label lblReportDispName;
        public System.Windows.Forms.Label lblGenaralPrinter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel1;
        internal r_framework.CustomControl.CustomRadioButton OUTPUT_KBN_VERTICAL;
        internal r_framework.CustomControl.CustomRadioButton OUTPUT_KBN_HORIZONTAL;
        public r_framework.CustomControl.CustomNumericTextBox2 OUTPUT_KBN_VALUE;
        public r_framework.CustomControl.CustomListBox listBoxtReprotDispName;
        private System.Windows.Forms.Label lblBlankSetting;
        private System.Windows.Forms.Label lblDirection;
        private System.Windows.Forms.Label label7;
        public r_framework.CustomControl.CustomNumericTextBox2 txtPrtSetBlankBottom;
        public r_framework.CustomControl.CustomNumericTextBox2 txtPrtSetBlankTop;
        public r_framework.CustomControl.CustomNumericTextBox2 txtPrtSetBlankLeft;
        public r_framework.CustomControl.CustomNumericTextBox2 txtPrtSetBlankRight;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblOutputPrinter;
        private System.Windows.Forms.Label lblOutputPaper;
        private System.Windows.Forms.Label lblOutputDevice;
        public r_framework.CustomControl.CustomListBox listBoxtOutputPrinter;
        public r_framework.CustomControl.CustomListBox listBoxtOutputPaper;
        public r_framework.CustomControl.CustomListBox listBoxOutputDevice;
        public System.Windows.Forms.Panel pn_foot;
        public r_framework.CustomControl.CustomButton bt_func12;
        public System.Windows.Forms.Label lb_hint;
        public r_framework.CustomControl.CustomButton bt_func10;
        public r_framework.CustomControl.CustomButton bt_func1;
        public System.Windows.Forms.Label lb_title;
        public System.Windows.Forms.Label ptGenaralPrinter;
        public System.Windows.Forms.Label ptlRecognizeReport;
        public System.Drawing.Printing.PrintDocument PrtPaperSizes;
        internal r_framework.CustomControl.CustomButton bt_func2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        internal r_framework.CustomControl.CustomRadioButton COLOR_KBN_COLOR;
        internal r_framework.CustomControl.CustomRadioButton COLOR_KBN_MONOCHRO;
        internal r_framework.CustomControl.CustomNumericTextBox2 COLOR_KBN_VALUE;
    }
}