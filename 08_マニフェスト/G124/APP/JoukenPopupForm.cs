using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using System.Collections.Generic;
using r_framework.Entity;
using r_framework.Logic;

namespace Shougun.Core.PaperManifest.ManifestCheckHyo.APP
{
    public partial class JoukenPopupForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 範囲条件指定ロジッククラス
        /// </summary>
        private LogicClassJouken logic;

        /// <summary>メッセージクラス</summary>
        public MessageBoxShowLogic messageShowLogic { get; private set; }

        /// <summary>
        /// エラー発生状態(True:エラー発生)
        /// </summary>
        private bool error;

        #endregion

        public JoukenPopupForm(JoukenParam joukenParam)
        {
            this.InitializeComponent();

            // ロジッククラス作成
            this.logic = new LogicClassJouken(this);

            this.messageShowLogic = new MessageBoxShowLogic();
            this.Joken = joukenParam;
        }

        public JoukenParam Joken { get; set; }

        /// <summary>
        /// 検索実行処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void SearchExec(object sender, EventArgs e)
        {
            /// 20141022 Houkakou 「マニチェック表」の日付チェックを追加する　start
            if (this.logic.DateCheck())
            {
                // エラー
                this.error = true;
                return;
            }
            /// 20141022 Houkakou 「マニチェック表」の日付チェックを追加する　end
            //// 設定条件を保存
            if (!this.logic.SaveParams()) { return; }

            // Formクローズ
            this.FormClose(sender, null);
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
        /// キー押下処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void ItemKeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F7:
                    // 検索実行
                    this.DialogResult = DialogResult.OK;
                    this.SearchExec(sender, null);
                    break;
                case Keys.F12:
                    // キャンセル
                    this.DialogResult = DialogResult.Cancel;
                    this.FormClose(sender, null);
                    break;
                default:
                    // NOTHING
                    break;
            }
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
            this.logic.WindowInit(this.Joken);
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

        /// <summary> チェック条件と年月日のコントロールコントロールの活性/非活性制御を実行する </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void CustomNumericTextBox2CheckTaishou_TextChanged(object sender, EventArgs e)
        {
            // 対象に依ってチェック条件と年月日のコントロールコントロールの活性/非活性制御を行う
            if (this.CustomNumericTextBox2CheckTaishou.Text == "1")
            {
                // 対象：マニフェスト
                this.customPanelCheckJouken.Enabled = true;
                this.CntlcustomPanelNemgappi();                      // 年月日コントロールの活性/非活性関数をCALL
                // 20140626 ria EV004852 一覧と抽出条件の変更 start
                this.customPanelCheckItem.Enabled = true;
                // 20140626 ria EV004852 一覧と抽出条件の変更 end
                this.JYOUKEN_DELETE_FLG.Enabled = false;
                this.JYOUKEN_DELETE_FLG.Checked = false;

                this.Joken_EnableCange();
            }
            else if (this.CustomNumericTextBox2CheckTaishou.Text == "2")
            {
                // 対象：マスタ
                this.customPanelCheckJouken.Enabled = false;
                this.customPanelNemgappi.Enabled = false;       // 非活性
                // 20140626 ria EV004852 一覧と抽出条件の変更 start
                this.customPanelCheckItem.Enabled = false;
                // 20140626 ria EV004852 一覧と抽出条件の変更 end
                this.JYOUKEN_DELETE_FLG.Enabled = true;
                this.JYOUKEN_DELETE_FLG.Checked = true;

                this.cantxt_UnpanJyutakuNameCd.Enabled = false;
                this.cantxt_SyobunJyutakuNameCd.Enabled = false;
                this.cantxt_UnpanJyugyobaNameCd.Enabled = false;
            }
            this.CntlcustomPanelTeishutsu();
        }

        /// <summary> チェック条件に依る年月日コントロールの活性/非活性制御を実行する </summary>
        private void CntlcustomPanelNemgappi()
        {
            // 条件:5(交付年月日なし)、6(紐付け不整合チェック)
            if ((this.CustomNumericTextBox2CheckJouken.Text == "5") ||
                (this.CustomNumericTextBox2CheckJouken.Text == "6"))
            {
                this.customPanelNemgappi.Enabled = false;   // 非活性
            }
            else
            {
                this.customPanelNemgappi.Enabled = true;    // 活性
            }
        }

