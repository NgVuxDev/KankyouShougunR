using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using CommonChouhyouPopup.App;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Configuration;
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
using Shougun.Core.Common.BusinessCommon.Const;
using Shougun.Core.Reception.UketsukeSyuusyuuNyuuryoku;
using System.Text;

namespace Shougun.Core.Allocation.HaishaWariateDay
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        #region 定数

        /// <summary>
        /// 運転者CD
        /// </summary>
        internal const string CELL_NAME_UNTENSHA_CD = "cellUntenshaCd";

        /// <summary>
        /// 運転者名
        /// </summary>
        internal const string CELL_NAME_UNTENSHA_NAME = "cellUntenshaName";

        /// <summary>
        /// 車輌CD
        /// </summary>
        internal const string CELL_NAME_SHARYOU_CD = "cellSharyouCd";

        /// <summary>
        /// 車輌名
        /// </summary>
        internal const string CELL_NAME_SHARYOU_NAME = "cellSharyouName";

        /// <summary>
        /// 車種CD
        /// </summary>
        internal const string CELL_NAME_SHASHU_CD = "cellShashuCd";

        /// <summary>
        /// 車種名
        /// </summary>
        internal const string CELL_NAME_SHASHU_NAME = "cellShashuName";

        /// <summary>
        /// 運搬業者CD
        /// </summary>
        internal const string CELL_NAME_UNPAN_GYOUSHA_CD = "cellUnpanGyoushaCd";

        /// <summary>
        /// 運搬業者名
        /// </summary>
        internal const string CELL_NAME_UNPAN_GYOUSHA_NAME = "cellUnpanGyoushaName";

        #endregion

        /// <summary>
        /// 配車割当（一日）DTO
        /// </summary>
        private DTO_Haisha dtoHaisha = new DTO_Haisha();
        /// <summary>
        /// システムID枝番DTO
        /// </summary>
        private DTO_IdSeq dtoIdSeq = new DTO_IdSeq();
        /// <summary>
        /// 配車割当（一日）DAO
        /// </summary>
        private DAO_T_HAISHA_WARIATE_DAY dao_HAISHA = DaoInitUtility.GetComponent<DAO_T_HAISHA_WARIATE_DAY>();
        /// <summary>
        /// 配車メモDAO
        /// </summary>
        private DAO_T_HAISHA_MEMO daoT_HAISHA_MEMO = DaoInitUtility.GetComponent<DAO_T_HAISHA_MEMO>();
        /// <summary>
        /// 受付（収集）入力DAO
        /// </summary>
        private DAO_T_UKETSUKE_SS_ENTRY daoT_UKETSUKE_SS_ENTRY = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SS_ENTRY>();
        /// <summary>
        /// 受付（収集）明細DAO
        /// </summary>
        private DAO_T_UKETSUKE_SS_DETAIL daoT_UKETSUKE_SS_DETAIL = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SS_DETAIL>();
        /// <summary>
        /// コンテナ稼動予定DAO
        /// </summary>
        private DAO_T_CONTENA_RESERVE daoT_CONTENA_RESERVE = DaoInitUtility.GetComponent<DAO_T_CONTENA_RESERVE>();
        /// <summary>
        /// 受付（出荷）入力DAO
        /// </summary>
        private DAO_T_UKETSUKE_SK_ENTRY daoT_UKETSUKE_SK_ENTRY = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SK_ENTRY>();
        /// <summary>
        /// 受付（出荷）明細DAO
        /// </summary>
        private DAO_T_UKETSUKE_SK_DETAIL daoT_UKETSUKE_SK_DETAIL = DaoInitUtility.GetComponent<DAO_T_UKETSUKE_SK_DETAIL>();
        /// <summary>
        /// 定期配車入力DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_ENTRY daoT_TEIKI_HAISHA_ENTRY = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_ENTRY>();
        /// <summary>
        /// 定期配車明細DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_DETAIL daoT_TEIKI_HAISHA_DETAIL = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_DETAIL>();
        /// <summary>
        /// 定期配車荷降DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_NIOROSHI daoT_TEIKI_HAISHA_NIOROSHI = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_NIOROSHI>();
        /// <summary>
        /// 定期配車詳細DAO
        /// </summary>
        private DAO_T_TEIKI_HAISHA_SHOUSAI daoT_TEIKI_HAISHA_SHOUSAI = DaoInitUtility.GetComponent<DAO_T_TEIKI_HAISHA_SHOUSAI>();

        /// <summary>
        /// 拠点マスタ
        /// </summary>
        private IM_KYOTENDao m_kyotendao;
        /// <summary>
        /// 業者Dao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;
        /// <summary>
        /// 現場Dao
        /// </summary>
        private IM_GENBADao genbaDao;
        /// <summary>
        /// 都道府県Dao
        /// </summary>
        private IM_TODOUFUKENDao todoufukenDao;
        /// <summary>
        /// 運転者Dao
        /// </summary>
        private IM_SHAINDao untenshaDao;
        /// <summary>
        /// 車種Dao
        /// </summary>
        private IM_SHASHUDao shashuDao;
        /// <summary>
        /// 運搬業者Dao
        /// </summary>
        private IM_GYOUSHADao unpanGyoyushaDao;
        /// <summary>
        /// 車輌Dao
        /// </summary>
        private IM_SHARYOUDao sharyouDao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// モバイル連携DAO
        /// </summary>
        private IT_MOBISYO_RTDao mobisyoRtDao;

        /// <summary>
        /// コース名Dao
        /// </summary>
        private IM_COURSE_NAMEDao courseDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form
        /// </summary>
        private HeaderForm header;

        /// <summary>
        /// 親フォーム
        /// </summary>
        private BusinessBaseForm parentForm;

        /// <summary>
        /// 検索結果DataTable
        /// </summary>
        private DataTable resultHaisha = new DataTable();
        private DataTable resultMihaisha = new DataTable();

        /// <summary>
        /// 
        /// </summary>
        public Cell selectedCell = null;

        /// <summary>
        /// 前回値チェック用変数(配車セル用)
        /// </summary>
        private Dictionary<string, string> beforeValuesForHaisha = new Dictionary<string, string>();

        //指定コントロールのペイントを禁止
        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, IntPtr lParam);

        private const int WM_SETREDRAW = 0xB;

        /// <summary>

        /// <summary>
        /// 配車済件数
        /// </summary>
        private int _haishaCount;
        public int HaishaCount
        {
            get { return _haishaCount; }
            set
            {
                _haishaCount = value;
                this.header.tbHaishaCount.Text = this._haishaCount.ToString();
                this.header.tbTotalCount.Text = (this._haishaCount + this._mihaishaCount).ToString();
            }
        }

        /// <summary>
        /// 未配車件数
        /// </summary>
        private int _mihaishaCount;
        public int MihaishaCount
        {
            get { return _mihaishaCount; }
            set
            {
                _mihaishaCount = value;
                this.header.tbMihaishaCount.Text = this._mihaishaCount.ToString();
                this.header.tbTotalCount.Text = (this._haishaCount + this._mihaishaCount).ToString();
            }
        }

        /// <summary>
        /// メッセージ共通クラス
        /// </summary>
        public MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

        /// <summary>
        /// MultiRow
        /// </summary>
        private GcCustomMultiRow mrHaisha;
        private GcCustomMultiRow mrMihaisha;

        /// <summary>
        /// 車両数
        /// </summary>
        private int sharyouCount;

        // 20141015 koukouei 休動管理機能 start
        internal bool validatedFlag;
        internal GcCustomTextBoxCell errorCell;
        // 20141015 koukouei 休動管理機能 end

        /// <summary>
        /// Validation内の処理をキャンセルするかを判断するフラグです
        /// </summary>
        internal bool validationCancelFlg = false;


        internal bool changeHaishaFlg = false;
        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.parentForm = (BusinessBaseForm)this.form.Parent;
            this.header = (HeaderForm)this.parentForm.headerForm;
            this.mrHaisha = this.form.mrHaisha;
            this.mrMihaisha = this.form.mrMihaisha;
            this.m_kyotendao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.mobisyoRtDao = DaoInitUtility.GetComponent<IT_MOBISYO_RTDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.todoufukenDao = DaoInitUtility.GetComponent<IM_TODOUFUKENDao>();
            this.untenshaDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.shashuDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
            this.unpanGyoyushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
            this.courseDao = DaoInitUtility.GetComponent<IM_COURSE_NAMEDao>();
            this.msgLogic = new MessageBoxShowLogic();

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 初期化

        #region 画面初期化処理
        /// <summary>
        /// 
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得
                this.GetSysInfoInit();
                //　ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                this.parentForm.ProcessButtonPanel.Visible = false;
                this.form.dtpSagyouDate.Value = parentForm.sysDate;
                // 20141015 koukouei 休動管理機能 start
                this.form.SAGYOU_DATE.Value = parentForm.sysDate;
                // 20141015 koukouei 休動管理機能 end

                if (IsShasyuSyaryou())
                {
                    // 運転者は非表示
                    this.form.lblUntensha.Visible = false;
                    this.form.UNTENSHA_CD.Visible = false;
                    this.form.UNTENSHA_NAME.Visible = false;
                }
                else
                {
                    // 車種は非表示
                    this.form.lblShashu.Visible = false;
                    this.form.SHASHU_CD.Visible = false;
                    this.form.SHASHU_NAME.Visible = false;
                }

                this.SetHeaderFormInitialData();

                // 配車割当明細の項目位置設定
                this.ExecuteAlignmentForHaisha();


                this.Bind();
               
                this.form.mrHaisha.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                this.form.mrHaisha.Size = new Size(this.form.mrHaisha.Size.Width, this.form.lbWariateSettei.Location.Y - this.form.mrHaisha.Location.Y - 5);
                this.form.mrMihaisha.Anchor = AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                this.form.mrMihaisha.Size = new Size(this.form.mrMihaisha.Size.Width, this.form.lbWariateSettei.Location.Y - this.form.mrMihaisha.Location.Y - 5);
                this.form.tbShoriKbn.Text = "1";
                this.form.tbWariateSettei.Text = "1";

                this.SetColumnHeader();
                
                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("G026", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    // 参照モードに変更
                    this.SetReferenceMode();
                }

                if (sysInfoEntity != null && !sysInfoEntity.HAISHA_WARIATE_KAISHI.IsNull)
                {
                    this.ScrollHaishaDataGrid(sysInfoEntity.HAISHA_WARIATE_KAISHI.Value);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
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

        #region ScrollHaishaDataGrid
        /// <summary>
        /// ScrollHaishaDataGrid
        /// </summary>
        public void ScrollHaishaDataGrid(int kijunResu)
        {
            try
            {
                kijunResu = kijunResu + 1;
                string suffix = GetSuffix(kijunResu);
                decimal x = this.mrHaisha.ColumnHeaders[0].Cells["cellHeader01"].Size.Width * (decimal)mrHaisha.ZoomFactor * (kijunResu - 1);
                int pointX = int.Parse(Math.Round(x, MidpointRounding.AwayFromZero).ToString());
                int pointY = this.mrHaisha.ColumnHeaders[0].Cells["cellHeader01"].Location.Y;
                this.form.mrHaisha.FirstDisplayedLocation = new Point(pointX, pointY);
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }

        }
        #endregion

        #region セット列ヘッダ
        
        /// <summary>
        /// セット列ヘッダ
        /// </summary>
        /// <returns></returns>
        private void SetColumnHeader()
        {
            int haishaWariateKaishi = 0;
            /*if (sysInfoEntity != null && !sysInfoEntity.HAISHA_WARIATE_KAISHI.IsNull)
            {
                haishaWariateKaishi = sysInfoEntity.HAISHA_WARIATE_KAISHI.Value;
            }*/

            for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
            {
                if (haishaWariateKaishi >= ConstClass.DENPYOU_COUNT)
                {
                    haishaWariateKaishi = 0;
                }
                string suffix = this.GetSuffix(i + 1);
                this.mrHaisha.ColumnHeaders[0]["cellHeader" + suffix].Value = haishaWariateKaishi;
                haishaWariateKaishi += 1;
            }
        }
        
        #endregion
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
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            var buttonSetting = new ButtonSetting();
            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, ConstClass.BUTTON_SETTING_XML);
        }
        #endregion

        #region ボタン初期化処理
        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            ButtonControlUtility.SetButtonInfo(buttonSetting, this.parentForm, WINDOW_TYPE.ICHIRAN_WINDOW_FLAG);

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// ボタンのイベント初期化処理
        /// </summary>
        /// <returns></returns>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            this.form.C_Regist(this.parentForm.bt_func9);

            this.parentForm.bt_func1.Click += this.bt_func1_Click;
            this.parentForm.bt_func2.Click += this.bt_func2_Click; 
            this.parentForm.bt_func3.Click += this.bt_func3_Click;
            this.parentForm.bt_func4.Click += this.bt_func4_Click;
            this.parentForm.bt_func5.Click += this.bt_func5_Click;
            this.parentForm.bt_func6.Click += this.bt_func6_Click;
            //this.parentForm.bt_func7.Click += this.bt_func7_Click;
            this.parentForm.bt_func8.Click += this.bt_func8_Click;
            this.parentForm.bt_func9.Click += this.bt_func9_Click;
            this.parentForm.bt_func10.Click += this.bt_func10_Click;
            this.parentForm.bt_func11.Click += this.bt_func11_Click;
            this.parentForm.bt_func12.Click += this.bt_func12_Click;
            this.form.btnSeiretsu.Click += new EventHandler(btnSeiretsu_Click);
            EnableButtons(false);

            LogUtility.DebugMethodEnd();
        }


        #endregion

        #region ヘッダ初期値設定
        /// <summary>
        /// ヘッダの初期値を設定します
        /// </summary>
        private void SetHeaderFormInitialData()
        {
                this.header.KYOTEN_CD.Text = "99";
                var dao = DaoInitUtility.GetComponent<r_framework.Dao.IM_KYOTENDao>();
                var kyoten = dao.GetDataByCd(this.header.KYOTEN_CD.Text);
                if (null != kyoten)
                {
                    this.header.KYOTEN_NAME.Text = kyoten.KYOTEN_NAME_RYAKU;
                }

        }
        #endregion

        #region ボタン制御
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enabled"></param>
        private void EnableButtons(bool enabled)
        {
            this.parentForm.bt_func1.Enabled = enabled;
            this.parentForm.bt_func2.Enabled = enabled;
            this.parentForm.bt_func3.Enabled = enabled;
            this.parentForm.bt_func4.Enabled = enabled;
            this.parentForm.bt_func5.Enabled = enabled;
            this.parentForm.bt_func6.Enabled = enabled;
            //this.parentForm.bt_func7.Enabled = enabled;
            this.parentForm.bt_func9.Enabled = enabled;
            this.parentForm.bt_func10.Enabled = enabled;
            this.parentForm.bt_func11.Enabled = enabled;

            this.form.btnSeiretsu.Enabled = enabled;
        }
        #endregion

        #region 配車割当明細の項目表示制御
        /// <summary>
        /// 
        /// </summary>
        internal void ExecuteAlignmentForHaisha()
        {
            LogUtility.DebugMethodStart();

            // 項目表示制御開始
            this.mrHaisha.SuspendLayout();
            var newTemplate = this.mrHaisha.Template;

            // 各項目の位置設定
            if (IsShasyuSyaryou())
            {
                if (!AppConfig.AppOptions.IsMAPBOX())
                {
                    #region mapboxオプションなし、車輛軸
                    var MapHeader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader1"];
                    MapHeader.Visible = false;
                    var MapCell = newTemplate.Row.Cells["buttonMap"];
                    MapCell.Visible = false;

                    // 一列目（車輌）
                    var ShashuHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellShashu"];
                    ShashuHeader.Location = new Point(25, 0);
                    var SharyouHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellSharyou"];
                    SharyouHeader.Location = new Point(25, 21);
                    var ShashuCdCell = newTemplate.Row.Cells["cellShashuCd"];
                    ShashuCdCell.Location = new Point(25, 0);
                    ShashuCdCell.TabIndex = 0;
                    var Unused1Cell = newTemplate.Row.Cells["cellUnused1"];
                    Unused1Cell.Location = new Point(85, 0);
                    var ShashuNameCell = newTemplate.Row.Cells["cellShashuName"];
                    ShashuNameCell.Location = new Point(25, 20);
                    ShashuNameCell.TabIndex = 1;
                    var SharyouCdCell = newTemplate.Row.Cells["cellSharyouCd"];
                    SharyouCdCell.Location = new Point(25, 48);
                    SharyouCdCell.TabIndex = 4;
                    var Unused2Cell = newTemplate.Row.Cells["cellUnused2"];
                    Unused2Cell.Location = new Point(85, 48);
                    var SaidaiWakusuuCell = newTemplate.Row.Cells["cellSaidaiWakusuu"];
                    SaidaiWakusuuCell.Location = new Point(25, 68);
                    var WorkClosedSharyouCell = newTemplate.Row.Cells["cellWorkClosedSharyou"];
                    WorkClosedSharyouCell.Location = new Point(54, 68);
                    var SharyouNameCell = newTemplate.Row.Cells["cellSharyouName"];
                    SharyouNameCell.Location = new Point(25, 76);
                    SharyouNameCell.TabIndex = 5;
                    var cellGyoushaCd = newTemplate.Row.Cells["cellGyoushaCd"];
                    cellGyoushaCd.Location = new Point(85, 68);
                    var cellGyoushaName = newTemplate.Row.Cells["cellGyoushaName"];
                    cellGyoushaName.Location = new Point(108, 68);
                    // 二列目（運転者）
                    var UntenshaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUntensha"];
                    UntenshaHeader.Location = new Point(174, 0);
                    var UntenshaCdCell = newTemplate.Row.Cells["cellUntenshaCd"];
                    UntenshaCdCell.Location = new Point(174, 0);
                    UntenshaCdCell.TabIndex = 2;
                    var UntenshaNameCell = newTemplate.Row.Cells["cellUntenshaName"];
                    UntenshaNameCell.Location = new Point(174, 20);
                    UntenshaNameCell.TabIndex = 3;
                    var WorkClosedUntenshaCell = newTemplate.Row.Cells["cellWorkClosedUntensha"];
                    WorkClosedUntenshaCell.Location = new Point(174, 52);
                    var BikouCell = newTemplate.Row.Cells["cellBikou"];
                    BikouCell.Location = new Point(174, 48);
                    var UnpanGyoushaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUnpanGyousha"];
                    UnpanGyoushaHeader.Location = new Point(174, 21);
                    var UnpanGyoushaCdCell = newTemplate.Row.Cells["cellUnpanGyoushaCd"];
                    UnpanGyoushaCdCell.Location = new Point(174, 48);
                    UnpanGyoushaCdCell.TabIndex = 6;
                    var UnpanGyoushaNameCell = newTemplate.Row.Cells["cellUnpanGyoushaName"];
                    UnpanGyoushaNameCell.Location = new Point(174, 76);
                    UnpanGyoushaNameCell.TabIndex = 7;
                    // 固定列は「運転者」
                    this.mrHaisha.FreezeLeftCellName = "cellUntenshaName";

                    // 地図表示ボタンを非表示にする分の位置調整
                    var meisaiHeader = newTemplate.ColumnHeaders[0].Cells["cellHeader01"];
                    meisaiHeader.Location = new Point(244, 0);
                    var HaishaShuruiCell = newTemplate.Row.Cells["cellHaishaShurui01"];
                    HaishaShuruiCell.Location = new Point(244, 0);
                    var HaishaSijishoStatusCell = newTemplate.Row.Cells["cellHaishaSijishoStatus01"];
                    HaishaSijishoStatusCell.Location = new Point(244, 20);
                    var DenpyouContentCell = newTemplate.Row.Cells["cellDenpyouContent01"];
                    DenpyouContentCell.Location = new Point(244, 40);
                    var GenchakuJikanCell = newTemplate.Row.Cells["cellGenchakuJikan01"];
                    GenchakuJikanCell.Location = new Point(264, 0);
                    var SagyouDateKubunCell = newTemplate.Row.Cells["cellSagyouDateKubun01"];
                    SagyouDateKubunCell.Location = new Point(264, 0);
                    var HaishaSijishoCheckBoxCell = newTemplate.Row.Cells["cellHaishaSijishoCheckBox01"];
                    HaishaSijishoCheckBoxCell.Location = new Point(309, 20);
                    var EmptyCell = newTemplate.Row.Cells["cellEmpty01"];
                    EmptyCell.Location = new Point(334, 20);
                    #endregion
                }
                else
                {
                    #region mapboxオプションあり、車輛軸
                    var MapHeader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader1"];
                    MapHeader.Visible = true;
                    var MapCell = newTemplate.Row.Cells["buttonMap"];
                    MapCell.Visible = true;

                    // 一列目（車輌）
                    var ShashuHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellShashu"];
                    ShashuHeader.Location = new Point(52, 0);
                    var SharyouHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellSharyou"];
                    SharyouHeader.Location = new Point(52, 21);
                    var ShashuCdCell = newTemplate.Row.Cells["cellShashuCd"];
                    ShashuCdCell.Location = new Point(52, 0);
                    ShashuCdCell.TabIndex = 0;
                    var Unused1Cell = newTemplate.Row.Cells["cellUnused1"];
                    Unused1Cell.Location = new Point(112, 0);
                    var ShashuNameCell = newTemplate.Row.Cells["cellShashuName"];
                    ShashuNameCell.Location = new Point(52, 20);
                    ShashuNameCell.TabIndex = 1;
                    var SharyouCdCell = newTemplate.Row.Cells["cellSharyouCd"];
                    SharyouCdCell.Location = new Point(52, 48);
                    SharyouCdCell.TabIndex = 4;
                    var Unused2Cell = newTemplate.Row.Cells["cellUnused2"];
                    Unused2Cell.Location = new Point(112, 48);
                    var SaidaiWakusuuCell = newTemplate.Row.Cells["cellSaidaiWakusuu"];
                    SaidaiWakusuuCell.Location = new Point(52, 68);
                    var WorkClosedSharyouCell = newTemplate.Row.Cells["cellWorkClosedSharyou"];
                    WorkClosedSharyouCell.Location = new Point(54, 68);
                    var SharyouNameCell = newTemplate.Row.Cells["cellSharyouName"];
                    SharyouNameCell.Location = new Point(52, 76);
                    SharyouNameCell.TabIndex = 5;
                    var cellGyoushaCd = newTemplate.Row.Cells["cellGyoushaCd"];
                    cellGyoushaCd.Location = new Point(112, 68);
                    var cellGyoushaName = newTemplate.Row.Cells["cellGyoushaName"];
                    cellGyoushaName.Location = new Point(135, 68);
                    // 二列目（運転者）
                    var UntenshaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUntensha"];
                    UntenshaHeader.Location = new Point(201, 0);
                    var UntenshaCdCell = newTemplate.Row.Cells["cellUntenshaCd"];
                    UntenshaCdCell.Location = new Point(201, 0);
                    UntenshaCdCell.TabIndex = 2;
                    var UntenshaNameCell = newTemplate.Row.Cells["cellUntenshaName"];
                    UntenshaNameCell.Location = new Point(201, 20);
                    UntenshaNameCell.TabIndex = 3;
                    var WorkClosedUntenshaCell = newTemplate.Row.Cells["cellWorkClosedUntensha"];
                    WorkClosedUntenshaCell.Location = new Point(201, 52);
                    var BikouCell = newTemplate.Row.Cells["cellBikou"];
                    BikouCell.Location = new Point(201, 48);
                    var UnpanGyoushaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUnpanGyousha"];
                    UnpanGyoushaHeader.Location = new Point(201, 21);
                    var UnpanGyoushaCdCell = newTemplate.Row.Cells["cellUnpanGyoushaCd"];
                    UnpanGyoushaCdCell.Location = new Point(201, 48);
                    UnpanGyoushaCdCell.TabIndex = 6;
                    var UnpanGyoushaNameCell = newTemplate.Row.Cells["cellUnpanGyoushaName"];
                    UnpanGyoushaNameCell.Location = new Point(201, 76);
                    UnpanGyoushaNameCell.TabIndex = 7;
                    // 固定列は「運転者」

                    this.mrHaisha.FreezeLeftCellName = "cellUntenshaName";
                    // 地図表示ボタンを表示する分の位置調整
                    var meisaiHeader = newTemplate.ColumnHeaders[0].Cells["cellHeader01"];
                    meisaiHeader.Location = new Point(271, 0);
                    var HaishaShuruiCell = newTemplate.Row.Cells["cellHaishaShurui01"];
                    HaishaShuruiCell.Location = new Point(271, 0);
                    var HaishaSijishoStatusCell = newTemplate.Row.Cells["cellHaishaSijishoStatus01"];
                    HaishaSijishoStatusCell.Location = new Point(271, 20);
                    var DenpyouContentCell = newTemplate.Row.Cells["cellDenpyouContent01"];
                    DenpyouContentCell.Location = new Point(271, 40);
                    var GenchakuJikanCell = newTemplate.Row.Cells["cellGenchakuJikan01"];
                    GenchakuJikanCell.Location = new Point(291, 0);
                    var SagyouDateKubunCell = newTemplate.Row.Cells["cellSagyouDateKubun01"];
                    SagyouDateKubunCell.Location = new Point(291, 0);
                    var HaishaSijishoCheckBoxCell = newTemplate.Row.Cells["cellHaishaSijishoCheckBox01"];
                    HaishaSijishoCheckBoxCell.Location = new Point(336, 20);
                    var EmptyCell = newTemplate.Row.Cells["cellEmpty01"];
                    EmptyCell.Location = new Point(361, 20);
                    #endregion
                }
            }
            else
            {
                if (!AppConfig.AppOptions.IsMAPBOX())
                {
                    #region mapboxオプションなし、運転者軸
                    var MapHeader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader1"];
                    MapHeader.Visible = false;
                    var MapCell = newTemplate.Row.Cells["buttonMap"];
                    MapCell.Visible = false;

                    // 一列目（運転者）
                    var UntenshaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUntensha"];
                    UntenshaHeader.Location = new Point(25, 0);
                    var UntenshaCdCell = newTemplate.Row.Cells["cellUntenshaCd"];
                    UntenshaCdCell.Location = new Point(25, 0);
                    UntenshaCdCell.TabIndex = 0;
                    var UntenshaNameCell = newTemplate.Row.Cells["cellUntenshaName"];
                    UntenshaNameCell.Location = new Point(25, 20);
                    UntenshaNameCell.TabIndex = 1;
                    var WorkClosedUntenshaCell = newTemplate.Row.Cells["cellWorkClosedUntensha"];
                    WorkClosedUntenshaCell.Location = new Point(25, 52);
                    var BikouCell = newTemplate.Row.Cells["cellBikou"];
                    BikouCell.Location = new Point(25, 48);
                    var UnpanGyoushaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUnpanGyousha"];
                    UnpanGyoushaHeader.Location = new Point(25, 21);
                    var UnpanGyoushaCdCell = newTemplate.Row.Cells["cellUnpanGyoushaCd"];
                    UnpanGyoushaCdCell.Location = new Point(25, 48);
                    UnpanGyoushaCdCell.TabIndex = 4;
                    var UnpanGyoushaNameCell = newTemplate.Row.Cells["cellUnpanGyoushaName"];
                    UnpanGyoushaNameCell.Location = new Point(25, 76);
                    UnpanGyoushaNameCell.TabIndex = 5;
                    // 二列目（車輌）
                    var ShashuHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellShashu"];
                    ShashuHeader.Location = new Point(95, 0);
                    var SharyouHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellSharyou"];
                    SharyouHeader.Location = new Point(95, 21);
                    var ShashuCdCell = newTemplate.Row.Cells["cellShashuCd"];
                    ShashuCdCell.Location = new Point(95, 0);
                    ShashuCdCell.TabIndex = 2;
                    var Unused1Cell = newTemplate.Row.Cells["cellUnused1"];
                    Unused1Cell.Location = new Point(155, 0);
                    var ShashuNameCell = newTemplate.Row.Cells["cellShashuName"];
                    ShashuNameCell.Location = new Point(95, 20);
                    ShashuNameCell.TabIndex = 3;
                    var SharyouCdCell = newTemplate.Row.Cells["cellSharyouCd"];
                    SharyouCdCell.Location = new Point(95, 48);
                    SharyouCdCell.TabIndex = 6;
                    var Unused2Cell = newTemplate.Row.Cells["cellUnused2"];
                    Unused2Cell.Location = new Point(155, 48);
                    var SaidaiWakusuuCell = newTemplate.Row.Cells["cellSaidaiWakusuu"];
                    SaidaiWakusuuCell.Location = new Point(95, 76);
                    var WorkClosedSharyouCell = newTemplate.Row.Cells["cellWorkClosedSharyou"];
                    WorkClosedSharyouCell.Location = new Point(124, 76);
                    var SharyouNameCell = newTemplate.Row.Cells["cellSharyouName"];
                    SharyouNameCell.Location = new Point(95, 76);
                    SharyouNameCell.TabIndex = 7;
                    var cellGyoushaCd = newTemplate.Row.Cells["cellGyoushaCd"];
                    cellGyoushaCd.Location = new Point(155, 76);
                    var cellGyoushaName = newTemplate.Row.Cells["cellGyoushaName"];
                    cellGyoushaName.Location = new Point(178, 76);
                    // 固定列は「車輌」
                    this.mrHaisha.FreezeLeftCellName = "cellShashuName";

                    // 地図表示ボタンを非表示にする分の位置調整
                    var meisaiHeader = newTemplate.ColumnHeaders[0].Cells["cellHeader01"];
                    meisaiHeader.Location = new Point(244, 0);
                    var HaishaShuruiCell = newTemplate.Row.Cells["cellHaishaShurui01"];
                    HaishaShuruiCell.Location = new Point(244, 0);
                    var HaishaSijishoStatusCell = newTemplate.Row.Cells["cellHaishaSijishoStatus01"];
                    HaishaSijishoStatusCell.Location = new Point(244, 20);
                    var DenpyouContentCell = newTemplate.Row.Cells["cellDenpyouContent01"];
                    DenpyouContentCell.Location = new Point(244, 40);
                    var GenchakuJikanCell = newTemplate.Row.Cells["cellGenchakuJikan01"];
                    GenchakuJikanCell.Location = new Point(264, 0);
                    var SagyouDateKubunCell = newTemplate.Row.Cells["cellSagyouDateKubun01"];
                    SagyouDateKubunCell.Location = new Point(264, 0);
                    var HaishaSijishoCheckBoxCell = newTemplate.Row.Cells["cellHaishaSijishoCheckBox01"];
                    HaishaSijishoCheckBoxCell.Location = new Point(309, 20);
                    var EmptyCell = newTemplate.Row.Cells["cellEmpty01"];
                    EmptyCell.Location = new Point(334, 20);
                    #endregion
                }
                else
                {
                    #region mapboxオプションあり、運転者軸

                    var MapHeader = newTemplate.ColumnHeaders[0].Cells["gcCustomColumnHeader1"];
                    MapHeader.Visible = true;
                    var MapCell = newTemplate.Row.Cells["buttonMap"];
                    MapCell.Visible = true;

                    // 一列目（運転者）
                    var UntenshaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUntensha"];
                    UntenshaHeader.Location = new Point(52, 0);
                    var UntenshaCdCell = newTemplate.Row.Cells["cellUntenshaCd"];
                    UntenshaCdCell.Location = new Point(52, 0);
                    UntenshaCdCell.TabIndex = 0;
                    var UntenshaNameCell = newTemplate.Row.Cells["cellUntenshaName"];
                    UntenshaNameCell.Location = new Point(52, 20);
                    UntenshaNameCell.TabIndex = 1;
                    var WorkClosedUntenshaCell = newTemplate.Row.Cells["cellWorkClosedUntensha"];
                    WorkClosedUntenshaCell.Location = new Point(52, 52);
                    var BikouCell = newTemplate.Row.Cells["cellBikou"];
                    BikouCell.Location = new Point(52, 48);
                    var UnpanGyoushaHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellUnpanGyousha"];
                    UnpanGyoushaHeader.Location = new Point(52, 21);
                    var UnpanGyoushaCdCell = newTemplate.Row.Cells["cellUnpanGyoushaCd"];
                    UnpanGyoushaCdCell.Location = new Point(52, 48);
                    UnpanGyoushaCdCell.TabIndex = 4;
                    var UnpanGyoushaNameCell = newTemplate.Row.Cells["cellUnpanGyoushaName"];
                    UnpanGyoushaNameCell.Location = new Point(52, 76);
                    UnpanGyoushaNameCell.TabIndex = 5;
                    // 二列目（車輌）
                    var ShashuHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellShashu"];
                    ShashuHeader.Location = new Point(122, 0);
                    var SharyouHeader = newTemplate.ColumnHeaders[0].Cells["columnHeaderCellSharyou"];
                    SharyouHeader.Location = new Point(122, 21);
                    var ShashuCdCell = newTemplate.Row.Cells["cellShashuCd"];
                    ShashuCdCell.Location = new Point(122, 0);
                    ShashuCdCell.TabIndex = 2;
                    var Unused1Cell = newTemplate.Row.Cells["cellUnused1"];
                    Unused1Cell.Location = new Point(182, 0);
                    var ShashuNameCell = newTemplate.Row.Cells["cellShashuName"];
                    ShashuNameCell.Location = new Point(122, 20);
                    ShashuNameCell.TabIndex = 3;
                    var SharyouCdCell = newTemplate.Row.Cells["cellSharyouCd"];
                    SharyouCdCell.Location = new Point(122, 48);
                    SharyouCdCell.TabIndex = 6;
                    var Unused2Cell = newTemplate.Row.Cells["cellUnused2"];
                    Unused2Cell.Location = new Point(182, 48);
                    var SharyouNameCell = newTemplate.Row.Cells["cellSharyouName"];
                    SharyouNameCell.Location = new Point(122, 76);
                    SharyouNameCell.TabIndex = 7;

                    // 非表示項目
                    var SaidaiWakusuuCell = newTemplate.Row.Cells["cellSaidaiWakusuu"];
                    SaidaiWakusuuCell.Location = new Point(122, 76);
                    var WorkClosedSharyouCell = newTemplate.Row.Cells["cellWorkClosedSharyou"];
                    WorkClosedSharyouCell.Location = new Point(124, 76);
                    var cellGyoushaCd = newTemplate.Row.Cells["cellGyoushaCd"];
                    cellGyoushaCd.Location = new Point(182, 76);
                    var cellGyoushaName = newTemplate.Row.Cells["cellGyoushaName"];
                    cellGyoushaName.Location = new Point(178, 76);
                    // 固定列は「車輌」
                    this.mrHaisha.FreezeLeftCellName = "cellShashuName";

                    // 地図表示ボタンを表示する分の位置調整
                    var meisaiHeader = newTemplate.ColumnHeaders[0].Cells["cellHeader01"];
                    meisaiHeader.Location = new Point(271, 0);
                    var HaishaShuruiCell = newTemplate.Row.Cells["cellHaishaShurui01"];
                    HaishaShuruiCell.Location = new Point(271, 0);
                    var HaishaSijishoStatusCell = newTemplate.Row.Cells["cellHaishaSijishoStatus01"];
                    HaishaSijishoStatusCell.Location = new Point(271, 20);
                    var DenpyouContentCell = newTemplate.Row.Cells["cellDenpyouContent01"];
                    DenpyouContentCell.Location = new Point(271, 40);
                    var GenchakuJikanCell = newTemplate.Row.Cells["cellGenchakuJikan01"];
                    GenchakuJikanCell.Location = new Point(291, 0);
                    var SagyouDateKubunCell = newTemplate.Row.Cells["cellSagyouDateKubun01"];
                    SagyouDateKubunCell.Location = new Point(291, 0);
                    var HaishaSijishoCheckBoxCell = newTemplate.Row.Cells["cellHaishaSijishoCheckBox01"];
                    HaishaSijishoCheckBoxCell.Location = new Point(336, 20);
                    var EmptyCell = newTemplate.Row.Cells["cellEmpty01"];
                    EmptyCell.Location = new Point(361, 20);
                    #endregion
                }
            }

            // 配車割当明細に反映
            this.mrHaisha.Template = newTemplate;
            this.mrHaisha.ResumeLayout();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 参照モード
        /// <summary>
        /// 参照モード用項目制御処理を行います
        /// </summary>
        private void SetReferenceMode()
        {
            /* MainForm */
            this.form.mrHaisha.ReadOnly = true;     // 配車一覧
            this.form.mrMihaisha.ReadOnly = true;   // 未配車一覧
            this.form.tbHaishaMemo.ReadOnly = true; // メモ
            /* FunctionButton */
            this.parentForm.bt_func1.Enabled = false;   // [F1]印刷全選択
            this.parentForm.bt_func2.Enabled = false;   // [F1]印刷全選択
            this.parentForm.bt_func3.Enabled = true;    // [F3]参照
            this.parentForm.bt_func4.Enabled = true;    // [F4]配車割当表 
            this.parentForm.bt_func5.Enabled = false;   // [F5]作業日変更
            this.parentForm.bt_func6.Enabled = false;   // [F6]一括確定
            this.parentForm.bt_func9.Enabled = false;   // [F9]登録
            this.parentForm.bt_func10.Enabled = false;  // [F10]行追加 
            this.parentForm.bt_func11.Enabled = false;  // [F11]行削除 

            this.form.btnSeiretsu.Enabled = false;
        }
        #endregion

        #endregion

        #region Function

        #region Functionボタン 押下処理
        #region  整列
        
        /// <summary>
        /// 整列
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void btnSeiretsu_Click(object sender, EventArgs e)
        {
            if (this.resultHaisha != null && this.resultHaisha.Rows.Count > 0)
            {
                if (this.changeHaishaFlg == true)
                {
                    this.msgLogic.MessageBoxShow("I020");
                    return;
                }
                var tmpDt = resultHaisha.Copy();
                if (this.IsShasyuSyaryou())
                {
                    tmpDt.DefaultView.Sort = "SHASYU_CD, SHARYOU_CD, GYOUSHA_CD, SHAIN_CD";
                }
                else
                {
                    tmpDt.DefaultView.Sort = "SHAIN_CD, SHASYU_CD, SHARYOU_CD, GYOUSHA_CD";
                }
                this.resultHaisha = tmpDt.DefaultView.ToTable();
                this.Bind();
            }
        }
        
        #endregion
        #region  F1 印刷選択
        /// <summary>
        /// F1 印刷選択
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func1_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.SetAllUketsuke((dr, n, isSS) =>
                    {
                        if (!dr.Field<string>("HAISHA_SHURUI" + n).Equals(ConstClass.HAISHA_SHURUI_KAKU)
                            || !dr.Field<string>("HAISHA_JOKYO" + n).Equals(ConstClass.HAISHA_JOKYO_CD_HAISHA)
                            || !dr.Field<string>("HAISHA_SIJISHO_STATUS" + n).Equals(ConstClass.HAISHA_SIJISHO_FALSE)) return;
                        dr.SetField<bool>("HAISHA_SIJISHO_CHECKED" + n, true);
                    }
                );
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region  F2 指示書

        /// <summary>
        /// F2 最適化
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func2_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                //this.SortHaisha();
                this.HaishaSijisho();

            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region  F3 参照
        /// <summary>
        /// F3 参照
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func3_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                if (this.selectedCell != null)
                {
                    //入力画面へ遷移する（参照モード）
                    forwardNyuuryoku(WINDOW_TYPE.REFERENCE_WINDOW_FLAG);
                }
                else
                {
                    //アラートを表示し、画面遷移しない
                    msgLogic.MessageBoxShow("E076");
                    return;
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        #region  F4 配車割当表
        
        /// <summary>
        /// F4 配車割当表
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func4_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                
                if (msgLogic.MessageBoxShow("C095") == DialogResult.No)
                {
                    return;
                }
                bool haishaFlg = true;
                var sharyouTable = this.dao_HAISHA.GetHaishaWariateData(this.dtoHaisha);
                if (sharyouTable == null || sharyouTable.Rows.Count == 0)
                {
                    sharyouTable = this.dao_HAISHA.GetHaishaSharyouData(this.dtoHaisha);
                    if (sharyouTable.Rows.Count > 0)
                    {
                        var tmpDt = sharyouTable.Copy();
                        if (this.IsShasyuSyaryou())
                        {
                            tmpDt.DefaultView.Sort = "SHASYU_CD, SHARYOU_CD, GYOUSHA_CD, SHAIN_CD";
                        }
                        else
                        {
                            tmpDt.DefaultView.Sort = "SHAIN_CD, SHASYU_CD, SHARYOU_CD, GYOUSHA_CD";
                        }
                        sharyouTable = tmpDt.DefaultView.ToTable();
                    }
                    haishaFlg = false;
                }
                var resultHaishaPrint = CreateHaishaTable(sharyouTable, haishaFlg);

                //this.sharyouCount = this.resultHaisha.Rows.Count;

                var resultDenpyo = this.dao_HAISHA.GetHaishaDenpyo(this.dtoHaisha);


                //this.HaishaCount = 0;

                foreach (DataRow denpyou in resultDenpyo.Rows)
                {
                    var gyoushaCd = denpyou["UNPAN_GYOUSHA_CD"].ToString();
                    var shashuCd = denpyou["SHASHU_CD"].ToString();
                    var sharyouCd = denpyou["SHARYOU_CD"].ToString();
                    var untenshaCd = denpyou["UNTENSHA_CD"].ToString();

                    // 配車先の行を検索
                    var haishaRow = resultHaishaPrint.AsEnumerable().Where(r => r["GYOUSHA_CD"].ToString() == gyoushaCd
                                                                             && r["SHASYU_CD"].ToString() == shashuCd
                                                                             && r["SHARYOU_CD"].ToString() == sharyouCd
                                                                             && r["SHAIN_CD"].ToString() == untenshaCd).FirstOrDefault();

                    if (null == haishaRow)
                    {
                        // 車種名を取得
                        var shashuDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
                        var mShashu = shashuDao.GetAllValidData(new M_SHASHU() { SHASHU_CD = shashuCd }).DefaultIfEmpty(new M_SHASHU()).FirstOrDefault();
                        // 車輌名を取得
                        var sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
                        var mSharyou = sharyouDao.GetAllValidData(new M_SHARYOU() { SHARYOU_CD = sharyouCd, GYOUSHA_CD = gyoushaCd }).DefaultIfEmpty(new M_SHARYOU()).FirstOrDefault();
                        // 社員名を取得
                        var mShainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
                        var mShain = mShainDao.GetAllValidData(new M_SHAIN() { SHAIN_CD = untenshaCd }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();
                        // 運搬業者名を取得
                        var mUnpanGyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                        var mUnpanGyousha = mUnpanGyoushaDao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = gyoushaCd }).DefaultIfEmpty(new M_GYOUSHA()).FirstOrDefault();
                        // 行がなければ、新規行を追加する
                        haishaRow = resultHaishaPrint.NewRow();
                        haishaRow["GYOUSHA_CD"] = gyoushaCd;
                        haishaRow["GYOUSHA_NAME_RYAKU"] = mUnpanGyousha.GYOUSHA_NAME_RYAKU;
                        haishaRow["SHASYU_CD"] = shashuCd;
                        haishaRow["SHASHU_NAME_RYAKU"] = mShashu.SHASHU_NAME_RYAKU;
                        haishaRow["SHARYOU_CD"] = sharyouCd;
                        haishaRow["SHARYOU_NAME_RYAKU"] = mSharyou.SHARYOU_NAME_RYAKU;
                        haishaRow["SHAIN_CD"] = untenshaCd;
                        haishaRow["SHAIN_NAME_RYAKU"] = mShain.SHAIN_NAME_RYAKU;
                        resultHaishaPrint.Rows.Add(haishaRow);

                    }
                    if (haishaRow != null)
                    {
                        int emptyCell = 0;

                        if ((this.sysInfoEntity != null && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1) || (denpyou["HAISHA_FLG"].ToString() == ConstClass.NO_HAISHA_FLG))
                        {
                            // 配車割当開始を取得。
                            int kijyunIndex = Int16.Parse(this.sysInfoEntity.HAISHA_WARIATE_KAISHI.ToString()) + 1;

                            for (int i = kijyunIndex; i <= ConstClass.DENPYOU_COUNT; i++)
                            {
                                // あいている配車のセルを探す
                                if (String.IsNullOrEmpty(haishaRow["DENPYOU_CONTENT" + GetSuffix(i)].ToString()) && !(bool)haishaRow["KARADENPYOU_FLG_" + GetSuffix(i)])
                                {
                                    emptyCell = i;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            emptyCell = int.Parse(denpyou["DENPYOU_SEQ"].ToString());
                        }
                        if (emptyCell != 0 && emptyCell <= ConstClass.DENPYOU_COUNT)
                        {
                            foreach (var di in ConstClass.DENPYOU_INFO)
                            {
                                var fn = di.FieldName;
                                if (fn != "ROW_NUM")
                                {
                                    haishaRow[fn + GetSuffix(emptyCell)] = denpyou[fn];
                                }
                            }

                            // 配車件数
                            //this.HaishaCount++;
                        }
                    }
                }

                if (resultHaishaPrint == null || resultHaishaPrint.Rows.Count == 0)
                {
                    return;
                }
                //20150720 hoanghm end edit

                int kijunResu = 0;
                if (sysInfoEntity != null && !sysInfoEntity.HAISHA_WARIATE_KAISHI.IsNull)
                {
                    kijunResu = sysInfoEntity.HAISHA_WARIATE_KAISHI.Value;
                }
                bool isShashuSaryou = this.IsShasyuSyaryou();
                var reportInfoR657 = new ReportInfoR657(resultHaishaPrint, kijunResu, (DateTime)this.form.dtpSagyouDate.Value, isShashuSaryou);

                reportInfoR657.Create(@".\Template\R657-Form.xml", "LAYOUT1", new DataTable());

                reportInfoR657.Title = "配車割当表";

                var formReport = new FormReportPrintPopup(reportInfoR657, "R657", WINDOW_ID.T_HAISHA_WARIATE_DAY);

                formReport.PrintInitAction = 2;

                //直接印刷
                formReport.PrintXPS();
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        
        #endregion
        #region  F5 作業日変
        /// <summary>
        /// F5 作業日変
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func5_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.ChangeSagyouDate();
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region  F6 一括確定
        /// <summary>
        /// F6 一括確定
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func6_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                bool[] errFlgSharyouCd = new bool[this.mrHaisha.Rows.Count];
                bool[] errFlgUntenshaCd = new bool[this.mrHaisha.Rows.Count];
                int rowIndex = -1;
                ClearInputError();
                this.SetAllUketsuke((dr, n, isSS, idx) =>
                    {
                        if (dr.Field<string>("SHARYOU_CD") != null && !dr.Field<string>("SHARYOU_CD").Equals(string.Empty)
                            && dr.Field<string>("SHAIN_CD") != null && !dr.Field<string>("SHAIN_CD").Equals(string.Empty))
                        {
                            // 車輌と運転者の双方に入力があれば一括確定する
                            dr.SetField<string>("HAISHA_SHURUI" + n, ConstClass.HAISHA_SHURUI_KAKU);
                        }
                        else
                        {
                            // 車輌CD又は運転者CDに入力が無い場合
                            if (dr.Field<string>("SHARYOU_CD") == null || dr.Field<string>("SHARYOU_CD").Equals(string.Empty))
                            {
                                // 車輌CD入力無し
                                errFlgSharyouCd[idx] = true;
                                dr.SetField<string>("HAISHA_SHURUI" + n, string.Empty);
                                if (rowIndex < 0) rowIndex = idx;
                            }
                            if (dr.Field<string>("SHAIN_CD") == null || dr.Field<string>("SHAIN_CD").Equals(string.Empty))
                            {
                                // 運転者CD入力無し
                                errFlgUntenshaCd[idx] = true;
                                dr.SetField<string>("HAISHA_SHURUI" + n, string.Empty);
                                if (rowIndex < 0) rowIndex = idx;
                            }
                        }
                    }
                );
                if (rowIndex >= 0)
                {

                    msgLogic.MessageBoxShow("E196");
                    SetNInputError(errFlgSharyouCd, errFlgUntenshaCd, rowIndex);
                }
                else
                {
                    this.changeHaishaFlg = true;
                }

            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 入力エラークリア
        /// </summary>
        private void ClearInputError()
        {
            foreach (Row row in this.mrHaisha.Rows)
            {
                var cellSharyouCd = (GcCustomTextBoxCell)row[CELL_NAME_SHARYOU_CD];
                cellSharyouCd.IsInputErrorOccured = false;
                var cellUntenshaCd = (GcCustomTextBoxCell)row[CELL_NAME_UNTENSHA_CD];
                cellUntenshaCd.IsInputErrorOccured = false;
            }
        }

        /// <summary>
        /// 入力無しエラー制御
        /// </summary>
        /// <param name="errFlgSharyouCd"></param>
        /// <param name="errFlgUntenshaCd"></param>
        /// <param name="rowIndex"></param>
        private void SetNInputError(bool[] errFlgSharyouCd, bool[] errFlgUntenshaCd, int rowIndex)
        {
            for (int i = 0; i < this.mrHaisha.Rows.Count; i++)
            {
                if (errFlgSharyouCd[i])
                {
                    // 車輌CD入力エラー
                    var cellSharyouCd = (GcCustomTextBoxCell)this.mrHaisha[i, CELL_NAME_SHARYOU_CD];
                    cellSharyouCd.IsInputErrorOccured = true;
                }
                if (errFlgUntenshaCd[i])
                {
                    // 運転者CD入力エラー
                    var cellUntenshaCd = (GcCustomTextBoxCell)this.mrHaisha[i, CELL_NAME_UNTENSHA_CD];
                    cellUntenshaCd.IsInputErrorOccured = true;
                }
            }
            this.mrHaisha.FirstDisplayedCellPosition = new GrapeCity.Win.MultiRow.CellPosition(rowIndex, 0);
        }
        #endregion

        #region  F8 検索
        /// <summary>
        /// F8 検索
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func8_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //this.form.mrHaisha.CellValidated-=new EventHandler<CellEventArgs>(this.form.mrHaisha_CellValidated);

                if (!this.CheckInpurtSearch())
                {
                    return;
                }
                else
                {
                    // 20141015 koukouei 休動管理機能 start
                    this.validatedFlag = false;
                    // 20141015 koukouei 休動管理機能 end
                    this.Search();
                    this.Bind();
                    this.form.searchConditionForShashuCd.Text = this.form.SHASHU_CD.Text;

                    //画面のペイントを禁止(行ジャンプ時に、明細がぶれるため)
                    SendMessage(this.form.Handle, WM_SETREDRAW, 0, IntPtr.Zero);

                    //ThangNguyen [Add] 20150724 Start
                    if (sysInfoEntity != null && !sysInfoEntity.HAISHA_WARIATE_KAISHI.IsNull)
                    {
                        this.ScrollHaishaDataGrid(sysInfoEntity.HAISHA_WARIATE_KAISHI.Value);
                    }
                    //ThangNguyen [Add] 20150724 End

                    //検索時も行ジャンプを有効にする。
                    this.JumpGridRowByCode();
                    //画面のペイントを出来るようにする
                    SendMessage(this.form.Handle, WM_SETREDRAW, 1, IntPtr.Zero);
                    //画面をリフレッシュする
                    this.form.Refresh();
                }
            }
            finally
            {
                this.selectedCell = null;
                this.form.mrHaisha.Refresh();
                this.form.mrMihaisha.Refresh();
                //this.form.mrHaisha.CellValidated += new EventHandler<CellEventArgs>(this.form.mrHaisha_CellValidated);
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region  F9 登録
        /// <summary>
        /// F9 登録
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func9_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                this.Regist(false);

                //20150727 hoanghm edit
                //// 車種にフォーカス設定
                //this.form.SHASHU_CD.Focus();
                if (this.form.RegistErrorFlag)
                {
                    if (!this.FocusErrorControl())
                    {
                        this.SetFocusInGridHaisha();
                    }
                }
                else
                {
                    if (this.form.SHASHU_CD.Visible)
                    {
                        // 車種にフォーカス設定
                        this.form.SHASHU_CD.Focus();
                    }
                    else
                    {
                        this.form.UNTENSHA_CD.Focus();
                    }
                }
                this.ClearRequiredSetting();
                //20150727 hoanghm end edit
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion
        #region  F10 行追加
        
        /// <summary>
        /// F10 行追加
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func10_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                if (this.mrHaisha.CurrentRow != null)
                {
                    this.form.mrHaisha.CellFormatting -= new System.EventHandler<GrapeCity.Win.MultiRow.CellFormattingEventArgs>(this.form.mrHaisha_CellFormatting);
                    if (this.mrHaisha.CurrentRow.Index >= 0 && this.mrHaisha.CurrentRow.Index < this.resultHaisha.Rows.Count)
                    {
                        DataRow row = this.resultHaisha.NewRow();
                        this.resultHaisha.Rows.InsertAt(row, this.mrHaisha.CurrentRow.Index);
                        this.changeHaishaFlg = true;
                    }
                    else if (this.mrHaisha.CurrentRow.Index == this.resultHaisha.Rows.Count)
                    {
                        DataRow row = this.resultHaisha.NewRow();
                        this.resultHaisha.Rows.Add(row);
                        this.changeHaishaFlg = true;
                    }
                    this.form.mrHaisha.CellFormatting += new System.EventHandler<GrapeCity.Win.MultiRow.CellFormattingEventArgs>(this.form.mrHaisha_CellFormatting);

                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        
        #endregion
        #region  F11 行削除
        /// <summary>
        /// F11 行削除
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func11_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);
                //this.HaishaSijisho();
                if (this.mrHaisha.CurrentRow != null)
                {
                    // 権限チェック

                    if (r_framework.Authority.Manager.CheckAuthority("G026", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        if (this.mrHaisha.CurrentRow.Index >= 0 && this.mrHaisha.CurrentRow.Index < this.resultHaisha.Rows.Count)
                        {
                            if (CheckDeleteRowHaisha(this.mrHaisha.CurrentRow.Index))
                            {
                                this.form.mrHaisha.CellFormatting -= new System.EventHandler<GrapeCity.Win.MultiRow.CellFormattingEventArgs>(this.form.mrHaisha_CellFormatting);
                                if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrHaisha.Name)
                                {
                                    if (this.mrHaisha.CurrentRow.Index == this.selectedCell.RowIndex)
                                    {
                                        this.selectedCell = null;
                                    }
                                }

                                this.DeleteRowHaisha(this.mrHaisha.CurrentRow.Index);
                                this.resultHaisha.Rows.RemoveAt(this.mrHaisha.CurrentRow.Index);
                                this.changeHaishaFlg = true;
                                
                                this.form.mrHaisha.CellFormatting += new System.EventHandler<GrapeCity.Win.MultiRow.CellFormattingEventArgs>(this.form.mrHaisha_CellFormatting);
                            }
                        }

                    }
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="rowIdx"></param>
       /// <returns></returns>
        private bool CheckDeleteRowHaisha(int rowIdx)
        {
            var drHaisha = this.resultHaisha.Rows[rowIdx];

            for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
            {
                string n = GetSuffix(i + 1);
                if (drHaisha.Field<short>("SHUBETSU_KBN_" + n) != ConstClass.SHUBETSU_KBN_EMPTY)
                {
                    var denpyouNumber = drHaisha.Field<long>("DENPYOU_NUM_" + n).ToString();
                    var shubetsu = drHaisha.Field<short>("SHUBETSU_KBN_" + n);

                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
                    {
                        if (!this.RenkeiCheck(denpyouNumber, "1", shubetsu))
                        {
                            return false;
                        }
                    }
                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                    {
                        if (!this.RenkeiCheck(denpyouNumber, "1", shubetsu))
                        {
                            return false;
                        }
                    }
                    else if (shubetsu == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
                    {
                        if (!this.RenkeiCheck(denpyouNumber, "0", shubetsu))
                        {
                            return false;
                        }
                    }
                }
            }

            for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
            {
                string n = GetSuffix(i + 1);
                if (drHaisha.Field<short>("SHUBETSU_KBN_" + n) != ConstClass.SHUBETSU_KBN_EMPTY)
                {
                    if (drHaisha.Field<string>("HAISHA_SHURUI" + n).Equals(ConstClass.HAISHA_SHURUI_KAKU))
                    {
                        this.msgLogic.MessageBoxShow("W007");
                        return false;
                    }
                }
            }
            for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
            {
                string n = GetSuffix(i + 1);
                if (drHaisha.Field<short>("SHUBETSU_KBN_" + n) != ConstClass.SHUBETSU_KBN_EMPTY && !String.IsNullOrEmpty(drHaisha["DENPYOU_NUM_" + n].ToString()))
                {
                    if (this.msgLogic.MessageBoxShow("C093") == DialogResult.Yes)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
                
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rowIdx"></param>
        private void DeleteRowHaisha(int rowIdx)
        {
            for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
            {
                string n = GetSuffix(i + 1);
                var drHaisha = this.resultHaisha.Rows[rowIdx];
                if (drHaisha.Field<short>("SHUBETSU_KBN_" + n) != ConstClass.SHUBETSU_KBN_EMPTY && !String.IsNullOrEmpty(drHaisha["DENPYOU_NUM_" + n].ToString()))
                {
                    var drMihaisha = this.resultMihaisha.NewRow();
                    foreach (var di in ConstClass.DENPYOU_INFO)
                    {
                        var fn = di.FieldName;
                        drMihaisha[fn] = drHaisha[fn + n];
                        drHaisha[fn + n] = di.DefaultValue;
                    }
                    this.resultMihaisha.Rows.Add(drMihaisha);
                    ++this.MihaishaCount;
                    --this.HaishaCount;
                }
            }
        }
        
        #endregion

        #region  F12 閉じる
        /// <summary>
        /// F12 閉じる
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Syste.EventArgs</param>
        private void bt_func12_Click(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // フォームを閉じる
                //this.form.Close();
                this.parentForm.Close();
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        #region Function処理実行

        #region 作業日変更
        /// <summary>
        /// 作業日変更
        /// </summary>
        private void ChangeSagyouDate()
        {
            LogUtility.DebugMethodStart();

            if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrHaisha.Name)
            {
                var drHaisha = this.resultHaisha.Rows[this.selectedCell.RowIndex];
                string suffix = this.GetColumn(this.selectedCell);
                if (drHaisha.Field<short>("SHUBETSU_KBN_" + suffix) != ConstClass.SHUBETSU_KBN_EMPTY)
                {

                    var denpyouNumber = drHaisha.Field<long>("DENPYOU_NUM_" + suffix).ToString();

                    //#158079 (INXS)作業日変更Check Start
                    if (AppConfig.AppOptions.IsInxsUketsuke()
                        && drHaisha.Field<short>("SHUBETSU_KBN_" + suffix) == ConstClass.SHUBETSU_KBN_UKETSUKE_SS
                        && !CheckChangeInxsUketsukeSagyouDate(denpyouNumber))
                    {
                        return;
                    }
                    //#158079 (INXS)作業日変更Check end

                    switch (drHaisha.Field<short>("SHUBETSU_KBN_" + suffix))
                    {
                        case ConstClass.SHUBETSU_KBN_TEIKI_HAISHA:
                            msgLogic.MessageBoxShow("E113");
                            break;
                        case ConstClass.SHUBETSU_KBN_UKETSUKE_SS:
                        case ConstClass.SHUBETSU_KBN_UKETSUKE_SK:
                            if (!this.RenkeiCheck(denpyouNumber, "1", drHaisha.Field<short>("SHUBETSU_KBN_" + suffix)))
                            {
                            }
                            else
                            {
                                if (drHaisha.Field<string>("HAISHA_SHURUI" + suffix) == ConstClass.HAISHA_SHURUI_KAKU)
                                {
                                    msgLogic.MessageBoxShow("E115");
                                }
                                else
                                {
                                    // No.3089-->
                                    string syasyuname = string.Empty;
                                    if (this.dtoHaisha.ShashuCd != string.Empty)
                                    {
                                        syasyuname = this.form.SHASHU_NAME.Text;
                                    }
                                    // No.3089<--
                                    string untenshaname = string.Empty;
                                    if (this.dtoHaisha.UntenshaCd != string.Empty)
                                    {
                                        untenshaname = this.form.UNTENSHA_NAME.Text;
                                    }
                                    var g027 = new Shougun.Core.Allocation.SagyoubiHenkou.G027();
                                    var f = g027.CreateForm(
                                        this.dtoHaisha.SagyouDate,
                                        this.dtoHaisha.KyotenCd,
                                        this.dtoHaisha.ShashuCd,
                                        //this.form.SHASHU_NAME.Text,   // No.3089
                                        syasyuname,                     // No.3089
                                        drHaisha,
                                        suffix,
                                        this.dtoHaisha.HaisyaKubun,
                                        this.sysInfoEntity,             // No.2544関連
                                        this.dtoHaisha.UntenshaCd,
                                        untenshaname
                                        );
                                    // 20141015 koukouei 休動管理機能 start
                                    this.form.SAGYOU_DATE.Value = this.dtoHaisha.SagyouDate;
                                    // 20141015 koukouei 休動管理機能 end
                                    if (f.ShowDialog() == DialogResult.OK)
                                    {
                                        //ThangNguyen [Update] 20150723 Start
                                        msgLogic.MessageBoxShow("I021");
                                        this.validatedFlag = false;
                                        this.selectedCell = null;
                                        this.Search();
                                        this.Bind();
                                        this.form.searchConditionForShashuCd.Text = this.form.SHASHU_CD.Text;
                                        if (sysInfoEntity != null && !sysInfoEntity.HAISHA_WARIATE_KAISHI.IsNull)
                                        {
                                            this.ScrollHaishaDataGrid(sysInfoEntity.HAISHA_WARIATE_KAISHI.Value);
                                        }
                                        //ThangNguyen [Update] 20150723 End
                                        
                                    }
                                }
                            }
                            break;
                    }
                }
            }
            else if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrMihaisha.Name)
            {
                msgLogic.MessageBoxShow("E114");
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 一括解除
        /// <summary>
        /// 一括解除
        /// </summary>
        /// <param name="f"></param>
        private void SetAllUketsuke(Action<DataRow, string, bool> f)
        {
            //LogUtility.DebugMethodStart(f);

            foreach (var dr in this.resultHaisha.AsEnumerable())
            {
                for (int i = 0; i < ConstClass.DENPYOU_COUNT; ++i)
                {
                    var n = GetSuffix(i + 1);
                    var shubetsu = dr.Field<short>("SHUBETSU_KBN_" + n);
                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS ||
                        shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                    {
                        f(dr, n, shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS);
                    }
                }
            }

            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 一括確定（上記一括解除メソッドをオーバーロード）
        /// </summary>
        /// <param name="f"></param>
        private void SetAllUketsuke(Action<DataRow, string, bool, int> f)
        {
            //LogUtility.DebugMethodStart(f);

            foreach (var dr in this.resultHaisha.AsEnumerable())
            {
                for (int i = 0; i < ConstClass.DENPYOU_COUNT; ++i)
                {
                    var n = GetSuffix(i + 1);
                    var shubetsu = dr.Field<short>("SHUBETSU_KBN_" + n);
                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS ||
                        shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                    {
                        f(dr, n, shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS, this.resultHaisha.Rows.IndexOf(dr));
                    }
                }
            }

            //LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 検索

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableSharyou"></param>
        /// <param name="haishaFlg"></param>
        /// <returns></returns>
        public DataTable CreateHaishaTable(DataTable tableSharyou, bool haishaFlg)
        {
            DataTable tableHaisha = new DataTable();
            foreach (var di in ConstClass.DENPYOU_HEADER_INFO)
            {
                tableHaisha.Columns.Add(di.FieldName, di.type);
                tableHaisha.Columns[di.FieldName].DefaultValue = di.DefaultValue;
            }
            for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
            {
                foreach (var di in ConstClass.DENPYOU_INFO)
                {
                    tableHaisha.Columns.Add(di.FieldName + GetSuffix(i + 1), di.type);
                    tableHaisha.Columns[di.FieldName + GetSuffix(i + 1)].DefaultValue = di.DefaultValue;

                }
            }
            foreach (DataRow row in tableSharyou.Rows)
            {
                DataRow rowHaisha = tableHaisha.NewRow();
                foreach (var di in ConstClass.DENPYOU_HEADER_INFO)
                {
                    var fn = di.FieldName;

                    rowHaisha[fn] = row[fn];
                }
                if (haishaFlg == true)
                {
                    for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
                    {
                        string suffix = GetSuffix(i + 1);
                        if (row["KARADENPYOU_FLG_" + suffix] != DBNull.Value)
                        {
                            rowHaisha["KARADENPYOU_FLG_" + suffix] = row["KARADENPYOU_FLG_" + suffix];
                        }
                    }
                }
                tableHaisha.Rows.Add(rowHaisha);
            }
            return tableHaisha;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            this.dtoHaisha.KyotenCd = this.header.KYOTEN_CD.Text;
            this.dtoHaisha.ShashuCd = this.form.SHASHU_CD.Text;
            this.dtoHaisha.SagyouDate = ((DateTime)this.form.dtpSagyouDate.Value).Date.ToString();
            this.dtoHaisha.ShoriKbn = this.form.tbShoriKbn.Text;
            this.dtoHaisha.UntenshaCd = this.form.UNTENSHA_CD.Text;

            this.form.SAGYOU_DATE.Value = this.dtoHaisha.SagyouDate;


            this.dtoHaisha.HaisyaKubun = IsShasyuSyaryou(); // No.2544

            bool haishaFlg = true;
            var sharyouTable = this.dao_HAISHA.GetHaishaWariateData(this.dtoHaisha);
            if (sharyouTable == null || sharyouTable.Rows.Count == 0)
            {
                sharyouTable = this.dao_HAISHA.GetHaishaSharyouData(this.dtoHaisha);
                if (sharyouTable.Rows.Count > 0)
                {
                    var tmpDt = sharyouTable.Copy();
                    if (this.IsShasyuSyaryou())
                    {
                        tmpDt.DefaultView.Sort = "SHASYU_CD, SHARYOU_CD, GYOUSHA_CD, SHAIN_CD";
                    }
                    else
                    {
                        tmpDt.DefaultView.Sort = "SHAIN_CD, SHASYU_CD, SHARYOU_CD, GYOUSHA_CD";
                    }
                    sharyouTable = tmpDt.DefaultView.ToTable();
                }
                haishaFlg = false;
            }
            this.resultHaisha = CreateHaishaTable(sharyouTable, haishaFlg);

            this.sharyouCount = this.resultHaisha.Rows.Count;

            var resultDenpyo = this.dao_HAISHA.GetHaishaDenpyo(this.dtoHaisha);


            this.HaishaCount = 0;

            foreach (DataRow denpyou in resultDenpyo.Rows)
            {
                var gyoushaCd = denpyou["UNPAN_GYOUSHA_CD"].ToString();
                var shashuCd = denpyou["SHASHU_CD"].ToString();
                var sharyouCd = denpyou["SHARYOU_CD"].ToString();
                var untenshaCd = denpyou["UNTENSHA_CD"].ToString();

                // 配車先の行を検索
                var haishaRow = this.resultHaisha.AsEnumerable().Where(r => r["GYOUSHA_CD"].ToString() == gyoushaCd
                                                                         && r["SHASYU_CD"].ToString() == shashuCd
                                                                         && r["SHARYOU_CD"].ToString() == sharyouCd
                                                                         && r["SHAIN_CD"].ToString() == untenshaCd).FirstOrDefault();

                if (null == haishaRow)
                {
                    // 車種名を取得
                    var shashuDao = DaoInitUtility.GetComponent<IM_SHASHUDao>();
                    var mShashu = shashuDao.GetAllValidData(new M_SHASHU() { SHASHU_CD = shashuCd }).DefaultIfEmpty(new M_SHASHU()).FirstOrDefault();
                    // 車輌名を取得
                    var sharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();
                    var mSharyou = sharyouDao.GetAllValidData(new M_SHARYOU() { SHARYOU_CD = sharyouCd, GYOUSHA_CD = gyoushaCd }).DefaultIfEmpty(new M_SHARYOU()).FirstOrDefault();
                    // 社員名を取得
                    var mShainDao = DaoInitUtility.GetComponent<IM_SHAINDao>();
                    var mShain = mShainDao.GetAllValidData(new M_SHAIN() { SHAIN_CD = untenshaCd }).DefaultIfEmpty(new M_SHAIN()).FirstOrDefault();
                    // 運搬業者名を取得
                    var mUnpanGyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
                    var mUnpanGyousha = mUnpanGyoushaDao.GetAllValidData(new M_GYOUSHA() { GYOUSHA_CD = gyoushaCd }).DefaultIfEmpty(new M_GYOUSHA()).FirstOrDefault();
                    // 行がなければ、新規行を追加する
                    haishaRow = this.resultHaisha.NewRow();
                    haishaRow["GYOUSHA_CD"] = gyoushaCd;
                    haishaRow["GYOUSHA_NAME_RYAKU"] = mUnpanGyousha.GYOUSHA_NAME_RYAKU;
                    haishaRow["SHASYU_CD"] = shashuCd;
                    haishaRow["SHASHU_NAME_RYAKU"] = mShashu.SHASHU_NAME_RYAKU;
                    haishaRow["SHARYOU_CD"] = sharyouCd;
                    haishaRow["SHARYOU_NAME_RYAKU"] = mSharyou.SHARYOU_NAME_RYAKU;
                    haishaRow["SHAIN_CD"] = untenshaCd;
                    haishaRow["SHAIN_NAME_RYAKU"] = mShain.SHAIN_NAME_RYAKU;
                    this.resultHaisha.Rows.Add(haishaRow);

                }
                if (haishaRow != null)
                {
                    int emptyCell = 0;

                    if ((this.sysInfoEntity != null && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1) || (denpyou["HAISHA_FLG"].ToString() == ConstClass.NO_HAISHA_FLG))
                    {
                        int tempWariateKaishi = this.sysInfoEntity.HAISHA_WARIATE_KAISHI.IsNull ? 0 : (int)this.sysInfoEntity.HAISHA_WARIATE_KAISHI;

                        for (int i = (tempWariateKaishi + 1); i <= ConstClass.DENPYOU_COUNT; i++)
                        {
                            // あいている配車のセルを探す
                            if (String.IsNullOrEmpty(haishaRow["DENPYOU_CONTENT" + GetSuffix(i)].ToString()) && !(bool)haishaRow["KARADENPYOU_FLG_" + GetSuffix(i)])
                            {
                                emptyCell = i;
                                break;
                            }
                        }
                    }
                    else
                    {
                        emptyCell = int.Parse(denpyou["DENPYOU_SEQ"].ToString());
                    }
                    if (emptyCell != 0 && emptyCell <= ConstClass.DENPYOU_COUNT)
                    {
                        foreach (var di in ConstClass.DENPYOU_INFO)
                        {
                            var fn = di.FieldName;
                            if (fn != "ROW_NUM")
                            {
                                haishaRow[fn + GetSuffix(emptyCell)] = denpyou[fn];
                            }
                        }

                        // 配車件数
                        this.HaishaCount++;
                    }
                }
            }

            this.resultMihaisha = this.dao_HAISHA.GetMihaisha(this.dtoHaisha);
            foreach (var di in ConstClass.DENPYOU_INFO)
            {
                var fn = di.FieldName;
                var colDenpyo = resultDenpyo.Columns[fn];
                var colMihaisha = this.resultMihaisha.Columns[fn];
                colMihaisha.MaxLength = colDenpyo.MaxLength;
            }

            this.resultMihaisha.DefaultView.Sort = "SORT_KEY2_, SORT_KEY4_, SORT_KEY5_, SORT_KEY6_";
            this.resultMihaisha = this.resultMihaisha.DefaultView.ToTable();

            this.MihaishaCount = this.resultMihaisha.Rows.Count;

            var hm = daoT_HAISHA_MEMO.GetData(this.dtoHaisha);
            this.form.tbHaishaMemo.Text = hm == null ? "" : hm.HAISHA_MEMO;
            this.form.tbHaishaMemo.Refresh();

            this.changeHaishaFlg = false;
            // 権限チェック
            if (r_framework.Authority.Manager.CheckAuthority("G026", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                // 修正権限が有る場合はボタン活性
                EnableButtons(true);
            }
            else
            {
                // 新規権限が無い場合は参照モード
                this.SetReferenceMode();
            }
            LogUtility.DebugMethodEnd(this.sharyouCount);

            return this.sharyouCount;
        }
        #endregion

        #region 登録
        /// <summary>
        /// 登録
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            if (!isRowDouble()) return;

            // 必須入力チェック
            this.SetRequiredSetting();
            int iNumControl = 4;
            Control[] aryCtrl = new Control[iNumControl];
            aryCtrl[0] = this.header.KYOTEN_CD;
            aryCtrl[1] = this.form.dtpSagyouDate;
            aryCtrl[2] = this.form.tbShoriKbn;
            aryCtrl[3] = this.mrHaisha;
            var autoCheckLogic = new AutoRegistCheckLogic(aryCtrl, aryCtrl);
            this.form.RegistErrorFlag = autoCheckLogic.AutoRegistCheck();
            if (this.form.RegistErrorFlag) return;
            if (this.SharyouUntenshaInputErrorCheck())
            {
                this.form.RegistErrorFlag = true;
            }
            if (this.form.RegistErrorFlag) return;
            // 20141015 koukouei 休動管理機能 start
            if (!ChkWorkClose())
            {
                return;
            }
            // 20141015 koukouei 休動管理機能 end

            #region Function Denpyou

            Func<string, short> GetHaishaShuruiCd = (haishaShurui) =>
                haishaShurui == ConstClass.HAISHA_SHURUI_KARI ? (short)2 :
                haishaShurui == ConstClass.HAISHA_SHURUI_KAKU ? (short)3 :
                (short)1;
            Func<string, string> GetHaishaShuruiName = (haishaShurui) =>
                haishaShurui == ConstClass.HAISHA_SHURUI_KARI ? "仮押" :
                haishaShurui == ConstClass.HAISHA_SHURUI_KAKU ? "確定" :
                "通常";
            Func<string, bool> GetHaishaSijishoFlg = (status) =>
                status == ConstClass.HAISHA_SIJISHO_TRUE;
            Func<string, bool> GetMailSendFlg = (status) =>
                status == ConstClass.MAIL_SEND_TRUE;

            Func<DataRow, string, bool> RegistUketsukeSS = (dr, n) =>
            {
                bool result = false;
                var sse = this.daoT_UKETSUKE_SS_ENTRY.GetData(this.dtoIdSeq);
                if (sse == null)
                {
                    // 更新対象データが取得できない場合は、排他エラーとみなして処理終了
                    result = true;
                    return result;
                }

                var createDate = sse.CREATE_DATE;
                var createUser = sse.CREATE_USER;
                var createPc = sse.CREATE_PC;

                sse.DELETE_FLG = true;
                new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(sse).SetSystemProperty(sse, true);
                this.daoT_UKETSUKE_SS_ENTRY.Update(sse);

                sse.DELETE_FLG = false;
                new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(sse).SetSystemProperty(sse, false);
                if (sse.SEQ > 0)
                {
                    sse.CREATE_DATE = createDate;
                    sse.CREATE_USER = createUser;
                    sse.CREATE_PC = createPc;
                }
                sse.SEQ = sse.SEQ + 1;
                // 車輌・運転者のどちらかが入力されていない場合は、配車状況を変更しない
                if (!String.IsNullOrEmpty(dr.Field<string>("SHARYOU_CD")) && !String.IsNullOrEmpty(dr.Field<string>("SHAIN_CD")))
                {
                    // 計上 or 回収なし の場合は配車状況を変更しない
                    if (ConstClass.HAISHA_JOKYO_CD_KEIJO != sse.HAISHA_JOKYO_CD.ToString() && ConstClass.HAISHA_JOKYO_CD_NASHI != sse.HAISHA_JOKYO_CD.ToString())
                    {
                        sse.HAISHA_JOKYO_CD = 2;
                        sse.HAISHA_JOKYO_NAME = "配車";
                    }
                }
                var haishaShurui = dr.Field<string>("HAISHA_SHURUI" + n);
                sse.HAISHA_SHURUI_CD = GetHaishaShuruiCd(haishaShurui);
                sse.HAISHA_SHURUI_NAME = GetHaishaShuruiName(haishaShurui);
                sse.SAGYOU_DATE = this.dtoHaisha.SagyouDate;
                sse.SHARYOU_CD = dr.Field<string>("SHARYOU_CD");
                sse.SHARYOU_NAME = dr.Field<string>("SHARYOU_NAME_RYAKU");
                sse.SHASHU_CD = dr.Field<string>("SHASYU_CD");
                sse.SHASHU_NAME = dr.Field<string>("SHASHU_NAME_RYAKU");
                sse.UNTENSHA_CD = dr.Field<string>("SHAIN_CD");
                sse.UNTENSHA_NAME = dr.Field<string>("SHAIN_NAME_RYAKU");
                sse.HAISHA_SIJISHO_FLG = GetHaishaSijishoFlg(dr.Field<string>("HAISHA_SIJISHO_STATUS" + n));
                sse.MAIL_SEND_FLG = GetMailSendFlg(dr.Field<string>("MAIL_SEND_STATUS" + n));
                sse.UNPAN_GYOUSHA_CD = dr.Field<string>("GYOUSHA_CD");
                sse.UNPAN_GYOUSHA_NAME = dr.Field<string>("GYOUSHA_NAME_RYAKU");
                this.daoT_UKETSUKE_SS_ENTRY.Insert(sse);

                var ssdArray = this.daoT_UKETSUKE_SS_DETAIL.GetData(this.dtoIdSeq);
                foreach (var ssd in ssdArray)
                {
                    new DataBinderLogic<T_UKETSUKE_SS_DETAIL>(ssd).SetSystemProperty(ssd, false);
                    ssd.SEQ = sse.SEQ;
                    this.daoT_UKETSUKE_SS_DETAIL.Insert(ssd);
                }

                var crArray = this.daoT_CONTENA_RESERVE.GetData(this.dtoIdSeq);
                foreach (var cr in crArray)
                {
                    createDate = cr.CREATE_DATE;
                    createUser = cr.CREATE_USER;
                    createPc = cr.CREATE_PC;

                    cr.DELETE_FLG = true;
                    new DataBinderLogic<T_CONTENA_RESERVE>(cr).SetSystemProperty(cr, true);
                    this.daoT_CONTENA_RESERVE.Update(cr);

                    cr.DELETE_FLG = false;
                    new DataBinderLogic<T_CONTENA_RESERVE>(cr).SetSystemProperty(cr, false);
                    if (cr.SEQ > 0)
                    {
                        cr.CREATE_DATE = createDate;
                        cr.CREATE_USER = createUser;
                        cr.CREATE_PC = createPc;
                    }
                    cr.SEQ = sse.SEQ;
                    this.daoT_CONTENA_RESERVE.Insert(cr);
                }
                return result;
            };

            Func<DataRow, bool> RegistUketsukeSS_Mihaisha = (dr) =>
            {
                bool result = false;

                var sse = this.daoT_UKETSUKE_SS_ENTRY.GetData(this.dtoIdSeq);
                if (sse == null)
                {
                    // 更新対象データが取得できない場合は、排他エラーとみなして処理終了
                    result = true;
                    return result;
                }

                var createDate = sse.CREATE_DATE;
                var createUser = sse.CREATE_USER;
                var createPc = sse.CREATE_PC;

                sse.DELETE_FLG = true;
                new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(sse).SetSystemProperty(sse, true);
                this.daoT_UKETSUKE_SS_ENTRY.Update(sse);

                sse.DELETE_FLG = false;
                new DataBinderLogic<T_UKETSUKE_SS_ENTRY>(sse).SetSystemProperty(sse, false);
                if (sse.SEQ > 0)
                {
                    sse.CREATE_DATE = createDate;
                    sse.CREATE_USER = createUser;
                    sse.CREATE_PC = createPc;
                }
                sse.SEQ = sse.SEQ + 1;

                // 計上 or 回収なし の場合は配車状況を変更しない
                if (ConstClass.HAISHA_JOKYO_CD_KEIJO != sse.HAISHA_JOKYO_CD.ToString() && ConstClass.HAISHA_JOKYO_CD_NASHI != sse.HAISHA_JOKYO_CD.ToString())
                {
                    sse.HAISHA_JOKYO_CD = SqlInt16.Parse(ConstClass.HAISHA_JOKYO_CD_JUCHU);
                    sse.HAISHA_JOKYO_NAME = ConstClass.HAISHA_JOKYO_NAME_JUCHU;
                }
                var haishaShurui = dr.Field<string>("HAISHA_SHURUI");
                sse.HAISHA_SHURUI_CD = GetHaishaShuruiCd(haishaShurui);
                sse.HAISHA_SHURUI_NAME = GetHaishaShuruiName(haishaShurui);
                sse.HAISHA_SIJISHO_FLG = GetHaishaSijishoFlg(dr.Field<string>("HAISHA_SIJISHO_STATUS"));
                sse.MAIL_SEND_FLG = GetMailSendFlg(dr.Field<string>("MAIL_SEND_STATUS"));
                sse.UNPAN_GYOUSHA_CD = dr.Field<string>("GYOUSHA_CD");
                sse.UNPAN_GYOUSHA_NAME = dr.Field<string>("GYOUSHA_NAME_RYAKU");
                // 車輌情報を未登録にする
                sse.SHASHU_CD = null;
                sse.SHASHU_NAME = null;
                sse.SHARYOU_CD = null;
                sse.SHARYOU_NAME = null;
                sse.UNTENSHA_CD = null;
                sse.UNTENSHA_NAME = null;
                this.daoT_UKETSUKE_SS_ENTRY.Insert(sse);

                var ssdArray = this.daoT_UKETSUKE_SS_DETAIL.GetData(this.dtoIdSeq);
                foreach (var ssd in ssdArray)
                {
                    new DataBinderLogic<T_UKETSUKE_SS_DETAIL>(ssd).SetSystemProperty(ssd, false);
                    ssd.SEQ = sse.SEQ;
                    this.daoT_UKETSUKE_SS_DETAIL.Insert(ssd);
                }

                var crArray = this.daoT_CONTENA_RESERVE.GetData(this.dtoIdSeq);
                foreach (var cr in crArray)
                {
                    createDate = cr.CREATE_DATE;
                    createUser = cr.CREATE_USER;
                    createPc = cr.CREATE_PC;

                    cr.DELETE_FLG = true;
                    new DataBinderLogic<T_CONTENA_RESERVE>(cr).SetSystemProperty(cr, true);
                    this.daoT_CONTENA_RESERVE.Update(cr);

                    cr.DELETE_FLG = false;
                    new DataBinderLogic<T_CONTENA_RESERVE>(cr).SetSystemProperty(cr, false);
                    if (cr.SEQ > 0)
                    {
                        cr.CREATE_DATE = createDate;
                        cr.CREATE_USER = createUser;
                        cr.CREATE_PC = createPc;
                    }
                    cr.SEQ = sse.SEQ;
                    this.daoT_CONTENA_RESERVE.Insert(cr);
                }
                return result;
            };

            Func<DataRow, string, bool> RegistUketsukeSK = (dr, n) =>
            {
                bool result = false;
                var ske = this.daoT_UKETSUKE_SK_ENTRY.GetData(this.dtoIdSeq);
                if (ske == null)
                {
                    // 更新対象データが取得できない場合は、排他エラーとみなして処理終了
                    result = true;
                    return result;
                }

                var createDate = ske.CREATE_DATE;
                var createUser = ske.CREATE_USER;
                var createPc = ske.CREATE_PC;

                ske.DELETE_FLG = true;
                new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(ske).SetSystemProperty(ske, true);
                this.daoT_UKETSUKE_SK_ENTRY.Update(ske);

                ske.DELETE_FLG = false;
                new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(ske).SetSystemProperty(ske, false);
                if (ske.SEQ > 0)
                {
                    ske.CREATE_DATE = createDate;
                    ske.CREATE_USER = createUser;
                    ske.CREATE_PC = createPc;
                }
                ske.SEQ = ske.SEQ + 1;
                // 車輌・運転者のどちらかが入力されていない場合は、配車状況を変更しない
                if (!String.IsNullOrEmpty(dr.Field<string>("SHARYOU_CD")) && !String.IsNullOrEmpty(dr.Field<string>("SHAIN_CD")))
                {
                    // 計上 or 回収なし の場合は配車状況を変更しない
                    if (ConstClass.HAISHA_JOKYO_CD_KEIJO != ske.HAISHA_JOKYO_CD.ToString() && ConstClass.HAISHA_JOKYO_CD_NASHI != ske.HAISHA_JOKYO_CD.ToString())
                    {
                        ske.HAISHA_JOKYO_CD = 2;
                        ske.HAISHA_JOKYO_NAME = "配車";
                    }
                }
                var haishaShurui = dr.Field<string>("HAISHA_SHURUI" + n);
                ske.HAISHA_SHURUI_CD = GetHaishaShuruiCd(haishaShurui);
                ske.HAISHA_SHURUI_NAME = GetHaishaShuruiName(haishaShurui);
                ske.SAGYOU_DATE = this.dtoHaisha.SagyouDate;
                ske.SHARYOU_CD = dr.Field<string>("SHARYOU_CD");
                ske.SHARYOU_NAME = dr.Field<string>("SHARYOU_NAME_RYAKU");
                ske.SHASHU_CD = dr.Field<string>("SHASYU_CD");
                ske.SHASHU_NAME = dr.Field<string>("SHASHU_NAME_RYAKU");
                ske.UNTENSHA_CD = dr.Field<string>("SHAIN_CD");
                ske.UNTENSHA_NAME = dr.Field<string>("SHAIN_NAME_RYAKU");
                ske.HAISHA_SIJISHO_FLG = GetHaishaSijishoFlg(dr.Field<string>("HAISHA_SIJISHO_STATUS" + n));
                ske.MAIL_SEND_FLG = GetMailSendFlg(dr.Field<string>("MAIL_SEND_STATUS" + n));
                ske.UNPAN_GYOUSHA_CD = dr.Field<string>("GYOUSHA_CD");
                ske.UNPAN_GYOUSHA_NAME = dr.Field<string>("GYOUSHA_NAME_RYAKU");
                this.daoT_UKETSUKE_SK_ENTRY.Insert(ske);

                var skdArray = this.daoT_UKETSUKE_SK_DETAIL.GetData(this.dtoIdSeq);
                foreach (var skd in skdArray)
                {
                    new DataBinderLogic<T_UKETSUKE_SK_DETAIL>(skd).SetSystemProperty(skd, false);
                    skd.SEQ = ske.SEQ;
                    this.daoT_UKETSUKE_SK_DETAIL.Insert(skd);
                }
                return result;
            };

            Func<DataRow, bool> RegistUketsukeSK_Mihaisha = (dr) =>
            {
                bool result = false;
                var ske = this.daoT_UKETSUKE_SK_ENTRY.GetData(this.dtoIdSeq);
                if (ske == null)
                {
                    // 更新対象データが取得できない場合は、排他エラーとみなして処理終了
                    result = true;
                    return result;
                }

                var createDate = ske.CREATE_DATE;
                var createUser = ske.CREATE_USER;
                var createPc = ske.CREATE_PC;

                ske.DELETE_FLG = true;
                new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(ske).SetSystemProperty(ske, true);
                this.daoT_UKETSUKE_SK_ENTRY.Update(ske);

                ske.DELETE_FLG = false;
                new DataBinderLogic<T_UKETSUKE_SK_ENTRY>(ske).SetSystemProperty(ske, false);
                if (ske.SEQ > 0)
                {
                    ske.CREATE_DATE = createDate;
                    ske.CREATE_USER = createUser;
                    ske.CREATE_PC = createPc;
                }
                ske.SEQ = ske.SEQ + 1;
                // 計上 or 回収なし の場合は配車状況を変更しない
                if (ConstClass.HAISHA_JOKYO_CD_KEIJO != ske.HAISHA_JOKYO_CD.ToString() && ConstClass.HAISHA_JOKYO_CD_NASHI != ske.HAISHA_JOKYO_CD.ToString())
                {
                    ske.HAISHA_JOKYO_CD = SqlInt16.Parse(ConstClass.HAISHA_JOKYO_CD_JUCHU);
                    ske.HAISHA_JOKYO_NAME = ConstClass.HAISHA_JOKYO_NAME_JUCHU;
                }
                var haishaShurui = dr.Field<string>("HAISHA_SHURUI");
                ske.HAISHA_SHURUI_CD = GetHaishaShuruiCd(haishaShurui);
                ske.HAISHA_SHURUI_NAME = GetHaishaShuruiName(haishaShurui);
                ske.HAISHA_SIJISHO_FLG = GetHaishaSijishoFlg(dr.Field<string>("HAISHA_SIJISHO_STATUS"));
                ske.MAIL_SEND_FLG = GetMailSendFlg(dr.Field<string>("MAIL_SEND_STATUS"));
                ske.UNPAN_GYOUSHA_CD = dr.Field<string>("GYOUSHA_CD");
                ske.UNPAN_GYOUSHA_NAME = dr.Field<string>("GYOUSHA_NAME_RYAKU");
                // 車輌情報を未登録にする
                ske.SHASHU_CD = null;
                ske.SHASHU_NAME = null;
                ske.SHARYOU_CD = null;
                ske.SHARYOU_NAME = null;
                ske.UNTENSHA_CD = null;
                ske.UNTENSHA_NAME = null;
                this.daoT_UKETSUKE_SK_ENTRY.Insert(ske);

                var skdArray = this.daoT_UKETSUKE_SK_DETAIL.GetData(this.dtoIdSeq);
                foreach (var skd in skdArray)
                {
                    new DataBinderLogic<T_UKETSUKE_SK_DETAIL>(skd).SetSystemProperty(skd, false);
                    skd.SEQ = ske.SEQ;
                    this.daoT_UKETSUKE_SK_DETAIL.Insert(skd);
                }
                return result;
            };

            Func<DataRow, string, bool> RegistTeikiHaisha = (dr, n) =>
            {
                bool result = false;
                var tke = this.daoT_TEIKI_HAISHA_ENTRY.GetData(this.dtoIdSeq);
                if (tke == null)
                {
                    // 更新対象データが取得できない場合は、排他エラーとみなして処理終了
                    result = true;
                    return result;
                }

                var createDate = tke.CREATE_DATE;
                var createUser = tke.CREATE_USER;
                var createPc = tke.CREATE_PC;

                tke.DELETE_FLG = true;
                new DataBinderLogic<T_TEIKI_HAISHA_ENTRY>(tke).SetSystemProperty(tke, true);
                this.daoT_TEIKI_HAISHA_ENTRY.Update(tke);

                tke.DELETE_FLG = false;
                new DataBinderLogic<T_TEIKI_HAISHA_ENTRY>(tke).SetSystemProperty(tke, false);
                if (tke.SEQ > 0)
                {
                    tke.CREATE_DATE = createDate;
                    tke.CREATE_USER = createUser;
                    tke.CREATE_PC = createPc;
                }
                tke.SEQ = tke.SEQ + 1;
                tke.SAGYOU_DATE = DateTime.Parse(this.dtoHaisha.SagyouDate);
                tke.SHARYOU_CD = dr.Field<string>("SHARYOU_CD");
                tke.SHASHU_CD = dr.Field<string>("SHASYU_CD");
                tke.UNTENSHA_CD = dr.Field<string>("SHAIN_CD");
                tke.UNPAN_GYOUSHA_CD = dr.Field<string>("GYOUSHA_CD");
                this.daoT_TEIKI_HAISHA_ENTRY.Insert(tke);

                var tkdArray = this.daoT_TEIKI_HAISHA_DETAIL.GetData(this.dtoIdSeq);
                foreach (var tkd in tkdArray)
                {
                    new DataBinderLogic<T_TEIKI_HAISHA_DETAIL>(tkd).SetSystemProperty(tkd, false);
                    tkd.SEQ = tke.SEQ;
                    this.daoT_TEIKI_HAISHA_DETAIL.Insert(tkd);
                }

                var tknArray = this.daoT_TEIKI_HAISHA_NIOROSHI.GetData(this.dtoIdSeq);
                foreach (var tkn in tknArray)
                {
                    new DataBinderLogic<T_TEIKI_HAISHA_NIOROSHI>(tkn).SetSystemProperty(tkn, false);
                    tkn.SEQ = tke.SEQ;
                    this.daoT_TEIKI_HAISHA_NIOROSHI.Insert(tkn);
                }

                var tksArray = this.daoT_TEIKI_HAISHA_SHOUSAI.GetData(this.dtoIdSeq);
                foreach (var tks in tksArray)
                {
                    new DataBinderLogic<T_TEIKI_HAISHA_SHOUSAI>(tks).SetSystemProperty(tks, false);
                    tks.SEQ = tke.SEQ;
                    this.daoT_TEIKI_HAISHA_SHOUSAI.Insert(tks);
                }

                return result;
            };

            Func<DataRow, bool> RegistTeikiHaisha_Mihaisha = (dr) =>
            {
                bool result = false;
                var tke = this.daoT_TEIKI_HAISHA_ENTRY.GetData(this.dtoIdSeq);
                if (tke == null)
                {
                    // 更新対象データが取得できない場合は、排他エラーとみなして処理終了
                    result = true;
                    return result;
                }

                var createDate = tke.CREATE_DATE;
                var createUser = tke.CREATE_USER;
                var createPc = tke.CREATE_PC;

                tke.DELETE_FLG = true;
                new DataBinderLogic<T_TEIKI_HAISHA_ENTRY>(tke).SetSystemProperty(tke, true);
                this.daoT_TEIKI_HAISHA_ENTRY.Update(tke);

                tke.DELETE_FLG = false;
                new DataBinderLogic<T_TEIKI_HAISHA_ENTRY>(tke).SetSystemProperty(tke, false);
                if (tke.SEQ > 0)
                {
                    tke.CREATE_DATE = createDate;
                    tke.CREATE_USER = createUser;
                    tke.CREATE_PC = createPc;
                }
                tke.SEQ = tke.SEQ + 1;
                tke.SAGYOU_DATE = DateTime.Parse(this.dtoHaisha.SagyouDate);
                // 車輌情報を未登録にする
                tke.SHASHU_CD = string.Empty;
                tke.SHARYOU_CD = string.Empty;
                tke.UNPAN_GYOUSHA_CD = string.Empty;
                tke.UNTENSHA_CD = string.Empty;
                this.daoT_TEIKI_HAISHA_ENTRY.Insert(tke);

                var tkdArray = this.daoT_TEIKI_HAISHA_DETAIL.GetData(this.dtoIdSeq);
                foreach (var tkd in tkdArray)
                {
                    new DataBinderLogic<T_TEIKI_HAISHA_DETAIL>(tkd).SetSystemProperty(tkd, false);
                    tkd.SEQ = tke.SEQ;
                    this.daoT_TEIKI_HAISHA_DETAIL.Insert(tkd);
                }

                var tknArray = this.daoT_TEIKI_HAISHA_NIOROSHI.GetData(this.dtoIdSeq);
                foreach (var tkn in tknArray)
                {
                    new DataBinderLogic<T_TEIKI_HAISHA_NIOROSHI>(tkn).SetSystemProperty(tkn, false);
                    tkn.SEQ = tke.SEQ;
                    this.daoT_TEIKI_HAISHA_NIOROSHI.Insert(tkn);
                }

                var tksArray = this.daoT_TEIKI_HAISHA_SHOUSAI.GetData(this.dtoIdSeq);
                foreach (var tks in tksArray)
                {
                    new DataBinderLogic<T_TEIKI_HAISHA_SHOUSAI>(tks).SetSystemProperty(tks, false);
                    tks.SEQ = tke.SEQ;
                    this.daoT_TEIKI_HAISHA_SHOUSAI.Insert(tks);
                }
                return result;
            };
            #endregion Function Denpyou

            System.Action DeleteWariateDay = () =>
            {
                // 作業日で登録済の配車割当（一日）を検索
                var wdArray = this.dao_HAISHA.GetHaishaWariateDay(this.dtoHaisha);
                if (wdArray.Any())
                {
                    foreach (T_HAISHA_WARIATE_DAY wd in wdArray)
                    {
                        // 登録済の配車割当（一日）を削除
                        wd.DELETE_FLG = true;
                        //new DataBinderLogic<T_HAISHA_WARIATE_DAY>(wd).SetSystemProperty(wd, true);
                        this.dao_HAISHA.Update(wd);
                    }
                }
            };

            Action<DataRow, int> RegistWariateDay = (dr, rowNum) =>
            {
                this.dtoHaisha.ShainCd = dr.Field<string>("SHAIN_CD");
                this.dtoHaisha.SharyouCd = dr.Field<string>("SHARYOU_CD");
                this.dtoHaisha.GyoushaCd = dr.Field<string>("GYOUSHA_CD");
                if (this.dtoHaisha.ShainCd == null)
                {
                    this.dtoHaisha.ShainCd = string.Empty;
                }
                if (this.dtoHaisha.SharyouCd == null)
                {
                    this.dtoHaisha.SharyouCd = string.Empty;
                }
                if (this.dtoHaisha.GyoushaCd == null)
                {
                    this.dtoHaisha.GyoushaCd = string.Empty;
                }
                var wdArray = this.dao_HAISHA.GetData(this.dtoHaisha);
                T_HAISHA_WARIATE_DAY wd = new T_HAISHA_WARIATE_DAY();
                if (wdArray.Any())
                {
                    // 過去に登録されている組み合わせの場合は、SEQ+1
                    wd = wdArray[0];
                    wd.SEQ = wd.SEQ + 1;
                }
                else
                {
                    // 新しい組み合わせの場合は、SEQ=1
                    wd.SAGYOU_DATE = DateTime.Parse(this.dtoHaisha.SagyouDate);
                    wd.SEARCH_SAGYOU_DATE = this.dtoHaisha.SagyouDate;
                    wd.SHARYOU_CD = this.dtoHaisha.SharyouCd;
                    wd.GYOUSHA_CD = this.dtoHaisha.GyoushaCd;
                    wd.UNTENSHA_CD = this.dtoHaisha.ShainCd;
                    wd.SEQ = 1;
                }
                wd.ROW_NUM = rowNum;
                wd.SHASHU_CD = dr.Field<string>("SHASYU_CD");

                wd.DELETE_FLG = false;
                var createDate = wd.CREATE_DATE;
                var createUser = wd.CREATE_USER;
                var createPc = wd.CREATE_PC;
                new DataBinderLogic<T_HAISHA_WARIATE_DAY>(wd).SetSystemProperty(wd, false);
                if (!string.IsNullOrEmpty(createUser))
                {
                    wd.CREATE_DATE = createDate;
                    wd.CREATE_USER = createUser;
                    wd.CREATE_PC = createPc;
                }

                var type = wd.GetType();
                for (int i = 0; i < ConstClass.DENPYOU_COUNT; ++i)
                {
                    var n = GetSuffix(i + 1);
                    var shubetsuKbn = new SqlInt16((short)dr["SHUBETSU_KBN_" + n]);
                    var systemId = new SqlInt64((long)dr["SYSTEM_ID_" + n]);
                    var denpyouNum = new SqlInt64((long)dr["DENPYOU_NUM_" + n]);
                    var karaDenpyouFlg = new SqlBoolean((bool)dr["KARADENPYOU_FLG_" + n]);
                    if (shubetsuKbn == ConstClass.SHUBETSU_KBN_EMPTY)
                    {
                        shubetsuKbn = SqlInt16.Null;
                        systemId = denpyouNum = SqlInt64.Null;
                    }

                    // wd.SHUBETSU_KBN_01~30 = shubetsuKbn
                    type.GetProperty("SHUBETSU_KBN_" + n).SetValue(wd, shubetsuKbn, null);
                    // wd.SYSTEM_ID_01~30 = systemId
                    type.GetProperty("SYSTEM_ID_" + n).SetValue(wd, systemId, null);
                    // wd.DENPYOU_NUM_01~30 = denpyouNum
                    type.GetProperty("DENPYOU_NUM_" + n).SetValue(wd, denpyouNum, null);
                    // wd.KARADENPYOU_FLG_01~30 = karaDenpyouFlg
                    type.GetProperty("KARADENPYOU_FLG_" + n).SetValue(wd, karaDenpyouFlg, null);
                }

                this.dao_HAISHA.Insert(wd);
            };

            System.Action RegistHaishaMemo = () =>
            {
                var hm = daoT_HAISHA_MEMO.GetData(this.dtoHaisha);
                if (hm != null)
                {
                    hm.DELETE_FLG = true;
                    new DataBinderLogic<T_HAISHA_MEMO>(hm).SetSystemProperty(hm, false);
                    this.daoT_HAISHA_MEMO.Update(hm);
                    hm.SEQ = hm.SEQ + 1;
                }
                else
                {
                    hm = new T_HAISHA_MEMO();
                    hm.SYSTEM_ID = daoT_HAISHA_MEMO.GetNextSystemId();
                    hm.SEQ = 1;
                    hm.SAGYOU_DATE = DateTime.Parse(this.dtoHaisha.SagyouDate);
                    hm.HAISHA_MEMO = "";
                }

                hm.DELETE_FLG = false;
                hm.HAISHA_MEMO = this.form.tbHaishaMemo.Text;
                new DataBinderLogic<T_HAISHA_MEMO>(hm).SetSystemProperty(hm, true);
                this.daoT_HAISHA_MEMO.Insert(hm);
            };

            try
            {
                bool isUpdated = false; // 更新対象データが更新されているか #100045
                using (var tran = new Transaction())
                {
                    DeleteWariateDay();

                    for (int r = 0; r < this.resultHaisha.Rows.Count; ++r)
                    {
                        var dr = this.resultHaisha.Rows[r];
                        for (int c = 0; c < ConstClass.DENPYOU_COUNT; ++c)
                        {
                            var n = GetSuffix(c + 1);
                            if (dr.Field<short>("SHUBETSU_KBN_" + n) != ConstClass.SHUBETSU_KBN_EMPTY)
                            {
                                //bool rowChanged = dr.Field<long>("ROW_NUM" + n) != r + 1;
                                this.dtoIdSeq.SystemId = dr.Field<long>("SYSTEM_ID_" + n);
                                this.dtoIdSeq.Seq = dr.Field<int>("SEQ" + n);
                                switch (dr.Field<short>("SHUBETSU_KBN_" + n))
                                {
                                    case ConstClass.SHUBETSU_KBN_UKETSUKE_SS:
                                        isUpdated = RegistUketsukeSS(dr, n);

                                        break;
                                    case ConstClass.SHUBETSU_KBN_UKETSUKE_SK:
                                        isUpdated = RegistUketsukeSK(dr, n);

                                        break;
                                    case ConstClass.SHUBETSU_KBN_TEIKI_HAISHA:
                                        isUpdated = RegistTeikiHaisha(dr, n);
                                        break;
                                }

                                if (isUpdated)
                                {
                                    break;
                                }
                            }
                        }

                        if (isUpdated)
                        {
                            break;
                        }
                        //RegistWariateDay(dr);
                        RegistWariateDay(dr, r);
                    }

                    if (!isUpdated)
                    {
                        for (int r = 0; r < this.resultMihaisha.Rows.Count; ++r)
                        {
                            var dr = this.resultMihaisha.Rows[r];
                            this.dtoIdSeq.SystemId = dr.Field<long>("SYSTEM_ID_");
                            this.dtoIdSeq.Seq = dr.Field<int>("SEQ");
                            switch (dr.Field<short>("SHUBETSU_KBN_"))
                            {
                                case ConstClass.SHUBETSU_KBN_UKETSUKE_SS:
                                    isUpdated = RegistUketsukeSS_Mihaisha(dr);
                                    break;
                                case ConstClass.SHUBETSU_KBN_UKETSUKE_SK:
                                    isUpdated = RegistUketsukeSK_Mihaisha(dr);
                                    break;
                                case ConstClass.SHUBETSU_KBN_TEIKI_HAISHA:
                                    isUpdated = RegistTeikiHaisha_Mihaisha(dr);
                                    break;
                                default:
                                    // Nothing
                                    break;
                            }

                            if (isUpdated)
                            {
                                break;
                            }
                        }
                    }

                    if (!isUpdated)
                    {
                        RegistHaishaMemo();

                        tran.Commit();
                    }
                }

                if (isUpdated)
                {
                    // 更新対象データが取得できない場合は、排他エラーとみなして処理終了
                    this.msgLogic.MessageBoxShow("E080");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("I001", "登録");
                    this.Search();
                    this.Bind();
                    //ThangNguyen [Add] 20150724 Start
                    if (sysInfoEntity != null && !sysInfoEntity.HAISHA_WARIATE_KAISHI.IsNull)
                    {
                        this.ScrollHaishaDataGrid(sysInfoEntity.HAISHA_WARIATE_KAISHI.Value);
                    }
                    //ThangNguyen [Add] 20150724 End
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex); //排他はエラー
                this.msgLogic.MessageBoxShow("E080");
            }
            catch
            {
                this.msgLogic.MessageBoxShow("E093");
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 配車指示書
        /// <summary>
        /// 
        /// </summary>
        private void HaishaSijisho()
        {
            LogUtility.DebugMethodStart();

            Action<string, bool, bool> DoPrint = (denpyouNum, isKaku, isSS) =>
            {
                // printクラスを呼出
                var reportInfoR345_R350 = new ReportInfoR345_R350(
                    isKaku ? WINDOW_ID.R_SAGYOU_SIJISYO : WINDOW_ID.R_HAISYA_IRAISYO,
                    this.dao_HAISHA);

                // 控え印刷フラグ
                string hikaePrintFlg;
                if (this.IsHikaePrint())
                {
                    // 正・控え（二枚印刷）
                    hikaePrintFlg = "1";
                }
                else
                {
                    // 正のみ印刷
                    hikaePrintFlg = "0";
                }
                // 控え印刷(0：正のみ　1:正・控え２部)
                reportInfoR345_R350.ParameterList["HikaeType"] = hikaePrintFlg;
                // 伝票番号(受付番号)
                reportInfoR345_R350.ParameterList["DenpyouNumber"] = denpyouNum;
                // 受付種類(1：収集　2:出荷)
                reportInfoR345_R350.ParameterList["UketukeType"] = isSS ? "1" : "2";

                reportInfoR345_R350.Create(@".\Template\R345_R350-Form.xml", "LAYOUT1", new DataTable());

                reportInfoR345_R350.Title = "指示書";

                //var formReport = new FormReport(reportInfoR345_R350,
                //    isSS ? WINDOW_ID.UKETSUKE_SHUSHU : WINDOW_ID.UKETSUKE_SHUKKA);
                var formReport = new FormReportPrintPopup(reportInfoR345_R350, "R350",
                                    isSS ? WINDOW_ID.UKETSUKE_SHUSHU : WINDOW_ID.UKETSUKE_SHUKKA);

                // 印刷設定の取得（11：配車指示書）
                //formReport.SetPrintSetting(11);

                // 印刷ポップ画面表示（テスト用）
                //formReport.ShowDialog();

                // 印刷アプリ初期動作(直印刷)
                formReport.PrintInitAction = 1;

                //直接印刷
                formReport.PrintXPS();
            };

            try
            {
                bool isChecked = false;
                using (var tran = new Transaction())
                {
                    this.SetAllUketsuke((dr, n, isSS) =>
                    {
                        if (!dr.Field<bool>("HAISHA_SIJISHO_CHECKED" + n)) return;
                        if (!dr.Field<string>("HAISHA_SHURUI" + n).Equals(ConstClass.HAISHA_SHURUI_KAKU)) return;
                        isChecked = true;
                        dr.SetField<string>("HAISHA_SIJISHO_STATUS" + n, ConstClass.HAISHA_SIJISHO_TRUE);
                        this.dtoIdSeq.SystemId = dr.Field<long>("SYSTEM_ID_" + n);
                        this.dtoIdSeq.Seq = dr.Field<int>("SEQ" + n);
                        if (isSS)
                        {
                            var sse = this.daoT_UKETSUKE_SS_ENTRY.GetDataForLatestData(this.dtoIdSeq);
                            sse.HAISHA_SIJISHO_FLG = true;
                            this.daoT_UKETSUKE_SS_ENTRY.Update(sse);
                        }
                        else
                        {
                            var ske = this.daoT_UKETSUKE_SK_ENTRY.GetDataForLatestData(this.dtoIdSeq);
                            ske.HAISHA_SIJISHO_FLG = true;
                            this.daoT_UKETSUKE_SK_ENTRY.Update(ske);
                        }

                        var denpyouNum = dr.Field<long>("DENPYOU_NUM_" + n).ToString();
                        bool isKaku = dr.Field<string>("HAISHA_SHURUI" + n) == ConstClass.HAISHA_SHURUI_KAKU;
                        DoPrint(denpyouNum, isKaku, isSS);
                    });

                    tran.Commit();
                }
                if (isChecked)
                {
                    this.msgLogic.MessageBoxShow("I001", "印刷");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E076");
                }
            }
            catch (Seasar.Dao.NotSingleRowUpdatedRuntimeException ex)
            {
                LogUtility.Error(ex);
                this.msgLogic.MessageBoxShow("E080");
            }
            catch
            {
                this.msgLogic.MessageBoxShow("E093");
                throw;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #endregion


        #endregion


        #region 入力軸が車種／車両か
        /// <summary>
        /// 入力軸が車種／車両か
        /// </summary>
        /// <returns>true:車種／車両　false:運転者</returns>
        internal bool IsShasyuSyaryou()
        {
            bool retResult = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.sysInfoEntity != null)
                {
                    // 2の場合
                    if (this.ChgDBNullToValue(sysInfoEntity.HAISHA_ONEDAY_NYUURYOKU_KBN, string.Empty).ToString().Equals("2"))
                    {
                        // trueを返す
                        retResult = true;
                    }
                }

                return retResult;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retResult);
            }
        }
        #endregion

        #region 選択解除


        /// <summary>
        /// 
        /// </summary>
        public void ClearForm()
        {
            this.form.mrHaisha.CellValidated-=new EventHandler<CellEventArgs>(this.form.mrHaisha_CellValidated);
            this.selectedCell = null;
            // 前に選択した配車セルの位置を初期化
            this.resultHaisha = new DataTable();
            this.resultMihaisha = new DataTable();
            this.mrHaisha.DataSource = null;
            this.mrMihaisha.DataSource = null;
            this.EnableButtons(false);
            this.form.mrHaisha.CellValidated+=new EventHandler<CellEventArgs>(this.form.mrHaisha_CellValidated);
        }
        #endregion

        #region 検索結果を表示
        /// <summary>
        /// 検索結果を表示
        /// </summary>
        public void Bind()
        {
            LogUtility.DebugMethodStart();
            this.mrHaisha.CellValidated -= this.form.mrHaisha_CellValidated;
            this.selectedCell = null;
            this.mrHaisha.CurrentCell = null;
            this.mrMihaisha.CurrentCell = null;
            this.mrHaisha.DataSource = this.resultHaisha;
            this.mrMihaisha.DataSource = this.resultMihaisha;
            this.mrHaisha.Refresh();
            this.mrMihaisha.Refresh();

            // 入力された車種、運転者コードの最初の行を表示
            this.JumpGridRowByCode();

            this.mrHaisha.CellValidated += this.form.mrHaisha_CellValidated;
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 入力された車種または運転者コードの最初の表示行にジャンプする
        /// </summary>
        internal bool JumpGridRowByCode()
        {
            bool ret = true;
            try
            {
                if ((this.dtoHaisha.HaisyaKubun && !string.IsNullOrEmpty(this.form.SHASHU_CD.Text))
                    || !this.dtoHaisha.HaisyaKubun && !string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    int rowCnt = 0;
                    foreach (Row row in this.mrHaisha.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        if ((this.dtoHaisha.HaisyaKubun && this.form.SHASHU_CD.Text.Equals(row.Cells[CELL_NAME_SHASHU_CD].Value.ToString()))
                            || (!this.dtoHaisha.HaisyaKubun && this.form.UNTENSHA_CD.Text.Equals(row.Cells[CELL_NAME_UNTENSHA_CD].Value.ToString())))
                        {
                            // 車輌を軸とするときは入力された車輌コードと、運転者を軸とするときは入力された運転者コードと
                            // 合致する行が存在すれば、その行まで移動
                            this.mrHaisha.FirstDisplayedCellPosition = new GrapeCity.Win.MultiRow.CellPosition(rowCnt, 0);
                            row.Selected = true;
                            break;
                        }
                        rowCnt++;
                    }
                }
                else
                {
                    // 車種、運転者の入力がない場合は、先頭行を表示
                    if (this.mrHaisha.Rows.Count > 0)
                    {
                        this.mrHaisha.FirstDisplayedCellPosition = new GrapeCity.Win.MultiRow.CellPosition(0, 0);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("JumpGridRowByCode", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 入力画面へ遷移する
        /// <summary>
        /// 入力画面へ遷移する
        /// </summary>
        private void forwardNyuuryoku(WINDOW_TYPE windowType)
        {
            try
            {
                LogUtility.DebugMethodStart(windowType);

                //画面遷移
                if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrHaisha.Name)
                {
                    var dr = this.resultHaisha.Rows[this.selectedCell.RowIndex];
                    string suffix = this.GetColumn(this.selectedCell);
                    if (dr.Field<short>("SHUBETSU_KBN_" + suffix) != ConstClass.SHUBETSU_KBN_EMPTY)
                    {
                        var denpyouNumber = dr.Field<long>("DENPYOU_NUM_" + suffix).ToString();
                        var shubetsu = dr.Field<short>("SHUBETSU_KBN_" + suffix);

                        if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
                        {
                            // 受付(収集)入力画面へ遷移する。
                            FormManager.OpenFormWithAuth("G015", windowType, windowType, denpyouNumber);
                        }
                        else if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                        {
                            // 受付(出荷)入力画面へ遷移する。
                            FormManager.OpenFormWithAuth("G016", windowType, windowType, denpyouNumber);
                        }
                        else if (shubetsu == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
                        {
                            // 定期配車入力画面へ遷移する。
                            FormManager.OpenFormWithAuth("G030", windowType, windowType, denpyouNumber);
                        }
                    }
                }
                else if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrMihaisha.Name)
                {
                    var dr = this.resultMihaisha.Rows[this.selectedCell.RowIndex];
                    var denpyouNumber = dr.Field<long>("DENPYOU_NUM_").ToString();
                    var shubetsu = dr.Field<short>("SHUBETSU_KBN_");

                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
                    {
                        // 受付(収集)入力画面へ遷移する。
                        FormManager.OpenFormWithAuth("G015", windowType, windowType, denpyouNumber);
                    }
                    else if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                    {
                        // 受付(出荷)入力画面へ遷移する。
                        FormManager.OpenFormWithAuth("G016", windowType, windowType, denpyouNumber);
                    }
                    else if (shubetsu == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
                    {
                        // 定期配車入力画面へ遷移する。
                        FormManager.OpenFormWithAuth("G030", windowType, windowType, denpyouNumber);
                    }
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd(windowType);
            }
        }
        #endregion

       

        #region Format

        #region 伝票Format
        /// <summary>
        /// 伝票Format
        /// </summary>
        /// <param name="e"></param>
        /// <param name="dr"></param>
        /// <param name="fn1"></param>
        /// <param name="fn2"></param>
        private void FormatDenpyou(CellFormattingEventArgs e, DataRow dr, string fn1, string fn2)
        {
            if (e.CellName.Contains("HaishaShurui"))
            {
                var shurui = dr[fn1].ToString();
                e.CellStyle.ForeColor =
                    shurui == ConstClass.HAISHA_SHURUI_KAKU ? Color.Red :
                    shurui == ConstClass.HAISHA_SHURUI_KARI ? Color.Green :
                    e.CellStyle.ForeColor;
            }
            else if (e.CellName.Contains("GenchakuJikan"))
            {
                var bkColor = dr[fn2];
                if (!bkColor.Equals(DBNull.Value))
                {
                    //e.CellStyle.BackColor = Color.FromArgb((int)bkColor);
                    // 現着マスター画面でKnownColorの値が設定されたので、KnownColorから取得
                    e.CellStyle.BackColor = Color.FromKnownColor((KnownColor)int.Parse(bkColor.ToString()));
                }
            }
            else if (e.CellName.Contains("SagyouDateKubun"))
            {
                if (e.Value != null)
                {
                    var v = e.Value.ToString();
                    e.Value = v == "" ? "" : "期間";
                    if (v == "期間終了")
                    {
                        e.CellStyle.ForeColor = Color.Red;
                    }
                }
            }
        }
        #endregion

        #region 配車CellFormatting
        /// <summary>
        /// 配車CellFormatting
        /// </summary>
        /// <param name="e"></param>
        public bool mrHaisha_CellFormatting(CellFormattingEventArgs e)
        {
            bool ret = true;
            try
            {
                //LogUtility.DebugMethodStart(e);
                if (e.CellName == "cellUnused1" || e.CellName == "cellUnused2" ||
                      e.CellName == "cellUntenshaName" || e.CellName == "cellShashuName" ||
                      e.CellName == "cellUnpanGyoushaName" || e.CellName == "cellSharyouName")
                {
                    e.CellStyle.BackColor = Color.GreenYellow;
                    e.CellStyle.SelectionBackColor = Color.GreenYellow;
                    return true;
                }

                int i;
                if (e.RowIndex < this.resultHaisha.Rows.Count && int.TryParse(GetColumn(e.CellName), out i))
                {
                    var drHaisha = this.resultHaisha.Rows[e.RowIndex];
                    string suffix = GetColumn(e.CellName);

                    if (this.sysInfoEntity != null && !this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU.IsNull && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1)
                    {
                        if (drHaisha.Field<bool>("WORK_CLOSED_SHARYOU") || drHaisha.Field<bool>("WORK_CLOSED_UNTENSHA") || i > drHaisha.Field<int>("SAIDAI_WAKU_SUU"))
                        {
                            e.CellStyle.BackColor = Color.LightGray;
                            e.CellStyle.SelectionBackColor = Color.LightGray;
                            e.CellStyle.DisabledBackColor = Color.LightGray;
                            return true;
                        }
                    }

                    bool karadenpyou = drHaisha.Field<bool>("KARADENPYOU_FLG_" + suffix);
                    if (karadenpyou)
                    {
                        e.CellStyle.BackColor = Color.Gray;
                        e.CellStyle.SelectionBackColor = Color.Gray;
                        e.CellStyle.DisabledBackColor = Color.Gray;
                        return true;
                    }

                    if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrHaisha.Name
                        && e.RowIndex == this.selectedCell.RowIndex && this.GetColumn(this.selectedCell) == suffix)
                    {
                        if (ConstClass.BlockHaishaCellName.Contains(e.CellName.Substring(0, e.CellName.Length - 2)))
                        {
                            e.CellStyle.BackColor = ConstClass.blockDetailColor;
                            e.CellStyle.SelectionBackColor = ConstClass.blockDetailColor;
                            e.CellStyle.DisabledBackColor = ConstClass.blockDetailColor;
                        }
                    }

                    FormatDenpyou(e, drHaisha,
                        "HAISHA_SHURUI" + suffix,
                        "GENCHAKU_BACK_COLOR" + suffix);

                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrHaisha_CellFormatting", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                //LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 未配車CellFormatting
        /// <summary>
        /// 未配車CellFormatting
        /// </summary>
        /// <param name="e"></param>
        public bool mrMihaisha_CellFormatting(CellFormattingEventArgs e)
        {
            bool ret = true;
            try
            {
                 //LogUtility.DebugMethodStart(e);
                if (e.RowIndex < this.resultMihaisha.Rows.Count)
                {
                    var drMihaisha = ((DataRowView)this.mrMihaisha.Rows[e.RowIndex].DataBoundItem).Row;
                    if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrMihaisha.Name && e.RowIndex == this.selectedCell.RowIndex)
                    {
                        if (ConstClass.BlockMihaishaCellName.Contains(e.CellName))
                        {
                            e.CellStyle.BackColor = ConstClass.blockDetailColor;
                            e.CellStyle.SelectionBackColor = ConstClass.blockDetailColor;
                            e.CellStyle.DisabledBackColor = ConstClass.blockDetailColor;
                        }
                    }

                    FormatDenpyou(e, drMihaisha,
                        "HAISHA_SHURUI",
                        "GENCHAKU_BACK_COLOR");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrMihaisha_CellFormatting", ex);
                 this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                //LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 配車Cell編集
        /// <summary>
        /// 配車Cell編集
        /// </summary>
        /// <param name="e"></param>
        public bool mrHaisha_CellBeginEdit(CellBeginEditEventArgs e)
        {
            bool ret = true;
            try
            {
                //LogUtility.DebugMethodStart(e);

                if (!this.parentForm.bt_func9.Enabled)
                {
                    // F9（登録）ボタンが使用不可（検索の前）の場合は入力不可
                    e.Cancel = true;
                }
                else if (e.CellName.Contains("CheckBox"))
                {
                    // 既に割当のあるセルのみチェック可
                    if (e.RowIndex == this.resultHaisha.Rows.Count)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        var drHaisha = this.resultHaisha.Rows[e.RowIndex];
                        var colNum = GetColumn(e.CellName);
                        var shubetsuKbn = drHaisha.Field<short>("SHUBETSU_KBN_" + colNum);
                        if (shubetsuKbn == ConstClass.SHUBETSU_KBN_EMPTY ||
                            shubetsuKbn == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
                        {
                            e.Cancel = true;
                        }
                        var haishaShurui = drHaisha.Field<string>("HAISHA_SHURUI" + colNum);
                        if (haishaShurui != ConstClass.HAISHA_SHURUI_KAKU)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            // 配車種類が確定以外であればチェック不可とする
                            if (false == this.checkHaishaShurui(drHaisha, shubetsuKbn, colNum))
                            {
                                e.Cancel = true;
                            }
                        }
                    }
                }
                else if (e.RowIndex < this.sharyouCount)
                {
                    if (e.CellName != CELL_NAME_UNTENSHA_CD && e.CellName != CELL_NAME_SHASHU_CD && e.CellName != CELL_NAME_SHARYOU_CD && e.CellName != CELL_NAME_UNPAN_GYOUSHA_CD)
                    {
                        // 既存の行では運転者、車種、車輌のみ入力可
                        e.Cancel = true;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrHaisha_CellBeginEdit", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                //LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #endregion

        #region セルクリック

        #region 配車セルクリック処理
        /// <summary>
        /// 伝票取替え処理
        /// </summary>
        /// <param name="drHaishaTo"></param>
        /// <param name="suffixTo"></param>
        /// <param name="drHaishaFrom"></param>
        /// <param name="suffixFrom"></param>
        private bool SwapDenpyou(DataRow drHaishaTo, string suffixTo, DataRow drHaishaFrom, string suffixFrom)
        {
            // 前伝票
            var denpyouNumberFrom = drHaishaFrom.Field<long>("DENPYOU_NUM_" + suffixFrom).ToString();
            var shubetsuFrom = drHaishaFrom.Field<short>("SHUBETSU_KBN_" + suffixFrom);

            if (shubetsuFrom == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
            {
                if (!this.RenkeiCheck(denpyouNumberFrom, "1", shubetsuFrom))
                {
                    return false;
                }
            }
            else if (shubetsuFrom == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
            {
                if (!this.RenkeiCheck(denpyouNumberFrom, "1", shubetsuFrom))
                {
                    return false;
                }
            }
            else if (shubetsuFrom == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
            {
                if (!this.RenkeiCheck(denpyouNumberFrom, "0", shubetsuFrom))
                {
                    return false;
                }
            }

            // 後伝票
            var denpyouNumberTo = drHaishaTo.Field<long>("DENPYOU_NUM_" + suffixTo).ToString();
            var shubetsuTo = drHaishaTo.Field<short>("SHUBETSU_KBN_" + suffixTo);

            if (shubetsuTo == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
            {
                if (!this.RenkeiCheck(denpyouNumberTo, "1", shubetsuTo))
                {
                    return false;
                }
            }
            if (shubetsuTo == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
            {
                if (!this.RenkeiCheck(denpyouNumberTo, "1", shubetsuTo))
                {
                    return false;
                }
            }
            else if (shubetsuTo == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
            {
                if (!this.RenkeiCheck(denpyouNumberTo, "0", shubetsuTo))
                {
                    return false;
                }
            }


            // 確定の場合、交換禁止
            if (drHaishaFrom.Field<string>("HAISHA_SHURUI" + suffixFrom) == ConstClass.HAISHA_SHURUI_KAKU ||
                    drHaishaTo.Field<string>("HAISHA_SHURUI" + suffixTo) == ConstClass.HAISHA_SHURUI_KAKU)
            {
                msgLogic.MessageBoxShow("E112");
                return false;
            }
            if (drHaishaTo == drHaishaFrom)
            {
                // 設定された最大枠数を超える場合
                if (this.sysInfoEntity != null && !this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU.IsNull && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1)
                {
                    int intMaxWakuSuu = drHaishaTo.Field<int>("SAIDAI_WAKU_SUU");
                    if (int.Parse(suffixTo) > intMaxWakuSuu)
                    {
                        // 確認メッセージを表示
                        if (this.msgLogic.MessageBoxShow("C056") != DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                }

                Object objTemp = null;
                foreach (var di in ConstClass.DENPYOU_INFO)
                {
                    var fn = di.FieldName;
                    objTemp = drHaishaTo[fn + suffixTo];
                    drHaishaTo[fn + suffixTo] = drHaishaFrom[fn + suffixFrom];
                    drHaishaFrom[fn + suffixFrom] = objTemp;
                }
                this.changeHaishaFlg = true;
                return true;
            }
            else
            {
                if (!drHaishaTo.Field<bool>("WORK_CLOSED_SHARYOU") &&
                    !drHaishaTo.Field<bool>("WORK_CLOSED_UNTENSHA") ||
                    msgLogic.MessageBoxShow("C054") == DialogResult.Yes)
                {

                    // 設定された最大枠数を超える場合
                    if (this.sysInfoEntity != null && !this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU.IsNull && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1)
                    {
                        int intMaxWakuSuu = drHaishaTo.Field<int>("SAIDAI_WAKU_SUU");
                        if (int.Parse(suffixTo) > intMaxWakuSuu)
                        {
                            // 確認メッセージを表示
                            if (this.msgLogic.MessageBoxShow("C056") != DialogResult.Yes)
                            {
                                return false;
                            }
                        }
                    }

                    Object objTemp = null;
                    foreach (var di in ConstClass.DENPYOU_INFO)
                    {
                        var fn = di.FieldName;
                        objTemp = drHaishaTo[fn + suffixTo];
                        drHaishaTo[fn + suffixTo] = drHaishaFrom[fn + suffixFrom];
                        drHaishaFrom[fn + suffixFrom] = objTemp;
                    }
                    this.changeHaishaFlg = true;
                    return true;
                }
            }
            return false;
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="miHaishaRowIndex"></param>
        /// <param name="suffixTo"></param>
        /// <param name="drHaishaTo"></param>
        private bool AllocateDenpyou(int miHaishaRowIndex, string suffixTo, DataRow drHaishaTo)
        {
            var drMihaishaFrom = ((DataRowView)this.mrMihaisha.Rows[miHaishaRowIndex].DataBoundItem).Row;

            // 後伝票
            var denpyouNumberTo = drHaishaTo.Field<long>("DENPYOU_NUM_" + suffixTo).ToString();
            var shubetsuTo = drHaishaTo.Field<short>("SHUBETSU_KBN_" + suffixTo);

            bool swapFlg = false;
            if (shubetsuTo == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
            {
                if (!this.RenkeiCheck(denpyouNumberTo, "1", shubetsuTo))
                {
                    return false;
                }
                swapFlg = true;
            }
            else if (shubetsuTo == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
            {
                if (!this.RenkeiCheck(denpyouNumberTo, "1", shubetsuTo))
                {
                    return false;
                }
                swapFlg = true;
            }
            else if (shubetsuTo == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
            {
                if (!this.RenkeiCheck(denpyouNumberTo, "0", shubetsuTo))
                {
                    return false;
                }
                swapFlg = true;
            }

            // 確定の場合、交換禁止
            if (drHaishaTo.Field<string>("HAISHA_SHURUI" + suffixTo) == ConstClass.HAISHA_SHURUI_KAKU)
            {
                msgLogic.MessageBoxShow("E112");
                return false;
            }

            if (!drHaishaTo.Field<bool>("WORK_CLOSED_SHARYOU") &&
                    !drHaishaTo.Field<bool>("WORK_CLOSED_UNTENSHA") ||
                    msgLogic.MessageBoxShow("C054") == DialogResult.Yes)
            {

                // 設定された最大枠数を超える場合
                if (this.sysInfoEntity != null && !this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU.IsNull && this.sysInfoEntity.HAISHA_WARIATE_KUUHAKU == 1)
                {
                    int intMaxWakuSuu = drHaishaTo.Field<int>("SAIDAI_WAKU_SUU");
                    if (int.Parse(suffixTo) > intMaxWakuSuu)
                    {
                        // 確認メッセージを表示
                        if (this.msgLogic.MessageBoxShow("C056") != DialogResult.Yes)
                        {
                            return false;
                        }
                    }
                }


                //Phan bo vao luoi haisha
                DataRow drMihaishaTemp = this.resultMihaisha.NewRow();
                foreach (var di in ConstClass.DENPYOU_INFO)
                {
                    var fn = di.FieldName;
                    if (drHaishaTo[fn + suffixTo].ToString() != di.DefaultValue.ToString())
                    {
                        drMihaishaTemp[fn] = drHaishaTo[fn + suffixTo];
                    }
                    drHaishaTo[fn + suffixTo] = drMihaishaFrom[fn];
                }
                this.resultMihaisha.Rows.Remove(drMihaishaFrom);
                this.MihaishaCount--;
                this.HaishaCount++;

                if (swapFlg)
                {
                    foreach (var di in ConstClass.DENPYOU_INFO)
                    {
                        var fn = di.FieldName;
                        if (drMihaishaTemp[fn] == null || (drMihaishaTemp[fn] != null && drMihaishaTemp[fn].ToString() == "") || drMihaishaTemp[fn] == System.DBNull.Value)
                        {
                            drMihaishaTemp[fn] = di.DefaultValue;
                        }
                    }
                    this.resultMihaisha.Rows.InsertAt(drMihaishaTemp, miHaishaRowIndex);
                    this.MihaishaCount++;
                    this.HaishaCount--;
                }

                this.changeHaishaFlg = true;
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// 空伝票
        /// </summary>
        /// <param name="cellHaisha"></param>
        private void KaraDenpyou(Cell cellHaisha)
        {
            if (isNewHaisha(cellHaisha))
            {
                return;
            }
            string suffix = GetColumn(cellHaisha.Name);
            if (this.resultHaisha.Rows[cellHaisha.RowIndex].Field<short>("SHUBETSU_KBN_" + suffix) == ConstClass.SHUBETSU_KBN_EMPTY)
            {
                if (!(bool)this.resultHaisha.Rows[cellHaisha.RowIndex]["KARADENPYOU_FLG_" + suffix])
                {
                    this.resultHaisha.Rows[cellHaisha.RowIndex]["KARADENPYOU_FLG_" + suffix] = true;
                }
                else
                {
                    this.resultHaisha.Rows[cellHaisha.RowIndex]["KARADENPYOU_FLG_" + suffix] = false;
                }
                this.changeHaishaFlg = true;
            }
        }
        

        /// 配車セルクリック処理
        /// </summary>
        /// <param name="e"></param>
        public bool mrHaisha_CellClick(CellEventArgs e)
        {
            bool ret = true;
            try
            {
                //LogUtility.DebugMethodStart(e);
				var cellHaisha = this.mrHaisha[e.RowIndex, e.CellName];

                if (e.CellName == "buttonMap")
                {
                    if (!isNewHaisha(cellHaisha) && this.resultHaisha.Rows.Count != 0)
                    {
                        // 地図表示ボタン押下時の処理
                        this.mapPopupOpen(e.RowIndex);
                    }
                    return true;
                }

                if (r_framework.Authority.Manager.CheckAuthority("G026", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    if (this.form.tbWariateSettei.Text == "1")
                    {
                        if (!isNewHaisha(cellHaisha) && this.isDenpyouContent(cellHaisha))
                        {
                            string suffixTo = GetColumn(cellHaisha.Name);
                            if (!(bool)this.resultHaisha.Rows[e.RowIndex]["KARADENPYOU_FLG_" + suffixTo])
                            {
                                var drHaishaTo = this.resultHaisha.Rows[cellHaisha.RowIndex];
                                if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrHaisha.Name)
                                {
                                    string suffixFrom = this.GetColumn(this.selectedCell);
                                    if (this.selectedCell.RowIndex != e.RowIndex || suffixFrom != suffixTo)
                                    {
                                        var drHaishaFrom = this.resultHaisha.Rows[this.selectedCell.RowIndex];
                                        if (drHaishaFrom.Field<short>("SHUBETSU_KBN_" + suffixFrom) != ConstClass.SHUBETSU_KBN_EMPTY)
                                        {
                                            SwapDenpyou(drHaishaTo, suffixTo, drHaishaFrom, suffixFrom);
                                        }
                                    }

                                    this.selectedCell = null;
                                    this.form.mrHaisha.Refresh();
                                    this.form.mrMihaisha.Refresh();
                                }
                                else if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrMihaisha.Name)
                                {
                                    //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 START
                                    if (AppConfig.AppOptions.IsInxsUketsuke())
                                    {
                                        DataRow drMihaisha = ((DataRowView)this.mrMihaisha.Rows[this.selectedCell.RowIndex].DataBoundItem).Row;
                                        if (drMihaisha.Field<short>("SHUBETSU_KBN_") == ConstClass.SHUBETSU_KBN_UKETSUKE_SS
                                            && !RenkeiInxsCheckChangeSagyou(drMihaisha.Field<long>("DENPYOU_NUM_").ToString()))
                                        {
                                            this.selectedCell = null;
                                            this.form.mrHaisha.Refresh();
                                            this.form.mrMihaisha.Refresh();
                                            return true;
                                        }
                                    }
                                    //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 END
                                    

                                    AllocateDenpyou(this.selectedCell.RowIndex, suffixTo, drHaishaTo);

                                    this.selectedCell = null;
                                    this.form.mrHaisha.Refresh();
                                    this.form.mrMihaisha.Refresh();
                                }
                                else
                                {
                                    this.selectedCell = cellHaisha;
                                    this.form.mrHaisha.Refresh();
                                    this.form.mrMihaisha.Refresh();
                                }
                            }
                        }
                    }
                    else if (this.form.tbWariateSettei.Text == "2")
                    {
                        if (!isNewHaisha(cellHaisha) && this.isDenpyouContent(cellHaisha))
                        {
                            KaraDenpyou(cellHaisha);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrHaisha_CellClick", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                //LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
         
        

        #endregion

        #region 未配車セルクリック処理

        /// <summary>
        /// 
        /// </summary>
        /// <param name="drHaishaFrom"></param>
        /// <param name="suffixFrom"></param>
        private bool ReleaseDenpyou(DataRow drHaishaFrom, string suffixFrom)
        {
            var denpyouNumber = drHaishaFrom.Field<long>("DENPYOU_NUM_" + suffixFrom).ToString();
            var shubetsu = drHaishaFrom.Field<short>("SHUBETSU_KBN_" + suffixFrom);

            if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
            {
                if (!this.RenkeiCheck(denpyouNumber, "1", shubetsu))
                {
                    return false;
                }
            }
            else if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
            {
                if (!this.RenkeiCheck(denpyouNumber, "1", shubetsu))
                {
                    return false;
                }
            }
            else if (shubetsu == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
            {
                if (!this.RenkeiCheck(denpyouNumber, "0", shubetsu))
                {
                    return false;
                }
            }
            if (drHaishaFrom.Field<string>("HAISHA_SHURUI" + suffixFrom) == ConstClass.HAISHA_SHURUI_KAKU)
            {
                return false;
            }

            //Luu vao dong Mihaisha
            var drMihaisha = this.resultMihaisha.NewRow();
            foreach (var di in ConstClass.DENPYOU_INFO)
            {
                var fn = di.FieldName;
                drMihaisha[fn] = drHaishaFrom[fn + suffixFrom];
                drHaishaFrom[fn + suffixFrom] = di.DefaultValue;
            }
            this.resultMihaisha.Rows.Add(drMihaisha);

            this.MihaishaCount++;
            this.HaishaCount--;

            this.changeHaishaFlg = true;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool mrMihaisha_CellClick(CellEventArgs e)
        {
            bool ret = true;
            try
            {
                //LogUtility.DebugMethodStart(e);
                if (r_framework.Authority.Manager.CheckAuthority("G026", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    if (this.form.tbWariateSettei.Text == "1")
                    {
                        var cellMihaisha = e.Scope == CellScope.Row ? this.mrMihaisha[e.RowIndex, e.CellName] : null;
                        if (isDenpyouContent(cellMihaisha))
                        {
                            if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrHaisha.Name)
                            {
                                var drHaishaFrom = this.resultHaisha.Rows[selectedCell.RowIndex];
                                string suffixFrom = this.GetColumn(this.selectedCell);

                                if (drHaishaFrom.Field<short>("SHUBETSU_KBN_" + suffixFrom) != ConstClass.SHUBETSU_KBN_EMPTY)
                                {

                                    ReleaseDenpyou(drHaishaFrom, suffixFrom);

                                    this.selectedCell = null;
                                    this.form.mrHaisha.Refresh();
                                    this.form.mrMihaisha.Refresh();
                                }
                            }
                            else if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrMihaisha.Name)
                            {
                                this.selectedCell = null;
                                this.form.mrMihaisha.Refresh();
                            }
                            else
                            {

                                this.selectedCell = cellMihaisha;
                                this.form.mrHaisha.Refresh();
                                this.form.mrMihaisha.Refresh();
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrMihaisha_CellClick", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                //LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        #endregion

        #region 未配車リストクリック

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public bool mrMihaisha_MouseDown(MouseEventArgs e)
        {
            bool ret = true;
            try
            {
                //LogUtility.DebugMethodStart(e);
                if (r_framework.Authority.Manager.CheckAuthority("G026", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    if (this.form.tbWariateSettei.Text == "1")
                    {
                        HitTestInfo hti = this.form.mrMihaisha.HitTest(e.X, e.Y);
                        if (hti.Type == HitTestType.None)
                        {
                            if (this.selectedCell != null && this.selectedCell.GcMultiRow.Name == this.mrHaisha.Name)
                            {
                                var drHaishaFrom = this.resultHaisha.Rows[this.selectedCell.RowIndex];
                                string suffixFrom = this.GetColumn(this.selectedCell);
                                if (drHaishaFrom.Field<short>("SHUBETSU_KBN_" + suffixFrom) != ConstClass.SHUBETSU_KBN_EMPTY)
                                {
                                    ReleaseDenpyou(drHaishaFrom, suffixFrom);
                                }
                            }
                            this.selectedCell = null;
                            this.form.mrHaisha.Refresh();
                            this.form.mrMihaisha.Refresh();
                        }

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrMihaisha_MouseDown", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                //LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
        #endregion

        #region 配車リストのCellDoubleクリック処理
        /// <summary>
        /// 配車リストのCellDoubleクリック処理
        /// </summary>
        /// <param name="e"></param>
        public void mrHaisha_CellDoubleClick(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                if (e.RowIndex >= 0)
                {
                    this.selectedCell = null;
                    var cellHaisha = this.mrHaisha[e.RowIndex, e.CellName];
                    if (!isNewHaisha(cellHaisha) && this.isDenpyouContent(cellHaisha))
                    {
                        var suffix = GetColumn(e.CellName);

                        this.selectedCell = cellHaisha;
                        this.form.mrHaisha.Refresh();
                        this.form.mrMihaisha.Refresh();

                        var dr = this.resultHaisha.Rows[e.RowIndex];
                        // No.2840-->
                        string[] slist = new string[8];
                        slist[0] = dr.Field<string>("SHASYU_CD");
                        slist[1] = dr.Field<string>("SHASHU_NAME_RYAKU");
                        slist[2] = dr.Field<string>("SHARYOU_CD");
                        slist[3] = dr.Field<string>("SHARYOU_NAME_RYAKU");
                        slist[4] = dr.Field<string>("SHAIN_CD");
                        slist[5] = dr.Field<string>("SHAIN_NAME_RYAKU");
                        slist[6] = dr.Field<string>("GYOUSHA_CD");
                        slist[7] = dr.Field<string>("GYOUSHA_NAME_RYAKU");
                        // No.2840<--

                        var denpyouNumber = dr.Field<long>("DENPYOU_NUM_" + suffix).ToString();
                        var shubetsu = dr.Field<short>("SHUBETSU_KBN_" + suffix);

                        if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
                        {
                            // 受付(収集)入力画面へ遷移する。
                            FormManager.OpenFormModalWithAuth("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNumber, slist);// No.2840
                        }
                        else if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                        {
                            // 受付(出荷)入力画面へ遷移する。
                            FormManager.OpenFormModalWithAuth("G016", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNumber, slist);// No.2840
                        }
                        else if (shubetsu == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
                        {
                            // 定期配車入力画面へ遷移する。
                            FormManager.OpenFormModalWithAuth("G030", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNumber, "", "", slist);// No.2840
                        }

                    }
                    if (!r_framework.Authority.Manager.CheckAuthority("G026", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                    {
                        this.selectedCell = null;
                    }
                    this.form.mrHaisha.Refresh();
                    this.form.mrMihaisha.Refresh();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrHaisha_CellDoubleClick", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #region 未配車リストのCellDoubleクリック処理
        /// <summary>
        /// 未配車リストのCellDoubleクリック処理
        /// </summary>
        /// <param name="e"></param>
        public void mrMihaisha_CellDoubleClick(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);
                this.selectedCell = null;

                var cellMihaisha = e.Scope == CellScope.Row ? this.mrMihaisha[e.RowIndex, e.CellName] : null;
                if (isDenpyouContent(cellMihaisha))
                {
                    this.selectedCell = cellMihaisha;
                    this.form.mrHaisha.Refresh();
                    this.form.mrMihaisha.Refresh();

                    var dr = this.resultMihaisha.Rows[e.RowIndex];
                    var denpyouNumber = dr.Field<long>("DENPYOU_NUM_").ToString();
                    var shubetsu = dr.Field<short>("SHUBETSU_KBN_");
                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
                    {
                        // 受付(収集)入力画面へ遷移する。
                        FormManager.OpenFormModalWithAuth("G015", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNumber);
                    }
                    else if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                    {
                        // 受付(出荷)入力画面へ遷移する。
                        FormManager.OpenFormModalWithAuth("G016", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNumber);
                    }
                    else if (shubetsu == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
                    {
                        // 定期配車入力画面へ遷移する。
                        FormManager.OpenFormModalWithAuth("G030", WINDOW_TYPE.REFERENCE_WINDOW_FLAG, WINDOW_TYPE.REFERENCE_WINDOW_FLAG, denpyouNumber);
                    }
                }
                if (!r_framework.Authority.Manager.CheckAuthority("G026", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.selectedCell = null;
                }
                this.form.mrHaisha.Refresh();
                this.form.mrMihaisha.Refresh();
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrMihaisha_CellDoubleClick", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }
        #endregion

        #endregion

        #region 配車CellEnter
        /// <summary>
        /// 配車CellEnter
        /// </summary>
        /// <param name="e"></param>
        internal void mrHaisha_OnCellEnter(CellEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(e);

                Row row = this.mrHaisha.CurrentRow;
                if (row == null)
                {
                    return;
                }

                if (this.parentForm.bt_func9.Enabled)
                {
                    // 20141015 koukouei 休動管理機能 start
                    if (this.validatedFlag && this.errorCell != null)
                    {
                        this.mrHaisha.CellValidated -= this.form.mrHaisha_CellValidated;
                        this.mrHaisha.CellEnter -= this.form.mrHaisha_OnCellEnter;
                        this.mrHaisha.CurrentCell = this.errorCell;
                        this.mrHaisha.CellEnter += this.form.mrHaisha_OnCellEnter;
                        this.mrHaisha.CellValidated += this.form.mrHaisha_CellValidated;
                        this.errorCell = null;
                        //this.validatedFlag = false;
                        this.form.mrHaisha.Focus();
                        return;
                    }
                    // 20141015 koukouei 休動管理機能 end

                    switch (e.CellName)
                    {
                        case CELL_NAME_SHARYOU_CD:
                        case CELL_NAME_UNTENSHA_CD:
                        case CELL_NAME_SHASHU_CD:
                        case CELL_NAME_UNPAN_GYOUSHA_CD:
                            // 前回値チェック用データをセット
                            if (beforeValuesForHaisha.ContainsKey(e.CellName))
                            {
                                beforeValuesForHaisha[e.CellName] = Convert.ToString(row.Cells[e.CellName].Value);
                            }
                            else
                            {
                                beforeValuesForHaisha.Add(e.CellName, Convert.ToString(row.Cells[e.CellName].Value));
                            }
                            // 車輌CD入力の場合は、運搬情報を退避しておく（運転者が入力軸の場合は退避した値を戻すため）
                            if (e.CellName.Equals(CELL_NAME_SHARYOU_CD))
                            {
                                // 運搬情報の前回値をセット
                                this.SetUnpanJouhouBeforeValue(row);
                            }
                            break;
                    }
                }
                if (e.CellName.Contains("DenpyouContent"))
                {
                    this.mrHaisha.CurrentCellBorderLine = new Line(LineStyle.None, Color.Red);
                }
                else
                {
                    this.mrHaisha.CurrentCellBorderLine = new Line(LineStyle.Medium, Color.Red);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrHaisha_OnCellEnter", ex);
                this.msgLogic.MessageBoxShow("E245", "");
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 運搬情報の前回値をセットします
        /// </summary>
        /// <param name="row">Row</param>
        internal void SetUnpanJouhouBeforeValue(Row row)
        {
            // 運転者CD
            if (beforeValuesForHaisha.ContainsKey(CELL_NAME_UNTENSHA_CD))
            {
                beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD] = Convert.ToString(row.Cells[CELL_NAME_UNTENSHA_CD].Value);
            }
            else
            {
                beforeValuesForHaisha.Add(CELL_NAME_UNTENSHA_CD, Convert.ToString(row.Cells[CELL_NAME_UNTENSHA_CD].Value));
            }

            // 運転者名
            if (beforeValuesForHaisha.ContainsKey(CELL_NAME_UNTENSHA_NAME))
            {
                beforeValuesForHaisha[CELL_NAME_UNTENSHA_NAME] = Convert.ToString(row.Cells[CELL_NAME_UNTENSHA_NAME].Value);
            }
            else
            {
                beforeValuesForHaisha.Add(CELL_NAME_UNTENSHA_NAME, Convert.ToString(row.Cells[CELL_NAME_UNTENSHA_NAME].Value));
            }

            // 車種CD
            if (beforeValuesForHaisha.ContainsKey(CELL_NAME_SHASHU_CD))
            {
                beforeValuesForHaisha[CELL_NAME_SHASHU_CD] = Convert.ToString(row.Cells[CELL_NAME_SHASHU_CD].Value);
            }
            else
            {
                beforeValuesForHaisha.Add(CELL_NAME_SHASHU_CD, Convert.ToString(row.Cells[CELL_NAME_SHASHU_CD].Value));
            }

            // 車種名
            if (beforeValuesForHaisha.ContainsKey(CELL_NAME_SHASHU_NAME))
            {
                beforeValuesForHaisha[CELL_NAME_SHASHU_NAME] = Convert.ToString(row.Cells[CELL_NAME_SHASHU_NAME].Value);
            }
            else
            {
                beforeValuesForHaisha.Add(CELL_NAME_SHASHU_NAME, Convert.ToString(row.Cells[CELL_NAME_SHASHU_NAME].Value));
            }

            // 運搬業者CD
            if (beforeValuesForHaisha.ContainsKey(CELL_NAME_UNPAN_GYOUSHA_CD))
            {
                beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD] = Convert.ToString(row.Cells[CELL_NAME_UNPAN_GYOUSHA_CD].Value);
            }
            else
            {
                beforeValuesForHaisha.Add(CELL_NAME_UNPAN_GYOUSHA_CD, Convert.ToString(row.Cells[CELL_NAME_UNPAN_GYOUSHA_CD].Value));
            }

            // 運搬業者名
            if (beforeValuesForHaisha.ContainsKey(CELL_NAME_UNPAN_GYOUSHA_NAME))
            {
                beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_NAME] = Convert.ToString(row.Cells[CELL_NAME_UNPAN_GYOUSHA_NAME].Value);
            }
            else
            {
                beforeValuesForHaisha.Add(CELL_NAME_UNPAN_GYOUSHA_NAME, Convert.ToString(row.Cells[CELL_NAME_UNPAN_GYOUSHA_NAME].Value));
            }

            // 車輌CD
            if (beforeValuesForHaisha.ContainsKey(CELL_NAME_SHARYOU_CD))
            {
                beforeValuesForHaisha[CELL_NAME_SHARYOU_CD] = Convert.ToString(row.Cells[CELL_NAME_SHARYOU_CD].Value);
            }
            else
            {
                beforeValuesForHaisha.Add(CELL_NAME_SHARYOU_CD, Convert.ToString(row.Cells[CELL_NAME_SHARYOU_CD].Value));
            }

            // 車輌名
            if (beforeValuesForHaisha.ContainsKey(CELL_NAME_SHARYOU_NAME))
            {
                beforeValuesForHaisha[CELL_NAME_SHARYOU_NAME] = Convert.ToString(row.Cells[CELL_NAME_SHARYOU_NAME].Value);
            }
            else
            {
                beforeValuesForHaisha.Add(CELL_NAME_SHARYOU_NAME, Convert.ToString(row.Cells[CELL_NAME_SHARYOU_NAME].Value));
            }
        }
        #endregion

        #region 配車CellValidating
        /// <summary>
        /// 確定伝票が割り当てられている行の配車情報があるかチェックし変更を中断する
        /// </summary>
        /// <param name="e"></param>
        public void CheckKakuteiDenpyou(CellEventArgs e)
        {
            try
            {
                //LogUtility.DebugMethodStart(e);

                if (this.parentForm.bt_func9.Enabled)
                {
                    switch (e.CellName)
                    {
                        case CELL_NAME_SHARYOU_CD:
                        case CELL_NAME_UNTENSHA_CD:
                        case CELL_NAME_SHASHU_CD:
                        case CELL_NAME_UNPAN_GYOUSHA_CD:
                            // 初期化
                            this.validationCancelFlg = false;
                            string comparisonValue = string.Empty;
                            var cell = (GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, e.CellName];

                            // ゼロパディング
                            if (cell.EditedFormattedValue != null && !string.IsNullOrEmpty(cell.EditedFormattedValue.ToString()))
                            {
                                var maxLength = ((GcCustomAlphaNumTextBoxCell)this.mrHaisha[e.RowIndex, e.CellIndex]).MaxLength;
                                comparisonValue = cell.EditedFormattedValue.ToString().PadLeft(maxLength, '0');
                            }

                            // 前回値と変更が無かったら処理中断
                            if (beforeValuesForHaisha.ContainsKey(e.CellName)
                                && beforeValuesForHaisha[e.CellName].Equals(
                                    Convert.ToString(comparisonValue)) && !this.validatedFlag)
                            {
                                if (e.CellName == CELL_NAME_SHARYOU_CD)
                                {
                                    string untenshaCd = this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_CD].Value == null ? string.Empty : this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_CD].Value.ToString();
                                    string beforeUntenshaCd = beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD] ==null?string.Empty:beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD];
                                    string shashuCd = this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_CD].Value == null ? string.Empty : this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_CD].Value.ToString();
                                    string beforeshashuCd = beforeValuesForHaisha[CELL_NAME_SHASHU_CD] ==null?string.Empty:beforeValuesForHaisha[CELL_NAME_SHASHU_CD];
                                    string gyoushaCd = this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_CD].Value == null ? string.Empty : this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_CD].Value.ToString();
                                    string beforegyoushaCd = beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD] == null ? string.Empty : beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD];
                                    if (beforeValuesForHaisha.ContainsKey(CELL_NAME_SHARYOU_NAME) && !string.IsNullOrEmpty(beforeValuesForHaisha[CELL_NAME_SHARYOU_NAME])
                                        && untenshaCd == beforeUntenshaCd
                                        && shashuCd == beforeshashuCd
                                        && gyoushaCd == beforegyoushaCd
                                        )
                                    {
                                        this.validationCancelFlg = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    this.validationCancelFlg = true;
                                    return;
                                }
                            }

                            if (e.RowIndex >= resultHaisha.Rows.Count)
                            {
                                // 空行クリック時は何もしない
                            }
                            else
                            {

                                var drHaisha = this.resultHaisha.Rows[e.RowIndex];

                                for (int i = 0; i < ConstClass.DENPYOU_COUNT; i++)
                                {
                                    string n = GetSuffix(i + 1);
                                    var denpyouNumber = drHaisha.Field<long>("DENPYOU_NUM_" + n).ToString();
                                    var shubetsu = drHaisha.Field<short>("SHUBETSU_KBN_" + n);

                                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS || shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                                    {
                                        if (!this.RenkeiCheck(denpyouNumber, "1", shubetsu))
                                        {
                                            // mobile使用中伝票が割り当てられている行の配車情報は編集不可
                                            // CDを戻す
                                            this.mrHaisha[e.RowIndex, e.CellIndex].Value = beforeValuesForHaisha[e.CellName];
                                            // 名称を戻す
                                            if (this.mrHaisha[e.RowIndex, e.CellIndex].Value != null)
                                            {
                                                var cd = this.mrHaisha[e.RowIndex, e.CellIndex].Value.ToString();
                                                switch (e.CellName)
                                                {
                                                    // 運転者
                                                    case (CELL_NAME_UNTENSHA_CD):
                                                        this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_NAME].Value = this.untenshaDao.GetDataByCd(cd) == null ? string.Empty : this.untenshaDao.GetDataByCd(cd).SHAIN_NAME_RYAKU;
                                                        break;

                                                    // 車種
                                                    case (CELL_NAME_SHASHU_CD):
                                                        this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_NAME].Value = this.shashuDao.GetDataByCd(cd) == null ? string.Empty : this.shashuDao.GetDataByCd(cd).SHASHU_NAME_RYAKU;
                                                        break;

                                                    // 運搬業者
                                                    case (CELL_NAME_UNPAN_GYOUSHA_CD):
                                                        this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_NAME].Value = this.unpanGyoyushaDao.GetDataByCd(cd) == null ? string.Empty : this.unpanGyoyushaDao.GetDataByCd(cd).GYOUSHA_NAME_RYAKU;
                                                        break;

                                                    // 車輌
                                                    case (CELL_NAME_SHARYOU_CD):
                                                        // 運転者
                                                        if (beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD] != null)
                                                        {
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_CD].Value = beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD];
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_NAME].Value = beforeValuesForHaisha[CELL_NAME_UNTENSHA_NAME];
                                                        }

                                                        // 車種
                                                        if (beforeValuesForHaisha[CELL_NAME_SHASHU_CD] != null)
                                                        {
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_CD].Value = beforeValuesForHaisha[CELL_NAME_SHASHU_CD];
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_NAME].Value = beforeValuesForHaisha[CELL_NAME_SHASHU_NAME];
                                                        }

                                                        // 運搬業者
                                                        if (beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD] != null)
                                                        {
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_CD].Value = beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD];
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_NAME].Value = beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_NAME];
                                                        }

                                                        // 車輌
                                                        if (beforeValuesForHaisha[CELL_NAME_SHARYOU_CD] != null)
                                                        {
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_SHARYOU_CD].Value = beforeValuesForHaisha[CELL_NAME_SHARYOU_CD];
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_SHARYOU_NAME].Value = beforeValuesForHaisha[CELL_NAME_SHARYOU_NAME];
                                                        }

                                                        break;
                                                }
                                            }
                                            // この後のValidetion処理を行わない
                                            this.validationCancelFlg = true;
                                            return;
                                        }
                                    }
                                    else if (shubetsu == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
                                    {
                                        if (!this.RenkeiCheck(denpyouNumber, "0", shubetsu))
                                        {
                                            // 確定伝票が割り当てられている行の配車情報は編集不可
                                            // CDを戻す
                                            this.mrHaisha[e.RowIndex, e.CellIndex].Value = beforeValuesForHaisha[e.CellName];
                                            // 名称を戻す
                                            if (this.mrHaisha[e.RowIndex, e.CellIndex].Value != null)
                                            {
                                                var cd = this.mrHaisha[e.RowIndex, e.CellIndex].Value.ToString();
                                                switch (e.CellName)
                                                {
                                                    // 運転者
                                                    case (CELL_NAME_UNTENSHA_CD):
                                                        this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_NAME].Value = this.untenshaDao.GetDataByCd(cd) == null ? string.Empty : this.untenshaDao.GetDataByCd(cd).SHAIN_NAME_RYAKU;
                                                        break;

                                                    // 車種
                                                    case (CELL_NAME_SHASHU_CD):
                                                        this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_NAME].Value = this.shashuDao.GetDataByCd(cd) == null ? string.Empty : this.shashuDao.GetDataByCd(cd).SHASHU_NAME_RYAKU;
                                                        break;

                                                    // 運搬業者
                                                    case (CELL_NAME_UNPAN_GYOUSHA_CD):
                                                        this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_NAME].Value = this.unpanGyoyushaDao.GetDataByCd(cd) == null ? string.Empty : this.unpanGyoyushaDao.GetDataByCd(cd).GYOUSHA_NAME_RYAKU;
                                                        break;

                                                    // 車輌
                                                    case (CELL_NAME_SHARYOU_CD):
                                                        // 運転者
                                                        if (beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD] != null)
                                                        {
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_CD].Value = beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD];
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_NAME].Value = beforeValuesForHaisha[CELL_NAME_UNTENSHA_NAME];
                                                        }

                                                        // 車種
                                                        if (beforeValuesForHaisha[CELL_NAME_SHASHU_CD] != null)
                                                        {
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_CD].Value = beforeValuesForHaisha[CELL_NAME_SHASHU_CD];
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_NAME].Value = beforeValuesForHaisha[CELL_NAME_SHASHU_NAME];
                                                        }

                                                        // 運搬業者
                                                        if (beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD] != null)
                                                        {
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_CD].Value = beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD];
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_NAME].Value = beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_NAME];
                                                        }

                                                        // 車輌
                                                        if (beforeValuesForHaisha[CELL_NAME_SHARYOU_CD] != null)
                                                        {
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_SHARYOU_CD].Value = beforeValuesForHaisha[CELL_NAME_SHARYOU_CD];
                                                            this.mrHaisha[e.RowIndex, CELL_NAME_SHARYOU_NAME].Value = beforeValuesForHaisha[CELL_NAME_SHARYOU_NAME];
                                                        }

                                                        break;
                                                }
                                            }
                                            // この後のValidetion処理を行わない
                                            this.validationCancelFlg = true;
                                            return;
                                        }
                                    }
                                }
                            }

                            // 配車種類のListを取得
                            var haishaShuruiList = this.mrHaisha.Rows[e.RowIndex].Cells.ToList().Where(c => c.Name.Contains("cellHaishaShurui")).ToList();
                            foreach (var shurui in haishaShuruiList)
                            {
                                // 確定伝票がある場合
                                if (shurui.Value != null && (shurui.Value.ToString() == ConstClass.HAISHA_SHURUI_KAKU))
                                {
                                    // 確定伝票が割り当てられている行の配車情報は編集不可
                                    msgLogic.MessageBoxShow("E229");

                                    // CDを戻す
                                    this.mrHaisha[e.RowIndex, e.CellIndex].Value = beforeValuesForHaisha[e.CellName];
                                    // 名称を戻す
                                    if (this.mrHaisha[e.RowIndex, e.CellIndex].Value != null)
                                    {
                                        var cd = this.mrHaisha[e.RowIndex, e.CellIndex].Value.ToString();
                                        switch (e.CellName)
                                        {
                                            // 運転者
                                            case (CELL_NAME_UNTENSHA_CD):
                                                this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_NAME].Value = this.untenshaDao.GetDataByCd(cd) == null ? string.Empty : this.untenshaDao.GetDataByCd(cd).SHAIN_NAME_RYAKU;
                                                break;

                                            // 車種
                                            case (CELL_NAME_SHASHU_CD):
                                                this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_NAME].Value = this.shashuDao.GetDataByCd(cd) == null ? string.Empty : this.shashuDao.GetDataByCd(cd).SHASHU_NAME_RYAKU;
                                                break;

                                            // 運搬業者
                                            case (CELL_NAME_UNPAN_GYOUSHA_CD):
                                                this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_NAME].Value = this.unpanGyoyushaDao.GetDataByCd(cd) == null ? string.Empty : this.unpanGyoyushaDao.GetDataByCd(cd).GYOUSHA_NAME_RYAKU;
                                                break;

                                            // 車輌
                                            case (CELL_NAME_SHARYOU_CD):
                                                // 運転者
                                                if (beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD] != null)
                                                {
                                                    this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_CD].Value = beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD];
                                                    this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_NAME].Value = beforeValuesForHaisha[CELL_NAME_UNTENSHA_NAME];
                                                }

                                                // 車種
                                                if (beforeValuesForHaisha[CELL_NAME_SHASHU_CD] != null)
                                                {
                                                    this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_CD].Value = beforeValuesForHaisha[CELL_NAME_SHASHU_CD];
                                                    this.mrHaisha[e.RowIndex, CELL_NAME_SHASHU_NAME].Value = beforeValuesForHaisha[CELL_NAME_SHASHU_NAME];
                                                }

                                                // 運搬業者
                                                if (beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD] != null)
                                                {
                                                    this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_CD].Value = beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD];
                                                    this.mrHaisha[e.RowIndex, CELL_NAME_UNPAN_GYOUSHA_NAME].Value = beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_NAME];
                                                }

                                                // 車輌
                                                if (beforeValuesForHaisha[CELL_NAME_SHARYOU_CD] != null)
                                                {
                                                    this.mrHaisha[e.RowIndex, CELL_NAME_SHARYOU_CD].Value = beforeValuesForHaisha[CELL_NAME_SHARYOU_CD];
                                                    this.mrHaisha[e.RowIndex, CELL_NAME_SHARYOU_NAME].Value = beforeValuesForHaisha[CELL_NAME_SHARYOU_NAME];
                                                }

                                                break;
                                        }
                                    }

                                    // この後のValidetion処理を行わない
                                    this.validationCancelFlg = true;
                                    break;
                                }
                            }
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckKakuteiDenpyou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                this.validationCancelFlg = true;
            }
            finally
            {
                //LogUtility.DebugMethodEnd();
            }
        }


        #endregion

        #region 配車CellValidated
        /// <summary>
        /// 配車CellValidated
        /// </summary>
        /// <param name="e"></param>
        public void mrHaisha_CellValidated(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (this.parentForm.bt_func9.Enabled)
                {
                    this.validatedFlag = false;
                    this.errorCell = null;

                    switch (e.CellName)
                    {
                        // 20141015 koukouei 休動管理機能 start
                        case CELL_NAME_SHARYOU_CD:
                            this.changeHaishaFlg = true;
                            // 車輌CD
                            var cellSharyouCd = (GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, e.CellName];

                            var sharyouCd = cellSharyouCd.Value == null ? "" : cellSharyouCd.Value.ToString();
                            if (sharyouCd != "")
                            {
                                var wd = (int)cellSharyouCd.CharactersNumber;
                                sharyouCd = sharyouCd.PadLeft(wd, '0');
                            }

                            var cellShashuCd = this.mrHaisha[e.RowIndex, "cellShashuCd"];
                            var cellShashuName = this.mrHaisha[e.RowIndex, "cellShashuName"];
                            var cellSharyouName = this.mrHaisha[e.RowIndex, "cellSharyouName"];
                            var cellUnpanGyoushaCd = this.mrHaisha[e.RowIndex, "cellUnpanGyoushaCd"];
                            var cellUnpanGyoushaName = this.mrHaisha[e.RowIndex, "cellUnpanGyoushaName"];
                            var cellSaidaiWakusuu = this.mrHaisha[e.RowIndex, "cellSaidaiWakusuu"];
                            var cellWorkClosedSharyou = this.mrHaisha[e.RowIndex, "cellWorkClosedSharyou"];

                            this.dtoHaisha.SharyouCd = sharyouCd;
                            if (!string.IsNullOrEmpty(cellShashuCd.Value.ToString()))
                            {
                                this.dtoHaisha.ShashuCd = cellShashuCd.Value.ToString();
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(this.form.searchConditionForShashuCd.Text))
                                {
                                    // もし絞り込まれていたら検索条件の車種で絞込み
                                    // 絞り込まれている状態で検索条件以外の車種を設定されると裏でおかしな挙動をしてしまうため
                                    this.dtoHaisha.ShashuCd = this.form.searchConditionForShashuCd.Text;
                                }
                                else
                                {
                                    this.dtoHaisha.ShashuCd = null;
                                }
                            }
                            if (!string.IsNullOrEmpty(cellUnpanGyoushaCd.Value.ToString()))
                            {
                                this.dtoHaisha.GyoushaCd = cellUnpanGyoushaCd.Value.ToString();
                            }
                            else
                            {
                                this.dtoHaisha.GyoushaCd = null;
                            }

                            System.Action ClearSharyou = () =>
                            {
                                // cellShashuCd.Value = "";
                                // cellShashuName.Value = "";
                                cellSharyouName.Value = "";
                                // cellUnpanGyoushaCd.Value = "";
                                // cellUnpanGyoushaName.Value = "";
                                cellSaidaiWakusuu.Value = ConstClass.DENPYOU_COUNT;
                                cellWorkClosedSharyou.Value = false;
                            };
                            if (this.dtoHaisha.SharyouCd == "")
                            {
                                ClearSharyou();
                            }
                            else
                            {
                                // 検索条件から運転者（社員CD）をはずす
                                string shainCD = this.dtoHaisha.ShainCd;
                                this.dtoHaisha.ShainCd = null;
                                cellSharyouName.Value = string.Empty;
                                var dt = this.dao_HAISHA.GetSharyou(this.dtoHaisha);
                                switch (dt.Rows.Count)
                                {
                                    case 0:
                                        this.msgLogic.MessageBoxShow("E020", "車輌");
                                        ((GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, e.CellName]).IsInputErrorOccured = true;
                                        this.validatedFlag = true;
                                        this.errorCell = cellSharyouCd;
                                        ClearSharyou();
                                        this.form.mrHaisha.Focus();
                                        return;
                                    case 1:
                                        var dr = dt.Rows[0];
                                        cellShashuCd.Value = dr["SHASYU_CD"];
                                        cellShashuName.Value = dr["SHASHU_NAME_RYAKU"];
                                        cellSharyouName.Value = dr["SHARYOU_NAME_RYAKU"];
                                        cellUnpanGyoushaCd.Value = dr["GYOUSHA_CD"];
                                        cellUnpanGyoushaName.Value = dr["GYOUSHA_NAME_RYAKU"];
                                        cellSaidaiWakusuu.Value = dr["SAIDAI_WAKU_SUU"];
                                        cellWorkClosedSharyou.Value = dr["WORK_CLOSED_SHARYOU"];

                                        // 20141015 koukouei 休動管理機能 start
                                        if (dr["WORK_CLOSED_SHARYOU"].Equals(true))
                                        {
                                            cellSharyouCd.IsInputErrorOccured = true;
                                            cellSharyouCd.Style.BackColor = Constans.ERROR_COLOR;
                                            cellSharyouName.Value = "";
                                            msgLogic.MessageBoxShow("E206", "車輛", string.Format("作業日：{0}", dtoHaisha.SagyouDate));
                                            this.validatedFlag = true;
                                            this.errorCell = cellSharyouCd;
                                            this.form.mrHaisha.Focus();
                                            return;
                                        }
                                        // 20141015 koukouei 休動管理機能 end
                                        break;
                                    default:
                                        //SendKeys.Send(" ");
                                        CustomControlExtLogic.PopUp(cellSharyouCd);
                                        this.validatedFlag = true;
                                        this.errorCell = cellSharyouCd;
                                        this.form.mrHaisha.Focus();
                                        return;
                                }
                                this.dtoHaisha.ShainCd = shainCD;
                            }

                            // 車輌CD
                            if (!IsShasyuSyaryou())
                            {
                                // 入力軸が運転者
                                var cell_UntenshaCd = this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_CD];
                                if (beforeValuesForHaisha.ContainsKey(CELL_NAME_UNTENSHA_CD)
                                    && !beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD].Equals(Convert.ToString(cell_UntenshaCd.Value))
                                    && !string.IsNullOrEmpty(beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD]))
                                {
                                    // 車輌CD入力時に同時セットされる運転者CDが、既に入力されている運転者CDと
                                    // 異なる場合は、同時セット前の運転者CDに戻す
                                    cell_UntenshaCd.Value = beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD];
                                    var cell_UntenshaName = this.mrHaisha[e.RowIndex, CELL_NAME_UNTENSHA_NAME];
                                    cell_UntenshaName.Value = beforeValuesForHaisha[CELL_NAME_UNTENSHA_NAME];
                                }
                            }
                            //this.mrMihaisha.Focus();
                            //this.mrHaisha.Focus();
                            break;

                        case CELL_NAME_UNTENSHA_CD:
                            this.changeHaishaFlg = true;
                            var cellUntenshaCd = (GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, e.CellName];

                            // 運転者CDが空の場合は名称をクリア
                            if (cellUntenshaCd.Value == null || string.IsNullOrEmpty(cellUntenshaCd.Value.ToString()))
                            {
                                this.mrHaisha[e.RowIndex, "cellUntenshaName"].Value = string.Empty;
                                return;
                            }

                            var cellUntenshaName = this.mrHaisha[e.RowIndex, "cellUntenshaName"];
                            cellUntenshaName.Value = string.Empty;
                            // 運転者ではない場合はエラー
                            this.dtoHaisha.ShainCd = Convert.ToString(cellUntenshaCd.Value);
                            var dtu = this.dao_HAISHA.GetShain(this.dtoHaisha);
                            if (dtu.Rows.Count > 0)
                            {
                                var dr = dtu.Rows[0];
                                if (dr["UNTEN_KBN"].Equals(false))
                                {
                                    msgLogic.MessageBoxShow("E020", "運転者");
                                    this.validatedFlag = true;
                                    this.errorCell = cellUntenshaCd;
                                    this.form.mrHaisha.Focus();
                                    return;
                                }
                                // 20141015 koukouei 休動管理機能 start
                                if (dr["WORK_CLOSED_UNTENSHA"].Equals(true))
                                {
                                    cellUntenshaCd.IsInputErrorOccured = true;
                                    cellUntenshaCd.Style.BackColor = Constans.ERROR_COLOR;
                                    msgLogic.MessageBoxShow("E206", "運転者", string.Format("作業日：{0}", dtoHaisha.SagyouDate));
                                    this.validatedFlag = true;
                                    this.errorCell = cellUntenshaCd;
                                    this.form.mrHaisha.Focus();
                                    return;
                                }
                                // 20141015 koukouei 休動管理機能 end
                                cellUntenshaName.Value = dr["SHAIN_NAME_RYAKU"];
                            }
                            else
                            {
                                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                                msgLogic.MessageBoxShow("E020", "運転者");
                                this.validatedFlag = true;
                                this.errorCell = cellUntenshaCd;
                                this.form.mrHaisha.Focus();
                                return;
                            }
                            // 運転者
                            this.dtoHaisha.ShainCd = this.mrHaisha[e.RowIndex, e.CellName].Value.ToString();
                            var cellBikou = this.mrHaisha[e.RowIndex, "cellBikou"];
                            var cellWorkClosedUntensha = this.mrHaisha[e.RowIndex, "cellWorkClosedUntensha"];
                            cellBikou.Value = "";
                            cellWorkClosedUntensha.Value = false;
                            if (this.dtoHaisha.ShainCd != "")
                            {
                                var dt = this.dao_HAISHA.GetShain(this.dtoHaisha);
                                if (dt.Rows.Count > 0)
                                {
                                    var dr = dt.Rows[0];
                                    // 備考欄など設定
                                    cellBikou.Value = dr["BIKOU"].ToString();
                                    cellWorkClosedUntensha.Value = dr["WORK_CLOSED_UNTENSHA"];
                                }
                            }
                            break;

                        case CELL_NAME_SHASHU_CD:
                            this.changeHaishaFlg = true;
                            var shashuCd = (GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, e.CellName];

                            // 車種CDが空の場合は名称をクリア
                            if (shashuCd.Value == null || string.IsNullOrEmpty(shashuCd.Value.ToString()))
                            {
                                this.mrHaisha[e.RowIndex, "cellShashuName"].Value = string.Empty;
                            }

                            // 条件に指定された車種と一緒かどうかチェック
                            if (!string.IsNullOrEmpty(this.form.searchConditionForShashuCd.Text)
                                && !this.form.searchConditionForShashuCd.Text.Equals(Convert.ToString(shashuCd.FormattedValue)))
                            {
                                msgLogic.MessageBoxShow("E034", "検索条件の車種CDと同じCDを");
                                this.validatedFlag = true;
                                this.errorCell = shashuCd;
                                this.form.mrHaisha.Focus();
                                return;
                            }
                            if (shashuCd.Value != null && !string.IsNullOrEmpty(shashuCd.Value.ToString()))
                            {
                                var shashu = shashuDao.GetAllValidData(new M_SHASHU() { SHASHU_CD = shashuCd.Value.ToString() }).FirstOrDefault();
                                if (shashu == null)
                                {
                                    msgLogic.MessageBoxShow("E020", "車種");
                                    this.validatedFlag = true;
                                    this.errorCell = shashuCd;
                                    this.form.mrHaisha.Focus();
                                    return;
                                }
                                this.mrHaisha[e.RowIndex, "cellShashuName"].Value = shashu.SHASHU_NAME_RYAKU;

                                // 車輌取得
                                M_SHARYOU keyEntity = new M_SHARYOU();
                                keyEntity.GYOUSHA_CD = Convert.ToString(this.mrHaisha[e.RowIndex, "cellUnpanGyoushaCd"].Value);
                                keyEntity.SHARYOU_CD = Convert.ToString(this.mrHaisha[e.RowIndex, "cellSharyouCd"].Value);
                                var sharyou = this.sharyouDao.GetAllValidData(keyEntity).FirstOrDefault();

                                if (sharyou == null)
                                {
                                    this.mrHaisha[e.RowIndex, "cellSharyouCd"].Value = string.Empty;
                                    this.mrHaisha[e.RowIndex, "cellSharyouName"].Value = string.Empty;
                                }

                                // 車種一致チェックを行う
                                else if (!string.IsNullOrEmpty(sharyou.SHASYU_CD) && Convert.ToString(shashuCd.Value) != sharyou.SHASYU_CD)
                                {
                                    // メッセージ表示
                                    this.msgLogic.MessageBoxShow("E104", "車輌CD", "車種");
                                    this.validatedFlag = true;
                                    this.errorCell = shashuCd;
                                    this.form.mrHaisha.Focus();
                                    return;
                                }
                            }
                            // ポップアップの設定を変更
                            PopupSearchSendParamDto gyoushaCdSearchParamDto = new PopupSearchSendParamDto();
                            gyoushaCdSearchParamDto.And_Or = CONDITION_OPERATOR.AND;
                            gyoushaCdSearchParamDto.Control = "cellUnpanGyoushaCd";
                            gyoushaCdSearchParamDto.KeyName = "key001";

                            PopupSearchSendParamDto sharyouCdSearchParamDto = new PopupSearchSendParamDto();
                            sharyouCdSearchParamDto.And_Or = CONDITION_OPERATOR.AND;
                            sharyouCdSearchParamDto.Control = "cellSharyouCd";
                            sharyouCdSearchParamDto.KeyName = "key002";

                            PopupSearchSendParamDto shashuCdSearchParamDto = new PopupSearchSendParamDto();
                            shashuCdSearchParamDto.And_Or = CONDITION_OPERATOR.AND;
                            shashuCdSearchParamDto.KeyName = "key003";

                            PopupSearchSendParamDto sagyouDateSearchParamDto = new PopupSearchSendParamDto();
                            sagyouDateSearchParamDto.And_Or = CONDITION_OPERATOR.AND;
                            sagyouDateSearchParamDto.Control = "SAGYOU_DATE";
                            sagyouDateSearchParamDto.KeyName = "SAGYOU_DATE";

                            PopupSearchSendParamDto tekiyouSearchParamDto = new PopupSearchSendParamDto();
                            tekiyouSearchParamDto.And_Or = CONDITION_OPERATOR.AND;
                            tekiyouSearchParamDto.KeyName = "TEKIYOU_FLG";
                            tekiyouSearchParamDto.Value = "FALSE";

                            if (shashuCd == null
                                || string.IsNullOrEmpty(Convert.ToString(shashuCd.FormattedValue)))
                            {
                                // 初期値を設定
                                shashuCdSearchParamDto.Control = "searchConditionForShashuCd";
                            }
                            else
                            {
                                shashuCdSearchParamDto.Control = CELL_NAME_SHASHU_CD;
                            }

                            Collection<PopupSearchSendParamDto> searchParamList = new Collection<PopupSearchSendParamDto>();
                            searchParamList.Add(shashuCdSearchParamDto);
                            searchParamList.Add(gyoushaCdSearchParamDto);
                            searchParamList.Add(sharyouCdSearchParamDto);
                            searchParamList.Add(sagyouDateSearchParamDto);
                            searchParamList.Add(tekiyouSearchParamDto);

                            var cell_sharyouCd = this.mrHaisha[e.RowIndex, CELL_NAME_SHARYOU_CD] as ICustomControl;
                            if (cell_sharyouCd != null)
                            {
                                cell_sharyouCd.PopupSearchSendParams = searchParamList;
                            }

                            //if (!string.IsNullOrEmpty(this.mrHaisha[e.RowIndex, CELL_NAME_SHARYOU_CD].Value.ToString()))
                            //{
                            //    // 車輌CDに既に入力がある場合は、車種CDの入力値を元に戻す
                            //    if (beforeValuesForHaisha.ContainsKey(CELL_NAME_SHASHU_CD)
                            //        && !beforeValuesForHaisha[CELL_NAME_SHASHU_CD].Equals(Convert.ToString(shashuCd.Value)))
                            //    {
                            //        shashuCd.Value = beforeValuesForHaisha[CELL_NAME_SHASHU_CD];
                            //    }
                            //}
                            break;

                        case CELL_NAME_UNPAN_GYOUSHA_CD:
                            this.changeHaishaFlg = true;
                            var cellUpnGyoushaCd = (GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, e.CellName];

                            // 運搬業者CDが空の場合は名称をクリア
                            if (cellUpnGyoushaCd.Value == null || string.IsNullOrEmpty(cellUpnGyoushaCd.Value.ToString()))
                            {
                                this.mrHaisha[e.RowIndex, "cellUnpanGyoushaName"].Value = string.Empty;
                            }

                            // 一旦初期化
                            var cellUpnGyoushaName = this.mrHaisha[e.RowIndex, "cellUnpanGyoushaName"];
                            cellUpnGyoushaName.Value = string.Empty;

                            var errorCD = false;
                            if (cellUpnGyoushaCd.Value != null && !string.IsNullOrEmpty(cellUpnGyoushaCd.Value.ToString()))
                            {
                                // 入力CDが運搬業者として登録されているかのチェック
                                var findEntity = new M_GYOUSHA();
                                findEntity.GYOUSHA_CD = cellUpnGyoushaCd.Value.ToString();
                                var entitys = this.gyoushaDao.GetAllValidData(findEntity);

                                List<M_GYOUSHA> retlist = new List<M_GYOUSHA>();
                                if (entitys != null && entitys.Length > 0)
                                {
                                    string sagyobi = string.Empty;
                                    DateTime sysDate = ((BusinessBaseForm)this.form.Parent).sysDate.Date;
                                    if (!string.IsNullOrEmpty(this.form.SAGYOU_DATE.Text))
                                    {
                                        sagyobi = this.form.SAGYOU_DATE.Value.ToString();
                                    }
                                    else
                                    {
                                        sagyobi = sysDate.ToString();
                                    }

                                    for (int i = 0; i < entitys.Length; i++)
                                    {
                                        string strBegin = entitys[i].TEKIYOU_BEGIN.ToString();
                                        string strEnd = entitys[i].TEKIYOU_END.ToString();
                                        if (string.IsNullOrEmpty(sagyobi))
                                        {
                                            sagyobi = sysDate.ToString();
                                        }

                                        if (entitys[i].TEKIYOU_BEGIN.IsNull)
                                        {
                                            strBegin = "0001/01/01 00:00:01";
                                        }

                                        if (entitys[i].TEKIYOU_END.IsNull)
                                        {
                                            strEnd = "9999/12/31 23:59:59";
                                        }

                                        //作業日は適用期間より範囲外の場合
                                        if (sagyobi.CompareTo(strBegin) < 0 || sagyobi.CompareTo(strEnd) > 0 || !entitys[0].UNPAN_JUTAKUSHA_KAISHA_KBN.IsTrue)
                                        {
                                            continue;
                                        }
                                        else
                                        {
                                            retlist.Add(entitys[i]);
                                        }
                                    }
                                }

                                if (retlist != null && retlist.Count > 0)
                                {
                                    // 運搬業者名をセット
                                    cellUpnGyoushaName.Value = retlist[0].GYOUSHA_NAME_RYAKU;
                                }
                                else
                                {
                                    errorCD = true;
                                }
                            }
                            //show message if gyousha CD is error
                            if (errorCD)
                            {
                                // CDが運搬業者に該当していなかったため、エラー
                                msgLogic.MessageBoxShow("E020", "業者");
                                //cellUpnGyoushaCd.Value = "";
                                this.validatedFlag = true;
                                this.errorCell = cellUpnGyoushaCd;
                                this.form.mrHaisha.Focus();
                            }

                            if (true == string.IsNullOrEmpty(cellUpnGyoushaCd.Value.ToString()))
                            {
                                // ブランク入力の場合、車輌をクリア
                                ((GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, "cellSharyouCd"]).Value = "";
                                ((GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, "cellSharyouName"]).Value = "";
                                ((GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, "cellSaidaiWakusuu"]).Value = ConstClass.DENPYOU_COUNT;
                            }
                            else
                            {
                                if ((true == beforeValuesForHaisha.ContainsKey(CELL_NAME_UNPAN_GYOUSHA_CD)) &&
                                   (false == beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD].Equals(Convert.ToString(cellUpnGyoushaCd.Value))))
                                {
                                    // 運搬業者に変更があった場合、車輌をクリア
                                    ((GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, "cellSharyouCd"]).Value = "";
                                    ((GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, "cellSharyouName"]).Value = "";
                                    ((GcCustomTextBoxCell)this.mrHaisha[e.RowIndex, "cellSaidaiWakusuu"]).Value = ConstClass.DENPYOU_COUNT;
                                }
                            }
                            break;
                        // 20141015 koukouei 休動管理機能 end
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("mrHaisha_CellValidated", ex);
                if (ex is SQLRuntimeException)
                {
                    this.msgLogic.MessageBoxShow("E093", "");
                }
                else
                {
                    this.msgLogic.MessageBoxShow("E245", "");
                }
            }
            finally
            {
                LogUtility.DebugMethodEnd();
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
        private object ChgDBNullToValue(object obj, object value)
        {
            object returnVal = null;
            if (obj is DBNull)
            {
                returnVal = value;
            }
            else
            {
                returnVal = obj;
            }

            return returnVal;
        }
        #endregion

        #region 入力軸に基づき必須入力項目を変更
        /// <summary>
        /// 入力軸に基づき必須入力項目を変更
        /// </summary>
        internal void SetRequiredSetting()
        {
            // 設定
            SelectCheckDto existCheck = new SelectCheckDto();
            existCheck.CheckMethodName = "必須チェック";
            Collection<SelectCheckDto> existChecks = new Collection<SelectCheckDto>();
            existChecks.Add(existCheck);

            // 配車明細の必須チェック項目変更
            this.mrHaisha.SuspendLayout();
            foreach (var rowHaisha in this.mrHaisha.Rows)
            {
                if (IsShasyuSyaryou())
                {
                    // 入力軸が車輌
                    var obj1 = rowHaisha.Cells[CELL_NAME_SHARYOU_CD];
                    var obj2 = rowHaisha.Cells[CELL_NAME_UNTENSHA_CD];
                    PropertyUtility.SetValue(obj1, "RegistCheckMethod", existChecks);
                    PropertyUtility.SetValue(obj2, "RegistCheckMethod", null);
                }
                else
                {
                    // 入力軸が運転者
                    var obj1 = rowHaisha.Cells[CELL_NAME_SHARYOU_CD];
                    var obj2 = rowHaisha.Cells[CELL_NAME_UNTENSHA_CD];
                    PropertyUtility.SetValue(obj1, "RegistCheckMethod", null);
                    PropertyUtility.SetValue(obj2, "RegistCheckMethod", existChecks);
                }
            }
            this.mrHaisha.ResumeLayout();
            PropertyUtility.SetValue(this.header.KYOTEN_CD, "RegistCheckMethod", existChecks);
            PropertyUtility.SetValue(this.form.dtpSagyouDate, "RegistCheckMethod", existChecks);
            PropertyUtility.SetValue(this.form.tbShoriKbn, "RegistCheckMethod", existChecks);
        }
        #endregion

        #region 受付指示書(控え)印刷するか
        /// <summary>
        /// 受付指示書(控え)印刷するか
        /// </summary>
        /// <returns>true:印刷　false:印刷しない</returns>
        internal bool IsHikaePrint()
        {
            bool retResult = false;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.sysInfoEntity != null)
                {
                    // 1の場合
                    if (this.ChgDBNullToValue(sysInfoEntity.UKETSUKE_SIJISHO_SUB_PRINT_KBN, string.Empty).ToString().Equals("1"))
                    {
                        // trueを返す
                        retResult = true;
                    }
                }

                return retResult;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(retResult);
            }
        }
        #endregion

        #region その他

        public void LogicalDelete()
        {
            throw new NotImplementedException();
        }

        public void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private string GetColumn(Cell cell)
        {
            return GetColumn(cell.Name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cellName"></param>
        /// <returns></returns>
        private string GetColumn(string cellName)
        {
            return cellName.Substring(cellName.Length - 2);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private string GetSuffix(int i)
        {
            return i.ToString("D2");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool isDenpyouContent(Cell cell)
        {
            return cell != null && cell.Name.Contains("DenpyouContent");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool isNewHaisha(Cell cell)
        {
            return cell.RowIndex == this.resultHaisha.Rows.Count;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private bool isInputKey(Cell cell)
        {
            bool returnVal = false;
            if (cell.RowIndex < this.resultHaisha.Rows.Count)
            {
                var cellUntenshaCd = this.mrHaisha[cell.RowIndex, "cellUntenshaCd"];
                var cellSharyouCd = this.mrHaisha[cell.RowIndex, "cellSharyouCd"];

                if (IsShasyuSyaryou())
                {
                    // 入力軸が車輌の場合は、車輌が入力必須
                    if (!string.IsNullOrEmpty(cellSharyouCd.Value.ToString()))
                    {
                        returnVal = true;
                    }
                }
                else
                {
                    // 入力軸が運転者の場合は、運転者が入力必須
                    if (!string.IsNullOrEmpty(cellUntenshaCd.Value.ToString()))
                    {
                        returnVal = true;
                    }
                }
            }
            return returnVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool isRowDouble()
        {
            try
            {
                // 入力行の運転者、車輌、業者に重複がないかどうかチェック
                Dictionary<string, string> mrHaishaInputRow = new Dictionary<string, string>();
                foreach (var drHaisha in this.resultHaisha.AsEnumerable())
                {
                    string RowKey = drHaisha["SHAIN_CD"].ToString() + drHaisha["GYOUSHA_CD"].ToString() + drHaisha["SHARYOU_CD"].ToString();
                    string dummyData = string.Empty;
                    mrHaishaInputRow.Add(RowKey, dummyData);
                }

            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);

                if (ex is System.ArgumentException)
                {
                    msgLogic.MessageBoxShow("E031", "入力行の運転者と車輌");
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        /// <summary>
        /// 運転者チェック
        /// </summary>
        internal bool CheckUntensha()
        {
            bool ret = false;
            try
            {
                LogUtility.DebugMethodStart();

                // 初期化
                this.form.UNTENSHA_NAME.Text = string.Empty;

                if (string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
                {
                    // 運転者CDがなければ何もしない。
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }

                // 入力された運転者CDでマスタ検索
                this.dtoHaisha.ShainCd = this.form.UNTENSHA_CD.Text;
                var dt = this.dao_HAISHA.GetShain(this.dtoHaisha);
                if (dt.Rows.Count == 0)
                {
                    // エラーメッセージ（存在しない場合）
                    this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                    this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                    msgLogic.MessageBoxShow("E020", "社員");
                    this.form.UNTENSHA_CD.Focus();
                    ret = false;
                    LogUtility.DebugMethodEnd(ret);
                    return ret;
                }
                else
                {
                    var dr = dt.Rows[0];
                    if (!(Boolean)dr["UNTEN_KBN"])
                    {
                        // エラーメッセージ（運転者ではない場合）
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        msgLogic.MessageBoxShow("E020", "運転者");
                        this.form.UNTENSHA_CD.Focus();
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                    // 20141015 koukouei 休動管理機能 start
                    // 休動チェック
                    else if ((Boolean)dr["WORK_CLOSED_UNTENSHA"])
                    {
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = r_framework.Const.Constans.ERROR_COLOR;
                        msgLogic.MessageBoxShow("E206", "運転者", string.Format("作業日：{0}", dtoHaisha.SagyouDate));
                        this.form.UNTENSHA_CD.Focus();
                        ret = false;
                        LogUtility.DebugMethodEnd(ret);
                        return ret;
                    }
                    // 20141015 koukouei 休動管理機能 end
                    else
                    {
                        this.form.UNTENSHA_NAME.Text = dr["SHAIN_NAME_RYAKU"].ToString();
                    }
                }
                ret = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckUntensha", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 対象の伝票が確定済みかチェックを行う
        /// </summary>
        /// <param name="row">チェック対象行</param>
        /// <param name="kbn">登録されている受付伝票種別</param>
        /// <param name="colNum">カラムNo</param>
        /// <returns name="bool">TRUE:確定済み, FALSE:確定されていない</returns>
        private bool checkHaishaShurui(DataRow row, short kbn, string colNum)
        {
            var ret = false;

            // 配車種類CD
            Int16 cd = CommonConst.HAISHA_SHURUI_TSUUJOU;

            // KEY情報を取得
            var systemID = row.Field<Int64>("SYSTEM_ID_" + colNum);
            var seq = row.Field<Int32>("SEQ" + colNum);

            if (kbn == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
            {
                // 収集受付より伝票検索
                var dto = new DTO_IdSeq();
                dto.SystemId = systemID;
                dto.Seq = seq;
                var entity = this.daoT_UKETSUKE_SS_ENTRY.GetData(dto);

                if (entity != null)
                {
                    // 該当伝票があれば、配車種類CDを取得
                    cd = entity.HAISHA_SHURUI_CD.Value;
                }
            }
            else if (kbn == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
            {
                // 出荷受付より伝票検索
                var dto = new DTO_IdSeq();
                dto.SystemId = systemID;
                dto.Seq = seq;
                var entity = this.daoT_UKETSUKE_SK_ENTRY.GetData(dto);

                if (entity != null)
                {
                    // 該当伝票があれば、配車種類CDを取得
                    cd = entity.HAISHA_SHURUI_CD.Value;
                }
            }

            if (cd == CommonConst.HAISHA_SHURUI_KAKUTEI)
            {
                // 確定済み
                ret = true;
            }

            return ret;
        }

        /// <summary>
        /// 車輌または運転者CDの入力が無い場合のエラー制御
        /// </summary>
        /// <returns></returns>
        private bool SharyouUntenshaInputErrorCheck()
        {
            LogUtility.DebugMethodStart();

            bool retVal = false;
            bool[] errFlgSharyouCd = new bool[this.mrHaisha.Rows.Count];
            bool[] errFlgUntenshaCd = new bool[this.mrHaisha.Rows.Count];
            int rowIndex = -1;

            ClearInputError();
            this.SetAllUketsuke((dr, n, isSS, idx) =>
            {
                if (dr.Field<string>("HAISHA_SHURUI" + n) == ConstClass.HAISHA_SHURUI_KAKU)
                {
                    // 車輌CD又は運転者CDに入力が無い場合
                    if (dr.Field<string>("SHARYOU_CD") == null || dr.Field<string>("SHARYOU_CD").Equals(string.Empty))
                    {
                        // 車輌CD入力無し
                        errFlgSharyouCd[idx] = true;
                        if (rowIndex < 0) rowIndex = idx;
                    }
                    if (dr.Field<string>("SHAIN_CD") == null || dr.Field<string>("SHAIN_CD").Equals(string.Empty))
                    {
                        // 運転者CD入力無し
                        errFlgUntenshaCd[idx] = true;
                        if (rowIndex < 0) rowIndex = idx;
                    }
                }
            }
            );
            if (rowIndex >= 0)
            {
                msgLogic.MessageBoxShow("E197");
                SetNInputError(errFlgSharyouCd, errFlgUntenshaCd, rowIndex);
                retVal = true;
            }

            LogUtility.DebugMethodEnd();
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
        // 20141015 koukouei 休動管理機能 start
        #region 休動チェック
        public bool ChkWorkClose()
        {
            
            string shashuCd = this.dtoHaisha.ShashuCd;
            this.dtoHaisha.ShashuCd = null;
            if (!string.IsNullOrEmpty(this.form.UNTENSHA_CD.Text))
            {
                // 運転者
                this.dtoHaisha.ShainCd = Convert.ToString(this.form.UNTENSHA_CD.Text);
                var dtu = this.dao_HAISHA.GetShain(this.dtoHaisha);
                if (dtu.Rows.Count > 0)
                {
                    var dr = dtu.Rows[0];
                    if (dr["WORK_CLOSED_UNTENSHA"].Equals(true))
                    {
                        this.form.UNTENSHA_CD.IsInputErrorOccured = true;
                        this.form.UNTENSHA_CD.BackColor = Constans.ERROR_COLOR;
                        msgLogic.MessageBoxShow("E206", "運転者", string.Format("作業日：{0}", dtoHaisha.SagyouDate));
                        return false;
                    }
                }
            }

            foreach (Row row in this.form.mrHaisha.Rows)
            {
                var cellUntenshaCd = (GcCustomTextBoxCell)row[CELL_NAME_UNTENSHA_CD];
                var cellSharyouCd = (GcCustomTextBoxCell)row[CELL_NAME_SHARYOU_CD];
                var cellGyoushaCd = (GcCustomTextBoxCell)row[CELL_NAME_UNPAN_GYOUSHA_CD];

                if (!string.IsNullOrEmpty(Convert.ToString(cellUntenshaCd.Value)))
                {
                    // 運転者
                    this.dtoHaisha.ShainCd = Convert.ToString(cellUntenshaCd.Value);
                    var dtu = this.dao_HAISHA.GetShain(this.dtoHaisha);
                    if (dtu.Rows.Count > 0)
                    {
                        var dr = dtu.Rows[0];
                        if (dr["WORK_CLOSED_UNTENSHA"].Equals(true))
                        {
                            cellUntenshaCd.IsInputErrorOccured = true;
                            cellUntenshaCd.Style.BackColor = Constans.ERROR_COLOR;
                            msgLogic.MessageBoxShow("E206", "運転者", string.Format("作業日：{0}", dtoHaisha.SagyouDate));
                            return false;
                        }
                    }
                }

                if (!string.IsNullOrEmpty(Convert.ToString(cellSharyouCd.Value)))
                {
                    // 車輛
                    this.dtoHaisha.SharyouCd = Convert.ToString(cellSharyouCd.Value);
                    this.dtoHaisha.GyoushaCd = Convert.ToString(cellGyoushaCd.Value);
                    var dt = this.dao_HAISHA.GetSharyou(this.dtoHaisha);
                    if (dt.Rows.Count > 0)
                    {
                        var dr = dt.Rows[0];
                        if (dr["WORK_CLOSED_SHARYOU"].Equals(true))
                        {
                            cellSharyouCd.IsInputErrorOccured = true;
                            cellSharyouCd.Style.BackColor = Constans.ERROR_COLOR;
                            msgLogic.MessageBoxShow("E206", "車輛", string.Format("作業日：{0}", dtoHaisha.SagyouDate));
                            return false;
                        }
                    }
                }
            }

            this.dtoHaisha.ShashuCd = shashuCd;
            return true;
        }
        #endregion
        // 20141015 koukouei 休動管理機能 end

        #region 20150727 hoanghm add

        /// <summary>
        /// Clear require setting in haisha grid
        /// </summary>
        internal void ClearRequiredSetting()
        {
            this.mrHaisha.SuspendLayout();
            foreach (var rowHaisha in this.mrHaisha.Rows)
            {
                var obj1 = rowHaisha.Cells[CELL_NAME_SHARYOU_CD];
                var obj2 = rowHaisha.Cells[CELL_NAME_UNTENSHA_CD];
                PropertyUtility.SetValue(obj1, "RegistCheckMethod", null);
                PropertyUtility.SetValue(obj2, "RegistCheckMethod", null);
            }
            this.mrHaisha.ResumeLayout();

            PropertyUtility.SetValue(this.header.KYOTEN_CD, "RegistCheckMethod", null);
            PropertyUtility.SetValue(this.form.dtpSagyouDate, "RegistCheckMethod", null);
            PropertyUtility.SetValue(this.form.tbShoriKbn, "RegistCheckMethod", null);

            this.form.RegistErrorFlag = false;
        }

        /// <summary>
        /// Set focus on control in haisha grid
        /// </summary>
        internal void SetFocusInGridHaisha()
        {
            this.mrHaisha.Focus();
            foreach (var rowHaisha in this.mrHaisha.Rows)
            {
                if (IsShasyuSyaryou())
                {
                    // 入力軸が車輌
                    var cellSharyou = (GcCustomTextBoxCell)rowHaisha.Cells[CELL_NAME_SHARYOU_CD];
                    if (cellSharyou.IsInputErrorOccured == true)
                    {
                        cellSharyou.Selected = true;
                        this.mrHaisha.FirstDisplayedCellPosition = new CellPosition(cellSharyou.RowIndex, cellSharyou.CellIndex);
                        break;
                    }
                }
                else
                {
                    // 入力軸が運転者
                    var cellUntensha = (GcCustomTextBoxCell)rowHaisha.Cells[CELL_NAME_UNTENSHA_CD];
                    if (cellUntensha.IsInputErrorOccured == true)
                    {
                        cellUntensha.Selected = true;
                        this.mrHaisha.FirstDisplayedCellPosition = new CellPosition(cellUntensha.RowIndex, cellUntensha.CellIndex);
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Check input and show messages when buttun F8 press
        /// </summary>
        /// <returns>false: error input</returns>
        internal bool CheckInpurtSearch()
        {
            MessageUtility messageUtility = new MessageUtility();
            string msg = "";
            if (string.IsNullOrEmpty(this.header.KYOTEN_CD.Text))
            {
                this.header.KYOTEN_CD.IsInputErrorOccured = true;
                msg = string.Format(messageUtility.GetMessage("E001").MESSAGE, "拠点");
            }
            if (string.IsNullOrEmpty(this.form.dtpSagyouDate.Text))
            {
                this.form.dtpSagyouDate.IsInputErrorOccured = true;
                msg += "\n" + string.Format(messageUtility.GetMessage("E001").MESSAGE, "作業日");
            }
            if (string.IsNullOrEmpty(this.form.tbShoriKbn.Text))
            {
                this.form.tbShoriKbn.IsInputErrorOccured = true;
                msg += "\n" + string.Format(messageUtility.GetMessage("E001").MESSAGE, "処理区分");
            }
            if (this.header.KYOTEN_CD.Text == "")
            {
                this.msgLogic.MessageBoxShowError(msg);
                this.header.KYOTEN_CD.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(this.form.dtpSagyouDate.Text))
            {
                this.msgLogic.MessageBoxShowError(msg);
                this.form.dtpSagyouDate.Focus();
                return false;
            }
            else if (string.IsNullOrEmpty(this.form.tbShoriKbn.Text))
            {
                this.msgLogic.MessageBoxShowError(msg);
                this.form.tbShoriKbn.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Focus to error control not in Haisha grid (Kyoten,...)
        /// </summary>
        /// <returns>true: not focus in haisha grid; false: focus in haisha grid</returns>
        internal bool FocusErrorControl()
        {
            if (this.header.KYOTEN_CD.Text == "")
            {
                this.header.KYOTEN_CD.Focus();
                return true;
            }
            else if (string.IsNullOrEmpty(this.form.dtpSagyouDate.Text))
            {
                this.form.dtpSagyouDate.Focus();
                return true;
            }
            else if (string.IsNullOrEmpty(this.form.tbShoriKbn.Text))
            {
                this.form.tbShoriKbn.Focus();
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region 20150728 hoanghm add


        internal void PopUpAfter()
        {
            var row = this.mrHaisha.CurrentRow;
            var cell = this.mrHaisha.CurrentCell;
            cell.Value = cell.Value == null ? string.Empty : cell.Value;
            if (this.beforeValuesForHaisha.ContainsKey(cell.Name))
            {
                // 運転者
                if (cell.Name == CELL_NAME_UNTENSHA_CD)
                {
                    // 運転者CD
                    beforeValuesForHaisha[CELL_NAME_UNTENSHA_CD] = cell.Value.ToString();
                    // 運転者名
                    beforeValuesForHaisha[CELL_NAME_UNTENSHA_NAME] = row.Cells[CELL_NAME_UNTENSHA_NAME].ToString();
                }
                // 車種
                if (cell.Name == CELL_NAME_SHASHU_CD)
                {
                    // 車種CD
                    beforeValuesForHaisha[CELL_NAME_SHASHU_CD] = cell.Value.ToString();
                    // 車種名
                    beforeValuesForHaisha[CELL_NAME_SHASHU_NAME] = row.Cells[CELL_NAME_SHASHU_NAME].ToString();
                }
                // 運搬業者
                if (cell.Name == CELL_NAME_UNPAN_GYOUSHA_CD)
                {
                    // 運搬業者CD
                    beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_CD] = cell.Value.ToString();
                    // 運搬業者名
                    beforeValuesForHaisha[CELL_NAME_UNPAN_GYOUSHA_NAME] = row.Cells[CELL_NAME_UNPAN_GYOUSHA_NAME].ToString();
                }
            }
        }

        #endregion

        #region 連携チェック
        internal bool RenkeiCheck(string uketsukeNum, string srenkeiKbn, short shubetsu)
        {
            try
            {
                if (string.IsNullOrEmpty(uketsukeNum))
                {
                    return true;
                }

                DataTable dt = this.mobisyoRtDao.GetRenkeiData(srenkeiKbn, uketsukeNum);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
                    {
                        this.msgLogic.MessageBoxShow("E261", "現在回収中", "編集");
                    }
                    if (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK)
                    {
                        this.msgLogic.MessageBoxShow("E261", "現在運搬中", "編集");
                    }
                    if (shubetsu == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
                    {
                        this.msgLogic.MessageBoxShow("E261", "現在配車中", "編集");
                    }
                    return false;
                }

                // ロジこんぱす連携済みであるかをチェックする。
                // 配送Noが設定されている場合は、連携済みとしてアラートを表示する。
                string selectStr;
                if ((shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SS)
                    || (shubetsu == ConstClass.SHUBETSU_KBN_UKETSUKE_SK))
                {
                    // 抽出1件に絞らないとエラーする
                    selectStr = "SELECT TOP 1 * FROM T_LOGI_LINK_STATUS LLS"
                        + " LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD ON LDD.SYSTEM_ID = LLS.SYSTEM_ID AND LDD.DELETE_FLG = 0"
                        + " WHERE LDD.REF_DENPYOU_NO = " + uketsukeNum
                        + " AND LDD.DENPYOU_ATTR != 3"  //3:定期以外
                        + " AND LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                        + " AND LLS.DELETE_FLG = 0";
                }
                else
                {
                    // 抽出1件に絞らないとエラーする
                    selectStr = "SELECT TOP 1 * FROM T_LOGI_LINK_STATUS LLS"
                        + " LEFT JOIN T_LOGI_DELIVERY_DETAIL LDD ON LDD.SYSTEM_ID = LLS.SYSTEM_ID AND LDD.DELETE_FLG = 0"
                        + " WHERE LDD.REF_DENPYOU_NO = " + uketsukeNum
                        + " AND LDD.DENPYOU_ATTR = 3"  //3:定期
                        + " AND LLS.LINK_STATUS <> 3"  // 「3：受信済」以外
                        + " AND LLS.DELETE_FLG = 0";
                }


                // データ取得
                dt = this.dao_HAISHA.GetDateForStringSql(selectStr);
                // 連携済みの場合はアラートを表示する。
                if (dt.Rows.Count > 0)
                {
                    this.msgLogic.MessageBoxShow("E261", "ロジこんぱす連携中", "編集");
                    return false;
                }

                // NAVITIME連携中であるかをチェックする。(定期のみ)
                if (shubetsu == ConstClass.SHUBETSU_KBN_TEIKI_HAISHA)
                {
                    selectStr = " SELECT TOP 1 * FROM T_TEIKI_HAISHA_ENTRY T "
                                + " INNER JOIN T_NAVI_DELIVERY D ON T.SYSTEM_ID = D.TEIKI_SYSTEM_ID AND D.DELETE_FLG = 0 "
                                + " INNER JOIN T_NAVI_LINK_STATUS L ON D.SYSTEM_ID = L.SYSTEM_ID AND L.LINK_STATUS != 3 "
                                + " WHERE T.DELETE_FLG = 0 "
                                + " AND T.TEIKI_HAISHA_NUMBER = " + uketsukeNum;

                    // データ取得
                    dt = this.dao_HAISHA.GetDateForStringSql(selectStr);
                    // 連携済みの場合はアラートを表示する。
                    if (dt.Rows.Count > 0)
                    {
                        this.msgLogic.MessageBoxShow("E261", "NAVITIME連携中", "編集");
                        return false;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("RenkeiCheck", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RenkeiCheck", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                return false;
            }

            return true;
        }
        #endregion

        #region 地図表示設定呼び出し

        private void mapPopupOpen(int rowIndex)
        {
            try
            {
                DataTable dt = new DataTable();

                // 地図表示設定に渡したい項目を作成
                dt.Columns.Add("SAGYOU_DATE");          // 作業日
                dt.Columns.Add("SARYOU_CD");            // 車輛CD
                dt.Columns.Add("SARYOU_NAME");          // 車輛名
                dt.Columns.Add("SHASHU_CD");            // 車種CD
                dt.Columns.Add("SHASHU_NAME");          // 車種名
                dt.Columns.Add("UNTENSHA_CD");          // 運転者CD
                dt.Columns.Add("UNTENSHA_NAME");        // 運転者名
                dt.Columns.Add("UNPAN_GYOUSHA_CD");     // 運搬業者CD
                dt.Columns.Add("UNPAN_GYOUSHA_NAME");   // 運搬業者名
                dt.Columns.Add("COLUMN_NUMBER");        // 割当済みの列番号
                dt.Columns.Add("DATA_SHURUI");          // データの種類(0：割当済み、1：未配車)
                dt.Columns.Add("SHUBETSU_KBN");         // 種別区分(0：空、1：収集受付、2：出荷受付、3：定期配車)
                dt.Columns.Add("SYSTEM_ID");            // システムID
                dt.Columns.Add("SEQ");                  // SEQ

                // 地図表示設定に渡したい割当済みの情報を設定
                var dr = this.resultHaisha.Rows[rowIndex];
                for (int c = 0; c < ConstClass.DENPYOU_COUNT; ++c)
                {
                    DataRow datarow;
                    datarow = dt.NewRow();
                    datarow["SAGYOU_DATE"] = this.form.SAGYOU_DATE.Text;
                    datarow["SARYOU_CD"] = Convert.ToString(this.mrHaisha[rowIndex, "cellSharyouCd"].Value);
                    datarow["SARYOU_NAME"] = Convert.ToString(this.mrHaisha[rowIndex, "cellSharyouName"].Value);
                    datarow["SHASHU_CD"] = Convert.ToString(this.mrHaisha[rowIndex, "cellShashuCd"].Value);
                    datarow["SHASHU_NAME"] = Convert.ToString(this.mrHaisha[rowIndex, "cellShashuName"].Value);
                    datarow["UNTENSHA_CD"] = Convert.ToString(this.mrHaisha[rowIndex, "cellUntenshaCd"].Value);
                    datarow["UNTENSHA_NAME"] = Convert.ToString(this.mrHaisha[rowIndex, "cellUntenshaName"].Value);
                    datarow["UNPAN_GYOUSHA_CD"] = Convert.ToString(this.mrHaisha[rowIndex, "cellUnpanGyoushaCd"].Value);
                    datarow["UNPAN_GYOUSHA_NAME"] = Convert.ToString(this.mrHaisha[rowIndex, "cellUnpanGyoushaName"].Value);
                    datarow["COLUMN_NUMBER"] = c;
                    datarow["DATA_SHURUI"] = 0;
                    var n = GetSuffix(c + 1);
                    datarow["SHUBETSU_KBN"] = dr.Field<short>("SHUBETSU_KBN_" + n);
                    datarow["SYSTEM_ID"] = dr.Field<long>("SYSTEM_ID_" + n);
                    datarow["SEQ"] = dr.Field<int>("SEQ" + n);

                    dt.Rows.Add(datarow);
                }

                // 地図表示設定に渡したい未配車の情報を設定
                for (int r = 0; r < this.resultMihaisha.Rows.Count; ++r)
                {
                    dr = this.resultMihaisha.Rows[r];
                    this.dtoIdSeq.SystemId = dr.Field<long>("SYSTEM_ID_");
                    this.dtoIdSeq.Seq = dr.Field<int>("SEQ");

                    DataRow datarow;
                    datarow = dt.NewRow();
                    datarow["SAGYOU_DATE"] = this.form.SAGYOU_DATE.Text;
                    datarow["SARYOU_CD"] = string.Empty;
                    datarow["SARYOU_NAME"] = string.Empty;
                    datarow["SHASHU_CD"] = string.Empty;
                    datarow["SHASHU_NAME"] = string.Empty;
                    datarow["UNTENSHA_CD"] = string.Empty;
                    datarow["UNTENSHA_NAME"] = string.Empty;
                    datarow["UNPAN_GYOUSHA_CD"] = string.Empty;
                    datarow["UNPAN_GYOUSHA_NAME"] = string.Empty;
                    datarow["COLUMN_NUMBER"] = r + 1;
                    datarow["DATA_SHURUI"] = 1;
                    datarow["SHUBETSU_KBN"] = dr.Field<short>("SHUBETSU_KBN_");
                    datarow["SYSTEM_ID"] = dr.Field<long>("SYSTEM_ID_");
                    datarow["SEQ"] = dr.Field<int>("SEQ");
                    dt.Rows.Add(datarow);
                }

                mapPopupForm initialPopupForm = new mapPopupForm();
                initialPopupForm.table = dt;
                initialPopupForm.ShowDialog();
            }
            catch (Exception ex)
            {
                LogUtility.Error("mapPopupOpen", ex);
                this.msgLogic.MessageBoxShowError(ex.Message);
            }
        }

        private bool GetGyousha(string gyoushaCd)
        {

            return true;
        }

        private bool GetGenba(string gyoushaCd, string genbaCd)
        {
            var gyoushaEntity = new M_GYOUSHA();

            return true;
        }
        #endregion

        //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 START
        private bool RenkeiInxsCheckChangeSagyou(string uketsukeNumber)
        {
            bool isOk = true;
            try
            {

                if (string.IsNullOrEmpty(uketsukeNumber))
                {
                    return isOk;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT T_USSE.UKETSUKE_NUMBER ");
                sql.Append(" FROM T_UKETSUKE_SS_ENTRY T_USSE ");
                sql.Append(" INNER JOIN T_PICKUP_REQUEST_UKETSUKE_SS_INXS AS PRU_SS_INXS ");
                sql.Append(" ON PRU_SS_INXS.UKETSUKE_SYSTEM_ID = T_USSE.SYSTEM_ID ");
                sql.Append(" INNER JOIN T_PICKUP_REQUEST_INXS ");
                sql.Append(" ON T_PICKUP_REQUEST_INXS.SYSTEM_ID = PRU_SS_INXS.REQUEST_SYSTEM_ID ");
                sql.Append(" AND T_PICKUP_REQUEST_INXS.DELETE_FLG = 0 ");
                sql.Append(" INNER JOIN T_PICKUP_REQUEST_PREFERENCE_DATE_INXS AS PREFERENCE_DATE ");
                sql.Append(" ON PREFERENCE_DATE.SYSTEM_ID = T_PICKUP_REQUEST_INXS.SYSTEM_ID  ");
                sql.Append(" AND PREFERENCE_DATE.SEQ = T_PICKUP_REQUEST_INXS.SEQ ");
                sql.Append(" AND PREFERENCE_DATE.[ROW_NUMBER] = 10 ");
                sql.AppendFormat(" WHERE T_USSE.UKETSUKE_NUMBER = ({0}) ", uketsukeNumber);
                sql.AppendFormat(" AND PREFERENCE_DATE.PREFERENCE_DATE != CONVERT(DATETIME, '{0}', 102) ", DateTime.Parse(this.form.SAGYOU_DATE.Text).ToString("yyyy-MM-dd"));
                sql.Append(" AND T_USSE.DELETE_FLG = 0 ");

                var dt = this.dao_HAISHA.GetDateForStringSql(sql.ToString());
                // 連携済みの場合はアラートを表示する。
                if (dt != null && dt.Rows.Count > 0)
                {
                    short haishaHenkouSagyouDatekbn = 1;
                    if (this.sysInfoEntity != null && !this.sysInfoEntity.HAISHA_HENKOU_SAGYOU_DATE_KBN.IsNull)
                    {
                        haishaHenkouSagyouDatekbn = this.sysInfoEntity.HAISHA_HENKOU_SAGYOU_DATE_KBN.Value;
                    }

                    string error = string.Empty;
                    if (haishaHenkouSagyouDatekbn == 1) //Confirm
                    {
                        error = string.Format("受付番号：{0}のINXS依頼情報画面の確定日と作業日が異なりますが、「F9：登録」ボタン押下で作業日を変更します。収集受付担当者に連絡してください。", uketsukeNumber);
                        if (this.msgLogic.MessageBoxShowConfirm(error, MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                        {
                            isOk = false;
                        }
                    }
                    else //Error
                    {
                        error = string.Format("受付番号：{0}のINXS依頼情報画面の確定日と作業日が異なる為、割り当てできません。", uketsukeNumber);
                        isOk = false;
                        this.msgLogic.MessageBoxShowError(error);
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("RenkeiInxsCheckChangeSagyou", ex1);
                this.msgLogic.MessageBoxShow("E093", "");
                isOk = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("RenkeiInxsCheckChangeSagyou", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                isOk = false;
            }

            return isOk;
        }
        //SONNT #142900 作業日変更したら、INXS確定日と比較 2020/10 END

        #region #158079 作業日変更Check

        /// <summary>
        /// 作業日変更Check
        /// </summary>
        /// <param name="uketsukeNumber">受付Number</param>
        /// <returns>True is Ok</returns>
        private bool CheckChangeInxsUketsukeSagyouDate(string uketsukeNumber)
        {
            bool checkResult = true;
            try
            {

                if (string.IsNullOrEmpty(uketsukeNumber))
                {
                    return checkResult;
                }

                StringBuilder sql = new StringBuilder();
                sql.Append(" SELECT T_USSE.UKETSUKE_NUMBER ");
                sql.Append(" FROM T_UKETSUKE_SS_ENTRY T_USSE ");
                sql.Append(" INNER JOIN T_PICKUP_REQUEST_UKETSUKE_SS_INXS AS PRU_SS_INXS ");
                sql.Append(" ON PRU_SS_INXS.UKETSUKE_SYSTEM_ID = T_USSE.SYSTEM_ID ");
                sql.Append(" INNER JOIN T_PICKUP_REQUEST_INXS ");
                sql.Append(" ON T_PICKUP_REQUEST_INXS.SYSTEM_ID = PRU_SS_INXS.REQUEST_SYSTEM_ID ");
                sql.Append(" AND T_PICKUP_REQUEST_INXS.DELETE_FLG = 0 ");
                sql.AppendFormat(" WHERE T_USSE.UKETSUKE_NUMBER = ({0}) ", uketsukeNumber);
                sql.Append(" AND T_USSE.DELETE_FLG = 0 ");

                var dt = this.dao_HAISHA.GetDateForStringSql(sql.ToString());
                if (dt != null && dt.Rows.Count > 0)
                {
                    short haishaHenkouSagyouDatekbn = 1;
                    if (this.sysInfoEntity != null && !this.sysInfoEntity.HAISHA_HENKOU_SAGYOU_DATE_KBN.IsNull)
                    {
                        haishaHenkouSagyouDatekbn = this.sysInfoEntity.HAISHA_HENKOU_SAGYOU_DATE_KBN.Value;
                    }

                    if (haishaHenkouSagyouDatekbn == 1)
                    {
                        if (this.msgLogic.MessageBoxShow("C116", uketsukeNumber) != DialogResult.Yes)
                        {
                            checkResult = false;
                        }
                    }
                    else
                    {
                        checkResult = false;
                        this.msgLogic.MessageBoxShow("E311", uketsukeNumber);
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckChangeInxsUketsukeSagyouDate", ex);
                this.msgLogic.MessageBoxShow("E245", "");
                checkResult = false;
            }

            return checkResult;
        }

        #endregion
    }
}
