// $Id$
using System;
using System.Data;
using System.Windows.Forms;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.IchiranCommon.APP;
//ロジこん連携用
using Shougun.Core.ExternalConnection.ExternalCommon.Const;

namespace Shougun.Core.ExternalConnection.GaibuRenkeiGenbaIchiran
{
    public partial class GaibuRenkeiGenbaIchiranForm : IchiranSuperForm
    {
        #region フィールド
        /// <summary>
        /// ビジネスロジック
        /// </summary>
        private LogicClass Logic;

        /// <summary>
        /// 前回業者コード
        /// </summary>
        public string beforGyoushaCD = string.Empty;

        private string preGyoushaCd { get; set; }
        private string curGyoushaCd { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="denshuKbn"></param>
        public GaibuRenkeiGenbaIchiranForm()
            : base(DENSHU_KBN.GAIBU_RENKEI_GENBA_ICHIRAN, false)
        {
            InitializeComponent();

            // 社員CDを取得すること
            this.ShainCd = SystemProperty.Shain.CD;
        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面Load処理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            // デフォルトパターンを設定する
            base.OnLoad(e);

            // 初回のみ登録
            if (this.Logic == null)
            {
                // ビジネスロジックの初期化
                this.Logic = new LogicClass(this);

                // 画面初期化
                bool catchErr = this.Logic.WindowInit();
                if (catchErr)
                {
                    return;
                }

                this.customSearchHeader1.Visible = true;
                this.customSearchHeader1.Location = new System.Drawing.Point(4, 151);
                this.customSearchHeader1.Size = new System.Drawing.Size(997, 26);

                this.customSortHeader1.Location = new System.Drawing.Point(4, 173);
                this.customSortHeader1.Size = new System.Drawing.Size(997, 26);

                this.customDataGridView1.Location = new System.Drawing.Point(3, 200);
                this.customDataGridView1.Size = new System.Drawing.Size(997, 250);

                // 汎用検索機能が未実装の為、汎用検索は非表示
                this.searchString.Visible = false;

                // Anchorの設定は必ずOnLoadで行うこと
                if (this.customDataGridView1 != null)
                {
                    this.customDataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Left;
                }
            }

            this.PatternReload();

            //一覧内のチェックボックスの設定
            this.Logic.HeaderCheckBoxSupport();

            // フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            // パターンヘッダのみ表示
            this.ShowData();
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

        #region ファンクションボタン 押下処理

        #region F2 新規
        /// <summary>
        /// F2 新規
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal virtual void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.OpenWindow(WINDOW_TYPE.NEW_WINDOW_FLAG, true);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F3 修正
        /// <summary>
        /// F3 修正
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal virtual void bt_func3_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.OpenWindow(WINDOW_TYPE.UPDATE_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F6 CSV
        /// <summary>
        /// F6 CSV
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal virtual void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);


                //一覧に明細行がない場合
                if (this.customDataGridView1.RowCount == 0)
                {
                    //アラートを表示し、CSV出力処理はしない
                    this.Logic.msgLogic.MessageBoxShow("E044");
                }
                else
                {
                    //CSV出力確認メッセージを表示する
                    if (this.Logic.msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                    {
                        //共通部品を利用して、画面に表示されているデータをCSVに出力する
                        CSVExport CSVExport = new CSVExport();
                        CSVExport.ConvertCustomDataGridViewToCsv(this.customDataGridView1, true, true, DENSHU_KBN.GAIBU_RENKEI_GENBA_ICHIRAN.ToTitleString(), this);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func6_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(sender, e);
            }
        }
        #endregion

        #region F7 ｸﾘｱ
        /// <summary>
        /// F7 検索条件クリア
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal virtual void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 条件初期化
            this.txtNum_RenkeiJyoukyou.Clear();
            this.GYOUSHA_CD.Clear();
            this.GYOUSHA_RNAME.Clear();
            this.GENBA_CD.Clear();
            this.GENBA_RNAME.Clear();
            this.GENBA_FURIGANA.Clear();
            this.GENBA_NAME1.Clear();
            this.GENBA_NAME2.Clear();
            this.GENBA_NAME_RYAKU.Clear();
            this.DIGI_POINT_KANA_NAME.Clear();
            this.DIGI_POINT_NAME.Clear();
            this.DIGI_MAP_NAME.Clear();
            this.TEKIYOU_BEGIN.Clear();
            this.TEKIYOU_END.Clear();
            this.beforGyoushaCD = string.Empty;
            this.Logic.SetHyoujiJoukenInit();
            var ds = (DataTable)this.customDataGridView1.DataSource;
            if (ds != null)
            {
                ds.Clear();
                this.customDataGridView1.DataSource = ds;
            }

            GaibuRenkeiGenbaIchiran.Properties.Settings.Default.Save();

            //フィルタの初期化
            this.customSearchHeader1.ClearCustomSearchSetting();

            //並び順の初期化
            this.customSortHeader1.ClearCustomSortSetting();

            //連携状況の初期値
            this.txtNum_RenkeiJyoukyou.Text = "3";

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F8 検索
        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal virtual void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                // パターン未登録の場合検索処理を行わない
                if (this.PatternNo == 0)
                {
                    this.Logic.msgLogic.MessageBoxShow("E057", "パターンが登録", "検索");
                    return;
                }

                // 日付チェック
                if (this.Logic.DateCheck())
                {
                    return;
                }

                this.Logic.Search();

                this.Logic.SaveHyoujiJoukenDefault();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_func8_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region F9 連携登録
        /// <summary>
        /// F9 連携登録
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal virtual void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //送信チェック


            //送信
            int count = this.Logic.JsonConnection(HTTP_METHOD.PUT);
            if (count < 0)
            {
            }
            else if (count == 0)
            {
                this.Logic.msgLogic.MessageBoxShow("C001");
            }
            else
            {
                this.Logic.msgLogic.MessageBoxShow("I001", "連携");

                //処理完了したら再抽出
                this.bt_func8_Click(sender, e);
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F10 並び換え
        /// <summary>
        /// F10 並び替え
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal virtual void bt_func10_Click(object sender, EventArgs e)
        {
            this.customSortHeader1.ShowCustomSortSettingDialog();
        }
        #endregion

        #region F11 ﾌｨﾙﾀ
        /// <summary>
        /// F11 フィルタ
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal virtual void bt_func11_Click(object sender, EventArgs e)
        {
            this.customSearchHeader1.ShowCustomSearchSettingDialog();
        }
        #endregion

        #region F12 閉じる
        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        internal virtual void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.SaveHyoujiJoukenDefault();

            if (this.Logic.parentForm != null)
            {
                this.Logic.parentForm.Close();
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #endregion

        #region サブファンクション押下処理

        /// <summary>
        /// パターン一覧画面へ遷移
        /// </summary>
        internal virtual void bt_process1_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            try
            {
                var sysID = this.OpenPatternIchiran();

                if (!string.IsNullOrEmpty(sysID))
                {
                    this.SetPatternBySysId(sysID);
                    this.ShowData();
                }
                //一覧内のチェックボックスの設定
                this.Logic.HeaderCheckBoxSupport();
            }
            catch (Exception ex)
            {
                LogUtility.Error("bt_process1_Click", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 外部連携CSV
        /// </summary>
        internal virtual void bt_process2_Click(object sender, System.EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.Logic.OutputCSV();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region イベント
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void TEKIYOU_END_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var tekiyouBeginTextBox = this.TEKIYOU_BEGIN;
            var tekiyouEndTextBox = this.TEKIYOU_END;
            tekiyouEndTextBox.Text = tekiyouBeginTextBox.Text;

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// TEKIYOU_BEGIN_Leaveイベント
        /// </summary>
        /// <returns></returns>
        internal void TEKIYOU_BEGIN_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TEKIYOU_END.Text))
            {
                this.TEKIYOU_END.IsInputErrorOccured = false;
                this.TEKIYOU_END.BackColor = Constans.NOMAL_COLOR;
            }
        }

        /// <summary>
        /// TEKIYOU_END_Leaveイベント
        /// </summary>
        /// <returns></returns>
        internal void TEKIYOU_END_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.TEKIYOU_BEGIN.Text))
            {
                this.TEKIYOU_BEGIN.IsInputErrorOccured = false;
                this.TEKIYOU_BEGIN.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region 検索結果表示
        /// <summary>
        /// 検索結果表示処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ShowData()
        {
            if (!this.DesignMode)
            {
                if (this.Table != null && this.PatternNo != 0)
                {
                    // 明細に表示
                    this.logic.CreateDataGridView(this.Table);

                    // 検索件数を設定し、画面に表示
                    var parentForm = base.Parent;

                    this.HideKeyColumns();
                }
            }
        }

        #region 指定した明細列を非表示に
        /// <summary>
        /// 指定した明細列を非表示に
        /// </summary>
        private void HideKeyColumns()
        {
            if (this.customDataGridView1.DataSource != null && this.Table != null)
            {

                //検索時のヘッダーチェックボックス解除処理
                if (this.customDataGridView1.Columns.Contains(ConstCls.CELL_CHECKBOX))
                {
                    DataGridViewCheckBoxHeaderCell header = this.customDataGridView1.Columns[ConstCls.CELL_CHECKBOX].HeaderCell as DataGridViewCheckBoxHeaderCell;
                    if (header != null)
                    {
                        header._checked = false;
                    }

                }

                foreach (DataGridViewColumn col in this.customDataGridView1.Columns)
                {
                    if (col.Name == ConstCls.KEY_ID1 ||
                        col.Name == ConstCls.KEY_ID2 ||
                        //明細の非表示列もここで
                        col.Name == ConstCls.HIDDEN_POINT_ID ||
                        col.Name == ConstCls.HIDDEN_POINT_NAME ||
                        col.Name == ConstCls.HIDDEN_POINT_KANA_NAME ||
                        col.Name == ConstCls.HIDDEN_MAP_NAME ||
                        col.Name == ConstCls.HIDDEN_POST_CODE ||
                        col.Name == ConstCls.HIDDEN_PREFECTURES ||
                        col.Name == ConstCls.HIDDEN_ADDRESS1 ||
                        col.Name == ConstCls.HIDDEN_ADDRESS2 ||
                        col.Name == ConstCls.HIDDEN_TEL_NO ||
                        col.Name == ConstCls.HIDDEN_FAX_NO ||
                        col.Name == ConstCls.HIDDEN_CONTACT_NAME ||
                        col.Name == ConstCls.HIDDEN_MAIL_ADDRESS ||
                        col.Name == ConstCls.HIDDEN_RANGE_RADIUS ||
                        col.Name == ConstCls.HIDDEN_REMARKS
                        )
                    {
                        col.Visible = false;
                    }
                    //ついでにチェックボックスのReadOnlyを解除
                    if (col.Name == ConstCls.CELL_CHECKBOX)
                    {
                        col.ReadOnly = false;
                    }

                }

                //更についでに外部連携現場保守未入力データはチェックボックスを入力不可に
                foreach (DataGridViewRow row in this.customDataGridView1.Rows)
                {
                    if (row.Cells[ConstCls.HIDDEN_POINT_ID].Value.ToString() == string.Empty)
                    {
                        row.Cells[ConstCls.CELL_CHECKBOX].ReadOnly = true;
                    }
                }
            }
        }
        #endregion

        #endregion

        #region 画面を閉じる
        /// <summary>
        /// 画面クローズ
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);

            try
            {
                // 表示条件保存
                this.Logic.SaveHyoujiJoukenDefault();
            }
            catch (Exception ex)
            {
                // 画面が閉じれなくなるのでログのみ
                LogUtility.Fatal("OnClosing", ex);
            }
        }
        #endregion

        #region 抽出条件変更時処理
        /// <summary>
        /// 一覧表示条件チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ICHIRAN_HYOUJI_JOUKEN_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox item = (CheckBox)sender;
            if (!item.Checked)
            {
                if (!this.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    this.Logic.msgLogic.MessageBoxShow("E001", "表示条件");
                    item.Checked = true;
                }
            }
        }

        private void GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 業者が入力されてない場合
            if (String.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                // 関連項目クリア
                this.GYOUSHA_CD.Text = string.Empty;
                this.GYOUSHA_RNAME.Text = String.Empty;
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_RNAME.Text = String.Empty;
                this.beforGyoushaCD = string.Empty;
            }
            else if (this.beforGyoushaCD != this.GYOUSHA_CD.Text)
            {
                this.GENBA_CD.Text = String.Empty;
                this.GENBA_RNAME.Text = String.Empty;
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }
        }

