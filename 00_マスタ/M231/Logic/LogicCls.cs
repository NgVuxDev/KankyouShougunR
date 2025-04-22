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
using Shougun.Core.Master.CourseNameHoshu.APP;
using Shougun.Core.Master.CourseNameHoshu.DAO;
using Shougun.Core.Master.CourseNameHoshu.DTO;

namespace Shougun.Core.Master.CourseNameHoshu.Logic
{
    /// <summary>
    /// コース名入力画面のビジネスロジック
    /// </summary>
    public class LogicCls : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.CourseNameHoshu.Setting.ButtonSetting.xml";

        private bool isSelect = false;
        public bool isRegist = true;

        /// <summary>
        /// コース名画面Form
        /// </summary>
        private UIForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// コース名のエンティティ
        /// </summary>
        private M_COURSE_NAME[] entitys;

        /// <summary>
        /// コース名のDao
        /// </summary>
        private DaoCls dao;

        /// <summary>
        /// 拠点名dao
        /// </summary>
        private IM_KYOTENDao kyotenDao;

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
        public CourseNameDto SearchString { get; set; }

        /// <summary>
        /// 拠点名
        /// </summary>
        public M_KYOTEN SearchKyoTenMei { get; set; }

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
            try
            {
                LogUtility.DebugMethodStart(targetForm);

                this.form = targetForm;
                this.dao = DaoInitUtility.GetComponent<DaoCls>();
                this.kyotenDao = DaoInitUtility.GetComponent<IM_KYOTENDao>();
                this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            }
            catch (Exception ex)
            {
                LogUtility.Error("LogicCls", ex);
                throw;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
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

                // 権限チェック
                if (!r_framework.Authority.Manager.CheckAuthority("M231", WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    this.DispReferenceMode();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
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

                LogUtility.DebugMethodEnd(buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath));

                return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateButtonInfo", ex);
                throw;
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

                // 処理No制御
                parentForm.txb_process.Enabled = false;
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
                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = false;

                //DataTable dt = this.form.Ichiran.DataSource as DataTable;
                //dt.Clear();
                //this.form.Ichiran.DataSource = dt;
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

        /// <summary>
        /// 取り消し
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

                //DataTable dt = this.form.Ichiran.DataSource as DataTable;
                //dt.Clear();
                //this.form.Ichiran.DataSource = dt;
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

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        public void SetSearchString()
        {
            try
            {
                LogUtility.DebugMethodStart();

                CourseNameDto entity = new CourseNameDto();

                // CONDITION_DBFIELD
                if (string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName) &&
                    !string.IsNullOrEmpty(Properties.Settings.Default.ConditionValue_DBFieldsName))
                {
                    this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                }

                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text))
                {
                    // 検索条件の設定
                    // 削除
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("DELETE_FLG"))
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

                    // コース名CD
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("COURSE_NAME_CD"))
                    {
                        entity.COURSE_NAME_CD = this.form.CONDITION_VALUE.Text;
                    }

