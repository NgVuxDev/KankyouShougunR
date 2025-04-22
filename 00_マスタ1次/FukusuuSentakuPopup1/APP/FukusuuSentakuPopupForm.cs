using System.Windows.Forms;
using FukusuuSentakuPopup1.Logic;
using r_framework.APP.PopUp.Base;
using r_framework.Utility;
using r_framework.Const;
using r_framework.Logic;
using System;

namespace FukusuuSentakuPopup1.APP
{
    /// <summary>
    /// 複数選択ポップアップ
    /// </summary>
    public partial class FukusuuSentakuPopupForm : SuperPopupForm
    {
        /// <summary>
        /// ロジッククラス
        /// </summary>
        internal FukusuuSentakuPopupLogic logic;

        /// <summary>
        /// SQL
        /// </summary>
        internal string sql { get; set; }

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FukusuuSentakuPopupForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// オンロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            logic = new FukusuuSentakuPopupLogic(this);
            this.logic.WindowInit();
            this.logic.MasterSearch();
            this.logic.SetCheck();
        }

        /// <summary>
        /// Formクローズ処理
        /// </summary>
        internal virtual void FormClose(object sender, System.EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 選択処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void DataSelection(object sender, System.EventArgs e)
        {
            if (this.WindowId == WINDOW_ID.M_HOUKOKUSHO_BUNRUI_UNPAN)
            {
                var check = false;
                var tsumikae = false;
                foreach (var row in this.masterDetail.Rows)
                {
                    var cell = row.Cells["CHECKED"];
                    var cellTsumikae = row.Cells["TSUMIKAE"];
                    check = false;
                    tsumikae = false;
                    bool.TryParse(Convert.ToString(cellTsumikae.Value), out tsumikae);
                    if (cell.Value == null)
                    {
                        check = false;
                    }
                    else
                    {
                        check = (bool)cell.Value;
                    }
                    if (!check && tsumikae)
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E219");
                        return;
                    }
                }
            }
            this.logic.ElementDecision();
            //if (this.WindowId == WINDOW_ID.M_HOUKOKUSHO_BUNRUI_UNPAN)
            //{
            //    this.logic.UpdateData();
            //}
        }

        /// <summary>
        /// キー押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DetailKeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.logic.ElementDecision();
            }
            else if (e.KeyCode == Keys.F1)
            {
                this.logic.ElementDecision();
            }
            else if (e.KeyCode == Keys.F12)
            {
                base.ReturnParams = null;
                this.Close();
            }
        }

        /// <summary>
        /// フォームのクロージングイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FukusuuSentakuPopupForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.logic.isReturnValueInit)
            {
                base.ReturnParams = null;
            }
        }
    }
}
