using System;
using System.Drawing;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using Seasar.Quill;
using Shougun.Core.ElectronicManifest.RealInfoSearch.Logic;
using Shougun.Core.Message;
using System.Text;
using System.Collections.Generic;

namespace Shougun.Core.ElectronicManifest.RealInfoSearch
{
    public partial class UIForm : Shougun.Core.Common.IchiranCommon.APP.IchiranSuperForm
    {

        #region フィールド

        /// <summary>
        /// 画面ロジック
        /// </summary>
        internal LogicClass MILogic;

        /// <summary>
        /// 初回フラグ
        /// </summary>
        internal Boolean isLoaded = false;

        /// <summary>
        /// 親フォーム
        /// </summary>
        public BusinessBaseForm ParentBaseForm { get; private set; }

        /// <summary>
        /// メッセージ表示を判定する変数
        /// </summary>
        public bool isShowMessage = false;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        private string beforeHstJigyoushaCd = string.Empty;
        private string beforeSbnJigyoushaCd = string.Empty;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(DENSHU_KBN.SAISHIN_JOUHOU_SHOUKAI, false)
        {
            this.InitializeComponent();

            //社員コード
            this.ShainCd = SystemProperty.Shain.CD;

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.MILogic = new LogicClass(this);
            
            // 完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }
        #endregion

        #region 初期化
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            if (!this.isLoaded)
            {
                // 排出
                Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                this.cntb_HstJigyoushaCd, this.cntb_HstJigyoushaName, this.cntb_HstJigyoushaBtn,
                this.cntb_HstJigyoujouCd, this.cntb_HstJigyoujouName, this.cntb_HstJigyoujouBtn,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.HAISHUTSU_NIZUMI_GYOUSHA | Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA, false, false,
                Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.HAISHUTSU_NIZUMI_GENBA,
                true, true, true);

                // ヒントテキストのみ再設定
                this.cntb_HstJigyoushaCd.Tag = "半角7文字以内で入力してください（スペースキー押下にて、検索画面を表示します）";
                this.cntb_HstJigyoujouCd.Tag = "半角10文字以内で入力してください（スペースキー押下にて、検索画面を表示します）";

                // 運搬受託者
                Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                    this.cntb_UpnJigyoushaCd, this.cntb_UpnJigyoushaName, this.cntb_UpnJigyoushaBtn,
                    null, null, null,
                    Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                    Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.UNPAN_JUTAKUSHA_KAISHA, false, false,
                    Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.NONE,
                    false, true, false);

                // ヒントテキストのみ再設定
                this.cntb_UpnJigyoushaCd.Tag = "半角7文字以内で入力してください（スペースキー押下にて、検索画面を表示します）";

                // 処分
                Shougun.Core.Common.BusinessCommon.Logic.DenshiMasterDataLogic.SetPopupSetting(
                    this.cntb_SbnJigyoushaCd, this.cntb_SbnJigyoushaName, this.cntb_SbnJigyoushaBtn,
                    this.cntb_SbnJigyoujouCd, this.cntb_SbnJigyoujouName, this.cntb_SbnJigyoujouBtn,
                    Common.BusinessCommon.Logic.DenshiMasterDataLogic.MANI_KBN.DENSHI,
                    Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUSYA_KBN.SHOBUN_NIOROSHI_GYOUSHA, false, false,
                    Common.BusinessCommon.Logic.DenshiMasterDataLogic.JIGYOUJOU_KBN.SHOBUN_NIOROSHI_GENBA,
                    true, true, true);

                // ヒントテキストのみ再設定
                this.cntb_SbnJigyoushaCd.Tag = "半角7文字以内で入力してください（スペースキー押下にて、検索画面を表示します）";
                this.cntb_SbnJigyoujouCd.Tag = "半角10文字以内で入力してください（スペースキー押下にて、検索画面を表示します）";
            }

            base.OnLoad(e);

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

            if (!this.isLoaded)
            {
                //初期化、初期表示
                if (!this.MILogic.WindowInit())
                {
                    return;
                }

                //index
                this.customDataGridView1.TabIndex = 49;

                // グリッドビューサイズ設定
                this.customDataGridView1.Size = new Size(997, 253);

                // グリッドビュー場所設定
                this.customDataGridView1.Location = new System.Drawing.Point(3, 203);

                //キー入力設定
                var parentForm = (BusinessBaseForm)this.Parent;

                //画面全体
                parentForm.KeyDown += new KeyEventHandler(UIForm_KeyDown);

                //処理No（ESC）
                parentForm.txb_process.KeyDown += new KeyEventHandler(TXB_PROCESS_KeyDown);

                // 非表示列登録
                this.SetHiddenColumns(this.MILogic.HIDDEN_KANRI_ID, this.MILogic.HIDDEN_LATEST_SEQ,
                    this.MILogic.UPN_ROUTE_NO_INSERT, this.MILogic.EDI_PASSWORD_HST_INSERT,
                    this.MILogic.EDI_PASSWORD_SBN_INSERT, this.MILogic.EDI_PASSWORD_UPN_INSERT, this.MILogic.CAN_CHECK);

                //表示の初期化
                if (!this.MILogic.ClearScreen("Initial"))
                {
                    return;
                }
            }

