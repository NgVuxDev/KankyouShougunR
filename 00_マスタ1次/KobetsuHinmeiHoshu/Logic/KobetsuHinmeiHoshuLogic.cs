// $Id: KobetsuHinmeiHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KobetsuHinmeiHoshu.APP;
using KobetsuHinmeiHoshu.Validator;
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

namespace KobetsuHinmeiHoshu.Logic
{
    /// <summary>
    /// 個別品名保守画面のビジネスロジック
    /// </summary>
    public class KobetsuHinmeiHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "KobetsuHinmeiHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_HINMEI_DATA_SQL = "KobetsuHinmeiHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_HINMEI_DATA_SQL = "KobetsuHinmeiHoshu.Sql.GetHinmeiDataSql.sql";

        /// <summary>
        /// 個別品名保守画面Form
        /// </summary>
        private KobetsuHinmeiHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        public M_KOBETSU_HINMEI[] entitys;

        /// <summary>
        /// 品名読込み用
        /// 表示されている一覧を保持し、登録時に削除
        /// </summary>
        public M_KOBETSU_HINMEI[] deleteEntitys;

        private bool isAllSearch;

        /// <summary>
        /// 個別品名のDao
        /// </summary>
        private IM_KOBETSU_HINMEIDao dao;

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
        /// 品名のDao
        /// </summary>
        private IM_HINMEIDao hinmeiDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        public Cell cell;

        /// <summary>
        /// 前回値保存
        /// </summary>
        internal string previousValue { get; set; }

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
        public M_KOBETSU_HINMEI SearchString { get; set; }

        /// <summary>
        /// コントロールのユーティリティ
        /// </summary>
        public ControlUtility controlUtil = new ControlUtility();

