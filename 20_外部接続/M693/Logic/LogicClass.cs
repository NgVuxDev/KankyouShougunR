using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Logicompass;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;
using Shougun.Core.ExternalConnection.GaibuRenkeiGenbaHoshu.APP;

namespace Shougun.Core.ExternalConnection.GaibuRenkeiGenbaHoshu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region 定数

        private readonly string RENKEI_MI = "未連携";
        private readonly string RENKEI_ZUMI = "連携済";
        private readonly string RENKEI_HORYU = "保留登録";

        #endregion

        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.GaibuRenkeiGenbaHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// 画面上に表示するメッセージボックスを
        /// メッセージIDから検索し表示する処理
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        private LogiLogic logiLogic;

        private IM_SYS_INFODao sysInfoDao;

        private IM_TORIHIKISAKIDao torihikisakiDao;

        private IM_GYOUSHADao gyoushaDao;

        private IM_GENBADao genbaDao;

        private IM_TODOUFUKENDao todofukenDao;

        private IM_GENBA_DIGIDao genbaDigiDao;

        private IM_DIGI_OUTPUT_GENBADao digiOutputGenbaDao;

        private IS_LOGI_CONNECTDao logiConnectDao;

        /// <summary>
        /// 取得した取引先エンティティを保持する
        /// </summary>
        private List<M_TORIHIKISAKI> torihikisakiList = new List<M_TORIHIKISAKI>();

        /// <summary>
        /// 取得した業者エンティティを保持する
        /// </summary>
        private List<M_GYOUSHA> gyoushaList = new List<M_GYOUSHA>();

        /// <summary>
        /// 取得した現場エンティティを保持する
        /// </summary>
        private List<M_GENBA> genbaList = new List<M_GENBA>();

        /// <summary>
        /// 不正な入力をされたかを示します
        /// </summary>
        internal bool isInputError = false; // true:エラー有, false:エラー無

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// ヘッダーフォーム
        /// </summary>
        internal HeaderForm headerForm;

        #endregion

        #region プロパティ

        /// <summary>業者CD</summary>
        internal string GYOUSHA_CD { get; set; }

        /// <summary>現場CD</summary>
        internal string GENBA_CD { get; set; }

        /// <summary>現場デジタコ連携情報マスタ</summary>
        private M_GENBA_DIGI GENBA_DIGI { get; set; }

        /// <summary>デジタコ連携現場</summary>
        private M_DIGI_OUTPUT_GENBA DIGI_OUTPUT_GENBA { get; set; }

        /// <summary>登録用：現場デジタコ連携情報マスタ</summary>
        private M_GENBA_DIGI REGIST_GENBA_DIGI { get; set; }

        /// <summary>登録用：地点API</summary>
        private INFO_POINT REGIST_INFO_POINT { get; set; }

        /// <summary>システム設定</summary>
        private M_SYS_INFO SYS_INFO { get; set; }
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.todofukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.genbaDigiDao = DaoInitUtility.GetComponent<IM_GENBA_DIGIDao>();
            this.digiOutputGenbaDao = DaoInitUtility.GetComponent<IM_DIGI_OUTPUT_GENBADao>();
            this.logiConnectDao = DaoInitUtility.GetComponent<IS_LOGI_CONNECTDao>();

            this.msgLogic = new MessageBoxShowLogic();
            this.logiLogic = new LogiLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 初期化処理
        /// <summary>
        /// 初期化
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <returns>true:エラー, false:正常</returns>
        internal bool WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.headerForm = (HeaderForm)this.parentForm.headerForm;

                this.SYS_INFO = sysInfoDao.GetAllDataForCode("0");

                // ボタンのテキストを初期化
                this.ButtonInit(this.parentForm);

                // イベントの初期化
                this.EventInit(this.parentForm);

                // 処理モード別画面初期化
                this.ModeInit(windowType, this.parentForm);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.msgLogic.MessageBoxShow("E245");
                return true;
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
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

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
                // 新規ボタン(F2)イベント生成
                parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

                // 連携削除ボタン(F4)イベント生成
                parentForm.bt_func4.Click += new EventHandler(this.form.Delete);

                // 一覧ボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.FormSearch);

                // 連携登録ボタン(F9)イベント生成
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                // 保留登録ボタン(F10)イベント生成
                this.form.C_Regist(parentForm.bt_func10);
                parentForm.bt_func10.Click += new EventHandler(this.form.HoldRegist);
                parentForm.bt_func10.ProcessKbn = PROCESS_KBN.NEW;

                // 取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
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
        #endregion

        #region モード別初期化処理
        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType">画面区分</param>
        /// <param name="parentForm">ベースフォーム</param>
        internal void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            // 共通初期化処理
            this.GENBA_DIGI = null;
            this.DIGI_OUTPUT_GENBA = null;
            this.REGIST_GENBA_DIGI = null;
            this.REGIST_INFO_POINT = null;

            this.ControlUnLock(parentForm, true);

            switch (windowType)
            {
                // 【新規】モード
                case WINDOW_TYPE.NEW_WINDOW_FLAG:
                    this.WindowInitNew(parentForm);
                    break;

                // 【修正】モード
                case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                    this.WindowInitUpdate(parentForm);
                    break;

                // 【参照】モード
                case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                    this.WindowInitReference(parentForm);
                    break;

                // デフォルトは【新規】モード
                default:
                    this.WindowInitNew(parentForm);
                    break;
            }
        }

        /// <summary>
        /// 全入力コントロールの活性制御
        /// </summary>
        /// <param name="parentForm">ベースフォーム</param>
        /// <param name="isUnLock">true:活性, false:非活性</param>
        private void ControlUnLock(BusinessBaseForm parentForm, bool isUnLock)
        {
            this.form.POINT_ID.Enabled = isUnLock;
            this.form.BT_SAIBAN.Enabled = isUnLock;
            this.form.GYOUSHA_CD.Enabled = isUnLock;
            this.form.BT_GYOUSHA_SEARCH.Enabled = isUnLock;
            this.form.GENBA_CD.Enabled = isUnLock;
            this.form.BT_GENBA_SEARCH.Enabled = isUnLock;
            this.form.BT_GENBA_COPY.Enabled = isUnLock;
            this.form.POINT_KANA_NAME.Enabled = isUnLock;
            this.form.POINT_NAME.Enabled = isUnLock;
            this.form.MAP_NAME.Enabled = isUnLock;
            this.form.RANGE_RADIUS.Enabled = isUnLock;
            this.form.UNTENSHA_SHIJI_JIKOU1.Enabled = isUnLock;
            this.form.UNTENSHA_SHIJI_JIKOU2.Enabled = isUnLock;
            this.form.UNTENSHA_SHIJI_JIKOU3.Enabled = isUnLock;

            this.form.POST_CODE.Enabled = isUnLock;
            this.form.GENBA_TODOUFUKEN_CD.Enabled = isUnLock;
            this.form.ADDRESS1.Enabled = isUnLock;
            this.form.ADDRESS2.Enabled = isUnLock;
            this.form.TEL_NO.Enabled = isUnLock;
            this.form.FAX_NO.Enabled = isUnLock;
            this.form.CONTACT_NAME.Enabled = isUnLock;

            parentForm.bt_func2.Enabled = isUnLock;
            parentForm.bt_func4.Enabled = isUnLock;
            parentForm.bt_func7.Enabled = isUnLock;
            parentForm.bt_func9.Enabled = isUnLock;
            parentForm.bt_func10.Enabled = isUnLock;
            parentForm.bt_func11.Enabled = isUnLock;
            parentForm.bt_func12.Enabled = isUnLock;
        }

        #region 新規モード
        /// <summary>
        /// 画面項目初期化【新規】モード
        /// </summary>
        /// <param name="parentForm">ベースフォーム</param>
        /// <returns>true:エラー, false:正常</returns>
        private bool WindowInitNew(BusinessBaseForm parentForm)
        {
            try
            {
                // 画面の初期化
                this.form.POINT_ID.Text = string.Empty;
                this.form.TORIHIKISAKI_CD.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
                this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
                this.form.GYOUSHA_CD.Text = string.Empty;
                this.form.GYOUSHA_NAME1.Text = string.Empty;
                this.form.GYOUSHA_NAME2.Text = string.Empty;
                this.form.GENBA_CD.Text = string.Empty;
                this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                this.form.TEKIYOU_BEGIN.Value = null;
                this.form.TEKIYOU_END.Value = null;

                this.form.POINT_KANA_NAME.Text = string.Empty;
                this.form.POINT_NAME.Text = string.Empty;
                this.form.MAP_NAME.Text = string.Empty;
                this.form.RANGE_RADIUS.Text = this.SYS_INFO.DIGI_RANGE_RADIUS.IsNull ? string.Empty : this.SYS_INFO.DIGI_RANGE_RADIUS.ToString();
                this.form.UNTENSHA_SHIJI_JIKOU1.Text = string.Empty;
                this.form.UNTENSHA_SHIJI_JIKOU2.Text = string.Empty;
                this.form.UNTENSHA_SHIJI_JIKOU3.Text = string.Empty;

                // 最新現場入力情報
                this.form.GENBA_POST.Text = string.Empty;
                this.form.GENBA_TODOUFUKEN_CD.Text = string.Empty;
                this.form.GENBA_TODOUFUKEN_NAME_RYAKU.Text = string.Empty;
                this.form.GENBA_ADDRESS1.Text = string.Empty;
                this.form.GENBA_ADDRESS2.Text = string.Empty;
                this.form.GENBA_FURIGANA.Text = string.Empty;
                this.form.GENBA_NAME1.Text = string.Empty;
                this.form.GENBA_NAME2.Text = string.Empty;
                this.form.GENBA_TEL.Text = string.Empty;
                this.form.GENBA_KEITAI_TEL.Text = string.Empty;
                this.form.GENBA_FAX.Text = string.Empty;
                this.form.TANTOUSHA.Text = string.Empty;

                // 連携時現場入力情報
                this.form.POST_CODE.Text = string.Empty;
                this.form.PREFECTURES.Text = string.Empty;
                this.form.ADDRESS1.Text = string.Empty;
                this.form.ADDRESS2.Text = string.Empty;
                this.form.TEL_NO.Text = string.Empty;
                this.form.FAX_NO.Text = string.Empty;
                this.form.CONTACT_NAME.Text = string.Empty;

                //
                this.headerForm.LastUpdateUser.Text = string.Empty;
                this.headerForm.LastUpdateDate.Text = string.Empty;
                this.headerForm.CreateUser.Text = string.Empty;
                this.headerForm.CreateDate.Text = string.Empty;
                this.headerForm.OutputUser.Text = string.Empty;
                this.headerForm.OutputDate.Text = string.Empty;
                this.headerForm.lb_renkei.Text = RENKEI_MI;

                // 業者CD、現場CDが設定されていればマスタ情報を設定
                SetWindowDataInitNew();

                // 
                this.form.POINT_ID.ReadOnly = false;
                this.form.POINT_ID.TabStop = true;
                this.form.BT_SAIBAN.Enabled = true;
                this.form.GYOUSHA_CD.ReadOnly = false;
                this.form.GYOUSHA_CD.TabStop = true;
                this.form.BT_GYOUSHA_SEARCH.Enabled = true;
                this.form.GENBA_CD.ReadOnly = false;
                this.form.GENBA_CD.TabStop = true;
                this.form.BT_GENBA_SEARCH.Enabled = true;

                parentForm.bt_func4.Enabled = false;

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNew", ex);
                this.msgLogic.MessageBoxShow("E245");
                return true;
            }
        }
        #endregion

        #region 修正モード
        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        /// <param name="parentForm">ベースフォーム</param>
        /// <returns>true:エラー, false:正常</returns>
        private bool WindowInitUpdate(BusinessBaseForm parentForm)
        {
            try
            {
                // 検索結果を画面に設定
                this.SetWindowData();

                // 
                this.form.POINT_ID.ReadOnly = true;
                this.form.POINT_ID.TabStop = false;
                this.form.BT_SAIBAN.Enabled = false;
                this.form.GYOUSHA_CD.ReadOnly = true;
                this.form.GYOUSHA_CD.TabStop = false;
                this.form.BT_GYOUSHA_SEARCH.Enabled = false;
                this.form.GENBA_CD.ReadOnly = true;
                this.form.GENBA_CD.TabStop = false;
                this.form.BT_GENBA_SEARCH.Enabled = false;

                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitUpdate", ex2);
                this.msgLogic.MessageBoxShow("E093");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.msgLogic.MessageBoxShow("E245");
                return true;
            }
        }
        #endregion

        #region 参照モード
        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        /// <param name="parentForm">ベースフォーム</param>
        /// <returns>true:エラー, false:正常</returns>
        private bool WindowInitReference(BusinessBaseForm parentForm)
        {
            try
            {
                this.ControlUnLock(parentForm, false);

                // 検索結果を画面に設定
                this.SetWindowData();

                this.parentForm.bt_func2.Enabled = true;
                this.parentForm.bt_func7.Enabled = true;
                this.parentForm.bt_func12.Enabled = true;

                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInitReference", ex2);
                this.msgLogic.MessageBoxShow("E093");
                return true;
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(ex.Message))
                {
                    LogUtility.Error("WindowInitReference", ex);
                    this.msgLogic.MessageBoxShow("E245");
                }
                return true;
            }
        }
        #endregion

        /// <summary>
        /// マスタ情報を取得して画面に設定
        /// </summary>
        /// <remarks>新規モードからの呼出用</remarks>
        private void SetWindowDataInitNew()
        {
            try
            {
                if (string.IsNullOrEmpty(this.GYOUSHA_CD) || string.IsNullOrEmpty(this.GENBA_CD))
                {
                    return;
                }

                // 各マスタ情報取得
                bool catchErr;
                var gyousha = this.GetGyousha(this.GYOUSHA_CD, out catchErr, true);
                if (!catchErr)
                {
                    return;
                }

                var genba = this.GetGenba(this.GYOUSHA_CD, this.GENBA_CD, out catchErr, true);
                if (!catchErr)
                {
                    return;
                }

                var torihikisaki = this.GetTorihikisaki(genba.TORIHIKISAKI_CD, out catchErr);
                if (!catchErr)
                {
                    return;
                }

                // データを画面に設定
                this.form.TORIHIKISAKI_CD.Text = genba.TORIHIKISAKI_CD;
                if (torihikisaki != null)
                {
                    this.form.TORIHIKISAKI_NAME1.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    this.form.TORIHIKISAKI_NAME2.Text = torihikisaki.TORIHIKISAKI_NAME2;
                }
                this.form.GYOUSHA_CD.Text = this.GYOUSHA_CD;
                this.form.GYOUSHA_NAME1.Text = gyousha.GYOUSHA_NAME1;
                this.form.GYOUSHA_NAME2.Text = gyousha.GYOUSHA_NAME2;
                this.form.GENBA_CD.Text = this.GENBA_CD;
                SetGenbaRelationInfo(genba);

                // 前回値保存
                this.form.Control_Enter(this.form.GYOUSHA_CD, null);
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetWindowDataInitNew", ex2);
                this.msgLogic.MessageBoxShow("E093");
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowDataInitNew", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
        }

        /// <summary>
        /// マスタ情報を取得して画面に設定
        /// </summary>
        /// <remarks>編集、参照モードからの呼出用</remarks>
        private void SetWindowData()
        {
            try
            {
                if (string.IsNullOrEmpty(this.GYOUSHA_CD) || string.IsNullOrEmpty(this.GENBA_CD))
                {
                    return;
                }

                // 各マスタ情報取得
                bool catchErr;
                var gyousha = this.GetGyousha(this.GYOUSHA_CD, out catchErr, true);
                if (!catchErr)
                {
                    return;
                }

                var genba = this.GetGenba(this.GYOUSHA_CD, this.GENBA_CD, out catchErr, true);
                if (!catchErr)
                {
                    return;
                }

                var torihikisaki = this.GetTorihikisaki(genba.TORIHIKISAKI_CD, out catchErr);
                if (!catchErr)
                {
                    return;
                }

                this.GENBA_DIGI = this.genbaDigiDao.GetDataByCd(this.GYOUSHA_CD, this.GENBA_CD);
                this.DIGI_OUTPUT_GENBA = this.digiOutputGenbaDao.GetDataByCd(this.GYOUSHA_CD, this.GENBA_CD);

                M_TODOUFUKEN todofuken = null;
                if (!this.GENBA_DIGI.GENBA_TODOUFUKEN_CD.IsNull)
                {
                    todofuken = this.todofukenDao.GetDataByCd(this.GENBA_DIGI.GENBA_TODOUFUKEN_CD.ToString());
                }

                // データを画面に設定
                this.form.POINT_ID.Text = this.GENBA_DIGI.POINT_ID;
                this.form.TORIHIKISAKI_CD.Text = genba.TORIHIKISAKI_CD;
                if (torihikisaki != null)
                {
                    this.form.TORIHIKISAKI_NAME1.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    this.form.TORIHIKISAKI_NAME2.Text = torihikisaki.TORIHIKISAKI_NAME2;
                }
                this.form.GYOUSHA_CD.Text = this.GENBA_DIGI.GYOUSHA_CD;
                this.form.GYOUSHA_NAME1.Text = gyousha.GYOUSHA_NAME1;
                this.form.GYOUSHA_NAME2.Text = gyousha.GYOUSHA_NAME2;
                this.form.GENBA_CD.Text = this.GENBA_DIGI.GENBA_CD;
                SetGenbaRelationInfo(genba);

                this.form.POINT_KANA_NAME.Text = this.GENBA_DIGI.POINT_KANA_NAME;
                this.form.POINT_NAME.Text = this.GENBA_DIGI.POINT_NAME;
                this.form.MAP_NAME.Text = this.GENBA_DIGI.MAP_NAME;
                if (!this.GENBA_DIGI.RANGE_RADIUS.IsNull)
                {
                    this.form.RANGE_RADIUS.Text = this.GENBA_DIGI.RANGE_RADIUS.ToString();
                }
                else
                {
                    this.form.RANGE_RADIUS.Text = string.Empty;
                }
                this.form.UNTENSHA_SHIJI_JIKOU1.Text = this.GENBA_DIGI.UNTENSHA_SHIJI_JIKOU1;
                this.form.UNTENSHA_SHIJI_JIKOU2.Text = this.GENBA_DIGI.UNTENSHA_SHIJI_JIKOU2;
                this.form.UNTENSHA_SHIJI_JIKOU3.Text = this.GENBA_DIGI.UNTENSHA_SHIJI_JIKOU3;

                // 連携現場入力情報
                this.form.POST_CODE.Text = this.GENBA_DIGI.POST_CODE;
                if (!this.GENBA_DIGI.GENBA_TODOUFUKEN_CD.IsNull)
                {
                    this.form.GENBA_TODOUFUKEN_CD.Text = this.GENBA_DIGI.GENBA_TODOUFUKEN_CD.ToString();
                    this.form.PREFECTURES.Text = todofuken.TODOUFUKEN_NAME;
                }
                else
                {
                    this.form.GENBA_TODOUFUKEN_CD.Text = string.Empty;
                    this.form.PREFECTURES.Text = string.Empty;
                }
                this.form.ADDRESS1.Text = this.GENBA_DIGI.ADDRESS1;
                this.form.ADDRESS2.Text = this.GENBA_DIGI.ADDRESS2;
                this.form.TEL_NO.Text = this.GENBA_DIGI.TEL_NO;
                this.form.FAX_NO.Text = this.GENBA_DIGI.FAX_NO;
                this.form.CONTACT_NAME.Text = this.GENBA_DIGI.CONTACT_NAME;

                //
                this.headerForm.LastUpdateUser.Text = this.GENBA_DIGI.UPDATE_USER;
                this.headerForm.LastUpdateDate.Text = this.GENBA_DIGI.UPDATE_DATE.ToString();
                this.headerForm.CreateUser.Text = this.GENBA_DIGI.CREATE_USER;
                this.headerForm.CreateDate.Text = this.GENBA_DIGI.CREATE_DATE.ToString();
                this.headerForm.OutputUser.Text = string.Empty;
                this.headerForm.OutputDate.Text = string.Empty;

                string lblRenkei;
                if (this.DIGI_OUTPUT_GENBA == null)
                {
                    lblRenkei = RENKEI_MI;
                }
                else
                {
                    this.headerForm.OutputUser.Text = this.DIGI_OUTPUT_GENBA.OUTPUT_USER;
                    this.headerForm.OutputDate.Text = this.DIGI_OUTPUT_GENBA.OUTPUT_DATE.ToString();
                    lblRenkei = (this.GENBA_DIGI.UPDATE_DATE < this.DIGI_OUTPUT_GENBA.OUTPUT_DATE) ? RENKEI_ZUMI : RENKEI_HORYU;
                }
                this.headerForm.lb_renkei.Text = lblRenkei;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SetWindowData", ex2);
                this.msgLogic.MessageBoxShow("E093");
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowData", ex);
                this.msgLogic.MessageBoxShow("E245");
            }
        }
        #endregion

        #region 採番
        /// <summary>
        /// 現場CD採番処理
        /// </summary>
        /// <returns>true:エラー, false:正常/returns>
        internal bool Saiban()
        {
            try
            {
                // 業者マスタのCDの最大値+1を取得
                int keyPointId = -1;

                var keyGyoushasaibanFlag = IsOverCDLimit(out keyPointId);

                if (keyGyoushasaibanFlag || keyPointId < 1)
                {
                    // 採番エラー
                    this.msgLogic.MessageBoxShow("E041");
                    this.form.POINT_ID.Text = "";
                }
                else
                {
                    // ゼロパディング後、テキストへ設定
                    this.form.POINT_ID.Text = String.Format("{0:D" + this.form.POINT_ID.MaxLength + "}", keyPointId);
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Saiban", ex2);
                this.msgLogic.MessageBoxShow("E093");
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Saiban", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// CDがMax値より上かどうかチェックする。
        /// </summary>
        /// <param name="maxPlusKeyValue">CD+1した値を格納します。Max値を超えている場合は-1を返します。</param>
        /// <returns>採番のMAX値を超えている場合はture。超えていない場合はfalseを返します。</returns>
        private bool IsOverCDLimit(out int maxPlusKeyValue)
        {
            var maxPlusKey = this.genbaDigiDao.GetMaxPlusKey();

            var allKeyDate = this.genbaDigiDao.GetDateByChokuchiKbn1();

            foreach (M_GENBA_DIGI genbaDigiEntity in allKeyDate)
            {
                var pointId = int.Parse(genbaDigiEntity.POINT_ID);
                if (pointId == maxPlusKey)
                {
                    maxPlusKey = pointId + 1;
                }
            }

            maxPlusKeyValue = -1;
            if (this.form.POINT_ID.MaxLength < maxPlusKey.ToString().Length)
            {
                maxPlusKey = this.genbaDigiDao.GetMinBlankNo(null);
                if (this.form.POINT_ID.MaxLength < maxPlusKey.ToString().Length)
                {
                    return true;
                }
            }
            maxPlusKeyValue = maxPlusKey;
            return false;
        }
        #endregion

        #region 現場情報コピー
        /// <summary>
        /// 現場情報コピー
        /// </summary>
        internal void CopyGenbaInfo()
        {
            var genbaCd = this.form.GENBA_CD.Text;
            var gyoushaCd = this.form.GYOUSHA_CD.Text;

            if (string.IsNullOrEmpty(genbaCd))
            {
                this.msgLogic.MessageBoxShow("E012", this.form.LBL_GENBA_CD.Text);
                return;
            }

            bool catchErr;
            var genba = this.GetGenba(gyoushaCd, genbaCd, out catchErr, true);
            if (!catchErr)
            {
                return;
            }

            // 
            this.form.POINT_KANA_NAME.Text = genba.GENBA_FURIGANA;
            // 21文字目以降は切捨て
            if (20 < genba.GENBA_NAME_RYAKU.Length)
            {
                this.form.POINT_NAME.Text = genba.GENBA_NAME_RYAKU.Substring(0, 20);
            }
            else
            {
                this.form.POINT_NAME.Text = genba.GENBA_NAME_RYAKU;
            }
            // 11文字目以降は切捨て
            if (10 < genba.GENBA_NAME_RYAKU.Length)
            {
                this.form.MAP_NAME.Text = genba.GENBA_NAME_RYAKU.Substring(0, 10);
            }
            else
            {
                this.form.MAP_NAME.Text = genba.GENBA_NAME_RYAKU;
            }

            this.form.UNTENSHA_SHIJI_JIKOU1.Text = genba.UNTENSHA_SHIJI_JIKOU1;
            this.form.UNTENSHA_SHIJI_JIKOU2.Text = genba.UNTENSHA_SHIJI_JIKOU2;
            this.form.UNTENSHA_SHIJI_JIKOU3.Text = genba.UNTENSHA_SHIJI_JIKOU3;

            // 連携時現場入力情報
            this.form.POST_CODE.Text = genba.GENBA_POST;
            if (!genba.GENBA_TODOUFUKEN_CD.IsNull)
            {
                this.form.GENBA_TODOUFUKEN_CD.Text = genba.GENBA_TODOUFUKEN_CD.ToString();
                this.form.PREFECTURES.Text = todofukenDao.GetDataByCd(genba.GENBA_TODOUFUKEN_CD.ToString()).TODOUFUKEN_NAME;
            }
            else
            {
                this.form.GENBA_TODOUFUKEN_CD.Text = string.Empty;
                this.form.PREFECTURES.Text = string.Empty;
            }
            this.form.ADDRESS1.Text = genba.GENBA_ADDRESS1;
            this.form.ADDRESS2.Text = genba.GENBA_ADDRESS2;
            this.form.TEL_NO.Text = genba.GENBA_TEL;
            this.form.FAX_NO.Text = genba.GENBA_FAX;
            this.form.CONTACT_NAME.Text = genba.TANTOUSHA;
        }
        #endregion

        #region 現場関連情報クリア
        /// <summary>
        /// 現場の関連情報をクリア
        /// </summary>
        internal void ClearGenbaRelationInfo()
        {
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
            this.form.TEKIYOU_BEGIN.Value = null;
            this.form.TEKIYOU_END.Value = null;
            this.form.GENBA_POST.Text = string.Empty;
            this.form.GENBA_TODOUFUKEN_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_ADDRESS1.Text = string.Empty;
            this.form.GENBA_ADDRESS2.Text = string.Empty;
            this.form.GENBA_FURIGANA.Text = string.Empty;
            this.form.GENBA_NAME1.Text = string.Empty;
            this.form.GENBA_NAME2.Text = string.Empty;
            this.form.GENBA_TEL.Text = string.Empty;
            this.form.GENBA_KEITAI_TEL.Text = string.Empty;
            this.form.GENBA_FAX.Text = string.Empty;
            this.form.TANTOUSHA.Text = string.Empty;

            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME1.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME2.Text = string.Empty;
        }
        #endregion

        #region 各種チェック処理
        #region 業者チェック
        /// <summary>
        /// 業者チェック
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool CheckGyousha()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var genbaCd = this.form.GENBA_CD.Text;

                // 現場項目クリア
                ClearGenbaRelationInfo();

                // 入力されてない場合
                if (String.IsNullOrEmpty(gyoushaCd))
                {
                    // 関連項目クリア
                    this.form.GYOUSHA_NAME1.Text = String.Empty;
                    this.form.GYOUSHA_NAME2.Text = String.Empty;

                    return true;
                }

                // 業者を取得
                // (入力エラーチェックは前回値と比較する前に行っているためここではチェックしない）
                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }

                this.form.GYOUSHA_NAME1.Text = gyousha.GYOUSHA_NAME1;
                this.form.GYOUSHA_NAME2.Text = gyousha.GYOUSHA_NAME2;

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 業者CDエラーチェック
        /// <summary>
        /// 業者CDエラーチェック
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool ErrorCheckGyousha()
        {
            var gyoushaCd = this.form.GYOUSHA_CD.Text;
            bool ren = true;

            if (!String.IsNullOrEmpty(gyoushaCd))
            {
                // 業者を取得
                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null == gyousha)
                {
                    // 業者名１、業者名２設定
                    this.form.GYOUSHA_NAME1.Text = String.Empty;
                    this.form.GYOUSHA_NAME2.Text = String.Empty;
                    this.msgLogic.MessageBoxShow("E020", "業者");
                    this.form.GYOUSHA_CD.Focus();
                    this.form.GYOUSHA_CD.IsInputErrorOccured = true;
                    this.isInputError = true;
                    return false;
                }

            }
            return ren;
        }
        #endregion

        #region 現場チェック
        /// <summary>
        /// 現場チェック
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool CheckGenba()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var torihikisakiCd = this.form.TORIHIKISAKI_CD.Text;
                var gyoushaCd = this.form.GYOUSHA_CD.Text;
                var genbaCd = this.form.GENBA_CD.Text;

                // 入力されてない場合
                if (String.IsNullOrEmpty(genbaCd))
                {
                    return true;
                }

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    this.msgLogic.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_CD.IsInputErrorOccured = true;
                    this.form.GENBA_CD.Focus();
                    this.isInputError = true;
                    return false;
                }

                // 現場情報を取得
                if (this.GetGenbaList(gyoushaCd, genbaCd).Count() == 0)
                {
                    // マスタに現場が存在しない場合
                    // 現場の関連情報をクリア
                    this.ClearGenbaRelationInfo();
                    this.form.GENBA_CD.Text = genbaCd;
                    this.msgLogic.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                    this.form.GENBA_CD.IsInputErrorOccured = true;
                    this.isInputError = true;
                    return false;
                }

                bool catchErr = true;
                var gyousha = this.GetGyousha(gyoushaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                var genba = this.GetGenba(gyoushaCd, genbaCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }

                //マスタに業者CDが存在しない場合
                //又は取引日外の業者CDが選択された場合
                if (null == genba)
                {
                    // 現場の関連情報をクリア
                    this.ClearGenbaRelationInfo();
                    this.form.GENBA_CD.Text = genbaCd;
                    this.msgLogic.MessageBoxShow("E062", "業者");
                    this.form.GENBA_CD.Focus();
                    this.form.GENBA_CD.IsInputErrorOccured = true;
                    this.isInputError = true;
                    return false;
                }
                else
                {
                    // 現場が見つかったので現場名などをセット
                    this.SetGenbaRelationInfo(genba);

                    torihikisakiCd = genba.TORIHIKISAKI_CD;
                }

                // 取引先を取得
                var torihikisaki = this.GetTorihikisaki(torihikisakiCd, out catchErr);
                if (!catchErr)
                {
                    return false;
                }
                if (null != torihikisaki)
                {
                    // 取引先設定
                    this.form.TORIHIKISAKI_CD.Text = torihikisaki.TORIHIKISAKI_CD;
                    this.form.TORIHIKISAKI_NAME1.Text = torihikisaki.TORIHIKISAKI_NAME1;
                    this.form.TORIHIKISAKI_NAME2.Text = torihikisaki.TORIHIKISAKI_NAME2;
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 登録済みチェック
        /// <summary>
        /// 登録済みデータかチェック
        /// </summary>
        internal void CheckRegisteredData()
        {
            var gyoushaCd = this.form.GYOUSHA_CD.Text;
            var genbaCd = this.form.GENBA_CD.Text;

            if (string.IsNullOrEmpty(gyoushaCd)
                || string.IsNullOrEmpty(genbaCd))
            {
                return;
            }

            // 登録済みデータの確認
            var genbaDigiEntity = genbaDigiDao.GetDataByCd(gyoushaCd, genbaCd);
            if (genbaDigiEntity != null)
            {
                var daialogResult = this.msgLogic.MessageBoxShow("C017");
                if (DialogResult.No.Equals(daialogResult))
                {
                    this.form.GENBA_CD.Text = string.Empty;
                    ClearGenbaRelationInfo();
                    this.form.GENBA_CD.Focus();
                    return;
                }

                // 登録済みデータの読み込み
                this.form.OpenEditModeByKey(genbaDigiEntity);
            }
        }
        #endregion

        #region 連携削除チェック
        /// <summary>
        /// 連携削除の入力チェック
        /// </summary>
        /// <returns>true:エラー, false:正常</returns>
        internal bool HasErrorDeleteData()
        {
            if (this.DIGI_OUTPUT_GENBA == null)
            {
                this.msgLogic.MessageBoxShowError("連携データが存在しないため削除できません。");
                return true;
            }

            return false;
        }
        #endregion
        #endregion

        #region 各マスター情報を取得
        /// <summary>
        /// 取引CDで取引先を取得します
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <param name="catchErr">true:正常, false:エラー</param>
        /// <returns>取引先エンティティ</returns>
        private M_TORIHIKISAKI GetTorihikisaki(string torihikisakiCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);

                // 取得済みの取引先リストから取得
                var torihikisaki = this.torihikisakiList.Where(t => t.TORIHIKISAKI_CD == torihikisakiCd).FirstOrDefault();
                if (null == torihikisaki)
                {
                    // なければDBから取得
                    var keyEntity = new M_TORIHIKISAKI();
                    keyEntity.TORIHIKISAKI_CD = torihikisakiCd;
                    torihikisaki = this.torihikisakiDao.GetAllValidData(keyEntity).FirstOrDefault();
                    if (null != torihikisaki)
                    {
                        this.torihikisakiList.Add(torihikisaki);
                    }
                }

                if (null != torihikisaki)
                {
                    string strBegin = torihikisaki.TEKIYOU_BEGIN.ToString();
                    string strEnd = torihikisaki.TEKIYOU_END.ToString();
                    string sagyobi = this.parentForm.sysDate.Date.ToString();

                    if (torihikisaki.TEKIYOU_BEGIN.IsNull)
                    {
                        strBegin = "0001/01/01 00:00:01";
                    }

                    if (torihikisaki.TEKIYOU_END.IsNull)
                    {
                        strEnd = "9999/12/31 23:59:59";
                    }

                    if (!string.IsNullOrEmpty(sagyobi))
                    {
                        //作業日は適用期間より範囲外の場合
                        if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                        {
                            LogUtility.DebugMethodEnd(null, catchErr);
                            return null;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(torihikisaki, catchErr);

                return torihikisaki;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetTorihikisaki", ex1);
                this.msgLogic.MessageBoxShow("E093");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetTorihikisaki", ex);
                this.msgLogic.MessageBoxShow("E245");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 業者CD、現場CDで現場リストを取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns>現場エンティティリスト</returns>
        private M_GENBA[] GetGenbaList(string gyoushaCd, string genbaCd)
        {
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

            List<M_GENBA> retlist = new List<M_GENBA>();
            IEnumerable<M_GENBA> gList = null;
            // 取得済みの現場リストから取得
            gList = this.genbaList.Where(g => g.GYOUSHA_CD == gyoushaCd && g.GENBA_CD == genbaCd);
            if (gList.Count() == 0)
            {
                // なければDBから取得
                var keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                var genbaEntities = this.genbaDao.GetAllValidData(keyEntity);
                if (null != genbaEntities)
                {
                    this.genbaList.AddRange(genbaEntities);
                    gList = genbaEntities;
                }
            }

            if (null != gList && gList.Count() != 0)
            {
                M_GENBA[] temp = gList.ToArray();
                for (int i = 0; i < temp.Length; i++)
                {
                    string strBegin = temp[i].TEKIYOU_BEGIN.ToString();
                    string strEnd = temp[i].TEKIYOU_END.ToString();
                    string sagyobi = this.parentForm.sysDate.Date.ToString();

                    if (temp[i].TEKIYOU_BEGIN.IsNull)
                    {
                        strBegin = "0001/01/01 00:00:01";
                    }

                    if (temp[i].TEKIYOU_END.IsNull)
                    {
                        strEnd = "9999/12/31 23:59:59";
                    }

                    if (!string.IsNullOrEmpty(sagyobi))
                    {
                        //作業日は適用期間より範囲外の場合
                        if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                        {
                            continue;
                        }
                        else
                        {
                            retlist.Add(temp[i]);
                        }
                    }
                }
            }

            LogUtility.DebugMethodEnd();

            return retlist.ToArray();
        }

        /// <summary>
        /// 現場CDと業者CDで現場を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <param name="catchErr">true:正常, false:エラー</param>
        /// <param name="isnotNeedDeleteFlg">true:削除済み適用期間外も含める,false:削除済み適用期間外も含めない</param>
        /// <returns>現場エンティティ</returns>
        private M_GENBA GetGenba(string gyoushaCd, string genbaCd, out bool catchErr, bool isnotNeedDeleteFlg = false)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd, catchErr, isnotNeedDeleteFlg);

                // 取得済みの現場リストから取得
                var genba = this.genbaList.Where(g => g.GYOUSHA_CD == gyoushaCd && g.GENBA_CD == genbaCd).FirstOrDefault();
                if (null == genba)
                {
                    // なければDBから取得
                    var keyEntity = new M_GENBA();
                    keyEntity.GYOUSHA_CD = gyoushaCd;
                    keyEntity.GENBA_CD = genbaCd;
                    if (isnotNeedDeleteFlg)
                    {
                        keyEntity.ISNOT_NEED_DELETE_FLG = true;
                    }
                    genba = this.genbaDao.GetAllValidData(keyEntity).FirstOrDefault();
                    if (null != genba)
                    {
                        this.genbaList.Add(genba);
                    }
                }

                if (null != genba && !isnotNeedDeleteFlg)
                {
                    string strBegin = genba.TEKIYOU_BEGIN.ToString();
                    string strEnd = genba.TEKIYOU_END.ToString();
                    string sagyobi = this.parentForm.sysDate.Date.ToString();

                    if (genba.TEKIYOU_BEGIN.IsNull)
                    {
                        strBegin = "0001/01/01 00:00:01";
                    }

                    if (genba.TEKIYOU_END.IsNull)
                    {
                        strEnd = "9999/12/31 23:59:59";
                    }

                    if (!string.IsNullOrEmpty(sagyobi))
                    {
                        //作業日は適用期間より範囲外の場合
                        if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                        {
                            LogUtility.DebugMethodEnd(null, catchErr);
                            return null;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(genba, catchErr);

                return genba;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenba", ex1);
                this.msgLogic.MessageBoxShow("E093");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenba", ex);
                this.msgLogic.MessageBoxShow("E245");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        #endregion

        #region 画面表示
        /// <summary>
        /// 現場マスタを画面の各項目に設定
        /// </summary>
        /// <param name="genba">現場マスタ</param>
        private void SetGenbaRelationInfo(M_GENBA genba)
        {
            this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
            this.form.TEKIYOU_BEGIN.Value = genba.TEKIYOU_BEGIN.IsNull ? string.Empty : genba.TEKIYOU_BEGIN.ToString();
            this.form.TEKIYOU_END.Value = genba.TEKIYOU_END.IsNull ? string.Empty : genba.TEKIYOU_END.ToSqlString();
            this.form.GENBA_POST.Text = genba.GENBA_POST;
            this.form.GENBA_ADDRESS1.Text = genba.GENBA_ADDRESS1;
            this.form.GENBA_ADDRESS2.Text = genba.GENBA_ADDRESS2;
            this.form.GENBA_FURIGANA.Text = genba.GENBA_FURIGANA;
            this.form.GENBA_NAME1.Text = genba.GENBA_NAME1;
            this.form.GENBA_NAME2.Text = genba.GENBA_NAME2;
            this.form.GENBA_TEL.Text = genba.GENBA_TEL;
            this.form.GENBA_KEITAI_TEL.Text = genba.GENBA_KEITAI_TEL;
            this.form.GENBA_FAX.Text = genba.GENBA_FAX;
            this.form.TANTOUSHA.Text = genba.TANTOUSHA;

            if (!genba.GENBA_TODOUFUKEN_CD.IsNull)
            {
                var todofukenEntity = todofukenDao.GetDataByCd(genba.GENBA_TODOUFUKEN_CD.ToString());
                if (todofukenEntity != null)
                {
                    this.form.GENBA_TODOUFUKEN_NAME_RYAKU.Text = todofukenEntity.TODOUFUKEN_NAME;
                }
            }
        }
        #endregion

        #region Entity作成
        /// <summary>
        /// 業者CDで業者を取得します
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="catchErr">true:正常, false:エラー</param>
        /// <param name="isnotNeedDeleteFlg">true:削除済み適用期間外も含める,false:削除済み適用期間外も含めない</param>
        /// <returns>業者エンティティ</returns>
        private M_GYOUSHA GetGyousha(string gyoushaCd, out bool catchErr, bool isnotNeedDeleteFlg = false)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, catchErr, isnotNeedDeleteFlg);

                // 取得済みの業者リストから取得
                var gyousha = this.gyoushaList.Where(g => g.GYOUSHA_CD == gyoushaCd).FirstOrDefault();
                if (null == gyousha)
                {
                    // なければDBから取得
                    var keyEntity = new M_GYOUSHA();
                    keyEntity.GYOUSHA_CD = gyoushaCd;
                    if (isnotNeedDeleteFlg)
                    {
                        keyEntity.ISNOT_NEED_DELETE_FLG = true;
                    }
                    gyousha = this.gyoushaDao.GetAllValidData(keyEntity).FirstOrDefault();
                    if (null != gyousha)
                    {
                        this.gyoushaList.Add(gyousha);
                    }
                }

                if (null != gyousha && !isnotNeedDeleteFlg)
                {
                    string strBegin = gyousha.TEKIYOU_BEGIN.ToString();
                    string strEnd = gyousha.TEKIYOU_END.ToString();
                    string sagyobi = this.parentForm.sysDate.Date.ToString();

                    if (gyousha.TEKIYOU_BEGIN.IsNull)
                    {
                        strBegin = "0001/01/01 00:00:01";
                    }

                    if (gyousha.TEKIYOU_END.IsNull)
                    {
                        strEnd = "9999/12/31 23:59:59";
                    }

                    if (!string.IsNullOrEmpty(sagyobi))
                    {
                        //作業日は適用期間より範囲外の場合
                        if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0)
                        {
                            LogUtility.DebugMethodEnd(null, catchErr);
                            return null;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(gyousha, catchErr);

                return gyousha;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGyousha", ex1);
                this.msgLogic.MessageBoxShow("E093");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                this.msgLogic.MessageBoxShow("E245");
                catchErr = false;
                LogUtility.DebugMethodEnd(null, catchErr);
                return null;
            }
        }

        /// <summary>
        /// 登録用Entity作成
        /// </summary>
        /// <param name="isRenkeiToroku">連携登録含みか</param>
        internal void CreateEntity(bool isRenkeiToroku)
        {
            LogUtility.DebugMethodStart(isRenkeiToroku);

            this.REGIST_GENBA_DIGI = CreateGenbaDigiEntity();

            if (isRenkeiToroku)
            {
                this.REGIST_INFO_POINT = CreatePointJsonDto(this.REGIST_GENBA_DIGI, this.SYS_INFO);
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 現場_デジタコ連携情報の登録用Entity作成
        /// </summary>
        /// <returns>現場_デジタコ連携情報マスタ</returns>
        private M_GENBA_DIGI CreateGenbaDigiEntity()
        {
            var genbaDigi = new M_GENBA_DIGI();

            genbaDigi.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            genbaDigi.GENBA_CD = this.form.GENBA_CD.Text;
            genbaDigi.POINT_ID = this.form.POINT_ID.Text;
            genbaDigi.POINT_NAME = this.form.POINT_NAME.Text;
            genbaDigi.POINT_KANA_NAME = this.form.POINT_KANA_NAME.Text;
            genbaDigi.MAP_NAME = this.form.MAP_NAME.Text;
            genbaDigi.POST_CODE = this.form.POST_CODE.Text;

            if (!string.IsNullOrEmpty(this.form.GENBA_TODOUFUKEN_CD.Text))
            {
                genbaDigi.GENBA_TODOUFUKEN_CD = SqlInt16.Parse(this.form.GENBA_TODOUFUKEN_CD.Text);
            }
            genbaDigi.ADDRESS1 = this.form.ADDRESS1.Text;
            genbaDigi.ADDRESS2 = this.form.ADDRESS2.Text;
            genbaDigi.TEL_NO = this.form.TEL_NO.Text;
            genbaDigi.FAX_NO = this.form.FAX_NO.Text;
            genbaDigi.CONTACT_NAME = this.form.CONTACT_NAME.Text;

            if (!string.IsNullOrEmpty(this.form.RANGE_RADIUS.Text))
            {
                genbaDigi.RANGE_RADIUS = SqlInt32.Parse(this.form.RANGE_RADIUS.Text);
            }

            bool catchErr;
            var genba = this.GetGenba(this.form.GYOUSHA_CD.Text, this.form.GENBA_CD.Text, out catchErr, true);
            if (!catchErr)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU1.Text))
            {
                genbaDigi.REMARKS = this.form.UNTENSHA_SHIJI_JIKOU1.Text;
            }
            if (string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU1.Text) && !string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU2.Text))
            {
                genbaDigi.REMARKS = this.form.UNTENSHA_SHIJI_JIKOU2.Text;   
            }
            else if (!string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU1.Text) && !string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU2.Text))
            {
                genbaDigi.REMARKS = genbaDigi.REMARKS + "\r\n" + this.form.UNTENSHA_SHIJI_JIKOU2.Text;
            }
            if (string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU1.Text) && string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU2.Text) && !string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU3.Text))
            {
                genbaDigi.REMARKS = this.form.UNTENSHA_SHIJI_JIKOU3.Text;
            }
            else if ((!string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU1.Text) || !string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU2.Text)) && !string.IsNullOrEmpty(this.form.UNTENSHA_SHIJI_JIKOU3.Text))
            {
                genbaDigi.REMARKS = genbaDigi.REMARKS + "\r\n" + this.form.UNTENSHA_SHIJI_JIKOU3.Text;
            }
            genbaDigi.UNTENSHA_SHIJI_JIKOU1 = this.form.UNTENSHA_SHIJI_JIKOU1.Text;
            genbaDigi.UNTENSHA_SHIJI_JIKOU2 = this.form.UNTENSHA_SHIJI_JIKOU2.Text;
            genbaDigi.UNTENSHA_SHIJI_JIKOU3 = this.form.UNTENSHA_SHIJI_JIKOU3.Text;
            genbaDigi.KEITAI_TEL = genba.GENBA_KEITAI_TEL;
            genbaDigi.GENBA_NAME1 = genba.GENBA_NAME1;
            genbaDigi.GENBA_NAME2 = genba.GENBA_NAME2;

            var dataBinder = new DataBinderLogic<M_GENBA_DIGI>(genbaDigi);
            dataBinder.SetSystemProperty(genbaDigi, false);

            if (this.form.WindowType == WINDOW_TYPE.DELETE_WINDOW_FLAG ||
               this.form.WindowType == WINDOW_TYPE.UPDATE_WINDOW_FLAG)
            {
                genbaDigi.CREATE_PC = this.GENBA_DIGI.CREATE_PC;
                genbaDigi.CREATE_USER = this.GENBA_DIGI.CREATE_USER;
                genbaDigi.CREATE_DATE = this.GENBA_DIGI.CREATE_DATE;
                genbaDigi.TIME_STAMP = this.GENBA_DIGI.TIME_STAMP;
            }

            return genbaDigi;
        }
        #endregion

        #region システム日付取得
        /// <summary>
        /// システム日付取得
        /// </summary>
        /// <returns>システム日付</returns>
        private DateTime GetDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.genbaDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        #endregion

        #region 地点用API作成
        /// <summary>
        /// 地点用API作成
        /// </summary>
        /// <param name="digiGenba">現場_デジタコ連携情報マスタ</param>
        /// <param name="sysInfo">システム設定</param>
        /// <returns>地点API</returns>
        internal INFO_POINT CreatePointJsonDto(M_GENBA_DIGI digiGenba, M_SYS_INFO sysInfo)
        {
            if (digiGenba == null || sysInfo == null)
            {
                return null;
            }

            var point = new INFO_POINT();

            point.Company_Id = sysInfo.DIGI_CORP_ID;
            point.Point_Id = digiGenba.POINT_ID;
            point.Point_Name = digiGenba.POINT_NAME;
            point.Point_Kana_Name = digiGenba.POINT_KANA_NAME;
            point.Map_Name = digiGenba.MAP_NAME;
            point.Post_Code = digiGenba.POST_CODE;
            point.Prefectures = this.form.PREFECTURES.Text;
            point.Address1 = digiGenba.ADDRESS1 + digiGenba.ADDRESS2;
            //point.Address2 = digiGenba.ADDRESS2;
            point.Tel_No = digiGenba.TEL_NO;
            point.Fax_No = digiGenba.FAX_NO;
            point.Contact_Name = digiGenba.CONTACT_NAME;
            point.Range_Radius = digiGenba.RANGE_RADIUS.Value;
            point.Remarks = digiGenba.REMARKS;
            return point;
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="isRenkeiToroku">連携登録含みか</param>
        /// <returns>true:正常, false:エラー</returns>
        [Transaction]
        internal bool RegistData(bool isRenkeiToroku)
        {
            try
            {
                LogUtility.DebugMethodStart(isRenkeiToroku);

                // 
                this.genbaDigiDao.Insert(this.REGIST_GENBA_DIGI);

                if (isRenkeiToroku)
                {
                    var sql = string.Format("INSERT INTO M_DIGI_OUTPUT_GENBA VALUES ('{0}', '{1}', '{2}', GETDATE(), '{3}', {4})", REGIST_GENBA_DIGI.GYOUSHA_CD, REGIST_GENBA_DIGI.GENBA_CD, SystemProperty.UserName, SystemInformation.ComputerName, 0);
                    this.digiOutputGenbaDao.ExecuteForStringSql(sql);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E245");
                }
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="isRenkeiToroku">連携登録含みか</param>
        /// <returns>true:正常, false:エラー</returns>
        [Transaction]
        public bool UpdateData(bool isRenkeiToroku)
        {
            try
            {
                LogUtility.DebugMethodStart(isRenkeiToroku);

                // 
                this.genbaDigiDao.Update(this.REGIST_GENBA_DIGI);

                if (isRenkeiToroku)
                {
                    string sql;
                    if (DIGI_OUTPUT_GENBA == null)
                    {
                        sql = string.Format("INSERT INTO M_DIGI_OUTPUT_GENBA VALUES ('{0}', '{1}', '{2}', GETDATE(), '{3}', {4})", REGIST_GENBA_DIGI.GYOUSHA_CD, REGIST_GENBA_DIGI.GENBA_CD, SystemProperty.UserName, SystemInformation.ComputerName, 0);
                    }
                    else
                    {
                        sql = string.Format("UPDATE M_DIGI_OUTPUT_GENBA SET OUTPUT_USER = '{0}', OUTPUT_DATE = GETDATE(), OUTPUT_PC = '{1}' WHERE GYOUSHA_CD = '{2}' AND GENBA_CD = '{3}'", SystemProperty.UserName, SystemInformation.ComputerName, REGIST_GENBA_DIGI.GYOUSHA_CD, REGIST_GENBA_DIGI.GENBA_CD);

                    }
                    this.digiOutputGenbaDao.ExecuteForStringSql(sql);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E245");
                }
                return false;

            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region 削除処理
        /// <summary>
        /// 削除処理
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        [Transaction]
        public bool DeleteData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 連携削除処理
                // M_DIGI_OUTPUT_GENBAのみ物理削除
                // M_GENBA_DIGIのデータは関与しない
                var sql = string.Format("DELETE FROM M_DIGI_OUTPUT_GENBA WHERE GYOUSHA_CD = '{0}' AND GENBA_CD = '{1}'", REGIST_GENBA_DIGI.GYOUSHA_CD, REGIST_GENBA_DIGI.GENBA_CD);
                this.digiOutputGenbaDao.ExecuteForStringSql(sql);
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.msgLogic.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.msgLogic.MessageBoxShow("E245");
                }
                return false;

            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region データ送信
        /// <summary>
        /// デジタコ連携登録処理
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool RegistDataDigi()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var connect = logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_POINTS);
                logiLogic.HttpPut(connect.URL, connect.CONTENT_TYPE, this.REGIST_INFO_POINT);

            }
            catch (WebException)
            {
                //LogiLogic.cs側でエラー表示しているので何もしない
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        /// <summary>
        /// デジタコ連携削除処理
        /// </summary>
        /// <returns>true:正常, false:エラー</returns>
        internal bool DeleteDataDigi()
        {
            LogUtility.DebugMethodStart();
            try
            {
                var connect = logiConnectDao.GetDataByContentName(LogiConst.CONTENT_NAME_POINTS);
                var apiURL = string.Format(connect.URL + LogiConst.API_PARAMETER_URL_POINTS, SYS_INFO.DIGI_CORP_ID, this.REGIST_GENBA_DIGI.POINT_ID);
                logiLogic.HttpDelete(apiURL, connect.CONTENT_TYPE);
            }
            catch (WebException)
            {
                //LogiLogic.cs側でエラー表示しているので何もしない
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E245");
                return false;
            }
            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region デジタコ連携PK取得
        /// <summary>
        /// 地点IDから現場デジタコ連携情報のPK取得
        /// </summary>
        /// <param name="genbaDigi">現場_デジタコ連携情報マスタ</param>
        /// <returns>true:正常, false:エラー</returns>
        internal bool GetGenbaDigiKeyCode(out M_GENBA_DIGI genbaDigi)
        {
            genbaDigi = null;
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(this.form.POINT_ID.Text))
                {
                    var entitys = this.genbaDigiDao.GetDataByPointId(this.form.POINT_ID.Text);
                    if (entitys.Length == 0)
                    {
                        return result;
                    }
                    else if (1 < entitys.Length)
                    {
                        this.msgLogic.MessageBoxShowError("同一の地点IDが2件以上登録されています。");
                        return result;
                    }

                    var daialogResult = this.msgLogic.MessageBoxShow("C017");
                    if (DialogResult.No.Equals(daialogResult))
                    {
                        this.form.POINT_ID.Text = string.Empty;
                        this.form.POINT_ID.Focus();
                        return result;
                    }

                    genbaDigi = entitys.First();
                    result = true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetGenbaDigiKeyCode", ex1);
                this.msgLogic.MessageBoxShow("E093");
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGenbaDigiKeyCode", ex);
                this.msgLogic.MessageBoxShow("E245");
            }

            return result;
        }
        #endregion

        #region 外部地点、地図表示名の入力チェック
        /// <summary>
        /// 外部地点、地図表示名に半角カンマが入力されているかチェック
        /// </summary>
        /// <param name="pointName">外部地点名</param>
        /// <param name="mapName">外部地図表示名</param>
        /// <returns>true:正常, false:エラー</returns>
        internal bool GaibuPointMapNameCheck(string pointName, string mapName)
        {
            try
            {
                if (!string.IsNullOrEmpty(pointName) || !string.IsNullOrEmpty(mapName))
                {
                    if (pointName.Contains(",") && mapName.Contains(","))
                    {
                        this.msgLogic.MessageBoxShowError("外部地点名は、半角カンマを使用できません。\n外部地図表示名は、半角カンマを使用できません。");
                        return false;
                    }
                    else if (pointName.Contains(","))
                    {
                        this.msgLogic.MessageBoxShowError("外部地点名は、半角カンマを使用できません。");
                        return false;
                    }
                    else if (mapName.Contains(","))
                    {
                        this.msgLogic.MessageBoxShowError("外部地図表示名は、半角カンマを使用できません。");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GaibuPointMapNameCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }
        }

        #endregion

        #region 未使用

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        public int Search()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
