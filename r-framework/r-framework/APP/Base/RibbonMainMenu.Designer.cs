namespace r_framework.APP.Base
{
    partial class RibbonMainMenu
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
            // リボンのリソースを解放
            if (!this.ribbonMenu.IsDisposed)
            {
                this.ribbonMenu.Dispose();
            }

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RibbonMainMenu));
            this.ribbonMenu = new System.Windows.Forms.Ribbon();
            this.txb_quickSearch = new r_framework.CustomControl.CustomTextBox();
            this.seachButton = new r_framework.CustomControl.CustomButton();
            this.FocusBox = new r_framework.CustomControl.CustomTextBox();
            this.SuspendLayout();
            // 
            // ribbonMenu
            // 
            this.ribbonMenu.AllowMinimized = false;
            this.ribbonMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ribbonMenu.BackColor = System.Drawing.Color.White;
            this.ribbonMenu.CaptionBarVisible = false;
            this.ribbonMenu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F);
            this.ribbonMenu.Location = new System.Drawing.Point(0, 0);
            this.ribbonMenu.Minimized = false;
            this.ribbonMenu.Name = "ribbonMenu";
            // 
            // 
            // 
            this.ribbonMenu.OrbDropDown.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ribbonMenu.OrbDropDown.BackColor = System.Drawing.SystemColors.Window;
            this.ribbonMenu.OrbDropDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ribbonMenu.OrbDropDown.BorderRoundness = 8;
            this.ribbonMenu.OrbDropDown.CausesValidation = false;
            this.ribbonMenu.OrbDropDown.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ribbonMenu.OrbDropDown.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ribbonMenu.OrbDropDown.Location = new System.Drawing.Point(0, 0);
            this.ribbonMenu.OrbDropDown.Name = "";
            this.ribbonMenu.OrbDropDown.Size = new System.Drawing.Size(200, 400);
            this.ribbonMenu.OrbDropDown.TabIndex = 1;
            this.ribbonMenu.OrbDropDown.Closed += new System.EventHandler(this.OrbDropDown_Closed);
            this.ribbonMenu.OrbImage = null;
            this.ribbonMenu.OrbStyle = System.Windows.Forms.RibbonOrbStyle.Office_2010;
            this.ribbonMenu.OrbText = "マスタ";
            // 
            // 
            // 
            this.ribbonMenu.QuickAcessToolbar.Enabled = false;
            this.ribbonMenu.RibbonTabFont = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ribbonMenu.Size = new System.Drawing.Size(1180, 95);
            this.ribbonMenu.TabIndex = 3;
            this.ribbonMenu.TabsMargin = new System.Windows.Forms.Padding(10, 2, 0, 0);
            this.ribbonMenu.TabsPadding = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.ribbonMenu.ThemeColor = System.Windows.Forms.RibbonTheme.Blue;
            this.ribbonMenu.ActiveTabChanged += new System.EventHandler(this.ribbonMenu_ActiveTabChanged);
            // 
            // txb_quickSearch
            // 
            this.txb_quickSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txb_quickSearch.BackColor = System.Drawing.SystemColors.Window;
            this.txb_quickSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txb_quickSearch.CharactersNumber = new decimal(new int[] {
            32767,
            0,
            0,
            0});
            this.txb_quickSearch.DefaultBackColor = System.Drawing.Color.Empty;
            this.txb_quickSearch.DisplayPopUp = null;
            this.txb_quickSearch.ErrorMessage = "";
            this.txb_quickSearch.FocusOutCheckMethod = null;
            this.txb_quickSearch.ForeColor = System.Drawing.Color.Black;
            this.txb_quickSearch.GetCodeMasterField = "";
            this.txb_quickSearch.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.txb_quickSearch.IsInputErrorOccured = false;
            this.txb_quickSearch.Location = new System.Drawing.Point(956, 2);
            this.txb_quickSearch.MaxLength = 40;
            this.txb_quickSearch.Name = "txb_quickSearch";
            this.txb_quickSearch.PopupGetMasterField = "";
            this.txb_quickSearch.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("txb_quickSearch.PopupSearchSendParams")));
            this.txb_quickSearch.PopupSetFormField = "";
            this.txb_quickSearch.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.txb_quickSearch.popupWindowSetting = null;
            this.txb_quickSearch.RegistCheckMethod = null;
            this.txb_quickSearch.SetFormField = "";
            this.txb_quickSearch.Size = new System.Drawing.Size(200, 19);
            this.txb_quickSearch.TabIndex = 9;
            this.txb_quickSearch.Tag = "";
            // 
            // seachButton
            // 
            this.seachButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.seachButton.DefaultBackColor = System.Drawing.Color.Empty;
            this.seachButton.Image = global::r_framework.Properties.Resources.icon_search;
            this.seachButton.Location = new System.Drawing.Point(1158, 1);
            this.seachButton.Name = "seachButton";
            this.seachButton.ProcessKbn = r_framework.Const.PROCESS_KBN.NONE;
            this.seachButton.Size = new System.Drawing.Size(21, 21);
            this.seachButton.TabIndex = 10;
            this.seachButton.TabStop = false;
            this.seachButton.UseVisualStyleBackColor = true;
            this.seachButton.Click += new System.EventHandler(this.seachButton_Click);
            // 
            // FocusBox
            // 
            this.FocusBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.FocusBox.DefaultBackColor = System.Drawing.Color.Empty;
            this.FocusBox.DisplayPopUp = null;
            this.FocusBox.FocusOutCheckMethod = null;
            this.FocusBox.ForeColor = System.Drawing.Color.Black;
            this.FocusBox.IsInputErrorOccured = false;
            this.FocusBox.Location = new System.Drawing.Point(4, 3);
            this.FocusBox.Name = "FocusBox";
            this.FocusBox.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("FocusBox.PopupSearchSendParams")));
            this.FocusBox.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.FocusBox.popupWindowSetting = null;
            this.FocusBox.ReadOnly = true;
            this.FocusBox.RegistCheckMethod = null;
            this.FocusBox.Size = new System.Drawing.Size(58, 19);
            this.FocusBox.TabIndex = 1;
            this.FocusBox.GotFocus += new System.EventHandler(this.FocusBox_GotFocus);
            this.FocusBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FocusBox_KeyDown);
            this.FocusBox.LostFocus += new System.EventHandler(this.FocusBox_LostFocus);
            // 
            // RibbonMainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1180, 95);
            this.Controls.Add(this.seachButton);
            this.Controls.Add(this.txb_quickSearch);
            this.Controls.Add(this.ribbonMenu);
            this.Controls.Add(this.FocusBox);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "RibbonMainMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "環境将軍R";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Invalidated += new System.Windows.Forms.InvalidateEventHandler(this.Form_Invalidated);
            this.GotFocus += new System.EventHandler(this.RibbonMainMenu_GotFocus);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Ribbon ribbonMenu;
        private CustomControl.CustomTextBox txb_quickSearch;
        private CustomControl.CustomButton seachButton;
        private CustomControl.CustomTextBox FocusBox;
    }
}

