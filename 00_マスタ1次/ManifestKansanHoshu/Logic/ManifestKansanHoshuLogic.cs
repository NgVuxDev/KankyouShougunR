// $Id: ManifestKansanHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using ManifestKansanHoshu.APP;
using ManifestKansanHoshu.Dto;
using ManifestKansanHoshu.Validator;
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

namespace ManifestKansanHoshu.Logic
{
    /// <summary>
    /// マニフェスト換算保守画面のビジネスロジック
    /// </summary>
    public class ManifestKansanHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "ManifestKansanHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_MANIFEST_KANSAN_DATA_SQL = "ManifestKansanHoshu.Sql.GetIchiranDataSql.sql";
        private readonly string GET_ICHIRAN_MANIFEST_KANSAN_DATA_BY_HAIKINAME_SQL = "ManifestKansanHoshu.Sql.GetIchiranDataByHaikiNameSql.sql";
        private readonly string GET_ICHIRAN_MANIFEST_KANSAN_DATA_BY_NISUGATA_SQL = "ManifestKansanHoshu.Sql.GetIchiranDataByNisugataSql.sql";
        private readonly string GET_UNIT_DATA_SQL = "ManifestKansanHoshu.Sql.GetIchiranDataByUnit.sql";

        private readonly string UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";

        private readonly string GET_MANIFEST_KANSAN_DATA_SQL = "ManifestKansanHoshu.Sql.GetManifestKansanDataSql.sql";

        private readonly string UPDATE_MANIFEST_KANSAN_DATA_SQL = "ManifestKansanHoshu.Sql.UpdateManifestKansanDataSql.sql";

