using System;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.FormManager;

namespace Shougun.Core.SalesPayment.HannyushutsuIchiran
{
    /// <summary>
    /// Formクラス
    /// </summary>
    public partial class HannyushutsuIchiranForm : SuperForm
    {
        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// ビジネスロジッククラス
        /// </summary>
        private HannyushutsuIchiranLogicClass logic;

        /// <summary>
        /// UIHeader.cs
        /// </summary>
        private HannyushutsuIchiranHeader headerForm;

        /// <summary>
        /// 伝種区分
        /// </summary>
        internal DENSHU_KBN denshuKbn;

        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 前回業者CD
        /// </summary>
        string ZengyousyaCD = string.Empty;

        /// <summary>
        /// 前回業者CD
        /// </summary>
        string ZenNiorosigyousyaCD = string.Empty;
        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        /// <param name="windowType"></param>
        /// <param name="denshuKbn"></param>
        public HannyushutsuIchiranForm(HannyushutsuIchiranHeader headerForm, WINDOW_TYPE windowType, DENSHU_KBN denshuKbn)
            : base(WINDOW_ID.T_HANNYU_YOTEI_ICHIRAN, windowType)
        {
            this.InitializeComponent();
            this.logic = new HannyushutsuIchiranLogicClass(this);

            this.headerForm = headerForm;
            this.logic.setHeaderForm(headerForm);

            this.denshuKbn = denshuKbn;
        }
        #endregion

        #region 初期処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            this.logic.WindowInit();
        }
        #endregion

        #region イベント
        /// <summary>
        /// 車輌検証後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSharyouCd_Validated(object sender, EventArgs e)
        {
            this.logic.CheckSharyou();
        }

        /// <summary>
        /// 車輌選択ポップアップ起動後処理
        /// </summary>
        public virtual void txtSharyouCd_RemovePopUpNameAfterExecutePopUp()
        {
            this.txtSharyouCd.PopupWindowName = string.Empty;
        }

        /// <summary>
        /// 現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGenbaCd_Validated(object sender, EventArgs e)
        {
            this.logic.ChechGenbaCd();
        }
        
        /// <summary>
        /// 荷降業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void txtNioroshiGyoushaCd_Validated(object sender, EventArgs e)
        {
            if (!this.logic.CheckNioroshiGyoushaCd())
            {
                return;
            }
            // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            this.logic.NiorosiGyousyaCheck(ZenNiorosigyousyaCD);
            // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
        }

        /// <summary>
        /// 荷降現場更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void txtNioroshiGenbaCd_Validated(object sender, EventArgs e)
        {
            this.logic.ChechNioroshiGenbaCd();
        }

        /// <summary>
        /// 搬入予定一覧セルエンター処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHannyuYotei_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //搬入予定一覧の選択を解除する。
            dgvHanshutsuYotei.CurrentCell = null;

            //搬出予定一覧の選択を解除する。
            this.logic.GetHannyuYoteiUketsukeNumber(e);
        }

        /// <summary>
        /// 搬出予定一覧セルエンター処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvHanshutsuYotei_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //搬入予定一覧の選択を解除する。
            dgvHannyuYotei.CurrentCell = null;

            this.logic.GetHanshutsuYoteiUketsukeNumber(e);
        }

        #endregion

        #region Function押下時処理
        /// <summary>
        /// F1 計量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func1_Click(object sender, EventArgs e)
        {
            this.logic.Function1ClickLogic();
        }

        /// <summary>
        /// F2 受入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func2_Click(object sender, EventArgs e)
        {
            this.logic.Function2ClickLogic();
        }

        /// <summary>
        /// F3 出荷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func3_Click(object sender, EventArgs e)
        {
            this.logic.Function3ClickLogic();
        }

        /// <summary>
        /// F4 売上支払
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func4_Click(object sender, EventArgs e)
        {
            this.logic.Function4ClickLogic();
        }

        /// <summary>
        /// F6 CSV出力
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func6_Click(object sender, EventArgs e)
        {
            this.logic.Function6ClickLogic();
        }

        /// <summary>
        /// F7 条件クリア
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func7_Click(object sender, EventArgs e)
        {
            this.logic.Function7ClickLogic();
        }

        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func8_Click(object sender, EventArgs e)
        {
            this.logic.Function8ClickLogic();
        }

        /// <summary>
        /// F9 画面切替
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func9_Click(object sender, EventArgs e)
        {
            this.logic.Function9ClickLogic();
        }

        /// <summary>
        /// F10 搬入並替
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func10_Click(object sender, EventArgs e)
        {
            this.logic.Function10ClickLogic();
        }

        /// <summary>
        /// F11 搬出並替
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func11_Click(object sender, EventArgs e)
        {
            this.logic.Function11ClickLogic();
        }

        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void bt_func12_Click(object sender, EventArgs e)
        {
            this.logic.Function12ClickLogic();
        }

        #endregion

        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        /// <summary>
        /// 業者フォカスアウト処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGyoushaCd_Leave(object sender, EventArgs e)
        {
            ZengyousyaCD = this.txtGyoushaCd.Text;
        }

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtGyoushaCd_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.GyousyaCheck(ZengyousyaCD);
        }

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void gyousyaPopopafter()
        {
            this.logic.GyousyaCheck(ZengyousyaCD);
        }


        /// <summary>
        /// 荷降業者フォカスアウト処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNioroshiGyoushaCd_Leave(object sender, EventArgs e)
        {
            ZenNiorosigyousyaCD = this.txtNioroshiGyoushaCd.Text;
        }

        /// <summary>
        /// 業者更新後処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void niorosigyousyaPopopafter()
        {
            this.logic.NiorosiGyousyaCheck(ZenNiorosigyousyaCD);
        }
        // 20150921 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end
       
    }
}
