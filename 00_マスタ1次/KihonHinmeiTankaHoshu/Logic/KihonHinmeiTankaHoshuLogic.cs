// $Id: KihonHinmeiTankaHoshuLogic.cs 52370 2015-06-16 02:14:23Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KihonHinmeiTankaHoshu.APP;
using KihonHinmeiTankaHoshu.MultiRowTemplate;
using KihonHinmeiTankaHoshu.Validator;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Dto;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace KihonHinmeiTankaHoshu.Logic
{
    /// <summary>
    /// 基本品名単価保守画面のビジネスロジック
    /// </summary>
    public class KihonHinmeiTankaHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "KihonHinmeiTankaHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_KIHONHINMEITANKA_DATA_SQL = "KihonHinmeiTankaHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_KIHONHINMEITANKA_DATA_SQL = "KihonHinmeiTankaHoshu.Sql.GetKihonHinmeiTankaDataSql.sql";

        private readonly string GET_UNPAN_GYOUSHA_DATA_SQL = "KihonHinmeiTankaHoshu.Sql.GetUnpanGyoushaDataSql.sql";

        private readonly string GET_NIOROSHI_GYOUSHA_DATA_SQL = "KihonHinmeiTankaHoshu.Sql.GetNioroshiGyoushaDataSql.sql";

        private readonly string GET_NIOROSHI_GENBA_DATA_SQL = "KihonHinmeiTankaHoshu.Sql.GetNioroshiGenbaDataSql.sql";

        private readonly string GET_HINMEI_DATA_SQL = "KihonHinmeiTankaHoshu.Sql.GetHinmeiDataSql.sql";

        private readonly string GET_HINMEI_DATA_LIST_SQL_FOR_URIAGE = "KihonHinmeiTankaHoshu.Sql.GetHinmeiDataListSqlForUriage.sql";

        private readonly string GET_HINMEI_DATA_LIST_SQL_FOR_SHIHARAI = "KihonHinmeiTankaHoshu.Sql.GetHinmeiDataListSqlForShiharai.sql";

        /// <summary>
        /// 基本品名単価保守画面Form
        /// </summary>
        private KihonHinmeiTankaHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        public M_KIHON_HINMEI_TANKA[] entitys;

        /// <summary>
        /// 品名読込み用
        /// 表示されている一覧を保持し、登録時に削除
        /// </summary>
        public M_KIHON_HINMEI_TANKA[] deleteEntitys;

        private bool isAllSearch;

        /// <summary>
        /// 基本品名名単価のDao
        /// </summary>
        private IM_KIHON_HINMEI_TANKADao dao;

        /// <summary>
        /// 品名のDao
        /// </summary>
        private IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        /// <summary>
        /// 種類のDao
        /// </summary>
        private IM_SHURUIDao shuruiDao;

        /// <summary>
        /// 単位のDao
        /// </summary>
        private IM_UNITDao unitDao;

        /// <summary>
        /// 品名マスタ読み込み中
        /// True＝読み込み中
        /// </summary>
        internal bool isNowLoadingHinmeiMaster = false;

        // VUNGUYEN 20150525 #1294 START
        public Cell cell;

        // VUNGUYEN 20150525 #1294 END
        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal MasterBaseForm parentForm;

        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        private string previousNioroshiGyousha { get; set; }

        #endregion フィールド

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_KIHON_HINMEI_TANKA SearchString { get; set; }

        /// <summary>
        /// 検索条件(SYS_ID)
        /// </summary>
        public M_KIHON_HINMEI_TANKA[] SearchSysId { get; set; }

        /// <summary>
        /// 検索結果(品名)
        /// </summary>
        public DataTable SearchResultHinmei { get; set; }

        /// <summary>
        /// 検索結果(伝種区分)
        /// </summary>
        public DataTable SearchResultDenshu { get; set; }

        /// <summary>
        /// 検索結果(単位)
        /// </summary>
        public DataTable SearchResultUnit { get; set; }

        /// <summary>
        /// 検索結果(運搬業者)
        /// </summary>
        public DataTable SearchResultGyousha { get; set; }

        /// <summary>
        /// 検索結果(荷降先業者)
        /// </summary>
        public DataTable SearchResultNioroshiGyousha { get; set; }

        /// <summary>
        /// 検索結果(荷降先現場)
        /// </summary>
        public DataTable SearchResultNioroshiGenba { get; set; }

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>
        /// 処理対象伝票区分
        /// </summary>
        public Int16 TargetDenpyouKbn { get; set; }

        #endregion プロパティ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public KihonHinmeiTankaHoshuLogic(KihonHinmeiTankaHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_KIHON_HINMEI_TANKADao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.shuruiDao = DaoInitUtility.GetComponent<IM_SHURUIDao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (MasterBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked && !this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        public void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged -= new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);

            if (this.entitySysInfo != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Value;
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Value;
            }
            else
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = true;
            }

            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.Ichiran.ReadOnly = true;
            this.form.Ichiran.AllowUserToAddRows = false;
            this.form.Ichiran.IsBrowsePurpose = true;

            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func9.Enabled = false;
        }

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResult = GetSearchResult();
                this.SetSearchResultAll();

                //M_KIHON_HINMEI_TANKA searchParams = new M_KIHON_HINMEI_TANKA();
                //searchParams.DENPYOU_KBN_CD = this.SearchString.DENPYOU_KBN_CD;
                //this.SearchResultAll = dao.GetDataBySqlFile(this.GET_KIHONHINMEITANKA_DATA_SQL, searchParams);

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked;
                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI = this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked;

                Properties.Settings.Default.Save();

                int count = this.SearchResult.Rows == null ? 0 : 1;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
        }

        /// <summary>
        /// 検索してSearchResultを返す
        /// </summary>
        /// <returns></returns>
        public DataTable GetSearchResult()
        {
            checkDensyuCd();
            var searchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_KIHONHINMEITANKA_DATA_SQL
                                                        , this.SearchString
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked
                                                        , this.form.SYURUI_SHITEI.Checked
                                                        , this.form.SHURUI_CD.Text
                                                        );
            return searchResult;
        }

        /// <summary>
        /// 検索してSearchResultAllにセット
        /// </summary>
        public void SetSearchResultAll()
        {
            checkDensyuCd();
            M_KIHON_HINMEI_TANKA searchParams = new M_KIHON_HINMEI_TANKA();
            searchParams.DENPYOU_KBN_CD = this.SearchString.DENPYOU_KBN_CD;
            this.SearchResultAll = dao.GetDataBySqlFile(this.GET_KIHONHINMEITANKA_DATA_SQL, searchParams);
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var focus = (this.form.TopLevelControl as Form).ActiveControl;
                this.form.Ichiran.Focus();

                var parentForm = (MasterBaseForm)this.form.Parent;

                var entityList = new M_KIHON_HINMEI_TANKA[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_KIHON_HINMEI_TANKA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KIHON_HINMEI_TANKA>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KihonHinmeiTankaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var kihonhinmeiEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_KIHON_HINMEI_TANKA> addList = new List<M_KIHON_HINMEI_TANKA>();
                foreach (var kihonhinmeiEntity in kihonhinmeiEntityList)
                {
                    if (kihonhinmeiEntity.DELETE_FLG == isDelete)
                    {
                        if (parentForm.bt_process1.Text == "[1] 支払")
                        {
                            kihonhinmeiEntity.DENPYOU_KBN_CD = 1;
                        }
                        else
                        {
                            kihonhinmeiEntity.DENPYOU_KBN_CD = 2;
                        }

                        MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), kihonhinmeiEntity);
                        addList.Add(kihonhinmeiEntity);
                    }
                }

                this.form.Ichiran.DataSource = preDt;

                this.entitys = addList.ToArray();

                if (focus != null)
                {
                    focus.Focus();
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                SetSearchString();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 基本品名単価の重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KihonHinmeiTankaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt = ((DataTable)this.form.Ichiran.DataSource).GetChanges();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    LogUtility.DebugMethodEnd(true);
                    return true;
                }

                KihonHinmeiTankaHoshuValidator vali = new KihonHinmeiTankaHoshuValidator();

                if (this.isNowLoadingHinmeiMaster)
                {
                    this.SetSearchResultAll();//削除したので最新を取得
                }
                var result = vali.KihonHinmeitanCDValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);
                LogUtility.DebugMethodEnd(result);

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        public bool Preview()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C011", "基本品名単価一覧表");

                MessageBox.Show("未実装");

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// CSV
        /// </summary>
        public bool CSV()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    MultiRowIndexCreateLogic multirowLocationLogic = new MultiRowIndexCreateLogic();
                    multirowLocationLogic.multiRow = this.form.Ichiran;

                    multirowLocationLogic.CreateLocations();

                    // VUNGUYEN 20150525 #1294 START
                    CSVFileLogicCustom csvLogic = new CSVFileLogicCustom();
                    // VUNGUYEN 20150525 #1294 END

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);

                    #region 新しいCSV出力利用するように、余計なメッセージを削除

                    //msgLogic.MessageBoxShow("I000");

                    #endregion 新しいCSV出力利用するように、余計なメッセージを削除
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        public bool CancelCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                bool catchErr = SetIchiran();

                LogUtility.DebugMethodEnd();
                return catchErr;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        #region 登録/更新/削除

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                //独自チェックの記述例を書く

                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_KIHON_HINMEI_TANKA kihonhinmeitankaEntity in this.entitys)
                        {
                            M_KIHON_HINMEI_TANKA entity = null;

                            // SYS_IDの値判断
                            if (!kihonhinmeitankaEntity.SYS_ID.IsNull)
                            {
                                entity = this.dao.GetDataByCd(kihonhinmeitankaEntity.SYS_ID.ToString());
                            }

                            if (entity == null)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (kihonhinmeitankaEntity.DELETE_FLG)
                                {
                                    continue;
                                }
                                // MAXのSYS_IDを取得する
                                Int64 sysId = Convert.ToInt64(this.dao.GetMaxPlusKey());
                                kihonhinmeitankaEntity.SYS_ID = sysId;
                                this.dao.Insert(kihonhinmeitankaEntity);
                            }
                            else
                            {
                                this.dao.Update(kihonhinmeitankaEntity);
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");
                }
                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C021");
                if (result == DialogResult.Yes)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_KIHON_HINMEI_TANKA kihonhinmeitankaEntity in this.entitys)
                        {
                            // SYS_IDの値判断
                            if (kihonhinmeitankaEntity.SYS_ID.IsNull)
                            {
                                continue;
                            }

                            M_KIHON_HINMEI_TANKA entity = this.dao.GetDataByCd(kihonhinmeitankaEntity.SYS_ID.ToString());
                            if (entity != null)
                            {
                                this.dao.Update(kihonhinmeitankaEntity);
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    msgLogic.MessageBoxShow("I001", "削除");
                }

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 論理削除　品名読込用
        /// </summary>
        [Transaction]
        public virtual bool LogicalDeleteForHinmeiYomikomi()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // トランザクション開始
                using (var tran = new Transaction())
                {
                    //削除処理
                    foreach (M_KIHON_HINMEI_TANKA kihonhinmeitankaEntity in this.deleteEntitys)
                    {
                        // SYS_IDの値判断
                        if (kihonhinmeitankaEntity.SYS_ID.IsNull)
                        {
                            continue;
                        }

                        M_KIHON_HINMEI_TANKA entity = this.dao.GetDataByCd(kihonhinmeitankaEntity.SYS_ID.ToString());
                        if (entity != null)
                        {
                            this.dao.Update(kihonhinmeitankaEntity);
                        }
                    }
                    // トランザクション終了
                    tran.Commit();
                }
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("LogicalDeleteForHinmeiYomikomi", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("LogicalDeleteForHinmeiYomikomi", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDeleteForHinmeiYomikomi", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 品名読込時削除
        /// </summary>
        public bool DeleteForHinmeiLoading()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C021");
                if (result == DialogResult.Yes)
                {

                    var dt = this.SearchResult;

                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow row = dt.Rows[i];

                        if (row["DELETE_FLG"].ToString() == "True")
                        {
                            row.Delete();
                        }
                    }
                    return false;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("DeleteForHinmeiLoading", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion 登録/更新/削除

        #region Equals/GetHashCode/ToString

        /// <summary>
        /// クラスが等しいかどうか判定
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public override bool Equals(object other)
        {
            //objがnullか、型が違うときは、等価でない
            if (other == null || this.GetType() != other.GetType())
            {
                return false;
            }

            KihonHinmeiTankaHoshuLogic localLogic = other as KihonHinmeiTankaHoshuLogic;
            return localLogic == null ? false : true;
        }

        /// <summary>
        /// ハッシュコード取得
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// 該当するオブジェクトを文字列形式で取得
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return base.ToString();
        }

        #endregion Equals/GetHashCode/ToString

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            try
            {
                var table = this.SearchResult;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }
                this.form.Ichiran.DataSource = null;
                this.form.Ichiran.DataSource = table;

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M211", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.DispReferenceMode();
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (MasterBaseForm)this.form.Parent;
            ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// Title処理
        /// </summary>
        [Transaction]
        public virtual bool TitleInit()
        {
            try
            {
                var parentForm = (MasterBaseForm)this.form.Parent;

                var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");

                //システム設定より画面Title取得
                if (parentForm.bt_process1.Text == "")
                {
                    switch (this.entitySysInfo.KIHON_HINMEI_DEFAULT.Value.ToString())
                    {
                        case "1":
                            parentForm.bt_process1.Text = "[1] 売上";
                            break;

                        case "2":
                            parentForm.bt_process1.Text = "[1] 支払";
                            break;

                        default:
                            break;
                    }
                }

                if ("[1] 売上".Equals(parentForm.bt_process1.Text))
                {
                    titleControl.Text = "基本品名単価入力（売上）";
                    parentForm.bt_process1.Text = "[1] 支払";
                    parentForm.txb_process.Text = "1";
                    this.TargetDenpyouKbn = 1;
                }
                else if ("[1] 支払".Equals(parentForm.bt_process1.Text))
                {
                    titleControl.Text = "基本品名単価入力（支払）";
                    parentForm.bt_process1.Text = "[1] 売上";
                    parentForm.txb_process.Text = "2";
                    this.TargetDenpyouKbn = 2;
                }

                //品名読込
                parentForm.bt_process2.Text = "[2] 品名読込";

                // 修正権限が無い場合、品名読み込みを押下できないようにする
                if (r_framework.Authority.Manager.CheckAuthority("M211", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    parentForm.bt_process2.Enabled = true;
                }
                else
                {
                    parentForm.bt_process2.Enabled = false;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("TitleInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 伝種区分をタイトルから取得
        /// </summary>
        [Transaction]
        public virtual void checkDensyuCd()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;
            var titleControl = (Label)controlUtil.FindControl(parentForm, "lb_title");
            M_KIHON_HINMEI_TANKA entity = new M_KIHON_HINMEI_TANKA();
            //システム設定より画面Title取得
            if (titleControl.Text == "基本品名単価入力")
            {
                switch (this.entitySysInfo.KIHON_HINMEI_DEFAULT.Value.ToString())
                {
                    case "1":
                        titleControl.Text = "基本品名単価入力（売上）";
                        break;

                    case "2":
                        titleControl.Text = "基本品名単価入力（支払）";
                        break;

                    default:
                        break;
                }
            }

            if (titleControl.Text == "基本品名単価入力（売上）")
            {
                entity.DENPYOU_KBN_CD = 1;
            }
            else
            {
                entity.DENPYOU_KBN_CD = 2;
            }

            this.SearchString = entity;
        }

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            //削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            //条件取消ボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.CancelCondition);

            //検索ボタン(F8)イベント生成
            parentForm.bt_func8.Click += new EventHandler(this.form.Search);

            //登録ボタン(F9)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //売上/支払ボタン(1)イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.form.Change);

            //品名読込ボタン(2)イベント生成
            parentForm.bt_process2.Click += new EventHandler(this.form.HinmeiLoad);

            //表示条件イベント生成
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
            this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.CheckedChanged += new System.EventHandler(this.form.ICHIRAN_HYOUJI_JOUKEN_CheckedChanged);
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
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;
            M_KIHON_HINMEI_TANKA entity = new M_KIHON_HINMEI_TANKA();

            if (parentForm.bt_process1.Text == "[1] 売上")
            {
                entity.DENPYOU_KBN_CD = 2;
            }
            else
            {
                entity.DENPYOU_KBN_CD = 1;
            }

            this.SearchString = entity;
        }

        /// <summary>
        /// DataTableのクローン処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetCloneDataTable(DataTable dt)
        {
            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            return table;
        }

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearCondition()
        {
            this.SetHyoujiJoukenInit();

            //初期化
            this.form.SYURUI_ALL.Checked = true;
            this.form.SHURUI_CD.Text = "";
            this.form.SHURUI_NAME_RYAKU.Text = "";

            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }

        /// <summary>
        /// 品名検索
        /// </summary>
        /// <param name="e"></param>
        public virtual void SearchHinmei(CellValidatingEventArgs e)
        {
            LogUtility.DebugMethodStart(e);

            if (e.FormattedValue == null || string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
            {
                this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.HINMEI_NAME_RYAKU].Value = string.Empty;
                LogUtility.DebugMethodEnd(e);
                return;
            }

            int padLen = int.Parse(((GcCustomAlphaNumTextBoxCell)((KihonHinmeiTankaHoshuDetail)this.form.Ichiran.Template).Row[Const.KihonHinmeiTankaHoshuConstans.HINMEI_CD]).CharactersNumber.ToString());
            M_HINMEI cond = new M_HINMEI();
            cond.HINMEI_CD = e.FormattedValue.ToString().PadLeft(padLen, '0');
            if (this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value != null && !string.IsNullOrWhiteSpace(this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value.ToString()))
            {
                cond.DENSHU_KBN_CD = Int16.Parse(this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value.ToString());
            }
            cond.DENPYOU_KBN_CD = this.TargetDenpyouKbn;
            DataTable hin = this.hinmeiDao.GetDataBySqlFile(this.GET_HINMEI_DATA_SQL, cond);
            if (hin.Rows.Count <= 0)
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E020", "品名");
                e.Cancel = true;
                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                return;
            }

            if (this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value == null || string.IsNullOrWhiteSpace(this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value.ToString()))
            {
                this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.DENSHU_KBN_CD].Value = hin.Rows[0][Const.KihonHinmeiTankaHoshuConstans.DENSHU_KBN_CD].ToString();
                this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.DENSHU_KBN_NAME_RYAKU].Value = hin.Rows[0][Const.KihonHinmeiTankaHoshuConstans.DENSHU_KBN_NAME_RYAKU].ToString();
            }
            this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.HINMEI_CD].Value = hin.Rows[0][Const.KihonHinmeiTankaHoshuConstans.HINMEI_CD].ToString();
            this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.HINMEI_NAME_RYAKU].Value = hin.Rows[0][Const.KihonHinmeiTankaHoshuConstans.HINMEI_NAME_RYAKU].ToString();

            LogUtility.DebugMethodEnd(e);
        }

        /// <summary>
        /// 一覧セル編集開始時イベント処理
        /// </summary>
        /// <param name="e"></param>
        public bool IchiranCellEnter(CellEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (e.CellName.Equals("NIOROSHI_GYOUSHA_CD") && this.form.Ichiran.Rows.Count > 0 && this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value != null)
                {
                    this.previousNioroshiGyousha = this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString();
                }
                else
                {
                    this.previousNioroshiGyousha = string.Empty;
                }

                // 新規行の場合には削除チェックさせない
                if (this.form.Ichiran.Rows[e.RowIndex].IsNewRow)
                {
                    this.form.Ichiran.Rows[e.RowIndex]["DELETE_FLG"].Selectable = false;
                }
                else
                {
                    this.form.Ichiran.Rows[e.RowIndex]["DELETE_FLG"].Selectable = true;
                }

                // 新行入力の場合、適用開始日に本日を設定
                if (this.form.Ichiran.Rows[e.RowIndex].IsNewRow)
                {
                    // 20151102 katen #12048 「システム日付」の基準作成、適用 start
                    //this.form.Ichiran[e.RowIndex, "TEKIYOU_BEGIN"].Value = DateTime.Today;
                    this.form.Ichiran[e.RowIndex, "TEKIYOU_BEGIN"].Value = this.parentForm.sysDate.Date;
                    // 20151102 katen #12048 「システム日付」の基準作成、適用 end
                }

                // セル固有処理
                if (e.CellName.Equals(Const.KihonHinmeiTankaHoshuConstans.HINMEI_CD))
                {
                    GcCustomAlphaNumTextBoxCell target = (GcCustomAlphaNumTextBoxCell)this.form.Ichiran[e.RowIndex, e.CellName];
                    target.PopupSearchSendParams[1].SubCondition[1].Value = this.TargetDenpyouKbn.ToString();

                    // 伝種区分に応じて絞り込み条件を切り替える。
                    PopupSearchSendParamDto searchParam = new PopupSearchSendParamDto();
                    PopupSearchSendParamDto searchParam9 = new PopupSearchSendParamDto();

                    if (this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value != null && !string.IsNullOrWhiteSpace(this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value.ToString()))
                    {
                        searchParam.And_Or = CONDITION_OPERATOR.AND;
                        searchParam.KeyName = "DENSHU_KBN_CD";
                        searchParam.Value = this.form.Ichiran[e.RowIndex, "DENSHU_KBN_CD"].Value.ToString();

                        searchParam9.And_Or = CONDITION_OPERATOR.OR;
                        searchParam9.KeyName = "DENSHU_KBN_CD";
                        searchParam9.Value = "9";
                    }

                    target.PopupSearchSendParams[target.PopupSearchSendParams.Count - 1].SubCondition = new Collection<PopupSearchSendParamDto>();
                    target.PopupSearchSendParams[target.PopupSearchSendParams.Count - 1].SubCondition.Add(searchParam);
                    target.PopupSearchSendParams[target.PopupSearchSendParams.Count - 1].SubCondition.Add(searchParam9);
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranCellEnter", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 一覧のセル編集終了イベント
        /// </summary>
        /// <param name="e"></param>
        public bool IchiranCellValidating(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                GcCustomMultiRow list = this.form.Ichiran;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //品名
                if (e.CellName.Equals(Const.KihonHinmeiTankaHoshuConstans.HINMEI_CD))
                {
                    this.SearchHinmei(e);
                }

                //運搬業者
                if (e.CellName.Equals("UNPAN_GYOUSHA_CD") && list.Rows.Count > 0 && list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value != null)
                {
                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString()))
                    {
                        // マスタ存在チェック
                        M_GYOUSHA gyousha = new M_GYOUSHA();
                        gyousha.GYOUSHA_CD = this.form.Ichiran[e.RowIndex, "UNPAN_GYOUSHA_CD"].Value.ToString();
                        DataTable dt = this.gyoushaDao.GetDataBySqlFile(GET_UNPAN_GYOUSHA_DATA_SQL, gyousha);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.form.Ichiran[e.RowIndex, "UNPAN_GYOUSHA_RYAKU"].Value = dt.Rows[0]["UNPAN_GYOUSHA_RYAKU"].ToString();
                        }
                        else
                        {
                            this.form.Ichiran[e.RowIndex, "UNPAN_GYOUSHA_RYAKU"].Value = string.Empty;
                            msgLogic.MessageBoxShow("E020", "業者");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, "UNPAN_GYOUSHA_RYAKU"].Value = string.Empty;
                    }
                }

                //荷降業者
                if (e.CellName.Equals("NIOROSHI_GYOUSHA_CD") && list.Rows.Count > 0 && list[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value != null)
                {
                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString()))
                    {
                        if (this.previousNioroshiGyousha != this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString())
                        {
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_CD"].Value = string.Empty;
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = string.Empty;
                        }

                        // マスタ存在チェック
                        M_GYOUSHA gyousha = new M_GYOUSHA();
                        gyousha.GYOUSHA_CD = this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString();
                        DataTable dt = this.gyoushaDao.GetDataBySqlFile(GET_NIOROSHI_GYOUSHA_DATA_SQL, gyousha);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_RYAKU"].Value = dt.Rows[0]["NIOROSHI_GYOUSHA_RYAKU"].ToString();
                        }
                        else
                        {
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_RYAKU"].Value = string.Empty;
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_CD"].Value = string.Empty;
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = string.Empty;
                            msgLogic.MessageBoxShow("E020", "業者");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_RYAKU"].Value = string.Empty;
                        this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_CD"].Value = string.Empty;
                        this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = string.Empty;
                    }
                }

                //荷降現場
                if (e.CellName.Equals("NIOROSHI_GENBA_CD") && list.Rows.Count > 0 && list[e.RowIndex, "NIOROSHI_GENBA_CD"].Value != null)
                {
                    if (!string.IsNullOrWhiteSpace(list[e.RowIndex, "NIOROSHI_GENBA_CD"].Value.ToString()))
                    {
                        // 荷降先現場CDが入力されている状態で、荷降先業者CDがクリアされていた場合、エラーとする
                        if (list[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value == null || string.IsNullOrWhiteSpace(list[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString()))
                        {
                            msgLogic.MessageBoxShow("E051", "荷降先業者");
                            list[e.RowIndex, "NIOROSHI_GENBA_CD"].Value = string.Empty;
                            if (list.EditingControl != null)
                            {
                                list.EditingControl.Text = string.Empty;
                            }
                            e.Cancel = true;
                            LogUtility.DebugMethodEnd(e);
                            return false;
                        }

                        // マスタ存在チェック
                        M_GENBA genba = new M_GENBA();
                        M_GYOUSHA gyousha = new M_GYOUSHA();
                        genba.GYOUSHA_CD = this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value.ToString();
                        genba.GENBA_CD = this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_CD"].Value.ToString();
                        DataTable dt = this.genbaDao.GetDataBySqlFile(GET_NIOROSHI_GENBA_DATA_SQL, genba);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_CD"].Value = dt.Rows[0]["NIOROSHI_GYOUSHA_CD"].ToString();
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GYOUSHA_RYAKU"].Value = dt.Rows[0]["NIOROSHI_GYOUSHA_RYAKU"].ToString();
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = dt.Rows[0]["NIOROSHI_GENBA_RYAKU"].ToString();
                        }
                        else
                        {
                            this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = string.Empty;
                            msgLogic.MessageBoxShow("E020", "現場");
                            e.Cancel = true;
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, "NIOROSHI_GENBA_RYAKU"].Value = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd(e);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                e.Cancel = true;
                LogUtility.Error("IchiranCellValidating", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(e);
                return true;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                LogUtility.Error("IchiranCellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e);
                return true;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fswit"></param>
        /// <returns></returns>
        public bool IchiranCellSwitchCdName(CellEventArgs e, Const.KihonHinmeiTankaHoshuConstans.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case Const.KihonHinmeiTankaHoshuConstans.FocusSwitch.IN:
                    // 名称にフォーカス時実行
                    if (e.CellName.Equals(Const.KihonHinmeiTankaHoshuConstans.DENSHU_KBN_NAME_RYAKU))
                    {
                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.DENSHU_KBN_CD];
                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    else if (e.CellName.Equals(Const.KihonHinmeiTankaHoshuConstans.UNIT_NAME_RYAKU))
                    {
                        this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.UNIT_CD].Visible = true;
                        this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.UNIT_CD].UpdateBackColor(false);

                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.UNIT_CD];
                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case Const.KihonHinmeiTankaHoshuConstans.FocusSwitch.OUT:
                    // CDに検証成功後実行
                    if (e.CellName.Equals(Const.KihonHinmeiTankaHoshuConstans.DENSHU_KBN_CD))
                    {
                        this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.DENSHU_KBN_NAME_RYAKU].Visible = true;
                        this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.DENSHU_KBN_NAME_RYAKU].UpdateBackColor(false);
                    }
                    else if (e.CellName.Equals(Const.KihonHinmeiTankaHoshuConstans.UNIT_CD))
                    {
                        this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.UNIT_NAME_RYAKU].Visible = true;
                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.UNIT_NAME_RYAKU];
                        this.form.Ichiran[e.RowIndex, Const.KihonHinmeiTankaHoshuConstans.UNIT_NAME_RYAKU].UpdateBackColor(false);

                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// 単位チェック
        /// </summary>
        /// <returns></returns>
        internal bool unitCheck(int index)
        {
            bool Iserr = false;
            M_UNIT unit = new M_UNIT();
            unit.UNIT_CD = Convert.ToInt16(this.form.Ichiran.Rows[index].Cells["UNIT_CD"].Value);
            M_UNIT[] result = this.unitDao.GetAllValidData(unit);

            if (result == null || result.Length == 0)
            {
                Iserr = true;
            }
            else
            {
                this.form.Ichiran.Rows[index].Cells["UNIT_CD"].Value = result[0].UNIT_CD.Value;
                this.form.Ichiran.Rows[index].Cells["UNIT_NAME_RYAKU"].Value = result[0].UNIT_NAME_RYAKU;
            }
            return Iserr;
        }

        /// <summary>
        /// 単価フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string FormatSystemTanka(Decimal num)
        {
            string format = "#,##0";
            if (!string.IsNullOrWhiteSpace(this.entitySysInfo.SYS_TANKA_FORMAT))
            {
                format = this.entitySysInfo.SYS_TANKA_FORMAT;
                switch (this.entitySysInfo.SYS_TANKA_FORMAT_CD.Value)
                {
                    case 1:
                    case 2:
                        num = Math.Floor(num);
                        break;

                    case 3:
                        num = Math.Floor(num * 10) / 10;
                        break;

                    case 4:
                        num = Math.Floor(num * 100) / 100;
                        break;

                    case 5:
                        num = Math.Floor(num * 1000) / 1000;
                        break;
                }
            }
            return string.Format("{0:" + format + "}", num);
        }

        /// <summary>
        /// 品名読込み時の既存データ削除用
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateDeleteEntity()
        {
            try
            {
                var parentForm = (MasterBaseForm)this.form.Parent;

                var entityList = new M_KIHON_HINMEI_TANKA[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_KIHON_HINMEI_TANKA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KIHON_HINMEI_TANKA>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KihonHinmeiTankaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                //this.form.Ichiran.DataSource = dt.GetChanges();

                var kihonhinmeiEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_KIHON_HINMEI_TANKA> addList = new List<M_KIHON_HINMEI_TANKA>();
                foreach (var kihonhinmeiEntity in kihonhinmeiEntityList)
                {
                    //if (kihonhinmeiEntity.DELETE_FLG == isDelete)
                    //{
                    kihonhinmeiEntity.DELETE_FLG = true;//すべて削除する
                    if (parentForm.bt_process1.Text == "[1] 支払")
                    {
                        kihonhinmeiEntity.DENPYOU_KBN_CD = 1;
                    }
                    else
                    {
                        kihonhinmeiEntity.DENPYOU_KBN_CD = 2;
                    }

                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), kihonhinmeiEntity);
                    addList.Add(kihonhinmeiEntity);
                    //}
                }

                this.form.Ichiran.DataSource = preDt;

                this.deleteEntitys = addList.ToArray();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateDeleteEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 種類情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchSyuruiName(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrWhiteSpace(this.form.SHURUI_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    var tmp = shuruiDao.GetDataByCd(this.form.SHURUI_CD.Text);

                    if (tmp != null)
                    {
                        this.form.SHURUI_NAME_RYAKU.Text = tmp.SHURUI_NAME_RYAKU.ToString();
                    }
                    else
                    {
                        if (e != null)
                        {
                            this.form.SHURUI_NAME_RYAKU.Text = string.Empty;
                            msgLogic.MessageBoxShow("E020", "種類");
                            e.Cancel = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchSyuruiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 品名マスタから一覧を取得して、一覧にセット
        /// </summary>
        /// <param name="e"></param>
        public virtual bool LoadingHinmeiListToIchiran()
        {
            try
            {
                M_HINMEI cond = new M_HINMEI();
                if (this.form.SYURUI_SHITEI.Checked)
                {
                    cond.SHURUI_CD = this.form.SHURUI_CD.Text;
                }
                else
                {
                    cond.SHURUI_CD = null;
                }

                //売上、支払で書分け
                DataTable hin;
                var parentForm = (MasterBaseForm)this.form.Parent;

                if ("[1] 売上".Equals(parentForm.bt_process1.Text))
                {
                    //支払
                    hin = this.hinmeiDao.GetDataBySqlFile(this.GET_HINMEI_DATA_LIST_SQL_FOR_SHIHARAI, cond);
                }
                else
                {
                    //売上
                    hin = this.hinmeiDao.GetDataBySqlFile(this.GET_HINMEI_DATA_LIST_SQL_FOR_URIAGE, cond);
                }

                if (hin.Rows.Count <= 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "品名");
                    return false;
                }

                foreach (DataRow row in hin.Rows)
                {
                    DataRow newHinmeiRow = this.SearchResult.NewRow();
                    newHinmeiRow["DENPYOU_KBN_CD"] = row["DENPYOU_KBN_CD"].ToString();
                    //newHinmeiRow[""] = eRow[""].ToString();
                    newHinmeiRow["HINMEI_CD"] = row["HINMEI_CD"].ToString();
                    newHinmeiRow["HINMEI_NAME_RYAKU"] = row["HINMEI_NAME_RYAKU"].ToString();

                    newHinmeiRow["DENSHU_KBN_CD"] = row["DENSHU_KBN_CD"].ToString();
                    newHinmeiRow["DENSHU_KBN_NAME_RYAKU"] = row["DENSHU_KBN_NAME_RYAKU"].ToString();

                    if (row["UNIT_CD"].ToString() != "")
                    {
                        newHinmeiRow["UNIT_CD"] = row["UNIT_CD"].ToString();
                        newHinmeiRow["UNIT_NAME_RYAKU"] = row["UNIT_NAME_RYAKU"].ToString();
                    }
                    // 20151102 katen #12048 「システム日付」の基準作成、適用 start
                    //newHinmeiRow["TEKIYOU_BEGIN"] = DateTime.Today;//デフォルト値を入れる
                    newHinmeiRow["TEKIYOU_BEGIN"] = this.parentForm.sysDate.Date;//デフォルト値を入れる
                    // 20151102 katen #12048 「システム日付」の基準作成、適用 end

                    this.SearchResult.Rows.Add(newHinmeiRow);
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LoadingHinmeiListToIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        // 20141217 Houkakou 「基本品名単価入力」の日付チェックを追加する　start

        #region 日付チェック

        /// <summary>
        /// 日付チェック
        /// </summary>
        /// <returns></returns>
        internal bool DateCheck(out bool catchErr)
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                catchErr = false;
                bool bFlag = false;

                foreach (Row row in this.form.Ichiran.Rows)
                {
                    if (row.Cells["DELETE_FLG"].Value != null && row.Cells["DELETE_FLG"].Value.ToString() == "True"
                        && (row.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(row.Cells["CREATE_USER"].Value.ToString())))
                    {
                        continue;
                    }
                    row.Cells["TEKIYOU_BEGIN"].Style.BackColor = Constans.NOMAL_COLOR;
                    row.Cells["TEKIYOU_END"].Style.BackColor = Constans.NOMAL_COLOR;

                    string strdate_from = Convert.ToString(row.Cells["TEKIYOU_BEGIN"].Value);
                    string strdate_to = Convert.ToString(row.Cells["TEKIYOU_END"].Value);

                    //nullチェック
                    if (string.IsNullOrEmpty(strdate_from))
                    {
                        continue;
                    }
                    if (string.IsNullOrEmpty(strdate_to))
                    {
                        continue;
                    }

                    DateTime date_from = Convert.ToDateTime(row.Cells["TEKIYOU_BEGIN"].Value);
                    DateTime date_to = Convert.ToDateTime(row.Cells["TEKIYOU_END"].Value);

                    // 日付FROM > 日付TO 場合
                    if (date_to.CompareTo(date_from) < 0)
                    {
                        row.Cells["TEKIYOU_BEGIN"].Style.BackColor = Constans.ERROR_COLOR;
                        row.Cells["TEKIYOU_END"].Style.BackColor = Constans.ERROR_COLOR;

                        bFlag = true;
                    }
                }
                return bFlag;
            }
            catch (Exception ex)
            {
                catchErr = true;
                LogUtility.Error("DateCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        #endregion 日付チェック

        // 20141217 Houkakou 「基本品名単価入力」の日付チェックを追加する　end

        // VUNGUYEN 20150525 #1294 START
        public void Ichiran_DoubleClick(object sender, EventArgs e)
        {
            if (this.cell != null && this.cell.GcMultiRow != null && this.cell.GcMultiRow.EditingControl != null && this.cell.Name.Equals("TEKIYOU_END"))
            {
                PropertyUtility.SetTextOrValue(this.form.Ichiran.Rows[cell.RowIndex].Cells["TEKIYOU_END"], this.form.Ichiran.Rows[cell.RowIndex].Cells["TEKIYOU_BEGIN"].Value);
                if (string.IsNullOrEmpty(Convert.ToString(this.form.Ichiran.Rows[cell.RowIndex].Cells["TEKIYOU_BEGIN"].Value)))
                {
                    this.cell.GcMultiRow.EditingControl.Text = "";
                }
                else
                {
                    this.cell.GcMultiRow.EditingControl.Text = Convert.ToDateTime(this.form.Ichiran.Rows[cell.RowIndex].Cells["TEKIYOU_BEGIN"].Value).ToString("yyyy/MM/dd");
                }
            }
        }
        // VUNGUYEN 20150525 #1294 END
    }
}