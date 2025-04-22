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
using r_framework.Authority;

namespace Shougun.Core.Adjustment.Shiharaicheckhyo
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

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="userKyotenCd">拠点コード</param>
        /// <param name="headerForm">ヘッダフォーム</param>
        /// <param name="windowType">画面タイプ</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE windowType)
            : base(WINDOW_ID.T_SHIHARAI_CHECK, windowType)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            this.headerForm = headerForm;
            this.logic.setHeaderForm(headerForm);

            checkBoxAll.Visible = true;
            checkBoxAll.SendToBack();

        }
        #endregion コンストラクタ

        #region 初期処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント引数</param>
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
            String kyotenName = this.logic.SelectKyotenNameRyaku(txtKyotenCD.Text, out catchErr);
            if (!catchErr)
            {
                return;
            }
            txtKyotenName.Text = kyotenName;

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }
        }
        #endregion 初期処理

        #region ボタン押下イベント
        /// <summary>
        /// 検索期間算出処理(前月)
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function3Click(object sender, EventArgs e)
        {
            object dateFrom = null;
            object dateTo = null;

            if (!this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, false))
            {
                return;
            }

            dtpSearchDateFrom.Value = dateFrom;
            dtpSearchDateTo.Value = dateTo;

        }

        /// <summary>
        /// 検索期間算出処理(翌月)
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function4Click(object sender, EventArgs e)
        {
            object dateFrom = null;
            object dateTo = null;

            if (!this.logic.SetDatePreviousMonth(out dateFrom, out dateTo, true))
            {
                return;
            }

            dtpSearchDateFrom.Value = dateFrom;
            dtpSearchDateTo.Value = dateTo;
        }

        /// <summary>
        /// CSV処理
        /// </summary>
        public void Function5Click(object sender, EventArgs e)
        {
            this.logic.Function5ClickLogic();
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function8Click(object sender, EventArgs e)
        {
            this.logic.Function8ClickLogic();
        }

        /// <summary>
        /// 印刷処理
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function6Click(object sender, EventArgs e)
        {
            this.logic.Function6ClickLogic();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void Function9Click(object sender, EventArgs e)
        {
            this.logic.Function9ClickLogic();
        }
        #endregion ボタン押下イベント

        #region グリッド締列チェックボックスイベント
        /// <summary>
        /// 列ヘッダーにチェックボックスを表示
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void Dgv_TorihikisakiIchiran_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            this.logic.DgvTorihikisakiIchiranCellPaintingLogic(e);
        }

        /// <summary>
        /// 列ヘッダーのチェックボックスを押したときに、すべて選択用のチェックボックス状態を切り替え
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void Dgv_TorihikisakiIchiran_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            this.logic.DgvTorihikisakiIchiranCellClickLogic(e);

        }

        /// <summary>
        /// すべての行のチェック状態を切り替える
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void CheckBoxAll_CheckedChanged(object sender, EventArgs e)
        {
            this.logic.CheckBoxAllCheckedChangedLogic();

        }
        #endregion グリッド締列チェックボックスイベント

        #region 締日コンボボックスイベント
        /// <summary>
        /// 締日コンボボックス変更
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void cmbShimebi_SelectedValueChanged(object sender, EventArgs e)
        {
            this.logic.TxtShimeDateValidatedLogic();
        }
        #endregion 締日コンボボックスイベント

        #region 検索期間FROMロストフォーカス
        /// <summary>
        /// 検索期間FROMロストフォーカス
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void dtp_SearchDateFrom_Validated(object sender, EventArgs e)
        {
            this.logic.dtpSearchDateFromValidatedLogic();
        }
        #endregion 検索期間FROMロストフォーカス

        #region 検索期間TOロストフォーカス
        /// <summary>
        /// 検索期間FROMロストフォーカス
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        private void dtp_SearchDateTo_Validated(object sender, EventArgs e)
        {
            this.logic.dtpSearchDateToValidatedLogic();
        }
        #endregion 検索期間TOロストフォーカス

        #region 終了処理
        /// <summary>
        /// Formクローズ処理
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void FormClose(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }
        #endregion 終了処理

        /// 20141023 Houkakou 「支払チェック表」の日付チェックを追加する　start
        private void dtpSearchDateFrom_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.dtpSearchDateTo.Text))
            {
                this.dtpSearchDateTo.IsInputErrorOccured = false;
                this.dtpSearchDateTo.BackColor = Constans.NOMAL_COLOR;
            }
        }

        private void dtpSearchDateTo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.dtpSearchDateFrom.Text))
            {
                this.dtpSearchDateFrom.IsInputErrorOccured = false;
                this.dtpSearchDateFrom.BackColor = Constans.NOMAL_COLOR;
            }
        }
        /// 20141023 Houkakou 「支払チェック表」の日付チェックを追加する　end
    }
}
