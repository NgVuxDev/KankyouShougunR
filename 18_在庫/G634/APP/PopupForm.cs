using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.APP.Base;
using Shougun.Core.Stock.ZaikoKanriHyo.Const;

namespace Shougun.Core.Stock.ZaikoKanriHyo
{
    /// <summary>
    /// 範囲条件指定ポップアップ画面
    /// </summary>
    public partial class PopupForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 範囲条件
        /// </summary>
        public UIConstans.ConditionInfo Joken { get; set; }
        /// <summary>
        /// 範囲条件指定ロジッククラス
        /// </summary>
        private PopupLogicClass logic;
        /// <summary>
        /// エラー発生状態(True:エラー発生)
        /// </summary>
        internal bool isError;

        /// <summary>
        /// 業者CDの前回値
        /// </summary>
        internal string preGyoushaCd;

        /// <summary>
        /// 現場CDの前回値
        /// </summary>
        internal string preGenbaCd;

        /// <summary>
        /// 在庫品名CDの前回値
        /// </summary>
        internal string preZaikoHinmeiCd;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="param">範囲条件情報</param>
		public PopupForm(UIConstans.ConditionInfo param)
        {
            // コンポーネントの初期化
            InitializeComponent();

            // ロジッククラス作成
            logic = new PopupLogicClass(this);

            // 画面表示種別を一旦保存
            this.Joken = param;
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
            if (this.isError)
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
                bool catchErr = false;
                this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, false, out catchErr);
                if (catchErr) { return; }

                DATE_FROM.Value = dateFrom;
                DATE_TO.Value = dateTo;
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

