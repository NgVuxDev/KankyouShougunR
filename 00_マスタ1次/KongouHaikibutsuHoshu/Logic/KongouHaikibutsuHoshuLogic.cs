// $Id: KongouHaikibutsuHoshuLogic.cs 49662 2015-05-14 11:05:54Z d-sato $
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using KongouHaikibutsuHoshu.APP;
using KongouHaikibutsuHoshu.Dto;
using KongouHaikibutsuHoshu.Validator;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.APP.PopUp.Base;
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
using Shougun.Core.Common.BusinessCommon.Dto;
using Shougun.Core.Common.BusinessCommon.Logic;

namespace KongouHaikibutsuHoshu.Logic
{
    /// <summary>
    /// 混合廃棄物保守画面のビジネスロジック
    /// </summary>
    public class KongouHaikibutsuHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "KongouHaikibutsuHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_DATA_SQL = "KongouHaikibutsuHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_KONGOUHAIKIBUTSU_DATA_SQL = "KongouHaikibutsuHoshu.Sql.GetKongouHaikibutsuDataSql.sql";

        private readonly string GET_HAIKI_KBN_DATA_SQL = "KongouHaikibutsuHoshu.Sql.GetHaikiKbnDataSql.sql";

        private readonly string UPDATE_KONGOU_HAIKIBUTSU_DATA_SQL = "KongouHaikibutsuHoshu.Sql.UpdateKongouHaikibutsuDataSql.sql";

