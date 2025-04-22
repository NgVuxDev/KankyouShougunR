using System;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Shougun.Core.Message;
using System.ComponentModel;

namespace Shougun.Core.BusinessManagement.MitumoriIchiran
{
    public partial class HeaderForm : r_framework.APP.Base.HeaderBaseForm
    {
        #region ヘッダ画面Form

        public HeaderForm()
        {
            try
            {
                // 初期化
                InitializeComponent();

                // Load前に非表示にすれば、タイトルは左に詰まる
                base.windowTypeLabel.Visible = false;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("HeaderForm", ex);
                throw ex;
            }
        }

        #endregion

        #region 画面コントロールイベント

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                // 画面Load
                base.OnLoad(e);

                // 初期値設定
                this.ReadDataNumber.Text = "0";
                var parentForm = (BusinessBaseForm)this.Parent;
                this.HIDUKE_TO.Value = parentForm.sysDate;
                this.HIDUKE_FROM.Value = parentForm.sysDate;
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("OnLoad", ex);
                throw ex;
            }
        }

        private void txtNum_HidukeSentaku_Validating(object sender, CancelEventArgs e)
        {
            // 日付区分「1」、「2」以外場合
            if (string.IsNullOrEmpty(this.txtNum_HidukeSentaku.Text))
            {
                MessageBoxUtility.MessageBoxShow("W001", "1", "2");
                e.Cancel = true;
            }
        }

        private void radbtnDenpyouHiduke_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // 伝票日付ラジオボタンチェック
                if (this.radbtnDenpyouHiduke.Checked)
                {
                    this.txtNum_HidukeSentaku.Text = "1";
                    this.lab_HidukeNyuuryoku.Text = "伝票日付※";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("radbtnDenpyouHiduke_CheckedChanged", ex);
                throw ex;
            }
        }

        private void radbtnNyuuryokuHiduke_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                // 入力日付ラジオボタンチェック
                if (this.radbtnNyuuryokuHiduke.Checked)
                {
                    this.txtNum_HidukeSentaku.Text = "2";
                    this.lab_HidukeNyuuryoku.Text = "入力日付※";
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("radbtnNyuuryokuHiduke_CheckedChanged", ex);
                throw ex;
            }
        }

        private void KYOTEN_CD_Leave(object sender, EventArgs e)
        {
            try
            {
                // 変数定義
                int i;

                // 拠点整数以外場合
                if (!int.TryParse(this.KYOTEN_CD.Text, out i))
                {
                    this.KYOTEN_CD.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Fatal("KYOTEN_CD_Leave", ex);
                throw ex;
            }
        }

        #endregion

        // koukouei 20141021 「From > To」のアラート表示タイミング変更 start
        private void HIDUKE_FROM_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_TO.IsInputErrorOccured = false;
            this.HIDUKE_TO.BackColor = Constans.NOMAL_COLOR;
        }

        private void HIDUKE_TO_Leave(object sender, EventArgs e)
        {
            this.HIDUKE_FROM.IsInputErrorOccured = false;
            this.HIDUKE_FROM.BackColor = Constans.NOMAL_COLOR;
        }

        // koukouei 20141021 「From > To」のアラート表示タイミング変更 end
    }
}