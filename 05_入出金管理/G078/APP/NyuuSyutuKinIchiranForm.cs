using System;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dto;
using Seasar.Quill;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.IchiranCommon.APP;
using Shougun.Core.Common.IchiranCommon.Logic;
using Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran.Const;
using Seasar.Framework.Exceptions;
using r_framework.Utility;
using r_framework.Logic;

namespace Shougun.Core.ReceiptPayManagement.NyuuSyutuKinIchiran
{
    [Implementation]
    public partial class NyuuSyutuKinIchiranForm : IchiranSuperForm
    {
        #region フィールド

        internal NyuuSyutuKinIchiran.LogicClass NyuuSyutuKinLogic;

        internal HeaderForm header_new;

        private Boolean isLoaded;

        #endregion

        public NyuuSyutuKinIchiranForm(HeaderForm headerForm)
            : this(headerForm, String.Empty)
        {
        }

        /// <summary>
        /// コンストラクタ（伝票種類の初期設定値を設定する場合に使用）
        /// </summary>
        /// <param name="headerForm">ヘッダ</param>
        /// <param name="denpyoShurui">伝票種類（1.入金 2.出金）</param>
        public NyuuSyutuKinIchiranForm(HeaderForm headerForm, string denpyoShurui)
            : base(DENSHU_KBN.NYUUSHUKKIN_ICHIRAN, false)
        {
            this.InitializeComponent();
            this.header_new = headerForm;

            this.DenpyoShurui = denpyoShurui;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.NyuuSyutuKinLogic = new LogicClass(this);

            //headerFormにSettingsの値
            this.header_new.KYOTEN_CD.Text = Properties.Settings.Default.SET_KYOTEN_CD;

            // 20150917 katen #12048 「システム日付」の基準作成、適用 start
            ////開始日付
            //if ("".Equals(Properties.Settings.Default.SET_HIDUKE_FROM))
            //{
            //    this.header_new.HIDUKE_FROM.Text = DateTime.Now.ToString();
            //}
            //else
            //{
            //    this.header_new.HIDUKE_FROM.Text = Properties.Settings.Default.SET_HIDUKE_FROM;
            //}

            ////終了日付
            //if ("".Equals(Properties.Settings.Default.SET_HIDUKE_TO))
            //{
            //    this.header_new.HIDUKE_TO.Text = DateTime.Now.ToString();
            //}
            //else
            //{
            //    this.header_new.HIDUKE_TO.Text = Properties.Settings.Default.SET_HIDUKE_TO;
            //}
            // 20150917 katen #12048 「システム日付」の基準作成、適用 start

            //日付選択
            if ("".Equals(Properties.Settings.Default.SET_HIDUKESENTAKU))
            {
                this.header_new.txtNum_HidukeSentaku.Text = "1";
            }
            else
            {
                this.header_new.txtNum_HidukeSentaku.Text = Properties.Settings.Default.SET_HIDUKESENTAKU;
            }

            // 伝票種類
            NyuuSyutuKinLogic.SetInitialDenpyoShurui();

            NyuuSyutuKinLogic.SetHeader(header_new);

            //社員コードを取得すること
            this.ShainCd = SystemProperty.Shain.CD;

            //Main画面で社員コード値を取得すること
            NyuuSyutuKinLogic.syainCode = SystemProperty.Shain.CD;

            //var parentForm = (BusinessBaseForm)this.Parent;
            //parentForm.bt_func10.Enabled = false;

            isLoaded = false;

            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }

        #region 画面コントロールイベント

        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e">イベント</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // タイトル
            this.header_new.lb_title.Text = WINDOW_TITLEExt.ToTitleString(WINDOW_ID.T_NYUSHUTSUKIN_ICHIRAN);

            // 画面情報の初期化
            if (isLoaded == false)
            {
                if (!this.NyuuSyutuKinLogic.WindowInit())
                {
                    return;
                }
                //this.SetLogicSelect();
                //this.NyuuSyutuKinLogic.Search();

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(3, 91);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(3, 113);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 140);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 290);

                // 画面初期化
                if (!this.NyuuSyutuKinLogic.ClearScreen("Initial"))
                {
                    return;
                }

