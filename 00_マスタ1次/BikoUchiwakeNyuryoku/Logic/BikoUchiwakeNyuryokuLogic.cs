// $Id: BikoUchiwakeNyuryokuLogic.cs 54689 2015-07-06 06:23:33Z y-hosokawa@takumi-sys.co.jp $
using BikoUchiwakeNyuryoku.APP;
using BikoUchiwakeNyuryoku.Validator;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterCommon.Utility;
using r_framework.APP.Base;
using r_framework.Const;
using r_framework.Dao;
using r_framework.Entity;
using r_framework.Logic;
using r_framework.Setting;
using r_framework.Utility;
using Seasar.Dao;
using Seasar.Framework.Exceptions;
using Seasar.Quill.Attrs;
using Shougun.Core.Common.BusinessCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;

using System.Windows.Forms;

namespace BikoUchiwakeNyuryoku.Logic
{
    /// <summary>
    /// 銀行支店保守画面のビジネスロジック
    /// </summary>
    public class BikoUchiwakeNyuryokuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "BikoUchiwakeNyuryoku.Setting.ButtonSetting.xml";

        private readonly string GET_BIKO_UCHIWAKE_NYURYOKU_DATA_SQL = "BikoUchiwakeNyuryoku.Sql.GetBikoUchiwakeNyuryokudataSql.sql";

        private readonly string GET_ICHIRAN_BIKO_UCHIWAKE_DATA_SQL = "BikoUchiwakeNyuryoku.Sql.GetIchiranDataSql.sql";

        private readonly string GET_BIKO_DATA_SQL = "BikoUchiwakeNyuryoku.Sql.GetBikodataSql.sql";

        //private readonly string CHECK_DELETE_BANK_SHITEN_SQL = "BikoUchiwakeNyuryoku.Sql.CheckDeleteBankShitenSql.sql";

        /// <summary>
        /// 銀行支店保守画面Form
        /// </summary>
        private BikoUchiwakeNyuryokuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        private M_BIKO_UCHIWAKE_NYURYOKU[] entitys;

        private bool isAllSearch;

        /// <summary>
        /// 銀行支店のDao
        /// </summary>
        private IM_BIKO_UCHIWAKE_NYURYOKUDao dao;

        /// <summary>
        /// 銀行のDao
        /// </summary>
        public IM_BIKO_PATAN_NYURYOKUDao bikoPatanDao;

        //202050409
        public IM_BIKO_SENTAKUSHI_NYURYOKUDao bikoSentaDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        /// <summary>
        /// 重複チェックするか判断するフラグ
        /// </summary>
        internal bool isJuuhukuCheck = true;

        // VUNGUYEN 20150525 #1294 START
        public Cell cell;

        // VUNGUYEN 20150525 #1294 END

        // 20150922 katen #12048 「システム日付」の基準作成、適用 start
        internal MasterBaseForm parentForm;

        // 20150922 katen #12048 「システム日付」の基準作成、適用 end

        public bool isRenkeiError = false;
        public int errorCell = 0;
        public int errorRow = 0;

        /// <summary>
        /// Before 銀行支店CD
        /// </summary>
        public string beforeBikoCd = string.Empty;

        #endregion フィールド

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
        public M_BIKO_UCHIWAKE_NYURYOKU SearchString { get; set; }

        /// <summary>
        /// 検索結果(銀行)
        /// </summary>
        public DataTable SearchResultBiko { get; set; }

        //20250411
        public DataTable newDt { get; set; }

