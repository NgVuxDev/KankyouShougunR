namespace Shougun.Core.PaperManifest.ManifestPattern
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
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PATTERN_NAME = new r_framework.CustomControl.CustomTextBox();
            this.HST_GYOUSHA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.HST_GENBA_NAME = new r_framework.CustomControl.CustomTextBox();
            this.cbtn_HaisyutuGyousyaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cbtn_HaisyutuJigyoubaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.cbtn_UnpanJyutaku1San = new r_framework.CustomControl.CustomPopupOpenButton();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.casbtn_SyobunJyutaku = new r_framework.CustomControl.CustomPopupOpenButton();
            this.HST_UNBAN_JUTAKU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.HST_SHOUBU_JUTAKU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.HST_SHOUBU_JIGYOU_NAME = new r_framework.CustomControl.CustomTextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cbtn_UnpanJyugyobaSan = new r_framework.CustomControl.CustomPopupOpenButton();
            this.ctxt_ShobungyoshaName = new r_framework.CustomControl.CustomTextBox();
            this.ctxt_UnpangyoshaName = new r_framework.CustomControl.CustomTextBox();
            this.ctxt_HaisyutugenbaName = new r_framework.CustomControl.CustomTextBox();
            this.ctxt_HaisyutugyoshaName = new r_framework.CustomControl.CustomTextBox();
            this.cantxt_HaisyutugyoshaCD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.cantxt_UnpangyoshaCD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.cantxt_ShobungyoshaCD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.ctxt_ShobunGenbaName = new r_framework.CustomControl.CustomTextBox();
            this.cantxt_ShobunGenbaCD = new CustomerControls_Ex.CustomAlphaNumTextBox_Ex();
            this.cantxt_HaisyutugenbaCD = new CustomerControls_Ex.CustomAlphaNumTextBox_Ex();
            this.SuspendLayout();
            // 
            // searchString
            // 
            this.searchString.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.searchString.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.searchString.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.FocusOutCheckMethod")));
            this.searchString.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.searchString.ForeColor = System.Drawing.Color.Black;
            this.searchString.Location = new System.Drawing.Point(76, 228);
            this.searchString.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("searchString.PopupSearchSendParams")));
            this.searchString.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("searchString.popupWindowSetting")));
            this.searchString.ReadOnly = true;
            this.searchString.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("searchString.RegistCheckMethod")));
            this.searchString.Size = new System.Drawing.Size(302, 157);
            this.searchString.TabStop = false;
            this.searchString.Tag = "検索条件設定画面で設定した条件が表示されます";
            this.searchString.Text = "【検索条件】\r\n業者略称名　＝　ホームズ産業　かつ\r\n現場略称名　＝　県営野球場建設工事　かつ\r\n（運転者名　=　山田　または　運転者名　=　財津　竜太）";
            // 
            // bt_ptn1
            // 
            this.bt_ptn1.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn1.Location = new System.Drawing.Point(1, 427);
            this.bt_ptn1.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn1.TabIndex = 205;
            this.bt_ptn1.TabStop = false;
            this.bt_ptn1.Text = "パターン1";
            this.bt_ptn1.Click += new System.EventHandler(this.bt_ptn1_Click);
            // 
            // bt_ptn2
            // 
            this.bt_ptn2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn2.Location = new System.Drawing.Point(201, 427);
            this.bt_ptn2.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn2.TabIndex = 206;
            this.bt_ptn2.TabStop = false;
            this.bt_ptn2.Text = "パターン2";
            this.bt_ptn2.Click += new System.EventHandler(this.bt_ptn2_Click);
            // 
            // bt_ptn3
            // 
            this.bt_ptn3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn3.Location = new System.Drawing.Point(401, 427);
            this.bt_ptn3.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn3.TabIndex = 207;
            this.bt_ptn3.TabStop = false;
            this.bt_ptn3.Text = "パターン3";
            this.bt_ptn3.Click += new System.EventHandler(this.bt_ptn3_Click);
            // 
            // bt_ptn4
            // 
            this.bt_ptn4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn4.Location = new System.Drawing.Point(601, 427);
            this.bt_ptn4.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn4.TabIndex = 208;
            this.bt_ptn4.TabStop = false;
            this.bt_ptn4.Text = "パターン4";
            this.bt_ptn4.Click += new System.EventHandler(this.bt_ptn4_Click);
            // 
            // bt_ptn5
            // 
            this.bt_ptn5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.bt_ptn5.Location = new System.Drawing.Point(801, 427);
            this.bt_ptn5.Size = new System.Drawing.Size(198, 22);
            this.bt_ptn5.TabIndex = 209;
            this.bt_ptn5.TabStop = false;
            this.bt_ptn5.Text = "パターン5";
            this.bt_ptn5.Click += new System.EventHandler(this.bt_ptn5_Click);
            // 
            // customSortHeader1
            // 
            this.customSortHeader1.AutoScroll = true;
            this.customSortHeader1.Location = new System.Drawing.Point(3, 158);
            this.customSortHeader1.Size = new System.Drawing.Size(997, 24);
            this.customSortHeader1.TabIndex = 301;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(589, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 20);
            this.label2.TabIndex = 407;
            this.label2.Text = "パターン名";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(589, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 20);
            this.label3.TabIndex = 408;
            this.label3.Text = "排出事業者名";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(589, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(110, 20);
            this.label4.TabIndex = 409;
            this.label4.Text = "排出事業場名";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PATTERN_NAME
            // 
            this.PATTERN_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.PATTERN_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PATTERN_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.PATTERN_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.PATTERN_NAME.DisplayItemName = "パターン名";
            this.PATTERN_NAME.DisplayPopUp = null;
            this.PATTERN_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.FocusOutCheckMethod")));
            this.PATTERN_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.PATTERN_NAME.ForeColor = System.Drawing.Color.Black;
            this.PATTERN_NAME.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.PATTERN_NAME.IsInputErrorOccured = false;
            this.PATTERN_NAME.Location = new System.Drawing.Point(704, 22);
            this.PATTERN_NAME.Name = "PATTERN_NAME";
            this.PATTERN_NAME.PopupAfterExecute = null;
            this.PATTERN_NAME.PopupBeforeExecute = null;
            this.PATTERN_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("PATTERN_NAME.PopupSearchSendParams")));
            this.PATTERN_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.PATTERN_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("PATTERN_NAME.popupWindowSetting")));
            this.PATTERN_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("PATTERN_NAME.RegistCheckMethod")));
            this.PATTERN_NAME.ShortItemName = "パターン名";
            this.PATTERN_NAME.Size = new System.Drawing.Size(220, 20);
            this.PATTERN_NAME.TabIndex = 105;
            this.PATTERN_NAME.TextChanged += new System.EventHandler(this.PATTERN_NAME_TextChanged);
            this.PATTERN_NAME.Validating += new System.ComponentModel.CancelEventHandler(this.PATTERN_NAME_Validating);
            // 
            // HST_GYOUSHA_NAME
            // 
            this.HST_GYOUSHA_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.HST_GYOUSHA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HST_GYOUSHA_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.HST_GYOUSHA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HST_GYOUSHA_NAME.DisplayItemName = "排出事業者名";
            this.HST_GYOUSHA_NAME.DisplayPopUp = null;
            this.HST_GYOUSHA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_GYOUSHA_NAME.FocusOutCheckMethod")));
            this.HST_GYOUSHA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HST_GYOUSHA_NAME.ForeColor = System.Drawing.Color.Black;
            this.HST_GYOUSHA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HST_GYOUSHA_NAME.IsInputErrorOccured = false;
            this.HST_GYOUSHA_NAME.Location = new System.Drawing.Point(704, 44);
            this.HST_GYOUSHA_NAME.Name = "HST_GYOUSHA_NAME";
            this.HST_GYOUSHA_NAME.PopupAfterExecute = null;
            this.HST_GYOUSHA_NAME.PopupBeforeExecute = null;
            this.HST_GYOUSHA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HST_GYOUSHA_NAME.PopupSearchSendParams")));
            this.HST_GYOUSHA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HST_GYOUSHA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HST_GYOUSHA_NAME.popupWindowSetting")));
            this.HST_GYOUSHA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_GYOUSHA_NAME.RegistCheckMethod")));
            this.HST_GYOUSHA_NAME.ShortItemName = "排出事業者名";
            this.HST_GYOUSHA_NAME.Size = new System.Drawing.Size(220, 20);
            this.HST_GYOUSHA_NAME.TabIndex = 106;
            this.HST_GYOUSHA_NAME.TextChanged += new System.EventHandler(this.HST_GYOUSHA_NAME_TextChanged);
            this.HST_GYOUSHA_NAME.Validating += new System.ComponentModel.CancelEventHandler(this.HST_GYOUSHA_NAME_Validating);
            // 
            // HST_GENBA_NAME
            // 
            this.HST_GENBA_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.HST_GENBA_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HST_GENBA_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.HST_GENBA_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HST_GENBA_NAME.DisplayItemName = "排出事業場名";
            this.HST_GENBA_NAME.DisplayPopUp = null;
            this.HST_GENBA_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_GENBA_NAME.FocusOutCheckMethod")));
            this.HST_GENBA_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HST_GENBA_NAME.ForeColor = System.Drawing.Color.Black;
            this.HST_GENBA_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HST_GENBA_NAME.IsInputErrorOccured = false;
            this.HST_GENBA_NAME.Location = new System.Drawing.Point(704, 66);
            this.HST_GENBA_NAME.Name = "HST_GENBA_NAME";
            this.HST_GENBA_NAME.PopupAfterExecute = null;
            this.HST_GENBA_NAME.PopupBeforeExecute = null;
            this.HST_GENBA_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HST_GENBA_NAME.PopupSearchSendParams")));
            this.HST_GENBA_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HST_GENBA_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HST_GENBA_NAME.popupWindowSetting")));
            this.HST_GENBA_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_GENBA_NAME.RegistCheckMethod")));
            this.HST_GENBA_NAME.ShortItemName = "排出事業場名";
            this.HST_GENBA_NAME.Size = new System.Drawing.Size(220, 20);
            this.HST_GENBA_NAME.TabIndex = 107;
            this.HST_GENBA_NAME.TextChanged += new System.EventHandler(this.HST_GENBA_NAME_TextChanged);
            this.HST_GENBA_NAME.Validating += new System.ComponentModel.CancelEventHandler(this.HST_GENBA_NAME_Validating);
            // 
            // cbtn_HaisyutuGyousyaSan
            // 
            this.cbtn_HaisyutuGyousyaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_HaisyutuGyousyaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_HaisyutuGyousyaSan.DBFieldsName = null;
            this.cbtn_HaisyutuGyousyaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_HaisyutuGyousyaSan.DisplayItemName = null;
            this.cbtn_HaisyutuGyousyaSan.DisplayPopUp = null;
            this.cbtn_HaisyutuGyousyaSan.ErrorMessage = null;
            this.cbtn_HaisyutuGyousyaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.FocusOutCheckMethod")));
            this.cbtn_HaisyutuGyousyaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_HaisyutuGyousyaSan.GetCodeMasterField = null;
            this.cbtn_HaisyutuGyousyaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_HaisyutuGyousyaSan.Image")));
            this.cbtn_HaisyutuGyousyaSan.ItemDefinedTypes = null;
            this.cbtn_HaisyutuGyousyaSan.LinkedSettingTextBox = null;
            this.cbtn_HaisyutuGyousyaSan.LinkedTextBoxs = null;
            this.cbtn_HaisyutuGyousyaSan.Location = new System.Drawing.Point(449, 21);
            this.cbtn_HaisyutuGyousyaSan.Name = "cbtn_HaisyutuGyousyaSan";
            this.cbtn_HaisyutuGyousyaSan.PopupAfterExecute = null;
            this.cbtn_HaisyutuGyousyaSan.PopupAfterExecuteMethod = "cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod";
            this.cbtn_HaisyutuGyousyaSan.PopupBeforeExecute = null;
            this.cbtn_HaisyutuGyousyaSan.PopupBeforeExecuteMethod = "cantxt_HaisyutuGyousyaCd_PopupBeforeExecuteMethod";
            this.cbtn_HaisyutuGyousyaSan.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cbtn_HaisyutuGyousyaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.PopupSearchSendParams")));
            this.cbtn_HaisyutuGyousyaSan.PopupSetFormField = "cantxt_HaisyutugyoshaCD,ctxt_HaisyutugyoshaName";
            this.cbtn_HaisyutuGyousyaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cbtn_HaisyutuGyousyaSan.PopupWindowName = "検索共通ポップアップ";
            this.cbtn_HaisyutuGyousyaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.popupWindowSetting")));
            this.cbtn_HaisyutuGyousyaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuGyousyaSan.RegistCheckMethod")));
            this.cbtn_HaisyutuGyousyaSan.SearchDisplayFlag = 0;
            this.cbtn_HaisyutuGyousyaSan.SetFormField = "cantxt_HaisyutugyoshaCD,ctxt_HaisyutugyoshaName";
            this.cbtn_HaisyutuGyousyaSan.ShortItemName = null;
            this.cbtn_HaisyutuGyousyaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_HaisyutuGyousyaSan.TabIndex = 414;
            this.cbtn_HaisyutuGyousyaSan.TabStop = false;
            this.cbtn_HaisyutuGyousyaSan.Tag = "排出事業者の検索を行います";
            this.cbtn_HaisyutuGyousyaSan.Text = " ";
            this.cbtn_HaisyutuGyousyaSan.UseVisualStyleBackColor = false;
            this.cbtn_HaisyutuGyousyaSan.ZeroPaddengFlag = false;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label6.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(22, 22);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(110, 20);
            this.label6.TabIndex = 400;
            this.label6.Text = "排出事業者";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(22, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 20);
            this.label7.TabIndex = 401;
            this.label7.Text = "排出事業場";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbtn_HaisyutuJigyoubaSan
            // 
            this.cbtn_HaisyutuJigyoubaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_HaisyutuJigyoubaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_HaisyutuJigyoubaSan.DBFieldsName = null;
            this.cbtn_HaisyutuJigyoubaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_HaisyutuJigyoubaSan.DisplayItemName = null;
            this.cbtn_HaisyutuJigyoubaSan.DisplayPopUp = null;
            this.cbtn_HaisyutuJigyoubaSan.ErrorMessage = null;
            this.cbtn_HaisyutuJigyoubaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.FocusOutCheckMethod")));
            this.cbtn_HaisyutuJigyoubaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_HaisyutuJigyoubaSan.GetCodeMasterField = null;
            this.cbtn_HaisyutuJigyoubaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.Image")));
            this.cbtn_HaisyutuJigyoubaSan.ItemDefinedTypes = null;
            this.cbtn_HaisyutuJigyoubaSan.LinkedSettingTextBox = null;
            this.cbtn_HaisyutuJigyoubaSan.LinkedTextBoxs = null;
            this.cbtn_HaisyutuJigyoubaSan.Location = new System.Drawing.Point(449, 44);
            this.cbtn_HaisyutuJigyoubaSan.Name = "cbtn_HaisyutuJigyoubaSan";
            this.cbtn_HaisyutuJigyoubaSan.PopupAfterExecute = null;
            this.cbtn_HaisyutuJigyoubaSan.PopupAfterExecuteMethod = "cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod";
            this.cbtn_HaisyutuJigyoubaSan.PopupBeforeExecute = null;
            this.cbtn_HaisyutuJigyoubaSan.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_POST,GENBA_TEL,GENBA_ADDRESS1,GYOUSHA_CD,GYOUSHA_" +
                "NAME_RYAKU";
            this.cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.PopupSearchSendParams")));
            this.cbtn_HaisyutuJigyoubaSan.PopupSetFormField = "cantxt_HaisyutugenbaCD,ctxt_HaisyutugenbaName";
            this.cbtn_HaisyutuJigyoubaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cbtn_HaisyutuJigyoubaSan.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cbtn_HaisyutuJigyoubaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.popupWindowSetting")));
            this.cbtn_HaisyutuJigyoubaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_HaisyutuJigyoubaSan.RegistCheckMethod")));
            this.cbtn_HaisyutuJigyoubaSan.SearchDisplayFlag = 0;
            this.cbtn_HaisyutuJigyoubaSan.SetFormField = "cantxt_HaisyutugenbaCD,ctxt_HaisyutugenbaName";
            this.cbtn_HaisyutuJigyoubaSan.ShortItemName = null;
            this.cbtn_HaisyutuJigyoubaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_HaisyutuJigyoubaSan.TabIndex = 415;
            this.cbtn_HaisyutuJigyoubaSan.TabStop = false;
            this.cbtn_HaisyutuJigyoubaSan.Tag = "排出事業場の検索を行います";
            this.cbtn_HaisyutuJigyoubaSan.Text = " ";
            this.cbtn_HaisyutuJigyoubaSan.UseVisualStyleBackColor = false;
            this.cbtn_HaisyutuJigyoubaSan.ZeroPaddengFlag = false;
            // 
            // cbtn_UnpanJyutaku1San
            // 
            this.cbtn_UnpanJyutaku1San.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_UnpanJyutaku1San.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_UnpanJyutaku1San.DBFieldsName = null;
            this.cbtn_UnpanJyutaku1San.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_UnpanJyutaku1San.DisplayItemName = null;
            this.cbtn_UnpanJyutaku1San.DisplayPopUp = null;
            this.cbtn_UnpanJyutaku1San.ErrorMessage = null;
            this.cbtn_UnpanJyutaku1San.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_UnpanJyutaku1San.FocusOutCheckMethod")));
            this.cbtn_UnpanJyutaku1San.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_UnpanJyutaku1San.GetCodeMasterField = null;
            this.cbtn_UnpanJyutaku1San.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_UnpanJyutaku1San.Image")));
            this.cbtn_UnpanJyutaku1San.ItemDefinedTypes = null;
            this.cbtn_UnpanJyutaku1San.LinkedSettingTextBox = null;
            this.cbtn_UnpanJyutaku1San.LinkedTextBoxs = null;
            this.cbtn_UnpanJyutaku1San.Location = new System.Drawing.Point(449, 67);
            this.cbtn_UnpanJyutaku1San.Name = "cbtn_UnpanJyutaku1San";
            this.cbtn_UnpanJyutaku1San.PopupAfterExecute = null;
            this.cbtn_UnpanJyutaku1San.PopupAfterExecuteMethod = "cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod";
            this.cbtn_UnpanJyutaku1San.PopupBeforeExecute = null;
            this.cbtn_UnpanJyutaku1San.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cbtn_UnpanJyutaku1San.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_UnpanJyutaku1San.PopupSearchSendParams")));
            this.cbtn_UnpanJyutaku1San.PopupSetFormField = "cantxt_UnpangyoshaCD,ctxt_UnpangyoshaName";
            this.cbtn_UnpanJyutaku1San.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cbtn_UnpanJyutaku1San.PopupWindowName = "検索共通ポップアップ";
            this.cbtn_UnpanJyutaku1San.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_UnpanJyutaku1San.popupWindowSetting")));
            this.cbtn_UnpanJyutaku1San.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_UnpanJyutaku1San.RegistCheckMethod")));
            this.cbtn_UnpanJyutaku1San.SearchDisplayFlag = 0;
            this.cbtn_UnpanJyutaku1San.SetFormField = "cantxt_UnpangyoshaCD,ctxt_UnpangyoshaName";
            this.cbtn_UnpanJyutaku1San.ShortItemName = null;
            this.cbtn_UnpanJyutaku1San.Size = new System.Drawing.Size(22, 22);
            this.cbtn_UnpanJyutaku1San.TabIndex = 416;
            this.cbtn_UnpanJyutaku1San.TabStop = false;
            this.cbtn_UnpanJyutaku1San.Tag = "運搬受託者の検索を行います";
            this.cbtn_UnpanJyutaku1San.Text = " ";
            this.cbtn_UnpanJyutaku1San.UseVisualStyleBackColor = false;
            this.cbtn_UnpanJyutaku1San.ZeroPaddengFlag = false;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label5.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(22, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(110, 20);
            this.label5.TabIndex = 402;
            this.label5.Text = "運搬受託者";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label8.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label8.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(22, 91);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(110, 20);
            this.label8.TabIndex = 403;
            this.label8.Text = "処分受託者";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label9.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label9.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(22, 114);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 20);
            this.label9.TabIndex = 405;
            this.label9.Text = "処分事業場";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label10.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(589, 88);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(110, 20);
            this.label10.TabIndex = 410;
            this.label10.Text = "運搬受託者名";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label11.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label11.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label11.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(589, 132);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(110, 20);
            this.label11.TabIndex = 413;
            this.label11.Text = "処分事業場名";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.label12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label12.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(589, 110);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(110, 20);
            this.label12.TabIndex = 412;
            this.label12.Text = "処分受託者名";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // casbtn_SyobunJyutaku
            // 
            this.casbtn_SyobunJyutaku.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.casbtn_SyobunJyutaku.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.casbtn_SyobunJyutaku.DBFieldsName = null;
            this.casbtn_SyobunJyutaku.DefaultBackColor = System.Drawing.Color.Empty;
            this.casbtn_SyobunJyutaku.DisplayItemName = null;
            this.casbtn_SyobunJyutaku.DisplayPopUp = null;
            this.casbtn_SyobunJyutaku.ErrorMessage = null;
            this.casbtn_SyobunJyutaku.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("casbtn_SyobunJyutaku.FocusOutCheckMethod")));
            this.casbtn_SyobunJyutaku.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.casbtn_SyobunJyutaku.GetCodeMasterField = null;
            this.casbtn_SyobunJyutaku.Image = ((System.Drawing.Image)(resources.GetObject("casbtn_SyobunJyutaku.Image")));
            this.casbtn_SyobunJyutaku.ItemDefinedTypes = null;
            this.casbtn_SyobunJyutaku.LinkedSettingTextBox = null;
            this.casbtn_SyobunJyutaku.LinkedTextBoxs = null;
            this.casbtn_SyobunJyutaku.Location = new System.Drawing.Point(449, 90);
            this.casbtn_SyobunJyutaku.Name = "casbtn_SyobunJyutaku";
            this.casbtn_SyobunJyutaku.PopupAfterExecute = null;
            this.casbtn_SyobunJyutaku.PopupAfterExecuteMethod = "cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod";
            this.casbtn_SyobunJyutaku.PopupBeforeExecute = null;
            this.casbtn_SyobunJyutaku.PopupBeforeExecuteMethod = "cantxt_SyobunJyutakuNameCd_PopupBeforeExecuteMethod";
            this.casbtn_SyobunJyutaku.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.casbtn_SyobunJyutaku.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("casbtn_SyobunJyutaku.PopupSearchSendParams")));
            this.casbtn_SyobunJyutaku.PopupSetFormField = "cantxt_ShobungyoshaCD,ctxt_ShobungyoshaName";
            this.casbtn_SyobunJyutaku.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.casbtn_SyobunJyutaku.PopupWindowName = "検索共通ポップアップ";
            this.casbtn_SyobunJyutaku.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("casbtn_SyobunJyutaku.popupWindowSetting")));
            this.casbtn_SyobunJyutaku.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("casbtn_SyobunJyutaku.RegistCheckMethod")));
            this.casbtn_SyobunJyutaku.SearchDisplayFlag = 0;
            this.casbtn_SyobunJyutaku.SetFormField = "cantxt_ShobungyoshaCD,ctxt_ShobungyoshaName";
            this.casbtn_SyobunJyutaku.ShortItemName = null;
            this.casbtn_SyobunJyutaku.Size = new System.Drawing.Size(22, 22);
            this.casbtn_SyobunJyutaku.TabIndex = 417;
            this.casbtn_SyobunJyutaku.TabStop = false;
            this.casbtn_SyobunJyutaku.Tag = "処分受託者の検索を行います";
            this.casbtn_SyobunJyutaku.Text = " ";
            this.casbtn_SyobunJyutaku.UseVisualStyleBackColor = false;
            this.casbtn_SyobunJyutaku.ZeroPaddengFlag = false;
            // 
            // HST_UNBAN_JUTAKU_NAME
            // 
            this.HST_UNBAN_JUTAKU_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.HST_UNBAN_JUTAKU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HST_UNBAN_JUTAKU_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.HST_UNBAN_JUTAKU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HST_UNBAN_JUTAKU_NAME.DisplayItemName = "排出事業場名";
            this.HST_UNBAN_JUTAKU_NAME.DisplayPopUp = null;
            this.HST_UNBAN_JUTAKU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_UNBAN_JUTAKU_NAME.FocusOutCheckMethod")));
            this.HST_UNBAN_JUTAKU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HST_UNBAN_JUTAKU_NAME.ForeColor = System.Drawing.Color.Black;
            this.HST_UNBAN_JUTAKU_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HST_UNBAN_JUTAKU_NAME.IsInputErrorOccured = false;
            this.HST_UNBAN_JUTAKU_NAME.Location = new System.Drawing.Point(704, 88);
            this.HST_UNBAN_JUTAKU_NAME.Name = "HST_UNBAN_JUTAKU_NAME";
            this.HST_UNBAN_JUTAKU_NAME.PopupAfterExecute = null;
            this.HST_UNBAN_JUTAKU_NAME.PopupBeforeExecute = null;
            this.HST_UNBAN_JUTAKU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HST_UNBAN_JUTAKU_NAME.PopupSearchSendParams")));
            this.HST_UNBAN_JUTAKU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HST_UNBAN_JUTAKU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HST_UNBAN_JUTAKU_NAME.popupWindowSetting")));
            this.HST_UNBAN_JUTAKU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_UNBAN_JUTAKU_NAME.RegistCheckMethod")));
            this.HST_UNBAN_JUTAKU_NAME.ShortItemName = "運搬受託者名";
            this.HST_UNBAN_JUTAKU_NAME.Size = new System.Drawing.Size(220, 20);
            this.HST_UNBAN_JUTAKU_NAME.TabIndex = 108;
            this.HST_UNBAN_JUTAKU_NAME.Tag = "運搬受託者名は20文字以内で入力してください。";
            this.HST_UNBAN_JUTAKU_NAME.TextChanged += new System.EventHandler(this.HST_GENBA_NAME_TextChanged);
            this.HST_UNBAN_JUTAKU_NAME.Validating += new System.ComponentModel.CancelEventHandler(this.HST_GENBA_NAME_Validating);
            // 
            // HST_SHOUBU_JUTAKU_NAME
            // 
            this.HST_SHOUBU_JUTAKU_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.HST_SHOUBU_JUTAKU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HST_SHOUBU_JUTAKU_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.HST_SHOUBU_JUTAKU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HST_SHOUBU_JUTAKU_NAME.DisplayItemName = "排出事業場名";
            this.HST_SHOUBU_JUTAKU_NAME.DisplayPopUp = null;
            this.HST_SHOUBU_JUTAKU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_SHOUBU_JUTAKU_NAME.FocusOutCheckMethod")));
            this.HST_SHOUBU_JUTAKU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HST_SHOUBU_JUTAKU_NAME.ForeColor = System.Drawing.Color.Black;
            this.HST_SHOUBU_JUTAKU_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HST_SHOUBU_JUTAKU_NAME.IsInputErrorOccured = false;
            this.HST_SHOUBU_JUTAKU_NAME.Location = new System.Drawing.Point(704, 110);
            this.HST_SHOUBU_JUTAKU_NAME.Name = "HST_SHOUBU_JUTAKU_NAME";
            this.HST_SHOUBU_JUTAKU_NAME.PopupAfterExecute = null;
            this.HST_SHOUBU_JUTAKU_NAME.PopupBeforeExecute = null;
            this.HST_SHOUBU_JUTAKU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HST_SHOUBU_JUTAKU_NAME.PopupSearchSendParams")));
            this.HST_SHOUBU_JUTAKU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HST_SHOUBU_JUTAKU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HST_SHOUBU_JUTAKU_NAME.popupWindowSetting")));
            this.HST_SHOUBU_JUTAKU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_SHOUBU_JUTAKU_NAME.RegistCheckMethod")));
            this.HST_SHOUBU_JUTAKU_NAME.ShortItemName = "処分受託者名";
            this.HST_SHOUBU_JUTAKU_NAME.Size = new System.Drawing.Size(220, 20);
            this.HST_SHOUBU_JUTAKU_NAME.TabIndex = 109;
            this.HST_SHOUBU_JUTAKU_NAME.Tag = "処分受託者名は20文字以内で入力してください。";
            this.HST_SHOUBU_JUTAKU_NAME.TextChanged += new System.EventHandler(this.HST_GENBA_NAME_TextChanged);
            this.HST_SHOUBU_JUTAKU_NAME.Validating += new System.ComponentModel.CancelEventHandler(this.HST_GENBA_NAME_Validating);
            // 
            // HST_SHOUBU_JIGYOU_NAME
            // 
            this.HST_SHOUBU_JIGYOU_NAME.BackColor = System.Drawing.SystemColors.Window;
            this.HST_SHOUBU_JIGYOU_NAME.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.HST_SHOUBU_JIGYOU_NAME.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.HST_SHOUBU_JIGYOU_NAME.DefaultBackColor = System.Drawing.Color.Empty;
            this.HST_SHOUBU_JIGYOU_NAME.DisplayItemName = "排出事業場名";
            this.HST_SHOUBU_JIGYOU_NAME.DisplayPopUp = null;
            this.HST_SHOUBU_JIGYOU_NAME.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_SHOUBU_JIGYOU_NAME.FocusOutCheckMethod")));
            this.HST_SHOUBU_JIGYOU_NAME.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.HST_SHOUBU_JIGYOU_NAME.ForeColor = System.Drawing.Color.Black;
            this.HST_SHOUBU_JIGYOU_NAME.ImeMode = System.Windows.Forms.ImeMode.Hiragana;
            this.HST_SHOUBU_JIGYOU_NAME.IsInputErrorOccured = false;
            this.HST_SHOUBU_JIGYOU_NAME.Location = new System.Drawing.Point(704, 132);
            this.HST_SHOUBU_JIGYOU_NAME.Name = "HST_SHOUBU_JIGYOU_NAME";
            this.HST_SHOUBU_JIGYOU_NAME.PopupAfterExecute = null;
            this.HST_SHOUBU_JIGYOU_NAME.PopupBeforeExecute = null;
            this.HST_SHOUBU_JIGYOU_NAME.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("HST_SHOUBU_JIGYOU_NAME.PopupSearchSendParams")));
            this.HST_SHOUBU_JIGYOU_NAME.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.HST_SHOUBU_JIGYOU_NAME.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("HST_SHOUBU_JIGYOU_NAME.popupWindowSetting")));
            this.HST_SHOUBU_JIGYOU_NAME.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("HST_SHOUBU_JIGYOU_NAME.RegistCheckMethod")));
            this.HST_SHOUBU_JIGYOU_NAME.ShortItemName = "処分事業場名";
            this.HST_SHOUBU_JIGYOU_NAME.Size = new System.Drawing.Size(220, 20);
            this.HST_SHOUBU_JIGYOU_NAME.TabIndex = 110;
            this.HST_SHOUBU_JIGYOU_NAME.Tag = "処分事業場名は20文字以内で入力してください。";
            this.HST_SHOUBU_JIGYOU_NAME.TextChanged += new System.EventHandler(this.HST_GENBA_NAME_TextChanged);
            this.HST_SHOUBU_JIGYOU_NAME.Validating += new System.ComponentModel.CancelEventHandler(this.HST_GENBA_NAME_Validating);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("ＭＳ ゴシック", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label13.Location = new System.Drawing.Point(587, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 11);
            this.label13.TabIndex = 406;
            this.label13.Text = "※カナ検索";
            // 
            // cbtn_UnpanJyugyobaSan
            // 
            this.cbtn_UnpanJyugyobaSan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.cbtn_UnpanJyugyobaSan.CharactersNumber = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.cbtn_UnpanJyugyobaSan.DBFieldsName = null;
            this.cbtn_UnpanJyugyobaSan.DefaultBackColor = System.Drawing.Color.Empty;
            this.cbtn_UnpanJyugyobaSan.DisplayItemName = null;
            this.cbtn_UnpanJyugyobaSan.DisplayPopUp = null;
            this.cbtn_UnpanJyugyobaSan.ErrorMessage = null;
            this.cbtn_UnpanJyugyobaSan.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_UnpanJyugyobaSan.FocusOutCheckMethod")));
            this.cbtn_UnpanJyugyobaSan.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cbtn_UnpanJyugyobaSan.GetCodeMasterField = null;
            this.cbtn_UnpanJyugyobaSan.Image = ((System.Drawing.Image)(resources.GetObject("cbtn_UnpanJyugyobaSan.Image")));
            this.cbtn_UnpanJyugyobaSan.ItemDefinedTypes = null;
            this.cbtn_UnpanJyugyobaSan.LinkedSettingTextBox = null;
            this.cbtn_UnpanJyugyobaSan.LinkedTextBoxs = null;
            this.cbtn_UnpanJyugyobaSan.Location = new System.Drawing.Point(449, 113);
            this.cbtn_UnpanJyugyobaSan.Name = "cbtn_UnpanJyugyobaSan";
            this.cbtn_UnpanJyugyobaSan.PopupAfterExecute = null;
            this.cbtn_UnpanJyugyobaSan.PopupAfterExecuteMethod = "cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod";
            this.cbtn_UnpanJyugyobaSan.PopupBeforeExecute = null;
            this.cbtn_UnpanJyugyobaSan.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_POST,GENBA_TEL,GENBA_ADDRESS1,GYOUSHA_CD,GYOUSHA_" +
                "CD,GYOUSHA_NAME_RYAKU";
            this.cbtn_UnpanJyugyobaSan.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cbtn_UnpanJyugyobaSan.PopupSearchSendParams")));
            this.cbtn_UnpanJyugyobaSan.PopupSetFormField = "cantxt_ShobunGenbaCD,ctxt_ShobunGenbaName";
            this.cbtn_UnpanJyugyobaSan.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cbtn_UnpanJyugyobaSan.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cbtn_UnpanJyugyobaSan.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cbtn_UnpanJyugyobaSan.popupWindowSetting")));
            this.cbtn_UnpanJyugyobaSan.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cbtn_UnpanJyugyobaSan.RegistCheckMethod")));
            this.cbtn_UnpanJyugyobaSan.SearchDisplayFlag = 0;
            this.cbtn_UnpanJyugyobaSan.SetFormField = "cantxt_ShobunGenbaCD,ctxt_ShobunGenbaName";
            this.cbtn_UnpanJyugyobaSan.ShortItemName = null;
            this.cbtn_UnpanJyugyobaSan.Size = new System.Drawing.Size(22, 22);
            this.cbtn_UnpanJyugyobaSan.TabIndex = 418;
            this.cbtn_UnpanJyugyobaSan.TabStop = false;
            this.cbtn_UnpanJyugyobaSan.Tag = "処分事業場の検索を行います";
            this.cbtn_UnpanJyugyobaSan.Text = " ";
            this.cbtn_UnpanJyugyobaSan.UseVisualStyleBackColor = false;
            this.cbtn_UnpanJyugyobaSan.ZeroPaddengFlag = false;
            // 
            // ctxt_ShobungyoshaName
            // 
            this.ctxt_ShobungyoshaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_ShobungyoshaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_ShobungyoshaName.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.ctxt_ShobungyoshaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_ShobungyoshaName.DisplayPopUp = null;
            this.ctxt_ShobungyoshaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_ShobungyoshaName.FocusOutCheckMethod")));
            this.ctxt_ShobungyoshaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.ctxt_ShobungyoshaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_ShobungyoshaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_ShobungyoshaName.IsInputErrorOccured = false;
            this.ctxt_ShobungyoshaName.Location = new System.Drawing.Point(196, 91);
            this.ctxt_ShobungyoshaName.MaxLength = 40;
            this.ctxt_ShobungyoshaName.Name = "ctxt_ShobungyoshaName";
            this.ctxt_ShobungyoshaName.PopupAfterExecute = null;
            this.ctxt_ShobungyoshaName.PopupBeforeExecute = null;
            this.ctxt_ShobungyoshaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_ShobungyoshaName.PopupSearchSendParams")));
            this.ctxt_ShobungyoshaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_ShobungyoshaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_ShobungyoshaName.popupWindowSetting")));
            this.ctxt_ShobungyoshaName.ReadOnly = true;
            this.ctxt_ShobungyoshaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_ShobungyoshaName.RegistCheckMethod")));
            this.ctxt_ShobungyoshaName.Size = new System.Drawing.Size(247, 20);
            this.ctxt_ShobungyoshaName.TabIndex = 203;
            this.ctxt_ShobungyoshaName.TabStop = false;
            this.ctxt_ShobungyoshaName.Tag = "";
            // 
            // ctxt_UnpangyoshaName
            // 
            this.ctxt_UnpangyoshaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_UnpangyoshaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_UnpangyoshaName.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.ctxt_UnpangyoshaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_UnpangyoshaName.DisplayPopUp = null;
            this.ctxt_UnpangyoshaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_UnpangyoshaName.FocusOutCheckMethod")));
            this.ctxt_UnpangyoshaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.ctxt_UnpangyoshaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_UnpangyoshaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_UnpangyoshaName.IsInputErrorOccured = false;
            this.ctxt_UnpangyoshaName.Location = new System.Drawing.Point(196, 68);
            this.ctxt_UnpangyoshaName.MaxLength = 40;
            this.ctxt_UnpangyoshaName.Name = "ctxt_UnpangyoshaName";
            this.ctxt_UnpangyoshaName.PopupAfterExecute = null;
            this.ctxt_UnpangyoshaName.PopupBeforeExecute = null;
            this.ctxt_UnpangyoshaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_UnpangyoshaName.PopupSearchSendParams")));
            this.ctxt_UnpangyoshaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_UnpangyoshaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_UnpangyoshaName.popupWindowSetting")));
            this.ctxt_UnpangyoshaName.ReadOnly = true;
            this.ctxt_UnpangyoshaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_UnpangyoshaName.RegistCheckMethod")));
            this.ctxt_UnpangyoshaName.Size = new System.Drawing.Size(247, 20);
            this.ctxt_UnpangyoshaName.TabIndex = 202;
            this.ctxt_UnpangyoshaName.TabStop = false;
            this.ctxt_UnpangyoshaName.Tag = "";
            // 
            // ctxt_HaisyutugenbaName
            // 
            this.ctxt_HaisyutugenbaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_HaisyutugenbaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_HaisyutugenbaName.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.ctxt_HaisyutugenbaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_HaisyutugenbaName.DisplayPopUp = null;
            this.ctxt_HaisyutugenbaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HaisyutugenbaName.FocusOutCheckMethod")));
            this.ctxt_HaisyutugenbaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.ctxt_HaisyutugenbaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_HaisyutugenbaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_HaisyutugenbaName.IsInputErrorOccured = false;
            this.ctxt_HaisyutugenbaName.Location = new System.Drawing.Point(196, 45);
            this.ctxt_HaisyutugenbaName.MaxLength = 40;
            this.ctxt_HaisyutugenbaName.Name = "ctxt_HaisyutugenbaName";
            this.ctxt_HaisyutugenbaName.PopupAfterExecute = null;
            this.ctxt_HaisyutugenbaName.PopupBeforeExecute = null;
            this.ctxt_HaisyutugenbaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_HaisyutugenbaName.PopupSearchSendParams")));
            this.ctxt_HaisyutugenbaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_HaisyutugenbaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_HaisyutugenbaName.popupWindowSetting")));
            this.ctxt_HaisyutugenbaName.ReadOnly = true;
            this.ctxt_HaisyutugenbaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HaisyutugenbaName.RegistCheckMethod")));
            this.ctxt_HaisyutugenbaName.Size = new System.Drawing.Size(247, 20);
            this.ctxt_HaisyutugenbaName.TabIndex = 201;
            this.ctxt_HaisyutugenbaName.TabStop = false;
            this.ctxt_HaisyutugenbaName.Tag = "";
            // 
            // ctxt_HaisyutugyoshaName
            // 
            this.ctxt_HaisyutugyoshaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_HaisyutugyoshaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_HaisyutugyoshaName.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.ctxt_HaisyutugyoshaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_HaisyutugyoshaName.DisplayPopUp = null;
            this.ctxt_HaisyutugyoshaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HaisyutugyoshaName.FocusOutCheckMethod")));
            this.ctxt_HaisyutugyoshaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.ctxt_HaisyutugyoshaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_HaisyutugyoshaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_HaisyutugyoshaName.IsInputErrorOccured = false;
            this.ctxt_HaisyutugyoshaName.Location = new System.Drawing.Point(196, 22);
            this.ctxt_HaisyutugyoshaName.MaxLength = 40;
            this.ctxt_HaisyutugyoshaName.Name = "ctxt_HaisyutugyoshaName";
            this.ctxt_HaisyutugyoshaName.PopupAfterExecute = null;
            this.ctxt_HaisyutugyoshaName.PopupBeforeExecute = null;
            this.ctxt_HaisyutugyoshaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_HaisyutugyoshaName.PopupSearchSendParams")));
            this.ctxt_HaisyutugyoshaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_HaisyutugyoshaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_HaisyutugyoshaName.popupWindowSetting")));
            this.ctxt_HaisyutugyoshaName.ReadOnly = true;
            this.ctxt_HaisyutugyoshaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_HaisyutugyoshaName.RegistCheckMethod")));
            this.ctxt_HaisyutugyoshaName.Size = new System.Drawing.Size(247, 20);
            this.ctxt_HaisyutugyoshaName.TabIndex = 200;
            this.ctxt_HaisyutugyoshaName.TabStop = false;
            this.ctxt_HaisyutugyoshaName.Tag = "";
            // 
            // cantxt_HaisyutugyoshaCD
            // 
            this.cantxt_HaisyutugyoshaCD.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_HaisyutugyoshaCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_HaisyutugyoshaCD.ChangeUpperCase = true;
            this.cantxt_HaisyutugyoshaCD.CharacterLimitList = null;
            this.cantxt_HaisyutugyoshaCD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_HaisyutugyoshaCD.DBFieldsName = "GYOUSHA_CD";
            this.cantxt_HaisyutugyoshaCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_HaisyutugyoshaCD.DisplayItemName = "排出事業者";
            this.cantxt_HaisyutugyoshaCD.DisplayPopUp = null;
            this.cantxt_HaisyutugyoshaCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HaisyutugyoshaCD.FocusOutCheckMethod")));
            this.cantxt_HaisyutugyoshaCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cantxt_HaisyutugyoshaCD.ForeColor = System.Drawing.Color.Black;
            this.cantxt_HaisyutugyoshaCD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cantxt_HaisyutugyoshaCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_HaisyutugyoshaCD.IsInputErrorOccured = false;
            this.cantxt_HaisyutugyoshaCD.ItemDefinedTypes = "varchar";
            this.cantxt_HaisyutugyoshaCD.Location = new System.Drawing.Point(137, 22);
            this.cantxt_HaisyutugyoshaCD.MaxLength = 6;
            this.cantxt_HaisyutugyoshaCD.Name = "cantxt_HaisyutugyoshaCD";
            this.cantxt_HaisyutugyoshaCD.PopupAfterExecute = null;
            this.cantxt_HaisyutugyoshaCD.PopupAfterExecuteMethod = "cantxt_HaisyutuGyousyaCd_PopupAfterExecuteMethod";
            this.cantxt_HaisyutugyoshaCD.PopupBeforeExecute = null;
            this.cantxt_HaisyutugyoshaCD.PopupBeforeExecuteMethod = "cantxt_HaisyutuGyousyaCd_PopupBeforeExecuteMethod";
            this.cantxt_HaisyutugyoshaCD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cantxt_HaisyutugyoshaCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_HaisyutugyoshaCD.PopupSearchSendParams")));
            this.cantxt_HaisyutugyoshaCD.PopupSendParams = new string[0];
            this.cantxt_HaisyutugyoshaCD.PopupSetFormField = "cantxt_HaisyutuGyousyaCd,cantxt_HaisyutuGyousyaName";
            this.cantxt_HaisyutugyoshaCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cantxt_HaisyutugyoshaCD.PopupWindowName = "検索共通ポップアップ";
            this.cantxt_HaisyutugyoshaCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_HaisyutugyoshaCD.popupWindowSetting")));
            this.cantxt_HaisyutugyoshaCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HaisyutugyoshaCD.RegistCheckMethod")));
            this.cantxt_HaisyutugyoshaCD.SetFormField = "cantxt_HaisyutuGyousyaCd,cantxt_HaisyutuGyousyaName";
            this.cantxt_HaisyutugyoshaCD.Size = new System.Drawing.Size(60, 20);
            this.cantxt_HaisyutugyoshaCD.TabIndex = 0;
            this.cantxt_HaisyutugyoshaCD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_HaisyutugyoshaCD.ZeroPaddengFlag = true;
            this.cantxt_HaisyutugyoshaCD.TextChanged += new System.EventHandler(this.cantxt_HaisyutugyoshaCD_TextChanged);
            // 
            // cantxt_UnpangyoshaCD
            // 
            this.cantxt_UnpangyoshaCD.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_UnpangyoshaCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_UnpangyoshaCD.ChangeUpperCase = true;
            this.cantxt_UnpangyoshaCD.CharacterLimitList = null;
            this.cantxt_UnpangyoshaCD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_UnpangyoshaCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_UnpangyoshaCD.DisplayItemName = "運搬受託者";
            this.cantxt_UnpangyoshaCD.DisplayPopUp = null;
            this.cantxt_UnpangyoshaCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_UnpangyoshaCD.FocusOutCheckMethod")));
            this.cantxt_UnpangyoshaCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cantxt_UnpangyoshaCD.ForeColor = System.Drawing.Color.Black;
            this.cantxt_UnpangyoshaCD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cantxt_UnpangyoshaCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_UnpangyoshaCD.IsInputErrorOccured = false;
            this.cantxt_UnpangyoshaCD.Location = new System.Drawing.Point(137, 68);
            this.cantxt_UnpangyoshaCD.MaxLength = 6;
            this.cantxt_UnpangyoshaCD.Name = "cantxt_UnpangyoshaCD";
            this.cantxt_UnpangyoshaCD.PopupAfterExecute = null;
            this.cantxt_UnpangyoshaCD.PopupAfterExecuteMethod = "cantxt_UnpanJyutaku1NameCd_PopupAfterExecuteMethod";
            this.cantxt_UnpangyoshaCD.PopupBeforeExecute = null;
            this.cantxt_UnpangyoshaCD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cantxt_UnpangyoshaCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_UnpangyoshaCD.PopupSearchSendParams")));
            this.cantxt_UnpangyoshaCD.PopupSetFormField = "cantxt_UnpanJyutaku1NameCd,cantxt_UnpanJyutaku1Name,cnt_UnpanJyutaku1Zip,cnt_Unpa" +
                "nJyutaku1Tel,ctxt_UnpanJyutakuAdd";
            this.cantxt_UnpangyoshaCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cantxt_UnpangyoshaCD.PopupWindowName = "検索共通ポップアップ";
            this.cantxt_UnpangyoshaCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_UnpangyoshaCD.popupWindowSetting")));
            this.cantxt_UnpangyoshaCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_UnpangyoshaCD.RegistCheckMethod")));
            this.cantxt_UnpangyoshaCD.SetFormField = "cantxt_UnpanJyutaku1NameCd,cantxt_UnpanJyutaku1Name,cnt_UnpanJyutaku1Zip,cnt_Unpa" +
                "nJyutaku1Tel,ctxt_UnpanJyutakuAdd";
            this.cantxt_UnpangyoshaCD.Size = new System.Drawing.Size(60, 20);
            this.cantxt_UnpangyoshaCD.TabIndex = 102;
            this.cantxt_UnpangyoshaCD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_UnpangyoshaCD.ZeroPaddengFlag = true;
            // 
            // cantxt_ShobungyoshaCD
            // 
            this.cantxt_ShobungyoshaCD.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_ShobungyoshaCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_ShobungyoshaCD.ChangeUpperCase = true;
            this.cantxt_ShobungyoshaCD.CharacterLimitList = null;
            this.cantxt_ShobungyoshaCD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_ShobungyoshaCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_ShobungyoshaCD.DisplayItemName = "処分受託者";
            this.cantxt_ShobungyoshaCD.DisplayPopUp = null;
            this.cantxt_ShobungyoshaCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_ShobungyoshaCD.FocusOutCheckMethod")));
            this.cantxt_ShobungyoshaCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cantxt_ShobungyoshaCD.ForeColor = System.Drawing.Color.Black;
            this.cantxt_ShobungyoshaCD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cantxt_ShobungyoshaCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_ShobungyoshaCD.IsInputErrorOccured = false;
            this.cantxt_ShobungyoshaCD.Location = new System.Drawing.Point(137, 91);
            this.cantxt_ShobungyoshaCD.MaxLength = 6;
            this.cantxt_ShobungyoshaCD.Name = "cantxt_ShobungyoshaCD";
            this.cantxt_ShobungyoshaCD.PopupAfterExecute = null;
            this.cantxt_ShobungyoshaCD.PopupAfterExecuteMethod = "cantxt_SyobunJyutakuNameCd_PopupAfterExecuteMethod";
            this.cantxt_ShobungyoshaCD.PopupBeforeExecute = null;
            this.cantxt_ShobungyoshaCD.PopupBeforeExecuteMethod = "cantxt_SyobunJyutakuNameCd_PopupBeforeExecuteMethod";
            this.cantxt_ShobungyoshaCD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU,GYOUSHA_POST,GYOUSHA_TEL,GYOUSHA_ADDRESS1";
            this.cantxt_ShobungyoshaCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_ShobungyoshaCD.PopupSearchSendParams")));
            this.cantxt_ShobungyoshaCD.PopupSetFormField = "cantxt_SyobunJyutakuNameCd,cantxt_SyobunJyutakuName,cnt_SyobunJyutakuZip,cnt_Syob" +
                "unJyutakuTel,ctxt_SyobunJyutakuAdd";
            this.cantxt_ShobungyoshaCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.cantxt_ShobungyoshaCD.PopupWindowName = "検索共通ポップアップ";
            this.cantxt_ShobungyoshaCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_ShobungyoshaCD.popupWindowSetting")));
            this.cantxt_ShobungyoshaCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_ShobungyoshaCD.RegistCheckMethod")));
            this.cantxt_ShobungyoshaCD.SetFormField = "cantxt_SyobunJyutakuNameCd,cantxt_SyobunJyutakuName,cnt_SyobunJyutakuZip,cnt_Syob" +
                "unJyutakuTel,ctxt_SyobunJyutakuAdd";
            this.cantxt_ShobungyoshaCD.Size = new System.Drawing.Size(60, 20);
            this.cantxt_ShobungyoshaCD.TabIndex = 103;
            this.cantxt_ShobungyoshaCD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_ShobungyoshaCD.ZeroPaddengFlag = true;
            this.cantxt_ShobungyoshaCD.TextChanged += new System.EventHandler(this.cantxt_ShobungyoshaCD_TextChanged);
            // 
            // ctxt_ShobunGenbaName
            // 
            this.ctxt_ShobunGenbaName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ctxt_ShobunGenbaName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ctxt_ShobunGenbaName.CharactersNumber = new decimal(new int[] {
            80,
            0,
            0,
            0});
            this.ctxt_ShobunGenbaName.DefaultBackColor = System.Drawing.Color.Empty;
            this.ctxt_ShobunGenbaName.DisplayPopUp = null;
            this.ctxt_ShobunGenbaName.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_ShobunGenbaName.FocusOutCheckMethod")));
            this.ctxt_ShobunGenbaName.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.ctxt_ShobunGenbaName.ForeColor = System.Drawing.Color.Black;
            this.ctxt_ShobunGenbaName.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.ctxt_ShobunGenbaName.IsInputErrorOccured = false;
            this.ctxt_ShobunGenbaName.Location = new System.Drawing.Point(196, 114);
            this.ctxt_ShobunGenbaName.MaxLength = 40;
            this.ctxt_ShobunGenbaName.Name = "ctxt_ShobunGenbaName";
            this.ctxt_ShobunGenbaName.PopupAfterExecute = null;
            this.ctxt_ShobunGenbaName.PopupBeforeExecute = null;
            this.ctxt_ShobunGenbaName.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ctxt_ShobunGenbaName.PopupSearchSendParams")));
            this.ctxt_ShobunGenbaName.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ctxt_ShobunGenbaName.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ctxt_ShobunGenbaName.popupWindowSetting")));
            this.ctxt_ShobunGenbaName.ReadOnly = true;
            this.ctxt_ShobunGenbaName.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ctxt_ShobunGenbaName.RegistCheckMethod")));
            this.ctxt_ShobunGenbaName.Size = new System.Drawing.Size(247, 20);
            this.ctxt_ShobunGenbaName.TabIndex = 204;
            this.ctxt_ShobunGenbaName.TabStop = false;
            this.ctxt_ShobunGenbaName.Tag = "";
            // 
            // cantxt_ShobunGenbaCD
            // 
            this.cantxt_ShobunGenbaCD.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_ShobunGenbaCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_ShobunGenbaCD.ChangeUpperCase = true;
            this.cantxt_ShobunGenbaCD.CharacterLimitList = null;
            this.cantxt_ShobunGenbaCD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_ShobunGenbaCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_ShobunGenbaCD.DisplayItemName = "処分事業所";
            this.cantxt_ShobunGenbaCD.DisplayPopUp = null;
            this.cantxt_ShobunGenbaCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_ShobunGenbaCD.FocusOutCheckMethod")));
            this.cantxt_ShobunGenbaCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cantxt_ShobunGenbaCD.ForeColor = System.Drawing.Color.Black;
            this.cantxt_ShobunGenbaCD.GenbaTypeColumnName = "SHOBUN_NIOROSHI_GENBA_KBN";
            this.cantxt_ShobunGenbaCD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.cantxt_ShobunGenbaCD.GyoushaCd_ControlName = "cantxt_ShobungyoshaCD";
            this.cantxt_ShobunGenbaCD.GyoushaTypeColumnName = "SHOBUN_NIOROSHI_GYOUSHA_KBN";
            this.cantxt_ShobunGenbaCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_ShobunGenbaCD.IsInputErrorOccured = false;
            this.cantxt_ShobunGenbaCD.Location = new System.Drawing.Point(137, 114);
            this.cantxt_ShobunGenbaCD.MaxLength = 6;
            this.cantxt_ShobunGenbaCD.Name = "cantxt_ShobunGenbaCD";
            this.cantxt_ShobunGenbaCD.PopupAfterExecute = null;
            this.cantxt_ShobunGenbaCD.PopupAfterExecuteMethod = "cantxt_UnpanJyugyobaNameCd_PopupAfterExecuteMethod";
            this.cantxt_ShobunGenbaCD.PopupBeforeExecute = null;
            this.cantxt_ShobunGenbaCD.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU,GENBA_POST,GENBA_TEL,GENBA_ADDRESS1,GYOUSHA_CD";
            this.cantxt_ShobunGenbaCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_ShobunGenbaCD.PopupSearchSendParams")));
            this.cantxt_ShobunGenbaCD.PopupSetFormField = "cantxt_UnpanJyugyobaNameCd,cantxt_UnpanJyugyobaName,cnt_UnpanJyugyobaZip,cnt_Unpa" +
                "nJyugyobaTel,ctxt_UnpanJyugyobaAdd,cantxt_UnpanJyugyobaGyoCD";
            this.cantxt_ShobunGenbaCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cantxt_ShobunGenbaCD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cantxt_ShobunGenbaCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_ShobunGenbaCD.popupWindowSetting")));
            this.cantxt_ShobunGenbaCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_ShobunGenbaCD.RegistCheckMethod")));
            this.cantxt_ShobunGenbaCD.SetFormField = "cantxt_UnpanJyugyobaNameCd,cantxt_UnpanJyugyobaName,cnt_UnpanJyugyobaZip,cnt_Unpa" +
                "nJyugyobaTel,ctxt_UnpanJyugyobaAdd,cantxt_UnpanJyugyobaGyoCD,cantxt_SyobunJyutak" +
                "uNameCd,cantxt_SyobunJyutakuName";
            this.cantxt_ShobunGenbaCD.Size = new System.Drawing.Size(60, 20);
            this.cantxt_ShobunGenbaCD.TabIndex = 104;
            this.cantxt_ShobunGenbaCD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_ShobunGenbaCD.ZeroPaddengFlag = true;
            // 
            // cantxt_HaisyutugenbaCD
            // 
            this.cantxt_HaisyutugenbaCD.BackColor = System.Drawing.SystemColors.Window;
            this.cantxt_HaisyutugenbaCD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.cantxt_HaisyutugenbaCD.ChangeUpperCase = true;
            this.cantxt_HaisyutugenbaCD.CharacterLimitList = null;
            this.cantxt_HaisyutugenbaCD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.cantxt_HaisyutugenbaCD.DefaultBackColor = System.Drawing.Color.Empty;
            this.cantxt_HaisyutugenbaCD.DisplayItemName = "排出事業場";
            this.cantxt_HaisyutugenbaCD.DisplayPopUp = null;
            this.cantxt_HaisyutugenbaCD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HaisyutugenbaCD.FocusOutCheckMethod")));
            this.cantxt_HaisyutugenbaCD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.25F);
            this.cantxt_HaisyutugenbaCD.ForeColor = System.Drawing.Color.Black;
            this.cantxt_HaisyutugenbaCD.GenbaTypeColumnName = "HAISHUTSU_NIZUMI_GENBA_KBN";
            this.cantxt_HaisyutugenbaCD.GetCodeMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.cantxt_HaisyutugenbaCD.GyoushaCd_ControlName = "cantxt_HaisyutugyoshaCD";
            this.cantxt_HaisyutugenbaCD.GyoushaTypeColumnName = "HAISHUTSU_NIZUMI_GYOUSHA_KBN";
            this.cantxt_HaisyutugenbaCD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.cantxt_HaisyutugenbaCD.IsInputErrorOccured = false;
            this.cantxt_HaisyutugenbaCD.Location = new System.Drawing.Point(137, 45);
            this.cantxt_HaisyutugenbaCD.MaxLength = 6;
            this.cantxt_HaisyutugenbaCD.Name = "cantxt_HaisyutugenbaCD";
            this.cantxt_HaisyutugenbaCD.PopupAfterExecute = null;
            this.cantxt_HaisyutugenbaCD.PopupAfterExecuteMethod = "cantxt_HaisyutuJigyoubaName_PopupAfterExecuteMethod";
            this.cantxt_HaisyutugenbaCD.PopupBeforeExecute = null;
            this.cantxt_HaisyutugenbaCD.PopupGetMasterField = "GENBA_CD,GENBA_NAME_RYAKU";
            this.cantxt_HaisyutugenbaCD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("cantxt_HaisyutugenbaCD.PopupSearchSendParams")));
            this.cantxt_HaisyutugenbaCD.PopupSendParams = new string[0];
            this.cantxt_HaisyutugenbaCD.PopupSetFormField = "cantxt_HaisyutuJigyoubaName,ctxt_HaisyutuJigyoubaName,cnt_HaisyutuJigyoubaZip,cnt" +
                "_HaisyutuJigyoubaTel,ctxt_HaisyutuJigyoubaAdd,cantxt_HaisyutuGyousyaCd,ctxt_Hais" +
                "yutuGyousyaName";
            this.cantxt_HaisyutugenbaCD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.cantxt_HaisyutugenbaCD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.cantxt_HaisyutugenbaCD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("cantxt_HaisyutugenbaCD.popupWindowSetting")));
            this.cantxt_HaisyutugenbaCD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("cantxt_HaisyutugenbaCD.RegistCheckMethod")));
            this.cantxt_HaisyutugenbaCD.SetFormField = "cantxt_HaisyutuJigyoubaName,ctxt_HaisyutuJigyoubaName,cnt_HaisyutuJigyoubaZip,cnt" +
                "_HaisyutuJigyoubaTel,ctxt_HaisyutuJigyoubaAdd,cantxt_HaisyutuGyousyaCd,ctxt_Hais" +
                "yutuGyousyaName";
            this.cantxt_HaisyutugenbaCD.Size = new System.Drawing.Size(60, 20);
            this.cantxt_HaisyutugenbaCD.TabIndex = 101;
            this.cantxt_HaisyutugenbaCD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.cantxt_HaisyutugenbaCD.ZeroPaddengFlag = true;
            // 
            // UIForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 490);
            this.Controls.Add(this.ctxt_ShobunGenbaName);
            this.Controls.Add(this.cantxt_ShobunGenbaCD);
            this.Controls.Add(this.cbtn_UnpanJyugyobaSan);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.casbtn_SyobunJyutaku);
            this.Controls.Add(this.cantxt_ShobungyoshaCD);
            this.Controls.Add(this.ctxt_ShobungyoshaName);
            this.Controls.Add(this.cbtn_UnpanJyutaku1San);
            this.Controls.Add(this.cantxt_UnpangyoshaCD);
            this.Controls.Add(this.ctxt_UnpangyoshaName);
            this.Controls.Add(this.cbtn_HaisyutuJigyoubaSan);
            this.Controls.Add(this.cantxt_HaisyutugenbaCD);
            this.Controls.Add(this.ctxt_HaisyutugenbaName);
            this.Controls.Add(this.cbtn_HaisyutuGyousyaSan);
            this.Controls.Add(this.ctxt_HaisyutugyoshaName);
            this.Controls.Add(this.cantxt_HaisyutugyoshaCD);
            this.Controls.Add(this.HST_SHOUBU_JIGYOU_NAME);
            this.Controls.Add(this.HST_SHOUBU_JUTAKU_NAME);
            this.Controls.Add(this.HST_UNBAN_JUTAKU_NAME);
            this.Controls.Add(this.HST_GENBA_NAME);
            this.Controls.Add(this.HST_GYOUSHA_NAME);
            this.Controls.Add(this.PATTERN_NAME);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.Name = "UIForm";
            this.Text = "UIForm";
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label6, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.Controls.SetChildIndex(this.label5, 0);
            this.Controls.SetChildIndex(this.label8, 0);
            this.Controls.SetChildIndex(this.label12, 0);
            this.Controls.SetChildIndex(this.label10, 0);
            this.Controls.SetChildIndex(this.label9, 0);
            this.Controls.SetChildIndex(this.label11, 0);
            this.Controls.SetChildIndex(this.label7, 0);
            this.Controls.SetChildIndex(this.PATTERN_NAME, 0);
            this.Controls.SetChildIndex(this.HST_GYOUSHA_NAME, 0);
            this.Controls.SetChildIndex(this.HST_GENBA_NAME, 0);
            this.Controls.SetChildIndex(this.HST_UNBAN_JUTAKU_NAME, 0);
            this.Controls.SetChildIndex(this.HST_SHOUBU_JUTAKU_NAME, 0);
            this.Controls.SetChildIndex(this.HST_SHOUBU_JIGYOU_NAME, 0);
            this.Controls.SetChildIndex(this.cantxt_HaisyutugyoshaCD, 0);
            this.Controls.SetChildIndex(this.ctxt_HaisyutugyoshaName, 0);
            this.Controls.SetChildIndex(this.cbtn_HaisyutuGyousyaSan, 0);
            this.Controls.SetChildIndex(this.ctxt_HaisyutugenbaName, 0);
            this.Controls.SetChildIndex(this.cantxt_HaisyutugenbaCD, 0);
            this.Controls.SetChildIndex(this.cbtn_HaisyutuJigyoubaSan, 0);
            this.Controls.SetChildIndex(this.ctxt_UnpangyoshaName, 0);
            this.Controls.SetChildIndex(this.cantxt_UnpangyoshaCD, 0);
            this.Controls.SetChildIndex(this.cbtn_UnpanJyutaku1San, 0);
            this.Controls.SetChildIndex(this.ctxt_ShobungyoshaName, 0);
            this.Controls.SetChildIndex(this.cantxt_ShobungyoshaCD, 0);
            this.Controls.SetChildIndex(this.casbtn_SyobunJyutaku, 0);
            this.Controls.SetChildIndex(this.label13, 0);
            this.Controls.SetChildIndex(this.cbtn_UnpanJyugyobaSan, 0);
            this.Controls.SetChildIndex(this.cantxt_ShobunGenbaCD, 0);
            this.Controls.SetChildIndex(this.ctxt_ShobunGenbaName, 0);
            this.Controls.SetChildIndex(this.bt_ptn1, 0);
            this.Controls.SetChildIndex(this.bt_ptn2, 0);
            this.Controls.SetChildIndex(this.bt_ptn3, 0);
            this.Controls.SetChildIndex(this.bt_ptn4, 0);
            this.Controls.SetChildIndex(this.bt_ptn5, 0);
            this.Controls.SetChildIndex(this.searchString, 0);
            this.Controls.SetChildIndex(this.customSortHeader1, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        internal r_framework.CustomControl.CustomTextBox PATTERN_NAME;
        internal r_framework.CustomControl.CustomTextBox HST_GYOUSHA_NAME;
        internal r_framework.CustomControl.CustomTextBox HST_GENBA_NAME;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_HaisyutuGyousyaSan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_HaisyutuJigyoubaSan;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_UnpanJyutaku1San;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        internal r_framework.CustomControl.CustomPopupOpenButton casbtn_SyobunJyutaku;
        internal r_framework.CustomControl.CustomTextBox HST_UNBAN_JUTAKU_NAME;
        internal r_framework.CustomControl.CustomTextBox HST_SHOUBU_JUTAKU_NAME;
        internal r_framework.CustomControl.CustomTextBox HST_SHOUBU_JIGYOU_NAME;
        private System.Windows.Forms.Label label13;
        internal r_framework.CustomControl.CustomPopupOpenButton cbtn_UnpanJyugyobaSan;
        internal r_framework.CustomControl.CustomTextBox ctxt_ShobungyoshaName;
        internal r_framework.CustomControl.CustomTextBox ctxt_UnpangyoshaName;
        internal r_framework.CustomControl.CustomTextBox ctxt_HaisyutugenbaName;
        internal r_framework.CustomControl.CustomTextBox ctxt_HaisyutugyoshaName;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_HaisyutugyoshaCD;
       
        public CustomerControls_Ex.CustomAlphaNumTextBox_Ex cantxt_HaisyutugenbaCD;
        public CustomerControls_Ex.CustomAlphaNumTextBox_Ex cantxt_ShobunGenbaCD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_UnpangyoshaCD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox cantxt_ShobungyoshaCD;
       
        internal r_framework.CustomControl.CustomTextBox ctxt_ShobunGenbaName;
    }
}