// $Id: UnchinTankaHoshuLogic.cs 42925 2015-02-24 06:44:33Z y-hosokawa@takumi-sys.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.CustomControl;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using Shougun.Core.Master.UnchinTankaHoshu.APP;
using Shougun.Core.Master.UnchinTankaHoshu.Dto;
using Shougun.Core.Master.UnchinTankaHoshu.Validator;

namespace Shougun.Core.Master.UnchinTankaHoshu.Logic
{
    /// <summary>
    /// 運賃単価入力画面のビジネスロジック
    /// </summary>
    public class UnchinTankaHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "Shougun.Core.Master.UnchinTankaHoshu.Setting.ButtonSetting.xml";

        /// <summary>
        /// 運賃単価入力画面Form
        /// </summary>
        private UnchinTankaHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private DataDto[] entitys;

        private bool isAllSearch;

        private List<String> messageList;

        /// <summary>
        /// エラーメッセージプロパティ
        /// </summary>
        private List<String> errorMessages;

        // Popup運賃品名
        public IM_UNCHIN_HINMEIDao unchinHinmeiDao;

        // Popup運賃品名
        public IM_UNITDao unitDao;

        /// <summary>
        /// 運賃単価入力のDao
        /// </summary>
        private Shougun.Core.Master.UnchinTankaHoshu.Dao.IM_UNCHIN_TANKADao dao;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao sysInfoDao;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        private Boolean jyoukenFlg = false;

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal MasterBaseForm parentForm;
        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        public bool isRegist = true;

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
        public M_UNCHIN_TANKA SearchString { get; set; }

        /// <summary>
        /// 検索結果(業者)
        /// </summary>
        public M_GYOUSHA SearchResultGyousha { get; set; }

        #endregion

        #region コンストラクタ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public UnchinTankaHoshuLogic(UnchinTankaHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<Shougun.Core.Master.UnchinTankaHoshu.Dao.IM_UNCHIN_TANKADao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.sysInfoDao = DaoInitUtility.GetComponent<IM_SYS_INFODao>();
            this.unchinHinmeiDao = DaoInitUtility.GetComponent<IM_UNCHIN_HINMEIDao>();
            this.unitDao = DaoInitUtility.GetComponent<IM_UNITDao>();

            this.entitySysInfo = null;
            M_SYS_INFO[] sysInfo = this.sysInfoDao.GetAllData();
            if (sysInfo != null && sysInfo.Length > 0)
            {
                this.entitySysInfo = sysInfo[0];
            }

