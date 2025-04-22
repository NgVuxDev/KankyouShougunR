namespace ItakuKeiyakuHoshu.APP
{
    partial class PatternRegistForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatternRegistForm));
            this.bt_ptn12 = new r_framework.CustomControl.CustomButton();
            this.LABEL_TITLE = new System.Windows.Forms.Label();
            this.bt_ptn9 = new r_framework.CustomControl.CustomButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.LABEL_KEIYAKUSHO_SELECT = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.PATTERN_FURIGANA = new r_framework.CustomControl.CustomTextBox();
            this.PATTERN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // bt_ptn12
            // 
            this.bt_ptn12.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_ptn12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn12.Location = new System.Drawing.Point(353, 165);
            this.bt_ptn12.Name = "bt_ptn12";
            this.bt_ptn12.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn12.Size = new System.Drawing.Size(100, 35);
            this.bt_ptn12.TabIndex = 3;
            this.bt_ptn12.Tag = "画面を閉じます";
            this.bt_ptn12.Text = "[F12] 閉じる";
            this.bt_ptn12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_ptn12.UseVisualStyleBackColor = true;
            // 
            // LABEL_TITLE
            // 
            this.LABEL_TITLE.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.LABEL_TITLE.Font = new System.Drawing.Font("ＭＳ ゴシック", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LABEL_TITLE.ForeColor = System.Drawing.Color.White;
            this.LABEL_TITLE.Location = new System.Drawing.Point(183, 33);
            this.LABEL_TITLE.Name = "LABEL_TITLE";
            this.LABEL_TITLE.Size = new System.Drawing.Size(350, 34);
            this.LABEL_TITLE.TabIndex = 8;
            this.LABEL_TITLE.Text = "最終処分場パターン登録";
            this.LABEL_TITLE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bt_ptn9
            // 
            this.bt_ptn9.DefaultBackColor = System.Drawing.Color.Empty;
            this.bt_ptn9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn9.Location = new System.Drawing.Point(247, 165);
            this.bt_ptn9.Name = "bt_ptn9";
            this.bt_ptn9.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.bt_ptn9.Size = new System.Drawing.Size(100, 35);
            this.bt_ptn9.TabIndex = 2;
            this.bt_ptn9.Tag = "パターンを登録します";
            this.bt_ptn9.Text = "[F9] 登録";
            this.bt_ptn9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.bt_ptn9.UseVisualStyleBackColor = true;
            // 
            // LABEL_KEIYAKUSHO_SELECT
            // 
            this.LABEL_KEIYAKUSHO_SELECT.AutoSize = true;
            this.LABEL_KEIYAKUSHO_SELECT.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.LABEL_KEIYAKUSHO_SELECT.Location = new System.Drawing.Point(17, 84);
            this.LABEL_KEIYAKUSHO_SELECT.Name = "LABEL_KEIYAKUSHO_SELECT";
            this.LABEL_KEIYAKUSHO_SELECT.Size = new System.Drawing.Size(203, 13);
            this.LABEL_KEIYAKUSHO_SELECT.TabIndex = 6;
            this.LABEL_KEIYAKUSHO_SELECT.Text = "パターン名を入力してください。";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 20);
            this.label1.TabIndex = 9;
            this.label1.Text = "フリガナ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 10;
            this.label2.Text = "パターン名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PATTERN_FURIGANA
            // 
            this.PATTERN_FURIGANA.BackColor = System.Drawing.SystemColors.Window;
            this.PATTERN_FURIGANA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PATTERN_FURIGANA.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.PATTERN_FURIGANA.DBFieldsName = "";
            this.PATTERN_FURIGANA.DefaultBackColor = System.Drawing.Color.Empty;
            this.PATTERN_FURIGANA.DisplayItemName = "フリガナ";
            this.PATTERN_FURIGANA.DisplayPopUp = null;
            this.PATTERN_FURIGANA.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_FURIGANA.FocusOutCheckMethod")));
            this.PATTERN_FURIGANA.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PATTERN_FURIGANA.ForeColor = System.Drawing.Color.Black;
            this.PATTERN_FURIGANA.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.PATTERN_FURIGANA.IsInputErrorOccured = false;
            this.PATTERN_FURIGANA.ItemDefinedTypes = "";
            this.PATTERN_FURIGANA.Location = new System.Drawing.Point(100, 106);
            this.PATTERN_FURIGANA.MaxLength = 40;
            this.PATTERN_FURIGANA.Name = "PATTERN_FURIGANA";
            this.PATTERN_FURIGANA.PopupAfterExecute = null;
            this.PATTERN_FURIGANA.PopupBeforeExecute = null;
            this.PATTERN_FURIGANA.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_FURIGANA.PopupSearchSendParams")));
            this.PATTERN_FURIGANA.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_FURIGANA.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_FURIGANA.popupWindowSetting")));
            this.PATTERN_FURIGANA.prevText = null;
            this.PATTERN_FURIGANA.PrevText = null;
            this.PATTERN_FURIGANA.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_FURIGANA.RegistCheckMethod")));
            this.PATTERN_FURIGANA.ShortItemName = "";
            this.PATTERN_FURIGANA.Size = new System.Drawing.Size(570, 20);
            this.PATTERN_FURIGANA.TabIndex = 1;
            this.PATTERN_FURIGANA.Tag = "全角４０文字以内で入力してください。";
            this.PATTERN_FURIGANA.Validating += new System.ComponentModel.CancelEventHandler(this.PATTERN_FURIGANA_Validating);
            // 
            // PATTERN_NAME
            // 
            this.PATTERN_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.PATTERN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PATTERN_NAME.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.PATTERN_NAME.DBFieldsName = "";
            this.PATTERN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.PATTERN_NAME.DisplayItemName = "パターン名";
            this.PATTERN_NAME.DisplayPopUp = null;
            this.PATTERN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.FocusOutCheckMethod")));
            this.PATTERN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PATTERN_NAME.ForeColor = System.Drawing.Color.Black;
            this.PATTERN_NAME.FuriganaAutoSetControl = "PATTERN_FURIGANA";
            this.PATTERN_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.PATTERN_NAME.IsInputErrorOccured = false;
            this.PATTERN_NAME.ItemDefinedTypes = "";
            this.PATTERN_NAME.Location = new System.Drawing.Point(100, 128);
            this.PATTERN_NAME.MaxLength = 40;
            this.PATTERN_NAME.Name = "PATTERN_NAME";
            this.PATTERN_NAME.PopupAfterExecute = null;
            this.PATTERN_NAME.PopupBeforeExecute = null;
            this.PATTERN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_NAME.PopupSearchSendParams")));
            this.PATTERN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_NAME.popupWindowSetting")));
            this.PATTERN_NAME.prevText = null;
            this.PATTERN_NAME.PrevText = null;
            this.PATTERN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.RegistCheckMethod")));
            this.PATTERN_NAME.ShortItemName = "";
            this.PATTERN_NAME.Size = new System.Drawing.Size(290, 20);
            this.PATTERN_NAME.TabIndex = 0;
            this.PATTERN_NAME.Tag = "全角２０文字以内で入力してください";
            // 
            // PatternRegistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(247)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(684, 237);
            this.Controls.Add(this.PATTERN_NAME);
            this.Controls.Add(this.PATTERN_FURIGANA);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.bt_ptn9);
            this.Controls.Add(this.LABEL_TITLE);
            this.Controls.Add(this.bt_ptn12);
            this.Controls.Add(this.LABEL_KEIYAKUSHO_SELECT);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "PatternRegistForm";
            this.Text = "最終処分場パターン登録";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public r_framework.CustomControl.CustomButton bt_ptn12;
        public r_framework.CustomControl.CustomButton bt_ptn9;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label LABEL_KEIYAKUSHO_SELECT;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal r_framework.CustomControl.CustomTextBox PATTERN_FURIGANA;
        internal r_framework.CustomControl.CustomTextBox PATTERN_NAME;
        internal System.Windows.Forms.Label LABEL_TITLE;
    }
}