                // パターンを設定
                if (!this.PatternUpdate())
                {
                    return;
                }
            }
            //else
            //{
            //    this.SetLogicSelect();
            //}
            //if (isLoaded == true)
            //{
            //    this.NyuuSyutuKinLogic.Search();
            //}

            ////DataGridViewの列名とソート順を取得
            //this.SetLogicSelect();

            //2013.12.15 naitou upd パターン更新 start
            ////検索処理
            //this.NyuuSyutuKinLogic.Search();
            //2013.12.15 naitou upd パターン更新 end

            isLoaded = true;
            // ソート条件の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            //this.Table = this.NyuuSyutuKinLogic.SearchResult;
            //2013.12.15 naitou upd パターン更新 start
            //base.OnLoad時にthis.Tableに設定されたヘッダー情報をグリッドに表示する

            //2013.12.23 naitou upd start
            ////if (!this.DesignMode)
            ////{
            ////    this.customDataGridView1.DataSource = null;
            ////    if (this.Table != null)
            ////    {
            ////        this.logic.CreateDataGridView(this.Table);
            ////    }
            ////}

            this.ShowHeader();
            //2013.12.23 naitou upd end

            //2013.12.15 naitou upd パターン更新 end

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

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
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

        /// <summary>
        /// 検索結果を表示
        /// </summary>
        /// <param name="e">イベント</param>
        public void SetSearch()
        {
            //this.Table = this.NyuuSyutuKinLogic.SearchResult; //2013.12.23 naitou upd
            if (!this.DesignMode)
            {
                this.logic.CreateDataGridView(this.NyuuSyutuKinLogic.SearchResult); //2013.12.23 naitou upd
            }
        }

        #endregion

        //2013.12.23 naitou upd start
        //#region 並替移動
        ///// <summary>
        ///// 並替移動
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //public virtual void MoveToSort(object sender, EventArgs e)
        //{
        //    this.customSortHeader1.Focus();
        //}

        //#endregion
        //2013.12.23 naitou upd end

        #region 検索結果表示

        /// <summary>
        /// 検索結果表示
        /// </summary>
        public virtual void ShowData()
        {
            //this.Table = this.NyuuSyutuKinLogic.SearchResult; //2013.12.23 naitou upd

            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                this.logic.CreateDataGridView(this.NyuuSyutuKinLogic.SearchResult); //2013.12.23 naitou upd
                foreach (DataGridViewColumn column in this.customDataGridView1.Columns)
                {
                    if (ConstCls.HIDDEN_SYSTEM_ID.Equals(column.Name) || 
                        ConstCls.HIDDEN_NYUUKIN_NUMBER.Equals(column.Name) ||
                        ConstCls.HIDDEN_SHUKKIN_NUMBER.Equals(column.Name) ||
                        ConstCls.HIDDEN_DETAIL_SYSTEM_ID.Equals(column.Name) ||
                        ConstCls.HIDDEN_TOK_INPUT_KBN.Equals(column.Name))
                    {
                        column.Visible = false;
                    }
                }
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

        //2013.12.23 naitou upd start
        //#region controlの操作(伝票種類、入金、出金)

        ///// <summary>
        ///// 入金ChangedEvent
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void radbtn_Nyuukin_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.radbtn_Nyuukin.Checked)
        //    {
        //        this.txtNum_DenpyouSyurui.Text = "1";
        //    }
        //}

        ///// <summary>
        ///// 出金ChangedEvent
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void radbtn_Syuutukin_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (this.radbtn_Syuutukin.Checked)
        //    {
        //        this.txtNum_DenpyouSyurui.Text = "2";
        //    }
        //}

        //#endregion
        //2013.12.23 naitou upd end

        //2013.12.23 naitou upd start
        //private void txtNum_DenpyouSyurui_TextChanged(object sender, EventArgs e)
        //{
        //    if ("1".Equals(this.txtNum_DenpyouSyurui.Text))
        //    {
        //        this.radbtn_Nyuukin.Checked = true;
        //    }

        //    if ("2".Equals(this.txtNum_DenpyouSyurui.Text))
        //    {
        //        this.radbtn_Syuutukin.Checked = true;
        //    }
        //}

        //private void txtNum_DenpyouSyurui_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == '1')
        //    {
        //        radbtn_Nyuukin.Checked = true;
        //        txtNum_DenpyouSyurui.SelectAll();
        //        e.Handled = true;
        //    }

        //    if (e.KeyChar == '2')
        //    {
        //        radbtn_Syuutukin.Checked = true;
        //        txtNum_DenpyouSyurui.SelectAll();
        //        e.Handled = true;
        //    }
        //}
        //2013.12.23 naitou upd end

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

        /// <summary>
        /// パターン再表示
        /// </summary>
        internal bool PatternUpdate()
        {
            bool ret = true;
            try
            {
                this.DenshuKbn = this.NyuuSyutuKinLogic.DenshuKbn;

                this.logic = new Shougun.Core.Common.IchiranCommon.Logic.IchiranBaseLogic(this);
                this.PatternReload(true);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("PatternUpdate", ex1);
                this.NyuuSyutuKinLogic.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("PatternUpdate", ex);
                this.NyuuSyutuKinLogic.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// パターンで作成された選択クエリを取得します。
        /// </summary>
        /// <returns>クエリ文字列</returns>
        internal string GetSelectQuery()
        {
            return this.SelectQuery;
        }

        /// <summary>
        /// パターンで作成されたソートクエリを取得します。
        /// </summary>
        /// <returns>クエリ文字列</returns>
        internal string GetOrderByQuery()
        {
            return this.OrderByQuery;
        }

        /// <summary>
        /// パターンで作成された結合クエリを取得します。
        /// </summary>
        /// <returns>クエリ文字列</returns>
        internal string GetJoinQuery()
        {
            return this.JoinQuery;
        }

        /// <summary>
        /// 引数で渡された伝票種類
        /// </summary>
        public string DenpyoShurui { get; private set; }

        private void txtNum_DenpyouSyurui_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_DenpyouSyurui.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("W001", "1", "2");
                e.Cancel = true;
            }

        }
    }
}