        /// <summary>
        /// チェック条件による、運搬受託者・処分受託者・処分事業場のコントロールの活性/非活性制御を実行する
        /// </summary>
        private void Joken_EnableCange()
        {
            // 6(紐付け不整合チェック)
            if (this.CustomNumericTextBox2CheckJouken.Text == "6")
            {
                // 非活性
                this.cantxt_SyobunJyutakuNameCd.Enabled = false;
                this.cantxt_UnpanJyugyobaNameCd.Enabled = false;
                this.cantxt_UnpanJyutakuNameCd.Enabled = false;
            }
            else
            {
                // 活性
                this.cantxt_SyobunJyutakuNameCd.Enabled = true;
                this.cantxt_UnpanJyugyobaNameCd.Enabled = true;
                this.cantxt_UnpanJyutakuNameCd.Enabled = true;
            }
        }

        /// <summary> チェック条件に依る提出先コントロールの活性/非活性制御を実行する </summary>
        private void CntlcustomPanelTeishutsu()
        {
            // 条件:マニフェスト・処分方法
            if (this.cbxCheckItem9.Checked && this.CustomNumericTextBox2CheckTaishou.Text == "1")
            {
                this.CHIIKI_CD.Enabled = true;    // 活性
                this.CHIIKI_NAME.Enabled = true;    // 活性
                this.bt_chiiki_search.Enabled = true;    // 活性
            }
            else
            {
                this.CHIIKI_CD.Enabled = false;   // 非活性
                this.CHIIKI_NAME.Enabled = false;   // 非活性
                this.bt_chiiki_search.Enabled = false;   // 非活性
                this.CHIIKI_CD.Text = string.Empty;
                this.CHIIKI_NAME.Text = string.Empty;
            }
        }

        /// <summary> 表示のタイミングでコントロールの活性/非活性の制御を行う </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        private void JoukenPopupForm_Shown(object sender, EventArgs e)
        {
            // 対象に依ってチェック条件と年月日のコントロールコントロールの活性/非活性制御を行う
            if (this.CustomNumericTextBox2CheckTaishou.Text == "1")
            {
                // 対象：マニフェスト
                this.customPanelCheckJouken.Enabled = true;
                this.CntlcustomPanelNemgappi();                      // 年月日コントロールの活性/非活性関数をCALL
                // 20140626 ria EV004852 一覧と抽出条件の変更 start
                this.customPanelCheckItem.Enabled = true;
                // 20140626 ria EV004852 一覧と抽出条件の変更 end
                this.Joken_EnableCange();
            }
            else if (this.CustomNumericTextBox2CheckTaishou.Text == "2")
            {
                // 対象：マスタ
                this.customPanelCheckJouken.Enabled = false;
                this.customPanelNemgappi.Enabled = false;       // 非活性
                // 20140626 ria EV004852 一覧と抽出条件の変更 start
                this.customPanelCheckItem.Enabled = false;
                // 20140626 ria EV004852 一覧と抽出条件の変更 end
            }
        }

