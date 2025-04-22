// $Id: LogicCls.cs 43108 2015-02-26 00:37:53Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Master.ContenaShuruiHoshu.APP;
using Shougun.Core.Master.ContenaShuruiHoshu.Const;
using Shougun.Core.Master.ContenaShuruiHoshu.Dao;

namespace Shougun.Core.Master.ContenaShuruiHoshu.Logic
{
    /// <summary>
    /// コンテナ種類画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.ContenaShuruiHoshu.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        /// <summary>
        /// コンテナ種類画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// ベースフォーム
        /// </summary>
        public MasterBaseForm parentForm;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// コンテナ種類のエンティティ
        /// </summary>
        private M_CONTENA_SHURUI[] entitys;

        /// <summary>
        /// コンテナ種類のDao
        /// </summary>
        private DaoCls dao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        /// <summary>
        /// メッセージ出力用のユーティリティ
        /// </summary>
        private MessageUtility MessageUtil;

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_CONTENA_SHURUI SearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>

        #endregion

        #region 初期化処理

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            // メッセージ出力用のユーティリティ
            MessageUtil = new MessageUtility();
            this.dao = DaoInitUtility.GetComponent<DaoCls>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            LogUtility.DebugMethodEnd(targetForm);
        }

        # endregion

        #region 画面初期化処理

        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public void WindowInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // 親フォームオブジェクト取得
                parentForm = (MasterBaseForm)this.form.Parent;

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                // システム情報を取得し、初期値をセットする
                //GetSysInfoInit();

                // 処理No（ESC)を入力不可にする
                this.parentForm.txb_process.Enabled = false;

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    // システム情報を取得し、初期値をセットする
                    this.GetSysInfoInit();
                }

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M204", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region ボタン初期化処理

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = this.CreateButtonInfo();
                var parentForm = (MasterBaseForm)this.form.Parent;
                ButtonControlUtility.SetButtonInfo(buttonSetting, parentForm, this.form.WindowType);
            }
            catch (Exception ex)
            {
                LogUtility.Error("ButtonInit", ex);
                throw;
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
            try
            {
                LogUtility.DebugMethodStart();

                var buttonSetting = new ButtonSetting();

                var thisAssembly = Assembly.GetExecutingAssembly();
                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region イベントの初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var parentForm = (MasterBaseForm)this.form.Parent;

                // 削除ボタン(F4)イベント生成
                this.form.C_MasterRegist(parentForm.bt_func4);
                parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
                parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

                //CSV出力ボタン(F6)イベント生成
                parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

                //条件クリアボタン(F7)イベント生成
                parentForm.bt_func7.Click += new EventHandler(this.form.ClearCondition);
                //parentForm.bt_func7.CausesValidation = false;

                //検索ボタン(F8)イベント生成
                parentForm.bt_func8.Click += new EventHandler(this.form.Search);
                //parentForm.bt_func8.CausesValidation = false;

                //登録ボタン(F9)イベント生成
                this.form.C_MasterRegist(parentForm.bt_func9);
                parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
                parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

                //取消ボタン(F11)イベント生成
                parentForm.bt_func11.Click += new EventHandler(this.form.Cancel);
                //parentForm.bt_func11.CausesValidation = false;

                //閉じるボタン(F12)イベント生成
                parentForm.bt_func12.Click += new EventHandler(this.form.FormClose);
            }
            catch (Exception ex)
            {
                LogUtility.Error("EventInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 参照モード表示

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

        #endregion

        #region 検索条件初期化

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        public void ClearCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
                this.form.CONDITION_ITEM.Text = string.Empty;
                //this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOU.Checked = false;
                //this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
                //this.form.ICHIRAN_HYOUJI_JOUKEN_TEKIYOUGAI.Checked = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ClearCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public bool SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();

                M_CONTENA_SHURUI entity = new M_CONTENA_SHURUI();

                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
                {
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                    {
                        //必要？
                        // 削除項目が選択された場合
                        //if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        //    this.form.CONDITION_VALUE.DBFieldsName.Equals("DELETE_FLG"))
                        //{
                        //    if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                        //        "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                        //        "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        //    {
                        //        this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = true;
                        //    }
                        //    else if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                        //        "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                        //        "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        //    {
                        //        this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
                        //    }
                        //    else
                        //    {
                        //        return false;
                        //    }
                        //}

                        //検索条件の設定
                        entity.SetValue(this.form.CONDITION_VALUE);
                    }
                }

                this.SearchString = entity;
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetSearchString", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 検索結果を一覧に設定

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal void SetIchiran()
        {
            try
            {
                LogUtility.DebugMethodStart();
                var table = this.SearchResult;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.DataSource = table;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region システム情報を取得し、初期値をセットする

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo[0];
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #endregion

        #region 業務処理

        #region 更新処理

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            throw new NotImplementedException();
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
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

            try
            {
                if (!isSelect)
                {
                    msgLogic.MessageBoxShow("E075", "削除");
                }
                else
                {
                    using (Transaction tran = new Transaction())
                    {
                        var result = msgLogic.MessageBoxShow("C021");
                        if (result == DialogResult.Yes)
                        {
                            if (this.entitys != null)
                            {
                                foreach (M_CONTENA_SHURUI contenashuruiEntity in this.entitys)
                                {
                                    if (contenashuruiEntity.CONTENA_SHURUI_CD == null)
                                    {
                                        msgLogic.MessageBoxShow("E075", "削除");
                                        return;
                                    }
                                    M_CONTENA_SHURUI entity = this.dao.GetDataByCd(contenashuruiEntity.CONTENA_SHURUI_CD.ToString());
                                    if (entity != null)
                                    {
                                        this.dao.Update(contenashuruiEntity);
                                    }
                                }
                            }
                            tran.Commit();
                            msgLogic.MessageBoxShow("I001", "削除");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //例外はここで処理
                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    // 排他エラーの場合
                    LogUtility.Error(ConstCls.ExceptionErrMsg.HAITA, ex);
                }
                else
                {
                    LogUtility.Error(ConstCls.ExceptionErrMsg.REIGAI, ex);
                    throw;
                }
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 物理削除処理

        /// <summary>
        /// 物理削除処理
        /// </summary>
        [Transaction]
        public virtual void PhysicalDelete()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region CSV出力

        /// <summary>
        /// CSV出力
        /// </summary>
        public void CSVOutput()
        {
            try
            {
                LogUtility.DebugMethodStart();
                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
                {
                    CSVExport csvExport = new CSVExport();
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_CONTENA_SHURUI), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region 条件取消

        /// <summary>
        /// 条件取消
        /// </summary>
        public void CancelCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();
                ClearCondition();
                SetIchiran();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region データ取得処理

        /// <summary>
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            LogUtility.DebugMethodStart();

            int count = 0;
            try
            {
                // エラーの場合、０件を戻る
                if (!SetSearchString())
                {
                    return 0;
                }

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                //this.SearchResult = ConstCls.ConvertGridViewDataTbl(this.SearchResult);
                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked; ;

                Properties.Settings.Default.Save();
                if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
                {
                    count = 1;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            LogUtility.DebugMethodEnd(count);
            return count;
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual bool RegistData(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            //独自チェックの記述例を書く
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        if (this.entitys != null)
                        {
                            // トランザクション開始
                            foreach (M_CONTENA_SHURUI contenashuruiEntity in this.entitys)
                            {
                                M_CONTENA_SHURUI entity = this.dao.GetDataByCd(contenashuruiEntity.CONTENA_SHURUI_CD.ToString());
                                if (entity == null)
                                {
                                    this.dao.Insert(contenashuruiEntity);
                                }
                                else
                                {
                                    this.dao.Update(contenashuruiEntity);
                                }
                            }
                        }
                        tran.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Debug(ex);//例外はここで処理

                if (ex is Seasar.Dao.NotSingleRowUpdatedRuntimeException)
                {
                    LogUtility.Warn(ex); //排他は警告
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E080");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    var messageShowLogic = new MessageBoxShowLogic();
                    messageShowLogic.MessageBoxShow("E093");
                }
                return false;
            }

            LogUtility.DebugMethodEnd(errorFlag);
            return true;
        }

        #endregion

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            var contenaCd = string.Empty;
            string[] strList;

            foreach (DataGridViewRow gcRwos in this.form.Ichiran.Rows)
            {
                if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                {
                    if (gcRwos.Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(gcRwos.Cells["CREATE_USER"].Value.ToString()))
                    {
                        continue;
                    }
                    contenaCd += gcRwos.Cells["CONTENA_SHURUI_CD"].Value.ToString() + ",";
                }
            }

            if (!string.IsNullOrEmpty(contenaCd))
            {
                contenaCd = contenaCd.Substring(0, contenaCd.Length - 1);
                strList = contenaCd.Split(',');
                DataTable dtTable = dao.GetDataContena(strList);
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    string strName = string.Empty;

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strName += "\n" + dr["NAME"].ToString();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E258", "コンテナ種類", "コンテナ類CD", strName);

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

        #region 取消処理

        /// <summary>
        /// 取消処理
        /// </summary>
        public void Cancel()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();

                // 検索項目を初期値にセットする
                this.form.CONDITION_VALUE.Text = "";
                this.form.CONDITION_VALUE.DBFieldsName = "";
                this.form.CONDITION_VALUE.ItemDefinedTypes = "";
                this.form.CONDITION_ITEM.Text = "";
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region コンテナ種類CDの重複チェック

        /// <summary>
        /// コンテナ種類CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string contena_Shurui_Cd, DataTable dtDetailList, out bool catchErr)
        {
            try
            {
                LogUtility.DebugMethodStart(contena_Shurui_Cd, dtDetailList);
                catchErr = true;
                // 画面で種類CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells[ConstCls.CONTENA_SHURUI_CD].Value.Equals(Convert.ToString(contena_Shurui_Cd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    return true;
                }

                // 検索結果で種類CD重複チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (contena_Shurui_Cd.Equals(dtDetailList.Rows[i]["CONTENA_SHURUI_CD"]))
                    {
                        return false;
                    }
                }

                // DBで種類CD重複チェック
                M_CONTENA_SHURUI entity = this.dao.GetDataByCd(contena_Shurui_Cd);

                if (entity != null)
                {
                    return true;
                }

                return false;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheck", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                catchErr = false;
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                catchErr = false;
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(contena_Shurui_Cd, dtDetailList);
            }
        }

        #endregion

        #region コントロールから対象のEntityを作成する

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var entityList = new M_CONTENA_SHURUI[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_CONTENA_SHURUI();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_CONTENA_SHURUI>(entityList);
                DataTable dt = this.form.Ichiran.DataSource as DataTable;

                if (dt == null || dt.Rows.Count == 0)
                {
                    return false;
                }

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                int count = 0;
                List<M_CONTENA_SHURUI> mContenaShuruiList = new List<M_CONTENA_SHURUI>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i][0].ToString() != "" && this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            if (string.IsNullOrEmpty(dt.Rows[i]["CREATE_USER"].ToString())
                                && (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null
                                || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                            {
                                isSelect = true;
                                count++;
                                continue;
                            }
                            isSelect = true;
                            var m_ContenaShuruiEntity = CreateEntityForDataGridRow(dt.Rows[i]);
                            var dataBinderEntry = new DataBinderLogic<M_CONTENA_SHURUI>(m_ContenaShuruiEntity);
                            dataBinderEntry.SetSystemProperty(m_ContenaShuruiEntity, true);
                            m_ContenaShuruiEntity.DELETE_FLG = true;
                            mContenaShuruiList.Add(m_ContenaShuruiEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mContenaShuruiList.ToArray();
                    if (this.entitys.Length == 0 && count == 0)
                    {
                        return false;
                    }
                }
                else
                {
                    // 変更分のみ取得
                    List<M_CONTENA_SHURUI> addList = new List<M_CONTENA_SHURUI>();
                    if (dt.GetChanges() == null)
                    {
                        this.entitys = new List<M_CONTENA_SHURUI>().ToArray();
                        return false;
                    }

                    // 元の値から全く変化がなければ、 RowState を元の状態に戻す
                    foreach (DataRow row in dt.Rows)
                    {
                        if (!DataTableUtility.IsDataRowChanged(row))
                        {
                            row.AcceptChanges();
                        }
                    }

                    if (dt.GetChanges() == null)
                    {
                        return true;
                    }

                    // 変更したデータ取得
                    var rows = dt.GetChanges().Select("DELETE_FLG = 0");

                    // データ変更なし
                    if (rows.Length == 0)
                    {
                        this.entitys = new List<M_CONTENA_SHURUI>().ToArray();
                        return false;
                    }

                    var contenashuruiEntityList = CreateEntityForDataGrid(rows);
                    for (int i = 0; i < contenashuruiEntityList.Count; i++)
                    {
                        var contenashuruiEntity = contenashuruiEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            if (this.form.Ichiran.Rows[j].Cells[ConstCls.CONTENA_SHURUI_CD].Value.Equals(Convert.ToString(contenashuruiEntity.CONTENA_SHURUI_CD)) &&
                                     bool.Parse(this.form.Ichiran.Rows[j].Cells[ConstCls.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(contenashuruiEntity, false);
                                var dataBinderEntry = new DataBinderLogic<M_CONTENA_SHURUI>(contenashuruiEntity);
                                dataBinderEntry.SetSystemProperty(contenashuruiEntity, false);
                                addList.Add(contenashuruiEntity);
                                break;
                            }
                        }
                        if (addList.Count > 0)
                        {
                            this.entitys = addList.ToArray();
                        }
                        else
                        {
                            this.entitys = new List<M_CONTENA_SHURUI>().ToArray();
                            return false;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(isDelete);
            }
        }

        #endregion

        #region CreateEntityForDataGrid

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <returns>
        /// entityList
        /// </returns>
        internal List<M_CONTENA_SHURUI> CreateEntityForDataGrid(IEnumerable<DataRow> rows)
        {
            try
            {
                LogUtility.DebugMethodStart(rows);

                var entityList = rows.Select(r => CreateEntityForDataGridRow(r)).ToList();

                return entityList;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGrid", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(rows);
            }
        }

        #endregion

        #region CreateEntityForDataGridRow

        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        /// <returns>
        /// mContenaShurui
        /// </returns>
        internal M_CONTENA_SHURUI CreateEntityForDataGridRow(DataRow row)
        {
            try
            {
                LogUtility.DebugMethodStart(row);

                M_CONTENA_SHURUI mContenaShurui = new M_CONTENA_SHURUI();

                // CONTENA_SHURUI_CD
                if (!DBNull.Value.Equals(row.Field<string>("CONTENA_SHURUI_CD")))
                {
                    mContenaShurui.CONTENA_SHURUI_CD = row.Field<string>("CONTENA_SHURUI_CD");
                }

                // CONTENA_SHURUI_NAME
                if (!DBNull.Value.Equals(row.Field<string>("CONTENA_SHURUI_NAME")))
                {
                    mContenaShurui.CONTENA_SHURUI_NAME = row.Field<string>("CONTENA_SHURUI_NAME");
                }

                // CONTENA_SHURUI_NAME_RYAKU
                if (!DBNull.Value.Equals(row.Field<string>("CONTENA_SHURUI_NAME_RYAKU")))
                {
                    mContenaShurui.CONTENA_SHURUI_NAME_RYAKU = row.Field<string>("CONTENA_SHURUI_NAME_RYAKU");
                }

                // CONTENA_SHURUI_FURIGANA
                if (!DBNull.Value.Equals(row.Field<string>("CONTENA_SHURUI_FURIGANA")))
                {
                    mContenaShurui.CONTENA_SHURUI_FURIGANA = row.Field<string>("CONTENA_SHURUI_FURIGANA");
                }

                // CONTENA_SHURUI_BIKOU
                if (!DBNull.Value.Equals(row.Field<string>("CONTENA_SHURUI_BIKOU")))
                {
                    mContenaShurui.CONTENA_SHURUI_BIKOU = row.Field<string>("CONTENA_SHURUI_BIKOU");
                }

                // DELETE_FLG
                if (!DBNull.Value.Equals(row.Field<bool>("DELETE_FLG")))
                {
                    mContenaShurui.DELETE_FLG = row.Field<bool>("DELETE_FLG");
                }
                else
                {
                    mContenaShurui.DELETE_FLG = false;
                }

                // TIME_STAMP
                if (!DBNull.Value.Equals(row.Field<byte[]>("TIME_STAMP")))
                {
                    mContenaShurui.TIME_STAMP = row.Field<byte[]>("TIME_STAMP");
                }
                // SHOYUU_DAISUU
                if (!DBNull.Value.Equals(row["SHOYUU_DAISUU"]))
                {
                    mContenaShurui.SHOYUU_DAISUU = row.Field<Int32>("SHOYUU_DAISUU");
                }
                return mContenaShurui;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGridRow", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(row);
            }
        }

        #endregion

        #region Mulit行メッセージを生成

        /// <summary>
        /// Mulit行メッセージを生成
        /// </summary>
        /// <param name="msgID">メッセージID</param>
        /// <param name="str">整形時に利用する文言のリスト</param>
        /// <returns>整形済みメッセージ</returns>
        private string CreateMulitMessage(string msgID, params string[] str)
        {
            // 整形済みメッセージ
            string msgResult = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(msgID, str);

                // メッセージ原本
                MessageUtil = new MessageUtility();
                string msg = MessageUtil.GetMessage("E001").MESSAGE;

                for (int i = 0; i < str.Length; i++)
                {
                    string msgTmp = string.Format(msg, str[i]);
                    if (!string.IsNullOrEmpty(msgResult))
                    {
                        msgResult += "\r\n";
                    }
                    msgResult += msgTmp;
                }

                return msgResult;
            }
            catch (Exception ex)
            {
                LogUtility.Error(ConstCls.ExceptionErrMsg.REIGAI, ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd(msgID, str);
            }
        }

        #endregion

        #region DataGridViewデータ件数チェック処理

        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        public bool ActionBeforeCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.Ichiran.Rows.Count > 1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ActionBeforeCheck", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region NOT NULL制約を一時的に解除

        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        public void ColumnAllowDBNull()
        {
            try
            {
                LogUtility.DebugMethodStart();

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("ColumnAllowDBNull", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        #region レコード選択チェック処理

        /// <summary>
        /// レコード選択チェック処理
        /// </summary>
        public bool isSelectFlg()
        {
            try
            {
                LogUtility.DebugMethodStart();

                if (!isSelect)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("isSelectFlg", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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

        public virtual void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
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
                var allEntityList = this.dao.GetAllData().Select(s => s.CONTENA_SHURUI_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["CONTENA_SHURUI_CD"]).Where(c => c.Value != null).ToList().
                                        ForEach(c => c.ReadOnly = allEntityList.Contains(c.Value.ToString()));
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("EditableToPrimaryKey", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }
    }
}