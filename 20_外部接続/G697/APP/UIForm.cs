using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Utility;
using Seasar.Quill;

namespace Shougun.Core.ExternalConnection.HaisouKeikakuTeiki
{
    /// <summary>
    /// 配送計画一覧
    /// </summary>
    public partial class UIForm : SuperForm
    {
        #region フィールド
        /// <summary>
        /// 画面ロジック
        /// </summary>
        private HaisouKeikakuTeiki.LogicClass logic = null;

        /// <summary>
        /// 表示されたフラグ
        /// </summary>
        private bool isShown = false;

        /// <summary>
        /// 前回業者コード
        /// </summary>
        private string beforGyoushaCD = string.Empty;

        private string preGyoushaCd { get; set; }
        private string curGyoushaCd { get; set; }

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public UIForm()
            : base(WINDOW_ID.T_HAISOU_KEIKAKU_TEIKI)
        {
            this.InitializeComponent();

            // 画面タイプなど引数値は変更となるが基本的にやることは変わらない
            this.logic = new LogicClass(this);

            //完全に固定。ここには変更を入れない
            QuillInjector.GetInstance().Inject(this);
        }
        #endregion

        #region 画面Load処理
        /// <summary>
        /// 画面ロード
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // 区分
            this.txtNum_RenkeiTaishou.Text = "1";

            // 初期化、初期表示
            if (!this.logic.WindowInit()) { return; }

            // 初期フォーカス位置を設定します
            this.txtNum_RenkeiJyoukyou.Focus();

            // Anchorの設定は必ずOnLoadで行うこと
            if (this.Ichiran1 != null)
            {
                this.Ichiran1.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            }
        }

        /// <summary>
        /// 初回表出イベント
        /// </summary>
        /// <param name="e"></param>
        protected override void OnShown(EventArgs e)
        {
            // この画面を最大化したくない場合は下記のように
            // OnShownでWindowStateをNomalに指定する
            //this.ParentForm.WindowState = FormWindowState.Normal;

            if (!isShown)
            {
                this.Height -= 7;
                isShown = true;
            }

            base.OnShown(e);
        }
        #endregion

        #region ファンクションボタンのイベント

        #region F1 一括設定
        /// <summary>
        /// 一括設定(F1)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.IkkatsuSettei();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F4 削除
        /// <summary>
        /// 削除(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func4_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ButtonEnabledFalse();

            // 登録前チェック
            if (!this.logic.RegistChk(true))
            {
                this.logic.ButtonEnabledControl();
                return;
            }

            // DTOにセット
            this.logic.SetNaviCheckDetail();

            // 登録用Entityの作成
            this.logic.CreateEntity("DEL", 2);

            // CSV作成処理
            string filepath = this.logic.OutputCSV("DEL");
            if (string.IsNullOrEmpty(filepath))
            {
                // 出力失敗
                this.logic.ButtonEnabledControl();
                return;
            }

            // API通信処理(登録)
            int pid = this.logic.RegistAPI(filepath);
            if (pid == 0)
            {
                // API通信失敗
                this.logic.ButtonEnabledControl();
                return;
            }

            // API通信が成功したら一時テーブルを削除
            bool result = this.logic.NaviEventRegist("DEL", pid);
            if (!result)
            {
                this.logic.ButtonEnabledControl();
                return;
            }

            // DB登録
            result = this.logic.DeleteData();
            if (result)
            {
                this.logic.msgLogic.MessageBoxShow("I001", "削除");
            }
            else
            {
                // 登録失敗
                this.logic.ButtonEnabledControl();
                return;
            }

            // 抽出
            this.logic.Search();
            this.logic.ButtonEnabledControl();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F7 条件ｸﾘｱ
        /// <summary>
        /// 条件クリア(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func7_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.SetInitialRenkeiCondition();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F8 検索
        /// <summary>
        /// 検索(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func8_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 抽出
            int count = this.logic.Search();

