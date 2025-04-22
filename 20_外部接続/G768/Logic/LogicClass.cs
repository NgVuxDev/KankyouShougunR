using System;
using System.Collections.Generic;
using System.Data;
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
using Shougun.Core.ExternalConnection.SmsIchiran.APP;
using Shougun.Core.ExternalConnection.SmsIchiran.DAO;
using Shougun.Core.ExternalConnection.SmsIchiran.DTO;

namespace Shougun.Core.ExternalConnection.SmsIchiran.Logic
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
                                                                                    "SEQ", "COURSE_NAME_CD", "COURSE_NAME","ROW_NUMBER", "ROUND_NO", "SMS_GYOUSHA_CD", "SMS_GYOUSHA_NAME", 
                                                                                    "SMS_GENBA_CD", "SMS_GENBA_NAME", "RECEIVER_NAME", "MOBILE_PHONE_NUMBER", "ERROR_CODE", "ERROR_DETAIL", "SEND_DATE", "SEND_USER" };

        /// <summary>伝票種類テーブル名</summary>
        /// <summary>収集</summary>
        private static readonly string T_UKETSUKE_SS_ENTRY = "T_UKETSUKE_SS_ENTRY";
        /// <summary>出荷</summary>
        private static readonly string T_UKETSUKE_SK_ENTRY = "T_UKETSUKE_SK_ENTRY";
        /// <summary>持込</summary>
        private static readonly string T_UKETSUKE_MK_ENTRY = "T_UKETSUKE_MK_ENTRY";
        /// <summary>定期</summary>
        private static readonly string T_TEIKI_HAISHA_ENTRY = "T_TEIKI_HAISHA_ENTRY";
        #endregion

        #region フィールド
        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.SmsIchiran.Setting.ButtonSetting.xml";

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        internal UIHeader headerForm { get; set; }

        /// <summary>
        /// ベースフォーム
        /// </summary>
        internal BusinessBaseForm parentForm { get; set; }

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞロジッククラス
        /// </summary>
        internal SMSLogic smsLogic;

        /// <summary>
        /// Form
        /// </summary>
        internal UIForm form;

        /// <summary>
        /// システム設定のEntity
        /// </summary>
        internal M_SYS_INFO SysInfo;

        /// <summary>
        /// 使用する伝票名
        /// </summary>
        private string denpyouName;

        /// <summary>
        /// 使用するテーブル名（4．収集+出荷、5．収集+持込用）
        /// </summary>
        private string unionTableName;

        /// <summary>
        /// リクエスト送信前エラーフラグ
        /// </summary>
        internal bool reqErrFlg = false;

        #region DAO
        
        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ一覧DAO
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

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者連携現場Dao
        /// </summary>
        private IM_SMS_RECEIVER_LINK_GENBADao smsReceiverLinkGenbaDao;

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
            this.smsReceiverLinkGenbaDao = DaoInitUtility.GetComponent<IM_SMS_RECEIVER_LINK_GENBADao>();

            this.msgLogic = new MessageBoxShowLogic();
            this.smsLogic = new SMSLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //フォーム初期化
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.headerForm = (UIHeader)parentForm.headerForm;

                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                // イベントの初期化
                this.EventInit(parentForm);

                // システム設定を取得する。
                this.SysInfo = this.sysInfoDao.GetAllDataForCode("0");

                #region ヘッダーの初期化

                // 拠点CD取得
                this.SetUserProfile();

                // 送信状況取得、項目設定
                if (!this.SysInfo.SMS_SEND_JOKYO.IsNull)
                {
                    headerForm.SEND_JOKYO.Text = this.SysInfo.SMS_SEND_JOKYO.ToString();
                }
                else
                {
                    headerForm.SEND_JOKYO.Text = "1";
                }

                //アラート件数の初期値セット
                if (!this.SysInfo.ICHIRAN_ALERT_KENSUU.IsNull)
                {
                    this.headerForm.alertNumber.Text = this.SysInfo.ICHIRAN_ALERT_KENSUU.ToString();
                }
                headerForm.readDataNumber.Text = "0";

                #endregion

                // 検索条件項目の初期設定
                this.SearchConditionInit(SysInfo);

                this.form.Ichiran.AutoGenerateColumns = false;
                this.form.Ichiran.DataSource = CreateEmptyDataTable();

                this.HeaderCheckBoxSupport();

                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
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
                // SMS送信ボタン（F1）イベント作成
                parentForm.bt_func1.Click += new EventHandler(this.form.bt_func1_Click);

                // 新規ボタン（F2）イベント作成
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

                // 着信一覧ボタン（[1]）イベント作成
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
        /// 送信状況の値より、各項目設定
        /// </summary>
        internal void SmsStatusSetting()
        {
            try
            {
                if (this.parentForm != null)
                {
                    if (this.form.Ichiran.Rows.Count > 0)
                    {
                        // 一覧をクリア
                        this.form.Ichiran.DataSource = null;
                        // 読込データ件数
                        headerForm.readDataNumber.Text = "0";
                    }

                    // 伝票種類が6．定期時、明細行追加（コースCD・名、順番、回数）
                    if (this.form.SMS_DENPYOU_SHURUI.Text == "6")
                    {
                        this.IchiranColumnSetting_Teiki(true);
                    }
                    else
                    {
                        this.IchiranColumnSetting_Teiki(false);
                    }

                    // 送信状況=1．未送信である場合
                    if (headerForm.SEND_JOKYO.Text == "1")
                    {
                        this.parentForm.bt_func1.Enabled = true;

                        if (this.form.SMS_DENPYOU_SHURUI.Text != "3" && 
                            this.form.SMS_DENPYOU_SHURUI.Text != "5" && 
                            this.form.SMS_DENPYOU_SHURUI.Text != "6")
                        {
                            this.form.haishaJokyoPanel.Enabled = true;
                            if (!this.SysInfo.SMS_HAISHA_JOKYO.IsNull)
                            {
                                this.form.SMS_HAISHA_JOKYO.Text = this.SysInfo.SMS_HAISHA_JOKYO.ToString();
                            }
                            else
                            {
                                this.form.SMS_HAISHA_JOKYO.Text = "1";
                            }
                        }

                        
                    }
                    else if(headerForm.SEND_JOKYO.Text == "2")
                    {
                        this.parentForm.bt_func1.Enabled = false;
                        this.form.haishaJokyoPanel.Enabled = false;
                        this.form.SMS_HAISHA_JOKYO.Text = string.Empty;
                    }
                    else if (headerForm.SEND_JOKYO.Text == "3")
                    {
                        this.parentForm.bt_func1.Enabled = true;
                        this.form.haishaJokyoPanel.Enabled = false;
                        this.form.SMS_HAISHA_JOKYO.Text = string.Empty;
                    }
                    else
                    {
                        this.msgLogic.MessageBoxShowError("送信状況に正しい値を入力してください。");
                    }
                }
            }
            catch(Exception ex)
            {
                LogUtility.Error("SmsStatusSetting", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
        }

        /// <summary>
        /// 一覧列設定（定期）
        /// </summary>
        /// <param name="teikiFlg"></param>
        internal void IchiranColumnSetting_Teiki(bool teikiFlg)
        {
            if (teikiFlg)
            {
                // コース名CD、コース名、順番、回数を可視化
                this.form.Ichiran.Columns["COURSE_NAME_CD"].Visible = true;
                this.form.Ichiran.Columns["COURSE_NAME"].Visible = true;
                this.form.Ichiran.Columns["ROW_NUMBER"].Visible = true;
                this.form.Ichiran.Columns["ROUND_NO"].Visible = true;
            }
            else
            {
                // コース名CD、コース名、順番、回数を非可視化
                this.form.Ichiran.Columns["COURSE_NAME_CD"].Visible = false;
                this.form.Ichiran.Columns["COURSE_NAME"].Visible = false;
                this.form.Ichiran.Columns["ROW_NUMBER"].Visible = false;
                this.form.Ichiran.Columns["ROUND_NO"].Visible = false;
            }
        }

        /// <summary>
        /// ユーザー定義情報設定処理
        /// </summary>
        private void SetUserProfile()
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
        /// 検索条件項目の初期値設定
        /// </summary>
        /// <param name="sysInfoEntity"></param>
        private void SearchConditionInit(M_SYS_INFO sysInfoEntity)
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 作業日（From、To）
            this.form.SAGYOU_DATE_FROM.Value = parentForm.sysDate.Date;
            this.form.SAGYOU_DATE_TO.Value = parentForm.sysDate.Date;

            // 伝票種類
            if (!sysInfoEntity.SMS_DENPYOU_SHURUI.IsNull)
            {
                this.form.SMS_DENPYOU_SHURUI.Text = sysInfoEntity.SMS_DENPYOU_SHURUI.ToString();
            }
        }

        /// <summary>
        /// 空のデータテーブル作成
        /// </summary>
        /// <returns></returns>
        private DataTable CreateEmptyDataTable()
        {
            // 並び替え、フィルタの初期表示対策
            DataTable dt = new DataTable();

            ICHIRAN_COLUMN.ForEach(n => dt.Columns.Add(n));

            return dt;
        }

        /// <summary>
        /// 新規
        /// </summary>
        /// <returns></returns>
        internal void ChangeNewWindow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.Ichiran.CurrentRow == null)
                {
                    this.msgLogic.MessageBoxShowError("登録するデータが選択されていません。");
                    return;
                }

                // ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報設定
                string[] paramList = this.SmsParamListSetting();

                List<int> smsReceiverList = this.SmsReceiverListSetting();

                // ｼｮｰﾄﾒｯｾｰｼﾞ受信者が0件である場合、エラー表示
                if (smsReceiverList.Count == 0 || paramList == null)
                {
                    this.msgLogic.MessageBoxShowError("現場入力（マスタ）に受信者情報が登録されていません。\r\n受信者情報を登録してください。");
                    return;
                }
                // 不具合等でｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報が存在しない場合は、エラー表示
                if (smsReceiverList.Count == 0 || paramList == null)
                {
                    this.msgLogic.MessageBoxShowError("ｼｮｰﾄﾒｯｾｰｼﾞ入力への連携処理に失敗しました。");
                    return;
                }
                //入力画面へ遷移する（新規モード）
                FormManager.OpenForm("G767", smsReceiverList, WINDOW_TYPE.NEW_WINDOW_FLAG, WINDOW_ID.T_SMS_ICHIRAN, paramList);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChangeNewWindow", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 条件クリア
        /// </summary>
        /// <returns></returns>
        internal bool Clear()
        {
            // 作業日From～To
            this.form.SAGYOU_DATE_FROM.Text = string.Empty;
            this.form.SAGYOU_DATE_TO.Text = string.Empty;

            // 業者CD、業者名
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME.Text = string.Empty;

            // 現場CD、現場名
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME.Text = string.Empty;

            // 運搬業者CD、運搬業者名
            this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
            this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;

            #region ヘッダーの初期化

            // 拠点CD取得
            this.SetUserProfile();

            // 送信状況取得、項目設定
            if (!this.SysInfo.SMS_SEND_JOKYO.IsNull)
            {
                headerForm.SEND_JOKYO.Text = this.SysInfo.SMS_SEND_JOKYO.ToString();
            }
            else
            {
                headerForm.SEND_JOKYO.Text = "1";
            }

            //アラート件数の初期値セット
            if (!this.SysInfo.ICHIRAN_ALERT_KENSUU.IsNull)
            {
                this.headerForm.alertNumber.Text = this.SysInfo.ICHIRAN_ALERT_KENSUU.ToString();
            }
            headerForm.readDataNumber.Text = "0";

            #endregion

            // 検索条件項目の初期設定
            this.SearchConditionInit(SysInfo);

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
            try
            {
                // 必須チェック
                if (!this.SearchCheck())
                {
                    return 1;
                }

                //日付チェックを追加
                if (!this.DateCheck())
                {
                    return 1;
                }

                // 検索条件作成
                var dto = CreateSearchDto();

                // 伝票種類を確認
                this.DenpyouShuruiCheck(this.form.SMS_DENPYOU_SHURUI.Text);

                // 検索
                var list = this.SearchSqlSetting(dto);
                this.form.Ichiran.DataSource = list;

                // ソート、並び替え対応
                this.form.customSortHeader1.SortDataTable(list);
                this.form.customSearchHeader1.SearchDataTable(list);

                // ヘッダフォーム更新
                RefreshHeaderForm();

                if (list.Rows.Count == 0)
                {
                    this.msgLogic.MessageBoxShow("C001");
                }

                return 0;
            }
            catch (DataException dataEx)
            {
                LogUtility.Error("Search", dataEx);
                return 1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                throw;
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

            if (this.form.Ichiran != null)
            {
                headerForm.readDataNumber.Text = this.form.Ichiran.Rows.Count.ToString();
            }
            else
            {
                headerForm.readDataNumber.Text = "0";
            }
        }


        /// <summary>
        /// 着信一覧起動
        /// </summary>
        /// <returns></returns>
        internal void IchiranOpen()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool retResult;
                // 現場メモ一覧を表示
                retResult = FormManager.OpenFormWithAuth("G769", WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
            }
            catch (Exception ex)
            {
                // 例外エラー
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShowError("着信結果　表示不可");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 送信チェックボックスのオンチェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SmsSendCheck()
        {
            LogUtility.DebugMethodStart();

            Boolean rtn = false;

            try
            {
                // 連携チェックボックスの値チェック
                int SendCheckCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells["sendFlg"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["sendFlg"].Value)
                    {
                        SendCheckCount++;
                    }
                }

                rtn = (SendCheckCount > 0);

                if(!rtn)
                {
                    this.msgLogic.MessageBoxShowError("チェックされている明細がありません。\r\nショートメッセージを送信する明細にチェックを付けてください。");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SmsSendCheck", ex);
                this.msgLogic.MessageBoxShow("E245");
                rtn = false;
            }

            LogUtility.DebugMethodEnd();
            return rtn;
        }

        /// <summary>
        /// 送信メッセージ設定
        /// </summary>
        /// <param name="index">行番号</param>
        internal void PrameSet(int index)
        {
            LogUtility.DebugMethodStart();
            try
            {
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                M_SYS_INFO sysInfo = new DBAccessor().GetSysInfo();

                string gyoushaCd = this.form.Ichiran.Rows[index].Cells["SMS_GYOUSHA_CD"].Value.ToString();
                string genbaCd = this.form.Ichiran.Rows[index].Cells["SMS_GENBA_CD"].Value.ToString();
                string genbaName = this.form.Ichiran.Rows[index].Cells["SMS_GENBA_NAME"].Value.ToString();

                string denoyouShuruiCd = this.DenpyouShuruiValueChange(this.form.Ichiran.Rows[index].Cells["DENPYOU_SHURUI"].Value.ToString());
                string haishaJokyoName = this.form.Ichiran.Rows[index].Cells["HAISHA_JOKYO_NAME"].Value.ToString();

                // 件名
                this.form.sendPrame.Add(smsLogic.SubjectSetting(denoyouShuruiCd, haishaJokyoName));

                // 挨拶文
                // システム個別設定入力の挨拶文を参照
                string gre = this.GetUserProfileValue(userProfile, "挨拶文");
                if (string.IsNullOrEmpty(gre))
                {
                    // システム個別設定入力で挨拶文の入力が無い場合、
                    // システム設定入力の挨拶文を参照
                    gre = sysInfo.SMS_GREETINGS;
                }
                this.form.sendPrame.Add(gre);

                // 本文
                string[] textArray = smsLogic.TextInitSetting(denoyouShuruiCd,
                                        haishaJokyoName,
                                        ((DateTime)this.form.Ichiran.Rows[index].Cells["SAGYOU_DATE"].Value).ToString("yyyy/MM/dd(ddd)"),
                                        this.form.Ichiran.Rows[index].Cells["GENCHAKU_TIME"].Value.ToString(),
                                        gyoushaCd,
                                        genbaCd,
                                        genbaName);
                this.form.sendPrame.Add(textArray[0]);
                this.form.sendPrame.Add(textArray[1]);
                this.form.sendPrame.Add(textArray[2]);
                this.form.sendPrame.Add(textArray[3]);

                // 署名
                string sig = this.GetUserProfileValue(userProfile, "署名");
                if (string.IsNullOrEmpty(sig))
                {
                    // システム個別設定入力で署名の入力が無い場合、
                    // システム設定入力の署名を参照
                    sig = sysInfo.SMS_SIGNATURE;
                }
                this.form.sendPrame.Add(sig);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SendMessageSetting", ex);
                this.msgLogic.MessageBoxShow("E245");
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// SMS長文分割送信APIリクエスト後
        /// </summary>
        internal void LongSmsSplitSendAPI_After(T_SMS smsEntity, string[] responseArray, SMSLogic smsLogic)
        {
            try
            {
                // リクエストが送れなかった場合は処理中断
                if (string.IsNullOrEmpty(responseArray[0]) && string.IsNullOrEmpty(responseArray[1]))
                {
                    reqErrFlg = true;
                }
                else
                {
                    // APIが正常である場合は、メッセージIDのみを適用
                    if (!string.IsNullOrEmpty(responseArray[0]))
                    {
                        // メッセージID
                        smsEntity.MESSAGE_ID = responseArray[0];
                    }
                    else if (string.IsNullOrEmpty(responseArray[0]) && !string.IsNullOrEmpty(responseArray[1]))
                    {
                        // エラーコード、エラー詳細
                        smsEntity.ERROR_CODE = responseArray[1];
                        string errDetail = smsLogic.SMSErrorSummarySetting(smsEntity.ERROR_CODE);
                        if (!string.IsNullOrEmpty(errDetail))
                        {
                            smsEntity.ERROR_DETAIL = smsLogic.SMSErrorSummarySetting(smsEntity.ERROR_CODE);
                        }
                    }

                    // 送信日、送信者はAPIの結果に関係なく適用
                    smsEntity.SEND_DATE_R = this.sysDate();
                    smsEntity.SEND_USER = SystemProperty.UserName;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LongSmsSplitSendAPI_After", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        #region 新規用Entity作成
        /// <summary>
        /// Entity作成
        /// </summary>
        internal T_SMS CreateEntity(int index, string mobilePhoneNumber, string receiverName)
        {
            var registEntity = new T_SMS();

            string systemId = string.Empty;
            if (!string.IsNullOrEmpty(this.form.Ichiran.Rows[index].Cells["SYSTEM_ID"].FormattedValue.ToString()))
            {
                systemId = this.form.Ichiran.Rows[index].Cells["SYSTEM_ID"].Value.ToString();
            }
            else
            {
                systemId = this.dao.GetMaxSystemId();
            }
            string denNo = this.form.Ichiran.Rows[index].Cells["DENPYOU_NUMBER"].Value.ToString();

            // T_SMSのEntity取得
            var smsEntry = this.dao.GetSearchSMSEntity(systemId, denNo);
            if (smsEntry != null)
            {
                registEntity.SYSTEM_ID = SqlInt32.Parse(systemId);
                registEntity.SAGYOU_DATE = smsEntry.SAGYOU_DATE;
                registEntity.GENCHAKU_TIME = smsEntry.GENCHAKU_TIME;
                registEntity.DENPYOU_SHURUI = smsEntry.DENPYOU_SHURUI;
                // 伝票種類＝1．収集、2．出荷である場合、配車状況名を設定
                if (smsEntry.DENPYOU_SHURUI == 1 || smsEntry.DENPYOU_SHURUI == 2)
                {
                    registEntity.HAISHA_JOKYO_NAME = smsEntry.HAISHA_JOKYO_NAME;
                }
                // 伝票種類＝4．定期である場合、行番号を設定
                if (smsEntry.DENPYOU_SHURUI == 4)
                {
                    registEntity.SEQ = smsEntry.SEQ;
                    registEntity.ROW_NUMBER = smsEntry.ROW_NUMBER;
                }
                registEntity.DENPYOU_NUMBER = smsEntry.DENPYOU_NUMBER;
                registEntity.GYOUSHA_CD = smsEntry.GYOUSHA_CD;
                registEntity.GYOUSHA_NAME = smsEntry.GYOUSHA_NAME;
                registEntity.GENBA_CD = smsEntry.GENBA_CD;
                registEntity.GENBA_NAME = smsEntry.GENBA_NAME;
                registEntity.RECEIVER_NAME = smsEntry.RECEIVER_NAME;
                registEntity.MOBILE_PHONE_NUMBER = smsEntry.MOBILE_PHONE_NUMBER;
            }
            else
            {
                registEntity.SYSTEM_ID = SqlInt32.Parse(this.dao.GetMaxSystemId());
                if (!string.IsNullOrEmpty(this.form.Ichiran.Rows[index].Cells["SAGYOU_DATE"].FormattedValue.ToString()))
                {
                    var date = this.form.Ichiran.Rows[index].Cells["SAGYOU_DATE"].Value.ToString();
                    var dateLength = date.Length;
                    registEntity.SAGYOU_DATE = date.Remove(dateLength - 7, 7).ToString();
                }
                if (!string.IsNullOrEmpty(this.form.Ichiran.Rows[index].Cells["GENCHAKU_TIME"].FormattedValue.ToString()))
                {
                    registEntity.GENCHAKU_TIME = this.form.Ichiran.Rows[index].Cells["GENCHAKU_TIME"].Value.ToString();
                }
                registEntity.DENPYOU_SHURUI = SqlInt16.Parse(this.DenpyouShuruiSetting(this.form.Ichiran.Rows[index].Cells["DENPYOU_SHURUI"].Value.ToString()));
                if (!string.IsNullOrEmpty(this.form.Ichiran.Rows[index].Cells["HAISHA_JOKYO_NAME"].FormattedValue.ToString()))
                {
                    registEntity.HAISHA_JOKYO_NAME = this.form.Ichiran.Rows[index].Cells["HAISHA_JOKYO_NAME"].Value.ToString();
                }
                registEntity.DENPYOU_NUMBER = SqlInt64.Parse(this.form.Ichiran.Rows[index].Cells["DENPYOU_NUMBER"].Value.ToString());
                if (!string.IsNullOrEmpty(this.form.Ichiran.Rows[index].Cells["ROW_NUMBER"].FormattedValue.ToString()))
                {
                    registEntity.SEQ = SqlInt32.Parse(this.dao.GetMaxTeikiSeq(denpyouName, denNo));
                    registEntity.ROW_NUMBER = SqlInt16.Parse(this.form.Ichiran.Rows[index].Cells["ROW_NUMBER"].Value.ToString());
                }
                registEntity.GYOUSHA_CD = this.form.Ichiran.Rows[index].Cells["SMS_GYOUSHA_CD"].Value.ToString();
                registEntity.GYOUSHA_NAME = this.form.Ichiran.Rows[index].Cells["SMS_GYOUSHA_NAME"].Value.ToString();
                registEntity.GENBA_CD = this.form.Ichiran.Rows[index].Cells["SMS_GENBA_CD"].Value.ToString();
                registEntity.GENBA_NAME = this.form.Ichiran.Rows[index].Cells["SMS_GENBA_NAME"].Value.ToString();
                registEntity.RECEIVER_NAME = receiverName;
                registEntity.MOBILE_PHONE_NUMBER = mobilePhoneNumber;
            }
            // API関連項目、送信日、送信者はNULL
            registEntity.MESSAGE_ID = null;
            registEntity.ERROR_CODE = null;
            registEntity.ERROR_DETAIL = null;
            registEntity.SEND_DATE_R = SqlDateTime.Null;
            registEntity.SEND_USER = null;

            var dataBinderDelivery = new DataBinderLogic<T_SMS>(registEntity);
            dataBinderDelivery.SetSystemProperty(registEntity, false);

            return registEntity;
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        internal void RegistData(T_SMS entity)
        {
            try
            {
                using (Transaction tran = new Transaction())
                {
                    T_SMS searchEntity = this.dao.GetSearchSMSEntity(entity.SYSTEM_ID.ToString(), entity.DENPYOU_NUMBER.ToString());
                    if (searchEntity == null)
                    {
                        this.dao.Insert(entity);
                    }
                    else
                    {
                        // データがある場合TIME_STAMP設定
                        entity.TIME_STAMP = (byte[])searchEntity.TIME_STAMP;
                        this.dao.Update(entity);
                    }
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistData", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E080");
                    throw;
                }
                else if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093");
                    throw;
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245");
                    throw;
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
        /// 伝票種類　値変更
        /// </summary>
        /// <param name="denpyouShurui"></param>
        private string DenpyouShuruiValueChange(string denpyouShurui)
        {
            string changeValue = string.Empty;

            switch(denpyouShurui)
            {
                case "収集":
                    changeValue = "1";
                    break;
                case "出荷":
                    changeValue = "2";
                    break;
                case "持込":
                    changeValue = "3";
                    break;
                case "定期":
                    changeValue = "4";
                    break;
                default:
                    break;
            }

            return changeValue;
        }

        /// <summary>
        /// 検索条件用のDTO作成
        /// </summary>
        /// <returns></returns>
        private SearchDTO CreateSearchDto()
        {
            var dto = new SearchDTO();

            if (!string.IsNullOrEmpty(this.headerForm.KYOTEN_CD.Text) && this.headerForm.KYOTEN_CD.Text != "99")
            {
                dto.KYOTEN_CD = SqlInt16.Parse(this.headerForm.KYOTEN_CD.Text).ToString();
            }
            if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE_FROM.Text))
            {
                dto.SAGYOU_DATE_FROM = this.form.SAGYOU_DATE_FROM.Value.ToString();
            }
            if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE_TO.Text))
            {
                dto.SAGYOU_DATE_TO = this.form.SAGYOU_DATE_TO.Value.ToString();
            }
            if (!string.IsNullOrEmpty(this.headerForm.SEND_JOKYO.Text))
            {
                dto.SEND_JOKYO = int.Parse(this.headerForm.SEND_JOKYO.Text);
            }
            if (!string.IsNullOrEmpty(this.form.SMS_HAISHA_JOKYO.Text))
            {
                dto.HAISHA_JOKYO = this.form.SMS_HAISHA_JOKYO.Text;
            }
            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                dto.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            }
            if (!string.IsNullOrEmpty(form.GENBA_CD.Text))
            {
                dto.GENBA_CD = this.form.GENBA_CD.Text;
            }
            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                dto.UNPAN_GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }

            return dto;
        }

        /// <summary>
        /// 検索SQL設定
        /// </summary>
        /// <param name="denpyou">検索を行う伝票名</param>
        /// <param name="dto">検索条件</param>
        /// <returns></returns>
        private DataTable SearchSqlSetting(SearchDTO dto)
        {
            var data = new DataTable();

            if (denpyouName == T_UKETSUKE_SS_ENTRY)
            {
                // 伝票種類が1．収集以外の場合
                if (!string.IsNullOrEmpty(unionTableName))
                {
                    if (unionTableName == T_UKETSUKE_SK_ENTRY)
                    {
                        // 4．収集+出荷
                        data = this.dao.GetIchiranSS_SKDataSql(dto);
                    }
                    else
                    {
                        // 5．収集+持込
                        data = this.dao.GetIchiranSS_MKDataSql(dto);
                    }
                }
                else
                {
                    // 1．収集
                    data = this.dao.GetIchiranSSDataSql(dto);
                }
            }
            else if (denpyouName == T_UKETSUKE_SK_ENTRY)
            {
                // 2．出荷
                data = this.dao.GetIchiranSKDataSql(dto);
            }
            else if (denpyouName == T_UKETSUKE_MK_ENTRY)
            {
                // 3．持込
                data = this.dao.GetIchiranMKDataSql(dto);
            }
            else if (denpyouName == T_TEIKI_HAISHA_ENTRY)
            {
                // 6．定期
                data = this.dao.GetIchiranTEIKIDataSql(dto);

                // 検索に使用する項目
                string denpyouNum = string.Empty; // 伝票番号
                string rowNum = string.Empty; // 行番号
                string seq = string.Empty; // SEQ
                string teikiSeq = string.Empty; // SEQ(定期)

                // 送信状況=1．未送信かつ伝票種類=6．定期の場合、検索結果の絞込
                if (this.headerForm.SEND_JOKYO.Text == "1")
                {
                    // SEQが最大のデータを取得
                    for (int i = 0; i < data.Rows.Count; i++)
                    {
                        // 検索に使用する項目
                        denpyouNum = data.Rows[i]["DENPYOU_NUMBER"].ToString();
                        rowNum = data.Rows[i]["ROW_NUMBER"].ToString();
                        seq = data.Rows[i]["SEQ"].ToString();
                        teikiSeq = data.Rows[i]["TEIKI_SEQ"].ToString();

                        // 対象の行番号で既にｼｮｰﾄﾒｯｾｰｼﾞが送信されている場合は一覧から除外
                        string rowSql = "SELECT SYSTEM_ID FROM T_SMS ";
                        rowSql += string.Format("WHERE DENPYOU_SHURUI = '4' AND DENPYOU_NUMBER = '{0}' ", denpyouNum);
                        rowSql += string.Format("AND ROW_NUMBER = '{0}' ", rowNum);
                        var rowNumSearch = this.dao.GetDataTableSql(rowSql);
                        if (rowNumSearch.Rows.Count != 0)
                        {
                            data.Rows[i].Delete();
                            continue;
                        }

                        // SEQ最大値取得(定期）
                        string maxSeq_Teiki = this.dao.GetMaxTeikiSeq(denpyouName, denpyouNum);
                        //if (maxSeq_Sms.Rows[0]["SEQ"].ToString() != seq || maxSeq_Teiki != teikiSeq)
                        if (maxSeq_Teiki != teikiSeq)
                        {
                            // SEQ(定期)が最大値ではないデータは一覧から除外
                            data.Rows[i].Delete();
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(seq))
                            {
                                continue;
                            }

                            // SEQ最大値取得(ｼｮｰﾄﾒｯｾｰｼﾞ）
                            var maxSeq_Sms = this.dao.GetDataTableSql(string.Format("SELECT ISNULL(MAX(SEQ),1) AS SEQ FROM T_SMS WHERE DENPYOU_NUMBER = '{0}' ", denpyouNum));

                            if (maxSeq_Sms.Rows[0]["SEQ"].ToString() != seq)
                            {
                                // SEQ(ｼｮｰﾄﾒｯｾｰｼﾞ)が最大値ではないデータは一覧から除外
                                data.Rows[i].Delete();
                            }
                        }
                    }
                }
                // 送信状況=1．未送信以外かつ伝票種類=6．定期の場合、検索結果の絞込
                else if (this.headerForm.SEND_JOKYO.Text != "1")
                {
                    // SEQが最大のデータを取得
                    for (int j = 0; j < data.Rows.Count; j++)
                    {
                        // 検索に使用する項目
                        denpyouNum = data.Rows[j]["DENPYOU_NUMBER"].ToString();
                        rowNum = data.Rows[j]["ROW_NUMBER"].ToString();
                        teikiSeq = data.Rows[j]["TEIKI_SEQ"].ToString();

                        // 対象の行番号で既にｼｮｰﾄﾒｯｾｰｼﾞが送信されている場合は一覧から除外
                        string rowSql = "SELECT SYSTEM_ID FROM T_SMS ";
                        rowSql += string.Format("WHERE DENPYOU_SHURUI = '4' AND DENPYOU_NUMBER = '{0}' ", denpyouNum);
                        rowSql += string.Format("AND ROW_NUMBER = '{0}' ", rowNum);
                        rowSql += string.Format("AND SEQ = '{0}' ", teikiSeq);
                        var rowNumSearch = this.dao.GetDataTableSql(rowSql);
                        if (rowNumSearch.Rows.Count == 0)
                        {
                            data.Rows[j].Delete();
                        }
                    }
                }
                data.AcceptChanges();
            }

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

            this.form.SAGYOU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.SAGYOU_DATE_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE_FROM.Text))
            {
                return true;
            }
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE_TO.Text))
            {
                return true;
            }

            DateTime date_from = DateTime.Parse(this.form.SAGYOU_DATE_FROM.Text);
            DateTime date_to = DateTime.Parse(this.form.SAGYOU_DATE_TO.Text);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.SAGYOU_DATE_FROM.IsInputErrorOccured = true;
                this.form.SAGYOU_DATE_TO.IsInputErrorOccured = true;
                this.form.SAGYOU_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.SAGYOU_DATE_TO.BackColor = Constans.ERROR_COLOR;
                string[] errorMsg = { "作業日From", "作業日To" };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.SAGYOU_DATE_FROM.Focus();
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
            // 送信状況
            if (string.IsNullOrEmpty(this.headerForm.SEND_JOKYO.Text))
            {
                this.msgLogic.MessageBoxShow("E012", "送信状況");
                this.headerForm.SEND_JOKYO.BackColor = ConstCls.ERROR_COLOR;
                this.headerForm.SEND_JOKYO.Focus();
                return false;
            }
            // 伝票種類
            if (string.IsNullOrEmpty(this.form.SMS_DENPYOU_SHURUI.Text))
            {
                this.msgLogic.MessageBoxShow("E012", "伝票種類");
                this.form.SMS_DENPYOU_SHURUI.BackColor = ConstCls.ERROR_COLOR;
                this.form.SMS_DENPYOU_SHURUI.Focus();
                return false;
            }

            // 作業日
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE_FROM.Text) && string.IsNullOrEmpty(this.form.SAGYOU_DATE_TO.Text))
            {
                this.msgLogic.MessageBoxShow("E012", "作業日");
                this.form.SAGYOU_DATE_FROM.BackColor = ConstCls.ERROR_COLOR;
                this.form.SAGYOU_DATE_FROM.Focus();
                return false;
            }

            return true;
        }
        #endregion

        #region 伝票種類チェック

        /// <summary>
        /// 伝票種類チェック
        /// </summary>
        private void DenpyouShuruiCheck(string denpyouShurui)
        {
            switch(this.form.SMS_DENPYOU_SHURUI.Text)
            {
                case "1": // 収集
                    denpyouName = T_UKETSUKE_SS_ENTRY;
                    unionTableName = string.Empty;
                    break;
                case "2": // 出荷
                    denpyouName = T_UKETSUKE_SK_ENTRY;
                    unionTableName = string.Empty;
                    break;
                case "3": // 持込
                    denpyouName = T_UKETSUKE_MK_ENTRY;
                    unionTableName = string.Empty;
                    break;
                case "4": // 収集+出荷
                    denpyouName = T_UKETSUKE_SS_ENTRY;
                    unionTableName = T_UKETSUKE_SK_ENTRY;
                    break;
                case "5": // 収集+持込
                    denpyouName = T_UKETSUKE_SS_ENTRY;
                    unionTableName = T_UKETSUKE_MK_ENTRY;
                    break;
                case "6": // 定期
                    denpyouName = T_TEIKI_HAISHA_ENTRY;
                    unionTableName = string.Empty;
                    break;
                default:
                    this.msgLogic.MessageBoxShowInformation("1～6のいずれかで入力して下さい。");
                    break;
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

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ入力画面に渡す情報設定
        /// </summary>
        /// <returns></returns>
        internal string[] SmsParamListSetting()
        {
            string[] smsparamList = new string[7];

            // 選択行の値を参照
            smsparamList[0] = this.DenpyouShuruiSetting(this.form.Ichiran.CurrentRow.Cells["DENPYOU_SHURUI"].Value.ToString());
            // 伝票種類が定期である場合、定期配車番号を参照してSEQを取得
            if (denpyouName == T_TEIKI_HAISHA_ENTRY)
            {
                smsparamList[1] = this.dao.GetMaxTeikiSeq(denpyouName, this.form.Ichiran.CurrentRow.Cells["DENPYOU_NUMBER"].Value.ToString());
            }
            else
            {
                smsparamList[1] = this.dao.GetMaxUketsukeSeq(denpyouName, this.form.Ichiran.CurrentRow.Cells["DENPYOU_NUMBER"].Value.ToString());
            }
            smsparamList[2] = this.form.Ichiran.CurrentRow.Cells["DENPYOU_NUMBER"].Value.ToString();
            smsparamList[3] = this.form.Ichiran.CurrentRow.Cells["SMS_GYOUSHA_CD"].Value.ToString();
            smsparamList[4] = this.form.Ichiran.CurrentRow.Cells["SMS_GENBA_CD"].Value.ToString();
            smsparamList[5] = ((DateTime)this.form.Ichiran.CurrentRow.Cells["SAGYOU_DATE"].Value).ToString("yyyy/MM/dd(ddd)");
            if (this.form.SMS_DENPYOU_SHURUI.Text == "6")
            {
                smsparamList[6] = this.form.Ichiran.CurrentRow.Cells["ROW_NUMBER"].Value.ToString();
            }
            else
            {
                smsparamList[6] = null;
            }

            return smsparamList;
        }

        /// <summary>
        /// 伝票種類設定（ｼｮｰﾄﾒｯｾｰｼﾞ入力用）
        /// </summary>
        /// <param name="denpyouShurui">選択行の伝票種類</param>
        /// <returns></returns>
        private string DenpyouShuruiSetting(string denpyouShurui)
        {
            string value = string.Empty;

            if (denpyouShurui == "収集")
            {
                value = "1";
            }
            else if (denpyouShurui == "出荷")
            {
                value = "2";
            }
            else if (denpyouShurui == "持込")
            {
                value = "3";
            }
            else if (denpyouShurui == "定期")
            {
                value = "4";
            }

            return value;
        }

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者リスト取得
        /// </summary>
        /// <returns></returns>
        internal List<int> SmsReceiverListSetting()
        {
            List<int> smsReceiverList = null;
            List<M_SMS_RECEIVER_LINK_GENBA> smsReceiverLink = null;
            string currentGyoushaCd = this.form.Ichiran.CurrentRow.Cells["SMS_GYOUSHA_CD"].FormattedValue.ToString();
            string currentGenbaCd = this.form.Ichiran.CurrentRow.Cells["SMS_GENBA_CD"].FormattedValue.ToString();

            // 選択行の値を参照
            smsReceiverLink = this.smsReceiverLinkGenbaDao.GetDataForSmsNyuuryoku(currentGyoushaCd, currentGenbaCd);
            
            if (smsReceiverLink != null)
            {
                smsReceiverList = smsReceiverLink.Select(n => n.SYSTEM_ID.Value).ToList();
            }

            return smsReceiverList;
        }

        private void HeaderCheckBoxSupport()
        {
            LogUtility.DebugMethodStart();

            DataGridViewCheckBoxHeaderCell newheader = new DataGridViewCheckBoxHeaderCell(0, true);
            newheader.Value = "送信\n ";
            newheader.Tag = "すべてのｼｮｰﾄﾒｯｾｰｼﾞを一括送信したい場合、チェックを付けてください";
            if (this.form.Ichiran.Columns.Count > 0)
            {
                for (int i = 0; i < this.form.Ichiran.Columns.Count - 1; i++)
                {
                    if (this.form.Ichiran.Columns[i].Name == "sendFlg")
                    {
                        this.form.Ichiran.Columns[i].HeaderCell = newheader;
                        this.form.Ichiran.Columns[i].HeaderText = "送信\n ";
                        this.form.Ichiran.Columns[i].ToolTipText = "一括送信の対象データの場合、チェックを付けてください";
                        this.form.Ichiran.Columns[i].Resizable = DataGridViewTriState.False;
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        internal DataTable SmsReceiverSearch(string gyoushaCd, string genbaCd)
        {
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

                DataTable searchResult = new DataTable();

                var smsLinkGenbaSearch = this.smsReceiverLinkGenbaDao.CheckDataForSmsNyuuryoku(gyoushaCd, genbaCd);
                if (smsLinkGenbaSearch != null)
                {
                    #region 検索SQL生成

                    string sql = "SELECT RECEIVER_NAME, R.MOBILE_PHONE_NUMBER FROM M_SMS_RECEIVER R ";
                    sql += "LEFT JOIN M_SMS_RECEIVER_LINK_GENBA RLG ";
                    sql += "ON R.SYSTEM_ID = RLG.SYSTEM_ID ";
                    sql += "AND R.MOBILE_PHONE_NUMBER = RLG.MOBILE_PHONE_NUMBER ";
                    sql += "WHERE DELETE_FLG = 0 ";
                    sql += string.Format("AND GYOUSHA_CD = '{0}' AND GENBA_CD = '{1}' ", gyoushaCd, genbaCd);

                    #endregion
                    searchResult = this.dao.GetDataTableSql(sql);
                }

                return searchResult;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("SmsReceiverSearch", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(gyoushaCd, genbaCd);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SmsReceiverSearch", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(gyoushaCd, genbaCd);
                return null;
            }
        }

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

    internal class SubLogicClass
    {
        #region プログレスバーイベントハンドラ

        public delegate void SetProgressBarEventHandler(int min, int max, int value);
        public event SetProgressBarEventHandler setProgressBar;
        public virtual void onSetProgressBar(int min, int max, int value)
        {
            if (setProgressBar != null)
            {
                setProgressBar(min, max, value);
            }
        }

        public delegate void IncProgressBarEventHandler();
        public event IncProgressBarEventHandler incProgressBar;
        public virtual void onIncProgressBar()
        {
            if (incProgressBar != null)
            {
                incProgressBar();
            }
        }

        public delegate void ResetProgressBarEventHandler();
        public event ResetProgressBarEventHandler resetProgressBar;
        public virtual void onResetProgressBar()
        {
            if (resetProgressBar != null)
            {
                resetProgressBar();
            }
        }

        #endregion

    }
}
