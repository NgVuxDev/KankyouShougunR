using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlTypes;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Master.ContenaHoshu.APP;
using Shougun.Core.Master.ContenaHoshu.Const;
using Shougun.Core.Master.ContenaHoshu.Dao;
using Shougun.Core.Master.ContenaHoshu.Validator;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.ContenaHoshu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    public class ContenaHoshuLogic : IBuisinessLogic
    {

        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.ContenaHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// CDチェック用
        /// </summary>
        private DataTable dtDetailList = new DataTable();

        /// <summary>
        /// Form
        /// </summary>
        private ContenaHoshuForm form;

        /// <summary>
        /// BaseForm
        /// </summary>
        internal MasterBaseForm parentForm;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// コンテナのDao
        /// </summary>
        private IM_CONTENADao imContenaDao;

        /// <summary>
        /// コンテナのDao
        /// </summary>
        private ContenaDao contenaDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao imGenbaDao;

        /// <summary>
        /// 車輌のDao
        /// </summary>
        private IM_SHARYOUDao imSharyouDao;

        /// <summary>
        /// コンテナのエンティティ
        /// </summary>
        private M_CONTENA[] entitys;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// 20141226 Houkakou 「コンテナ入力」のダブルクリックを追加する　start
        /// <summary>
        /// ダブルクリック
        /// </summary>
        private Control oldDetailEditingControl;

        /// <summary>
        /// ダブルクリックのCell
        /// </summary>
        private Cell cell;
        /// 20141226 Houkakou 「コンテナ入力」のダブルクリックを追加する　end

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
        public DTOClass SearchString { get; set; }

        public bool isRegist = true;

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ContenaHoshuLogic(ContenaHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.contenaDao = DaoInitUtility.GetComponent<ContenaDao>();
            this.imContenaDao = DaoInitUtility.GetComponent<IM_CONTENADao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.imGenbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();
            this.imSharyouDao = DaoInitUtility.GetComponent<IM_SHARYOUDao>();

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 初期化処理

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                // システム情報を取得し、初期値をセットする
                //GetSysInfoInit();

                // 処理No制御
                // 親フォームオブジェクト取得
                parentForm = (MasterBaseForm)this.form.Parent;
                this.parentForm.txb_process.Enabled = false;

                this.allControl = this.form.allControl;

                this.form.txt_ConditionValue.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.txt_ConditionValue.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.txt_ConditionValue.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.txt_ConditionItem.Text = Properties.Settings.Default.ConditionItem_Text;

                this.form.chk_Sakujo.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.chk_Sakujo.Checked)
                {
                    // システム情報を取得し、初期値をセットする
                    if (!this.GetSysInfoInit())
                    {
                        ret = false;
                        return ret;
                    }
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M205", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
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

        #endregion

        #region ボタン初期化処理
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
        #endregion

        #region イベントの初期化処理
        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (MasterBaseForm)this.form.Parent;

            // 削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            //parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ClearCondition);

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

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }
        #endregion

        #region 参照モード表示

        /// <summary>
        /// 参照モード表示に変更します
        /// </summary>
        private void DispReferenceMode()
        {
            // MainForm
            this.form.mlt_gcCustomMultiRow.ReadOnly = true;
            this.form.mlt_gcCustomMultiRow.AllowUserToAddRows = false;

            // FunctionButton
            var parentForm = (MasterBaseForm)this.form.Parent;
            parentForm.bt_func4.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func9.Enabled = false;
        }

        #endregion

        #endregion

        #region 削除処理

        #region 削除チェック
        /// <summary>
        /// 削除チェック
        /// </summary>
        public virtual bool LogicalChkDelete()
        {
            LogUtility.DebugMethodStart();

            //空行チェック
            if (!ChkBlankRow())
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E075");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 論理削除処理
        /// <summary>
        /// 論理削除処理
        /// </summary>
        [Transaction]
        public virtual void LogicalDelete()
        {
            LogUtility.DebugMethodStart();

            try
            {
                this.isRegist = true;
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C021");
                if (result == DialogResult.Yes)
                {
                    using (Transaction tran = new Transaction())
                    {
                        foreach (M_CONTENA contenashuruiEntity in this.entitys)
                        {
                            M_CONTENA entity = this.imContenaDao.GetDataByCd(contenashuruiEntity);
                            if (entity != null)
                            {
                                this.contenaDao.Update(contenashuruiEntity);
                            }
                        }
                        tran.Commit();
                    }
                    msgLogic.MessageBoxShow("I001", "削除");
                }

            }
            catch (Exception ex)
            {

                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.form.errmessage.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //登録エラー
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 物理削除
        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region 登録ボタン処理

        #region 登録チェック
        /// <summary>
        /// 登録チェック
        /// </summary>
        public virtual bool ChkRegist()
        {
            LogUtility.DebugMethodStart();

            //空行チェック
            if (!ChkBlankRow())
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E061");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region 登録処理
        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            try
            {
                this.isRegist = true;
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        foreach (M_CONTENA contenaEntity in this.entitys)
                        {
                            M_CONTENA entity = this.imContenaDao.GetDataByCd(contenaEntity);
                            if (entity == null)
                            {
                                this.contenaDao.Insert(contenaEntity);
                            }
                            else
                            {
                                this.contenaDao.Update(contenaEntity);
                            }
                        }
                        tran.Commit();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    this.form.errmessage.MessageBoxShow("E080");
                }
                else if (ex is SQLRuntimeException)
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245");
                }
                this.isRegist = false;
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag">エラーフラグ</param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region CSVボタン処理

        #region CSV出力チェック
        /// <summary>
        /// CSV出力チェック
        /// </summary>
        public virtual bool ChkCSVOutput()
        {
            LogUtility.DebugMethodStart();

            //空行チェック
            if (!ChkBlankRow())
            {
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("E044");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        public bool CSVOutput()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.dgvCsv, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_CONTENA), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #endregion

        #region 条件クリアボタン処理

        #region 条件クリア
        /// <summary>
        /// 条件クリア
        /// </summary>
        public void CancelCondition()
        {
            LogUtility.DebugMethodStart();
            ClearCondition();
            SetIchiran();
            LogUtility.DebugMethodEnd();
        }
        #endregion

        #endregion

        #region 取消ボタン処理

        #region 取消処理
        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel(out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (!initCondition())
                {
                    LogUtility.DebugMethodEnd(false, catchErr);
                    return false;
                }
                SetSearchString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(true, catchErr);
            return true;
        }
        #endregion

        #endregion

        #region 検索処理
        /// <summary>
        /// 検索処理
        /// </summary>
        /// <returns>取得件数</returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();
            int count = 0;
            try
            {

                SetSearchString();

                //検索
                this.SearchResult = contenaDao.GetContenaDataForEntity(SearchString,
                                                                       this.form.chk_Sakujo.Checked);
                this.SearchResultCheck = contenaDao.GetContenaDataForEntity(SearchString,
                                                                       this.form.chk_Sakujo.Checked);
                this.SearchResultAll = contenaDao.GetContenaAllDataForEntity(new M_CONTENA());

                Properties.Settings.Default.ConditionValue_Text = this.form.txt_ConditionValue.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.txt_ConditionValue.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.txt_ConditionValue.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.txt_ConditionItem.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.chk_Sakujo.Checked;

                Properties.Settings.Default.Save();

                dtDetailList = this.SearchResult.Copy();

                //取得したデータをグリッドに設定
                //this.form.mlt_gcCustomMultiRow.DataSource = SearchResult;

                //取得したデータをCSVに設定
                this.form.dgvCsv.DataSource = SearchResult;

                count = this.SearchResult.Rows.Count;
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
            LogUtility.DebugMethodEnd(count);
            return count;
        }
        #endregion

        #region 検索条件初期化

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        public bool initCondition()
        {

            LogUtility.DebugMethodStart();

            // システム情報を取得し、初期値をセットする
            if (!GetSysInfoInit())
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            this.form.txt_ConditionItem.Text = string.Empty;
            this.form.txt_ConditionValue.Text = string.Empty;

            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region 検索条件クリア

        /// <summary>
        /// 検索条件クリア
        /// </summary>
        public void ClearCondition()
        {

            LogUtility.DebugMethodStart();

            //this.form.chk_Tekiyoucyuu.Checked = false;
            //this.form.chk_Sakujo.Checked = false;
            //this.form.chk_Tekiyoukikangai.Checked = false;
            this.form.txt_ConditionItem.Text = string.Empty;
            this.form.txt_ConditionValue.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件の設定
        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            LogUtility.DebugMethodStart();

            DTOClass entity = new DTOClass();

            if (!string.IsNullOrEmpty(this.form.txt_ConditionValue.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.txt_ConditionValue.ItemDefinedTypes))
                {
                    //検索条件の設定
                    entity.SetValue(this.form.txt_ConditionValue);
                }
            }

            this.SearchString = entity;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索結果を一覧に設定
        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            LogUtility.DebugMethodStart();

            var table = this.SearchResult;

            table.BeginLoadData();

            for (int i = 0; i < table.Columns.Count; i++)
            {
                table.Columns[i].ReadOnly = false;
            }

            this.form.mlt_gcCustomMultiRow.DataSource = table;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region ListをDataTableに変換
        /// <summary>
        /// ListをDataTableに変換
        /// </summary>
        /// <param name="list">変換対象</param>
        /// <returns>変換結果</returns>
        public DataTable ListToDataTable(IList list)
        {
            LogUtility.DebugMethodStart(list);

            DataTable result = new DataTable();
            if (list.Count > 0)
            {
                PropertyInfo[] propertys = list[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    Type colType = pi.PropertyType;
                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {

                        colType = colType.GetGenericArguments()[0];

                    }

                    result.Columns.Add(pi.Name, colType);
                }

                for (int i = 0; i < list.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(list[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    result.LoadDataRow(array, true);
                }
            }

            LogUtility.DebugMethodEnd(result);
            return result;
        }

        #endregion

        #region コンテナのEntityを作成する
        /// <summary>
        /// コンテナのEntityを作成する
        /// </summary>
        /// <param name="isDelete"></param>
        public bool CreateEntity(bool isDelete, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(isDelete);
                catchErr = true;
                var entityList = new M_CONTENA[this.form.mlt_gcCustomMultiRow.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_CONTENA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_CONTENA>(entityList);

                DataTable dt = this.form.mlt_gcCustomMultiRow.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals("TIME_STAMP"))
                    {
                        column.Unique = false;
                    }

                }

                dt.BeginLoadData();

                var contenaEntityList = CreateEntityForDataGrid(this.form.mlt_gcCustomMultiRow, isDelete);

                // 変更分のみ取得
                DataTable dtCh = new DataTable();
                if (!isDelete)
                {
                    dtCh = dt.GetChanges();
                }
                else
                {
                    dtCh = ListToDataTable((IList)contenaEntityList);
                }


                //変更がない場合は処理を終了させる
                if (dtCh == null || dtCh.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(false, isDelete, catchErr);
                    return false;
                }

                List<M_CONTENA> addList = new List<M_CONTENA>();
                for (int i = 0; i < dtCh.Rows.Count; i++)
                {

                    for (int j = 0; j < contenaEntityList.Count; j++)
                    {
                        var contenaEntity = contenaEntityList[j];
                        bool isFind = false;
                        if (dtCh.Rows[i]["CONTENA_SHURUI_CD"].ToString() == contenaEntity.CONTENA_SHURUI_CD &&
                            dtCh.Rows[i]["CONTENA_CD"].ToString() == contenaEntity.CONTENA_CD)
                        {
                            isFind = true;
                        }

                        if (isFind)
                        {
                            //2014.05.22 EV003859_コンテナ状況CDをブランクで登録すると、自動で0が表示されてしまう。 by 胡　start
                            if (contenaEntity.JOUKYOU_KBN == 0)
                            {
                                contenaEntity.JOUKYOU_KBN = SqlInt16.Null;
                            }
                            //2014.05.22 EV003859_コンテナ状況CDをブランクで登録すると、自動で0が表示されてしまう。 by 胡　end
                            addList.Add(contenaEntity);
                            break;
                        }
                    }

                    this.entitys = addList.ToArray();

                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CreateEntity", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            LogUtility.DebugMethodEnd(true, isDelete, catchErr);
            return true;
        }

        #endregion

        #region nullをFalseに置き換える
        /// <summary>
        /// nullをFalseに置き換える
        /// </summary>
        /// <param name="chkText">チェック対象</param>
        /// <returns>置き換え後文字列</returns>
        private bool NullToFalse(object chkText)
        {

            LogUtility.DebugMethodStart(chkText);

            if (chkText == null || DBNull.Value.Equals(chkText))
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            else
            {
                LogUtility.DebugMethodEnd((bool)chkText);
                return (bool)chkText;
            }
        }

        #endregion

        #region DataTableのクローン処理
        /// <summary>
        /// DataTableのクローン処理
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private DataTable GetCloneDataTable(DataTable dt)
        {
            LogUtility.DebugMethodStart(dt);

            // dtのスキーマや制約をコピー
            DataTable table = dt.Clone();

            foreach (DataRow row in dt.Rows)
            {
                DataRow addRow = table.NewRow();

                // カラム情報をコピー
                addRow.ItemArray = row.ItemArray;

                table.Rows.Add(addRow);
            }

            LogUtility.DebugMethodEnd(table);
            return table;
        }

        #endregion

        #region システム情報を取得し、初期値をセットする
        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public bool GetSysInfoInit()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                    this.form.chk_Sakujo.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetSysInfoInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }
        #endregion

        #region 削除チェック

        /// <summary>
        /// 削除チェック
        /// </summary>
        /// <param name="multiRow">一覧</param>
        /// <param name="isDelete">削除フラグ</param>
        /// <returns></returns>
        private List<M_CONTENA> CreateEntityForDataGrid(GcCustomMultiRow multiRow, bool isDelete)
        {

            LogUtility.DebugMethodStart(multiRow, isDelete);

            var entityList = new List<M_CONTENA>();
            for (int i = 0; i < multiRow.RowCount - 1; i++)
            {
                if ((bool)NullToFalse(multiRow.Rows[i].Cells["chkDelete"].Value) == isDelete)
                {
                    M_CONTENA mContena = new M_CONTENA();

                    //コンテナ種類CD
                    mContena.CONTENA_SHURUI_CD = String.Format("{0:D" + ContenaHoshuConstans.CONTENASYURUICD_MAXLENGTH + "}", multiRow.Rows[i].Cells["txtContenaSyuruiCd"].Value.ToString());
                    //コンテナCD
                    mContena.CONTENA_CD = String.Format("{0:D" + ContenaHoshuConstans.CONTENACD_MAXLENGTH + "}", multiRow.Rows[i].Cells["txtContenaCd"].Value.ToString());
                    //コンテナ名
                    mContena.CONTENA_NAME = multiRow.Rows[i].Cells["txtContenaMei"].Value.ToString();
                    //コンテナ略称名
                    mContena.CONTENA_NAME_RYAKU = multiRow.Rows[i].Cells["txtContenaRyakuMei"].Value.ToString();
                    //業者CD
                    mContena.GYOUSHA_CD = multiRow.Rows[i].Cells["txtGyoushaCd"].Value.ToString();
                    //現場CD
                    mContena.GENBA_CD = multiRow.Rows[i].Cells["txtGenbaCd"].Value.ToString();
                    //拠点CD
                    mContena.KYOTEN_CD = SqlInt16.Parse(BlankToZero(multiRow.Rows[i].Cells["txtKyotenCd"].Value.ToString()));
                    //車輌CD
                    mContena.SHARYOU_CD = multiRow.Rows[i].Cells["txtSyaryouCd"].Value.ToString();
                    //設置日
                    if (multiRow.Rows[i].Cells["dtimSettibi"].Value.ToString() != "")
                    {
                        mContena.SECCHI_DATE = SqlDateTime.Parse(multiRow.Rows[i].Cells["dtimSettibi"].Value.ToString());
                    }
                    //引揚日
                    if (multiRow.Rows[i].Cells["dateTimeHikiage"].Value.ToString() != "")
                    {
                        mContena.HIKIAGE_DATE = SqlDateTime.Parse(multiRow.Rows[i].Cells["dateTimeHikiage"].Value.ToString());
                    }
                    //状況区分
                    mContena.JOUKYOU_KBN = SqlInt16.Parse(BlankToZero(multiRow.Rows[i].Cells["txtJoukyouCd"].Value.ToString()));
                    //購入日
                    if (multiRow.Rows[i].Cells["dtimKounyubi"].Value.ToString() != "")
                    {
                        mContena.KOUNYUU_DATE = SqlDateTime.Parse(multiRow.Rows[i].Cells["dtimKounyubi"].Value.ToString());
                    }
                    //最新修復日
                    if (multiRow.Rows[i].Cells["dtimSaishinsyuufukubi"].Value.ToString() != "")
                    {
                        mContena.LAST_SHUUFUKU_DATE = SqlDateTime.Parse(multiRow.Rows[i].Cells["dtimSaishinsyuufukubi"].Value.ToString());
                    }
                    //コンテナ備考
                    mContena.CONTENA_BIKOU = multiRow.Rows[i].Cells["txtBikou"].Value.ToString();

                    //最終更新者
                    //最終更新日時
                    //最終更新PC
                    var WHO = new DataBinderLogic<M_CONTENA>(mContena);
                    WHO.SetSystemProperty(mContena, false);

                    //削除フラグ
                    mContena.DELETE_FLG = isDelete;
                    //タイムスタンプ
                    if (multiRow.Rows[i].Cells["txtTimeStamp"].Value.Equals(System.DBNull.Value))
                    {
                        mContena.TIME_STAMP = null;
                    }
                    else
                    {
                        mContena.TIME_STAMP = (byte[])multiRow.Rows[i].Cells["txtTimeStamp"].Value;
                    }

                    entityList.Add(mContena);
                }

            }
            LogUtility.DebugMethodEnd(entityList);
            return entityList;
        }

        #endregion

        #region 空行チェック
        /// <summary>
        /// 空行チェック
        /// </summary>
        /// <returns>チェック結果</returns>
        private bool ChkBlankRow()
        {
            LogUtility.DebugMethodStart();

            //明細行が1行の場合
            if (this.form.mlt_gcCustomMultiRow.RowCount == 1)
            {
                if (this.form.mlt_gcCustomMultiRow.Rows[0].Cells["txtContenaSyuruiCd"].FormattedValue == null)
                {
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }
            }
            LogUtility.DebugMethodEnd(true);
            return true;
        }

        #endregion

        #region キー項目の重複チェック
        /// <summary>
        /// キー項目の重複チェック
        /// </summary>
        /// <returns>チェック結果</returns>
        public bool DuplicationCheck(out bool catchErr)
        {
            catchErr = true;
            bool result = true;
            try
            {
                LogUtility.DebugMethodStart();

                ContenaHoshuValidator vali = new ContenaHoshuValidator();

                result = vali.contenaCDValidator(this.form.mlt_gcCustomMultiRow, this.dtDetailList, this.SearchResultCheck, this.SearchResultAll);
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("DuplicationCheck", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(result, catchErr);
            }
            return result;
        }
        #endregion

        #region 空白をゼロに置き換える
        /// <summary>
        /// 空白をゼロに置き換える
        /// </summary>
        /// <param name="chkText">チェック対象</param>
        /// <returns>置き換え後文字列</returns>
        private String BlankToZero(string chkText)
        {

            LogUtility.DebugMethodStart(chkText);

            if (chkText == "")
            {
                LogUtility.DebugMethodEnd("0");
                return "0";
            }
            else
            {
                LogUtility.DebugMethodEnd(chkText);
                return chkText;
            }
        }
        #endregion

        #region 業者入力チェック
        /// <summary>
        /// 業者入力チェック
        /// </summary>
        /// <param name="gyousha">業者CD</param>
        /// <returns>チェック結果</returns>
        public bool ChkGyoushaNyuryoku(object gyousha)
        {
            LogUtility.DebugMethodStart(gyousha);

            if (!string.IsNullOrEmpty(gyousha.ToString()))
            {
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            else
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }
        }
        #endregion

        #region 現場情報取得
        /// <summary>
        /// 現場情報取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public string getGenbaInfo(string gyoushaCd, string genbaCd, out bool catchErr)
        {
            string returnVal = string.Empty;
            catchErr = true;
            try
            {
                //LogUtility.DebugMethodStart(gyoushaCd,genbaCd);

                if (string.IsNullOrEmpty(genbaCd))
                {
                    //LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }

                M_GENBA keyEntity = new M_GENBA();
                keyEntity.GYOUSHA_CD = gyoushaCd;
                keyEntity.GENBA_CD = genbaCd;
                var genba = this.imGenbaDao.GetAllValidData(keyEntity);

                if (genba == null || genba.Length < 1)
                {
                    //LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }
                returnVal = genba[0].GENBA_NAME_RYAKU;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("getGenbaInfo", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("getGenbaInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            //LogUtility.DebugMethodEnd(returnVal);
            return returnVal;
        }
        #endregion

        #region 車輌情報取得
        /// <summary>
        /// 車輌情報取得
        /// </summary>
        /// <param name="genbaCd"></param>
        /// <returns></returns>
        public string getSharyouInfo(string sharyouCd, string gyoushaCd, out bool catchErr)
        {
            catchErr = true;
            string returnVal = string.Empty;
            try
            {

                LogUtility.DebugMethodStart(sharyouCd, gyoushaCd);

                if (string.IsNullOrEmpty(sharyouCd))
                {
                    LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }

                if (string.IsNullOrEmpty(gyoushaCd))
                {
                    LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }

                M_SHARYOU keyEntity = new M_SHARYOU();
                keyEntity.SHARYOU_CD = sharyouCd;
                keyEntity.GYOUSHA_CD = gyoushaCd;
                var sharyou = this.imSharyouDao.GetAllValidData(keyEntity);

                if (sharyou == null || sharyou.Length < 1)
                {
                    LogUtility.DebugMethodEnd(returnVal);
                    return returnVal;
                }
                returnVal = sharyou[0].SHARYOU_NAME_RYAKU;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("getSharyouInfo", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("getSharyouInfo", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(returnVal);
            }
            return returnVal;
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

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            bool ret = true;
            try
            {
                // DBから主キーのListを取得
                var allPrimaryKeyList = this.imContenaDao.GetAllData().Select(s => new
                {
                    CONTENA_SHURUI_CD = s.CONTENA_SHURUI_CD,
                    CONTENA_CD = s.CONTENA_CD
                }).ToList();

                // DBに存在する行の主キーを非活性にする
                foreach (Row r in this.form.mlt_gcCustomMultiRow.Rows)
                {
                    if (r.Cells["txtContenaSyuruiCd"].Value != null &&
                        r.Cells["txtContenaCd"].Value != null)
                    {
                        int count = 0;
                        count += allPrimaryKeyList.Select(s => s.CONTENA_SHURUI_CD).ToList().Contains(r.Cells["txtContenaSyuruiCd"].Value) ? 1 : 0;
                        count += allPrimaryKeyList.Select(s => s.CONTENA_CD).ToList().Contains(r.Cells["txtContenaCd"].Value) ? 1 : 0;

                        if (count >= 2)
                        {
                            r.Cells["txtContenaSyuruiCd"].ReadOnly = true;
                            r.Cells["txtContenaCd"].ReadOnly = true;
                            r.Cells["txtContenaSyuruiCd"].UpdateBackColor(false);
                            r.Cells["txtContenaCd"].UpdateBackColor(false);
                        }
                    }
                }
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
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }
    }
}