            LogUtility.DebugMethodEnd();
        }

        #endregion

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

                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (MasterBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end

                // ボタンのテキストを初期化
                this.ButtonInit();

                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                this.form.UNPAN_GYOUSHA_CD.Text = Properties.Settings.Default.GyoushaValue_Text.Trim();
                this.form.UNPAN_GYOUSHA_NAME.Text = Properties.Settings.Default.GyoushaName_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                this.form.Ichiran.AllowUserToAddRows = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

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
            var buttonSetting = new ButtonSetting();

            var thisAssembly = Assembly.GetExecutingAssembly();
            return buttonSetting.LoadButtonSetting(thisAssembly, this.ButtonInfoXmlPath);
        }

        #endregion

        #region イベントの初期化処理

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

            ////ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            //parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

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

        #endregion

        #region 表示条件初期値設定処理

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

        #endregion

        #region 参照モード表示に変更します

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

        #endregion

        #region データ取得処理

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

                this.SearchResult = dao.GetIchiranDataSqlFile(this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                // 現在の業者データに変更
                M_UNCHIN_TANKA searchParams = new M_UNCHIN_TANKA();
                searchParams.UNPAN_GYOUSHA_CD = this.SearchString.UNPAN_GYOUSHA_CD;
                this.SearchResultAll = dao.GetDataByUnpanGyoushaCdMinCols(searchParams);

                // 20151208 SQL検索項目を削減する Start
                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), new DataRowUnchinTankaHoshuCompare());
                // 20151208 SQL検索項目を削減する End

                Properties.Settings.Default.GyoushaValue_Text = this.form.UNPAN_GYOUSHA_CD.Text;
                Properties.Settings.Default.GyoushaName_Text = this.form.UNPAN_GYOUSHA_NAME.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;

                Properties.Settings.Default.Save();

                count = this.SearchResult.Rows.Count == 0 ? 0 : 1;

                if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                {
                    this.form.Ichiran.ReadOnly = true;
                    // FunctionButton
                    var parentForm = (MasterBaseForm)this.form.Parent;
                    parentForm.bt_func4.Enabled = false;
                    parentForm.bt_func6.Enabled = false;
                    parentForm.bt_func9.Enabled = false;
                }
                else
                {
                    this.form.Ichiran.ReadOnly = false;
                    // FunctionButton
                    var parentForm = (MasterBaseForm)this.form.Parent;
                    parentForm.bt_func4.Enabled = true;
                    parentForm.bt_func6.Enabled = true;
                    parentForm.bt_func9.Enabled = true;
                }
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
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

        #region コントロールから対象のEntityを作成する

        /// <summary>
        /// コントロールから対象のEntityを作成する
        /// </summary>
        public bool CreateEntity(bool isDelete)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(isDelete);

                var entityList = new M_UNCHIN_TANKA[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_UNCHIN_TANKA();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_UNCHIN_TANKA>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.UnchinTankaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 元の値から全く変化がなければ、 RowState を元の状態に戻す
                foreach (DataRow row in dt.Rows)
                {
                    if (!DataTableUtility.IsDataRowChanged(row))
                    {
                        row.AcceptChanges();
                    }
                }

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var kansanEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<DataDto> addList = new List<DataDto>();
                int count = 0;
                foreach (var unchinTankaEntity in kansanEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells["DELETE_FLG"].Value != null
                            && Convert.ToString(row.Cells["DELETE_FLG"].Value) == "True"
                            && (row.Cells["CREATE_USER"].Value == null
                            || string.IsNullOrEmpty(row.Cells["CREATE_USER"].Value.ToString())))
                        {
                            continue;
                        }
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD) && n.Value.ToString().Equals(unchinTankaEntity.UNCHIN_HINMEI_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.UnchinTankaHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            DataDto data = new DataDto();

                            // 修正対象が本当に修正されているかチェックする
                            if (!unchinTankaEntity.UNCHIN_HINMEI_CD.Equals(string.Empty))
                            {
                                DataRow[] dr = this.SearchResultAll.Select(String.Format("UNCHIN_HINMEI_CD = '{0}'", unchinTankaEntity.UNCHIN_HINMEI_CD));
                                if (dr.Length > 0
                                    && ((bool)dr[0][Const.UnchinTankaHoshuConstans.DELETE_FLG]).Equals(unchinTankaEntity.DELETE_FLG.Value)
                                    && dr[0][Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD].ToString().Equals(unchinTankaEntity.UNCHIN_HINMEI_CD)
                                    && (dr[0][Const.UnchinTankaHoshuConstans.UNIT_CD] == null ? string.Empty : dr[0][Const.UnchinTankaHoshuConstans.UNIT_CD].ToString()) == (unchinTankaEntity.UNIT_CD.IsNull ? string.Empty : unchinTankaEntity.UNIT_CD.ToString())
                                    && (dr[0][Const.UnchinTankaHoshuConstans.TANKA] == System.DBNull.Value ? string.Empty : dr[0][Const.UnchinTankaHoshuConstans.TANKA].ToString()) == (unchinTankaEntity.TANKA.IsNull ? string.Empty : unchinTankaEntity.TANKA.ToString())
                                    && (dr[0][Const.UnchinTankaHoshuConstans.SHASHU_CD] == null ? string.Empty : dr[0][Const.UnchinTankaHoshuConstans.SHASHU_CD].ToString()) == (string.IsNullOrEmpty(unchinTankaEntity.SHASHU_CD) ? string.Empty : unchinTankaEntity.SHASHU_CD.ToString())
                                    && dr[0][Const.UnchinTankaHoshuConstans.BIKOU].ToString().Equals(unchinTankaEntity.BIKOU))
                                {
                                    break;
                                }
                            }
                            unchinTankaEntity.SetValue(this.form.UNPAN_GYOUSHA_CD);
                            unchinTankaEntity.SetValue(this.form.UNPAN_GYOUSHA_NAME);
                            unchinTankaEntity.UNPAN_GYOUSHA_NAME = this.form.UNPAN_GYOUSHA_NAME.Text;
                            if (string.IsNullOrEmpty(unchinTankaEntity.SHASHU_CD))
                            {
                                unchinTankaEntity.SHASHU_CD = "";
                            }
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), unchinTankaEntity);
                            data.entity = unchinTankaEntity;
                            DataRow tempRow = ((DataTable)this.form.Ichiran.DataSource).Rows[count];
                            data.updateKey.UNPAN_GYOUSHA_CD = tempRow["UK_UNPAN_GYOUSHA_CD"] != DBNull.Value ? tempRow["UK_UNPAN_GYOUSHA_CD"].ToString() : null;
                            data.updateKey.UNCHIN_HINMEI_CD = tempRow["UK_UNCHIN_HINMEI_CD"] != DBNull.Value ? tempRow["UK_UNCHIN_HINMEI_CD"].ToString() : null;
                            if (tempRow["UK_UNIT_CD"] != DBNull.Value)
                            {
                                data.updateKey.UNIT_CD = Convert.ToInt16(tempRow["UK_UNIT_CD"]);
                            }
                            data.updateKey.SHASHU_CD = tempRow["UK_SHASHU_CD"] != DBNull.Value ? tempRow["UK_SHASHU_CD"].ToString() : null;
                            addList.Add(data);
                            count++;
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

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 取消処理

        /// <summary>
        /// 取消処理
        /// </summary>
        public bool Cancel()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                jyoukenFlg = false;
                ClearCondition(jyoukenFlg);
                SetSearchString();
                this.form.Ichiran.ReadOnly = true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region プレビュー  未実装

        /// <summary>
        /// プレビュー
        /// </summary>
        public void Preview()
        {
            LogUtility.DebugMethodStart();

            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            msgLogic.MessageBoxShow("C011", "車輌一覧表");

            MessageBox.Show("未実装");

            LogUtility.DebugMethodEnd();
        }

        #endregion

        #region CSV

        /// <summary>
        /// CSV
        /// </summary>
        public bool CSV()
        {
            bool ret = true;
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
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 条件取消

        /// <summary>
        /// 条件取消
        /// </summary>
        public bool CancelCondition()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                jyoukenFlg = false;
                ClearCondition(jyoukenFlg);
                SetSearchString();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }

            LogUtility.DebugMethodEnd(ret);
            return ret;
        }

        #endregion

        #region 登録/更新/削除

        /// <summary>
        /// 登録処理
        /// </summary>
        /// <param name="errorFlag"></param>
        [Transaction]
        public virtual void Regist(bool errorFlag)
        {
            // 重複エラー時用のメッセージで使用
            string dispUnchinHinmeiCd = string.Empty;
            Int16 dispUnitCd = -1;
            string dispShashuCd = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(errorFlag);
                //独自チェックの記述例を書く

                this.isRegist = true;
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (DataDto data in this.entitys)
                        {
                            M_UNCHIN_TANKA entity = this.dao.GetDataByCd(data.updateKey);
                            dispUnchinHinmeiCd = string.Empty;
                            dispUnitCd = -1;
                            dispShashuCd = string.Empty;

                            if (entity == null)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                if (data.entity.DELETE_FLG)
                                {
                                    continue;
                                }
                                this.dao.Insert(data.entity);
                            }
                            else
                            {
                                dispUnchinHinmeiCd = data.entity.UNCHIN_HINMEI_CD;
                                dispUnitCd = data.entity.UNIT_CD.IsNull ? Convert.ToInt16(-1) : (Int16)data.entity.UNIT_CD;
                                dispShashuCd = data.entity.SHASHU_CD;
                                this.dao.UpdateBySqlFile(data.entity, data.updateKey);
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
                this.form.errmessage.MessageBoxShow("E080", "");
                this.isRegist = false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Regist", ex2);
                var tempEx = ex2.Args[0] as SqlException;
                if ((!string.IsNullOrEmpty(dispUnchinHinmeiCd)
                    || dispUnitCd >= 0
                    || !string.IsNullOrEmpty(dispShashuCd))
                    && (tempEx != null && tempEx.Number == Constans.SQL_EXCEPTION_NUMBER_DUPLICATE))
                {
                    this.form.errmessage.MessageBoxShow("E259", "単価または備考", "・運賃品名CD：" + dispUnchinHinmeiCd + "、単位CD：" + dispUnitCd + "、車種CD：" + dispShashuCd);
                }
                else
                {
                    this.form.errmessage.MessageBoxShow("E093", "");
                }
                this.isRegist = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                this.isRegist = false;
            }
            LogUtility.DebugMethodEnd();
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
                        foreach (DataDto data in this.entitys)
                        {
                            M_UNCHIN_TANKA entity = this.dao.GetDataByCd(data.updateKey);
                            if (entity != null)
                            {
                                this.dao.Update(data.entity);
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

            LogUtility.DebugMethodEnd();
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

            UnchinTankaHoshuLogic localLogic = other as UnchinTankaHoshuLogic;
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

        #region 検索結果を一覧に設定

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
            bool ret = true;
            try
            {
                var table = this.SearchResult;

                // テーブルデータの空判断
                if (table != null)
                {
                    table.BeginLoadData();

                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        table.Columns[i].ReadOnly = false;
                    }
                }

                this.form.Ichiran.DataSource = table;

                if (r_framework.Authority.Manager.CheckAuthority("M640", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.DispReferenceMode();
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 検索条件の設定

        /// <summary>
        /// 検索条件の設定
        /// </summary>
        private void SetSearchString()
        {
            M_UNCHIN_TANKA entity = new M_UNCHIN_TANKA();

            if (!string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
            {
                // 検索条件の設定
                entity.SetValue(this.form.UNPAN_GYOUSHA_CD);
            }
            this.SearchString = entity;
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

        #endregion

        #region 検索条件初期化

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearCondition(Boolean jyoukenFlg)
        {
            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

            if (!jyoukenFlg)
            {
                this.form.UNPAN_GYOUSHA_CD.Text = string.Empty;
                this.form.UNPAN_GYOUSHA_NAME.Text = string.Empty;
                this.form.Ichiran.DataSource = null;
                this.form.Ichiran.AllowUserToAddRows = false;
                this.SearchResult = null;
                this.SearchResultAll = null;
                this.SearchString = null;
            }
        }

        #endregion

        #region 運搬業者CDValidated

        /// <summary>
        /// 運搬業者CDValidated
        /// </summary>
        /// <param name="sender">イベントが発生したオブジェクト</param>
        /// <param name="e">イベント引数</param>
        internal bool UNPAN_GYOUSHA_CD_Validated()
        {
            bool ret = true;
            try
            {
                var messageLogic = new MessageBoxShowLogic();

                int cnt = this.form.Ichiran.Rows.Cast<Row>().Where(r => !r.IsNewRow).Count();
                string beforeGyoushaCd = Properties.Settings.Default.GyoushaValue_Text;
                if (cnt > 0 && Properties.Settings.Default.GyoushaValue_Text != this.form.UNPAN_GYOUSHA_CD.Text)
                {
                    DialogResult result = messageLogic.MessageBoxShow("C088");
                    if (result == DialogResult.No)
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = Properties.Settings.Default.GyoushaName_Text;
                        this.form.UNPAN_GYOUSHA_CD.Text = Properties.Settings.Default.GyoushaValue_Text;
                        return ret;
                    }
                    else
                    {
                        FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
                        // 明細部クリア
                        this.form.Ichiran.DataSource = null;
                        this.form.Ichiran.AllowUserToAddRows = false;
                        this.SearchResult = null;
                        this.SearchResultAll = null;
                        this.SearchString = null;
                        this.form.Ichiran.ReadOnly = true;
                    }
                }

                if (this.form.beforeGyoushaCd != this.form.UNPAN_GYOUSHA_CD.Text || this.form.isError)
                {
                    this.form.isError = false;

                    if (string.IsNullOrEmpty(this.form.UNPAN_GYOUSHA_CD.Text))
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = "";
                        // 明細部クリア
                        this.form.Ichiran.DataSource = null;
                        this.form.Ichiran.AllowUserToAddRows = false;
                        this.SearchResult = null;
                        this.SearchResultAll = null;
                        this.SearchString = null;
                        this.form.Ichiran.ReadOnly = true;
                        return ret;
                    }

                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
                    gyousha = this.gyoushaDao.GetUnpanGyoushaData(gyousha);

                    if (gyousha == null)
                    {
                        this.form.isError = true;
                        this.form.UNPAN_GYOUSHA_CD.BackColor = Constans.ERROR_COLOR;
                        this.form.UNPAN_GYOUSHA_NAME.Text = "";
                        messageLogic.MessageBoxShow("E020", "運搬業者");
                        this.form.UNPAN_GYOUSHA_CD.Focus();
                        return ret;
                    }
                    else
                    {
                        this.form.UNPAN_GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                        this.form.Ichiran.ReadOnly = true;
                    }
                }
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CD_Validated", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                ret = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("UNPAN_GYOUSHA_CD_Validated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            return ret;
        }

        #endregion

        #region 運賃品名名称の設定

        /// <summary>
        /// 運賃品名名称の設定
        /// </summary>
        /// <param name="e"></param>
        public bool IchiranCellValidatingUnchinHinmei(CellCancelEventArgs e)
        {
            try
            {
                var cell = this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD] as GcCustomTextBoxCell;
                if (!string.IsNullOrWhiteSpace(Convert.ToString(cell.Value)))
                {
                    string unchinHinmeiCd = cell.Value.ToString();
                    if (this.form.preValue == unchinHinmeiCd && !this.form.isError)
                    {
                        return true;
                    }

                    this.form.isError = false;
                    var unchinHinmeiEntry = unchinHinmeiDao.GetAllValidData(new M_UNCHIN_HINMEI { UNCHIN_HINMEI_CD = unchinHinmeiCd }).FirstOrDefault();
                    if (unchinHinmeiEntry != null)
                    {
                        this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_NAME].Value = unchinHinmeiEntry.UNCHIN_HINMEI_NAME.ToString();
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "運賃品名");
                        e.Cancel = true;
                        if (this.form.Ichiran.EditingControl != null)
                        {
                            ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                        }
                        cell.IsInputErrorOccured = true;
                        cell.UpdateBackColor();
                        this.form.isError = true;
                        return false;
                    }
                }
                else
                {
                    this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_NAME].Value = string.Empty;
                }
                cell.Style.BackColor = Constans.NOMAL_COLOR;
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IchiranCellUnchinHinmeiValidating", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranCellUnchinHinmeiValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        #endregion

        #region 運賃単位名称の設定

        /// <summary>
        /// 運賃単位名称の設定
        /// </summary>
        /// <param name="e"></param>
        public bool IchiranCellValidatingUnit(CellCancelEventArgs e)
        {
            try
            {
                var cell = this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNIT_CD] as GcCustomTextBoxCell;
                if (!string.IsNullOrWhiteSpace(Convert.ToString(cell.Value)))
                {
                    string unitCd = cell.Value.ToString();
                    if (this.form.preValue == unitCd && !this.form.isError)
                    {
                        return true;
                    }

                    Int16 cd = Convert.ToInt16(unitCd);
                    var unitEntry = unitDao.GetAllValidData(new M_UNIT { UNIT_CD = cd }).FirstOrDefault();
                    if (unitEntry != null)
                    {
                        this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNIT_NAME_RYAKU].Value = unitEntry.UNIT_NAME_RYAKU;
                    }
                    else
                    {
                        MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                        msgLogic.MessageBoxShow("E020", "単位");
                        e.Cancel = true;
                        if (this.form.Ichiran.EditingControl != null)
                        {
                            ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                        }
                        cell.IsInputErrorOccured = true;
                        cell.UpdateBackColor();
                        return false;
                    }
                }
                else
                {
                    this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNIT_NAME_RYAKU].Value = string.Empty;
                }
                cell.Style.BackColor = Constans.NOMAL_COLOR;
                return true;
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("IchiranCellUnitValidating", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("IchiranCellUnitValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return false;
            }
        }

        #endregion

        #region 単位入力・表示切り替え

        /// <summary>
        ///
        /// </summary>
        /// <param name="e"></param>
        /// <param name="fswit"></param>
        /// <returns></returns>
        public bool IchiranCellSwitchCdName(CellEventArgs e, Const.UnchinTankaHoshuConstans.FocusSwitch fswit)
        {
            switch (fswit)
            {
                case Const.UnchinTankaHoshuConstans.FocusSwitch.IN:
                    // 単位名称にフォーカス時実行
                    if (e.CellName.Equals(Const.UnchinTankaHoshuConstans.UNIT_NAME_RYAKU))
                    {
                        this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNIT_CD].Visible = true;
                        this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNIT_CD].UpdateBackColor(false);

                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNIT_CD];
                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                case Const.UnchinTankaHoshuConstans.FocusSwitch.OUT:
                    // 単位CDに検証成功後実行
                    if (e.CellName.Equals(Const.UnchinTankaHoshuConstans.UNIT_CD))
                    {
                        this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNIT_NAME_RYAKU].Visible = true;
                        this.form.Ichiran.CurrentCell = this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNIT_NAME_RYAKU];
                        this.form.Ichiran[e.RowIndex, Const.UnchinTankaHoshuConstans.UNIT_NAME_RYAKU].UpdateBackColor(false);

                        this.form.Ichiran[e.RowIndex, e.CellIndex].Visible = false;
                    }
                    break;

                default:
                    break;
            }

            return true;
        }

        #endregion

        #region 単価フォーマット

        /// <summary>
        /// 単価フォーマット
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string FormatSystemTanka(decimal num)
        {
            string format = "#,##0";
            if (!string.IsNullOrWhiteSpace(this.entitySysInfo.SYS_TANKA_FORMAT))
            {
                format = this.entitySysInfo.SYS_TANKA_FORMAT;
                switch (this.entitySysInfo.SYS_TANKA_FORMAT_CD.Value)
                {
                    case 1:
                    case 2:
                        num = Math.Floor(num);
                        break;

                    case 3:
                        num = Math.Floor(num * 10) / 10;
                        break;

                    case 4:
                        num = Math.Floor(num * 100) / 100;
                        break;

                    case 5:
                        num = Math.Floor(num * 1000) / 1000;
                        break;
                }
            }
            return string.Format("{0:" + format + "}", num);
        }

        #endregion

        #region 登録時チェック処理

        /// <summary>
        /// 登録時チェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool CheckRegist(object sender, EventArgs e)
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageUtility msgUtil = new MessageUtility();
                this.errorMessages = new List<string>();

                // 必須チェック
                if (sender is GcCustomAlphaNumTextBoxCell)
                {
                    GcCustomAlphaNumTextBoxCell item = sender as GcCustomAlphaNumTextBoxCell;
                    int rowIdx = item.RowIndex;

                    if (item.Name.Equals(Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD))
                    {
                        Row row = this.form.Ichiran.Rows[rowIdx];

                        // 一旦、エラー表示を解除する
                        row[Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD].Style.BackColor = Constans.NOMAL_COLOR;
                        row[Const.UnchinTankaHoshuConstans.UNIT_CD].Style.BackColor = Constans.NOMAL_COLOR;
                        row[Const.UnchinTankaHoshuConstans.SHASHU_CD].Style.BackColor = Constans.NOMAL_COLOR;
                        row[Const.UnchinTankaHoshuConstans.TANKA].Style.BackColor = Constans.NOMAL_COLOR;

                        // 何か入力されている行のみチェックする
                        // 削除チェックされている行はチェックしない
                        var tsukiDeleteFlg = row["DELETE_FLG"].Value as bool?;
                        if (
                            !(tsukiDeleteFlg.HasValue && tsukiDeleteFlg.Value) &&
                            (
                                !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD].Value) ||
                                !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.UnchinTankaHoshuConstans.UNIT_CD].Value) ||
                                !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.UnchinTankaHoshuConstans.SHASHU_CD].Value) ||
                                !this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.UnchinTankaHoshuConstans.TANKA].Value)
                            )
                        )
                        {
                            if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD].Value))
                            {
                                errorMessages.Add(string.Format(msgUtil.GetMessage("E020").MESSAGE, "運賃品名"));
                                row[Const.UnchinTankaHoshuConstans.UNCHIN_HINMEI_CD].Style.BackColor = Constans.ERROR_COLOR;
                            }
                            if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.UnchinTankaHoshuConstans.UNIT_CD].Value))
                            {
                                errorMessages.Add(string.Format(msgUtil.GetMessage("E020").MESSAGE, "単位"));
                                row[Const.UnchinTankaHoshuConstans.UNIT_CD].Style.BackColor = Constans.ERROR_COLOR;
                            }
                            if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.UnchinTankaHoshuConstans.SHASHU_CD].Value))
                            {
                                errorMessages.Add(string.Format(msgUtil.GetMessage("E020").MESSAGE, "車種"));
                                row[Const.UnchinTankaHoshuConstans.SHASHU_CD].Style.BackColor = Constans.ERROR_COLOR;
                            }
                            if (this.ValueIsNullOrDBNullOrWhiteSpace(row[Const.UnchinTankaHoshuConstans.TANKA].Value))
                            {
                                errorMessages.Add(string.Format(msgUtil.GetMessage("E020").MESSAGE, "単価"));
                                row[Const.UnchinTankaHoshuConstans.TANKA].Style.BackColor = Constans.ERROR_COLOR;
                            }
                        }
                    }
                }
                this.errorMessagesUniq();
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckRegist", ex);
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
        /// アラートの内容の重複確認クリア
        /// </summary>
        public void errorMessagesClear()
        {
            messageList.Clear();
        }

        /// <summary>
        /// アラートの内容の重複削除
        /// </summary>
        /// <param name="e"></param>
        private void errorMessagesUniq()
        {
            List<string> deleteList = new List<string>();

            foreach (string item in errorMessages)
            {
                if (messageList.Contains(item))
                {
                    deleteList.Add(item);
                }
                else
                {
                    messageList.Add(item);
                }
            }
            foreach (string item in deleteList)
            {
                errorMessages.Remove(item);
            }
        }

        #endregion

        #region 重複チェック

        /// <summary>
        /// 運賃品名単価の重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            bool result = true;
            try
            {
                LogUtility.DebugMethodStart();

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.UnchinTankaHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt = ((DataTable)this.form.Ichiran.DataSource).GetChanges();
                if (dt == null || dt.Rows.Count <= 0)
                {
                    LogUtility.DebugMethodEnd(result);
                    return result;
                }

                UnchinTankaHoshuValidator vali = new UnchinTankaHoshuValidator();
                //削除したので最新を取得
                M_UNCHIN_TANKA searchParams = new M_UNCHIN_TANKA();
                searchParams.UNPAN_GYOUSHA_CD = this.SearchString.UNPAN_GYOUSHA_CD;
                this.SearchResultAll = dao.GetDataByUnpanGyoushaCdMinCols(searchParams);
                result = vali.UnchinTankaValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);
            }
            catch (SQLRuntimeException ex1)
            {
                LogUtility.Error("DuplicationCheck", ex1);
                this.form.errmessage.MessageBoxShow("E093", "");
                result = false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                result = false;
            }
            LogUtility.DebugMethodEnd(result);
            return result;
        }

        #endregion

        #region 値がNULLまたはDBNullまたは空白であるかどうかをチェックする。

        /// <summary>
        /// 値がNULLまたはDBNullまたは空白であるかどうかをチェックする。
        /// </summary>
        /// <param name="objValue">値</param>
        /// <returns>true:空白　false:空白でない</returns>
        private bool ValueIsNullOrDBNullOrWhiteSpace(object objValue)
        {
            return objValue == null || DBNull.Value.Equals(objValue) || string.IsNullOrWhiteSpace(objValue.ToString());
        }

        #endregion

        #region 画面を閉じる際の運搬業者チェック（OKなら値を保存）
        public bool CheckUnpanGyousha(string cd)
        {
            bool ret = true;
            LogUtility.DebugMethodStart(cd);
            if (!string.IsNullOrEmpty(cd))
            {
                cd = cd.PadLeft(6, '0');
            }
            M_GYOUSHA gyousha = new M_GYOUSHA();
            gyousha.GYOUSHA_CD = this.form.UNPAN_GYOUSHA_CD.Text;
            gyousha = this.gyoushaDao.GetUnpanGyoushaData(gyousha);
            if (gyousha == null)
            {
                ret = false;
            }
            LogUtility.DebugMethodEnd();
            return ret;
        }
        #endregion
    }
}