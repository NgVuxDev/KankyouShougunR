using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku.DAO;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Logic;
using Shougun.Core.Message;

namespace Shougun.Core.Billing.GetsujiShouhizeiChouseiNyuuryoku
{
    internal class LogicClass : IBuisinessLogic
    {
        #region Field

        /// <summary>ヘッダフォーム</summary>
        private UIHeader headerForm;

        /// <summary>メインフォーム</summary>
        private UIForm form;

        /// <summary>月次年月</summary>
        internal DateTime GetsujiYM;

        /// <summary>売上月次締処理情報DAO</summary>
        private IT_MONTHLY_LOCK_URDao lockUrDao;

        /// <summary>支払月次締処理情報DAO</summary>
        private IT_MONTHLY_LOCK_SHDao lockShDao;

        /// <summary>売上月次調整情報DAO</summary>
        private IT_MONTHLY_ADJUST_URDao adjustUrDao;

        /// <summary>支払月次調整情報DAO</summary>
        private IT_MONTHLY_ADJUST_SHDao adjustShDao;

        /// <summary>検索結果</summary>
        private DataTable searchResult;

        /// <summary>検索結果(変更前用)</summary>
        private DataTable preSearchResult;

        /// <summary>最新の売上月次調整データ</summary>
        private T_MONTHLY_ADJUST_UR[] monthlyAdjustUrList;

        /// <summary>最新の支払月次調整データ</summary>
        private T_MONTHLY_ADJUST_SH[] monthlyAdjustShList;

        private MessageBoxShowLogic errmessage;

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

            // DAO初期化
            this.lockUrDao = DaoInitUtility.GetComponent<IT_MONTHLY_LOCK_URDao>();
            this.lockShDao = DaoInitUtility.GetComponent<IT_MONTHLY_LOCK_SHDao>();
            this.adjustUrDao = DaoInitUtility.GetComponent<IT_MONTHLY_ADJUST_URDao>();
            this.adjustShDao = DaoInitUtility.GetComponent<IT_MONTHLY_ADJUST_SHDao>();
            this.errmessage = new MessageBoxShowLogic();

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

                // ボタン初期化
                this.ButtonInit();

                // イベント初期化
                this.EventInit();

                // 画面情報の初期化
                this.FormInit();

                // 一覧初期化
                if (!this.DetailInit())
                {
                    ret = false;
                }
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
            parentForm.bt_func9.Click += new EventHandler(bt_func9_Click);      //保存
            parentForm.bt_func12.Click += new EventHandler(bt_func12_Click);    //閉じる

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化

        /// <summary>
        /// 画面初期化
        /// </summary>
        private void FormInit()
        {
            this.form.GETSUJI_DATE.Value = this.GetsujiYM;
            this.form.SHIME_STATUS.Text = "締済"; // "締済"固定

            if (WINDOW_ID.T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_UR == form.WindowId)
            {
                this.form.DETAIL.Columns["NYUUSHUKKIN_KINGAKU"].HeaderText = "入金額";
                this.form.DETAIL.Columns["KINGAKU"].HeaderText = "税抜売上金額";
                this.form.DETAIL.Columns["TOTAL_KINGAKU"].HeaderText = "税込売上金額";
            }
            else
            {
                this.form.DETAIL.Columns["NYUUSHUKKIN_KINGAKU"].HeaderText = "出金額";
                this.form.DETAIL.Columns["KINGAKU"].HeaderText = "税抜支払金額";
                this.form.DETAIL.Columns["TOTAL_KINGAKU"].HeaderText = "税込支払金額";
            }
        }

        #endregion

        #region データ取得

