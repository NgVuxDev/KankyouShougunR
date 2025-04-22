using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Data;
using System.Windows.Forms;
using Seasar.Quill.Attrs;
using GrapeCity.Win.MultiRow;
using System.Data.SqlTypes;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Shougun.Core.Common.TenpyouTankaIkatsuHenkou.APP;
using Shougun.Core.Common.TenpyouTankaIkatsuHenkou.DTO;
using Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Dao;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using r_framework.FormManager;
using Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Utility;
using r_framework.CustomControl;
namespace Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {
        #region フィールド
        /// <summary>
        /// DTO
        /// </summary>
        private DTOCls dto;

        /// <summary>
        /// Header
        /// </summary>
        private UIHeader header;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal BusinessBaseForm parentForm;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// BusinessCommonのDBAccesser
        /// </summary>
        private Shougun.Core.Common.BusinessCommon.DBAccessor commonAccesser;

        /// <summary>
        /// 伝票単価明細
        /// </summary>
        private GetDenpyouTankaDetailDao denpyouTankaDetailDao;

        /// <summary>
        /// 受入単価明細
        /// </summary>
        private GetUkeireTankaSabunDetailDao ukeireTankaSabunDetailDao;

        /// <summary>
        /// 出荷単価明細
        /// </summary>
        private GetShukkaTankaSabunDetailDao shukkaTankaSabunDetailDao;

        /// <summary>
        /// 売上支払単価明細
        /// </summary>
        private GetUrShTankaSabunDetailDao urshTankaSabunDetailDao;

        /// <summary>
        /// 出荷DETAILのDao
        /// </summary>
        private T_SHUKKA_DETAILDao daoShukkaDetail;

        /// <summary>
        /// 出荷ENTRYのDao
        /// </summary>
        private T_SHUKKA_ENTRYDao daoShukkaEntry;

        /// <summary>
        /// 受入DETAILのDao
        /// </summary>
        private T_UKEIRE_DETAILDao daoUkeireDetail;

        /// <summary>
        /// 受入ENTRYのDao
        /// </summary>
        private T_UKEIRE_ENTRYDao daoUkeireEntry;

        /// <summary>
        /// 売上/支払DETAILのDao
        /// </summary>
        private T_UR_SH_DETAILDao daoUrshDetail;

        /// <summary>
        /// 売上/支払ENTRYのDao
        /// </summary>
        private T_UR_SH_ENTRYDao daoUrshEntry;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        public M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// ButtonSetting.xmlファイルパス
        /// </summary>
        private readonly string ButtonInfoXmlPath = "Shougun.Core.Common.TenpyouTankaIkatsuHenkou.Setting.ButtonSetting.xml";

        /// <summary>
        /// HeaderFormのクリアコントロール名一覧
        /// </summary>
        private string[] clearHeaderControlNames = { "alertNumber", "ReadDataNumber" };

        /// <summary>
        /// UIFormのクリアコントロール名一覧
        /// </summary>
        private string[] clearUiFormControlNames = { "KYOTEN_CD","KYOTEN_NAME_RYAKU","DENPYOU_DATE_FROM","DENPYOU_DATE_TO",
                                                     "TORIHIKISAKI_CD","TORIHIKISAKI_NAME","GYOUSHA_CD","GYOUSHA_NAME","GENBA_CD","GENBA_NAME","HINMEI_CD","HINMEI_NAME","IKATSU_TANKA",
                                                     "UNPAN_GYOUSHA_CD","UNPAN_GYOUSHA_NAME","NIOROSHI_GYOUSHA_CD","NIOROSHI_GYOUSHA_NAME","NIOROSHI_GENBA_CD","NIOROSHI_GENBA_NAME","UNIT_CD","UNIT_NAME" };

        /// <summary>
        /// 検索条件の結果
        /// </summary>
        public DataTable SearchDenpyouTankaDetailResult { get; set; }

        /// <summary>
        /// 更新条件
        /// </summary>
        public List<T_ITAKU_MEMO_IKKATSU_ENTRY> ItakuMemoIkkatsuEntryList { get; set; }

        /// <summary>
        /// 受入を削除Entity
        /// </summary>
        private T_UKEIRE_ENTRY delUkeireEntryEntity = new T_UKEIRE_ENTRY();

        /// <summary>
        /// 出荷を削除Entity
        /// </summary>
        private T_SHUKKA_ENTRY delShukkaEntryEntity = new T_SHUKKA_ENTRY();

        /// <summary>
        /// 売上／支払を削除Entity
        /// </summary>
        private T_UR_SH_ENTRY delUrshEntryEntity = new T_UR_SH_ENTRY();

        /// <summary>
        /// 受入をインサートEntity
        /// </summary>
        private T_UKEIRE_ENTRY insUkeireEntryEntity = new T_UKEIRE_ENTRY();

        /// <summary>
        /// 出荷をインサートEntity
        /// </summary>
        private T_SHUKKA_ENTRY insShukkaEntryEntity = new T_SHUKKA_ENTRY();

        /// <summary>
        /// 売上／支払をインサートEntity
        /// </summary>
        private T_UR_SH_ENTRY insUrshEntryEntity = new T_UR_SH_ENTRY();

        /// <summary>
        /// 受入をインサートDetail
        /// </summary>
        private T_UKEIRE_DETAIL insUkeireEntryDetail = new T_UKEIRE_DETAIL();

        /// <summary>
        /// 出荷をインサートDetail
        /// </summary>
        private T_SHUKKA_DETAIL insShukkaEntryDetail = new T_SHUKKA_DETAIL();

        /// <summary>
        /// 売上／支払をインサートDetail
        /// </summary>
        private T_UR_SH_DETAIL insUrshEntryDetail = new T_UR_SH_DETAIL();

        /// <summary>
        /// 削除受付入力Entityを格納リスト
        /// </summary>
        private List<T_UKEIRE_ENTRY> delUkeireEntryEntityList = new List<T_UKEIRE_ENTRY>();

        /// <summary>
        /// 削除出荷入力Entityを格納リスト
        /// </summary>
        private List<T_SHUKKA_ENTRY> delShukkaEntryEntityList = new List<T_SHUKKA_ENTRY>();

        /// <summary>
        /// 削除売上／支払入力Entityを格納リスト
        /// </summary>
        private List<T_UR_SH_ENTRY> delUrshEntryEntityList = new List<T_UR_SH_ENTRY>();

        /// <summary>
        /// インサート受付入力Entityを格納リスト
        /// </summary>
        private List<T_UKEIRE_ENTRY> insUkeireEntryEntityList = new List<T_UKEIRE_ENTRY>();

        /// <summary>
        /// インサート出荷入力Entityを格納リスト
        /// </summary>
        private List<T_SHUKKA_ENTRY> insShukkaEntryEntityList = new List<T_SHUKKA_ENTRY>();

        /// <summary>
        /// インサート売上／支払入力Entityを格納リスト
        /// </summary>
        private List<T_UR_SH_ENTRY> insUrshEntryEntityList = new List<T_UR_SH_ENTRY>();

        /// <summary>
        /// インサート受付入力明細Entityを格納リスト
        /// </summary>
        private List<T_UKEIRE_DETAIL> insUkeireEntryDetailList = new List<T_UKEIRE_DETAIL>();

        /// <summary>
        /// インサート出荷入力明細Entityを格納リスト
        /// </summary>
        private List<T_SHUKKA_DETAIL> insShukkaEntryDetailList = new List<T_SHUKKA_DETAIL>();

        /// <summary>
        /// インサート売上／支払入力明細Entityを格納リスト
        /// </summary>
        private List<T_UR_SH_DETAIL> insUrshEntryDetailList = new List<T_UR_SH_DETAIL>();

        /// <summary>
        /// システム情報に設定されたアラート件数
        /// </summary>
        public int alertCount { get; set; }

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        internal MessageBoxShowLogic msgLogic;

