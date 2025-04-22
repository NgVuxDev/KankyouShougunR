// $Id: TegatahokanshaHoshuLogic.cs 19015 2014-04-11 04:31:42Z sc.h.hashimoto $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using MasterCommon.Logic;
using MasterCommon.Utility;
using Shougun.Core.Common.BusinessCommon;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using TegatahokanshaHoshu.APP;
using TegatahokanshaHoshu.Dto;
using TegatahokanshaHoshu.Validator;
using Seasar.Framework.Exceptions;
using Seasar.Dao;


namespace TegatahokanshaHoshu.Logic
{
    /// <summary>
    /// 手形保管者画面のビジネスロジック
    /// </summary>
    public class TegatahokanshaHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "TegatahokanshaHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_SHAIN_DATA_SQL = "TegatahokanshaHoshu.Sql.GetIchiranDataSqlShainHoshu.sql";

        private readonly string GET_ICHIRAN_TEGATA_HOKANSHA_DATA_SQL = "TegatahokanshaHoshu.Sql.GetIchiranDataSqlTegatahokanshaHoshu.sql";

        private readonly string GET_TEGATA_HOKANSHA_DATA_SQL = "TegatahokanshaHoshu.Sql.GetTegataHokanshadataSql.sql";

        /// <summary>
        /// 手形保管者画面Form
        /// </summary>
        private TegatahokanshaHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_TEGATA_HOKANSHA[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 手形保管者のDao
        /// </summary>
        private IM_TEGATA_HOKANSHADao daoTegatahokansha;
        
        /// <summary>
        /// 社員のDao
        /// </summary>
        private IM_SHAINDao daoShain;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

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
        public M_TEGATA_HOKANSHA SearchString { get; set; }

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        public TegatahokanshaHoshuDto tegatahokanshaHoshuDto { get; set; }


        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public TegatahokanshaHoshuLogic(TegatahokanshaHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.daoTegatahokansha = DaoInitUtility.GetComponent<IM_TEGATA_HOKANSHADao>();
            this.daoShain = DaoInitUtility.GetComponent<IM_SHAINDao>();
            this.tegatahokanshaHoshuDto = new TegatahokanshaHoshuDto();
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
                if (this.tegatahokanshaHoshuDto.PropertiesShainExistCheck())
                {
                    // 社員マスタの条件で取得
                    this.SearchResult = daoShain.GetIchiranDataSqlFile(this.GET_ICHIRAN_SHAIN_DATA_SQL
                                            , this.tegatahokanshaHoshuDto.ShainSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                }
                else
                {
                    // 手形保管者マスタの条件で取得
                    this.SearchResult = daoTegatahokansha.GetIchiranDataSqlFile(this.GET_ICHIRAN_TEGATA_HOKANSHA_DATA_SQL
                                            , this.tegatahokanshaHoshuDto.TegatahokanshaSearchString
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked);
                }

                this.SearchResultAll = daoTegatahokansha.GetDataBySqlFile(this.GET_TEGATA_HOKANSHA_DATA_SQL, new M_TEGATA_HOKANSHA());

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

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
                LogUtility.DebugMethodStart();

                var entityList = new M_TEGATA_HOKANSHA[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_TEGATA_HOKANSHA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_TEGATA_HOKANSHA>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                if (!isDelete)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        // NOT NULL制約を一時的に解除(新規追加行対策)
                        column.AllowDBNull = true;

                        // TIME_STAMPがなぜか一意制約有のため、解除
                        if (column.ColumnName.Equals(Const.TegatahokanshaHoshuConstans.TIME_STAMP))
                            column.Unique = false;
                    }

                    dt.BeginLoadData();

                    preDt = GetCloneDataTable(dt);

                    // 変更分のみ取得
                    this.form.Ichiran.DataSource = dt.GetChanges();
                }

                List<M_TEGATA_HOKANSHA> addList = new List<M_TEGATA_HOKANSHA>();
                if (this.form.Ichiran.DataSource != null)
                {
                    foreach (DataRow row in ((DataTable)this.form.Ichiran.DataSource).Rows)
                    {
                        M_TEGATA_HOKANSHA ent = new M_TEGATA_HOKANSHA();
                        ent.SHAIN_CD = row[Const.TegatahokanshaHoshuConstans.SHAIN_CD].ToString();
                        ent.TEGATA_HOKANSHA_BIKOU = row[Const.TegatahokanshaHoshuConstans.TEGATA_HOKANSHA_BIKOU].ToString();
                        dataBinderLogic.SetSystemProperty(ent, false);
                        MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), ent);
                        ent.DELETE_FLG = (bool)row[Const.TegatahokanshaHoshuConstans.DELETE_FLG];
                        addList.Add(ent);
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
        /// 社員CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            LogUtility.DebugMethodStart();

            TegatahokanshaHoshuValidator vali = new TegatahokanshaHoshuValidator();
            bool result = vali.ShainCDValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);

            LogUtility.DebugMethodEnd();

            return result;
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        public bool Preview()
        {
            try
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C011", "手形保管者一覧表");

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
                        foreach (M_TEGATA_HOKANSHA tegatahokanshaEntity in this.entitys)
                        {
                            if (!String.IsNullOrEmpty(tegatahokanshaEntity.SHAIN_CD))
                            {
                                M_TEGATA_HOKANSHA m = this.daoTegatahokansha.GetDataByCd(tegatahokanshaEntity.SHAIN_CD);
                                if (m == null)
                                {
                                    this.daoTegatahokansha.Insert(tegatahokanshaEntity);
                                }
                                else
                                {
                                    this.daoTegatahokansha.Update(tegatahokanshaEntity);
                                }
                                UpdateRelationInfo(tegatahokanshaEntity, false);
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
                        foreach (M_TEGATA_HOKANSHA tegatahokanshaEntity in this.entitys)
                        {
                            M_TEGATA_HOKANSHA entity = this.daoTegatahokansha.GetDataByCd(tegatahokanshaEntity.SHAIN_CD);
                            if (entity != null)
                            {
                                this.daoTegatahokansha.Update(tegatahokanshaEntity);
                                UpdateRelationInfo(tegatahokanshaEntity, true);
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
        /// インスタンスが等しいかどうか判定
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

            TegatahokanshaHoshuLogic localLogic = other as TegatahokanshaHoshuLogic;
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
            M_TEGATA_HOKANSHA entityTegatahokansha = new M_TEGATA_HOKANSHA();   // TODO:メインマスタEntity(表示する作成者等があるEntity)
            M_SHAIN entityShain = new M_SHAIN();           				  // TODO:サブマスタEntity

            // TODO : メインマスタのEntityを指定する
            bool isExist = this.EntityExistCheck(entityTegatahokansha, this.form.CONDITION_VALUE.DBFieldsName);

            // TODO : true=メインマスタEntityのSetValue使用、false=サブマスタEntityのSetValue使用
            if (isExist)
            {
                // 検索条件の設定(手形保管者マスタ)
                entityTegatahokansha.SetValue(this.form.CONDITION_VALUE);
            }
            else
            {
                // 検索条件の設定(社員マスタ)
                entityShain.SetValue(this.form.CONDITION_VALUE);
            }
            
            tegatahokanshaHoshuDto.ShainSearchString = entityShain;
            tegatahokanshaHoshuDto.TegatahokanshaSearchString = entityTegatahokansha;
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
        /// 手形保管者保守に関連する情報の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isDelete"></param>
        private void UpdateRelationInfo(M_TEGATA_HOKANSHA entity, bool isDelete)
        {
            if (entity == null
                || string.IsNullOrEmpty(entity.SHAIN_CD))
            {
                return;
            }
        }
    }
}
