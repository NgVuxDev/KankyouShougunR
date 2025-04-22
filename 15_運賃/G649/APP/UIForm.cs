// $Id: UIForm.cs 33171 2014-10-23 09:28:33Z fangjk@oec-h.com $
using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using Shougun.Core.Carriage.UnchinDaichouHaniJokenPopUp.Const;
using Shougun.Core.Carriage.UnchinDaichouHaniJokenPopUp.Logic;

namespace Shougun.Core.Carriage.UnchinDaichouHaniJokenPopUp.APP
{
    /// <summary>
    /// 範囲条件指定ポップアップ画面
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 範囲条件
        /// </summary>
        public UIConstans.ConditionInfo Joken { get; set; }
        /// <summary>
        /// 範囲条件指定ロジッククラス
        /// </summary>
        private LogicClass logic;
        /// <summary>
        /// エラー発生状態(True:エラー発生)
        /// </summary>
        private bool Error;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="param">範囲条件情報</param>
        public UIForm(UIConstans.ConditionInfo param)
        {
            // コンポーネントの初期化
            InitializeComponent();

            // ロジッククラス作成
            logic = new LogicClass(this);

            // 画面表示種別を一旦保存
            this.Joken = param;
        }

        /// <summary>
        /// ポップアップによる検索後処理(From側)
        /// </summary>
        public void FromCDPopupAfterUpdate()
        {
            // 整合性チェックを走らせるためにSetFocusを強制発行し、前回値を削除する
            // ※CustomControlOnEnterのタイミングで前回値は更新されるため、
            // ※先に前回値を削除してしまうと、前回値削除⇒SetFocus⇒OnEnter⇒前回値更新といった流れになってしまう
            this.UNPAN_GYOUSHA_CD_FROM.Focus();
        }

        /// <summary>
        /// ポップアップによる検索後処理(To側)
        /// </summary>
        public void ToCDPopupAfterUpdate()
        {
            // 整合性チェックを走らせるためにSetFocusを強制発行し、前回値を削除する
            // ※CustomControlOnEnterのタイミングで前回値は更新されるため、
            // ※先に前回値を削除してしまうと、前回値削除⇒SetFocus⇒OnEnter⇒前回値更新といった流れになってしまう
            this.UNPAN_GYOUSHA_CD_END.Focus();
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
            if ((this.Error == true) || (this.FocusOutErrorFlag == true))
            {
                // エラーが発生している場合は閉じない
                // ※DialogResult設定を行っている場合はFormCloseしてしまうため
                e.Cancel = true;
            }
        }

        #region ボタン押下イベント
        /// <summary>
        /// 検索期間算出処理(前月)
        /// </summary>
        public virtual void Function1Click(object sender, EventArgs e)
        {
            if (this.btn_zengetsu.Enabled)
            {
                object dateFrom = null;
                object dateTo = null;
                bool catchErr = false;
                this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, false, out catchErr);
                if (catchErr) { return; }
                DATE_HANI_START.Value = dateFrom;
                DATE_HANI_END.Value = dateTo;
            }
        }

        /// <summary>
        /// 検索期間算出処理(翌月)
        /// </summary>
        public virtual void Function2Click(object sender, EventArgs e)
        {
            if (this.btn_jigetsu.Enabled)
            {
                object dateFrom = null;
                object dateTo = null;
                bool catchErr = false;
                this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, true, out catchErr);
                if (catchErr) { return; }
                DATE_HANI_START.Value = dateFrom;
                DATE_HANI_END.Value = dateTo;
            }
        }

        /// <summary>
        /// 検索実行処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void SearchExec(object sender, EventArgs e)
        {
            if (this.logic.DateCheck())
            {
                this.Error = true;
                return;
            }

            this.bt_func8.Focus();

            // 登録時チェック
            bool catchErr = false;
            this.Error = this.logic.RegistCheck(out catchErr);
            if (catchErr) { return; }
            if ((this.Error == false) && (this.FocusOutErrorFlag == false))
            {
                // 設定条件を保存
                this.logic.SaveParams();

                // Formクローズ
                this.FormClose(sender, null);
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
            this.Error = false;

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
                case Keys.F1:
                    // 前月
                    this.Function1Click(sender, null);
                    break;
                case Keys.F2:
                    // 次月
                    this.Function2Click(sender, null);
                    break;
                case Keys.F8:
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

        #endregion ボタン押下イベント

        /// <summary>
        /// 開始運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_FROM_Validated(object sender, EventArgs e)
        {
            this.logic.UnpanGyoushaCheck(1);
        }

        /// <summary>
        /// 終了運搬業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_END_Validated(object sender, EventArgs e)
        {
            this.logic.UnpanGyoushaCheck(2);
        }

        /// <summary>
        /// 終了運搬業者 ダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TORIHIKISAKI_CD_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 「開始運搬業者」の情報を「終了運搬業者」へコピーします。
            this.UNPAN_GYOUSHA_CD_END.Text = this.UNPAN_GYOUSHA_CD_FROM.Text;
            this.UNPAN_GYOUSHA_NAME_END.Text = this.UNPAN_GYOUSHA_NAME_FROM.Text;
        }

        /// <summary>
        /// 開始伝票日付フォーカスロスト処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_HANI_START_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DATE_HANI_END.Text))
            {
                this.DATE_HANI_END.IsInputErrorOccured = false;
                this.DATE_HANI_END.BackColor = Constans.NOMAL_COLOR;
            }
        }

        /// <summary>
        /// 終了伝票日付フォーカスロスト処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_HANI_END_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DATE_HANI_START.Text))
            {
                this.DATE_HANI_START.IsInputErrorOccured = false;
                this.DATE_HANI_START.BackColor = Constans.NOMAL_COLOR;
            }
        }

        /// <summary>
        /// 終了伝票日付 ダブルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DATE_HANI_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.DATE_HANI_END.Text = this.DATE_HANI_START.Text;
        }
    }
}
