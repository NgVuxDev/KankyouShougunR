// $Id: LogicClass.cs 57278 2015-07-30 09:42:51Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MasterKyoutsuPopup2.APP;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Common.BusinessCommon.Xml;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku
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
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Allocation.TeikiHaisyaJisekiNyuuryoku.Setting.ButtonSetting.xml";

        /// <summary>
        /// UIForm form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// UIHeader headForm
        /// </summary>
        public UIHeader headForm;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;
        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// システム設定． 数量フォーマット
        /// </summary>
        private String systemSuuryouFormat;

        /// <summary>
        /// 定期実績入力情報のDao
        /// </summary>
        private IT_TEIKI_JISSEKI_ENTRYDao teikiEntryDao;

        /// <summary>
        /// 定期実績明細情報のDao
        /// </summary>
        public IT_TEIKI_JISSEKI_DETAILDao teikiDetailDao;

        /// <summary>
        /// 定期実績荷卸のDao
        /// </summary>
        private IT_TEIKI_JISSEKI_NIOROSHIDao teikiNioroshiDao;
        /// <summary>
        /// 定期配車入力情報のDao
        /// </summary>
        private IT_TEIKI_HAISHA_ENTRYDao teikiHaishaEntryDao;

        /// <summary>
        /// 定期配車明細情報のDao
        /// </summary>
        private IT_TEIKI_HAISHA_DETAILDao teikiHaishaDetailDao;

        /// <summary>
        /// 定期配車荷降のDao
        /// </summary>
        private IT_TEIKI_HAISHA_NIOROSHIDao teikiHaishaNioroshiDao;
        /// <summary>
        /// システムID採番Dao
        /// </summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// コース情報のDao
        /// </summary>
        private IM_COURSEDao courseDao;

        /// <summary>
        /// 伝種採番Dao
        /// </summary>
        private IS_NUMBER_DENSHUDao numberDenshuDao;
        /// <summary>
        /// ｺｰｽ名称CD
        /// </summary>
        public M_COURSE_NAME[] mCourseNameAll;

        /// <summary>
        /// 現場マスタのDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 車輌マスタのDao
        /// </summary>
        private IM_SHARYOUDao sharyouDao;
        /// <summary>
        /// 車種マスタのDao
        /// </summary>
        private IM_SHASHUDao shashuDao;
        /// <summary>
        /// 社員マスタのDao
        /// </summary>
        private IM_SHAINDao shainDao;
        /// <summary>
        ///単位マスタ Dao
        /// </summary>
        private IM_UNITDao unitDao;
        /// <summary>
        /// 単位
        /// </summary>
        private M_UNIT[] mUnitAll;

        /// <summary>
        /// 業者マスタDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;
        /// <summary>
        /// IM_KYOTENDao(拠点Dao)
        /// </summary>
        private IM_KYOTENDao kyotenDao;

        // 20141015 koukouei 休動管理機能 start
        /// <summary>
        /// 運転者休動マスタDao
        /// </summary>
        private IM_WORK_CLOSED_UNTENSHADao workClosedUntenshaDao;
        /// <summary>
        /// 車輌休動マスタDao
        /// </summary>
        private IM_WORK_CLOSED_SHARYOUDao workClosedSharyouDao;
        /// <summary>
        /// 荷降現場休動マスタDao
        /// </summary>
        private IM_WORK_CLOSED_HANNYUUSAKIDao workClosedHanYuusakiDao;
        // 20141015 koukouei 休動管理機能 end

        /// <summary>
        ///  収集GRIDセール背景色設定するリスト
        /// </summary>
        public List<DataGridViewCell> mCellAriList = new List<DataGridViewCell>();

        /// <summary>収集一覧で動的に作成される品名カラムの境界値:12</summary>
        private readonly int SyuusyuuColumnIndex = 11;

        /// <summary>
        /// 不正な入力をされたかを示します
        /// </summary>
        internal bool isInputError = false;

        /// <summary>
        /// 現場のDao（登録時用）
        /// </summary>
        internal IM_CUSTOM_GENBADao genbaCustomDao;

        internal MessageBoxShowLogic MsgBox;

        /// <summary>
        /// 車輌検索ポップアップをロジックで起動させたかのフラグ
        /// </summary>
        internal bool isCalledSharyouPopupFromLogic = false;

        /// <summary>
        /// モバイル連携Dao
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;

        /// <summary>
        /// 現場追加・品名追加ボタン押下有無フラグ
        /// </summary>
        internal bool hasGenbaHinmeiAdded = false;
        #endregion

        #region プロパティ
        /// <summary>
        /// 検索条件
        /// </summary>
        public DTOClass searchDto { get; set; }

        /// <summary>
        /// 定期実績番号
        /// </summary>
        public string teikiJisejiNumber { get; set; }

        /// <summary>
        /// 曜日CD（参照モード用）
        /// </summary>
        public string dayCd { get; set; }

        /// <summary>
        /// コース名称CD（参照モード用）
        /// </summary>
        public string courseNameCd { get; set; }

        /// <summary>
        /// 検索結果（定期実績入力）
        /// </summary>
        public DataTable searchResultEntry { get; set; }

        /// <summary>
        /// 検索結果（定期実績明細）
        /// </summary>
        public DataTable searchResultDetail { get; set; }
        /// <summary>
        /// 検索結果（定期実績荷卸）
        /// </summary>
        public DataTable searchResultNioroshi { get; set; }

        /// <summary>
        /// 検索結果（換算情報）
        /// </summary>
        public DataTable searchResultKannsannInfo { get; set; }

        ///// <summary>
        ///// 検索結果（コース明細）
        ///// </summary>
        //public DataTable searchResultCourseDetail { get; set; }

        ///// <summary>
        ///// 検索結果（コース荷卸先）
        ///// </summary>
        //public DataTable searchResultCourseNioroshi { get; set; }

        /// <summary>
        /// システム情報
        /// </summary>
        private M_SYS_INFO entitysM_SYS_INFO { get; set; }

        /// <summary>
        /// 定期実績入力テーブルの情報
        /// </summary>
        private T_TEIKI_JISSEKI_ENTRY entitysT_TEIKI_ENTRY { get; set; }

        /// <summary>
        /// 定期実績明細テーブルの情報
        /// </summary>
        private T_TEIKI_JISSEKI_DETAIL entitysT_TEIKI_DETAIL { get; set; }

        /// <summary>
        /// 定期実績明細リスト
        /// </summary>
        private List<T_TEIKI_JISSEKI_DETAIL> entityDetailList { get; set; }

        /// <summary>
        /// 定期実績荷卸テーブルの情報
        /// </summary>
        private T_TEIKI_JISSEKI_NIOROSHI entitysT_TEIKI_NIOROSHI { get; set; }

        /// <summary>
        /// 定期実績荷卸リスト
        /// </summary>
        private List<T_TEIKI_JISSEKI_NIOROSHI> entityNioroshilList { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }


        /// <summary>
        /// 詳細一覧データ
        /// </summary>
        public ShushuIchiranData mShushuIchiranData { get; set; }

        #endregion

        #region 初期化処理

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.teikiEntryDao = DaoInitUtility.GetComponent<IT_TEIKI_JISSEKI_ENTRYDao>();
            this.teikiDetailDao = DaoInitUtility.GetComponent<IT_TEIKI_JISSEKI_DETAILDao>();
            this.teikiNioroshiDao = DaoInitUtility.GetComponent<IT_TEIKI_JISSEKI_NIOROSHIDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<r_framework.Dao.IS_NUMBER_DENSHUDao>();
            this.teikiHaishaEntryDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_ENTRYDao>();         // 定期配車入力情報のDao
            this.teikiHaishaDetailDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_DETAILDao>();       // 定期配車明細情報のDao
            this.teikiHaishaNioroshiDao = DaoInitUtility.GetComponent<IT_TEIKI_HAISHA_NIOROSHIDao>();   // 定期配車荷降のDao
            this.genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();                 // 現場マスタのDao
            this.sharyouDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHARYOUDao>();             // 車輌マスタのDao           
            this.shashuDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHASHUDao>();               // 車種マスタのDao
            this.shainDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_SHAINDao>();                 // 社員マスタのDao
            this.kyotenDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();               // 拠点マスタのDao
            this.gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            // 20141015 koukouei 休動管理機能 start
            this.workClosedUntenshaDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_UNTENSHADao>();     // 運転者休動マスタDao
            this.workClosedSharyouDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_SHARYOUDao>();       // 車輌休動マスタDao
            this.workClosedHanYuusakiDao = DaoInitUtility.GetComponent<IM_WORK_CLOSED_HANNYUUSAKIDao>();// 搬入先休動マスタDao
            // 20141015 koukouei 休動管理機能 end
            this.genbaCustomDao = DaoInitUtility.GetComponent<IM_CUSTOM_GENBADao>();                    // 現場マスタのDao(登録時用)

            // 単位Dao
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
            // コース情報のDao
            this.courseDao = DaoInitUtility.GetComponent<IM_COURSEDao>();
            // モバイル連携Dao
            this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();

            //詳細一覧情報保存場所
            this.mShushuIchiranData = new ShushuIchiranData();
            this.MsgBox = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                this.parentForm = (BusinessBaseForm)this.form.Parent;

                // システム情報を取得
                this.GetSysInfoInit();

                // 単位情報を取得
                this.mUnitAll = this.unitDao.GetAllValidData(new M_UNIT() { ISNOT_NEED_DELETE_FLG = true });

                // ベースフォームオブジェクト取得
                var parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダー項目
                this.headForm = (UIHeader)((BusinessBaseForm)this.form.ParentForm).headerForm;

                // イベントの初期化処理
                this.EventInit(parentForm);

                //グリッド→DataTableへの変換イベント
                this.form.DetailIchiran.CellParsing += new DataGridViewCellParsingEventHandler(Ichiran_CellParsing);
                this.form.NioroshiIchiran.CellParsing += new DataGridViewCellParsingEventHandler(Ichiran_CellParsing);
                this.form.syuusyuuDetailIchiran.CellParsing += new DataGridViewCellParsingEventHandler(Ichiran_CellParsing);
                this.form.syuusyuuDetailIchiranSoukei.CellParsing += new DataGridViewCellParsingEventHandler(Ichiran_CellParsing);

                // ｺｰｽ情報 ポップアップ初期化
                PopUpDataInit();

                // 処理モード別画面初期化
                this.ModeInit(windowType, parentForm);

                if (this.form.mHaishaFlg)
                {
                    this.form.TEIKI_HAISHA_NUMBER.Text = this.form.bakTeikiHaishaNumber;

                    // 定期配車番号変更後処理の対応。空にしないと処理が中断されるため。
                    this.form.bakTeikiHaishaNumber = string.Empty;
                    this.form.TeikiHaishaNumberValidated(null, null);
                }

                // 数量書式修正 
                if (!string.IsNullOrEmpty(systemSuuryouFormat))
                {
                    this.form.DetailIchiran.Columns["SUURYOU"].DefaultCellStyle.Format = string.Format(systemSuuryouFormat);
                    this.form.DetailIchiran.Columns["ANBUN_SUURYOU"].DefaultCellStyle.Format = string.Format(systemSuuryouFormat);
                    this.form.DetailIchiran.Columns["KANSAN_SUURYOU"].DefaultCellStyle.Format = string.Format(systemSuuryouFormat);
                    this.form.NioroshiIchiran.Columns["NIOROSHI_RYOU"].DefaultCellStyle.Format = string.Format(systemSuuryouFormat);
                }
                else
                {
                    this.form.DetailIchiran.Columns["SUURYOU"].DefaultCellStyle.Format = string.Format("#,###");
                    this.form.DetailIchiran.Columns["ANBUN_SUURYOU"].DefaultCellStyle.Format = string.Format("#,###");
                    this.form.DetailIchiran.Columns["KANSAN_SUURYOU"].DefaultCellStyle.Format = string.Format("#,###");
                    this.form.NioroshiIchiran.Columns["NIOROSHI_RYOU"].DefaultCellStyle.Format = string.Format("#,###");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            // 拠点初期値設定
            this.SetInitKyoten();
        }

        #region システム情報を取得
        /// <summary>
        ///  システム情報を取得
        /// </summary>
        internal void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                    // 数量フォーマット
                    this.systemSuuryouFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_SUURYOU_FORMAT, string.Empty).ToString();
                }
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

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                var buttonSetting = this.CreateButtonInfo();
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        //<summary>
        //イベントの初期化処理
        //</summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                // F1切替　イベント生成
                parentForm.bt_func1.Click += new EventHandler(this.form.SetKirikaeFrom);

                // F2　新規イベント生成
                parentForm.bt_func2.Click += new EventHandler(this.form.CreateMode);

                // F3　修正ボイベント生成
                parentForm.bt_func3.Click += new EventHandler(this.form.UpdateMode);

                // F4　按分実行　イベント生成
                parentForm.bt_func4.Click += new EventHandler(this.form.AnbunnJicou);

                //  F5 現場追加 イベント生成
                parentForm.bt_func5.Click += new EventHandler(this.form.AddGenba);

                // F6 品名追加 イベント生成
                parentForm.bt_func6.Click += new EventHandler(this.form.AddHinnmei);

                // 一覧(F7)イベント生成 TODO
                parentForm.bt_func7.Click += new EventHandler(this.form.ShowIchiran);
                // 登録(F9)イベント生成
                this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                // 行削除ボタン(F11)イベント
                parentForm.bt_func11.Click += new EventHandler(this.form.RemoveRow);

                // 閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // 荷降行削除ボタン
                parentForm.bt_process1.Click -= new EventHandler(this.form.DeleteNioroshiRow);
                parentForm.bt_process1.Click += new EventHandler(this.form.DeleteNioroshiRow);

                // 実績番号変更後処理
                this.form.TEIKI_JISSEKI_NUMBER.Validated -= new EventHandler(this.form.TeikiHaishaJisekiNumberValidated);
                this.form.TEIKI_JISSEKI_NUMBER.Validated += new EventHandler(this.form.TeikiHaishaJisekiNumberValidated);

                // 配車番号変更後処理
                this.form.TEIKI_HAISHA_NUMBER.Validated -= new EventHandler(this.form.TeikiHaishaNumberValidated);
                this.form.TEIKI_HAISHA_NUMBER.Validated += new EventHandler(this.form.TeikiHaishaNumberValidated);

                // 運転者
                this.form.UNTENSHA_CD.Validated -= new EventHandler(this.form.UNTENSHA_CDValidated);
                this.form.UNTENSHA_CD.Validated += new EventHandler(this.form.UNTENSHA_CDValidated);
                this.form.UNTENSHA_CD.Enter -= new EventHandler(this.form.Control_Enter);
                this.form.UNTENSHA_CD.Enter += new EventHandler(this.form.Control_Enter);

                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Validated -= new EventHandler(this.form.UNPAN_GYOUSHA_CDValidated);
                this.form.UNPAN_GYOUSHA_CD.Validated += new EventHandler(this.form.UNPAN_GYOUSHA_CDValidated);
                this.form.UNPAN_GYOUSHA_CD.Enter -= new EventHandler(this.form.Control_Enter);
                this.form.UNPAN_GYOUSHA_CD.Enter += new EventHandler(this.form.Control_Enter);

                // コース
                this.form.COURSE_NAME_CD.Enter -= new EventHandler (this.form.Control_Enter);
                this.form.COURSE_NAME_CD.Enter += new EventHandler(this.form.Control_Enter);

                // 車種
                this.form.SHASHU_CD.Enter -= new EventHandler(this.form.Control_Enter);
                this.form.SHASHU_CD.Enter += new EventHandler(this.form.Control_Enter);

                // 車輌
                this.form.SHARYOU_CD.Enter -= new EventHandler(this.form.Control_Enter);
                this.form.SHARYOU_CD.Enter += new EventHandler(this.form.Control_Enter);

                // 補助員
                this.form.HOJOIN_CD.Enter -= new EventHandler(this.form.Control_Enter);
                this.form.HOJOIN_CD.Enter += new EventHandler(this.form.Control_Enter);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 処理モード別画面初期化処理
        /// </summary>
        /// <param name="windowType"></param>
        private void ModeInit(WINDOW_TYPE windowType, BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType, parentForm);
                // ボタンのテキストを初期化
                this.ButtonInit(parentForm);

                switch (windowType)
                {
                    // 【新規】モード
                    case WINDOW_TYPE.NEW_WINDOW_FLAG:
                        this.WindowInitNew(parentForm);
                        break;

                    // 【参照】モード
                    case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                        if (!this.WindowInitReference(parentForm)) { return; }
                        break;

                    // 【修正】モード
                    case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                        if (!this.WindowInitUpdate(parentForm)) { return; }
                        break;

                    // 【削除】モード
                    case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                        this.WindowInitDelete(parentForm);
                        break;

                    // デフォルトは【新規】モード
                    default:
                        this.WindowInitNew(parentForm);
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ModeInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期化処理モード【新規】
        /// </summary>
        public void WindowInitNew(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(this.teikiJisejiNumber))
                {
                    // 実績番号
                    this.form.TEIKI_JISSEKI_NUMBER.Text = string.Empty;
                    //【新規】モードで初期化
                    if (!WindowInitNewMode(parentForm)) { return; }
                }
                else
                {
                    //【複写】モードで初期化
                    WindowInitNewCopyMode(parentForm);
                }
                this.form.TEIKI_HAISHA_NUMBER.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNew", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 画面項目初期化
        /// </summary>
        public bool WindowInitNewMode(BusinessBaseForm parentForm)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 全コントロール操作可能とする
                this.AllControlLock(true);

                // 最新のSYS_INFOを取得 TODO
                M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
                if (sysInfo != null && sysInfo.Length > 0)
                {
                    this.entitysM_SYS_INFO = sysInfo[0];
                }
                else
                {
                    this.entitysM_SYS_INFO = null;
                }

                #region Header部項目
                // 拠点初期値設定
                this.SetInitKyoten();
                // 初回登録者、登録日時
                this.headForm.CreateDate.Text = string.Empty;
                this.headForm.CreateUser.Text = string.Empty;
                // 最終更新者、更新日時
                this.headForm.LastUpdateDate.Text = string.Empty;
                this.headForm.LastUpdateUser.Text = string.Empty;
                #endregion

                #region Detail-Header部項目
                //定期配車番号使用可
                this.form.TEIKI_HAISHA_NUMBER.Enabled = true;
                this.form.TEIKI_HAISHA_NUMBER.Text = string.Empty;
                // 天気
                this.form.txtWEATHER.Text = string.Empty;
                //// 伝票日付
                //this.form.DENPYOU_DATE.Value =DateTime.Today;
                // 振替配車
                this.form.FURIKAE_HAISHA_KBN.Text = "1";
                this.form.FURIKAE_HAISHA_KBN.ReadOnly = false;
                this.form.FURIKAE_HAISHA_KBN_1.Enabled = true;
                this.form.FURIKAE_HAISHA_KBN_2.Enabled = true;
                // 作業日
                this.form.SAGYOU_DATE.Value = this.parentForm.sysDate;
                // コース
                this.form.COURSE_NAME_CD.Text = string.Empty;
                this.form.COURSE_NAME_RYAKU.Text = string.Empty;
                this.form.DAY_CD.Text = string.Empty;
                // 車種
                this.form.SHASHU_CD.Text = string.Empty;
                this.form.SHASHU_NAME_RYAKU.Text = string.Empty;
                // 車輌
                this.form.SHARYOU_CD.Text = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                // 運転者
                this.form.UNTENSHA_CD.Text = string.Empty;
                this.form.UNTENSHA_NAME.Text = string.Empty;
                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                // 補助員
                this.form.HOJOIN_CD.Text = string.Empty;
                this.form.HOJOIN_NAME.Text = string.Empty;
                // 出庫メーター
                this.form.SHUKKO_METER.Text = string.Empty;
                // 出庫時間_時
                this.form.SHUKKO_HOUR.Text = string.Empty;
                // 出庫時間_分
                this.form.SHUKKO_MINUTE.Text = string.Empty;
                // 帰庫メーター
                this.form.KIKO_METER.Text = string.Empty;
                // 帰庫時間_時
                this.form.KIKO_HOUR.Text = string.Empty;
                // 帰庫時間_分
                this.form.KIKO_MINUTE.Text = string.Empty;
                #endregion

                #region Detail-Detail-1部項目
                // 明細クリア
                // 明細をクリアする前に、単位と換算後単位のReadOnlyをtrueに設定する。
                // 単位もしくは換算後単位にフォーカスが当たった状態だとセルが未確定と判断されるため。
                for (int i = 0; i < this.form.DetailIchiran.Rows.Count - 1; i++)
                {
                    this.form.DetailIchiran.Rows[i].Cells["UNIT_CD"].ReadOnly = true;
                    this.form.DetailIchiran.Rows[i].Cells["KANSAN_UNIT_CD"].ReadOnly = true;
                }
                this.form.DetailIchiran.Rows.Clear();
                #endregion

                #region Detail-Detail-3部項目
                // 明細クリア
                this.form.syuusyuuDetailIchiran.Rows.Clear();
                #endregion

                #region Detail-Detail-3部項目
                // 明細クリア
                this.form.NioroshiIchiran.Rows.Clear();
                #endregion

                #region Detail-Detail-4部項目
                // 明細クリア
                this.form.syuusyuuDetailIchiranSoukei.Rows.Clear();
                #endregion

                #region Footer部項目

                // functionボタン
                parentForm.bt_func1.Enabled = true;     // 切替：使用可
                parentForm.bt_func2.Enabled = true;     // 新規：使用可
                parentForm.bt_func3.Enabled = false;    // 修正：使用不可
                parentForm.bt_func4.Enabled = true;     // 按分実行：使用可
                parentForm.bt_func5.Enabled = true;     // 現場追加：使用可
                parentForm.bt_func6.Enabled = true;     // 品名追加：使用可
                parentForm.bt_func7.Enabled = true;     // 一覧：使用可               
                parentForm.bt_func9.Enabled = true;     // 登録：使用可
                //parentForm.bt_func11.Enabled = false;   // 行削除：使用不可
                parentForm.bt_func12.Enabled = true;    // 閉じる：使用可

                // 処理No(ESC)
                parentForm.txb_process.Enabled = true;
                #endregion

                // 初期カーソル位置を予めセット
                this.form.DetailIchiran.CurrentCell = this.form.DetailIchiran.Rows[0].Cells[ConstCls.DetailColName.ROUND_NO];
                this.form.NioroshiIchiran.CurrentCell = this.form.NioroshiIchiran.Rows[0].Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD];

                // #21305 例外対策のため、集計単位のみ個別で指定。
                this.form.DetailIchiran.Rows[0].Cells[ConstCls.DetailColName.TSUKIGIME_KBN].ReadOnly = true;

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewMode", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }


        #region 拠点設定
        /// <summary>
        /// 拠点初期値設定
        /// </summary>
        private void SetInitKyoten()
        {
            LogUtility.DebugMethodStart();

            // 既にデータが入ってる場合はデフォルト値で上書きしない
            if (true == string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
            {
                // 拠点
                CurrentUserCustomConfigProfile userProfile = CurrentUserCustomConfigProfile.Load();
                this.headForm.KYOTEN_CD.Text = this.GetUserProfileValue(userProfile, "拠点CD");
                if (!string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text.ToString()))
                {
                    this.headForm.KYOTEN_CD.Text = this.headForm.KYOTEN_CD.Text.ToString().PadLeft(this.headForm.KYOTEN_CD.MaxLength, '0');
                    this.CheckKyotenCd();
                }
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ユーザー定義情報取得処理
        /// <summary>
        /// ユーザー定義情報取得処理
        /// </summary>
        /// <param name="profile">ＸＭＬファイルにアクセスするためのクラス</param>
        /// <param name="key">キー</param>
        /// <returns>キーに紐づく値</returns>
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

        #region ヘッダーの拠点CDの存在チェック
        /// <summary>
        /// ヘッダーの拠点CDの存在チェック
        /// </summary>
        internal void CheckKyotenCd()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.headForm.KYOTEN_NAME_RYAKU.Text = string.Empty;

                if (string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
                {
                    this.headForm.KYOTEN_NAME_RYAKU.Text = string.Empty;
                    return;
                }

                short kyoteCd = -1;
                if (!short.TryParse(this.headForm.KYOTEN_CD.Text, out kyoteCd))
                {
                    return;
                }

                M_KYOTEN keyEntity = new M_KYOTEN();
                keyEntity.KYOTEN_CD = kyoteCd;
                var kyotens = this.kyotenDao.GetAllValidData(keyEntity);

                // 存在チェック
                if (kyotens == null || kyotens.Length < 1)
                {
                    //MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    //msgLogic.MessageBoxShow("E020", "拠点");
                    //this.headerForm.KYOTEN_CD.Focus();
                    return;
                }
                else
                {
                    // キーが１つなので複数はヒットしないはず
                    M_KYOTEN kyoten = kyotens[0];
                    this.headForm.KYOTEN_NAME_RYAKU.Text = kyoten.KYOTEN_NAME_RYAKU.ToString();
                }
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

        /// <summary>
        /// 画面項目初期化【複写】モード
        /// </summary>
        private void WindowInitNewCopyMode(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 全コントロールを操作可能とする
                this.AllControlLock(true);

                // データをDBから取得
                this.SetWindowData();
                // 画面項目初期化
                if (!this.WindowInitNewMode(parentForm)) { return; }
                // 検索結果を画面に設定
                this.SetDataForWindow();

                // 配車実績番号:ブランク
                this.form.TEIKI_JISSEKI_NUMBER.Text = string.Empty;
                this.form.SAGYOU_DATE.Text = string.Empty;  // No.2676
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitNewCopyMode", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// 画面項目初期化【修正】モード
        /// </summary>
        public bool WindowInitUpdate(BusinessBaseForm parentForm)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 全コントロールを操作可能とする
                this.AllControlLock(true);

                // データをDBから取得
                this.SetWindowData();
                // 画面項目初期化
                if (!this.WindowInitNewMode(parentForm))
                {
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                // 検索結果を画面に設定
                this.SetDataForWindow();
                //定期配車番号使用不可
                this.form.TEIKI_JISSEKI_NUMBER.Enabled = false;
                this.form.TEIKI_HAISHA_NUMBER.Enabled = false;
                #region Footer部項目

                // functionボタン
                parentForm.bt_func1.Enabled = true;     // 切替：使用可
                parentForm.bt_func2.Enabled = true;     // 新規：使用可
                parentForm.bt_func3.Enabled = true;    // 修正：使用可
                parentForm.bt_func4.Enabled = true;     // 按分実行：使用可
                parentForm.bt_func5.Enabled = true;     // 現場追加：使用可
                parentForm.bt_func6.Enabled = true;     // 品名追加：使用可
                parentForm.bt_func7.Enabled = true;     // 一覧：使用可               
                parentForm.bt_func9.Enabled = true;     // 登録：使用可
                //parentForm.bt_func11.Enabled = false;   // 行削除：使用不可
                parentForm.bt_func12.Enabled = true;    // 閉じる：使用可
                // 処理No(ESC)
                parentForm.txb_process.Enabled = true;
                #endregion
                //数量書式ために、システム情報取得
                this.GetSysInfoInit();

                // 数量書式修正 
                if (!string.IsNullOrEmpty(systemSuuryouFormat))
                {
                    this.form.DetailIchiran.Columns["SUURYOU"].DefaultCellStyle.Format = string.Format(systemSuuryouFormat);
                    this.form.DetailIchiran.Columns["ANBUN_SUURYOU"].DefaultCellStyle.Format = string.Format(systemSuuryouFormat);
                    this.form.DetailIchiran.Columns["KANSAN_SUURYOU"].DefaultCellStyle.Format = string.Format(systemSuuryouFormat);
                    this.form.NioroshiIchiran.Columns["NIOROSHI_RYOU"].DefaultCellStyle.Format = string.Format(systemSuuryouFormat);
                }
                else
                {
                    this.form.DetailIchiran.Columns["SUURYOU"].DefaultCellStyle.Format = string.Format("#,###");
                    this.form.DetailIchiran.Columns["ANBUN_SUURYOU"].DefaultCellStyle.Format = string.Format("#,###");
                    this.form.DetailIchiran.Columns["KANSAN_SUURYOU"].DefaultCellStyle.Format = string.Format("#,###");
                    this.form.NioroshiIchiran.Columns["NIOROSHI_RYOU"].DefaultCellStyle.Format = string.Format("#,###");
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitUpdate", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 画面項目初期化【参照】モード
        /// </summary>
        public bool WindowInitReference(BusinessBaseForm parentForm)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // データをDBから取得
                this.SetWindowData();
                // 画面項目初期化
                if (!this.WindowInitNewMode(parentForm))
                {
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                // 検索結果を画面に設定
                this.SetDataForWindow();
                //定期配車番号使用不可
                this.form.TEIKI_HAISHA_NUMBER.Enabled = false;
                // 全コントロールを操作不可とする
                this.AllControlLock(false);

                #region Footer部項目

                // functionボタン
                parentForm.bt_func1.Enabled = false;     // 切替：使用可
                parentForm.bt_func2.Enabled = true;     // 新規：使用可
                parentForm.bt_func3.Enabled = false;    // 修正：使用可
                parentForm.bt_func4.Enabled = false;     // 按分実行：使用可
                parentForm.bt_func5.Enabled = false;     // 現場追加：使用可
                parentForm.bt_func6.Enabled = false;     // 品名追加：使用可
                parentForm.bt_func7.Enabled = true;     // 一覧：使用可               
                parentForm.bt_func9.Enabled = false;     // 登録：使用可
                //parentForm.bt_func11.Enabled = false;   // 行削除：使用不可
                parentForm.bt_func12.Enabled = true;    // 閉じる：使用可
                // 処理No(ESC)
                parentForm.txb_process.Enabled = false;
                #endregion

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitDelete", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 画面項目初期化【削除】モード
        /// </summary>
        public void WindowInitDelete(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // データをDBから取得
                this.SetWindowData();
                // 画面項目初期化
                if (!this.WindowInitNewMode(parentForm)) { return; }
                // 検索結果を画面に設定
                this.SetDataForWindow();
                //定期配車番号使用不可
                this.form.TEIKI_HAISHA_NUMBER.Enabled = false;
                // 全コントロールを操作不可とする
                this.AllControlLock(false);

                #region Footer部項目

                // functionボタン
                parentForm.bt_func1.Enabled = true;     // 切替：使用可
                parentForm.bt_func2.Enabled = true;     // 新規：使用可
                parentForm.bt_func3.Enabled = true;    // 修正：使用可
                parentForm.bt_func4.Enabled = false;     // 按分実行：使用可
                parentForm.bt_func5.Enabled = false;     // 現場追加：使用可
                parentForm.bt_func6.Enabled = false;     // 品名追加：使用可
                parentForm.bt_func7.Enabled = false;     // 一覧：使用可               
                parentForm.bt_func9.Enabled = true;     // 登録：使用可
                //parentForm.bt_func11.Enabled = false;   // 行削除：使用不可
                parentForm.bt_func12.Enabled = true;    // 閉じる：使用可
                // 処理No(ESC)
                parentForm.txb_process.Enabled = false;
                #endregion

            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInitDelete", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// データをDBから取得
        /// </summary>
        private void SetWindowData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 定期実績番号を検索条件に設定する
                this.searchDto = new DTOClass();
                searchDto.TeikiJissekiNumber = this.teikiJisejiNumber;

                // 定期実績入力情報を取得する
                searchResultEntry = null;
                searchResultEntry = teikiEntryDao.GetEntryData(this.searchDto);

                if (this.searchResultEntry.Rows.Count == 0)
                {
                    //メッセージ
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E045");

                    // 処理モードを新規に変更
                    this.form.SetWindowType(WINDOW_TYPE.NEW_WINDOW_FLAG);
                    // 画面項目初期化【新規】モード
                    this.WindowInitNewMode((BusinessBaseForm)this.form.Parent);
                    return;
                }

                // システムID、枝番を検索条件に設定する
                searchDto.SystemId = long.Parse(this.searchResultEntry.Rows[0]["SYSTEM_ID"].ToString());
                searchDto.Seq = int.Parse(this.searchResultEntry.Rows[0]["SEQ"].ToString());

                // 定期実績明細情報を取得する
                searchResultDetail = teikiDetailDao.GetDetailData(this.searchDto);

                // 定期実績荷卸情報を取得する
                searchResultNioroshi = teikiNioroshiDao.GetNioroshiData(this.searchDto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetWindowData", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 検索結果を画面に設定
        /// </summary>
        public void SetDataForWindow()
        {
            try
            {
                LogUtility.DebugMethodStart();

                string strDate;
                string strHour;
                string strMinute;

                #region Header部項目
                searchResultEntry.BeginLoadData();
                if (this.searchResultEntry.Rows.Count == 0)
                {
                    return;
                }

                // 拠点CD、略称
                if (!string.IsNullOrEmpty(this.searchResultEntry.Rows[0]["KYOTEN_CD"].ToString()))
                {
                    this.headForm.KYOTEN_CD.Text = int.Parse(this.searchResultEntry.Rows[0]["KYOTEN_CD"].ToString()).ToString("D2");
                }
                this.headForm.KYOTEN_NAME_RYAKU.Text = this.searchResultEntry.Rows[0]["KYOTEN_NAME_RYAKU"].ToString();
                // 初回登録者、登録日時
                this.headForm.CreateDate.Text = this.searchResultEntry.Rows[0]["CREATE_DATE"].ToString();
                this.headForm.CreateUser.Text = this.searchResultEntry.Rows[0]["CREATE_USER"].ToString();
                // 最終更新者、更新日時
                this.headForm.LastUpdateDate.Text = this.searchResultEntry.Rows[0]["UPDATE_DATE"].ToString();
                this.headForm.LastUpdateUser.Text = this.searchResultEntry.Rows[0]["UPDATE_USER"].ToString();
                #endregion

                #region Detail-Header部項目
                // 実績番号
                this.form.TEIKI_JISSEKI_NUMBER.Text = this.searchResultEntry.Rows[0]["TEIKI_JISSEKI_NUMBER"].ToString();
                // 天気
                this.form.txtWEATHER.Text = this.searchResultEntry.Rows[0]["WEATHER"].ToString();
                // 配車番号
                this.form.TEIKI_HAISHA_NUMBER.Text = this.searchResultEntry.Rows[0]["TEIKI_HAISHA_NUMBER"].ToString();
                //// 伝票日付
                //if (!string.IsNullOrEmpty(this.searchResultEntry.Rows[0]["DENPYOU_DATE"].ToString()))
                //{
                //    this.form.DENPYOU_DATE.Value = DateTime.Parse(this.searchResultEntry.Rows[0]["DENPYOU_DATE"].ToString());
                //}                
                // 振替配車
                this.form.FURIKAE_HAISHA_KBN.Text = this.searchResultEntry.Rows[0]["FURIKAE_HAISHA_KBN"].ToString();
                if (string.IsNullOrEmpty(this.form.TEIKI_HAISHA_NUMBER.Text))
                {
                    this.form.FURIKAE_HAISHA_KBN.ReadOnly = false;
                    this.form.FURIKAE_HAISHA_KBN_1.Enabled = true;
                    this.form.FURIKAE_HAISHA_KBN_2.Enabled = true;
                }
                else
                {
                    this.form.FURIKAE_HAISHA_KBN.ReadOnly = true;
                    this.form.FURIKAE_HAISHA_KBN_1.Enabled = false;
                    this.form.FURIKAE_HAISHA_KBN_2.Enabled = false;
                }

                if (!string.IsNullOrEmpty(this.searchResultEntry.Rows[0]["DAY_CD"].ToString()))
                {
                    this.form.DAY_CD.Text = this.searchResultEntry.Rows[0]["DAY_CD"].ToString();
                }

                // 作業日
                if (!string.IsNullOrEmpty(this.searchResultEntry.Rows[0]["SAGYOU_DATE"].ToString()))
                {
                    this.form.SAGYOU_DATE.Value = DateTime.Parse(this.searchResultEntry.Rows[0]["SAGYOU_DATE"].ToString());
                }
                // コース
                this.form.COURSE_NAME_CD.Text = this.searchResultEntry.Rows[0]["COURSE_NAME_CD"].ToString();
                this.form.COURSE_NAME_RYAKU.Text = this.searchResultEntry.Rows[0]["COURSE_NAME_RYAKU"].ToString();
                // 車種
                this.form.SHASHU_CD.Text = this.searchResultEntry.Rows[0]["SHASHU_CD"].ToString();
                this.form.SHASHU_NAME_RYAKU.Text = this.searchResultEntry.Rows[0]["SHASHU_NAME_RYAKU"].ToString();
                // 車輌
                this.form.SHARYOU_CD.Text = this.searchResultEntry.Rows[0]["SHARYOU_CD"].ToString();
                this.form.SHARYOU_NAME_RYAKU.Text = this.searchResultEntry.Rows[0]["SHARYOU_NAME_RYAKU"].ToString();
                // 運転者
                this.form.UNTENSHA_CD.Text = this.searchResultEntry.Rows[0]["UNTENSHA_CD"].ToString();
                this.form.UNTENSHA_NAME.Text = this.searchResultEntry.Rows[0]["UNTENSHA_NAME"].ToString();
                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Text = this.searchResultEntry.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                this.form.UNPAN_GYOUSHA_NAME.Text = this.searchResultEntry.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString();
                // 補助員
                this.form.HOJOIN_CD.Text = this.searchResultEntry.Rows[0]["HOJOIN_CD"].ToString();
                this.form.HOJOIN_NAME.Text = this.searchResultEntry.Rows[0]["HOJOIN_NAME"].ToString();
                // 出庫
                this.form.SHUKKO_METER.Text = this.searchResultEntry.Rows[0]["SHUKKO_METER"].ToString();
                this.form.SHUKKO_HOUR.Text = this.searchResultEntry.Rows[0]["SHUKKO_HOUR"].ToString();
                this.form.SHUKKO_MINUTE.Text = this.searchResultEntry.Rows[0]["SHUKKO_MINUTE"].ToString();
                // 帰庫
                this.form.KIKO_METER.Text = this.searchResultEntry.Rows[0]["KIKO_METER"].ToString();
                this.form.KIKO_HOUR.Text = this.searchResultEntry.Rows[0]["KIKO_HOUR"].ToString();
                this.form.KIKO_MINUTE.Text = this.searchResultEntry.Rows[0]["KIKO_MINUTE"].ToString();

                // 排他用タイムスタンプ（非表示）
                byte[] timeStamp = (byte[])this.searchResultEntry.Rows[0]["TIME_STAMP"];
                this.form.TIME_STAMP_ENTRY.Text = ConvertStrByte.ByteToString(timeStamp);
                // システムID（非表示）
                this.form.SYSTEM_ID.Text = this.searchResultEntry.Rows[0]["SYSTEM_ID"].ToString();
                // 枝番（非表示）
                this.form.SEQ.Text = this.searchResultEntry.Rows[0]["SEQ"].ToString();

                #endregion

                #region Detail-Detail-1部項目

                if (!this.InitDetailIchiranGrid(true)) { return; }

                #endregion

                #region Detail-Detail-3部項目
                // 検索結果（定期実績荷卸）
                searchResultNioroshi.BeginLoadData();

                // 明細クリア
                this.form.NioroshiIchiran.Rows.Clear();
                if (searchResultNioroshi.Rows.Count > 0)
                {
                    // 明細行を追加
                    this.form.NioroshiIchiran.Rows.Add(searchResultNioroshi.Rows.Count);
                    // 検索結果設定
                    for (int j = 0; j < searchResultNioroshi.Rows.Count; j++)
                    {
                        // 荷降No
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_NUMBER, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_NUMBER], string.Empty);
                        //障害 #11416  定期配車実績入力　確定済みの行で使用している荷降行が変更できてしまう。
                        bool catchErr = false;
                        if (this.CheckClmOKDetailIchiran(Convert.ToString(this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_NUMBER, j].Value), out catchErr))
                        {
                            if (catchErr) { return; }
                            foreach (DataGridViewCell cell in this.form.NioroshiIchiran.Rows[j].Cells)
                            {
                                if (!cell.ReadOnly)
                                {
                                    cell.ReadOnly = true;
                                    var ia = cell as ICustomAutoChangeBackColor;
                                    if (ia != null)
                                    {
                                        CustomControlExtLogic.UpdateBackColor(ia);
                                    }
                                }
                            }
                        }
                        //障害 #11416  定期配車実績入力　確定済みの行で使用している荷降行が変更できてしまう。
                        // 荷降業者CD
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], string.Empty);
                        // 荷降業者名
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU], string.Empty);
                        // 荷降現場CD
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], string.Empty);
                        // 荷降現場名
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU], string.Empty);
                        // 荷降量
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_RYOU, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.NIOROSHI_RYOU], string.Empty);
                        // 単位
                        this.form.NioroshiIchiran["UNIT", j].Value = "kg";

                        if (this.TryChgDateTimeToDHM(searchResultNioroshi.Rows[j][ConstCls.NioroshiColName.HANNYUU_DATE], out strDate, out strHour, out strMinute))
                        {
                            // 搬入時
                            this.form.NioroshiIchiran[ConstCls.NioroshiColName.HANNYUU_HOUR, j].Value = strHour;
                            // 搬入分
                            this.form.NioroshiIchiran[ConstCls.NioroshiColName.HANNYUU_MIN, j].Value = strMinute;

                        }
                        // TIME_STAMP
                        this.form.NioroshiIchiran[ConstCls.NioroshiColName.TIME_STAMP_NIOROSHI, j].Value = this.ChgDBNullToValue(searchResultNioroshi.Rows[j]["TIME_STAMP"], string.Empty);

                    }

                }
                #endregion

                // 初期カーソル位置を予めセット
                this.form.DetailIchiran.CurrentCell = this.form.DetailIchiran.Rows[0].Cells[ConstCls.DetailColName.ROUND_NO];
                //20150925 hoanghm #12912 start
                //this.form.NioroshiIchiran.CurrentCell = this.form.NioroshiIchiran.Rows[0].Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD];
                this.form.NioroshiIchiran.CurrentCell = this.form.NioroshiIchiran.Rows[0].Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER];
                //20150925 hoanghm #12912 end
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDataForWindow", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 全コントロール制御
        /// </summary>
        /// <param name="isBool">true:操作可、false:操作不可</param>
        private void AllControlLock(bool isBool)
        {
            try
            {
                LogUtility.DebugMethodStart(isBool);

                #region Header部項目
                // 拠点
                this.headForm.KYOTEN_CD.Enabled = isBool;
                #endregion

                #region Detail-Header部項目
                // 実績番号
                this.form.TEIKI_JISSEKI_NUMBER.Enabled = isBool;

                if (this.form.WindowType == WINDOW_TYPE.REFERENCE_WINDOW_FLAG)
                {
                    // 実績番号前ボタン
                    this.form.previousButton.Enabled = true;
                    // 実績番号次ボタン
                    this.form.nextButton.Enabled = true;
                }
                else
                {
                    // 実績番号前ボタン
                    this.form.previousButton.Enabled = isBool;
                    // 実績番号次ボタン
                    this.form.nextButton.Enabled = isBool;
                }

                // 天気
                this.form.txtWEATHER.Enabled = isBool;
                //// 伝票日付
                //this.form.DENPYOU_DATE.Enabled = isBool;
                // 作業日
                this.form.SAGYOU_DATE.Enabled = isBool;
                // コース
                this.form.COURSE_NAME_CD.Enabled = isBool;
                // 車種
                this.form.SHASHU_CD.Enabled = isBool;
                // 車輌
                this.form.SHARYOU_CD.Enabled = isBool;
                // 運転者
                this.form.UNTENSHA_CD.Enabled = isBool;
                // 運搬業者
                this.form.UNPAN_GYOUSHA_CD.Enabled = isBool;
                // 補助員
                this.form.HOJOIN_CD.Enabled = isBool;
                // 出庫
                this.form.SHUKKO_METER.Enabled = isBool;
                // 出庫時間_時
                this.form.SHUKKO_HOUR.Enabled = isBool;
                // 出庫時間_分
                this.form.SHUKKO_MINUTE.Enabled = isBool;
                //  帰庫
                this.form.KIKO_METER.Enabled = isBool;
                // 帰庫時間_時
                this.form.KIKO_HOUR.Enabled = isBool;
                // 帰庫時間_分
                this.form.KIKO_MINUTE.Enabled = isBool;
                // 振替配車
                this.form.FURIKAE_HAISHA_KBN.Enabled = isBool;
                this.form.FURIKAE_HAISHA_KBN_1.Enabled = isBool;
                this.form.FURIKAE_HAISHA_KBN_2.Enabled = isBool;
                #endregion

                #region Detail-Detail-1部項目
                // 定期実績明細
                this.form.DetailIchiran.ReadOnly = !isBool;
                // 入力不可列を再設定
                if (isBool)
                {
                    // NO
                    this.form.DetailIchiran.Columns["KAKUTEI_FLG"].ReadOnly = isBool;
                    // 入力区分
                    this.form.DetailIchiran.Columns[ConstCls.DetailColName.INPUT_KBN_NAME].ReadOnly = isBool;
                    // 業者名
                    this.form.DetailIchiran.Columns[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].ReadOnly = isBool;
                    // 現場名
                    this.form.DetailIchiran.Columns[ConstCls.DetailColName.GENBA_NAME_RYAKU].ReadOnly = isBool;
                    // 品名
                    this.form.DetailIchiran.Columns[ConstCls.DetailColName.HINMEI_NAME_RYAKU].ReadOnly = isBool;
                    // 伝票区分
                    this.form.DetailIchiran.Columns[ConstCls.DetailColName.DENPYOU_KBN_CD_NM].ReadOnly = isBool;
                    // 契約区分
                    this.form.DetailIchiran.Columns[ConstCls.DetailColName.KEIYAKU_KBN_NM].ReadOnly = isBool;
                    // 集計単位
                    // #21305 特定パターン時のみArgumentOutOfRangeExceptionの例外が発生するため回避
                    // 1.修正モードで契約区分が「2:単価」以外の明細を選択して、現場追加or品名追加ボタン押下。
                    // ※選択行以降の明細に契約区分「2:単価」の明細があること
                    // 2.修正ボタンor新規ボタンで例外が発生。
                    if (!hasGenbaHinmeiAdded)
                    {
                        this.form.DetailIchiran.Columns[ConstCls.DetailColName.TSUKIGIME_KBN].ReadOnly = isBool;
                    }
                    this.form.DetailIchiran.Columns[ConstCls.DetailColName.TSUKIGIME_KBN_NM].ReadOnly = isBool;
                }

                #endregion

                #region Detail-Detail-2部項目
                // 定期実績明細
                this.form.syuusyuuDetailIchiran.ReadOnly = !isBool;
                // 入力不可列を再設定
                if (isBool)
                {
                    // NO
                    this.form.syuusyuuDetailIchiran.Columns["clmOk2"].ReadOnly = isBool;
                    // 業者名
                    this.form.syuusyuuDetailIchiran.Columns["clmGYOUSHA_NAME_RYAKU"].ReadOnly = isBool;
                    // 現場名
                    this.form.syuusyuuDetailIchiran.Columns["clmGENBA_NAME_RYAKU"].ReadOnly = isBool;
                }

                #endregion

                #region Detail-Detail-3部項目
                // 定期実績荷卸部
                this.form.NioroshiIchiran.ReadOnly = !isBool;
                // 入力不可列を再設定
                if (isBool)
                {
                    // 荷降No
                    this.form.NioroshiIchiran.Columns[ConstCls.NioroshiColName.NIOROSHI_NUMBER].ReadOnly = isBool;
                    // 荷降業者名
                    this.form.NioroshiIchiran.Columns[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].ReadOnly = isBool;
                    // 荷降現場名
                    this.form.NioroshiIchiran.Columns[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].ReadOnly = isBool;
                    // 単位
                    this.form.NioroshiIchiran.Columns["UNIT"].ReadOnly = isBool;
                }

                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("AllControlLock", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        /// <summary>
        /// //グリッド→DataTableへの変換イベント
        /// </summary>
        /// <param name="sender">イベントが発生したコントロール</param>
        /// <param name="e">変換情報</param>
        private void Ichiran_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            // 空文字を入力された場合
            if ("".Equals(e.Value))
            {
                e.Value = System.DBNull.Value;  // AllowDBNull=trueの場合は nullはNG DBNullはOK
                e.ParsingApplied = true;        // 後続の解析不要
            }
        }

        /// <summary>
        /// 拠点CD、作業日チェック
        /// コース名を入力する場合の拠点CD、作業日は必須
        /// </summary>
        /// <returns>true:問題なし, false:問題あり</returns>
        internal bool CheckKyotenAndSagyouDate()
        {
            bool returnVal = true;
            try
            {
                String errMessage = string.Empty;
                // 拠点に入力が無い場合エラー
                if (string.IsNullOrEmpty(this.headForm.KYOTEN_CD.Text))
                {
                    errMessage = "拠点CD";
                }

                int errorKbn = 0;
                // 振替配車に入力が無い場合エラー
                if (string.IsNullOrEmpty(this.form.FURIKAE_HAISHA_KBN.Text))
                {
                    if (string.IsNullOrEmpty(errMessage))
                    {
                        errMessage = "振替配車";
                    }
                    else
                    {
                        errMessage = errMessage + "、振替配車";
                    }
                    errorKbn = 1;
                }

                // 作業日に入力が無い場合エラー
                if (this.form.SAGYOU_DATE.Value == null)
                {
                    if (string.IsNullOrEmpty(errMessage))
                    {
                        errMessage = "作業日";
                    }
                    else
                    {
                        errMessage = errMessage + "、作業日";
                    }
                    if (errorKbn == 0)
                    {
                        errorKbn = 2;
                    }
                }

                // エラーがあるの場合
                if (!string.IsNullOrEmpty(errMessage))
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E034", errMessage);
                    if (errorKbn == 1)
                    {
                        this.form.FURIKAE_HAISHA_KBN.Focus();
                    }
                    else
                    {
                        this.form.SAGYOU_DATE.Focus();
                    }
                    returnVal = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKyotenAndSagyouDate", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
            }
            return returnVal;
        }

        /// <summary>
        /// ｺｰｽ情報 ポップアップ初期化
        /// </summary>
        public void PopUpDataInit(string courseNameCd = null)
        {
            // ｺｰｽ情報 ポップアップ取得
            // 表示用データ取得＆加工
            var ShainDataTable = this.GetPopUpData(this.form.COURSE_NAME_CD.PopupGetMasterField.Split(',').Select(s => s.Trim().ToUpper()), courseNameCd);
            // TableNameを設定すれば、ポップアップのタイトルになる
            ShainDataTable.TableName = "ｺｰｽ名称情報";

            // 列名とデータソース設定
            this.form.COURSE_NAME_CD.PopupDataHeaderTitle = new string[] { "ｺｰｽ名称CD", "ｺｰｽ名称", "曜日" };
            this.form.COURSE_NAME_CD.PopupDataSource = ShainDataTable;
        }

        /// <summary>
        /// マスタポップアップ用データテーブル取得
        /// </summary>
        /// <param name="displayCol">表示対象列(物理名)</param>
        /// <returns></returns>
        public DataTable GetPopUpData(IEnumerable<string> displayCol, string courseNameCD = null)
        {
            LogUtility.DebugMethodStart(displayCol, courseNameCD);

            DataTable ret = null;

            LogUtility.DebugMethodEnd(ret);

            var mCourseDetailDao = DaoInitUtility.GetComponent<IM_COURSE_DETAILDao>();

            var courseNameListDto = new CourseNameListDto();
            courseNameListDto.SagyouDate = Convert.ToDateTime(this.form.SAGYOU_DATE.Value);
            // 曜日CD
            if (this.form.FURIKAE_HAISHA_KBN.Text != "2" && this.form.SAGYOU_DATE.Value != null)
            {
                courseNameListDto.DayCd = DateUtility.GetShougunDayOfWeek(courseNameListDto.SagyouDate);
            }
            courseNameListDto.KyotenCd = this.headForm.KYOTEN_CD.Text;

            // コースCD
            if (!string.IsNullOrEmpty(courseNameCD))
            {
                courseNameListDto.CourseNameCd = courseNameCD;
            }
            ret = mCourseDetailDao.GetCourseNameListForPopup(courseNameListDto);

            return ret;
        }

        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        public void CheckPopup(KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (e.KeyCode == Keys.Space)
            {
                if (this.form.DetailIchiran.Columns[this.form.DetailIchiran.CurrentCell.ColumnIndex].Name.Equals("TSUKIGIME_KBN"))
                {

                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;
                    row = dt.NewRow();
                    row["CD"] = "1";
                    row["VALUE"] = "伝票";
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "合算";
                    dt.Rows.Add(row);
                    dt.TableName = "集計単位";
                    form.table = dt;
                    form.PopupTitleLabel = "集計単位";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "集計単位CD", "集計単位" };


                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.DetailIchiran.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["TSUKIGIME_KBN_NM"].Value = form.ReturnParams[1][0].Value.ToString();
                    }
                }

                if (this.form.DetailIchiran.Columns[this.form.DetailIchiran.CurrentCell.ColumnIndex].Name.Equals("KEIYAKU_KBN"))
                {

                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;
                    row = dt.NewRow();
                    if (Convert.ToString(this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["INPUT_KBN"].Value) == "2")
                    {
                        row["CD"] = "1";
                        row["VALUE"] = "定期";
                        dt.Rows.Add(row);
                    }
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "単価";
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "3";
                    row["VALUE"] = "回収のみ";
                    dt.Rows.Add(row);
                    dt.TableName = "契約区分";
                    form.table = dt;
                    form.PopupTitleLabel = "契約区分";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "契約区分CD", "契約区分" };

                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.DetailIchiran.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["KEIYAKU_KBN_NM"].Value = form.ReturnParams[1][0].Value.ToString();
                    }
                }
                if (this.form.DetailIchiran.Columns[this.form.DetailIchiran.CurrentCell.ColumnIndex].Name.Equals("DENPYOU_KBN_CD"))
                {

                    MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("CD", typeof(string));
                    dt.Columns.Add("VALUE", typeof(string));
                    dt.Columns[0].ReadOnly = true;
                    dt.Columns[1].ReadOnly = true;
                    DataRow row;
                    row = dt.NewRow();
                    row["CD"] = "1";
                    row["VALUE"] = "売上";
                    dt.Rows.Add(row);
                    row = dt.NewRow();
                    row["CD"] = "2";
                    row["VALUE"] = "支払";
                    dt.Rows.Add(row);
                    dt.TableName = "伝票区分";
                    form.table = dt;
                    form.PopupTitleLabel = "伝票区分";
                    form.PopupGetMasterField = "CD,VALUE";
                    form.PopupDataHeaderTitle = new string[] { "伝票区分CD", "伝票区分名" };

                    form.ShowDialog();
                    if (form.ReturnParams != null)
                    {
                        this.form.DetailIchiran.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = form.ReturnParams[1][0].Value.ToString();
                    }
                }
            }
        }

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>
        /// <param name="value">変化値</param>
        /// <returns>object</returns>
        private object ChgDBNullToValue(object obj, object value)
        {
            if (obj is DBNull)
            {
                return value;
            }
            else
            {
                return obj;
            }
        }

        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">対象</param>       
        /// <returns>object</returns>
        public string GetCellValue(DataGridViewCell obj)
        {
            if (obj.Value == null)
            {
                return string.Empty;
            }
            else
            {
                return obj.Value.ToString();
            }
        }
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        public int Search()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 実績番号変更後処理
        /// <summary>
        /// 実績番号変更後処理
        /// </summary>
        public void TeikiHaishaJisekiNumberValidated(out bool catchErr)
        {
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 定期配車実績番号に変更がなければ、処理を中断する
                if (!String.IsNullOrEmpty(this.form.bakTeikiHaishaJissekiNumber) && this.form.bakTeikiHaishaJissekiNumber == this.form.TEIKI_JISSEKI_NUMBER.Text)
                {
                    LogUtility.DebugMethodEnd(catchErr);
                    return;
                }

                // 定期実績番号を検索条件に設定する
                this.searchDto = new DTOClass();
                this.searchDto.TeikiJissekiNumber = this.form.TEIKI_JISSEKI_NUMBER.Text;

                // 定期実績入力情報を取得する
                this.searchResultEntry = teikiEntryDao.GetEntryData(this.searchDto);

                // レコードが存在しない
                if (this.searchResultEntry.Rows.Count == 0)
                {
                    //メッセージ
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E045");
                    this.form.TEIKI_JISSEKI_NUMBER.Focus();
                    //定期配車番号使用可
                    this.form.TEIKI_HAISHA_NUMBER.Enabled = true;
                    LogUtility.DebugMethodEnd(catchErr);
                    return;
                }

                // 実績番号設定
                this.teikiJisejiNumber = this.form.TEIKI_JISSEKI_NUMBER.Text;
                // 変更前の定期配車実績番号を保存
                this.form.bakTeikiHaishaJissekiNumber = this.form.TEIKI_JISSEKI_NUMBER.Text;
                // 変更前の定期配車番号を保存
                this.form.bakTeikiHaishaNumber = this.form.TEIKI_HAISHA_NUMBER.Text;

                // 権限チェック(配車・実績番号変更時に事前に修正・参照権限無しをチェック済みのためアラート無し)
                if (r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 処理モードを修正に変更
                    this.form.SetWindowType(WINDOW_TYPE.UPDATE_WINDOW_FLAG);
                    // 画面項目初期化【修正】モード
                    if (!this.WindowInitUpdate((BusinessBaseForm)this.form.Parent))
                    {
                        catchErr = true;
                        LogUtility.DebugMethodEnd(catchErr);
                        return;
                    }
                }
                else
                {
                    // 処理モードを参照に変更
                    this.form.SetWindowType(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                    // 画面項目初期化【参照】モード
                    this.WindowInitReference((BusinessBaseForm)this.form.Parent);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHaishaJisekiNumberValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
        }
        #endregion

        #region 配車番号変更後処理
        /// <summary>
        /// 配車番号変更後処理
        /// </summary>
        public void TeikiHaishaNumberValidated(out bool catchErr)
        {
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 定期配車番号に変更がなければ、処理を中断する
                if (this.form.bakTeikiHaishaNumber == this.form.TEIKI_HAISHA_NUMBER.Text)
                {
                    return;
                }

                var messageShowLogic = new MessageBoxShowLogic();

                if (!RenkeiCheck(this.form.TEIKI_HAISHA_NUMBER.Text))
                {
                    this.form.TEIKI_HAISHA_NUMBER.Focus();
                    return;
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false) &&
                    !r_framework.Authority.Manager.CheckAuthority("G289", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, false))
                {
                    messageShowLogic.MessageBoxShow("E158", "修正");
                    this.form.TEIKI_HAISHA_NUMBER.Focus();
                    return;
                }

                // 定期配車番号を検索条件に設定する
                this.searchDto = new DTOClass();
                this.searchDto.TeikiHaishaNumber = this.form.TEIKI_HAISHA_NUMBER.Text;

                DataTable ExistHaishaNumber = teikiEntryDao.ExistHaishaNumber(this.searchDto);
                // データが存在する場合
                if (ExistHaishaNumber.Rows.Count > 0)
                {
                    var result = messageShowLogic.MessageBoxShow("C060");
                    if (result == DialogResult.Yes)
                    {
                        // 定期配車番号を元に定期実績情報を取得する
                        this.searchResultEntry = teikiEntryDao.GetHaisyaJissekiEntryData(this.searchDto);
                        // データが存在しない場合
                        if (this.searchResultEntry.Rows.Count == 0)
                        {
                            // アラート表示し、フォーカス移動しない                
                            messageShowLogic.MessageBoxShow("E045");
                            this.form.TEIKI_HAISHA_NUMBER.Focus();
                            return;
                        }
                        // 実績番号設定
                        this.form.TEIKI_JISSEKI_NUMBER.Text = this.searchResultEntry.Rows[0]["TEIKI_JISSEKI_NUMBER"].ToString();

                        // 実績番号を元にデータ取得
                        TeikiHaishaJisekiNumberValidated(out catchErr);
                        if (catchErr) { return; }
                        // 20141015 koukouei 休動管理機能 start
                        if (!ChkSharyouWordClose(false))
                        {
                            string date = string.Format("作業日：{0}", Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd"));
                            messageShowLogic.MessageBoxShow("E208", "定期配車番号：", this.form.TEIKI_HAISHA_NUMBER.Text, "車輛", date);
                            this.form.SHARYOU_CD.Focus();
                            return;
                        }

                        if (!ChkUntenshaWordClose(false))
                        {
                            string date = string.Format("作業日：{0}", Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd"));
                            messageShowLogic.MessageBoxShow("E208", "定期配車番号：", this.form.TEIKI_HAISHA_NUMBER.Text, "運転者", date);
                            this.form.UNTENSHA_CD.Focus();
                            return;
                        }

                        foreach (DataGridViewRow row in this.form.NioroshiIchiran.Rows)
                        {
                            if (!ChkGenbaWordClose(row, false))
                            {
                                string date = string.Format("作業日：{0}", Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd"));
                                messageShowLogic.MessageBoxShow("E208", "定期配車番号：", this.form.TEIKI_HAISHA_NUMBER.Text, "荷降現場", date);
                                this.form.NioroshiIchiran.CurrentCell = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD];
                                this.form.NioroshiIchiran.Focus();
                                return;
                            }
                        }
                        // 20141015 koukouei 休動管理機能 end
                    }
                    else
                    {
                        this.form.TEIKI_HAISHA_NUMBER.Focus();
                        return;
                    }
                }
                else
                {
                    // 定期配車入力情報を取得する
                    this.searchResultEntry = teikiHaishaEntryDao.GetHaisyaEntryData(this.searchDto);
                    // データが存在しない場合
                    if (this.searchResultEntry.Rows.Count == 0)
                    {
                        // アラート表示し、フォーカス移動しない                
                        messageShowLogic.MessageBoxShow("E045");
                        this.form.TEIKI_HAISHA_NUMBER.Focus();
                        return;
                    }
                    // システムID、枝番を検索条件に設定する
                    searchDto.SystemId = long.Parse(this.searchResultEntry.Rows[0]["SYSTEM_ID"].ToString());
                    searchDto.Seq = int.Parse(this.searchResultEntry.Rows[0]["SEQ"].ToString());
                    // 定期配車明細情報を取得する
                    searchResultDetail = teikiHaishaDetailDao.GetHaisyaDetailData(this.searchDto);
                    // 定期配車荷降情報を取得する
                    searchResultNioroshi = teikiHaishaNioroshiDao.GetHaisyaNioroshiData(this.searchDto);

                    // 全コントロールを操作可能とする
                    this.AllControlLock(true);
                    // 画面項目初期化
                    if (!this.WindowInitNewMode((BusinessBaseForm)this.form.Parent)) { return; }
                    // 検索結果を画面に設定
                    this.SetDataForWindow();
                    // 入力された定期配車番号を保存
                    this.form.bakTeikiHaishaNumber = this.form.TEIKI_HAISHA_NUMBER.Text;
                    // 20141015 koukouei 休動管理機能 start
                    var msgLogic = new MessageBoxShowLogic();
                    if (!ChkSharyouWordClose(false))
                    {
                        string date = string.Format("作業日：{0}", Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd"));
                        msgLogic.MessageBoxShow("E208", "定期配車番号：", this.form.TEIKI_HAISHA_NUMBER.Text, "車輛", date);
                        this.form.SHARYOU_CD.Focus();
                        return;
                    }

                    if (!ChkUntenshaWordClose(false))
                    {
                        string date = string.Format("作業日：{0}", Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd"));
                        msgLogic.MessageBoxShow("E208", "定期配車番号：", this.form.TEIKI_HAISHA_NUMBER.Text, "運転者", date);
                        this.form.UNTENSHA_CD.Focus();
                        return;
                    }

                    foreach (DataGridViewRow row in this.form.NioroshiIchiran.Rows)
                    {
                        if (!ChkGenbaWordClose(row, false))
                        {
                            string date = string.Format("作業日：{0}", Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd"));
                            msgLogic.MessageBoxShow("E208", "定期配車番号：", this.form.TEIKI_HAISHA_NUMBER.Text, "荷降現場", date);
                            this.form.NioroshiIchiran.CurrentCell = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD];
                            this.form.NioroshiIchiran.Focus();
                            return;
                        }
                    }
                    // 20141015 koukouei 休動管理機能 end
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("TeikiHaishaNumberValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(catchErr);
            }
        }
        #endregion

        /// <summary>
        /// 運転者CDバリデート
        /// </summary>
        public bool UNTENSHA_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.UNTENSHA_NAME.Text = "";

                var untenShain = this.shainDao.GetAllValidData(new M_SHAIN()).FirstOrDefault(s => (bool)s.UNTEN_KBN && s.SHAIN_CD == this.form.UNTENSHA_CD.Text);
                if (untenShain == null)
                {
                    // エラーメッセージ
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "運転者");
                    this.isInputError = true;
                    this.form.UNTENSHA_CD.Focus();
                    return false;
                }

                if (!string.IsNullOrEmpty(this.form.HOJOIN_CD.Text) &&
                       this.form.UNTENSHA_CD.Text.Equals(this.form.HOJOIN_CD.Text))
                {
                    // エラーメッセージ
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E031", "運転者、補助員の指定");
                    this.isInputError = true;
                    this.form.UNTENSHA_CD.Focus();
                    return false;
                }
                // 20141015 koukouei 休動管理機能 start
                // 休動チェック
                else if (!this.ChkUntenshaWordClose(true))
                {
                    this.form.UNTENSHA_CD.Focus();
                    return false;
                }
                // 20141015 koukouei 休動管理機能 end

                this.form.UNTENSHA_NAME.Text = untenShain.SHAIN_NAME_RYAKU;                
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNTENSHA_CDValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return true;
        }

        /// <summary>
        /// 運搬業者CDバリデート
        /// </summary>
        public bool UNPAN_GYOUSHA_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.UNPAN_GYOUSHA_NAME.Text = "";
                this.form.SHARYOU_CD.Text = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                this.form.ClearBeforeText(this.form.SHARYOU_CD.Name);

                var unpanGyousha = this.gyoushaDao.GetAllValidData(new M_GYOUSHA()).FirstOrDefault(s => s.GYOUSHA_CD == this.form.UNPAN_GYOUSHA_CD.Text);
                if (unpanGyousha == null || !unpanGyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    // エラーメッセージ
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.form.UNPAN_GYOUSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.isInputError = true;
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                    return false;
                }

                SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                {
                    tekiyouDate = date;
                }
                if (unpanGyousha.TEKIYOU_BEGIN.IsNull && unpanGyousha.TEKIYOU_END.IsNull)
                {
                    // 名称セット
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
                }
                else if (unpanGyousha.TEKIYOU_BEGIN.IsNull && !unpanGyousha.TEKIYOU_END.IsNull
                    && tekiyouDate.CompareTo(unpanGyousha.TEKIYOU_END) <= 0)
                {
                    // 名称セット
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
                }
                else if (!unpanGyousha.TEKIYOU_BEGIN.IsNull && unpanGyousha.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(unpanGyousha.TEKIYOU_BEGIN) >= 0)
                {
                    // 名称セット
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
                }
                else if (!unpanGyousha.TEKIYOU_BEGIN.IsNull && !unpanGyousha.TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(unpanGyousha.TEKIYOU_BEGIN) >= 0
                        && tekiyouDate.CompareTo(unpanGyousha.TEKIYOU_END) <= 0)
                {
                    // 名称セット
                    this.form.UNPAN_GYOUSHA_NAME.Text = unpanGyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // エラーメッセージ
                    this.form.UNPAN_GYOUSHA_CD.IsInputErrorOccured = true;
                    this.form.UNPAN_GYOUSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.isInputError = true;
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CDValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                this.isInputError = true;
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return true;
        }

        #region Functionボタン 押下処理

        /// <summary>
        /// F9 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                this.isRegist = false;

                if (!errorFlag)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        // 定期実績入力テーブル登録
                        this.entitysT_TEIKI_ENTRY.DELETE_FLG = false;
                        this.teikiEntryDao.Insert(this.entitysT_TEIKI_ENTRY);

                        // 定期実績明細テーブル登録           
                        foreach (T_TEIKI_JISSEKI_DETAIL detail in this.entityDetailList)
                        {
                            this.teikiDetailDao.Insert(detail);
                        }
                        // 定期実績荷卸テーブル登録           
                        foreach (T_TEIKI_JISSEKI_NIOROSHI nioroshi in this.entityNioroshilList)
                        {
                            this.teikiNioroshiDao.Insert(nioroshi);
                        }

                        // トランザクション終了
                        tran.Commit();
                    }
                    this.isRegist = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                this.isRegist = false;

                if (!errorFlag)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        // データを変更したチェック
                        foreach (T_TEIKI_JISSEKI_DETAIL detail in this.entityDetailList)
                        {
                            if (!detail.UR_SH_NUMBER.IsNull)
                            {
                                T_TEIKI_JISSEKI_DETAIL[] arrayTjd = this.teikiDetailDao.GetTeikiJissekiDetailData(detail.UR_SH_NUMBER.ToString());
                                if (arrayTjd == null || arrayTjd.Length == 0)
                                {
                                    var messageShowLogic = new MessageBoxShowLogic();
                                    messageShowLogic.MessageBoxShow("E080");
                                    return;
                                }
                            }
                        }

                        // 定期実績入力テーブル更新（削除）
                        this.entitysT_TEIKI_ENTRY.DELETE_FLG = true;
                        this.teikiEntryDao.Update(this.entitysT_TEIKI_ENTRY);

                        // 定期実績入力テーブル登録（新しい枝番での追加）
                        this.entitysT_TEIKI_ENTRY.DELETE_FLG = false;
                        this.entitysT_TEIKI_ENTRY.SEQ = this.entitysT_TEIKI_ENTRY.SEQ + 1;
                        this.teikiEntryDao.Insert(this.entitysT_TEIKI_ENTRY);

                        // 定期実績明細テーブル登録（新しい枝番での追加）
                        foreach (T_TEIKI_JISSEKI_DETAIL detail in this.entityDetailList)
                        {
                            detail.SEQ = this.entitysT_TEIKI_ENTRY.SEQ;
                            this.teikiDetailDao.Insert(detail);
                        }


                        // 定期実績荷卸テーブル登録           
                        foreach (T_TEIKI_JISSEKI_NIOROSHI nioroshi in this.entityNioroshilList)
                        {
                            nioroshi.SEQ = this.entitysT_TEIKI_ENTRY.SEQ;
                            this.teikiNioroshiDao.Insert(nioroshi);
                        }

                        // トランザクション終了
                        tran.Commit();
                    }
                    this.isRegist = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("Update", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// F9 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.isRegist = false;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C026");
                if (result == DialogResult.Yes)
                {
                    // トランザクション開始
                    using (Transaction tran = new Transaction())
                    {
                        // 定期実績入力テーブル更新（削除）    
                        this.entitysT_TEIKI_ENTRY.DELETE_FLG = true;
                        this.teikiEntryDao.Update(this.entitysT_TEIKI_ENTRY);

                        /// 20141117 Houkakou 「更新日、登録日の見直し」　start
                        this.entitysT_TEIKI_ENTRY.DELETE_FLG = true;
                        this.entitysT_TEIKI_ENTRY.SEQ = this.entitysT_TEIKI_ENTRY.SEQ + 1;
                        this.teikiEntryDao.Insert(this.entitysT_TEIKI_ENTRY);
                        /// 20141117 Houkakou 「更新日、登録日の見直し」　end

                        foreach (T_TEIKI_JISSEKI_DETAIL detail in this.entityDetailList)
                        {
                            detail.SEQ = this.entitysT_TEIKI_ENTRY.SEQ;
                            this.teikiDetailDao.Insert(detail);
                        }

                        foreach (T_TEIKI_JISSEKI_NIOROSHI nioroshi in this.entityNioroshilList)
                        {
                            nioroshi.SEQ = this.entitysT_TEIKI_ENTRY.SEQ;
                            this.teikiNioroshiDao.Insert(nioroshi);
                        }

                        // トランザクション終了
                        tran.Commit();
                    }
                    this.isRegist = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E080", "");
                }
                else if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        /// <param name="isDleteFlg">削除フラグ</param>
        /// <returns></returns> 
        public bool CreateEntity(bool isDleteFlg, WINDOW_TYPE WindowType)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(isDleteFlg, WindowType);

                // 新規の場合、システムIDを採番する
                if (WINDOW_TYPE.NEW_WINDOW_FLAG == WindowType)
                {
                    this.form.SYSTEM_ID.Text = SaibanSystemId().ToString();
                    this.form.SEQ.Text = "1";
                    this.form.TEIKI_JISSEKI_NUMBER.Text = SaibanTeikiHaishaNumber().ToString();
                    this.teikiJisejiNumber = this.form.TEIKI_JISSEKI_NUMBER.Text;
                }

                #region 定期実績入力テーブル
                this.entitysT_TEIKI_ENTRY = new T_TEIKI_JISSEKI_ENTRY();
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.SYSTEM_ID);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.SEQ);
                this.entitysT_TEIKI_ENTRY.SetValue(this.headForm.KYOTEN_CD);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.TEIKI_JISSEKI_NUMBER);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.TEIKI_HAISHA_NUMBER);
                if (this.form.FURIKAE_HAISHA_KBN.Text == "2")
                {
                    // 振替配車「2.振替」時のみ、値を設定
                    this.entitysT_TEIKI_ENTRY.SetValue(this.form.DAY_CD);
                }
                //this.entitysT_TEIKI_ENTRY.SetValue(this.form.DENPYOU_DATE);
                //this.entitysT_TEIKI_ENTRY.SetValue(this.form.SAGYOU_DATE);
                //if (this.form.DENPYOU_DATE.Value != null)
                //{
                //    this.entitysT_TEIKI_ENTRY.DENPYOU_DATE = (DateTime)this.form.DENPYOU_DATE.Value;
                //}
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.FURIKAE_HAISHA_KBN);
                if (this.form.SAGYOU_DATE.Value != null)
                {
                    this.entitysT_TEIKI_ENTRY.SAGYOU_DATE = (DateTime)this.form.SAGYOU_DATE.Value;
                }

                this.entitysT_TEIKI_ENTRY.SetValue(this.form.txtWEATHER);

                this.entitysT_TEIKI_ENTRY.SetValue(this.form.COURSE_NAME_CD);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.SHARYOU_CD);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.SHASHU_CD);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.UNTENSHA_CD);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.UNPAN_GYOUSHA_CD);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.HOJOIN_CD);

                this.entitysT_TEIKI_ENTRY.SetValue(this.form.KIKO_METER);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.KIKO_HOUR);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.KIKO_MINUTE);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.SHUKKO_METER);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.SHUKKO_HOUR);
                this.entitysT_TEIKI_ENTRY.SetValue(this.form.SHUKKO_MINUTE);
                // TIME_STAMPを画面から取得、更新用エンティティに設定
                this.entitysT_TEIKI_ENTRY.TIME_STAMP = ConvertStrByte.StringToByte(this.form.TIME_STAMP_ENTRY.Text);

                // 更新者情報設定
                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.T_TEIKI_JISSEKI_ENTRY>(this.entitysT_TEIKI_ENTRY);
                dataBinderLogic.SetSystemProperty(this.entitysT_TEIKI_ENTRY, false);
                // 修正モードの場合、定期実績入力Entityの作成情報を設定
                /// 20141117 Houkakou 「更新日、登録日の見直し」　start
                if (this.form.WindowType.Equals(WINDOW_TYPE.UPDATE_WINDOW_FLAG) || this.form.WindowType.Equals(WINDOW_TYPE.DELETE_WINDOW_FLAG))
                /// 20141117 Houkakou 「更新日、登録日の見直し」　end
                {
                    // 作成者
                    this.entitysT_TEIKI_ENTRY.CREATE_USER = this.searchResultEntry.Rows[0]["CREATE_USER"].ToString();
                    // 作成日
                    this.entitysT_TEIKI_ENTRY.CREATE_DATE = DateTime.Parse(this.searchResultEntry.Rows[0]["CREATE_DATE"].ToString());
                    // 作成PC
                    this.entitysT_TEIKI_ENTRY.CREATE_PC = this.searchResultEntry.Rows[0]["CREATE_PC"].ToString();
                }
                #endregion

                #region 定期実績明細テーブル
                int rowNumber = 0;
                this.entityDetailList = new List<T_TEIKI_JISSEKI_DETAIL>();
                DataGridViewRow detailRow = null;

                int rowCount = 0;
                long currentDetailSystemId = -1;
                long maxDetailSystemId = 0;

                //DETAIL_SYSTEM_IDを行数分まとめて採番する
                //新規行ではなく、DETAIL_SYSTEM_IDが設定されていない行数を取得する
                rowCount = this.form.DetailIchiran.Rows.Cast<DataGridViewRow>().Where(row => !row.IsNewRow
                                                                                      && string.IsNullOrEmpty(row.Cells[ConstCls.DetailColName.DETAIL_SYSTEM_ID].FormattedValue.ToString())
                                                                                     ).Count();
                if (rowCount != 0)
                {
                    maxDetailSystemId = this.SaibanSystemId(rowCount);
                    currentDetailSystemId = maxDetailSystemId - rowCount;
                }

                for (int i = 0; i < this.form.DetailIchiran.Rows.Count - 1; i++)
                {
                    detailRow = this.form.DetailIchiran.Rows[i];
                    rowNumber = i + 1;

                    this.entitysT_TEIKI_DETAIL = new T_TEIKI_JISSEKI_DETAIL();
                    // システムID
                    this.entitysT_TEIKI_DETAIL.SetValue(this.form.SYSTEM_ID);
                    // 枝番
                    this.entitysT_TEIKI_DETAIL.SetValue(this.form.SEQ);
                    // 明細システムID
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.DETAIL_SYSTEM_ID].FormattedValue.ToString()))
                    {
                        // DB参照元がある明細は、そのキーをそのまま。
                        // this.entitysT_TEIKI_DETAIL.SetValue((ICustomControl)detailRow.Cells[ConstCls.DetailColName.DETAIL_SYSTEM_ID])
                        this.entitysT_TEIKI_DETAIL.DETAIL_SYSTEM_ID = SqlInt64.Parse(detailRow.Cells[ConstCls.DetailColName.DETAIL_SYSTEM_ID].FormattedValue.ToString());
                    }
                    else
                    {
                        // 追加した明細の場合、システムID採番クラスより１つ採番した戻り番号
                        this.form.DETAIL_SYSTEM_ID_HIDDEN.Text = currentDetailSystemId.ToString();
                        if (currentDetailSystemId <= maxDetailSystemId)
                        {
                            currentDetailSystemId++;
                        }
                        this.entitysT_TEIKI_DETAIL.SetValue(this.form.DETAIL_SYSTEM_ID_HIDDEN);
                    }

                    // 定期実績番号
                    this.entitysT_TEIKI_DETAIL.SetValue(this.form.TEIKI_JISSEKI_NUMBER);
                    // 行番号
                    this.entitysT_TEIKI_DETAIL.ROW_NUMBER = (SqlInt16)Int16.Parse(rowNumber.ToString());
                    // 入力区分
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.INPUT_KBN].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.INPUT_KBN = SqlInt16.Parse(detailRow.Cells[ConstCls.DetailColName.INPUT_KBN].FormattedValue.ToString());
                    }
                    // 回数
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.ROUND_NO].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.ROUND_NO = SqlInt32.Parse(detailRow.Cells[ConstCls.DetailColName.ROUND_NO].FormattedValue.ToString());
                    }
                    // 業者CD
                    this.entitysT_TEIKI_DETAIL.GYOUSHA_CD = detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString();
                    // 現場CD
                    this.entitysT_TEIKI_DETAIL.GENBA_CD = detailRow.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString();
                    // 品名CD
                    this.entitysT_TEIKI_DETAIL.HINMEI_CD = detailRow.Cells[ConstCls.DetailColName.HINMEI_CD].FormattedValue.ToString();
                    // 数量
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.SUURYOU].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.SUURYOU = SqlDecimal.Parse(detailRow.Cells[ConstCls.DetailColName.SUURYOU].FormattedValue.ToString().Replace(",","").Trim());
                    }
                    // 単位CD
                    if (detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value != null && !string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString().Trim()))
                    {
                        this.entitysT_TEIKI_DETAIL.UNIT_CD = SqlInt16.Parse(detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString());
                    }

                    // 実数
                    this.entitysT_TEIKI_DETAIL.ANBUN_FLG = this.ConvertToBool(detailRow.Cells["ANBUN_FLG"].Value);

                    //換算後数量
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.KANSAN_SUURYOU = SqlDecimal.Parse(detailRow.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].FormattedValue.ToString().Replace(",", "").Trim());
                    }
                    //換算後単位CD
                    if (detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value != null && !string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value.ToString().Trim()))
                    {
                        this.entitysT_TEIKI_DETAIL.KANSAN_UNIT_CD = SqlInt16.Parse(detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value.ToString());
                    }

                    //按分後数量                   
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.ANBUN_SUURYOU].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.ANBUN_SUURYOU = SqlDecimal.Parse(detailRow.Cells[ConstCls.DetailColName.ANBUN_SUURYOU].FormattedValue.ToString().Replace(",", "").Trim());
                    }
                    // 荷降No
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.NIOROSHI_NUMBER = SqlInt32.Parse(detailRow.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].FormattedValue.ToString());
                    }
                    // 収集時刻
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_HOUR].FormattedValue.ToString()) &&
                        !string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_MIN].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.SHUUSHUU_TIME = DateTime.Parse(detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_HOUR].FormattedValue.ToString() + ":" + detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_MIN].FormattedValue.ToString());
                    }
                    // 品名備考
                    this.entitysT_TEIKI_DETAIL.HINMEI_BIKOU = detailRow.Cells[ConstCls.DetailColName.HINMEI_BIKOU].FormattedValue.ToString();
                    // 収集備考
                    this.entitysT_TEIKI_DETAIL.KAISHUU_BIKOU = detailRow.Cells[ConstCls.DetailColName.KAISHUU_BIKOU].FormattedValue.ToString();
                    // 月極区分
                    if (!string.IsNullOrEmpty(detailRow.Cells["TSUKIGIME_KBN"].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.TSUKIGIME_KBN = SqlInt16.Parse(detailRow.Cells["TSUKIGIME_KBN"].FormattedValue.ToString());
                    }
                    // 契約区分
                    if (!string.IsNullOrEmpty(detailRow.Cells["KEIYAKU_KBN"].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.KEIYAKU_KBN = SqlInt16.Parse(detailRow.Cells["KEIYAKU_KBN"].FormattedValue.ToString());
                    }
                    // 伝票区分
                    if (!string.IsNullOrEmpty(detailRow.Cells["DENPYOU_KBN_CD_NM"].FormattedValue.ToString()))
                    {
                        if (detailRow.Cells["DENPYOU_KBN_CD_NM"].FormattedValue.Equals("売上"))
                        {
                            this.entitysT_TEIKI_DETAIL.DENPYOU_KBN_CD = 1;
                        }
                        else if (detailRow.Cells["DENPYOU_KBN_CD_NM"].FormattedValue.Equals("支払"))
                        {
                            this.entitysT_TEIKI_DETAIL.DENPYOU_KBN_CD = 2;
                        }
                    }
                    // 換算後単位モバイル出力フラグ
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.KANSAN_UNIT_MOBILE_OUTPUT_FLG = SqlBoolean.Parse(detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG].FormattedValue.ToString());
                    }

                    if (!string.IsNullOrEmpty(detailRow.Cells["UR_SH_NUMBER_NUMERIC"].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.UR_SH_NUMBER = SqlInt64.Parse(detailRow.Cells["UR_SH_NUMBER_NUMERIC"].FormattedValue.ToString());
                    }
                    // 確定
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.KAKUTEI_FLG].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_DETAIL.KAKUTEI_FLG = SqlBoolean.Parse(detailRow.Cells[ConstCls.DetailColName.KAKUTEI_FLG].FormattedValue.ToString());
                    }
                    else
                    {
                        this.entitysT_TEIKI_DETAIL.KAKUTEI_FLG = false;
                    }

                    entityDetailList.Add(this.entitysT_TEIKI_DETAIL);
                }
                #endregion

                #region 定期実績荷卸テーブル
                this.entityNioroshilList = new List<T_TEIKI_JISSEKI_NIOROSHI>();
                DataGridViewRow nioroshiRow = null;
                // 荷降業者CD
                string gyoushaCd = string.Empty;
                // 荷降現場CD
                string genbaCd = string.Empty;
                for (int j = 0; j < this.form.NioroshiIchiran.Rows.Count - 1; j++)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[j];
                    // 業者CD、現場CDを取得する
                    gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();
                    // 空行は登録対象外とする
                    if (string.IsNullOrEmpty(gyoushaCd) && string.IsNullOrEmpty(genbaCd))
                    {
                        continue;
                    }
                    rowNumber = j + 1;
                    this.entitysT_TEIKI_NIOROSHI = new T_TEIKI_JISSEKI_NIOROSHI();
                    // システムID
                    this.entitysT_TEIKI_NIOROSHI.SetValue(this.form.SYSTEM_ID);
                    // 枝番
                    this.entitysT_TEIKI_NIOROSHI.SetValue(this.form.SEQ);
                    // 荷降No
                    this.entitysT_TEIKI_NIOROSHI.NIOROSHI_NUMBER = SqlInt32.Parse(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].FormattedValue.ToString());
                    // 定期実績番号
                    this.entitysT_TEIKI_NIOROSHI.SetValue(this.form.TEIKI_JISSEKI_NUMBER);
                    // 行番号
                    this.entitysT_TEIKI_NIOROSHI.ROW_NUMBER = (SqlInt16)Int16.Parse(rowNumber.ToString());
                    // 業者CD
                    this.entitysT_TEIKI_NIOROSHI.NIOROSHI_GYOUSHA_CD = gyoushaCd;
                    // 現場CD
                    this.entitysT_TEIKI_NIOROSHI.NIOROSHI_GENBA_CD = genbaCd;
                    // 荷降量
                    if (!string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_RYOU].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_NIOROSHI.NIOROSHI_RYOU = SqlDecimal.Parse(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_RYOU].FormattedValue.ToString().Replace(",", "").Trim());
                    }
                    // 搬入時刻
                    if (!string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_HOUR].FormattedValue.ToString()) &&
                        !string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_MIN].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_NIOROSHI.HANNYUU_DATE = DateTime.Parse(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_HOUR].FormattedValue.ToString() + ":" + nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_MIN].FormattedValue.ToString());
                    }
                    // タイムスタンプ
                    if (!string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.TIME_STAMP_NIOROSHI].FormattedValue.ToString()))
                    {
                        this.entitysT_TEIKI_NIOROSHI.TIME_STAMP = (byte[])nioroshiRow.Cells[ConstCls.NioroshiColName.TIME_STAMP_NIOROSHI].Value;
                    }
                    entityNioroshilList.Add(this.entitysT_TEIKI_NIOROSHI);
                }
                #endregion

            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// システムID採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public int SaibanSystemId(int addCount = 0)
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 戻り値を初期化
                int returnInt = 1;

                // 処理区分：130（定期実績）
                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_JISSEKI;

                // 処理区分をもとに削除されていないシステムID採番のデータを取得する
                var updateEntity = this.numberSystemDao.GetNumberSystemData(entity);
                // システムIDの最大値+1を取得する
                returnInt = this.numberSystemDao.GetMaxPlusKey(entity);
                if (addCount != 0)
                {
                    //指定数分追加して先行でSYSTEM_IDの採番を行う
                    returnInt = returnInt + addCount;
                }

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_JISSEKI;
                    updateEntity.CURRENT_NUMBER = returnInt;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberSystemDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnInt;
                    this.numberSystemDao.Update(updateEntity);
                }
                return returnInt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanSystemId", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 定期実績番号採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        public int SaibanTeikiHaishaNumber()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 戻り値を初期化
                int returnInt = -1;

                // 処理区分：130（定期実績）
                var entity = new S_NUMBER_DENSHU();
                entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_JISSEKI;

                // 処理区分をもとに削除されていない伝種採番のデータを取得する
                var updateEntity = this.numberDenshuDao.GetNumberDenshuData(entity);
                // 伝種連番の最大値+1を取得する
                returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_DENSHU();
                    updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_JISSEKI;
                    updateEntity.CURRENT_NUMBER = returnInt;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_DENSHU>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberDenshuDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = returnInt;
                    this.numberDenshuDao.Update(updateEntity);
                }
                return returnInt;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SaibanSystemId", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 入力チェック
        /// <summary>
        /// 車輌CD（＆車種CD）存在チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckSharyouExsit(out bool catchErr)
        {
            bool result = true;
            catchErr = false;

            try
            {
                LogUtility.DebugMethodStart();

                // 車輌CDが入力されていない場合、チェックしない
                if (string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    return result;
                }

                // 検索条件設定
                M_SHARYOU entity = new M_SHARYOU();
                // 業者CD
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    entity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                }
                // 車輌CD
                entity.SHARYOU_CD = this.form.SHARYOU_CD.Text;
                // 車種CD
                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    entity.SHASYU_CD = this.form.SHASHU_CD.Text;
                }

                // [運搬業者CD,車輌CD,車種CD]でM_SHARYOUを検索する
                var returnEntitys = sharyouDao.GetAllValidData(entity);
                // レコードが存在しない場合、エラー
                if (returnEntitys.Length == 0)
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSharyouExsit", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                result = false;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }

        /// <summary>
        /// 作業時間入力チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckSagyouTime()
        {
            bool result = false;
            try
            {
                var msgLogic = new MessageBoxShowLogic();

                if (!string.IsNullOrEmpty(this.form.SHUKKO_HOUR.Text) && string.IsNullOrEmpty(this.form.SHUKKO_MINUTE.Text))
                {
                    msgLogic.MessageBoxShow("E148", "出庫時間");
                    this.form.SHUKKO_MINUTE.IsInputErrorOccured = true;
                    this.form.groupBox1.Focus();
                    this.form.SHUKKO_MINUTE.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.SHUKKO_HOUR.Text) && !string.IsNullOrEmpty(this.form.SHUKKO_MINUTE.Text))
                {
                    msgLogic.MessageBoxShow("E148", "出庫時間");
                    this.form.SHUKKO_HOUR.IsInputErrorOccured = true;
                    this.form.groupBox1.Focus();
                    this.form.SHUKKO_HOUR.Focus();
                }
                else if (!string.IsNullOrEmpty(this.form.KIKO_HOUR.Text) && string.IsNullOrEmpty(this.form.KIKO_MINUTE.Text))
                {
                    msgLogic.MessageBoxShow("E148", "帰庫時間");
                    this.form.KIKO_MINUTE.IsInputErrorOccured = true;
                    this.form.groupBox2.Focus();
                    this.form.KIKO_MINUTE.Focus();
                }
                else if (string.IsNullOrEmpty(this.form.KIKO_HOUR.Text) && !string.IsNullOrEmpty(this.form.KIKO_MINUTE.Text))
                {
                    msgLogic.MessageBoxShow("E148", "帰庫時間");
                    this.form.KIKO_HOUR.IsInputErrorOccured = true;
                    this.form.groupBox2.Focus();
                    this.form.KIKO_HOUR.Focus();
                }
                else
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckSagyouTime", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 明細部の入力チェックを行う
        /// </summary>
        /// <returns></returns>
        internal bool IsInputCheckOK()
        {
            bool result = true;

            try
            {
                LogUtility.DebugMethodStart();

                // 荷降業者CD
                string gyoushaCd = string.Empty;
                // 荷降現場CD
                string genbaCd = string.Empty;
                // 荷降明細部のレコード
                DataGridViewRow nioroshiRow = null;
                //品名明細部のレコード
                DataGridViewRow detailRow = null;
                bool nioroshiNumberErrorFlag = false;
                bool isExsitFlag = false;
                // 荷降No
                string nioroshiNumber = string.Empty;

                #region Detail-Detail-1部（荷降明細部）荷降業者入力チェック
                bool gyoushaCdErrorFlag = false;
                for (int i = 0; i < this.form.NioroshiIchiran.RowCount - 1; i++)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[i];

                    // 業者CD、現場CDを取得する
                    gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();

                    // 荷降現場CDに入力が有り、当該項目が入力されていない場合
                    if (!string.IsNullOrEmpty(genbaCd) && string.IsNullOrEmpty(gyoushaCd))
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], true);
                        gyoushaCdErrorFlag = true;
                    }
                }

                if (gyoushaCdErrorFlag)
                {
                    // アラート表示 TODO
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E012", "荷降業者");
                    result = false;
                    return result;
                }
                #endregion

                #region Detail-Detail-1部（荷降明細部）荷降現場入力チェック
                bool genbaCdErrorFlag = false;

                for (int i = 0; i < this.form.NioroshiIchiran.RowCount - 1; i++)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[i];

                    // 業者CD、現場CDを取得する
                    gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();

                    // 荷降業者CDに入力が有り、当該項目が入力されていない場合
                    if (!string.IsNullOrEmpty(gyoushaCd) && string.IsNullOrEmpty(genbaCd))
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], true);
                        genbaCdErrorFlag = true;
                    }
                }

                if (genbaCdErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E012", "荷降現場");
                    result = false;
                    return result;
                }
                #endregion

                #region Detail-Detail-1部（荷降明細部）搬入時間入力チェック

                bool hannyuuTimeErrorFlag = false;
                for (int i = 0; i < this.form.NioroshiIchiran.RowCount - 1; i++)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[i];
                    if (!string.IsNullOrEmpty(this.GetCellValue(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_HOUR])) &&
                        string.IsNullOrEmpty(this.GetCellValue(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_MIN])))
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_MIN], true);
                        hannyuuTimeErrorFlag = true;
                    }
                    else if (string.IsNullOrEmpty(this.GetCellValue(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_HOUR])) &&
                        !string.IsNullOrEmpty(this.GetCellValue(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_MIN])))
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_HOUR], true);
                        hannyuuTimeErrorFlag = true;
                    }
                }

                if (hannyuuTimeErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E148", "搬入時間");
                    result = false;
                    return result;
                }

                #endregion Detail-Detail-1部（荷降明細部）搬入時間入力チェック

                #region Detail-Detail-1部（荷降明細部）入力行の連続性チェック
                bool brankErrorFlag = false;
                // 荷降業者CD
                gyoushaCd = string.Empty;
                // 荷降現場CD
                genbaCd = string.Empty;

                for (int i = 0; i < this.form.NioroshiIchiran.RowCount - 1; i++)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[i];

                    // [荷降業者CD,荷降現場CD]に入力有る行か上から連続していない場合
                    if (i > 0 && string.IsNullOrEmpty(genbaCd) && string.IsNullOrEmpty(gyoushaCd))
                    {
                        if (!string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString())
                            || !string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString()))
                        {
                            // エラー項目背景色は赤色に設定
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.form.NioroshiIchiran.Rows[i - 1].Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.form.NioroshiIchiran.Rows[i - 1].Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], true);
                            brankErrorFlag = true;
                        }
                    }
                    // 業者CD、現場CDを取得する
                    gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();
                }

                if (brankErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E012", "空白行を空けずに荷降業者CD,荷降現場CD");
                    result = false;
                    return result;
                }
                #endregion
                
                bool IsDBExsitErrorFlag = false;

                #region Detail-Detail-1部（荷降明細部）最後行の入力チェック
                // 荷降業者CD
                gyoushaCd = string.Empty;
                // 荷降現場CD
                genbaCd = string.Empty;
                if (this.form.NioroshiIchiran.RowCount > 1)
                {
                    nioroshiRow = this.form.NioroshiIchiran.Rows[this.form.NioroshiIchiran.RowCount - 2];
                    // 業者CD、現場CDを取得する
                    gyoushaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                    genbaCd = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();
                    // [荷降業者CD,荷降現場CD]に入力有る行か上から連続していない場合
                    if (string.IsNullOrEmpty(genbaCd) && string.IsNullOrEmpty(gyoushaCd))
                    {

                        if (!string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_RYOU].FormattedValue.ToString())
                            || !string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_HOUR].FormattedValue.ToString())
                            || !string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.HANNYUU_MIN].FormattedValue.ToString()))
                        {
                            // エラー項目背景色は赤色に設定
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.form.NioroshiIchiran.Rows[this.form.NioroshiIchiran.RowCount - 2].Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(this.form.NioroshiIchiran.Rows[this.form.NioroshiIchiran.RowCount - 2].Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], true);
                            // アラート表示
                            var messageShowLogic = new MessageBoxShowLogic();
                            messageShowLogic.MessageBoxShow("E012", "空白行を空けずに荷降業者CD,荷降現場CD");
                            result = false;
                            return result;
                        }
                    }
                }
                #endregion

                #region Detail-Detail-2部（品名明細部）荷降Noの整合性チェック

                nioroshiNumberErrorFlag = false;
                isExsitFlag = false;
                // 荷降No
                nioroshiNumber = string.Empty;

                detailRow = null;
                for (int j = 0; j < this.form.DetailIchiran.RowCount; j++)
                {
                    detailRow = this.form.DetailIchiran.Rows[j];
                    isExsitFlag = false;

                    // 荷降Noを取得する
                    nioroshiNumber = detailRow.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].FormattedValue.ToString();
                    // ブランクの場合、チェックしない
                    if (string.IsNullOrEmpty(nioroshiNumber))
                    {
                        continue;
                    }

                    // 荷降明細部の荷降Noと比較
                    for (int k = 0; k < this.form.NioroshiIchiran.RowCount - 1; k++)
                    {
                        nioroshiRow = this.form.NioroshiIchiran.Rows[k];

                        // 荷降明細部で荷降業者、荷降現場があるのものは比較対象にする
                        if (!string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString())
                            || !string.IsNullOrEmpty(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString()))
                        {
                            // 荷降明細部に存在する場合
                            if (nioroshiNumber.Equals(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].FormattedValue.ToString()))
                            {
                                isExsitFlag = true;
                                break;
                            }
                        }
                    }

                    // 荷降明細部に存在しない場合
                    if (!isExsitFlag)
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL], true);
                        nioroshiNumberErrorFlag = true;
                    }
                }

                if (nioroshiNumberErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E062", "荷降明細の荷降No");
                    result = false;
                    return result;
                }
                #endregion

                #region Detail-Detail-2部（品名明細部）荷降No毎の数量と按分数量の合計を荷降量の整合性チェック
                // 荷降No
                nioroshiNumber = string.Empty;

                decimal iAllAnbunSuurou = 0;
                decimal iNioroshiRyou = 0;
                nioroshiRow = null;
                // 荷降明細部
                for (int xk = 0; xk < this.form.NioroshiIchiran.RowCount - 1; xk++)
                {
                    // 行データを取得する
                    nioroshiRow = this.form.NioroshiIchiran.Rows[xk];
                    // 荷降Noを取得する
                    nioroshiNumber = nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].FormattedValue.ToString();
                    //荷降量を取得する
                    iNioroshiRyou = string.IsNullOrEmpty(GetCellValue(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_RYOU])) ? 0 : decimal.Parse(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_RYOU].FormattedValue.ToString());
                    isExsitFlag = false;
                    //品名明細部取得
                    detailRow = null;
                    iAllAnbunSuurou = 0;

                    for (int j = 0; j < this.form.DetailIchiran.RowCount; j++)
                    {
                        detailRow = this.form.DetailIchiran.Rows[j];
                        // 背景色をクリア
                        //荷降No
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL], false);
                        //按分後数量
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.ANBUN_SUURYOU], false);

                        // 品名明細部に同じかつ按分後数量に値がある場合
                        if (nioroshiNumber.Equals(detailRow.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].FormattedValue.ToString()) &&
                            !string.IsNullOrEmpty(this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.ANBUN_SUURYOU])))
                        {
                            isExsitFlag = true;

                            //按分後数量合計を取得する
                            iAllAnbunSuurou = iAllAnbunSuurou + (string.IsNullOrEmpty(this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.ANBUN_SUURYOU])) ? 0 : decimal.Parse(detailRow.Cells[ConstCls.DetailColName.ANBUN_SUURYOU].FormattedValue.ToString()));

                        }
                    }

                    //荷降No毎の数量と按分数量の合計を荷降量と差異がある場合
                    if (isExsitFlag && iNioroshiRyou != iAllAnbunSuurou)
                    {
                        // エラー項目背景色は赤色に設定
                        for (int j = 0; j < this.form.DetailIchiran.RowCount; j++)
                        {
                            detailRow = this.form.DetailIchiran.Rows[j];

                            // 品名明細部に同じ場合
                            if (nioroshiNumber.Equals(detailRow.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].FormattedValue.ToString()))
                            {
                                //荷降No
                                ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL], true);

                                //按分後数量
                                ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.ANBUN_SUURYOU], true);
                            }
                        }
                        // アラート表示
                        var messageShowLogic = new MessageBoxShowLogic();
                        messageShowLogic.MessageBoxShow("E108", "品名明細部の荷降No[" + nioroshiNumber + "]の荷降量[" + iNioroshiRyou + "]と按分後数量の合計[" + iAllAnbunSuurou + "]です。");
                        result = false;
                        return result;
                    }

                }

                #endregion

                #region Detail-Detail-2部（品名明細部）重複チェック回数、業者CD、現場CD、品名CD、単位CDの組み合わせで重複する行がある場合。
                // 重複チェック用リストを作成
                Hashtable hashData = new Hashtable();
                Hashtable hashErrData = new Hashtable();
                for (int i = 0; i < this.form.DetailIchiran.RowCount; i++)
                {
                    //品名明細部取得
                    detailRow = null;
                    detailRow = this.form.DetailIchiran.Rows[i];

                    // 背景色をクリア
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.ROUND_NO], false);
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD], false);
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.GENBA_CD], false);
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.HINMEI_CD], false);
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.UNIT_CD], false);

                    // [回数,業者CD,現場CD,品名CD,単位CD]の組み合わせ
                    if (!string.IsNullOrEmpty(GetCellValue(detailRow.Cells[ConstCls.DetailColName.ROUND_NO]))
                        && !string.IsNullOrEmpty(GetCellValue(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD]))
                        && !string.IsNullOrEmpty(GetCellValue(detailRow.Cells[ConstCls.DetailColName.GENBA_CD]))
                       && !string.IsNullOrEmpty(GetCellValue(detailRow.Cells[ConstCls.DetailColName.HINMEI_CD]))
                         && !string.IsNullOrEmpty(GetCellValue(detailRow.Cells[ConstCls.DetailColName.UNIT_CD])))
                    {
                        string key = detailRow.Cells[ConstCls.DetailColName.ROUND_NO].FormattedValue.ToString()
                                        + detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString()
                                        + detailRow.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString()
                                        + detailRow.Cells[ConstCls.DetailColName.HINMEI_CD].FormattedValue.ToString()
                                        + detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString();
                        if (hashData.Contains(key))
                        {
                            if (!hashErrData.Contains(key))
                            {
                                // [回数,業者CD,現場CD,品名CD,単位CD]の組み合わせが同じ行が有る場合アラート表示                     
                                hashErrData.Add(key, detailRow);
                            }
                        }
                        else
                        {
                            hashData.Add(key, detailRow);
                        }
                    }
                }

                if (hashErrData.Count > 0)
                {
                    foreach (string errDadaKey in hashErrData.Keys)
                    {
                        DataGridViewRow detailErrRow = (DataGridViewRow)hashErrData[errDadaKey];
                        for (int i = 0; i < this.form.DetailIchiran.RowCount; i++)
                        {
                            //品名明細部取得
                            detailRow = null;
                            detailRow = this.form.DetailIchiran.Rows[i];

                            if (!string.IsNullOrEmpty(GetCellValue(detailRow.Cells[ConstCls.DetailColName.ROUND_NO]))
                                && !string.IsNullOrEmpty(GetCellValue(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD]))
                                && !string.IsNullOrEmpty(GetCellValue(detailRow.Cells[ConstCls.DetailColName.GENBA_CD]))
                               && !string.IsNullOrEmpty(GetCellValue(detailRow.Cells[ConstCls.DetailColName.HINMEI_CD]))
                                 && !string.IsNullOrEmpty(GetCellValue(detailRow.Cells[ConstCls.DetailColName.UNIT_CD])))
                            {   // No.2985
                                // [回数,業者CD,現場CD,品名CD,単位CD]の組み合わせ
                                string key = detailRow.Cells[ConstCls.DetailColName.ROUND_NO].FormattedValue.ToString()
                                                + detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString()
                                                + detailRow.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString()
                                                + detailRow.Cells[ConstCls.DetailColName.HINMEI_CD].FormattedValue.ToString()
                                                + detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString();
                                if (key.Equals(errDadaKey))
                                {
                                    // エラー項目背景色は赤色に設定                     
                                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.ROUND_NO], true);
                                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD], true);
                                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.GENBA_CD], true);
                                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.HINMEI_CD], true);
                                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.UNIT_CD], true);
                                }
                            }   // No.2985
                        }
                    }
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E031", "回数、業者CD、現場CD、品名CD、単位CDの組み合わせ");
                    result = false;
                    return result;
                }
                #endregion

                #region Detail-Detail-2部（品名明細部）必須チェック
                IsDBExsitErrorFlag = false;

                for (int i = 0; i < this.form.DetailIchiran.RowCount - 1; i++)
                {
                    detailRow = this.form.DetailIchiran.Rows[i];
                    // 背景色をクリア
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["TSUKIGIME_KBN"], false);


                    // 集計単位を取得する
                    string strTSUKIGIME_KBN = detailRow.Cells["TSUKIGIME_KBN"].FormattedValue.ToString();
                    string strKEIYAKU_KBN = detailRow.Cells["KEIYAKU_KBN"].FormattedValue.ToString();


                    if (strKEIYAKU_KBN.Equals("2") && string.IsNullOrEmpty(strTSUKIGIME_KBN))
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["TSUKIGIME_KBN"], true);
                        IsDBExsitErrorFlag = true;
                    }
                }

                if (IsDBExsitErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E001", "契約区分の値が「2」の時は、集計単位");
                    result = false;
                    return result;
                }
                #endregion

                #region Detail-Detail-2部（品名明細部）単位と按分単位kgチェック
                IsDBExsitErrorFlag = false;

                for (int i = 0; i < this.form.DetailIchiran.RowCount - 1; i++)
                {
                    detailRow = this.form.DetailIchiran.Rows[i];
                    // 背景色をクリア
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.UNIT_CD], false);
                    ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD], false);


                    // 単位を取得する
                    string strUNIT_CD = detailRow.Cells[ConstCls.DetailColName.UNIT_CD].FormattedValue.ToString();
                    string strKANSAN_UNIT_CD = detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].FormattedValue.ToString();


                    if (strUNIT_CD.Equals("kg") || strKANSAN_UNIT_CD.Equals("kg"))
                    {
                    }
                    else
                    {
                        // エラー項目背景色は赤色に設定
                        if (!strUNIT_CD.Equals("kg"))
                        {
                            ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.UNIT_CD], true);
                        }
                        if (!strKANSAN_UNIT_CD.Equals("kg"))
                        {
                            ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD], true);
                        }
                        IsDBExsitErrorFlag = true;
                    }
                }

                if (IsDBExsitErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E145");
                    result = false;
                    return result;
                }
                #endregion


                #region Detail-Detail-2部（品名明細部）収集時間入力チェック

                bool shuushuuTimeErrorFlag = false;
                for (int i = 0; i < this.form.DetailIchiran.RowCount - 1; i++)
                {
                    detailRow = this.form.DetailIchiran.Rows[i];
                    if (!string.IsNullOrEmpty(this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_HOUR])) &&
                       string.IsNullOrEmpty(this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_MIN])))
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_MIN], true);
                        shuushuuTimeErrorFlag = true;
                    }
                    else if (string.IsNullOrEmpty(this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_HOUR])) &&
                       !string.IsNullOrEmpty(this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_MIN])))
                    {
                        // エラー項目背景色は赤色に設定
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_HOUR], true);
                        shuushuuTimeErrorFlag = true;
                    }
                }

                if (shuushuuTimeErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E148", "収集時間");
                    result = false;
                    return result;
                }

                #endregion Detail-Detail-2部（品名明細部）収集時間入力チェック

                #region Detail-Detail-2部（品名明細部）単位と換算後単位の重複チェック

                bool duplicatedUnitErrorFlag = false;
                for (int i = 0; i < this.form.DetailIchiran.RowCount - 1; i++)
                {
                    detailRow = this.form.DetailIchiran.Rows[i];

                    string unitCd = this.GetCellValue(detailRow.Cells["UNIT_CD"]);
                    string kansanUnitCd = this.GetCellValue(detailRow.Cells["KANSAN_UNIT_CD"]);

                    if (string.IsNullOrEmpty(unitCd) && string.IsNullOrEmpty(kansanUnitCd))
                    {
                        continue;
                    }

                    if (!string.IsNullOrEmpty(unitCd))
                    {
                        unitCd = unitCd.PadLeft(2, '0');
                    }
                    if (!string.IsNullOrEmpty(kansanUnitCd))
                    {
                        kansanUnitCd = kansanUnitCd.PadLeft(2, '0');
                    }

                    if (unitCd.Equals(kansanUnitCd))
                    {
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["UNIT_CD"], true);
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["KANSAN_UNIT_CD"], true);
                        duplicatedUnitErrorFlag = true;
                    }

                    if (ContainsDuplicatedUnitCd(detailRow))
                    {
                        duplicatedUnitErrorFlag = true;
                    }
                }

                if (duplicatedUnitErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E031", "同一の回数、業者、現場、品名で単位又は換算後単位");
                    result = false;
                    return result;
                }

                #endregion Detail-Detail-2部（品名明細部）単位と換算後単位の重複チェック

            }
            catch (Exception ex)
            {
                LogUtility.Error("IsInputCheckOK", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
            return result;
        }

        /// <summary>
        /// 単位、換算後単位の重複チェック
        /// </summary>
        /// <param name="currentRow"></param>
        /// <returns>true:エラー有, false:エラー無</returns>
        private bool ContainsDuplicatedUnitCd(DataGridViewRow currentRow)
        {
            string currentRoundNo = this.GetCellValue(currentRow.Cells["ROUND_NO"]);
            string currentGyoshaCd = this.GetCellValue(currentRow.Cells["GYOUSHA_CD"]);
            string currentGenbaCd = this.GetCellValue(currentRow.Cells["GENBA_CD"]);
            string currentHinmeiCd = this.GetCellValue(currentRow.Cells["HINMEI_CD"]);
            string currentUnitCd = this.GetCellValue(currentRow.Cells["UNIT_CD"]);
            string currentKansanUnitCd = this.GetCellValue(currentRow.Cells["KANSAN_UNIT_CD"]);

            // 回数,業者,現場,品名が空の場合は未チェック
            if (string.IsNullOrEmpty(currentRoundNo)
                || string.IsNullOrEmpty(currentGyoshaCd)
                || string.IsNullOrEmpty(currentGenbaCd)
                || string.IsNullOrEmpty(currentHinmeiCd)
                || (string.IsNullOrEmpty(currentUnitCd) && string.IsNullOrEmpty(currentKansanUnitCd)))
            {
                return false;
            }

            foreach (DataGridViewRow grdRow in this.form.DetailIchiran.Rows)
            {
                if (grdRow.Index == this.form.DetailIchiran.Rows.Count - 1)
                {
                    return false;
                }

                // 同列の場合は未チェック
                if (grdRow.Index == currentRow.Index)
                {
                    continue;
                }

                string roundNo = this.GetCellValue(grdRow.Cells["ROUND_NO"]);
                string gyoshaCd = this.GetCellValue(grdRow.Cells["GYOUSHA_CD"]);
                string genbaCd = this.GetCellValue(grdRow.Cells["GENBA_CD"]);
                string hinmeiCd = this.GetCellValue(grdRow.Cells["HINMEI_CD"]);
                string unitCd = this.GetCellValue(grdRow.Cells["UNIT_CD"]);
                string kansanUnitCd = this.GetCellValue(grdRow.Cells["KANSAN_UNIT_CD"]);

                if (string.IsNullOrEmpty(roundNo) || string.IsNullOrEmpty(gyoshaCd) || string.IsNullOrEmpty(genbaCd) || string.IsNullOrEmpty(hinmeiCd))
                {
                    continue;
                }

                if (roundNo.Equals(currentRoundNo) && gyoshaCd.Equals(currentGyoshaCd) && genbaCd.Equals(currentGenbaCd) && hinmeiCd.Equals(currentHinmeiCd))
                {
                    var messageShowLogic = new MessageBoxShowLogic();
                    if (!string.IsNullOrEmpty(currentUnitCd))
                    {
                        if (currentUnitCd.Equals(unitCd))
                        {
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["UNIT_CD"], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(currentRow.Cells["UNIT_CD"], true);
                            return true;
                        }
                        else if (currentUnitCd.Equals(kansanUnitCd))
                        {
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["KANSAN_UNIT_CD"], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(currentRow.Cells["UNIT_CD"], true);
                            return true;
                        }
                    }

                    if (!string.IsNullOrEmpty(currentKansanUnitCd))
                    {
                        if (currentKansanUnitCd.Equals(unitCd))
                        {
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["UNIT_CD"], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(currentRow.Cells["KANSAN_UNIT_CD"], true);
                            return true;
                        }
                        else if (currentKansanUnitCd.Equals(kansanUnitCd))
                        {
                            ControlUtility.SetInputErrorOccuredForDgvCell(grdRow.Cells["KANSAN_UNIT_CD"], true);
                            ControlUtility.SetInputErrorOccuredForDgvCell(currentRow.Cells["KANSAN_UNIT_CD"], true);
                            return true;
                        }
                    }
                }
            }

            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="objFrom"></param>
        /// <param name="objTo"></param>
        /// <returns></returns>
        public bool IntegerFromToCheck(object objFrom, object objTo)
        {
            // アラート表示
            var messageShowLogic = new MessageBoxShowLogic();
            bool result = true;
            try
            {
                CustomNumericTextBox2 From = (CustomNumericTextBox2)objFrom;
                CustomNumericTextBox2 To = (CustomNumericTextBox2)objTo;
                //エラークリア
                if (From.IsInputErrorOccured)
                {
                    From.IsInputErrorOccured = false;
                }
                if (To.IsInputErrorOccured)
                {
                    To.IsInputErrorOccured = false;
                }

                if (!string.IsNullOrEmpty(From.Text) && !string.IsNullOrEmpty(To.Text))
                {
                    if (decimal.Parse(From.Text.Replace(",", "").Trim()) > decimal.Parse(To.Text.Replace(",", "").Trim()))
                    {
                        From.IsInputErrorOccured = true;
                        To.IsInputErrorOccured = true;
                        string[] msgS = new string[2];
                        msgS[0] = "出庫時のメーター";
                        msgS[1] = "帰庫時のメーター";
                        messageShowLogic.MessageBoxShow("E032", msgS);
                        From.Focus();
                        result = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IntegerFromToCheck", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                result = false;
            }
            return result;
        }

        #endregion

        #region 前の番号を取得
        /// <summary>
        /// 前の番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">番号</param>
        /// <param name="kyoten">拠点</param>
        /// <returns>前の番号</returns>
        internal String GetPreviousNumber(String tableName, String fieldName, String numberValue, String kyoten, out bool catchErr)
        {
            String returnVal = string.Empty;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue, kyoten);

                // SQL文作成            
                string selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                selectStr += " WHERE DELETE_FLG = 0 ";
                if (!String.IsNullOrEmpty(numberValue))
                {
                    selectStr += "   AND " + fieldName + " < " + long.Parse(numberValue);
                }
                if (!String.IsNullOrEmpty(kyoten))
                {
                    selectStr += "   AND KYOTEN_CD = " + kyoten;
                }

                // データ取得
                DataTable dt = this.teikiHaishaEntryDao.GetDateForStringSql(selectStr);
                // 取得できない場合、配車番号の最大値を取得する
                if (!String.IsNullOrEmpty(numberValue) && Convert.ToString(dt.Rows[0]["MAX_NUMBER"]) == "")
                {
                    selectStr = "SELECT MAX(" + fieldName + ") AS MAX_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    if (!String.IsNullOrEmpty(kyoten))
                    {
                        selectStr += "   AND KYOTEN_CD = " + kyoten;
                    }
                    // データ取得
                    dt = this.teikiEntryDao.GetDateForStringSql(selectStr);
                }

                // 前の配車番号を戻す
                returnVal = Convert.ToString(dt.Rows[0]["MAX_NUMBER"]);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetPreviousNumber", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = string.Empty;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }
        #endregion

        #region 次の番号を取得
        /// <summary>
        /// 次の番号を取得
        /// </summary>
        /// <param name="tableName">テーブル物理名称</param>
        /// <param name="fieldName">フィールド名</param>
        /// <param name="numberValue">番号</param>
        /// <param name="kyoten">拠点</param>
        /// <returns>次の番号</returns>
        internal String GetNextNumber(String tableName, String fieldName, String numberValue, String kyoten, out bool catchErr)
        {
            String returnVal = string.Empty;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(tableName, fieldName, numberValue, kyoten);

                // SQL文作成
                string selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                selectStr += " WHERE DELETE_FLG = 0 ";
                if (!String.IsNullOrEmpty(numberValue))
                {
                    selectStr += "   AND " + fieldName + " > " + int.Parse(numberValue);
                }
                if (!String.IsNullOrEmpty(kyoten))
                {
                    selectStr += "   AND KYOTEN_CD = " + kyoten;
                }

                // データ取得
                DataTable dt = this.teikiHaishaEntryDao.GetDateForStringSql(selectStr);
                // 取得できない場合、配車番号の最小値を取得する
                if (Convert.ToString(dt.Rows[0]["MIN_NUMBER"]) == "")
                {
                    selectStr = "SELECT MIN(" + fieldName + ") AS MIN_NUMBER FROM " + tableName;
                    selectStr += " WHERE DELETE_FLG = 0 ";
                    if (!String.IsNullOrEmpty(kyoten))
                    {
                        selectStr += "   AND KYOTEN_CD = " + kyoten;
                    }
                    // データ取得
                    dt = this.teikiEntryDao.GetDateForStringSql(selectStr);
                }

                // 次の配車番号を戻す
                returnVal = Convert.ToString(dt.Rows[0]["MIN_NUMBER"]);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNextNumber", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = string.Empty;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal, catchErr);
            }
            return returnVal;
        }
        #endregion

        #region [yyyy/mm/dd hh:mm:ss]処理

        #region [Date,Hour,Minute]をDateTime[yyyy/mm/dd hh:mm:ss]に組合
        /// <summary>
        /// [Date,Hour,Minute]をDateTime[yyyy/mm/dd hh:mm:ss]に組合
        /// </summary>
        /// <param name="objDate">date</param>
        /// <param name="hour">hour</param>
        /// <param name="minute">minute</param>
        /// <param name="strDatetime">組合値</param>
        /// <returns>bool</returns>
        private bool TryChgDHMtoDateTime(object objDate, string hour, string minute, out string strDatetime)
        {
            // 戻り値初期化
            strDatetime = string.Empty;

            // DateがNullの場合
            if (objDate == null)
            {
                // 処理終了
                return false;
            }

            strDatetime += ((DateTime)objDate).ToShortDateString();
            strDatetime += " ";
            strDatetime += this.ChgNullToValue(hour, "00");
            strDatetime += ":";
            strDatetime += this.ChgNullToValue(minute, "00");
            strDatetime += ":00";

            return true;
        }
        #endregion

        #region [Hour,Minute]を[hh:mm]に組合
        /// <summary>
        /// [Hour,Minute]を[hh:mm]に組合
        /// </summary>
        /// <param name="hour">hour</param>
        /// <param name="minute">minute</param>
        /// <param name="strTime">組合値</param>
        /// <returns>bool</returns>
        private bool TryChgHMtoTime(string hour, string minute, out string strTime)
        {
            // 戻り値初期化
            strTime = string.Empty;

            if (string.IsNullOrEmpty(hour) || string.IsNullOrEmpty(minute))
            {
                return false;
            }

            strTime += this.ChgNullToValue(hour, "00");
            strTime += ":";
            strTime += this.ChgNullToValue(minute, "00");

            return true;
        }
        #endregion

        #region DateTime[yyyy/mm/dd hh:mm:ss]値を分解
        /// <summary>
        /// DateTime[yyyy/mm/dd hh:mm:ss]値を分解
        /// </summary>
        /// <param name="objDateTime">分解対象</param>
        /// <param name="date">date</param>
        /// <param name="hour">hour</param>
        /// <param name="minute">minute</param>
        /// <returns>bool</returns>
        private bool TryChgDateTimeToDHM(object objDateTime, out string date, out string hour, out string minute)
        {
            // 戻り値初期化
            date = string.Empty;
            hour = string.Empty;
            minute = string.Empty;

            // DBNullの場合
            if (objDateTime is DBNull || string.IsNullOrEmpty(objDateTime.ToString()))
            {
                // 処理終了
                return false;
            }

            // DateTimeに変換
            DateTime dateTime = DateTime.Parse(objDateTime.ToString());

            // Date
            date = dateTime.Date.ToShortDateString();
            // Hour
            hour = dateTime.Hour.ToString();
            // Minute
            minute = dateTime.Minute.ToString();

            return true;
        }
        #endregion

        #region Time値[hh:mm]を分解
        /// <param name="objTime">分解対象</param>
        /// <param name="hour">hour</param>
        /// <param name="minute">minute</param>
        /// <returns>bool</returns>
        private bool TryChgTimeToHM(object objTime, out string hour, out string minute)
        {
            // 戻り値初期化
            hour = string.Empty;
            minute = string.Empty;

            // DBNullの場合
            if (objTime is DBNull)
            {
                // 処理終了
                return false;
            }

            // Timeを配列に変換
            string[] temp = objTime.ToString().Split(':');
            if (temp.Length > 1)
            {
                // Hour
                hour = objTime.ToString().Split(':')[0];
                // Minute
                minute = objTime.ToString().Split(':')[1];
            }

            return true;
        }
        #endregion

        #region Null値を指定値に変換
        /// <summary>
        /// Null値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>string</returns>
        private string ChgNullToValue(string obj, string value)
        {
            if (string.IsNullOrEmpty(obj))
            {
                return value;
            }
            else
            {
                return obj;
            }
        }
        #endregion
        #endregion

        #region F4按分実行と換算後情報取得
        /// <summary>
        /// F4按分実行
        /// </summary>
        public void SetAnbun()
        {
            try
            {
                // 実数チェックがある行の按分後数量設定
                foreach (var row in this.form.DetailIchiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow))
                {
                    // 実数
                    var jissuuFlg = false;
                    if (null != row.Cells["ANBUN_FLG"].Value)
                    {
                        jissuuFlg = this.ConvertToBool(row.Cells["ANBUN_FLG"].Value);
                    }
                    else
                    {
                        row.Cells["ANBUN_FLG"].Value = false;
                    }
                    // 単位
                    var unitCd = String.Empty;
                    if (null != row.Cells[ConstCls.DetailColName.UNIT_CD].Value)
                    {
                        unitCd = row.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString();
                    }
                    // 換算後単位
                    var kansanUnitCd = String.Empty;
                    if (null != row.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value)
                    {
                        kansanUnitCd = row.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value.ToString();
                    }

                    row.Cells[ConstCls.DetailColName.ANBUN_SUURYOU].Value = 0;
                    if (jissuuFlg && ConstCls.mKg_UnitCdKg == kansanUnitCd)
                    {
                        // 換算後数量を使用する
                        if (null != row.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].Value && !String.IsNullOrEmpty(row.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].Value.ToString()))
                        {
                            row.Cells[ConstCls.DetailColName.ANBUN_SUURYOU].Value = decimal.Parse(row.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].Value.ToString());
                        }
                    }
                    else if (jissuuFlg && ConstCls.mKg_UnitCdKg == unitCd)
                    {
                        // 数量を使用する
                        if (null != row.Cells[ConstCls.DetailColName.SUURYOU].Value && !String.IsNullOrEmpty(row.Cells[ConstCls.DetailColName.SUURYOU].Value.ToString()))
                        {
                            row.Cells[ConstCls.DetailColName.ANBUN_SUURYOU].Value = decimal.Parse(row.Cells[ConstCls.DetailColName.SUURYOU].Value.ToString());
                        }
                    }
                }

                var targetNioroshiRow = this.form.NioroshiIchiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow
                                                                                                        && null != r.Cells[ConstCls.NioroshiColName.NIOROSHI_RYOU].Value
                                                                                                        && !String.IsNullOrEmpty(r.Cells[ConstCls.NioroshiColName.NIOROSHI_RYOU].Value.ToString())
                                                                                                        && 0 < decimal.Parse(r.Cells[ConstCls.NioroshiColName.NIOROSHI_RYOU].Value.ToString())
                                                                                                    );
                foreach (var nioroshiRow in targetNioroshiRow)
                {
                    // 按分対象の行を取得
                    var targetDetailRow = this.form.DetailIchiran.Rows.Cast<DataGridViewRow>().Where(r => false == r.IsNewRow
                                                                                                 && null != r.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].Value && r.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].Value.ToString() == nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_NUMBER].Value.ToString()
                                                                                                 && ((null != r.Cells[ConstCls.DetailColName.UNIT_CD].Value && ConstCls.mKg_UnitCdKg == r.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString() && null != r.Cells[ConstCls.DetailColName.SUURYOU].Value && !String.IsNullOrEmpty(r.Cells[ConstCls.DetailColName.SUURYOU].Value.ToString()))
                                                                                                  || (null != r.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value && ConstCls.mKg_UnitCdKg == r.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value.ToString() && null != r.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].Value && !String.IsNullOrEmpty(r.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].Value.ToString())))
                                                                                              );
                    if (0 < targetDetailRow.Count())
                    {
                        // 荷卸し数量を計算（荷卸し数量 - 実数）
                        var nioroshiSuuryou = decimal.Parse(nioroshiRow.Cells[ConstCls.NioroshiColName.NIOROSHI_RYOU].Value.ToString());
                        nioroshiSuuryou -= targetDetailRow.Where(r => true == this.ConvertToBool(r.Cells["ANBUN_FLG"].Value)).Sum(r => decimal.Parse(r.Cells[ConstCls.DetailColName.ANBUN_SUURYOU].Value.ToString()));

                        // 実数以外の合計数量を計算（単位 or 換算後単位 が kg のもの）
                        var totalSuuryou = targetDetailRow.Where(r => null != r.Cells[ConstCls.DetailColName.UNIT_CD].Value && ConstCls.mKg_UnitCdKg == r.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString() && false == (this.ConvertToBool(r.Cells["ANBUN_FLG"].Value))).Sum(r => decimal.Parse(r.Cells[ConstCls.DetailColName.SUURYOU].Value.ToString()));
                        totalSuuryou += targetDetailRow.Where(r => null != r.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value && ConstCls.mKg_UnitCdKg == r.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value.ToString() && false == (this.ConvertToBool(r.Cells["ANBUN_FLG"].Value))).Sum(r => decimal.Parse(r.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].Value.ToString()));

                        // 按分数量計
                        var anbunSuuryouTotal = 0m;
                        foreach (var detailRow in targetDetailRow.Where(r => false == this.ConvertToBool(r.Cells["ANBUN_FLG"].Value)))
                        {
                            var suuryou = 0m;
                            if (null != detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value && ConstCls.mKg_UnitCdKg == detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value.ToString())
                            {
                                suuryou = decimal.Parse(detailRow.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].Value.ToString());
                            }
                            else if (null != detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value && ConstCls.mKg_UnitCdKg == detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString())
                            {
                                suuryou = decimal.Parse(detailRow.Cells[ConstCls.DetailColName.SUURYOU].Value.ToString());
                            }

                            var anbunSuuryou = Math.Round(nioroshiSuuryou * suuryou / totalSuuryou, MidpointRounding.AwayFromZero);
                            detailRow.Cells[ConstCls.DetailColName.ANBUN_SUURYOU].Value = anbunSuuryou;

                            anbunSuuryouTotal += anbunSuuryou;
                        }

                        // 最終行で端数を調整
                        if (0 != nioroshiSuuryou - anbunSuuryouTotal)
                        {
                            if (null != targetDetailRow.Where(r => false == this.ConvertToBool(r.Cells["ANBUN_FLG"].Value)).LastOrDefault())
                            {
                                targetDetailRow.Where(r => false == this.ConvertToBool(r.Cells["ANBUN_FLG"].Value)).Last().Cells[ConstCls.DetailColName.ANBUN_SUURYOU].Value
                                    = decimal.Parse(targetDetailRow.Where(r => false == this.ConvertToBool(r.Cells["ANBUN_FLG"].Value)).Last().Cells[ConstCls.DetailColName.ANBUN_SUURYOU].Value.ToString()) + nioroshiSuuryou - anbunSuuryouTotal;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetAnbun", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
        }

        /// <summary>
        /// 画面中に換算情報 設定
        /// </summary>
        /// <param name="detailRow"></param>
        public void setKansannInfo(DataGridViewRow detailRow)
        {
            LogUtility.DebugMethodStart(detailRow);

            searchDto = new DTOClass();
            // 換算情報 
            searchDto.GyoushaCd = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD]).ToString();
            searchDto.GenbaCd = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.GENBA_CD]).ToString();
            searchDto.HinmeiCd = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.HINMEI_CD]).ToString();
            string strUnitCd = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.UNIT_CD]).ToString();
            string suuryou = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.SUURYOU]).ToString();
            string kaisanSuuryou = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.KANSAN_SUURYOU]).ToString();
            string kaisannUnitCd = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD]).ToString();
            string denpyouKbnCd = this.GetCellValue(detailRow.Cells["DENPYOU_KBN_CD"]).ToString();
            string roundNo = this.GetCellValue(detailRow.Cells["ROUND_NO"]).ToString();

            if (!string.IsNullOrEmpty(searchDto.GyoushaCd)
                && !string.IsNullOrEmpty(searchDto.GenbaCd)
                && !string.IsNullOrEmpty(searchDto.HinmeiCd)
                && !string.IsNullOrEmpty(strUnitCd)
                && !string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text)
                && !string.IsNullOrEmpty(denpyouKbnCd))
            {
                //回数があった場合のみ、コース詳細の換算値を取得する
                if (!string.IsNullOrEmpty(roundNo))
                {
                    searchDto.UnitCd = int.Parse(strUnitCd);
                    searchDto.DenpyouKbnCd = int.Parse(denpyouKbnCd);
                    searchDto.RoundNo = int.Parse(roundNo);
                    searchDto.dayYoNi = this.form.DAY_CD.Text;
                    searchDto.courseNameCd = this.form.COURSE_NAME_CD.Text;
                    // 定期実績換算情報を取得する
                    this.searchResultKannsannInfo = teikiDetailDao.GetCourseKansanData(this.searchDto);
                }
                else
                {
                    this.searchResultKannsannInfo = null;
                }
                //換算情報設定
                this.SetKansanInfo(detailRow);
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 換算情報を取得と設定
        /// </summary>
        public void SetKansanInfo(DataGridViewRow dtRow)
        {
            LogUtility.DebugMethodStart(dtRow);

            string strKansanUnitCd = string.Empty;
            string strKansanUnitNm = string.Empty;
            string strUnitCd = string.Empty;
            string strSUURYOU = string.Empty;
            string strKANSANCHI = string.Empty;
            string strKansanUnitMobileOutputFlg = string.Empty;

            bool kansanInfoExist = false;
            // 単位CD
            strUnitCd = this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.UNIT_CD]).ToString();
            // 数量
            strSUURYOU = this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.SUURYOU]).ToString();

            // 換算情報 
            string condetion = string.Empty;
            DataTable dt = null;
            if (!string.IsNullOrEmpty(this.form.TEIKI_HAISHA_NUMBER.Text))
            {
                this.searchDto.TeikiHaishaNumber = this.form.TEIKI_HAISHA_NUMBER.Text;
                dt = teikiDetailDao.GetTeikiKansanData(this.searchDto);
                if (dt.Rows.Count == 1)
                {
                    // 換算値                  
                    strKANSANCHI = this.ChgDBNullToValue(dt.Rows[0][ConstCls.DetailColName.KANSANCHI], string.Empty).ToString();
                    // 換算後単位CD                  
                    strKansanUnitCd = this.ChgDBNullToValue(dt.Rows[0][ConstCls.DetailColName.KANSAN_UNIT_CD], string.Empty).ToString();
                    // 換算後単位
                    strKansanUnitNm = this.ChgDBNullToValue(dt.Rows[0][ConstCls.DetailColName.UNITKANSAN_NAME], string.Empty).ToString();
                    // 換算後単位モバイル出力フラグ
                    strKansanUnitMobileOutputFlg = this.ChgDBNullToValue(dt.Rows[0][ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG], SqlBoolean.False).ToString();

                    // 換算情報存在
                    kansanInfoExist = true;
                }
            }
            if (!kansanInfoExist && searchResultKannsannInfo != null && searchResultKannsannInfo.Rows.Count > 0)
            {
                DataRow[] dtRows = this.searchResultKannsannInfo.Select(condetion);
                if (dtRows.Length == 1)
                {
                    // 換算値                  
                    strKANSANCHI = this.ChgDBNullToValue(dtRows[0][ConstCls.DetailColName.KANSANCHI], string.Empty).ToString();
                    // 換算後単位CD                  
                    strKansanUnitCd = this.ChgDBNullToValue(dtRows[0][ConstCls.DetailColName.KANSAN_UNIT_CD], string.Empty).ToString();
                    // 換算後単位
                    strKansanUnitNm = this.ChgDBNullToValue(dtRows[0][ConstCls.DetailColName.UNITKANSAN_NAME], string.Empty).ToString();
                    // 換算後単位モバイル出力フラグ
                    strKansanUnitMobileOutputFlg = this.ChgDBNullToValue(dtRows[0][ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG], SqlBoolean.False).ToString();
                    
                    // 換算情報存在
                    kansanInfoExist = true;
                }
            }
            if (!kansanInfoExist)
            {
                // 現場マスタから取得
                dt = teikiDetailDao.GetKansanData(this.searchDto);
                if (dt.Rows.Count == 1)
                {
                    // 換算値                  
                    strKANSANCHI = this.ChgDBNullToValue(dt.Rows[0][ConstCls.DetailColName.KANSANCHI], string.Empty).ToString();
                    // 換算後単位CD                  
                    strKansanUnitCd = this.ChgDBNullToValue(dt.Rows[0][ConstCls.DetailColName.KANSAN_UNIT_CD], string.Empty).ToString();
                    // 換算後単位
                    strKansanUnitNm = this.ChgDBNullToValue(dt.Rows[0][ConstCls.DetailColName.UNITKANSAN_NAME], string.Empty).ToString();
                    // 換算後単位モバイル出力フラグ
                    strKansanUnitMobileOutputFlg = this.ChgDBNullToValue(dt.Rows[0][ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG], SqlBoolean.False).ToString();

                    // 換算情報存在
                    kansanInfoExist = true;
                }
            }

            // ① 他の単位をKgへ換算の場合
            // ② Kgを他の単位へ換算の場合
            // ③ Kg→Kgへの単位換算の場合
            //《公式》:[換算値] × [数量] = [換算後数量]
            // 数量が未入力の場合は換算後数量の計算を行わない
            if (!String.IsNullOrEmpty(this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.SUURYOU]))
                && (!string.IsNullOrEmpty(strKANSANCHI) && kansanInfoExist)
                && (ConstCls.mKg_UnitCdKg.Equals(strUnitCd) || ConstCls.mKg_UnitCdKg.Equals(strKansanUnitCd))
                && (string.IsNullOrEmpty(strKansanUnitMobileOutputFlg) || strKansanUnitMobileOutputFlg.Equals(SqlBoolean.False.ToString()))
                )
            {
                // 換算値
                dtRow.Cells[ConstCls.DetailColName.KANSANCHI].Value = strKANSANCHI;
                // 換算後単位CD
                dtRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value = strKansanUnitCd;
                // 換算後単位
                dtRow.Cells[ConstCls.DetailColName.UNITKANSAN_NAME].Value = strKansanUnitNm;
                // 換算後単位モバイル出力フラグ
                dtRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = strKansanUnitMobileOutputFlg;

                // 換算値
                decimal iKANSANCHI = string.IsNullOrEmpty(this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.KANSANCHI]).ToString()) ? 0 : decimal.Parse(this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.KANSANCHI]));
                // 数量
                decimal iSUURYOU = decimal.Parse(this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.SUURYOU]));

                // 換算後数量 this.SuuryouAndTankFormat(this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.KANSAN_SUURYOU], 0), systemSuuryouFormat);
                dtRow.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].Value = this.SuuryouAndTankFormat(iSUURYOU * iKANSANCHI, systemSuuryouFormat);
            } // ④ 該当の単位が見つからない場合
            else
            {
                if (IsUpdatableKansan(dtRow))
                {
                    // 換算値
                    dtRow.Cells[ConstCls.DetailColName.KANSANCHI].Value = string.Empty;
                    //// 月極区分
                    //dtRow.Cells["TSUKIGIME_KBN"].Value = string.Empty;             

                    decimal iSUURYOU = 0;
                    if (!string.IsNullOrEmpty(strSUURYOU))
                    {
                        iSUURYOU = decimal.Parse(strSUURYOU);
                    }
                    // 換算後数量
                    dtRow.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].Value = string.Empty; //this.SuuryouAndTankFormat(iSUURYOU, systemSuuryouFormat);
                    // 換算後単位CD
                    dtRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value = "3";
                    // 換算後単位
                    dtRow.Cells[ConstCls.DetailColName.UNITKANSAN_NAME].Value = "kg";//this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.UNIT_NAME_RYAKU]).ToString();
                    // 換算後単位モバイル出力フラグ
                    dtRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = SqlBoolean.True.ToString();
                }
                else if (!string.IsNullOrEmpty(strKansanUnitCd) && !string.IsNullOrEmpty(strKansanUnitNm))
                {
                    // 換算値
                    dtRow.Cells[ConstCls.DetailColName.KANSANCHI].Value = string.Empty;

                    decimal iSUURYOU = 0;
                    if (!string.IsNullOrEmpty(strSUURYOU))
                    {
                        iSUURYOU = decimal.Parse(strSUURYOU);
                    }
                    // 換算後数量
                    dtRow.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].Value = string.Empty;
                    // 換算後単位CD
                    dtRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value = strKansanUnitCd;
                    // 換算後単位
                    dtRow.Cells[ConstCls.DetailColName.UNITKANSAN_NAME].Value = strKansanUnitNm;
                    // 換算後単位モバイル出力フラグ
                    dtRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value = SqlBoolean.True.ToString();
                }
            }
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 換算後関連の更新対象か
        /// </summary>
        /// <param name="dtRow"></param>
        /// <returns></returns>
        private bool IsUpdatableKansan(DataGridViewRow dtRow)
        {
            string gyousyaCd = this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.GYOUSHA_CD]);
            string genbaCd = this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.GENBA_CD]);
            string hinmeiCd = this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.HINMEI_CD]);
            string suuryou = this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.SUURYOU]);
            string unitCd = this.GetCellValue(dtRow.Cells[ConstCls.DetailColName.UNIT_CD]);

            decimal num = 0;
            decimal baknum = 0;
            decimal.TryParse(suuryou, out num);
            decimal.TryParse(this.form.bakSuuryou, out baknum);

            // 単位CDでKgが入力済みの場合は更新対象外
            if (IsUnitCdOfKg(unitCd))
            {
                return false;
            }

            // 業者CD,現場CD,品名CD,数量,単位CDの何れかが変更された場合のみ、更新対象
            if (!gyousyaCd.Equals(this.form.bakGyousyaCd))
            {
                return true;
            }
            if (!genbaCd.Equals(this.form.bakGenbaCd))
            {
                return true;
            }
            if (!hinmeiCd.Equals(this.form.bakHinmeiCd))
            {
                return true;
            }
            if (num != baknum)
            {
                return true;
            }
            if (!unitCd.Equals(this.form.bakUnitCd))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 指定された単位CDがKgか判定
        /// </summary>
        /// <param name="unitCd"></param>
        /// <returns></returns>
        private bool IsUnitCdOfKg(string unitCd)
        {
            if (string.IsNullOrEmpty(unitCd))
            {
                return false;
            }

            string paddingKg = ConstCls.mKg_UnitCdKg.PadLeft(2, '0');
            if (ConstCls.mKg_UnitCdKg.Equals(unitCd) || paddingKg.Equals(unitCd))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 収集毎タブの一覧での換算情報設定
        /// </summary>
        /// <param name="e"></param>
        public bool SetKansanInfoSyuusyuu(DataGridViewCellEventArgs e)
        {
            bool ret = true;
            try
            {
                // 「収集分」行より前の行の場合は処理終了
                if (-1 == e.RowIndex || e.ColumnIndex < SyuusyuuColumnIndex)
                {
                    return ret;
                }

                DataGridViewRow detailRow = this.form.syuusyuuDetailIchiran.Rows[e.RowIndex];
                if (detailRow == null || detailRow.Cells[e.ColumnIndex] == null || this.form.syuusyuuDetailIchiran.Columns[e.ColumnIndex].Tag == null)
                {
                    return ret;
                }

                var hinmeiColumn = this.form.syuusyuuDetailIchiran.Columns[e.ColumnIndex].Tag as ShushuIchiranData.HinmeiUnitColumn;
                // 換算後単位の場合、計算対象外のため終了
                if (hinmeiColumn == null || !hinmeiColumn.IsUnit)
                {
                    return ret;
                }

                if ((ConstCls.mKg_UnitCdKg.Equals(hinmeiColumn.UnitCD) || ConstCls.mKg_UnitCdKg.Equals(hinmeiColumn.KansanUnitCD)))
                {
                    string strGyoushaCd = this.GetCellValue(detailRow.Cells["clmGYOUSHA_CD"]);
                    string strGenbaCd = this.GetCellValue(detailRow.Cells["clmGENBA_CD"]);
                    string strHinmeiCd = hinmeiColumn.HinmeiCD;
                    string strUnitCd = hinmeiColumn.UnitCD;
                    string strDenpyouKbnCd = hinmeiColumn.DenpyouKbnCd;

                    int unitCd = 0;
                    int denpyouKbnCd = 0;
                    if (string.IsNullOrEmpty(strGyoushaCd)
                        || string.IsNullOrEmpty(strGenbaCd)
                        || string.IsNullOrEmpty(strHinmeiCd)
                        || string.IsNullOrEmpty(strUnitCd)
                        || !int.TryParse(strUnitCd, out unitCd)
                        || string.IsNullOrEmpty(strDenpyouKbnCd)
                        || !int.TryParse(strDenpyouKbnCd, out denpyouKbnCd))
                    {
                        return ret;
                    }

                    // 換算単位CDのあるカラム位置を取得
                    int kansanUnitCdIndex = GetKansanUnitCdColumnIndex(hinmeiColumn.HinmeiCD, hinmeiColumn.KansanUnitCD);
                    if (kansanUnitCdIndex == -1)
                    {
                        return ret;
                    }

                    DTOClass dto = new DTOClass();
                    dto.GyoushaCd = strGyoushaCd;
                    dto.GenbaCd = strGenbaCd;
                    dto.HinmeiCd = strHinmeiCd;
                    dto.UnitCd = unitCd;
                    dto.DenpyouKbnCd = denpyouKbnCd;
                    var result = teikiDetailDao.GetKansanData(dto);
                    if (result == null || result.Rows.Count == 0)
                    {
                        return ret;
                    }
                    // 換算値を取得
                    string strKansanchi = this.ChgDBNullToValue(result.Rows[0][ConstCls.DetailColName.KANSANCHI], string.Empty).ToString();
                    decimal kansanchi = 0;
                    string suuryouStr = this.GetCellValue(detailRow.Cells[e.ColumnIndex]);
                    decimal suuryou = 0;

                    // 前回値と変更が無かったら処理中断
                    if (this.form.bakSuuryouSuusuIchiran.ContainsKey(e.ColumnIndex)
                    && !this.form.bakSuuryouSuusuIchiran[e.ColumnIndex].Equals(
                        Convert.ToString(detailRow.Cells[e.ColumnIndex].Value)))
                    {
                        // 対象の数量を更新
                        if (string.IsNullOrEmpty(strKansanchi) || !decimal.TryParse(strKansanchi, out kansanchi)
                            || string.IsNullOrEmpty(suuryouStr) || !decimal.TryParse(suuryouStr, out suuryou))
                        {
                            detailRow.Cells[kansanUnitCdIndex].Value = string.Empty;
                            return ret;
                        }

                        detailRow.Cells[kansanUnitCdIndex].Value = this.SuuryouAndTankFormat(suuryou * kansanchi, this.systemSuuryouFormat);
                        // 前回値チェック用データをセット
                        if (this.form.bakSuuryouSuusuIchiran.ContainsKey(e.ColumnIndex))
                        {
                            this.form.bakSuuryouSuusuIchiran[e.ColumnIndex] = Convert.ToString(detailRow.Cells[e.ColumnIndex].Value);
                        }
                        else
                        {
                            this.form.bakSuuryouSuusuIchiran.Add(e.ColumnIndex, Convert.ToString(detailRow.Cells[e.ColumnIndex].Value));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetKansanInfoSyuusyuu", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 指定された品名CDと単位CDに紐付く換算後単位CDのカラム位置を取得
        /// </summary>
        /// <param name="hinmeiCd"></param>
        /// <param name="kansanunitCd"></param>
        /// <returns></returns>
        private int GetKansanUnitCdColumnIndex(string hinmeiCd, string kansanunitCd)
        {
            if (string.IsNullOrEmpty(hinmeiCd) || string.IsNullOrEmpty(kansanunitCd))
            {
                return -1;
            }

            for (int i = SyuusyuuColumnIndex; i < this.form.syuusyuuDetailIchiran.Columns.Count; i++)
            {
                var header = this.form.syuusyuuDetailIchiran.Columns[i];
                if (header.Tag == null)
                {
                    continue;
                }

                string targetHinmeiCd = header.Name.Substring(0, 6);
                string targetUnitCd = header.Name.Substring(6);
                if (targetHinmeiCd.Equals(hinmeiCd) && targetUnitCd.Equals(kansanunitCd))
                {
                    return i;
                }
            }

            return -1;
        }


        /// <summary>
        /// 伝票区分、契約区分、集計単位情報を取得と設定
        /// </summary>
        /// <param name="detailRow">選択行</param>
        /// <param name="e">DataGridViewCellValidatingEventArgs</param>
        public void GetDataSetKbnInfo(DataGridViewRow detailRow, DataGridViewCellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(detailRow, e);

                //伝票区分
                string strDENPYOU_KBN_CD = string.Empty;
                //契約区分
                string strKEIYAKU_KBN = string.Empty;
                //計上区分(集計単位)
                string strKEIJYOU_KBN = string.Empty;

                DTOClass searchDto = new DTOClass();
                // 換算情報 
                searchDto.GyoushaCd = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD]).ToString();
                searchDto.GenbaCd = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.GENBA_CD]).ToString();
                searchDto.HinmeiCd = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.HINMEI_CD]).ToString();
                string strUnitCd = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.UNIT_CD]).ToString();

                if (searchDto.GyoushaCd.Equals(this.form.mOrgDedailDtoInfo.GyoushaCd) &&
                    searchDto.GenbaCd.Equals(this.form.mOrgDedailDtoInfo.GenbaCd) &&
                    searchDto.HinmeiCd.Equals(this.form.mOrgDedailDtoInfo.HinmeiCd) &&
                    strUnitCd.Equals(this.form.mOrgDedailDtoInfo.UnitCd.ToString()))
                {
                    // 伝票区分Nullチェック
                    var denpyouKbn = this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value == null ? string.Empty : this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value.ToString();

                    if ((this.form.DetailIchiran.Columns[ConstCls.DetailColName.HINMEI_CD].Index == e.ColumnIndex)
                        && string.IsNullOrEmpty(denpyouKbn))
                    {
                        // 品名CDの入力時、かつ、伝票区分CDの入力がない場合
                        M_HINMEI HimeiInfo = DaoInitUtility.GetComponent<IM_HINMEIDao>().GetDataByCd(searchDto.HinmeiCd);
                        if (HimeiInfo != null)
                        {
                            switch (HimeiInfo.DENPYOU_KBN_CD.ToString())
                            {
                                // 売上
                                case "1":
                                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = HimeiInfo.DENPYOU_KBN_CD.ToString();
                                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = "売上";
                                    break;
                                // 支払
                                case "2":
                                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = HimeiInfo.DENPYOU_KBN_CD.ToString();
                                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = "支払";
                                    break;
                                // 上記以外はポップアップで選択
                                default:
                                    // ポップアップ起動
                                    Dictionary<int, List<r_framework.Dto.PopupReturnParam>> returnParams = ShouDenpyouKbnPopUp();
                                    if (returnParams != null)
                                    {
                                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = returnParams[0][0].Value.ToString();
                                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = returnParams[1][0].Value.ToString();
                                        form.bCancelDenpyoPopup = false;
                                    }
                                    else
                                    {
                                        // 選択されていない場合はフォーカス移動させない
                                        e.Cancel = true;
                                        //売上支払入力の処理に合わせるための処理
                                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = string.Empty;
                                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = string.Empty;
                                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["HINMEI_NAME_RYAKU"].Value = string.Empty;
                                        form.bCancelDenpyoPopup = true;
                                    }
                                    break;
                            }
                        }
                        return;
                    }
                    else
                    {
                        // その他、変更が無い場合はデータ取得&設定はしない
                        return;
                    }
                }

                // 業者CD、現場CD、品名CD、単位CDの何れかが変更された場合 &&
                // 業者CD、現場CD、品名CDが入力済みの場合
                if (!string.IsNullOrEmpty(searchDto.GenbaCd) &&
                    !string.IsNullOrEmpty(searchDto.GyoushaCd) &&
                    !string.IsNullOrEmpty(searchDto.HinmeiCd))
                {
                    // 定期実績換算情報を取得する
                    if (!string.IsNullOrEmpty(strUnitCd))
                    {
                        searchDto.UnitCd = int.Parse(strUnitCd);
                    }
                    this.searchResultKannsannInfo = this.teikiDetailDao.GetKansanData(searchDto);
                    if (searchResultKannsannInfo != null && searchResultKannsannInfo.Rows.Count > 0)
                    {
                        if (searchResultKannsannInfo.Rows.Count > 1)
                        {
                            #region 複数レコードの場合
                            // 単位CDでソートしておく
                            DataTable sortedDataTable = searchResultKannsannInfo.Clone();
                            DataView dataView = searchResultKannsannInfo.DefaultView;
                            dataView.Sort = "ROW_NO";
                            foreach (DataRowView dataRowView in dataView)
                            {
                                sortedDataTable.ImportRow(dataRowView.Row);
                            }

                            // ポップアップ表示
                            MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
                            DataTable dt = new DataTable();
                            dt.Columns.Add("HINMEI_NAME_RYAKU", typeof(string));
                            dt.Columns.Add("DENPYOU_KBN_CD", typeof(string));
                            dt.Columns.Add("DENPYOU_KBN_NAME_RYAKU", typeof(string));
                            dt.Columns.Add("UNIT_CD", typeof(string));
                            dt.Columns.Add("UNIT_NAME_RYAKU", typeof(string));
                            dt.Columns.Add("KEIYAKU_KBN", typeof(string));
                            dt.Columns.Add("KEIYAKU_KBN_NM", typeof(string));
                            dt.Columns.Add("KEIJYOU_KBN", typeof(string));
                            dt.Columns.Add("KEIJYOU_KBN_NM", typeof(string));

                            dt.Columns[0].ReadOnly = true;
                            dt.Columns[1].ReadOnly = true;
                            dt.Columns[2].ReadOnly = true;
                            dt.Columns[3].ReadOnly = true;
                            dt.Columns[4].ReadOnly = true;
                            dt.Columns[5].ReadOnly = true;
                            dt.Columns[6].ReadOnly = true;
                            dt.Columns[7].ReadOnly = true;
                            dt.Columns[8].ReadOnly = true;
                            DataRow row;
                            foreach (DataRow kansanRow in sortedDataTable.Rows)
                            {
                                row = dt.NewRow();
                                row["HINMEI_NAME_RYAKU"] = kansanRow["HINMEI_NAME_RYAKU"].ToString();
                                row["DENPYOU_KBN_CD"] = kansanRow["DENPYOU_KBN_CD"].ToString();
                                row["DENPYOU_KBN_NAME_RYAKU"] = kansanRow["DENPYOU_KBN_CD_NM"].ToString();
                                row["UNIT_CD"] = kansanRow["UNIT_CD"].ToString();
                                row["UNIT_NAME_RYAKU"] = kansanRow["UNIT_NAME_RYAKU"].ToString();
                                string keiyakuKbn = kansanRow["KEIYAKU_KBN"].ToString();
                                switch (keiyakuKbn)
                                {
                                    case "1":
                                        row["KEIYAKU_KBN"] = keiyakuKbn;
                                        row["KEIYAKU_KBN_NM"] = "定期";
                                        break;
                                    case "2":
                                        row["KEIYAKU_KBN"] = keiyakuKbn;
                                        row["KEIYAKU_KBN_NM"] = "単価";
                                        break;
                                    case "3":
                                        row["KEIYAKU_KBN"] = keiyakuKbn;
                                        row["KEIYAKU_KBN_NM"] = "回収のみ";
                                        break;
                                    default:
                                        row["KEIYAKU_KBN"] = string.Empty;
                                        row["KEIYAKU_KBN_NM"] = string.Empty;
                                        break;
                                }
                                string keijouKbn = this.ChgDBNullToValue(kansanRow["KEIJYOU_KBN"], string.Empty).ToString();
                                if (!string.IsNullOrEmpty(keijouKbn))
                                {
                                    row["KEIJYOU_KBN"] = keijouKbn;
                                    switch (keijouKbn)
                                    {
                                        case "1":
                                            row["KEIJYOU_KBN_NM"] = "伝票";
                                            break;
                                        case "2":
                                            row["KEIJYOU_KBN_NM"] = "合算";
                                            break;
                                        default:
                                            row["KEIJYOU_KBN_NM"] = "";
                                            break;
                                    }
                                }
                                else
                                {
                                    row["KEIJYOU_KBN"] = "";
                                    row["KEIJYOU_KBN_NM"] = "";
                                }
                                dt.Rows.Add(row);
                            }
                            dt.TableName = "定期回収情報";
                            form.table = dt;
                            form.PopupTitleLabel = "定期回収情報";
                            form.PopupGetMasterField = "HINMEI_NAME_RYAKU,DENPYOU_KBN_CD,DENPYOU_KBN_NAME_RYAKU,UNIT_CD,UNIT_NAME_RYAKU,KEIYAKU_KBN,KEIYAKU_KBN_NM,KEIJYOU_KBN,KEIJYOU_KBN_NM";
                            form.PopupDataHeaderTitle = new string[] { "品名", "伝票区分CD", "伝票区分", "単位CD", "単位名", "契約区分", "契約区分名", "集計区分", "集計区分名" };
                            form.Size = new System.Drawing.Size(850, 500);
                            DataGridViewCellStyle style = form.customDataGridView1.ColumnHeadersDefaultCellStyle;
                            style.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
                            form.customDataGridView1.ColumnHeadersDefaultCellStyle = style;
                            form.customDataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
                            form.StartPosition = FormStartPosition.CenterParent;
                            form.ShowDialog();

                            if (form.ReturnParams != null)
                            {
                                // 伝票区分
                                detailRow.Cells["DENPYOU_KBN_CD"].Value = form.ReturnParams[1][0].Value.ToString();
                                detailRow.Cells["DENPYOU_KBN_CD_NM"].Value = form.ReturnParams[2][0].Value.ToString();

                                // 単位
                                strUnitCd = form.ReturnParams[3][0].Value.ToString();
                                detailRow.Cells["UNIT_CD"].Value = strUnitCd;
                                detailRow.Cells["UNIT_NAME_RYAKU"].Value = form.ReturnParams[4][0].Value.ToString();

                                // 契約区分
                                strKEIYAKU_KBN = form.ReturnParams[5][0].Value.ToString();
                                detailRow.Cells["KEIYAKU_KBN"].Value = strKEIYAKU_KBN;
                                detailRow.Cells["KEIYAKU_KBN_NM"].Value = form.ReturnParams[6][0].Value.ToString();

                                // 集計単位
                                strKEIJYOU_KBN = form.ReturnParams[7][0].Value.ToString();
                                detailRow.Cells["TSUKIGIME_KBN"].Value = strKEIJYOU_KBN;
                                detailRow.Cells["TSUKIGIME_KBN_NM"].Value = form.ReturnParams[8][0].Value.ToString();

                                // 空行選択対策
                                if (!string.IsNullOrEmpty(strUnitCd))
                                {
                                    // 入力区分
                                    if (detailRow.Cells["INPUT_KBN"].Value != "1")
                                    {
                                        detailRow.Cells["INPUT_KBN"].Value = "2";
                                        detailRow.Cells["INPUT_KBN_NAME"].Value = ConstCls.INPUT_KBN_2;

                                        // 回数
                                        var gyousha = Convert.ToString(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value);
                                        var genba = Convert.ToString(detailRow.Cells[ConstCls.DetailColName.GENBA_CD].Value);
                                        detailRow.Cells[ConstCls.DetailColName.ROUND_NO].Value = GetRoundNo(gyousha, genba);

                                        this.form.isCellValidating = true;
                                        detailRow.Cells[ConstCls.DetailColName.ROUND_NO].ReadOnly = true;
                                        detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].ReadOnly = true;
                                        detailRow.Cells[ConstCls.DetailColName.GENBA_CD].ReadOnly = true;
                                        detailRow.Cells[ConstCls.DetailColName.HINMEI_CD].ReadOnly = true;
                                        detailRow.Cells[ConstCls.DetailColName.UNIT_CD].ReadOnly = true;
                                        this.form.isCellValidating = false;
                                    }
                                }
                            }
                            else
                            {
                                // キャンセル時はフォーカス移動しない
                                e.Cancel = true;
                                return;
                            }
                            #endregion 複数レコードの場合
                        }
                        else
                        {
                            #region 1件の場合
                            // 入力区分
                            if (detailRow.Cells["INPUT_KBN"].Value != "1")
                            {
                                detailRow.Cells["INPUT_KBN"].Value = "2";
                                detailRow.Cells["INPUT_KBN_NAME"].Value = ConstCls.INPUT_KBN_2;
                            }

                            // 伝票区分
                            detailRow.Cells["DENPYOU_KBN_CD"].Value = searchResultKannsannInfo.Rows[0]["DENPYOU_KBN_CD"].ToString();
                            detailRow.Cells["DENPYOU_KBN_CD_NM"].Value = searchResultKannsannInfo.Rows[0]["DENPYOU_KBN_CD_NM"].ToString();

                            // 単位
                            strUnitCd = searchResultKannsannInfo.Rows[0]["UNIT_CD"].ToString();
                            detailRow.Cells["UNIT_CD"].Value = strUnitCd;
                            detailRow.Cells["UNIT_NAME_RYAKU"].Value = searchResultKannsannInfo.Rows[0]["UNIT_NAME_RYAKU"].ToString();

                            // 契約区分
                            strKEIYAKU_KBN = searchResultKannsannInfo.Rows[0]["KEIYAKU_KBN"].ToString();
                            switch (strKEIYAKU_KBN)
                            {
                                case "1":
                                    detailRow.Cells["KEIYAKU_KBN"].Value = strKEIYAKU_KBN;
                                    detailRow.Cells["KEIYAKU_KBN_NM"].Value = "定期";
                                    break;
                                case "2":
                                    detailRow.Cells["KEIYAKU_KBN"].Value = strKEIYAKU_KBN;
                                    detailRow.Cells["KEIYAKU_KBN_NM"].Value = "単価";
                                    break;
                                case "3":
                                    detailRow.Cells["KEIYAKU_KBN"].Value = strKEIYAKU_KBN;
                                    detailRow.Cells["KEIYAKU_KBN_NM"].Value = "回収のみ";
                                    break;
                                default:
                                    detailRow.Cells["KEIYAKU_KBN"].Value = string.Empty;
                                    detailRow.Cells["KEIYAKU_KBN_NM"].Value = string.Empty;
                                    break;
                            }

                            // 集計単位
                            string keijouKbn = this.ChgDBNullToValue(searchResultKannsannInfo.Rows[0]["KEIJYOU_KBN"], string.Empty).ToString();
                            detailRow.Cells["TSUKIGIME_KBN"].Value = keijouKbn;
                            if (!string.IsNullOrEmpty(keijouKbn))
                            {
                                switch (keijouKbn)
                                {
                                    case "1":
                                        detailRow.Cells["TSUKIGIME_KBN_NM"].Value = "伝票";
                                        break;
                                    case "2":
                                        detailRow.Cells["TSUKIGIME_KBN_NM"].Value = "合算";
                                        break;
                                    default:
                                        detailRow.Cells["TSUKIGIME_KBN_NM"].Value = "";
                                        break;
                                }
                            }
                            else
                            {
                                detailRow.Cells["TSUKIGIME_KBN_NM"].Value = string.Empty;
                            }

                            if (detailRow.Cells["INPUT_KBN"].Value != "1")
                            {
                                // 回数
                                var gyousha = Convert.ToString(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value);
                                var genba = Convert.ToString(detailRow.Cells[ConstCls.DetailColName.GENBA_CD].Value);
                                detailRow.Cells[ConstCls.DetailColName.ROUND_NO].Value = GetRoundNo(gyousha, genba);
                                this.form.isCellValidating = true;
                                detailRow.Cells[ConstCls.DetailColName.ROUND_NO].ReadOnly = true;
                                detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].ReadOnly = true;
                                detailRow.Cells[ConstCls.DetailColName.GENBA_CD].ReadOnly = true;
                                detailRow.Cells[ConstCls.DetailColName.HINMEI_CD].ReadOnly = true;
                                detailRow.Cells[ConstCls.DetailColName.UNIT_CD].ReadOnly = true;
                                this.form.isCellValidating = false;
                            }
                            #endregion 1件の場合
                        }

                        #region 契約区分操作可否設定
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG && strKEIYAKU_KBN.Equals("2"))
                        {
                            detailRow.Cells["TSUKIGIME_KBN"].ReadOnly = false;
                            this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText.Replace("※", "") + "※";
                        }
                        else if (strKEIYAKU_KBN.Equals("1") || strKEIYAKU_KBN.Equals("3"))
                        {
                            detailRow.Cells["TSUKIGIME_KBN"].ReadOnly = true;
                            this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText.Replace("※", "");
                        }
                        #endregion 契約区分操作可否設定
                    }
                    else
                    {
                        #region 定期実績換算情報が存在しない場合

                        #region 品名CD変更時伝票設定
                        if (this.form.DetailIchiran.Columns[ConstCls.DetailColName.HINMEI_CD].Index == e.ColumnIndex)
                        {
                            M_HINMEI HimeiInfo = DaoInitUtility.GetComponent<IM_HINMEIDao>().GetDataByCd(searchDto.HinmeiCd);
                            if (HimeiInfo != null)
                            {
                                switch (HimeiInfo.DENPYOU_KBN_CD.ToString())
                                {
                                    // 売上
                                    case "1":
                                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = HimeiInfo.DENPYOU_KBN_CD.ToString();
                                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = "売上";

                                        break;
                                    // 支払
                                    case "2":
                                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = HimeiInfo.DENPYOU_KBN_CD.ToString();
                                        this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = "支払";
                                        break;
                                    // 上記以外はポップアップで選択
                                    default:
                                        // ポップアップ起動
                                        Dictionary<int, List<r_framework.Dto.PopupReturnParam>> returnParams = ShouDenpyouKbnPopUp();

                                        if (returnParams != null)
                                        {
                                            this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = returnParams[0][0].Value.ToString();
                                            this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = returnParams[1][0].Value.ToString();
                                        }
                                        else
                                        {
                                            // 選択されていない場合はフォーカス移動させない
                                            e.Cancel = true;
                                            ////売上支払入力の処理に合わせるための処理
                                            this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = string.Empty;
                                            this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = string.Empty;
                                            this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["HINMEI_NAME_RYAKU"].Value = string.Empty;
                                            form.bCancelDenpyoPopup = true;
                                            
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E020", "品名");
                                e.Cancel = true;
                            }
                        }
                        #endregion 品名CD変更時伝票設定

                        #region 契約区分未入力時初期値設定
                        if (this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["KEIYAKU_KBN"].Value == null ||
                            string.IsNullOrEmpty(this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["KEIYAKU_KBN"].Value.ToString()))
                        {
                            // 契約区分⇒1：単価、集計単位⇒1：伝票で設定する
                            this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["KEIYAKU_KBN"].Value = "2";
                            this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["KEIYAKU_KBN_NM"].Value = "単価";
                            this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["TSUKIGIME_KBN"].Value = "1";
                            this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["TSUKIGIME_KBN_NM"].Value = "伝票";
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                detailRow.Cells["TSUKIGIME_KBN"].ReadOnly = false;
                                this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText.Replace("※", "") + "※";
                            }
                        }
                        #endregion 契約区分未入力時初期値設定

                        if (!e.Cancel)
                        {
                            detailRow.Cells["INPUT_KBN"].Value = "1";
                            detailRow.Cells["INPUT_KBN_NAME"].Value = ConstCls.INPUT_KBN_1;
                        }
                        else
                        {
                            detailRow.Cells["INPUT_KBN"].Value = string.Empty;
                            detailRow.Cells["INPUT_KBN_NAME"].Value = string.Empty;
                        }
                        #endregion 定期実績換算情報が存在しない場合
                    }
                }
                else if ((this.form.DetailIchiran.Columns[ConstCls.DetailColName.HINMEI_CD].Index == e.ColumnIndex) &&
                        !string.IsNullOrEmpty(searchDto.HinmeiCd) &&
                        (string.IsNullOrEmpty(searchDto.GenbaCd) ||
                         string.IsNullOrEmpty(searchDto.GyoushaCd)))
                {
                    // 業者または現場の値が無く品名が入力された場合は伝票設定を行う
                    M_HINMEI HimeiInfo = DaoInitUtility.GetComponent<IM_HINMEIDao>().GetDataByCd(searchDto.HinmeiCd);
                    if (HimeiInfo != null)
                    {
                        switch (HimeiInfo.DENPYOU_KBN_CD.ToString())
                        {
                            // 売上
                            case "1":
                                this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = HimeiInfo.DENPYOU_KBN_CD.ToString();
                                this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = "売上";

                                break;
                            // 支払
                            case "2":
                                this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = HimeiInfo.DENPYOU_KBN_CD.ToString();
                                this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = "支払";
                                break;
                            // 上記以外はポップアップで選択
                            default:
                                // ポップアップ起動
                                Dictionary<int, List<r_framework.Dto.PopupReturnParam>> returnParams = ShouDenpyouKbnPopUp();

                                if (returnParams != null)
                                {
                                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = returnParams[0][0].Value.ToString();
                                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = returnParams[1][0].Value.ToString();
                                }
                                else
                                {
                                    // 選択されていない場合はフォーカス移動させない
                                    e.Cancel = true;  
                                    //売上支払入力の処理に合わせるための処理
                                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = string.Empty;
                                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = string.Empty;
                                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["HINMEI_NAME_RYAKU"].Value = string.Empty;
                                    form.bCancelDenpyoPopup = true;
                                                                    
                                }
                                break;
                        }
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "品名");
                        e.Cancel = true;
                    }
                }
                else if ((this.form.DetailIchiran.Columns[ConstCls.DetailColName.HINMEI_CD].Index == e.ColumnIndex) && string.IsNullOrEmpty(searchDto.HinmeiCd))
                {
                    // 品名が空にされた場合は伝票区分をクリア
                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD"].Value = string.Empty;
                    this.form.DetailIchiran.Rows[this.form.DetailIchiran.CurrentCell.RowIndex].Cells["DENPYOU_KBN_CD_NM"].Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetDataSetKbnInfo", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業者CD,現場CDから回数を取得する
        /// </summary>
        /// <param name="gyoushaCd">業者CD</param>
        /// <param name="genbaCd">現場CD</param>
        /// <returns></returns>
        private string GetRoundNo(string gyoushaCd, string genbaCd)
        {
            if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
            {
                return string.Empty;
            }

            int roundNo = 0;
            for (int i = 0; i < this.form.DetailIchiran.Rows.Count; i++)
            {
                var rowGyoushaCd = Convert.ToString(this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, i].Value);
                var rowGenbaCd = Convert.ToString(this.form.DetailIchiran[ConstCls.DetailColName.GENBA_CD, i].Value);

                // カレント行と同一の業者CD、現場CDがあれば回数をインクリメント
                if (gyoushaCd.Equals(rowGyoushaCd) && genbaCd.Equals(rowGenbaCd))
                {
                    roundNo++;
                }
            }

            return roundNo.ToString();
        }

        /// <summary>
        /// 伝票区分選択ポップアップ起動
        /// </summary>
        /// <returns>ポップアップ選択結果</returns>
        private Dictionary<int, List<r_framework.Dto.PopupReturnParam>> ShouDenpyouKbnPopUp()
        {
            MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
            DataTable dt = new DataTable();
            dt.Columns.Add("CD", typeof(string));
            dt.Columns.Add("VALUE", typeof(string));
            dt.Columns[0].ReadOnly = true;
            dt.Columns[1].ReadOnly = true;
            DataRow row;
            row = dt.NewRow();
            row["CD"] = "1";
            row["VALUE"] = "売上";
            dt.Rows.Add(row);
            row = dt.NewRow();
            row["CD"] = "2";
            row["VALUE"] = "支払";
            dt.Rows.Add(row);
            dt.TableName = "伝票区分";
            form.table = dt;
            form.PopupTitleLabel = "伝票区分";
            form.PopupGetMasterField = "CD,VALUE";
            form.PopupDataHeaderTitle = new string[] { "伝票区分CD", "伝票区分名" };

            form.ShowDialog();
            return form.ReturnParams;
        }

        #endregion

        #region 品名GRID画面と収集GRID画面処理
        /// <summary>
        ///  品名データ＝＞収集データ作成
        /// </summary>
        /// <param name="argDetailIchiran">一覧データテーブル</param>
        public void DetailIchiranDataToShushuIchiranData(DataTable argDetailIchiran)
        {
            //List<ShushuIchiranData.Gyousha> GyoushaList = new List<ShushuIchiranData.Gyousha>();
            //データ存在判断する。
            if (argDetailIchiran.Rows.Count <= 0)
            {
                //this.mShushuIchiranData.GyoushaList = null;
                this.mShushuIchiranData.GenbaList = null;
                return;
            }
            //空テーブル作成
            DataTable dtDetailIchiranData = GetTableConstructor();
            DataRow detailRow = null;

            //元のデータ新テーブル設定
            for (int rowIndex = 0; rowIndex < argDetailIchiran.Rows.Count; rowIndex++)
            {
                DataRow newRow = dtDetailIchiranData.NewRow();
                detailRow = searchResultDetail.Rows[rowIndex];
                for (int colIndex = 0; colIndex < this.searchResultDetail.Columns.Count; colIndex++)
                {
                    if (!string.IsNullOrEmpty(detailRow[colIndex].ToString()))
                    {
                        //データ存在時
                        newRow[colIndex] = detailRow[colIndex];
                    }
                    else
                    {
                        //データ存在しない時
                        newRow[colIndex] = string.Empty;
                    }
                }
                dtDetailIchiranData.Rows.Add(newRow);
            }

            #region 業者CD情報list作成
            #region Mod
            var grpByGyshaGenba = from r in dtDetailIchiranData.AsEnumerable()
                                  group r by new
                                  {
                                      ROUND_NO = r.Field<string>("ROUND_NO"),
                                      GYOUSHA_CD = r.Field<string>("GYOUSHA_CD"),
                                      GYOUSHA_NAME_RYAKU = r.Field<string>("GYOUSHA_NAME_RYAKU"),
                                      GENBA_CD = r.Field<string>("GENBA_CD"),
                                      GENBA_NAME_RYAKU = r.Field<string>("GENBA_NAME_RYAKU"),
                                      KAISHUU_BIKOU = r.Field<string>("KAISHUU_BIKOU"),
                                  } into grps
                                  select new
                                  {
                                      ROUND_NO = grps.Key.ROUND_NO,
                                      GYOUSHA_CD = grps.Key.GYOUSHA_CD,
                                      GYOUSHA_NAME_RYAKU = grps.Key.GYOUSHA_NAME_RYAKU,
                                      GENBA_CD = grps.Key.GENBA_CD,
                                      GENBA_NAME_RYAKU = grps.Key.GENBA_NAME_RYAKU,
                                      KAISHUU_BIKOU = grps.Key.KAISHUU_BIKOU
                                  };
            List<ShushuIchiranData.Genba> GenbaList = new List<ShushuIchiranData.Genba>();
            foreach (var grpItem in grpByGyshaGenba)
            {
                ShushuIchiranData.Genba pGenba = new ShushuIchiranData.Genba();
                pGenba.GyoushaCD = grpItem.GYOUSHA_CD;
                pGenba.GyoushaName = grpItem.GYOUSHA_NAME_RYAKU;

                pGenba.RoundNo = grpItem.ROUND_NO;
                //現場CD
                pGenba.GenbaCD = grpItem.GENBA_CD;
                //現場名称
                pGenba.GenbaName = grpItem.GENBA_NAME_RYAKU;
                //業者現場
                pGenba.GyoushaGenba = grpItem.GYOUSHA_CD + grpItem.GENBA_CD;// grpItem.GYOUSHAGENBA;
                //収集備考
                pGenba.SyuusyuuMemo = grpItem.KAISHUU_BIKOU;

                #region 品名CDについてグルプー行情報作成
                var rows = from r in dtDetailIchiranData.AsEnumerable()
                           where r.Field<string>("ROUND_NO") == grpItem.ROUND_NO
                           && r.Field<string>("GYOUSHA_CD") == grpItem.GYOUSHA_CD
                           && r.Field<string>("GENBA_CD") == grpItem.GENBA_CD
                           select r;
                var HinMeiInfo = rows.GroupBy(r => r.Field<string>("HINMEIUNIT"),
                     (k, g) => new
                     {
                         HINMEIUNIT = k,
                         HINMEI_NAME = g.First().Field<string>("HINMEI_NAME_RYAKU"),
                         KAKUTEI_FLG = g.First().Field<string>("KAKUTEI_FLG"),
                         UNIT_CD = g.First().Field<string>("UNIT_CD"),
                         UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                         SUURYOU = g.First().Field<string>("SUURYOU"),
                         KANSAN_SUURYOU = g.First().Field<string>("KANSAN_SUURYOU"),
                         KANSAN_UNIT_CD = g.First().Field<string>("KANSAN_UNIT_CD"),
                         KANSAN_UNIT_NAME = g.First().Field<string>("UNITKANSAN_NAME"),
                         ANBUN_SUURYOU = g.First().Field<string>("ANBUN_SUURYOU"),
                         SHUUSHUU_TIME = g.First().Field<string>("SHUUSHUU_TIME"),
                         HINMEI_BIKOU = g.First().Field<string>("HINMEI_BIKOU"),
                         KAISHUU_BIKOU = g.First().Field<string>("KAISHUU_BIKOU"),
                         DETAIL_SYSTEM_ID = g.First().Field<string>("DETAIL_SYSTEM_ID"),
                         NIOROSHI_NUMBER = g.First().Field<string>("NIOROSHI_NUMBER"),
                         HINMEI_CD = g.First().Field<string>("HINMEI_CD"),
                         KANSANCHI = g.First().Field<string>("KANSANCHI"),
                         KANSAN_UNIT_MOBILE_OUTPUT_FLG = g.First().Field<string>("KANSAN_UNIT_MOBILE_OUTPUT_FLG"),
                         UR_SH_NUMBER_NUMERIC = g.First().Field<string>("UR_SH_NUMBER_NUMERIC"),

                     }).ToList();

                #endregion
                List<ShushuIchiranData.Hinmei> HinmeiList = new List<ShushuIchiranData.Hinmei>();

                #region 品名情報設定
                // 品名CD＆単位CD
                foreach (var Heimei in HinMeiInfo)
                {
                    ShushuIchiranData.Hinmei pHinmei = new ShushuIchiranData.Hinmei();
                    // 確定
                    pHinmei.KakuteiFlg = Convert.ToBoolean(Heimei.KAKUTEI_FLG);
                    // 品名CD
                    pHinmei.HinmeiCD = Heimei.HINMEI_CD;
                    pHinmei.HinmeiName = Heimei.HINMEI_NAME;
                    // 数量
                    pHinmei.Suuryou = Heimei.SUURYOU;
                    // 単位
                    pHinmei.UnitCD = Heimei.UNIT_CD.ToString();
                    pHinmei.UnitName = Heimei.UNIT_NAME;
                    //換算後数量
                    pHinmei.KansangoSuuryou = Heimei.KANSAN_SUURYOU;
                    //換算後単位CD
                    pHinmei.KansangoUnitCD = Heimei.KANSAN_UNIT_CD.ToString();
                    // 換算後単位
                    pHinmei.KansangoUnitName = Heimei.KANSAN_UNIT_NAME;
                    // 按分後数
                    pHinmei.AnbungoSuuryou = Heimei.ANBUN_SUURYOU;
                    // 荷降No
                    pHinmei.NioroshiNo = Heimei.NIOROSHI_NUMBER;
                    //収集時間
                    pHinmei.SyuusyuuTime = Heimei.SHUUSHUU_TIME;

                    if (!string.IsNullOrEmpty(Heimei.SHUUSHUU_TIME.ToString()))
                    {
                        // DateTimeに変換
                        DateTime dateTime = DateTime.Parse(Heimei.SHUUSHUU_TIME);
                        // 収集時
                        pHinmei.SyuusyuuHour = dateTime.Hour.ToString();
                        // 収集分
                        pHinmei.SyuusyuuMin = dateTime.Minute.ToString();

                    }
                    else
                    {
                        // 収集時
                        pHinmei.SyuusyuuHour = string.Empty;
                        // 収集分
                        pHinmei.SyuusyuuMin = string.Empty;
                    }

                    // 品名備考
                    pHinmei.HinmeiMemo = Heimei.HINMEI_BIKOU;
                    // 収集備考
                    pHinmei.SyuusyuuMemo = Heimei.KAISHUU_BIKOU;
                    // 明細システムID
                    pHinmei.DetailSystemId = Heimei.DETAIL_SYSTEM_ID.ToString();

                    //品名と単位組み合わせ
                    if (!string.IsNullOrEmpty(Heimei.HINMEI_CD) && !string.IsNullOrEmpty(Heimei.UNIT_CD))
                    {
                        //品名と単位組み合わせ同時存在時は収集画面列作成。
                        pHinmei.HinmeiUnit = Heimei.HINMEI_CD + Heimei.UNIT_CD;
                    }
                    else
                    {
                        pHinmei.HinmeiUnit = string.Empty;
                    }

                    //品名と換算後単位組み合わせ
                    if (!string.IsNullOrEmpty(Heimei.HINMEI_CD) && !string.IsNullOrEmpty(Heimei.KANSAN_UNIT_CD))
                    {
                        //品名と換算後単位組み合わせ同時存在時は収集画面列作成。
                        pHinmei.HinmeiKansanUnit = Heimei.HINMEI_CD + Heimei.KANSAN_UNIT_CD;
                    }
                    else
                    {
                        pHinmei.HinmeiKansanUnit = string.Empty;
                    }

                    pHinmei.Kansanchi = Heimei.KANSANCHI;
                    if (SqlBoolean.False.ToString().Equals(Heimei.KANSAN_UNIT_MOBILE_OUTPUT_FLG))
                    {
                        pHinmei.KansanUnitMobileOutputFlg = false;
                    }
                    else
                    {
                        pHinmei.KansanUnitMobileOutputFlg = true;
                    }

                    pHinmei.UrShNumberNumeric = Heimei.UR_SH_NUMBER_NUMERIC;

                    //list作成
                    HinmeiList.Add(pHinmei);
                }
                //現場情報存在時は品名List作成
                pGenba.HinmeiList = HinmeiList;
                //現場List作成
                GenbaList.Add(pGenba);
                #endregion
            }
            #endregion
            #endregion

            //収集画面品名列情報作成
            //品名,単位毎
            var HeimeiColumnInfo = dtDetailIchiranData.AsEnumerable().GroupBy(r => r.Field<string>("HINMEIUNIT"),
                       (k, g) => new
                       {
                           HINMEIUNIT = k,
                           ROUND_NO = g.First().Field<string>("ROUND_NO"),
                           GYOUSHAGENBA = g.First().Field<string>("GYOUSHAGENBA"),
                           GYOUSHA_CD = g.First().Field<string>("GYOUSHA_CD"),
                           GENBA_CD = g.First().Field<string>("GENBA_CD"),
                           HINMEI_CD = g.First().Field<string>("HINMEI_CD"),
                           HINMEI_NAME = g.First().Field<string>("HINMEI_NAME_RYAKU"),
                           UNIT_CD = g.First().Field<string>("UNIT_CD"),
                           UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                           KANSAN_UNIT_CD = g.First().Field<string>("KANSAN_UNIT_CD"),
                           KANSAN_UNIT_NAME = g.First().Field<string>("UNITKANSAN_NAME"),
                           DENPYOU_KBN_CD = g.First().Field<string>("DENPYOU_KBN_CD"),
                       }).OrderBy(r => r.HINMEI_CD)
                                    .ThenBy(r => r.UNIT_CD)
                                    .ToList();

            List<ShushuIchiranData.HinmeiUnitColumn> ColumnInfoList = new List<ShushuIchiranData.HinmeiUnitColumn>();
            foreach (var ColumnNameInfo in HeimeiColumnInfo)
            {
                ShushuIchiranData.HinmeiUnitColumn pHinmeiColumn = new ShushuIchiranData.HinmeiUnitColumn();
                pHinmeiColumn.HinmeiUnit = ColumnNameInfo.HINMEIUNIT;
                pHinmeiColumn.GyoushaGenba = ColumnNameInfo.GYOUSHAGENBA;
                pHinmeiColumn.RoundNo = ColumnNameInfo.ROUND_NO;
                pHinmeiColumn.GyoushaCD = ColumnNameInfo.GYOUSHA_CD;
                pHinmeiColumn.GenbaCD = ColumnNameInfo.GENBA_CD;
                pHinmeiColumn.HinmeiCD = ColumnNameInfo.HINMEI_CD;
                pHinmeiColumn.HinmeiName = ColumnNameInfo.HINMEI_NAME;
                pHinmeiColumn.UnitCD = ColumnNameInfo.UNIT_CD;
                pHinmeiColumn.UnitName = ColumnNameInfo.UNIT_NAME;
                pHinmeiColumn.KansanUnitCD = ColumnNameInfo.KANSAN_UNIT_CD;
                pHinmeiColumn.KansanUnitName = ColumnNameInfo.KANSAN_UNIT_NAME;
                pHinmeiColumn.DenpyouKbnCd = ColumnNameInfo.DENPYOU_KBN_CD;
                pHinmeiColumn.IsUnit = true;

                ColumnInfoList.Add(pHinmeiColumn);
            }

            //品名,換算後単位毎
            var HeimeiKansanColumnInfo = dtDetailIchiranData.AsEnumerable().GroupBy(r => r.Field<string>("HINMEIKANSANUNIT"),
                       (k, g) => new
                       {
                           HINMEIUNIT = k,
                           ROUND_NO = g.First().Field<string>("ROUND_NO"),
                           GYOUSHAGENBA = g.First().Field<string>("GYOUSHAGENBA"),
                           GYOUSHA_CD = g.First().Field<string>("GYOUSHA_CD"),
                           GENBA_CD = g.First().Field<string>("GENBA_CD"),
                           HINMEI_CD = g.First().Field<string>("HINMEI_CD"),
                           HINMEI_NAME = g.First().Field<string>("HINMEI_NAME_RYAKU"),
                           UNIT_CD = g.First().Field<string>("UNIT_CD"),
                           UNIT_NAME = g.First().Field<string>("UNIT_NAME_RYAKU"),
                           KANSAN_UNIT_CD = g.First().Field<string>("KANSAN_UNIT_CD"),
                           KANSAN_UNIT_NAME = g.First().Field<string>("UNITKANSAN_NAME"),
                           DENPYOU_KBN_CD = g.First().Field<string>("DENPYOU_KBN_CD"),
                       }).OrderBy(r => r.HINMEI_CD)
                                    .ThenBy(r => r.UNIT_CD)
                                    .ToList();
            foreach (var ColumnNameInfo in HeimeiKansanColumnInfo)
            {
                if (ColumnInfoList.Any(n => n.GyoushaCD.Equals(ColumnNameInfo.GYOUSHA_CD) && n.GenbaCD.Equals(ColumnNameInfo.GENBA_CD)
                    && n.HinmeiCD.Equals(ColumnNameInfo.HINMEI_CD) && n.DispUnitCD().Equals(ColumnNameInfo.KANSAN_UNIT_CD)))
                {
                    // 同一の業者CD,現場CD,品名CD,単位CD(換算後単位CDも含む)がある場合、品名単位カラムは作成しない
                    continue;
                }

                ShushuIchiranData.HinmeiUnitColumn pHinmeiColumn = new ShushuIchiranData.HinmeiUnitColumn();
                pHinmeiColumn.HinmeiUnit = ColumnNameInfo.HINMEIUNIT;
                pHinmeiColumn.GyoushaGenba = ColumnNameInfo.GYOUSHAGENBA;
                pHinmeiColumn.RoundNo = ColumnNameInfo.ROUND_NO;
                pHinmeiColumn.GyoushaCD = ColumnNameInfo.GYOUSHA_CD;
                pHinmeiColumn.GenbaCD = ColumnNameInfo.GENBA_CD;
                pHinmeiColumn.HinmeiCD = ColumnNameInfo.HINMEI_CD;
                pHinmeiColumn.HinmeiName = ColumnNameInfo.HINMEI_NAME;
                pHinmeiColumn.UnitCD = ColumnNameInfo.UNIT_CD;
                pHinmeiColumn.UnitName = ColumnNameInfo.UNIT_NAME;
                pHinmeiColumn.KansanUnitCD = ColumnNameInfo.KANSAN_UNIT_CD;
                pHinmeiColumn.KansanUnitName = ColumnNameInfo.KANSAN_UNIT_NAME;
                pHinmeiColumn.DenpyouKbnCd = ColumnNameInfo.DENPYOU_KBN_CD;
                pHinmeiColumn.IsUnit = false;

                ColumnInfoList.Add(pHinmeiColumn);
            }

            //収集品名列作成
            //品名CD,単位CD順
            //品名CD,単位CDが重複する場合は、一列だけ作成する
            var HinmeiUnitList = ColumnInfoList.GroupBy(r => r.HinmeiUnit);
            var ColumnInfoListOrder = new List<ShushuIchiranData.HinmeiUnitColumn>();
            foreach (var hinmeiunit in HinmeiUnitList)
            {
                foreach (var ColumnInfo in ColumnInfoList)
                {
                    if (hinmeiunit.Key.Equals(ColumnInfo.HinmeiUnit))
                    {
                        ColumnInfoListOrder.Add(ColumnInfo);
                        break;
                    }
                }
            }
            this.mShushuIchiranData.HinmeiUnitInfoList = ColumnInfoListOrder.OrderBy(n => n.HinmeiCD).ThenBy(n => n.DispUnitCD()).ToList();
            //詳細一覧情報作成
            //this.mShushuIchiranData.GyoushaList = GyoushaList;
            this.mShushuIchiranData.GenbaList = GenbaList;

        }
        /// <summary>
        /// 品名GRID　＝＞品名データ作成
        /// </summary>
        /// <returns></returns>
        public DataTable DetailIchiranGridToDetailIchiranData(out bool catchErr)
        {
            DataTable dtDetailIchiranData = new DataTable();
            catchErr = false;
            try
            {
                DataGridViewColumn detailCol = null;
                for (int i = 0; i < this.form.DetailIchiran.Columns.Count; i++)
                {
                    detailCol = this.form.DetailIchiran.Columns[i];
                    dtDetailIchiranData.Columns.Add(detailCol.Name);
                }
                dtDetailIchiranData.Columns.Add("SHUUSHUU_TIME");
                dtDetailIchiranData.Columns.Remove("NIOROSHI_NUMBER_DETAIL");
                dtDetailIchiranData.Columns.Add("NIOROSHI_NUMBER");
                //dtDetailIchiranData.Columns.Add("UR_SH_NUMBER_NUMERIC");
                #region 定期実績明細テーブル


                DataGridViewRow detailRow = null;
                for (int i = 0; i < this.form.DetailIchiran.Rows.Count - 1; i++)
                {
                    detailRow = this.form.DetailIchiran.Rows[i];

                    DataRow dtRow = dtDetailIchiranData.NewRow();
                    if (detailRow.Cells["KAKUTEI_FLG"].Value == null || detailRow.Cells["KAKUTEI_FLG"].Value.ToString().Equals("0") || !(bool)detailRow.Cells["KAKUTEI_FLG"].Value)
                    {

                        dtRow["KAKUTEI_FLG"] = false;
                    }
                    else
                    {
                        dtRow["KAKUTEI_FLG"] = true;
                    }
                    // 明細システムID
                    dtRow["DETAIL_SYSTEM_ID"] = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.DETAIL_SYSTEM_ID]).ToString();

                    // 入力区分
                    dtRow["INPUT_KBN"] = this.GetCellValue(detailRow.Cells[ConstCls.DetailColName.INPUT_KBN]).ToString();

                    // 回数
                    dtRow["ROUND_NO"] = detailRow.Cells[ConstCls.DetailColName.ROUND_NO].FormattedValue.ToString();
                    // 業者CD
                    dtRow["GYOUSHA_CD"] = detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString();
                    // 業者
                    dtRow["GYOUSHA_NAME_RYAKU"] = detailRow.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].FormattedValue.ToString();
                    // 現場CD
                    dtRow["GENBA_CD"] = detailRow.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString();
                    // 現場
                    dtRow["GENBA_NAME_RYAKU"] = detailRow.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].FormattedValue.ToString();
                    // 品名CD
                    dtRow["HINMEI_CD"] = detailRow.Cells[ConstCls.DetailColName.HINMEI_CD].FormattedValue.ToString();
                    dtRow["HINMEI_NAME_RYAKU"] = detailRow.Cells[ConstCls.DetailColName.HINMEI_NAME_RYAKU].FormattedValue.ToString();
                    // 数量
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.SUURYOU].FormattedValue.ToString()))
                    {
                        dtRow["SUURYOU"] = detailRow.Cells[ConstCls.DetailColName.SUURYOU].FormattedValue.ToString();
                    }
                    else
                    {
                        dtRow["SUURYOU"] = string.Empty;
                    }
                    // 単位CD
                    if (detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value != null && !string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString()))
                    {
                        dtRow["UNIT_CD"] = detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString();
                        dtRow["UNIT_NAME_RYAKU"] = detailRow.Cells[ConstCls.DetailColName.UNIT_NAME_RYAKU].Value.ToString();
                    }
                    else
                    {
                        dtRow["UNIT_CD"] = string.Empty;
                        dtRow["UNIT_NAME_RYAKU"] = string.Empty;
                    }

                    // 換算後数量
                    dtRow["KANSAN_SUURYOU"] = detailRow.Cells[ConstCls.DetailColName.KANSAN_SUURYOU].FormattedValue.ToString();

                    // 実数
                    dtRow["ANBUN_FLG"] = detailRow.Cells["ANBUN_FLG"].FormattedValue.ToString();

                    // 換算後単位
                    dtRow["KANSAN_UNIT_CD"] = detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value != null ? detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value.ToString() : string.Empty;
                    dtRow["UNITKANSAN_NAME"] = detailRow.Cells[ConstCls.DetailColName.UNITKANSAN_NAME].Value != null ? detailRow.Cells[ConstCls.DetailColName.UNITKANSAN_NAME].Value.ToString() : string.Empty;

                    // 業者現場
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString())
                        && !string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString()))
                    {
                        dtRow["GYOUSHAGENBA"] = detailRow.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString()
                            + detailRow.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString();
                        detailRow.Cells["GYOUSHAGENBA"].Value = dtRow["GYOUSHAGENBA"].ToString();
                    }
                    else
                    {
                        dtRow["GYOUSHAGENBA"] = detailRow.Cells["GYOUSHAGENBA"].FormattedValue.ToString();
                    }


                    // 品名単位CD
                    if (detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value != null && !string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString())
                       && !string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.HINMEI_CD].FormattedValue.ToString()))
                    {
                        dtRow["HINMEIUNIT"] = detailRow.Cells[ConstCls.DetailColName.HINMEI_CD].FormattedValue.ToString()
                            + detailRow.Cells[ConstCls.DetailColName.UNIT_CD].Value.ToString();
                        detailRow.Cells["HINMEIUNIT"].Value = dtRow["HINMEIUNIT"].ToString();
                    }
                    else
                    {
                        dtRow["HINMEIUNIT"] = string.Empty;
                    }


                    // 品名換算後単位CD
                    if (detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value != null && !string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value.ToString())
                       && !string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.HINMEI_CD].FormattedValue.ToString()))
                    {
                        dtRow["HINMEIKANSANUNIT"] = detailRow.Cells[ConstCls.DetailColName.HINMEI_CD].FormattedValue.ToString()
                            + detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_CD].Value.ToString();
                        detailRow.Cells["HINMEIKANSANUNIT"].Value = dtRow["HINMEIKANSANUNIT"].ToString();
                    }
                    else
                    {
                        dtRow["HINMEIKANSANUNIT"] = string.Empty;
                    }

                    // 換算後単位モバイル出力フラグ
                    dtRow["KANSAN_UNIT_MOBILE_OUTPUT_FLG"] = detailRow.Cells[ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG].Value;

                    // 換算値
                    dtRow["KANSANCHI"] = detailRow.Cells[ConstCls.DetailColName.KANSANCHI].Value;

                    //按分数量
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.ANBUN_SUURYOU].FormattedValue.ToString()))
                    {
                        dtRow["ANBUN_SUURYOU"] = detailRow.Cells[ConstCls.DetailColName.ANBUN_SUURYOU].FormattedValue.ToString();
                    }
                    else
                    {
                        dtRow["ANBUN_SUURYOU"] = string.Empty;
                    }
                    // 荷降No
                    if (!string.IsNullOrEmpty(detailRow.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].FormattedValue.ToString()))
                    {
                        dtRow["NIOROSHI_NUMBER"] = detailRow.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].FormattedValue.ToString();
                    }
                    else
                    {
                        dtRow["NIOROSHI_NUMBER"] = string.Empty;
                    }
                    string hour = detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_HOUR].FormattedValue.ToString();
                    string minute = detailRow.Cells[ConstCls.DetailColName.SHUUSHUU_MIN].FormattedValue.ToString();
                    // 収集時刻
                    if (!string.IsNullOrEmpty(hour) ||
                        !string.IsNullOrEmpty(minute))
                    {
                        if (string.IsNullOrEmpty(hour))
                        {
                            hour = "0";
                        }
                        else if (string.IsNullOrEmpty(minute))
                        {
                            minute = "0";
                        }
                        dtRow["SHUUSHUU_TIME"] = DateTime.Parse(hour + ":" + minute);
                    }
                    else
                    {
                        dtRow["SHUUSHUU_TIME"] = string.Empty;
                    }
                    // 品名備考
                    dtRow["HINMEI_BIKOU"] = detailRow.Cells[ConstCls.DetailColName.HINMEI_BIKOU].FormattedValue.ToString();
                    // 収集備考
                    dtRow["KAISHUU_BIKOU"] = detailRow.Cells[ConstCls.DetailColName.KAISHUU_BIKOU].FormattedValue.ToString();
                    // 月極区分(集計単位)
                    dtRow["TSUKIGIME_KBN"] = detailRow.Cells["TSUKIGIME_KBN"].FormattedValue.ToString();
                    // 契約区分
                    dtRow["KEIYAKU_KBN"] = detailRow.Cells["KEIYAKU_KBN"].FormattedValue.ToString();
                    // 伝票区分
                    dtRow["DENPYOU_KBN_CD"] = detailRow.Cells["DENPYOU_KBN_CD"].FormattedValue.ToString();

                    // 売上支払番号
                    if (!string.IsNullOrEmpty(detailRow.Cells["UR_SH_NUMBER_NUMERIC"].FormattedValue.ToString()))
                    {
                        dtRow["UR_SH_NUMBER_NUMERIC"] = detailRow.Cells["UR_SH_NUMBER_NUMERIC"].FormattedValue.ToString();
                    }

                    dtDetailIchiranData.Rows.Add(dtRow);
                }

                this.CheckUrShNumberAndControlReadOnly();
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("DetailIchiranGridToDetailIchiranData", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
            }
            return dtDetailIchiranData;
        }
        /// <summary>
        /// テーブル作成
        /// </summary>
        /// <returns></returns>
        private DataTable GetTableConstructor()
        {
            DataTable dtDetailIchiranData = new DataTable();
            DataColumn detailCol = null;
            for (int i = 0; i < this.searchResultDetail.Columns.Count; i++)
            {
                detailCol = searchResultDetail.Columns[i];
                dtDetailIchiranData.Columns.Add(detailCol.Caption);
            }

            return dtDetailIchiranData;
        }
        /// <summary>
        /// 品名GRIDデータ設定
        /// </summary>
        /// <param name="isInit">画面初期表示時</param>
        public bool InitDetailIchiranGrid(bool isInit)
        {
            bool ret = true;
            try
            {
                string strDate;
                string strHour;
                string strMinute;

                var genbaTeikiHinmeiDao = DaoInitUtility.GetComponent<IM_GENBA_TEIKI_HINMEIDao>();

                #region Detail-Detail-1部項目
                // 検索結果（定期実績明細）
                searchResultDetail.BeginLoadData();

                // 明細クリア
                this.form.DetailIchiran.Rows.Clear();
                if (searchResultDetail.Rows.Count > 0)
                {
                    // 明細行を追加
                    this.form.DetailIchiran.Rows.Add(searchResultDetail.Rows.Count);
                    // 検索結果設定
                    for (int i = 0; i < searchResultDetail.Rows.Count; i++)
                    {
                        // 確定
                        this.form.DetailIchiran["KAKUTEI_FLG", i].Value = string.IsNullOrEmpty(searchResultDetail.Rows[i]["KAKUTEI_FLG"].ToString()) ? false : Convert.ToBoolean(searchResultDetail.Rows[i]["KAKUTEI_FLG"]);
                        // 入力区分
                        this.form.DetailIchiran[ConstCls.DetailColName.INPUT_KBN, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.INPUT_KBN], string.Empty);
                        if(searchResultDetail.Rows[i][ConstCls.DetailColName.INPUT_KBN].ToString() == "1")
                        {
                            this.form.DetailIchiran[ConstCls.DetailColName.INPUT_KBN_NAME, i].Value = ConstCls.INPUT_KBN_1;
                        }
                        else if(searchResultDetail.Rows[i][ConstCls.DetailColName.INPUT_KBN].ToString() == "2")
                        {
                            this.form.DetailIchiran[ConstCls.DetailColName.INPUT_KBN_NAME, i].Value = ConstCls.INPUT_KBN_2;
                            this.form.DetailIchiran[ConstCls.DetailColName.ROUND_NO, i].ReadOnly = true;
                            this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, i].ReadOnly = true;
                            this.form.DetailIchiran[ConstCls.DetailColName.GENBA_CD, i].ReadOnly = true;
                            this.form.DetailIchiran[ConstCls.DetailColName.HINMEI_CD, i].ReadOnly = true;
                            this.form.DetailIchiran[ConstCls.DetailColName.UNIT_CD, i].ReadOnly = true;
                        }
                        // 回数
                        this.form.DetailIchiran[ConstCls.DetailColName.ROUND_NO, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.ROUND_NO], string.Empty);
                        // 業者CD
                        this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.GYOUSHA_CD], string.Empty);
                        // 業者名
                        this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.GYOUSHA_NAME_RYAKU], string.Empty);
                        // 現場CD
                        this.form.DetailIchiran[ConstCls.DetailColName.GENBA_CD, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.GENBA_CD], string.Empty);
                        // 現場名
                        this.form.DetailIchiran[ConstCls.DetailColName.GENBA_NAME_RYAKU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.GENBA_NAME_RYAKU], string.Empty);
                        // 品名CD
                        this.form.DetailIchiran[ConstCls.DetailColName.HINMEI_CD, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.HINMEI_CD], string.Empty);
                        this.form.DetailIchiran[ConstCls.DetailColName.HINMEI_NAME_RYAKU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.HINMEI_NAME_RYAKU], string.Empty);

                        // 数量
                        this.form.DetailIchiran[ConstCls.DetailColName.SUURYOU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.SUURYOU], string.Empty);
                        // 単位
                        if (UnitCdExisted(searchResultDetail.Rows[i][ConstCls.DetailColName.UNIT_CD], isInit))
                        {
                            this.form.DetailIchiran[ConstCls.DetailColName.UNIT_CD, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.UNIT_CD], string.Empty);
                            this.form.DetailIchiran[ConstCls.DetailColName.UNIT_NAME_RYAKU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.UNIT_NAME_RYAKU], string.Empty);
                        }
                        else
                        {
                            // 存在しない単位CDの場合、表示上は空白(単位名称が空)となるため、空を設定する(モバイル対策)
                            this.form.DetailIchiran[ConstCls.DetailColName.UNIT_CD, i].Value = string.Empty;
                            this.form.DetailIchiran[ConstCls.DetailColName.UNIT_NAME_RYAKU, i].Value = string.Empty;
                        }

                        //換算後数量
                        this.form.DetailIchiran[ConstCls.DetailColName.KANSAN_SUURYOU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.KANSAN_SUURYOU], string.Empty);

                        // 実数
                        this.form.DetailIchiran["ANBUN_FLG", i].Value = searchResultDetail.Rows[i]["ANBUN_FLG"];

                        // 換算後単位
                        if (UnitCdExisted(searchResultDetail.Rows[i][ConstCls.DetailColName.KANSAN_UNIT_CD], isInit))
                        {
                            this.form.DetailIchiran[ConstCls.DetailColName.KANSAN_UNIT_CD, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.KANSAN_UNIT_CD], string.Empty);
                            this.form.DetailIchiran[ConstCls.DetailColName.UNITKANSAN_NAME, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.UNITKANSAN_NAME], string.Empty);
                        }
                        else
                        {
                            // 存在しない単位CDの場合、表示上は空白(単位名称が空)となるため、空を設定する(モバイル対策)
                            this.form.DetailIchiran[ConstCls.DetailColName.KANSAN_UNIT_CD, i].Value = string.Empty;
                            this.form.DetailIchiran[ConstCls.DetailColName.UNITKANSAN_NAME, i].Value = string.Empty;
                        }

                        // 換算後単位モバイル出力フラグ
                        this.form.DetailIchiran[ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.KANSAN_UNIT_MOBILE_OUTPUT_FLG], SqlBoolean.False).ToString();

                        // 換算値
                        this.form.DetailIchiran[ConstCls.DetailColName.KANSANCHI, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.KANSANCHI].ToString(), string.Empty);

                        // 按分後数
                        this.form.DetailIchiran[ConstCls.DetailColName.ANBUN_SUURYOU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.ANBUN_SUURYOU], string.Empty);
                        // 荷降No
                        this.form.DetailIchiran[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i]["NIOROSHI_NUMBER"], string.Empty);


                        if (this.TryChgDateTimeToDHM(searchResultDetail.Rows[i]["SHUUSHUU_TIME"], out strDate, out strHour, out strMinute))
                        {
                            // 収集時
                            this.form.DetailIchiran[ConstCls.DetailColName.SHUUSHUU_HOUR, i].Value = strHour;
                            // 収集分
                            this.form.DetailIchiran[ConstCls.DetailColName.SHUUSHUU_MIN, i].Value = strMinute;

                        }
                        // 品名備考
                        this.form.DetailIchiran[ConstCls.DetailColName.HINMEI_BIKOU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.HINMEI_BIKOU], string.Empty);
                        // 収集備考
                        this.form.DetailIchiran[ConstCls.DetailColName.KAISHUU_BIKOU, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.KAISHUU_BIKOU], string.Empty);
                        // 業者CDと現場CD
                        this.form.DetailIchiran["GYOUSHAGENBA", i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i]["GYOUSHAGENBA"], string.Empty);
                        // 品名CDと単位CD
                        this.form.DetailIchiran["HINMEIUNIT", i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i]["HINMEIUNIT"], string.Empty);
                        // 品名CDと換算後単位CD
                        this.form.DetailIchiran["HINMEIKANSANUNIT", i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i]["HINMEIKANSANUNIT"], string.Empty);


                        // 明細システムID
                        this.form.DetailIchiran[ConstCls.DetailColName.DETAIL_SYSTEM_ID, i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i][ConstCls.DetailColName.DETAIL_SYSTEM_ID], string.Empty);

                        // 契約区分
                        this.form.DetailIchiran["KEIYAKU_KBN", i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i]["KEIYAKU_KBN"], string.Empty);
                        // 契約区分名の表示を行う     
                        if (this.form.DetailIchiran["KEIYAKU_KBN", i].Value.ToString().Equals("1"))
                        {
                            this.form.DetailIchiran.Rows[i].Cells["KEIYAKU_KBN_NM"].Value = "定期";
                            //集計区分
                            this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN"].Value = string.Empty;
                            this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN_NM"].Value = string.Empty;
                            this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN"].ReadOnly = true;
                            this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText.Replace("※", "");

                        }
                        else if (this.form.DetailIchiran["KEIYAKU_KBN", i].Value.ToString().Equals("2"))
                        {
                            this.form.DetailIchiran.Rows[i].Cells["KEIYAKU_KBN_NM"].Value = "単価";
                            //集計区分
                            if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                            {
                                this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN"].ReadOnly = false;
                            }
                            this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText + "※";

                        }
                        else if (this.form.DetailIchiran["KEIYAKU_KBN", i].Value.ToString().Equals("3"))
                        {
                            this.form.DetailIchiran.Rows[i].Cells["KEIYAKU_KBN_NM"].Value = "回収のみ";
                            //集計区分
                            this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN"].Value = string.Empty;
                            this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN_NM"].Value = string.Empty;
                            this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN"].ReadOnly = true;
                            this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText.Replace("※", "");

                        }
                        //伝票区分
                        string strDENPYOU_KBN_CD = this.ChgDBNullToValue(searchResultDetail.Rows[i]["DENPYOU_KBN_CD"], string.Empty).ToString();
                        if (strDENPYOU_KBN_CD.Equals("1"))
                        {
                            this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD"].Value = strDENPYOU_KBN_CD;
                            this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD_NM"].Value = "売上";

                        }
                        else if (strDENPYOU_KBN_CD.Equals("2"))
                        {
                            this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD"].Value = strDENPYOU_KBN_CD;
                            this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD_NM"].Value = "支払";
                        }
                        else
                        {
                            this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD"].Value = string.Empty;
                            this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD_NM"].Value = string.Empty;
                        }

                        //// 月極区分
                        //this.form.DetailIchiran["TSUKIGIME_KBN", i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i]["TSUKIGIME_KBN"], string.Empty);

                        string strTSUKIGIME_KBN = this.ChgDBNullToValue(searchResultDetail.Rows[i]["TSUKIGIME_KBN"], string.Empty) == null ? string.Empty : this.ChgDBNullToValue(searchResultDetail.Rows[i]["TSUKIGIME_KBN"], string.Empty).ToString();

                        if (!string.IsNullOrEmpty(strTSUKIGIME_KBN))
                        {
                            if ("1".Equals(strTSUKIGIME_KBN))
                            {
                                // 集計単位
                                this.form.DetailIchiran["TSUKIGIME_KBN", i].Value = "1";
                                this.form.DetailIchiran["TSUKIGIME_KBN_NM", i].Value = "伝票";
                            }
                            else if ("2".Equals(strTSUKIGIME_KBN))
                            {
                                // 集計単位
                                this.form.DetailIchiran["TSUKIGIME_KBN", i].Value = "2";
                                this.form.DetailIchiran["TSUKIGIME_KBN_NM", i].Value = "合算";
                            }
                            else
                            {
                                // 集計単位
                                this.form.DetailIchiran["TSUKIGIME_KBN", i].Value = string.Empty;
                                this.form.DetailIchiran["TSUKIGIME_KBN_NM", i].Value = string.Empty;
                            }
                        }

                        this.form.DetailIchiran["UR_SH_NUMBER_NUMERIC", i].Value = this.ChgDBNullToValue(searchResultDetail.Rows[i]["UR_SH_NUMBER_NUMERIC"], string.Empty);

                    }

                    this.CheckUrShNumberAndControlReadOnly();
                }
                #endregion
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitDetailIchiranGrid", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 指定された単位
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="isCheckDeleted">削除済みデータもチェックするかのフラグ</param>
        /// <returns></returns>
        private bool UnitCdExisted(object cell, bool isCheckDeleted)
        {
            string unitCd = this.ChgDBNullToValue(cell, string.Empty).ToString();

            foreach (M_UNIT unit in this.mUnitAll)
            {
                if (unit.UNIT_CD.ToString().Equals(unitCd))
                {
                    if (!isCheckDeleted && unit.DELETE_FLG.IsFalse)
                    {
                        return true;
                    }
                    else if (isCheckDeleted)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 収集GRIDデータ設定
        /// </summary>
        public bool InitShushuIchiranGrid()
        {
            bool ret = true;
            try
            {
                ShushuIchiranData data = this.mShushuIchiranData;

                //収集毎画面初期化
                this.ClearShushuIchiranGrid();

                //品名と単位組み合わせにつて収集画面の品名列作成（明細欄）
                if (this.mShushuIchiranData.HinmeiUnitInfoList != null)
                {
                    //列追加
                    foreach (ShushuIchiranData.HinmeiUnitColumn HeimeiUnitInfo in this.mShushuIchiranData.HinmeiUnitInfoList)
                    {
                        if (!string.IsNullOrEmpty(HeimeiUnitInfo.HinmeiUnit) && !"1".Equals(HeimeiUnitInfo.HinmeiUnit))
                        {
                            AddColumn(HeimeiUnitInfo, this.form.syuusyuuDetailIchiran);
                            AddColumn(HeimeiUnitInfo, this.form.syuusyuuDetailIchiranSoukei);
                            AddColumn(HeimeiUnitInfo, this.form.syuusyuuDetailIchiranScroll);
                        }
                    }
                }

                #region 収集Gridデータ初期化設定
                //if (this.mShushuIchiranData.GyoushaList.Count > 0)
                if (this.mShushuIchiranData.GenbaList.Count > 0)
                {
                    //foreach (ShushuIchiranData.Gyousha gyoushaData in this.mShushuIchiranData.GyoushaList)
                    //{
                    //    foreach (ShushuIchiranData.Genba GenbaData in gyoushaData.GenbaList)
                        foreach (ShushuIchiranData.Genba GenbaData in this.mShushuIchiranData.GenbaList)
                        {
                            this.form.syuusyuuDetailIchiran.Rows.Add();
                            if (this.form.syuusyuuDetailIchiranSoukei.Rows.Count < 1)
                            {
                                this.form.syuusyuuDetailIchiranSoukei.Rows.Add();
                                // 選択されているセルをなくす
                                this.form.syuusyuuDetailIchiranSoukei.CurrentCell = null;
                            }
                            DataGridViewRow newRow = this.form.syuusyuuDetailIchiran.Rows[this.form.syuusyuuDetailIchiran.Rows.Count - 1];
                            DataGridViewRow newRowSoukei = this.form.syuusyuuDetailIchiranSoukei.Rows[this.form.syuusyuuDetailIchiranSoukei.Rows.Count - 1];

                            // 業者現場
                            newRow.Cells["clmGYOUSHAGENBA"].Value = GenbaData.GyoushaGenba;
                            // 回数
                            newRow.Cells["clmROUND_NO"].Value = GenbaData.RoundNo;
                            // 業者CD
                            newRow.Cells["clmGYOUSHA_CD"].Value = GenbaData.GyoushaCD;
                            // 業者名
                            newRow.Cells["clmGYOUSHA_NAME_RYAKU"].Value = GenbaData.GyoushaName;
                            // 現場CD
                            newRow.Cells["clmGENBA_CD"].Value = GenbaData.GenbaCD;
                            // 現場名
                            newRow.Cells["clmGENBA_NAME_RYAKU"].Value = GenbaData.GenbaName;
                            // 収集備考
                            newRow.Cells["clmKAISHUU_BIKOU"].Value = GenbaData.SyuusyuuMemo;
                            //収集GRIDセール背景色設定
                            foreach (ShushuIchiranData.HinmeiUnitColumn HinmeiData in this.mShushuIchiranData.HinmeiUnitInfoList)
                            {
                                if (!string.IsNullOrEmpty(HinmeiData.HinmeiUnit))
                                {
                                    newRow.Cells[HinmeiData.HinmeiUnit].Style.BackColor = Constans.READONLY_COLOR;
                                    newRow.Cells[HinmeiData.HinmeiUnit].ReadOnly = true;
                                    newRowSoukei.Cells[HinmeiData.HinmeiUnit].Style.BackColor = Constans.READONLY_COLOR;
                                    newRowSoukei.Cells[HinmeiData.HinmeiUnit].ReadOnly = true;
                                }
                            }
                            // 確定
                            bool clmOk2Value = false;
                            // 収集時間
                            string ShuushuuTimeValue = string.Empty;
                            foreach (ShushuIchiranData.Hinmei HinmeiData in GenbaData.HinmeiList)
                            {
                                if (!string.IsNullOrEmpty(HinmeiData.HinmeiUnit))
                                {
                                    decimal iSUURYOU = 0;
                                    if (!string.IsNullOrEmpty(HinmeiData.Suuryou))
                                    {
                                        iSUURYOU = decimal.Parse(HinmeiData.Suuryou);
                                        //数量
                                        newRow.Cells[HinmeiData.HinmeiUnit].Value = this.SuuryouAndTankFormat(iSUURYOU, systemSuuryouFormat);//HinmeiData.Suuryou;
                                    }
                                    //収集GRID 数量セルの背景色、ReadOnlyの設定
                                    if (HinmeiData.KakuteiFlg)
                                    {
                                        newRow.Cells[HinmeiData.HinmeiUnit].Style.BackColor = Constans.READONLY_COLOR;
                                        newRow.Cells[HinmeiData.HinmeiUnit].ReadOnly = true;
                                    }
                                    else
                                    {
                                        newRow.Cells[HinmeiData.HinmeiUnit].Style.BackColor = Constans.NOMAL_COLOR;
                                        newRow.Cells[HinmeiData.HinmeiUnit].ReadOnly = false;
                                    }
                                    mCellAriList.Add(newRow.Cells[HinmeiData.HinmeiUnit]);
                                    // 収集備考
                                    newRow.Cells["clmKAISHUU_BIKOU"].Value = HinmeiData.SyuusyuuMemo;

                                    // 確定
                                    clmOk2Value = HinmeiData.KakuteiFlg;

                                    // 収集時間
                                    if (string.IsNullOrEmpty(ShuushuuTimeValue)
                                        || (!string.IsNullOrEmpty(HinmeiData.SyuusyuuTime) && DateTime.Parse(ShuushuuTimeValue).CompareTo(DateTime.Parse(HinmeiData.SyuusyuuTime)) < 0))
                                    {
                                        ShuushuuTimeValue = HinmeiData.SyuusyuuTime;
                                    }
                                }

                                if (!string.IsNullOrEmpty(HinmeiData.HinmeiKansanUnit))
                                {
                                    decimal iKANSANSUURYOU = 0;
                                    if (!string.IsNullOrEmpty(HinmeiData.KansangoSuuryou))
                                    {
                                        iKANSANSUURYOU = decimal.Parse(HinmeiData.KansangoSuuryou);

                                        // 換算後数量
                                        newRow.Cells[HinmeiData.HinmeiKansanUnit].Value = this.SuuryouAndTankFormat(iKANSANSUURYOU, systemSuuryouFormat);
                                    }
                                    // 収集GRID 数量セル背景色、ReadOnlyの設定
                                    if (HinmeiData.KakuteiFlg)
                                    {
                                        newRow.Cells[HinmeiData.HinmeiKansanUnit].Style.BackColor = Constans.READONLY_COLOR;
                                        newRow.Cells[HinmeiData.HinmeiKansanUnit].ReadOnly = true;
                                    }
                                    else
                                    {
                                        newRow.Cells[HinmeiData.HinmeiKansanUnit].Style.BackColor = Constans.NOMAL_COLOR;
                                        newRow.Cells[HinmeiData.HinmeiKansanUnit].ReadOnly = false;
                                    }
                                    mCellAriList.Add(newRow.Cells[HinmeiData.HinmeiKansanUnit]);
                                }
                            }
                            // 確定
                            if (clmOk2Value)
                            {
                                newRow.Cells["clmOk2"].Value = clmOk2Value;
                            }
                            // 収集時間
                            if (!string.IsNullOrEmpty(ShuushuuTimeValue))
                            {
                                // DateTimeに変換
                                DateTime dateTime = DateTime.Parse(ShuushuuTimeValue);
                                // 収集時
                                newRow.Cells["clmSHUUSHUU_HOUR"].Value = dateTime.Hour.ToString();
                                // 収集分
                                newRow.Cells["clmSHUUSHUU_MIN"].Value = dateTime.Minute.ToString();
                            }
                        }
                    //}
                }
                #endregion

                // 初期カーソル位置を予めセット
                this.form.syuusyuuDetailIchiran.CurrentCell = this.form.syuusyuuDetailIchiran.Rows[0].Cells["clmROUND_NO"];
                // 平行スクロールバー連動不備の対策(#100865)
                // 垂直スクロールバーが表示される場合、その領域分、平行スクロールがずれる。
                if (this.form.syuusyuuDetailIchiran.Controls[1].Visible)
                {
                    // 垂直スクロールバーの有無を確認し、ダミーカラムを追加
                    DgvCustomNumericTextBox2Column textColumn = new DgvCustomNumericTextBox2Column();
                    textColumn.Width = this.form.syuusyuuDetailIchiran.Controls[1].Width;
                    textColumn.ReadOnly = true;
                    this.form.syuusyuuDetailIchiranScroll.Columns.Add(textColumn);
                    DgvCustomNumericTextBox2Column textColumn2 = new DgvCustomNumericTextBox2Column();
                    textColumn2.Width = this.form.syuusyuuDetailIchiran.Controls[1].Width;
                    textColumn2.ReadOnly = true;
                    this.form.syuusyuuDetailIchiranSoukei.Columns.Add(textColumn2);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitShushuIchiranGrid", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 明細欄（総計）をセット
        /// </summary>
        internal bool SetSoukei()
        {
            bool ret = true;
            try
            {
                decimal sum = 0;

                foreach (DataGridViewColumn c in this.form.syuusyuuDetailIchiran.Columns.Cast<DataGridViewColumn>().Where(c => c.Index > 10))
                {
                    // 初期化
                    sum = 0;
                    string clmInsertName = string.Empty;

                    // 列毎の合計を計算
                    foreach (DataGridViewRow r in this.form.syuusyuuDetailIchiran.Rows)
                    {
                        if (r.Cells[c.Name].Value != null && !string.IsNullOrWhiteSpace(r.Cells[c.Name].Value.ToString()))
                        {
                            sum = sum + decimal.Parse(r.Cells[c.Name].Value.ToString());
                            clmInsertName = c.Name;
                        }
                    }
                    // 値を明細欄（総計）にセット
                    this.form.syuusyuuDetailIchiranSoukei[c.Name, 0].Value = this.SuuryouAndTankFormat(sum, systemSuuryouFormat);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSoukei", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 収集画面初期化
        /// </summary>
        public void ClearShushuIchiranGrid()
        {
            //収集毎画面初期化
            this.form.syuusyuuDetailIchiran.Rows.Clear();
            if (this.form.syuusyuuDetailIchiran.Columns.Count > SyuusyuuColumnIndex)
            {
                int deletCount = this.form.syuusyuuDetailIchiran.Columns.Count - 1;

                for (int colIndex = deletCount; colIndex >= SyuusyuuColumnIndex; colIndex--)
                {
                    this.form.syuusyuuDetailIchiran.Columns.RemoveAt(colIndex);
                }
            }

            //収集毎総計初期化
            this.form.syuusyuuDetailIchiranSoukei.Rows.Clear();
            if (this.form.syuusyuuDetailIchiranSoukei.Columns.Count > SyuusyuuColumnIndex)
            {
                int deletCount = this.form.syuusyuuDetailIchiranSoukei.Columns.Count - 1;

                for (int colIndex = deletCount; colIndex >= SyuusyuuColumnIndex; colIndex--)
                {
                    this.form.syuusyuuDetailIchiranSoukei.Columns.RemoveAt(colIndex);
                }
            }

            //20150925 hoanghm #13137 start
            this.form.syuusyuuDetailIchiranScroll.Rows.Clear();
            if (this.form.syuusyuuDetailIchiranScroll.Columns.Count > SyuusyuuColumnIndex)
            {
                int deletCount = this.form.syuusyuuDetailIchiranScroll.Columns.Count - 1;

                for (int colIndex = deletCount; colIndex >= SyuusyuuColumnIndex; colIndex--)
                {
                    this.form.syuusyuuDetailIchiranScroll.Columns.RemoveAt(colIndex);
                }
            }
            //20150925 hoanghm #13137 end
        }

        /// <summary>
        /// DataGridView列を追加する
        /// </summary>
        /// <param name="unitColumn">品名情報</param>
        /// <param name="dgv">追加先のDataGridView</param>
        private void AddColumn(ShushuIchiranData.HinmeiUnitColumn unitColumn, DataGridView dgv)
        {
            //DataGridViewTextBoxColumn列を作成する
            DgvCustomNumericTextBox2Column textColumn = new DgvCustomNumericTextBox2Column();
            //データソースの"Column1"をバインドする
            textColumn.DataPropertyName = unitColumn.HinmeiUnit;
            //名前とヘッダーを設定する
            textColumn.Name = unitColumn.HinmeiUnit;
            textColumn.HeaderText = unitColumn.HinmeiName + "\r\n" + unitColumn.DispUnitName();
            //ソート設定
            textColumn.SortMode = DataGridViewColumnSortMode.NotSortable;
            //自動サイズ設定
            textColumn.Resizable = DataGridViewTriState.True;
            textColumn.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //設定          
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UIForm));
            textColumn.FocusOutCheckMethod = ((System.Collections.ObjectModel.Collection<r_framework.Dto.SelectCheckDto>)(resources.GetObject(unitColumn.HinmeiUnit + ".FocusOutCheckMethod")));
            //サイズ設定
            textColumn.Width = 150;

            textColumn.FormatSetting = "システム設定(数量書式)";

            var rangeSettingDto = new RangeSettingDto();
            rangeSettingDto.Max = new decimal(new int[] {
            1215752191,
            23,
            0,
            196608});
            textColumn.RangeSetting = rangeSettingDto;

            //単位の取得先を保持
            textColumn.Tag = unitColumn;

            //列を追加する
            dgv.Columns.Add(textColumn);
        }
        /// <summary>
        /// 収集画面の変更データを品名データに更新する。
        /// </summary>
        /// <returns></returns>
        public DataTable ShushuIchiranGridToDetailIchiranData(out bool catchErr)
        {
            //空テーブル作成
            DataTable dtDetailIchiranData = this.searchResultDetail;
            catchErr = false;
            try
            {
                DataGridViewRow shushuRow = null;
                List<DataRow> NewRowList = new List<DataRow>();
                for (int i = 0; i < this.form.syuusyuuDetailIchiran.Rows.Count; i++)
                {
                    //一行データ取得
                    shushuRow = this.form.syuusyuuDetailIchiran.Rows[i];
                    string jyouken = string.Empty;
                    DataRow[] dtRows = null;

                    foreach (DataGridViewColumn shushuCol in this.form.syuusyuuDetailIchiran.Columns)
                    {

                        if (shushuCol.Index >= SyuusyuuColumnIndex)
                        {
                            var hinmeiColumn = shushuCol.Tag as ShushuIchiranData.HinmeiUnitColumn;
                            if (hinmeiColumn == null)
                            {
                                continue;
                            }
                            foreach (DataRow dtRow in dtDetailIchiranData.Rows)
                            {
                                if (dtRow["ROUND_NO"].ToString().Equals(this.GetCellValue(shushuRow.Cells["clmROUND_NO"]).ToString()))
                                {
                                    if (dtRow["GYOUSHAGENBA"].ToString().Equals(this.GetCellValue(shushuRow.Cells["clmGYOUSHAGENBA"]).ToString()))
                                    {
                                        if (!string.IsNullOrEmpty(dtRow["HINMEIUNIT"].ToString())
                                            && dtRow["HINMEIUNIT"].ToString().Equals(shushuCol.Name)
                                            && hinmeiColumn.IsUnit)
                                        {
                                            //更新データ
                                            dtRow["ROUND_NO"] = this.GetCellValue(shushuRow.Cells["clmROUND_NO"]).ToString();
                                            //dtRow["INPUT_KBN"] = this.GetCellValue(shushuRow.Cells["clmINPUT_KBN"]).ToString();
                                            dtRow["GYOUSHA_CD"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_CD"]).ToString();
                                            dtRow["GYOUSHA_NAME_RYAKU"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_NAME_RYAKU"]).ToString();
                                            dtRow["GENBA_CD"] = this.GetCellValue(shushuRow.Cells["clmGENBA_CD"]).ToString();
                                            dtRow["GENBA_NAME_RYAKU"] = this.GetCellValue(shushuRow.Cells["clmGENBA_NAME_RYAKU"]).ToString();
                                            dtRow["KAISHUU_BIKOU"] = this.GetCellValue(shushuRow.Cells["clmKAISHUU_BIKOU"]).ToString();
                                            dtRow["GYOUSHAGENBA"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_CD"]).ToString() + this.GetCellValue(shushuRow.Cells["clmGENBA_CD"]).ToString();
                                            //dtRow["DENPYOU_KBN_CD"] = hinmeiColumn.DenpyouKbnCd;
                                            // 収集時刻
                                            dtRow["SHUUSHUU_HOUR"] = this.GetCellValue(shushuRow.Cells["clmSHUUSHUU_HOUR"]);
                                            dtRow["SHUUSHUU_MIN"] = this.GetCellValue(shushuRow.Cells["clmSHUUSHUU_MIN"]);
                                            SetShuushuuTime(dtRow);
                                            dtRow["SUURYOU"] = this.GetCellValue(shushuRow.Cells[shushuCol.Index]).ToString();
                                            // 同一品名について、換算単位が存在する場合
                                            if (!string.IsNullOrEmpty(hinmeiColumn.KansanUnitCD.ToString()))
                                            {
                                                dtRow["KANSAN_SUURYOU"] = this.GetCellValue(shushuRow.Cells[hinmeiColumn.HinmeiCD + hinmeiColumn.KansanUnitCD]).ToString();
                                                if (shushuRow.Cells[hinmeiColumn.HinmeiCD + hinmeiColumn.KansanUnitCD].ReadOnly)
                                                {
                                                    dtRow["KANSAN_UNIT_MOBILE_OUTPUT_FLG"] = SqlBoolean.False.ToString();
                                                }
                                                else
                                                {
                                                    dtRow["KANSAN_UNIT_MOBILE_OUTPUT_FLG"] = SqlBoolean.True.ToString();
                                                }
                                            }
                                        }
                                        else if (!string.IsNullOrEmpty(dtRow["HINMEIKANSANUNIT"].ToString())
                                                 && dtRow["HINMEIKANSANUNIT"].ToString().Equals(shushuCol.Name)
                                                 && !hinmeiColumn.IsUnit)
                                        {
                                            //更新データ
                                            dtRow["ROUND_NO"] = this.GetCellValue(shushuRow.Cells["clmROUND_NO"]).ToString();
                                            //dtRow["INPUT_KBN"] = this.GetCellValue(shushuRow.Cells["clmINPUT_KBN"]).ToString();
                                            dtRow["GYOUSHA_CD"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_CD"]).ToString();
                                            dtRow["GYOUSHA_NAME_RYAKU"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_NAME_RYAKU"]).ToString();
                                            dtRow["GENBA_CD"] = this.GetCellValue(shushuRow.Cells["clmGENBA_CD"]).ToString();
                                            dtRow["GENBA_NAME_RYAKU"] = this.GetCellValue(shushuRow.Cells["clmGENBA_NAME_RYAKU"]).ToString();
                                            dtRow["KAISHUU_BIKOU"] = this.GetCellValue(shushuRow.Cells["clmKAISHUU_BIKOU"]).ToString();
                                            dtRow["GYOUSHAGENBA"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_CD"]).ToString() + this.GetCellValue(shushuRow.Cells["clmGENBA_CD"]).ToString();
                                            //dtRow["DENPYOU_KBN_CD"] = hinmeiColumn.DenpyouKbnCd;
                                            dtRow["SHUUSHUU_HOUR"] = this.GetCellValue(shushuRow.Cells["clmSHUUSHUU_HOUR"]);
                                            dtRow["SHUUSHUU_MIN"] = this.GetCellValue(shushuRow.Cells["clmSHUUSHUU_MIN"]);
                                            SetShuushuuTime(dtRow);
                                            dtRow["KANSAN_SUURYOU"] = this.GetCellValue(shushuRow.Cells[shushuCol.Index]).ToString();
                                            if (shushuRow.Cells[shushuCol.Index].ReadOnly)
                                            {
                                                dtRow["KANSAN_UNIT_MOBILE_OUTPUT_FLG"] = SqlBoolean.False.ToString();
                                            }
                                            else
                                            {
                                                dtRow["KANSAN_UNIT_MOBILE_OUTPUT_FLG"] = SqlBoolean.True.ToString();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        if (shushuCol.Index >= SyuusyuuColumnIndex && !string.IsNullOrEmpty(this.GetCellValue(shushuRow.Cells[shushuCol.Name])))
                        {
                            var hinmeiColumn = shushuCol.Tag as ShushuIchiranData.HinmeiUnitColumn;
                            if (hinmeiColumn == null)
                            {
                                continue;
                            }

                            string whereUnit = string.Empty;

                            if (hinmeiColumn.IsUnit)
                            {
                                whereUnit = "' AND  HINMEIUNIT = '";
                            }
                            else
                            {
                                whereUnit = "' AND  HINMEIKANSANUNIT = '";
                            }
                            jyouken = " GYOUSHAGENBA = '" + hinmeiColumn.GyoushaGenba + whereUnit + shushuCol.Name + "' ";

                            dtRows = dtDetailIchiranData.Select(jyouken);
                            if (dtRows.Length <= 0)
                            {
                                // 新規追加行に含まれているか(単位、換算後単位同時入力時)
                                var newList = NewRowList.Where(n => n["ROUND_NO"].Equals(this.GetCellValue(shushuRow.Cells["clmROUND_NO"]).ToString())
                                                                 && n["GYOUSHA_CD"].Equals(this.GetCellValue(shushuRow.Cells["clmGYOUSHA_CD"]).ToString())
                                                                 && n["GENBA_CD"].Equals(this.GetCellValue(shushuRow.Cells["clmGENBA_CD"]).ToString())
                                                                 && n["HINMEI_CD"].Equals(shushuCol.Name.Substring(0, 6)))
                                                        .ToList()
                                                        .FirstOrDefault();
                                if (newList == null)
                                {
                                    // 行追加
                                    DataRow newRow = dtDetailIchiranData.NewRow();
                                    newRow["ROUND_NO"] = this.GetCellValue(shushuRow.Cells["clmROUND_NO"]).ToString();
                                    newRow["GYOUSHA_CD"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_CD"]).ToString();
                                    newRow["GYOUSHA_NAME_RYAKU"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_NAME_RYAKU"]).ToString();
                                    newRow["GENBA_CD"] = this.GetCellValue(shushuRow.Cells["clmGENBA_CD"]).ToString();
                                    newRow["GENBA_NAME_RYAKU"] = this.GetCellValue(shushuRow.Cells["clmGENBA_NAME_RYAKU"]).ToString();
                                    newRow["KAISHUU_BIKOU"] = this.GetCellValue(shushuRow.Cells["clmKAISHUU_BIKOU"]).ToString();
                                    newRow["GYOUSHAGENBA"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_CD"]).ToString() + this.GetCellValue(shushuRow.Cells["clmGENBA_CD"]).ToString();
                                    newRow["DENPYOU_KBN_CD"] = hinmeiColumn.DenpyouKbnCd;
                                    newRow["SHUUSHUU_HOUR"] = this.GetCellValue(shushuRow.Cells["clmSHUUSHUU_HOUR"]);
                                    newRow["SHUUSHUU_MIN"] = this.GetCellValue(shushuRow.Cells["clmSHUUSHUU_MIN"]);
                                    SetShuushuuTime(newRow);

                                    // 品名CD
                                    newRow["HINMEI_CD"] = shushuCol.Name.Substring(0, 6);
                                    newRow["HINMEI_NAME_RYAKU"] = shushuCol.HeaderText.Replace("\r\n", ":").Split(':')[0];
                                    if (hinmeiColumn.IsUnit)
                                    {
                                        // 単位CD
                                        newRow["UNIT_CD"] = shushuCol.Name.Substring(6);
                                        newRow["UNIT_NAME_RYAKU"] = shushuCol.HeaderText.Replace("\r\n", ":").Split(':')[1];

                                        newRow["SUURYOU"] = this.GetCellValue(shushuRow.Cells[shushuCol.Index]).ToString();
                                    }
                                    else
                                    {
                                        // 換算後単位CD
                                        newRow["KANSAN_UNIT_CD"] = shushuCol.Name.Substring(6);
                                        newRow["UNITKANSAN_NAME"] = shushuCol.HeaderText.Replace("\r\n", ":").Split(':')[1];

                                        newRow["KANSAN_SUURYOU"] = this.GetCellValue(shushuRow.Cells[shushuCol.Index]).ToString();
                                    }

                                    NewRowList.Add(newRow);
                                }
                                else
                                {
                                    // 行更新
                                    if (hinmeiColumn.IsUnit)
                                    {
                                        // 単位CD
                                        newList["UNIT_CD"] = shushuCol.Name.Substring(6);
                                        newList["UNIT_NAME_RYAKU"] = shushuCol.HeaderText.Replace("\r\n", ":").Split(':')[1];

                                        newList["SUURYOU"] = this.GetCellValue(shushuRow.Cells[shushuCol.Index]).ToString();
                                    }
                                    else
                                    {
                                        // 換算後単位CD
                                        newList["KANSAN_UNIT_CD"] = shushuCol.Name.Substring(6);
                                        newList["UNITKANSAN_NAME"] = shushuCol.HeaderText.Replace("\r\n", ":").Split(':')[1];

                                        newList["KANSAN_SUURYOU"] = this.GetCellValue(shushuRow.Cells[shushuCol.Index]).ToString();
                                    }
                                }
                            }
                        }
                    }
                    jyouken = " GYOUSHAGENBA = '" + this.GetCellValue(shushuRow.Cells["clmGYOUSHAGENBA"]).ToString() + "'";
                    jyouken += " AND ROUND_NO = '" + this.GetCellValue(shushuRow.Cells["clmROUND_NO"]).ToString() + "'";
                    dtRows = dtDetailIchiranData.Select(jyouken);
                    var newRowCount = NewRowList.Where(n => n["ROUND_NO"].Equals(this.GetCellValue(shushuRow.Cells["clmROUND_NO"]).ToString())
                                                         && n["GYOUSHA_CD"].Equals(this.GetCellValue(shushuRow.Cells["clmGYOUSHA_CD"]).ToString())
                                                         && n["GENBA_CD"].Equals(this.GetCellValue(shushuRow.Cells["clmGENBA_CD"]).ToString()))
                                                .ToList().Count;
                    if (dtRows.Length <= 0 && newRowCount <= 0)
                    {
                        DataRow newRow = dtDetailIchiranData.NewRow();
                        newRow["ROUND_NO"] = this.GetCellValue(shushuRow.Cells["clmROUND_NO"]).ToString();
                        newRow["GYOUSHA_CD"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_CD"]).ToString();
                        newRow["GYOUSHA_NAME_RYAKU"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_NAME_RYAKU"]).ToString();
                        newRow["GENBA_CD"] = this.GetCellValue(shushuRow.Cells["clmGENBA_CD"]).ToString();
                        newRow["GENBA_NAME_RYAKU"] = this.GetCellValue(shushuRow.Cells["clmGENBA_NAME_RYAKU"]).ToString();
                        newRow["KAISHUU_BIKOU"] = this.GetCellValue(shushuRow.Cells["clmKAISHUU_BIKOU"]).ToString();
                        newRow["GYOUSHAGENBA"] = this.GetCellValue(shushuRow.Cells["clmGYOUSHA_CD"]).ToString() + this.GetCellValue(shushuRow.Cells["clmGENBA_CD"]).ToString();
                        newRow["SHUUSHUU_HOUR"] = this.GetCellValue(shushuRow.Cells["clmSHUUSHUU_HOUR"]);
                        newRow["SHUUSHUU_MIN"] = this.GetCellValue(shushuRow.Cells["clmSHUUSHUU_MIN"]);
                        SetShuushuuTime(newRow);
                        dtDetailIchiranData.Rows.Add(newRow);
                    }

                }
                dtDetailIchiranData.AcceptChanges();
                //新規行追加
                if (NewRowList.Count > 0)
                {
                    foreach (DataRow newRow in NewRowList)
                    {
                        dtDetailIchiranData.Rows.Add(newRow);
                    }
                }
                dtDetailIchiranData.AcceptChanges();
                //収集毎に存在しない業者CDと現場CDが、品名毎に存在する場合は、品名毎から削除する
                int[] idxArray = new int[dtDetailIchiranData.Rows.Count];
                for (int i = 0; i < dtDetailIchiranData.Rows.Count; i++)
                    idxArray[i] = -1;
                int idx = 0;
                foreach (DataRow dtRow in dtDetailIchiranData.Rows)
                {
                    string gyoshagenbaCD = string.Empty;
                    if (dtRow["GYOUSHAGENBA"] != null)
                        gyoshagenbaCD = dtRow["GYOUSHAGENBA"].ToString();
                    bool hinmeiflg = false;
                    foreach (DataGridViewRow shushuData in this.form.syuusyuuDetailIchiran.Rows)
                    {
                        string gyoshaCDSS = string.Empty;
                        string genbaCDSS = string.Empty;
                        if (shushuData.Cells["clmGYOUSHA_CD"].Value != null)
                            gyoshaCDSS = shushuData.Cells["clmGYOUSHA_CD"].Value.ToString();
                        if (shushuData.Cells["clmGENBA_CD"].Value != null)
                            genbaCDSS = shushuData.Cells["clmGENBA_CD"].Value.ToString();
                        if (gyoshagenbaCD == gyoshaCDSS + genbaCDSS)
                        {
                            hinmeiflg = true;
                            break;
                        }
                    }
                    if (this.form.syuusyuuDetailIchiran.Rows.Count <= 0)
                    {
                        // 収集毎が既にクリアされている場合は、品名毎は一律全て表示
                        hinmeiflg = true;
                    }
                    if (!hinmeiflg && !string.IsNullOrEmpty(gyoshagenbaCD))
                    {
                        //収集毎に存在しない業者CDと現場CDのある行のインデックスを取得
                        idxArray[idx] = dtDetailIchiranData.Rows.IndexOf(dtRow);
                        idx++;
                    }
                }
                for (int i = 0; i < dtDetailIchiranData.Rows.Count; i++)
                    if (idxArray[i] >= 0)
                        //品名データから削除
                        dtDetailIchiranData.Rows.RemoveAt(idxArray[0]);
                dtDetailIchiranData.AcceptChanges();

                this.searchResultDetail = dtDetailIchiranData;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ShushuIchiranGridToDetailIchiranData", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                catchErr = true;
            }
            finally
            {
            }
            return dtDetailIchiranData;
        }

        /// <summary>
        /// 収集時、収集分から収集時刻を設定
        /// </summary>
        /// <param name="row"></param>
        private void SetShuushuuTime(DataRow row)
        {
            string hour = this.ChgDBNullToValue(row["SHUUSHUU_HOUR"], string.Empty).ToString();
            string minute = this.ChgDBNullToValue(row["SHUUSHUU_MIN"], string.Empty).ToString();

            // 収集時刻
            if (!string.IsNullOrEmpty(hour) ||
                !string.IsNullOrEmpty(minute))
            {
                if (string.IsNullOrEmpty(hour))
                {
                    hour = "0";
                }
                else if (string.IsNullOrEmpty(minute))
                {
                    minute = "0";
                }
                row["SHUUSHUU_TIME"] = DateTime.Parse(hour + ":" + minute);
            }
            else
            {
                row["SHUUSHUU_TIME"] = string.Empty;
            }
        }
        #endregion

        #region 車輌チェック
        /// <summary>
        /// 車輌チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChechSharyouCd()
        {
            bool returnVal = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 20141015 koukouei 休動管理機能 start
                /*// 前回値と変わらない場合は処理を行わない
                if (true == this.oldSharyouCD.Equals(this.form.SHARYOU_CD.Text))
                {
                    // 処理終了
                    returnVal = true;
                    return returnVal;
                }*/
                // 20141015 koukouei 休動管理機能 end

                // 車輌名をクリア
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;

                // 入力されてない場合
                if (String.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
                {
                    // 処理終了
                    returnVal = true;
                    return returnVal;
                }

                // 車輌情報取得
                var sharyou = this.GetSharyou(this.form.SHARYOU_CD.Text);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (sharyou == null)
                {
                    // 背景色変更
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    // メッセージ表示
                    msgLogic.MessageBoxShow("E020", "車輌");
                    this.isInputError = true;
                    return returnVal;
                }

                // 20141015 koukouei 休動管理機能 start
                // 休動チェック
                if (!sharyou.DELETE_FLG.IsTrue && !this.ChkSharyouWordClose(true))
                {
                    return returnVal;
                }
                // 20141015 koukouei 休動管理機能 end

                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text) && !sharyou.SHASYU_CD.Equals(this.form.SHASHU_CD.Text))
                {
                    // 背景色変更
                    this.form.SHARYOU_CD.IsInputErrorOccured = true;
                    // メッセージ表示
                    msgLogic.MessageBoxShow("E104", "車輌", "車種");
                    this.isInputError = true;
                    return returnVal;
                }


                // 車輌名設定
                this.form.SHARYOU_NAME_RYAKU.Text = sharyou.SHARYOU_NAME_RYAKU;

                // 車種入力されてない場合
                if (string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    // 車種情報取得
                    var shashu = this.GetSharshu(sharyou.SHASYU_CD);
                    if (shashu != null)
                    {
                        // 車種情報設定
                        this.form.SHASHU_CD.Text = shashu.SHASHU_CD;
                        this.form.SHASHU_NAME_RYAKU.Text = shashu.SHASHU_NAME_RYAKU;
                    }
                }

                // 運転者入力されてない場合
                if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    // 社員情報取得
                    var shain = this.GetShain(sharyou.SHAIN_CD);
                    if (shain != null)
                    {
                        // 運転者情報設定
                        this.form.UNTENSHA_CD.Text = shain.SHAIN_CD;
                        this.form.UNTENSHA_NAME.Text = shain.SHAIN_NAME_RYAKU;
                    }
                }

                // 運搬業者が入力されてない場合
                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    // 業者情報取得
                    var gyousha = this.GetGyousha(sharyou.GYOUSHA_CD);
                    if (gyousha != null)
                    {
                        // 業者情報設定
                        this.form.UNPAN_GYOUSHA_CD.Text = gyousha.GYOUSHA_CD;
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }

                // 処理終了
                returnVal = true;
                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechSharyouCd", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 車輌情報取得
        /// </summary>
        /// <param name="sharyouCd">車輌CD</param>
        /// <returns></returns>
        public M_SHARYOU GetSharyou(string sharyouCd)
        {
            M_SHARYOU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(sharyouCd);

                if (string.IsNullOrEmpty(sharyouCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHARYOU keyEntity = new M_SHARYOU();
                if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    keyEntity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                }
                keyEntity.SHARYOU_CD = sharyouCd;
                // 車種入力されている場合
                if (!string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                {
                    keyEntity.SHASYU_CD = this.form.SHASHU_CD.Text;
                }
                SqlDateTime sagyouDate = SqlDateTime.Null;
                if(!string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                {
                    sagyouDate = SqlDateTime.Parse(this.form.SAGYOU_DATE.Value.ToString());
                }

                // [運搬業者CD,車輌CD,車種CD]でM_SHARYOUを検索する
                var returnEntitys = sharyouDao.GetAllValidDataForGyoushaKbn(keyEntity, "9", sagyouDate, true, false, false);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    if (returnEntitys.Length == 1)
                    {
                        // 1件
                        returnVal = returnEntitys[0];
                    }
                    else
                    {
                        // ヒット数が複数件の場合、ポップアップ表示
                        this.form.SHARYOU_CD.Focus();
                        this.isCalledSharyouPopupFromLogic = true;
                        // 検索ポップアップ起動
                        this.PopUpConditionsSharyouSwitch(true);
                        var result = CustomControlExtLogic.PopUp(this.form.SHARYOU_CD);
                        this.PopUpConditionsSharyouSwitch(false);

                        // 車輌名を一旦クリアしているので入力されている場合は選択されたとする
                        if (!string.IsNullOrEmpty(this.form.SHARYOU_NAME_RYAKU.Text))
                        {
                            returnVal = new M_SHARYOU();
                            returnVal.SHARYOU_NAME_RYAKU = this.form.SHARYOU_NAME_RYAKU.Text;
                            returnVal.SHASYU_CD = this.form.SHASHU_CD.Text;
                            returnVal.SHAIN_CD = this.form.UNTENSHA_CD.Text;
                            returnVal.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                            // 20141015 koukouei 休動管理機能 start
                            returnVal.DELETE_FLG = true;
                            // 20141015 koukouei 休動管理機能 end
                        }
                        else
                        {
                            // 返却値は空白をセット
                            returnVal = new M_SHARYOU();
                            returnVal.SHARYOU_NAME_RYAKU = "";
                            returnVal.SHASYU_CD = "";
                            returnVal.SHAIN_CD = "";
                            returnVal.GYOUSHA_CD = "";
                            // 20141015 koukouei 休動管理機能 start
                            returnVal.DELETE_FLG = true;
                            // 20141015 koukouei 休動管理機能 end
                        }
                    }
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharyou", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 車輌PopUpの検索条件に車輌CDを含めるかを引数によって設定します
        /// </summary>
        /// <param name="isPopupConditionsSharyouCD"></param>
        internal void PopUpConditionsSharyouSwitch(bool isPopupConditionsSharyouCD)
        {
            PopupSearchSendParamDto sharyouParam = new PopupSearchSendParamDto();
            sharyouParam.And_Or = CONDITION_OPERATOR.AND;
            sharyouParam.Control = "SHARYOU_CD";
            sharyouParam.KeyName = "key002";

            if (isPopupConditionsSharyouCD)
            {
                if (!this.form.SHARYOU_CD.PopupSearchSendParams.Contains(sharyouParam))
                {
                    this.form.SHARYOU_CD.PopupSearchSendParams.Add(sharyouParam);
                }
            }
            else
            {
                var paramsCount = this.form.SHARYOU_CD.PopupSearchSendParams.Count;
                for (int i = 0; i < paramsCount; i++)
                {
                    if (this.form.SHARYOU_CD.PopupSearchSendParams[i].Control == "SHARYOU_CD" &&
                        this.form.SHARYOU_CD.PopupSearchSendParams[i].KeyName == "key002")
                    {
                        this.form.SHARYOU_CD.PopupSearchSendParams.RemoveAt(i);
                    }
                }
            }
        }

        /// <summary>
        /// 車種情報取得
        /// </summary>
        /// <param name="shashuCd">車種CD</param>
        /// <returns></returns>
        public M_SHASHU GetSharshu(string shashuCd)
        {
            M_SHASHU returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shashuCd);

                if (string.IsNullOrEmpty(shashuCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHASHU keyEntity = new M_SHASHU();
                keyEntity.SHASHU_CD = shashuCd;

                // [車種CD]でM_SHASHUを検索する
                var returnEntitys = this.shashuDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = returnEntitys[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSharshu", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 社員情報取得
        /// </summary>
        /// <param name="shainCd">社員CD</param>
        /// <returns></returns>
        public M_SHAIN GetShain(string shainCd)
        {
            M_SHAIN returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(shainCd);

                if (string.IsNullOrEmpty(shainCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_SHAIN keyEntity = new M_SHAIN();
                keyEntity.SHAIN_CD = shainCd;
                keyEntity.UNTEN_KBN = true;

                // [社員CD,運転者フラグ=true]でM_SHAINを検索する
                var returnEntitys = this.shainDao.GetAllValidData(keyEntity);
                if (returnEntitys != null && returnEntitys.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = returnEntitys[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetShain", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 業者取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GYOUSHA GetGyousha(string gyoushaCd)
        {
            M_GYOUSHA returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd);

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);

                if (gyousha != null && gyousha.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = gyousha[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGyousha", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }

        /// <summary>
        /// 現場取得
        /// </summary>
        /// <param name="gyoushaCd"></param>
        /// <returns></returns>
        public M_GENBA GetGenba(string gyoushaCd, string genbaCd)
        {
            M_GENBA returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(gyoushaCd, genbaCd);

                if (string.IsNullOrEmpty(gyoushaCd) || string.IsNullOrEmpty(genbaCd))
                {
                    return returnVal;
                }

                // 検索条件設定
                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity);

                if (genba != null && genba.Length > 0)
                {
                    // PK指定のため1件
                    returnVal = genba[0];
                }

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetGEnba", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 単価、数量の共通フォーマット
        /// <summary>
        /// 単価、数量の共通フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public string SuuryouAndTankFormat(object num, String format)
        {
            string returnVal = string.Empty;
            try
            {
                LogUtility.DebugMethodStart(num, format);

                returnVal = string.Format("{0:" + format + "}", num);

                return returnVal;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
        }
        #endregion

        #region 単位CD入力チェック処理
        /// <summary>
        /// 単位品名CD入力チェック処理
        /// </summary>
        /// <returns>bool(OK:true NG:false)</returns>
        private bool IsUnitChkOK(DataGridViewRow dgvRow, string argUnitCd, string UnitName)
        {
            bool result = true;
            try
            {
                LogUtility.DebugMethodStart(dgvRow, argUnitCd, UnitName);
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 単位略名をクリア
                dgvRow.Cells[UnitName].Value = string.Empty;

                // 未入力の場合、処理中止
                if (dgvRow.Cells[argUnitCd].Value == null ||
                    string.IsNullOrEmpty(dgvRow.Cells[argUnitCd].Value.ToString()))
                {
                    return result;
                }

                // 単位CDを取得
                string unitCD = dgvRow.Cells[argUnitCd].Value.ToString();

                // DBにて該当単位を検索
                M_UNIT srchEntity = new M_UNIT();
                try
                {
                    srchEntity.UNIT_CD = Int16.Parse(unitCD);
                }
                catch
                {
                    ControlUtility.SetInputErrorOccuredForDgvCell(dgvRow.Cells[argUnitCd], true);
                    // メッセージを表示し、処理中止
                    msgLogic.MessageBoxShow("E020", "単位");
                    result = false;
                    return result;
                }
                var entity = this.unitDao.GetAllValidData(srchEntity);
                // 存在しない場合
                if (entity == null || entity.Length == 0)
                {

                    ControlUtility.SetInputErrorOccuredForDgvCell(dgvRow.Cells[argUnitCd], true);
                    // メッセージを表示し、処理中止
                    msgLogic.MessageBoxShow("E020", "単位");
                    result = false;
                    return result;
                }

                // 単位略名を設定
                dgvRow.Cells[UnitName].Value = entity[0].UNIT_NAME_RYAKU;

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
        }

        #region 品名CD入力チェック＆名称設定処理

        /// <summary>
        /// 指定された行の品名CDで存在チェックおよび名称の設定を行います
        /// </summary>
        /// <param name="row">DataGridViewの対象行</param>
        /// <returns></returns>
        private bool CheckAndSettingHinmei(DataGridViewRow row)
        {
            try
            {
                LogUtility.DebugMethodStart(row);

                // 品名をクリア
                row.Cells[ConstCls.DetailColName.HINMEI_NAME_RYAKU].Value = string.Empty;

                // 未入力チェック
                if (row.Cells[ConstCls.DetailColName.HINMEI_CD].Value == null ||
                    string.IsNullOrEmpty(row.Cells[ConstCls.DetailColName.HINMEI_CD].Value.ToString()))
                {
                    return true;
                }

                // 品名CD
                string hinmeiCd = row.Cells[ConstCls.DetailColName.HINMEI_CD].FormattedValue.ToString();

                // DB検索(削除データ除外)
                M_HINMEI[] datas = DaoInitUtility.GetComponent<IM_HINMEIDao>().GetAllValidData(new M_HINMEI { HINMEI_CD = hinmeiCd });
                if (datas == null || datas.Length < 1)
                {
                    // 存在しないデータのためエラー
                    ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[ConstCls.DetailColName.HINMEI_CD], true);
                    new MessageBoxShowLogic().MessageBoxShow("E020", "品名");
                    this.isInputError = true;
                    return false;
                }

                // 品名を設定
                row.Cells[ConstCls.DetailColName.HINMEI_NAME_RYAKU].Value = datas[0].HINMEI_NAME_RYAKU;

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

        #region DataGridViewのCellValidating処理
        /// <summary>
        /// DataGridViewのCellValidating処理
        /// </summary>
        /// <param name="e"></param>
        /// <returns>bool</returns>
        internal bool DataGridViewCellValidating(DataGridViewCellValidatingEventArgs e)
        {
            bool result = true;
            try
            {
                LogUtility.DebugMethodStart(e);

                DataGridViewRow row = this.form.DetailIchiran.Rows[e.RowIndex];

                // 下記列の場合
                switch (this.form.DetailIchiran.Columns[e.ColumnIndex].Name)
                {
                    // 単位CDの場合
                    case ConstCls.DetailColName.UNIT_CD:
                        // 単位CDのチェック結果を返す
                        result = this.IsUnitChkOK(row, ConstCls.DetailColName.UNIT_CD, ConstCls.DetailColName.UNIT_NAME_RYAKU);
                        break;
                    //  換算後単位CDの場合
                    case ConstCls.DetailColName.KANSAN_UNIT_CD:
                        // 単位CDのチェック結果を返す
                        result = this.IsUnitChkOK(row, ConstCls.DetailColName.KANSAN_UNIT_CD, ConstCls.DetailColName.UNITKANSAN_NAME);
                        break;
                    case ConstCls.DetailColName.HINMEI_CD:
                        // 品名CDのチェック結果を返す
                        result = CheckAndSettingHinmei(row);
                        break;
                    case ConstCls.DetailColName.KEIYAKU_KBN:
                        string keiyakuKbn = this.GetCellValue(row.Cells["KEIYAKU_KBN"]);
                        string inputKbn = this.GetCellValue(row.Cells["INPUT_KBN"]);
                        if (keiyakuKbn == "1" && inputKbn != "2")
                        {
                            MessageBoxShowLogic msg = new MessageBoxShowLogic();
                            row.Cells["KEIYAKU_KBN_NM"].Value = string.Empty;
                            row.Cells["TSUKIGIME_KBN"].Value = string.Empty;
                            row.Cells["TSUKIGIME_KBN_NM"].Value = string.Empty;
                            row.Cells["TSUKIGIME_KBN"].ReadOnly = true;
                            msg.MessageBoxShow("E020", "契約区分");
                            this.isInputError = true;
                            return false;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DataGridViewCellValidating", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result);
            }
            return result;
        }
        #endregion

        #endregion

        #region コース操作時処理
        /// <summary>
        /// 定期配車入力にデータが存在するかを判定します
        /// </summary>
        /// <param name="courseNameCd">コース名CD</param>
        /// <returns>True：存在する　False：存在しない</returns>
        //internal bool IsExistTeikiHaisha(string courseNameCd)
        //{
        //    bool result = true;
        //    try
        //    {
        //        LogUtility.DebugMethodStart(courseNameCd);

        //        string sql = "SELECT COUNT(COURSE_NAME_CD) AS COUNT FROM T_TEIKI_HAISHA_ENTRY WHERE DELETE_FLG = 0 AND COURSE_NAME_CD = '" + courseNameCd + "'";
        //        DataTable dt = this.teikiHaishaEntryDao.GetDateForStringSql(sql);
        //        int count = 0;
        //        if (int.TryParse(dt.Rows[0]["COUNT"].ToString(), out count))
        //        {
        //            if (count == 0)
        //            {
        //                result = false;
        //            }
        //        }
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("IsExistTeikiHaisha", ex);
        //        throw;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd(courseNameCd);
        //    }
        //}

        /// <summary>
        ///  コースマスタ情報を一覧に反映します
        /// </summary>
        /// <param name="courseNameCd">コース名称CD</param>
        /// <param name="sagyouDate">作業日(適用基準日)</param>
        internal void SetCourseInfo(string courseNameCd, string sagyouDate, out bool catchErr)
        {
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart(courseNameCd, sagyouDate);
                var messageShowLogic = new MessageBoxShowLogic();

                // 作業日が未入力の場合エラー
                if (string.IsNullOrEmpty(sagyouDate))
                {
                    messageShowLogic.MessageBoxShow("E012", "作業日");
                    return;
                }

                // 曜日変換
                DateTime dateTime = DateTime.Parse(sagyouDate);
                int dayCd = DateUtility.GetShougunDayOfWeek(dateTime);

                if (this.form.FURIKAE_HAISHA_KBN.Text == "2")
                {
                    dayCd = 0;
                    switch (this.form.DAY_NM.Text)
                    {
                        case "月":
                            dayCd = 1;
                            break;
                        case "火":
                            dayCd = 2;
                            break;
                        case "水":
                            dayCd = 3;
                            break;
                        case "木":
                            dayCd = 4;
                            break;
                        case "金":
                            dayCd = 5;
                            break;
                        case "土":
                            dayCd = 6;
                            break;
                        case "日":
                            dayCd = 7;
                            break;
                    }
                }
                // コースマスタ情報取得
                DataTable courseDt = this.GetCourse(dayCd, courseNameCd);
                DataTable courseNioroshiDt = this.GetNioroshiIchiranFromCourse(dayCd, courseNameCd);
                DataTable courseDetailDt = this.GetDetailIchiranFromCourse(dayCd, courseNameCd, sagyouDate);

                // 該当データが無かった場合処理中断
                if (courseDt.Rows.Count == 0 || courseDetailDt.Rows.Count == 0)
                {
                    return;
                }

                // 確認メッセージ表示
                if (messageShowLogic.MessageBoxShow("C046", "コース入力の設定を反映します。\r\n既にあるデータを更新") == DialogResult.Yes)
                {
                    // コース情報設定
                    this.AllControlLock(true);
                    this.SetCourse(courseDt);
                    this.SetNioroshiIchiranFromCourse(courseNioroshiDt);
                    this.SetDetailIchiranFromCourse(courseDetailDt);
                    this.form.DAY_CD.Text = dayCd.ToString();
                    // 20141015 koukouei 休動管理機能 start
                    var msgLogic = new MessageBoxShowLogic();
                    if (!ChkSharyouWordClose(false))
                    {
                        string date = string.Format("作業日：{0}", Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd"));
                        msgLogic.MessageBoxShow("E208", "コース番号：", this.form.COURSE_NAME_CD.Text, "車輛", date);
                        this.form.SHARYOU_CD.Focus();
                        return;
                    }

                    if (!ChkUntenshaWordClose(false))
                    {
                        string date = string.Format("作業日：{0}", Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd"));
                        msgLogic.MessageBoxShow("E208", "コース番号：", this.form.COURSE_NAME_CD.Text, "運転者", date);
                        this.form.UNTENSHA_CD.Focus();
                        return;
                    }

                    foreach (DataGridViewRow row in this.form.NioroshiIchiran.Rows)
                    {
                        if (!ChkGenbaWordClose(row, false))
                        {
                            string date = string.Format("作業日：{0}", Convert.ToDateTime(this.form.SAGYOU_DATE.Text).ToString("yyyy/MM/dd"));
                            msgLogic.MessageBoxShow("E208", "コース番号：", this.form.COURSE_NAME_CD.Text, "荷降現場", date);
                            this.form.NioroshiIchiran.CurrentCell = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD];
                            this.form.NioroshiIchiran.Focus();
                            return;
                        }
                    }
                    // 20141015 koukouei 休動管理機能 end

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetCourseInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(courseNameCd, sagyouDate, catchErr);
            }
        }

        /// <summary>
        /// コースマスタから荷降一覧用データを取得します
        /// </summary>
        /// <param name="dayCd">曜日CD</param>
        /// <param name="courseNameCd">コース名称CD</param>
        /// <returns></returns>
        internal DataTable GetCourse(int dayCd, string courseNameCd)
        {
            try
            {
                LogUtility.DebugMethodStart(dayCd, courseNameCd);
                DTOClass searchDto = new DTOClass();
                searchDto.dayCd = dayCd;
                searchDto.courseNameCd = courseNameCd;

                return courseDao.GetCourseData(searchDto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetCourse", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dayCd, courseNameCd);
            }
        }

        /// <summary>
        /// コースマスタから荷降一覧用データを取得します
        /// </summary>
        /// <param name="dayCd">曜日CD</param>
        /// <param name="courseNameCd">コース名称CD</param>
        /// <returns></returns>
        internal DataTable GetNioroshiIchiranFromCourse(int dayCd, string courseNameCd)
        {
            try
            {
                LogUtility.DebugMethodStart(dayCd, courseNameCd);
                DTOClass searchDto = new DTOClass();
                searchDto.dayCd = dayCd;
                searchDto.courseNameCd = courseNameCd;

                return courseDao.GetCourseNioroshiData(searchDto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetNioroshiIchiranFromCourse", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dayCd, courseNameCd);
            }
        }

        /// <summary>
        /// コースマスタから明細一覧用データを取得します
        /// </summary>
        /// <param name="dayCd">曜日CD</param>
        /// <param name="courseNameCd">コース名称CD</param>
        /// <param name="sagyouDate">作業日</param>
        /// <returns></returns>
        internal DataTable GetDetailIchiranFromCourse(int dayCd, string courseNameCd, string sagyouDate)
        {
            try
            {
                LogUtility.DebugMethodStart(dayCd, courseNameCd, sagyouDate);
                DTOClass searchDto = new DTOClass();
                searchDto.dayCd = dayCd;
                searchDto.courseNameCd = courseNameCd;
                searchDto.tekiyouDate = sagyouDate;

                return courseDao.GetCourseDetailData(searchDto);
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetDetailIchiranFromCourse", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dayCd, courseNameCd, sagyouDate);
            }
        }

        /// <summary>
        /// コースマスタ情報を設定します
        /// </summary>
        /// <param name="dt">コースマスタ検索結果</param>
        internal void SetCourse(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }

                DataRow row = dt.Rows[0];
                this.form.UNPAN_GYOUSHA_CD.Text = Convert.ToString(row["UNPAN_GYOUSHA_CD"]);
                this.form.SHASHU_CD.Text = Convert.ToString(row["SHASHU_CD"]);
                this.form.SHARYOU_CD.Text = Convert.ToString(row["SHARYOU_CD"]);
                this.form.UNTENSHA_CD.Text = Convert.ToString(row["UNTENSHA_CD"]);
                this.form.HOJOIN_CD.Text = string.Empty;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetCourse", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dt);
            }
        }

        /// <summary>
        /// コースマスタ情報を荷降一覧に設定します
        /// </summary>
        /// <param name="dt">荷降一覧用コースマスタ検索結果</param>
        internal void SetNioroshiIchiranFromCourse(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }

                // 明細行を追加
                this.form.NioroshiIchiran.Rows.Clear();
                this.form.NioroshiIchiran.Rows.Add(dt.Rows.Count);
                // 検索結果設定
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 荷降No
                    this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_NUMBER, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.NioroshiColName.NIOROSHI_NUMBER], string.Empty);
                    // 荷降業者CD
                    this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD], string.Empty);
                    // 荷降業者名
                    this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU], string.Empty);
                    // 荷降現場CD
                    this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], string.Empty);
                    // 荷降現場名
                    this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU], string.Empty);
                    // 荷降量
                    this.form.NioroshiIchiran[ConstCls.NioroshiColName.NIOROSHI_RYOU, i].Value = string.Empty;
                    // 単位
                    this.form.NioroshiIchiran["UNIT", i].Value = "kg";
                    // TIME_STAMP
                    this.form.NioroshiIchiran[ConstCls.NioroshiColName.TIME_STAMP_NIOROSHI, i].Value = string.Empty;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetNioroshiIchiranFromCourse", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dt);
            }
        }

        /// <summary>
        /// コースマスタ情報を明細一覧に設定します
        /// </summary>
        /// <param name="dt">明細一覧用コースマスタ検索結果</param>
        internal void SetDetailIchiranFromCourse(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                if (dt == null || dt.Rows.Count == 0)
                {
                    return;
                }

                var genbaTeikiHinmeiDao = DaoInitUtility.GetComponent<IM_GENBA_TEIKI_HINMEIDao>();

                // 明細行を追加
                this.form.DetailIchiran.Rows.Clear();
                this.form.DetailIchiran.Rows.Add(dt.Rows.Count);
                // 検索結果設定
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 確定
                    this.form.DetailIchiran["KAKUTEI_FLG", i].Value = false;
                    // 回数
                    this.form.DetailIchiran[ConstCls.DetailColName.ROUND_NO, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.ROUND_NO], string.Empty);
                    // 業者CD
                    this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_CD, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.GYOUSHA_CD], string.Empty);
                    // 業者名
                    this.form.DetailIchiran[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.GYOUSHA_NAME_RYAKU], string.Empty);
                    // 現場CD
                    this.form.DetailIchiran[ConstCls.DetailColName.GENBA_CD, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.GENBA_CD], string.Empty);
                    // 現場名
                    this.form.DetailIchiran[ConstCls.DetailColName.GENBA_NAME_RYAKU, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.GENBA_NAME_RYAKU], string.Empty);
                    // 品名
                    this.form.DetailIchiran[ConstCls.DetailColName.HINMEI_CD, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.HINMEI_CD], string.Empty);
                    this.form.DetailIchiran[ConstCls.DetailColName.HINMEI_NAME_RYAKU, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.HINMEI_NAME_RYAKU], string.Empty);
                    // 数量
                    this.form.DetailIchiran[ConstCls.DetailColName.SUURYOU, i].Value = string.Empty;
                    // 単位
                    this.form.DetailIchiran[ConstCls.DetailColName.UNIT_CD, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.UNIT_CD], string.Empty);
                    this.form.DetailIchiran[ConstCls.DetailColName.UNIT_NAME_RYAKU, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.UNIT_NAME_RYAKU], string.Empty);
                    //換算後数量
                    this.form.DetailIchiran[ConstCls.DetailColName.KANSAN_SUURYOU, i].Value = string.Empty;
                    //換算後単位CD
                    this.form.DetailIchiran[ConstCls.DetailColName.KANSAN_UNIT_CD, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.KANSAN_UNIT_CD], string.Empty);
                    // 換算後単位
                    this.form.DetailIchiran[ConstCls.DetailColName.UNITKANSAN_NAME, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.UNITKANSAN_NAME], string.Empty);
                    // 按分後数
                    this.form.DetailIchiran[ConstCls.DetailColName.ANBUN_SUURYOU, i].Value = string.Empty;
                    // 荷降No
                    this.form.DetailIchiran[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL], string.Empty);
                    // 品名備考
                    this.form.DetailIchiran[ConstCls.DetailColName.HINMEI_BIKOU, i].Value = this.ChgDBNullToValue(dt.Rows[i][ConstCls.DetailColName.HINMEI_BIKOU], string.Empty);
                    // 収集備考
                    this.form.DetailIchiran[ConstCls.DetailColName.KAISHUU_BIKOU, i].Value = string.Empty;
                    // 業者CDと現場CD
                    this.form.DetailIchiran["GYOUSHAGENBA", i].Value = string.Empty;
                    // 品名CDと単位CD
                    this.form.DetailIchiran["HINMEIUNIT", i].Value = string.Empty;
                    // 明細システムID
                    this.form.DetailIchiran[ConstCls.DetailColName.DETAIL_SYSTEM_ID, i].Value = string.Empty;
                    // 契約区分
                    this.form.DetailIchiran["KEIYAKU_KBN", i].Value = this.ChgDBNullToValue(dt.Rows[i]["KEIYAKU_KBN"], string.Empty);
                    // 契約区分名の表示を行う     
                    if (this.form.DetailIchiran["KEIYAKU_KBN", i].Value.ToString().Equals("1"))
                    {
                        this.form.DetailIchiran.Rows[i].Cells["KEIYAKU_KBN_NM"].Value = "定期";
                        //集計区分
                        this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN"].Value = string.Empty;
                        this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN_NM"].Value = string.Empty;
                        this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN"].ReadOnly = true;
                        this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText.Replace("※", "");

                    }
                    else if (this.form.DetailIchiran["KEIYAKU_KBN", i].Value.ToString().Equals("2"))
                    {
                        this.form.DetailIchiran.Rows[i].Cells["KEIYAKU_KBN_NM"].Value = "単価";
                        //集計区分
                        if (this.form.WindowType != WINDOW_TYPE.DELETE_WINDOW_FLAG)
                        {
                            this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN"].ReadOnly = false;
                        }
                        this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText + "※";

                    }
                    else if (this.form.DetailIchiran["KEIYAKU_KBN", i].Value.ToString().Equals("3"))
                    {
                        this.form.DetailIchiran.Rows[i].Cells["KEIYAKU_KBN_NM"].Value = "回収のみ";
                        //集計区分
                        this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN"].Value = string.Empty;
                        this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN_NM"].Value = string.Empty;
                        this.form.DetailIchiran.Rows[i].Cells["TSUKIGIME_KBN"].ReadOnly = true;
                        this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText = this.form.DetailIchiran.Columns["TSUKIGIME_KBN"].HeaderText.Replace("※", "");

                    }
                    //伝票区分
                    string strDENPYOU_KBN_CD = this.ChgDBNullToValue(dt.Rows[i]["DENPYOU_KBN_CD"], string.Empty).ToString();
                    if (strDENPYOU_KBN_CD.Equals("1"))
                    {
                        this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD"].Value = strDENPYOU_KBN_CD;
                        this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD_NM"].Value = "売上";

                    }
                    else if (strDENPYOU_KBN_CD.Equals("2"))
                    {
                        this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD"].Value = strDENPYOU_KBN_CD;
                        this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD_NM"].Value = "支払";
                    }
                    else
                    {
                        this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD"].Value = string.Empty;
                        this.form.DetailIchiran.Rows[i].Cells["DENPYOU_KBN_CD_NM"].Value = string.Empty;
                    }

                    //// 月極区分
                    string strTSUKIGIME_KBN = this.ChgDBNullToValue(dt.Rows[i]["TSUKIGIME_KBN"], string.Empty) == null ? string.Empty : this.ChgDBNullToValue(dt.Rows[i]["TSUKIGIME_KBN"], string.Empty).ToString();
                    if (!string.IsNullOrEmpty(strTSUKIGIME_KBN))
                    {
                        if ("1".Equals(strTSUKIGIME_KBN))
                        {
                            // 集計単位
                            this.form.DetailIchiran["TSUKIGIME_KBN", i].Value = "1";
                            this.form.DetailIchiran["TSUKIGIME_KBN_NM", i].Value = "伝票";
                        }
                        else if ("2".Equals(strTSUKIGIME_KBN))
                        {
                            // 集計単位
                            this.form.DetailIchiran["TSUKIGIME_KBN", i].Value = "2";
                            this.form.DetailIchiran["TSUKIGIME_KBN_NM", i].Value = "合算";
                        }
                        else
                        {
                            // 集計単位
                            this.form.DetailIchiran["TSUKIGIME_KBN", i].Value = string.Empty;
                            this.form.DetailIchiran["TSUKIGIME_KBN_NM", i].Value = string.Empty;
                        }
                    }

                    this.form.DetailIchiran["INPUT_KBN", i].Value = dt.Rows[i]["INPUT_KBN"];
                    string inputKbn = Convert.ToString(this.form.DetailIchiran["INPUT_KBN", i].Value);
                    if (inputKbn == "1")
                    {
                        this.form.DetailIchiran["INPUT_KBN_NAME", i].Value = ConstCls.INPUT_KBN_1;
                    }
                    else if (inputKbn == "2")
                    {
                        this.form.DetailIchiran["INPUT_KBN_NAME", i].Value = ConstCls.INPUT_KBN_2;
                        this.form.DetailIchiran["ROUND_NO", i].ReadOnly = true;
                        this.form.DetailIchiran["GYOUSHA_CD", i].ReadOnly = true;
                        this.form.DetailIchiran["GENBA_CD", i].ReadOnly = true;
                        this.form.DetailIchiran["HINMEI_CD", i].ReadOnly = true;
                        this.form.DetailIchiran["UNIT_CD", i].ReadOnly = true;
                    }

                    // コースマスタ情報から取得した按分フラグを実数に設定する。
                    this.form.DetailIchiran["ANBUN_FLG", i].Value = this.ChgDBNullToValue(dt.Rows[i]["ANBUN_FLG"], false);

                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetDetailIchiranFromCourse", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(dt);
            }
        }

        /// <summary>
        /// コースマスタに紐付く情報のセット
        /// </summary>
        /// <param name="dayCD">曜日CD</param>
        /// <param name="courseNameCD">コース名称CD</param>
        internal void setCourseData(Int16 dayCD, string courseNameCD)
        {
            var courseDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_COURSEDao>();

            // キーとなるCDを基にコースマスタを取得
            var findEntity = new M_COURSE();
            findEntity.DAY_CD = dayCD;
            findEntity.COURSE_NAME_CD = courseNameCD;
            var courseEntitys = courseDao.GetAllValidData(findEntity);

            if ((courseEntitys != null) && (courseEntitys.Length > 0))
            {
                // 車種・車輌・運転者・運搬業者を一旦クリア
                this.form.SHASHU_CD.Text = string.Empty;
                this.form.SHASHU_NAME_RYAKU.Text = string.Empty;
                this.form.SHARYOU_CD.Text = string.Empty;
                this.form.SHARYOU_NAME_RYAKU.Text = string.Empty;
                this.form.UNTENSHA_CD.Text = string.Empty;
                this.form.UNTENSHA_NAME.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;

                // 各CDを取得
                // ※キーCDによる検索のため、該当するCDは唯一
                var shashuCD = courseEntitys[0].SHASHU_CD;
                var sharyouCD = courseEntitys[0].SHARYOU_CD;
                var untenshaCD = courseEntitys[0].UNTENSHA_CD;
                var unpanGyoushaCD = courseEntitys[0].UNPAN_GYOUSHA_CD;

                // 車種CD
                if (false == string.IsNullOrEmpty(shashuCD))
                {
                    // 車種マスタを検索
                    var data = new M_SHASHU();
                    data.SHASHU_CD = shashuCD;
                    var entitys = this.shashuDao.GetAllValidData(data);
                    if ((entitys != null) && (entitys.Length > 0))
                    {
                        // 各情報をセット
                        // ※キーCDによる検索のため、該当するCDは唯一
                        this.form.SHASHU_CD.Text = entitys[0].SHASHU_CD;
                        this.form.SHASHU_NAME_RYAKU.Text = entitys[0].SHASHU_NAME_RYAKU;
                    }
                }

                // 車輌CD
                if (false == string.IsNullOrEmpty(sharyouCD))
                {
                    // 車輌マスタを検索
                    var data = new M_SHARYOU();
                    data.SHARYOU_CD = sharyouCD;
                    if (false == string.IsNullOrEmpty(unpanGyoushaCD))
                    {
                        data.GYOUSHA_CD = unpanGyoushaCD;
                    }
                    else
                    {
                        data.GYOUSHA_CD = "";
                    }
                    var entitys = this.sharyouDao.GetAllValidData(data);
                    if ((entitys != null) && (entitys.Length > 0))
                    {
                        // 各情報をセット
                        // ※キーCDによる検索のため、該当するCDは唯一
                        this.form.SHARYOU_CD.Text = entitys[0].SHARYOU_CD;
                        this.form.SHARYOU_NAME_RYAKU.Text = entitys[0].SHARYOU_NAME_RYAKU;
                    }
                }

                // 運転者CD
                if (false == string.IsNullOrEmpty(untenshaCD))
                {
                    // 社員マスタを検索
                    var data = new M_SHAIN();
                    data.SHAIN_CD = untenshaCD;
                    var entitys = this.shainDao.GetAllValidData(data);
                    if ((entitys != null) && (entitys.Length > 0))
                    {
                        // 各情報をセット
                        // ※キーCDによる検索のため、該当するCDは唯一
                        this.form.UNTENSHA_CD.Text = entitys[0].SHAIN_CD;
                        this.form.UNTENSHA_NAME.Text = entitys[0].SHAIN_NAME_RYAKU;
                    }
                }

                // 運搬業者CD
                if (false == string.IsNullOrEmpty(unpanGyoushaCD))
                {
                    // 業者マスタを検索
                    var data = new M_GYOUSHA();
                    data.GYOUSHA_CD = unpanGyoushaCD;
                    var entitys = this.gyoushaDao.GetAllValidData(data);
                    if ((entitys != null) && (entitys.Length > 0))
                    {
                        // 各情報をセット
                        // ※キーCDによる検索のため、該当するCDは唯一
                        this.form.UNPAN_GYOUSHA_CD.Text = entitys[0].GYOUSHA_CD;
                        this.form.UNPAN_GYOUSHA_NAME.Text = entitys[0].GYOUSHA_NAME_RYAKU;
                    }
                }
            }
        }

        #endregion コース操作時処理

        #region Detail部活性制御
        /// <summary>
        /// KAKUTEI_FLGをチェックし、定期実績明細の行をRaedOnlyにする
        /// 実績売上支払確定がされている明細は編集不可とする。
        /// 修正したい場合は実績売上支払確定からキャンセル操作をして確定を解除すること
        /// </summary>
        private void CheckUrShNumberAndControlReadOnly()
        {
            LogUtility.DebugMethodStart();

            foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
            {
                if (row.IsNewRow)
                {
                    continue;
                }

                bool kakuteiFlg = Convert.ToBoolean(row.Cells["KAKUTEI_FLG"].Value);
                if (kakuteiFlg)
                {
                    //row.ReadOnly = true;

                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        cell.ReadOnly = true;

                        var ia = cell as ICustomAutoChangeBackColor;
                        if (ia != null)
                        {
                            CustomControlExtLogic.UpdateBackColor(ia);
                        }
                    }
                }
            }

            LogUtility.DebugMethodEnd();
        }

        internal bool IsExecutableCell(DataGridViewRow row)
        {
            bool returnVal = true;
            try
            {
                LogUtility.DebugMethodStart(row);

                if (row != null
                    && !string.IsNullOrEmpty(row.Cells["UR_SH_NUMBER_NUMERIC"].FormattedValue.ToString()))
                {
                    returnVal = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExecutableCell", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                returnVal = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
        }

        //障害 #11416  定期配車実績入力　確定済みの行で使用している荷降行が変更できてしまう。
        /// <summary>
        /// 品名毎タブの各明細行が確定になっている場合は、行全体を編集不可に設定する
        /// </summary>
        internal bool CheckClmOKDetailIchiran(string NIOROSHI_NUMBER, out bool catchErr)
        {
            bool ret = false;
            catchErr = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (string.IsNullOrEmpty(NIOROSHI_NUMBER))
                {
                    foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        //確定済み
                        if (row.Cells["KAKUTEI_FLG"].Value != null && this.ConvertToBool(row.Cells["KAKUTEI_FLG"].Value))
                        {
                            return ret = true;
                        }
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in this.form.DetailIchiran.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        //荷降Noと確定済み
                        if (row.Cells["KAKUTEI_FLG"].Value != null && this.ConvertToBool(row.Cells["KAKUTEI_FLG"].Value)
                            && row.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].Value != null
                            && Convert.ToString(row.Cells[ConstCls.DetailColName.NIOROSHI_NUMBER_DETAIL].Value).Equals(NIOROSHI_NUMBER))
                        {
                            return ret = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckClmOKDetailIchiran", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = true;
                catchErr = true;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret, catchErr);
            }
            return ret;
        }
        //障害 #11416

        internal bool IsExecutableCellForsyuusyuuDetail(DataGridViewRow row)
        {
            bool retVal = true;
            try
            {
                LogUtility.DebugMethodStart(row);

                if (row != null
                    && row.Cells["clmOk2"].Value != null && this.ConvertToBool(row.Cells["clmOk2"].Value))
                {
                    retVal = false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("IsExecutableCellForsyuusyuuDetail", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                retVal = false;

            }
            finally
            {
                LogUtility.DebugMethodEnd(retVal);
            }
            return retVal;
        }
        #endregion

        #region Equals/GetHashCode/ToString

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {

            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        #endregion

        /// <summary>
        /// オブジェクトをboolに変換します
        /// </summary>
        /// <param name="value">変換対象のオブジェクト</param>
        /// <returns>変換後の値（nullはfalse）</returns>
        internal bool ConvertToBool(object value)
        {
            LogUtility.DebugMethodStart(value);

            var ret = false;

            if (null != value)
            {
                if (false == bool.TryParse(value.ToString(), out ret))
                {
                    if (value.Equals(1))
                    {
                        ret = true;
                    }
                }
            }

            LogUtility.DebugMethodEnd(ret);

            return ret;
        }

        #region 按分実行時の単位・換算後単位の重複チェック
        /// <summary>
        /// 品名毎一覧の各レコードで単位・換算後単位で共に「kg」指定のエラー有無を判定
        /// </summary>
        /// <returns>true:エラー有,false:エラー無</returns>
        internal bool DuplicatedUnitCdKg()
        {
            bool duplicatedUnitErrorFlag = false;
            try
            {
                for (int i = 0; i < this.form.DetailIchiran.RowCount - 1; i++)
                {
                    DataGridViewRow detailRow = this.form.DetailIchiran.Rows[i];

                    string unitCd = this.GetCellValue(detailRow.Cells["UNIT_CD"]);
                    string kansanUnitCd = this.GetCellValue(detailRow.Cells["KANSAN_UNIT_CD"]);

                    if (string.IsNullOrEmpty(unitCd) || string.IsNullOrEmpty(kansanUnitCd))
                    {
                        continue;
                    }

                    if (ConstCls.mKg_UnitCdKg.Equals(unitCd) && ConstCls.mKg_UnitCdKg.Equals(kansanUnitCd))
                    {
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["UNIT_CD"], true);
                        ControlUtility.SetInputErrorOccuredForDgvCell(detailRow.Cells["KANSAN_UNIT_CD"], true);
                        duplicatedUnitErrorFlag = true;
                    }
                }

                if (duplicatedUnitErrorFlag)
                {
                    // アラート表示
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E031", "単位と換算後単位");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicatedUnitCdKg", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                duplicatedUnitErrorFlag = true;
            }
            finally
            {
            }
            return duplicatedUnitErrorFlag;
        }
        #endregion

        // 20141015 koukouei 休動管理機能 start
        #region 補助員CDバリデート
        /// <summary>
        /// 補助員CDバリデート
        /// </summary>
        public void HOJOIN_CDValidated()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 一旦初期化
                this.form.HOJOIN_NAME.Text = "";

                var untenShain = this.shainDao.GetAllValidData(new M_SHAIN()).FirstOrDefault(s => (bool)s.UNTEN_KBN && s.SHAIN_CD == this.form.HOJOIN_CD.Text);
                if (untenShain == null)
                {
                    // エラーメッセージ
                    this.form.HOJOIN_CD.IsInputErrorOccured = true;
                    this.form.HOJOIN_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "社員");
                    this.isInputError = true;
                    this.form.HOJOIN_CD.Focus();
                    return;
                }

                if (!string.IsNullOrEmpty(this.form.HOJOIN_CD.Text) &&
                       this.form.UNTENSHA_CD.Text.Equals(this.form.HOJOIN_CD.Text))
                {
                    // エラーメッセージ
                    this.form.HOJOIN_CD.IsInputErrorOccured = true;
                    this.form.HOJOIN_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E031", "運転者、補助員の指定");
                    this.isInputError = true;
                    this.form.HOJOIN_CD.Focus();
                    return;
                }
                // 休動チェック
                else if (!this.ChkHojoinWordClose(true))
                {
                    this.form.HOJOIN_CD.Focus();
                    return;
                }

                this.form.HOJOIN_NAME.Text = untenShain.SHAIN_NAME_RYAKU;
            }
            catch (Exception ex)
            {
                LogUtility.Error("HOJOIN_CDValidated", ex);
                this.MsgBox.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        /// <summary>
        /// 荷降業者CDバリデート
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public bool ChechNioroshiGyoushaCd(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                M_GYOUSHA keygyousyaEntity = new M_GYOUSHA();
                // 荷降業者CD
                String gyoushaCd = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = string.Empty;
                    this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value = string.Empty;
                    this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                var msgLogic = new MessageBoxShowLogic();
                keygyousyaEntity.GYOUSHA_CD = gyoushaCd;
                var gyousya = this.gyoushaDao.GetAllValidData(keygyousyaEntity);
                if (gyousya == null || gyousya.Length < 1 ||
                    (!gyousya[0].SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue && !gyousya[0].UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue))
                {
                    msgLogic.MessageBoxShow("E020", "荷降業者");
                    this.isInputError = true;
                    this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = string.Empty;
                    this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value = string.Empty;
                    this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                    ret = false;
                }
                else
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    if (gyousya[0].TEKIYOU_BEGIN.IsNull && gyousya[0].TEKIYOU_END.IsNull)
                    {
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyousya[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (gyousya[0].TEKIYOU_BEGIN.IsNull && !gyousya[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(gyousya[0].TEKIYOU_END) <= 0)
                    {
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyousya[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (!gyousya[0].TEKIYOU_BEGIN.IsNull && gyousya[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousya[0].TEKIYOU_BEGIN) >= 0)
                    {
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyousya[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (!gyousya[0].TEKIYOU_BEGIN.IsNull && !gyousya[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousya[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(gyousya[0].TEKIYOU_END) <= 0)
                    {
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyousya[0].GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E020", "荷降業者");
                        this.isInputError = true;
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = string.Empty;
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value = string.Empty;
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechNioroshiGyoushaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #region 荷降現場CDバリデート
        /// <summary>
        /// 荷降現場CDバリデート
        /// </summary>
        public bool ChechNioroshiGenbaCd(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                // 一旦初期化
                //var cellGenbaName = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU];
                //string genbaName = Convert.ToString(cellGenbaName.Value);
                //cellGenbaName.Value = "";
                // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                // 休動チェック
                if (!this.ChkGenbaWordClose(row, true))
                {
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                //cellGenbaName.Value = genbaName;
                // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

                M_GENBA keyEntity = new M_GENBA();
                // 荷降業者CD
                String gyoushaCd = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
                String genbaCd = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();

                var msgLogic = new MessageBoxShowLogic();
                if (string.IsNullOrEmpty(gyoushaCd) && !string.IsNullOrEmpty(genbaCd))
                {
                    msgLogic.MessageBoxShow("E051", "荷降業者");
                    this.isInputError = true;
                    this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value = string.Empty;
                    if (this.form.NioroshiIchiran.EditingControl != null)
                    {
                        this.form.NioroshiIchiran.EditingControl.Text = string.Empty;
                    }
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (string.IsNullOrEmpty(genbaCd))
                {
                    this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity);
                if (genba == null || genba.Length < 1)
                {
                    msgLogic.MessageBoxShow("E020", "荷降現場");
                    this.isInputError = true;
                    this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                else
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    if (genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull)
                    {
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0)
                    {
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        msgLogic.MessageBoxShow("E020", "荷降現場");
                        this.isInputError = true;
                        this.form.NioroshiIchiran.CurrentRow.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }

                // 荷降業者名を設定
                if (!this.SetNioroshiGyoushaName(row)) { ret = false; }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechNioroshiGenbaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion


        #region 業者CDバリデート
        /// <summary>
        /// 業者CDバリデート
        /// </summary>
        public bool CheckGyoushaCd(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 業者CD
                String gyoushaCd = row.Cells["clmGYOUSHA_CD"].FormattedValue.ToString();

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    this.form.syuusyuuDetailIchiran.CurrentRow.Cells["clmGYOUSHA_NAME_RYAKU"].Value = string.Empty;
                    this.form.syuusyuuDetailIchiran.CurrentRow.Cells["clmGENBA_CD"].Value = string.Empty;
                    this.form.syuusyuuDetailIchiran.CurrentRow.Cells["clmGENBA_NAME_RYAKU"].Value = string.Empty;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);
                if (gyousha == null || gyousha.Length < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.isInputError = true;
                    row.Cells["clmGYOUSHA_NAME_RYAKU"].Value = string.Empty;
                    row.Cells["clmGENBA_CD"].Value = string.Empty;
                    row.Cells["clmGENBA_NAME_RYAKU"].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (this.form.syuusyuuDetailIchiran.CurrentRow.Index >= 0)
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }

                    if (gyousha[0].TEKIYOU_BEGIN.IsNull && gyousha[0].TEKIYOU_END.IsNull)
                    {
                        row.Cells["clmGYOUSHA_NAME_RYAKU"].Value = gyousha[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (gyousha[0].TEKIYOU_BEGIN.IsNull && !gyousha[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells["clmGYOUSHA_NAME_RYAKU"].Value = gyousha[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (!gyousha[0].TEKIYOU_BEGIN.IsNull && gyousha[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_BEGIN) >= 0)
                    {
                        row.Cells["clmGYOUSHA_NAME_RYAKU"].Value = gyousha[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (!gyousha[0].TEKIYOU_BEGIN.IsNull && !gyousha[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells["clmGYOUSHA_NAME_RYAKU"].Value = gyousha[0].GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.isInputError = true;
                        row.Cells["clmGYOUSHA_NAME_RYAKU"].Value = string.Empty;
                        row.Cells["clmGENBA_CD"].Value = string.Empty;
                        row.Cells["clmGENBA_NAME_RYAKU"].Value = string.Empty;
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechiGyoushaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 業者CDバリデート
        /// <summary>
        /// 業者CDバリデート
        /// </summary>
        public bool ChechiGyoushaCd(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 業者CD
                String gyoushaCd = row.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString();

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = string.Empty;
                    row.Cells[ConstCls.DetailColName.GENBA_CD].Value = string.Empty;
                    row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = string.Empty;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_GYOUSHA keyEntity = new M_GYOUSHA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var gyousha = this.gyoushaDao.GetAllValidData(keyEntity);
                if (gyousha == null || gyousha.Length < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.isInputError = true;
                    row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = string.Empty;
                    row.Cells[ConstCls.DetailColName.GENBA_CD].Value = string.Empty;
                    row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (row.Index >= 0)
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    
                    if (gyousha[0].TEKIYOU_BEGIN.IsNull && gyousha[0].TEKIYOU_END.IsNull)
                    {
                        row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyousha[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (gyousha[0].TEKIYOU_BEGIN.IsNull && !gyousha[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyousha[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (!gyousha[0].TEKIYOU_BEGIN.IsNull && gyousha[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_BEGIN) >= 0)
                    {
                        row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyousha[0].GYOUSHA_NAME_RYAKU;
                    }
                    else if (!gyousha[0].TEKIYOU_BEGIN.IsNull && !gyousha[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(gyousha[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyousha[0].GYOUSHA_NAME_RYAKU;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "業者");
                        this.isInputError = true;
                        row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = string.Empty;
                        row.Cells[ConstCls.DetailColName.GENBA_CD].Value = string.Empty;
                        row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = string.Empty;
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechiGyoushaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion
        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        #region 現場CDバリデート
        /// <summary>
        /// 現場CDバリデート
        /// </summary>
        public bool ChechiGenbaCd(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 業者CD
                String gyoushaCd = row.Cells[ConstCls.DetailColName.GYOUSHA_CD].FormattedValue.ToString();
                // 現場CD
                String genbaCd = row.Cells[ConstCls.DetailColName.GENBA_CD].FormattedValue.ToString();
                var cell = row.Cells[ConstCls.DetailColName.GENBA_CD] as DgvCustomAlphaNumTextBoxCell;

                if (string.IsNullOrEmpty(genbaCd))
                {
                    LogUtility.DebugMethodEnd(ret);
                    row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = string.Empty;
                    return ret;
                }

                if (string.IsNullOrEmpty(gyoushaCd) && !string.IsNullOrEmpty(genbaCd))
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E051", "業者");
                    this.isInputError = true;
                    row.Cells[ConstCls.DetailColName.GENBA_CD].Value = string.Empty;
                    if (this.form.DetailIchiran.EditingControl != null)
                    {
                        this.form.DetailIchiran.EditingControl.Text = string.Empty;
                    }
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity);
                if (genba == null || genba.Length < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.isInputError = true;
                    row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (row.Index >= 0)
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    if (genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull)
                    {
                        row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0)
                    {
                        row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "現場");
                        this.isInputError = true;
                        row.Cells[ConstCls.DetailColName.GENBA_NAME_RYAKU].Value = string.Empty;
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechiGenbaCd", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion
        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

        #region 休動チェック
        /// <summary>
        /// 休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkWordClose()
        {
            bool ret = false;
            try
            {
                if (!ChkSharyouWordClose(true))
                {
                    this.form.SHARYOU_CD.Focus();
                    return ret;
                }

                if (!ChkUntenshaWordClose(true))
                {
                    this.form.UNTENSHA_CD.Focus();
                    return ret;
                }

                if (!ChkHojoinWordClose(true))
                {
                    this.form.HOJOIN_CD.Focus();
                    return ret;
                }

                foreach (DataGridViewRow row in this.form.NioroshiIchiran.Rows)
                {
                    if (!ChkGenbaWordClose(row, true))
                    {
                        this.form.NioroshiIchiran.CurrentCell = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD];
                        this.form.NioroshiIchiran.Focus();
                        return ret;
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChkWordClose", ex);
                if (ex is SQLRuntimeException)
                {
                    this.MsgBox.MessageBoxShow("E093", "");
                }
                else
                {
                    this.MsgBox.MessageBoxShow("E245", "");
                }
                ret = false;
            }
            // 処理終了
            return ret;
        }
        #endregion

        #region 車輌休動チェック
        /// <summary>
        /// 車輌休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkSharyouWordClose(bool messageFlg)
        {
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text) || string.IsNullOrEmpty(this.form.SHARYOU_CD.Text))
            {
                return true;
            }

            M_WORK_CLOSED_SHARYOU sharyou = new M_WORK_CLOSED_SHARYOU();
            sharyou.SHARYOU_CD = this.form.SHARYOU_CD.Text;
            if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                sharyou.GYOUSHA_CD = "";
            }
            else
            {
                sharyou.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            }
            sharyou.CLOSED_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
            M_WORK_CLOSED_SHARYOU[] sharyous = this.workClosedSharyouDao.GetAllValidData(sharyou);
            if (sharyous.Length > 0)
            {
                // エラーメッセージ
                this.form.SHARYOU_CD.IsInputErrorOccured = true;
                this.form.SHARYOU_CD.BackColor = Constans.ERROR_COLOR;
                var msgLogic = new MessageBoxShowLogic();
                string date = string.Format("作業日：{0}", sharyou.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                if (messageFlg)
                {
                    msgLogic.MessageBoxShow("E206", "車輛", date);
                }
                return false;
            }

            // 処理終了
            return true;
        }
        #endregion

        #region 運転者休動チェック
        /// <summary>
        /// 運転者休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkUntenshaWordClose(bool messageFlg)
        {
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
            {
                return true;
            }

            M_WORK_CLOSED_UNTENSHA untensha = new M_WORK_CLOSED_UNTENSHA();
            untensha.SHAIN_CD = this.form.UNTENSHA_CD.Text;
            untensha.CLOSED_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
            M_WORK_CLOSED_UNTENSHA[] untenshas = this.workClosedUntenshaDao.GetAllValidData(untensha);
            if (untenshas.Length > 0)
            {
                // エラーメッセージ
                this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                this.form.UNTENSHA_CD.BackColor = Constans.ERROR_COLOR;
                var msgLogic = new MessageBoxShowLogic();
                string date = string.Format("作業日：{0}", untensha.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                if (messageFlg)
                {
                    msgLogic.MessageBoxShow("E206", "運転者", date);
                }
                return false;
            }

            // 処理終了
            return true;
        }
        #endregion

        #region 補助員休動チェック
        /// <summary>
        /// 補助員休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkHojoinWordClose(bool messageFlg)
        {
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
            {
                return true;
            }

            M_WORK_CLOSED_UNTENSHA untensha = new M_WORK_CLOSED_UNTENSHA();
            untensha.SHAIN_CD = this.form.HOJOIN_CD.Text;
            untensha.CLOSED_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
            M_WORK_CLOSED_UNTENSHA[] sharyous = this.workClosedUntenshaDao.GetAllValidData(untensha);
            if (sharyous.Length > 0)
            {
                // エラーメッセージ
                this.form.HOJOIN_CD.IsInputErrorOccured = true;
                this.form.HOJOIN_CD.BackColor = Constans.ERROR_COLOR;
                var msgLogic = new MessageBoxShowLogic();
                string date = string.Format("作業日：{0}", untensha.CLOSED_DATE.Value.ToString("yyyy/MM/dd"));
                if (messageFlg)
                {
                    msgLogic.MessageBoxShow("E206", "補助員", date);
                }
                return false;
            }

            // 処理終了
            return true;
        }
        #endregion

        #region 荷降現場休動チェック
        /// <summary>
        /// 荷降現場休動チェック
        /// </summary>
        /// <returns></returns>
        internal bool ChkGenbaWordClose(DataGridViewRow row, bool messageFlg)
        {
            if (string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
            {
                return true;
            }

            // 荷降業者CD
            String gyoushaCd = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].FormattedValue.ToString();
            // 荷降現場CD
            String genbaCd = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].FormattedValue.ToString();
            var cell = row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD] as DgvCustomAlphaNumTextBoxCell;

            // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            if (string.IsNullOrEmpty(genbaCd))
            {
                return true;
            }

            if (string.IsNullOrEmpty(gyoushaCd) && !string.IsNullOrEmpty(genbaCd))
            {
                var msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E051", "荷降現場");
                row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD].Value = string.Empty;
                if (this.form.NioroshiIchiran.EditingControl != null)
                {
                    this.form.NioroshiIchiran.EditingControl.Text = string.Empty;
                }
                return false;
            }
            // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

            M_WORK_CLOSED_HANNYUUSAKI hannyuu = new M_WORK_CLOSED_HANNYUUSAKI();
            hannyuu.GYOUSHA_CD = gyoushaCd;
            hannyuu.GENBA_CD = genbaCd;
            hannyuu.CLOSED_DATE = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
            M_WORK_CLOSED_HANNYUUSAKI[] hannyuus = this.workClosedHanYuusakiDao.GetAllValidData(hannyuu);
            if (hannyuus.Length > 0)
            {
                // エラーメッセージ
                ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_CD], true);
                DateTime dt = Convert.ToDateTime(this.form.SAGYOU_DATE.Text);
                var msgLogic = new MessageBoxShowLogic();
                string date = string.Format("作業日：{0}", dt.ToString("yyyy/MM/dd"));
                if (messageFlg)
                {
                    msgLogic.MessageBoxShow("E206", "荷降現場", date);
                    // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
                    row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = string.Empty;
                    // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
                }
                return false;
            }

            // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
            M_GENBA keyEntity = new M_GENBA();
            keyEntity.GENBA_CD = genbaCd;
            keyEntity.GYOUSHA_CD = gyoushaCd;
            var genba = this.genbaDao.GetAllValidData(keyEntity);
            if (genba == null || genba.Length < 1)
            {
                return true;
            }

            // 20151026 BUNN #12040 STR
            if (!genba[0].SHOBUN_NIOROSHI_GENBA_KBN.IsTrue && !genba[0].SAISHUU_SHOBUNJOU_KBN.IsTrue && !genba[0].TSUMIKAEHOKAN_KBN.IsTrue)
            // 20151026 BUNN #12040 END
            {
                return true;
            }
            M_GYOUSHA keygyousyaEntity = new M_GYOUSHA();
            keygyousyaEntity.GYOUSHA_CD = gyoushaCd;
            var gyousya = this.gyoushaDao.GetAllValidData(keygyousyaEntity);
            // 20151026 BUNN #12040 STR
            if (!gyousya.Any() || !gyousya[0].SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue && !gyousya[0].UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
            // 20151026 BUNN #12040 END
            {
               return true;
            }

            if (row.Index >= 0)
            {
                SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                DateTime date;
                if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                {
                    tekiyouDate = date;
                }
                if (genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull)
                {
                    row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                }
                else if (genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                    && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                {
                    row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                }
                else if (!genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0)
                {
                    row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                }
                else if (!genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0
                        && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                {
                    row.Cells[ConstCls.NioroshiColName.NIOROSHI_GENBA_NAME_RYAKU].Value = genba[0].GENBA_NAME_RYAKU;
                }
            }
            // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END

            // 処理終了
            return true;
        }
        #endregion
        // 20141015 koukouei 休動管理機能 end

        /// <summary>
        /// 現場CDと業者CDを元に業者名をセットします
        /// </summary>
        /// <param name="row">DataGridViewRow</param>
        internal bool SetGyoushaName(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                // 業者CDが入力され、かつ業者名が空の場合
                if ((row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value != null
                    && !string.IsNullOrEmpty(row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value.ToString()))
                    && (row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value == null
                    || row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value.ToString() == string.Empty))
                {
                    // 業者を取得
                    var gyoushaEntity = this.GetGyousha(row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value.ToString());

                    // 取得できた場合
                    if (gyoushaEntity != null)
                    {
                        SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                        {
                            tekiyouDate = date;
                        }
                        if (gyoushaEntity.TEKIYOU_BEGIN.IsNull && gyoushaEntity.TEKIYOU_END.IsNull)
                        {
                            // 業者名を設定
                            row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (gyoushaEntity.TEKIYOU_BEGIN.IsNull && !gyoushaEntity.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_END) <= 0)
                        {
                            // 業者名を設定
                            row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (!gyoushaEntity.TEKIYOU_BEGIN.IsNull && gyoushaEntity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_BEGIN) >= 0)
                        {
                            // 業者名を設定
                            row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (!gyoushaEntity.TEKIYOU_BEGIN.IsNull && !gyoushaEntity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_BEGIN) >= 0
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_END) <= 0)
                        {
                            // 業者名を設定
                            row.Cells[ConstCls.DetailColName.GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyoushaName", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 荷降現場CDと荷降業者CDを元に荷降業者名をセットします
        /// </summary>
        /// <param name="row">DataGridViewRow</param>
        internal bool SetNioroshiGyoushaName(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                // 荷降業者CDが入力され、かつ荷降業者名が空の場合
                if ((row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value != null
                    && !string.IsNullOrEmpty(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value.ToString()))
                    && (row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value == null
                    || row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value.ToString() == string.Empty))
                {
                    // 業者を取得
                    var gyoushaEntity = this.GetGyousha(row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_CD].Value.ToString());

                    // 取得できた場合
                    if (gyoushaEntity != null)
                    {
                        SqlDateTime tekiyouDate = SqlDateTime.Null;
                        DateTime date;
                        if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                        {
                            tekiyouDate = date;
                        }
                        if (gyoushaEntity.TEKIYOU_BEGIN.IsNull && gyoushaEntity.TEKIYOU_END.IsNull)
                        {
                            // 荷降業者名を設定
                            row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (gyoushaEntity.TEKIYOU_BEGIN.IsNull && !gyoushaEntity.TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_END) <= 0)
                        {
                            // 荷降業者名を設定
                            row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (!gyoushaEntity.TEKIYOU_BEGIN.IsNull && gyoushaEntity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_BEGIN) >= 0)
                        {
                            // 荷降業者名を設定
                            row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                        else if (!gyoushaEntity.TEKIYOU_BEGIN.IsNull && !gyoushaEntity.TEKIYOU_END.IsNull
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_BEGIN) >= 0
                                && tekiyouDate.CompareTo(gyoushaEntity.TEKIYOU_END) <= 0)
                        {
                            // 荷降業者名を設定
                            row.Cells[ConstCls.NioroshiColName.NIOROSHI_GYOUSHA_NAME_RYAKU].Value = gyoushaEntity.GYOUSHA_NAME_RYAKU;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetNioroshiGyoushaName", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        /// <summary>
        /// 登録用チェック処理で使用する現場を取得する
        /// </summary>
        /// <param name="targetRows"></param>
        /// <returns></returns>
        internal M_GENBA[] GetCheckGenbaAllValidData(DataGridViewRow[] targetRows)
        {
            // 業者・現場取得
            var gyoushaGenbaCds = targetRows
                .GroupBy(row =>
                    new
                    {
                        GYOUSHA_CD = Convert.ToString(row.Cells[ConstCls.DetailColName.GYOUSHA_CD].Value),
                        GENBA_CD = Convert.ToString(row.Cells[ConstCls.DetailColName.GENBA_CD].Value)
                    })
                .Select(row => row.Key)
                .ToArray();

            // クエリ作成
            var sbQuery = new System.Text.StringBuilder();
            foreach (var gyoushaGenbaCd in gyoushaGenbaCds)
            {
                if (sbQuery.Length == 0)
                {
                    sbQuery.Append("(");
                }
                else if (0 < sbQuery.Length)
                {
                    sbQuery.Append(" OR ");
                }
                sbQuery.AppendFormat("(GYOUSHA_CD = '{0}' AND GENBA_CD = '{1}')", gyoushaGenbaCd.GYOUSHA_CD, gyoushaGenbaCd.GENBA_CD);
            }
            if (0 < sbQuery.Length)
            {
                sbQuery.Append(")");
            }

            return this.genbaCustomDao.GetCheckGenbaAllValidData(sbQuery.ToString());
        }

        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 start
        #region 収集現場CDバリデート
        /// <summary>
        /// 収集現場CDバリデート
        /// </summary>
        public bool ChechiSSGenbaCd(DataGridViewRow row)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // 荷降業者CD
                String gyoushaCd = row.Cells["clmGYOUSHA_CD"].FormattedValue.ToString();
                // 荷降現場CD
                String genbaCd = row.Cells["clmGENBA_CD"].FormattedValue.ToString();
                var cell = row.Cells["clmGENBA_CD"] as DgvCustomAlphaNumTextBoxCell;

                if (string.IsNullOrEmpty(genbaCd))
                {
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (string.IsNullOrEmpty(gyoushaCd) && !string.IsNullOrEmpty(genbaCd))
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.isInputError = true;
                    row.Cells["clmGENBA_NAME_RYAKU"].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GENBA_CD = genbaCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var genba = this.genbaDao.GetAllValidData(keyEntity);
                if (genba == null || genba.Length < 1)
                {
                    var msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    this.isInputError = true;
                    row.Cells["clmGENBA_NAME_RYAKU"].Value = string.Empty;
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                if (this.form.syuusyuuDetailIchiran.CurrentRow.Index >= 0)
                {
                    SqlDateTime tekiyouDate = this.parentForm.sysDate.Date;
                    DateTime date;
                    if (!string.IsNullOrWhiteSpace(this.form.SAGYOU_DATE.Text) && DateTime.TryParse(this.form.SAGYOU_DATE.Text, out date))
                    {
                        tekiyouDate = date;
                    }
                    
                    if (genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull)
                    {
                        row.Cells["clmGENBA_NAME_RYAKU"].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                        && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells["clmGENBA_NAME_RYAKU"].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0)
                    {
                        row.Cells["clmGENBA_NAME_RYAKU"].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else if (!genba[0].TEKIYOU_BEGIN.IsNull && !genba[0].TEKIYOU_END.IsNull
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_BEGIN) >= 0
                            && tekiyouDate.CompareTo(genba[0].TEKIYOU_END) <= 0)
                    {
                        row.Cells["clmGENBA_NAME_RYAKU"].Value = genba[0].GENBA_NAME_RYAKU;
                    }
                    else
                    {
                        var msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "現場");
                        this.isInputError = true;
                        row.Cells["clmGENBA_NAME_RYAKU"].Value = string.Empty;
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ChechiSSGenbaCd", ex);
                this.MsgBox.MessageBoxShow("E245", "");
                this.isInputError = true;
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion
        // 20150928 koukoukon #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 end

        /// <summary>
        /// 伝票区分選択のポップアップを表示します
        /// </summary>
        /// <returns>選択された伝票区分CD</returns>
        internal void Ichiran_RowsAdded()
        {
            LogUtility.DebugMethodStart();

            var row = this.form.DetailIchiran.CurrentRow;
            if (row == null)
            {
                return;
            }

            row.Cells["INPUT_KBN"].Value = "1";
            row.Cells["INPUT_KBN_NAME"].Value = ConstCls.INPUT_KBN_1;

            LogUtility.DebugMethodEnd();

            return;
        }

        /// <summary>
        /// 新規登録時 配車番号をチェック
        /// </summary>
        /// <returns></returns>
        internal bool checkHaishaNumber()
        {
            // 定期配車番号を検索条件に設定する
            this.searchDto = new DTOClass();
            this.searchDto.TeikiHaishaNumber = this.form.TEIKI_HAISHA_NUMBER.Text;
            DataTable ExistHaishaNumber = teikiEntryDao.ExistHaishaNumber(this.searchDto);
            // データが存在する場合
            if (ExistHaishaNumber.Rows.Count > 0)
            {
                return false;
            }
            return true;
        }

        #region 連携チェック
        internal bool RenkeiCheck(string haishaNum)
        {
            bool ret = true;
            try
            {
                if (string.IsNullOrEmpty(haishaNum))
                {
                    return true;
                }

                DataTable dt = this.mobisyoRtDao.GetRenkeiData("0", haishaNum);
                if (dt != null && dt.Rows.Count > 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E262","現在配車中","完了後、実績取込にて、定期配車実績データを作成");
                    return false;
                }

                // ロジこんぱす連携済みであるかをチェックする。
                string selectStr;
                selectStr = "SELECT DISTINCT LLS.* FROM T_LOGI_LINK_STATUS LLS "
                    + "LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD on LDD.SYSTEM_ID = LLS.SYSTEM_ID and LDD.DELETE_FLG = 0";
                selectStr += " WHERE LDD.DENPYOU_ATTR = 3"  // 3：定期
                    + " and LDD.REF_DENPYOU_NO = " + haishaNum
                    + " and LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                    + " and LLS.DELETE_FLG = 0";

                // データ取得
                dt = this.teikiEntryDao.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    this.MsgBox.MessageBoxShow("E261", "ロジこんぱす連携中", "呼出し");
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                ret = false;
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }

            return true;
        }
        #endregion

    }
}
