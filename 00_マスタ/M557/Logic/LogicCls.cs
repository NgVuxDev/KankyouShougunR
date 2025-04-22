// $Id: LogicCls.cs 30248 2014-09-17 05:05:35Z sanbongi $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
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
using Shougun.Core.Master.DenshiShinSeiKeiroName.APP;
using M557Dao = Shougun.Core.Master.DenshiShinSeiKeiroName.Dao;

namespace Shougun.Core.Master.DenshiShinSeiKeiroName.Logic
{
    /// <summary>
    /// 申請経路名入力画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.DenshiShinSeiKeiroName.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        /// <summary>
        /// 申請経路名入力画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_DENSHI_SHINSEI_ROUTE_NAME[] entitys;

        /// <summary>
        /// 申請経路名のDao
        /// </summary>
        private M557Dao.DaoCls dao;

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
        /// 検索条件
        /// </summary>
        public M_DENSHI_SHINSEI_ROUTE_NAME SearchString { get; set; }

        /// <summary>
        /// dtDetailList
        /// </summary>
        public DataTable dtDetailList = new DataTable();

        /// <summary>
        /// 登録・更新・削除処理の成否
        /// </summary>
        public bool isRegist { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public LogicCls(UIForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<M557Dao.DaoCls>();
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
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();

                //前の表示条件を初期値に設定する
                if (Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED)
                {
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M557", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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
            int count = 0;
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;

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
        public bool CreateEntity(bool isDelete, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var entityList = new M_DENSHI_SHINSEI_ROUTE_NAME[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_DENSHI_SHINSEI_ROUTE_NAME();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_DENSHI_SHINSEI_ROUTE_NAME>(entityList);
                DataTable dt = this.form.Ichiran.DataSource as DataTable;

                if (dt == null || dt.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(false, catchErr);
                    return false;
                }

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

                int count = 0;
                List<M_DENSHI_SHINSEI_ROUTE_NAME> mDenshiShinseiRouteNameList = new List<M_DENSHI_SHINSEI_ROUTE_NAME>();
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
                            var m_DenshiShinseiRouteNameEntity = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_DenshiShinseiRouteNameEntity, true);
                            m_DenshiShinseiRouteNameEntity.DELETE_FLG = true;
                            mDenshiShinseiRouteNameList.Add(m_DenshiShinseiRouteNameEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mDenshiShinseiRouteNameList.ToArray();
                    if (this.entitys.Length == 0 && count == 0)
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }
                else
                {
                    // 変更分のみ取得
                    List<M_DENSHI_SHINSEI_ROUTE_NAME> addList = new List<M_DENSHI_SHINSEI_ROUTE_NAME>();
                    if (dt.GetChanges() == null)
                    {
                        this.entitys = new List<M_DENSHI_SHINSEI_ROUTE_NAME>().ToArray();
                        LogUtility.DebugMethodEnd(false, catchErr);
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
                        LogUtility.DebugMethodEnd(true, catchErr);
                        return true;
                    }

                    var rows = dt.GetChanges().Select("DELETE_FLG = 0 and DENSHI_SHINSEI_ROUTE_CD IS NOT NULL");

                    if (rows.Length == 0)
                    {
                        this.entitys = new List<M_DENSHI_SHINSEI_ROUTE_NAME>().ToArray();
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }

                    var DenshiShinseiRouteNameEntityList = CreateEntityForDataGrid(rows);
                    for (int i = 0; i < DenshiShinseiRouteNameEntityList.Count; i++)
                    {
                        var DenshiShinseiRouteNameEntity = DenshiShinseiRouteNameEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;

                            if (this.form.Ichiran.Rows[j].Cells["DENSHI_SHINSEI_ROUTE_CD"].Value.ToString().PadLeft(2, '0').Equals(DenshiShinseiRouteNameEntity.DENSHI_SHINSEI_ROUTE_CD) &&
                                bool.Parse(this.form.Ichiran.Rows[j].Cells["DELETE_FLG"].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(DenshiShinseiRouteNameEntity, false);
                                addList.Add(DenshiShinseiRouteNameEntity);
                                break;
                            }
                        }
                        if (0 < addList.Count)
                        {
                            this.entitys = addList.ToArray();
                        }
                        else
                        {
                            this.entitys = new List<M_DENSHI_SHINSEI_ROUTE_NAME>().ToArray();
                            LogUtility.DebugMethodEnd(false, catchErr);
                            return false;
                        }
                    }
                }

                LogUtility.DebugMethodEnd(true, catchErr);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                catchErr = false;
                LogUtility.DebugMethodEnd(false, catchErr);
                return false;
            }
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

                InitCondition();
                DataTable dt = new DataTable();
                dt = this.form.Ichiran.DataSource as DataTable;
                if (dt != null)
                {
                    dt.Clear();
                }
                this.form.Ichiran.DataSource = dt;
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
        /// 申請経路名CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string strDenshiShinseiRouteCd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(strDenshiShinseiRouteCd);

                // 画面で申請経路CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    string strCD1 = this.form.Ichiran.Rows[i].Cells["DENSHI_SHINSEI_ROUTE_CD"].Value.ToString();
                    if (strCD1.Equals(Convert.ToString(strDenshiShinseiRouteCd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果でPK重複チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (strDenshiShinseiRouteCd.Equals(dtDetailList.Rows[i]["DENSHI_SHINSEI_ROUTE_CD"].ToString()))
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }

                // DBでPK重複チェック
                M_DENSHI_SHINSEI_ROUTE_NAME entity = this.dao.GetDataByCd(strDenshiShinseiRouteCd);

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

        /// <summary>
        /// CSV
        /// </summary>
        public void CSV()
        {
            LogUtility.DebugMethodStart();
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            if (msgLogic.MessageBoxShow("C012") == DialogResult.Yes)
            {
                CSVExport csvExport = new CSVExport();
                csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_DENSHI_SHINSEI_ROUTE_NAME), this.form);
            }
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
                //独自チェックの記述例を書く

                this.isRegist = false;
                //エラーではない場合登録処理を行う
                if (errorFlag)
                {
                    return;
                }
                // トランザクション開始
                using (var tran = new Transaction())
                {
                    if (this.entitys != null)
                    {
                        foreach (M_DENSHI_SHINSEI_ROUTE_NAME denshiShinseiRouteNameEntity in this.entitys)
                        {
                            M_DENSHI_SHINSEI_ROUTE_NAME entity = this.dao.GetDataByCd(denshiShinseiRouteNameEntity.DENSHI_SHINSEI_ROUTE_CD.ToString());
                            if (entity == null)
                            {
                                this.dao.Insert(denshiShinseiRouteNameEntity);
                            }
                            else
                            {
                                this.dao.Update(denshiShinseiRouteNameEntity);
                            }
                        }
                    }
                    // トランザクション終了
                    tran.Commit();
                }
                this.isRegist = true;
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
                    LogUtility.Error(ex); //DBエラー
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245");
                }
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

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                if (!isSelect)
                {
                    msgLogic.MessageBoxShow("E075", "削除");
                }
                else
                {
                    var result = msgLogic.MessageBoxShow("C021");
                    if (result == DialogResult.Yes)
                    {
                        // トランザクション開始
                        using (var tran = new Transaction())
                        {
                            if (this.entitys != null)
                            {
                                foreach (M_DENSHI_SHINSEI_ROUTE_NAME denshiShineseiRouteNameEntity in this.entitys)
                                {
                                    M_DENSHI_SHINSEI_ROUTE_NAME entity = this.dao.GetDataByCd(denshiShineseiRouteNameEntity.DENSHI_SHINSEI_ROUTE_CD.ToString());
                                    if (entity != null)
                                    {
                                        this.dao.Update(denshiShineseiRouteNameEntity);
                                    }
                                }
                            }
                            // トランザクション終了
                            tran.Commit();
                        }

                        msgLogic.MessageBoxShow("I001", "削除");
                    }
                }
                this.isRegist = true;
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
                    LogUtility.Error(ex); //DBエラー
                    this.form.errmessage.MessageBoxShow("E093");
                }
                else
                {
                    LogUtility.Error(ex); //その他はエラー
                    this.form.errmessage.MessageBoxShow("E245");
                }
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

            LogicCls localLogic = other as LogicCls;
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
            try
            {
                LogUtility.DebugMethodStart();

                M_DENSHI_SHINSEI_ROUTE_NAME entity = new M_DENSHI_SHINSEI_ROUTE_NAME();

                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
                {
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                    {
                        //検索条件の設定
                        entity.SetValue(this.form.CONDITION_VALUE);
                    }
                }

                this.SearchString = entity;
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

        /// <summary>
        /// 検索文字列が検索項目に対して不正な文字かのチェックを行う
        /// </summary>
        /// <returns>True：正常　False：不正</returns>
        public bool CheckSearchString()
        {
            // SetSearchStringメソッド中のentity.SetValueで値によってはFormatでシステムエラーになるため、
            // ここで一度チェックをする。
            // 現在は、経路CDのみエラーが発生するため、経路CDのみチェックを行う。
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
            try
            {
                LogUtility.DebugMethodStart();

                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
                this.form.CONDITION_ITEM.Text = string.Empty;
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
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

        #region CreateEntityForDataGrid

        /// <summary>
        /// CreateEntityForDataGrid
        /// </summary>
        /// <returns>
        /// entityList
        /// </returns>
        private List<M_DENSHI_SHINSEI_ROUTE_NAME> CreateEntityForDataGrid(IEnumerable<DataRow> rows)
        {
            try
            {
                LogUtility.DebugMethodStart(rows);

                var entityList = rows.Select(r => CreateEntityForDataRow(r)).ToList();

                LogUtility.DebugMethodEnd(entityList);
                return entityList;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGrid", ex);
                throw;
            }
        }

        #endregion

        #region CreateEntityForDataRow

        /// <summary>
        /// CreateEntityForDataRow
        /// </summary>
        /// <returns>
        /// mBupanZaikoHokanBasyo
        /// </returns>
        private M_DENSHI_SHINSEI_ROUTE_NAME CreateEntityForDataRow(DataRow row)
        {
            try
            {
                LogUtility.DebugMethodStart(row);

                M_DENSHI_SHINSEI_ROUTE_NAME mDenshiShinseiRouteName = new M_DENSHI_SHINSEI_ROUTE_NAME();

                // DELETE_FLG
                if (!DBNull.Value.Equals(row["DELETE_FLG"]))
                {
                    mDenshiShinseiRouteName.DELETE_FLG = (Boolean)row["DELETE_FLG"];
                }
                else
                {
                    mDenshiShinseiRouteName.DELETE_FLG = false;
                }

                // DENSHI_SHINSEI_ROUTE_CD
                if (!DBNull.Value.Equals(row["DENSHI_SHINSEI_ROUTE_CD"]))
                {
                    mDenshiShinseiRouteName.DENSHI_SHINSEI_ROUTE_CD = row["DENSHI_SHINSEI_ROUTE_CD"].ToString().PadLeft(2, '0');
                }

                // DENSHI_SHINSEI_ROUTE_NAME
                if (!DBNull.Value.Equals(row["DENSHI_SHINSEI_ROUTE_NAME"]))
                {
                    mDenshiShinseiRouteName.DENSHI_SHINSEI_ROUTE_NAME = (string)row["DENSHI_SHINSEI_ROUTE_NAME"];
                }

                // UNIT_CD
                if (!DBNull.Value.Equals(row["DENSHI_SHINSEI_ROUTE_FURIGANA"]))
                {
                    mDenshiShinseiRouteName.DENSHI_SHINSEI_ROUTE_FURIGANA = (string)row["DENSHI_SHINSEI_ROUTE_FURIGANA"];
                }

                // CREATE_USER
                if (!DBNull.Value.Equals(row["CREATE_USER"]))
                {
                    mDenshiShinseiRouteName.CREATE_USER = (string)row["CREATE_USER"];
                }

                // UPDATE_USER
                if (!DBNull.Value.Equals(row["UPDATE_USER"]))
                {
                    mDenshiShinseiRouteName.UPDATE_USER = (string)row["UPDATE_USER"];
                }

                // UPDATE_DATE
                if (!string.IsNullOrEmpty(row["UPDATE_DATE"].ToString()))
                {
                    mDenshiShinseiRouteName.UPDATE_DATE = (DateTime)row["UPDATE_DATE"];
                }

                // CREATE_DATE
                if (!string.IsNullOrEmpty(row["CREATE_DATE"].ToString()))
                {
                    mDenshiShinseiRouteName.CREATE_DATE = (DateTime)row["CREATE_DATE"];
                }

                // TIME_STAMP
                if (!DBNull.Value.Equals(row["TIME_STAMP"]))
                {
                    mDenshiShinseiRouteName.TIME_STAMP = (byte[])row["TIME_STAMP"];
                }

                LogUtility.DebugMethodEnd(mDenshiShinseiRouteName);
                return mDenshiShinseiRouteName;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataRow", ex);
                throw;
            }
        }

        #endregion

        #region CreateEntityForDataGridRow

        /// <summary>
        /// CreateEntityForDataGridRow
        /// </summary>
        /// <returns>
        /// mBupanZaikoHokanBasyo
        /// </returns>
        internal M_DENSHI_SHINSEI_ROUTE_NAME CreateEntityForDataGridRow(DataGridViewRow row)
        {
            try
            {
                LogUtility.DebugMethodStart(row);

                M_DENSHI_SHINSEI_ROUTE_NAME mDenshiShinseiRouteName = new M_DENSHI_SHINSEI_ROUTE_NAME();

                // DELETE_FLG
                if (!DBNull.Value.Equals(row.Cells["DELETE_FLG"].Value))
                {
                    mDenshiShinseiRouteName.DELETE_FLG = (Boolean)row.Cells["DELETE_FLG"].Value;
                }
                else
                {
                    mDenshiShinseiRouteName.DELETE_FLG = false;
                }

                // DENSHI_SHINSEI_ROUTE_CD
                if (!DBNull.Value.Equals(row.Cells["DENSHI_SHINSEI_ROUTE_CD"].Value))
                {
                    mDenshiShinseiRouteName.DENSHI_SHINSEI_ROUTE_CD = row.Cells["DENSHI_SHINSEI_ROUTE_CD"].Value.ToString().PadLeft(2, '0');
                }

                // DENSHI_SHINSEI_ROUTE_NAME
                if (!DBNull.Value.Equals(row.Cells["DENSHI_SHINSEI_ROUTE_NAME"].Value))
                {
                    mDenshiShinseiRouteName.DENSHI_SHINSEI_ROUTE_NAME = (string)row.Cells["DENSHI_SHINSEI_ROUTE_NAME"].Value;
                }

                // UNIT_CD
                if (!DBNull.Value.Equals(row.Cells["DENSHI_SHINSEI_ROUTE_FURIGANA"].Value))
                {
                    mDenshiShinseiRouteName.DENSHI_SHINSEI_ROUTE_FURIGANA = (string)row.Cells["DENSHI_SHINSEI_ROUTE_FURIGANA"].Value;
                }

                // CREATE_USER
                if (!DBNull.Value.Equals(row.Cells["CREATE_USER"].Value))
                {
                    mDenshiShinseiRouteName.CREATE_USER = (string)row.Cells["CREATE_USER"].Value;
                }

                // UPDATE_USER
                if (!DBNull.Value.Equals(row.Cells["UPDATE_USER"].Value))
                {
                    mDenshiShinseiRouteName.UPDATE_USER = (string)row.Cells["UPDATE_USER"].Value;
                }

                // UPDATE_DATE
                if (!string.IsNullOrEmpty(row.Cells["UPDATE_DATE"].Value.ToString()))
                {
                    mDenshiShinseiRouteName.UPDATE_DATE = (DateTime)row.Cells["UPDATE_DATE"].Value;
                }

                // CREATE_DATE
                if (!string.IsNullOrEmpty(row.Cells["CREATE_DATE"].Value.ToString()))
                {
                    mDenshiShinseiRouteName.CREATE_DATE = (DateTime)row.Cells["CREATE_DATE"].Value;
                }

                // TIME_STAMP
                if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
                {
                    mDenshiShinseiRouteName.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
                }

                LogUtility.DebugMethodEnd(mDenshiShinseiRouteName);
                return mDenshiShinseiRouteName;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGridRow", ex);
                throw;
            }
        }

        #endregion

        #region システム情報を取得し、初期値をセットする

        /// <summary>
        ///  システム情報を取得し、初期値をセットする
        /// </summary>
        private void GetSysInfoInit()
        {
            try
            {
                LogUtility.DebugMethodStart();

                // システム情報を取得し、初期値をセットする
                M_SYS_INFO sysInfo = this.daoSysInfo.GetAllDataForCode("0");
                if (sysInfo != null)
                {
                    this.entitySysInfo = sysInfo;
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED;
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
                    if (column.ColumnName.Equals("TIME_STAMP"))
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

        #region 検索条件初期化

        /// <summary>
        /// 取り消し初期化
        /// </summary>
        public void InitCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.form.CONDITION_VALUE.Text = string.Empty;
                this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
                this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
                this.form.CONDITION_ITEM.Text = string.Empty;

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();
            }
            catch (Exception ex)
            {
                LogUtility.Error("InitCondition", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        #endregion

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.DENSHI_SHINSEI_ROUTE_CD).Where(s => !s.Equals(DBNull.Value)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["DENSHI_SHINSEI_ROUTE_CD"]).Where(c => !string.IsNullOrEmpty(Convert.ToString(c.Value))).ToList().
                                        ForEach(c => c.ReadOnly = allEntityList.Contains(c.Value));
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("EditableToPrimaryKey", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
            return true;
        }
    }
}