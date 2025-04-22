// $Id: HinmeiHoshuLogic.cs 53932 2015-06-29 09:37:00Z chenzz@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using HinmeiHoshu.APP;
using HinmeiHoshu.Const;
using HinmeiHoshu.Dto;
using HinmeiHoshu.Validator;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;

namespace HinmeiHoshu.Logic
{
    /// <summary>
    /// 品名保守画面のビジネスロジック
    /// </summary>
    public class HinmeiHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "HinmeiHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_HINMEI_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranHinmeidataSql.sql";

        private readonly string GET_HINMEI_DATA_SQL = "HinmeiHoshu.Sql.GetHinmeiDataSql.sql";

        private readonly string GET_UNIT_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranUnitDataSql.sql";

        private readonly string GET_SHURUI_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranShuruidataSql.sql";

        private readonly string GET_BUNRUI_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranBunruidataSql.sql";

        private readonly string GET_HOUKOKUSHO_BUNRUI_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranHoukokushoBunruidataSql.sql";

        private readonly string GET_JISSEKI_BUNRUI_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranJissekiBunruidataSql.sql";

        private readonly string CHECK_DELETE_HINMEI_SQL = "HinmeiHoshu.Sql.CheckDeleteHinmeiSql.sql";

        private readonly string GET_SP_CHOKKOU_HAIKI_SHURUI_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranSPChokkouHaikiShuruidataSql.sql";

        private readonly string GET_SP_TSUMIKAE_HAIKI_SHURUI_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranSPTsumikaeHaikiShuruidataSql.sql";

        private readonly string GET_KP_HAIKI_SHURUI_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranKPHaikiShuruidataSql.sql";

        private readonly string GET_DENSHI_HAIKI_SHURUI_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranDenshiHaikiShuruidataSql.sql";

        //20250313
        private readonly string GET_HAIKI_NAME_DATA_SQL = "HinmeiHoshu.Sql.GetIchiranHaikiNamedataSql.sql";

