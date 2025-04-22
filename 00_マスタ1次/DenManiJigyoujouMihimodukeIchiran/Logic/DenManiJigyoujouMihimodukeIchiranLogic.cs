// $Id: DenManiJigyoujouMihimodukeIchiranLogic.cs 19527 2014-04-18 06:03:51Z sc.n.tanaka $
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using DenManiJigyoujouMihimodukeIchiran.APP;
using GrapeCity.Win.MultiRow;
using MasterCommon.Logic;
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

namespace DenManiJigyoujouMihimodukeIchiran.Logic
{
    /// <summary>
    /// 電マニ事業場未紐付一覧画面のビジネスロジック
    /// </summary>
    public class DenManiJigyoujouMihimodukeIchiranLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "DenManiJigyoujouMihimodukeIchiran.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_DATA_SQL = "DenManiJigyoujouMihimodukeIchiran.Sql.GetIchiranDataSql.sql";

        private readonly string GET_PRIMARY_KEY_SQL = "DenManiJigyoujouMihimodukeIchiran.Sql.GetPrimaryKeySql.sql";

        private readonly string UPDATE_DATA_SQL = "DenManiJigyoujouMihimodukeIchiran.Sql.UpdateDataSql.sql";

        /// <summary>
        /// 電マニ事業場未紐付一覧画面Form
        /// </summary>
        private DenManiJigyoujouMihimodukeIchiranForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 電子事業場のエンティティ
        /// </summary>
        private M_DENSHI_JIGYOUJOU[] entitys;

        /// <summary>
        /// 電子事業場のDao
        /// </summary>
        private IM_DENSHI_JIGYOUJOUDao dao;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

        /// <summary>
        /// 現場のDao
        /// </summary>
        private IM_GENBADao genbaDao;

        /// <summary>
        /// 変更前業者
        /// </summary>
        internal string befGyoushaCd { get; set; }

        #endregion

        #region プロパティ

        /// <summary>
        /// 検索結果
        /// </summary>
        public DataTable SearchResult { get; set; }

