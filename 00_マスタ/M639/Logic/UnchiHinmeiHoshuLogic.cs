using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
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
using Shougun.Core.Master.UnchiHinmeiHoshu.APP;
using Shougun.Core.Master.UnchiHinmeiHoshu.Dao;
using Shougun.Core.Master.UnchiHinmeiHoshu.Dto;
using Shougun.Core.Master.UnchiHinmeiHoshu.Validator;

namespace Shougun.Core.Master.UnchiHinmeiHoshu.Logic
{
    /// <summary>
    /// 運賃品名入力画面のビジネスロジック
    /// </summary>
    public class UnchiHinmeiHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.UnchiHinmeiHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// 運賃品名入力画面Form
        /// </summary>
        private UnchiHinmeiHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_UNCHIN_HINMEI[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 運賃品名のDao
        /// </summary>
        private DaoCls dao;

        /// <summary>
        /// 単位のDao
        /// </summary>
        private IM_UNITDao unitDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// 単位情報のEntity
        /// </summary>
        private M_UNIT unitInfo;

        /// <summary>
        /// 単位情報のEntity
        /// </summary>
        private M_UNIT dispunitInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

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
        public M_UNCHIN_HINMEI SearchString { get; set; }

        /// <summary>
        /// 検索結果(単位)
        /// </summary>
        public DataTable SearchResultUnit { get; set; }

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        public UnchiHinmeiHoshuDto UnchihinmeiHoshuDto { get; set; }

        public bool isRegist { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public UnchiHinmeiHoshuLogic(UnchiHinmeiHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<DaoCls>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();
            this.UnchihinmeiHoshuDto = new UnchiHinmeiHoshuDto();
            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

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

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
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
        internal void DispReferenceMode()
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
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                // 品名マスタの条件で取得
                this.SearchResult = dao.GetIchiranDataSqlFile(this.UnchihinmeiHoshuDto.HinmeiSearchString
                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.UnchihinmeiHoshuDto.HinmeiSearchString
                                        , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                this.SearchResultAll = dao.GetDataMinColsBySqlFile(new M_UNCHIN_HINMEI());

                // 20151208 SQL検索項目を削減する Start
                //          全件検索したかどうか判断するため、主キーだけ比較するは十分ので、
                //          行比較をValidator用のDataRowUnchiHinmeiCompareに変更するように修正
                //this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);
                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), new DataRowUnchiHinmeiCompare());
                // 20151208 SQL検索項目を削減する End

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                count = this.SearchResult.Rows == null ? 0 : 1;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                count = -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                count = -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd(count);
            }
            return count;
        }

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            var focus = (this.form.TopLevelControl as Form).ActiveControl;
            this.form.Ichiran.Focus();

            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                var entityList = new M_UNCHIN_HINMEI[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_UNCHIN_HINMEI();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_UNCHIN_HINMEI>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.UnchiHinmeiHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var hinmeiEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_UNCHIN_HINMEI> addList = new List<M_UNCHIN_HINMEI>();
                foreach (var hinmeiEntity in hinmeiEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.UnchiHinmeiHoshuConstans.UNCHIN_HINMEI_CD) && n.Value.ToString().Equals(hinmeiEntity.UNCHIN_HINMEI_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.UnchiHinmeiHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            // 修正対象が本当に修正されているかチェックする
                            if (!hinmeiEntity.UNCHIN_HINMEI_CD.Equals(string.Empty))
                            {
                                DataRow[] dr = this.SearchResultAll.Select(string.Format("UNCHIN_HINMEI_CD = '{0}'", hinmeiEntity.UNCHIN_HINMEI_CD));
                                if (dr.Length > 0
                                    && ((bool)dr[0][Const.UnchiHinmeiHoshuConstans.DELETE_FLG]).Equals(hinmeiEntity.DELETE_FLG.Value)
                                    && dr[0][Const.UnchiHinmeiHoshuConstans.UNCHIN_HINMEI_CD].ToString().Equals(hinmeiEntity.UNCHIN_HINMEI_CD)
                                    && dr[0][Const.UnchiHinmeiHoshuConstans.UNCHIN_HINMEI_NAME].ToString().Equals(hinmeiEntity.UNCHIN_HINMEI_NAME)
                                    && dr[0][Const.UnchiHinmeiHoshuConstans.UNCHIN_HINMEI_FURIGANA].ToString().Equals(hinmeiEntity.UNCHIN_HINMEI_FURIGANA)
                                    && (dr[0][Const.UnchiHinmeiHoshuConstans.UNIT_CD] == null ? string.Empty : dr[0][Const.UnchiHinmeiHoshuConstans.UNIT_CD].ToString()) == (hinmeiEntity.UNIT_CD.IsNull ? string.Empty : hinmeiEntity.UNIT_CD.ToString())
                                    && dr[0][Const.UnchiHinmeiHoshuConstans.BIKOU].ToString().Equals(hinmeiEntity.BIKOU))
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                if (focus is Control && !focus.IsDisposed)
                {
                    focus.Focus();
                }
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                ClearCondition();
                SetSearchString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        /// <summary>
        /// 運賃品名CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            LogUtility.DebugMethodStart();

            UnchiHinmeiHoshuValidator vali = new UnchiHinmeiHoshuValidator();
            bool result = vali.HinmeiCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);

            LogUtility.DebugMethodEnd();

            return result;
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        public void Preview()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            msgLogic.MessageBoxShow("C011", "運賃品名入力");

            MessageBox.Show("未実装");

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// CSV
        /// </summary>
        public void CSV()
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
            }

            LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 条件取消
        /// </summary>
        public void CancelCondition()
        {
            LogUtility.DebugMethodStart();

            ClearCondition();
            SetSearchString();

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

                this.isRegist = true;
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_UNCHIN_HINMEI hinmeiEntity in this.entitys)
                        {
                            M_UNCHIN_HINMEI entity = this.dao.GetDataByCd(hinmeiEntity.UNCHIN_HINMEI_CD);
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
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("Regist", ex1);
                this.form.errmessage.MessageBoxShow("E080", ""); ;
                this.isRegist = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                this.isRegist = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                this.isRegist = false;
            }
            finally
            {
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

                this.isRegist = true;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C021");
                if (result == DialogResult.Yes)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_UNCHIN_HINMEI hinmeiEntity in this.entitys)
                        {
                            M_UNCHIN_HINMEI entity = this.dao.GetDataByCd(hinmeiEntity.UNCHIN_HINMEI_CD);
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
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", "");
                this.isRegist = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                this.isRegist = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                this.isRegist = false;
            }
            finally
            {
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

            UnchiHinmeiHoshuLogic localLogic = other as UnchiHinmeiHoshuLogic;
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
        public void SetIchiran()
        {
            var table = this.SearchResult;

            table.BeginLoadData();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }

            this.form.Ichiran.DataSource = table;

            // 権限チェック
            if (r_framework.Authority.Manager.CheckAuthority("M639", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
            {
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
            }
            else
            {
                this.DispReferenceMode();
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
            M_UNCHIN_HINMEI entityUnchiHinmei = new M_UNCHIN_HINMEI();
            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // 単位が条件の時はCDではなく略名称に変更
                    if (this.form.CONDITION_VALUE.DBFieldsName == "UNIT_CD")
                    {
                        this.form.CONDITION_VALUE.DBFieldsName = "UNIT_NAME";
                        this.form.CONDITION_VALUE.ItemDefinedTypes = "varchar";
                    }

                    bool isExistHinmei = this.EntityExistCheck(entityUnchiHinmei, this.form.CONDITION_VALUE.DBFieldsName);

                    if (isExistHinmei)
                    {
                        // 検索条件の設定(品名マスタ)
                        entityUnchiHinmei.SetValue(this.form.CONDITION_VALUE);
                    }
                }
            }
            UnchihinmeiHoshuDto.HinmeiSearchString = entityUnchiHinmei;
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
        internal void settingSysDataDisp(int index)
        {
            //システム初期値の単位が必須で無いためNullチェックが必要
            if (!this.entitySysInfo.HINMEI_UNIT_CD.IsNull)
            {
                this.form.Ichiran[index, "UNIT_CD"].Value = this.entitySysInfo.HINMEI_UNIT_CD.Value;
                this.form.Ichiran[index, "UNIT_NAME_RYAKU"].Value = this.dispunitInfo.UNIT_NAME_RYAKU;
            }
        }

        /// <summary>
        /// 一覧バリデーション処理
        /// </summary>
        /// <param name="e"></param>
        public bool IchiranValidating(object sender, CellValidatingEventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                if (e.CellName.Equals(Const.UnchiHinmeiHoshuConstans.UNCHIN_HINMEI_CD))
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
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fswit"></param>
        /// <returns></returns>
        public bool IchiranCellSwitchCdName(CellEventArgs e, Const.UnchiHinmeiHoshuConstans.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case Const.UnchiHinmeiHoshuConstans.FocusSwitch.IN:
                    // 単位名称にフォーカス時実行
                    if (e.CellName.Equals(Const.UnchiHinmeiHoshuConstans.UNIT_NAME_RYAKU))
                    {
                        this.form.Ichiran[e.RowIndex, Const.UnchiHinmeiHoshuConstans.UNIT_CD].Visible = true;
                        this.form.Ichiran[e.RowIndex, Const.UnchiHinmeiHoshuConstans.UNIT_CD].UpdateBackColor(false);

                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.UnchiHinmeiHoshuConstans.UNIT_CD];
                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case Const.UnchiHinmeiHoshuConstans.FocusSwitch.OUT:
                    // 単位CDに検証成功後実行
                    if (e.CellName.Equals(Const.UnchiHinmeiHoshuConstans.UNIT_CD))
                    {
                        this.form.Ichiran[e.RowIndex, Const.UnchiHinmeiHoshuConstans.UNIT_NAME_RYAKU].Visible = true;
                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.UnchiHinmeiHoshuConstans.UNIT_NAME_RYAKU];
                        this.form.Ichiran[e.RowIndex, Const.UnchiHinmeiHoshuConstans.UNIT_NAME_RYAKU].UpdateBackColor(false);

                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        /// <summary>
        /// 削除」をつけた運賃品名が運賃単価マスタに存在する場合
        /// </summary>
        /// <returns></returns>
        internal bool UnchinHinmeiCdCheck()
        {
            bool bFlag = false;

            var Cd = new StringBuilder();

            int j = 0;
            for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
            {
                if (bool.Parse(this.form.Ichiran.Rows[i]["DELETE_FLG"].Value.ToString()))
                {
                    if (j != 0)
                    {
                        Cd.Append(",");
                    }
                    Cd.Append("'" + this.form.Ichiran.Rows[i]["UNCHIN_HINMEI_CD"].Value.ToString() + "'");
                    j++;
                }
            }
            if (Cd.Length != 0)
            {
                var sql = new StringBuilder();

                sql.Append("SELECT UNCHIN_HINMEI_CD ");
                sql.Append("FROM M_UNCHIN_TANKA  ");
                sql.AppendFormat("WHERE UNCHIN_HINMEI_CD IN ({0}) ", Cd.ToString());
                sql.AppendFormat(" AND DELETE_FLG = 0");
                var strSql = sql.ToString();
                var table = this.dao.GetDateForStringSql(strSql);

                if (table.Rows.Count > 0)
                {
                    bFlag = true;
                }
            }
            return bFlag;
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
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            bool ret = true;
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllKeys().Select(s => s.UNCHIN_HINMEI_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["UNCHIN_HINMEI_CD"]).Where(c => c.Value != null).ToList().
                    ForEach(c =>
                    {
                        c.ReadOnly = allEntityList.Contains(c.Value.ToString());
                        c.UpdateBackColor(false);   // MultiRowの場合、ここで背景色をセットする。
                    });
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("EditableToPrimaryKey", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
    }
}