        /// <summary>
        /// 業者 PopupBeforeExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupBeforeExecuteMethod()
        {
            preGyoushaCd = this.GYOUSHA_CD.Text;
        }

        /// <summary>
        /// 業者 PopupAfterExecuteMethod
        /// </summary>
        public void GYOUSHA_PopupAfterExecuteMethod()
        {
            curGyoushaCd = this.GYOUSHA_CD.Text;
            if (preGyoushaCd != curGyoushaCd)
            {
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_RNAME.Text = string.Empty;
            }
        }
        #endregion

        #region 現場一覧Shownイベント
        /// <summary>
        /// 現場一覧のShownイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenbaIchiranForm_Shown(object sender, EventArgs e)
        {
            // 初期フォーカス位置を設定します
            this.txtNum_RenkeiJyoukyou.Focus();
        }
        #endregion

        #region テンポラリー変数に業者CDをセットする

        /// <summary>
        /// 現場CDValidatingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //beforGyoushaCDは値がない場合に値をセットします。
            if (string.IsNullOrEmpty(this.beforGyoushaCD))
            {
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }

            if (string.IsNullOrEmpty(this.GENBA_CD.Text))
            {
                this.GENBA_RNAME.Text = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(this.GYOUSHA_CD.Text))
            {
                this.Logic.msgLogic.MessageBoxShow("E051", "業者");
                this.GENBA_CD.Text = string.Empty;
                this.GENBA_CD.Focus();
                return;
            }

            this.Logic.CheckGenba();
        }

        public void AfterPopupGenba()
        {
            //beforGyoushaCDは値がない場合に値をセットします。
            if (string.IsNullOrEmpty(this.beforGyoushaCD))
            {
                this.beforGyoushaCD = this.GYOUSHA_CD.Text;
            }
        }

        #endregion

    }
}

