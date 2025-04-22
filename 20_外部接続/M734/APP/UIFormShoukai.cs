using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Utility;
using Seasar.Quill;

// 電子契約最新照会のF1から展開

namespace Shougun.Core.ExternalConnection.DenshiKeiyakuSaishinShoukai
{
    public partial class ShoukaiJouken : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClassShoukai logic;

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ShoukaiJouken()
            : base()
        {
            try
            {
                this.InitializeComponent();

                // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
                this.logic = new LogicClassShoukai(this);

                // 完全に固定。ここには変更を入れない
                QuillInjector.GetInstance().Inject(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region 初期化

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.logic = new LogicClassShoukai(this);

            if (!this.logic.WindowInit()) { return; }
        }

        #endregion

        #region ファンクションベント

        #region F8 最新照会
        /// <summary>
        /// F8 最新照会
        /// </summary>
        internal void Reference(object sender, EventArgs e)
        {
            // 作成者の入力チェック
            if (string.IsNullOrEmpty(this.SHAIN_CD.Text))
            {
                this.logic.msgLogic.MessageBoxShow("E001", "作成者");
                return;
            }

            // クライアントIDが設定されているか確認する。
            if (!this.logic.CheckClientId()) { return; }

            // ページのチェック
            if (!this.logic.CheckPage()) { return; }

            if (this.logic.DenshiKeiyakuReference())
            {
                this.logic.msgLogic.MessageBoxShowInformation("照会が完了しました。");
            }

            // 照会して閉じる場合はOKを返す
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        #endregion

        #region F12 ｷｬﾝｾﾙ

        /// <summary>
        /// F12 Formクローズ処理
        /// </summary>
        internal void FormClose(object sender, EventArgs e)
        {
            // ｷｬﾝｾﾙで閉じる場合はキャンセルを返す
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        #endregion

        #endregion

        #region Formイベント

        /// <summary>
        /// キー押下時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ControlKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F8:
                    ControlUtility.ClickButton(this, "bt_func8");
                    break;
                case Keys.F12:
                    ControlUtility.ClickButton(this, "bt_func12");
                    break;
                case Keys.Enter:
                    //SendKeys.Send("{TAB}");
                    break;
            }
        }

        /// <summary>
        /// 前回照会最終ページにフォーカスがあるときにキーが押されると発生します
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_LastReference_KeyPress(object sender, KeyPressEventArgs e)
        {
            //「.」と「,」を除く
            if (e.KeyChar == (char)46 || e.KeyChar == (char)44) e.Handled = true;
        }

        /// <summary>
        /// 契約状況のキーダウンイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Keiyaku_Jyoukyou_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.logic.CheckListPopup();
            }
        }

        /// <summary>
        /// 契約状況のフォーカスアウトイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KEIYAKU_JYOUKYOU_CD_Validated(object sender, EventArgs e)
        {
            // 番号が削除された場合
            if (string.IsNullOrEmpty(this.KEIYAKU_JYOUKYOU_CD.Text))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = string.Empty;
                return;
            }

            string strKeiyakuJyoukyouCd = this.KEIYAKU_JYOUKYOU_CD.Text;
            if (strKeiyakuJyoukyouCd.Equals("0"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_0;
            }
            else if (strKeiyakuJyoukyouCd.Equals("1"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_1;
            }
            else if (strKeiyakuJyoukyouCd.Equals("2"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_2;
            }
            else if (strKeiyakuJyoukyouCd.Equals("3"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_3;
            }
            else if (strKeiyakuJyoukyouCd.Equals("4"))
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = ConstCls.KEIYAKU_JYOUKYOU_NAME_4;
            }
            else
            {
                this.KEIYAKU_JYOUKYOU_NAME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 全ファイル照会のチェックボックス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbx_AllFile_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbx_AllFile.Checked)
            {
                this.txt_Page.Enabled = false;
            }
            else
            {
                this.txt_Page.Enabled = true;
            }
        }

        #endregion
    }
}