        /// <summary>
        /// マニフェスト換算保守画面Form
        /// </summary>
        private ManifestKansanHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 更新データ用Entity
        /// </summary>
        public DataDto[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// マニフェスト換算のDao
        /// </summary>
        private IM_MANIFEST_KANSANDao dao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        /// <summary>
        /// 単位のDao
        /// </summary>
        private IM_UNITDao unitdao;

        /// <summary>
        /// 単位情報のEntity
        /// </summary>
        private M_UNIT dispunitInfo;

        /// <summary>
        /// 廃棄物名称のDao
        /// </summary>
        private IM_HAIKI_NAMEDao haikiNamedao;

        /// <summary>
        /// 荷姿のDao
        /// </summary>
        private IM_NISUGATADao nisugatadao;

        /// <summary>
        /// 報告書分類Dao
        /// </summary>
        private IM_HOUKOKUSHO_BUNRUIDao houkokushodao;

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
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_MANIFEST_KANSAN SearchString { get; set; }

        /// <summary>
        /// 検索結果(基本単位)
        /// </summary>
        public DataTable SearchUniteName { get; set; }

        /// <summary>
        /// 検索条件（マニフェスト換算以外のマスタ項目）
        /// </summary>
        public ManifestKansanHoshuDto othersSearchString { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public ManifestKansanHoshuLogic(ManifestKansanHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_MANIFEST_KANSANDao>();

            this.unitdao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.haikiNamedao = DaoInitUtility.GetComponent<IM_HAIKI_NAMEDao>();
            this.nisugatadao = DaoInitUtility.GetComponent<IM_NISUGATADao>();
            this.houkokushodao = DaoInitUtility.GetComponent<IM_HOUKOKUSHO_BUNRUIDao>();

            this.othersSearchString = new ManifestKansanHoshuDto();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.unitdao = DaoInitUtility.GetComponent<IM_UNITDao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            //システム設定より表示単位略称の取得
            this.dispunitInfo = null;
            if (false == this.entitySysInfo.MANI_KANSAN_UNIT_CD.IsNull)
            {
                int dispUniCd = this.entitySysInfo.MANI_KANSAN_UNIT_CD.Value;
                M_UNIT dispUnitInfo = unitdao.GetDataByCd(dispUniCd);
                if (dispUnitInfo != null)
                {
                    this.dispunitInfo = dispUnitInfo;
                }
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

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;
                this.form.HOUKOKUSHO_BUNRUI_CD.Text = Properties.Settings.Default.ManiHoukokushoCd_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1979
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                SearchKihonUnitCd();
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

                DataTable SearchResultTemp = new DataTable();

                // プロパティ設定有無チェック
                if (this.othersSearchString.PropertiesUnitExistsCheck())
                {
                    // 単位マスタの条件で取得
                    SearchResultTemp = unitdao.GetIchiranDataSqlFile(this.GET_UNIT_DATA_SQL
                                            , this.othersSearchString.UnitSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else if (this.othersSearchString.PropertiesHaikiNameExistCheck())
                {
                    // 廃棄物名称マスタの条件で取得
                    SearchResultTemp = haikiNamedao.GetIchiranDataSqlFile(this.GET_ICHIRAN_MANIFEST_KANSAN_DATA_BY_HAIKINAME_SQL
                                            , this.othersSearchString.haikiNameSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else if (this.othersSearchString.PropertiesNisugataExistCheck())
                {
                    // 荷姿マスタの条件で取得
                    SearchResultTemp = nisugatadao.GetIchiranDataSqlFile(this.GET_ICHIRAN_MANIFEST_KANSAN_DATA_BY_NISUGATA_SQL
                                            , this.othersSearchString.nisugataSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else
                {
                    //マニフェスト換算マスタの条件で取得
                    SearchResultTemp = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_MANIFEST_KANSAN_DATA_SQL
                                                                , this.othersSearchString.manifestKansanSearchString
                                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }

                // ヘッダ部入力された報告書分類ＣＤと一致するデータ取得
                String selectSql = "HOUKOKUSHO_BUNRUI_CD = '" + this.form.HOUKOKUSHO_BUNRUI_CD.Text + "'";
                DataRow[] tempRows = SearchResultTemp.Select(selectSql);

                this.SearchResult = SearchResultTemp.Clone();
                foreach (DataRow tempR in tempRows)
                {
                    this.SearchResult.ImportRow(tempR);
                }
                this.SearchResult.AcceptChanges();

                M_MANIFEST_KANSAN cond = new M_MANIFEST_KANSAN();
                cond.HOUKOKUSHO_BUNRUI_CD = this.form.HOUKOKUSHO_BUNRUI_CD.Text;
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_MANIFEST_KANSAN_DATA_SQL, cond);

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;
                Properties.Settings.Default.ManiHoukokushoCd_Text = this.form.HOUKOKUSHO_BUNRUI_CD.Text;
                Properties.Settings.Default.ManiHoukokushoName_Text = this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked; ;

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

                var entityList = new M_MANIFEST_KANSAN[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_MANIFEST_KANSAN();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_MANIFEST_KANSAN>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.ManifestKansanHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                // 変更分からエンティティを作成
                List<DataDto> addList = new List<DataDto>();
                if (this.form.Ichiran.DataSource != null)
                {
                    foreach (DataRow row in ((DataTable)this.form.Ichiran.DataSource).Rows)
                    {
                        bool delFlg = false;
                        if (row[Const.ManifestKansanHoshuConstans.DELETE_FLG] != DBNull.Value)
                        {
                            delFlg = (bool)row[Const.ManifestKansanHoshuConstans.DELETE_FLG];
                        }
                        if (delFlg == isDelete)
                        {
                            DataDto data = new DataDto();

                            data.entity.HOUKOKUSHO_BUNRUI_CD = this.form.HOUKOKUSHO_BUNRUI_CD.Text;
                            data.entity.HAIKI_NAME_CD = string.Empty;
                            if (row[Const.ManifestKansanHoshuConstans.HAIKI_NAME_CD] != DBNull.Value)
                            {
                                data.entity.HAIKI_NAME_CD = (string)row[Const.ManifestKansanHoshuConstans.HAIKI_NAME_CD];
                            }
                            if (row[Const.ManifestKansanHoshuConstans.UNIT_CD] != DBNull.Value)
                            {
                                data.entity.UNIT_CD = (Int16)row[Const.ManifestKansanHoshuConstans.UNIT_CD];
                            }
                            data.entity.NISUGATA_CD = string.Empty;
                            if (row[Const.ManifestKansanHoshuConstans.NISUGATA_CD] != DBNull.Value)
                            {
                                data.entity.NISUGATA_CD = (string)row[Const.ManifestKansanHoshuConstans.NISUGATA_CD];
                            }
                            if (row[Const.ManifestKansanHoshuConstans.KANSANSHIKI] != DBNull.Value)
                            {
                                data.entity.KANSANSHIKI = (Int16)row[Const.ManifestKansanHoshuConstans.KANSANSHIKI];
                            }
                            if (row[Const.ManifestKansanHoshuConstans.KANSANCHI] != DBNull.Value)
                            {
                                data.entity.KANSANCHI = Convert.ToDecimal(row[Const.ManifestKansanHoshuConstans.KANSANCHI]);
                            }
                            data.entity.MANIFEST_KANSAN_BIKOU = string.Empty;
                            if (row[Const.ManifestKansanHoshuConstans.MANIFEST_KANSAN_BIKOU] != DBNull.Value)
                            {
                                data.entity.MANIFEST_KANSAN_BIKOU = (string)row[Const.ManifestKansanHoshuConstans.MANIFEST_KANSAN_BIKOU];
                            }
                            data.entity.DELETE_FLG = isDelete;

                            dataBinderLogic.SetSystemProperty(data.entity, isDelete);
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), data.entity);

                            if (row[Const.ManifestKansanHoshuConstans.UK_HOUKOKUSHO_BUNRUI_CD] != DBNull.Value)
                            {
                                data.updateKey.HOUKOKUSHO_BUNRUI_CD = (string)row[Const.ManifestKansanHoshuConstans.UK_HOUKOKUSHO_BUNRUI_CD];
                            }
                            if (row[Const.ManifestKansanHoshuConstans.UK_HAIKI_NAME_CD] != DBNull.Value)
                            {
                                data.updateKey.HAIKI_NAME_CD = (string)row[Const.ManifestKansanHoshuConstans.UK_HAIKI_NAME_CD];
                            }
                            if (row[Const.ManifestKansanHoshuConstans.UK_UNIT_CD] != DBNull.Value)
                            {
                                data.updateKey.UNIT_CD = (Int16)row[Const.ManifestKansanHoshuConstans.UK_UNIT_CD];
                            }
                            if (row[Const.ManifestKansanHoshuConstans.UK_NISUGATA_CD] != DBNull.Value)
                            {
                                data.updateKey.NISUGATA_CD = (string)row[Const.ManifestKansanHoshuConstans.UK_NISUGATA_CD];
                            }

                            addList.Add(data);
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
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1979

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
        /// マニフェスト換算データの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart(entitys);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.ManifestKansanHoshuConstans.TIME_STAMP))
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

                ManifestKansanHoshuValidator vali = new ManifestKansanHoshuValidator();
                bool result = vali.PrimaryCDValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);

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
                msgLogic.MessageBoxShow("C011", "マニフェスト換算一覧表");

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
                //20150415 minhhoang edit #1748
                //do not reload search result when F7 press
                //ClearCondition();
                ClearConditionF7();
                //20150415 minhhoang end edit #1748
                SetSearchString();

                LogUtility.DebugMethodEnd();
                return false;
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
            // 重複エラー時用のメッセージで使用
            string dispHaikiNameCd = string.Empty;
            Int16 dispUnitKbn = -1;
            string dispNisugataCd = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(errorFlag);
                //独自チェックの記述例を書く

                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (DataDto data in this.entitys)
                        {
                            M_MANIFEST_KANSAN entity = this.dao.GetDataByCd(data.updateKey);
                            dispHaikiNameCd = string.Empty;
                            dispUnitKbn = -1;
                            dispNisugataCd = string.Empty;

                            if (entity == null)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (data.entity.DELETE_FLG)
                                {
                                    continue;
                                }
                                this.dao.Insert(data.entity);
                            }
                            else
                            {
                                dispHaikiNameCd = data.entity.HAIKI_NAME_CD;
                                dispUnitKbn = data.entity.UNIT_CD.IsNull ? Convert.ToInt16(-1) : (Int16)data.entity.UNIT_CD;
                                dispNisugataCd = data.entity.NISUGATA_CD;
                                this.dao.UpdateBySqlFile(this.UPDATE_MANIFEST_KANSAN_DATA_SQL, data.entity, data.updateKey);
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

                var tempEx = ex2.Args[0] as SqlException;
                if ((!string.IsNullOrEmpty(dispHaikiNameCd)
                    || dispUnitKbn >= 0
                    || !string.IsNullOrEmpty(dispNisugataCd))
                    && (tempEx != null && tempEx.Number == Constans.SQL_EXCEPTION_NUMBER_DUPLICATE))
                {
                    this.form.errmessage.MessageBoxShow("E259", "換算値または備考", "・廃棄物名称CD：" + dispHaikiNameCd + "、単位区分CD：" + dispUnitKbn.ToString() + "、荷姿CD：" + dispNisugataCd);
                }
                else
                {
                    this.form.errmessage.MessageBoxShow("E093", "");
                }
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
                        foreach (DataDto data in this.entitys)
                        {
                            M_MANIFEST_KANSAN entity = this.dao.GetDataByCd(data.updateKey);
                            if (entity != null)
                            {
                                this.dao.UpdateBySqlFile(this.UPDATE_MANIFEST_KANSAN_DATA_SQL, data.entity, data.updateKey);
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

            ManifestKansanHoshuLogic localLogic = other as ManifestKansanHoshuLogic;
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
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            try
            {
                var table = this.SearchResult;

                if (table != null)
                {
                    table.BeginLoadData();

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[i].ReadOnly = false;
                        table.Columns[i].AllowDBNull = true;

                        // TIME_STAMPがなぜか一意制約有のため、解除
                        if (table.Columns[i].ColumnName.Equals(Const.ManifestKansanHoshuConstans.TIME_STAMP))
                        {
                            table.Columns[i].Unique = false;
                        }
                    }
                }

                this.form.Ichiran.DataSource = table;

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M227", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.DispReferenceMode();
                }

                this.SearchKansanShiki();
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

            //プレビュ機能削除
            ////ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            //parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

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
            M_MANIFEST_KANSAN entity = new M_MANIFEST_KANSAN();
            M_HAIKI_NAME haikiNameentity = new M_HAIKI_NAME();
            M_NISUGATA nisugataentity = new M_NISUGATA();
            M_UNIT unitEntity = new M_UNIT();

            // 検索条件の設定
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

                    bool isExistManifestKansan = this.EntityExistCheck(entity, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistHaikiname = this.EntityExistCheck(haikiNameentity, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistNisugata = this.EntityExistCheck(nisugataentity, this.form.CONDITION_VALUE.DBFieldsName);
                    bool isExistUnit = this.EntityExistCheck(unitEntity, this.form.CONDITION_VALUE.DBFieldsName);

                    if (isExistManifestKansan)
                    {
                        // 検索条件の設定(マニフェスト換算マスタ)
                        entity.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (isExistHaikiname)
                    {
                        // 検索条件の設定(廃棄物名称マスタ)
                        haikiNameentity.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (isExistNisugata)
                    {
                        // 検索条件の設定(荷姿マスタ)
                        nisugataentity.SetValue(this.form.CONDITION_VALUE);
                    }
                    else if (isExistUnit)
                    {
                        // 検索条件の設定(単位マスタ)
                        unitEntity.SetValue(this.form.CONDITION_VALUE);
                    }
                }
            }

            othersSearchString.manifestKansanSearchString = entity;
            othersSearchString.haikiNameSearchString = haikiNameentity;
            othersSearchString.nisugataSearchString = nisugataentity;
            othersSearchString.UnitSearchString = unitEntity;
        }

        /// <summary>
        /// Entity内のプロパティに指定プロパティが存在するかチェック
        /// </summary>
        /// <param name="entity">マニフェスト換算マスタEntity</param>
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
            this.form.HOUKOKUSHO_BUNRUI_CD.Text = string.Empty;
            this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text = string.Empty;
            Properties.Settings.Default.ManiHoukokushoName_Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }

        #region 20150415 minhhoang edit #1748

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearConditionF7()
        {
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;
            Properties.Settings.Default.ManiHoukokushoName_Text = string.Empty;

            this.SetHyoujiJoukenInit();
        }

        #endregion

        /// <summary>
        /// 換算式表示設定
        /// </summary>
        [Transaction]
        public virtual bool SearchKansanShiki()
        {
            try
            {
                LogUtility.DebugMethodStart();

                foreach (Row row in this.form.Ichiran.Rows)
                {
                    Cell cell = row.Cells[Const.ManifestKansanHoshuConstans.KANSANSHIKI];
                    if (cell.Value == null || string.IsNullOrEmpty(cell.Value.ToString()))
                    {
                        continue;
                    }

                    if (row.Cells[Const.ManifestKansanHoshuConstans.KANSANSHIKI].Value.ToString().Equals(Const.ManifestKansanHoshuConstans.KANSANSHIKI_0))
                    {
                        row.Cells[Const.ManifestKansanHoshuConstans.KANSANSHIKI_SHOW].Value = Const.ManifestKansanHoshuConstans.KANSANSHIKI_0_shown;
                    }
                    else if (row.Cells[Const.ManifestKansanHoshuConstans.KANSANSHIKI].Value.ToString().Equals(Const.ManifestKansanHoshuConstans.KANSANSHIKI_1))
                    {
                        row.Cells[Const.ManifestKansanHoshuConstans.KANSANSHIKI_SHOW].Value = Const.ManifestKansanHoshuConstans.KANSANSHIKI_1_shown;
                    }
                    else
                    {
                        row.Cells[Const.ManifestKansanHoshuConstans.KANSANSHIKI].Value = DBNull.Value;
                        row.Cells[Const.ManifestKansanHoshuConstans.KANSANSHIKI_SHOW].Value = "";
                    }

                    row.Cells[Const.ManifestKansanHoshuConstans.KANSANSHIKI_SHOW].Visible = true;
                    row.Cells[Const.ManifestKansanHoshuConstans.KANSANSHIKI_SHOW].Selectable = true;
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchKansanShiki", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        // 基本単位の取得
        public void SearchKihonUnitCd()
        {
            LogUtility.DebugMethodStart();

            // システム設定マニフェスト換算基本単位ＣＤを取得
            M_SYS_INFO[] sysInfodata = this.daoSysInfo.GetAllData();

            if (sysInfodata.Length <= 0)
            {
                return;
            }

            // 報告書分類から略称を取得する
            var houkokusyoBunrui = this.houkokushodao.GetDataByCd(this.form.HOUKOKUSHO_BUNRUI_CD.Text);
            if (houkokusyoBunrui != null)
            {
                // 画面の「報告書分類略称」に報告書分類略称をセットする
                this.form.HOUKOKUSHO_BUNRUI_NAME_RYAKU.Text = houkokusyoBunrui.HOUKOKUSHO_BUNRUI_NAME_RYAKU;
            }

            if (!sysInfodata[0].MANI_KANSAN_KIHON_UNIT_CD.IsNull)
            {
                // 単位マスタから単位略称名を取得する
                M_UNIT unitdata = unitdao.GetDataByCd((int)sysInfodata[0].MANI_KANSAN_KIHON_UNIT_CD.Value);

                if (unitdata != null)
                {
                    // 画面の「基本単位」に単位略称名をセットする
                    this.form.KIHON_UNIT_CD.Text = unitdata.UNIT_NAME_RYAKU;
                }
            }

            // マニフェスト換算値情報単位ＣＤを取得する
            if (!sysInfodata[0].MANI_KANSAN_UNIT_CD.IsNull)
            {
                Const.ManifestKansanHoshuConstans.MANI_KANSAN_UNIT_CD = sysInfodata[0].MANI_KANSAN_UNIT_CD.Value.ToString();
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 新しい行にシステムデータ初期値設定
        /// </summary>
        internal bool settingSysDataDisp(int index)
        {
            try
            {
                if (this.dispunitInfo != null)
                {
                    this.form.Ichiran[index, "UNIT_CD"].Value = this.dispunitInfo.UNIT_CD.Value;
                    this.form.Ichiran[index, "UNIT_NAME"].Value = this.dispunitInfo.UNIT_NAME_RYAKU;
                }
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
        /// 検索条件が数字のみ入力.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.form.CONDITION_VALUE.DBFieldsName.Equals(Const.ManifestKansanHoshuConstans.KANSANCHI))
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
        public bool IchiranSwitchCdName(CellEventArgs e, Const.ManifestKansanHoshuConstans.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case Const.ManifestKansanHoshuConstans.FocusSwitch.IN:
                    // 単位名称にフォーカス時実行
                    if (e.CellName.Equals(Const.ManifestKansanHoshuConstans.UNIT_NAME))
                    {
                        this.form.Ichiran[e.RowIndex, Const.ManifestKansanHoshuConstans.UNIT_CD].Visible = true;
                        this.form.Ichiran[e.RowIndex, Const.ManifestKansanHoshuConstans.UNIT_CD].UpdateBackColor(false);

                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.ManifestKansanHoshuConstans.UNIT_CD];
                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case Const.ManifestKansanHoshuConstans.FocusSwitch.OUT:
                    // 単位CDに検証成功後実行
                    if (e.CellName.Equals(Const.ManifestKansanHoshuConstans.UNIT_CD))
                    {
                        this.form.Ichiran[e.RowIndex, Const.ManifestKansanHoshuConstans.UNIT_NAME].Visible = true;
                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.ManifestKansanHoshuConstans.UNIT_NAME];
                        this.form.Ichiran[e.RowIndex, Const.ManifestKansanHoshuConstans.UNIT_NAME].UpdateBackColor(false);

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
            M_UNIT[] result = this.unitdao.GetAllValidData(unit);

            if (result == null || result.Length == 0)
            {
                Iserr = true;
            }
            else
            {
                this.form.Ichiran.Rows[index].Cells["UNIT_CD"].Value = result[0].UNIT_CD.Value;
                this.form.Ichiran.Rows[index].Cells["UNIT_NAME"].Value = result[0].UNIT_NAME_RYAKU;
            }
            return Iserr;
        }
    }

}