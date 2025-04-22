// $Id: LogicCls.cs 36567 2014-12-05 02:08:13Z fangjk@oec-h.com $
using System;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections;
using System.Windows.Forms;
using System.Data.SqlTypes;
using System.Collections.Generic;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using r_framework.CustomControl;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Common.BusinessCommon.Utility;
using Shougun.Core.Master.ContenaJoukyouHoshu.Const;    
using Shougun.Core.Master.ContenaJoukyouHoshu.APP;
using Shougun.Core.Master.ContenaJoukyouHoshu.Dao;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.ContenaJoukyouHoshu.Logic
{
    /// <summary>
    /// コンテナ状況画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.ContenaJoukyouHoshu.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        /// <summary>
        /// コンテナ状況画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// コンテナ状況のエンティティ
        /// </summary>
        private M_CONTENA_JOUKYOU[] entitys;

        /// <summary>
        /// コンテナ状況のDao
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

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }
        /// <summary>
        /// 検索条件
        /// </summary>
        public M_CONTENA_JOUKYOU SearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        public DataTable dtDetailList = new DataTable();

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
            this.dao = DaoInitUtility.GetComponent<DaoCls>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

            LogUtility.DebugMethodEnd();
        }
        # endregion

        #region 画面初期化処理
        /// <summary>
        /// 画面初期化処理
        /// </summary>
        public bool WindowInit()
        {
            LogUtility.DebugMethodStart();

            // ボタンのテキストを初期化
            this.ButtonInit();

            // イベントの初期化処理
            this.EventInit();

            this.allControl = this.form.allControl;

            // システム情報を取得し、初期値をセットする
            if (!GetSysInfoInit())
            {
                return false;
            }

            //前の表示条件を初期値に設定する
            if (Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED)
            {
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
            }

            this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
            this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
            this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
            this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

            LogUtility.DebugMethodEnd();
            return true;
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

        #region ボタン設定の読込
        /// <summary>
        /// ボタン設定の読込
        /// </summary>
        private ButtonSetting[] CreateButtonInfo()
        {
            LogUtility.DebugMethodStart();

            ButtonSetting[] rtn;

            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();

            rtn = buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);

            LogUtility.DebugMethodEnd(rtn);
            return rtn;
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
            this.form.C_Regist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

            //CSV出力ボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSVOutput);

            //条件クリアボタン(F7)イベント生成
            parentForm.bt_func7.Click += new EventHandler(this.form.ClearCondition);

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

            // 処理No
            parentForm.txb_process.Enabled = false;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 検索条件初期化
        /// <summary>
        /// 検索条件初期化
        /// </summary>
        public void ClearCondition()
        {
            LogUtility.DebugMethodStart();

            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 検索条件の設定
        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public bool SetSearchString()
        {
            LogUtility.DebugMethodStart();

            M_CONTENA_JOUKYOU entity = new M_CONTENA_JOUKYOU();

            //// CONDITION_TYPE
            //if (string.IsNullOrEmpty(this.form.CONDITION_TYPE.Text) &&
            //    !string.IsNullOrEmpty(Properties.Settings.Default.ConditionValue_ItemDefinedTypes))
            //{
            //    this.form.CONDITION_TYPE.Text = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
            //}

            //// CONDITION_DBFIELD
            //if (string.IsNullOrEmpty(this.form.CONDITION_DBFIELD.Text) &&
            //    !string.IsNullOrEmpty(Properties.Settings.Default.ConditionValue_DBFieldsName))
            //{
            //    this.form.CONDITION_DBFIELD.Text = Properties.Settings.Default.ConditionValue_DBFieldsName;
            //}

            ////this.form.CONDITION_ITEM.Text = this.form.CONDITION_ITEM.Text.Replace("※", "");
            //if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            //{
            //    if (!string.IsNullOrEmpty(this.form.CONDITION_TYPE.Text))
            //    {
            //        // 検索条件の設定
            //        // 削除
            //        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
            //            this.form.CONDITION_DBFIELD.Text.Equals("chb_delete"))
            //        {
            //            if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
            //                "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
            //                "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
            //            {
            //                entity.DELETE_FLG = true;
            //                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = true;
            //            }
            //            if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
            //                "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
            //                "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
            //            {
            //                entity.DELETE_FLG = false;
            //                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
            //            }
            //        }

            //        // コンテナ状況CD
            //        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
            //            this.form.CONDITION_DBFIELD.Text.Equals("CONTENA_JOUKYOU_CD"))
            //        {
            //            try
            //            {
            //                entity.CONTENA_JOUKYOU_CD = SqlInt16.Parse(this.form.CONDITION_VALUE.Text);
            //            }
            //            catch (Exception ex)
            //            {
            //                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //                msgLogic.MessageBoxShow("E012", "コンテナ状況CDは数値");

            //                form.CONDITION_VALUE.Focus();
            //                LogUtility.DebugMethodEnd(false);
            //                return false;
            //            }
            //        }

            //        // コンテナ状況名
            //        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
            //            this.form.CONDITION_DBFIELD.Text.Equals("CONTENA_JOUKYOU_NAME"))
            //        {
            //            entity.CONTENA_JOUKYOU_NAME = this.form.CONDITION_VALUE.Text;
            //        }

            //        // 略称
            //        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
            //            this.form.CONDITION_DBFIELD.Text.Equals("CONTENA_JOUKYOU_NAME_RYAKU"))
            //        {
            //            entity.CONTENA_JOUKYOU_NAME_RYAKU = this.form.CONDITION_VALUE.Text;
            //        }

            //        //// フリガナ
            //        //if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
            //        //    this.form.CONDITION_DBFIELD.Text.Equals("CONTENA_JOUKYOU_FURIGANA"))
            //        //{
            //        //    entity.CONTENA_JOUKYOU_FURIGANA = this.form.CONDITION_VALUE.Text;
            //        //}

            //        // 備考
            //        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
            //            this.form.CONDITION_DBFIELD.Text.Equals("CONTENA_JOUKYOU_BIKOU"))
            //        {
            //            entity.CONTENA_JOUKYOU_BIKOU = this.form.CONDITION_VALUE.Text;
            //        }

            //        // 更新者
            //        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
            //            this.form.CONDITION_DBFIELD.Text.Equals("UPDATE_USER"))
            //        {
            //            entity.UPDATE_USER = this.form.CONDITION_VALUE.Text;
            //        }

            //        // 更新日
            //        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
            //            this.form.CONDITION_DBFIELD.Text.Equals("UPDATE_DATE"))
            //        {
            //            entity.SEARCH_UPDATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
            //        }

            //        // 作成者
            //        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
            //            this.form.CONDITION_DBFIELD.Text.Equals("CREATE_USER"))
            //        {
            //            entity.CREATE_USER = this.form.CONDITION_VALUE.Text;
            //        }

            //        // 作成日
            //        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
            //            this.form.CONDITION_DBFIELD.Text.Equals("CREATE_DATE"))
            //        {
            //            entity.SEARCH_CREATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
            //        }
            //    }
            //}

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    //必要？
                    // 削除項目が選択された場合
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("DELETE_FLG"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
                        }
                    }
                    if (this.form.CONDITION_VALUE.DBFieldsName.Equals("CONTENA_JOUKYOU_CD"))
                    {
                        if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) && decimal.Parse(this.form.CONDITION_VALUE.Text) > 32767)
                        {
                            return false;
                        }

                    }

                    //検索条件の設定
                    entity.SetValue(this.form.CONDITION_VALUE);
                }
            }

            this.SearchString = entity;

            LogUtility.DebugMethodEnd(true);
            return true;
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

            this.form.Ichiran.DataSource = table;

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region システム情報を取得し、初期値をセットする
        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        public bool GetSysInfoInit()
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
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("GetSysInfoInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
            return true;
        }
        #endregion

        #endregion

        #region 業務処理

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

        #region 更新処理
        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Update(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);
            LogUtility.DebugMethodEnd();
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

                    var result = msgLogic.MessageBoxShow("C021");
                    if (result == DialogResult.Yes)
                    {
                        using (Transaction tran = new Transaction())
                        {
                            foreach (M_CONTENA_JOUKYOU contenashuruiEntity in this.entitys)
                            {
                                //// if (contenashuruiEntity.CONTENA_JOUKYOU_CD == null)
                                //if (SqlInt16.Null.Equals(contenashuruiEntity.CONTENA_JOUKYOU_CD))
                                //{
                                //    msgLogic.MessageBoxShow("E075", "削除");
                                //    return;
                                //}
                                M_CONTENA_JOUKYOU entity = this.dao.GetDataByCd(contenashuruiEntity.CONTENA_JOUKYOU_CD.ToString());
                                if (entity != null)
                                {
                                    ////String PCName = System.Environment.MachineName;
                                    //String UsrName = System.Environment.UserName;
                                    ////UsrName = UsrName.Length > 16 ? UsrName.Substring(0, 16) : UsrName;
                                    ////contenashuruiEntity.DELETE_FLG = true;
                                    //contenashuruiEntity.UPDATE_USER = UsrName;
                                    ////contenashuruiEntity.UPDATE_DATE = DateTime.Now;
                                    ////contenashuruiEntity.UPDATE_PC = PCName;

                                    //var WHO = new DataBinderLogic<M_CONTENA_JOUKYOU>(contenashuruiEntity);
                                    //WHO.SetSystemProperty(contenashuruiEntity, false);

                                    this.dao.Update(contenashuruiEntity);
                                }
                            }
                            tran.Commit();
                        }
                        msgLogic.MessageBoxShow("I001", "削除");
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
            LogUtility.DebugMethodStart();
            LogUtility.DebugMethodEnd();
            throw new NotImplementedException();
        }
        #endregion

        #region CSV出力
        /// <summary>
        /// CSV出力
        /// </summary>
        public void CSVOutput()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
            {
                // CSVFileLogic csvLogic = new CSVFileLogic();
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_CONTENA_JOUKYOU), this.form);
                // msgLogic.MessageBoxShow("I000", "CSV出力");
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            var contenaJoukyouCd = string.Empty;
            string[] strList;
            DataTable dt = this.form.Ichiran.DataSource as DataTable;
            foreach (DataRow Row in dt.Rows)
            {
                if (Row["DELETE_FLG"] != null && Row["DELETE_FLG"].ToString() == "True")
                {
                    contenaJoukyouCd += Row["CONTENA_JOUKYOU_CD"].ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(contenaJoukyouCd))
            {
                contenaJoukyouCd = contenaJoukyouCd.Substring(0, contenaJoukyouCd.Length - 1);
                strList = contenaJoukyouCd.Split(',');
                DataTable dtTable = dao.GetDataBySqlFileCheck(strList);
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    string strName = string.Empty;

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strName += "\n" + dr["NAME"].ToString();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E258", "コンテナ", "コンテナCD", strName);

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

        #region 条件取消
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
                if (false == SetSearchString())
                {
                    LogUtility.DebugMethodEnd(-1);
                    return -1;
                }

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                //Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_DBFIELD.Text;
                //Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_TYPE.Text;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                dtDetailList = this.SearchResult.Copy();

                if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
                {
                    count = 1;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
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
        public virtual void Regist(bool errorFlag)
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
                        // トランザクション開始
                        foreach (M_CONTENA_JOUKYOU contenajoukyouEntity in this.entitys)
                        {
                            M_CONTENA_JOUKYOU entity = this.dao.GetDataByCd(contenajoukyouEntity.CONTENA_JOUKYOU_CD.ToString());
                            //String PCName = System.Environment.MachineName;
                            //String strUser = System.Environment.UserName;
                            //strUser = strUser.Length > 16 ? strUser.Substring(0, 16) : strUser;
                            //contenajoukyouEntity.UPDATE_PC = PCName;
                            //contenajoukyouEntity.UPDATE_USER = strUser;
                            if (entity == null)
                            {
                                //contenajoukyouEntity.CREATE_PC = PCName;
                                //contenajoukyouEntity.CREATE_USER = strUser;
                                this.dao.Insert(contenajoukyouEntity);
                            }
                            else
                            {
                                this.dao.Update(contenajoukyouEntity);
                            }
                        }
                        tran.Commit();
                    }
                    msgLogic.MessageBoxShow("I001", "登録");
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
            }

            LogUtility.DebugMethodEnd();
        }
        #endregion

        #region 取消処理
        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            LogUtility.DebugMethodStart();

            // システム情報を取得し、初期値をセットする
            if (!GetSysInfoInit())
            {
                return false;
            }

            ClearCondition();

            LogUtility.DebugMethodEnd();
            return true;
        }
        #endregion

        #region コンテナ状況CDの重複チェック
        /// <summary>
        /// コンテナ状況CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string contena_Shurui_Cd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(contena_Shurui_Cd);
            catchErr = true;

            try
            {
                // 画面で種類CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells[ConstCls.CONTENA_JOUKYOU_CD].Value.ToString().Equals(Convert.ToString(contena_Shurui_Cd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果で種類CD重複チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    // if (contena_Shurui_Cd.Equals(dtDetailList.Rows[i][1].ToString()))
                    if (contena_Shurui_Cd.Equals(dtDetailList.Rows[i]["CONTENA_JOUKYOU_CD"].ToString()))
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }

                // DBで種類CD重複チェック
                M_CONTENA_JOUKYOU entity = this.dao.GetDataByCd(contena_Shurui_Cd);

                if (entity != null)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheck", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(false, catchErr);
            return false;
        }
        #endregion

        #region コントロールから対象のEntityを作成する
        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete, out bool catchErr)
        {
            LogUtility.DebugMethodStart(isDelete);
            catchErr = true;

            try
            {
                var entityList = new M_CONTENA_JOUKYOU[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_CONTENA_JOUKYOU();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_CONTENA_JOUKYOU>(entityList);
                DataTable dt = this.form.Ichiran.DataSource as DataTable;

                if (dt == null || dt.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(false, catchErr);
                    return false;
                }

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

                dt.BeginLoadData();
                preDt = GetCloneDataTable(dt);

                List<M_CONTENA_JOUKYOU> mContenaShuruiList = new List<M_CONTENA_JOUKYOU>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            isSelect = true;
                            var m_ContenaShuruiEntity = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_ContenaShuruiEntity, true);
                            //// 仮実装 start
                            //m_ContenaShuruiEntity.CREATE_USER = "CREATE_USER";
                            //m_ContenaShuruiEntity.UPDATE_USER = "UPDATE_USER";
                            //// 仮実装 end
                            m_ContenaShuruiEntity.DELETE_FLG = true;
                            mContenaShuruiList.Add(m_ContenaShuruiEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mContenaShuruiList.ToArray();
                }
                else
                {
                    // 変更分のみ取得
                    List<M_CONTENA_JOUKYOU> addList = new List<M_CONTENA_JOUKYOU>();

                    DataTable dtChange = new DataTable();

                    if (dt.GetChanges() == null)
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                    else
                    {
                        dtChange = dt.GetChanges();
                    }
                    var contenashuruiEntityList = CreateEntityForDataGrid(dtChange);
                    for (int i = 0; i < contenashuruiEntityList.Count; i++)
                    {
                        var contenashuruiEntity = contenashuruiEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            //if (this.form.Ichiran.Rows[j].Cells[ConstCls.CONTENA_JOUKYOU_CD].Value.Equals(Convert.ToString(contenashuruiEntity.CONTENA_JOUKYOU_CD)) &&
                            //         bool.Parse(this.form.Ichiran.Rows[j].Cells[ConstCls.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            if (Convert.ToString(contenashuruiEntity.CONTENA_JOUKYOU_CD).Equals(this.form.Ichiran.Rows[j].Cells[ConstCls.CONTENA_JOUKYOU_CD].Value.ToString()) &&
                                bool.Parse(this.form.Ichiran.Rows[j].Cells[ConstCls.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(contenashuruiEntity, false);
                                // 仮実装 start
                                //contenashuruiEntity.CREATE_USER = "CREATE_USER";
                                //contenashuruiEntity.UPDATE_USER = "UPDATE_USER";
                                //// 仮実装 end
                                addList.Add(contenashuruiEntity);
                                break;
                            }
                        }
                        this.form.Ichiran.DataSource = preDt;
                        this.entitys = addList.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
            }

            LogUtility.DebugMethodEnd(true, catchErr);
            return true;
        }
        #endregion

        #region CreateEntityForDataGrid
        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <returns>
        /// entityList
        /// </returns>
        internal List<M_CONTENA_JOUKYOU> CreateEntityForDataGrid(DataTable gridView)
        {
            LogUtility.DebugMethodStart(gridView);

            var entityList = new List<M_CONTENA_JOUKYOU>();
            if (gridView == null)
            {
                return entityList;
            }
            for (int i = 0; i < gridView.Rows.Count; i++)
            {
                entityList.Add(CreateEntityForDataRow(gridView.Rows[i]));
            }

            LogUtility.DebugMethodEnd(entityList);
            return entityList;
        }
        #endregion

        #region CreateEntityForDataGridRow
        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        /// <returns>
        /// mContenaShurui
        /// </returns>
        internal M_CONTENA_JOUKYOU CreateEntityForDataGridRow(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_CONTENA_JOUKYOU mContenaShurui = new M_CONTENA_JOUKYOU();

            // CONTENA_JOUKYOU_CD
            if (!DBNull.Value.Equals(row.Cells["CONTENA_JOUKYOU_CD"].Value))
            {
                mContenaShurui.CONTENA_JOUKYOU_CD = SqlInt16.Parse(row.Cells["CONTENA_JOUKYOU_CD"].Value.ToString());
            }

            // CONTENA_JOUKYOU_NAME
            if (!DBNull.Value.Equals(row.Cells["CONTENA_JOUKYOU_NAME"].Value))
            {
                mContenaShurui.CONTENA_JOUKYOU_NAME = (string)row.Cells["CONTENA_JOUKYOU_NAME"].Value;
            }

            // CONTENA_JOUKYOU_NAME_RYAKU
            if (!DBNull.Value.Equals(row.Cells["CONTENA_JOUKYOU_NAME_RYAKU"].Value))
            {
                mContenaShurui.CONTENA_JOUKYOU_NAME_RYAKU = (string)row.Cells["CONTENA_JOUKYOU_NAME_RYAKU"].Value;
            }

            //// CONTENA_JOUKYOU_FURIGANA
            //if (!DBNull.Value.Equals(row.Cells["CONTENA_JOUKYOU_FURIGANA"].Value))
            //{
            //    mContenaShurui.CONTENA_JOUKYOU_FURIGANA = (string)row.Cells["CONTENA_JOUKYOU_FURIGANA"].Value;
            //}

            // CONTENA_JOUKYOU_BIKOU
            if (!DBNull.Value.Equals(row.Cells["CONTENA_JOUKYOU_BIKOU"].Value))
            {
                mContenaShurui.CONTENA_JOUKYOU_BIKOU = (string)row.Cells["CONTENA_JOUKYOU_BIKOU"].Value;
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row.Cells["DELETE_FLG"].Value))
            {
                mContenaShurui.DELETE_FLG = (Boolean)row.Cells["DELETE_FLG"].Value;
            }
            else
            {
                mContenaShurui.DELETE_FLG = false;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
            {
                mContenaShurui.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
            }

            LogUtility.DebugMethodEnd(mContenaShurui);
            return mContenaShurui;
        }
        #endregion

        #region CreateEntityForDataRow

        /// <summary>
        /// CreateEntityForDataRow
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal M_CONTENA_JOUKYOU CreateEntityForDataRow(DataRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_CONTENA_JOUKYOU mContenaShurui = new M_CONTENA_JOUKYOU();

            // CONTENA_JOUKYOU_CD
            if (!DBNull.Value.Equals(row["CONTENA_JOUKYOU_CD"]))
            {
                mContenaShurui.CONTENA_JOUKYOU_CD = SqlInt16.Parse(row["CONTENA_JOUKYOU_CD"].ToString());
            }

            // CONTENA_JOUKYOU_NAME
            if (!DBNull.Value.Equals(row["CONTENA_JOUKYOU_NAME"]))
            {
                mContenaShurui.CONTENA_JOUKYOU_NAME = (string)row["CONTENA_JOUKYOU_NAME"];
            }

            // CONTENA_JOUKYOU_NAME_RYAKU
            if (!DBNull.Value.Equals(row["CONTENA_JOUKYOU_NAME_RYAKU"]))
            {
                mContenaShurui.CONTENA_JOUKYOU_NAME_RYAKU = (string)row["CONTENA_JOUKYOU_NAME_RYAKU"];
            }

            //// CONTENA_JOUKYOU_FURIGANA
            //if (!DBNull.Value.Equals(row.Cells["CONTENA_JOUKYOU_FURIGANA"].Value))
            //{
            //    mContenaShurui.CONTENA_JOUKYOU_FURIGANA = (string)row.Cells["CONTENA_JOUKYOU_FURIGANA"].Value;
            //}

            // CONTENA_JOUKYOU_BIKOU
            if (!DBNull.Value.Equals(row["CONTENA_JOUKYOU_BIKOU"]))
            {
                mContenaShurui.CONTENA_JOUKYOU_BIKOU = (string)row["CONTENA_JOUKYOU_BIKOU"];
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row["DELETE_FLG"]))
            {
                mContenaShurui.DELETE_FLG = (Boolean)row["DELETE_FLG"];
            }
            else
            {
                mContenaShurui.DELETE_FLG = false;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row["TIME_STAMP"]))
            {
                mContenaShurui.TIME_STAMP = (byte[])row["TIME_STAMP"];
            }

            LogUtility.DebugMethodEnd(mContenaShurui);
            return mContenaShurui;
        }

        #endregion

        #region 登録前のチェック
        /// <summary>
        /// 登録前のチェック
        /// </summary>
        /// <returns></returns>
        public Boolean CheckBeforeUpdate()
        {
            LogUtility.DebugMethodStart();

            //var msgLogic = new MessageBoxShowLogic();

            //ArrayList errColName = new ArrayList();

            Boolean rtn = false;

            //Boolean isErr;

            //// 必須入力チェック
            //isErr = false;
            //for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
            //{
            //    if (null == this.form.Ichiran.Rows[i].Cells["CONTENA_JOUKYOU_CD"].Value ||
            //        "".Equals(this.form.Ichiran.Rows[i].Cells["CONTENA_JOUKYOU_CD"].Value.ToString().Trim()))
            //    {
            //        if (false == isErr)
            //        {
            //            errColName.Add("コンテナ状況CD");
            //            isErr = true;
            //        }
            //        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["CONTENA_JOUKYOU_CD"], true);
            //    }
            //}

            //// コンテナ状況名
            //isErr = false;
            //for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
            //{
            //    if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["CONTENA_JOUKYOU_NAME"].Value) ||
            //        "".Equals(this.form.Ichiran.Rows[i].Cells["CONTENA_JOUKYOU_NAME"].Value.ToString().Trim()))
            //    {
            //        if (false == isErr)
            //        {
            //            errColName.Add("コンテナ状況名");
            //            isErr = true;
            //        }
            //        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["CONTENA_JOUKYOU_NAME"], true);
            //    }
            //}

            //// 略称
            //isErr = false;
            //for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
            //{
            //    if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["CONTENA_JOUKYOU_NAME_RYAKU"].Value) ||
            //        "".Equals(this.form.Ichiran.Rows[i].Cells["CONTENA_JOUKYOU_NAME_RYAKU"].Value.ToString().Trim()))
            //    {
            //        if (false == isErr)
            //        {
            //            errColName.Add("略称");
            //            isErr = true;
            //        }
            //        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["CONTENA_JOUKYOU_NAME_RYAKU"], true);
            //    }
            //}

            //rtn = (errColName.Count == 0);

            //MessageUtility messageUtility = new MessageUtility();
            //string message = messageUtility.GetMessage("E001").MESSAGE;

            //string errMsg = "";
            //if (!rtn)
            //{
            //    foreach (string colName in errColName)
            //    {
            //        if (errMsg.Length > 0)
            //        {
            //            errMsg += "\n";
            //        }
            //        errMsg += message.Replace("{0}", colName);
            //    }
            //    MessageBox.Show(errMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}

            rtn = true;

            LogUtility.DebugMethodEnd(rtn);
            return rtn;
        }
        #endregion

        #region DataGridViewデータ件数チェック処理
        /// <summary>
        /// DataGridViewデータ件数チェック処理
        /// </summary>
        public bool ActionBeforeCheck()
        {
            LogUtility.DebugMethodStart();

            if (this.form.Ichiran.Rows.Count > 1)
            {
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
        }
        #endregion

        #region NOT NULL制約を一時的に解除
        /// <summary>
        /// NOT NULL制約を一時的に解除
        /// </summary>
        public void ColumnAllowDBNull()
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

            LogUtility.DebugMethodEnd();
        }
        #endregion

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

        #region テーブル固定値編集不可処理

        /// <summary>
        /// テーブル固定値定義書に存在するデータの場合、削除、名称、略称、適用期間を変更不可にする
        /// </summary>
        internal void SetFixedIchiran()
        {
            foreach (DataGridViewRow row in this.form.Ichiran.Rows)
            {
                if (!row.IsNewRow)
                {
                    this.SetFixedRow(row);
                }
            }
        }

        /// <summary>
        /// テーブル固定値の変更不可処理を行います
        /// </summary>
        /// <param name="row">変更不可処理を行うための判定データが入力されているRow</param>
        internal void SetFixedRow(DataGridViewRow row)
        {
            if (this.CheckFixedRow(row))
            {
                foreach (var columnName in Const.ConstCls.FixedColumnList)
                {
                    row.Cells[columnName].ReadOnly = true;
                }
            }
        }

        /// <summary>
        /// テーブル固定値のデータかどうかを調べる
        /// </summary>
        /// <param name="row">行データ</param>
        /// <returns>結果</returns>
        internal bool CheckFixedRow(DataGridViewRow row)
        {
            if (row != null)
            {
                var objValue = row.Cells[Const.ConstCls.CONTENA_JOUKYOU_CD].Value;
                if (objValue != null)
                {
                    var strCd = objValue.ToString();
                    if (Const.ConstCls.FixedRowList.Contains(strCd))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        #endregion
    }
}
