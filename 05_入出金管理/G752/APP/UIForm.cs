using System;
using r_framework.Const;
using r_framework.Dto;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.IchiranCommon.APP;
using System.Windows.Forms;

namespace Shougun.Core.ReceiptPayManagement.ShukkinKeshikomi
{
    [Implementation]
    public partial class UIForm : IchiranSuperForm
    {
        #region フィールド

        private ShukkinKeshikomi.LogicClass Logic;

        private UIHeader header_new;

        private Boolean isLoaded;

        /// <summary>起動元画面の拠点CD</summary>
        internal string kyotenCdForStartUpPoint;

        /// <summary>起動元画面の伝票日付</summary>
        internal string denpyouHidukeForStartUpPoint;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;
        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="headerForm"></param>
        /// <param name="strShukkinNum">出金番号</param>
        /// <param name="kyotenCd">拠点CD</param>
        /// <param name="denpyouHidukeForm">伝票日付</param>
        public UIForm(UIHeader headerForm, String strShukkinNum, string kyotenCd, string denpyouHidukeForm)
            : base(DENSHU_KBN.SHUKKIN_KESHIKOMI_RIEKI_ICHIRAN, false)
        {
            this.InitializeComponent();

            this.header_new = headerForm;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);

            //headerFormにSettingsの値

            this.header_new.KYOTEN_CD.Text = Properties.Settings.Default.SET_KYOTEN_CD;

            // 出金入力画面から遷移した場合
            this.kyotenCdForStartUpPoint = kyotenCd;
            this.denpyouHidukeForStartUpPoint = denpyouHidukeForm;

            //出金番号
            this.Shukkin_CD.Text = strShukkinNum;

            Logic.SetHeader(header_new);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;
            
            //Main画面で社員コード値を取得すること
            Logic.syainCode = SystemProperty.Shain.CD;
            //2013.12.23 naitou upd end

            isLoaded = false;
        }

        #region 画面コントロールイベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //2013.12.23 naitou upd start
            // 画面情報の初期化
            if (isLoaded == false)
            {
                if (!this.Logic.WindowInit())
                {
                    return;
                }
                //this.setLogicSelect();
                //this.Logic.Search();

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(3, 92);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(3, 115);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 143);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 290);
            }

            // パターンをロードする
            this.PatternReload();

            //画面初期化
            if (!this.Logic.ClearScreen("Initial"))
            {
                return;
            }

            // 出金入力から起動された場合、検索条件をセットしなおす
            if (!this.Logic.SetStartUpCondition())
            {
                return;
            }

            isLoaded = true;

            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            //base.OnLoad時にthis.Tableに設定されたヘッダー情報をグリッドに表示する
            this.ShowHeader();
            //2013.12.23 naitou upd end

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
                this.bt_ptn1.Top += 7;
                this.bt_ptn2.Top += 7;
                this.bt_ptn3.Top += 7;
                this.bt_ptn4.Top += 7;
                this.bt_ptn5.Top += 7;
            }

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
            var tabIndex = this.customSortHeader1.TabIndex;
            var arrControl = new Control[] { this.customDataGridView1, this.bt_ptn1, this.bt_ptn2, this.bt_ptn3, this.bt_ptn4, this.bt_ptn5 };
            foreach (var ctr in arrControl)
            {
                tabIndex += 1;
                ctr.TabIndex = tabIndex;
            }
            if (this.customDataGridView1 != null && this.customDataGridView1.Rows.Count == 0)
            {
                this.customDataGridView1.TabStop = false;
            }
        }

        /// <summary>
        /// 初回表示イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            base.OnShown(e);
        }

        //2013.12.23 naitou upd start
        /// <summary>
        /// 検索結果を表示
        /// </summary>
        /// <param name="e">イベント</param>
        public void SetSearch()
        {
            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.Logic.SearchResult); 
            }
        }
        //2013.12.23 naitou upd end

        #endregion

        #region DataGridViewの列名とソート順を取得する

        /// <summary>
        /// 共通からSQL文でDataGridViewの列名とソート順を取得する
        /// </summary>
        public void setLogicSelect()
        {
            this.Logic.selectQuery = this.logic.SelectQeury;
            this.Logic.orderByQuery = this.logic.OrderByQuery;
            this.Logic.joinQuery = this.logic.JoinQuery;
        }

        #endregion

        #region 検索結果表示

        /// <summary>
        /// 検索結果表示
        /// </summary>
        public virtual void ShowData()
        {
            //this.Table = this.Logic.SearchResult; //2013.12.23 naitou upd
            this.customSortHeader1.SortDataTable(Table); // まず抽出データをソートしてから
            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                this.logic.CreateDataGridView(this.Logic.SearchResult); //2013.12.23 naitou upd

                this.HideSystemColumn();
            }
        }

        /// <summary>
        /// システム必須列を非表示にします。
        /// </summary>
        private void HideSystemColumn()
        {
            // システムID
            if (this.customDataGridView1.Columns.Contains(this.Logic.HIDDEN_SYSTEM_ID))
            {
                this.customDataGridView1.Columns[this.Logic.HIDDEN_SYSTEM_ID].Visible = false;
            }

            // 出金番号
            if (this.customDataGridView1.Columns.Contains(this.Logic.HIDDEN_SHUKKIN_NUMBER))
            {
                this.customDataGridView1.Columns[this.Logic.HIDDEN_SHUKKIN_NUMBER].Visible = false;
            }

            // 消込回数
            if (this.customDataGridView1.Columns.Contains(this.Logic.HIDDEN_KESHIKOMI_SEQ))
            {
                this.customDataGridView1.Columns[this.Logic.HIDDEN_KESHIKOMI_SEQ].Visible = false;
            }
        }

        //2013.12.23 naitou upd start
        /// <summary>
        /// ヘッダー表示
        /// </summary>
        public void ShowHeader()
        {
            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                if (this.Table != null)
                {
                    this.logic.CreateDataGridView(this.Table);
                }
            }
        }
        //2013.12.23 naitou upd end

        #endregion

        //2013.12.15 naitou upd パターン更新 start
        /// <summary>
        /// パターンボタン更新処理
        /// </summary>
        /// <param name="sender">イベント対象オブジェクト</param>
        /// <param name="e">イベントクラス</param>
        /// <param name="ptnNo">パターンNo(0はデフォルトパターンを表示)</param>
        public void PatternButtonUpdate(object sender, System.EventArgs e, int ptnNo = -1)
        {
            if (ptnNo != -1) this.PatternNo = ptnNo;
            this.OnLoad(e);
        }
        //2013.12.15 naitou upd パターン更新 end
    }
}
