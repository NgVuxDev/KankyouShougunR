using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Message;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Shougun.Core.Common.BusinessCommon.Dao;
using r_framework.Dao;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Billing.GetsujiShori
{
    internal class LogicClass : IBuisinessLogic
    {
        #region Const

        /// <summary>[定数] 締状態 - 締済/// </summary>
        private const string SHIME_STATUS_SHIMEZUMI = "締済";
        /// <summary>[定数] 締状態 - 未締/// </summary>
        private const string SHIME_STATUS_MISHIME = "未締";

        #endregion

        #region Field

        /// <summary>ヘッダフォーム</summary>
        private UIHeader headerForm;
        /// <summary>メインフォーム</summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>月次締処理のDAO</summary>
        private IT_GETSUJI_SHORIDao GetsujiShoriDao;
        /// <summary>売上月次締処理テーブルのDAO</summary>
        private IT_MONTHLY_LOCK_URDao MonthlyLockUrDao;
        /// <summary>支払月次締処理テーブルのDAO</summary>
        private IT_MONTHLY_LOCK_SHDao MonthlyLockShDao;
        /// <summary>在庫月次締処理テーブルのDAO</summary>
        private IT_MONTHLY_LOCK_ZAIKODao MonthlyLockZaikoDao;
        /// <summary>売上月次調整テーブルのDAO</summary>
        private IT_MONTHLY_ADJUST_URDao MonthlyAdjustUrDao;
        /// <summary>支払月次調整テーブルのDAO</summary>
        private IT_MONTHLY_ADJUST_SHDao MonthlyAdjustShDao;
        /// <summary>月次処理中用DAO</summary>
        private T_GETSUJI_SHORI_CHUDao getsujiShoriChuDao;

        /// <summary>月次処理中フラグ(月次処理中の他動作防止用)</summary>
        private bool isGetsujiProcess = false;

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        private MessageBoxShowLogic errmessage;

        /// <summary>
        /// 月次処理の実行区分[1:旧月次処理/2:適格対応月次処理]
        /// </summary>
        internal int OldInvoiceKBN;

        /// <summary>
        /// システム設定-請求タブ：旧請求書印刷「1.する」「2.しない」
        /// </summary>
        internal string OldSeikyuuPrintKBN;

        #endregion

        #region Constructor

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="header">ヘッダフォーム</param>
        /// <param name="form">メインフォーム</param>
        public LogicClass(UIHeader header, UIForm form)
        {
            LogUtility.DebugMethodStart(header, form);

            this.headerForm = header;
            this.form = form;

            this.GetsujiShoriDao = DaoInitUtility.GetComponent<IT_GETSUJI_SHORIDao>();
            this.MonthlyLockUrDao = DaoInitUtility.GetComponent<IT_MONTHLY_LOCK_URDao>();
            this.MonthlyLockShDao = DaoInitUtility.GetComponent<IT_MONTHLY_LOCK_SHDao>();
            this.MonthlyAdjustUrDao = DaoInitUtility.GetComponent<IT_MONTHLY_ADJUST_URDao>();
            this.MonthlyAdjustShDao = DaoInitUtility.GetComponent<IT_MONTHLY_ADJUST_SHDao>();
            this.MonthlyLockZaikoDao = DaoInitUtility.GetComponent<IT_MONTHLY_LOCK_ZAIKODao>();
            this.getsujiShoriChuDao = DaoInitUtility.GetComponent<T_GETSUJI_SHORI_CHUDao>();

            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            this.errmessage = new MessageBoxShowLogic();

            DBAccessor accessor = new DBAccessor();
            M_SYS_INFO sysInfo = accessor.GetSysInfo();
            this.OldSeikyuuPrintKBN = sysInfo.OLD_SEIKYUU_PRINT_KBN.ToString();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region Method

        #region 画面初期表示

        /// <summary>
        /// 画面情報の初期化を行います
        /// </summary>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                this.ButtonInit();
                this.EventInit();
                this.SetInitDisplayValue();
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #region ボタン設定初期化

        /// <summary>
        /// ボタン初期化処理を行います
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            // 締処理中テーブルに請求締め処理中のデータがあればF10を表示
            var table = this.getsujiShoriChuDao.GetAllData();

            parentForm.bt_func10.Enabled = (!table.Rows.Count.Equals(0));
            parentForm.bt_func10.Text = parentForm.bt_func10.Enabled ? parentForm.bt_func10.Text : string.Empty;
            
            //旧月次処理実行ボタン
            if (this.OldSeikyuuPrintKBN != "1")
            {
                parentForm.bt_process5.Enabled = false;
                parentForm.bt_process5.Text = string.Empty;
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込を行います
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            Type cType = this.GetType();
            string strButtonInfoXmlPath = cType.Namespace;
            strButtonInfoXmlPath += ".Setting.ButtonSetting.xml";
            LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath));

            return buttonSetting.LoadButtonSetting(thisAssembly, strButtonInfoXmlPath);
        }

        #endregion

        #region イベント初期化

        /// <summary>
        /// イベント処理の初期化を行います
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (BusinessBaseForm)this.form.Parent;
            parentForm.bt_func1.Click += new EventHandler(bt_func1_Click);          //前月
            parentForm.bt_func2.Click += new EventHandler(bt_func2_Click);          //次月
            parentForm.bt_func4.Click += new EventHandler(bt_func4_Click);          //締解除
            parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);          //実行
            parentForm.bt_func10.Click += new EventHandler(bt_func10_Click);        //ﾛｯｸ解除
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);        //閉じる
            parentForm.bt_process1.Click += new EventHandler(bt_process1_Click);    //月次消費税調整(売上)
            parentForm.bt_process2.Click += new EventHandler(bt_process2_Click);    //月次消費税調整(支払)
            if (this.OldSeikyuuPrintKBN == "1")
            {
                parentForm.bt_process5.Click += new EventHandler(bt_process5_Click);    //旧月次処理
            }
            parentForm.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期値設定

        /// <summary>
        /// 画面表示項目の初期値を設定します
        /// </summary>
        private void SetInitDisplayValue()
        {
            /* 月次年月、締状態を設定                                  */
            /* 保存データから最新年月のデータを売上/支払ともに取得し、 */
            /* 最新の年月+1ヶ月を月次年月に表示                        */

            int year = 0;
            int month = 0;
            this.GetLatestGetsujiDate(ref year, ref month);

            if (year != 0 && month != 0)
            {
                // 月次年月は年月までの表示のため日付は1日を指定
                DateTime date = new DateTime(year, month, 1);

                this.form.GETSUJI_DATE.Value = date.AddMonths(1);
                this.form.SHIME_STATUS.Text = SHIME_STATUS_MISHIME;
            }
            else
            {
                // データが無い場合は実行時の年月を使用
                DateTime date = new DateTime(this.parentForm.sysDate.Year, this.parentForm.sysDate.Month, 1);
                this.form.GETSUJI_DATE.Value = date;
                this.form.SHIME_STATUS.Text = SHIME_STATUS_MISHIME;
            }
        }

        #endregion

        #endregion

        #region FunctionButtonクリックイベント

        #region [F1]前月

        /// <summary>
        /// [F1]前月ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 月次処理中
            if (this.isGetsujiProcess)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            DateTime dateTime = DateTime.Parse(this.form.GETSUJI_DATE.Value.ToString()).AddMonths(-1);
            this.form.GETSUJI_DATE.Value = dateTime;

            // 締状態更新
            this.UpdateShimeStatus();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [F2]次月

        /// <summary>
        /// [F2]次月ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 月次処理中
            if (this.isGetsujiProcess)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            DateTime dateTime = DateTime.Parse(this.form.GETSUJI_DATE.Value.ToString()).AddMonths(1);
            this.form.GETSUJI_DATE.Value = dateTime;

            // 締状態更新
            this.UpdateShimeStatus();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [F4]締解除

        /// <summary>
        /// [F4]締解除ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // 月次処理中
                if (this.isGetsujiProcess)
                {
                    return;
                }

                // 実行確認アラート
                DialogResult result = MessageBoxUtility.MessageBoxShow("C046", "解除");
                if (result != DialogResult.Yes)
                {
                    return;
                }

                /* 別ユーザが月次処理中かのチェック */
                GetsujiShoriCheckLogicClass checkLogic = new GetsujiShoriCheckLogicClass();
                if (checkLogic.CheckGetsujiShoriChu())
                {
                    MessageBoxUtility.MessageBoxShow("E224", "締解除");
                    return;
                }

                /* 事前に最新締済年月かのチェックを行う */
                /* チェックOKなら論理削除を行う         */
                string errMsg = string.Empty;
                using (Transaction tran = new Transaction())
                {
                    /* 最新締年月かのチェック */
                    int chkYear = 0;
                    int chkMonth = 0;
                    this.GetLatestGetsujiDate(ref chkYear, ref chkMonth);

                    DateTime getsujiDate = DateTime.Parse(this.form.GETSUJI_DATE.Value.ToString());
                    if (chkYear == 0 && chkMonth == 0)
                    {
                        // 月次データが無いのでエラー
                        errMsg = "月次処理が一度も行われていないため実行出来ません。";
                    }
                    else if(getsujiDate.Year != chkYear || getsujiDate.Month != chkMonth)
                    {
                        // 現在表示されてる月次年月が最新ではないのでエラー
                        errMsg = "最新の月次年月ではないため実行出来ません。";
                    }
                    else
                    {
                        // 論理削除実行
                        this.LogicalDelete();
                        tran.Commit();
                    }
                }

                if(string.IsNullOrEmpty(errMsg))
                {
                    // 完了メッセージ
                    MessageBoxUtility.MessageBoxShow("I001", "締解除");

                    // 締状況更新
                    this.form.SHIME_STATUS.Text = SHIME_STATUS_MISHIME;
                }
                else
                {
                    // エラーメッセージ
                    MessageBoxUtility.MessageBoxShowError(errMsg);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region [F9]実行

        /// <summary>
        /// [F9]実行ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            //旧請求締処理
            OldInvoiceKBN = 2;

            NewOld_invoice(sender, e);
        }

        #endregion

        #region ﾛｯｸ解除イベント
        /// <summary>
        /// 月次処理中に強制終了してしまった場合、月次処理中テーブルをクリア出来るようにする
        /// </summary>
        /// <param name="sender">イベント呼び出し元オブジェクト</param>
        /// <param name="e">e</param>
        /// <exception cref="System.NotImplementedException"></exception>
        void bt_func10_Click(object sender, EventArgs e)
        {
            // CLIENT_COMPUTER_NAME、CLIENT_USER_NAME、CREATE_USERが一致した場合T_GETSUJI_SHORI_CHUテーブルをクリア
            GetEnvironmentInfoClass environment = new GetEnvironmentInfoClass();
            var nowEnvName = environment.GetComputerAndUserName();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            var user = SystemProperty.UserName;
            string createPc = string.Empty;
            string clientUserName = string.Empty;
            string createUser = string.Empty;
            var table = this.getsujiShoriChuDao.GetAllData();

            if (table.Rows.Count <= 0)
            {
                msgLogic.MessageBoxShow("E241", "月次処理");
                return;
            }

            // 月次処理中テーブルに自分がいるか検索
            foreach (DataRow row in table.Rows)
            {
                if (row["CLIENT_COMPUTER_NAME"].Equals(nowEnvName.Item1)
                    && row["CLIENT_USER_NAME"].Equals(nowEnvName.Item2)
                    && row["CREATE_USER"].Equals(user))
                {
                    var result = msgLogic.MessageBoxShow("C094", "月次処理");
                    if (result == DialogResult.Yes)
                    {
                        var entity = new T_GETSUJI_SHORI_CHU();
                        entity.YEAR = (Int16)row["YEAR"];
                        entity.MONTH = (Int16)row["MONTH"];
                        entity.TIME_STAMP = (byte[])row["TIME_STAMP"];

                        // 月次処理中テーブルをDeleteします。
                        using (Transaction tran = new Transaction())
                        {
                            int CntNyuukinIns = this.getsujiShoriChuDao.Delete(entity);
                            tran.Commit();
                        }
                        msgLogic.MessageBoxShow("I017", "月次処理を中止");
                    }
                    this.ButtonInit();
                    return;
                }

                // どのＰＣがロックしているかを取得
                if (!string.IsNullOrEmpty(row["CLIENT_COMPUTER_NAME"].ToString()) &&
                    !string.IsNullOrEmpty(row["CLIENT_USER_NAME"].ToString()) &&
                    !string.IsNullOrEmpty(row["CREATE_USER"].ToString()))
                {
                    createPc = row["CLIENT_COMPUTER_NAME"].ToString();
                    clientUserName = row["CLIENT_USER_NAME"].ToString();
                    createUser = row["CREATE_USER"].ToString();
                }
            }

            // 月次処理中テーブルにデータは存在するが、自分ではなかった場合
            if (!string.IsNullOrEmpty(createPc) && !string.IsNullOrEmpty(clientUserName) && !string.IsNullOrEmpty(createUser))
            {
                msgLogic.MessageBoxShow("E240", "月次処理", createPc, clientUserName, createUser);
            }
            else
            {
                msgLogic.MessageBoxShow("E241", "月次処理");
            }
            return;
        }
        #endregion


        #region [F12]閉じる

        /// <summary>
        /// [F12]閉じるボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            // 月次処理中
            if (this.isGetsujiProcess)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();
        }

        #endregion

        #region [1]月次消費税調整(売上)

        /// <summary>
        /// [1]月次消費税調整(売上)ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_process1_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 月次処理中
            if (this.isGetsujiProcess)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            DateTime date = DateTime.Parse(this.form.GETSUJI_DATE.Value.ToString());

            // 最新締め年月チェック
            int year = 0;
            int month = 0;
            this.GetLatestGetsujiDate(ref year, ref month);
            if (year != date.Year || month != date.Month)
            {
                MessageBoxUtility.MessageBoxShowError("最新の月次年月ではないため実行出来ません。");
                return;
            }

            /* 別ユーザが月次処理中かのチェック */
            GetsujiShoriCheckLogicClass checkLogic = new GetsujiShoriCheckLogicClass();
            if (checkLogic.CheckGetsujiShoriChu())
            {
                MessageBoxUtility.MessageBoxShow("E224", "実行");
                return;
            }

            // 月次消費税調整(売上)画面起動
            FormManager.OpenFormModal("G618", date, WINDOW_ID.T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_UR);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [2]月次消費税調整(支払)

        /// <summary>
        /// [2]月次消費税調整(支払)ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_process2_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (this.isGetsujiProcess)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            DateTime date = DateTime.Parse(this.form.GETSUJI_DATE.Value.ToString());

            // 最新締め年月チェック
            int year = 0;
            int month = 0;
            this.GetLatestGetsujiDate(ref year, ref month);
            if (year != date.Year || month != date.Month)
            {
                MessageBoxUtility.MessageBoxShowError("最新の月次年月ではないため実行出来ません。");
                return;
            }

            /* 別ユーザが月次処理中かのチェック */
            GetsujiShoriCheckLogicClass checkLogic = new GetsujiShoriCheckLogicClass();
            if (checkLogic.CheckGetsujiShoriChu())
            {
                MessageBoxUtility.MessageBoxShow("E224", "実行");
                return;
            }

            // 月次消費税調整(支払)画面起動
            FormManager.OpenFormModal("G618", date, WINDOW_ID.T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_SH);

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region [5]旧月次処理

        /// <summary>
        /// [5]旧月次処理ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_process5_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            //旧請求締処理
            OldInvoiceKBN = 1;

            NewOld_invoice(sender, e);

            LogUtility.DebugMethodEnd();
        }

        #endregion


        #region 月次処理本体

        /// <summary>
        /// [F9]実行or[5]旧月次処理クリック
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewOld_invoice(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            // 月次処理中
            if (isGetsujiProcess)
            {
                return;
            }

            // 月次処理ロジック生成(進行状況を表示させるためプログレスバーを指定)
            var parentForm = (BusinessBaseForm)this.form.Parent;
            ToolStripProgressBar progresBar = parentForm.progresBar;
            GetsujiShoriLogic logic = new GetsujiShoriLogic(progresBar);

            // 実行月次年月(xxxx年xx月1日で取得される)
            DateTime getsujiDate = DateTime.Parse(this.form.GETSUJI_DATE.Value.ToString());

            /* 別ユーザが月次処理中かのチェック */
            GetsujiShoriCheckLogicClass checkLogic = new GetsujiShoriCheckLogicClass();
            if (checkLogic.CheckGetsujiShoriChu())
            {
                MessageBoxUtility.MessageBoxShow("E224", "実行");
                return;
            }

            this.isGetsujiProcess = true;

            try
            {
                // 月次処理中テーブル登録
                logic.InsertGetsujiShoriChu(getsujiDate.Year, getsujiDate.Month);

                /* 最新締年月取得 */
                int chkYear = 0;
                int chkMonth = 0;
                this.GetLatestGetsujiDate(ref chkYear, ref chkMonth);

                // 実行確認アラート
                if (chkYear != 0 && chkMonth != 0)
                {
                    // 初回時は月次処理ロジック側で処理に時間がかかるが行うかを問う旨のアラートを表示
                    // しているので、ここでは2回目以降の場合のみ表示を行う。
                    DialogResult result = MessageBoxUtility.MessageBoxShow("C046", "月次処理を実行");
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                }

                /* 最新締年月かのチェック */
                if (chkYear != 0 && chkMonth != 0)
                {
                    DateTime latestGetsujiDate = new DateTime(chkYear, chkMonth, 1);
                    if (getsujiDate.CompareTo(latestGetsujiDate.AddMonths(1)) > 0)
                    {
                        // 最新月次年月+1ヶ月が現在実行しようとしている月次年月ではない場合は中断
                        StringBuilder msg = new StringBuilder();
                        msg.Append(chkYear);
                        msg.Append("年");
                        msg.Append(chkMonth);
                        msg.Append("月以降の月次処理が行われていないため実行出来ません。");
                        MessageBoxUtility.MessageBoxShowError(msg.ToString());

                        // 別ユーザが同月次年月で月次処理 or 削除をしたことを考慮し、締状況を更新する
                        this.UpdateShimeStatus();
                        return;
                    }
                    else if (getsujiDate.CompareTo(latestGetsujiDate.AddMonths(1)) < 0)
                    {
                        // 過去の月次処理は行えないので中断
                        StringBuilder msg = new StringBuilder();
                        msg.Append(chkYear);
                        msg.Append("年");
                        msg.Append(chkMonth);
                        msg.Append("月に月次処理が行われているため、過去の月次年月では実行出来ません。");
                        MessageBoxUtility.MessageBoxShowError(msg.ToString());

                        // 別ユーザが同月次年月で月次処理 or 削除をしたことを考慮し、締状況を更新する
                        this.UpdateShimeStatus();
                        return;
                    }
                }

                /* 未確定・滞留伝票存在チェック */
                string fromDate = string.Empty;
                if (chkYear != 0 && chkMonth != 0)
                {
                    // 最新年月チェックでデータが存在しない初回実行の場合、
                    // 過去全ての伝票でチェックを行うためデータがある場合だけFromを設定する。
                    fromDate = getsujiDate.ToString("yyyy/MM/dd");
                }
                string toDate = getsujiDate.AddMonths(1).AddDays(-1).ToString("yyyy/MM/dd");
                bool isExist = CheckExistNotAvailableDenpyou(fromDate, toDate);
                if (isExist)
                {
                    // 未確定・滞留伝票がある場合は月次処理中断
                    MessageBoxUtility.MessageBoxShowError("月次年月内に未確定伝票、滞留伝票、未検収伝票のいずれかが存在するため実行できません。");
                    return;
                }

                /* 月次処理実行 */
                // 月次処理がキャンセルしたかの判定用フラグ
                bool isComplete = false;

                // 月次処理実行
                isComplete = logic.GetsujiShori(getsujiDate.Year, getsujiDate.Month, true, OldInvoiceKBN, this.form.WindowId);

                /* 登録処理 */
                if (isComplete)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        // 月次データ取得
                        string key = getsujiDate.ToString("yyyy/MM");
                        List<T_MONTHLY_LOCK_UR> monthlyLockUrList = new List<T_MONTHLY_LOCK_UR>();
                        if (logic.MonthlyLockUrDatas.Count > 0)
                        {
                            monthlyLockUrList = logic.MonthlyLockUrDatas[key];
                        }
                        List<T_MONTHLY_LOCK_SH> monthlyLockShList = new List<T_MONTHLY_LOCK_SH>();
                        if (logic.MonthlyLockShDatas.Count > 0)
                        {
                            monthlyLockShList = logic.MonthlyLockShDatas[key];
                        }
                        // 在庫月次処理を追加　chenzz　start
                        List<T_MONTHLY_LOCK_ZAIKO> monthlyLockZaikoList = new List<T_MONTHLY_LOCK_ZAIKO>();
                        if (logic.MonthlyLockZaikoDatas.Count > 0)
                        {
                            monthlyLockZaikoList = logic.MonthlyLockZaikoDatas[key];
                        }
                        // 在庫月次処理を追加　chenzz　end

                        // 登録
                        this.Regist(monthlyLockUrList, monthlyLockShList, monthlyLockZaikoList, getsujiDate.Year, getsujiDate.Month);

                        // トランザクション終了
                        tran.Commit();
                    }

                    // 完了メッセージ
                    MessageBoxUtility.MessageBoxShow("I001", "月次処理");
                    // 締状態を締済に更新
                    this.form.SHIME_STATUS.Text = SHIME_STATUS_SHIMEZUMI;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                // 月次処理中テーブル削除
                logic.DeleteGetsujiShoriChu(getsujiDate.Year, getsujiDate.Month);
                this.isGetsujiProcess = false;
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion




        #region FormClosingイベント

        /// <summary>
        /// FormClosingイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // 月次処理中に閉じないようにする
            if (isGetsujiProcess)
            {
                e.Cancel = true;
            }
        }

        #endregion

        #endregion

        #region テーブル検索処理

        /// <summary>
        /// T_MONTHLY_LOCK_UR：売上月次締処理テーブルを検索します
        /// </summary>
        /// <param name="entity">条件絞込用Entity(PK項目のみ使用)</param>
        /// <returns></returns>
        private T_MONTHLY_LOCK_UR[] GetMonthlyLockUrData(T_MONTHLY_LOCK_UR entity)
        {
            try
            {
                LogUtility.DebugMethodStart(entity);

                if (entity == null)
                {
                    entity = new T_MONTHLY_LOCK_UR();
                }
                T_MONTHLY_LOCK_UR[] data = this.MonthlyLockUrDao.GetAllValidData(entity);
                return data;
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// T_MONTHLY_LOCK_SH：支払月次締処理テーブルを検索します
        /// </summary>
        /// <param name="entity">条件絞込用Entity(PK項目のみ使用)</param>
        /// <returns></returns>
        private T_MONTHLY_LOCK_SH[] GetMonthlyLockShData(T_MONTHLY_LOCK_SH entity)
        {
            try
            {
                LogUtility.DebugMethodStart(entity);

                if (entity == null)
                {
                    entity = new T_MONTHLY_LOCK_SH();
                }
                T_MONTHLY_LOCK_SH[] data = this.MonthlyLockShDao.GetAllValidData(entity);
                return data;
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// T_MONTHLY_LOCK_ZAIKO：在庫月次締処理テーブルを検索します
        /// </summary>
        /// <param name="entity">条件絞込用Entity(PK項目のみ使用)</param>
        /// <returns></returns>
        private T_MONTHLY_LOCK_ZAIKO[] GetMonthlyLockZaikoData(T_MONTHLY_LOCK_ZAIKO entity)
        {
            try
            {
                LogUtility.DebugMethodStart(entity);

                if (entity == null)
                {
                    entity = new T_MONTHLY_LOCK_ZAIKO();
                }
                T_MONTHLY_LOCK_ZAIKO[] data = this.MonthlyLockZaikoDao.GetAllValidData(entity);
                return data;
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// T_MONTHLY_ADJUST_UR：売上月次調整テーブルを検索します
        /// </summary>
        /// <param name="entity">条件絞込用Entity(PK項目のみ使用)</param>
        /// <returns></returns>
        private T_MONTHLY_ADJUST_UR[] GetMonthlyAdjustUrData(T_MONTHLY_ADJUST_UR entity)
        {
            try
            {
                LogUtility.DebugMethodStart(entity);

                if (entity == null)
                {
                    entity = new T_MONTHLY_ADJUST_UR();
                }
                T_MONTHLY_ADJUST_UR[] data = this.MonthlyAdjustUrDao.GetAllValidData(entity);
                return data;
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// T_MONTHLY_ADJUST_SH：支払月次調整テーブルを検索します
        /// </summary>
        /// <param name="entity">条件絞込用Entity(PK項目のみ使用)</param>
        /// <returns></returns>
        private T_MONTHLY_ADJUST_SH[] GetMonthlyAdjustShData(T_MONTHLY_ADJUST_SH entity)
        {
            try
            {
                LogUtility.DebugMethodStart(entity);

                if (entity == null)
                {
                    entity = new T_MONTHLY_ADJUST_SH();
                }
                T_MONTHLY_ADJUST_SH[] data = this.MonthlyAdjustShDao.GetAllValidData(entity);
                return data;
            }
            catch
            {
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 月次処理データを登録します
        /// </summary>
        /// <param name="monthlyLockUrList">売上月次処理データのリスト</param>
        /// <param name="monthlyLockShList">支払月次処理データのリスト</param>
        /// <param name="year">月次年月の年</param>
        /// <param name="month">月次年月の月</param>
        private void Regist(List<T_MONTHLY_LOCK_UR> monthlyLockUrList, List<T_MONTHLY_LOCK_SH> monthlyLockShList, List<T_MONTHLY_LOCK_ZAIKO> monthlyLockZaikoList, int year, int month)
        {
            /* SEQは設定されていないので、削除データも含めたSEQが最大の全取引先データを取得しSEQを設定する */

            #region 売上
            {
                // 過去月次処理データでSEQが最大のデータを取得
                DataTable latestData = this.MonthlyLockUrDao.GetLatestMonthlyLockUrData(year, month);

                foreach (T_MONTHLY_LOCK_UR data in monthlyLockUrList)
                {
                    // SEQの設定
                    DataRow[] rows = latestData.Select("TORIHIKISAKI_CD = '" + data.TORIHIKISAKI_CD + "'");
                    if (rows != null && rows.Length > 0)
                    {
                        // 最大で1件ヒット
                        int seq = int.Parse(rows[0]["SEQ"].ToString());
                        data.SEQ = seq + 1;
                    }
                    else
                    {
                        // データが無い場合は初回
                        data.SEQ = 1;
                    }

                    // 登録者、更新者情報の設定
                    SetSystemProperty(data);

                    // Insert
                    this.MonthlyLockUrDao.Insert(data);
                }

            }
            #endregion

            #region 支払
            {
                // 過去月次処理データでSEQが最大のデータを取得
                DataTable latestData = this.MonthlyLockShDao.GetLatestMonthlyLockShData(year, month);

                foreach (T_MONTHLY_LOCK_SH data in monthlyLockShList)
                {
                    // SEQの設定
                    DataRow[] rows = latestData.Select("TORIHIKISAKI_CD = '" + data.TORIHIKISAKI_CD + "'");
                    if (rows != null && rows.Length > 0)
                    {
                        // 最大で1件ヒット
                        int seq = int.Parse(rows[0]["SEQ"].ToString());
                        data.SEQ = seq + 1;
                    }
                    else
                    {
                        // データが無い場合は初回
                        data.SEQ = 1;
                    }

                    // 登録者、更新者情報の設定
                    SetSystemProperty(data);

                    // Insert
                    this.MonthlyLockShDao.Insert(data);
                }
            }
            #endregion

            #region 在庫
            {
                try
                {
                    LogUtility.DebugMethodStart(monthlyLockUrList, monthlyLockShList, monthlyLockZaikoList, year, month);
                // 過去月次処理データでSEQが最大のデータを取得
                DataTable latestData = this.MonthlyLockZaikoDao.GetLatestMonthlyLockZaikoData(year, month);

                foreach (T_MONTHLY_LOCK_ZAIKO data in monthlyLockZaikoList)
                {
                    // SEQの設定
                    DataRow[] rows = latestData.Select("GYOUSHA_CD = '" + data.GYOUSHA_CD + "' AND GENBA_CD = '" + data.GENBA_CD + "' AND ZAIKO_HINMEI_CD = '" + data.ZAIKO_HINMEI_CD + "'");
                    if (rows != null && rows.Length > 0)
                    {
                        // 最大で1件ヒット
                        int seq = int.Parse(rows[0]["SEQ"].ToString());
                        data.SEQ = seq + 1;
                    }
                    else
                    {
                        // データが無い場合は初回
                        data.SEQ = 1;
                    }

                    // 登録者、更新者情報の設定
                    SetSystemProperty(data);

                    // Insert
                    this.MonthlyLockZaikoDao.Insert(data);
                }
            }
                catch
                {
                    throw;
                }
                finally
                {
                    LogUtility.DebugMethodEnd();
                }
            }
            #endregion
        }

        /// <summary>
        /// システム自動設定のプロパティを設定します
        /// </summary>
        /// <param name="entity">設定を行うEntity</param>
        public void SetSystemProperty(SuperEntity entity)
        {
            string computerName = SystemInformation.ComputerName;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            DateTime now = this.getDBDateTime();
            //entity.CREATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
            entity.CREATE_DATE = SqlDateTime.Parse(now.ToString());
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            entity.CREATE_USER = SystemProperty.UserName;
            entity.CREATE_PC = computerName;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            //entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
            entity.UPDATE_DATE = SqlDateTime.Parse(now.ToString());
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            entity.UPDATE_USER = SystemProperty.UserName;
            entity.UPDATE_PC = computerName;
        }

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
        #endregion

        #region 締解除処理(論理削除)

        /// <summary>
        /// 論理削除処理を実行します
        /// </summary>
        public void LogicalDelete()
        {
            // 最新レコードに削除フラグを設定しUpdateを行う
            // その後、最新SEQ+1にし更新者情報を設定した削除レコードをInsertする

            LogUtility.DebugMethodStart();

            /* 更新データ検索条件 */
            DateTime getsujiDate = DateTime.Parse(this.form.GETSUJI_DATE.Value.ToString());
            short searchYear = short.Parse(getsujiDate.Year.ToString());
            short searchMonth = short.Parse(getsujiDate.Month.ToString());

            #region 売上月次締処理テーブル

            T_MONTHLY_LOCK_UR monthlyLockUrEntity = new T_MONTHLY_LOCK_UR();
            monthlyLockUrEntity.YEAR = searchYear;
            monthlyLockUrEntity.MONTH = searchMonth;
            T_MONTHLY_LOCK_UR[] monthlyLockUrDatas = this.GetMonthlyLockUrData(monthlyLockUrEntity);

            if (monthlyLockUrDatas != null)
            {
                for (int i = 0; i < monthlyLockUrDatas.Length; i++)
                {
                    T_MONTHLY_LOCK_UR monthlyLockUrData = monthlyLockUrDatas[i];

                    // Update
                    monthlyLockUrData.DELETE_FLG = true;
                    this.MonthlyLockUrDao.Update(monthlyLockUrData);

                    // Insert
                    monthlyLockUrData.SEQ = monthlyLockUrData.SEQ + 1;
                    this.SetSystemPropertyOnlyUpdateInfo(monthlyLockUrData);
                    this.MonthlyLockUrDao.Insert(monthlyLockUrData);
                }
            }

            #endregion

            #region 売上月次調整データ取得

            T_MONTHLY_ADJUST_UR monthlyAdjustUrEntity = new T_MONTHLY_ADJUST_UR();
            monthlyAdjustUrEntity.YEAR = searchYear;
            monthlyAdjustUrEntity.MONTH = searchMonth;
            T_MONTHLY_ADJUST_UR[] monthlyAdjustUrDatas = this.GetMonthlyAdjustUrData(monthlyAdjustUrEntity);

            if (monthlyAdjustUrDatas != null)
            {
                for (int i = 0; i < monthlyAdjustUrDatas.Length; i++)
                {
                    T_MONTHLY_ADJUST_UR monthlyAdjustUrData = monthlyAdjustUrDatas[i];

                    // Update
                    monthlyAdjustUrData.DELETE_FLG = true;
                    this.MonthlyAdjustUrDao.Update(monthlyAdjustUrData);

                    // Insert
                    monthlyAdjustUrData.UPDATE_SEQ = 1;
                    monthlyAdjustUrData.SEQ = monthlyAdjustUrData.SEQ + 1;
                    this.SetSystemPropertyOnlyUpdateInfo(monthlyAdjustUrData);
                    this.MonthlyAdjustUrDao.Insert(monthlyAdjustUrData);
                }
            }

            #endregion

            #region 支払月次締処理テーブルデータ取得

            T_MONTHLY_LOCK_SH monthlyLockShEntity = new T_MONTHLY_LOCK_SH();
            monthlyLockShEntity.YEAR = searchYear;
            monthlyLockShEntity.MONTH = searchMonth;
            T_MONTHLY_LOCK_SH[] monthlyLockShDatas = this.GetMonthlyLockShData(monthlyLockShEntity);

            if (monthlyLockShDatas != null)
            {
                for (int i = 0; i < monthlyLockShDatas.Length; i++)
                {
                    T_MONTHLY_LOCK_SH monthlyLockShData = monthlyLockShDatas[i];

                    // Update
                    monthlyLockShData.DELETE_FLG = true;
                    this.MonthlyLockShDao.Update(monthlyLockShData);

                    // Insert
                    monthlyLockShData.SEQ = monthlyLockShData.SEQ + 1;
                    this.SetSystemPropertyOnlyUpdateInfo(monthlyLockShData);
                    this.MonthlyLockShDao.Insert(monthlyLockShData);
                }
            }

            #endregion

            #region 支払月次調整テーブルデータ取得

            T_MONTHLY_ADJUST_SH monthlyAdjustShEntity = new T_MONTHLY_ADJUST_SH();
            monthlyAdjustShEntity.YEAR = searchYear;
            monthlyAdjustShEntity.MONTH = searchMonth;
            T_MONTHLY_ADJUST_SH[] monthlyAdjustShDatas = this.GetMonthlyAdjustShData(monthlyAdjustShEntity);

            if (monthlyAdjustShDatas != null)
            {
                for (int i = 0; i < monthlyAdjustShDatas.Length; i++)
                {
                    T_MONTHLY_ADJUST_SH monthlyAdjustShData = monthlyAdjustShDatas[i];

                    // Update
                    monthlyAdjustShData.DELETE_FLG = true;
                    this.MonthlyAdjustShDao.Update(monthlyAdjustShData);

                    // Insert
                    monthlyAdjustShData.UPDATE_SEQ = 1;
                    monthlyAdjustShData.SEQ = monthlyAdjustShData.SEQ + 1;
                    this.SetSystemPropertyOnlyUpdateInfo(monthlyAdjustShData);
                    this.MonthlyAdjustShDao.Insert(monthlyAdjustShData);
                }
            }

            #endregion

            #region 在庫締処理テーブル

            T_MONTHLY_LOCK_ZAIKO zaikoEntity = new T_MONTHLY_LOCK_ZAIKO();
            zaikoEntity.YEAR = searchYear;
            zaikoEntity.MONTH = searchMonth;
            T_MONTHLY_LOCK_ZAIKO[] zaikoDatas = this.GetMonthlyLockZaikoData(zaikoEntity);

            if (zaikoDatas != null)
            {
                for (int i = 0; i < zaikoDatas.Length; i++)
                {
                    T_MONTHLY_LOCK_ZAIKO monthlyLockZaikoData = zaikoDatas[i];

                    // Update
                    monthlyLockZaikoData.DELETE_FLG = true;
                    this.MonthlyLockZaikoDao.Update(monthlyLockZaikoData);

                    // Insert
                    monthlyLockZaikoData.SEQ = monthlyLockZaikoData.SEQ + 1;
                    this.SetSystemPropertyOnlyUpdateInfo(monthlyLockZaikoData);
                    this.MonthlyLockZaikoDao.Insert(monthlyLockZaikoData);
                }
            }

            #endregion

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// システム自動設定のプロパティを更新者情報のみ設定します
        /// </summary>
        /// <param name="entity">設定を行うEntity</param>
        public void SetSystemPropertyOnlyUpdateInfo(SuperEntity entity)
        {
            string computerName = SystemInformation.ComputerName;
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            DateTime now = this.getDBDateTime();
            //entity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
            entity.UPDATE_DATE = SqlDateTime.Parse(now.ToString());
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            entity.UPDATE_USER = SystemProperty.UserName;
            entity.UPDATE_PC = computerName;
        }

        #endregion

        #region 最新締年月取得

        /// <summary>
        /// 締済みの最新年月を引数で指定した年月に設定します
        /// 最新締めデータが無い場合、引数の値は更新されません
        /// </summary>
        /// <param name="year">月次年月の年</param>
        /// <param name="month">月次年月の月</param>
        private void GetLatestGetsujiDate(ref int year, ref int month)
        {
            // 売上月次締処理　支払月次締処理
            T_MONTHLY_LOCK_UR urEntity = new T_MONTHLY_LOCK_UR();
            T_MONTHLY_LOCK_UR[] monthlyLockUrData = GetMonthlyLockUrData(urEntity);
            T_MONTHLY_LOCK_SH shEntity = new T_MONTHLY_LOCK_SH();
            T_MONTHLY_LOCK_SH[] monthlyLockShData = GetMonthlyLockShData(shEntity);
            T_MONTHLY_LOCK_ZAIKO zaikoEntity = new T_MONTHLY_LOCK_ZAIKO();
            T_MONTHLY_LOCK_ZAIKO[] monthlyLockZaikoData = GetMonthlyLockZaikoData(zaikoEntity);

            DateTime dateTime = this.parentForm.sysDate;
            if (monthlyLockUrData != null && monthlyLockUrData.Length > 0)
            {
                // 最新順にソート済みのため1行目
                T_MONTHLY_LOCK_UR row = monthlyLockUrData[0];
                year = int.Parse(row.YEAR.ToString());
                month = int.Parse(row.MONTH.ToString());
                dateTime = new DateTime(year, month, 1);
            }

            if (monthlyLockShData != null && monthlyLockShData.Length > 0)
            {
                // 最新順にソート済みのため1行目
                T_MONTHLY_LOCK_SH row = monthlyLockShData[0];
                int shYear = int.Parse(row.YEAR.ToString());
                int shMonth = int.Parse(row.MONTH.ToString());
                DateTime shDateTime = new DateTime(shYear, shMonth, 1);

                // 最終締処理日を比較して最新の方の日付を使用する
                int compareResult = dateTime.CompareTo(shDateTime);
                if (compareResult < 0)
                {
                    year = shYear;
                    month = shMonth;
                }
            }

            if (monthlyLockZaikoData != null && monthlyLockZaikoData.Length > 0)
            {
                // 最新順にソート済みのため1行目
                T_MONTHLY_LOCK_ZAIKO row = monthlyLockZaikoData[0];
                int zaikoYear = int.Parse(row.YEAR.ToString());
                int zaikoMonth = int.Parse(row.MONTH.ToString());
                DateTime zaikoDateTime = new DateTime(zaikoYear, zaikoMonth, 1);

                // 最終締処理日を比較して最新の方の日付を使用する
                int compareResult = dateTime.CompareTo(zaikoDateTime);
                if (compareResult < 0)
                {
                    year = zaikoYear;
                    month = zaikoMonth;
                }
            }

            //if (monthlyLockUrData != null && monthlyLockUrData.Length > 0 &&
            //    monthlyLockShData != null && monthlyLockShData.Length > 0)
            //{
            //    // 最新順にソート済みのため1行目
            //    T_MONTHLY_LOCK_UR urRow = monthlyLockUrData[0];
            //    int urYear = int.Parse(urRow.YEAR.ToString());
            //    int urMonth = int.Parse(urRow.MONTH.ToString());
            //    // 月次年月は年月までの表示のため日付は1日を指定
            //    DateTime urDateTime = new DateTime(urYear, urMonth, 1);

            //    // 最新順にソート済みのため1行目
            //    T_MONTHLY_LOCK_SH shRow = monthlyLockShData[0];
            //    int shYear = int.Parse(shRow.YEAR.ToString());
            //    int shMonth = int.Parse(shRow.MONTH.ToString());
            //    // 月次年月は年月までの表示のため日付は1日を指定
            //    DateTime shDateTime = new DateTime(shYear, shMonth, 1);

            //    // 売上・支払の最終締処理日を比較して最新の方の日付を使用する
            //    int compareResult = urDateTime.CompareTo(shDateTime);
            //    if (compareResult < 0)
            //    {
            //        year = shDateTime.Year;
            //        month = shDateTime.Month;
            //    }
            //    else if (compareResult > 0)
            //    {
            //        year = urDateTime.Year;
            //        month = urDateTime.Month;
            //    }
            //    else
            //    {
            //        // 等しいのでどちらを使用しても良い
            //        year = urDateTime.Year;
            //        month = urDateTime.Month;
            //    }
            //}
            //else if (monthlyLockUrData != null && monthlyLockUrData.Length > 0)
            //{
            //    // 最新順にソート済みのため1行目
            //    T_MONTHLY_LOCK_UR row = monthlyLockUrData[0];
            //    year = int.Parse(row.YEAR.ToString());
            //    month = int.Parse(row.MONTH.ToString());
            //}
            //else if (monthlyLockShData != null && monthlyLockShData.Length > 0)
            //{
            //    // 最新順にソート済みのため1行目
            //    T_MONTHLY_LOCK_SH row = monthlyLockShData[0];
            //    year = int.Parse(row.YEAR.ToString());
            //    month = int.Parse(row.MONTH.ToString());
            //}
        }

        #endregion

        #region 締状態ラベル更新

        private void UpdateShimeStatus()
        {
            DateTime getsujiDate = DateTime.Parse(this.form.GETSUJI_DATE.Value.ToString());

            /* 締状態チェック */
            short year = short.Parse(getsujiDate.Year.ToString());
            short month = short.Parse(getsujiDate.Month.ToString());
            if (this.ShimeStatusCheck(year, month))
            {
                // 締済
                this.form.SHIME_STATUS.Text = SHIME_STATUS_SHIMEZUMI;
            }
            else
            {
                // 未締
                this.form.SHIME_STATUS.Text = SHIME_STATUS_MISHIME;
            }
        }

        #region 締状態チェック

        /// <summary>
        /// 指定した年月で締データが存在するかチェックを行います
        /// </summary>
        /// <param name="year">月次年月の年</param>
        /// <param name="month">月次年月の月</param>
        /// <returns>締データ有：True　締データ無：False</returns>
        private bool ShimeStatusCheck(short year, short month)
        {
            bool returnVal = false;

            T_MONTHLY_LOCK_UR urEntity = new T_MONTHLY_LOCK_UR();
            urEntity.YEAR = year;
            urEntity.MONTH = month;
            T_MONTHLY_LOCK_UR[] urData = this.GetMonthlyLockUrData(urEntity);

            if (urData != null && urData.Length > 0)
            {
                returnVal = true;
            }
            else
            {
                T_MONTHLY_LOCK_SH shEntity = new T_MONTHLY_LOCK_SH();
                shEntity.YEAR = year;
                shEntity.MONTH = month;

                T_MONTHLY_LOCK_SH[] shData = this.GetMonthlyLockShData(shEntity);

                if (shData != null && shData.Length > 0)
                {
                    returnVal = true;
                }
            }

            return returnVal;
        }

        #endregion

        #endregion

        #region 未確定・滞留伝票存在チェック

        /// <summary>
        /// 指定した期間内に受入・出荷・売上/支払伝票で未確定または滞留伝票が存在するかをチェックします
        /// </summary>
        /// <param name="fromDate">検索期間From(yyyy/MM/dd形式)</param>
        /// <param name="toDate">検索期間To(yyyy/MM/dd形式)</param>
        /// <returns>True：存在する　False：存在しない</returns>
        private bool CheckExistNotAvailableDenpyou(string fromDate, string toDate)
        {
            bool val = true;

            DataTable dt = this.GetsujiShoriDao.CheckExistNotAvailableDenpyou(fromDate, toDate);
            if (dt== null || dt.Rows.Count == 0)
            {
                val = false;
            }

            return val;
        }

        #endregion

        #region Not Use

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #endregion
    }
}