            if (count == 0)
            {
                this.logic.msgLogic.MessageBoxShow("C001");
                return;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F9 データ連携
        /// <summary>
        /// データ連携(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            this.logic.ButtonEnabledFalse();

            // 登録前チェック
            if (!this.logic.RegistChk(false))
            {
                this.logic.ButtonEnabledControl();
                return;
            }

            // DTOにセット
            this.logic.SetNaviCheckDetail();

            // 送信済みデータのチェック
            if (!this.logic.RegistUserChk())
            {
                this.logic.ButtonEnabledControl();
                return;
            }

            // 登録用Entityの作成
            if (this.logic.renkei_kbn == 3)
            {
                this.logic.CreateEntity("UPD", 2);
            }
            else
            {
                this.logic.CreateEntity("INS", 2);
            }
            // CSV作成処理
            string filepath = this.logic.OutputCSV("INS");
            if (string.IsNullOrEmpty(filepath))
            {
                // 出力失敗
                this.logic.ButtonEnabledControl();
                return;
            }

            // API通信処理(登録)
            int pid = this.logic.RegistAPI(filepath);
            if (pid == 0)
            {
                // API通信失敗
                this.logic.ButtonEnabledControl();
                return;
            }

            // API通信が成功したらProcessingIdを保存
            bool result = this.logic.NaviEventRegist("INS", pid);
            if (!result)
            {
                this.logic.ButtonEnabledControl();
                return;
            }

            // DB登録
            if (this.logic.renkei_kbn == 3)
            {
                result = this.logic.RegistData("UPD");
            }
            else
            {
                result = this.logic.RegistData("INS");
            }
            if (result)
            {
                this.logic.msgLogic.MessageBoxShow("I001", "連携");
            }
            else
            {
                // 登録失敗
                this.logic.ButtonEnabledControl();
                return;
            }

            // 抽出
            this.logic.Search();
            this.logic.ButtonEnabledControl();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region F12 閉じる
        /// <summary>
        /// 閉じる(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal virtual void bt_func12_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //処理
            var parentForm = (BusinessBaseForm)this.Parent;

            this.Ichiran1.DataSource = "";

            this.Close();
            parentForm.Close();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region subF1 コース最適化
        /// <summary>
        /// コース最適化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            DataGridViewCell cell = this.Ichiran1.CurrentCell;
            if (cell != null)
            {
                int index = cell.RowIndex;

                // 画面引数取得
                long systemId = Convert.ToInt64(this.Ichiran1.Rows[index].Cells["DATA_SYSTEM_ID"].Value);

                // TODO:データ存在チェック

                bool returnFlg;
                // API通信処理(確認)
                string result = this.logic.CheckAPI(index, out returnFlg);
                if (result != string.Empty)
                {
                    var sb = new StringBuilder();
                    sb.Append(result);
                    // アラート用フォームを開く
                    Form1.ShowMsgForm(sb);
                    return;
                }
                if (returnFlg)
                {
                    return;
                }

                // コース最適化入力を開く
                r_framework.FormManager.FormManager.OpenFormWithAuth("G698", WINDOW_TYPE.UPDATE_WINDOW_FLAG, systemId);
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #endregion

        #region 各項目のイベント処理

        #region 連携変更時の処理
        /// <summary>
        /// 連携が変更された時の処理
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal void txtNum_RenkeiJyoukyou_TextChanged(object sender, EventArgs e)
        {
            // 連携の値を変数にセット
            if (this.txtNum_RenkeiJyoukyou.Text == string.Empty)
            {
                this.logic.renkei_kbn = 0;
            }
            else
            {
                this.logic.renkei_kbn = int.Parse(this.txtNum_RenkeiJyoukyou.Text);
            }
            this.logic.ButtonEnabledControl();
        }

        /// <summary>
        /// 連携未入力不許可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtNum_RenkeiJyoukyou_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_RenkeiJyoukyou.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.txtNum_RenkeiJyoukyou.Focus();
                this.txtNum_RenkeiJyoukyou.Text = "1";
                this.logic.msgLogic.MessageBoxShow("W001", "1", "3");
            }
        }
        #endregion

        #region 最適化対象
        /// <summary>
        /// 最適化対象が変更された時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtNum_RenkeiTaishou_TextChanged(object sender, EventArgs e)
        {
            // 連携の値を変数にセット
            if (this.txtNum_RenkeiTaishou.Text == string.Empty)
            {
                this.logic.renkei_taishou = 0;
            }
            else
            {
                this.logic.renkei_taishou = int.Parse(this.txtNum_RenkeiTaishou.Text);
            }
            this.COURSE_NAME_CD.Enabled = false;

            this.logic.ButtonEnabledControl();
        }