        private void customDateTimePickerHidukeHaniShiteiStart_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.customDateTimePickerHidukeHaniShiteiEnd.Text))
            {
                this.customDateTimePickerHidukeHaniShiteiEnd.IsInputErrorOccured = false;
                this.customDateTimePickerHidukeHaniShiteiEnd.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void customDateTimePickerHidukeHaniShiteiEnd_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.customDateTimePickerHidukeHaniShiteiStart.Text))
            {
                this.customDateTimePickerHidukeHaniShiteiStart.IsInputErrorOccured = false;
                this.customDateTimePickerHidukeHaniShiteiStart.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void CustomNumericTextBox2CheckJouken_TextChanged(object sender, EventArgs e)
        {
            this.CntlcustomPanelNemgappi();
            this.Joken_EnableCange();
        }

        private void cbxCheckItem9_CheckedChanged(object sender, EventArgs e)
        {
            this.CntlcustomPanelTeishutsu();
        }

        /// <summary>
        /// チェック条件のコントロールの活性/日か正制御を実行する
        /// 電マニ含む条件の場合⇒活性。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomNumericTextBox2CheckBunrui_TextChanged(object sender, EventArgs e)
        {
            // 対象に依ってチェック条件のコントロールコントロールの活性/非活性制御を行う
            if (this.CustomNumericTextBox2CheckBunrui.Text == "1" || this.CustomNumericTextBox2CheckBunrui.Text == "2" || this.CustomNumericTextBox2CheckBunrui.Text == "3")
            {
                // 対象：紙マニ（産廃・積替・建廃）のみ
                this.JYOUKEN_YOYAKU_FLG.Enabled = false;
            }
            else if (this.CustomNumericTextBox2CheckBunrui.Text == "4" || this.CustomNumericTextBox2CheckBunrui.Text == "5")
            {
                // 対象：電マニ含む
                this.JYOUKEN_YOYAKU_FLG.Enabled = true;
            }

        }

        /// <summary>
        /// Enter時の値保持
        /// </summary>
        private Dictionary<Control, string> _EnterValue = new Dictionary<Control, string>();
        private object lastObject = null;

        internal void EnterEventInit()
        {
            foreach (var c in controlUtil.GetAllControls(this))
            {
                c.Enter += new EventHandler(this.SaveTextOnEnter);
            }
        }

        /// <summary>
        /// Enter時　入力値保存
        /// </summary>
        /// <param name="value"></param>
        private void SaveTextOnEnter(object sender, EventArgs e)
        {
            var value = sender as Control;

            if (value == null)
            {
                return;
            }

            //エラー等でフォーカス移動しなかった場合は、値クリアして強制チェックするようにする。 
            // ※1（正常）→0（エラー）→1と入れた場合 チェックする。
            // ※※この処理がない場合、0（エラー）→0（ノーチェック）となってしまう。
            if (lastObject == sender)
            {
                if (_EnterValue.ContainsKey(value))
                {
                    _EnterValue[value] = null;
                }
                else
                {
                    _EnterValue.Add(value, null);
                }

                return;

            }

            this.lastObject = sender;

            if (_EnterValue.ContainsKey(value))
            {
                _EnterValue[value] = value.Text;
            }
            else
            {
                _EnterValue.Add(value, value.Text);
            }
        }
        /// <summary>
        /// 値比較時
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal string get_EnterValue(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return null;
            }
            return _EnterValue[value];
        }

        /// <summary>
        /// 変更チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        internal bool isChanged(object sender)
        {
            var value = sender as Control;

            if (value == null)
            {
                return true; //その他は常時変更有とみなす
            }

            string oldValue = this.get_EnterValue(value);

            return !string.Equals(oldValue, value.Text); //一致する場合変更なし
        }

        private void cantxt_UnpanJyutakuNameCd_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.ctxt_UnpanJyutakuName.Text = string.Empty;

            if (string.IsNullOrEmpty(this.cantxt_UnpanJyutakuNameCd.Text))
            {
                return;
            }
            else
            {
                int a = this.logic.UPNCheck(this.cantxt_UnpanJyutakuNameCd.Text);

                if (a != 0)
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                }
            }

        }

        private void cantxt_SyobunJyutakuNameCd_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.ctxt_SyobunJyutakuName.Text = string.Empty;
            this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
            this.ctxt_UnpanJyugyobaName.Text = string.Empty;

            if (string.IsNullOrEmpty(this.cantxt_SyobunJyutakuNameCd.Text))
            {
                return;
            }
            else
            {
                int a = this.logic.SBNCheck(this.cantxt_SyobunJyutakuNameCd.Text,"");
                if (a !=  0)
                {
                    this.messageShowLogic.MessageBoxShow("E020", "業者");
                    e.Cancel = true;
                }
            }
        }

        private void cantxt_UnpanJyugyobaNameCd_Validating(object sender, CancelEventArgs e)
        {
            if (!isChanged(sender)) { return; }

            this.ctxt_UnpanJyugyobaName.Text = string.Empty;

            if (string.IsNullOrEmpty(this.cantxt_UnpanJyugyobaNameCd.Text))
            {
                return;
            }
            else
            {
                if (string.IsNullOrEmpty(this.cantxt_SyobunJyutakuNameCd.Text))
                {
                    this.messageShowLogic.MessageBoxShow("E051", "処分受託者");
                    this.cantxt_UnpanJyugyobaNameCd.Text = string.Empty;
                    e.Cancel = true;
                    return;
                }

                int a = this.logic.SBNCheck(this.cantxt_SyobunJyutakuNameCd.Text, this.cantxt_UnpanJyugyobaNameCd.Text);

                if (a !=  0)
                {
                    this.messageShowLogic.MessageBoxShow("E020", "現場");
                    e.Cancel = true;
                }
            }
        }
    }
}
