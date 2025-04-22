using System;
using r_framework.Const;
using r_framework.Logic;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using System.Windows.Forms;

namespace Shougun.Core.SalesPayment.Denpyouichiran
{
    [Implementation]
    public partial class DenpyouichiranForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region フィールド
        private Denpyouichiran.LogicClass DenpyouichiranLogic;

        private string selectQuery = string.Empty;

        private string orderQuery = string.Empty;

        private string joinQuery = string.Empty;

        HeaderForm header_new;

        private Boolean isLoaded;

        /// <summary>
        /// ベースロジックで作成したSELECTクエリ
        /// </summary>
        internal string baseSelectQuery = string.Empty;

        /// <summary>
        /// ベースロジックで作成したORDER BYクエリ
        /// </summary>
        internal string baseOrderByQuery = string.Empty;

        /// <summary>
        /// ベースロジックで作成したJOINクエリ
        /// </summary>
        internal string baseJoinQuery = string.Empty;
        #endregion

        /// <summary>
        /// 画面ロジック
        /// </summary>
        public DenpyouichiranForm(DENSHU_KBN denshuKbn, string searchString, HeaderForm headerFor, string txt_SyainCode)
            : base(DENSHU_KBN.DENPYOU_ICHIRAN, false)
        {
            this.InitializeComponent();
            this.header_new = headerFor;
            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.DenpyouichiranLogic = new LogicClass(this);
            if (!string.IsNullOrEmpty(searchString))
            {
                string getSearchString = searchString.Replace("\r", "").Replace("\n", "");
                //検索対象文字列取得
                this.DenpyouichiranLogic.searchString = getSearchString; 
            }
            DenpyouichiranLogic.SetHeader(header_new);
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
            //社員コードを取得すること
            this.ShainCd = txt_SyainCode;
            //Main画面で社員コード値を取得すること
            DenpyouichiranLogic.syainCode = txt_SyainCode;
            //伝種区分を取得すること
            DenpyouichiranLogic.denShu_Kbn = denshuKbn;
            //タブオーダー設定
            this.customDataGridView1.TabIndex = 42;
            isLoaded = false;
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            base.OnShown(e);
        }

