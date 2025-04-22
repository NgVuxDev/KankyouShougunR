// $Id: KeiryouChouseiHoshuLogic.cs 37791 2014-12-19 08:22:08Z fangjk@oec-h.com $
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KeiryouChouseiHoshu.APP;
using KeiryouChouseiHoshu.Dto;
using KeiryouChouseiHoshu.Validator;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace KeiryouChouseiHoshu.Logic
{
    /// <summary>
    /// 計量調整保守画面のビジネスロジック
    /// </summary>
    public class KeiryouChouseiHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "KeiryouChouseiHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_KEIRYOU_CHOUSEI_DATA_SQL = "KeiryouChouseiHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_ICHIRAN_HINMEI_DATA_SQL1 = "KeiryouChouseiHoshu.Sql.GetIchiranHinmeiDataSql1.sql";

        private readonly string GET_ICHIRAN_HINMEI_DATA_SQL2 = "KeiryouChouseiHoshu.Sql.GetIchiranHinmeiDataSql2.sql";

        private readonly string GET_ICHIRAN_SHURUI_DATA_SQL = "KeiryouChouseiHoshu.Sql.GetIchiranShuruiDataSql.sql";

        private readonly string GET_ICHIRAN_UNIT_DATA_SQL = "KeiryouChouseiHoshu.Sql.GetIchiranUnitDataSql.sql";

        private readonly string GET_KEIRYOU_CHOUSEI_DATA_SQL = "KeiryouChouseiHoshu.Sql.GetKeiryouChouseiDataSql.sql";

        private readonly string GET_TORIHIKISAKI_DATA_SQL = "KeiryouChouseiHoshu.Sql.GetTorihikisakiDataSql.sql";

        private readonly string GET_GYOUSHA_DATA_SQL = "KeiryouChouseiHoshu.Sql.GetGyoushaDataSql.sql";

        private readonly string GET_GENBA_DATA_SQL = "KeiryouChouseiHoshu.Sql.GetGenbaDataSql.sql";

        /// <summary>
        /// 計量調整保守画面Form
        /// </summary>
        private KeiryouChouseiHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_KEIRYOU_CHOUSEI[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 計量調整のDao
        /// </summary>
        private IM_KEIRYOU_CHOUSEIDao dao;

        /// <summary>
        /// 取引先のDao
        /// </summary>
        private IM_TORIHIKISAKIDao torihikisakiDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 種類のDao
        /// </summary>
        private IM_SHURUIDao shuruiDao;

        /// <summary>
        /// 品名のDao
        /// </summary>
        private IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// 単位のDao
        /// </summary>
        private IM_UNITDao unitDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

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
        public M_KEIRYOU_CHOUSEI SearchString { get; set; }

        /// <summary>
        /// 検索結果(取引先)
        /// </summary>
        public DataTable SearchResultTorihikisaki { get; set; }

        /// <summary>
        /// 検索結果(現場)
        /// </summary>
        public DataTable SearchResultGenba { get; set; }

        /// <summary>
        /// 検索結果(業者)
        /// </summary>
        public DataTable SearchResultGyousha { get; set; }

        /// <summary>
        /// 検索結果(種類)
        /// </summary>
        public DataTable SearchResultShurui { get; set; }

        /// <summary>
        /// 検索結果(品名)
        /// </summary>
        public DataTable SearchResultHinmei { get; set; }

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        public KeiryouChouseiHoshuDto keiryouChouseiHoshuDto { get; set; }

        #endregion プロパティ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public KeiryouChouseiHoshuLogic(KeiryouChouseiHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_KEIRYOU_CHOUSEIDao>();
            this.torihikisakiDao = DaoInitUtility.GetComponent<IM_TORIHIKISAKIDao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.shuruiDao = DaoInitUtility.GetComponent<IM_SHURUIDao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.keiryouChouseiHoshuDto = new KeiryouChouseiHoshuDto();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

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

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;
                this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GyoushaValue_Text;
                this.form.GYOUSHA_NAME_RYAKU.Text = Properties.Settings.Default.GyoushaName_Text;
                this.form.GENBA_CD.Text = Properties.Settings.Default.GenbaValue_Text;
                this.form.GENBA_NAME_RYAKU.Text = Properties.Settings.Default.GenbaName_Text;
                this.form.TORIHIKISAKI_CD.Text = Properties.Settings.Default.TorihikisakiValue_Text;
                this.form.TORIHIKISAKI_NAME_RYAKU.Text = Properties.Settings.Default.TorihikisakiName_Text;

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
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();
                // プロパティ設定有無チェック
                switch (this.keiryouChouseiHoshuDto.PropertiesKeiryouChouseiExistCheck())
                {
                    case 1:
                        //  品名マスタ検索（種類ＣＤ）
                        this.SearchResult = hinmeiDao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HINMEI_DATA_SQL1
                                                                    , this.keiryouChouseiHoshuDto.HinmeiSearchString
                                                                    , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                        break;

                    case 2:
                        //  品名マスタ検索（品名）
                        this.SearchResult = hinmeiDao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HINMEI_DATA_SQL2
                                                                    , this.keiryouChouseiHoshuDto.HinmeiSearchString
                                                                    , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                        break;

                    case 3:
                        //  種類マスタ検索
                        this.SearchResult = shuruiDao.GetIchiranDataSqlFile(this.GET_ICHIRAN_SHURUI_DATA_SQL
                                                                    , this.keiryouChouseiHoshuDto.ShuruiSearchString
                                                                    , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                        break;

                    case 4:
                        //  単位マスタ検索
                        this.SearchResult = unitDao.GetIchiranDataSqlFile(this.GET_ICHIRAN_UNIT_DATA_SQL
                                                                    , this.keiryouChouseiHoshuDto.UnitSearchString
                                                                    , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                        break;

                    default:
                        //  計量調整マスタ検索
                        this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_KEIRYOU_CHOUSEI_DATA_SQL
                                                                    , this.keiryouChouseiHoshuDto.KeiryouChouseiSearchString
                                                                    , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked
                                                                    , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                                                                    , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked);
                        break;
                }

                M_KEIRYOU_CHOUSEI searchParam = new M_KEIRYOU_CHOUSEI();
                searchParam.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                searchParam.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                searchParam.GENBA_CD = this.form.GENBA_CD.Text;
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_KEIRYOU_CHOUSEI_DATA_SQL, searchParam);

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;
                Properties.Settings.Default.GenbaValue_Text = this.form.GENBA_CD.Text;
                Properties.Settings.Default.GenbaName_Text = this.form.GENBA_NAME_RYAKU.Text;
                Properties.Settings.Default.GyoushaValue_Text = this.form.GYOUSHA_CD.Text;
                Properties.Settings.Default.GyoushaName_Text = this.form.GYOUSHA_NAME_RYAKU.Text;
                Properties.Settings.Default.TorihikisakiValue_Text = this.form.TORIHIKISAKI_CD.Text;
                Properties.Settings.Default.TorihikisakiName_Text = this.form.TORIHIKISAKI_NAME_RYAKU.Text;
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
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var focus = (this.form.TopLevelControl as Form).ActiveControl;
                this.form.Ichiran.Focus();

                var entityList = new M_KEIRYOU_CHOUSEI[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_KEIRYOU_CHOUSEI();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KEIRYOU_CHOUSEI>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KeiryouChouseiHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var keiryouChouseiEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_KEIRYOU_CHOUSEI> addList = new List<M_KEIRYOU_CHOUSEI>();
                foreach (var keiryouChouseiEntity in keiryouChouseiEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.KeiryouChouseiHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                            {
                                keiryouChouseiEntity.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                            }
                            else
                            {
                                keiryouChouseiEntity.TORIHIKISAKI_CD = "";
                            }
                            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                            {
                                keiryouChouseiEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                            }
                            else
                            {
                                keiryouChouseiEntity.GYOUSHA_CD = "";
                            }
                            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                            {
                                keiryouChouseiEntity.GENBA_CD = this.form.GENBA_CD.Text;
                            }
                            else
                            {
                                keiryouChouseiEntity.GENBA_CD = "";
                            }

                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), keiryouChouseiEntity);
                            addList.Add(keiryouChouseiEntity);
                            break;
                        }
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
        /// 計量調整の重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                KeiryouChouseiHoshuValidator vali = new KeiryouChouseiHoshuValidator();
                DataTable dt = this.form.Ichiran.DataSource as DataTable;

                if (this.SearchResult == null)
                {
                    this.SearchResult = dt;
                }
                if (this.SearchResultAll == null)
                {
                    this.SearchResultAll = dt;
                }

                bool result = vali.KeiryouChouseiCDValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd(result);

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
                msgLogic.MessageBoxShow("C011", "計量調整一覧表");

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

                    CSVFileLogic csvLogic = new CSVFileLogic();

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
        public void CancelCondition()
        {
            LogUtility.DebugMethodStart();

            ClearCondition();
            SetIchiran();

            LogUtility.DebugMethodEnd();
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
                        foreach (M_KEIRYOU_CHOUSEI keiryouChouseiEntity in this.entitys)
                        {
                            M_KEIRYOU_CHOUSEI entity = this.dao.GetDataByCd(keiryouChouseiEntity);
                            if (entity == null)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (keiryouChouseiEntity.DELETE_FLG)
                                {
                                    continue;
                                }
                                this.dao.Insert(keiryouChouseiEntity);
                            }
                            else
                            {
                                this.dao.Update(keiryouChouseiEntity);
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
                        foreach (M_KEIRYOU_CHOUSEI keiryouChouseiEntity in this.entitys)
                        {
                            M_KEIRYOU_CHOUSEI entity = this.dao.GetDataByCd(keiryouChouseiEntity);
                            if (entity != null)
                            {
                                this.dao.Update(keiryouChouseiEntity);
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

            KeiryouChouseiHoshuLogic localLogic = other as KeiryouChouseiHoshuLogic;
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
        /// 品名検索処理
        /// </summary>
        /// <param name="hinmeiCd"></param>
        /// <param name="rowIndex"></param>
        public bool SearchHinmei(string hinmeiCd, CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(hinmeiCd, e);

                int rowIndex = e.RowIndex;

                // 品名マスタの検索を行う
                // 種類が入力されていれば、種類も条件とする
                M_HINMEI cond = new M_HINMEI();
                cond.HINMEI_CD = hinmeiCd;
                if (this.form.Ichiran[rowIndex, Const.KeiryouChouseiHoshuConstans.SHURUI_CD].Value != null &&
                    !string.IsNullOrWhiteSpace(this.form.Ichiran[rowIndex, Const.KeiryouChouseiHoshuConstans.SHURUI_CD].Value.ToString()))
                {
                    cond.SHURUI_CD = this.form.Ichiran[rowIndex, Const.KeiryouChouseiHoshuConstans.SHURUI_CD].Value.ToString();
                }
                M_HINMEI[] hin = this.hinmeiDao.GetAllValidData(cond);
                if (hin == null || hin.Length <= 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "品名");
                    e.Cancel = true;
                    ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                }
                else
                {
                    // 品名略称をセットする
                    this.form.Ichiran[rowIndex, Const.KeiryouChouseiHoshuConstans.HINMEI_NAME_RYAKU].Value = hin[0].HINMEI_NAME_RYAKU;
                    // 種類が未指定の場合、品名に設定されている種類をセットする
                    if (string.IsNullOrWhiteSpace(cond.SHURUI_CD))
                    {
                        M_SHURUI shu = this.shuruiDao.GetDataByCd(hin[0].SHURUI_CD);
                        if (shu != null)
                        {
                            this.form.Ichiran[rowIndex, Const.KeiryouChouseiHoshuConstans.SHURUI_CD].Value = shu.SHURUI_CD;
                            this.form.Ichiran[rowIndex, Const.KeiryouChouseiHoshuConstans.SHURUI_NAME_RYAKU].Value = shu.SHURUI_NAME_RYAKU;
                        }
                        else
                        {
                            this.form.Ichiran[rowIndex, Const.KeiryouChouseiHoshuConstans.SHURUI_CD].Value = string.Empty;
                            this.form.Ichiran[rowIndex, Const.KeiryouChouseiHoshuConstans.SHURUI_NAME_RYAKU].Value = string.Empty;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(hinmeiCd, e);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchHinmei", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHinmei", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(hinmeiCd, e);
                return true;
            }
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            try
            {
                string WhereJouken = string.Empty;
                if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
                {
                    WhereJouken += "TORIHIKISAKI_CD = " + this.form.TORIHIKISAKI_CD.Text;
                }
                else
                {
                    WhereJouken += "TORIHIKISAKI_CD = ''";
                }
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    if (WhereJouken != "")
                    {
                        WhereJouken += " AND ";
                    }
                    WhereJouken += "GYOUSHA_CD = " + this.form.GYOUSHA_CD.Text;
                }
                else
                {
                    if (WhereJouken != "")
                    {
                        WhereJouken += " AND ";
                    }
                    WhereJouken += "GYOUSHA_CD = ''";
                }
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    if (WhereJouken != "")
                    {
                        WhereJouken += " AND ";
                    }
                    WhereJouken += "GENBA_CD = " + this.form.GENBA_CD.Text;
                }
                else
                {
                    if (WhereJouken != "")
                    {
                        WhereJouken += " AND ";
                    }
                    WhereJouken += "GENBA_CD = ''";
                }
                DataRow[] dr = this.SearchResult.Select(WhereJouken);

                DataTable table = this.SearchResult.Clone();

                foreach (DataRow row in dr)
                {
                    table.ImportRow(row);
                }

                if (dr == null)
                {
                    return false;
                }

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.DataSource = table;

                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            //削除ボタン(F4)イベント生成
            this.form.C_Regist(parentForm.bt_func4);
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
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

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
            M_KEIRYOU_CHOUSEI entityKeiryouChousei = new M_KEIRYOU_CHOUSEI();
            M_HINMEI entityHinmei = new M_HINMEI();
            M_SHURUI entityShurui = new M_SHURUI();
            M_UNIT entityUnit = new M_UNIT();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    //  計量調整マスタ項目チェック
                    bool isExist = this.EntityExistCheck(entityKeiryouChousei, this.form.CONDITION_VALUE.DBFieldsName);
                    if (isExist)
                    {
                        // 検索条件の設定(計量調整マスタ)
                        if (this.form.CONDITION_VALUE.ItemDefinedTypes == "double")
                        {
                            entityKeiryouChousei.SetValue(this.form.CONDITION_VALUE);
                        }
                        else
                        {
                            entityKeiryouChousei.SetValue(this.form.CONDITION_VALUE);
                        }
                    }
                    else
                    {
                        //  品名マスタ項目チェック
                        isExist = this.EntityExistCheck(entityHinmei, this.form.CONDITION_VALUE.DBFieldsName);
                        if (isExist)
                        {
                            // 検索条件の設定(品名マスタ)
                            entityHinmei.SetValue(this.form.CONDITION_VALUE);
                        }
                        else
                        {
                            //  種類マスタ項目チェック
                            isExist = this.EntityExistCheck(entityShurui, this.form.CONDITION_VALUE.DBFieldsName);
                            if (isExist)
                            {
                                // 検索条件の設定(種類マスタ)
                                entityShurui.SetValue(this.form.CONDITION_VALUE);
                            }
                            else
                            {
                                //  単位マスタ項目チェック
                                isExist = this.EntityExistCheck(entityUnit, this.form.CONDITION_VALUE.DBFieldsName);
                                if (isExist)
                                {
                                    // 検索条件の設定(単位マスタ)
                                    entityUnit.SetValue(this.form.CONDITION_VALUE);
                                }
                            }
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(this.form.TORIHIKISAKI_CD.Text))
            {
                // 検索条件の設定
                entityKeiryouChousei.SetValue(this.form.TORIHIKISAKI_CD);
            }

            if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
            {
                // 検索条件の設定
                entityKeiryouChousei.SetValue(this.form.GYOUSHA_CD);
            }

            if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
            {
                // 検索条件の設定
                entityKeiryouChousei.SetValue(this.form.GENBA_CD);
            }
            keiryouChouseiHoshuDto.HinmeiSearchString = entityHinmei;
            keiryouChouseiHoshuDto.ShuruiSearchString = entityShurui;
            keiryouChouseiHoshuDto.UnitSearchString = entityUnit;
            keiryouChouseiHoshuDto.KeiryouChouseiSearchString = entityKeiryouChousei;
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
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;
            this.form.TORIHIKISAKI_CD.Text = string.Empty;
            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
            this.form.GENBA_CD.Text = string.Empty;
            this.form.GENBA_NAME_RYAKU.Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }

        /// <summary>
        /// 取引先名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchTorihikisakiName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrWhiteSpace(this.form.TORIHIKISAKI_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    M_TORIHIKISAKI tmp = new M_TORIHIKISAKI();
                    tmp.TORIHIKISAKI_CD = this.form.TORIHIKISAKI_CD.Text;
                    this.SearchResultTorihikisaki = torihikisakiDao.GetDataBySqlFile(this.GET_TORIHIKISAKI_DATA_SQL, tmp);

                    if (this.SearchResultTorihikisaki != null && this.SearchResultTorihikisaki.Rows.Count > 0)
                    {
                        DataRow row = this.SearchResultTorihikisaki.Rows[0];
                        this.form.TORIHIKISAKI_NAME_RYAKU.Text = row["TORIHIKISAKI_NAME_RYAKU"].ToString();
                    }
                    else
                    {
                        if (e != null)
                        {
                            this.form.TORIHIKISAKI_NAME_RYAKU.Text = string.Empty;
                            msgLogic.MessageBoxShow("E020", "取引先");
                            e.Cancel = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchTorihikisakiName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchTorihikisakiName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 現場名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchGenbaName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                if (string.IsNullOrEmpty(this.form.GENBA_CD.Text)) { return false; }
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    if (e != null)
                    {
                        this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                        msgLogic.MessageBoxShow("E020", "現場");
                        e.Cancel = true;
                    }
                    return false;
                }
                // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END

                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text) && !string.IsNullOrWhiteSpace(this.form.GENBA_CD.Text))
                {
                    M_GENBA tmp = new M_GENBA();
                    tmp.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    tmp.GENBA_CD = this.form.GENBA_CD.Text;
                    this.SearchResultGenba = genbaDao.GetDataBySqlFile(this.GET_GENBA_DATA_SQL, tmp);

                    if (this.SearchResultGenba != null && this.SearchResultGenba.Rows.Count > 0)
                    {
                        DataRow row = this.SearchResultGenba.Rows[0];
                        this.form.GENBA_NAME_RYAKU.Text = row["GENBA_NAME_RYAKU"].ToString();

                        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                        this.SetTorihikisakiInfo(row["TORIHIKISAKI_CD"].ToString());
                        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
                    }
                    else
                    {
                        if (e != null)
                        {
                            this.form.GENBA_NAME_RYAKU.Text = string.Empty;
                            msgLogic.MessageBoxShow("E020", "現場");
                            e.Cancel = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGenbaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenbaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 業者名称情報の取得
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchGyoushaName(CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text))
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                    M_GYOUSHA tmp = new M_GYOUSHA();
                    tmp.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    this.SearchResultGyousha = gyoushaDao.GetDataBySqlFile(this.GET_GYOUSHA_DATA_SQL, tmp);

                    if (this.SearchResultGyousha != null && this.SearchResultGyousha.Rows.Count > 0)
                    {
                        DataRow row = this.SearchResultGyousha.Rows[0];
                        this.form.GYOUSHA_NAME_RYAKU.Text = row["GYOUSHA_NAME_RYAKU"].ToString();

                        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
                        this.SetTorihikisakiInfo(row["TORIHIKISAKI_CD"].ToString());
                        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
                    }
                    else
                    {
                        if (e != null)
                        {
                            this.form.GYOUSHA_NAME_RYAKU.Text = string.Empty;
                            msgLogic.MessageBoxShow("E020", "業者");
                            e.Cancel = true;
                        }
                    }
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGyoushaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyoushaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// データテーブル作成
        /// </summary>
        private DataTable createDataTable()
        {
            DataTable dt = new DataTable();

            // 取引先CD
            DataColumn dc1 = new DataColumn("TORIHIKISAKI_CD", Type.GetType("System.String"));

            // 業者CD
            DataColumn dc2 = new DataColumn("GYOUSHA_CD", Type.GetType("System.String"));

            // 現場CD
            DataColumn dc3 = new DataColumn("GENBA_CD", Type.GetType("System.String"));

            // 品名CD
            DataColumn dc4 = new DataColumn("HINMEI_CD", Type.GetType("System.String"));

            // 単位CD
            DataColumn dc5 = new DataColumn("UNIT_CD", Type.GetType("System.Int16"));

            // 調整値
            DataColumn dc6 = new DataColumn("CHOUSEICHI", Type.GetType("System.Double"));

            // 調整割合
            DataColumn dc7 = new DataColumn("CHOUSEIWARIAI", Type.GetType("System.Double"));

            // 計量調整備考
            DataColumn dc8 = new DataColumn("KEIRYOU_CHOUSEI_BIKOU", Type.GetType("System.String"));

            // 適用開始日
            DataColumn dc9 = new DataColumn("TEKIYOU_BEGIN", Type.GetType("System.DateTime"));

            // 適用終了日
            DataColumn dc10 = new DataColumn("TEKIYOU_END", Type.GetType("System.DateTime"));

            // 作成者
            DataColumn dc11 = new DataColumn("CREATE_USER", Type.GetType("System.String"));

            // 作成日時
            DataColumn dc12 = new DataColumn("CREATE_DATE", Type.GetType("System.DateTime"));

            // 最終更新者
            DataColumn dc13 = new DataColumn("UPDATE_USER", Type.GetType("System.String"));

            // 最終更新日時
            DataColumn dc14 = new DataColumn("UPDATE_DATE", Type.GetType("System.DateTime"));

            // 削除フラグ
            DataColumn dc15 = new DataColumn("DELETE_FLG", Type.GetType("System.String"));

            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);
            dt.Columns.Add(dc5);
            dt.Columns.Add(dc6);
            dt.Columns.Add(dc7);
            dt.Columns.Add(dc8);
            dt.Columns.Add(dc9);
            dt.Columns.Add(dc10);
            dt.Columns.Add(dc11);
            dt.Columns.Add(dc12);
            dt.Columns.Add(dc13);
            dt.Columns.Add(dc14);
            dt.Columns.Add(dc15);

            return dt;
        }

        /// <summary>
        /// Entity内のプロパティに指定プロパティが存在するかチェック
        /// </summary>
        /// <param name="entity">マスタEntity</param>
        /// <param name="dbFieldName">存在チェックしたいプロパティ名</param>
        /// <returns>true:プロパティあり、false:プロパティなし</returns>
        private bool EntityExistCheck(object entity, string dbFieldName)
        {
            bool result = false;

            // マスタEntityのプロパティ取得
            var properties = entity.GetType().GetProperties();

            // プロパティ名検索
            foreach (var property in properties)
            {
                if (property.Name == dbFieldName)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        /// 20141217 Houkakou 「計量調整入力」の日付チェックを追加する　start

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

                bool bFlag = false;
                catchErr = false;

                foreach (Row row in this.form.Ichiran.Rows)
                {
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

        /// 20141217 Houkakou 「計量調整入力」の日付チェックを追加する　end

        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) STR
        /// <summary>
        /// 取引先情報設定
        /// </summary>
        /// <param name="toriCd"></param>
        /// <returns></returns>
        internal void SetTorihikisakiInfo(string toriCd)
        {
            LogUtility.DebugMethodStart();

            if (!string.IsNullOrWhiteSpace(toriCd))
            {
                M_TORIHIKISAKI entity = new M_TORIHIKISAKI();
                entity.TORIHIKISAKI_CD = toriCd;
                var toriList = this.torihikisakiDao.GetAllValidData(entity);

                if (toriList != null && toriList.Length > 0)
                {
                    this.form.TORIHIKISAKI_CD.Text = toriList[0].TORIHIKISAKI_CD;
                    this.form.TORIHIKISAKI_NAME_RYAKU.Text = toriList[0].TORIHIKISAKI_NAME_RYAKU;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        // 20150914 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御(入力タイプ) END
    }
}