        /// <summary>
        /// 品名保守画面Form
        /// </summary>
        private HinmeiHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_HINMEI[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 品名のDao
        /// </summary>
        private IM_HINMEIDao dao;

        /// <summary>
        /// 単位のDao
        /// </summary>
        private IM_UNITDao unitDao;

        /// <summary>
        /// 伝種区分のDao
        /// </summary>
        private IM_DENSHU_KBNDao denshuKbnDao;

        /// <summary>
        /// 伝票区分のDao
        /// </summary>
        private IM_DENPYOU_KBNDao denpyouKbnDao;

        /// <summary>
        /// 種類のDao
        /// </summary>
        private IM_SHURUIDao shuruiDao;

        /// <summary>
        /// 分類のDao
        /// </summary>
        private IM_BUNRUIDao bunruiDao;

        /// <summary>
        /// 報告書分類のDao
        /// </summary>
        private IM_HOUKOKUSHO_BUNRUIDao houkokushoBunruiDao;

        /// <summary>
        /// 一般廃用報告書分類のDao
        /// </summary>
        private IM_JISSEKI_BUNRUIDao jissekiBunruiDao;

        /// <summary>
        /// 廃棄物種類のDao
        /// </summary>
        private IM_HAIKI_SHURUIDao haikiShuruiDao;

        /// <summary>
        /// 電子廃棄物種類のDao
        /// </summary>
        private IM_DENSHI_HAIKI_SHURUIDao denshiHaikiShuruiDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        //20250312
        private IM_HAIKI_NAMEDao haikiNameDao;

        //20250313
        private IM_NISUGATADao nisugataDao;

        private IM_SHOBUN_HOUHOUDao shobunHouhouDao;

        /// <summary>
        /// 単位情報のEntity
        /// </summary>
        private M_UNIT unitInfo;

        /// <summary>
        /// 単位情報のEntity
        /// </summary>
        private M_UNIT dispunitInfo;

        /// <summary>
        /// 伝種区分のEntity
        /// </summary>
        private M_DENSHU_KBN denshuKbnInfo;

        /// <summary>
        /// 伝票区分のEntity
        /// </summary>
        private M_DENPYOU_KBN denpyouKbnInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        // VUNGUYEN 20150525 #1294 START
        public Cell cell;
        // VUNGUYEN 20150525 #1294 END

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal MasterBaseForm parentForm;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索結果(重複チェック用)
        /// </summary>
        public DataTable SearchResultCheck { get; set; }

        /// <summary>
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_HINMEI SearchString { get; set; }

        /// <summary>
        /// 検索結果(単位)
        /// </summary>
        public DataTable SearchResultUnit { get; set; }

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        public HinmeiHoshuDto hinmeiHoshuDto { get; set; }

        /// <summary>
        /// 検索結果(伝種区分)
        /// </summary>
        private DataTable SearchResultDenshuKbn { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public HinmeiHoshuLogic(HinmeiHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.denpyouKbnDao = DaoInitUtility.GetComponent<IM_DENPYOU_KBNDao>();
            this.denshuKbnDao = DaoInitUtility.GetComponent<IM_DENSHU_KBNDao>();
            this.shuruiDao = DaoInitUtility.GetComponent<IM_SHURUIDao>();
            this.bunruiDao = DaoInitUtility.GetComponent<IM_BUNRUIDao>();
            this.houkokushoBunruiDao = DaoInitUtility.GetComponent<IM_HOUKOKUSHO_BUNRUIDao>();
            this.haikiShuruiDao = DaoInitUtility.GetComponent<IM_HAIKI_SHURUIDao>();
            this.denshiHaikiShuruiDao = DaoInitUtility.GetComponent<IM_DENSHI_HAIKI_SHURUIDao>();
            this.hinmeiHoshuDto = new HinmeiHoshuDto();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.jissekiBunruiDao = DaoInitUtility.GetComponent<IM_JISSEKI_BUNRUIDao>();

            //20250312
            this.haikiNameDao = DaoInitUtility.GetComponent<IM_HAIKI_NAMEDao>();

            //20250313
            this.nisugataDao = DaoInitUtility.GetComponent<IM_NISUGATADao>();
            this.shobunHouhouDao = DaoInitUtility.GetComponent<IM_SHOBUN_HOUHOUDao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            //システム設定より単位情報の取得
            this.unitInfo = null;
            if (false == this.entitySysInfo.KANSAN_UNIT_CD.IsNull)
            {
                int unitCd = this.entitySysInfo.KANSAN_UNIT_CD.Value;
                M_UNIT unitMasterInfo = unitDao.GetDataByCd(unitCd);
                if (unitMasterInfo != null)
                {
                    this.unitInfo = unitMasterInfo;
                }
            }

            //システム設定より表示単位略称の取得
            if (!this.entitySysInfo.HINMEI_UNIT_CD.IsNull)
            {
                int dispUniCd = this.entitySysInfo.HINMEI_UNIT_CD.Value;
                M_UNIT dispUnitInfo = unitDao.GetDataByCd(dispUniCd);
                this.dispunitInfo = null;
                if (dispUnitInfo != null)
                {
                    this.dispunitInfo = dispUnitInfo;
                }
            }

            ////システム設定より伝種区分情報の取得
            string denshuKbnCd = this.entitySysInfo.HINMEI_DENSHU_KBN_CD.Value.ToString();
            M_DENSHU_KBN denshuSysInfo = denshuKbnDao.GetDataByCd(denshuKbnCd);
            this.denshuKbnInfo = null;
            if (denshuSysInfo != null)
            {
                this.denshuKbnInfo = denshuSysInfo;
            }

            //システム設定より伝票区分情報の取得
            string denpyouKbunCd = this.entitySysInfo.HINMEI_DENPYOU_KBN_CD.Value.ToString();
            M_DENPYOU_KBN denpyouKbnSysInfo = denpyouKbnDao.GetDataByCd(denpyouKbunCd);
            this.denpyouKbnInfo = null;
            if (denpyouKbnSysInfo != null)
            {
                this.denpyouKbnInfo = denpyouKbnSysInfo;
            }

            // 伝種区分フォーカスアウト用に予めデータを取得
            string whereSql = "WHERE DENSHU_KBN_CD = 1 OR DENSHU_KBN_CD = 2 OR DENSHU_KBN_CD = 3 OR DENSHU_KBN_CD = 9";
            this.SearchResultDenshuKbn = denshuKbnDao.GetAllMasterDataForPopup(whereSql);

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

                this.form.Ichiran.Template = this.form.hinmeiHoshuDetail1;

                this.allControl = this.form.allControl;

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        public void SetHyoujiJoukenInit()
        {
            LogUtility.DebugMethodStart();

            if (this.entitySysInfo != null)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
            }

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

                SetSearchString();

                // プロパティ設定有無チェック
                if (this.hinmeiHoshuDto.PropertiesUnitExistsCheck())
                {
                    // 単位マスタの条件で取得
                    this.SearchResult = unitDao.GetIchiranDataSqlFile(this.GET_UNIT_DATA_SQL
                                            , this.hinmeiHoshuDto.UnitSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    this.SearchResultCheck = unitDao.GetIchiranDataSqlFile(this.GET_UNIT_DATA_SQL
                                            , this.hinmeiHoshuDto.UnitSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else if (this.hinmeiHoshuDto.PropertiesShuruiExistCheck())
                {
                    // 種類マスタの条件で取得
                    this.SearchResult = shuruiDao.GetIchiranDataSqlFile(this.GET_SHURUI_DATA_SQL
                                            , this.hinmeiHoshuDto.ShuruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    this.SearchResultCheck = shuruiDao.GetIchiranDataSqlFile(this.GET_SHURUI_DATA_SQL
                                            , this.hinmeiHoshuDto.ShuruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else if (this.hinmeiHoshuDto.PropertiesBunruiExistCheck())
                {
                    // 分類マスタの条件で取得
                    this.SearchResult = bunruiDao.GetIchiranDataSqlFile(this.GET_BUNRUI_DATA_SQL
                                            , this.hinmeiHoshuDto.BunruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    this.SearchResultCheck = bunruiDao.GetIchiranDataSqlFile(this.GET_BUNRUI_DATA_SQL
                                            , this.hinmeiHoshuDto.BunruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else if (this.hinmeiHoshuDto.PropertiesHoukokushoBunruiExistCheck())
                {
                    // 報告書分類マスタの条件で取得
                    this.SearchResult = houkokushoBunruiDao.GetIchiranDataSqlFile(this.GET_HOUKOKUSHO_BUNRUI_DATA_SQL
                                            , this.hinmeiHoshuDto.HoukokushoBunruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    this.SearchResultCheck = houkokushoBunruiDao.GetIchiranDataSqlFile(this.GET_HOUKOKUSHO_BUNRUI_DATA_SQL
                                            , this.hinmeiHoshuDto.HoukokushoBunruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else if (this.hinmeiHoshuDto.PropertiesJissekiBunruiExistCheck())
                {
                    // 一般廃用報告書分類マスタの条件で取得
                    this.SearchResult = jissekiBunruiDao.GetIchiranDataSqlFile2(this.GET_JISSEKI_BUNRUI_DATA_SQL
                                            , this.hinmeiHoshuDto.JissekiBunruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    this.SearchResultCheck = jissekiBunruiDao.GetIchiranDataSqlFile2(this.GET_JISSEKI_BUNRUI_DATA_SQL
                                            , this.hinmeiHoshuDto.JissekiBunruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                #region 20250314
                //else if (this.hinmeiHoshuDto.PropertiesHaikiNameExistCheck())
                //{
                //    this.SearchResult = haikiNameDao.GetIchiranDataSqlFile(this.GET_HAIKI_NAME_DATA_SQL
                //                            , this.hinmeiHoshuDto.HaikiNameSearchString
                //                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                //    this.SearchResultCheck = haikiNameDao.GetIchiranDataSqlFile(this.GET_HAIKI_NAME_DATA_SQL
                //                            , this.hinmeiHoshuDto.HaikiNameSearchString
                //                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                //}
                #endregion 20250314
                else if (this.hinmeiHoshuDto.PropertiesHaikiShuruiExistCheck())
                {
                    if (this.form.CONDITION_VALUE.DBFieldsName == Const.HinmeiHoshuConstans.SP_CHOKKOU_HAIKI_SHURUI_NAME)
                    {
                        // 産廃直行廃棄物種類マスタの条件で取得
                        this.SearchResult = haikiShuruiDao.GetIchiranDataSqlFile(this.GET_SP_CHOKKOU_HAIKI_SHURUI_DATA_SQL
                                                , this.hinmeiHoshuDto.HaikiShuruiSearchString
                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                        this.SearchResultCheck = haikiShuruiDao.GetIchiranDataSqlFile(this.GET_SP_CHOKKOU_HAIKI_SHURUI_DATA_SQL
                                                , this.hinmeiHoshuDto.HaikiShuruiSearchString
                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    }
                    else if (this.form.CONDITION_VALUE.DBFieldsName == Const.HinmeiHoshuConstans.SP_TSUMIKAE_HAIKI_SHURUI_NAME)
                    {
                        // 産廃積替廃棄物種類マスタの条件で取得
                        this.SearchResult = haikiShuruiDao.GetIchiranDataSqlFile(this.GET_SP_TSUMIKAE_HAIKI_SHURUI_DATA_SQL
                                                , this.hinmeiHoshuDto.HaikiShuruiSearchString
                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                        this.SearchResultCheck = haikiShuruiDao.GetIchiranDataSqlFile(this.GET_SP_TSUMIKAE_HAIKI_SHURUI_DATA_SQL
                                                , this.hinmeiHoshuDto.HaikiShuruiSearchString
                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    }
                    else if (this.form.CONDITION_VALUE.DBFieldsName == Const.HinmeiHoshuConstans.KP_HAIKI_SHURUI_NAME)
                    {
                        // 建廃廃棄物種類マスタの条件で取得
                        this.SearchResult = haikiShuruiDao.GetIchiranDataSqlFile(this.GET_KP_HAIKI_SHURUI_DATA_SQL
                                                , this.hinmeiHoshuDto.HaikiShuruiSearchString
                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                        this.SearchResultCheck = haikiShuruiDao.GetIchiranDataSqlFile(this.GET_KP_HAIKI_SHURUI_DATA_SQL
                                                , this.hinmeiHoshuDto.HaikiShuruiSearchString
                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    }
                }
                else if (this.hinmeiHoshuDto.PropertiesDenshiHaikiShuruiExistCheck())
                {
                    // 電子廃棄物種類マスタの条件で取得
                    this.SearchResult = denshiHaikiShuruiDao.GetIchiranDataSqlFile(this.GET_DENSHI_HAIKI_SHURUI_DATA_SQL
                                            , this.hinmeiHoshuDto.DenshiHaikiShuruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    this.SearchResultCheck = denshiHaikiShuruiDao.GetIchiranDataSqlFile(this.GET_DENSHI_HAIKI_SHURUI_DATA_SQL
                                            , this.hinmeiHoshuDto.DenshiHaikiShuruiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else
                {
                    // 品名マスタの条件で取得
                    this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HINMEI_DATA_SQL
                                            , this.hinmeiHoshuDto.HinmeiSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                    this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HINMEI_DATA_SQL
                                                , this.hinmeiHoshuDto.HinmeiSearchString
                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }

                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_HINMEI_DATA_SQL, new M_HINMEI());

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                // 検索結果の税区分名を設定する
                this.SearchResult.Columns[Const.HinmeiHoshuConstans.ZEI_KBN_NAME_RYAKU].ReadOnly = false;
                foreach (DataRow row in this.SearchResult.Rows)
                {
                    row[Const.HinmeiHoshuConstans.ZEI_KBN_NAME_RYAKU] = this.GetZeiKbnName(row[Const.HinmeiHoshuConstans.ZEI_KBN_CD]);
                }

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

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
                LogUtility.DebugMethodStart();

                var entityList = new M_HINMEI[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_HINMEI();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_HINMEI>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.HinmeiHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var hinmeiEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_HINMEI> addList = new List<M_HINMEI>();
                foreach (var hinmeiEntity in hinmeiEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.HinmeiHoshuConstans.HINMEI_CD) && n.Value.ToString().Equals(hinmeiEntity.HINMEI_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.HinmeiHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            // 修正対象が本当に修正されているかチェックする
                            if (!hinmeiEntity.HINMEI_CD.Equals(string.Empty))
                            {
                                DataRow[] dr = this.SearchResultAll.Select(String.Format("HINMEI_CD = '{0}'", hinmeiEntity.HINMEI_CD));
                                if (dr.Length > 0
                                    && ((bool)dr[0][Const.HinmeiHoshuConstans.DELETE_FLG]).Equals(hinmeiEntity.DELETE_FLG.Value)
                                    && dr[0][Const.HinmeiHoshuConstans.HINMEI_CD].ToString().Equals(hinmeiEntity.HINMEI_CD)
                                    && dr[0][Const.HinmeiHoshuConstans.HINMEI_NAME].ToString().Equals(hinmeiEntity.HINMEI_NAME)
                                    && dr[0][Const.HinmeiHoshuConstans.HINMEI_NAME_RYAKU].ToString().Equals(hinmeiEntity.HINMEI_NAME_RYAKU)
                                    && dr[0][Const.HinmeiHoshuConstans.HINMEI_FURIGANA].ToString().Equals(hinmeiEntity.HINMEI_FURIGANA)
                                    && (dr[0][Const.HinmeiHoshuConstans.UNIT_CD] == null ? string.Empty : dr[0][Const.HinmeiHoshuConstans.UNIT_CD].ToString()) == (hinmeiEntity.UNIT_CD.IsNull ? string.Empty : hinmeiEntity.UNIT_CD.ToString())
                                    && (dr[0][Const.HinmeiHoshuConstans.DENSHU_KBN_CD] == null ? string.Empty : dr[0][Const.HinmeiHoshuConstans.DENSHU_KBN_CD].ToString()) == (hinmeiEntity.DENSHU_KBN_CD.IsNull ? string.Empty : hinmeiEntity.DENSHU_KBN_CD.ToString())
                                    && (dr[0][Const.HinmeiHoshuConstans.DENPYOU_KBN_CD] == null ? string.Empty : dr[0][Const.HinmeiHoshuConstans.DENPYOU_KBN_CD].ToString()) == (hinmeiEntity.DENPYOU_KBN_CD.IsNull ? string.Empty : hinmeiEntity.DENPYOU_KBN_CD.ToString())
                                    && (dr[0][Const.HinmeiHoshuConstans.ZEI_KBN_CD] == null ? string.Empty : dr[0][Const.HinmeiHoshuConstans.ZEI_KBN_CD].ToString()) == (hinmeiEntity.ZEI_KBN_CD.IsNull ? string.Empty : hinmeiEntity.ZEI_KBN_CD.ToString())
                                    && dr[0][Const.HinmeiHoshuConstans.SHURUI_CD].ToString().Equals(hinmeiEntity.SHURUI_CD)
                                    && dr[0][Const.HinmeiHoshuConstans.BUNRUI_CD].ToString().Equals(hinmeiEntity.BUNRUI_CD)
                                    && dr[0][Const.HinmeiHoshuConstans.SP_CHOKKOU_HAIKI_SHURUI_CD].ToString().Equals(hinmeiEntity.SP_CHOKKOU_HAIKI_SHURUI_CD)
                                    && dr[0][Const.HinmeiHoshuConstans.SP_TSUMIKAE_HAIKI_SHURUI_CD].ToString().Equals(hinmeiEntity.SP_TSUMIKAE_HAIKI_SHURUI_CD)
                                    && dr[0][Const.HinmeiHoshuConstans.JISSEKI_BUNRUI_CD].ToString().Equals(hinmeiEntity.JISSEKI_BUNRUI_CD)
                                    && dr[0][Const.HinmeiHoshuConstans.KP_HAIKI_SHURUI_CD].ToString().Equals(hinmeiEntity.KP_HAIKI_SHURUI_CD)
                                    && dr[0][Const.HinmeiHoshuConstans.DM_HAIKI_SHURUI_CD].ToString().Equals(hinmeiEntity.DM_HAIKI_SHURUI_CD)
                                    && dr[0][HinmeiHoshuConstans.TC_KOME_KANZANKEISU].ToString().Equals(hinmeiEntity.TC_KOME_KANZANKEISU) //20250312
                                    && dr[0][HinmeiHoshuConstans.HAIKI_MONO_MEISHO_CD].ToString().Equals(hinmeiEntity.HAIKI_MONO_MEISHO_CD) //20250313
                                    && dr[0][HinmeiHoshuConstans.NISUGATA_CD].ToString().Equals(hinmeiEntity.NISUGATA_CD)
                                    && dr[0][HinmeiHoshuConstans.SHOBUN_HOHO_CD].ToString().Equals(hinmeiEntity.SHOBUN_HOHO_CD))
                                {
                                    break;
                                }
                            }

                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), hinmeiEntity);
                            addList.Add(hinmeiEntity);
                            break;
                        }
                    }
                }

                this.form.Ichiran.DataSource = preDt;

                this.entitys = addList.ToArray();

                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 品名CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            LogUtility.DebugMethodStart();

            HinmeiHoshuValidator vali = new HinmeiHoshuValidator();
            bool result = vali.HinmeiCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);

            LogUtility.DebugMethodEnd();

            return result;
        }

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var hinmeiCd = string.Empty;
                string[] strList;

                foreach (Row gcRwos in this.form.Ichiran.Rows)
                {
                    if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                    {
                        hinmeiCd += gcRwos.Cells["HINMEI_CD"].Value.ToString() + ",";
                    }
                }

                if (!string.IsNullOrEmpty(hinmeiCd))
                {
                    hinmeiCd = hinmeiCd.Substring(0, hinmeiCd.Length - 1);
                    strList = hinmeiCd.Split(',');
                    DataTable dtTable = dao.GetDataBySqlFileCheck(this.CHECK_DELETE_HINMEI_SQL, strList);
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Empty;

                        foreach (DataRow dr in dtTable.Rows)
                        {
                            strName += "\n" + dr["NAME"].ToString();
                        }

                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E258", "品名", "品名CD", strName);

                        ret = false;
                    }
                    else
                    {
                        ret = true;
                    }
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
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
                msgLogic.MessageBoxShow("C011", "品名一覧表");

                MessageBox.Show("未実装");

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
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
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
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
                SetSearchString();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
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
                        foreach (M_HINMEI hinmeiEntity in this.entitys)
                        {
                            M_HINMEI entity = this.dao.GetDataByCd(hinmeiEntity.HINMEI_CD);
                            if (entity == null)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (hinmeiEntity.DELETE_FLG)
                                {
                                    continue;
                                }
                                this.dao.Insert(hinmeiEntity);
                            }
                            else
                            {
                                this.dao.Update(hinmeiEntity);
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
                        foreach (M_HINMEI hinmeiEntity in this.entitys)
                        {
                            M_HINMEI entity = this.dao.GetDataByCd(hinmeiEntity.HINMEI_CD);
                            if (entity != null)
                            {
                                this.dao.Update(hinmeiEntity);
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

        #endregion

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

            HinmeiHoshuLogic localLogic = other as HinmeiHoshuLogic;
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

        #endregion

        /// <summary>
        /// 税区分名称の設定
        /// </summary>
        /// <param name="rowIndex"></param>
        public bool SetZeiKbnName(int rowIndex)
        {
            try
            {
                LogUtility.DebugMethodStart(rowIndex);

                Row row = this.form.Ichiran.Rows[rowIndex];
                Cell cell = row.Cells[Const.HinmeiHoshuConstans.ZEI_KBN_CD];
                int zeiKbnCD = 0;
                if (cell.Value != null && !string.IsNullOrEmpty(cell.Value.ToString()))
                {
                    zeiKbnCD = int.Parse(cell.Value.ToString());
                }

                string zeiKbnName = this.GetZeiKbnName(zeiKbnCD);

                row.Cells[Const.HinmeiHoshuConstans.ZEI_KBN_NAME_RYAKU].Value = zeiKbnName;

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetZeiKbnName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 税区分名称取得処理
        /// </summary>
        /// <param name="zeiKbnCd"></param>
        /// <returns></returns>
        public string GetZeiKbnName(object zeiKbnCd)
        {
            LogUtility.DebugMethodStart(zeiKbnCd);

            int cd = 0;
            if (zeiKbnCd != null && !string.IsNullOrWhiteSpace(zeiKbnCd.ToString()))
            {
                cd = int.Parse(zeiKbnCd.ToString());
            }

            string zeiKbnName = string.Empty;
            switch (cd)
            {
                case 1:
                    zeiKbnName = Const.HinmeiHoshuConstans.ZEI_KBN_NAME_SOTOZEI;
                    break;

                case 2:
                    zeiKbnName = Const.HinmeiHoshuConstans.ZEI_KBN_NAME_UTIZEI;
                    break;

                case 3:
                    zeiKbnName = Const.HinmeiHoshuConstans.ZEI_KBN_NAME_HIKAZEI;
                    break;

                default:
                    break;
            }

            LogUtility.DebugMethodEnd(zeiKbnName);
            return zeiKbnName;
        }

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        public bool SetIchiran()
        {
            try
            {
                var table = this.SearchResult;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.DataSource = table;

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M230", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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

            //検索条件イベント生成
            this.form.CONDITION_VALUE.KeyPress += new KeyPressEventHandler(CONDITION_VALUE_KeyPress);
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
            M_HINMEI entityHinmei = new M_HINMEI();
            M_UNIT entityUnit = new M_UNIT();
            M_SHURUI entityShurui = new M_SHURUI();
            M_BUNRUI entityBunrui = new M_BUNRUI();
            M_HOUKOKUSHO_BUNRUI entityHoukokushoBunrui = new M_HOUKOKUSHO_BUNRUI();
            M_HAIKI_SHURUI entityHaikiShurui = new M_HAIKI_SHURUI();
            M_DENSHI_HAIKI_SHURUI entityDenshiHaikiShurui = new M_DENSHI_HAIKI_SHURUI();
            M_JISSEKI_BUNRUI entityJissekiBunrui = new M_JISSEKI_BUNRUI();

            //20250313
            M_HAIKI_NAME entityHaikiName = new M_HAIKI_NAME();
            M_NISUGATA entityNisugata = new M_NISUGATA();
            M_SHOBUN_HOUHOU entityShobunHouhou = new M_SHOBUN_HOUHOU();
            

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // 単位が条件の時はCDではなく略名称に変更
                    if (this.form.CONDITION_VALUE.DBFieldsName == "UNIT_CD")
                    {
                        this.form.CONDITION_VALUE.DBFieldsName = "UNIT_NAME_RYAKU";
                        this.form.CONDITION_VALUE.ItemDefinedTypes = "varchar";
                    }

                    bool isExistHinmei = this.EntityExistCheck(entityHinmei, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistUnit = this.EntityExistCheck(entityUnit, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistShurui = this.EntityExistCheck(entityShurui, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistBunrui = this.EntityExistCheck(entityBunrui, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistJissekiBunrui = this.EntityExistCheck(entityJissekiBunrui, this.form.CONDITION_VALUE.DBFieldsName);

                    //20250314
                    bool isExistHaikiName = this.EntityExistCheck(entityHaikiName, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistNisugata = this.EntityExistCheck(entityNisugata, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistShobunHouhou = this.EntityExistCheck(entityShobunHouhou, this.form.CONDITION_VALUE.DBFieldsName);

                    if (isExistHinmei)
                    {
                        // 検索条件の設定(品名マスタ)
                        entityHinmei.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (isExistUnit)
                    {
                        // 検索条件の設定(単位マスタ)
                        entityUnit.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (isExistShurui)
                    {
                        // 検索条件の設定(種類マスタ)
                        entityShurui.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (isExistBunrui)
                    {
                        // 検索条件の設定(分類マスタ)
                        entityBunrui.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (isExistJissekiBunrui)
                    {
                        entityJissekiBunrui.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (this.form.CONDITION_VALUE.DBFieldsName == Const.HinmeiHoshuConstans.SP_CHOKKOU_HAIKI_SHURUI_NAME
                            || this.form.CONDITION_VALUE.DBFieldsName == Const.HinmeiHoshuConstans.SP_TSUMIKAE_HAIKI_SHURUI_NAME
                            || this.form.CONDITION_VALUE.DBFieldsName == Const.HinmeiHoshuConstans.KP_HAIKI_SHURUI_NAME)
                    {
                        entityHaikiShurui.HAIKI_SHURUI_NAME_RYAKU = this.form.CONDITION_VALUE.Text;
                    }
                    else if (this.form.CONDITION_VALUE.DBFieldsName == Const.HinmeiHoshuConstans.DM_HAIKI_SHURUI_NAME)
                    {
                        entityDenshiHaikiShurui.HAIKI_SHURUI_NAME = this.form.CONDITION_VALUE.Text;
                    }
                    #region 20250314
                    //else if (this.form.CONDITION_VALUE.DBFieldsName == HinmeiHoshuConstans.HAIKI_MONO_MEISHO_CD)
                    //{
                    //    entityHaikiName.HAIKI_NAME_RYAKU = this.form.CONDITION_VALUE.Text;
                    //}
                    //else if (this.form.CONDITION_VALUE.DBFieldsName == HinmeiHoshuConstans.NISUGATA_CD)
                    //{
                    //    entityNisugata.NISUGATA_NAME = this.form.CONDITION_VALUE.Text;
                    //}
                    //else if (this.form.CONDITION_VALUE.DBFieldsName == HinmeiHoshuConstans.SHOBUN_HOHO_CD)
                    //{
                    //    entityShobunHouhou.SHOBUN_HOUHOU_NAME = this.form.CONDITION_VALUE.Text;
                    //}
                    else if (isExistHaikiName)
                    {
                        entityHaikiName.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (isExistNisugata)
                    {
                        entityNisugata.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (isExistShobunHouhou)
                    {
                        entityShobunHouhou.SetValue(this.form.CONDITION_VALUE);
                    }
                    #endregion 20250314
                    else
                    {
                        // 検索条件の設定(報告書分類マスタ)
                        entityHoukokushoBunrui.SetValue(this.form.CONDITION_VALUE);
                    }
                }
            }

            hinmeiHoshuDto.ShuruiSearchString = entityShurui;
            hinmeiHoshuDto.UnitSearchString = entityUnit;
            hinmeiHoshuDto.BunruiSearchString = entityBunrui;
            hinmeiHoshuDto.HoukokushoBunruiSearchString = entityHoukokushoBunrui;
            hinmeiHoshuDto.HinmeiSearchString = entityHinmei;
            hinmeiHoshuDto.JissekiBunruiSearchString = entityJissekiBunrui;
            hinmeiHoshuDto.HaikiShuruiSearchString = entityHaikiShurui;
            hinmeiHoshuDto.DenshiHaikiShuruiSearchString = entityDenshiHaikiShurui;

            //20250312
            hinmeiHoshuDto.HaikiNameSearchString = entityHaikiName;
            hinmeiHoshuDto.NisugataSearchString = entityNisugata;
            hinmeiHoshuDto.ShobunHouhouSearchString = entityShobunHouhou;
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

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }

        /// <summary>
        /// 新しい行にシステムデータ初期値設定
        /// </summary>
        internal bool settingSysDataDisp(int index)
        {
            try
            {
                //システム初期値の単位が必須で無いためNullチェックが必要
                if (!this.entitySysInfo.HINMEI_UNIT_CD.IsNull)
                {
                    this.form.Ichiran[index, "UNIT_CD"].Value = this.entitySysInfo.HINMEI_UNIT_CD.Value;
                    this.form.Ichiran[index, "UNIT_NAME_RYAKU"].Value = this.dispunitInfo.UNIT_NAME_RYAKU;
                }

                this.form.Ichiran[index, "DENSHU_KBN_CD"].Value = this.entitySysInfo.HINMEI_DENSHU_KBN_CD.Value;
                this.form.Ichiran[index, "DENSHU_KBN_NAME_RYAKU"].Value = this.denshuKbnInfo.DENSHU_KBN_NAME_RYAKU;

                this.form.Ichiran[index, "DENPYOU_KBN_CD"].Value = this.entitySysInfo.HINMEI_DENPYOU_KBN_CD.Value;
                this.form.Ichiran[index, "DENPYOU_KBN_NAME_RYAKU"].Value = this.denpyouKbnInfo.DENPYOU_KBN_NAME_RYAKU;

                // システム設定の税区分/締処理形態は暫定で「1：請求毎税・伝票毎税」固定のため、下記条件をコメントアウト
                //if (!this.entitySysInfo.SYS_ZEI_KEISAN_KBN_USE_KBN.IsNull && this.entitySysInfo.SYS_ZEI_KEISAN_KBN_USE_KBN.Value != 1)
                //{
                //システム初期値の税区分が必須で無いためNullチェックが必要
                if (!this.entitySysInfo.HINMEI_ZEI_KBN_CD.IsNull)
                {
                    this.form.Ichiran[index, "ZEI_KBN_CD"].Value = this.entitySysInfo.HINMEI_ZEI_KBN_CD.Value;
                    this.SetZeiKbnName(index);
                }
                //}
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("settingSysDataDisp", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 一覧バリデーション処理
        /// </summary>
        /// <param name="e"></param>
        public bool IchiranValidating(object sender, CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (e.CellName.Equals(Const.HinmeiHoshuConstans.HINMEI_CD))
                {
                    bool isNoErr = this.DuplicationCheck();
                    if (!isNoErr)
                    {
                        e.Cancel = true;

                        GcMultiRow gc = sender as GcMultiRow;
                        if (gc != null && gc.EditingControl != null)
                        {
                            ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                        }
                    }
                }
                if (e.CellName.Equals(Const.HinmeiHoshuConstans.SP_CHOKKOU_HAIKI_SHURUI_CD))
                {
                    if (e.FormattedValue != null && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        M_HAIKI_SHURUI c = new M_HAIKI_SHURUI();
                        c.HAIKI_KBN_CD = 1;
                        c.HAIKI_SHURUI_CD = e.FormattedValue.ToString().PadLeft(4, '0');
                        M_HAIKI_SHURUI[] shu = this.haikiShuruiDao.GetAllValidData(c);
                        if (shu != null && shu.Length > 0)
                        {
                            this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = shu[0].HAIKI_SHURUI_NAME_RYAKU;
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "廃棄物種類");
                            e.Cancel = true;
                            if (this.form.Ichiran.EditingControl != null)
                            {
                                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                            }
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = string.Empty;
                    }
                }
                if (e.CellName.Equals(Const.HinmeiHoshuConstans.SP_TSUMIKAE_HAIKI_SHURUI_CD))
                {
                    if (e.FormattedValue != null && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        M_HAIKI_SHURUI c = new M_HAIKI_SHURUI();
                        c.HAIKI_KBN_CD = 3;
                        c.HAIKI_SHURUI_CD = e.FormattedValue.ToString().PadLeft(4, '0');
                        M_HAIKI_SHURUI[] shu = this.haikiShuruiDao.GetAllValidData(c);
                        if (shu != null && shu.Length > 0)
                        {
                            this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = shu[0].HAIKI_SHURUI_NAME_RYAKU;
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "廃棄物種類");
                            e.Cancel = true;
                            if (this.form.Ichiran.EditingControl != null)
                            {
                                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                            }
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = string.Empty;
                    }
                }
                if (e.CellName.Equals(Const.HinmeiHoshuConstans.KP_HAIKI_SHURUI_CD))
                {
                    if (e.FormattedValue != null && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        M_HAIKI_SHURUI c = new M_HAIKI_SHURUI();
                        c.HAIKI_KBN_CD = 2;
                        c.HAIKI_SHURUI_CD = e.FormattedValue.ToString().PadLeft(4, '0');
                        M_HAIKI_SHURUI[] shu = this.haikiShuruiDao.GetAllValidData(c);
                        if (shu != null && shu.Length > 0)
                        {
                            this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = shu[0].HAIKI_SHURUI_NAME_RYAKU;
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "廃棄物種類");
                            e.Cancel = true;
                            if (this.form.Ichiran.EditingControl != null)
                            {
                                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                            }
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = string.Empty;
                    }
                }
                if (e.CellName.Equals(Const.HinmeiHoshuConstans.DM_HAIKI_SHURUI_CD))
                {
                    if (e.FormattedValue != null && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        M_DENSHI_HAIKI_SHURUI c = new M_DENSHI_HAIKI_SHURUI();
                        c.HAIKI_SHURUI_CD = e.FormattedValue.ToString().PadLeft(4, '0');
                        M_DENSHI_HAIKI_SHURUI[] shu = this.denshiHaikiShuruiDao.GetAllValidData(c);
                        if (shu != null && shu.Length > 0)
                        {
                            this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = shu[0].HAIKI_SHURUI_NAME;
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "電子廃棄物種類");
                            e.Cancel = true;
                            if (this.form.Ichiran.EditingControl != null)
                            {
                                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                            }
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = string.Empty;
                    }
                }

                #region 20250312
                if (e.CellName.Equals(Const.HinmeiHoshuConstans.HAIKI_MONO_MEISHO_CD))
                {
                    if (e.FormattedValue != null && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        M_HAIKI_NAME c = new M_HAIKI_NAME();
                        c.HAIKI_NAME_CD = e.FormattedValue.ToString().PadLeft(6, '0');
                        M_HAIKI_NAME[] mei = this.haikiNameDao.GetAllValidData(c);
                        if (mei != null && mei.Length > 0)
                        {
                            this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = mei[0].HAIKI_NAME_RYAKU;
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "");
                            e.Cancel = true;
                            if (this.form.Ichiran.EditingControl != null)
                            {
                                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                            }
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = string.Empty;
                    }
                }
                #endregion 20250312

                #region 20250313
                if (e.CellName.Equals(Const.HinmeiHoshuConstans.NISUGATA_CD))
                {
                    if (e.FormattedValue != null && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        M_NISUGATA c = new M_NISUGATA();
                        c.NISUGATA_CD = e.FormattedValue.ToString().PadLeft(2, '0');
                        M_NISUGATA[] ni = this.nisugataDao.GetAllValidData(c);
                        if (ni != null && ni.Length > 0)
                        {
                            this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = ni[0].NISUGATA_NAME;
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "");
                            e.Cancel = true;
                            if (this.form.Ichiran.EditingControl != null)
                            {
                                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                            }
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = string.Empty;
                    }
                }

                if (e.CellName.Equals(Const.HinmeiHoshuConstans.SHOBUN_HOHO_CD))
                {
                    if (e.FormattedValue != null && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        M_SHOBUN_HOUHOU c = new M_SHOBUN_HOUHOU();
                        c.SHOBUN_HOUHOU_CD = e.FormattedValue.ToString().PadLeft(3, '0');
                        M_SHOBUN_HOUHOU[] sho = this.shobunHouhouDao.GetAllValidData(c);
                        if (sho != null && sho.Length > 0)
                        {
                            this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = sho[0].SHOBUN_HOUHOU_NAME;
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", "");
                            e.Cancel = true;
                            if (this.form.Ichiran.EditingControl != null)
                            {
                                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                            }
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = string.Empty;
                    }
                }
                #endregion 20250313

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allPrimaryKeyList = this.dao.GetAllData().Select(s => s.HINMEI_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["HINMEI_CD"]).Where(c => c.Value != null).ToList().
                                            ForEach(c =>
                                            {
                                                c.ReadOnly = allPrimaryKeyList.Contains(c.Value.ToString());
                                                c.UpdateBackColor(false);
                                            });
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 伝種区分の存在チェックおよび伝種区分名の設定を行います
        /// </summary>
        /// <param name="e">CellValidatingEventArgs</param>
        internal bool DenshuKbnCheckAndSetting(CellValidatingEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return false;

                if (e.FormattedValue == null || string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    this.form.Ichiran.Rows[e.RowIndex].Cells["DENSHU_KBN_NAME_RYAKU"].Value = string.Empty;
                    return false;
                }

                bool isExist = false;
                string val = this.form.Ichiran.Rows[e.RowIndex].Cells[e.CellIndex].Value.ToString();
                foreach (DataRow dr in this.SearchResultDenshuKbn.Rows)
                {
                    if (val.Equals(dr["CD"].ToString()))
                    {
                        isExist = true;
                        this.form.Ichiran.Rows[e.RowIndex].Cells["DENSHU_KBN_NAME_RYAKU"].Value = dr["NAME"].ToString();
                    }
                }

                if (!isExist)
                {
                    new MessageBoxShowLogic().MessageBoxShow("E020", "伝種区分");
                    e.Cancel = true;
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DenshuKbnCheckAndSetting", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        /// <summary>
        /// 検索条件が数字のみ入力.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.form.CONDITION_VALUE.DBFieldsName.Equals(Const.HinmeiHoshuConstans.DENPYOU_KBN_CD)
                || this.form.CONDITION_VALUE.DBFieldsName.Equals(Const.HinmeiHoshuConstans.DENSHU_KBN_CD)
                || this.form.CONDITION_VALUE.DBFieldsName.Equals(Const.HinmeiHoshuConstans.ZEI_KBN_CD))
            {
                if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Tab && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fswit"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public bool IchiranSwitchCdName(CellEventArgs e, HinmeiHoshuConstans.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case HinmeiHoshuConstans.FocusSwitch.IN:
                    // 単位名称にフォーカス時実行
                    if (e.CellName.Equals(HinmeiHoshuConstans.UNIT_NAME_RYAKU))
                    {
                        this.form.Ichiran[e.RowIndex, Const.HinmeiHoshuConstans.UNIT_CD].Visible = true;
                        this.form.Ichiran[e.RowIndex, Const.HinmeiHoshuConstans.UNIT_CD].UpdateBackColor(false);

                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, HinmeiHoshuConstans.UNIT_CD];
                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case HinmeiHoshuConstans.FocusSwitch.OUT:
                    // 単位CDに検証成功後実行
                    if (e.CellName.Equals(HinmeiHoshuConstans.UNIT_CD))
                    {
                        this.form.Ichiran[e.RowIndex, HinmeiHoshuConstans.UNIT_NAME_RYAKU].Visible = true;
                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.HinmeiHoshuConstans.UNIT_NAME_RYAKU];
                        this.form.Ichiran[e.RowIndex, HinmeiHoshuConstans.UNIT_NAME_RYAKU].UpdateBackColor(false);

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

        #region 検索条件チェック

        /// <summary>
        /// 検索文字列が検索項目に対して不正な文字かのチェックを行う
        /// </summary>
        /// <returns>True：正常　False：不正</returns>
        public bool CheckSearchString()
        {
            // SetSearchStringメソッド中のentity.SetValueで値によってはFormatでシステムエラーになるためチェックを行う。
            // 現在は、数値項目のみエラーが発生するため、該当項目のみチェックを行っている。
            // 汎用的に行うのであればSetValueで扱っている全ての型に対してチェックを行う。

            LogUtility.DebugMethodStart();

            bool retVal = true;

            if (string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName) || string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
            {
                return retVal;
            }

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                /* チェック実行 */
                if (this.form.CONDITION_VALUE.ItemDefinedTypes.ToLower() == DB_TYPE.SMALLINT.ToTypeString())
                {
                    short dummy = 0;
                    retVal = short.TryParse(this.form.CONDITION_VALUE.Text, out dummy);
                }
            }

            LogUtility.DebugMethodEnd(retVal);
            return retVal;
        }

        #endregion
    }
}