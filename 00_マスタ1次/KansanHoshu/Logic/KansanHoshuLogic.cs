// $Id: KansanHoshuLogic.cs 37791 2014-12-19 08:22:08Z fangjk@oec-h.com $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KansanHoshu.APP;
using KansanHoshu.Const;
using KansanHoshu.Dto;
using KansanHoshu.MultiRowTemplate;
using KansanHoshu.Validator;
using MasterCommon.Logic;
using MasterCommon.Utility;
using Shougun.Core.Common.BusinessCommon;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Seasar.Framework.Exceptions;
using Seasar.Dao;

namespace KansanHoshu.Logic
{
    /// <summary>
    /// 換算値保守画面のビジネスロジック
    /// </summary>
    public class KansanHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "KansanHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_KANSAN_DATA_SQL = "KansanHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_ICHIRAN_HINMEI_DATA_SQL = "KansanHoshu.Sql.GetIchiranHinmeiDataSql.sql";

        private readonly string GET_KANSAN_DATA_SQL = "KansanHoshu.Sql.GetKansanDataSql.sql";

        private readonly string GET_UNIT_NAME_RYAKU = "KansanHoshu.Sql.GetUniteNameSql.sql";

        private readonly string UNIT_NAME_RYAKU = "UNIT_NAME_RYAKU";

        private readonly string GET_DENPYOU_KBN_DATA_SQL = "KansanHoshu.Sql.GetDenPyouKbndataSql.sql";

        private readonly string GET_HINMEI_DATA_SQL = "KansanHoshu.Sql.GetHinmeiDataSql.sql";

        private readonly string UPDATE_KANSAN_DATA_SQL = "KansanHoshu.Sql.UpdateKansanDataSql.sql";

