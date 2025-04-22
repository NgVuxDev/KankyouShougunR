using System;
using System.Collections.Generic;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Utility;

namespace Shougun.Core.ExternalConnection.FileUpload
{
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private LogicClass Logic;

        /// <summary>
        /// ファイルIDリスト
        /// </summary>
        internal List<long> fileIdList;

        /// <summary>
        /// 遷移元画面のタイトル
        /// </summary>
        internal WINDOW_ID windowId;

        /// <summary>
        /// 遷移元画面のパラメータ(PKキー)リスト
        /// </summary>
        /// <remarks>
        /// 委託契約書
        /// 1.システムID
        /// 2.ファイル名(PKキーではない)
        /// 地域別許可番号入力
        /// 1.許可区分
        /// 2.業者CD
        /// 3.現場CD
        /// 4.地域CD
        /// </remarks>
        internal string[] paramList;

        /// <summary>
        /// INXS処理transactionId
        /// </summary>
        internal string transactionId;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : this(null, WINDOW_ID.NONE, null)
        {
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="fileIdList"></param>
        /// <param name="windowId"></param>
        /// <param name="paramList"></param>
        public UIForm(List<long> fileIdList, WINDOW_ID windowId, string[] paramList)
            : base(DENSHU_KBN.FILE_UPLOAD, false)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.Logic = new LogicClass(this);

            this.fileIdList = fileIdList;
            this.windowId = windowId;
            this.paramList = paramList;
        }
        #endregion

        /// <summary>
        /// 画面読み込み処理
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            //Init transaction id
            this.transactionId = Guid.NewGuid().ToString();

            if (!this.Logic.WindowInit()) { return; }

            this.customSearchHeader1.Visible = false;
            this.customSearchHeader1.Location = new System.Drawing.Point(2, 64);
            this.customSearchHeader1.Size = new System.Drawing.Size(640, 26);

            this.customSortHeader1.Visible = false;
            this.customSortHeader1.Location = new System.Drawing.Point(2, 64);
            this.customSortHeader1.Size = new System.Drawing.Size(640, 26);

            this.customDataGridView1.Location = new System.Drawing.Point(3, 70);
            this.customDataGridView1.Size = new System.Drawing.Size(997, 380);

            this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;

            // チェックボックスを配置する。
            this.Logic.HeaderCheckBoxSupport();

            // 表示区分の初期表示
            var isSearch = this.Logic.SetHyoujiKbn(this.fileIdList);

            if (isSearch)
            {
                // 一覧表示
                this.Logic.SetIchiran();
            }
        }

        /// <summary>
        /// FormのShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_Shown(object sender, EventArgs e)
        {
            // 画面最大化対策としてNormalを指定する
            this.ParentForm.WindowState = FormWindowState.Normal;

            //// この画面では最大化/最小化ボタンを表示　また、画面サイズの変更も可能とする
            //this.ParentForm.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            //this.ParentForm.MinimizeBox = true;
            //this.ParentForm.MaximizeBox = true;
        }

        #region ファンクションボタン
        /// <summary>
        /// 削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.Delete();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// プレビュー(F5)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.Preview();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件クリア(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.Clear();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //検索実行
            this.Logic.SearchLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// アップロード(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.Upload();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ダウンロード(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func10_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.Download();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.customDataGridView1.DataSource = "";

            if (this.Logic.parentbaseform != null)
            {
                this.Logic.parentbaseform.Close();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion
    }
}
