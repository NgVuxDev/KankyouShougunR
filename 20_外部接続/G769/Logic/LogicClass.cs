using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Data.SqlTypes;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.FormManager;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.SmsResult.APP;
using Shougun.Core.ExternalConnection.SmsResult.DAO;
using Shougun.Core.ExternalConnection.SmsResult.DTO;

namespace Shougun.Core.ExternalConnection.SmsResult
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 定数
        /// <summary>一覧のカラム名</summary>
        /// <remarks>一覧に表示項目を追加した場合、ここにも追加すること</remarks>
        private static readonly List<string> ICHIRAN_COLUMN = new List<string>() { "sendFlg", "SAGYOU_DATE", "GENCHAKU_TIME", "sendJokyo", "HAISHA_JOKYO_NAME", "DENPYOU_SHURUI", "DENPYOU_NUMBER",
                                                                                    "SMS_GYOUSHA_CD", "SMS_GYOUSHA_NAME", "SMS_GENBA_CD", "SMS_GENBA_NAME", "RECEIVER_NAME", "MOBILE_PHONE_NUMBER",
                                                                                    "ERROR_CODE", "ERROR_DETAIL", "SEND_DATE", "SEND_USER" };
        #endregion

        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.SmsResult.Setting.ButtonSetting.xml";

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        internal UIHeader header { get; set; }

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm { get; set; }

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞロジッククラス
        /// </summary>
        internal SMSLogic smsLogic;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// システム設定のEntity
        /// </summary>
        internal M_SYS_INFO SysInfo;

        /// <summary>
        /// リクエスト送信前エラーフラグ
        /// </summary>
        internal bool reqErrFlg = false;

        #region DAO
        
        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ着信結果DAO
        /// </summary>
        private DAOClass dao;

        /// <summary>
        /// システム設定DAO
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// IM_KYOTENDao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        #endregion

        #region プロパティ

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞのEntity
        /// </summary>
        public T_SMS smsEntry;

        #endregion

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();

            this.msgLogic = new MessageBoxShowLogic();
            this.smsLogic = new SMSLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                //フォーム初期化
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.header = (UIHeader)parentForm.headerForm;

                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                // イベントの初期化
                this.EventInit(parentForm);

                // システム設定を取得する。
                this.SysInfo = this.sysInfoDao.GetAllDataForCode("0");

                // DataGridViewの位置設定
                this.form.bt_ptn1.Location = new Point(this.form.bt_ptn1.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 20);
                this.form.bt_ptn2.Location = new Point(this.form.bt_ptn2.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 20);
                this.form.bt_ptn3.Location = new Point(this.form.bt_ptn3.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 20);
                this.form.bt_ptn4.Location = new Point(this.form.bt_ptn4.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 20);
                this.form.bt_ptn5.Location = new Point(this.form.bt_ptn5.Location.X, this.form.customDataGridView1.Location.Y + this.form.customDataGridView1.Size.Height + 20);

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// header設定
        /// </summary>
        public void SetHeader(UIHeader header)
        {
            try
            {
                LogUtility.DebugMethodStart(header);
                this.header = header;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetHeader", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(header);
            }
        }

        /// <summary>
        /// 画面初期表示
        /// </summary>
        public void InitFrom()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //並び替えと明細の設定
                this.form.customSearchHeader1.Visible = true;
                this.form.customSearchHeader1.Location = new System.Drawing.Point(0, 158);
                this.form.customSearchHeader1.Size = new System.Drawing.Size(992, 26);
                this.form.customSortHeader1.Location = new System.Drawing.Point(0, 184);
                this.form.customSortHeader1.Size = new Size(992, 26);
                //明細部：　ブランク
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.TabIndex = 60;
                this.form.customDataGridView1.Location = new Point(0, 209);
                this.form.customDataGridView1.Size = new Size(992, 230);
                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Columns.Clear();
                //headForm初期
                Init_Header();
                
                // 検索条件の初期化
                this.Init_Item();
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitFrom", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// HeadForm初期表示
        /// </summary>
        public void Init_Header()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 拠点CD取得
                this.SetUserProfile();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Init_Header", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期表示
        /// </summary>
        public void Init_Item(bool initial = true)
        {
            try
            {
                LogUtility.DebugMethodStart();
                //汎用検索 : ブランク  
                this.form.searchString.Text = string.Empty;
                // 作業日（From、To）
                this.form.DATE_FROM.Value = parentForm.sysDate.Date;
                this.form.DATE_TO.Value = parentForm.sysDate.Date;
                // 日付種類
                this.form.DATE_SHURUI.Text = "1";
                // 伝票種類
                this.form.SMS_DENPYOU_SHURUI.Text = "9";
                // 受信者状態
                this.form.SMS_RECEIVER_STATUS.Text = "9";
                //業者CD、業者名 : ブランク
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME.Text = string.Empty;
                //現場CD、現場名 : ブランク               
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME.Text = string.Empty;
                //並び順をクリア
                this.form.customSortHeader1.ClearCustomSortSetting();
                this.form.customSearchHeader1.ClearCustomSearchSetting();

                //フォーカス移動
                if (this.form.searchString.Visible == true)
                {
                    this.form.searchString.Focus();
                }
                else
                {
                    this.form.SMS_DENPYOU_SHURUI.Focus();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Init_Item", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        /// <param name="parentForm">ベースフォーム</param>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            LogUtility.DebugMethodStart(parentForm);

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        /// <param name="parentForm">ベースフォーム</param>
        private void EventInit(BusinessBaseForm parentForm)
        {
            try
            {
                // SMS状況照会ボタン（F1）イベント作成
                parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);

                // 個別照会ボタン（F2）イベント作成
                parentForm.bt_func2.Click += new EventHandler(this.form.bt_func2_Click);

                // 条件クリアボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);

                // 検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);

                // 並び替えボタン(F10)イベント生成
                parentForm.bt_func10.Click += new EventHandler(this.form.bt_func10_Click);

                // フィルタボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

                // パターン一覧画面遷移イベント作成
                parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ユーザー定義情報設定処理
        /// </summary>
        internal void SetUserProfile()
        {
            LogUtility.DebugMethodStart();

            // ヘッダフォーム更新
            var parentForm = (BusinessBaseForm)this.form.Parent;
            var headerForm = (UIHeader)parentForm.headerForm;

            // XMLから拠点CDを取得
            const string XML_KYOTEN_CD_KEY_NAME = "拠点CD";
            CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
            headerForm.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, XML_KYOTEN_CD_KEY_NAME);
            if (!string.IsNullOrEmpty(headerForm.KYOTEN_CD.Text.ToString()))
            {
                headerForm.KYOTEN_CD.Text = headerForm.KYOTEN_CD.Text.ToString().PadLeft(headerForm.KYOTEN_CD.MaxLength, '0');

                // 拠点略称を設定
                this.CheckKyotenCd(headerForm.KYOTEN_CD, headerForm.KYOTEN_NAME_RYAKU);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <returns></returns>
        internal bool Clear()
        {
            // 作業日From～To
            this.form.DATE_FROM.Text = sysDate().ToString();
            this.form.DATE_TO.Text = sysDate().ToString();

            // 伝票種類（初期化）
            this.form.SMS_DENPYOU_SHURUI.Text = "9";

            // 受信者状態（初期化）
            this.form.SMS_RECEIVER_STATUS.Text = "9";

            // 業者CD、業者名
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME.Text = string.Empty;

            // 現場CD、現場名
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME.Text = string.Empty;

            // 拠点CD取得
            this.SetUserProfile();

            this.form.customSortHeader1.ClearCustomSortSetting();
            this.form.customSearchHeader1.ClearCustomSearchSetting();

            return true;
        }

        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            // 必須チェック
            if (!this.SearchCheck())
            {
                return 0;
            }

            //日付チェックを追加
            if (!this.DateCheck())
            {
                return 0;
            }

            // 検索
            var list = this.SearchSqlSetting();
            if (list != null)
            {
                //DataGridViewに値の設定を行う
                this.form.logic.CreateDataGridView(list);
                //DataGridViewのプロパティ再設定
                setDataGridView();
            }
            else
            {
                this.form.customDataGridView1.DataSource = null;
                this.form.customDataGridView1.Columns.Clear();
            }

            if (list.Rows.Count == 0)
            {
                this.msgLogic.MessageBoxShow("C001");
            }

            return 0;
        }

        /// <summary>
        /// DataGridViewのプロパティ再設定
        /// </summary>
        private void setDataGridView()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //行の追加オプション(false)
                this.form.customDataGridView1.AllowUserToAddRows = false;
                this.form.customDataGridView1.AllowUserToResizeColumns = true;
                setDataGridViewColumn(this.form.customDataGridView1);
                this.form.customDataGridView1.AllowUserToResizeRows = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDataGridView", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// Column非表示設定
        /// </summary>
        /// <param name="dtgv"></param>
        private void setDataGridViewColumn(CustomDataGridView dtgv)
        {
            try
            {
                LogUtility.DebugMethodStart(dtgv);
                // システムID
                if (dtgv.Columns.Contains(ConstCls.HIDDEN_SYSTEM_ID))
                {
                    dtgv.Columns[ConstCls.HIDDEN_SYSTEM_ID].Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("setDataGridViewColumn", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dtgv);
            }
        }

        /// <summary>
        /// ヘッダフォーム更新
        /// </summary>
        internal void RefreshHeaderForm()
        {
            // ヘッダフォーム更新
            var parentForm = (BusinessBaseForm)this.form.Parent;
            var headerForm = (UIHeader)parentForm.headerForm;
        }

        /// <summary>
        /// パターン一覧起動
        /// </summary>
        /// <returns></returns>
        internal void PatternIchiranOpen()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var sysID = this.form.OpenPatternIchiran();
                // 適用ボタンが押された場合
                if (!string.IsNullOrEmpty(sysID))
                {
                    this.form.SetPatternBySysId(sysID);
                    this.form.ShowData(this.form.Table);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranOpen", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// SMS送信結果取得APIリクエスト後
        /// </summary>
        internal void SmsSendResultGetAPI_After(string[] responseArray, SMSLogic smsLogic)
        {
            try
            {
                // リクエストが送れなかった場合は処理中断
                if (string.IsNullOrEmpty(responseArray[0]))
                {
                    reqErrFlg = true;
                }
                else
                {
                    // APIが正常である場合は、各項目を適用
                    if (!string.IsNullOrEmpty(responseArray[0]) && responseArray[0].Contains("100"))
                    {
                        // メッセージ状態コード
                        this.smsEntry.SMS_STATUS = SqlInt16.Parse(responseArray[1]);
                        // 送達結果コード
                        this.smsEntry.RECEIVER_STATUS = SqlInt16.Parse(responseArray[2]);
                        // キャリア
                        this.smsEntry.CARRIER = SqlInt16.Parse(responseArray[3]);
                        // ｼｮｰﾄﾒｯｾｰｼﾞ送信日時（空電）
                        this.smsEntry.SEND_DATE_KARADEN = SqlDateTime.Parse(responseArray[4]);
                    }
                    else if (string.IsNullOrEmpty(responseArray[0]) && !string.IsNullOrEmpty(responseArray[1]))
                    {
                        // エラーコード、エラー詳細
                        this.smsEntry.ERROR_CODE = responseArray[0];
                        string errDetail = smsLogic.SMSErrorSummarySetting(smsEntry.ERROR_CODE);
                        if (!string.IsNullOrEmpty(errDetail))
                        {
                            smsEntry.ERROR_DETAIL = smsLogic.SMSErrorSummarySetting(smsEntry.ERROR_CODE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LongSmsSplitSendAPI_After", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        #region 更新用Entity作成
        /// <summary>
        /// Entity作成
        /// </summary>
        internal void CreateEntity()
        {
            string systemId = this.form.customDataGridView1.CurrentRow.Cells[ConstCls.HIDDEN_SYSTEM_ID].Value.ToString();

            // T_SMSのEntity取得
            this.smsEntry = this.dao.GetDataBySystemId(systemId);

            // API関連項目、送信日、送信者はNULL
            smsEntry.SMS_STATUS = SqlInt16.Null;
            smsEntry.RECEIVER_STATUS = SqlInt16.Null;
            smsEntry.CARRIER = SqlInt16.Null;
            smsEntry.ERROR_CODE = null;
            smsEntry.ERROR_DETAIL = null;
            smsEntry.SEND_DATE_KARADEN = SqlDateTime.Null;

            var dataBinderDelivery = new DataBinderLogic<T_SMS>(smsEntry);
            dataBinderDelivery.SetSystemProperty(smsEntry, false);

            smsEntry.CREATE_DATE = smsEntry.CREATE_DATE;
            smsEntry.CREATE_PC = smsEntry.CREATE_PC;
            smsEntry.CREATE_USER = smsEntry.CREATE_USER;
            smsEntry.TIME_STAMP = smsEntry.TIME_STAMP;
        }
        #endregion

        #region 修正登録
        /// <summary>
        /// 修正登録
        /// </summary>
        internal void UpdateData(T_SMS entity)
        {
            try
            {
                using (Transaction tran = new Transaction())
                {
                    // entry
                    this.dao.Update(entity);

                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UpdateData", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 業者情報取得
        /// </summary>
        /// <param name="gosyaCd">業者CD</param>
        public M_GYOUSHA[] GetGyousyaInfo(string gosyaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gosyaCd);
                IM_GYOUSHADao gDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                M_GYOUSHA gEntity = new M_GYOUSHA();
                gEntity.GYOUSHA_CD = gosyaCd;
                gEntity.ISNOT_NEED_DELETE_FLG = true;
                //業者情報取得
                var returnEntitys = gDao.GetAllValidData(gEntity);
                LogUtility.DebugMethodEnd(returnEntitys, catchErr);
                return returnEntitys;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousyaInfo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousyaInfo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 現場情報取得
        /// </summary>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="gosyaCd">業者CD</param>
        /// <returns></returns>
        public M_GENBA[] GetGenbaInfo(string genbaCd, string gosyaCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(genbaCd, gosyaCd);
                IM_GENBADao gDao = DaoInitUtility.GetComponent<IM_GENBADao>();
                M_GENBA gEntity = new M_GENBA();
                //現場CD
                gEntity.GENBA_CD = genbaCd;
                //業者CD
                if (gosyaCd != "")
                {
                    gEntity.GYOUSHA_CD = gosyaCd;
                }
                gEntity.ISNOT_NEED_DELETE_FLG = true;
                //現場情報取得
                //現場マスタ（M_GENBA）を[業者CD]、[現場CD]で検索する
                M_GENBA[] returnEntitys = gDao.GetAllValidData(gEntity);
                LogUtility.DebugMethodEnd(returnEntitys, catchErr);
                return returnEntitys;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenbaInfo", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenbaInfo", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// JOIN句生成
        /// </summary>
        /// <returns></returns>
        private string CreateJoinQuery()
        {
            string joinQuery = string.Empty;

            if (this.form.SMS_DENPYOU_SHURUI.Text == "1")
            {
                // 1．収集
                joinQuery += " LEFT JOIN T_UKETSUKE_SS_ENTRY ON T_SMS.DENPYOU_NUMBER = T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER ";
            }
            else if (this.form.SMS_DENPYOU_SHURUI.Text == "2")
            {
                // 2．出荷
                joinQuery += " LEFT JOIN T_UKETSUKE_SK_ENTRY ON T_SMS.DENPYOU_NUMBER = T_UKETSUKE_SK_ENTRY.UKETSUKE_NUMBER ";
            }
            else if (this.form.SMS_DENPYOU_SHURUI.Text == "3")
            {
                // 3．持込
                joinQuery += " LEFT JOIN T_UKETSUKE_MK_ENTRY ON T_SMS.DENPYOU_NUMBER = T_UKETSUKE_MK_ENTRY.UKETSUKE_NUMBER ";
            }
            else if (this.form.SMS_DENPYOU_SHURUI.Text == "4")
            {
                // 4．収集+出荷
                joinQuery += " LEFT JOIN T_UKETSUKE_SS_ENTRY ON T_SMS.DENPYOU_NUMBER = T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER ";
                joinQuery += " LEFT JOIN T_UKETSUKE_SK_ENTRY ON T_SMS.DENPYOU_NUMBER = T_UKETSUKE_SK_ENTRY.UKETSUKE_NUMBER ";
            }
            else if (this.form.SMS_DENPYOU_SHURUI.Text == "5")
            {
                // 5．収集+持込
                joinQuery += " LEFT JOIN T_UKETSUKE_SS_ENTRY ON T_SMS.DENPYOU_NUMBER = T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER ";
                joinQuery += " LEFT JOIN T_UKETSUKE_MK_ENTRY ON T_SMS.DENPYOU_NUMBER = T_UKETSUKE_MK_ENTRY.UKETSUKE_NUMBER ";
            }
            else if (this.form.SMS_DENPYOU_SHURUI.Text == "6")
            {
                // 6．定期
                joinQuery += " LEFT JOIN T_TEIKI_HAISHA_ENTRY ON T_SMS.DENPYOU_NUMBER = T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER ";
            }
            else
            {
                // 9．全て
                joinQuery += " LEFT JOIN T_UKETSUKE_SS_ENTRY ON T_SMS.DENPYOU_NUMBER = T_UKETSUKE_SS_ENTRY.UKETSUKE_NUMBER ";
                joinQuery += " LEFT JOIN T_UKETSUKE_SK_ENTRY ON T_SMS.DENPYOU_NUMBER = T_UKETSUKE_SK_ENTRY.UKETSUKE_NUMBER ";
                joinQuery += " LEFT JOIN T_UKETSUKE_MK_ENTRY ON T_SMS.DENPYOU_NUMBER = T_UKETSUKE_MK_ENTRY.UKETSUKE_NUMBER ";
                joinQuery += " LEFT JOIN T_TEIKI_HAISHA_ENTRY ON T_SMS.DENPYOU_NUMBER = T_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER ";
            }

            return joinQuery;
        }

        /// <summary>
        /// 検索SQL設定
        /// </summary>
        /// <param name="denpyou">検索を行う伝票名</param>
        /// <param name="dto">検索条件</param>
        /// <returns></returns>
        private DataTable SearchSqlSetting()
        {
            var data = new DataTable();

            //SQL文格納StringBuilder
            var sql = new StringBuilder();

            //SELECT句未取得なら検索できない
            if (string.IsNullOrEmpty(this.form.SelectQuery))
            {
                return data;
            }

            #region SELECT句
            sql.Append(" SELECT DISTINCT ");
            //出力パターンよりのSQL
            sql.Append(this.form.SelectQuery);
            //システムID
            sql.AppendFormat(" ,T_SMS.SYSTEM_ID AS {0} ", ConstCls.HIDDEN_SYSTEM_ID);
            #endregion

            #region FROM句
            sql.Append(" FROM T_SMS ");

            // JOIN句設定
            sql.Append(this.CreateJoinQuery());
            #endregion

            #region WHERE句
            sql.Append(" WHERE MESSAGE_ID IS NOT NULL ");

            // 拠点CD
            if (!string.IsNullOrEmpty(header.KYOTEN_CD.Text) && header.KYOTEN_CD.Text != "99")
            {
                // 1．収集、2．出荷、3．持込、6．定期
                if (this.form.SMS_DENPYOU_SHURUI.Text == "1" ||
                    this.form.SMS_DENPYOU_SHURUI.Text == "2" ||
                    this.form.SMS_DENPYOU_SHURUI.Text == "3" ||
                    this.form.SMS_DENPYOU_SHURUI.Text == "6")
                {
                    sql.Append(" AND KYOTEN_CD= '" + header.KYOTEN_CD.Text + "' ");
                }
                // 4．収集+出荷
                else if (this.form.SMS_DENPYOU_SHURUI.Text == "4")
                {
                    sql.Append(" AND ( T_UKETSUKE_SS_ENTRY.KYOTEN_CD= '" + header.KYOTEN_CD.Text + "' ");
                    sql.Append(" OR T_UKETSUKE_SK_ENTRY.KYOTEN_CD= '" + header.KYOTEN_CD.Text + "' ) ");
                }
                // 5．収集+持込
                else if (this.form.SMS_DENPYOU_SHURUI.Text == "5")
                {
                    sql.Append(" AND ( T_UKETSUKE_SS_ENTRY.KYOTEN_CD= '" + header.KYOTEN_CD.Text + "' ");
                    sql.Append(" OR T_UKETSUKE_MK_ENTRY.KYOTEN_CD= '" + header.KYOTEN_CD.Text + "' ) ");
                }
                // 9．全て
                else
                {
                    sql.Append(" AND ( T_UKETSUKE_SS_ENTRY.KYOTEN_CD= '" + header.KYOTEN_CD.Text + "' ");
                    sql.Append(" OR T_UKETSUKE_SK_ENTRY.KYOTEN_CD= '" + header.KYOTEN_CD.Text + "' ");
                    sql.Append(" OR T_UKETSUKE_MK_ENTRY.KYOTEN_CD= '" + header.KYOTEN_CD.Text + "' ");
                    sql.Append(" OR T_TEIKI_HAISHA_ENTRY.KYOTEN_CD= '" + header.KYOTEN_CD.Text + "' ) ");
                }

            }

            // 検索日付種類
            if (this.form.DATE_SHURUI.Text == "1")
            {
                if (!string.IsNullOrEmpty(this.form.DATE_FROM.Text))
                {
                    sql.Append(" AND T_SMS.SAGYOU_DATE >= '" + DateTime.Parse(this.form.DATE_FROM.Text).ToShortDateString() + "' ");
                }
                if (!string.IsNullOrEmpty(this.form.DATE_TO.Text))
                {
                    sql.Append(" AND T_SMS.SAGYOU_DATE <= '" + DateTime.Parse(this.form.DATE_TO.Text).ToShortDateString() + "' ");
                }
            }
            else if (this.form.DATE_SHURUI.Text == "2")
            {
                if (!string.IsNullOrEmpty(this.form.DATE_FROM.Text))
                {
                    sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, SEND_DATE_R, 111), 120) >= '" + DateTime.Parse(this.form.DATE_FROM.Text).ToShortDateString() + "' ");
                }
                if (!string.IsNullOrEmpty(this.form.DATE_TO.Text))
                {
                    sql.Append(" AND CONVERT(DATETIME, CONVERT(nvarchar, SEND_DATE_R, 111), 120) <= '" + DateTime.Parse(this.form.DATE_TO.Text).ToShortDateString() + "' ");
                }
            }

            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                sql.Append(" AND T_SMS.GYOUSHA_CD = '" + this.form.GYOUSHA_CD.Text + "' ");
            }

            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                sql.Append(" AND T_SMS.GENBA_CD = '" + this.form.GENBA_CD.Text + "' ");
            }

            if (!string.IsNullOrEmpty(this.form.SMS_DENPYOU_SHURUI.Text) && this.form.SMS_DENPYOU_SHURUI.Text != "9")
            {
                if (this.form.SMS_DENPYOU_SHURUI.Text == "1")
                {
                    // 1．収集
                    sql.Append(" AND DENPYOU_SHURUI = 1");
                }
                else if (this.form.SMS_DENPYOU_SHURUI.Text == "2")
                {
                    // 2．出荷
                    sql.Append(" AND DENPYOU_SHURUI = 2");
                }
                else if (this.form.SMS_DENPYOU_SHURUI.Text == "3")
                {
                    // 3．持込
                    sql.Append(" AND DENPYOU_SHURUI = 3");
                }
                else if (this.form.SMS_DENPYOU_SHURUI.Text == "4")
                {
                    // 4．収集+出荷
                    sql.Append(" AND ( DENPYOU_SHURUI = 1 OR DENPYOU_SHURUI = 2)");
                }
                else if (this.form.SMS_DENPYOU_SHURUI.Text == "5")
                {
                    // 5．収集+持込
                    sql.Append(" AND ( DENPYOU_SHURUI = 1 OR DENPYOU_SHURUI = 3)");
                }
                else if (this.form.SMS_DENPYOU_SHURUI.Text == "6")
                {
                    // 6．定期 
                    sql.Append(" AND DENPYOU_SHURUI = 4");
                }
            }

            if (!string.IsNullOrEmpty(this.form.SMS_RECEIVER_STATUS.Text))
            {
                if (this.form.SMS_RECEIVER_STATUS.Text != "9")
                {
                    sql.Append(" AND RECEIVER_STATUS = '" + this.form.SMS_RECEIVER_STATUS.Text + "' ");
                }
            }

            #endregion

            #region ORDERBY句
            if (!string.IsNullOrEmpty(this.form.OrderByQuery))
            {
                sql.Append(" ORDER BY ");
                sql.Append(this.form.OrderByQuery);
            }
            #endregion

            data = this.dao.GetDataForSql(sql.ToString());

            return data;
        }

        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        private bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.DATE_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.DATE_FROM.Text))
            {
                return true;
            }
            if (string.IsNullOrEmpty(this.form.DATE_TO.Text))
            {
                return true;
            }

            DateTime date_from = DateTime.Parse(this.form.DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.DATE_FROM.IsInputErrorOccured = true;
                this.form.DATE_TO.IsInputErrorOccured = true;
                this.form.DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.DATE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "日付From", "日付To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.DATE_FROM.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region 拠点CD取得
        
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        private string GetUserProfileValue(CurrentUserCustomConfigProfile profile, string key)
        {
            LogUtility.DebugMethodStart(profile, key);

            string result = string.Empty;

            foreach (CurrentUserCustomConfigProfile.SettingsCls.ItemSettings item in profile.Settings.DefaultValue)
            {
                if (item.Name.Equals(key))
                {
                    result = item.Value;
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd(CustomNumericTextBox2 headerKyotenCd, CustomTextBox headerKyotenNameRyaku)
        {
            // 初期化
            headerKyotenNameRyaku.Text = string.Empty;

            if (string.IsNullOrEmpty(headerKyotenCd.Text))
            {
                headerKyotenNameRyaku.Text = string.Empty;
                return;
            }

            short kyoteCd = -1;
            if (!short.TryParse(string.Format("{0:#,0}", headerKyotenCd.Text), out kyoteCd))
            {
                return;
            }

            var kyotens = this.GetDataByCodeForKyoten(kyoteCd);

            // 存在チェック
            if (kyotens == null || kyotens.Length < 1)
            {
                this.msgLogic.MessageBoxShow("E020", "拠点");
                headerKyotenCd.Focus();
                return;
            }
            else
            {
                // キーが１つなので複数はヒットしないはず
                M_KYOTEN kyoten = kyotens[0];
                headerKyotenNameRyaku.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
            }
        }

        /// <summary>
        /// 拠点を取得
        /// 適用開始日、適用終了日、削除フラグについては
        /// 自動でWHERE句を生成するためこのメソッドでは指定する必要はない。
        /// </summary>
        /// <param name="kyotenCd">KYOTEN_CD</param>
        /// <returns></returns>
        internal M_KYOTEN[] GetDataByCodeForKyoten(short kyotenCd)
        {
            M_KYOTEN keyEntity = new M_KYOTEN();
            keyEntity.KYOTEN_CD = kyotenCd;
            return this.kyotenDao.GetAllValidData(keyEntity);
        }

        #endregion

        #region 検索前チェック

        /// <summary>
        /// 検索前チェック
        /// </summary>
        /// <returns></returns>
        private bool SearchCheck()
        {
            string errMsg = string.Empty;

            // 日付
            if (string.IsNullOrEmpty(this.form.DATE_FROM.Text) && string.IsNullOrEmpty(this.form.DATE_TO.Text))
            {
                errMsg += "日付を入力してください。" + "\r\n";
                if(string.IsNullOrEmpty(this.form.DATE_FROM.Text))
                {
                    this.form.DATE_FROM.BackColor = ConstCls.ERROR_COLOR;
                }
                if(string.IsNullOrEmpty(this.form.DATE_TO.Text))
                {
                    this.form.DATE_TO.BackColor = ConstCls.ERROR_COLOR;
                }
            }

            // 日付種類
            if (string.IsNullOrEmpty(this.form.DATE_SHURUI.Text))
            {
                errMsg += "日付種類を入力してください。" + "\r\n";
                this.form.DATE_SHURUI.BackColor = ConstCls.ERROR_COLOR;
            }

            // 伝票種類
            if (string.IsNullOrEmpty(this.form.SMS_DENPYOU_SHURUI.Text))
            {
                errMsg += "伝票種類を入力してください。" + "\r\n";
                this.form.SMS_DENPYOU_SHURUI.BackColor = ConstCls.ERROR_COLOR;
            }

            // 受信者状態
            if (string.IsNullOrEmpty(this.form.SMS_RECEIVER_STATUS.Text))
            {
                errMsg += "受信者状態を入力してください。" + "\r\n";
                this.form.SMS_RECEIVER_STATUS.BackColor = ConstCls.ERROR_COLOR;
            }

            if (!string.IsNullOrEmpty(errMsg))
            {
                this.msgLogic.MessageBoxShowError(errMsg);
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region システム日付の取得

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private SqlDateTime sysDate()
        {
            DateTime now = DateTime.Now;
            GET_SYSDATEDao dao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            DataTable dt = dao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return SqlDateTime.Parse(now.ToString());
        }

        #endregion

        #region 未使用
        #region 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 更新
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 論理削除
        /// <summary>
        /// 論理削除
        /// </summary>
        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #region 物理削除
        /// <summary>
        /// 物理削除
        /// </summary>
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