        /// <summary>
        /// 換算値保守画面Form
        /// </summary>
        private KansanHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        public DataDto[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 換算値のDao
        /// </summary>
        private IM_KANSANDao dao;

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
        private IM_UNITDao unitDao;

        /// <summary>
        /// 単位情報のEntity
        /// </summary>
        private M_UNIT unitInfo;

        /// <summary>
        /// 種類マスタのDao
        /// </summary>
        private IM_SHURUIDao ShuruiDao;

        /// <summary>
        /// 品名マスタのDao
        /// </summary>
        private IM_HINMEIDao HinmeiDao;

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        public KansanHoshuDto kansanHoshuDto { get; set; }

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

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
        /// 検索結果(基本単位)
        /// </summary>
        public DataTable SearchUniteName { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_KANSAN SearchString { get; set; }

        /// <summary>
        /// 検索結果(種類)
        /// </summary>
        public DataTable SearchResultShurui { get; set; }

        /// <summary>
        /// 処理対象伝票区分
        /// </summary>
        public Int16 TargetDenpyouKbn { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public KansanHoshuLogic(KansanHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_KANSANDao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.ShuruiDao = DaoInitUtility.GetComponent<IM_SHURUIDao>();
            this.HinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.kansanHoshuDto = new KansanHoshuDto();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            //システム設定より単位情報の取得
            int unitCd = this.entitySysInfo.KANSAN_UNIT_CD.Value;
            M_UNIT unitMasterInfo = unitDao.GetDataByCd(unitCd);
            this.unitInfo = null;
            if (unitMasterInfo != null)
            {
                this.unitInfo = unitMasterInfo;
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

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                //種類の初期化処理
                this.form.SHURUI_CD.Text = Properties.Settings.Default.ShuruiItem_Text;

                //単位区分の初期化処理
                this.SearchKihonUnitCd();

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
                if (this.kansanHoshuDto.PropertiesHaikiShuruiExistCheck())
                {
                    // 品名マスタの条件で検索
                    this.SearchResult = HinmeiDao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HINMEI_DATA_SQL
                                                                , this.kansanHoshuDto.HinmeiSearchString
                                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else
                {
                    // 換算値マスタの条件で検索
                    this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_KANSAN_DATA_SQL
                                                                , this.kansanHoshuDto.KansanSearchString
                                                                , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                M_KANSAN searchParams = new M_KANSAN();
                searchParams.DENPYOU_KBN_CD = this.kansanHoshuDto.KansanSearchString.DENPYOU_KBN_CD;
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_KANSAN_DATA_SQL, searchParams);
                DataRow[] dr;
                if (string.IsNullOrWhiteSpace(this.form.SHURUI_CD.Text))
                {
                    dr = this.SearchResult.Select(string.Empty, "HINMEI_CD, UNIT_CD");
                }
                else
                {
                    dr = this.SearchResult.Select(String.Format("SHURUI_CD = '{0}'", this.form.SHURUI_CD.Text), "HINMEI_CD, UNIT_CD");
                }
                if (dr != null)
                {
                    DataTable tbl = this.SearchResultAll.Clone();
                    foreach (DataRow row in dr)
                    {
                        tbl.ImportRow(row);
                    }
                    tbl.AcceptChanges();
                    this.SearchResult = tbl;
                }

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ShuruiItem_Text = this.form.SHURUI_CD.Text;

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
                LogUtility.DebugMethodStart(isDelete);

                var focus = (this.form.TopLevelControl as Form).ActiveControl;
                this.form.Ichiran.Focus();

                var parentForm = (MasterBaseForm)this.form.Parent;

                var entityList = new M_KANSAN[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_KANSAN();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KANSAN>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KansanHoshuConstans.TIME_STAMP))
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
                        if (row[Const.KansanHoshuConstans.DELETE_FLG] != DBNull.Value)
                        {
                            delFlg = (bool)row[Const.KansanHoshuConstans.DELETE_FLG];
                        }
                        if (delFlg == isDelete)
                        {
                            DataDto data = new DataDto();

                            if (parentForm.bt_process1.Text == "[1] 売上")
                            {
                                data.entity.DENPYOU_KBN_CD = 2;
                            }
                            else
                            {
                                data.entity.DENPYOU_KBN_CD = 1;
                            }
                            data.entity.HINMEI_CD = string.Empty;
                            if (row[Const.KansanHoshuConstans.HINMEI_CD] != DBNull.Value)
                            {
                                data.entity.HINMEI_CD = (string)row[Const.KansanHoshuConstans.HINMEI_CD];
                            }
                            if (row[Const.KansanHoshuConstans.UNIT_CD] != DBNull.Value)
                            {
                                data.entity.UNIT_CD = (Int16)row[Const.KansanHoshuConstans.UNIT_CD];
                            }
                            if (row[Const.KansanHoshuConstans.KANSANSHIKI] != DBNull.Value)
                            {
                                data.entity.KANSANSHIKI = (Int16)row[Const.KansanHoshuConstans.KANSANSHIKI];
                            }
                            if (row[Const.KansanHoshuConstans.KANSANCHI] != DBNull.Value)
                            {
                                data.entity.KANSANCHI = Convert.ToDecimal(row[Const.KansanHoshuConstans.KANSANCHI]);
                            }
                            data.entity.KANSAN_BIKOU = string.Empty;
                            if (row[Const.KansanHoshuConstans.KANSAN_BIKOU] != DBNull.Value)
                            {
                                data.entity.KANSAN_BIKOU = (string)row[Const.KansanHoshuConstans.KANSAN_BIKOU];
                            }
                            data.entity.DELETE_FLG = isDelete;

                            dataBinderLogic.SetSystemProperty(data.entity, isDelete);
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), data.entity);

                            if (row[Const.KansanHoshuConstans.UK_DENPYOU_KBN_CD] != DBNull.Value)
                            {
                                data.updateKey.DENPYOU_KBN_CD = (Int16)row[Const.KansanHoshuConstans.UK_DENPYOU_KBN_CD];
                            }
                            if (row[Const.KansanHoshuConstans.UK_HINMEI_CD] != DBNull.Value)
                            {
                                data.updateKey.HINMEI_CD = (string)row[Const.KansanHoshuConstans.UK_HINMEI_CD];
                            }
                            if (row[Const.KansanHoshuConstans.UK_UNIT_CD] != DBNull.Value)
                            {
                                data.updateKey.UNIT_CD = (Int16)row[Const.KansanHoshuConstans.UK_UNIT_CD];
                            }

                            addList.Add(data);
                        }
                    }
                }

                this.form.Ichiran.DataSource = preDt;

                this.entitys = addList.ToArray();

                if (focus is Control && !focus.IsDisposed)
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
        public void Cancel()
        {
            LogUtility.DebugMethodStart();

            ClearCondition();
            SetSearchString();

            LogUtility.DebugMethodEnd();
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
                    if (column.ColumnName.Equals(Const.KansanHoshuConstans.TIME_STAMP))
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

                KansanHoshuValidator vali = new KansanHoshuValidator();
                bool result = vali.KansanCDValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd(result);
                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        public void Preview()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            msgLogic.MessageBoxShow("C011", "換算値一覧表");

            MessageBox.Show("未実装");

            LogUtility.DebugMethodEnd();
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
                    #endregion
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
                        foreach (DataDto data in this.entitys)
                        {
                            M_KANSAN entity = this.dao.GetDataByCd(data.updateKey);
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
                                this.dao.UpdateBySqlFile(this.UPDATE_KANSAN_DATA_SQL, data.entity, data.updateKey);
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
                        foreach (DataDto data in this.entitys)
                        {
                            M_KANSAN entity = this.dao.GetDataByCd(data.updateKey);
                            if (entity != null)
                            {
                                this.dao.UpdateBySqlFile(this.UPDATE_KANSAN_DATA_SQL, data.entity, data.updateKey);
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

            KansanHoshuLogic localLogic = other as KansanHoshuLogic;
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
        /// 品名検索
        /// </summary>
        /// <param name="e"></param>
        public virtual bool SearchHinmei(CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(e);

                if (e.FormattedValue == null || string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                {
                    this.form.Ichiran[e.RowIndex, Const.KansanHoshuConstans.HINMEI_NAME_RYAKU].Value = string.Empty;
                    LogUtility.DebugMethodEnd(e);
                    return false;
                }

                int padLen = int.Parse(((GcCustomAlphaNumTextBoxCell)((KansanHoshuDetail)this.form.Ichiran.Template).Row[Const.KansanHoshuConstans.HINMEI_CD]).CharactersNumber.ToString());
                M_HINMEI cond = new M_HINMEI();
                cond.HINMEI_CD = e.FormattedValue.ToString().PadLeft(padLen, '0');
                cond.DENPYOU_KBN_CD = this.TargetDenpyouKbn;
                string shurui = this.form.SHURUI_CD.Text;
                if (!string.IsNullOrWhiteSpace(shurui))
                {
                    cond.SHURUI_CD = shurui;
                }
                DataTable hin = this.HinmeiDao.GetDataBySqlFile(this.GET_HINMEI_DATA_SQL, cond);
                if (hin != null && hin.Rows.Count > 0)
                {
                    this.form.Ichiran[e.RowIndex, Const.KansanHoshuConstans.SHURUI_CD_TEMP].Value = hin.Rows[0][Const.KansanHoshuConstans.SHURUI_CD].ToString();
                    this.form.Ichiran[e.RowIndex, Const.KansanHoshuConstans.HINMEI_NAME_RYAKU].Value = hin.Rows[0][Const.KansanHoshuConstans.HINMEI_NAME_RYAKU].ToString();
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "品名");
                    e.Cancel = true;
                    ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                }

                LogUtility.DebugMethodEnd(e);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchHinmei", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(e);
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
                DataTable table = this.SearchResult;

                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                    table.Columns[i].AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (table.Columns[i].ColumnName.Equals(Const.KansanHoshuConstans.TIME_STAMP))
                    {
                        table.Columns[i].Unique = false;
                    }
                }

                this.form.Ichiran.DataSource = table;

                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);

                this.SearchKansanShiki();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
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
                    switch (this.entitySysInfo.KANSAN_DEFAULT.Value.ToString())
                    {
                        case "1":
                            parentForm.bt_process1.Text = "[1] 売上";
                            break;
                        case "2":
                            parentForm.bt_process1.Text = "換算値入力（支払）";
                            break;

                        default:
                            break;
                    }
                }

                if (parentForm.bt_process1.Text == "[1] 売上")
                {
                    titleControl.Text = "換算値入力（売上）";
                    parentForm.bt_process1.Text = "[1] 支払";
                    parentForm.txb_process.Text = "1";
                    this.TargetDenpyouKbn = 1;
                }
                else
                {
                    titleControl.Text = "換算値入力（支払）";
                    parentForm.bt_process1.Text = "[1] 売上";
                    parentForm.txb_process.Text = "2";
                    this.TargetDenpyouKbn = 2;
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
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            var parentForm = (MasterBaseForm)this.form.Parent;

            //削除ボタン(F4)イベント生成
            this.form.C_Regist(parentForm.bt_func4);
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
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

            //取消ボタン(F11)イベント生成
            parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);

            //閉じるボタン(F12)イベント生成
            parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);

            //売上/支払  
            parentForm.bt_process1.Click += new EventHandler(this.form.Change);

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
            M_KANSAN entity = new M_KANSAN();
            M_HINMEI entityHinmei = new M_HINMEI();
            int intVal;

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName)
                 && !this.form.CONDITION_VALUE.DBFieldsName.Equals(KansanHoshuConstans.BUSHO_NAME_RYAKU))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    bool isExist = this.EntityExistCheck(entity, this.form.CONDITION_VALUE.DBFieldsName);

