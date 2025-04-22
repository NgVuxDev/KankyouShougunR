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
using Seasar.Quill;
using r_framework.Authority;

namespace Shougun.Core.Billing.Seikyucheckhyo
{
    public partial class UIForm : SuperForm
    {
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass logic;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        private UIHeader headerForm;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        #region "コンストラクタ"
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="windowType">画面タイプ</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType)
            : base(WINDOW_ID.T_SEIKYU_CHECK, windowType)
        {
            this.InitializeComponent();

            //日付の初期値を今日の日付に設定(デザイナで設定すると自動で書き換えられるため)
            dtp_SearchDateFrom.Value = DateTime.Now;
            dtp_SearchDateTo.Value = DateTime.Now;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.headerForm = headerForm;
            this.logic.setHeaderForm(headerForm);

            checkBoxAll.Visible = true;
            checkBoxAll.SendToBack();
        }
        #endregion "コンストラクタ"

        #region "初期処理"
        /// <summary>
        /// 画面Load処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!this.logic.WindowInit())
            {
                return;
            }

            //取引先データ取得
            if (!this.logic.GetTorihikisaki(null))
            {
                return;
            }
            //取引先データグリッド設定
            if (!this.logic.SetTorihikisaki())
            {
                return;
            }

            //拠点CDから拠点名略称を取得
            bool catchErr = true;
            String kyotenName = this.logic.SelectKyotenNameRyaku(txt_KyotenCD.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            txt_KyotenName.Text = kyotenName;

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }
        #endregion "初期処理"

        #region "ボタン押下イベント"
        /// <summary>
        /// 検索期間算出処理(前月)
        /// </summary>
        public void Function3Click(object sender, EventArgs e)
        {
            object dateFrom = null;
            object dateTo = null;

            if (!this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, false))
            {
                return;
            }

            dtp_SearchDateFrom.Value = dateFrom;
            dtp_SearchDateTo.Value = dateTo;

        }

        /// <summary>
        /// 検索期間算出処理(翌月)
        /// </summary>
        public void Function4Click(object sender, EventArgs e)
        {
            object dateFrom = null;
            object dateTo = null;

            if (!this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, true))
            {
                return;
            }

            dtp_SearchDateFrom.Value = dateFrom;
            dtp_SearchDateTo.Value = dateTo;
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public void Function8Click(object sender, EventArgs e)
        {
            this.logic.Function8ClickLogic();
        }

        /// <summary>
        /// CSV処理
        /// </summary>
        public void Function5Click(object sender, EventArgs e)
        {
            this.logic.Function5ClickLogic();
        }

        /// <summary>
        /// 印刷処理
        /// </summary>
        public void Function6Click(object sender, EventArgs e)
        {
            this.logic.Function6ClickLogic();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        public void Function9Click(object sender, EventArgs e)
        {
            this.logic.Function9ClickLogic();
        }
        #endregion "ボタン押下イベント"

        #region "グリッド締列チェックボックスイベント"
        /// <summary>
        /// 列ヘッダーにチェックボックスを表示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_TorihikisakiIchiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            this.logic.DgvTorihikisakiIchiranCellPaintingLogic(e);
        }

        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Dgv_TorihikisakiIchiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.DgvTorihikisakiIchiranCellClickLogic(e);

        }

        /// <summary>
        /// すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.CheckBoxAllCheckedChangedLogic();

        }
        #endregion "グリッド締列チェックボックスイベント"

        #region "締日コンボボックスイベント"
        /// <summary>
        /// 締日コンボボックス変更
        /// </summary>
        private void cb_shimebi_SelectedValueChanged(object sender, EventArgs e)
        {
            this.logic.TxtShimeDateValidatedLogic();
        }

        #endregion "締日コンボボックスイベント"

        #region "検索期間FROMロストフォーカス"
        private void dtp_SearchDateFrom_Validated(object sender, EventArgs e)
        {
            //this.logic.dtpSearchDateFromValidatedLogic();
            if (!string.IsNullOrEmpty(this.dtp_SearchDateTo.Text))
            {
                this.dtp_SearchDateTo.IsInputErrorOccured = false;
                this.dtp_SearchDateTo.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion "検索期間FROMロストフォーカス"

        #region "検索期間TOロストフォーカス"
        private void dtp_SearchDateTo_Validated(object sender, EventArgs e)
        {
            //this.logic.dtpSearchDateToValidatedLogic();
            if (!string.IsNullOrEmpty(this.dtp_SearchDateFrom.Text))
            {
                this.dtp_SearchDateFrom.IsInputErrorOccured = false;
                this.dtp_SearchDateFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion "検索期間TOロストフォーカス"

        #region "終了処理"
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }
        #endregion "終了処理"




    }
}