        /// <summary>
        /// ControlUtility
        /// </summary>
        internal ControlUtility controlUtil;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            try
            {
                LogUtility.DebugMethodStart(targetForm);
                // メインフォーム
                this.form = targetForm;
                // ControlUtility
                this.controlUtil = new ControlUtility();
                // DTO
                this.dto = new DTOCls();
                // システム情報Dao
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
                this.commonAccesser = new Shougun.Core.Common.BusinessCommon.DBAccessor();
                // メッセージ表示オブジェクト
                this.msgLogic = new MessageBoxShowLogic();
                //伝票単価明細
                this.denpyouTankaDetailDao = DaoInitUtility.GetComponent<GetDenpyouTankaDetailDao>();
                //受入単価明細
                this.ukeireTankaSabunDetailDao = DaoInitUtility.GetComponent<GetUkeireTankaSabunDetailDao>();
                //出荷単価明細
                this.shukkaTankaSabunDetailDao = DaoInitUtility.GetComponent<GetShukkaTankaSabunDetailDao>();
                //売上支払単価明細
                this.urshTankaSabunDetailDao = DaoInitUtility.GetComponent<GetUrShTankaSabunDetailDao>();
                //出荷DETAILのDao
                this.daoShukkaDetail = DaoInitUtility.GetComponent<T_SHUKKA_DETAILDao>();
                //出荷ENTRYのDao
                this.daoShukkaEntry = DaoInitUtility.GetComponent<T_SHUKKA_ENTRYDao>();
                //受入DETAILのDao
                this.daoUkeireDetail = DaoInitUtility.GetComponent<T_UKEIRE_DETAILDao>();
                //受入ENTRYのDao
                this.daoUkeireEntry = DaoInitUtility.GetComponent<T_UKEIRE_ENTRYDao>();
                //売上/支払DETAILのDao
                this.daoUrshDetail = DaoInitUtility.GetComponent<T_UR_SH_DETAILDao>();
                //売上/支払ENTRYのDao
                this.daoUrshEntry = DaoInitUtility.GetComponent<T_UR_SH_ENTRYDao>();
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

        #region 初期処理

        #region  画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                this.parentForm = (BusinessBaseForm)this.form.Parent;
                // ヘッダー項目
                this.header = (UIHeader)((BusinessBaseForm)this.form.ParentForm).headerForm;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化
                this.EventInit(parentForm);

                //システム情報を取得し、初期値をセットする
                this.HearerSysInfoInit();
                // 画面コントロール状態設定
                InitControlMode();

                // 画面初期値設定
                InitData();
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

        #region 画面コントロール状態設定
        /// <summary>
        /// 画面コントロール状態設定
        /// </summary>
        private void InitControlMode()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                //隠しボッタン（使用しない）
                parentForm.bt_process1.Visible = false;
                parentForm.bt_process2.Visible = false;
                parentForm.bt_process3.Visible = false;
                parentForm.bt_process4.Visible = false;
                parentForm.bt_process5.Visible = false;
                parentForm.lb_process.Visible = false;
                parentForm.txb_process.Visible = false;
                parentForm.ProcessButtonPanel.Visible = false;
                parentForm.bt_func9.Enabled = false;
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

        #region 画面初期値設定
        /// <summary>
        /// 画面初期値設定
        /// </summary>
        internal void InitData()
        {
            try
            {
                LogUtility.DebugMethodStart();
                //変更方法
                this.form.txtNum_HenkouHouhou.Text = "1";
                //伝票日付
                this.form.txtNum_DenpyouHiduke.Text = "1";
                //確定区分
                this.form.txtNum_KakuteiKubun.Text = "1";
                //伝票種類
                this.form.txtNum_DenpyouShurui.Text = "4";
                //伝票区分
                this.form.txtNum_DenpyouKubun.Text = "1";
                //読み取りデータ数
                this.header.ReadDataNumber.Text = "0";

                //日付取得
                DateTime now = this.parentForm.sysDate;
                DateTime dt_str = new DateTime(now.Year, now.Month, 1);
                DateTime dt_end = dt_str.AddMonths(1).AddDays(-1);
                //当月の一日
                this.form.DENPYOU_DATE_FROM.Value = dt_str;
                //当月の末日
                this.form.DENPYOU_DATE_TO.Value = dt_end;
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

        #region システム情報初期化処理
        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void HearerSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();
                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];


                    this.header.alertNumber.Text = string.Format("{0:" + "#,##0" + "}", int.Parse(this.sysInfoEntity.ICHIRAN_ALERT_KENSUU.Value.ToString()));

                    // システム情報からアラート件数を取得
                    this.alertCount = (int)this.sysInfoEntity.ICHIRAN_ALERT_KENSUU;
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

        #region ボタン初期化
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (BusinessBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

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

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var controlUtil = new ControlUtility();
                foreach (var button in buttonSetting)
                {
                    var cont = controlUtil.FindControl(parentForm, button.ButtonName);
                    switch (this.form.WindowType)
                    {
                        case WINDOW_TYPE.NEW_WINDOW_FLAG:
                            cont.Text = button.NewButtonName;
                            break;
                        case WINDOW_TYPE.REFERENCE_WINDOW_FLAG:
                            cont.Text = button.ReferButtonName;
                            break;
                        case WINDOW_TYPE.UPDATE_WINDOW_FLAG:
                            cont.Text = button.UpdateButtonName;
                            break;
                        case WINDOW_TYPE.DELETE_WINDOW_FLAG:
                            cont.Text = button.DeleteButtonName;
                            break;
                    }
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
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit(BusinessBaseForm parentForm)
        {
            try
            {
                LogUtility.DebugMethodStart(parentForm);

                //検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.Search);

                //登録ボタン(F9)イベント生成
                //this.form.C_Regist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

                // 20141201 teikyou ダブルクリックを追加する　start
                this.form.DENPYOU_DATE_TO.MouseDoubleClick += new MouseEventHandler(DENPYOU_DATE_TO_MouseDoubleClick);
                // 20141201 teikyou ダブルクリックを追加する　end

                /// 20141203 Houkakou 「伝票単価一括変更」の日付チェックを追加する　start
                this.form.DENPYOU_DATE_FROM.Leave += new System.EventHandler(DENPYOU_DATE_FROM_Leave);
                this.form.DENPYOU_DATE_TO.Leave += new System.EventHandler(DENPYOU_DATE_TO_Leave);
                /// 20141203 Houkakou 「伝票単価一括変更」の日付チェックを追加する　end 

                this.form.UNPAN_GYOUSHA_CD.Validated += new EventHandler(this.form.UNPAN_GYOUSHA_CD_Validated);
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

        #endregion

        #region  データ処理

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();

                this.dto = new DTOCls();

                //検索条件設定  
                //拠点
                dto.KYOTEN_CD = ChgNullToValue(this.form.KYOTEN_CD.Text.ToString().Trim(), null);
                //伝票日付の場合
                if (this.form.txtNum_DenpyouHiduke.Text.ToString().Trim().Equals("1"))
                {
                    dto.DENPYOU_DATE_FROM = string.IsNullOrEmpty(this.form.DENPYOU_DATE_FROM.Text.Trim()) ? null : this.form.DENPYOU_DATE_FROM.Text.Substring(0, 10).Trim();
                    dto.DENPYOU_DATE_TO = string.IsNullOrEmpty(this.form.DENPYOU_DATE_TO.Text.Trim()) ? null : this.form.DENPYOU_DATE_TO.Text.Substring(0, 10).Trim();
                }
                //入力日付の場合
                else if (this.form.txtNum_DenpyouHiduke.Text.ToString().Trim().Equals("2"))
                {
                    dto.CREATE_DATE_FROM = string.IsNullOrEmpty(this.form.DENPYOU_DATE_FROM.Text.Trim()) ? null : this.form.DENPYOU_DATE_FROM.Text.Substring(0, 10).Trim();
                    dto.CREATE_DATE_TO = string.IsNullOrEmpty(this.form.DENPYOU_DATE_TO.Text.Trim()) ? null : this.form.DENPYOU_DATE_TO.Text.Substring(0, 10).Trim();
                }
                //取引先
                dto.TORIHIKISAKI_CD = ChgNullToValue(this.form.TORIHIKISAKI_CD.Text.ToString().Trim(), null);
                //業者
                dto.GYOUSHA_CD = ChgNullToValue(this.form.GYOUSHA_CD.Text.ToString().Trim(), null);
                //現場
                dto.GENBA_CD = ChgNullToValue(this.form.GENBA_CD.Text.ToString().Trim(), null);
                //品名
                dto.HINMEI_CD = ChgNullToValue(this.form.HINMEI_CD.Text.ToString().Trim(), null);
                //確定区分(1.すべて　2.未確定　3.確定済み)
                if (this.form.txtNum_KakuteiKubun.Text.ToString().Trim().Equals("2"))
                {
                    dto.KAKUTEI_KBN = "2";    //未確定
                }
                else if (this.form.txtNum_KakuteiKubun.Text.ToString().Trim().Equals("3"))
                {
                    dto.KAKUTEI_KBN = "1";　  //確定
                }
                //伝票区分(1.売上   2.支払)
                if (this.form.txtNum_DenpyouKubun.Text.ToString().Trim().Equals("1"))
                {
                    dto.DENPYOU_KBN_CD = "1";          //売上
                }
                else if (this.form.txtNum_DenpyouKubun.Text.ToString().Trim().Equals("2"))
                {
                    dto.DENPYOU_KBN_CD = "2";          //支払
                }
                //伝票種類（1:受入　2:出荷　3:　売上/支払　4:全て）
                if (this.form.txtNum_DenpyouShurui.Text.ToString().Trim().Equals("1"))
                {
                    dto.DENPYOU_SHURUI = "1";
                }
                else if (this.form.txtNum_DenpyouShurui.Text.ToString().Trim().Equals("2"))
                {
                    dto.DENPYOU_SHURUI = "2";
                }
                else if (this.form.txtNum_DenpyouShurui.Text.ToString().Trim().Equals("3"))
                {
                    dto.DENPYOU_SHURUI = "3";
                }
                else if (this.form.txtNum_DenpyouShurui.Text.ToString().Trim().Equals("4"))
                {
                    dto.DENPYOU_SHURUI = "4";
                }
                //運搬業者
                dto.UNPAN_GYOUSHA_CD = ChgNullToValue(this.form.UNPAN_GYOUSHA_CD.Text.ToString().Trim(), null);
                //荷降業者
                dto.NIOROSHI_GYOUSHA_CD = ChgNullToValue(this.form.NIOROSHI_GYOUSHA_CD.Text.ToString().Trim(), null);
                //荷降現場
                dto.NIOROSHI_GENBA_CD = ChgNullToValue(this.form.NIOROSHI_GENBA_CD.Text.ToString().Trim(), null);
                //単位
                dto.UNIT_CD = ChgNullToValue(this.form.UNIT_CD.Text.ToString().Trim(), null);

                //検索SQLを実行する。
                DataTable dt = denpyouTankaDetailDao.GetDataToDataTable(dto);

                //検索条件の結果
                SearchDenpyouTankaDetailResult = dt;

                // 明細クリア
                this.form.grdIchiran.Rows.Clear();
                this.header.ReadDataNumber.Text = "0";

                if (dt.Rows.Count == 0)
                {
                    return count;
                }

                count = dt.Rows.Count;
                return count;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();
                DataTable table = this.SearchDenpyouTankaDetailResult;

                // 明細クリア
                this.form.grdIchiran.Rows.Clear();
                this.header.ReadDataNumber.Text = "0";

                if (table != null)
                {
                    table.BeginLoadData();
                    //Headarのアラート件数を処理する。
                    DialogResult result = DialogResult.Yes;
                    //読み取り明細データの件数
                    this.header.ReadDataNumber.Text = this.SearchDenpyouTankaDetailResult == null ? "0"
                        : (string.IsNullOrEmpty(this.SearchDenpyouTankaDetailResult.Rows.Count.ToString()) ? "0"
                        : this.SearchDenpyouTankaDetailResult.Rows.Count.ToString());

                    //読み取り明細データの件数は最大件数を超えるとき、提示メッセージを表示する。
                    string strAlertCount = this.header.alertNumber.Text.ToString();
                    if (!string.IsNullOrEmpty(strAlertCount) && !strAlertCount.Equals("0") && int.Parse(strAlertCount.Replace(",", "")) < table.Rows.Count)
                    {
                        MessageBoxShowLogic showLogic = new MessageBoxShowLogic();
                        result = showLogic.MessageBoxShow("C025");
                    }
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }
                    // 画面にデータを表示

                    // 明細行を追加
                    this.form.grdIchiran.Rows.Add(table.Rows.Count);

                    // 検索結果設定
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        // 伝票日付
                        this.form.grdIchiran[i, "DENPYOU_DATE"].Value = this.ChgDBNullToValue(table.Rows[i]["DENPYOU_DATE"], string.Empty);
                        // 取引先CD
                        this.form.grdIchiran[i, "TORIHIKISAKI_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["TORIHIKISAKI_CD"], string.Empty);
                        // 取引先名
                        this.form.grdIchiran[i, "TORIHIKISAKI_NAME"].Value = this.ChgDBNullToValue(table.Rows[i]["TORIHIKISAKI_NAME"], string.Empty);
                        // 現場CD
                        this.form.grdIchiran[i, "GENBA_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["GENBA_CD"], string.Empty);
                        // 現場名
                        this.form.grdIchiran[i, "GENBA_NAME"].Value = this.ChgDBNullToValue(table.Rows[i]["GENBA_NAME"], string.Empty);
                        // 荷降業者CD
                        this.form.grdIchiran[i, "NIOROSHI_GYOUSHA_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["NIOROSHI_GYOUSHA_CD"], string.Empty);
                        // 荷降業者名
                        this.form.grdIchiran[i, "NIOROSHI_GYOUSHA_NAME"].Value = this.ChgDBNullToValue(table.Rows[i]["NIOROSHI_GYOUSHA_NAME"], string.Empty);
                        // 伝票種類
                        this.form.grdIchiran[i, "DENPYOU_SHURUI"].Value = this.ChgDBNullToValue(table.Rows[i]["DENPYOU_SHURUI"], string.Empty);
                        // 業者CD
                        this.form.grdIchiran[i, "GYOUSHA_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["GYOUSHA_CD"], string.Empty);
                        // 業者名
                        this.form.grdIchiran[i, "GYOUSHA_NAME"].Value = this.ChgDBNullToValue(table.Rows[i]["GYOUSHA_NAME"], string.Empty);
                        // 運搬業者CD
                        this.form.grdIchiran[i, "UNPAN_GYOUSHA_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["UNPAN_GYOUSHA_CD"], string.Empty);
                        // 運搬業者名
                        this.form.grdIchiran[i, "UNPAN_GYOUSHA_NAME"].Value = this.ChgDBNullToValue(table.Rows[i]["UNPAN_GYOUSHA_NAME"], string.Empty);
                        // 荷降現場CD
                        this.form.grdIchiran[i, "NIOROSHI_GENBA_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["NIOROSHI_GENBA_CD"], string.Empty);
                        // 荷降現場名
                        this.form.grdIchiran[i, "NIOROSHI_GENBA_NAME"].Value = this.ChgDBNullToValue(table.Rows[i]["NIOROSHI_GENBA_NAME"], string.Empty);
                        // 伝票番号
                        this.form.grdIchiran[i, "DENPYOU_NO"].Value = this.ChgDBNullToValue(table.Rows[i]["DENPYOU_NO"], string.Empty);
                        // 品名CD
                        this.form.grdIchiran[i, "HINMEI_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["HINMEI_CD"], string.Empty);
                        // 品名略名
                        this.form.grdIchiran[i, "HINMEI_NAME"].Value = this.ChgDBNullToValue(table.Rows[i]["HINMEI_NAME"], string.Empty);
                        // 数量
                        this.form.grdIchiran[i, "SUURYOU"].Value = Convert.ToDecimal(this.ChgDBNullToValue(table.Rows[i]["SUURYOU"], 0M));
                        // 単位CD
                        this.form.grdIchiran[i, "UNIT_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["UNIT_CD"], string.Empty);
                        // 単位名
                        this.form.grdIchiran[i, "UNIT_NAME"].Value = this.ChgDBNullToValue(table.Rows[i]["UNIT_NAME_RYAKU"], string.Empty);
                        // 単価
                        this.form.grdIchiran[i, "TANKA"].Value = Convert.ToDecimal(this.ChgDBNullToValue(table.Rows[i]["TANKA"], 0M));
                        // 金額
                        if (string.IsNullOrEmpty(this.ChgDBNullToValue(table.Rows[i]["HINMEI_ZEI_KBN_CD"], string.Empty).ToString()))
                        {
                            this.form.grdIchiran[i, "KINGAKU"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(this.ChgDBNullToValue(table.Rows[i]["KINGAKU"], 0M)));
                        }
                        else
                        {
                            this.form.grdIchiran[i, "KINGAKU"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(this.ChgDBNullToValue(table.Rows[i]["HINMEI_KINGAKU"], 0M)));
                        }
                        // 品名金額
                        this.form.grdIchiran[i, "HINMEI_KINGAKU"].Value = CommonCalc.DecimalFormat(Convert.ToDecimal(this.ChgDBNullToValue(table.Rows[i]["HINMEI_KINGAKU"], 0M)));
                        // システムID
                        this.form.grdIchiran[i, "SYSTEM_ID"].Value = this.ChgDBNullToValue(table.Rows[i]["SYSTEM_ID"], string.Empty);
                        // 枝番
                        this.form.grdIchiran[i, "SEQ"].Value = this.ChgDBNullToValue(table.Rows[i]["SEQ"], string.Empty);
                        // 明細システムID
                        this.form.grdIchiran[i, "DETAIL_SYSTEM_ID"].Value = this.ChgDBNullToValue(table.Rows[i]["DETAIL_SYSTEM_ID"], string.Empty);
                        // 伝票区分
                        this.form.grdIchiran[i, "DENPYOU_KBN_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["DENPYOU_KBN_CD"], string.Empty);
                        // 品名税区分CD
                        this.form.grdIchiran[i, "HINMEI_ZEI_KBN_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["HINMEI_ZEI_KBN_CD"], string.Empty);
                        // 売上消費税率
                        this.form.grdIchiran[i, "URIAGE_SHOUHIZEI_RATE"].Value = this.ChgDBNullToValue(table.Rows[i]["URIAGE_SHOUHIZEI_RATE"], string.Empty);
                        // 支払消費税率
                        this.form.grdIchiran[i, "SHIHARAI_SHOUHIZEI_RATE"].Value = this.ChgDBNullToValue(table.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"], string.Empty);
                        // 売上税区分
                        this.form.grdIchiran[i, "URIAGE_ZEI_KBN_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["URIAGE_ZEI_KBN_CD"], string.Empty);
                        // 支払税区分
                        this.form.grdIchiran[i, "SHIHARAI_ZEI_KBN_CD"].Value = this.ChgDBNullToValue(table.Rows[i]["SHIHARAI_ZEI_KBN_CD"], string.Empty);

                        // CellのAlignスタイル設定、数字タイプは右寄せ
                        this.SetCellAlign(this.form.grdIchiran[i, "UNIT_CD"], "MiddleRight");
                        this.SetCellAlign(this.form.grdIchiran[i, "TANKA"], "MiddleRight");
                        this.SetCellAlign(this.form.grdIchiran[i, "KINGAKU"], "MiddleRight");
                        this.SetCellAlign(this.form.grdIchiran[i, "SHIN_TANKA"], "MiddleRight");
                        this.SetCellAlign(this.form.grdIchiran[i, "SHIN_KINGAKU"], "MiddleRight");
                    }
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

        #region 単価設定
        /// <summary>
        /// 単価設定
        /// </summary>
        internal void CalcTanka()
        {
            try
            {
                //LogUtility.DebugMethodStart();

                // 変更方法によって、新単価を設定する。
                switch (this.form.txtNum_HenkouHouhou.Text.Trim())
                {
                    case "1":
                        // 単価手入力
                        TankaTenyuuryoku();
                        break;
                    case "2":
                        // マスタ単価読み込み設定
                        MasterCalcTanka();
                        break;
                    case "3":
                        // 一括単価設定
                        IkkatsuCalcTanka();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region マスタ単価読み込み設定
        /// <summary>
        /// マスタ単価読み込み設定
        /// </summary>
        internal void MasterCalcTanka()
        {
            try
            {
                //LogUtility.DebugMethodStart();
                for (int i = 0; i < this.form.grdIchiran.Rows.Count; i++)
                {
                    Row row = this.form.grdIchiran.Rows[i];
                    //マスタ単価取得
                    GetMasterCalcTanka(row);

                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region 単価手入力
        /// <summary>
        /// 単価手入力
        /// </summary>
        private void TankaTenyuuryoku()
        {
            try
            {
                //LogUtility.DebugMethodStart();
                for (int i = 0; i < this.form.grdIchiran.Rows.Count; i++)
                {
                    Row row = this.form.grdIchiran.Rows[i];
                    row.Cells["SHIN_TANKA"].ReadOnly = false;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 一括単価設定
        /// <summary>
        /// 一括単価設定
        /// </summary>
        internal void IkkatsuCalcTanka()
        {
            try
            {
                //LogUtility.DebugMethodStart();
                for (int i = 0; i < this.form.grdIchiran.Rows.Count; i++)
                {
                    Row row = this.form.grdIchiran.Rows[i];
                    if (String.IsNullOrEmpty(Convert.ToString(this.form.IKATSU_TANKA.Text)))
                    {
                        return;
                    }
                    // 単価フォーマット
                    String systemTankaFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_TANKA_FORMAT, string.Empty).ToString();
                    // 単価を設定
                    this.form.grdIchiran.Rows[i].Cells["SHIN_TANKA"].Value = this.SuuryouAndTankFormat(decimal.Parse(this.form.IKATSU_TANKA.Text), systemTankaFormat);
                    this.form.grdIchiran.Rows[i].Cells["SHIN_TANKA"].ReadOnly = true;
                    //背景が灰色
                    this.form.grdIchiran.Rows[i].Cells["SHIN_TANKA"].Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region マスタ単価取得
        /// <summary>
        /// マスタ単価取得
        /// </summary>
        internal void GetMasterCalcTanka(Row targetRow)
        {
            try
            {
                //LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return;
                }
                if (String.IsNullOrEmpty(Convert.ToString(targetRow.Cells["HINMEI_CD"].Value)))
                {
                    return;
                }
                // 単価
                decimal tanka = -1;

                // 個別品名単価から取得    
                var kobetsuhinmeiTanka = this.commonAccesser.GetKobetsuhinmeiTanka(
                    Convert.ToInt16(targetRow.Cells["DENPYOU_KBN_CD"].Value),
                    ChgNullToValue(targetRow.Cells["TORIHIKISAKI_CD"].Value.ToString(), null),
                    ChgNullToValue(targetRow.Cells["GYOUSHA_CD"].Value.ToString(), null),
                    ChgNullToValue(targetRow.Cells["GENBA_CD"].Value.ToString(), null),
                    ChgNullToValue(targetRow.Cells["UNPAN_GYOUSHA_CD"].Value.ToString(), null),
                    ChgNullToValue(targetRow.Cells["NIOROSHI_GYOUSHA_CD"].Value.ToString(), null),
                    ChgNullToValue(targetRow.Cells["NIOROSHI_GENBA_CD"].Value.ToString(), null),
                    Convert.ToString(targetRow.Cells["HINMEI_CD"].Value),
                    Convert.ToInt16(targetRow.Cells["UNIT_CD"].Value)
                    );

                // 個別品名単価から情報が取れない場合は基本品名単価の検索
                if (kobetsuhinmeiTanka == null)
                {
                    var kihonHinmeiTanka = this.commonAccesser.GetKihonHinmeitanka(
                        Convert.ToInt16(targetRow.Cells["DENPYOU_KBN_CD"].Value),
                        ChgNullToValue(targetRow.Cells["UNPAN_GYOUSHA_CD"].Value.ToString(), null),
                        ChgNullToValue(targetRow.Cells["NIOROSHI_GYOUSHA_CD"].Value.ToString(), null),
                        ChgNullToValue(targetRow.Cells["NIOROSHI_GENBA_CD"].Value.ToString(), null),
                        Convert.ToString(targetRow.Cells["HINMEI_CD"].Value),
                        Convert.ToInt16(targetRow.Cells["UNIT_CD"].Value)
                        );
                    if (kihonHinmeiTanka != null)
                    {
                        decimal.TryParse(Convert.ToString(kihonHinmeiTanka.TANKA.Value), out tanka);
                    }
                }
                else
                {
                    decimal.TryParse(Convert.ToString(kobetsuhinmeiTanka.TANKA.Value), out tanka);
                }

                if (tanka < 0)
                {
                    targetRow.Cells["SHIN_TANKA"].ReadOnly = true;
                    return;
                }
                // 単価フォーマット
                String systemTankaFormat = this.ChgDBNullToValue(sysInfoEntity.SYS_TANKA_FORMAT, string.Empty).ToString();
                // 単価を設定
                targetRow.Cells["SHIN_TANKA"].Value = this.SuuryouAndTankFormat(tanka, systemTankaFormat);
                //読み取り専用
                targetRow.Cells["SHIN_TANKA"].ReadOnly = true;
                //背景が灰色
                targetRow.Cells["SHIN_TANKA"].Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 金額計算
        /// <summary>
        /// 金額計算
        /// </summary>
        internal void CalcKingaku()
        {
            try
            {
                LogUtility.DebugMethodStart();
                for (int i = 0; i < this.form.grdIchiran.Rows.Count; i++)
                {
                    Row row = this.form.grdIchiran.Rows[i];
                    //明細金額計算
                    CalcDetailKingaku(row);
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

        #region 明細金額計算
        /// <summary>
        /// 明細金額計算
        /// </summary>
        internal void CalcDetailKingaku(Row targetRow)
        {
            try
            {
                //LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return;
                }

                // 取引先が未入力或いは伝票区分が未入力である場合
                if (String.IsNullOrEmpty(targetRow.Cells["TORIHIKISAKI_CD"].Value.ToString())
                    || String.IsNullOrEmpty(targetRow.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                {
                    return;
                }

                decimal suuryou = 0;
                decimal tanka = 0;
                short kingakuHasuuCd = 0;
                // 数量
                decimal.TryParse(Convert.ToString(targetRow.Cells["SUURYOU"].Value), out suuryou);
                // float.TryParse(Convert.ToString(targetRow.Cells["SUURYOU"].FormattedValue), out suuryou);
                // 単価
                // decimal.TryParse(Convert.ToString(targetRow.Cells["SHIN_TANKA"].Value), out tanka);
                decimal.TryParse(Convert.ToString(targetRow.Cells["SHIN_TANKA"].FormattedValue), out tanka);

                // 端数取得
                kingakuHasuuCd = CalcHasuu(targetRow);

                // 金額
                targetRow.Cells["SHIN_KINGAKU"].Value = CommonCalc.DecimalFormat(CommonCalc.FractionCalc((decimal)suuryou * tanka, kingakuHasuuCd));

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 消費税計算
        /// <summary>
        /// 消費税計算
        /// </summary>
        internal void CalcShouhizei()
        {
            try
            {
                //LogUtility.DebugMethodStart();
                for (int i = 0; i < this.form.grdIchiran.Rows.Count; i++)
                {
                    Row row = this.form.grdIchiran.Rows[i];
                    //明細消費税計算
                    CalcDetailShouhizei(row);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }

        }
        #endregion

        #region 明細消費税計算
        /// <summary>
        /// 明細消費税計算
        /// </summary>
        internal void CalcDetailShouhizei(Row targetRow)
        {
            try
            {
                //LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return;
                }
                // 取引先が未入力或いは伝票区分が未入力である場合
                if (String.IsNullOrEmpty(targetRow.Cells["TORIHIKISAKI_CD"].Value.ToString())
                    || String.IsNullOrEmpty(targetRow.Cells["DENPYOU_KBN_CD"].Value.ToString()))
                {
                    return;
                }

                // 明細．金額
                decimal meisaiKingaku = 0;
                decimal.TryParse(Convert.ToString(targetRow.Cells["SHIN_KINGAKU"].FormattedValue), out meisaiKingaku);

                decimal taxRate = 0;
                int zeikeisanKbn = 0;
                int zeiKbnCd = 0;
                int kingakuHasuuCd = 0;

                // 伝票区分により、取引先情報で税計算区分CD,税区分CD,消費税端数CDを設定
                switch (Convert.ToString(targetRow.Cells["DENPYOU_KBN_CD"].Value))
                {
                    //売上
                    case "1":
                        // 税区分CD　
                        if (targetRow.Cells["URIAGE_ZEI_KBN_CD"].Value != null &&
                           !string.IsNullOrEmpty(targetRow.Cells["URIAGE_ZEI_KBN_CD"].Value.ToString()))
                            zeiKbnCd = int.Parse(Convert.ToString(targetRow.Cells["URIAGE_ZEI_KBN_CD"].Value));
                        //売上消費税率
                        if (targetRow.Cells["URIAGE_SHOUHIZEI_RATE"].Value != null &&
                            !string.IsNullOrEmpty(targetRow.Cells["URIAGE_SHOUHIZEI_RATE"].Value.ToString()))
                            taxRate = decimal.Parse(Convert.ToString(targetRow.Cells["URIAGE_SHOUHIZEI_RATE"].Value));
                        break;
                    //支払
                    case "2":
                        // 税区分CD　
                        if (targetRow.Cells["SHIHARAI_ZEI_KBN_CD"].Value != null &&
                           !string.IsNullOrEmpty(targetRow.Cells["SHIHARAI_ZEI_KBN_CD"].Value.ToString()))
                            zeiKbnCd = int.Parse(Convert.ToString(targetRow.Cells["SHIHARAI_ZEI_KBN_CD"].Value));
                        //支払消費税率
                        if (targetRow.Cells["SHIHARAI_SHOUHIZEI_RATE"].Value != null &&
                            !string.IsNullOrEmpty(targetRow.Cells["SHIHARAI_SHOUHIZEI_RATE"].Value.ToString()))
                            taxRate = decimal.Parse(Convert.ToString(targetRow.Cells["SHIHARAI_SHOUHIZEI_RATE"].Value));
                        break;
                    default:
                        break;
                }


                // 税区分CD(品名)
                String hinmeiZeiKbnCd = Convert.ToString(targetRow.Cells["HINMEI_ZEI_KBN_CD"].Value);

                // 初期化
                targetRow.Cells["TAX_SOTO"].Value = "0";
                targetRow.Cells["TAX_UCHI"].Value = "0";
                targetRow.Cells["HINMEI_TAX_SOTO"].Value = "0";
                targetRow.Cells["HINMEI_TAX_UCHI"].Value = "0";

                // 消費税
                //品名税区分空白の場合
                if (String.IsNullOrEmpty(hinmeiZeiKbnCd))
                {
                    switch (zeiKbnCd.ToString())
                    {
                        case "1":
                            // 消費税外税
                            if (targetRow.Cells["DENPYOU_KBN_CD"].Value.ToString().Equals("1") || targetRow.Cells["DENPYOU_KBN_CD"].Value.ToString().Equals("2"))
                            {
                                targetRow.Cells["TAX_SOTO"].Value =
                                    CommonCalc.FractionCalc(meisaiKingaku * taxRate, kingakuHasuuCd);
                            }
                            break;

                        case "2":
                            // 消費税内税
                            targetRow.Cells["TAX_UCHI"].Value = meisaiKingaku - (meisaiKingaku / (taxRate + 1));
                            targetRow.Cells["TAX_UCHI"].Value =
                                CommonCalc.FractionCalc((decimal)targetRow.Cells["TAX_UCHI"].Value, kingakuHasuuCd);
                            break;

                        default:
                            break;
                    }
                }
                //品名税区分非空白の場合
                else
                {
                    //税区分CD　
                    switch (hinmeiZeiKbnCd.ToString())
                    {
                        case "1":
                            // 品名別消費税外税
                            targetRow.Cells["HINMEI_TAX_SOTO"].Value =
                                CommonCalc.FractionCalc(meisaiKingaku * taxRate, kingakuHasuuCd);
                            break;

                        case "2":
                            // 品名別消費税内税
                            targetRow.Cells["HINMEI_TAX_UCHI"].Value = meisaiKingaku - (meisaiKingaku / (taxRate + 1));
                            targetRow.Cells["HINMEI_TAX_UCHI"].Value =
                                CommonCalc.FractionCalc((decimal)targetRow.Cells["HINMEI_TAX_UCHI"].Value, kingakuHasuuCd);
                            break;

                        default:
                            break;
                    }

                }

            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 端数を取得
        /// <summary>
        /// 端数を取得
        /// </summary>
        internal short CalcHasuu(Row targetRow)
        {
            short returnVal = 0;

            try
            {
                LogUtility.DebugMethodStart(targetRow);

                if (targetRow == null)
                {
                    return returnVal;
                }

                // 取引先が未入力或いは伝票区分が未入力である場合
                if (String.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text)
                    || targetRow.Cells["DENPYOU_KBN_CD"].Value == null)
                {
                    return returnVal;
                }

                // 伝票区分により、端数を設定
                switch (Convert.ToString(targetRow.Cells["DENPYOU_KBN_CD"].Value))
                {
                    case "1":
                        // 取引先請求
                        var torihikisakiSeikyuu = this.GetTorihikisakiSeikyuu(this.form.TORIHIKISAKI_CD.Text);
                        if (torihikisakiSeikyuu != null)
                        {
                            short.TryParse(Convert.ToString(torihikisakiSeikyuu.TAX_HASUU_CD), out returnVal);
                        }
                        break;
                    case "2":
                        // 取引先支払
                        var torihikisakiShiharai = this.GetTorihikisakiShiharai(this.form.TORIHIKISAKI_CD.Text);
                        if (torihikisakiShiharai != null)
                        {
                            short.TryParse(Convert.ToString(torihikisakiShiharai.TAX_HASUU_CD), out returnVal);
                        }
                        break;
                    default:
                        break;
                }


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

        #region 取引先_請求情報を取得
        /// <summary>
        /// 取引先_請求情報を取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns></returns>
        public M_TORIHIKISAKI_SEIKYUU GetTorihikisakiSeikyuu(string torihikisakiCd)
        {
            M_TORIHIKISAKI_SEIKYUU returnVal = null;

            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);

                if (string.IsNullOrEmpty(torihikisakiCd))
                {
                    return returnVal;
                }

                IM_TORIHIKISAKI_SEIKYUUDao dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SEIKYUUDao>();
                returnVal = dao.GetDataByCd(torihikisakiCd);

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

        #region 取引先_支払情報を取得
        /// <summary>
        /// 取引先_支払情報を取得
        /// </summary>
        /// <param name="torihikisakiCd">取引先CD</param>
        /// <returns></returns>
        public M_TORIHIKISAKI_SHIHARAI GetTorihikisakiShiharai(string torihikisakiCd)
        {
            M_TORIHIKISAKI_SHIHARAI returnVal = null;
            try
            {
                LogUtility.DebugMethodStart(torihikisakiCd);
                if (string.IsNullOrEmpty(torihikisakiCd))
                {
                    return returnVal;
                }

                IM_TORIHIKISAKI_SHIHARAIDao dao = DaoInitUtility.GetComponent<IM_TORIHIKISAKI_SHIHARAIDao>();
                returnVal = dao.GetDataByCd(torihikisakiCd);

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

        #region Entityを作成する

        /// <summary>
        /// 各Entitysデータを作成
        /// </summary>
        public void CreateEntity()
        {
            try
            {
                LogUtility.DebugMethodStart();

                //伝票番号
                string denpyouNO = "";
                //伝票種類
                string denpyouShurui = "";
                //一応保存テブル
                var saveDt = new DataTable();

                //受付入力DBに更新Entityリストを初期化する。
                delUkeireEntryEntityList.Clear();
                insUkeireEntryEntityList.Clear();
                insUkeireEntryDetailList.Clear();
                //出荷入力DBに更新Entityリストを初期化する。
                delShukkaEntryEntityList.Clear();
                insShukkaEntryEntityList.Clear();
                insShukkaEntryDetailList.Clear();
                //売上/支払入力DBに更新Entityリストを初期化する。
                delUrshEntryEntityList.Clear();
                insUrshEntryEntityList.Clear();
                insUrshEntryDetailList.Clear();

                //保存テブルコラムを構成する。
                for (int col = 0; col < this.form.grdIchiran.Columns.Count; col++)
                {
                    // 表示対象の列だけを順番に追加
                    saveDt.Columns.Add(this.form.grdIchiran.Columns[col].Name, Type.GetType("System.String"));
                }

                for (int i = 0; i < this.form.grdIchiran.Rows.Count; i++)
                {
                    Row row = this.form.grdIchiran.Rows[i];
                    //一行目
                    if (i == 0)
                    {
                        //saveDtテブルに追加
                        AddMultRowToDataTable(saveDt, row);
                        denpyouNO = row.Cells["DENPYOU_NO"].Value.ToString();
                        denpyouShurui = row.Cells["DENPYOU_SHURUI"].Value.ToString();
                    }
                    else
                    {
                        //伝票番号と伝票種類が変わってない場合。
                        if (denpyouNO == row.Cells["DENPYOU_NO"].Value.ToString() &&
                            denpyouShurui == row.Cells["DENPYOU_SHURUI"].Value.ToString())
                        {
                            //saveDtテブルに追加
                            AddMultRowToDataTable(saveDt, row);
                        }
                        //伝票番号と伝票種類が変わた場合、一応計算とEntity作成
                        else
                        {
                            //（受付、出荷、売上/支払）論理削除Entityを作成
                            this.CreateDelEntryEntity(saveDt);

                            //（受付、出荷、売上/支払）Entityをリストに追加
                            this.CreateEntryEntity(saveDt);

                            //（受付、出荷、売上/支払）明細Entityを作成
                            this.CreateDetailEntity(saveDt);

                            //伝票番号と伝票種類が変わた場合、改めて計算する。
                            saveDt.Rows.Clear();
                            //saveDtテブルに追加
                            AddMultRowToDataTable(saveDt, row);
                            denpyouNO = row.Cells["DENPYOU_NO"].Value.ToString();
                            denpyouShurui = row.Cells["DENPYOU_SHURUI"].Value.ToString();
                        }
                    }
                    //最後行目
                    if (i == this.form.grdIchiran.Rows.Count - 1)
                    {
                        //（受付、出荷、売上/支払）論理削除Entityを作成
                        this.CreateDelEntryEntity(saveDt);

                        //（受付、出荷、売上/支払）Entityをリストに追加
                        this.CreateEntryEntity(saveDt);

                        //（受付、出荷、売上/支払）明細Entityを作成
                        this.CreateDetailEntity(saveDt);
                    }
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

        /// <summary>
        ///（受入、出荷、売上/支払）明細Entityを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateDetailEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart();
                //空白行の場合
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }
                else
                {
                    //受入
                    DataTable dtUkeire = dt.Clone();
                    //出荷
                    DataTable dtShuka = dt.Clone();
                    //売上/支払
                    DataTable dtUrshs = dt.Clone();
                    //伝票種類によって、データを分ける(受入、出荷、売上/支払)。
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["DENPYOU_SHURUI"].ToString().Equals("受入"))
                        {
                            dtUkeire.Rows.Add(dt.Rows[i].ItemArray);
                        }
                        else if (dt.Rows[i]["DENPYOU_SHURUI"].ToString().Equals("出荷"))
                        {
                            dtShuka.Rows.Add(dt.Rows[i].ItemArray);
                        }
                        else if (dt.Rows[i]["DENPYOU_SHURUI"].ToString().Equals("売上/支払"))
                        {
                            dtUrshs.Rows.Add(dt.Rows[i].ItemArray);
                        }
                    }

                    //受入明細Entityを作成
                    if (dtUkeire.Rows.Count > 0)
                        CreateUkeireDetailEntity(dtUkeire);
                    //出荷明細Entityを作成
                    if (dtShuka.Rows.Count > 0)
                        CreateShukkaDetailEntity(dtShuka);
                    //売上/支払明細Entityを作成
                    if (dtUrshs.Rows.Count > 0)
                        CreateUrshDetailEntity(dtUrshs);
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

        /// <summary>
        ///受入明細Entityを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateUkeireDetailEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart();
                this.dto.SYSTEM_ID = dt.Rows[0]["SYSTEM_ID"].ToString();
                this.dto.SEQ = dt.Rows[0]["SEQ"].ToString();
                //受入明細テブル
                DataTable retUkeireTankaSabunDetailDT = this.ukeireTankaSabunDetailDao.GetDataToDataTable(this.dto);
                //画面に表示されるデータ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DTOClass dtoDetail = new DTOClass();
                    dtoDetail.SystemID = long.Parse(dt.Rows[i]["SYSTEM_ID"].ToString());
                    dtoDetail.SEQ = int.Parse(dt.Rows[i]["SEQ"].ToString());
                    dtoDetail.DetailSystemID = long.Parse(dt.Rows[i]["DETAIL_SYSTEM_ID"].ToString());
                    //受入明細テブル
                    DataTable retUkeireEntityDT = this.daoUkeireDetail.GetDataToDataTable(dtoDetail);
                    //受入をインサートDetail
                    this.insUkeireEntryDetail = new T_UKEIRE_DETAIL();
                    //システムID
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SYSTEM_ID"].ToString()))
                        insUkeireEntryDetail.SYSTEM_ID = SqlInt64.Parse(retUkeireEntityDT.Rows[0]["SYSTEM_ID"].ToString());
                    //枝番
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SEQ"].ToString()))
                        insUkeireEntryDetail.SEQ = SqlInt32.Parse(retUkeireEntityDT.Rows[0]["SEQ"].ToString()) + 1;
                    //明細システムID
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString()))
                        insUkeireEntryDetail.DETAIL_SYSTEM_ID = SqlInt32.Parse(retUkeireEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString());
                    //受入番号
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["UKEIRE_NUMBER"].ToString()))
                        insUkeireEntryDetail.UKEIRE_NUMBER = SqlInt32.Parse(retUkeireEntityDT.Rows[0]["UKEIRE_NUMBER"].ToString());
                    //行番号
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["ROW_NO"].ToString()))
                        insUkeireEntryDetail.ROW_NO = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["ROW_NO"].ToString());
                    //確定区分
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["KAKUTEI_KBN"].ToString()))
                        insUkeireEntryDetail.KAKUTEI_KBN = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["KAKUTEI_KBN"].ToString());
                    //売上支払日付
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString()))
                        insUkeireEntryDetail.URIAGESHIHARAI_DATE = SqlDateTime.Parse(retUkeireEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString());
                    //総重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["STACK_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.STACK_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["STACK_JYUURYOU"].ToString());
                    //空車重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["EMPTY_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.EMPTY_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["EMPTY_JYUURYOU"].ToString());
                    //割振重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["WARIFURI_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.WARIFURI_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["WARIFURI_JYUURYOU"].ToString());
                    //割振割合
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["WARIFURI_PERCENT"].ToString()))
                        insUkeireEntryDetail.WARIFURI_PERCENT = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["WARIFURI_PERCENT"].ToString());
                    //調整重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["CHOUSEI_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.CHOUSEI_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["CHOUSEI_JYUURYOU"].ToString());
                    //調整割合
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["CHOUSEI_PERCENT"].ToString()))
                        insUkeireEntryDetail.CHOUSEI_PERCENT = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["CHOUSEI_PERCENT"].ToString());
                    //容器CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["YOUKI_CD"].ToString()))
                        insUkeireEntryDetail.YOUKI_CD = retUkeireEntityDT.Rows[0]["YOUKI_CD"].ToString();
                    //容器数量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["YOUKI_SUURYOU"].ToString()))
                        insUkeireEntryDetail.YOUKI_SUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["YOUKI_SUURYOU"].ToString());
                    //容器重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["YOUKI_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.YOUKI_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["YOUKI_JYUURYOU"].ToString());
                    //伝票区分CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                        insUkeireEntryDetail.DENPYOU_KBN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString());
                    //品名CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["HINMEI_CD"].ToString()))
                        insUkeireEntryDetail.HINMEI_CD = retUkeireEntityDT.Rows[0]["HINMEI_CD"].ToString();
                    //品名
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["HINMEI_NAME"].ToString()))
                        insUkeireEntryDetail.HINMEI_NAME = retUkeireEntityDT.Rows[0]["HINMEI_NAME"].ToString();
                    //正味重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NET_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.NET_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["NET_JYUURYOU"].ToString());
                    //数量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SUURYOU"].ToString()))
                        insUkeireEntryDetail.SUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["SUURYOU"].ToString());
                    //単位CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["UNIT_CD"].ToString()))
                        insUkeireEntryDetail.UNIT_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["UNIT_CD"].ToString());
                    //単価
                    if (!string.IsNullOrEmpty(dt.Rows[i]["SHIN_TANKA"].ToString()))
                        insUkeireEntryDetail.TANKA = Convert.ToDecimal(dt.Rows[i]["SHIN_TANKA"].ToString());
                    //品名税区分空白の場合
                    if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null || string.IsNullOrEmpty(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        //金額
                        if (!string.IsNullOrEmpty(dt.Rows[i]["SHIN_KINGAKU"].ToString()))
                            insUkeireEntryDetail.KINGAKU = Convert.ToDecimal(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                        //品名別金額
                        insUkeireEntryDetail.HINMEI_KINGAKU = 0;
                    }
                    //品名税区分非空白の場合
                    else
                    {
                        //金額
                        insUkeireEntryDetail.KINGAKU = 0;
                        //品名別金額
                        if (!string.IsNullOrEmpty(dt.Rows[i]["SHIN_KINGAKU"].ToString()))
                            insUkeireEntryDetail.HINMEI_KINGAKU = Convert.ToDecimal(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                    }
                    //消費税外税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["TAX_SOTO"].ToString()))
                        insUkeireEntryDetail.TAX_SOTO = SqlDecimal.Parse(dt.Rows[i]["TAX_SOTO"].ToString());
                    //消費税内税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["TAX_UCHI"].ToString()))
                        insUkeireEntryDetail.TAX_UCHI = SqlDecimal.Parse(dt.Rows[i]["TAX_UCHI"].ToString());
                    //品名別税区分CD
                    if (!string.IsNullOrEmpty(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        insUkeireEntryDetail.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString());
                    //品名別消費税外税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString()))
                        insUkeireEntryDetail.HINMEI_TAX_SOTO = SqlDecimal.Parse(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString());
                    //品名別消費税内税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString()))
                        insUkeireEntryDetail.HINMEI_TAX_UCHI = SqlDecimal.Parse(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString());
                    //明細備考
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["MEISAI_BIKOU"].ToString()))
                        insUkeireEntryDetail.MEISAI_BIKOU = retUkeireEntityDT.Rows[0]["MEISAI_BIKOU"].ToString();
                    //荷姿数量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString()))
                        insUkeireEntryDetail.NISUGATA_SUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString());
                    //荷姿単位CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString()))
                        insUkeireEntryDetail.NISUGATA_UNIT_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString());
                    //// 作成と更新情報設定
                    //var dbLogic = new DataBinderLogic<r_framework.Entity.T_UKEIRE_DETAIL>(insUkeireEntryDetail);
                    //dbLogic.SetSystemProperty(insUkeireEntryDetail, false);
                    //インサート受付入力明細Entityリストに追加
                    this.insUkeireEntryDetailList.Add(insUkeireEntryDetail);

                }

                //画面に表示されない、DBに存在しているデータ
                for (int i = 0; i < retUkeireTankaSabunDetailDT.Rows.Count; i++)
                {
                    DTOClass dtoDetail = new DTOClass();
                    dtoDetail.SystemID = long.Parse(retUkeireTankaSabunDetailDT.Rows[i]["SYSTEM_ID"].ToString());
                    dtoDetail.SEQ = int.Parse(retUkeireTankaSabunDetailDT.Rows[i]["SEQ"].ToString());
                    dtoDetail.DetailSystemID = long.Parse(retUkeireTankaSabunDetailDT.Rows[i]["DETAIL_SYSTEM_ID"].ToString());
                    //受入明細テブル
                    DataTable retUkeireEntityDT = this.daoUkeireDetail.GetDataToDataTable(dtoDetail);
                    //受入をインサートDetail
                    this.insUkeireEntryDetail = new T_UKEIRE_DETAIL();
                    //システムID
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SYSTEM_ID"].ToString()))
                        insUkeireEntryDetail.SYSTEM_ID = SqlInt64.Parse(retUkeireEntityDT.Rows[0]["SYSTEM_ID"].ToString());
                    //枝番
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SEQ"].ToString()))
                        insUkeireEntryDetail.SEQ = SqlInt32.Parse(retUkeireEntityDT.Rows[0]["SEQ"].ToString()) + 1;
                    //明細システムID
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString()))
                        insUkeireEntryDetail.DETAIL_SYSTEM_ID = SqlInt32.Parse(retUkeireEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString());
                    //受入番号
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["UKEIRE_NUMBER"].ToString()))
                        insUkeireEntryDetail.UKEIRE_NUMBER = SqlInt32.Parse(retUkeireEntityDT.Rows[0]["UKEIRE_NUMBER"].ToString());
                    //行番号
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["ROW_NO"].ToString()))
                        insUkeireEntryDetail.ROW_NO = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["ROW_NO"].ToString());
                    //確定区分
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["KAKUTEI_KBN"].ToString()))
                        insUkeireEntryDetail.KAKUTEI_KBN = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["KAKUTEI_KBN"].ToString());
                    //売上支払日付
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString()))
                        insUkeireEntryDetail.URIAGESHIHARAI_DATE = SqlDateTime.Parse(retUkeireEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString());
                    //総重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["STACK_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.STACK_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["STACK_JYUURYOU"].ToString());
                    //空車重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["EMPTY_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.EMPTY_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["EMPTY_JYUURYOU"].ToString());
                    //割振重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["WARIFURI_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.WARIFURI_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["WARIFURI_JYUURYOU"].ToString());
                    //割振割合
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["WARIFURI_PERCENT"].ToString()))
                        insUkeireEntryDetail.WARIFURI_PERCENT = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["WARIFURI_PERCENT"].ToString());
                    //調整重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["CHOUSEI_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.CHOUSEI_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["CHOUSEI_JYUURYOU"].ToString());
                    //調整割合
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["CHOUSEI_PERCENT"].ToString()))
                        insUkeireEntryDetail.CHOUSEI_PERCENT = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["CHOUSEI_PERCENT"].ToString());
                    //容器CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["YOUKI_CD"].ToString()))
                        insUkeireEntryDetail.YOUKI_CD = retUkeireEntityDT.Rows[0]["YOUKI_CD"].ToString();
                    //容器数量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["YOUKI_SUURYOU"].ToString()))
                        insUkeireEntryDetail.YOUKI_SUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["YOUKI_SUURYOU"].ToString());
                    //容器重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["YOUKI_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.YOUKI_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["YOUKI_JYUURYOU"].ToString());
                    //伝票区分CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                        insUkeireEntryDetail.DENPYOU_KBN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString());
                    //品名CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["HINMEI_CD"].ToString()))
                        insUkeireEntryDetail.HINMEI_CD = retUkeireEntityDT.Rows[0]["HINMEI_CD"].ToString();
                    //品名
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["HINMEI_NAME"].ToString()))
                        insUkeireEntryDetail.HINMEI_NAME = retUkeireEntityDT.Rows[0]["HINMEI_NAME"].ToString();
                    //正味重量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NET_JYUURYOU"].ToString()))
                        insUkeireEntryDetail.NET_JYUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["NET_JYUURYOU"].ToString());
                    //数量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SUURYOU"].ToString()))
                        insUkeireEntryDetail.SUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["SUURYOU"].ToString());
                    //単位CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["UNIT_CD"].ToString()))
                        insUkeireEntryDetail.UNIT_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["UNIT_CD"].ToString());
                    //単価
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["TANKA"].ToString()))
                        insUkeireEntryDetail.TANKA = Convert.ToDecimal(retUkeireEntityDT.Rows[0]["TANKA"].ToString());
                    //金額
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["KINGAKU"].ToString()))
                        insUkeireEntryDetail.KINGAKU = Convert.ToDecimal(retUkeireEntityDT.Rows[0]["KINGAKU"].ToString());
                    //品名別金額
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["HINMEI_KINGAKU"].ToString()))
                        insUkeireEntryDetail.HINMEI_KINGAKU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["HINMEI_KINGAKU"].ToString());
                    //消費税外税
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["TAX_SOTO"].ToString()))
                        insUkeireEntryDetail.TAX_SOTO = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["TAX_SOTO"].ToString());
                    //消費税内税
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["TAX_UCHI"].ToString()))
                        insUkeireEntryDetail.TAX_UCHI = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["TAX_UCHI"].ToString());
                    //品名別税区分CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["HINMEI_ZEI_KBN_CD"].ToString()))
                        insUkeireEntryDetail.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["HINMEI_ZEI_KBN_CD"].ToString());
                    //品名別消費税外税
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["HINMEI_TAX_SOTO"].ToString()))
                        insUkeireEntryDetail.HINMEI_TAX_SOTO = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["HINMEI_TAX_SOTO"].ToString());
                    //品名別消費税内税
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["HINMEI_TAX_UCHI"].ToString()))
                        insUkeireEntryDetail.HINMEI_TAX_UCHI = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["HINMEI_TAX_UCHI"].ToString());
                    //明細備考
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["MEISAI_BIKOU"].ToString()))
                        insUkeireEntryDetail.MEISAI_BIKOU = retUkeireEntityDT.Rows[0]["MEISAI_BIKOU"].ToString();
                    //荷姿数量
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString()))
                        insUkeireEntryDetail.NISUGATA_SUURYOU = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString());
                    //荷姿単位CD
                    if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString()))
                        insUkeireEntryDetail.NISUGATA_UNIT_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString());
                    //// 作成と更新情報設定
                    //var dbLogic = new DataBinderLogic<r_framework.Entity.T_UKEIRE_DETAIL>(insUkeireEntryDetail);
                    //dbLogic.SetSystemProperty(insUkeireEntryDetail, false);
                    //インサート受付入力明細Entityリストに追加
                    this.insUkeireEntryDetailList.Add(insUkeireEntryDetail);
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

        /// <summary>
        ///出荷明細Entityを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateShukkaDetailEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                this.dto.SYSTEM_ID = dt.Rows[0]["SYSTEM_ID"].ToString();
                this.dto.SEQ = dt.Rows[0]["SEQ"].ToString();
                //出荷明細テブル
                DataTable retShukkaTankaSabunDetailDT = this.shukkaTankaSabunDetailDao.GetDataToDataTable(this.dto);
                //画面に表示されるデータ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DTOClass dtoDetail = new DTOClass();
                    dtoDetail.SystemID = long.Parse(dt.Rows[i]["SYSTEM_ID"].ToString());
                    dtoDetail.SEQ = int.Parse(dt.Rows[i]["SEQ"].ToString());
                    dtoDetail.DetailSystemID = long.Parse(dt.Rows[i]["DETAIL_SYSTEM_ID"].ToString());
                    //出荷明細テブル
                    DataTable retShukkaEntityDT = this.daoShukkaDetail.GetDataToDataTable(dtoDetail);
                    //出荷をインサートDetail
                    this.insShukkaEntryDetail = new T_SHUKKA_DETAIL();
                    //システムID
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SYSTEM_ID"].ToString()))
                        insShukkaEntryDetail.SYSTEM_ID = SqlInt64.Parse(retShukkaEntityDT.Rows[0]["SYSTEM_ID"].ToString());
                    //枝番
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SEQ"].ToString()))
                        insShukkaEntryDetail.SEQ = SqlInt32.Parse(retShukkaEntityDT.Rows[0]["SEQ"].ToString()) + 1;
                    //明細システムID
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString()))
                        insShukkaEntryDetail.DETAIL_SYSTEM_ID = SqlInt32.Parse(retShukkaEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString());
                    //出荷番号
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHUKKA_NUMBER"].ToString()))
                        insShukkaEntryDetail.SHUKKA_NUMBER = SqlInt32.Parse(retShukkaEntityDT.Rows[0]["SHUKKA_NUMBER"].ToString());
                    //行番号
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["ROW_NO"].ToString()))
                        insShukkaEntryDetail.ROW_NO = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["ROW_NO"].ToString());
                    //確定区分
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KAKUTEI_KBN"].ToString()))
                        insShukkaEntryDetail.KAKUTEI_KBN = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["KAKUTEI_KBN"].ToString());
                    //売上支払日付
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString()))
                        insShukkaEntryDetail.URIAGESHIHARAI_DATE = SqlDateTime.Parse(retShukkaEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString());
                    //総重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["STACK_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.STACK_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["STACK_JYUURYOU"].ToString());
                    //空車重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["EMPTY_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.EMPTY_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["EMPTY_JYUURYOU"].ToString());
                    //割振重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["WARIFURI_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.WARIFURI_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["WARIFURI_JYUURYOU"].ToString());
                    //割振割合
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["WARIFURI_PERCENT"].ToString()))
                        insShukkaEntryDetail.WARIFURI_PERCENT = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["WARIFURI_PERCENT"].ToString());
                    //調整重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["CHOUSEI_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.CHOUSEI_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["CHOUSEI_JYUURYOU"].ToString());
                    //調整割合
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["CHOUSEI_PERCENT"].ToString()))
                        insShukkaEntryDetail.CHOUSEI_PERCENT = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["CHOUSEI_PERCENT"].ToString());
                    //容器CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["YOUKI_CD"].ToString()))
                        insShukkaEntryDetail.YOUKI_CD = retShukkaEntityDT.Rows[0]["YOUKI_CD"].ToString();
                    //容器数量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["YOUKI_SUURYOU"].ToString()))
                        insShukkaEntryDetail.YOUKI_SUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["YOUKI_SUURYOU"].ToString());
                    //容器重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["YOUKI_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.YOUKI_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["YOUKI_JYUURYOU"].ToString());
                    //伝票区分CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                        insShukkaEntryDetail.DENPYOU_KBN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString());
                    //品名CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["HINMEI_CD"].ToString()))
                        insShukkaEntryDetail.HINMEI_CD = retShukkaEntityDT.Rows[0]["HINMEI_CD"].ToString();
                    //品名
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["HINMEI_NAME"].ToString()))
                        insShukkaEntryDetail.HINMEI_NAME = retShukkaEntityDT.Rows[0]["HINMEI_NAME"].ToString();
                    //正味重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NET_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.NET_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["NET_JYUURYOU"].ToString());
                    //数量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SUURYOU"].ToString()))
                        insShukkaEntryDetail.SUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["SUURYOU"].ToString());
                    //単位CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["UNIT_CD"].ToString()))
                        insShukkaEntryDetail.UNIT_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["UNIT_CD"].ToString());
                    //単価
                    if (!string.IsNullOrEmpty(dt.Rows[i]["SHIN_TANKA"].ToString()))
                        insShukkaEntryDetail.TANKA = Convert.ToDecimal(dt.Rows[i]["SHIN_TANKA"].ToString());
                    //品名税区分空白の場合
                    if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null || string.IsNullOrEmpty(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        //金額
                        if (!string.IsNullOrEmpty(dt.Rows[i]["SHIN_KINGAKU"].ToString()))
                            insShukkaEntryDetail.KINGAKU = Convert.ToDecimal(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                        //品名別金額
                        insShukkaEntryDetail.HINMEI_KINGAKU = 0;
                    }
                    //品名税区分非空白の場合
                    else
                    {
                        //金額
                        insShukkaEntryDetail.KINGAKU = 0;
                        //品名別金額
                        if (!string.IsNullOrEmpty(dt.Rows[i]["SHIN_KINGAKU"].ToString()))
                            insShukkaEntryDetail.HINMEI_KINGAKU = Convert.ToDecimal(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                    }
                    //消費税外税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["TAX_SOTO"].ToString()))
                        insShukkaEntryDetail.TAX_SOTO = SqlDecimal.Parse(dt.Rows[i]["TAX_SOTO"].ToString());
                    //消費税内税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["TAX_UCHI"].ToString()))
                        insShukkaEntryDetail.TAX_UCHI = SqlDecimal.Parse(dt.Rows[i]["TAX_UCHI"].ToString());
                    //品名別税区分CD
                    if (!string.IsNullOrEmpty(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        insShukkaEntryDetail.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString());
                    //品名別消費税外税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString()))
                        insShukkaEntryDetail.HINMEI_TAX_SOTO = SqlDecimal.Parse(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString());
                    //品名別消費税内税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString()))
                        insShukkaEntryDetail.HINMEI_TAX_UCHI = SqlDecimal.Parse(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString());
                    //明細備考
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["MEISAI_BIKOU"].ToString()))
                        insShukkaEntryDetail.MEISAI_BIKOU = retShukkaEntityDT.Rows[0]["MEISAI_BIKOU"].ToString();
                    //荷姿数量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString()))
                        insShukkaEntryDetail.NISUGATA_SUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString());
                    //荷姿単位CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString()))
                        insShukkaEntryDetail.NISUGATA_UNIT_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString());
                    //// 作成と更新情報設定
                    //var dbLogic = new DataBinderLogic<r_framework.Entity.T_Shukka_DETAIL>(insShukkaEntryDetail);
                    //dbLogic.SetSystemProperty(insShukkaEntryDetail, false);
                    //インサート出荷入力明細Entityリストに追加
                    this.insShukkaEntryDetailList.Add(insShukkaEntryDetail);

                }

                //画面に表示されない、DBに存在しているデータ
                for (int i = 0; i < retShukkaTankaSabunDetailDT.Rows.Count; i++)
                {
                    DTOClass dtoDetail = new DTOClass();
                    dtoDetail.SystemID = long.Parse(retShukkaTankaSabunDetailDT.Rows[i]["SYSTEM_ID"].ToString());
                    dtoDetail.SEQ = int.Parse(retShukkaTankaSabunDetailDT.Rows[i]["SEQ"].ToString());
                    dtoDetail.DetailSystemID = long.Parse(retShukkaTankaSabunDetailDT.Rows[i]["DETAIL_SYSTEM_ID"].ToString());
                    //出荷明細テブル
                    DataTable retShukkaEntityDT = this.daoShukkaDetail.GetDataToDataTable(dtoDetail);
                    //出荷をインサートDetail
                    this.insShukkaEntryDetail = new T_SHUKKA_DETAIL();
                    //システムID
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SYSTEM_ID"].ToString()))
                        insShukkaEntryDetail.SYSTEM_ID = SqlInt64.Parse(retShukkaEntityDT.Rows[0]["SYSTEM_ID"].ToString());
                    //枝番
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SEQ"].ToString()))
                        insShukkaEntryDetail.SEQ = SqlInt32.Parse(retShukkaEntityDT.Rows[0]["SEQ"].ToString()) + 1;
                    //明細システムID
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString()))
                        insShukkaEntryDetail.DETAIL_SYSTEM_ID = SqlInt32.Parse(retShukkaEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString());
                    //出荷番号
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHUKKA_NUMBER"].ToString()))
                        insShukkaEntryDetail.SHUKKA_NUMBER = SqlInt32.Parse(retShukkaEntityDT.Rows[0]["SHUKKA_NUMBER"].ToString());
                    //行番号
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["ROW_NO"].ToString()))
                        insShukkaEntryDetail.ROW_NO = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["ROW_NO"].ToString());
                    //確定区分
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KAKUTEI_KBN"].ToString()))
                        insShukkaEntryDetail.KAKUTEI_KBN = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["KAKUTEI_KBN"].ToString());
                    //売上支払日付
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString()))
                        insShukkaEntryDetail.URIAGESHIHARAI_DATE = SqlDateTime.Parse(retShukkaEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString());
                    //総重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["STACK_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.STACK_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["STACK_JYUURYOU"].ToString());
                    //空車重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["EMPTY_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.EMPTY_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["EMPTY_JYUURYOU"].ToString());
                    //割振重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["WARIFURI_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.WARIFURI_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["WARIFURI_JYUURYOU"].ToString());
                    //割振割合
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["WARIFURI_PERCENT"].ToString()))
                        insShukkaEntryDetail.WARIFURI_PERCENT = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["WARIFURI_PERCENT"].ToString());
                    //調整重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["CHOUSEI_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.CHOUSEI_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["CHOUSEI_JYUURYOU"].ToString());
                    //調整割合
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["CHOUSEI_PERCENT"].ToString()))
                        insShukkaEntryDetail.CHOUSEI_PERCENT = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["CHOUSEI_PERCENT"].ToString());
                    //容器CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["YOUKI_CD"].ToString()))
                        insShukkaEntryDetail.YOUKI_CD = retShukkaEntityDT.Rows[0]["YOUKI_CD"].ToString();
                    //容器数量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["YOUKI_SUURYOU"].ToString()))
                        insShukkaEntryDetail.YOUKI_SUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["YOUKI_SUURYOU"].ToString());
                    //容器重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["YOUKI_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.YOUKI_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["YOUKI_JYUURYOU"].ToString());
                    //伝票区分CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                        insShukkaEntryDetail.DENPYOU_KBN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString());
                    //品名CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["HINMEI_CD"].ToString()))
                        insShukkaEntryDetail.HINMEI_CD = retShukkaEntityDT.Rows[0]["HINMEI_CD"].ToString();
                    //品名
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["HINMEI_NAME"].ToString()))
                        insShukkaEntryDetail.HINMEI_NAME = retShukkaEntityDT.Rows[0]["HINMEI_NAME"].ToString();
                    //正味重量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NET_JYUURYOU"].ToString()))
                        insShukkaEntryDetail.NET_JYUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["NET_JYUURYOU"].ToString());
                    //数量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SUURYOU"].ToString()))
                        insShukkaEntryDetail.SUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["SUURYOU"].ToString());
                    //単位CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["UNIT_CD"].ToString()))
                        insShukkaEntryDetail.UNIT_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["UNIT_CD"].ToString());
                    //単価
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["TANKA"].ToString()))
                        insShukkaEntryDetail.TANKA = Convert.ToDecimal(retShukkaEntityDT.Rows[0]["TANKA"].ToString());
                    //金額
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KINGAKU"].ToString()))
                        insShukkaEntryDetail.KINGAKU = Convert.ToDecimal(retShukkaEntityDT.Rows[0]["KINGAKU"].ToString());
                    //品名別金額
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["HINMEI_KINGAKU"].ToString()))
                        insShukkaEntryDetail.HINMEI_KINGAKU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["HINMEI_KINGAKU"].ToString());
                    //消費税外税
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["TAX_SOTO"].ToString()))
                        insShukkaEntryDetail.TAX_SOTO = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["TAX_SOTO"].ToString());
                    //消費税内税
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["TAX_UCHI"].ToString()))
                        insShukkaEntryDetail.TAX_UCHI = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["TAX_UCHI"].ToString());
                    //品名別税区分CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["HINMEI_ZEI_KBN_CD"].ToString()))
                        insShukkaEntryDetail.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["HINMEI_ZEI_KBN_CD"].ToString());
                    //品名別消費税外税
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["HINMEI_TAX_SOTO"].ToString()))
                        insShukkaEntryDetail.HINMEI_TAX_SOTO = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["HINMEI_TAX_SOTO"].ToString());
                    //品名別消費税内税
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["HINMEI_TAX_UCHI"].ToString()))
                        insShukkaEntryDetail.HINMEI_TAX_UCHI = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["HINMEI_TAX_UCHI"].ToString());
                    //明細備考
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["MEISAI_BIKOU"].ToString()))
                        insShukkaEntryDetail.MEISAI_BIKOU = retShukkaEntityDT.Rows[0]["MEISAI_BIKOU"].ToString();
                    //荷姿数量
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString()))
                        insShukkaEntryDetail.NISUGATA_SUURYOU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString());
                    //荷姿単位CD
                    if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString()))
                        insShukkaEntryDetail.NISUGATA_UNIT_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString());
                    //// 作成と更新情報設定
                    //var dbLogic = new DataBinderLogic<r_framework.Entity.T_Shukka_DETAIL>(insShukkaEntryDetail);
                    //dbLogic.SetSystemProperty(insShukkaEntryDetail, false);
                    //インサート受付入力明細Entityリストに追加
                    this.insShukkaEntryDetailList.Add(insShukkaEntryDetail);
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

        /// <summary>
        ///売上/支払明細Entityを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateUrshDetailEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                this.dto.SYSTEM_ID = dt.Rows[0]["SYSTEM_ID"].ToString();
                this.dto.SEQ = dt.Rows[0]["SEQ"].ToString();
                //売上/支払明細テブル
                DataTable retUrshTankaSabunDetailDT = this.urshTankaSabunDetailDao.GetDataToDataTable(this.dto);
                //画面に表示されるデータ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DTOClass dtoDetail = new DTOClass();
                    dtoDetail.SystemID = long.Parse(dt.Rows[i]["SYSTEM_ID"].ToString());
                    dtoDetail.SEQ = int.Parse(dt.Rows[i]["SEQ"].ToString());
                    dtoDetail.DetailSystemID = long.Parse(dt.Rows[i]["DETAIL_SYSTEM_ID"].ToString());
                    //売上/支払明細テブル
                    DataTable retUrshEntityDT = this.daoUrshDetail.GetDataToDataTable(dtoDetail);
                    //売上/支払をインサートDetail
                    this.insUrshEntryDetail = new T_UR_SH_DETAIL();
                    //システムID
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SYSTEM_ID"].ToString()))
                        insUrshEntryDetail.SYSTEM_ID = SqlInt64.Parse(retUrshEntityDT.Rows[0]["SYSTEM_ID"].ToString());
                    //枝番
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SEQ"].ToString()))
                        insUrshEntryDetail.SEQ = SqlInt32.Parse(retUrshEntityDT.Rows[0]["SEQ"].ToString()) + 1;
                    //明細システムID
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString()))
                        insUrshEntryDetail.DETAIL_SYSTEM_ID = SqlInt32.Parse(retUrshEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString());
                    //売上/支払番号
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["UR_SH_NUMBER"].ToString()))
                        insUrshEntryDetail.UR_SH_NUMBER = SqlInt32.Parse(retUrshEntityDT.Rows[0]["UR_SH_NUMBER"].ToString());
                    //行番号
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["ROW_NO"].ToString()))
                        insUrshEntryDetail.ROW_NO = SqlInt16.Parse(retUrshEntityDT.Rows[0]["ROW_NO"].ToString());
                    //確定区分
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["KAKUTEI_KBN"].ToString()))
                        insUrshEntryDetail.KAKUTEI_KBN = SqlInt16.Parse(retUrshEntityDT.Rows[0]["KAKUTEI_KBN"].ToString());
                    //売上支払日付
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString()))
                        insUrshEntryDetail.URIAGESHIHARAI_DATE = SqlDateTime.Parse(retUrshEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString());
                    //伝票区分CD
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                        insUrshEntryDetail.DENPYOU_KBN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString());
                    //品名CD
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["HINMEI_CD"].ToString()))
                        insUrshEntryDetail.HINMEI_CD = retUrshEntityDT.Rows[0]["HINMEI_CD"].ToString();
                    //品名
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["HINMEI_NAME"].ToString()))
                        insUrshEntryDetail.HINMEI_NAME = retUrshEntityDT.Rows[0]["HINMEI_NAME"].ToString();
                    //数量
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SUURYOU"].ToString()))
                        insUrshEntryDetail.SUURYOU = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["SUURYOU"].ToString());
                    //単位CD
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["UNIT_CD"].ToString()))
                        insUrshEntryDetail.UNIT_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["UNIT_CD"].ToString());
                    //単価
                    if (!string.IsNullOrEmpty(dt.Rows[i]["SHIN_TANKA"].ToString()))
                        insUrshEntryDetail.TANKA = Convert.ToDecimal(dt.Rows[i]["SHIN_TANKA"].ToString());
                    //品名税区分空白の場合
                    if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null || string.IsNullOrEmpty(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        //金額
                        if (!string.IsNullOrEmpty(dt.Rows[i]["SHIN_KINGAKU"].ToString()))
                            insUrshEntryDetail.KINGAKU = Convert.ToDecimal(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                        //品名別金額
                        insUrshEntryDetail.HINMEI_KINGAKU = 0;
                    }
                    //品名税区分非空白の場合
                    else
                    {
                        //金額
                        insUrshEntryDetail.KINGAKU = 0;
                        //品名別金額
                        if (!string.IsNullOrEmpty(dt.Rows[i]["SHIN_KINGAKU"].ToString()))
                            insUrshEntryDetail.HINMEI_KINGAKU = Convert.ToDecimal(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                    }
                    //消費税外税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["TAX_SOTO"].ToString()))
                        insUrshEntryDetail.TAX_SOTO = SqlDecimal.Parse(dt.Rows[i]["TAX_SOTO"].ToString());
                    //消費税内税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["TAX_UCHI"].ToString()))
                        insUrshEntryDetail.TAX_UCHI = SqlDecimal.Parse(dt.Rows[i]["TAX_UCHI"].ToString());
                    //品名別税区分CD
                    if (!string.IsNullOrEmpty(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                        insUrshEntryDetail.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString());
                    //品名別消費税外税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString()))
                        insUrshEntryDetail.HINMEI_TAX_SOTO = SqlDecimal.Parse(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString());
                    //品名別消費税内税
                    if (!string.IsNullOrEmpty(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString()))
                        insUrshEntryDetail.HINMEI_TAX_UCHI = SqlDecimal.Parse(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString());
                    //明細備考
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["MEISAI_BIKOU"].ToString()))
                        insUrshEntryDetail.MEISAI_BIKOU = retUrshEntityDT.Rows[0]["MEISAI_BIKOU"].ToString();
                    //荷姿数量
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString()))
                        insUrshEntryDetail.NISUGATA_SUURYOU = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString());
                    //荷姿単位CD
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString()))
                        insUrshEntryDetail.NISUGATA_UNIT_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString());
                    //// 作成と更新情報設定
                    //var dbLogic = new DataBinderLogic<r_framework.Entity.T_Ursh_DETAIL>(insUrshEntryDetail);
                    //dbLogic.SetSystemProperty(insUrshEntryDetail, false);
                    //インサート受付入力明細Entityリストに追加
                    this.insUrshEntryDetailList.Add(insUrshEntryDetail);

                }

                //画面に表示されない、DBに存在しているデータ
                for (int i = 0; i < retUrshTankaSabunDetailDT.Rows.Count; i++)
                {
                    DTOClass dtoDetail = new DTOClass();
                    dtoDetail.SystemID = long.Parse(retUrshTankaSabunDetailDT.Rows[i]["SYSTEM_ID"].ToString());
                    dtoDetail.SEQ = int.Parse(retUrshTankaSabunDetailDT.Rows[i]["SEQ"].ToString());
                    dtoDetail.DetailSystemID = long.Parse(retUrshTankaSabunDetailDT.Rows[i]["DETAIL_SYSTEM_ID"].ToString());
                    //売上/支払明細テブル
                    DataTable retUrshEntityDT = this.daoUrshDetail.GetDataToDataTable(dtoDetail);
                    //売上/支払をインサートDetail
                    this.insUrshEntryDetail = new T_UR_SH_DETAIL();
                    //システムID
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SYSTEM_ID"].ToString()))
                        insUrshEntryDetail.SYSTEM_ID = SqlInt64.Parse(retUrshEntityDT.Rows[0]["SYSTEM_ID"].ToString());
                    //枝番
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SEQ"].ToString()))
                        insUrshEntryDetail.SEQ = SqlInt32.Parse(retUrshEntityDT.Rows[0]["SEQ"].ToString()) + 1;
                    //明細システムID
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString()))
                        insUrshEntryDetail.DETAIL_SYSTEM_ID = SqlInt32.Parse(retUrshEntityDT.Rows[0]["DETAIL_SYSTEM_ID"].ToString());
                    //売上/支払番号
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["UR_SH_NUMBER"].ToString()))
                        insUrshEntryDetail.UR_SH_NUMBER = SqlInt32.Parse(retUrshEntityDT.Rows[0]["UR_SH_NUMBER"].ToString());
                    //行番号
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["ROW_NO"].ToString()))
                        insUrshEntryDetail.ROW_NO = SqlInt16.Parse(retUrshEntityDT.Rows[0]["ROW_NO"].ToString());
                    //確定区分
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["KAKUTEI_KBN"].ToString()))
                        insUrshEntryDetail.KAKUTEI_KBN = SqlInt16.Parse(retUrshEntityDT.Rows[0]["KAKUTEI_KBN"].ToString());
                    //売上支払日付
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString()))
                        insUrshEntryDetail.URIAGESHIHARAI_DATE = SqlDateTime.Parse(retUrshEntityDT.Rows[0]["URIAGESHIHARAI_DATE"].ToString());
                    //伝票区分CD
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString()))
                        insUrshEntryDetail.DENPYOU_KBN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["DENPYOU_KBN_CD"].ToString());
                    //品名CD
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["HINMEI_CD"].ToString()))
                        insUrshEntryDetail.HINMEI_CD = retUrshEntityDT.Rows[0]["HINMEI_CD"].ToString();
                    //品名
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["HINMEI_NAME"].ToString()))
                        insUrshEntryDetail.HINMEI_NAME = retUrshEntityDT.Rows[0]["HINMEI_NAME"].ToString();
                    //数量
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SUURYOU"].ToString()))
                        insUrshEntryDetail.SUURYOU = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["SUURYOU"].ToString());
                    //単位CD
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["UNIT_CD"].ToString()))
                        insUrshEntryDetail.UNIT_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["UNIT_CD"].ToString());
                    //単価
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["TANKA"].ToString()))
                        insUrshEntryDetail.TANKA = Convert.ToDecimal(retUrshEntityDT.Rows[0]["TANKA"].ToString());
                    //金額
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["KINGAKU"].ToString()))
                        insUrshEntryDetail.KINGAKU = Convert.ToDecimal(retUrshEntityDT.Rows[0]["KINGAKU"].ToString());
                    //品名別金額
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["HINMEI_KINGAKU"].ToString()))
                        insUrshEntryDetail.HINMEI_KINGAKU = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["HINMEI_KINGAKU"].ToString());
                    //消費税外税
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["TAX_SOTO"].ToString()))
                        insUrshEntryDetail.TAX_SOTO = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["TAX_SOTO"].ToString());
                    //消費税内税
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["TAX_UCHI"].ToString()))
                        insUrshEntryDetail.TAX_UCHI = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["TAX_UCHI"].ToString());
                    //品名別税区分CD
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["HINMEI_ZEI_KBN_CD"].ToString()))
                        insUrshEntryDetail.HINMEI_ZEI_KBN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["HINMEI_ZEI_KBN_CD"].ToString());
                    //品名別消費税外税
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["HINMEI_TAX_SOTO"].ToString()))
                        insUrshEntryDetail.HINMEI_TAX_SOTO = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["HINMEI_TAX_SOTO"].ToString());
                    //品名別消費税内税
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["HINMEI_TAX_UCHI"].ToString()))
                        insUrshEntryDetail.HINMEI_TAX_UCHI = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["HINMEI_TAX_UCHI"].ToString());
                    //明細備考
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["MEISAI_BIKOU"].ToString()))
                        insUrshEntryDetail.MEISAI_BIKOU = retUrshEntityDT.Rows[0]["MEISAI_BIKOU"].ToString();
                    //荷姿数量
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString()))
                        insUrshEntryDetail.NISUGATA_SUURYOU = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["NISUGATA_SUURYOU"].ToString());
                    //荷姿単位CD
                    if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString()))
                        insUrshEntryDetail.NISUGATA_UNIT_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["NISUGATA_UNIT_CD"].ToString());
                    //// 作成と更新情報設定
                    //var dbLogic = new DataBinderLogic<r_framework.Entity.T_Ursh_DETAIL>(insUrshEntryDetail);
                    //dbLogic.SetSystemProperty(insUrshEntryDetail, false);
                    //インサート売上/支払入力明細Entityリストに追加
                    this.insUrshEntryDetailList.Add(insUrshEntryDetail);
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

        /// <summary>
        ///（受入、出荷、売上/支払）Entityをリストに追加
        /// </summary>
        /// <param name="dt"></param>
        private void CreateEntryEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                //空白行の場合
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }
                else
                {
                    //受入
                    DataTable dtUkeire = dt.Clone();
                    //出荷
                    DataTable dtShuka = dt.Clone();
                    //売上/支払
                    DataTable dtUrshs = dt.Clone();
                    //伝票種類によって、データを分ける(受入、出荷、売上/支払)。
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["DENPYOU_SHURUI"].ToString().Equals("受入"))
                        {
                            dtUkeire.Rows.Add(dt.Rows[i].ItemArray);
                        }
                        else if (dt.Rows[i]["DENPYOU_SHURUI"].ToString().Equals("出荷"))
                        {
                            dtShuka.Rows.Add(dt.Rows[i].ItemArray);
                        }
                        else if (dt.Rows[i]["DENPYOU_SHURUI"].ToString().Equals("売上/支払"))
                        {
                            dtUrshs.Rows.Add(dt.Rows[i].ItemArray);
                        }
                    }

                    //受入Entityリストを作成
                    if (dtUkeire.Rows.Count > 0)
                        CreateUkeireEntryEntity(dtUkeire);
                    //出荷Entityリストを作成
                    if (dtShuka.Rows.Count > 0)
                        CreateShukkaEntryEntity(dtShuka);
                    //売上／支払Entityリストを作成
                    if (dtUrshs.Rows.Count > 0)
                        CreateUrshEntryEntity(dtUrshs);
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

        /// <summary>
        ///受入Entityリストを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateUkeireEntryEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                DTOClass dto = new DTOClass();
                dto.SystemID = long.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                dto.SEQ = int.Parse(dt.Rows[0]["SEQ"].ToString());
                this.dto.SYSTEM_ID = dt.Rows[0]["SYSTEM_ID"].ToString();
                this.dto.SEQ = dt.Rows[0]["SEQ"].ToString();
                //受入Entityテブル
                DataTable retUkeireEntityDT = this.daoUkeireEntry.GetDataToDataTable(dto);
                //受入明細テブル
                DataTable retUkeireTankaSabunDetailDT = this.ukeireTankaSabunDetailDao.GetDataToDataTable(this.dto);

                //売上金額合計
                decimal uriageKingakuTotal = 0;
                //売上外税合計（伝票毎）
                decimal uriageTaxSoto = 0;
                //売上内税合計（伝票毎）
                decimal uriageTaxuchi = 0;
                //売上外税合計（明細毎）
                decimal uriageTaxSotoTotal = 0;
                //売上内税合計（明細毎）
                decimal uriageTaxUchiTotal = 0;
                //品名売上金額合計
                decimal hinmeiUriageKingakuTotal = 0;
                //品名売上外税合計
                decimal hinmeiUriageTaxSotoTotal = 0;
                //品名売上内税合計
                decimal hinmeiUriageTaxUchiTotal = 0;
                //支払金額合計
                decimal shiharaiKingakuTotal = 0;
                //支払外税合計（伝票毎）
                decimal shiharaiTaxSoto = 0;
                //支払内税合計（伝票毎）
                decimal shihraiTaxUchi = 0;
                //支払外税合計（明細毎）
                decimal shiharaiTaxSotoTotal = 0;
                //支払内税合計（明細毎）
                decimal shiharaiTaxUchiTotal = 0;
                //品名支払金額合計
                decimal hinmeiShiharaiKingauTotal = 0;
                //品名支払外税合計
                decimal hinmeiShiharaiTaxSotoTotal = 0;
                //品名支払内税合計
                decimal hinmeiShiharaiTaxUchiTotal = 0;
                // 取引先_請求情報マスタ
                int seikyuuZeikeisanKbn = 0;
                int seikyuuZeiKbnCd = 0;
                int seikyuuKingakuHasuuCd = 0;

                // 取引先_支払情報マスタ
                int shiharaiZeikeisanKbn = 0;
                int shiharaiZeiKbnCd = 0;
                int shiharaiKingakuHasuuCd = 0;

                // 取引先_請求情報マスタ
                var torihikisakiSeikyuu = this.GetTorihikisakiSeikyuu(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                if (torihikisakiSeikyuu != null)
                {
                    // 税計算区分CD　
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.ZEI_KEISAN_KBN_CD), out seikyuuZeikeisanKbn);
                    // 税区分CD　
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.ZEI_KBN_CD), out seikyuuZeiKbnCd);
                    // 消費税端数CD　
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.TAX_HASUU_CD), out seikyuuKingakuHasuuCd);
                }

                // 取引先_支払情報マスタ
                var torihikisakiShiharai = this.GetTorihikisakiShiharai(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                if (torihikisakiShiharai != null)
                {
                    // 税計算区分CD　
                    int.TryParse(Convert.ToString(torihikisakiShiharai.ZEI_KEISAN_KBN_CD), out shiharaiZeikeisanKbn);
                    // 税区分CD　
                    int.TryParse(Convert.ToString(torihikisakiShiharai.ZEI_KBN_CD), out shiharaiZeiKbnCd);
                    // 消費税端数CD　
                    int.TryParse(Convert.ToString(torihikisakiShiharai.TAX_HASUU_CD), out shiharaiKingakuHasuuCd);
                }

                //画面に表示されるデータ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 明細．金額
                    decimal meisaiKingaku = 0;
                    decimal.TryParse(Convert.ToString(dt.Rows[i]["SHIN_KINGAKU"]), out meisaiKingaku);
                    //売上消費税率
                    decimal uriageShouhizeiRate = 0;
                    if (retUkeireEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                        uriageShouhizeiRate = decimal.Parse(retUkeireEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString());
                    //支払消費税率
                    decimal shiharaiShouhizeiRate = 0;
                    if (retUkeireEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                        shiharaiShouhizeiRate = decimal.Parse(retUkeireEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                    //品名税区分空白の場合
                    if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null || string.IsNullOrEmpty(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        //売上
                        if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            //売上金額合計
                            uriageKingakuTotal += meisaiKingaku;
                            //売上税計算区分は明細毎の場合
                            if (retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //売上外税合計（明細毎）
                                    uriageTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * uriageShouhizeiRate, seikyuuKingakuHasuuCd);
                                }
                                //内税
                                else if (retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //売上内税合計（明細毎）
                                    uriageTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + uriageShouhizeiRate));
                                    uriageTaxUchiTotal = CommonCalc.FractionCalc(uriageTaxUchiTotal, seikyuuKingakuHasuuCd);
                                }
                            }
                        }
                        //支払
                        else if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            //支払金額合計
                            shiharaiKingakuTotal += meisaiKingaku;
                            //支払税計算区分は明細毎の場合
                            if (retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //支払外税合計（明細毎）
                                    shiharaiTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * shiharaiShouhizeiRate, shiharaiKingakuHasuuCd);
                                }
                                //内税
                                else if (retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //支払内税合計（明細毎）
                                    shiharaiTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + shiharaiShouhizeiRate));
                                    shiharaiTaxUchiTotal = CommonCalc.FractionCalc(shiharaiTaxUchiTotal, shiharaiKingakuHasuuCd);
                                }
                            }
                        }
                    }
                    else
                    {
                        //売上
                        if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            hinmeiUriageKingakuTotal += decimal.Parse(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                            //外税
                            if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiUriageTaxSotoTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString());
                            }
                            //内税
                            else if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                hinmeiUriageTaxUchiTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString());
                            }
                        }
                        //支払
                        else if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            hinmeiShiharaiKingauTotal += decimal.Parse(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                            //外税
                            if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiShiharaiTaxSotoTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString());
                            }
                            //内税
                            else if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                hinmeiShiharaiTaxUchiTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString());
                            }
                        }
                    }
                }

                //画面に表示されない、DBに存在しているデータ
                for (int i = 0; i < retUkeireTankaSabunDetailDT.Rows.Count; i++)
                {

                    // 明細．金額
                    decimal meisaiKingaku = 0;
                    if (retUkeireTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null ||
                       string.IsNullOrEmpty(retUkeireTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        decimal.TryParse(Convert.ToString(retUkeireTankaSabunDetailDT.Rows[i]["KINGAKU"]), out meisaiKingaku);
                    }
                    else
                    {
                        decimal.TryParse(Convert.ToString(retUkeireTankaSabunDetailDT.Rows[i]["HINMEI_KINGAKU"]), out meisaiKingaku);
                    }
                    decimal taxRate = 0;
                    int zeikeisanKbn = 0;
                    int zeiKbnCd = 0;
                    int kingakuHasuuCd = 0;
                    //売上消費税率
                    decimal uriageShouhizeiRate = 0;
                    if (retUkeireEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                        uriageShouhizeiRate = decimal.Parse(retUkeireEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString());
                    //支払消費税率
                    decimal shiharaiShouhizeiRate = 0;
                    if (retUkeireEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                        shiharaiShouhizeiRate = decimal.Parse(retUkeireEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                    // 伝票区分により、取引先情報で税計算区分CD,税区分CD,消費税端数CDを設定
                    switch (Convert.ToString(retUkeireTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"]))
                    {
                        case "1":
                            //売上消費税率
                            if (retUkeireTankaSabunDetailDT.Rows[i]["URIAGE_SHOUHIZEI_RATE"] != null &&
                                !string.IsNullOrEmpty(retUkeireTankaSabunDetailDT.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                taxRate = decimal.Parse(Convert.ToString(retUkeireTankaSabunDetailDT.Rows[i]["URIAGE_SHOUHIZEI_RATE"]));
                            break;
                        case "2":
                            //支払消費税率
                            if (retUkeireTankaSabunDetailDT.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"] != null &&
                                !string.IsNullOrEmpty(retUkeireTankaSabunDetailDT.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                taxRate = decimal.Parse(Convert.ToString(retUkeireTankaSabunDetailDT.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"]));
                            break;
                        default:
                            break;
                    }

                    decimal TaxSoto = 0;

                    //品名税区分空白の場合
                    if (retUkeireTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null ||
                        string.IsNullOrEmpty(retUkeireTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        //売上
                        if ((retUkeireTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            //売上金額合計
                            uriageKingakuTotal += meisaiKingaku;
                            //売上税計算区分は明細毎の場合
                            if (retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //売上外税合計（明細毎）
                                    //uriageTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * uriageShouhizeiRate, seikyuuKingakuHasuuCd);
                                    decimal.TryParse(Convert.ToString(retUkeireTankaSabunDetailDT.Rows[i]["TAX_SOTO"]), out TaxSoto);
                                    uriageTaxSotoTotal += TaxSoto;
                                }
                                //内税
                                else if (retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //売上内税合計（明細毎）
                                    uriageTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + uriageShouhizeiRate));
                                    uriageTaxUchiTotal = CommonCalc.FractionCalc(uriageTaxUchiTotal, seikyuuKingakuHasuuCd);
                                }
                            }
                        }
                        //支払
                        else if ((retUkeireTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            //支払金額合計
                            shiharaiKingakuTotal += meisaiKingaku;
                            //支払税計算区分は明細毎の場合
                            if (retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //支払外税合計（明細毎）
                                    //shiharaiTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * shiharaiShouhizeiRate, shiharaiKingakuHasuuCd);
                                    decimal.TryParse(Convert.ToString(retUkeireTankaSabunDetailDT.Rows[i]["TAX_SOTO"]), out TaxSoto);
                                    shiharaiTaxSotoTotal += TaxSoto;
                                }
                                //内税
                                else if (retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //支払内税合計（明細毎）
                                    shiharaiTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + shiharaiShouhizeiRate));
                                    shiharaiTaxUchiTotal = CommonCalc.FractionCalc(shiharaiTaxUchiTotal, shiharaiKingakuHasuuCd);
                                }
                            }
                        }
                    }
                    else
                    {
                        //売上
                        if ((retUkeireTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            hinmeiUriageKingakuTotal += meisaiKingaku;
                            //外税
                            if (retUkeireTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiUriageTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * taxRate, kingakuHasuuCd);

                            }
                            //内税
                            else if (retUkeireTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                decimal hinmei_tax_uchi = meisaiKingaku - (meisaiKingaku / (taxRate + 1));
                                hinmei_tax_uchi = CommonCalc.FractionCalc(hinmei_tax_uchi, kingakuHasuuCd);
                                hinmeiUriageTaxUchiTotal += hinmei_tax_uchi;
                            }
                        }
                        //支払
                        else if ((retUkeireTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            hinmeiShiharaiKingauTotal += meisaiKingaku;
                            //外税
                            if (retUkeireTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiShiharaiTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * taxRate, kingakuHasuuCd);
                            }
                            //内税
                            else if (retUkeireTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                decimal hinmei_tax_uchi = meisaiKingaku - (meisaiKingaku / (taxRate + 1));
                                hinmei_tax_uchi = CommonCalc.FractionCalc(hinmei_tax_uchi, kingakuHasuuCd);
                                hinmeiShiharaiTaxUchiTotal += hinmei_tax_uchi;
                            }
                        }
                    }
                }

                //売上税計算区分は伝票毎の場合
                if (retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("1") ||
                    retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("2"))
                {
                    //外税
                    if (retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("1"))
                    {
                        uriageTaxSoto = CommonCalc.FractionCalc(uriageKingakuTotal * decimal.Parse(retUkeireEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()), seikyuuKingakuHasuuCd);
                    }
                    //内税
                    else if (retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("2"))
                    {
                        uriageTaxuchi = CommonCalc.FractionCalc(uriageKingakuTotal * decimal.Parse(retUkeireEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()), seikyuuKingakuHasuuCd);
                    }
                }

                //支払税計算区分は伝票毎の場合
                if (retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("1") ||
                    retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("2"))
                {
                    //外税
                    if (retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("1"))
                    {
                        shiharaiTaxSoto = CommonCalc.FractionCalc(shiharaiKingakuTotal * decimal.Parse(retUkeireEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()), shiharaiKingakuHasuuCd);
                    }
                    //内税
                    else if (retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("2"))
                    {
                        shihraiTaxUchi = CommonCalc.FractionCalc(shiharaiKingakuTotal * decimal.Parse(retUkeireEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()), shiharaiKingakuHasuuCd);
                    }
                }

                //受入入力Entity
                this.insUkeireEntryEntity = new T_UKEIRE_ENTRY();
                // SYSTEM_ID
                if (!string.IsNullOrEmpty(dt.Rows[0]["SYSTEM_ID"].ToString()))
                    this.insUkeireEntryEntity.SYSTEM_ID = SqlInt64.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                // 枝番
                if (!string.IsNullOrEmpty(dt.Rows[0]["SEQ"].ToString()))
                    this.insUkeireEntryEntity.SEQ = SqlInt32.Parse(dt.Rows[0]["SEQ"].ToString()) + 1;
                //滞留登録区分
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["TAIRYUU_KBN"].ToString()))
                    this.insUkeireEntryEntity.TAIRYUU_KBN = SqlBoolean.Parse(retUkeireEntityDT.Rows[0]["TAIRYUU_KBN"].ToString());
                //拠点CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["KYOTEN_CD"].ToString()))
                    this.insUkeireEntryEntity.KYOTEN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["KYOTEN_CD"].ToString());
                //受入番号
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["UKEIRE_NUMBER"].ToString()))
                    this.insUkeireEntryEntity.UKEIRE_NUMBER = SqlInt64.Parse(retUkeireEntityDT.Rows[0]["UKEIRE_NUMBER"].ToString());
                //日連番
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["DATE_NUMBER"].ToString()))
                    this.insUkeireEntryEntity.DATE_NUMBER = SqlInt32.Parse(retUkeireEntityDT.Rows[0]["DATE_NUMBER"].ToString());
                //年連番
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["YEAR_NUMBER"].ToString()))
                    this.insUkeireEntryEntity.YEAR_NUMBER = SqlInt32.Parse(retUkeireEntityDT.Rows[0]["YEAR_NUMBER"].ToString());
                //確定区分
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["KAKUTEI_KBN"].ToString()))
                    this.insUkeireEntryEntity.KAKUTEI_KBN = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["KAKUTEI_KBN"].ToString());
                //伝票日付
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["DENPYOU_DATE"].ToString()))
                    this.insUkeireEntryEntity.DENPYOU_DATE = SqlDateTime.Parse(retUkeireEntityDT.Rows[0]["DENPYOU_DATE"].ToString());
                //売上日付
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["URIAGE_DATE"].ToString()))
                    this.insUkeireEntryEntity.URIAGE_DATE = SqlDateTime.Parse(retUkeireEntityDT.Rows[0]["URIAGE_DATE"].ToString());
                //支払日付
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHIHARAI_DATE"].ToString()))
                    this.insUkeireEntryEntity.SHIHARAI_DATE = SqlDateTime.Parse(retUkeireEntityDT.Rows[0]["SHIHARAI_DATE"].ToString());
                //取引先CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["TORIHIKISAKI_CD"].ToString()))
                    this.insUkeireEntryEntity.TORIHIKISAKI_CD = retUkeireEntityDT.Rows[0]["TORIHIKISAKI_CD"].ToString();
                //取引先名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["TORIHIKISAKI_NAME"].ToString()))
                    this.insUkeireEntryEntity.TORIHIKISAKI_NAME = retUkeireEntityDT.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                //業者CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["GYOUSHA_CD"].ToString()))
                    this.insUkeireEntryEntity.GYOUSHA_CD = retUkeireEntityDT.Rows[0]["GYOUSHA_CD"].ToString();
                //業者名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["GYOUSHA_NAME"].ToString()))
                    this.insUkeireEntryEntity.GYOUSHA_NAME = retUkeireEntityDT.Rows[0]["GYOUSHA_NAME"].ToString();
                //現場CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["GENBA_CD"].ToString()))
                    this.insUkeireEntryEntity.GENBA_CD = retUkeireEntityDT.Rows[0]["GENBA_CD"].ToString();
                //現場名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["GENBA_NAME"].ToString()))
                    this.insUkeireEntryEntity.GENBA_NAME = retUkeireEntityDT.Rows[0]["GENBA_NAME"].ToString();
                //荷降業者CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString()))
                    this.insUkeireEntryEntity.NIOROSHI_GYOUSHA_CD = retUkeireEntityDT.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                //荷降業者名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NIOROSHI_GYOUSHA_NAME"].ToString()))
                    this.insUkeireEntryEntity.NIOROSHI_GYOUSHA_NAME = retUkeireEntityDT.Rows[0]["NIOROSHI_GYOUSHA_NAME"].ToString();
                //荷降現場CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NIOROSHI_GENBA_CD"].ToString()))
                    this.insUkeireEntryEntity.NIOROSHI_GENBA_CD = retUkeireEntityDT.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                //荷降現場名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NIOROSHI_GENBA_NAME"].ToString()))
                    this.insUkeireEntryEntity.NIOROSHI_GENBA_NAME = retUkeireEntityDT.Rows[0]["NIOROSHI_GENBA_NAME"].ToString();
                //営業担当者CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString()))
                    this.insUkeireEntryEntity.EIGYOU_TANTOUSHA_CD = retUkeireEntityDT.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString();
                //営業担当者名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["EIGYOU_TANTOUSHA_NAME"].ToString()))
                    this.insUkeireEntryEntity.EIGYOU_TANTOUSHA_NAME = retUkeireEntityDT.Rows[0]["EIGYOU_TANTOUSHA_NAME"].ToString();
                //入力担当者CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_CD"].ToString()))
                    this.insUkeireEntryEntity.NYUURYOKU_TANTOUSHA_CD = retUkeireEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_CD"].ToString();
                //入力担当者名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_NAME"].ToString()))
                    this.insUkeireEntryEntity.NYUURYOKU_TANTOUSHA_NAME = retUkeireEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                //車輌CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHARYOU_CD"].ToString()))
                    this.insUkeireEntryEntity.SHARYOU_CD = retUkeireEntityDT.Rows[0]["SHARYOU_CD"].ToString();
                //車輌名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHARYOU_NAME"].ToString()))
                    this.insUkeireEntryEntity.SHARYOU_NAME = retUkeireEntityDT.Rows[0]["SHARYOU_NAME"].ToString();
                //車種CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHASHU_CD"].ToString()))
                    this.insUkeireEntryEntity.SHASHU_CD = retUkeireEntityDT.Rows[0]["SHASHU_CD"].ToString();
                //車種名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHASHU_NAME"].ToString()))
                    this.insUkeireEntryEntity.SHASHU_NAME = retUkeireEntityDT.Rows[0]["SHASHU_NAME"].ToString();
                //運搬業者CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["UNPAN_GYOUSHA_CD"].ToString()))
                    this.insUkeireEntryEntity.UNPAN_GYOUSHA_CD = retUkeireEntityDT.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                //運搬業者名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString()))
                    this.insUkeireEntryEntity.UNPAN_GYOUSHA_NAME = retUkeireEntityDT.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString();
                //運転者CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["UNTENSHA_CD"].ToString()))
                    this.insUkeireEntryEntity.UNTENSHA_CD = retUkeireEntityDT.Rows[0]["UNTENSHA_CD"].ToString();
                //運転者名
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["UNTENSHA_NAME"].ToString()))
                    this.insUkeireEntryEntity.UNTENSHA_NAME = retUkeireEntityDT.Rows[0]["UNTENSHA_NAME"].ToString();
                //人数
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NINZUU_CNT"].ToString()))
                    this.insUkeireEntryEntity.NINZUU_CNT = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["NINZUU_CNT"].ToString());
                //形態区分CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["KEITAI_KBN_CD"].ToString()))
                    this.insUkeireEntryEntity.KEITAI_KBN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["KEITAI_KBN_CD"].ToString());
                //台貫区分
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["DAIKAN_KBN"].ToString()))
                    this.insUkeireEntryEntity.DAIKAN_KBN = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["DAIKAN_KBN"].ToString());
                //コンテナ操作CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["CONTENA_SOUSA_CD"].ToString()))
                    this.insUkeireEntryEntity.CONTENA_SOUSA_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["CONTENA_SOUSA_CD"].ToString());
                //マニフェスト種類CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["MANIFEST_SHURUI_CD"].ToString()))
                    this.insUkeireEntryEntity.MANIFEST_SHURUI_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["MANIFEST_SHURUI_CD"].ToString());
                //マニフェスト手配CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["MANIFEST_TEHAI_CD"].ToString()))
                    this.insUkeireEntryEntity.MANIFEST_TEHAI_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["MANIFEST_TEHAI_CD"].ToString());
                //伝票備考
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["DENPYOU_BIKOU"].ToString()))
                    this.insUkeireEntryEntity.DENPYOU_BIKOU = retUkeireEntityDT.Rows[0]["DENPYOU_BIKOU"].ToString();
                //滞留備考
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["TAIRYUU_BIKOU"].ToString()))
                    this.insUkeireEntryEntity.TAIRYUU_BIKOU = retUkeireEntityDT.Rows[0]["TAIRYUU_BIKOU"].ToString();
                //受付番号
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["UKETSUKE_NUMBER"].ToString()))
                    this.insUkeireEntryEntity.UKETSUKE_NUMBER = SqlInt64.Parse(retUkeireEntityDT.Rows[0]["UKETSUKE_NUMBER"].ToString());
                //計量番号
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["KEIRYOU_NUMBER"].ToString()))
                    this.insUkeireEntryEntity.KEIRYOU_NUMBER = SqlInt64.Parse(retUkeireEntityDT.Rows[0]["KEIRYOU_NUMBER"].ToString());
                //領収書番号
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["RECEIPT_NUMBER"].ToString()))
                    this.insUkeireEntryEntity.RECEIPT_NUMBER = SqlInt32.Parse(retUkeireEntityDT.Rows[0]["RECEIPT_NUMBER"].ToString());
                //正味合計
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["NET_TOTAL"].ToString()))
                    this.insUkeireEntryEntity.NET_TOTAL = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["NET_TOTAL"].ToString());
                //売上消費税率
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                    this.insUkeireEntryEntity.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString());
                //売上金額合計
                this.insUkeireEntryEntity.URIAGE_KINGAKU_TOTAL = uriageKingakuTotal;
                //売上伝票毎消費税外税
                this.insUkeireEntryEntity.URIAGE_TAX_SOTO = uriageTaxSoto;
                //売上伝票毎消費税内税
                this.insUkeireEntryEntity.URIAGE_TAX_UCHI = uriageTaxuchi;
                //売上明細毎消費税外税合計
                this.insUkeireEntryEntity.URIAGE_TAX_SOTO_TOTAL = uriageTaxSotoTotal;
                //売上明細毎消費税内税合計
                this.insUkeireEntryEntity.URIAGE_TAX_UCHI_TOTAL = uriageTaxUchiTotal;
                //品名別売上金額合計
                this.insUkeireEntryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = hinmeiUriageKingakuTotal;
                //品名別売上消費税外税合計
                this.insUkeireEntryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = hinmeiUriageTaxSotoTotal;
                //品名別売上消費税内税合計
                this.insUkeireEntryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = hinmeiUriageTaxUchiTotal;
                //支払消費税率
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                    this.insUkeireEntryEntity.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(retUkeireEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                //支払金額合計
                this.insUkeireEntryEntity.SHIHARAI_KINGAKU_TOTAL = shiharaiKingakuTotal;
                //支払伝票毎消費税外税
                this.insUkeireEntryEntity.SHIHARAI_TAX_SOTO = shiharaiTaxSoto;
                //支払伝票毎消費税内税
                this.insUkeireEntryEntity.SHIHARAI_TAX_UCHI = shihraiTaxUchi;
                //支払明細毎消費税外税合計
                this.insUkeireEntryEntity.SHIHARAI_TAX_SOTO_TOTAL = shiharaiTaxSotoTotal;
                //支払明細毎消費税内税合計
                this.insUkeireEntryEntity.SHIHARAI_TAX_UCHI_TOTAL = shiharaiTaxUchiTotal;
                //品名別支払金額合計
                this.insUkeireEntryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = hinmeiShiharaiKingauTotal;
                //品名別支払消費税外税合計
                this.insUkeireEntryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = hinmeiShiharaiTaxSotoTotal;
                //品名別支払消費税内税合計
                this.insUkeireEntryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = hinmeiShiharaiTaxUchiTotal;
                //売上税計算区分CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                    this.insUkeireEntryEntity.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                //売上税区分CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString()))
                    this.insUkeireEntryEntity.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString());
                //売上取引区分CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                    this.insUkeireEntryEntity.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["URIAGE_TORIHIKI_KBN_CD"].ToString());
                //支払税計算区分CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                    this.insUkeireEntryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                //支払税区分CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString()))
                    this.insUkeireEntryEntity.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString());
                //支払取引区分CD
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                    this.insUkeireEntryEntity.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(retUkeireEntityDT.Rows[0]["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                // 作成と更新情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.T_UKEIRE_ENTRY>(insUkeireEntryEntity);
                dbLogic.SetSystemProperty(insUkeireEntryEntity, false);
                // 作成日
                if (!string.IsNullOrEmpty(retUkeireEntityDT.Rows[0]["CREATE_DATE"].ToString()))
                    this.insUkeireEntryEntity.CREATE_DATE = SqlDateTime.Parse(retUkeireEntityDT.Rows[0]["CREATE_DATE"].ToString());
                // 削除フラグ
                this.insUkeireEntryEntity.DELETE_FLG = false;
                //インサート受付入力Entityリストに追加
                insUkeireEntryEntityList.Add(insUkeireEntryEntity);
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

        /// <summary>
        ///出荷Entityリストを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateShukkaEntryEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart();
                DTOClass dto = new DTOClass();
                dto.SystemID = long.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                dto.SEQ = int.Parse(dt.Rows[0]["SEQ"].ToString());
                this.dto.SYSTEM_ID = dt.Rows[0]["SYSTEM_ID"].ToString();
                this.dto.SEQ = dt.Rows[0]["SEQ"].ToString();
                //出荷Entityテブル
                DataTable retShukkaEntityDT = this.daoShukkaEntry.GetDataToDataTable(dto);
                //出荷明細テブル
                DataTable retShukkaTankaSabunDetailDT = this.shukkaTankaSabunDetailDao.GetDataToDataTable(this.dto);

                //売上金額合計
                decimal uriageKingakuTotal = 0;
                //売上外税合計（伝票毎）
                decimal uriageTaxSoto = 0;
                //売上内税合計（伝票毎）
                decimal uriageTaxuchi = 0;
                //売上外税合計（明細毎）
                decimal uriageTaxSotoTotal = 0;
                //売上内税合計（明細毎）
                decimal uriageTaxUchiTotal = 0;
                //品名売上金額合計
                decimal hinmeiUriageKingakuTotal = 0;
                //品名売上外税合計
                decimal hinmeiUriageTaxSotoTotal = 0;
                //品名売上内税合計
                decimal hinmeiUriageTaxUchiTotal = 0;
                //支払金額合計
                decimal shiharaiKingakuTotal = 0;
                //支払外税合計（伝票毎）
                decimal shiharaiTaxSoto = 0;
                //支払内税合計（伝票毎）
                decimal shihraiTaxUchi = 0;
                //支払外税合計（明細毎）
                decimal shiharaiTaxSotoTotal = 0;
                //支払内税合計（明細毎）
                decimal shiharaiTaxUchiTotal = 0;
                //品名支払金額合計
                decimal hinmeiShiharaiKingauTotal = 0;
                //品名支払外税合計
                decimal hinmeiShiharaiTaxSotoTotal = 0;
                //品名支払内税合計
                decimal hinmeiShiharaiTaxUchiTotal = 0;
                // 取引先_請求情報マスタ
                int seikyuuZeikeisanKbn = 0;
                int seikyuuZeiKbnCd = 0;
                int seikyuuKingakuHasuuCd = 0;

                // 取引先_支払情報マスタ
                int shiharaiZeikeisanKbn = 0;
                int shiharaiZeiKbnCd = 0;
                int shiharaiKingakuHasuuCd = 0;

                // 取引先_請求情報マスタ
                var torihikisakiSeikyuu = this.GetTorihikisakiSeikyuu(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                if (torihikisakiSeikyuu != null)
                {
                    // 税計算区分CD　
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.ZEI_KEISAN_KBN_CD), out seikyuuZeikeisanKbn);
                    // 税区分CD　
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.ZEI_KBN_CD), out seikyuuZeiKbnCd);
                    // 消費税端数CD　
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.TAX_HASUU_CD), out seikyuuKingakuHasuuCd);
                }

                // 取引先_支払情報マスタ
                var torihikisakiShiharai = this.GetTorihikisakiShiharai(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                if (torihikisakiShiharai != null)
                {
                    // 税計算区分CD　
                    int.TryParse(Convert.ToString(torihikisakiShiharai.ZEI_KEISAN_KBN_CD), out shiharaiZeikeisanKbn);
                    // 税区分CD　
                    int.TryParse(Convert.ToString(torihikisakiShiharai.ZEI_KBN_CD), out shiharaiZeiKbnCd);
                    // 消費税端数CD　
                    int.TryParse(Convert.ToString(torihikisakiShiharai.TAX_HASUU_CD), out shiharaiKingakuHasuuCd);
                }

                //画面に表示されるデータ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 明細．金額
                    decimal meisaiKingaku = 0;
                    decimal.TryParse(Convert.ToString(dt.Rows[i]["SHIN_KINGAKU"]), out meisaiKingaku);
                    //売上消費税率
                    decimal uriageShouhizeiRate = 0;
                    if (retShukkaEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                        uriageShouhizeiRate = decimal.Parse(retShukkaEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString());
                    //支払消費税率
                    decimal shiharaiShouhizeiRate = 0;
                    if (retShukkaEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                        shiharaiShouhizeiRate = decimal.Parse(retShukkaEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                    //品名税区分空白の場合
                    if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null || string.IsNullOrEmpty(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        //売上
                        if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            //売上金額合計
                            uriageKingakuTotal += meisaiKingaku;
                            //売上税計算区分は明細毎の場合
                            if (retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //売上外税合計（明細毎）
                                    uriageTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * uriageShouhizeiRate, seikyuuKingakuHasuuCd);
                                }
                                //内税
                                else if (retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //売上内税合計（明細毎）
                                    uriageTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + uriageShouhizeiRate));
                                    uriageTaxUchiTotal = CommonCalc.FractionCalc(uriageTaxUchiTotal, seikyuuKingakuHasuuCd);
                                }
                            }
                        }
                        //支払
                        else if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            //支払金額合計
                            shiharaiKingakuTotal += meisaiKingaku;
                            //支払税計算区分は明細毎の場合
                            if (retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //支払外税合計（明細毎）
                                    shiharaiTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * shiharaiShouhizeiRate, shiharaiKingakuHasuuCd);
                                }
                                //内税
                                else if (retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //支払内税合計（明細毎）
                                    shiharaiTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + shiharaiShouhizeiRate));
                                    shiharaiTaxUchiTotal = CommonCalc.FractionCalc(shiharaiTaxUchiTotal, shiharaiKingakuHasuuCd);
                                }
                            }
                        }
                    }
                    else
                    {
                        //売上
                        if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            hinmeiUriageKingakuTotal += decimal.Parse(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                            //外税
                            if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiUriageTaxSotoTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString());
                            }
                            //内税
                            else if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                hinmeiUriageTaxUchiTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString());
                            }
                        }
                        //支払
                        else if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            hinmeiShiharaiKingauTotal += decimal.Parse(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                            //外税
                            if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiShiharaiTaxSotoTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString());
                            }
                            //内税
                            else if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                hinmeiShiharaiTaxUchiTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString());
                            }
                        }
                    }
                }

                //画面に表示されない、DBに存在しているデータ
                for (int i = 0; i < retShukkaTankaSabunDetailDT.Rows.Count; i++)
                {

                    // 明細．金額
                    decimal meisaiKingaku = 0;
                    if (retShukkaTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null ||
                      string.IsNullOrEmpty(retShukkaTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        decimal.TryParse(Convert.ToString(retShukkaTankaSabunDetailDT.Rows[i]["KINGAKU"]), out meisaiKingaku);
                    }
                    else
                    {
                        decimal.TryParse(Convert.ToString(retShukkaTankaSabunDetailDT.Rows[i]["HINMEI_KINGAKU"]), out meisaiKingaku);
                    }
                    decimal taxRate = 0;
                    int zeikeisanKbn = 0;
                    int zeiKbnCd = 0;
                    int kingakuHasuuCd = 0;
                    //売上消費税率
                    decimal uriageShouhizeiRate = 0;
                    if (retShukkaEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                        uriageShouhizeiRate = decimal.Parse(retShukkaEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString());
                    //支払消費税率
                    decimal shiharaiShouhizeiRate = 0;
                    if (retShukkaEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                        shiharaiShouhizeiRate = decimal.Parse(retShukkaEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                    // 伝票区分により、取引先情報で税計算区分CD,税区分CD,消費税端数CDを設定
                    switch (Convert.ToString(retShukkaTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"]))
                    {
                        case "1":
                            //売上消費税率
                            if (retShukkaTankaSabunDetailDT.Rows[i]["URIAGE_SHOUHIZEI_RATE"] != null &&
                                !string.IsNullOrEmpty(retShukkaTankaSabunDetailDT.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                taxRate = decimal.Parse(Convert.ToString(retShukkaTankaSabunDetailDT.Rows[i]["URIAGE_SHOUHIZEI_RATE"]));
                            break;
                        case "2":
                            //支払消費税率
                            if (retShukkaTankaSabunDetailDT.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"] != null &&
                                !string.IsNullOrEmpty(retShukkaTankaSabunDetailDT.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                taxRate = decimal.Parse(Convert.ToString(retShukkaTankaSabunDetailDT.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"]));
                            break;
                        default:
                            break;
                    }

                    decimal TaxSoto = 0;

                    //品名税区分空白の場合
                    if (retShukkaTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null ||
                        string.IsNullOrEmpty(retShukkaTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        //売上
                        if ((retShukkaTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            //売上金額合計
                            uriageKingakuTotal += meisaiKingaku;
                            //売上税計算区分は明細毎の場合
                            if (retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //売上外税合計（明細毎）
                                    //uriageTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * uriageShouhizeiRate, seikyuuKingakuHasuuCd);
                                    decimal.TryParse(Convert.ToString(retShukkaTankaSabunDetailDT.Rows[i]["TAX_SOTO"]), out TaxSoto);
                                    uriageTaxSotoTotal += TaxSoto;
                                }
                                //内税
                                else if (retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //売上内税合計（明細毎）
                                    uriageTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + uriageShouhizeiRate));
                                    uriageTaxUchiTotal = CommonCalc.FractionCalc(uriageTaxUchiTotal, seikyuuKingakuHasuuCd);
                                }
                            }
                        }
                        //支払
                        else if ((retShukkaTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            //支払金額合計
                            shiharaiKingakuTotal += meisaiKingaku;
                            //支払税計算区分は明細毎の場合
                            if (retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //支払外税合計（明細毎）
                                    //shiharaiTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * shiharaiShouhizeiRate, shiharaiKingakuHasuuCd);
                                    decimal.TryParse(Convert.ToString(retShukkaTankaSabunDetailDT.Rows[i]["TAX_SOTO"]), out TaxSoto);
                                    shiharaiTaxSotoTotal += TaxSoto;
                                }
                                //内税
                                else if (retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //支払内税合計（明細毎）
                                    shiharaiTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + shiharaiShouhizeiRate));
                                    shiharaiTaxUchiTotal = CommonCalc.FractionCalc(shiharaiTaxUchiTotal, shiharaiKingakuHasuuCd);
                                }
                            }
                        }
                    }
                    else
                    {
                        //売上
                        if ((retShukkaTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            hinmeiUriageKingakuTotal += meisaiKingaku;
                            //外税
                            if (retShukkaTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiUriageTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * taxRate, kingakuHasuuCd);

                            }
                            //内税
                            else if (retShukkaTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                decimal hinmei_tax_uchi = meisaiKingaku - (meisaiKingaku / (taxRate + 1));
                                hinmei_tax_uchi = CommonCalc.FractionCalc(hinmei_tax_uchi, kingakuHasuuCd);
                                hinmeiUriageTaxUchiTotal += hinmei_tax_uchi;
                            }
                        }
                        //支払
                        else if ((retShukkaTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            hinmeiShiharaiKingauTotal += meisaiKingaku;
                            //外税
                            if (retShukkaTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiShiharaiTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * taxRate, kingakuHasuuCd);
                            }
                            //内税
                            else if (retShukkaTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                decimal hinmei_tax_uchi = meisaiKingaku - (meisaiKingaku / (taxRate + 1));
                                hinmei_tax_uchi = CommonCalc.FractionCalc(hinmei_tax_uchi, kingakuHasuuCd);
                                hinmeiShiharaiTaxUchiTotal += hinmei_tax_uchi;
                            }
                        }
                    }
                }

                //売上税計算区分は伝票毎の場合
                if (retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("1") ||
                    retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("2"))
                {
                    //外税
                    if (retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("1"))
                    {
                        uriageTaxSoto = CommonCalc.FractionCalc(uriageKingakuTotal * decimal.Parse(retShukkaEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()), seikyuuKingakuHasuuCd);
                    }
                    //内税
                    else if (retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("2"))
                    {
                        uriageTaxuchi = CommonCalc.FractionCalc(uriageKingakuTotal * decimal.Parse(retShukkaEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()), seikyuuKingakuHasuuCd);
                    }
                }

                //支払税計算区分は伝票毎の場合
                if (retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("1") ||
                    retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("2"))
                {
                    //外税
                    if (retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("1"))
                    {
                        shiharaiTaxSoto = CommonCalc.FractionCalc(shiharaiKingakuTotal * decimal.Parse(retShukkaEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()), shiharaiKingakuHasuuCd);
                    }
                    //内税
                    else if (retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("2"))
                    {
                        shihraiTaxUchi = CommonCalc.FractionCalc(shiharaiKingakuTotal * decimal.Parse(retShukkaEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()), shiharaiKingakuHasuuCd);
                    }
                }

                //出荷入力Entity
                this.insShukkaEntryEntity = new T_SHUKKA_ENTRY();
                // SYSTEM_ID
                if (!string.IsNullOrEmpty(dt.Rows[0]["SYSTEM_ID"].ToString()))
                    this.insShukkaEntryEntity.SYSTEM_ID = SqlInt64.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                // 枝番
                if (!string.IsNullOrEmpty(dt.Rows[0]["SEQ"].ToString()))
                    this.insShukkaEntryEntity.SEQ = SqlInt32.Parse(dt.Rows[0]["SEQ"].ToString()) + 1;
                //滞留登録区分
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["TAIRYUU_KBN"].ToString()))
                    this.insShukkaEntryEntity.TAIRYUU_KBN = SqlBoolean.Parse(retShukkaEntityDT.Rows[0]["TAIRYUU_KBN"].ToString());
                //拠点CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KYOTEN_CD"].ToString()))
                    this.insShukkaEntryEntity.KYOTEN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["KYOTEN_CD"].ToString());
                //受入番号
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHUKKA_NUMBER"].ToString()))
                    this.insShukkaEntryEntity.SHUKKA_NUMBER = SqlInt64.Parse(retShukkaEntityDT.Rows[0]["Shukka_NUMBER"].ToString());
                //日連番
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["DATE_NUMBER"].ToString()))
                    this.insShukkaEntryEntity.DATE_NUMBER = SqlInt32.Parse(retShukkaEntityDT.Rows[0]["DATE_NUMBER"].ToString());
                //年連番
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["YEAR_NUMBER"].ToString()))
                    this.insShukkaEntryEntity.YEAR_NUMBER = SqlInt32.Parse(retShukkaEntityDT.Rows[0]["YEAR_NUMBER"].ToString());
                //確定区分
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KAKUTEI_KBN"].ToString()))
                    this.insShukkaEntryEntity.KAKUTEI_KBN = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["KAKUTEI_KBN"].ToString());
                //伝票日付
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["DENPYOU_DATE"].ToString()))
                    this.insShukkaEntryEntity.DENPYOU_DATE = SqlDateTime.Parse(retShukkaEntityDT.Rows[0]["DENPYOU_DATE"].ToString());
                //売上日付
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["URIAGE_DATE"].ToString()))
                    this.insShukkaEntryEntity.URIAGE_DATE = SqlDateTime.Parse(retShukkaEntityDT.Rows[0]["URIAGE_DATE"].ToString());
                //支払日付
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHIHARAI_DATE"].ToString()))
                    this.insShukkaEntryEntity.SHIHARAI_DATE = SqlDateTime.Parse(retShukkaEntityDT.Rows[0]["SHIHARAI_DATE"].ToString());
                //取引先CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["TORIHIKISAKI_CD"].ToString()))
                    this.insShukkaEntryEntity.TORIHIKISAKI_CD = retShukkaEntityDT.Rows[0]["TORIHIKISAKI_CD"].ToString();
                //取引先名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["TORIHIKISAKI_NAME"].ToString()))
                    this.insShukkaEntryEntity.TORIHIKISAKI_NAME = retShukkaEntityDT.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                //業者CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["GYOUSHA_CD"].ToString()))
                    this.insShukkaEntryEntity.GYOUSHA_CD = retShukkaEntityDT.Rows[0]["GYOUSHA_CD"].ToString();
                //業者名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["GYOUSHA_NAME"].ToString()))
                    this.insShukkaEntryEntity.GYOUSHA_NAME = retShukkaEntityDT.Rows[0]["GYOUSHA_NAME"].ToString();
                //現場CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["GENBA_CD"].ToString()))
                    this.insShukkaEntryEntity.GENBA_CD = retShukkaEntityDT.Rows[0]["GENBA_CD"].ToString();
                //現場名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["GENBA_NAME"].ToString()))
                    this.insShukkaEntryEntity.GENBA_NAME = retShukkaEntityDT.Rows[0]["GENBA_NAME"].ToString();
                //荷積業者CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString()))
                    this.insShukkaEntryEntity.NIZUMI_GYOUSHA_CD = retShukkaEntityDT.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString();
                //荷積業者名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NIZUMI_GYOUSHA_NAME"].ToString()))
                    this.insShukkaEntryEntity.NIZUMI_GYOUSHA_NAME = retShukkaEntityDT.Rows[0]["NIZUMI_GYOUSHA_NAME"].ToString();
                //荷積現場CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NIZUMI_GENBA_CD"].ToString()))
                    this.insShukkaEntryEntity.NIZUMI_GENBA_CD = retShukkaEntityDT.Rows[0]["NIZUMI_GENBA_CD"].ToString();
                //荷積現場名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NIZUMI_GENBA_NAME"].ToString()))
                    this.insShukkaEntryEntity.NIZUMI_GENBA_NAME = retShukkaEntityDT.Rows[0]["NIZUMI_GENBA_NAME"].ToString();
                //営業担当者CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString()))
                    this.insShukkaEntryEntity.EIGYOU_TANTOUSHA_CD = retShukkaEntityDT.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString();
                //営業担当者名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["EIGYOU_TANTOUSHA_NAME"].ToString()))
                    this.insShukkaEntryEntity.EIGYOU_TANTOUSHA_NAME = retShukkaEntityDT.Rows[0]["EIGYOU_TANTOUSHA_NAME"].ToString();
                //入力担当者CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_CD"].ToString()))
                    this.insShukkaEntryEntity.NYUURYOKU_TANTOUSHA_CD = retShukkaEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_CD"].ToString();
                //入力担当者名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_NAME"].ToString()))
                    this.insShukkaEntryEntity.NYUURYOKU_TANTOUSHA_NAME = retShukkaEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                //車輌CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHARYOU_CD"].ToString()))
                    this.insShukkaEntryEntity.SHARYOU_CD = retShukkaEntityDT.Rows[0]["SHARYOU_CD"].ToString();
                //車輌名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHARYOU_NAME"].ToString()))
                    this.insShukkaEntryEntity.SHARYOU_NAME = retShukkaEntityDT.Rows[0]["SHARYOU_NAME"].ToString();
                //車種CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHASHU_CD"].ToString()))
                    this.insShukkaEntryEntity.SHASHU_CD = retShukkaEntityDT.Rows[0]["SHASHU_CD"].ToString();
                //車種名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHASHU_NAME"].ToString()))
                    this.insShukkaEntryEntity.SHASHU_NAME = retShukkaEntityDT.Rows[0]["SHASHU_NAME"].ToString();
                //運搬業者CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["UNPAN_GYOUSHA_CD"].ToString()))
                    this.insShukkaEntryEntity.UNPAN_GYOUSHA_CD = retShukkaEntityDT.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                //運搬業者名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString()))
                    this.insShukkaEntryEntity.UNPAN_GYOUSHA_NAME = retShukkaEntityDT.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString();
                //運転者CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["UNTENSHA_CD"].ToString()))
                    this.insShukkaEntryEntity.UNTENSHA_CD = retShukkaEntityDT.Rows[0]["UNTENSHA_CD"].ToString();
                //運転者名
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["UNTENSHA_NAME"].ToString()))
                    this.insShukkaEntryEntity.UNTENSHA_NAME = retShukkaEntityDT.Rows[0]["UNTENSHA_NAME"].ToString();
                //人数
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NINZUU_CNT"].ToString()))
                    this.insShukkaEntryEntity.NINZUU_CNT = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["NINZUU_CNT"].ToString());
                //形態区分CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KEITAI_KBN_CD"].ToString()))
                    this.insShukkaEntryEntity.KEITAI_KBN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["KEITAI_KBN_CD"].ToString());
                //台貫区分
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["DAIKAN_KBN"].ToString()))
                    this.insShukkaEntryEntity.DAIKAN_KBN = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["DAIKAN_KBN"].ToString());
                //コンテナ操作CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["CONTENA_SOUSA_CD"].ToString()))
                    this.insShukkaEntryEntity.CONTENA_SOUSA_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["CONTENA_SOUSA_CD"].ToString());
                //マニフェスト種類CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["MANIFEST_SHURUI_CD"].ToString()))
                    this.insShukkaEntryEntity.MANIFEST_SHURUI_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["MANIFEST_SHURUI_CD"].ToString());
                //マニフェスト手配CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["MANIFEST_TEHAI_CD"].ToString()))
                    this.insShukkaEntryEntity.MANIFEST_TEHAI_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["MANIFEST_TEHAI_CD"].ToString());
                //伝票備考
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["DENPYOU_BIKOU"].ToString()))
                    this.insShukkaEntryEntity.DENPYOU_BIKOU = retShukkaEntityDT.Rows[0]["DENPYOU_BIKOU"].ToString();
                //滞留備考
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["TAIRYUU_BIKOU"].ToString()))
                    this.insShukkaEntryEntity.TAIRYUU_BIKOU = retShukkaEntityDT.Rows[0]["TAIRYUU_BIKOU"].ToString();
                //受付番号
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["UKETSUKE_NUMBER"].ToString()))
                    this.insShukkaEntryEntity.UKETSUKE_NUMBER = SqlInt64.Parse(retShukkaEntityDT.Rows[0]["UKETSUKE_NUMBER"].ToString());
                //計量番号
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KEIRYOU_NUMBER"].ToString()))
                    this.insShukkaEntryEntity.KEIRYOU_NUMBER = SqlInt64.Parse(retShukkaEntityDT.Rows[0]["KEIRYOU_NUMBER"].ToString());
                //領収書番号
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["RECEIPT_NUMBER"].ToString()))
                    this.insShukkaEntryEntity.RECEIPT_NUMBER = SqlInt32.Parse(retShukkaEntityDT.Rows[0]["RECEIPT_NUMBER"].ToString());
                //正味合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["NET_TOTAL"].ToString()))
                    this.insShukkaEntryEntity.NET_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["NET_TOTAL"].ToString());
                //売上消費税率
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                    this.insShukkaEntryEntity.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString());
                //売上金額合計
                this.insShukkaEntryEntity.URIAGE_AMOUNT_TOTAL = uriageKingakuTotal;
                //売上伝票毎消費税外税
                this.insShukkaEntryEntity.URIAGE_TAX_SOTO = uriageTaxSoto;
                //売上伝票毎消費税内税
                this.insShukkaEntryEntity.URIAGE_TAX_UCHI = uriageTaxuchi;
                //売上明細毎消費税外税合計
                this.insShukkaEntryEntity.URIAGE_TAX_SOTO_TOTAL = uriageTaxSotoTotal;
                //売上明細毎消費税内税合計
                this.insShukkaEntryEntity.URIAGE_TAX_UCHI_TOTAL = uriageTaxUchiTotal;
                //品名別売上金額合計
                this.insShukkaEntryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = hinmeiUriageKingakuTotal;
                //品名別売上消費税外税合計
                this.insShukkaEntryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = hinmeiUriageTaxSotoTotal;
                //品名別売上消費税内税合計
                this.insShukkaEntryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = hinmeiUriageTaxUchiTotal;
                //支払消費税率
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                    this.insShukkaEntryEntity.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                //支払金額合計
                this.insShukkaEntryEntity.SHIHARAI_AMOUNT_TOTAL = shiharaiKingakuTotal;
                //支払伝票毎消費税外税
                this.insShukkaEntryEntity.SHIHARAI_TAX_SOTO = shiharaiTaxSoto;
                //支払伝票毎消費税内税
                this.insShukkaEntryEntity.SHIHARAI_TAX_UCHI = shihraiTaxUchi;
                //支払明細毎消費税外税合計
                this.insShukkaEntryEntity.SHIHARAI_TAX_SOTO_TOTAL = shiharaiTaxSotoTotal;
                //支払明細毎消費税内税合計
                this.insShukkaEntryEntity.SHIHARAI_TAX_UCHI_TOTAL = shiharaiTaxUchiTotal;
                //品名別支払金額合計
                this.insShukkaEntryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = hinmeiShiharaiKingauTotal;
                //品名別支払消費税外税合計
                this.insShukkaEntryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = hinmeiShiharaiTaxSotoTotal;
                //品名別支払消費税内税合計
                this.insShukkaEntryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = hinmeiShiharaiTaxUchiTotal;
                //売上税計算区分CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                    this.insShukkaEntryEntity.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                //売上税区分CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString()))
                    this.insShukkaEntryEntity.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString());
                //売上取引区分CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                    this.insShukkaEntryEntity.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["URIAGE_TORIHIKI_KBN_CD"].ToString());
                //支払税計算区分CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                    this.insShukkaEntryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                //支払税区分CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString()))
                    this.insShukkaEntryEntity.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString());
                //支払取引区分CD
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                    this.insShukkaEntryEntity.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(retShukkaEntityDT.Rows[0]["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                //検収日付
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_DATE"].ToString()))
                    this.insShukkaEntryEntity.KENSHU_DATE = SqlDateTime.Parse(retShukkaEntityDT.Rows[0]["KENSHU_DATE"].ToString());
                //出荷時正味合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHUKKA_NET_TOTAL"].ToString()))
                    this.insShukkaEntryEntity.SHUKKA_NET_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["SHUKKA_NET_TOTAL"].ToString());
                //検収時正味合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_NET_TOTAL"].ToString()))
                    this.insShukkaEntryEntity.KENSHU_NET_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_NET_TOTAL"].ToString());
                //差分
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SABUN"].ToString()))
                    this.insShukkaEntryEntity.SABUN = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["SABUN"].ToString());
                //出荷金額合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SHUKKA_KINGAKU_TOTAL"].ToString()))
                    this.insShukkaEntryEntity.SHUKKA_KINGAKU_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["SHUKKA_KINGAKU_TOTAL"].ToString());
                //検収金額合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_KINGAKU_TOTAL"].ToString()))
                    this.insShukkaEntryEntity.KENSHU_KINGAKU_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_KINGAKU_TOTAL"].ToString());
                //差額
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["SAGAKU"].ToString()))
                    this.insShukkaEntryEntity.SAGAKU = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["SAGAKU"].ToString());
                // 作成と更新情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.T_SHUKKA_ENTRY>(insShukkaEntryEntity);
                dbLogic.SetSystemProperty(insShukkaEntryEntity, false);
                // 作成日
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["CREATE_DATE"].ToString()))
                    this.insShukkaEntryEntity.CREATE_DATE = SqlDateTime.Parse(retShukkaEntityDT.Rows[0]["CREATE_DATE"].ToString());

                // 要検収区分
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_MUST_KBN"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_MUST_KBN = SqlBoolean.Parse(retShukkaEntityDT.Rows[0]["KENSHU_MUST_KBN"].ToString());
                }

                // 検収売上消費税率
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_SHOUHIZEI_RATE"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_SHOUHIZEI_RATE"].ToString());
                }

                // 検収売上金額合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_AMOUNT_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_URIAGE_AMOUNT_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_AMOUNT_TOTAL"].ToString());
                }

                // 検収売上伝票毎消費税外税
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_TAX_SOTO"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_URIAGE_TAX_SOTO = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_TAX_SOTO"].ToString());
                }

                // 検収売上伝票毎消費税内税
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_TAX_UCHI"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_URIAGE_TAX_UCHI = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_TAX_UCHI"].ToString());
                }

                // 検収売上明細毎消費税外税合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_TAX_SOTO_TOTAL"].ToString());
                }

                // 検収売上明細毎消費税内税合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_TAX_UCHI_TOTAL"].ToString());
                }

                // 検収品名別売上金額合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_URIAGE_KINGAKU_TOTAL"].ToString());
                }

                // 検収品名別売上消費税外税合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_URIAGE_TAX_SOTO_TOTAL"].ToString());
                }

                // 検収品名別売上消費税内税合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_URIAGE_TAX_UCHI_TOTAL"].ToString());
                }

                // 検収支払消費税率
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_SHOUHIZEI_RATE"].ToString());
                }

                // 検収支払金額合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_AMOUNT_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_SHIHARAI_AMOUNT_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_AMOUNT_TOTAL"].ToString());
                }

                // 検収支払伝票毎消費税外税
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_TAX_SOTO"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_SHIHARAI_TAX_SOTO = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_TAX_SOTO"].ToString());
                }

                // 検収支払伝票毎消費税内税
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_TAX_UCHI"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_SHIHARAI_TAX_UCHI = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_TAX_UCHI"].ToString());
                }

                // 検収支払明細毎消費税外税合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                }

                // 検収支払明細毎消費税内税合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                }

                // 検収品名別支払金額合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_SHIHARAI_KINGAKU_TOTAL"].ToString());
                }

                // 検収品名別支払消費税外税合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_SHIHARAI_TAX_SOTO_TOTAL"].ToString());
                }

                // 検収品名別支払消費税内税合計
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL = SqlDecimal.Parse(retShukkaEntityDT.Rows[0]["KENSHU_HINMEI_SHIHARAI_TAX_UCHI_TOTAL"].ToString());
                }

                // 検収売上日付
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_DATE"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_URIAGE_DATE = SqlDateTime.Parse(retShukkaEntityDT.Rows[0]["KENSHU_URIAGE_DATE"].ToString());
                }

                // 検収支払日付
                if (!string.IsNullOrEmpty(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_DATE"].ToString()))
                {
                    this.insShukkaEntryEntity.KENSHU_SHIHARAI_DATE = SqlDateTime.Parse(retShukkaEntityDT.Rows[0]["KENSHU_SHIHARAI_DATE"].ToString());
                }

                // 削除フラグ
                this.insShukkaEntryEntity.DELETE_FLG = false;
                //インサート受付入力Entityリストに追加
                insShukkaEntryEntityList.Add(insShukkaEntryEntity);
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

        /// <summary>
        ///売上／支払Entityリストを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateUrshEntryEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                DTOClass dto = new DTOClass();
                dto.SystemID = long.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                dto.SEQ = int.Parse(dt.Rows[0]["SEQ"].ToString());
                this.dto.SYSTEM_ID = dt.Rows[0]["SYSTEM_ID"].ToString();
                this.dto.SEQ = dt.Rows[0]["SEQ"].ToString();
                //売上／支払Entityテブル
                DataTable retUrshEntityDT = this.daoUrshEntry.GetDataToDataTable(dto);
                //売上／支払明細テブル
                DataTable retUrshTankaSabunDetailDT = this.urshTankaSabunDetailDao.GetDataToDataTable(this.dto);

                //売上金額合計
                decimal uriageKingakuTotal = 0;
                //売上外税合計（伝票毎）
                decimal uriageTaxSoto = 0;
                //売上内税合計（伝票毎）
                decimal uriageTaxuchi = 0;
                //売上外税合計（明細毎）
                decimal uriageTaxSotoTotal = 0;
                //売上内税合計（明細毎）
                decimal uriageTaxUchiTotal = 0;
                //品名売上金額合計
                decimal hinmeiUriageKingakuTotal = 0;
                //品名売上外税合計
                decimal hinmeiUriageTaxSotoTotal = 0;
                //品名売上内税合計
                decimal hinmeiUriageTaxUchiTotal = 0;
                //支払金額合計
                decimal shiharaiKingakuTotal = 0;
                //支払外税合計（伝票毎）
                decimal shiharaiTaxSoto = 0;
                //支払内税合計（伝票毎）
                decimal shihraiTaxUchi = 0;
                //支払外税合計（明細毎）
                decimal shiharaiTaxSotoTotal = 0;
                //支払内税合計（明細毎）
                decimal shiharaiTaxUchiTotal = 0;
                //品名支払金額合計
                decimal hinmeiShiharaiKingauTotal = 0;
                //品名支払外税合計
                decimal hinmeiShiharaiTaxSotoTotal = 0;
                //品名支払内税合計
                decimal hinmeiShiharaiTaxUchiTotal = 0;
                // 取引先_請求情報マスタ
                int seikyuuZeikeisanKbn = 0;
                int seikyuuZeiKbnCd = 0;
                int seikyuuKingakuHasuuCd = 0;

                // 取引先_支払情報マスタ
                int shiharaiZeikeisanKbn = 0;
                int shiharaiZeiKbnCd = 0;
                int shiharaiKingakuHasuuCd = 0;

                // 取引先_請求情報マスタ
                var torihikisakiSeikyuu = this.GetTorihikisakiSeikyuu(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                if (torihikisakiSeikyuu != null)
                {
                    // 税計算区分CD　
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.ZEI_KEISAN_KBN_CD), out seikyuuZeikeisanKbn);
                    // 税区分CD　
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.ZEI_KBN_CD), out seikyuuZeiKbnCd);
                    // 消費税端数CD　
                    int.TryParse(Convert.ToString(torihikisakiSeikyuu.TAX_HASUU_CD), out seikyuuKingakuHasuuCd);
                }

                // 取引先_支払情報マスタ
                var torihikisakiShiharai = this.GetTorihikisakiShiharai(dt.Rows[0]["TORIHIKISAKI_CD"].ToString());
                if (torihikisakiShiharai != null)
                {
                    // 税計算区分CD　
                    int.TryParse(Convert.ToString(torihikisakiShiharai.ZEI_KEISAN_KBN_CD), out shiharaiZeikeisanKbn);
                    // 税区分CD　
                    int.TryParse(Convert.ToString(torihikisakiShiharai.ZEI_KBN_CD), out shiharaiZeiKbnCd);
                    // 消費税端数CD　
                    int.TryParse(Convert.ToString(torihikisakiShiharai.TAX_HASUU_CD), out shiharaiKingakuHasuuCd);
                }

                //画面に表示されるデータ
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // 明細．金額
                    decimal meisaiKingaku = 0;
                    decimal.TryParse(Convert.ToString(dt.Rows[i]["SHIN_KINGAKU"]), out meisaiKingaku);
                    //売上消費税率
                    decimal uriageShouhizeiRate = 0;
                    if (retUrshEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                        uriageShouhizeiRate = decimal.Parse(retUrshEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString());
                    //支払消費税率
                    decimal shiharaiShouhizeiRate = 0;
                    if (retUrshEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                        shiharaiShouhizeiRate = decimal.Parse(retUrshEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                    //品名税区分空白の場合
                    if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null || string.IsNullOrEmpty(dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        //売上
                        if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            //売上金額合計
                            uriageKingakuTotal += meisaiKingaku;
                            //売上税計算区分は明細毎の場合
                            if (retUrshEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retUrshEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //売上外税合計（明細毎）
                                    uriageTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * uriageShouhizeiRate, seikyuuKingakuHasuuCd);

                                }
                                //内税
                                else if (retUrshEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //売上内税合計（明細毎）
                                    uriageTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + uriageShouhizeiRate));
                                    uriageTaxUchiTotal = CommonCalc.FractionCalc(uriageTaxUchiTotal, seikyuuKingakuHasuuCd);
                                }
                            }
                        }
                        //支払
                        else if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            //支払金額合計
                            shiharaiKingakuTotal += meisaiKingaku;
                            //支払税計算区分は明細毎の場合
                            if (retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //支払外税合計（明細毎）
                                    shiharaiTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * shiharaiShouhizeiRate, shiharaiKingakuHasuuCd);
                                }
                                //内税
                                else if (retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //支払内税合計（明細毎）
                                    shiharaiTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + shiharaiShouhizeiRate));
                                    shiharaiTaxUchiTotal = CommonCalc.FractionCalc(shiharaiTaxUchiTotal, shiharaiKingakuHasuuCd);
                                }
                            }
                        }
                    }
                    else
                    {
                        //売上
                        if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            hinmeiUriageKingakuTotal += decimal.Parse(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                            //外税
                            if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiUriageTaxSotoTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString());
                            }
                            //内税
                            else if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                hinmeiUriageTaxUchiTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString());
                            }
                        }
                        //支払
                        else if ((dt.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            hinmeiShiharaiKingauTotal += decimal.Parse(dt.Rows[i]["SHIN_KINGAKU"].ToString());
                            //外税
                            if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiShiharaiTaxSotoTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_SOTO"].ToString());
                            }
                            //内税
                            else if (dt.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                hinmeiShiharaiTaxUchiTotal += decimal.Parse(dt.Rows[i]["HINMEI_TAX_UCHI"].ToString());
                            }
                        }
                    }
                }

                //画面に表示されない、DBに存在しているデータ
                for (int i = 0; i < retUrshTankaSabunDetailDT.Rows.Count; i++)
                {

                    // 明細．金額
                    decimal meisaiKingaku = 0;
                    if (retUrshTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null ||
                       string.IsNullOrEmpty(retUrshTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        decimal.TryParse(Convert.ToString(retUrshTankaSabunDetailDT.Rows[i]["KINGAKU"]), out meisaiKingaku);
                    }
                    else
                    {
                        decimal.TryParse(Convert.ToString(retUrshTankaSabunDetailDT.Rows[i]["HINMEI_KINGAKU"]), out meisaiKingaku);
                    }
                    decimal taxRate = 0;
                    int zeikeisanKbn = 0;
                    int zeiKbnCd = 0;
                    int kingakuHasuuCd = 0;
                    //売上消費税率
                    decimal uriageShouhizeiRate = 0;
                    if (retUrshEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                        uriageShouhizeiRate = decimal.Parse(retUrshEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString());
                    //支払消費税率
                    decimal shiharaiShouhizeiRate = 0;
                    if (retUrshEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"] != null &&
                        !string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                        shiharaiShouhizeiRate = decimal.Parse(retUrshEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString());

                    // 伝票区分により、取引先情報で税計算区分CD,税区分CD,消費税端数CDを設定
                    switch (Convert.ToString(retUrshTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"]))
                    {
                        case "1":
                            //売上消費税率
                            if (retUrshTankaSabunDetailDT.Rows[i]["URIAGE_SHOUHIZEI_RATE"] != null &&
                                !string.IsNullOrEmpty(retUrshTankaSabunDetailDT.Rows[i]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                                taxRate = decimal.Parse(Convert.ToString(retUrshTankaSabunDetailDT.Rows[i]["URIAGE_SHOUHIZEI_RATE"]));
                            break;
                        case "2":
                            //支払消費税率
                            if (retUrshTankaSabunDetailDT.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"] != null &&
                               !string.IsNullOrEmpty(retUrshTankaSabunDetailDT.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                                taxRate = decimal.Parse(Convert.ToString(retUrshTankaSabunDetailDT.Rows[i]["SHIHARAI_SHOUHIZEI_RATE"]));
                            break;
                        default:
                            break;
                    }

                    decimal TaxSoto = 0;

                    //品名税区分空白の場合
                    if (retUrshTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString() == null ||
                        string.IsNullOrEmpty(retUrshTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString()))
                    {
                        //売上
                        if ((retUrshTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            //売上金額合計
                            uriageKingakuTotal += meisaiKingaku;
                            //売上税計算区分は明細毎の場合
                            if (retUrshEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retUrshEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //売上外税合計（明細毎）
                                    // uriageTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * uriageShouhizeiRate, seikyuuKingakuHasuuCd);
                                    decimal.TryParse(Convert.ToString(retUrshTankaSabunDetailDT.Rows[i]["TAX_SOTO"]), out TaxSoto);
                                    uriageTaxSotoTotal += TaxSoto;
                                }
                                //内税
                                else if (retUrshEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //売上内税合計（明細毎）
                                    uriageTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + uriageShouhizeiRate));
                                    uriageTaxUchiTotal = CommonCalc.FractionCalc(uriageTaxUchiTotal, seikyuuKingakuHasuuCd);
                                }
                            }
                        }
                        //支払
                        else if ((retUrshTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            //支払金額合計
                            shiharaiKingakuTotal += meisaiKingaku;
                            //支払税計算区分は明細毎の場合
                            if (retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("3"))
                            {
                                //外税
                                if (retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("1"))
                                {
                                    //支払外税合計（明細毎）
                                    //shiharaiTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * shiharaiShouhizeiRate, shiharaiKingakuHasuuCd);
                                    decimal.TryParse(Convert.ToString(retUrshTankaSabunDetailDT.Rows[i]["TAX_SOTO"]), out TaxSoto);
                                    shiharaiTaxSotoTotal += TaxSoto;
                                }
                                //内税
                                else if (retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("2"))
                                {
                                    //支払内税合計（明細毎）
                                    shiharaiTaxUchiTotal += meisaiKingaku - (meisaiKingaku / (1 + shiharaiShouhizeiRate));
                                    shiharaiTaxUchiTotal = CommonCalc.FractionCalc(shiharaiTaxUchiTotal, shiharaiKingakuHasuuCd);
                                }
                            }
                        }
                    }
                    else
                    {
                        //売上
                        if ((retUrshTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("1")))
                        {
                            hinmeiUriageKingakuTotal += meisaiKingaku;
                            //外税
                            if (retUrshTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiUriageTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * taxRate, kingakuHasuuCd);

                            }
                            //内税
                            else if (retUrshTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                decimal hinmei_tax_uchi = meisaiKingaku - (meisaiKingaku / (taxRate + 1));
                                hinmei_tax_uchi = CommonCalc.FractionCalc(hinmei_tax_uchi, kingakuHasuuCd);
                                hinmeiUriageTaxUchiTotal += hinmei_tax_uchi;
                            }
                        }
                        //支払
                        else if ((retUrshTankaSabunDetailDT.Rows[i]["DENPYOU_KBN_CD"].ToString().Equals("2")))
                        {
                            hinmeiShiharaiKingauTotal += meisaiKingaku;
                            //外税
                            if (retUrshTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("1"))
                            {
                                hinmeiShiharaiTaxSotoTotal += CommonCalc.FractionCalc(meisaiKingaku * taxRate, kingakuHasuuCd);
                            }
                            //内税
                            else if (retUrshTankaSabunDetailDT.Rows[i]["HINMEI_ZEI_KBN_CD"].ToString().Equals("2"))
                            {
                                decimal hinmei_tax_uchi = meisaiKingaku - (meisaiKingaku / (taxRate + 1));
                                hinmei_tax_uchi = CommonCalc.FractionCalc(hinmei_tax_uchi, kingakuHasuuCd);
                                hinmeiShiharaiTaxUchiTotal += hinmei_tax_uchi;
                            }
                        }
                    }
                }

                //売上税計算区分は伝票毎の場合
                if (retUrshEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("1") ||
                    retUrshEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString().Equals("2"))
                {
                    //外税
                    if (retUrshEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("1"))
                    {
                        uriageTaxSoto = CommonCalc.FractionCalc(uriageKingakuTotal * decimal.Parse(retUrshEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()), seikyuuKingakuHasuuCd);
                    }
                    //内税
                    else if (retUrshEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString().Equals("2"))
                    {
                        uriageTaxuchi = CommonCalc.FractionCalc(uriageKingakuTotal * decimal.Parse(retUrshEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()), seikyuuKingakuHasuuCd);
                    }
                }

                //支払税計算区分は伝票毎の場合
                if (retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("1") ||
                    retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString().Equals("2"))
                {
                    //外税
                    if (retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("1"))
                    {
                        shiharaiTaxSoto = CommonCalc.FractionCalc(shiharaiKingakuTotal * decimal.Parse(retUrshEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()), shiharaiKingakuHasuuCd);
                    }
                    //内税
                    else if (retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString().Equals("2"))
                    {
                        shihraiTaxUchi = CommonCalc.FractionCalc(shiharaiKingakuTotal * decimal.Parse(retUrshEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()), shiharaiKingakuHasuuCd);
                    }
                }

                //売上／支払入力Entity
                this.insUrshEntryEntity = new T_UR_SH_ENTRY();
                // SYSTEM_ID
                if (!string.IsNullOrEmpty(dt.Rows[0]["SYSTEM_ID"].ToString()))
                    this.insUrshEntryEntity.SYSTEM_ID = SqlInt64.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                // 枝番
                if (!string.IsNullOrEmpty(dt.Rows[0]["SEQ"].ToString()))
                    this.insUrshEntryEntity.SEQ = SqlInt32.Parse(dt.Rows[0]["SEQ"].ToString()) + 1;
                //拠点CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["KYOTEN_CD"].ToString()))
                    this.insUrshEntryEntity.KYOTEN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["KYOTEN_CD"].ToString());
                //受入番号
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["UR_SH_NUMBER"].ToString()))
                    this.insUrshEntryEntity.UR_SH_NUMBER = SqlInt64.Parse(retUrshEntityDT.Rows[0]["UR_SH_NUMBER"].ToString());
                //日連番
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["DATE_NUMBER"].ToString()))
                    this.insUrshEntryEntity.DATE_NUMBER = SqlInt32.Parse(retUrshEntityDT.Rows[0]["DATE_NUMBER"].ToString());
                //年連番
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["YEAR_NUMBER"].ToString()))
                    this.insUrshEntryEntity.YEAR_NUMBER = SqlInt32.Parse(retUrshEntityDT.Rows[0]["YEAR_NUMBER"].ToString());
                //確定区分
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["KAKUTEI_KBN"].ToString()))
                    this.insUrshEntryEntity.KAKUTEI_KBN = SqlInt16.Parse(retUrshEntityDT.Rows[0]["KAKUTEI_KBN"].ToString());
                //伝票日付
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["DENPYOU_DATE"].ToString()))
                    this.insUrshEntryEntity.DENPYOU_DATE = SqlDateTime.Parse(retUrshEntityDT.Rows[0]["DENPYOU_DATE"].ToString());
                //売上日付
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["URIAGE_DATE"].ToString()))
                    this.insUrshEntryEntity.URIAGE_DATE = SqlDateTime.Parse(retUrshEntityDT.Rows[0]["URIAGE_DATE"].ToString());
                //支払日付
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHIHARAI_DATE"].ToString()))
                    this.insUrshEntryEntity.SHIHARAI_DATE = SqlDateTime.Parse(retUrshEntityDT.Rows[0]["SHIHARAI_DATE"].ToString());
                //取引先CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["TORIHIKISAKI_CD"].ToString()))
                    this.insUrshEntryEntity.TORIHIKISAKI_CD = retUrshEntityDT.Rows[0]["TORIHIKISAKI_CD"].ToString();
                //取引先名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["TORIHIKISAKI_NAME"].ToString()))
                    this.insUrshEntryEntity.TORIHIKISAKI_NAME = retUrshEntityDT.Rows[0]["TORIHIKISAKI_NAME"].ToString();
                //業者CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["GYOUSHA_CD"].ToString()))
                    this.insUrshEntryEntity.GYOUSHA_CD = retUrshEntityDT.Rows[0]["GYOUSHA_CD"].ToString();
                //業者名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["GYOUSHA_NAME"].ToString()))
                    this.insUrshEntryEntity.GYOUSHA_NAME = retUrshEntityDT.Rows[0]["GYOUSHA_NAME"].ToString();
                //現場CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["GENBA_CD"].ToString()))
                    this.insUrshEntryEntity.GENBA_CD = retUrshEntityDT.Rows[0]["GENBA_CD"].ToString();
                //現場名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["GENBA_NAME"].ToString()))
                    this.insUrshEntryEntity.GENBA_NAME = retUrshEntityDT.Rows[0]["GENBA_NAME"].ToString();
                //荷積業者CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString()))
                    this.insUrshEntryEntity.NIZUMI_GYOUSHA_CD = retUrshEntityDT.Rows[0]["NIZUMI_GYOUSHA_CD"].ToString();
                //荷積業者名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NIZUMI_GYOUSHA_NAME"].ToString()))
                    this.insUrshEntryEntity.NIZUMI_GYOUSHA_NAME = retUrshEntityDT.Rows[0]["NIZUMI_GYOUSHA_NAME"].ToString();
                //荷積現場CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NIZUMI_GENBA_CD"].ToString()))
                    this.insUrshEntryEntity.NIZUMI_GENBA_CD = retUrshEntityDT.Rows[0]["NIZUMI_GENBA_CD"].ToString();
                //荷積現場名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NIZUMI_GENBA_NAME"].ToString()))
                    this.insUrshEntryEntity.NIZUMI_GENBA_NAME = retUrshEntityDT.Rows[0]["NIZUMI_GENBA_NAME"].ToString();
                //荷降業者CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString()))
                    this.insUrshEntryEntity.NIOROSHI_GYOUSHA_CD = retUrshEntityDT.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                //荷降業者名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NIOROSHI_GYOUSHA_NAME"].ToString()))
                    this.insUrshEntryEntity.NIOROSHI_GYOUSHA_NAME = retUrshEntityDT.Rows[0]["NIOROSHI_GYOUSHA_NAME"].ToString();
                //荷降現場CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NIOROSHI_GENBA_CD"].ToString()))
                    this.insUrshEntryEntity.NIOROSHI_GENBA_CD = retUrshEntityDT.Rows[0]["NIOROSHI_GENBA_CD"].ToString();
                //荷降現場名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NIOROSHI_GENBA_NAME"].ToString()))
                    this.insUrshEntryEntity.NIOROSHI_GENBA_NAME = retUrshEntityDT.Rows[0]["NIOROSHI_GENBA_NAME"].ToString();
                //営業担当者CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString()))
                    this.insUrshEntryEntity.EIGYOU_TANTOUSHA_CD = retUrshEntityDT.Rows[0]["EIGYOU_TANTOUSHA_CD"].ToString();
                //営業担当者名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["EIGYOU_TANTOUSHA_NAME"].ToString()))
                    this.insUrshEntryEntity.EIGYOU_TANTOUSHA_NAME = retUrshEntityDT.Rows[0]["EIGYOU_TANTOUSHA_NAME"].ToString();
                //入力担当者CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_CD"].ToString()))
                    this.insUrshEntryEntity.NYUURYOKU_TANTOUSHA_CD = retUrshEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_CD"].ToString();
                //入力担当者名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_NAME"].ToString()))
                    this.insUrshEntryEntity.NYUURYOKU_TANTOUSHA_NAME = retUrshEntityDT.Rows[0]["NYUURYOKU_TANTOUSHA_NAME"].ToString();
                //車輌CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHARYOU_CD"].ToString()))
                    this.insUrshEntryEntity.SHARYOU_CD = retUrshEntityDT.Rows[0]["SHARYOU_CD"].ToString();
                //車輌名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHARYOU_NAME"].ToString()))
                    this.insUrshEntryEntity.SHARYOU_NAME = retUrshEntityDT.Rows[0]["SHARYOU_NAME"].ToString();
                //車種CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHASHU_CD"].ToString()))
                    this.insUrshEntryEntity.SHASHU_CD = retUrshEntityDT.Rows[0]["SHASHU_CD"].ToString();
                //車種名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHASHU_NAME"].ToString()))
                    this.insUrshEntryEntity.SHASHU_NAME = retUrshEntityDT.Rows[0]["SHASHU_NAME"].ToString();
                //運搬業者CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["UNPAN_GYOUSHA_CD"].ToString()))
                    this.insUrshEntryEntity.UNPAN_GYOUSHA_CD = retUrshEntityDT.Rows[0]["UNPAN_GYOUSHA_CD"].ToString();
                //運搬業者名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString()))
                    this.insUrshEntryEntity.UNPAN_GYOUSHA_NAME = retUrshEntityDT.Rows[0]["UNPAN_GYOUSHA_NAME"].ToString();
                //運転者CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["UNTENSHA_CD"].ToString()))
                    this.insUrshEntryEntity.UNTENSHA_CD = retUrshEntityDT.Rows[0]["UNTENSHA_CD"].ToString();
                //運転者名
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["UNTENSHA_NAME"].ToString()))
                    this.insUrshEntryEntity.UNTENSHA_NAME = retUrshEntityDT.Rows[0]["UNTENSHA_NAME"].ToString();
                //人数
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["NINZUU_CNT"].ToString()))
                    this.insUrshEntryEntity.NINZUU_CNT = SqlInt16.Parse(retUrshEntityDT.Rows[0]["NINZUU_CNT"].ToString());
                //形態区分CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["KEITAI_KBN_CD"].ToString()))
                    this.insUrshEntryEntity.KEITAI_KBN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["KEITAI_KBN_CD"].ToString());
                //コンテナ操作CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["CONTENA_SOUSA_CD"].ToString()))
                    this.insUrshEntryEntity.CONTENA_SOUSA_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["CONTENA_SOUSA_CD"].ToString());
                //マニフェスト種類CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["MANIFEST_SHURUI_CD"].ToString()))
                    this.insUrshEntryEntity.MANIFEST_SHURUI_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["MANIFEST_SHURUI_CD"].ToString());
                //マニフェスト手配CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["MANIFEST_TEHAI_CD"].ToString()))
                    this.insUrshEntryEntity.MANIFEST_TEHAI_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["MANIFEST_TEHAI_CD"].ToString());
                //伝票備考
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["DENPYOU_BIKOU"].ToString()))
                    this.insUrshEntryEntity.DENPYOU_BIKOU = retUrshEntityDT.Rows[0]["DENPYOU_BIKOU"].ToString();
                //受付番号
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["UKETSUKE_NUMBER"].ToString()))
                    this.insUrshEntryEntity.UKETSUKE_NUMBER = SqlInt64.Parse(retUrshEntityDT.Rows[0]["UKETSUKE_NUMBER"].ToString());
                //領収書番号
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["RECEIPT_NUMBER"].ToString()))
                    this.insUrshEntryEntity.RECEIPT_NUMBER = SqlInt32.Parse(retUrshEntityDT.Rows[0]["RECEIPT_NUMBER"].ToString());
                //売上消費税率
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString()))
                    this.insUrshEntryEntity.URIAGE_SHOUHIZEI_RATE = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["URIAGE_SHOUHIZEI_RATE"].ToString());
                //売上金額合計
                this.insUrshEntryEntity.URIAGE_AMOUNT_TOTAL = uriageKingakuTotal;
                //売上伝票毎消費税外税
                this.insUrshEntryEntity.URIAGE_TAX_SOTO = uriageTaxSoto;
                //売上伝票毎消費税内税
                this.insUrshEntryEntity.URIAGE_TAX_UCHI = uriageTaxuchi;
                //売上明細毎消費税外税合計
                this.insUrshEntryEntity.URIAGE_TAX_SOTO_TOTAL = uriageTaxSotoTotal;
                //売上明細毎消費税内税合計
                this.insUrshEntryEntity.URIAGE_TAX_UCHI_TOTAL = uriageTaxUchiTotal;
                //品名別売上金額合計
                this.insUrshEntryEntity.HINMEI_URIAGE_KINGAKU_TOTAL = hinmeiUriageKingakuTotal;
                //品名別売上消費税外税合計
                this.insUrshEntryEntity.HINMEI_URIAGE_TAX_SOTO_TOTAL = hinmeiUriageTaxSotoTotal;
                //品名別売上消費税内税合計
                this.insUrshEntryEntity.HINMEI_URIAGE_TAX_UCHI_TOTAL = hinmeiUriageTaxUchiTotal;
                //支払消費税率
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString()))
                    this.insUrshEntryEntity.SHIHARAI_SHOUHIZEI_RATE = SqlDecimal.Parse(retUrshEntityDT.Rows[0]["SHIHARAI_SHOUHIZEI_RATE"].ToString());
                //支払金額合計
                this.insUrshEntryEntity.SHIHARAI_AMOUNT_TOTAL = shiharaiKingakuTotal;
                //支払伝票毎消費税外税
                this.insUrshEntryEntity.SHIHARAI_TAX_SOTO = shiharaiTaxSoto;
                //支払伝票毎消費税内税
                this.insUrshEntryEntity.SHIHARAI_TAX_UCHI = shihraiTaxUchi;
                //支払明細毎消費税外税合計
                this.insUrshEntryEntity.SHIHARAI_TAX_SOTO_TOTAL = shiharaiTaxSotoTotal;
                //支払明細毎消費税内税合計
                this.insUrshEntryEntity.SHIHARAI_TAX_UCHI_TOTAL = shiharaiTaxUchiTotal;
                //品名別支払金額合計
                this.insUrshEntryEntity.HINMEI_SHIHARAI_KINGAKU_TOTAL = hinmeiShiharaiKingauTotal;
                //品名別支払消費税外税合計
                this.insUrshEntryEntity.HINMEI_SHIHARAI_TAX_SOTO_TOTAL = hinmeiShiharaiTaxSotoTotal;
                //品名別支払消費税内税合計
                this.insUrshEntryEntity.HINMEI_SHIHARAI_TAX_UCHI_TOTAL = hinmeiShiharaiTaxUchiTotal;
                //売上税計算区分CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString()))
                    this.insUrshEntryEntity.URIAGE_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["URIAGE_ZEI_KEISAN_KBN_CD"].ToString());
                //売上税区分CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString()))
                    this.insUrshEntryEntity.URIAGE_ZEI_KBN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["URIAGE_ZEI_KBN_CD"].ToString());
                //売上取引区分CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["URIAGE_TORIHIKI_KBN_CD"].ToString()))
                    this.insUrshEntryEntity.URIAGE_TORIHIKI_KBN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["URIAGE_TORIHIKI_KBN_CD"].ToString());
                //支払税計算区分CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString()))
                    this.insUrshEntryEntity.SHIHARAI_ZEI_KEISAN_KBN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KEISAN_KBN_CD"].ToString());
                //支払税区分CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString()))
                    this.insUrshEntryEntity.SHIHARAI_ZEI_KBN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["SHIHARAI_ZEI_KBN_CD"].ToString());
                //支払取引区分CD
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["SHIHARAI_TORIHIKI_KBN_CD"].ToString()))
                    this.insUrshEntryEntity.SHIHARAI_TORIHIKI_KBN_CD = SqlInt16.Parse(retUrshEntityDT.Rows[0]["SHIHARAI_TORIHIKI_KBN_CD"].ToString());
                //月極一括作成区分
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["TSUKI_CREATE_KBN"].ToString()))
                    this.insUrshEntryEntity.TSUKI_CREATE_KBN = SqlBoolean.Parse(retUrshEntityDT.Rows[0]["TSUKI_CREATE_KBN"].ToString());
                // 作成と更新情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.T_UR_SH_ENTRY>(insUrshEntryEntity);
                dbLogic.SetSystemProperty(insUrshEntryEntity, false);
                // 作成日
                if (!string.IsNullOrEmpty(retUrshEntityDT.Rows[0]["CREATE_DATE"].ToString()))
                    this.insUrshEntryEntity.CREATE_DATE = SqlDateTime.Parse(retUrshEntityDT.Rows[0]["CREATE_DATE"].ToString());
                // 削除フラグ
                this.insUrshEntryEntity.DELETE_FLG = false;
                //インサート受付入力Entityリストに追加
                insUrshEntryEntityList.Add(insUrshEntryEntity);
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

        /// <summary>
        ///（受入、出荷、売上/支払）論理削除Entityを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateDelEntryEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                //空白行の場合
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }
                else
                {
                    //受入
                    DataTable dtUkeire = dt.Clone();
                    //出荷
                    DataTable dtShuka = dt.Clone();
                    //売上/支払
                    DataTable dtUrshs = dt.Clone();
                    //伝票種類によって、データを分ける(受入、出荷、売上/支払)。
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["DENPYOU_SHURUI"].ToString().Equals("受入"))
                        {
                            dtUkeire.Rows.Add(dt.Rows[i].ItemArray);
                        }
                        else if (dt.Rows[i]["DENPYOU_SHURUI"].ToString().Equals("出荷"))
                        {
                            dtShuka.Rows.Add(dt.Rows[i].ItemArray);
                        }
                        else if (dt.Rows[i]["DENPYOU_SHURUI"].ToString().Equals("売上/支払"))
                        {
                            dtUrshs.Rows.Add(dt.Rows[i].ItemArray);
                        }
                    }

                    //受入論理削除Entityリストを作成
                    if (dtUkeire.Rows.Count > 0)
                        CreateDelUkeireEntryEntity(dtUkeire);
                    //出荷論理削除Entityリストを作成
                    if (dtShuka.Rows.Count > 0)
                        CreateDelShukkaEntryEntity(dtShuka);
                    //売上／支払論理削除Entityリストを作成
                    if (dtUrshs.Rows.Count > 0)
                        CreateDelUrshEntryEntity(dtUrshs);
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

        /// <summary>
        ///受入論理削除Entityリストを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateDelUkeireEntryEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                //空白行の場合
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }
                this.delUkeireEntryEntity = new T_UKEIRE_ENTRY();
                DTOClass dto = new DTOClass();
                dto.SystemID = long.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                dto.SEQ = int.Parse(dt.Rows[0]["SEQ"].ToString());
                //受入Entityテブル
                DataTable retUkeireEntityDT = this.daoUkeireEntry.GetDataToDataTable(dto);

                // SYSTEM_ID(元データのシステムID)
                this.delUkeireEntryEntity.SYSTEM_ID = SqlInt64.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());

                // SEQ(元データのSEQ)
                this.delUkeireEntryEntity.SEQ = SqlInt32.Parse(dt.Rows[0]["SEQ"].ToString());

                // 作成と更新情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.T_UKEIRE_ENTRY>(this.delUkeireEntryEntity);
                dbLogic.SetSystemProperty(this.delUkeireEntryEntity, false);

                // 更新日
                this.delUkeireEntryEntity.UPDATE_DATE = SqlDateTime.Parse(retUkeireEntityDT.Rows[0]["UPDATE_DATE"].ToString());

                // 更新者
                this.delUkeireEntryEntity.UPDATE_USER = retUkeireEntityDT.Rows[0]["UPDATE_USER"].ToString();

                // 更新PC
                this.delUkeireEntryEntity.UPDATE_PC = retUkeireEntityDT.Rows[0]["UPDATE_PC"].ToString();

                // 削除フラグ
                this.delUkeireEntryEntity.DELETE_FLG = true;

                // TIME_STAMP
                this.delUkeireEntryEntity.TIME_STAMP = (byte[])retUkeireEntityDT.Rows[0]["TIME_STAMP"];

                //削除受付入力Entityリストに追加
                this.delUkeireEntryEntityList.Add(this.delUkeireEntryEntity);

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

        /// <summary>
        ///出荷論理削除Entityリストを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateDelShukkaEntryEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                //空白行の場合
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }
                this.delShukkaEntryEntity = new T_SHUKKA_ENTRY();
                DTOClass dto = new DTOClass();
                dto.SystemID = long.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                dto.SEQ = int.Parse(dt.Rows[0]["SEQ"].ToString());
                //出荷Entityテブル
                DataTable retShukkaEntityDT = this.daoShukkaEntry.GetDataToDataTable(dto);

                // SYSTEM_ID(元データのシステムID)
                this.delShukkaEntryEntity.SYSTEM_ID = SqlInt64.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());

                // SEQ(元データのSEQ)
                this.delShukkaEntryEntity.SEQ = SqlInt32.Parse(dt.Rows[0]["SEQ"].ToString());

                // 作成と更新情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.T_SHUKKA_ENTRY>(this.delShukkaEntryEntity);
                dbLogic.SetSystemProperty(this.delShukkaEntryEntity, false);

                // 更新日
                this.delShukkaEntryEntity.UPDATE_DATE = SqlDateTime.Parse(retShukkaEntityDT.Rows[0]["UPDATE_DATE"].ToString());

                // 更新者
                this.delShukkaEntryEntity.UPDATE_USER = retShukkaEntityDT.Rows[0]["UPDATE_USER"].ToString();

                // 更新PC
                this.delShukkaEntryEntity.UPDATE_PC = retShukkaEntityDT.Rows[0]["UPDATE_PC"].ToString();

                // 削除フラグ
                this.delShukkaEntryEntity.DELETE_FLG = true;

                // TIME_STAMP
                this.delShukkaEntryEntity.TIME_STAMP = (byte[])retShukkaEntityDT.Rows[0]["TIME_STAMP"];

                //削除受付入力Entityリストに追加
                this.delShukkaEntryEntityList.Add(this.delShukkaEntryEntity);

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

        /// <summary>
        ///売上／支払論理削除Entityリストを作成
        /// </summary>
        /// <param name="dt"></param>
        private void CreateDelUrshEntryEntity(DataTable dt)
        {
            try
            {
                LogUtility.DebugMethodStart(dt);
                //空白行の場合
                if (dt == null || dt.Rows.Count <= 0)
                {
                    return;
                }
                this.delUrshEntryEntity = new T_UR_SH_ENTRY();
                DTOClass dto = new DTOClass();
                dto.SystemID = long.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());
                dto.SEQ = int.Parse(dt.Rows[0]["SEQ"].ToString());
                //売上／支払Entityテブル
                DataTable retUrshEntityDT = this.daoUrshEntry.GetDataToDataTable(dto);

                // SYSTEM_ID(元データのシステムID)
                this.delUrshEntryEntity.SYSTEM_ID = SqlInt64.Parse(dt.Rows[0]["SYSTEM_ID"].ToString());

                // SEQ(元データのSEQ)
                this.delUrshEntryEntity.SEQ = SqlInt32.Parse(dt.Rows[0]["SEQ"].ToString());

                // 作成と更新情報設定
                var dbLogic = new DataBinderLogic<r_framework.Entity.T_UR_SH_ENTRY>(this.delUrshEntryEntity);
                dbLogic.SetSystemProperty(this.delUrshEntryEntity, false);

                // 更新日
                this.delUrshEntryEntity.UPDATE_DATE = SqlDateTime.Parse(retUrshEntityDT.Rows[0]["UPDATE_DATE"].ToString());

                // 更新者
                this.delUrshEntryEntity.UPDATE_USER = retUrshEntityDT.Rows[0]["UPDATE_USER"].ToString();

                // 更新PC
                this.delUrshEntryEntity.UPDATE_PC = retUrshEntityDT.Rows[0]["UPDATE_PC"].ToString();

                // 削除フラグ
                this.delUrshEntryEntity.DELETE_FLG = true;

                // TIME_STAMP
                this.delUrshEntryEntity.TIME_STAMP = (byte[])retUrshEntityDT.Rows[0]["TIME_STAMP"];

                //削除受付入力Entityリストに追加
                this.delUrshEntryEntityList.Add(this.delUrshEntryEntity);

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

        /// <summary>
        /// MultRowタイプをDataTableタイプにチェンジする。
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="eRow"></param>
        private void AddMultRowToDataTable(DataTable dt, Row row)
        {
            try
            {
                //LogUtility.DebugMethodStart();
                DataRow rowc = dt.NewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    rowc[row.Cells[i].Name] = row.Cells[i].Value;
                }
                dt.Rows.Add(rowc);
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

        #region Null値を指定値に変換
        /// <summary>
        /// Null値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>string</returns>
        private string ChgNullToValue(string obj, string value)
        {
            try
            {
                //LogUtility.DebugMethodStart(obj, value);
                if (string.IsNullOrEmpty(obj))
                {
                    return value;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd(obj);
            }
        }
        #endregion

        #region DBNull値を指定値に変換
        /// <summary>
        /// DBNull値を指定値に変換
        /// </summary>
        /// <param name="obj">チェック対象</param>
        /// <param name="value">設定値</param>
        /// <returns>object</returns>
        public object ChgDBNullToValue(object obj, object value)
        {
            try
            {
                //LogUtility.DebugMethodStart(obj, value);
                if (obj is DBNull)
                {
                    return value;
                }
                else if (obj.GetType().Namespace.Equals("System.Data.SqlTypes"))
                {
                    INullable objChk = (INullable)obj;
                    if (objChk.IsNull)
                        return value;
                    else
                        return obj;
                }
                else
                {
                    return obj;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
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
            string str = string.Format("{0:" + format + "}", num);
            return str;
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        [Transaction]
        internal bool RegistData()
        {
            try
            {
                LogUtility.DebugMethodStart();

                using (Transaction tran = new Transaction())
                {

                    //受付入力の元レコードを論理削除
                    foreach (T_UKEIRE_ENTRY entity in this.delUkeireEntryEntityList)
                    {
                        // 登録処理を行う
                        this.daoUkeireEntry.Update(entity);
                    }

                    //  受付入力レコードをループ
                    foreach (T_UKEIRE_ENTRY entity in this.insUkeireEntryEntityList)
                    {
                        // 登録処理を行う
                        this.daoUkeireEntry.Insert(entity);
                    }

                    //受付入力明細レコードをループ
                    foreach (T_UKEIRE_DETAIL entity in this.insUkeireEntryDetailList)
                    {
                        // 登録処理を行う
                        this.daoUkeireDetail.Insert(entity);
                    }


                    //出荷入力の元レコードを論理削除
                    foreach (T_SHUKKA_ENTRY entity in this.delShukkaEntryEntityList)
                    {
                        // 登録処理を行う
                        this.daoShukkaEntry.Update(entity);
                    }

                    //  出荷入力レコードをループ
                    foreach (T_SHUKKA_ENTRY entity in this.insShukkaEntryEntityList)
                    {
                        // 登録処理を行う
                        this.daoShukkaEntry.Insert(entity);
                    }

                    //出荷入力明細レコードをループ
                    foreach (T_SHUKKA_DETAIL entity in this.insShukkaEntryDetailList)
                    {
                        // 登録処理を行う
                        this.daoShukkaDetail.Insert(entity);
                    }

                    //売上/支払入力の元レコードを論理削除
                    foreach (T_UR_SH_ENTRY entity in this.delUrshEntryEntityList)
                    {
                        // 登録処理を行う
                        this.daoUrshEntry.Update(entity);
                    }

                    //  売上/支払入力レコードをループ
                    foreach (T_UR_SH_ENTRY entity in this.insUrshEntryEntityList)
                    {
                        // 登録処理を行う
                        this.daoUrshEntry.Insert(entity);
                    }

                    //売上/支払入力明細レコードをループ
                    foreach (T_UR_SH_DETAIL entity in this.insUrshEntryDetailList)
                    {
                        // 登録処理を行う
                        this.daoUrshDetail.Insert(entity);
                    }

                    // コミット
                    tran.Commit();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                return false;

            }

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region 取消処理
        /// <summary>
        /// 取消処理
        /// </summary>
        internal void Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // UIFormのコントロールを制御
                List<string> formControlNameList = new List<string>();
                formControlNameList.AddRange(clearUiFormControlNames);
                formControlNameList.AddRange(clearHeaderControlNames);
                foreach (var controlName in formControlNameList)
                {
                    // メインフォームからコントロールを取得
                    Control control = controlUtil.FindControl(this.form, controlName);

                    if (control == null)
                    {
                        // ヘッダフォームからコントロールを取得
                        control = controlUtil.FindControl(this.header, controlName);
                    }

                    if (control == null)
                    {
                        continue;
                    }

                    PropertyInfo property;
                    // 日付コントロールの場合
                    if (control is CustomDateTimePicker)
                    {
                        // Valueをクリア
                        ((CustomDateTimePicker)control).Value = null;
                    }
                    else
                    {
                        // Textプロパティを取得
                        property = control.GetType().GetProperty("Text");
                        if (property != null)
                        {
                            // クリア
                            property.SetValue(control, string.Empty, null);
                        }
                    }

                    // IsInputErrorOccuredプロパティを取得
                    property = control.GetType().GetProperty("IsInputErrorOccured");
                    if (property != null)
                    {
                        // クリア
                        property.SetValue(control, false, null);
                    }
                }
                // 明細クリア
                //this.form.grdIchiran.CellValidating -= this.form.dgvDetail_CellValidating;
                //this.dgvDetail.CurrentCell = null;
                this.form.grdIchiran.Rows.Clear();
                //this.dgvDetail.CellValidating += this.form.dgvDetail_CellValidating;
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

        #region 一覧の新規単価ReadOnly設定
        /// <summary>
        /// 一覧の新規単価ReadOnly設定
        /// </summary>
        /// <param name="readOnlyFlag"></param>
        internal void changeIchiranTankaReadOnly(bool readOnlyFlag)
        {
            try
            {
                LogUtility.DebugMethodStart();
                //変更方法によって、新単価セルは入力可/入力不可切り替え
                for (int i = 0; i < this.form.grdIchiran.Rows.Count; i++)
                {
                    Row row = this.form.grdIchiran.Rows[i];
                    //読取専用か
                    row.Cells["SHIN_TANKA"].ReadOnly = readOnlyFlag;
                    //バック色は変更
                    if (readOnlyFlag)
                    {
                        //灰色
                        row.Cells["SHIN_TANKA"].Style.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(250)))), ((int)(((byte)(230)))));
                        row.Cells["SHIN_TANKA"].Selected = false;
                    }
                    else
                    {
                        row.Cells["SHIN_TANKA"].Style.BackColor = System.Drawing.Color.Empty;
                        row.Cells["SHIN_TANKA"].Selected = true;
                    }
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

        #region 明細CellのAlign設定
        /// <summary>
        /// 明細CellのAlign設定
        /// </summary>
        /// <param name="eCell">セール</param>
        private void SetCellAlign(GrapeCity.Win.MultiRow.Cell cell, string alignMode)
        {
            try
            {
                //LogUtility.DebugMethodStart(eCell, alignFlg);

                switch (alignMode)
                {
                    case "MiddleCenter":
                        cell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleCenter;
                        break;
                    case "MiddleRight":
                        cell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleRight;
                        break;
                    case "MiddleLeft":
                        cell.Style.TextAlign = GrapeCity.Win.MultiRow.MultiRowContentAlignment.MiddleLeft;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                //LogUtility.DebugMethodEnd(breakNo);
            }
        }
        #endregion
        /// <summary>
        /// 新単価の必須項目の背景が紅色
        /// </summary>
        internal void ShinTankaChk()
        {
            for (int i = 0; i < this.form.grdIchiran.Rows.Count; i++)
            {
                Row row = this.form.grdIchiran.Rows[i];

                if (null == row.Cells["SHIN_TANKA"].Value || string.IsNullOrEmpty(row.Cells["SHIN_TANKA"].Value.ToString()) || "0".Equals(row.Cells["SHIN_TANKA"].Value))
                {
                    row.Cells["SHIN_TANKA"].Selected = false;
                    //背景が紅色
                    row.Cells["SHIN_TANKA"].Style.BackColor = System.Drawing.Color.Red;

                }

            }
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        /// <returns></returns>
        internal bool CheckGenba()
        {
            bool correct = false;

            if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                // 現場CDが空の場合、チェックは行わず、名称は空欄とする
                this.form.GENBA_NAME.Text = string.Empty;
                correct = true;
            }
            else
            {
                // 現場チェック
                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                keyEntity.GENBA_CD = this.form.GENBA_CD.Text;

                r_framework.Dao.IM_GENBADao genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
                var genbaData = genbaDao.GetAllValidData(keyEntity);
                if (genbaData != null && genbaData.Length > 0)
                {
                    var genba = genbaData[0];
                    this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                    correct = true;
                }
            }

            if (!correct)
            {
                //エラー時、名称を空欄にする。
                this.form.GENBA_NAME.Text = string.Empty;
                this.msgLogic.MessageBoxShow("E020", "現場");
            }
            return correct;
        }

        /// <summary>
        /// 荷卸現場チェック 
        /// </summary>
        internal bool CheckNiororhiGenba()
        {
            bool sucsess = false;

            if (string.IsNullOrEmpty(this.form.NIOROSHI_GENBA_CD.Text))
            {
                // 荷降現場CDが空の場合、チェックは行わず、名称は空欄とする
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                sucsess = true;
            }
            else
            {
                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                keyEntity.GENBA_CD = this.form.NIOROSHI_GENBA_CD.Text;

                r_framework.Dao.IM_GENBADao genbaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GENBADao>();
                var genbaData = genbaDao.GetAllValidData(keyEntity);

                // 荷降場チェック
                if (genbaData != null && genbaData.Length > 0)
                {
                    var genba = genbaData[0];

                    // 20151026 BUNN #12040 STR
                    // 処分事業場/荷降現場、積替え保管区分、最終処分場チェック
                    if (genba.SHOBUN_NIOROSHI_GENBA_KBN.IsTrue || genba.TSUMIKAEHOKAN_KBN.IsTrue || genba.SAISHUU_SHOBUNJOU_KBN.IsTrue)
                    // 20151026 BUNN #12040 END
                    {
                        this.form.NIOROSHI_GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                        sucsess = true;
                    }
                }
            }

            if (!sucsess)
            {
                //エラー時、名称を空欄にする。
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                this.msgLogic.MessageBoxShow("E020", "現場");
            }
            return sucsess;
        }

        /// <summary>
        /// 荷降業者チェック
        /// </summary>
        internal bool CheckNioroshiGyoushaCd()
        {
            bool correct = false;

            var msgLogic = new MessageBoxShowLogic();

            if (string.IsNullOrEmpty(this.form.NIOROSHI_GYOUSHA_CD.Text))
            {
                // 荷降業者CDが空の場合は、荷降現場CDも空にし、チェックは行わない
                this.form.NIOROSHI_GENBA_CD.Text = string.Empty;
                this.form.NIOROSHI_GENBA_NAME.Text = string.Empty;
                correct = true;
            }
            else
            {
                // 入力された荷降業者CDのチェック
                M_GYOUSHA keyEntityGyousha = new M_GYOUSHA();
                keyEntityGyousha.GYOUSHA_CD = this.form.NIOROSHI_GYOUSHA_CD.Text;
                r_framework.Dao.IM_GYOUSHADao gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
                var gyoushaData = gyoushaDao.GetAllValidData(keyEntityGyousha);
                if (gyoushaData != null && gyoushaData.Length > 0)
                {
                    M_GYOUSHA gyousha = gyoushaData[0];
                    // 20151026 BUNN #12040 STR
                    if (gyousha.SHOBUN_NIOROSHI_GYOUSHA_KBN.IsTrue || gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                    // 20151026 BUNN #12040 END
                    {
                        // 受託者区分、荷降業者区分のいずれかがTrueの場合、荷降業者名をセット
                        this.form.NIOROSHI_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                        correct = true;
                    }
                    else
                    {
                        // 入力された業者CDが荷降業者に該当しない場合は、エラー
                        msgLogic.MessageBoxShow("E058");
                        this.form.NIOROSHI_GYOUSHA_CD.Focus();
                    }
                }
                else
                {
                    msgLogic.MessageBoxShow("E020", "業者");
                    this.form.NIOROSHI_GYOUSHA_CD.Focus();
                }
            }
            return correct;
        }

        #region ダブルクリック時にFrom項目の入力内容をコピーする
        /// <summary>
        /// ダブルクリック時にFrom項目の入力内容をコピーする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // 20141201 teikyou ダブルクリックを追加する　start
        private void DENPYOU_DATE_TO_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            var denpyouDateFromTextBox = this.form.DENPYOU_DATE_FROM;
            var denpyouDateToTextBox = this.form.DENPYOU_DATE_TO;
            denpyouDateToTextBox.Text = denpyouDateFromTextBox.Text;

            LogUtility.DebugMethodEnd();
        }
        // 20141201 teikyou ダブルクリックを追加する　end
        #endregion

        #region 登録/更新/削除（実装しない）

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
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

        /// 20141203 Houkakou 「伝票単価一括変更」の日付チェックを追加する　start
        #region 日付チェック
        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck()
        {
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            this.form.DENPYOU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            this.form.DENPYOU_DATE_TO.BackColor = Constans.NOMAL_COLOR;

            //nullチェック
            if (string.IsNullOrEmpty(this.form.DENPYOU_DATE_FROM.Text))
            {
                return false;
            }
            if (string.IsNullOrEmpty(this.form.DENPYOU_DATE_TO.Text))
            {
                return false;
            }

            DateTime date_from = Convert.ToDateTime(this.form.DENPYOU_DATE_FROM.Value);
            DateTime date_to = Convert.ToDateTime(this.form.DENPYOU_DATE_TO.Value);

            // 日付FROM > 日付TO 場合
            if (date_to.CompareTo(date_from) < 0)
            {
                this.form.DENPYOU_DATE_FROM.IsInputErrorOccured = true;
                this.form.DENPYOU_DATE_TO.IsInputErrorOccured = true;
                this.form.DENPYOU_DATE_FROM.BackColor = Constans.ERROR_COLOR;
                this.form.DENPYOU_DATE_TO.BackColor = Constans.ERROR_COLOR;

                string strFrom = null;
                string strTo = null;
                string strNum_DenpyouHiduke = this.form.txtNum_DenpyouHiduke.Text;
                if (strNum_DenpyouHiduke.Equals("1"))
                {
                    strFrom = "伝票日付From";
                    strTo = "伝票日付To";
                }
                else
                {
                    strFrom = "入力日付From";
                    strTo = "入力日付To";
                }
                string[] errorMsg = { strFrom, strTo };
                msgLogic.MessageBoxShow("E030", errorMsg);
                this.form.DENPYOU_DATE_FROM.Focus();
                return true;
            }

            return false;
        }
        #endregion

        #region DENPYOU_DATE_FROM_Leaveイベント
        /// <summary>
        /// DENPYOU_DATE_FROM_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void DENPYOU_DATE_FROM_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE_TO.Text))
            {
                this.form.DENPYOU_DATE_TO.IsInputErrorOccured = false;
                this.form.DENPYOU_DATE_TO.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion

        #region DENPYOU_DATE_TO_Leaveイベント
        /// <summary>
        /// DENPYOU_DATE_TO_Leaveイベント
        /// </summary>
        /// <returns></returns>
        private void DENPYOU_DATE_TO_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.DENPYOU_DATE_FROM.Text))
            {
                this.form.DENPYOU_DATE_FROM.IsInputErrorOccured = false;
                this.form.DENPYOU_DATE_FROM.BackColor = Constans.NOMAL_COLOR;
            }
        }
        #endregion
        /// 20141203 Houkakou 「伝票単価一括変更」の日付チェックを追加する　end

        /// <summary>
        /// 運搬業者チェック
        /// </summary>
        internal void CheckUnpanGyousha()
        {
            if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                return;
            }

            M_GYOUSHA entity = new M_GYOUSHA();
            r_framework.Dao.IM_GYOUSHADao gyoushaDao = DaoInitUtility.GetComponent<r_framework.Dao.IM_GYOUSHADao>();
            entity.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            var gyoushaData = gyoushaDao.GetAllValidData(entity);

            if (gyoushaData != null && gyoushaData.Length > 0)
            {
                M_GYOUSHA gyousha = gyoushaData[0];
                if (gyousha.UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                {
                    this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    // 入力された業者CDが運搬業者に該当しない場合は、エラー
                    msgLogic.MessageBoxShow("E058");
                    this.form.UNPAN_GYOUSHA_CD.Focus();
                }
            }
            else
            {
                msgLogic.MessageBoxShow("E020", "業者");
                this.form.UNPAN_GYOUSHA_CD.Focus();
            }
        }
    }
}
