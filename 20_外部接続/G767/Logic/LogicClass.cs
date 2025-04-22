using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Configuration;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Dao;
using Shougun.Core.Common.BusinessCommon.Xml;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.Message;

namespace Shougun.Core.ExternalConnection.SmsNyuuryoku
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {

        #region フィールド

        /// <summary>
        /// ボタン設定格納ファイル
        /// </summary>
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.SmsNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// メッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// 検索結果
        /// </summary>
        private DataTable searchResult;

        /// <summary>
        /// システム設定
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        private UIHeader HeaderForm;

        /// <summary>
        /// IM_KYOTENDao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ連携現場のDao
        /// </summary>
        public SmsReceiverLinkGenbaDao smsReceiverLinkGenbaDao;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞのDao
        /// </summary>
        public T_SMSDao smsDao;

        /// <summary>
        /// システム設定のEntity
        /// </summary>
        private M_SYS_INFO SysInfo;

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞのEntity
        /// </summary>
        public T_SMS smsEntry;

        /// <summary>
        /// リクエスト送信前エラーフラグ
        /// </summary>
        internal bool reqErrFlg = false;

        #endregion

        #region プロパティ

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ受信者データテーブル
        /// </summary>
        public DataTable SmsReceiverTable { get; set; }

        /// <summary>
        /// 配車状況名（伝票種類が持込、定期時はBLANK）
        /// </summary>
        private string haisyajokyoName = string.Empty;

        /// <summary>
        /// SEQ（定期用）
        /// </summary>
        private string seq = string.Empty;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm">UIForm</param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.msgLogic = new MessageBoxShowLogic();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
            // ｼｮｰﾄﾒｯｾｰｼﾞ
            this.smsReceiverLinkGenbaDao = DaoInitUtility.GetComponent<SmsReceiverLinkGenbaDao>();
            this.smsDao = DaoInitUtility.GetComponent<T_SMSDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理

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

                //ボタンのテキストを初期化
                this.ButtonInit();

                //ボタン制御
                this.ButtonEnabledControl();

                //イベントの初期化処理
                this.EventInit();

                // キー入力設定
                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // ヘッダーフォームを取得
                this.HeaderForm = (UIHeader)this.parentForm.headerForm;

                // システム設定を取得する。
                this.SysInfo = this.daoSysInfo.GetAllDataForCode("0");
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return ret;
        }

        /// <summary>
        /// ボタン設定の読み込み
        /// </summary>
        /// <returns></returns>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonsetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();

            return buttonsetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            var buttonSetting = this.CreateButtonInfo();

            //親フォームのボタン表示
            var parentForm = (BusinessBaseForm)this.form.Parent;
            if (!AppConfig.AppOptions.IsSMS())
            {
                foreach (var button in buttonSetting)
                {
                    if (button.ButtonName == "bt_process1")
                    {
                        button.NewButtonName = string.Empty;
                        button.UpdateButtonName = string.Empty;
                        button.ReferButtonName = string.Empty;
                        button.DeleteButtonName = string.Empty;
                        button.DefaultHintText = string.Empty;
                    }
                }
            }
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, WINDOW_TYPE.NEW_WINDOW_FLAG);
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;

            // 伝票参照(F1)イベント生成
            parentForm.bt_func1.Click -= new EventHandler(this.form.DenpyouReference);
            parentForm.bt_func1.Click += new EventHandler(this.form.DenpyouReference);
            // SMS送信(F9)イベント作成
            parentForm.bt_func9.Click -= new EventHandler(this.form.bt_func9_Click);
            parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click -= new EventHandler(this.form.Cancel);
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            // 閉じる(F12)イベント作成
            parentForm.bt_func12.Click -= new EventHandler(this.form.bt_func12_Click);
            parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);

            // 全文クリア
            parentForm.bt_process1.Click -= new EventHandler(this.form.bt_process1_Click);
            parentForm.bt_process1.Click += new EventHandler(this.form.bt_process1_Click);

            // 一覧のEnterイベント生成
            this.form.customDataGridView1.CellEnter -= new DataGridViewCellEventHandler(this.form.Ichiran_CellEnter);
            this.form.customDataGridView1.CellEnter += new DataGridViewCellEventHandler(this.form.Ichiran_CellEnter);
        }

        #endregion

        #region ボタン制御
        /// <summary>
        /// ファンクションボタンの制御
        /// </summary>
        private void ButtonEnabledControl()
        {
            var parentForm = (BusinessBaseForm)this.form.Parent;
            // 初期化
            // 新規モードの場合
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                parentForm.bt_func3.Enabled = false;
            }
            // 修正モードの場合
            else if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                parentForm.bt_func2.Enabled = true;
            }
            // 削除モードの場合
            else if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG)
            {
                parentForm.bt_func2.Enabled = false;
                parentForm.bt_process1.Enabled = false;
            }
            // 参照モードの場合
            else
            {
                parentForm.bt_func2.Enabled = false;
                parentForm.bt_func3.Enabled = false;
                parentForm.bt_func9.Enabled = false;
                parentForm.bt_process1.Enabled = false;
            }

            parentForm.bt_func12.Enabled = true;
        }
        #endregion

        /// <summary>
        /// テーブルからデータを取得する。
        /// </summary>
        private void GetTableData(string denpyouShurui)
        {
            try
            {
                string sql = string.Empty;

                LogUtility.DebugMethodStart();

                // 収集
                if (this.form.paramList[0] == "1")
                {
                    // 検索条件設定
                    var seq = this.form.paramList[1];
                    var uketsukeNumber = this.form.paramList[2];

                    // SQL文作成
                    sql = "SELECT * FROM T_UKETSUKE_SS_ENTRY WHERE 1 = 1 ";
                    if(seq != null)
                    {
                        sql += string.Format("AND SEQ = {0} ", seq);
                    }
                    if(uketsukeNumber != null)
                    {
                        sql += string.Format("AND UKETSUKE_NUMBER = {0} ", uketsukeNumber);
                    }
                }
                // 出荷
                else if (this.form.paramList[0] == "2")
                {
                    // 検索条件設定
                    var seq = this.form.paramList[1];
                    var uketsukeNumber = this.form.paramList[2];

                    // SQL文作成
                    sql = "SELECT * FROM T_UKETSUKE_SK_ENTRY WHERE 1 = 1 ";
                    if(seq != null)
                    {
                        sql += string.Format("AND SEQ = {0} ", seq);
                    }
                    if(uketsukeNumber != null)
                    {
                        sql += string.Format("AND UKETSUKE_NUMBER = {0} ", uketsukeNumber);
                    }
                }
                // 持込
                else if (this.form.paramList[0] == "3")
                {
                    // 検索条件設定
                    var seq = this.form.paramList[1];
                    var uketsukeNumber = this.form.paramList[2];

                    // SQL文作成
                    sql = "SELECT * FROM T_UKETSUKE_MK_ENTRY WHERE 1 = 1 ";
                    if(seq != null)
                    {
                        sql += string.Format("AND SEQ = {0} ", seq);
                    }
                    if(uketsukeNumber != null)
                    {
                        sql += string.Format("AND UKETSUKE_NUMBER = {0} ", uketsukeNumber);
                    }
                }
                // 定期
                else if (this.form.paramList[0] == "4")
                {
                    // SQL文作成
                    sql = this.TeikiSearchSql();
                    if(this.form.paramList != null)
                    {
                        sql += string.Format("WHERE HE.SEQ = '{0}' AND HD.SEQ = '{0}' ", this.form.paramList[1]);
                        sql += string.Format("AND HE.TEIKI_HAISHA_NUMBER = '{0}' ", this.form.paramList[2]);
                        sql += string.Format("AND HD.GYOUSHA_CD = '{0}' ", this.form.paramList[3]);
                        sql += string.Format("AND HD.GENBA_CD = '{0}' ", this.form.paramList[4]);
                        if (this.form.paramList[6] != null)
                        {
                            sql += string.Format("AND HD.ROW_NUMBER = '{0}' ", this.form.paramList[6]);
                        }
                    }
                }

                searchResult = this.smsDao.GetDataTableSql(sql);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTableData", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTableData", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd("GetTableData");
            }
        }

        /// <summary>
        /// 画面に反映
        /// </summary>
        internal void SetValue()
        {
            //
            this.form.DENPYOU_SHURUI.Text = this.form.paramList[0];

            // データを取得する。
            this.GetTableData(this.form.DENPYOU_SHURUI.Text);

            if (this.searchResult != null)
            {
                // 拠点
                this.HeaderForm.KYOTEN_CD.Text = this.searchResult.Rows[0]["KYOTEN_CD"].ToString();
                var kyotenName = this.kyotenDao.GetDataByCd(this.searchResult.Rows[0]["KYOTEN_CD"].ToString());
                if(kyotenName != null)
                {
                    this.HeaderForm.KYOTEN_NAME_RYAKU.Text = kyotenName.KYOTEN_NAME_RYAKU;
                }
                
                // 伝票種類＝4．配車の場合のみ、値のセット内容が異なる
                if (this.form.DENPYOU_SHURUI.Text == "4")
                {
                    // 伝票番号
                    this.form.DENPYOU_NUMBER.Text = this.searchResult.Rows[0]["TEIKI_HAISHA_NUMBER"].ToString();

                    // 作業日
                    this.form.SAGYOU_DATE.Text = this.form.paramList[5];

                    // 希望時間
                    if(!string.IsNullOrEmpty(this.searchResult.Rows[0]["KIBOU_TIME"].ToString()))
                    {
                        this.form.GENCHAKU_TIME.Text = this.searchResult.Rows[0]["KIBOU_TIME"].ToString().Substring(0, 5);
                    }

                    // コース情報
                    this.form.COURSE_NAME_CD.Text = this.searchResult.Rows[0]["COURSE_NAME_CD"].ToString();
                    this.form.COURSE_NAME.Text = this.searchResult.Rows[0]["COURSE_NAME_RYAKU"].ToString();
                    this.seq = this.searchResult.Rows[0]["SEQ"].ToString();
                    this.form.ROW_NUMBER.Text = this.searchResult.Rows[0]["ROW_NUMBER"].ToString();

                    // 業者
                    this.form.GYOUSHA_CD.Text = this.searchResult.Rows[0]["GYOUSHA_CD"].ToString();
                    this.form.GYOUSHA_NAME.Text = this.searchResult.Rows[0]["GYOUSHA_NAME_RYAKU"].ToString();

                    // 現場
                    this.form.GENBA_CD.Text = this.searchResult.Rows[0]["GENBA_CD"].ToString();
                    this.form.GENBA_NAME.Text = this.searchResult.Rows[0]["GENBA_NAME_RYAKU"].ToString();
                    if(this.searchResult.Rows[0]["GENBA_ADDRESS1"] != null)
                    {
                        this.form.GENBA_ADDRESS1.Text = this.searchResult.Rows[0]["GENBA_ADDRESS1"].ToString();
                    }
                    if(this.searchResult.Rows[0]["GENBA_ADDRESS2"] != null)
                    {
                        this.form.GENBA_ADDRESS2.Text = this.searchResult.Rows[0]["GENBA_ADDRESS2"].ToString();
                    }

                    // 営業担当者
                    if(this.searchResult.Rows[0]["EIGYOU_TANTOU_CD"] != null)
                    {
                        this.form.EIGYOU_TANTOUSHA_CD.Text = this.searchResult.Rows[0]["EIGYOU_TANTOU_CD"].ToString();
                    }
                    if(this.searchResult.Rows[0]["SHAIN_NAME_RYAKU"] != null)
                    {
                        this.form.EIGYOU_TANTOUSHA_NAME.Text = this.searchResult.Rows[0]["SHAIN_NAME_RYAKU"].ToString();
                    }

                    // 運搬業者
                    if(this.searchResult.Rows[0]["UNPAN_GYOUSHA_CD"] != null)
                    {
                        this.form.UNPAN_GYOUSHA_CD.Text = this.searchResult.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                    }
                    if(this.searchResult.Rows[0]["UNPAGYOUSHA_NAME_RYAKU"] != null)
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = this.searchResult.Rows[0]["UNPAGYOUSHA_NAME_RYAKU"].ToString();
                    }

                    // 車種
                    if(this.searchResult.Rows[0]["SHASHU_CD"] != null)
                    {
                        this.form.SHASHU_CD.Text = this.searchResult.Rows[0]["SHASHU_CD"].ToString();
                    }
                    if(this.searchResult.Rows[0]["SHASHU_NAME_RYAKU"] != null)
                    {
                        this.form.SHASHU_NAME.Text = this.searchResult.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                    }

                    // 車輌
                    if(this.searchResult.Rows[0]["SHARYOU_CD"] != null)
                    {
                        this.form.SHARYOU_CD.Text = this.searchResult.Rows[0]["SHARYOU_CD"].ToString();
                    }
                    if(this.searchResult.Rows[0]["SHARYOU_NAME_RYAKU"] != null)
                    {
                        this.form.SHARYOU_NAME.Text = this.searchResult.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                    }
                }
                // 伝票種類＝1．収集、2．出荷、3．持込は同じように値をセット
                else
                {
                    // 伝票番号
                    this.form.DENPYOU_NUMBER.Text = this.searchResult.Rows[0]["UKETSUKE_NUMBER"].ToString();

                    // 作業日
                    this.form.SAGYOU_DATE.Text = this.form.paramList[5];

                    // 現着時間
                    if(!string.IsNullOrEmpty(this.searchResult.Rows[0]["GENCHAKU_TIME"].ToString()))
                    {
                        this.form.GENCHAKU_TIME.Text = this.searchResult.Rows[0]["GENCHAKU_TIME"].ToString().Substring(0, 5);
                    }

                    // コース情報はBLANK
                    this.form.COURSE_NAME_CD.Text = string.Empty;
                    this.form.COURSE_NAME.Text = string.Empty;
                    this.form.ROW_NUMBER.Text = string.Empty;

                    // 業者
                    this.form.GYOUSHA_CD.Text = this.searchResult.Rows[0]["GYOUSHA_CD"].ToString();
                    this.form.GYOUSHA_NAME.Text = this.searchResult.Rows[0]["GYOUSHA_NAME"].ToString();

                    // 現場
                    this.form.GENBA_CD.Text = this.searchResult.Rows[0]["GENBA_CD"].ToString();
                    this.form.GENBA_NAME.Text = this.searchResult.Rows[0]["GENBA_NAME"].ToString();
                    // 伝票種類＝3．持込では現場住所が存在しないためスルー
                    if (this.form.DENPYOU_SHURUI.Text != "3")
                    {
                        if(this.searchResult.Rows[0]["GENBA_ADDRESS1"] != null)
                        {
                            this.form.GENBA_ADDRESS1.Text = this.searchResult.Rows[0]["GENBA_ADDRESS1"].ToString();
                        }
                        if(this.searchResult.Rows[0]["GENBA_ADDRESS2"] != null)
                        {
                            this.form.GENBA_ADDRESS2.Text = this.searchResult.Rows[0]["GENBA_ADDRESS2"].ToString();
                        }
                    }

                    // 営業担当者
                    if(this.searchResult.Rows[0]["EIGYOU_TANTOUSHA_CD"] != null)
                    {
                        this.form.EIGYOU_TANTOUSHA_CD.Text = this.searchResult.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString();
                    }
                    if(this.searchResult.Rows[0]["EIGYOU_TANTOUSHA_NAME"] != null)
                    {
                        this.form.EIGYOU_TANTOUSHA_NAME.Text = this.searchResult.Rows[0]["EIGYOU_TANTOUSHA_NAME"].ToString();
                    }

                    // 運搬業者
                    if(this.searchResult.Rows[0]["UNPAN_GYOUSHA_CD"] != null)
                    {
                        this.form.UNPAN_GYOUSHA_CD.Text = this.searchResult.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                    }
                    if(this.searchResult.Rows[0]["UNPAN_GYOUSHA_NAME"] != null)
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = this.searchResult.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString();
                    }

                    // 車種
                    if(this.searchResult.Rows[0]["SHASHU_CD"] != null)
                    {
                        this.form.SHASHU_CD.Text = this.searchResult.Rows[0]["SHASHU_CD"].ToString();
                    }
                    if(this.searchResult.Rows[0]["SHASHU_NAME"] != null)
                    {
                        this.form.SHASHU_NAME.Text = this.searchResult.Rows[0]["SHASHU_NAME"].ToString();
                    }

                    // 車輌
                    if(this.searchResult.Rows[0]["SHARYOU_CD"] != null)
                    {
                        this.form.SHARYOU_CD.Text = this.searchResult.Rows[0]["SHARYOU_CD"].ToString();
                    }
                    if(this.searchResult.Rows[0]["SHARYOU_NAME"] != null)
                    {
                        this.form.SHARYOU_NAME.Text = this.searchResult.Rows[0]["SHARYOU_NAME"].ToString();
                    }
                }

                #region 入力項目初期設定（件名、本文…）
                
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                M_SYS_INFO sysInfo = new DBAccessor().GetSysInfo();
                SMSLogic smsMsgLogic = new SMSLogic();

                // 伝票種類＝3．持込、4．定期以外である場合、配車状況を設定
                if (this.form.DENPYOU_SHURUI.Text != "3" && this.form.DENPYOU_SHURUI.Text != "4")
                {
                    haisyajokyoName = this.searchResult.Rows[0]["HAISHA_JOKYO_NAME"].ToString();
                }

                // 件名
                this.form.SUBJECT.Text = smsMsgLogic.SubjectSetting(this.form.DENPYOU_SHURUI.Text, haisyajokyoName);

                // 受信者（区分）
                this.form.RECEIVER_KBN.Text = "1";

                // 挨拶文
                // システム個別設定入力の挨拶文を参照
                string greetings = this.GetUserProfileValue(userProfile, "挨拶文");
                if (string.IsNullOrEmpty(greetings))
                {
                    // システム個別設定入力で挨拶文の入力が無い場合、
                    // システム設定入力の挨拶文を参照
                    greetings = sysInfo.SMS_GREETINGS;
                }
                if (greetings.Contains("\n"))
                {
                    // 改行文字を変換
                    var gre = Regex.Unescape(greetings);
                    this.form.GREETINGS.Text = Regex.Replace(gre, "\n", "\r\n");
                }
                else
                {
                    this.form.GREETINGS.Text = greetings;
                }

                // 署名
                string signature = this.GetUserProfileValue(userProfile, "署名");
                if (string.IsNullOrEmpty(signature))
                {
                    // システム個別設定入力で署名の入力が無い場合、
                    // システム設定入力の署名を参照
                    signature = sysInfo.SMS_SIGNATURE;
                }
                if (signature.Contains("\n"))
                {
                    // 改行文字を変換
                    var sig = Regex.Unescape(signature);
                    this.form.SIGNATURE.Text = Regex.Replace(sig, "\n", "\r\n");
                }
                else
                {
                    this.form.SIGNATURE.Text = signature;
                }

                string[] textArray = smsMsgLogic.TextInitSetting(this.form.DENPYOU_SHURUI.Text,
                                                                 haisyajokyoName,
                                                                 this.form.SAGYOU_DATE.Text,
                                                                 this.form.GENCHAKU_TIME.Text,
                                                                 this.form.GYOUSHA_CD.Text,
                                                                 this.form.GENBA_CD.Text,
                                                                 this.form.GENBA_NAME.Text);
                this.form.TEXT1.Text = textArray[0];
                this.form.TEXT2.Text = textArray[1];
                this.form.TEXT3.Text = textArray[2];
                this.form.TEXT4.Text = textArray[3];

                #endregion

                #region ｼｮｰﾄﾒｯｾｰｼﾞ受信者マスタ検索
                // ｼｮｰﾄﾒｯｾｰｼﾞ連携現場を検索
                var smsLinkGenbaSearch = this.smsReceiverLinkGenbaDao.CheckDataByPhoneNumberAndCd(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text);
                if(smsLinkGenbaSearch != null)
                {
                    #region 検索SQL生成

                    string sql = "SELECT RECEIVER_NAME, R.MOBILE_PHONE_NUMBER FROM M_SMS_RECEIVER R ";
                    sql += "LEFT JOIN M_SMS_RECEIVER_LINK_GENBA RLG ";
                    sql += "ON R.SYSTEM_ID = RLG.SYSTEM_ID ";
                    sql += "AND R.MOBILE_PHONE_NUMBER = RLG.MOBILE_PHONE_NUMBER ";
                    sql += "WHERE DELETE_FLG = 0 ";
                    sql += string.Format("AND GYOUSHA_CD = '{0}' AND GENBA_CD = '{1}' ", smsLinkGenbaSearch.GYOUSHA_CD, smsLinkGenbaSearch.GENBA_CD);

                    #endregion

                    this.SmsReceiverTable = this.smsDao.GetDataTableSql(sql);
                    if(SmsReceiverTable != null)
                    {
                        this.SetIchiranSmsReceiver();
                    }
                }

                #endregion
            }
            else
            {
                this.form.SAGYOU_DATE.Text = string.Empty;
                this.form.GENCHAKU_TIME.Text = string.Empty;
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME.Text = string.Empty;
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME.Text = string.Empty;
                this.form.GENBA_ADDRESS1.Text = string.Empty;
                this.form.GENBA_ADDRESS2.Text = string.Empty;
                this.form.EIGYOU_TANTOUSHA_CD.Text = string.Empty;
                this.form.EIGYOU_TANTOUSHA_NAME.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                this.form.SHASHU_CD.Text = string.Empty;
                this.form.SHASHU_NAME.Text = string.Empty;
                this.form.SHARYOU_CD.Text = string.Empty;
                this.form.SHARYOU_NAME.Text = string.Empty;

                this.form.customDataGridView1.Rows.Clear();
            }

            // 新規モード時
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                this.HeaderForm.windowTypeLabel.Text = "新規";
                this.HeaderForm.windowTypeLabel.BackColor = System.Drawing.Color.Aqua;
                this.HeaderForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                // 全ての項目を活性にする。
                this.form.SUBJECT.Enabled = true;
                this.form.GREETINGS.Enabled = true;
                this.form.TEXT1.Enabled = true;
                this.form.TEXT2.Enabled = true;
                this.form.TEXT3.Enabled = true;
                this.form.TEXT4.Enabled = true;
                this.form.SIGNATURE.Enabled = true;
                this.form.customDataGridView1.Columns["sendFlg"].ReadOnly = false;
            }
            // 修正モード時
            else if (this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                this.HeaderForm.windowTypeLabel.Text = "修正";
                this.HeaderForm.windowTypeLabel.BackColor = System.Drawing.Color.Yellow;
                this.HeaderForm.windowTypeLabel.ForeColor = System.Drawing.Color.Black;

                // 全ての項目を活性にする。
                this.form.SUBJECT.Enabled = true;
                this.form.GREETINGS.Enabled = true;
                this.form.TEXT1.Enabled = true;
                this.form.TEXT2.Enabled = true;
                this.form.TEXT3.Enabled = true;
                this.form.TEXT4.Enabled = true;
                this.form.SIGNATURE.Enabled = true;
                this.form.customDataGridView1.Columns["sendFlg"].ReadOnly = false;
            }
            // 削除モードもしくは参照モード時
            else if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG || this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
            {
                // 全ての項目を非活性にする。
                this.form.SUBJECT.Enabled = false;
                this.form.GREETINGS.Enabled = false;
                this.form.TEXT1.Enabled = false;
                this.form.TEXT2.Enabled = false;
                this.form.TEXT3.Enabled = false;
                this.form.TEXT4.Enabled = false;
                this.form.SIGNATURE.Enabled = false;
                this.form.customDataGridView1.Columns["sendFlg"].ReadOnly = true;

                //明細欄の背景色と文字色を変更
                var rows = this.form.customDataGridView1.Rows;
                foreach (DataGridViewRow row in rows)
                {
                    var cells = row.Cells;
                    foreach (DataGridViewCell cell in cells)
                    {
                        var changeColor = cell as ICustomAutoChangeBackColor;
                        if (changeColor != null)
                        {
                            ((ICustomAutoChangeBackColor)cell).AutoChangeBackColorEnabled = false;
                            cell.Style.BackColor = Constans.READONLY_COLOR;
                            cell.Style.ForeColor = Constans.READONLY_COLOR_FORE;
                            cell.Style.SelectionBackColor = Constans.READONLY_COLOR;
                            cell.Style.SelectionForeColor = Constans.READONLY_COLOR_FORE;
                        }
                    }
                }
            }
        }

        #region 登録用Entity作成
        /// <summary>
        /// Entity作成
        /// </summary>
        internal void CreateEntity(string mobilePhoneNumber, string receiverName)
        {
            var registEntity = new T_SMS();

            string denNo = this.form.DENPYOU_NUMBER.Text;

            // 新規
            if (this.form.WindowType == WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                // 新規である場合、システムID採番
                registEntity.SYSTEM_ID = this.smsDao.GetMaxSystemId();
                if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                {
                    var sDateLength = this.form.SAGYOU_DATE.Text.Length;
                    registEntity.SAGYOU_DATE = this.form.SAGYOU_DATE.Text.Remove(sDateLength - 3, 3);
                }
                if (!string.IsNullOrEmpty(this.form.GENCHAKU_TIME.Text))
                {
                    registEntity.GENCHAKU_TIME = this.form.GENCHAKU_TIME.Text;
                }
                registEntity.DENPYOU_SHURUI = SqlInt16.Parse(this.form.DENPYOU_SHURUI.Text);
                // 伝票種類＝1．収集、2．出荷である場合、配車状況名を設定
                if(registEntity.DENPYOU_SHURUI == 1 || registEntity.DENPYOU_SHURUI == 2)
                {
                    registEntity.HAISHA_JOKYO_NAME = this.searchResult.Rows[0]["HAISHA_JOKYO_NAME"].ToString();
                }
                registEntity.DENPYOU_NUMBER = SqlInt64.Parse(this.form.DENPYOU_NUMBER.Text);
                // 伝票種類＝4．定期である場合、行番号を設定
                if (registEntity.DENPYOU_SHURUI == 4)
                {
                    registEntity.SEQ = SqlInt32.Parse(this.seq);
                    registEntity.ROW_NUMBER = SqlInt16.Parse(this.form.ROW_NUMBER.Text);
                }
                else
                {
                    registEntity.SEQ = SqlInt32.Null;
                    registEntity.ROW_NUMBER = SqlInt16.Null;
                }
                registEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                registEntity.GYOUSHA_NAME = this.form.GYOUSHA_NAME.Text;
                registEntity.GENBA_CD = this.form.GENBA_CD.Text;
                registEntity.GENBA_NAME = this.form.GENBA_NAME.Text;
                registEntity.RECEIVER_NAME = receiverName;
                registEntity.MOBILE_PHONE_NUMBER = mobilePhoneNumber;
            }
            // 修正
            else
            {
                // T_SMSのEntity取得
                this.smsEntry = this.smsDao.GetSearchSMSEntity(this.form.SystemId, denNo);
                registEntity.SYSTEM_ID = SqlInt32.Parse(this.form.SystemId);

                registEntity.SEND_DATE_R = this.smsEntry.SEND_DATE_R;
                registEntity.SAGYOU_DATE = this.smsEntry.SAGYOU_DATE;
                registEntity.GENCHAKU_TIME = this.smsEntry.GENCHAKU_TIME;
                registEntity.DENPYOU_SHURUI = this.smsEntry.DENPYOU_SHURUI;
                // 伝票種類＝1．収集、2．出荷である場合、配車状況名を設定
                if(this.smsEntry.DENPYOU_SHURUI == 1 || this.smsEntry.DENPYOU_SHURUI == 2)
                {
                    registEntity.HAISHA_JOKYO_NAME = this.smsEntry.HAISHA_JOKYO_NAME;
                }
                registEntity.DENPYOU_NUMBER = this.smsEntry.DENPYOU_NUMBER;
                // 伝票種類＝4．定期である場合、行番号を設定
                if(registEntity.DENPYOU_SHURUI == 4)
                {
                    registEntity.SEQ = this.smsEntry.SEQ;
                    registEntity.ROW_NUMBER = this.smsEntry.ROW_NUMBER;
                }
                registEntity.GYOUSHA_CD = this.smsEntry.GYOUSHA_CD;
                registEntity.GYOUSHA_NAME = this.smsEntry.GYOUSHA_NAME;
                registEntity.GENBA_CD = this.smsEntry.GENBA_CD;
                registEntity.GENBA_NAME = this.smsEntry.GENBA_NAME;
                registEntity.RECEIVER_NAME = this.smsEntry.RECEIVER_NAME;
                registEntity.MOBILE_PHONE_NUMBER = this.smsEntry.MOBILE_PHONE_NUMBER;
            }

            // API関連項目、送信日、送信者はNULL
            registEntity.MESSAGE_ID = null;
            registEntity.ERROR_CODE = null;
            registEntity.ERROR_DETAIL = null;
            registEntity.SEND_DATE_R = SqlDateTime.Null;
            registEntity.SEND_USER = null;

            var dataBinderDelivery = new DataBinderLogic<T_SMS>(registEntity);
            dataBinderDelivery.SetSystemProperty(registEntity, false);

            if (this.form.WindowType != WINDOW_TYPE.NEW_WINDOW_FLAG)
            {
                registEntity.CREATE_DATE = this.smsEntry.CREATE_DATE;
                registEntity.CREATE_PC = this.smsEntry.CREATE_PC;
                registEntity.CREATE_USER = this.smsEntry.CREATE_USER;
                registEntity.TIME_STAMP = this.smsEntry.TIME_STAMP;
            }

            this.smsEntry = registEntity;
        }
        #endregion

        #region 登録前チェック

        internal bool RegistCheck()
        {
            // 本文が入力されていること
            if (string.IsNullOrEmpty(this.form.TEXT1.Text)
                && string.IsNullOrEmpty(this.form.TEXT2.Text)
                && string.IsNullOrEmpty(this.form.TEXT3.Text)
                && string.IsNullOrEmpty(this.form.TEXT4.Text))
            {
                this.msgLogic.MessageBoxShow("E012", "本文");
                return false;
            }

            return true;
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        internal void RegistData()
        {
            try
            {
                // Insert処理実行
                this.smsDao.Insert(this.smsEntry);
            }
            catch (Exception ex)
            {
                LogUtility.Error("RegistData", ex);
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

        #region 修正登録
        /// <summary>
        /// 修正登録
        /// </summary>
        [Transaction]
        internal void UpdateData()
        {
            try
            {
                // Update処理実行
                this.smsDao.Update(this.smsEntry);
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
        /// 一覧 - CELL_ENTER_EVENT
        /// </summary>
        /// <param name="columnIndex"></param>
        internal void CellEnter(int columnIndex)
        {
            string cellName = this.form.customDataGridView1.Columns[columnIndex].Name;

            if (string.IsNullOrEmpty(cellName))
            {
                return;
            }

            // IME制御
            switch (cellName)
            {
                default:
                    this.form.customDataGridView1.ImeMode = ImeMode.Hiragana;
                    break;
            }
        }

        #region ユーザー定義情報取得処理
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
        #endregion

        /// <summary>
        /// 入力項目クリア処理
        /// </summary>
        internal bool Cancel()
        {
            // 入力項目をクリアする
            this.form.SUBJECT.Text = "";
            this.form.GREETINGS.Text = "";
            this.form.TEXT1.Text = "";
            this.form.TEXT2.Text = "";
            this.form.TEXT3.Text = "";
            this.form.TEXT4.Text = "";
            this.form.SIGNATURE.Text = "";

            return true;
        }

        #region ｼｮｰﾄﾒｯｾｰｼﾞ関連

        /// <summary>
        /// 定期検索SQL生成（保留）
        /// </summary>
        private string TeikiSearchSql()
        {
            string sql = string.Empty;

            #region SELECT句

            sql += "SELECT HE.*, ";
            sql += "HD.KIBOU_TIME, ";
            sql += "CO.COURSE_NAME_RYAKU, ";
            sql += "HD.SEQ, ";
            sql += "HD.ROW_NUMBER, ";
            sql += "HD.GYOUSHA_CD, ";
            sql += "GY.GYOUSHA_NAME_RYAKU, ";
            sql += "HD.GENBA_CD, ";
            sql += "GE.GENBA_NAME_RYAKU, ";
            sql += "GE.GENBA_ADDRESS1, ";
            sql += "GE.GENBA_ADDRESS2, ";
            sql += "GE.EIGYOU_TANTOU_CD, ";
            sql += "SH.SHAIN_NAME_RYAKU, ";
            sql += "UGY.GYOUSHA_NAME_RYAKU AS UNPAGYOUSHA_NAME_RYAKU, ";
            sql += "SHU.SHASHU_NAME_RYAKU, ";
            sql += "RYO.SHARYOU_NAME_RYAKU ";

            #endregion

            #region FROM句

            sql += "FROM T_TEIKI_HAISHA_ENTRY HE ";

            #endregion

            #region JOIN句

            sql += "LEFT JOIN T_TEIKI_HAISHA_DETAIL HD ON HE.TEIKI_HAISHA_NUMBER = HD.TEIKI_HAISHA_NUMBER ";
            sql += "LEFT JOIN M_COURSE_NAME CO ON HE.COURSE_NAME_CD = CO.COURSE_NAME_CD ";
            sql += "LEFT JOIN M_GYOUSHA GY ON HD.GYOUSHA_CD = GY.GYOUSHA_CD ";
            sql += "LEFT JOIN M_GENBA GE ON HD.GYOUSHA_CD = GE.GYOUSHA_CD AND HD.GENBA_CD = GE.GENBA_CD ";
            sql += "LEFT JOIN M_SHAIN SH ON GE.EIGYOU_TANTOU_CD = SH.SHAIN_CD ";
            sql += "LEFT JOIN M_GYOUSHA UGY ON HE.UNPAN_GYOUSHA_CD = UGY.GYOUSHA_CD ";
            sql += "LEFT JOIN M_SHASHU SHU ON HE.SHASHU_CD = SHU.SHASHU_CD ";
            sql += "LEFT JOIN M_SHARYOU RYO ON HE.SHARYOU_CD = RYO.SHARYOU_CD AND GY.GYOUSHA_CD = RYO.GYOUSHA_CD ";

            #endregion

            return sql;
        }

        internal void SetIchiranSmsReceiver()
        {
            try
            {
                var table = this.SmsReceiverTable;

                table.BeginLoadData();

                this.form.customDataGridView1.DataSource = table;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiranSmsReceiver", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 送信先チェック
        /// </summary>
        /// <returns></returns>
        internal Boolean SmsSendCheck()
        {
            LogUtility.DebugMethodStart();

            Boolean rtn = false;

            try
            {
                // 送信チェックボックスの値チェック
                int SendCheckCount = 0;
                for (int i = 0; i < this.form.customDataGridView1.Rows.Count; i++)
                {
                    if (this.form.customDataGridView1.Rows[i].Cells["sendFlg"].Value != null && (bool)this.form.customDataGridView1.Rows[i].Cells["sendFlg"].Value)
                    {
                        SendCheckCount++;
                        break;
                    }
                }

                rtn = (SendCheckCount > 0);

                if(!rtn)
                {
                    this.msgLogic.MessageBoxShowError("ショートメッセージの送信先が選択されていません。\n送信先をチェックしてください。");
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
        /// SMS長文分割送信APIリクエスト後
        /// </summary>
        internal void LongSmsSplitSendAPI_After(string[] responseArray, SMSLogic smsLogic)
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
                        this.smsEntry.MESSAGE_ID = responseArray[0];
                    }
                    else if (string.IsNullOrEmpty(responseArray[0]) && !string.IsNullOrEmpty(responseArray[1]))
                    {
                        // エラーコード、エラー詳細
                        this.smsEntry.ERROR_CODE = responseArray[1];
                        string errDetail = smsLogic.SMSErrorSummarySetting(this.smsEntry.ERROR_CODE);
                        if (!string.IsNullOrEmpty(errDetail))
                        {
                            this.smsEntry.ERROR_DETAIL = smsLogic.SMSErrorSummarySetting(this.smsEntry.ERROR_CODE);
                        }
                    }

                    // 送信日、送信者はAPIの結果に関係なく適用
                    this.smsEntry.SEND_DATE_R = this.sysDate();
                    this.HeaderForm.SEND_DATE.Text = this.smsEntry.SEND_DATE_R.ToString();
                    this.smsEntry.SEND_USER = SystemProperty.UserName;
                    this.HeaderForm.SEND_USER.Text = this.smsEntry.SEND_USER.ToString();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LongSmsSplitSendAPI_After", ex);
                this.msgLogic.MessageBoxShow("E245", "");
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

        #region ｼｮｰﾄﾒｯｾｰｼﾞ一覧から画面起動

        /// <summary>
        /// ｼｮｰﾄﾒｯｾｰｼﾞ一覧から起動した際の伝票種類取得
        /// </summary>
        /// <returns></returns>
        private string SetValueFromSmsIchiran()
        {
            var eachDenpyou = new DataTable();
            string shurui = string.Empty;
            string sql = string.Empty;

            sql += "SELECT DISTINCT SMS.DENPYOU_NUMBER, ";
            sql += "SS.SYSTEM_ID AS SS_ID, ";
            sql += "SK.SYSTEM_ID AS SK_ID, ";
            sql += "MK.SYSTEM_ID AS MK_ID ";

            sql += "FROM T_SMS SMS ";

            sql += "LEFT OUTER JOIN T_UKETSUKE_SS_ENTRY SS ON SMS.DENPYOU_NUMBER = SS.UKETSUKE_NUMBER ";
            sql += "LEFT OUTER JOIN T_UKETSUKE_SK_ENTRY SK ON SMS.DENPYOU_NUMBER = SK.UKETSUKE_NUMBER ";
            sql += "LEFT OUTER JOIN T_UKETSUKE_MK_ENTRY MK ON SMS.DENPYOU_NUMBER = MK.UKETSUKE_NUMBER ";

            sql += string.Format("WHERE SMS.DENPYOU_NUMBER = {0}", this.form.paramList[1]);

            eachDenpyou = this.smsDao.GetDataTableSql(sql);

            if (eachDenpyou != null)
            {
                if (!string.IsNullOrEmpty(eachDenpyou.Rows[0]["SS_ID"].ToString()))
                {
                    shurui = "1";
                }
                else if (!string.IsNullOrEmpty(eachDenpyou.Rows[0]["SK_ID"].ToString()))
                {
                    shurui = "2";
                }
                else if (!string.IsNullOrEmpty(eachDenpyou.Rows[0]["MK_ID"].ToString()))
                {
                    shurui = "3";
                }
            }

            return shurui;
        }

        #endregion

        /// <summary>
        /// 送信項目設定
        /// </summary>
        internal void PrameSet()
        {
            this.form.sendPrame.Add(this.form.SUBJECT.Text); // 件名
            this.form.sendPrame.Add(this.form.GREETINGS.Text);　// 挨拶文
            this.form.sendPrame.Add(this.form.TEXT1.Text); // 本文1
            this.form.sendPrame.Add(this.form.TEXT2.Text); // 本文2
            this.form.sendPrame.Add(this.form.TEXT3.Text); // 本文3
            this.form.sendPrame.Add(this.form.TEXT4.Text); // 本文4
            this.form.sendPrame.Add(this.form.SIGNATURE.Text); // 署名
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
        #region 検索
        /// <summary>
        /// 検索
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            throw new NotImplementedException();
        }
        #endregion
        #endregion
    }
}