        /// <summary>
        /// 混合廃棄物保守画面Form
        /// </summary>
        private KongouHaikibutsuHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private DataDto[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 混合廃棄物のDao
        /// </summary>
        private IM_KONGOU_HAIKIBUTSUDao dao;

        /// <summary>
        /// 廃棄物区分のDao
        /// </summary>
        private IM_HAIKI_KBNDao HaikiKbnDao;

        /// <summary>
        /// 混合種類のDao
        /// </summary>
        private IM_KONGOU_SHURUIDao daoKongouShurui;

        /// <summary>
        /// 検索条件(Dto)
        /// </summary>
        public KongouHaikibutsuHoshuDto kongouHaikibutsuHoshuDto { get; set; }

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

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
        /// 検索結果(全件)
        /// </summary>
        public DataTable SearchResultAll { get; set; }

        /// <summary>
        /// 検索条件
        /// </summary>
        public M_KONGOU_HAIKIBUTSU SearchString { get; set; }

        /// <summary>
        /// 検索結果(廃棄物区分)
        /// </summary>
        public DataTable SearchResultHaikiKbn { get; set; }

        /// <summary>
        /// 検索結果(廃棄物種類)
        /// </summary>
        public DataTable SearchResultHaikiShurui { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public KongouHaikibutsuHoshuLogic(KongouHaikibutsuHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_KONGOU_HAIKIBUTSUDao>();

            this.HaikiKbnDao = DaoInitUtility.GetComponent<IM_HAIKI_KBNDao>();

            this.daoKongouShurui = DaoInitUtility.GetComponent<IM_KONGOU_SHURUIDao>();

            this.kongouHaikibutsuHoshuDto = new KongouHaikibutsuHoshuDto();

            this.daoSysInfo = DaoInitUtility.GetComponent<IM_SYS_INFODao>();

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

                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                // 廃棄物区分指定の初期設定
                this.form.HAIKI_KBN_CD.Text = Properties.Settings.Default.HaikiKbnCd_Text;
                this.form.HAIKI_KBN_NAME_RYAKU.Text = Properties.Settings.Default.HaikiKbnName_Text;

                // 混合種類指定の初期設定
                this.form.KONGOU_SHURUI_CD.Text = Properties.Settings.Default.KongouShuruiCd_Text;
                this.form.KONGOU_SHURUI_NAME_RYAKU.Text = Properties.Settings.Default.KongouShuruiName_Text;
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1979

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
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                // プロパティ設定有無チェック
                //  混合廃棄物マスタの条件で検索
                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_DATA_SQL
                                                            , this.kongouHaikibutsuHoshuDto.KongouHaikibutsuSearchString);

                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_KONGOUHAIKIBUTSU_DATA_SQL, this.kongouHaikibutsuHoshuDto.KongouHaikibutsuSearchString);

                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                // 廃棄物区分指定の初期設定
                Properties.Settings.Default.HaikiKbnCd_Text = this.form.HAIKI_KBN_CD.Text;
                Properties.Settings.Default.HaikiKbnName_Text = this.form.HAIKI_KBN_NAME_RYAKU.Text;

                // 混合種類指定の初期設定
                Properties.Settings.Default.KongouShuruiCd_Text = this.form.KONGOU_SHURUI_CD.Text;
                Properties.Settings.Default.KongouShuruiName_Text = this.form.KONGOU_SHURUI_NAME_RYAKU.Text;

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

                var entityList = new M_KONGOU_HAIKIBUTSU[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_KONGOU_HAIKIBUTSU();
                }

                // 適用開始終了日を援用するために廃棄物種類を取得する
                M_KONGOU_SHURUI cond = new M_KONGOU_SHURUI();
                cond.HAIKI_KBN_CD = Int16.Parse(this.form.HAIKI_KBN_CD.Text);
                cond.KONGOU_SHURUI_CD = this.form.KONGOU_SHURUI_CD.Text;
                M_KONGOU_SHURUI shu = this.daoKongouShurui.GetDataByCd(cond);

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_KONGOU_HAIKIBUTSU>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.KongouHaikibutsuHoshuConstans.TIME_STAMP))
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

                var KongouHaikibutsuEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<DataDto> addList = new List<DataDto>();
                foreach (var KongouHaikibutsuEntity in KongouHaikibutsuEntityList)
                {
                    DataDto data = new DataDto();
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.IsNewRow)
                        {
                            continue;
                        }
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.KongouHaikibutsuHoshuConstans.HAIKI_SHURUI_CD) && n.Value.ToString().Equals(KongouHaikibutsuEntity.HAIKI_SHURUI_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.KongouHaikibutsuHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            KongouHaikibutsuEntity.SetValue(this.form.HAIKI_KBN_CD);
                            KongouHaikibutsuEntity.SetValue(this.form.KONGOU_SHURUI_CD);
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), KongouHaikibutsuEntity);
                            data.entity = KongouHaikibutsuEntity;
                            DataRow tempRowData = ((DataTable)this.form.Ichiran.DataSource).Rows[row.Index];
                            if (tempRowData != null)
                            {
                                if (tempRowData["UK_HAIKI_KBN_CD"] != DBNull.Value)
                                {
                                    data.updateKey.HAIKI_KBN_CD = (Int16)tempRowData["UK_HAIKI_KBN_CD"];
                                }
                                data.updateKey.KONGOU_SHURUI_CD = tempRowData["UK_KONGOU_SHURUI_CD"] != DBNull.Value ? tempRowData["UK_KONGOU_SHURUI_CD"].ToString() : null;
                                data.updateKey.HAIKI_SHURUI_CD = tempRowData["UK_HAIKI_SHURUI_CD"] != DBNull.Value ? tempRowData["UK_HAIKI_SHURUI_CD"].ToString() : null;
                            }
                            addList.Add(data);
                            break;
                        }
                    }
                }

                this.form.Ichiran.DataSource = preDt;

                this.entitys = addList.ToArray();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("CreateEntity", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd();
                return true;
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
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1979

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
        /// 廃棄物区分CD、混合種類CD、廃棄物種類CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                KongouHaikibutsuHoshuValidator vali = new KongouHaikibutsuHoshuValidator();
                bool result = vali.HaikikunCDValidator(this.form.Ichiran, this.SearchResult, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd();

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }

        /// <summary>
        /// プレビュー
        /// </summary>
        public bool Preview()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C011", "混合廃棄物一覧表");

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

                SetSearchString();

                LogUtility.DebugMethodEnd();
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return true;
            }
        }

        public bool CheckHiritsu()
        {
            try
            {
                LogUtility.DebugMethodStart();
                bool result = true;

                decimal per = 0;
                int count = 0;
                foreach (Row row in this.form.Ichiran.Rows)
                {
                    if (row[Const.KongouHaikibutsuHoshuConstans.DELETE_FLG].Value != DBNull.Value && (bool)row[Const.KongouHaikibutsuHoshuConstans.DELETE_FLG].Value) continue;
                    decimal p;
                    if (row["HAIKI_HIRITSU"] != null && row["HAIKI_HIRITSU"].Value != DBNull.Value && decimal.TryParse(row["HAIKI_HIRITSU"].Value.ToString(), out p))
                    {
                        per += p;
                    }

                    if (!row.IsNewRow)
                    {
                        count++;
                    }
                }
                if (per != 100 && count != 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E073", "廃棄物比率");
                    result = false;
                }

                LogUtility.DebugMethodEnd(result);
                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CheckHiritsu", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd();
                return false;
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
            // 重複エラー時用のメッセージで使用
            string dispHaikiShuruiCd = string.Empty;

            try
            {
                LogUtility.DebugMethodStart(errorFlag);

                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (DataDto data in this.entitys)
                        {
                            M_KONGOU_HAIKIBUTSU haikiEntity = this.dao.GetDataByCd(data.updateKey);
                            dispHaikiShuruiCd = string.Empty;

                            if (haikiEntity == null)
                            {
                                this.dao.Insert(data.entity);
                            }
                            else
                            {
                                dispHaikiShuruiCd = data.entity.HAIKI_SHURUI_CD;
                                this.dao.UpdateBySqlFile(this.UPDATE_KONGOU_HAIKIBUTSU_DATA_SQL, data.entity, data.updateKey);
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
                var tempEx = ex2.Args[0] as SqlException;
                if (!string.IsNullOrEmpty(dispHaikiShuruiCd)
                    && (tempEx != null && tempEx.Number == Constans.SQL_EXCEPTION_NUMBER_DUPLICATE))
                {
                    this.form.errmessage.MessageBoxShow("E259", "廃棄物比率または備考", "・廃棄物種類CD：" + dispHaikiShuruiCd);
                }
                else
                {
                    this.form.errmessage.MessageBoxShow("E093", "");
                }
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
                        foreach (DataDto data in this.entitys)
                        {
                            M_KONGOU_HAIKIBUTSU haikiEntity = this.dao.GetDataByCd(data.entity);

                            if (haikiEntity != null)
                            {
                                this.dao.Delete(data.entity);
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

            KongouHaikibutsuHoshuLogic localLogic = other as KongouHaikibutsuHoshuLogic;
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
                DataRow[] dr = this.SearchResult.Select(String.Format("KONGOU_SHURUI_CD = '{0}'", this.form.KONGOU_SHURUI_CD.Text));

                DataTable table = this.SearchResult.Clone();

                foreach (DataRow row in dr)
                {
                    table.ImportRow(row);
                }

                if (dr == null)
                {
                    return false;
                }

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.DataSource = table;

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M228", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.DispReferenceMode();
                }
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

            //混合種類CD編集終了イベント
            this.form.KONGOU_SHURUI_CD.Validating += new System.ComponentModel.CancelEventHandler(KONGOU_SHURUI_CD_Validating);
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
            M_KONGOU_HAIKIBUTSU entity = new M_KONGOU_HAIKIBUTSU();

            if (!string.IsNullOrEmpty(this.form.HAIKI_KBN_CD.Text))
            {
                // 検索条件の設定
                entity.SetValue(this.form.HAIKI_KBN_CD);
            }

            if (!string.IsNullOrEmpty(this.form.KONGOU_SHURUI_CD.Text))
            {
                // 検索条件の設定
                entity.SetValue(this.form.KONGOU_SHURUI_CD);
            }
            kongouHaikibutsuHoshuDto.KongouHaikibutsuSearchString = entity;
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
            this.form.HAIKI_KBN_CD.Text = string.Empty;
            this.form.HAIKI_KBN_NAME_RYAKU.Text = string.Empty;
            this.form.KONGOU_SHURUI_CD.Text = string.Empty;
            this.form.KONGOU_SHURUI_NAME_RYAKU.Text = string.Empty;

            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
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
        /// 混合種類CD編集終了イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KONGOU_SHURUI_CD_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!string.IsNullOrEmpty(this.form.KONGOU_SHURUI_CD.Text))
            {
                M_KONGOU_SHURUI entity = new M_KONGOU_SHURUI();
                if (!string.IsNullOrEmpty(this.form.HAIKI_KBN_CD.Text))
                {
                    entity.SetValue(this.form.HAIKI_KBN_CD);
                }
                entity.SetValue(this.form.KONGOU_SHURUI_CD);
                var returnEntitys = daoKongouShurui.GetAllValidData(entity);
                if (returnEntitys.Length == 0)
                {
                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("E020", "混合種類");
                    e.Cancel = true;
                }
                else
                {
                    if (returnEntitys.Length == 1)
                    {
                        this.form.KONGOU_SHURUI_NAME_RYAKU.Text = returnEntitys[0].KONGOU_SHURUI_NAME_RYAKU;
                    }
                    else
                    {
                        SuperPopupForm classinfo;
                        // 呼出しDLLを作成
                        var popup = new PopupSetting();
                        var methodSetting = popup.GetSetting(this.form.KONGOU_SHURUI_CD.PopupWindowName);
                        var assemblyName = methodSetting.AssemblyName;
                        var calassNameSpace = methodSetting.ClassNameSpace;
                        var assembltyName = assemblyName + ".dll";
                        var m = Assembly.LoadFrom(assembltyName);
                        var objectHandler = Activator.CreateInstanceFrom(m.CodeBase, assemblyName + "." + calassNameSpace);
                        classinfo = objectHandler.Unwrap() as SuperPopupForm;
                        classinfo.WindowId = WINDOW_ID.M_KONGOU_SHURUI;
                        classinfo.IsMasterAccessStartUp = true;
                        classinfo.Entity = entity;
                        classinfo.popupWindowSetting = this.form.KONGOU_SHURUI_CD.popupWindowSetting;
                        //ポップアップ表示
                        DialogResult result = DialogResult.None;
                        result = classinfo.ShowDialog();
                        bool emptyflg = true;
                        if (result == DialogResult.OK && classinfo.ReturnParams != null)
                        {
                            int i = 0;
                            foreach (var returnParam in classinfo.ReturnParams.Values)
                            {
                                if (i == 0 && !string.IsNullOrEmpty(Convert.ToString(returnParam[0].Value)))
                                {
                                    this.form.HAIKI_KBN_CD.Text = returnParam[0].Value.ToString().PadLeft(2, '0');
                                    emptyflg = false;
                                }
                                if (i == 1 && !string.IsNullOrEmpty(Convert.ToString(returnParam[0].Value)))
                                {
                                    this.form.HAIKI_KBN_NAME_RYAKU.Text = returnParam[0].Value.ToString();
                                    emptyflg = false;
                                }
                                if (i == 3 && !string.IsNullOrEmpty(Convert.ToString(returnParam[0].Value)))
                                {
                                    this.form.KONGOU_SHURUI_NAME_RYAKU.Text = returnParam[0].Value.ToString();
                                    emptyflg = false;
                                }
                                i++;
                            }
                        }
                        if (emptyflg)
                        {
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 電子廃棄物種類CDの存在性チェック
        /// </summary>
        /// <returns></returns>
        public bool ExistCheck(int rowIndex, int cellIndex)
        {
            try
            {
                var DsMasterLogic = new DenshiMasterDataLogic();
                var dto = new DenshiSearchParameterDtoCls();
                var cell = this.form.Ichiran.Rows[rowIndex].Cells[cellIndex] as GcCustomTextBoxCell;
                var msgLogic = new MessageBoxShowLogic();
                // 廃棄物種類
                if (string.IsNullOrEmpty(cell.GetResultText()))
                {
                    // 廃棄物種類名をクリア
                    this.form.Ichiran.Rows[rowIndex].Cells["HAIKI_SHURUI_NAME_RYAKU"].Value = string.Empty;
                    return true;
                }

                this.form.Ichiran.Rows[rowIndex].Cells[cellIndex].Value =
                    this.form.Ichiran.Rows[rowIndex].Cells[cellIndex].Value.ToString().PadLeft(7, '0').ToUpper();

                dto.HAIKISHURUICD = cell.GetResultText();
                dto.EDI_MEMBER_ID = string.Empty;
                var haikiShuruiDt = DsMasterLogic.GetDenshiHaikiShuruiData(dto);
                if (haikiShuruiDt == null || haikiShuruiDt.Rows.Count < 1)
                {
                    this.form.Ichiran.Rows[rowIndex].Cells["HAIKI_SHURUI_NAME_RYAKU"].Value = string.Empty;
                    msgLogic.MessageBoxShow("E020", "廃棄種類コード");
                    return false;
                }
                else
                {
                    // キーが1つなので、複数ヒットはないはず
                    this.form.Ichiran.Rows[rowIndex].Cells["HAIKI_SHURUI_NAME_RYAKU"].Value
                        = haikiShuruiDt.Rows[0]["HAIKI_SHURUI_NAME"];
                }
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
                this.form.errmessage.MessageBoxShow("E245");
                LogUtility.DebugMethodEnd();
                return false;
            }
        }
    }
}