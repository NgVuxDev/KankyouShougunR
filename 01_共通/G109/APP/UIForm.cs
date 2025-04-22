using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Logic;
using Shougun.Core.Common.BusinessCommon.Base.BaseForm;
using Seasar.Quill;
using Seasar.Quill.Attrs;

namespace Shougun.Core.Billing.AtenaLabel
{
    /// <summary>
    /// 宛名ラベル画面
    /// </summary>
    [Implementation]
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 宛名ラベル画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        UIHeader headerForm;

        /// <summary>
        /// 画面種別
        /// </summary>
        public int dispType { get; set; }

        /// <summary>
        /// 変更前業者CD
        /// </summary>
        public string previousGyousha { get; set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm(UIHeader headerForm, int dispType)
            : base(WINDOW_ID.T_ATENA_RABERU, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG)
        {
            // コンポーネントの初期化
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);
            this.headerForm = headerForm;
            this.logic.setHeaderForm(headerForm);
            this.dispType = dispType;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

           /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);
            this.logic.WindowInit();

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void FormClose(object sender, EventArgs e)
        {
            // Formクローズ
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Close();
            parentForm.Close();
        }

        // No.3883-->
        /// <summary>
        /// 締日変更時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbShimebi_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.logic.ChangeShimebiProcess();
        }

        /// <summary>
        /// 期間開始変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpTaishoKikanFrom_Validated(object sender, EventArgs e)
        {
            this.logic.TaishoKikanFrom_TextChanged();
        }

        /// <summary>
        /// 期間終了変更処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtpTaishoKikanTo_Validated(object sender, EventArgs e)
        {
            this.logic.TaishoKikanTo_TextChanged();
        }

        //ThangNguyen [Add] 20150827 #10667 Start
        private void dtpTaishoKikanFrom_Leave(object sender, EventArgs e)
        {
            this.dtpTaishoKikanFrom.BackColor = SystemColors.Window;
            this.dtpTaishoKikanTo.BackColor = SystemColors.Window;
            this.ExecuteAfterLostFocus(this.dtpTaishoKikanFrom);
        }

        private void ExecuteAfterLostFocus(r_framework.CustomControl.CustomDateTimePicker dtPicker)
        {
            try
            {
                string timeTmp = dtPicker.Value.ToString();
                if (timeTmp != "")
                {
                    string[] arrayTime = timeTmp.Split(' ');
                    if (arrayTime.Length > 0 && arrayTime[0].Length == 10)
                    {
                        dtPicker.Value = arrayTime[0];
                    }
                }
            }
            catch (Exception ex)
            {
                ex.ToString();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                string timeTmp = dtPicker.GetResultText();
                if (timeTmp == "")
                {
                    msgLogic.MessageBoxShow("E001", "請求締日");
                    dtPicker.Focus();
                    return;
                }
                else 
                {
                    msgLogic.MessageBoxShow("E084", dtPicker.GetResultText());
                    dtPicker.Focus();
                    return;
                }
            }
        }

        private void dtpTaishoKikanTo_Leave(object sender, EventArgs e)
        {
            this.dtpTaishoKikanFrom.BackColor = SystemColors.Window;
            this.dtpTaishoKikanTo.BackColor = SystemColors.Window;
            this.ExecuteAfterLostFocus(this.dtpTaishoKikanTo);
        }
        //ThangNguyen [Add] 20150827 #10667 End
        // No.3883<--

        public void SetGyoushaPopupBef()
        {
            this.previousGyousha = this.txtGyoushaCd.Text;
        }

        public void SetGyoushaPopupAft()
        {
            if (this.previousGyousha != this.txtGyoushaCd.Text)
            {
                switch (this.txtPrintHouhou.Text)
                {
                    case "1":
                        this.txtKobetuShiteiCd1.Text = string.Empty;
                        this.txtKobetuShiteiCd2.Text = string.Empty;
                        this.txtKobetuShiteiCd3.Text = string.Empty;
                        this.txtKobetuShiteiCd4.Text = string.Empty;
                        this.txtKobetuShiteiCd5.Text = string.Empty;
                        this.txtKobetuShiteiCd6.Text = string.Empty;
                        this.txtKobetuShiteiCd7.Text = string.Empty;
                        this.txtKobetuShiteiCd8.Text = string.Empty;
                        this.txtKobetuShiteiCd9.Text = string.Empty;
                        this.txtKobetuShiteiCd10.Text = string.Empty;
                        this.txtKobetuShiteiCd11.Text = string.Empty;
                        this.txtKobetuShiteiCd12.Text = string.Empty;

                        this.txtKobetuShiteiName1.Text = string.Empty;
                        this.txtKobetuShiteiName2.Text = string.Empty;
                        this.txtKobetuShiteiName3.Text = string.Empty;
                        this.txtKobetuShiteiName4.Text = string.Empty;
                        this.txtKobetuShiteiName5.Text = string.Empty;
                        this.txtKobetuShiteiName6.Text = string.Empty;
                        this.txtKobetuShiteiName7.Text = string.Empty;
                        this.txtKobetuShiteiName8.Text = string.Empty;
                        this.txtKobetuShiteiName9.Text = string.Empty;
                        this.txtKobetuShiteiName10.Text = string.Empty;
                        this.txtKobetuShiteiName11.Text = string.Empty;
                        this.txtKobetuShiteiName12.Text = string.Empty;
                        break;
                    case "2":
                        this.txtGenbaCd.Text = string.Empty;
                        this.txtGenbaName.Text = string.Empty;
                        break;
                }
            }
        }
    }
}