                DATE_FROM.Value = dateFrom;
                DATE_TO.Value = dateTo;
            }
        }

        /// <summary>
        /// 検索実行処理
        /// </summary>
        /// <param name="sender">発生元オブジェクト</param>
        /// <param name="e">イベントデータ</param>
        internal virtual void SearchExec(object sender, EventArgs e)
        {
            //業者FromToがブランクの場合MIN、MAXをセットする。
            if (string.IsNullOrEmpty(this.GYOUSHA_CD_FROM.Text) && this.GYOUSHA_CD_FROM.Enabled)
            {
                this.GYOUSHA_CD_FROM.Text = this.logic.GetGyoushaCdMin();
                this.GYOUSHA_NAME_FROM.Text = this.logic.dba.GetGyoushaName(this.GYOUSHA_CD_FROM.Text);
            }
            if (string.IsNullOrEmpty(this.GYOUSHA_CD_TO.Text) && this.GYOUSHA_CD_TO.Enabled)
            {
                this.GYOUSHA_CD_TO.Text = this.logic.GetGyoushaCdMax();
                this.GYOUSHA_NAME_TO.Text = this.logic.dba.GetGyoushaName(this.GYOUSHA_CD_TO.Text);
            }
            //現場FromToがブランクの場合MIN、MAXをセットする。
            if (this.GYOUSHA_CD_FROM.Text == this.GYOUSHA_CD_TO.Text && !string.IsNullOrEmpty(this.GYOUSHA_CD_FROM.Text))
            {
                if (string.IsNullOrEmpty(this.GENBA_CD_FROM.Text) && this.GENBA_CD_FROM.Enabled)
                {
                    this.GENBA_CD_FROM.Text = this.logic.GetGenbaCdMin(this.GYOUSHA_CD_FROM.Text);
                    this.GENBA_NAME_FROM.Text = this.logic.dba.GetGenbaName(this.GYOUSHA_CD_FROM.Text, GENBA_CD_FROM.Text);
                }
                if (string.IsNullOrEmpty(this.GENBA_CD_TO.Text) && this.GENBA_CD_TO.Enabled)
                {
                    this.GENBA_CD_TO.Text = this.logic.GetGenbaCdMax(this.GYOUSHA_CD_FROM.Text);
                    this.GENBA_NAME_TO.Text = this.logic.dba.GetGenbaName(this.GYOUSHA_CD_FROM.Text, GENBA_CD_FROM.Text);
                }
            }
            //在庫品名FromToがブランクの場合MIN、MAXをセットする。
            if (string.IsNullOrEmpty(this.ZAIKO_HINMEI_CD_FROM.Text))
            {
                this.ZAIKO_HINMEI_CD_FROM.Text = this.logic.GetZaikoHinmeiCdMin();
                this.ZAIKO_HINMEI_NAME_FROM.Text = this.logic.dba.GetZaikoHinmeiName(this.ZAIKO_HINMEI_CD_FROM.Text);
            }
            if (string.IsNullOrEmpty(this.ZAIKO_HINMEI_CD_TO.Text))
            {
                this.ZAIKO_HINMEI_CD_TO.Text = this.logic.GetZaikoHinmeiCdMax();
                this.ZAIKO_HINMEI_NAME_FROM.Text = this.logic.dba.GetZaikoHinmeiName(this.ZAIKO_HINMEI_CD_TO.Text);
            }

            // 必須チェック
            this.isError = this.logic.RegistCheck();
            if (!this.isError)
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
            this.isError = false;

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
        #endregion "ボタン押下イベント"

        #region 業者
        /// <summary>
        /// 業者Toをダブルクリックで、FromからToへコードをコピーする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GyoushaToDoubleClick(object sender, MouseEventArgs e)
        {
            this.GYOUSHA_CD_TO.Text = this.GYOUSHA_CD_FROM.Text;
            this.GYOUSHA_NAME_TO.Text = this.GYOUSHA_NAME_FROM.Text;
        }

        private void GYOUSHA_CD_FROM_Validated(object sender, EventArgs e)
        {
            this.logic.GYOUSHA_CD_FROM_Validated();
        }

        private void GYOUSHA_CD_TO_Validated(object sender, EventArgs e)
        {
            this.logic.GYOUSHA_CD_TO_Validated();
        }

        private void GYOUSHA_CD_FROM_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                // 業者CDが入力エラーで無い場合、現在の入力値を変更前業者CDとする
                this.preGyoushaCd = this.GYOUSHA_CD_FROM.Text;
            }
        }

        private void GYOUSHA_CD_TO_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                // 業者CDが入力エラーで無い場合、現在の入力値を変更前業者CDとする
                this.preGyoushaCd = this.GYOUSHA_CD_TO.Text;
            }
        }

        /// <summary>
        /// ポップアップによる検索前処理(From側)
        /// </summary>
        public void gyoushaCdFromPopupBefore()
        {
            if (!this.isError)
            {
                // 業者CDが入力エラーで無い場合、現在の入力値を変更前業者CDとする
                this.preGyoushaCd = this.GYOUSHA_CD_FROM.Text;
            }
        }

        /// <summary>
        /// ポップアップによる検索後処理(From側)
        /// </summary>
        public void gyoushaCdFromPopupAfter()
        {
            this.logic.GYOUSHA_CD_FROM_Validated();
        }

        /// <summary>
        /// ポップアップによる検索前処理(To側)
        /// </summary>
        public void gyoushaCdToPopupBefore()
        {
            if (!this.isError)
            {
                // 業者CDが入力エラーで無い場合、現在の入力値を変更前業者CDとする
                this.preGyoushaCd = this.GYOUSHA_CD_TO.Text;
            }
        }

        /// <summary>
        /// ポップアップによる検索後処理(To側)
        /// </summary>
        public void gyoushaCdToPopupAfter()
        {
            this.logic.GYOUSHA_CD_TO_Validated();
        }
        #endregion

        #region 現場
        /// <summary>
        /// 現場Toをダブルクリックで、FromからToへコードをコピーする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaToDoubleClick(object sender, MouseEventArgs e)
        {
            this.GENBA_CD_TO.Text = this.GENBA_CD_FROM.Text;
            this.GENBA_NAME_TO.Text = this.GENBA_NAME_FROM.Text;
        }

        private void GENBA_CD_FROM_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                // 現場CDが入力エラーで無い場合、現在の入力値を変更前現場CDとする
                this.preGenbaCd = this.GENBA_CD_FROM.Text;
            }
        }

        private void GENBA_CD_TO_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                // 現場CDが入力エラーで無い場合、現在の入力値を変更前現場CDとする
                this.preGenbaCd = this.GENBA_CD_TO.Text;
            }
        }

        private void GENBA_CD_FROM_Validated(object sender, EventArgs e)
        {
            this.logic.GENBA_CD_FROM_Validated();
        }

        private void GENBA_CD_TO_Validated(object sender, EventArgs e)
        {
            this.logic.GENBA_CD_TO_Validated();
        }

        /// <summary>
        /// ポップアップによる検索前処理(From側)
        /// </summary>
        public void genbaCdFromPopupBefore()
        {
            if (!this.isError)
            {
                // 業者CDが入力エラーで無い場合、現在の入力値を変更前業者CDとする
                this.preGenbaCd = this.GENBA_CD_FROM.Text;
            }
        }

        /// <summary>
        /// ポップアップによる検索後処理(From側)
        /// </summary>
        public void genbaCdFromPopupAfter()
        {
            this.logic.GENBA_CD_FROM_Validated();
        }

        /// <summary>
        /// ポップアップによる検索前処理(To側)
        /// </summary>
        public void genbaCdToPopupBefore()
        {
            if (!this.isError)
            {
                // 業者CDが入力エラーで無い場合、現在の入力値を変更前業者CDとする
                this.preGenbaCd = this.GENBA_CD_TO.Text;
            }
        }

        /// <summary>
        /// ポップアップによる検索後処理(To側)
        /// </summary>
        public void genbaCdToPopupAfter()
        {
            this.logic.GENBA_CD_TO_Validated();
        }
        #endregion

        #region 在庫品名
        /// <summary>
        /// 在庫品名Toをダブルクリックで、FromからToへコードをコピーする。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ZaikoHinmeiToDoubleClick(object sender, MouseEventArgs e)
        {
            this.ZAIKO_HINMEI_CD_TO.Text = this.ZAIKO_HINMEI_CD_FROM.Text;
            this.ZAIKO_HINMEI_NAME_TO.Text = this.ZAIKO_HINMEI_NAME_FROM.Text;
        }

        private void ZAIKO_HINMEI_CD_FROM_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                // 現場CDが入力エラーで無い場合、現在の入力値を変更前現場CDとする
                this.preZaikoHinmeiCd = this.ZAIKO_HINMEI_CD_FROM.Text;
            }
        }

        private void ZAIKO_HINMEI_CD_TO_Enter(object sender, EventArgs e)
        {
            if (!this.isError)
            {
                // 現場CDが入力エラーで無い場合、現在の入力値を変更前現場CDとする
                this.preZaikoHinmeiCd = this.ZAIKO_HINMEI_CD_TO.Text;
            }
        }

        private void ZAIKO_HINMEI_CD_FROM_Validated(object sender, EventArgs e)
        {
            this.logic.ZAIKO_HINMEI_CD_FROM_Validated();
        }

        private void ZAIKO_HINMEI_CD_TO_Validated(object sender, EventArgs e)
        {
            this.logic.ZAIKO_HINMEI_CD_TO_Validated();
        }


        /// <summary>
        /// ポップアップによる検索前処理(From側)
        /// </summary>
        public void zaikoHinmeiCdFromPopupBefore()
        {
            if (!this.isError)
            {
                // 業者CDが入力エラーで無い場合、現在の入力値を変更前業者CDとする
                this.preZaikoHinmeiCd = this.ZAIKO_HINMEI_CD_FROM.Text;
            }
        }

        /// <summary>
        /// ポップアップによる検索後処理(From側)
        /// </summary>
        public void zaikoHinmeiCdFromPopupAfter()
        {
            this.logic.ZAIKO_HINMEI_CD_FROM_Validated();
        }

        /// <summary>
        /// ポップアップによる検索前処理(To側)
        /// </summary>
        public void zaikoHinmeiCdToPopupBefore()
        {
            if (!this.isError)
            {
                // 業者CDが入力エラーで無い場合、現在の入力値を変更前業者CDとする
                this.preZaikoHinmeiCd = this.ZAIKO_HINMEI_CD_TO.Text;
            }
        }

        /// <summary>
        /// ポップアップによる検索後処理(To側)
        /// </summary>
        public void zaikoHinmeiCdToPopupAfter()
        {
            this.logic.ZAIKO_HINMEI_CD_TO_Validated();
        }
        #endregion

        private void rdobtn_gyoushaGenba_CheckedChanged(object sender, EventArgs e)
        {
            if (rdobtn_gyoushaGenba.Checked)
            {
                this.GYOUSHA_CD_FROM.Enabled = true;
                this.GYOUSHA_CD_TO.Enabled = true;
                this.GYOUSHA_POPUP_FROM.Enabled = true;
                this.GYOUSHA_POPUP_TO.Enabled = true;
            }
        }

        private void rdobtn_zaikoHinmei_CheckedChanged(object sender, EventArgs e)
        {
            if (rdobtn_zaikoHinmei.Checked)
            {
                this.GYOUSHA_CD_FROM.Enabled = false;
                this.GYOUSHA_CD_TO.Enabled = false;
                this.GENBA_CD_FROM.Enabled = false;
                this.GENBA_CD_TO.Enabled = false;
                this.GYOUSHA_CD_FROM.Text = "";
                this.GYOUSHA_NAME_FROM.Text = "";
                this.GYOUSHA_CD_TO.Text = "";
                this.GYOUSHA_NAME_TO.Text = "";
                this.GENBA_CD_FROM.Text = "";
                this.GENBA_NAME_FROM.Text = "";
                this.GENBA_CD_TO.Text = "";
                this.GENBA_NAME_TO.Text = "";
                this.GYOUSHA_POPUP_FROM.Enabled = false;
                this.GYOUSHA_POPUP_TO.Enabled = false;
                this.GENBA_POPUP_FROM.Enabled = false;
                this.GENBA_POPUP_TO.Enabled = false;
            }
        }
    }
}
