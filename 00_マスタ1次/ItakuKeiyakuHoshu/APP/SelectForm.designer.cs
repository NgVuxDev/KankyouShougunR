namespace ItakuKeiyakuHoshu.APP
{
    partial class SelectForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectForm));
            r_framework.Dto.RangeSettingDto rangeSettingDto1 = new r_framework.Dto.RangeSettingDto();
            r_framework.Dto.RangeSettingDto rangeSettingDto2 = new r_framework.Dto.RangeSettingDto();
            this.bt_ptn12 = new r_framework.CustomControl.CustomButton();
            this.LABEL_TITLE = new System.Windows.Forms.Label();
            this.bt_ptn9 = new r_framework.CustomControl.CustomButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LABEL_KEIYAKUSHO_SHOSHIKI = new System.Windows.Forms.Label();
            this.LABEL_KEIYAKUSHO_SHURUI = new System.Windows.Forms.Label();
            this.KEIYAKUSHO_SHOSHIKI_PANEL = new r_framework.CustomControl.CustomPanel();
            this.KENPAI_YOSHIKI = new r_framework.CustomControl.CustomRadioButton();
            this.ZENSANREN_YOSHIKI = new r_framework.CustomControl.CustomRadioButton();
            this.KEIYAKUSHO_SHOSHIKI = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KEIYAKUSHO_SHURUI_PANEL = new r_framework.CustomControl.CustomPanel();
            this.SHUSHU_SHOBUN_KEIYAKU = new r_framework.CustomControl.CustomRadioButton();
            this.SHOBUN_KEIYAKU = new r_framework.CustomControl.CustomRadioButton();
            this.SHUSHU_KEIYAKU = new r_framework.CustomControl.CustomRadioButton();
            this.KEIYAKUSHO_SHURUI = new r_framework.CustomControl.CustomNumericTextBox2();
            this.KEIYAKUSHO_SHOSHIKI_PANEL.SuspendLayout();
            this.KEIYAKUSHO_SHURUI_PANEL.SuspendLayout();
            this.SuspendLayout();
            // 
            // bt_ptn12
            // 
            this.bt_ptn12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_ptn12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn12.Location = new System.Drawing.Point(268, 215);
            this.bt_ptn12.Name = "bt_ptn12";
            this.bt_ptn12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn12.Size = new System.Drawing.Size(90, 35);
            this.bt_ptn12.TabIndex = 130;
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
            this.LABEL_TITLE.Size = new System.Drawing.Size(251, 34);
            this.LABEL_TITLE.TabIndex = 8;
            this.LABEL_TITLE.Text = "委託契約書選択";
            this.LABEL_TITLE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_ptn9
            // 
            this.bt_ptn9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_ptn9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn9.Location = new System.Drawing.Point(173, 215);
            this.bt_ptn9.Name = "bt_ptn9";
            this.bt_ptn9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn9.Size = new System.Drawing.Size(90, 35);
            this.bt_ptn9.TabIndex = 120;
            this.bt_ptn9.TabStop = false;
            this.bt_ptn9.Tag = "見積入力画面を表示します";
            this.bt_ptn9.Text = "[F9]\r\n確定";
            this.bt_ptn9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_ptn9.UseVisualStyleBackColor = true;
            // 
            // LABEL_KEIYAKUSHO_SHOSHIKI
            // 
            this.LABEL_KEIYAKUSHO_SHOSHIKI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LABEL_KEIYAKUSHO_SHOSHIKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LABEL_KEIYAKUSHO_SHOSHIKI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LABEL_KEIYAKUSHO_SHOSHIKI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LABEL_KEIYAKUSHO_SHOSHIKI.ForeColor = System.Drawing.Color.White;
            this.LABEL_KEIYAKUSHO_SHOSHIKI.Location = new System.Drawing.Point(12, 64);
            this.LABEL_KEIYAKUSHO_SHOSHIKI.Name = "LABEL_KEIYAKUSHO_SHOSHIKI";
            this.LABEL_KEIYAKUSHO_SHOSHIKI.Size = new System.Drawing.Size(115, 20);
            this.LABEL_KEIYAKUSHO_SHOSHIKI.TabIndex = 10;
            this.LABEL_KEIYAKUSHO_SHOSHIKI.Text = "委託契約書書式";
            this.LABEL_KEIYAKUSHO_SHOSHIKI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // LABEL_KEIYAKUSHO_SHURUI
            // 
            this.LABEL_KEIYAKUSHO_SHURUI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LABEL_KEIYAKUSHO_SHURUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LABEL_KEIYAKUSHO_SHURUI.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LABEL_KEIYAKUSHO_SHURUI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LABEL_KEIYAKUSHO_SHURUI.ForeColor = System.Drawing.Color.White;
            this.LABEL_KEIYAKUSHO_SHURUI.Location = new System.Drawing.Point(12, 122);
            this.LABEL_KEIYAKUSHO_SHURUI.Name = "LABEL_KEIYAKUSHO_SHURUI";
            this.LABEL_KEIYAKUSHO_SHURUI.Size = new System.Drawing.Size(115, 20);
            this.LABEL_KEIYAKUSHO_SHURUI.TabIndex = 60;
            this.LABEL_KEIYAKUSHO_SHURUI.Text = "委託契約書種類";
            this.LABEL_KEIYAKUSHO_SHURUI.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // KEIYAKUSHO_SHOSHIKI_PANEL
            // 
            this.KEIYAKUSHO_SHOSHIKI_PANEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKUSHO_SHOSHIKI_PANEL.Controls.Add(this.KENPAI_YOSHIKI);
            this.KEIYAKUSHO_SHOSHIKI_PANEL.Controls.Add(this.ZENSANREN_YOSHIKI);
            this.KEIYAKUSHO_SHOSHIKI_PANEL.Controls.Add(this.KEIYAKUSHO_SHOSHIKI);
            this.KEIYAKUSHO_SHOSHIKI_PANEL.Location = new System.Drawing.Point(141, 65);
            this.KEIYAKUSHO_SHOSHIKI_PANEL.Name = "KEIYAKUSHO_SHOSHIKI_PANEL";
            this.KEIYAKUSHO_SHOSHIKI_PANEL.Size = new System.Drawing.Size(222, 51);
            this.KEIYAKUSHO_SHOSHIKI_PANEL.TabIndex = 20;
            // 
            // KENPAI_YOSHIKI
            // 
            this.KENPAI_YOSHIKI.AutoSize = true;
            this.KENPAI_YOSHIKI.DefaultBackColor = System.Drawing.Color.Empty;
            this.KENPAI_YOSHIKI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENPAI_YOSHIKI.FocusOutCheckMethod")));
            this.KENPAI_YOSHIKI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.KENPAI_YOSHIKI.LinkedTextBox = "KEIYAKUSHO_SHOSHIKI";
            this.KENPAI_YOSHIKI.Location = new System.Drawing.Point(33, 25);
            this.KENPAI_YOSHIKI.Name = "KENPAI_YOSHIKI";
            this.KENPAI_YOSHIKI.PopupAfterExecute = null;
            this.KENPAI_YOSHIKI.PopupBeforeExecute = null;
            this.KENPAI_YOSHIKI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KENPAI_YOSHIKI.PopupSearchSendParams")));
            this.KENPAI_YOSHIKI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KENPAI_YOSHIKI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KENPAI_YOSHIKI.popupWindowSetting")));
            this.KENPAI_YOSHIKI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KENPAI_YOSHIKI.RegistCheckMethod")));
            this.KENPAI_YOSHIKI.Size = new System.Drawing.Size(165, 17);
            this.KENPAI_YOSHIKI.TabIndex = 50;
            this.KENPAI_YOSHIKI.Tag = "";
            this.KENPAI_YOSHIKI.Text = "2.建廃個別様式契約書";
            this.KENPAI_YOSHIKI.UseVisualStyleBackColor = true;
            this.KENPAI_YOSHIKI.Value = "2";
            // 
            // ZENSANREN_YOSHIKI
            // 
            this.ZENSANREN_YOSHIKI.AutoSize = true;
            this.ZENSANREN_YOSHIKI.DefaultBackColor = System.Drawing.Color.Empty;
            this.ZENSANREN_YOSHIKI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZENSANREN_YOSHIKI.FocusOutCheckMethod")));
            this.ZENSANREN_YOSHIKI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.ZENSANREN_YOSHIKI.LinkedTextBox = "KEIYAKUSHO_SHOSHIKI";
            this.ZENSANREN_YOSHIKI.Location = new System.Drawing.Point(33, 2);
            this.ZENSANREN_YOSHIKI.Name = "ZENSANREN_YOSHIKI";
            this.ZENSANREN_YOSHIKI.PopupAfterExecute = null;
            this.ZENSANREN_YOSHIKI.PopupBeforeExecute = null;
            this.ZENSANREN_YOSHIKI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ZENSANREN_YOSHIKI.PopupSearchSendParams")));
            this.ZENSANREN_YOSHIKI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ZENSANREN_YOSHIKI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ZENSANREN_YOSHIKI.popupWindowSetting")));
            this.ZENSANREN_YOSHIKI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ZENSANREN_YOSHIKI.RegistCheckMethod")));
            this.ZENSANREN_YOSHIKI.Size = new System.Drawing.Size(151, 17);
            this.ZENSANREN_YOSHIKI.TabIndex = 40;
            this.ZENSANREN_YOSHIKI.Tag = "";
            this.ZENSANREN_YOSHIKI.Text = "1.全産連様式契約書";
            this.ZENSANREN_YOSHIKI.UseVisualStyleBackColor = true;
            this.ZENSANREN_YOSHIKI.Value = "1";
            // 
            // KEIYAKUSHO_SHOSHIKI
            // 
            this.KEIYAKUSHO_SHOSHIKI.BackColor = System.Drawing.SystemColors.Window;
            this.KEIYAKUSHO_SHOSHIKI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKUSHO_SHOSHIKI.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKUSHO_SHOSHIKI.DisplayPopUp = null;
            this.KEIYAKUSHO_SHOSHIKI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHO_SHOSHIKI.FocusOutCheckMethod")));
            this.KEIYAKUSHO_SHOSHIKI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KEIYAKUSHO_SHOSHIKI.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKUSHO_SHOSHIKI.IsInputErrorOccured = false;
            this.KEIYAKUSHO_SHOSHIKI.LinkedRadioButtonArray = new string[] {
        "ZENSANREN_YOSHIKI",
        "KENPAI_YOSHIKI"};
            this.KEIYAKUSHO_SHOSHIKI.Location = new System.Drawing.Point(-1, -1);
            this.KEIYAKUSHO_SHOSHIKI.Name = "KEIYAKUSHO_SHOSHIKI";
            this.KEIYAKUSHO_SHOSHIKI.PopupAfterExecute = null;
            this.KEIYAKUSHO_SHOSHIKI.PopupBeforeExecute = null;
            this.KEIYAKUSHO_SHOSHIKI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKUSHO_SHOSHIKI.PopupSearchSendParams")));
            this.KEIYAKUSHO_SHOSHIKI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKUSHO_SHOSHIKI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKUSHO_SHOSHIKI.popupWindowSetting")));
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
            this.KEIYAKUSHO_SHOSHIKI.RangeSetting = rangeSettingDto1;
            this.KEIYAKUSHO_SHOSHIKI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHO_SHOSHIKI.RegistCheckMethod")));
            this.KEIYAKUSHO_SHOSHIKI.ShortItemName = "委託契約書式";
            this.KEIYAKUSHO_SHOSHIKI.Size = new System.Drawing.Size(28, 20);
            this.KEIYAKUSHO_SHOSHIKI.TabIndex = 30;
            this.KEIYAKUSHO_SHOSHIKI.WordWrap = false;
            // 
            // KEIYAKUSHO_SHURUI_PANEL
            // 
            this.KEIYAKUSHO_SHURUI_PANEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKUSHO_SHURUI_PANEL.Controls.Add(this.SHUSHU_SHOBUN_KEIYAKU);
            this.KEIYAKUSHO_SHURUI_PANEL.Controls.Add(this.SHOBUN_KEIYAKU);
            this.KEIYAKUSHO_SHURUI_PANEL.Controls.Add(this.SHUSHU_KEIYAKU);
            this.KEIYAKUSHO_SHURUI_PANEL.Controls.Add(this.KEIYAKUSHO_SHURUI);
            this.KEIYAKUSHO_SHURUI_PANEL.Location = new System.Drawing.Point(141, 122);
            this.KEIYAKUSHO_SHURUI_PANEL.Name = "KEIYAKUSHO_SHURUI_PANEL";
            this.KEIYAKUSHO_SHURUI_PANEL.Size = new System.Drawing.Size(222, 68);
            this.KEIYAKUSHO_SHURUI_PANEL.TabIndex = 70;
            // 
            // SHUSHU_SHOBUN_KEIYAKU
            // 
            this.SHUSHU_SHOBUN_KEIYAKU.AutoSize = true;
            this.SHUSHU_SHOBUN_KEIYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUSHU_SHOBUN_KEIYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUSHU_SHOBUN_KEIYAKU.FocusOutCheckMethod")));
            this.SHUSHU_SHOBUN_KEIYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHUSHU_SHOBUN_KEIYAKU.LinkedTextBox = "KEIYAKUSHO_SHURUI";
            this.SHUSHU_SHOBUN_KEIYAKU.Location = new System.Drawing.Point(33, 44);
            this.SHUSHU_SHOBUN_KEIYAKU.Name = "SHUSHU_SHOBUN_KEIYAKU";
            this.SHUSHU_SHOBUN_KEIYAKU.PopupAfterExecute = null;
            this.SHUSHU_SHOBUN_KEIYAKU.PopupBeforeExecute = null;
            this.SHUSHU_SHOBUN_KEIYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUSHU_SHOBUN_KEIYAKU.PopupSearchSendParams")));
            this.SHUSHU_SHOBUN_KEIYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUSHU_SHOBUN_KEIYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUSHU_SHOBUN_KEIYAKU.popupWindowSetting")));
            this.SHUSHU_SHOBUN_KEIYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUSHU_SHOBUN_KEIYAKU.RegistCheckMethod")));
            this.SHUSHU_SHOBUN_KEIYAKU.Size = new System.Drawing.Size(158, 17);
            this.SHUSHU_SHOBUN_KEIYAKU.TabIndex = 110;
            this.SHUSHU_SHOBUN_KEIYAKU.Tag = "";
            this.SHUSHU_SHOBUN_KEIYAKU.Text = "3.収集運搬/処分契約";
            this.SHUSHU_SHOBUN_KEIYAKU.UseVisualStyleBackColor = true;
            this.SHUSHU_SHOBUN_KEIYAKU.Value = "3";
            // 
            // SHOBUN_KEIYAKU
            // 
            this.SHOBUN_KEIYAKU.AutoSize = true;
            this.SHOBUN_KEIYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHOBUN_KEIYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_KEIYAKU.FocusOutCheckMethod")));
            this.SHOBUN_KEIYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHOBUN_KEIYAKU.LinkedTextBox = "KEIYAKUSHO_SHURUI";
            this.SHOBUN_KEIYAKU.Location = new System.Drawing.Point(33, 23);
            this.SHOBUN_KEIYAKU.Name = "SHOBUN_KEIYAKU";
            this.SHOBUN_KEIYAKU.PopupAfterExecute = null;
            this.SHOBUN_KEIYAKU.PopupBeforeExecute = null;
            this.SHOBUN_KEIYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHOBUN_KEIYAKU.PopupSearchSendParams")));
            this.SHOBUN_KEIYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHOBUN_KEIYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHOBUN_KEIYAKU.popupWindowSetting")));
            this.SHOBUN_KEIYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHOBUN_KEIYAKU.RegistCheckMethod")));
            this.SHOBUN_KEIYAKU.Size = new System.Drawing.Size(95, 17);
            this.SHOBUN_KEIYAKU.TabIndex = 100;
            this.SHOBUN_KEIYAKU.Tag = "";
            this.SHOBUN_KEIYAKU.Text = "2.処分契約";
            this.SHOBUN_KEIYAKU.UseVisualStyleBackColor = true;
            this.SHOBUN_KEIYAKU.Value = "2";
            // 
            // SHUSHU_KEIYAKU
            // 
            this.SHUSHU_KEIYAKU.AutoSize = true;
            this.SHUSHU_KEIYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHUSHU_KEIYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUSHU_KEIYAKU.FocusOutCheckMethod")));
            this.SHUSHU_KEIYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHUSHU_KEIYAKU.LinkedTextBox = "KEIYAKUSHO_SHURUI";
            this.SHUSHU_KEIYAKU.Location = new System.Drawing.Point(33, 2);
            this.SHUSHU_KEIYAKU.Name = "SHUSHU_KEIYAKU";
            this.SHUSHU_KEIYAKU.PopupAfterExecute = null;
            this.SHUSHU_KEIYAKU.PopupBeforeExecute = null;
            this.SHUSHU_KEIYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHUSHU_KEIYAKU.PopupSearchSendParams")));
            this.SHUSHU_KEIYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHUSHU_KEIYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHUSHU_KEIYAKU.popupWindowSetting")));
            this.SHUSHU_KEIYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHUSHU_KEIYAKU.RegistCheckMethod")));
            this.SHUSHU_KEIYAKU.Size = new System.Drawing.Size(123, 17);
            this.SHUSHU_KEIYAKU.TabIndex = 90;
            this.SHUSHU_KEIYAKU.Tag = "";
            this.SHUSHU_KEIYAKU.Text = "1.収集運搬契約";
            this.SHUSHU_KEIYAKU.UseVisualStyleBackColor = true;
            this.SHUSHU_KEIYAKU.Value = "1";
            // 
            // KEIYAKUSHO_SHURUI
            // 
            this.KEIYAKUSHO_SHURUI.BackColor = System.Drawing.SystemColors.Window;
            this.KEIYAKUSHO_SHURUI.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.KEIYAKUSHO_SHURUI.DefaultBackColor = System.Drawing.Color.Empty;
            this.KEIYAKUSHO_SHURUI.DisplayPopUp = null;
            this.KEIYAKUSHO_SHURUI.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHO_SHURUI.FocusOutCheckMethod")));
            this.KEIYAKUSHO_SHURUI.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.KEIYAKUSHO_SHURUI.ForeColor = System.Drawing.Color.Black;
            this.KEIYAKUSHO_SHURUI.IsInputErrorOccured = false;
            this.KEIYAKUSHO_SHURUI.LinkedRadioButtonArray = new string[] {
        "SHUSHU_KEIYAKU",
        "SHOBUN_KEIYAKU",
        "SHUSHU_SHOBUN_KEIYAKU"};
            this.KEIYAKUSHO_SHURUI.Location = new System.Drawing.Point(-1, -1);
            this.KEIYAKUSHO_SHURUI.Name = "KEIYAKUSHO_SHURUI";
            this.KEIYAKUSHO_SHURUI.PopupAfterExecute = null;
            this.KEIYAKUSHO_SHURUI.PopupBeforeExecute = null;
            this.KEIYAKUSHO_SHURUI.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("KEIYAKUSHO_SHURUI.PopupSearchSendParams")));
            this.KEIYAKUSHO_SHURUI.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.KEIYAKUSHO_SHURUI.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("KEIYAKUSHO_SHURUI.popupWindowSetting")));
            rangeSettingDto2.Max = new decimal(new int[] {
            3,
            0,
            0,
            0});
            rangeSettingDto2.Min = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.KEIYAKUSHO_SHURUI.RangeSetting = rangeSettingDto2;
            this.KEIYAKUSHO_SHURUI.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("KEIYAKUSHO_SHURUI.RegistCheckMethod")));
            this.KEIYAKUSHO_SHURUI.Size = new System.Drawing.Size(28, 20);
            this.KEIYAKUSHO_SHURUI.TabIndex = 80;
            this.KEIYAKUSHO_SHURUI.WordWrap = false;
            // 
            // SelectForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(375, 262);
            this.Controls.Add(this.KEIYAKUSHO_SHURUI_PANEL);
            this.Controls.Add(this.KEIYAKUSHO_SHOSHIKI_PANEL);
            this.Controls.Add(this.LABEL_KEIYAKUSHO_SHURUI);
            this.Controls.Add(this.LABEL_KEIYAKUSHO_SHOSHIKI);
            this.Controls.Add(this.bt_ptn9);
            this.Controls.Add(this.LABEL_TITLE);
            this.Controls.Add(this.bt_ptn12);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SelectForm";
            this.Text = "委託契約書選択";
            this.KEIYAKUSHO_SHOSHIKI_PANEL.ResumeLayout(false);
            this.KEIYAKUSHO_SHOSHIKI_PANEL.PerformLayout();
            this.KEIYAKUSHO_SHURUI_PANEL.ResumeLayout(false);
            this.KEIYAKUSHO_SHURUI_PANEL.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LABEL_TITLE;
        public r_framework.CustomControl.CustomButton bt_ptn12;
        public r_framework.CustomControl.CustomButton bt_ptn9;
        private System.Windows.Forms.ToolTip toolTip1;
        internal r_framework.CustomControl.CustomNumericTextBox2 KEIYAKUSHO_SHOSHIKI;
        public r_framework.CustomControl.CustomRadioButton ZENSANREN_YOSHIKI;
        public r_framework.CustomControl.CustomRadioButton KENPAI_YOSHIKI;
        public r_framework.CustomControl.CustomRadioButton SHOBUN_KEIYAKU;
        public r_framework.CustomControl.CustomRadioButton SHUSHU_KEIYAKU;
        internal r_framework.CustomControl.CustomNumericTextBox2 KEIYAKUSHO_SHURUI;
        public r_framework.CustomControl.CustomRadioButton SHUSHU_SHOBUN_KEIYAKU;
        internal System.Windows.Forms.Label LABEL_KEIYAKUSHO_SHURUI;
        internal r_framework.CustomControl.CustomPanel KEIYAKUSHO_SHURUI_PANEL;
        internal System.Windows.Forms.Label LABEL_KEIYAKUSHO_SHOSHIKI;
        internal r_framework.CustomControl.CustomPanel KEIYAKUSHO_SHOSHIKI_PANEL;
    }
}
