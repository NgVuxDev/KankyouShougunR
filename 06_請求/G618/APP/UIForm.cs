using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Utility;
using Seasar.Quill;

namespace Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku
{
    public partial class UIForm : SuperForm
    {
        #region Field

        /// <summary>ヘッダーフォーム</summary>
        private UIHeader headerForm;

        /// <summary>ロジッククラス</summary>
        private LogicClass logic;

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        /// <param name="type">画面区分</param>
        /// <param name="dt">月次年月</param>
        /// <param name="windowId">画面ID</param>
        public UIForm(UIHeader headerForm, WINDOW_TYPE type, DateTime dt, WINDOW_ID windowId)
            : base(windowId, type)
        {
            InitializeComponent();

            this.logic = new LogicClass(headerForm, this);
            this.logic.GetsujiYM = dt;
            this.headerForm = headerForm;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #endregion

        #region Event

        /// <summary>
        /// 画面Loadイベント
        /// </summary>
        /// <param name="e">EventArgs</param>
        protected override void OnLoad(EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            this.logic.WindowInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一覧CellEnter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DETAIL_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var colunName = this.DETAIL.Columns[e.ColumnIndex].Name;

            // IME制御
            switch (colunName)
            {
                case "ADJUST_TAX":
                    this.DETAIL.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
                default:
                    this.DETAIL.ImeMode = System.Windows.Forms.ImeMode.Disable;
                    break;
            }
        }

        /// <summary>
        /// 一覧CellValidated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DETAIL_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            CustomDataGridView dgv = sender as CustomDataGridView;
            string colName = dgv.Columns[e.ColumnIndex].Name;

            if ("ADJUST_TAX" == colName)
            {
                // 調整前差引残高 + 消費税調整額 = 調整後差引残高 の計算

                // 消費税調整額の取得
                var adjustTax = dgv.Rows[e.RowIndex].Cells["ADJUST_TAX"].Value;

                // 調整前差引残高の取得
                var lockZandaka = dgv.Rows[e.RowIndex].Cells["LOCK_ZANDAKA"].Value;

                if (adjustTax == null || lockZandaka == null)
                {
                    return;
                }

                if (string.IsNullOrEmpty(adjustTax.ToString()))
                {
                    adjustTax = 0;

                    // 空欄時は0で初期化
                    dgv.Rows[e.RowIndex].Cells["ADJUST_TAX"].Value = 0m;
                }

                if (string.IsNullOrEmpty(lockZandaka.ToString()))
                {
                    lockZandaka = 0;
                }

                // 調整後差引残高の計算
                bool catchErr = true;
                dgv.Rows[e.RowIndex].Cells["ZANDAKA"].Value = logic.CalculateZandaka(lockZandaka, adjustTax, out catchErr);
            }
        }

        /// <summary>
        /// 検索用取引先CD_Validated
        /// 一覧の該当行にフォーカスを当てます
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SEARCH_TORIHIKISAKI_CD_Validated(object sender, EventArgs e)
        {
            this.logic.SearchIchiranTorihikisakiRow();
        }

        /// <summary>
        /// 検索用取引先ボタン_PopupAfterExcuteMethod
        /// 一覧の該当行にフォーカスを当てます
        /// </summary>
        public void btnSearchTorihikisaki_PopupAfterExcuteMethod()
        {
            this.logic.SearchIchiranTorihikisakiRow();
        }

        #endregion
    }
}
