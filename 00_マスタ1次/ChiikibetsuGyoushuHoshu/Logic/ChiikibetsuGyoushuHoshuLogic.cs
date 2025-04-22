// $Id: ChiikibetsuGyoushuHoshuLogic.cs 51723 2015-06-08 06:14:52Z hoangvu@e-mall.co.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using ChiikibetsuGyoushuHoshu.APP;
using ChiikibetsuGyoushuHoshu.Validator;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
using MasterCommon.Utility;
using Microsoft.VisualBasic;
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

namespace ChiikibetsuGyoushuHoshu.Logic
{
    /// <summary>
    /// 地域別業種保守画面のビジネスロジック
    /// </summary>
    public class ChiikibetsuGyoushuHoshuLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "ChiikibetsuGyoushuHoshu.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_CHIIKIBETSU_GYOUSHU_DATA_SQL = "ChiikibetsuGyoushuHoshu.Sql.GetIchiranDataSql.sql";

        private readonly string GET_CHIIKIBETSU_GYOUSHU_DATA_SQL = "ChiikibetsuGyoushuHoshu.Sql.GetChiikibetsuGyoushuDataSql.sql";

        private readonly string GET_CHIIKIBETSU_GYOUSHU_BY_CD_SQL = "ChiikibetsuGyoushuHoshu.Sql.GetChiikibetsuGyoushuByCdSql.sql";

        private readonly string GET_CHIIKIBETSU_GYOUSHU_STRUCT_SQL = "ChiikibetsuGyoushuHoshu.Sql.GetIchiranStructSql.sql";

        private readonly string GET_CHIIKIBETSU_GYOUSHU_ALL_SQL = "ChiikibetsuGyoushuHoshu.Sql.GetIchiranAllDataSql.sql";

        /// <summary>
        /// 地域別業種保守画面Form
        /// </summary>
        private ChiikibetsuGyoushuHoshuForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 地域別業種マスタエンティティ
        /// </summary>
        private M_CHIIKIBETSU_GYOUSHU[] entitys;

        /// <summary>
        /// 地域コードマスタエンティティ
        /// </summary>
        private M_CHIIKI chiikiEntity;

        /// <summary>
        /// 全件検索フラグ
        /// </summary>
        private bool isAllSearch;

        /// <summary>
        /// 地域別業種のDao
        /// </summary>
        private IM_CHIIKIBETSU_GYOUSHUDao dao;

        /// <summary>
        /// 地域のDao
        /// </summary>
        private IM_CHIIKIDao chiikiDao;

        /// <summary>
        /// システム設定のDao
        /// </summary>
        private IM_SYS_INFODao daoSysInfo;

        /// <summary>
        /// システム設定のエンティティ
        /// </summary>
        private M_SYS_INFO entitySysInfo;

        // VUNGUYEN 20150525 #1294 START
        public Cell cell;
        // VUNGUYEN 20150525 #1294 END

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
        public M_CHIIKIBETSU_GYOUSHU SearchString { get; set; }