        /// <summary>
        /// 最適化対象未入力不許可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void txtNum_RenkeiTaishou_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNum_RenkeiTaishou.Text))
            {
                //警告メッセージを表示して、フォーカス移動しない
                this.txtNum_RenkeiTaishou.Focus();
                this.txtNum_RenkeiTaishou.Text = "1";
                this.logic.msgLogic.MessageBoxShow("W001", "1", "2");
            }
        }
        #endregion

        #region 曜日
        /// <summary>
        /// 曜日未入力不許可
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DAY_CD_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.DAY_CD.Text))
            {
                this.DAY_CD.Focus();
                this.DAY_CD.Text = "8";
                this.logic.msgLogic.MessageBoxShow("W001", "1", "3");
            }
        }
        #endregion

        #region コース
        /// <summary>
        /// コースの存在チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void COURSE_NAME_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                // キャンセルボタン or 未入力時は何もしない
                if (null != this.CancelButton && this.ActiveControl == this.CancelButton || this.COURSE_NAME_CD.Text.Trim().Length == 0)
                {
                    this.COURSE_NAME_RYAKU.Clear();
                    return;
                }

                var shortName = this.logic.mCourseNameAll.Where(s => s.COURSE_NAME_CD == this.COURSE_NAME_CD.Text.Trim()).Select(s => s.COURSE_NAME_RYAKU).FirstOrDefault();
                if (shortName == null)
                {
                    this.COURSE_NAME_CD.IsInputErrorOccured = true;
                    e.Cancel = true;

                    //レコードが存在しない
                    this.logic.msgLogic.MessageBoxShow("E020", "コース");
                    this.COURSE_NAME_CD.SelectAll();
                    this.COURSE_NAME_RYAKU.Clear();
                }
                else
                {
                    this.COURSE_NAME_RYAKU.Text = shortName;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("COURSE_NAME_CD_Validating", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region Validating
        /// <summary>
        /// 出発業者チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUPPATSU_GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 業者が入力されてない場合
            if (string.IsNullOrEmpty(this.SHUPPATSU_GYOUSHA_CD.Text))
            {
                // 関連項目クリア
                this.SHUPPATSU_GYOUSHA_CD.Text = string.Empty;
                this.SHUPPATSU_GYOUSHA_NAME.Text = string.Empty;
                this.SHUPPATSU_GENBA_CD.Text = string.Empty;
                this.SHUPPATSU_GENBA_NAME.Text = string.Empty;
                this.beforGyoushaCD = string.Empty;
                return;
            }
            else if (this.beforGyoushaCD != this.SHUPPATSU_GYOUSHA_CD.Text)
            {
                this.SHUPPATSU_GENBA_CD.Text = string.Empty;
                this.SHUPPATSU_GENBA_NAME.Text = string.Empty;
                this.beforGyoushaCD = this.SHUPPATSU_GYOUSHA_CD.Text;
            }
            this.logic.CheckShuppatsuGyousha();
        }

        /// <summary>
        /// 出発現場チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUPPATSU_GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //beforGyoushaCDは値がない場合に値をセットします。
            if (string.IsNullOrEmpty(this.beforGyoushaCD))
            {
                this.beforGyoushaCD = this.SHUPPATSU_GYOUSHA_CD.Text;
            }

            if (string.IsNullOrEmpty(this.SHUPPATSU_GENBA_CD.Text))
            {
                this.SHUPPATSU_GENBA_NAME.Text = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(this.SHUPPATSU_GYOUSHA_CD.Text))
            {
                this.logic.msgLogic.MessageBoxShow("E051", "業者");
                this.SHUPPATSU_GENBA_CD.Text = string.Empty;
                this.SHUPPATSU_GENBA_CD.Focus();
                return;
            }

            this.logic.CheckShuppatsuGenba();
        }


        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 業者が入力されてない場合
            if (string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text))
            {
                // 関連項目クリア
                this.NIOROSHI_GYOUSHA_CD.Text = string.Empty;
                this.NIOROSHI_GYOUSHA_NAME.Text = string.Empty;
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.beforGyoushaCD = string.Empty;
                return;
            }
            else if (this.beforGyoushaCD != this.NIOROSHI_GYOUSHA_CD.Text)
            {
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.beforGyoushaCD = this.NIOROSHI_GYOUSHA_CD.Text;
            }
            this.logic.CheckNioroshiGyousha();
        }

        /// <summary>
        /// 荷降現場チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_GENBA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //beforGyoushaCDは値がない場合に値をセットします。
            if (string.IsNullOrEmpty(this.beforGyoushaCD))
            {
                this.beforGyoushaCD = this.NIOROSHI_GYOUSHA_CD.Text;
            }

            if (string.IsNullOrEmpty(this.NIOROSHI_GENBA_CD.Text))
            {
                this.NIOROSHI_GENBA_NAME.Text = string.Empty;
                return;
            }

            if (string.IsNullOrEmpty(this.NIOROSHI_GYOUSHA_CD.Text))
            {
                this.logic.msgLogic.MessageBoxShow("E051", "業者");
                this.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.NIOROSHI_GENBA_CD.Focus();
                return;
            }

            this.logic.CheckNioroshiGenba();
        }

        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHARYOU_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.ChechSharyouCd();
        }

        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UNPAN_GYOUSHA_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.CheckUnpanGyoushaCd();
        }

        /// <summary>
        /// 運転者チェック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHAIN_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.logic.CheckUntenshaCd();
        }

        /// <summary>
        /// 出発時間Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SHUPPATSU_TIME_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string value = Convert.ToString(this.SHUPPATSU_TIME.Text);
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            // 時間の入力チェック
            string ret = this.logic.IsTimeChkOKForm(value);
            if (ret == string.Empty)
            {
                e.Cancel = true;
            }
            else
            {
                this.SHUPPATSU_TIME.Text = ret;
                this.NIOROSHI_TIME.Text = string.Empty;
            }
        }

        /// <summary>
        /// 荷降時間Validating
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NIOROSHI_TIME_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string value = Convert.ToString(this.NIOROSHI_TIME.Text);
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            // 時間の入力チェック
            string ret = this.logic.IsTimeChkOKForm(value);
            if (ret == string.Empty)
            {
                e.Cancel = true;
            }
            else
            {
                this.NIOROSHI_TIME.Text = ret;
                this.SHUPPATSU_TIME.Text = string.Empty;
            }
        }
        #endregion

        #region CellValidating
        /// <summary>
        /// 各セル毎のValidating処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var cell = this.Ichiran1.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (e.RowIndex >= 0)
            {
                if (this.Ichiran1.Columns[this.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("DATA_DEPARTURE_TIME"))
                {
                    #region 出発時刻
                    // 出発時刻入力時、荷降時刻をクリアする
                    if (!string.IsNullOrEmpty(Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_DEPARTURE_TIME"].Value)))
                    {
                        // 時間の入力チェック
                        if (!this.logic.IsTimeChkOKDGV(cell))
                        {
                            e.Cancel = true;
                            this.Ichiran1.BeginEdit(false);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_ARRIVAL_TIME"].Value)))
                        {
                            this.Ichiran1.Rows[e.RowIndex].Cells["DATA_ARRIVAL_TIME"].Value = string.Empty;
                        }
                    }
                    #endregion
                }
                else if (this.Ichiran1.Columns[this.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("DATA_ARRIVAL_TIME"))
                {
                    #region 荷降時刻
                    // 荷降時刻入力時、出発時刻をクリアする
                    if (this.Ichiran1.Rows[e.RowIndex].Cells["DATA_ARRIVAL_TIME"].Value != null && !string.IsNullOrEmpty(Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_ARRIVAL_TIME"].Value)))
                    {
                        // 時間の入力チェック
                        if (!this.logic.IsTimeChkOKDGV(cell))
                        {
                            e.Cancel = true;
                            this.Ichiran1.BeginEdit(false);
                        }
                        if (!string.IsNullOrEmpty(Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_DEPARTURE_TIME"].Value)))
                        {
                            this.Ichiran1.Rows[e.RowIndex].Cells["DATA_DEPARTURE_TIME"].Value = string.Empty;
                        }
                    }
                    #endregion
                }
                else if (this.Ichiran1.Columns[this.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("DATA_SHUPPATSU_GYOUSHA_CD"))
                {
                    #region 出発業者
                    // 出発業者手入力時
                    string gyoushaCd = Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SHUPPATSU_GYOUSHA_CD"].Value);
                    if (gyoushaCd == string.Empty)
                    {
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SHUPPATSU_GENBA_CD"].Value = string.Empty;
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SHUPPATSU_GENBA_NAME"].Value = string.Empty;
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SHUPPATSU_EIGYOUSHO_CD"].Value = string.Empty;
                        return;
                    }

                    var gyoushaName = this.logic.naviShuppatsuGenbaDao.GetStringDataByGyoushaName(gyoushaCd);
                    if (gyoushaName != null && 0 < gyoushaName.Length)
                    {
                        //名称なし
                    }
                    else
                    {
                        this.logic.msgLogic.MessageBoxShow("E020", "業者");
                        e.Cancel = true;
                    }
                    #endregion
                }
                else if (this.Ichiran1.Columns[this.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("DATA_SHUPPATSU_GENBA_CD"))
                {
                    #region 出発現場
                    // 出発現場手入力時
                    string gyoushaCd = Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SHUPPATSU_GYOUSHA_CD"].Value);
                    string genbaCd = Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SHUPPATSU_GENBA_CD"].Value);
                    if (genbaCd == string.Empty)
                    {
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SHUPPATSU_GENBA_NAME"].Value = string.Empty;
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SHUPPATSU_EIGYOUSHO_CD"].Value = string.Empty;
                        return;
                    }

                    var genbaName = this.logic.naviShuppatsuGenbaDao.GetStringDataByGenbaName(gyoushaCd, genbaCd);
                    if (genbaName != null && 0 < genbaName.Length)
                    {
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SHUPPATSU_GENBA_NAME"].Value = genbaName;
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SHUPPATSU_EIGYOUSHO_CD"].Value = this.logic.naviShuppatsuGenbaDao.GetStringDataByCd(gyoushaCd, genbaCd);
                    }
                    else
                    {
                        this.logic.msgLogic.MessageBoxShow("E020", "現場");
                        e.Cancel = true;
                    }
                    #endregion
                }
                else if (this.Ichiran1.Columns[this.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("DATA_NIOROSHI_GYOUSHA_CD"))
                {
                    #region 荷降業者
                    // 荷降業者手入力時
                    string gyoushaCd = Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_NIOROSHI_GYOUSHA_CD"].Value);
                    if (gyoushaCd == string.Empty)
                    {
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_NIOROSHI_GENBA_CD"].Value = string.Empty;
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_NIOROSHI_GENBA_NAME"].Value = string.Empty;
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_NIOROSHI_EIGYOUSHO_CD"].Value = string.Empty;
                        return;
                    }

                    var gyoushaName = this.logic.naviShuppatsuGenbaDao.GetStringDataByGyoushaName(gyoushaCd);
                    if (gyoushaName != null && 0 < gyoushaName.Length)
                    {
                        //名称なし
                    }
                    else
                    {
                        this.logic.msgLogic.MessageBoxShow("E020", "業者");
                        e.Cancel = true;
                    }
                    #endregion
                }
                else if (this.Ichiran1.Columns[this.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("DATA_NIOROSHI_GENBA_CD"))
                {
                    #region 荷降現場
                    // 荷降現場手入力時
                    string gyoushaCd = Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_NIOROSHI_GYOUSHA_CD"].Value);
                    string genbaCd = Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_NIOROSHI_GENBA_CD"].Value);
                    if (genbaCd == string.Empty)
                    {
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_NIOROSHI_GENBA_NAME"].Value = string.Empty;
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_NIOROSHI_EIGYOUSHO_CD"].Value = string.Empty;
                        return;
                    }

                    var genbaName = this.logic.naviShuppatsuGenbaDao.GetStringDataByGenbaName(gyoushaCd, genbaCd);
                    if (genbaName != null && 0 < genbaName.Length)
                    {
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_NIOROSHI_GENBA_NAME"].Value = genbaName;
                        this.Ichiran1.Rows[e.RowIndex].Cells["DATA_NIOROSHI_EIGYOUSHO_CD"].Value = this.logic.naviShuppatsuGenbaDao.GetStringDataByCd(gyoushaCd, genbaCd);
                    }
                    else
                    {
                        this.logic.msgLogic.MessageBoxShow("E020", "現場");
                        e.Cancel = true;
                    }
                    #endregion
                }
                else if (this.Ichiran1.Columns[this.Ichiran1.CurrentCell.ColumnIndex].Name.Equals("DATA_SAGYOUSHA_CD"))
                {
                    #region 作業者CD
                    // 作業者CD手入力時
                    string naviShainCd = Convert.ToString(this.Ichiran1.Rows[e.RowIndex].Cells["DATA_SAGYOUSHA_CD"].Value);
                    if (naviShainCd == string.Empty)
                    {
                        return;
                    }
                    else
                    {
                        var cnt = this.logic.naviShainDao.GetDataNaviCd(naviShainCd);
                        if (cnt < 1)
                        {
                            this.logic.msgLogic.MessageBoxShow("E020", "NAVITIMEマスタ連携");
                            e.Cancel = true;
                        }
                    }
                    #endregion
                }
            }
        }
        #endregion

        #region Enter
        private void SHUPPATSU_TIME_Enter(object sender, EventArgs e)
        {
            // 「:」を削除
            this.SHUPPATSU_TIME.Text = Convert.ToString(this.SHUPPATSU_TIME.Text).Replace(":", string.Empty);
        }

        private void NIOROSHI_TIME_Enter(object sender, EventArgs e)
        {
            // 「:」を削除
            this.NIOROSHI_TIME.Text = Convert.ToString(this.NIOROSHI_TIME.Text).Replace(":", string.Empty);
        }
        #endregion

        #region CellEnter
        /// <summary>
        /// 入力可能の場合に「:」を削除する処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ichiran1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0)
                {
                    return;
                }

                if (this.Ichiran1.Columns["DATA_DEPARTURE_TIME"].Index == e.ColumnIndex && !this.Ichiran1[e.ColumnIndex, e.RowIndex].ReadOnly)
                {
                    // 入力可能な場合「:」を削除
                    this.Ichiran1[e.ColumnIndex, e.RowIndex].Value = Convert.ToString(this.Ichiran1[e.ColumnIndex, e.RowIndex].Value).Replace(":", string.Empty);
                }

                if (this.Ichiran1.Columns["DATA_ARRIVAL_TIME"].Index == e.ColumnIndex && !this.Ichiran1[e.ColumnIndex, e.RowIndex].ReadOnly)
                {
                    // 入力可能な場合「:」を削除
                    this.Ichiran1[e.ColumnIndex, e.RowIndex].Value = Convert.ToString(this.Ichiran1[e.ColumnIndex, e.RowIndex].Value).Replace(":", string.Empty);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailIchiran_CellEnter", ex);
                throw ex;
            }

        }
        #endregion

        #region 作業日
        /// <summary>
        /// 作業日更新時、該当する曜日を自動セットする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SAGYOU_DATE_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // 未入力
            if (string.IsNullOrEmpty(this.SAGYOU_DATE.Text))
            {
                return;
            }

            // コースの場合のみ
            if (this.logic.renkei_taishou == 2)
            {
                return;
            }
            // 独自定義(日:7 月:1 火:2 水:3 木:4 金:5 土:6)
            string[] daysList = { "7", "1", "2", "3", "4", "5", "6" };
            var theDay = Convert.ToDateTime(this.SAGYOU_DATE.Text);
            DayOfWeek dow = theDay.DayOfWeek;

            // 曜日CDにセット
            this.DAY_CD.Text = daysList[(int)dow];
        }
        #endregion

        #endregion

        #region 明細でスペース押下時の処理
        private void Ichiran1_KeyDown(object sender, KeyEventArgs e)
        {
            this.logic.CheckPopup(e);
        }

        private void Ichiran1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            e.Control.KeyDown -= this.Ichiran1_KeyDown;
            e.Control.KeyDown += this.Ichiran1_KeyDown;
        }
        #endregion

    }
}
