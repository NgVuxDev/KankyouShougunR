namespace Shougun.Core.Scale.KeiryouIchiran.APP
{
    partial class KensakuControl
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KensakuControl));
            this.TORIHIKISAKI_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.TORIHIKISAKI_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.TORIHIKISAKI_LABEL = new System.Windows.Forms.Label();
            this.GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GYOUSHA_LABEL = new System.Windows.Forms.Label();
            this.GENBA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.GENBA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.GENBA_LABEL = new System.Windows.Forms.Label();
            this.UNPAN_GYOUSHA_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.UNPAN_GYOUSHA_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.UNPAN_GYOUSHA_LABEL = new System.Windows.Forms.Label();
            this.ISNOT_NEED_DELETE_FLG = new r_framework.CustomControl.CustomTextBox();
            this.SHARYOU_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.SHASHU_LABEL = new System.Windows.Forms.Label();
            this.SHARYOU_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SHASHU_NAME_RYAKU = new r_framework.CustomControl.CustomTextBox();
            this.SHARYOU_LABEL = new System.Windows.Forms.Label();
            this.SHASHU_CD = new r_framework.CustomControl.CustomAlphaNumTextBox();
            this.SuspendLayout();
            // 
            // TORIHIKISAKI_NAME_RYAKU
            // 
            this.TORIHIKISAKI_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.TORIHIKISAKI_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_NAME_RYAKU.ChangeUpperCase = true;
            this.TORIHIKISAKI_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_NAME_RYAKU.DisplayPopUp = null;
            this.TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.FocusOutCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_NAME_RYAKU.IsInputErrorOccured = false;
            this.TORIHIKISAKI_NAME_RYAKU.Location = new System.Drawing.Point(169, 2);
            this.TORIHIKISAKI_NAME_RYAKU.Name = "TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_NAME_RYAKU.PopupAfterExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupBeforeExecute = null;
            this.TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.PopupSearchSendParams")));
            this.TORIHIKISAKI_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.TORIHIKISAKI_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.popupWindowSetting")));
            this.TORIHIKISAKI_NAME_RYAKU.ReadOnly = true;
            this.TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_NAME_RYAKU.RegistCheckMethod")));
            this.TORIHIKISAKI_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.TORIHIKISAKI_NAME_RYAKU.TabIndex = 30;
            this.TORIHIKISAKI_NAME_RYAKU.TabStop = false;
            this.TORIHIKISAKI_NAME_RYAKU.Tag = "";
            // 
            // TORIHIKISAKI_CD
            // 
            this.TORIHIKISAKI_CD.BackColor = System.Drawing.SystemColors.Window;
            this.TORIHIKISAKI_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_CD.ChangeUpperCase = true;
            this.TORIHIKISAKI_CD.CharacterLimitList = null;
            this.TORIHIKISAKI_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.TORIHIKISAKI_CD.DBFieldsName = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.TORIHIKISAKI_CD.DisplayItemName = "取引先";
            this.TORIHIKISAKI_CD.DisplayPopUp = null;
            this.TORIHIKISAKI_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.FocusOutCheckMethod")));
            this.TORIHIKISAKI_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_CD.ForeColor = System.Drawing.Color.Black;
            this.TORIHIKISAKI_CD.GetCodeMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.TORIHIKISAKI_CD.IsInputErrorOccured = false;
            this.TORIHIKISAKI_CD.ItemDefinedTypes = "varchar";
            this.TORIHIKISAKI_CD.Location = new System.Drawing.Point(115, 2);
            this.TORIHIKISAKI_CD.MaxLength = 6;
            this.TORIHIKISAKI_CD.Name = "TORIHIKISAKI_CD";
            this.TORIHIKISAKI_CD.PopupAfterExecute = null;
            this.TORIHIKISAKI_CD.PopupBeforeExecute = null;
            this.TORIHIKISAKI_CD.PopupGetMasterField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("TORIHIKISAKI_CD.PopupSearchSendParams")));
            this.TORIHIKISAKI_CD.PopupSetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_TORIHIKISAKI;
            this.TORIHIKISAKI_CD.PopupWindowName = "検索共通ポップアップ";
            this.TORIHIKISAKI_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("TORIHIKISAKI_CD.popupWindowSetting")));
            this.TORIHIKISAKI_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("TORIHIKISAKI_CD.RegistCheckMethod")));
            this.TORIHIKISAKI_CD.SetFormField = "TORIHIKISAKI_CD,TORIHIKISAKI_NAME_RYAKU";
            this.TORIHIKISAKI_CD.ShortItemName = "";
            this.TORIHIKISAKI_CD.Size = new System.Drawing.Size(55, 20);
            this.TORIHIKISAKI_CD.TabIndex = 20;
            this.TORIHIKISAKI_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.TORIHIKISAKI_CD.ZeroPaddengFlag = true;
            // 
            // TORIHIKISAKI_LABEL
            // 
            this.TORIHIKISAKI_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.TORIHIKISAKI_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TORIHIKISAKI_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TORIHIKISAKI_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.TORIHIKISAKI_LABEL.ForeColor = System.Drawing.Color.White;
            this.TORIHIKISAKI_LABEL.Location = new System.Drawing.Point(0, 2);
            this.TORIHIKISAKI_LABEL.Name = "TORIHIKISAKI_LABEL";
            this.TORIHIKISAKI_LABEL.Size = new System.Drawing.Size(110, 20);
            this.TORIHIKISAKI_LABEL.TabIndex = 10;
            this.TORIHIKISAKI_LABEL.Text = "取引先";
            this.TORIHIKISAKI_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GYOUSHA_NAME_RYAKU
            // 
            this.GYOUSHA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GYOUSHA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_NAME_RYAKU.ChangeUpperCase = true;
            this.GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(169, 24);
            this.GYOUSHA_NAME_RYAKU.Name = "GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.GYOUSHA_NAME_RYAKU.TabIndex = 60;
            this.GYOUSHA_NAME_RYAKU.TabStop = false;
            this.GYOUSHA_NAME_RYAKU.Tag = "";
            // 
            // GYOUSHA_CD
            // 
            this.GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_CD.ChangeUpperCase = true;
            this.GYOUSHA_CD.CharacterLimitList = null;
            this.GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GYOUSHA_CD.DBFieldsName = "GYOUSHA_CD";
            this.GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GYOUSHA_CD.DisplayItemName = "業者";
            this.GYOUSHA_CD.DisplayPopUp = null;
            this.GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.FocusOutCheckMethod")));
            this.GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GYOUSHA_CD.IsInputErrorOccured = false;
            this.GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.GYOUSHA_CD.Location = new System.Drawing.Point(115, 24);
            this.GYOUSHA_CD.MaxLength = 6;
            this.GYOUSHA_CD.Name = "GYOUSHA_CD";
            this.GYOUSHA_CD.PopupAfterExecute = null;
            this.GYOUSHA_CD.PopupAfterExecuteMethod = "GYOUSHA_CD_Pupafter";
            this.GYOUSHA_CD.PopupBeforeExecute = null;
            this.GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupMultiSelect = true;
            this.GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GYOUSHA_CD.PopupSearchSendParams")));
            this.GYOUSHA_CD.PopupSendParams = new string[0];
            this.GYOUSHA_CD.PopupSetFormField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GYOUSHA_CD.popupWindowSetting")));
            this.GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GYOUSHA_CD.RegistCheckMethod")));
            this.GYOUSHA_CD.SetFormField = "GYOUSYA_CD,GYOUSYA_NAME_RYAKU";
            this.GYOUSHA_CD.ShortItemName = "業者CD";
            this.GYOUSHA_CD.Size = new System.Drawing.Size(55, 20);
            this.GYOUSHA_CD.TabIndex = 50;
            this.GYOUSHA_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.GYOUSHA_CD.ZeroPaddengFlag = true;
            this.GYOUSHA_CD.Enter += new System.EventHandler(this.GYOUSHA_CD_Enter);
            this.GYOUSHA_CD.Validated += new System.EventHandler(this.GYOUSHA_CD_Validated);
            // 
            // GYOUSHA_LABEL
            // 
            this.GYOUSHA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GYOUSHA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GYOUSHA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GYOUSHA_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GYOUSHA_LABEL.ForeColor = System.Drawing.Color.White;
            this.GYOUSHA_LABEL.Location = new System.Drawing.Point(0, 24);
            this.GYOUSHA_LABEL.Name = "GYOUSHA_LABEL";
            this.GYOUSHA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.GYOUSHA_LABEL.TabIndex = 40;
            this.GYOUSHA_LABEL.Text = "業者";
            this.GYOUSHA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GENBA_NAME_RYAKU
            // 
            this.GENBA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.GENBA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_NAME_RYAKU.ChangeUpperCase = true;
            this.GENBA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_NAME_RYAKU.DisplayPopUp = null;
            this.GENBA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.FocusOutCheckMethod")));
            this.GENBA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.GENBA_NAME_RYAKU.IsInputErrorOccured = false;
            this.GENBA_NAME_RYAKU.Location = new System.Drawing.Point(169, 46);
            this.GENBA_NAME_RYAKU.Name = "GENBA_NAME_RYAKU";
            this.GENBA_NAME_RYAKU.PopupAfterExecute = null;
            this.GENBA_NAME_RYAKU.PopupBeforeExecute = null;
            this.GENBA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_NAME_RYAKU.PopupSearchSendParams")));
            this.GENBA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.GENBA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_NAME_RYAKU.popupWindowSetting")));
            this.GENBA_NAME_RYAKU.ReadOnly = true;
            this.GENBA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_NAME_RYAKU.RegistCheckMethod")));
            this.GENBA_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.GENBA_NAME_RYAKU.TabIndex = 90;
            this.GENBA_NAME_RYAKU.TabStop = false;
            this.GENBA_NAME_RYAKU.Tag = "";
            // 
            // GENBA_CD
            // 
            this.GENBA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.GENBA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_CD.ChangeUpperCase = true;
            this.GENBA_CD.CharacterLimitList = null;
            this.GENBA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.GENBA_CD.DBFieldsName = "GENBA_CD";
            this.GENBA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.GENBA_CD.DisplayItemName = "";
            this.GENBA_CD.DisplayPopUp = null;
            this.GENBA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.FocusOutCheckMethod")));
            this.GENBA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_CD.ForeColor = System.Drawing.Color.Black;
            this.GENBA_CD.GetCodeMasterField = "GENBA_CD, GENBA_NAME_RYAKU";
            this.GENBA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.GENBA_CD.IsInputErrorOccured = false;
            this.GENBA_CD.ItemDefinedTypes = "varchar";
            this.GENBA_CD.Location = new System.Drawing.Point(115, 46);
            this.GENBA_CD.MaxLength = 6;
            this.GENBA_CD.Name = "GENBA_CD";
            this.GENBA_CD.PopupAfterExecute = null;
            this.GENBA_CD.PopupBeforeExecute = null;
            this.GENBA_CD.PopupGetMasterField = "GENBA_CD, GENBA_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("GENBA_CD.PopupSearchSendParams")));
            this.GENBA_CD.PopupSetFormField = "GENBA_CD, GENBA_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU";
            this.GENBA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GENBA;
            this.GENBA_CD.PopupWindowName = "複数キー用検索共通ポップアップ";
            this.GENBA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("GENBA_CD.popupWindowSetting")));
            this.GENBA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("GENBA_CD.RegistCheckMethod")));
            this.GENBA_CD.SetFormField = "GENBA_CD, GENBA_NAME_RYAKU";
            this.GENBA_CD.ShortItemName = "";
            this.GENBA_CD.Size = new System.Drawing.Size(55, 20);
            this.GENBA_CD.TabIndex = 80;
            this.GENBA_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.GENBA_CD.ZeroPaddengFlag = true;
            this.GENBA_CD.Validated += new System.EventHandler(this.GENBA_CD_Validated);
            // 
            // GENBA_LABEL
            // 
            this.GENBA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.GENBA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GENBA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.GENBA_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.GENBA_LABEL.ForeColor = System.Drawing.Color.White;
            this.GENBA_LABEL.Location = new System.Drawing.Point(0, 46);
            this.GENBA_LABEL.Name = "GENBA_LABEL";
            this.GENBA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.GENBA_LABEL.TabIndex = 70;
            this.GENBA_LABEL.Text = "現場";
            this.GENBA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UNPAN_GYOUSHA_NAME_RYAKU
            // 
            this.UNPAN_GYOUSHA_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.UNPAN_GYOUSHA_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_NAME_RYAKU.ChangeUpperCase = true;
            this.UNPAN_GYOUSHA_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_NAME_RYAKU.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_RYAKU.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNPAN_GYOUSHA_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_NAME_RYAKU.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_NAME_RYAKU.Location = new System.Drawing.Point(169, 68);
            this.UNPAN_GYOUSHA_NAME_RYAKU.Name = "UNPAN_GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_NAME_RYAKU.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_NAME_RYAKU.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_RYAKU.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.UNPAN_GYOUSHA_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_RYAKU.popupWindowSetting")));
            this.UNPAN_GYOUSHA_NAME_RYAKU.ReadOnly = true;
            this.UNPAN_GYOUSHA_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_NAME_RYAKU.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.UNPAN_GYOUSHA_NAME_RYAKU.TabIndex = 120;
            this.UNPAN_GYOUSHA_NAME_RYAKU.TabStop = false;
            this.UNPAN_GYOUSHA_NAME_RYAKU.Tag = "";
            // 
            // UNPAN_GYOUSHA_CD
            // 
            this.UNPAN_GYOUSHA_CD.BackColor = System.Drawing.SystemColors.Window;
            this.UNPAN_GYOUSHA_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_CD.ChangeUpperCase = true;
            this.UNPAN_GYOUSHA_CD.CharacterLimitList = null;
            this.UNPAN_GYOUSHA_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.UNPAN_GYOUSHA_CD.DBFieldsName = "";
            this.UNPAN_GYOUSHA_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.UNPAN_GYOUSHA_CD.DisplayItemName = "";
            this.UNPAN_GYOUSHA_CD.DisplayPopUp = null;
            this.UNPAN_GYOUSHA_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.FocusOutCheckMethod")));
            this.UNPAN_GYOUSHA_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNPAN_GYOUSHA_CD.ForeColor = System.Drawing.Color.Black;
            this.UNPAN_GYOUSHA_CD.GetCodeMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.UNPAN_GYOUSHA_CD.IsInputErrorOccured = false;
            this.UNPAN_GYOUSHA_CD.ItemDefinedTypes = "varchar";
            this.UNPAN_GYOUSHA_CD.Location = new System.Drawing.Point(115, 68);
            this.UNPAN_GYOUSHA_CD.MaxLength = 6;
            this.UNPAN_GYOUSHA_CD.Name = "UNPAN_GYOUSHA_CD";
            this.UNPAN_GYOUSHA_CD.PopupAfterExecute = null;
            this.UNPAN_GYOUSHA_CD.PopupBeforeExecute = null;
            this.UNPAN_GYOUSHA_CD.PopupGetMasterField = "GYOUSHA_CD,GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.PopupSearchSendParams")));
            this.UNPAN_GYOUSHA_CD.PopupSetFormField = "UNPAN_GYOUSHA_CD,UNPAN_GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_GYOUSHA;
            this.UNPAN_GYOUSHA_CD.PopupWindowName = "検索共通ポップアップ";
            this.UNPAN_GYOUSHA_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.popupWindowSetting")));
            this.UNPAN_GYOUSHA_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("UNPAN_GYOUSHA_CD.RegistCheckMethod")));
            this.UNPAN_GYOUSHA_CD.SetFormField = "UNPAN_GYOUSHA_CD,UNPAN_GYOUSHA_NAME_RYAKU";
            this.UNPAN_GYOUSHA_CD.ShortItemName = "運搬業者CD";
            this.UNPAN_GYOUSHA_CD.Size = new System.Drawing.Size(55, 20);
            this.UNPAN_GYOUSHA_CD.TabIndex = 110;
            this.UNPAN_GYOUSHA_CD.Tag = "半角6桁以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            this.UNPAN_GYOUSHA_CD.ZeroPaddengFlag = true;
            this.UNPAN_GYOUSHA_CD.Validated += new System.EventHandler(this.UNPNA_GYOUSHA_CD_Validated);
            // 
            // UNPAN_GYOUSHA_LABEL
            // 
            this.UNPAN_GYOUSHA_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.UNPAN_GYOUSHA_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.UNPAN_GYOUSHA_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UNPAN_GYOUSHA_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.UNPAN_GYOUSHA_LABEL.ForeColor = System.Drawing.Color.White;
            this.UNPAN_GYOUSHA_LABEL.Location = new System.Drawing.Point(0, 68);
            this.UNPAN_GYOUSHA_LABEL.Name = "UNPAN_GYOUSHA_LABEL";
            this.UNPAN_GYOUSHA_LABEL.Size = new System.Drawing.Size(110, 20);
            this.UNPAN_GYOUSHA_LABEL.TabIndex = 100;
            this.UNPAN_GYOUSHA_LABEL.Text = "運搬業者";
            this.UNPAN_GYOUSHA_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ISNOT_NEED_DELETE_FLG
            // 
            this.ISNOT_NEED_DELETE_FLG.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.ISNOT_NEED_DELETE_FLG.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ISNOT_NEED_DELETE_FLG.CharactersNumber = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.ISNOT_NEED_DELETE_FLG.DBFieldsName = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.DefaultBackColor = System.Drawing.Color.Empty;
            this.ISNOT_NEED_DELETE_FLG.DisplayPopUp = null;
            this.ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.FocusOutCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ISNOT_NEED_DELETE_FLG.ForeColor = System.Drawing.Color.Black;
            this.ISNOT_NEED_DELETE_FLG.IsInputErrorOccured = false;
            this.ISNOT_NEED_DELETE_FLG.ItemDefinedTypes = "bit";
            this.ISNOT_NEED_DELETE_FLG.Location = new System.Drawing.Point(470, 80);
            this.ISNOT_NEED_DELETE_FLG.MaxLength = 20;
            this.ISNOT_NEED_DELETE_FLG.Name = "ISNOT_NEED_DELETE_FLG";
            this.ISNOT_NEED_DELETE_FLG.PopupAfterExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupBeforeExecute = null;
            this.ISNOT_NEED_DELETE_FLG.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.PopupSearchSendParams")));
            this.ISNOT_NEED_DELETE_FLG.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.ISNOT_NEED_DELETE_FLG.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.popupWindowSetting")));
            this.ISNOT_NEED_DELETE_FLG.ReadOnly = true;
            this.ISNOT_NEED_DELETE_FLG.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("ISNOT_NEED_DELETE_FLG.RegistCheckMethod")));
            this.ISNOT_NEED_DELETE_FLG.Size = new System.Drawing.Size(40, 20);
            this.ISNOT_NEED_DELETE_FLG.TabIndex = 190;
            this.ISNOT_NEED_DELETE_FLG.TabStop = false;
            this.ISNOT_NEED_DELETE_FLG.Tag = "";
            this.ISNOT_NEED_DELETE_FLG.Text = "TRUE";
            this.ISNOT_NEED_DELETE_FLG.Visible = false;
            // 
            // SHARYOU_NAME_RYAKU
            // 
            this.SHARYOU_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHARYOU_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHARYOU_NAME_RYAKU.DBFieldsName = "SHARYOU_NAME_RYAKU";
            this.SHARYOU_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHARYOU_NAME_RYAKU.DisplayPopUp = null;
            this.SHARYOU_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_NAME_RYAKU.FocusOutCheckMethod")));
            this.SHARYOU_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHARYOU_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.SHARYOU_NAME_RYAKU.IsInputErrorOccured = false;
            this.SHARYOU_NAME_RYAKU.ItemDefinedTypes = "varchar";
            this.SHARYOU_NAME_RYAKU.Location = new System.Drawing.Point(169, 112);
            this.SHARYOU_NAME_RYAKU.MaxLength = 10;
            this.SHARYOU_NAME_RYAKU.Name = "SHARYOU_NAME_RYAKU";
            this.SHARYOU_NAME_RYAKU.PopupAfterExecute = null;
            this.SHARYOU_NAME_RYAKU.PopupBeforeExecute = null;
            this.SHARYOU_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHARYOU_NAME_RYAKU.PopupSearchSendParams")));
            this.SHARYOU_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHARYOU_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHARYOU_NAME_RYAKU.popupWindowSetting")));
            this.SHARYOU_NAME_RYAKU.ReadOnly = true;
            this.SHARYOU_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_NAME_RYAKU.RegistCheckMethod")));
            this.SHARYOU_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.SHARYOU_NAME_RYAKU.TabIndex = 180;
            this.SHARYOU_NAME_RYAKU.TabStop = false;
            this.SHARYOU_NAME_RYAKU.Tag = " ";
            // 
            // SHASHU_LABEL
            // 
            this.SHASHU_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.SHASHU_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHASHU_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SHASHU_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHASHU_LABEL.ForeColor = System.Drawing.Color.White;
            this.SHASHU_LABEL.Location = new System.Drawing.Point(0, 90);
            this.SHASHU_LABEL.Name = "SHASHU_LABEL";
            this.SHASHU_LABEL.Size = new System.Drawing.Size(110, 20);
            this.SHASHU_LABEL.TabIndex = 130;
            this.SHASHU_LABEL.Text = "車種";
            this.SHASHU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHARYOU_CD
            // 
            this.SHARYOU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHARYOU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHARYOU_CD.ChangeUpperCase = true;
            this.SHARYOU_CD.CharacterLimitList = null;
            this.SHARYOU_CD.CharactersNumber = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.SHARYOU_CD.DBFieldsName = "SHARYOU_CD";
            this.SHARYOU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHARYOU_CD.DisplayItemName = "車輌";
            this.SHARYOU_CD.DisplayPopUp = null;
            this.SHARYOU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_CD.FocusOutCheckMethod")));
            this.SHARYOU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHARYOU_CD.ForeColor = System.Drawing.Color.Black;
            this.SHARYOU_CD.GetCodeMasterField = "SHARYOU_CD, SHARYOU_NAME_RYAKU";
            this.SHARYOU_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHARYOU_CD.IsInputErrorOccured = false;
            this.SHARYOU_CD.ItemDefinedTypes = "varchar";
            this.SHARYOU_CD.Location = new System.Drawing.Point(115, 112);
            this.SHARYOU_CD.MaxLength = 6;
            this.SHARYOU_CD.Name = "SHARYOU_CD";
            this.SHARYOU_CD.PopupAfterExecute = null;
            this.SHARYOU_CD.PopupBeforeExecute = null;
            this.SHARYOU_CD.PopupGetMasterField = "SHARYOU_CD, SHARYOU_NAME_RYAKU, GYOUSHA_CD, GYOUSHA_NAME_RYAKU, SHASHU_CD, SHASHU" +
    "_NAME_RYAKU";
            this.SHARYOU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHARYOU_CD.PopupSearchSendParams")));
            this.SHARYOU_CD.PopupSetFormField = "SHARYOU_CD, SHARYOU_NAME_RYAKU, UNPAN_GYOUSHA_CD, UNPAN_GYOUSHA_NAME_RYAKU, SHASH" +
    "U_CD, SHASHU_NAME_RYAKU";
            this.SHARYOU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHARYOU;
            this.SHARYOU_CD.PopupWindowName = "車両選択共通ポップアップ";
            this.SHARYOU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHARYOU_CD.popupWindowSetting")));
            this.SHARYOU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHARYOU_CD.RegistCheckMethod")));
            this.SHARYOU_CD.SetFormField = "SHARYOU_CD, SHARYOU_NAME_RYAKU";
            this.SHARYOU_CD.Size = new System.Drawing.Size(55, 20);
            this.SHARYOU_CD.TabIndex = 170;
            this.SHARYOU_CD.Tag = "車輌を指定してください";
            this.SHARYOU_CD.ZeroPaddengFlag = true;
            this.SHARYOU_CD.Enter += new System.EventHandler(this.SHARYOU_CD_Enter);
            this.SHARYOU_CD.Validating += new System.ComponentModel.CancelEventHandler(this.SHARYOU_CD_Validating);
            // 
            // SHASHU_NAME_RYAKU
            // 
            this.SHASHU_NAME_RYAKU.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            this.SHASHU_NAME_RYAKU.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHASHU_NAME_RYAKU.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHASHU_NAME_RYAKU.DisplayPopUp = null;
            this.SHASHU_NAME_RYAKU.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHASHU_NAME_RYAKU.FocusOutCheckMethod")));
            this.SHASHU_NAME_RYAKU.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHASHU_NAME_RYAKU.ForeColor = System.Drawing.Color.Black;
            this.SHASHU_NAME_RYAKU.IsInputErrorOccured = false;
            this.SHASHU_NAME_RYAKU.Location = new System.Drawing.Point(169, 90);
            this.SHASHU_NAME_RYAKU.Name = "SHASHU_NAME_RYAKU";
            this.SHASHU_NAME_RYAKU.PopupAfterExecute = null;
            this.SHASHU_NAME_RYAKU.PopupBeforeExecute = null;
            this.SHASHU_NAME_RYAKU.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHASHU_NAME_RYAKU.PopupSearchSendParams")));
            this.SHASHU_NAME_RYAKU.PopupWindowId = r_framework.Const.WINDOW_ID.MAIN_MENU;
            this.SHASHU_NAME_RYAKU.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHASHU_NAME_RYAKU.popupWindowSetting")));
            this.SHASHU_NAME_RYAKU.ReadOnly = true;
            this.SHASHU_NAME_RYAKU.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHASHU_NAME_RYAKU.RegistCheckMethod")));
            this.SHASHU_NAME_RYAKU.Size = new System.Drawing.Size(286, 20);
            this.SHASHU_NAME_RYAKU.TabIndex = 150;
            this.SHASHU_NAME_RYAKU.TabStop = false;
            this.SHASHU_NAME_RYAKU.Tag = " ";
            // 
            // SHARYOU_LABEL
            // 
            this.SHARYOU_LABEL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(105)))), ((int)(((byte)(51)))));
            this.SHARYOU_LABEL.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHARYOU_LABEL.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SHARYOU_LABEL.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHARYOU_LABEL.ForeColor = System.Drawing.Color.White;
            this.SHARYOU_LABEL.Location = new System.Drawing.Point(0, 112);
            this.SHARYOU_LABEL.Name = "SHARYOU_LABEL";
            this.SHARYOU_LABEL.Size = new System.Drawing.Size(110, 20);
            this.SHARYOU_LABEL.TabIndex = 160;
            this.SHARYOU_LABEL.Text = "車輌";
            this.SHARYOU_LABEL.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SHASHU_CD
            // 
            this.SHASHU_CD.BackColor = System.Drawing.SystemColors.Window;
            this.SHASHU_CD.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SHASHU_CD.ChangeUpperCase = true;
            this.SHASHU_CD.CharacterLimitList = null;
            this.SHASHU_CD.CharactersNumber = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.SHASHU_CD.DBFieldsName = "";
            this.SHASHU_CD.DefaultBackColor = System.Drawing.Color.Empty;
            this.SHASHU_CD.DisplayItemName = "車種";
            this.SHASHU_CD.DisplayPopUp = null;
            this.SHASHU_CD.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHASHU_CD.FocusOutCheckMethod")));
            this.SHASHU_CD.Font = new System.Drawing.Font("ＭＳ ゴシック", 9.75F);
            this.SHASHU_CD.ForeColor = System.Drawing.Color.Black;
            this.SHASHU_CD.GetCodeMasterField = "SHASHU_CD,SHASHU_NAME_RYAKU";
            this.SHASHU_CD.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.SHASHU_CD.IsInputErrorOccured = false;
            this.SHASHU_CD.ItemDefinedTypes = "varchar";
            this.SHASHU_CD.Location = new System.Drawing.Point(115, 90);
            this.SHASHU_CD.MaxLength = 3;
            this.SHASHU_CD.Name = "SHASHU_CD";
            this.SHASHU_CD.PopupAfterExecute = null;
            this.SHASHU_CD.PopupBeforeExecute = null;
            this.SHASHU_CD.PopupGetMasterField = "SHASHU_CD,SHASHU_NAME_RYAKU";
            this.SHASHU_CD.PopupSearchSendParams = ((System.Collections.ObjectModel.Collection<r_framework.Dto.PopupSearchSendParamDto>)(resources.GetObject("SHASHU_CD.PopupSearchSendParams")));
            this.SHASHU_CD.PopupSetFormField = "SHASHU_CD,SHASHU_NAME";
            this.SHASHU_CD.PopupWindowId = r_framework.Const.WINDOW_ID.M_SHASHU;
            this.SHASHU_CD.PopupWindowName = "マスタ共通ポップアップ";
            this.SHASHU_CD.popupWindowSetting = ((System.Collections.ObjectModel.Collection<r_framework.Dto.JoinMethodDto>)(resources.GetObject("SHASHU_CD.popupWindowSetting")));
            this.SHASHU_CD.RegistCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject("SHASHU_CD.RegistCheckMethod")));
            this.SHASHU_CD.SetFormField = "SHASHU_CD,SHASHU_NAME_RYAKU";
            this.SHASHU_CD.ShortItemName = "車種CD";
            this.SHASHU_CD.Size = new System.Drawing.Size(55, 20);
            this.SHASHU_CD.TabIndex = 140;
            this.SHASHU_CD.Tag = "車種を指定してください";
            this.SHASHU_CD.ZeroPaddengFlag = true;
            // 
            // KensakuControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.SHARYOU_NAME_RYAKU);
            this.Controls.Add(this.SHASHU_LABEL);
            this.Controls.Add(this.SHARYOU_CD);
            this.Controls.Add(this.SHASHU_NAME_RYAKU);
            this.Controls.Add(this.SHARYOU_LABEL);
            this.Controls.Add(this.SHASHU_CD);
            this.Controls.Add(this.ISNOT_NEED_DELETE_FLG);
            this.Controls.Add(this.UNPAN_GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.UNPAN_GYOUSHA_CD);
            this.Controls.Add(this.UNPAN_GYOUSHA_LABEL);
            this.Controls.Add(this.GENBA_NAME_RYAKU);
            this.Controls.Add(this.GENBA_CD);
            this.Controls.Add(this.GENBA_LABEL);
            this.Controls.Add(this.GYOUSHA_NAME_RYAKU);
            this.Controls.Add(this.GYOUSHA_CD);
            this.Controls.Add(this.GYOUSHA_LABEL);
            this.Controls.Add(this.TORIHIKISAKI_NAME_RYAKU);
            this.Controls.Add(this.TORIHIKISAKI_CD);
            this.Controls.Add(this.TORIHIKISAKI_LABEL);
            this.Name = "KensakuControl";
            this.Size = new System.Drawing.Size(523, 137);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal r_framework.CustomControl.CustomTextBox TORIHIKISAKI_NAME_RYAKU;
        internal System.Windows.Forms.Label TORIHIKISAKI_LABEL;
        internal r_framework.CustomControl.CustomTextBox GYOUSHA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GYOUSHA_CD;
        internal System.Windows.Forms.Label GYOUSHA_LABEL;
        internal r_framework.CustomControl.CustomTextBox GENBA_NAME_RYAKU;
        internal System.Windows.Forms.Label GENBA_LABEL;
        internal r_framework.CustomControl.CustomTextBox UNPAN_GYOUSHA_NAME_RYAKU;
        internal r_framework.CustomControl.CustomAlphaNumTextBox UNPAN_GYOUSHA_CD;
        internal System.Windows.Forms.Label UNPAN_GYOUSHA_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox GENBA_CD;
        internal r_framework.CustomControl.CustomAlphaNumTextBox TORIHIKISAKI_CD;
        internal r_framework.CustomControl.CustomTextBox ISNOT_NEED_DELETE_FLG;
        internal r_framework.CustomControl.CustomTextBox SHARYOU_NAME_RYAKU;
        internal System.Windows.Forms.Label SHASHU_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHARYOU_CD;
        internal r_framework.CustomControl.CustomTextBox SHASHU_NAME_RYAKU;
        internal System.Windows.Forms.Label SHARYOU_LABEL;
        internal r_framework.CustomControl.CustomAlphaNumTextBox SHASHU_CD;
    }
}