                    // コース名
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("COURSE_NAME"))
                    {
                        entity.COURSE_NAME = this.form.CONDITION_VALUE.Text;
                    }

                    // 略称
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("COURSE_NAME_RYAKU"))
                    {
                        entity.COURSE_NAME_RYAKU = this.form.CONDITION_VALUE.Text;
                    }

                    // フリガナ
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("COURSE_NAME_FURIGANA"))
                    {
                        entity.COURSE_NAME_FURIGANA = this.form.CONDITION_VALUE.Text;
                    }

                    // 拠点CD
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("KYOTEN_CD"))
                    {
                        entity.KYOTEN_CD = this.form.CONDITION_VALUE.Text;
                    }

                    // 拠点略称名
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("KYOTEN_NAME_RYAKU"))
                    {
                        entity.KYOTEN_NAME_RYAKU = this.form.CONDITION_VALUE.Text;
                    }

                    // 月曜日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("MONDAY"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.MONDAY = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.MONDAY = false;
                        }
                    }

                    // 火曜日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("TUESDAY"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.TUESDAY = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.TUESDAY = false;
                        }
                    }

                    // 水曜日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("WEDNESDAY"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.WEDNESDAY = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.WEDNESDAY = false;
                        }
                    }

                    // 木曜日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("THURSDAY"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.THURSDAY = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.THURSDAY = false;
                        }
                    }

                    // 金曜日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("FRIDAY"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.FRIDAY = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.FRIDAY = false;
                        }
                    }

                    // 土曜日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("SATURDAY"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.SATURDAY = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.SATURDAY = false;
                        }
                    }

                    // 日曜日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("SUNDAY"))
                    {
                        if ("TRUE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "1".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "１".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.SUNDAY = true;
                        }
                        if ("FALSE".Equals(this.form.CONDITION_VALUE.Text.Trim().ToUpper()) ||
                            "0".Equals(this.form.CONDITION_VALUE.Text.Trim()) ||
                            "０".Equals(this.form.CONDITION_VALUE.Text.Trim()))
                        {
                            entity.SUNDAY = false;
                        }
                    }

                    // コース名備考
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("COURSE_NAME_BIKOU"))
                    {
                        entity.COURSE_NAME_BIKOU = this.form.CONDITION_VALUE.Text;
                    }

                    // 更新者
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("UPDATE_USER"))
                    {
                        entity.UPDATE_USER = this.form.CONDITION_VALUE.Text;
                    }

                    // 更新日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("UPDATE_DATE"))
                    {
                        entity.UPDATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
                    }

                    // 作成者
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("CREATE_USER"))
                    {
                        entity.CREATE_USER = this.form.CONDITION_VALUE.Text;
                    }

                    // 作成日
                    if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.Text) &&
                        this.form.CONDITION_VALUE.DBFieldsName.Equals("CREATE_DATE"))
                    {
                        entity.CREATE_DATE = this.form.CONDITION_VALUE.Text.Replace('/', '-');
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
                M_SYS_INFO sysInfo = this.sysInfoDao.GetAllDataForCode("0");
                if (sysInfo != null)
                {
                    this.sysInfoEntity = sysInfo;
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
            try
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
            catch (Exception ex)
            {
                LogUtility.Error("GetCloneDataTable", ex);
                throw;
            }
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
            LogUtility.DebugMethodStart();
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
                            foreach (M_COURSE_NAME bupanZaikoHokanBasyoEntity in this.entitys)
                            {
                                //if (bupanZaikoHokanBasyoEntity.COURSE_NAME_CD == null)
                                //{
                                //    msgLogic.MessageBoxShow("E075", "削除");

                                //    LogUtility.DebugMethodEnd();
                                //    return;
                                //}
                                M_COURSE_NAME entity = this.dao.GetDataByCd(bupanZaikoHokanBasyoEntity.COURSE_NAME_CD.ToString());
                                if (entity != null)
                                {
                                    //entity.DELETE_FLG = true;
                                    //entity.UPDATE_DATE = bupanZaikoHokanBasyoEntity.UPDATE_DATE;
                                    //entity.UPDATE_PC = bupanZaikoHokanBasyoEntity.UPDATE_PC;
                                    //entity.UPDATE_USER = bupanZaikoHokanBasyoEntity.UPDATE_USER;

                                    this.dao.Update(bupanZaikoHokanBasyoEntity);
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
                    csvExport.ConvertCustomDataGridViewToCsv(this.form.Ichiran, true, false, WINDOW_TITLEExt.ToTitleString(WINDOW_ID.M_COURSE_NAME), this.form);
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSVOutput", ex);
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

        #region 条件取消

        /// <summary>
        /// 条件取消
        /// </summary>
        public void CancelCondition()
        {
            try
            {
                LogUtility.DebugMethodStart();
                InitCondition();
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
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                this.SearchResult = dao.GetIchiranDataSql(this.SearchString, this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                //Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_TYPE.Text;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.Save();

                dtDetailList = this.SearchResult.Copy();

                int count = 0;
                if (this.SearchResult.Rows != null && this.SearchResult.Rows.Count > 0)
                {
                    count = 1;
                }

                LogUtility.DebugMethodEnd(count);

                return count;
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
        }

        #endregion

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            LogUtility.DebugMethodStart();

            bool ret = true;
            var courseNameCd = string.Empty;
            string[] strList;
            DataTable dt = this.form.Ichiran.DataSource as DataTable;
            foreach (DataRow Row in dt.Rows)
            {
                if (Row["DELETE_FLG"] != null && Row["DELETE_FLG"].ToString() == "True")
                {
                    courseNameCd += Row["COURSE_NAME_CD"].ToString() + ",";
                }
            }
            if (!string.IsNullOrEmpty(courseNameCd))
            {
                courseNameCd = courseNameCd.Substring(0, courseNameCd.Length - 1);
                strList = courseNameCd.Split(',');
                DataTable dtTable = dao.GetDataBySqlFileCheck(strList);
                if (dtTable != null && dtTable.Rows.Count > 0)
                {
                    string strName = string.Empty;

                    foreach (DataRow dr in dtTable.Rows)
                    {
                        strName += "\n" + dr["NAME"].ToString();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    DialogResult result = msgLogic.MessageBoxShow("C098", "コース名", "コース名CD", strName);
                    if (result == DialogResult.Yes || result == DialogResult.OK)
                    {
                        ret = true;
                    }
                    else
                    {
                        ret = false;
                    }
                }
                else
                {
                    ret = true;
                }
            }

            LogUtility.DebugMethodEnd();
            return ret;
        }

        #region 登録処理

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            LogUtility.DebugMethodStart(errorFlag);

            this.isRegist = true;

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
                        foreach (M_COURSE_NAME bupanZaikoHokanBasyoEntity in this.entitys)
                        {
                            M_COURSE_NAME entity = this.dao.GetDataByCd(bupanZaikoHokanBasyoEntity.COURSE_NAME_CD.ToString());
                            if (entity == null)
                            {
                                this.dao.Insert(bupanZaikoHokanBasyoEntity);
                            }
                            else
                            {
                                this.dao.Update(bupanZaikoHokanBasyoEntity);
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
                    LogUtility.Error(ex); //エラー
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
            try
            {
                LogUtility.DebugMethodStart();

                InitCondition();
                SetSearchString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
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

        #region コース名CDの重複チェック

        /// <summary>
        /// コース名CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck(string bupanZaiko_HokanBasyo_Cd, out bool catchErr)
        {
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(bupanZaiko_HokanBasyo_Cd);

                // 画面で種類CD重複チェック
                int recCount = 0;
                for (int i = 0; i < this.form.Ichiran.Rows.Count - 1; i++)
                {
                    string strCD = this.form.Ichiran.Rows[i].Cells["COURSE_NAME_CD"].Value.ToString().PadLeft(6, '0');
                    if (strCD.Equals(Convert.ToString(bupanZaiko_HokanBasyo_Cd)))
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
                    if (bupanZaiko_HokanBasyo_Cd.Equals(dtDetailList.Rows[i][1]))
                    {
                        LogUtility.DebugMethodEnd(false, catchErr);
                        return false;
                    }
                }

                // DBで種類CD重複チェック
                M_COURSE_NAME entity = this.dao.GetDataByCd(bupanZaiko_HokanBasyo_Cd);

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
            catchErr = true;
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var entityList = new M_COURSE_NAME[this.form.Ichiran.Rows.Count - 1];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_COURSE_NAME();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_COURSE_NAME>(entityList);
                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                              
                if (dt == null || dt.Rows.Count == 0)
                {
                    LogUtility.DebugMethodEnd(false, catchErr);
                    return false;
                }

                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    if (true == string.IsNullOrEmpty(dt.Rows[i]["COURSE_NAME_CD"].ToString()))
                    {
                        // 新規追加行対策として、コースCDが入力されていなければ、登録対象から除外
                        dt.Rows[i].Delete();
                    }
                }

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
                preDt = GetCloneDataTable(dt);

                List<M_COURSE_NAME> mBupanZaikoHokanBasyoList = new List<M_COURSE_NAME>();
                if (isDelete)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && (bool)this.form.Ichiran.Rows[i].Cells["chb_delete"].Value)
                        {
                            isSelect = true;
                            var m_BupanZaikoHokanBasyoEntity = CreateEntityForDataGridRow(this.form.Ichiran.Rows[i]);
                            dataBinderLogic.SetSystemProperty(m_BupanZaikoHokanBasyoEntity, true);
                            mBupanZaikoHokanBasyoList.Add(m_BupanZaikoHokanBasyoEntity);
                        }
                    }
                }

                if (isDelete)
                {
                    this.entitys = mBupanZaikoHokanBasyoList.ToArray();
                }
                else
                {
                    // 変更分のみ取得
                    List<M_COURSE_NAME> addList = new List<M_COURSE_NAME>();
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
                    var bupanZaikoHokanBasyoEntityList = CreateEntityForDataGrid(dtChange);
                    for (int i = 0; i < bupanZaikoHokanBasyoEntityList.Count; i++)
                    {
                        var bupanZaikoHokanBasyoEntity = bupanZaikoHokanBasyoEntityList[i];
                        for (int j = 0; j < this.form.Ichiran.Rows.Count - 1; j++)
                        {
                            bool isFind = false;
                            if (this.form.Ichiran.Rows[j].Cells["COURSE_NAME_CD"].Value.Equals(Convert.ToString(bupanZaikoHokanBasyoEntity.COURSE_NAME_CD)) &&
                                     bool.Parse(this.form.Ichiran.Rows[j].Cells["DELETE_FLG"].FormattedValue.ToString()) == isDelete)
                            {
                                isFind = true;
                            }

                            if (isFind)
                            {
                                dataBinderLogic.SetSystemProperty(bupanZaikoHokanBasyoEntity, false);
                                addList.Add(bupanZaikoHokanBasyoEntity);
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
        internal List<M_COURSE_NAME> CreateEntityForDataGrid(DataTable gridView)
        {
            try
            {
                LogUtility.DebugMethodStart(gridView);

                var entityList = new List<M_COURSE_NAME>();
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
        internal M_COURSE_NAME CreateEntityForDataRow(DataRow row)
        {
            try
            {
                LogUtility.DebugMethodStart(row);

                M_COURSE_NAME mBupanZaikoHokanBasyo = new M_COURSE_NAME();

                // DELETE_FLG
                if (!DBNull.Value.Equals(row["DELETE_FLG"]))
                {
                    mBupanZaikoHokanBasyo.DELETE_FLG = (Boolean)row["DELETE_FLG"];
                }
                else
                {
                    mBupanZaikoHokanBasyo.DELETE_FLG = false;
                }

                // COURSE_NAME_CD
                if (!DBNull.Value.Equals(row["COURSE_NAME_CD"]))
                {
                    mBupanZaikoHokanBasyo.COURSE_NAME_CD = (string)row["COURSE_NAME_CD"];
                }

                // COURSE_NAME
                if (!DBNull.Value.Equals(row["COURSE_NAME"]))
                {
                    mBupanZaikoHokanBasyo.COURSE_NAME = (string)row["COURSE_NAME"];
                }

                // COURSE_NAME_RYAKU
                if (!DBNull.Value.Equals(row["COURSE_NAME_RYAKU"]))
                {
                    mBupanZaikoHokanBasyo.COURSE_NAME_RYAKU = (string)row["COURSE_NAME_RYAKU"];
                }

                // COURSE_NAME_FURIGANA
                if (!DBNull.Value.Equals(row["COURSE_NAME_FURIGANA"]))
                {
                    mBupanZaikoHokanBasyo.COURSE_NAME_FURIGANA = (string)row["COURSE_NAME_FURIGANA"];
                }

                // KYOTEN_CD
                if (!DBNull.Value.Equals(row["KYOTEN_CD"]))
                {
                    mBupanZaikoHokanBasyo.KYOTEN_CD = SqlInt16.Parse(row["KYOTEN_CD"].ToString());
                }

                // MONDAY
                if (!DBNull.Value.Equals(row["MONDAY"]))
                {
                    mBupanZaikoHokanBasyo.MONDAY = SqlBoolean.Parse(row["MONDAY"].ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.MONDAY = false;
                }

                // TUESDAY
                if (!DBNull.Value.Equals(row["TUESDAY"]))
                {
                    mBupanZaikoHokanBasyo.TUESDAY = SqlBoolean.Parse(row["TUESDAY"].ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.TUESDAY = false;
                }

                // WEDNESDAY
                if (!DBNull.Value.Equals(row["WEDNESDAY"]))
                {
                    mBupanZaikoHokanBasyo.WEDNESDAY = SqlBoolean.Parse(row["WEDNESDAY"].ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.WEDNESDAY = false;
                }

                // THURSDAY
                if (!DBNull.Value.Equals(row["THURSDAY"]))
                {
                    mBupanZaikoHokanBasyo.THURSDAY = SqlBoolean.Parse(row["THURSDAY"].ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.THURSDAY = false;
                }

                // FRIDAY
                if (!DBNull.Value.Equals(row["FRIDAY"]))
                {
                    mBupanZaikoHokanBasyo.FRIDAY = SqlBoolean.Parse(row["FRIDAY"].ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.FRIDAY = false;
                }

                // SATURDAY
                if (!DBNull.Value.Equals(row["SATURDAY"]))
                {
                    mBupanZaikoHokanBasyo.SATURDAY = SqlBoolean.Parse(row["SATURDAY"].ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.SATURDAY = false;
                }

                // SUNDAY
                if (!DBNull.Value.Equals(row["SUNDAY"]))
                {
                    mBupanZaikoHokanBasyo.SUNDAY = SqlBoolean.Parse(row["SUNDAY"].ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.SUNDAY = false;
                }

                // COURSE_NAME_BIKOU
                if (!DBNull.Value.Equals(row["COURSE_NAME_BIKOU"]))
                {
                    mBupanZaikoHokanBasyo.COURSE_NAME_BIKOU = (string)row["COURSE_NAME_BIKOU"];
                }

                // TIME_STAMP
                if (!DBNull.Value.Equals(row["TIME_STAMP"]))
                {
                    mBupanZaikoHokanBasyo.TIME_STAMP = (byte[])row["TIME_STAMP"];
                }

                LogUtility.DebugMethodEnd(mBupanZaikoHokanBasyo);
                return mBupanZaikoHokanBasyo;
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
        internal M_COURSE_NAME CreateEntityForDataGridRow(DataGridViewRow row)
        {
            try
            {
                LogUtility.DebugMethodStart(row);

                M_COURSE_NAME mBupanZaikoHokanBasyo = new M_COURSE_NAME();

                // DELETE_FLG
                if (!DBNull.Value.Equals(row.Cells["DELETE_FLG"].Value))
                {
                    mBupanZaikoHokanBasyo.DELETE_FLG = (Boolean)row.Cells["DELETE_FLG"].Value;
                }
                else
                {
                    mBupanZaikoHokanBasyo.DELETE_FLG = false;
                }

                // COURSE_NAME_CD
                if (!DBNull.Value.Equals(row.Cells["COURSE_NAME_CD"].Value))
                {
                    mBupanZaikoHokanBasyo.COURSE_NAME_CD = (string)row.Cells["COURSE_NAME_CD"].Value;
                }

                // COURSE_NAME
                if (!DBNull.Value.Equals(row.Cells["COURSE_NAME"].Value))
                {
                    mBupanZaikoHokanBasyo.COURSE_NAME = (string)row.Cells["COURSE_NAME"].Value;
                }

                // COURSE_NAME_RYAKU
                if (!DBNull.Value.Equals(row.Cells["COURSE_NAME_RYAKU"].Value))
                {
                    mBupanZaikoHokanBasyo.COURSE_NAME_RYAKU = (string)row.Cells["COURSE_NAME_RYAKU"].Value;
                }

                // COURSE_NAME_FURIGANA
                if (!DBNull.Value.Equals(row.Cells["COURSE_NAME_FURIGANA"].Value))
                {
                    mBupanZaikoHokanBasyo.COURSE_NAME_FURIGANA = (string)row.Cells["COURSE_NAME_FURIGANA"].Value;
                }

                // KYOTEN_CD
                if (!DBNull.Value.Equals(row.Cells["KYOTEN_CD"].Value))
                {
                    mBupanZaikoHokanBasyo.KYOTEN_CD = SqlInt16.Parse(row.Cells["KYOTEN_CD"].Value.ToString());
                }

                // MONDAY
                if (!DBNull.Value.Equals(row.Cells["MONDAY"].Value))
                {
                    mBupanZaikoHokanBasyo.MONDAY = SqlBoolean.Parse(row.Cells["MONDAY"].Value.ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.MONDAY = false;
                }

                // TUESDAY
                if (!DBNull.Value.Equals(row.Cells["TUESDAY"].Value))
                {
                    mBupanZaikoHokanBasyo.TUESDAY = SqlBoolean.Parse(row.Cells["TUESDAY"].Value.ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.TUESDAY = false;
                }

                // WEDNESDAY
                if (!DBNull.Value.Equals(row.Cells["WEDNESDAY"].Value))
                {
                    mBupanZaikoHokanBasyo.WEDNESDAY = SqlBoolean.Parse(row.Cells["WEDNESDAY"].Value.ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.WEDNESDAY = false;
                }

                // THURSDAY
                if (!DBNull.Value.Equals(row.Cells["THURSDAY"].Value))
                {
                    mBupanZaikoHokanBasyo.THURSDAY = SqlBoolean.Parse(row.Cells["THURSDAY"].Value.ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.THURSDAY = false;
                }

                // FRIDAY
                if (!DBNull.Value.Equals(row.Cells["FRIDAY"].Value))
                {
                    mBupanZaikoHokanBasyo.FRIDAY = SqlBoolean.Parse(row.Cells["FRIDAY"].Value.ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.FRIDAY = false;
                }

                // SATURDAY
                if (!DBNull.Value.Equals(row.Cells["SATURDAY"].Value))
                {
                    mBupanZaikoHokanBasyo.SATURDAY = SqlBoolean.Parse(row.Cells["SATURDAY"].Value.ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.SATURDAY = false;
                }

                // SUNDAY
                if (!DBNull.Value.Equals(row.Cells["SUNDAY"].Value))
                {
                    mBupanZaikoHokanBasyo.SUNDAY = SqlBoolean.Parse(row.Cells["SUNDAY"].Value.ToString());
                }
                else
                {
                    mBupanZaikoHokanBasyo.SUNDAY = false;
                }

                // COURSE_NAME_BIKOU
                if (!DBNull.Value.Equals(row.Cells["COURSE_NAME_BIKOU"].Value))
                {
                    mBupanZaikoHokanBasyo.COURSE_NAME_BIKOU = (string)row.Cells["COURSE_NAME_BIKOU"].Value;
                }

                // TIME_STAMP
                if (!DBNull.Value.Equals(row.Cells["TIME_STAMP"].Value))
                {
                    mBupanZaikoHokanBasyo.TIME_STAMP = (byte[])row.Cells["TIME_STAMP"].Value;
                }

                LogUtility.DebugMethodEnd(mBupanZaikoHokanBasyo);
                return mBupanZaikoHokanBasyo;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntityForDataGridRow", ex);
                throw;
            }
        }

        #endregion

        #region 登録前のチェック

        /// <summary>
        /// 登録前のチェック
        /// </summary>
        /// <returns></returns>
        public Boolean CheckBeforeUpdate()
        {
            try
            {
                LogUtility.DebugMethodStart();

                var msgLogic = new MessageBoxShowLogic();

                //ArrayList errColName = new ArrayList();

                //Boolean rtn = false;

                //Boolean isErr;

                //// 必須入力チェック
                //// コースCD
                //isErr = false;
                //for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                //{
                //    if (null == this.form.Ichiran.Rows[i].Cells["COURSE_NAME_CD"].Value ||
                //        "".Equals(this.form.Ichiran.Rows[i].Cells["COURSE_NAME_CD"].Value.ToString().Trim()))
                //    {
                //        if (false == isErr)
                //        {
                //            errColName.Add("コース名CD");
                //            isErr = true;
                //        }
                //        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["COURSE_NAME_CD"], true);
                //    }
                //}

                //// コース名
                //isErr = false;
                //for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                //{
                //    if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["COURSE_NAME"].Value) ||
                //        "".Equals(this.form.Ichiran.Rows[i].Cells["COURSE_NAME"].Value.ToString().Trim()))
                //    {
                //        if (false == isErr)
                //        {
                //            errColName.Add("コース名");
                //            isErr = true;
                //        }
                //        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["COURSE_NAME"], true);
                //    }
                //}

                //// 略称
                //isErr = false;
                //for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                //{
                //    if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["COURSE_NAME_RYAKU"].Value) ||
                //        "".Equals(this.form.Ichiran.Rows[i].Cells["COURSE_NAME_RYAKU"].Value.ToString().Trim()))
                //    {
                //        if (false == isErr)
                //        {
                //            errColName.Add("略称");
                //            isErr = true;
                //        }
                //        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["COURSE_NAME_RYAKU"], true);
                //    }
                //}

                //// フリガナ
                //isErr = false;
                //for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                //{
                //    if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["COURSE_NAME_FURIGANA"].Value) ||
                //        "".Equals(this.form.Ichiran.Rows[i].Cells["COURSE_NAME_FURIGANA"].Value.ToString().Trim()))
                //    {
                //        if (false == isErr)
                //        {
                //            errColName.Add("フリガナ");
                //            isErr = true;
                //        }
                //        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["COURSE_NAME_FURIGANA"], true);
                //    }
                //}

                //// 拠点CD
                //isErr = false;
                //for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                //{
                //    if (DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["KYOTEN_CD"].Value) ||
                //        "".Equals(this.form.Ichiran.Rows[i].Cells["KYOTEN_CD"].Value.ToString().Trim()))
                //    {
                //        if (false == isErr)
                //        {
                //            errColName.Add("拠点CD");
                //            isErr = true;
                //        }
                //        ControlUtility.SetInputErrorOccuredForDgvCell(this.form.Ichiran.Rows[i].Cells["KYOTEN_CD"], true);
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

                //    LogUtility.DebugMethodEnd(rtn);
                //    return rtn;
                //}
                // 曜日
                for (int i = 0; i < this.form.Ichiran.RowCount - 1; i++)
                {
                    if ((this.form.Ichiran.Rows[i].Cells["chb_delete"].Value != null && this.form.Ichiran.Rows[i].Cells["chb_delete"].Value.ToString() == "True")
                         && (this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value == null || string.IsNullOrEmpty(this.form.Ichiran.Rows[i].Cells["CREATE_USER"].Value.ToString())))
                    {
                        continue;
                    }
                    if ((this.form.Ichiran.Rows[i].Cells["MONDAY"].Value.Equals(false) ||
                        DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["MONDAY"].Value)) &&
                        (this.form.Ichiran.Rows[i].Cells["TUESDAY"].Value.Equals(false) ||
                        DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["TUESDAY"].Value)) &&
                        (this.form.Ichiran.Rows[i].Cells["WEDNESDAY"].Value.Equals(false) ||
                        DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["WEDNESDAY"].Value)) &&
                        (this.form.Ichiran.Rows[i].Cells["THURSDAY"].Value.Equals(false) ||
                        DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["THURSDAY"].Value)) &&
                        (this.form.Ichiran.Rows[i].Cells["FRIDAY"].Value.Equals(false) ||
                        DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["FRIDAY"].Value)) &&
                        (this.form.Ichiran.Rows[i].Cells["SATURDAY"].Value.Equals(false) ||
                        DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["SATURDAY"].Value)) &&
                        (this.form.Ichiran.Rows[i].Cells["SUNDAY"].Value.Equals(false) ||
                        DBNull.Value.Equals(this.form.Ichiran.Rows[i].Cells["SUNDAY"].Value)))
                    {
                        this.form.Ichiran.Rows[i].Cells["MONDAY"].Selected = false;
                        this.form.Ichiran.Rows[i].Cells["TUESDAY"].Selected = false;
                        this.form.Ichiran.Rows[i].Cells["WEDNESDAY"].Selected = false;
                        this.form.Ichiran.Rows[i].Cells["THURSDAY"].Selected = false;
                        this.form.Ichiran.Rows[i].Cells["FRIDAY"].Selected = false;
                        this.form.Ichiran.Rows[i].Cells["SATURDAY"].Selected = false;
                        this.form.Ichiran.Rows[i].Cells["SUNDAY"].Selected = false;

                        this.form.Ichiran.Rows[i].Cells["MONDAY"].Style.BackColor = Constans.ERROR_COLOR;
                        this.form.Ichiran.Rows[i].Cells["TUESDAY"].Style.BackColor = Constans.ERROR_COLOR;
                        this.form.Ichiran.Rows[i].Cells["WEDNESDAY"].Style.BackColor = Constans.ERROR_COLOR;
                        this.form.Ichiran.Rows[i].Cells["THURSDAY"].Style.BackColor = Constans.ERROR_COLOR;
                        this.form.Ichiran.Rows[i].Cells["FRIDAY"].Style.BackColor = Constans.ERROR_COLOR;
                        this.form.Ichiran.Rows[i].Cells["SATURDAY"].Style.BackColor = Constans.ERROR_COLOR;
                        this.form.Ichiran.Rows[i].Cells["SUNDAY"].Style.BackColor = Constans.ERROR_COLOR;

                        msgLogic.MessageBoxShow("E094");

                        LogUtility.DebugMethodEnd(false);
                        return false;
                    }
                }

                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckBeforeUpdate", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
                return false;
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
                    LogUtility.DebugMethodEnd(false);
                    return false;
                }

                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("ActionBeforeCheck", ex);
                throw;
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
                var allEntityList = this.dao.GetAllData().Select(s => s.COURSE_NAME_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Cast<DataGridViewRow>().Select(r => r.Cells["COURSE_NAME_CD"]).Where(c => c.Value != null).ToList().
                                        ForEach(c => c.ReadOnly = allEntityList.Contains(c.Value.ToString()));
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