            // パターン読込
            this.PatternReload(!this.isLoaded);

            //検索
            //this.MILogic.meisaihyoujiFlg = false;//2013.12.25 touti upd画面起動時に検索しない 処理方法修正
            this.MILogic.selectQuery = this.logic.SelectQeury;
            this.MILogic.orderByQuery = this.logic.OrderByQuery;
            this.MILogic.joinQuery = this.logic.JoinQuery;

            //2013.12.25 touti 追加 パターン更新 start
            //base.OnLoad時にthis.Tableに設定されたヘッダー情報をグリッドに表示する
            //this.MILogic.Search();
            if (!this.DesignMode)
            {
                this.customDataGridView1.DataSource = null;
                this.customDataGridView1.Columns.Clear();
                if (this.Table != null)
                {
                    if (!this.MILogic.HeaderCheckBoxSupport())
                    {
                        return;
                    }

                    this.customDataGridView1.AllowUserToAddRows = false;
                    this.customDataGridView1.MultiSelect = false;
                    this.logic.CreateDataGridView(this.Table);
                }
            }
            //2013.12.25 touti 追加 パターン更新 end

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.customDataGridView1 != null)
            {
                this.customDataGridView1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }

            this.isLoaded = true;
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
        #endregion

        #region 画面イベント
        /// <summary>
        /// フォーム
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UIForm_KeyDown(object sender, KeyEventArgs e)
        {
            //switch (e.KeyCode)
            //{
            //    case Keys.Escape://ESCキー
            //        this.MILogic.SetFocusTxbProcess();
            //        break;
            //}
        }

        /// <summary>
        /// パターン1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn2_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn3_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn4_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// パターン5
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_ptn5_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// プレビュ(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func7_Click(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func8_Click(object sender, EventArgs e)
        {
            // パターンチェック
            if (this.PatternNo == 0)
            {
                MessageBoxUtility.MessageBoxShow("E057", "パターンが登録", "検索");
                return;
            }

            if (this.MILogic.CheckDate())
            {
                return;
            }

            if (this.MILogic.Search() == -1)
            {
                return;
            }

            if (this.customDataGridView1.RowCount == 0)
            {
                MessageBoxUtility.MessageBoxShow("C001");
            }
        }

        /// <summary>
        /// 情報照会
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func9_Click(object sender, EventArgs e)
        {
            this.MILogic.infoSearch();
        }

        /// <summary>
        /// 並び替え(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func10_Click(object sender, EventArgs e)
        {
            //2013.12.26 touti 起動時並び替えボタンを押すとシステムエラーになって　バグ対応 start 
            //if (this.MILogic.SearchResult.Rows.Count > 0)
            if (this.customDataGridView1.RowCount > 0)
            //2013.12.26 touti 起動時並び替えボタンを押すとシステムエラーになって　バグ対応 end 
            {
                this.customSortHeader1.ShowCustomSortSettingDialog();
            }

            // UNDONE:並び替え時にチェックボックスがnullになってしまう場合の暫定対策。
            // チェックボックスがnullの場合、チェックボックスの件数制限時にNullReferenceExceptionが発生してしまう。
            // 並び替え時にチェックボックスの値を復元したりする対応等があった場合、以下のメソッドを見直す必要がある。
            this.MILogic.ClearCheckBox();
        }

        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.Parent;

            this.customDataGridView1.DataSource = "";

            this.Close();
            parentForm.Close();
        }

        /// <summary>
        /// パターン一覧(1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process1_Click(object sender, EventArgs e)
        {
            var sysID = this.OpenPatternIchiran();

            this.MILogic.selectQuery = this.logic.SelectQeury;
            this.MILogic.orderByQuery = this.logic.OrderByQuery;
            this.MILogic.joinQuery = this.logic.JoinQuery;

            if (!string.IsNullOrEmpty(sysID))
            {
                this.ShowData();
            }
        }

        /// <summary>
        /// 検索条件設定(2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void bt_process2_Click(object sender, EventArgs e)
        {
            //仕様不明なため、未実装。確認用
            MessageBox.Show("検索条件設定画面", "画面遷移");
        }

        /// <summary>
        /// 特定現場照会処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //public virtual void bt_process3_Click(object sender, EventArgs e)
        //{
        //    this.MILogic.specialInfoSearch();
        //}


        /// <summary>
        /// ESCキー
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TXB_PROCESS_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    this.MILogic.SelectButton();
            //}
        }

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

        /// <summary>
        /// マニ番条件テキストチェンジ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cntb_Jyoken_KBN_TextChanged(object sender, EventArgs e)
        {
            this.MILogic.SetEnabledManiNumberControl();
        }

        #endregion

        #region 一覧画面表示
        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        public virtual void ShowData()
        {
            this.logic.CreateDataGridView(this.MILogic.SearchResult);
        }
        #endregion

        #region DataGridView_CcellClick
        /// <summary>
        /// DataGridViewのセルをクリックするエベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*
        public void customDataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //データがある場合
            if (this.customDataGridView1.Rows.Count > 0)
            {
                //ヘッダーセルじゃない場合
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    //DataGridViewにチェックした行
                    List<string> list = new List<string>();
                    foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                    {
                        if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].EditedFormattedValue == true)
                        {
                            if (!list.Contains(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                            {
                                list.Add(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString());
                            }
                        }
                    }
                    //チェックできる行
                    int checkCnt = this.MILogic.MAX_CHECK - this.MILogic.GetRequestDataInDay() - list.Count;

                    //チェックできる行がある場合
                    if (checkCnt > 0)
                    {
                        foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                        {
                            if (dgvRow.Cells[7].Value.ToString() == "1")
                            {
                                dgvRow.Cells[0].ReadOnly = false;
                            }
                        }
                        DataGridViewCell cell = customDataGridView1[e.ColumnIndex, e.RowIndex];

                        if (e.ColumnIndex == 0)
                        {
                            if (this.customDataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() == "1"
                                && (bool)cell.EditedFormattedValue == false)
                            {
                                if (!list.Contains(this.customDataGridView1.Rows[e.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                                {
                                    list.Add(this.customDataGridView1.Rows[e.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString());
                                    checkCnt--;
                                    if (checkCnt == 0)
                                    {
                                        isShowMessage = true;
                                    }
                                }
                                cell.Value = true;
                            }
                            else if ((bool)cell.EditedFormattedValue)
                            {
                                cell.Value = false;
                            }
                        }
                    }
                    //チェックできる行がない場合
                    if (checkCnt <= 0)
                    {
                        foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                        {
                            if ((bool)dgvRow.Cells[0].Value == false && !list.Contains(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                            {
                                dgvRow.Cells[0].ReadOnly = true;
                            }
                            else
                            {
                                dgvRow.Cells[0].ReadOnly = false;
                            }
                            if ((bool)customDataGridView1[0, e.RowIndex].EditedFormattedValue == false
                                && list.Contains(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString())
                                && list.Contains(this.customDataGridView1.Rows[e.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString())
                                && dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString().Equals(this.customDataGridView1.Rows[e.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                            {
                                dgvRow.Cells[0].Value = true;
                            }
                            else
                            {
                                dgvRow.Cells[0].Value = false;
                            }
                        }

                        isShowMessage = true;
                    }
                    else
                    {
                        isShowMessage = false;
                    }

                    if (isShowMessage == true && this.customDataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() == "1"
                        && (bool)customDataGridView1[e.ColumnIndex, e.RowIndex].EditedFormattedValue == false
                        && !list.Contains(this.customDataGridView1.Rows[e.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("{0}の最新情報照会件数が100件を超えたため、これ以上のチェックは出来ません",
                                String.Format("過去{0}時間以内", this.MILogic.EXECUTING_DECISION_HOUR));
                        MessageBox.Show(sb.ToString(), "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    this.customDataGridView1.RefreshEdit();
                    this.customDataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
                }
            }
        }
        */
        #endregion

        /// <summary>
        /// ヘッダーセルクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customDataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewColumn col = this.customDataGridView1.Columns[e.ColumnIndex];
            if (col is DataGridViewCheckBoxColumn)
            {
                CustomDgvCheckBoxHeaderCell_Ex header = col.HeaderCell as CustomDgvCheckBoxHeaderCell_Ex;
                if (header != null)
                {
                    header.MouseClick(e);
                    this.customDataGridView1.Refresh();
                }
            }
        }

        /// <summary>
        /// CellMouseClickイベント
        /// (CheckBoxクリック、セルクリック時に発生)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customDataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //データがある場合
            if (this.customDataGridView1.Rows.Count > 0)
            {
                //ヘッダーセルじゃない場合
                if (e.RowIndex >= 0 && e.ColumnIndex == 0)
                {
                    // チェックボックスOFF制御
                    if ((bool)this.customDataGridView1[0, e.RowIndex].EditedFormattedValue)
                    {
                        this.customDataGridView1[0, e.RowIndex].Value = false;
                    }
                    // チェックボックスON制御
                    else
                    {
                        //DataGridViewにチェックした行
                        List<string> list = new List<string>();
                        foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                        {
                            if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].EditedFormattedValue == true)
                            {
                                if (!list.Contains(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                                {
                                    list.Add(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString());
                                }
                            }
                        }

                        int retCount = this.MILogic.GetRequestDataInDay();
                        if (retCount == -1)
                        {
                            return;
                        }

                        //チェックできる行
                        int checkCnt = this.MILogic.MAX_CHECK - retCount - list.Count;

                        //チェックできる行がある場合
                        if (checkCnt > 0)
                        {
                            foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                            {
                                if (dgvRow.Cells[7].Value.ToString() == "1")
                                {
                                    dgvRow.Cells[0].ReadOnly = false;
                                }
                            }
                            DataGridViewCell cell = customDataGridView1[e.ColumnIndex, e.RowIndex];

                            if (e.ColumnIndex == 0)
                            {
                                if (this.customDataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() == "1"
                                    && (bool)cell.EditedFormattedValue == false)
                                {
                                    if (!list.Contains(this.customDataGridView1.Rows[e.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                                    {
                                        list.Add(this.customDataGridView1.Rows[e.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString());
                                        checkCnt--;
                                        if (checkCnt == 0)
                                        {
                                            isShowMessage = true;
                                        }
                                    }
                                    cell.Value = true;
                                }
                            }
                        }
                        //チェックできる行がない場合
                        if (checkCnt <= 0)
                        {
                            foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                            {
                                if ((bool)dgvRow.Cells[0].Value == false && !list.Contains(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                                {
                                    dgvRow.Cells[0].ReadOnly = true;
                                }
                                else
                                {
                                    dgvRow.Cells[0].ReadOnly = false;
                                }
                                if ((bool)customDataGridView1[0, e.RowIndex].EditedFormattedValue == false
                                    && list.Contains(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString())
                                    && list.Contains(this.customDataGridView1.Rows[e.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString())
                                    && dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString().Equals(this.customDataGridView1.Rows[e.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                                {
                                    dgvRow.Cells[0].Value = true;
                                }
                            }

                            isShowMessage = true;
                        }
                        else
                        {
                            isShowMessage = false;
                        }

                        if (isShowMessage == true && this.customDataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString() == "1"
                            && (bool)customDataGridView1[e.ColumnIndex, e.RowIndex].EditedFormattedValue == false
                            && !list.Contains(this.customDataGridView1.Rows[e.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("{0}の最新情報照会件数が{1}件を超えたため、これ以上のチェックは出来ません",
                                    String.Format("過去{0}時間以内", this.MILogic.EXECUTING_DECISION_HOUR),
                                    this.MILogic.MAX_CHECK);
                            MessageBox.Show(sb.ToString(), "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.customDataGridView1[e.ColumnIndex, e.RowIndex].Tag = "G413";
                            this.customDataGridView1.CancelEdit();
                            return;
                        }
                    }
                }

                this.customDataGridView1.RefreshEdit();
                this.customDataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// CurrentCellDirtyStateChangedイベント
        /// (CheckBoxクリック、スペースキー押下時に発生)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void customDataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            DataGridViewCell cell = this.customDataGridView1.CurrentCell;
            //データがある場合
            if (this.customDataGridView1.Rows.Count > 0)
            {
                //ヘッダーセルじゃない場合
                if (cell.RowIndex >= 0 && cell.ColumnIndex == 0 && this.customDataGridView1.IsCurrentCellDirty)
                {
                    // チェックボックスOFF制御
                    if ((bool)this.customDataGridView1[0, cell.RowIndex].EditedFormattedValue)
                    {
                        this.customDataGridView1[0, cell.RowIndex].Value = false;
                    }
                    // チェックボックスON制御
                    else
                    {
                        //DataGridViewにチェックした行
                        List<string> list = new List<string>();
                        foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                        {
                            if (dgvRow.Cells[0].Value != null && (bool)dgvRow.Cells[0].EditedFormattedValue == true)
                            {
                                if (!list.Contains(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                                {
                                    list.Add(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString());
                                }
                            }
                        }

                        int retCount = this.MILogic.GetRequestDataInDay();
                        if (retCount == -1)
                        {
                            return;
                        }

                        //チェックできる行
                        int checkCnt = this.MILogic.MAX_CHECK - retCount - list.Count;

                        //チェックできる行がある場合
                        if (checkCnt > 0)
                        {
                            foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                            {
                                if (dgvRow.Cells[7].Value.ToString() == "1")
                                {
                                    dgvRow.Cells[0].ReadOnly = false;
                                }
                            }

                            if (cell.ColumnIndex == 0)
                            {
                                if (this.customDataGridView1.Rows[cell.RowIndex].Cells[7].Value.ToString() == "1"
                                    && !(bool)cell.EditedFormattedValue)
                                {
                                    if (!list.Contains(this.customDataGridView1.Rows[cell.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                                    {
                                        list.Add(this.customDataGridView1.Rows[cell.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString());
                                        checkCnt--;
                                        if (checkCnt == 0)
                                        {
                                            isShowMessage = true;
                                        }
                                    }
                                    cell.Value = true;
                                }
                            }
                        }
                        //チェックできる行がない場合
                        if (checkCnt <= 0)
                        {
                            foreach (DataGridViewRow dgvRow in this.customDataGridView1.Rows)
                            {
                                if ((bool)dgvRow.Cells[0].Value == false && !list.Contains(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                                {
                                    dgvRow.Cells[0].ReadOnly = true;
                                }
                                else
                                {
                                    dgvRow.Cells[0].ReadOnly = false;
                                }
                                if ((bool)customDataGridView1[0, cell.RowIndex].EditedFormattedValue == false
                                    && list.Contains(dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString())
                                    && list.Contains(this.customDataGridView1.Rows[cell.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString())
                                    && dgvRow.Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString().Equals(this.customDataGridView1.Rows[cell.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString()))
                                {
                                    dgvRow.Cells[0].Value = true;
                                }
                            }

                            isShowMessage = true;
                        }
                        else
                        {
                            isShowMessage = false;
                        }

                        if (isShowMessage == true && this.customDataGridView1.Rows[cell.RowIndex].Cells[7].Value.ToString() == "1"
                            && (bool)customDataGridView1[cell.ColumnIndex, cell.RowIndex].EditedFormattedValue == false
                            && !list.Contains(this.customDataGridView1.Rows[cell.RowIndex].Cells[this.MILogic.COLUMN_NAME_MANIFEST_ID].Value.ToString())
                            && string.IsNullOrEmpty((string)cell.Tag))
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("{0}の最新情報照会件数が{1}件を超えたため、これ以上のチェックは出来ません",
                                    String.Format("過去{0}時間以内", this.MILogic.EXECUTING_DECISION_HOUR),
                                    this.MILogic.MAX_CHECK);
                            MessageBox.Show(sb.ToString(), "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.customDataGridView1.CancelEdit();
                            return;
                        }
                    }
                }

                this.customDataGridView1.RefreshEdit();
                this.customDataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        /// <summary>
        /// 排出事業者 PopupBeforeExecuteMethod
        /// </summary>
        public void HstJigyousha_PopupBeforeExecuteMethod()
        {
            this.beforeHstJigyoushaCd = this.cntb_HstJigyoushaCd.Text;
        }

        /// <summary>
        /// 排出事業者 PopupAfterExecuteMethod
        /// </summary>
        public void HstJigyousha_PopupAfterExecuteMethod()
        {
            if (this.beforeHstJigyoushaCd != this.cntb_HstJigyoushaCd.Text)
            {
                this.cntb_HstJigyoujouCd.Text = string.Empty;
                this.cntb_HstJigyoujouName.Text = string.Empty;
            }
        }

        /// <summary>
        /// 処分受託者 PopupBeforeExecuteMethod
        /// </summary>
        public void SbnJigyoushaCd_PopupBeforeExecuteMethod()
        {
            this.beforeSbnJigyoushaCd = this.cntb_SbnJigyoushaCd.Text;
        }

        /// <summary>
        /// 処分受託者 PopupAfterExecuteMethod
        /// </summary>
        public void SbnJigyoushaCd_PopupAfterExecuteMethod()
        {
            if (this.beforeSbnJigyoushaCd != this.cntb_SbnJigyoushaCd.Text)
            {
                this.cntb_SbnJigyoujouCd.Text = string.Empty;
                this.cntb_SbnJigyoujouName.Text = string.Empty;
            }
        }
    }
}
