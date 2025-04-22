using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.CustomControl.DataGridCustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.ExternalConnection.ExternalCommon.Const;
using Shougun.Core.ExternalConnection.ExternalCommon.DTO.Navitime;
using Shougun.Core.ExternalConnection.ExternalCommon.Logic;

namespace Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku
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
        private string ButtonInfoXmlPath = "Shougun.Core.ExternalConnection.CourseSaitekikaNyuuryoku.Setting.ButtonSetting.xml";

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
        private UIHeader headerForm;

        /// <summary>
        /// メッセージ
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// ナビタイム連携用ロジッククラス
        /// </summary>
        private NaviLogic navilogic;

        /// <summary>
        /// DAO
        /// </summary>
        private DAOClass dao;

        /// <summary>
        /// 配送計画連携状況管理DAO
        /// </summary>
        private NaviLinkStatusDAO naviLinkStatusDao;

        /// <summary>
        /// 配送計画DTO DAO
        /// </summary>
        private NaviDeliveryDtoDAO courseSaitekikaDAO;

        /// <summary>
        /// システム設定DAO
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// コース明細DAO
        /// </summary>
        private IM_COURSE_DETAILDao courseDetailDao;

        /// <summary>
        /// コース_明細変更履歴DAO
        /// </summary>
        private ICHANGE_LOG_M_COURSE_DETAILDao changeLogCourseDetailDao;

        /// <summary>
        /// 定期配車入力DAO
        /// </summary>
        private TeikiHaishaEntryDAO teikiHaishaEntryDAO;

        /// <summary>
        /// 定期配車明細DAO
        /// </summary>
        private TeikiHaishaDetailDAO teikiHaishaDetailDAO;

        /// <summary>
        /// 定期配車詳細DAO
        /// </summary>
        private TeikiHaishaShousaiDAO teikiHaishaShousaiDAO;

        /// <summary>
        /// 定期配車荷卸DAO
        /// </summary>
        private TeikiHaishaNioroshiDAO teikiHaishaNioroshiDAO;

        /// <summary>
        /// システムID採番Dao
        /// </summary>
        private IS_NUMBER_SYSTEMDao numberSystemDao;

        /// <summary>
        /// 伝種採番Dao
        /// </summary>
        private IS_NUMBER_DENSHUDao numberDenshuDao;

        /// <summary>
        /// NAVITIME接続管理Dao
        /// </summary>
        private IS_NAVI_CONNECTDao naviConnectDao;
        #endregion

        #region プロパティ
        /// <summary>
        /// 再取込時の対象日(yyyyMMdd形式)
        /// </summary>
        private string SearchTargetDate { get; set; }

        /// <summary>
        /// システムID
        /// </summary>
        internal long SystemId { get; set; }

        /// <summary>
        /// 配送計画連携状況管理
        /// </summary>
        private T_NAVI_LINK_STATUS NAVI_LINK_STATUS { get; set; }

        /// <summary>
        /// 配送計画DTO
        /// </summary>
        private T_NAVI_DELIVERY_DTO NAVI_DELIVERY_DTO { get; set; }

        /// <summary>
        /// システム設定
        /// </summary>
        private M_SYS_INFO SYS_INFO { get; set; }

        /// <summary>
        /// コース明細リスト
        /// </summary>
        private List<M_COURSE_DETAIL> COURSE_DETAIL_LIST { get; set; }

        /// <summary>
        /// コース明細リスト(削除用リスト)
        /// </summary>
        private List<M_COURSE_DETAIL> COURSE_DETAIL_DELETE_LIST { get; set; }

        /// <summary>
        /// コース明細変更履歴リスト
        /// </summary>
        private List<CHANGE_LOG_M_COURSE_DETAIL> CHANGE_LOG_COURSE_DETAIL_LIST { get; set; }

        /// <summary>
        /// 定期配車入力(前回登録値)
        /// </summary>
        private T_TEIKI_HAISHA_ENTRY PRE_TEIKI_HAISHA_ENTRY { get; set; }

        /// <summary>
        /// 定期配車入力(登録用)
        /// </summary>
        private T_TEIKI_HAISHA_ENTRY TEIKI_HAISHA_ENTRY { get; set; }

        /// <summary>
        /// 定期配車明細リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_DETAIL> TEIKI_HAISHA_DETAIL_LIST { get; set; }

        /// <summary>
        /// 定期配車詳細リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_SHOUSAI> TEIKI_HAISHA_SHOUSAI_LIST { get; set; }

        /// <summary>
        /// 定期配車荷卸リスト
        /// </summary>
        private List<T_TEIKI_HAISHA_NIOROSHI> TEIKI_HAISHA_NIOROSHI_LIST { get; set; }
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

            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.naviLinkStatusDao = DaoInitUtility.GetComponent<NaviLinkStatusDAO>();
            this.courseSaitekikaDAO = DaoInitUtility.GetComponent<NaviDeliveryDtoDAO>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.courseDetailDao = DaoInitUtility.GetComponent<IM_COURSE_DETAILDao>();
            this.changeLogCourseDetailDao = DaoInitUtility.GetComponent<ICHANGE_LOG_M_COURSE_DETAILDao>();
            this.numberSystemDao = DaoInitUtility.GetComponent<IS_NUMBER_SYSTEMDao>();
            this.numberDenshuDao = DaoInitUtility.GetComponent<IS_NUMBER_DENSHUDao>();
            this.naviConnectDao = DaoInitUtility.GetComponent<IS_NAVI_CONNECTDao>();
            this.teikiHaishaEntryDAO = DaoInitUtility.GetComponent<TeikiHaishaEntryDAO>();
            this.teikiHaishaDetailDAO = DaoInitUtility.GetComponent<TeikiHaishaDetailDAO>();
            this.teikiHaishaShousaiDAO = DaoInitUtility.GetComponent<TeikiHaishaShousaiDAO>();
            this.teikiHaishaNioroshiDAO = DaoInitUtility.GetComponent<TeikiHaishaNioroshiDAO>();

            //メッセージ用
            this.msgLogic = new MessageBoxShowLogic();
            this.navilogic = new NaviLogic();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        /// <returns></returns>
        internal void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // キー入力設定
                this.parentForm = (BusinessBaseForm)this.form.Parent;
                this.headerForm = (UIHeader)this.parentForm.headerForm;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // 検索条件の初期化処理
                this.SetInitialForm();

                // 検証or補助コントロール(開発用機能のため非表示)
                DisplayDeleteMatter();
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
        }
        #endregion

        #region 案件削除機能
        /// <summary>
        /// 仮機能
        /// </summary>
        private void DisplayDeleteMatter()
        {
            // 削除予定
            var disp = false;
            if (disp)
            {
                // テスト用ボタンイベント作成
                parentForm.bt_process2.Click += new EventHandler(this.form.bt_process2_Click);
            }
            else
            {
                this.form.label1.Visible = false;
                this.form.USER_CODE.Visible = false;
                this.form.label2.Visible = false;
                this.form.MATTER_CODE_FROM.Visible = false;
                this.form.label3.Visible = false;
                this.form.MATTER_CODE_TO.Visible = false;

                this.parentForm.bt_process2.Enabled = true;
                this.parentForm.bt_process2.Text = "";
            }
        }
        #endregion

        #region ボタン設定
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
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);
        }
        #endregion

        #region イベント初期化
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            // 上ボタン(F4)イベント作成
            this.parentForm.bt_func4.Click += new EventHandler(this.form.bt_func4_Click);
            // 下ボタン(F5)イベント作成
            this.parentForm.bt_func5.Click += new EventHandler(this.form.bt_func5_Click);
            // 再取込ボタン(F7)イベント作成
            this.parentForm.bt_func7.Click += new EventHandler(this.form.bt_func7_Click);
            // 順番整列ボタン(F8)イベント作成
            this.parentForm.bt_func8.Click += new EventHandler(this.form.bt_func8_Click);
            // 登録ボタン(F9)イベント作成
            this.form.C_Regist(this.parentForm.bt_func9);
            this.parentForm.bt_func9.Click += new EventHandler(this.form.bt_func9_Click);
            this.parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;
            // 取消ボタン(F11)イベント作成
            this.parentForm.bt_func11.Click += new EventHandler(this.form.bt_func11_Click);
            // 閉じるボタン(F12)イベント作成
            this.parentForm.bt_func12.Click += new EventHandler(this.form.bt_func12_Click);
        }
        #endregion

        #region 初期状態の画面
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        internal void SetInitialForm()
        {
            // ボタン制御
            this.parentForm.bt_func4.Enabled = true;
            this.parentForm.bt_func5.Enabled = true;
            this.parentForm.bt_func7.Enabled = true;
            this.parentForm.bt_func8.Enabled = true;
            this.parentForm.bt_func9.Enabled = false;
            this.parentForm.bt_func11.Enabled = true;
            this.parentForm.bt_func12.Enabled = true;
            this.parentForm.bt_process1.Enabled = false;

            // 一覧初期化
            this.form.customDataGridView2.DataSource = new List<SaitekikaDTO>();

            // データ取得
            this.SYS_INFO = this.sysInfoDao.GetAllDataForCode("0");
            this.GetCourseInfo(this.SystemId);
            this.GetRegistData(this.headerForm);

            // 最適化対象の活性制御
            var isCourseSaitekika = IsCourseSaitekika();

            // 定期の時は作業日は読取専用
            this.form.TARGET_DATE.ReadOnly = !isCourseSaitekika;
            this.form.TARGET_DATE.TabStop = isCourseSaitekika;

            this.headerForm.NAVI_GET_TIME.Focus();
        }
        #endregion

        #region コースマスタ選択済みか（最適化対象）
        /// <summary>
        /// 最適化対象でコースマスタを選択済みか
        /// </summary>
        /// <returns></returns>
        private bool IsCourseSaitekika()
        {
            if (ConstCls.SAITEKIKA_TAISHO_1 == this.NAVI_DELIVERY_DTO.SAITEKIKA_TAISHO.Value)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 登録用データ取得
        /// <summary>
        /// 登録用データを事前取得
        /// </summary>
        /// <param name="headerForm"></param>
        private void GetRegistData(UIHeader headerForm)
        {
            this.COURSE_DETAIL_LIST = new List<M_COURSE_DETAIL>();
            this.COURSE_DETAIL_DELETE_LIST = new List<M_COURSE_DETAIL>();
            this.CHANGE_LOG_COURSE_DETAIL_LIST = new List<CHANGE_LOG_M_COURSE_DETAIL>();

            this.NAVI_LINK_STATUS = this.naviLinkStatusDao.GetDataByCd(this.NAVI_DELIVERY_DTO.SYSTEM_ID.Value);

            if (IsCourseSaitekika())
            {
                var entity = new M_COURSE_DETAIL();
                entity.DAY_CD = this.NAVI_DELIVERY_DTO.DAY_CD;
                entity.COURSE_NAME_CD = this.NAVI_DELIVERY_DTO.COURSE_NAME_CD;

                this.COURSE_DETAIL_LIST = this.courseDetailDao.GetAllValidData(entity).ToList();
            }
            else
            {
                this.PRE_TEIKI_HAISHA_ENTRY = this.teikiHaishaEntryDAO.GetDataByCd(this.NAVI_DELIVERY_DTO.TEIKI_SYSTEM_ID.Value, this.NAVI_DELIVERY_DTO.TEIKI_SEQ.Value);
                this.TEIKI_HAISHA_DETAIL_LIST = this.teikiHaishaDetailDAO.GetDataListBySeq(this.NAVI_DELIVERY_DTO.TEIKI_SYSTEM_ID.Value, this.NAVI_DELIVERY_DTO.TEIKI_SEQ.Value);
                this.TEIKI_HAISHA_SHOUSAI_LIST = this.teikiHaishaShousaiDAO.GetDataListBySeq(this.NAVI_DELIVERY_DTO.TEIKI_SYSTEM_ID.Value, this.NAVI_DELIVERY_DTO.TEIKI_SEQ.Value);
                this.TEIKI_HAISHA_NIOROSHI_LIST = this.teikiHaishaNioroshiDAO.GetDataListBySeq(this.NAVI_DELIVERY_DTO.TEIKI_SYSTEM_ID.Value, this.NAVI_DELIVERY_DTO.TEIKI_SEQ.Value);
            }
        }
        #endregion

        #region コースリスト取得
        /// <summary>
        /// コース一覧取得
        /// </summary>
        /// <param name="systemId">システムID</param>
        private void GetCourseInfo(long systemId)
        {
            this.NAVI_DELIVERY_DTO = null;

            if (systemId < 1)
            {
                return;
            }

            var dto = this.courseSaitekikaDAO.GetDataByCd(systemId);
            if (dto == null)
            {
                return;
            }

            this.NAVI_DELIVERY_DTO = dto;

            // 取得情報を画面に設定
            SetCourseSaitekikaDTO(this.NAVI_DELIVERY_DTO);

            // 一覧情報取得・設定
            if (IsCourseSaitekika())
            {
                var dt = dao.GetCourseDetail(this.NAVI_DELIVERY_DTO.DAY_CD, this.NAVI_DELIVERY_DTO.COURSE_NAME_CD);
                this.form.customDataGridView1.DataSource = dt;
            }
            else
            {
                var dt = dao.GetTeikiHaishaDetail(this.NAVI_DELIVERY_DTO.TEIKI_SYSTEM_ID, this.NAVI_DELIVERY_DTO.TEIKI_SEQ);
                this.form.customDataGridView1.DataSource = dt;
            }
        }

        /// <summary>
        /// 指定されたDTOを画面に設定
        /// </summary>
        /// <param name="dto"></param>
        private void SetCourseSaitekikaDTO(T_NAVI_DELIVERY_DTO dto)
        {
            if (dto == null)
            {
                return;
            }

            // 後で削除
            this.form.USER_CODE.Text = dto.SAGYOUSHA_CD;

            this.headerForm.KYOTEN_CD.Text = Convert.ToString(dto.KYOTEN_CD);
            this.headerForm.KYOTEN_NAME_RYAKU.Text = dto.KYOTEN_NAME_RYAKU;
            this.headerForm.NAVI_GET_TIME.Text = this.SYS_INFO.NAVI_GET_TIME.IsNull ? "2" : this.SYS_INFO.NAVI_GET_TIME.ToString();

            this.form.TEIKI_HAISHA_NUMBER.Text = dto.TEIKI_HAISHA_NUMBER.IsNull ? string.Empty : dto.TEIKI_HAISHA_NUMBER.ToString();
            this.form.COURSE_NAME_CD.Text = dto.COURSE_NAME_CD;
            this.form.COURSE_NAME.Text = dto.COURSE_NAME_RYAKU;
            this.form.TARGET_DATE.Text = dto.DELIVERY_DATE.Value.ToString("yyyyMMdd");
            SetHourMinute(dto.DEPARTURE_TIME, this.form.SHUPPATSU_TIME_HOUR, this.form.SHUPPATSU_TIME_MINUTE);
            SetHourMinute(dto.ARRIVAL_TIME, this.form.TOUCHAKU_TIME_HOUR, this.form.TOUCHAKU_TIME_MINUTE);
            this.form.UNPAN_GYOUSHA_NAME_RYAKU.Text = dto.UNPAN_GYOUSHA_NAME_RYAKU;
            this.form.UNTENSHA_NAME_RYAKU.Text = dto.UNTENSHA_NAME_RYAKU;
            this.form.SHASHU_NAME_RYAKU.Text = dto.SHASHU_NAME_RYAKU;
            this.form.SHARYOU_NAME_RYAKU.Text = dto.SHARYOU_NAME_RYAKU;
            this.form.SHUPPATSU_GYOUSHA_CD.Text = dto.SHUPPATSU_GYOUSHA_CD;
            this.form.SHUPPATSU_GYOUSHA_NAME_RYAKU.Text = dto.SHUPPATSU_GYOUSHA_NAME_RYAKU;
            this.form.SHUPPATSU_GENBA_CD.Text = dto.SHUPPATSU_GENBA_CD;
            this.form.SHUPPATSU_GENBA_NAME_RYAKU.Text = dto.SHUPPATSU_GENBA_NAME_RYAKU;
            this.form.NIOROSHI_GYOUSHA_CD.Text = dto.NIOROSHI_GYOUSHA_CD;
            this.form.NIOROSHI_GYOUSHA_NAME_RYAKU.Text = dto.NIOROSHI_GYOUSHA_NAME_RYAKU;
            this.form.NIOROSHI_GENBA_CD.Text = dto.NIOROSHI_GENBA_CD;
            this.form.NIOROSHI_GENBA_NAME_RYAKU.Text = dto.NIOROSHI_GENBA_NAME_RYAKU;
        }

        /// <summary>
        /// 指定された時間を、時･分のコントロールに分割して設定
        /// </summary>
        /// <param name="time">時間</param>
        /// <param name="hour">時を表示するコントロール</param>
        /// <param name="minute">分を表示するコントロール</param>
        private void SetHourMinute(SqlDateTime time, CustomNumericTextBox2 hour, CustomNumericTextBox2 minute)
        {
            if (time.IsNull)
            {
                hour.Text = string.Empty;
                minute.Text = string.Empty;
            }
            else
            {
                SetHourMinute(time.Value, hour, minute);
            }
        }

        /// <summary>
        /// 指定された時間を、時･分のコントロールに分割して設定
        /// </summary>
        /// <param name="time">時間</param>
        /// <param name="hour">時を表示するコントロール</param>
        /// <param name="minute">分を表示するコントロール</param>
        private void SetHourMinute(string time, CustomNumericTextBox2 hour, CustomNumericTextBox2 minute)
        {
            DateTime dt;
            if (DateTime.TryParse(time, out dt))
            {
                SetHourMinute(dt, hour, minute);
            }
            else
            {
                hour.Text = string.Empty;
                minute.Text = string.Empty;
            }
        }

        /// <summary>
        /// 指定された時間を、時･分のコントロールに分割して設定
        /// </summary>
        /// <param name="time">時間</param>
        /// <param name="hour">時を表示するコントロール</param>
        /// <param name="minute">分を表示するコントロール</param>
        private void SetHourMinute(DateTime time, CustomNumericTextBox2 hour, CustomNumericTextBox2 minute)
        {
            if (time == null)
            {
                hour.Text = string.Empty;
                minute.Text = string.Empty;
            }
            else
            {
                hour.Text = time.Hour.ToString();
                minute.Text = time.Minute.ToString();
            }
        }
        #endregion

        #region 実績情報取得
        /// <summary>
        /// 実績情報取得
        /// </summary>
        internal void GetExperience()
        {
            try
            {
                // 一覧初期化
                this.form.customDataGridView2.DataSource = new List<SaitekikaDTO>();

                var postDto = new NaviRequestDto();
                postDto.targetDate = DateTime.Parse(this.form.TARGET_DATE.Value.ToString()).ToString("yyyyMMdd");
                postDto.userCode = this.NAVI_DELIVERY_DTO.SAGYOUSHA_CD;

                this.SearchTargetDate = postDto.targetDate;

                // 「6.案件の実績情報取得」API取得
                var connectDto = this.naviConnectDao.GetDataByContentName(NaviConst.CONTENT_NAME_GET_EXPERIENCE);
                if (connectDto == null)
                {
                    return;
                }

                RES_GET_EXPERIENCE dto = new RES_GET_EXPERIENCE();
                var result = this.navilogic.HttpPOST<RES_GET_EXPERIENCE>(connectDto.URL, connectDto.CONTENT_TYPE, postDto, out dto);
                if (!result || dto == null)
                {
                    // 例外が発生時はNULLなので処理終了
                    return;
                }

                if (dto.Results == null)
                {
                    string msg = string.Empty;
                    // resultsがnullの場合はエラー情報があるはず
                    foreach (string err in dto.ErrorMessage)
                    {
                        msg += err.ToString() + System.Environment.NewLine;
                    }

                    this.msgLogic.MessageBoxShowError(msg);
                    return;
                }

                // 配送計画で指定された便番号で実績情報取得
                var matterList = dto.Results.Matter.Where(n => n.DeliveryOrder == this.NAVI_DELIVERY_DTO.BIN_NO.Value).ToList();

                // NAVITIME側で編集される前の現場情報格納
                List<SaitekikaDTO> saitekikaDtos;
                if (IsCourseSaitekika())
                {
                    saitekikaDtos = dao.GetCourseDetailDto(this.NAVI_DELIVERY_DTO.DAY_CD, this.NAVI_DELIVERY_DTO.COURSE_NAME_CD);
                }
                else
                {
                    saitekikaDtos = dao.GetTeikiHaishaDetailDto(this.NAVI_DELIVERY_DTO.TEIKI_SYSTEM_ID, this.NAVI_DELIVERY_DTO.TEIKI_SEQ);
                }

                // 警告文表示判定
                var dispWarning = false;
                // NAVITIME側で編集された現場情報格納
                List<SaitekikaDTO> newSaitekikaDtos = new List<SaitekikaDTO>();

                // 実績情報を元に、順番を再付番
                foreach (var matter in matterList.OrderBy(n => n.MatterNo))
                {
                    // NAVITIME動態システム側で新たに追加された現場か判定
                    if (string.IsNullOrEmpty(matter.MatterCode))
                    {
                        if (!string.IsNullOrEmpty(matter.VisitCode) && matter.VisitCode.Length == 12)
                        {
                            var gyoushaCd = matter.VisitCode.Substring(0, 6);
                            var genbaCd = matter.VisitCode.Substring(6, 6);

                            // 案件コードが連携済みか判定
                            var dt = this.dao.GetOutputGenba(gyoushaCd, genbaCd);
                            if (dt == null || dt.Rows.Count == 0)
                            {
                                dispWarning = true;
                            }
                            else
                            {
                                var newDto = new SaitekikaDTO();
                                // 新規登録なのでNULLとする
                                newDto.PRE_ROW_NO = null;
                                newDto.IS_ADD_GENBA = true;

                                // 「6.案件の実績情報取得」APIを設定
                                newDto.GYOUSHA_CD = gyoushaCd;
                                newDto.GENBA_CD = genbaCd;
                                newDto.MATTER_CODE = matter.MatterCode;        // 案件コード
                                newDto.INDEX_NO = matter.MatterNo.ToString();  // インデックス番号
                                newDto.ROW_NO = matter.MatterNo.ToString();    // 順番

                                newDto.GENBA_NAME_RYAKU = Convert.ToString(dt.Rows[0]["GENBA_NAME_RYAKU"]);
                                newDto.BIKOU = null;

                                newSaitekikaDtos.Add(newDto);
                            }
                        }
                    }
                    else
                    {
                        // 複数(回数２以上)を考慮し、案件コードが未設定の条件も追加
                        var saitekikaDto = saitekikaDtos.Where(n => matter.VisitCode.Equals(n.GYOUSHA_CD + n.GENBA_CD)
                                                                 && string.IsNullOrEmpty(n.MATTER_CODE)
                                                                 && !n.IS_ADD_GENBA)
                                                        .FirstOrDefault();
                        if (saitekikaDto != null)
                        {
                            // 事前に「変更前」番号を設定
                            saitekikaDto.PRE_ROW_NO = saitekikaDto.ROW_NO;

                            // 「6.案件の実績情報取得」APIを設定
                            saitekikaDto.MATTER_CODE = matter.MatterCode;        // 案件コード
                            saitekikaDto.INDEX_NO = matter.MatterNo.ToString();  // インデックス番号
                            saitekikaDto.ROW_NO = matter.MatterNo.ToString();    // 順番

                            newSaitekikaDtos.Add(saitekikaDto);
                        }
                    }
                }

                // NAVITIME側で削除された現場情報を取得・反映
                var delDtos = saitekikaDtos.Except(newSaitekikaDtos.Where(n => !n.IS_ADD_GENBA), new SaitekikaDTOComparer());
                if (IsCourseSaitekika())
                {
                    foreach (var delDto in delDtos)
                    {
                        var delEntity = this.COURSE_DETAIL_LIST.Where(n => n.REC_ID.ToString().Equals(delDto.ID)).FirstOrDefault();
                        if (delEntity != null)
                        {
                            this.COURSE_DETAIL_LIST.RemoveAll(n => n.REC_ID.Equals(delEntity.REC_ID));
                            // 削除用のコース明細を別途保持
                            this.COURSE_DETAIL_DELETE_LIST.Add(delEntity);
                        }
                    }
                }
                else
                {
                    foreach (var delDto in delDtos)
                    {
                        var delEntity = this.TEIKI_HAISHA_DETAIL_LIST.Where(n => n.DETAIL_SYSTEM_ID.ToString().Equals(delDto.ID)).FirstOrDefault();
                        if (delEntity != null)
                        {
                            // 定期配車はSEQが増える(履歴が追加される)形式なので、リストから削除するだけでOK
                            this.TEIKI_HAISHA_DETAIL_LIST.RemoveAll(n => n.DETAIL_SYSTEM_ID.Equals(delEntity.DETAIL_SYSTEM_ID));
                        }
                    }
                }

                // 「到着予定時刻」項目の設定
                if (ConstCls.NAVI_GET_TIME_1.Equals(this.headerForm.NAVI_GET_TIME.Text))
                {
                    GetArrivalTime(newSaitekikaDtos);
                }

                // 作業時間・現場入りの設定
                var dispList = newSaitekikaDtos.OrderBy(n => Convert.ToInt32(n.ROW_NO)).ToList();
                SetHourMinute(dispList.First().KIBOU_TIME, this.form.FIRST_SAGYOU_TIME_HOUR, this.form.FIRST_SAGYOU_TIME_MINUTE);
                SetHourMinute(dispList.First().ESTIMATED_ARRIVAL_TIME, this.form.FIRST_GENBA_HOUR, this.form.FIRST_GENBA_MINUTE);
                SetHourMinute(dispList.Last().KIBOU_TIME, this.form.LAST_SAGYOU_TIME_HOUR, this.form.LAST_SAGYOU_TIME_MINUTE);
                SetHourMinute(dispList.Last().ESTIMATED_ARRIVAL_TIME, this.form.LAST_GENBA_HOUR, this.form.LAST_GENBA_MINUTE);

                this.form.customDataGridView2.DataSource = dispList;

                // 登録ボタンを活性
                this.parentForm.bt_func9.Enabled = true;
                this.parentForm.bt_process1.Enabled = true;

                ChangeCellColor();

                // 警告表示
                if (matterList == null || matterList.Count == 0)
                {
                    // デバック用に表示
                    this.msgLogic.MessageBoxShowWarn("該当の便番号の情報を取得できないため、変更前の情報を表示します。");
                }
                else if (dispWarning)
                {
                    this.msgLogic.MessageBoxShowWarn("NAVITIMEマスタ連携が未実施の現場が含まれています。\r\nナビタイム画面で追加した現場情報の確認と反映後の環境将軍で情報修正を行ってください。");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetExperience", ex);
                throw;
            }
        }

        /// <summary>
        /// コース最適化DTO比較クラス
        /// </summary>
        class SaitekikaDTOComparer : IEqualityComparer<SaitekikaDTO>
        {
            /// <summary>
            /// 指定されたクラスが等しいか判定
            /// </summary>
            /// <param name="objA"></param>
            /// <param name="objB"></param>
            /// <returns></returns>
            public bool Equals(SaitekikaDTO objA, SaitekikaDTO objB)
            {
                if (Object.Equals(objA, objB))
                {
                    return true;
                }

                if (objA == null || objB == null || string.IsNullOrEmpty(objA.ID) || string.IsNullOrEmpty(objB.ID))
                {
                    return false;
                }
                // IDで比較
                return (objA.ID == objB.ID);
            }

            /// <summary>
            /// ハッシュコード取得
            /// </summary>
            /// <param name="obj"></param>
            /// <returns></returns>
            public int GetHashCode(SaitekikaDTO obj)
            {
                return obj.ID.GetHashCode();
            }
        }

        /// <summary>
        /// 到着時刻取得
        /// </summary>
        /// <param name="dtos"></param>
        private void GetArrivalTime(List<SaitekikaDTO> dtos)
        {
            try
            {
                foreach (var dto in dtos.Where(n => !string.IsNullOrEmpty(n.MATTER_CODE)))
                {
                    // Dtoに案件コードをセット
                    var postDto = new NaviRequestDto();
                    postDto.matterCode = dto.MATTER_CODE;

                    // 実績取得
                    var connectDto = this.naviConnectDao.GetDataByContentName(NaviConst.CONTENT_NAME_GET_ARRIVAL_TIME);
                    if (connectDto == null)
                    {
                        return;
                    }

                    var api = new RES_GET_ARRIVAL_TIME();
                    var result = this.navilogic.HttpPOST<RES_GET_ARRIVAL_TIME>(connectDto.URL, connectDto.CONTENT_TYPE, postDto, out api);
                    if (!result || api == null)
                    {
                        continue;
                    }

                    if (api.ErrorMessage != null)
                    {
                        // エラーが発生した段階で強制終了
                        return;
                    }

                    var time = api.Results.Matter.First().EstimatedArrivalTime;
                    // NAVITIME側で新規現場を追加し、ルート検索をしないとNULLとなるので注意
                    if (time != null)
                    {
                        dto.ESTIMATED_ARRIVAL_DATE = time;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetArrivalTime", ex);
                throw;
            }
        }

        /// <summary>
        /// セルの背景色変更
        /// </summary>
        private void ChangeCellColor()
        {
            if (this.NAVI_DELIVERY_DTO == null
                || this.NAVI_DELIVERY_DTO.SAGYOU_BEGIN_HOUR.IsNull
                || this.NAVI_DELIVERY_DTO.SAGYOU_BEGIN_MINUTE.IsNull)
            {
                // 作業時間(From)がブランクの場合、背景色の変更処理は無し
                return;
            }

            List<SaitekikaDTO> dtos = this.form.customDataGridView2.DataSource as List<SaitekikaDTO>;
            if (dtos == null || dtos.Count == 0)
            {
                return;
            }

            for (int i = 0; i < dtos.Count; i++)
            {
                // 明示的に自動背景色変更モードをfalseにする。
                var cell = this.form.customDataGridView2.Rows[i].Cells[ConstCls.AFTER_ESTIMATED_ARRIVAL_TIME] as DgvCustomAlphaNumTextBoxCell;
                if (cell != null)
                {
                    cell.AutoChangeBackColorEnabled = false;
                }

                var color = GetArrivalTimeBackColor(dtos[i]);
                this.form.customDataGridView2.Rows[i].Cells[ConstCls.AFTER_ESTIMATED_ARRIVAL_TIME].Style.BackColor = color;
                this.form.customDataGridView2.Rows[i].Cells[ConstCls.AFTER_ESTIMATED_ARRIVAL_TIME].Style.SelectionBackColor = color;
            }
        }

        /// <summary>
        /// 背景色取得
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>セルの背景色</returns>
        private Color GetArrivalTimeBackColor(SaitekikaDTO dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.KIBOU_TIME) || string.IsNullOrEmpty(dto.ESTIMATED_ARRIVAL_DATE))
            {
                return Constans.READONLY_COLOR;
            }

            DateTime kibou;
            DateTime arrival;
            if (CalcKibouTime(dto.KIBOU_TIME, out kibou) && DateTime.TryParseExact(dto.ESTIMATED_ARRIVAL_DATE, "yyyyMMdd HH:mm", null, System.Globalization.DateTimeStyles.None, out arrival))
            {
                if (kibou < arrival)
                {
                    // (仮)希望日時 < 到着予定時間
                    return ConstCls.COLOR_RED;
                }
                else if (kibou == arrival)
                {
                    // (仮)希望日時 = 到着予定時間
                    return r_framework.Const.Constans.READONLY_COLOR;
                }
                else
                {
                    // (仮)希望日時 > 到着予定時間
                    return ConstCls.COLOR_YELLOW;
                }
            }
            else
            {
                return r_framework.Const.Constans.READONLY_COLOR;
            }
        }

        /// <summary>
        /// 希望日時算出
        /// </summary>
        /// <param name="kibouTime">希望時間</param>
        /// <param name="result">希望日時</param>
        /// <returns>true:成功, false:失敗</returns>
        private bool CalcKibouTime(string kibouTime, out DateTime result)
        {
            result = DateTime.MinValue;

            // 希望時間
            DateTime kibouDt;
            if (!DateTime.TryParse(kibouTime, out kibouDt))
            {
                return false;
            }

            // 作業日
            DateTime targetDt;
            if (string.IsNullOrEmpty(this.form.TARGET_DATE.Text) || !DateTime.TryParse(this.form.TARGET_DATE.Text, out targetDt))
            {
                return false;
            }

            // 作業時間(From)
            if (this.NAVI_DELIVERY_DTO == null
                || this.NAVI_DELIVERY_DTO.SAGYOU_BEGIN_HOUR.IsNull
                || this.NAVI_DELIVERY_DTO.SAGYOU_BEGIN_MINUTE.IsNull)
            {
                return false;
            }
            var today = DateTime.Today;
            DateTime sagyouTimeDt = new DateTime(today.Year, today.Month, today.Day, this.NAVI_DELIVERY_DTO.SAGYOU_BEGIN_HOUR.Value, this.NAVI_DELIVERY_DTO.SAGYOU_BEGIN_MINUTE.Value, 0);

            // （仮）希望日時
            DateTime tempKibouDt = new DateTime(targetDt.Year, targetDt.Month, targetDt.Day, kibouDt.Hour, kibouDt.Minute, kibouDt.Second);
            if (kibouDt < sagyouTimeDt)
            {
                // 作業時間（From） ＞ 希望時間 の場合は作業日+1
                tempKibouDt = tempKibouDt.AddDays(1);
            }

            result = tempKibouDt;
            return true;
        }
        #endregion

        #region 並び替え
        /// <summary>
        /// 順番整列
        /// </summary>
        internal void SortIchiran()
        {
            try
            {
                // 明細行が空行１行の場合
                if (this.form.customDataGridView2.Rows.Count == 0)
                {
                    return;
                }

                // 順番の必須チェック
                bool isErrorFlag = false;
                for (int i = 0; i < this.form.customDataGridView2.Rows.Count; i++)
                {
                    DataGridViewRow row = this.form.customDataGridView2.Rows[i];
                    if (string.IsNullOrEmpty(row.Cells[ConstCls.AFTER_ROW_NO].FormattedValue.ToString()))
                    {
                        isErrorFlag = true;
                        break;
                    }
                }

                if (isErrorFlag)
                {
                    //コース明細が表示されていない場合順番確定処理を行わない。
                    if (0 < this.form.customDataGridView2.Rows.Count)
                    {
                        // 順番を採番
                        for (int j = 0; j < this.form.customDataGridView2.Rows.Count; j++)
                        {
                            this.form.customDataGridView2.Rows[j].Cells[ConstCls.AFTER_ROW_NO].Value = Int32.Parse((j + 1).ToString());
                        }
                    }
                }

                // 明細リストを「順番」の昇順に並びかえる
                var dtos = this.form.customDataGridView2.DataSource as List<SaitekikaDTO>;
                var sortedDtos = dtos.OrderBy(n => Int32.Parse(n.ROW_NO)).ToList();

                // 連番の再付番
                int index = 1;
                sortedDtos.ForEach(n => n.INDEX_NO = (index++).ToString());

                this.form.customDataGridView2.DataSource = sortedDtos;

                ChangeCellColor();
            }
            catch (Exception ex)
            {
                LogUtility.Error("SortIchiran", ex);
                throw;
            }
        }
        #endregion

        #region 上・下
        /// <summary>
        /// 上下移動処理
        /// </summary>
        /// <param name="currentIndex">インデックス</param>
        /// <param name="isUp">true:上, false:下</param>
        internal void MoveIchiran(int currentIndex, bool isUp)
        {
            if (isUp && currentIndex == 0)
            {
                // 最上段選択時、「上」ボタン押下は処理終了
                return;
            }

            if (!isUp && currentIndex == this.form.customDataGridView2.Rows.Count - 1)
            {
                // 最下段選択時、「下」ボタン押下は処理終了
                return;
            }

            var dtos = this.form.customDataGridView2.DataSource as List<SaitekikaDTO>;

            var selectDto = dtos.Where(n => n.INDEX_NO.Equals((currentIndex + 1).ToString())).First();
            int moveIndex = 0;
            if (isUp)
            {
                moveIndex = currentIndex - 1;
            }
            else
            {
                moveIndex = currentIndex + 1;
            }

            dtos.RemoveAt(currentIndex);
            dtos.Insert(moveIndex, selectDto);

            // 連番の再付番
            int index = 1;
            dtos.ForEach(n => n.INDEX_NO = (index++).ToString());

            this.form.customDataGridView2.DataSource = new List<SaitekikaDTO>();
            this.form.customDataGridView2.DataSource = dtos;

            this.form.customDataGridView2.CurrentCell = this.form.customDataGridView2[this.form.customDataGridView2.CurrentCell.ColumnIndex, moveIndex];
            ChangeCellColor();
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録チェック
        /// </summary>
        /// <returns>true:エラー有, false:エラー無</returns>
        internal bool HasError()
        {
            var hasError = false;

            var dtoList = this.form.customDataGridView2.DataSource as List<SaitekikaDTO>;

            if (dtoList.Count == 0)
            {
                this.msgLogic.MessageBoxShow("E061");
                return true;
            }

            return hasError;
        }

        /// <summary>
        /// 日付(yyyyMMdd)から曜日CD取得
        /// </summary>
        /// <param name="date">日付</param>
        /// <returns>曜日CD</returns>
        private int ConvertDayCD(string date)
        {
            DateTime dt;
            if (string.IsNullOrEmpty(date) || !DateTime.TryParse(date, out dt))
            {
                return 0;
            }

            var dayCd = dt.ToString("ddd");
            int result = 0;
            switch (dayCd)
            {
                case "月":
                    result = 1;
                    break;
                case "火":
                    result = 2;
                    break;
                case "水":
                    result = 3;
                    break;
                case "木":
                    result = 4;
                    break;
                case "金":
                    result = 5;
                    break;
                case "土":
                    result = 6;
                    break;
                case "日":
                    result = 7;
                    break;
            }
            return result;
        }

        /// <summary>
        /// 登録用データ作成
        /// </summary>
        internal void CreateEntity()
        {
            // 共通
            this.NAVI_LINK_STATUS.LINK_STATUS = 3;  // 3.配送計画取得済み
            var date = this.NAVI_LINK_STATUS.CREATE_DATE;
            var user = this.NAVI_LINK_STATUS.CREATE_USER;
            var pc = this.NAVI_LINK_STATUS.CREATE_PC;

            var dataBinderLink = new DataBinderLogic<T_NAVI_LINK_STATUS>(this.NAVI_LINK_STATUS);
            dataBinderLink.SetSystemProperty(this.NAVI_LINK_STATUS, false);

            this.NAVI_LINK_STATUS.CREATE_DATE = date;
            this.NAVI_LINK_STATUS.CREATE_USER = user;
            this.NAVI_LINK_STATUS.CREATE_PC = pc;

            if (IsCourseSaitekika())
            {
                // コースマスタ
                var dtoList = this.form.customDataGridView2.DataSource as List<SaitekikaDTO>;

                // NAVITIME新規追加現場対応
                foreach (var dto in dtoList)
                {
                    if (dto.IS_ADD_GENBA)
                    {
                        var detail = new M_COURSE_DETAIL();
                        detail.DAY_CD = this.NAVI_DELIVERY_DTO.DAY_CD;                          // 曜日CD
                        detail.COURSE_NAME_CD = this.NAVI_DELIVERY_DTO.COURSE_NAME_CD;          // コース名CD
                        detail.REC_ID = GetRecId(detail.DAY_CD.Value, detail.COURSE_NAME_CD);   // レコードID
                        detail.ROW_NO = SqlInt32.Parse(dto.INDEX_NO);                           // 行番号
                        detail.ROUND_NO = SqlInt32.Parse(dto.ROUND_NO);                         // 回数
                        detail.GYOUSHA_CD = dto.GYOUSHA_CD;                                     // 業者CD
                        detail.GENBA_CD = dto.GENBA_CD;                                         // 現場CD
                        detail.BIKOU = dto.BIKOU ?? string.Empty;                               // 備考

                        this.COURSE_DETAIL_LIST.Add(detail);
                    }
                    else
                    {
                        var entity = this.COURSE_DETAIL_LIST.Where(n => n.REC_ID.ToString().Equals(dto.ID)).First();

                        entity.ROW_NO = SqlInt32.Parse(dto.INDEX_NO);   // 行番号
                        entity.ROUND_NO = SqlInt32.Parse(dto.ROUND_NO); // 回数
                        entity.BIKOU = dto.BIKOU;                       // 備考
                    }
                }

                // コース_明細変更履歴
                if (this.COURSE_DETAIL_LIST.Any())
                {
                    foreach (var entity in this.COURSE_DETAIL_LIST)
                    {
                        var detail = new CHANGE_LOG_M_COURSE_DETAIL();
                        detail.SYSTEM_ID = GetSystemId(DENSHU_KBN.COURSE);
                        detail.DAY_CD = entity.DAY_CD;
                        detail.COURSE_NAME_CD = entity.COURSE_NAME_CD;
                        detail.REC_ID = entity.REC_ID;
                        detail.ROW_NO = entity.ROW_NO;
                        detail.ROUND_NO = entity.ROUND_NO;
                        detail.GYOUSHA_CD = entity.GYOUSHA_CD;
                        detail.GENBA_CD = entity.GENBA_CD;
                        detail.KIBOU_TIME = entity.KIBOU_TIME;
                        detail.SAGYOU_TIME_MINUTE = entity.SAGYOU_TIME_MINUTE;
                        detail.BIKOU = entity.BIKOU;

                        this.CHANGE_LOG_COURSE_DETAIL_LIST.Add(detail);
                    }
                }
            }
            else
            {
                // 定期配車
                var entry = new T_TEIKI_HAISHA_ENTRY();
                entry.SYSTEM_ID = this.PRE_TEIKI_HAISHA_ENTRY.SYSTEM_ID;
                entry.SEQ = this.PRE_TEIKI_HAISHA_ENTRY.SEQ + 1;
                entry.KYOTEN_CD = this.PRE_TEIKI_HAISHA_ENTRY.KYOTEN_CD;
                entry.TEIKI_HAISHA_NUMBER = this.PRE_TEIKI_HAISHA_ENTRY.TEIKI_HAISHA_NUMBER;
                entry.FURIKAE_HAISHA_KBN = this.PRE_TEIKI_HAISHA_ENTRY.FURIKAE_HAISHA_KBN;
                entry.DAY_CD = this.PRE_TEIKI_HAISHA_ENTRY.DAY_CD;
                entry.SAGYOU_DATE = this.PRE_TEIKI_HAISHA_ENTRY.SAGYOU_DATE;
                entry.SAGYOU_BEGIN_HOUR = this.PRE_TEIKI_HAISHA_ENTRY.SAGYOU_BEGIN_HOUR;
                entry.SAGYOU_BEGIN_MINUTE = this.PRE_TEIKI_HAISHA_ENTRY.SAGYOU_BEGIN_MINUTE;
                entry.SAGYOU_END_HOUR = this.PRE_TEIKI_HAISHA_ENTRY.SAGYOU_END_HOUR;
                entry.SAGYOU_END_MINUTE = this.PRE_TEIKI_HAISHA_ENTRY.SAGYOU_END_MINUTE;
                entry.COURSE_NAME_CD = this.PRE_TEIKI_HAISHA_ENTRY.COURSE_NAME_CD;
                entry.FURIKAE_HAISHA_KBN = this.PRE_TEIKI_HAISHA_ENTRY.FURIKAE_HAISHA_KBN;
                entry.DAY_CD = this.PRE_TEIKI_HAISHA_ENTRY.DAY_CD;
                entry.SHARYOU_CD = this.PRE_TEIKI_HAISHA_ENTRY.SHARYOU_CD;
                entry.SHASHU_CD = this.PRE_TEIKI_HAISHA_ENTRY.SHASHU_CD;
                entry.UNTENSHA_CD = this.PRE_TEIKI_HAISHA_ENTRY.UNTENSHA_CD;
                entry.UNPAN_GYOUSHA_CD = this.PRE_TEIKI_HAISHA_ENTRY.UNPAN_GYOUSHA_CD;
                entry.HOJOIN_CD = this.PRE_TEIKI_HAISHA_ENTRY.HOJOIN_CD;
                entry.SHUPPATSU_GYOUSHA_CD = this.NAVI_DELIVERY_DTO.SHUPPATSU_GYOUSHA_CD;
                entry.SHUPPATSU_GENBA_CD = this.NAVI_DELIVERY_DTO.SHUPPATSU_GENBA_CD;

                var dataBinderTeiki = new DataBinderLogic<T_TEIKI_HAISHA_ENTRY>(entry);
                dataBinderTeiki.SetSystemProperty(entry, false);
                entry.CREATE_USER = this.PRE_TEIKI_HAISHA_ENTRY.CREATE_USER;
                entry.CREATE_PC = this.PRE_TEIKI_HAISHA_ENTRY.CREATE_PC;
                entry.CREATE_DATE = this.PRE_TEIKI_HAISHA_ENTRY.CREATE_DATE;
                entry.DELETE_FLG = false;
                this.TEIKI_HAISHA_ENTRY = entry;

                //
                this.PRE_TEIKI_HAISHA_ENTRY.DELETE_FLG = true;
                this.PRE_TEIKI_HAISHA_ENTRY.UPDATE_USER = entry.UPDATE_USER;
                this.PRE_TEIKI_HAISHA_ENTRY.UPDATE_PC = entry.UPDATE_PC;
                this.PRE_TEIKI_HAISHA_ENTRY.UPDATE_DATE = entry.UPDATE_DATE;

                // 
                this.TEIKI_HAISHA_DETAIL_LIST.ForEach(n => n.SEQ = this.TEIKI_HAISHA_ENTRY.SEQ);

                var dtoList = this.form.customDataGridView2.DataSource as List<SaitekikaDTO>;
                foreach (var dto in dtoList)
                {
                    if (dto.IS_ADD_GENBA)
                    {
                        var detail = new T_TEIKI_HAISHA_DETAIL();
                        detail.SYSTEM_ID = entry.SYSTEM_ID;
                        detail.SEQ = entry.SEQ;
                        detail.DETAIL_SYSTEM_ID = GetSystemId(DENSHU_KBN.TEIKI_HAISHA);
                        detail.TEIKI_HAISHA_NUMBER = entry.TEIKI_HAISHA_NUMBER;
                        detail.ROW_NUMBER = SqlInt16.Parse(dto.INDEX_NO);                       // 行番号
                        detail.ROUND_NO = SqlInt32.Parse(dto.ROUND_NO);                         // 回数
                        detail.GYOUSHA_CD = dto.GYOUSHA_CD;                                     // 業者CD
                        detail.GENBA_CD = dto.GENBA_CD;                                         // 現場CD
                        if (!string.IsNullOrEmpty(dto.KIBOU_TIME))
                        {
                            detail.KIBOU_TIME = Convert.ToDateTime(dto.KIBOU_TIME);             // 希望時間
                        }
                        if (!string.IsNullOrEmpty(dto.SAGYOU_TIME_MINUTE))
                        {
                            detail.SAGYOU_TIME_MINUTE = Convert.ToInt16(dto.SAGYOU_TIME_MINUTE);// 作業時間_分
                        }
                        detail.MEISAI_BIKOU = dto.BIKOU ?? string.Empty;                        // 備考

                        this.TEIKI_HAISHA_DETAIL_LIST.Add(detail);
                    }
                    else
                    {
                        var detail = this.TEIKI_HAISHA_DETAIL_LIST.Where(n => n.DETAIL_SYSTEM_ID.ToString().Equals(dto.ID)).First();

                        detail.ROW_NUMBER = SqlInt16.Parse(dto.INDEX_NO);   // 行番号
                        detail.ROUND_NO = SqlInt32.Parse(dto.ROUND_NO);     // 回数
                        detail.MEISAI_BIKOU = dto.BIKOU;                    // 備考
                    }
                }

                //
                this.TEIKI_HAISHA_SHOUSAI_LIST = this.TEIKI_HAISHA_SHOUSAI_LIST ?? new List<T_TEIKI_HAISHA_SHOUSAI>();
                this.TEIKI_HAISHA_SHOUSAI_LIST.ForEach(n => n.SEQ = this.TEIKI_HAISHA_ENTRY.SEQ);

                //
                this.TEIKI_HAISHA_NIOROSHI_LIST = this.TEIKI_HAISHA_NIOROSHI_LIST ?? new List<T_TEIKI_HAISHA_NIOROSHI>();
                this.TEIKI_HAISHA_NIOROSHI_LIST.ForEach(n => n.SEQ = this.TEIKI_HAISHA_ENTRY.SEQ);
            }
        }

        /// <summary>
        /// データ登録処理
        /// </summary>
        /// <returns></returns>
        internal bool RegistData()
        {
            LogUtility.DebugMethodStart();

            bool result = false;
            try
            {
                using (var tran = new TransactionUtility())
                {
                    // コースマスタ
                    if (IsCourseSaitekika())
                    {
                        foreach (var detail in this.COURSE_DETAIL_LIST)
                        {
                            if (detail.TIME_STAMP == null)
                            {
                                // TIME_STAMPがNULLの場合は、NAVITIME側で新規作成されたデータと判定
                                this.courseDetailDao.Insert(detail);
                            }
                            else
                            {
                                this.courseDetailDao.Update(detail);
                            }
                        }
                        this.COURSE_DETAIL_DELETE_LIST.ForEach(n => this.courseDetailDao.Delete(n));
                        this.CHANGE_LOG_COURSE_DETAIL_LIST.ForEach(n => this.changeLogCourseDetailDao.Insert(n));
                    }
                    else
                    {
                        // 定期配車
                        this.teikiHaishaEntryDAO.Update(this.PRE_TEIKI_HAISHA_ENTRY);
                        this.teikiHaishaEntryDAO.Insert(this.TEIKI_HAISHA_ENTRY);
                        this.TEIKI_HAISHA_DETAIL_LIST.ForEach(n => this.teikiHaishaDetailDAO.Insert(n));
                        this.TEIKI_HAISHA_SHOUSAI_LIST.ForEach(n => this.teikiHaishaShousaiDAO.Insert(n));
                        this.TEIKI_HAISHA_NIOROSHI_LIST.ForEach(n => this.teikiHaishaNioroshiDAO.Insert(n));
                    }

                    tran.Commit();
                    result = true;
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Warn(ex1); //排他は警告
                this.msgLogic.MessageBoxShow("E080");
                result = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error(ex2); //その他SQLエラー
                this.msgLogic.MessageBoxShow("E093");
                result = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex); //その他エラー
                this.msgLogic.MessageBoxShow("E245");
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return result;
        }

        /// <summary>
        /// コース明細のレコードIDの最大値+1を取得する
        /// </summary>
        /// <param name="dayCd">曜日CD</param>
        /// <param name="courseNameCd">コース名称CD</param>
        /// <returns></returns>
        private int GetRecId(int dayCd, string courseNameCd)
        {
            if (dayCd == 0 || string.IsNullOrEmpty(courseNameCd))
            {
                return 0;
            }

            int recId = 1;
            string sql = string.Format("SELECT ISNULL(MAX(REC_ID),0) FROM M_COURSE_DETAIL WHERE DAY_CD = {0} AND COURSE_NAME_CD = '{1}'", dayCd, courseNameCd);
            recId = this.dao.ExecuteForStringSql(sql);
            return recId + 1;
        }

        /// <summary>
        /// システムIDの最大値+1を取得する
        /// </summary>
        /// <param name="denshuKbn">伝種区分</param>
        /// <returns>システムID</returns>
        private long GetSystemId(DENSHU_KBN denshuKbn)
        {
            long systemId = 1;

            using (Transaction tran = new Transaction())
            {
                var entity = new S_NUMBER_SYSTEM();
                entity.DENSHU_KBN_CD = (Int16)denshuKbn;

                // テーブルロックをかけつつ、既存データがあるかを検索する。
                var updateEntity = this.numberSystemDao.GetNumberSystemDataWithTableLock(entity);
                systemId = this.numberSystemDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_SYSTEM();
                    updateEntity.DENSHU_KBN_CD = (Int16)denshuKbn;
                    updateEntity.CURRENT_NUMBER = systemId;
                    updateEntity.DELETE_FLG = false;
                    var dataBinderEntry = new DataBinderLogic<S_NUMBER_SYSTEM>(updateEntity);
                    dataBinderEntry.SetSystemProperty(updateEntity, false);

                    this.numberSystemDao.Insert(updateEntity);
                }
                else
                {
                    updateEntity.CURRENT_NUMBER = systemId;
                    this.numberSystemDao.Update(updateEntity);
                }
                tran.Commit();
            }

            return systemId;
        }

        /// <summary>
        /// 定期配車番号採番処理
        /// </summary>
        /// <returns>最大ID+1</returns>
        private int GetTeikiHaishaNumber()
        {
            int returnInt = -1;

            using (Transaction tran = new Transaction())
            {
                // 処理区分：120（定期配車）
                var entity = new S_NUMBER_DENSHU();
                entity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_HAISHA;

                // テーブルロックをかけつつ、既存データがあるかを検索する。
                var updateEntity = this.numberDenshuDao.GetNumberDenshuDataWithTableLock(entity);
                // 伝種連番の最大値+1を取得する
                returnInt = this.numberDenshuDao.GetMaxPlusKey(entity);

                if (updateEntity == null || updateEntity.CURRENT_NUMBER < 1)
                {
                    updateEntity = new S_NUMBER_DENSHU();
                    updateEntity.DENSHU_KBN_CD = (Int16)r_framework.Const.DENSHU_KBN.TEIKI_HAISHA;
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

                // コミット
                tran.Commit();
            }
            return returnInt;
        }
        #endregion

        #region 同一作業者か判定
        /// <summary>
        /// 表示中の作業者と指定されたシステムIDに紐付く作業者が同一か判定
        /// </summary>
        /// <param name="systemId">システムID</param>
        /// <returns>true:同じ, false:違う</returns>
        internal bool IsSameSagyousha(long systemId)
        {
            var dto = this.courseSaitekikaDAO.GetDataByCd(systemId);
            if (dto == null)
            {
                // 存在しない場合はtrueを返す
                return true;
            }

            var result = this.NAVI_DELIVERY_DTO.SAGYOUSHA_CD.Equals(dto.SAGYOUSHA_CD);
            return result;
        }
        #endregion

        #region CSV出力
        /// <summary>
        /// CSV作成
        /// </summary>
        /// <returns></returns>
        internal string CreateCSV()
        {
            string filePath = string.Empty;

            try
            {
                // 出力データを整形する。
                var dataList = new List<string>();

                // ヘッダ情報(空行で構わない)
                dataList.Add(string.Empty);

                // 便毎の削除データとして作成
                StringBuilder sb = new StringBuilder();
                sb.Append(this.SearchTargetDate)                                // 対象日
                    .AppendFormat(",{0}", this.NAVI_DELIVERY_DTO.SAGYOUSHA_CD)  // 作業者ユーザーコード
                    .Append(",4")                                               // スケジュール作成区分(全便:2　単便:4)
                    .Append(",")                                                // 訪問順番
                    .Append(",")                                                // 訪問先コード
                    .Append(",")                                                // 案件名称
                    .Append(",")                                                // 案件詳細
                    .Append(",")                                                // 到着希望日
                    .Append(",")                                                // 到着希望時刻
                    .Append(",")                                                // 作業時間(分)
                    .Append(",1")                                               // 更新モード(1:削除)
                    .Append(",")                                                // 案件コード
                    .Append(",")                                                // ルート作成方法
                    .Append(",")                                                // 出発営業所コード
                    .Append(",")                                                // 到着営業所コード
                    .Append(",")                                                // 出発時刻(デモ用なのでとりあえず今の時間を出発とする)
                    .Append(",")                                                // 到着時刻
                    .Append(",")                                                // 渋滞考慮
                    .Append(",")                                                // スマートIC考慮
                    .Append(",")                                                // 車両タイプ
                    .Append(",")                                                // 優先
                    .AppendFormat(",{0}", this.NAVI_DELIVERY_DTO.BIN_NO.Value)  // 便番号(全便:未指定　単便:便番号指定)
                    .Append(",");                                               // エラー事由
                dataList.Add(sb.ToString());

                filePath = navilogic.OutputCSV(ConstCls.CSV_FILE_NAME, dataList);

                return filePath;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateCSV", ex);
                return filePath;
            }
        }
        #endregion

        #region API通信(登録)
        /// <summary>
        /// 配車計画情報一括登録のAPI
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        internal int RegistAPI(string filePath)
        {
            try
            {
                string msg = string.Empty;

                // 配送計画送信
                var postDto = new NaviRequestDto();
                postDto.filePath = filePath;

                var connectDto = this.naviConnectDao.GetDataByContentName(NaviConst.CONTENT_NAME_UPLOAD);
                if (connectDto == null)
                {
                    return 0;
                }

                RES_UPLOAD dto = new RES_UPLOAD();
                var result = this.navilogic.HttpPOST<RES_UPLOAD>(connectDto.URL, connectDto.CONTENT_TYPE, postDto, out dto);
                if (!result || dto == null)
                {
                    return 0;
                }

                if (dto.Success)
                {
                    return int.Parse(dto.ProcessingId);
                }
                else
                {
                    // 通るパターンがよく分からないけどとりあえず設置
                    // ハッシュ値
                    foreach (var err in dto.ErrorMessage)
                    {
                        msg += err + System.Environment.NewLine;
                    }
                    this.msgLogic.MessageBoxShowError(msg);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
        }
        #endregion

        #region 配送計画関連のデータ更新
        /// <summary>
        /// 配送計画関連のデータ更新
        /// </summary>
        /// <returns></returns>
        internal bool RegistDataNavi()
        {
            LogUtility.DebugMethodStart();
            bool result = false;

            try
            {
                using (var tran = new TransactionUtility())
                {
                    // 共通
                    this.naviLinkStatusDao.Update(this.NAVI_LINK_STATUS);

                    string sql = string.Format("DELETE FROM T_NAVI_COLLABORATION_EVENTS WHERE SYSTEM_ID = {0}", this.NAVI_DELIVERY_DTO.SYSTEM_ID);
                    this.dao.ExecuteForStringSql(sql);

                    tran.Commit();
                    result = true;
                }
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Warn(ex1); //排他は警告
                this.msgLogic.MessageBoxShow("E080");
                result = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error(ex2); //その他SQLエラー
                this.msgLogic.MessageBoxShow("E093");
                result = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex); //その他エラー
                this.msgLogic.MessageBoxShow("E245");
                result = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }

            return result;
        }
        #endregion

        #region 案件削除
        /// <summary>
        /// 案件削除
        /// </summary>
        internal void DeleteMatter()
        {
            // 入力チェック
            if (HasErrorMatterCode())
            {
                return;
            }

            if (DialogResult.Yes != this.msgLogic.MessageBoxShow("C026"))
            {
                return;
            }

            long matterCodeFrom = long.Parse(this.form.MATTER_CODE_FROM.Text);
            long matterCodeTo = 0;
            if (!string.IsNullOrEmpty(this.form.MATTER_CODE_TO.Text))
            {
                matterCodeTo = long.Parse(this.form.MATTER_CODE_TO.Text);
            }

            long matterCode = matterCodeFrom;
            List<string> errList = new List<string>();
            int i = 0;
            var sb = new StringBuilder();
            do
            {
                // 案件削除
                var postDto = new NaviRequestDto();
                postDto.matterCode = matterCode.ToString();
                RES_DELETE_MATTER dto = new RES_DELETE_MATTER();
                var result = this.navilogic.HttpPOST<RES_DELETE_MATTER>("/" + NaviConst.naviDELETE_MATTER, WebAPI_ContentType.APPLICATION_X_WWW_FORM_URLENCODED, postDto, out dto);

                if (!result || dto == null)
                {
                    if (!string.IsNullOrEmpty(sb.ToString()))
                    {
                        errList.Add(sb.ToString());
                    }
                    errList.ForEach(n => this.msgLogic.MessageBoxShowError(n));

                    this.msgLogic.MessageBoxShowError(string.Format("案件コード{0}：異常終了のため処理終了します", matterCode));
                    // 異常終了時はNULLのため、処理終了
                    return;
                }

                if (!dto.Success)
                {
                    sb.AppendLine(string.Format("案件コード{0}：処理失敗 {1}", string.Format("{0:D6}", matterCode), dto.ErrorMessage.FirstOrDefault()));
                    i++;

                    if (i == 10)
                    {
                        // 10件単位でまとめてエラー出力
                        errList.Add(sb.ToString());

                        // 変数初期化
                        i = 0;
                        sb = new StringBuilder();
                    }
                }

                if (matterCodeTo == 0)
                {
                    // Fromのみ入力時は処理終了
                    break;
                }

                matterCode++;
            } while (matterCode <= matterCodeTo);

            if (!string.IsNullOrEmpty(sb.ToString()))
            {
                errList.Add(sb.ToString());
            }

            if (errList.Any())
            {
                errList.ForEach(n => this.msgLogic.MessageBoxShowError(n));
                return;
            }

            this.msgLogic.MessageBoxShowInformation("案件削除完了しました");
        }

        /// <summary>
        /// 案件コード削除時のチェック処理
        /// </summary>
        /// <returns></returns>
        private bool HasErrorMatterCode()
        {
            if (string.IsNullOrEmpty(this.form.MATTER_CODE_FROM.Text))
            {
                this.form.MATTER_CODE_FROM.IsInputErrorOccured = true;
                this.msgLogic.MessageBoxShow("E001", "案件コードFrom");
                return true;
            }

            if (!string.IsNullOrEmpty(this.form.MATTER_CODE_TO.Text))
            {
                long cdFrom = 0;
                long cdTo = 0;

                var fromResult = long.TryParse(this.form.MATTER_CODE_FROM.Text, out cdFrom);
                var toResult = long.TryParse(this.form.MATTER_CODE_TO.Text, out cdTo);

                if (fromResult && toResult)
                {
                    if (cdTo < cdFrom)
                    {
                        this.form.MATTER_CODE_FROM.IsInputErrorOccured = true;
                        this.form.MATTER_CODE_TO.IsInputErrorOccured = true;

                        this.msgLogic.MessageBoxShow("E032", "案件コードFrom", "案件コードTo");
                        return true;
                    }
                }
            }

            return false;
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