        /// <summary>
        /// 地域CD
        /// </summary>
        public string ChiikiCD { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public ChiikibetsuGyoushuHoshuLogic(ChiikibetsuGyoushuHoshuForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_CHIIKIBETSU_GYOUSHUDao>();
            this.chiikiDao = DaoInitUtility.GetComponent<IM_CHIIKIDao>();
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

                this.form.CHIIKI_CD.Text = Properties.Settings.Default.CHIIKI_CD_TEXT;
                if (!string.IsNullOrEmpty(this.ChiikiCD))
                {
                    this.form.CHIIKI_CD.Text = this.ChiikiCD;
                }
                if (!string.IsNullOrEmpty(this.form.CHIIKI_CD.Text))
                {
                    this.chiikiEntity = this.chiikiDao.GetDataByCd(this.form.CHIIKI_CD.Text);
                    if (this.chiikiEntity != null)
                    {
                        this.form.CHIIKI_NAME.Text = this.chiikiEntity.CHIIKI_NAME_RYAKU;
                    }
                }

                this.form.CONDITION_VALUE.Text = Properties.Settings.Default.ConditionValue_Text;
                this.form.CONDITION_VALUE.DBFieldsName = Properties.Settings.Default.ConditionValue_DBFieldsName;
                this.form.CONDITION_VALUE.ItemDefinedTypes = Properties.Settings.Default.ConditionValue_ItemDefinedTypes;
                this.form.CONDITION_ITEM.Text = Properties.Settings.Default.ConditionItem_Text;

                this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked = Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED;
                if (!this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked)
                {
                    this.SetHyoujiJoukenInit();
                }
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1981
                FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("WindowInit", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
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
            try
            {
                LogUtility.DebugMethodStart();

                SetSearchString();

                // 対象データを抽出する
                this.SearchResult = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_CHIIKIBETSU_GYOUSHU_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);
                this.SearchResultCheck = dao.GetIchiranDataSqlFile(this.GET_ICHIRAN_CHIIKIBETSU_GYOUSHU_DATA_SQL
                                                            , this.SearchString
                                                            , this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked);

                // M_CHIIKIBETSU_GYOUSHUに存在しない項目での絞り込みはデータ取得後に行う
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
                {
                    switch (this.form.CONDITION_VALUE.DBFieldsName)
                    {
                        // 業種名
                        // 全角/半角・ひらがなカタカナ・大文字小文字を区別しないであいまい検索
                        case ("GYOUSHU_NAME_RYAKU"):
                            var table = (
                                from row in this.SearchResult.AsEnumerable()
                                let columnID = row.Field<string>(this.form.CONDITION_VALUE.DBFieldsName)
                                where Strings.StrConv(Strings.StrConv(Strings.StrConv(columnID, VbStrConv.Wide, 0), VbStrConv.ProperCase, 0), VbStrConv.Hiragana, 0)
                                        .Contains(Strings.StrConv(Strings.StrConv(Strings.StrConv(this.form.CONDITION_VALUE.Text, VbStrConv.Wide, 0), VbStrConv.ProperCase, 0), VbStrConv.Hiragana, 0))
                                select row
                                    );
                            var tableCheck = (
                                from row in this.SearchResultCheck.AsEnumerable()
                                let columnID = row.Field<string>(this.form.CONDITION_VALUE.DBFieldsName)
                                where Strings.StrConv(Strings.StrConv(Strings.StrConv(columnID, VbStrConv.Wide, 0), VbStrConv.ProperCase, 0), VbStrConv.Hiragana, 0)
                                        .Contains(Strings.StrConv(Strings.StrConv(Strings.StrConv(this.form.CONDITION_VALUE.Text, VbStrConv.Wide, 0), VbStrConv.ProperCase, 0), VbStrConv.Hiragana, 0))
                                select row
                                    );

                            if (table.Count() != 0)
                            {
                                this.SearchResult = table.CopyToDataTable();
                                this.SearchResultCheck = tableCheck.CopyToDataTable();  //同一SQL文の為、件数チェックはtable.countを使用
                            }
                            else
                            {
                                this.SearchResult.Clear();
                                this.SearchResultCheck.Clear();
                            }
                            break;
                    }
                }
                // 対象外も含め、全データを抽出する
                M_CHIIKIBETSU_GYOUSHU searchParam = new M_CHIIKIBETSU_GYOUSHU();
                searchParam.CHIIKI_CD = this.form.CHIIKI_CD.Text;
                this.SearchResultAll = dao.GetDataBySqlFile(this.GET_CHIIKIBETSU_GYOUSHU_DATA_SQL, searchParam);

                // 全データ抽出フラグをセットする
                this.isAllSearch = this.SearchResult.AsEnumerable().SequenceEqual(this.SearchResultAll.AsEnumerable(), DataRowComparer.Default);

                // 検索条件を保存する
                Properties.Settings.Default.CHIIKI_CD_TEXT = this.form.CHIIKI_CD.Text;

                Properties.Settings.Default.ConditionValue_Text = this.form.CONDITION_VALUE.Text;
                Properties.Settings.Default.ConditionValue_DBFieldsName = this.form.CONDITION_VALUE.DBFieldsName;
                Properties.Settings.Default.ConditionValue_ItemDefinedTypes = this.form.CONDITION_VALUE.ItemDefinedTypes;
                Properties.Settings.Default.ConditionItem_Text = this.form.CONDITION_ITEM.Text;

                Properties.Settings.Default.ICHIRAN_HYOUJI_JOUKEN_DELETED = this.form.ICHIRAN_HYOUJI_JOUKEN_DELETED.Checked;
                Properties.Settings.Default.Save();

                // 行数を取得する
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

                var entityList = new M_CHIIKIBETSU_GYOUSHU[this.form.Ichiran.Rows.Count];
                for (int i = 0; i < entityList.Length; i++)
                {
                    entityList[i] = new M_CHIIKIBETSU_GYOUSHU();
                }

                var dataBinderLogic = new DataBinderLogic<r_framework.Entity.M_CHIIKIBETSU_GYOUSHU>(entityList);

                DataTable dt = this.form.Ichiran.DataSource as DataTable;
                DataTable preDt = new DataTable();
                foreach (DataColumn column in dt.Columns)
                {
                    // NOT NULL制約を一時的に解除(新規追加行対策)
                    column.AllowDBNull = true;

                    // TIME_STAMPがなぜか一意制約有のため、解除
                    if (column.ColumnName.Equals(Const.ChiikibetsuGyoushuHoshuConstans.TIME_STAMP))
                    {
                        column.Unique = false;
                    }
                }

                dt.BeginLoadData();

                preDt = GetCloneDataTable(dt);

                // 変更分のみ取得
                this.form.Ichiran.DataSource = dt.GetChanges();

                var CHIIKIEntityList = dataBinderLogic.CreateEntityForDataTable(this.form.Ichiran);

                List<M_CHIIKIBETSU_GYOUSHU> addList = new List<M_CHIIKIBETSU_GYOUSHU>();
                foreach (var CHIIKIEntity in CHIIKIEntityList)
                {
                    foreach (Row row in this.form.Ichiran.Rows)
                    {
                        if (row.Cells.Any(n => (n.DataField.Equals(Const.ChiikibetsuGyoushuHoshuConstans.GYOUSHU_CD) && n.Value.ToString().Equals(CHIIKIEntity.GYOUSHU_CD))) &&
                            row.Cells.Any(n => (n.DataField.Equals(Const.ChiikibetsuGyoushuHoshuConstans.DELETE_FLG) && bool.Parse(n.FormattedValue.ToString()) == isDelete)))
                        {
                            CHIIKIEntity.CHIIKI_CD = this.form.CHIIKI_CD.Text;
                            MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), CHIIKIEntity);
                            addList.Add(CHIIKIEntity);
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
                this.form.errmessage.MessageBoxShow("E245", "");
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
                this.form.CHIIKI_CD.Focus();
                this.form.Ichiran.AllowUserToAddRows = false; // thongh 2015/12/28 #1981

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Cancel", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        /// <summary>
        /// 報告業種CDの重複チェック
        /// </summary>
        /// <returns></returns>
        public bool DuplicationCheck()
        {
            try
            {
                LogUtility.DebugMethodStart();

                ChiikibetsuGyoushuHoshuValidator vali = new ChiikibetsuGyoushuHoshuValidator();
                bool result = vali.CHIIKICDValidator(this.form.Ichiran, this.SearchResultCheck, this.SearchResultAll, this.isAllSearch);

                LogUtility.DebugMethodEnd();

                return result;
            }
            catch (Exception ex)
            {
                LogUtility.Error("DuplicationCheck", ex);
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
                msgLogic.MessageBoxShow("C011", "地域業種一覧表");

                MessageBox.Show("未実装");

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("Preview", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
                    multirowLocationLogic.multiRow = (r_framework.CustomControl.GcCustomMultiRow)this.form.Ichiran;

                    multirowLocationLogic.CreateLocations();

                    // VUNGUYEN 20150525 #1294 START
                    CSVFileLogicCustom csvLogic = new CSVFileLogicCustom();
                    // VUNGUYEN 20150525 #1294 END

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = (r_framework.CustomControl.GcCustomMultiRow)this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("CSV", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
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
                this.form.CHIIKI_CD.Focus();

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

                DataTable dt;

                //独自チェックの記述例を書く
                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_CHIIKIBETSU_GYOUSHU CHIIKIEntity in this.entitys)
                        {
                            dt = dao.GetDataBySqlFile(this.GET_CHIIKIBETSU_GYOUSHU_BY_CD_SQL, CHIIKIEntity);
                            if (dt.Rows.Count <= 0)
                            {
                                //削除チェックが付けられている場合は、新規登録を行わな
                                if (CHIIKIEntity.DELETE_FLG)
                                {
                                    continue;
                                }

                                this.dao.Insert(CHIIKIEntity);
                            }
                            else
                            {
                                this.dao.Update(CHIIKIEntity);
                            }

                            UpdateRelationInfo(CHIIKIEntity, false);
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
                this.form.errmessage.MessageBoxShow("E093", "");
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

                DataTable dt;

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                var result = msgLogic.MessageBoxShow("C021");
                if (result == DialogResult.Yes)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_CHIIKIBETSU_GYOUSHU CHIIKIEntity in this.entitys)
                        {
                            dt = dao.GetDataBySqlFile(this.GET_CHIIKIBETSU_GYOUSHU_BY_CD_SQL, CHIIKIEntity);
                            if (dt.Rows.Count > 0)
                            {
                                this.dao.Update(CHIIKIEntity);
                                UpdateRelationInfo(CHIIKIEntity, true);
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

            ChiikibetsuGyoushuHoshuLogic localLogic = other as ChiikibetsuGyoushuHoshuLogic;
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
        internal bool SetIchiran(bool isRead)
        {
            try
            {
                var table = this.SearchResult.Clone();
                if (isRead)
                {
                    DataTable temp = this.SearchResult.Clone();
                    for (int i = 0; i < this.SearchResult.Rows.Count; i++)
                    {
                        bool isKey = false;
                        foreach (DataRow dr in this.SearchResultAll.Rows)
                        {
                            if (SearchResult.Rows[i]["CHIIKI_CD"].ToString() == dr["CHIIKI_CD"].ToString()
                                && SearchResult.Rows[i]["GYOUSHU_CD"].ToString() == dr["GYOUSHU_CD"].ToString())
                            {
                                DataRow row = table.NewRow();
                                row["CHIIKI_CD"] = dr["CHIIKI_CD"];
                                row["GYOUSHU_CD"] = dr["GYOUSHU_CD"];
                                row["HOUKOKU_GYOUSHU_CD"] = SearchResult.Rows[i]["HOUKOKU_GYOUSHU_CD"];
                                row["GYOUSHU_NAME_RYAKU"] = SearchResult.Rows[i]["GYOUSHU_NAME_RYAKU"];
                                row["HOUKOKU_GYOUSHU_NAME"] = SearchResult.Rows[i]["HOUKOKU_GYOUSHU_NAME"];
                                row["CHIIKIBETSU_GYOUSHU_BIKOU"] = SearchResult.Rows[i]["CHIIKIBETSU_GYOUSHU_BIKOU"];
                                row["CREATE_USER"] = dr["CREATE_USER"];
                                row["CREATE_DATE"] = dr["CREATE_DATE"];
                                row["CREATE_PC"] = dr["CREATE_PC"];
                                row["DELETE_FLG"] = false;
                                row["TIME_STAMP"] = dr["TIME_STAMP"];
                                table.Rows.Add(row);
                                isKey = true;
                            }
                        }

                        if (!isKey)
                        {
                            DataRow row = table.NewRow();
                            row["CHIIKI_CD"] = SearchResult.Rows[i]["CHIIKI_CD"];
                            row["GYOUSHU_CD"] = SearchResult.Rows[i]["GYOUSHU_CD"];
                            row["GYOUSHU_NAME_RYAKU"] = SearchResult.Rows[i]["GYOUSHU_NAME_RYAKU"];
                            row["HOUKOKU_GYOUSHU_CD"] = SearchResult.Rows[i]["HOUKOKU_GYOUSHU_CD"];
                            row["HOUKOKU_GYOUSHU_NAME"] = SearchResult.Rows[i]["HOUKOKU_GYOUSHU_NAME"];
                            row["CHIIKIBETSU_GYOUSHU_BIKOU"] = SearchResult.Rows[i]["CHIIKIBETSU_GYOUSHU_BIKOU"];
                            row["DELETE_FLG"] = false;
                            table.Rows.Add(row);
                        }
                    }
                }
                else
                {
                    table = this.SearchResult;
                }
                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.DataSource = table;

                // 権限チェック
                if (r_framework.Authority.Manager.CheckAuthority("M238", r_framework.Const.WINDOW_TYPE.UPDATE_WINDOW_FLAG, false))
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

            //業種読込ボタン(process1)イベント生成
            parentForm.bt_process1.Click += new EventHandler(this.ReadGyoushu);

            //ESCテキストイベント生成
            parentForm.txb_process.KeyDown += new KeyEventHandler(txb_process_KeyDown);
            parentForm.txb_process.MaxLength = 1;
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
            M_CHIIKIBETSU_GYOUSHU entity = new M_CHIIKIBETSU_GYOUSHU();

            // 地域CDの設定
            entity.CHIIKI_CD = this.form.CHIIKI_CD.Text;

            if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.DBFieldsName))
            {
                if (!string.IsNullOrEmpty(this.form.CONDITION_VALUE.ItemDefinedTypes))
                {
                    // M_CHIIKIBETSU_GYOUSHUに存在しない項目は検索条件に含めない
                    switch (this.form.CONDITION_VALUE.DBFieldsName)
                    {
                        case ("GYOUSHU_NAME_RYAKU"):
                            break;

                        default:
                            // 検索条件の設定
                            entity.SetValue(this.form.CONDITION_VALUE);
                            break;
                    }
                }
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
            this.form.CHIIKI_CD.Text = string.Empty;
            this.form.CHIIKI_NAME.Text = string.Empty;

            // 検索条件クリア
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            this.SetHyoujiJoukenInit();
            FunctionControl.ControlFunctionButton((MasterBaseForm)this.form.ParentForm, false);
        }

        #region 20150416 minhhoang edit #1748

        /// <summary>
        /// 検索条件初期化
        /// </summary>
        private void ClearConditionF7()
        {
            // 検索条件クリア
            this.form.CONDITION_VALUE.Text = string.Empty;
            this.form.CONDITION_VALUE.DBFieldsName = string.Empty;
            this.form.CONDITION_VALUE.ItemDefinedTypes = string.Empty;
            this.form.CONDITION_ITEM.Text = string.Empty;

            this.SetHyoujiJoukenInit();
        }

        #endregion

        /// <summary>
        /// 地域別業種保守に関連する情報の更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isDelete"></param>
        private void UpdateRelationInfo(M_CHIIKIBETSU_GYOUSHU entity, bool isDelete)
        {
            if (entity == null
                || string.IsNullOrEmpty(entity.CHIIKI_CD))
            {
                return;
            }
        }

        /// <summary>
        /// ESCテキストボックスKeyUp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void txb_process_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                LogUtility.DebugMethodStart(sender, e);

                // エンターキーが押された場合
                if (e.KeyCode == Keys.Enter)
                {
                    string proc = ((MasterBaseForm)this.form.Parent).txb_process.Text;
                    if (!string.IsNullOrWhiteSpace(proc))
                    {
                        if (proc.Equals("1"))
                        {
                            this.ReadGyoushu(sender, e);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex.Message);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 業種読込ボタン押下処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReadGyoushu(object sender, EventArgs e)
        {
            try
            {
                LogUtility.DebugMethodEnd(sender, e);

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();

                // 地域コード入力チェック
                if (string.IsNullOrWhiteSpace(this.form.CHIIKI_CD.Text))
                {
                    msgLogic.MessageBoxShow("E001", "地域");
                    this.form.CHIIKI_CD.Focus();
                    return;
                }

                // 問い合わせ確認
                if (msgLogic.MessageBoxShow("C066", "報告用業種", "業種") != DialogResult.Yes)
                {
                    this.form.CHIIKI_CD.Focus();
                    return;
                }

                this.form.Ichiran.DataSource = null;
                this.form.Ichiran.AllowUserToAddRows = true;//thongh 2015/12/28 #1981
                this.SearchResult = null;
                this.SearchResultAll = null;
                this.isAllSearch = false;

                // DataTable構造作成
                this.SearchResult = this.dao.GetDataBySqlFile(this.GET_CHIIKIBETSU_GYOUSHU_STRUCT_SQL, null);
                this.SearchResultAll = this.dao.GetDataBySqlFile(this.GET_CHIIKIBETSU_GYOUSHU_ALL_SQL, null);
                for (int i = 0; i < this.SearchResult.Columns.Count; i++)
                {
                    this.SearchResult.Columns[i].AllowDBNull = true;
                    this.SearchResult.Columns[i].ReadOnly = false;
                    this.SearchResult.Columns[i].Unique = false;
                }

                // 業種読込処理
                M_GYOUSHU[] gyoushu = DaoInitUtility.GetComponent<IM_GYOUSHUDao>().GetAllValidData(new M_GYOUSHU());
                if (gyoushu != null && gyoushu.Length > 0)
                {
                    for (int i = 0; i < gyoushu.Length; i++)
                    {
                        DataRow row = this.SearchResult.NewRow();
                        row["CHIIKI_CD"] = this.form.CHIIKI_CD.Text;
                        row["GYOUSHU_CD"] = gyoushu[i].GYOUSHU_CD;
                        row["GYOUSHU_NAME_RYAKU"] = gyoushu[i].GYOUSHU_NAME_RYAKU;
                        row["HOUKOKU_GYOUSHU_CD"] = gyoushu[i].GYOUSHU_CD;
                        row["HOUKOKU_GYOUSHU_NAME"] = stringCut(gyoushu[i].GYOUSHU_NAME, this.SearchResult.Columns["HOUKOKU_GYOUSHU_NAME"].MaxLength);
                        row["CHIIKIBETSU_GYOUSHU_BIKOU"] = gyoushu[i].GYOUSHU_BIKOU;
                        this.SearchResult.Rows.Add(row);
                    }
                }

                // 検索結果表示処理
                this.SetIchiran(true);

                // フォーカス移動
                this.form.Ichiran.Focus();
            }
            catch (Exception ex)
            {
                LogUtility.Error(ex.Message);
                throw ex;
            }
            finally
            {
                LogUtility.DebugMethodEnd();
            }
        }

        /// <summary>
        /// 固定長さを戻す
        /// </summary>
        /// <param name="temp"></param>
        /// <param name="intLen"></param>
        /// <returns></returns>
        private string stringCut(string temp, int intLen)
        {
            string retStr = temp;

            if (!string.IsNullOrEmpty(retStr))
            {
                if (retStr.Length > intLen)
                {
                    retStr = retStr.Substring(0, intLen);
                }
            }

            return retStr;
        }

        /// <summary>
        /// 主キーが同一の行がDBに存在する場合、主キーを非活性にする
        /// </summary>
        internal bool EditableToPrimaryKey()
        {
            try
            {
                // DBから主キーのListを取得
                var allEntityList = this.dao.GetAllData().Select(s => s.GYOUSHU_CD).Where(s => !string.IsNullOrEmpty(s)).ToList();

                // DBに存在する行の主キーを非活性にする
                this.form.Ichiran.Rows.Select(r => r.Cells["GYOUSHU_CD"]).Where(c => c.Value != null).ToList().
                                            ForEach(c =>
                                            {
                                                c.ReadOnly = allEntityList.Contains(c.Value.ToString());
                                                c.UpdateBackColor(false);
                                            });
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("EditableToPrimaryKey", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("EditableToPrimaryKey", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }
    }
}