        #endregion

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="targetForm"></param>
        public DenManiJigyoujouMihimodukeIchiranLogic(DenManiJigyoujouMihimodukeIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUJOUDao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();
            this.genbaDao = DaoInitUtility.GetComponent<IM_GENBADao>();

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

                this.form.GYOUSHA_CD.Text = Properties.Settings.Default.GYOUSHA_CD_TEXT;
                this.form.GENBA_CD.Text = Properties.Settings.Default.GENBA_CD_TEXT;
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text))
                {
                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                    if (gyousha != null)
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
                    }
                }
                if (!string.IsNullOrWhiteSpace(this.form.GENBA_CD.Text))
                {
                    M_GENBA genbaSearch = new M_GENBA();
                    genbaSearch.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    genbaSearch.GENBA_CD = this.form.GENBA_CD.Text;
                    M_GENBA genba = new M_GENBA();
                    genba = this.genbaDao.GetDataByCd(genbaSearch);
                    if (genba != null)
                    {
                        this.form.GENBA_NAME.Text = genba.GENBA_NAME_RYAKU;
                    }
                }

                LogUtility.DebugMethodEnd(false);
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("WindowInit", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
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
        /// データ取得処理
        /// </summary>
        /// <returns></returns>
        public int Search()
        {
            try
            {
                LogUtility.DebugMethodStart();

                this.SearchResult = this.dao.GetDataBySqlFile(this.GET_ICHIRAN_DATA_SQL, new M_DENSHI_JIGYOUJOU());

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

                DataRowView drv;
                DataRow r;
                M_DENSHI_JIGYOUJOU jigyoujou;
                var dataBinderLogic = new DataBinderLogic<M_DENSHI_JIGYOUSHA>(new M_DENSHI_JIGYOUSHA());
                List<M_DENSHI_JIGYOUJOU> addList = new List<M_DENSHI_JIGYOUJOU>();
                SelectedRowCollection rows = this.form.Ichiran.SelectedRows;
                foreach (Row row in rows)
                {
                    // 更新用エンティティの作成
                    drv = (DataRowView)row.DataBoundItem;
                    r = drv.Row;
                    jigyoujou = new M_DENSHI_JIGYOUJOU();
                    jigyoujou.EDI_MEMBER_ID = r["EDI_MEMBER_ID"].ToString();
                    jigyoujou.JIGYOUJOU_CD = r["JIGYOUJOU_CD"].ToString();
                    jigyoujou.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    jigyoujou.GENBA_CD = this.form.GENBA_CD.Text;
                    dataBinderLogic.SetSystemProperty(jigyoujou, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), jigyoujou);
                    addList.Add(jigyoujou);
                }
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
        /// プレビュー
        /// </summary>
        public bool Preview()
        {
            try
            {
                LogUtility.DebugMethodStart();

                MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                msgLogic.MessageBoxShow("C011", "電子事業場未紐付一覧表");

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
                    multirowLocationLogic.multiRow = this.form.Ichiran;

                    multirowLocationLogic.CreateLocations();

                    CSVFileLogic csvLogic = new CSVFileLogic();

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = this.form.Ichiran;

                    WINDOW_ID id = this.form.WindowId;

                    csvLogic.FileName = id.ToTitleString();
                    csvLogic.headerOutputFlag = true;

                    csvLogic.CreateCSVFile(this.form);

                    #region 新しいCSV出力利用するように、余計なメッセージを削除
                    //msgLogic.MessageBoxShow("I000");
                    #endregion
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


                //エラーではない場合登録処理を行う
                if (!errorFlag)
                {
                    // トランザクション開始
                    using (var tran = new Transaction())
                    {
                        foreach (M_DENSHI_JIGYOUJOU jigyoujou in this.entitys)
                        {
                            this.SearchResult = dao.GetDataBySqlFile(this.GET_PRIMARY_KEY_SQL, jigyoujou);
                            if (this.SearchResult.Rows.Count > 0)
                            {
                                this.dao.GetDataBySqlFile(this.UPDATE_DATA_SQL, jigyoujou);
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");

                    this.form.GYOUSHA_CD.Text = string.Empty;
                    this.form.GYOUSHA_NAME.Text = string.Empty;
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME.Text = string.Empty;

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
            throw new NotImplementedException();
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

            DenManiJigyoujouMihimodukeIchiranLogic localLogic = other as DenManiJigyoujouMihimodukeIchiranLogic;
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
                var table = this.SearchResult;

                table.BeginLoadData();

                for (int i = 0; i < table.Columns.Count; i++)
                {
                    table.Columns[i].ReadOnly = false;
                }

                this.form.Ichiran.IsBrowsePurpose = false;
                this.form.Ichiran.DataSource = table;
                this.form.Ichiran.IsBrowsePurpose = true;

                // 業者区分名設定処理
                bool catchErr = this.SetGyoushaKbnName();
                return catchErr;
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

            //ﾌﾟﾚﾋﾞｭｰボタン(F5)イベント生成
            parentForm.bt_func5.Click += new EventHandler(this.form.Preview);

            //CSVボタン(F6)イベント生成
            parentForm.bt_func6.Click += new EventHandler(this.form.CSV);

            //登録ボタン(F9)イベント生成
            this.form.C_Regist(parentForm.bt_func9);
            parentForm.bt_func9.Click += new EventHandler(this.form.Regist);
            parentForm.bt_func9.ProcessKbn = PROCESS_KBN.NEW;

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
        /// 業者区分名設定処理
        /// </summary>
        internal bool SetGyoushaKbnName()
        {
            try
            {
                for (int i = 0; i < this.form.Ichiran.RowCount; i++)
                {
                    switch ((this.form.Ichiran.Rows[i].Cells["JIGYOUSHA_KBN"].Value).ToString())
                    {
                        case "1":
                            this.form.Ichiran.Rows[i].Cells["JIGYOUSHA_KBN_NAME"].Value = "排出事業者";
                            break;
                        case "2":
                            this.form.Ichiran.Rows[i].Cells["JIGYOUSHA_KBN_NAME"].Value = "運搬業者";
                            break;
                        case "3":
                            this.form.Ichiran.Rows[i].Cells["JIGYOUSHA_KBN_NAME"].Value = "処分業者";
                            break;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                LogUtility.Error("SetGyoushaKbnName", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                return true;
            }
        }

        // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 STR
        internal bool GyoushaValidated()
        {
            try
            {
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    this.form.GYOUSHA_NAME.Text = string.Empty;
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME.Text = string.Empty;
                    return false;
                }

                if (this.befGyoushaCd != this.form.GYOUSHA_CD.Text)
                {
                    // 現場情報初期化
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_NAME.Text = string.Empty;
                }

                M_GYOUSHA entity = new M_GYOUSHA();
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                var entitys = this.gyoushaDao.GetAllValidData(entity);
                if (entitys != null && entitys.Length > 0)
                {
                    this.form.GYOUSHA_NAME.Text = entitys[0].GYOUSHA_NAME_RYAKU;
                }
                else
                {
                    this.form.GYOUSHA_NAME.Text = string.Empty;
                    var messagelog = new MessageBoxShowLogic();
                    messagelog.MessageBoxShow("E020", "業者");
                    this.form.GYOUSHA_CD.Focus();
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GyoushaValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GyoushaValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }

        internal bool GenbaValidated()
        {
            try
            {
                if (string.IsNullOrEmpty(this.form.GENBA_CD.Text))
                {
                    this.form.GENBA_NAME.Text = string.Empty;
                    return false;
                }

                var messagelog = new MessageBoxShowLogic();
                if (string.IsNullOrEmpty(this.form.GYOUSHA_CD.Text))
                {
                    messagelog.MessageBoxShow("E051", "業者");
                    this.form.GENBA_CD.Text = string.Empty;
                    this.form.GENBA_CD.Focus();
                    return false;
                }

                M_GENBA entity = new M_GENBA();
                entity.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                entity.GENBA_CD = this.form.GENBA_CD.Text;
                var genba = this.genbaDao.GetAllValidData(entity);
                if (genba != null && genba.Length > 0)
                {
                    this.form.GENBA_NAME.Text = genba[0].GENBA_NAME_RYAKU;
                }
                else
                {
                    this.form.GENBA_NAME.Text = string.Empty;
                    messagelog.MessageBoxShow("E020", "現場");
                    this.form.GENBA_CD.Focus();
                }
                return false;
            }
            catch (SQLRuntimeException ex2)
            {
                LogUtility.Error("GenbaValidated", ex2);
                this.form.errmessage.MessageBoxShow("E093", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
            catch (Exception ex)
            {
                LogUtility.Error("GenbaValidated", ex);
                this.form.errmessage.MessageBoxShow("E245", "");
                LogUtility.DebugMethodEnd(true);
                return true;
            }
        }
        // 20150917 BUNN #12111 取引先、業者、現場の各ＣＤの親子関係に関する制御 END
    }
}