        #endregion プロパティ

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public BikoUchiwakeNyuryokuLogic(BikoUchiwakeNyuryokuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;

            this.dao = DaoInitUtility.GetComponent<IM_BIKO_UCHIWAKE_NYURYOKUDao>();
            this.bikoPatanDao = DaoInitUtility.GetComponent<IM_BIKO_PATAN_NYURYOKUDao>();
            this.bikoSentaDao = DaoInitUtility.GetComponent<IM_BIKO_SENTAKUSHI_NYURYOKUDao>();
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
                // 20150922 katen #12048 「システム日付」の基準作成、適用 start
                this.parentForm = (MasterBaseForm)this.form.Parent;
                // 20150922 katen #12048 「システム日付」の基準作成、適用 end
                // ボタンのテキストを初期化
                this.ButtonInit();
                // イベントの初期化処理
                this.EventInit();

                this.allControl = this.form.allControl;

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;
                this.form.BIKO_KBN_CD.Text = Properties.Settings.Default.BikosValue_Text;
                this.form.BIKO_NAME_RYAKU.Text = Properties.Settings.Default.BikosName_Text;

                //this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;

                //if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                //{
                //    this.SetHyoujiJoukenInit();
                //}
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                this.EditableToPrimaryKey();

                this.form.Ichiran.AllowUserToAddRows = true; // thongh 2015/12/28 #1983
                //this.setOnlineBankVisible();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 表示条件初期値設定処理
        /// </summary>
        //public void SetHyoujiJoukenInit()
        //{
        //    LogUtility.DebugMethodStart();

        //    if (this.entitySysInfo != null)
        //    {
        //        this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = this.entitySysInfo.ICHIRAN_HYOUJI_JOUKEN_DELETED.Value;
        //    }
        //    LogUtility.DebugMethodEnd();
        //}

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
            //parentForm.bt_func4.Enabled = false;
            parentForm.bt_func6.Enabled = true;
            parentForm.bt_func9.Enabled = false;
        }

        /// <summary>
        /// キーダウンイベントを一旦受け付ける
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            LogUtility.DebugMethodStart(sender, e);

