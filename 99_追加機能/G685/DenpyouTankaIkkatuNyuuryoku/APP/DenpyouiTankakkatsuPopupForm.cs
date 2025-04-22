using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Shougun.Core.BusinessManagement.DenpyouIkkatuPopupUpdate.DTO;
using Shougun.Core.BusinessManagement.DenpyouIkkatuPopupUpdate.Logic;
using Shougun.Core.BusinessManagement.DenpyouDetailIkkatuUpdate.DTO;
using System.Drawing;
using r_framework.CustomControl;

namespace Shougun.Core.BusinessManagement.DenpyouIkkatuPopupUpdate.APP
{
    public partial class DenpyouiTankakkatsuPopupForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 範囲条件指定ロジッククラス
        /// </summary>
        private DenpyouiTankakkatsuLogic logic;

        internal bool isInputError = false;

        /// <summary> 入力パラメータ </summary>
        public NyuuryokuParamDto NyuuryokuParam { get; set; }


        public bool cbxHinmei { get; set; }
        public bool cbxSuuryou { get; set; }
        public bool cbxTanka { get; set; }
        public bool cbxMeisaiBikou { get; set; }
        public bool cbxUnit { get; set; }

        public string denshuKbnCd { get; set; }

        /// <summary>
        /// エラー発生状態(True:エラー発生)
        /// </summary>
        private bool error;

        #endregion

        public DenpyouiTankakkatsuPopupForm(SendPopupParam SendParam)
        {
            this.InitializeComponent();

            this.cbxHinmei = SendParam.cbxHinmei;
            this.cbxSuuryou = SendParam.cbxSuuryou;
            this.cbxTanka = SendParam.cbxTanka;
            this.cbxMeisaiBikou = SendParam.cbxMeisaiBikou;
            this.cbxUnit = SendParam.cbxUnit;
            this.denshuKbnCd = SendParam.denshuKbnCd;

            // ロジッククラス作成
            this.logic = new DenpyouiTankakkatsuLogic(this);

        }

        /// <summary>
        /// Form読み込み処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnLoad(EventArgs e)
        {
            // 親クラスのロード
            base.OnLoad(e);

            // 画面の初期化
            this.logic.WindowInit();
        }

        /// <summary>
        /// Form読み込み処理
        /// </summary>
        /// <param name="e">イベントデータ</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            if (this.error == true)
            {
                // エラーが発生している場合は閉じない
                // ※DialogResult設定を行っている場合はFormCloseしてしまうため
                e.Cancel = true;
            }
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void FormClose(object sender, EventArgs e)
        {
            // エラーキャンセル
            this.error = false;

            // Formクローズ
            this.Close();
        }

        /// <summary>
        /// Formクリア処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void kuria(object sender, EventArgs e)
        {
            this.logic.Kuria();
        }

        /// <summary>
        /// 一括入力処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void Nyuuryoku(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;

            if (!this.logic.SaveParams()) { return; }