        /// <summary>
        /// 処理対象伝票区分
        /// </summary>
        public Int16 TargetDenpyouKbn { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public KobetsuHinmeiHoshuLogic(KobetsuHinmeiHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_KOBETSU_HINMEIDao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.hinmeiDao = DaoInitUtility.GetComponent<IM_HINMEIDao>();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.daoSysInfo.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            this.SearchResult = null;
            this.SearchResultAll = null;

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

                this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GYOUSHA_CD;
                this.form.GYOUSHA_NAME_RYAKU.Text = Properties.Settings.Default.GYOUSHA_NAME_RYAKU;
                this.form.GENBA_CD.Text = Properties.Settings.Default.GENBA_CD;
                this.form.GENBA_NAME_RYAKU.Text = Properties.Settings.Default.GENBA_NAME_RYAKU;

                this.form.beforGyousaCD = this.form.GYOUSHA_CD.Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                this.form.Ichiran.AllowUserToAddRows = false;

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

                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_HINMEI_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_HINMEI_DATA_SQL, this.SearchString);

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                Properties.Settings.Default.GYOUSHA_NAME_RYAKU = this.form.GYOUSHA_NAME_RYAKU.Text;
                Properties.Settings.Default.GENBA_CD = this.form.GENBA_CD.Text;
                Properties.Settings.Default.GENBA_NAME_RYAKU = this.form.GENBA_NAME_RYAKU.Text;
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

                var entityList = new M_KOBETSU_HINMEI[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_KOBETSU_HINMEI();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KOBETSU_HINMEI>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KobetsuHinmeiHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var hinmeiEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_KOBETSU_HINMEI> addList = new List<M_KOBETSU_HINMEI>();
                foreach (var hinmeiEntity in hinmeiEntityList)
                {
                    hinmeiEntity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    hinmeiEntity.GENBA_CD = this.form.GENBA_CD.Text;
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.KobetsuHinmeiHoshuConstans.HINMEI_CD) && n.Value.ToString().Equals(hinmeiEntity.HINMEI_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.KobetsuHinmeiHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), hinmeiEntity);
                            addList.Add(hinmeiEntity);
                            break;
                        }
                    }
                }

                this.form.Ichiran.DataSource = preDt;

                this.entitys = addList.ToArray();

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
        /// 品名CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                KobetsuHinmeiHoshuValidator vali = new KobetsuHinmeiHoshuValidator();
                bool result = vali.HinmeiCDValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd();

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

                    CSVFileLogicCustom csvLogic = new CSVFileLogicCustom();

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
        public void CancelCondition()
        {
            LogUtility.DebugMethodStart();

            ClearCondition();
            bool catchErr = SetIchiran();

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
                        foreach (M_KOBETSU_HINMEI hinmeiEntity in this.entitys)
                        {
                            M_KOBETSU_HINMEI entity = this.dao.GetDataByCd(new M_KOBETSU_HINMEI() { GYOUSHA_CD = this.form.GYOUSHA_CD.Text, GENBA_CD = this.form.GENBA_CD.Text, HINMEI_CD = hinmeiEntity.HINMEI_CD });
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
                        foreach (M_KOBETSU_HINMEI hinmeiEntity in this.entitys)
                        {
                            M_KOBETSU_HINMEI entity = this.dao.GetDataByCd(new M_KOBETSU_HINMEI() { GYOUSHA_CD = hinmeiEntity.GYOUSHA_CD, GENBA_CD = hinmeiEntity.GENBA_CD, HINMEI_CD = hinmeiEntity.HINMEI_CD });
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

            KobetsuHinmeiHoshuLogic localLogic = other as KobetsuHinmeiHoshuLogic;
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
                string WhereJouken = string.Empty;
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    WhereJouken += "GYOUSHA_CD = '" + this.form.GYOUSHA_CD.Text + "'";
                }
                else
                {
                    WhereJouken += "GYOUSHA_CD = ''";
                }
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    if (WhereJouken != "")
                    {
                        WhereJouken += " AND ";
                    }
                    WhereJouken += "GENBA_CD = '" + this.form.GENBA_CD.Text + "'";
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
                if (dr == null)
                {
                    return false;
                }

                DataTable table = this.SearchResult.Clone();
                foreach (DataRow row in dr)
                {
                    table.ImportRow(row);
                }
                table.AcceptChanges();
                this.SearchResult = table;

                table.BeginLoadData();
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                    table.Columns[i].AllowDBNull = true;
                }
                this.form.Ichiran.DataSource = null;//リロード
                this.form.Ichiran.DataSource = table;

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M662", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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

            this.form.GYOUSHA_CD.Enter += new EventHandler(this.form.GYOUSHA_CD_Enter);
            this.form.GYOUSHA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.GYOUSHA_CD_Validating);
            this.form.GENBA_CD.Enter += new EventHandler(this.form.GENBA_CD_Enter);
            this.form.GENBA_CD.Validating += new System.ComponentModel.CancelEventHandler(this.form.GENBA_CD_Validating);
            this.form.Ichiran.CellEnter += new EventHandler<CellEventArgs>(this.form.Ichiran_CellEnter);
            this.form.Ichiran.CellValidating += new EventHandler<CellValidatingEventArgs>(this.form.Ichiran_CellValidating);
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
            M_KOBETSU_HINMEI entity = new M_KOBETSU_HINMEI();
            entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
            entity.GENBA_CD = this.form.GENBA_CD.Text;

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
            this.form.GYOUSHA_CD.Text = string.Empty;
            this.form.GENBA_CD.Text = string.Empty;
            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }

        /// <summary>
        /// 業者名称情報の取得
        /// </summary>
        [Transaction]
        public virtual bool SearchGyoushaName()
        {
            try
            {
                bool ret = true;
                LogUtility.DebugMethodStart();

                M_GYOUSHA gyousha = gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text.PadLeft(this.form.GYOUSHA_CD.MaxLength, '0'));

                if (gyousha != null && !gyousha.DELETE_FLG.IsTrue)
                {
                    this.form.GYOUSHA_NAME_RYAKU.Text = gyousha.GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "業者");
                    ret = false;
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGyoushaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGyoushaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 業者チェック
        /// </summary>
        [Transaction]
        public bool CheckGyousha(string cd)
        {
            bool ret = true;
            LogUtility.DebugMethodStart(cd);
            if (!string.IsNullOrEmpty(cd))
            {
                cd = cd.PadLeft(6, '0');
            }
            M_GYOUSHA gyousha = gyoushaDao.GetDataByCd(cd);
            if (gyousha == null || gyousha.DELETE_FLG.IsTrue)
            {
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 現場名称情報の取得
        /// </summary>
        [Transaction]
        public virtual bool SearchGenbaName()
        {
            try
            {
                bool ret = true;
                LogUtility.DebugMethodStart();
                M_GENBA con = new M_GENBA();
                if (!string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    con.GYOUSHA_CD = this.form.GYOUSHA_CD.Text.PadLeft(this.form.GYOUSHA_CD.MaxLength, '0');
                }
                if (!string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    con.GENBA_CD = this.form.GENBA_CD.Text.PadLeft(this.form.GENBA_CD.MaxLength, '0');
                }
                M_GENBA genba = genbaDao.GetDataByCd(con);

                if (genba != null && !genba.DELETE_FLG.IsTrue)
                {
                    this.form.GENBA_CD.Text = genba.GENBA_CD;
                    this.form.GENBA_NAME_RYAKU.Text = genba.GENBA_NAME_RYAKU;
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "現場");
                    ret = false;
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchGenbaName", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchGenbaName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// 現場チェック
        /// </summary>
        [Transaction]
        public bool CheckGenba(string gyoushaCd, string genbaCd)
        {
            bool ret = true;
            LogUtility.DebugMethodStart(gyoushaCd, genbaCd);
            M_GENBA con = new M_GENBA();
            if (!string.IsNullOrEmpty(gyoushaCd))
            {
                con.GYOUSHA_CD = gyoushaCd.PadLeft(6, '0');
            }
            if (!string.IsNullOrEmpty(genbaCd))
            {
                con.GENBA_CD = genbaCd.PadLeft(6, '0');
            }
            M_GENBA genba = genbaDao.GetDataByCd(con);
            if (genba == null || genba.DELETE_FLG.IsTrue)
            {
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }

        /// <summary>
        /// 品名取得
        /// </summary>
        [Transaction]
        public virtual bool SearchHinmei(CellValidatingEventArgs e)
        {
            try
            {
                bool ret = true;
                LogUtility.DebugMethodStart();
                string hinmeiCd = Convert.ToString(this.form.Ichiran.Rows[e.RowIndex].Cells[e.CellIndex].Value);
                if (string.IsNullOrEmpty(hinmeiCd))
                {
                    this.form.Ichiran.Rows[e.RowIndex].Cells[Const.KobetsuHinmeiHoshuConstans.HINMEI_NAME_RYAKU].Value = "";
                    return ret;
                }
                M_HINMEI hinmei = hinmeiDao.GetDataByCd(hinmeiCd);

                if (hinmei != null && !hinmei.DELETE_FLG.IsTrue)
                {
                    this.form.Ichiran.Rows[e.RowIndex].Cells[Const.KobetsuHinmeiHoshuConstans.HINMEI_NAME_RYAKU].Value = hinmei.HINMEI_NAME_RYAKU;
                }
                else
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "品名");
                    ret = false;
                    e.Cancel = true;
                }

                LogUtility.DebugMethodEnd();
                return ret;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("SearchHinmei", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                e.Cancel = true;
                LogUtility.Error("SearchHinmei", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
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
                LogUtility.DebugMethodEnd();
                return true;
            }
        }
    }
}