        /// <summary>
        /// 一覧データ初期化
        /// </summary>
        private bool DetailInit()
        {
            LogUtility.DebugMethodStart();

            // データ取得
            if (this.Search() == -1)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            // 画面に検索結果を設定
            this.form.DETAIL.DataSource = this.searchResult;

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        /// <summary>
        /// 検索処理
        /// </summary>
        public int Search()
        {
            bool isUR = true;
            if (WINDOW_ID.T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_SH == form.WindowId)
            {
                isUR = false;
            }

            this.searchResult = this.lockUrDao.GetIchiran(this.GetsujiYM.Year, this.GetsujiYM.Month, isUR);

            // データ初期化
            foreach (DataRow row in this.searchResult.Rows)
            {
                if (row["ADJUST_TAX"] == DBNull.Value)
                {
                    row["ADJUST_TAX"] = decimal.Zero;
                }

                if (row["ZANDAKA"] == DBNull.Value)
                {
                    // 調整前差引残高 + 消費税調整額 = 調整後差引残高
                    bool catchErr = true;
                    decimal zandaka = CalculateZandaka(row["LOCK_ZANDAKA"], row["ADJUST_TAX"], out catchErr);
                    if (!catchErr)
                    {
                        return -1;
                    }

                    row["ZANDAKA"] = zandaka;
                }
            }

            // 更新用にデータ取得
            if (isUR)
            {
                this.monthlyAdjustUrList = this.adjustUrDao.GetLatestSeqMonthlyAdjustUrData(this.GetsujiYM.Year, this.GetsujiYM.Month);
            }
            else
            {
                this.monthlyAdjustShList = this.adjustShDao.GetLatestSeqMonthlyAdjustShData(this.GetsujiYM.Year, this.GetsujiYM.Month);
            }

            // 変更前用にデータ保存
            this.preSearchResult = CreateCloneDataTable(this.searchResult);

            // 
            return this.searchResult.Rows.Count;
        }

        #endregion

        #endregion

        #region FunctionButtonクリックイベント

        /// <summary>
        /// [F9]実行ボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var msgLogic = new MessageBoxShowLogic();

            // 月次処理中チェック
            GetsujiShoriCheckLogicClass getsujiShoriCheckLogic = new GetsujiShoriCheckLogicClass();
            if (getsujiShoriCheckLogic.CheckGetsujiShoriChu())
            {
                // 月次処理中は登録不可
                msgLogic.MessageBoxShow("E224", "実行");
                LogUtility.DebugMethodEnd();
                return;
            }

            // 最新締め年月チェック
            int year = 0;
            int month = 0;
            getsujiShoriCheckLogic.GetLatestGetsujiDate(ref year, ref month);
            if (year != this.GetsujiYM.Year || month != this.GetsujiYM.Month)
            {
                StringBuilder sb = new StringBuilder();
                if (year == 0 && month == 0)
                {
                    // 月次処理データが存在しない場合
                    sb.Append("月次処理データが存在しないため登録できません。");
                }
                else
                {
                    sb.AppendFormat("最新の締済み月次年月は{0}年{1}月です。", year, month)
                      .Append(Environment.NewLine)
                      .Append("月次年月が異なるため登録できません。");
                }

                MessageBoxUtility.MessageBoxShowError(sb.ToString());

                LogUtility.DebugMethodEnd();
                return;
            }

            var dt = this.form.DETAIL.DataSource as DataTable;
            if (dt == null)
            {
                LogUtility.DebugMethodEnd();
                return;
            }

            // 画面で変更があったデータのみ取得
            DataTable registDataTable = GetDifferenceIchiranData(dt);

            // 月次締処理データのSEQが変更されていないかチェック
            if (HadChangedSeqMonthlyLock(registDataTable))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("月次締処理データが更新されています。")
                  .Append("再度、月次消費税調整入力画面を開きなおしてください。");

                MessageBoxUtility.MessageBoxShowError(sb.ToString());

                LogUtility.DebugMethodEnd();
                return;
            }

            try
            {
                using (var tran = new Transaction())
                {
                    // 売上
                    if (WINDOW_ID.T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_UR == form.WindowId)
                    {
                        this.RegistUR(registDataTable);
                    }
                    // 支払
                    else if (WINDOW_ID.T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_SH == form.WindowId)
                    {
                        this.RegistSH(registDataTable);
                    }

                    tran.Commit();

                    msgLogic.MessageBoxShow("I001", "登録");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    // SQLエラー用メッセージを出力
                    msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    throw;
                }
            }