            // Formクローズ
            this.Close();

        }

        private void HINMEI_CD_UKEIRE_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox tb = this.HINMEI_CD_UKEIRE as CustomTextBox;
            bool changed = false;
            if (tb.IsInputErrorOccured)
            {
                changed = true;
            }
            else
            {
                changed = !string.Equals(tb.prevText, tb.Text);
            }
            if (!changed)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.HINMEI_CD_UKEIRE.Text))
            {
                this.HINMEI_NAME_UKEIRE.Text = string.Empty;
                this.DENPYOU_KBN_CD.Text = string.Empty;
                this.DENPYOU_KBN_NAME.Text = string.Empty;
            }
            else
            {
                // 品名名称の取得
                if (this.logic.SearchHinmeiNameUkeire(e))
                {
                    this.HINMEI_CD_UKEIRE.IsInputErrorOccured = true;
                    return;
                }

                if (string.IsNullOrEmpty(this.DENPYOU_KBN_CD.Text))
                {
                    this.DENPYOU_KBN_NAME.Text = string.Empty;
                    bool bResult = this.logic.SetDenpyouKbn();
                    if (!bResult)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void HINMEI_CD_SHUKKA_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox tb = this.HINMEI_CD_SHUKKA as CustomTextBox;
            bool changed = false;
            if (tb.IsInputErrorOccured)
            {
                changed = true;
            }
            else
            {
                changed = !string.Equals(tb.prevText, tb.Text);
            }
            if (!changed)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.HINMEI_CD_SHUKKA.Text))
            {
                this.HINMEI_NAME_SHUKKA.Text = string.Empty;
                this.DENPYOU_KBN_CD.Text = string.Empty;
                this.DENPYOU_KBN_NAME.Text = string.Empty;
            }
            else
            {
                // 品名名称の取得
                if (this.logic.SearchHinmeiNameShukka(e))
                {
                    this.HINMEI_CD_SHUKKA.IsInputErrorOccured = true;
                }

                if (string.IsNullOrEmpty(this.DENPYOU_KBN_CD.Text))
                {
                    this.DENPYOU_KBN_NAME.Text = string.Empty;
                    bool bResult = this.logic.SetDenpyouKbn();
                    if (!bResult)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void HINMEI_CD_URSH_Validating(object sender, CancelEventArgs e)
        {
            CustomTextBox tb = this.HINMEI_CD_URSH as CustomTextBox;
            bool changed = false;
            if (tb.IsInputErrorOccured)
            {
                changed = true;
            }
            else
            {
                changed = !string.Equals(tb.prevText, tb.Text);
            }
            if (!changed)
            {
                return;
            }
            if (string.IsNullOrEmpty(this.HINMEI_CD_URSH.Text))
            {
                this.HINMEI_NAME_URSH.Text = string.Empty;
                this.DENPYOU_KBN_CD.Text = string.Empty;
                this.DENPYOU_KBN_NAME.Text = string.Empty;
            }
            else
            {
                // 品名名称の取得
                if (this.logic.SearchHinmeiNameUrsh(e))
                {
                    this.HINMEI_CD_SHUKKA.IsInputErrorOccured = true;
                }

                if (string.IsNullOrEmpty(this.DENPYOU_KBN_CD.Text))
                {
                    this.DENPYOU_KBN_NAME.Text = string.Empty;
                    bool bResult = this.logic.SetDenpyouKbn();
                    if (!bResult)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        private void TANNKA_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.TANNKA.Text) && this.TANNKA.ReadOnly == false)
            {

                this.KINNGAKU.ReadOnly = true;
                this.TANNKA_ZOUGENN.ReadOnly = true;
                this.KINNGAKU.Text = string.Empty;
                this.TANNKA_ZOUGENN.Text = string.Empty;
            }
            else if (string.IsNullOrWhiteSpace(this.TANNKA.Text) && this.TANNKA.ReadOnly == false)
            {
                this.KINNGAKU.ReadOnly = false;
                this.TANNKA_ZOUGENN.ReadOnly = false;
                this.KINNGAKU.Text = string.Empty;
                this.TANNKA_ZOUGENN.Text = string.Empty;
            }
        }

        private void TANNKA_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TANNKA.Text))
            {
                this.KINNGAKU.TabStop = false;
                this.TANNKA_ZOUGENN.TabStop = false;
            }
            else
            {
                this.KINNGAKU.TabStop = true;
                this.TANNKA_ZOUGENN.TabStop = true;
            }
        }

        private void KINNGAKU_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.KINNGAKU.Text) && this.KINNGAKU.ReadOnly == false)
            {
                this.TANNKA.ReadOnly = true;
                this.TANNKA_ZOUGENN.ReadOnly = true;
                this.TANNKA.Text = string.Empty;
                this.TANNKA_ZOUGENN.Text = string.Empty;
            }
            else if (string.IsNullOrWhiteSpace(this.KINNGAKU.Text) && this.KINNGAKU.ReadOnly == false)
            {
                this.TANNKA.ReadOnly = false;
                this.TANNKA_ZOUGENN.ReadOnly = false;
                this.TANNKA.Text = string.Empty;
                this.TANNKA_ZOUGENN.Text = string.Empty;
            }
        }

        private void KINNGAKU_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.KINNGAKU.Text))
            {
                this.TANNKA.TabStop = false;
                this.TANNKA_ZOUGENN.TabStop = false;
            }
            else
            {
                this.TANNKA.TabStop = true;
                this.TANNKA_ZOUGENN.TabStop = true;
            }
        }

        private void TANNKA_ZOUGENN_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(this.TANNKA_ZOUGENN.Text) && this.TANNKA_ZOUGENN.ReadOnly == false)
            {
                this.TANNKA.ReadOnly = true;
                this.KINNGAKU.ReadOnly = true;
                this.TANNKA.Text = string.Empty;
                this.KINNGAKU.Text = string.Empty;
            }
            else if (string.IsNullOrWhiteSpace(this.TANNKA_ZOUGENN.Text) && this.TANNKA_ZOUGENN.ReadOnly == false)
            {
                this.TANNKA.ReadOnly = false;
                this.KINNGAKU.ReadOnly = false;
                this.TANNKA.Text = string.Empty;
                this.KINNGAKU.Text = string.Empty;
            }
        }

        private void TANNKA_ZOUGENN_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TANNKA_ZOUGENN.Text))
            {
                this.TANNKA.TabStop = false;
                this.KINNGAKU.TabStop = false;
            }
            else
            {
                this.TANNKA.TabStop = true;
                this.KINNGAKU.TabStop = true;
            }
        }
    }
}
