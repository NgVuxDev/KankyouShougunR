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
using r_framework.CustomControl;

namespace Shougun.Core.ReceiptPayManagement.ShukkinKeshikomiShusei
{
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>親フォーム</summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>画面ロジック</summary>
        private LogicClass logic;

        /// <summary>出金番号</summary>
        public string shukkinNumber;

        /// <summary>消込額</summary>
        public decimal keshikomiGakuWk = 0;

        public string SeisanDateto;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm(WINDOW_TYPE windowType, String SEISAN_DATE_TO, String TORIHIKISAKI_CD, String TORIHIKISAKI_NAME, String SHUKKIN_NUMBER)
            : base(WINDOW_ID.T_SYUKKIN_KESHIKOMI_SHUSEI, WINDOW_TYPE.NONE)
        {
            this.InitializeComponent();
            this.WindowType = windowType;
            this.logic = new LogicClass(this);
            this.SeisanDateto = SEISAN_DATE_TO;
            this.TORIHIKISAKI_CD.Text = TORIHIKISAKI_CD;
            this.TORIHIKISAKI_NAME.Text = TORIHIKISAKI_NAME;

            if (!string.IsNullOrWhiteSpace(SHUKKIN_NUMBER))
            {
                this.shukkinNumber = SHUKKIN_NUMBER;
            }
        }
        #endregion

        #region 初期化処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            this.logic.WindowInit();
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            // Anchorの設定は必ずOnShownで行うこと
            this.dgvShukkinDeleteMeisai.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
        }
        #endregion

        #region ファンクションイベント

        #region 検索
        /// <summary>
        /// 検索を実行する。
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void bt_func8_Click(object sender, EventArgs e)
        {
            this.logic.IchiranSearch();
        }
        #endregion

        #region 実行
        /// <summary>
        /// 出金消込データを登録する。
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void bt_func9_Click(object sender, EventArgs e)
        {
            this.logic.Function9ClickLogic();
        }
        #endregion

        #region 閉じる
        /// <summary>
        /// 画面を閉じる。
        /// </summary>
        /// <param name="sender">オブジェクト情報</param>
        /// <param name="e">イベント引数</param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;
            this.Close();
            parentForm.Close();
        }
        #endregion

        #endregion

        #region コントロールイベント

        #region CellValidating
        /// <summary>
        /// 値検証イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvShukkinDeleteMeisai_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var dgvTextBoxCell = this.dgvShukkinDeleteMeisai.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell;
            if (dgvTextBoxCell != null)
            {
                switch (dgvTextBoxCell.Name)
                {
                    case "KeshikomiGaku":
                        dgvTextBoxCell.IsInputErrorOccured = false;
                        if (this.logic.IsInvalidKeshikomiGaku(e.RowIndex, e.ColumnIndex))
                        {
                            dgvTextBoxCell.IsInputErrorOccured = true;
                            e.Cancel = true;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion

        private void dgvShukkinDeleteMeisai_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            var dgvTextBoxCell = this.dgvShukkinDeleteMeisai.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell;
            if (dgvTextBoxCell != null)
            {
                switch (dgvTextBoxCell.Name)
                {
                    case "KeshikomiGaku":
                        decimal.TryParse(Convert.ToString(this.dgvShukkinDeleteMeisai.Rows[e.RowIndex].Cells[e.ColumnIndex].Value), out this.keshikomiGakuWk);
                        break;

                    default:
                        break;
                }
            }
        }
        #region EditingControlShowin
        /// <summary>
        /// コントロール編集時イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvShukkinDeleteMeisai_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            //表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                CustomDataGridView dgv = (CustomDataGridView)sender;

                //編集のために表示されているコントロールを取得
                TextBox tb = (TextBox)e.Control;

                //列によってIMEのモードを変更する
                if (dgv.CurrentCell.OwningColumn.Name == "KeshikomiGaku")
                {
                    tb.ImeMode = ImeMode.Disable;
                }
                else if (dgv.CurrentCell.OwningColumn.Name == "KESHIKOMI_BIKOU")
                {
                    tb.ImeMode = ImeMode.Hiragana;
                }
            }
        }
        #endregion

        #region 検証後イベント
        /// <summary>
        /// 検証後イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvShukkinDeleteMeisai_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            //this.logic.CalculateKingakuForKeshikomiMeisai(null);
            var dgvTextBoxCell = this.dgvShukkinDeleteMeisai.Rows[e.RowIndex].Cells[e.ColumnIndex] as DgvCustomTextBoxCell;
            if (dgvTextBoxCell != null)
            {
                switch (dgvTextBoxCell.Name)
                {
                    case "KeshikomiGaku":
                        this.logic.CalculateKingakuForKeshikomiMeisai(e.RowIndex, e.ColumnIndex);
                        break;

                    default:
                        break;
                }
            }
        }
        #endregion

        #region Validatedイベント
        /// <summary>
        /// 取引先Validating処理
        /// </summary
        internal void TORIHIKISAKI_CD_Validating(object sender, CancelEventArgs e)
        {
            this.logic.TORIHIKISAKI_CD_Validating(sender, e);
        }

        private void SEISAN_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.SEISAN_DATE_TO.Text = this.SEISAN_DATE_FROM.Text;
        }

        
        #endregion

        #endregion
    }
}