        #region 画面コントロールイベント
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            // 画面情報の初期化
            if (!isLoaded)
            {
                if (!this.DenpyouichiranLogic.WindowInit())
                {
                    return;
                }
                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 183);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);
                this.customSortHeader1.Location = new System.Drawing.Point(4, 210);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);
                this.customDataGridView1.Location = new System.Drawing.Point(3, 239);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 191);
                // ヘッダー名チェック
                if (String.IsNullOrEmpty(this.txtNum_DenpyoKind.Text))
                {
                    this.DenpyouichiranLogic.disp_Flg = 0;
                }
                else
                {
                    this.DenpyouichiranLogic.disp_Flg = int.Parse(this.txtNum_DenpyoKind.Text);
                    switch (this.DenpyouichiranLogic.disp_Flg)
                    {
                        case 1:
                            this.DenshuKbn = DENSHU_KBN.UKEIRE_ICHIRAN;
                            break;
                        case 2:
                            this.DenshuKbn = DENSHU_KBN.SHUKKA_ICHIRAN;
                            break;
                        case 3:
                            this.DenshuKbn = DENSHU_KBN.URIAGE_SHIHARAI_ICHIRAN;
                            break;
                        case 4:
                            this.DenshuKbn = DENSHU_KBN.DAINOU_ICHIRAN;
                            break;
                        //PhuocLoc 2021/05/05 #148576 -Start
                        case 5:
                            this.DenshuKbn = DENSHU_KBN.UKEIRE_SHUKKA_URSH_ICHIRAN;
                            break;
                        //PhuocLoc 2021/05/05 #148576 -End
                        //20150422 Jyokou 4935_4 END
                        default:
                            this.DenshuKbn = DENSHU_KBN.DENPYOU_ICHIRAN;
                            break;
                    }
                    this.header_new.lb_title.Text = this.DenshuKbn.ToTitleString();
                    this.Parent.Text = r_framework.Dto.SystemProperty.CreateWindowTitle(this.header_new.lb_title.Text);
                }

                this.PatternUpdate();
                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                }
            }
            isLoaded = true;
            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();
            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();
            // ベースロジックで作成したクエリを一旦保存
            this.baseSelectQuery = this.logic.SelectQeury;
            this.baseOrderByQuery = this.logic.OrderByQuery;
            this.baseJoinQuery = this.logic.JoinQuery;
            //パターン区分が受入実績の時は、伝票区分を非活性にする
            if (this.logic.currentPatternDto != null && this.logic.currentPatternDto.OutputPattern.OUTPUT_KBN == 3)
            {
                this.customPanel2.Enabled = false;
            }
            else
            {
                this.customPanel2.Enabled = true;
            }

            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
            }
            //thongh 2015/10/16 #13526 start
            //読込データ件数の設定
            if (this.customDataGridView1 != null)
            {
                this.header_new.ReadDataNumber.Text = this.customDataGridView1.Rows.Count.ToString();
            }
            else
            {
                this.header_new.ReadDataNumber.Text = "0";
            }
            //thongh 2015/10/16 #13526 end
            this.customSortHeader1.TabStop = false;
            this.customSearchHeader1.TabStop = false;
        }
        #endregion

        /// <summary>
        /// パターン再表示（グリッドビューは更新しない）
        /// </summary>
        public void PatternUpdate()
        {
            // ロジッククラスを初期化（伝種区分を更新するため）
            this.logic = new Shougun.Core.Common.IchiranCommon.Logic.IchiranBaseLogic(this);
            this.PatternReload(true);
            this.DenpyouichiranLogic.HideColumnHeader();
            // ベースロジックで作成したクエリを一旦保存
            this.baseSelectQuery = this.logic.SelectQeury;
            this.baseOrderByQuery = this.logic.OrderByQuery;
            this.baseJoinQuery = this.logic.JoinQuery;
            this.customPanel2.Enabled = true;
        }

        #region 並替移動
        /// <summary>
        /// 並替移動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void MoveToSort(object sender, EventArgs e)
        {
            this.customSortHeader1.Focus();
        }

        #endregion

        #region 検索結果表示
        public virtual void ShowData()
        {
            this.Table = this.DenpyouichiranLogic.searchResult;
            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Table);
                this.DenpyouichiranLogic.HideColumnHeader();
            }
        }
        #endregion

        // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        // 現場CDフォカスアウト
        public void GYOUSYA_CD_Pupafter()
        {
            this.DenpyouichiranLogic.GYOUSHA_CD_After();
        }

        // 出荷業者更新後CDフォカスアウト
        public void SHUKKA_GYOUSYA_CD_Pupafter()
        {
            this.DenpyouichiranLogic.SHUKKA_GYOUSHA_CD_After();
        }

        // 荷積業者CDフォカスアウト
        public void NIDUMIGYOUSYA_CD_Pupafter()
        {
            this.DenpyouichiranLogic.NIDUMIGYOUSYA_CD_After();
        }

        // 荷卸業者CDフォカスアウト
        public void NIOROSHIGYOUSYA_CD_Pupafter()
        {
            this.DenpyouichiranLogic.NIOROSHIGYOUSYA_CD_After();
        }
        // 20150916 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        private void txtNum_DenpyoKind_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_DenpyoKind.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "4");
                e.Cancel = true;
            }
        }

        private void txtNum_Denpyoukubun_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_Denpyoukubun.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "3");
                e.Cancel = true;
            }
        }

        private void txtNum_KenshuMustKbn_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_KenshuMustKbn.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "3");
                e.Cancel = true;
            }
        }

        private void txtNum_KenshuJyoukyou_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_KenshuJyoukyou.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "2");
                e.Cancel = true;
            }
        }
    }
}
