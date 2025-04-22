using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Utility;
using Shougun.Core.Common.Kakepopup.Const;
using Shougun.Core.Common.Kakepopup.Logic;
using r_framework.APP.Base;


namespace Shougun.Core.Common.Kakepopup.App
{
    /// <summary>
    /// 範囲条件指定ポップアップ画面
    /// </summary>
    public partial class KakePopupForm : SuperForm
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
        //public KakePopupForm(Form form, WINDOW_TYPE windowType)
		public KakePopupForm(UIConstans.ConditionInfo param)
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
            this.txt_starttorihikisakicd.Focus();
            this.PreviousValue = string.Empty;
        }

        /// <summary>
        /// ポップアップによる検索後処理(To側)
        /// </summary>
        public void ToCDPopupAfterUpdate()
        {
            // 整合性チェックを走らせるためにSetFocusを強制発行し、前回値を削除する
            // ※CustomControlOnEnterのタイミングで前回値は更新されるため、
            // ※先に前回値を削除してしまうと、前回値削除⇒SetFocus⇒OnEnter⇒前回値更新といった流れになってしまう
            this.txt_endtorihikisakicd.Focus();
            this.PreviousValue = string.Empty;
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
            if (this.Error == true)
            {
                // エラーが発生している場合は閉じない
                // ※DialogResult設定を行っている場合はFormCloseしてしまうため
                e.Cancel = true;
            }
        }

        #region "ボタン押下イベント"
        /// <summary>
        /// 検索期間算出処理(前月)
        /// </summary>
        public virtual void Function1Click(object sender, EventArgs e)
        {
            if (this.btn_zengetsu.Enabled)
            {
                object dateFrom = null;
                object dateTo = null;

                this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, false);

                dtp_denpyouhizukefrom.Value = dateFrom;
                dtp_denpyouhizuketo.Value = dateTo;
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

                this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, true);

                dtp_denpyouhizukefrom.Value = dateFrom;
                dtp_denpyouhizuketo.Value = dateTo;
            }
        }

        /// <summary>
        /// 検索実行処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void SearchExec(object sender, EventArgs e)
        {

            //取引先FromToがブランクの場合MIN、MAXをセットする。
            if (this.txt_starttorihikisakicd.Text == "")
            {
                this.txt_starttorihikisakicd.Text = this.logic.GetTorihikisakiCdMin();
            }
            if (this.txt_endtorihikisakicd.Text == "")
            {
                this.txt_endtorihikisakicd.Text = this.logic.GetTorihikisakiCdMax();
            }

            // 必須チェック
            this.Error = this.logic.RegistCheck();
            if (this.Error == false)
            {
                //// 設定条件を保存
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

        #endregion "ボタン押下イベント"


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

        #region "出力内容テキストボックスイベント"
        /// <summary>
        /// 出力内容テキストボックス変更
        /// </summary>
        private void txt_syuturyokunaiyou_TextChanged(object sender, EventArgs e)
        {
            this.logic.TxtSyuturyokuNaiyouChangedLogic();
        }
        #endregion "出力内容テキストボックスイベント"


        /// <summary>
        /// 取引先Toをダブルクリックで、FromからToへコードをコピーする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TorihikisakiToDoubleClick(object sender, MouseEventArgs e)
        {
            this.txt_endtorihikisakicd.Text = this.txt_starttorihikisakicd.Text;
            this.txt_endtorihikisakiname.Text = this.txt_starttorihikisakiname.Text;
        }
    }
}
