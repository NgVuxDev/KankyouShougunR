// $Id: LogicCls.cs 38032 2014-12-23 07:44:08Z fangjk@oec-h.com $
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
using Shougun.Core.Master.ManiFestTeHaiHoshu.APP;
using Shougun.Core.Master.ManiFestTeHaiHoshu.Const;
using Shougun.Core.Master.ManiFestTeHaiHoshu.Dao;
using Seasar.Quill.Util;
using Seasar.Framework.Exceptions;

namespace Shougun.Core.Master.ManiFestTeHaiHoshu.Logic
{
    /// <summary>
    /// ビジネスロジック
    /// </summary>
    internal class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.ManiFestTeHaiHoshu.Setting.ButtonSetting.xml";

        private bool isSelect = false;

        /// <summary>
        /// マニフェスト手配入力画面Form
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
        /// マニフェスト手配のエンティティ
        /// </summary>
        private M_MANIFEST_TEHAI[] entitys;

        /// <summary>
        /// マニフェスト手配のDao
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
        public M_MANIFEST_TEHAI SearchString { get; set; }

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
        public bool WindowInit()
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

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    // システム情報を取得し、初期値をセットする
                    this.GetSysInfoInit();
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("WindowInit", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
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
                //parentForm.bt_func8.CausesValidation = false;

                //登録ボタン(F9)イベント生成
                this.form.C_Regist(parentForm.bt_func9);
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

                M_MANIFEST_TEHAI entity = new M_MANIFEST_TEHAI();

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

                        if (this.form.CONDITION_VALUE.DBFieldsName.Equals("MANIFEST_TEHAI_CD")
                            && !string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
                        {
                            if (this.form.CONDITION_VALUE.Text.Length > 2
                            || !IsNumeric(this.form.CONDITION_VALUE.Text))
                            {
                                return false;
                            }
                        }
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

        #region 数値判定処理
        /// <summary>
        /// 数値判定処理
        /// </summary>
        public static bool IsNumeric(string stTarget)
        {
            decimal dNullable;

            return decimal.TryParse(
                stTarget,
                System.Globalization.NumberStyles.Any,
                null,
                out dNullable
            );
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
                this.form.msgLogic.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GetSysInfoInit", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            LogUtility.DebugMethodEnd(true);
            return true;
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
                            foreach (M_MANIFEST_TEHAI manifesttehaiEntity in this.entitys)
                            {
                                if (manifesttehaiEntity.MANIFEST_TEHAI_CD.IsNull)
                                {
                                    msgLogic.MessageBoxShow("E075", "削除");
                                    return;
                                }
                                M_MANIFEST_TEHAI entity = this.dao.GetDataByCd(manifesttehaiEntity.MANIFEST_TEHAI_CD.ToString());
                                if (entity != null)
                                {
                                    this.dao.Update(manifesttehaiEntity);
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
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_MANIFEST_TEHAI), this.form);
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
                this.form.CONDITION_ITEM.Focus();
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
            try
            {
                LogUtility.DebugMethodStart();

                // エラーの場合、０件を戻る
                if (!SetSearchString())
                {
                    return 0;
                }

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                dtDetailList = this.SearchResult.Copy();

                int count = 0;
                if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
                {
                    count = 1;
                }

                return count;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("Search", ex1);
                this.form.msgLogic.MessageBoxShow("E093", "");
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
                return -1;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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
                        foreach (M_MANIFEST_TEHAI manifesttehaiEntity in this.entitys)
                        {
                            M_MANIFEST_TEHAI entity = this.dao.GetDataByCd(manifesttehaiEntity.MANIFEST_TEHAI_CD.ToString());
                            if (entity == null)
                            {
                                this.dao.Insert(manifesttehaiEntity);
                            }
                            else
                            {
                                this.dao.Update(manifesttehaiEntity);
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
                if (!GetSysInfoInit())
                {
                    return;
                }

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

        #region マニフェスト手配CDの重複チェック
        /// <summary>
        /// マニフェスト手配CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string manifest_Tehai_Cd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(manifest_Tehai_Cd);

                // 画面でマニフェスト手配CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    if (this.form.Ichiran.Rows[i].Cells[ConstCls.MANIFEST_TEHAI_CD].Value.Equals(Int16.Parse(manifest_Tehai_Cd)))
                    {
                        recCount++;
                    }
                }

                if (recCount > 1)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

                // 検索結果でマニフェスト手配CD重複チェック
                for (int i = 0; i < dtDetailList.Rows.Count; i++)
                {
                    if (Int16.Parse(manifest_Tehai_Cd).Equals(dtDetailList.Rows[i][ConstCls.MANIFEST_TEHAI_CD]))
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }

                }

                // DBでマニフェスト手配CD重複チェック
                M_MANIFEST_TEHAI entity = this.dao.GetDataByCd(manifest_Tehai_Cd);

                if (entity != null)
                {
                    LogUtility.DebugMethodEnd(true, catchErr);
                    return true;
                }

            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
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
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var entityList = new M_MANIFEST_TEHAI[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_MANIFEST_TEHAI();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_MANIFEST_TEHAI>(entityList);
                DataTable dt = this.form.Ichiran.DataSource as DataTable;

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.ConstCls.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                List<M_MANIFEST_TEHAI> mManifestTehaiList = new List<M_MANIFEST_TEHAI>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            isSelect = true;
                            var m_ManifestTehaiEntity = CreateEntityForDataGridRow(dt.Rows[i]);
                            var dataBinderEntry = new DataBinderLogic<M_MANIFEST_TEHAI>(m_ManifestTehaiEntity);
                            dataBinderEntry.SetSystemProperty(m_ManifestTehaiEntity, true);
                            m_ManifestTehaiEntity.DELETE_FLG = true;
                            mManifestTehaiList.Add(m_ManifestTehaiEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mManifestTehaiList.ToArray();
                    if (this.entitys.Length == 0)
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }
                else
                {
                    // 変更分のみ取得
                    List<M_MANIFEST_TEHAI> addList = new List<M_MANIFEST_TEHAI>();
                    if (dt.GetChanges() == null)
                    {
                        this.entitys = new List<M_MANIFEST_TEHAI>().ToArray();
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }

                    // 変更したデータ取得
                    var rows = dt.GetChanges().Select("DELETE_FLG = 0");

                    // データ変更なし
                    if (rows.Length == 0)
                    {
                        this.entitys = new List<M_MANIFEST_TEHAI>().ToArray();
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }

                    var manifestTehaiEntityList = CreateEntityForDataGrid(rows);
                    for (int i = 0; i < manifestTehaiEntityList.Count; i++)
                    {
                        var manifestTehaiEntity = manifestTehaiEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            if (null != this.form.Ichiran.Rows[j].Cells[Const.ConstCls.MANIFEST_TEHAI_CD].Value)
                            {
                                if (this.form.Ichiran.Rows[j].Cells[Const.ConstCls.MANIFEST_TEHAI_CD].Value.ToString().Equals(Convert.ToString(manifestTehaiEntity.MANIFEST_TEHAI_CD)) &&
                                bool.Parse(this.form.Ichiran.Rows[j].Cells[Const.ConstCls.DELETE_FLG].FormattedValue.ToString()) == isDelete)
                                {
                                    isFind = true;
                                }
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(manifestTehaiEntity, false);
                                var dataBinderEntry = new DataBinderLogic<M_MANIFEST_TEHAI>(manifestTehaiEntity);
                                dataBinderEntry.SetSystemProperty(manifestTehaiEntity, false);
                                addList.Add(manifestTehaiEntity);
                                break;
                            }
                        }
                        this.entitys = addList.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.msgLogic.MessageBoxShow("E245", "");
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
        internal List<M_MANIFEST_TEHAI> CreateEntityForDataGrid(IEnumerable<DataRow> rows)
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
        /// mManifestTehai
        /// </returns>
        internal M_MANIFEST_TEHAI CreateEntityForDataGridRow(DataRow row)
        {
            try
            {
                LogUtility.DebugMethodStart(row);

                M_MANIFEST_TEHAI mManifestTehai = new M_MANIFEST_TEHAI();
                // MANIFEST_TEHAI_CD
                if (!DBNull.Value.Equals(row["MANIFEST_TEHAI_CD"]))
                {
                    mManifestTehai.MANIFEST_TEHAI_CD = row.Field<Int16>("MANIFEST_TEHAI_CD");
                }

                // MANIFEST_TEHAI_NAME
                if (!DBNull.Value.Equals(row.Field<string>("MANIFEST_TEHAI_NAME")))
                {
                    mManifestTehai.MANIFEST_TEHAI_NAME = row.Field<string>("MANIFEST_TEHAI_NAME");
                }

                // MANIFEST_TEHAI_NAME_RYAKU
                if (!DBNull.Value.Equals(row.Field<string>("MANIFEST_TEHAI_NAME_RYAKU")))
                {
                    mManifestTehai.MANIFEST_TEHAI_NAME_RYAKU = row.Field<string>("MANIFEST_TEHAI_NAME_RYAKU");
                }

                // MANIFEST_TEHAI_BIKOU
                if (!DBNull.Value.Equals(row.Field<string>("MANIFEST_TEHAI_BIKOU")))
                {
                    mManifestTehai.MANIFEST_TEHAI_BIKOU = row.Field<string>("MANIFEST_TEHAI_BIKOU");
                }

                // DELETE_FLG
                if (!DBNull.Value.Equals(row.Field<bool>("DELETE_FLG")))
                {
                    mManifestTehai.DELETE_FLG = row.Field<bool>("DELETE_FLG");
                }
                else
                {
                    mManifestTehai.DELETE_FLG = false;
                }

                // TIME_STAMP
                if (!DBNull.Value.Equals(row.Field<byte[]>("TIME_STAMP")))
                {
                    mManifestTehai.TIME_STAMP = row.Field<byte[]>("TIME_STAMP");
                }

                return mManifestTehai;
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
                    if (column.ColumnName.Equals(Const.ConstCls.TIME_STAMP))
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
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            var MANIFEST_TEHAI_CD = string.Empty;
            string[] strList;

            foreach (System.Windows.Forms.DataGridViewRow gcRwos in this.form.Ichiran.Rows)
            {
                if (gcRwos.Cells["DELETE_FLG"].Value != null && gcRwos.Cells["DELETE_FLG"].Value.ToString() == "True")
                {
                    MANIFEST_TEHAI_CD += gcRwos.Cells["MANIFEST_TEHAI_CD"].Value.ToString() + ",";
                }
            }

            if (!string.IsNullOrEmpty(MANIFEST_TEHAI_CD))
            {
                strList = MANIFEST_TEHAI_CD.Split(',');
                DataTable dtTable = dao.GetDataBySqlFileCheck(strList);
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    string strName = string.Empty;

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strName += "\n" + dr["NAME"].ToString();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E258", "マニフェスト手配", "マニフェスト手配CD", strName);

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

        public virtual void Regist(bool errorFlag)
        {
            throw new NotImplementedException();
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
                var objValue = row.Cells[Const.ConstCls.MANIFEST_TEHAI_CD].Value;
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
