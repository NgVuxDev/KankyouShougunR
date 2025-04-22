using System.Data;
using System.Linq;
using System.Windows.Forms;
using MasterKyoutsuPopup2.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Utility;
using r_framework.Logic;
namespace MasterKyoutsuPopup2.APP
{
    public partial class MasterKyoutsuPopupForm : SuperPopupForm
    {

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        public MasterKyoutsuPopupLogic logic;
        internal MessageBoxShowLogic errmessage = new MessageBoxShowLogic();

        public string sql { get; set; }

        public MasterKyoutsuPopupForm()
        {
            InitializeComponent();
            this.customDataGridView1.IsBrowsePurpose = true;
        }

        protected override void OnLoad(System.EventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            base.OnLoad(e);

            this.logic = new MasterKyoutsuPopupLogic(this);

            bool catchErr = this.logic.WindowInit();
            if (catchErr)
            {
                return;
            }

            //FW_QA158_F12を押すと、ポップアップと一緒に呼び出し元まで閉じる。
            this.customDataGridView1.KeyDown += this.DetailKeyDown; //行選択のEnterがKeyUpだけでやるとフォーカスがした移動してから選択されるため、ガードが必要

            var displayColumnName = (this.PopupGetMasterField ?? "").Split(',').Select(s => s.Trim().ToUpper());
            if (this.table != null && displayColumnName.Count() > 0)
            {
                if (!string.IsNullOrWhiteSpace(this.table.TableName))
                {
                    this.Text = this.table.TableName;
                    this.lb_title.Text = this.table.TableName;
                }
                var removeColumnsName = this.table.Columns.OfType<DataColumn>().Where(s => !displayColumnName.Contains(s.ColumnName)).Select(s => s.ColumnName).ToList();
                removeColumnsName.ForEach(s => this.table.Columns.Remove(s));

                this.logic.DataGridViewLoad(this.table, this.PopupDataHeaderTitle);
            }
            else
            {
                 catchErr = this.logic.MasterSearch();
                if (catchErr)
                {
                    return;
                }
            }

            // 表示を調整
            catchErr = this.logic.CordinateGridSize();
            if (catchErr)
            {
                return;
            }
            this.bt_func12.Click += this.FormClose;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        internal void FormClose(object sender, System.EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        /// <summary>
        /// ダブルクリック時処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }
            bool catchErr = this.logic.ElementDecision();
            if (catchErr)
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        //FW_QA158_F12を押すと、ポップアップと一緒に呼び出し元まで閉じる。
        //KeyDownをKeyUpに変更
        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DetailKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            //グリッドでのEnterはKeyDownでカーソルが下移動するするため、Downで先にハンドルする必要がある
            if (e.KeyCode == Keys.Enter)
            {
                bool catchErr = this.logic.ElementDecision();
                if (catchErr)
                {
                    return;
                }
                e.Handled = true;
                this.DialogResult = DialogResult.OK;
            }
        }

        //ST13 ラベルが見切れる

        /// <summary>
        /// ラベルタイトルの横幅最大値
        /// </summary>
        /// <remarks>
        /// レイアウトに変更があった場合、下記値を再設定する必要有
        /// </remarks>
        private readonly int TitleMaxWidth = 400;

        /// <summary>
        /// ラベルタイトルテキストチェンジ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lb_title_TextChanged(object sender, System.EventArgs e)
        {
            ControlUtility.AdjustTitleSize(lb_title, this.TitleMaxWidth);
        }

        #region 絞込みイベント
        /// <summary>
        /// 絞込み処理
        /// </summary>
        internal void Filter(object sender, System.EventArgs e)
        {
            this.logic.Filter();
        }
        #endregion

        #region 条件入力イベント
        /// <summary>
        /// 条件入力処理
        /// </summary>
        internal void InputCondition(object sender, System.EventArgs e)
        {
            this.CONDITION_TEXT.Focus();
        }
        #endregion
    }
}
