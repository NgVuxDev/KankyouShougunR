// $Id: LogicClass.cs 43108 2015-02-26 00:37:53Z y-hosokawa@takumi-sys.co.jp $
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
using Shougun.Core.Master.GenchakuJikanHoshu.DAO;

namespace Shougun.Core.Master.GenchakuJikanHoshu
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicClass : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.GenchakuJikanHoshu.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        public bool isRegist = true;

        /// <summary>
        /// DTO
        /// </summary>
        //private DTOClass dto;

        /// <summary>
        /// 現着時間入力画面Form
        /// </summary>
        private GenchakuJikanHoshuForm form;

        /// <summary>
        /// 現着時間のエンティティ
        /// </summary>
        private M_GENCHAKU_TIME[] entitys;

        /// <summary>
        /// 現着時間のDao
        /// </summary>
        private DAOClass dao;

        /// <summary>
        /// システム情報のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム情報のエンティティ
        /// </summary>
        private M_SYS_INFO sysInfoEntity;

        public DataTable dtDetailList = new DataTable();

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        private GET_SYSDATEDao dateDao;
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_GENCHAKU_TIME SearchString { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public LogicClass(GenchakuJikanHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            //this.dto = new DTOClass();

            //this.dao = DaoInitUtility.GetComponent<IM_GENCHAKU_TIMEDao>();
            this.dao = DaoInitUtility.GetComponent<DAOClass>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            // 20151030 katen #12048 「システム日付」の基準作成、適用 start
            this.dateDao = DaoInitUtility.GetComponent<GET_SYSDATEDao>();//DBサーバ日付を取得するため作成したDao
            // 20151030 katen #12048 「システム日付」の基準作成、適用 end
            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 画面初期化処理

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

                // システム情報を取得し、初期値をセットする
                GetSysInfoInit();

                //前の表示条件を初期値に設定する
                if (Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED)
                {
                    this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                }

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_DBFIELD.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_DBFIELD.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                // 2014/01/23 oonaka add 検索条件保存内容不正対応 start
                // 選択された条件カラムにより、サブ情報、IMEを設定
                this.form.SetConditionControl(this.form.CONDITION_ITEM.Text);
                // 2014/01/23 oonaka add 検索条件保存内容不正対応 end

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M223", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }

            LogUtility.DebugMethodEnd(true);
            return true;
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

        #region ボタン初期化処理

        /// <summary>
        /// ボタン初期化処理
        /// </summary>
        private void ButtonInit()
        {
            LogUtility.DebugMethodStart();

            var buttonSetting = this.CreateButtonInfo();
            var parentForm = (MasterBaseForm)this.form.Parent;
            //var parentForm = (BusinessBaseForm)this.form.Parent;
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

            var buttonSetting = new ButtonSetting();
            var parentForm = (MasterBaseForm)this.form.Parent;
            //var parentForm = (BusinessBaseForm)this.form.Parent;
            var thisAssembly = Assembly.GetExecutingAssembly();

            LogUtility.DebugMethodEnd();

            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region イベント初期化処理

        /// <summary>
        /// イベントの初期化処理
        /// </summary>
        private void EventInit()
        {
            LogUtility.DebugMethodStart();

            var parentForm = (MasterBaseForm)this.form.Parent;
            //var parentForm = (BusinessBaseForm)this.form.Parent;
            // 削除ボタン(F4)イベント生成
            this.form.C_MasterRegist(parentForm.bt_func4);
            parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

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

            // 処理Noを非活性
            parentForm.txb_process.Enabled = false;

            LogUtility.DebugMethodEnd();
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
        public void GetSysInfoInit()
        {
            LogUtility.DebugMethodStart();

            // システム情報を取得し、初期値をセットする
            M_SYS_INFO[] sysInfo = sysInfoDao.GetAllData();
            if (sysInfo != null)
            {
                this.sysInfoEntity = sysInfo[0];
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = (bool)this.sysInfoEntity.ICHIRAN_HYOUJI_JOUKEN_DELETED;
            }

            LogUtility.DebugMethodEnd();
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
            this.isRegist = true;

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
                            if (this.entitys != null)
                            {
                                foreach (M_GENCHAKU_TIME genchakuTimeEntity in this.entitys)
                                {
                                    M_GENCHAKU_TIME entity = this.dao.GetDataByCd(genchakuTimeEntity.GENCHAKU_TIME_CD.ToString());

                                    if (entity != null)
                                    {
                                        this.dao.Update(genchakuTimeEntity);
                                    }
                                }
                            }
                            tran.Commit();
                        }
                        msgLogic.MessageBoxShow("I001", "選択データの削除");
                    }
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
                    LogUtility.Error(ex); //DBエラー
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

        #region 物理削除処理

        /// <summary>
        /// 物理削除処理
        /// </summary>
        public void PhysicalDelete()
        {
            LogUtility.DebugMethodStart();

            LogUtility.DebugMethodEnd();

            throw new NotImplementedException();
        }

        #endregion

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            //独自チェックの記述例を書く
            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            this.isRegist = true;

            try
            {
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    using (Transaction tran = new Transaction())
                    {
                        if (this.entitys != null)
                        {
                            foreach (M_GENCHAKU_TIME genchakuTimeEntity in this.entitys)
                            {
                                M_GENCHAKU_TIME entity = this.dao.GetDataByCd(genchakuTimeEntity.GENCHAKU_TIME_CD.ToString());

                                if (entity == null)
                                {
                                    this.dao.Insert(genchakuTimeEntity);
                                }
                                else
                                {
                                    this.dao.Update(genchakuTimeEntity);
                                }
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
                    this.form.errmessage.MessageBoxShow("E245", "");
                }
                this.isRegist = false;
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

            try
            {
                GetSysInfoInit();

                // 検索項目を初期値にセットする
                this.form.CONDITION_VALUE.Text = "";
                this.form.CONDITION_VALUE.DBFieldsName = "";
                this.form.CONDITION_VALUE.ItemDefinedTypes = "";
                this.form.CONDITION_ITEM.Text = "";
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Cancel", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }

            LogUtility.DebugMethodEnd();
            return true;
        }

        #endregion

        #region 現着時間CDの重複チェック

        /// <summary>
        /// 現着時間CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string genchaku_Time_Cd, out bool catchErr)
        {
            LogUtility.DebugMethodStart(genchaku_Time_Cd);
            catchErr = true;
            try
            {
                // 画面で現着時間CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells[Const.GenchakuJikanHoshuConstans.GENCHAKU_TIME_CD].Value.ToString().Equals(Convert.ToString(genchaku_Time_Cd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果で現着CD重複チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (genchaku_Time_Cd.Equals(dtDetailList.Rows[i][1].ToString()))
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }

                // DBで現着CD重複チェック
                M_GENCHAKU_TIME entity = this.dao.GetDataByCd(genchaku_Time_Cd.ToString());
                //M_GENCHAKU_TIME entity = this.dao.GetDataByCd(genchaku_Time_Cd);

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

        #region 更新処理

        /// <summary>
        /// 更新処理
        /// </summary>
        /// <param name="errorFlag"></param>
        public void Update(bool errorFlag)
        {
            throw new NotImplementedException();
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

        #region 優先順位重複チェック（不要）

        ///// <summary>
        ///// 優先順位の重複チェック処理
        ///// </summary>
        ///// <returns>true:重複なし, false:重複</returns>
        //public bool PriorityCheck()
        //{
        //    LogUtility.DebugMethodStart();

        //    // 結果の初期値
        //    var result = true;

        //    try
        //    {
        //        // チェック済リストの初期化
        //        var checkList = new List<string>();

        //        for (int i = 0; i < this.form.Ichiran.Rows.Count; i++)
        //        {
        //            if (this.form.Ichiran.Rows[i].Cells["GENCHAKU_PRIORITY"].Value == null)
        //            {
        //                // nullは次へ
        //                continue;
        //            }

        //            var target = this.form.Ichiran.Rows[i].Cells["GENCHAKU_PRIORITY"].Value.ToString();

        //            // 対象が既に1度チェックされていた場合、該当のセルを全て赤くする
        //            // （2度目以降は既に赤くなっているはずなので処理は行わない）
        //            if (checkList.Count(n => n == target) == 1)
        //            {
        //                foreach (DataGridViewRow row in this.form.Ichiran.Rows)
        //                {
        //                    if (row.Cells["GENCHAKU_PRIORITY"].Value == null)
        //                    {
        //                        // nullは次へ
        //                        continue;
        //                    }
        //                    if (row.Cells["GENCHAKU_PRIORITY"].Value.ToString() == target)
        //                    {
        //                        // 赤くする
        //                        ControlUtility.SetInputErrorOccuredForDgvCell(row.Cells["GENCHAKU_PRIORITY"], true);
        //                    }
        //                }

        //                // 重複
        //                result = false;
        //            }

        //            // チェックリストへ格納
        //            checkList.Add(target);
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Fatal(ex);
        //        throw ex;
        //    }
        //    finally
        //    {
        //        LogUtility.DebugMethodEnd(result);
        //    }
        //}

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
                var entityList = new M_GENCHAKU_TIME[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_GENCHAKU_TIME();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_GENCHAKU_TIME>(entityList);
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
                    if (column.ColumnName.Equals(Const.GenchakuJikanHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();
                preDt = GetCloneDataTable(dt);

                List<M_GENCHAKU_TIME> mGenchakuTimeiList = new List<M_GENCHAKU_TIME>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            if (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null
                                || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString()))
                            {
                                isSelect = true;
                                continue;
                            }

                            isSelect = true;
                            var m_GenchakuTimeEntity = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                            //var m_GenchakuTimeEntity = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_GenchakuTimeEntity, true);

                            m_GenchakuTimeEntity.DELETE_FLG = true;

                            mGenchakuTimeiList.Add(m_GenchakuTimeEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mGenchakuTimeiList.ToArray();
                }
                else
                {
                    // 変更分のみ取得
                    List<M_GENCHAKU_TIME> addList = new List<M_GENCHAKU_TIME>();

                    DataTable dtChange = new DataTable();

                    if (dt.GetChanges() == null)
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                    else
                    {
                        // 元の値から全く変化がなければ、 RowState を元の状態に戻す
                        foreach (DataRow row in dt.Rows)
                        {
                            if (!DataTableUtility.IsDataRowChanged(row))
                            {
                                row.AcceptChanges();
                            }
                        }

                        dtChange = dt.GetChanges();
                    }

                    var genchakuTimeEntityList = CreateEntityForDataGrid(dtChange);

                    for (int i = 0; i < genchakuTimeEntityList.Count; i++)
                    {
                        var genchakuTimeEntity = genchakuTimeEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            //if (this.form.Ichiran.Rows[j].Cells[Const.GenchakuJikanHoshuConstans.GENCHAKU_TIME_CD].Value.Equals(Convert.ToString(genchakuTimeEntity.GENCHAKU_TIME_CD)) &&
                            //         bool.Parse(this.form.Ichiran.Rows[j].Cells[Const.GenchakuJikanHoshuConstans.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                            if (this.form.Ichiran.Rows[j].Cells[Const.GenchakuJikanHoshuConstans.GENCHAKU_TIME_CD].Value != null)
                            {
                                if (Convert.ToString(genchakuTimeEntity.GENCHAKU_TIME_CD).Equals(this.form.Ichiran.Rows[j].Cells[Const.GenchakuJikanHoshuConstans.GENCHAKU_TIME_CD].Value.ToString()) &&
                                        bool.Parse(this.form.Ichiran.Rows[j].Cells[Const.GenchakuJikanHoshuConstans.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                                {
                                    isFind = true;
                                }
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(genchakuTimeEntity, false);
                                addList.Add(genchakuTimeEntity);
                                break;
                            }
                        }
                        this.form.Ichiran.DataSource = preDt;
                        this.entitys = addList.ToArray();
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("CreateEntity", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                catchErr = false;
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
        /// データグリッドビューから登録用Entity作成
        /// </summary>
        /// <param name="gridView"></param>
        /// <returns></returns>
        internal List<M_GENCHAKU_TIME> CreateEntityForDataGrid(DataTable gridView)
        {
            LogUtility.DebugMethodStart(gridView);

            var entityList = new List<M_GENCHAKU_TIME>();
            if (gridView == null)
            {
                LogUtility.DebugMethodEnd(entityList);
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
        /// データグリッドのエンティティ作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal M_GENCHAKU_TIME CreateEntityForDataGridRow(DataGridViewRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_GENCHAKU_TIME mGenchakuTime = new M_GENCHAKU_TIME();

            // GENCHAKU_TIME_CD
            if (!DBNull.Value.Equals(row.Cells["GENCHAKU_TIME_CD"].Value))
            {
                mGenchakuTime.GENCHAKU_TIME_CD = SqlInt16.Parse(row.Cells["GENCHAKU_TIME_CD"].Value.ToString());
            }

            // GENCHAKU_TIME_NAME
            if (!DBNull.Value.Equals(row.Cells["GENCHAKU_TIME_NAME"].Value))
            {
                mGenchakuTime.GENCHAKU_TIME_NAME = (string)row.Cells["GENCHAKU_TIME_NAME"].Value;
            }

            // GENCHAKU_TIME_NAME_RYAKU
            if (!DBNull.Value.Equals(row.Cells["GENCHAKU_TIME_NAME_RYAKU"].Value))
            {
                mGenchakuTime.GENCHAKU_TIME_NAME_RYAKU = (string)row.Cells["GENCHAKU_TIME_NAME_RYAKU"].Value;
            }

            // CONTENA_SHURUI_FURIGANA
            if (!DBNull.Value.Equals(row.Cells["GENCHAKU_PRIORITY"].Value))
            {
                mGenchakuTime.GENCHAKU_PRIORITY = SqlInt16.Parse(row.Cells["GENCHAKU_PRIORITY"].Value.ToString());
            }

            // GENCHAKU_BACK_COLOR
            if (!DBNull.Value.Equals(row.Cells["GENCHAKU_BACK_COLOR"].Value))
            {
                mGenchakuTime.GENCHAKU_BACK_COLOR = SqlInt32.Parse(row.Cells["GENCHAKU_BACK_COLOR"].Value.ToString());
            }

            // GENCHAKU_TIME_BIKOU
            if (!DBNull.Value.Equals(row.Cells["GENCHAKU_TIME_BIKOU"].Value))
            {
                mGenchakuTime.GENCHAKU_TIME_BIKOU = (string)row.Cells["GENCHAKU_TIME_BIKOU"].Value;
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row.Cells["chb_delete"].Value))
            {
                mGenchakuTime.DELETE_FLG = (Boolean)row.Cells["chb_delete"].Value;
            }
            else
            {
                mGenchakuTime.DELETE_FLG = false;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
            {
                mGenchakuTime.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
            }

            LogUtility.DebugMethodEnd(mGenchakuTime);

            return mGenchakuTime;
        }

        #endregion

        #region CreateEntityForDataRow

        /// <summary>
        /// エンティティの中身作成
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        internal M_GENCHAKU_TIME CreateEntityForDataRow(DataRow row)
        {
            LogUtility.DebugMethodStart(row);

            M_GENCHAKU_TIME mGenchakuTime = new M_GENCHAKU_TIME();

            // GENCHAKU_TIME_CD
            if (!DBNull.Value.Equals(row["GENCHAKU_TIME_CD"]))
            {
                mGenchakuTime.GENCHAKU_TIME_CD = SqlInt16.Parse(row["GENCHAKU_TIME_CD"].ToString());
            }

            // GENCHAKU_TIME_NAME
            if (!DBNull.Value.Equals(row["GENCHAKU_TIME_NAME"]))
            {
                mGenchakuTime.GENCHAKU_TIME_NAME = (string)row["GENCHAKU_TIME_NAME"];
            }

            // GENCHAKU_TIME_NAME_RYAKU
            if (!DBNull.Value.Equals(row["GENCHAKU_TIME_NAME_RYAKU"]))
            {
                mGenchakuTime.GENCHAKU_TIME_NAME_RYAKU = (string)row["GENCHAKU_TIME_NAME_RYAKU"];
            }

            // CONTENA_SHURUI_FURIGANA
            if (!DBNull.Value.Equals(row["GENCHAKU_PRIORITY"]))
            {
                mGenchakuTime.GENCHAKU_PRIORITY = SqlInt16.Parse(row["GENCHAKU_PRIORITY"].ToString());
            }

            // GENCHAKU_BACK_COLOR
            if (!DBNull.Value.Equals(row["GENCHAKU_BACK_COLOR"]))
            {
                mGenchakuTime.GENCHAKU_BACK_COLOR = SqlInt32.Parse(row["GENCHAKU_BACK_COLOR"].ToString());
            }

            // GENCHAKU_TIME_BIKOU
            if (!DBNull.Value.Equals(row["GENCHAKU_TIME_BIKOU"]))
            {
                mGenchakuTime.GENCHAKU_TIME_BIKOU = (string)row["GENCHAKU_TIME_BIKOU"];
            }

            // DELETE_FLG
            if (!DBNull.Value.Equals(row["DELETE_FLG"]))
            {
                mGenchakuTime.DELETE_FLG = (Boolean)row["DELETE_FLG"];
            }
            else
            {
                mGenchakuTime.DELETE_FLG = false;
            }

            // TIME_STAMP
            if (!DBNull.Value.Equals(row["TIME_STAMP"]))
            {
                mGenchakuTime.TIME_STAMP = (byte[])row["TIME_STAMP"];
            }

            LogUtility.DebugMethodEnd(mGenchakuTime);

            return mGenchakuTime;
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
                if (column.ColumnName.Equals(Const.GenchakuJikanHoshuConstans.TIME_STAMP))
                {
                    column.Unique = false;
                }
            }

            LogUtility.DebugMethodEnd();
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
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_GENCHAKU_TIME), this.form);
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
            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public Boolean SetSearchString()
        {
            LogUtility.DebugMethodStart();

            M_GENCHAKU_TIME entity = new M_GENCHAKU_TIME();
            string errorColumn = string.Empty;

            // このタイミングにはすでにCONDITION_TYPE、CONDITION_DBFIELDは設定済み。再度Settingsから読み込まない。
            // 2014/01/23 oonaka delete 検索条件保存内容不正対応 start
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
            // 2014/01/23 oonaka delete 検索条件保存内容不正対応 end

            //this.form.CONDITION_ITEM.Text = this.form.CONDITION_ITEM.Text.Replace("※", "");
            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
            {
                //if (!string.IsNullOrEmpty(this.form.CONDITION_TYPE.Text))
                if (!string.IsNullOrEmpty(this.form.CONDITION_DBFIELD.DBFieldsName))
                {
                    // 検索条件の設定
                    // 削除
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("DELETE_FLG"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.DELETE_FLG = true;
                            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.DELETE_FLG = false;
                            this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;
                        }
                    }

                    // 現着時間CD
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("GENCHAKU_TIME_CD"))
                    {
                        try
                        {
                            entity.GENCHAKU_TIME_CD = SqlInt16.Parse(this.form.CONDITION_VALUE.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E084", this.form.CONDITION_ITEM.Text);

                            form.CONDITION_VALUE.Focus();

                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
                    }

                    // 現着時間名
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("GENCHAKU_TIME_NAME"))
                    {
                        entity.GENCHAKU_TIME_NAME = this.form.CONDITION_VALUE.Text;
                    }

                    // 略称
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("GENCHAKU_TIME_NAME_RYAKU"))
                    {
                        entity.GENCHAKU_TIME_NAME_RYAKU = this.form.CONDITION_VALUE.Text;
                    }

                    // 優先順位
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("GENCHAKU_PRIORITY"))
                    {
                        try
                        {
                            entity.GENCHAKU_PRIORITY = SqlInt16.Parse(this.form.CONDITION_VALUE.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E012", "優先順位は数値");

                            form.CONDITION_VALUE.Focus();
                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
                    }

                    // 背景色
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("GENCHAKU_BACK_COLOR"))
                    {
                        try
                        {
                            entity.GENCHAKU_BACK_COLOR = SqlInt16.Parse(this.form.CONDITION_VALUE.Text);
                        }
                        catch (Exception ex)
                        {
                            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                            msgLogic.MessageBoxShow("E012", "背景色は数値");

                            form.CONDITION_VALUE.Focus();

                            LogUtility.DebugMethodEnd(false);
                            return false;
                        }
                    }

                    // 備考
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("GENCHAKU_TIME_BIKOU"))
                    {
                        entity.GENCHAKU_TIME_BIKOU = this.form.CONDITION_VALUE.Text;
                    }

                    // 更新者
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("UPDATE_USER"))
                    {
                        entity.UPDATE_USER = this.form.CONDITION_VALUE.Text;
                    }

                    // 更新日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("UPDATE_DATE"))
                    {
                        entity.SEARCH_UPDATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                    }

                    // 作成者
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("CREATE_USER"))
                    {
                        entity.CREATE_USER = this.form.CONDITION_VALUE.Text;
                    }

                    // 作成日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_DBFIELD.DBFieldsName.Equals("CREATE_DATE"))
                    {
                        entity.SEARCH_CREATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                    }
                }
            }

            this.SearchString = entity;

            LogUtility.DebugMethodEnd(true);

            return true;
        }

        #endregion

        #region データ取得処理

        /// <summary>
        /// 検索取得処理
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

                //Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                //Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_DBFIELD.Text;
                //Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_TYPE.Text;
                //Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

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
                var allEntityList = this.dao.GetAllData().Select(s => s.GENCHAKU_TIME_CD).Where(s => !s.Value.Equals(DBNull.Value)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["GENCHAKU_TIME_CD"]).Where(c => !string.IsNullOrEmpty(Convert.ToString(c.Value))).ToList().
                                        ForEach(c => c.ReadOnly = allEntityList.Contains((Int16)c.Value));
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

        // 20151030 katen #12048 「システム日付」の基準作成、適用 start
        internal DateTime getDBDateTime()
        {
            DateTime now = DateTime.Now;
            System.Data.DataTable dt = this.dateDao.GetDateForStringSql("SELECT GETDATE() AS DATE_TIME");//DBサーバ日付を取得する
            if (dt.Rows.Count > 0)
            {
                now = Convert.ToDateTime(dt.Rows[0]["DATE_TIME"]);
            }
            return now;
        }
        // 20151030 katen #12048 「システム日付」の基準作成、適用 end
    }
}