            // 再検索
            this.DetailInit();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// [F12]閉じるボタンクリックイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            this.form.Close();
            parentForm.Close();
        }

        #endregion

        #region 一覧 - 検索(フォーカスIN)処理

        /// <summary>
        /// 検索用取引先項目の値で一覧を検索しフォーカスを当てます
        /// </summary>
        internal bool SearchIchiranTorihikisakiRow()
        {
            bool ret = true;
            try
            {
                string torihikisakiCd = this.form.SEARCH_TORIHIKISAKI_CD.Text;
                string torihikisakiName = this.form.SEARCH_TORIHIKISAKI_NAME.Text;

                if (string.IsNullOrEmpty(torihikisakiCd) || string.IsNullOrEmpty(torihikisakiName))
                {
                    return ret;
                }

                foreach (DataGridViewRow row in this.form.DETAIL.Rows)
                {
                    if (row.Cells["TORIHIKISAKI_CD"].Value.ToString().Equals(torihikisakiCd) &&
                        row.Cells["TORIHIKISAKI_NAME_RYAKU"].Value.ToString().Equals(torihikisakiName))
                    {
                        int rowIndex = row.Index;
                        this.form.DETAIL.CurrentCell = this.form.DETAIL.Rows[rowIndex].Cells["ADJUST_TAX"];
                        this.form.DETAIL.Focus();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchIchiranTorihikisakiRow", ex);
                this.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 差分更新用に一覧中で検索実行時から変更があったデータを取得します
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetDifferenceIchiranData(DataTable dt)
        {
            DataTable dataTable = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                string torihikisakiCd = row["TORIHIKISAKI_CD"].ToString();

                // 取引先CDのみで取得可能
                var preData = this.preSearchResult.AsEnumerable()
                                                  .Where(n => torihikisakiCd.Equals(n["TORIHIKISAKI_CD"].ToString()))
                                                  .ToList();

                if (preData.Count == 0)
                {
                    continue;
                }

                // 消費税調整額
                var tax = GetDecimal_ByObject(row["ADJUST_TAX"]);
                var preTax = GetDecimal_ByObject(preData[0]["ADJUST_TAX"]);

                // 調整後差引残高
                var zandaka = GetDecimal_ByObject(row["ZANDAKA"]);
                var preZandaka = GetDecimal_ByObject(preData[0]["ZANDAKA"]);

                // 値の変更有無を判定
                if (tax != preTax ||
                   zandaka != preZandaka)
                {
                    dataTable.ImportRow(row);
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 売上月次調整登録
        /// </summary>
        /// <param name="registDataTable"></param>
        private void RegistUR(DataTable registDataTable)
        {
            var updateList = new List<T_MONTHLY_ADJUST_UR>();
            var insertList = new List<T_MONTHLY_ADJUST_UR>();

            // 変更分があったデータから、登録・更新用のリスト取得
            CreateEntityUR(registDataTable, out updateList, out insertList);

            foreach (var updateEntity in updateList)
            {
                this.adjustUrDao.UpdateLogicalDeleteFlag(updateEntity);
            }

            foreach (var insertEntity in insertList)
            {
                this.adjustUrDao.Insert(insertEntity);
            }
        }

        /// <summary>
        /// 支払月次調整登録
        /// </summary>
        /// <param name="registDataTable"></param>
        private void RegistSH(DataTable registDataTable)
        {
            var updateList = new List<T_MONTHLY_ADJUST_SH>();
            var insertList = new List<T_MONTHLY_ADJUST_SH>();

            // 変更分があったデータから、登録・更新用のリスト取得
            CreateEntitySH(registDataTable, out updateList, out insertList);

            foreach (var updateEntity in updateList)
            {
                this.adjustShDao.UpdateLogicalDeleteFlag(updateEntity);
            }

            foreach (var insertEntity in insertList)
            {
                this.adjustShDao.Insert(insertEntity);
            }
        }

        #region CreateEntity

        /// <summary>
        /// 売上の登録用と更新用のEntityリスト作成
        /// </summary>
        /// <param name="registDataTable"></param>
        /// <param name="updateList"></param>
        /// <param name="insertList"></param>
        private void CreateEntityUR(DataTable registDataTable, out List<T_MONTHLY_ADJUST_UR> updateList, out List<T_MONTHLY_ADJUST_UR> insertList)
        {
            // 作成者～最終更新PC等の共通項目の更新は、データ登録直前に別途設定
            updateList = new List<T_MONTHLY_ADJUST_UR>();   // 削除フラグ=True のリスト
            insertList = new List<T_MONTHLY_ADJUST_UR>();   // 消費税調整額、調整後差引残高が更新されたリスト

            var dbLogic = new DataBinderLogic<T_MONTHLY_ADJUST_UR>(new T_MONTHLY_ADJUST_UR());
            foreach (DataRow row in registDataTable.Rows)
            {
                string torihikisakiCd = row["TORIHIKISAKI_CD"].ToString();

                // 最新月次調整データ取得
                var preData = this.monthlyAdjustUrList.Where(n => n.TORIHIKISAKI_CD.Equals(torihikisakiCd))
                                                      .ToList()
                                                      .FirstOrDefault();

                // 更新枝番
                int updateSeq = 0;

                if (preData != null)
                {
                    // 削除フラグの更新
                    if (preData.DELETE_FLG.IsFalse)
                    {
                        // データ更新
                        preData.YEAR = SqlInt16.Parse(this.GetsujiYM.Year.ToString());
                        preData.MONTH = SqlInt16.Parse(this.GetsujiYM.Month.ToString());
                        preData.DELETE_FLG = SqlBoolean.True.Value;

                        updateList.Add(preData);
                    }

                    // 最新の更新枝番取得
                    updateSeq = preData.UPDATE_SEQ.Value;
                }

                // 更新枝番のインクリメント
                updateSeq += 1;

                // 登録用データ作成
                var insertEntity = CreateMonthlyAdjustUR(row, updateSeq);

                // 作成・更新情報設定
                if (preData != null)
                {
                    // 過去データがある場合は作成者情報は引き継ぎ新者情報のみ設定
                    insertEntity.CREATE_USER = preData.CREATE_USER;
                    insertEntity.CREATE_DATE = preData.CREATE_DATE;
                    insertEntity.CREATE_PC = preData.CREATE_PC;
                    insertEntity.UPDATE_USER = SystemProperty.UserName;
                    insertEntity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    insertEntity.UPDATE_PC = SystemInformation.ComputerName;
                }
                else
                {
                    dbLogic.SetSystemProperty(insertEntity, false);
                }

                insertList.Add(insertEntity);
            }
        }

        /// <summary>
        /// 売上月次調整Entity作成
        /// </summary>
        /// <param name="row"></param>
        /// <param name="updateSeq"></param>
        /// <returns></returns>
        private T_MONTHLY_ADJUST_UR CreateMonthlyAdjustUR(DataRow row, int updateSeq)
        {
            if (row == null)
            {
                return null;
            }

            T_MONTHLY_ADJUST_UR entity = new T_MONTHLY_ADJUST_UR();

            // UPDATE_SEQ以外のPK項目はT_MONTHLY_LOCK_URから取得
            // LEFT JOINで取得しているためNULLの場合もあるため
            entity.TORIHIKISAKI_CD = GetString_ByObject(row["TORIHIKISAKI_CD"]);
            entity.YEAR = GetShort_ByObject(row["YEAR"]);
            entity.MONTH = GetShort_ByObject(row["MONTH"]);
            entity.SEQ = GetInt_ByObject(row["SEQ"]);

            entity.UPDATE_SEQ = updateSeq;
            entity.DELETE_FLG = SqlBoolean.False.Value;

            // 画面情報を設定
            entity.ADJUST_TAX = GetDecimal_ByObject(row["ADJUST_TAX"]);
            entity.ZANDAKA = GetDecimal_ByObject(row["ZANDAKA"]);

            return entity;
        }

        /// <summary>
        /// 支払の登録用と更新用のEntityリスト作成
        /// </summary>
        /// <param name="registDataTable"></param>
        /// <param name="updateList"></param>
        /// <param name="insertList"></param>
        private void CreateEntitySH(DataTable registDataTable, out List<T_MONTHLY_ADJUST_SH> updateList, out List<T_MONTHLY_ADJUST_SH> insertList)
        {
            // 作成者～最終更新PC等の共通項目の更新は、データ登録直前に設定
            updateList = new List<T_MONTHLY_ADJUST_SH>();   // 削除フラグ=True のリスト
            insertList = new List<T_MONTHLY_ADJUST_SH>();   // 消費税調整額、調整後差引残高が更新されたリスト

            var dbLogic = new DataBinderLogic<T_MONTHLY_ADJUST_SH>(new T_MONTHLY_ADJUST_SH());
            foreach (DataRow row in registDataTable.Rows)
            {
                string torihikisakiCd = row["TORIHIKISAKI_CD"].ToString();

                // 最新月次調整データ取得
                var preData = this.monthlyAdjustShList.Where(n => n.TORIHIKISAKI_CD.Equals(torihikisakiCd))
                                                      .ToList()
                                                      .FirstOrDefault();

                // 更新枝番
                int updateSeq = 0;

                if (preData != null)
                {
                    // 削除フラグの更新
                    if (preData.DELETE_FLG.IsFalse)
                    {
                        // データ更新
                        preData.YEAR = SqlInt16.Parse(this.GetsujiYM.Year.ToString());
                        preData.MONTH = SqlInt16.Parse(this.GetsujiYM.Month.ToString());
                        preData.DELETE_FLG = SqlBoolean.True.Value;

                        updateList.Add(preData);
                    }

                    // 最新の更新枝番取得
                    updateSeq = preData.UPDATE_SEQ.Value;
                }

                // 更新枝番のインクリメント
                updateSeq += 1;

                // 登録用データ作成
                var insertEntity = CreateMonthlyAdjustSH(row, updateSeq);

                if (preData != null)
                {
                    // 過去データがある場合は作成者情報は引き継ぎ新者情報のみ設定
                    insertEntity.CREATE_USER = preData.CREATE_USER;
                    insertEntity.CREATE_DATE = preData.CREATE_DATE;
                    insertEntity.CREATE_PC = preData.CREATE_PC;
                    insertEntity.UPDATE_USER = SystemProperty.UserName;
                    insertEntity.UPDATE_DATE = SqlDateTime.Parse(DateTime.Now.ToString());
                    insertEntity.UPDATE_PC = SystemInformation.ComputerName;
                }
                else
                {
                    dbLogic.SetSystemProperty(insertEntity, false);
                }

                insertList.Add(insertEntity);
            }
        }

        /// <summary>
        /// 支払月次調整Entity作成
        /// </summary>
        /// <param name="row"></param>
        /// <param name="updateSeq"></param>
        /// <returns></returns>
        private T_MONTHLY_ADJUST_SH CreateMonthlyAdjustSH(DataRow row, int updateSeq)
        {
            if (row == null)
            {
                return null;
            }

            T_MONTHLY_ADJUST_SH entity = new T_MONTHLY_ADJUST_SH();

            // UPDATE_SEQ以外のPK項目はT_MONTHLY_LOCK_SHから取得
            // LEFT JOINで取得しているためNULLの場合もあるため
            entity.TORIHIKISAKI_CD = GetString_ByObject(row["TORIHIKISAKI_CD"]);
            entity.YEAR = GetShort_ByObject(row["YEAR"]);
            entity.MONTH = GetShort_ByObject(row["MONTH"]);
            entity.SEQ = GetInt_ByObject(row["SEQ"]);

            entity.UPDATE_SEQ = updateSeq;
            entity.DELETE_FLG = SqlBoolean.False.Value;

            // 画面情報を設定
            entity.ADJUST_TAX = GetDecimal_ByObject(row["ADJUST_TAX"]);
            entity.ZANDAKA = GetDecimal_ByObject(row["ZANDAKA"]);

            return entity;
        }

        #endregion

        #endregion

        #region 月次締処理データの変更有無

        /// <summary>
        /// 月次締処理データの更新有無判定
        /// </summary>
        /// <param name="dt">更新対象データ</param>
        /// <returns>true:変更有, false:変更無</returns>
        private bool HadChangedSeqMonthlyLock(DataTable dt)
        {
            // 対象の月次締処理データのSEQが検索時と同じかチェック
            // SEQがずれている場合、月次締処理が実行されたとみなしエラーとする

            bool result = false;

            if (dt == null || dt.Rows.Count == 0)
            {
                return result;
            }

            bool isUR = true;
            if (WINDOW_ID.T_GETSUJI_SHOUHIZEI_CHOSEI_NYURYOKU_SH == form.WindowId)
            {
                isUR = false;
            }

            // 最新データ取得
            var newDt = this.lockUrDao.GetIchiran(this.GetsujiYM.Year, this.GetsujiYM.Month, isUR);

            foreach (DataRow row in dt.Rows)
            {
                string torihikisakiCd = row["TORIHIKISAKI_CD"].ToString();
                int seq = Convert.ToInt32(row["SEQ"]);

                var exist = newDt.AsEnumerable().Where(n => n["TORIHIKISAKI_CD"].Equals(row["TORIHIKISAKI_CD"])
                                                         && !n["SEQ"].Equals(row["SEQ"]))
                                                .ToList()
                                                .Any();
                if (exist)
                {
                    // SEQがずれたデータが存在した場合は処理終了
                    result = true;
                    break;
                }
            }

            return result;
        }

        #endregion

        #region 共通処理

        /// <summary>
        /// データテーブルのクローンを作成
        /// </summary>
        /// <param name="dataTable">データテーブル</param>
        /// <returns></returns>
        private DataTable CreateCloneDataTable(DataTable dataTable)
        {
            var cloneTable = dataTable.Clone();

            foreach (DataRow dr in this.searchResult.Rows)
            {
                cloneTable.ImportRow(dr);
            }

            return cloneTable;
        }

        /// <summary>
        /// 文字列取得
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetString_ByObject(object value)
        {
            var result = value as string;
            if (result == null)
            {
                result = string.Empty;
            }

            return result;
        }

        /// <summary>
        /// short取得
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private short GetShort_ByObject(object value, short defaultValue = 0)
        {
            short result = defaultValue;
            if (value is short)
            {
                result = (short)value;
            }

            return result;
        }

        /// <summary>
        /// INT取得
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private int GetInt_ByObject(object value, int defaultValue = 0)
        {
            int result = defaultValue;
            if (value is int)
            {
                result = (int)value;
            }

            return result;
        }

        /// <summary>
        /// decimal取得
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private decimal GetDecimal_ByObject(object value, decimal defaultValue = 0m)
        {
            decimal result = defaultValue;
            if (value is decimal)
            {
                result = (decimal)value;
            }

            return result;
        }

        /// <summary>
        /// 調整後差引残高 を計算
        /// </summary>
        /// <param name="lockZandaka">調整前差引残高</param>
        /// <param name="adjustTax">消費税調整額</param>
        /// <returns></returns>
        internal decimal CalculateZandaka(object lockZandaka, object adjustTax, out bool catchErr)
        {
            catchErr = true;
            try
            {
                var zandaka = Convert.ToDecimal(lockZandaka);
                var tax = Convert.ToDecimal(adjustTax);

                // 調整前差引残高 + 消費税調整額 = 調整後差引残高
                var result = zandaka + tax;

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CalculateZandaka", ex);
                this.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                return 0;
            }
        }

        #endregion

        #region Not Use

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
        /// 論理削除処理
        /// </summary>
        public void LogicalDelete()
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