                    if (isExist)
                    {
                        // 検索条件の設定(換算値マスタ)
                        if (this.form.CONDITION_VALUE.DBFieldsName == "UNIT_CD" && int.TryParse(this.form.CONDITION_VALUE.Text, out intVal))
                        {
                            entity.UNIT_CD = Convert.ToInt16(this.form.CONDITION_VALUE.Text);
                        }
                        else
                        {
                            entity.SetValue(this.form.CONDITION_VALUE);
                        }
                    }
                    else
                    {
                        // 検索条件の設定(品名マスタ)
                        entityHinmei.SetValue(this.form.CONDITION_VALUE);
                    }
                }
            }

            if (parentForm.bt_process1.Text == "[1] 売上")
            {
                entity.DENPYOU_KBN_CD = 2;
                entityHinmei.DENPYOU_KBN_CD = 2;
            }
            else
            {
                entity.DENPYOU_KBN_CD = 1;
                entityHinmei.DENPYOU_KBN_CD = 1;
            }

            // 検索条件の設定
            kansanHoshuDto.HinmeiSearchString = entityHinmei;
            kansanHoshuDto.KansanSearchString = entity;
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
            this.form.SHURUI_CD.Text = string.Empty;
            this.form.SHURUI_NAME_RYAKU.Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

            this.form.Ichiran.DataSource = null;
            this.form.Ichiran.RowCount = 1;
            SearchResult = null;
            SearchResultAll = null;
            SearchString = null;
        }

        /// <summary>
        /// 種類略称名の取得
        /// </summary>
        [Transaction]
        public virtual bool SearchDenPyouKbnName()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultShurui = ShuruiDao.GetDataBySqlFile(this.GET_DENPYOU_KBN_DATA_SQL, new M_SHURUI());

                if (this.SearchResultShurui.Rows != null)
                {
                    this.SetDenPyouKbnName();
                }

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchDenPyouKbnName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        /// <summary>
        /// 種類略称名の設定
        /// </summary>
        private void SetDenPyouKbnName()
        {
            if (this.SearchResultShurui.Rows.Count == 0)
            {
                this.form.SHURUI_NAME_RYAKU.Text = string.Empty;
                return;
            }

            foreach (DataRow row in this.SearchResultShurui.Rows)
            {
                this.form.SHURUI_NAME_RYAKU.Text = string.Empty;

                if (this.form.SHURUI_CD.Text == row["SHURUI_CD"].ToString())
                {
                    this.form.SHURUI_NAME_RYAKU.Text = row["SHURUI_NAME_RYAKU"].ToString();
                    break;
                }
            }
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
                    Cell cell = row.Cells[Const.KansanHoshuConstans.KANSANSHIKI];
                    if (cell.Value == null || string.IsNullOrEmpty(Convert.ToString(cell.Value)))
                    {
                        continue;
                    }


                    if (row.Cells[Const.KansanHoshuConstans.KANSANSHIKI].Value.ToString().Equals(Const.KansanHoshuConstans.KANSANCHI_0))
                    {
                        row.Cells[Const.KansanHoshuConstans.KANSANCHI_SHOW].Value = Const.KansanHoshuConstans.KANSANCHI_0_shown;
                    }
                    else if (row.Cells[Const.KansanHoshuConstans.KANSANSHIKI].Value.ToString().Equals(Const.KansanHoshuConstans.KANSANCHI_1))
                    {
                        row.Cells[Const.KansanHoshuConstans.KANSANCHI_SHOW].Value = Const.KansanHoshuConstans.KANSANCHI_1_shown;
                    }
                    else
                    {
                        row.Cells[Const.KansanHoshuConstans.KANSANSHIKI].Value = DBNull.Value;
                        row.Cells[Const.KansanHoshuConstans.KANSANCHI_SHOW].Value = "";
                    }

                    row.Cells[Const.KansanHoshuConstans.KANSANCHI_SHOW].Visible = true;
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

            this.SearchUniteName = unitDao.GetIchiranDataSqlFile(this.GET_UNIT_NAME_RYAKU
                                                        , new M_UNIT()
                                                        , false);

            if (SearchUniteName.Rows.Count > 0)
            {
                this.form.KIHON_UNIT_CD.Text = SearchUniteName.Rows[0].Field<String>(UNIT_NAME_RYAKU);
            }



            // システム設定 換算値情報基本単位CDを取得
            M_SYS_INFO[] sysInfodata = this.daoSysInfo.GetAllData();

            if (sysInfodata.Length <= 0)
            {
                return;
            }

            if (!sysInfodata[0].KANSAN_KIHON_UNIT_CD.IsNull)
            {
                // 単位マスタから単位略称名を取得する
                M_UNIT unitdata = unitDao.GetDataByCd((int)sysInfodata[0].KANSAN_KIHON_UNIT_CD.Value);

                if (unitdata != null)
                {
                    // 画面の「基本単位」に単位略称名をセットする
                    this.form.KIHON_UNIT_CD.Text = unitdata.UNIT_NAME_RYAKU;
                }
            }

            // 換算値情報単位CDを取得する
            if (!sysInfodata[0].KANSAN_UNIT_CD.IsNull)
            {
                Const.KansanHoshuConstans.KANSAN_UNIT_CD = sysInfodata[0].KANSAN_UNIT_CD.Value.ToString();
            }

            LogUtility.DebugMethodEnd();
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
        /// 新しい行にシステムデータ初期値設定
        /// </summary>
        internal void settingSysDataDisp(int index)
        {
            this.form.Ichiran[index, "UNIT_CD"].Value = this.entitySysInfo.KANSAN_UNIT_CD.Value;
            this.form.Ichiran[index, "UNIT_NAME_RYAKU"].Value = this.unitInfo.UNIT_NAME_RYAKU;
        }
    }
}
