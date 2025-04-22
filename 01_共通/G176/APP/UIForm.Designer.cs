namespace Shougun.Core.Common.KensakuKekkaIchiran
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
            this.SearchValue = new r_framework.CustomControl.CustomTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.CharactersNumber = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.searchString.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.searchString.MaxLength = 40;
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Tag = "検索文字列を入力してください";
            this.searchString.Visible = false;
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(4, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn1.TabIndex = 4;
            this.bt_ptn1.Text = "パターン1";
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn2.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn2.TabIndex = 5;
            this.bt_ptn2.Text = "パターン2";
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(404, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn3.TabIndex = 6;
            this.bt_ptn3.Text = "パターン3";
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(604, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn4.TabIndex = 7;
            this.bt_ptn4.Text = "パターン4";
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(804, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(194, 24);
            this.bt_ptn5.TabIndex = 8;
            this.bt_ptn5.Text = "パターン5";
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.AutoSize = true;
            this.customSortHeader1.Location = new System.Drawing.Point(3, 158);
            // 
            // SearchValue
            // 
            this.SearchValue.BackColor = System.Drawing.SystemColors.Window;
            this.SearchValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchValue.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.SearchValue.DefaultBackColor = System.Drawing.Color.Empty;
            this.SearchValue.DisplayItemName = "検索条件";
            this.SearchValue.DisplayPopUp = null;
            this.SearchValue.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SearchValue.FocusOutCheckMethod")));
            this.SearchValue.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SearchValue.ForeColor = System.Drawing.Color.Black;
            this.SearchValue.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.SearchValue.IsInputErrorOccured = false;
            this.SearchValue.ItemDefinedTypes = "varchar";
            this.SearchValue.Location = new System.Drawing.Point(119, 9);
            this.SearchValue.MaxLength = 40;
            this.SearchValue.Name = "SearchValue";
            this.SearchValue.PopupAfterExecute = null;
            this.SearchValue.PopupBeforeExecute = null;
            this.SearchValue.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SearchValue.PopupSearchSendParams")));
            this.SearchValue.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SearchValue.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SearchValue.popupWindowSetting")));
            this.SearchValue.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SearchValue.RegistCheckMethod")));
            this.SearchValue.Size = new System.Drawing.Size(568, 20);
            this.SearchValue.TabIndex = 559;
            this.SearchValue.Tag = "全角40文字以内で入力してください";
            this.SearchValue.Text = "あいうえおあいうえおあいうえおあいうえおあいうえおあいうえおあいうえおあいうえお";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(4, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 558;
            this.label4.Text = "検索条件";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 490);
            this.Controls.Add(this.SearchValue);
            this.Controls.Add(this.label4);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.customSearchHeader1, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.SearchValue, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox SearchValue;
        public System.Windows.Forms.Label label4;

    }
}