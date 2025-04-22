// $Id: DenManiJigyoushaMihimodukeIchiranLogic.cs 19518 2014-04-18 04:59:22Z sc.m.moriya@willwave.jp $
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using DenManiJigyoushaMihimodukeIchiran.APP;
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

namespace DenManiJigyoushaMihimodukeIchiran.Logic
{
    /// <summary>
    /// (電マニ)事業者未紐付一覧画面のビジネスロジック
    /// </summary>
    public class DenManiJigyoushaMihimodukeIchiranLogic : IBuisinessLogic
    {
        #region フィールド

        private readonly string ButtonInfoXmlPath = "DenManiJigyoushaMihimodukeIchiran.Setting.ButtonSetting.xml";

        private readonly string GET_ICHIRAN_DATA_SQL = "DenManiJigyoushaMihimodukeIchiran.Sql.GetIchiranDataSql.sql";

        private readonly string GET_PRIMARY_KEY_SQL = "DenManiJigyoushaMihimodukeIchiran.Sql.GetPrimaryKeySql.sql";

        private readonly string UPDATE_DATA_SQL = "DenManiJigyoushaMihimodukeIchiran.Sql.UpdateDataSql.sql";

        /// <summary>
        /// 電マニ事業者未紐付一覧画面Form
        /// </summary>
        private DenManiJigyoushaMihimodukeIchiranForm form;

        /// <summary>
        /// Form画面で使用されている全てのカスタムコントロール
        /// </summary>
        private Control[] allControl;

        /// <summary>
        /// 電子事業者のエンティティ
        /// </summary>
        private M_DENSHI_JIGYOUSHA[] entitys;

        /// <summary>
        /// 電子事業者のDao
        /// </summary>
        private IM_DENSHI_JIGYOUSHADao dao;

        /// <summary>
        /// 業者のDao
        /// </summary>
        private IM_GYOUSHADao gyoushaDao;

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
        public DenManiJigyoushaMihimodukeIchiranLogic(DenManiJigyoushaMihimodukeIchiranForm targetForm)
        {
            LogUtility.DebugMethodStart(targetForm);

            this.form = targetForm;
            this.dao = DaoInitUtility.GetComponent<IM_DENSHI_JIGYOUSHADao>();
            this.gyoushaDao = DaoInitUtility.GetComponent<IM_GYOUSHADao>();

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
                if (!string.IsNullOrWhiteSpace(this.form.GYOUSHA_CD.Text))
                {
                    M_GYOUSHA gyousha = new M_GYOUSHA();
                    gyousha = this.gyoushaDao.GetDataByCd(this.form.GYOUSHA_CD.Text);
                    if (gyousha != null)
                    {
                        this.form.GYOUSHA_NAME.Text = gyousha.GYOUSHA_NAME_RYAKU;
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

                this.SearchResult = this.dao.GetDataBySqlFile(this.GET_ICHIRAN_DATA_SQL, new M_DENSHI_JIGYOUSHA());

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
                M_DENSHI_JIGYOUSHA jigyousha;
                var dataBinderLogic = new DataBinderLogic<M_DENSHI_JIGYOUSHA>(new M_DENSHI_JIGYOUSHA());
                List<M_DENSHI_JIGYOUSHA> addList = new List<M_DENSHI_JIGYOUSHA>();
                SelectedRowCollection rows = this.form.Ichiran.SelectedRows;
                foreach (Row row in rows)
                {
                    // 更新用エンティティの作成
                    drv = (DataRowView)row.DataBoundItem;
                    r = drv.Row;
                    jigyousha = new M_DENSHI_JIGYOUSHA();
                    jigyousha.EDI_MEMBER_ID = r["EDI_MEMBER_ID"].ToString();
                    jigyousha.GYOUSHA_CD = this.form.GYOUSHA_CD.Text;
                    dataBinderLogic.SetSystemProperty(jigyousha, false);
                    MasterCommonLogic.SetFooterProperty(MasterCommonLogic.GetCurrentShain(this.form), jigyousha);
                    addList.Add(jigyousha);
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
                msgLogic.MessageBoxShow("C011", "電子事業者未紐付一覧表");

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

                    CSVFileLogic csvLogic = new CSVFileLogic();

                    csvLogic.MultirowLocation = multirowLocationLogic.sortEndList;

                    csvLogic.Detail = (r_framework.CustomControl.GcCustomMultiRow)this.form.Ichiran;

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
                        foreach (M_DENSHI_JIGYOUSHA gyousha in this.entitys)
                        {
                            this.SearchResult = dao.GetDataBySqlFile(this.GET_PRIMARY_KEY_SQL, gyousha);
                            if (this.SearchResult.Rows.Count > 0)
                            {
                                this.dao.GetDataBySqlFile(this.UPDATE_DATA_SQL, gyousha);
                            }
                        }
                        // トランザクション終了
                        tran.Commit();
                    }

                    MessageBoxShowLogic msgLogic = new MessageBoxShowLogic();
                    msgLogic.MessageBoxShow("I001", "登録");

                    this.form.GYOUSHA_CD.Text = string.Empty;
                    this.form.GYOUSHA_NAME.Text = string.Empty;
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

            DenManiJigyoushaMihimodukeIchiranLogic localLogic = other as DenManiJigyoushaMihimodukeIchiranLogic;
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

                this.form.Ichiran.DataSource = table;

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
    }
}

