// $Id: UIForm.cs 33171 2014-10-23 09:28:33Z fangjk@oec-h.com $
using System;
using System.ComponentModel;
using System.Windows.Forms;
using r_framework.APP.Base;
using Shougun.Core.SalesManagement.MotochoHaniJokenPopUp.Const;
using Shougun.Core.SalesManagement.MotochoHaniJokenPopUp.Logic;
using System.Collections.ObjectModel;
using r_framework.Dto;
using r_framework.Const;

namespace Shougun.Core.SalesManagement.MotochoHaniJokenPopUp.APP
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
			this.TORIHIKISAKI_CD_START.Focus();
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
			this.TORIHIKISAKI_CD_END.Focus();
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
			if((this.Error == true) || (this.FocusOutErrorFlag == true))
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

                if (!this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, false))
                {
                    return;
                }

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

                if (!this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, true))
                {
                    return;
                }

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
            /// 20141023 Houkakou 日付チェックを追加する　start
            if (this.logic.DateCheck())
            {
                this.Error = true;
                return;
            }
            /// 20141023 Houkakou 日付チェックを追加する　end
            
			// 整合性チェックを走らせるためにSetFocusを強制発行
			this.bt_func8.Focus();
			
			// 登録時チェック
			this.Error = this.logic.RegistCheck();
			if((this.Error == false) && (this.FocusOutErrorFlag == false))
			{
				// 設定条件を保存
                if (!this.logic.SaveParams())
                {
                    return;
                }

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

		#endregion "ボタン押下イベント"

		/// <summary>
		/// 元帳種類のテキストチェンジイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MOTOTYOU_KBN_TextChanged(object sender, EventArgs e)
		{
			// 元帳種類が、2.現金元帳の場合  
			if (this.TORIHIKI_KBN.Text == "2")
			{
				//締日をブランクにして当月の月初月末を設定する
                this.cb_shimebi.Text = "";
                // 20150902 katen #12048 「システム日付」の基準作成、適用 start
                //DateTime today = DateTime.Today;
                DateTime today = this.sysDate;
                // 20150902 katen #12048 「システム日付」の基準作成、適用 end
				DateTime firstDay = today.AddDays(-today.Day + 1);
				DateTime endDay = firstDay.AddMonths(1).AddDays(-1);
				this.DATE_HANI_START.Value = firstDay;
				this.DATE_HANI_END.Value = endDay;

				// 抽出方法を 1.伝票日付：月初／月末で固定する
				this.URIAGE_DATE_SHIMEBI.Enabled = false;
				this.TYUUSYUTU_KBN.Text = "1";
				this.TYUUSYUTU_KBN.Enabled = false;
                this.cb_shimebi.Enabled = false;

            }
			else
			{
				// 抽出方法の 「1.伝票日付：月初／月末で固定」を解除
				this.URIAGE_DATE_SHIMEBI.Enabled = true;
				this.TYUUSYUTU_KBN.Enabled = true;
                this.cb_shimebi.Enabled = true;
            }
		}

		#region "締日テキストボックスイベント"
		/// <summary>
		/// 締日テキストボックス変更
		/// </summary>
		private void cb_shimebi_SelectedIndexChanged(object sender, EventArgs e)
		{
            // 締日コンボボックス変更処理
            this.logic.CmbShimeDateValidatedLogic();
        }
		#endregion "締日テキストボックスイベント"

		/// <summary>
		/// 抽出方法のテキストチェンジイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TYUUSYUTU_KBN_TextChanged(object sender, EventArgs e)
		{
			// 抽出方法が、1.伝票日付:月初／月末の場合
			if (this.TYUUSYUTU_KBN.Text == "1")
			{
				// 締日をブランクにして当月の月初月末を設定する
                this.cb_shimebi.Text = "";
                // 20150902 katen #12048 「システム日付」の基準作成、適用 start
                //DateTime today = DateTime.Today;
                DateTime today = this.sysDate;
                // 20150902 katen #12048 「システム日付」の基準作成、適用 end
				DateTime firstDay = today.AddDays(-today.Day + 1);
				DateTime endDay = firstDay.AddMonths(1).AddDays(-1);
				this.DATE_HANI_START.Value = firstDay;
				this.DATE_HANI_END.Value = endDay;

				// 締日を非活性にする
				this.cb_shimebi.Enabled = false;

				// 締日の必須チェックをはずす
				this.cb_shimebi.RegistCheckMethod = new Collection<SelectCheckDto>();
				this.SHIMEBI_LABEL.Text = "締日";

                // 締日を活性にする
                this.cb_shimebi.Enabled = true;
                this.cb_shimebi.Text = "";

                this.DATE_HANI_START.Enabled = true;
                this.DATE_HANI_END.Enabled = true;

                if (this.TORIHIKI_KBN.Text == "2")
                {
                    this.cb_shimebi.Enabled = false;
                    this.cb_shimebi.Text = "";
                }
            }
            else if (this.TYUUSYUTU_KBN.Text == "2")
            {
                // 締日を活性にする
                this.cb_shimebi.Enabled = true;

                // 当日日付の直前の締日に設定
                // 当日日付
                // 20150902 katen #12048 「システム日付」の基準作成、適用 start
                //var toDay = DateTime.Today;
                var toDay = this.sysDate;
                // 20150902 katen #12048 「システム日付」の基準作成、適用 end
                var toDayDate = toDay.Day;

                // 当月月末日
                var endOfTheMonth = new DateTime(toDay.Year, toDay.Month, DateTime.DaysInMonth(toDay.Year, toDay.Month));

                // 締日を活性にする
                this.cb_shimebi.Enabled = true;
                this.cb_shimebi.Text = "";

                DateTime firstDay = toDay.AddDays(-toDay.Day + 1);
                DateTime endDay = firstDay.AddMonths(1).AddDays(-1);
                this.DATE_HANI_START.Value = firstDay;
                this.DATE_HANI_END.Value = endDay;
                this.DATE_HANI_START.Enabled = true;
                this.DATE_HANI_END.Enabled = true;

                this.SHIMEBI_LABEL.Text = "締日";
            }
		}

		/// <summary>
		/// 取引先STARTのValidating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TORIHIKISAKI_CD_START_Validating(object sender, CancelEventArgs e)
		{
			var torihikisakiCd = this.TORIHIKISAKI_CD_START.Text;
			var shimebi = this.cb_shimebi.Text;

			// 取引先と拠点の関係をチェック
            if (false == this.logic.CheckTorihikisakiShimebi(shimebi, torihikisakiCd, "1"))
            {
                this.TORIHIKISAKI_NAME_START.Text = string.Empty;

                this.TORIHIKISAKI_CD_START.Focus();
            }
		}

		/// <summary>
		/// 取引先ENDのValidating
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TORIHIKISAKI_CD_END_Validating(object sender, CancelEventArgs e)
		{
			var torihikisakiCd = this.TORIHIKISAKI_CD_END.Text;
			var shimebi = this.cb_shimebi.Text;

			// 取引先と拠点の関係をチェック
			if (false == this.logic.CheckTorihikisakiShimebi(shimebi, torihikisakiCd, "2"))
			{
				this.TORIHIKISAKI_NAME_END.Text = string.Empty;

				this.TORIHIKISAKI_CD_END.Focus();
			}
		}

		/// <summary>
		/// 取引先CD_END ダブルクリックイベント
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TORIHIKISAKI_CD_END_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			// 「取引先CD STRAT」の情報を「取引先CD END」へコピーします。
			this.TORIHIKISAKI_CD_END.Text = this.TORIHIKISAKI_CD_START.Text;
			this.TORIHIKISAKI_NAME_END.Text = this.TORIHIKISAKI_NAME_START.Text;
		}

        /// 20141023 Houkakou 日付チェックを追加する　start
        private void DATE_HANI_START_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DATE_HANI_END.Text))
            {
                this.DATE_HANI_END.IsInputErrorOccured = false;
                this.DATE_HANI_END.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void DATE_HANI_END_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.DATE_HANI_START.Text))
            {
                this.DATE_HANI_START.IsInputErrorOccured = false;
                this.DATE_HANI_START.BackColor = Constans.NOMAL_COLOR;
            }
        }

        /// 20141023 Houkakou 日付チェックを追加する　end
	}
}