            if (e.KeyCode == Keys.F12)
            {
                this.form.Ichiran.RowValidating -= new EventHandler<CellCancelEventArgs>(this.form.Ichiran_RowValidating);
                this.form.FormClose(sender, e);
            }

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

                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_BIKO_UCHIWAKE_DATA_SQL
                                                               , this.SearchString); //, this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_BIKO_UCHIWAKE_DATA_SQL
                                                               , this.SearchString); //, this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked

                M_BIKO_UCHIWAKE_NYURYOKU cond = new M_BIKO_UCHIWAKE_NYURYOKU();
                cond.BIKO_CD = this.form.BIKO_KBN_CD.Text;
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_BIKO_UCHIWAKE_NYURYOKU_DATA_SQL, cond);
                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;
                Properties.Settings.Default.BikosValue_Text = this.form.BIKO_KBN_CD.Text;
                Properties.Settings.Default.BikosName_Text = this.form.BIKO_NAME_RYAKU.Text;

                //Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.Save();

                int count = this.SearchResult.Rows == null ? 0 : 1;

                LogUtility.DebugMethodEnd(count);
                return count;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("Search", ex2);
                this.form.errmessage.MessageBoxShow("E093", string.Empty);
                LogUtility.DebugMethodEnd(-1);
                return -1;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Search", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
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

                var entityList = new M_BIKO_UCHIWAKE_NYURYOKU[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_BIKO_UCHIWAKE_NYURYOKU();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_BIKO_UCHIWAKE_NYURYOKU>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();

                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.BikoUchiwakeNyuryokuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                this.newDt = preDt.Copy();

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var bikouchiwakeEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_BIKO_UCHIWAKE_NYURYOKU> addList = new List<M_BIKO_UCHIWAKE_NYURYOKU>();
                foreach (var bikouchiwakeEntity in bikouchiwakeEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.BikoUchiwakeNyuryokuConstans.BIKO_CD) && n.Value.ToString().Equals(bikouchiwakeEntity.BIKO_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.BikoUchiwakeNyuryokuConstans.BIKO_NOTE) && n.Value.ToString().Equals(bikouchiwakeEntity.BIKO_NOTE))))
                        {
                            bikouchiwakeEntity.SetValue(this.form.BIKO_KBN_CD);
                            bikouchiwakeEntity.BIKO_NAME_RYAKU = this.form.BIKO_NAME_RYAKU.Text;
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), bikouchiwakeEntity);
                            addList.Add(bikouchiwakeEntity);
                            break;
                        }
                    }
                }

                this.form.Ichiran.DataSource = preDt;

                this.entitys = addList.ToArray();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CreateEntity", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd(true);
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
                this.form.ClearMesai();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 銀行支店CDの重複チェック
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            LogUtility.DebugMethodStart();

            BikoUchiwakeNyuryokuValidator vali = new BikoUchiwakeNyuryokuValidator();
            bool result = true;
            if (this.isJuuhukuCheck)
            {
                result = vali.BikoUchiwakeNyuryokuCDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);
            }

            LogUtility.DebugMethodEnd();

            return result;
        }

        /// <summary>
        /// 削除できるかどうかチェックする
        /// </summary>
        public bool CheckDelete()
        {
            try
            {
                LogUtility.DebugMethodStart();

                bool ret = true;
                var bikoKbnCd = this.form.BIKO_KBN_CD.Text;
                string[] strList = bikoKbnCd.Split(',');

                DataTable dtTable = dao.GetDataBySqlFileCheck(strList);

                if (strList != null && strList.Length > 0)
                {
                    if (dtTable != null && dtTable.Rows.Count > 0)
                    {
                        string strName = string.Join("\n", dtTable.AsEnumerable()
                                                                      .Select(r => r["NAME"].ToString())
                                                                      .Distinct());
                        strName = "\n" + strName;

                        new MessageBoxShowLogic().MessageBoxShow("E258", "備考パターン", "備考パターンCD", strName);
                        
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
            catch (Exception ex)
            {
                LogUtility.Error("CheckDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(false);
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
                msgLogic.MessageBoxShow("C011", "銀行支店一覧表");

                MessageBox.Show("未実装");

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd(true);
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

                    // VUNGUYEN 20150525 #1294 START
                    CSVFileLogicCustom csvLogic = new CSVFileLogicCustom();
                    // VUNGUYEN 20150525 #1294 END

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    #region 新しいCSV出力利用するように

                    // CSV出力するときだけ名称を変更したいのでココで変更
                    csvLogic.CsvHeaders =
                        CsvUtility.CsvHeaders.CreateCustomHeaders(new string[] { "KOUZA_SHURUI" }, new string[] { "口座種類名" });

                    #endregion 新しいCSV出力利用するように

                    csvLogic.CreateCSVFile(this.form);
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd(true);
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

                ClearConditionF7();
                SetSearchString();

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CancelCondition", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
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

                //CreateEntityFromIchiran();

                //独自チェックの記述例を書く
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_BIKO_UCHIWAKE_NYURYOKU bikouchiwakeEntity in this.entitys)
                        {
                            M_BIKO_UCHIWAKE_NYURYOKU entity = this.dao.GetDataByCd(bikouchiwakeEntity);
                            if (entity == null)
                            {
                                // 削除チェックが付けられている場合は、新規登録を行わない
                                //if (bikouchiwakeEntity.DELETE_FLG)
                                //{
                                //    continue;
                                //}
                                this.dao.Insert(bikouchiwakeEntity);
                            }
                            else
                            {
                                this.dao.Update(bikouchiwakeEntity);
                            }
                        }

                        var binder = new DataBinderLogic<M_BIKO_UCHIWAKE_NYURYOKU>(new M_BIKO_UCHIWAKE_NYURYOKU[0]);
                        var oldList = binder.CreateEntityForDataTable(this.SearchResultCheck);

                        var newKeys = new HashSet<string>(this.newDt.AsEnumerable()
                                                                       .Select(row => $"{row["BIKO_KBN_CD"]}|{row["BIKO_CD"]}")
                                                                       .Where(key => !string.IsNullOrWhiteSpace(key)));

                        var deletedList = oldList.Where(e => !newKeys.Contains($"{e.BIKO_KBN_CD}|{e.BIKO_CD}")).ToList();

                        if (!this.CheckDelete())
                        {
                            return;
                        }

                        foreach (var deleted in deletedList)
                        {
                            this.dao.Delete(deleted);
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
                this.form.errmessage.MessageBoxShow("E080", string.Empty);
                LogUtility.DebugMethodEnd();
                return;
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex2);
                this.form.errmessage.MessageBoxShow("E093", string.Empty);
                LogUtility.DebugMethodEnd();
                return;
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("Regist", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd();
                return;
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
                        foreach (M_BIKO_UCHIWAKE_NYURYOKU bikouchiwakeEntity in this.entitys)
                        {
                            M_BIKO_UCHIWAKE_NYURYOKU entity = this.dao.GetDataByCd(bikouchiwakeEntity);
                            if (entity != null)
                            {
                                this.dao.Update(bikouchiwakeEntity);
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    msgLogic.MessageBoxShow("I001", "削除");
                }

                this.form.RegistErrorFlag = false;
                LogUtility.DebugMethodEnd();
                return;
            }
            catch (NotSingleRowUpdatedRuntimeException ex1)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex1);
                this.form.errmessage.MessageBoxShow("E080", string.Empty);
                LogUtility.DebugMethodEnd();
                return;
            }
            catch (SQLRuntimeException ex2)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex2);
                this.form.errmessage.MessageBoxShow("E093", string.Empty);
                LogUtility.DebugMethodEnd();
                return;
            }
            catch (Exception ex)
            {
                this.form.RegistErrorFlag = true;
                LogUtility.Error("LogicalDelete", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd();
                return;
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

        #endregion 登録/更新/削除

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

            BikoUchiwakeNyuryokuLogic localLogic = other as BikoUchiwakeNyuryokuLogic;
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

        #endregion Equals/GetHashCode/ToString

        /// <summary>
        /// 検索結果を一覧に設定
        /// </summary>
        internal bool SetIchiran()
        {
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

                this.isJuuhukuCheck = false;
                this.form.Ichiran.DataSource = table;
                this.isJuuhukuCheck = true;

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M199", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
                {
                    FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, true);
                }
                else
                {
                    this.DispReferenceMode();
                }
                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetIchiran", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
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

            //全てのキーダウンを一旦受ける
            parentForm.KeyDown += new KeyEventHandler(this.Form_KeyDown);

            //削除ボタン(F4)イベント生成
            //this.form.C_MasterRegist(parentForm.bt_func4);
            //parentForm.bt_func4.Click += new EventHandler(this.form.LogicalDelete);
            //parentForm.bt_func4.ProcessKbn = PROCESS_KBN.DELETE;

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

            //20250410
            parentForm.bt_process1.Click += new EventHandler(this.form.InsertRow);

            parentForm.bt_process2.Click += new EventHandler(this.form.DeleteRow);

            //行バリデーションイベント生成
            this.form.Ichiran.RowValidating += new EventHandler<CellCancelEventArgs>(this.form.Ichiran_RowValidating);
            //検索条件イベント生成
            this.form.CONDITION_VALUE.KeyPress += new KeyPressEventHandler(CONDITION_VALUE_KeyPress);
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
            M_BIKO_UCHIWAKE_NYURYOKU entity = new M_BIKO_UCHIWAKE_NYURYOKU();

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // 検索条件の設定
                    entity.SetValue(this.form.CONDITION_VALUE);
                }
            }
            if (!string.IsNullOrEmpty(this.form.BIKO_KBN_CD.Text))
            {
                // 検索条件の設定
                entity.SetValue(this.form.BIKO_KBN_CD);
            }
            this.SearchString = entity;
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
            this.form.BIKO_KBN_CD.Text = string.Empty;
            this.form.BIKO_NAME_RYAKU.Text = string.Empty;
            Properties.Settings.Default.BikosName_Text = string.Empty;

            //this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }

        #region 20150416 minhhoang edit #1748

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearConditionF7()
        {
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            //this.SetHyoujiJoukenInit();
        }

        #endregion 20150416 minhhoang edit #1748

        /// <summary>
        /// 銀行名称情報の取得
        /// </summary>
        [Transaction]
        public virtual bool SearchBikoName()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResultBiko = bikoPatanDao.GetDataBySqlFile(this.GET_BIKO_DATA_SQL, new M_BIKO_PATAN_NYURYOKU());

                if (this.SearchResultBiko.Rows != null)
                {
                    this.SetBikoName();
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SearchBikoName", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 銀行名称の設定
        /// </summary>
        private void SetBikoName()
        {
            if (this.SearchResultBiko.Rows.Count == 0)
            {
                this.form.BIKO_NAME_RYAKU.Text = string.Empty;
                return;
            }

            foreach (DataRow row in this.SearchResultBiko.Rows)
            {
                this.form.BIKO_NAME_RYAKU.Text = string.Empty;

                if (this.form.BIKO_KBN_CD.Text == row["BIKO_CD"].ToString())
                {
                    this.form.BIKO_NAME_RYAKU.Text = row["BIKO_NAME_RYAKU"].ToString();
                    break;
                }
            }
        }

        /// <summary>
        /// 銀行取得
        /// </summary>
        /// <param name="bankCd">銀行CD</param>
        /// <returns></returns>
        internal M_BIKO_PATAN_NYURYOKU[] getBiko(string bikoCd)
        {
            var bikoCondition = new M_BIKO_PATAN_NYURYOKU();
            bikoCondition.BIKO_CD = bikoCd;
            return this.bikoPatanDao.GetAllValidData(bikoCondition);
        }

        /// <summary>
        /// ポップアップ判定処理
        /// </summary>
        /// <param name="e"></param>
        //public bool CheckPopup(KeyEventArgs e)
        //{
        //    try
        //    {
        //        LogUtility.DebugMethodStart(e);

        //        if (e.KeyCode == Keys.Space)
        //        {
        //            if (this.form.Ichiran.Columns[this.form.Ichiran.CurrentCell.CellIndex].Name.Equals(Const.BikoUchiwakeNyuryokuConstans.KOUZA_SHURUI_CD))
        //            {
        //                MasterKyoutsuPopupForm form = new MasterKyoutsuPopupForm();
        //                DataTable dt = new DataTable();
        //                dt.Columns.Add("CD", typeof(string));
        //                dt.Columns.Add("VALUE", typeof(string));
        //                dt.Columns[0].ReadOnly = true;
        //                dt.Columns[1].ReadOnly = true;
        //                DataRow row;
        //                row = dt.NewRow();
        //                row["CD"] = "1";
        //                row["VALUE"] = "普通預金";
        //                dt.Rows.Add(row);
        //                row = dt.NewRow();
        //                row["CD"] = "2";
        //                row["VALUE"] = "当座預金";
        //                dt.Rows.Add(row);
        //                row = dt.NewRow();
        //                row["CD"] = "9";
        //                row["VALUE"] = "その他";
        //                dt.Rows.Add(row);
        //                form.table = dt;
        //                //form.title = "口座種類検索";
        //                //form.headerList = new List<string>();
        //                //form.headerList.Add("口座種類CD");
        //                //form.headerList.Add("口座種類名");
        //                form.PopupTitleLabel = "口座種類検索";
        //                form.PopupGetMasterField = "CD,VALUE";
        //                form.PopupDataHeaderTitle = new string[] { "口座種類CD", "口座種類名" };
        //                form.ShowDialog();
        //                if (form.ReturnParams != null)
        //                {
        //                    this.form.Ichiran.EditingControl.Text = form.ReturnParams[0][0].Value.ToString();
        //                    this.form.Ichiran[this.form.Ichiran.CurrentCell.RowIndex, Const.BikoUchiwakeNyuryokuConstans.KOUZA_SHURUI_CD].Value = form.ReturnParams[0][0].Value.ToString();
        //                    this.form.Ichiran[this.form.Ichiran.CurrentCell.RowIndex, Const.BikoUchiwakeNyuryokuConstans.KOUZA_SHURUI].Value = form.ReturnParams[1][0].Value.ToString();
        //                }
        //            }
        //        }

        //        LogUtility.DebugMethodEnd(false);
        //        return false;
        //    }
        //    catch (Exception ex)
        //    {
        //        LogUtility.Error("CheckPopup", ex);
        //        this.form.errmessage.MessageBoxShow("E245", "");
        //        LogUtility.DebugMethodEnd(true);
        //        return true;
        //    }
        //}

        /// <summary>
        /// 入力バリデーション
        /// </summary>
        /// <param name="e"></param>
        public bool Ichiran_CellValidating(object sender, CellValidatingEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                //20250409
                if (e.CellName.Equals(Const.BikoUchiwakeNyuryokuConstans.BIKO_CD))
                {
                    if (e.FormattedValue != null && !string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        M_BIKO_SENTAKUSHI_NYURYOKU c = new M_BIKO_SENTAKUSHI_NYURYOKU();
                        c.BIKO_CD = e.FormattedValue.ToString().PadLeft(3, '0');
                        M_BIKO_SENTAKUSHI_NYURYOKU[] bikoSenta = this.bikoSentaDao.GetAllValidData(c);

                        if (bikoSenta != null && bikoSenta.Length > 0)
                        {
                            this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = bikoSenta[0].BIKO_NOTE;
                        }
                        else
                        {
                            msgLogic.MessageBoxShow("E020", string.Empty);
                            e.Cancel = true;
                            if (this.form.Ichiran.EditingControl != null)
                            {
                                ((TextBoxEditingControl)this.form.Ichiran.EditingControl).SelectAll();
                            }
                        }
                    }
                    else
                    {
                        this.form.Ichiran[e.RowIndex, e.CellIndex + 1].Value = string.Empty;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_CellValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 行バリデーション
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public bool Ichiran_RowValidating(object sender, CellCancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                bool isNoErr = this.DuplicationCheck();
                if (!isNoErr)
                {
                    e.Cancel = true;

                    GcMultiRow gc = sender as GcMultiRow;
                    if (gc != null && gc.EditingControl != null)
                    {
                        if (gc.EditingControl is TextBoxEditingControl)
                        {
                            ((TextBoxEditingControl)gc.EditingControl).SelectAll();
                        }
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Ichiran_RowValidating", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// check valid renkei CD
        /// </summary>
        /// <param name="currentRow">input row</param>
        /// <returns>true is valid</returns>
        //private bool CheckValidRenkeiCd(Row currentRow)
        //{
        //    if (string.IsNullOrEmpty(Convert.ToString(currentRow.Cells[Const.BikoUchiwakeNyuryokuConstans.RENKEI_CD].Value))
        //        || string.IsNullOrEmpty(Convert.ToString(currentRow.Cells[Const.BikoUchiwakeNyuryokuConstans.BANK_SHITEN_CD].Value)))
        //    {
        //        return true;
        //    }

        //    foreach (Row row in this.form.Ichiran.Rows)
        //    {
        //        if (row.IsNewRow)
        //        {
        //            continue;
        //        }
        //        if (!string.IsNullOrEmpty(Convert.ToString(row.Cells[Const.BikoUchiwakeNyuryokuConstans.RENKEI_CD].Value))
        //            && row.Cells[Const.BikoUchiwakeNyuryokuConstans.RENKEI_CD].Value.ToString().Equals(currentRow.Cells[Const.BikoUchiwakeNyuryokuConstans.RENKEI_CD].Value.ToString())
        //            && !row.Cells[Const.BikoUchiwakeNyuryokuConstans.BANK_SHITEN_CD].Value.ToString().Equals(currentRow.Cells[Const.BikoUchiwakeNyuryokuConstans.BANK_SHITEN_CD].Value.ToString()))
        //        {
        //            return false;
        //        }
        //    }

        //    IEnumerable<DataRow> enumRow = this.SearchResultCheck.AsEnumerable();
        //    IEnumerable<DataRow> enumRowAll = this.SearchResultAll.AsEnumerable();

        //    var rows = enumRowAll.Except(enumRow, new DataRowBikoUchiwakeNyuryokuCompare());

        //    foreach (DataRow row in rows)
        //    {
        //        if (!string.IsNullOrEmpty(Convert.ToString(row[Const.BikoUchiwakeNyuryokuConstans.RENKEI_CD]))
        //            && row[Const.BikoUchiwakeNyuryokuConstans.RENKEI_CD].ToString().Equals(currentRow.Cells[Const.BikoUchiwakeNyuryokuConstans.RENKEI_CD].Value.ToString())
        //            && !row[Const.BikoUchiwakeNyuryokuConstans.BANK_SHITEN_CD].ToString().Equals(currentRow.Cells[Const.BikoUchiwakeNyuryokuConstans.BANK_SHITEN_CD].Value.ToString()))
        //        {
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        /// <summary>
        /// 入力バリデーション
        /// </summary>
        /// <param name="e"></param>
        public void Ichiran_CellValidated(object sender, CellEventArgs e)
        {
            //LogUtility.DebugMethodStart(sender, e);

            //if (e.CellName.Equals(Const.BikoUchiwakeNyuryokuConstans.BANK_SHITEN_CD))
            //{
            //    if (!this.beforeBikoCd.Equals(Convert.ToString(this.form.Ichiran.Rows[e.RowIndex].Cells[Const.BikoUchiwakeNyuryokuConstans.BANK_SHITEN_CD].Value)))
            //    {
            //        this.form.Ichiran.Rows[e.RowIndex].Cells[Const.BikoUchiwakeNyuryokuConstans.RENKEI_CD].Value = this.form.Ichiran.Rows[e.RowIndex].Cells[Const.BikoUchiwakeNyuryokuConstans.BANK_SHITEN_CD].Value;

            //        if (!this.CheckValidRenkeiCd(this.form.Ichiran.Rows[e.RowIndex]))
            //        {
            //            this.errorCell = this.form.Ichiran.Rows[e.RowIndex].Cells[Const.BikoUchiwakeNyuryokuConstans.RENKEI_CD].CellIndex;
            //            this.errorRow = e.RowIndex;
            //            this.isRenkeiError = true;

            //            MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
            //            msgLogic.MessageBoxShow("E022", "入力された連携用CD");
            //        }
            //    }
            //}

            //LogUtility.DebugMethodEnd();
        }

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.BIKO_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["BIKO_CD"]).Where(c => c.Value != null).ToList().
                                            ForEach(c =>
                                            {
                                                c.ReadOnly = allEntityList.Contains(c.Value.ToString());
                                                c.UpdateBackColor(false);
                                            });

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245", string.Empty);
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 検索条件が数字のみ入力.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CONDITION_VALUE_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.form.CONDITION_VALUE.DBFieldsName.Equals(Const.BikoUchiwakeNyuryokuConstans.BIKO_CD))
            {
                if ((e.KeyChar < '0' || '9' < e.KeyChar) && e.KeyChar != (char)Keys.Enter && e.KeyChar != (char)Keys.Tab && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        /// <summary>
        /// オプションの有無を確認して、明細行のオンラインバンク連携に関する項目の表示を制御する
        /// </summary>
        /// <returns></returns>
        //private bool setOnlineBankVisible()
        //{
        //    bool onlineBankVisible = r_framework.Configuration.AppConfig.AppOptions.IsOnlinebank();

        //    if (!onlineBankVisible)
        //    {
        //        int currentWidth = this.form.Ichiran.Columns["RENKEI_CD"].Width;
        //        int nextLeft = this.form.Ichiran.Columns["BANK_SHITEN_BIKOU"].Left;
        //        int nextWidth = this.form.Ichiran.Columns["BANK_SHITEN_BIKOU"].Width;

        //        // 連携CD列を不可視にする
        //        this.ChangePropertyForGC(new string[] { "gcCustomColumnHeader10" }, new string[] { "RENKEI_CD" }, "Visible", false);
        //        // 連携CDの分だけ、次の項目の幅を拡げる
        //        this.ChangePropertyForGC(new string[] { "gcCustomColumnHeader9" }, new string[] { "BANK_SHITEN_BIKOU" }, "Location", new System.Drawing.Point(nextLeft - currentWidth, 0));
        //        this.ChangePropertyForGC(new string[] { "gcCustomColumnHeader9" }, new string[] { "BANK_SHITEN_BIKOU" }, "Size", new System.Drawing.Size(nextWidth + currentWidth, 21));
        //    }
        //    return onlineBankVisible;
        //}

        /// <summary>
        /// 明細の制御
        /// </summary>
        /// <param name="headerNames">対象カラムヘッダー名一覧</param>
        /// <param name="cellNames">対象セル名一覧</param>
        /// <param name="value">プロパティの新しい値</param>
        private void ChangePropertyForGC(string[] headerNames, string[] cellNames, string propertyName, object value)
        {
            this.form.Ichiran.SuspendLayout();

            ControlUtility controlUtil = new ControlUtility();
            var newTemplate = this.form.Ichiran.Template;

            if (headerNames != null && 0 < headerNames.Length)
            {
                var obj1 = controlUtil.FindControl(newTemplate.ColumnHeaders[0].Cells.ToArray(), headerNames);
                foreach (var o in obj1)
                {
                    PropertyUtility.SetValue(o, propertyName, value);
                }
            }

            if (cellNames != null && 0 < cellNames.Length)
            {
                var obj2 = controlUtil.FindControl(newTemplate.Row.Cells.ToArray(), cellNames);
                foreach (var o in obj2)
                {
                    PropertyUtility.SetValue(o, propertyName, value);
                }
            }

            this.form.Ichiran.Template = newTemplate;

            this.form.Ichiran.ResumeLayout();
        }

        //20250409
        public bool BikoKbnCdValidating(object sender, CancelEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                string bikoCd = this.form.BIKO_KBN_CD.Text; 

                if (!string.IsNullOrEmpty(bikoCd))
                {
                    M_BIKO_PATAN_NYURYOKU c = new M_BIKO_PATAN_NYURYOKU();
                    c.BIKO_CD = bikoCd;
                    M_BIKO_PATAN_NYURYOKU[] biko = this.bikoPatanDao.GetAllValidData(c);

                    if (biko != null && biko.Length > 0)
                    {
                        this.form.BIKO_NAME_RYAKU.Text = biko[0].BIKO_NAME_RYAKU;
                    }
                    else
                    {
                        this.form.BIKO_NAME_RYAKU.Text = string.Empty;
                        this.form.BIKO_KBN_CD.IsInputErrorOccured = true;
                        this.form.BIKO_KBN_CD.UpdateBackColor(false);
                        msgLogic.MessageBoxShowError("備考パターンCDマスタに存在しないコードが入力されました。");
                        e.Cancel = true;
                    }
                }
                else
                {
                    this.form.BIKO_KBN_CD.IsInputErrorOccured = false;
                    this.form.BIKO_KBN_CD.UpdateBackColor(false);
                    this.form.BIKO_NAME_RYAKU.Text = string.Empty;
                    //this.form.Ichiran.DataSource = null;
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("BikoKBNCDValidating ", ex);
                this.form.errmessage.MessageBoxShowError("サンプルCDは存在しませんのでご注意ください");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        //20250410
        internal bool InsertRow()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.Ichiran.CurrentRow == null)
                {
                    return false;
                }

                this.form.Ichiran.CellValidating -= this.form.Ichiran_CellValidating;
                this.form.Ichiran.CellValidated -= this.form.Ichiran_CellValidated;

                var dataTable = (DataTable)this.form.Ichiran.DataSource;

                int insertIndex = this.form.Ichiran.CurrentRow.IsNewRow ? dataTable.Rows.Count : this.form.Ichiran.CurrentRow.Index;

                dataTable.Rows.InsertAt(dataTable.NewRow(), insertIndex);

                if (insertIndex > 0 && insertIndex < this.form.Ichiran.Rows.Count)
                {
                    this.form.Ichiran.Rows[insertIndex].Cells["BIKO_CD"].Selected = true;
                }

                this.form.Ichiran.Focus();

                this.form.Ichiran.CellValidating += this.form.Ichiran_CellValidating;
                this.form.Ichiran.CellValidated += this.form.Ichiran_CellValidated;
            }
            catch (InvalidOperationException ex)
            {
                LogUtility.Error("InsertRow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            catch (Exception ex1)
            {
                LogUtility.Error("InsertRow", ex1);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            finally
            {
                LogUtility.DebugMethodEnd(ret);
            }
            return ret;
        }

        internal bool DeleteRow()
        {
            bool ret = true;
            try
            {
                LogUtility.DebugMethodStart();

                if (this.form.Ichiran.CurrentRow == null || this.form.Ichiran.CurrentRow.IsNewRow)
                {
                    return false;
                }

                if (this.form.Ichiran.CurrentRow.Index == this.form.Ichiran.RowCount - 1)
                {
                    return ret;
                }

                var dataTable = (DataTable)this.form.Ichiran.DataSource;

                dataTable.Rows.RemoveAt(this.form.Ichiran.CurrentRow.Index);
            }
            catch (InvalidOperationException ex)
            {
                LogUtility.Error("DeleteRow", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                ret = false;
            }
            catch (Exception ex1)
            {
                LogUtility.Error("DeleteRow", ex1);
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